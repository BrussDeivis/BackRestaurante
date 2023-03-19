using Neodynamic.SDK.Web;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Infraestructura;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Negocio.Almacen;
using Tsp.Sigescom.WebApplication.Models;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public class CommonAlmacenController : BaseController
    {
        protected readonly IActorNegocioLogica actorNegocioLogica;
        protected readonly IMaestroLogica maestroLogica;
        protected readonly IOperacionLogica operacionLogica;
        protected readonly IInventarioHistorico_Logica inventarioHistoricoLogica;
        protected readonly IConfiguracionLogica configuracionLogica;
        protected readonly IGeneracionArchivosLogica generacionArchivosLogica;
        protected readonly IFacturacionElectronicaLogica facturacionElectronicaLogica;
        protected readonly IMailer mailer;
        protected readonly IBarCodeUtil barCodeUtil;

        public CommonAlmacenController() : base()
        {
            actorNegocioLogica = Dependencia.Resolve<IActorNegocioLogica>();
            maestroLogica = Dependencia.Resolve<IMaestroLogica>();
            operacionLogica = Dependencia.Resolve<IOperacionLogica>();
            configuracionLogica = Dependencia.Resolve<IConfiguracionLogica>();
            mailer = Dependencia.Resolve<IMailer>();
            barCodeUtil = Dependencia.Resolve<IBarCodeUtil>();
            generacionArchivosLogica = Dependencia.Resolve<IGeneracionArchivosLogica>();
            facturacionElectronicaLogica = Dependencia.Resolve<IFacturacionElectronicaLogica>();
            inventarioHistoricoLogica = Dependencia.Resolve<IInventarioHistorico_Logica>();

        }

        public ComprobanteDeAlmacen ObtenerMovimientoAlmacen(EstablecimientoComercialExtendidoConLogo sede, List<Proveedor> proveedores, List<Detalle_maestro> modalidades, List<Detalle_maestro> motivos, ComprobanteDeAlmacen comprobanteDeAlmacen)
        {
            try
            {
                MovimientoDeAlmacen movimientoDeAlmacen = operacionLogica.ObtenerMovimientoDeMercaderia(comprobanteDeAlmacen.Id);

                if (movimientoDeAlmacen.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteGuiaDeRemisionRemitente)
                {
                    var ubigeos = maestroLogica.obtenerUbigeo(new int[] { Convert.ToInt32(movimientoDeAlmacen.IdUbigeoOrigenDeTraslado()), Convert.ToInt32(movimientoDeAlmacen.IdUbigeoDestinoDeTraslado()) });
                    movimientoDeAlmacen.UbigeoOrigen = ubigeos.Single(u => u.id == Convert.ToInt32(movimientoDeAlmacen.IdUbigeoOrigenDeTraslado())).descripcion_corta;
                    movimientoDeAlmacen.UbigeoDestino = ubigeos.Single(u => u.id == Convert.ToInt32(movimientoDeAlmacen.IdUbigeoDestinoDeTraslado())).descripcion_corta;
                    var QrBytes = barCodeUtil.ObtenerCodigoQR(movimientoDeAlmacen.Informacion);
                    comprobanteDeAlmacen.CadenaHtmlDeComprobante80 = CoreHtmlStringBuilder.ObtenerHtmlString(movimientoDeAlmacen, FormatoImpresion._80mm, QrBytes, sede, proveedores, modalidades, motivos, this);
                    comprobanteDeAlmacen.CadenaHtmlDeComprobanteA4 = CoreHtmlStringBuilder.ObtenerHtmlString(movimientoDeAlmacen, FormatoImpresion.A4, QrBytes, sede, proveedores, modalidades, motivos, this);
                }
                else if (movimientoDeAlmacen.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaAlmacenInterna)
                {
                    var QrContent = facturacionElectronicaLogica.ObtenerQR(movimientoDeAlmacen, sede);
                    var QrBytes = barCodeUtil.ObtenerCodigoQR(QrContent);
                    comprobanteDeAlmacen.CadenaHtmlDeComprobante80 = CoreHtmlStringBuilder.ObtenerHtmlString(movimientoDeAlmacen, FormatoImpresion._80mm, QrBytes, sede, proveedores, modalidades, motivos, this);
                    comprobanteDeAlmacen.CadenaHtmlDeComprobanteA4 = CoreHtmlStringBuilder.ObtenerHtmlString(movimientoDeAlmacen, FormatoImpresion.A4, QrBytes, sede, proveedores, modalidades, motivos, this);
                }
                else
                {
                    //comprobanteDeAlmacen.CadenaHtmlDeComprobante80 = CoreHtmlStringBuilder.ObtenerHtmlString(movimientoDeAlmacen, FormatoImpresion._80mm, null, sede, null, null, null, this);
                    //comprobanteDeAlmacen.CadenaHtmlDeComprobanteA4 = CoreHtmlStringBuilder.ObtenerHtmlString(movimientoDeAlmacen, FormatoImpresion.A4, null, sede, null, null, null, this);
                    comprobanteDeAlmacen = new ComprobanteDeAlmacen() { Id = movimientoDeAlmacen.IdOperacionReferencia() };
                    ObtenerOrdenMovimientoAlmacen(sede, comprobanteDeAlmacen, movimientoDeAlmacen.IdTipoTransaccionOperacionReferencia());
                }
                return comprobanteDeAlmacen;
            }
            catch (Exception e)
            {
                throw new ControllerException("Error al intentar obtener el movimiento de almacen", e);
            }
        }

        public ComprobanteDeAlmacen ObtenerOrdenMovimientoAlmacen(EstablecimientoComercialExtendidoConLogo sede, ComprobanteDeAlmacen comprobanteDeAlmacen, int idTipoTransaccion)
        {
            try
            {
                if (Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentas.Contains(idTipoTransaccion))
                {
                    OrdenDeVenta ordenDeVenta = operacionLogica.ObtenerOrdenDeVenta(comprobanteDeAlmacen.Id);
                    var QrContentOrden = facturacionElectronicaLogica.ObtenerQR(ordenDeVenta, sede);
                    var QrBytesOrden = barCodeUtil.ObtenerCodigoQR(QrContentOrden);
                    comprobanteDeAlmacen.CadenaHtmlDeComprobante80 = CoreHtmlStringBuilder.ObtenerHtmlString(ordenDeVenta, FormatoImpresion._80mm, QrBytesOrden, sede, this, maestroLogica);
                    comprobanteDeAlmacen.CadenaHtmlDeComprobanteA4 = CoreHtmlStringBuilder.ObtenerHtmlString(ordenDeVenta, FormatoImpresion.A4, QrBytesOrden, sede, this, maestroLogica);
                }
                else if (Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeCompras.Contains(idTipoTransaccion))
                {
                    OrdenDeCompra ordenDecompra = operacionLogica.ObtenerOrdenDeCompra(comprobanteDeAlmacen.Id);
                    comprobanteDeAlmacen.CadenaHtmlDeComprobante80 = CoreHtmlStringBuilder.ObtenerHtmlString(ordenDecompra, FormatoImpresion._80mm, null, sede, this, maestroLogica);
                    comprobanteDeAlmacen.CadenaHtmlDeComprobanteA4 = CoreHtmlStringBuilder.ObtenerHtmlString(ordenDecompra, FormatoImpresion.A4, null, sede, this, maestroLogica);
                }
                //Verificar en el caso de las operaciones que no tienen ordenes, ejemplo: Guia de remision
                return comprobanteDeAlmacen;
            }
            catch (Exception e)
            {
                throw new ControllerException("Error al intentar obtener la orden del movimiento de almacen", e);
            }
        }
    }
}
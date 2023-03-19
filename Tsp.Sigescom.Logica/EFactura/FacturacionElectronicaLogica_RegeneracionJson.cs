using Tsp.Sigescom.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Config;
using Tsp.FacturacionElectronica.Modelo;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Entidades.ComprobantesModel;
using Newtonsoft.Json;
using System.Text;
using OpenInvoicePeru.Comun.Dto.Modelos;
using OpenInvoicePeru.Comun.Dto.Intercambio;
using Tsp.Sigescom.Modelo;
using System.IO;
using System.Threading;
using System.Net;
using System.Linq.Expressions;
using Tsp.Sigescom.Modelo.Entidades.Sesion;
using Tsp.Sigescom.Modelo.Entidades.EFactura;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;
using Tsp.Sigescom.Utilitarios.RestHelper;
using Tsp.Sigescom.Modelo.Interfaces.Infraestructura;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;

namespace Tsp.FacturacionElectronica.Logica
{
    public partial class FacturacionElectronicaLogica
    {
        #region REGENERAR JSON 

        public async Task<OperationResult> RegenerarJsonDocumento(long idDocumento)
        {
            try
            {
                EstablecimientoComercialExtendido sede = _sedeLogica.ObtenerSedeExtendida();
                Documento documento = ObtenerDocumentoElectronicoIncluidoBinario(idDocumento);
                byte[] archivo = null;
                //Dependiendo del tipo de documento se generara nuevamente el json
                if (documento.codigoTipoComprobante == MaestroSettings.Default.CodigoDetalleMaestroBoleta)
                {
                    archivo = RegenerarJsonBoleta(documento.idSigescom, sede);
                }
                else if (documento.codigoTipoComprobante == MaestroSettings.Default.CodigoDetalleMaestroFactura)
                {
                    archivo = RegenerarJsonFactura(documento.idSigescom, sede);
                }
                else if (documento.codigoTipoComprobante == MaestroSettings.Default.CodigoDetalleMaestroNotaDeCredito)
                {
                    archivo = await RegenerarJsonNotasDeCredito(documento.idSigescom, sede);
                }
                else if (documento.codigoTipoComprobante == MaestroSettings.Default.CodigoDetalleMaestroNotaDeDebito)
                {
                    archivo = await RegenerarJsonNotasDeDebito(documento.idSigescom, sede);
                }
                else if (documento.codigoTipoComprobante == MaestroSettings.Default.CodigoDetalleMaestroGuiaDeRemisionRemitente)
                {
                    archivo = await RegenerarJsonGuiaDeRemision(documento.idSigescom, sede);
                }
                Binario binario = new Binario()
                {
                    id = documento.idBinarioDocumento,
                    archivoBinario = archivo
                };
                OperationResult result = ActualizarBinario(binario);
                Util.ManejoEnLogicaResultadoSinExito(result, "Error al regenerar el json del documento");
                return result;
            }

            catch (Exception e)
            {
                throw new LogicaException("Error al regenerar el json del documento", e);
            }
        }

        public byte[] RegenerarJsonBoleta(long idOrden, EstablecimientoComercialExtendido sede)
        {
            try
            {
                OrdenDeVenta ordenDeVenta = _operacionLogica.ObtenerOrdenDeVenta(idOrden);

                EstablecimientoComercialExtendido establecimiento = _establecimientoDatos.ObtenerEstablecimientoComercialExtendido((int)ordenDeVenta.Transaccion().Actor_negocio2.id_actor_negocio_padre);
                BoletaDeVenta boletaDeVenta = new BoletaDeVenta(ordenDeVenta, sede, establecimiento, null, false, (ModoImpresionCaracteristicasEnum)VentasSettings.Default.modoImpresionCaracteristicas);
                DocumentoElectronico documentoElectronico = _generacionArchivosLogica.ObtenerDocumentoElectronicoBoleta(boletaDeVenta);
                return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(documentoElectronico));
            }
            catch (Exception e)
            {
                throw new Exception("Error al intentar regenerar el json de boleta", e);
            }
        }

        public byte[] RegenerarJsonFactura(long idOrden, EstablecimientoComercialExtendido sede)
        {
            try
            {
                OrdenDeVenta ordenDeVenta = _operacionLogica.ObtenerOrdenDeVenta(idOrden);
                EstablecimientoComercialExtendido establecimiento = _establecimientoDatos.ObtenerEstablecimientoComercialExtendidoConLogo((int)ordenDeVenta.Transaccion().Actor_negocio2.id_actor_negocio_padre);
                Factura factura = new Factura(ordenDeVenta, sede, establecimiento, null, false, (ModoImpresionCaracteristicasEnum)VentasSettings.Default.modoImpresionCaracteristicas);
                DocumentoElectronico documentoElectronico = _generacionArchivosLogica.ObtenerDocumentoElectronicoFactura(factura);
                return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(documentoElectronico));
            }
            catch (Exception e)
            {
                throw new Exception("Error al intentar regenerar el json de factura", e);
            }
        }

        public async Task<byte[]> RegenerarJsonNotasDeCredito(long idOrden, EstablecimientoComercialExtendido sede)
        {
            try
            {
                OperacionDeVenta operacionDeVenta = _operacionLogica.ObtenerOrdenDeVenta(idOrden);
                EstablecimientoComercialExtendidoConLogo establecimiento = _establecimientoDatos.ObtenerEstablecimientoComercialExtendidoConLogo((int)operacionDeVenta.Transaccion().Actor_negocio2.id_actor_negocio_padre);

                var tiposNotasDeCredito = await _maestroLogica.ObtenerDetallesMaestrosAsync(MaestroSettings.Default.IdMaestroTipoDeNotaDeCreditoElectronica);
                NotaDeCredito notaDeCredito = new NotaDeCredito(operacionDeVenta, sede, establecimiento, tiposNotasDeCredito, null, false, (ModoImpresionCaracteristicasEnum)VentasSettings.Default.modoImpresionCaracteristicas);
                DocumentoElectronico documentoElectronico = _generacionArchivosLogica.ObtenerDocumentoElectronicoNotaCredito(notaDeCredito);
                return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(documentoElectronico));
            }
            catch (Exception e)
            {
                throw new Exception("Error al intentar regenerar el json de nota de credito", e);
            }
        }

        public async Task<byte[]> RegenerarJsonNotasDeDebito(long idOrden, EstablecimientoComercialExtendido sede)
        {
            try
            {
                OperacionDeVenta operacionDeVenta = _operacionLogica.ObtenerOrdenDeVenta(idOrden);
                EstablecimientoComercialExtendido establecimiento = _establecimientoDatos.ObtenerEstablecimientoComercialExtendido((int)operacionDeVenta.Transaccion().Actor_negocio2.id_actor_negocio_padre);
                var tiposNotasDeDebito = await _maestroLogica.ObtenerDetallesMaestrosAsync(MaestroSettings.Default.IdMaestroTipoDeNotaDeDebitoElectronica);
                NotaDeDebito notaDeDebito = new NotaDeDebito(operacionDeVenta, sede, establecimiento, tiposNotasDeDebito, null, false, (ModoImpresionCaracteristicasEnum)VentasSettings.Default.modoImpresionCaracteristicas);
                DocumentoElectronico documentoElectronico = _generacionArchivosLogica.ObtenerDocumentoElectronicoNotaDebito(notaDeDebito);
                return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(documentoElectronico));
            }
            catch (Exception e)
            {
                throw new Exception("Error al intentar regenerar el json de nota de debito", e);
            }
        }

        public async Task<byte[]> RegenerarJsonGuiaDeRemision(long idMovimiento, EstablecimientoComercialExtendido sede)
        {
            try
            {
                MovimientoDeAlmacen movimientoDeAlmacen = _operacionLogica.ObtenerMovimientoDeMercaderia(idMovimiento);
                EstablecimientoComercialExtendido establecimiento = _establecimientoDatos.ObtenerEstablecimientoComercialExtendido((int)movimientoDeAlmacen.Transaccion().Actor_negocio2.id_actor_negocio_padre);
                var proveedores = _actorNegocioLogica.ObtenerProveedoresVigentes();
                var modalidadesDeTransporte = await _maestroLogica.ObtenerDetallesMaestrosAsync(MaestroSettings.Default.IdMaestroModalidadDeTraslado);
                var motivosDeTransporte = await _maestroLogica.ObtenerDetallesMaestrosAsync(MaestroSettings.Default.IdMaestroMotivoDeTraslado);
                GuiaDeRemision guiaDeRemision = new GuiaDeRemision(movimientoDeAlmacen, sede, establecimiento, null, false, (ModoImpresionCaracteristicasEnum)VentasSettings.Default.modoImpresionCaracteristicas, proveedores, modalidadesDeTransporte, motivosDeTransporte);
                GuiaRemision guiaRemision = _generacionArchivosLogica.ObtenerDocumentoElectronicoGuiaDeRemision(guiaDeRemision);
                return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(guiaRemision));
            }
            catch (Exception e)
            {
                throw new Exception("Error al intentar regenerar el json de guia de remision", e);
            }
        }

        #endregion
    }
}

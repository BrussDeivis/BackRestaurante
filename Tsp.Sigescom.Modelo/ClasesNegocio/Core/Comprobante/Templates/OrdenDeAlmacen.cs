using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.Entidades.ComprobantesModel
{
    public class OrdenDeAlmacen : DocumentoElectronicoImpreso
    {
        public override string NombreTipo { get; set; } = "ORDEN DE ALMACÉN";

        public OrdenDeAlmacen()
        {

        }

        public OrdenDeAlmacen(OrdenDeMovimientoDeAlmacen ordenDeMovimiento, EstablecimientoComercialExtendido sede, EstablecimientoComercialExtendido establecimiento, byte[] qrBytes, bool mostrarEncabezadoTestigo, ModoImpresionCaracteristicasEnum modoImpresionCaracteristicas)
        {
            IdOrden = ordenDeMovimiento.Id;
            FechaEmision = ordenDeMovimiento.FechaEmision;
            Emisor = new Emisor(sede, establecimiento);
            Receptor = new Receptor(new EstablecimientoComercialExtendido(ordenDeMovimiento.Transaccion().Actor_negocio1));
            Detalles = Detalle.Convert(ordenDeMovimiento.Detalles(), modoImpresionCaracteristicas, "", false);
            MostrarMensajeAmazonia = false;
            MensajeNegocio = AplicacionSettings.Default.MensajeDeNegocio;
            MostrarMensajeNegocio = AplicacionSettings.Default.MostrarMensajeDeNegocio;
            Observacion = ordenDeMovimiento.Observacion();
            MostrarLogo = FacturacionElectronicaSettings.Default.MostrarLogoEnComprobanteImpreso;
            CodigoQR = qrBytes;
            CodigoSunatMoneda = ordenDeMovimiento.Moneda().Codigo;
            CodigoSunatTipo = ordenDeMovimiento.Comprobante().CodigoTipo;
            Serie = ordenDeMovimiento.Comprobante().NumeroDeSerie;
            Numero = ordenDeMovimiento.Comprobante().NumeroDeComprobante;
            MostrarTestigo = mostrarEncabezadoTestigo;
            ResolucionAutorizacionSunat = FacturacionElectronicaSettings.Default.ResolucionEmisionElectronica;
            IdEstadoActual = MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado;

        }
        public static List<OrdenDeAlmacen> Convert(List<OrdenDeMovimientoDeAlmacen> ordenes, EstablecimientoComercialExtendido sede, ModoImpresionCaracteristicasEnum modoImpresionCaracteristicas)
        {
            List<OrdenDeAlmacen> resultado = new List<OrdenDeAlmacen>();
            foreach (var item in ordenes)
            {
                resultado.Add(new OrdenDeAlmacen(item, sede, new EstablecimientoComercialExtendidoConLogo(item.Transaccion().Actor_negocio2.Actor_negocio2), null, false, modoImpresionCaracteristicas));
            }
            return resultado;
        }
    }
}

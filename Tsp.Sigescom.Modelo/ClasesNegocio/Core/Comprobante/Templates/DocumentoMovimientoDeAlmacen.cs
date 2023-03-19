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
    public class DocumentoMovimientoDeAlmacen : DocumentoElectronicoVenta
    {
        public override string NombreTipo { get; set; }
        public DocumentoMovimientoDeAlmacen()
        {

        }

        public DocumentoMovimientoDeAlmacen(MovimientoDeAlmacen movimiento, EstablecimientoComercialExtendidoConLogo sede, EstablecimientoComercialExtendido establecimiento, byte[] qrBytes, bool mostrarEncabezadoTestigo, ModoImpresionCaracteristicasEnum modoImpresionCaracteristicas)
        {
            FechaEmision = movimiento.FechaEmision;
            Emisor = new Emisor(sede, establecimiento);
            Receptor = new Receptor(new EstablecimientoComercialExtendido( movimiento.Transaccion().Actor_negocio1));
            Detalles = Detalle.Convert(movimiento.Detalles(), modoImpresionCaracteristicas, "", false);
            MostrarMensajeAmazonia = false;
            MensajeNegocio = AplicacionSettings.Default.MensajeDeNegocio;
            Observacion = movimiento.Observacion();
            MostrarLogo = FacturacionElectronicaSettings.Default.MostrarLogoEnComprobanteImpreso;
            CodigoQR = qrBytes;
            Serie = movimiento.Comprobante().NumeroDeSerie;
            Numero = movimiento.Comprobante().NumeroDeComprobante;
            ImporteTotal = movimiento.Total;
            ImporteTotalEnLetras = Util.APalabras(movimiento.Total, movimiento.MonedaPlural());
            Descuento = movimiento.Descuento();
            Igv = movimiento.Igv();
            MostrarTestigo = mostrarEncabezadoTestigo;
            ResolucionAutorizacionSunat = FacturacionElectronicaSettings.Default.ResolucionEmisionElectronica;
            NombreTipo = movimiento.Comprobante().NombreTipo;
        }

    }
}

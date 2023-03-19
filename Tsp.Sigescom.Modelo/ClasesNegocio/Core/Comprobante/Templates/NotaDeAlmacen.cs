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
    public class NotaDeAlmacen : DocumentoElectronicoVenta
    {
        public override string NombreTipo { get; set; } = "NOTA DE ALMACÉN";
        public Receptor ResponsableReceptor { get; set; }
        public Receptor ResponsableOrigen { get; set; }
        public Receptor Origen { get; set; }
        public bool EsTrasladoInterno { get; set; }
        public NotaDeAlmacen()
        {

        }

        public NotaDeAlmacen(MovimientoDeAlmacen movimiento, EstablecimientoComercialExtendidoConLogo sede, EstablecimientoComercialExtendido establecimiento, byte[] qrBytes, bool mostrarEncabezadoTestigo, ModoImpresionCaracteristicasEnum modoImpresionCaracteristicas)
        {
            FechaEmision = movimiento.FechaEmision;
            Emisor = new Emisor(sede, establecimiento);
            EsTrasladoInterno = movimiento.IdTipoTransaccion == TransaccionSettings.Default.IdTipoTransaccionIngresoMercaderíaDesplazamientoInterno || movimiento.IdTipoTransaccion == TransaccionSettings.Default.IdTipoTransaccionSalidaMercaderíaDesplazamientoInterno;
            if(EsTrasladoInterno)
            {
                var ingresoMercaderia = movimiento.IdTipoTransaccion == TransaccionSettings.Default.IdTipoTransaccionIngresoMercaderíaDesplazamientoInterno;
                var idTipoTransaccionComplementaria = ingresoMercaderia ? TransaccionSettings.Default.IdTipoTransaccionSalidaMercaderíaDesplazamientoInterno : TransaccionSettings.Default.IdTipoTransaccionIngresoMercaderíaDesplazamientoInterno;
                var empleadoResponsable = movimiento.Transaccion().Transaccion3.Transaccion11.Single(t => t.id_tipo_transaccion == idTipoTransaccionComplementaria && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado).Actor_negocio;
                Receptor = new Receptor(new EstablecimientoComercialExtendido(ingresoMercaderia ? movimiento.Transaccion().Actor_negocio2 : movimiento.Transaccion().Actor_negocio1));
                ResponsableReceptor = new Receptor(new EstablecimientoComercialExtendido(ingresoMercaderia ?  movimiento.Transaccion().Actor_negocio : empleadoResponsable));
                Origen = new Receptor(new EstablecimientoComercialExtendido(ingresoMercaderia ? movimiento.Transaccion().Actor_negocio1 : movimiento.Transaccion().Actor_negocio2));
                ResponsableOrigen = new Receptor(new EstablecimientoComercialExtendido(ingresoMercaderia ? empleadoResponsable : movimiento.Transaccion().Actor_negocio));
            }
            else
            {
                Receptor = new Receptor(new EstablecimientoComercialExtendido(movimiento.Transaccion().Actor_negocio1));
            }
            Detalles = Detalle.Convert(movimiento.Detalles(), modoImpresionCaracteristicas, "", false);/* operacion.DetalleUnificado() == "" ? Detalle.Convert(operacion.Detalles(), modoImpresionCaracteristicas) : Detalle.Convert(operacion.DetalleContemplandoUnificacionDeConceptos(), modoImpresionCaracteristicas);*/
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
            EsInvalidada = movimiento.EstaInvalidada();
            MotivoInvalidacion = movimiento.MotivoInvalidacion();

        }

    }
}

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
    public class Factura : DocumentoElectronicoVenta
    {
        public override string NombreTipo { get; set; } = "FACTURA ELECTRÓNICA";
        //public string PlacaDelVehiculo { get; set; }
        public bool TieneGuiaDeRemision { get; set; }
        public List<Relacionado> GuiasDeRemision { get; set; }
        //public string TipoDeDocumentoGuiaDeRemision { get; set; }
        //public string SerieDelDocumentoGuiaDeRemision { get; set; }
        //public int NumeroDelDocumentoGuiaDeRemision { get; set; }
        public bool MostrarInformacionAdicional80 { get; set; }
        public string InformacionAdicional80 { get; set; }
        public bool MostrarInformacionAdicionalA4 { get; set; }
        public string InformacionAdicionalA4 { get; set; }

        public Factura()
        { }
        public Factura(OrdenDeVenta orden, EstablecimientoComercialExtendido sede, EstablecimientoComercialExtendido establecimiento, byte[] qrBytes, bool mostrarEncabezadoTestigo, ModoImpresionCaracteristicasEnum modoImpresionCaracteristicas) : base(orden, sede, establecimiento, qrBytes, mostrarEncabezadoTestigo, modoImpresionCaracteristicas)
        {
            TieneGuiaDeRemision = orden.TieneGuiaDeRemision();
            //TipoDeDocumentoGuiaDeRemision = orden.TieneGuiaDeRemision() ? orden.GuiaRemision().NombreTipo : "";
            //SerieDelDocumentoGuiaDeRemision = orden.TieneGuiaDeRemision() ? orden.GuiaRemision().NumeroDeSerie : "";
            //NumeroDelDocumentoGuiaDeRemision = orden.TieneGuiaDeRemision() ? orden.GuiaRemision().NumeroDeComprobante : 0;
            MostrarInformacionAdicional80 = !String.IsNullOrEmpty(VentasSettings.Default.InformacionAdicionalFactura80);
            InformacionAdicional80 = VentasSettings.Default.InformacionAdicionalFactura80;
            MostrarInformacionAdicionalA4 = !String.IsNullOrEmpty(VentasSettings.Default.InformacionAdicionalFacturaA4);
            InformacionAdicionalA4 = VentasSettings.Default.InformacionAdicionalFacturaA4;
            FormaPago = orden.ModoDePago() == ModoPago.Contado ? Enumerado.GetDescription(FormaPagoEnum.Contado) : Enumerado.GetDescription(FormaPagoEnum.Credito);
            MontoACredito = orden.ModoDePago() == ModoPago.Contado ? 0 : orden.Cuotas().Sum(c => c.total) - orden.PagoEnFechaEmision();
            Cuotas = orden.ModoDePago() == ModoPago.Contado ? new List<DetalleCuota>() : DetalleCuota.Convert(orden.Cuotas(), orden.ModoDePago(), orden.PagoEnFechaEmision());
        }
        public Factura(OrdenDeVenta orden, EstablecimientoComercialExtendidoConLogo sede, EstablecimientoComercialExtendido establecimiento, byte[] qrBytes, bool mostrarEncabezadoTestigo, ModoImpresionCaracteristicasEnum modoImpresionCaracteristicas) : base(orden, sede, establecimiento, qrBytes, mostrarEncabezadoTestigo, modoImpresionCaracteristicas)
        {
            TieneGuiaDeRemision = orden.TieneGuiaDeRemision();
            //TipoDeDocumentoGuiaDeRemision = orden.TieneGuiaDeRemision() ? orden.GuiaRemision().NombreTipo : "";
            //SerieDelDocumentoGuiaDeRemision = orden.TieneGuiaDeRemision() ? orden.GuiaRemision().NumeroDeSerie : "";
            //NumeroDelDocumentoGuiaDeRemision = orden.TieneGuiaDeRemision() ? orden.GuiaRemision().NumeroDeComprobante : 0;
            MostrarInformacionAdicional80 = !String.IsNullOrEmpty(VentasSettings.Default.InformacionAdicionalFactura80);
            InformacionAdicional80 = VentasSettings.Default.InformacionAdicionalFactura80;
            MostrarInformacionAdicionalA4 = !String.IsNullOrEmpty(VentasSettings.Default.InformacionAdicionalFacturaA4);
            InformacionAdicionalA4 = VentasSettings.Default.InformacionAdicionalFacturaA4;
            FormaPago = orden.ModoDePago() == ModoPago.Contado ? Enumerado.GetDescription(FormaPagoEnum.Contado) : Enumerado.GetDescription(FormaPagoEnum.Credito);
            MontoACredito = orden.ModoDePago() == ModoPago.Contado ? 0 : orden.Cuotas().Sum(c => c.total) - orden.PagoEnFechaEmision();
            Cuotas = orden.ModoDePago() == ModoPago.Contado ? new List<DetalleCuota>() : DetalleCuota.Convert(orden.Cuotas(), orden.ModoDePago(), orden.PagoEnFechaEmision());
        }

        public static List<Factura> Convert(List<OrdenDeVenta> ordenes, EstablecimientoComercialExtendido sede, ModoImpresionCaracteristicasEnum modoImpresionCaracteristicas)
        {
            List<Factura> resultado = new List<Factura>();
            foreach (var orden in ordenes)
            {
                resultado.Add(new Factura(orden, sede,new EstablecimientoComercialExtendidoConLogo(orden.Transaccion().Actor_negocio2.Actor_negocio2), null, false, modoImpresionCaracteristicas));
            }
            return resultado;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Humanizer;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;

namespace Tsp.Sigescom.Modelo.Entidades.ComprobantesModel
{
    public class BoletaDeVenta: DocumentoElectronicoVenta
    {
        public override string NombreTipo { get; set; } = "BOLETA DE VENTA ELECTRÓNICA";
        public bool MostrarInformacionAdicional80 { get; set; }
        public string InformacionAdicional80 { get; set; }
        public bool MostrarInformacionAdicionalA4 { get; set; }
        public string InformacionAdicionalA4 { get; set; }
        public BoletaDeVenta()
        {

        }

        public BoletaDeVenta(OrdenDeVenta orden, EstablecimientoComercialExtendido sede, EstablecimientoComercialExtendido establecimiento, byte[] qrBytes, bool mostrarEncabezadoTestigo, ModoImpresionCaracteristicasEnum modoImpresionCaracteristicas) : base(orden, sede, establecimiento, qrBytes, mostrarEncabezadoTestigo, modoImpresionCaracteristicas)
        {
            MostrarInformacionAdicional80 = !String.IsNullOrEmpty(VentasSettings.Default.InformacionAdicionalBoletaVenta80);
            InformacionAdicional80 = VentasSettings.Default.InformacionAdicionalBoletaVenta80;
            MostrarInformacionAdicionalA4 = !String.IsNullOrEmpty(VentasSettings.Default.InformacionAdicionalBoletaVentaA4);
            InformacionAdicionalA4 = VentasSettings.Default.InformacionAdicionalBoletaVentaA4;
            FormaPago = orden.ModoDePago() == ModoPago.Contado ? Enumerado.GetDescription(FormaPagoEnum.Contado) : Enumerado.GetDescription(FormaPagoEnum.Credito);
            MontoACredito = orden.ModoDePago() == ModoPago.Contado ? 0 : orden.Cuotas().Sum(c => c.total) - orden.PagoEnFechaEmision();
            Cuotas = orden.ModoDePago() == ModoPago.Contado ? new List<DetalleCuota>() : DetalleCuota.Convert(orden.Cuotas(), orden.ModoDePago(), orden.PagoEnFechaEmision());
        }

        public BoletaDeVenta(OrdenDeVenta orden, EstablecimientoComercialExtendidoConLogo sede, EstablecimientoComercialExtendido establecimiento, byte[] qrBytes, bool mostrarEncabezadoTestigo, ModoImpresionCaracteristicasEnum modoImpresionCaracteristicas) : base(orden, sede, establecimiento, qrBytes, mostrarEncabezadoTestigo, modoImpresionCaracteristicas)
        {
            MostrarInformacionAdicional80 = !String.IsNullOrEmpty(VentasSettings.Default.InformacionAdicionalBoletaVenta80);
            InformacionAdicional80 = VentasSettings.Default.InformacionAdicionalBoletaVenta80;
            MostrarInformacionAdicionalA4 = !String.IsNullOrEmpty(VentasSettings.Default.InformacionAdicionalBoletaVentaA4);
            InformacionAdicionalA4 = VentasSettings.Default.InformacionAdicionalBoletaVentaA4;
            FormaPago = orden.ModoDePago() == ModoPago.Contado ? Enumerado.GetDescription(FormaPagoEnum.Contado) : Enumerado.GetDescription(FormaPagoEnum.Credito);
            MontoACredito = orden.ModoDePago() == ModoPago.Contado ? 0 : orden.Cuotas().Sum(c => c.total) - orden.PagoEnFechaEmision();
            Cuotas = orden.ModoDePago() == ModoPago.Contado ? new List<DetalleCuota>() : DetalleCuota.Convert(orden.Cuotas(), orden.ModoDePago(), orden.PagoEnFechaEmision());
        }

        public static List<BoletaDeVenta> Convert(List<OrdenDeVenta> ordenes, EstablecimientoComercialExtendido sede, ModoImpresionCaracteristicasEnum modoImpresionCaracteristicas)
        {
            List<BoletaDeVenta> resultado = new List<BoletaDeVenta>();
            foreach (var item in ordenes)
            {
                resultado.Add(new BoletaDeVenta(item, sede, new EstablecimientoComercialExtendidoConLogo(item.Transaccion().Actor_negocio2.Actor_negocio2), null, false, modoImpresionCaracteristicas));
            }
            return resultado;
        }
    }
}


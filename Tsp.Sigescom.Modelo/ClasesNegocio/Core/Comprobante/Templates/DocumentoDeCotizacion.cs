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
    public class DocumentoDeCotizacion : DocumentoElectronicoVenta
    {
        public override string NombreTipo { get; set; } = "COTIZACIÓN";
        public DateTime FechaVencimiento { get; set; }
        public bool MostrarIgv { get; set; }
        public bool MostrarInformacionAdicional80 { get; set; }
        public string InformacionAdicional80 { get; set; }
        public bool MostrarInformacionAdicionalA4 { get; set; }
        public string InformacionAdicionalA4 { get; set; }

        public DocumentoDeCotizacion(OrdenDeCotizacion orden, EstablecimientoComercialExtendido sede, EstablecimientoComercialExtendido establecimiento, byte[] qrBytes, bool mostrarEncabezadoTestigo, ModoImpresionCaracteristicasEnum modoImpresionCaracteristicas) : base(orden, sede, establecimiento, qrBytes, mostrarEncabezadoTestigo, modoImpresionCaracteristicas)
        {
            FechaVencimiento = orden.FechaVencimiento;
            MostrarIgv = orden.Igv() > 0;
            MostrarInformacionAdicional80 = !String.IsNullOrEmpty(AplicacionSettings.Default.InformacionAdicionalCotizacion80);
            InformacionAdicional80 = AplicacionSettings.Default.InformacionAdicionalCotizacion80;
            MostrarInformacionAdicionalA4 = !String.IsNullOrEmpty(AplicacionSettings.Default.InformacionAdicionalCotizacionA4);
            InformacionAdicionalA4 = AplicacionSettings.Default.InformacionAdicionalCotizacionA4;
        }

        public DocumentoDeCotizacion(OrdenDeCotizacion orden, EstablecimientoComercialExtendidoConLogo sede, EstablecimientoComercialExtendido establecimiento, byte[] qrBytes, bool mostrarEncabezadoTestigo, ModoImpresionCaracteristicasEnum modoImpresionCaracteristicas) : base(orden, sede, establecimiento, qrBytes, mostrarEncabezadoTestigo, modoImpresionCaracteristicas)
        {
            FechaVencimiento = orden.FechaVencimiento;
            MostrarIgv = orden.Igv() > 0;
            MostrarInformacionAdicional80 = !String.IsNullOrEmpty(AplicacionSettings.Default.InformacionAdicionalCotizacion80);
            InformacionAdicional80 = AplicacionSettings.Default.InformacionAdicionalCotizacion80;
            MostrarInformacionAdicionalA4 = !String.IsNullOrEmpty(AplicacionSettings.Default.InformacionAdicionalCotizacionA4);
            InformacionAdicionalA4 = AplicacionSettings.Default.InformacionAdicionalCotizacionA4;
        }
    }
}

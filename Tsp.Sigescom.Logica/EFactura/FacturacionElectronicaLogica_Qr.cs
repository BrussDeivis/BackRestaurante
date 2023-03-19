using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.FacturacionElectronica.Logica
{
    public partial class FacturacionElectronicaLogica
    {
        public string ObtenerQR(OrdenDeVenta orden, EstablecimientoComercial sede)
        {
            return (sede.DocumentoIdentidad+"|" + orden.Comprobante().NombreTipo+ "|" + orden.Comprobante().NumeroDeSerie+"|"+orden.Comprobante().NumeroDeComprobante+"|"+
               orden.Igv() + "|"+orden.Total+"|"+orden.FechaEmision+ "|"+orden.CodigoSunatTipoDocumentoIdentidadCliente+"|"+ orden.NumeroDocumentoIdentidadCliente+"|"+ "");
        }

        public string ObtenerQR(MovimientoDeAlmacen movimiento, EstablecimientoComercial sede)
        {
            return (sede.DocumentoIdentidad + "|" + movimiento.Comprobante().NombreTipo + "|" + movimiento.Comprobante().NumeroDeSerie + "|" + movimiento.Comprobante().NumeroDeComprobante + "|" + movimiento.FechaEmision + "|" + movimiento.Tercero().CodigoSunatTipoDocumentoIdentidad() + "|" + movimiento.Tercero().DocumentoIdentidad + "|" + "");
        }
    }
}

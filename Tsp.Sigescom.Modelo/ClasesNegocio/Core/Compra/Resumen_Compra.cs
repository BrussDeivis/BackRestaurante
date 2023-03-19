using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.Partial
{
    public class Resumen_Compra
    { 
        public long Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int IdTipoComprobante { get; set; }
        public string TipoComprobante { get; set; }
        public string CodigoComprobante { get; set; }
        public string SerieComprobante { get; set; }
        public int NumeroComprobante { get; set; }
        public int IdProveedor { get; set; }
        public string DocumentoProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public decimal Importe { get; set; }
        public decimal ImporteTotal { get => Importe * Signo; }
        public string ValorParametroTipoOperacionDeCompra { get; set; }
        public string ValorParametroModoDePago { get; set; }
        public string Estado { get; set; }
        public string TipoDeCompra { get => ValorParametroTipoOperacionDeCompra != null ? Enumerado.GetDescription((TipoOperacionCompra)System.Convert.ToInt32(ValorParametroTipoOperacionDeCompra)) : Enumerado.GetDescription(TipoOperacionCompra.Ninguno); }
        public string ModoDePago { get => ValorParametroModoDePago != null ? Enumerado.GetDescription((ModoPago) System.Convert.ToInt32(ValorParametroModoDePago)) : Enumerado.GetDescription(ModoPago.Contado); }
        public string FechaEmision { get => FechaInicio.ToString("dd/MM/yyyy"); }
        public string Comprobante { get => SerieComprobante + "-" + NumeroComprobante; }
        public string Proveedor { get => (IdProveedor == ActorSettings.Default.idProveedorGenerico ? "-" : DocumentoProveedor) + " | " + NombreProveedor.Replace("|", " "); }
        public int Signo { get => IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito ? -1 : 1; }

        public Resumen_Compra()
        {

        }

        public static List<Resumen_Compra> Convert()
        {
            return new List<Resumen_Compra>();
        }
    }

    public class ResumenDeTipoCompra
    {
        public string NombreTipo { get; set; }
        public decimal Importe { get; set; }
        public ResumenDeTipoCompra()
        {

        }

        public ResumenDeTipoCompra(string nombreTipo, decimal importe)
        {
            NombreTipo = nombreTipo;
            Importe = importe;
        }

        public static List<ResumenDeTipoCompra> Convertir(List<Resumen_Compra> noGravadas, List<Resumen_Compra> gravadasDestinadasAVentasGravadas, List<Resumen_Compra> gravadasDestinadasAVentasNoGravadas, List<Resumen_Compra> gravadasDestinadasAVentasGravadasYNoGravadas)
        {
            List<ResumenDeTipoCompra> resumenes = new List<ResumenDeTipoCompra>();
            if(noGravadas.Count > 0) resumenes.Add(new ResumenDeTipoCompra(noGravadas.FirstOrDefault().TipoDeCompra, noGravadas.Sum(r => r.ImporteTotal)));
            if(gravadasDestinadasAVentasGravadas.Count > 0) resumenes.Add(new ResumenDeTipoCompra(gravadasDestinadasAVentasGravadas.FirstOrDefault().TipoDeCompra, gravadasDestinadasAVentasGravadas.Sum(r => r.ImporteTotal)));
            if(gravadasDestinadasAVentasNoGravadas.Count > 0) resumenes.Add(new ResumenDeTipoCompra(gravadasDestinadasAVentasNoGravadas.FirstOrDefault().TipoDeCompra, gravadasDestinadasAVentasNoGravadas.Sum(r => r.ImporteTotal)));
            if(gravadasDestinadasAVentasGravadasYNoGravadas.Count > 0) resumenes.Add(new ResumenDeTipoCompra(gravadasDestinadasAVentasGravadasYNoGravadas.FirstOrDefault().TipoDeCompra, gravadasDestinadasAVentasGravadasYNoGravadas.Sum(r => r.ImporteTotal)));
            return resumenes;
        }

        public static List<ResumenDeTipoCompra> Convert()
        {
            return new List<ResumenDeTipoCompra>();
        }
    }
}
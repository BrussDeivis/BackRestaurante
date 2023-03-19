using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.Partial
{
    public class Reporte_Puntos_Canjeados
    {
        public int IdCaja { get; set; }
        public string NombreCaja { get; set; }
        public DateTime FechaPago { get; set; }
        public string CodigoTipoDocumentoCliente { get; set; }
        public string NumeroDocumentoCliente { get; set; }
        public string Cliente { get; set; }
        public string CodigoTipoComprobante { get; set; }
        public string TipoComprobante { get; set; }
        public string SerieComprobante { get; set; }
        public int NumeroComprobante { get; set; }
        public decimal Puntos { get; set; }
        public decimal Monto { get; set; }
        public bool EsIngreso { get; set; }

        public string Comprobante { get => SerieComprobante + "-" + NumeroComprobante; }

        public int PuntosRealizados { get => EsIngreso ? (int)Puntos : 0; }
        public int PuntosAnulados { get => EsIngreso ? 0 : (int)Puntos; }
        public decimal MontoRealizado { get => EsIngreso ? Monto : 0; }
        public decimal MontoAnulado { get => EsIngreso ? 0 : Monto; }
        public Reporte_Puntos_Canjeados()
        {

        }

        public static List<Reporte_Puntos_Canjeados> Convert()
        {
            return new List<Reporte_Puntos_Canjeados>();
        }
    }

    public class Resumen_Puntos_Canjeados
    {
        public int IdCaja { get; set; }
        public string NombreCaja { get; set; }
        public int PuntosRealizados { get; set; }
        public int PuntosAnulados { get; set; }
        public decimal MontoRealizado { get; set; }
        public decimal MontoAnulado { get; set; }
        public int PuntosCanjeados => PuntosRealizados - PuntosAnulados;
        public decimal MontoCanjeado { get => MontoRealizado - MontoAnulado; }

        public Resumen_Puntos_Canjeados()
        {

        }

        public static List<Resumen_Puntos_Canjeados> Convert()
        {
            return new List<Resumen_Puntos_Canjeados>();
        }

        public static List<Resumen_Puntos_Canjeados> Convert(List<Reporte_Puntos_Canjeados> registrosDePuntosCanjeados)
        {
            var resumen = registrosDePuntosCanjeados.GroupBy(rpc => new
            {
                idEntidad = rpc.IdCaja,
                nombreEntidad = rpc.NombreCaja,
            }).Select(r => new Resumen_Puntos_Canjeados()
            {
                IdCaja = r.Key.idEntidad,
                NombreCaja = r.Key.nombreEntidad,
                PuntosRealizados = r.Sum(d => d.PuntosRealizados),
                PuntosAnulados = r.Sum(d => d.PuntosAnulados),
                MontoRealizado = r.Sum(d => d.MontoRealizado),
                MontoAnulado = r.Sum(d => d.MontoAnulado),
            }).ToList();
            return resumen;
        }
    }
}

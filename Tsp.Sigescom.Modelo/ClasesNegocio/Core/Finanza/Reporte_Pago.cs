using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.Partial
{
    public class Reporte_Pago
    {
        /// <summary>
        /// Nombre del actor negocio (cliente o proveedor)
        /// </summary>
        /// 
        private string primerNombre;
        private string numeroDocumento;
        private string codigoCuota;
        private DateTime fechaPago;
        private string tipoComprobante;
        private string codigoComprobante;
        private string numeroSerie;
        private int numeroComprobante;
        private IEnumerable<string> trazas;
        private decimal pagoACuenta;

        public string NumeroDocumento { get => numeroDocumento; set => numeroDocumento = value; }
        public string CodigoCuota { get => codigoCuota; set => codigoCuota = value; }
        public DateTime FechaPago { get => fechaPago; set => fechaPago = value; }
        public string TipoComprobante { get => tipoComprobante; set => tipoComprobante = value; }
        public string NumeroSerie { get => numeroSerie; set => numeroSerie = value; }
        public int NumeroComprobante { get => numeroComprobante; set => numeroComprobante = value; }
        public decimal PagoACuenta { get => pagoACuenta; set => pagoACuenta = value; }
        public string PrimerNombre { get => primerNombre.Replace("|", " "); set => primerNombre = value; }
        public string CodigoComprobante { get => codigoComprobante; set => codigoComprobante = value; }
        public IEnumerable<string> Trazas { get => trazas; set => trazas = value; }

        public string  NombresTrazas
        {
            get { return  Trazas != null ?  string.Join(", ", Trazas.ToArray()) : ""; }
        }



        public Reporte_Pago()
        {

        }

        public static List<Reporte_Pago> Resumen(List<Reporte_Pago> pagos)
        {
            List<Reporte_Pago> reportePagosResumen = new List<Reporte_Pago>();

            reportePagosResumen = pagos.GroupBy(d => new { d.NumeroDocumento })
                                         .Select(dc =>
                                               new Reporte_Pago()
                                               {
                                                   PrimerNombre = dc.First().PrimerNombre,
                                                   NumeroDocumento = dc.Key.NumeroDocumento,
                                                   PagoACuenta = dc.Sum(s => s.PagoACuenta),
                                               }).ToList();
            return reportePagosResumen;
        }
    }
}

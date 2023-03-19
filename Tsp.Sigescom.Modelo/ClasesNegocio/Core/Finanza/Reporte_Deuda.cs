using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.Partial
{
    public class Reporte_Deuda
    {
        /// <summary>
        /// Nombre del actor negocio (cliente o proveedor)
        /// </summary>
        /// 
        private string primerNombre;
        private string numeroDocumento;
        private string codigoCuota;
        private DateTime fechaVencimiento;
        private string tipoComprobante;
        private string codigoComprobante;
        private string numeroSerie;
        private int numeroComprobante;
        private decimal total;
        private decimal pagoACuenta;
        private decimal saldo;
        private decimal revocado;





        public string NumeroDocumento { get => numeroDocumento; set => numeroDocumento = value; }
        public string CodigoCuota { get => codigoCuota; set => codigoCuota = value; }
        public DateTime FechaVencimiento { get => fechaVencimiento; set => fechaVencimiento = value; }
        public string TipoComprobante { get => tipoComprobante; set => tipoComprobante = value; }
        public string NumeroSerie { get => numeroSerie; set => numeroSerie = value; }
        public int NumeroComprobante { get => numeroComprobante; set => numeroComprobante = value; }
        public decimal Total { get => total; set => total = value; }
        public decimal PagoACuenta { get => pagoACuenta; set => pagoACuenta = value; }
        public string PrimerNombre { get => primerNombre.Replace("|", " "); set => primerNombre = value; }
        public decimal Revocado { get => revocado; set => revocado = value; }
        public decimal Saldo { get => saldo; set => saldo = value; }
        public string CodigoComprobante { get => codigoComprobante; set => codigoComprobante = value; }

        public Reporte_Deuda()
        {

        }

        public static List<Reporte_Deuda> Resumen(List<Reporte_Deuda> deudas)
        {
            List<Reporte_Deuda> reporteDeudasResumen = new List<Reporte_Deuda>();

            reporteDeudasResumen = deudas.GroupBy(d => new { d.NumeroDocumento })
                                         .Select(dc =>
                                               new Reporte_Deuda()
                                               {
                                                   PrimerNombre = dc.First().PrimerNombre,
                                                   NumeroDocumento = dc.Key.NumeroDocumento,
                                                   Total = dc.Sum(s => s.Total),
                                                   PagoACuenta = dc.Sum(s => s.PagoACuenta),
                                                   Saldo = dc.Sum(s => s.Total) -  dc.Sum(s => s.PagoACuenta),
                                               }).ToList();

            return reporteDeudasResumen;
        }
    }
}

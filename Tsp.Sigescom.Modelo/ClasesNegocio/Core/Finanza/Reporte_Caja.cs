using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.Partial
{
    public class Movimiento_Caja
    {
        public DateTime Fecha { get; set; }
        public int IdCaja { get; set; }
        public string NombreCaja { get; set; }
        public int IdConcepto { get; set; }
        public string Concepto { get; set; }
        public string CodigoTipoComprobante { get; set; }
        public string SerieYNumeroComprobante { get; set; }
        public decimal Monto { get; set; }
        public bool EsIngreso { get; set; }
        public int Index { get; set; }
        public decimal Ingreso
        {
            get
            { return EsIngreso ? Monto : 0; }
        }
        public decimal Egreso
        {
            get
            { return EsIngreso ? 0 : Monto; }
        }
        public decimal Saldo { get; set; }

        public Movimiento_Caja()
        { }

        public static List<Movimiento_Caja> Convert()
        {
            return new List<Movimiento_Caja>();
        }

        public static List<Movimiento_Caja> Convert(List<Resumen_Movimiento_Caja> movimientosResumen, List<Movimiento_Caja> movimientosCaja, List<CentroDeAtencionExtendido> cajasVigentes)
        {
            List<Movimiento_Caja> movimientosCajaResultado = new List<Movimiento_Caja>();
            foreach (var movimiento in movimientosResumen)
            {
                var movimientosCajaSeleccionada = movimientosCaja.Where(m => m.IdCaja == movimiento.IdCaja).OrderBy(m => m.Fecha).ToList();
                decimal saldo = movimiento.SaldoInicial;
                int index = 0;
                foreach (var movimientoSeleccionado in movimientosCajaSeleccionada)
                {
                    saldo += (movimientoSeleccionado.EsIngreso ? movimientoSeleccionado.Monto : (-1 * movimientoSeleccionado.Monto));
                    var item = new Movimiento_Caja
                    {
                        Index = index++,
                        Fecha = movimientoSeleccionado.Fecha,
                        IdConcepto = movimientoSeleccionado.IdConcepto,
                        Concepto = movimientoSeleccionado.Concepto,
                        CodigoTipoComprobante = movimientoSeleccionado.CodigoTipoComprobante,
                        SerieYNumeroComprobante = movimientoSeleccionado.SerieYNumeroComprobante,
                        IdCaja = movimientoSeleccionado.IdCaja,
                        NombreCaja = movimientoSeleccionado.NombreCaja,
                        EsIngreso = movimientoSeleccionado.EsIngreso,
                        Monto = movimientoSeleccionado.Monto,
                        Saldo = saldo
                    };
                    movimientosCajaResultado.Add(item);
                }
                movimiento.NombreCaja = cajasVigentes.First(cv => cv.Id == movimiento.IdCaja).Nombre;
                movimiento.Ingreso = movimientosCajaSeleccionada.Sum(m => m.Ingreso);
                movimiento.Egreso = movimientosCajaSeleccionada.Sum(m => m.Egreso);
            }

           
            
            return movimientosCajaResultado;
        }
    }

    public class Resumen_Movimiento_Caja
    {
        public int IdCaja { get; set; }
        public string NombreCaja { get; set; }
        public decimal SaldoInicial { get; set; }
        public decimal Ingreso { get; set; }
        public decimal Egreso { get; set; }
        public decimal SaldoFinal
        {
            get
            { return SaldoInicial + Ingreso - Egreso; }
        }

        public static List<Resumen_Movimiento_Caja> Resumen(List<Movimiento_Caja> resultado)
        {
            return resultado.GroupBy(t => t.IdCaja
            ).Select(t => new Resumen_Movimiento_Caja()
            {
                IdCaja = t.Key,
                NombreCaja = t.First().NombreCaja,
                Ingreso = t.Sum(tt => tt.Ingreso),
                Egreso = t.Sum(tt => tt.Egreso),
            }).ToList();
        }

        public Resumen_Movimiento_Caja()
        { }

        public static List<Resumen_Movimiento_Caja> Convert()
        {
            return new List<Resumen_Movimiento_Caja>();
        }
    }
}

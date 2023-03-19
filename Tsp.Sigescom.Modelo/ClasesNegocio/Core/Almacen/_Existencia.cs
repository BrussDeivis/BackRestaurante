using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class _Existencia
    {
        private readonly Detalle_transaccion detalleDeTransaccion;

        public _Existencia()
        {
        }

        public _Existencia(Detalle_transaccion detalle_Transaccion)
        {
            this.detalleDeTransaccion = detalle_Transaccion;
        }

        public long Id
        {
            get { return this.detalleDeTransaccion.id; }
        }

        public long IdInventarioFisico
        {
            get { return this.detalleDeTransaccion.id_transaccion; }
        }

        public int IdConcepto
        {
            get { return this.detalleDeTransaccion.id_concepto_negocio; }
        }

        public string Lote
        {
            get { return this.detalleDeTransaccion.lote; }
        }

        public decimal Cantidad
        {
            get { return this.detalleDeTransaccion.cantidad; }
        }

        public static List<_Existencia> Convert_(List<Detalle_transaccion> detalles)
        {
            List<_Existencia> existencias = new List<_Existencia>();
            foreach (var detalle in detalles)
            {
                existencias.Add(new _Existencia(detalle));
            }
            return existencias;
        }

        public static List<Detalle_transaccion> Convert_(List<_Existencia> existencias)
        {
            List<Detalle_transaccion> detalles = new List<Detalle_transaccion>();
            foreach (var existencia in existencias)
            {
                detalles.Add(new Detalle_transaccion(existencia.Id, existencia.Cantidad, existencia.IdConcepto, "Inventario fisico", 0, 0, null, 0, null, null, 0, 0, 0, null, null, null));
            }
            return detalles;
        }
    }
}

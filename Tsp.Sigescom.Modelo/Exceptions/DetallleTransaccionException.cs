using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades
{
    public class DetalleTransaccionException : Exception
    {
        public Detalle_transaccion detalleTransaccion { get; set; }
        public DetalleTransaccionException(Exception dbConcurrencyException, Detalle_transaccion detalleTransaccion, Transaccion transaccion) : base("La cantidad del concepto de negocio de "+ detalleTransaccion.Concepto_negocio.nombre + " ha variado", dbConcurrencyException)
        {
            this.detalleTransaccion = detalleTransaccion;
        }
    }
}

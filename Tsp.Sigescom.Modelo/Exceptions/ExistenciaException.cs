using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades
{
    public class ExistenciaException : Exception
    {
        public Existencia existencia = null;
        public Transaccion transaccion = null;
        public ExistenciaException(Exception dbConcurrencyException, Existencia existencia, Transaccion transaccion) : base("El valor de las existencias para " + existencia.Concepto_negocio.nombre + " ha variado", dbConcurrencyException)
        {
            this.existencia = existencia;
            this.transaccion = transaccion;
        }

    }

}

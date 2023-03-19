using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;

namespace Tsp.Sigescom.Modelo.Entidades
{
    public partial class Pago_cuota
    {
        public Pago_cuota()
        {
        }
        public Pago_cuota(long idTransaccion, int idCuota, decimal importe)
        {
            this.id_transaccion = idTransaccion;
            SetData(idCuota, importe);
            ValidarId(idTransaccion, idCuota);
        }
        public Pago_cuota(int idCuota, decimal importe)
        {
            SetData(idCuota, importe);
            ValidarId(idCuota);
        }
        public void SetData(int idCuota, decimal importe)
        {
            this.id_cuota = idCuota;
            this.importe = importe;
        }
        protected void ValidarId(long idTransaccion, int idCuota)
        {
            if (idCuota < 1) { throw new IdNoValidoException(idCuota, "cuota"); }
            if (idTransaccion < 1) { throw new IdNoValidoException(idTransaccion, "transaccion"); }
        }
        protected void ValidarId(int idCuota)
        {
            if (idCuota < 1) { throw new IdNoValidoException(idCuota, "cuota"); }
        }
    }
}

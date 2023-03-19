using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;

namespace Tsp.Sigescom.Modelo.Entidades
{
    public partial class Evento_transaccion
    {
        public Evento_transaccion(long idTransaccion, int idEmpleado, int idEvento, DateTime fecha, string comentario)
        {
            setData(idTransaccion, idEmpleado, idEvento, fecha, comentario);
            validarId(idEmpleado, idEmpleado, idTransaccion);
        }
        public Evento_transaccion(int idEmpleado, int idEvento, DateTime fecha, string comentario)
        {
            setData(idEmpleado, idEvento, fecha, comentario);
            validarId(idEmpleado, idEvento);
        }
        public void setData(long idTransaccion, int idEmpleado, int idEvento, DateTime fecha, string comentario)
        {
            this.id_transaccion = idTransaccion;
            setData(idEmpleado, idEvento, fecha, comentario);
        }
        public void setData(int idEmpleado, int idEvento, DateTime fecha, string comentario)
        {
            this.id_empleado = idEmpleado;
            this.id_evento = idEvento;
            this.fecha = fecha;
            this.comentario = comentario;

        }
        protected void validarId(int idEmpleado, int idEvento, long idTransaccion)
        {
            if (idTransaccion < 1) { throw new IdNoValidoException(idTransaccion, "transaccion"); }
            validarId(idEmpleado, idEvento);
        }
        protected void validarId(int idEmpleado, int idEvento)
        {
            if (idEmpleado < 1) { throw new IdNoValidoException(idEmpleado, "empleado"); }
            if (idEvento < 1) { throw new IdNoValidoException(idEvento, "evento"); }
        }
    }
}

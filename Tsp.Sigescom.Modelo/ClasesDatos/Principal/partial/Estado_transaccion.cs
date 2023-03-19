using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;

namespace Tsp.Sigescom.Modelo.Entidades
{
    public partial class Estado_transaccion
    {
        public Estado_transaccion()
        {
        }
        public Estado_transaccion(long idTransaccion, int idEmpleado, int idEstado, DateTime fecha, string comentario)
        {
            setData(idTransaccion, idEmpleado, idEstado, fecha, comentario);
            validarId(idEmpleado, idEmpleado, idTransaccion);
        }
        public Estado_transaccion(int idEmpleado, int idEstado, DateTime fecha, string comentario)
        {
            setData(idEmpleado, idEstado, fecha, comentario);
            validarId(idEmpleado, idEstado);
        }
        public void setData(long idTransaccion, int idEmpleado, int idEstado, DateTime fecha, string comentario)
        {
            this.id_transaccion = idTransaccion;
            setData(idEmpleado, idEstado, fecha, comentario);
        }
        public void setData(int idEmpleado, int idEstado, DateTime fecha, string comentario)
        {
            this.id_empleado = idEmpleado;
            this.id_estado = idEstado;
            this.fecha = fecha;
            this.comentario = comentario;

        }
        protected void validarId(int idEmpleado, int idEstado, long idTransaccion)
        {
            if (idTransaccion < 1) { throw new IdNoValidoException(idTransaccion, "transaccion"); }
            validarId(idEmpleado, idEstado);
        }
        protected void validarId(int idEmpleado, int idEstado)
        {
            if (idEmpleado < 1) { throw new IdNoValidoException(idEmpleado, "empleado"); }
            if (idEstado < 1) { throw new IdNoValidoException(idEstado, "estado"); }
        }
    }
}

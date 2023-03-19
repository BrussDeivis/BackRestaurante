using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;

namespace Tsp.Sigescom.Modelo.Entidades
{
    public partial class Estado_cuota
    {
        public Estado_cuota()
        {
        }
        public Estado_cuota(int idCuota, int idEmpleado, int idEstado, DateTime fecha, string comentario)
        {
            setData(idCuota, idEmpleado, idEstado, fecha, comentario);
            validarId(idEmpleado, idEstado, idCuota);
        }
        public Estado_cuota(int idEmpleado, int idEstado, DateTime fecha, string comentario)
        {
            setData(idEmpleado, idEstado, fecha, comentario);
            validarId(idEmpleado, idEstado);
        }
        public void setData(int idCuota, int idEmpleado, int idEstado, DateTime fecha, string comentario)
        {
            this.id_cuota = idCuota;
            setData(idEmpleado, idEstado, fecha, comentario);
        }
        public void setData(int idEmpleado, int idEstado, DateTime fecha, string comentario)
        {
            this.id_empleado = idEmpleado;
            this.id_estado = idEstado;
            this.fecha = fecha;
            this.comentario = comentario;
        }
        protected void validarId(int idEmpleado, int idEstado, int idCuota)
        {
            validarId(idEmpleado, idEstado);
            if (idCuota < 1) { throw new IdNoValidoException(idCuota, "cuota"); }
        }
        protected void validarId(int idEmpleado, int idEstado)
        {
            if (idEmpleado < 1) { throw new IdNoValidoException(idEmpleado, "empleado"); }
            if (idEstado < 1) { throw new IdNoValidoException(idEstado, "estado"); }
        }
    }
}

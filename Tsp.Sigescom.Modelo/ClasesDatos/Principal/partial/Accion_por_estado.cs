using Tsp.Sigescom.Modelo.Entidades.Exceptions;

namespace Tsp.Sigescom.Modelo.Entidades
{
    partial class Accion_por_estado
    {
        public Accion_por_estado()
        {

        }
        public Accion_por_estado(int idTipoTransaccion, int idEstadoActual, int idAccionPosible)
        {
            setData(idTipoTransaccion, idEstadoActual, idAccionPosible);
            validarId(idEstadoActual, idAccionPosible, idTipoTransaccion);
        }

        public Accion_por_estado(int idEstadoAccion, int idTipoTransaccion, int idEstadoActual, int idAccionPosible)
        {
            setData(idEstadoAccion, idTipoTransaccion, idEstadoActual, idAccionPosible);
            validarId(idEstadoActual, idAccionPosible, idTipoTransaccion);
        }
        public void setData(int idTipoTransaccion, int idEstadoActual, int idAccionPosible)
        {
            this.id_tipo_transaccion = idTipoTransaccion;
            this.id_estado_actual = idEstadoActual;
            this.id_accion_posible = idAccionPosible;
        }

        public void setData(int idEstadoAccion, int idTipoTransaccion, int idEstadoActual, int idAccionPosible)
        {
            this.id= idEstadoAccion;
            setData(idTipoTransaccion, idEstadoActual, idAccionPosible);

        }
    protected void validarId(int idEstadoActual, int idAccionPosible, int idTipoTransaccion)
        {
            if (idEstadoActual < 1) { throw new IdNoValidoException(idEstadoActual, "Estado Actual"); }
            if (idAccionPosible < 1) { throw new IdNoValidoException(idAccionPosible, "Accion posible"); }
            if (idTipoTransaccion < 1) { throw new IdNoValidoException(idTipoTransaccion, "Tipo transaccion"); }

        }
        public string TipoTransaccion
        {
            get { return this.Tipo_transaccion.nombre; }
        }
        public string Accion
        {
            get { return this.Detalle_maestro1.nombre; }
        }
        public string Estado
        {
            get { return this.Detalle_maestro.nombre; }
        }
    }
}

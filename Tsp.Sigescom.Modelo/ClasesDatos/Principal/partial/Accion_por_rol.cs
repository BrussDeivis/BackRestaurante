using Tsp.Sigescom.Modelo.Entidades.Exceptions;

namespace Tsp.Sigescom.Modelo.Entidades
{
    partial class Accion_por_rol
    {
        public Accion_por_rol()
        {

        }
        public Accion_por_rol(int idTipoTransaccion, int idRol, int idAccionPosible, int idUnidadNegocio)
        {
            setData(idTipoTransaccion, idRol, idAccionPosible, idUnidadNegocio);
            validarId(idAccionPosible, idUnidadNegocio, idRol, idTipoTransaccion);
        }
        public Accion_por_rol(int idRolAccion,int idTipoTransaccion, int idRol, int idAccionPosible, int idUnidadNegocio)
        {
            setData(idRolAccion, idTipoTransaccion, idRol, idAccionPosible, idUnidadNegocio);
            validarId(idAccionPosible, idUnidadNegocio, idRol, idTipoTransaccion);
        }
        public void setData(int idRolAccion,int idTipoTransaccion, int idRol, int idAccionPosible, int idUnidadNegocio)
        {
            this.id = idRolAccion;
            setData(idTipoTransaccion, idRol, idAccionPosible, idUnidadNegocio);

        }
        public void setData(int idTipoTransaccion, int idRol, int idAccionPosible, int idUnidadNegocio)
        {
            this.id_tipo_transaccion = idTipoTransaccion;
            this.id_rol = idRol;
            this.id_accion_posible = idAccionPosible;
            this.id_unidad_negocio = idUnidadNegocio;
        }

        protected void validarId(int idAccionPosible, int idUnidadNegocio, int idRol, int idTipoTransaccion)
        {
            if (idAccionPosible < 1) { throw new IdNoValidoException(idAccionPosible, "Accion posible"); }
            if (idTipoTransaccion < 1) { throw new IdNoValidoException(idTipoTransaccion, "Tipo transaccion"); }
            if (idUnidadNegocio < 1) { throw new IdNoValidoException(idUnidadNegocio, "Unidad de Negocio"); }
            if (idRol < 1) { throw new IdNoValidoException(idRol, "Rol"); }

        }
        public string TipoTransaccion
            {
                  get{ return this.Tipo_transaccion.nombre; }
            }
        public string RolPersonal
        {
            get { return this.Rol.nombre; }
        }
        public string Accion
        {
            get { return this.Detalle_maestro.nombre; }
        }
        public string UnidadNegocio
        {
            get { return this.Detalle_maestro1.codigo; }
        }
    }
}

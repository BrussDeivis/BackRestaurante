using System.Collections.Generic;
using System.Linq;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class AccionOperativa
    {
        Detalle_maestro accion;

        public AccionOperativa(Detalle_maestro accion)
        {
            this.accion = accion;
        }

        public int IdAccion
        {
            get { return accion.id; }
        }

        public string CodigoAccion
        {
            get { return accion.codigo; }
        }

        public string NombreAccion
        {
            get { return accion.nombre; }
        }

        public List<Rol> rolesPermitidos(int idTipoTransaccion, int idUnidadNegocio)
        {
            return this.accion.Accion_por_rol.Where(apr => apr.id_tipo_transaccion == idTipoTransaccion && apr.id_unidad_negocio == idUnidadNegocio).Select(apr=>apr.Rol).ToList();
        }

        public List<Detalle_maestro> unidadesNegocioPermitidas(int idTipoTransaccion, int idRol)
        {
            return this.accion.Accion_por_rol.Where(apr => apr.id_tipo_transaccion == idTipoTransaccion && apr.id_rol== idRol).Select(apr => apr.Detalle_maestro1).ToList();
        }

        public List<Detalle_maestro> unidadesNegocioPermitidas(int idTipoTransaccion, Empleado empleado)
        {
            int[] idsRol = empleado.ObtenerRolesHijosVigentes().Select(r => r.id).ToArray();
            return this.accion.Accion_por_rol.Where(apr => apr.id_tipo_transaccion == idTipoTransaccion && idsRol.Contains(apr.id_rol)).Select(apr => apr.Detalle_maestro1).ToList();
        }

        public static List<AccionOperativa>  convert(List<Detalle_maestro> acciones)
        {
            List<AccionOperativa> accionesOperativas = new List<AccionOperativa>();
            try
            {
                foreach (var accion in acciones)
                {
                    accionesOperativas.Add(new AccionOperativa(accion));
                }
            }
            catch
            {
            
            }
            return accionesOperativas;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo.Interfaces.Logica
{
    public interface IAplicacionLogica
    {
        List<Tipo_transaccion> obtenerTiposDeTransaccion();

        List<Rol> obtenerRolesPersonal();

        List<Accion_por_estado> obtenerAccionesPosiblesPorTipoTransaccionYEstadoActual(int idTipoTransaccion, int idEstado);
        List<Accion_por_rol> obtenerAccionesPosiblesPorTipoTransaccionYRolPersonal(int idTipoTransaccion, int idRolPersonal);

        OperationResult actualizarPermisosPorRolYEstado(List<Accion_por_rol> rolAcciones, List<Accion_por_estado> estadoAcciones, int idTipoTransaccion, int idRolPersonal, int idEstadoActual);
        List<Rol> obtenerRolesTercero();
    }
}

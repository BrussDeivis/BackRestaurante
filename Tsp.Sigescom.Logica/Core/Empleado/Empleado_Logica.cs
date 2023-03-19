using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Custom;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Empleado;
using Tsp.Sigescom.Modelo.Interfaces.Logica;

namespace Tsp.Sigescom.Logica.Core.Empleado
{
    public class Empleado_Logica: IEmpleado_Logica
    {
        private readonly IEmpleado_Repositorio _empleadoRepositorio;

        public Empleado_Logica(IEmpleado_Repositorio empleadoRepositorio)
        {
            _empleadoRepositorio = empleadoRepositorio;
        }
        public int ObtenerIdEmpleado(string idUsuario)
        {
            try { return _empleadoRepositorio.ObtenerId(idUsuario); } catch (Exception e) { throw e; }
        }
        public string ObtenerNombreEmpleado(string idUsuario)
        {
            try { return _empleadoRepositorio.ObtenerNombre(idUsuario); } catch (Exception e) { throw e; }
        }
        public Empleado_ ObtenerEmpleadoInclusiveRoles(string idUsuario)
        {
            return _empleadoRepositorio.ObtenerEmpleado(idUsuario);
        }
        public Empleado_ ObtenerEmpleadoInclusiveRoles(int id)
        {
            return _empleadoRepositorio.ObtenerEmpleado(id);
        }

    }
}

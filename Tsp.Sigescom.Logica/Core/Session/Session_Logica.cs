using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.TipoCambio;
using Tsp.Sigescom.Modelo.Custom;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Entidades.Sesion;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Almacen;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Empleado;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;
using Tsp.Sigescom.Modelo.Negocio.Establecimientos;

namespace Tsp.Sigescom.Logica.Core.Permisos
{
    public class Session_Logica : ISession_Logica
    {
        protected readonly IRoles_Repositorio _rolesDatos;
        protected readonly IEmpleado_Repositorio _empleadoDatos;
        protected readonly ICentroDeAtencion_Logica _centroDeAtencionLogica;
        protected readonly ISede_Logica _sedeLogica;



        public Session_Logica(IRoles_Repositorio rolesDatos, IEmpleado_Repositorio empleadoDatos, ICentroDeAtencion_Logica centroDeAtencionLogica, ISede_Logica sedeLogica)
        {
            _rolesDatos = rolesDatos;
            _empleadoDatos = empleadoDatos;
            _centroDeAtencionLogica = centroDeAtencionLogica;
            _sedeLogica = sedeLogica;
        }


        public UserProfileSessionData GenerarSesionUsuario(string userId, string userName)
        {
            Empleado_ empleado = _empleadoDatos.ObtenerEmpleado(userId);
            List<CentroDeAtencion> centrosDeAtencionProgramados = _centroDeAtencionLogica.ObtenerCentrosDeAtencionProgramados_(empleado.Id);
            //A partir del user logueado, construir el model
            var profile = new UserProfileSessionData() { IdUsuario = userId, NombreUsuario = userName, CentrosDeAtencionProgramados = centrosDeAtencionProgramados };
            profile.Empleado = empleado ?? throw new Exception("No existe un empleado para la cuenta de usuario, por lo tanto no se puede realizar la operación");
            profile.SetCentrosDeAtencionProgramados(centrosDeAtencionProgramados);
            if (profile.CentrosDeAtencionProgramados.Count == 1) profile.CentroDeAtencionSeleccionado = profile.CentrosDeAtencionProgramados.FirstOrDefault();
            return profile;
        }

        /// <summary>
        /// Para APIs que requieran realizar transacciones, por defecto y por ahora, se utilizará un usuario que no tiene turnos asignados
        /// </summary>
        /// <returns></returns>
        public UserProfileSessionData GenerarSesionUsuario()
        {
            Empleado_ empleado = _empleadoDatos.ObtenerEmpleado(ActorSettings.Default.IdActorNegocioEmpleadoPorDefecto);
            List<CentroDeAtencion> centrosDeAtencionProgramados = _centroDeAtencionLogica.ObtenerCentrosDeAtencionProgramados_(empleado.Id);
            //A partir del user logueado, construir el model
            var profile = new UserProfileSessionData() { IdUsuario = "", NombreUsuario = "apiUser", CentrosDeAtencionProgramados = centrosDeAtencionProgramados };
            profile.Empleado = empleado ?? throw new Exception("No existe un empleado para la cuenta de usuario, por lo tanto no se puede realizar la operación");
            profile.SetCentrosDeAtencionProgramados(centrosDeAtencionProgramados);
            if (profile.CentrosDeAtencionProgramados.Count == 1) profile.CentroDeAtencionSeleccionado = profile.CentrosDeAtencionProgramados.FirstOrDefault();
            return profile;
        }


        public UserProfileSessionData ResolverSession(UserProfileSessionData profileData, TipoCambio tipoCambio, int idCentroDeAtencionInicioSesion)
        {
            profileData.Sede = _sedeLogica.ObtenerSedeConLogo();
            profileData.CentroDeAtencionSeleccionado = new CentroDeAtencion()
            {
                Id = idCentroDeAtencionInicioSesion,
            };
            profileData.IdCentroDeAtencionInicioSesion = idCentroDeAtencionInicioSesion;
            profileData.IdCentroAtencionQueTieneElStockIntegrada = idCentroDeAtencionInicioSesion;
            profileData.TipoDeCambio = tipoCambio;
            return profileData;
        }
    }
}

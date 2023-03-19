using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Custom;
using Tsp.Sigescom.Modelo;

namespace Tsp.Sigescom.Logica
{
    public partial class ActorNegocioLogica
    {




        public List<Empleado> ObtenerCajerosVigentes()
        {
            return Empleado.Convert(_actorRepositorio.ObtenerActorDeNegocioPrincipal(ActorSettings.Default.IdRolEmpleado, ActorSettings.Default.IdRolCajero, true).ToList());
        }

        public List<Empleado> ObtenerAlmacenerosVigentes()
        {
            return Empleado.Convert(_actorRepositorio.ObtenerActorDeNegocioPrincipal(ActorSettings.Default.IdRolEmpleado, ActorSettings.Default.IdRolAlmacenero, true).ToList());

        }

        public List<Empleado> ObtenerVendedoresVigentes()
        {
            return Empleado.Convert(_actorRepositorio.ObtenerActorDeNegocioPrincipal(ActorSettings.Default.IdRolEmpleado, ActorSettings.Default.IdRolVendedor, true).ToList());

        }

        public List<Empleado> ObtenerCompradoresVigentes()
        {
            return Empleado.Convert(_actorRepositorio.ObtenerActorDeNegocioPrincipal(ActorSettings.Default.IdRolEmpleado, ActorSettings.Default.IdRolComprador, true).ToList());

        }
        public DateTime FechaValidaParaBD(DateTime fechaNacimiento)
        {

            DateTime fechaNacimientoValido;
            DateTime dateNullBD = new DateTime(0001, 01, 1, 0, 0, 0);
            int result = DateTime.Compare(dateNullBD, fechaNacimiento);

            if (result == 0)
            {
                fechaNacimientoValido = DateTimeUtil.FechaActual();
                return fechaNacimientoValido;
            }

            return fechaNacimiento;
        }
        public OperationResult CrearEmpleado(string codigo, int idTipoDocumentoIdentidad, string numeroDocumentoIdentidad, string apellidoPaterno, string apellidoMaterno, string nombres, string correo, int idClaseActor, int idEstadoLegalActor, DateTime fechaNacimiento, string telefono, List<int> roles, List<Direccion> direcciones)
        {
            try
            {
                //validamos el documento de identidad
                _validacionActorNegocio_Logica.ValidarExistenciaDeDocumento(idTipoDocumentoIdentidad, numeroDocumentoIdentidad);
                DateTime fechaActual = DateTimeUtil.FechaActual();
                DateTime fechaFin = fechaActual.AddYears(ActorSettings.Default.vigenciaEnAnyosPorDefectoDeActorDeNegocioEntidadInterna);
                telefono = telefono ?? "";
                correo = correo ?? "";
                //string nombreComercial = nombres + " " + apellidoPaterno + " " + apellidoMaterno;
                //string razonSocial =  razonSocial = apellidoPaterno + "|" + apellidoMaterno + "|" + nombres;
       
    
                if (idTipoDocumentoIdentidad == ActorSettings.Default.IdTipoDocumentoIdentidadDni && numeroDocumentoIdentidad.Trim().Length != 8) throw new Exception("El numero de documento de identidad debe de tener 8 digitos");
                if (idTipoDocumentoIdentidad == ActorSettings.Default.IdTipoDocumentoIdentidadRuc && numeroDocumentoIdentidad.Trim().Length != 11) throw new Exception("El numero de documento de identidad debe de tener 11 digitos");
                //Verificamos si el codigo es nulo obtenemos un codigo
                codigo = codigo ?? ObtenerProximoCodigo(ActorSettings.Default.IdRolEmpleado);
                //creamos el Actor de negocio
                Actor_negocio empleado = new Actor_negocio(ActorSettings.Default.IdRolEmpleado, fechaActual, fechaFin, codigo, true, false, "");
                //creamos al actor
                var primerNombre = apellidoPaterno + "|" + apellidoMaterno + "|" + nombres;
                var segundoNombre = nombres + " " + apellidoPaterno + " " + apellidoMaterno;
                var tercerNombre = nombres + " " + apellidoPaterno.Substring(0, 1) + apellidoMaterno.Substring(0, 1);
                Actor actor = new Actor(idTipoDocumentoIdentidad, FechaValidaParaBD(fechaNacimiento), numeroDocumentoIdentidad, primerNombre, segundoNombre, telefono, ActorSettings.Default.IdTipoActorPersonaNatural, ActorSettings.Default.IdFotoActorPorDefecto, idClaseActor, idEstadoLegalActor, correo, tercerNombre, "");
                //guardamos las direcciones
                actor.Direccion = direcciones;
                //asignamos en actor al empleado
                empleado.Actor = actor;
                //agregamos los roles personal
                if (roles != null)
                {
                    foreach (var item in roles)
                    {
                        empleado.Actor.Actor_negocio.Add(new Actor_negocio(item, fechaActual, fechaFin, codigo, true, false, ""));
                    }
                }
                return _actor_Repositorio.CrearActorNegocio(empleado);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al crear el empleado",e);
            }
        }
        public OperationResult CrearEmpleadoActualizandoActor(int idActor, string codigo, int idTipoDocumentoIdentidad, string numeroDocumentoIdentidad, string apellidoPaterno, string apellidoMaterno, string nombres, string correo, int idClaseActor, int idEstadoLegalActor, DateTime fechaNacimiento, string telefono, List<int> roles, List<Direccion> direcciones)
        {
            try
            {
                DateTime fechaActual = DateTimeUtil.FechaActual();
                DateTime fechaFin = fechaActual.AddYears(ActorSettings.Default.vigenciaEnAnyosPorDefectoDeActorDeNegocioEntidadInterna);
                telefono = telefono ?? "";
                correo = correo ?? "";
                //Verificamos si el codigo es nulo obtenemos un codigo
                codigo = codigo ?? ObtenerProximoCodigo(ActorSettings.Default.IdRolEmpleado);
                //creamos el Actor de negocio
                Actor_negocio empleado = new Actor_negocio(idActor, ActorSettings.Default.IdRolEmpleado, fechaActual, fechaFin, codigo, true, false, "", null);
                //creamos al actor
                var primerNombre = apellidoPaterno + "|" + apellidoMaterno + "|" + nombres;
                var segundoNombre = nombres + " " + apellidoPaterno + " " + apellidoMaterno;
                var tercerNombre = nombres + " " + apellidoPaterno.Substring(0, 1) + apellidoMaterno.Substring(0, 1);
                Actor actor = new Actor(idActor, idTipoDocumentoIdentidad, FechaValidaParaBD(fechaNacimiento), numeroDocumentoIdentidad, primerNombre, segundoNombre, telefono, ActorSettings.Default.IdTipoActorPersonaNatural, ActorSettings.Default.IdFotoActorPorDefecto, idClaseActor, idEstadoLegalActor, correo, tercerNombre, "");
                //guardamos las direcciones
                actor.Direccion = direcciones;
                //asignamos en actor al empleado
                empleado.Actor = actor;
                //agregamos los roles personal
                if (roles != null)
                {
                    foreach (var item in roles)
                    {
                        empleado.Actor.Actor_negocio.Add(new Actor_negocio(item, fechaActual, fechaFin, codigo, true, false, ""));
                    }
                }
                return _actorRepositorio.CrearActorNegocioActualizandoActor(empleado);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al crear el empleado y actualizar el actor",e);
            }
        }

        public OperationResult ActualizarEmpleado(int idEmpleado, int idActor, string codigo, int idTipoDocumentoIdentidad, string numeroDocumentoIdentidad, string apellidoPaterno, string apellidoMaterno, string nombres, string correo, int idClaseActor, int idEstadoLegalActor, DateTime fechaNacimiento, string telefono, List<int> roles, List<Direccion> direcciones)
        {
            try
            {
                DateTime fechaActual = DateTimeUtil.FechaActual();
                DateTime fechaFin = fechaActual.AddYears(ActorSettings.Default.vigenciaEnAnyosPorDefectoDeActorDeNegocioEntidadInterna);

                telefono = telefono ?? "";
                correo = correo ?? "";
                codigo = codigo ?? numeroDocumentoIdentidad;
                //creamos el Actor de negocio
                Actor_negocio empleado = new Actor_negocio(idEmpleado, idActor, ActorSettings.Default.IdRolEmpleado, fechaActual, fechaFin, codigo, true, false, "");
                //creamos al actor
                var primerNombre = apellidoPaterno + "|" + apellidoMaterno + "|" + nombres;
                var segundoNombre = nombres + " " + apellidoPaterno + " " + apellidoMaterno;
                var tercerNombre = nombres + " " + apellidoPaterno.Substring(0, 1) + apellidoMaterno.Substring(0, 1);
                Actor actor = new Actor(idActor, idTipoDocumentoIdentidad, FechaValidaParaBD(fechaNacimiento), numeroDocumentoIdentidad, primerNombre, segundoNombre, telefono, ActorSettings.Default.IdTipoActorPersonaNatural, ActorSettings.Default.IdFotoActorPorDefecto, idClaseActor, idEstadoLegalActor, correo, tercerNombre, "");
                //guardamos las direcciones
                actor.Direccion = direcciones;
                //asignamos en actor al empleado
                empleado.Actor = actor;
                //agregamos los roles personal
                if (roles != null)
                {
                    foreach (var item in roles)
                    {
                        empleado.Actor.Actor_negocio.Add(new Actor_negocio(idActor, item, fechaActual, fechaFin, codigo, true, false, "", null));
                    }
                }
                var resultadoActualizarEmpleado= _actor_Repositorio.ActualizarActorNegocio(empleado);
                return resultadoActualizarEmpleado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al actualizar el empleado",e);
            }
        }

        public Empleado ObtenerEmpleado(int id)
        {
            try
            {
                Actor_negocio actorNegocio = _actorRepositorio.obtenerActorDeNegocio(id, ActorSettings.Default.IdRolEmpleado);
                return new Empleado(actorNegocio);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Empleado> ObtenerEmpleadosVigentes()
        {
            try { return Empleado.Convert(_actor_Repositorio.ObtenerActorDeNegocioPorRolVigentesAhora(ActorSettings.Default.IdRolEmpleado).ToList()); }
            catch (Exception e) { throw e; }
        }

        public OperationResult EstablecerUsuario(string idUsuario, int idEmpleado)
        {
            try
            {
                return _actorRepositorio.establecerIdUsuario(idUsuario, idEmpleado);
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        public OperationResult DarDeBajaEmpleado(int idEmpleado)
        {
            try
            {
                return _actor_Repositorio.DarDeBajaActorNegocioAhora(idEmpleado, ActorSettings.Default.IdRolEmpleado);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int[] obtenerAccionesPosiblesParaEmpleado(string idUsuario, int idTipoTransaccion)
        {
            try
            {
                return _actorRepositorio.obtenerAccionesPosibles(idUsuario, idTipoTransaccion);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int[] ObtenerIdsAccionesPosibles(string idUsuario, int idEmpleado)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}

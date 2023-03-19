using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Custom;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;

namespace Tsp.Sigescom.Logica
{
    public partial class ActorNegocioLogica
    {

        #region CREAR Y ACTUALIZAR
        Direccion DireccionNoEspecificada()
        {
            return new Direccion()
            {
                id_tipo_direccion = MaestroSettings.Default.IdDetalleMaestroTipoDireccionDomicilioFiscal,
                es_activo = true,
                es_principal = true,
                id_nacion = MaestroSettings.Default.IdDetalleMaestroNacionPeru,
                id_tipo_via = null,
                id_tipo_zona = null,
                id_ubigeo = ActorSettings.Default.idUbigeoNoEspecificado,
                detalle = "-"
            };
        }


        public ActorComercial_ ObtenerClienteGenerico()
        {
            return _actor_Repositorio.ObtenerActorComercial(ActorSettings.Default.IdClienteGenerico);
        }

        /// <summary>
        /// Como no se tiene dirección, se creará una por defecto.
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        public OperationResult CrearCliente(RegistroActorComercial cliente)
        {
            try
            {
                return GuardarActorComercial(ActorSettings.Default.IdRolCliente, cliente);
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }
        public OperationResult CrearCliente(int idTipoPersona, string razonSocial, string apellidoPaterno, string apellidoMaterno, string nombres, string nombreComercial, string nombreCorto, int idTipoDocumentoIdentidad, string numeroDocumentoIdentidad, int? idClaseActor, int? idEstadoLegalActor, string correo, string telefono, List<Direccion> direcciones, int idComprobantePredeterminado)
        {
            try
            {
                //validamos el documento de identidad
                _validacionActorNegocio_Logica.ValidarExistenciaDeDocumento(idTipoDocumentoIdentidad, numeroDocumentoIdentidad);

                DateTime fechaActual = DateTimeUtil.FechaActual();
                DateTime fechaFin = fechaActual.AddYears(ActorSettings.Default.vigenciaEnAnyosPorDefectoDeActorDeNegocioCliente);
                telefono = telefono ?? "";
                correo = correo ?? "";

                if (idTipoPersona == ActorSettings.Default.IdTipoActorPersonaNatural)
                {
                    idClaseActor = idClaseActor != null ? idClaseActor : ActorSettings.Default.IdClaseActorPersonaNaturalPorDefecto;
                    idEstadoLegalActor = idEstadoLegalActor != null ? idEstadoLegalActor : ActorSettings.Default.IdEstadoLegalActorPersonaNaturalPorDefecto;
                    razonSocial = apellidoPaterno + "|" + apellidoMaterno + "|" + nombres;

                }
                else if (idTipoPersona == ActorSettings.Default.IdTipoActorPersonaJuridica)
                {
                    idClaseActor = idClaseActor != null ? idClaseActor : ActorSettings.Default.IdClaseActorPersonaJuridicaPorDefecto;
                    idEstadoLegalActor = idEstadoLegalActor != null ? idEstadoLegalActor : ActorSettings.Default.IdEstadoLegalActorPersonaJuridicaPorDefecto;

                }
                else
                {
                    idClaseActor = idClaseActor != null ? idClaseActor : ActorSettings.Default.IdClaseActorEntidadInternaPorDefecto;
                    idEstadoLegalActor = idEstadoLegalActor != null ? idEstadoLegalActor : ActorSettings.Default.IdEstadoLegalActorEntidadInternaPorDefecto;
                }

                if (idTipoDocumentoIdentidad == ActorSettings.Default.IdTipoDocumentoIdentidadDni && numeroDocumentoIdentidad.Trim().Length != 8) throw new Exception("El numero de documento de identidad debe de tener 8 digitos");
                if (idTipoDocumentoIdentidad == ActorSettings.Default.IdTipoDocumentoIdentidadRuc && numeroDocumentoIdentidad.Trim().Length != 11) throw new Exception("El numero de documento de identidad debe de tener 11 digitos");

                string codigo = ObtenerProximoCodigo(ActorSettings.Default.IdRolCliente);
                //creamos el Actor de negocio
                Actor_negocio cliente = new Actor_negocio(ActorSettings.Default.IdRolCliente, fechaActual, fechaFin, codigo, true, false, "");
                //creamos al actor

                Actor actor = new Actor(idTipoDocumentoIdentidad, fechaActual, numeroDocumentoIdentidad, razonSocial, nombreComercial, telefono, idTipoPersona,
                    ActorSettings.Default.IdFotoActorPorDefecto, (int)idClaseActor, (int)idEstadoLegalActor, correo, nombreCorto, "");
                //guardamos las direcciones si hay por lo menos una
                if (direcciones != null)
                {
                    actor.Direccion = direcciones;
                }
                //Agregamos el parametro de comprobante predeterminado si tiene
                if (idComprobantePredeterminado != 0)
                {
                    cliente.Parametro_actor_negocio.Add(new Parametro_actor_negocio(MaestroSettings.Default.IdDetalleMaestroParametroComprobanteDeClientePredeterminado, idComprobantePredeterminado.ToString()));
                }
                //asignamos en actor al proveedor
                cliente.Actor = actor;

                return _actor_Repositorio.CrearActorNegocio(cliente);
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }
        public OperationResult CrearClienteActualizandoActor(int idActor, int idTipoActor, string razonSocial, string apellidoPaterno, string apellidoMaterno, string nombres, string nombreComercial, string nombreCorto, int idTipoDocumentoIdentidad, string numeroDocumentoIdentidad, int? idClaseActor, int? idEstadoLegalActor, string correo, string telefono, List<Direccion> direcciones, int idComprobantePredeterminado)
        {
            try
            {
                DateTime fechaActual = DateTimeUtil.FechaActual();
                DateTime fechaFin = fechaActual.AddYears(ActorSettings.Default.vigenciaEnAnyosPorDefectoDeActorDeNegocioCliente);
                telefono = telefono ?? "";
                correo = correo ?? "";

                if (idTipoActor == ActorSettings.Default.IdTipoActorPersonaNatural)
                {
                    idClaseActor = idClaseActor != null ? idClaseActor : ActorSettings.Default.IdClaseActorPersonaNaturalPorDefecto;
                    idEstadoLegalActor = idEstadoLegalActor != null ? idEstadoLegalActor : ActorSettings.Default.IdEstadoLegalActorPersonaNaturalPorDefecto;
                    razonSocial = apellidoPaterno + "|" + apellidoMaterno + "|" + nombres;
                }
                else if (idTipoActor == ActorSettings.Default.IdTipoActorPersonaJuridica)
                {
                    idClaseActor = idClaseActor != null ? idClaseActor : ActorSettings.Default.IdClaseActorPersonaJuridicaPorDefecto;
                    idEstadoLegalActor = idEstadoLegalActor != null ? idEstadoLegalActor : ActorSettings.Default.IdEstadoLegalActorPersonaJuridicaPorDefecto;
                }
                else
                {
                    idClaseActor = idClaseActor != null ? idClaseActor : ActorSettings.Default.IdClaseActorEntidadInternaPorDefecto;
                    idEstadoLegalActor = idEstadoLegalActor != null ? idEstadoLegalActor : ActorSettings.Default.IdEstadoLegalActorEntidadInternaPorDefecto;
                }
                string codigo = ObtenerProximoCodigo(ActorSettings.Default.IdRolProveedor);
                //creamos el Actor de negocio
                Actor_negocio cliente = new Actor_negocio(idActor, ActorSettings.Default.IdRolCliente, fechaActual, fechaFin, codigo, true, false, "", null);
                //creamos al actor
                Actor actor = new Actor(idActor, idTipoDocumentoIdentidad, fechaActual, numeroDocumentoIdentidad, razonSocial, nombreComercial, telefono, idTipoActor,
                    ActorSettings.Default.IdFotoActorPorDefecto, (int)idClaseActor, (int)idEstadoLegalActor, correo, nombreCorto, "");
                //guardamos las direcciones si hay 
                if (direcciones != null)
                {
                    actor.Direccion = direcciones;
                }
                //Agregamos el parametro de comprobante predeterminado si tiene
                if (idComprobantePredeterminado != 0)
                {
                    cliente.Parametro_actor_negocio.Add(new Parametro_actor_negocio(MaestroSettings.Default.IdDetalleMaestroParametroComprobanteDeClientePredeterminado, idComprobantePredeterminado.ToString()));
                }
                //asignamos en actor al proveedor
                cliente.Actor = actor;

                return _actorRepositorio.CrearActorNegocioActualizandoActor(cliente);
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }

        public OperationResult ActualizarCliente(int idCliente, int idActor, string codigo, int idTipoActor, string razonSocial, string apellidoPaterno, string apellidoMaterno, string nombres, string nombreComercial, string nombreCorto, int idTipoDocumentoIdentidad, string numeroDocumentoIdentidad, int? idClaseActor, int? idEstadoLegalActor, string correo, string telefono, List<Direccion> direcciones, int idComprobantePredeterminado)
        {
            try
            {
                DateTime fechaActual = DateTimeUtil.FechaActual();
                DateTime fechaFin = fechaActual.AddYears(ActorSettings.Default.vigenciaEnAnyosPorDefectoDeActorDeNegocioCliente);
                telefono = telefono ?? "";
                correo = correo ?? "";
                codigo = codigo ?? "";

                if (idTipoActor == ActorSettings.Default.IdTipoActorPersonaNatural)
                {
                    idClaseActor = idClaseActor != null ? idClaseActor : ActorSettings.Default.IdClaseActorPersonaNaturalPorDefecto;
                    idEstadoLegalActor = idEstadoLegalActor != null ? idEstadoLegalActor : ActorSettings.Default.IdEstadoLegalActorPersonaNaturalPorDefecto;
                    razonSocial = apellidoPaterno + "|" + apellidoMaterno + "|" + nombres;
                }
                else if (idTipoActor == ActorSettings.Default.IdTipoActorPersonaJuridica)
                {
                    idClaseActor = idClaseActor != null ? idClaseActor : ActorSettings.Default.IdClaseActorPersonaJuridicaPorDefecto;
                    idEstadoLegalActor = idEstadoLegalActor != null ? idEstadoLegalActor : ActorSettings.Default.IdEstadoLegalActorPersonaJuridicaPorDefecto;
                }
                else
                {
                    idClaseActor = idClaseActor != null ? idClaseActor : ActorSettings.Default.IdClaseActorEntidadInternaPorDefecto;
                    idEstadoLegalActor = idEstadoLegalActor != null ? idEstadoLegalActor : ActorSettings.Default.IdEstadoLegalActorEntidadInternaPorDefecto;
                }
                //creamos el Actor de negocio
                Actor_negocio cliente = new Actor_negocio(idCliente, idActor, ActorSettings.Default.IdRolCliente, fechaActual, fechaFin, codigo, true, false, "");
                //creamos al actor
                Actor actor = new Actor(idActor, idTipoDocumentoIdentidad, fechaActual, numeroDocumentoIdentidad, razonSocial, nombreComercial,
                    telefono, idTipoActor,
                    ActorSettings.Default.IdFotoActorPorDefecto, (int)idClaseActor, (int)idEstadoLegalActor, correo, nombreCorto, "");
                //guardamos las direcciones si hay 
                if (direcciones != null)
                {
                    actor.Direccion = direcciones;
                }
                //Agregamos el parametro de comprobante predeterminado si tiene
                if (idComprobantePredeterminado != 0)
                {
                    cliente.Parametro_actor_negocio.Add(new Parametro_actor_negocio(idCliente, MaestroSettings.Default.IdDetalleMaestroParametroComprobanteDeClientePredeterminado, idComprobantePredeterminado.ToString()));
                }
                //asignamos en actor al cliente
                cliente.Actor = actor;

                return _actor_Repositorio.ActualizarActorNegocio(cliente);
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }
        public OperationResult DarDeBajaCliente(int idCliente)
        {
            try
            {
                return _actor_Repositorio.DarDeBajaActorNegocioAhora(idCliente, ActorSettings.Default.IdRolCliente);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region consultas

        public List<ResumenCliente> ObtenerClientesVigentes()
        {
            try
            {
                return _actor_Repositorio.ObtenerResumenClientesVigentes().ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<ItemGenerico>> ObtenerClientesVigentesComoItemsGenericos()
        {
            try
            {
                return (await _actorRepositorio.ObtenerActoresDeNegocioPrincipalesVigentesComoItemsGenericos(ActorSettings.Default.IdRolEmpleado, ActorSettings.Default.IdRolCliente)).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<List<ItemGenerico>> ObtenerClientesVigentesComoItemsGenericosPorIdRol()
        {
            try
            {
                var clientes = (await _actorRepositorio.ObtenerActoresDeNegocioPrincipalesVigentesComoItemsGenericosPorIdRol(ActorSettings.Default.IdRolCliente)).ToList();
                clientes.ForEach(c => c.Nombre = c.Nombre.Replace("|", " "));
                return clientes;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //To do:Mejorar esto por favor
        public List<Modelo.Entidades.Custom.Cliente> ObtenerClientesConCuotasVigentes()
        {
            try
            {
                return Modelo.Entidades.Custom.Cliente.Convert(_actorRepositorio.ObtenerActorDeNegocioIncluidoTransaccion1PorRolVigentesAhora(ActorSettings.Default.IdRolCliente).ToList());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Modelo.Entidades.Custom.Cliente> ObtenerClientesVigentesParaVenta()
        {
            try
            {
                return Modelo.Entidades.Custom.Cliente.Convert(_actorRepositorio.obtenerActorDeNegocioIncluidoActorPorRolVigentesAhora(ActorSettings.Default.IdRolCliente).ToList());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Cliente ObtenerCliente(int id)
        {
            try
            {
                Actor_negocio actorNegocio = _actorRepositorio.obtenerActorDeNegocio(id, ActorSettings.Default.IdRolCliente);
                return new Modelo.Entidades.Custom.Cliente(actorNegocio);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        #endregion

        #region

        public ActorComercial_ ObtenerClientePorDni(string dni)
        {
            Modelo.Custom.ActorComercial_ cliente = null;
            var actorDeNegocio = _actorRepositorio.ObtenerActorDeNegocioVigentePorDocumentoIdentidad(dni, ActorSettings.Default.IdRolCliente);
            if (actorDeNegocio != null)
            {
                cliente = new ActorComercial_(actorDeNegocio);
            }
            return cliente;
        }

        #endregion

        public string ObtenerUltimaPlacaDeCliente(int idCliente)
        {
            try
            {
                return _transaccionRepositorio.ObtenerUltimoRegistroDeDetalleTransaccionDeTransaccionOrdenVentaDeUnCliente(idCliente);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener el ultimo registro de detalle de transaccion de un cliente.");
            }
        }
    }

}

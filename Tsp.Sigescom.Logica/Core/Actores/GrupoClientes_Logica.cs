using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Custom;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Actores;
using Tsp.Sigescom.Modelo.Interfaces.Datos.CentrosDeAtencion;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;
using Tsp.Sigescom.Modelo.Negocio.Actores;
using Tsp.Sigescom.Modelo.Negocio.Almacen;

namespace Tsp.Sigescom.Logica
{
    public partial class GrupoClientes_Logica : IGrupoClientes_Logica
    {
        private readonly IActor_Repositorio _actor_Repositorio;
        private readonly IVinculoActor_Repositorio _vinculoActor_Repositorio;
        private readonly IConsultaActor_Repositorio _consultaActor_Repositorio;

        public GrupoClientes_Logica(IActor_Repositorio actor_Repositorio, IVinculoActor_Repositorio vinculoActor_Repositorio, IConsultaActor_Repositorio consultaActor_Repositorio)
        {
            _actor_Repositorio = actor_Repositorio;
            _vinculoActor_Repositorio = vinculoActor_Repositorio;
            _consultaActor_Repositorio = consultaActor_Repositorio;
        }

        public GrupoClientes_Logica()
        {
        }
        public OperationResult CrearGrupoClientes(GrupoClientes grupoClientes)
        {
            try
            {
                if (_vinculoActor_Repositorio.ExisteCodigoGrupoClientes((int)TipoVinculo.MiembroGrupo, grupoClientes.Codigo))
                {
                    throw new LogicaException("Error al intentar crear el grupo de clientes, el codigo de grupo de clientes ya existe.");
                }
                if (_vinculoActor_Repositorio.ExisteNombreGrupoClientesEnGruposClientesVigentes((int)TipoVinculo.MiembroGrupo, grupoClientes.Nombre))
                {
                    throw new LogicaException("Error al intentar crear el grupo de clientes, el nombre de grupo de clientes ya existe.");
                }
                var grupoActorNegocio = GenerarGrupoClientesActorNegocio(grupoClientes);
                var resultado = _actor_Repositorio.CrearActorNegocio(grupoActorNegocio);
                return resultado;
            }
            catch (Exception e) { throw e; }
        }
        public OperationResult ActualizarGrupoClientes(GrupoClientes grupoClientes)
        {
            try
            {
                if (_vinculoActor_Repositorio.ExisteCodigoGrupoClientesExceptoGrupoClientes((int)TipoVinculo.MiembroGrupo, grupoClientes.Codigo, grupoClientes.Id))
                {
                    throw new LogicaException("Error al intentar crear el grupo de clientes, el codigo de grupo de clientes ya existe.");
                }
                if (_vinculoActor_Repositorio.ExisteNombreGrupoClientesEnGruposClientesVigentesExceptoGrupoClientes((int)TipoVinculo.MiembroGrupo, grupoClientes.Nombre, grupoClientes.Id))
                {
                    throw new LogicaException("Error al intentar crear el grupo de clientes, el nombre de grupo de clientes ya existe.");
                }
                ValidarActualizacionGrupoClientes(grupoClientes);
                var grupoActorNegocio = GenerarGrupoClientesActorNegocio(grupoClientes);
                grupoActorNegocio.id = grupoClientes.Id;
                grupoActorNegocio.id_actor = grupoClientes.IdActor;
                grupoActorNegocio.Actor.id = grupoClientes.IdActor;
                return _vinculoActor_Repositorio.ActualizarActorPrincipalConVinculosActorNegocio(grupoActorNegocio);
            }
            catch (Exception e) { throw e; }
        }
        public void ValidarActualizacionGrupoClientes(GrupoClientes grupoClientes)
        {
            var grupoClientesActual = ObtenerGrupoClientes(grupoClientes.Id);
            foreach (var cliente in grupoClientesActual.Clientes)
            {
                if (!grupoClientes.Clientes.Select(c => c.Id).Contains(cliente.Id))
                {
                    if (_vinculoActor_Repositorio.ExisteDeudaDeClienteEnOperacionesVentaConGrupoClientes(cliente.Id, grupoClientes.Id))
                    {
                        throw new LogicaException("Error al intentar actualizar el grupo de clientes, el cliente " + cliente.Nombre + " tiene deuda pendiente.");
                    }
                }
            }
        }
        private Actor_negocio GenerarGrupoClientesActorNegocio(GrupoClientes grupoClientes)
        {
            try
            {
                DateTime fechaActual = DateTimeUtil.FechaActual();
                DateTime fechaFin = fechaActual.AddYears(ActorSettings.Default.vigenciaEnAnyosPorDefectoDeActorDeNegocioEntidadInterna);
                //Crear el actor de negocio
                Actor_negocio grupoActorNegocio = new Actor_negocio(ActorSettings.Default.IdRolGrupoClientes, fechaActual, fechaFin, "", true, false, "");
                //Crear al actor
                Actor actor = new Actor(ActorSettings.Default.IdDetalleMaestroDocumentoIdentidadGrupoClientes, fechaActual, grupoClientes.Codigo, grupoClientes.Nombre, "", "", ActorSettings.Default.IdTipoActorGrupoClientes, ActorSettings.Default.IdFotoActorPorDefecto, ActorSettings.Default.IdClaseActorGrupoClientes, ActorSettings.Default.IdEstadoLegalGrupoClientes, "", "", "")
                {
                    id_detalle_multiproposito = grupoClientes.Tipo.Id,
                    id_detalle_multiproposito1 = grupoClientes.Clasificacion.Id
                };
                //Asignar el actor al grupo actor negocio
                grupoActorNegocio.Actor = actor; 
                //Asignar el responsable al grupo de clientes
                grupoActorNegocio.Vinculo_Actor_Negocio1 = new List<Vinculo_Actor_Negocio> { new Vinculo_Actor_Negocio
                {
                    id_actor_negocio_principal = grupoClientes.Responsable.Id,
                    desde = fechaActual,
                    hasta = fechaFin,
                    descripcion = "",
                    tipo_vinculo = (int)TipoVinculo.ResponsableGrupo,
                    es_vigente = true
                } };
                //Asignar los clientes como miembros de grupo de clientes
                grupoActorNegocio.Vinculo_Actor_Negocio = new List<Vinculo_Actor_Negocio>();
                if (grupoClientes.Clientes != null && grupoClientes.Clientes.Count > 0)
                {
                    foreach (var cliente in grupoClientes.Clientes)
                    {
                        var vinculo = new Vinculo_Actor_Negocio
                        {
                            id_actor_negocio_principal = grupoClientes.Id,
                            id_actor_negocio_vinculado = cliente.Id,
                            desde = fechaActual,
                            hasta = fechaFin,
                            descripcion = "",
                            tipo_vinculo = (int)TipoVinculo.MiembroGrupo,
                            es_vigente = true
                        };
                        grupoActorNegocio.Vinculo_Actor_Negocio.Add(vinculo);
                    }
                }
                return grupoActorNegocio;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar generar actor negocio habitacion", e);
            }
        }
        public List<GrupoClientesResumen> ObtenerGruposClientes()
        {
            try
            {
                var gruposClientes = _vinculoActor_Repositorio.ObtenerGruposClientes().ToList();
                return gruposClientes;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener grupos de clientes", e);
            }
        }
        public GrupoClientes ObtenerGrupoClientes(int idGrupoClientes)
        {
            try
            {
                var grupoClientes = _vinculoActor_Repositorio.ObtenerGrupoClientes((int)TipoVinculo.MiembroGrupo, idGrupoClientes);
                grupoClientes.Responsable = _consultaActor_Repositorio.ObtenerActorComercial_(ActorSettings.Default.IdRolCliente, grupoClientes.Responsable.Id);
                return grupoClientes;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener grupo de clientes", e);
            }
        }
        public OperationResult DarBajaGrupoClientes(int idGrupoClientes)
        {
            try
            {
                DateTime fechaActual = DateTimeUtil.FechaActual();
                DateTime fechaFin = fechaActual.AddYears(10);
                if (_vinculoActor_Repositorio.ExisteDeudaDeGrupoClientesEnOperacionesVenta(idGrupoClientes))
                {
                    throw new LogicaException("Error al intentar dar de baja el grupo de clientes, tienen deudas pendientes.");
                }
                var grupoClientes = ObtenerGrupoClientes(idGrupoClientes);
                var grupoActorNegocio = GenerarGrupoClientesActorNegocio(grupoClientes);
                grupoActorNegocio.id = grupoClientes.Id;
                grupoActorNegocio.id_actor = grupoClientes.IdActor;
                grupoActorNegocio.Actor.id = grupoClientes.IdActor;
                grupoActorNegocio.es_vigente = false;
                grupoActorNegocio.fecha_fin = fechaActual;
                return _vinculoActor_Repositorio.ActualizarActorPrincipalConVinculosActorNegocio(grupoActorNegocio);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar dar de baja el grupo de clientes", e);
            }
        }

    }
}

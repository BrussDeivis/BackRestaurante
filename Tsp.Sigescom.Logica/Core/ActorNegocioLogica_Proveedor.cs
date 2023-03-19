using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Logica
{
    public partial class ActorNegocioLogica
    {
        #region Crear y actualizar
        public OperationResult CrearProveedor(int idTipoPersona, string razonSocial, string apellidoPaterno, string apellidoMaterno, string nombres, string nombreComercial, string nombreCorto, int idTipoDocumentoIdentidad,
            string numeroDocumentoIdentidad, int? idClaseActor, int? idEstadoLegalActor, string correo, string telefono, List<Direccion> direcciones)
        {
            try
            {
                //validamos el documento de identidad
                _validacionActorNegocio_Logica.ValidarExistenciaDeDocumento(idTipoDocumentoIdentidad, numeroDocumentoIdentidad);

                DateTime fechaActual = DateTimeUtil.FechaActual();
                DateTime fechaFin = fechaActual.AddYears(ActorSettings.Default.vigenciaEnAnyosPorDefectoDeActorDeNegocioProveedor);
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

                string codigo = ObtenerProximoCodigo(ActorSettings.Default.IdRolProveedor);
                //creamos el Actor de negocio
                Actor_negocio proveedor = new Actor_negocio(ActorSettings.Default.IdRolProveedor, fechaActual, fechaFin, codigo, true, false, "");
                //creamos al actor

                Actor actor = new Actor(idTipoDocumentoIdentidad, fechaActual, numeroDocumentoIdentidad, razonSocial, nombreComercial, telefono, idTipoPersona,
                    ActorSettings.Default.IdFotoActorPorDefecto, (int)idClaseActor, (int)idEstadoLegalActor, correo, nombreCorto, "");
                //guardamos las direcciones si hay por lo menos una
                if (direcciones != null)
                {
                    actor.Direccion = direcciones;
                }
                //asignamos en actor al proveedor
                proveedor.Actor = actor;
                //agregamos los roles personal

                return _actor_Repositorio.CrearActorNegocio(proveedor);
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }
        public OperationResult CrearProveedorActualizandoActor(int idActor, int idTipoActor, string razonSocial, string apellidoPaterno, string apellidoMaterno, string nombres, string nombreComercial, string nombreCorto,
            int idTipoDocumentoIdentidad, string numeroDocumentoIdentidad, int? idClaseActor, int? idEstadoLegalActor, string correo, string telefono, List<Direccion> direcciones)
        {
            try
            {
                DateTime fechaActual = DateTimeUtil.FechaActual();
                DateTime fechaFin = fechaActual.AddYears(ActorSettings.Default.vigenciaEnAnyosPorDefectoDeActorDeNegocioProveedor);
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
                Actor_negocio proveedor = new Actor_negocio(idActor, ActorSettings.Default.IdRolProveedor, fechaActual, fechaFin, codigo, true, false, "",  null);
                //creamos al actor
                Actor actor = new Actor(idActor, idTipoDocumentoIdentidad, fechaActual, numeroDocumentoIdentidad, razonSocial, nombreComercial, telefono, idTipoActor,
                    ActorSettings.Default.IdFotoActorPorDefecto, (int)idClaseActor, (int)idEstadoLegalActor, correo, nombreCorto, "");
                //guardamos las direcciones si hay 
                if (direcciones != null)
                {
                    actor.Direccion = direcciones;
                }
                //asignamos en actor al proveedor
                proveedor.Actor = actor;

                return _actorRepositorio.CrearActorNegocioActualizandoActor(proveedor);
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }

        public OperationResult ActualizarProveedor(int idProveedor, int idActor, string codigo, int idTipoActor, string razonSocial, string apellidoPaterno, string apellidoMaterno, string nombres, string nombreComercial,
            string nombreCorto, int idTipoDocumentoIdentidad, string numeroDocumentoIdentidad, int? idClaseActor, int? idEstadoLegalActor, string correo, string telefono, List<Direccion> direcciones)
        {
            try
            {
                DateTime fechaActual = DateTimeUtil.FechaActual();
                DateTime fechaFin = fechaActual.AddYears(ActorSettings.Default.vigenciaEnAnyosPorDefectoDeActorDeNegocioProveedor);
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
                Actor_negocio proveedor = new Actor_negocio(idProveedor, idActor, ActorSettings.Default.IdRolProveedor, fechaActual, fechaFin, codigo, true, false, "");
                //creamos al actor
                Actor actor = new Actor(idActor, idTipoDocumentoIdentidad, fechaActual, numeroDocumentoIdentidad, razonSocial, nombreComercial,
                    telefono, idTipoActor,
                    ActorSettings.Default.IdFotoActorPorDefecto, (int)idClaseActor, (int)idEstadoLegalActor, correo, nombreCorto, "");
                //guardamos las direcciones si hay 
                if (direcciones != null)
                {
                    actor.Direccion = direcciones;
                }
                //asignamos en actor al proveedor
                proveedor.Actor = actor;

                return _actor_Repositorio.ActualizarActorNegocio(proveedor);
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }
        public OperationResult DarDeBajaProveedor(int idProveedor)
        {
            try
            {
                return _actor_Repositorio.DarDeBajaActorNegocioAhora(idProveedor, ActorSettings.Default.IdRolProveedor);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region consultas
        public List<Proveedor> ObtenerProveedores()
        {
            try
            {
                return Proveedor.Convert(_actorRepositorio.obtenerActorDeNegocioIncluidoActorPorRolVigentesAhora(ActorSettings.Default.IdRolProveedor).ToList());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Proveedor> ObtenerProveedoresVigentes()
        {
            try
            {
                return Proveedor.Convert(_actor_Repositorio.ObtenerActorDeNegocioPorRolVigentesAhora(ActorSettings.Default.IdRolProveedor).ToList());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Proveedor ObtenerProveedor(int id)
        {
            try
            {
                Actor_negocio actorNegocio = _actorRepositorio.obtenerActorDeNegocio(id, ActorSettings.Default.IdRolProveedor);
                return new Proveedor(actorNegocio);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<ItemGenerico>> ObtenerProveedoresVigentesComoItemsGenericos()
        {
            try
            {
                var proveedores = (await _actorRepositorio.ObtenerActoresDeNegocioPrincipalesVigentesComoItemsGenericosPorIdRol(ActorSettings.Default.IdRolProveedor)).ToList();
                proveedores.ForEach(p => p.Nombre = p.Nombre.Replace("|", " "));
                return proveedores;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

    }


}

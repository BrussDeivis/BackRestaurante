using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace Tsp.Sigescom.AccesoDatos.Actores
{
    public partial class VinculoActor_Datos : IVinculoActor_Repositorio
    {
        public IEnumerable<Vinculo_Actor_Negocio> ObtenerVinculosActorNegocioParaActorPrincipal(int idActorNegocioPrincipal, DateTime fecha, TipoVinculo tipo)
        {
            SigescomEntities _db = new SigescomEntities();
            return _db.Vinculo_Actor_Negocio.Where(van => van.id_actor_negocio_principal == idActorNegocioPrincipal && van.tipo_vinculo == (int)tipo && van.desde <= fecha && van.hasta >= fecha);
        }

        public OperationResult CaducarVinculoActorNegocio(int idVinculo, DateTime fechaCaducidad)
        {
            SigescomEntities _db = new SigescomEntities();
            var vinculo = _db.Vinculo_Actor_Negocio.SingleOrDefault(van => van.id == idVinculo && van.es_vigente);
            vinculo.hasta = fechaCaducidad;
            vinculo.es_vigente = false;
            _db.SaveChanges();
            return new OperationResult(OperationResultEnum.Success);
        }

        public bool ExisteVinculoVigente(int idActorNegocioVinculado, int idActorNegocioPrincipal)
        {
            SigescomEntities _db = new SigescomEntities();
            return _db.Vinculo_Actor_Negocio.Any(van => van.id_actor_negocio_vinculado == idActorNegocioVinculado && van.id_actor_negocio_principal == idActorNegocioPrincipal && van.es_vigente);
        }

        public OperationResult CrearVinculoActorNegocio(Vinculo_Actor_Negocio vinculoActorNegocio)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                _db.Vinculo_Actor_Negocio.Add(vinculoActorNegocio);
                _db.SaveChanges();
                var result = new OperationResult(OperationResultEnum.Success);
                result.data = vinculoActorNegocio.id;
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool ExisteCodigoGrupoClientes(int enumTipoVinculo, string codigoGrupoClientes)
        {
            SigescomEntities _db = new SigescomEntities();
            return _db.Vinculo_Actor_Negocio.Where(van => van.tipo_vinculo == enumTipoVinculo).Any(van => van.Actor_negocio.Actor.numero_documento_identidad == codigoGrupoClientes);
        }
        public bool ExisteNombreGrupoClientesEnGruposClientesVigentes(int enumTipoVinculo, string nombreGrupoClientes)
        {
            SigescomEntities _db = new SigescomEntities();
            return _db.Vinculo_Actor_Negocio.Where(van => van.tipo_vinculo == enumTipoVinculo).Any(van => van.Actor_negocio.Actor.primer_nombre == nombreGrupoClientes && van.Actor_negocio.es_vigente);
        }

        public bool ExisteCodigoGrupoClientesExceptoGrupoClientes(int enumTipoVinculo, string codigoGrupoClientes, int idGrupoClientes)
        {
            SigescomEntities _db = new SigescomEntities();
            return _db.Vinculo_Actor_Negocio.Where(van => van.tipo_vinculo == enumTipoVinculo).Any(van => van.Actor_negocio.Actor.numero_documento_identidad == codigoGrupoClientes && van.Actor_negocio.id != idGrupoClientes);
        }
        public bool ExisteNombreGrupoClientesEnGruposClientesVigentesExceptoGrupoClientes(int enumTipoVinculo, string nombreGrupoClientes, int idGrupoClientes)
        {
            SigescomEntities _db = new SigescomEntities();
            return _db.Vinculo_Actor_Negocio.Where(van => van.tipo_vinculo == enumTipoVinculo).Any(van => van.Actor_negocio.Actor.primer_nombre == nombreGrupoClientes && van.Actor_negocio.es_vigente && van.Actor_negocio.id != idGrupoClientes);
        }

        public bool ExisteDeudaDeClienteEnOperacionesVentaConGrupoClientes(int idCliente, int idGrupoClientes)
        {
            SigescomEntities _db = new SigescomEntities();
            return _db.Transaccion.Where(t => Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentas.Contains(t.id_tipo_transaccion) && t.id_actor_negocio_externo == idCliente && t.id_actor_negocio_externo1 == idGrupoClientes).SelectMany(t => t.Cuota).Any(c => c.pago_a_cuenta - c.total > 0);
        }

        public bool ExisteDeudaDeGrupoClientesEnOperacionesVenta(int idGrupoClientes)
        {
            SigescomEntities _db = new SigescomEntities();
            return _db.Transaccion.Where(t => Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentas.Contains(t.id_tipo_transaccion) && t.id_actor_negocio_externo1 == idGrupoClientes).SelectMany(t => t.Cuota).Any(c => c.saldo > 0);
        }

        public OperationResult ActualizarActorPrincipalConVinculosActorNegocio(Actor_negocio actorNegocioConVinculos)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                var dbActorNegocioPrincipal = _db.Actor_negocio.Include(an => an.Actor).Single(an => an.id == actorNegocioConVinculos.id);
                _db.Entry(dbActorNegocioPrincipal).CurrentValues.SetValues(actorNegocioConVinculos);
                _db.Entry(dbActorNegocioPrincipal.Actor).CurrentValues.SetValues(actorNegocioConVinculos.Actor);

                Vinculo_Actor_Negocio dbVinculoActoresDeNegocioResponsable = _db.Vinculo_Actor_Negocio.FirstOrDefault(van => van.id_actor_negocio_vinculado == actorNegocioConVinculos.id && van.es_vigente == true && van.tipo_vinculo == (int)TipoVinculo.ResponsableGrupo);

                dbVinculoActoresDeNegocioResponsable.id_actor_negocio_principal = actorNegocioConVinculos.Vinculo_Actor_Negocio1.First().id_actor_negocio_principal;

                List<Vinculo_Actor_Negocio> dbVinculosActoresDeNegocio = _db.Vinculo_Actor_Negocio.Where(van => van.id_actor_negocio_principal == actorNegocioConVinculos.id && van.es_vigente == true && van.tipo_vinculo == (int)TipoVinculo.MiembroGrupo).ToList();
                foreach (var dbVinculo in dbVinculosActoresDeNegocio)
                {
                    if (!actorNegocioConVinculos.Vinculo_Actor_Negocio.Any(van => van.id_actor_negocio_vinculado == dbVinculo.id_actor_negocio_vinculado))
                    {
                        dbVinculo.hasta = DateTimeUtil.FechaActual();
                        dbVinculo.es_vigente = false;
                    }
                }
                foreach (var vinculo in actorNegocioConVinculos.Vinculo_Actor_Negocio)
                {
                    if (!dbVinculosActoresDeNegocio.Any(van => van.id_actor_negocio_vinculado == vinculo.id_actor_negocio_vinculado))
                    {
                        _db.Vinculo_Actor_Negocio.Add(vinculo);
                    }
                }
                _db.SaveChanges();
                return new OperationResult(OperationResultEnum.Success);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<GrupoClientesResumen> ObtenerGruposClientes()
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                var resultado = _db.Actor_negocio.Where(an => an.id_rol == ActorSettings.Default.IdRolGrupoClientes).Select(an => new GrupoClientesResumen
                {
                    Id = an.id,
                    Codigo = an.Actor.numero_documento_identidad,
                    Nombre = an.Actor.primer_nombre,
                    EsVigente = an.es_vigente,
                    Tipo = an.Actor.Detalle_maestro1.nombre,
                    Clasificacion = an.Actor.Detalle_maestro2.nombre,
                    DocumentoResponsable = an.Vinculo_Actor_Negocio1.FirstOrDefault().Actor_negocio.Actor.Detalle_maestro.valor + " - " + an.Vinculo_Actor_Negocio1.FirstOrDefault().Actor_negocio.Actor.numero_documento_identidad,
                    NombreResponsable = an.Vinculo_Actor_Negocio1.FirstOrDefault().Actor_negocio.Actor.primer_nombre.Replace("|", " "),
                    TelefonoResponsable = an.Vinculo_Actor_Negocio1.FirstOrDefault().Actor_negocio.Actor.telefono,
                    CorreoResponsable = an.Vinculo_Actor_Negocio1.FirstOrDefault().Actor_negocio.Actor.correo,
                    Clientes = an.Vinculo_Actor_Negocio.Select(van => new MiembroGrupoClientes
                    {
                        Id = van.id_actor_negocio_vinculado,
                        Documento = van.Actor_negocio1.Actor.Detalle_maestro.valor + " - " + van.Actor_negocio1.Actor.numero_documento_identidad,
                        Nombre = van.Actor_negocio1.Actor.primer_nombre.Replace("|", " "),
                        EsVigente = van.es_vigente
                    }).ToList()
                }).ToList();
                //var resultado = _db.Vinculo_Actor_Negocio.Where(van => van.tipo_vinculo == enumTipoVinculo).GroupBy(van =>
                //new
                //{
                //    Id = van.id_actor_negocio_principal,
                //    CodigoGrupo = van.Actor_negocio.Actor.numero_documento_identidad,
                //    NombreGrupo = van.Actor_negocio.Actor.primer_nombre,
                //    EsVigente = van.es_vigente,
                //    Tipo = van.Detalle_maestro.nombre,
                //    Clasificacion = van.Detalle_maestro1.nombre,
                //    DocumentoResponsable = van.Actor_negocio2.Actor.Detalle_maestro.valor + " - " + van.Actor_negocio2.Actor.numero_documento_identidad,
                //    NombreResponsable = van.Actor_negocio2.Actor.primer_nombre.Replace("|", " "),
                //}).Select(g => new GrupoClientesResumen
                //{
                //    Id = g.Key.Id,
                //    CodigoGrupo = g.Key.CodigoGrupo,
                //    NombreGrupo = g.Key.NombreGrupo,
                //    EsVigente = g.Key.EsVigente,
                //    Tipo = g.Key.Tipo,
                //    Clasificacion = g.Key.Clasificacion,
                //    DocumentoResponsable = g.Key.DocumentoResponsable,
                //    NombreResponsable = g.Key.NombreGrupo,
                //    Clientes = g.Select(c => new ItemGenerico
                //    {
                //        Id = c.id_actor_negocio_vinculado,
                //        Codigo = c.Actor_negocio1.Actor.Detalle_maestro.valor + " - " + c.Actor_negocio1.Actor.numero_documento_identidad,
                //        Nombre = c.Actor_negocio1.Actor.primer_nombre.Replace("|", " ")
                //    }).ToList()
                //}).ToList();
                //var vinculosActorNegocio = _db.Vinculo_Actor_Negocio.Where(van => van.tipo_vinculo == enumTipoVinculo).Select(van =>
                //new
                //{
                //    Id = van.id_actor_negocio_principal,
                //    CodigoGrupo = van.Actor_negocio.Actor.numero_documento_identidad,
                //    NombreGrupo = van.Actor_negocio.Actor.primer_nombre,
                //    EsVigente = van.es_vigente,
                //    Tipo = van.Detalle_maestro.nombre,
                //    Clasificacion = van.Detalle_maestro1.nombre,
                //    DocumentoResponsable = van.Actor_negocio2.Actor.Detalle_maestro.valor + " - " + van.Actor_negocio2.Actor.numero_documento_identidad,
                //    NombreResponsable = van.Actor_negocio2.Actor.primer_nombre.Replace("|", " "),
                //    IdCliente = van.id_actor_negocio_vinculado,
                //    DocumentoCliente = van.Actor_negocio1.Actor.Detalle_maestro.valor + " - " + van.Actor_negocio1.Actor.numero_documento_identidad,
                //    NombreCliente = van.Actor_negocio1.Actor.primer_nombre.Replace("|", " "),
                //});
                //var resultado = vinculosActorNegocio.GroupBy(van => new
                //{
                //    van.Id,
                //    van.CodigoGrupo,
                //    van.NombreGrupo,
                //    van.EsVigente,
                //    van.Tipo,
                //    van.Clasificacion,
                //    van.DocumentoResponsable,
                //    van.NombreResponsable
                //}).Select(g => new GrupoClientesResumen
                //{
                //    Id = g.Key.Id,
                //    CodigoGrupo = g.Key.CodigoGrupo,
                //    NombreGrupo = g.Key.NombreGrupo,
                //    EsVigente = g.Key.EsVigente,
                //    Tipo = g.Key.Tipo,
                //    Clasificacion = g.Key.Clasificacion,
                //    DocumentoResponsable = g.Key.DocumentoResponsable,
                //    NombreResponsable = g.Key.NombreGrupo,
                //    Clientes = g.Where(gg => gg.Id == g.Key.Id).Select(c => new ItemGenerico {
                //        Id = c.Id,
                //        Codigo = c.DocumentoCliente,
                //        Nombre = c.NombreCliente
                //    }).ToList()
                //}).ToList();
                return resultado;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener grupos de clientes", e);
            }
        }

        public GrupoClientes ObtenerGrupoClientes(int enumTipoVinculo, int idGrupoCliente)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                var resultado = _db.Actor_negocio.Where(an => an.id_rol == ActorSettings.Default.IdRolGrupoClientes && an.id == idGrupoCliente).Select(an => new GrupoClientes
                {
                    Id = an.id,
                    IdActor = an.id_actor,
                    Codigo = an.Actor.numero_documento_identidad,
                    Nombre = an.Actor.primer_nombre,
                    Tipo = new ItemGenericoBase
                    {
                        Id = (int)an.Actor.id_detalle_multiproposito,
                        Nombre = an.Actor.Detalle_maestro1.nombre,
                    },
                    Clasificacion = new ItemGenericoBase
                    {
                        Id = (int)an.Actor.id_detalle_multiproposito1,
                        Nombre = an.Actor.Detalle_maestro2.nombre,
                    },
                    Responsable = new ActorComercial_
                    {
                        Id = (int)an.Vinculo_Actor_Negocio1.FirstOrDefault().id_actor_negocio_principal
                    },
                    Clientes = an.Vinculo_Actor_Negocio.Where(van => van.es_vigente).Select(van => new MiembroGrupoClientes
                    {
                        Id = van.id_actor_negocio_vinculado,
                        Documento = van.Actor_negocio1.Actor.Detalle_maestro.valor + " - " + van.Actor_negocio1.Actor.numero_documento_identidad,
                        Nombre = van.Actor_negocio1.Actor.primer_nombre.Replace("|", " "),
                        EsVigente = van.es_vigente
                    }).ToList()
                }).SingleOrDefault();
                //var resultado = _db.Vinculo_Actor_Negocio.Where(van => van.tipo_vinculo == enumTipoVinculo && van.id_actor_negocio_principal == idGrupoCliente && van.es_vigente).GroupBy(van =>
                //new
                //{
                //    Id = van.id_actor_negocio_principal,
                //    IdActor = van.Actor_negocio.id_actor,
                //    CodigoGrupo = van.Actor_negocio.Actor.numero_documento_identidad,
                //    NombreGrupo = van.Actor_negocio.Actor.primer_nombre,
                //    EsVigente = van.es_vigente,
                //    IdTipo = (int)van.id_detalle_maestro1,
                //    Tipo = van.Detalle_maestro.nombre,
                //    IdClasificacion = (int)van.id_detalle_maestro2,
                //    Clasificacion = van.Detalle_maestro1.nombre,
                //    IdResponsable = van.Actor_negocio2.id,
                //}).Select(g => new GrupoClientes
                //{
                //    Id = g.Key.Id,
                //    IdActor = g.Key.IdActor,
                //    Codigo = g.Key.CodigoGrupo,
                //    Nombre = g.Key.NombreGrupo,
                //    Tipo = new ItemGenerico
                //    {
                //        Id = g.Key.IdTipo,
                //        Nombre = g.Key.Tipo
                //    },
                //    Clasificacion = new ItemGenerico
                //    {
                //        Id = g.Key.IdClasificacion,
                //        Nombre = g.Key.Clasificacion
                //    },
                //    Responsable = new ActorComercial_
                //    {
                //        Id = g.Key.IdResponsable
                //    },
                //    Clientes = g.Select(c => new ItemGenerico
                //    {
                //        Id = c.id_actor_negocio_vinculado,
                //        Codigo = c.Actor_negocio1.Actor.Detalle_maestro.valor + " - " + c.Actor_negocio1.Actor.numero_documento_identidad,
                //        Nombre = c.Actor_negocio1.Actor.primer_nombre.Replace("|", " ")
                //    }).ToList()
                //}).SingleOrDefault();
                return resultado;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener un grupo de clientes", e);
            }
        }
        public IEnumerable<ItemGenerico> ObtenerGruposActoresComerciales(int[] idsRolesGrupos)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                var resultado = _db.Actor_negocio.Where(an => idsRolesGrupos.Contains(an.id_rol) && an.es_vigente).Select(an => new ItemGenerico
                {
                    Id = an.id,
                    Nombre = an.Actor.numero_documento_identidad + " - " + an.Actor.primer_nombre
                }).ToList();
                return resultado;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener grupos de clientes", e);
            }
        }

        public IEnumerable<ItemGenerico> ObtenerGruposActoresComercialesPorRol(int idRolGrupo)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                var resultado = _db.Actor_negocio.Where(an => an.id_rol == idRolGrupo && an.es_vigente).Select(an => new ItemGenerico
                {
                    Id = an.id,
                    Nombre = an.Actor.numero_documento_identidad + " - " + an.Actor.primer_nombre
                }).ToList();
                return resultado;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener grupos de clientes", e);
            }
        }

        public IEnumerable<ItemGenerico> ObtenerActoresComercialesDeGrupoActoresComercialesPorRol(int enumTipoVinculo, int idRolGrupo, int idGrupoActoresComerciales)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                var resultado = _db.Vinculo_Actor_Negocio.Where(van => van.id_actor_negocio_principal == idGrupoActoresComerciales && enumTipoVinculo == van.tipo_vinculo && van.es_vigente && van.Actor_negocio.id_rol == idRolGrupo).Select(van => new ItemGenerico
                {
                    Id = van.id_actor_negocio_vinculado,
                    Nombre = van.Actor_negocio1.Actor.numero_documento_identidad + " - " + van.Actor_negocio1.Actor.primer_nombre.Replace("|", " ")
                }).ToList();
                return resultado;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener grupos de clientes", e);
            }
        }

        public IEnumerable<ItemGenerico> ObtenerGruposActoresComercialesPorRolDeActorComercial(int enumTipoVinculo, int idRolGrupo, int idActorComercial)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                var resultado = _db.Vinculo_Actor_Negocio.Where(van => van.id_actor_negocio_vinculado == idActorComercial && enumTipoVinculo == van.tipo_vinculo && van.es_vigente && van.Actor_negocio.id_rol == idRolGrupo && van.Actor_negocio.es_vigente).Select(van => new ItemGenerico
                {
                    Id = van.id_actor_negocio_principal,
                    Nombre = van.Actor_negocio.Actor.numero_documento_identidad + " - " + van.Actor_negocio.Actor.primer_nombre
                }).ToList();
                return resultado;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener grupos de clientes", e);
            }
        }
    }
}
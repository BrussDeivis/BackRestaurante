using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Custom;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Actores;

namespace Tsp.Sigescom.AccesoDatos.Sigescom.Actores
{
    public class ConsultaActor_Datos : IConsultaActor_Repositorio
    {
        public ActorComercial_ ObtenerActorComercial_(int idRol, int idActorComercial)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                var actorComercial = _db.Actor_negocio.Where(an => an.id_rol == idRol && an.id == idActorComercial).
                    Select(actorDeNegocio => new ActorComercial_
                    {
                        Id = actorDeNegocio.id,
                        IdActor = actorDeNegocio.id_actor,
                        Codigo = actorDeNegocio.codigo_negocio,
                        Telefono = actorDeNegocio.Actor.telefono,
                        Correo = actorDeNegocio.Actor.correo,
                        NombreComercial = actorDeNegocio.Actor.segundo_nombre,
                        NombreORazonSocial = actorDeNegocio.Actor.primer_nombre,
                        NumeroDocumentoIdentidad = actorDeNegocio.Actor.numero_documento_identidad,
                        TipoDocumentoIdentidad = new ItemGenerico
                        {
                            Id = actorDeNegocio.Actor.id_documento_identidad,
                            Nombre = actorDeNegocio.Actor.Detalle_maestro.valor,
                            Valor = actorDeNegocio.Actor.Detalle_maestro.valor
                        },
                        TipoPersona = new ItemGenerico
                        {
                            Id = actorDeNegocio.Actor.id_tipo_actor,
                            Nombre = actorDeNegocio.Actor.Tipo_actor.nombre,
                            Valor = ""
                        },
                        ClaseActor = new ItemGenerico
                        {
                            Id = actorDeNegocio.Actor.id_clase_actor,
                            Nombre = actorDeNegocio.Actor.Clase_actor.nombre,
                            Valor = ""
                        },
                        EstadoLegal = new ItemGenerico
                        {
                            Id = actorDeNegocio.Actor.id_estado_legal,
                            Nombre = actorDeNegocio.Actor.Estado_legal.nombre,
                            Valor = ""
                        },
                        FechaNacimiento = actorDeNegocio.Actor.fecha_nacimiento,
                        Nacionalidad = new ItemGenerico
                        {
                            Id = actorDeNegocio.Actor.Detalle_maestro1.id,
                            Nombre = actorDeNegocio.Actor.Detalle_maestro1.nombre,
                            Valor = ""
                        },
                        DomicilioFiscal = new Direccion_
                        {
                            Id = actorDeNegocio.Actor.Direccion.FirstOrDefault(d => d.es_principal).id,
                            Pais = new ItemGenerico
                            {
                                Id = actorDeNegocio.Actor.Direccion.FirstOrDefault().Detalle_maestro1.id,
                                Nombre = actorDeNegocio.Actor.Direccion.FirstOrDefault().Detalle_maestro1.nombre,
                                Valor = ""
                            },
                            Ubigeo = new ItemGenerico
                            {
                                Id = actorDeNegocio.Actor.Direccion.FirstOrDefault(d => d.es_principal).Ubigeo.id,
                                Nombre = actorDeNegocio.Actor.Direccion.FirstOrDefault(d => d.es_principal).Ubigeo.descripcion_larga,
                                Valor = ""
                            },
                            Detalle = actorDeNegocio.Actor.Direccion.FirstOrDefault(d => d.es_principal).detalle
                        }
                    }).FirstOrDefault();
                if (actorComercial != null)
                {
                    actorComercial.ApellidoPaterno = actorComercial.TipoPersona.Id == ActorSettings.Default.IdTipoActorPersonaNatural ? actorComercial.NombreORazonSocial.Split('|')[0] : "";
                    actorComercial.ApellidoMaterno = actorComercial.TipoPersona.Id == ActorSettings.Default.IdTipoActorPersonaNatural && actorComercial.NombreORazonSocial.Split('|').Count() > 1 ? actorComercial.NombreORazonSocial.Split('|')[1] : ".";
                    actorComercial.Nombres = actorComercial.TipoPersona.Id == ActorSettings.Default.IdTipoActorPersonaNatural && actorComercial.NombreORazonSocial.Split('|').Count() > 2 ? actorComercial.NombreORazonSocial.Split('|')[2] : "";
                    actorComercial.NombreORazonSocial = actorComercial.NombreORazonSocial.Replace("|", " ");
                    if (idRol == ActorSettings.Default.IdRolEmpleado)
                    {
                        actorComercial.Roles = new List<ItemGenerico>();
                        foreach (var rol in _db.Actor.Where(a => a.id == actorComercial.IdActor).SelectMany(a => a.Actor_negocio).Where(an => an.Rol.id_rol_padre == ActorSettings.Default.IdRolEmpleado && an.es_vigente == true).Select(an => an.Rol).ToList())
                        {
                            actorComercial.Roles.Add(new ItemGenerico(rol.id, rol.nombre));
                        }
                    }
                }
                return actorComercial;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener actor comercial", e);
            }
        }

        public ActorComercial_ ObtenerActorComercial_(int idRol, string numeroDocumento)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                var actorComercial = _db.Actor_negocio.Where(an => an.id_rol == idRol && an.es_vigente && an.Actor.numero_documento_identidad == numeroDocumento).
                    Select(actorDeNegocio => new ActorComercial_
                    {
                        Id = actorDeNegocio.id,
                        IdActor = actorDeNegocio.id_actor,
                        Codigo = actorDeNegocio.codigo_negocio,
                        Telefono = actorDeNegocio.Actor.telefono,
                        Correo = actorDeNegocio.Actor.correo,
                        NombreComercial = actorDeNegocio.Actor.segundo_nombre,
                        NombreORazonSocial = actorDeNegocio.Actor.primer_nombre,
                        NumeroDocumentoIdentidad = actorDeNegocio.Actor.numero_documento_identidad,
                        TipoDocumentoIdentidad = new ItemGenerico
                        {
                            Id = actorDeNegocio.Actor.id_documento_identidad,
                            Nombre = actorDeNegocio.Actor.Detalle_maestro.valor,
                            Valor = actorDeNegocio.Actor.Detalle_maestro.valor
                        },
                        TipoPersona = new ItemGenerico
                        {
                            Id = actorDeNegocio.Actor.id_tipo_actor,
                            Nombre = actorDeNegocio.Actor.Tipo_actor.nombre,
                            Valor = ""
                        },
                        ClaseActor = new ItemGenerico
                        {
                            Id = actorDeNegocio.Actor.id_clase_actor,
                            Nombre = actorDeNegocio.Actor.Clase_actor.nombre,
                            Valor = ""
                        },
                        EstadoLegal = new ItemGenerico
                        {
                            Id = actorDeNegocio.Actor.id_estado_legal,
                            Nombre = actorDeNegocio.Actor.Estado_legal.nombre,
                            Valor = ""
                        },
                        FechaNacimiento = actorDeNegocio.Actor.fecha_nacimiento,
                        Nacionalidad = new ItemGenerico
                        {
                            Id = actorDeNegocio.Actor.Detalle_maestro1.id,
                            Nombre = actorDeNegocio.Actor.Detalle_maestro1.nombre,
                            Valor = ""
                        },
                        DomicilioFiscal = new Direccion_
                        {
                            Id = actorDeNegocio.Actor.Direccion.FirstOrDefault(d => d.es_principal).id,
                            Pais = new ItemGenerico
                            {
                                Id = actorDeNegocio.Actor.Direccion.FirstOrDefault().Detalle_maestro1.id,
                                Nombre = actorDeNegocio.Actor.Direccion.FirstOrDefault().Detalle_maestro1.nombre,
                                Valor = ""
                            },
                            Ubigeo = new ItemGenerico
                            {
                                Id = actorDeNegocio.Actor.Direccion.FirstOrDefault(d => d.es_principal).Ubigeo.id,
                                Nombre = actorDeNegocio.Actor.Direccion.FirstOrDefault(d => d.es_principal).Ubigeo.descripcion_larga,
                                Valor = ""
                            },
                            Detalle = actorDeNegocio.Actor.Direccion.FirstOrDefault(d => d.es_principal).detalle
                        }
                    }).FirstOrDefault();
                if (actorComercial != null)
                {
                    actorComercial.ApellidoPaterno = actorComercial.TipoPersona.Id == ActorSettings.Default.IdTipoActorPersonaNatural ? actorComercial.NombreORazonSocial.Split('|')[0] : "";
                    actorComercial.ApellidoMaterno = actorComercial.TipoPersona.Id == ActorSettings.Default.IdTipoActorPersonaNatural && actorComercial.NombreORazonSocial.Split('|').Count() > 1 ? actorComercial.NombreORazonSocial.Split('|')[1] : ".";
                    actorComercial.Nombres = actorComercial.TipoPersona.Id == ActorSettings.Default.IdTipoActorPersonaNatural && actorComercial.NombreORazonSocial.Split('|').Count() > 2 ? actorComercial.NombreORazonSocial.Split('|')[2] : "";
                    actorComercial.NombreORazonSocial = actorComercial.NombreORazonSocial.Replace("|", " ");
                    if (idRol == ActorSettings.Default.IdRolEmpleado)
                    {
                        actorComercial.Roles = new List<ItemGenerico>();
                        foreach (var rol in _db.Actor_negocio.Where(an => an.Rol.id_rol_padre == ActorSettings.Default.IdRolEmpleado && an.es_vigente == true).Select(an => an.Rol).ToList())
                        {
                            actorComercial.Roles.Add(new ItemGenerico(rol.id, rol.nombre));
                        }
                    }
                }
                return actorComercial;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener actor comercial", e);
            }
        }

        public ActorComercial_ ObtenerActorComercial_(int idRol, int idTipoDocumento, string numeroDocumento)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                var actorComercial = _db.Actor_negocio.Where(an => an.id_rol == idRol && an.es_vigente && an.Actor.id_documento_identidad == idTipoDocumento && an.Actor.numero_documento_identidad == numeroDocumento).
                    Select(actorDeNegocio => new ActorComercial_
                    {
                        Id = actorDeNegocio.id,
                        IdActor = actorDeNegocio.id_actor,
                        Codigo = actorDeNegocio.codigo_negocio,
                        Telefono = actorDeNegocio.Actor.telefono,
                        Correo = actorDeNegocio.Actor.correo,
                        NombreComercial = actorDeNegocio.Actor.segundo_nombre,
                        NombreORazonSocial = actorDeNegocio.Actor.primer_nombre,
                        NumeroDocumentoIdentidad = actorDeNegocio.Actor.numero_documento_identidad,
                        TipoDocumentoIdentidad = new ItemGenerico
                        {
                            Id = actorDeNegocio.Actor.id_documento_identidad,
                            Nombre = actorDeNegocio.Actor.Detalle_maestro.valor,
                            Valor = actorDeNegocio.Actor.Detalle_maestro.valor
                        },
                        TipoPersona = new ItemGenerico
                        {
                            Id = actorDeNegocio.Actor.id_tipo_actor,
                            Nombre = actorDeNegocio.Actor.Tipo_actor.nombre,
                            Valor = ""
                        },
                        ClaseActor = new ItemGenerico
                        {
                            Id = actorDeNegocio.Actor.id_clase_actor,
                            Nombre = actorDeNegocio.Actor.Clase_actor.nombre,
                            Valor = ""
                        },
                        EstadoLegal = new ItemGenerico
                        {
                            Id = actorDeNegocio.Actor.id_estado_legal,
                            Nombre = actorDeNegocio.Actor.Estado_legal.nombre,
                            Valor = ""
                        },
                        FechaNacimiento = actorDeNegocio.Actor.fecha_nacimiento,
                        Nacionalidad = new ItemGenerico
                        {
                            Id = actorDeNegocio.Actor.Detalle_maestro1.id,
                            Nombre = actorDeNegocio.Actor.Detalle_maestro1.nombre,
                            Valor = ""
                        },
                        DomicilioFiscal = new Direccion_
                        {
                            Id = actorDeNegocio.Actor.Direccion.FirstOrDefault(d => d.es_principal).id,
                            Pais = new ItemGenerico
                            {
                                Id = actorDeNegocio.Actor.Direccion.FirstOrDefault().Detalle_maestro1.id,
                                Nombre = actorDeNegocio.Actor.Direccion.FirstOrDefault().Detalle_maestro1.nombre,
                                Valor = ""
                            },
                            Ubigeo = new ItemGenerico
                            {
                                Id = actorDeNegocio.Actor.Direccion.FirstOrDefault(d => d.es_principal).Ubigeo.id,
                                Nombre = actorDeNegocio.Actor.Direccion.FirstOrDefault(d => d.es_principal).Ubigeo.descripcion_larga,
                                Valor = ""
                            },
                            Detalle = actorDeNegocio.Actor.Direccion.FirstOrDefault(d => d.es_principal).detalle
                        }
                    }).FirstOrDefault();
                if (actorComercial != null)
                {
                    actorComercial.ApellidoPaterno = actorComercial.TipoPersona.Id == ActorSettings.Default.IdTipoActorPersonaNatural ? actorComercial.NombreORazonSocial.Split('|')[0] : "";
                    actorComercial.ApellidoMaterno = actorComercial.TipoPersona.Id == ActorSettings.Default.IdTipoActorPersonaNatural && actorComercial.NombreORazonSocial.Split('|').Count() > 1 ? actorComercial.NombreORazonSocial.Split('|')[1] : ".";
                    actorComercial.Nombres = actorComercial.TipoPersona.Id == ActorSettings.Default.IdTipoActorPersonaNatural && actorComercial.NombreORazonSocial.Split('|').Count() > 2 ? actorComercial.NombreORazonSocial.Split('|')[2] : "";
                    actorComercial.NombreORazonSocial = actorComercial.NombreORazonSocial.Replace("|", " ");
                    if (idRol == ActorSettings.Default.IdRolEmpleado)
                    {
                        actorComercial.Roles = new List<ItemGenerico>();
                        foreach (var rol in _db.Actor_negocio.Where(an => an.Rol.id_rol_padre == ActorSettings.Default.IdRolEmpleado && an.es_vigente == true).Select(an => an.Rol).ToList())
                        {
                            actorComercial.Roles.Add(new ItemGenerico(rol.id, rol.nombre));
                        }
                    }
                }
                return actorComercial;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener actor comercial", e);
            }
        }

    }
}

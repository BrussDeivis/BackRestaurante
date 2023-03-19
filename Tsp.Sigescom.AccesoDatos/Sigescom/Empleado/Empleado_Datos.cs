using Tsp.Sigescom.Modelo.Custom;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Empleado;
using System.Linq;
using System.Data.Entity;

using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Config;
using System;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;

namespace Tsp.Sigescom.AccesoDatos.Empleado
{
    public partial class Empleado_Datos: IEmpleado_Repositorio
    {
        public Empleado_ ObtenerEmpleado(string idUsuario)
        {
            SigescomEntities _db = new SigescomEntities();
            var empleado = _db.Actor_negocio.Where(an => an.id_rol == ActorSettings.Default.IdRolEmpleado && an.id_usuario == idUsuario).ToList().Select(an => new Empleado_
            {
                Id = an.id,
                IdActor = an.id_actor,
                Nombres = an.Actor.primer_nombre.Split('|')[2],
                ApellidoPaterno = an.Actor.primer_nombre.Split('|')[0],
                ApellidoMaterno = an.Actor.primer_nombre.Split('|')[1],
                NombreCorto = an.Actor.tercer_nombre,
                NumeroDocumentoIdentidad = an.Actor.numero_documento_identidad,
                TipoDocumentoIdentidad = new ItemGenerico { Id = an.Actor.id_documento_identidad, Nombre = an.Actor.Detalle_maestro.nombre },
                Roles = an.Actor.Actor_negocio.Where(_an => _an.es_vigente).Select(__an => new RolDeNegocio_
                {
                    Id = __an.Rol.id,
                    Nombre = __an.Rol.nombre
                })
            }).FirstOrDefault();
            return empleado;
        }
        public Empleado_ ObtenerEmpleado(int id)
        {
            SigescomEntities _db = new SigescomEntities();

            var empleado = _db.Actor_negocio.Include(an => an.Actor).Include(an => an.Actor.Detalle_maestro).Where(an => an.id == id && an.id_rol == ActorSettings.Default.IdRolEmpleado).ToList().Select(an => new Empleado_
            {
                Id = an.id,
                IdActor = an.id_actor,
                Nombres = an.Actor.primer_nombre.Split('|')[2],
                ApellidoPaterno = an.Actor.primer_nombre.Split('|')[0],
                ApellidoMaterno = an.Actor.primer_nombre.Split('|')[1],
                NombreCorto = an.Actor.tercer_nombre,
                NumeroDocumentoIdentidad = an.Actor.numero_documento_identidad,
                TipoDocumentoIdentidad = new ItemGenerico { Id = an.Actor.id_documento_identidad, Nombre = an.Actor.Detalle_maestro.nombre },
                Roles = an.Actor.Actor_negocio.Where(_an => _an.es_vigente).Select(__an => new RolDeNegocio_
                {
                    Id = __an.Rol.id,
                    Nombre = __an.Rol.nombre
                })
            }).FirstOrDefault();
            return empleado;
        }

        public int ObtenerId(string idUsuario)
        {
            SigescomEntities _db = new SigescomEntities();

            try
            {
                return _db.Actor_negocio.SingleOrDefault(an => an.id_usuario == idUsuario && an.es_vigente).id;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener el id de actor de negocio para el id de usuario: " + idUsuario, e);
            }
        }

        public string ObtenerNombre(string idUsuario)
        {
            SigescomEntities _db = new SigescomEntities();

            try
            {
                var usuario = _db.Actor_negocio.SingleOrDefault(an => an.id_usuario == idUsuario && an.es_vigente);
                return usuario.PrimerNombre + " " + usuario.SegundoNombre + " " + usuario.TercerNombre;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener el nombre de actor de negocio para el id de usuario: " + idUsuario, e);
            }
        }
    }
}
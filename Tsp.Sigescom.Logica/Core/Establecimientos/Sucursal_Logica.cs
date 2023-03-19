using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Actores;
using Tsp.Sigescom.Modelo.Interfaces.Datos.CentrosDeAtencion;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Establecimientos;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Negocio.Establecimientos;

namespace Tsp.Sigescom.Logica.Core.Establecimientos
{
    public class Sucursal_Logica : ISucursal_Logica
    {
        private readonly IEstablecimiento_Repositorio _establecimientoRepositorio;
        private readonly IActor_Repositorio _actorRepositorio;



        public Sucursal_Logica(IEstablecimiento_Repositorio establecimientoRepositorio, IActor_Repositorio actorRepositorio)
        {
            _establecimientoRepositorio = establecimientoRepositorio;
            _actorRepositorio = actorRepositorio;
        }


        public Actor CrearActorParaSucursal(int idActor, string informacionPublicitaria, string nombre, string nombreInterno, string telefono, string correo, Direccion direccion, byte[] foto)
        {
            telefono = telefono ?? "";
            correo = correo ?? "";
            EstablecimientoComercial sede = _establecimientoRepositorio.ObtenerEstablecimientoComercial(ActorSettings.Default.IdActorNegocioSede);
            string numeroDocumentoIdentidad = sede.DocumentoIdentidad;
            int idTipoPersona = sede.IdTipoPersona;
            int idEstadoLegalActor = sede.IdEstadoLegal;
            int idClaseActor = sede.IdClaseActor;
            Actor actor;
            if (idActor == 0)
            {
                actor = new Actor()
                {
                    id_documento_identidad = ActorSettings.Default.IdTipoDocumentoIdentidadRuc,
                    fecha_nacimiento = DateTimeUtil.FechaActual(),
                    numero_documento_identidad = numeroDocumentoIdentidad,
                    primer_nombre = nombre,
                    segundo_nombre = "",
                    telefono = telefono,
                    id_tipo_actor = idTipoPersona,
                    id_clase_actor = idClaseActor,
                    id_estado_legal = idEstadoLegalActor,
                    correo = correo,
                    tercer_nombre = nombreInterno,// Es el nombre con el que se conoce al establecimiento comercial, Ejm: Razon social : Comercial ABC EIRL, Nombre comercial: Comercial ABC, Nombre Interno: Tienda Principal Tingo
                    pagina_web = "",
                    informacion_multiproposito = informacionPublicitaria ?? ""
                };
            }
            else
            {
                actor = new Actor()
                {
                    id = idActor,
                    id_documento_identidad = ActorSettings.Default.IdTipoDocumentoIdentidadRuc,
                    fecha_nacimiento = DateTimeUtil.FechaActual(),
                    numero_documento_identidad = numeroDocumentoIdentidad,
                    primer_nombre = nombre,
                    segundo_nombre = "",
                    telefono = telefono,
                    id_tipo_actor = idTipoPersona,
                    id_clase_actor = idClaseActor,
                    id_estado_legal = idEstadoLegalActor,
                    correo = correo,
                    tercer_nombre = nombreInterno,// Es el nombre con el que se conoce al establecimiento comercial, Ejm: Razon social : Comercial ABC EIRL, Nombre comercial: Comercial ABC, Nombre Interno: Tienda Principal Tingo
                    pagina_web = "",
                    informacion_multiproposito = informacionPublicitaria ?? ""
                };
            }
            if (foto != null)
            {
                actor.Foto = CrearFoto(foto);
            }
            else
            {
                actor.id_foto = ActorSettings.Default.IdFotoActorPorDefecto;
            }
            if (direccion != null)
            {
                actor.Direccion = CrearDirecciones(direccion);
            }
            return actor;
        }

        public OperationResult CrearSucursal(string codigoEstablecimiento, string codigoEstablecimientoDigemid, string informacionPublicitaria, string nombre, string nombreInterno, string telefono, string correo, Direccion direccion, byte[] foto)
        {
            try
            {
                codigoEstablecimientoDigemid = ActorSettings.Default.PermitirRegistroCodigoDigemidEnEstableciemientoComercial ? codigoEstablecimientoDigemid : null;
                codigoEstablecimiento = codigoEstablecimiento ?? "";
                DateTime fechaActual = DateTimeUtil.FechaActual();
                DateTime fechaFin = fechaActual.AddYears(ActorSettings.Default.vigenciaEnAnyosPorDefectoDeActorDeNegocioEntidadInterna);
                Actor_negocio sucursal = new Actor_negocio(ActorSettings.Default.IdRolSucursal, fechaActual, fechaFin, codigoEstablecimiento, true, false, "")
                {
                    Actor = CrearActorParaSucursal(0, informacionPublicitaria, nombre, nombreInterno, telefono, correo, direccion, foto)
                };
                sucursal.extension_json = codigoEstablecimientoDigemid == null ? "" : "{ codigodigemid: \"" + codigoEstablecimientoDigemid + "\" }";
                return _actorRepositorio.CrearActorNegocio(sucursal);
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }

        public OperationResult ActualizarSucursal(int idActor, int idSucursal, string codigoEstablecimiento, string codigoEstablecimientoDigemid, string informacionPublicitaria, string nombre, string nombreInterno, string telefono, string correo, Direccion direccion, byte[] foto)
        {
            try

            {
                codigoEstablecimientoDigemid = ActorSettings.Default.PermitirRegistroCodigoDigemidEnEstableciemientoComercial ? codigoEstablecimientoDigemid : null;
                DateTime fechaActual = DateTimeUtil.FechaActual();
                DateTime fechaFin = fechaActual.AddYears(ActorSettings.Default.vigenciaEnAnyosPorDefectoDeActorDeNegocioEntidadInterna);
                Actor_negocio sucursal = new Actor_negocio(idSucursal, idActor, ActorSettings.Default.IdRolSucursal, fechaActual, fechaFin, codigoEstablecimiento, true, false, "")
                {
                    Actor = CrearActorParaSucursal(idActor, informacionPublicitaria, nombre, nombreInterno, telefono, correo, direccion, foto)
                };
                sucursal.extension_json = codigoEstablecimientoDigemid == null ? "" : "{ codigodigemid: \"" + codigoEstablecimientoDigemid + "\" }";
                return _actorRepositorio.ActualizarActorNegocioSinTomarEnCuentaAParametros(sucursal);
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }

        public List<Sucursal> ObtenerSucursalesVigentes()
        {
            try
            {
                return Sucursal.Convert(_actorRepositorio.ObtenerActorDeNegocioPorRolVigentesAhora(ActorSettings.Default.IdRolSucursal).ToList());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public EstablecimientoComercial ObtenerSucursalComoEstablecimiento(int idSucursal)
        {
            try
            {
                return _establecimientoRepositorio.ObtenerEstablecimientoComercial(idSucursal);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public EstablecimientoComercial ObtenerSucursal(int idSucursal)
        {
            try
            {
                return _establecimientoRepositorio.ObtenerEstablecimientoComercial(idSucursal);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public OperationResult DarDeBajaSucursal(int idSucursal)
        {
            try
            {
                return _actorRepositorio.DarDeBajaActorNegocioAhora(idSucursal, ActorSettings.Default.IdRolSucursal);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Direccion> CrearDirecciones(Direccion direccion)
        {
            List<Direccion> direcciones = new List<Direccion>();
            direcciones.Add(direccion);
            return direcciones;

        }

        public Foto CrearFoto(byte[] foto)
        {
            Foto fotografia = new Foto();
            fotografia.imagen = foto;
            return fotografia;
        }

    }
}

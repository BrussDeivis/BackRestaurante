using System;
using System.Collections.Generic;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Actores;
using Tsp.Sigescom.Modelo.Interfaces.Datos.CentrosDeAtencion;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Establecimientos;
using Tsp.Sigescom.Modelo.Negocio.Actores;
using Tsp.Sigescom.Modelo.Negocio.Establecimientos;

namespace Tsp.Sigescom.Logica.Core.Establecimientos
{
    public class Sede_Logica : ISede_Logica
    {
        private readonly IEstablecimiento_Repositorio _establecimientoRepositorio;
        private readonly ISede_Repositorio _sedeRepositorio;
        private readonly IActor_Repositorio _actorRepositorio;
        private readonly ICentroDeAtencion_Repositorio _centroAtencionRepositorio;
        private readonly IValidacionActorNegocio_Logica _validacionActorNegocioLogica;


        public Sede_Logica(IEstablecimiento_Repositorio establecimientoRepositorio, ISede_Repositorio sedeRepositorio, IActor_Repositorio actorRepositorio, ICentroDeAtencion_Repositorio centroAtencionRepositorio, IValidacionActorNegocio_Logica validacionActorNegocioLogica)
        {
            _establecimientoRepositorio = establecimientoRepositorio;
            _sedeRepositorio = sedeRepositorio;
            _actorRepositorio = actorRepositorio;
            _centroAtencionRepositorio = centroAtencionRepositorio;
            _validacionActorNegocioLogica = validacionActorNegocioLogica;
        }


        public Actor CrearActorParaSede(int idActor, string numeroDocumentoIdentidad, string codigoEstablecimiento, string informacionPublicitaria, int idTipoPersona, int idClaseActor, string razonSocial, string nombreComercial, string nombreInterno, string telefono, string correo, Direccion direccion, byte[] foto)
        {
            if (numeroDocumentoIdentidad.Trim().Length != 11) throw new Exception("El numero de documento de identidad debe de tener 11 digitos");
            telefono = telefono ?? "";
            correo = correo ?? "";
            int idEstadoLegalActor;
            if (idTipoPersona == ActorSettings.Default.IdTipoActorPersonaNatural)
            {
                idClaseActor = idClaseActor != 0 ? idClaseActor : ActorSettings.Default.IdClaseActorPersonaNaturalPorDefecto;
                idEstadoLegalActor = ActorSettings.Default.IdEstadoLegalActorPersonaNaturalPorDefecto;
            }
            else if (idTipoPersona == ActorSettings.Default.IdTipoActorPersonaJuridica)
            {
                idClaseActor = idClaseActor != 0 ? idClaseActor : ActorSettings.Default.IdClaseActorPersonaJuridicaPorDefecto;
                idEstadoLegalActor = ActorSettings.Default.IdEstadoLegalActorPersonaJuridicaPorDefecto;
            }
            else
            {
                idClaseActor = idClaseActor != 0 ? idClaseActor : ActorSettings.Default.IdClaseActorEntidadInternaPorDefecto;
                idEstadoLegalActor = ActorSettings.Default.IdEstadoLegalActorEntidadInternaPorDefecto;
            }
            Actor actor;
            if (idActor == 0)
            {
                actor = new Actor()
                {
                    id_documento_identidad = ActorSettings.Default.IdTipoDocumentoIdentidadRuc,
                    fecha_nacimiento = DateTimeUtil.FechaActual(),
                    numero_documento_identidad = numeroDocumentoIdentidad,
                    primer_nombre = razonSocial,
                    segundo_nombre = nombreComercial,
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
                    primer_nombre = razonSocial,
                    segundo_nombre = nombreComercial,
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

        public OperationResult CrearSede(string numeroDocumentoIdentidad, string codigoEstablecimiento, string codigoEstablecimientoDigemid, string informacionPublicitaria, int idTipoPersona, int idClaseActor, string razonSocial, string nombreComercial, string nombreInterno, string telefono, string correo, Direccion direccion, byte[] foto)
        {
            try
            {
                codigoEstablecimientoDigemid = ActorSettings.Default.PermitirRegistroCodigoDigemidEnEstableciemientoComercial ? codigoEstablecimientoDigemid : null;
                codigoEstablecimiento = codigoEstablecimiento ?? "";
                //Validamos el documento de identidad
                _validacionActorNegocioLogica.ValidarExistenciaDeDocumento(ActorSettings.Default.IdTipoDocumentoIdentidadRuc, numeroDocumentoIdentidad);
                DateTime fechaInicio = DateTimeUtil.FechaActual();
                DateTime fechaFin = fechaInicio.AddYears(ActorSettings.Default.vigenciaEnAnyosPorDefectoDeActorDeNegocioEntidadInterna);
                Actor_negocio sede = new Actor_negocio(ActorSettings.Default.IdRolSede, fechaInicio, fechaFin, "", true, false, "")
                {
                    Actor = CrearActorParaSede(0, numeroDocumentoIdentidad, codigoEstablecimiento, informacionPublicitaria, idTipoPersona, idClaseActor, razonSocial, nombreComercial, nombreInterno, telefono, correo, direccion, foto)
                };
                sede.extension_json = codigoEstablecimientoDigemid == null ? null : "{ codigodigemid: \"" + codigoEstablecimientoDigemid + "\" }";
                return _actorRepositorio.CrearActorNegocio(sede);
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }

        public OperationResult ActualizarSede(int idActor, int idSede, string numeroDocumentoIdentidad, string codigoEstablecimiento, string codigoEstablecimientoDigemid, string informacionPublicitaria, int idTipoPersona, int idClaseActor, string razonSocial, string nombreComercial, string nombreInterno, string telefono, string correo, Direccion direccion, byte[] foto)
        {
            try
            {
                codigoEstablecimientoDigemid = ActorSettings.Default.PermitirRegistroCodigoDigemidEnEstableciemientoComercial ? codigoEstablecimientoDigemid : null;
                DateTime fechaInicio = DateTimeUtil.FechaActual();
                DateTime fechaFin = fechaInicio.AddYears(ActorSettings.Default.vigenciaEnAnyosPorDefectoDeActorDeNegocioEntidadInterna);
                Actor_negocio sede = new Actor_negocio(idSede, idActor, ActorSettings.Default.IdRolSede, fechaInicio, fechaFin, codigoEstablecimiento, true, false, "")
                {
                    Actor = CrearActorParaSede(idActor, numeroDocumentoIdentidad, codigoEstablecimiento, informacionPublicitaria, idTipoPersona, idClaseActor, razonSocial, nombreComercial, nombreInterno, telefono, correo, direccion, foto)
                };
                sede.extension_json = codigoEstablecimientoDigemid == null ? "" : "{ codigodigemid: \"" + codigoEstablecimientoDigemid + "\" }";
                var result = _actorRepositorio.ActualizarActorNegocioSinTomarEnCuentaAParametros(sede);
                if (result.code_result == OperationResultEnum.Success)
                {
                    _centroAtencionRepositorio.ActualizarDocumentoIdentidadDeTodosLosCentrosDeAtencionVigentes(sede.DocumentoIdentidad);
                }
                return result;
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }

        public EstablecimientoComercialExtendido ObtenerSedeExtendida()
        {
            try
            {
                return _establecimientoRepositorio.ObtenerEstablecimientoComercialExtendido(ActorSettings.Default.IdActorNegocioSede);
            }
            catch (Exception e) { throw; }
        }

        public EstablecimientoComercial ObtenerSede()
        {
            try
            {

                return _establecimientoRepositorio.ObtenerEstablecimientoComercial(ActorSettings.Default.IdActorNegocioSede);
            }
            catch (Exception e) { throw; }
        }

        public EstablecimientoComercialExtendidoConLogo ObtenerSedeConLogo()
        {
            try
            {
                return _establecimientoRepositorio.ObtenerEstablecimientoComercialExtendidoConLogo(ActorSettings.Default.IdActorNegocioSede);
            }
            catch (Exception e) { throw; }

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

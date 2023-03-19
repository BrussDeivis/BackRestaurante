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
    public partial class ActorNegocioLogica : IActorNegocioLogica
    {
        #region building
        private readonly IActorRepositorio _actorRepositorio;
        private readonly IActor_Repositorio _actor_Repositorio;

        private readonly ITransaccionRepositorio _transaccionRepositorio;
        private readonly IMaestroRepositorio _maestroRepositorio;
        private readonly ICentroDeAtencion_Repositorio _centroDeAtencionRepositorio;
        private readonly IRoles_Repositorio _rolesRepositorio;
          
        private readonly IInventarioActual_Logica _inventarioActual_Logica;
        private readonly IValidacionActorNegocio_Logica _validacionActorNegocio_Logica;
        private readonly IVinculoActor_Repositorio _vinculoActor_Repositorio;
        private readonly IConsultaActor_Repositorio _consultaActor_Repositorio;


        public ActorNegocioLogica(IActorRepositorio actorRepositorio, IConceptoRepositorio conceptoRepositorio, ITransaccionRepositorio transaccionRepositorio, IMaestroRepositorio maestroRepositorio, IInventarioActual_Logica inventarioActual_Logica, ICentroDeAtencion_Repositorio centroDeAtencionRepositorio, IRoles_Repositorio rolesRepositorio, IActor_Repositorio actor_Repositorio, IValidacionActorNegocio_Logica validacionActorNegocio_Logica, IVinculoActor_Repositorio vinculoActor_Repositorio, IConsultaActor_Repositorio consultaActor_Repositorio)
        {
            _actorRepositorio = actorRepositorio;
            _actor_Repositorio = actor_Repositorio;
            _transaccionRepositorio = transaccionRepositorio;
            _maestroRepositorio = maestroRepositorio;
            _inventarioActual_Logica = inventarioActual_Logica;
            _centroDeAtencionRepositorio = centroDeAtencionRepositorio;
            _rolesRepositorio = rolesRepositorio;
            _validacionActorNegocio_Logica = validacionActorNegocio_Logica;
            _vinculoActor_Repositorio = vinculoActor_Repositorio;
            _consultaActor_Repositorio = consultaActor_Repositorio;
        }
        public ActorNegocioLogica()
        {
        }
        #endregion

        #region Consultas
        public RespuestaVerificacionActorNegocio VerificarActor(int idTipoDocumento, string numeroDocumento, int idRol)
        {
            try
            {
                return _actorRepositorio.verificarActor(idTipoDocumento, numeroDocumento, idRol);
            }
            catch (Exception e) { throw e; }
        }
        public List<Tipo_actor> ObtenerTiposDeActor()
        {
            try { return _actorRepositorio.obtenerTiposDeActor().ToList(); }
            catch (Exception e) { throw e; }

        }
        public List<Clase_actor> ObtenerTiposDeClaseActor(int idTipoActor)
        {
            try { return _actorRepositorio.obtenerClasesDeActor(idTipoActor).ToList(); }
            catch (Exception e) { throw e; }
        }
        public List<Estado_legal> ObtenerTiposDeEstadoLegal(int idTipoActor)
        {
            try { return _actorRepositorio.obtenerEstadosLegales(idTipoActor).ToList(); }
            catch (Exception e) { throw e; }
        }
        public List<Clase_actor> ObtenerListaSexos()
        {
            int idTipoActorNatural = ActorSettings.Default.IdTipoActorPersonaNatural;
            try { return _actorRepositorio.obtenerClasesDeActor(idTipoActorNatural).ToList(); }
            catch (Exception e) { throw e; }
        }
        public List<Clase_actor> ObtenerListaTiposDeSociedad()
        {
            int idTipoActorJuridica = ActorSettings.Default.IdTipoActorPersonaJuridica;
            try { return _actorRepositorio.obtenerClasesDeActor(idTipoActorJuridica).ToList(); }
            catch (Exception e) { throw e; }
        }
        public List<Estado_legal> ObtenerListaEstadosCiviles()
        {
            int idTipoActorNatural = ActorSettings.Default.IdTipoActorPersonaNatural;
            try { return _actorRepositorio.obtenerEstadosLegales(idTipoActorNatural).ToList(); }
            catch (Exception e) { throw e; }
        }
        public string ObtenerDenominacionClase(int idTipoActor)
        {
            try
            {
                return (_actorRepositorio.obtenerDenominacionClase(idTipoActor));
            }
            catch (Exception e) { throw e; }
        }
        public string ObtenerProximoCodigo(int idRol)
        {
            try
            {
                string maximoCodigo = _actorRepositorio.obtenerMaximoCodigo(idRol);
                return (Convert.ToInt32(maximoCodigo) + 1).ToString();
            }
            catch (Exception e) { throw e; }
        }
        public ActorComercial ObtenerActorComercial(int id)
        {
            try
            {
                Actor_negocio actorNegocio = _actor_Repositorio.ObtenerActorDeNegocio(id);
                return new ActorComercial(actorNegocio);
            }
            catch (Exception e) { throw; }
        }
        public List<Rol> ObtenerRolesPersonal()
        {
            try
            {
                return _rolesRepositorio.ObtenerRolesHijos(ActorSettings.Default.IdRolEmpleado).ToList();
            }
            catch (Exception e) { throw e; }
        }
        public string ObtenerDireccionActorComercial(int id)
        {
            try
            {
                string direccion = "";
                ActorComercial actor = new ActorComercial(_actor_Repositorio.ObtenerActorDeNegocio(id));
                if (actor.DomicilioFiscal() != null)
                {
                    direccion = (actor.DomicilioFiscal().detalle + " , " + actor.DomicilioFiscal().Ubigeo.descripcion_larga).ToUpper();
                }
                return direccion;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int ObtenerIdUbigeoDireccionActorComercial(int id)
        {
            try
            {
                int ubigeo = 0;
                ActorComercial actor = new ActorComercial(_actor_Repositorio.ObtenerActorDeNegocio(id));
                if (actor.DomicilioFiscal() != null)
                {
                    ubigeo = actor.DomicilioFiscal().Ubigeo.id;
                }
                return ubigeo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public ItemGenerico ObtenerUbigeoDireccionActorComercial(int id)
        {
            try
            {
                ItemGenerico ubigeo = new ItemGenerico();
                ActorComercial actor = new ActorComercial(_actor_Repositorio.ObtenerActorDeNegocio(id));
                if (actor.DomicilioFiscal() != null)
                {
                    ubigeo = new ItemGenerico
                    {
                        Id = actor.DomicilioFiscal().Ubigeo.id,
                        Nombre = actor.DomicilioFiscal().Ubigeo.descripcion_larga
                    };
                }
                return ubigeo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string ObtenerDetalleDireccionActorComercial(int id)
        {
            try
            {
                string direccion = "";
                ActorComercial actor = new ActorComercial(_actor_Repositorio.ObtenerActorDeNegocio(id));
                if (actor.DomicilioFiscal() != null)
                {
                    direccion = actor.DomicilioFiscal().detalle.ToUpper();
                }
                return direccion;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Crear, Obtener, Actualizar Turno
        public OperationResult CrearTurno(int idCentroDeAtencion, int idEmpleado, DateTime desde, DateTime hasta)
        {
            try
            {
                DateTime fechaActual = DateTimeUtil.FechaActual();
                Vinculo_Actor_Negocio turno = new Vinculo_Actor_Negocio();
                turno.id_actor_negocio_principal = idCentroDeAtencion;
                turno.id_actor_negocio_vinculado = idEmpleado;
                turno.es_vigente = (hasta > fechaActual.Date);
                turno.desde = desde;
                turno.hasta = hasta;
                turno.descripcion = "";
                turno.tipo_vinculo = (int)TipoVinculo.Turno;
                return _vinculoActor_Repositorio.CrearVinculoActorNegocio(turno);
            }
            catch (Exception e) { throw e; }
        }
        public TurnoDeEmpleado ObtenerTurno(int idTurno)
        {
            try
            {
                return new TurnoDeEmpleado(_actorRepositorio.ObtenerVinculoActorNegocioSegunElTipoDeVinculo(idTurno, (int)TipoVinculo.Turno));
            }
            catch (Exception e) { throw e; }
        }
        public List<TurnoDeEmpleado> ObtenerTurnos()
        {
            try
            {
                return TurnoDeEmpleado.Convert(_actorRepositorio.ObtenerVinculosActorNegocioSegunElTipoDeVinculo((int)TipoVinculo.Turno).ToList());
            }
            catch (Exception e) { throw e; }
        }
        public OperationResult ActualizarTurno(int id, int idCentroDeAtencion, int idEmpleado, DateTime desde, DateTime hasta)
        {
            try
            {
                DateTime fechaActual = DateTimeUtil.FechaActual();
                Vinculo_Actor_Negocio turno = new Vinculo_Actor_Negocio();
                turno.id = id;
                turno.id_actor_negocio_principal = idCentroDeAtencion;
                turno.id_actor_negocio_vinculado = idEmpleado;
                turno.es_vigente = (hasta > fechaActual.Date);
                turno.desde = desde;
                turno.hasta = hasta;
                turno.descripcion = "";
                turno.tipo_vinculo = (int)TipoVinculo.Turno;
                return _actorRepositorio.ActualizarVinculoActorNegocio(turno);
            }
            catch (Exception e) { throw e; }
        }
        #endregion

        #region Crear, Obtener, Actualizar Cartera de clientes
        public OperationResult CrearCarteraDeClientes(int idCentroDeAtencion, List<int> idClientes)
        {
            try
            {
                List<Vinculo_Actor_Negocio> carteraDeClientes = new List<Vinculo_Actor_Negocio>();
                DateTime fechaActual = DateTimeUtil.FechaActual();
                DateTime fechaFin = fechaActual.AddYears(50);
                for (int i = 0; i < idClientes.Count; i++)
                {
                    Vinculo_Actor_Negocio vinculo = new Vinculo_Actor_Negocio
                    {
                        id_actor_negocio_principal = idCentroDeAtencion,
                        id_actor_negocio_vinculado = idClientes[i],
                        desde = fechaActual,
                        hasta = fechaFin,
                        descripcion = "",
                        tipo_vinculo = (int)TipoVinculo.CarteraDeCliente,
                        es_vigente = true
                    };
                    carteraDeClientes.Add(vinculo);
                }
                return _actorRepositorio.CrearVinculosActorNegocio(carteraDeClientes);
            }
            catch (Exception e) { throw e; }
        }
        public OperationResult ActualizarCarteraDeClientes(int idCentroDeAtencion, List<int> idClientes)
        {
            try
            {
                List<Vinculo_Actor_Negocio> carteraDeClientes = new List<Vinculo_Actor_Negocio>();
                DateTime fechaActual = DateTimeUtil.FechaActual();
                DateTime fechaFin = fechaActual.AddYears(50);
                for (int i = 0; i < idClientes.Count; i++)
                {
                    Vinculo_Actor_Negocio vinculo = new Vinculo_Actor_Negocio
                    {
                        id_actor_negocio_principal = idCentroDeAtencion,
                        id_actor_negocio_vinculado = idClientes[i],
                        desde = fechaActual,
                        hasta = fechaFin,
                        descripcion = "",
                        tipo_vinculo = (int)TipoVinculo.CarteraDeCliente,
                        es_vigente = true
                    };
                    carteraDeClientes.Add(vinculo);
                }
                return _actorRepositorio.ActualizarVinculosActorNegocio(idCentroDeAtencion, carteraDeClientes, (int)TipoVinculo.CarteraDeCliente);
            }
            catch (Exception e) { throw e; }
        }
        public List<CarteraDeClientes> ObtenerCarterasDeClientes()
        {
            try
            {
                return CarteraDeClientes.Convert(_actorRepositorio.ObtenerVinculosActorDeNegocio((int)TipoVinculo.CarteraDeCliente).ToList());
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public CarteraDeClientes ObtenerCarteraDeClientesSegunCentroDeAtencion(int idCentroAtencion)
        {
            try
            {
                return new CarteraDeClientes(_actorRepositorio.ObtenerVinculosActorDeNegocio(idCentroAtencion, (int)TipoVinculo.CarteraDeCliente).ToList());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Cliente> ObtenerClientesSegunElCentroDeAtencionYElTipoDeVinculo(int idCentroDeAtencion, int tipoDeVinculo)
        {
            try
            {
                return Cliente.Convert(_actorRepositorio.ObtenerActoresDeNegocioSegunActorNegocioPrincipalYElTipoDeVinculoIncluyendoCuotas(idCentroDeAtencion, tipoDeVinculo).ToList());
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<int> ObtenerIdsCentrosDeAtencionDeLaCarteraDeClientes()
        {
            try
            {
                return _actorRepositorio.ObtenerIdsActoresDeNegocioPrincipalDeVinculoActorNegocio((int)TipoVinculo.CarteraDeCliente).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<SelectorActorComercial> ObtenerActoresComercialesVigentesPorRolYBusqueda(int idRol, string cadenaBusqueda)
        {
            try
            {
                List<SelectorActorComercial> resultado = _actorRepositorio.ObtenerActoresComercialesVigentesPorRolYBusqueda(idRol, cadenaBusqueda).ToList();
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener actores comerciales", e);
            }
        }
        public List<ItemGenerico> ObtenerActoresComercialesPorCentroDeAtencion(int idCentroDeAtencion)
        {
            try
            {
                List<ItemGenerico> resultado = _actorRepositorio.ObtenerActoresComercialesPorCentroDeAtencion(idCentroDeAtencion).ToList();
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener actores comerciales", e);
            }
        }
        public ItemGenerico DeterminarTipoDeDocumentoDeIdentidad(string documento)
        {
            ItemGenerico tipoDocumentoIdentidad = null;
            if (documento.Length == 8)
            {
                tipoDocumentoIdentidad = new ItemGenerico { Id = ActorSettings.Default.IdTipoDocumentoIdentidadDni, Nombre = "DNI", Valor = "DNI" };
            }
            else if (documento.Length == 11)
            {
                tipoDocumentoIdentidad = new ItemGenerico { Id = ActorSettings.Default.IdTipoDocumentoIdentidadRuc, Nombre = "RUC", Valor = "RUC" };
            }
            return tipoDocumentoIdentidad;
        }
        public ItemGenerico ObtenerTipoDeDocumentoDeIdentidad(int idTipoDocumento)
        {
            var tipoDocumento = _maestroRepositorio.ObtenerDetalle(idTipoDocumento);
            ItemGenerico tipoDocumentoIdentidad = new ItemGenerico { Id = tipoDocumento.id, Nombre = tipoDocumento.valor, Valor = tipoDocumento.valor };
            return tipoDocumentoIdentidad;
        }
        public int ObtenerAñosDeVigenciaDeActorComercialPorRol(int idRol)
        {
            if (idRol == ActorSettings.Default.IdRolCliente)
            {
                return ActorSettings.Default.vigenciaEnAnyosPorDefectoDeActorDeNegocioCliente;
            }
            else
            {
                return ActorSettings.Default.vigenciaEnAnyosPorDefectoDeActorDeNegocioProveedor;
            }
        }
        /// <summary>
        /// Guarda(crea, actualiza) un actor comercial(cliente, proveedor, empleado), como no se tiene dirección, se creará una por defecto.
        /// </summary>
        /// <param name="idRol"></param>
        /// <param name="actorComercial"></param>
        /// <returns></returns>
        public OperationResult GuardarActorComercial(int idRol, RegistroActorComercial actorComercial)
        {
            try
            {
                OperationResult result = new OperationResult();
                DateTime fechaActual = DateTimeUtil.FechaActual();
                //Determinar valores de documento y validaciones de los datos del actor
                actorComercial.TipoDocumentoIdentidad = ObtenerTipoDeDocumentoDeIdentidad(actorComercial.TipoDocumentoIdentidad.Id);
                _validacionActorNegocio_Logica.ValidarDocumentoIdentidad(actorComercial.NumeroDocumentoIdentidad, actorComercial.TipoDocumentoIdentidad);
                _validacionActorNegocio_Logica.ValidarExisteActorConElMismoDocumentoYDistintoId(actorComercial.IdActor, actorComercial.NumeroDocumentoIdentidad);
                _validacionActorNegocio_Logica.ValidarExisteActorComercialConElMismoDocumentoVigente(idRol, actorComercial.NumeroDocumentoIdentidad, actorComercial.Id);
                if (actorComercial.Id != 0) _validacionActorNegocio_Logica.ValidarOperaciondesDeActorComercial(idRol, actorComercial.Id, actorComercial.NumeroDocumentoIdentidad);
                //Calculos de los valores que tendra el actor comercial como el codigo, fecha 
                actorComercial.Codigo = actorComercial.Codigo ?? ObtenerProximoCodigo(idRol);
                DateTime fechaInicial = fechaActual;
                DateTime fechaFinal = fechaInicial.AddYears(ObtenerAñosDeVigenciaDeActorComercialPorRol(idRol));
                actorComercial.Correo = actorComercial.Correo ?? "";
                actorComercial.Telefono = actorComercial.Telefono ?? "";
                actorComercial.NombreCorto = actorComercial.NombreCorto ?? "";
                actorComercial.NombreComercial = actorComercial.NombreComercial ?? "";
                actorComercial.NombreComercial = idRol == ActorSettings.Default.IdRolEmpleado ? actorComercial.Nombres + " " + actorComercial.ApellidoPaterno + " " + actorComercial.ApellidoMaterno : actorComercial.NombreComercial;
                if(idRol== ActorSettings.Default.IdRolEmpleado)
                {
                    actorComercial.NombreCorto = actorComercial.Nombres + " " + actorComercial.ApellidoPaterno.Substring(0, 1) + actorComercial.ApellidoMaterno.Substring(0, 1);
                }
                actorComercial.Nacionalidad = actorComercial.Nacionalidad ?? new ItemGenerico(MaestroSettings.Default.IdDetalleMaestroNacionPeru, MaestroSettings.Default.NombreDetalleMaestroNacionPeru);
                actorComercial.FechaNacimiento = actorComercial.FechaNacimiento == new DateTime() ? fechaActual : actorComercial.FechaNacimiento;
                actorComercial.TipoPersona = actorComercial.TipoPersona ?? (actorComercial.TipoDocumentoIdentidad.Id == ActorSettings.Default.IdTipoDocumentoIdentidadRuc ? new ItemGenerico(actorComercial.NumeroDocumentoIdentidad.StartsWith("10") ? ActorSettings.Default.IdTipoActorPersonaNatural : ActorSettings.Default.IdTipoActorPersonaJuridica) : new ItemGenerico(ActorSettings.Default.IdTipoActorPersonaNatural));
                //Nombre o razon social se determina de acuerdo al tipo de persona, si es natural se concatena napellido paterno, materno y nombre y en el caso de juridica va la razon
                actorComercial.NombreORazonSocial = actorComercial.TipoPersona.Id == ActorSettings.Default.IdTipoActorPersonaNatural ? actorComercial.ApellidoPaterno + "|" + actorComercial.ApellidoMaterno + "|" + actorComercial.Nombres : actorComercial.NombreORazonSocial;
                actorComercial.ClaseActor = actorComercial.ClaseActor ?? (actorComercial.TipoPersona.Id == ActorSettings.Default.IdTipoActorPersonaNatural ? new ItemGenerico(ActorSettings.Default.IdClaseActorPersonaNaturalPorDefecto, ActorSettings.Default.NombreClaseActorPersonaNaturalPorDefecto) : new ItemGenerico(ActorSettings.Default.IdClaseActorPersonaJuridicaPorDefecto));
                actorComercial.EstadoLegal = actorComercial.EstadoLegal ?? (actorComercial.TipoPersona.Id == ActorSettings.Default.IdTipoActorPersonaNatural ? new ItemGenerico(ActorSettings.Default.IdEstadoLegalActorPersonaNaturalPorDefecto) : new ItemGenerico(ActorSettings.Default.IdEstadoLegalActorPersonaJuridicaPorDefecto));
                //Crear el actor con los valores de actor comercial y ponemos la direccion registrada o una direccion por defecto no especificado
                Actor actor = new Actor(actorComercial.TipoDocumentoIdentidad.Id, actorComercial.FechaNacimiento, actorComercial.NumeroDocumentoIdentidad, actorComercial.NombreORazonSocial, actorComercial.NombreComercial, actorComercial.Telefono, actorComercial.TipoPersona.Id, ActorSettings.Default.IdFotoActorPorDefecto, actorComercial.ClaseActor.Id, actorComercial.EstadoLegal.Id, actorComercial.Correo, actorComercial.NombreCorto, "")
                {
                    id_detalle_multiproposito = actorComercial.Nacionalidad.Id
                };
                actor.Direccion.Add(actorComercial.DomicilioFiscal != null ? actorComercial.DomicilioFiscal.Convert() : DireccionNoEspecificada());
                //Crear el actor de negocio con en nuevo actor
                Actor_negocio actor_negocio = new Actor_negocio(idRol, fechaInicial, fechaFinal, actorComercial.Codigo, true, false, "") { Actor = actor };
                //En caso el rol sea cliente y esta permitido el comprobante por defecto lo agregamos
                if (idRol == ActorSettings.Default.IdRolCliente)
                {
                    if (ActorSettings.Default.PermitirComprobantePorDefectoEnCliente)
                    {
                        actorComercial.ComprobantePredeterminado = actorComercial.ComprobantePredeterminado ?? new ItemGenerico(MaestroSettings.Default.IdDetalleMaestroComprobanteNotaVentaInterna);
                        actor_negocio.Parametro_actor_negocio.Add(new Parametro_actor_negocio(MaestroSettings.Default.IdDetalleMaestroParametroComprobanteDeClientePredeterminado, actorComercial.ComprobantePredeterminado.Id.ToString()));
                    }
                }
                //En caso el rol sea empleado registar los roles de personal que tendra el empleado
                if (idRol == ActorSettings.Default.IdRolEmpleado)
                {
                    if (actorComercial.Roles != null && actorComercial.Roles.Count > 0)
                    {
                        foreach (var rol in actorComercial.Roles)
                        {
                            actor_negocio.Actor.Actor_negocio.Add(new Actor_negocio(rol.Id, fechaInicial, fechaFinal, actorComercial.Codigo, true, false, ""));
                        }
                    }
                }
                //Verificar si es que se tiene que crear el actor de negocio, actualizar el actor de negocio o crear actor de negocio y actualizar el actor comercial
                if (actorComercial.Id > 0)
                {
                    if (actorComercial.IdActor > 0)
                    {
                        actor_negocio.id = actorComercial.Id;
                        actor_negocio.id_actor = actor_negocio.Actor.id = actorComercial.IdActor;
                        actor_negocio.Actor.Direccion.ToList().ForEach(d => d.id_actor = actorComercial.IdActor);
                        actor_negocio.Actor.Actor_negocio.ToList().ForEach(d => d.id_actor = actorComercial.IdActor);
                        result = _actor_Repositorio.ActualizarActorNegocio(actor_negocio);
                    }
                    else
                    {
                        actor_negocio.id = actorComercial.Id;
                        actor_negocio.Actor.Direccion.ToList().ForEach(d => d.id_actor = actorComercial.IdActor);
                        actor_negocio.Actor.Actor_negocio.ToList().ForEach(d => d.id_actor = actorComercial.IdActor);
                        result = _actor_Repositorio.ActualizarActorNegocioCreandoActor(actor_negocio);
                    }
                }
                else
                {
                    if (actorComercial.IdActor > 0)
                    {
                        actor_negocio.id_actor = actor_negocio.Actor.id = actorComercial.IdActor;
                        actor_negocio.Actor.Direccion.ToList().ForEach(d => d.id_actor = actorComercial.IdActor);
                        actor_negocio.Actor.Actor_negocio.ToList().ForEach(d => d.id_actor = actorComercial.IdActor);
                        result = _actor_Repositorio.CrearActorNegocioActualizandoActor(actor_negocio);
                    }
                    else
                    {
                        _validacionActorNegocio_Logica.ValidarExistenciaDeDocumento(actorComercial.TipoDocumentoIdentidad.Id, actorComercial.NumeroDocumentoIdentidad);
                        result = _actor_Repositorio.CrearActorNegocio(actor_negocio);
                    }
                }
                actorComercial.Id = actor_negocio.id;
                actorComercial.IdActor = actor_negocio.id_actor;
                actorComercial.NombreORazonSocial = actorComercial.NombreORazonSocial.Replace("|", " ");
                result.information = actorComercial;
                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar guardar el actor comercial", e);
            }
        }
        public ActorComercial_ ObtenerActorComercialCreandoloSiExisteSoloComoActor(int idRol, string documento)
        {
            ItemGenerico tipoDocumentoIdentidad = DeterminarTipoDeDocumentoDeIdentidad(documento);
            //Obtener actor comercial por numero documento
            ActorComercial_ actorComercial = _consultaActor_Repositorio.ObtenerActorComercial_(idRol, documento);
            if (actorComercial == null && tipoDocumentoIdentidad != null)
            {
                //Obtener el id actor por documento y tipo documento
                int idActor = _actorRepositorio.ObtenerIdActor(documento, tipoDocumentoIdentidad.Id);
                if (idActor > 0)
                {
                    var fechaActual = DateTimeUtil.FechaActual();
                    var codigo = ObtenerProximoCodigo(idRol);
                    DateTime fechaFin = fechaActual.AddYears(idRol == ActorSettings.Default.IdRolCliente ? ActorSettings.Default.vigenciaEnAnyosPorDefectoDeActorDeNegocioCliente : idRol == ActorSettings.Default.IdRolProveedor ? ActorSettings.Default.IdRolProveedor : ActorSettings.Default.vigenciaEnAnyosPorDefectoDeActorDeNegocioEntidadInterna);
                    var actorDeNegocio = new Actor_negocio(idRol, fechaActual, fechaFin, codigo, true, false, "") { id_actor = idActor };
                    _actor_Repositorio.CrearActorNegocio(actorDeNegocio);//creamos el actor de negocio con el actor obtenido
                    var nuevoActorComercial = _actor_Repositorio.ObtenerActorComercial(actorDeNegocio.id);
                    actorComercial = nuevoActorComercial;
                }
            }
            return actorComercial;
        }
        public ActorComercial_ ObtenerActorComercialPorId(int idRol, int id)
        {
            try
            {
                ActorComercial_ actorComercial = _consultaActor_Repositorio.ObtenerActorComercial_(idRol, id);
                return actorComercial;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener actor comercial", e);
            }
        }

        public ActorComercial_ ObtenerActorComercialCreandoloSiExisteSoloComoActor(int idRol, int idTipoDocumento, string documento)
        {
            //si existe el actor comercial: traerlo
            ActorComercial_ actorComercial = _consultaActor_Repositorio.ObtenerActorComercial_(idRol, idTipoDocumento, documento);
            //de lo contrario:
            if (actorComercial == null)
            {
                int idActor = _actorRepositorio.ObtenerIdActor(documento, idTipoDocumento); //tratar de conseguir el actor con el documento de identidad
                if (idActor > 0)//si existe el actor
                {
                    var fechaActual = DateTimeUtil.FechaActual();
                    var codigo = ObtenerProximoCodigo(idRol);
                    DateTime fechaFin = fechaActual.AddYears(idRol == ActorSettings.Default.IdRolCliente ? ActorSettings.Default.vigenciaEnAnyosPorDefectoDeActorDeNegocioCliente : idRol == ActorSettings.Default.IdRolProveedor ? ActorSettings.Default.IdRolProveedor : ActorSettings.Default.vigenciaEnAnyosPorDefectoDeActorDeNegocioEntidadInterna);
                    var actorDeNegocio = new Actor_negocio(idRol, fechaActual, fechaFin, codigo, true, false, "") { id_actor = idActor };
                    _actor_Repositorio.CrearActorNegocio(actorDeNegocio);//creamos el actor de negocio con el actor obtenido
                    var nuevoActorComercial = _actor_Repositorio.ObtenerActorComercial(actorDeNegocio.id);
                    actorComercial = nuevoActorComercial;
                }
            }
            return actorComercial;
        }

        public ActorComercial_ ResolverYObtenerActorComercial(int idRol, RegistroActorComercial registroActorComercial)
        {
            ActorComercial_ actorComercial = ObtenerActorComercialCreandoloSiExisteSoloComoActor(idRol, registroActorComercial.TipoDocumentoIdentidad.Id, registroActorComercial.NumeroDocumentoIdentidad);
            if (actorComercial == null)
            {
                actorComercial = (ActorComercial_)GuardarActorComercial(idRol, registroActorComercial).information;
            }
            return actorComercial;
        }
        #endregion

        public List<ItemGenerico> ObtenerGruposActoresComerciales()
        {
            try
            {
                var idsRolesGrupoActorComercial = Diccionario.IdsRolesGrupoActorComercial;
                var gruposClientes = _vinculoActor_Repositorio.ObtenerGruposActoresComerciales(idsRolesGrupoActorComercial.ToArray()).ToList();
                return gruposClientes;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener grupos de clientes", e);
            }
        }

        public List<ItemGenerico> ObtenerGruposActoresComercialesPorRol(int idRol)
        {
            try
            {
                var idRolGrupo = Diccionario.MapeoRolActorComercialVsIdRolGrupoActorComercial.Single(m => m.Key == idRol).Value;
                var gruposClientes = _vinculoActor_Repositorio.ObtenerGruposActoresComercialesPorRol(idRolGrupo).ToList();
                return gruposClientes;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener grupos de clientes", e);
            }
        }

        public List<ItemGenerico> ObtenerActoresComercialesDeGrupoActoresComercialesPorRol(int idRol, int idGrupoActoresComerciales)
        {
            try
            {
                var idRolGrupo = Diccionario.MapeoRolActorComercialVsIdRolGrupoActorComercial.Single(m => m.Key == idRol).Value;
                var gruposClientes = _vinculoActor_Repositorio.ObtenerActoresComercialesDeGrupoActoresComercialesPorRol((int)TipoVinculo.MiembroGrupo, idRolGrupo, idGrupoActoresComerciales).ToList();
                return gruposClientes;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener grupos de clientes", e);
            }
        }

        public List<ItemGenerico> ObtenerGruposActoresComercialesPorRolDeActorComercial(int idRol, int idActorComercial)
        {
            try
            {
                var idRolGrupo = Diccionario.MapeoRolActorComercialVsIdRolGrupoActorComercial.Single(m => m.Key == idRol).Value;
                var gruposClientes = _vinculoActor_Repositorio.ObtenerGruposActoresComercialesPorRolDeActorComercial((int)TipoVinculo.MiembroGrupo, idRolGrupo, idActorComercial).ToList();
                return gruposClientes;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener grupos de clientes", e);
            }
        }
    }
}

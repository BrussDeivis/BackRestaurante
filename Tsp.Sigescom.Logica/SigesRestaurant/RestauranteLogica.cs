using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Logica;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.ClasesNegocio.SigesRestaurant.Comprobantes;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Entidades.Sesion;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Actores;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Establecimientos;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Restaurant;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;
using Tsp.Sigescom.Modelo.Negocio.Restaurant;
using Tsp.Sigescom.Modelo.SigesRestaurant;

namespace Tsp.Sigescom.Parking.Logica
{
    public partial class RestauranteLogica : IRestauranteLogica
    {

        private readonly IOperacionLogica _operacionLogica;
        private readonly IMaestroRepositorio _maestroRepositorio;
        private readonly IActorRepositorio _actorRepositorio;
        private readonly IActor_Repositorio _actor_Repositorio;
        private readonly ICentroDeAtencion_Logica _centroDeAtencionLogica;

        private readonly IRestauranteRepositorio _restauranteRepositorio;
        private readonly ITransaccionRepositorio _transaccionRepositorio;

        private readonly IActorNegocioLogica _actorlogica;

        private readonly IMesa_Repositorio _mesaRepositorio;
        private readonly IAtencion_Repositorio _atencionRepositorio;
        private readonly IEstablecimiento_Repositorio _establecimientoRepositorio;




        public RestauranteLogica(IConceptoRepositorio conceptoRepositorio, ITransaccionRepositorio transaccionRepositorio, IMaestroRepositorio maestroRepositorio, IActorRepositorio actorRepositorio, IRestauranteRepositorio restauranteRepositorio, IPrecioRepositorio precioRepositorio, IFacturacionRepositorio facturacionRepositorio, IActorNegocioLogica actorlogica, ICodigosOperacion_Logica codigosOperacionLogica, IPermisos_Logica permisosLogica, IMesa_Repositorio mesaRepositorio, IAtencion_Repositorio atencionRepositorio, IActor_Repositorio actor_Repositorio, ICentroDeAtencion_Logica centroDeAtencionLogica, IEstablecimiento_Repositorio establecimientoRepositorio)
        {
            _transaccionRepositorio = transaccionRepositorio;
            _maestroRepositorio = maestroRepositorio;
            _actorRepositorio = actorRepositorio;
            _restauranteRepositorio = restauranteRepositorio;
            _mesaRepositorio = mesaRepositorio;
            _atencionRepositorio = atencionRepositorio;
            _operacionLogica = new OperacionLogica(_transaccionRepositorio, _maestroRepositorio, _actorRepositorio, null, facturacionRepositorio, codigosOperacionLogica, permisosLogica, null, actor_Repositorio, null, null);
            _actorlogica = actorlogica;
            _actor_Repositorio = actor_Repositorio;
            _centroDeAtencionLogica = centroDeAtencionLogica;
            _establecimientoRepositorio = establecimientoRepositorio;

        }
        public AtencionRestaurante ObtenerAtencionDeMesa(int idMesa)
        {
            try
            {
                AtencionRestaurante atencion;
                Mesa mesa = new Mesa(_mesaRepositorio.ObtenerMesa(idMesa));
                if (mesa.EstadoOcupado)
                {
                    atencion = _atencionRepositorio.ObtenerAtencionDeMesaOcupada(idMesa);
                }
                else
                {
                    atencion = new AtencionRestaurante(mesa);
                }
                return atencion;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al Obtener Atención de Mesa", e);
            }
        }
        public async Task<List<ItemJerarquico>> ObtenerCategorias()
        {
            try
            {
                return (await _maestroRepositorio.ObtenerDetallesJerarquicos(MaestroSettings.Default.IdMaestroCategoriaConcepto)).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener categorias", e);
            }
        }

        public List<ItemJerarquico> ObtenerCategoriasRestaurante()
        {
            try
            {
                var categorias = _maestroRepositorio.ObtenerDetallesJerarquicosPorRolConceptoNegocio(RestauranteSettings.Default.IdRolConceptoRestaurante).ToList();
                return categorias;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener categorias de restaurante", e);
            }
        }

        public async Task<List<Mesa>> ObtenerMesasDeAmbiente(int idAmbiente)
        {
            try
            {
                List<Actor_negocio> actoresDeNegocio = (await _actorRepositorio.ObtenerActoresDeNegocioVigentesPorIdActorNegocioPadre(idAmbiente)).ToList();
                List<Mesa> mesas = Mesa.Convert(actoresDeNegocio);
                return mesas;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener mesas de ambiente", e);
            }
        }
        public async Task<List<Ambiente>> ObtenerAmbientes()
        {
            try
            {
                List<Actor_negocio> actoresDeNegocio = (await _actorRepositorio.ObtenerActoresDeNegocioPorRolVigentes(RestauranteSettings.Default.IdRolAmbiente)).ToList();
                List<Ambiente> ambientes = Ambiente.Convert(actoresDeNegocio);

                return ambientes;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener ambientes", e);
            }
        }
        public async Task<List<Ambiente>> ObtenerAmbientes(int IdEstablecimiento)
        {
            try
            {
                List<Actor_negocio> actoresDeNegocio = (await _actorRepositorio.ObtenerActoresDeNegocioPorRolVigentes(RestauranteSettings.Default.IdRolAmbiente, IdEstablecimiento)).ToList();
                List<Ambiente> ambientes = Ambiente.Convert(actoresDeNegocio);

                return ambientes;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener ambientes", e);
            }
        }

        public List<CentroAtencionRestaurante> ObtenerPuntoDeVeliveryPuntoAlPasoVigentes(int IdEstablecimiento)
        {
            List<CentroAtencionRestaurante> centrosAtencionRestaurante = new List<CentroAtencionRestaurante>();
            List<int> idRoles = new List<int>();
            if (RestauranteSettings.Default.PermitirVentaPorDelivery) idRoles.Add(ActorSettings.Default.IdRolPuntoDelivery); 
            if (RestauranteSettings.Default.PermitirVentaAlPaso) idRoles.Add(ActorSettings.Default.IdRolPuntoAlPaso);
            var centrosAtencion = _centroDeAtencionLogica.ObtenerCentrosDeAtencionVigentesPorEstablecimientoComercial(IdEstablecimiento).ToList();
            centrosAtencion = centrosAtencion.Where(c => c.RolesHijosVigentes.Select(r => r.Id).Any(i => idRoles.Contains(i))).ToList();
            foreach (var centroAtencion in centrosAtencion)
            {
                centrosAtencionRestaurante.Add(new CentroAtencionRestaurante
                {
                    Id = centroAtencion.Id,
                    Nombre = centroAtencion.Nombre,
                    EsPuntoDelivery = RestauranteSettings.Default.PermitirVentaPorDelivery ? centroAtencion.RolesHijosVigentes.SingleOrDefault(r => r.Id == ActorSettings.Default.IdRolPuntoDelivery) != null : false,
                    EsPuntoAlPaso = RestauranteSettings.Default.PermitirVentaAlPaso ? centroAtencion.RolesHijosVigentes.SingleOrDefault(r => r.Id == ActorSettings.Default.IdRolPuntoAlPaso) != null : false,
                });
            }
            return centrosAtencionRestaurante;
        }

        public async Task<List<ItemGenerico>> ObtenerMozosVigentes()
        {
            try
            {
                var mozos = (await _actorRepositorio.ObtenerActoresDeNegocioPrincipalesVigentesComoItemsGenericos(ActorSettings.Default.IdRolEmpleado, RestauranteSettings.Default.IdRolMozo)).ToList();
                mozos.ForEach(m => { var nombreArray = m.Nombre.Split('|'); m.Nombre = nombreArray[2] + " " + nombreArray[0] + " " + nombreArray[1]; });
                return mozos;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener los mozos", e);
            }
        }

        public OperationResult AtenderDetalleDeOrden(int idDetalleDeOrden)
        {
            Orden_Atencion ordenAtencion = _restauranteRepositorio.ObtenerOrdenDeAtencionDeDetalleOrden(idDetalleDeOrden);
            if (ordenAtencion.Estado != MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
            {
                throw new LogicaException("No se puede cerrar la orden, debido a que no cuenta con el estado confirmado");
            }
            OperationResult result = _restauranteRepositorio.ActualizarEstadoDeDetalleDeOrden_CalcularOrden_CalcularAtencion(idDetalleDeOrden, EstadoDeDetalleDeOrden.Atendido);
            return result;
        }

        public OperationResult ObservarDetalleDeOrden(int idDetalleDeOrden)
        {
            Orden_Atencion ordenAtencion = _restauranteRepositorio.ObtenerOrdenDeAtencionDeDetalleOrden(idDetalleDeOrden);
            if (ordenAtencion.Estado != MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
            {
                throw new LogicaException("No se puede cerrar la orden, debido a que no cuenta con el estado confirmado");
            }
            OperationResult result = _restauranteRepositorio.ActualizarEstadoDeDetalleDeOrden_CalcularOrden_CalcularAtencion(idDetalleDeOrden, EstadoDeDetalleDeOrden.Observado);
            return result;
        }

        public OperationResult DevolverDetalleDeOrden(int idDetalleDeOrden)
        {
            Orden_Atencion ordenAtencion = _restauranteRepositorio.ObtenerOrdenDeAtencionDeDetalleOrden(idDetalleDeOrden);
            if (ordenAtencion.Estado != MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
            {
                throw new LogicaException("No se puede cerrar la orden, debido a que no cuenta con el estado confirmado");
            }
            OperationResult result = _restauranteRepositorio.ActualizarEstadoDeDetalleDeOrden_CalcularOrden_CalcularAtencion(idDetalleDeOrden, EstadoDeDetalleDeOrden.Devuelto);
            return result;
        }

        public OperationResult AnularDetalleDeOrden(int idDetalleDeOrden)
        {
            Orden_Atencion ordenAtencion = _restauranteRepositorio.ObtenerOrdenDeAtencionDeDetalleOrden(idDetalleDeOrden);
            if (ordenAtencion.Estado != MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
            {
                throw new LogicaException("No se puede cerrar la orden, debido a que no cuenta con el estado confirmado");
            }
            OperationResult result = _restauranteRepositorio.ActualizarEstadoDeDetalleDeOrden_CalcularOrden_CalcularAtencion(idDetalleDeOrden, EstadoDeDetalleDeOrden.Anulado);
            return result;
        }

        public OperationResult ReanudarDetalleDeOrden(int idDetalleDeOrden)
        {
            Orden_Atencion ordenAtencion = _restauranteRepositorio.ObtenerOrdenDeAtencionDeDetalleOrden(idDetalleDeOrden);
            if (ordenAtencion.Estado != MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
            {
                throw new LogicaException("No se puede cerrar la orden, debido a que no cuenta con el estado confirmado");
            }
            OperationResult result = _restauranteRepositorio.ActualizarEstadoDeDetalleDeOrden_CalcularOrden_CalcularAtencion(idDetalleDeOrden, EstadoDeDetalleDeOrden.Registrado);
            return result;
        }

        public OperationResult PrepararDetalleDeOrden(int idDetalleDeOrden)
        {
            OperationResult result = _restauranteRepositorio.ActualizarEstadoDeDetalleDeOrden_CalcularOrden_CalcularAtencion(idDetalleDeOrden, EstadoDeDetalleDeOrden.Preparacion);
            return result;
        }
        public OperationResult ServirDetalleDeOrden(int idDetalleDeOrden)
        {
            OperationResult result = _restauranteRepositorio.ActualizarEstadoDeDetalleDeOrden_CalcularOrden_CalcularAtencion(idDetalleDeOrden, EstadoDeDetalleDeOrden.Servido);
            return result;
        }

        public OperationResult ReanudarTodosLosDetallesDeOrden(long idOrden)
        {
            try
            {
                Orden_Atencion ordenAtencion = _restauranteRepositorio.ObtenerOrdenDeAtencion(idOrden);
                if (ordenAtencion.Estado != MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                {
                    throw new LogicaException("No se puede cerrar la orden, debido a que no cuenta con el estado confirmado");
                }
                OperationResult result = _restauranteRepositorio.ActualizarEstadosDeDetallesDeOrden_CalcularOrden_CalcularAtencion(idOrden, EstadoDeDetalleDeOrden.Registrado);
                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar reanudar todos los detalles de la orden", e);
            }
        }
        public OperationResult AnularTodosLosDetallesDeOrden(long idOrden)
        {
            try
            {
                Orden_Atencion ordenAtencion = _restauranteRepositorio.ObtenerOrdenDeAtencion(idOrden);
                if (ordenAtencion.Estado != MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                {
                    throw new LogicaException("No se puede cerrar la orden, debido a que no cuenta con el estado confirmado");
                }
                OperationResult result = _restauranteRepositorio.ActualizarEstadosDeDetallesDeOrden_CalcularOrden_CalcularAtencion(idOrden, EstadoDeDetalleDeOrden.Anulado);
                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar anular todos los detalles de la orden", e);
            }
        }

        public OperationResult AtenderTodosLosDetallesDeOrden(long idOrden)
        {
            try
            {
                Orden_Atencion ordenAtencion = _restauranteRepositorio.ObtenerOrdenDeAtencion(idOrden);
                if (ordenAtencion.Estado != MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                {
                    throw new LogicaException("No se puede cerrar la orden, debido a que no cuenta con el estado confirmado");
                }
                OperationResult result = _restauranteRepositorio.ActualizarEstadosDeDetallesDeOrden_CalcularOrden_CalcularAtencion(idOrden, EstadoDeDetalleDeOrden.Atendido);
                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar atender todos los detalles de la orden", e);
            }
        }

        public OperationResult PrepararDetallesDeOrdenes(long[] idsDetallesDeOrdenes)
        {
            OperationResult result = _restauranteRepositorio.ActualizarEstadosDeDetallesDeOrdenes(idsDetallesDeOrdenes, EstadoDeDetalleDeOrden.Preparacion);
            return result;
        }
        public OperationResult ServirDetallesDeOrdenes(long[] idsDetallesDeOrdenes)
        {
            OperationResult result = _restauranteRepositorio.ActualizarEstadosDeDetallesDeOrdenes(idsDetallesDeOrdenes, EstadoDeDetalleDeOrden.Servido);
            return result;
        }

        public OperationResult FinalizarAtencionCocina(long idAtencion)
        {
            Transaccion dbtransaccion = _transaccionRepositorio.ObtenerTransaccion(idAtencion);
            dbtransaccion.id_estado_actual = MaestroSettings.Default.IdDetalleMaestroEstadoCanjeado;
            OperationResult result = _transaccionRepositorio.ActualizarTransaccion(dbtransaccion);
            return result;
        }



        public List<AtencionRestaurante> ObtenerAtencionesConfirmadas()
        {
            // CAMBIAR POR DETALLE DE MAESTRO ESTADO ATENCION SOLICIUD
            var estadoRegistradoId = MaestroSettings.Default.IdDetalleMaestroEstadoRegistrado;
            List<AtencionRestaurante> atenciones = _restauranteRepositorio.ObtenerAtencionesPorEstado(estadoRegistradoId).ToList();
            return atenciones;
        }

        public List<AtencionSinMesa> ObtenerAtencionesSinMesa(int idCentroAtencion)
        {
            var idsEstados = new int[] { MaestroSettings.Default.IdDetalleMaestroEstadoRegistrado, MaestroSettings.Default.IdDetalleMaestroEstadoCerrado };
            List<AtencionSinMesa> atenciones = _restauranteRepositorio.ObtenerAtencionesSinMesaPorEstados(idsEstados, idCentroAtencion).OrderByDescending(a => a.Fecha).ToList();
            return atenciones;
        }


        public List<ResumenAtencion> ObtenerResumenAtencionesCerradas(DateTime fechaDesde, DateTime fechaHasta, SesionRestaurante sesion)
        {
            var idEstadoCerrado = MaestroSettings.Default.IdDetalleMaestroEstadoCerrado;
            var idsCentrosDeAtencion = _centroDeAtencionLogica.ObtenerIdsCentrosAtencionConRolPuntoVenta(sesion.SesionDeUsuario.EstablecimientoComercialSeleccionado.Id);
            List<ResumenAtencion> atenciones = _restauranteRepositorio.ObtenerResumenDeAtencionesPorEstado(fechaDesde, fechaHasta, idEstadoCerrado, idsCentrosDeAtencion).OrderBy(a => a.ImporteTotalCero).ThenBy(a => a.Facturado).ThenBy(a => a.Confirmado).ThenByDescending(a => a.FechaAtencion).ToList();

            return atenciones;
        }
        public List<Orden_Atencion> ObtenerOrdenesPorEstadoDesdeUnAmbiente(int numEstado, int idAmbiente)
        {
            List<Orden_Atencion> ordenes = _restauranteRepositorio.ObtenerOrdenesPorEstadoDeUnAmbiente(numEstado, idAmbiente).ToList();

            return ordenes;

        }

        public List<Orden_Atencion> ObtenerOrdenesPorEstado(int numEstado)
        {
            List<Orden_Atencion> ordenes = _restauranteRepositorio.ObtenerOrdenesPorEstado(numEstado).ToList();

            return ordenes;

        }

        public List<OrdenAtencion_Consulta> ObtenerOrdenesAtencion(DateTime desde, DateTime hasta, int idEstablecimiento)
        {
            var ordenes = _restauranteRepositorio.ObtenerOrdenesAtencion(desde, hasta, idEstablecimiento).ToList();
            var atenciones = ordenes.GroupBy(o => o.IdAtencion).ToList();
            atenciones.ForEach(a => { if (a.Sum(oa => oa.Importe) == 0) { a.ToList().ForEach(o => o.Estado = "N/A"); } });
            ordenes.ForEach(o => o.Estado = o.Estado != "N/A" ? (o.Facturado ? "FACTURADO" : "PENDIENTE") : "N/A");
            ordenes.ForEach(o => o.Mesa = o.EsAtencionEnSalon ? "Mesa " + o.Mesa : o.Mesa);
            ordenes = ordenes.OrderByDescending(o => o.FechaHoraAtencion).ToList();
            return ordenes;
        }

        public List<OrdenPorModoAtencion_Consulta> ObtenerOrdenesPorModoAtencion(DateTime desde, DateTime hasta, int idEstablecimiento)
        {
            var ordenes = _restauranteRepositorio.ObtenerOrdenesPorModoAtencion(desde, hasta, idEstablecimiento).ToList();
            var atenciones = ordenes.GroupBy(o => o.IdAtencion).ToList();
            atenciones.ForEach(a => { if (a.Sum(oa => oa.Importe) == 0) { a.ToList().ForEach(o => o.Estado = "N/A"); } });
            ordenes.ForEach(o => o.Estado = o.Estado != "N/A" ? (o.Facturado ? "FACTURADO" : "PENDIENTE") : "N/A");
            ordenes.ForEach(o => o.Mesa = o.EsAtencionEnSalon ? "Mesa " + o.Mesa : o.Mesa);
            ordenes.ForEach(o => o.Ambiente = o.ModoAtencion + " - " + o.Ambiente);
            ordenes = ordenes.OrderByDescending(o => o.FechaHoraAtencion).ToList();
            return ordenes;
        }

        public List<DetalleOrden> ObtenerDetallesDeOrden(int idOrden)
        {
            try
            {
                return _restauranteRepositorio.ObtenerDetallesDeOrden(idOrden).ToList();

            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener los detalles de la orden", e);
            }
        }
        public async Task<OperationResult> CrearAmbiente(Ambiente ambiente)
        {
            try
            {
                List<Actor_negocio> actoresDeNegocio = (await _actorRepositorio.ObtenerActoresDeNegocioPorRolVigentes(RestauranteSettings.Default.IdRolAmbiente, ambiente.Establecimiento.Id)).ToList();
                if (actoresDeNegocio.Select(an => an.Actor.primer_nombre).Contains(ambiente.Nombre))
                {
                    throw new LogicaException("Ya existe un ambiente con el nombre ingresado, porfavor ingrese otro nombre.");
                }
                Actor_negocio actorNegocioAmbiente = GenerarActorDeNegocioDesdeAmbiente(ambiente);

                var resultado = _actor_Repositorio.CrearActorNegocio(actorNegocioAmbiente);

                resultado.information = "ok";
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar crear el ambiente", e);
            }
        }
        public async Task<OperationResult> ActualizarAmbiente(Ambiente ambiente)
        {
            try
            {
                List<Actor_negocio> actoresDeNegocio = (await _actorRepositorio.ObtenerActoresDeNegocioPorRolVigentes(RestauranteSettings.Default.IdRolAmbiente, ambiente.Establecimiento.Id)).ToList();
                actoresDeNegocio.Remove(actoresDeNegocio.Single(a => a.id == ambiente.Id));
                if (actoresDeNegocio.Select(an => an.Actor.primer_nombre.ToUpper()).Contains(ambiente.Nombre.ToUpper()))
                {
                    throw new LogicaException("Ya existe un ambiente con el nombre ingresado, porfavor ingrese otro nombre.");
                }
                Actor_negocio ActorAmbiente = _actor_Repositorio.ObtenerActorDeNegocio(ambiente.Id);

                ActorAmbiente.extension_json = "{nombre: \"" + ambiente.Nombre + "\", filas: " + ambiente.Filas + ", columnas: " + ambiente.Columnas + ", mesastemporales: " + ambiente.MesasTemporales.ToString().ToLower() + "}";
                ActorAmbiente.Actor.primer_nombre = ambiente.Nombre;

                var result = _actor_Repositorio.ActualizarActorNegocioIncluidoActor(ActorAmbiente);
                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar actualizar el ambiente", e);
            }
        }

        public async Task<OperationResult> EliminarAmbiente(int idAmbiente)
        {
            try
            {
                List<Actor_negocio> actoresDeNegocio = (await _actorRepositorio.ObtenerActoresDeNegocioVigentesPorIdActorNegocioPadre(idAmbiente)).ToList();
                if (actoresDeNegocio.Count() > 0)
                {
                    throw new LogicaException("No es posible eliminar el ambiente debido a que tiene mesas vigentes");
                }
                Actor_negocio ActorAmbiente = _actor_Repositorio.ObtenerActorDeNegocio(idAmbiente);
                ActorAmbiente.es_vigente = false;
                ActorAmbiente.fecha_fin = DateTimeUtil.FechaActual();
                var result = _actor_Repositorio.ActualizarActorNegocioIncluidoActor(ActorAmbiente);
                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar eliminar el ambiente", e);
            }
        }
        public OperationResult CrearMesa(Mesa mesa)
        {
            try
            {
                Actor_negocio mesaActor = GenerarActorDeNegocioDesdeMesa(mesa);
                OperationResult resultado = _actor_Repositorio.CrearActorNegocio(mesaActor);
                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Actor GenerarActorDesdeAmbiente(Ambiente ambiente)
        {
            try
            {
                Actor actor = new Actor()
                {
                    id_documento_identidad = RestauranteSettings.Default.IdDetalleMaestroDocumentoIdentidadAmbiente,
                    numero_documento_identidad = "",
                    primer_nombre = ambiente.Nombre,
                    segundo_nombre = "",
                    fecha_nacimiento = DateTimeUtil.FechaActual(),
                    telefono = "",
                    id_tipo_actor = RestauranteSettings.Default.IdTipoActorRestauranteAmbiente,
                    id_foto = 1,
                    id_clase_actor = RestauranteSettings.Default.IdClaseActorRestauranteAmbiente,
                    id_estado_legal = 1,
                    correo = "",
                    tercer_nombre = "",
                    pagina_web = "",
                    informacion_multiproposito = null,
                    id_detalle_multiproposito = null,
                    id_detalle_multiproposito1 = null
                };
                return actor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Actor_negocio GenerarActorDeNegocioDesdeAmbiente(Ambiente ambiente)
        {
            try
            {
                Actor_negocio actor = new Actor_negocio()
                {
                    id_rol = RestauranteSettings.Default.IdRolAmbiente,
                    id_actor_negocio_padre = ambiente.Establecimiento.Id,
                    fecha_inicio = DateTimeUtil.FechaActual(),
                    fecha_fin = DateTimeUtil.FechaActual().AddYears(ActorSettings.Default.vigenciaEnAnyosPorDefectoDeActorDeNegocioEntidadInterna),
                    codigo_negocio = "",
                    es_vigente = true,
                    extension_json = "{nombre: \"" + ambiente.Nombre + "\", filas: " + ambiente.Filas + ", columnas: " + ambiente.Columnas + ", mesastemporales: " + ambiente.MesasTemporales.ToString().ToLower() + "}",
                    indicador1 = false,
                    id_detalle_maestro_multiproposito = null,
                    Actor = GenerarActorDesdeAmbiente(ambiente)

                };

                return actor;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public Actor GenerarActorDesdeMesa(Mesa mesa)
        {
            try
            {
                return new Actor()
                {
                    id_documento_identidad = RestauranteSettings.Default.IdDetalleMaestroDocumentoIdentidadMesa,
                    numero_documento_identidad = "",
                    primer_nombre = mesa.Nombre,
                    segundo_nombre = "",
                    fecha_nacimiento = DateTimeUtil.FechaActual(),
                    telefono = "",
                    id_tipo_actor = RestauranteSettings.Default.IdTipoActorRestauranteMesa,
                    id_foto = 1,
                    id_clase_actor = RestauranteSettings.Default.IdClaseActorRestauranteMesa,
                    id_estado_legal = 1,
                    correo = "",
                    tercer_nombre = "",
                    pagina_web = "",
                    informacion_multiproposito = null,
                    id_detalle_multiproposito = null,
                    id_detalle_multiproposito1 = null
                };
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public Actor_negocio GenerarActorDeNegocioDesdeMesa(Mesa mesa)
        {
            try
            {
                return new Actor_negocio()
                {
                    id_rol = RestauranteSettings.Default.IdRolMesa,
                    fecha_inicio = DateTimeUtil.FechaActual(),
                    fecha_fin = DateTimeUtil.FechaActual().AddYears(ActorSettings.Default.vigenciaEnAnyosPorDefectoDeActorDeNegocioEntidadInterna),
                    codigo_negocio = "",
                    es_vigente = true,
                    extension_json = "{nombre: \"" + mesa.Nombre + "\", fila: " + mesa.Fila + ", columna: " + mesa.Columna + "}",
                    indicador1 = mesa.EstadoOcupado,
                    id_detalle_maestro_multiproposito = null,
                    Actor = GenerarActorDesdeMesa(mesa),
                    id_actor_negocio_padre = mesa.IdAmbiente
                };
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public async Task<List<ItemRestaurante>> ObtenerItemsDeRestaurante()
        {
            try
            {
                var itemsRestaurante = (await _restauranteRepositorio.ObtenerItemsDeRestaurante()).ToList();
                return itemsRestaurante;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener los items de restaurante", e);
            }
        }
        public async Task<List<ItemRestaurante>> ObtenerItemsDeRestaurantePorCategoria(int idCategoria)
        {
            try
            {
                var categoriasPadreHijo = (await _restauranteRepositorio.ObtenerDetallesJerarquicos(MaestroSettings.Default.IdMaestroCategoriaConcepto)).ToList();
                List<ItemJerarquico> idsCategoriaABuscar = new List<ItemJerarquico>() { categoriasPadreHijo.Single(c => c.Id == idCategoria) };
                List<ItemJerarquico> idsCategoriaRepetir = new List<ItemJerarquico>() { categoriasPadreHijo.Single(c => c.Id == idCategoria) };
                do
                {
                    var idCategoriaActual = idsCategoriaRepetir.First();
                    idsCategoriaRepetir.AddRange(categoriasPadreHijo.Where(cph => cph.IdPadre == idCategoriaActual.Id));
                    idsCategoriaABuscar.AddRange(categoriasPadreHijo.Where(cph => cph.IdPadre == idCategoriaActual.Id));
                    idsCategoriaRepetir.Remove(idCategoriaActual);
                } while (idsCategoriaRepetir.Count() > 0);
                return _restauranteRepositorio.ObtenerItemsDeRestaurantePorCategorias(idsCategoriaABuscar.Select(c => c.Id).ToList()).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener los items de restaurante", e);
            }
        }
        public List<int> ObtenerIdsCategoriaHijo(List<ItemJerarquico> categoriasPadreHijo, long idCategoriaPadre)
        {
            try
            {
                return null;// .ToList(); ;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener Categorias", e);
            }
        }
        public long[] ObtenerIdsDeitemsPorCategoria(long idCategoria)
        {
            try
            {
                return _restauranteRepositorio.ObtenerIdsDeItemsDeRestaurantePorCategoria((int)idCategoria); ;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener Categorias", e);
            }
        }
        public ItemRestaurante ObtenerItemDeRestauranteIncluyendoComplementosDeFamilia(int idItem)
        {
            try
            {
                var item = _restauranteRepositorio.ObtenerItemDeRestauranteConPrecio(idItem);
                if (item != null)
                {
                    item.ComplementosDeFamilia = _restauranteRepositorio.ObtenerComplementosPorFamilia(item.IdFamilia).ToList();
                }
                return item;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener Item", e); ;
            }
        }
        public OperationResult ActualizarJsonDetalleDeDetalleOrden(long idDetalle, string stringJsonDetalle)
        {
            try
            {
                OperationResult result = new OperationResult();
                result = _restauranteRepositorio.ActualizarDetalleDeDetalleTransaccion(idDetalle, stringJsonDetalle);
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<SesionRestaurante> ObtenerSesion(UserProfileSessionData userProfileSessionData)
        {
            try
            {
                return new SesionRestaurante()
                {
                    SesionDeUsuario = userProfileSessionData,
                    Ambientes = await ObtenerAmbientes()
                };
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public OperationResult CrearAtencionConOrden(AtencionRestaurante atencion, SesionRestaurante sesion)
        {
            try
            {
                if (atencion.Ordenes.Count() <= 0)
                {
                    throw new LogicaException("No se puede guardar la atención debido a que no tiene órdenes");
                }
                foreach (var detalle in atencion.Ordenes.SelectMany(o => o.DetallesDeOrden))
                {
                    if (detalle.Importe == 0)
                    {
                        throw new LogicaException("No se puede guardar la atención, debido a que tiene un importe 0");
                    }
                }
                OperationResult result = new OperationResult();
                if (atencion.EsAtencionConMesa)
                {
                    Mesa mesa = new Mesa(_mesaRepositorio.ObtenerMesa(atencion.Mesa.Id));
                    if (mesa.EstadoOcupado)
                    {
                        throw new LogicaException("No se puede crear la atención debido a que la mesa indicada esta ocupada");
                    }
                    var ordenConAtencion = GenerarOrdenConAtencion(atencion, sesion);
                    //Marcar mesa como ocupada
                    Actor_negocio mesaActorNegocio = _actorRepositorio.obtenerActorDeNegocio(atencion.Mesa.Id, RestauranteSettings.Default.IdRolMesa);
                    mesaActorNegocio.indicador1 = true;
                    //Guardar la atencion y actualizar la mesa
                    result = GuardarAtencionYActualizarMesa(ordenConAtencion, mesaActorNegocio);
                    var orden = atencion.Ordenes = new List<Orden_Atencion>() { _restauranteRepositorio.ObtenerOrdenDeAtencionIncluidoDetallesDeOrdenItemsDeRestauranteYDetallesDeComplemento(ordenConAtencion.id) };
                    atencion.Id = ordenConAtencion.Transaccion2.id;
                    atencion.Estado = MaestroSettings.Default.IdDetalleMaestroEstadoRegistrado;
                    result.information = atencion;
                }
                else
                {
                    var mesaActorNegocio = GenerarActorDeNegocioDesdeMesa(new Mesa
                    {
                        Nombre = atencion.Cliente,
                        Columna = 0,
                        Fila = 0,
                        EstadoOcupado = true,
                        IdAmbiente = atencion.Mesa.IdAmbiente
                    });
                    var ordenConAtencion = GenerarOrdenConAtencion(atencion, sesion);
                    ordenConAtencion.Transaccion2.indicador1 = !atencion.EsAtencionConMesa;
                    ordenConAtencion.Transaccion2.indicador2 = atencion.EsAtencionPorDelivery;
                    //Guardar la atencion y tambien creamos la mesa
                    result = GuardarAtencionYCrearMesa(ordenConAtencion, mesaActorNegocio);
                    var orden = atencion.Ordenes = new List<Orden_Atencion>() { _restauranteRepositorio.ObtenerOrdenDeAtencionIncluidoDetallesDeOrdenItemsDeRestauranteYDetallesDeComplemento(ordenConAtencion.id) };
                    atencion.Id = ordenConAtencion.Transaccion2.id;
                    atencion.Mesa.Id = mesaActorNegocio.id;
                    atencion.Mesa.Nombre = mesaActorNegocio.Actor.primer_nombre;
                    atencion.Mesa.JsonData = mesaActorNegocio.extension_json;
                    atencion.Estado = MaestroSettings.Default.IdDetalleMaestroEstadoRegistrado;
                    result.information = atencion;
                }
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public Transaccion GenerarOrdenConAtencion(AtencionRestaurante atencion, SesionRestaurante sesion)
        {
            var fechaActual = DateTimeUtil.FechaActual();
            Transaccion atencionTransaccion = ConvertirAtencionEnTransaccion(atencion, sesion);
            atencionTransaccion.fecha_inicio = fechaActual;
            atencionTransaccion.fecha_fin = fechaActual;
            atencionTransaccion.fecha_registro_sistema = fechaActual;
            atencionTransaccion.fecha_registro_contable = fechaActual;
            //Agregar el estado registrado por defecto
            atencionTransaccion.Estado_transaccion.Add(new Estado_transaccion()
            {
                id_estado = MaestroSettings.Default.IdDetalleMaestroEstadoRegistrado,
                fecha = fechaActual,
                id_empleado = sesion.SesionDeUsuario.Empleado.Id,
                comentario = "Estado por defecto asignado el crear la atencion"
            });

            Transaccion ordenTransaccion = ConvertirOrdenATransaccion(atencion.Ordenes.ElementAtOrDefault(0), sesion);
            Comprobante comprobante = GenerarComprobante(sesion);
            ordenTransaccion.codigo = comprobante.numero_serie + " - " + comprobante.numero;
            ordenTransaccion.Comprobante = comprobante;
            ordenTransaccion.Transaccion2 = atencionTransaccion;
            ordenTransaccion.fecha_inicio = fechaActual;
            ordenTransaccion.fecha_fin = fechaActual;
            ordenTransaccion.fecha_registro_contable = fechaActual;
            ordenTransaccion.fecha_registro_sistema = fechaActual;
            return ordenTransaccion;
        }

        public OperationResult GuardarAtencionYActualizarMesa(Transaccion orden, Actor_negocio mesa)
        {
            OperationResult resultado = null;
            bool hayProblemaDeConcurrenciaSerieComprobante;
            do
            {
                try
                {
                    resultado = _transaccionRepositorio.CrearTransaccionYModificarActorDeNegocio(orden, mesa);
                }
                catch (SerieComprobanteException e)
                {
                    _transaccionRepositorio.RefreshEntity(e.serieComprobante);
                    //corregir el numero del comprobante
                    _operacionLogica.RegenerarNumeracionComprobantePropioAutonumerable(orden.Comprobante, e.serieComprobante);
                    hayProblemaDeConcurrenciaSerieComprobante = true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return resultado;
            }
            while (hayProblemaDeConcurrenciaSerieComprobante);
        }

        public OperationResult GuardarAtencionYCrearMesa(Transaccion orden, Actor_negocio mesa)
        {
            OperationResult resultado = null;
            bool hayProblemaDeConcurrenciaSerieComprobante;
            do
            {
                try
                {
                    resultado = _transaccionRepositorio.CrearTransaccionYCrearActorDeNegocio(orden, mesa);
                }
                catch (SerieComprobanteException e)
                {
                    _transaccionRepositorio.RefreshEntity(e.serieComprobante);
                    //Corregir el numero del comprobante
                    _operacionLogica.RegenerarNumeracionComprobantePropioAutonumerable(orden.Comprobante, e.serieComprobante);
                    hayProblemaDeConcurrenciaSerieComprobante = true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return resultado;
            }
            while (hayProblemaDeConcurrenciaSerieComprobante);
        }
        public OperationResult AgregarOrdenDeAtencion(Orden_Atencion orden, SesionRestaurante sesion)
        {
            try
            {
                AtencionRestaurante atencion = _atencionRepositorio.ObtenerAtencionConDatosMinimosDeOrdenYDetallesSoloParaCerrarla(orden.IdAtencion);
                if (atencion.Estado != MaestroSettings.Default.IdDetalleMaestroEstadoRegistrado)
                {
                    throw new LogicaException("No se puede agregar la orden a la atencion, debido a que la atencion no cuenta con el estado registrado");
                }
                foreach (var detalle in orden.DetallesDeOrden)
                {
                    if (detalle.Importe == 0)
                    {
                        throw new LogicaException("No se puede  agregar la orden a la atencion, debido a que tiene un importe 0");
                    }
                }
                Transaccion ordenTransaccion = ConvertirOrdenATransaccion(orden, sesion);
                OperationResult result = new OperationResult();
                bool hayProblemaDeConcurrenciaSerieComprobante;
                do
                {
                    hayProblemaDeConcurrenciaSerieComprobante = false;
                    try
                    {
                        var fechaActual = DateTimeUtil.FechaActual();
                        Modelo.Entidades.Comprobante comprobante = GenerarComprobante(sesion);
                        ordenTransaccion.codigo = comprobante.numero_serie + " - " + comprobante.numero;
                        ordenTransaccion.Comprobante = comprobante;
                        ordenTransaccion.id_transaccion_padre = orden.IdAtencion;
                        ordenTransaccion.fecha_inicio = fechaActual;
                        ordenTransaccion.fecha_fin = fechaActual;
                        ordenTransaccion.fecha_registro_contable = fechaActual;
                        ordenTransaccion.fecha_registro_sistema = fechaActual;
                        //Crear la transaccion movimiento
                        result = _transaccionRepositorio.CrearTransaccion(ordenTransaccion);
                        var result2 = _restauranteRepositorio.CalcularAtencion(orden.IdAtencion);
                    }
                    catch (SerieComprobanteException e)
                    {
                        _transaccionRepositorio.RefreshEntity(e.serieComprobante);
                        //corregir el numero del comprobante
                        _operacionLogica.RegenerarNumeracionComprobantePropioAutonumerable(ordenTransaccion.Comprobante, e.serieComprobante);
                        hayProblemaDeConcurrenciaSerieComprobante = true;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    orden = _restauranteRepositorio.ObtenerOrdenDeAtencionIncluidoDetallesDeOrdenItemsDeRestauranteYDetallesDeComplemento(ordenTransaccion.id);

                    result.information = orden;
                    return result;
                }
                while (hayProblemaDeConcurrenciaSerieComprobante);


            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar agregar Orden", e);
            }

        }



        public Modelo.Entidades.Comprobante GenerarComprobante(SesionRestaurante sesionRestaurante)
        {
            //Obtener la serie del comprobante
            Serie_comprobante serie = _transaccionRepositorio.ObtenerPrimeraSerieDeComprobantePorCentroDeAtencionYComprobante(RestauranteSettings.Default.IdDetalleMaestroTipoComprobanteOrden, sesionRestaurante.SesionDeUsuario.IdCentroDeAtencionSeleccionado);
            if (serie == null)
            {
                throw new LogicaException("No se ha encontrado una SERIE DE COMPROBANTE para este tipo de transaccion.");
            }
            //Crear el comprobante
            Modelo.Entidades.Comprobante comprobante = _operacionLogica.GenerarComprobantePropioAutonumerable(serie.id);
            return comprobante;
        }


        public Transaccion ConvertirAtencionEnTransaccion(AtencionRestaurante atencion, SesionRestaurante sesion)
        {

            Transaccion transaccion = new Transaccion()
            {
                codigo = "", // POR DEFECTO , DEBE CAMBIARSE
                id_comprobante = TransaccionSettings.Default.IdComprobanteGenerico, //CREAR COMPROBANTE
                tipo_cambio = sesion.SesionDeUsuario.TipoDeCambio.ValorVenta,
                importe_total = atencion.ImporteAtencion,
                id_actor_negocio_interno = sesion.IdCentroAtencion,
                id_actor_negocio_interno1 = atencion.Mesa.Id,
                id_actor_negocio_externo = ActorSettings.Default.IdClienteGenerico, // POR DECTO
                id_moneda = MaestroSettings.Default.IdDetalleMaestroMonedaSoles,
                id_empleado = sesion.SesionDeUsuario.Empleado.Id,
                id = atencion.Id,
                id_tipo_transaccion = RestauranteSettings.Default.IdTipoTransaccionAtencionDeRestaurante,
                comentario = atencion.TipoDePago.ToString(),
                id_unidad_negocio = MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal,
                es_concreta = false,
                enum1 = (int)TipoPago.Ninguno
            };

            return transaccion;
        }
        private Transaccion ConvertirOrdenATransaccion(Orden_Atencion orden, SesionRestaurante sesion)
        {
            Transaccion transaccion = new Transaccion()
            {
                codigo = orden.Codigo, //POR DEFECTO DEBE CAMBIARSE
                id_transaccion_padre = orden.IdAtencion,
                importe_total = orden.ImporteOrden,
                id = orden.Id,
                id_actor_negocio_interno = sesion.SesionDeUsuario.Empleado.TieneRol(ActorSettings.Default.IdRolCajero) ? orden.Mozo.Id : sesion.SesionDeUsuario.Empleado.Id,
                id_actor_negocio_externo = ActorSettings.Default.IdClienteGenerico, // POR DECTO
                id_empleado = sesion.SesionDeUsuario.Empleado.Id,
                id_estado_actual = orden.Estado,
                tipo_cambio = sesion.SesionDeUsuario.TipoDeCambio.ValorVenta,
                id_moneda = MaestroSettings.Default.IdDetalleMaestroMonedaSoles,
                Detalle_transaccion = orden.DetallesDeOrden.Select(ddo => ConvertirDetalleDeOrdenADetalleDeTransaccion(ddo)).ToList(),
                id_tipo_transaccion = RestauranteSettings.Default.IdTipoTransaccionOrdenDeRestaurante,
                id_unidad_negocio = MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal,
                es_concreta = false,
                comentario = ""
            };

            return transaccion;
        }
        private Detalle_transaccion ConvertirDetalleDeOrdenADetalleDeTransaccion(DetalleOrden detalleOrden)
        {
            Detalle_transaccion detalle = new Detalle_transaccion()
            {
                id = detalleOrden.Id,
                cantidad = detalleOrden.Cantidad,
                id_concepto_negocio = detalleOrden.IdItem,
                precio_unitario = detalleOrden.Precio,
                indicadorMultiproposito = detalleOrden.Estado,
                total = detalleOrden.Importe,
                detalle = detalleOrden.DetalleItemRestauranteJson,
                id_transaccion = detalleOrden.IdTransaccion
            };
            return detalle;
        }

        public OperationResult ActualizarMesa(Mesa mesa)
        {
            try
            {
                Mesa mesaActual = new Mesa(_mesaRepositorio.ObtenerMesa(mesa.Id));
                if (mesaActual.EstadoOcupado)
                {
                    throw new LogicaException("No se puede actualizar la mesa, debido a que la mesa indicada esta ocupada");
                }
                Actor_negocio ActorMesa = _actor_Repositorio.ObtenerActorDeNegocio(mesa.Id);

                ActorMesa.indicador1 = mesa.EstadoOcupado;
                ActorMesa.extension_json = "{ nombre: \"" + mesa.Nombre + "\", fila: " + mesa.Fila + ", columna:" + mesa.Columna + " }";
                ActorMesa.Actor.primer_nombre = mesa.Nombre;

                var result = _actor_Repositorio.ActualizarActorNegocioIncluidoActor(ActorMesa);
                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar actualizar la mesa", e);
            }
        }
        public OperationResult EliminarMesa(int idMesa)
        {
            try
            {
                Mesa mesa = new Mesa(_mesaRepositorio.ObtenerMesa(idMesa));
                if (mesa.EstadoOcupado)
                {
                    throw new LogicaException("No se puede eliminar la mesa, debido a que la mesa indicada esta ocupada");
                }
                Actor_negocio ActorMesa = _actor_Repositorio.ObtenerActorDeNegocio(idMesa);
                ActorMesa.es_vigente = false;
                ActorMesa.fecha_fin = DateTimeUtil.FechaActual();
                var result = _actor_Repositorio.ActualizarActorNegocioIncluidoActor(ActorMesa);
                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar eliminar la mesa", e);
            }
        }

        public OperationResult CambiarEstadoDeOrden(long idOrden, int estado)
        {
            try
            {

                OperationResult result = new OperationResult();
                Transaccion orden = _transaccionRepositorio.ObtenerTransaccion(idOrden);
                orden.id_estado_actual = estado;
                result = _transaccionRepositorio.ActualizarTransaccion(orden);
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public OperationResult ActualizarImportesDeTransaccion(List<ItemGenerico> nuevosImportes)
        {
            try
            {
                OperationResult result = new OperationResult();

                long[] Ids = (long[])nuevosImportes.Select(ni => (long)ni.Id);
                long[] Importes = (long[])nuevosImportes.Select(ni => long.Parse(ni.Valor));
                IEnumerable<Transaccion> transacciones = _transaccionRepositorio.ObtenerTransacciones(Ids).ToList();

                var count = 0;
                foreach (var transaccion in transacciones)
                {
                    transaccion.importe_total = Importes[count];
                    _transaccionRepositorio.ActualizarTransaccion(transaccion);
                    count++;
                }

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public OperationResult CerrarOrden(long idOrden)
        {
            try
            {
                Orden_Atencion ordenAtencion = _restauranteRepositorio.ObtenerOrdenDeAtencion(idOrden);
                if (ordenAtencion.Estado != MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                {
                    throw new LogicaException("No se puede cerrar la orden, debido a que no cuenta con el estado confirmado");
                }
                OperationResult result = new OperationResult();
                Transaccion orden = _transaccionRepositorio.ObtenerTransaccion(idOrden);
                orden.id_estado_actual = MaestroSettings.Default.IdDetalleMaestroEstadoCerrado;
                result = _transaccionRepositorio.ActualizarTransaccion(orden);
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public OperationResult CerrarAtencion(long idAtencion, SesionRestaurante sesion)
        {
            try
            {
                OperationResult result = new OperationResult();
                var fechaActual = DateTimeUtil.FechaActual();
                Transaccion transaccionAtencion = _transaccionRepositorio.ObtenerTransaccion(idAtencion);
                Actor_negocio actorNegocioMesa = null;
                if (transaccionAtencion.importe_total == 0)
                {
                    actorNegocioMesa = transaccionAtencion.Actor_negocio4;
                    actorNegocioMesa.indicador1 = false;
                }
                Estado_transaccion estadoCerrado = new Estado_transaccion()
                {
                    id_transaccion = idAtencion,
                    id_estado = MaestroSettings.Default.IdDetalleMaestroEstadoCerrado,
                    fecha = fechaActual,
                    id_empleado = sesion.SesionDeUsuario.Empleado.Id,
                    comentario = "Estado asignado al momento de cerrar la atencion"
                };
                result = actorNegocioMesa == null ? _transaccionRepositorio.CrearEstadoDeTransaccionAhora(estadoCerrado) : _transaccionRepositorio.CrearEstadoTransaccionActualizarActorNegocio(estadoCerrado, actorNegocioMesa);
                result.information = estadoCerrado.id_estado;
                result.objeto = actorNegocioMesa != null;
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }

        }



        public OperationResult AnularAtencion(long idAtencion)
        {
            try
            {
                OperationResult result = new OperationResult();
                Transaccion atencion = _transaccionRepositorio.ObtenerTransaccion(idAtencion);

                if (atencion != null)
                {
                    atencion.id_estado_actual = MaestroSettings.Default.IdDetalleMaestroEstadoAnulado;
                    //atencion.enum1 = (int)EstadosDeAtencion.Anulado;
                    atencion.Actor_negocio1.indicador1 = false;

                    result = _transaccionRepositorio.ActualizarTransaccion(atencion);
                }
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public OperationResult CambiarEstadoDeOrdenes(long[] ids, int estado)
        {
            try
            {
                OperationResult result = _restauranteRepositorio.ActualizarEstadoDeOrdenes(ids, estado);
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<ResumenOrden_Consulta> ObtenerReporteDeResumenDeOrdenes(DateTime Desde, DateTime Hasta)
        {
            List<ResumenOrden_Consulta> InfoReporte = _restauranteRepositorio.ObtenerReporteOrdenesIncluyendoMozo(Desde, Hasta).ToList();
            return InfoReporte;
        }

        public List<ResumenOrdenesPorMozo_Consulta> ObtenerReporteDeResumenDeMozos(DateTime desde, DateTime hasta)
        {
            List<ResumenOrdenesPorMozo_Consulta> InfoReporte = _restauranteRepositorio.ObtenerReporteMozoIncluyendoCantidadDeOrdenes(desde, hasta).ToList();
            return InfoReporte;
        }

        public List<ItemRestaurante_Consulta> ObtenerReporteDeItemsDeRestaurante(DateTime desde, DateTime hasta)
        {
            List<ItemRestaurante_Consulta> InfoReporte = _restauranteRepositorio.ObtenerOrdenesInclusiveItemsYDetallesDeOrden(desde, hasta).ToList();
            return InfoReporte;
        }
        public List<DetalleOrden_Consulta> ObtenerReporteDetallesAtendidosEnOrdenes(DateTime desde, DateTime hasta)
        {
            List<DetalleOrden_Consulta> InfoReporte = _restauranteRepositorio.ObtenerOrdenesDetalladasIncluyendoMozoyDetalleDeOrden(desde, hasta, EstadoDeDetalleDeOrden.Atendido).ToList();
            return InfoReporte;
        }

        public List<DetalleOrden_Consulta> ObtenerReporteDetallesDevueltosEnOrdenes(DateTime desde, DateTime hasta)
        {
            List<DetalleOrden_Consulta> InfoReporte = _restauranteRepositorio.ObtenerOrdenesDetalladasIncluyendoMozoyDetalleDeOrden(desde, hasta, EstadoDeDetalleDeOrden.Devuelto).ToList();
            return InfoReporte;
        }

        public AtencionRestaurante ObtenerAtencionEspecifica(long id)
        {
            AtencionRestaurante atencion = _restauranteRepositorio.ObtenerAtencionEspecifica(id);
            atencion.Comprobantes = _operacionLogica.ObtenerOrdenesDeVenta_SegunOperacionOrigen(id).ToList();
            return atencion;
        }

        public Dictionary<long, long> ObtenerIdsOrdenDeVentaDeAtencion(long idAtencion)
        {
            var idsTransaccion = _restauranteRepositorio.ObtenerIdsTransaccionDeTransaccionReferencia(idAtencion).ToArray();
            Dictionary<long, long> diccionario = new Dictionary<long, long>();
            for (int i = 0; i < idsTransaccion.Count(); i++)
            {
                diccionario.Add(i, idsTransaccion[i]);
            }
            return diccionario;
        }

        public List<Resumen_Transaccion_Venta_PorConceptoBasico_Y_ConceptoDeNegocio> ObtenerOrdenesDeVentaPorConceptoTransferidasYConfirmadas(int[] idsPuntosDeVenta, DateTime fechaDesde, DateTime fechaHasta)
        {
            var resultado = new List<Resumen_Transaccion_Venta_PorConceptoBasico_Y_ConceptoDeNegocio>();


            var resumenOrdenesEnLAsQueIngresaDineroYSalenBienes = _transaccionRepositorio.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstados(idsPuntosDeVenta, Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasDondeIngresaDineroYSalenBienes, fechaDesde, fechaHasta, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado).ToList();

            var resumenOrdenesEnLAsQueSaleDineroEIngresanBienes = _transaccionRepositorio.ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstados(idsPuntosDeVenta, Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasDondeSaleDineroEIngresanBienesExceptoInvalidaciones, MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado, fechaDesde, fechaHasta).ToList();

            //Hacer valores negativos
            foreach (var item in resumenOrdenesEnLAsQueSaleDineroEIngresanBienes)
            {
                item.Cantidad *= -1;
                item.Importe *= -1;
            }

            //conseguimos los ids de los conceptos de negocio comunes entre ambas colecciones
            int[] idsConceptosDeNegocioComunes = resumenOrdenesEnLAsQueIngresaDineroYSalenBienes.Select(o => o.IdConceptoNegocio).Intersect(resumenOrdenesEnLAsQueSaleDineroEIngresanBienes.Select(o => o.IdConceptoNegocio)).Distinct().ToArray();

            resultado.AddRange(resumenOrdenesEnLAsQueIngresaDineroYSalenBienes.Where(o => !idsConceptosDeNegocioComunes.Contains(o.IdConceptoNegocio)));

            resultado.AddRange(resumenOrdenesEnLAsQueSaleDineroEIngresanBienes.Where(o => !idsConceptosDeNegocioComunes.Contains(o.IdConceptoNegocio)));

            //Unificar los items comunes
            foreach (var idConceptoNegocio in idsConceptosDeNegocioComunes)
            {
                var itemPositivo = resumenOrdenesEnLAsQueIngresaDineroYSalenBienes.SingleOrDefault(o => o.IdConceptoNegocio == idConceptoNegocio);
                var itemNegativo = resumenOrdenesEnLAsQueSaleDineroEIngresanBienes.SingleOrDefault(o => o.IdConceptoNegocio == idConceptoNegocio);

                resultado.Add(new Resumen_Transaccion_Venta_PorConceptoBasico_Y_ConceptoDeNegocio() { IdConceptoNegocio = idConceptoNegocio, CodigoBarra = itemPositivo.CodigoBarra, ConceptoNegocio = itemPositivo.ConceptoNegocio, NombreBasico = itemPositivo.NombreBasico, Cantidad = itemPositivo.Cantidad + itemNegativo.Cantidad, Importe = itemPositivo.Importe + itemNegativo.Importe });
            }
            return resultado.OrderBy(r => r.CodigoBarra ?? r.ConceptoNegocio).ToList();
        }


        /// <summary>
        /// solo se consideran los dettalles de orden que no se encuentres anulados o devueltos
        /// </summary>
        /// <param name="atencion"></param>
        /// <param name="sesionRestaurante"></param>
        /// <returns></returns>
        public ComprobanteCuentaAtencion ObtenerComprobanteCuentaAtencion(long idAtencion)
        {
            var atencion = _restauranteRepositorio.ObtenerAtencionEspecifica(idAtencion);
            var idEstablecimiento = _mesaRepositorio.ObtenerIdEstablecimiento(atencion.Mesa.Id);
            var establecimiento = _establecimientoRepositorio.ObtenerEstablecimientoComercialComoItemGenerico(idEstablecimiento);
            var nombreEstablecimiento = establecimiento.Nombre;
            var ordenesFiltradas = atencion.Ordenes.ToList();
            ordenesFiltradas.ForEach(o => { o.DetallesDeOrden = o.DetallesDeOrden.Where(dor => dor.Estado != (int)EstadoDeDetalleDeOrden.Devuelto && dor.Estado != (int)EstadoDeDetalleDeOrden.Anulado).ToList(); });
            ordenesFiltradas = ordenesFiltradas.Where(o => o.DetallesDeOrden.Count() > 0).ToList();
            atencion.Ordenes = ordenesFiltradas;
            AgruparDetallesPorIdPrecioUnitario(atencion);
            return new ComprobanteCuentaAtencion(atencion, nombreEstablecimiento, DateTimeUtil.FechaActual());
        }

        private void AgruparDetallesPorIdPrecioUnitario(AtencionRestaurante atencion)
        {
            try
            {
                foreach (var orden in atencion.Ordenes)
                {
                    var nuevosDetalles = new List<DetalleOrden>();
                    var detallesOrdenAgrupados = orden.DetallesDeOrden.GroupBy(d => new { d.IdItem, d.Precio });
                    foreach (var grupo in detallesOrdenAgrupados)
                    {
                        nuevosDetalles.Add(new DetalleOrden()
                        {
                            Id = grupo.Key.IdItem,
                            Cantidad = grupo.Sum(g => g.Cantidad),
                            Precio = grupo.Key.Precio,
                            Importe = grupo.Sum(g => g.Importe),
                            NombreItem = grupo.First().NombreItem
                        });
                    }
                    orden.DetallesDeOrden = nuevosDetalles;
                }
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar determinar los detalles de venta", e);
            }
        }
        public ComprobanteOrden ObtenerComprobanteOrdenSinItemsAnulados(long idOrden)
        {
            var orden = _restauranteRepositorio.ObtenerOrdenDeAtencionIncluidoDetallesDeOrdenItemsDeRestauranteYDetallesDeComplemento(idOrden);
            var detallesSinAnulados = orden.DetallesDeOrden.ToList().Where(d => d.Estado != (int)EstadoDeDetalleDeOrden.Anulado);
            orden.DetallesDeOrden = detallesSinAnulados;
            orden.Mesa = new Mesa(_mesaRepositorio.ObtenerMesa(orden.IdMesa));
            var idEstablecimiento = _mesaRepositorio.ObtenerIdEstablecimiento(orden.IdMesa);
            var establecimiento = _establecimientoRepositorio.ObtenerEstablecimientoComercialComoItemGenerico(idEstablecimiento);
            var nombreEstablecimiento = establecimiento.Nombre;
            return new ComprobanteOrden(orden, nombreEstablecimiento);
        }

        public List<Complemento> ObtenerComplementosDeFamilia(int idFamilia)
        {
            try
            {
                List<Complemento> complementos = _restauranteRepositorio.ObtenerComplementosPorFamilia(idFamilia).ToList();
                return complementos;

            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener complementos de familia", e);
            }
        }

        public List<Complemento> ObtenerComplementos()
        {
            try
            {
                List<Complemento> complementos = _restauranteRepositorio.ObtenerComplementos().ToList();
                complementos.OrderBy(c => c.Familia).ThenBy(c => c.Nombre);
                List<Complemento> complementosResult = new List<Complemento>();
                foreach (var complemento in complementos)
                {
                    complemento.MostrarFamilia = !complementosResult.Select(c => c.Familia).Contains(complemento.Familia);
                    complemento.NumeroComplementos = complementos.Where(c => c.Familia == complemento.Familia).Count();
                    complementosResult.Add(complemento);
                }
                return complementosResult;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener complementos de familia", e);
            }
        }
        public OperationResult ActualizarComplemento(Complemento complemento)
        {
            try
            {
                return _restauranteRepositorio.ActualizarComplemento(complemento);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar actualizar el complemento", e);
            }
        }
    }
}



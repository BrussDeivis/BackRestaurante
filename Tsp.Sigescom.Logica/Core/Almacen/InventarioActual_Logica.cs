using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Actores;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Almacen;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Transacciones;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio.Conceptos;
using Tsp.Sigescom.Modelo.Negocio.Almacen;

namespace Tsp.Sigescom.Logica.Core.Almacen
{
    public class InventarioActual_Logica: IInventarioActual_Logica
    {
        protected readonly IInventarioActualRepositorio _inventarioActualDatos;
        protected readonly IMaestrosAlmacen_Repositorio _maestrosAlmacenDatos;

        protected readonly IPermisos_Logica _permisosLogica;
        protected readonly ICodigosOperacion_Logica _codigosOperacionLogica;

        protected readonly ICrearTransaccion_Repositorio _crearTranaccionDatos;

        protected readonly IConcepto_Repositorio _conceptoDatos;
        protected readonly IActor_Repositorio _actorDatos;

        protected readonly ICrearDetalleTransaccion_Repositorio _crearDetallesTransaccionDatos;
        protected readonly IConsultaDetalleTransaccion_Repositorio _consultaDetallesTransaccionDatos;
        protected readonly IEliminarDetalleTransaccion_Repositorio _eliminarDetallesTransaccionDatos;




        public InventarioActual_Logica(IInventarioActualRepositorio inventarioActualDatos, IPermisos_Logica permiso_logica, ICodigosOperacion_Logica codigosOperacionLogica, IMaestrosAlmacen_Repositorio maestrosAlmacenDatos, ICrearTransaccion_Repositorio crearTranaccionDatos, IConcepto_Repositorio conceptoDatos, IActor_Repositorio actorDatos, ICrearDetalleTransaccion_Repositorio crearDetallesTransaccionDatos, IConsultaDetalleTransaccion_Repositorio consultaDetallesTransaccionDatos, IEliminarDetalleTransaccion_Repositorio eliminarDetallesTransaccionDatos)
        {
            _inventarioActualDatos = inventarioActualDatos;
            _permisosLogica = permiso_logica;
            _codigosOperacionLogica = codigosOperacionLogica;
            _maestrosAlmacenDatos = maestrosAlmacenDatos;
            _crearTranaccionDatos = crearTranaccionDatos;
            _conceptoDatos = conceptoDatos;
            _actorDatos = actorDatos;
            _crearDetallesTransaccionDatos = crearDetallesTransaccionDatos;
            _consultaDetallesTransaccionDatos = consultaDetallesTransaccionDatos;
            _eliminarDetallesTransaccionDatos = eliminarDetallesTransaccionDatos;
        }


        public Transaccion GenerarInventarioActual(int idEmpleado, int idCentroDeAtencion)
        {
            try
            {
                Operacion operacionInventarioActual = new Operacion(_inventarioActualDatos.ObtenerUltimaOperacionInventarioActual());
                var fechaActual = DateTimeUtil.FechaActual();
                Transaccion inventarioFisico = CrearInventarioActual(operacionInventarioActual.Id, operacionInventarioActual.IdComprobante, idEmpleado, fechaActual, "", idCentroDeAtencion);
                return inventarioFisico;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al generar operacion inventario actual", e);
            }
        }

        //Crear inventario fisico
        public Transaccion CrearInventarioActual(long idOperacionInventarioActual, long idComprobante, int idEmpleado, DateTime fechaRegistro, string observacion, int idCentroDeAtencion)
        {
            try
            {
                var sufijoCodigo = "IF";
                var accionARealizar = MaestroSettings.Default.IdDetalleMaestroAccionConfirmar;
                int idMoneda = MaestroSettings.Default.IdDetalleMaestroMonedaSoles;
                int idUnidadNegocio = MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal;
                decimal tipoDeCambio = 1;
                //Validamos la accion a realizar
                _permisosLogica.ValidarAccion(idEmpleado, accionARealizar, TransaccionSettings.Default.IdTipoTransaccionInventarioActual, idUnidadNegocio);
                //obtenemos el codigo
                string codigo = _codigosOperacionLogica.ObtenerSiguienteCodigoParaFacturacion(sufijoCodigo, TransaccionSettings.Default.IdTipoTransaccionInventarioActual);
                //Crear transaccion inventario fisico
                Transaccion inventarioFisico = new Transaccion(codigo, idOperacionInventarioActual, fechaRegistro, TransaccionSettings.Default.IdTipoTransaccionInventarioActual, idUnidadNegocio, true, fechaRegistro, fechaRegistro, observacion, fechaRegistro, idEmpleado, 0, idCentroDeAtencion, idMoneda, tipoDeCambio, null, idCentroDeAtencion);
                inventarioFisico.id_comprobante = idComprobante;
                Estado_transaccion estado = new Estado_transaccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaRegistro, "Estado confirmado al crear la transaccion inventario actual");
                inventarioFisico.Estado_transaccion.Add(estado);

                return inventarioFisico;

            }
            catch (Exception e)
            {
                throw new LogicaException("Error al crear inventario actual", e);
            }
        }

        public List<Detalle_transaccion> ResolverYCrearDetallesDeInventarioActual(int idCentroDeAtencion)
        {
            try
            {
                List<Detalle_transaccion> detalles = new List<Detalle_transaccion>();
                //Obtener los detalles de inventafio fisico, se usaran para descartar los conceptos de negocio que ya se registraron
                List<Detalle_transaccion> detallesInventarioFisico = _inventarioActualDatos.ObtenerDetallesInventarioActualIncluyendoConceptoNegocio(idCentroDeAtencion).ToList();
                //Verificar si es necesario llamar a los conceptos ya que cuando se agrega un concepto negocio se van a actualizar los inventarios fisicos
                var conceptosDeNegocioActual = _maestrosAlmacenDatos.ObtenerIdsConceptosComerciales();
                List<int> idConceptosAAgregar = detallesInventarioFisico != null ? conceptosDeNegocioActual.Except(detallesInventarioFisico.Select(dt => dt.id_concepto_negocio)).ToList() : conceptosDeNegocioActual.ToList();
                //Cuando se compre o se vende afectar lote, y cantidad
                //
                foreach (var idConcepto in idConceptosAAgregar)
                {
                    detalles.Add(new Detalle_transaccion(0, idConcepto, "Inventario Actual", 0m, 0m, null, 0, null, null, 0m, 0m, 0m));
                }
                return detalles;
            }
            catch (Exception)
            {
                throw new LogicaException("Error al resolver y crear detalles de inventario actual");
            }
        }


        public List<InventarioFisico> InventariosFisicosActuales(List<ItemGenericoBase> almacenes, bool todasLasFamilias, int[] idsFamilias)
        {
            try
            {
                var idsAlmacenes = almacenes.Select(a => a.Id).ToArray();
                idsFamilias = todasLasFamilias ? _maestrosAlmacenDatos.ObtenerFamiliasBienes().Select(f => f.Id).ToArray() : idsFamilias;
                var inventarios = _inventarioActualDatos.ObtenerInventariosFisicosActuales(idsAlmacenes, idsFamilias).ToList();
                //var inventarios = (todasLasFamilias ? _inventarioActualDatos.ObtenerInventariosFisicosActuales(idsAlmacenes) : _inventarioActualDatos.ObtenerInventariosFisicosActuales(idsAlmacenes, idsFamilias)).ToList();
                inventarios.ForEach(i => i.NombreAlmacen = almacenes.Single(a => a.Id == i.IdAlmacen).Nombre);
                return inventarios;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener Inventario actual", e);
            }

        }

        public List<InventarioSemaforo> InventariosSemaforoActuales(List<ItemGenericoBase> almacenes, bool todasLasFamilias, int[] idsFamilias, bool estadoBajo, bool estadoNormal, bool estadoAlto)
        {
            try
            {
                var idsAlmacenes = almacenes.Select(a => a.Id).ToArray();
                List<int> nivelesRequeridos = new List<int>();
                nivelesRequeridos.Add((int)NivelStockSemaforoEnum.Indeterminado);
                if (estadoAlto) nivelesRequeridos.Add((int)NivelStockSemaforoEnum.Alto);
                if (estadoBajo) nivelesRequeridos.Add((int)NivelStockSemaforoEnum.Bajo);
                if (estadoNormal) nivelesRequeridos.Add((int)NivelStockSemaforoEnum.Normal);
                idsFamilias = todasLasFamilias ? _maestrosAlmacenDatos.ObtenerFamiliasBienes().Select(f => f.Id).ToArray() : idsFamilias;
                var inventarios = _inventarioActualDatos.ObtenerInventariosSemaforoActuales(idsAlmacenes, idsFamilias).ToList();
                //var inventarios = (todasLasFamilias ? _inventarioActualDatos.ObtenerInventariosSemaforoActuales(idsAlmacenes) : _inventarioActualDatos.ObtenerInventariosSemaforoActuales(idsAlmacenes, idsFamilias)).ToList();
                inventarios = inventarios.Where(i => nivelesRequeridos.Contains(i.ValorSemaforoInt)).OrderBy(i => i.Concepto).ToList();
                inventarios.ForEach(i => i.NombreAlmacen = almacenes.Single(a => a.Id == i.IdAlmacen).Nombre);
                return inventarios;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener Inventarios actuales - Semáforo", e);
            }
        }
        public List<InventarioValorizado> InventariosValorizadosActuales(List<ItemGenericoBase> almacenes, bool todasLasFamilias, int[] idsFamilias)
        {
            try
            {
                var idsAlmacenes = almacenes.Select(a => a.Id).ToArray();
                List<int> nivelesRequeridos = new List<int>();
                idsFamilias = todasLasFamilias ? _maestrosAlmacenDatos.ObtenerFamiliasBienes().Select(f => f.Id).ToArray() : idsFamilias;
                var inventarios = _inventarioActualDatos.ObtenerInventariosValorizadosActuales(idsAlmacenes, idsFamilias).ToList();
                //var inventarios = (todasLasFamilias ? _inventarioActualDatos.ObtenerInventariosValorizadosActuales(idsAlmacenes) : _inventarioActualDatos.ObtenerInventariosValorizadosActuales(idsAlmacenes, idsFamilias)).ToList();
                inventarios.ForEach(i => i.NombreAlmacen = almacenes.Single(a => a.Id == i.IdAlmacen).Nombre);
                return inventarios;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener Inventarios actuales", e);
            }
        }

        public List<Reporte_Inventario_Valorizado> ObtenerInventarioValorizadoActual(int idAlmacen, int idCentroAtencionPrecios, int[] idsConceptosBasicos, int[] idsValoresDeCaracteristicas)
        {
            try

            {
                if (idsConceptosBasicos.Length == 0 && idsValoresDeCaracteristicas.Length == 0)
                {
                    return _inventarioActualDatos.ObtenerInventarioActual(idAlmacen, idCentroAtencionPrecios).ToList();
                }

                if (idsConceptosBasicos.Length > 0 && idsValoresDeCaracteristicas.Length > 0)
                {
                    return _inventarioActualDatos.ObtenerInventarioActualPorCaracteristicasYFamilias(idAlmacen, idCentroAtencionPrecios, idsValoresDeCaracteristicas, idsConceptosBasicos).ToList();
                }

                if (idsConceptosBasicos.Length > 0 && idsValoresDeCaracteristicas.Length == 0)
                {
                    return _inventarioActualDatos.ObtenerInventarioActualPorFamilias(idAlmacen, idCentroAtencionPrecios, idsConceptosBasicos).ToList();
                }


                if (idsConceptosBasicos.Length == 0 && idsValoresDeCaracteristicas.Length > 0)
                {
                    return _inventarioActualDatos.ObtenerInventarioActualPorCaracteristicas(idAlmacen, idCentroAtencionPrecios, idsValoresDeCaracteristicas).ToList();
                }

                throw new LogicaException("Ingrese al menos una caracteristica o un concepto basico");

            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener inventario valorizado", e);
            }
        }

        public OperationResult CrearInventarioActual(int idAlmacen, int idEmpleado)
        {
            List<Detalle_transaccion> detalles;
            OperationResult resultInventarioFisico = null;

            detalles = ResolverYCrearDetallesDeInventarioActual(idAlmacen);
            if (detalles.Count() > 0)
            {
                Transaccion inventarioFisico = GenerarInventarioActual(idEmpleado, idAlmacen);
                resultInventarioFisico = _crearTranaccionDatos.CrearTransaccionYDetallesTransaccionMasivo(inventarioFisico, detalles);
            }
            return resultInventarioFisico;
        }

        public OperationResult CrearDetallesInventariosActualesDeConceptosDeNegocioDelConceptoBasico(int idFamilia)
        {
            List<Detalle_transaccion> detalles = new List<Detalle_transaccion>();
            //Obtener todos los conceptos de negocio que pertenezcan al concepto basico
            List<int> idsConceptos = _conceptoDatos.ObtenerConceptosComercialesPorFamilia(idFamilia).Select(c => c.id).ToList();
            //Obtener los ids de los almacenes y de los inventarios 
            Dictionary<long, long> idsAlmacenIdsInventarioFisico = _inventarioActualDatos.ObtenerIdsInventarioActualPorAlmacen();
            //Obtener los ids de los centros de atencion almacenes 
            List<int> idsAlmacenes = _actorDatos.ObtenerIdsActorDeNegocioVigentePrincipal(ActorSettings.Default.IdRolEntidadInterna, ActorSettings.Default.IdRolAlmacen).ToList();
            foreach (var idAlmacen in idsAlmacenes)
            {
                //Obtener el id del inventario fisico del centro de atencion
                List<Detalle_transaccion> detallesDeInventario = _consultaDetallesTransaccionDatos.ObtenerDetalleTransaccion(idAlmacen, TransaccionSettings.Default.IdTipoTransaccionInventarioActual, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, idsConceptos.ToArray()).ToList();
                //Obtener los ids de conceptos de negocio a agregar, que no esten en el inventario  
                List<int> idConceptosAAgregar = detallesDeInventario != null ? idsConceptos.Except(detallesDeInventario.Select(e => e.id_concepto_negocio)).ToList() : idsConceptos.ToList();
                foreach (var idConceptoAAgregar in idConceptosAAgregar)
                {
                    detalles.Add(new Detalle_transaccion(idsAlmacenIdsInventarioFisico.Single(i => i.Key == idAlmacen).Value, 0, idConceptoAAgregar, "Inventario Físico", 0m, 0m, null, 0m, 0, null, 0m, 0m, 0m));
                }
            }
            return _crearDetallesTransaccionDatos.CrearDetallesDeTransaccionMasivo(detalles);
        }

        public OperationResult EliminarDetallesInventariosActualesDeConceptosDeNegocioDelConceptoBasico(int idFamilia)
        {
            List<Detalle_transaccion> detalles = new List<Detalle_transaccion>();
            //Obtener todos los conceptos de negocio que pertenezcan al concepto basico
            List<int> idsConceptos = _conceptoDatos.ObtenerConceptosComercialesPorFamilia(idFamilia).Select(c => c.id).ToList();
            //Obtener los ids de los centros de atencion almacenes 
            List<int> idsAlmacenes = _actorDatos.ObtenerIdsActorDeNegocioVigentePrincipal(ActorSettings.Default.IdRolEntidadInterna, ActorSettings.Default.IdRolAlmacen).ToList();
            foreach (var idAlmacen in idsAlmacenes)
            {
                //Obtener el id del inventario fisico del centro de atencion
                List<Detalle_transaccion> detallesDeInventario = _consultaDetallesTransaccionDatos.ObtenerDetalleTransaccion(idAlmacen, TransaccionSettings.Default.IdTipoTransaccionInventarioActual, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, idsConceptos.ToArray()).ToList();
                //Agregar los detalles del inventario del almacen de los conceptos a eliminarse
                detalles.AddRange(detallesDeInventario);
            }
            return _eliminarDetallesTransaccionDatos.EliminarDetallesDeTransaccionMasivo(detalles);
        }

        public Dictionary<long, long> ObtenerIdsAlmacenIdsInventarioActual()
        {
            try
            {
                return _inventarioActualDatos.ObtenerIdsInventarioActualPorAlmacen();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener los ids de almacen e ids de inventario actual", e);
            }
        }

    }
}

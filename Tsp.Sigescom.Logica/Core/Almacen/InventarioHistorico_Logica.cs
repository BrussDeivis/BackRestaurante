using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Concepto;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Almacen;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Transacciones;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio.Conceptos;
using Tsp.Sigescom.Modelo.Negocio.Almacen;

namespace Tsp.Sigescom.Logica.Core.Almacen
{
    public class InventarioHistorico_Logica: IInventarioHistorico_Logica
    {
        protected readonly IInventarioRepositorio _almacenReportingDatos;
        protected readonly IInventarioHistorico_Repositorio _inventarioHistoricoDatos;
        protected readonly IMovimientos_Repositorio _movimientosDatos;
        protected readonly IConcepto_Repositorio _conceptoDatos;
        protected readonly IConsultaTransaccion_Repositorio _consultaTransaccionDatos;
        protected readonly IMaestrosAlmacen_Repositorio _maestrosAlmacenDatos;

        protected readonly ICrearTransaccion_Repositorio _crearTransaccionDatos;


        public InventarioHistorico_Logica(IInventarioRepositorio almacenReportingDatos, IInventarioHistorico_Repositorio inventarioHistoricoDatos, IMovimientos_Repositorio movimientosDatos, IConcepto_Repositorio conceptoDatos, IConsultaTransaccion_Repositorio consultaTransaccionDatos, IMaestrosAlmacen_Repositorio maestrosAlmacenDatos, ICrearTransaccion_Repositorio crearTransaccionDatos)
        {
            _almacenReportingDatos = almacenReportingDatos;
            _inventarioHistoricoDatos = inventarioHistoricoDatos;
            _movimientosDatos = movimientosDatos;
            _conceptoDatos = conceptoDatos;
            _consultaTransaccionDatos = consultaTransaccionDatos;
            _maestrosAlmacenDatos = maestrosAlmacenDatos;
            _crearTransaccionDatos = crearTransaccionDatos;
        }
        public OperationResult CrearInventariosLogicosHoy(int idEmpleado)
        {
            DateTime fechaActual = DateTimeUtil.FechaActual();
            return CrearInventarios(idEmpleado, fechaActual);

        }

        public OperationResult CrearInventarios(int idEmpleado, DateTime fecha)
        {
            try
            {
                var nuevosInventarios = GenerarInventarios(idEmpleado, fecha);
                if (nuevosInventarios.Count() > 0)
                {
                    return _crearTransaccionDatos.CrearTransaccionesYDetallesTransaccionMasivo(nuevosInventarios);
                }
                throw new LogicaException("No se pudo generar el inventario histórico porque no hubo movimientos de entrada o salida");
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al generar inventario histórico", e);
            }
        }

        public OperationResult CrearInventario(int idEmpleado, int idAlmacen, DateTime fecha)
        {
            try
            {
                var nuevoInventario = GenerarInventario(idAlmacen, idEmpleado, fecha);

                var resultado = _crearTransaccionDatos.CrearTransaccionesYDetallesTransaccionMasivo(new List<Transaccion>() { nuevoInventario });
                resultado.information = nuevoInventario;
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener inventario histórico. " + "\n " + e.Message, e);
            }
        }

        public List<InventarioFisico> ObtenerInventariosFisicos( int idAlmacen, int idEmpleado, DateTime fecha)
        {
            try
            {
                var inventario = GenerarInventario(idAlmacen, idEmpleado, fecha);
                return ConvertToInventarioFisico(inventario);
            }
            catch (NohayMovimientosDeBienesException e)
            {
                List<InventarioFisico> inventario = new List<InventarioFisico>();
                inventario = (e.HayInventarioPrevio ? _almacenReportingDatos.ObtenerInventarioFisico((long)e.IdUltimoInventario).ToList() : throw new LogicaException("No se cuenta con un inventario a la fecha solicitada por que no existen operaciones de entrada/salida de bienes. " + "\n" + e.Message, e));
                return inventario;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener inventario histórico. " + "\n " + e.Message, e);
            }
        }

        public List<InventarioSemaforo> ObtenerInventariosSemaforo(int idEmpleado, int idAlmacen, DateTime fecha)
        {
            try
            {
                var inventario = GenerarInventario(idAlmacen, idEmpleado, fecha);
                return ConvertToInventarioSemaforo(inventario);
            }
            catch (NohayMovimientosDeBienesException e)
            {
                List<InventarioSemaforo> inventario = new List<InventarioSemaforo>();
                inventario = (e.HayInventarioPrevio ? _almacenReportingDatos.ObtenerInventarioSemaforo((long)e.IdUltimoInventario).ToList() : throw new LogicaException("No se cuenta con un inventario a la fecha solicitada por que no existen operaciones de entrada/salida de bienes. " + "\n" + e.Message, e));
                return inventario;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener inventario histórico. " + "\n " + e.Message, e);
            }
        }

        public List<InventarioValorizado> ObtenerInventariosValorizados(int idEmpleado, int idAlmacen, DateTime fecha)
        {
            try
            {
                var inventario = GenerarInventario(idAlmacen, idEmpleado, fecha);

                return ConvertToInventarioValorizado(inventario);
            }
            catch (NohayMovimientosDeBienesException e)
            {
                List<InventarioValorizado> inventario = new List<InventarioValorizado>();
                inventario = (e.HayInventarioPrevio ? _almacenReportingDatos.ObtenerInventarioValorizado((long)e.IdUltimoInventario).ToList() : throw new LogicaException("No se cuenta con un inventario a la fecha solicitada por que no existen operaciones de entrada/salida de bienes. " + "\n" + e.Message, e));
                return inventario;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener inventario histórico. " + "\n " + e.Message, e);
            }
        }

        public List<InventarioFisico> ObtenerInventariosFisicos(int idAlmacen, int idEmpleado,  DateTime fecha, int[] familias)
        {
            try
            {
                var inventario = GenerarInventario(idAlmacen, idEmpleado, fecha, familias);
                return ConvertToInventarioFisico(inventario);
            }
            catch (NohayMovimientosDeBienesException e)
            {
                List<InventarioFisico> inventario = new List<InventarioFisico>();
                inventario = (e.HayInventarioPrevio ? _almacenReportingDatos.ObtenerInventarioFisico((long)e.IdUltimoInventario, familias).ToList() : throw new LogicaException("No se cuenta con un inventario a la fecha solicitada por que no existen operaciones de entrada/salida de bienes. " + "\n" + e.Message, e));
                return inventario;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener inventario histórico. " + "\n " + e.Message, e);
            }
        }

        public List<InventarioValorizado> ObtenerInventariosValorizados(int idEmpleado, int idAlmacen, DateTime fecha, int[] familias)
        {
            try
            {
                var inventario = GenerarInventario(idAlmacen, idEmpleado, fecha, familias);
                return ConvertToInventarioValorizado(inventario);
            }
            catch (NohayMovimientosDeBienesException e)
            {
                List<InventarioValorizado> inventario = new List<InventarioValorizado>();
                inventario = (e.HayInventarioPrevio ? _almacenReportingDatos.ObtenerInventarioValorizado((long)e.IdUltimoInventario, familias).ToList() : throw new LogicaException("No se cuenta con un inventario a la fecha solicitada por que no existen operaciones de entrada/salida de bienes. " + "\n" + e.Message, e));
                return inventario;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener inventario histórico. " + "\n " + e.Message, e);
            }
        }



        public List<InventarioSemaforo> ObtenerInventariosSemaforo(int idEmpleado, int idAlmacen, DateTime fecha, int[] familias)
        {
            try
            {
                var inventario = GenerarInventario(idAlmacen, idEmpleado, fecha, familias);
                return ConvertToInventarioSemaforo(inventario);
            }
            catch (NohayMovimientosDeBienesException e)
            {
                List<InventarioSemaforo> inventario = new List<InventarioSemaforo>();
                inventario = (e.HayInventarioPrevio ? _almacenReportingDatos.ObtenerInventarioSemaforo((long)e.IdUltimoInventario, familias).ToList() : throw new LogicaException("No se cuenta con un inventario a la fecha solicitada por que no existen operaciones de entrada/salida de bienes. " + "\n" + e.Message, e));
                return inventario;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener inventario histórico. "+ "\n "+ e.Message, e);
            }
        }



        private List<InventarioFisico> ConvertToInventarioFisico(Transaccion inventarioTransaccion)
        {
            List<InventarioFisico> inventarios = new List<InventarioFisico>();
            List<Concepto> conceptos = _conceptoDatos.ObtenerConceptos(inventarioTransaccion.Detalle_transaccion.Select(dt => dt.id_concepto_negocio).Distinct().ToArray()).ToList();
            inventarios = inventarioTransaccion.Detalle_transaccion.Select(dt => new InventarioFisico()
            {
                CodigoBarra = conceptos.Single(c => c.Id == dt.id_concepto_negocio).CodigoBarra,
                Concepto = conceptos.Single(c => c.Id == dt.id_concepto_negocio).Nombre,
                Cantidad = dt.cantidad,
                Familia = conceptos.Single(c => c.Id == dt.id_concepto_negocio).Familia,
                Lote = dt.lote,
                UnidadMedida = conceptos.Single(c => c.Id == dt.id_concepto_negocio).UnidadMedida,
            }
            ).ToList();
            return inventarios;

        }

        private List<InventarioValorizado> ConvertToInventarioValorizado(Transaccion inventarioTransaccion)
        {
            List<InventarioValorizado> inventarios = new List<InventarioValorizado>();
            //conseguir conceptos
            List<Concepto> conceptos = _conceptoDatos.ObtenerConceptos(inventarioTransaccion.Detalle_transaccion.Select(dt=> dt.id_concepto_negocio).Distinct().ToArray()).ToList();
            inventarios = inventarioTransaccion.Detalle_transaccion.Select(dt => new InventarioValorizado()
            {
                CodigoBarra = conceptos.Single(c => c.Id == dt.id_concepto_negocio).CodigoBarra,
                Concepto = conceptos.Single(c=>c.Id== dt.id_concepto_negocio).Nombre,
                Cantidad = dt.cantidad,
                Familia = conceptos.Single(c => c.Id == dt.id_concepto_negocio).Familia,
                Lote = dt.lote,
                UnidadMedida = conceptos.Single(c => c.Id == dt.id_concepto_negocio).UnidadMedida,
                ValorTotal = dt.total,
                ValorUnitario = dt.precio_unitario
            }
            ).ToList();
            return inventarios;
        }

        private List<InventarioSemaforo> ConvertToInventarioSemaforo(Transaccion inventarioTransaccion)
        {
            List<InventarioSemaforo> inventarios = new List<InventarioSemaforo>();
            List<Concepto> conceptos = _conceptoDatos.ObtenerConceptos(inventarioTransaccion.Detalle_transaccion.Select(dt => dt.id_concepto_negocio).Distinct().ToArray()).ToList();
            inventarios = inventarioTransaccion.Detalle_transaccion.Select(dt => new InventarioSemaforo()
            {
                CodigoBarra = conceptos.Single(c => c.Id == dt.id_concepto_negocio).CodigoBarra,
                Concepto = conceptos.Single(c => c.Id == dt.id_concepto_negocio).Nombre,
                Cantidad = dt.cantidad,
                Familia = conceptos.Single(c => c.Id == dt.id_concepto_negocio).Familia,
                Lote = dt.lote,
                UnidadMedida = conceptos.Single(c => c.Id == dt.id_concepto_negocio).UnidadMedida,
                StockMinimo = conceptos.Single(c => c.Id == dt.id_concepto_negocio).StockMinimo

            }
            ).ToList();
            return inventarios;

        }


        public List<Transaccion> GenerarInventarios(int idEmpleado, DateTime fecha)
        {
            try
            {
                List<Transaccion> nuevosInventarios = new List<Transaccion>();
                ///Obtener ids de centros de atención con rol de almacén.
                int[] idsAlmacenes = _maestrosAlmacenDatos.ObtenerIdsAlmacenes();
                foreach (var idAlmacen in idsAlmacenes)
                {
                    var nuevoInventario = GenerarInventario(idAlmacen, idEmpleado, fecha);
                    nuevosInventarios.Add(nuevoInventario);
                }
                return nuevosInventarios;
            }
            catch (NohayMovimientosDeBienesException e)
            {
                throw (e);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al generar inventario histórico", e);
            }
        }

        public Transaccion GenerarInventario(int idAlmacen, int idEmpleado, DateTime fecha)
        {
            return GenerarInventario(idAlmacen, idEmpleado, fecha, null);

        }

        private Transaccion GenerarInventario(int idAlmacen, int idEmpleado, DateTime fecha, int[] familias)
        {
            try
            {
                Transaccion nuevoInventario = null;
                DateTime? fechaPrimeraTransaccion = fecha;
                ///Obtener ultimoInventarioLogico 
                /////todo:tratar de traer objeto inventario...
                var ultimoInventario = (familias !=null? _inventarioHistoricoDatos.ObtenerUltimoInventarioValorizadoHistoricoAnteriorA(idAlmacen, fecha, familias) : _inventarioHistoricoDatos.ObtenerUltimoInventarioValorizadoHistoricoAnteriorA(idAlmacen, fecha)).ToList();
                if (ultimoInventario == null || ultimoInventario.Count == 0)
                {
                    fechaPrimeraTransaccion = _consultaTransaccionDatos.ObtenerFechaPrimeraTransaccion(idAlmacen);
                }
                ///Calculamos la fecha a partir de la cual se contemplaran las transacciones que participaran en el inventario
                var fechaDesde = ultimoInventario != null && ultimoInventario.Count>0? ultimoInventario.First().Fecha.AddMilliseconds(1) : fechaPrimeraTransaccion != null ? (DateTime)fechaPrimeraTransaccion : fecha;
                ///Obtener fecha hasta la cual se contemplará las transacciones que participarán en el inventario 
                var fechaHasta = fecha;
                ///Obtener los movimientos ocurridos luego del ultimo inventario
                var movimientos = (AplicacionSettings.Default.PermitirGestionDeLotes ? (familias != null ? _movimientosDatos.ObtenerMovimientosDeConceptoNegocioYLote(idAlmacen, fechaDesde, fechaHasta, familias) : _movimientosDatos.ObtenerMovimientosDeConceptoNegocioYLote(idAlmacen, fechaDesde, fechaHasta)) : (familias != null ? _movimientosDatos.ObtenerMovimientosDeConceptoNegocio(idAlmacen, fechaDesde, fechaHasta, familias) : _movimientosDatos.ObtenerMovimientosDeConceptoNegocio(idAlmacen, fechaDesde, fechaHasta)) ).ToList();
                ///Conseguir una lista de conceptos de negocio uniendo los conceptos del ultimo inventario y los de los movimientos.
                List<ConceptoLote> conceptosLotes = new List<ConceptoLote>();
                if (ultimoInventario != null && ultimoInventario.Count > 0)
                {
                    var grupos_inventario = ultimoInventario.GroupBy(i => new { i.IdConcepto, i.Lote }).Select(g => g.First()).ToList();
                    grupos_inventario.ForEach(g => conceptosLotes.Add(new ConceptoLote() { IdConcepto = g.IdConcepto, Lote = g.Lote }));
                }
                var mensaje = "No se puede generar el inventario por que no hay movimientos de bienes";
                if (movimientos == null || movimientos.Count <= 0)
                {
                    throw ultimoInventario == null || ultimoInventario.Count==0 ? new NohayMovimientosDeBienesException(mensaje, false, 0, null) : throw new NohayMovimientosDeBienesException(mensaje, true, ultimoInventario.First().IdTransaccion, ultimoInventario.First().Fecha);
                }
                var grupos_movimientos = movimientos.GroupBy(m => new { m.Id_concepto_negocio, m.Lote }).Select(g => g.First()).ToList();
                grupos_movimientos.ForEach(g => conceptosLotes.Add(new ConceptoLote() { IdConcepto = g.Id_concepto_negocio, Lote = g.Lote }));
                var conceptosLotesDistinct = conceptosLotes.GroupBy(cl => new { cl.IdConcepto, cl.Lote }).Select(g => g.First()).ToList();
                nuevoInventario = new Transaccion("", null, DateTimeUtil.FechaActual(), TransaccionSettings.Default.IdTipoTransaccionInventarioHistorico,
                    MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal, false, fechaHasta, fechaHasta, TransaccionSettings.Default.IdComprobanteGenerico,
                    "Inventario logico para agilizar las operaciones de movimiento fisico", fechaHasta, idEmpleado, 0m, idAlmacen, MaestroSettings.Default.IdDetalleMaestroMonedaSoles, 1m, null, idAlmacen);
                //Agregar el estado al inventario historico
                Estado_transaccion estadoTransaccion = new Estado_transaccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaHasta, "Estado inicial asignado automaticamente al realizar un inventario historico");
                nuevoInventario.Estado_transaccion.Add(estadoTransaccion);
                //Por cada concepto y lote, Agregar un detalle al inventario sumando a cada movimiento de cada concepto de la colección, la cantidad indicada en el último inventario histórico
                foreach (var item in conceptosLotesDistinct)
                {
                    nuevoInventario.Detalle_transaccion.Add(GenerarDetalleInventario(item,ultimoInventario, movimientos, item.IdConcepto, item.Lote));
                }
                return nuevoInventario;
            }
            catch (NohayMovimientosDeBienesException e)
            {
                throw (e);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al generar inventario histórico", e);
            }
        }
        public Detalle_transaccion GenerarDetalleInventario(ConceptoLote item,List<InventarioValorizado> ultimoInventario, List<Movimientos_concepto_negocio_actor_negocio_interno> movimientos, int idConcepto, string lote)
        {
            var inventarioConceptoLote = ultimoInventario != null ? ultimoInventario.SingleOrDefault(dt => dt.IdConcepto == idConcepto && dt.Lote == lote) : null;
            var movimientosConceptoLote = movimientos.Where(m => m.Id_concepto_negocio == idConcepto && m.Lote == lote);

            var cantidad_principal = (ultimoInventario != null && inventarioConceptoLote != null ?
                inventarioConceptoLote.Cantidad : 0) +
                (movimientosConceptoLote != null ? movimientosConceptoLote.Sum(m => m.Entradas_principal) : 0) -
                (movimientosConceptoLote != null ? movimientosConceptoLote.Sum(m => m.Salidas_principal) : 0); ;

            var cantidad_secundaria = (decimal)(
                (ultimoInventario != null && inventarioConceptoLote != null ?
                inventarioConceptoLote.CantidadSecundaria : 0) +
                (movimientosConceptoLote != null ? movimientosConceptoLote.Sum(m => m.Entradas_secundaria) : 0) -
                 (movimientosConceptoLote != null ? movimientosConceptoLote.Sum(m => m.Salidas_secundaria) : 0));

            var costoTotal = (ultimoInventario != null && inventarioConceptoLote != null ?
                inventarioConceptoLote.ValorTotal : 0) +
                (movimientosConceptoLote != null ? movimientosConceptoLote.Sum(m => m.Total) : 0);

            var costoUnitario = cantidad_principal != 0 ? costoTotal / cantidad_principal : 0;
            return (new Detalle_transaccion(cantidad_principal, item.IdConcepto, "Inventario", costoUnitario, costoTotal, null, cantidad_secundaria, null, null, 0m, 0m, 0m) { lote = item.Lote });
        }

        public OperationResult CrearInventarioHistoricoClonandoInventarioFisico(int idEmpleado)
        {
            try
            {
                DateTime fechaActual = DateTimeUtil.FechaActual();
                DateTime? fechaPrimeraTransaccion = fechaActual;
                //Instanciar una nueva lista de inventarios
                List<Transaccion> nuevosInventarios = new List<Transaccion>();
                //Obtener ids de centros de atención con rol de almacén.
                int[] idsAlmacenes = _maestrosAlmacenDatos.ObtenerIdsAlmacenes();
                //Por cada centro de atención con rol almacén
                foreach (var idAlmacen in idsAlmacenes)
                {
                    //Obtener ultimoInventarioLogico
                    var ultimoInventario = _inventarioHistoricoDatos.ObtenerUltimoInventarioHistorico(idAlmacen);
                    //en caso no exista un inventario, obtenemos la fecha de la primera transaccion realizada en el almacen
                    if (ultimoInventario == null)
                    {
                        fechaPrimeraTransaccion = _consultaTransaccionDatos.ObtenerFechaPrimeraTransaccion(idAlmacen);
                    }
                    //Calcular la fecha a partir de la cual se contemplaran las transacciones que participaran en el inventario
                    var fechaDesde = ultimoInventario != null ? ultimoInventario.fecha_inicio.AddMilliseconds(1) : fechaPrimeraTransaccion != null ? (DateTime)fechaPrimeraTransaccion : fechaActual;
                    //Obtener fecha hasta la cual se contemplará las transacciones que participarán en el inventario 
                    var fechaHasta = fechaActual;
                    //Obtener los inventarios fisicos del almacen
                    var inventarioFisico = _inventarioHistoricoDatos.ObtenerTransaccionInclusiveDetalleTransaccion(idAlmacen, TransaccionSettings.Default.IdTipoTransaccionInventarioActual, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado);
                    if (inventarioFisico != null)
                    {
                        var nuevoInventario = inventarioFisico.CloneTransaccionYDetalles();
                        //Ingresar los nuevos datos del nuevo inventario logico
                        nuevoInventario.codigo = "IL";
                        nuevoInventario.id_empleado = idEmpleado;
                        nuevoInventario.id_tipo_transaccion = TransaccionSettings.Default.IdTipoTransaccionInventarioHistorico;
                        nuevoInventario.comentario = "Inventario histórico para agilizar las operaciones de movimiento fisico";
                        nuevoInventario.id_comprobante = TransaccionSettings.Default.IdComprobanteGenerico;
                        nuevoInventario.fecha_inicio = fechaHasta;
                        nuevoInventario.fecha_fin = fechaHasta;
                        nuevoInventario.fecha_registro_contable = fechaHasta;
                        nuevoInventario.fecha_registro_sistema = fechaHasta;
                        //Agregar el estado al inventario historico
                        Estado_transaccion estadoTransaccion = new Estado_transaccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaActual, "Estado inicial asignado automaticamente al realizar un inventario historico");
                        nuevoInventario.Estado_transaccion.Add(estadoTransaccion);
                        //Agregamos el nuevo inventario a la coleccion de nuevos inventarios
                        nuevosInventarios.Add(nuevoInventario);
                    }
                }
                if (nuevosInventarios.Count() > 0)
                {
                    return _crearTransaccionDatos.CrearTransaccionesYDetallesTransaccionMasivo(nuevosInventarios);
                }
                throw new LogicaException("No se pudo generar el inventario histórico, debido a que no hay inventarios actuales");
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al generar inventario histórico", e);
            }
        }

        public OperationResult CrearInventariosLogicosPorLote(int idEmpleado)
        {
            try
            {
                DateTime fechaActual = DateTimeUtil.FechaActual();
                DateTime? fechaPrimeraTransaccion = fechaActual;
                //Instanciamos una nueva lista de inventarios
                List<Transaccion> nuevosInventarios = new List<Transaccion>();
                //Obtener ids de centros de atención con rol de almacén.
                int[] idsAlmacenes = _maestrosAlmacenDatos.ObtenerIdsAlmacenes();
                //Por cada centro de atención con rol almacén
                foreach (var idAlmacen in idsAlmacenes)
                {
                    //¿se hará un inventario logico para todos los centros de atención, es decir una transaccion que dentro tenga varias subtransacciones en la misma fecha?
                    //Obtener ultimoInventarioLogico
                    var ultimoInventario = _inventarioHistoricoDatos.ObtenerUltimoInventarioHistorico(idAlmacen);
                    //en caso no exista un inventario, obtenemos la fecha de la primera transaccion realizada en el almacen
                    if (ultimoInventario == null )
                    {
                        fechaPrimeraTransaccion = _consultaTransaccionDatos.ObtenerFechaPrimeraTransaccion(idAlmacen);
                    }
                    //Calculamos la fecha a partir de la cual se contemplaran las transacciones que participaran en el inventario
                    var fechaDesde = ultimoInventario != null ? ultimoInventario.fecha_inicio.AddMilliseconds(1) : fechaPrimeraTransaccion != null ? (DateTime)fechaPrimeraTransaccion : fechaActual;
                    //Obtener fecha hasta la cual se contemplará las transacciones que participarán en el inventario 
                    var fechaHasta = fechaActual;
                    //Obtener una coleccion: idConceptoNegocio, suma de cantidades (las de entrada se consideran positivas y las salidas, negativas) de los detalles de las transacciones que ocurrieron despues del inventario logico (y que tengan accion concreta de entrada y salida de conceptos de negocio)
                    var movimientos = _movimientosDatos.ObtenerMovimientosConceptoNegocioConLotePorEntidadInterna(idAlmacen, fechaDesde, fechaHasta).ToList();
                    //Conseguir una lista unificada de conceptos de negocio uniendo los conceptos del ultimo inventario y los de los movimientos.
                    List<VencimientoConceptoNegocio> conceptosConLote = new List<VencimientoConceptoNegocio>();
                    if (ultimoInventario != null)
                    {
                        conceptosConLote.AddRange(ultimoInventario.Detalle_transaccion.Select(dt => new VencimientoConceptoNegocio()
                        {
                            IdConcepto = dt.id_concepto_negocio,
                            Lote = dt.lote
                        }).Distinct());
                    }
                    if (movimientos != null)
                    {
                        conceptosConLote.AddRange(movimientos.Select(m => new VencimientoConceptoNegocio()
                        {
                            IdConcepto = m.Id_concepto_negocio,
                            Lote = m.Lote
                        }).Distinct());
                    }

                    if (movimientos != null && movimientos.Count() > 0)
                    {
                        //Generar un nuevo inventario histórico con la fecha actual.
                        Transaccion nuevoInventario = new Transaccion("", null, fechaHasta, TransaccionSettings.Default.IdTipoTransaccionInventarioHistorico,
                            MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal, false, fechaHasta, fechaHasta, TransaccionSettings.Default.IdComprobanteGenerico,
                            "Inventario histórico para agilizar las operaciones de movimiento fisico", fechaHasta, idEmpleado, 0m, idAlmacen, MaestroSettings.Default.IdDetalleMaestroMonedaSoles, 1m, null, idAlmacen);
                        //Agregar el estado al inventario historico
                        Estado_transaccion estadoTransaccion = new Estado_transaccion(idEmpleado, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaActual, "Estado inicial asignado automaticamente al realizar un inventario historico");
                        nuevoInventario.Estado_transaccion.Add(estadoTransaccion);
                        //Agregamos el nuevo inventario a la coleccion de nuevos inventarios
                        nuevosInventarios.Add(nuevoInventario);
                        //Por cada concepto, Agregar un detalle al inventario sumando a cada movimiento de cada concepto de la colección, la cantidad indicada en el último inventario histórico
                        foreach (var concepto in conceptosConLote.Distinct())
                        {
                            //Calculamos la nueva cantidad principal
                            decimal cantidad_principal = 0;
                            cantidad_principal = cantidad_principal + (ultimoInventario != null &&
                            ultimoInventario.Detalle_transaccion.SingleOrDefault(dt => dt.id_concepto_negocio == concepto.IdConcepto && dt.lote == concepto.Lote) != null ?
                            ultimoInventario.Detalle_transaccion.SingleOrDefault(dt => dt.id_concepto_negocio == concepto.IdConcepto && dt.lote == concepto.Lote).cantidad : 0)
                            +
                            (
                            movimientos.Where(m => m.Id_concepto_negocio == concepto.IdConcepto && m.Lote == concepto.Lote) != null ?
                            movimientos.Where(m => m.Id_concepto_negocio == concepto.IdConcepto && m.Lote == concepto.Lote).Sum(m => m.Entradas_principal) : 0
                            )
                            -
                            (
                             movimientos.Where(m => m.Id_concepto_negocio == concepto.IdConcepto && m.Lote == concepto.Lote) != null ?
                             movimientos.Where(m => m.Id_concepto_negocio == concepto.IdConcepto && m.Lote == concepto.Lote).Sum(m => m.Salidas_principal) : 0
                            );

                            //Calculamos la nueva cantidad secundaria
                            decimal cantidad_secundaria = 0;
                            cantidad_secundaria = cantidad_secundaria + (decimal)(
                                ultimoInventario != null &&
                                ultimoInventario.Detalle_transaccion.SingleOrDefault(dt => dt.id_concepto_negocio == concepto.IdConcepto && dt.lote == concepto.Lote) != null ?
                                ultimoInventario.Detalle_transaccion.SingleOrDefault(dt => dt.id_concepto_negocio == concepto.IdConcepto && dt.lote == concepto.Lote).cantidad_secundaria : 0
                                +
                                (
                                  movimientos.Where(m => m.Id_concepto_negocio == concepto.IdConcepto && m.Lote == concepto.Lote) != null ?
                                 movimientos.Where(m => m.Id_concepto_negocio == concepto.IdConcepto && m.Lote == concepto.Lote).Sum(m => m.Entradas_principal) : 0
                                 )
                                 -
                                 (
                                 movimientos.Where(m => m.Id_concepto_negocio == concepto.IdConcepto && m.Lote == concepto.Lote) != null ?
                                 movimientos.Where(m => m.Id_concepto_negocio == concepto.IdConcepto && m.Lote == concepto.Lote).Sum(m => m.Salidas_principal) : 0
                                ));
                            //Agregamos el nuevo detalle
                            nuevoInventario.Detalle_transaccion.Add(new Detalle_transaccion(cantidad_principal, concepto.IdConcepto, "Inventario histórico", 0m, 0m, null, cantidad_secundaria, null, null, 0m, 0m, 0m, concepto.Lote, null, null));
                        }
                    }
                }
                if (nuevosInventarios.Count() > 0)
                {
                    return _crearTransaccionDatos.CrearTransaccionesYDetallesTransaccionMasivo(nuevosInventarios);
                }
                throw new LogicaException("No se pudo generar el inventario histórico porque no hubo movimientos de entrada o salida");
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al generar inventario histórico", e);
            }
        }

        public InventarioConceptoNegocio ObtenerInventarioHistoricoPorConceptoDeNegocio(int idAlmacen, int idConcepto, string lote, DateTime fecha)
        {
            try
            {
                DateTime fechaActual = DateTimeUtil.FechaActual();
                DateTime? fechaPrimeraTransaccion = fechaActual;
                //Restamos un milisegundo para obtener el ultimo inventario generado antes de la fecha ingresado por parametro
                DateTime fechaHasta = fecha.AddMilliseconds(-1);
                DateTime fechaDesde;

                decimal costo_unitario = 0;
                decimal cantidad_principal = 0;
                decimal cantidad_secundaria = 0;
                decimal costo_total = 0;

                InventarioConceptoNegocio ultimoInventario = _inventarioHistoricoDatos.ObtenerUltimoInventarioHistoricoAntesDe(idAlmacen, idConcepto, lote, fecha);
                //Obtener ultimo movimiento inventario logico
                costo_unitario = ultimoInventario != null ? ultimoInventario.CostoUnitario : 0;
                costo_total = ultimoInventario != null ? ultimoInventario.CostoTotal : 0;
                //Calculamos la cantidades que se tienen en la ultima salida de mercaderia
                cantidad_principal = ultimoInventario != null ? ultimoInventario.CantidadPrincipal : 0;
                cantidad_secundaria = ultimoInventario != null ? ultimoInventario.CantidadSecundaria : 0;

                if (ultimoInventario == null )
                {
                    fechaPrimeraTransaccion = _consultaTransaccionDatos.ObtenerFechaPrimeraTransaccion(idAlmacen);
                }
                //Calculamos la fecha a partir de la cual se contemplaran las transacciones que participaran en el inventario
                fechaDesde = ultimoInventario != null ? ultimoInventario.Fecha.AddMilliseconds(1) : fechaPrimeraTransaccion != null ? (DateTime)fechaPrimeraTransaccion : fechaActual;

                //Obtener una coleccion: idConceptoNegocio, suma de cantidades (las de entrada se consideran positivas y las salidas, negativas) de los detalles de las transacciones que ocurrieron despues del inventario logico (y que tengan accion concreta de entrada y salida de conceptos de negocio)
                var movimientos = _movimientosDatos.ObtenerMovimientosConceptoNegocioPorEntidadInternaYConceptoNegocio(idAlmacen, idConcepto, fechaDesde, fechaHasta).ToList();
                foreach (var movimiento in movimientos)
                {
                    var factor = movimiento.EsIngreso ? 1 : -1;
                    cantidad_principal += movimiento.Cantidad_Principal * factor;
                    cantidad_secundaria += movimiento.Cantidad_Secundaria * factor;
                    costo_total += movimiento.Total * factor;
                }
                costo_unitario = (costo_total / cantidad_principal);

                return new InventarioConceptoNegocio()
                {
                    IdConceptoNegocio = idConcepto,
                    Lote = lote,
                    Fecha = fechaHasta,
                    CantidadPrincipal = cantidad_principal,
                    CantidadSecundaria = cantidad_secundaria,
                    CostoUnitario = costo_unitario,
                    CostoTotal = costo_total
                };
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener inventario histórico.  por concepto de negocio", e);
            }
        }


        private Detalle_transaccion CrearNuevoDetalleParaInventarioLogico(Detalle_transaccion detalleMovimiento, long idInventaioLogico, bool esEntrada)
        {
            var factor = esEntrada ? 1 : -1;
            return new Detalle_transaccion()
            {
                id_transaccion = idInventaioLogico,
                id_concepto_negocio = detalleMovimiento.id_concepto_negocio,
                cantidad = detalleMovimiento.cantidad * factor,
                cantidad_secundaria = detalleMovimiento.cantidad_secundaria * factor,
                detalle = "Inventario",
                precio_unitario = detalleMovimiento.precio_unitario,
                total = detalleMovimiento.total,
                id_precio = null,
                indicadorMultiproposito = 0,
                id_cuenta_contable = null,
                lote = detalleMovimiento.lote
            };
        }


        public DateTime ObtenerFechaDelUltimoInventarioHistorico(int idAlmacen)
        {
            try
            {

                var ultimoInventario = _inventarioHistoricoDatos.ObtenerUltimoInventarioHistorico(idAlmacen);
                if (ultimoInventario != null)
                {
                    return ultimoInventario.fecha_inicio;
                }

                throw new LogicaException("No se pudo obtener fecha del último inventario  histórico ");
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener inventario histórico. ", e);
            }
        }

        public Transaccion ObtenerUltimoInventarioLogico(int idAlmacen)
        {
            try
            {
                Transaccion ultimoInventario = _inventarioHistoricoDatos.ObtenerUltimoInventarioHistorico(idAlmacen);
                return ultimoInventario;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener último inventario histórico", e);
            }
        }

    }
}

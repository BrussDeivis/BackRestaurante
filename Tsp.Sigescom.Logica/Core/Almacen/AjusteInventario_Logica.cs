using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Almacen;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Compras;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Transacciones;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Negocio.Almacen;

namespace Tsp.Sigescom.Logica.Core.Almacen
{
    public class AjusteInventario_Logica : IAjusteInventario_Logica
    {
        protected readonly IInventarioActualRepositorio _inventarioActualDatos;
        protected readonly IMovimientos_Repositorio _movimientosDatos;

        protected readonly IMaestrosAlmacen_Repositorio _maestrosAlmacenDatos;

        protected readonly IPermisos_Logica _permisosLogica;
        protected readonly ICodigosOperacion_Logica _codigosOperacionLogica;
        protected readonly ICrearTransaccion_Repositorio _crearTransaccionesDatos;
        protected readonly IConsultaTransaccion_Repositorio _consultaTransaccionDatos;
        protected readonly IConsultaCompras_Repositorio _consultaComprasDatos;
        protected readonly IActualizarDetalleTransaccion_Repositorio _actualizarDetallesTransaccionDatos;




        const string PrefijoCodigoEntrada = "AIE";
        const string PrefijoCodigoSalida = "AIS";


        public AjusteInventario_Logica(IInventarioActualRepositorio inventarioActualDatos, IMaestrosAlmacen_Repositorio maestrosAlmacenDatos, IMovimientos_Repositorio movimientosDatos, ICrearTransaccion_Repositorio crearTransaccionesDatos, IConsultaTransaccion_Repositorio consultaTransaccionDatos, IConsultaCompras_Repositorio consultaComprasDatos, IPermisos_Logica permiso_logica, ICodigosOperacion_Logica codigosOperacionLogica, IActualizarDetalleTransaccion_Repositorio actualizarDetallesTransaccionDatos)
        {
            _inventarioActualDatos = inventarioActualDatos;
            _movimientosDatos = movimientosDatos;
            _permisosLogica = permiso_logica;
            _codigosOperacionLogica = codigosOperacionLogica;
            _maestrosAlmacenDatos = maestrosAlmacenDatos;
            _crearTransaccionesDatos = crearTransaccionesDatos;
            _consultaTransaccionDatos = consultaTransaccionDatos;
            _consultaComprasDatos = consultaComprasDatos;
            _actualizarDetallesTransaccionDatos = actualizarDetallesTransaccionDatos;
        }


        public OperationResult CuadrarStockFisicoEntreInventarioActualYMovimientos()
        {
            try
            {
                var idEmpleado = ActorSettings.Default.IdActorNegocioEmpleadoPorDefecto;
                var idsAlmacenes = _maestrosAlmacenDatos.ObtenerIdsAlmacenes();
                var inventariosActuales = _inventarioActualDatos.ObtenerInventariosValorizadosActuales(idsAlmacenes).ToList();
                var idsConceptosEnInventario = inventariosActuales.Select(i => i.IdConcepto).ToArray();
                var saldosMovimientos = (AplicacionSettings.Default.PermitirGestionDeLotes ? _movimientosDatos.ObtenerSaldosDeMovimientosPorConceptoYLote(idsConceptosEnInventario, idsAlmacenes) : _movimientosDatos.ObtenerSaldosDeMovimientosPorConcepto(idsConceptosEnInventario, idsAlmacenes)).ToList();
                List<InventarioValorizado> ajustesEntradas = new List<InventarioValorizado>();
                List<InventarioValorizado> ajustesSalidas = new List<InventarioValorizado>();
                foreach (var inventarioActual in inventariosActuales)
                {
                    var saldo_movimiento = saldosMovimientos.SingleOrDefault(sm => sm.IdAlmacen == inventarioActual.IdAlmacen && sm.IdConcepto == inventarioActual.IdConcepto && sm.Lote == inventarioActual.Lote);

                    if(saldo_movimiento == null)
                    {
                        if (inventarioActual.Cantidad > 0)
                        {
                            ajustesEntradas.Add(new InventarioValorizado() { IdAlmacen = inventarioActual.IdAlmacen, IdConcepto = inventarioActual.IdConcepto, Lote = inventarioActual.Lote, Cantidad = inventarioActual.Cantidad, CantidadSecundaria = inventarioActual.CantidadSecundaria });
                        }
                        else
                        {
                            ajustesSalidas.Add(new InventarioValorizado() { IdAlmacen = inventarioActual.IdAlmacen, IdConcepto = inventarioActual.IdConcepto, Lote = inventarioActual.Lote, Cantidad = inventarioActual.Cantidad, CantidadSecundaria = inventarioActual.CantidadSecundaria });
                        }
                    }
                    else if (saldo_movimiento.Cantidad < inventarioActual.Cantidad)
                    {
                        ajustesEntradas.Add(new InventarioValorizado() { IdAlmacen = saldo_movimiento.IdAlmacen, IdConcepto = saldo_movimiento.IdConcepto, Lote = saldo_movimiento.Lote, Cantidad = inventarioActual.Cantidad - saldo_movimiento.Cantidad, CantidadSecundaria = inventarioActual.CantidadSecundaria - saldo_movimiento.CantidadSecundaria });
                    }
                    else if (saldo_movimiento.Cantidad > inventarioActual.Cantidad)
                    {
                        ajustesSalidas.Add(new InventarioValorizado() { IdAlmacen = saldo_movimiento.IdAlmacen, IdConcepto = saldo_movimiento.IdConcepto, Lote = saldo_movimiento.Lote, Cantidad = saldo_movimiento.Cantidad - inventarioActual.Cantidad, CantidadSecundaria = saldo_movimiento.CantidadSecundaria - inventarioActual.CantidadSecundaria });
                    }
                }
                var costosUnitariosCompras = _consultaComprasDatos.ObtenerValorUnitarioDePrimeraOrdenDeCompraConPrecioMayorACero(ajustesEntradas.Union(ajustesSalidas).Select(a => a.IdConcepto).Distinct().ToArray()).ToList();
                ajustesEntradas.ForEach(ae => { ae.ValorUnitario = (inventariosActuales.SingleOrDefault(ia=> ia.IdConcepto== ae.IdConcepto && ia.IdAlmacen== ae.IdAlmacen).ValorUnitario!=0? inventariosActuales.SingleOrDefault(ia => ia.IdConcepto == ae.IdConcepto && ia.IdAlmacen == ae.IdAlmacen).ValorUnitario:(
                    costosUnitariosCompras.FirstOrDefault(cu => cu.IdConcepto == ae.IdConcepto)!=null? costosUnitariosCompras.FirstOrDefault(cu => cu.IdConcepto == ae.IdConcepto).Precio:0)); ae.ValorTotal = ae.Cantidad * ae.ValorUnitario; });
                return GenerarYCrearTransaccionesAjustesInventario(idsAlmacenes, idEmpleado, ajustesEntradas, ajustesSalidas);
                
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al cuadrar stock fisico entreInventario actual y movimientos " + e.Message, e);
            }
        }

        private OperationResult GenerarYCrearTransaccionesAjustesInventario(int[] idsAlmacenes, int idEmpleado ,List<InventarioValorizado> ajustesEntradas, List<InventarioValorizado> ajustesSalidas)
        {
            try
            {
                List<Transaccion> transaccionesAjustes = new List<Transaccion>();
                foreach (var idAlmacen in idsAlmacenes)
                {
                    var hayAjustesEntradas = ajustesEntradas.Where(ae => ae.IdAlmacen == idAlmacen).Any();
                    var hayAjustesSalidas = ajustesSalidas.Where(ae => ae.IdAlmacen == idAlmacen).Any();
                    var codigoEntrada = _codigosOperacionLogica.ObtenerMaximoCodigoParaTransaccion(PrefijoCodigoEntrada, TransaccionSettings.Default.IdTipoTransaccionEntradaBienesAjusteInventario);
                    var codigoSalida = _codigosOperacionLogica.ObtenerMaximoCodigoParaTransaccion(PrefijoCodigoSalida, TransaccionSettings.Default.IdTipoTransaccionSalidaBienesAjusteInventario);
                    if (hayAjustesEntradas)
                    {
                        transaccionesAjustes.Add(GenerarAjusteInventario(idEmpleado, idAlmacen, true, codigoEntrada, ajustesEntradas.Where(ae => ae.IdAlmacen == idAlmacen).ToList()));
                    }
                    if (hayAjustesSalidas)
                    {
                        transaccionesAjustes.Add(GenerarAjusteInventario(idEmpleado, idAlmacen, false, codigoSalida, ajustesSalidas.Where(a => a.IdAlmacen == idAlmacen).ToList()));
                    }
                }
                return _crearTransaccionesDatos.CrearTransacciones(transaccionesAjustes);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al generar y crear transacciones. " + e.Message, e);
            }
        }

        private Transaccion GenerarAjusteInventario(int idEmpleado, int idAlmacen, bool esEntrada, int maximoCodigo ,List<InventarioValorizado> detalles)
        {
            try
            {

                var fechaActual = DateTimeUtil.FechaActual();
                var fechaPrimeraTransaccion = _consultaTransaccionDatos.ObtenerFechaPrimeraTransaccion(idAlmacen)?? fechaActual;
                var observacion = "";
                var prefijoCodigo = esEntrada?PrefijoCodigoEntrada:PrefijoCodigoSalida;
                var idTipoTransaccion = esEntrada ? TransaccionSettings.Default.IdTipoTransaccionEntradaBienesAjusteInventario : TransaccionSettings.Default.IdTipoTransaccionSalidaBienesAjusteInventario;
                int idMoneda = MaestroSettings.Default.IdDetalleMaestroMonedaSoles;
                int idUnidadNegocio = MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal;
                decimal tipoDeCambio = 1;
                string codigo = prefijoCodigo + (++maximoCodigo).ToString();

                Transaccion ajusteInventario = new Transaccion(codigo, null, fechaActual, idTipoTransaccion, idUnidadNegocio, true, fechaPrimeraTransaccion, fechaPrimeraTransaccion, observacion, fechaPrimeraTransaccion, idEmpleado, 0, idAlmacen, idMoneda, tipoDeCambio, null, idAlmacen);
                ajusteInventario.id_comprobante = TransaccionSettings.Default.IdComprobanteGenerico;
                ajusteInventario.Detalle_transaccion = InventarioValorizado.ToDetallesTransaccion(detalles.Where(ae => ae.IdAlmacen == idAlmacen).ToList(), "ajuste inventario");
                return ajusteInventario;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al generar ajustes de inventario", e);
            }
        }

        

        public OperationResult RecalcularCostoUnitarioYTotalEnMovimientos()
        {
            try
            {
                var idsAlmacenes = _maestrosAlmacenDatos.ObtenerIdsAlmacenes();
                var inventariosActuales = _inventarioActualDatos.ObtenerInventariosValorizadosActuales(idsAlmacenes).ToList();
                var idsConceptosEnInventario = inventariosActuales.Select(i => i.IdConcepto).Distinct().ToArray();
                var fechaActual = DateTimeUtil.FechaActual();
                var tiposOperacionSegunInventario = Diccionario.TiposDeTransaccionMovimientoDeBienesConCostoUnitarioSegunInventario;
                var tiposOperacionSegunOrden = Diccionario.TiposDeTransaccionMovimientoDeBienesConCostoUnitarioObtenidoDeLaOrden;
                var tiposOperacionSegunTransaccionOrigen = Diccionario.TiposDeTransaccionMovimientoDeBienesConCostoUnitarioObtenidoDeMovimientoDeTransaccionOrigen;
                foreach (var idAlmacen in idsAlmacenes)
                {
                    var fechaPrimeraTransaccion = _consultaTransaccionDatos.ObtenerFechaPrimeraTransaccion(idAlmacen) ?? fechaActual;
                    var movimientos = _movimientosDatos.ObtenerMovimientosDeAlmacenesConOrdenYOrigen(idAlmacen, idsConceptosEnInventario, fechaPrimeraTransaccion, fechaActual).ToList();
                    if(movimientos.Count>0)
                    { 
                        foreach (var idConcepto in idsConceptosEnInventario)
                        {
                            //buscar si hay un ajuste de inventario para ese concepto
                            var entradaAjustesInventario = movimientos.SingleOrDefault(m => m.IdTipoTransaccion == TransaccionSettings.Default.IdTipoTransaccionEntradaBienesAjusteInventario && m.IdConcepto== idConcepto);
                        
                            decimal saldoImporte = entradaAjustesInventario!=null? entradaAjustesInventario.ImporteTotal:0;
                            decimal saldoCantidad = entradaAjustesInventario != null ? entradaAjustesInventario.Cantidad : 0; 
                            decimal costoUnitarioPromedio = entradaAjustesInventario != null ? entradaAjustesInventario.ImporteUnitario : 0;
                            var movimientosConcepto = movimientos.Where(m => m.IdConcepto == idConcepto && ( (entradaAjustesInventario != null  && m.IdDetalleTransaccion!=entradaAjustesInventario.IdDetalleTransaccion)|| entradaAjustesInventario == null)).OrderBy(m=> m.Fecha).ToList();
                            foreach (var movimiento in movimientosConcepto)
                            {
                                var factor = movimiento.EsEntrada ? 1 : -1;
                                saldoCantidad += movimiento.Cantidad * factor;

                                if (tiposOperacionSegunInventario.Contains(movimiento.IdTipoTransaccion))
                                {
                                    movimiento.ImporteUnitario = costoUnitarioPromedio;
                                    movimiento.ImporteTotal = movimiento.Cantidad * movimiento.ImporteUnitario;
                                }
                                else if (tiposOperacionSegunOrden.Contains(movimiento.IdTipoTransaccion))
                                {
                                    movimiento.ImporteTotal = TransaccionSettings.Default.AplicaLeyAmazonia ? movimiento.ImporteTotalOrden : movimiento.ImporteTotalOrden - movimiento.IgvOrden;
                                    movimiento.ImporteUnitario = movimiento.Cantidad!=0? movimiento.ImporteTotal / movimiento.Cantidad:0;
                                }
                                else if (tiposOperacionSegunTransaccionOrigen.Contains(movimiento.IdTipoTransaccion))
                                {
                                    var movimientoOrigen = movimiento.IdTransaccionMovimientoOrigen != null ? movimientos.FirstOrDefault(m => m.IdTransaccion == movimiento.IdTransaccionMovimientoOrigen && m.IdConcepto == idConcepto): null;
                                    movimiento.ImporteUnitario = movimientoOrigen!=null? movimientos.FirstOrDefault(m => m.IdTransaccion == movimiento.IdTransaccionMovimientoOrigen && m.IdConcepto == idConcepto ).ImporteUnitario: costoUnitarioPromedio;
                                    movimiento.ImporteTotal = movimiento.ImporteUnitario * movimiento.Cantidad;
                                }
                                saldoImporte += movimiento.ImporteTotal * factor;
                                costoUnitarioPromedio = saldoCantidad!=0? saldoImporte / saldoCantidad:0;

                            }
                            var inventarioActualConcepto = inventariosActuales.SingleOrDefault(i => i.IdAlmacen == idAlmacen && i.IdConcepto == idConcepto);
                            if(inventarioActualConcepto!= null)
                            {
                                inventarioActualConcepto.ValorUnitario = costoUnitarioPromedio;
                                inventarioActualConcepto.ValorTotal = saldoImporte;
                            }

                    }
                        inventariosActuales.ForEach(i => movimientos.Add(new MovimientoAlmacen() { IdDetalleTransaccion = i.IdDetalleTransaccion, ImporteUnitario = i.ValorUnitario, ImporteTotal = i.ValorTotal }));
                        _actualizarDetallesTransaccionDatos.ActualizarValorUnitarioyValorTotal(movimientos);
                    }
                }
                return new OperationResult(OperationResultEnum.Success);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al recalcular costo unitario y total de movimientos de mercaderia", e);
            }
           
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Entidades.Sesion;
using Tsp.Sigescom.Modelo.Interfaces.Datos;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Almacen;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Transacciones;
using Tsp.Sigescom.Modelo.Negocio.Almacen;
using Tsp.Sigescom.Modelo.Negocio.Almacen.Report;
using Tsp.Sigescom.Modelo.Negocio.Core.Actor;
using Tsp.Sigescom.Modelo.Negocio.Establecimientos;

namespace Tsp.Sigescom.Logica.Core.Almacen
{
    public class AlmacenReporte_Logica : IAlmacenReporte_Logica
    {
        protected readonly IInventarioHistorico_Logica _inventarioHistoricoLogica;
        protected readonly IInventarioRepositorio _almacenReportingDatos;
        protected readonly IInventarioHistorico_Repositorio _inventarioHistoricoDatos;
        protected readonly IMovimientos_Repositorio _movimientosDatos;
        protected readonly IInventarioActualRepositorio _inventarioActualDatos;
        protected readonly IMaestrosAlmacen_Repositorio _maestrosAlmacenDatos;
        protected readonly IConsultaTransaccion_Repositorio _consultaTransaccionDatos;
        protected readonly IEstablecimiento_Logica _establecimientoLogica;



        public AlmacenReporte_Logica(IInventarioRepositorio almacenReportingDatos, IInventarioHistorico_Repositorio inventarioHistoricoDatos, IInventarioHistorico_Logica inventarioHistoricoLogica, IMovimientos_Repositorio movimientosDatos, IInventarioActualRepositorio inventarioActualDatos, IMaestrosAlmacen_Repositorio maestrosAlmacenDatos, IConsultaTransaccion_Repositorio consultaTransaccionDatos, IEstablecimiento_Logica establecimientoLogica)
        {
            _inventarioHistoricoLogica = inventarioHistoricoLogica;
            _almacenReportingDatos = almacenReportingDatos;
            _inventarioHistoricoDatos = inventarioHistoricoDatos;
            _movimientosDatos = movimientosDatos;
            _inventarioActualDatos = inventarioActualDatos;
            _maestrosAlmacenDatos = maestrosAlmacenDatos;
            _consultaTransaccionDatos = consultaTransaccionDatos;
            _establecimientoLogica = establecimientoLogica;
        }



        public PrincipalReportData ObtenerDatosParaReportePrincipal(UserProfileSessionData profileData)
        {
            var TieneRolAdministradorDeNegocio = profileData.Empleado.TieneRol(ActorSettings.Default.idRolAdministradorDeNegocio);
            if (profileData.CentroDeAtencionSeleccionado == null && !TieneRolAdministradorDeNegocio)
            {
                throw new LogicaException("No cuenta con suficientes permisos para acceder a este reporte");
            }
            var establecimientosConAlmacenes = TieneRolAdministradorDeNegocio ? Establecimiento.Convert(_establecimientoLogica.ObtenerEstablecimientosComercialesVigentesConSusAlmacenes().ToList()) : null;

            var data = new PrincipalReportData()
            {
                FechaActual_ = DateTimeUtil.FechaActual(),
                EsAdministrador = TieneRolAdministradorDeNegocio,
                Establecimientos = TieneRolAdministradorDeNegocio ? establecimientosConAlmacenes : new List<Establecimiento>() { profileData.EstablecimientoComercialSeleccionado.ToEstablecimiento() },
                Almacenes = TieneRolAdministradorDeNegocio ? establecimientosConAlmacenes.SelectMany(e => e.CentrosAtencion).ToList() : new List<ItemGenerico>() { profileData.CentroDeAtencionSeleccionado.ToItemGenerico() },
                Familias = _maestrosAlmacenDatos.ObtenerFamiliasBienes().ToList().OrderBy(f => f.Nombre).ToList(),
                Conceptos = _maestrosAlmacenDatos.ObtenerConceptosNegocioComercializablesBienes().ToList().OrderBy(c => c.Nombre).ToList()
            };
            if (!TieneRolAdministradorDeNegocio) data.Establecimientos.SingleOrDefault().CentrosAtencion = new List<ItemGenerico>() { profileData.CentroDeAtencionSeleccionado.ToItemGenerico() };
            return data;
        }
        public InventarioFisico InventarioFisicoHistorico(int idAlmacen, DateTime fecha, int idConcepto)
        {
            try
            {
                InventarioFisico inventario = new InventarioFisico() { Fecha = fecha };
                DateTime? fechaPrimeraTransaccion = (DateTime)fecha;
                ///Obtener ultimoInventarioLogico
                InventarioFisico ultimoInventario = _inventarioHistoricoDatos.ObtenerUltimoInventarioFisicoHistoricoAnteriorA(idAlmacen, idConcepto, fecha);
                ///en caso no exista un inventario, obtenemos la fecha de la primera transaccion realizada en el almacen
                if (ultimoInventario == null)
                {
                    fechaPrimeraTransaccion = _consultaTransaccionDatos.ObtenerFechaPrimeraTransaccion(idAlmacen);
                }
                ///Calculamos la fecha a partir de la cual se contemplaran las transacciones que participaran en el inventario
                var fechaDesde = ultimoInventario != null ? ultimoInventario.Fecha.AddMilliseconds(1) : fechaPrimeraTransaccion != null ? (DateTime)fechaPrimeraTransaccion : fecha;
                ///Obtener fecha hasta la cual se contemplará las transacciones que participarán en el inventario 
                var _fechaHasta = fecha;
                ///Obtener los movimientos ocurridos luego del ultimo inventario
                Movimientos_concepto_negocio_actor_negocio_interno movimientos = _movimientosDatos.ObtenerMovimientosDeConceptoNegocio(idAlmacen, idConcepto, fechaDesde, _fechaHasta);
                inventario.IdAlmacen = idAlmacen;
                inventario.IdConcepto = idConcepto;
                inventario.Cantidad = (ultimoInventario != null ? ultimoInventario.Cantidad : 0) + (movimientos != null ? movimientos.Entradas_principal - movimientos.Salidas_principal : 0);
                return inventario;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener inventario" + "\n " + e.Message, e);
            }
        }
        public List<InventarioFisico> ObtenerInventarioFisicoHistorico(int idAlmacen, int idEmpleado, DateTime fechaHasta, bool todasLasFamilias, int[] idsFamilias)
        {
            try
            {
                List<InventarioFisico> inventario = new List<InventarioFisico>();
                idsFamilias = todasLasFamilias ? _maestrosAlmacenDatos.ObtenerFamiliasBienes().Select(f => f.Id).ToArray() : idsFamilias;
                inventario = _inventarioHistoricoLogica.ObtenerInventariosFisicos(idAlmacen, idEmpleado, fechaHasta, idsFamilias).ToList();
                //inventario = (todasLasFamilias ? _inventarioHistoricoLogica.ObtenerInventariosFisicos(idAlmacen, idEmpleado, fechaHasta) : _inventarioHistoricoLogica.ObtenerInventariosFisicos(idAlmacen, idEmpleado, fechaHasta, idsFamilias)).ToList();
                return inventario;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener inventario" + "\n " + e.Message, e);
            }
        }

        public InventarioValorizado ObtenerInventarioValorizadoHistorico(int idAlmacen, int idEmpleado, DateTime fecha, int idConcepto)
        {
            try
            {
                InventarioValorizado inventario = new InventarioValorizado() { Fecha = fecha };
                DateTime? fechaPrimeraTransaccion = (DateTime)fecha;
                ///Obtener ultimoInventarioLogico
                InventarioValorizado ultimoInventario = _inventarioHistoricoDatos.ObtenerUltimoInventarioValorizadoHistoricoAnteriorA(idAlmacen, idConcepto, fecha);
                ///en caso no exista un inventario, obtenemos la fecha de la primera transaccion realizada en el almacen
                if (ultimoInventario == null)
                {
                    fechaPrimeraTransaccion = _consultaTransaccionDatos.ObtenerFechaPrimeraTransaccion(idAlmacen);
                }
                ///Calculamos la fecha a partir de la cual se contemplaran las transacciones que participaran en el inventario
                var fechaDesde = ultimoInventario != null ? ultimoInventario.Fecha.AddMilliseconds(1) : fechaPrimeraTransaccion != null ? (DateTime)fechaPrimeraTransaccion : fecha;
                ///Obtener fecha hasta la cual se contemplará las transacciones que participarán en el inventario 
                var _fechaHasta = fecha;
                ///Obtener los movimientos ocurridos luego del ultimo inventario
                Movimientos_concepto_negocio_actor_negocio_interno movimientos = _movimientosDatos.ObtenerMovimientosDeConceptoNegocio(idAlmacen, idConcepto, fechaDesde, _fechaHasta);
                inventario.IdAlmacen = idAlmacen;
                inventario.IdConcepto = idConcepto;
                inventario.Cantidad = (ultimoInventario != null ? ultimoInventario.Cantidad : 0) + (movimientos != null ? movimientos.Entradas_principal - movimientos.Salidas_principal : 0);
                inventario.ValorTotal = (ultimoInventario != null ? ultimoInventario.ValorTotal : 0) + (movimientos != null ? movimientos.Total : 0);
                inventario.ValorUnitario = inventario.Cantidad != 0 ? inventario.ValorTotal / inventario.Cantidad : 0;
                return inventario;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener inventario" + "\n " + e.Message, e);
            }
        }
        public List<InventarioValorizado> InventarioValorizadoHistorico(int idAlmacen, int idEmpleado, DateTime fechaHasta, bool todasLasFamilias, int[] idsFamilias)
        {
            try
            {
                List<InventarioValorizado> inventario = new List<InventarioValorizado>();
                idsFamilias = todasLasFamilias ? _maestrosAlmacenDatos.ObtenerFamiliasBienes().Select(f => f.Id).ToArray() : idsFamilias;
                inventario = _inventarioHistoricoLogica.ObtenerInventariosValorizados(idEmpleado, idAlmacen, fechaHasta, idsFamilias).ToList();
                //inventario = (todasLasFamilias ? _inventarioHistoricoLogica.ObtenerInventariosValorizados(idEmpleado, idAlmacen, fechaHasta) : _inventarioHistoricoLogica.ObtenerInventariosValorizados(idEmpleado, idAlmacen, fechaHasta, idsFamilias)).ToList();
                return inventario;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener inventario" + "\n " + e.Message, e);
            }
        }


        public List<EntradaAlmacen> Entradas(int idAlmacen, DateTime fechaDesde, DateTime fechaHasta, bool todasLasFamilias, int[] idsFamilias)
        {
            try
            {
                idsFamilias = todasLasFamilias ? _maestrosAlmacenDatos.ObtenerFamiliasBienes().Select(f => f.Id).ToArray() : idsFamilias;
                List<EntradaAlmacen> entradas = _movimientosDatos.ObtenerEntradas(idAlmacen, fechaDesde, fechaHasta, idsFamilias).ToList();
                //List<EntradaAlmacen> entradas = (todasLasFamilias ? _movimientosDatos.ObtenerEntradas(idAlmacen, fechaDesde, fechaHasta) : _movimientosDatos.ObtenerEntradas(idAlmacen, fechaDesde, fechaHasta, idsFamilias)).ToList();
                return entradas;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener Entradas", e);
            }

        }
        public List<SalidaAlmacen> Salidas(int idAlmacen, DateTime fechaDesde, DateTime fechaHasta, bool todasLasFamilias, int[] idsFamilias)
        {
            try
            {
                idsFamilias = todasLasFamilias ? _maestrosAlmacenDatos.ObtenerFamiliasBienes().Select(f => f.Id).ToArray() : idsFamilias;
                List<SalidaAlmacen> salidas = _movimientosDatos.ObtenerSalidas(idAlmacen, fechaDesde, fechaHasta, idsFamilias).ToList();
                //List<SalidaAlmacen> salidas = (todasLasFamilias ? _movimientosDatos.ObtenerSalidas(idAlmacen, fechaDesde, fechaHasta) : _movimientosDatos.ObtenerSalidas(idAlmacen, fechaDesde, fechaHasta, familias)).ToList();
                return salidas;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener Entradas", e);
            }

        }
        public List<InventarioVencimiento> Vencimientos(int idAlmacen, DateTime fechaDesde, DateTime fechaHasta, bool todasLasFamilias, int[] idsFamilias)
        {
            try
            {
                List<InventarioVencimiento> inventario = new List<InventarioVencimiento>();
                var gestionGlobalLotes = AplicacionSettings.Default.PermitirGestionDeLotes;
                idsFamilias = todasLasFamilias ? _maestrosAlmacenDatos.ObtenerFamiliasBienes().Select(f => f.Id).ToArray() : idsFamilias;
                if (gestionGlobalLotes)
                {
                    inventario = _inventarioActualDatos.ObtenerVencimientosInventarioActual(idAlmacen, fechaDesde, fechaHasta, idsFamilias).ToList();
                    //inventario = (todasLasFamilias ? _inventarioActualDatos.ObtenerVencimientosInventarioActual(idAlmacen, fechaDesde, fechaHasta) : _inventarioActualDatos.ObtenerVencimientosInventarioActual(idAlmacen, fechaDesde, fechaHasta, idsFamilias)).ToList();
                }
                else
                {
                    var vencimientosEntradas = _almacenReportingDatos.ObtenerVencimientoConceptosIngresados(idAlmacen, fechaDesde, fechaHasta, idsFamilias).ToList();
                    //var vencimientosEntradas = (todasLasFamilias ? _almacenReportingDatos.ObtenerVencimientoConceptosIngresados(idAlmacen, fechaDesde, fechaHasta) : _almacenReportingDatos.ObtenerVencimientoConceptosIngresados(idAlmacen, fechaDesde, fechaHasta, idsFamilias)).ToList();
                    var idsConceptos = vencimientosEntradas.Select(v => v.IdConcepto).Distinct().ToArray();
                    var inventarioActual = _inventarioActualDatos.ObtenerInventarioFisicoConceptosActual(idAlmacen, idsConceptos);
                    vencimientosEntradas.ForEach(ve => inventario.Add(new InventarioVencimiento()
                    {
                        CodigoBarra = ve.CodigoBarra,
                        Concepto = ve.Concepto,
                        UnidadMedida = ve.UnidadMedida,
                        Lote = ve.Lote,
                        Cantidad = inventarioActual.Where(i => i.IdConcepto == ve.IdConcepto).Sum(dt => dt.Cantidad),
                        FechaVencimiento = ve.FechaVencimiento,
                        IdConcepto = ve.IdConcepto
                    }));
                }
                return inventario;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener vencimientos de inventario", e);
            }

        }


        public List<InventarioSemaforo> InventarioSemaforoHistorico(int idAlmacen, int idEmpleado, DateTime fechaHasta, bool todasLasFamilias, int[] idsFamilias, bool estadoBajo, bool estadoNormal, bool estadoAlto)
        {
            List<int> nivelesRequeridos = new List<int>();
            nivelesRequeridos.Add((int)NivelStockSemaforoEnum.Indeterminado);
            if (estadoAlto) nivelesRequeridos.Add((int)NivelStockSemaforoEnum.Alto);
            if (estadoBajo) nivelesRequeridos.Add((int)NivelStockSemaforoEnum.Bajo);
            if (estadoNormal) nivelesRequeridos.Add((int)NivelStockSemaforoEnum.Normal);
            try
            {
                List<InventarioSemaforo> inventario = new List<InventarioSemaforo>();
                idsFamilias = todasLasFamilias ? _maestrosAlmacenDatos.ObtenerFamiliasBienes().Select(f => f.Id).ToArray() : idsFamilias;
                inventario = _inventarioHistoricoLogica.ObtenerInventariosSemaforo(idEmpleado, idAlmacen, fechaHasta, idsFamilias).ToList();
                //inventario = (todasLasFamilias ? _inventarioHistoricoLogica.ObtenerInventariosSemaforo(idEmpleado, idAlmacen, fechaHasta) : _inventarioHistoricoLogica.ObtenerInventariosSemaforo(idEmpleado, idAlmacen, fechaHasta, idsFamilias)).ToList();
                return inventario.Where(i => nivelesRequeridos.Contains(i.ValorSemaforoInt)).OrderBy(i => i.Concepto).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener inventario" + "\n " + e.Message, e);
            }
        }




        public List<DetalleKardexFisico> KardexFisico(int idAlmacen, int idEmpleado, DateTime fechaDesde, DateTime fechaHasta, int idConcepto)
        {
            try
            {
                var saldoInicial = InventarioFisicoHistorico(idAlmacen, fechaDesde, idConcepto);
                var movimientos = _movimientosDatos.ObtenerDetallesMovimientoAlmacen(idAlmacen, idConcepto, fechaDesde, fechaHasta).ToList();
                var idsOrdenMovimientoDiferenteABoletaFactura = movimientos.Where(m => m.IdTipoComprobante != MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta && m.IdTipoComprobante != MaestroSettings.Default.IdDetalleMaestroComprobanteFactura && m.IdOrden!=null).Select(m => (long)m.IdOrden).ToArray();
                var hayComprobantesDiferentesABoletaYFactura = idsOrdenMovimientoDiferenteABoletaFactura.Length > 0;

                var comprobantesOrdenMovimientoADiferenteBoletaFactura = hayComprobantesDiferentesABoletaYFactura?_movimientosDatos.ObtenerComprobantesDeOrdenes(idsOrdenMovimientoDiferenteABoletaFactura.ToArray()):null;
                DetalleKardexFisico detalleSaldoInicial = new DetalleKardexFisico() { Index = 0, Fecha = fechaDesde, ActorExterno = "", Operacion = "Saldo Inicial", CantidadSaldo = saldoInicial.Cantidad };
                List<DetalleKardexFisico> kardex = new List<DetalleKardexFisico>();
                kardex.Add(detalleSaldoInicial);
                var cantidadInicial = detalleSaldoInicial.CantidadSaldo;
                int factor = 1;
                decimal saldoCantidad = cantidadInicial;
                int index = detalleSaldoInicial.Index;
                movimientos.ForEach(m =>
                {
                    var comprobanteOrden = m.IdTipoComprobante != MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta && m.IdTipoComprobante != MaestroSettings.Default.IdDetalleMaestroComprobanteFactura && comprobantesOrdenMovimientoADiferenteBoletaFactura!=null && comprobantesOrdenMovimientoADiferenteBoletaFactura.Any(c => c.IdOperacion == m.IdOrden)? " (" + comprobantesOrdenMovimientoADiferenteBoletaFactura.Single(c => c.IdOperacion == m.IdOrden).Comprobante + ")" : "";

                    factor = m.EsEntrada ? 1 : -1;
                    saldoCantidad += m.Cantidad * factor;
                    kardex.Add(new DetalleKardexFisico() { Index = ++index, Fecha = m.Fecha, ActorExterno = m.NombreActorNegocioExterno, Operacion = m.NombreTipoTransaccion + comprobanteOrden  , CodigoTipoComprobante = m.CodigoTipoComprobante, SerieYNumeroComprobante = m.NumeroSerie + "-" + m.NumeroComprobante, CantidadEntrada = m.EsEntrada ? m.Cantidad : 0, CantidadSaldo = saldoCantidad, CantidadSalida = !m.EsEntrada ? m.Cantidad : 0, });
                });
                return kardex;
            }
            catch (Exception e)
            {

                throw new LogicaException("Error al intentar generar el kardex fisico",e);
            }
        }

        public List<DetalleKardexValorizado> KardexValorizado(int idAlmacen, int idEmpleado, DateTime fechaDesde, DateTime fechaHasta, int idConcepto)
        {
            try
            {
                var saldoInicial = ObtenerInventarioValorizadoHistorico(idAlmacen, idEmpleado, fechaDesde, idConcepto);
                var movimientos = _movimientosDatos.ObtenerDetallesMovimientoAlmacen(idAlmacen, idConcepto, fechaDesde, fechaHasta).ToList();
                var idsOrdenMovimientoDiferenteABoletaFactura = movimientos.Where(m => m.IdTipoComprobante != MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta && m.IdTipoComprobante != MaestroSettings.Default.IdDetalleMaestroComprobanteFactura && m.IdOrden != null).Select(m => (long)m.IdOrden).ToArray();
                var hayComprobantesDiferentesABoletaYFactura = idsOrdenMovimientoDiferenteABoletaFactura.Length > 0;

                var comprobantesOrdenMovimientoADiferenteBoletaFactura = hayComprobantesDiferentesABoletaYFactura ? _movimientosDatos.ObtenerComprobantesDeOrdenes(idsOrdenMovimientoDiferenteABoletaFactura.ToArray()):null;

                DetalleKardexValorizado detalleSaldoInicial = new DetalleKardexValorizado() { Index = 0, Fecha = fechaDesde, ActorExterno = "", Operacion = "Saldo Inicial", CantidadSaldo = saldoInicial.Cantidad, ImporteUnitarioSaldo = saldoInicial.ValorUnitario, ImporteTotalSaldo = saldoInicial.ValorTotal };
                List<DetalleKardexValorizado> kardex = new List<DetalleKardexValorizado>();
                kardex.Add(detalleSaldoInicial);
                int factor = 1;
                decimal saldoCantidad = detalleSaldoInicial.CantidadSaldo;
                decimal saldoImporteTotal = detalleSaldoInicial.ImporteTotalSaldo;
                int index = detalleSaldoInicial.Index;
                movimientos.ForEach(m =>
                {
                    var comprobanteOrden = m.IdTipoComprobante != MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta && m.IdTipoComprobante != MaestroSettings.Default.IdDetalleMaestroComprobanteFactura && comprobantesOrdenMovimientoADiferenteBoletaFactura != null && comprobantesOrdenMovimientoADiferenteBoletaFactura.Any(c => c.IdOperacion == m.IdOrden) ? " (" + comprobantesOrdenMovimientoADiferenteBoletaFactura.Single(c => c.IdOperacion == m.IdOrden).Comprobante + ")" : "";
                    factor = m.EsEntrada ? 1 : -1;
                    var factorEntrada = m.EsEntrada ? 1 : 0;
                    var factorSalida = !m.EsEntrada ? 1 : 0;

                    saldoCantidad += m.Cantidad * factor;
                    saldoImporteTotal += m.ImporteTotal * factor;

                    kardex.Add(new DetalleKardexValorizado() { Index = ++index, Fecha = m.Fecha, ActorExterno = m.NombreActorNegocioExterno, Operacion = m.NombreTipoTransaccion + comprobanteOrden, CodigoTipoComprobante = m.CodigoTipoComprobante, SerieYNumeroComprobante = m.NumeroSerie + "-" + m.NumeroComprobante, CantidadEntrada = factorEntrada * m.Cantidad, ImporteUnitarioEntrada = factorEntrada * m.ImporteUnitario, ImporteTotalEntrada = factorEntrada * m.ImporteTotal, CantidadSalida = factorSalida * m.Cantidad, ImporteUnitarioSalida = factorSalida * m.ImporteUnitario, ImporteTotalSalida = factorSalida * m.ImporteTotal, CantidadSaldo = saldoCantidad, ImporteUnitarioSaldo = saldoCantidad != 0 ? (saldoImporteTotal / saldoCantidad) : 0, ImporteTotalSaldo = saldoImporteTotal });
                });
                return kardex;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar generar el kardex valorizado", e);
            }

        }

        public StockMinMax ObtenerStockMinimoYMaximo(int idConcepto)
        {
            decimal stockMinimo = _maestrosAlmacenDatos.ObtenerStockMinimo(idConcepto);
            decimal stockMaximo = stockMinimo * (1 + ((decimal)ConceptoSettings.Default.PorcentajeParaObtenerStockMaximo / 100));
            return new StockMinMax() { IdConcepto = idConcepto, StockMinimo = stockMinimo, StockMaximo = stockMaximo };
        }


    }
}

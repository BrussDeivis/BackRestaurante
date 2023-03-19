using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Entidades.Sesion;
using Tsp.Sigescom.Modelo.Interfaces.Datos;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;
using Tsp.Sigescom.Modelo.Negocio.Finanza;
using Tsp.Sigescom.Modelo.Negocio.Finanza.Report;
using Tsp.Sigescom.Modelo.Negocio.Core.Actor;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Finanza.PlainModel;
using Tsp.Sigescom.Modelo.Negocio.Establecimientos;

namespace Tsp.Sigescom.Logica.Core.Finanza
{
    public class FinanzaReporte_Logica : IFinanzaReporte_Logica
    {
        protected readonly IActorNegocioLogica _actorNegocioLogica;
        protected readonly IOperacionLogica _operacionLogica;
        protected readonly IMaestroRepositorio _maestroDatos;
        protected readonly IFinanzaReporte_Repositorio _finanzaReportingDatos;
        protected readonly ITransaccionRepositorio _transaccionDatos;
        protected readonly IEstablecimiento_Logica _establecimientoLogica;

        
        public FinanzaReporte_Logica(IActorNegocioLogica actorNegocioLogica, IMaestroRepositorio maestroDatos, IFinanzaReporte_Repositorio finanzaReportingDatos, IOperacionLogica operacionLogica, ITransaccionRepositorio transaccionDatos, IEstablecimiento_Logica establecimientoLogica)
        {
            _actorNegocioLogica = actorNegocioLogica;
            _maestroDatos = maestroDatos;
            _finanzaReportingDatos = finanzaReportingDatos;
            _operacionLogica = operacionLogica;
            _transaccionDatos = transaccionDatos;
            _establecimientoLogica = establecimientoLogica;
        }
        public PrincipalReportData ObtenerDatosParaReportePrincipal(UserProfileSessionData profileData)
        {
            var establecimientosConSusCajasCuentas = new List<Establecimiento>();
            var establecimientoSesion = profileData.EstablecimientoComercialSeleccionado.ToEstablecimiento();
            var TieneRolAdministradorDeNegocio = profileData.Empleado.TieneRol(ActorSettings.Default.idRolAdministradorDeNegocio);
            var CajaSesion = profileData.CentroDeAtencionSeleccionado.ToItemGenerico();
            
            if (!TieneRolAdministradorDeNegocio)
            {
                establecimientoSesion.CentrosAtencion = new List<ItemGenerico> { CajaSesion };
            }

            if (TieneRolAdministradorDeNegocio)
            {
                establecimientosConSusCajasCuentas = Establecimiento.Convert(_establecimientoLogica.ObtenerEstablecimientosComercialesVigentesConSusCajas());
                establecimientosConSusCajasCuentas.ForEach(ec => ec.CentrosAtencion.ForEach(ca => ca.Valor = "false"));
                var cuentasBancarias = _operacionLogica.ObtenerCuentasBancariasConEntidadFinancieraConMoneda();
                cuentasBancarias.ForEach(cb => cb.Valor = "true");
                establecimientosConSusCajasCuentas.ForEach(e => e.CentrosAtencion.AddRange(cuentasBancarias));
            }
            var mediosPago = _maestroDatos.ObtenerDetallesComoItemsGenericos(MaestroSettings.Default.IdMaestroMedioDePago).ToList();
            var data = new PrincipalReportData()
            {
                FechaActual_ = DateTimeUtil.FechaActual(),
                EstablecimientoSesion = establecimientoSesion,
                EsAdministrador = TieneRolAdministradorDeNegocio,
                Establecimientos = TieneRolAdministradorDeNegocio ? establecimientosConSusCajasCuentas : new List<Establecimiento>() { establecimientoSesion },
                Cajas = TieneRolAdministradorDeNegocio ? establecimientosConSusCajasCuentas.SelectMany(e => e.CentrosAtencion).Where(ca =>ca.Valor == "false").ToList() : new List<ItemGenerico>() { profileData.CentroDeAtencionSeleccionado.ToItemGenerico() },
                MediosPago = mediosPago,
                MediosPagoCuenta = mediosPago.Where(m => m.Id == MaestroSettings.Default.IdDetalleMaestroMedioDepagoTransferenciaDeFondos || m.Id == MaestroSettings.Default.IdDetalleMaestroMedioDepagoDepositoEnCuenta).ToList(),
                OperacionesIngresos = _transaccionDatos.ObtenerTipoDeTransaccionPorAccionDeNegocio(TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnCaja, true).ToList(),
                OperacionesEgresos = _transaccionDatos.ObtenerTipoDeTransaccionPorAccionDeNegocio(TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnCaja, false).ToList()

            };
            return data;
        }

        public List<IngresoEgreso> Ingresos(bool esCuenta, int idCajaCuenta, DateTime fechaDesde, DateTime fechaHasta, bool todosLosMediosPago, int[] mediosPago, bool todasLasOperaciones, int[] operaciones)
        {
            try
            {
                List<IngresoEgreso> ingresos;
                if (esCuenta)
                {
                    ingresos = (todosLosMediosPago ? (todasLasOperaciones ? _finanzaReportingDatos.ObtenerIngresosEgresosEnCuentaBancaria(idCajaCuenta, fechaDesde, fechaHasta, true).ToList() : _finanzaReportingDatos.ObtenerIngresosEgresosEnCuentaBancariaPorOperaciones(idCajaCuenta, fechaDesde, fechaHasta, operaciones).ToList()) : (todasLasOperaciones ? _finanzaReportingDatos.ObtenerIngresosEgresosEnCuentaBancariaPorMediosPago(idCajaCuenta, fechaDesde, fechaHasta, true, mediosPago).ToList() : _finanzaReportingDatos.ObtenerIngresosEgresosEnCuentaBancariaPorOperacionesYMediosPago(idCajaCuenta, fechaDesde, fechaHasta, operaciones, mediosPago).ToList()));
                }
                else
                {
                    ingresos = (todosLosMediosPago ? (todasLasOperaciones ? _finanzaReportingDatos.ObtenerIngresosEgresos(idCajaCuenta, fechaDesde, fechaHasta, true).ToList() : _finanzaReportingDatos.ObtenerIngresosEgresosPorOperaciones(idCajaCuenta, fechaDesde, fechaHasta, operaciones).ToList()) : (todasLasOperaciones ? _finanzaReportingDatos.ObtenerIngresosEgresosPorMediosPago(idCajaCuenta, fechaDesde, fechaHasta, true, mediosPago).ToList() : _finanzaReportingDatos.ObtenerIngresosEgresosPorOperacionesYMediosPago(idCajaCuenta, fechaDesde, fechaHasta, operaciones, mediosPago).ToList()));
                }
                ingresos = ingresos.OrderBy(m => m.Fecha).ToList();
                return ingresos;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener Ingresos", e);
            }
        }

        public List<IngresoEgreso> Egresos(bool esCuenta, int idCajaCuenta, DateTime fechaDesde, DateTime fechaHasta, bool todosLosMediosPago, int[] mediosPago, bool todasLasOperaciones, int[] operaciones)
        {
            try
            {
                List<IngresoEgreso> egresos;
                if (esCuenta)
                {
                    egresos = (todosLosMediosPago ? (todasLasOperaciones ? _finanzaReportingDatos.ObtenerIngresosEgresosEnCuentaBancaria(idCajaCuenta, fechaDesde, fechaHasta, false).ToList() : _finanzaReportingDatos.ObtenerIngresosEgresosEnCuentaBancariaPorOperaciones(idCajaCuenta, fechaDesde, fechaHasta, operaciones).ToList()) : (todasLasOperaciones ? _finanzaReportingDatos.ObtenerIngresosEgresosEnCuentaBancariaPorMediosPago(idCajaCuenta, fechaDesde, fechaHasta, false, mediosPago).ToList() : _finanzaReportingDatos.ObtenerIngresosEgresosEnCuentaBancariaPorOperacionesYMediosPago(idCajaCuenta, fechaDesde, fechaHasta, operaciones, mediosPago).ToList()));
                }
                else
                {
                    egresos = (todosLosMediosPago ? (todasLasOperaciones ? _finanzaReportingDatos.ObtenerIngresosEgresos(idCajaCuenta, fechaDesde, fechaHasta, false).ToList() : _finanzaReportingDatos.ObtenerIngresosEgresosPorOperaciones(idCajaCuenta, fechaDesde, fechaHasta, operaciones).ToList()) : (todasLasOperaciones ? _finanzaReportingDatos.ObtenerIngresosEgresosPorMediosPago(idCajaCuenta, fechaDesde, fechaHasta, false, mediosPago).ToList() : _finanzaReportingDatos.ObtenerIngresosEgresosPorOperacionesYMediosPago(idCajaCuenta, fechaDesde, fechaHasta, operaciones, mediosPago).ToList()));
                }
                egresos = egresos.OrderBy(m => m.Fecha).ToList();
                return egresos;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener Egresos", e);
            }
        }

        public decimal ObtenerSaldo(int idCajaCuenta, DateTime fecha, bool esCuenta)
        {
            var ultimoArqueoDeCaja = _transaccionDatos.ObtenerUltimaTransaccionAntesDe(idCajaCuenta, TransaccionSettings.Default.IdTipoTransaccionArqueoCaja, fecha);
            var hayArqueoDeCajaAnterior = ultimoArqueoDeCaja != null;
            DateTime fechaPrimeraTransaccion = (DateTime)_transaccionDatos.ObtenerFechaPrimeraTransaccion();
            var fechaDesdeParaSaldoInicial = hayArqueoDeCajaAnterior ? ultimoArqueoDeCaja.fecha_inicio : fechaPrimeraTransaccion;
            var fechaHastaParaSaldoInicial = fecha;

            var movimientosParaSaldoInicial = esCuenta ? _finanzaReportingDatos.ObtenerIngresosEgresosEnCuentaBancaria(idCajaCuenta, fechaDesdeParaSaldoInicial, fechaHastaParaSaldoInicial).ToList() : _finanzaReportingDatos.ObtenerIngresosEgresos(idCajaCuenta, fechaDesdeParaSaldoInicial, fechaHastaParaSaldoInicial).ToList();

            var saldoInicial = (hayArqueoDeCajaAnterior ? ultimoArqueoDeCaja.importe_total : 0) + (movimientosParaSaldoInicial.Count > 0 ? movimientosParaSaldoInicial.Where(m => m.EsIngreso).Sum(m => m.Importe) - movimientosParaSaldoInicial.Where(m => !m.EsIngreso).Sum(m => m.Importe) : 0);
            return saldoInicial;
        }

        public FlujoIngresoEgreso Flujo(bool esCuenta, int idCajaCuenta, DateTime fechaDesde, DateTime fechaHasta, bool todosLosMediosPago, int[] mediosPago)
        {
            try
            {
                var saldoInicial = ObtenerSaldo(idCajaCuenta, fechaDesde, esCuenta);
                var detallesFlujo = esCuenta ? (todosLosMediosPago ? _finanzaReportingDatos.ObtenerIngresosEgresosEnCuentaBancaria(idCajaCuenta, fechaDesde, fechaHasta).OrderBy(m => m.Fecha).ToList() : _finanzaReportingDatos.ObtenerIngresosEgresosEnCuentaBancariaPorMediosPago(idCajaCuenta, fechaDesde, fechaHasta, mediosPago).OrderBy(m => m.Fecha).ToList()) : (todosLosMediosPago ? _finanzaReportingDatos.ObtenerIngresosEgresos(idCajaCuenta, fechaDesde, fechaHasta).OrderBy(m => m.Fecha).ToList() : _finanzaReportingDatos.ObtenerIngresosEgresosPorMediosPago(idCajaCuenta, fechaDesde, fechaHasta, mediosPago).OrderBy(m => m.Fecha).ToList());

                var saldo = saldoInicial;
                foreach (var detalleFlujo in detallesFlujo)
                {
                    saldo += (detalleFlujo.EsIngreso ? detalleFlujo.Importe : (-1 * detalleFlujo.Importe));
                    detalleFlujo.Saldo = saldo;
                }
                var saldoFinal = saldo;
                return new FlujoIngresoEgreso()
                {
                    Resumen = new ResumenFlujo()
                    {
                        SaldoInicial = saldoInicial,
                        Ingresos = detallesFlujo.Where(df => df.EsIngreso).Sum(df => df.Importe),
                        Egresos = detallesFlujo.Where(df => !df.EsIngreso).Sum(df => df.Importe),
                        SaldoFinal = saldoFinal
                    },
                    Detalles = detallesFlujo
                };
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener Flujo", e);
            }
        }

        public List<OperacionGrupo> ObtenerCuentasPorCobrarGrupos(bool todosLosGrupos, int[] idsGrupos)
        {
            try
            {
                idsGrupos = todosLosGrupos ? _actorNegocioLogica.ObtenerGruposActoresComerciales().Select(g => g.Id).ToArray() : idsGrupos;
                List<OperacionGrupo> porCobrarGrupos = _finanzaReportingDatos.ObtenerCuentasPorCobarGrupos(idsGrupos).ToList();
                var gruposPorCobrarGrupos = porCobrarGrupos.GroupBy(g => new { g.IdGrupo, g.NombreGrupo, g.DocumentoResponsable, g.NombreResponsable, g.IdCliente, g.NombreCliente });
                var resultado = gruposPorCobrarGrupos.Select(g => new OperacionGrupo()
                {
                    IdGrupo = g.Key.IdGrupo,
                    NombreGrupo = g.Key.NombreGrupo,
                    DocumentoResponsable = g.Key.DocumentoResponsable,
                    NombreResponsable = g.Key.NombreResponsable,
                    IdCliente = g.Key.IdCliente,
                    NombreCliente = g.Key.NombreCliente,
                    InfoComprobante = String.Join(", ", g.Select(gg => gg.InfoComprobante).Distinct().ToArray()),
                    NumeroOperaciones = g.Select(gg => gg.InfoComprobante).Distinct().Count(),
                    Importe = g.Sum(gg => gg.ImporteTotal),
                    Revocado = g.Sum(gg => gg.RevocadoTotal),
                    ACuenta = g.Sum(gg => gg.ACuentaTotal),
                    Saldo = g.Sum(gg => gg.SaldoTotal),
                }).ToList();
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener cuentas por cobrar por grupos", e);
            }
        }
        public List<OperacionGrupoDetallado> ObtenerCuentasPorCobrarGrupoDetallado(int idGrupo)
        {
            try
            {
                List<OperacionGrupoDetallado> porCobrarGruposDetallado = _finanzaReportingDatos.ObtenerCuentasPorCobarGrupoDetallado(idGrupo).ToList();
                var gruposPorCobrarGrupos = porCobrarGruposDetallado.GroupBy(g => new { g.Id, g.IdTipoTransaccion, g.NombreResponsable, g.DocumentoCliente, g.NombreCliente, g.Emision, g.TipoComprobante, g.IdComprobante });
                var resultado = gruposPorCobrarGrupos.Select(g => new OperacionGrupoDetallado()
                {
                    Id = g.Key.Id,
                    IdTipoTransaccion = g.Key.IdTipoTransaccion,
                    NombreResponsable = g.Key.NombreResponsable,
                    DocumentoCliente = g.Key.DocumentoCliente,
                    NombreCliente = g.Key.NombreCliente,
                    Emision = g.Key.Emision,
                    TipoComprobante = g.Key.TipoComprobante,
                    IdComprobante = g.Key.IdComprobante,
                    SerieYNumeroComprobante = g.First().SerieYNumeroComprobante,
                    Importe = g.Sum(gg => gg.Importe),
                    Revocado = g.Sum(gg => gg.Revocado),
                    ACuenta = g.Sum(gg => gg.ACuenta),
                    Saldo = g.Sum(gg => gg.Saldo)
                }).ToList();
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener cuentas por cobrar por grupo detallado", e);
            }
        }
    }
}

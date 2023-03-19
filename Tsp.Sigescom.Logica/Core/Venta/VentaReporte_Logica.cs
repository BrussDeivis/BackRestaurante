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
using Tsp.Sigescom.Modelo.Negocio.Venta;
using Tsp.Sigescom.Modelo.Negocio.Venta.Report;
using Tsp.Sigescom.Modelo.Negocio.Core.Actor;
using Tsp.Sigescom.Modelo.Negocio.Establecimientos;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Venta;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Finanza.PlainModel;
using Tsp.Sigescom.Modelo.Interfaces.Logica;

namespace Tsp.Sigescom.Logica.Core.Venta
{
    public class VentaReporte_Logica : IVentaReporte_Logica
    {
        protected readonly IVentaReporte_Repositorio _ventaReporteDatos;
        protected readonly IMaestrosVenta_Repositorio _maestrosVentaDatos;
        protected readonly IEstablecimiento_Logica _establecimientoLogica;
        protected readonly IActorNegocioLogica _actorNegocioLogica;

        public VentaReporte_Logica(IVentaReporte_Repositorio ventaReporteDatos, IMaestrosVenta_Repositorio maestrosVentaDatos, IEstablecimiento_Logica establecimientoLogica, IActorNegocioLogica actorNegocioLogica)
        {
            _ventaReporteDatos = ventaReporteDatos;
            _maestrosVentaDatos = maestrosVentaDatos;
            _establecimientoLogica = establecimientoLogica;
            _actorNegocioLogica = actorNegocioLogica;
        }

        public PrincipalReportData ObtenerDatosParaReportePrincipal(UserProfileSessionData profileData)
        {
            var TieneRolAdministradorDeNegocio = profileData.Empleado.TieneRol(ActorSettings.Default.idRolAdministradorDeNegocio);
            if (profileData.CentroDeAtencionSeleccionado == null && !TieneRolAdministradorDeNegocio)
            {
                throw new LogicaException("No cuenta con suficientes permisos para acceder a este reporte");
            }
            var establecimientosConPuntosVentas = TieneRolAdministradorDeNegocio ? Establecimiento.Convert(_establecimientoLogica.ObtenerEstablecimientosComercialesVigentesConSusPuntosVentas().ToList()) : null;

            var data = new PrincipalReportData()
            {
                FechaActual_ = DateTimeUtil.FechaActual(),
                EsAdministrador = TieneRolAdministradorDeNegocio,
                Establecimientos = TieneRolAdministradorDeNegocio ? establecimientosConPuntosVentas : new List<Establecimiento>() { profileData.EstablecimientoComercialSeleccionado.ToEstablecimiento() },
                PuntosVentas = TieneRolAdministradorDeNegocio ? establecimientosConPuntosVentas.SelectMany(e => e.CentrosAtencion).ToList() : new List<ItemGenerico>() { profileData.CentroDeAtencionSeleccionado.ToItemGenerico() },
                Familias = _maestrosVentaDatos.ObtenerFamilias().ToList()
            };
            if (!TieneRolAdministradorDeNegocio) data.Establecimientos.SingleOrDefault().CentrosAtencion = new List<ItemGenerico>() { profileData.CentroDeAtencionSeleccionado.ToItemGenerico() };
            return data;
        }
        public List<OperacionFamiliaGrupo> ObtenerVentasPorFamiliasGrupos(int idPuntoVenta, DateTime fechaDesde, DateTime fechaHasta, bool todasLasFamilias, int[] idsFamilias, bool todosLosGrupos, int[] idsGrupos)
        {
            try
            {
                idsFamilias = todasLasFamilias ? _maestrosVentaDatos.ObtenerFamilias().Select(g => g.Id).ToArray() : idsFamilias;
                var idsFamiliasList = idsFamilias.ToList();
                idsFamiliasList.Add(MaestroSettings.Default.IdDetalleMaestroConceptoDescuento);
                idsFamiliasList.Add(MaestroSettings.Default.IdDetalleMaestroConceptoInteres);
                idsGrupos = todosLosGrupos ? _actorNegocioLogica.ObtenerGruposActoresComerciales().Select(g => g.Id).ToArray() : idsGrupos;
                var idsTipoTransaccion = Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasExceptoInvalidaciones;
                var idEstado = MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado;
                List<OperacionFamiliaGrupo> ventasPorFamiliasGrupos = _ventaReporteDatos.ObtenerVentasPorFamiliasGrupos(idsTipoTransaccion, idEstado, idPuntoVenta, fechaDesde, fechaHasta, idsFamiliasList.ToArray(), idsGrupos).ToList();
                return ventasPorFamiliasGrupos;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener ventas por familias grupos", e);
            }
        }
        public List<OperacionGrupo> ObtenerVentasPorGrupos(int idPuntoVenta, DateTime fechaDesde, DateTime fechaHasta, bool todosLosGrupos, int[] idsGrupos)
        {
            try
            {
                idsGrupos = todosLosGrupos ? _actorNegocioLogica.ObtenerGruposActoresComerciales().Select(g => g.Id).ToArray() : idsGrupos;
                var idsTipoTransaccion = Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasExceptoInvalidaciones;
                var idEstado = MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado;
                List<OperacionGrupo> ventasPorGrupos = _ventaReporteDatos.ObtenerVentasPorGrupos(idsTipoTransaccion, idEstado, idPuntoVenta, fechaDesde, fechaHasta, idsGrupos).ToList();
                var gruposVentasPorGrupos = ventasPorGrupos.GroupBy(g => new { g.IdGrupo, g.NombreGrupo, g.DocumentoResponsable, g.NombreResponsable, g.IdCliente, g.NombreCliente });
                var resultado = gruposVentasPorGrupos.Select(g => new OperacionGrupo()
                {
                    IdGrupo = g.Key.IdGrupo,
                    NombreGrupo = g.Key.NombreGrupo,
                    DocumentoResponsable = g.Key.DocumentoResponsable,
                    NombreResponsable = g.Key.NombreResponsable,
                    IdCliente = g.Key.IdCliente,
                    NombreCliente = g.Key.NombreCliente,
                    InfoComprobante = String.Join(", ", g.Select(gg => gg.InfoComprobante).Distinct().ToArray()),
                    NumeroOperaciones = g.Select(gg => gg.InfoComprobante).Distinct().Count(),
                    Importe = g.Sum(gg => gg.ImporteTotal)
                }).ToList();
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener ventas por grupos", e);
            }
        }
        public List<OperacionGrupoDetallado> ObtenerVentasPorGrupoDetallado(int idPuntoVenta, DateTime fechaDesde, DateTime fechaHasta, int idGrupo)
        {
            try
            {
                var idsTipoTransaccion = Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasExceptoInvalidaciones;
                var idEstado = MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado;
                List<OperacionGrupoDetallado> ventasPorGrupoDetallado = _ventaReporteDatos.ObtenerVentasPorGrupoDetallado(idsTipoTransaccion, idEstado, idPuntoVenta, fechaDesde, fechaHasta, idGrupo).ToList();
                return ventasPorGrupoDetallado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener ventas por grupo detallado", e);
            }
        }
    }
}

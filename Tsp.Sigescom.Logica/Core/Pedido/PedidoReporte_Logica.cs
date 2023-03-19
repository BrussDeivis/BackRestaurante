using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Pedido.ReportData;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Entidades.Sesion;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Venta;
using Tsp.Sigescom.Modelo.Interfaces.Negocio.Pedido;
using Tsp.Sigescom.Modelo.Negocio.Core.Actor;
using Tsp.Sigescom.Modelo.Negocio.Establecimientos;

namespace Tsp.Sigescom.Logica.Core.Pedido
{
    public class PedidoReporte_Logica : IPedidoReporte_Logica
    {
        protected readonly IEstablecimiento_Logica _establecimientoLogica;
        protected readonly IMaestrosVenta_Repositorio _maestrosVentaDatos;


        public PedidoReporte_Logica(IEstablecimiento_Logica establecimiento_Logica, IMaestrosVenta_Repositorio maestrosVentaDatos)
        {
            _establecimientoLogica = establecimiento_Logica;
            _maestrosVentaDatos = maestrosVentaDatos;
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
                Establecimientos = TieneRolAdministradorDeNegocio ? establecimientosConPuntosVentas : new List<Establecimiento>() { profileData.EstablecimientoComercialSeleccionado.ToEstablecimiento() }
            };
            if (!TieneRolAdministradorDeNegocio) data.Establecimientos.SingleOrDefault().CentrosAtencion = new List<ItemGenerico>() { profileData.CentroDeAtencionSeleccionado.ToItemGenerico() };
            return data;
        }

    }
}

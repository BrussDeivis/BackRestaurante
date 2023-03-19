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
using Tsp.Sigescom.Modelo.Entidades.Custom.configuraciones;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Entidades.Sesion;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Restaurant;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;
using Tsp.Sigescom.Modelo.Negocio.Establecimientos;
using Tsp.Sigescom.Modelo.Negocio.Restaurant;
using Tsp.Sigescom.Modelo.SigesRestaurant;

namespace Tsp.Sigescom.Parking.Logica
{
    public partial class Caja_Logica : ICaja_Logica
    {

        private readonly IAtencion_Repositorio _atencionRepositorio;
        private readonly IMesa_Repositorio _mesaRepositorio;
        private readonly IEstablecimiento_Logica _establecimientoLogica;






        public Caja_Logica(IAtencion_Repositorio atencionRepositorio, IMesa_Repositorio mesaRepositorio, IEstablecimiento_Logica establecimientoLogica)
        {
            _atencionRepositorio = atencionRepositorio;
            _mesaRepositorio = mesaRepositorio;
            _establecimientoLogica = establecimientoLogica;
        }

        public ConfiguracionRestauranteCaja ObtenerConfiguracionParaCaja(SesionRestaurante sesion)
        {
            var fechaActual = DateTimeUtil.FechaActual();
            var configuracion = new ConfiguracionRestauranteCaja()
            {
                UsuarioTieneRolCajero = sesion.SesionDeUsuario.Empleado.TieneRol(ActorSettings.Default.IdRolCajero),
                FechaDesde = fechaActual.Date.AddDays(-RestauranteSettings.Default.MaximoDiasConsultaAtenciones),
                FechaHasta = fechaActual.Date.AddDays(1).AddTicks(-1)
            };
            return configuracion;
        }
    }
}



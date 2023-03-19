using System.Collections.Generic;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades.Sesion;
using Tsp.Sigescom.Modelo.SigesRestaurant;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.configuraciones
{
    public class ConfiguracionRestauranteAtencion
    {
        public ConfiguracionRestauranteAtencion() 
        {
            
        }
        public readonly int IdMedioPagoDefault = MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo;
        public readonly int TipoDePagoSimple = (int)TipoPago.Simple;
        public readonly int TipoDePagoCuentaDividida = (int)TipoPago.DivididoPorMonto;
        public readonly int TipoDePagoCuentaDiferenciadaDetallada = (int)TipoPago.DivididoPorItem;
        public readonly int IdEstadoRegistrado = MaestroSettings.Default.IdDetalleMaestroEstadoRegistrado;
        public readonly int IdEstadoCerrado = MaestroSettings.Default.IdDetalleMaestroEstadoCerrado;
        public readonly int IdEstadoFinalizado = MaestroSettings.Default.IdDetalleMaestroEstadoFinalizado;
        public readonly int IdEstadoAnulado = MaestroSettings.Default.IdDetalleMaestroEstadoAnulado;
        public readonly ConfiguracionRestauranteOrden ConfiguracionDeOrden = ConfiguracionRestauranteOrden.Default;
        public readonly int IdRolCliente = ActorSettings.Default.IdRolCliente;

        public readonly int IdRolMozo = RestauranteSettings.Default.IdRolMozo;
        public readonly int IdRolCajero = ActorSettings.Default.IdRolCajero;
        public readonly int IdCategoriaNula = ConceptoSettings.Default.IdCategoriaNula;
        public readonly bool ModuloPreparacionActivado = RestauranteSettings.Default.ModuloPreparacionActivado;
        public readonly bool PermitirCierreRapidoDeAtencion= RestauranteSettings.Default.PermitirCierreRapidoDeAtencion;
        public string NombreRolCliente{ get; set; }
        public int IdEmpleado{ get; set; }

        public bool UsuarioTieneRolCajero{ get; set; }
        public bool PermitirVentaEnMesa { get; set; }
        public bool PermitirVentaPorDelivery { get; set; }
        public bool PermitirVentaAlPaso { get; set; }

        public ItemGenerico EstablecimientoSesion { get; set; }
        public bool UsuarioTieneRolAdministradorDeNegocio { get; set; }
        public bool UsuarioTieneRolCajeroYMozo { get; set; }
        public List<ItemGenerico> Establecimientos { get; set; }


    }
}


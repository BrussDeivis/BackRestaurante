using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.configuraciones
{
    public class ConfiguracionReserva
    {
        public readonly int IdRolCliente = ActorSettings.Default.IdRolCliente;
        public readonly int IdRolHuesped = HotelSettings.Default.IdRolHuesped;
        public readonly int NumeroDecimalesEnCantidad = AplicacionSettings.Default.NumeroDecimalesEnCantidad;
        public readonly int TiempoEsperaBusquedaSelector = AplicacionSettings.Default.TiempoDeEsperaEnBusquedaSelectoresDeGranCantidad;
        public readonly int MinimoCaracteresBuscarActorComercial = ActorSettings.Default.MinimoDeCaracteresParaBuscarEnSelectorActorComercial;
        public readonly int IdMedioDePagoEfectivo = MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo;
        public readonly int IdClienteGenerico = ActorSettings.Default.IdClienteGenerico;
        public readonly int DiasMaximoAnticipacionReserva = HotelSettings.Default.DiasMaximoAnticipacionReserva;
        public readonly int DiasMaximoDuracionReserva = HotelSettings.Default.DiasMaximoDuracionReserva;
        public readonly string MascaraDeVisualizacionValidacionRegistroCliente = ActorSettings.Default.MascaraDeVisualizacionValidacionRegistroCliente;
        public string FechaActual;
        public bool AgregarDiaAFechaDesde; 

    }
}


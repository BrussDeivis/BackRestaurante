using System.Collections.Generic;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;

namespace Tsp.Sigescom.Modelo.Entidades
{
    public static class DiccionarioHotel
    {
        public class AccionesEstadoHabitacion
        {
            public List<ItemGenerico> General { get; set; }
            public List<ItemGenerico> FechaPasada { get; set; }

        }
        static ItemGenerico Reservar = new ItemGenerico() { Id = (int)AccionHabitacion.Reservar, Nombre = "Reservar", Valor = "glyphicon glyphicon-plus", Codigo = "abrirNuevaReserva" };
        static ItemGenerico Ver = new ItemGenerico() { Id = (int)AccionHabitacion.Ver, Nombre = "Ver Reserva", Valor = "fa fa-search", Codigo = "verDetalleReserva" };
        static ItemGenerico CambiarHabitacion = new ItemGenerico() { Id = (int)AccionHabitacion.CambiarHabitacion, Nombre = "Cambiar Habitacion", Valor = "fa fa-exchange" };
        static ItemGenerico CheckOut = new ItemGenerico() { Id = (int)AccionHabitacion.CheckOut, Nombre = "CheckOut", Valor = "fa fa-sign-out" };
        static ItemGenerico CheckIn = new ItemGenerico() { Id = (int)AccionHabitacion.CheckIn, Nombre = "CheckIn", Valor = "fa fa-sign-in" };
        static ItemGenerico Consumos = new ItemGenerico() { Id = (int)AccionHabitacion.Consumos, Nombre = "Consumos", Valor = "fa fa-cart-arrow-down", Codigo = "registrarConsumo" };
        static ItemGenerico Facturacion = new ItemGenerico() { Id = (int)AccionHabitacion.Facturacion, Nombre = "Facturacion", Valor = "fa fa-money" };
        static ItemGenerico AnularReserva = new ItemGenerico() { Id = (int)AccionHabitacion.AnularReserva, Nombre = "Anular Reserva", Valor = "fa fa-minus-circle" };
        static ItemGenerico AmpliarAtencion = new ItemGenerico() { Id = (int)AccionHabitacion.AmpliarAtencion, Nombre = "Ampliar Atencion", Valor = "fa fa-calendar-plus-o" };
        static ItemGenerico IniciarLimpieza = new ItemGenerico() { Id = (int)AccionHabitacion.IniciarLimpieza, Nombre = "Iniciar Limpieza", Valor = "flaticon-household" };
        static ItemGenerico FinalizarLimpieza = new ItemGenerico() { Id = (int)AccionHabitacion.FinalizarLimpieza, Nombre = "Finalizar Limpieza", Valor = "flaticon-household" };
        //public static List<ItemGenerico> AccionesDeEstadoHabitacionDisponible()
        //{
        //    List<ItemGenerico> list = new List<ItemGenerico> {
        //        Reservar,
        //        IniciarLimpieza,
        //        FinalizarLimpieza
        //    };
        //    return list;
        //}
        
        //public static List<ItemGenerico> AccionesDeEstadoHabitacionReservada()
        //{
        //    List<ItemGenerico> list = new List<ItemGenerico> {
        //        Ver,
        //        IniciarLimpieza,
        //        FinalizarLimpieza
        //    };
        //    return list;
        //}
        //public static List<ItemGenerico> AccionesDeEstadoHabitacionOcupada()
        //{
        //    List<ItemGenerico> list = new List<ItemGenerico> {
        //        Ver,
        //        IniciarLimpieza,
        //        FinalizarLimpieza
        //    };
        //    return list;
        //}
        public static AccionesEstadoHabitacion AccionesDeEstadoHabitacionDisponible()
        {
            AccionesEstadoHabitacion acciones = new AccionesEstadoHabitacion
            {
                General = new List<ItemGenerico>
                {
                    Reservar,
                }
                
            };
            return acciones;
        }
        public static AccionesEstadoHabitacion AccionesDeEstadoHabitacionReservada()
        {
            AccionesEstadoHabitacion acciones = new AccionesEstadoHabitacion
            {
                General = new List<ItemGenerico>
                {
                    Ver
                },
                FechaPasada = new List<ItemGenerico>
                {
                    Ver
                }
            };
            return acciones;
        }
        public static AccionesEstadoHabitacion AccionesDeEstadoHabitacionOcupada()
        {
            AccionesEstadoHabitacion acciones = new AccionesEstadoHabitacion
            {
                General = new List<ItemGenerico>
                {
                    Ver,
                    Consumos
                },
                FechaPasada = new List<ItemGenerico>
                {
                    Ver,
                    
                }
            };
            return acciones;
        }
        public static Dictionary<int, string> AccionesProcesoDeHabitacion = new Dictionary<int, string> {
            {(int)AccionProcesoHabitacion.Confirmar,"Confirmar Reserva"},
            {(int)AccionProcesoHabitacion.CheckIn,"Registrar Check In"},
            {(int)AccionProcesoHabitacion.CheckOut,"Registrar Check Out"},
            {(int)AccionProcesoHabitacion.Facturar,"Facturar"},
            {(int)AccionProcesoHabitacion.CambiarHabitacion,"Cambio De Habitaicon"},
            {(int)AccionProcesoHabitacion.AnularReserva,"Anukar Reserva"},
            {(int)AccionProcesoHabitacion.EditarFecha,"Editar Fecha De Registro"}
        };

        public static Dictionary<int, string> ValorEstado = new Dictionary<int, string> {
            {MaestroSettings.Default.IdDetalleMaestroEstadoRegistrado,"orange"},
            {MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado,"yellow"},
            {MaestroSettings.Default.IdDetalleMaestroEstadoFacturado,"aqua"},
            {MaestroSettings.Default.IdDetalleMaestroEstadoCheckedIn,"green"},
            {MaestroSettings.Default.IdDetalleMaestroEstadoSalidaCambiado,"mediumvioletred"},
            {MaestroSettings.Default.IdDetalleMaestroEstadoEntradaCambiado,"limegreen"},
            {MaestroSettings.Default.IdDetalleMaestroEstadoCheckedOut,"brown"},
            {MaestroSettings.Default.IdDetalleMaestroEstadoAnulado,"red"},
            {MaestroSettings.Default.IdDetalleMaestroEstadoIncidente,"purple"},
        };

        public static int[] IdsMediosDePagoParaGeneracionAutomaticaDeComprobante = {
            (int)MaestroSettings.Default.IdDetalleMaestroMedioDePagoTarjetaCredito,
            (int)MaestroSettings.Default.IdDetalleMaestroMedioDePagoTarjetaDebito,
        };
    }
}

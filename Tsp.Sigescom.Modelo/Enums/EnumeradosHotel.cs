using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Tsp.Sigescom.Modelo.Entidades
{
    public enum AccionHabitacion
    {
        Reservar = 1,
        Ver = 2,
        CambiarHabitacion = 3,
        CheckOut = 4,
        CheckIn = 5,
        Consumos = 6,
        Facturacion = 7,
        AnularReserva = 8,
        AmpliarAtencion = 9,
        IniciarLimpieza = 10,
        FinalizarLimpieza = 11
    }
    public enum AccionProcesoHabitacion
    {
        Confirmar = 1,
        CheckOut = 2,
        CheckIn = 3,
        Facturar = 4,
        CambiarHabitacion = 5,
        AnularReserva = 6,
        EditarFecha = 7
    }

    public enum EstadoHabitacionEnum
    {
        Todo = 0,
        Disponible = 1,
        Ocupado = 2,
        Reservado = 3,
        OcupadoDisponible = 4,
    }
    public enum ModoFacturacionHotel
    {  
        [Description("Global")]
        Global = 1,
        [Description("Individual")]
        Individual = 2,
        [Description("No Especificado")]
        NoEspecificado = 3,
    }
    public static class EnumeradosHotel
    {
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attr = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            //string descripcion = (attr.Length > 0) ? (attr[0] as DescriptionAttribute).Description : value.ToString();
            return (attr.Length > 0) ? (attr[0] as DescriptionAttribute).Description : value.ToString();
            //return (attr.Length == 0) ? value.ToString() : (attr[0] as DescriptionAttribute).Description;
        }
    }
}

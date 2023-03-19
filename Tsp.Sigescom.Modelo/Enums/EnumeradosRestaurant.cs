using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Tsp.Sigescom.Modelo.Entidades
{

    public enum EstadoDeDetalleDeOrden
    {
        Registrado = 0,
        Preparacion = 1,
        Servido = 2,
        Anulado = 3,
        Atendido = 4,
        Devuelto = 5,
        Observado = 6

    }

  

    public enum ModoAtencionEnum
    {
        [Description("SALON")]
        Salon = 1,
        [Description("DELIVERY")]
        Delivery = 2,
        [Description("AL PASO")]
        AlPaso = 3,
    }


    public enum TipoPago
    {
        [Description("Ninguno")]
        Ninguno = 0,
        [Description("Simple")]
        Simple = 1,
        /// <summary>
        /// La cuenta es pagada por varias personas, los montos podrían ser iguales o distintos.
        /// </summary>
        [Description("Dividido por monto")]
        DivididoPorMonto = 2, 
        [Description("Dividido por item")]
        DivididoPorItem = 3
    }
    public static class EnumeradosRestaurant
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

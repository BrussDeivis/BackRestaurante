using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class NotaViewModel
    {
        [DataMember]
        public long IdCuota;
        public decimal Saldo;
        public decimal MontoPago;

        public NotaViewModel() {}

        //public NotaViewModel(Comprobante c)
        //{

        //}

        //public static List<NotaViewModel> Convert(List<CuentaPorCobrarPagar> lista)
        //{
        //    List<NotaViewModel> respuesta = new List<NotaViewModel>();
        //    foreach (var cuenta in lista)
        //    {
        //        respuesta.Add(new NotaViewModel(cuenta));
        //    }
        //    return respuesta;
        //}
    }
}
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class OperacionBandejaViewModel
    {
        [DataMember]
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Semana { get; set; }
        public string Cliente { get; set; }
        public string Shipper { get; set; }
        public string Consignatario { get; set; }
        public string Moneda { get; set; }
        public decimal TotalSoles { get; set; }
        public decimal TotalDolares { get; set; }
        public string Booking { get; set; }
        public string Bl { get; set; }
        public string Contenedor { get; set; }
        public string Naviera { get; set; }
        public string Nave { get; set; }
        public string Puerto { get; set; }
        public string Producto { get; set; }
        public string Variedad { get; set; }
        public string UnidadNegocio { get; set; }
        public string Estado { get; set; }

        public OperacionBandejaViewModel()
        {

        }

        //public OperacionBandejaViewModel(ServicioPrestadoA sp)
        //{
        //    this.Id = sp.Operacion.Id;
        //    this.Codigo = sp.Operacion.Codigo;
        //    this.Semana = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(Convert.ToDateTime(sp.Operacion.FechaCarga), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday).ToString();
        //    this.Cliente = sp.Operacion.RazonSocialCliente;
        //    this.Shipper = sp.Operacion.Shipper.RazonSocial;
        //    this.Consignatario = sp.Operacion.Consignatario.RazonSocial;
        //    this.Moneda = sp.obtenerContenedorDeCostos().Moneda().Valor;
        //    this.TotalSoles = sp.obtenerContenedorDeCostos().IdMoneda == TransaccionSettings.Default.idDetalleMaestroMonedaSoles ? sp.obtenerContenedorDeCostos().Total : sp.obtenerContenedorDeCostos().Total* sp.obtenerContenedorDeCostos().TipoDeCambio;
        //    this.TotalDolares = sp.obtenerContenedorDeCostos().IdMoneda == TransaccionSettings.Default.idDetalleMaestroMonedaSoles ? sp.obtenerContenedorDeCostos().Total / sp.obtenerContenedorDeCostos().TipoDeCambio : sp.obtenerContenedorDeCostos().Total;
        //    this.Booking = sp.Operacion.Booking();
        //    this.Bl = sp.Operacion.Bl();
        //    this.Contenedor = sp.Operacion.NumeroSerieContenedor();
        //    this.Naviera = sp.Operacion.LineaNaviera()!=null?sp.Operacion.LineaNaviera().NombreCorto:null;
        //    this.Puerto = sp.Operacion.Puerto()!=null?sp.Operacion.Puerto().NombreCorto:null;
        //    this.Producto = sp.Operacion.Producto().Nombre;
        //    this.Nave = sp.Operacion.Nave();
        //    this.Variedad = sp.Operacion.Variedad().Nombre;
        //    this.UnidadNegocio = sp.Operacion.UnidadNegocio;
        //    this.Estado = sp.codigoEstadoActual;
        //}

        //public static new List<OperacionBandejaViewModel> convert(List<ServicioPrestadoA> servicios)
        //{
        //    var lista = new List<OperacionBandejaViewModel>();

        //    foreach (var servicio in servicios)
        //    {
        //        lista.Add(new OperacionBandejaViewModel(servicio));
        //    }
        //    return lista;
        //}
    }
}
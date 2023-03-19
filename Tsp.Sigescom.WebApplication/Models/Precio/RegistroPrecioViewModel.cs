using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class RegistroPrecioViewModel
    {
        [DataMember]
        public int IdConcepto { get; set; }
        public List<RegistroPuntosDePrecioViewModel> PuntosDePrecio { get; set; }

        public RegistroPrecioViewModel()
        {

        }

        public RegistroPrecioViewModel(List<Precio_Compra_Venta_Concepto> precios, int idConcepto, List<ComboGenericoViewModel> totalPuntosDePrecio, List<ComboGenericoViewModel> totalTarifas, DateTime fechaActual)
        {
            this.IdConcepto = idConcepto;
            PuntosDePrecio = new List<RegistroPuntosDePrecioViewModel>();
            Decimal precioCompra = precios.Count != 0 ? precios.First().PrecioCompra : 0;
            foreach (var item in totalPuntosDePrecio)
            {
                PuntosDePrecio.Add(new RegistroPuntosDePrecioViewModel(new ComboGenericoViewModel(item.Id, item.Nombre), precios.Where(p => p.IdPuntoPrecio == item.Id).ToList(), totalTarifas, precioCompra, fechaActual));
            }
        }

        /*
         
            Casos: 
            1. No existe ningun precio
            2. Existe precios en una sucursal  y en la otra no
            3. Si tiene precio, marcar el check box
        */
        public static List<Precio_Compra_Venta_Concepto> Convert(RegistroPrecioViewModel registro)
        {
            List<Precio_Compra_Venta_Concepto> precios = new List<Precio_Compra_Venta_Concepto>(); 
            foreach (var itemPuntoDePrecio in registro.PuntosDePrecio)
            {
                foreach (var itemTarifaDePrecio in itemPuntoDePrecio.Tarifas)
                {
                    if (itemTarifaDePrecio.EsIngresado || itemTarifaDePrecio.IdPrecio != 0)
                    {
                        precios.Add(new Precio_Compra_Venta_Concepto(itemTarifaDePrecio.IdPrecio, itemPuntoDePrecio.PuntoDePrecio.Id, itemTarifaDePrecio.Tarifa.Id, itemTarifaDePrecio.Valor, itemTarifaDePrecio.FechaDesde, itemTarifaDePrecio.FechaHasta, itemTarifaDePrecio.Descripcion, itemTarifaDePrecio.EsIngresado));
                    }
                }
            }
            return precios;
        }
    }

    [Serializable]
    [DataContract]
    public class RegistroPuntosDePrecioViewModel
    {
        [DataMember]
        public ComboGenericoViewModel PuntoDePrecio { get; set; }
        public List<RegistroTarifaPrecioViewModel> Tarifas { get; set; }

        public RegistroPuntosDePrecioViewModel()
        {

        }

        public RegistroPuntosDePrecioViewModel(ComboGenericoViewModel puntoDePrecio, List<Precio_Compra_Venta_Concepto> tarifas, List<ComboGenericoViewModel> totalTarifas, Decimal precioCompra, DateTime fechaActual)
        {
            PuntoDePrecio = puntoDePrecio;
            Tarifas = new List<RegistroTarifaPrecioViewModel>();
            foreach (var item in totalTarifas)
            {
                var tarifa = tarifas.SingleOrDefault(p => p.IdTarifa == item.Id);
                if (tarifa != null)
                {
                    //Seleccionar el check box si no es nulo o vacio el precio de venta
                    bool esNuloOVacioPrecioVenta = string.IsNullOrEmpty(tarifa.PrecioVenta.ToString("N" + AplicacionSettings.Default.NumeroDecimalesEnPrecio));
                    Tarifas.Add(new RegistroTarifaPrecioViewModel(!esNuloOVacioPrecioVenta, tarifa.IdPrecio, tarifa.PrecioCompra.ToString("N" + AplicacionSettings.Default.NumeroDecimalesEnPrecio), tarifa.PrecioVenta.ToString("N" + AplicacionSettings.Default.NumeroDecimalesEnPrecio), new ComboGenericoViewModel(tarifa.IdTarifa, tarifa.Tarifa), tarifa.PrecioVenta, tarifa.FechaInicio, tarifa.FechaFin, tarifa.Descripcion));
                }
                else
                {
                    Tarifas.Add(new RegistroTarifaPrecioViewModel(false, 0, precioCompra.ToString("N" + AplicacionSettings.Default.NumeroDecimalesEnPrecio), 0.ToString("N" + AplicacionSettings.Default.NumeroDecimalesEnPrecio), new ComboGenericoViewModel(item.Id, item.Nombre), 0, fechaActual, fechaActual.AddMonths(ConceptoSettings.Default.PrecioDuracionPorDefectoEnMeses), "NINGUNO"));
                }
            }
        }
    }

    [Serializable]
    [DataContract]
    public class RegistroTarifaPrecioViewModel
    {
        [DataMember]
        public int IdPrecio { get; set; }
        public bool EsIngresado { get; set; }
        public string PrecioCompra { get; set; }
        public string PrecioActual { get; set; }
        public ComboGenericoViewModel Tarifa { get; set; }
        public decimal Valor { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public string Descripcion { get; set; }

        public RegistroTarifaPrecioViewModel()
        {

        }

        public RegistroTarifaPrecioViewModel(bool esIngresado, int idPrecio, string precioCompra, string precioActual, ComboGenericoViewModel tarifa, decimal valor, DateTime fechaDesde, DateTime fechaHasta, string descripcion)
        {
            EsIngresado = esIngresado;
            IdPrecio = idPrecio;
            PrecioCompra = precioCompra;
            PrecioActual = precioActual;
            Tarifa = tarifa;
            Valor = valor;
            FechaDesde = fechaDesde;
            FechaHasta = fechaHasta;
            Descripcion = descripcion;
        }
    }
}
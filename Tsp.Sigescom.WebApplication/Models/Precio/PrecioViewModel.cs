using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class PrecioViewModel
    {
        [DataMember]
        public int Id { get; set; }
        public ComboGenericoViewModel Mercaderia { get; set; }
        public ComboGenericoViewModel Tarifa { get; set; }
        public decimal Valor { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public string Descripcion { get; set; }

        public PrecioViewModel()
        {

        }

        public PrecioViewModel(Precio precio)
        {
            this.Id = precio.id;
            this.Mercaderia = new ComboGenericoViewModel(precio.id_concepto_negocio, precio.Concepto_negocio.nombre);
            this.Tarifa = new ComboGenericoViewModel(precio.id_tarifa_d,precio.Detalle_maestro3.nombre);
            this.Valor = precio.valor;
            this.FechaDesde = precio.fecha_inicio;
            this.FechaHasta = precio.fecha_fin;
            this.Descripcion = precio.descripcion;
        }

    }


    [Serializable]
    [DataContract]
    public class PrecioParaRegistroDeVentaViewModel
    {
        [DataMember]
        public int Id { get; set; }
        public string Tarifa { get; set; }
        public int IdTarifa { get; set; }
        public decimal Valor { get; set; }
        public string Codigo { get; set; }

        public PrecioParaRegistroDeVentaViewModel()
        {

        }


        public PrecioParaRegistroDeVentaViewModel(Precio precio)
        {
            this.Id = precio.id;
            this.Tarifa = precio.Detalle_maestro3.nombre;
            this.Valor = precio.valor;
            this.IdTarifa = precio.id_tarifa_d;
            this.Codigo = precio.Detalle_maestro3.codigo;
        }


        public PrecioParaRegistroDeVentaViewModel(Precio_Concepto_Negocio_Comercial precio)
        {
            this.Id = precio.Id;
            this.Tarifa = precio.Tarifa;
            this.Valor = precio.Valor;
            this.IdTarifa = precio.IdTarifa;
            this.Codigo = precio.Codigo;
        }

        internal static List<PrecioParaRegistroDeVentaViewModel> Convert(List<Precio> precios)
        {
            List<PrecioParaRegistroDeVentaViewModel> resultado = new List<PrecioParaRegistroDeVentaViewModel>();
            foreach (var precio in precios)
            {
                resultado.Add(new PrecioParaRegistroDeVentaViewModel(precio));
            }
            return resultado;
        }

        internal static List<PrecioParaRegistroDeVentaViewModel> Convert(List<Precio_Concepto_Negocio_Comercial> precios)
        {
            List<PrecioParaRegistroDeVentaViewModel> resultado = new List<PrecioParaRegistroDeVentaViewModel>();
            foreach (var precio in precios)
            {
                resultado.Add(new PrecioParaRegistroDeVentaViewModel(precio));
            }
            return resultado;
        }

        internal static List<Precio> Convert(List<PrecioParaRegistroDeVentaViewModel> precios)
        {

            List<Precio> resultado = new List<Precio>();
            Precio precio;
            foreach (var preciovm in precios)
            {
                precio = new Precio();
                precio.id_tarifa_d = preciovm.IdTarifa;
                precio.valor = preciovm.Valor;
                resultado.Add(precio);
            }
            return resultado;
        }

    }

    [Serializable]
    [DataContract]
    public class PrecioParaRegistroMercaderiaUnicaViewModel
    {
        [DataMember]
        public bool Activo { get; set; }
        public int IdTarifa { get; set; }
        public string NombreTarifa{ get; set; }
        public decimal? PrecioActual { get; set; }
        public decimal? NuevoPrecio { get; set; }

        public PrecioParaRegistroMercaderiaUnicaViewModel()
        {

        }


        public PrecioParaRegistroMercaderiaUnicaViewModel(int idTarifa, string nombreTarifa,decimal? valor )

        {


           this.Activo = valor != null ? true : false;
            this.IdTarifa = idTarifa;
            this.NombreTarifa = nombreTarifa;
            this.PrecioActual = valor;
            this.NuevoPrecio = valor;
        }


        //internal static List<PrecioParaRegistroMercaderiaUnicaViewModel> Convert(List<Precio> precios)
        //{
        //    List<PrecioParaRegistroMercaderiaUnicaViewModel> resultados = new List<PrecioParaRegistroMercaderiaUnicaViewModel>();
        //    foreach (var precio in precios)
        //    {
        //        resultados.Add(new PrecioParaRegistroMercaderiaUnicaViewModel(precio));
        //    }
        //    return resultados;

        //}

        internal static List<Precio> Convert(List<PrecioParaRegistroMercaderiaUnicaViewModel> preciosViewModel)
        {
            List<Precio> resultados = new List<Precio>();
            foreach (var precioViewModel in preciosViewModel)
            {

                if (precioViewModel.Activo == true && precioViewModel.NuevoPrecio >= 0)
                {
                    Precio precio = new Precio()
                    {
          
                        id_tarifa_d = precioViewModel.IdTarifa,
                        valor = (decimal)precioViewModel.NuevoPrecio

                    };
                    resultados.Add(precio);
                }



            }
            return resultados;

        }

        //Este metodo sirve para poder listar los tarifas con preccio en caso lo tenga
        internal static List<PrecioParaRegistroMercaderiaUnicaViewModel> Match(List<Detalle_maestro> tarifas, List<Precio> precios)
        {
            List<PrecioParaRegistroMercaderiaUnicaViewModel> resultados = new List<PrecioParaRegistroMercaderiaUnicaViewModel>();
            bool band;
            foreach (var tarifa in tarifas)
            {
                band = false;
                foreach (var precio in precios)
                {
                    if (tarifa.id == precio.id_tarifa_d)
                    {
                        resultados.Add(new PrecioParaRegistroMercaderiaUnicaViewModel(tarifa.id, tarifa.nombre, precio.valor));
                        band = true;
                        break;

                    }
                }

                if (!band)
                {
                    resultados.Add(new PrecioParaRegistroMercaderiaUnicaViewModel(tarifa.id, tarifa.nombre,null));

                }

            }
            

            return resultados;

        }



    }




}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models
{
    public class DireccionViewModel
    {
        public int Id { get; set; }
        public ComboGenericoViewModel Tipo { get; set; }
        public ComboGenericoViewModel Nacion { get; set; }
        public ComboGenericoViewModel Ubigeo { get; set; }
        public ComboGenericoViewModel Via { get; set; }
        public ComboGenericoViewModel Zona { get; set; }
        public string Detalle { get; set; }
        public bool EsVigente { get; set; }
        public bool EsPrincipal { get; set; }


        public DireccionViewModel()
        {

        }

        public DireccionViewModel(Direccion direccion)
        {
            this.Id = direccion.id;
            this.Tipo = new ComboGenericoViewModel(direccion.Detalle_maestro.id, direccion.Detalle_maestro.nombre);
            this.Nacion= new ComboGenericoViewModel(direccion.Detalle_maestro1.id, direccion.Detalle_maestro1.nombre);
            this.Ubigeo = new ComboGenericoViewModel(direccion.Ubigeo.id, direccion.Ubigeo.descripcion_corta);
            //this.Via = new ComboGenericoViewModel(direccion.Detalle_maestro2.id, direccion.Detalle_maestro2.nombre);
            //this.Zona = new ComboGenericoViewModel(direccion.Detalle_maestro3.id, direccion.Detalle_maestro3.nombre);
            this.Detalle = direccion.detalle;
            this.EsVigente = direccion.es_activo;
            this.EsPrincipal = direccion.es_principal;
        }

        public static List<DireccionViewModel> Convert(List<Direccion> direcciones)
        {
            List<DireccionViewModel> direcciones_ = new List<DireccionViewModel>();
            foreach(var item in direcciones)
            {
                direcciones_.Add(new DireccionViewModel(item));
            }
            return direcciones_;
        }

        public static string Direccion(Direccion direccion)

        {
            string direccionViewModel = (direccion.detalle + ", " + direccion.Ubigeo.descripcion_larga).ToUpper();
            return direccionViewModel;
        }

        public static string Direccion(Direccion_ direccion)

        {
            string direccionViewModel = (direccion.Detalle + ", " + direccion.Ubigeo.Nombre).ToUpper();
            return direccionViewModel;
        }

        //public static Direccion Convert(DireccionViewModel direccion)
        //{

        //    return new Direccion
        //    {
        //        direcciones.Add(new Direccion(item.Id, cliente.IdActor, item.Tipo.Id, item.Nacion.Id, item.Ubigeo.Id, item.Detalle,
        //                                null, null, item.EsPrincipal, item.EsVigente));
        //    detalle = direccion.Detalle
        //    }

        //}

    }
}
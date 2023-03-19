using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models.TipoTransaccion
{
    public class RegistroTipoDeTransaccionViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public ComboGenericoViewModel TransaccionMaestro { get; set; }
        public List<AccionDeNegocioPorTipoTransaccionViewModel> AccionesDeNegocioPorTipoDeTransaccion { get; set; }
     
        public RegistroTipoDeTransaccionViewModel()
        {
            this.TransaccionMaestro = new ComboGenericoViewModel();
            this.AccionesDeNegocioPorTipoDeTransaccion = new List<AccionDeNegocioPorTipoTransaccionViewModel>();
        }

        public RegistroTipoDeTransaccionViewModel(TipoDeTransaccion tipoDeTransaccion)
        {
            this.Id = tipoDeTransaccion.Id;
            this.Nombre = tipoDeTransaccion.Nombre;
            this.Descripcion = tipoDeTransaccion.Descripcion;
            this.TransaccionMaestro = new ComboGenericoViewModel(tipoDeTransaccion.TransaccionMaestro().Id, tipoDeTransaccion.TransaccionMaestro().Nombre);
            this.AccionesDeNegocioPorTipoDeTransaccion = AccionDeNegocioPorTipoTransaccionViewModel.Convert(tipoDeTransaccion.ListaDeAccionesDeNegocioPorTipoTransaccion().ToList());

        }

    }

}
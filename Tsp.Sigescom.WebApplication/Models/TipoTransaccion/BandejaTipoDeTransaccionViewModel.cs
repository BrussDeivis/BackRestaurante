using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models.TipoTransaccion
{
    [Serializable]
    [DataContract]
    public class BandejaTipoDeTransaccionViewModel
    {
        [DataMember]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string TransaccionMaestro { get; set; }
        public string AccionesDeNegocioPorTipoDeTransaccion { get; set; }

        public BandejaTipoDeTransaccionViewModel()
        {
        }

        public BandejaTipoDeTransaccionViewModel(TipoDeTransaccion tipoDeTransaccion)
        {
            this.Id = tipoDeTransaccion.Id;
            this.Nombre = tipoDeTransaccion.Nombre;
            this.Descripcion = tipoDeTransaccion.Descripcion;
            this.TransaccionMaestro = tipoDeTransaccion.NombreTransaccionMaestro();
            this.AccionesDeNegocioPorTipoDeTransaccion = CadenaAccionesDeNegocioPorTipoTransaccion(tipoDeTransaccion.ListaDeAccionesDeNegocioPorTipoTransaccion());
        }

        public static List<BandejaTipoDeTransaccionViewModel> Convert(List<TipoDeTransaccion> tiposDeTransaccion)
        {
            List<BandejaTipoDeTransaccionViewModel> tiposDeTransaccionViewModel = new List<BandejaTipoDeTransaccionViewModel>();
            foreach (var item in tiposDeTransaccion)
            {
                tiposDeTransaccionViewModel.Add(new BandejaTipoDeTransaccionViewModel(item));
            }
            return tiposDeTransaccionViewModel;
        }

        private string CadenaAccionesDeNegocioPorTipoTransaccion(List<AccionDeNegocioPorTipoTransaccion> listAccionesDeNegocioPorTipoTransaccion)
        {
            var lista = listAccionesDeNegocioPorTipoTransaccion.Select(antt => new { antt.AccionDeNegocio().Nombre, valor = antt.Valor ? "(E)" : "(S)" }).ToList();
            string accionesDeNegocioPorTipoTransaccion = "";
            foreach (var item in lista)
            {
                accionesDeNegocioPorTipoTransaccion += item.Nombre + " " + item.valor + ", ";
            }
            accionesDeNegocioPorTipoTransaccion = accionesDeNegocioPorTipoTransaccion == "" ? accionesDeNegocioPorTipoTransaccion : accionesDeNegocioPorTipoTransaccion.Substring(0, accionesDeNegocioPorTipoTransaccion.Length - 2);
            return accionesDeNegocioPorTipoTransaccion;
        }

    }
}
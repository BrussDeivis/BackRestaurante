using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Mvc;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.WebApplication.Models
{
    public class AccionDeNegocioPorTipoTransaccionViewModel
    {
        public int IdEntrada { get; set; }
        public int IdSalida { get; set; }
        public int IdAccionDeNegocio { get; set; }
        public string NombreAccionDeNegocio { get; set; }
        public bool Entrada { get; set; }
        public bool Salida { get; set; }

        public AccionDeNegocioPorTipoTransaccionViewModel()
        {

        }
        public AccionDeNegocioPorTipoTransaccionViewModel(int idAccionDeNegocio, string NombreAccionDeNegocio)
        {
            this.IdAccionDeNegocio = idAccionDeNegocio;
            this.NombreAccionDeNegocio = NombreAccionDeNegocio;
        }
        public AccionDeNegocioPorTipoTransaccionViewModel(int idAccionDeNegocio, string NombreAccionDeNegocio,  int idEntrada ,bool entrada, int idSalida , bool salida)
        {
            this.IdAccionDeNegocio = idAccionDeNegocio;
            this.NombreAccionDeNegocio = NombreAccionDeNegocio;
            this.IdEntrada = idEntrada;
            this.Entrada = entrada;
            this.IdSalida = idSalida;
            this.Salida = salida;
        }

        // De AccionDeNegocioPorTipoTransaccion a AccionDeNegocioPorTipoTransaccionViewModel
        public static List<AccionDeNegocioPorTipoTransaccionViewModel> Convert(List<AccionDeNegocioPorTipoTransaccion> accionesDeNegocioPorTipoDeTransaccion)
        {
            List<AccionDeNegocioPorTipoTransaccionViewModel> accionesDeNegocioPorTipoDeTransaccionViewModel = new List<AccionDeNegocioPorTipoTransaccionViewModel>();
            var accionesAgrupadasPorAccionNegocio = accionesDeNegocioPorTipoDeTransaccion.GroupBy(d => d.IdAccionDeNegocio).ToList();
            foreach (var accionAgrupadaPorAccionNegocio in accionesAgrupadasPorAccionNegocio)
            {
                accionesDeNegocioPorTipoDeTransaccionViewModel.Add(new AccionDeNegocioPorTipoTransaccionViewModel(accionAgrupadaPorAccionNegocio.Key, accionAgrupadaPorAccionNegocio.First().AccionDeNegocio().Nombre, accionAgrupadaPorAccionNegocio.SingleOrDefault(a => a.Valor) == null ? 0 : accionAgrupadaPorAccionNegocio.SingleOrDefault(a => a.Valor).Id, accionAgrupadaPorAccionNegocio.SingleOrDefault(a => a.Valor) != null, accionAgrupadaPorAccionNegocio.SingleOrDefault(a => !a.Valor) == null ? 0 : accionAgrupadaPorAccionNegocio.SingleOrDefault(a => !a.Valor).Id, accionAgrupadaPorAccionNegocio.SingleOrDefault(a => !a.Valor) != null));
            }
            return accionesDeNegocioPorTipoDeTransaccionViewModel;
        }

        // De AccionDeNegocioPorTipoTransaccion a AccionDeNegocioPorTipoTransaccionViewModel para editar
        public static List<AccionDeNegocioPorTipoTransaccionViewModel> SetearAccionesDeNegocioPorTipoDeTransaccionParaEditar(List<AccionDeNegocio> accionesDeNegocio, List<AccionDeNegocioPorTipoTransaccionViewModel> accionesDeNegocioPorTipoDeTransaccion)
        {
            List<AccionDeNegocioPorTipoTransaccionViewModel> accionesDeNegocioPorTipoTransaccionEditar = new List<AccionDeNegocioPorTipoTransaccionViewModel>();
            if (accionesDeNegocioPorTipoDeTransaccion == null)
            {
                accionesDeNegocioPorTipoDeTransaccion = new List<AccionDeNegocioPorTipoTransaccionViewModel>();
            }

            int idEntrada = 0;
            bool entrada = false;
            int idSalida = 0;
            bool salida = false;
            foreach (var accionDeNegocio in accionesDeNegocio)
            {
                bool band = false;
                foreach (var accionDeNegocioPorTipoDeTransaccion in accionesDeNegocioPorTipoDeTransaccion)
                {
                    if (accionDeNegocio.Id == accionDeNegocioPorTipoDeTransaccion.IdAccionDeNegocio)
                    {
                        band = true;
                        idEntrada = accionDeNegocioPorTipoDeTransaccion.IdEntrada;
                        entrada = accionDeNegocioPorTipoDeTransaccion.Entrada;
                        idSalida = accionDeNegocioPorTipoDeTransaccion.IdSalida;
                        salida = accionDeNegocioPorTipoDeTransaccion.Salida;
                    }
                }
                if (band)
                {
                    accionesDeNegocioPorTipoTransaccionEditar.Add(new AccionDeNegocioPorTipoTransaccionViewModel(accionDeNegocio.Id, accionDeNegocio.Nombre, idEntrada, entrada, idSalida, salida));
                }
                else
                {
                    accionesDeNegocioPorTipoTransaccionEditar.Add(new AccionDeNegocioPorTipoTransaccionViewModel(accionDeNegocio.Id, accionDeNegocio.Nombre));
                }
            }
            return accionesDeNegocioPorTipoTransaccionEditar;
        }


        // De AccionDeNegocio a AccionDeNegocioPorTipoTransaccionViewModel
        public static List<AccionDeNegocioPorTipoTransaccionViewModel> Convert(List<AccionDeNegocio> accionesDeNegocio)
        {
            List<AccionDeNegocioPorTipoTransaccionViewModel> accionesDeNegocioPorTipoTransaccionGenerico = new List<AccionDeNegocioPorTipoTransaccionViewModel>();
            foreach (var accionDeNegocio in accionesDeNegocio)
            {
                accionesDeNegocioPorTipoTransaccionGenerico.Add(new AccionDeNegocioPorTipoTransaccionViewModel(accionDeNegocio.Id, accionDeNegocio.Nombre));
            }
            return accionesDeNegocioPorTipoTransaccionGenerico;
        }
        // De AccionDeNegocioPorTipoTransaccionViewModel a  AccionDeNegocioPorTipoDeTransaccion
        public static List<AccionDeNegocioPorTipoTransaccion> Convert(List<AccionDeNegocioPorTipoTransaccionViewModel> accionesDeNegocioPorTipoDeTransaccionViewModel)
        {
            List<AccionDeNegocioPorTipoTransaccion> accionesDeNegocioPorTipoTransaccion = new List<AccionDeNegocioPorTipoTransaccion>();
            foreach (var accionDeNegocioPorTipoDeTransaccionViewModel in accionesDeNegocioPorTipoDeTransaccionViewModel)
            {
                if (accionDeNegocioPorTipoDeTransaccionViewModel.Entrada)
                {
                    accionesDeNegocioPorTipoTransaccion.Add(new AccionDeNegocioPorTipoTransaccion(accionDeNegocioPorTipoDeTransaccionViewModel.IdAccionDeNegocio, accionDeNegocioPorTipoDeTransaccionViewModel.IdEntrada, true));
                }
                if (accionDeNegocioPorTipoDeTransaccionViewModel.Salida)
                {
                    accionesDeNegocioPorTipoTransaccion.Add(new AccionDeNegocioPorTipoTransaccion(accionDeNegocioPorTipoDeTransaccionViewModel.IdAccionDeNegocio, accionDeNegocioPorTipoDeTransaccionViewModel.IdSalida, false));
                }
            }
            return accionesDeNegocioPorTipoTransaccion;
        }
    }


}
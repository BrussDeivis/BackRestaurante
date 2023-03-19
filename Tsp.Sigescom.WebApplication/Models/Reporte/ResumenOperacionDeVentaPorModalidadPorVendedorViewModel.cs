using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    public class ResumenOperacionDeVentaPorModalidadPorVendedorViewModel
    {
        public ResumenOperacionDeVentaPorModalidadPorVendedorViewModel()
        {

        }
        public ResumenOperacionDeVentaPorModalidadPorVendedorViewModel(string modalidad, int cantidadOperaciones, decimal importeTotal)
        {
            Modalidad = modalidad;
            CantidadOperaciones = cantidadOperaciones;
            ImporteTotal = importeTotal;

        }
        public string Modalidad { get; set; }
        public int CantidadOperaciones { get; set; }
        public decimal ImporteTotal { get; set; }



        public static List<ResumenOperacionDeVentaPorModalidadPorVendedorViewModel> Convert(List<Resumen_Transaccion_Por_Modalidad> ventas)
        {
            List<ResumenOperacionDeVentaPorModalidadPorVendedorViewModel> resumenVentaPorModalidad = new List<ResumenOperacionDeVentaPorModalidadPorVendedorViewModel>();
            foreach (var item in ventas)
            {
                resumenVentaPorModalidad.Add(
                    new ResumenOperacionDeVentaPorModalidadPorVendedorViewModel(
                    Enumerado.GetDescription((ModoOperacionEnum)Enum.Parse(typeof(ModoOperacionEnum), item.IdModalidad, true)),
                    item.CantidadDeOperaciones,
                    item.Importe));
            }
            return resumenVentaPorModalidad;
        }

    }




}
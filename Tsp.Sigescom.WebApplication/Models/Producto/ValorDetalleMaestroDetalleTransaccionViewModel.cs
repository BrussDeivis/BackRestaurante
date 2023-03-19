using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class ValorDetalleMaestroDetalleTransaccionViewModel
    {
        /// <summary>
        /// Se creo de esta forma para poder trabajarlo en la vista de una mejor manera
        /// </summary>
        public int Numero { get; set; }
        public List<ValorDetalleMaestroDetalleTransaccion> Valores { get; set; }


        public ValorDetalleMaestroDetalleTransaccionViewModel()
        {
        }

        public ValorDetalleMaestroDetalleTransaccionViewModel(int numero, List<ValorDetalleMaestroDetalleTransaccion> valores)
        {
            this.Numero = numero;
            this.Valores = valores;
        }

            

        public static List<ValorDetalleMaestroDetalleTransaccionViewModel> Convert(List<ValorDetalleMaestroDetalleTransaccion> valores)
        {
            List<ValorDetalleMaestroDetalleTransaccionViewModel> resultado = new List<ValorDetalleMaestroDetalleTransaccionViewModel>();



            if (valores != null)
            {    
                resultado.Add(new ValorDetalleMaestroDetalleTransaccionViewModel(0,valores));
        
            }

            //foreach (var valorAgrupado in valoresAgrupados)
            //{
            //    resultado.Add(new ValorDetalleMaestroDetalleTransaccionViewModel(valorAgrupado.FirstOrDefault().Numero,valores));
            //}
            return resultado;
        }

        public static List<ValorDetalleMaestroDetalleTransaccion> Convert(List<ValorDetalleMaestroDetalleTransaccionViewModel> valoresModel)
        {
            List<ValorDetalleMaestroDetalleTransaccion> resultado = new List<ValorDetalleMaestroDetalleTransaccion>();


            if (valoresModel != null)
            {
                foreach (var valorModel in valoresModel)
                {
                    foreach (var item in valorModel.Valores)
                    {
                        resultado.Add(new ValorDetalleMaestroDetalleTransaccion()
                        {
                            IdDetalleMaestro = item.IdDetalleMaestro,
                            IdDetalleTransaccion = item.IdDetalleTransaccion,
                            Numero = valorModel.Numero,
                            Valor = item.Valor
                        });
                    }
                }

              
            }

     
            return resultado;
        }

    }



}
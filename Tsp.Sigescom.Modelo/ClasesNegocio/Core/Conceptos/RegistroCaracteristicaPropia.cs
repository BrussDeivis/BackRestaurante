using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class CaracteristicaPropia
    {
        public int Numero { get; set; }
        public List<ValoreCaracteristicaPropia> Valores { get; set; }

        public CaracteristicaPropia()
        {
            this.Valores = new List<ValoreCaracteristicaPropia>();
        }

        public static List<ValorDetalleMaestroDetalleTransaccion> Convert(List<CaracteristicaPropia> caracteristicasViewModel)
        {
            List<ValorDetalleMaestroDetalleTransaccion> valores_ = new List<ValorDetalleMaestroDetalleTransaccion>();
            foreach (var caracteristicaModel in caracteristicasViewModel)
            {
                foreach (var item in caracteristicaModel.Valores)
                {
                    valores_.Add(new ValorDetalleMaestroDetalleTransaccion(item.Id,item.IdDetalleMaestro, caracteristicaModel.Numero, item.Valor));
                }
            }
            return valores_;
        }
    }

    public class ValoreCaracteristicaPropia
    {

        public int Id { get; set; }
        public int IdDetalleMaestro { get; set; }
        public string Valor { get; set; }

        public ValoreCaracteristicaPropia()
        {

        }

    }
}
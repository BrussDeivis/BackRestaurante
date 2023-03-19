using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.WebApplication.Models
{
    public class CaracteristicaViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool EsComun { get; set; }

        public CaracteristicaViewModel()
        {

        }

        public CaracteristicaViewModel(Detalle_maestro detalle)
        {
            Id = detalle.id;
            Nombre = detalle.nombre;
            EsComun = detalle.id_maestro == MaestroSettings.Default.IdMaestroCaracteristicaConcepto; ;
        }

        public static List<CaracteristicaViewModel> Convert(List<Detalle_maestro> detalles)
        {
            List<CaracteristicaViewModel> conceptos = new List<CaracteristicaViewModel>();
            foreach (var detalle in detalles)
            {
                conceptos.Add(new CaracteristicaViewModel(detalle ));
            }
            return conceptos;
        }
    }
}
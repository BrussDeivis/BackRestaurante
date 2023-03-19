using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class DetalleMaestroViewModel
    {
        [DataMember]
        public int Id { get; set; }
        public int IdMaestro { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Valor { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DetalleMaestroViewModel DetalleMaestroPadre { get; set; }

        public DetalleMaestroViewModel()
        {

        }
        public DetalleMaestroViewModel(Detalle_maestro detalle)
        {
            this.Id = detalle.id;
            this.IdMaestro = detalle.id_maestro;
            this.Codigo = detalle.codigo;
            this.Nombre = detalle.nombre;
            this.Valor = detalle.valor;
            this.FechaRegistro = detalle.fecha_registro;

        }

        public static DetalleMaestroViewModel ConvertirDetalleDetalleMaestro(Detalle_maestro detalle)
        {
            DetalleMaestroViewModel detalleMaestro = new DetalleMaestroViewModel(detalle);
            detalleMaestro.DetalleMaestroPadre = new DetalleMaestroViewModel(detalle.Detalle_detalle_maestro1.Single().Detalle_maestro);
            return detalleMaestro;
        }

        public static List<DetalleMaestroViewModel> ConvertirCategoriasDetalleDetalleMaestro(List<Detalle_maestro> detalles)
        {
            List<DetalleMaestroViewModel> nuevosDetalles = new List<DetalleMaestroViewModel>();
            foreach (var detalle in detalles)
            {
                var categoria = detalle.Detalle_detalle_maestro1.Count() > 0 ? ConvertirDetalleDetalleMaestro(detalle) : new DetalleMaestroViewModel(detalle);
                nuevosDetalles.Add(categoria);
            }
            return nuevosDetalles;
        }
    }
}
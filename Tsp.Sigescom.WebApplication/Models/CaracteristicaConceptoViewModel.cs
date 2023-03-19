using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class CaracteristicaConceptoViewModel
    {
        [DataMember]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public List<ComboGenericoViewModel> Valores { get; set; }
        public bool EsComun { get; set; }
        public bool EsVigente { get; set; }

        public CaracteristicaConceptoViewModel()
        {

        }
        public CaracteristicaConceptoViewModel(Detalle_maestro detalle)
        {
            this.Id = detalle.id;
            this.Nombre = detalle.nombre;
            this.Descripcion = detalle.valor;
            this.Valores = new List<ComboGenericoViewModel>();
            this.EsComun = detalle.id_maestro == MaestroSettings.Default.IdMaestroCaracteristicaConcepto;
            this.EsVigente = detalle.es_vigente;
            foreach (var item in detalle.Valor_caracteristica)
            {
                Valores.Add(new ComboGenericoViewModel(item.id,item.valor));
            }
        }

        public static List<CaracteristicaConceptoViewModel> Convert( List<Detalle_maestro> detalles)
        {
            List<CaracteristicaConceptoViewModel> caracteristicas = new List<CaracteristicaConceptoViewModel>();
            foreach (var item in detalles)
            {
                caracteristicas.Add(new CaracteristicaConceptoViewModel(item));
            }

            return caracteristicas;
        }

        public static List<CaracteristicaConceptoViewModel> Convert(Detalle_maestro detalle)
        {
            List<CaracteristicaConceptoViewModel> caracteristicas = new List<CaracteristicaConceptoViewModel>();

            foreach (var item in detalle.Caracteristica_concepto)
            {
                caracteristicas.Add(new CaracteristicaConceptoViewModel(item.Detalle_maestro1));
            }

            return caracteristicas;
        }
    }
}
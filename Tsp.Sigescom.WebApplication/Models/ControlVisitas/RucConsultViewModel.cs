using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.ApiIdentificacion.AccesoDatos;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class RucConsultViewModel
    {
        [DataMember]
        public string RazonSocial { get; set; }
        public string Ruc { get; set; }


        public RucConsultViewModel(ruc ruc)
        {
            this.RazonSocial = ruc.nombre_comercial;
            this.Ruc = ruc.numero_ruc;
        }

        public static List<RucConsultViewModel> Convert(List<ruc> rucs)
        {
            var ruc_ = new List<RucConsultViewModel>();

            foreach (var item in rucs)
            {
                ruc_.Add(new RucConsultViewModel(item));
            }
            return ruc_;
        }
    }
    

}
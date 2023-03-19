using Tsp.Sigescom.Modelo.Entidades.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class AccionOperativaVista
    {
        [DataMember]
        public int IdAccion { get; set; }
        public string CodigoAccion { get; set; }
        public string NombreAccion { get; set; }

        public AccionOperativaVista()
        {

        }

        public AccionOperativaVista(AccionOperativa ao)
        {
            this.IdAccion = ao.IdAccion;
            this.CodigoAccion = ao.CodigoAccion;
            this.NombreAccion= ao.NombreAccion;
        }

        public AccionOperativaVista(int id,string codigo,string nombre)
        {
            this.IdAccion = id;
            this.CodigoAccion = codigo;
            this.NombreAccion = nombre;
        }

        public static new List<AccionOperativaVista> convert(List<AccionOperativa> accionesOperativas)
        {
            var accionOperativaVista = new List<AccionOperativaVista>();

            foreach (var accionOperativa in accionesOperativas)
            {
                accionOperativaVista.Add(new AccionOperativaVista(accionOperativa));
            }
            return accionOperativaVista;
        }
    }
}
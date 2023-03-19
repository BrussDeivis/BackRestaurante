using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class ComboActorGenerico
    {
        [DataMember]
        public int Id { get; set; }
        public string NumeroDocumentoIdentidad { get; set; }
        public string RazonSocial { get; set; }
        public string NombreCorto { get; set; }
        public int IdTipoPago { get; set; }

        public ComboActorGenerico()
        {

        }
        public ComboActorGenerico(int id, string numeroDocumentoIdentidad,string razonSocial)
        {
            this.Id = id;
            this.NumeroDocumentoIdentidad = numeroDocumentoIdentidad;
            this.RazonSocial = razonSocial;
        }

        public ComboActorGenerico(int id, string numeroDocumentoIdentidad, string razonSocial,string nombreCorto)
        {
            this.Id = id;
            this.NumeroDocumentoIdentidad = numeroDocumentoIdentidad;
            this.RazonSocial = razonSocial;
            this.NombreCorto = nombreCorto;
        }
        public ComboActorGenerico(int id, string numeroDocumentoIdentidad, string razonSocial, string nombreCorto,int idTipoDePago)
        {
            this.Id = id;
            this.NumeroDocumentoIdentidad = numeroDocumentoIdentidad;
            this.RazonSocial = razonSocial;
            this.NombreCorto = nombreCorto;
            this.IdTipoPago = idTipoDePago;
        }
    }
}
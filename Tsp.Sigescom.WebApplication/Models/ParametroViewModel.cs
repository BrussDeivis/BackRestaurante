using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class ParametroViewModel
    {
        [DataMember]
        public int Id { get; set; }
        public int IdActorNegocio { get; set; }
        public int IdParametro { get; set; }
        public string NombreParametro { get; set; }
        public int IdValorParametro { get; set; }
        public string ValorParametro { get; set; }
        public decimal Valor { get; set; }

        public ParametroViewModel()
        {

        }

        public ParametroViewModel(int idParametro,int idValorParametro, string valorParametro,decimal valor)
        {
            this.IdParametro = idParametro;
            this.IdValorParametro = idValorParametro;
            this.ValorParametro = valorParametro;
            this.Valor = valor;
        }

        public ParametroViewModel(IDataReader reader)
        {
            this.Id = Convert.ToInt32(reader["Id"]);
            //  this.Nombre = Convert.ToInt32(reader["Id"]);
            // this.Apellido
        }
    }
}
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
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
    public class BandejaClienteViewModel
    {
        [DataMember]
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string RazonSocial { get; set; }
        public string TipoPersona { get; set; }
        public string TipoDocumentoIdentidad { get; set; }
        public string NumeroDocumentoIdentidad { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }

        public BandejaClienteViewModel(Cliente cliente)
        {
            this.Id = cliente.Id;
            this.Codigo = cliente.Codigo;
            this.RazonSocial = cliente.RazonSocial;
            this.NumeroDocumentoIdentidad = cliente.DocumentoIdentidad;
            this.TipoDocumentoIdentidad = cliente.CodigoTipoDocumentoIdentidad();
            this.TipoPersona = cliente.TipoPersona();
            this.Telefono = cliente.Telefono();
            this.Correo = cliente.Correo();
            if (cliente.DomicilioFiscal() != null)
            {
                this.Direccion = (cliente.DomicilioFiscal().detalle + " , " + cliente.DomicilioFiscal().Ubigeo.descripcion_larga).ToUpper();
            }
        }

        public static List<BandejaClienteViewModel> Convert(List<Cliente> clientes)
        {
            var clientes_ = new List<BandejaClienteViewModel>();

            foreach (var cliente in clientes)
            {
                clientes_.Add(new BandejaClienteViewModel(cliente));
            }
            return clientes_.OrderByDescending(c => c.Id).ToList();
        }
    }
    

}
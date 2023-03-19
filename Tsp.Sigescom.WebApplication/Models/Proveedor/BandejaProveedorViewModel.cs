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
    public class BandejaProveedorViewModel
    {
        [DataMember]
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string RazonSocial { get; set; }
        public string TipoPersona { get; set; }
        public string TipoDocumentoIdentidad { get; set; }
        public string NumeroDocumentoIdentidad { get; set; }
        public string Direccion { get; set; }

        public BandejaProveedorViewModel(Proveedor proveedor)
        {
            this.Id = proveedor.Id;
            this.Codigo = proveedor.Codigo;
            this.RazonSocial = proveedor.RazonSocial;
            this.NumeroDocumentoIdentidad = proveedor.DocumentoIdentidad;
            this.TipoDocumentoIdentidad = proveedor.CodigoTipoDocumentoIdentidad();
            this.TipoPersona = proveedor.TipoPersona();
            if (proveedor.DomicilioFiscal() != null)
            {
                this.Direccion = (proveedor.DomicilioFiscal().detalle + " , " + proveedor.DomicilioFiscal().Ubigeo.descripcion_larga).ToUpper();
            }
        }

        public static List<BandejaProveedorViewModel> Convert(List<Proveedor> proveedores)
        {
            var proveedores_ = new List<BandejaProveedorViewModel>();

            foreach (var proveedor in proveedores)
            {
                proveedores_.Add(new BandejaProveedorViewModel(proveedor));
            }
            return proveedores_;
        }
    }
    

}
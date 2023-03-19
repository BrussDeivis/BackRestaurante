using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Mvc;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models
{

    public class RegsitroProductoParaFacturacionViewModels
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool EsBien { get; set; }
        public ConceptoBasicoViewModel ConceptoBasico { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int IdActorVersion { get; set; }
        public int IdVersion { get; set; }
        public string VersionFila { get; set; }
        public decimal Stock { get; set; }

        public RegsitroProductoParaFacturacionViewModels()
        {

        }

        public RegsitroProductoParaFacturacionViewModels(int id, string nombre, decimal precioUnitario, byte[] versionFila ,decimal stock)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.PrecioUnitario = precioUnitario;
            this.VersionFila = versionFila!=null? Convert.ToBase64String(versionFila):null;
            this.Stock = stock;
        }
        public RegsitroProductoParaFacturacionViewModels(int id, string nombre, byte[] versionFila, decimal precioUnitario, decimal stock,int idVersion, int idActorVersion)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.VersionFila = versionFila != null ? Convert.ToBase64String(versionFila) : null;
            this.PrecioUnitario = precioUnitario;
            this.Stock = stock;
            this.IdVersion = idVersion;
            this.IdActorVersion = idActorVersion;
        }
    }

}
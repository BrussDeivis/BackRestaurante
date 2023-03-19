using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models
{
    public class RegistroTrasladoInternoDeMercaderiaViewModel
    {
        public long Id { get; set; }
        public DateTime Fecha { get; set; }
        public ComboGenericoViewModel AlmacenDestino { get; set; }
        public ComboGenericoViewModel ResponsableDestino { get; set; }
        public SelectorTipoDeComprobante TipoDeComprobante { get; set; }
        public IEnumerable<DetalleTrasladoInternoViewModel> Detalles { get; set; }
        public string Observacion { get; set; }

        public RegistroTrasladoInternoDeMercaderiaViewModel()
        {

        }

    }

    public class DetalleTrasladoInternoViewModel
    {
        public ProductoParaAlmacenViewModel Producto { get; set; }
        public decimal Cantidad { get; set; }
        public string Lote { get; set; }
        public string Observacion { get; set; }

        public DetalleTrasladoInternoViewModel()
        {

        }
    }
}
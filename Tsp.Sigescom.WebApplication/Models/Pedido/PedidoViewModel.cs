using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Pedido;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class PedidoViewModel
    {
        [DataMember]
        public long IdOrden { get; set; }
        public long IdCotizacion { get; set; }
        public string CadenaHtmlDeDocumento { get; set; }

        public PedidoViewModel(OrdenDePedido ordenDePedido)
        {
            this.IdOrden = ordenDePedido.Id;
            this.IdCotizacion = ordenDePedido.IdPedido;
        }

        public PedidoViewModel(OrdenDePedido ordenDePedido, string cadenaHtmlDeDocumento)
        {
            this.IdOrden = ordenDePedido.Id;
            this.IdCotizacion = ordenDePedido.IdPedido;
            this.CadenaHtmlDeDocumento = cadenaHtmlDeDocumento;
        }

        public static List<PedidoViewModel> Convert(List<OrdenDePedido> ordenesDeCotizacion)
        {
            List<PedidoViewModel> ordenes = new List<PedidoViewModel>();
            foreach (var ordenDePedido in ordenesDeCotizacion)
            {
                ordenes.Add(new PedidoViewModel(ordenDePedido));
            }
            return ordenes;
        }
    }
}
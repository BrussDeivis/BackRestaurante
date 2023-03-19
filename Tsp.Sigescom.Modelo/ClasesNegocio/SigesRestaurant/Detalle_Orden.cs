using System.Collections.Generic;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo.SigesRestaurant
{
    public class DetalleOrden
    {
        public long Id { get; set; }
        public string NombreItem { get; set; }
        public Detalle_Item_Restaurante DetalleItemRestaurante { get; set; } //DetallesComplementos
        public int IdItem { get; set; }
        public long IdTransaccion { get; set; }
        public string DetalleItemRestauranteJson { get; set; }//DetallesComplementosJson
        public decimal Precio { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Importe { get; set; }
        public int Estado { get; set; }
        public string EtiquetaEstado { get => Estado == (int)EstadoDeDetalleDeOrden.Devuelto ? "devuelto" : Estado == (int)EstadoDeDetalleDeOrden.Anulado ? "anulado" : ""; }
        public List<string> ValoresComplementos { get; set; }
        public bool EsBien { get; set; }
        public DetalleOrden()
        {

        }

       
    }
}

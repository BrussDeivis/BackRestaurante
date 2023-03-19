using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.Partial
{
    public class Precio_Compra_Venta_Concepto
    {
        public int IdPrecio { get; set; }
        public int IdPuntoPrecio { get; set; }
        public string PuntoPrecio { get; set; }
        public int IdTarifa { get; set; }
        public string Tarifa { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal Valor { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Descripcion { get; set; }
        public bool Seleccionado { get; set; }

        public Precio_Compra_Venta_Concepto()
        {

        }

        public Precio_Compra_Venta_Concepto(int idPrecio, int idPuntoPrecio, int idTarifa, decimal valor, DateTime fechaInicio, DateTime fechaFin, string descripcion, bool seleccionado)
        {
            IdPrecio = idPrecio;
            IdPuntoPrecio = idPuntoPrecio;
            IdTarifa = idTarifa;
            Valor = valor;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            Descripcion = descripcion;
            Seleccionado = seleccionado;
        }
    }
}

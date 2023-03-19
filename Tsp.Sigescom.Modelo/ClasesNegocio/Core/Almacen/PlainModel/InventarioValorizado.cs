using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel
{
    public class InventarioValorizado: InventarioFisico
    {

        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }


        public InventarioValorizado()
        {

        }
        new public List<InventarioValorizado> Convert()
        {
            return new List<InventarioValorizado>();
        }

        public static List<Detalle_transaccion> ToDetallesTransaccion(List<InventarioValorizado> inventarioValorizados,string  descripcion)
        {
            List<Detalle_transaccion> detalles = new List<Detalle_transaccion>();
            foreach (var detalle in inventarioValorizados)
            {
                detalles.Add(new Detalle_transaccion(detalle.Cantidad, detalle.IdConcepto, descripcion, detalle.ValorUnitario, detalle.ValorTotal, null, detalle.CantidadSecundaria, null, null, 0m, 0m, 0m));
            }
            return detalles;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel
{
    public class InventarioHistorico : InventarioFisico
    {

        public string Descripcion { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }


        public InventarioHistorico()
        {

        }
        new public List<InventarioHistorico> Convert()
        {
            return new List<InventarioHistorico>();
        }

        public static List<Detalle_transaccion> ToDetallesTransaccion(List<InventarioHistorico> inventarioValorizados,string  descripcion)
        {
            List<Detalle_transaccion> detalles = new List<Detalle_transaccion>();
            foreach (var detalle in inventarioValorizados)
            {
                detalles.Add(new Detalle_transaccion(detalle.Cantidad, detalle.IdConcepto, descripcion, detalle.ValorUnitario, detalle.ValorTotal, null, detalle.CantidadSecundaria, null, null, 0m, 0m, 0m));
            }
            return detalles;
        }
        public Detalle_transaccion ToDetalleTransaccion()
        {
            return new Detalle_transaccion(IdDetalleTransaccion, Cantidad, IdConcepto, Descripcion, ValorUnitario, ValorTotal, null, CantidadSecundaria, null, null, 0m, 0m, 0m, Lote, null, null);
        }
    }
}

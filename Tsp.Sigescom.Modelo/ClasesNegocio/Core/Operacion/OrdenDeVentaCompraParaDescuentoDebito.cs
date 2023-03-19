using System;
using System.Collections.Generic;
using System.Linq;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class OrdenDeVentaCompraParaDescuentoDebito
    {

        public long idOrden;
        public List<Detalle_transaccion> detalles;

        public OrdenDeVentaCompraParaDescuentoDebito()
        {

        }
        public OrdenDeVentaCompraParaDescuentoDebito(long id, List<Detalle_transaccion> detalles)
        {
            this.idOrden = id;
            this.detalles = detalles;
        }

    }

}

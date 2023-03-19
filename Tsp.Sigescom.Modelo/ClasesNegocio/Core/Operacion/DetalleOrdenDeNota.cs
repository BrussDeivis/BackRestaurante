using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class DetalleOrdenDeNota : DetalleDeOperacion
    {
        public decimal MontoRevocado { get; set; }
        public decimal MontoDevuelto { get; set; }
        public decimal MontoDetalle { get; set; }
        public decimal IgvRevocado { get; set; }
        public decimal IcbperRevocado { get; set; }
        public decimal ImporteRevocado { get; set; }
        public decimal IgvDevuelto { get; set; }
        public decimal IcbperDevuelto { get; set; }
        public decimal ImporteDevuelto { get; set; }
        public DetalleOrdenDeNota()
        {
        }

        public DetalleOrdenDeNota(Detalle_transaccion _detalleTransaccion) : base(_detalleTransaccion)
        {
        }

        public DetalleOrdenDeNota(Detalle_transaccion _detalleTransaccion, decimal montoDetalle, decimal montoRevocado, decimal montoDevuelto) : base(_detalleTransaccion)
        {
            this.MontoDetalle = montoDetalle;
            this.MontoRevocado = montoRevocado;
            this.MontoDevuelto = montoDevuelto;
        }
    }
}


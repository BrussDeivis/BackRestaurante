using System;
using System.Collections.Generic;
using Tsp.Sigescom.Config;
using System.Linq;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class Cobrox : MovimientoEconomico
    {

        private Transaccion cobro;

        public Cobrox(Transaccion transaccion) : base(transaccion)
        {
            this.cobro = transaccion;
        }

        public static List<Cobrox> Convert(List<Transaccion> transacciones)
        {
            List<Cobrox> cobros = new List<Cobrox>();
            foreach (var item in transacciones)
            {
                cobros.Add(new Cobrox(item));
            }
            return cobros;
        }

        public Empleado Cajero()
        {
            return new Empleado(this.cobro.Actor_negocio);
        }

        public Cliente Cliente()
        {
            return new Cliente(this.cobro.Actor_negocio1);
        }

        public ActorComercial Caja()
        {
            return new ActorComercial(this.cobro.Actor_negocio2);
        }

        public decimal ImporteTotal()
        {
            decimal importe = cobro.importe_total;
            return importe;
        }

        public List<DetallePagoCuotax> Detalles()
        {
            return DetallePagoCuotax.Convert(this.cobro.Pago_cuota.ToList());
        }
    }

    public class DetallePagoCuotax
    {
        private string codigoCuota;
        private string comprobanteOrden;
        private decimal importe;

        public string CodigoCuota { get => codigoCuota; set => codigoCuota = value; }
        public string ComprobanteOrden { get => comprobanteOrden; set => comprobanteOrden = value; }
        public decimal Importe { get => importe; set => importe = value; }

        public DetallePagoCuotax(Pago_cuota pago)
        {
            CodigoCuota = pago.Cuota.codigo;
            ComprobanteOrden = pago.Cuota.Transaccion.Comprobante.numero_serie + " - " + pago.Cuota.Transaccion.Comprobante.numero;
            Importe = pago.importe;
        }

        public DetallePagoCuotax(string codigoCuota, string comprobanteOrden, decimal importe)
        {
            CodigoCuota = codigoCuota;
            ComprobanteOrden = comprobanteOrden;
            Importe = importe;
        }

        public static List<DetallePagoCuotax> Convert(List<Pago_cuota> pagos)
        {
            List<DetallePagoCuotax> detalles = new List<DetallePagoCuotax>(); 
            foreach (var item in pagos)
            {
                detalles.Add(new DetallePagoCuotax(item));
            }
            return detalles;
        }
    }

}

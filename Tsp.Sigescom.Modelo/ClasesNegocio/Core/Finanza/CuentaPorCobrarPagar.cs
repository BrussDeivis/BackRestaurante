using System;
using System.Collections.Generic;
using System.Linq;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class CuentaPorCobrarPagar
    {
        private Cuota cuota;         

        public CuentaPorCobrarPagar()
        {

        }

        public CuentaPorCobrarPagar(Cuota cuota)
        {
            this.cuota= cuota;
        }
 
        public int Id
        {
            get { return this.cuota.id; }
        }

        public long IdOperacionBase
        {
            get { return this.cuota.id_transaccion; }
        }

        public string Codigo
        {
            get { return this.cuota.codigo; }
        }

        public bool PorCobrar
        {
            get { return this.cuota.por_cobrar; }
        }
        public OperacionGenericaNivel3 OperacionBase()
        {
            return new OperacionGenericaNivel3(this.cuota.Transaccion);
        }
        public DateTime FechaDeEmision
        {
            get { return this.cuota.fecha_emision; }
        }

        public DateTime FechaDeVencimiento
        {
            get { return this.cuota.fecha_vencimiento; }
        }

        public decimal Saldo()
        {
            return cuota.saldo;
        }

        public decimal Capital()
        {
            return cuota.capital;
        }

        public decimal Interes()
        {
            return cuota.interes;
        }

        public decimal Total()
        {
            return cuota.total;
        }

        public decimal Revocado()
        {
            return cuota.revocado;
        }

        public decimal Pago()
        {
            return cuota.pago_a_cuenta;
        }

        public ActorComercial ClienteProveedor()
        {
            return new ActorComercial(this.cuota.Transaccion.Actor_negocio1);
        }

        public static List<CuentaPorCobrarPagar> convert(List<Cuota> cuotas)
        {
            var cuentas = new List<CuentaPorCobrarPagar>();

            foreach (var cuota in cuotas)
            {
                cuentas.Add(new CuentaPorCobrarPagar(cuota));
            }
            return cuentas;
        }

    }
}

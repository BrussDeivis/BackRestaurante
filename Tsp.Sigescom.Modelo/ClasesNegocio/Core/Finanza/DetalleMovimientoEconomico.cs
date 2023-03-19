using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class DetalleMovimientoEconomico
    {
        protected Pago_cuota pago_Cuota;

        public DetalleMovimientoEconomico(Pago_cuota pago_Cuota)
        {
            this.pago_Cuota = pago_Cuota;
        }

        public long Idcuota
        {
            get { return this.pago_Cuota.id_cuota; }
        }

        public long IdMovimientoEconomico
        {
            get { return this.pago_Cuota.id_transaccion; }
        }

        public decimal Importe
        {
            get { return this.pago_Cuota.importe; }
        }

        public CuentaPorCobrarPagar Cuota()
        {
            return new CuentaPorCobrarPagar(this.pago_Cuota.Cuota);
        }

        public MovimientoEconomico MovimientoEconomico()
        {
            return new MovimientoEconomico(this.pago_Cuota.Transaccion);
        }

        public static List<DetalleMovimientoEconomico> Convert(List<Pago_cuota> pago_Cuotas)
        {
            List<DetalleMovimientoEconomico> detalles = new List<DetalleMovimientoEconomico>();
            foreach (var pago_Cuota in pago_Cuotas)
            {
                detalles.Add(new DetalleMovimientoEconomico(pago_Cuota));
            }
            return detalles;
        }
    }
}

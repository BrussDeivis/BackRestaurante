using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class MovimientoEconomico : OperacionGenericaNivel3
    {
        public MovimientoEconomico()
        {

        }

        public MovimientoEconomico(Transaccion transaccion) : base(transaccion)
        {

        }

        public bool EsIngreso()
        {
            return this.TipoTransaccion().ListaDeAccionesDeNegocioPorTipoTransaccion().Single(a => a.IdAccionDeNegocio == (int)AccionesDeNegocioEnum.MovimientoEnCaja).Valor;
        }

        public CuentaPorCobrarPagar Cuota()
        {
            return new CuentaPorCobrarPagar(this.transaccion.Pago_cuota.SingleOrDefault().Cuota);
        }

        public TrazaDePago TrazaDePago()
        {
            return this.transaccion.Traza_pago.Count > 0 ? new TrazaDePago(this.transaccion.Traza_pago.First()) : null;
        }

        public Empleado Cajero()
        {
            return new Empleado(this.transaccion.Actor_negocio);
        }

        public ActorComercial Caja()
        {
            return new ActorComercial(this.transaccion.Actor_negocio2);
        }

        public ActorComercial PagadorRecibidor()
        {
            return new ActorComercial(this.transaccion.Actor_negocio1);
        }

        public List<DetalleMovimientoEconomico> Detalles()
        {
            return DetalleMovimientoEconomico.Convert(this.transaccion.Pago_cuota.ToList());
        }

        public static List<MovimientoEconomico> Convert(List<Transaccion> transacciones)
        {
            List<MovimientoEconomico> movimientos = new List<MovimientoEconomico>();
            foreach (var transaccion in transacciones)
            {
                movimientos.Add(new MovimientoEconomico(transaccion));
            }
            return movimientos;
        }
    }

    public class MovimientoEconomico_
    {
        public ItemGenerico Caja { get; set; }
        public ItemGenerico Cajero { get; set; }
        public ComprobanteDeNegocio_ Comprobante { get; set; }
        public TrazaPago_ Traza { get; set; }
        public List<PagoCuota_> PagoCuota { get; set; }
        public int IdTipoTransaccion { get; set; }
        public int IdActorComercial { get; set; }
        public string Observacion { get; set; }
        public decimal Total { get; set; }
    }

    public class PagoCuota_
    {
        public int IdCuota { get; set; }
        public long IdMovimientoEconomico { get; set; }
        public decimal Importe { get; set; }
        public Cuota Cuota { get; set; }
        public MovimientoEconomico MovimientoEconomico { get; set; }

        public Pago_cuota Convert()
        {
            return new Pago_cuota(IdCuota, Importe);
        }
    }

    public class TrazaPago_
    {
        public ItemGenerico MedioDePago { get; set; }
        public ItemGenerico EntidadFinanciera { get; set; }
        public ItemGenerico CuentaBancaria { get; set; }
        public ItemGenerico OperadorTarjeta { get; set; }
        public string InformacionDePago { get; set; }

        public Traza_pago Convert()
        {
            return new Traza_pago(MedioDePago.Id, InformacionDePago, EntidadFinanciera.Id);
        }
    }
}
using Newtonsoft.Json;
using System;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{


    public class InfoPago
    {
        public decimal ImporteAPagar { get; set; }
        public decimal ImporteEntregado { get; set; }
        public decimal Vuelto { get; set; }
        public ItemGenerico EntidadFinanciera { get; set; }
        public ItemGenerico CuentaBancaria { get; set; }
        public ItemGenerico OperadorTarjeta { get; set; }
        public string NumeroOperacion { get; set; }
        public string DatosDepositante { get; set; }
        public string CuentaDestino { get; set; }
        public string CuentaOrigen { get; set; }
        public string Observacion { get; set; }
        public int PuntosPendientes { get; set; }
        public int PuntosCajeados { get; set; }
        public decimal MontoCajeado { get; set; }
        public string InformacionJson { get; set; }

    }
    //public class Pago_
    //{
    //    public decimal ImporteAPagar { get; set; }
    //    public string Observacion { get; set; }
    //}
    //public class PagoEfectivo : Pago_
    //{
    //    public decimal ImporteEntregado { get; set; }
    //    public decimal Vuelto { get; set; }
    //}

    //public class PagoBancarizado : Pago_
    //{
    //    public ItemGenerico EntidadBancaria { get; set; }
    //    public string NumeroOperacion { get; set; }
    //}
    //public class PagoTransferenciaDeFondos : PagoBancarizado
    //{
    //    public string CuentaOrigen { get; set; }
    //    public string CuentaDestino { get; set; }
    //}

    //public class PagoDepositoEnCuenta : PagoBancarizado
    //{
    //    public string DatosDepositante { get; set; }
    //    public string CuentaDestino { get; set; }
    //}
    //public class PagoConTarjeta : PagoBancarizado
    //{
    //    /// <summary>
    //    /// Visa, MastreCard, etc.
    //    /// </summary>
    //    public ItemGenerico OperadorTarjeta { get; set; }
    //}

    public class TrazaDePago_
    {
        public ItemGenerico MedioDePago { get; set; }
        public InfoPago Info { get; set; }

        public TrazaDePago_()
        {

        }
        public Traza_pago Convert()
        {
            return new Traza_pago() { id_medio_pago = MedioDePago.Id, traza = JsonConvert.SerializeObject(this.Info) };
        }


    }

    public class PuntosDeCliente
    {
        public int PuntosPorCanjear { get; set; }
        public decimal ValorDeUnPuntoComoMedioDePago { get => VentasSettings.Default.ValorDeUnPuntoComoMedioDePago; }
        public decimal ValorPuntosPorCanjear { get => Decimal.Round(PuntosPorCanjear * VentasSettings.Default.ValorDeUnPuntoComoMedioDePago, 2); }
    }
    /// <summary>
    /// Al momento de realizar el canje se guarda el id de la transaccion y la cantidad de puntos canjeados por trasnaccion
    /// </summary>
    public class PuntoCanjeado
    {
        public long Id { get; set; }
        public int Cantidad { get; set; }
        public PuntoCanjeado(long id, int cantidad)
        {
            Id = id; 
            Cantidad = cantidad;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.Core.Finanza.PlainModel
{
    public class OperacionGrupoDetallado
    {
        public long Id { get; set; }
        public int IdTipoTransaccion { get; set; }
        public string NombreResponsable { get; set; }
        public string DocumentoCliente { get; set; }
        public string NombreCliente { get; set; }
        public long IdComprobante { get; set; }
        public string TipoComprobante { get; set; }
        public string SerieYNumeroComprobante { get; set; }
        public DateTime Emision { get; set; }
        public decimal Importe { get; set; }
        public decimal ImporteTotal { get => Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasDondeIngresaDinero_.Contains(IdTipoTransaccion) ? Importe * 1 : Importe * -1; }
        public decimal Revocado { get; set; }
        public decimal RevocadoTotal { get => Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasDondeIngresaDinero_.Contains(IdTipoTransaccion) ? Revocado * 1 : Revocado * -1; }
        public decimal ACuenta { get; set; }
        public decimal ACuentaTotal { get => Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasDondeIngresaDinero_.Contains(IdTipoTransaccion) ? ACuenta * 1 : ACuenta * -1; }
        public decimal Saldo { get; set; }
        public decimal SaldoTotal { get => Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasDondeIngresaDinero_.Contains(IdTipoTransaccion) ? Saldo * 1 : Saldo * -1; }

        public List<OperacionGrupoDetallado> Convert()
        {
            return new List<OperacionGrupoDetallado>();
        }

    }

}

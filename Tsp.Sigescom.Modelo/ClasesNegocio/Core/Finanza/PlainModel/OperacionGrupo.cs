using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.Core.Finanza.PlainModel
{
    public class OperacionGrupo
    {
        public int IdTipoTransaccion { get; set; }
        public int IdGrupo { get; set; }
        public string NombreGrupo { get; set; }
        public string DocumentoResponsable { get; set; }
        public string NombreResponsable { get; set; }
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public long IdComprobante { get; set; }
        public string InfoComprobante { get; set; }
        public int NumeroOperaciones { get; set; }
        public decimal Importe { get; set; }
        public decimal ImporteTotal { get => Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasDondeIngresaDinero_.Contains(IdTipoTransaccion) ? Importe * 1 : Importe * -1; }
        public decimal Revocado { get; set; }
        public decimal RevocadoTotal { get => Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasDondeIngresaDinero_.Contains(IdTipoTransaccion) ? Revocado * 1 : Revocado * -1; }
        public decimal ACuenta { get; set; }
        public decimal ACuentaTotal { get => Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasDondeIngresaDinero_.Contains(IdTipoTransaccion) ? ACuenta * 1 : ACuenta * -1; }
        public decimal Saldo { get; set; }
        public decimal SaldoTotal { get => Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasDondeIngresaDinero_.Contains(IdTipoTransaccion) ? Saldo * 1 : Saldo * -1; }

        public List<OperacionGrupo> Convert()
        {
            return new List<OperacionGrupo>();
        }

    }

}

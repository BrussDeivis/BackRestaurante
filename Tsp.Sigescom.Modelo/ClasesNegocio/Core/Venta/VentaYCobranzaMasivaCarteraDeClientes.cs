using System;
using System.Collections.Generic;
using Tsp.Sigescom.Config;
using System.Linq;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class VentaYCobranzaCarteraDeClientes
    {

        private long idTransaccion;
        private int idCentroAtencion;
        private string nombreCentroAtencion;
        private VentaMasiva venta;
        private CobranzaMasiva conbranza;
        /// <summary>
        ///     Fecha que se emite cuando la venta y cobranza de cartera
        /// </summary>
        private DateTime fechaEmision;

        public VentaYCobranzaCarteraDeClientes( long idTransaccion, int idCentroAtencion, string nombreCentroAtencion, VentaMasiva venta, CobranzaMasiva cobranza)
        {
            this.IdTransaccion = idTransaccion;
            this.IdCentroAtencion = idCentroAtencion;
            this.NombreCentroAtencion = nombreCentroAtencion;
            this.FechaEmision = venta != null ? venta.FechaEmision : cobranza != null ? cobranza.FechaEmision : new DateTime();
            this.Venta = venta;
            this.Cobranza = cobranza;
        }

        public VentaYCobranzaCarteraDeClientes(int idCentroAtencion, string nombreCentroAtencion, DateTime fechaEmision, VentaMasiva venta, CobranzaMasiva cobranza)
        {
            this.IdTransaccion = idTransaccion;
            this.IdCentroAtencion = idCentroAtencion;
            this.NombreCentroAtencion = nombreCentroAtencion;
            this.FechaEmision = fechaEmision;
            this.Venta = venta;
            this.Cobranza = cobranza;
        }

        public long IdTransaccion { get => idTransaccion; set => idTransaccion = value; }
        public DateTime FechaEmision { get => fechaEmision; set => fechaEmision = value; }
        public VentaMasiva Venta { get => venta; set => venta = value; }
        public CobranzaMasiva Cobranza { get => conbranza; set => conbranza = value; }
        public int IdCentroAtencion { get => idCentroAtencion; set => idCentroAtencion = value; }
        public string NombreCentroAtencion { get => nombreCentroAtencion; set => nombreCentroAtencion = value; }

        public List<int> ListaClientesUnificado()
        {
            List<int> idsClientesUnificados = new List<int>();
            if(Venta != null)
            {
                idsClientesUnificados.AddRange(Venta.Detalles.Select(d => d.IdCliente).ToList());
            }
            if (Cobranza != null)
            {
            idsClientesUnificados.AddRange(Cobranza.Detalles.Select(d => d.IdCliente).ToList());
            }
            return idsClientesUnificados.Distinct().ToList();
        }
    }

}

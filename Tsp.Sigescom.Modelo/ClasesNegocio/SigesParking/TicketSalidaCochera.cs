using System;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.Entidades.ComprobantesModel;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.Custom.SigesParking
{
    public class TicketSalidaCochera : ComprobanteImpreso
    {
        public byte[] CodigoBarra { get; set; }
        public string CodigoBarraSrc { get { return ("data:image/jpeg;base64," + Convert.ToBase64String(CodigoBarra, 0, CodigoBarra.Length)); } }
        public override string NombreTipo { get; set; } = "SALIDA COCHERA";
        public MovimientoCochera Salida { get; set; }

        public TicketSalidaCochera(EstablecimientoComercialExtendidoConLogo sede, EstablecimientoComercialExtendido establecimientoOperacion, MovimientoCochera movimiento, string serieTicket, int numeroTicket, byte[] codigoBarrasMovimiento ) : base()
        {
            this.Serie = serieTicket;
            this.Numero = numeroTicket;
            this.EsInvalidada = !movimiento.EsValido;
            this.CodigoBarra = codigoBarrasMovimiento;
            this.Emisor = new Emisor(sede, establecimientoOperacion);
            this.FechaEmision = movimiento.Salida;
            this.IdEstadoActual = MaestroSettings.Default.IdDetalleMaestroEstadoIngresado;
            this.Salida = movimiento;
        }
    }
}

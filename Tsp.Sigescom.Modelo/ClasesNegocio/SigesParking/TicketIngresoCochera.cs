using System;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.Entidades.ComprobantesModel;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.Custom.SigesParking
{
    public class TicketIngresoCochera: ComprobanteImpreso
    {
        public byte[] CodigoBarra { get; set; }
        public string CodigoBarraSrc { get { return ("data:image/jpeg;base64," + Convert.ToBase64String(CodigoBarra, 0, CodigoBarra.Length)); } }

        public override string NombreTipo { get; set; } = "INGRESO COCHERA";

        public Ingreso Ingreso { get; set; }

        public TicketIngresoCochera(EstablecimientoComercialExtendidoConLogo sede, EstablecimientoComercialExtendidoConLogo establecimientoOperacion, Ingreso ingreso, string serieTicket, int numeroTicket, byte[] codigoBarras):base  ()
        {
            this.Serie= serieTicket;
            this.Numero= numeroTicket;
            this.EsInvalidada = !ingreso.EsValido;
            this.CodigoBarra = codigoBarras;
            this.Emisor = new Emisor(sede, establecimientoOperacion);
            this.FechaEmision = ingreso.FechaHora;
            this.IdEstadoActual = MaestroSettings.Default.IdDetalleMaestroEstadoIngresado;
            this.Ingreso = ingreso;
        }

        
    }
}

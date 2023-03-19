using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.Partial
{
    public class Orden_Recibir_Entregar
    {
        private long id;
        private DateTime fechaInicio;
        private string codigoTipoComprobante;
        private string tipoComprobante;
        private string serieComprobante;
        private int numeroComprobante;
        private string tipoDeOperacion;
        private string serieComprobanteOrden;
        private int numeroComprobanteOrden;
        private string codigoTipoDocumentoActorComercial;
        private string numeroDocumentoActorComercial;
        private string actorComercial;
        private bool porRecibir;
        private string centroDeAtencion;
        private string establecimiento;


        public long Id { get => id; set => id = value; }
        public DateTime FechaInicio { get => fechaInicio; set => fechaInicio = value; }
        public string CodigoTipoComprobante { get => codigoTipoComprobante; set => codigoTipoComprobante = value; }
        public string TipoComprobante { get => tipoComprobante; set => tipoComprobante = value; }
        public string SerieComprobante { get => serieComprobante; set => serieComprobante = value; }
        public int NumeroComprobante { get => numeroComprobante; set => numeroComprobante = value; }
        public string TipoDeOperacion { get => tipoDeOperacion; set => tipoDeOperacion = value; }
        public string SerieComprobanteOrden { get => serieComprobanteOrden; set => serieComprobanteOrden = value; }
        public int NumeroComprobanteOrden { get => numeroComprobanteOrden; set => numeroComprobanteOrden = value; }
        public string CodigoTipoDocumentoActorComercial { get => codigoTipoDocumentoActorComercial; set => codigoTipoDocumentoActorComercial = value; }
        public string NumeroDocumentoActorComercial { get => numeroDocumentoActorComercial; set => numeroDocumentoActorComercial = value; }
        public string ActorComercial { get => actorComercial; set => actorComercial = value; }
        public bool PorRecibir { get => porRecibir; set => porRecibir = value; }
        public string CentroDeAtencion { get => centroDeAtencion; set => centroDeAtencion = value; }
        public string Establecimiento { get => establecimiento; set => establecimiento = value; }

        public string Fecha { get => FechaInicio.ToString("dd/MM/yyyy hh:mm:ss tt"); }
        public string Comprobante { get => SerieComprobante + "-" + NumeroComprobante; }
        public string ComprobanteOrden { get => SerieComprobanteOrden + "-" + NumeroComprobanteOrden; }
        public string Tercero { get => CodigoTipoDocumentoActorComercial + " : " + NumeroDocumentoActorComercial + " : " + ActorComercial.Replace("|", " "); }
    }
}

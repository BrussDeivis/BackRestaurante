using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.Partial
{
    public class Cobro_Pago
    {
        private long id;
        private string codigoTipoDocumentoEmpleado;
        private string numeroDocumentoEmpleado;
        private string empleado;
        private string codigoTipoDocumentoActorComercial;
        private string numeroDocumentoActorComercial;
        private string actorComercial;
        private decimal total;
        private DateTime fechaInicio;
        private string medioDePago;
        private bool esCobro;
        private string tipoDeOperacion;
        private string centroAtencion;
        private string establecimiento;
        private string serieNumeroComprobante;
        private string nombreComprobante;


        public long Id { get => id; set => id = value; }
        public string CodigoTipoDocumentoEmpleado { get => codigoTipoDocumentoEmpleado; set => codigoTipoDocumentoEmpleado = value; }
        public string NumeroDocumentoEmpleado { get => numeroDocumentoEmpleado; set => numeroDocumentoEmpleado = value; }
        public string Empleado { get => empleado; set => empleado = value; }
        public string CodigoTipoDocumentoActorComercial { get => codigoTipoDocumentoActorComercial; set => codigoTipoDocumentoActorComercial = value; }
        public string NumeroDocumentoActorComercial { get => numeroDocumentoActorComercial; set => numeroDocumentoActorComercial = value; }
        public string ActorComercial { get => actorComercial; set => actorComercial = value; }
        public decimal Total { get => total; set => total = value; }
        public DateTime FechaInicio { get => fechaInicio; set => fechaInicio = value; }
        public string MedioDePago { get => medioDePago; set => medioDePago = value; }
        public bool EsCobro { get => esCobro; set => esCobro = value; }
        public string TipoDeOperacion { get => tipoDeOperacion; set => tipoDeOperacion = value; }
        public string CentroAtencion { get => centroAtencion; set => centroAtencion = value; }
        public string Establecimiento { get => establecimiento; set => establecimiento = value; }
        public string Fecha { get => FechaInicio.ToString("dd/MM/yyyy hh:mm:ss tt"); }
        public string Pagador { get => esCobro ? CodigoTipoDocumentoActorComercial + " : " + NumeroDocumentoActorComercial + " : " + ActorComercial : CodigoTipoDocumentoEmpleado + " : " + NumeroDocumentoEmpleado + " : " + Empleado; }
        public string Recibidor { get => esCobro ? CodigoTipoDocumentoEmpleado + " : " + NumeroDocumentoEmpleado + " : " + Empleado : CodigoTipoDocumentoActorComercial + " : " + NumeroDocumentoActorComercial + " : " + ActorComercial; }
        public string SerieNumeroDocumento { get => serieNumeroComprobante; set => serieNumeroComprobante = value; }
        public string TipoDocumento { get => nombreComprobante; set => nombreComprobante = value; }

        //this.Documento = ActorComercial.CodigoTipoDocumentoIdentidad() + " " + pago.ActorComercial().DocumentoIdentidad;
        //  this.ActorComercial = pago.ActorComercial().RazonSocial;
        //  this.Total = pago.Total.ToString("0.00");
        //  this.FechaDePago = pago.FechaEmision.ToString("dd/MM/yyyy");
        //  //this.Afavor = pago.aFavor();
        //  this.MedioDePago = pago.TrazaDePago() != null ? pago.TrazaDePago().MedioDePago().nombre : "Efectivo";
    }
}

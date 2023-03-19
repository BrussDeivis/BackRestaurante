using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.Partial
{
    public class Entrada_Salida_Almacen
    {
        private long id;
        private DateTime fechaInicio;
        private string codigoTipoComprobante;
        private string tipoComprobante;
        private string serieComprobante;
        private int numeroComprobante;
        private string tipoDeOperacion;
        private string codigoTipoDocumentoEmpleado;
        private string numeroDocumentoEmpleado;
        private string empleado;
        private string codigoTipoDocumentoActorComercial;
        private string numeroDocumentoActorComercial;
        private string actorComercial;
        private bool esEntrada;
        private string centroDeAtencion;
        private string establecimiento;

        public long Id { get => id; set => id = value; }
        public DateTime FechaInicio { get => fechaInicio; set => fechaInicio = value; }
        public string CodigoTipoComprobante { get => codigoTipoComprobante; set => codigoTipoComprobante = value; }
        public string TipoComprobante { get => tipoComprobante; set => tipoComprobante = value; }
        public string SerieComprobante { get => serieComprobante; set => serieComprobante = value; }
        public int NumeroComprobante { get => numeroComprobante; set => numeroComprobante = value; }
        public string TipoDeOperacion { get => tipoDeOperacion; set => tipoDeOperacion = value; }
        public string CodigoTipoDocumentoEmpleado { get => codigoTipoDocumentoEmpleado; set => codigoTipoDocumentoEmpleado = value; }
        public string NumeroDocumentoEmpleado { get => numeroDocumentoEmpleado; set => numeroDocumentoEmpleado = value; }
        public string Empleado { get => empleado; set => empleado = value; }
        public string CodigoTipoDocumentoActorComercial { get => codigoTipoDocumentoActorComercial; set => codigoTipoDocumentoActorComercial = value; }
        public string NumeroDocumentoActorComercial { get => numeroDocumentoActorComercial; set => numeroDocumentoActorComercial = value; }
        public string ActorComercial { get => actorComercial; set => actorComercial = value; }
        public bool EsEntrada { get => esEntrada; set => esEntrada = value; }
        public string CentroDeAtencion { get => centroDeAtencion; set => centroDeAtencion = value; }
        public string Establecimiento { get => establecimiento; set => establecimiento = value; }
        public string Fecha { get => FechaInicio.ToString("dd/MM/yyyy hh:mm:ss tt"); }
        public string Comprobante { get => SerieComprobante + "-" + NumeroComprobante; }
        public string OrigenDestino { get => CodigoTipoDocumentoActorComercial + " : " + NumeroDocumentoActorComercial + " : " + ActorComercial; }
        public string Responsable { get => CodigoTipoDocumentoEmpleado + " : " + NumeroDocumentoEmpleado + " : " + Empleado; }
    }
}

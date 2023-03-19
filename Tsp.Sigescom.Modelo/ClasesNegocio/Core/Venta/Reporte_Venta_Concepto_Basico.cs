using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.Partial
{
    public class Reporte_Concepto_Basico
    {

        
        private DateTime fechaInicio;
        private decimal cantidadVendida;
        private string nombreConceptoBasico;
        private int idConceptoNegocio;
        private string nombreConceptoNegocio;
        private string sufijo;

        private string nombrePresentacion;
        private decimal contenido;
        private string valorUnidadMedida;
        private string nombreUnidadMedida;

        private int idTipoComprobante;
        private string codigoTipoComprobante;
        private int numeroComprobante;
        private string numeroSerieComprobante;

        private string nombreCliente;
        private string numeroDocumentoCliente;

        private string direccionCentroDeAtencion;
        private int codigoUbigeo;




        public Reporte_Concepto_Basico()
        {

        }

        public DateTime FechaInicio { get => fechaInicio; set => fechaInicio = value; }
        public string NombreConceptoBasico { get => nombreConceptoBasico; set => nombreConceptoBasico = value; }
        public int IdConceptoNegocio { get => idConceptoNegocio; set => idConceptoNegocio = value; }
        public string NombreConceptoNegocio { get => nombreConceptoNegocio; set => nombreConceptoNegocio = value; }
        public string Sufijo { get => sufijo; set => sufijo = value; }
        public decimal Contenido { get => contenido; set => contenido = value; }
        public string ValorUnidadMedida { get => valorUnidadMedida; set => valorUnidadMedida = value; }
        public string CodigoTipoComprobante { get => codigoTipoComprobante; set => codigoTipoComprobante = value; }
        public int NumeroComprobante { get => numeroComprobante; set => numeroComprobante = value; }
        public string NombreCliente { get => nombreCliente; set => nombreCliente = value; }
        public string NumeroDocumentoCliente { get => numeroDocumentoCliente; set => numeroDocumentoCliente = value; }
        public string DireccionCentroDeAtencion { get => direccionCentroDeAtencion; set => direccionCentroDeAtencion = value; }

        public int IdTipoComprobante { get => idTipoComprobante; set => idTipoComprobante = value; }
        public int CodigoUbigeo { get => codigoUbigeo; set => codigoUbigeo = value; }
        public string NumeroSerieComprobante { get => numeroSerieComprobante; set => numeroSerieComprobante = value; }
        public decimal CantidadVendida { get => cantidadVendida; set => cantidadVendida = value; }
        public string NombreUnidadMedida { get => nombreUnidadMedida; set => nombreUnidadMedida = value; }
        public string NombrePresentacion { get => nombrePresentacion; set => nombrePresentacion = value; }

    }
}

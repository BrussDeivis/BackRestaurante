using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades
{
    public class VentaConceptoCliente
    {
        public string DocumentoIdentidad { get; set; }
        public string RazonSocial { get; set; }
        public string Direccion { get; set; }
        public int IdUbigeo { get; set; }
        public string Ubigeo { get; set; }
        public string Departamento { get => IdUbigeo == ActorSettings.Default.idUbigeoNoEspecificado ? "-" : Ubigeo.Split('-')[0]; }
        public string Provincia { get => IdUbigeo == ActorSettings.Default.idUbigeoNoEspecificado ? "-" : Ubigeo.Split('-')[1]; }
        public string Distrito { get => IdUbigeo == ActorSettings.Default.idUbigeoNoEspecificado ? "-" : Ubigeo.Split('-')[2]; }
        public DateTime FechaInicio { get; set; }
        public string FechaEmision { get => FechaInicio.ToString(); }
        public string SerieComprobante { get; set; }
        public int NumeroComprobante { get; set; }
        public string Comprobante { get => SerieComprobante + "-" + NumeroComprobante.ToString(); }
        public string Concepto { get; set; }
        public decimal Cantidad { get; set; }
        public string UnidadMedida { get; set; }


        public static List<VentaConceptoCliente> Convert()
        {
            return new List<VentaConceptoCliente>();
        }

    }


}

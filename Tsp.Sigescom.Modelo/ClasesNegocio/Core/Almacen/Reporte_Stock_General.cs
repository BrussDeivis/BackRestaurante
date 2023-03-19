using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.Partial
{
    public class Reporte_Stock_General
    {

        private string codigoBarraConceptoNegocio;
        private string nombreConceptoNegocio;
        private string unidadMedida;
        private string lote;
        private decimal stock;
        private string nombreConcepto;
        private string nombreCentroAtencion;
        private int idCentroAtencion;


        public Reporte_Stock_General()
        {

        }

        public string CodigoBarraConceptoNegocio { get => codigoBarraConceptoNegocio; set => codigoBarraConceptoNegocio = value; }
        public string NombreConceptoNegocio { get => nombreConceptoNegocio; set => nombreConceptoNegocio = value; }
        public string UnidadMedida { get => unidadMedida; set => unidadMedida = value; }
        public string Lote { get => lote; set => lote = value; }
        public decimal Stock { get => stock; set => stock = value; }
        public string NombreConcepto { get => nombreConcepto; set => nombreConcepto = value; }
        public string NombreCentroAtencion { get => nombreCentroAtencion; set => nombreCentroAtencion = value; }
        public int IdCentroAtencion { get => idCentroAtencion; set => idCentroAtencion = value; }
        public bool TieneStock { get => Stock > 0; }

        public static List<Reporte_Stock_General> Convert()
        {
            return new List<Reporte_Stock_General>();
        }
    }


}

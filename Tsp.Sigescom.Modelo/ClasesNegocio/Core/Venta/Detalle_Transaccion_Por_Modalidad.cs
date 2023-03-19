using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.Partial
{
    [Serializable]
    public class Detalle_Transaccion_Por_Modalidad
    {

        public string IdModalidad { get; set; }
        public string Modalidad {
            get
            { return Enumerado.GetDescription((ModoOperacionEnum)Enum.Parse(typeof(ModoOperacionEnum),IdModalidad, true)).ToUpper(); }
        }
        public int IdConceptoNegocio { get; set; }
        public string CodigoConceptoNegocio { get; set; }
        public string NombreConceptoNegocio { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario
        {
            get
            { return Importe / Cantidad; }
        }
        public decimal Importe { get; set; }

        public Detalle_Transaccion_Por_Modalidad()
        {}

        public static List<Detalle_Transaccion_Por_Modalidad> Convert()
        {
            return new List<Detalle_Transaccion_Por_Modalidad>();
        }
    }
}

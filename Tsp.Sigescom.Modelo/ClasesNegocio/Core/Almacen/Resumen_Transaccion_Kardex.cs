using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.Partial
{
    [Serializable]
    public class Resumen_Transaccion_Kardex
    {
        public string IdModalidad { get; set; }
        public string Modalidad
        {
            get
            { return Enumerado.GetDescription((ModoOperacionEnum)Enum.Parse(typeof(ModoOperacionEnum), IdModalidad, true)).ToUpper(); }
        }
        public int IdTipoDeTransaccion { get; set; }
        public int CantidadDeOperaciones { get; set; }
        public decimal Importe { get; set; }

        public Resumen_Transaccion_Kardex()
        {
            
        }
        public static List<Resumen_Transaccion_Por_Modalidad> Convert()
        {
            return new List<Resumen_Transaccion_Por_Modalidad>();
        }
    }
}

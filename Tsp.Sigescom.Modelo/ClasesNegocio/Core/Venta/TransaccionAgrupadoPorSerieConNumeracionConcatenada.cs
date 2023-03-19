using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.Partial
{
    [Serializable]
    public class TransaccionAgrupadoPorSerieConNumeracionConcatenada
    {
        /// <summary>
        /// Clase para el reporte de Comprobante general por serie.
        /// Conocido como comprobante agrupados por serie (Concatenados: 1,2,3,4)
        /// </summary>
        public string NombreCortoDocumento { get; set; }
        public string Serie { get; set; }
        public decimal ImporteSinIcbper { get; set; }
        //public decimal Icbper { get; set; }
        public string Comprobantes { get {return string.Join(";",NumerosComprobantes.ToArray()); }}
        public IEnumerable<int> NumerosComprobantes { get; set; }

        public IEnumerable<string> Icbpers { get; set; }
        public decimal IcbperCalculado { get => (Icbpers != null) && Icbpers.Count() > 0 ? Icbpers.Sum(i => Decimal.Parse(i)) : 0; }

        public decimal Importe { get => ImporteSinIcbper + IcbperCalculado;}


        public TransaccionAgrupadoPorSerieConNumeracionConcatenada()
        {
        }

        public static List<TransaccionAgrupadoPorSerieConNumeracionConcatenada> Convert()
        {
            return new List<TransaccionAgrupadoPorSerieConNumeracionConcatenada>();
        }
    }
}

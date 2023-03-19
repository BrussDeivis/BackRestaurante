using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.Partial
{
    [Serializable]
    public class TransaccionAgrupadoPorSerieConNumeracionInicioFin
    {
        /// <summary>
        /// Clase para el reporte de Comprobante general por serie.
        /// Conocido como comprobante agrupados por serie (Inicio - Fin)
        /// </summary>
        /// 
        private int idTipoComprobante;
        private string nombreCortoDocumento;
        private string serie;
        private int numeroInicial;
        private int numeroFinal;
        private decimal valorVenta;
        private decimal? igv;
        private IEnumerable<string> icbpers;
        private decimal importe;
        private decimal icbper;



        public TransaccionAgrupadoPorSerieConNumeracionInicioFin()
        {
        }

        public int IdTipoComprobante { get => idTipoComprobante; set => idTipoComprobante = value; }
        public string NombreCortoDocumento { get => nombreCortoDocumento; set => nombreCortoDocumento = value; }
        public string Serie { get => serie; set => serie = value; }
        public int NumeroInicial { get => numeroInicial; set => numeroInicial = value; }
        public int NumeroFinal { get => numeroFinal; set => numeroFinal = value; }
        public int Signo { get => IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito ? -1 : 1; }
        public decimal ValorVenta { get => (decimal)(valorVenta - igv) * Signo; set => valorVenta = value; }
        public decimal? Igv { get => igv * Signo; set => igv = value; }
        public IEnumerable<string> Icbpers { get => icbpers; set => icbpers = value; }
        public decimal Importe { get => importe * Signo; set => importe = value; }
        public decimal IcbperCalculado { get => (icbpers != null) && icbpers.Count() > 0 ? icbpers.Sum(i => Decimal.Parse(i)) : 0; }
        public decimal Icbper { get => IcbperCalculado * Signo ; set => icbper = value; }



    public static List<TransaccionAgrupadoPorSerieConNumeracionInicioFin> Convert()
        {
            return new List<TransaccionAgrupadoPorSerieConNumeracionInicioFin>();
        }
    }
}

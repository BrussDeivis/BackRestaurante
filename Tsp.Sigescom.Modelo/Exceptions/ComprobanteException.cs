using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades
{
    public class SerieComprobanteException : Exception
    {
        public Serie_comprobante serieComprobante { get; set; }
        public SerieComprobanteException(Exception dbConcurrencyException, Serie_comprobante serieComprobante) : base("El valor del número de comprobante para la serie "+ serieComprobante.numero + " ha variado", dbConcurrencyException)
        {
            this.serieComprobante = serieComprobante;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.Partial
{
    [Serializable]
    public class Precio_Concepto
    {
        public int Id { get; set; }
        public int IdConcepto { get; set; }
        public string Concepto { get; set; }
        public int IdTarifa { get; set; }
        public string Tarifa { get; set; }
        public decimal Precio { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public bool EsVigente { get; set; }

        public Precio_Concepto()
        {
        }
        public string PrecioString
        {
            get { return this.Precio.ToString("N" + AplicacionSettings.Default.NumeroDecimalesEnPrecio); }
        }

        public string FechaDesdeString
        {
            get { return this.FechaDesde.ToString("dd/MM/yyyy"); }
        }

        public string FechaHastaString
        {
            get { return this.FechaHasta.ToString("dd/MM/yyyy"); }
        }
    }
}

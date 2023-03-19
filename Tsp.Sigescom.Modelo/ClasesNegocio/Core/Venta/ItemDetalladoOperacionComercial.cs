using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.Partial
{
    /// <summary>
    /// por ejemplo, se agrupa por caracteristica
    /// </summary>
    public class ItemDetalladoOperacionComercial
    {
        public long IdItem { get; set; }
        /// <summary>
        /// operacion que contiene el detalle (cantidad, item, importe)
        /// </summary>
        public long IdOperacion { get; set; }

        /// <summary>
        /// Operacion  que envuelve a la operacion que contiene el detalle (cantidad, item, importe)
        /// </summary>
        public long? IdOperacionWrapper { get; set; }
        public string Comprobante { get; set; }
        public DateTime Fecha { get; set; }

        public string Familia { get; set; }
        public string Sufijo { get; set; }
        public string Caracteristica1 { get; set; }
        public string Caracteristica2 { get; set; }
        public string Caracteristica3 { get; set; }
        public string Caracteristica4 { get; set; }
        public string Caracteristica5 { get; set; }
        public string Caracteristica6 { get; set; }
        public string Caracteristica7 { get; set; }
        public string Caracteristica8 { get; set; }
        public string Caracteristica9 { get; set; }
        public string Caracteristica10 { get; set; }
        public decimal Cantidad{ get; set; }
        public decimal Importe { get; set; }
        public decimal PrecioUnitario
        { 
            get
            { return Importe / Cantidad; }
        }
        public string MedioPago { get; set; }
        public ItemDetalladoOperacionComercial()
        { }
        public ItemDetalladoOperacionComercial Clone()
        {
            return (ItemDetalladoOperacionComercial)this.MemberwiseClone();
        }

        public static List<ItemDetalladoOperacionComercial> Convert()
        {
            return new List<ItemDetalladoOperacionComercial>();
        }

        
    }
}

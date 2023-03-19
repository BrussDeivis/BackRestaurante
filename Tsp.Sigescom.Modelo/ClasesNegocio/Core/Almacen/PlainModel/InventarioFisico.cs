using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel
{
    public class InventarioFisico
    {
        public long IdDetalleTransaccion { get; set; }
        public long IdTransaccion { get; set; }
        public DateTime Fecha { get; set; }
        public int IdAlmacen { get; set; }
        public string NombreAlmacen { get; set; }
        public int IdConcepto { get; set; }
        public string CodigoBarra { get; set; }
        public string Concepto { get; set; }
        public string Familia { get; set; }
        public string UnidadMedida { get; set; }
        public string Lote { get; set; }
        public decimal Cantidad { get; set; }
        public decimal CantidadSecundaria { get; set; }
        public bool TieneStock { get { return Cantidad > 0; } }

        public InventarioFisico()
        {

        }
        public List<InventarioFisico> Convert()
        {
            return new List<InventarioFisico>();
        }

        

    }
}

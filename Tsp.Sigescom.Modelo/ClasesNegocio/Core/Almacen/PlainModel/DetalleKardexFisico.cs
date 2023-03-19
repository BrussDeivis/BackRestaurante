using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel
{
    public class DetalleKardexFisico
    {
        public int Index { get; set; }
        public DateTime Fecha { get; set; }
        public string ActorExterno { get; set; }
        public string Operacion { get; set; }
        public string CodigoTipoComprobante { get; set; }
        public string SerieYNumeroComprobante { get; set; }
        public Decimal CantidadEntrada { get; set; }
        public Decimal CantidadSalida { get; set; }
        public Decimal CantidadSaldo { get; set; }




        public DetalleKardexFisico()
        { }

        public static List<DetalleKardexFisico> Convert()
        {
            return new List<DetalleKardexFisico>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel
{
    [Serializable]
    public class MovimientoAlmacen
    {
        public long IdDetalleTransaccion { get; set; }
        public long IdConcepto { get; set; }
        public long? IdTransaccionMovimientoOrigen { get; set; }
        public long? IdTransaccionPadre { get; set; }

        public long IdTransaccion { get; set; }
        public long? IdOrden { get; set; }
        public DateTime Fecha { get; set; }
        public int IdActorNegocioExterno { get; set; }
        public string NombreActorNegocioExterno { get; set; } // CLIENTE O PROVEEDOR
        public int IdActorNegocioInterno { get; set; }//
        public string NombreActorNegocioInterno { get; set; }
        public int IdEmpleado { get; set; }
        public string Empleado { get; set; }
        public int IdTipoTransaccion { get; set; }
        public string NombreTipoTransaccion { get; set; }
        public int IdTipoComprobante { get; set; }
        public string NombreTipoComprobante { get; set; }
        public int NumeroComprobante { get; set; }
        public string CodigoTipoComprobante { get; set; }
        public string NumeroSerieDeComprobante { get; set; }
        public string NumeroSerieDeSerieComprobante { get; set; }

        public decimal Cantidad { get; set; }
        public decimal ImporteUnitario { get; set; }
        public decimal ImporteTotal { get; set; }
        public decimal ImporteTotalOrden { get; set; }
        public decimal IgvOrden { get; set; }


        public string NumeroSerie
        {
            get
            { return NumeroSerieDeSerieComprobante ?? NumeroSerieDeComprobante; }
        }

        public bool EsEntrada { get; set; }




        public MovimientoAlmacen()
        { }

        public static List<MovimientoAlmacen> Convert()
        {
            return new List<MovimientoAlmacen>();
        }
    }
}

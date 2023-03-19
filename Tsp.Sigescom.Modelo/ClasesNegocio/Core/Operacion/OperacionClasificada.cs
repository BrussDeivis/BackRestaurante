using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class OperacionClasificada
    {
        private int clasificador;
        private OperacionGenericaNivel3 operacion;

        public OperacionClasificada(int clasificador, OperacionGenericaNivel3 operacion)
        {
            this.Clasificador = clasificador;
            this.Operacion = operacion;
        }


        public static List<OperacionClasificada> Clasificar(List<OperacionGenericaNivel3> operacionesGenericas)
        {


            /*
                Orden de venta y su estado es confirmado  = 1
                Orden de venta y su estasdo es invalidado = 2 
                Orden de venta y su estado es anulado = 3
            */

            List<OperacionClasificada> operacionesClasificadas = new List<OperacionClasificada>();

            foreach (var item in operacionesGenericas)
            {
                int clasificador =
                 item.IdTipoTransaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta &&
                 (item.IdEstadoActual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado ||
                 item.IdEstadoActual == MaestroSettings.Default.IdDetalleMaestroEstadoTransmitido) ? 1
                 : item.IdTipoTransaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta &&
                 item.IdEstadoActual == MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado ? 2
                 : item.IdTipoTransaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta && item.IdEstadoActual == MaestroSettings.Default.IdDetalleMaestroEstadoAnulado ? 3 : 4;
             /* item.IdTipoTransaccion == TransaccionSettings.Default.IdTipoTransaccionCobranzaFacturasClientes && (item.IdEstadoActual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado || item.IdEstadoActual == MaestroSettings.Default.IdDetalleMaestroEstadoTransmitido*/ 

             operacionesClasificadas.Add(new OperacionClasificada(clasificador, item));
            }

            return operacionesClasificadas;
        }

        public int Clasificador { get => clasificador; set => clasificador = value; }
        public OperacionGenericaNivel3 Operacion { get => operacion; set => operacion = value; }
    }
    
}

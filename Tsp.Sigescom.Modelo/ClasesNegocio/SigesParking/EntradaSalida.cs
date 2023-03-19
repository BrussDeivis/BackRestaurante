using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;

namespace Tsp.Sigescom.Modelo.Custom.SigesParking
{
    public class EntradaSalida
    {
        /// <summary>
        /// Id de la transacción: representa tanto a la entrada como a la salida
        /// </summary>
        public long IdGeneral { get; set; }
        /// <summary>
        /// Id del estado_transaccion, representa a la entrada o salida
        /// </summary>
        public long IdEspecifico { get; set; }
        /// <summary>
        /// Id del estado
        /// </summary>
        public int IdTipoMovimiento { get; set; }
        public string Tipo { get { return IdTipoMovimiento == MaestroSettings.Default.IdDetalleMaestroEstadoIngresado ? "INGRESO" : IdTipoMovimiento == MaestroSettings.Default.IdDetalleMaestroEstadoFinalizado ? "SALIDA" : throw new ModeloException("Tipo de movimiento no válido. Se esperada ingreso o salida"); } }
        public DateTime FechaHora { get; set; }
        public string Ticket { get; set; }
        public string PlacaVehiculo { get; set; }
        public string DescripcionVehiculo { get; set; }

        public EntradaSalida()
        {
        }

        public List<EntradaSalida> Convert()
        {
            return null;
        }
    }
}

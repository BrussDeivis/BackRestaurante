using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Sesion;

namespace Tsp.Sigescom.Modelo.Custom.SigesParking
{

    public class TiempoHoras
    {
        /// <summary>
        /// Tiempo exacto de  horas. Ya tiene descontada la tolerancia
        /// </summary>
        public decimal Horas { get; set; }
        /// <summary>
        /// Se aplica redondeo hacia arriba por la minima fraccion de 1 hora
        /// </summary>
        public decimal HorasACobrar { get { return Math.Ceiling(this.Horas); } }
        public string HorasString { get; set; }
    }
    public class MovimientoCochera: MovimientoCocheraBasico
    {
        
        public bool PerdidaTicket { get; set; }
        public DetallesACobrar DetallesACobrar { get; set; }
        public bool VentaConsolidada { get; set; }
        public TiempoHoras TiempoExcesoSistemaPlanaPorTurnos { get; set; }
        public TiempoHoras TiempoSistemaPorHoras { get; set; }
        //todo: deberia contemplarse un parámetro de configuración que indique la fracción mínima de cobro. ya que podria ser 15 minutos, 30 minutos, etc.

        public MovimientoCochera()
        {
        }


        /// <summary>
        /// modifica la transaccion en base a los datos del movimiento de cochera. Datos de salida de cochera.
        /// </summary>
        /// <param name="transaccion"></param>
        public void EstablecerDatosDeSalida(Transaccion transaccion)
        {
            transaccion.indicador1 = this.PerdidaTicket;
            transaccion.indicador2 = this.VentaConsolidada;
            transaccion.importe1 = this.DetallesACobrar.Principal;
            transaccion.importe2 = this.DetallesACobrar.Exceso;
            transaccion.importe3 = this.DetallesACobrar.Ticket;
            transaccion.importe_total = this.DetallesACobrar.Principal + this.DetallesACobrar.Exceso + this.DetallesACobrar.Ticket;
            transaccion.fecha_fin =   this.Salida;
        }

        public List<MovimientoCochera> Convert()
        {
            return null;
        }
    }
}

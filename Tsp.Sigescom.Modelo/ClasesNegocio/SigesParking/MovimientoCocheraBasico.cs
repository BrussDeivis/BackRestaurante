using System;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.Custom.SigesParking
{
    public class MovimientoCocheraBasico
    {
        public long Id { get; set; }
        public long IdOrdenDeVenta { get; set; }
        public int IdCochera { get; set; }
        public Vehiculo Vehiculo { get; set; }
        public ActorComercial_ Cliente { get; set; }
        public DateTime Ingreso { get; set; }
        public DateTime Salida { get; set; }
        public bool EsValido { get; set; }
        public ComprobanteDeNegocioBasico_ Comprobante { get; set; }
        public ItemGenerico Estado{ get; set; }
        public string Observacion { get; set; }
        public ItemGenerico SistemaDePago { get; set; }
        public TurnoCochera Turno { get; set; }

        public string IngresoString { get { return Ingreso.ToString("dd/MM/yyyy h:mm tt"); } }
        public string SalidaString { get { return Salida.ToString("dd/MM/yyyy h:mm tt"); } }
        public bool PuedeVer { get {return true; } }
        public bool PuedeEditar { get { return this.Estado!=null&&this.Estado.Id== MaestroSettings.Default.IdDetalleMaestroEstadoIngresado; } }
        public bool PuedeInvalidar { get { return this.Estado != null && this.Estado.Id != MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado; } }
        public bool PuedeFinalizar { get { return this.Estado != null && this.Estado.Id == MaestroSettings.Default.IdDetalleMaestroEstadoIngresado; } }




        public MovimientoCocheraBasico()
        {
        }
    }
}

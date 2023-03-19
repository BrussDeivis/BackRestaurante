using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class FlujoDeAlmacen : OperacionGenericaNivel3
    {
        /*
        public FlujoDeAlmacen() 
        {

        }
        public FlujoDeAlmacen(Transaccion transaccion) : base(transaccion)
        {

        } 
        public int IdTipoOperacionAlamcen
        {
            get { return this.transaccion.id_tipo_transaccion; }
        }
        public bool? SeEntregoMercaderiaTotalMente()
        {
            var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroEstadoEntregaMercaderia);
            return parametro != null ? (bool?)parametro.valor.Equals("1") : null;
        }
        public DateTime? FechaInicioTransporte()
        {
            var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroFechaInicioTransporte);
            return parametro != null ? (DateTime?)System.Convert.ToDateTime(parametro.valor) : null;
        }
        public int? IdTransportista()
        {
            var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroIdTransportista);
            return parametro != null ? (int?)System.Convert.ToInt32(parametro.valor) : null;
        }
        public string PlacaMarcaTransportista()
        {
            var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroPlacaMarcaTransportista);
            return parametro != null ? parametro.valor : null;
        }
        public string NumeroLicenciaTransportista()
        {
            var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroNumeroLicenciaTransportista);
            return parametro != null ? parametro.valor : null;
        }
        public int? IdModalidadTransporte()
        {
            var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroIdModalidadTransporte);
            return parametro != null ? (int?)System.Convert.ToInt32(parametro.valor) : null; ;
        }
        public int? IdMotivoTransporte()
        {
            var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroIdMotivoTransporte);
            return parametro != null ? (int?)System.Convert.ToInt32(parametro.valor) : null;
        }
        public string Observacion()
        {
            return this.transaccion.comentario;
        }

        public OrdenDeMovimientoDeAlmacen OperacionDeReferencia()
        {
            return this.transaccion.Transaccion3 != null ? new OrdenDeMovimientoDeAlmacen(this.transaccion.Transaccion3) : null;
        }

        public List<DetalleOrdenDesplazamiento> Detalles()
        {
            return DetalleOrdenDesplazamiento.Convert_(this.transaccion.Detalle_transaccion.ToList());
        }

        public int IdTipoComprobante
        {
            get { return this.transaccion.Detalle_maestro.id_maestro; }
        }




        public static List<OrdenDeMovimientoDeAlmacen> Convert(List<Transaccion> transaciones)
        {
            List<OrdenDeMovimientoDeAlmacen> ordenes = new List<OrdenDeMovimientoDeAlmacen>();
            foreach (var transaccion in transaciones)
            {
                ordenes.Add(new OrdenDeMovimientoDeAlmacen(transaccion));
            }
            return ordenes;
        }*/
    }
}

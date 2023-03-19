using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class OrdenDeMovimientoDeAlmacen : OperacionGenericaNivel3
    {
        //readonly int[] idTiposTransaccionDeMovimientoDeMercaderia = { TransaccionSettings.Default.IdTipoTransaccionIngresaBienOservicio, TransaccionSettings.Default.IdTipoTransaccionIngresoBienesPorCompra, TransaccionSettings.Default.IdTipoTransaccionIngresoMercaderíaDesplazamientoInterno, TransaccionSettings.Default.IdTipoTransaccionSaleBienOservicio, TransaccionSettings.Default.IdTipoTransaccionSalidaBienesPorVenta, TransaccionSettings.Default.IdTipoTransaccionSalidaMercaderíaDesplazamientoInterno, TransaccionSettings.Default.IdTipoTransaccionSalidaDeMercaderiaPorAnulaciónDeCompra, TransaccionSettings.Default.IdTipoDeTransaccionSalidaDeMercaderiaPorAnulacionDeCompraPorErrorEnElRuc, TransaccionSettings.Default.IdTipoDeTransaccionSalidaDeMercaderiaPorDevolucionTotalDeCompra, TransaccionSettings.Default.IdTipoDeTransaccionSalidaDeMercaderiaPorDevolucionPorItemEnCompra, TransaccionSettings.Default.IdTipoTransaccionSalidaBienesPorVenta, TransaccionSettings.Default.IdTipoTransaccionIngresoDeMercaderiaPorAnulaciónDeVenta, TransaccionSettings.Default.IdTipoDeTransaccionIngresoDeMercaderiaPorAnulacionDeVentaPorErrorEnElRuc, TransaccionSettings.Default.IdTipoDeTransaccionIngresoDeMercaderiaPorDevolucionTotalDeVenta, TransaccionSettings.Default.IdTipoDeTransaccionIngresoDeMercaderiaPorDevolucionPorItemEnVenta
        //};

        public OrdenDeMovimientoDeAlmacen()
        {
        }

        public OrdenDeMovimientoDeAlmacen(Transaccion transaccion) : base(transaccion)
        {
        }

        public int IdTipoOperacionAlmacen
        {
            get { return this.transaccion.id_tipo_transaccion; }
        }


        public int IdTipoComprobante
        {
            get { return this.transaccion.Comprobante.Detalle_maestro.id; }
        }

        public int IdTercero()
        {
            return this.transaccion.id_actor_negocio_externo;
        }

        public string Observacion()
        {
            return this.transaccion.comentario;
        }

        public OperacionGenericaNivel3 OrdenDeOperacion()
        {
            //Devuelve la orden de la operacion que genero la orden de almacen, ejm : orden de venta, orden de compra, orden de invalidacion, etc.
            return new OperacionGenericaNivel3(this.transaccion.Transaccion3);
        }

        public List<MovimientoDeAlmacen> MovimientosDeAlmacen()
        {
            return this.transaccion.Transaccion1.Count > 0 ? MovimientoDeAlmacen.Convertir(this.transaccion.Transaccion1.ToList()) : null;
        }

        public List<DetalleDeOperacion> Detalles()
        {
            return DetalleDeOperacion.Convert(this.transaccion.Detalle_transaccion.ToList());
        }

        public static List<OrdenDeMovimientoDeAlmacen> Convertir(List<Transaccion> transaciones)
        {
            List<OrdenDeMovimientoDeAlmacen> ordenes = new List<OrdenDeMovimientoDeAlmacen>();
            foreach (var transaccion in transaciones)
            {
                ordenes.Add(new OrdenDeMovimientoDeAlmacen(transaccion));
            }
            return ordenes;
        } 

        public bool? SeMovioMercaderiaTotalMente()
        {
            var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroEstadoEntregaMercaderia);
            return parametro != null ? (bool?)parametro.valor.Equals("1") : null;
        }
    }
}

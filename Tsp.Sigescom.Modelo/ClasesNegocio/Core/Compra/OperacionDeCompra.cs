using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class OperacionDeCompra: OperacionGenericaNivel3
    {
        static int[] idsTipoSDeComprobantesAnulables = { MaestroSettings.Default.IdDetalleMaestroComprobanteFactura, MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta };
        public OperacionDeCompra()
        {

        }
        public OperacionDeCompra(Transaccion transaccion) : base(transaccion)
        {

        }
        /// <summary>
        /// Se refiere al tipo de operacion de compra y a que tipo de venta se destina gravada no gravada o ambas
        /// </summary>
        /// <returns></returns>
        public TipoOperacionCompra TipoDeOperacionDeCompra()
        {
            var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroTipoCompra);
            return parametro != null ? (TipoOperacionCompra)System.Convert.ToInt32(parametro.valor) : TipoOperacionCompra.Ninguno;
        }
        //public bool PagarInicialAlConfirmar()
        //{
        //    var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroPagarInicialAlConfirmar);
        //    return parametro != null ? parametro.valor == "1" ? true: false : false;
        //}

        public ModoPago ModoDePago()
        {
            var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroModoDePago);
            return (ModoPago)System.Convert.ToInt32(parametro.valor);
        }

        public ModoOperacionEnum TipoDeCompra()
        {
            var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroModoOperacionCompra);
            return parametro != null ? (ModoOperacionEnum)System.Convert.ToInt32(parametro.valor) : ModoOperacionEnum.Ninguno;
        }

        public bool? SeEntregoMercaderiaTotalMente()
        {
            var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroEstadoEntregaMercaderia);
            return parametro != null ? (bool?)parametro.valor.Equals("1") : false;
        }

        public bool? SeGeneroOrdenDeAlmacenTotalMente()
        {
            var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroEstadoGeneracionOrdenDeAlmacen);
            return parametro != null ? (bool?)parametro.valor.Equals("1") : false;
        }

        public int IdTipoOperacionCompra
        {
            get { return this.transaccion.id_tipo_transaccion; }
        }
        public Compra Compra()
        {
            return new Compra(this.transaccion.Transaccion2);
        }
        public DateTime FechaOrden()
        {
            return this.transaccion.fecha_inicio;
        }
        //public string NombreUsuario()
        //{
        //    var estado = this.transaccion.Estado_transaccion.SingleOrDefault(est => est.id_estado == TransaccionSettings.Default.IdDetalleMaestroEstadoEnRevision);
        //    int index = estado.Actor_negocio.Actor.correo.IndexOf("@");
        //    return estado != null ? index > -1 ? estado.Actor_negocio.Actor.correo.Substring(0, index) : null : null;
        //}

        public OperacionDeCompra OperacionDeReferencia()
        {
            return this.transaccion.Transaccion3 != null ? new OperacionDeCompra(this.transaccion.Transaccion3) : null;
        }

        public DetalleDeOperacion DetalleIGV()
        {
            int cuenta = this.transaccion.Detalle_transaccion.Count(dst => dst.id_concepto_negocio == ConceptoSettings.Default.IdConceptoNegocioIGV);
            switch (cuenta)
            {
                case 1: return new DetalleDeOperacion(this.transaccion.Detalle_transaccion.Single(dst => dst.id_concepto_negocio == ConceptoSettings.Default.IdConceptoNegocioIGV));
                default: return null;
            }


        }
                
        public List<DetalleDeOperacion> Detalles()
        {
            return DetalleDeOperacion.Convert(this.transaccion.Detalle_transaccion.ToList());
        }

        public List<Cuota> Cuotas()
        {
            var cuotas = this.transaccion.Cuota.ToList();
            return cuotas;
        }

        public long IdCompra
        {
            get { return (long)this.transaccion.id_transaccion_padre; }
        }

        public int IdTipoComprobante
        {
            get { return this.transaccion.Comprobante.Detalle_maestro.id; }
        }

        public Empleado Empleado()
        {
            return new Empleado(this.transaccion.Actor_negocio);
        }
        public Proveedor Proveedor()
        {
            return new Proveedor(this.transaccion.Actor_negocio1);
        }

        public bool hayPagos()
        {
            return this.transaccion.Transaccion2.Transaccion1.Any(st => st.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionPagoFacturasProveedores);
        }
        public decimal Saldo()
        {
            var pago = hayPagos() ? this.transaccion.Transaccion2.Transaccion1.Where(st => st.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionPagoFacturasProveedores).Sum(st => st.importe_total) : 0;
            return this.transaccion.importe_total - pago;
        }

        public bool esAnulableConNotaInterna()
        {
            if (this.IdTipoTransaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenDeCompra)
            {
                return this.IdEstadoActual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado ? true : false;
            }

            return false;
            
        }

        public bool esAnulableConNotaDeCredito()
        {
            return (this.IdTipoTransaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenDeCompra
            && idsTipoSDeComprobantesAnulables.Contains(this.IdTipoComprobante)
            && (this.IdEstadoActual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado));
        }

        public bool esOrdenDeCompra()
        {
            return this.IdTipoTransaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenDeCompra ? true : false;
        }

        public string Observacion()
        {
            return this.transaccion.comentario;
        }

        public String DetalleUnificado()
        {
            var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroDetalleUnificado);
            return parametro != null ? Convert.ToString(parametro.valor) : "";
        }

        public bool AplicaLeyDeAmazonia
        {
            get
            {
                return (TransaccionSettings.Default.AplicaLeyAmazonia && Igv() <= 0);
            }
        }

        public decimal ImporteTotalOperacionExonerada
        {
            get
            {
                return (Igv() <= 0 ? ValorDeVenta : 0);
            }
        }

        public decimal ImporteTotalOperacionInafecta
        {
            get
            {
                return 0;
            }
        }

        public decimal BaseImponibleOperacionGravada
        {
            get
            {
                return (Igv() > 0 ? ValorDeVenta : 0);
            }
        }

        public bool EsInvalidada
        {
            get
            {
                return (this.IdEstadoActual == MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado || (this.EstadoAnteriorAlActual != null && this.EstadoAnteriorAlActual.id == MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado));
            }
        }


        public String CodigoSunatDeTransaccion()
        {
            var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroCodigoTransaccionSunat);
            return parametro != null ? Convert.ToString(parametro.valor) : "";
        }

        public static List<OperacionDeCompra> Convert_(List<Transaccion> transaciones)
        {
            List<OperacionDeCompra> ordenes = new List<OperacionDeCompra>();
            foreach (var transaccion in transaciones)
            {
                ordenes.Add(new OperacionDeCompra(transaccion));
            }
            return ordenes;
        }
    }
}

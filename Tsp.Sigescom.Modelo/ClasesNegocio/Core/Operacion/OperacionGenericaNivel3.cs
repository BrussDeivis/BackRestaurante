
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Interfaces.Entidades;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class OperacionGenericaNivel3 : IOperacionNegocio
    {
        protected Transaccion transaccion;

        public OperacionGenericaNivel3()
        {
        }
        public OperacionGenericaNivel3(Transaccion transaccion)
        {
            this.transaccion = transaccion;
        }
        public List<AccionOperativa> ObtenerAccionesPosibles()
        {
            return AccionOperativa.convert(EstadoActual().Accion_por_estado.Where(ape => ape.id_tipo_transaccion == this.transaccion.id_tipo_transaccion).Select(ape => ape.Detalle_maestro1).ToList());
        }
        public int IdMoneda
        {
            get { return this.transaccion.id_moneda; }
        }
        public string CodigoMoneda()
        {
            return this.transaccion.Detalle_maestro1.codigo;
        }
        public DetalleGenerico Moneda()
        {
            return new DetalleGenerico(this.transaccion.Detalle_maestro1);
        }
        public String MonedaPlural()
        {
            return "Soles";
        }
        public decimal TipoDeCambio
        {
            get { return this.transaccion.tipo_cambio; }
        }
        public int IdTipoTransaccion
        {
            get { return this.transaccion.id_tipo_transaccion; }
        }
        public TipoDeTransaccion TipoTransaccion()
        {
            return new TipoDeTransaccion(this.transaccion.Tipo_transaccion);
        }
        public TipoDeTransaccion TipoTransaccionSuperior()
        {
            return new TipoDeTransaccion(this.transaccion.Transaccion2.Tipo_transaccion);
        }
        public long Id
        {
            get { return this.transaccion.id; }
        }
        public int IdUnidadDeNegocio
        {
            get { return this.transaccion.id_unidad_negocio; }
        }
        public decimal ValorDeVenta
        {
            get { return this.Total - this.Igv() - this.Icbper(); }
        }
        public decimal Igv()
        {
            return (decimal)this.transaccion.Detalle_transaccion.Sum(dt => dt.igv);
        }
        public decimal Isc()
        {
            return (decimal)this.transaccion.Detalle_transaccion.Sum(dt => dt.isc);
        }
        public ActorComercial Tercero()
        {
            return new ActorComercial(this.transaccion.Actor_negocio1);
        }
        /// <summary>
        /// sucursa. area propia donde se realiza o gestinoa la operacion.
        /// </summary>
        /// <returns></returns>
        public CentroDeAtencionExtendido CentroDeAtencion()
        {
            return new CentroDeAtencionExtendido(this.transaccion.Actor_negocio2);
        }
        public ComprobanteDeNegocio Comprobante()
        {
            return new ComprobanteDeNegocio(this.transaccion.Comprobante);
        }
        public Empleado Empleado()
        {
            return new Empleado(this.transaccion.Actor_negocio);
        }
        public Transaccion Transaccion()
        {
            return this.transaccion;
        }
        public List<Detalle_transaccion> DetalleTransaccion()
        {
            return this.transaccion.Detalle_transaccion.ToList();
        }
        public string Codigo
        {
            get { return this.transaccion.codigo; }
        }
        public DateTime FechaDeRegistro
        {
            get { return this.transaccion.fecha_registro_sistema; }
        }
        public DateTime FechaContabilizacion
        {
            get { return this.transaccion.fecha_registro_contable; }
        }
        public DateTime FechaEmision
        {
            get { return this.transaccion.fecha_inicio; }
        }
        public DateTime FechaVencimiento
        {
            get { return this.transaccion.fecha_fin; }
        }
        public string Comentario
        {
            get { return this.transaccion.comentario; }
        }
        public string Informacion
        {
            get { return this.transaccion.informacion; }
        }
        public long? IdTransaccionPadre
        {
            get { return this.transaccion.id_transaccion_padre; }
        }
        public string CodigoEstadoActual()
        {
            return this.EstadoActual().codigo; 
        }
        public Detalle_maestro EstadoAnteriorAlActual
        {
            get { return this.transaccion.EstadoAnteriorAlActual; }
        }
        public Detalle_maestro EstadoActual()
        {
            return this.transaccion.Detalle_maestro;
        }
        public bool EstaTransmitido()
        {
            return this.transaccion.EstaTransmitido();
        }
        public int IdEstadoActual
        {
            get { return this.transaccion.id_estado_actual ?? 0; }
        }
        public decimal Total
        {
            get { return this.transaccion.importe_total; }
        }
        public decimal Descuento()
        {
            return (decimal)this.transaccion.Detalle_transaccion.Sum(dt => dt.descuento);
        }

        public decimal Flete
        {
            get { return this.transaccion.Detalle_transaccion.SingleOrDefault(dt => dt.id_concepto_negocio == ConceptoSettings.Default.IdConceptoNegocioFlete) == null ? 0 : this.transaccion.Detalle_transaccion.SingleOrDefault(dt => dt.id_concepto_negocio == ConceptoSettings.Default.IdConceptoNegocioFlete).total; }
        }

        public decimal Icbper()
        {
            var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroIcbper);
            return parametro != null ? Convert.ToDecimal(parametro.valor) : 0;
        }

        public int NumeroBolsasDePlastico()
        {
            var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroNumeroBolsasDePlastico);
            return parametro != null ? Convert.ToInt32(parametro.valor) : 0;
        }

        public decimal ValorIcbper()
        {
            return NumeroBolsasDePlastico() != 0 ? Icbper() / NumeroBolsasDePlastico() : 0;
        }

        public static List<OperacionGenericaNivel3> Convertir(List<OperacionDeVenta> operaciones)
        {
            List<OperacionGenericaNivel3> operacionesGenericas = new List<OperacionGenericaNivel3>();
            foreach (var item in operaciones)
            {
                operacionesGenericas.Add(new OperacionGenericaNivel3(item.transaccion));
            }

            return operacionesGenericas;
        }

        public static List<OperacionGenericaNivel3> Convertir(List<MovimientoEconomico> cobranzas)
        {
            List<OperacionGenericaNivel3> operacionesGenericas = new List<OperacionGenericaNivel3>();
            foreach (var item in cobranzas)
            {
                operacionesGenericas.Add(new OperacionGenericaNivel3(item.transaccion));
            }

            return operacionesGenericas;
        }

    }
}

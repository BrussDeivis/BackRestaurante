using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class OperacionDeGasto : OperacionGenericaNivel3
    {
        public OperacionDeGasto()
        {

        }
        protected int[] idsConceptoNegocioSecundarios = { ConceptoSettings.Default.IdConceptoNegocioIGV };

        public OperacionDeGasto(Transaccion transaccion) : base(transaccion)
        {

        }


        public int IdTipoOperacionGasto
        {
            get { return this.transaccion.id_tipo_transaccion; }
        }

        public Gasto Gasto()
        {
            return new Gasto(this.transaccion.Transaccion2);
        }

        //public string NombreUsuario()
        //{
        //    var estado = this.transaccion.Estado_transaccion.SingleOrDefault(est => est.id_estado == TransaccionSettings.Default.IdDetalleMaestroEstadoEnRevision);
        //    int index = estado.Actor_negocio.Actor.correo.IndexOf("@");
        //    return estado != null ? index > -1 ? estado.Actor_negocio.Actor.correo.Substring(0, index) : null : null;
        //}

        public OperacionDeGasto OperacionDeReferencia()
        {
            return this.transaccion.Transaccion3 != null ? new OperacionDeGasto(this.transaccion.Transaccion3) : null;
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
            return this.transaccion.Cuota.ToList();
        }

        public long IdGasto
        {
            get { return (long)this.transaccion.id_transaccion_padre; }
        }

        public int IdTipoComprobante
        {
            get { return this.transaccion.Detalle_maestro1.id_maestro; }
        }

        public Empleado empleado()
        {
            return new Empleado(this.transaccion.Actor_negocio);
        }
        public Proveedor proveedor()
        {
            return new Proveedor(this.transaccion.Actor_negocio1);
        }

        internal int obtenerIdTipoTransaccionPago()
        {
            int idTipoTransaccionPago = 0;
            if (transaccion.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenGastoServiciosTerceros)
            {
                idTipoTransaccionPago = TransaccionSettings.Default.IdTipoTransaccionPagoGastoServiciosTerceros;
            }
            else if (transaccion.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenOtrosGastosGestion)
            {
                idTipoTransaccionPago = TransaccionSettings.Default.IdTipoTransaccionPagoOtrosGastosGestion;
            }
            else if (transaccion.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenGastoFinanciero)
            {
                idTipoTransaccionPago = TransaccionSettings.Default.IdTipoTransaccionPagoGastoFinanciero;
            }
            else if (transaccion.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenGastoPorTributos)
            {
                idTipoTransaccionPago = TransaccionSettings.Default.IdTipoTransaccionPagoGastoPorTributos;
            }
            return idTipoTransaccionPago;
        }

        public bool hayPagos()
        {
            
            return this.transaccion.Transaccion2.Transaccion1.Any(st => st.id_tipo_transaccion == obtenerIdTipoTransaccionPago());
        }
        public decimal Saldo()
        {
            var pago = hayPagos() ? this.transaccion.Transaccion2.Transaccion1.Where(st => st.id_tipo_transaccion == obtenerIdTipoTransaccionPago()).Sum(st => st.importe_total) : 0;
            return this.transaccion.importe_total - pago;
        }

        public static List<OperacionDeGasto> Convertir(List<Transaccion> transaciones)
        {
            List<OperacionDeGasto> ordenes = new List<OperacionDeGasto>();
            foreach (var transaccion in transaciones)
            {
                ordenes.Add(new OperacionDeGasto(transaccion));
            }
            return ordenes;
        }
    }
}
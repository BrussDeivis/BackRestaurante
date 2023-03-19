using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class Gasto
    {
        private readonly Transaccion transaccion;

        public Gasto (Transaccion _transaccion)
        {
            this.transaccion = _transaccion;
        }
        public Operacion OperacionGenerica()
        {
            return new Operacion(this.transaccion.Transaccion2);
        }
        public OrdenDeGasto OrdenDeGasto()
        {
            return new OrdenDeGasto(this.transaccion.Transaccion1.Single(t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenGastoServiciosTerceros || t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenOtrosGastosGestion ||
            t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenGastoFinanciero || t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenGastoPorTributos));
        }
        public List<MovimientoEconomico> ObtenerPagos()
        {
            return MovimientoEconomico.Convert(this.transaccion.Transaccion1.Where(t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionPagoGastoServiciosTerceros || t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionPagoOtrosGastosGestion || 
            t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionPagoGastoFinanciero || t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionPagoGastoPorTributos).ToList());
        }
        public long Id
        {
            get { return this.transaccion.id; }
        }
        public DateTime FechaRegistro
        {
            get { return this.transaccion.fecha_registro_sistema; }
        }
        public decimal ImporteTotal
        {
            get { return this.transaccion.importe_total; }
        }
        public DateTime FechaVencimiento
        {
            get { return this.transaccion.fecha_fin; }
        }
        public static List<Gasto> Convert(List<Transaccion> transaccion)
        {
            List<Gasto> Resultado = new List<Gasto>();
            foreach (var item in transaccion)
            {
                Resultado.Add(new Gasto(item));
            }
            return Resultado;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class OrdenDeGasto : OperacionDeGasto
    {
        public OrdenDeGasto()
        {
        }
        public OrdenDeGasto(Transaccion _transaccion)
        {
            this.transaccion = _transaccion;
        }
        public DateTime FechaOrden()
        {
            return this.transaccion.fecha_inicio;
        }
        //public bool hayReferenciasAOperacion()
        //{
        //    return this.subTransaccion.Sub_transaccion1.Any(st => st.id_tipo_transaccion == TransaccionSettings.Default.idTipoSubTransaccionReferenciaOrdenDeCompraServicioPrestadoA);
        //}
        //public List<ReferenciaOrdenDeCompraServicioPrestadoA> referenciasAOperacion()
        //{
        //    return ReferenciaOrdenDeCompraServicioPrestadoA.convert(this.subTransaccion.Sub_transaccion1.Where(st => st.id_tipo_transaccion == TransaccionSettings.Default.idTipoSubTransaccionReferenciaOrdenDeCompraServicioPrestadoA).ToList());
        //}
        public static List<OrdenDeGasto> Convert_(List<Transaccion> transaciones)
        {
            List<OrdenDeGasto> ordenes = new List<OrdenDeGasto>();
            foreach (var transaccion in transaciones)
            {
                ordenes.Add(new OrdenDeGasto(transaccion));
            }
            return ordenes;
        }
        public ModoPago ModoDePago()
        {
            var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroModoDePago);
            return (ModoPago)Convert.ToInt32(parametro.valor);
        }
    }
}

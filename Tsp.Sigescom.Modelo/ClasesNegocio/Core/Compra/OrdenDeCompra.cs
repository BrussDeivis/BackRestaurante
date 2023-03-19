using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class OrdenDeCompra : OperacionDeCompra
    {
        public OrdenDeCompra()
        {

        }
        public OrdenDeCompra(Transaccion _transaccion)
        {
            this.transaccion = _transaccion;
        }

        public List<OrdenDeMovimientoDeAlmacen> ObtenerOrdenesDeAlmacen()
        {
            return OrdenDeMovimientoDeAlmacen.Convertir(this.transaccion.Transaccion11.Where(t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAlmacen).ToList());
        }

        //public bool hayReferenciasAOperacion()
        //{
        //    return this.subTransaccion.Sub_transaccion1.Any(st => st.id_tipo_transaccion == TransaccionSettings.Default.idTipoSubTransaccionReferenciaOrdenDeCompraServicioPrestadoA);
        //}

        //public List<ReferenciaOrdenDeCompraServicioPrestadoA> referenciasAOperacion()
        //{
        //    return ReferenciaOrdenDeCompraServicioPrestadoA.convert(this.subTransaccion.Sub_transaccion1.Where(st => st.id_tipo_transaccion == TransaccionSettings.Default.idTipoSubTransaccionReferenciaOrdenDeCompraServicioPrestadoA).ToList());
        //}
        //public bool PagarInicialAlConfirmar()
        //{
        //    var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroPagarInicialAlConfirmar);
        //    return parametro != null ? parametro.Equals("1") : false;
        //}

        //public bool PagarInicialAlConfirmar()
        //{
        //    var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroPagarInicialAlConfirmar);
        //    return parametro != null ? parametro.Equals("1") : false;
        //}



        public bool TieneGuiaDeRemision()
        {
            var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroTieneGuiaDeRemision);
            return parametro != null ? parametro.valor.Equals("1") : false;
        }

        public static List<OrdenDeCompra> Convert_(List<Transaccion> transaciones)
        {
            List<OrdenDeCompra> ordenes = new List<OrdenDeCompra>();
            foreach (var transaccion in transaciones)
            {
                ordenes.Add(new OrdenDeCompra(transaccion));
            }
            return ordenes;
        }

    }
}

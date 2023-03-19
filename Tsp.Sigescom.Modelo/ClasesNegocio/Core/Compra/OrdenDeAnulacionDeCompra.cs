using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class OrdenDeAnulacionDeCompra : OperacionDeCompra
    {
        public OrdenDeAnulacionDeCompra()
        {

        }
        public OrdenDeAnulacionDeCompra(Transaccion _transaccion)
        {
            this.transaccion = _transaccion;
        }

      

        //public bool hayReferenciasAOperacion()
        //{
        //    return this.subTransaccion.Sub_transaccion1.Any(st => st.id_tipo_transaccion == TransaccionSettings.Default.idTipoSubTransaccionReferenciaOrdenDeCompraServicioPrestadoA);
        //}

        //public List<ReferenciaOrdenDeCompraServicioPrestadoA> referenciasAOperacion()
        //{
        //    return ReferenciaOrdenDeCompraServicioPrestadoA.convert(this.subTransaccion.Sub_transaccion1.Where(st => st.id_tipo_transaccion == TransaccionSettings.Default.idTipoSubTransaccionReferenciaOrdenDeCompraServicioPrestadoA).ToList());
        //}


        public static List<OrdenDeAnulacionDeCompra> Convert_(List<Transaccion> transaciones)
        {
            List<OrdenDeAnulacionDeCompra> ordenes = new List<OrdenDeAnulacionDeCompra>();
            foreach (var transaccion in transaciones)
            {
                ordenes.Add(new OrdenDeAnulacionDeCompra(transaccion));
            }
            return ordenes;
        }

    }
}

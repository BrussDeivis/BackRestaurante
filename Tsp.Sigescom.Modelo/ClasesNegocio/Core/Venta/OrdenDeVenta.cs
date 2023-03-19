using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class OrdenDeVenta : OperacionDeVenta
    {
        public OrdenDeVenta()
        {

        }
        public OrdenDeVenta(Transaccion _transaccion)
        {
            this.transaccion = _transaccion;
        }
       
        public static List<OrdenDeVenta> Convert_(List<Transaccion> transacciones)
        {
            List<OrdenDeVenta> ordenesDeVenta = new List<OrdenDeVenta>();
            foreach (var transaccion in transacciones)
            {
                ordenesDeVenta.Add(new OrdenDeVenta(transaccion));
            }
            return ordenesDeVenta;
        }

       

       
        
        public bool TieneGuiaDeRemision()
        {
            var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroTieneGuiaDeRemision);
            return parametro != null ? parametro.valor.Equals("1") : false;
        }

       

        public List<MovimientoDeAlmacen> GuiaDeRemision()
        {
            return null; 
        }

        /// <summary>
        /// Metodo para obtener el moviemiento de almacen de las ventas por cobros de vendedor
        /// </summary>
        /// <returns></returns>
        public OperacionDeVenta OperacionDeAlmacen()
        {
            return this.transaccion.Transaccion11 != null ? new OperacionDeVenta(this.transaccion.Transaccion11.FirstOrDefault()) : null;
        }


    }
}

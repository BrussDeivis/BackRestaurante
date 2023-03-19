using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Compras;

namespace Tsp.Sigescom.AccesoDatos.Compras

{

    public partial class ConsultaCompras_Datos: IConsultaCompras_Repositorio
    {

        public IEnumerable<PrecioComercial> ObtenerValorUnitarioDePrimeraOrdenDeCompraConPrecioMayorACero(int[] idConceptos)
        {
            SigescomEntities _db = new SigescomEntities();

            var partialResult = _db.Detalle_transaccion.Where(dt => idConceptos.Contains(dt.id_concepto_negocio) && dt.Transaccion.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenDeCompra).GroupBy(dt => new { dt.id_concepto_negocio }).Select(dt => new {
                IdConcepto = dt.Key.id_concepto_negocio,
                Detalle = dt.FirstOrDefault(d => d.precio_unitario > 0)
            }).ToList();

            var result= partialResult.Select(r=> new PrecioComercial() {
                IdConcepto= r.IdConcepto,
                Precio = TransaccionSettings.Default.AplicaLeyAmazonia? (r.Detalle!=null? r.Detalle.precio_unitario:0) : (r.Detalle != null? (r.Detalle.total - r.Detalle.igv) / r.Detalle.cantidad : 0)
            });
            
            return result;
        }

        public decimal ObtenerValorUnitarioDePrimeraOrdenDeCompra(int idConcepto)
        {
            SigescomEntities _db = new SigescomEntities();
            var result = _db.Detalle_transaccion.Where(dt => dt.id_concepto_negocio == idConcepto && dt.Transaccion.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenDeCompra).Select(dt => TransaccionSettings.Default.AplicaLeyAmazonia ? dt.precio_unitario:(dt.total-dt.igv)/dt.cantidad).FirstOrDefault();
            return result;
        }

    }
}
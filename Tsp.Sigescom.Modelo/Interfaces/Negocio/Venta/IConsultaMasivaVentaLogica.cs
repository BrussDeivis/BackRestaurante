using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;

namespace Tsp.Sigescom.Modelo.Interfaces.Negocio.Venta
{
    public interface IConsultaMasivaVentaLogica
    {
        List<TransaccionPorSerieDeComprobanteYCategoria> ObtenerComprobanteVentaPorSerieYCategoriaConfirmado(int[] idsPuntosDeVentas, DateTime fechaDesde, DateTime fechaHasta);
        List<TransaccionPorSerieDeComprobanteYCategoria> ObtenerComprobanteVentaPorSerieYCategoriaInvalidado(int[] idsPuntosDeVentas, DateTime fechaDesde, DateTime fechaHasta);
        List<TransaccionPorSerieDeComprobanteYConcepto> ObtenerComprobanteVentaPorSerieYConceptoConfirmado(int[] idsSeries, DateTime fechaDesde, DateTime fechaHasta);
        List<TransaccionPorSerieDeComprobanteYConcepto> ObtenerComprobanteVentaPorSerieYConceptoInvalidado(int[] idsSeries, DateTime fechaDesde, DateTime fechaHasta);
    }

}

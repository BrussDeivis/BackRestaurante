using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Transacciones;
using Tsp.Sigescom.Modelo.Interfaces.Negocio.Venta;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;

namespace Tsp.Sigescom.Logica.Core.Venta
{
    public class ConsultaMasivaVentaLogica : IConsultaMasivaVentaLogica
    {
        private readonly IConsultaTransaccion_Repositorio _consultaTransaccionRepositorio;

        public ConsultaMasivaVentaLogica(IConsultaTransaccion_Repositorio consultaTransaccionRepositorio)
        {
            this._consultaTransaccionRepositorio = consultaTransaccionRepositorio;
        }
        public List<TransaccionPorSerieDeComprobanteYCategoria> ObtenerComprobanteVentaPorSerieYCategoriaConfirmado(int[] idsPuntosDeVentas, DateTime fechaDesde, DateTime fechaHasta)
        {
            var resultado = _consultaTransaccionRepositorio.ObtenerTransaccionesPorSerieDeComprobanteYCategoria(idsPuntosDeVentas, Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasExceptoInvalidaciones, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaDesde, fechaHasta).ToList();
            foreach (var item in resultado.Where(r => r.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito))
            {
                item.Cantidad *= -1;
                item.Importe *= -1;
            }

            return resultado;
        }
        public List<TransaccionPorSerieDeComprobanteYCategoria> ObtenerComprobanteVentaPorSerieYCategoriaInvalidado(int[] idsPuntosDeVentas, DateTime fechaDesde, DateTime fechaHasta)
        {
            var resultado = _consultaTransaccionRepositorio.ObtenerTransaccionesPorSerieDeComprobanteYCategoria(idsPuntosDeVentas, Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasExceptoInvalidaciones, MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado, fechaDesde, fechaHasta).ToList();
            return resultado;
        }
        public List<TransaccionPorSerieDeComprobanteYConcepto> ObtenerComprobanteVentaPorSerieYConceptoConfirmado(int[] idsSeries, DateTime fechaDesde, DateTime fechaHasta)
        {
            var resultado = _consultaTransaccionRepositorio.ObtenerTransaccionesPorSerieDeComprobanteYConcepto(idsSeries, Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasExceptoInvalidaciones, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, fechaDesde, fechaHasta).ToList();
            foreach (var item in resultado.Where(r => r.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito))
            {
                item.Cantidad *= -1;
                item.Importe *= -1;
            }

            return resultado;
        }
        public List<TransaccionPorSerieDeComprobanteYConcepto> ObtenerComprobanteVentaPorSerieYConceptoInvalidado(int[] idsSeries, DateTime fechaDesde, DateTime fechaHasta)
        {
            var resultado = _consultaTransaccionRepositorio.ObtenerTransaccionesPorSerieDeComprobanteYConcepto(idsSeries, Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasExceptoInvalidaciones, MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado, fechaDesde, fechaHasta).ToList();
            return resultado;
        }
    }
}

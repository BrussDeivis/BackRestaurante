using System;
using System.Collections.Generic;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;

namespace Tsp.Sigescom.Modelo.Interfaces.Datos.Transacciones

{
    public interface IConsultaTransaccion_Repositorio
    {
        DateTime? ObtenerFechaPrimeraTransaccion(int idAlmacen);
        IEnumerable<Transaccion> ObtenerTransaccionesSegunTipoYConTipoTransaccionPadreDiferenteA(int idTipoTransaccion, int idTipoTransaccionPadre);
        IEnumerable<Transaccion> ObtenerTransaccionesPadre(long[] idsTransaccionesHijas);
        IEnumerable<TransaccionPorSerieDeComprobanteYCategoria> ObtenerTransaccionesPorSerieDeComprobanteYCategoria(int[] idsPuntosDeVentas, int[] idsTiposTransaccion, int idEstadoActual, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<TransaccionPorSerieDeComprobanteYConcepto> ObtenerTransaccionesPorSerieDeComprobanteYConcepto(int[] idsSeries, int[] idsTiposTransaccion, int idEstadoActual, DateTime fechaDesde, DateTime fechaHasta);
    }
}

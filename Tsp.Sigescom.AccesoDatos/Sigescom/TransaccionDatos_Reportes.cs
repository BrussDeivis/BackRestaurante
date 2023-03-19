using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.AccesoDatos.Generico;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Concepto;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;

namespace Tsp.Sigescom.AccesoDatos
{
    public partial class TransaccionDatos : RepositorioBase, ITransaccionRepositorio
    {
        public IEnumerable<Resumen_Transaccion_Por_Serie_y_Concepto_Negocio> ObtenerResumenTransaccionesSerieYConcepto(int idTipoTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                var Resumen = _db.Transaccion.Where(sbt => sbt.fecha_inicio >= fechaDesde && sbt.fecha_inicio <= fechaHasta && sbt.id_tipo_transaccion == idTipoTransaccion &&
                sbt.id_estado_actual == idUltimoEstado).SelectMany(t => t.Detalle_transaccion).
              GroupBy(dt => new
              {
                  dt.Transaccion.Comprobante.id_serie_comprobante,
                  dt.Transaccion.Comprobante.numero_serie,
                  dt.id_concepto_negocio,
                  dt.Concepto_negocio.nombre,
                  dt.Concepto_negocio.codigo_barra,
                  dt.Transaccion.Comprobante.Detalle_maestro.valor, //nombre corto de comprobante
                  precioUnitario = dt.precio_unitario

              }).Select(dt => new Resumen_Transaccion_Por_Serie_y_Concepto_Negocio()
              {
                  IdSerie = dt.Key.id_serie_comprobante.Value,
                  Serie = dt.Key.numero_serie,
                  CodigoBarra = dt.Key.codigo_barra,
                  IdConceptoNegocio = dt.Key.id_concepto_negocio,
                  ConceptoNegocio = dt.Key.nombre,
                  NombreCortoDocumento = dt.Key.valor,
                  Cantidad = dt.Sum(c => c.cantidad),
                  Importe = dt.Sum(i => i.total),
              });
                return Resumen.OrderBy(r => r.CodigoBarra != null ? r.CodigoBarra : r.ConceptoNegocio);

            }
            catch (Exception e)
            {
                throw e;
            }


        }

        public IEnumerable<Resumen_Transaccion_Por_Serie_y_ConceptoBasico> ObtenerResumenTransaccionesPorSerieYConeptoBasico(int idTipoTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                var Resumen = _db.Transaccion.Where(sbt => sbt.fecha_inicio >= fechaDesde && sbt.fecha_inicio <= fechaHasta && sbt.id_tipo_transaccion == idTipoTransaccion &&
                sbt.id_estado_actual == idUltimoEstado).SelectMany(t => t.Detalle_transaccion).
              GroupBy(dt => new
              {
                  dt.Transaccion.Comprobante.id_serie_comprobante,
                  dt.Transaccion.Comprobante.numero_serie,

                  dt.Transaccion.Comprobante.Detalle_maestro.valor,
                  IdConceptoBasico = dt.Concepto_negocio.id_concepto_basico,
                  NombreConceptoBasico = dt.Concepto_negocio.Detalle_maestro4.nombre
              }).Select(dt => new Resumen_Transaccion_Por_Serie_y_ConceptoBasico()
              {
                  IdSerie = dt.Key.id_serie_comprobante.Value,
                  Serie = dt.Key.numero_serie,
                  NombreCortoDocumento = dt.Key.valor,
                  ConceptoBasico = dt.Key.NombreConceptoBasico,
                  Cantidad = dt.Sum(c => c.cantidad),
                  Importe = dt.Sum(i => i.total),
              });
                return Resumen;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Resumen_Transaccion_Consolidado> ObtenerResumenTransacciones(int[] idsTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta, int idAccionNegocio) //cod:XY7 Y77        
        {
            try
            {
                var Resumen = _db.Transaccion.Where(sbt =>
               sbt.fecha_inicio >= fechaDesde && sbt.fecha_inicio <= fechaHasta && idsTransaccion.Contains(sbt.id_tipo_transaccion) && sbt.id_estado_actual == idUltimoEstado).
               GroupBy(t => new
               {
                   t.Tipo_transaccion.nombre,
                   //idEntidad = dt.Transaccion.id_actor_negocio_interno,
                   idTransaccion = t.id_tipo_transaccion,
                   // nombreTransaccion=  dt.Transaccion.Tipo_transaccion.nombre
                   entidadInterna = t.Actor_negocio2.Actor.primer_nombre,
                   entradaSalida = t.Tipo_transaccion.Accion_de_negocio_por_tipo_transaccion.FirstOrDefault(tt => tt.id_accion_de_negocio == idAccionNegocio).valor
               }).Select(dt => new Resumen_Transaccion_Consolidado()
               {
                   //IdTipoTransaccion = dt.Key.id_tipo_transaccion,
                   //IdEntidad = dt.Key.idEntidad,
                   EntidadInterna = dt.Key.entidadInterna,
                   IdTipoTransaccion = dt.Key.idTransaccion,
                   NombreTransaccion = dt.Key.nombre,
                   EntradaSalida = dt.Key.entradaSalida,
                   Importe = dt.Sum(i => i.importe_total)
               });
                return Resumen;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #region REPORTE DE GASTOS
        public IEnumerable<Resumen_Transaccion_Gasto_Por_Concepto> ObtenerResumenTransaccionesDeGastosPorConcepto(int idTransaccion, int idEstado, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                var Resumen = _db.Transaccion.Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && t.id_tipo_transaccion == idTransaccion && t.id_estado_actual == idEstado).SelectMany(t => t.Detalle_transaccion)
                 .Select(dt => new Resumen_Transaccion_Gasto_Por_Concepto()
                 {
                     Fecha = dt.Transaccion.fecha_inicio,
                     Serie = dt.Transaccion.Comprobante.numero_serie,
                     Numero = dt.Transaccion.Comprobante.numero,
                     IdConceptoNegocio = dt.id_concepto_negocio,
                     ConceptoNegocio = dt.Concepto_negocio.nombre,
                     Detalle = dt.detalle,
                     Cantidad = dt.cantidad,
                     Importe = dt.total
                 });
                return Resumen;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener el reporte de gastos", e);
            }
        }

        public IEnumerable<Resumen_Transaccion_Gasto_Por_Concepto> ObtenerResumenTransaccionesDeGastosPorConcepto(int idTransaccion, int idEstado, int[] idsCentroAtencion, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                var Resumen = _db.Transaccion.Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && t.id_tipo_transaccion == idTransaccion && idsCentroAtencion.Contains(t.id_actor_negocio_interno) && t.id_estado_actual == idEstado).SelectMany(t => t.Detalle_transaccion)
                  .Select(dt => new Resumen_Transaccion_Gasto_Por_Concepto()
                  {
                      Fecha = dt.Transaccion.fecha_inicio,
                      Serie = dt.Transaccion.Comprobante.numero_serie,
                      Numero = dt.Transaccion.Comprobante.numero,
                      IdConceptoNegocio = dt.id_concepto_negocio,
                      ConceptoNegocio = dt.Concepto_negocio.nombre,
                      Cantidad = dt.cantidad,
                      Importe = dt.total
                  });
                return Resumen;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener el reporte de gastos", e);
            }
        }
        #endregion
        public IEnumerable<TransaccionAgrupadoPorSerieConNumeracionInicioFin> ObtenerTransaccionesAgrupadasPorSerie(int[] idsPuntosDeVentas, int[] idsTiposTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta)//a10  CONFIRMADO
        {
            try
            {


                var Resumen = _db.Transaccion.Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && idsTiposTransaccion.Contains(t.id_tipo_transaccion)
                && idsPuntosDeVentas.Contains(t.id_actor_negocio_interno)
                && t.id_estado_actual == idUltimoEstado)

              .GroupBy(t => new
              {
                  t.Comprobante.id_tipo_comprobante,
                  t.Comprobante.Detalle_maestro.valor,
                  t.Comprobante.numero_serie
              }).Select(t => new TransaccionAgrupadoPorSerieConNumeracionInicioFin()
              {
                  IdTipoComprobante = t.Key.id_tipo_comprobante,
                  NombreCortoDocumento = t.Key.valor,
                  Serie = t.Key.numero_serie,
                  NumeroInicial = t.Min(a => a.Comprobante.numero),
                  NumeroFinal = t.Max(a => a.Comprobante.numero),
                  ValorVenta = t.SelectMany(dt => dt.Detalle_transaccion).Sum(dt => dt.total),
                  Igv = t.SelectMany(dt => dt.Detalle_transaccion).Sum(dt => dt.igv),
                  Icbpers = t.SelectMany(pt => pt.Parametro_transaccion).Where(pt => pt.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroIcbper).Select(pt => pt.valor),
                  Importe = t.Sum(i => i.importe_total)
              });
                return Resumen;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones", e);
            }
        }

        public IEnumerable<TransaccionAgrupadoPorSerieConNumeracionConcatenada> ObtenerTransaccionesAgrupadasPorSerie_(int[] idsPuntosDeVentas, int[] idsTipoTransaccion, int idEstadoQueDebeTener, DateTime fechaDesde, DateTime fechaHasta)// INVALIDADOS
        {
            try
            {
                var Resumen = _db.Transaccion.Where(sbt => sbt.fecha_inicio >= fechaDesde && sbt.fecha_inicio <= fechaHasta && idsTipoTransaccion.Contains(sbt.id_tipo_transaccion) && idsPuntosDeVentas.Contains(sbt.id_actor_negocio_interno)
                && sbt.Estado_transaccion.Select(et => et.id_estado).Contains(idEstadoQueDebeTener)
                )
              .GroupBy(t => new
              {
                  t.Comprobante.id_serie_comprobante,
                  t.Comprobante.numero_serie,
                  t.Comprobante.Detalle_maestro.valor
              }).Select(t => new TransaccionAgrupadoPorSerieConNumeracionConcatenada()
              {
                  NombreCortoDocumento = t.Key.valor,
                  Serie = t.Key.numero_serie,
                  NumerosComprobantes = t.Select(_dt => (int)_dt.Comprobante.numero).Distinct(),
                  ImporteSinIcbper = t.SelectMany(dt => dt.Detalle_transaccion).Sum(dt => dt.total),
                  Icbpers = t.SelectMany(pt => pt.Parametro_transaccion).Where(pt => pt.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroIcbper).Select(pt => pt.valor),
              });
                return Resumen;

            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones", e);
            }
        }


        public IEnumerable<TransaccionAgrupadoPorSerieConNumeracionInicioFin> ObtenerTransaccionesAgrupadasPorSeriePorIntervaloDiario(int[] idsPuntosDeVentas, int[] idsTiposTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                TimeSpan horadesde = new TimeSpan(fechaDesde.Hour, fechaDesde.Minute, fechaDesde.Second);
                TimeSpan horahasta = new TimeSpan(fechaHasta.Hour, fechaHasta.Minute, fechaHasta.Second);

                var Resumen = _db.Transaccion.Where(sbt => sbt.fecha_inicio >= fechaDesde && sbt.fecha_inicio <= fechaHasta && idsTiposTransaccion.Contains(sbt.id_tipo_transaccion)
                && idsPuntosDeVentas.Contains(sbt.id_actor_negocio_interno)
                && sbt.id_estado_actual == idUltimoEstado
                && DbFunctions.CreateTime(sbt.fecha_inicio.Hour, sbt.fecha_inicio.Minute, sbt.fecha_inicio.Second) >= horadesde
                && DbFunctions.CreateTime(sbt.fecha_inicio.Hour, sbt.fecha_inicio.Minute, sbt.fecha_inicio.Second) <= horahasta)

              .GroupBy(t => new
              {
                  t.Comprobante.id_tipo_comprobante,
                  t.Comprobante.Detalle_maestro.valor,
                  t.Comprobante.numero_serie
              }).Select(t => new TransaccionAgrupadoPorSerieConNumeracionInicioFin()
              {
                  IdTipoComprobante = t.Key.id_tipo_comprobante,
                  NombreCortoDocumento = t.Key.valor,
                  Serie = t.Key.numero_serie,
                  NumeroInicial = t.Min(a => a.Comprobante.numero),
                  NumeroFinal = t.Max(a => a.Comprobante.numero),
                  ValorVenta = t.SelectMany(dt => dt.Detalle_transaccion).Sum(dt => dt.total),
                  Igv = t.SelectMany(dt => dt.Detalle_transaccion).Sum(dt => dt.igv),
                  Icbpers = t.SelectMany(pt => pt.Parametro_transaccion).Where(pt => pt.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroIcbper).Select(pt => pt.valor),
                  Importe = t.Sum(i => i.importe_total)
              });
                return Resumen;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones", e);
            }
        }
        public IEnumerable<TransaccionAgrupadoPorSerieConNumeracionConcatenada> ObtenerTransaccionesAgrupadasPorSeriePorIntervaloDiario_(int[] idsPuntosDeVentas, int[] idsTipoTransaccion, int idEstadoQueDebeTener, DateTime fechaDesde, DateTime fechaHasta)// INVALIDADOS
        {
            try
            {
                TimeSpan horadesde = new TimeSpan(fechaDesde.Hour, fechaDesde.Minute, fechaDesde.Second);
                TimeSpan horahasta = new TimeSpan(fechaHasta.Hour, fechaHasta.Minute, fechaHasta.Second);

                var Resumen = _db.Transaccion.Where(sbt => sbt.fecha_inicio >= fechaDesde && sbt.fecha_inicio <= fechaHasta && idsTipoTransaccion.Contains(sbt.id_tipo_transaccion) && idsPuntosDeVentas.Contains(sbt.id_actor_negocio_interno)
                && sbt.Estado_transaccion.Select(et => et.id_estado).Contains(idEstadoQueDebeTener)
                && DbFunctions.CreateTime(sbt.fecha_inicio.Hour, sbt.fecha_inicio.Minute, sbt.fecha_inicio.Second) >= horadesde
                && DbFunctions.CreateTime(sbt.fecha_inicio.Hour, sbt.fecha_inicio.Minute, sbt.fecha_inicio.Second) <= horahasta
                )
              .GroupBy(t => new
              {
                  t.Comprobante.id_serie_comprobante,
                  t.Comprobante.numero_serie,
                  t.Comprobante.Detalle_maestro.valor,
              }).Select(t => new TransaccionAgrupadoPorSerieConNumeracionConcatenada()
              {
                  NombreCortoDocumento = t.Key.valor,
                  Serie = t.Key.numero_serie,
                  NumerosComprobantes = t.Select(_dt => (int)_dt.Comprobante.numero).Distinct(),
                  ImporteSinIcbper = t.SelectMany(dt => dt.Detalle_transaccion).Sum(dt => dt.total),
                  Icbpers = t.SelectMany(pt => pt.Parametro_transaccion).Where(pt => pt.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroIcbper).Select(pt => pt.valor),

              });
                return Resumen;

            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones", e);
            }
        }


        //// Nuevo método para el reporte de Transaccion agrupado por serie, con ESTADO = Trasnmitido-->Anulado : (Anulados)
        public IEnumerable<TransaccionAgrupadoPorSerieConNumeracionConcatenada> ObtenerTransaccionesAgrupadasPorSerie(int[] idsPuntosDeVentas, int idTipoTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta)// a12 anuladas  
        {
            try
            {
                var Resumen = _db.Transaccion.Where(sbt => sbt.fecha_inicio >= fechaDesde && sbt.fecha_inicio <= fechaHasta && sbt.id_tipo_transaccion == idTipoTransaccion && idsPuntosDeVentas.Contains(sbt.id_actor_negocio_interno)
                 && sbt.id_estado_actual == idUltimoEstado)

              .GroupBy(t => new
              {
                  t.Comprobante.id_serie_comprobante,
                  t.Comprobante.numero_serie,
                  t.Comprobante.Detalle_maestro.valor
              }).Select(t => new TransaccionAgrupadoPorSerieConNumeracionConcatenada()
              {
                  NombreCortoDocumento = t.Key.valor,
                  Serie = t.Key.numero_serie,
                  NumerosComprobantes = t.Select(_dt => (int)_dt.Comprobante.numero),
                  //string listOfPersons = string.Join(",", persons.Select(p => p.FirstName));
                  ImporteSinIcbper = t.SelectMany(dt => dt.Detalle_transaccion).Sum(dt => dt.total),
                  Icbpers = t.SelectMany(pt => pt.Parametro_transaccion).Where(pt => pt.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroIcbper).Select(pt => pt.valor),
              });
                return Resumen;

            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones", e);
            }
        }

        public IEnumerable<TransaccionPorSerieDeComprobanteYConceptoBasico> ObtenerTransaccionesPorSerieDeComprobanteYConceptoBasico(int[] idsPuntosDeVentas, int[] idsTiposTransaccion, DateTime fechaDesde, DateTime fechaHasta, int idUltimoEstado)
        {
            try
            {
                var Resumen = _db.Transaccion.Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta
                && idsTiposTransaccion.Contains(t.id_tipo_transaccion)
                && idsPuntosDeVentas.Contains(t.id_actor_negocio_interno)
                && t.id_estado_actual == idUltimoEstado
                )
                .SelectMany(t => t.Detalle_transaccion).
              GroupBy(dt => new
              {
                  //dt.Transaccion.Comprobante.id_serie_comprobante,
                  dt.Transaccion.Comprobante.id_tipo_comprobante,
                  dt.Transaccion.Comprobante.Detalle_maestro.valor,

                  dt.Transaccion.Comprobante.numero_serie,
                  IdConceptoBasico = dt.Concepto_negocio.id_concepto_basico,
                  NombreConceptoBasico = dt.Concepto_negocio.Detalle_maestro4.nombre
              }).Select(dt => new TransaccionPorSerieDeComprobanteYConceptoBasico()
              {
                  IdTipoComprobante = dt.Key.id_tipo_comprobante,
                  NombreCortoComprobante = dt.Key.valor,
                  Serie = dt.Key.numero_serie,
                  ConceptoBasico = dt.Key.NombreConceptoBasico,
                  Cantidad = dt.Sum(c => c.cantidad),
                  Importe = dt.Sum(i => i.total)
              });
                return Resumen;

            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones", e);
            }
        }

        public IEnumerable<TransaccionPorSerieDeComprobanteYConceptoBasico> ObtenerTransaccionesPorSerieDeComprobanteYConceptoBasico(int[] idsPuntosDeVentas, int[] idsTiposTransaccion, int idEstadoActual, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                var Resumen = _db.Transaccion.Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta
                && idsTiposTransaccion.Contains(t.id_tipo_transaccion)
                && idsPuntosDeVentas.Contains(t.id_actor_negocio_interno)
                && t.id_estado_actual == idEstadoActual)
                .SelectMany(t => t.Detalle_transaccion).
                GroupBy(dt => new
                {
                    //dt.Transaccion.Comprobante.id_serie_comprobante,
                    dt.Transaccion.Comprobante.id_tipo_comprobante,
                    dt.Transaccion.Comprobante.Detalle_maestro.valor,

                    dt.Transaccion.Comprobante.numero_serie,
                    IdConceptoBasico = dt.Concepto_negocio.id_concepto_basico,
                    NombreConceptoBasico = dt.Concepto_negocio.Detalle_maestro4.nombre
                }).Select(dt => new TransaccionPorSerieDeComprobanteYConceptoBasico()
                {
                    IdTipoComprobante = dt.Key.id_tipo_comprobante,
                    NombreCortoComprobante = dt.Key.valor,
                    Serie = dt.Key.numero_serie,
                    ConceptoBasico = dt.Key.NombreConceptoBasico,
                    Cantidad = dt.Sum(c => c.cantidad),
                    Importe = dt.Sum(i => i.total)
                });
                return Resumen;

            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones", e);

            }
        }

        public IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(int idPuntoDeVenta, int idTipoTransaccion, DateTime fechaDesde, int idUltimoEstado, DateTime fechaHasta)
        {
            try
            {
                var Resumen = _db.Transaccion.Where(sbt => sbt.fecha_inicio >= fechaDesde && sbt.fecha_inicio <= fechaHasta && sbt.id_tipo_transaccion == idTipoTransaccion && sbt.id_actor_negocio_interno == idPuntoDeVenta
                && sbt.id_estado_actual == idUltimoEstado
                );
                return Resumen;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones", e);

            }
        }

        public IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado_(int idPuntoDeVenta, int idTipoTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta)  //y46 anulado
        {
            try
            {
                var Resumen = _db.Transaccion.Where(sbt => sbt.fecha_inicio >= fechaDesde && sbt.fecha_inicio <= fechaHasta && sbt.id_tipo_transaccion == idTipoTransaccion && idPuntoDeVenta == sbt.id_actor_negocio_interno
                  && sbt.id_estado_actual == idUltimoEstado);
                return Resumen;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones", e);
            }
        }

        public IEnumerable<Resumen_Transaccion_Por_Serie_y_ConceptoBasico> ObtenerResumenTransaccionesInclusiveActoresYDetalleMaestroYEstado(int idPuntoDeVenta, int idTipoTransaccion, DateTime fechaDesde, DateTime fechaHasta, int idUltimoEstado)
        {
            try
            {
                var Resumen = _db.Transaccion.Where(sbt => sbt.fecha_inicio >= fechaDesde && sbt.fecha_inicio <= fechaHasta && sbt.id_tipo_transaccion == idTipoTransaccion && idPuntoDeVenta == sbt.id_actor_negocio_interno

                && sbt.id_estado_actual == idUltimoEstado
               ).SelectMany(t => t.Detalle_transaccion).
              GroupBy(dt => new
              {
                  dt.Transaccion.Comprobante.id_serie_comprobante,
                  dt.Transaccion.Comprobante.numero_serie,
                  dt.Transaccion.Comprobante.Detalle_maestro.valor,
                  IdConceptoBasico = dt.Concepto_negocio.id_concepto_basico,
                  NombreConceptoBasico = dt.Concepto_negocio.Detalle_maestro4.nombre
              }).Select(dt => new Resumen_Transaccion_Por_Serie_y_ConceptoBasico()
              {
                  IdSerie = dt.Key.id_serie_comprobante.Value,
                  Serie = dt.Key.numero_serie,
                  NombreCortoDocumento = dt.Key.valor,
                  ConceptoBasico = dt.Key.NombreConceptoBasico,
                  Cantidad = dt.Sum(c => c.cantidad),
                  Importe = dt.Sum(i => i.total)
              });
                return Resumen;
            }
            catch (Exception e)
            { throw e; }
        }

        public IEnumerable<Resumen_Transaccion_Por_Serie_y_ConceptoBasico> ObtenerResumenTransaccionesInclusiveActoresYDetalleMaestroYEstado(int idPuntoDeVenta, int idTipoTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta)//anulado XY6.3 
        {
            try
            {
                var Resumen = _db.Transaccion.Where(sbt => sbt.fecha_inicio >= fechaDesde && sbt.fecha_inicio <= fechaHasta && sbt.id_tipo_transaccion == idTipoTransaccion && idPuntoDeVenta == sbt.id_actor_negocio_interno
                 && sbt.id_estado_actual == idUltimoEstado)
                .SelectMany(t => t.Detalle_transaccion).
              GroupBy(dt => new
              {
                  dt.Transaccion.Comprobante.id_serie_comprobante,
                  dt.Transaccion.Comprobante.numero_serie,
                  dt.Transaccion.Comprobante.Detalle_maestro.valor,
                  IdConceptoBasico = dt.Concepto_negocio.id_concepto_basico,
                  NombreConceptoBasico = dt.Concepto_negocio.Detalle_maestro4.nombre
              }).Select(dt => new Resumen_Transaccion_Por_Serie_y_ConceptoBasico()
              {
                  IdSerie = dt.Key.id_serie_comprobante.Value,
                  Serie = dt.Key.numero_serie,
                  NombreCortoDocumento = dt.Key.valor,
                  ConceptoBasico = dt.Key.NombreConceptoBasico,
                  Cantidad = dt.Sum(c => c.cantidad),
                  Importe = dt.Sum(i => i.total)
              });
                return Resumen;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Resumen_Transaccion_Por_Serie_y_Concepto_Negocio> ObtenerResumenPorSerieYConceptoNegocioTransaccionesInclusiveActoresYDetalleMaestroYEstados(int idPuntoDeVenta, int idTipoTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                var Resumen = _db.Transaccion.Where(sbt => sbt.fecha_inicio >= fechaDesde && sbt.fecha_inicio <= fechaHasta && sbt.id_tipo_transaccion == idTipoTransaccion && idPuntoDeVenta == sbt.id_actor_negocio_interno && sbt.id_estado_actual == idUltimoEstado
               ).SelectMany(t => t.Detalle_transaccion).
              GroupBy(dt => new
              {
                  dt.Transaccion.Comprobante.id_serie_comprobante,
                  dt.Transaccion.Comprobante.numero_serie,
                  dt.id_concepto_negocio,
                  dt.Concepto_negocio.nombre,
                  dt.Concepto_negocio.codigo_barra,
                  dt.Transaccion.Comprobante.Detalle_maestro.valor, //nombre corto de comprobante
                  precioUnitario = dt.precio_unitario

              }).Select(dt => new Resumen_Transaccion_Por_Serie_y_Concepto_Negocio()
              {
                  IdSerie = dt.Key.id_serie_comprobante.Value,
                  Serie = dt.Key.numero_serie,
                  CodigoBarra = dt.Key.codigo_barra,
                  IdConceptoNegocio = dt.Key.id_concepto_negocio,
                  ConceptoNegocio = dt.Key.nombre,
                  NombreCortoDocumento = dt.Key.valor,
                  Cantidad = dt.Sum(c => c.cantidad),
                  Importe = dt.Sum(i => i.total)
              });
                return Resumen.OrderBy(r => r.CodigoBarra ?? r.ConceptoNegocio);
            }
            catch (Exception e)
            { throw e; }
        }

        public IEnumerable<Resumen_Transaccion_Por_Serie_y_ConceptoBasico> ObtenerResumenPorSerieYConceptoBasicoTransaccionesInclusiveActoresYDetalleMaestroYEstados(int idPuntoDeVenta, int idTipoTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta)//invalidos  cod.XY5.4 //corfirmados cod.XY5.2 
        {
            try
            {
                var Resumen = _db.Transaccion.Where(sbt => sbt.fecha_inicio >= fechaDesde && sbt.fecha_inicio <= fechaHasta && sbt.id_tipo_transaccion == idTipoTransaccion && idPuntoDeVenta == sbt.id_actor_negocio_interno && sbt.id_estado_actual == idUltimoEstado
              )
              .SelectMany(t => t.Detalle_transaccion).
              GroupBy(dt => new
              {
                  dt.Transaccion.Comprobante.id_serie_comprobante,
                  dt.Transaccion.Comprobante.numero_serie,
                  dt.Transaccion.Comprobante.Detalle_maestro.valor,
                  IdConceptoBasico = dt.Concepto_negocio.id_concepto_basico,
                  NombreConceptoBasico = dt.Concepto_negocio.Detalle_maestro4.nombre
              }).Select(dt => new Resumen_Transaccion_Por_Serie_y_ConceptoBasico()
              {
                  IdSerie = dt.Key.id_serie_comprobante.Value,
                  Serie = dt.Key.numero_serie,
                  NombreCortoDocumento = dt.Key.valor,
                  ConceptoBasico = dt.Key.NombreConceptoBasico,
                  Cantidad = dt.Sum(c => c.cantidad),
                  Importe = dt.Sum(i => i.total)
              });
                return Resumen;
            }
            catch (Exception e)
            { throw e; }
        }

        public IEnumerable<Resumen_Transaccion_Por_Serie_y_Concepto_Negocio> ObtenerResumenPorSerieYConceptoNegocioTransaccionesInclusiveActoresYDetalleMaestroYEstado(int idPuntoDeVenta, int idTipoTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                var Resumen = _db.Transaccion.Where(sbt => sbt.id_tipo_transaccion == idTipoTransaccion && idPuntoDeVenta == sbt.id_actor_negocio_interno
                && sbt.fecha_inicio >= fechaDesde && sbt.fecha_inicio <= fechaHasta && sbt.id_estado_actual == idUltimoEstado)
                .SelectMany(t => t.Detalle_transaccion).
              GroupBy(dt => new
              {
                  dt.Transaccion.Comprobante.id_serie_comprobante,
                  dt.Transaccion.Comprobante.numero_serie,
                  dt.id_concepto_negocio,
                  dt.Concepto_negocio.nombre,
                  dt.Concepto_negocio.codigo_barra,
                  dt.Transaccion.Comprobante.Detalle_maestro.valor, //nombre corto de comprobante,
                  precioUnitario = dt.precio_unitario

              }).Select(dt => new Resumen_Transaccion_Por_Serie_y_Concepto_Negocio()
              {
                  IdSerie = dt.Key.id_serie_comprobante.Value,
                  Serie = dt.Key.numero_serie,
                  CodigoBarra = dt.Key.codigo_barra,
                  IdConceptoNegocio = dt.Key.id_concepto_negocio,
                  ConceptoNegocio = dt.Key.nombre,
                  NombreCortoDocumento = dt.Key.valor,
                  Cantidad = dt.Sum(c => c.cantidad),
                  Importe = dt.Sum(i => i.total)
              });
                return Resumen.OrderBy(r => r.CodigoBarra ?? r.ConceptoNegocio);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Resumen_Transaccion_Por_Serie_y_ConceptoBasico> ObtenerResumenPorSerieYConceptoBasicoTransaccionesInclusiveActoresYDetalleMaestroYEstado(int idPuntoDeVenta, int idTipoTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                var Resumen = _db.Transaccion.Where(sbt => sbt.id_tipo_transaccion == idTipoTransaccion && idPuntoDeVenta == sbt.id_actor_negocio_interno
                && sbt.fecha_inicio >= fechaDesde && sbt.fecha_inicio <= fechaHasta && sbt.id_estado_actual == idUltimoEstado)
                .SelectMany(t => t.Detalle_transaccion).
              GroupBy(dt => new
              {
                  dt.Transaccion.Comprobante.id_serie_comprobante,
                  dt.Transaccion.Comprobante.numero_serie,
                  dt.Transaccion.Comprobante.Detalle_maestro.valor,
                  IdConceptoBasico = dt.Concepto_negocio.id_concepto_basico,
                  NombreConceptoBasico = dt.Concepto_negocio.Detalle_maestro4.nombre
              }).Select(dt => new Resumen_Transaccion_Por_Serie_y_ConceptoBasico()
              {
                  IdSerie = dt.Key.id_serie_comprobante.Value,
                  Serie = dt.Key.numero_serie,
                  NombreCortoDocumento = dt.Key.valor,
                  ConceptoBasico = dt.Key.NombreConceptoBasico,
                  Cantidad = dt.Sum(c => c.cantidad),
                  Importe = dt.Sum(i => i.total)
              });
                return Resumen;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Resumen_transaccion_Venta_PorConcepto> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstadoDeVendedor(int idEmpleado, int idTipoTransaccion, DateTime fechaDesde, DateTime fechaHasta, int idUltimoEstado)//c1.1 corfirmados  c1.2 invalidos 
        {
            try
            {
                var Resumen = _db.Transaccion.Where(sbt => sbt.id_tipo_transaccion == idTipoTransaccion && sbt.id_empleado == idEmpleado
                && sbt.fecha_inicio >= fechaDesde && sbt.fecha_inicio <= fechaHasta
                && sbt.id_estado_actual == idUltimoEstado
                ).SelectMany(t => t.Detalle_transaccion).
              GroupBy(dt => new
              {
                  idconcepto_negocio = dt.Concepto_negocio.id,
                  dt.Concepto_negocio.codigo_barra,
                  concepto = dt.Concepto_negocio.nombre,
                  precioUnitario = dt.precio_unitario
              }).Select(dt => new Resumen_transaccion_Venta_PorConcepto()
              {
                  IdConceptoNegocio = dt.Key.idconcepto_negocio,
                  CodigoBarra = dt.Key.codigo_barra,
                  Concepto = dt.Key.concepto,
                  Cantidad = dt.Sum(c => c.cantidad),
                  Importe = dt.Sum(i => i.total)
              });
                return Resumen.OrderBy(r => r.CodigoBarra != null ? r.CodigoBarra : r.Concepto);
            }
            catch (Exception e)
            { throw e; }
        }

        public IEnumerable<Resumen_transaccion_Venta_PorConcepto> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstadoDeVendedor(int idEmpleado, int idTipoTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta)//c1.3 anulado
        {
            try
            {
                var Resumen = _db.Transaccion.Where(sbt => sbt.id_tipo_transaccion == idTipoTransaccion && sbt.id_empleado == idEmpleado
                && sbt.fecha_inicio >= fechaDesde && sbt.fecha_inicio <= fechaHasta && sbt.id_estado_actual == idUltimoEstado)
                .SelectMany(t => t.Detalle_transaccion).
              GroupBy(dt => new
              {
                  idconcepto_negocio = dt.Concepto_negocio.id,
                  dt.Concepto_negocio.codigo_barra,
                  concepto = dt.Concepto_negocio.nombre,
                  precioUnitario = dt.precio_unitario

              }).Select(dt => new Resumen_transaccion_Venta_PorConcepto()
              {
                  IdConceptoNegocio = dt.Key.idconcepto_negocio,
                  CodigoBarra = dt.Key.codigo_barra,
                  Concepto = dt.Key.concepto,
                  Cantidad = dt.Sum(c => c.cantidad),
                  Importe = dt.Sum(i => i.total)
              });
                return Resumen.OrderBy(r => r.CodigoBarra ?? r.Concepto);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // Obtener transacciones Resumidas por Vendedor (Confirmadas, Invalidadas)
        public IEnumerable<ResumenTransaccionPorVendedor> ObtenerResumenTransaccionesInclusiveActoresYDetalleMaestroYEstado(int[] idsEmpleado, int idTipoTransaccion, DateTime fechaDesde, DateTime fechaHasta, int idUltimoEstado)// confirmadas a14.1  invalidadas a14.2 
        {
            try
            {
                var Resumen = _db.Transaccion.Where(sbt => sbt.id_tipo_transaccion == idTipoTransaccion && idsEmpleado.Contains(sbt.id_empleado)
                && sbt.fecha_inicio >= fechaDesde && sbt.fecha_inicio <= fechaHasta &&

                sbt.id_estado_actual == idUltimoEstado

               )
              .GroupBy(t => new
              {
                  t.Actor_negocio.Actor.numero_documento_identidad,
                  nombreEmpleado = t.Actor_negocio.Actor.segundo_nombre
              }).Select(t => new ResumenTransaccionPorVendedor()
              {
                  DNI = t.Key.numero_documento_identidad,
                  Nombre = t.Key.nombreEmpleado,
                  Importe = t.Sum(i => i.importe_total),
                  Icbpers = t.SelectMany(pt => pt.Parametro_transaccion).Where(pt => pt.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroIcbper).Select(pt => pt.valor),
              });
                return Resumen;
            }
            catch (Exception e)
            { throw e; }
        }

        public IEnumerable<ResumenTransaccionPorVendedor> ObtenerResumenTransaccionesInclusiveActoresYDetalleMaestroYEstado(int[] idEmpleado, int idTipoTransaccion, int idUtimoEstado, DateTime fechaDesde, DateTime fechaHasta)//a14.3 anulado
        {
            try
            {
                var Resumen = _db.Transaccion.Where(sbt => sbt.fecha_inicio >= fechaDesde && sbt.fecha_inicio <= fechaHasta && sbt.id_tipo_transaccion == idTipoTransaccion && idEmpleado.Contains(sbt.id_empleado)
                 && sbt.id_estado_actual == idUtimoEstado)
              .GroupBy(t => new
              {
                  t.Actor_negocio.Actor.numero_documento_identidad,
                  nombreEmpleado = t.Actor_negocio.Actor.primer_nombre + " " + t.Actor_negocio.Actor.segundo_nombre
                  // concepto = dt.Concepto_negocio.nombre
              }).Select(t => new ResumenTransaccionPorVendedor()
              {
                  DNI = t.Key.numero_documento_identidad,
                  Nombre = t.Key.nombreEmpleado,
                  Importe = t.Sum(i => i.importe_total)
              });
                return Resumen;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // Obtener transacciones Detalladas por Vendedor (Confirmadas, Invalidadas)
        public IEnumerable<DetalleTransaccionPorVendedor> ObtenerDetalleTransaccionesInclusiveActoresYDetalleMaestroYEstado(int[] idsEmpleado, int idTipoTransaccion, DateTime fechaDesde, DateTime fechaHasta, int idUltimoEstado)//confirmadas a14.4   invalidadas a14.5 
        {
            try
            {
                var Resumen = _db.Transaccion.Where(sbt => sbt.fecha_inicio >= fechaDesde && sbt.fecha_inicio <= fechaHasta && sbt.id_tipo_transaccion == idTipoTransaccion && idsEmpleado.Contains(sbt.id_empleado)
                && sbt.id_estado_actual == idUltimoEstado
                ).SelectMany(t => t.Detalle_transaccion).
              GroupBy(dt => new
              {
                  dt.Concepto_negocio.codigo_barra,
                  idconcepto_negocio = dt.Concepto_negocio.id,
                  concepto = dt.Concepto_negocio.nombre,
                  dni = dt.Transaccion.Actor_negocio.Actor.numero_documento_identidad,
                  empleado = dt.Transaccion.Actor_negocio.Actor.segundo_nombre,
                  precioUnitario = dt.precio_unitario
              }).Select(dt => new DetalleTransaccionPorVendedor()
              {
                  DNI = dt.Key.dni,
                  Empleado = dt.Key.empleado,
                  IdConceptoNegocio = dt.Key.idconcepto_negocio,
                  CodigoConceptoNegocio = dt.Key.codigo_barra,
                  NombreConceptoNegocio = dt.Key.concepto,
                  Cantidad = dt.Sum(c => c.cantidad),
                  Importe = dt.Sum(i => i.total)
              });
                return Resumen.OrderBy(r => r.CodigoConceptoNegocio ?? r.NombreConceptoNegocio);
            }
            catch (Exception e)
            { throw e; }
        }

        public IEnumerable<DetalleTransaccionPorVendedor> ObtenerDetalleTransaccionesInclusiveActoresYDetalleMaestroYEstado(int[] idsEmpleado, int idTipoTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta)//anuladas a14.6 
        {
            try
            {
                var Resumen = _db.Transaccion.Where(sbt => sbt.id_tipo_transaccion == idTipoTransaccion && idsEmpleado.Contains(sbt.id_empleado)
                && sbt.fecha_inicio >= fechaDesde && sbt.fecha_inicio <= fechaHasta && sbt.id_estado_actual == idUltimoEstado)
                .SelectMany(t => t.Detalle_transaccion).
              GroupBy(dt => new
              {
                  idconcepto_negocio = dt.Concepto_negocio.id,
                  dt.Concepto_negocio.codigo_barra,
                  concepto = dt.Concepto_negocio.nombre,
                  precioUnitario = dt.precio_unitario

              }).Select(dt => new DetalleTransaccionPorVendedor()
              {
                  IdConceptoNegocio = dt.Key.idconcepto_negocio,
                  CodigoConceptoNegocio = dt.Key.codigo_barra,
                  NombreConceptoNegocio = dt.Key.concepto,
                  Cantidad = dt.Sum(c => c.cantidad),
                  Importe = dt.Sum(i => i.total)
              });
                return Resumen.OrderBy(r => r.CodigoConceptoNegocio ?? r.NombreConceptoNegocio);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<ResumenDeTransaccionVenta> ObtenerResumenDeTransaccion(int idSerieComprobante, int[] idsTiposTransaccion, int[] idEstadosPosibles, int idEstadoAIgnorar, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                var Resumen = _db.Transaccion
                    .Where(t => idsTiposTransaccion.Contains(t.id_tipo_transaccion) && t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && t.Comprobante.id_serie_comprobante == idSerieComprobante && idEstadosPosibles.Contains((int)t.id_estado_actual) && (idEstadoAIgnorar != (int)t.id_estado_actual))
                  .Select(v => new ResumenDeTransaccionVenta()
                  {
                      CentroAtencion = v.Comprobante.Serie_comprobante.Actor_negocio.Actor.primer_nombre,
                      Fecha = v.fecha_inicio,
                      Numero = (int)v.Comprobante.numero,
                      NumeroDocumento = v.Actor_negocio1.Actor.numero_documento_identidad,
                      PrimerNombre = v.Actor_negocio1.Actor.primer_nombre,
                      Importe = v.importe_total,
                      Id = v.id,
                      TipoDocumento = v.Comprobante.Detalle_maestro.valor,
                      Serie = v.Comprobante.numero_serie,
                      OrdenVenta = v.codigo,
                      IdTipoTransaccion = v.id_tipo_transaccion,
                      IdEstado = (int)v.id_estado_actual,
                      Estado = v.Detalle_maestro.nombre
                  });
                return Resumen;
            }
            catch (Exception e)
            { throw new DatosException("Error al obtener transacciones", e); }
        }

        public IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstadoPorSerie(int idSerieComprobante, int idTipoTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta)//anulado XY2.3 
        {
            try
            {
                var Resumen = _db.Transaccion
                                 .Include(t => t.Comprobante)
                                 .Where(sbt => sbt.fecha_inicio >= fechaDesde && sbt.fecha_inicio <= fechaHasta
                                        && sbt.id_tipo_transaccion == idTipoTransaccion
                                        && sbt.Comprobante.id_serie_comprobante == idSerieComprobante
                                        && sbt.id_estado_actual == idUltimoEstado);
                return Resumen;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstadoPorSerie(int idSerieComprobante, int idTipoTransaccion, DateTime fechaDesde, DateTime fechaHasta, int idUltimoEstado)
        {
            try
            {
                var Resumen = _db.Transaccion
                    .Include(t => t.Comprobante)
                    .Where(sbt => sbt.fecha_inicio >= fechaDesde && sbt.fecha_inicio <= fechaHasta
                    && sbt.id_tipo_transaccion == idTipoTransaccion
                    && sbt.Comprobante.id_serie_comprobante == idSerieComprobante
                    && sbt.id_estado_actual == idUltimoEstado);
                return Resumen;
            }
            catch (Exception e)
            { throw e; }
        }

        //[Obsolete("Este método no puede ser usado", true)]
        public IEnumerable<Resumen_Transaccion_Venta_PorConceptoBasico_Y_ConceptoDeNegocio> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstados(int[] idPuntoDeVenta, int[] idsTiposTransaccion, DateTime fechaDesde, DateTime fechaHasta, int idUltimoEstado)
        {
            try
            {
                var Resumen = _db.Transaccion
                 .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta
                                   && idsTiposTransaccion.Contains(t.id_tipo_transaccion)
                                   && idPuntoDeVenta.Contains(t.id_actor_negocio_interno)
                                   && t.id_estado_actual == idUltimoEstado
                   )
                 .SelectMany(t => t.Detalle_transaccion)
                 .GroupBy(dt => new
                 {
                     dt.Concepto_negocio.codigo,
                     dt.id_concepto_negocio,
                     conceptoBasico = dt.Concepto_negocio.Detalle_maestro4.nombre,
                     codigoBarra = dt.Concepto_negocio.codigo_barra,
                     conceptoNegocio = dt.Concepto_negocio.nombre,
                     // precioUnitario = dt.precio_unitario

                 }).Select(dt => new Resumen_Transaccion_Venta_PorConceptoBasico_Y_ConceptoDeNegocio()
                 {
                     IdConceptoNegocio = dt.Key.id_concepto_negocio,
                     NombreBasico = dt.Key.conceptoBasico,
                     CodigoBarra = dt.Key.codigoBarra,
                     ConceptoNegocio = dt.Key.conceptoNegocio,
                     Cantidad = dt.Sum(a => a.cantidad),
                     Importe = dt.Sum(a => a.total)
                 });
                return Resumen;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener resumen de transacciones", e);
            }
        }

        public IEnumerable<Resumen_Transaccion_Venta_PorConceptoBasico_Y_ConceptoDeNegocio> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstados(int[] idsPuntoDeVenta, int[] idsTiposTransaccion, int idEstadoQueDebeContener, DateTime fechaDesde, DateTime fechaHasta)//anulado XY1.3 
        {
            try
            {
                var Resumen = _db.Transaccion
                    .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta
                    && idsTiposTransaccion.Contains(t.id_tipo_transaccion)
                    && idsPuntoDeVenta.Contains(t.id_actor_negocio_interno)
                    && t.Estado_transaccion.Select(et => et.id_estado).Contains(idEstadoQueDebeContener)
                )
                .SelectMany(t => t.Detalle_transaccion).
              GroupBy(dt => new
              {
                  dt.Concepto_negocio.codigo,
                  dt.id_concepto_negocio,
                  conceptoBasico = dt.Concepto_negocio.Detalle_maestro4.nombre,
                  codigoBarra = dt.Concepto_negocio.codigo_barra,
                  conceptoNegocio = dt.Concepto_negocio.nombre,
                  precioUnitario = dt.precio_unitario

              }).Select(dt => new Resumen_Transaccion_Venta_PorConceptoBasico_Y_ConceptoDeNegocio()
              {
                  IdConceptoNegocio = dt.Key.id_concepto_negocio,
                  NombreBasico = dt.Key.conceptoBasico,
                  CodigoBarra = dt.Key.codigoBarra,
                  ConceptoNegocio = dt.Key.conceptoNegocio,
                  Cantidad = dt.Sum(a => a.cantidad),
                  Importe = dt.Sum(a => a.total)

              });
                return Resumen.OrderBy(r => r.CodigoBarra != null ? r.CodigoBarra : r.ConceptoNegocio);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener resumen de transacciones", e);
            }
        }

        public IEnumerable<Resumen_Detalles_Consolidado_Por_Vendedor> ObtenerResumenDetallesConsolidadoPorVendedor(int[] idsTransaccion, int idEstadoActual, int idEmpleado, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                var Resumen = _db.Transaccion.Where(sbt => sbt.fecha_inicio >= fechaDesde && sbt.fecha_inicio <= fechaHasta && idsTransaccion.Contains(sbt.id_tipo_transaccion) && sbt.id_empleado == idEmpleado && sbt.id_estado_actual == idEstadoActual).SelectMany(t => t.Detalle_transaccion).
               GroupBy(dt => new
               {
                   idTipoTransaccion = dt.Transaccion.id_tipo_transaccion,
                   idEmpleado = dt.Transaccion.id_empleado,
                   idConceptoNegocio = dt.Concepto_negocio.id,
                   precioUnitario = dt.precio_unitario
               }).Select(dt => new Resumen_Detalles_Consolidado_Por_Vendedor()
               {
                   IdTipoTransaccion = dt.Key.idTipoTransaccion,
                   IdConceptoBasico = dt.FirstOrDefault().Concepto_negocio.Detalle_maestro4.id,
                   ConceptoBasico = dt.FirstOrDefault().Concepto_negocio.Detalle_maestro4.nombre,
                   IdConceptoNegocio = dt.Key.idConceptoNegocio,
                   CodigoBarra = dt.FirstOrDefault().Concepto_negocio.codigo_barra,
                   ConceptoNegocio = dt.FirstOrDefault().Concepto_negocio.nombre,
                   Cantidad = dt.Sum(c => c.cantidad),
                   Importe = dt.Sum(i => i.total)
               });
                return Resumen.OrderBy(r => r.CodigoBarra ?? r.ConceptoNegocio);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Resumen_Detalles_Consolidado_Por_Vendedor> ObtenerResumenDetallesConsolidadoPorVendedores(int[] idsTransaccion, int idEstadoActual, int[] idsEmpleado, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                var Resumen = _db.Transaccion.Where(sbt => sbt.fecha_inicio >= fechaDesde && sbt.fecha_inicio <= fechaHasta && idsTransaccion.Contains(sbt.id_tipo_transaccion) && idsEmpleado.Contains(sbt.id_empleado) && sbt.id_estado_actual == idEstadoActual).SelectMany(t => t.Detalle_transaccion).
               GroupBy(dt => new
               {
                   idTipoTransaccion = dt.Transaccion.id_tipo_transaccion,
                   idEmpleado = dt.Transaccion.id_empleado,
                   idConceptoBasico = dt.Concepto_negocio.Detalle_maestro4.id,
               }).Select(dt => new Resumen_Detalles_Consolidado_Por_Vendedor()
               {
                   IdTipoTransaccion = dt.Key.idTipoTransaccion,
                   IdEmpleado = dt.Key.idEmpleado,
                   Empleado = dt.FirstOrDefault().Transaccion.Actor_negocio.Actor.numero_documento_identidad + " - " + dt.FirstOrDefault().Transaccion.Actor_negocio.Actor.primer_nombre.Replace("|", " "),
                   IdConceptoBasico = dt.FirstOrDefault().Concepto_negocio.Detalle_maestro4.id,
                   ConceptoBasico = dt.FirstOrDefault().Concepto_negocio.Detalle_maestro4.nombre,
                   Cantidad = dt.Sum(c => c.cantidad),
                   Importe = dt.Sum(i => i.total)
               });
                return Resumen.OrderBy(r => r.IdConceptoBasico);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Resumen_Por_Concepto_Por_Vendedor_Contado_Credito> ObtenerConsolidadoPorVendedoresPorModoPagoPorConcepto_(int[] idsTipoTransaccion, int idEstadoActual, int[] idsEmpleado, int idParametro, string valorParametro, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                var Resumen = _db.Transaccion.Where(sbt => sbt.fecha_inicio >= fechaDesde && sbt.fecha_inicio <= fechaHasta && idsTipoTransaccion.Contains(sbt.id_tipo_transaccion) && idsEmpleado.Contains(sbt.id_empleado) && sbt.id_estado_actual == idEstadoActual).SelectMany(t => t.Detalle_transaccion).
               GroupBy(dt => new
               {
                   idTipoTransaccion = dt.Transaccion.id_tipo_transaccion,
                   idEmpleado = dt.Transaccion.id_empleado,
                   idConceptoNegocio = dt.Concepto_negocio.id,
                   esContado = dt.Transaccion.Parametro_transaccion.FirstOrDefault(pt => pt.id_parametro == idParametro).valor.Equals(valorParametro)
               }).Select(dt => new Resumen_Por_Concepto_Por_Vendedor_Contado_Credito()
               {
                   IdTipoTransaccion = dt.Key.idTipoTransaccion,
                   IdEmpleado = dt.Key.idEmpleado,
                   Empleado = dt.FirstOrDefault().Transaccion.Actor_negocio.Actor.numero_documento_identidad + " - " + dt.FirstOrDefault().Transaccion.Actor_negocio.Actor.primer_nombre.Replace("|", " "),
                   IdConceptoNegocio = dt.Key.idConceptoNegocio,
                   ConceptoNegocio = dt.FirstOrDefault().Concepto_negocio.nombre,
                   CodigoBarra = dt.FirstOrDefault().Concepto_negocio.codigo_barra,
                   Cantidad = dt.Sum(i => i.cantidad),
                   Importe = dt.Sum(i => i.total),
                   //EsContado = dt.FirstOrDefault().Transaccion.Parametro_transaccion.FirstOrDefault(pt => pt.id_parametro == idParametro).valor.Equals(valorParametro)
                   EsContado = dt.Key.esContado
               });
                return Resumen.OrderBy(r => r.CodigoBarra ?? r.ConceptoNegocio);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public IEnumerable<Resumen_Por_Concepto_Por_Vendedor_Contado_Credito> ObtenerConsolidadoPorVendedoresPorModoPagoPorConcepto(int[] idsTipoTransaccion, int idEstadoActual, int[] idsEmpleado, int idParametro, string valorParametro, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                List<Resumen_Por_Concepto_Por_Vendedor_Contado_Credito> resultado = new List<Resumen_Por_Concepto_Por_Vendedor_Contado_Credito>();
                var detallesTransacciones = _db.Transaccion.Where(sbt => sbt.fecha_inicio >= fechaDesde && sbt.fecha_inicio <= fechaHasta && idsTipoTransaccion.Contains(sbt.id_tipo_transaccion) && idsEmpleado.Contains(sbt.id_empleado) && sbt.id_estado_actual == idEstadoActual).SelectMany(t => t.Detalle_transaccion).ToList();
                var idsTransacciones = detallesTransacciones.Select(d=> d.id_transaccion).Distinct().ToArray();
                var contadoCredito= _db.Parametro_transaccion.Where(pt => idsTransacciones.Contains(pt.id_transaccion) && pt.id_parametro == idParametro ).Select(pt=> new {IdTransaccion=pt.id_transaccion, ContadoCredito= pt.valor}).ToList();
                var idsConceptos = detallesTransacciones.Select(dt => dt.id_concepto_negocio).Distinct().ToArray();
                var conceptos = _db.Concepto_negocio.Where(cn=> idsConceptos.Contains(cn.id)).Select(cn => new { IdConcepto = cn.id, CodigoBarra = cn.codigo_barra, Nombre = cn.nombre });
                var tiposTransacciones = _db.Transaccion.Where(t => idsTransacciones.Contains(t.id)).Select(t => new { IdTransaccion = t.id, IdTipoTransaccion = t.id_tipo_transaccion });
                var empleados = _db.Transaccion.Where(t => idsTransacciones.Contains(t.id)).Select(t => new {IdTransaccion = t.id, IdEmpleado=t.id_empleado, Nombre = t.Actor_negocio.Actor.numero_documento_identidad + " - " + t.Actor_negocio.Actor.primer_nombre.Replace("|", " ") });
                
                resultado= detallesTransacciones.Select(dt=> new Resumen_Por_Concepto_Por_Vendedor_Contado_Credito() { 
                    IdTipoTransaccion= tiposTransacciones.Single(tt=>tt.IdTransaccion== dt.id_transaccion).IdTipoTransaccion,
                IdEmpleado= empleados.Single(e => e.IdTransaccion == dt.id_transaccion).IdEmpleado,
                Empleado= empleados.Single(e => e.IdTransaccion == dt.id_transaccion).Nombre, IdConceptoNegocio= dt.id_concepto_negocio,
                ConceptoNegocio= conceptos.Single(c => c.IdConcepto == dt.id_concepto_negocio).Nombre,
                CodigoBarra= conceptos.Single(c => c.IdConcepto == dt.id_concepto_negocio).CodigoBarra,
                Cantidad = dt.cantidad, Importe= dt.total,
                EsContado= contadoCredito.Single(cc => cc.IdTransaccion == dt.id_transaccion).ContadoCredito.Equals(valorParametro)
                }).ToList();

                resultado = resultado.GroupBy(r=> new { idTipoTransaccion = r.IdTipoTransaccion, idEmpleado = r.IdEmpleado, idConceptoNegocio = r.IdConceptoNegocio, esContado = r.EsContado }).Select(r => new Resumen_Por_Concepto_Por_Vendedor_Contado_Credito()
                {
                    IdTipoTransaccion = r.Key.idTipoTransaccion, IdEmpleado = r.Key.idEmpleado, Empleado = r.FirstOrDefault().Empleado,
                    IdConceptoNegocio = r.Key.idConceptoNegocio, ConceptoNegocio = r.FirstOrDefault().ConceptoNegocio,
                    CodigoBarra = r.FirstOrDefault().CodigoBarra, Cantidad = r.Sum(i => i.Cantidad),
                    Importe = r.Sum(i => i.Importe), EsContado = r.Key.esContado
                }).OrderBy(r => r.CodigoBarra ?? r.ConceptoNegocio).ToList();
                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public IEnumerable<DetalleTransaccionVentaCobro> ObtenerDetallesEstadoCuentaClienteVenta(int idTransaccion, int idEstadoActual, int idActor, int idParametro, string valorParametro, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                var resultado = _db.Transaccion.Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && idTransaccion == t.id_tipo_transaccion && idActor == t.id_actor_negocio_externo && t.id_estado_actual == idEstadoActual && t.Parametro_transaccion.Count(pt => pt.id_parametro == idParametro) == 1 && t.Parametro_transaccion.FirstOrDefault(pt => pt.id_parametro == idParametro).valor == valorParametro).SelectMany(t => t.Detalle_transaccion)
                    .Select(dt => new DetalleTransaccionVentaCobro()
                    {
                        IdOperacion = dt.Transaccion.Transaccion2.Transaccion2.id,
                        Fecha = dt.Transaccion.fecha_inicio,
                        Codigo = dt.Concepto_negocio.codigo_barra,
                        Concepto = dt.Concepto_negocio.nombre,
                        Cantidad = dt.cantidad,
                        Importe = dt.total,
                    });
                return resultado;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener los detalles los estado de cuenta de cliente de ventas", e);
            }
        }


        public IEnumerable<DetalleTransaccionVentaCobro> ObtenerDetallesEstadoCuentaClienteCobro(int idTransaccion, int idActor, int idParametro, string valorParametro, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                var resultado = _db.Transaccion.Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && idTransaccion == t.id_tipo_transaccion && idActor == t.id_actor_negocio_externo && t.Parametro_transaccion.Count(pt => pt.id_parametro == idParametro) == 1 && t.Parametro_transaccion.FirstOrDefault(pt => pt.id_parametro == idParametro).valor == valorParametro)
                    .Select(t => new DetalleTransaccionVentaCobro()
                    {
                        IdOperacion = t.Transaccion2.id,
                        Fecha = t.fecha_inicio,
                        Cobro = t.importe_total,
                        Codigo = "-",
                        Concepto = "-",
                        Cantidad = 0,
                        Importe = 0,
                    }); ;
                return resultado;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener los detalles los estado de cuenta de cliente de cobro", e);
            }
        }

        #region REPORTE ESTADO DE CUENTA

        public IEnumerable<Transaccion> ObtenerTransaccionesInclusiveDetalleTransaccionPorActorDeNegocio(int idTipoTransaccion, int idUltimoEstado, int idActorNegocio, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                return _db.Transaccion
                                     .Include(t => t.Detalle_transaccion)
                                     .Include(t => t.Detalle_transaccion.Select(dt => dt.Concepto_negocio))
                                      .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta
                                                  && t.id_tipo_transaccion == idTipoTransaccion
                                                  && t.Actor_negocio1.id == idActorNegocio
                                                  && t.id_estado_actual == idUltimoEstado
                                            );
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Transaccion> ObtenerTransaccionesPorActorDeNegocio(int idTipoDeTransaccion, int idActorDeNegocio, DateTime fechaDesde, DateTime fechaHasta)
        {
            return _db.Transaccion

                                    .Include(t => t.Pago_cuota)
                                    .Include(t => t.Pago_cuota.Select(c => c.Cuota))
                                    .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta
                                                && t.id_tipo_transaccion == idTipoDeTransaccion
                                                && t.id_actor_negocio_externo == idActorDeNegocio
                                      );
        }
        /// <summary>
        /// Devuelve todas las cuotas emitidas antes de <paramref name="fecha"/> y que tienen monnto pendiente de pago
        /// </summary>
        /// <param name="idActorNegocio"></param>
        /// <param name="idEstadoCuota"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public Deuda_Actor_Negocio ObtenerDeudaActorNegocio(int idActorNegocio, DateTime fecha)
        {
            try
            {
                var deudas = _db.Actor_negocio.Where(an => an.id == idActorNegocio)
                                        .Select(an => new Deuda_Actor_Negocio()
                                        {
                                            TotalOrden = an.Transaccion1.Where(t => t.fecha_inicio < fecha).SelectMany(t => t.Cuota).Where(c => c.por_cobrar == true).Sum(c => c.total - c.revocado),
                                            TotalPagoCuota = an.Transaccion1.Where(t => t.fecha_inicio < fecha).SelectMany(t => t.Cuota).Where(c => c.por_cobrar == true).SelectMany(c => c.Pago_cuota).Where(pg => pg.Transaccion.fecha_inicio < fecha).Sum(pc => pc.importe)

                                        });
                var deudasLista = deudas.ToList();
                return deudasLista.SingleOrDefault();
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener deuda de actor de negocio", e);
            }
        }

        #endregion



        #region  VENTAS POR MODALIDAD CONSOLIDADO POR CONCEPTO Y PRECIO UNITARIO
        public IEnumerable<Resumen_Transaccion_Por_Modalidad> ObtenerResumenTransaccionesInclusiveParametroTransaccionActoresYDetalleMaestroYEstado(int idEmpleado, int[] idsTipoTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta, int idParametro, string[] valoresParametros)
        {
            try
            {
                var resumen = _db.Transaccion
                                   .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta
                                       && t.id_empleado == idEmpleado && idsTipoTransaccion.Contains(t.id_tipo_transaccion)
                                       && t.id_estado_actual == idUltimoEstado
                                       && (t.Parametro_transaccion.Count(pt => pt.id_parametro == idParametro) > 0 && valoresParametros.Contains(t.Parametro_transaccion.FirstOrDefault(pt => pt.id_parametro == idParametro).valor))

                                       ).GroupBy(t => new
                                       {
                                           t.Parametro_transaccion.FirstOrDefault(pt => pt.id_parametro == idParametro).valor,
                                           t.id_tipo_transaccion,

                                       }).Select(t => new Resumen_Transaccion_Por_Modalidad()
                                       {
                                           IdModalidad = t.Key.valor,
                                           IdTipoDeTransaccion = t.Key.id_tipo_transaccion,
                                           Importe = t.Sum(i => i.importe_total),
                                           Icbpers = t.SelectMany(pt => pt.Parametro_transaccion).Where(pt => pt.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroIcbper).Select(pt => pt.valor),
                                           CantidadDeOperaciones = t.Count()
                                       });
                return resumen;
            }
            catch (Exception e)
            { throw new DatosException("Error al obtener transacciones", e); }
        }

        public IEnumerable<Detalle_Transaccion_Por_Modalidad> ObtenerDetalleTransaccionesInclusiveParametroTransaccionActorDeNegocioDetalleMaestroYEstadoAgrupadoPorConceptoNegocio(int idEmpleado, int[] idsTipoTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta, int idParametro, string[] valoresParametros)
        {
            try
            {
                var detalles = _db.Transaccion
                           .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta
                            && t.id_empleado == idEmpleado && idsTipoTransaccion.Contains(t.id_tipo_transaccion)
                            && t.id_estado_actual == idUltimoEstado
                            && (t.Parametro_transaccion.Count(pt => pt.id_parametro == idParametro) > 0 && valoresParametros.Contains(t.Parametro_transaccion.FirstOrDefault(pt => pt.id_parametro == idParametro).valor))
                            ).SelectMany(t => t.Detalle_transaccion).
                                                  GroupBy(dt => new
                                                  {
                                                      dt.Concepto_negocio.codigo_barra,
                                                      idconcepto_negocio = dt.id_concepto_negocio,
                                                      concepto = dt.Concepto_negocio.nombre,
                                                      dt.Transaccion.Parametro_transaccion.FirstOrDefault(pt => pt.id_parametro == idParametro).valor,

                                                  }).Select(dt => new Detalle_Transaccion_Por_Modalidad()
                                                  {

                                                      IdModalidad = dt.Key.valor,
                                                      IdConceptoNegocio = dt.Key.idconcepto_negocio,
                                                      CodigoConceptoNegocio = dt.Key.codigo_barra,
                                                      NombreConceptoNegocio = dt.Key.concepto,
                                                      Cantidad = dt.Sum(c => c.cantidad),
                                                      Importe = dt.Sum(t => t.total)
                                                  });
                return detalles.OrderBy(r => r.CodigoConceptoNegocio ?? r.NombreConceptoNegocio);
            }
            catch (Exception e)
            { throw new DatosException("Error al obtener transacciones", e); }
        }

        public IEnumerable<Detalle_Transaccion_Por_Modalidad> ObtenerDetalleTransaccionesInclusiveParametroTransaccionActorDeNegocioDetalleMaestroYEstadoAgrupadoPorConceptoNegocioYPrecioUnitario(int idEmpleado, int[] idsTipoTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta, int idParametro, string[] valoresParametros)
        {
            try
            {
                var detalles = _db.Transaccion
                          .Where(t =>
                            t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta
                           && t.id_empleado == idEmpleado
                           && idsTipoTransaccion.Contains(t.id_tipo_transaccion)
                           && t.id_estado_actual == idUltimoEstado
                           && (t.Parametro_transaccion.Count(pt => pt.id_parametro == idParametro) > 0 && valoresParametros.Contains(t.Parametro_transaccion.FirstOrDefault(pt => pt.id_parametro == idParametro).valor))
                           ).SelectMany(t => t.Detalle_transaccion).
                                                 GroupBy(dt => new
                                                 {
                                                     dt.Concepto_negocio.codigo_barra,
                                                     idconcepto_negocio = dt.id_concepto_negocio,
                                                     concepto = dt.Concepto_negocio.nombre,
                                                     dt.Transaccion.Parametro_transaccion.FirstOrDefault(pt => pt.id_parametro == idParametro).valor,
                                                     dt.precio_unitario

                                                 }).Select(dt => new Detalle_Transaccion_Por_Modalidad()
                                                 {

                                                     IdModalidad = dt.Key.valor,
                                                     IdConceptoNegocio = dt.Key.idconcepto_negocio,
                                                     CodigoConceptoNegocio = dt.Key.codigo_barra,
                                                     NombreConceptoNegocio = dt.Key.concepto,
                                                     Cantidad = dt.Sum(c => c.cantidad),
                                                     Importe = dt.Sum(t => t.total)
                                                 });
                return detalles.OrderBy(r => r.CodigoConceptoNegocio ?? r.NombreConceptoNegocio);
            }
            catch (Exception e)
            { throw new DatosException("Error al obtener transacciones", e); }
        }

        #endregion

        #region DETALLE REPORTE KARDEX

        

        public Ultimo_Precio_Compra_Venta ObtenerUltimoPrecioCompraVenta(int idActorNegocioInterno, int idConceptoNegocio, int idTipoTransaccion, int idTarifaPrecioVenta)
        {
            try
            {
                var resultado = _db.Concepto_negocio.Where(cn => cn.id == idConceptoNegocio)

                                          .Select(
                                                cn => new Ultimo_Precio_Compra_Venta()
                                                {

                                                    UltimoPrecioCompra = cn.Detalle_transaccion
                                                                          .Where(dt => dt.Transaccion.id_actor_negocio_interno == idActorNegocioInterno && dt.Transaccion.id_tipo_transaccion == idTipoTransaccion)
                                                                          .OrderByDescending(dt => dt.Transaccion.fecha_inicio)
                                                                          .ThenByDescending(dt => dt.id).FirstOrDefault().precio_unitario,
                                                    UltimoPrecioVenta = cn.Precio1.Where(p => p.id_actor_negocio == idActorNegocioInterno && p.es_vigente == true && p.id_tarifa_d == idTarifaPrecioVenta)
                                                                          .OrderByDescending(p => p.id).FirstOrDefault().valor
                                                }

                                           ).FirstOrDefault();
                return resultado;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener ultimos precios", e);
            }
        }

        #endregion

        #region DEUDA Y PAGO

        public IEnumerable<Reporte_Deuda> ObtenerDeudas(bool porCobrar, int[] idsTiposTransaccion, int[] idsActorNegocioExterno)
        {
            try
            {
                var resultado = _db.Cuota
                                         .Where(c => c.por_cobrar == porCobrar
                                                 && c.saldo > 0
                                                 && idsTiposTransaccion.Contains(c.Transaccion.id_tipo_transaccion)
                                                 && idsActorNegocioExterno.Contains(c.Transaccion.id_actor_negocio_externo)
                                               )
                                          .Select(c => new Reporte_Deuda()
                                          {
                                              PrimerNombre = c.Transaccion.Actor_negocio1.Actor.primer_nombre,
                                              NumeroDocumento = c.Transaccion.Actor_negocio1.Actor.numero_documento_identidad,
                                              CodigoCuota = c.codigo,
                                              FechaVencimiento = c.fecha_vencimiento,
                                              TipoComprobante = c.Transaccion.Comprobante.Detalle_maestro.nombre,
                                              CodigoComprobante = c.Transaccion.Comprobante.Detalle_maestro.valor,
                                              NumeroSerie = c.Transaccion.Comprobante.numero_serie,
                                              NumeroComprobante = c.Transaccion.Comprobante.numero,
                                              Total = c.total,
                                              PagoACuenta = c.pago_a_cuenta,
                                              Revocado = c.revocado,
                                              Saldo = c.saldo
                                          });
                return resultado;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener deudas", e);
            }
        }

        public IEnumerable<Reporte_Deuda> ObtenerDeudas(bool porCobrar, int[] idsTiposTransaccion)
        {
            try
            {
                var resultado = _db.Cuota
                                         .Where(c => c.por_cobrar == porCobrar
                                                 && c.saldo > 0
                                                 && idsTiposTransaccion.Contains(c.Transaccion.id_tipo_transaccion)
                                               )
                                          .Select(c => new Reporte_Deuda()
                                          {
                                              PrimerNombre = c.Transaccion.Actor_negocio1.Actor.primer_nombre,
                                              NumeroDocumento = c.Transaccion.Actor_negocio1.Actor.numero_documento_identidad,
                                              CodigoCuota = c.codigo,
                                              FechaVencimiento = c.fecha_vencimiento,
                                              TipoComprobante = c.Transaccion.Comprobante.Detalle_maestro.nombre,
                                              CodigoComprobante = c.Transaccion.Comprobante.Detalle_maestro.valor,
                                              NumeroSerie = c.Transaccion.Comprobante.numero_serie,
                                              NumeroComprobante = c.Transaccion.Comprobante.numero,
                                              Total = c.total,
                                              PagoACuenta = c.pago_a_cuenta,
                                              Revocado = c.revocado,
                                              Saldo = c.saldo
                                          });
                return resultado;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener deudas", e);
            }
        }

        public IEnumerable<Reporte_Pago> ObtenerPagos(DateTime fechaDesde, DateTime fechaHasta, int[] idsTiposTransaccion, int[] idsActorNegocioInterno, int[] idsActorNegocioExterno)
        {
            try
            {
                var resultado = _db.Transaccion
                                   .Where(t =>
                                            t.fecha_inicio >= fechaDesde
                                            && t.fecha_inicio <= fechaHasta
                                            && idsTiposTransaccion.Contains(t.id_tipo_transaccion)
                                            && idsActorNegocioInterno.Contains(t.id_actor_negocio_interno)
                                            && idsActorNegocioExterno.Contains(t.id_actor_negocio_externo)
                                           )
                                     .SelectMany(t => t.Pago_cuota)
                                     //.Where(pc => pc.Cuota.Estado_cuota.OrderByDescending(ec => ec.id).FirstOrDefault().id_estado == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                                     .Select(pc => new Reporte_Pago()

                                     {
                                         PrimerNombre = pc.Cuota.Transaccion.Actor_negocio1.Actor.primer_nombre,
                                         NumeroDocumento = pc.Cuota.Transaccion.Actor_negocio1.Actor.numero_documento_identidad,
                                         CodigoCuota = pc.Cuota.codigo,
                                         FechaPago = pc.Cuota.Transaccion.fecha_inicio,
                                         TipoComprobante = pc.Cuota.Transaccion.Comprobante.Detalle_maestro.nombre,
                                         CodigoComprobante = pc.Cuota.Transaccion.Comprobante.Detalle_maestro.valor,
                                         NumeroSerie = pc.Cuota.Transaccion.Comprobante.numero_serie,
                                         Trazas = pc.Cuota.Transaccion.Traza_pago.Select(tp => tp.traza),
                                         NumeroComprobante = pc.Cuota.Transaccion.Comprobante.numero,
                                         PagoACuenta = pc.importe

                                     });
                return resultado;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener pagos", e);
            }
        }

        public IEnumerable<Reporte_Pago> ObtenerPagosExterno(DateTime fechaDesde, DateTime fechaHasta, int[] idsTiposTransaccion, int[] idsActorNegocioExterno)
        {
            try
            {
                var resultado = _db.Transaccion
                                   .Where(t =>
                                            t.fecha_inicio >= fechaDesde
                                            && t.fecha_inicio <= fechaHasta
                                            && idsTiposTransaccion.Contains(t.id_tipo_transaccion)
                                            && idsActorNegocioExterno.Contains(t.id_actor_negocio_externo)
                                           )
                                     .SelectMany(t => t.Pago_cuota)
                                     //.Where(pc => pc.Cuota.Estado_cuota.OrderByDescending(ec => ec.id).FirstOrDefault().id_estado == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                                     .Select(pc => new Reporte_Pago()

                                     {
                                         PrimerNombre = pc.Cuota.Transaccion.Actor_negocio1.Actor.primer_nombre,
                                         NumeroDocumento = pc.Cuota.Transaccion.Actor_negocio1.Actor.numero_documento_identidad,
                                         CodigoCuota = pc.Cuota.codigo,
                                         FechaPago = pc.Cuota.Transaccion.fecha_inicio,
                                         TipoComprobante = pc.Cuota.Transaccion.Comprobante.Detalle_maestro.nombre,
                                         CodigoComprobante = pc.Cuota.Transaccion.Comprobante.Detalle_maestro.valor,
                                         NumeroSerie = pc.Cuota.Transaccion.Comprobante.numero_serie,
                                         Trazas = pc.Cuota.Transaccion.Traza_pago.Select(tp => tp.traza),
                                         NumeroComprobante = pc.Cuota.Transaccion.Comprobante.numero,
                                         PagoACuenta = pc.importe

                                     });
                return resultado;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener pagos", e);
            }
        }

        public IEnumerable<Reporte_Pago> ObtenerPagosInterno(DateTime fechaDesde, DateTime fechaHasta, int[] idsTiposTransaccion, int[] idsActorNegocioInterno)
        {
            try
            {
                var resultado = _db.Transaccion
                                   .Where(t =>
                                            t.fecha_inicio >= fechaDesde
                                            && t.fecha_inicio <= fechaHasta
                                            && idsTiposTransaccion.Contains(t.id_tipo_transaccion)
                                            && idsActorNegocioInterno.Contains(t.id_actor_negocio_interno)
                                           )
                                     .SelectMany(t => t.Pago_cuota)
                                     //.Where(pc => pc.Cuota.Estado_cuota.OrderByDescending(ec => ec.id).FirstOrDefault().id_estado == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                                     .Select(pc => new Reporte_Pago()

                                     {
                                         PrimerNombre = pc.Cuota.Transaccion.Actor_negocio1.Actor.primer_nombre,
                                         NumeroDocumento = pc.Cuota.Transaccion.Actor_negocio1.Actor.numero_documento_identidad,
                                         CodigoCuota = pc.Cuota.codigo,
                                         FechaPago = pc.Cuota.Transaccion.fecha_inicio,
                                         TipoComprobante = pc.Cuota.Transaccion.Comprobante.Detalle_maestro.nombre,
                                         CodigoComprobante = pc.Cuota.Transaccion.Comprobante.Detalle_maestro.valor,
                                         NumeroSerie = pc.Cuota.Transaccion.Comprobante.numero_serie,
                                         Trazas = pc.Cuota.Transaccion.Traza_pago.Select(tp => tp.traza),
                                         NumeroComprobante = pc.Cuota.Transaccion.Comprobante.numero,
                                         PagoACuenta = pc.importe

                                     });
                return resultado;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener pagos", e);
            }
        }

        public IEnumerable<Reporte_Pago> ObtenerPagos(DateTime fechaDesde, DateTime fechaHasta, int[] idsTiposTransaccion)
        {
            try
            {
                var resultado = _db.Transaccion
                                   .Where(t =>
                                            t.fecha_inicio >= fechaDesde
                                            && t.fecha_inicio <= fechaHasta
                                            && idsTiposTransaccion.Contains(t.id_tipo_transaccion)
                                           )
                                     .SelectMany(t => t.Pago_cuota)
                                     //.Where(pc => pc.Cuota.Estado_cuota.OrderByDescending(ec => ec.id).FirstOrDefault().id_estado == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                                     .Select(pc => new Reporte_Pago()

                                     {
                                         PrimerNombre = pc.Cuota.Transaccion.Actor_negocio1.Actor.primer_nombre,
                                         NumeroDocumento = pc.Cuota.Transaccion.Actor_negocio1.Actor.numero_documento_identidad,
                                         CodigoCuota = pc.Cuota.codigo,
                                         FechaPago = pc.Cuota.Transaccion.fecha_inicio,
                                         TipoComprobante = pc.Cuota.Transaccion.Comprobante.Detalle_maestro.nombre,
                                         CodigoComprobante = pc.Cuota.Transaccion.Comprobante.Detalle_maestro.valor,
                                         NumeroSerie = pc.Cuota.Transaccion.Comprobante.numero_serie,
                                         Trazas = pc.Cuota.Transaccion.Traza_pago.Select(tp => tp.traza),
                                         NumeroComprobante = pc.Cuota.Transaccion.Comprobante.numero,
                                         PagoACuenta = pc.importe

                                     });
                return resultado;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener pagos", e);
            }
        }

        #endregion

        #region REPORTE DE CONCEPTOS PROXIMOS A VENCER POR ALMACEN 



        public IEnumerable<Movimientos_concepto_negocio_actor_negocio_interno> ObtenerMovimientosConceptoNegocioConLote(int idActorNegocioInterno, string[] conceptosNegocioConLote, DateTime fechaDesde)
        {
            try
            {
                var movimientos = _db.Transaccion
                    .Where(t => idActorNegocioInterno == t.id_actor_negocio_interno && t.fecha_inicio >= fechaDesde)
                    .SelectMany(t => t.Detalle_transaccion).Where(dt => conceptosNegocioConLote.Contains(String.Concat(dt.id_concepto_negocio, "-", dt.lote))).
                    GroupBy(dt => new
                    {
                        IdEntidadInterna = dt.Transaccion.id_actor_negocio_interno,
                        IdConceptoNegocio = dt.id_concepto_negocio,
                        Lote = dt.lote
                    }).Select(m => new Movimientos_concepto_negocio_actor_negocio_interno()
                    {
                        Id_entidad_interna = m.Key.IdEntidadInterna,
                        Id_concepto_negocio = m.Key.IdConceptoNegocio,
                        Lote = m.Key.Lote,
                        Entradas_principal = m.Where(dt => dt.Transaccion.Tipo_transaccion.Accion_de_negocio_por_tipo_transaccion.Any(antt => antt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnAlmacen && antt.valor) && dt.Transaccion.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado).Select(dt => dt.cantidad).DefaultIfEmpty(0).Sum(),
                        Salidas_principal = m.Where(dt => dt.Transaccion.Tipo_transaccion.Accion_de_negocio_por_tipo_transaccion.Any(antt => antt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnAlmacen && !antt.valor) && dt.Transaccion.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado).Select(dt => dt.cantidad).DefaultIfEmpty(0).Sum()
                    });
                return movimientos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region  REPORTE DE CONCEPTO BASICO

        public IEnumerable<Reporte_Concepto_Basico> ObtenerReportePorConceptoBasico(DateTime fechaDesde, DateTime fechaHasta, int idConceptoBasico, int[] idsTiposTransaccion, int[] idsTiposComprobante, int[] idsActorNegocioInterno)
        {
            try
            {

                var resultado = _db.Transaccion
                                   .Where(t =>
                                            t.fecha_inicio >= fechaDesde
                                            && t.fecha_inicio <= fechaHasta
                                            && idsActorNegocioInterno.Contains(t.id_actor_negocio_interno)
                                            && idsTiposTransaccion.Contains(t.id_tipo_transaccion)
                                            && idsTiposComprobante.Contains(t.Comprobante.id_tipo_comprobante)
                                            && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado
                                      )
                                     .SelectMany(t => t.Detalle_transaccion)
                                     .Where(dt => dt.Concepto_negocio.Detalle_maestro4.id == idConceptoBasico)
                                     .Select(rcb => new Reporte_Concepto_Basico()

                                     {
                                         FechaInicio = rcb.Transaccion.fecha_inicio,
                                         CantidadVendida = rcb.cantidad,
                                         IdConceptoNegocio = rcb.Concepto_negocio.Detalle_maestro4.id,
                                         NombreConceptoBasico = rcb.Concepto_negocio.Detalle_maestro4.nombre,
                                         NombreConceptoNegocio = rcb.Concepto_negocio.nombre,
                                         Sufijo = rcb.Concepto_negocio.sufijo,

                                         //Nombre presentacion
                                         NombrePresentacion = rcb.Concepto_negocio.Detalle_maestro3.nombre,

                                         //Contenido
                                         Contenido = rcb.Concepto_negocio.contenido,
                                         // Valor unidad medida y nombre unidad medida que se usa en presentacion
                                         ValorUnidadMedida = rcb.Concepto_negocio.Detalle_maestro1.valor,
                                         NombreUnidadMedida = rcb.Concepto_negocio.Detalle_maestro1.nombre,

                                         IdTipoComprobante = rcb.Transaccion.Comprobante.id_tipo_comprobante,
                                         CodigoTipoComprobante = rcb.Transaccion.Comprobante.Detalle_maestro.valor,
                                         NumeroComprobante = rcb.Transaccion.Comprobante.numero,
                                         NumeroSerieComprobante = rcb.Transaccion.Comprobante.numero_serie,

                                         NombreCliente = rcb.Transaccion.Actor_negocio1.Actor.primer_nombre,
                                         NumeroDocumentoCliente = rcb.Transaccion.Actor_negocio1.Actor.numero_documento_identidad,

                                         CodigoUbigeo = rcb.Transaccion.Actor_negocio2.Actor_negocio2.Actor.Direccion.Where(d => d.id_tipo_direccion == MaestroSettings.Default.IdDetalleMaestroTipoDireccionDomicilioFiscal && d.es_activo).FirstOrDefault().id_ubigeo,
                                         DireccionCentroDeAtencion = rcb.Transaccion.Actor_negocio2.Actor_negocio2.Actor.Direccion.Where(d => d.id_tipo_direccion == MaestroSettings.Default.IdDetalleMaestroTipoDireccionDomicilioFiscal && d.es_activo).FirstOrDefault().detalle,



                                     });

                return resultado;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener reporte de concepto básico", e);

            }
        }
        #endregion

        #region VALIDACIONES

        public int ObtenerLaCantidadDeDetallesDeTransaccionOcurridasPorConceptoNegocio(int idConceptoNegocio, int idTipoTransaccion)
        {
            int cantidad = _db.Transaccion.Where(t => t.id_tipo_transaccion == idTipoTransaccion)
                                          .SelectMany(t => t.Detalle_transaccion).Where(dt => dt.id_concepto_negocio == idConceptoNegocio)
                                          .Count();
            return cantidad;
        }

        #endregion


        #region REPORTE DE UTILIDAD DE VENTAS
        public IEnumerable<CostoUtilidadPorConcepto> ObtenerReporteDeUtilidadDeVentasPorFamilia(int idTransaccionOrden, int idTransaccionMovimiento, int idEstado, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                var Resumen = _db.Transaccion.Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && t.id_tipo_transaccion == idTransaccionOrden && t.id_estado_actual == idEstado)
                    .SelectMany(t => t.Detalle_transaccion).Where(dt => dt.Concepto_negocio.Detalle_maestro4.valor.Equals("1"))
                    .GroupBy(dt => new
                    {
                        idConcepto = dt.Concepto_negocio.Detalle_maestro4.id,
                        concepto = dt.Concepto_negocio.Detalle_maestro4.nombre,
                    }).Select(dt => new CostoUtilidadPorConcepto()
                    {
                        IdConcepto = dt.Key.idConcepto,
                        Concepto = dt.Key.concepto,
                        Cantidad = dt.Sum(i => i.cantidad),
                        Importe = dt.Sum(i => i.total),
                        Costo = dt.Sum(i => i.Transaccion.Transaccion11.Where(tt => tt.id_tipo_transaccion == idTransaccionMovimiento && tt.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado).SelectMany(td => td.Detalle_transaccion).Where(dtt => dtt.Concepto_negocio.Detalle_maestro4.id == dt.Key.idConcepto).Average(dd => dd.total))
                    });
                return Resumen;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener reporte de utilidad", e);
            }
        }
        public IEnumerable<CostoUtilidadPorConcepto> ObtenerReporteDeUtilidadDeVentasPorFamilia(int idTransaccionOrden, int idTransaccionMovimiento, int idEstado, int[] idsCentroAtencion, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                var Resumen = _db.Transaccion.Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && t.id_tipo_transaccion == idTransaccionOrden && t.id_estado_actual == idEstado && idsCentroAtencion.Contains(t.id_actor_negocio_interno))
                    .SelectMany(t => t.Detalle_transaccion).Where(dt => dt.Concepto_negocio.Detalle_maestro4.valor.Equals("1"))
                    .GroupBy(dt => new
                    {
                        idConcepto = dt.Concepto_negocio.Detalle_maestro4.id,
                        concepto = dt.Concepto_negocio.Detalle_maestro4.nombre,
                    }).Select(dt => new CostoUtilidadPorConcepto()
                    {
                        IdConcepto = dt.Key.idConcepto,
                        Concepto = dt.Key.concepto,
                        Cantidad = dt.Sum(i => i.cantidad),
                        Importe = dt.Sum(i => i.total),
                        Costo = dt.Sum(i => i.Transaccion.Transaccion11.Where(tt => tt.id_tipo_transaccion == idTransaccionMovimiento && tt.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado).SelectMany(td => td.Detalle_transaccion).Where(dtt => dtt.Concepto_negocio.Detalle_maestro4.id == dt.Key.idConcepto).Average(dd => dd.total))
                    });
                return Resumen;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener reporte de utilidad", e);
            }
        }

        public IEnumerable<CostoUtilidadPorConcepto> ObtenerReporteDeUtilidadDeVentasPorConcepto(int idTransaccionOrden, int idTransaccionMovimiento, int idEstado, int[] idsConceptoBasico, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                var Resumen = _db.Transaccion.Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && t.id_tipo_transaccion == idTransaccionOrden && t.id_estado_actual == idEstado)
                     .SelectMany(t => t.Detalle_transaccion).Where(dt => dt.Concepto_negocio.Detalle_maestro4.valor.Equals("1"))
                     .Where(dt => idsConceptoBasico.Contains(dt.Concepto_negocio.Detalle_maestro4.id))
                     .GroupBy(dt => new
                     {
                         idConcepto = dt.id_concepto_negocio,
                         concepto = dt.Concepto_negocio.nombre,
                     }).Select(dt => new CostoUtilidadPorConcepto()
                     {
                         IdConcepto = dt.Key.idConcepto,
                         Concepto = dt.Key.concepto,
                         Cantidad = dt.Sum(i => i.cantidad),
                         Importe = dt.Sum(i => i.total),
                         Costo = dt.Sum(i => i.Transaccion.Transaccion11.Where(tt => tt.id_tipo_transaccion == idTransaccionMovimiento && tt.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado).SelectMany(td => td.Detalle_transaccion).Where(d => d.id_concepto_negocio == dt.Key.idConcepto).Sum(dd => dd.total))
                     });
                return Resumen;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener reprote de utilidad", e);
            }
        }

        public IEnumerable<CostoUtilidadPorConcepto> ObtenerReporteDeUtilidadDeVentasPorConcepto(int idTransaccionOrden, int idTransaccionMovimiento, int idEstado, int[] idsCentroAtencion, int[] idsConceptoBasico, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                var Resumen = _db.Transaccion.Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && t.id_tipo_transaccion == idTransaccionOrden && idsCentroAtencion.Contains(t.id_actor_negocio_interno) && t.id_estado_actual == idEstado)
                     .SelectMany(t => t.Detalle_transaccion).Where(dt => dt.Concepto_negocio.Detalle_maestro4.valor.Equals("1"))
                     .Where(dt => idsConceptoBasico.Contains(dt.Concepto_negocio.Detalle_maestro4.id))
                     .GroupBy(dt => new
                     {
                         idConcepto = dt.id_concepto_negocio,
                         concepto = dt.Concepto_negocio.nombre,
                     }).Select(dt => new CostoUtilidadPorConcepto()
                     {
                         IdConcepto = dt.Key.idConcepto,
                         Concepto = dt.Key.concepto,
                         Cantidad = dt.Sum(i => i.cantidad),
                         Importe = dt.Sum(i => i.total),
                         Costo = dt.Sum(i => i.Transaccion.Transaccion11.Where(tt => tt.id_tipo_transaccion == idTransaccionMovimiento && tt.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado).SelectMany(td => td.Detalle_transaccion).Where(d => idsConceptoBasico.Contains(d.Concepto_negocio.Detalle_maestro4.id)).Sum(dd => dd.total))
                     });
                return Resumen;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener reprote de utilidad", e);
            }
        }
        #endregion

        public IEnumerable<ItemConGrupoOperacionComercial> ObtenerItemOperacionPorCaracteristica(int idTipoTransaccion, int idEstadoActual, DateTime fechaDesde, DateTime fechaHasta, int[] idsActorNegocioInterno, int idCaracteristica)
        {
            List<ItemConGrupoOperacionComercial> Items;
            try
            {
                Items = _db.Transaccion
                    .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta
                    && t.id_tipo_transaccion == idTipoTransaccion
                    && idsActorNegocioInterno.Contains(t.id_actor_negocio_interno)
                    && t.id_estado_actual == idEstadoActual
                )
                .SelectMany(t => t.Detalle_transaccion).
              GroupBy(dt => new
              {
                  IdConcepto = dt.Concepto_negocio.id,
                  NombreItem = dt.Concepto_negocio.nombre                  
                  //,
                  //IdGrupo = dt.Concepto_negocio.Valor_caracteristica_concepto_negocio.FirstOrDefault(vccn => vccn.Valor_caracteristica.id_caracteristica == idCaracteristica).Valor_caracteristica.id_caracteristica,
                  //NombreGrupo = dt.Concepto_negocio.Valor_caracteristica_concepto_negocio.FirstOrDefault(vccn => vccn.Valor_caracteristica.id_caracteristica == idCaracteristica).Valor_caracteristica.valor
              }).Select(dt => new ItemConGrupoOperacionComercial()
              {
                  IdItem = dt.Key.IdConcepto,
                  NombreItem = dt.Key.NombreItem
                  ,
                  //IdGrupo = dt.Key.IdGrupo,
                  //NombreGrupo = dt.Key.NombreGrupo,
                  Cantidad = dt.Sum(a => a.cantidad),
                  Importe = dt.Sum(a => a.total)

              }).ToList();

                var idsConceptosNegocio = Items.Select(i => i.IdItem).ToArray();

                var marcas = _db.Concepto_negocio.Where(cn => idsConceptosNegocio.Contains(cn.id)).SelectMany(cn => cn.Valor_caracteristica_concepto_negocio).Where(vccn => vccn.Valor_caracteristica.id_caracteristica == idCaracteristica).Select(vccn=> new ItemConGrupoOperacionComercial() {IdItem= vccn.id_concepto_negocio, IdGrupo= vccn.Valor_caracteristica.id, NombreGrupo= vccn.Valor_caracteristica.valor }).ToList();

                Items.ForEach(i => 
                { 
                    i.IdGrupo = marcas.Any(m => m.IdItem == i.IdItem)? marcas.Single(m => m.IdItem == i.IdItem).IdGrupo:0;
                    i.NombreGrupo = marcas.Any(m => m.IdItem == i.IdItem) ? marcas.Single(m => m.IdItem == i.IdItem).NombreGrupo:"NN";
                });

                
                return Items;
            }
            catch(Exception e)
            {
                throw new DatosException("Error al intentar obtener items ",e);
            }
        }

        public IEnumerable<ItemDetalladoOperacionComercial> ObtenerItemsDetalladoDeVentaConMedioPago(int idTipoTransaccion, int idEstadoActual, DateTime fechaDesde, DateTime fechaHasta, int[] idsCaracteristicas, int idPuntoVenta)
        {
            List<ItemDetalladoOperacionComercial> Items;
            try
            {
                Items = _db.Transaccion
                    .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta
                    && t.id_tipo_transaccion == idTipoTransaccion
                    && t.id_actor_negocio_interno== idPuntoVenta
                    && t.id_estado_actual == idEstadoActual
                )
                .SelectMany(t => t.Detalle_transaccion)
              .Select(dt => new ItemDetalladoOperacionComercial()
              {
                  IdItem = dt.id_concepto_negocio,
                  IdOperacion= dt.id_transaccion,
                  IdOperacionWrapper = dt.Transaccion.id_transaccion_padre,
                  Fecha = dt.Transaccion.fecha_inicio,
                  Comprobante= dt.Transaccion.Comprobante.numero_serie + "-"+dt.Transaccion.Comprobante.numero,
                  Familia= dt.Concepto_negocio.Detalle_maestro4.nombre,
                  Sufijo= dt.Concepto_negocio.sufijo,
                  Cantidad=dt.cantidad,
                  Importe = dt.total
              }).ToList();
                var idsConceptosNegocio = Items.Select(i => i.IdItem).ToArray();
                var caracteristicas = _db.Concepto_negocio.Where(cn => idsConceptosNegocio.Contains(cn.id)).SelectMany(cn => cn.Valor_caracteristica_concepto_negocio).Where(vccn => idsCaracteristicas.Contains(vccn.Valor_caracteristica.id_caracteristica)).Select(vccn => new ItemConGrupoOperacionComercial() { IdItem = vccn.id_concepto_negocio, IdGrupo = vccn.Valor_caracteristica.id_caracteristica, NombreGrupo = vccn.Valor_caracteristica.valor }).ToList();
                
                var idsTransaccionesWrappers = Items.Select(i => i.IdOperacionWrapper).Distinct().ToArray();
                var mediosPagoItems = _db.Transaccion.Where(t => idsTransaccionesWrappers.Contains(t.id_transaccion_padre) && t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionCobranzaFacturasClientes).Select(t => new ItemConGrupoOperacionComercial() {IdItem=t.id_transaccion_padre,  IdGrupo= t.Traza_pago.FirstOrDefault().id_medio_pago}).ToList();
                var idsMediosPagos = mediosPagoItems.Select(mp => mp.IdGrupo).Distinct().ToArray();
                var mediosPagoDetalleMaestro = _db.Detalle_maestro.Where(dm => dm.id_maestro == MaestroSettings.Default.IdMaestroMedioDePago).ToList();
                mediosPagoItems.ForEach(mp => mp.NombreGrupo = mediosPagoDetalleMaestro.Single(mp_ => mp_.id == mp.IdGrupo).nombre);

                Items.ForEach(item=> {
                    for (int i = 0; i < idsCaracteristicas.Count(); i++)
                    {
                        Type type = item.GetType();
                        System.Reflection.PropertyInfo propertyInfo = type.GetProperty("Caracteristica" + (i+1));
                        propertyInfo.SetValue(item,
                        caracteristicas.Any(m => m.IdItem == item.IdItem
                        && m.IdGrupo == idsCaracteristicas[i]) ? caracteristicas.Single(m => m.IdItem == item.IdItem && m.IdGrupo == idsCaracteristicas[i]).NombreGrupo : "");
                    }
                    item.MedioPago = mediosPagoItems.Any(mpi => mpi.IdItem == item.IdOperacionWrapper)?mediosPagoItems.First(mpi => mpi.IdItem == item.IdOperacionWrapper).NombreGrupo:"NINGUNO";
                });
                
                return Items;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener items ", e);
            }
        }
        
        public IEnumerable<VentaConceptoCliente> ObtenerItemVentaPorFamiliaCaracteristica(int idTipoTransaccion, int idEstadoActual, DateTime fechaDesde, DateTime fechaHasta, int[] idsActorNegocioInterno, int idFamilia, int idCaracteristica, int idValorCaracteristica)
        {
            try
            {
                var Items = _db.Transaccion
                    .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta
                    && t.id_tipo_transaccion == idTipoTransaccion
                    && idsActorNegocioInterno.Contains(t.id_actor_negocio_interno)
                    && t.id_estado_actual == idEstadoActual
                )
                .SelectMany(t => t.Detalle_transaccion).
                Where(dt => dt.Concepto_negocio.Detalle_maestro4.id == idFamilia
                && dt.Concepto_negocio.Valor_caracteristica_concepto_negocio.Any(vccn => vccn.Valor_caracteristica.id_caracteristica == idCaracteristica) 
                && dt.Concepto_negocio.Valor_caracteristica_concepto_negocio.Any(vccn => vccn.id_valor_caracteristica == idValorCaracteristica))
                .Select(dt => new VentaConceptoCliente()
                {
                    DocumentoIdentidad = dt.Transaccion.Actor_negocio1.Actor.numero_documento_identidad,
                    RazonSocial = dt.Transaccion.Actor_negocio1.Actor.primer_nombre.Replace("|", " "),
                    Direccion = dt.Transaccion.Actor_negocio1.Actor.Direccion.FirstOrDefault().detalle,
                    IdUbigeo = dt.Transaccion.Actor_negocio1.Actor.Direccion.FirstOrDefault().id_ubigeo,
                    Ubigeo = dt.Transaccion.Actor_negocio1.Actor.Direccion.FirstOrDefault().Ubigeo.descripcion_larga,
                    FechaInicio = dt.Transaccion.fecha_inicio,
                    SerieComprobante = dt.Transaccion.Comprobante.numero_serie,
                    NumeroComprobante = dt.Transaccion.Comprobante.numero,
                    Concepto = dt.Concepto_negocio.nombre,
                    Cantidad = dt.cantidad,
                    UnidadMedida = dt.Concepto_negocio.Detalle_maestro2.codigo

                }).ToList();
                return Items;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener items ", e);
            }
        }

        public IEnumerable<VentaConceptoCliente> ObtenerItemVentaPorCaracteristica(int idTipoTransaccion, int idEstadoActual, DateTime fechaDesde, DateTime fechaHasta, int[] idsActorNegocioInterno, int idCaracteristica, int idValorCaracteristica)
        {
            try
            {
                var Items = _db.Transaccion
                    .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta
                    && t.id_tipo_transaccion == idTipoTransaccion
                    && idsActorNegocioInterno.Contains(t.id_actor_negocio_interno)
                    && t.id_estado_actual == idEstadoActual
                )
                .SelectMany(t => t.Detalle_transaccion).
                Where(dt => dt.Concepto_negocio.Valor_caracteristica_concepto_negocio.Any(vccn => vccn.Valor_caracteristica.id_caracteristica == idCaracteristica)
                && dt.Concepto_negocio.Valor_caracteristica_concepto_negocio.Any(vccn => vccn.id_valor_caracteristica == idValorCaracteristica))
                .Select(dt => new VentaConceptoCliente()
                {
                    DocumentoIdentidad = dt.Transaccion.Actor_negocio1.Actor.numero_documento_identidad,
                    RazonSocial = dt.Transaccion.Actor_negocio1.Actor.primer_nombre.Replace("|", " "),
                    Direccion = dt.Transaccion.Actor_negocio1.Actor.Direccion.FirstOrDefault().detalle,
                    IdUbigeo = dt.Transaccion.Actor_negocio1.Actor.Direccion.FirstOrDefault().id_ubigeo,
                    Ubigeo = dt.Transaccion.Actor_negocio1.Actor.Direccion.FirstOrDefault().Ubigeo.descripcion_larga,
                    FechaInicio = dt.Transaccion.fecha_inicio,
                    SerieComprobante = dt.Transaccion.Comprobante.numero_serie,
                    NumeroComprobante = dt.Transaccion.Comprobante.numero,
                    Concepto = dt.Concepto_negocio.nombre,
                    Cantidad = dt.cantidad,
                    UnidadMedida = dt.Concepto_negocio.Detalle_maestro2.codigo

                }).ToList();
                return Items;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener items ", e);
            }
        }

        public IEnumerable<VentaConceptoCliente> ObtenerItemVentaPorConcepto(int idTipoTransaccion, int idEstadoActual, DateTime fechaDesde, DateTime fechaHasta, int[] idsActorNegocioInterno, int idConcepto)
        {
            try
            {
                var Items = _db.Transaccion
                     .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta
                     && t.id_tipo_transaccion == idTipoTransaccion
                     && idsActorNegocioInterno.Contains(t.id_actor_negocio_interno)
                     && t.id_estado_actual == idEstadoActual
                 )
                 .SelectMany(t => t.Detalle_transaccion)
                 .Where(dt => dt.Concepto_negocio.id == idConcepto)
                 .Select(dt => new VentaConceptoCliente()
                 {
                     DocumentoIdentidad = dt.Transaccion.Actor_negocio1.Actor.numero_documento_identidad,
                     RazonSocial = dt.Transaccion.Actor_negocio1.Actor.primer_nombre.Replace("|", " "),
                     Direccion = dt.Transaccion.Actor_negocio1.Actor.Direccion.FirstOrDefault().detalle,
                     IdUbigeo = dt.Transaccion.Actor_negocio1.Actor.Direccion.FirstOrDefault().id_ubigeo,
                     Ubigeo = dt.Transaccion.Actor_negocio1.Actor.Direccion.FirstOrDefault().Ubigeo.descripcion_larga,
                     FechaInicio = dt.Transaccion.fecha_inicio,
                     SerieComprobante = dt.Transaccion.Comprobante.numero_serie,
                     NumeroComprobante = dt.Transaccion.Comprobante.numero,
                     Concepto = dt.Concepto_negocio.nombre,
                     Cantidad = dt.cantidad,
                     UnidadMedida = dt.Concepto_negocio.Detalle_maestro2.codigo
                 }).ToList();
                return Items;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener items ", e);
            }
        }
        
        public IEnumerable<ResumenDeTransaccionGeneral> ObtenerResumenDeTransacciones(int[] idsTiposTransaccion, int idEstadoActual, DateTime fechaDesde, DateTime fechaHasta, int idCentroAtencion)
        {
            try
            {
                var _db = new SigescomEntities();
                return _db.Transaccion.Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && idsTiposTransaccion.Contains(t.id_tipo_transaccion) && t.id_actor_negocio_interno == idCentroAtencion).Select(t => new ResumenDeTransaccionGeneral()
                {
                    Id = t.id,
                    ActorNegocioExterno = t.Actor_negocio1.Actor.primer_nombre.Replace("|", " "),
                    Comprobante = t.Comprobante.numero_serie +"-"+ t.Comprobante.numero,
                    ComprobanteOperacionReferencia= t.Transaccion3.Comprobante.numero_serie + "-"+t.Transaccion3.Comprobante.numero,
                    Empleado= t.Actor_negocio.Actor.segundo_nombre,
                    Fecha= t.fecha_inicio,
                    FechaOperacionReferencia= t.Transaccion3.fecha_inicio,
                    ImporteTotal= t.importe_total,
                    Observacion= t.comentario
                }) ;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public IEnumerable<ResumenDeTransaccionGeneral> ObtenerResumenDeTransacciones(int[] idsTiposTransaccion, int idEstadoActual, DateTime fechaDesde, DateTime fechaHasta, int idCentroAtencion, int idParametroParaTipoOperacion)
        {
            try
            {
                var _db = new SigescomEntities();
                var resultado= _db.Transaccion.Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && idsTiposTransaccion.Contains(t.id_tipo_transaccion) && t.id_actor_negocio_interno == idCentroAtencion).Select(t => new ResumenDeTransaccionGeneral()
                {
                    Id = t.id,
                    ActorNegocioExterno = t.Actor_negocio1.Actor.primer_nombre.Replace("|", " "),
                    Comprobante = t.Comprobante.numero_serie + "-" + t.Comprobante.numero,
                    ComprobanteOperacionReferencia = t.Transaccion3.Comprobante.numero_serie + "-" + t.Transaccion3.Comprobante.numero,
                    Empleado = t.Actor_negocio.Actor.segundo_nombre,
                    Fecha = t.fecha_inicio,
                    FechaOperacionReferencia = t.Transaccion3.fecha_inicio,
                    ImporteTotal = t.importe_total,
                    Observacion = t.comentario,
                    CodigoTipoOperacionSunat= t.Parametro_transaccion.FirstOrDefault(pt=> pt.id_parametro== idParametroParaTipoOperacion).valor
                    
                });

                return resultado;
                
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #region REPORTE DE CAJA
        public IEnumerable<Movimiento_Caja> ObtenerMovimientoDeCaja(int idAccionDeNegocioMovimientoEnCaja, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                var Resumen = _db.Accion_de_negocio_por_tipo_transaccion
                                .Where(antt => antt.id_accion_de_negocio == idAccionDeNegocioMovimientoEnCaja)
                                .SelectMany(antt => antt.Tipo_transaccion.Transaccion).Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                    .Select(t => new Movimiento_Caja()
                    {
                        IdConcepto = t.id_tipo_transaccion,
                        Fecha = t.fecha_inicio,
                        IdCaja = t.id_actor_negocio_interno,
                        NombreCaja = t.Actor_negocio2.Actor.primer_nombre,
                        Concepto = t.Tipo_transaccion.nombre,
                        CodigoTipoComprobante = t.Comprobante.Detalle_maestro.valor,
                        SerieYNumeroComprobante = t.Comprobante.numero_serie + "-" + t.Comprobante.numero,
                        Monto = t.importe_total,
                        EsIngreso = t.Tipo_transaccion.Accion_de_negocio_por_tipo_transaccion.FirstOrDefault(anpt => anpt.id_accion_de_negocio == idAccionDeNegocioMovimientoEnCaja).valor
                    });
                return Resumen;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener reprote de utilidad", e);
            }
        }
        public IEnumerable<Movimiento_Caja> ObtenerMovimientoDeCaja(int idAccionDeNegocioMovimientoEnCaja, int idCentroAtencion, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                var Resumen = _db.Accion_de_negocio_por_tipo_transaccion
                                .Where(antt => antt.id_accion_de_negocio == idAccionDeNegocioMovimientoEnCaja)
                                .SelectMany(antt => antt.Tipo_transaccion.Transaccion).Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && t.id_actor_negocio_interno == idCentroAtencion && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                   .Select(t => new Movimiento_Caja()
                   {
                       IdConcepto = t.id_tipo_transaccion,
                       Fecha = t.fecha_inicio,
                       IdCaja = t.id_actor_negocio_interno,
                       NombreCaja = t.Actor_negocio2.Actor.primer_nombre,
                       Concepto = t.Tipo_transaccion.nombre,
                       CodigoTipoComprobante = t.Comprobante.Detalle_maestro.valor,
                       SerieYNumeroComprobante = t.Comprobante.numero_serie + "-" + t.Comprobante.numero,
                       Monto = t.importe_total,
                       EsIngreso = t.Tipo_transaccion.Accion_de_negocio_por_tipo_transaccion.FirstOrDefault(anpt => anpt.id_accion_de_negocio == idAccionDeNegocioMovimientoEnCaja).valor
                   });
                return Resumen;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener reprote de utilidad", e);
            }
        }
        public IEnumerable<Movimiento_Caja> ObtenerMovimientoDeCaja(int idAccionDeNegocioMovimientoEnCaja, int[] idsCentroAtencion, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                var Resumen = _db.Accion_de_negocio_por_tipo_transaccion
                                .Where(antt => antt.id_accion_de_negocio == idAccionDeNegocioMovimientoEnCaja)
                                .SelectMany(antt => antt.Tipo_transaccion.Transaccion).Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && idsCentroAtencion.Contains(t.id_actor_negocio_interno) && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                    .Select(t => new Movimiento_Caja()
                    {
                        IdConcepto = t.id_tipo_transaccion,
                        Fecha = t.fecha_inicio,
                        IdCaja = t.id_actor_negocio_interno,
                        NombreCaja = t.Actor_negocio2.Actor.primer_nombre,
                        Concepto = t.Tipo_transaccion.nombre,
                        CodigoTipoComprobante = t.Comprobante.Detalle_maestro.valor,
                        SerieYNumeroComprobante = t.Comprobante.numero_serie + "-" + t.Comprobante.numero,
                        Monto = t.importe_total,
                        EsIngreso = t.Tipo_transaccion.Accion_de_negocio_por_tipo_transaccion.FirstOrDefault(anpt => anpt.id_accion_de_negocio == idAccionDeNegocioMovimientoEnCaja).valor
                    });
                return Resumen;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener reprote de utilidad", e);
            }
        }

        #endregion
        public IEnumerable<DetalleCuotaPago> ObtenerDetallesCuotaPagoDeOperacion(long idOperacion)
        {
            try
            {
                return _db.Cuota.Where(c => c.id_transaccion == idOperacion)
                    .Select(t => new DetalleCuotaPago()
                    {
                        Id = t.id,
                        IdOperacion = t.id_transaccion,
                        Codigo = t.codigo,
                        Total = t.total,
                        Pagado = t.pago_a_cuenta,
                        Revocado = t.revocado,
                        Saldo = t.saldo,
                    });
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener reprote de utilidad", e);
            }
        }




    }
}
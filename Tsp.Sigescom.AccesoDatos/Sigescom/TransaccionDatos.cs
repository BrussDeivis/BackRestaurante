using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Tsp.Sigescom.AccesoDatos.Generico;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Venta;
using System.Data.Entity.SqlServer;

namespace Tsp.Sigescom.AccesoDatos
{
    public partial class TransaccionDatos : RepositorioBase, ITransaccionRepositorio
    {

        public Transaccion_consolidada ObtenerResumenTransaccionesDespuesDe(int idTipoTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta, int idSerie, long idMinimo)
        {
            try
            {
                var Resumen = _db.Transaccion.Where(sbt => sbt.id_tipo_transaccion == idTipoTransaccion
                && sbt.Comprobante.id_serie_comprobante == idSerie
                && sbt.fecha_inicio >= fechaDesde && sbt.fecha_inicio <= fechaHasta
                && sbt.id_estado_actual == idUltimoEstado
                && sbt.id > idMinimo)
                .SelectMany(t => t.Detalle_transaccion).
              GroupBy(dt => new
              {
                  Fecha = new { y = dt.Transaccion.fecha_inicio.Year, m = dt.Transaccion.fecha_inicio.Month, d = dt.Transaccion.fecha_inicio.Day },
                  //dt.Transaccion.fecha_inicio.Date,
                  dt.Transaccion.Comprobante.id_serie_comprobante,
                  dt.Transaccion.Comprobante.numero_serie

              }).Select(dt => new Transaccion_consolidada()
              {
                  Anyo = dt.Key.Fecha.y,
                  Mes = dt.Key.Fecha.m,
                  Dia = dt.Key.Fecha.d,
                  IdSerie = dt.Key.id_serie_comprobante,
                  Serie = dt.Key.numero_serie,
                  NumeroInicial = (int)dt.Min(m => m.Transaccion.Comprobante.numero),
                  NumeroFinal = (int)dt.Max(m => m.Transaccion.Comprobante.numero),
                  IGV = dt.Sum(c => (decimal)c.igv),
                  Total = dt.Sum(c => c.total),
                  UltimoId = dt.Max(c => c.id_transaccion)
              }).SingleOrDefault();
                return Resumen;

            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener resumen de transacciones", e);
            }
        }
        public OperationResult CrearTransaccionYModificarActorDeNegocio(Transaccion orden, Actor_negocio updmesa)
        {
            try
            {
                OperationResult result = new OperationResult();
                _db.Transaccion.Add(orden);
                Actor_negocio dbMesa = _db.Actor_negocio.SingleOrDefault(m => m.id == updmesa.id);
                _db.Entry(dbMesa).CurrentValues.SetValues(updmesa);
                result = Save();
                result.data = (long)orden.id_transaccion_padre;
                result.message = orden.id.ToString();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public OperationResult CrearTransaccionYCrearActorDeNegocio(Transaccion orden, Actor_negocio mesa)
        {
            try
            {
                OperationResult result = new OperationResult();
                _db.Transaccion.Add(orden);
                _db.Actor_negocio.Add(mesa);
                result = Save();
                result.data = (long)orden.id_transaccion_padre;
                result.message = orden.id.ToString();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Transaccion_consolidada ObtenerResumenTransaccionesEntre(int idTipoTransaccion, int idUltimoEstado, DateTime fechaInicio, DateTime fechaFin, int idSerie, long idMinimo, long idMaximo)
        {
            try
            {
                var Resumen = _db.Transaccion.Where(sbt => sbt.id_tipo_transaccion == idTipoTransaccion
                && sbt.Comprobante.id_serie_comprobante == idSerie
                && sbt.fecha_inicio >= fechaInicio && sbt.fecha_inicio <= fechaFin
                && sbt.id_estado_actual == idUltimoEstado
                && sbt.id > idMinimo && sbt.id < idMaximo)
                .SelectMany(t => t.Detalle_transaccion).
              GroupBy(dt => new
              {
                  Fecha = new { y = dt.Transaccion.fecha_inicio.Year, m = dt.Transaccion.fecha_inicio.Month, d = dt.Transaccion.fecha_inicio.Day },
                  //dt.Transaccion.fecha_inicio.Date,
                  CodigoComprobante = dt.Transaccion.Comprobante.Detalle_maestro.codigo,
                  dt.Transaccion.Comprobante.id_serie_comprobante,
                  dt.Transaccion.Comprobante.numero_serie

              }).Select(dt => new Transaccion_consolidada()
              {
                  Anyo = dt.Key.Fecha.y,
                  Mes = dt.Key.Fecha.m,
                  Dia = dt.Key.Fecha.d,
                  CodigoTipoComprobante = dt.Key.CodigoComprobante,
                  IdSerie = dt.Key.id_serie_comprobante,
                  Serie = dt.Key.numero_serie,
                  NumeroInicial = (int)dt.Min(m => m.Transaccion.Comprobante.numero),
                  NumeroFinal = (int)dt.Max(m => m.Transaccion.Comprobante.numero),
                  IGV = dt.Sum(c => (decimal)c.igv),
                  Total = dt.Sum(c => c.total),
                  UltimoId = dt.Max(c => c.id_transaccion)
              }).SingleOrDefault();
                return Resumen;

            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener resumen de transacciones", e);

            }
        }

        public long ObtenerIdPrimeraTransaccion(int[] idsTipoTransaccion, int[] idsTiposComprobantes, int idEstadoActualSiExiste, int idEstadoAnteriorOActual, DateTime fechaEmisionDesde, DateTime fechaEmisionHasta, int idSerie)
        {
            try
            {
                var resultado = _db.Transaccion
                    .Where(t =>
                    idsTipoTransaccion.Contains(t.id_tipo_transaccion)
                    && t.Comprobante.id_serie_comprobante == idSerie
                    && t.fecha_inicio >= fechaEmisionDesde
                    && t.fecha_inicio <= fechaEmisionHasta
                    ////si el ultimo estado es idEstadoAnteriorOActual
                    //&& ((t.Estado_transaccion.OrderByDescending(est => est.id).FirstOrDefault().id_estado == idEstadoAnteriorOActual)
                    ////si el ultimo estado es idEstadoActualSiExiste => el estado actual es el indicado por parametro
                    //|| (t.Estado_transaccion.OrderByDescending(est => est.id).FirstOrDefault().id_estado == idEstadoActualSiExiste
                    ////Y ademas el penultimo estado es el indicado por parametro idEstadoAnterior
                    //&& t.Estado_transaccion.OrderByDescending(est => est.id).Skip(1).Take(1).Select(e => e.id_estado).Contains(idEstadoAnteriorOActual)))
                    ).Min(t => t.id_comprobante);
                return resultado != null ? resultado : 0;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener id de la primera transaccion", e);
            }
        }

        public long ObtenerIdUltimaTransaccion(int[] idsTipoTransaccion, int[] idsTiposComprobantes, int idEstadoActualSiExiste, int idEstadoAnteriorOActual, DateTime fechaEmisionDesde, DateTime fechaEmisionHasta, int idSerie)
        {
            try
            {
                var resultado = _db.Transaccion
                     .Where(t =>
                     idsTipoTransaccion.Contains(t.id_tipo_transaccion)
                     && t.Comprobante.id_serie_comprobante == idSerie
                     && t.fecha_inicio >= fechaEmisionDesde
                     && t.fecha_inicio <= fechaEmisionHasta
                     ////si el ultimo estado es idEstadoAnteriorOActual
                     //&& ((t.Estado_transaccion.OrderByDescending(est => est.id).FirstOrDefault().id_estado == idEstadoAnteriorOActual)
                     ////si el ultimo estado es idEstadoActualSiExiste => el estado actual es el indicado por parametro
                     //|| (t.Estado_transaccion.OrderByDescending(est => est.id).FirstOrDefault().id_estado == idEstadoActualSiExiste
                     ////Y ademas el penultimo estado es el indicado por parametro idEstadoAnterior
                     //&& t.Estado_transaccion.OrderByDescending(est => est.id).Skip(1).Take(1).Select(e => e.id_estado).Contains(idEstadoAnteriorOActual)))
                     ).Max(t => t.id_comprobante);
                return resultado != null ? Convert.ToInt64(resultado) : 0;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener id de la ultima transaccion", e);
            }
        }

        #region OBTENER PRIMER Y ULTIMO NUMERO DE COMPROBANTE

        public long ObtenerNumeroDeComprobantePrimeraTransaccion(int[] idsTipoTransaccion, int[] idsTiposComprobantes, int idUltimoEstado, DateTime fechaEmisionDesde, DateTime fechaEmisionHasta, int idSerie)
        {
            try
            {
                var resultado = _db.Transaccion
                    .Where(t =>
                    idsTipoTransaccion.Contains(t.id_tipo_transaccion)
                    && t.Comprobante.id_serie_comprobante == idSerie
                    && t.fecha_inicio >= fechaEmisionDesde
                    && t.fecha_inicio <= fechaEmisionHasta
                    && t.id_estado_actual == idUltimoEstado
                    ).OrderBy(t => t.Comprobante.numero).FirstOrDefault();
                return resultado != null ? resultado.Comprobante.numero : 0;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener el numero de comprobante de la primera transaccion", e);
            }
        }

        public long ObtenerNumeroDeComprobanteUltimaTransaccion(int[] idsTipoTransaccion, int[] idsTiposComprobantes, int idUltimoEstado, DateTime fechaEmisionDesde, DateTime fechaEmisionHasta, int idSerie)
        {
            try
            {
                var resultado = _db.Transaccion
                     .Where(t =>
                     idsTipoTransaccion.Contains(t.id_tipo_transaccion)
                     && t.Comprobante.id_serie_comprobante == idSerie
                     && t.fecha_inicio >= fechaEmisionDesde
                     && t.fecha_inicio <= fechaEmisionHasta
                     && t.id_estado_actual == idUltimoEstado
                    ).OrderByDescending(t => t.Comprobante.numero).FirstOrDefault();
                return resultado != null ? resultado.Comprobante.numero : 0;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener el numero de comprobante de la ultima transaccion", e);
            }
        }

        #endregion

        #region LIBROS ELECTRONICOS

        public IEnumerable<Transaccion_consolidada> ObtenerTransaccionesConsolidadasEntreNumeroDeComprobante(int idTipoTransaccion, int idUltimoEstado, int idSerie, long numeroMinimo, long numeroMaximo)
        {
            try
            {
                var transacciones = _db.Transaccion.Where(t =>
                 t.id_tipo_transaccion == idTipoTransaccion
                && t.Comprobante.id_serie_comprobante == idSerie
                && t.Comprobante.numero > numeroMinimo && t.Comprobante.numero < numeroMaximo
                && t.id_estado_actual == idUltimoEstado)
                    .Select(t => new
                    {
                        Anyo = t.fecha_inicio.Year,
                        Mes = t.fecha_inicio.Month,
                        Dia = t.fecha_inicio.Day,
                        IdComprobante = t.Comprobante.id,
                        CodigoTipoComprobante = t.Comprobante.Detalle_maestro.codigo,
                        IdSerie = t.Comprobante.id_serie_comprobante,
                        Serie = t.Comprobante.numero_serie,
                        NumeroDeComprobante = t.Comprobante.numero,
                        IGV = t.Detalle_transaccion.Sum(tt => (decimal)tt.igv),
                        Total = t.importe_total,
                        CodigoMoneda = t.Detalle_maestro1.codigo,
                        TipoCambio = t.tipo_cambio
                        //Icbpers = t.Parametro_transaccion.Where(pt => pt.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroIcbper).Select(pt => pt.valor),
                    }).ToList();

                var resumen = transacciones
                    .GroupBy(g => new
                    {
                        Fecha = new { y = g.Anyo, m = g.Mes, d = g.Dia },
                        g.CodigoTipoComprobante,
                        g.IdSerie,
                        g.Serie,
                        g.CodigoMoneda
                    }).Select(t => new Transaccion_consolidada()
                    {
                        Anyo = t.Key.Fecha.y,
                        Mes = t.Key.Fecha.m,
                        Dia = t.Key.Fecha.d,
                        CodigoTipoComprobante = t.Key.CodigoTipoComprobante,
                        IdSerie = t.Key.IdSerie,
                        Serie = t.Key.Serie,
                        NumeroInicial = (int)t.Min(m => m.NumeroDeComprobante),
                        NumeroFinal = (int)t.Max(m => m.NumeroDeComprobante),
                        IGV = t.Sum(tt => (decimal)tt.IGV),
                        Total = t.Sum(tt => tt.Total),
                        CodigoMoneda = t.Key.CodigoMoneda,
                        TipoCambio = t.FirstOrDefault().TipoCambio,
                        UltimoId = t.Max(tt => tt.IdComprobante),
                        //Icbpers = t.SelectMany(pt => pt.Icbpers)

                    }).ToList().OrderBy(t => t.FechaEmision).ThenBy(t => t.Serie);

                return resumen;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener resumen de transacciones", e);
            }

            /*decimal)dt.Select(p => p.Transaccion.Parametro_transaccion.FirstOrDefault(pt => pt.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroIcbper)).Sum(pt => Convert.ToDecimal(pt.valor))
             
             */
        }

        public IEnumerable<Transaccion_consolidada> ObtenerTransaccionesConsolidadasEntreNumeroDeComprobantefechaHasta(int idTipoTransaccion, int idUltimoEstado, int idSerie, long numeroMinimo, long numeroMaximo, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                var transacciones = _db.Transaccion.Where(t =>
                t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta
                && t.id_tipo_transaccion == idTipoTransaccion
                && t.Comprobante.id_serie_comprobante == idSerie
                && t.Comprobante.numero > numeroMinimo && t.Comprobante.numero < numeroMaximo
                && t.id_estado_actual == idUltimoEstado)
                    .Select(t => new
                    {
                        Anyo = t.fecha_inicio.Year,
                        Mes = t.fecha_inicio.Month,
                        Dia = t.fecha_inicio.Day,
                        IdComprobante = t.Comprobante.id,
                        CodigoTipoComprobante = t.Comprobante.Detalle_maestro.codigo,
                        IdSerie = t.Comprobante.id_serie_comprobante,
                        Serie = t.Comprobante.numero_serie,
                        NumeroDeComprobante = t.Comprobante.numero,
                        IGV = t.Detalle_transaccion.Sum(tt => (decimal)tt.igv),
                        Total = t.importe_total,
                        CodigoMoneda = t.Detalle_maestro1.codigo,
                        TipoCambio = t.tipo_cambio
                        //Icbpers = t.Parametro_transaccion.Where(pt => pt.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroIcbper).Select(pt => pt.valor),
                    }).ToList();

                var resumen = transacciones
                    .GroupBy(g => new
                    {
                        Fecha = new { y = g.Anyo, m = g.Mes, d = g.Dia },
                        g.CodigoTipoComprobante,
                        g.IdSerie,
                        g.Serie,
                        g.CodigoMoneda
                    }).Select(t => new Transaccion_consolidada()
                    {
                        Anyo = t.Key.Fecha.y,
                        Mes = t.Key.Fecha.m,
                        Dia = t.Key.Fecha.d,
                        CodigoTipoComprobante = t.Key.CodigoTipoComprobante,
                        IdSerie = t.Key.IdSerie,
                        Serie = t.Key.Serie,
                        NumeroInicial = (int)t.Min(m => m.NumeroDeComprobante),
                        NumeroFinal = (int)t.Max(m => m.NumeroDeComprobante),
                        IGV = t.Sum(tt => (decimal)tt.IGV),
                        Total = t.Sum(tt => tt.Total),
                        CodigoMoneda = t.Key.CodigoMoneda,
                        TipoCambio = t.FirstOrDefault().TipoCambio,
                        UltimoId = t.Max(tt => tt.IdComprobante),
                        //Icbpers = t.SelectMany(pt => pt.Icbpers)

                    }).ToList().OrderBy(t => t.FechaEmision).ThenBy(t => t.Serie);

                return resumen;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener resumen de transacciones", e);
            }

            /*decimal)dt.Select(p => p.Transaccion.Parametro_transaccion.FirstOrDefault(pt => pt.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroIcbper)).Sum(pt => Convert.ToDecimal(pt.valor))
             
             */
        }

        #endregion

        public IEnumerable<Transaccion_consolidada> ObtenerTransaccionesConsolidadasEntre(int idTipoTransaccion, int idUltimoEstado, int idSerie, long idMinimo, long idMaximo)
        {
            try
            {

                var Resumen = _db.Transaccion.Where(sbt =>
                 sbt.id_tipo_transaccion == idTipoTransaccion
                && sbt.Comprobante.id_serie_comprobante == idSerie
                && sbt.id_comprobante > idMinimo && sbt.id_comprobante < idMaximo
                && sbt.id_estado_actual == idUltimoEstado
                ).SelectMany(t => t.Detalle_transaccion).
              GroupBy(dt => new
              {
                  Fecha = new { y = dt.Transaccion.fecha_inicio.Year, m = dt.Transaccion.fecha_inicio.Month, d = dt.Transaccion.fecha_inicio.Day },
                  //dt.Transaccion.fecha_inicio.Date,
                  CodigoComprobante = dt.Transaccion.Comprobante.Detalle_maestro.codigo,
                  dt.Transaccion.Comprobante.id_serie_comprobante,
                  dt.Transaccion.Comprobante.numero_serie
                  //,CodigoMoneda= dt.Transaccion.Detalle_maestro.codigo

              }).Select(dt => new Transaccion_consolidada()
              {
                  Anyo = dt.Key.Fecha.y,
                  Mes = dt.Key.Fecha.m,
                  Dia = dt.Key.Fecha.d,
                  CodigoTipoComprobante = dt.Key.CodigoComprobante,
                  IdSerie = dt.Key.id_serie_comprobante,
                  Serie = dt.Key.numero_serie,
                  NumeroInicial = (int)dt.Min(m => m.Transaccion.Comprobante.numero),
                  NumeroFinal = (int)dt.Max(m => m.Transaccion.Comprobante.numero),
                  IGV = dt.Sum(c => (decimal)c.igv),
                  Total = dt.Sum(c => c.total),
                  //CodigoMoneda = dt.Key.CodigoMoneda,
                  UltimoId = dt.Max(c => c.Transaccion.id_comprobante)
              }).ToList().OrderBy(t => t.FechaEmision).ThenBy(t => t.Serie);
                return Resumen;

            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener resumen de transacciones", e);

            }
        }

        public Transaccion_consolidada ObtenerResumenTransaccionesAntesDe(int idTipoTransaccion, int idUltimoEstado, DateTime fechaInicio, DateTime fechaFin, int idSerie, long idMaximo)
        {
            try
            {
                var Resumen = _db.Transaccion.Where(sbt => sbt.id_tipo_transaccion == idTipoTransaccion && sbt.Comprobante.id_serie_comprobante == idSerie
                && sbt.fecha_inicio >= fechaInicio && sbt.fecha_inicio <= fechaFin
                && sbt.id_estado_actual == idUltimoEstado
                && sbt.id < idMaximo)

                .SelectMany(t => t.Detalle_transaccion).
              GroupBy(dt => new
              {
                  Fecha = new { y = dt.Transaccion.fecha_inicio.Year, m = dt.Transaccion.fecha_inicio.Month, d = dt.Transaccion.fecha_inicio.Day },
                  CodigoComprobante = dt.Transaccion.Comprobante.Detalle_maestro.codigo,
                  dt.Transaccion.Comprobante.id_serie_comprobante,
                  dt.Transaccion.Comprobante.numero_serie

              }).Select(dt => new Transaccion_consolidada()
              {
                  Anyo = dt.Key.Fecha.y,
                  Mes = dt.Key.Fecha.m,
                  Dia = dt.Key.Fecha.d,
                  CodigoTipoComprobante = dt.Key.CodigoComprobante,
                  IdSerie = dt.Key.id_serie_comprobante,
                  Serie = dt.Key.numero_serie,
                  NumeroInicial = (int)dt.Min(m => m.Transaccion.Comprobante.numero),
                  NumeroFinal = (int)dt.Max(m => m.Transaccion.Comprobante.numero),
                  IGV = dt.Sum(c => (decimal)c.igv),
                  Total = dt.Sum(c => c.total),
                  UltimoId = dt.Max(c => c.id_transaccion)
              }).SingleOrDefault();
                return Resumen;

            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener resumen de transacciones", e);
            }
        }

        public Transaccion_consolidada ObtenerResumenTransacciones(int idTipoTransaccion, int idUltimoEstado, DateTime fechaInicio, DateTime fechaFin, int idSerie)
        {
            try
            {
                var Resumen = _db.Transaccion
                    .Where(t =>
                    t.fecha_inicio >= fechaInicio && t.fecha_inicio <= fechaFin
                    && t.id_tipo_transaccion == idTipoTransaccion
                    && t.Comprobante.id_serie_comprobante == idSerie
                    && t.id_estado_actual == idUltimoEstado
                )
                .
              GroupBy(dt => new
              {
                  Fecha = new { y = dt.fecha_inicio.Year, m = dt.fecha_inicio.Month, d = dt.fecha_inicio.Day },
                  CodigoComprobante = dt.Comprobante.Detalle_maestro.codigo,
                  dt.Comprobante.id_serie_comprobante,
                  dt.Comprobante.numero_serie

              }).Select(dt => new Transaccion_consolidada()
              {
                  Anyo = dt.Key.Fecha.y,
                  Mes = dt.Key.Fecha.m,
                  Dia = dt.Key.Fecha.d,
                  CodigoTipoComprobante = dt.Key.CodigoComprobante,

                  IdSerie = dt.Key.id_serie_comprobante,
                  Serie = dt.Key.numero_serie,
                  NumeroInicial = (int)dt.Min(m => m.Comprobante.numero),
                  NumeroFinal = (int)dt.Max(m => m.Comprobante.numero),
                  IGV = dt.Sum(c => (decimal)(c.Detalle_transaccion.Sum(_dt => _dt.igv))),
                  Total = dt.Sum(c => c.importe_total),
                  UltimoId = dt.Max(c => c.id)
              });


                return Resumen.SingleOrDefault();

            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener resumen de transacciones", e);
            }
        }

        public IEnumerable<Transaccion_consolidada> ObtenerResumenTransacciones(int idTipoTransaccion, int idTipoComprobante, int idUltimoEstado, long idMinimo, long idMaximo)
        {
            try
            {
                var Resumen = _db.Transaccion.Where(sbt => sbt.id_tipo_transaccion == idTipoTransaccion && sbt.Comprobante.id_tipo_comprobante == idTipoComprobante &&
                sbt.id_estado_actual == idUltimoEstado && sbt.id > idMinimo && sbt.id < idMaximo).SelectMany(t => t.Detalle_transaccion).
              GroupBy(dt => new
              {
                  Fecha = new { y = dt.Transaccion.fecha_inicio.Year, m = dt.Transaccion.fecha_inicio.Month, d = dt.Transaccion.fecha_inicio.Day },
                  CodigoComprobante = dt.Transaccion.Comprobante.Detalle_maestro.codigo,
                  dt.Transaccion.Comprobante.id_serie_comprobante,
                  dt.Transaccion.Comprobante.numero_serie,

              }).Select(dt => new Transaccion_consolidada()
              {
                  Anyo = dt.Key.Fecha.y,
                  Mes = dt.Key.Fecha.m,
                  Dia = dt.Key.Fecha.d,
                  CodigoTipoComprobante = dt.Key.CodigoComprobante,
                  IdSerie = dt.Key.id_serie_comprobante,
                  Serie = dt.Key.numero_serie,
                  NumeroInicial = (int)dt.Min(m => m.Transaccion.Comprobante.numero),
                  NumeroFinal = (int)dt.Max(m => m.Transaccion.Comprobante.numero),
                  IGV = dt.Sum(c => (decimal)c.igv),
                  Total = dt.Sum(c => c.total),
                  UltimoId = dt.Max(c => c.id_transaccion)
              });
                return Resumen;

            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener resumen de transacciones", e);

            }
        }

        public IEnumerable<Transaccion_consolidada> ObtenerResumenTransacciones(int idTipoTransaccion, int idTipoComprobante, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                var Resumen = _db.Transaccion.Where(sbt => sbt.id_tipo_transaccion == idTipoTransaccion && sbt.Comprobante.id_tipo_comprobante == idTipoComprobante &&
                sbt.id_estado_actual == idUltimoEstado && sbt.fecha_inicio >= fechaDesde && sbt.fecha_inicio <= fechaHasta).SelectMany(t => t.Detalle_transaccion).
              GroupBy(dt => new
              {
                  Fecha = new { y = dt.Transaccion.fecha_inicio.Year, m = dt.Transaccion.fecha_inicio.Month, d = dt.Transaccion.fecha_inicio.Day },
                  CodigoComprobante = dt.Transaccion.Comprobante.Detalle_maestro.codigo,
                  dt.Transaccion.Comprobante.id_serie_comprobante,
                  dt.Transaccion.Comprobante.numero_serie

              }).Select(dt => new Transaccion_consolidada()
              {
                  Anyo = dt.Key.Fecha.y,
                  Mes = dt.Key.Fecha.m,
                  Dia = dt.Key.Fecha.d,
                  CodigoTipoComprobante = dt.Key.CodigoComprobante,
                  IdSerie = dt.Key.id_serie_comprobante,
                  Serie = dt.Key.numero_serie,
                  NumeroInicial = (int)dt.Min(m => m.Transaccion.Comprobante.numero),
                  NumeroFinal = (int)dt.Max(m => m.Transaccion.Comprobante.numero),
                  IGV = dt.Sum(c => (decimal)c.igv),
                  Total = dt.Sum(c => c.total),
                  UltimoId = dt.Max(c => c.id_transaccion)
              });
                return Resumen;

            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener resumen de transacciones", e);
            }
        }

        public IEnumerable<Transaccion> ObtenerTransacciones(long[] idsTransacciones)
        {
            try
            {
                return _db.Transaccion.Where(st => idsTransacciones.Contains(st.id)).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Transaccion> ObtenerTransacciones(long[] idsTransaccion, int idTipoTransaccion)
        {
            try
            {
                return _db.Transaccion.
                Include(t => t.Detalle_transaccion).
                Include(t => t.Comprobante).
                Include(t => t.Transaccion1).
                Include(t => t.Detalle_transaccion.Select(dst => dst.Cuenta_contable)).
                Where(t => idsTransaccion.Contains(t.id) && t.id_tipo_transaccion == idTipoTransaccion);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public IEnumerable<Transaccion> ObtenerTransacciones(int idTipoTransaccion, int idUltimoEstado)
        {
            try
            {
                return _db.Transaccion
                                   .Where(t => t.id_tipo_transaccion == idTipoTransaccion
                                               && t.id_estado_actual == idUltimoEstado
                                   );

            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones", e);
            }
        }



        public Transaccion ObtenerUltimaTransaccion(int idTipoTransaccion)
        {
            try
            {
                return _db.Transaccion.Where(t => t.id_tipo_transaccion == idTipoTransaccion).OrderByDescending(t => t.id).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


         


        public Transaccion ObtenerUltimaTransaccion(int idActorNegocioInterno, int idTipoTransaccion)
        {
            try
            {
                return _db.Transaccion
                          .Where(t => t.id_actor_negocio_interno == idActorNegocioInterno && t.id_tipo_transaccion == idTipoTransaccion)
                          .OrderByDescending(t => t.id).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public long[] ObtenerIdsDeTransaccionesPosteriores(int idActorNegocioInterno, int idTipoTransaccion, DateTime fecha)
        {
            try
            {
                return _db.Transaccion.Where(t => t.id_actor_negocio_interno == idActorNegocioInterno && t.id_tipo_transaccion == idTipoTransaccion && t.fecha_inicio > fecha).Select(t => t.id).ToArray();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Transaccion ObtenerUltimaTransaccionAntesDe(int idTipoTransaccion, DateTime fechaInicio)
        {
            try
            {
                return _db.Transaccion.Include(t => t.Detalle_transaccion)
                          .Where(t => t.id_tipo_transaccion == idTipoTransaccion && t.fecha_inicio <= fechaInicio)
                          .OrderByDescending(t => t.id).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Transaccion ObtenerUltimaTransaccionAntesDe(int idActorNegocioInterno, int idTipoTransaccion, DateTime fechaInicio)
        {
            try
            {
                return _db.Transaccion.Include(t => t.Detalle_transaccion)
                          .Where(t => t.id_actor_negocio_interno == idActorNegocioInterno && t.id_tipo_transaccion == idTipoTransaccion && t.fecha_inicio <= fechaInicio)
                          .OrderByDescending(t => t.id).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Retorna un detalle transaccion segun el id concepto de negocio
        /// </summary>
        /// <param name="idActorNegocioInterno"></param>
        /// <param name="idTipoTransaccion"></param>
        /// <param name="idConceptoNegocio"></param>
        /// <returns></returns>
        public Detalle_transaccion ObtenerDetalleTransaccionPorConceptoNegocio(int idActorNegocioInterno, int idTipoTransaccion, int idConceptoNegocio)
        {
            try
            {
                return _db.Transaccion.Where(t => t.id_tipo_transaccion == idTipoTransaccion && t.id_actor_negocio_interno == idActorNegocioInterno)
                                      .OrderByDescending(t => t.id).SelectMany(t => t.Detalle_transaccion)
                                      .Where(dt => dt.id_concepto_negocio == idConceptoNegocio).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener detalle transaccion", e);
            }
        }

        public Transaccion ObtenerPrimeraTransaccion(int idActorNegocio, int idTipoTransaccion)
        {
            try
            {
                return _db.Transaccion.Include(t => t.Detalle_transaccion)
                          .Where(t => t.id_actor_negocio_externo == idActorNegocio && t.id_tipo_transaccion == idTipoTransaccion).OrderByDescending(t => t.id).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Tipo_cambio ObtenerTipoDeCambio(DateTime fecha)
        {
            try
            {
                var tipoCambio = _db.Tipo_cambio.SingleOrDefault(tc => tc.fecha == fecha);
                var tipoCambioOpcional = _db.Tipo_cambio.Where(tc => tc.fecha < fecha).OrderByDescending(tc => tc.id).First();
                return tipoCambio != null ? tipoCambio : tipoCambioOpcional;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Cuenta_Cobrar_Pagar> ObtenerCuentasPorCobrarOPagar(bool porCobrar)
        {
            try
            {
                var cuotas = _db.Cuota.
                   Where(c => c.por_cobrar == porCobrar 
                   && c.saldo > 0).
                   Select(c => new Cuenta_Cobrar_Pagar()
                   {
                       IdCuota = c.id,
                       CodigoCuota = c.codigo,
                       IdTransaccion = c.id_transaccion,
                       IdTipoTransaccion = c.Transaccion.id_tipo_transaccion,
                       TipoTransaccion = c.Transaccion.Transaccion2.Tipo_transaccion.nombre,
                       IdActorComercial = c.Transaccion.id_actor_negocio_externo,
                       CodigoTipoDocumentoActorComercial = c.Transaccion.Actor_negocio1.Actor.Detalle_maestro.valor,
                       NumeroDocumentoActorComercial = c.Transaccion.Actor_negocio1.Actor.numero_documento_identidad,
                       ActorComercial = c.Transaccion.Actor_negocio1.Actor.primer_nombre.Replace("|", " "),
                       NombreGrupo = c.Transaccion.Actor_negocio3.Actor.primer_nombre,
                       CodigoTipoComprobante = c.Transaccion.Comprobante.Detalle_maestro.valor,
                       TipoComprobante = c.Transaccion.Comprobante.Detalle_maestro.nombre,
                       SerieComprobante = c.Transaccion.Comprobante.numero_serie,
                       NumeroComprobante = c.Transaccion.Comprobante.numero,
                       FechaInicio = c.fecha_emision,
                       FechaFin = c.fecha_vencimiento,
                       PagoACuenta = c.pago_a_cuenta,
                       Total = c.total,
                       Revocado = c.revocado,
                       Saldo = c.saldo
                   }).OrderByDescending(c => c.IdCuota);
                return cuotas;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener cuotas por cobrar o pagar", e);
            }
        }

        public IEnumerable<Cuenta_Cobrar_Pagar> ObtenerCuentasPorCobrarOPagarPorGrupos(bool porCobrar, int?[] idsGrupos)
        {
            try
            {
                var cuotas = _db.Cuota.
                   Where(c => c.por_cobrar == porCobrar && c.saldo > 0 && idsGrupos.Contains(c.Transaccion.id_actor_negocio_externo1)).
                   Select(c => new Cuenta_Cobrar_Pagar()
                   {
                       IdCuota = c.id,
                       CodigoCuota = c.codigo,
                       IdTransaccion = c.id_transaccion,
                       IdTipoTransaccion = c.Transaccion.id_tipo_transaccion,
                       TipoTransaccion = c.Transaccion.Transaccion2.Tipo_transaccion.nombre,
                       IdActorComercial = c.Transaccion.id_actor_negocio_externo,
                       CodigoTipoDocumentoActorComercial = c.Transaccion.Actor_negocio1.Actor.Detalle_maestro.valor,
                       NumeroDocumentoActorComercial = c.Transaccion.Actor_negocio1.Actor.numero_documento_identidad,
                       ActorComercial = c.Transaccion.Actor_negocio1.Actor.primer_nombre.Replace("|", " "),
                       NombreGrupo = c.Transaccion.Actor_negocio3.Actor.primer_nombre,
                       CodigoTipoComprobante = c.Transaccion.Comprobante.Detalle_maestro.valor,
                       TipoComprobante = c.Transaccion.Comprobante.Detalle_maestro.nombre,
                       SerieComprobante = c.Transaccion.Comprobante.numero_serie,
                       NumeroComprobante = c.Transaccion.Comprobante.numero,
                       FechaInicio = c.fecha_emision,
                       FechaFin = c.fecha_vencimiento,
                       PagoACuenta = c.pago_a_cuenta,
                       Total = c.total,
                       Revocado = c.revocado,
                       Saldo = c.saldo
                   }).OrderByDescending(c => c.IdCuota);
                return cuotas;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener cuotas por cobrar o pagar", e);
            }
        }

        public IEnumerable<Cobro_Pago> ObtenerCobrosOPagos(bool esCobro, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                var movimientos = _db.Transaccion.
                    Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && t.Tipo_transaccion.Accion_de_negocio_por_tipo_transaccion.Any(antt => antt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnCaja && antt.valor == esCobro) && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado).
                    Select(t => new Cobro_Pago()
                    {
                        Id = t.id,
                        FechaInicio = t.fecha_inicio,
                        CodigoTipoDocumentoEmpleado = t.Actor_negocio.Actor.Detalle_maestro.valor,
                        NumeroDocumentoEmpleado = t.Actor_negocio.Actor.numero_documento_identidad,
                        Empleado = t.Actor_negocio.Actor.primer_nombre.Replace("|", " "),
                        CodigoTipoDocumentoActorComercial = t.Actor_negocio1.Actor.Detalle_maestro.valor,
                        NumeroDocumentoActorComercial = t.Actor_negocio1.Actor.numero_documento_identidad,
                        ActorComercial = t.Actor_negocio1.Actor.primer_nombre.Replace("|", " "),
                        Total = t.importe_total,
                        MedioDePago = t.Traza_pago.Count > 0 ? t.Traza_pago.FirstOrDefault().Detalle_maestro1.nombre : "-",
                        EsCobro = esCobro,
                        TipoDeOperacion = (t.Pago_cuota.Count == 1 && t.id_transaccion_padre != null) ? t.Transaccion2.Tipo_transaccion.nombre : "-",
                        TipoDocumento = t.Comprobante.Detalle_maestro.nombre,
                        SerieNumeroDocumento = t.Comprobante.numero_serie + "-" + t.Comprobante.numero
                    }).OrderByDescending(c => c.Id);
                return movimientos;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener cobros o pagos", e);
            }
        }

        public IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(int idTipoTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta, int idSerie)
        {
            try
            {
                return _db.Transaccion.Include(t => t.Actor_negocio).Include(t => t.Actor_negocio1).Include(t => t.Actor_negocio2)
                    .Include(t => t.Actor_negocio.Actor.Detalle_maestro)
                    .Include(t => t.Actor_negocio1.Actor.Detalle_maestro)
                    .Include(t => t.Actor_negocio2.Actor.Detalle_maestro)
                    .Include(t => t.Detalle_maestro1).Include(t => t.Estado_transaccion.Select(et => et.Detalle_maestro))
                    .Include(t => t.Comprobante).Include(t => t.Comprobante.Detalle_maestro)
                  .Where(t => t.id_tipo_transaccion == idTipoTransaccion && t.Comprobante.id_serie_comprobante == idSerie && t.id_estado_actual == idUltimoEstado && t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(int idTipoTransaccion, DateTime fechaDesde, DateTime fechaHasta, int idUltimoEstado, int idSerie)
        {
            try
            {
                return _db.Transaccion
                    .Include(t => t.Actor_negocio)
                    .Include(t => t.Actor_negocio.Actor)
                    .Include(t => t.Actor_negocio.Actor.Detalle_maestro)
                    .Include(t => t.Actor_negocio1)
                    .Include(t => t.Actor_negocio2)
                    .Include(t => t.Actor_negocio1.Actor.Detalle_maestro)
                    .Include(t => t.Actor_negocio2.Actor.Detalle_maestro)
                    .Include(t => t.Detalle_maestro1)
                    .Include(t => t.Estado_transaccion)
                    .Include(t => t.Estado_transaccion.Select(et => et.Detalle_maestro))
                    .Include(t => t.Comprobante)
                    .Include(t => t.Comprobante.Detalle_maestro)
                  .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && t.id_tipo_transaccion == idTipoTransaccion && t.Comprobante.id_serie_comprobante == idSerie && t.id_estado_actual == idUltimoEstado);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(int[] idsTiposTransaccion, int[] idsTiposComprobantes, DateTime fechaDesde, DateTime fechaHasta, int idUltimoEstado)
        {
            try
            {
                return _db.Transaccion.Include(t => t.Actor_negocio).Include(t => t.Actor_negocio1).Include(t => t.Actor_negocio2)
                    .Include(t => t.Actor_negocio.Actor.Detalle_maestro)
                    .Include(t => t.Actor_negocio1.Actor.Detalle_maestro)
                    .Include(t => t.Actor_negocio2.Actor.Detalle_maestro)
                    .Include(t => t.Detalle_maestro1).Include(t => t.Estado_transaccion.Select(et => et.Detalle_maestro))
                    .Include(t => t.Comprobante).Include(t => t.Comprobante.Detalle_maestro)
                    .Where(t => idsTiposTransaccion.Contains(t.id_tipo_transaccion)
                    && t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta
                    && idsTiposComprobantes.Contains(t.Comprobante.id_tipo_comprobante)
                    && t.id_estado_actual == idUltimoEstado);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones", e);
            }
        }

        public IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(int[] idsTiposTransaccion, int[] idsTiposComprobantes, int idEstadoQueDebeContener, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                return _db.Transaccion.Include(t => t.Actor_negocio).Include(t => t.Actor_negocio1).Include(t => t.Actor_negocio2)
                    .Include(t => t.Actor_negocio.Actor.Detalle_maestro)
                    .Include(t => t.Actor_negocio1.Actor.Detalle_maestro)
                    .Include(t => t.Actor_negocio2.Actor.Detalle_maestro)
                    .Include(t => t.Detalle_maestro1).Include(t => t.Estado_transaccion.Select(et => et.Detalle_maestro))
                    .Include(t => t.Comprobante).Include(t => t.Comprobante.Detalle_maestro)
                    .Where(t => idsTiposTransaccion.Contains(t.id_tipo_transaccion)
                    && t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta
                    && idsTiposComprobantes.Contains(t.Comprobante.id_tipo_comprobante)
                    && t.Estado_transaccion.Select(et => et.id_estado).Contains(idEstadoQueDebeContener));
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones", e);
            }
        }

        public IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(int[] idsTiposTransaccion, int[] idsTiposComprobantes, int[] idsEstados, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                return _db.Transaccion
                    //.Include(t => t.Actor_negocio)
                    .Include(t => t.Actor_negocio1)
                    .Include(t => t.Actor_negocio1.Actor)
                    //.Include(t => t.Actor_negocio2)
                    //.Include(t => t.Actor_negocio.Actor.Detalle_maestro)
                    .Include(t => t.Actor_negocio1.Actor.Detalle_maestro)
                    //.Include(t => t.Actor_negocio2.Actor.Detalle_maestro)
                    .Include(t => t.Detalle_maestro1)
                    .Include(t => t.Estado_transaccion.Select(et => et.Detalle_maestro))
                    .Include(t => t.Comprobante)
                    .Include(t => t.Comprobante.Detalle_maestro)
                    .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta
                    && idsTiposTransaccion.Contains(t.id_tipo_transaccion)
                    && idsTiposComprobantes.Contains(t.Comprobante.id_tipo_comprobante)
                    && t.Evento_transaccion.Where(et => idsEstados.Contains(et.id_evento)).Count() > 0
                    );
                //&& idsEstados.Contains(t.Evento_transaccion.whereid_estado)
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstadoSinConcepto(int[] idsTiposTransaccion, int[] idsTiposComprobantes, int[] idsEstados, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                return _db.Transaccion
                    //.Include(t => t.Actor_negocio)
                    .Include(t => t.Actor_negocio1)
                    .Include(t => t.Actor_negocio1.Actor)
                    //.Include(t => t.Actor_negocio2)
                    //.Include(t => t.Actor_negocio.Actor.Detalle_maestro)
                    .Include(t => t.Actor_negocio1.Actor.Detalle_maestro)
                    //.Include(t => t.Actor_negocio2.Actor.Detalle_maestro)
                    .Include(t => t.Detalle_maestro1)
                    .Include(t => t.Detalle_transaccion.Select(dt => dt.Concepto_negocio))

                    .Include(t => t.Estado_transaccion.Select(et => et.Detalle_maestro))
                    .Include(t => t.Comprobante)
                    .Include(t => t.Comprobante.Detalle_maestro)
                    //transaccion de referencia
                    .Include(t => t.Transaccion3.Comprobante)
                    .Include(t => t.Transaccion3.Comprobante.Detalle_maestro)
                    .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && idsTiposTransaccion.Contains(t.id_tipo_transaccion) && idsTiposComprobantes.Contains(t.Comprobante.id_tipo_comprobante)
                                && idsEstados.Contains(t.Evento_transaccion.OrderByDescending(est => est.id).FirstOrDefault().id_evento));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<ReporteVentaDetalladoSinConcepto> ObtenerReporteVentaDetalladoSinConcepto(int[] idsTiposTransaccion, int[] idsTiposComprobantes, int[] idsEstados, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                return _db.Transaccion.Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && idsTiposTransaccion.Contains(t.id_tipo_transaccion) && idsTiposComprobantes.Contains(t.Comprobante.id_tipo_comprobante) && idsEstados.Contains(t.Evento_transaccion.OrderByDescending(est => est.id).FirstOrDefault().id_evento))
                    .Select(t => new ReporteVentaDetalladoSinConcepto()
                    {
                        FechaEmisionComprobante = t.fecha_inicio,
                        FechaVencimientoComprobante = t.fecha_fin,
                        IdTipoComprobante = t.Comprobante.Detalle_maestro.id,
                        TipoComprobante = t.Comprobante.Detalle_maestro.codigo,
                        SerieComprobante = t.Comprobante.numero_serie,
                        NumeroComprobante = t.Comprobante.numero,
                        TipoDocumentoIdentidadCliente = (t.Actor_negocio1.id == ActorSettings.Default.IdClienteGenerico) ? "" : t.Actor_negocio1.Actor.Detalle_maestro.codigo,
                        NumeroDocumentoIdentidadCliente = (t.Actor_negocio1.id == ActorSettings.Default.IdClienteGenerico) ? "0" : t.Actor_negocio1.Actor.numero_documento_identidad,
                        RazonSocialCliente = (t.Actor_negocio1.id == ActorSettings.Default.IdClienteGenerico) ? "CLIENTE PÚBLICO" : t.Actor_negocio1.Actor.primer_nombre.Replace("|", " "),
                        IdEstado = (int)t.id_estado_actual,
                        ValorTotal = t.importe_total,
                        ValorIsc = (decimal)t.Detalle_transaccion.Sum(dt => dt.isc),
                        ValorIgv = (decimal)t.Detalle_transaccion.Sum(dt => dt.igv),
                        Icbpers = t.Parametro_transaccion.Where(pt => pt.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroIcbper).Select(pt => pt.valor),
                        ValorTipoCambio = t.tipo_cambio,
                        FechaComprobanteReferencia = t.Transaccion3 != null ? (DateTime?)t.Transaccion3.fecha_inicio : null,
                        TipoComprobanteReferencia = t.Transaccion3 != null ? t.Transaccion3.Comprobante.Detalle_maestro.codigo : null,
                        SerieComprobanteReferencia = t.Transaccion3 != null ? t.Transaccion3.Comprobante.numero_serie : null,
                        NumeroComprobanteReferencia = t.Transaccion3 != null ? t.Transaccion3.Comprobante.numero : 0,
                    });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstadoYConceptoNegocio(int[] idsTiposTransaccion, int[] idsTiposComprobantes, int[] idsEstados, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                return _db.Transaccion
                    //.Include(t => t.Actor_negocio)
                    .Include(t => t.Actor_negocio1)
                    .Include(t => t.Actor_negocio1.Actor)
                    //.Include(t => t.Actor_negocio2)
                    //.Include(t => t.Actor_negocio.Actor.Detalle_maestro)
                    .Include(t => t.Actor_negocio1.Actor.Detalle_maestro)
                    //.Include(t => t.Actor_negocio2.Actor.Detalle_maestro)
                    .Include(t => t.Detalle_maestro1)
                    .Include(t => t.Detalle_transaccion.Select(dt => dt.Concepto_negocio))

                    .Include(t => t.Estado_transaccion.Select(et => et.Detalle_maestro))
                    .Include(t => t.Comprobante)
                    .Include(t => t.Comprobante.Detalle_maestro)
                    //transaccion de referencia
                    .Include(t => t.Transaccion3.Comprobante)
                    .Include(t => t.Transaccion3.Comprobante.Detalle_maestro)
                    .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && idsTiposTransaccion.Contains(t.id_tipo_transaccion) && idsTiposComprobantes.Contains(t.Comprobante.id_tipo_comprobante)
                                 && idsEstados.Contains(t.Evento_transaccion.OrderByDescending(est => est.id).FirstOrDefault().id_evento));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public long ObtenerIdTransaccion(int idActorNegocioInterno, int idTipoTransaccion, int idUltimoEstado)
        {
            try
            {
                return _db.Transaccion.SingleOrDefault(t => t.id_tipo_transaccion == idTipoTransaccion && t.id_actor_negocio_interno == idActorNegocioInterno && t.id_estado_actual == idUltimoEstado).id;
            }
            catch (Exception e)
            {
                throw new DatosException("No se pudo obtener id transaccion", e);
            }

        }

        public long ObtenerIdTransaccion(int idActorNegocioInterno, int idTipoTransaccion)
        {
            try
            {
                return _db.Transaccion.SingleOrDefault(t => t.id_tipo_transaccion == idTipoTransaccion && t.id_actor_negocio_interno == idActorNegocioInterno).id;
            }
            catch (Exception e)
            {
                throw new DatosException("No se pudo obtener id transaccion", e);
            }

        }


        public IEnumerable<long> ObtenerIdTransacciones(int idTipoTransaccion, int idTipoComprobante, int idEstado)
        {
            try
            {
                return _db.Transaccion.Include(t => t.Comprobante)
                    .Where(t => t.id_tipo_transaccion == idTipoTransaccion && t.Comprobante.id_tipo_comprobante == idTipoComprobante && t.id_estado_actual == idEstado).Select(t => t.id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<long> ObtenerIdTransacciones(int idTipoTransaccion, int[] idsTiposComprobantes, int[] idsEstados)
        {
            try
            {
                return _db.Transaccion.Include(t => t.Comprobante)
                    .Where(t => t.id_tipo_transaccion == idTipoTransaccion && idsTiposComprobantes.Contains(t.Comprobante.id_tipo_comprobante) && idsEstados.Contains((int)t.id_estado_actual)).Select(t => t.id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #region OBTENER TRANSACCIONES CON PARAMETROS DE TIPO DE TRANSACCION, TIPO DE CROMPROBANTE, EVENTO A EVITAR, FECHA HASTA DONDE OBTENER Y OTROS.

        public IEnumerable<Transaccion> ObtenerTransacciones(int idTipoTransaccion, int idTipoComprobante, int idEventoAEvitar, DateTime fechaHasta, int cantidadAObtener)
        {
            try
            {
                return _db.Transaccion.
                    Include(sbt => sbt.Detalle_transaccion).
                    Include(sbt => sbt.Detalle_transaccion.Select(dt => dt.Concepto_negocio)).
                    Include(sbt => sbt.Detalle_maestro1).
                    Include(sbt => sbt.Comprobante).
                    Include(sbt => sbt.Parametro_transaccion).
                    Include(sbt => sbt.Actor_negocio).
                    Include(sbt => sbt.Actor_negocio1).
                    Include(sbt => sbt.Actor_negocio1.Actor).
                    Include(sbt => sbt.Actor_negocio1.Actor.Direccion).
                    Include(sbt => sbt.Actor_negocio2).
                    Include(sbt => sbt.Estado_transaccion)
                    .Where(t => t.id_tipo_transaccion == idTipoTransaccion && t.Comprobante.id_tipo_comprobante == idTipoComprobante && (!t.Evento_transaccion.Any(et => et.id_evento == idEventoAEvitar)) && t.fecha_inicio < fechaHasta).Take(cantidadAObtener);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Transaccion> ObtenerTransacciones(int idTipoTransaccion, int idTipoComprobante, int idEventoAEvitar, DateTime fechaHasta)
        {
            try
            {
                return _db.Transaccion.
                   Include(sbt => sbt.Detalle_transaccion).
                    Include(sbt => sbt.Detalle_transaccion.Select(dt => dt.Concepto_negocio)).
                    Include(sbt => sbt.Detalle_maestro1).
                    Include(sbt => sbt.Comprobante).
                    Include(sbt => sbt.Parametro_transaccion).
                    Include(sbt => sbt.Actor_negocio).
                    Include(sbt => sbt.Actor_negocio1).
                    Include(sbt => sbt.Actor_negocio1.Actor).
                    Include(sbt => sbt.Actor_negocio1.Actor.Direccion).
                    Include(sbt => sbt.Actor_negocio2).
                    Include(sbt => sbt.Estado_transaccion)
                    .Where(t => t.id_tipo_transaccion == idTipoTransaccion && t.Comprobante.id_tipo_comprobante == idTipoComprobante && (!t.Evento_transaccion.Any(et => et.id_evento == idEventoAEvitar)) && t.fecha_inicio < fechaHasta);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Transaccion> ObtenerTransacciones(int[] idsTiposTransacciones, int idTipoComprobante, int idEventoAEvitar, DateTime fechaHasta)
        {
            try
            {
                return _db.Transaccion.
                    Include(sbt => sbt.Detalle_transaccion).
                    Include(sbt => sbt.Detalle_transaccion.Select(dt => dt.Concepto_negocio)).
                    Include(sbt => sbt.Detalle_maestro1).
                    Include(sbt => sbt.Comprobante).
                    Include(sbt => sbt.Parametro_transaccion).
                    Include(sbt => sbt.Actor_negocio).
                    Include(sbt => sbt.Actor_negocio1).
                    Include(sbt => sbt.Actor_negocio1.Actor).
                    Include(sbt => sbt.Actor_negocio1.Actor.Direccion).
                    Include(sbt => sbt.Actor_negocio2).
                    Include(sbt => sbt.Estado_transaccion)
                    .Where(t => idsTiposTransacciones.Contains(t.id_tipo_transaccion) && t.Comprobante.id_tipo_comprobante == idTipoComprobante && (!t.Evento_transaccion.Any(et => et.id_evento == idEventoAEvitar)) && t.fecha_inicio < fechaHasta);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        public IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstadoFiltradoPorEmpleadoYActorInterno(int[] idsTiposTransaccion, int[] idsTiposComprobantes, int idEmpleado, int idActorNegocioInterno, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                return _db.Transaccion.Include(t => t.Actor_negocio).Include(t => t.Actor_negocio1).Include(t => t.Actor_negocio2)
                    .Include(t => t.Actor_negocio.Actor.Detalle_maestro)
                    .Include(t => t.Actor_negocio1.Actor.Detalle_maestro)
                    .Include(t => t.Actor_negocio2.Actor.Detalle_maestro)
                    .Include(t => t.Detalle_maestro1).Include(t => t.Estado_transaccion.Select(et => et.Detalle_maestro))
                    .Include(t => t.Comprobante).Include(t => t.Comprobante.Detalle_maestro)
                    .Include(t => t.Parametro_transaccion)
                    .Where(t => t.id_empleado == idEmpleado && t.id_actor_negocio_interno == idActorNegocioInterno && idsTiposTransaccion.Contains(t.id_tipo_transaccion) && idsTiposComprobantes.Contains(t.Comprobante.id_tipo_comprobante) && t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones", e);
            }
        }


        public IEnumerable<Resumen_Venta> ObtenerResumenesVentas(int[] idsTiposTransaccion, int[] idsTiposComprobantes, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {

                return _db.Transaccion
                        .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta
                       && idsTiposComprobantes.Contains(t.Comprobante.id_tipo_comprobante) && idsTiposTransaccion.Contains(t.id_tipo_transaccion))
                        .Select(
                            t => new Resumen_Venta()
                            {
                                Id = t.id,
                                FechaEmision = t.fecha_inicio,
                                IdTipoComprobante = t.Comprobante.Detalle_maestro.id,
                                TipoComprobante = t.Comprobante.Detalle_maestro.nombre,
                                CodigoComprobante = t.Comprobante.Detalle_maestro.valor,
                                SerieComprobante = t.Comprobante.numero_serie,
                                NumeroComprobante = t.Comprobante.numero,
                                IdCliente = t.id_actor_negocio_externo,
                                IdTipoDocumentoCliente = t.Actor_negocio1.Actor.id_documento_identidad,
                                DocumentoCliente = t.Actor_negocio1.Actor.numero_documento_identidad,
                                NombreCliente = t.Actor_negocio1.Actor.primer_nombre,
                                PrimerNombreCajero = t.Actor_negocio.Actor.primer_nombre,
                                SegundoNombreCajero = t.Actor_negocio.Actor.segundo_nombre,
                                TercerNombreCajero = t.Actor_negocio.Actor.tercer_nombre,
                                ImporteTotal = t.importe_total,
                                ValorParametroTipoDeVenta = t.Parametro_transaccion.FirstOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroTipoVenta) != null ? t.Parametro_transaccion.FirstOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroTipoVenta).valor : null,
                                ValorParametroModoDePago = t.Parametro_transaccion.FirstOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroModoDePago) != null ? t.Parametro_transaccion.FirstOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroModoDePago).valor : null,
                                ValorParametroAliasDeCliente = t.Parametro_transaccion.FirstOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroAliasCliente) != null ? t.Parametro_transaccion.FirstOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroAliasCliente).valor : null,
                                IdEstado = t.id_estado_actual,
                                Estado = t.Detalle_maestro.nombre,
                                Transmitido = t.Evento_transaccion.Select(ev => ev.id_evento).Contains(MaestroSettings.Default.IdDetalleMaestroEstadoTransmitido)

                            }
                    ).OrderByDescending(t => t.FechaEmision);

            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones", e);
            }
        }
        public IEnumerable<Resumen_Venta> ObtenerResumenesVentas(int[] idsTiposTransaccion, int[] idsTiposComprobantes, DateTime fechaDesde, DateTime fechaHasta, int idCliente)
        {
            try
            {

                return _db.Transaccion
                        .Where(t => t.id_actor_negocio_externo == idCliente && t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta
                       && idsTiposComprobantes.Contains(t.Comprobante.id_tipo_comprobante) && idsTiposTransaccion.Contains(t.id_tipo_transaccion))
                        .Select(
                            t => new Resumen_Venta()
                            {
                                Id = t.id,
                                FechaEmision = t.fecha_inicio,
                                IdTipoComprobante = t.Comprobante.Detalle_maestro.id,
                                TipoComprobante = t.Comprobante.Detalle_maestro.nombre,
                                CodigoComprobante = t.Comprobante.Detalle_maestro.valor,
                                SerieComprobante = t.Comprobante.numero_serie,
                                NumeroComprobante = t.Comprobante.numero,
                                IdCliente = t.id_actor_negocio_externo,
                                IdTipoDocumentoCliente = t.Actor_negocio1.Actor.id_documento_identidad,
                                DocumentoCliente = t.Actor_negocio1.Actor.numero_documento_identidad,
                                NombreCliente = t.Actor_negocio1.Actor.primer_nombre,
                                PrimerNombreCajero = t.Actor_negocio.Actor.primer_nombre,
                                SegundoNombreCajero = t.Actor_negocio.Actor.segundo_nombre,
                                TercerNombreCajero = t.Actor_negocio.Actor.tercer_nombre,
                                ImporteTotal = t.importe_total,
                                ValorParametroTipoDeVenta = t.Parametro_transaccion.FirstOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroTipoVenta) != null ? t.Parametro_transaccion.FirstOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroTipoVenta).valor : null,
                                ValorParametroModoDePago = t.Parametro_transaccion.FirstOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroModoDePago) != null ? t.Parametro_transaccion.FirstOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroModoDePago).valor : null,
                                ValorParametroAliasDeCliente = t.Parametro_transaccion.FirstOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroAliasCliente) != null ? t.Parametro_transaccion.FirstOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroAliasCliente).valor : null,
                                IdEstado = t.id_estado_actual,
                                Estado = t.Detalle_maestro.nombre,
                                Transmitido = t.Evento_transaccion.Select(ev => ev.id_evento).Contains(MaestroSettings.Default.IdDetalleMaestroEstadoTransmitido)

                            }
                    ).OrderByDescending(t => t.FechaEmision);

            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones", e);
            }
        }
        public IEnumerable<Resumen_Venta> ObtenerResumenesVentas(int[] idsTiposTransaccion, int[] idsTiposComprobantes, int idEmpleado, int idActorNegocioInterno, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {

                return _db.Transaccion
                        .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && idEmpleado == t.id_empleado && idActorNegocioInterno == t.id_actor_negocio_interno
                       && idsTiposComprobantes.Contains(t.Comprobante.id_tipo_comprobante) && idsTiposTransaccion.Contains(t.id_tipo_transaccion))
                        .Select(

                            t => new Resumen_Venta()
                            {
                                Id = t.id,
                                FechaEmision = t.fecha_inicio,
                                IdTipoComprobante = t.Comprobante.Detalle_maestro.id,
                                TipoComprobante = t.Comprobante.Detalle_maestro.nombre,
                                CodigoComprobante = t.Comprobante.Detalle_maestro.valor,
                                SerieComprobante = t.Comprobante.numero_serie,
                                NumeroComprobante = t.Comprobante.numero,
                                IdCliente = t.id_actor_negocio_externo,
                                IdTipoDocumentoCliente = t.Actor_negocio1.Actor.id_documento_identidad,
                                DocumentoCliente = t.Actor_negocio1.Actor.numero_documento_identidad,
                                NombreCliente = t.Actor_negocio1.Actor.primer_nombre,
                                PrimerNombreCajero = t.Actor_negocio.Actor.primer_nombre,
                                SegundoNombreCajero = t.Actor_negocio.Actor.segundo_nombre,
                                TercerNombreCajero = t.Actor_negocio.Actor.tercer_nombre,
                                ImporteTotal = t.importe_total,
                                ValorParametroTipoDeVenta = t.Parametro_transaccion.FirstOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroTipoVenta) != null ? t.Parametro_transaccion.FirstOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroTipoVenta).valor : null,
                                ValorParametroModoDePago = t.Parametro_transaccion.FirstOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroModoDePago) != null ? t.Parametro_transaccion.FirstOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroModoDePago).valor : null,
                                ValorParametroAliasDeCliente = t.Parametro_transaccion.FirstOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroAliasCliente) != null ? t.Parametro_transaccion.FirstOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroAliasCliente).valor : null,
                                IdEstado = t.id_estado_actual,
                                Estado = t.Detalle_maestro.nombre,
                                Transmitido = t.Evento_transaccion.Select(ev => ev.id_evento).Contains(MaestroSettings.Default.IdDetalleMaestroEstadoTransmitido)
                            }
                    ).OrderByDescending(t => t.FechaEmision);

            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones", e);
            }
        }
        public IEnumerable<Resumen_Venta> ObtenerResumenesVentas(int[] idsTiposTransaccion, int[] idsTiposComprobantes, int idEmpleado, int idActorNegocioInterno, DateTime fechaDesde, DateTime fechaHasta, int idCliente)
        {
            try
            {

                return _db.Transaccion
                        .Where(t => t.id_actor_negocio_externo == idCliente && t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && idEmpleado == t.id_empleado && idActorNegocioInterno == t.id_actor_negocio_interno
                       && idsTiposComprobantes.Contains(t.Comprobante.id_tipo_comprobante) && idsTiposTransaccion.Contains(t.id_tipo_transaccion))
                        .Select(

                            t => new Resumen_Venta()
                            {
                                Id = t.id,
                                FechaEmision = t.fecha_inicio,
                                IdTipoComprobante = t.Comprobante.Detalle_maestro.id,
                                TipoComprobante = t.Comprobante.Detalle_maestro.nombre,
                                CodigoComprobante = t.Comprobante.Detalle_maestro.valor,
                                SerieComprobante = t.Comprobante.numero_serie,
                                NumeroComprobante = t.Comprobante.numero,
                                IdCliente = t.id_actor_negocio_externo,
                                IdTipoDocumentoCliente = t.Actor_negocio1.Actor.id_documento_identidad,
                                DocumentoCliente = t.Actor_negocio1.Actor.numero_documento_identidad,
                                NombreCliente = t.Actor_negocio1.Actor.primer_nombre,
                                PrimerNombreCajero = t.Actor_negocio.Actor.primer_nombre,
                                SegundoNombreCajero = t.Actor_negocio.Actor.segundo_nombre,
                                TercerNombreCajero = t.Actor_negocio.Actor.tercer_nombre,
                                ImporteTotal = t.importe_total,
                                ValorParametroTipoDeVenta = t.Parametro_transaccion.FirstOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroTipoVenta) != null ? t.Parametro_transaccion.FirstOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroTipoVenta).valor : null,
                                ValorParametroModoDePago = t.Parametro_transaccion.FirstOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroModoDePago) != null ? t.Parametro_transaccion.FirstOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroModoDePago).valor : null,
                                ValorParametroAliasDeCliente = t.Parametro_transaccion.FirstOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroAliasCliente) != null ? t.Parametro_transaccion.FirstOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroAliasCliente).valor : null,
                                IdEstado = t.id_estado_actual,
                                Estado = t.Detalle_maestro.nombre,
                                Transmitido = t.Evento_transaccion.Select(ev => ev.id_evento).Contains(MaestroSettings.Default.IdDetalleMaestroEstadoTransmitido)
                            }
                    ).OrderByDescending(t => t.FechaEmision);

            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones", e);
            }
        }

        public IEnumerable<Resumen_Venta> ObtenerResumenesVentas(int[] idsTiposTransaccion, int[] idsTiposComprobantes, string comprobante)
        {
            try
            {

                return _db.Transaccion
                        .Where(t => comprobante.Equals(t.Comprobante.numero_serie + "-" + t.Comprobante.numero) && idsTiposComprobantes.Contains(t.Comprobante.id_tipo_comprobante) && idsTiposTransaccion.Contains(t.id_tipo_transaccion))
                        .Select(
                            t => new Resumen_Venta()
                            {
                                Id = t.id,
                                FechaEmision = t.fecha_inicio,
                                IdTipoComprobante = t.Comprobante.Detalle_maestro.id,
                                TipoComprobante = t.Comprobante.Detalle_maestro.nombre,
                                CodigoComprobante = t.Comprobante.Detalle_maestro.valor,
                                SerieComprobante = t.Comprobante.numero_serie,
                                NumeroComprobante = t.Comprobante.numero,
                                IdCliente = t.id_actor_negocio_externo,
                                IdTipoDocumentoCliente = t.Actor_negocio1.Actor.id_documento_identidad,
                                DocumentoCliente = t.Actor_negocio1.Actor.numero_documento_identidad,
                                NombreCliente = t.Actor_negocio1.Actor.primer_nombre,
                                PrimerNombreCajero = t.Actor_negocio.Actor.primer_nombre,
                                SegundoNombreCajero = t.Actor_negocio.Actor.segundo_nombre,
                                TercerNombreCajero = t.Actor_negocio.Actor.tercer_nombre,
                                ImporteTotal = t.importe_total,
                                ValorParametroTipoDeVenta = t.Parametro_transaccion.FirstOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroTipoVenta) != null ? t.Parametro_transaccion.FirstOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroTipoVenta).valor : null,
                                ValorParametroModoDePago = t.Parametro_transaccion.FirstOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroModoDePago) != null ? t.Parametro_transaccion.FirstOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroModoDePago).valor : null,
                                ValorParametroAliasDeCliente = t.Parametro_transaccion.FirstOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroAliasCliente) != null ? t.Parametro_transaccion.FirstOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroAliasCliente).valor : null,
                                IdEstado = t.id_estado_actual,
                                Estado = t.Detalle_maestro.nombre,
                                Transmitido = t.Evento_transaccion.Select(ev => ev.id_evento).Contains(MaestroSettings.Default.IdDetalleMaestroEstadoTransmitido)
                            }
                    ).OrderByDescending(t => t.FechaEmision);

            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones", e);
            }
        }

        public IEnumerable<Resumen_Venta> ObtenerResumenesVentas_(int[] idsTiposTransaccion, int[] idsTiposComprobantes, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                _db.Configuration.LazyLoadingEnabled = false;
                var transacciones = _db.Transaccion.Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && idsTiposComprobantes.Contains(t.Comprobante.id_tipo_comprobante) && idsTiposTransaccion.Contains(t.id_tipo_transaccion)).ToArray();
                var idsTransacciones = transacciones.Select(t => t.id).ToArray();
                var idsComprobantes = transacciones.Select(t => t.id_comprobante).ToArray();
                var comprobantes = _db.Comprobante.Where(c => idsComprobantes.Contains(c.id)).ToArray();
                var tiposComprobantes = _db.Detalle_maestro.Where(c => idsTiposComprobantes.Contains(c.id)).ToArray();
                //var detallesTransacciones = _db.Detalle_transaccion.Where(dt => idsTransacciones.Contains(dt.id_transaccion));
                var idsEmpleados = transacciones.Select(c => c.id_empleado).Distinct().ToArray();
                var idsActoresNegocioExternos = transacciones.Select(c => c.id_actor_negocio_externo).Distinct().ToArray();
                var idsActoresNegocio = idsEmpleados.Union(idsActoresNegocioExternos).ToArray();
                var actoresNegocio = _db.Actor_negocio.Where(an => idsActoresNegocio.Contains(an.id)).Include(an => an.Actor).ToArray();
                var parametrosTransacciones = _db.Parametro_transaccion.Where(pt => idsTransacciones.Contains(pt.id_transaccion)).ToArray();
                var idsEstadosTransacciones = transacciones.Select(t => t.id_estado_actual).Distinct().ToArray();
                var EstadosTransacciones = _db.Detalle_maestro.Where(dm => idsEstadosTransacciones.Contains(dm.id)).ToArray();
                var EventosTransacciones = _db.Evento_transaccion.Where(et => idsTransacciones.Contains(et.id_transaccion)).ToArray();

                List<Resumen_Venta> resumenes_Ventas = new List<Resumen_Venta>();
                foreach (var transaccion in transacciones)
                {
                    resumenes_Ventas.Add(new Resumen_Venta()
                    {
                        Id = transaccion.id,
                        FechaEmision = transaccion.fecha_inicio,
                        IdTipoComprobante = tiposComprobantes.Single(tc => tc.id == comprobantes.Single(c => c.id == transaccion.id_comprobante).id_tipo_comprobante).id,
                        TipoComprobante = tiposComprobantes.Single(tc => tc.id == comprobantes.Single(c => c.id == transaccion.id_comprobante).id_tipo_comprobante).nombre,
                        CodigoComprobante = tiposComprobantes.Single(tc => tc.id == comprobantes.Single(c => c.id == transaccion.id_comprobante).id_tipo_comprobante).valor,
                        SerieComprobante = comprobantes.Single(c => c.id == transaccion.id_comprobante).numero_serie,
                        NumeroComprobante = comprobantes.Single(c => c.id == transaccion.id_comprobante).numero,
                        IdCliente = transaccion.id_actor_negocio_externo,
                        IdTipoDocumentoCliente = actoresNegocio.Single(an => an.id == transaccion.id_actor_negocio_externo).Actor.id_documento_identidad,
                        DocumentoCliente = actoresNegocio.Single(an => an.id == transaccion.id_actor_negocio_externo).Actor.numero_documento_identidad,
                        NombreCliente = actoresNegocio.Single(an => an.id == transaccion.id_actor_negocio_externo).Actor.primer_nombre,
                        PrimerNombreCajero = actoresNegocio.Single(an => an.id == transaccion.id_empleado).Actor.primer_nombre,
                        SegundoNombreCajero = actoresNegocio.Single(an => an.id == transaccion.id_empleado).Actor.segundo_nombre,
                        TercerNombreCajero = actoresNegocio.Single(an => an.id == transaccion.id_empleado).Actor.tercer_nombre,
                        ImporteTotal = transaccion.importe_total,
                        ValorParametroTipoDeVenta = parametrosTransacciones.SingleOrDefault(pt => pt.id_transaccion == transaccion.id && pt.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroTipoVenta)?.valor,
                        ValorParametroModoDePago = parametrosTransacciones.SingleOrDefault(pt => pt.id_transaccion == transaccion.id && pt.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroModoDePago)?.valor,
                        ValorParametroAliasDeCliente = parametrosTransacciones.SingleOrDefault(pt => pt.id_transaccion == transaccion.id && pt.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroAliasCliente)?.valor,
                        IdEstado = EstadosTransacciones.Single(et => et.id == transaccion.id_estado_actual).id,
                        Estado = EstadosTransacciones.Single(et => et.id == transaccion.id_estado_actual).nombre,
                        Transmitido = EventosTransacciones.SingleOrDefault(et => et.id_transaccion == transaccion.id && et.id_evento == MaestroSettings.Default.IdDetalleMaestroEstadoTransmitido) != null
                    });
                }
                return resumenes_Ventas;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones", e);
            }
            finally
            {
                _db.Configuration.LazyLoadingEnabled = true;
            }
        }

        public IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(int[] idsTiposTransaccion, int[] idsTiposComprobantes, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {


                return _db.Transaccion
                    .Include(t => t.Actor_negocio).Include(t => t.Actor_negocio1)
                    .Include(t => t.Actor_negocio2)
                    .Include(t => t.Actor_negocio.Actor.Detalle_maestro)
                    .Include(t => t.Actor_negocio1.Actor.Detalle_maestro)
                    .Include(t => t.Actor_negocio2.Actor.Detalle_maestro)
                    .Include(t => t.Detalle_maestro1).Include(t => t.Estado_transaccion.Select(et => et.Detalle_maestro))
                    .Include(t => t.Comprobante).Include(t => t.Comprobante.Detalle_maestro)
                    .Include(t => t.Parametro_transaccion)
                    .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && idsTiposComprobantes.Contains(t.Comprobante.id_tipo_comprobante) && idsTiposTransaccion.Contains(t.id_tipo_transaccion));
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones", e);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idTipoTransaccion"></param>
        /// <param name="fechaDesde"></param>
        /// <param name="fechaHasta"></param>
        /// <returns></returns>

        public IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(int idTipoTransaccion, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                return _db.Transaccion.
                    Include(t => t.Actor_negocio).
                    Include(t => t.Actor_negocio1).
                    Include(t => t.Actor_negocio2).
                    Include(t => t.Actor_negocio.Actor).
                    Include(t => t.Actor_negocio1.Actor).
                    Include(t => t.Actor_negocio2.Actor).
                    Include(t => t.Actor_negocio.Actor.Detalle_maestro).
                    Include(t => t.Actor_negocio1.Actor.Detalle_maestro).
                    Include(t => t.Actor_negocio2.Actor.Detalle_maestro).
                    Include(t => t.Detalle_maestro1).
                    Include(t => t.Estado_transaccion.Select(et => et.Detalle_maestro)).
                    Include(t => t.Comprobante).
                    Include(t => t.Comprobante.Detalle_maestro).
                    Include(t => t.Parametro_transaccion).
                    Where(t => t.id_tipo_transaccion == idTipoTransaccion && t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones", e);
            }
        }

        public IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(int[] idsTipoTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                return _db.Transaccion
                    .Include(t => t.Detalle_transaccion)
                    .Include(t => t.Detalle_transaccion.Select(dt => dt.Concepto_negocio.Detalle_maestro4))
                    .Include(t => t.Actor_negocio)
                    .Include(t => t.Actor_negocio1)
                    .Include(t => t.Actor_negocio2)
                    .Include(t => t.Actor_negocio.Actor.Detalle_maestro)
                    .Include(t => t.Actor_negocio1.Actor.Detalle_maestro)
                    .Include(t => t.Actor_negocio2.Actor.Detalle_maestro)
                    .Include(t => t.Detalle_maestro1)
                    .Include(t => t.Estado_transaccion.Select(et => et.Detalle_maestro))
                    .Include(t => t.Comprobante)
                    .Include(t => t.Comprobante.Detalle_maestro)
                     .Include(t => t.Parametro_transaccion)
                  .Where(t => t.fecha_inicio >= fechaDesde
                              && t.fecha_inicio <= fechaHasta
                              && idsTipoTransaccion.Contains(t.id_tipo_transaccion)
                              && t.id_estado_actual == idUltimoEstado
                        );
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones", e);
            }
        }

        public IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado_(int idTipoTransaccion, int idEntidadInterna, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                return _db.Transaccion.Include(t => t.Actor_negocio).Include(t => t.Actor_negocio1).Include(t => t.Actor_negocio2)
                    .Include(t => t.Actor_negocio.Actor.Detalle_maestro)
                    .Include(t => t.Actor_negocio1.Actor.Detalle_maestro)
                    .Include(t => t.Actor_negocio2.Actor.Detalle_maestro)
                    .Include(t => t.Detalle_maestro1).Include(t => t.Estado_transaccion.Select(et => et.Detalle_maestro))
                    .Include(t => t.Comprobante).Include(t => t.Comprobante.Detalle_maestro)
                    .Where(t => t.id_actor_negocio_interno == idEntidadInterna && t.id_tipo_transaccion == idTipoTransaccion && t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones", e);
            }
        }

        public IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado_(int[] idsTipoTransaccion, int idEntidadInterna, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                return _db.Transaccion.Include(t => t.Actor_negocio).Include(t => t.Actor_negocio1).Include(t => t.Actor_negocio2)
                    .Include(t => t.Actor_negocio.Actor.Detalle_maestro)
                    .Include(t => t.Actor_negocio1.Actor.Detalle_maestro)
                    .Include(t => t.Actor_negocio2.Actor.Detalle_maestro)
                    .Include(t => t.Detalle_maestro1).Include(t => t.Estado_transaccion.Select(et => et.Detalle_maestro))
                    .Include(t => t.Comprobante).Include(t => t.Comprobante.Detalle_maestro)
                    .Where(t => t.id_actor_negocio_interno == idEntidadInterna && idsTipoTransaccion.Contains(t.id_tipo_transaccion) && t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones", e);
            }
        }

        public IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado_(int[] idsTipoTransaccion, int idUltimoEstado, int idEntidadInterna, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                var transacciones = _db.Transaccion
                    .Include(t => t.Tipo_transaccion)
                    .Include(t => t.Transaccion2.Tipo_transaccion)
                    .Include(t => t.Actor_negocio1)
                    .Include(t => t.Actor_negocio1.Actor)
                    .Include(t => t.Comprobante)
                    .Include(t => t.Comprobante.Detalle_maestro)
                    .Include(t => t.Comprobante.Detalle_maestro.Tipo_transaccion_tipo_comprobante)
                    .Include(t => t.Estado_transaccion)
                    .Where(t => t.id_actor_negocio_interno == idEntidadInterna
                    && idsTipoTransaccion.Contains(t.id_tipo_transaccion)
                    && t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta
                    && t.id_estado_actual == idUltimoEstado
                    );

                return transacciones;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones", e);
            }
        }
        public IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado_(int[] idsTipoTransaccion, int[] idsEntidadInterna, int idTipoComprobante, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                return _db.Transaccion.Include(t => t.Actor_negocio).Include(t => t.Actor_negocio1).Include(t => t.Actor_negocio2)
                    .Include(t => t.Actor_negocio.Actor.Detalle_maestro)
                    .Include(t => t.Actor_negocio1.Actor.Detalle_maestro)
                    .Include(t => t.Actor_negocio2.Actor.Detalle_maestro)
                    .Include(t => t.Detalle_maestro1).Include(t => t.Estado_transaccion.Select(et => et.Detalle_maestro))
                    .Include(t => t.Comprobante).Include(t => t.Comprobante.Detalle_maestro)
                    .Where(t => idsEntidadInterna.Contains(t.id_actor_negocio_interno) && idsTipoTransaccion.Contains(t.id_tipo_transaccion) && t.Comprobante.id_tipo_comprobante == idTipoComprobante && t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones", e);
            }
        }
        public IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado_(int[] idsTipoTransaccion, int[] idsEntidadInterna, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                return _db.Transaccion.Include(t => t.Actor_negocio).Include(t => t.Actor_negocio1).Include(t => t.Actor_negocio2)
                    .Include(t => t.Actor_negocio.Actor.Detalle_maestro)
                    .Include(t => t.Actor_negocio1.Actor.Detalle_maestro)
                    .Include(t => t.Actor_negocio2.Actor.Detalle_maestro)
                    .Include(t => t.Detalle_maestro1).Include(t => t.Estado_transaccion.Select(et => et.Detalle_maestro))
                    .Include(t => t.Comprobante).Include(t => t.Comprobante.Detalle_maestro)
                    .Where(t => idsEntidadInterna.Contains(t.id_actor_negocio_interno) && idsTipoTransaccion.Contains(t.id_tipo_transaccion) && t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones", e);
            }
        }
        public IEnumerable<Entrada_Salida_Almacen> ObtenerEntradasOSalidasDeAlmacen(bool esEntrada, List<int> idsCentrosAtencion, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                var movimientos = _db.Transaccion.
                    Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && idsCentrosAtencion.Contains(t.id_actor_negocio_interno) && t.Tipo_transaccion.Accion_de_negocio_por_tipo_transaccion.Any(antt => antt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioMovimientoEnAlmacen && antt.valor == esEntrada) && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado).
                    Select(t => new Entrada_Salida_Almacen()
                    {
                        Id = t.id,
                        FechaInicio = t.fecha_inicio,
                        CodigoTipoComprobante = t.Comprobante.Detalle_maestro.valor,
                        TipoComprobante = t.Comprobante.Detalle_maestro.nombre,
                        SerieComprobante = t.Comprobante.numero_serie,
                        NumeroComprobante = t.Comprobante.numero,
                        TipoDeOperacion = t.Transaccion2.Tipo_transaccion.nombre,
                        CodigoTipoDocumentoEmpleado = t.Actor_negocio.Actor.Detalle_maestro.valor,
                        NumeroDocumentoEmpleado = t.Actor_negocio.Actor.numero_documento_identidad,
                        Empleado = t.Actor_negocio.Actor.primer_nombre.Replace("|", " "),
                        CodigoTipoDocumentoActorComercial = t.Actor_negocio1.Actor.Detalle_maestro.valor,
                        NumeroDocumentoActorComercial = t.Actor_negocio1.Actor.numero_documento_identidad,
                        ActorComercial = t.Actor_negocio1.Actor.primer_nombre.Replace("|", " "),
                        EsEntrada = esEntrada,
                        CentroDeAtencion = t.Actor_negocio2.Actor.primer_nombre,
                        Establecimiento = t.Actor_negocio2.Actor_negocio2.Actor.tercer_nombre
                    }).OrderByDescending(c => c.Id);
                return movimientos;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener entradas o salidas de almacen", e);
            }
        }

        public IEnumerable<Orden_Recibir_Entregar> ObtenerOrdenesPorRecibirOPorEntregarDeAlmacen(bool porRecibir, List<int> idsCentrosAtencion, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                var movimientos = _db.Transaccion.
                    Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && idsCentrosAtencion.Contains(t.id_actor_negocio_interno) && t.Transaccion2.Tipo_transaccion.Accion_de_negocio_por_tipo_transaccion.Any(antt => antt.id_accion_de_negocio == TransaccionSettings.Default.IdAccionDeNegocioCompromisoDeAlmacen && antt.valor == porRecibir) && t.Comprobante.id_tipo_comprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteOrdenDeAlmacen && (t.Parametro_transaccion.FirstOrDefault(tt => tt.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroEstadoEntregaMercaderia).valor == ((int)IngresoTotal.No).ToString())).
                    Select(t => new Orden_Recibir_Entregar()
                    {
                        Id = t.id,
                        FechaInicio = t.fecha_inicio,
                        CodigoTipoComprobante = t.Transaccion3.Comprobante.Detalle_maestro.valor,
                        TipoComprobante = t.Transaccion3.Comprobante.Detalle_maestro.nombre,
                        SerieComprobante = t.Transaccion3.Comprobante.numero_serie,
                        NumeroComprobante = t.Transaccion3.Comprobante.numero,
                        TipoDeOperacion = t.Transaccion2.Tipo_transaccion.nombre,
                        SerieComprobanteOrden = t.Comprobante.numero_serie,
                        NumeroComprobanteOrden = t.Comprobante.numero,
                        CodigoTipoDocumentoActorComercial = t.Actor_negocio1.Actor.Detalle_maestro.valor,
                        NumeroDocumentoActorComercial = t.Actor_negocio1.Actor.numero_documento_identidad,
                        ActorComercial = t.Actor_negocio1.Actor.primer_nombre,
                        PorRecibir = porRecibir,
                        CentroDeAtencion = t.Actor_negocio2.Actor.primer_nombre,
                        Establecimiento = t.Actor_negocio2.Actor_negocio2.Actor.primer_nombre
                    }).OrderByDescending(c => c.Id);
                return movimientos;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener ordenes por recibir o por entregar de almacen", e);
            }
        }

        public IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(int idTipoTransaccion, int idUltimoEstado, int[] idsEntidadInterna, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                return _db.Transaccion.Include(t => t.Actor_negocio).Include(t => t.Actor_negocio1).Include(t => t.Actor_negocio2)
                    .Include(t => t.Actor_negocio.Actor.Detalle_maestro)
                    .Include(t => t.Actor_negocio1.Actor.Detalle_maestro)
                    .Include(t => t.Actor_negocio2.Actor.Detalle_maestro)
                    .Include(t => t.Detalle_maestro1).Include(t => t.Estado_transaccion.Select(et => et.Detalle_maestro))
                    .Include(t => t.Comprobante).Include(t => t.Comprobante.Detalle_maestro)
                    .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && idsEntidadInterna.Contains(t.id_actor_negocio_interno) && t.id_tipo_transaccion == idTipoTransaccion && t.id_estado_actual == idUltimoEstado);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones", e);
            }
        }

        public IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(int idTipoTransaccion, int[] idsEntidadInterna, DateTime fechaDesde, int idUltimoEstado, DateTime fechaHasta) //y22
        {
            try
            {
                return _db.Transaccion
                    .Include(t => t.Actor_negocio)
                    .Include(t => t.Actor_negocio1)
                    .Include(t => t.Actor_negocio2)
                    //.Include(t => t.Actor_negocio.Actor.Detalle_maestro)
                    //.Include(t => t.Actor_negocio1.Actor.Detalle_maestro)
                    .Include(t => t.Actor_negocio2.Actor)
                    //.Include(t => t.Actor_negocio2.Actor.Detalle_maestro)

                    .Include(t => t.Detalle_transaccion)
                    .Include(t => t.Detalle_transaccion.Select(dt => dt.Concepto_negocio))
                    .Include(t => t.Detalle_transaccion.Select(dt => dt.Concepto_negocio.Detalle_maestro4))
                    .Include(t => t.Detalle_maestro1)
                    .Include(t => t.Estado_transaccion)
                    .Include(t => t.Estado_transaccion.Select(et => et.Detalle_maestro))
                    .Include(t => t.Comprobante).Include(t => t.Comprobante.Detalle_maestro)
                    .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && idsEntidadInterna.Contains(t.id_actor_negocio_interno) && t.id_tipo_transaccion == idTipoTransaccion && t.id_estado_actual == idUltimoEstado);
            }

            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones", e);
            }
        }


        public IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(int idTipoTransaccion, int idUltimoEstado, int idEntidadInterna, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                return _db.Transaccion.Include(t => t.Actor_negocio).Include(t => t.Actor_negocio1).Include(t => t.Actor_negocio2)
                    .Include(t => t.Actor_negocio.Actor.Detalle_maestro)
                    .Include(t => t.Actor_negocio1.Actor.Detalle_maestro)
                    .Include(t => t.Actor_negocio2.Actor.Detalle_maestro)
                    .Include(t => t.Detalle_maestro1).Include(t => t.Estado_transaccion.Select(et => et.Detalle_maestro))
                    .Include(t => t.Comprobante).Include(t => t.Comprobante.Detalle_maestro)
                    .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && t.id_actor_negocio_interno == idEntidadInterna && t.id_tipo_transaccion == idTipoTransaccion && t.id_estado_actual == idUltimoEstado);
            }
            catch (Exception e)
            {
                throw e; throw new DatosException("Error al obtener transacciones", e);
            }
        }

        public IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(int idTipoTransaccion, int[] idsEstados, int idEntidadInterna, DateTime fechaDesde, DateTime fechaHasta) //y33
        {
            try
            {
                return _db.Transaccion.Include(t => t.Actor_negocio).Include(t => t.Actor_negocio1).Include(t => t.Actor_negocio2)
                    .Include(t => t.Actor_negocio.Actor.Detalle_maestro)
                    .Include(t => t.Actor_negocio1.Actor.Detalle_maestro)
                    .Include(t => t.Actor_negocio2.Actor.Detalle_maestro)
                    .Include(t => t.Detalle_maestro1).Include(t => t.Estado_transaccion.Select(et => et.Detalle_maestro))
                    .Include(t => t.Comprobante).Include(t => t.Comprobante.Detalle_maestro)
                    .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && t.id_actor_negocio_interno == idEntidadInterna && t.id_tipo_transaccion == idTipoTransaccion && idsEstados.Contains((int)t.id_estado_actual));
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones", e);
            }
        }


        public Transaccion ObtenerTransaccionInclusiveActoresYDetalleMaestroYEstado(long id)
        {
            try
            {
                return _db.Transaccion.
                    Include(sbt => sbt.Detalle_transaccion).
                    Include(sbt => sbt.Comprobante).
                    Include(sbt => sbt.Comprobante.Detalle_maestro).
                    Include(sbt => sbt.Parametro_transaccion).
                    Include(sbt => sbt.Actor_negocio).
                    Include(sbt => sbt.Actor_negocio1).
                    Include(sbt => sbt.Actor_negocio2).
                    SingleOrDefault(t => t.id == id);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones", e);
            }
        }
        /// <summary>
        /// Metodo devuelve una transaccion con sus cuotas y transaccion 2 con comprobante
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Transaccion ObtenerTransaccionConCuotasInclusiveTransaccion2ConComprobante(long id)
        {
            try
            {
                return _db.Transaccion.
                    Include(sbt => sbt.Cuota).
                    Include(sbt => sbt.Transaccion2).
                    Include(sbt => sbt.Transaccion2.Comprobante).
                    SingleOrDefault(t => t.id == id);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones", e);
            }
        }
        /// <summary>
        /// Devuelve una Transaccion, la transaccion padre de este con sus respectivas transacciones hijas 
        /// Ejemplo: Se da el id de la operacion y esta me devuelve la transaccion wrapper con sus transacciones hijas (orden, transacciones de movimiento de mercaderia)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Transaccion ObtenerTransaccionInclusiveActorDeNegocio1Transaccion11(long idTransaccion)
        {
            try
            {
                return _db.Transaccion.
                    Include(t => t.Actor_negocio1).
                    Include(t => t.Actor_negocio1.Actor).
                    Include(t => t.Actor_negocio1.Actor.Detalle_maestro).
                    Include(t => t.Actor_negocio1.Actor.Direccion).
                    Include(t => t.Actor_negocio1.Rol).
                    Include(t => t.Transaccion11).
                    Include(t => t.Transaccion11.Select(t11 => t11.Detalle_transaccion)).
                    SingleOrDefault(t => t.id == idTransaccion);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones", e);
            }
        }

        /// <summary>
        /// Devuelve la Transaccion donde su id de Transaccion padre es idTransaccionPadre y su tipo de transaccion 
        /// Se asume que solo debe existir una trnasaccion del tipo idTipoTransaccion para la transaccion padre idTransaccionPadre
        /// </summary>
        /// <param name="idTransaccionPadre"></param>
        /// <returns></returns>
        public Transaccion ObtenerTransaccionInclusiveActoresYDetalleMaestroYEstadoYDetalleTransaccion(long idTransaccionPadre, long idTipoTransaccion)
        {
            try
            {
                return _db.Transaccion.
                    Include(sbt => sbt.Detalle_transaccion).
                    Include(sbt => sbt.Detalle_maestro1).
                    Include(sbt => sbt.Comprobante).
                    Include(sbt => sbt.Parametro_transaccion).
                    Include(sbt => sbt.Actor_negocio).
                    Include(sbt => sbt.Actor_negocio1).
                    Include(sbt => sbt.Actor_negocio1.Actor.Direccion).
                    Include(sbt => sbt.Actor_negocio2).
                    SingleOrDefault(t => t.id_transaccion_padre == idTransaccionPadre && t.id_tipo_transaccion == idTipoTransaccion);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones", e);
            }
        }

        public Transaccion ObtenerTransaccionInclusiveActoresYDetalleMaestroYEstadoYDetalleTransaccion(long idTransaccion)
        {
            try
            {
                var transaccion =
                _db.Transaccion.
                    Include(sbt => sbt.Detalle_transaccion).
                    Include(sbt => sbt.Detalle_transaccion.Select(dt => dt.Concepto_negocio)).
                    Include(sbt => sbt.Detalle_transaccion.Select(dt => dt.Concepto_negocio.Detalle_maestro4)).
                    Include(sbt => sbt.Detalle_transaccion.Select(dt => dt.Concepto_negocio.Valor_caracteristica_concepto_negocio.Select(vccn => vccn.Valor_caracteristica.Detalle_maestro))).
                    Include(sbt => sbt.Detalle_maestro1).
                    Include(sbt => sbt.Comprobante).
                    Include(sbt => sbt.Comprobante.Detalle_maestro).
                    Include(sbt => sbt.Parametro_transaccion).
                    Include(sbt => sbt.Actor_negocio).
                    Include(sbt => sbt.Actor_negocio.Actor).
                    Include(sbt => sbt.Actor_negocio1).
                    Include(sbt => sbt.Actor_negocio1.Actor).
                    Include(sbt => sbt.Actor_negocio1.Actor.Direccion).
                    Include(sbt => sbt.Actor_negocio1.Actor.Detalle_maestro).
                    Include(sbt => sbt.Actor_negocio1.Actor.Direccion.Select(d => d.Ubigeo)).
                    Include(sbt => sbt.Actor_negocio2).//Centro de atencion de la transaccion
                    Include(sbt => sbt.Actor_negocio2.Actor).//Actor del centro de atencion de la transaccion
                    Include(sbt => sbt.Actor_negocio2.Actor_negocio2).//Establecimiento comercial del centro de atencion
                    Include(sbt => sbt.Actor_negocio2.Actor_negocio2.Actor).//Actor del centro de atencion
                    Include(sbt => sbt.Actor_negocio2.Actor_negocio2.Actor.Direccion).//Direccion del centro de atencion
                    Include(sbt => sbt.Actor_negocio2.Actor_negocio2.Actor.Direccion.Select(d => d.Ubigeo)).//Ubigeo de la direccion del centro de atencion
                    Include(sbt => sbt.Estado_transaccion).
                    Include(sbt => sbt.Estado_transaccion.Select(et => et.Detalle_maestro)).
                    SingleOrDefault(t => t.id == idTransaccion);
                return transaccion;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones", e);
            }
        }

        public Transaccion ObtenerTransaccionInclusiveActoresYDetalleMaestroYEstadoYDetalleTransaccionConsultaComprobante(ConsultaComprobanteParameter consultaComprobante)
        {
            try
            {
                var IdsTipoTransaccion = Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentas;
                var numeroComprobante = int.Parse(consultaComprobante.Numero);
                var transaccion =
                _db.Transaccion.
                Include(sbt => sbt.Detalle_transaccion).
                Include(sbt => sbt.Detalle_transaccion.Select(dt => dt.Concepto_negocio)).
                Include(sbt => sbt.Detalle_transaccion.Select(dt => dt.Concepto_negocio.Detalle_maestro4)).
                Include(sbt => sbt.Detalle_transaccion.Select(dt => dt.Concepto_negocio.Valor_caracteristica_concepto_negocio.Select(vccn => vccn.Valor_caracteristica.Detalle_maestro))).
                Include(sbt => sbt.Detalle_maestro1).
                Include(sbt => sbt.Comprobante).
                Include(sbt => sbt.Comprobante.Detalle_maestro).
                Include(sbt => sbt.Parametro_transaccion).
                Include(sbt => sbt.Actor_negocio).
                Include(sbt => sbt.Actor_negocio.Actor).
                Include(sbt => sbt.Actor_negocio1).
                Include(sbt => sbt.Actor_negocio1.Actor).
                Include(sbt => sbt.Actor_negocio1.Actor.Direccion).
                Include(sbt => sbt.Actor_negocio1.Actor.Detalle_maestro).
                Include(sbt => sbt.Actor_negocio1.Actor.Direccion.Select(d => d.Ubigeo)).
                Include(sbt => sbt.Actor_negocio2).//Centro de atencion de la transaccion
                Include(sbt => sbt.Actor_negocio2.Actor).//Actor del centro de atencion de la transaccion
                Include(sbt => sbt.Actor_negocio2.Actor_negocio2).//Establecimiento comercial del centro de atencion
                Include(sbt => sbt.Actor_negocio2.Actor_negocio2.Actor).//Actor del centro de atencion
                Include(sbt => sbt.Actor_negocio2.Actor_negocio2.Actor.Direccion).//Direccion del centro de atencion
                Include(sbt => sbt.Actor_negocio2.Actor_negocio2.Actor.Direccion.Select(d => d.Ubigeo)).//Ubigeo de la direccion del centro de atencion
                Include(sbt => sbt.Estado_transaccion).
                Include(sbt => sbt.Estado_transaccion.Select(et => et.Detalle_maestro)).
                SingleOrDefault(t => IdsTipoTransaccion.Contains(t.id_tipo_transaccion) && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado && t.Comprobante.Detalle_maestro.codigo == consultaComprobante.Comprobante &&
                t.Comprobante.Serie_comprobante.numero == consultaComprobante.Serie && t.Comprobante.numero == numeroComprobante &&
                (t.fecha_inicio.Year == consultaComprobante.FechaEmision.Year &&
                t.fecha_inicio.Month == consultaComprobante.FechaEmision.Month &&
                t.fecha_inicio.Day == consultaComprobante.FechaEmision.Day) &&
                Math.Round(t.importe_total, 2) == consultaComprobante.Importe
                );
                return transaccion;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones", e);
            }
        }

        public List<Transaccion> ObtenerTransaccionesSegunOrigen_InclusiveActoresYDetalleMaestroYEstadoYDetalleTransaccion(long idTransaccionOrigen, int idTipoTransaccion)
        {
            try
            {
                var transacciones =
                _db.Transaccion.
                    Include(sbt => sbt.Detalle_transaccion).
                    Include(sbt => sbt.Detalle_transaccion.Select(dt => dt.Concepto_negocio)).
                    Include(sbt => sbt.Detalle_transaccion.Select(dt => dt.Concepto_negocio.Detalle_maestro4)).
                    Include(sbt => sbt.Detalle_transaccion.Select(dt => dt.Concepto_negocio.Valor_caracteristica_concepto_negocio.Select(vccn => vccn.Valor_caracteristica.Detalle_maestro))).
                    Include(sbt => sbt.Detalle_maestro1).
                    Include(sbt => sbt.Comprobante).
                    Include(sbt => sbt.Comprobante.Detalle_maestro).
                    Include(sbt => sbt.Parametro_transaccion).
                    Include(sbt => sbt.Actor_negocio).
                    Include(sbt => sbt.Actor_negocio.Actor).
                    Include(sbt => sbt.Actor_negocio1).
                    Include(sbt => sbt.Actor_negocio1.Actor).
                    Include(sbt => sbt.Actor_negocio1.Actor.Direccion).
                    Include(sbt => sbt.Actor_negocio1.Actor.Detalle_maestro).
                    Include(sbt => sbt.Actor_negocio1.Actor.Direccion.Select(d => d.Ubigeo)).
                    Include(sbt => sbt.Actor_negocio2).//Centro de atencion de la transaccion
                    Include(sbt => sbt.Actor_negocio2.Actor).//Actor del centro de atencion de la transaccion
                    Include(sbt => sbt.Actor_negocio2.Actor_negocio2).//Establecimiento comercial del centro de atencion
                    Include(sbt => sbt.Actor_negocio2.Actor_negocio2.Actor).//Actor del centro de atencion
                    Include(sbt => sbt.Actor_negocio2.Actor_negocio2.Actor.Direccion).//Direccion del centro de atencion
                    Include(sbt => sbt.Actor_negocio2.Actor_negocio2.Actor.Direccion.Select(d => d.Ubigeo)).//Ubigeo de la direccion del centro de atencion
                    Include(sbt => sbt.Estado_transaccion).
                    Include(sbt => sbt.Estado_transaccion.Select(et => et.Detalle_maestro)).
                    Where(t => t.id_transaccion_referencia == idTransaccionOrigen && t.id_tipo_transaccion == idTipoTransaccion);
                return transacciones.ToList();
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones", e);
            }
        }
        public IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstadoYDetalleTransaccion(long idTransaccionPadre, int idTipoTransaccion)
        {
            try
            {
                return _db.Transaccion.
                    Include(sbt => sbt.Detalle_transaccion).
                    Include(sbt => sbt.Detalle_transaccion.Select(dt => dt.Concepto_negocio)).
                    Include(sbt => sbt.Detalle_transaccion.Select(dt => dt.Concepto_negocio.Detalle_maestro4)).
                    Include(sbt => sbt.Detalle_transaccion.Select(dt => dt.Concepto_negocio.Valor_caracteristica_concepto_negocio.Select(vccn => vccn.Valor_caracteristica.Detalle_maestro))).
                    Include(sbt => sbt.Detalle_maestro1).
                    Include(sbt => sbt.Comprobante).
                    Include(sbt => sbt.Comprobante.Detalle_maestro).
                    Include(sbt => sbt.Parametro_transaccion).
                    Include(sbt => sbt.Actor_negocio).
                    Include(sbt => sbt.Actor_negocio1).
                    Include(sbt => sbt.Actor_negocio1.Actor.Direccion).
                    Include(sbt => sbt.Actor_negocio1.Actor.Detalle_maestro).
                    Include(sbt => sbt.Actor_negocio1.Actor.Direccion.Select(d => d.Ubigeo)).
                    Include(sbt => sbt.Actor_negocio2).
                    Include(sbt => sbt.Estado_transaccion).
                    Include(sbt => sbt.Estado_transaccion.Select(et => et.Detalle_maestro)).
                    Where(t => t.id_tipo_transaccion == idTipoTransaccion && t.Transaccion2.id == idTransaccionPadre);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones", e);
            }
        }

        public int ObtenerMaximoCodigoCuota(string empiezaEn)
        {
            int longitud = empiezaEn.Length;
            try
            {
                var ultimoCuota = _db.Cuota.
                    Where(c => c.codigo.StartsWith(empiezaEn) && c.codigo.EndsWith("_1")).OrderByDescending(c => c.id).FirstOrDefault();
                return ultimoCuota != null ? Convert.ToInt32(ultimoCuota.codigo.Substring(longitud, ultimoCuota.codigo.IndexOf("_") - longitud)) : 0;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener maximo codigo de cuota", e);
            }
        }

        public OperationResult CrearTransaccion(Transaccion transaccion)
        {
            try
            {
                _db.Transaccion.Add(transaccion);
                var result = Save();
                result.data = transaccion.id;
                result.information = transaccion;
                return result;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al crear transacción", e);

            }
        }





        public OperationResult ActualizarTransaccion(Transaccion transaccion)
        {
            try
            {
                Transaccion dbTransaccion = _db.Transaccion.Single(m => m.id == transaccion.id);
                _db.Entry(dbTransaccion).CurrentValues.SetValues(transaccion);
                var result = Save();
                result.data = transaccion.id;
                return result;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al actualizar transacción", e);
            }
        }

        public OperationResult ActualizarTransacciones(List<Transaccion> transacciones)
        {
            try
            {
                foreach (var transaccion in transacciones)
                {
                    Transaccion dbTransaccion1 = _db.Transaccion.Single(m => m.id == transaccion.id);
                    _db.Entry(dbTransaccion1).CurrentValues.SetValues(transaccion);
                }
                var result = Save();
                return result;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al actualizar transacciones", e);
            }
        }

        public PuntosDeCliente ObtenerPuntosDeCliente(DateTime fechaDesdeParaObtencionPuntos, int idCliente)
        {
            try
            {
                var transaccionesDePuntosPendientes = _db.Transaccion.Where(t => t.id_actor_negocio_externo == idCliente
                    && t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta
                    && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado
                    && t.fecha_inicio >= fechaDesdeParaObtencionPuntos
                    && t.cantidad3 > 0).ToList();
                PuntosDeCliente puntosDeCliente = new PuntosDeCliente
                {
                    PuntosPorCanjear = transaccionesDePuntosPendientes == null ? 0 : (int)transaccionesDePuntosPendientes.Sum(t => t.cantidad3)
                };
                return puntosDeCliente;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener los puntos del cliente", e);
            }
        }

        public IEnumerable<Transaccion> ObtenerTransaccionesParaCanjePuntos(DateTime fechaDesdeParaObtencionPuntos, int idCliente)
        {
            try
            {
                return _db.Transaccion.Where(t => t.id_actor_negocio_externo == idCliente
                    && t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta
                    && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado
                    && t.fecha_inicio >= fechaDesdeParaObtencionPuntos
                    && t.cantidad3 > 0).OrderBy(t => t.fecha_inicio);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener las transacciónes para el canje de puntos", e);
            }
        }

        public IEnumerable<Reporte_Puntos_Canjeados> ObtenerReportePuntosCanjeados(int idAccionDeNegocioMovimientoEnCaja, DateTime fechaDesde, DateTime fechaHasta, int[] idsCentrosAtencion)
        {
            try
            {
                var Resumen = _db.Accion_de_negocio_por_tipo_transaccion
                                .Where(antt => antt.id_accion_de_negocio == idAccionDeNegocioMovimientoEnCaja)
                                .SelectMany(antt => antt.Tipo_transaccion.Transaccion).Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && idsCentrosAtencion.Contains(t.id_actor_negocio_interno) && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado && t.cantidad1 > 0)
                    .Select(t => new Reporte_Puntos_Canjeados()
                    {
                        FechaPago = t.fecha_inicio,
                        IdCaja = t.id_actor_negocio_interno,
                        NombreCaja = t.Actor_negocio2.Actor.primer_nombre,
                        CodigoTipoComprobante = t.Transaccion2.Comprobante.Detalle_maestro.valor,
                        TipoComprobante = t.Transaccion2.Comprobante.Detalle_maestro.nombre,
                        SerieComprobante = t.Transaccion2.Comprobante.numero_serie,
                        NumeroComprobante = t.Transaccion2.Comprobante.numero,
                        CodigoTipoDocumentoCliente = t.Actor_negocio1.Actor.Detalle_maestro.valor,
                        NumeroDocumentoCliente = t.Actor_negocio1.Actor.numero_documento_identidad,
                        Cliente = t.Actor_negocio1.Actor.primer_nombre.Replace("|", " "),
                        Puntos = t.cantidad1,
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

        public IEnumerable<Reporte_Puntos_Pendientes> ObtenerReportePuntosPendientes(DateTime fechaDesdeParaObtencionPuntos)
        {
            try
            {
                return _db.Transaccion.Where(t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta
                    && t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado
                    && t.fecha_inicio >= fechaDesdeParaObtencionPuntos
                    && t.cantidad3 > 0)
                .GroupBy(t => new
                {
                    idCliente = t.id_actor_negocio_externo,
                    cliente = t.Actor_negocio1.Actor.primer_nombre,
                    numeroDocumento = t.Actor_negocio1.Actor.numero_documento_identidad
                }).Select(t => new Reporte_Puntos_Pendientes()
                {
                    NumeroDocumentoCliente = t.Key.numeroDocumento,
                    Cliente = t.Key.cliente.Replace("|", " "),
                    PuntosPendientes = (int)t.Sum(tt => tt.cantidad3)
                });
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener las transacciónes para el canje de puntos", e);
            }
        }
        public OperationResult ActualizarTransaccionTransaccion1DetallesParametro(Transaccion transaccion)
        {
            try
            {
                Transaccion dbTransaccion = _db.Transaccion.Single(m => m.id == transaccion.id);
                _db.Entry(dbTransaccion).CurrentValues.SetValues(transaccion);

                List<Transaccion> transacciones1 = transaccion.Transaccion1.ToList();
                foreach (var transaccion1 in transacciones1)
                {
                    Transaccion dbTransaccion1 = dbTransaccion.Transaccion1.Single(m => m.id == transaccion1.id);
                    _db.Entry(dbTransaccion1).CurrentValues.SetValues(transaccion1);

                    List<Detalle_transaccion> dbDetalles = dbTransaccion1.Detalle_transaccion.ToList();
                    List<Detalle_transaccion> updDetalles = transaccion1.Detalle_transaccion.ToList();
                    updDetalles.ForEach(d => d.Transaccion = dbTransaccion1);

                    foreach (var dbDetalle in dbDetalles)
                    {
                        if (!updDetalles.Any(dt => dt.id == dbDetalle.id))
                        {
                            _db.Detalle_transaccion.Remove(dbDetalle);
                        }
                    }
                    foreach (var updDetalle in updDetalles)
                    {
                        if (dbDetalles.Any(dt => dt.id == updDetalle.id))
                        {
                            Detalle_transaccion detalle = dbDetalles.Single(m => m.id == updDetalle.id);
                            _db.Entry(detalle).CurrentValues.SetValues(updDetalle);
                        }
                        else
                        {
                            _db.Detalle_transaccion.Add(updDetalle);
                        }
                    }

                    List<Parametro_transaccion> dbParametros = dbTransaccion1.Parametro_transaccion.ToList();
                    List<Parametro_transaccion> updParametros = transaccion1.Parametro_transaccion.ToList();
                    updParametros.ForEach(d => d.Transaccion = dbTransaccion1);

                    foreach (var dbParametro in dbParametros)
                    {
                        if (!updParametros.Any(dt => dt.id_parametro == dbParametro.id_parametro))
                        {
                            _db.Parametro_transaccion.Remove(dbParametro);
                        }
                    }
                    foreach (var updParametro in updParametros)
                    {
                        if (dbParametros.Any(dt => dt.id_parametro == updParametro.id_parametro))
                        {
                            Parametro_transaccion parametro = dbParametros.Single(m => m.id == updParametro.id);
                            _db.Entry(parametro).CurrentValues.SetValues(updParametro);
                        }
                        else
                        {
                            _db.Parametro_transaccion.Add(updParametro);
                        }
                    }
                }

                var result = Save();
                result.data = transaccion.id;
                return result;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al  actualizar transacción", e);
            }
        }

        public OperationResult CrearTransaccionYActualizarParametroTransaccion(Transaccion transaccion, Parametro_transaccion parametroTransaccion)
        {
            try
            {
                _db.Transaccion.Add(transaccion);
                if (parametroTransaccion != null)
                {
                    Parametro_transaccion dbParametroTransaccion = _db.Parametro_transaccion.Single(m => m.id == parametroTransaccion.id);
                    _db.Entry(dbParametroTransaccion).CurrentValues.SetValues(parametroTransaccion);
                }
                var result = Save();
                result.data = transaccion.id;
                return result;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al crear transacción", e);
            }
        }

        public OperationResult ActualizarParametroTransaccion(Parametro_transaccion parametroTransaccion)
        {
            try
            {
                Parametro_transaccion dbParametroTransaccion = _db.Parametro_transaccion.Single(m => m.id == parametroTransaccion.id);
                _db.Entry(dbParametroTransaccion).CurrentValues.SetValues(parametroTransaccion);
                var result = Save();
                return result;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al actualizar el parametro de transacción", e);
            }
        }

        public OperationResult ActualizarCoutas(List<Cuota> cuotas)
        {
            try
            {
                SetActualizarCuotas(cuotas);
                OperationResult result = new OperationResult();
                result = Save();
                return result;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al actualizar cuotas", e);
            }
        }

        public void SetActualizarCuotas(IEnumerable<Cuota> cuotas)
        {
            foreach (var item in cuotas)
            {
                Cuota dbCuota = _db.Cuota.Single(m => m.id == item.id);
                _db.Entry(dbCuota).CurrentValues.SetValues(item);
            }
        }

        public OperationResult CrearTransaccionYActualizarExistencias(Transaccion transaccion, List<Existencia> existencias)
        {
            try
            {
                _db.Transaccion.Add(transaccion);
                SetActualizarExistencias(existencias);
                var result = Save();
                result.data = transaccion.id;
                return result;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entries = ex.Entries.Single();
                return new OperationResult(new ExistenciaException(ex, (Existencia)entries.Entity, transaccion));
            }
            catch (Exception e)
            {
                throw new DatosException("Error al crear transacción", e);
            }
        }

        public OperationResult CrearTransaccionesYActualizarExistenciasYCuotas(Transaccion transaccion, List<Existencia> existencias, List<Cuota> cuotas)
        {
            try
            {
                _db.Transaccion.Add(transaccion);
                SetActualizarExistencias(existencias);
                SetActualizarCuotas(cuotas);
                var result = Save();
                result.data = 0;
                return result;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entries = ex.Entries.Single();
                return new OperationResult(new ExistenciaException(ex, (Existencia)entries.Entity, null));

            }
            catch (Exception e)
            {
                throw new DatosException("Error al crear transacciones", e);
            }
        }

        public OperationResult CrearEstadoTransaccionActualizarCuotas(Estado_transaccion estadoTransaccion, List<Cuota> cuotas)
        {
            try
            {
                _db.Estado_transaccion.Add(estadoTransaccion);
                foreach (var item in cuotas)
                {
                    Cuota dbCuota = _db.Cuota.Single(m => m.id == item.id);
                    _db.Entry(dbCuota).CurrentValues.SetValues(item);
                }
                var result = Save();
                result.data = estadoTransaccion.id_transaccion;
                return result;
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }

        public OperationResult CrearTransaccionesYActualizarExistencias(List<Transaccion> transacciones, List<Existencia> existencias)
        {
            try
            {
                _db.Transaccion.AddRange(transacciones);
                SetActualizarExistencias(existencias);
                var result = Save();
                result.data = 0;
                return result;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entries = ex.Entries.Single();
                return new OperationResult(new ExistenciaException(ex, (Existencia)entries.Entity, null));

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public OperationResult CrearTransaccionAgregarEstadoYActualizarExistencias(Transaccion transaccion, List<Existencia> existencias, Estado_transaccion estado)
        {
            try
            {
                _db.Transaccion.Add(transaccion);
                _db.Estado_transaccion.Add(estado);
                SetActualizarExistencias(existencias);
                var result = Save();
                result.data = transaccion.id;
                return result;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entries = ex.Entries.Single();
                return new OperationResult(new ExistenciaException(ex, (Existencia)entries.Entity, transaccion));

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public OperationResult CrearTransaccionAgregarEstadoYActualizarExistencias(Transaccion transaccion, List<Existencia> existencias, Estado_transaccion estadoTransaccion, List<Estado_cuota> estadosCuotas)
        {
            try
            {
                _db.Transaccion.Add(transaccion);
                _db.Estado_transaccion.Add(estadoTransaccion);
                foreach (var estadosCuota in estadosCuotas)
                {
                    _db.Estado_cuota.Add(estadosCuota);
                }
                SetActualizarExistencias(existencias);
                var result = Save();
                result.data = transaccion.id;
                return result;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entries = ex.Entries.Single();
                return new OperationResult(new ExistenciaException(ex, (Existencia)entries.Entity, transaccion));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public OperationResult ActualizarTransaccionYActualizarExistencias(Transaccion transaccion, List<Existencia> existencias)
        {
            try
            {
                Transaccion dbTransaccion = _db.Transaccion.Single(m => m.id == transaccion.id);
                _db.Entry(dbTransaccion).CurrentValues.SetValues(transaccion);
                SetActualizarExistencias(existencias);
                var result = Save();
                result.data = transaccion.id;
                return result;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entries = ex.Entries.Single();
                return new OperationResult(new ExistenciaException(ex, (Existencia)entries.Entity, transaccion));
            }
            catch (Exception e)
            {
                throw e;
            }
        }




        /// <summary>
        /// Crea la transaccion y crea el estado de una transaccion diferente a la creada
        /// </summary>
        /// <param name="transaccion"></param>
        /// <param name="estadoTransaccion"></param>
        /// <returns></returns>
        public OperationResult CrearTransaccionYCrearEstadoTransaccion(Transaccion transaccion, Estado_transaccion estadoTransaccion)
        {
            try
            {
                if (transaccion != null)
                {
                    _db.Transaccion.Add(transaccion);
                }
                if (estadoTransaccion != null)
                {
                    _db.Estado_transaccion.Add(estadoTransaccion);
                }
                return Save();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Actualiza una transaccion y crea una transaccion, tambien se crea estados de transaccion y estados de cuotas
        /// </summary>
        /// <param name="transaccionAActualizar"></param>
        /// <param name="transaccionACrear"></param>
        /// <param name="estadosTransaccionesACrear"></param>
        /// <param name="estadosCuotasACrear"></param>
        /// <returns></returns>
        public OperationResult ActualizarYCrear(Transaccion transaccionAActualizar, Transaccion transaccionACrear, List<Estado_transaccion> estadosTransaccionesACrear, List<Estado_cuota> estadosCuotasACrear)
        {
            try
            {
                if (transaccionAActualizar != null)
                {
                    Transaccion dbTransaccion = _db.Transaccion.Single(m => m.id == transaccionAActualizar.id);
                    _db.Entry(dbTransaccion).CurrentValues.SetValues(transaccionAActualizar);
                }
                if (transaccionACrear != null)
                {
                    _db.Transaccion.Add(transaccionACrear);
                }
                if (estadosTransaccionesACrear != null)
                {
                    foreach (var estadoTransaccion in estadosTransaccionesACrear)
                    {
                        _db.Estado_transaccion.Add(estadoTransaccion);
                    }
                }
                if (estadosCuotasACrear != null)
                {
                    foreach (var estadoCuota in estadosCuotasACrear)
                    {
                        _db.Estado_cuota.Add(estadoCuota);
                    }
                }
                return Save();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Crea las transacciones, tambien se crea estados de transaccion y estados de cuotas
        /// </summary>
        /// <param name="transaccionesACrear"></param>
        /// <param name="estadoTransaccionesACrear"></param>
        /// <param name="estadosCuotasACrear"></param>
        /// <returns></returns>
        public OperationResult CrearTransacionesYEstados(List<Transaccion> transaccionesACrear, List<Estado_transaccion> estadosTransaccionesACrear, List<Estado_cuota> estadosCuotasACrear)
        {
            try
            {
                if (transaccionesACrear != null)
                {
                    foreach (var transaccion in transaccionesACrear)
                    {
                        _db.Transaccion.Add(transaccion);
                    }
                }
                if (estadosTransaccionesACrear != null)
                {
                    foreach (var estadoTransaccion in estadosTransaccionesACrear)
                    {
                        _db.Estado_transaccion.Add(estadoTransaccion);
                    }
                }
                if (estadosCuotasACrear != null)
                {
                    foreach (var estadoCuota in estadosCuotasACrear)
                    {
                        _db.Estado_cuota.Add(estadoCuota);
                    }
                }
                return Save();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public OperationResult CrearTransacionesYEstados(List<Transaccion> transaccionesACrear, List<Estado_transaccion> estadosTransaccionesACrear, List<Estado_cuota> estadosCuotasACrear, List<Detalle_transaccion> updDetallesTransaccion, int idActorNegocioInternoTransaccionExistente, int idTipoTransaccionExistente, int idUltimoEstadoTransaccionExsitente, bool debeAumentarCantidad)
        {
            try
            {
                if (transaccionesACrear != null)
                {
                    foreach (var transaccion in transaccionesACrear)
                    {
                        _db.Transaccion.Add(transaccion);
                    }
                }
                if (estadosTransaccionesACrear != null)
                {
                    foreach (var estadoTransaccion in estadosTransaccionesACrear)
                    {
                        _db.Estado_transaccion.Add(estadoTransaccion);
                    }
                }
                if (estadosCuotasACrear != null)
                {
                    foreach (var estadoCuota in estadosCuotasACrear)
                    {
                        _db.Estado_cuota.Add(estadoCuota);
                    }
                }

                //Obtener los detalles de transaccion a actualizar
                //List<Detalle_transaccion> updDetallesTransaccion = transaccion.Detalle_transaccion.ToList();
                var idsConceptoNegocioUpd = updDetallesTransaccion.Select(vdu => vdu.id_concepto_negocio);

                //Obtener transaccion de base de datos que tiene estado confirmado del destino
                Transaccion dbTransaccionExistente = _db.Transaccion.Include(t => t.Detalle_transaccion).SingleOrDefault(t => t.id_actor_negocio_interno == idActorNegocioInternoTransaccionExistente
                                                        && t.id_tipo_transaccion == idTipoTransaccionExistente
                                                        && t.id_estado_actual == idUltimoEstadoTransaccionExsitente);

                //Obtener el inventario fisico que tiene estado confirmado del destino
                List<Detalle_transaccion> dbDetallesTransaccionDestino = dbTransaccionExistente.Detalle_transaccion.Where(dt => idsConceptoNegocioUpd.Contains(dt.id_concepto_negocio)).ToList();


                foreach (var updDetalleTransaccion in updDetallesTransaccion)
                {

                    bool tieneLote = !String.IsNullOrEmpty(updDetalleTransaccion.lote);

                    //Buscar el item de detalle de transaccion
                    var dbDetalleTransaccionExistente = dbDetallesTransaccionDestino.SingleOrDefault(dbdt => dbdt.id_concepto_negocio == updDetalleTransaccion.id_concepto_negocio && ((!tieneLote && String.IsNullOrEmpty(dbdt.lote)) || (tieneLote && dbdt.lote == updDetalleTransaccion.lote)));


                    if (dbDetalleTransaccionExistente != null)
                    {
                        //Modificar cantidad del inventario fisico del destino
                        dbDetalleTransaccionExistente.cantidad += debeAumentarCantidad ? updDetalleTransaccion.cantidad : (updDetalleTransaccion.cantidad * -1);
                    }
                    else
                    {
                        //Agregar el detalle de transaccion que se esta ingresando
                        dbTransaccionExistente.Detalle_transaccion.Add(updDetalleTransaccion);
                    }

                    //En caso debe de aumentar cantidad, se debe agregar los valores de caracteristica propia. De lo contrario se actualizar el id_detalle_transaccion
                    if (updDetalleTransaccion.Valor_detalle_maestro_detalle_transaccion != null)
                    {
                        //En caso debe de aumentar cantidad, se debe agregar los valores de caracteristica propia. De lo contrario se actualizar el id_detalle_transaccion
                        var updValorDetalles = updDetalleTransaccion.Valor_detalle_maestro_detalle_transaccion.ToList();

                        var dbValoresDetalles = dbDetalleTransaccionExistente.Valor_detalle_maestro_detalle_transaccion;

                        foreach (var valorDetalleUpd in updValorDetalles)
                        {
                            if (debeAumentarCantidad)
                            {
                                dbDetalleTransaccionExistente.Valor_detalle_maestro_detalle_transaccion.Add(valorDetalleUpd.Clone());
                            }
                            else
                            {
                                dbValoresDetalles.SingleOrDefault(dbvd => dbvd.id == valorDetalleUpd.id).Detalle_transaccion = updDetalleTransaccion;
                            }

                        }
                    }
                }
                OperationResult result = Save();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        /// <summary>
        /// Crea la transaccion, tambien se crea estado de transaccion y estados de cuotas
        /// </summary>
        /// <param name="transaccionACrear"></param>
        /// <param name="estadoTransaccionACrear"></param>
        /// <param name="estadosCuotasACrear"></param>
        /// <returns></returns>
        public OperationResult CrearTransacionYEstadoTransaccionYEstadosCuota(Transaccion transaccionACrear, Estado_transaccion estadoTransaccionACrear, List<Estado_cuota> estadosCuotasACrear)
        {
            try
            {
                _db.Transaccion.Add(transaccionACrear);
                _db.Estado_transaccion.Add(estadoTransaccionACrear);
                if (estadosCuotasACrear != null)
                {
                    foreach (var estadoCuota in estadosCuotasACrear)
                    {
                        _db.Estado_cuota.Add(estadoCuota);
                    }
                }
                return Save();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public OperationResult CrearEstadoTransaccionYCrearEstadoCuota(List<Estado_transaccion> estadoTransacciones, List<Estado_cuota> estadoCuotas)
        {
            try
            {
                foreach (var estadoTransaccion in estadoTransacciones)
                {
                    _db.Estado_transaccion.Add(estadoTransaccion);
                }
                foreach (var estadoCuota in estadoCuotas)
                {
                    _db.Estado_cuota.Add(estadoCuota);
                }
                return Save();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public OperationResult ActualizarExistencias(List<Existencia> existencias)
        {
            try
            {
                SetActualizarExistencias(existencias);
                var result = Save();
                result.data = existencias.FirstOrDefault().id;
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        private void SetActualizarExistencias(List<Existencia> existencias)
        {
            int[] idsConcepto = existencias.Select(e => e.id_concepto_negocio).ToArray();

            foreach (var existencia in existencias)
            {
                _db.Existencia.Attach(existencia);
                _db.Entry(existencia).State = EntityState.Modified;
                //_db.Entry(existencia_bd).OriginalValues["version_fila"] = existencia.version_fila;
            }
        }
        public Transaccion ObtenerTransaccionInclusiveEstados(long id)
        {
            try
            {
                return _db.Transaccion.Include(st => st.Estado_transaccion).SingleOrDefault(st => st.id == id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Transaccion ObtenerTransaccionInclusiveEstadoTransaccionYDetalleTransaccion(long id)
        {
            try
            {
                return _db.Transaccion.Include(st => st.Estado_transaccion).Include(st => st.Detalle_transaccion).SingleOrDefault(st => st.id == id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Transaccion ObtenerTransaccion(long id)
        {
            try
            {
                return _db.Transaccion.SingleOrDefault(st => st.id == id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Detalle_transaccion ObtenerDetalleTransaccion(long id)
        {
            try
            {
                return _db.Detalle_transaccion.SingleOrDefault(st => st.id == id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Transaccion ObtenerTransaccionInclusiveEstadoTransaccionDetalleMaestro(long id)
        {
            try
            {
                return _db.Transaccion.Include(t => t.Estado_transaccion).Include(t => t.Detalle_maestro).Single(t => t.id == id);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener la transaccion", e);
            }
        }
        public IEnumerable<Transaccion> ObtenerTransacciones1DeTransaccionInclusiveEstadoTransaccionDetalleMaestro(long id)
        {
            try
            {
                return _db.Transaccion.Include(t => t.Transaccion1).Include(t => t.Estado_transaccion).Include(t => t.Detalle_maestro).Where(t => t.id == id).SelectMany(t => t.Transaccion1);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones 1 de transaccion", e);
            }
        }
        public IEnumerable<Transaccion> ObtenerTransacciones11DeTransaccion(long id)
        {
            try
            {
                return _db.Transaccion.Where(t => t.id == id).SelectMany(t => t.Transaccion11);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones 1 de transaccion", e);
            }
        }
        public IEnumerable<Transaccion> ObtenerTransacciones11DeTransaccion3DeTransaccion(long id)
        {
            try
            {
                return _db.Transaccion.Where(t => t.id == id).Select(t => t.Transaccion3).SelectMany(t => t.Transaccion11);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones 1 de transaccion", e);
            }
        }
        public IEnumerable<Transaccion> ObtenerTransacciones11DeTransacciones(long[] ids)
        {
            try
            {
                return _db.Transaccion.Where(t => ids.Contains(t.id)).SelectMany(t => t.Transaccion11);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones 1 de transaccion", e);
            }
        }
        public Transaccion ObtenerTransaccionPadre(long id)
        {
            try
            {
                return _db.Transaccion.Single(t => t.id == id).Transaccion2;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener la transaccion padre", e);
            }
        }

        public bool ExisteTransaccion(int idTipoTransaccion, int idActorNegocioInterno, int idActorNegocioExterno1, int idEstadoActual)
        {
            return _db.Transaccion.Any(t => t.id_tipo_transaccion == idTipoTransaccion && t.id_actor_negocio_interno == idActorNegocioInterno && t.id_actor_negocio_externo1 == idActorNegocioExterno1 && t.id_estado_actual == idEstadoActual);
        }

        public OperationResult CrearEstadoDeTransaccionAhora(Estado_transaccion estadoTransaccion)
        {
            try
            {
                _db.Estado_transaccion.Add(estadoTransaccion);
                var result = Save();
                result.data = estadoTransaccion.id_transaccion;
                return result;
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }

        public OperationResult CrearEstadoDeTransaccionAhoraActualizarTransaccion(Estado_transaccion estadoTransaccion, Transaccion transaccion)
        {
            try
            {
                _db.Estado_transaccion.Add(estadoTransaccion);
                Transaccion dbTransaccion = _db.Transaccion.Single(m => m.id == transaccion.id);
                _db.Entry(dbTransaccion).CurrentValues.SetValues(transaccion);
                var result = Save();
                result.data = estadoTransaccion.id_transaccion;
                return result;
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }
        public OperationResult CrearEstadoTransaccionActualizarActorNegocio(Estado_transaccion estadoTransaccion, Actor_negocio actorNegocio)
        {
            try
            {
                if (estadoTransaccion != null)
                {
                    _db.Estado_transaccion.Add(estadoTransaccion);
                }
                if (actorNegocio != null)
                {
                    Actor_negocio dbActorNegocio = _db.Actor_negocio.Single(m => m.id == actorNegocio.id);
                    _db.Entry(dbActorNegocio).CurrentValues.SetValues(actorNegocio);
                }
                var result = Save();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public OperationResult CrearEstadosDeTransaccionesAhora(List<Estado_transaccion> estadosDeTransacciones)
        {
            try
            {
                foreach (var estados in estadosDeTransacciones)
                {
                    _db.Estado_transaccion.Add(estados);
                }
                return Save();
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }

        public OperationResult CrearEstadosDeTransaccionesAhoraActualizarTransaccion(List<Estado_transaccion> estadosDeTransacciones, Transaccion transaccion)
        { 
            try
            {
                foreach (var estados in estadosDeTransacciones)
                {
                    _db.Estado_transaccion.Add(estados);
                }
                Transaccion dbTransaccion = _db.Transaccion.Single(m => m.id == transaccion.id);
                _db.Entry(dbTransaccion).CurrentValues.SetValues(transaccion);
                return Save();
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }

        public OperationResult CrearEstadosMasivosDeTransacciones(List<Estado_transaccion> estadosDeTransacciones)
        {
            try
            {
                string commandInsert = "insert into Estado_transaccion values ";

                List<string> commands = new List<string>();
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 35, 0)))
                //{

                foreach (var estado in estadosDeTransacciones)
                {
                    commands.Add(Environment.NewLine + "(" + estado.id_transaccion + "," + estado.id_empleado + "," + estado.id_estado + ",'" + estado.fecha.ToString("MM-dd-yyyy hh:mm:ss") + "','" + estado.comentario.Replace("'", "''") + "'),");
                }


                var rondas = Math.Ceiling((double)commands.Count / 1000);
                for (int a = 0; a < rondas; a++)
                {
                    var subList = commands.Skip(a * 1000).Take(1000);

                    string com = "";
                    foreach (var item in subList)
                    {
                        com += item;
                    }
                    com = com.Substring(0, com.Length - 1);
                    _db.Database.ExecuteSqlCommand(commandInsert + com);

                }

                //scope.Complete();
                var resultadoDetails = new OperationResult(OperationResultEnum.Success);
                return resultadoDetails;
                //}
            }
            catch (Exception e)
            {
                throw new DatosException("Error al crear de forma masiva estados de transaccion", e);
            }
        }
        public OperationResult CrearEventoTransaccion(Evento_transaccion eventoTransaccion)
        {
            try
            {
                _db.Evento_transaccion.Add(eventoTransaccion);
                var result = Save();
                result.data = eventoTransaccion.id_transaccion;
                result.information = eventoTransaccion.id;
                return result;
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }
        public OperationResult CrearEventoTransaccionInformacionTransaccion(Evento_transaccion eventoTransaccion, string informacionTransaccion)
        {
            try
            {
                _db.Evento_transaccion.Add(eventoTransaccion);
                Transaccion dbTransaccion = _db.Transaccion.Single(m => m.id == eventoTransaccion.id_transaccion);
                dbTransaccion.informacion = informacionTransaccion;
                var result = Save();
                result.data = eventoTransaccion.id_transaccion;
                result.information = eventoTransaccion.id;
                return result;
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }
        public OperationResult CrearEventosTransacciones(List<Evento_transaccion> eventosTransacciones)
        {
            try
            {
                foreach (var eventoTransaccion in eventosTransacciones)
                {
                    _db.Evento_transaccion.Add(eventoTransaccion);
                }
                var result = Save();
                return result;
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }
        public OperationResult CrearEventosMasivosDeTransacciones(List<Evento_transaccion> eventosDeTransacciones)
        {
            try
            {
                string commandInsert = "insert into Evento_transaccion values ";

                List<string> commands = new List<string>();
                //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 35, 0)))
                //{

                foreach (var evento in eventosDeTransacciones)
                {
                    commands.Add(Environment.NewLine + "(" + evento.id_transaccion + "," + evento.id_empleado + "," + evento.id_evento + ",'" + evento.fecha.ToString("MM-dd-yyyy hh:mm:ss") + "','" + evento.comentario.Replace("'", "''") + "'),");
                }


                var rondas = Math.Ceiling((double)commands.Count / 1000);
                for (int a = 0; a < rondas; a++)
                {
                    var subList = commands.Skip(a * 1000).Take(1000);

                    string com = "";
                    foreach (var item in subList)
                    {
                        com += item;
                    }
                    com = com.Substring(0, com.Length - 1);
                    _db.Database.ExecuteSqlCommand(commandInsert + com);

                }

                //scope.Complete();
                var resultadoDetails = new OperationResult(OperationResultEnum.Success);
                return resultadoDetails;
                //}
            }
            catch (Exception e)
            {
                throw new DatosException("Error al crear de forma masiva eventos de transaccion", e);
            }
        }

        public OperationResult CrearTransacciones(Transaccion transaccion, List<Transaccion> transacciones1, List<Transaccion> transacciones2)
        {
            try
            {
                _db.Transaccion.Add(transaccion);
                foreach (var item in transacciones1)
                {
                    _db.Transaccion.Add(item);
                }
                foreach (var item in transacciones2)
                {
                    _db.Transaccion.Add(item);
                }

                var result = Save();
                result.data = transaccion.id;
                return result;
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }
        public OperationResult CrearTransacciones(Transaccion transaccion, List<Transaccion> transacciones1)
        {
            try
            {


                var result = Save();
                result.data = transaccion.id;
                return result;
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }

        public Comprobante ObtenerComprobanteDeTransaccion(long idTransaccion)
        {
            try
            {
                return _db.Transaccion.SingleOrDefault(t => t.id == idTransaccion).Comprobante;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Comprobante ObtenerComprobanteCero(int idSerieComprobante)
        {
            try
            {
                return _db.Comprobante.SingleOrDefault(c => c.id_serie_comprobante == idSerieComprobante && c.numero == 0);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<Tipo_transaccion_tipo_comprobante>> ObtenerTipoComprobantePorTipoDeTransaccion(int idTipoTransaccion)
        {
            try
            {
                var _asyncDb = new SigescomEntities();

                return await _asyncDb.Tipo_transaccion_tipo_comprobante.Where(tc => tc.id_tipo_transaccion == idTipoTransaccion).
                                            Include(tttc => tttc.Detalle_maestro).
                                            Include(tttc => tttc.Detalle_maestro.Serie_comprobante).ToListAsync();
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener tipo de comprobante", e);
            }
        }

        public async Task<IEnumerable<Tipo_transaccion_tipo_comprobante>> ObtenerTipoComprobantePorTipoDeTransaccion(int[] idsTipoTransaccion)
        {
            try
            {
                return await _db.Tipo_transaccion_tipo_comprobante.Where(tc => idsTipoTransaccion.Contains(tc.id_tipo_transaccion)).
                                            Include(tttc => tttc.Detalle_maestro).
                                            Include(tttc => tttc.Detalle_maestro.Serie_comprobante).ToListAsync();
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener tipo de comprobante", e);
            }
        }
        /// <summary>
        /// Obtiene el tipo de comprobante dando el id tipo de comprobante, Ejemplo paso el id de nota de credito y me devuelve los comprobantes con series de nota de credito
        /// </summary>
        /// <param name="idTipoTransaccionTipoComprobante"></param>
        /// <returns></returns>
        public IEnumerable<Tipo_transaccion_tipo_comprobante> ObtenerTipoComprobantePorTipoDeComprobante(int idTipoComprobante)
        {
            try
            {
                return _db.Tipo_transaccion_tipo_comprobante.Where(tc => tc.id_tipo_comprobante == idTipoComprobante).
                Include(tttc => tttc.Detalle_maestro).
                Include(tttc => tttc.Detalle_maestro.Serie_comprobante).ToList();
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener tipo de comprobante", e);
            }
        }

        public IEnumerable<Tipo_transaccion_tipo_comprobante> ObtenerTipoComprobantePorTipoDeComprobante(int[] idsTipoComprobante)
        {
            try
            {
                return _db.Tipo_transaccion_tipo_comprobante.Where(tc => idsTipoComprobante.Contains(tc.id_tipo_comprobante)).
                Include(tttc => tttc.Detalle_maestro).
                Include(tttc => tttc.Detalle_maestro.Serie_comprobante).ToList();
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener tipo de comprobante", e);
            }
        }

        public bool ExisteComprobante(int idActorNegocio, int idTipoComprobante, string numeroDeSerie, int numeroComprobante, int idTipoTransaccion, int idEstadoActual)
        {
            try
            {
                return _db.Comprobante.Any(c => c.id_tipo_comprobante == idTipoComprobante && c.numero_serie == numeroDeSerie && c.numero == numeroComprobante
                && c.Transaccion.Any(t => t.id_actor_negocio_externo == idActorNegocio && t.id_tipo_transaccion == idTipoTransaccion && t.id_estado_actual == idEstadoActual));
            }
            catch (Exception e)
            {
                throw new DatosException("Error al comprobar validez de comprobante", e);
            }
        }

        public Cuota ObtenerCuota(int idCuota)
        {
            try
            {
                return _db.Cuota.Single(c => c.id == idCuota);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener cuota", e);
            }
        }

        public Cuota ObtenerCuota(long idCuota)
        {
            try
            {
                return _db.Cuota.Single(c => c.id == idCuota);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener cuota", e);
            }
        }



        public Cuota ObtenerCuotaIncluidoOperacion(long idCuota)
        {
            try
            {
                return _db.Cuota.Include(c => c.Transaccion)
                    .Include(c => c.Transaccion.Actor_negocio1)
                    .Include(c => c.Transaccion.Actor_negocio1.Actor.Detalle_maestro)
                    .Include(c => c.Transaccion.Actor_negocio1.Rol)
                    .Include(c => c.Transaccion.Comprobante)
                    .Include(c => c.Transaccion.Detalle_transaccion)
                    .Include(c => c.Transaccion.Actor_negocio2)
                    .Single(c => c.id == idCuota);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener cuota", e);
            }
        }


        //System.Linq.IQueryable<System.Collections.Generic.ICollection<Cuota>> 
        /// <summary>
        /// Este metodo tiene por finalidad obtener todas la cuotas por cobrar o pargar segun sea el caso, en estado confirmado  de los actores de negocio segun el tipo de vinculo
        /// </summary>
        /// <param name="idActorDeNegocioVinculado"></param>
        /// <param name="tipoVinculo"></param>
        /// <param name="idEstado"></param>
        /// <returns></returns>

        public IEnumerable<Cuota> ObtenerCuotasConSaldo(bool porCobrar, int idEstado, int[] idsActoresDeNegocioExternos, int idTipoDeTransaccion)
        {
            try
            {
                return _db.Cuota.Include(c => c.Transaccion).Include(c => c.Pago_cuota).Where(c => c.por_cobrar == porCobrar && idsActoresDeNegocioExternos.Contains(c.Transaccion.id_actor_negocio_externo) && c.saldo > 0 && c.Transaccion.id_tipo_transaccion == idTipoDeTransaccion);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener cuotas", e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="estado_cuota"></param>
        /// <param name="porCobrar"></param>
        /// <returns></returns>
        public IEnumerable<Cuota> ObtenerCuotasConSaldo(bool porCobrar, int[] idsTiposTransaccion)//xy10
        {
            try
            {
                var resultado = _db.Cuota.Include(c => c.Transaccion)
                                         .Include(c => c.Transaccion.Actor_negocio1)
                                         .Include(c => c.Transaccion.Actor_negocio1.Actor)
                                         .Include(c => c.Transaccion.Detalle_maestro1)
                                         .Include(c => c.Comprobante)
                                         .Where(c => c.por_cobrar == porCobrar
                                         && c.saldo > 0
                                         && idsTiposTransaccion.Contains(c.Transaccion.id_tipo_transaccion));

                return resultado;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener cuotas con saldo", e);
            }
        }

        public IEnumerable<Cuota> ObtenerCuotasConSaldo(bool porCobrar, int idActorNegocio, int idTipoTransaccion)
        {
            try
            {
                var Resultado = _db.Cuota.Where(c => c.Transaccion.id_tipo_transaccion == idTipoTransaccion && c.por_cobrar == porCobrar && c.saldo > 0);
                return Resultado;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener cuotas con saldo", e);

            }
        }

        public List<Cuota> ObtenerCuotas(int[] idsCuotas)
        {
            try
            {
                return _db.Cuota.Include(c => c.Transaccion).Include(c => c.Transaccion.Transaccion2).Where(c => idsCuotas.Contains(c.id)).ToList();
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener cuotas", e);
            }
        }
        public OperationResult CrearTransacciones(List<Transaccion> transacciones)
        {
            foreach (var transaccion in transacciones)
            {
                _db.Transaccion.Add(transaccion);
            }
            return Save();
        }

        public IEnumerable<int> ObtenerIdsSeriesComprobantes(int[] idsTipoComprobante, bool estado)
        {
            return _db.Serie_comprobante.Where(sc => sc.es_vigente == estado && idsTipoComprobante.Contains(sc.id_tipo_comprobante)).Select(sc => sc.id);
        }

        public IEnumerable<int> ObtenerIdsSeriesComprobantes(int[] idsTipoComprobante)
        {
            return _db.Serie_comprobante.Where(sc => idsTipoComprobante.Contains(sc.id_tipo_comprobante)).Select(sc => sc.id);
        }

        public OperationResult CrearTipoDeCambio(DateTime fecha, int idMoneda, decimal compra, decimal venta)
        {
            throw new NotImplementedException();
        }

        public OperationResult ActualizarTipoDeCambio(Tipo_cambio tipo_cambio)
        {
            throw new NotImplementedException();
        }

        public List<Detalle_maestro> ObtenerTipoComprobante(int idMaestro)
        {
            throw new NotImplementedException();
        }

        public List<Tipo_transaccion_tipo_comprobante> obtenerTipoComprobantePorTipoDeTransaccion(int idTipoTransaccion, int idTipoComprobante)
        {
            throw new NotImplementedException();
        }

        public List<Cuenta_contable> ObtenerCuentasContable(string codigoInicialCuentaContable, int longitudCodigoCuentaContable)
        {
            throw new NotImplementedException();
        }

        public List<Tipo_cambio> ObtenerTipoDeCambio(DateTime desde, DateTime hasta)
        {
            throw new NotImplementedException();
        }

        public long ObtenerUnicoIdTransaccion(long idTransaccionPadre, int idTipoTransaccionADevolver)
        {
            return _db.Transaccion.SingleOrDefault(t => t.id_transaccion_padre == idTransaccionPadre && t.id_tipo_transaccion == idTipoTransaccionADevolver).id;
        }

        public long ObtenerUnicoIdTransaccion(long idTransaccionPadre, int idTipoTransaccionADevolver, int idUltimoEstado)
        {
            return _db.Transaccion.SingleOrDefault(t => t.id_transaccion_padre == idTransaccionPadre
            && t.id_tipo_transaccion == idTipoTransaccionADevolver
            && t.id_estado_actual == idUltimoEstado
            ).id;
        }

        public long ObtenerIdTipoTransaccion(long idTransaccion)
        {
            return _db.Transaccion.SingleOrDefault(t => t.id == idTransaccion).id_tipo_transaccion;
        }

        public Tipo_cambio ObtenerTipoDeCambioPorFecha(DateTime fecha)
        {
            throw new NotImplementedException();
        }

        public Tipo_cambio ObtenerTipoDeCambio()
        {
            throw new NotImplementedException();
        }

        public List<Existencia> ObtenerExistencias(int[] idsConceptosNegocio, int idActorNegocio)
        {

            return _db.Existencia.Where(e => e.id_punto_atencion == idActorNegocio && idsConceptosNegocio.Contains(e.id_concepto_negocio)).ToList();
        }

        public Existencia ObtenerExistencia(int idConceptoNegocio, int idPuntoAtencion)
        {

            try
            {
                return _db.Existencia.Single(e => e.id_punto_atencion == idPuntoAtencion && e.id_concepto_negocio == idConceptoNegocio);
            }
            catch (Exception e)
            {

                throw new DatosException("No se pudo obtener existencias", e);
            }

        }

        public Detalle_transaccion ObtenerDetalleTransaccion(int idActorNegocioInterno, int idTipoTransaccion, int idEstadoTransaccion, int idConceptoNegocio, string loteConceptoNegocio)
        {
            try
            {
                try
                {
                    return _db.Transaccion.SingleOrDefault(t => t.id_actor_negocio_interno == idActorNegocioInterno
                                                            && t.id_tipo_transaccion == idTipoTransaccion
                                                            && t.id_estado_actual == idEstadoTransaccion)
                                                            .Detalle_transaccion.SingleOrDefault(dt => dt.id_concepto_negocio == idConceptoNegocio
                                                                                                    && dt.lote == loteConceptoNegocio);


                }
                catch (Exception e)
                {
                    throw new DatosException("Error al obtener el detalle de transaccion", e);
                }
            }
            catch (Exception e)
            {

                throw new DatosException("No se pudo obtener existencias", e);
            }

        }

        #region Crear, Actualizar, Obtener (Serie De Comprobante)
        public OperationResult CrearSerieComprobante(Serie_comprobante serie)
        {
            try
            {
                _db.Serie_comprobante.Add(serie);
                var result = Save();
                result.data = serie.id;
                return result;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al crear serie de comprobante", e);

            }
        }
        public OperationResult ActualizarSerieComprobante(Serie_comprobante updSerie)
        {
            try
            {
                Serie_comprobante dbSerieComprobante = _db.Serie_comprobante.Single(m => m.id == updSerie.id);
                _db.Entry(dbSerieComprobante).CurrentValues.SetValues(updSerie);
                var result = Save();
                result.data = dbSerieComprobante.id;
                return result;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al actualizar serie de comprobante", e);
            }

        }
        public IEnumerable<Serie_comprobante> ObtenerSeriesComprobante()
        {
            try
            {
                return _db.Serie_comprobante.ToList();
            }
            catch (Exception e)
            {
                throw new DatosException(String.Format("Error al intentar obtener series de comprobantes"), e);
            }
        }
        public Serie_comprobante ObtenerSerieDeComprobante(int idSerieDeComprobante)
        {
            try
            {
                var serie = _db.Serie_comprobante.SingleOrDefault(sc => sc.id == idSerieDeComprobante);
                _db.Entry<Serie_comprobante>(serie).Reload();
                return serie;
            }
            catch (Exception e)
            {
                throw new DatosException(String.Format("Error al intentar obtener serie con id {0} ", idSerieDeComprobante), e);
            }
        }
        public Serie_comprobante ObtenerPrimeraSerieDeComprobantePorCentroDeAtencionYComprobante(int idTipoComprobante, int idCentroAtencion)
        {
            try
            {
                return _db.Serie_comprobante.FirstOrDefault(sc => sc.id_tipo_comprobante == idTipoComprobante && sc.id_propietario == idCentroAtencion);
            }
            catch (Exception e)
            {
                throw new DatosException(String.Format("Error al intentar obtener primera serie para para el tipo de comprobante {0} y para el centro de atencion {1}", idTipoComprobante, idCentroAtencion), e);

            }
        }
        public Serie_comprobante ObtenerPrimeraSerieDeComprobantePorCentroDeAtencionYComprobanteYPrefijoSerie(int idTipoComprobante, int idCentroAtencion, string prefijoSerie)
        {
            try
            {
                return _db.Serie_comprobante.FirstOrDefault(sc => sc.id_tipo_comprobante == idTipoComprobante && sc.id_propietario == idCentroAtencion && sc.numero.StartsWith(prefijoSerie));
            }
            catch (Exception e)
            {
                throw new DatosException(String.Format("Error al intentar obtener primera serie para para el tipo de comprobante {0} y para el centro de atencion {1}", idTipoComprobante, idCentroAtencion), e);

            }
        }
        public Serie_comprobante ObtenerPrimeraSerieDeComprobanteAutonumerable(int idTipoComprobante, int idCentroAtencion)
        {
            try
            {
                return _db.Serie_comprobante.FirstOrDefault(sc => sc.id_tipo_comprobante == idTipoComprobante && sc.id_propietario == idCentroAtencion && sc.es_autonumerable == true);
            }
            catch (Exception e)
            {
                throw new DatosException(String.Format("Error al intentar obtener primera serie autonumérica para para el tipo de comprobante {0} y para el centro de atencion {1}", idTipoComprobante, idCentroAtencion), e);
            }
        }

        public bool ExisteSerieDeComprobanteSegunTipoDeComprobante(int idTipoComprobante, string numeroDeSerie)
        {
            return _db.Serie_comprobante.Any(sc => sc.id_tipo_comprobante == idTipoComprobante && sc.numero == numeroDeSerie);
        }
        #endregion


        #region Crear, Actualizar, Obtener  (Tipo De Transaccion)

        public OperationResult CrearTipoTransaccion(Tipo_transaccion tipoDeTransaccion)
        {
            try

            {
                _db.Tipo_transaccion.Add(tipoDeTransaccion);
                var result = Save();
                result.data = tipoDeTransaccion.id;
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public OperationResult ActualizarTipoTransaccionConAccionNegocio(Tipo_transaccion tipoTransaccion)
        {
            try
            {
                Tipo_transaccion dbTipoTransaccion = _db.Tipo_transaccion.Single(tt => tt.id == tipoTransaccion.id);
                tipoTransaccion.nombre_corto = dbTipoTransaccion.nombre_corto; 
                _db.Entry(dbTipoTransaccion).CurrentValues.SetValues(tipoTransaccion);

                List<Accion_de_negocio_por_tipo_transaccion> updAcciones_de_negocio_por_tipo_transaccion = tipoTransaccion.Accion_de_negocio_por_tipo_transaccion.ToList();
                List<Accion_de_negocio_por_tipo_transaccion> dbAcciones_de_negocio_por_tipo_transaccion = dbTipoTransaccion.Accion_de_negocio_por_tipo_transaccion.ToList();

                foreach (var dbadnptt in dbAcciones_de_negocio_por_tipo_transaccion)
                {
                    if (updAcciones_de_negocio_por_tipo_transaccion.Any(updadnptt => updadnptt.id == dbadnptt.id))
                    {
                        var tttc = updAcciones_de_negocio_por_tipo_transaccion.Single(updadnptt => updadnptt.id == dbadnptt.id);
                        _db.Entry(dbadnptt).CurrentValues.SetValues(tttc);
                    }
                    else
                    {
                        _db.Accion_de_negocio_por_tipo_transaccion.Remove(dbadnptt);
                    }
                }
                foreach (var updadnptt in updAcciones_de_negocio_por_tipo_transaccion)
                {
                    if (!dbAcciones_de_negocio_por_tipo_transaccion.Any(d => d.id == updadnptt.id))
                    {
                        _db.Accion_de_negocio_por_tipo_transaccion.Add(updadnptt);
                    }
                }

                var result = Save();
                result.data = tipoTransaccion.id;
                return result;
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }
        public IEnumerable<Tipo_transaccion> ObtenerTiposDeTransaccionIncluidoAccionDeNegocio()
        {
            try
            {
                return _db.Tipo_transaccion.Include(tt => tt.Accion_de_negocio_por_tipo_transaccion)
                                           .Include(tt => tt.Accion_de_negocio_por_tipo_transaccion.Select(an => an.Accion_de_negocio))
                                           .OrderBy(tp => tp.nombre);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Tipo_transaccion ObtenerTipoDeTransaccionIncluidoAccionDeNegocio(int idTipoDeTransaccion)
        {
            try
            {
                return _db.Tipo_transaccion.Include(tt => tt.Accion_de_negocio_por_tipo_transaccion)
                                           .Include(tt => tt.Accion_de_negocio_por_tipo_transaccion.Select(an => an.Accion_de_negocio))
                                           .SingleOrDefault(tdt => tdt.id == idTipoDeTransaccion);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<ItemGenerico> ObtenerTipoDeTransaccionPorAccionDeNegocio(int idAccionNegocio, bool valor)
        {
            try
            {
                return _db.Accion_de_negocio_por_tipo_transaccion.Where(antt => antt.id_accion_de_negocio == idAccionNegocio && antt.valor == valor).Select(an => an.Tipo_transaccion).Select(an =>
                   new ItemGenerico
                   {
                       Id = an.id,
                       Nombre = an.nombre
                   });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion


        #region Obtener Accion de Negocio

        public IEnumerable<Accion_de_negocio> ObtenerAccionesDeNegocio()
        {
            try
            {
                return _db.Accion_de_negocio.OrderBy(an => an.nombre);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<long> ObtenerTransaccionesInclusiveActoresYDetalleMaestroYEstado(int[] idsTiposTransaccion, int[] idsTiposComprobantes, int idEstado)
        {
            throw new NotImplementedException();
        }

        #endregion

        public IEnumerable<Deuda_Actor_Negocio> ObtenerDeudasActorNegocioPorVinculoActorNegocio(int idTipoDeTransaccion, int idActorNegocioPrincipal, int tipoDeVinculo, DateTime fecha, int idParametroComprobantePredeterminado)
        {
            try
            {
                var deudas = _db.Vinculo_Actor_Negocio.Where(van => van.id_actor_negocio_principal == idActorNegocioPrincipal && van.tipo_vinculo == tipoDeVinculo && van.es_vigente == true)
                                        .Select(van => van.Actor_negocio1)

                                        .Select(an => new Deuda_Actor_Negocio()
                                        {
                                            Actor = an.Actor,
                                            ActorNegocio = an,
                                            TotalOrden = an.Transaccion1.Where(t => t.fecha_inicio <= fecha).SelectMany(t => t.Cuota).Where(c => c.por_cobrar == true)
                                                                        .Sum(c => c.total),
                                            TotalPagoCuota = an.Transaccion1.SelectMany(t => t.Cuota).Where(c => c.por_cobrar == true).SelectMany(c => c.Pago_cuota).Where(pg => pg.Transaccion.fecha_inicio <= fecha).Sum(pc => pc.importe),
                                            IdTipoComprobantePredeterminado = an.Parametro_actor_negocio.FirstOrDefault(pan => pan.id_parametro == idParametroComprobantePredeterminado) == null ? "0" : an.Parametro_actor_negocio.FirstOrDefault(pan => pan.id_parametro == idParametroComprobantePredeterminado).valor
                                        });
                return deudas;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener deudas", e);
            }
        } 



       

      


        


        public DateTime? ObtenerFechaPrimeraTransaccion(int idActorNegocioInterno)
        {
            var result = _db.Transaccion.OrderBy(t => t.fecha_inicio).FirstOrDefault(t => t.id_actor_negocio_interno == idActorNegocioInterno);
            return result != null ? (DateTime?)result.fecha_inicio : null;
        }

        public DateTime? ObtenerFechaPrimeraTransaccion()
        {
            var result = _db.Transaccion.OrderBy(t => t.fecha_inicio).FirstOrDefault();
            return result != null ? (DateTime?)result.fecha_inicio : null;
        }

        public DateTime? ObtenerFechaPrimeraTransaccionGenerica(int idTipoTransaccionGenerica)
        {
            var result = _db.Transaccion.Where(t => t.id_tipo_transaccion == idTipoTransaccionGenerica).OrderByDescending(t => t.id).FirstOrDefault();
            return result != null ? (DateTime?)result.fecha_inicio : null;
        }

        public DateTime? ObtenerFechaUltimaTransaccionDeAlgunoDeLosActoresVinculadosSegunTransaccion1(int idActorNegocioPrincipal, int idTipoTransaccionCobranzaFacturasClientes, DateTime fechaMaxima)
        {
            DateTime? fecha = null;
            try
            {

                var ultimaTransaccion = _db.Vinculo_Actor_Negocio.Where(van => van.id_actor_negocio_principal == idActorNegocioPrincipal)
                    .Select(van => van.Actor_negocio1).SelectMany(an => an.Transaccion1)
                    .Where(t => t.id_tipo_transaccion == idTipoTransaccionCobranzaFacturasClientes && t.fecha_inicio <= fechaMaxima)
                    .OrderByDescending(t => t.fecha_inicio).FirstOrDefault();


                fecha = ultimaTransaccion != null ? (DateTime?)ultimaTransaccion.fecha_inicio : null;
                return fecha;

            }
            catch (Exception e)
            {

                throw new DatosException("Error al intentar obtener la fecha de inicio de la Ultima transaccion", e);
            }
        }

        public DateTime? ObtenerFechaUltimaTransaccionActorNegocioExterno(int idActorNegocioExterno, int idTipoTransaccionCobranzaFacturasClientes, DateTime fechaMaxima)
        {
            DateTime? fecha = null;
            try
            {

                var ultimaTransaccion = _db.Actor_negocio.Where(an => an.id == idActorNegocioExterno).SelectMany(an => an.Transaccion1)
                    .Where(t => t.id_tipo_transaccion == idTipoTransaccionCobranzaFacturasClientes && t.fecha_inicio <= fechaMaxima)
                    .OrderByDescending(t => t.fecha_inicio).FirstOrDefault();
                fecha = ultimaTransaccion != null ? (DateTime?)ultimaTransaccion.fecha_inicio : null;
                return fecha;

            }
            catch (Exception e)
            {

                throw new DatosException("Error al intentar obtener la fecha de inicio de la Ultima transaccion", e);
            }
        }

        public DateTime? ObtenerFechaInicioUltimaTransaccion(int idActorNegocioInterno, int idTipoDeTransaccion, int idUltimoEstado, DateTime fechaMaxima, int idDetalleMaestroParametroTransaccion, string valorParametroTransaccion)
        {
            DateTime? fecha = null;
            try
            {
                var ultimaTransaccion = _db.Transaccion.Where(t => t.id_actor_negocio_interno == idActorNegocioInterno
                && t.id_tipo_transaccion == idTipoDeTransaccion
                && t.fecha_inicio <= fechaMaxima
                && t.Parametro_transaccion.Any(pt => pt.id_parametro == idDetalleMaestroParametroTransaccion)
                && t.Parametro_transaccion.FirstOrDefault(pt => pt.id_parametro == idDetalleMaestroParametroTransaccion).valor == valorParametroTransaccion
                && t.id_estado_actual == idUltimoEstado
                ).OrderByDescending(t => t.fecha_inicio).FirstOrDefault();
                fecha = ultimaTransaccion != null ? (DateTime?)ultimaTransaccion.fecha_inicio : null;
                return fecha;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener la fecha de inicio de la Ultima transaccion", e);
            }

        }

        public DateTime? ObtenerFechaInicioUltimaTransaccion(int idActorNegocioExterno, int idTipoDeTransaccion, int idUltimoEstado, DateTime fechaMaxima)
        {
            DateTime? fecha = null;
            try
            {

                var ultimaTransaccion = _db.Transaccion.Where(t => t.id_actor_negocio_externo == idActorNegocioExterno
                && t.id_tipo_transaccion == idTipoDeTransaccion
                && t.fecha_inicio <= fechaMaxima
                && t.id_estado_actual == idUltimoEstado
                )
                .OrderByDescending(t => t.fecha_inicio).FirstOrDefault();
                fecha = ultimaTransaccion != null ? (DateTime?)ultimaTransaccion.fecha_inicio : null;
                return fecha;
            }
            catch (Exception e)
            {

                throw new DatosException("Error al intentar obtener la fecha de inicio de la Ultima transaccion", e);
            }
        }



        #region VENTAS Y COBROS MASIVOS
        public IEnumerable<Transaccion> ObtenerTransaccionesInclusiveDetalleTransaccionComprobanteYActoresNegocio(int idTipoDeTransaccion, int[] idsEstadoDeTransaccion, int idParametroDeTransaccion, string valorParametroDeTransaccion, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                var transacciones = _db.Transaccion.
                                              Include(t => t.Detalle_transaccion).
                                              Include(t => t.Comprobante).
                                              Include(t => t.Comprobante.Detalle_maestro).
                                              Include(t => t.Detalle_maestro1).
                                              Include(t => t.Actor_negocio1).//Externo
                                              Include(t => t.Actor_negocio).//Empleado
                                              Include(t => t.Actor_negocio2).//Centro Atencion
                                              Where(t => t.id_tipo_transaccion == idTipoDeTransaccion && idsEstadoDeTransaccion.Contains((int)t.id_estado_actual) &&
                                              t.Parametro_transaccion.Count(pt => pt.id_parametro == idParametroDeTransaccion) == 1 && t.Parametro_transaccion.FirstOrDefault(pt => pt.id_parametro == idParametroDeTransaccion).valor == valorParametroDeTransaccion &&
                                              t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta);
                return transacciones;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener transacciones detalle transaccion, comprobante y actores de negocio", e);
            }
        }

        public IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresNegocioYPagoCuota(int idTipoDeTransaccion, int[] idsEstadoDeTransaccion, int idParametroDeTransaccion, string valorParametroDeTransaccion, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                return _db.Transaccion.
                                        Include(t => t.Actor_negocio2).
                                        Include(t => t.Actor_negocio).
                                        Include(t => t.Actor_negocio1).
                                        Include(t => t.Pago_cuota).
                                        Include(t => t.Pago_cuota.Select(c => c.Cuota)).
                                        Include(t => t.Pago_cuota.Select(c => c.Cuota).Select(tr => tr.Transaccion).Select(com => com.Comprobante)).
                                        Where(t => t.id_tipo_transaccion == idTipoDeTransaccion && idsEstadoDeTransaccion.Contains(t.Estado_transaccion.
                                        OrderByDescending(est => est.id).FirstOrDefault().id_estado) &&
                                        t.Parametro_transaccion.Count(pt => pt.id_parametro == idParametroDeTransaccion) == 1 && t.Parametro_transaccion.
                                        FirstOrDefault(pt => pt.id_parametro == idParametroDeTransaccion).valor == valorParametroDeTransaccion &&
                                        t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener transacciones inclusive con actores de negocio y pago de cuotas", e);
            }
        }

        public IEnumerable<Transaccion> ObtenerTransaccionesInclusiveDetalleTransaccionComprobanteYActoresNegocio(int idTipoDeTransaccion, int[] idsEstadoDeTransaccion, int idParametroDeTransaccion, string valorParametroDeTransaccion, DateTime fecha)
        {
            try
            {
                return _db.Transaccion.
                                    Include(t => t.Detalle_transaccion).
                                    Include(t => t.Comprobante).
                                    Include(t => t.Comprobante.Detalle_maestro).
                                    Include(t => t.Detalle_maestro1).
                                     Include(t => t.Actor_negocio1).//Externo
                                    Include(t => t.Actor_negocio).//Empleado
                                    Include(t => t.Actor_negocio2).//Centro Atencion
                                    Where(t => t.id_tipo_transaccion == idTipoDeTransaccion
                                                && idsEstadoDeTransaccion.Contains((int)t.id_estado_actual)
                                                && t.Parametro_transaccion.Count(pt => pt.id_parametro == idParametroDeTransaccion) == 1
                                                && t.Parametro_transaccion.FirstOrDefault(pt => pt.id_parametro == idParametroDeTransaccion).valor == valorParametroDeTransaccion
                                                && t.fecha_registro_sistema == fecha
                                    );
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener transacciones inclusive con actores de negocio y pago de cuotas", e);
            }

        }

        public IEnumerable<Transaccion> ObtenerTransaccionesInclusiveDetalleTransaccionComprobanteYActoresNegocio(int idTipoDeTransaccion, int[] idsEstadoDeTransaccion, int idParametroDeTransaccion, string valorParametroDeTransaccion, int idTransaccion)
        {
            try
            {
                return _db.Transaccion
                                .Where(t => t.id == idTransaccion)
                               .SelectMany(t1 => t1.Transaccion1)
                               .SelectMany(t1 => t1.Transaccion1)
                               .Where(t1 => t1.id_tipo_transaccion == idTipoDeTransaccion
                                       && idsEstadoDeTransaccion.Contains(t1.Estado_transaccion.OrderByDescending(est => est.id).FirstOrDefault().id_estado)
                                       && t1.Parametro_transaccion.Count(pt => pt.id_parametro == idParametroDeTransaccion) == 1
                                       && t1.Parametro_transaccion.FirstOrDefault(pt => pt.id_parametro == idParametroDeTransaccion).valor == valorParametroDeTransaccion
                               ).
                                    Include(t1 => t1.Detalle_transaccion).
                                    Include(t1 => t1.Comprobante).
                                    Include(t1 => t1.Comprobante.Detalle_maestro).
                                    Include(t1 => t1.Detalle_maestro1).
                                     Include(t1 => t1.Actor_negocio1).//Externo
                                    Include(t1 => t1.Actor_negocio).//Empleado
                                    Include(t1 => t1.Actor_negocio2)//Centro Atencion
                               ;

            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener transacciones inclusive con actores de negocio y pago de cuotas", e);
            }
        }

        public IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresNegocioYPagoCuota(int idTipoDeTransaccion, int idParametroDeTransaccion, string valorParametroDeTransaccion, DateTime fecha)
        {
            try
            {
                return _db.Transaccion.
                                    Include(t => t.Actor_negocio2).
                                    Include(t => t.Actor_negocio).
                                    Include(t => t.Actor_negocio1).
                                    Include(t => t.Pago_cuota).
                                    Include(t => t.Pago_cuota.Select(c => c.Cuota)).
                                    Include(t => t.Pago_cuota.Select(c => c.Cuota).Select(tr => tr.Transaccion).Select(com => com.Comprobante)).
                                    Where(t => t.id_tipo_transaccion == idTipoDeTransaccion
                                    && t.Parametro_transaccion.Count(pt => pt.id_parametro == idParametroDeTransaccion) == 1
                                    && t.Parametro_transaccion.FirstOrDefault(pt => pt.id_parametro == idParametroDeTransaccion).valor == valorParametroDeTransaccion
                                    && DateTime.Compare(t.fecha_registro_sistema, fecha) == 0);

            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener transacciones inclusive con actores de negocio y pago de cuotas", e);
            }

        }

        public IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresNegocioYPagoCuota(int idTipoDeTransaccion, int[] idsEstadoDeTransaccion, int idParametroDeTransaccion, string valorParametroDeTransaccion, int idTransaccion)
        {
            try
            {
                return _db.Transaccion
                                   .Where(t => t.id == idTransaccion)
                                   .SelectMany(t1 => t1.Transaccion1)
                                    .Where(t => t.id_tipo_transaccion == idTipoDeTransaccion
                                    && t.Parametro_transaccion.Count(pt => pt.id_parametro == idParametroDeTransaccion) == 1
                                    && t.Parametro_transaccion.FirstOrDefault(pt => pt.id_parametro == idParametroDeTransaccion).valor == valorParametroDeTransaccion
                                   ).
                                    Include(t => t.Actor_negocio2).
                                    Include(t => t.Actor_negocio).
                                    Include(t => t.Actor_negocio1).
                                    Include(t => t.Pago_cuota).
                                    Include(t => t.Pago_cuota.Select(c => c.Cuota)).
                                    Include(t => t.Pago_cuota.Select(c => c.Cuota).Select(tr => tr.Transaccion).Select(com => com.Comprobante));
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener transacciones inclusive con actores de negocio y pago de cuotas", e);
            }
        }

        public IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresNegocioYPagoCuota(int idTipoDeTransaccion, int idParametroDeTransaccion, string valorParametroDeTransaccion, int idTransaccion)
        {
            try
            {
                return _db.Transaccion
                                   .Where(t => t.id == idTransaccion)
                                   .SelectMany(t1 => t1.Transaccion1)
                                    .Where(t => t.id_tipo_transaccion == idTipoDeTransaccion
                                    && t.Parametro_transaccion.Count(pt => pt.id_parametro == idParametroDeTransaccion) == 1
                                    && t.Parametro_transaccion.FirstOrDefault(pt => pt.id_parametro == idParametroDeTransaccion).valor == valorParametroDeTransaccion
                                   ).
                                    Include(t => t.Actor_negocio2).
                                    Include(t => t.Actor_negocio).
                                    Include(t => t.Actor_negocio1).
                                    Include(t => t.Pago_cuota).
                                    Include(t => t.Pago_cuota.Select(c => c.Cuota)).
                                    Include(t => t.Pago_cuota.Select(c => c.Cuota).Select(tr => tr.Transaccion).Select(com => com.Comprobante));

            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener transacciones inclusive con actores de negocio y pago de cuotas", e);
            }

        }

        public IEnumerable<Transaccion> ObtenerTransaccionesInclusiveActoresNegocioYPagoCuota(int idTipoDeTransaccion, int idParametroDeTransaccion, string valorParametroDeTransaccion, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                return _db.Transaccion.
                                        Include(t => t.Actor_negocio2).
                                        Include(t => t.Actor_negocio).
                                        Include(t => t.Actor_negocio1).
                                        Include(t => t.Pago_cuota).
                                        Include(t => t.Pago_cuota.Select(c => c.Cuota)).
                                        Include(t => t.Pago_cuota.Select(c => c.Cuota)
                                                      .Select(tr => tr.Transaccion)
                                                      .Select(com => com.Comprobante)
                                                )
                                       .Where(t => t.id_tipo_transaccion == idTipoDeTransaccion
                                                  && t.Parametro_transaccion.Count(pt => pt.id_parametro == idParametroDeTransaccion) == 1
                                                  && t.Parametro_transaccion.FirstOrDefault(pt => pt.id_parametro == idParametroDeTransaccion).valor == valorParametroDeTransaccion
                                                  && t.fecha_inicio >= fechaDesde
                                                  && t.fecha_inicio <= fechaHasta
                                             );
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener transacciones inclusive con actores de negocio y pago de cuotas", e);
            }

        }

        #endregion

        #region METODOS REPORTE DE VENTAS ADSOFT Y AFEX

        public IEnumerable<Venta_Cliente> ObtenerVentasClienteConOperacionDeReferenciaSegunElEstadoQueDebeTener(int[] idsTiposComprobantes, int[] idsTiposTransaccion, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                return _db.Transaccion
                           .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta
                            && idsTiposTransaccion.Contains(t.id_tipo_transaccion)
                            && idsTiposComprobantes.Contains(t.Comprobante.id_tipo_comprobante))
                            //&& t.id_estado_actual == idUltimoEstado
                            .Select(t => new Venta_Cliente()
                            {
                                Id = t.id,
                                Anyo = t.fecha_inicio.Year,
                                Mes = t.fecha_inicio.Month,
                                Dia = t.fecha_inicio.Day,
                                FechaEmision = t.fecha_inicio,
                                IdTipoComprobante = t.Comprobante.id_tipo_comprobante,
                                CodigoComprobante = t.Comprobante.Detalle_maestro.codigo,
                                IdSerie = t.Comprobante.id_serie_comprobante,
                                NumeroSerie = t.Comprobante.numero_serie,
                                IdTipoDocumento = t.Actor_negocio1.Actor.id_documento_identidad,
                                NumeroComprobante = t.Comprobante.numero,
                                IdActorNegocioExterno = t.id_actor_negocio_externo,
                                CodigoDocumento = t.Actor_negocio1.Actor.Detalle_maestro.codigo,
                                NumeroDocumento = t.Actor_negocio1.Actor.numero_documento_identidad,
                                PrimerNombre = t.Actor_negocio1.Actor.primer_nombre,
                                Igv = t.importe8,
                                ValorIcbper = t.importe10,
                                ValorDeVenta = t.importe_total - t.importe8 - t.importe10,
                                ImporteTotal = t.importe_total,
                                IdEstadoActual = (int)t.id_estado_actual,
                                IdEstadoAnteriorAlActual = t.Estado_transaccion.OrderByDescending(est => est.id).Skip(1).Take(1).FirstOrDefault().id_estado,
                                CodigoMoneda = t.Detalle_maestro1.codigo,
                                NumeroSerieReferencia = t.Transaccion3.Comprobante.numero_serie,
                                NumeroComprobanteReferencia = t.Transaccion3.Comprobante.numero,
                                FechaEmisionReferencia = t.Transaccion3.fecha_inicio,
                                CodigoComprobanteReferencia = t.Transaccion3.Comprobante.Detalle_maestro.codigo
                            }
                            );
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener ventas por cliente", e);
            }
        }

        public IEnumerable<Venta_Cliente> ObtenerVentasClienteConOperacionDeReferenciaSegunElUltimoEstado(int[] idsTiposComprobantes, int[] idsTiposTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                return _db.Transaccion
                           .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta
                            && idsTiposTransaccion.Contains(t.id_tipo_transaccion)
                            && idsTiposComprobantes.Contains(t.Comprobante.id_tipo_comprobante)
                            && t.id_estado_actual == idUltimoEstado
                            )
                            .Select(t => new Venta_Cliente()
                            {
                                Id = t.id,

                                Anyo = t.fecha_inicio.Year,
                                Mes = t.fecha_inicio.Month,
                                Dia = t.fecha_inicio.Day,
                                IdTipoComprobante = t.Comprobante.id_tipo_comprobante,
                                CodigoComprobante = t.Comprobante.Detalle_maestro.codigo,
                                NombreCortoComprobante = t.Comprobante.Detalle_maestro.valor,
                                IdSerie = t.Comprobante.id_serie_comprobante,
                                NumeroSerie = t.Comprobante.numero_serie,
                                IdTipoDocumento = t.Actor_negocio1.Actor.id_documento_identidad,
                                NumeroComprobante = t.Comprobante.numero,
                                IdActorNegocioExterno = t.id_actor_negocio_externo,
                                CodigoDocumento = t.Actor_negocio1.Actor.Detalle_maestro.codigo,
                                NumeroDocumento = t.Actor_negocio1.Actor.numero_documento_identidad,
                                PrimerNombre = t.Actor_negocio1.Actor.primer_nombre,
                                Igv = t.importe8,
                                ValorIcbper = t.importe10,
                                ValorDeVenta = t.importe_total - t.importe8 - t.importe10,
                                ImporteTotal = t.importe_total,
                                IdEstadoActual = (int)t.id_estado_actual,
                                IdEstadoAnteriorAlActual = t.Estado_transaccion.OrderByDescending(est => est.id).Skip(1).Take(1).FirstOrDefault().id_estado,
                                CodigoMoneda = t.Detalle_maestro1.codigo,
                                NumeroSerieReferencia = t.Transaccion3 == null ? null : t.Transaccion3.Comprobante.numero_serie,
                                NumeroComprobanteReferencia = t.Transaccion3 == null ? 0 : t.Transaccion3.Comprobante.numero,
                                FechaEmisionReferencia = t.Transaccion3 == null ? new DateTime() : t.Transaccion3.fecha_inicio,
                                CodigoComprobanteReferencia = t.Transaccion3 == null ? null : t.Transaccion3.Comprobante.Detalle_maestro.codigo,
                            }
                            );
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener ventas por cliente", e);
            }
        }

        public IEnumerable<Venta_Cliente> ObtenerVentasCliente(int[] idsTiposComprobantes, int[] idsTiposTransaccion, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {

                return _db.Transaccion
                           .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta
                            && idsTiposTransaccion.Contains(t.id_tipo_transaccion)
                            && idsTiposComprobantes.Contains(t.Comprobante.id_tipo_comprobante)

                            )
                            .Select(t => new Venta_Cliente()
                            {
                                Id = t.id,
                                Anyo = t.fecha_inicio.Year,
                                Mes = t.fecha_inicio.Month,
                                Dia = t.fecha_inicio.Day,
                                IdTipoTransaccion = t.id_tipo_transaccion,
                                IdTipoComprobante = t.Comprobante.id_tipo_comprobante,
                                CodigoComprobante = t.Comprobante.Detalle_maestro.codigo,
                                IdSerie = t.Comprobante.id_serie_comprobante,
                                NumeroSerie = t.Comprobante.numero_serie,
                                IdTipoDocumento = t.Actor_negocio1.Actor.id_documento_identidad,
                                NumeroComprobante = t.Comprobante.numero,
                                IdActorNegocioExterno = t.id_actor_negocio_externo,
                                CodigoDocumento = t.Actor_negocio1.Actor.Detalle_maestro.codigo,
                                NumeroDocumento = t.Actor_negocio1.Actor.numero_documento_identidad,
                                PrimerNombre = t.Actor_negocio1.Actor.primer_nombre,
                                Igv = t.importe8,
                                ValorIcbper = t.importe10,
                                ValorDeVenta = t.importe_total - t.importe8 - t.importe10,
                                ImporteTotal = t.importe_total,
                                IdEstadoActual = (int)t.id_estado_actual,
                                IdEstadoAnteriorAlActual = t.Estado_transaccion.OrderByDescending(est => est.id).Skip(1).Take(1).FirstOrDefault().id_estado,
                                CodigoMoneda = t.Detalle_maestro1.codigo

                            }
                            );
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener ventas por cliente", e);
            }
        }

        public IEnumerable<Venta_Cliente> ObtenerVentasClienteSegunElEstadoQueDebeTener(int[] idsTiposComprobantes, int[] idsTiposTransaccion, int idUltimoEstado, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                return _db.Transaccion
                           .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta
                            && idsTiposTransaccion.Contains(t.id_tipo_transaccion)
                            && idsTiposComprobantes.Contains(t.Comprobante.id_tipo_comprobante)
                            && t.id_estado_actual == idUltimoEstado)
                            .Select(t => new Venta_Cliente()
                            {
                                Id = t.id,
                                Anyo = t.fecha_inicio.Year,
                                Mes = t.fecha_inicio.Month,
                                Dia = t.fecha_inicio.Day,
                                FechaEmision = t.fecha_inicio,
                                IdTipoComprobante = t.Comprobante.id_tipo_comprobante,
                                CodigoComprobante = t.Comprobante.Detalle_maestro.codigo,
                                NombreCortoComprobante = t.Comprobante.Detalle_maestro.valor,
                                IdSerie = t.Comprobante.id_serie_comprobante,
                                NumeroSerie = t.Comprobante.numero_serie,
                                IdTipoDocumento = t.Actor_negocio1.Actor.id_documento_identidad,
                                NumeroComprobante = t.Comprobante.numero,
                                IdActorNegocioExterno = t.id_actor_negocio_externo,
                                CodigoDocumento = t.Actor_negocio1.Actor.Detalle_maestro.codigo,
                                NumeroDocumento = t.Actor_negocio1.Actor.numero_documento_identidad,
                                PrimerNombre = t.Actor_negocio1.Actor.primer_nombre,
                                Igv = t.importe8,
                                ValorIcbper = t.importe10,
                                ValorDeVenta = t.importe_total - t.importe8 - t.importe10,
                                ImporteTotal = t.importe_total,
                                IdEstadoActual = (int)t.id_estado_actual,
                                IdEstadoAnteriorAlActual = t.Estado_transaccion.OrderByDescending(est => est.id).Skip(1).Take(1).FirstOrDefault().id_estado,
                                CodigoMoneda = t.Detalle_maestro1.codigo,
                            }
                            );
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener ventas por cliente", e);
            }
        }


        #endregion

        #region  DETALLES _TRANSACCION

        public OperationResult ActualizarDetallesTransaccion(List<Detalle_transaccion> detallesTransaccion)
        {
            try
            {
                SetActualizarDetallesTransaccion(detallesTransaccion);
                OperationResult result = new OperationResult();
                result = Save();
                return result;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al actualizar detalles de transaccion", e);
            }
        }
        public OperationResult CrearActualizarDetallesTransaccion(List<Detalle_transaccion> detallesTransaccionParaCrear, List<Detalle_transaccion> detallesTransaccionParaActualizar)
        {
            try
            {
                OperationResult result = new OperationResult();
                _db.Detalle_transaccion.AddRange(detallesTransaccionParaCrear);
                if (detallesTransaccionParaActualizar != null)
                {
                    SetActualizarDetallesTransaccion(detallesTransaccionParaActualizar);
                }
                result = Save();
                return result;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al crear y  actualizar detalles de transaccion", e);
            }
        }

        public void SetActualizarDetallesTransaccion(IEnumerable<Detalle_transaccion> detallesTransaccion)
        {
            var ids_updDetalles = detallesTransaccion.Select(d => d.id).ToArray();
            var db_detalles = _db.Detalle_transaccion.Where(dt => ids_updDetalles.Contains(dt.id)).ToArray();
            foreach (var item in detallesTransaccion)
            {

                Detalle_transaccion dbDetalle_Transaccion = db_detalles.Single(dt => dt.id == item.id);
                _db.Entry(dbDetalle_Transaccion).CurrentValues.SetValues(item);
            }
        }

        #endregion

        #region CARACTERISTICAS PROPIAS

        public IEnumerable<ValorDetalleMaestroDetalleTransaccion> ObtenerValoresDetallesMaestroDetallesTransaccionn(int idActorNegocioInterno, int idTipoTransaccion, int idConceptoNegocio)
        {
            try
            {
                //Enviamos el idActorNegocioInterno donde se realizaron la compra y sea el tipo de transaccion orden de compra y el idconceptonegocio 
                return _db.Transaccion.Where(t => t.id_actor_negocio_interno == idActorNegocioInterno && t.id_tipo_transaccion == idTipoTransaccion)
                                      .SelectMany(t => t.Detalle_transaccion)
                                      .Where(dt => dt.id_concepto_negocio == idConceptoNegocio)
                                      .SelectMany(dt => dt.Valor_detalle_maestro_detalle_transaccion)
                                      .Select(vdmdt => new ValorDetalleMaestroDetalleTransaccion()
                                      {
                                          Id = vdmdt.id,
                                          Numero = vdmdt.numero,
                                          Valor = vdmdt.valor,
                                          IdDetalleMaestro = vdmdt.id_detalle_maestro,
                                          IdDetalleTransaccion = vdmdt.id_detalle_transaccion,
                                      });
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener valores detalles de maestro detalles de transaccion", e);
            }
        }

        #endregion

        #region INVENTARIO FISICO



        



        public int ObtenerNumeroDeTransaccionesExistentes(int idActorNegocioInterno, int idTipoTransaccion, int idUltimoEstado)
        {
            try
            {
                return _db.Transaccion.Count(t => t.id_actor_negocio_interno == idActorNegocioInterno
                                                && t.id_tipo_transaccion == idTipoTransaccion
                                                && t.id_estado_actual == idUltimoEstado);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener transaccion inclusive detalles de transaccion", e);
            }
        }

        

        public void MarcarSerieComoModificada(Serie_comprobante serie)
        {
            _db.Entry(serie).State = EntityState.Modified;
        }




        public OperationResult CrearTransaccionCrearActualizarDetallesTransaccion(Transaccion transaccion, List<Detalle_transaccion> detallesTransaccionParaCrear, List<Detalle_transaccion> detallesTransaccionParaActualizar, Transaccion transaccionActualizar)
        {
            try
            {
                //Agregar la transaccion
                _db.Transaccion.Add(transaccion);
                if (detallesTransaccionParaCrear != null)
                {
                    _db.Detalle_transaccion.AddRange(detallesTransaccionParaCrear);
                }
                if (detallesTransaccionParaActualizar != null)
                {
                    SetActualizarDetallesTransaccion(detallesTransaccionParaActualizar);
                }

                if (transaccionActualizar != null)
                {
                    _db.Entry(transaccionActualizar).State = EntityState.Modified;
                }
                OperationResult result = Save();

                return result;
            }

            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();

                var typeModel = ObjectContext.GetObjectType(entry.Entity.GetType());

                if (typeModel.Name == typeof(Detalle_transaccion).Name)
                {
                    return new OperationResult(new DetalleTransaccionException(ex, (Detalle_transaccion)entry.Entity, transaccion));
                }
                else
                {
                    return new OperationResult(new SerieComprobanteException(ex, (Serie_comprobante)entry.Entity));
                }
            }
            catch (Exception e)
            {
                throw new DatosException("Error al actualizar inventario fisico", e);
            }
            finally
            {
                //_db.Configuration.AutoDetectChangesEnabled = true;

            }
        }
        public OperationResult CrearTransaccionActualizarTransaccionCrearEstadoTransaccion(Transaccion transaccionCrear, Transaccion transaccionActualizar, Estado_transaccion estadoTransaccionCrear)
        {
            try
            {
                //Agregar la transaccion
                _db.Transaccion.Add(transaccionCrear);
                _db.Estado_transaccion.Add(estadoTransaccionCrear);
                //Transaccion dbTransaccion = _db.Transaccion.Single(m => m.id == transaccionActualizar.id);
                //_db.Entry(dbTransaccion).CurrentValues.SetValues(transaccionActualizar);
                _db.Entry(transaccionActualizar).State = EntityState.Modified;
                OperationResult result = Save();
                return result;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al crear transaccion", e);
            }
        }
        public OperationResult CrearTransaccionesCrearEstadosTransaccionCrearEstadosCuotaCrearActualizarDetallesTransaccion(List<Transaccion> transaccionesParaCrear, List<Estado_transaccion> estadosDeTransaccionParaCrear, List<Estado_cuota> estadosDeCuotaParaCrear, List<Detalle_transaccion> detallesTransaccionParaCrear, List<Detalle_transaccion> detallesTransaccionParaActualizar, List<Actor_negocio> actoresNegocioParaActualizar)
        {
            try
            {
                if (transaccionesParaCrear != null)
                {
                    _db.Transaccion.AddRange(transaccionesParaCrear);
                }
                if (estadosDeTransaccionParaCrear != null)
                {
                    _db.Estado_transaccion.AddRange(estadosDeTransaccionParaCrear);
                }
                if (estadosDeCuotaParaCrear != null)
                {
                    _db.Estado_cuota.AddRange(estadosDeCuotaParaCrear);
                }
                _db.Detalle_transaccion.AddRange(detallesTransaccionParaCrear);
                SetActualizarDetallesTransaccion(detallesTransaccionParaActualizar);

                if (actoresNegocioParaActualizar != null && actoresNegocioParaActualizar.Count > 0) actoresNegocioParaActualizar.ForEach(t => _db.Entry(t).State = EntityState.Modified);

                OperationResult result = Save();
                return result;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();

                var typeModel = ObjectContext.GetObjectType(entry.Entity.GetType());

                if (typeModel.Name == typeof(Detalle_transaccion).Name)
                {
                    return new OperationResult(new DetalleTransaccionException(ex, (Detalle_transaccion)entry.Entity, transaccionesParaCrear.First()));
                }
                else
                {
                    return new OperationResult(new SerieComprobanteException(ex, (Serie_comprobante)entry.Entity));
                }
            }
            catch (Exception e)
            {
                throw new DatosException("Error al actualizar inventario fisico", e);
            }
        }

        public OperationResult CrearTransaccionCrearActualizarDetallesTransaccionCrearEstadoTransaccionCrearEstadosCuota(Transaccion transaccion, List<Detalle_transaccion> detallesTransaccionParaCrear, List<Detalle_transaccion> detallesTransaccionParaActualizar, Estado_transaccion estadoDeTransaccion, List<Estado_cuota> estadosDeCuota)
        {
            try
            {
                //Agregar la transaccion
                _db.Transaccion.Add(transaccion);
                _db.Detalle_transaccion.AddRange(detallesTransaccionParaCrear);
                SetActualizarDetallesTransaccion(detallesTransaccionParaActualizar);
                _db.Estado_transaccion.Add(estadoDeTransaccion);
                _db.Estado_cuota.AddRange(estadosDeCuota);

                OperationResult result = Save();
                return result;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();

                var typeModel = ObjectContext.GetObjectType(entry.Entity.GetType());

                if (typeModel.Name == typeof(Detalle_transaccion).Name)
                {
                    return new OperationResult(new DetalleTransaccionException(ex, (Detalle_transaccion)entry.Entity, transaccion));
                }
                else
                {
                    return new OperationResult(new SerieComprobanteException(ex, (Serie_comprobante)entry.Entity));
                }
            }
            catch (Exception e)
            {
                throw new DatosException("Error al actualizar inventario fisico", e);
            }
        }

        /// <summary>
        /// Se entiende que las entidades a crear son excluyentes, por ejemplo un estado de transaccion a crear, no forma parte de alguna transaccion a crear. Ya que si el estado a crear formase parte de alguna transaccion a crear, automaticamente el contexto lo resolverá.
        /// Se asume que las entidades a modificar, solo modificarán sus datos escalares y no ebtidades hijas o anidadas
        /// </summary>
        /// <param name="registro"></param>
        /// <returns></returns>
        public OperationResult RegistrarTransacciones(RegistroTransacciones registro)
        {
            try
            {
                if (registro.Transacciones_Crear != null) _db.Transaccion.AddRange(registro.Transacciones_Crear);
                if (registro.DetallesTransaccion_Crear != null) _db.Detalle_transaccion.AddRange(registro.DetallesTransaccion_Crear);
                if (registro.EstadosTransaccion_Crear != null) _db.Estado_transaccion.AddRange(registro.EstadosTransaccion_Crear);
                if (registro.EstadosCuota_Crear != null) _db.Estado_cuota.AddRange(registro.EstadosCuota_Crear);

                if (registro.Transacciones_Modificar != null && registro.Transacciones_Modificar.Count > 0) registro.Transacciones_Modificar.ForEach(t => _db.Entry(t).State = EntityState.Modified);

                if (registro.DetallesTransaccion_Modificar != null && registro.DetallesTransaccion_Modificar.Count > 0) registro.DetallesTransaccion_Modificar.ForEach(t => _db.Entry(t).State = EntityState.Modified);
                if (registro.Actores_Negocio_Modificar != null && registro.Actores_Negocio_Modificar.Count > 0) registro.Actores_Negocio_Modificar.ForEach(t => _db.Entry(t).State = EntityState.Modified);

                OperationResult result = Save();

                return result;
            }

            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();

                var typeModel = ObjectContext.GetObjectType(entry.Entity.GetType());

                if (typeModel.Name == typeof(Detalle_transaccion).Name)
                {
                    var detalle = (Detalle_transaccion)entry.Entity;
                    throw new DetalleTransaccionException(ex, detalle, detalle.Transaccion);
                }
                else
                {
                    var serie = (Serie_comprobante)entry.Entity;

                    throw new SerieComprobanteException(ex, serie);
                }
            }
            catch (Exception e)
            {
                throw new DatosException("Error al actualizar inventario fisico", e);
            }
            finally
            {

            }


        }

        public OperationResult CrearTransaccionActualizarDetalleTransaccionExistente(Transaccion transaccion, List<Detalle_transaccion> updDetallesTransaccion, int idActorNegocioInternoTransaccionExistenteOrigen, int idTipoTransaccionExistenteOrigen, int idUltimoEstadoTransaccionExsitenteOrigen, int idActorNegocioInternoTransaccionExistenteDestino, int idTipoTransaccionExistenteDestino, int idUltimoEstadoTransaccionExsitenteDestino)
        {
            try
            {
                //Agregar la transaccion
                _db.Transaccion.Add(transaccion);
                //Obtener los detalles de transaccion a actualizar
                //List<Detalle_transaccion> updDetallesTransaccion = transaccion.Detalle_transaccion.ToList();
                var idsConceptoNegocioUpd = updDetallesTransaccion.Select(vdu => vdu.id_concepto_negocio);
                //Obtener transaccion de base de datos que tiene estado confirmado del origen
                Transaccion dbTransaccionOrigen = _db.Transaccion.Include(t => t.Detalle_transaccion).SingleOrDefault(t => t.id_actor_negocio_interno == idActorNegocioInternoTransaccionExistenteOrigen
                                                        && t.id_tipo_transaccion == idTipoTransaccionExistenteOrigen
                                                        && t.id_estado_actual == idUltimoEstadoTransaccionExsitenteOrigen);

                //Obtener el inventario fisico que tiene estado confirmado del origen
                List<Detalle_transaccion> dbDetallesTransaccionOrigen = dbTransaccionOrigen.Detalle_transaccion.Where(dt => idsConceptoNegocioUpd.Contains(dt.id_concepto_negocio)).ToList();

                //Obtener transaccion de base de datos que tiene estado confirmado del destino
                Transaccion dbTransaccionDestino = _db.Transaccion.Include(t => t.Detalle_transaccion).SingleOrDefault(t => t.id_actor_negocio_interno == idActorNegocioInternoTransaccionExistenteDestino
                                                        && t.id_tipo_transaccion == idTipoTransaccionExistenteDestino
                                                        && t.id_estado_actual == idUltimoEstadoTransaccionExsitenteDestino);

                //Obtener el inventario fisico que tiene estado confirmado del destino
                List<Detalle_transaccion> dbDetallesTransaccionDestino = dbTransaccionDestino.Detalle_transaccion.Where(dt => idsConceptoNegocioUpd.Contains(dt.id_concepto_negocio)).ToList();


                foreach (var updDetalleTransaccion in updDetallesTransaccion)
                {

                    bool tieneLote = !String.IsNullOrEmpty(updDetalleTransaccion.lote);
                    //Buscar el item de detalle de transaccion
                    var dbDetalleTransaccionOrigen = dbDetallesTransaccionOrigen.SingleOrDefault(dbdt => dbdt.id_concepto_negocio == updDetalleTransaccion.id_concepto_negocio && ((!tieneLote && String.IsNullOrEmpty(dbdt.lote)) || (tieneLote && dbdt.lote == updDetalleTransaccion.lote)));

                    //Buscar el item de detalle de transaccion
                    var dbDetalleTransaccionDestino = dbDetallesTransaccionDestino.SingleOrDefault(dbdt => dbdt.id_concepto_negocio == updDetalleTransaccion.id_concepto_negocio && ((!tieneLote && String.IsNullOrEmpty(dbdt.lote)) || (tieneLote && dbdt.lote == updDetalleTransaccion.lote)));

                    //Modificar cantidad del inventario fisico del origen
                    dbDetalleTransaccionOrigen.cantidad = dbDetalleTransaccionOrigen.cantidad - updDetalleTransaccion.cantidad;

                    if (dbDetalleTransaccionDestino != null)
                    {
                        //Modificar cantidad del inventario fisico del destino
                        dbDetalleTransaccionDestino.cantidad = dbDetalleTransaccionDestino.cantidad + updDetalleTransaccion.cantidad;
                    }
                    else
                    {
                        //Agregar el detalle de transaccion que se esta ingresando
                        dbTransaccionDestino.Detalle_transaccion.Add(updDetalleTransaccion);
                    }


                    //En caso debe de aumentar cantidad, se debe agregar los valores de caracteristica propia. De lo contrario se actualizar el id_detalle_transaccion
                    if (updDetalleTransaccion.Valor_detalle_maestro_detalle_transaccion != null)
                    {
                        var updValorDetalles = updDetalleTransaccion.Valor_detalle_maestro_detalle_transaccion.ToList();
                        var dbValoresDetallesOrigen = dbDetalleTransaccionOrigen.Valor_detalle_maestro_detalle_transaccion;

                        foreach (var updValorDetalle in updValorDetalles)
                        {
                            dbValoresDetallesOrigen.SingleOrDefault(dbvd => dbvd.id == updValorDetalle.id).Detalle_transaccion = dbDetalleTransaccionDestino;
                        }
                    }

                }
                OperationResult result = Save();
                return result;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al actualizar inventario fisico", e);
            }
        }

        public IEnumerable<Detalle_transaccion> ObtenerDetallesTransaccion(long idTransaccion)
        {
            return _db.Detalle_transaccion.Where(dt => dt.id_transaccion == idTransaccion);
        }

        public IEnumerable<Detalle_transaccion> ObtenerDetallesTransaccion(long idTransaccion, IEnumerable<int> idsConceptoNegocio)
        {
            return _db.Detalle_transaccion.Where(dt => dt.id_transaccion == idTransaccion && idsConceptoNegocio.Contains(dt.id_concepto_negocio));
        }

        public int ObtenerNumeroDetallesDeTransaccionConCantidadMayorA0(long idConceptoBasico, int idTipoTransaccion)
        {
            return _db.Detalle_transaccion.Where(dt => dt.Transaccion.id_tipo_transaccion == idTipoTransaccion && dt.Concepto_negocio.Detalle_maestro4.id == idConceptoBasico && dt.cantidad > 0).Count();
        }

        #endregion




        #region IDS DE TRANSACCIONES PARA EMISION DE NOTAS DE CREDITO

        public IEnumerable<long> ObtenerIdsTransacciones(int idSerie, decimal montoMinimo, decimal montoMaximo, DateTime desde, DateTime hasta, int idCliente, int idTipoTransaccion, int idEstado)
        {
            return _db.Transaccion.Where(t => t.fecha_inicio > desde && t.fecha_inicio < hasta && t.id_tipo_transaccion == idTipoTransaccion && t.id_estado_actual == idEstado && t.id_actor_negocio_externo == idCliente && montoMinimo <= t.importe_total && montoMaximo >= t.importe_total && t.Comprobante.id_serie_comprobante == idSerie).Select(t => t.id);
        }

        public IEnumerable<MontoNotaDeCredito> ObtenerMontoAEmitirNotaCredito(int idSerieB002, int idSerieB006, int idSerieB007, decimal montoMinimo, decimal montoMaximo, DateTime desdeB002, DateTime hastaB002, DateTime desdeB006, DateTime hastaB006, DateTime desdeB007, DateTime hastaB007, int idCliente, int idTipoTransaccion, int idEstado)
        {
            return _db.Transaccion.Where(t => t.id_tipo_transaccion == idTipoTransaccion
            && t.id_estado_actual == idEstado
            && t.id_actor_negocio_externo == idCliente
            && montoMinimo <= t.importe_total
            && montoMaximo >= t.importe_total
            && ((t.Comprobante.id_serie_comprobante == idSerieB002 && t.fecha_inicio > desdeB002 && t.fecha_inicio < hastaB002)
            || (t.Comprobante.id_serie_comprobante == idSerieB006 && t.fecha_inicio > desdeB006 && t.fecha_inicio < hastaB006)
            || (t.Comprobante.id_serie_comprobante == idSerieB007 && t.fecha_inicio > desdeB007 && t.fecha_inicio < hastaB007)))
                .GroupBy(t => t.fecha_inicio.Month).Select(t => new MontoNotaDeCredito()
                {
                    Mes = t.FirstOrDefault().fecha_inicio.Month,
                    Monto = t.Sum(tt => tt.importe_total),
                    Comprobantes = t.Count()
                });
        }


        #endregion
        public IEnumerable<Transaccion> ObtenerTransaccionesPorTipoYContenganIdTransaccionPadre(long[] idsTransaccionesPadre, int idTipoTransaccion)
        {
            try
            {
                return _db.Transaccion.
                    Where(t => idsTransaccionesPadre.Contains((long)t.id_transaccion_padre) && t.id_tipo_transaccion == idTipoTransaccion);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones", e);
            }
        }
        public IEnumerable<Resumen_Venta> ObtenerGuiaRemision(int[] idsTiposTransaccion, int[] idsTiposComprobantes, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {

                return _db.Transaccion
                        .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta
                       && idsTiposComprobantes.Contains(t.Comprobante.id_tipo_comprobante) && idsTiposTransaccion.Contains(t.id_tipo_transaccion))
                        .Select(
                            t => new Resumen_Venta()
                            {
                                Id = t.id,
                                FechaEmision = t.fecha_inicio,
                                IdTipoComprobante = t.Comprobante.Detalle_maestro.id,
                                TipoComprobante = t.Comprobante.Detalle_maestro.nombre,
                                CodigoComprobante = t.Comprobante.Detalle_maestro.valor,
                                SerieComprobante = t.Comprobante.numero_serie,
                                NumeroComprobante = t.Comprobante.numero,
                                IdCliente = t.id_actor_negocio_externo,
                                IdTipoDocumentoCliente = t.Actor_negocio1.Actor.id_documento_identidad,
                                DocumentoCliente = t.Actor_negocio1.Actor.numero_documento_identidad,
                                NombreCliente = t.Actor_negocio1.Actor.primer_nombre,
                                PrimerNombreCajero = t.Actor_negocio.Actor.primer_nombre,
                                SegundoNombreCajero = t.Actor_negocio.Actor.segundo_nombre,
                                TercerNombreCajero = t.Actor_negocio.Actor.tercer_nombre,
                                ImporteTotal = t.importe_total,
                                ValorParametroTipoDeVenta = t.Parametro_transaccion.FirstOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroTipoVenta) != null ? t.Parametro_transaccion.FirstOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroTipoVenta).valor : null,
                                ValorParametroModoDePago = t.Parametro_transaccion.FirstOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroModoDePago) != null ? t.Parametro_transaccion.FirstOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroModoDePago).valor : null,
                                ValorParametroAliasDeCliente = t.Parametro_transaccion.FirstOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroAliasCliente) != null ? t.Parametro_transaccion.FirstOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroAliasCliente).valor : null,
                                IdEstado = t.id_estado_actual,
                                Estado = t.Detalle_maestro.nombre,
                                Transmitido = t.Evento_transaccion.Select(ev => ev.id_evento).Contains(MaestroSettings.Default.IdDetalleMaestroEstadoTransmitido)

                            }
                    ).OrderByDescending(t => t.FechaEmision);

            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones", e);
            }
        }

        public IEnumerable<long> ObtenerIdsGuiaRemisionPorEnviar(DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                List<int> idsTiposTransacciones = Diccionario.TiposDeTransaccionMovimientoDeBienes_Salidas.ToList();
                idsTiposTransacciones.AddRange(Diccionario.TiposDeTransaccionMovimientoDeBienes_Entradas.ToList());
                idsTiposTransacciones.AddRange(Diccionario.TiposDeTransaccionGuiasDeRemision.ToList());
                return _db.Transaccion.Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && idsTiposTransacciones.Contains(t.id_tipo_transaccion) && t.Comprobante.id_tipo_comprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteGuiaDeRemisionRemitente && (!t.Evento_transaccion.Any(et => et.id_evento == MaestroSettings.Default.IdDetalleMaestroEstadoTransmitido))).Select(t => t.id);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener transacciones", e);
            }
        }

        #region METODOS REPORTE DE COMPRAS

        public IEnumerable<Resumen_Compra> ObtenerResumenCompraPorTipoCompra(int[] idsTiposTransaccion, int[] idsTiposComprobante, int idEstado, int idParametroTransaccion, TipoOperacionCompra valorParametroTransaccion, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                return _db.Transaccion
                           .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta
                            && idsTiposTransaccion.Contains(t.id_tipo_transaccion)
                            && idsTiposComprobante.Contains(t.Comprobante.id_tipo_comprobante)
                            && t.id_estado_actual == idEstado
                            && (t.Parametro_transaccion.FirstOrDefault(tt => tt.id_parametro == idParametroTransaccion).valor == ((int)valorParametroTransaccion).ToString()))
                            .Select(t => new Resumen_Compra()
                            {
                                Id = t.id,
                                FechaInicio = t.fecha_inicio,
                                FechaRegistro= t.fecha_registro_sistema,
                                IdTipoComprobante = t.Comprobante.Detalle_maestro.id,
                                TipoComprobante = t.Comprobante.Detalle_maestro.nombre,
                                CodigoComprobante = t.Comprobante.Detalle_maestro.valor,
                                SerieComprobante = t.Comprobante.numero_serie,
                                NumeroComprobante = t.Comprobante.numero,
                                IdProveedor = t.id_actor_negocio_externo,
                                DocumentoProveedor = t.Actor_negocio1.Actor.numero_documento_identidad,
                                NombreProveedor = t.Actor_negocio1.Actor.primer_nombre,
                                Importe = t.importe_total,
                                ValorParametroTipoOperacionDeCompra = t.Parametro_transaccion.FirstOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroTipoCompra) != null ? t.Parametro_transaccion.FirstOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroTipoCompra).valor : null,
                                ValorParametroModoDePago = t.Parametro_transaccion.FirstOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroModoDePago) != null ? t.Parametro_transaccion.FirstOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroModoDePago).valor : null,
                                Estado = t.Detalle_maestro.nombre,
                            }
                            );
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener ventas por cliente", e);
            }
        }

        #endregion
        public Transaccion ObtenerTransaccionDeUltimoComprobante(int idSerieComprobante)
        {
            try
            {
                Comprobante comprobante = _db.Comprobante.Where(c => c.id_serie_comprobante == idSerieComprobante).OrderByDescending(c => c.id).FirstOrDefault();
                Transaccion transaccion = null;
                if (comprobante != null)
                {
                    transaccion = _db.Transaccion.Where(t => t.id_comprobante == comprobante.id).FirstOrDefault();
                }
                return transaccion;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener una transaccion por id serie", e);
            }
        }


        public string ObtenerUltimoRegistroDeDetalleTransaccionDeTransaccionOrdenVentaDeUnCliente(int idCliente)
        {
            string registroDetalleTransaccion = "";
            if(_db.Transaccion.Any(t => t.id_actor_negocio_externo == idCliente && t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta))
            {
                registroDetalleTransaccion = _db.Transaccion.Where(t => t.id_actor_negocio_externo == idCliente && t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta).OrderByDescending(t => t.id).FirstOrDefault().Detalle_transaccion.FirstOrDefault().registro;
            }
            
            return registroDetalleTransaccion;
        }
        public OperacionTipoTransaccionTipoComprobante ObtenerTipoTransaccionTipoComprobanteOperacion(long idOperacion)
        {
            try
            {
                return _db.Transaccion.Where(t => t.id == idOperacion).Select(t => 
                new OperacionTipoTransaccionTipoComprobante
                {
                    IdOperacion = t.id,
                    IdTipoTransaccion = t.id_tipo_transaccion,
                    IdTipoComprobante = t.Comprobante.id_tipo_comprobante,
                    SerieComprobante = t.Comprobante.numero_serie,
                    NumeroComprobante = t.Comprobante.numero,
                    TipoComprobante = t.Comprobante.Detalle_maestro.nombre,
                    Tercero = t.Actor_negocio1.Actor.primer_nombre.Replace("|", " "),
                    FechaInicio = t.fecha_inicio
                }).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener tipo de transaccion, tipo de comprobante", e);
            }
        }
    }

}

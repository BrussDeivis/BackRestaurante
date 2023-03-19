using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using Tsp.FacturacionElectronica.AccesoDatos;
using Tsp.FacturacionElectronica.Modelo;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.EFactura;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;

namespace Tsp.Sigescom.AccesoDatos
{
    public class FacturacionDatos : EFacturaRepositorioBase, IFacturacionRepositorio
    {
        public IFacturacionRepositorio ObtenerNuevoRepositorio()
        {
            return new FacturacionDatos();
        }
        #region Documento

        public OperationResult CrearDocumento(Documento documento)
        {
            try
            {
                _db.Documento.Add(documento);
                var result = Save();
                result.data = documento.id;
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public OperationResult ActualizarDocumento(Documento documento)
        {
            try
            {
                Documento dbDocumento = _db.Documento.Single(c => c.id == documento.id);
                _db.Entry(dbDocumento).CurrentValues.SetValues(documento);
                var result = Save();
                result.data = documento.id;
                return result;
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }

        public OperationResult ActualizarEstadoDocumento(long idSigescom, EstadoDocumentoElectronico estado, EstadoSigescomDocumentoElectronico estadoSigescom)
        {
            try
            {
                Documento dbDocumento = _db.Documento.Single(c => c.idSigescom == idSigescom);
                dbDocumento.estado = (int)estado;
                dbDocumento.estadoSigescom = (int)estadoSigescom;
                var result = Save();
                result.data = dbDocumento.id;
                return result;
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }

        public OperationResult CrearDocumentosMasivos(List<Documento> ListaDocumentos)
        {
            try
            {
                List<string> comandosSQL = new List<string>();
                comandosSQL.Add("declare @idBinario int");
                List<SqlParameter> parametrosSql = new List<SqlParameter>();
                int index = 1;
                foreach (var documento in ListaDocumentos)
                {
                    var comandoActual = (Environment.NewLine + "insert into Binario values (@ArchivoBionario" + index + "); set @idBinario = SCOPE_IDENTITY(); insert into Documento values (" + documento.idSigescom + ",'" + documento.codigoTipoComprobante + "','" + documento.serieComprobante + "','" + documento.numeroComprobante + "','" + documento.fechaEmision.ToString("MM-dd-yyyy HH:mm:ss.fff") + "',@idBinario,'" + documento.tipoComprobante + "'," + +documento.estado + "," + +documento.estadoSigescom + ",0);");
                    var parametroActual = new SqlParameter("@ArchivoBionario" + index, System.Data.SqlDbType.VarBinary);
                    parametroActual.Value = documento.Binario.archivoBinario;
                    comandosSQL.Add(comandoActual);
                    parametrosSql.Add(parametroActual);
                    index++;
                }
                string comandoAEjecutar = "";
                foreach (var item in comandosSQL)
                {
                    comandoAEjecutar += item;
                }
                var resultado = _db.Database.ExecuteSqlCommand(comandoAEjecutar, parametrosSql.ToArray());
                var resultadoDetails = new OperationResult(OperationResultEnum.Success);
                return resultadoDetails;
            }
            catch (Exception e)
            {
                throw new DatosException("Error en datos al crear documentos masivos", e);
            }
        }

        #endregion

        #region Envio

        public OperationResult CrearEnvio(Envio envio)
        {
            try
            {
                _db.Envio.Add(envio);
                var result = Save();
                result.data = envio.id;
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public OperationResult ActualizarEnvio(Envio envio)
        {
            try
            {
                string comando = "update Envio set estado = '" + envio.estado + "', codigoRespuesta = '" + envio.codigoRespuesta + "', observacion = '" + envio.observacion.Replace("'", "''") + "', idBinarioRespuesta = " + (envio.idBinarioRespuesta == null ? "null" : ("'" + envio.idBinarioRespuesta + "'")) + " where id = " + envio.id + ";";
                _db.Database.ExecuteSqlCommand(comando);
                return new OperationResult(OperationResultEnum.Success);
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }
        public OperationResult ActualizarEnvios(List<Envio> enviosAActualizar)
        {
            try
            {
                List<string> lineasComandos = new List<string>();
                foreach (var item in enviosAActualizar)
                {
                    lineasComandos.Add(Environment.NewLine + "update Envio set estado = '" + item.estado + "', codigoRespuesta = '" + item.codigoRespuesta + "', observacion = '" + item.observacion.Replace("'", "''") + "', idBinarioRespuesta = '" + item.idBinarioRespuesta + "' where id = " + item.id + ";");
                }
                string comando = "";
                foreach (var item in lineasComandos)
                {
                    comando += item;
                }
                _db.Database.ExecuteSqlCommand(comando);
                return new OperationResult(OperationResultEnum.Success);
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }

        public OperationResult ActualizarObservacionYEstadoEnvios(List<Envio> enviosAActualizar)
        {
            try
            {
                List<string> lineasComandos = new List<string>();
                foreach (var item in enviosAActualizar)
                {
                    lineasComandos.Add(Environment.NewLine + "update Envio set observacion = '" + item.observacion.Replace("'", "''") + "', estado = " + item.estado + " where id = " + item.id + ";");
                }
                string comando = "";
                foreach (var item in lineasComandos)
                {
                    comando += item;
                }
                _db.Database.ExecuteSqlCommand(comando);
                return new OperationResult(OperationResultEnum.Success);
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }

        public OperationResult ActualizarDocumentosAEnviados(long[] idsDocumentos)
        {
            try
            {
                List<string> lineasComandos = new List<string>();
                foreach (var item in idsDocumentos)
                {
                    lineasComandos.Add(Environment.NewLine + "update Documento set enviado = 1 where id = " + item + ";");
                }
                string comando = "";
                foreach (var item in lineasComandos)
                {
                    comando += item;
                }
                _db.Database.ExecuteSqlCommand(comando);
                return new OperationResult(OperationResultEnum.Success);
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }

        public OperationResult ActualizarEstadoEnvio(Envio envioAActualizar)
        {
            try
            {
                Envio dbEnvio = _db.Envio.Single(r => r.id == envioAActualizar.id);
                dbEnvio.estado = envioAActualizar.estado;
                _db.Entry(dbEnvio).Property(x => x.estado).IsModified = true;
                var result = Save();
                result.data = envioAActualizar.id;
                return result;
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }

        public OperationResult ActualizarEstadoObservacionTicketEnvio(Envio envio)
        {
            try
            {
                string comando = "update Envio set estado = '" + envio.estado + "', observacion = '" + envio.observacion.Replace("'", "''") + "', numeroTicket = '" + envio.numeroTicket + "' where id = " + envio.id + ";";
                _db.Database.ExecuteSqlCommand(comando);
                return new OperationResult(OperationResultEnum.Success);
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }

        public OperationResult ActualizarDocumentoAEnviado(long idDocumento)
        {
            try
            {
                Documento dbDocumento = _db.Documento.Single(r => r.id == idDocumento);
                dbDocumento.enviado = true;
                _db.Entry(dbDocumento).Property(x => x.enviado).IsModified = true;
                var result = Save();
                result.data = dbDocumento.id;
                return result;
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }

        public OperationResult ActualizarEstadoYObservacionEnvio(Envio envioAActualizar)
        {
            try
            {
                Envio dbEnvio = _db.Envio.Single(r => r.id == envioAActualizar.id);
                dbEnvio.estado = envioAActualizar.estado;
                dbEnvio.observacion = envioAActualizar.observacion;
                _db.Entry(dbEnvio).Property(x => x.estado).IsModified = true;
                _db.Entry(dbEnvio).Property(x => x.observacion).IsModified = true;
                var result = Save();
                result.data = envioAActualizar.id;
                return result;
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }

        #endregion

        #region EnvioDocumento

        public OperationResult CrearEnvioDocumento(EnvioDocumento envioDocumento)
        {
            try
            {
                _db.EnvioDocumento.Add(envioDocumento);
                var result = Save();
                result.data = envioDocumento.id;
                return result;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar crear envio de documento(s)", e);
            }
        }

        public OperationResult CrearEnvioDocumentoMasivo(long idEnvio, long[] idsDocumentos)
        {
            try
            {

                List<string> comandosSQL = new List<string>();
                comandosSQL.Add("declare @idDocumento int");
                List<SqlParameter> parametrosSql = new List<SqlParameter>();
                int index = 1;
                foreach (var idDocumento in idsDocumentos)
                {
                    var comandoActual = (Environment.NewLine + "insert into EnvioDocumento values ( @idDocumento" + index + ", " + idEnvio + ");");
                    var parametroActual = new SqlParameter("@idDocumento" + index, System.Data.SqlDbType.Int);
                    parametroActual.Value = idDocumento;
                    comandosSQL.Add(comandoActual);
                    parametrosSql.Add(parametroActual);
                    index++;
                }
                string comandoAEjecutar = "";
                foreach (var item in comandosSQL)
                {
                    comandoAEjecutar += item;
                }
                var resultado = _db.Database.ExecuteSqlCommand(comandoAEjecutar, parametrosSql.ToArray());
                var resultadoDetails = new OperationResult(OperationResultEnum.Success);
                return resultadoDetails;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al crear envio_documento", e);
            }
        }

        #endregion

        #region Binario

        public OperationResult CrearBinario(Binario archivoBinario)
        {
            try
            {
                _db.Binario.Add(archivoBinario);
                var result = Save();
                result.data = archivoBinario.id;
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public OperationResult ActualizarBinario(Binario binario)
        {
            try
            {
                Binario dbBinario = _db.Binario.Single(c => c.id == binario.id);
                _db.Entry(dbBinario).CurrentValues.SetValues(binario);
                var result = Save();
                result.data = binario.id;
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public byte[] ObtenerBinario(long idBinario)
        {
            try
            {
                return _db.Binario.SingleOrDefault(b => b.id == idBinario).archivoBinario;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region GetDocumento

        public IEnumerable<Documento> ObtenerDocumentos()
        {
            try
            {
                return _db.Documento.Include(d => d.EnvioDocumento);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Documento> ObtenerDocumentosEntreFechas(DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                return _db.Documento.Include(d => d.EnvioDocumento).Where(d => d.fechaEmision >= fechaDesde && d.fechaEmision <= fechaHasta);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Documento> ObtenerDocumentos(List<long> idDocumentos)
        {
            try
            {

                return _db.Documento.Include(d => d.EnvioDocumento)
                    .Include(d => d.EnvioDocumento.Select(ed => ed.Envio))
                    .Where(d => idDocumentos.Contains(d.id));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Documento ObtenerDocumentoElectronicoIncluidoBinario(long idDocumento)
        {
            try
            {
                return _db.Documento.Include(d => d.Binario).SingleOrDefault(dm => dm.id == idDocumento);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Documento ObtenerDocumentoElectronico(long idDocumento)
        {
            try
            {
                return _db.Documento.SingleOrDefault(dm => dm.id == idDocumento);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int ObtenerIdentificador(DateTime fechaConsulta, string tipoEnvio)
        {
            try
            {
                return _db.Envio.Where(dm => dm.fechaEnvio.Day == fechaConsulta.Day && dm.fechaEnvio.Month == fechaConsulta.Month && dm.fechaEnvio.Year == fechaConsulta.Year && dm.tipoEnvio == tipoEnvio).Count();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene los ids documentos por dia del primero que no se envio
        /// </summary>
        /// <param name="codigoTipoComprobante"></param>
        /// <returns></returns>
        public IEnumerable<long> ObtenerIdDocumentosAEnviarPorDia(string codigoTipoComprobante)
        {
            try
            {
                DateTime fechaReferencia = _db.Documento.Where(d => d.codigoTipoComprobante == codigoTipoComprobante && !d.EnvioDocumento.Any()).First().fechaEmision;
                DateTime fechaReferenciaMinimo = fechaReferencia.Date;
                DateTime fechaReferenciaMaximo = fechaReferencia.Date.AddDays(1).AddMilliseconds(-1);
                return _db.Documento.Where(d => d.codigoTipoComprobante == codigoTipoComprobante && !d.EnvioDocumento.Any() && fechaReferenciaMinimo <= d.fechaEmision && d.fechaEmision <= fechaReferenciaMaximo).Select(d => d.id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene los documentos por dia del primero que no se envio
        /// </summary>
        /// <param name="codigoTipoComprobante"></param>
        /// <returns></returns>
        public IEnumerable<Documento> ObtenerDocumentosIncluidoBinarioAEnviarPorDia(string codigoTipoComprobante)
        {
            try
            {
                IEnumerable<Documento> resultado = new List<Documento>();
                var primerDocumentoNoEnviado = _db.Documento.FirstOrDefault(d => !d.enviado && d.codigoTipoComprobante == codigoTipoComprobante);
                if (primerDocumentoNoEnviado == null)
                {
                    return resultado;
                }
                //DateTime fechaReferencia = _db.Documento.First(d => d.EnvioDocumento.Count() <= 0 && d.codigoTipoComprobante == codigoTipoComprobante).fechaEmision;
                DateTime fechaReferencia = primerDocumentoNoEnviado.fechaEmision;
                DateTime fechaReferenciaMinimo = fechaReferencia.Date;
                DateTime fechaReferenciaMaximo = fechaReferencia.Date.AddDays(1).AddMilliseconds(-1);
                resultado = _db.Documento.Include(d => d.Binario).Where(d => fechaReferenciaMinimo <= d.fechaEmision && d.fechaEmision <= fechaReferenciaMaximo && d.codigoTipoComprobante == codigoTipoComprobante && !d.enviado);
                return resultado;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener documentos a enviar por dia", e);
            }
        }

        /// <summary>
        /// Obtiene los ids documentos a enviar por dia del primero que no se envio
        /// </summary>
        /// <param name="codigoTipoComprobante"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public IEnumerable<Documento> ObtenerDocumentosAEnviarPorDia(string codigoTipoComprobante, int estado)
        {
            try
            {
                DateTime fechaReferencia = _db.Documento.Where(d => d.codigoTipoComprobante == codigoTipoComprobante && !d.EnvioDocumento.Any() && d.estado == estado).First().fechaEmision;
                DateTime fechaReferenciaMinimo = fechaReferencia.Date;
                DateTime fechaReferenciaMaximo = fechaReferencia.Date.AddDays(1).AddMilliseconds(-1);
                return _db.Documento.Where(d => d.codigoTipoComprobante == codigoTipoComprobante && !d.EnvioDocumento.Any() && fechaReferenciaMinimo <= d.fechaEmision && d.fechaEmision <= fechaReferenciaMaximo && d.estado == estado);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene los ids documentos por el mismo codigo de comprobante
        /// </summary>
        /// <param name="codigoTipoComprobante"></param>
        /// <returns></returns>
        public IEnumerable<long> ObtenerIdDocumentosAEnviar(string codigoTipoComprobante)
        {
            try
            {
                return _db.Documento.Where(d => d.codigoTipoComprobante == codigoTipoComprobante && !d.EnvioDocumento.Any()).Select(d => d.id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene los documentos por el mismo codigo de comprobante
        /// </summary>
        /// <param name="codigoTipoComprobante"></param>
        /// <returns></returns>
        public IEnumerable<Documento> ObtenerDocumentosAEnviar(string codigoTipoComprobante)
        {
            try
            {
                return _db.Documento.Where(d => d.codigoTipoComprobante == codigoTipoComprobante && !d.EnvioDocumento.Any());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene los documentos por el mismo codigo de comprobante incluido su archivo binario
        /// </summary>
        /// <param name="codigoTipoComprobante"></param>
        /// <returns></returns>
        public IEnumerable<Documento> ObtenerDocumentosIncluidoBinarioAEnviar(string codigoTipoComprobante, int estadoDocumento)
        {
            try
            {
                return _db.Documento.Include(d => d.Binario).Where(d => d.codigoTipoComprobante == codigoTipoComprobante && d.estado == estadoDocumento && !d.EnvioDocumento.Any());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene los documentos del mismo codigo de comprobante incluido su archivo binario
        /// </summary>
        /// <param name="codigoTipoComprobante"></param>
        /// <returns></returns>
        public IEnumerable<Documento> ObtenerDocumentosIncluidoBinarioAEnviar(string codigoTipoComprobante)
        {
            try
            {
                return _db.Documento.Include(d => d.Binario).Where(d => d.codigoTipoComprobante == codigoTipoComprobante && !d.EnvioDocumento.Any());
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region GetEnvio

        public IEnumerable<Envio> ObtenerEnvios()
        {
            try
            {
                return _db.Envio.Include(e => e.EnvioDocumento);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Envio ObtenerEnvio(long idEnvio)
        {
            try
            {
                return _db.Envio.Include(e => e.EnvioDocumento).
                    Include(e => e.EnvioDocumento.Select(ed => ed.Documento)).
                    Include(e => e.EnvioDocumento.Select(ed => ed.Documento.Binario)).
                    Single(e => e.id == idEnvio);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Envio> ObtenerEnviosInclusiveEnvioDocumentoYDocumentoEntreFechas(DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                return _db.Envio.Include(e => e.EnvioDocumento).Include(e => e.EnvioDocumento.Select(ed => ed.Documento)).Where(e => e.fechaEnvio >= fechaDesde && e.fechaEnvio <= fechaHasta);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<EnvioSimplificado> ObtenerEnviosSinCodigoDeRespuesta()
        {
            try
            {
                return _db.Envio.Where(e => e.estado == (int)EstadoEnvio.Pendiente || e.estado == (int)EstadoEnvio.ConExcepcion).Select(e => new EnvioSimplificado() { Id = e.id, NumeroTicket = e.numeroTicket, TipoEnvio = e.tipoEnvio, CodigoTipoDocumento = e.EnvioDocumento.FirstOrDefault().Documento.codigoTipoComprobante, SerieDocumento = e.EnvioDocumento.FirstOrDefault().Documento.serieComprobante, NumeroDocumento = e.EnvioDocumento.FirstOrDefault().Documento.numeroComprobante });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool HayEnvios(string tipoEnvio, int estado)
        {
            try
            {
                return _db.Envio.Any(e => e.tipoEnvio == tipoEnvio && e.estado == estado);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<long> IdEnvios(string tipoEnvio, int estado)
        {
            try
            {
                return _db.Envio.Where(e => e.tipoEnvio == tipoEnvio && e.estado == estado).Select(e => e.id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Envio> ObtenerEnviosIncluidoDocumentoIncluidoBinario(int estado)
        {
            try
            {
                return _db.Envio.Include(e => e.EnvioDocumento).
                    Include(e => e.EnvioDocumento.Select(ed => ed.Documento)).
                    Include(e => e.EnvioDocumento.Select(ed => ed.Documento.Binario)).
                    Where(e => e.estado == estado);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Envio> ObtenerEnviosIncluidoDocumentoIncluidoBinario(string tipoEnvio, int estado)
        {
            try
            {
                return _db.Envio.Include(e => e.EnvioDocumento).
                    Include(e => e.EnvioDocumento.Select(ed => ed.Documento)).
                    Include(e => e.EnvioDocumento.Select(ed => ed.Documento.Binario)).
                    Where(e => e.tipoEnvio == tipoEnvio && e.estado == estado);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region GetEnvioDocumento

        public IEnumerable<long> ObtenerIdDocumentosEnviados(long idEnvio)
        {
            try
            {
                return _db.EnvioDocumento.Where(ed => ed.idEnvio == idEnvio).Select(d => d.idDocumento);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Documento> ObtenerDocumentosEnviados(long idEnvio)
        {
            try
            {
                return _db.EnvioDocumento.Where(ed => ed.idEnvio == idEnvio).Select(d => d.Documento);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool HayDocumentosNoEnviados(string codigoTipoComprobante)
        {
            try
            {
                return _db.Documento.Count(d => d.codigoTipoComprobante == codigoTipoComprobante && d.EnvioDocumento.Count() <= 0) > 0;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene los documentos por el mismo codigo de comprobante y con el estado dado
        /// </summary>
        public bool HayDocumentosNoEnviados(string codigoTipoComprobante, int estado)
        {
            try
            {
                return _db.Documento.Any(d => d.codigoTipoComprobante == codigoTipoComprobante && !d.EnvioDocumento.Any() && d.estado == estado);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool DocumentoReferenciaFueAceptado(string tipoDocumentoReferencia, string numeroDocumentoReferencia, List<int> estadosDeAceptacion)
        {
            try
            {
                return _db.EnvioDocumento.Where(ed => ed.Documento.codigoTipoComprobante == tipoDocumentoReferencia && ed.Documento.serieComprobante + "-" + ed.Documento.numeroComprobante == numeroDocumentoReferencia).Any(ed => estadosDeAceptacion.Contains(ed.Envio.estado));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Documento> ObtenerDocumentosFaltantesAEmitirAnulacion(EstadoSigescomDocumentoElectronico estadoSigescom, int numeroEnviosAceptados, string codigoTipoComprobante, int[] estadosEnvioQueNoSeQuiere)
        {
            try
            {
                return _db.Documento.Where(d => d.estadoSigescom == (int)estadoSigescom && d.codigoTipoComprobante == codigoTipoComprobante // Comprueba el estado de sigescom sea igual al estado pasado en parametros y que el codigo de tipo de comprobante sea igaul al que se paso por parametro
                && d.EnvioDocumento.Where(ed => ed.Envio.modoEnvio == (int)ModoEnvio.Adicion).Count(ed => ed.Envio.estado == (int)EstadoEnvio.Aceptado || ed.Envio.estado == (int)EstadoEnvio.AceptadoConObservaciones) == numeroEnviosAceptados // Comprueba que los documentos tengan el numero de envio aceptados y aceptados por observacion iagual ql numeroEnviosAceptados parados por parametros
                && d.EnvioDocumento.Where(ed => ed.Envio.modoEnvio == (int)ModoEnvio.Anulacion).Count(ed => ed.Envio.estado == (int)EstadoEnvio.Aceptado || ed.Envio.estado == (int)EstadoEnvio.AceptadoConObservaciones) == 0 // Comprueba que los documentos tengan el numero de envio aceptados y aceptados por observacion iagual ql numeroEnviosAceptados parados por parametros
                && d.EnvioDocumento.Count(ed => estadosEnvioQueNoSeQuiere.Contains(ed.Envio.estado)) == 0 // Comprueba que los documentos no tengan envios de estadoEnvioQueNoSeQuiere
                );
            }
            catch (Exception e)
            {
                throw new DatosException("Erroe al obtener docuemntos", e);
            }
        }

        /// <summary>
        /// Devuelve aquellos documentos en estado indicado cuya cantidad de envios en estado aceptado para el tipo de envio dado sea igual a la cantidad indicada 
        /// </summary>
        public IEnumerable<Documento> ObtenerDocumentos(string codigoTipoComprobante, EstadoSigescomDocumentoElectronico estadoSigescom, string tipoEnvio1, int numeroEnviosAceptados1, string tipoEnvio2, int numeroEnviosAceptados2)
        {
            return _db.Documento.Where(d => d.estadoSigescom == (int)estadoSigescom //Se buscan las comprobantes cuya cantidad de envios aceptados o aceptados con observaciones por sunat es igual al 
            && d.codigoTipoComprobante == codigoTipoComprobante
            && d.EnvioDocumento.Count(ed => ed.Envio.tipoEnvio == tipoEnvio1 && (ed.Envio.estado == (int)EstadoEnvio.Aceptado || ed.Envio.estado == (int)EstadoEnvio.AceptadoConObservaciones)
            ) == numeroEnviosAceptados1
            && d.EnvioDocumento.Count(ed => ed.Envio.tipoEnvio == tipoEnvio2
            && (ed.Envio.estado == (int)EstadoEnvio.Aceptado || ed.Envio.estado == (int)EstadoEnvio.AceptadoConObservaciones)) == numeroEnviosAceptados2);
        }

        #endregion

    }
}

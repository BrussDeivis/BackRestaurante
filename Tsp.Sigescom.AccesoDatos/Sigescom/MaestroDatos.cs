using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Tsp.Sigescom.AccesoDatos.Generico;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;

namespace Tsp.Sigescom.AccesoDatos
{
    public class MaestroDatos : RepositorioBase, IMaestroRepositorio
    {
        public OperationResult crearMaestro(Maestro maestro)
        {
            try
            {
                _db.Maestro.Add(maestro);
                var result = Save();
                result.data = maestro.id;
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public OperationResult crearDetalleMaestro(Detalle_maestro detalle)
        {
            try
            {
                _db.Detalle_maestro.Add(detalle);
                var result = Save();
                result.data = detalle.id;
                return result;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al crear detalle de maestro", e);
            }
        }
        public OperationResult CrearDetalleDetalleMaestro(Detalle_detalle_maestro detalle)
        {
            try
            {
                _db.Detalle_detalle_maestro.Add(detalle);
                var result = Save();
                result.data = detalle.Detalle_maestro1.id;
                return result;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al crear detalle de detalle de maestro", e);
            }
        }

        public OperationResult actualizarMaestro(Maestro maestro)
        {
            try
            {
                Maestro dbMaestro = _db.Maestro.Single(m => m.id == maestro.id);
                _db.Entry(dbMaestro).CurrentValues.SetValues(maestro);
                var result = Save();
                result.data = maestro.id;
                return result;
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }

        public OperationResult actualizarDetalleMaestro(Detalle_maestro detalle)
        {
            try
            {
                Detalle_maestro dbDetalle = _db.Detalle_maestro.Single(m => m.id == detalle.id);
                detalle.fecha_registro = dbDetalle.fecha_registro;
                _db.Entry(dbDetalle).CurrentValues.SetValues(detalle);
                var result = Save();
                result.data = detalle.id;
                return result;
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }

        public OperationResult ActualizarDetalleDetalleMaestro(Detalle_detalle_maestro detalle)
        {
            try
            {
                Detalle_maestro dbDetalle = _db.Detalle_maestro.Single(m => m.id == detalle.Detalle_maestro1.id);
                detalle.Detalle_maestro1.fecha_registro = dbDetalle.fecha_registro;
                _db.Entry(dbDetalle).CurrentValues.SetValues(detalle.Detalle_maestro1);
                Detalle_detalle_maestro dbDetalleDetalle = dbDetalle.Detalle_detalle_maestro1.Single(d => d.id_detalle_maestro_secundario == detalle.Detalle_maestro1.id);
                dbDetalleDetalle.id_detalle_maestro_principal = detalle.id_detalle_maestro_principal;
                var result = Save();
                result.data = dbDetalle.id;
                return result;
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }

        public OperationResult DarDeBajaDetalleMaestro(int idDetalleMaestro, int idMaestro)
        {
            try
            {

                var dbDetalleMaestro = _db.Maestro.Where(m => m.id == idMaestro).SelectMany(m => m.Detalle_maestro).FirstOrDefault(dm => dm.id == idDetalleMaestro);
                dbDetalleMaestro.es_vigente = false;

                return Save();
            }
            catch (Exception e)
            {
                throw new DatosException("Erro al dar de baja detalle maestro", e);
            }

        }
        public OperationResult DarDeAltaDetalleMaestro(int idDetalleMaestro, int idMaestro)
        {
            try
            {

                var dbDetalleMaestro = _db.Maestro.Where(m => m.id == idMaestro).SelectMany(m => m.Detalle_maestro).FirstOrDefault(dm => dm.id == idDetalleMaestro);
                dbDetalleMaestro.es_vigente = true;

                return Save();
            }
            catch (Exception e)
            {
                throw new DatosException("Erro al dar de baja detalle maestro", e);
            }

        }


        public async Task<IEnumerable<ItemJerarquico>> ObtenerDetallesJerarquicos(int idMaestro)
        {
            try
            {
                var _dbAsync = new SigescomEntities();
                return (await _dbAsync.Detalle_detalle_maestro.Include(dm => dm.Detalle_maestro1).ToListAsync()).Select(ddm => new ItemJerarquico { Id = ddm.id_detalle_maestro_secundario, IdPadre = ddm.id_detalle_maestro_principal, Nombre = ddm.Detalle_maestro1.nombre });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<ItemJerarquico> ObtenerDetallesJerarquicosPorRolConceptoNegocio(int idRol)
        {
            try
            {
                var _db = new SigescomEntities();
                var idsCategorias = _db.Concepto_negocio_rol.Where(cnr => cnr.id_rol == idRol).Select(cnr => cnr.Concepto_negocio).Where(cnr => cnr.es_vigente).SelectMany(cn => cn.Detalle_maestro4.Categoria_concepto).Select(cc => cc.id_categoria).ToList();
                return _db.Detalle_detalle_maestro.Include(dm => dm.Detalle_maestro1).Where(dm => idsCategorias.Contains(dm.id_detalle_maestro_secundario)).Select(ddm => new ItemJerarquico { Id = ddm.id_detalle_maestro_secundario, IdPadre = ddm.id_detalle_maestro_principal, Nombre = ddm.Detalle_maestro1.nombre }); ;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<Detalle_maestro>> ObtenerDetallesAsync(int idMaestro)
        {
            try
            {
                var _dbAsync = new SigescomEntities();

                return await _dbAsync.Detalle_maestro.Where(dm => dm.id_maestro == idMaestro).ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<IEnumerable<Detalle_maestro>> ObtenerDetallesVigentesAsync(int idMaestro)
        {
            try
            {
                var _dbAsync = new SigescomEntities();

                return await _dbAsync.Detalle_maestro.Where(dm => dm.id_maestro == idMaestro && dm.es_vigente).ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public IEnumerable<ItemGenerico> ObtenerDetallesComoItemsGenericos(int idMaestro)
        {
            try
            {
                return _db.Detalle_maestro.Where(dm => dm.id_maestro == idMaestro)
                    .Select(dm => 
                    new ItemGenerico
                    { 
                        Id = dm.id, 
                        Nombre = dm.nombre 
                    });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public IEnumerable<Detalle_maestro> ObtenerDetalles(int idMaestro)
        {
            try
            {
                var _dbS = new SigescomEntities();

                return _dbS.Detalle_maestro.Where(dm => dm.id_maestro == idMaestro);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public IEnumerable<Detalle_maestro> ObtenerDetalles(int idMaestro, bool esVigente)
        {
            try
            {
                return _db.Detalle_maestro.Where(dm => dm.id_maestro == idMaestro && dm.es_vigente == esVigente);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public IEnumerable<Familia_Concepto_Comercial> ObtenerFamiliasConceptoComercial(int idMaestro, bool esVigente)
        {
            try
            {
                return _db.Detalle_maestro.Where(dm => dm.id_maestro == idMaestro
                && dm.es_vigente == esVigente).OrderBy(dm => dm.nombre)
                    .Select(dm => new Familia_Concepto_Comercial()
                    {
                        Id = dm.id,
                        Nombre = dm.nombre,
                        Esbien = dm.valor == "1"
                    }); ;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener detalles de maestro", e);
            }
        }
        public IEnumerable<Familia_Concepto_Comercial> ObtenerFamiliasConceptoComercial(int idMaestro, string valor, bool esVigente)
        {
            try
            {
                return _db.Detalle_maestro.Where(dm => dm.id_maestro == idMaestro
                && dm.valor == valor
                && dm.es_vigente == esVigente)
                    .Select(dm => new Familia_Concepto_Comercial()
                    {
                        Id = dm.id,
                        Nombre = dm.nombre,
                        Esbien = dm.valor == "1"
                    }); ;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener detalles de maestro", e);
            }
        }
        public IEnumerable<Familia_Concepto_Comercial> ObtenerFamiliasConceptoComercialPorRol(int idRol, bool esVigente)
        {
            try
            {
                return _db.Concepto_negocio_rol.Where(cnr => cnr.id_rol == idRol).Select(cnr => cnr.Concepto_negocio).Where(cnr => cnr.es_vigente == esVigente).Select(cn => cn.Detalle_maestro4).Distinct()
                    .Select(dm => new Familia_Concepto_Comercial()
                    {
                        Id = dm.id,
                        Nombre = dm.nombre,
                        Esbien = dm.valor == "1"
                    }); 
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener detalles de maestro", e);
            }
        }
        public IEnumerable<Familia_Concepto_Comercial> ObtenerFamiliasConceptoComercialPorRol(int idRol, string valor, bool esVigente)
        {
            try
            {
                return _db.Concepto_negocio_rol.Where(cnr => cnr.id_rol == idRol).Select(cnr => cnr.Concepto_negocio).Where(cnr => cnr.es_vigente == esVigente).Select(cn => cn.Detalle_maestro4).Where(dm => dm.valor == valor).Distinct()
                    .Select(dm => new Familia_Concepto_Comercial()
                    {
                        Id = dm.id,
                        Nombre = dm.nombre,
                        Esbien = dm.valor == "1"
                    }); ;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener detalles de maestro", e);
            }
        }
        public IEnumerable<Detalle_maestro> ObtenerDetallesVigentesPorValor(int idMaestro, bool esVigente, string valor)
        {
            try
            {
                return _db.Detalle_maestro.Where(dm => dm.id_maestro == idMaestro && dm.es_vigente == esVigente && dm.valor == valor);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Detalle_maestro> ObtenerDetalles(int[] idsMaestro, bool esVigente)
        {
            try
            {
                return _db.Detalle_maestro.Where(dm => idsMaestro.Contains(dm.id_maestro) && dm.es_vigente == esVigente);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener detalles maestro", e);
            }
        }

        public async Task<IEnumerable<Detalle_maestro>> ObtenerDetallesEspecificos(int[] idsDetalles)
        {
            try
            {
                var _asyncDb = new SigescomEntities();
                return await _asyncDb.Detalle_maestro.Where(dm => idsDetalles.Contains(dm.id)).ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public IEnumerable<Menu_aplicacion> obtenerMenus()
        {
            try
            {
                return _db.Menu_aplicacion;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public IEnumerable<Maestro> obtenerMaestros()
        {
            try
            {
                return _db.Maestro;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Documento_identidad> obtenerTiposDeDocumentosDeIdentidad()
        {
            try
            {
                return _db.Documento_identidad;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public IEnumerable<Ubigeo> obtenerUbigeos(int idPais)
        {
            try
            {
                return _db.Ubigeo.Where(u => u.id_distrito != 0 && u.id_pais == idPais).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Ubigeo> obtenerUbigeos(int[] idUbigeos)
        {
            try
            {
                return _db.Ubigeo.Where(u => idUbigeos.Contains(u.id));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Ubigeo> ObtenerRegiones()
        {
            try
            {
                return _db.Ubigeo.Where(u => u.id_distrito == 0 && u.id_provincia == 0).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Detalle_maestro> obtenerDetallesConcepto(int IdDetalleMaestro)
        {
            try
            {
                return _db.Categoria_concepto.Where(cc => cc.Detalle_maestro1.id == IdDetalleMaestro).Select(cc => cc.Detalle_maestro);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public IEnumerable<Detalle_maestro> obtenerDetallesMarca(int IdDetalleMaestro)
        {
            try
            {
                return _db.Marca_concepto.Where(mc => mc.Detalle_maestro.id == IdDetalleMaestro).Select(cc => cc.Detalle_maestro1);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Valor_caracteristica_concepto> obtenerValorCaracteristicaConcepto(int idConcepto)
        {
            try
            {
                return _db.Valor_caracteristica_concepto.Where(vcc => vcc.id_concepto == idConcepto);
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        public Detalle_maestro ObtenerDetalle(int idDetalle)
        {
            try
            {
                return _db.Detalle_maestro.FirstOrDefault(dm => dm.id == idDetalle);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Detalle_maestro ObtenerDetalleMaestroPorIdMaestroYNombre(int idMaestro, string nombre)
        {
            try
            {
                return _db.Detalle_maestro.SingleOrDefault(dm => dm.id_maestro == idMaestro && dm.nombre == nombre);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener detalle maestro por idMaestro y nombre", e);
            }
        }


        public Detalle_maestro ObtenerDetalle(int idDetalle, bool esVigente)
        {
            try
            {
                return _db.Detalle_maestro.Single(dm => dm.id == idDetalle && dm.es_vigente == esVigente);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Detalle_maestro ObtenerDetalleMaestroInclusiveCaracteristicaConceptoConDetalleMaestroYValorCaracteristica(int idDetalle, bool esVigente)
        {
            try
            {
                return _db.Detalle_maestro
                               .Include(dm => dm.Caracteristica_concepto)
                               .Include(dm => dm.Caracteristica_concepto.Select(cc => cc.Detalle_maestro1))
                               .Include(dm => dm.Caracteristica_concepto.Select(cc => cc.Detalle_maestro1.Valor_caracteristica))
                               .Single(dm => dm.id == idDetalle && dm.es_vigente == esVigente);
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        public Detalle_maestro ObtenerDetalleInclusiveValorCaracteristica(int idDetalle)
        {
            try
            {
                return _db.Detalle_maestro.Include(d => d.Valor_caracteristica.Select(vc => vc.Detalle_maestro)).Single(dm => dm.id == idDetalle);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Detalle_maestro> ObtenerDetallesInclusiveValorCaracteristica(int[] idsMaestro)
        {
            try
            {
                return _db.Detalle_maestro.Include(d => d.Valor_caracteristica.Select(vc => vc.Detalle_maestro)).Where(dm => idsMaestro.Contains(dm.id_maestro)).OrderBy(dm => dm.nombre);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public IEnumerable<Detalle_maestro> ObtenerDetallesVigentesInclusiveValorCaracteristica(int[] idsMaestro)
        {
            try
            {
                return _db.Detalle_maestro.Include(d => d.Valor_caracteristica.Select(vc => vc.Detalle_maestro)).Where(dm => idsMaestro.Contains(dm.id_maestro) && dm.es_vigente).OrderBy(dm => dm.nombre);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public Detalle_maestro obtenerDetalle(long idMaestro, string codigoDetalle)
        {
            try
            {
                return _db.Detalle_maestro.Single(dm => dm.id_maestro == idMaestro && dm.codigo == codigoDetalle);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Detalle_maestro ObtenerDetalleMaestro(int idMaestro, int idDetalleMaestro, bool esVigente)
        {
            try
            {
                return _db.Detalle_maestro.SingleOrDefault(dm => dm.id_maestro == idMaestro && dm.id == idDetalleMaestro && dm.es_vigente == esVigente);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener detalle_maestro", e);
            }

        }

        public IEnumerable<Detalle_maestro> obtenerDetallesMaestros(int IdMaestro)
        {
            return _db.Detalle_maestro.Where(dm => dm.id_maestro == IdMaestro);
        }



        public Maestro obtenerMaestro(int idMaestro)
        {
            throw new NotImplementedException();
        }


        public Tipo_cambio obtenerTipoDeCambio(DateTime date)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Detalle_maestro> obtenerDetalles(int[] idsMaestro)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Detalle_maestro> obtenerDetalles(int idMaestro, string filtroDeBusqueda)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Detalle_detalle_maestro> obtenerDetalleDetalles(int idDetalleMaestro, string filtroDeBusqueda)
        {
            throw new NotImplementedException();
        }


        public List<Documento_identidad> obtenerDocumentosIdentidad()
        {
            throw new NotImplementedException();
        }

        public List<Ubigeo> obtenerUbigeoDistrito()
        {
            throw new NotImplementedException();
        }

        public int obtenerNumeroMaximoDetalle(int idMaestroConfiguracionDeSistema)
        {
            throw new NotImplementedException();
        }

        public List<Detalle_maestro> obtenerDetallesEstados(int[] idsEstados)
        {
            throw new NotImplementedException();
        }



        public List<Detalle_maestro> obtenerDetallesVariedad(int idDetalle)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Detalle_maestro> ObtenerDetallesMaestrosIncluyendoCategoriaConcepto(int IdMaestro)
        {

            try
            {
                return _db.Detalle_maestro.Where(dm => dm.id_maestro == IdMaestro).Include(dm => dm.Categoria_concepto);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener concepto_negocio por nombre", e);
            }
        }

        public IEnumerable<Detalle_maestro> ObtenerDetallesMaestrosIncluyendoCategoriaConcepto(int IdMaestro, bool esVigente)
        {

            try
            {
                return _db.Detalle_maestro.Where(dm => dm.id_maestro == IdMaestro && dm.es_vigente == esVigente).Include(dm => dm.Categoria_concepto);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener concepto_negocio por nombre", e);
            }
        }
        public IEnumerable<Detalle_maestro> ObtenerTodoLosDetallesMaestrosIncluyendoCategoriaConcepto(int IdMaestro)
        {

            try
            {
                return _db.Detalle_maestro.Where(dm => dm.id_maestro == IdMaestro).Include(dm => dm.Categoria_concepto);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener concepto_negocio por nombre", e);
            }
        }
        /// <summary>
        /// devuelve el turno que corresponde a una fecha y hora dadas.
        /// </summary>
        /// <param name="fechaHoraReferencia"></param>
        /// <param name="idTipoTurno"></param>
        /// <returns></returns>
        public Turno ObtenerTurno(DateTime fechaHoraReferencia, int idTipoTurno)
        {
            var soloFechaReferencia = fechaHoraReferencia.Date;
            var turnos = _db.Turno.Where(t => t.id_tipo == idTipoTurno).ToList();
            var turno = turnos.FirstOrDefault(t => fechaHoraReferencia >= soloFechaReferencia.Add(t.hora_inicio) && fechaHoraReferencia <= soloFechaReferencia.Add(t.hora_inicio).AddHours((double)t.duracion_horas));
            return turno;
        }

        public List<Turno> ObtenerTurnos(int idTipoDeTurno)
        {
            return _db.Turno.Where(t => t.id_tipo == idTipoDeTurno).ToList();
        }


        #region VALIDACIONES 

        public bool ExisteNombreDeDetalleMaestro(int idMaestro, string nombre, bool esVigente)
        {
            try
            {
                return _db.Detalle_maestro.Where(dm => dm.id_maestro == idMaestro && dm.nombre == nombre && dm.es_vigente == esVigente).Any();
            }
            catch (Exception e)
            {
                throw new DatosException("Error al comprobar si existe el nombre de detalle_maestro ", e);
            }
        }
        public int ObtenerIdDetalleMaestro(int idMaestro, string codigo, bool esVigente)
        {
            try
            {
                return _db.Detalle_maestro.SingleOrDefault(dm => dm.id_maestro == idMaestro && dm.codigo == codigo && dm.es_vigente == esVigente).id;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener id de detalle_maestro ", e);
            }
        }

        #endregion
        public Tipo_cambio ObtenerTipoCambio(int idMoneda, DateTime fechaActual)
        {
            try
            {
                return _db.Tipo_cambio.SingleOrDefault(tc => tc.idMoneda == idMoneda && tc.fecha == fechaActual);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener el tipo de cambio", e);
            }
        }
        public OperationResult GuardarTipoCambio(Tipo_cambio tipoCambio)
        {
            try
            {
                _db.Tipo_cambio.Add(tipoCambio);
                var result = Save();
                result.data = tipoCambio.id;
                return result;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al guardar el tipo de cambio", e);
            }
        }
        public IEnumerable<Detalle_maestro> ObtenerDetallesPorFamilia(int idFamilia)
        {
            try
            {
                return _db.Caracteristica_concepto.Where(dm => dm.id_concepto == idFamilia).Select(dm => dm.Detalle_maestro1).Where(dm => dm.es_vigente);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener detalles maestro", e);
            }
        }
    }
}

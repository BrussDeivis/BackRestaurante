using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.AccesoDatos.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;

namespace Tsp.Sigescom.AccesoDatos
{
    public class ConceptoDatos : RepositorioBase, IConceptoRepositorio
    {
        #region CRUD CONCEPTO_NEGOCIO

        public OperationResult CrearConceptoDeNegocio(Concepto_negocio ConceptoDeNegocio)
        {
            try
            {
                _db.Concepto_negocio.Add(ConceptoDeNegocio);
                var resultado = Save();
                resultado.data = ConceptoDeNegocio.id;
                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public OperationResult CrearConceptoDeNegocioRol(Concepto_negocio_rol ConceptoDeNegocioRol)
        {
            try
            {
                _db.Concepto_negocio_rol.Add(ConceptoDeNegocioRol);
                var resultado = Save();
                resultado.data = ConceptoDeNegocioRol.id;
                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public OperationResult ActualizarConceptoNegocio(Concepto_negocio conceptoNegocio_upd)
        {
            try
            {
                Concepto_negocio conceptoNegocio_bd = _db.Concepto_negocio.Include(cn => cn.Parametro_concepto_negocio).Single(cn => cn.id == conceptoNegocio_upd.id);
                // conceptoNegocio_upd.codigo = conceptoNegocio_bd.codigo;
                _db.Entry(conceptoNegocio_bd).CurrentValues.SetValues(conceptoNegocio_upd);

                //valores caracteristicas
                List<Valor_caracteristica_concepto_negocio> valores_upd = conceptoNegocio_upd.Valor_caracteristica_concepto_negocio.ToList();
                List<Valor_caracteristica_concepto_negocio> valores_bd = conceptoNegocio_bd.Valor_caracteristica_concepto_negocio.ToList();

                foreach (var valor_bd in valores_bd)
                {
                    //si no existe, elimino
                    if (!valores_upd.Any(v => v.id_valor_caracteristica == valor_bd.id_valor_caracteristica))
                    {
                        _db.Valor_caracteristica_concepto_negocio.Remove(valor_bd);
                    }
                }
                //agrego los nuevos valores
                foreach (var valor_upd in valores_upd)
                {
                    if (!valores_bd.Any(v => v.id_valor_caracteristica == valor_upd.id_valor_caracteristica))
                    {
                        _db.Valor_caracteristica_concepto_negocio.Add(valor_upd);
                    }
                }
                //foto
                Foto foto_upd = conceptoNegocio_upd.Foto != null ? conceptoNegocio_upd.Foto : null;
                Foto foto_bd = conceptoNegocio_bd.Foto != null ? conceptoNegocio_bd.Foto : null;

                if (foto_upd != null)
                {
                    if (foto_bd != null && foto_bd.imagen != foto_upd.imagen)
                    {
                        foto_upd.id = foto_bd.id;
                        _db.Entry(foto_bd).CurrentValues.SetValues(foto_upd);
                    }
                    else
                    {
                        _db.Foto.Add(foto_upd);
                        conceptoNegocio_bd.Foto = foto_upd;

                    }
                }


                //Parametros del concepto de negocio
                //actualizamos datos de los paramentos
                List<Parametro_concepto_negocio> dbParametros = conceptoNegocio_bd.Parametro_concepto_negocio.ToList();
                List<Parametro_concepto_negocio> updParametros = conceptoNegocio_upd.Parametro_concepto_negocio.ToList();
                foreach (var dbParametro in dbParametros)
                {
                    //si existe el parametro, lo actualizamos
                    if (updParametros.Any(p => p.id_parametro == dbParametro.id_parametro))
                    {
                        dbParametro.id_valor_parametro = updParametros.Single(pan => pan.id_parametro == dbParametro.id_parametro).id_valor_parametro;
                        dbParametro.valor = updParametros.Single(pan => pan.id_parametro == dbParametro.id_parametro).valor;
                    }
                    else//sino eliminamos el parametro
                    {
                        _db.Parametro_concepto_negocio.Remove(dbParametro);
                    }
                }
                ///agregamos los nuevos parametros
                foreach (var updParametro in updParametros)
                {
                    if (!dbParametros.Any(p => p.id_parametro == updParametro.id_parametro))
                    {
                        _db.Parametro_concepto_negocio.Add(updParametro);
                    }
                }

                //Roles de concepto de negocio
                List<Concepto_negocio_rol> roles_upd = conceptoNegocio_upd.Concepto_negocio_rol.ToList();
                List<Concepto_negocio_rol> roles_bd = conceptoNegocio_bd.Concepto_negocio_rol.ToList();

                foreach (var rol_bd in roles_bd)
                {
                    //si no existe, elimino
                    if (!roles_upd.Any(v => v.id_rol == rol_bd.id_rol))
                    {
                        _db.Concepto_negocio_rol.Remove(rol_bd);
                    }
                }
                //agrego los nuevos valores
                foreach (var rol_upd in roles_upd)
                {
                    if (!roles_bd.Any(v => v.id_rol == rol_upd.id_rol))
                    {
                        _db.Concepto_negocio_rol.Add(rol_upd);
                    }
                }
                return Save();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public OperationResult ActualizarNombreConceptosNegocio(List<Concepto_negocio> conceptosNegocio)
        {
            try
            {
                foreach (var conceptoNegocio in conceptosNegocio)
                {
                    Concepto_negocio conceptoNegocio_bd = _db.Concepto_negocio.Single(cn => cn.id == conceptoNegocio.id);
                    conceptoNegocio_bd.nombre = conceptoNegocio.nombre;
                    _db.Entry(conceptoNegocio_bd).Property(x => x.nombre).IsModified = true;
                }
                var result = Save();
                return result;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al actualizar el nombre de los conceptos de negocio", e);
            }
        }
        public OperationResult ActualizarConcepto(Detalle_maestro concepto_upd)
        {
            try
            {
                Detalle_maestro concepto_bd = _db.Detalle_maestro.Single(dm => dm.id == concepto_upd.id);
                concepto_upd.fecha_registro = concepto_bd.fecha_registro;
                _db.Entry(concepto_bd).CurrentValues.SetValues(concepto_upd);

                List<Categoria_concepto> categorias_upd = concepto_upd.Categoria_concepto.ToList();
                List<Categoria_concepto> categorias_bd = concepto_bd.Categoria_concepto.ToList();

                foreach (var categoria_bd in categorias_bd)
                {
                    //si no existe, elimino
                    if (!categorias_upd.Any(d => d.id_categoria == categoria_bd.id_categoria && d.id_concepto == categoria_bd.id_concepto))
                    {
                        _db.Categoria_concepto.Remove(categoria_bd);
                    }
                }
                //agrego los nuevos valores
                foreach (var categoria_upd in categorias_upd)
                {
                    if (!categorias_bd.Any(d => d.id_categoria == categoria_upd.id_categoria && d.id_concepto == categoria_upd.id_concepto))
                    {
                        _db.Categoria_concepto.Add(categoria_upd);
                    }
                }
                List<Caracteristica_concepto> caracteristicas_upd = concepto_upd.Caracteristica_concepto.ToList();
                List<Caracteristica_concepto> caracteristicas_bd = concepto_bd.Caracteristica_concepto.ToList();

                foreach (var caracteristica_bd in caracteristicas_bd)
                {
                    //si no existe, elimino
                    if (!caracteristicas_upd.Any(d => d.id_caracteristica == caracteristica_bd.id_caracteristica && d.id_concepto == caracteristica_bd.id_concepto))
                    {
                        _db.Caracteristica_concepto.Remove(caracteristica_bd);
                    }
                }
                //agrego los nuevos valores
                foreach (var caracteristica_upd in caracteristicas_upd)
                {
                    if (!caracteristicas_bd.Any(d => d.id_caracteristica == caracteristica_upd.id_caracteristica && d.id_concepto == caracteristica_upd.id_concepto))
                    {
                        _db.Caracteristica_concepto.Add(caracteristica_upd);
                    }
                }

                var resultado = Save();
                resultado.data = concepto_upd.id;
                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public OperationResult DarDeBajaConceptoNegocio(int idConceptoNegocio)
        {
            try
            {
                //damos de baja al rol padre
                var dbConceptoNegocio = _db.Concepto_negocio.FirstOrDefault(an => an.id == idConceptoNegocio);
                dbConceptoNegocio.es_vigente = false;
                //registramos la fecha de la baja
                dbConceptoNegocio.fecha_baja = DateTimeUtil.FechaActual();
                //eliminamos el concepto del inventario actual.
                var detalles = _db.Detalle_transaccion.RemoveRange(_db.Transaccion.Where(t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInventarioActual).SelectMany(t => t.Detalle_transaccion).Where(dt => dt.id_concepto_negocio == idConceptoNegocio));
                return Save();
            }
            catch (Exception e)
            {
                throw new DatosException("Erro al dar de baja concepto_negocio", e);
            }
        }



        public bool TieneStockConceptoNegocio(int idConceptoNegocio)
        {
            try
            {
                var numeroInventarioMayorCero = _db.Transaccion.Where(t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionInventarioActual).SelectMany(t => t.Detalle_transaccion).Where(dt => dt.id_concepto_negocio == idConceptoNegocio).Where(dt => dt.cantidad > 0).Count();
                return numeroInventarioMayorCero > 0;
            }
            catch (Exception e)
            {
                throw new DatosException("Erro al dar de baja concepto_negocio", e);
            }

        }
        public OperationResult InvertirEsVigenteConceptoNegocio(int idConceptoNegocio)
        {
            try
            {
                //damos de baja al rol padre
                var dbConceptoNegocio = _db.Concepto_negocio.FirstOrDefault(an => an.id == idConceptoNegocio);
                dbConceptoNegocio.es_vigente = !dbConceptoNegocio.es_vigente;
                return Save();
            }
            catch (Exception e)
            {
                throw new DatosException("Erro al dar de baja concepto_negocio", e);
            }

        }

        #endregion

        public IEnumerable<Concepto_negocio> ObtenerConceptosDeNegocioPorRol(int idRol)
        {
            try
            {
                return _db.Concepto_negocio_rol
                        .Include(cnr => cnr.Rol)
                        .Include(cnr => cnr.Concepto_negocio)
                        .Include(cnr => cnr.Concepto_negocio.Detalle_maestro)
                        .Include(cnr => cnr.Concepto_negocio.Detalle_maestro4)
                        .Where(cnr => cnr.id_rol == idRol && cnr.Concepto_negocio.Precio1.Any()).ToList()
                        .Select(cnr => cnr.Concepto_negocio)
                        .Where(cnr => cnr.es_vigente);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Concepto_negocio> ObtenerConceptoPorRol(int idRol)
        {
            throw new NotImplementedException();
        }
        //
        public IEnumerable<Concepto_negocio> ObtenerConceptosDeNegocioPorRolIncluyendoExistenciasYPrecios(int idRol)
        {
            try
            {
                return _db.Concepto_negocio_rol.Include(cnr => cnr.Rol)
                    .Include(cnr => cnr.Concepto_negocio.Precio)
                    .Include(cnr => cnr.Concepto_negocio.Valor_caracteristica_concepto_negocio)
                    .Include(cnr => cnr.Concepto_negocio.Existencia)
                    .Where(cnr => cnr.id_rol == idRol)
                    .Select(cnr => cnr.Concepto_negocio);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //Metodo ya no se usa se cambio por el metodo: 
        [Obsolete("This method should not be used, use ObtenerReporteStockGeneral instead", true)]
        public IEnumerable<Concepto_negocio> ObtenerConceptosDeNegocioPorRolIncluyendoExistencia(int idRol)
        {
            try
            {
                return _db.Concepto_negocio_rol
                    .Include(cnr => cnr.Rol)
                    .Include(cnr => cnr.Concepto_negocio)
                    .Include(cnr => cnr.Concepto_negocio.Existencia)
                    .Include(cnr => cnr.Concepto_negocio.Detalle_maestro)
                    .Include(cnr => cnr.Concepto_negocio.Detalle_maestro4)
                    .Where(cnr => cnr.id_rol == idRol)
                    .Select(cnr => cnr.Concepto_negocio)
                    .Where(cn => cn.es_vigente).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Reporte_Stock_General> ObtenerReporteStockGeneral(int[] idsActorNegocioInterno, int idTipoTransaccionExistente, int idUltimoEstadoTransaccionExsitente)
        {
            try
            {
                var result = _db.Transaccion.Where(t => idsActorNegocioInterno.Contains(t.id_actor_negocio_interno)
                                        && t.id_tipo_transaccion == idTipoTransaccionExistente
                                        && t.Estado_transaccion.OrderByDescending(est => est.id).FirstOrDefault().id_estado == idUltimoEstadoTransaccionExsitente)
                                        .SelectMany(t => t.Detalle_transaccion)
                                        .Where(dt => dt.Concepto_negocio.es_vigente)
                                        .Select(dt => new Reporte_Stock_General()
                                        {
                                            CodigoBarraConceptoNegocio = dt.Concepto_negocio.codigo_barra,
                                            NombreConceptoNegocio = dt.Concepto_negocio.nombre,
                                            UnidadMedida = dt.Concepto_negocio.Detalle_maestro.codigo,
                                            Lote = dt.lote,
                                            Stock = dt.cantidad,
                                            NombreConcepto = dt.Concepto_negocio.Detalle_maestro4.nombre,
                                            NombreCentroAtencion = dt.Transaccion.Actor_negocio2.Actor.primer_nombre,
                                        }).ToList();
                result.ForEach(r => r.Stock = Math.Round(r.Stock, AplicacionSettings.Default.NumeroDecimalesEnCantidad));
                return result;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener reporte stock general", e);
            }
        }





        public IEnumerable<Concepto_negocio> ObtenerConceptosDeNegocioPorRolIncluyendoExistenciasYPreciosConUnPrecioVigenteComoMinimo(int idRol)
        {
            try
            {
                return _db.Concepto_negocio_rol.Include(cnr => cnr.Rol)
                    .Include(cnr => cnr.Concepto_negocio)
                    .Include(cnr => cnr.Concepto_negocio.Precio1)
                    .Include(cnr => cnr.Concepto_negocio.Existencia)
                    .Where(cnr => cnr.id_rol == idRol && cnr.Concepto_negocio.Precio1.Any(p => p.es_vigente)).ToList()
                    .Select(cnr => cnr.Concepto_negocio);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Concepto_negocio> ObtenerConceptosDeNegocioPorRolYPorConceptoBasico(int idRol, int idConceptoBasico)
        {
            try
            {
                return _db.Concepto_negocio_rol.Include(cnr => cnr.Rol)
                    .Where(cnr => cnr.id_rol == idRol && cnr.Concepto_negocio.id_concepto_basico == idConceptoBasico)
                    .Select(cnr => cnr.Concepto_negocio);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Concepto_negocio> ObtenerConceptosDeNegocioPorRolIncluyendoExistenciasYPrecios(int idRol, int idConceptoBasico)
        {
            try
            {
                return _db.Concepto_negocio_rol
                            .Include(cnr => cnr.Rol)
                            .Include(cnr => cnr.Concepto_negocio.Valor_caracteristica_concepto_negocio)
                            .Include(cnr => cnr.Concepto_negocio.Existencia)
                            .Where(cnr => cnr.id_rol == idRol && cnr.Concepto_negocio.id_concepto_basico == idConceptoBasico)
                            .Select(cnr => cnr.Concepto_negocio)
                            .Where(cnr => cnr.es_vigente);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Concepto_negocio> ObtenerConceptosDeNegocioPorRolIncluyendoExistenciasYPreciosConUnPrecioVigenteComoMinimoPorActorNegocio(int idActorNegocio, int idRol, int idConceptoBasico)
        {
            try
            {
                return _db.Concepto_negocio_rol
                                               .Include(cnr => cnr.Concepto_negocio.Precio1)
                                               .Include(cnr => cnr.Concepto_negocio.Precio1.Select(p => p.Detalle_maestro3))
                                               .Include(cnr => cnr.Concepto_negocio.Detalle_maestro4.Caracteristica_concepto)
                                               .Include(cnr => cnr.Concepto_negocio.Detalle_maestro4
                                                                                   .Caracteristica_concepto
                                                                                   .Select(cc => cc.Detalle_maestro1)
                                                        )
                                               .Include(cnr => cnr.Concepto_negocio.Existencia)
                                               .Where(cnr => cnr.id_rol == idRol
                                                             && cnr.Concepto_negocio.id_concepto_basico == idConceptoBasico
                                                             && cnr.Concepto_negocio
                                                                   .Precio1.
                                                                    Any(p => p.id_actor_negocio == idActorNegocio
                                                             && p.es_vigente)).ToList()
                                               .Select(cnr => cnr.Concepto_negocio)
                                               .Where(cn => cn.es_vigente)
                                               .Distinct();
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener conceptos de negocio por rol y concepto basico", e);
            }

        }

        public IEnumerable<Concepto_negocio> ObtenerConceptosDeNegocioPorRolesIncluyendoExistenciasYPrecios(int idRol)
        {
            try
            {
                return _db.Concepto_negocio_rol.Include(cnr => cnr.Rol)
                    .Include(cnr => cnr.Concepto_negocio.Precio)
                    .Include(cnr => cnr.Concepto_negocio.Valor_caracteristica_concepto_negocio)
                    .Include(cnr => cnr.Concepto_negocio.Existencia)
                    .Where(cnr => cnr.id_rol == idRol)
                    .Select(cnr => cnr.Concepto_negocio);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Concepto_negocio> ObtenerConceptosDeNegocioConPreciosPorFamilia(int idFamilia)
        {
            try
            {
                return _db.Concepto_negocio.Where(cn => cn.id_concepto_basico == idFamilia)
                    .Include(cn => cn.Precio)
                    .Include(cn => cn.Valor_caracteristica_concepto_negocio);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Concepto_negocio> ObtenerConceptosDeNegocioPorRolesIncluyendoExistenciasYPrecios(int idRol, bool esVigente)
        {
            try
            {
                return _db.Concepto_negocio_rol.Include(cnr => cnr.Rol)
                    .Include(cnr => cnr.Concepto_negocio.Precio)
                    .Include(cnr => cnr.Concepto_negocio.Valor_caracteristica_concepto_negocio)
                    .Include(cnr => cnr.Concepto_negocio.Existencia)
                    .Where(cnr => cnr.id_rol == idRol)
                    .Select(cnr => cnr.Concepto_negocio)
                    .Where(cnr => cnr.es_vigente == esVigente);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Concepto_negocio> ObtenerConceptosDeNegocioPorRolIncluyendoExistenciasPreciosYDetallesDeMaestro(int idRol, int idConceptoBasico)
        {
            try
            {
                var resultado =
                 _db.Concepto_negocio_rol.Where(cnr => cnr.id_rol == idRol).Include(cnr => cnr.Rol).Select(cnr => cnr.Concepto_negocio).Where(cn => cn.es_vigente)
                     .Include(cn => cn.Precio)
                     .Include(cn => cn.Valor_caracteristica_concepto_negocio)
                     .Include(cn => cn.Existencia)
                     .Include(cn => cn.Detalle_maestro)
                     .Include(cn => cn.Detalle_maestro1)
                     .Include(cn => cn.Detalle_maestro2)
                     .Include(cn => cn.Detalle_maestro3)
                     .Include(cn => cn.Detalle_maestro4)
                     .Include(cn => cn.Parametro_concepto_negocio)
                     .Include(cn => cn.Precio1)
                     .Where(cn => cn.id_concepto_basico == idConceptoBasico);

                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Concepto_negocio> ObtenerConceptosDeNegocioPorRolIncluyendoExistenciasPreciosYDetallesDeMaestro(int idRol, int idConceptoBasico, int[] idsValoresDeCaracteristicas)
        {

            try
            {
                var length = idsValoresDeCaracteristicas.Length;
                var resultado =
                 _db.Concepto_negocio_rol.Where(cnr => cnr.id_rol == idRol).Include(cnr => cnr.Rol).Select(cnr => cnr.Concepto_negocio).Where(cn => cn.es_vigente)
                     .Include(cn => cn.Precio)
                     .Include(cn => cn.Valor_caracteristica_concepto_negocio)
                     .Include(cn => cn.Existencia)
                     .Include(cn => cn.Detalle_maestro)
                     .Include(cn => cn.Detalle_maestro1)
                     .Include(cn => cn.Detalle_maestro2)
                     .Include(cn => cn.Detalle_maestro3)
                     .Include(cn => cn.Detalle_maestro4)
                     .Include(cn => cn.Parametro_concepto_negocio)
                     .Include(cn => cn.Precio1)
                     .Where(cn =>
                    cn.id_concepto_basico == idConceptoBasico

                     && cn.Valor_caracteristica_concepto_negocio.Where(vcc => idsValoresDeCaracteristicas.Contains(vcc.id_valor_caracteristica)).Count() == length);
                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Concepto_negocio> ObtenerConceptosDeNegocioPorRolIncluyendoExistenciasPreciosYDetallesDeMaestro(int idRol, int[] idsValoresDeCaracteristicas)
        {

            try

            {
                var length = idsValoresDeCaracteristicas.Length;
                var resultado =
                 _db.Concepto_negocio_rol.Where(cnr => cnr.id_rol == idRol).Include(cnr => cnr.Rol).Select(cnr => cnr.Concepto_negocio).Where(cn => cn.es_vigente)
                     .Include(cn => cn.Precio)
                     .Include(cn => cn.Valor_caracteristica_concepto_negocio)
                     .Include(cn => cn.Existencia)
                     .Include(cn => cn.Detalle_maestro)
                     .Include(cn => cn.Detalle_maestro1)
                     .Include(cn => cn.Detalle_maestro2)
                     .Include(cn => cn.Detalle_maestro3)
                     .Include(cn => cn.Detalle_maestro4)
                     .Include(cn => cn.Parametro_concepto_negocio)
                     .Include(cn => cn.Precio1)
                     .Where(
                     cn => cn.Valor_caracteristica_concepto_negocio.Where(vcc => idsValoresDeCaracteristicas.Contains(vcc.id_valor_caracteristica)).Count() == length);
                return resultado;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener conceptos de negocio", e);
            }
        }

        public IEnumerable<Concepto_negocio> ObtenerConceptosDeNegocioPorRolIncluyendoExistenciasYPreciosConUnPrecioVigenteComoMinimoParaActorNegocio(int idActorNegocio, int idRol)
        {
            try
            {
                return _db.Concepto_negocio_rol.Include(cnr => cnr.Rol)
                    .Include(cnr => cnr.Concepto_negocio)
                    .Include(cnr => cnr.Concepto_negocio.Precio1)
                    .Include(cnr => cnr.Concepto_negocio.Existencia)
                    .Where(cnr => cnr.id_rol == idRol && cnr.Concepto_negocio.Precio1.Any(p => p.id_actor_negocio == idActorNegocio && p.es_vigente)).ToList()
                    .Select(cnr => cnr.Concepto_negocio);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public Concepto_negocio ObtenerConceptoNegocioIncluyendoValorCaracteristicaConceptoNegocioYDetalleMaestroYCaracteristicaConcepto(int idConcepto)
        {
            try
            {
                return _db.Concepto_negocio
                                           .Include(cn => cn.Valor_caracteristica_concepto_negocio)
                                           .Include(dt => dt.Detalle_maestro4)
                                           .Include(dt => dt.Detalle_maestro4.Caracteristica_concepto)
                                           .Single(cnr => cnr.id == idConcepto);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Concepto_negocio ObtenerConceptoNegocioPorNombre(string nombre)
        {
            try
            {
                var conceptoNegocio = _db.Concepto_negocio
                                         .Where(cn => cn.nombre == nombre && cn.es_vigente)
                                         .FirstOrDefault();
                return conceptoNegocio;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener concepto_negocio por nombre", e);
            }
        }

        public Concepto_negocio ObtenerConceptoDeNegocioPorRolYIdConceptoNegocio(int idRol, int idConceptoNegocio)
        {
            try
            {
                return _db.Concepto_negocio_rol
                    .Where(cnr => cnr.id_rol == idRol)
                    .Select(cn => cn.Concepto_negocio)
                    .Where(cn => cn.id == idConceptoNegocio).FirstOrDefault();

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Concepto_negocio ObtenerConceptoDeNegocioPorIdConceptoNegocio(int idConceptoNegocio)
        {
            try
            {
                return _db.Concepto_negocio.Include(cn => cn.Detalle_maestro4)
                    .Where(cn => cn.id == idConceptoNegocio).FirstOrDefault();

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Concepto_negocio> ObtenerConceptosDeNegocioIncluyendoDetalleMaestro4(int[] idsConceptoNegocio)
        {
            try
            {
                return _db.Concepto_negocio.Include(cn => cn.Detalle_maestro4)
                    .Where(cn => idsConceptoNegocio.Contains(cn.id));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Concepto_negocio ObtenerConceptosDeNegocioPorRolYCodigoBarra(int idRol, string codigoBarra)
        {
            try
            {
                var resultado = _db.Concepto_negocio_rol.Include(cnr => cnr.Rol)
                    .Include(cnr => cnr.Concepto_negocio.Precio)
                    .Include(cnr => cnr.Concepto_negocio.Valor_caracteristica_concepto_negocio)
                    .Include(cnr => cnr.Concepto_negocio.Existencia)
                    .SingleOrDefault(cnr => cnr.id_rol == idRol && cnr.Concepto_negocio.codigo_barra == codigoBarra);
                return resultado != null ? resultado.Concepto_negocio : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Concepto_negocio ObtenerConceptoDeNegocioPorCodigoBarraIncluyendoExistenciasYPreciosConUnPrecioVigenteComoMinimoParaActorNegocio(int idActorNegocio, int idRol, string codigoBarra)
        {
            try
            {
                var resultado = _db.Concepto_negocio_rol.Include(cnr => cnr.Rol)
                    .Include(cnr => cnr.Concepto_negocio)
                    .Include(cnr => cnr.Concepto_negocio.Precio1)
                    .Include(cnr => cnr.Concepto_negocio.Existencia)
                    .SingleOrDefault(cnr => cnr.Concepto_negocio.codigo_barra == codigoBarra && cnr.id_rol == idRol
                    && cnr.Concepto_negocio.Precio1.Any(p => p.id_actor_negocio == idActorNegocio && p.es_vigente));
                return resultado?.Concepto_negocio;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public bool ExisteCodigoBarraEnConceptoVigente(int idRol, string codigoBarra)
        {
            try
            {
                var resultado = _db.Concepto_negocio_rol.Any(cnr => cnr.id_rol == idRol && cnr.Concepto_negocio.es_vigente && cnr.Concepto_negocio.codigo_barra == codigoBarra && codigoBarra != null);
                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool ExisteCodigoBarraEnConceptoVigenteAlActuaizar(int idConcepto, int idRol, string codigoBarra)
        {
            try
            {
                var resultado = _db.Concepto_negocio_rol.Any(cnr => cnr.id_rol == idRol && cnr.Concepto_negocio.es_vigente && cnr.Concepto_negocio.codigo_barra == codigoBarra && codigoBarra != null && cnr.Concepto_negocio.id != idConcepto);
                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #region CONCEPTOS NEGOCIOS COMERCIALES SIN STOCK Y PRECIOS
        public IEnumerable<Concepto_Negocio_Comercial> ObtenerConceptosDeNegocioComercialesPorRolesYIdConceptoBasicoYIdCategoriaYIdsValoresCaracteristicas(int[] idsRoles, int idConceptoBasico, int idCategoria, int[] idsValoresDeCaracteristicas)
        {
            try
            {
                var length = idsValoresDeCaracteristicas.Length;
                var resultado = _db.Concepto_negocio_rol.Where(cnr => idsRoles.Contains(cnr.id_rol))
                                                        .Select(cnr => cnr.Concepto_negocio)
                                                        .Where(cn => cn.Detalle_maestro4.Categoria_concepto.Select(cc => cc.id_categoria).Contains(idCategoria) && cn.id_concepto_basico == idConceptoBasico && cn.Valor_caracteristica_concepto_negocio.Where(vcc => idsValoresDeCaracteristicas.Contains(vcc.id_valor_caracteristica)).Count() == length && cn.es_vigente)
                                                        .Select(cn => new Concepto_Negocio_Comercial()
                                                        {
                                                            Id = cn.id,
                                                            Nombre = cn.nombre,
                                                            Codigo = cn.codigo,
                                                            CodigoBarra = cn.codigo_barra,
                                                            NombrePresentacion = cn.Detalle_maestro3.nombre,
                                                            Contenido = cn.contenido,
                                                            CodigoUnidadMedidaPresentacion = cn.Detalle_maestro1.codigo,
                                                            CodigoUnidadMedidaComercial = cn.Detalle_maestro.codigo
                                                        });
                return resultado;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener conceptos de negocio por roles y concepto basico y caracteristicas", e);
            }
        }
        public IEnumerable<Concepto_Negocio_Comercial> ObtenerConceptosDeNegocioComercialesPorRolesYIdConceptoBasicoYIdsValoresCaracteristicas(int[] idsRoles, int idConceptoBasico, int[] idsValoresDeCaracteristicas)
        {
            try
            {
                var length = idsValoresDeCaracteristicas.Length;
                var resultado = _db.Concepto_negocio_rol.Where(cnr => idsRoles.Contains(cnr.id_rol))
                                                        .Select(cnr => cnr.Concepto_negocio)
                                                        .Where(cn => cn.id_concepto_basico == idConceptoBasico && cn.Valor_caracteristica_concepto_negocio.Where(vcc => idsValoresDeCaracteristicas.Contains(vcc.id_valor_caracteristica)).Count() == length && cn.es_vigente)
                                                        .Select(cn => new Concepto_Negocio_Comercial()
                                                        {
                                                            Id = cn.id,
                                                            Nombre = cn.nombre,
                                                            Codigo = cn.codigo,
                                                            CodigoBarra = cn.codigo_barra,
                                                            NombrePresentacion = cn.Detalle_maestro3.nombre,
                                                            Contenido = cn.contenido,
                                                            CodigoUnidadMedidaPresentacion = cn.Detalle_maestro1.codigo,
                                                            CodigoUnidadMedidaComercial = cn.Detalle_maestro.codigo
                                                        });
                return resultado;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener conceptos de negocio por roles y concepto basico y caracteristicas", e);
            }
        }
        public IEnumerable<Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesPorRolesYIdCategoriaYIdsValoresDeCaracteristicas(int[] idsRoles, int idCategoria, int[] idsValoresDeCaracteristicas)
        {
            try
            {
                var length = idsValoresDeCaracteristicas.Length;
                var resultado = _db.Concepto_negocio_rol.Where(cnr => idsRoles.Contains(cnr.id_rol))
                                                        .Select(cnr => cnr.Concepto_negocio)
                                                        .Where(cn => cn.Detalle_maestro4.Categoria_concepto.Select(cc => cc.id_categoria).Contains(idCategoria) && cn.Valor_caracteristica_concepto_negocio.Where(vcc => idsValoresDeCaracteristicas.Contains(vcc.id_valor_caracteristica)).Count() == length && cn.es_vigente)
                                                        .Select(cn => new Concepto_Negocio_Comercial()
                                                        {
                                                            Id = cn.id,
                                                            Nombre = cn.nombre,
                                                            Codigo = cn.codigo,
                                                            CodigoBarra = cn.codigo_barra,
                                                            NombrePresentacion = cn.Detalle_maestro3.nombre,
                                                            Contenido = cn.contenido,
                                                            CodigoUnidadMedidaPresentacion = cn.Detalle_maestro1.codigo,
                                                            CodigoUnidadMedidaComercial = cn.Detalle_maestro.codigo
                                                        });
                return resultado;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener conceptos de negocio por roles y caracteristicas", e);
            }
        }
        public IEnumerable<Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesPorRolesYIdsValoresDeCaracteristicas(int[] idsRoles, int[] idsValoresDeCaracteristicas)
        {
            try
            {
                var length = idsValoresDeCaracteristicas.Length;
                var resultado = _db.Concepto_negocio_rol.Where(cnr => idsRoles.Contains(cnr.id_rol))
                                                        .Select(cnr => cnr.Concepto_negocio)
                                                        .Where(cn => cn.Valor_caracteristica_concepto_negocio.Where(vcc => idsValoresDeCaracteristicas.Contains(vcc.id_valor_caracteristica)).Count() == length && cn.es_vigente)
                                                        .Select(cn => new Concepto_Negocio_Comercial()
                                                        {
                                                            Id = cn.id,
                                                            Nombre = cn.nombre,
                                                            Codigo = cn.codigo,
                                                            CodigoBarra = cn.codigo_barra,
                                                            NombrePresentacion = cn.Detalle_maestro3.nombre,
                                                            Contenido = cn.contenido,
                                                            CodigoUnidadMedidaPresentacion = cn.Detalle_maestro1.codigo,
                                                            CodigoUnidadMedidaComercial = cn.Detalle_maestro.codigo
                                                        });
                return resultado;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener conceptos de negocio por roles y caracteristicas", e);
            }
        }
        public IEnumerable<Concepto_Negocio_Comercial> ObtenerConceptosDeNegocioComercialesPorRolesYIdConceptoBasicoYIdCategoria(int[] idsRoles, int idConceptoBasico, int idCategoria)
        {
            try
            {
                var resultado = _db.Concepto_negocio_rol.Where(cnr => idsRoles.Contains(cnr.id_rol))
                                                          .Select(cnr => cnr.Concepto_negocio).Where(cn => cn.Detalle_maestro4.Categoria_concepto.Select(cc => cc.id_categoria).Contains(idCategoria) && cn.id_concepto_basico == idConceptoBasico && cn.es_vigente)
                                                          .Select(cn => new Concepto_Negocio_Comercial()
                                                          {
                                                              Id = cn.id,
                                                              Nombre = cn.nombre,
                                                              Codigo = cn.codigo,
                                                              CodigoBarra = cn.codigo_barra,
                                                              NombrePresentacion = cn.Detalle_maestro3.nombre,
                                                              Contenido = cn.contenido,
                                                              CodigoUnidadMedidaPresentacion = cn.Detalle_maestro1.codigo,
                                                              CodigoUnidadMedidaComercial = cn.Detalle_maestro.codigo
                                                          });
                return resultado;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener conceptos de negocios por roles y concepto basico", e);
            }
        }
        public IEnumerable<Concepto_Negocio_Comercial> ObtenerConceptosDeNegocioComercialesPorRolesYIdConceptoBasico(int[] idsRoles, int idConceptoBasico)
        {
            try
            {
                var resultado = _db.Concepto_negocio_rol.Where(cnr => idsRoles.Contains(cnr.id_rol))
                                                          .Select(cnr => cnr.Concepto_negocio).Where(cn => cn.id_concepto_basico == idConceptoBasico && cn.es_vigente)
                                                          .Select(cn => new Concepto_Negocio_Comercial()
                                                          {
                                                              Id = cn.id,
                                                              Nombre = cn.nombre,
                                                              Codigo = cn.codigo,
                                                              CodigoBarra = cn.codigo_barra,
                                                              NombrePresentacion = cn.Detalle_maestro3.nombre,
                                                              Contenido = cn.contenido,
                                                              CodigoUnidadMedidaPresentacion = cn.Detalle_maestro1.codigo,
                                                              CodigoUnidadMedidaComercial = cn.Detalle_maestro.codigo
                                                          });
                return resultado;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener conceptos de negocios por roles y concepto basico", e);
            }
        }
        public IEnumerable<Concepto_Negocio_Comercial> ObtenerConceptosDeNegocioComercialesPorRolesYIdCategoria(int[] idsRoles, int idCategoria)
        {
            try
            {
                var resultado = _db.Concepto_negocio_rol.Where(cnr => idsRoles.Contains(cnr.id_rol))
                                                          .Select(cnr => cnr.Concepto_negocio).Where(cn => cn.Detalle_maestro4.Categoria_concepto.Select(cc => cc.id_categoria).Contains(idCategoria) && cn.es_vigente)
                                                          .Select(cn => new Concepto_Negocio_Comercial()
                                                          {
                                                              Id = cn.id,
                                                              Nombre = cn.nombre,
                                                              Codigo = cn.codigo,
                                                              CodigoBarra = cn.codigo_barra,
                                                              NombrePresentacion = cn.Detalle_maestro3.nombre,
                                                              Contenido = cn.contenido,
                                                              CodigoUnidadMedidaPresentacion = cn.Detalle_maestro1.codigo,
                                                              CodigoUnidadMedidaComercial = cn.Detalle_maestro.codigo
                                                          });
                return resultado;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener conceptos de negocios por roles", e);
            }
        }
        public IEnumerable<Concepto_Negocio_Comercial> ObtenerConceptosDeNegocioComercialesPorRoles(int[] idsRoles)
        {
            try
            {
                var resultado = _db.Concepto_negocio_rol.Where(cnr => idsRoles.Contains(cnr.id_rol))
                                                          .Select(cnr => cnr.Concepto_negocio).Where(cn => cn.es_vigente)
                                                          .Select(cn => new Concepto_Negocio_Comercial()
                                                          {
                                                              Id = cn.id,
                                                              Nombre = cn.nombre,
                                                              Codigo = cn.codigo,
                                                              CodigoBarra = cn.codigo_barra,
                                                              NombrePresentacion = cn.Detalle_maestro3.nombre,
                                                              Contenido = cn.contenido,
                                                              CodigoUnidadMedidaPresentacion = cn.Detalle_maestro1.codigo,
                                                              CodigoUnidadMedidaComercial = cn.Detalle_maestro.codigo
                                                          });
                return resultado;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener conceptos de negocios por roles", e);
            }
        }
        #endregion

        #region CONCEPTOS NEGOCIOS COMERCIALES CON STOCK Y PRECIOS
        public IEnumerable<Concepto_Negocio_Comercial> ObtenerConceptosDeNegocioComercialesPorRolesYIdConceptoBasicoYIdCategoriaYIdsValoresCaracteristicasInclyendoStockYPrecios(long idTransaccionExistente, int idActorNegocioInternoQueTieneLosPrecios, int[] idsRoles, int idTarifa, int[] idsTiposTransaccion, int idConceptoBasico, int idCategoria, int[] idsValoresDeCaracteristicas)
        {
            try
            {
                var length = idsValoresDeCaracteristicas.Length;
                var resultado = _db.Concepto_negocio_rol.Where(cnr => idsRoles.Contains(cnr.id_rol))
                                                        .Select(cnr => cnr.Concepto_negocio)
                                                        .Where(cn => cn.Detalle_maestro4.Categoria_concepto.Select(cc => cc.id_categoria).Contains(idCategoria) && cn.es_vigente)
                                                        .Where(cn => cn.id_concepto_basico == idConceptoBasico
                                                        && cn.Valor_caracteristica_concepto_negocio.Where(vcc => idsValoresDeCaracteristicas.Contains(vcc.id_valor_caracteristica)).Count() == length)
                                 .Select(cn => new Concepto_Negocio_Comercial()
                                 {
                                     Id = cn.id,
                                     Nombre = cn.nombre,
                                     Codigo = cn.codigo,
                                     CodigoBarra = cn.codigo_barra,
                                     NombrePresentacion = cn.Detalle_maestro3.nombre,
                                     Contenido = cn.contenido,
                                     CodigoUnidadMedidaPresentacion = cn.Detalle_maestro1.codigo,
                                     CodigoUnidadMedidaComercial = cn.Detalle_maestro.codigo,
                                     Stock = cn.Detalle_transaccion.Where(dt => dt.id_transaccion == idTransaccionExistente).Sum(dt => dt.cantidad),
                                     PrecioVenta = cn.Precio1.Where(p => p.id_actor_negocio == idActorNegocioInternoQueTieneLosPrecios && p.es_vigente && p.id_tarifa_d == idTarifa)
                                                             .OrderByDescending(p => p.id)
                                                             .FirstOrDefault().valor,
                                     CostoUnitario = cn.Detalle_transaccion.Where(d => d.Transaccion.id_actor_negocio_interno == idActorNegocioInternoQueTieneLosPrecios && idsTiposTransaccion.Contains(d.Transaccion.id_tipo_transaccion) && d.Transaccion.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                                                                           .OrderByDescending(dt => dt.Transaccion.fecha_inicio)
                                                                           .ThenByDescending(dt => dt.id)
                                                                           .FirstOrDefault().precio_unitario
                                 });
                return resultado;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener conceptos de negocio por concepto basico y caracteristicas", e);
            }
        }
        public IEnumerable<Concepto_Negocio_Comercial> ObtenerConceptosDeNegocioComercialesPorRolesYIdConceptoBasicoYIdsValoresCaracteristicasInclyendoStockYPrecios(long idTransaccionExistente, int idActorNegocioInternoQueTieneLosPrecios, int[] idsRoles, int idTarifa, int[] idsTiposTransaccion, int idConceptoBasico, int[] idsValoresDeCaracteristicas)
        {
            try
            {
                var length = idsValoresDeCaracteristicas.Length;
                var resultado = _db.Concepto_negocio_rol.Where(cnr => idsRoles.Contains(cnr.id_rol))
                                                        .Select(cnr => cnr.Concepto_negocio)
                                                        .Where(cn => cn.es_vigente)
                                                        .Where(cn =>
                                                        cn.id_concepto_basico == idConceptoBasico
                                                        && cn.Valor_caracteristica_concepto_negocio.Where(vcc => idsValoresDeCaracteristicas.Contains(vcc.id_valor_caracteristica)).Count() == length)
                                 .Select(cn => new Concepto_Negocio_Comercial()
                                 {
                                     Id = cn.id,
                                     Nombre = cn.nombre,
                                     Codigo = cn.codigo,
                                     CodigoBarra = cn.codigo_barra,
                                     NombrePresentacion = cn.Detalle_maestro3.nombre,
                                     Contenido = cn.contenido,
                                     CodigoUnidadMedidaPresentacion = cn.Detalle_maestro1.codigo,
                                     CodigoUnidadMedidaComercial = cn.Detalle_maestro.codigo,
                                     Stock = cn.Detalle_transaccion.Where(dt => dt.id_transaccion == idTransaccionExistente).Sum(dt => dt.cantidad),
                                     PrecioVenta = cn.Precio1.Where(p => p.id_actor_negocio == idActorNegocioInternoQueTieneLosPrecios && p.es_vigente && p.id_tarifa_d == idTarifa)
                                                             .OrderByDescending(p => p.id)
                                                             .FirstOrDefault().valor,
                                     CostoUnitario = cn.Detalle_transaccion.Where(d => d.Transaccion.id_actor_negocio_interno == idActorNegocioInternoQueTieneLosPrecios && idsTiposTransaccion.Contains(d.Transaccion.id_tipo_transaccion) && d.Transaccion.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                                                                           .OrderByDescending(dt => dt.Transaccion.fecha_inicio)
                                                                           .ThenByDescending(dt => dt.id)
                                                                           .FirstOrDefault().precio_unitario
                                 });
                return resultado;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener conceptos de negocio por concepto basico y caracteristicas", e);
            }
        }
        public IEnumerable<Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesPorRolesYIdCategoriaYIdsValoresDeCaracteristicasInclyendoStockYPrecios(long idTransaccionExistente, int idActorNegocioInternoQueTieneLosPrecios, int[] idsRoles, int idTarifa, int[] idsTiposTransaccion, int idCategoria, int[] idsValoresDeCaracteristicas)
        {

            try
            {
                var length = idsValoresDeCaracteristicas.Length;
                var resultado = _db.Concepto_negocio_rol.Where(cnr => idsRoles.Contains(cnr.id_rol))
                                                        .Select(cn => cn.Concepto_negocio)
                                                        .Where(cn => cn.Detalle_maestro4.Categoria_concepto.Select(cc => cc.id_categoria).Contains(idCategoria) && cn.es_vigente)
                                                        .Where(cn => cn.Valor_caracteristica_concepto_negocio
                                                                        .Where(vcc => idsValoresDeCaracteristicas.Contains(vcc.id_valor_caracteristica)).Count() == length)
                                                        .Select(cn => new Concepto_Negocio_Comercial()
                                                        {
                                                            Id = cn.id,
                                                            Nombre = cn.nombre,
                                                            Codigo = cn.codigo,
                                                            CodigoBarra = cn.codigo_barra,
                                                            NombrePresentacion = cn.Detalle_maestro3.nombre,
                                                            Contenido = cn.contenido,
                                                            CodigoUnidadMedidaPresentacion = cn.Detalle_maestro1.codigo,
                                                            CodigoUnidadMedidaComercial = cn.Detalle_maestro.codigo,
                                                            Stock = cn.Detalle_transaccion.Where(dt => dt.id_transaccion == idTransaccionExistente).Sum(dt => dt.cantidad),
                                                            PrecioVenta = cn.Precio1
                                                                            .Where(p => p.id_actor_negocio == idActorNegocioInternoQueTieneLosPrecios && p.es_vigente && p.id_tarifa_d == idTarifa)
                                                                            .OrderByDescending(p => p.id)
                                                                            .FirstOrDefault().valor,
                                                            CostoUnitario = cn.Detalle_transaccion
                                                                                  .Where(d => d.Transaccion.id_actor_negocio_interno == idActorNegocioInternoQueTieneLosPrecios && idsTiposTransaccion.Contains(d.Transaccion.id_tipo_transaccion) && d.Transaccion.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                                                                                  .OrderByDescending(dt => dt.Transaccion.fecha_inicio)
                                                                                  .ThenByDescending(dt => dt.id)
                                                                                  .FirstOrDefault().precio_unitario
                                                        });
                return resultado;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener conceptos de negocio por caracteristicas", e);
            }
        }
        public IEnumerable<Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesPorRolesYIdsValoresDeCaracteristicasInclyendoStockYPrecios(long idTransaccionExistente, int idActorNegocioInternoQueTieneLosPrecios, int[] idsRoles, int idTarifa, int[] idsTiposTransaccion, int[] idsValoresDeCaracteristicas)
        {

            try
            {
                var length = idsValoresDeCaracteristicas.Length;
                var resultado = _db.Concepto_negocio_rol.Where(cnr => idsRoles.Contains(cnr.id_rol))
                                                        .Select(cn => cn.Concepto_negocio)
                                                        .Where(cn => cn.es_vigente)
                                                        .Where(cn => cn.Valor_caracteristica_concepto_negocio
                                                                        .Where(vcc => idsValoresDeCaracteristicas.Contains(vcc.id_valor_caracteristica)).Count() == length)
                                                        .Select(cn => new Concepto_Negocio_Comercial()
                                                        {
                                                            Id = cn.id,
                                                            Nombre = cn.nombre,
                                                            Codigo = cn.codigo,
                                                            CodigoBarra = cn.codigo_barra,
                                                            NombrePresentacion = cn.Detalle_maestro3.nombre,
                                                            Contenido = cn.contenido,
                                                            CodigoUnidadMedidaPresentacion = cn.Detalle_maestro1.codigo,
                                                            CodigoUnidadMedidaComercial = cn.Detalle_maestro.codigo,
                                                            Stock = cn.Detalle_transaccion.Where(dt => dt.id_transaccion == idTransaccionExistente).Sum(dt => dt.cantidad),
                                                            PrecioVenta = cn.Precio1
                                                                            .Where(p => p.id_actor_negocio == idActorNegocioInternoQueTieneLosPrecios && p.es_vigente && p.id_tarifa_d == idTarifa)
                                                                            .OrderByDescending(p => p.id)
                                                                            .FirstOrDefault().valor,
                                                            CostoUnitario = cn.Detalle_transaccion
                                                                                  .Where(d => d.Transaccion.id_actor_negocio_interno == idActorNegocioInternoQueTieneLosPrecios && idsTiposTransaccion.Contains(d.Transaccion.id_tipo_transaccion) && d.Transaccion.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                                                                                  .OrderByDescending(dt => dt.Transaccion.fecha_inicio)
                                                                                  .ThenByDescending(dt => dt.id)
                                                                                  .FirstOrDefault().precio_unitario
                                                        });
                return resultado;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener conceptos de negocio por caracteristicas", e);
            }
        }
        public IEnumerable<Concepto_Negocio_Comercial> ObtenerConceptosDeNegocioComercialesPorRolesYIdConceptoBasicoYIdCategoriaInclyendoStockYPrecios(long idTransaccionExistente, int idActorNegocioInternoQueTieneLosPrecios, int[] idsRoles, int idTarifa, int[] idsTiposTransaccion, int idConceptoBasico, int idCategoria)
        {
            try
            {
                var resultado = _db.Concepto_negocio_rol.Where(cnr => idsRoles.Contains(cnr.id_rol))
                                                        .Select(cnr => cnr.Concepto_negocio)
                                                        .Where(cn => cn.Detalle_maestro4.Categoria_concepto.Select(cc => cc.id_categoria).Contains(idCategoria) && cn.es_vigente)
                                                        .Where(cn => cn.id_concepto_basico == idConceptoBasico)
                                 .Select(cn => new Concepto_Negocio_Comercial()
                                 {
                                     Id = cn.id,
                                     Nombre = cn.nombre,
                                     Codigo = cn.codigo,
                                     CodigoBarra = cn.codigo_barra,
                                     NombrePresentacion = cn.Detalle_maestro3.nombre,
                                     Contenido = cn.contenido,
                                     CodigoUnidadMedidaPresentacion = cn.Detalle_maestro1.codigo,
                                     CodigoUnidadMedidaComercial = cn.Detalle_maestro.codigo,
                                     Stock = cn.Detalle_transaccion.Where(dt => dt.id_transaccion == idTransaccionExistente).Sum(dt => dt.cantidad),
                                     PrecioVenta = cn.Precio1.Where(p => p.id_actor_negocio == idActorNegocioInternoQueTieneLosPrecios && p.es_vigente && p.id_tarifa_d == idTarifa)
                                                             .OrderByDescending(p => p.id)
                                                             .FirstOrDefault().valor,
                                     CostoUnitario = cn.Detalle_transaccion.Where(d => d.Transaccion.id_actor_negocio_interno == idActorNegocioInternoQueTieneLosPrecios && idsTiposTransaccion.Contains(d.Transaccion.id_tipo_transaccion) && d.Transaccion.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                                                                           .OrderByDescending(dt => dt.Transaccion.fecha_inicio)
                                                                           .ThenByDescending(dt => dt.id)
                                                                           .FirstOrDefault().precio_unitario
                                 });
                return resultado;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener conceptos de negocio por concepto basico", e);
            }
        }
        public IEnumerable<Concepto_Negocio_Comercial> ObtenerConceptosDeNegocioComercialesPorRolesYIdConceptoBasicoInclyendoStockYPrecios(long idTransaccionExistente, int idActorNegocioInternoQueTieneLosPrecios, int[] idsRoles, int idTarifa, int[] idsTiposTransaccion, int idConceptoBasico)
        {
            try
            {
                var resultado = _db.Concepto_negocio_rol.Where(cnr => idsRoles.Contains(cnr.id_rol))
                                                        .Select(cnr => cnr.Concepto_negocio)
                                                        .Where(cn => cn.es_vigente)
                                                        .Where(cn => cn.id_concepto_basico == idConceptoBasico)
                                 .Select(cn => new Concepto_Negocio_Comercial()
                                 {
                                     Id = cn.id,
                                     Nombre = cn.nombre,
                                     Codigo = cn.codigo,
                                     CodigoBarra = cn.codigo_barra,
                                     NombrePresentacion = cn.Detalle_maestro3.nombre,
                                     Contenido = cn.contenido,
                                     CodigoUnidadMedidaPresentacion = cn.Detalle_maestro1.codigo,
                                     CodigoUnidadMedidaComercial = cn.Detalle_maestro.codigo,
                                     Stock = cn.Detalle_transaccion.Where(dt => dt.id_transaccion == idTransaccionExistente).Sum(dt => dt.cantidad),
                                     PrecioVenta = cn.Precio1.Where(p => p.id_actor_negocio == idActorNegocioInternoQueTieneLosPrecios && p.es_vigente && p.id_tarifa_d == idTarifa)
                                                             .OrderByDescending(p => p.id)
                                                             .FirstOrDefault().valor,
                                     CostoUnitario = cn.Detalle_transaccion.Where(d => d.Transaccion.id_actor_negocio_interno == idActorNegocioInternoQueTieneLosPrecios && idsTiposTransaccion.Contains(d.Transaccion.id_tipo_transaccion) && d.Transaccion.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                                                                           .OrderByDescending(dt => dt.Transaccion.fecha_inicio)
                                                                           .ThenByDescending(dt => dt.id)
                                                                           .FirstOrDefault().precio_unitario
                                 });
                return resultado;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener conceptos de negocio por concepto basico", e);
            }
        }
        public IEnumerable<Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesPorRolesYIdCategoriaInclyendoStockYPrecios(long idTransaccionExistente, int idActorNegocioInternoQueTieneLosPrecios, int[] idsRoles, int idTarifa, int[] idsTiposTransaccion, int idCategoria)
        {
            try
            {
                var resultado = _db.Concepto_negocio_rol.Where(cnr => idsRoles.Contains(cnr.id_rol))
                                                        .Select(cnr => cnr.Concepto_negocio)
                                                        .Where(cn => cn.Detalle_maestro4.Categoria_concepto.Select(cc => cc.id_categoria).Contains(idCategoria) && cn.es_vigente)
                                                        .Select(cn => new Concepto_Negocio_Comercial()
                                                        {
                                                            Id = cn.id,
                                                            Nombre = cn.nombre,
                                                            Codigo = cn.codigo,
                                                            CodigoBarra = cn.codigo_barra,
                                                            NombrePresentacion = cn.Detalle_maestro3.nombre,
                                                            Contenido = cn.contenido,
                                                            CodigoUnidadMedidaPresentacion = cn.Detalle_maestro1.codigo,
                                                            CodigoUnidadMedidaComercial = cn.Detalle_maestro.codigo,
                                                            Stock = cn.Detalle_transaccion.Where(dt => dt.id_transaccion == idTransaccionExistente).Sum(dt => dt.cantidad),
                                                            PrecioVenta = cn.Precio1
                                                                            .Where(p => p.id_actor_negocio == idActorNegocioInternoQueTieneLosPrecios && p.es_vigente && p.id_tarifa_d == idTarifa)
                                                                            .OrderByDescending(p => p.id)
                                                                            .FirstOrDefault().valor,
                                                            CostoUnitario = cn.Detalle_transaccion
                                                                                  .Where(d => d.Transaccion.id_actor_negocio_interno == idActorNegocioInternoQueTieneLosPrecios && idsTiposTransaccion.Contains(d.Transaccion.id_tipo_transaccion) && d.Transaccion.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                                                                                  .OrderByDescending(dt => dt.Transaccion.fecha_inicio)
                                                                                  .ThenByDescending(dt => dt.id)
                                                                                  .FirstOrDefault().precio_unitario
                                                        });
                return resultado;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener conceptos de negocio", e);
            }
        }
        public IEnumerable<Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesPorRolesInclyendoStockYPrecios(long idTransaccionExistente, int idActorNegocioInternoQueTieneLosPrecios, int[] idsRoles, int idTarifa, int[] idsTiposTransaccion)
        {
            try
            {
                var resultado = _db.Concepto_negocio_rol.Where(cnr => idsRoles.Contains(cnr.id_rol))
                                                        .Select(cnr => cnr.Concepto_negocio)
                                                        .Where(cn => cn.es_vigente)
                                                        .Select(cn => new Concepto_Negocio_Comercial()
                                                        {
                                                            Id = cn.id,
                                                            Nombre = cn.nombre,
                                                            Codigo = cn.codigo,
                                                            CodigoBarra = cn.codigo_barra,
                                                            NombrePresentacion = cn.Detalle_maestro3.nombre,
                                                            Contenido = cn.contenido,
                                                            CodigoUnidadMedidaPresentacion = cn.Detalle_maestro1.codigo,
                                                            CodigoUnidadMedidaComercial = cn.Detalle_maestro.codigo,
                                                            Stock = cn.Detalle_transaccion.Where(dt => dt.id_transaccion == idTransaccionExistente).Sum(dt => dt.cantidad),
                                                            PrecioVenta = cn.Precio1
                                                                            .Where(p => p.id_actor_negocio == idActorNegocioInternoQueTieneLosPrecios && p.es_vigente && p.id_tarifa_d == idTarifa)
                                                                            .OrderByDescending(p => p.id)
                                                                            .FirstOrDefault().valor,
                                                            CostoUnitario = cn.Detalle_transaccion
                                                                                  .Where(d => d.Transaccion.id_actor_negocio_interno == idActorNegocioInternoQueTieneLosPrecios && idsTiposTransaccion.Contains(d.Transaccion.id_tipo_transaccion) && d.Transaccion.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                                                                                  .OrderByDescending(dt => dt.Transaccion.fecha_inicio)
                                                                                  .ThenByDescending(dt => dt.id)
                                                                                  .FirstOrDefault().precio_unitario
                                                        });
                return resultado;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener conceptos de negocio", e);
            }
        }
        #endregion

        #region CONCEPTOS NEGOCIOS COMERCIALES PARA OPERACIONES
        public IEnumerable<Selector_Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesPorRol(int idRol)
        {//TODAS LAS OPERACIONES (UN SOLO SELECTOR)
            try
            {
                return _db.Concepto_negocio_rol
                               .Where(cnr => cnr.id_rol == idRol && cnr.Concepto_negocio.es_vigente)
                               .Select(cnr => cnr.Concepto_negocio)
                               .Select(cn => new Selector_Concepto_Negocio_Comercial()
                               {
                                   Id = cn.id,
                                   SoloNombre = cn.nombre,
                                   CodigoBarra = cn.codigo_barra,
                               }).OrderBy(cnc => cnc.SoloNombre);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener el selector de conceptos de negocio por rol", e);
            }
        }

        public IEnumerable<Selector_Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesConStockPrecioPorRol(int idRol, long idTransaccionInventario, int idActorNegocioQueTienePrecios)
        {//TODAS LAS OPERACIONES (UN SOLO SELECTOR)
            try
            {
                return _db.ObtenerConceptosDeNegociosComercialesConStockPrecioPorRol(idRol, idTransaccionInventario, idActorNegocioQueTienePrecios)
                               .Select(cn => new Selector_Concepto_Negocio_Comercial()
                               {
                                   Id = cn.id,
                                   SoloNombre = cn.nombre,
                                   CodigoBarra = cn.codigo_barra,
                                   Precio = cn.precio,
                                   Stock = cn.stock,
                                   EsBien = cn.tipo_familia.Equals(((int)TipoFamiliaEnum.Bien).ToString())
                               }).OrderBy(cnc => cnc.SoloNombre);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener el selector de conceptos de negocio por rol", e);
            }
        }

        public IEnumerable<Selector_Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesPorRolYTipoFamilia(int idRol, string valorTipoFamilia)
        {//TODAS LAS OPERACIONES (UN SOLO SELECTOR)
            try
            {
                return _db.Concepto_negocio_rol
                               .Where(cnr => cnr.id_rol == idRol
                                             && cnr.Concepto_negocio.es_vigente
                                             && cnr.Concepto_negocio.Detalle_maestro4.valor == valorTipoFamilia)
                               .Select(cnr => cnr.Concepto_negocio)
                               .Select(cn => new Selector_Concepto_Negocio_Comercial()
                               {
                                   Id = cn.id,
                                   SoloNombre = cn.nombre,
                                   CodigoBarra = cn.codigo_barra,
                               }).OrderBy(cnc => cnc.SoloNombre);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener el selector de conceptos de negocio por rol", e);
            }
        }
        public IEnumerable<Selector_Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesConStockPrecioPorRolYTipoFamilia(int idRol, long idTransaccionInventario, int idActorNegocioQueTienePrecios, string valorTipoFamilia)
        {//TODAS LAS OPERACIONES (UN SOLO SELECTOR)
            try
            {
                return _db.ObtenerConceptosDeNegociosComercialesConStockPrecioPorRolYTipoFamilia(idRol, idTransaccionInventario, idActorNegocioQueTienePrecios, valorTipoFamilia)
                              .Select(cn => new Selector_Concepto_Negocio_Comercial()
                              {
                                  Id = cn.id,
                                  SoloNombre = cn.nombre,
                                  CodigoBarra = cn.codigo_barra,
                                  Precio = cn.precio,
                                  Stock = cn.stock,
                                  EsBien = cn.tipo_familia.Equals(((int)TipoFamiliaEnum.Bien).ToString())
                              }).OrderBy(cnc => cnc.SoloNombre);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener el selector de conceptos de negocio por rol", e);
            }
        }
        public IEnumerable<Selector_Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesPorRolYBusquedaConcepto(int idRol, string cadenaBusqueda)
        {//TODAS LAS OPERACIONES (UN SOLO SELECTOR SUPER)
            try
            {
                return _db.Concepto_negocio_rol
                               .Where(cnr => cnr.id_rol == idRol
                                             && cnr.Concepto_negocio.es_vigente
                                             && (cnr.Concepto_negocio.codigo_barra.Contains(cadenaBusqueda) || cnr.Concepto_negocio.nombre.Contains(cadenaBusqueda)))
                               .Select(cnr => cnr.Concepto_negocio)
                               .Select(cn => new Selector_Concepto_Negocio_Comercial()
                               {
                                   Id = cn.id,
                                   SoloNombre = cn.nombre,
                                   CodigoBarra = cn.codigo_barra,
                               }).OrderBy(cnc => cnc.SoloNombre);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener el selector de conceptos de negocio por rol, cadena de busqueda", e);
            }
        }
        public IEnumerable<Selector_Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesConStockPrecioPorRolYBusquedaConcepto(int idRol, long idTransaccionInventario, int idActorNegocioQueTienePrecios, string cadenaBusqueda)
        {//TODAS LAS OPERACIONES (UN SOLO SELECTOR SUPER)
            try
            {
                return _db.ObtenerConceptosDeNegociosComercialesConStockPrecioPorRolYBusquedaConcepto(idRol, idTransaccionInventario, idActorNegocioQueTienePrecios, cadenaBusqueda)
                               .Select(cn => new Selector_Concepto_Negocio_Comercial()
                               {
                                   Id = cn.id,
                                   SoloNombre = cn.nombre,
                                   CodigoBarra = cn.codigo_barra,
                                   Precio = cn.precio,
                                   Stock = cn.stock,
                                   EsBien = cn.tipo_familia.Equals(((int)TipoFamiliaEnum.Bien).ToString())
                               }).OrderBy(cnc => cnc.SoloNombre);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener el selector de conceptos de negocio por rol, cadena de busqueda", e);
            }
        }
        public IEnumerable<Selector_Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesPorRolYBusquedaConceptoYTipoFamilia(int idRol, string cadenaBusqueda, string valorTipoFamilia)
        {//TODAS LAS OPERACIONES (UN SOLO SELECTOR SUPER)
            try
            {
                return _db.Concepto_negocio_rol
                               .Where(cnr => cnr.id_rol == idRol
                                             && cnr.Concepto_negocio.es_vigente
                                             && (cnr.Concepto_negocio.codigo_barra.Contains(cadenaBusqueda) || cnr.Concepto_negocio.nombre.Contains(cadenaBusqueda))
                                             && cnr.Concepto_negocio.Detalle_maestro4.valor == valorTipoFamilia)
                               .Select(cnr => cnr.Concepto_negocio)
                               .Select(cn => new Selector_Concepto_Negocio_Comercial()
                               {
                                   Id = cn.id,
                                   SoloNombre = cn.nombre,
                                   CodigoBarra = cn.codigo_barra,
                               }).OrderBy(cnc => cnc.SoloNombre);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener el selector de conceptos de negocio por rol, cadena de busqueda", e);
            }
        }
        public IEnumerable<Selector_Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesConStockPrecioPorRolYBusquedaConceptoYTipoFamilia(int idRol, long idTransaccionInventario, int idActorNegocioQueTienePrecios, string cadenaBusqueda, string valorTipoFamilia)
        {//TODAS LAS OPERACIONES (UN SOLO SELECTOR SUPER)
            try
            {
                return _db.ObtenerConceptosDeNegociosComercialesConStockPrecioPorRolYBusquedaConceptoYTipoFamilia(idRol, idTransaccionInventario, idActorNegocioQueTienePrecios, cadenaBusqueda, valorTipoFamilia)
                              .Select(cn => new Selector_Concepto_Negocio_Comercial()
                              {
                                  Id = cn.id,
                                  SoloNombre = cn.nombre,
                                  CodigoBarra = cn.codigo_barra,
                                  Precio = cn.precio,
                                  Stock = cn.stock,
                                  EsBien = cn.tipo_familia.Equals(((int)TipoFamiliaEnum.Bien).ToString())
                              }).OrderBy(cnc => cnc.SoloNombre);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener el selector de conceptos de negocio por rol, cadena de busqueda", e);
            }
        }
        public IEnumerable<Selector_Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesPorRolYFamilia(int idRol, int idFamilia)
        {//TODAS LAS OPERACIONES (DOBLE SELECTOR)
            try
            {
                return _db.Concepto_negocio_rol
                                            .Where(cnr => cnr.id_rol == idRol
                                            && cnr.Concepto_negocio.id_concepto_basico == idFamilia
                                            && cnr.Concepto_negocio.es_vigente)
                                            .Select(cnr => cnr.Concepto_negocio)
                                            .Select(cn => new Selector_Concepto_Negocio_Comercial()
                                            {
                                                Id = cn.id,
                                                SoloNombre = cn.nombre,
                                                CodigoBarra = cn.codigo_barra,
                                            });
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener el selector de conceptos de negocios por rol y familia", e);
            }
        }

        public IEnumerable<Selector_Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesConStockPrecioPorRolYFamilia(int idRol, long idTransaccionInventario, int idActorNegocioQueTienePrecios, int idFamilia)
        {//TODAS LAS OPERACIONES (DOBLE SELECTOR)
            try
            {
                return _db.ObtenerConceptosDeNegociosComercialesConStockPrecioPorRolYFamilia(idRol, idTransaccionInventario, idActorNegocioQueTienePrecios, idFamilia)
                                .Select(cn => new Selector_Concepto_Negocio_Comercial()
                                {
                                    Id = cn.id,
                                    SoloNombre = cn.nombre,
                                    CodigoBarra = cn.codigo_barra,
                                    Precio = cn.precio,
                                    Stock = cn.stock,
                                    EsBien = cn.tipo_familia.Equals(((int)TipoFamiliaEnum.Bien).ToString())
                                }).OrderBy(cnc => cnc.SoloNombre);

            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener el selector de conceptos de negocios por rol y familia", e);
            }
        }
        public IEnumerable<Concepto_Negocio_Comercial_> ObtenerConceptosDeNegociosComercialesPorRolesYCodigoBarra(int idRol, string codigoBarra)
        {//COMPRAS GUIAS Y TRASLADO SIN STOCK CONTROLADO
            try
            {
                return _db.Concepto_negocio_rol
                                            .Where(cnr => cnr.id_rol == idRol
                                            && cnr.Concepto_negocio.codigo_barra == codigoBarra
                                            && cnr.Concepto_negocio.es_vigente)
                                            .Select(cnr => cnr.Concepto_negocio)
                                            .Select(cn => new Concepto_Negocio_Comercial_()
                                            {
                                                Id = cn.id,
                                                IdFamilia = cn.id_concepto_basico,
                                                CodigoBarra = cn.codigo_barra,
                                                Nombre = cn.nombre,
                                                ValorConceptoBasico = cn.Detalle_maestro4.valor,
                                            });
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener conceptos de negocio por codigo de barra", e);
            }
        }
        public IEnumerable<Concepto_Negocio_Comercial_> ObtenerConceptosDeNegociosComercialesPorRolesYCodigoBarra(int idRol, string codigoBarra, string valorTipoFamilia)
        {//COMPRAS GUIAS Y TRASLADO SIN STOCK CONTROLADO
            try
            {
                return _db.Concepto_negocio_rol
                                            .Where(cnr => cnr.id_rol == idRol
                                            && cnr.Concepto_negocio.codigo_barra == codigoBarra
                                            && cnr.Concepto_negocio.es_vigente
                                            && cnr.Concepto_negocio.Detalle_maestro4.valor == valorTipoFamilia)
                                            .Select(cnr => cnr.Concepto_negocio)
                                            .Select(cn => new Concepto_Negocio_Comercial_()
                                            {
                                                Id = cn.id,
                                                IdFamilia = cn.id_concepto_basico,
                                                CodigoBarra = cn.codigo_barra,
                                                Nombre = cn.nombre,
                                                ValorConceptoBasico = cn.Detalle_maestro4.valor,
                                            });
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener conceptos de negocio por codigo de barra", e);
            }
        }
        public IEnumerable<Concepto_Negocio_Comercial_> ObtenerConceptosDeNegociosComercialesConStockPorRolesYCodigoBarra(long idTransaccionInventario, int idRol, string codigoBarra)
        {//GUIAS CON STOCK CONTROLADO
            try
            {
                _db.Configuration.LazyLoadingEnabled = false;
                return _db.Concepto_negocio_rol
                               .Where(cnr => cnr.id_rol == idRol
                               && cnr.Concepto_negocio.codigo_barra == codigoBarra
                               && cnr.Concepto_negocio.es_vigente)
                               .Select(cnr => cnr.Concepto_negocio)
                               .Select(cn => new Concepto_Negocio_Comercial_()
                               {
                                   Id = cn.id,
                                   IdFamilia = cn.id_concepto_basico,
                                   Nombre = cn.nombre,
                                   CodigoBarra = cn.codigo_barra,
                                   ValorConceptoBasico = cn.Detalle_maestro4.valor,
                                   Stock = cn.Detalle_transaccion.Where(dt => dt.id_transaccion == idTransaccionInventario && dt.Concepto_negocio.codigo_barra == codigoBarra).FirstOrDefault().cantidad
                               });
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener conceptos de negocio por codigo de barra", e);
            }
            finally
            {
                _db.Configuration.LazyLoadingEnabled = true;
            }
        }
        public IEnumerable<Concepto_Negocio_Comercial_> ObtenerConceptosDeNegociosComercialesConStockPorRolesYCodigoBarra(long idTransaccionInventario, int idRol, string codigoBarra, string valorTipoFamilia)
        {//GUIAS CON STOCK CONTROLADO
            try
            {
                _db.Configuration.LazyLoadingEnabled = false;
                return _db.Concepto_negocio_rol
                               .Where(cnr => cnr.id_rol == idRol
                               && cnr.Concepto_negocio.codigo_barra == codigoBarra
                               && cnr.Concepto_negocio.es_vigente
                               && cnr.Concepto_negocio.Detalle_maestro4.valor == valorTipoFamilia)
                               .Select(cnr => cnr.Concepto_negocio)
                               .Select(cn => new Concepto_Negocio_Comercial_()
                               {
                                   Id = cn.id,
                                   IdFamilia = cn.id_concepto_basico,
                                   Nombre = cn.nombre,
                                   CodigoBarra = cn.codigo_barra,
                                   ValorConceptoBasico = cn.Detalle_maestro4.valor,
                                   Stock = cn.Detalle_transaccion.Where(dt => dt.id_transaccion == idTransaccionInventario && dt.Concepto_negocio.codigo_barra == codigoBarra).FirstOrDefault().cantidad
                               });
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener conceptos de negocio por codigo de barra", e);
            }
            finally
            {
                _db.Configuration.LazyLoadingEnabled = true;
            }
        }
        public IEnumerable<Concepto_Negocio_Comercial_> ObtenerConceptosDeNegociosComercialesConPrecioPorRolesYCodigoBarra(int idActorNegocioQueTienePrecios, int idRol, string codigoBarra)
        {//PRECIOS, VENTAS Y COTIZACIONES SIN STOCK CONTROLADO
            try
            {
                _db.Configuration.LazyLoadingEnabled = false;
                return _db.Concepto_negocio_rol
                               .Where(cnr => cnr.id_rol == idRol
                               && cnr.Concepto_negocio.codigo_barra == codigoBarra
                               && cnr.Concepto_negocio.es_vigente)
                               .Select(cnr => cnr.Concepto_negocio)
                               .Select(cn => new Concepto_Negocio_Comercial_()
                               {
                                   Id = cn.id,
                                   IdFamilia = cn.id_concepto_basico,
                                   Nombre = cn.nombre,
                                   CodigoBarra = cn.codigo_barra,
                                   ValorConceptoBasico = cn.Detalle_maestro4.valor,
                                   Precios = cn.Precio1.Where(p => p.id_actor_negocio == idActorNegocioQueTienePrecios && p.es_vigente)
                                                        .Select(p => new Precio_Concepto_Negocio_Comercial()
                                                        {
                                                            Id = p.id,
                                                            IdTarifa = p.id_tarifa_d,
                                                            Tarifa = p.Detalle_maestro3.nombre,
                                                            Valor = p.valor,
                                                            Codigo = p.Detalle_maestro3.codigo
                                                        }),
                               });
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener conceptos de negocio por codigo de barra", e);
            }
            finally
            {
                _db.Configuration.LazyLoadingEnabled = true;
            }
        }
        public IEnumerable<Concepto_Negocio_Comercial_> ObtenerConceptosDeNegociosComercialesConPrecioPorRolesYCodigoBarra(int idActorNegocioQueTienePrecios, int idRol, string codigoBarra, string valorTipoFamilia)
        {//PRECIOS, VENTAS Y COTIZACIONES SIN STOCK CONTROLADO
            try
            {
                _db.Configuration.LazyLoadingEnabled = false;
                return _db.Concepto_negocio_rol
                               .Where(cnr => cnr.id_rol == idRol
                               && cnr.Concepto_negocio.codigo_barra == codigoBarra
                               && cnr.Concepto_negocio.es_vigente
                               && cnr.Concepto_negocio.Detalle_maestro4.valor == valorTipoFamilia)
                               .Select(cnr => cnr.Concepto_negocio)
                               .Select(cn => new Concepto_Negocio_Comercial_()
                               {
                                   Id = cn.id,
                                   IdFamilia = cn.id_concepto_basico,
                                   Nombre = cn.nombre,
                                   CodigoBarra = cn.codigo_barra,
                                   ValorConceptoBasico = cn.Detalle_maestro4.valor,
                                   Precios = cn.Precio1.Where(p => p.id_actor_negocio == idActorNegocioQueTienePrecios && p.es_vigente)
                                                        .Select(p => new Precio_Concepto_Negocio_Comercial()
                                                        {
                                                            Id = p.id,
                                                            IdTarifa = p.id_tarifa_d,
                                                            Tarifa = p.Detalle_maestro3.nombre,
                                                            Valor = p.valor,
                                                            Codigo = p.Detalle_maestro3.codigo
                                                        }),
                               });
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener conceptos de negocio por codigo de barra", e);
            }
            finally
            {
                _db.Configuration.LazyLoadingEnabled = true;
            }
        }
        public IEnumerable<Concepto_Negocio_Comercial_> ObtenerConceptosDeNegociosComercialesConStockPrecioPorRolesYCodigoBarra(long idTransaccionInventario, int idActorNegocioQueTienePrecios, int idRol, string codigoBarra)//VENTAS Y COTIZACIONES CON STOCK CONTROLADO
        {
            try
            {
                _db.Configuration.LazyLoadingEnabled = false;
                return _db.Concepto_negocio_rol
                               .Where(cnr => cnr.id_rol == idRol
                               && cnr.Concepto_negocio.codigo_barra == codigoBarra
                               && cnr.Concepto_negocio.es_vigente)
                               .Select(cnr => cnr.Concepto_negocio)
                               .Select(cn => new Concepto_Negocio_Comercial_()
                               {
                                   Id = cn.id,
                                   IdFamilia = cn.id_concepto_basico,
                                   Nombre = cn.nombre,
                                   CodigoBarra = cn.codigo_barra,
                                   ValorConceptoBasico = cn.Detalle_maestro4.valor,
                                   Stock = cn.Detalle_transaccion.Where(dt => dt.id_transaccion == idTransaccionInventario && dt.Concepto_negocio.codigo_barra == codigoBarra).FirstOrDefault().cantidad,
                                   Precios = cn.Precio1.Where(p => p.id_actor_negocio == idActorNegocioQueTienePrecios && p.es_vigente)
                                                        .Select(p => new Precio_Concepto_Negocio_Comercial()
                                                        {
                                                            Id = p.id,
                                                            IdTarifa = p.id_tarifa_d,
                                                            Tarifa = p.Detalle_maestro3.nombre,
                                                            Valor = p.valor,
                                                            Codigo = p.Detalle_maestro3.codigo
                                                        }),
                               });
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener conceptos de negocio por codigo de barra", e);
            }
            finally
            {
                _db.Configuration.LazyLoadingEnabled = true;
            }
        }
        public IEnumerable<Concepto_Negocio_Comercial_> ObtenerConceptosDeNegociosComercialesConStockPrecioPorRolesYCodigoBarra(long idTransaccionInventario, int idActorNegocioQueTienePrecios, int idRol, string codigoBarra, string valorTipoFamilia)//VENTAS Y COTIZACIONES CON STOCK CONTROLADO
        {
            try
            {
                _db.Configuration.LazyLoadingEnabled = false;
                return _db.Concepto_negocio_rol
                               .Where(cnr => cnr.id_rol == idRol
                               && cnr.Concepto_negocio.codigo_barra == codigoBarra
                               && cnr.Concepto_negocio.es_vigente
                               && cnr.Concepto_negocio.Detalle_maestro4.valor == valorTipoFamilia)
                               .Select(cnr => cnr.Concepto_negocio)
                               .Select(cn => new Concepto_Negocio_Comercial_()
                               {
                                   Id = cn.id,
                                   IdFamilia = cn.id_concepto_basico,
                                   Nombre = cn.nombre,
                                   CodigoBarra = cn.codigo_barra,
                                   ValorConceptoBasico = cn.Detalle_maestro4.valor,
                                   Stock = cn.Detalle_transaccion.Where(dt => dt.id_transaccion == idTransaccionInventario && dt.Concepto_negocio.codigo_barra == codigoBarra).FirstOrDefault().cantidad,
                                   Precios = cn.Precio1.Where(p => p.id_actor_negocio == idActorNegocioQueTienePrecios && p.es_vigente)
                                                        .Select(p => new Precio_Concepto_Negocio_Comercial()
                                                        {
                                                            Id = p.id,
                                                            IdTarifa = p.id_tarifa_d,
                                                            Tarifa = p.Detalle_maestro3.nombre,
                                                            Valor = p.valor,
                                                            Codigo = p.Detalle_maestro3.codigo
                                                        }),
                               });
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener conceptos de negocio por codigo de barra", e);
            }
            finally
            {
                _db.Configuration.LazyLoadingEnabled = true;
            }
        }
        public Concepto_Negocio_Comercial_ ObtenerConceptoDeNegocioComercialPorRolesEIdConcepto(int idRol, int idConceptoNegocio)
        {//COMPRAS GUIAS Y TRASLADO SIN STOCK CONTROLADO
            try
            {
                return _db.Concepto_negocio_rol
                                            .Where(cnr => cnr.id_rol == idRol
                                            && cnr.Concepto_negocio.id == idConceptoNegocio
                                            && cnr.Concepto_negocio.es_vigente)
                                            .Select(cnr => cnr.Concepto_negocio)
                                            .Select(cn => new Concepto_Negocio_Comercial_()
                                            {
                                                Id = cn.id,
                                                IdFamilia = cn.id_concepto_basico,
                                                CodigoBarra = cn.codigo_barra,
                                                Nombre = cn.nombre,
                                                ValorConceptoBasico = cn.Detalle_maestro4.valor,
                                            }).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener conceptos de negocio por rol e id concepto", e);
            }
        }
        public Concepto_Negocio_Comercial_ ObtenerConceptoDeNegocioComercialConStockPorRolesEIdConcepto(long idTransaccionInventario, int idRol, int idConceptoNegocio)
        {//GUIAS CON STOCK CONTROLADO
            try
            {
                _db.Configuration.LazyLoadingEnabled = false;
                return _db.Concepto_negocio_rol
                               .Where(cnr => cnr.id_rol == idRol
                               && cnr.Concepto_negocio.id == idConceptoNegocio
                               && cnr.Concepto_negocio.es_vigente)
                               .Select(cnr => cnr.Concepto_negocio)
                               .Select(cn => new Concepto_Negocio_Comercial_()
                               {
                                   Id = cn.id,
                                   IdFamilia = cn.id_concepto_basico,
                                   Nombre = cn.nombre,
                                   CodigoBarra = cn.codigo_barra,
                                   ValorConceptoBasico = cn.Detalle_maestro4.valor,
                                   Stock = cn.Detalle_transaccion.Where(dt => dt.id_transaccion == idTransaccionInventario && dt.Concepto_negocio.id == idConceptoNegocio).FirstOrDefault().cantidad
                               }).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener conceptos de negocio por rol e id concepto", e);
            }
            finally
            {
                _db.Configuration.LazyLoadingEnabled = true;
            }
        }
        public Concepto_Negocio_Comercial_ ObtenerConceptoDeNegocioComercialConPrecioPorRolesEIdConcepto(int idActorNegocioQueTienePrecios, int idRol, int idConceptoNegocio)
        {//PRECIOS, VENTAS Y COTIZACIONES SIN STOCK CONTROLADO
            try
            {
                _db.Configuration.LazyLoadingEnabled = false;
                return _db.Concepto_negocio_rol
                               .Where(cnr => cnr.id_rol == idRol
                               && cnr.Concepto_negocio.id == idConceptoNegocio
                               && cnr.Concepto_negocio.es_vigente)
                               .Select(cnr => cnr.Concepto_negocio)
                               .Select(cn => new Concepto_Negocio_Comercial_()
                               {
                                   Id = cn.id,
                                   IdFamilia = cn.id_concepto_basico,
                                   Nombre = cn.nombre,
                                   CodigoBarra = cn.codigo_barra,
                                   ValorConceptoBasico = cn.Detalle_maestro4.valor,
                                   Precios = cn.Precio1.Where(p => p.id_actor_negocio == idActorNegocioQueTienePrecios && p.es_vigente)
                                                        .Select(p => new Precio_Concepto_Negocio_Comercial()
                                                        {
                                                            Id = p.id,
                                                            IdTarifa = p.id_tarifa_d,
                                                            Tarifa = p.Detalle_maestro3.nombre,
                                                            Valor = p.valor,
                                                            Codigo = p.Detalle_maestro3.codigo
                                                        }),
                               }).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener conceptos de negocio por rol e id concepto", e);
            }
            finally
            {
                _db.Configuration.LazyLoadingEnabled = true;
            }
        }
        public Concepto_Negocio_Comercial_ ObtenerConceptoDeNegocioComercialConStockPrecioPorRolesEIdConcepto(long idTransaccionInventario, int idActorNegocioQueTienePrecios, int idRol, int idConceptoNegocio)//VENTAS Y COTIZACIONES CON STOCK CONTROLADO
        {
            try
            {
                _db.Configuration.LazyLoadingEnabled = false;
                return _db.Concepto_negocio_rol
                               .Where(cnr => cnr.id_rol == idRol
                               && cnr.Concepto_negocio.id == idConceptoNegocio
                               && cnr.Concepto_negocio.es_vigente)
                               .Select(cnr => cnr.Concepto_negocio)
                               .Select(cn => new Concepto_Negocio_Comercial_()
                               {
                                   Id = cn.id,
                                   IdFamilia = cn.id_concepto_basico,
                                   Nombre = cn.nombre,
                                   CodigoBarra = cn.codigo_barra,
                                   ValorConceptoBasico = cn.Detalle_maestro4.valor,
                                   Stock = cn.Detalle_transaccion.Where(dt => dt.id_transaccion == idTransaccionInventario && dt.Concepto_negocio.id == idConceptoNegocio).FirstOrDefault().cantidad,
                                   Precios = cn.Precio1.Where(p => p.id_actor_negocio == idActorNegocioQueTienePrecios && p.es_vigente)
                                                        .Select(p => new Precio_Concepto_Negocio_Comercial()
                                                        {
                                                            Id = p.id,
                                                            IdTarifa = p.id_tarifa_d,
                                                            Tarifa = p.Detalle_maestro3.nombre,
                                                            Valor = p.valor,
                                                            Codigo = p.Detalle_maestro3.codigo
                                                        }),
                               }).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener conceptos de negocio por rol e id concepto", e);
            }
            finally
            {
                _db.Configuration.LazyLoadingEnabled = true;
            }
        }


























        //public IEnumerable<Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComerciales(int[] idRoles, int idFamilia)
        //{
        //    try
        //    {
        //        return _db.Concepto_negocio_rol
        //                                    .Where(cnr => idRoles.Contains(cnr.id_rol) && cnr.Concepto_negocio.id_concepto_basico == idFamilia && cnr.Concepto_negocio.es_vigente)
        //                                    .Select(cnr => cnr.Concepto_negocio)
        //                                    .Select(cn => new Concepto_Negocio_Comercial()
        //                                    {
        //                                        Id = cn.id,
        //                                        Nombre = cn.nombre,
        //                                        CodigoBarra = cn.codigo_barra,
        //                                        Sufijo = cn.sufijo,
        //                                        NombreConceptoBasico = cn.Detalle_maestro4.nombre,
        //                                        ValorConceptoBasico = cn.Detalle_maestro4.valor,
        //                                        ValoresCaracteristicas = cn.Valor_caracteristica_concepto_negocio
        //                                                       .Select(vccn => vccn.Valor_caracteristica)
        //                                                       .Select(vc => new ValorCaracteristica()
        //                                                       {
        //                                                           Id = vc.id,
        //                                                           IdCaracteristica = vc.id_caracteristica,
        //                                                           Valor = vc.valor
        //                                                       }).OrderBy(c => c.IdCaracteristica),
        //                                        NombrePresentacion = cn.Detalle_maestro3.nombre,
        //                                        Contenido = cn.contenido,
        //                                        UnidadMedidaPresentacion = cn.Detalle_maestro1.valor,
        //                                        UnidadMedidaComercial = cn.Detalle_maestro.valor,
        //                                        UnidadMedidaReferencial = cn.Detalle_maestro2.valor,
        //                                    });
        //    }
        //    catch (Exception e)
        //    {
        //        throw new DatosException("Error al obtener conceptos de negocios  por rol y idconcepto_basico", e);
        //    }
        //}



        //public IEnumerable<Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesIncluyendoPrecios(int idActorNegocioInternoQueTieneLosPrecios, int[] idsRoles, int idConceptoBasico)
        //{
        //    try
        //    {
        //        return _db.Concepto_negocio_rol
        //                       .Where(cnr => idsRoles.Contains(cnr.id_rol)
        //                                     && cnr.Concepto_negocio.id_concepto_basico == idConceptoBasico
        //                                     && cnr.Concepto_negocio.es_vigente)
        //                       .Select(cnr => cnr.Concepto_negocio)
        //                       .Select(cn => new Concepto_Negocio_Comercial()
        //                       {
        //                           Id = cn.id,
        //                           Nombre = cn.nombre,
        //                           CodigoBarra = cn.codigo_barra,
        //                           Sufijo = cn.sufijo,
        //                           IdConceptoBasico = cn.Detalle_maestro4.id,
        //                           NombreConceptoBasico = cn.Detalle_maestro4.nombre,
        //                           ValorConceptoBasico = cn.Detalle_maestro4.valor,
        //                           Precios = cn.Precio1.Where(p => p.id_actor_negocio == idActorNegocioInternoQueTieneLosPrecios && p.es_vigente)
        //                                                .Select(p => new Precio_Concepto_Negocio_Comercial()
        //                                                {
        //                                                    Id = p.id,
        //                                                    IdTarifa = p.id_tarifa_d,
        //                                                    Tarifa = p.Detalle_maestro3.nombre,
        //                                                    Valor = p.valor,
        //                                                    Codigo = p.Detalle_maestro3.codigo
        //                                                }),
        //                       });
        //    }
        //    catch (Exception e)
        //    {
        //        throw new DatosException("Error al obtener conceptos de negocio por rol y concepto basico", e);
        //    }
        //}

        //public IEnumerable<Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesIncluyendoPrecios(int idActorNegocioInternoQueTieneLosPrecios, int[] idsRoles)
        //{
        //    try
        //    {
        //        return _db.Concepto_negocio_rol
        //                       .Where(cnr => idsRoles.Contains(cnr.id_rol)
        //                                     && cnr.Concepto_negocio.es_vigente)
        //                       .Select(cnr => cnr.Concepto_negocio)
        //                       .Select(cn => new Concepto_Negocio_Comercial()
        //                       {
        //                           Id = cn.id,
        //                           Nombre = cn.nombre,
        //                           CodigoBarra = cn.codigo_barra,
        //                           Sufijo = cn.sufijo,
        //                           IdConceptoBasico = cn.Detalle_maestro4.id,
        //                           NombreConceptoBasico = cn.Detalle_maestro4.nombre,
        //                           ValorConceptoBasico = cn.Detalle_maestro4.valor,
        //                           Precios = cn.Precio1.Where(p => p.id_actor_negocio == idActorNegocioInternoQueTieneLosPrecios && p.es_vigente)
        //                                                .Select(p => new Precio_Concepto_Negocio_Comercial()
        //                                                {
        //                                                    Id = p.id,
        //                                                    IdTarifa = p.id_tarifa_d,
        //                                                    Tarifa = p.Detalle_maestro3.nombre,
        //                                                    Valor = p.valor,
        //                                                    Codigo = p.Detalle_maestro3.codigo
        //                                                }),
        //                       }).OrderBy(cnc => cnc.Nombre);
        //    }
        //    catch (Exception e)
        //    {
        //        throw new DatosException("Error al obtener conceptos de negocio por rol y concepto basico", e);
        //    }
        //}



        //public Concepto_Negocio_Comercial ObtenerConceptosDeNegociosComercialesPorCodigoBarraIncluyendoPrecios(int idActorNegocioInternoQueTieneLosPrecios, int[] idsRoles, string codigoBarra)
        //{
        //    try
        //    {
        //        _db.Configuration.LazyLoadingEnabled = false;
        //        return _db.Concepto_negocio_rol
        //                       .Where(cnr => idsRoles.Contains(cnr.id_rol)
        //                                     && cnr.Concepto_negocio.codigo_barra == codigoBarra
        //                                     && cnr.Concepto_negocio.es_vigente)
        //                        .Select(cnr => cnr.Concepto_negocio)
        //                       .Select(cn => new Concepto_Negocio_Comercial()
        //                       {
        //                           Id = cn.id,
        //                           Nombre = cn.nombre,
        //                           CodigoBarra = cn.codigo_barra,
        //                           Sufijo = cn.sufijo,
        //                           NombreConceptoBasico = cn.Detalle_maestro4.nombre,
        //                           ValorConceptoBasico = cn.Detalle_maestro4.valor,
        //                           Precios = cn.Precio1.Where(p => p.id_actor_negocio == idActorNegocioInternoQueTieneLosPrecios && p.es_vigente)
        //                                                .Select(p => new Precio_Concepto_Negocio_Comercial()
        //                                                {
        //                                                    Id = p.id,
        //                                                    IdTarifa = p.id_tarifa_d,
        //                                                    Tarifa = p.Detalle_maestro3.nombre,
        //                                                    Valor = p.valor,
        //                                                    Codigo = p.Detalle_maestro3.codigo
        //                                                })
        //                       }).SingleOrDefault();
        //    }
        //    catch (Exception e)
        //    {
        //        throw new DatosException("Error al obtener conceptos de negocio por rol y concepto basico", e);
        //    }
        //    finally
        //    {
        //        _db.Configuration.LazyLoadingEnabled = true;
        //    }
        //}

        //public Concepto_Negocio_Comercial ObtenerConceptosDeNegociosComercialesPorIdIncluyendoPrecios(int idActorNegocioInternoQueTieneLosPrecios, int[] idsRoles, int id)
        //{
        //    try
        //    {
        //        _db.Configuration.LazyLoadingEnabled = false;
        //        return _db.Concepto_negocio_rol
        //                       .Where(cnr => idsRoles.Contains(cnr.id_rol)
        //                                     && cnr.Concepto_negocio.id == id
        //                                     && cnr.Concepto_negocio.es_vigente)
        //                        .Select(cnr => cnr.Concepto_negocio)
        //                       .Select(cn => new Concepto_Negocio_Comercial()
        //                       {
        //                           Id = cn.id,
        //                           Nombre = cn.nombre,
        //                           CodigoBarra = cn.codigo_barra,
        //                           Sufijo = cn.sufijo,
        //                           NombreConceptoBasico = cn.Detalle_maestro4.nombre,
        //                           ValorConceptoBasico = cn.Detalle_maestro4.valor,
        //                           Precios = cn.Precio1.Where(p => p.id_actor_negocio == idActorNegocioInternoQueTieneLosPrecios && p.es_vigente)
        //                                                .Select(p => new Precio_Concepto_Negocio_Comercial()
        //                                                {
        //                                                    Id = p.id,
        //                                                    IdTarifa = p.id_tarifa_d,
        //                                                    Tarifa = p.Detalle_maestro3.nombre,
        //                                                    Valor = p.valor,
        //                                                    Codigo = p.Detalle_maestro3.codigo
        //                                                })
        //                       }).SingleOrDefault();
        //    }
        //    catch (Exception e)
        //    {
        //        throw new DatosException("Error al obtener conceptos de negocio por rol y concepto basico", e);
        //    }
        //    finally
        //    {
        //        _db.Configuration.LazyLoadingEnabled = true;
        //    }
        //}
        //public Concepto_Negocio_Comercial ObtenerConceptosDeNegociosComercialesPorCodigoBarraIncluyendoPrecioInclyendoComplementos(long idTransaccionExistente, int idActorNegocioInternoQueTieneLosPrecios, int[] idsRoles, string codigoBarra)
        //{
        //    try
        //    {
        //        _db.Configuration.LazyLoadingEnabled = false;
        //        return _db.Concepto_negocio_rol
        //                       .Where(cnr => idsRoles.Contains(cnr.id_rol)
        //                                     && cnr.Concepto_negocio.codigo_barra == codigoBarra
        //                                     && cnr.Concepto_negocio.es_vigente)
        //                        .Select(cnr => cnr.Concepto_negocio)
        //                       .Select(cn => new Concepto_Negocio_Comercial()
        //                       {
        //                           Id = cn.id,
        //                           Nombre = cn.nombre,
        //                           CodigoBarra = cn.codigo_barra,
        //                           ValorConceptoBasico = cn.Detalle_maestro4.valor,
        //                           Complementos = cn.Detalle_transaccion.Where(dt => dt.id_transaccion == idTransaccionExistente && dt.Concepto_negocio.codigo_barra == codigoBarra)
        //                          .Select(dt => new Complemento_Concepto_Negocio_Comercial()
        //                          {
        //                              Stock = dt.cantidad,
        //                              Precios = cn.Precio1.Where(p => p.id_actor_negocio == idActorNegocioInternoQueTieneLosPrecios && p.es_vigente)
        //                                                .Select(p => new Precio_Concepto_Negocio_Comercial()
        //                                                {
        //                                                    Id = p.id,
        //                                                    IdTarifa = p.id_tarifa_d,
        //                                                    Tarifa = p.Detalle_maestro3.nombre,
        //                                                    Valor = p.valor,
        //                                                    Codigo = p.Detalle_maestro3.codigo
        //                                                }),
        //                          })
        //                       }).SingleOrDefault();
        //    }
        //    catch (Exception e)
        //    {
        //        throw new DatosException("Error al obtener conceptos de negocio por rol y concepto basico", e);
        //    }
        //    finally
        //    {
        //        _db.Configuration.LazyLoadingEnabled = true;
        //    }
        //}
        //public Concepto_Negocio_Comercial ObtenerConceptosDeNegociosComercialesPorCodigoBarraIncluyendoPrecios(int idActorNegocioInternoQueTieneLosPrecios, int[] idsRoles, int idConceptoNegocio)
        //{
        //    try
        //    {
        //        _db.Configuration.LazyLoadingEnabled = false;
        //        return _db.Concepto_negocio_rol
        //                       .Where(cnr => idsRoles.Contains(cnr.id_rol)
        //                                     && cnr.Concepto_negocio.id == idConceptoNegocio
        //                                     && cnr.Concepto_negocio.es_vigente)
        //                        .Select(cnr => cnr.Concepto_negocio)
        //                       .Select(cn => new Concepto_Negocio_Comercial()
        //                       {
        //                           Id = cn.id,
        //                           Nombre = cn.nombre,
        //                           CodigoBarra = cn.codigo_barra,
        //                           Sufijo = cn.sufijo,
        //                           NombreConceptoBasico = cn.Detalle_maestro4.nombre,
        //                           ValorConceptoBasico = cn.Detalle_maestro4.valor,
        //                           Precios = cn.Precio1.Where(p => p.id_actor_negocio == idActorNegocioInternoQueTieneLosPrecios && p.es_vigente)
        //                                                .Select(p => new Precio_Concepto_Negocio_Comercial()
        //                                                {
        //                                                    Id = p.id,
        //                                                    IdTarifa = p.id_tarifa_d,
        //                                                    Tarifa = p.Detalle_maestro3.nombre,
        //                                                    Valor = p.valor,
        //                                                    Codigo = p.Detalle_maestro3.codigo
        //                                                })
        //                       }).SingleOrDefault();
        //    }
        //    catch (Exception e)
        //    {
        //        throw new DatosException("Error al obtener conceptos de negocio por rol y concepto basico", e);
        //    }
        //    finally
        //    {
        //        _db.Configuration.LazyLoadingEnabled = true;
        //    }
        //}

        //public Concepto_Negocio_Comercial ObtenerConceptosDeNegociosComercialesPorIdConceptoNegocioIncluyendoPrecioInclyendoComplementos(long idTransaccionExistente, int idActorNegocioInternoQueTieneLosPrecios, int[] idsRoles, int idConceptoNegocio)
        //{
        //    try
        //    {
        //        _db.Configuration.LazyLoadingEnabled = false;
        //        return _db.Concepto_negocio_rol
        //                       .Where(cnr => idsRoles.Contains(cnr.id_rol)
        //                                     && cnr.Concepto_negocio.id == idConceptoNegocio
        //                                     && cnr.Concepto_negocio.es_vigente)
        //                        .Select(cnr => cnr.Concepto_negocio)
        //                       .Select(cn => new Concepto_Negocio_Comercial_()
        //                       {
        //                           Id = cn.id,
        //                           Nombre = cn.nombre,
        //                           CodigoBarra = cn.codigo_barra,
        //                           ValorConceptoBasico = cn.Detalle_maestro4.valor,
        //                           Complementos = cn.Detalle_transaccion.Where(dt => dt.id_transaccion == idTransaccionExistente && dt.Concepto_negocio.id == idConceptoNegocio)
        //                          .Select(dt => new Complemento_Concepto_Negocio_Comercial()
        //                          {
        //                              Stock = dt.cantidad,
        //                              Precios = cn.Precio1.Where(p => p.id_actor_negocio == idActorNegocioInternoQueTieneLosPrecios && p.es_vigente)
        //                                                .Select(p => new Precio_Concepto_Negocio_Comercial()
        //                                                {
        //                                                    Id = p.id,
        //                                                    IdTarifa = p.id_tarifa_d,
        //                                                    Tarifa = p.Detalle_maestro3.nombre,
        //                                                    Valor = p.valor,
        //                                                    Codigo = p.Detalle_maestro3.codigo
        //                                                }),
        //                          })
        //                       }).SingleOrDefault();
        //    }
        //    catch (Exception e)
        //    {
        //        throw new DatosException("Error al obtener conceptos de negocio por id concepto negocio", e);
        //    }
        //    finally
        //    {
        //        _db.Configuration.LazyLoadingEnabled = true;
        //    }
        //}

        //public IEnumerable<Complemento_Concepto_Negocio_Comercial> ObtenerComplementoConceptoDeNegocioComercial(long idTransaccionExistente, int idActorNegocioInternoQueTieneLosPrecios, int idConceptoNegocio)
        //{
        //    try
        //    {
        //        _db.Configuration.LazyLoadingEnabled = false;
        //        return _db.Detalle_transaccion.Where(dt => dt.id_transaccion == idTransaccionExistente && dt.id_concepto_negocio == idConceptoNegocio)
        //            .Select(dt => new Complemento_Concepto_Negocio_Comercial()
        //            {
        //                Stock = dt.cantidad,
        //                Precios = dt.Concepto_negocio.Precio1.Where(p => p.id_actor_negocio == idActorNegocioInternoQueTieneLosPrecios && p.es_vigente)
        //                                                .Select(p => new Precio_Concepto_Negocio_Comercial()
        //                                                {
        //                                                    Id = p.id,
        //                                                    IdTarifa = p.id_tarifa_d,
        //                                                    Tarifa = p.Detalle_maestro3.nombre,
        //                                                    Valor = p.valor,
        //                                                    Codigo = p.Detalle_maestro3.codigo
        //                                                }),
        //            }).ToList();
        //    }
        //    catch (Exception e)
        //    {
        //        throw new DatosException("Error al obtener complementos concepto de negocio comercial", e);
        //    }
        //    finally
        //    {
        //        _db.Configuration.LazyLoadingEnabled = true;
        //    }
        //}

        //public IEnumerable<Concepto_Negocio_Comercial_Venta> ObtenerConceptosDeNegociosComercialesParaVenta(int idActorNegocio, int[] idsRoles, int idConceptoBasico)
        //{
        //    try
        //    {
        //        return _db.Concepto_negocio_rol
        //                       .Where(cnr => idsRoles.Contains(cnr.id_rol)
        //                                     && cnr.Concepto_negocio.id_concepto_basico == idConceptoBasico
        //                                     && cnr.Concepto_negocio.es_vigente)
        //                       .Select(cnr => cnr.Concepto_negocio)
        //                       .Select(cn => new Concepto_Negocio_Comercial_Venta()
        //                       {
        //                           Id = cn.id,
        //                           CodigoBarra = cn.codigo_barra,
        //                           Sufijo = cn.sufijo,
        //                           IdConceptoBasico = cn.Detalle_maestro4.id,
        //                           NombreConceptoBasico = cn.Detalle_maestro4.nombre,
        //                           ValorConceptoBasico = cn.Detalle_maestro4.valor,
        //                           ValoresCaracteristicas = cn.Valor_caracteristica_concepto_negocio
        //                                                       .Select(vccn => vccn.Valor_caracteristica)
        //                                                       .Select(vc => new ValorCaracteristica()
        //                                                       {
        //                                                           Id = vc.id,
        //                                                           IdCaracteristica = vc.id_caracteristica,
        //                                                           Valor = vc.valor
        //                                                       }).OrderBy(c => c.Valor),

        //                           Precios = cn.Precio1.Where(p => p.id_actor_negocio == idActorNegocio && p.es_vigente)
        //                                                .Select(p => new Precio_Concepto_Negocio_Comercial()
        //                                                {
        //                                                    Id = p.id,
        //                                                    IdTarifa = p.id_tarifa_d,
        //                                                    NombreTarifa = p.Detalle_maestro3.nombre,
        //                                                    Valor = p.valor,
        //                                                    Codigo = p.Detalle_maestro3.codigo
        //                                                }),

        //                           NombrePresentacion = cn.Detalle_maestro3.nombre,
        //                           Contenido = cn.contenido,
        //                           UnidadMedidaPresentacion = cn.Detalle_maestro1.valor,
        //                           UnidadMedidaComercial = cn.Detalle_maestro.valor,
        //                           UnidadMedidaReferencial = cn.Detalle_maestro2.valor
        //                       });
        //    }
        //    catch (Exception e)
        //    {
        //        throw new DatosException("Error al obtener conceptos de negocio por rol y concepto basico", e);
        //    }
        //}

        //public IEnumerable<Concepto_Negocio_Comercial_Venta> ObtenerConceptosDeNegociosComercialesParaVenta(int idActorNegocio, int[] idsRoles, string nombre)
        //{
        //    try
        //    {
        //        return _db.Concepto_negocio_rol
        //                       .Where(cnr => idsRoles.Contains(cnr.id_rol)
        //                                     && cnr.Concepto_negocio.nombre.Contains(nombre)
        //                                     && cnr.Concepto_negocio.es_vigente)
        //                       .Select(cnr => cnr.Concepto_negocio)
        //                       .Select(cn => new Concepto_Negocio_Comercial_Venta()
        //                       {
        //                           Id = cn.id,
        //                           CodigoBarra = cn.codigo_barra,
        //                           Sufijo = cn.sufijo,
        //                           IdConceptoBasico = cn.Detalle_maestro4.id,
        //                           NombreConceptoBasico = cn.Detalle_maestro4.nombre,
        //                           ValorConceptoBasico = cn.Detalle_maestro4.valor,
        //                           ValoresCaracteristicas = cn.Valor_caracteristica_concepto_negocio
        //                                                       .Select(vccn => vccn.Valor_caracteristica)
        //                                                       .Select(vc => new ValorCaracteristica()
        //                                                       {
        //                                                           Id = vc.id,
        //                                                           IdCaracteristica = vc.id_caracteristica,
        //                                                           Valor = vc.valor
        //                                                       }).OrderBy(c => c.Valor),

        //                           Precios = cn.Precio1.Where(p => p.id_actor_negocio == idActorNegocio && p.es_vigente)
        //                                                .Select(p => new Precio_Concepto_Negocio_Comercial()
        //                                                {
        //                                                    Id = p.id,
        //                                                    IdTarifa = p.id_tarifa_d,
        //                                                    NombreTarifa = p.Detalle_maestro3.nombre,
        //                                                    Valor = p.valor,
        //                                                    Codigo = p.Detalle_maestro3.codigo
        //                                                }),

        //                           NombrePresentacion = cn.Detalle_maestro3.nombre,
        //                           Contenido = cn.contenido,
        //                           UnidadMedidaPresentacion = cn.Detalle_maestro1.valor,
        //                           UnidadMedidaComercial = cn.Detalle_maestro.valor,
        //                           UnidadMedidaReferencial = cn.Detalle_maestro2.valor
        //                       });
        //    }
        //    catch (Exception e)
        //    {
        //        throw new DatosException("Error al obtener conceptos de negocio por rol y concepto basico", e);
        //    }
        //}


        //public Concepto_Negocio_Comercial_Venta ObtenerConceptosDeNegociosComercialesParaVentaPorCodigoBarra(int idActorNegocioInterno, int[] idsRoles, string codigoBarra, int idTipoTransaccion, int idEstadoTransaccion)
        //{
        //    try
        //    {
        //        _db.Configuration.LazyLoadingEnabled = false;
        //        return _db.Concepto_negocio_rol
        //                       .Where(cnr => idsRoles.Contains(cnr.id_rol)
        //                                     && cnr.Concepto_negocio.codigo_barra == codigoBarra
        //                                     && cnr.Concepto_negocio.es_vigente)
        //                        .Select(cnr => cnr.Concepto_negocio)
        //                       .Select(cn => new Concepto_Negocio_Comercial_Venta()
        //                       {
        //                           Id = cn.id,
        //                           CodigoBarra = cn.codigo_barra,
        //                           Sufijo = cn.sufijo,
        //                           NombreConceptoBasico = cn.Detalle_maestro4.nombre,
        //                           ValorConceptoBasico = cn.Detalle_maestro4.valor,
        //                           ValoresCaracteristicas = cn.Valor_caracteristica_concepto_negocio
        //                                                       .Select(vccn => vccn.Valor_caracteristica)
        //                                                       .Select(vc => new ValorCaracteristica()
        //                                                       {
        //                                                           Id = vc.id,
        //                                                           IdCaracteristica = vc.id_caracteristica,
        //                                                           Valor = vc.valor
        //                                                       }).OrderBy(c => c.Valor),
        //                           Precios = cn.Precio1.Where(p => p.id_actor_negocio == idActorNegocioInterno && p.es_vigente)
        //                                                .Select(p => new Precio_Concepto_Negocio_Comercial()
        //                                                {
        //                                                    Id = p.id,
        //                                                    IdTarifa = p.id_tarifa_d,
        //                                                    NombreTarifa = p.Detalle_maestro3.nombre,
        //                                                    Valor = p.valor,
        //                                                    Codigo = p.Detalle_maestro3.codigo
        //                                                }),
        //                           NombrePresentacion = cn.Detalle_maestro3.nombre,
        //                           Contenido = cn.contenido,
        //                           UnidadMedidaPresentacion = cn.Detalle_maestro1.valor,
        //                           UnidadMedidaComercial = cn.Detalle_maestro.valor,
        //                           UnidadMedidaReferencial = cn.Detalle_maestro2.valor
        //                       }).SingleOrDefault();
        //    }
        //    catch (Exception e)
        //    {
        //        throw new DatosException("Error al obtener conceptos de negocio por rol y concepto basico", e);
        //    }
        //    finally
        //    {
        //        _db.Configuration.LazyLoadingEnabled = true;
        //    }
        //}

        //public Concepto_Negocio_Comercial_Venta ObtenerConceptosDeNegociosComercialesParaVentaPorCodigoBarraInclyendoComplementos(int idActorNegocioInterno, int[] idsRoles, string codigoBarra, int idTipoTransaccion, int idEstadoTransaccion)
        //{
        //    try
        //    {
        //        _db.Configuration.LazyLoadingEnabled = false;
        //        return _db.Concepto_negocio_rol
        //                       .Where(cnr => idsRoles.Contains(cnr.id_rol)
        //                                     && cnr.Concepto_negocio.codigo_barra == codigoBarra
        //                                     && cnr.Concepto_negocio.es_vigente)
        //                        .Select(cnr => cnr.Concepto_negocio)
        //                       .Select(cn => new Concepto_Negocio_Comercial_Venta()
        //                       {
        //                           Id = cn.id,
        //                           CodigoBarra = cn.codigo_barra,
        //                           Sufijo = cn.sufijo,
        //                           NombreConceptoBasico = cn.Detalle_maestro4.nombre,
        //                           ValorConceptoBasico = cn.Detalle_maestro4.valor,
        //                           ValoresCaracteristicas = cn.Valor_caracteristica_concepto_negocio
        //                                                       .Select(vccn => vccn.Valor_caracteristica)
        //                                                       .Select(vc => new ValorCaracteristica()
        //                                                       {
        //                                                           Id = vc.id,
        //                                                           IdCaracteristica = vc.id_caracteristica,
        //                                                           Valor = vc.valor
        //                                                       }).OrderBy(c => c.Valor),
        //                           Precios = cn.Precio1.Where(p => p.id_actor_negocio == idActorNegocioInterno && p.es_vigente)
        //                                                .Select(p => new Precio_Concepto_Negocio_Comercial()
        //                                                {
        //                                                    Id = p.id,
        //                                                    IdTarifa = p.id_tarifa_d,
        //                                                    NombreTarifa = p.Detalle_maestro3.nombre,
        //                                                    Valor = p.valor,
        //                                                    Codigo = p.Detalle_maestro3.codigo
        //                                                }),
        //                           NombrePresentacion = cn.Detalle_maestro3.nombre,
        //                           Contenido = cn.contenido,
        //                           UnidadMedidaPresentacion = cn.Detalle_maestro1.valor,
        //                           UnidadMedidaComercial = cn.Detalle_maestro.valor,
        //                           UnidadMedidaReferencial = cn.Detalle_maestro2.valor,


        //                           Complementos = cn.Detalle_transaccion.Where(dt => dt.Transaccion.id_tipo_transaccion == idTipoTransaccion &&
        //                                                      dt.Transaccion.id_actor_negocio_interno == idActorNegocioInterno
        //                                                      && dt.Transaccion.Estado_transaccion.OrderByDescending(est => est.id).FirstOrDefault().id_estado == idEstadoTransaccion
        //                                                      && dt.Concepto_negocio.codigo_barra == codigoBarra)
        //                          .Select(dt => new Complemento_Concepto_Negocio_Comercial()
        //                          {
        //                              IdDetalle = dt.id,
        //                              Stock = dt.cantidad,
        //                              Lote = dt.lote,
        //                              CaracteristicasPropias = dt.Valor_detalle_maestro_detalle_transaccion
        //                              .Select(vdmdt => new ValorDetalleMaestroDetalleTransaccion()
        //                              {
        //                                  Id = vdmdt.id,
        //                                  Numero = vdmdt.numero,
        //                                  Valor = vdmdt.valor,
        //                                  IdDetalleMaestro = vdmdt.id_detalle_maestro,
        //                                  IdDetalleTransaccion = vdmdt.id_detalle_transaccion,
        //                              })
        //                          }).Where(ccnc => ccnc.Stock > 0).OrderBy(ccnc => ccnc.Lote)
        //                       }).SingleOrDefault();
        //    }
        //    catch (Exception e)
        //    {
        //        throw new DatosException("Error al obtener conceptos de negocio por rol y concepto basico", e);
        //    }
        //    finally
        //    {
        //        _db.Configuration.LazyLoadingEnabled = true;
        //    }
        //}


        public IEnumerable<Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesPorNombreIncluyendoPrecios(int idActorNegocioInternoQueTieneLosPrecios, int[] idsRoles, string nombre)
        {
            try
            {
                return _db.Concepto_negocio_rol
                               .Where(cnr => idsRoles.Contains(cnr.id_rol)
                                             && cnr.Concepto_negocio.nombre.Contains(nombre)
                                             && cnr.Concepto_negocio.es_vigente)
                               .Select(cnr => cnr.Concepto_negocio)
                               .Select(cn => new Concepto_Negocio_Comercial()
                               {
                                   Id = cn.id,
                                   Nombre = cn.nombre,
                                   Codigo = cn.codigo,
                                   CodigoBarra = cn.codigo_barra,
                                   Sufijo = cn.sufijo,
                                   IdConceptoBasico = cn.Detalle_maestro4.id,
                                   NombreConceptoBasico = cn.Detalle_maestro4.nombre,
                                   ValorConceptoBasico = cn.Detalle_maestro4.valor,
                                   Precios = cn.Precio1.Where(p => p.id_actor_negocio == idActorNegocioInternoQueTieneLosPrecios && p.es_vigente)
                                                        .Select(p => new Precio_Concepto_Negocio_Comercial()
                                                        {
                                                            Id = p.id,
                                                            IdTarifa = p.id_tarifa_d,
                                                            Tarifa = p.Detalle_maestro3.nombre,
                                                            Valor = p.valor,
                                                            Codigo = p.Detalle_maestro3.codigo
                                                        })
                               });
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener conceptos de negocio por rol y concepto basico", e);
            }
        }

        public IEnumerable<Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesPorNombreInclyendoStockPreciosYStock(int idActorNegocioInterno, int idTipoTransaccion, int idTarifa, int idUltimoEstado, int[] idsRoles, string nombre)
        {
            try
            {
                return _db.Concepto_negocio_rol
                               .Where(cnr => idsRoles.Contains(cnr.id_rol))
                               .Select(cnr => cnr.Concepto_negocio)
                               .Where(cn => cn.es_vigente && cn.nombre.Contains(nombre))
                               .Select(cn => new Concepto_Negocio_Comercial()
                               {
                                   Id = cn.id,
                                   Nombre = cn.nombre,
                                   Codigo = cn.codigo,
                                   CodigoBarra = cn.codigo_barra,
                                   Sufijo = cn.sufijo,
                                   IdConceptoBasico = cn.Detalle_maestro4.id,
                                   NombreConceptoBasico = cn.Detalle_maestro4.nombre,
                                   ValorConceptoBasico = cn.Detalle_maestro4.valor,
                                   ValoresCaracteristicas = cn.Valor_caracteristica_concepto_negocio
                                                               .Select(vccn => vccn.Valor_caracteristica)
                                                               .Select(vc => new ValorCaracteristica()
                                                               {
                                                                   Id = vc.id,
                                                                   IdCaracteristica = vc.id_caracteristica,
                                                                   NombreCaracteristica = vc.Detalle_maestro.nombre,
                                                                   Valor = vc.valor
                                                               }).OrderBy(c => c.Valor),

                                   Precios = cn.Precio1.Where(p => p.id_actor_negocio == idActorNegocioInterno && p.es_vigente)
                                                        .Select(p => new Precio_Concepto_Negocio_Comercial()
                                                        {
                                                            Id = p.id,
                                                            IdTarifa = p.id_tarifa_d,
                                                            Tarifa = p.Detalle_maestro3.nombre,
                                                            Valor = p.valor,
                                                            Codigo = p.Detalle_maestro3.codigo
                                                        }),

                                   NombrePresentacion = cn.Detalle_maestro3.nombre,
                                   Contenido = cn.contenido,
                                   CodigoUnidadMedidaPresentacion = cn.Detalle_maestro1.codigo,
                                   UnidadMedidaPresentacion = cn.Detalle_maestro1.valor,
                                   CodigoUnidadMedidaComercial = cn.Detalle_maestro.codigo,
                                   UnidadMedidaComercial = cn.Detalle_maestro.valor,
                                   UnidadMedidaReferencial = cn.Detalle_maestro2.valor,
                                   Stock = cn.Detalle_transaccion.Where(dt => dt.Transaccion.id_tipo_transaccion == idTipoTransaccion && dt.Transaccion.id_actor_negocio_interno == idActorNegocioInterno && dt.Transaccion.Estado_transaccion.OrderByDescending(est => est.id).FirstOrDefault().id_estado == idUltimoEstado).Sum(dt => dt.cantidad),
                                   PrecioVenta = cn.Precio1.Where(p => p.id_actor_negocio == idActorNegocioInterno && p.es_vigente && p.id_tarifa_d == idTarifa)
                                                                            .OrderByDescending(p => p.id)
                                                                            .FirstOrDefault().valor
                               });

            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener conceptos de negocio por rol y concepto basico", e);
            }
        }

        #endregion


        #region CONCEPTOS BASICOS
        public IEnumerable<Concepto_Basico> ObtenerConceptosBasicosIncluyendodoCaracteristicas(int idMaestro, bool esVigente)
        {
            try
            {
                return _db.Detalle_maestro.Where(dm => dm.id_maestro == idMaestro && dm.es_vigente == esVigente).Select(dm => new Concepto_Basico()
                {
                    Id = dm.id,
                    Nombre = dm.nombre,
                    Valor = dm.valor,
                    Caracteristicas = dm.Caracteristica_concepto.Select(cc => cc.Detalle_maestro1).Select(c => new Caracteristica()
                    {
                        IdDetalleMaestro = c.id,
                        Nombre = c.nombre,
                        Valor = c.valor,
                        IdMaestro = c.id_maestro
                    })
                });
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener conceptos básicos con característica ");
            }
        }
        #endregion


        #region CARACTERISTICAS

        public OperationResult ActualizarCaracteristica(Detalle_maestro caracteristica_upd)
        {
            try
            {
                Detalle_maestro caracteristica_bd = _db.Detalle_maestro.Single(dm => dm.id == caracteristica_upd.id);
                caracteristica_upd.fecha_registro = caracteristica_bd.fecha_registro;
                _db.Entry(caracteristica_bd).CurrentValues.SetValues(caracteristica_upd);

                List<Valor_caracteristica> valores_upd = caracteristica_upd.Valor_caracteristica.ToList();
                List<Valor_caracteristica> valores_bd = caracteristica_bd.Valor_caracteristica.ToList();

                foreach (var valor_db in valores_bd)
                {
                    //si existe actualizo
                    if (valores_upd.Any(d => d.id == valor_db.id))
                    {
                        var valor = valores_upd.Single(d => d.id == valor_db.id);
                        _db.Entry(valor_db).CurrentValues.SetValues(valor);
                    }
                    else//sino lo elimino
                    {
                        _db.Valor_caracteristica.Remove(valor_db);
                    }
                }
                //agrego los nuevos valores
                foreach (var valor_upd in valores_upd)
                {
                    if (!valores_bd.Any(d => d.id == valor_upd.id))
                    {
                        _db.Valor_caracteristica.Add(valor_upd);
                    }
                }
                var resultado = Save();
                resultado.data = caracteristica_upd.id;
                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Concepto_negocio> ConceptosNegocioVigentesConCaracteristica(int idCaracteristica)
        {
            try
            {
                return _db.Caracteristica_concepto.Where(cc => cc.id_caracteristica == idCaracteristica).Select(cc => cc.Detalle_maestro).SelectMany(dm => dm.Concepto_negocio4).Where(cn => cn.es_vigente);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Detalle_maestro> FamiliasVigentesConCaracteristica(int idCaracteristica)
        {
            try
            {
                return _db.Caracteristica_concepto.Where(cc => cc.id_caracteristica == idCaracteristica).Select(cc => cc.Detalle_maestro).Where(dm => dm.es_vigente);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

       

        #endregion

        #region VALOR_CARACTERISTICA

        public OperationResult GuardarValorCaracteristica(Valor_caracteristica valorCaracteristica)
        {
            try
            {
                _db.Valor_caracteristica.Add(valorCaracteristica);
                var resultado = Save();
                resultado.data = valorCaracteristica.id;
                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public OperationResult ActualizarValorCaracteristica(Valor_caracteristica valorCaracteristica_upd)
        {
            try
            {
                Valor_caracteristica valorCaracteristica_bd = _db.Valor_caracteristica.Single(vc => vc.id == valorCaracteristica_upd.id);
                _db.Entry(valorCaracteristica_bd).CurrentValues.SetValues(valorCaracteristica_upd);
                var resultado = Save();
                resultado.data = valorCaracteristica_upd.id;
                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Valor_caracteristica> ObtenerValoresDeCaracteristica(int idCaracteristica)
        {
            try
            {
                return _db.Valor_caracteristica.Where(vc => vc.id_caracteristica == idCaracteristica);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Valor_caracteristica ObtenerValorCaracteristica(int idValorCaracteristica)
        {
            try
            {
                return _db.Valor_caracteristica.SingleOrDefault(vc => vc.id == idValorCaracteristica);
            }
            catch (Exception e)
            {
                throw new DatosException("No se pudo obtener valor característica", e);
            }
        }

        #endregion


        #region RECURSOS

        public IEnumerable<Rol> ObtenerRolesDeConceptosExceptoMercaderiaYServicios(int aplicaA, int[] idsAExcluir)
        {
            try
            {
                return _db.Rol.Where(r => r.aplica_a == aplicaA && !idsAExcluir.Contains(r.id));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Existencia> ObtenerExistenciasIncluyendoConceptoNegocioYActorNegocio(int idEntidadInterna)
        {
            try
            {
                return _db.Existencia
                    .Include(e => e.Concepto_negocio).Include(e => e.Concepto_negocio.Detalle_maestro)
                    .Include(e => e.Actor_negocio)
                    .Where(e => e.id_punto_atencion == idEntidadInterna);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Detalle_maestro> ObtenerDetalleMaestro4DeConceptoNegocioConRol(int idRol)
        {
            try
            {
                return _db.Concepto_negocio_rol.Where(cnr => cnr.id_rol == idRol).Select(cnr => cnr.Concepto_negocio.Detalle_maestro4);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Precio_Compra_Venta_Concepto> ObtenerPreciosCompraVentaDeConceptoNegocio(int idConceptoNegocio)
        {
            try
            {
                IEnumerable<Precio_Compra_Venta_Concepto> precios = new List<Precio_Compra_Venta_Concepto>();

                var transaccionCompra = _db.Detalle_transaccion.OrderByDescending(dt => dt.id).FirstOrDefault(dt => dt.id_concepto_negocio == idConceptoNegocio && dt.Transaccion.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenDeCompra && dt.Transaccion.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado);
                decimal precioCompra = transaccionCompra != null ? transaccionCompra.precio_unitario : 0;

                var concepto = _db.Concepto_negocio.SingleOrDefault(cn => cn.id == idConceptoNegocio);

                if (concepto != null)
                {
                    precios = concepto.Precio1.Where(p => p.es_vigente)
                   .Select(p => new Precio_Compra_Venta_Concepto()
                   {
                       IdPrecio = p.id,
                       IdPuntoPrecio = p.id_actor_negocio,
                       PuntoPrecio = p.Actor_negocio.PrimerNombre,
                       IdTarifa = p.id_tarifa_d,
                       Tarifa = p.Detalle_maestro3.nombre,
                       PrecioCompra = precioCompra,
                       PrecioVenta = p.valor,
                       FechaInicio = p.fecha_inicio,
                       FechaFin = p.fecha_fin,
                       Descripcion = p.descripcion
                   }
                   );
                }
                return precios;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public decimal ObtenerPrecioPublicoDelIcbper(DateTime fecha)
        {
            try
            {
                decimal precioPublicoDelIcbper = _db.Concepto_negocio.FirstOrDefault(cn => cn.id == ConceptoSettings.Default.IdConceptoNegocioIcbper).Precio1.FirstOrDefault(p => p.id_tarifa_d == MaestroSettings.Default.IdDetalleMaestroTarifaNormal && p.fecha_inicio <= fecha && fecha <= p.fecha_fin).valor;
                return precioPublicoDelIcbper;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener el precio publico del icbper a la fecha", e);
            }
        }



        public int ObtenerMaximoCodigoParaConceptoNegocio(string prefijo, int idRolConcepto, int idConceptoBasico)
        {
            int longitud = prefijo.Length;
            try
            {
                var ultimoCodigo = _db.Concepto_negocio.
                    Where(t => t.Concepto_negocio_rol.Any(cnr => cnr.id_rol == idRolConcepto) && t.id_concepto_basico == idConceptoBasico && t.codigo.StartsWith(prefijo)).OrderByDescending(c => c.id).FirstOrDefault();
                return ultimoCodigo != null ? Convert.ToInt32(ultimoCodigo.codigo.Remove(0, longitud)) : 1;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        #endregion


        #region VALIDACIONES 

        //Concepto Negocio
        public bool ExisteCodigoDeBarraConceptoNegocio(string codigoBarra)
        {
            try
            {
                bool existe = _db.Concepto_negocio.Where(cn => cn.codigo_barra == codigoBarra
                                                        && cn.codigo_barra != ""
                                                        && cn.es_vigente).Any();
                return existe;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al comprobar si existe el codigo de barra ", e);
            }
        }

        public bool ExisteNombreConceptoNegocio(string nombre)
        {
            try
            {
                bool existe = _db.Concepto_negocio.Where(cn => cn.nombre == nombre && cn.es_vigente).Any();

                return existe;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al comprobar si existe el nombre de concepto_negocio", e);
            }

        }

        public bool TieneConceptosDeNegocio(int idDetalleMaestro, bool esVigente)
        {
            try
            {
                bool existe = _db.Detalle_maestro.Where(dm => dm.id == idDetalleMaestro).SelectMany(dm => dm.Concepto_negocio4).Where(cn => cn.es_vigente == esVigente).Any();

                return existe;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al comprobar si tiene asociado conceptos de negocio", e);
            }

        }

        //Valor caracteristica
        public bool ExisteNombreDeValorCaracteristica(int idCaracteristica, string valor)
        {
            try
            {
                bool existe = _db.Valor_caracteristica.Where(vc => vc.id_caracteristica == idCaracteristica && vc.valor == valor).Any();

                return existe;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al comprobar si existe el nombre de valor característica", e);
            }
        }

        public bool EsBien(string codigoBarra)
        {
            try
            {

                Concepto_negocio concepto = _db.Concepto_negocio.SingleOrDefault(cn => cn.codigo_barra == codigoBarra && cn.es_vigente);
                if (concepto == null)
                {
                    throw new AdvertenciaException("Código de barra ingresado no existe.");
                }
                bool esBien = concepto.Detalle_maestro4.valor == "1";
                return esBien;
            }
            catch (AdvertenciaException e)
            {
                throw new AdvertenciaException(e.Message);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al comprobar si es un bien el concepto", e);
            }
        }
        public bool EsBien(int id)
        {
            try
            {
                Concepto_negocio concepto = _db.Concepto_negocio.SingleOrDefault(cn => cn.id == id);
                if (concepto == null)
                {
                    throw new AdvertenciaException("No existe el concepto con el id indicado.");
                }
                bool esBien = concepto.Detalle_maestro4.valor == "1";
                return esBien;
            }
            catch (AdvertenciaException e)
            {
                throw new AdvertenciaException(e.Message);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al comprobar si el concepto es un bien", e);
            }
        }

        #endregion
        public IEnumerable<ReporteDigemid> ObtenerReporteConceptosDigemid(int idRol, int idActorNegocioInternoQueTieneLosPrecios, int idTarifaUnitariaDigemid)
        {
            try
            {
                var idsFamiliasNoValidas = Diccionario.IdsFamilasANoMostrar;
                var resultado = _db.Concepto_negocio_rol
                               .Where(cnr => cnr.id_rol == idRol && cnr.Concepto_negocio.es_vigente)
                               .Select(cnr => cnr.Concepto_negocio).Where(cn => !idsFamiliasNoValidas.Contains(cn.id_concepto_basico) && cn.codigo_negocio1 != "")
                               .Select(cn => new ReporteDigemid()
                               {
                                   CodigoConcepto = cn.codigo_negocio1,
                                   PrecioUnitario = cn.Precio1.FirstOrDefault(p => p.id_actor_negocio == idActorNegocioInternoQueTieneLosPrecios && p.es_vigente && p.id_tarifa_d == idTarifaUnitariaDigemid) == null ? 0 : cn.Precio1.FirstOrDefault(p => p.id_actor_negocio == idActorNegocioInternoQueTieneLosPrecios && p.es_vigente && p.id_tarifa_d == idTarifaUnitariaDigemid).valor
                               }).ToList();
                return resultado.Where(r => r.PrecioUnitario > 0);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener reporte de conceptos de digemid", e);
            }
        }
    }
}

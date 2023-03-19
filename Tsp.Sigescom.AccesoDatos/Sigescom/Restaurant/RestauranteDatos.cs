using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Tsp.Sigescom.AccesoDatos.Generico;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;
using Tsp.Sigescom.Modelo.SigesRestaurant;

namespace Tsp.Sigescom.AccesoDatos.Restaurant
{
    public partial class RestauranteDatos : RepositorioBase, IRestauranteRepositorio
    {


        public IEnumerable<Familia> ObtenerFamiliasSuperficial()
        {
            var idRolConceptoRestaurante = RestauranteSettings.Default.IdRolConceptoRestaurante;
            IEnumerable<Familia> Familias = _db.Concepto_negocio_rol.Where(cnr => cnr.id_rol == idRolConceptoRestaurante).Select(cnr => cnr.Concepto_negocio.Detalle_maestro4).Select(dm => new Familia()
            {
                Id = dm.id,
                Nombre = dm.nombre
            }).ToList();
            return Familias;
        }

        public OperationResult CalcularAtencion(long idAtencion)
        {
            try
            {
                var atencion = _db.Transaccion.FirstOrDefault(t => t.id == idAtencion);
                atencion.importe_total = atencion.Transaccion1.Sum(t => t.importe_total);
                var result = Save();
                result.data = atencion.id;
                result.information = atencion;
                return result;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al calcular atención", e);

            }
        }
        public OperationResult ActualizarEstadoDeDetalleDeOrden_CalcularOrden_CalcularAtencion(int idDetalleDeOrden, EstadoDeDetalleDeOrden estado)
        {
            try
            {
                OperationResult result = new OperationResult();
                var idsEstadosASumar = new int[] { (int)EstadoDeDetalleDeOrden.Atendido, (int)EstadoDeDetalleDeOrden.Observado, (int)EstadoDeDetalleDeOrden.Preparacion, (int)EstadoDeDetalleDeOrden.Registrado, (int)EstadoDeDetalleDeOrden.Servido };
                var idEstado = (int)estado;

                Detalle_transaccion dbdetalle = _db.Detalle_transaccion.Where(dt => dt.id == idDetalleDeOrden).FirstOrDefault();

                dbdetalle.indicadorMultiproposito = idEstado;
                var sumaDetalles = dbdetalle.Transaccion.Detalle_transaccion.Where(dt => idsEstadosASumar.Contains((int)dt.indicadorMultiproposito)).Sum(dt => dt.total);
                dbdetalle.Transaccion.importe_total = sumaDetalles;
                var sumaAtencion = dbdetalle.Transaccion.Transaccion2.Transaccion1.Sum(t => t.importe_total);
                dbdetalle.Transaccion.Transaccion2.importe_total = sumaAtencion;

                result = Save();
                result.data = idDetalleDeOrden;
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public OperationResult ActualizarEstadosDeDetallesDeOrdenes(long[] IdsDetallesDeOrdenes, EstadoDeDetalleDeOrden nuevoEstado)
        {
            try
            {
                OperationResult result = new OperationResult();
                var estado = (int)nuevoEstado;
                var detalles = _db.Detalle_transaccion.Where(dt => IdsDetallesDeOrdenes.Contains(dt.id)).ToList();
                detalles.ForEach(d => { d.indicadorMultiproposito = estado; });
                result = Save();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public OperationResult ActualizarEstadosDeDetallesDeOrden_CalcularOrden_CalcularAtencion(long idOrden, EstadoDeDetalleDeOrden nuevoEstado)
        {
            try
            {
                OperationResult result = new OperationResult();
                var idsEstadosASumar = new int[] { (int)EstadoDeDetalleDeOrden.Atendido, (int)EstadoDeDetalleDeOrden.Observado, (int)EstadoDeDetalleDeOrden.Preparacion, (int)EstadoDeDetalleDeOrden.Registrado, (int)EstadoDeDetalleDeOrden.Servido };
                var idEstado = (int)nuevoEstado;
                var detalles = _db.Detalle_transaccion.Where(dt => dt.id_transaccion == idOrden).ToList();
                var orden = _db.Transaccion.FirstOrDefault(t => t.id == idOrden);
                detalles.ForEach(d => { d.indicadorMultiproposito = idEstado; });

                var sumaDetalles = detalles.Where(dt => idsEstadosASumar.Contains((int)dt.indicadorMultiproposito)).Sum(dt => dt.total);
                orden.importe_total = sumaDetalles;
                var sumaAtencion = orden.Transaccion2.Transaccion1.Sum(t => t.importe_total);
                orden.Transaccion2.importe_total = sumaAtencion;

                result = Save();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public OperationResult ActualizarEstadoDeOrdenes(long[] Ids, int Estado)
        {
            try
            {
                OperationResult result = new OperationResult();

                var ordenes = _db.Transaccion.Where(t => Ids.Contains(t.id)).ToList();

                ordenes.ForEach(d =>
                {
                    d.id_estado_actual = Estado;
                });

                result = Save();

                return result;

            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public IEnumerable<Orden_Atencion> ObtenerOrdenesPorEstado(int NumEstado)
        {
            try
            {

                IEnumerable<Orden_Atencion> ordenes = _db.Transaccion.Where(t => t.id_tipo_transaccion == RestauranteSettings.Default.IdTipoTransaccionOrdenDeRestaurante && t.id_estado_actual == NumEstado).Select(t => new Orden_Atencion()
                {
                    Id = t.id,
                    Codigo = t.codigo,
                    Estado = (int)t.id_estado_actual,
                    IdAtencion = (long)t.id_transaccion_padre,
                    ImporteOrden = t.importe_total,
                    IdMesa = (int)t.Transaccion2.id_actor_negocio_interno1,
                    NombreMesa = t.Transaccion2.Actor_negocio4.Actor.primer_nombre,
                    DetallesDeOrden = t.Detalle_transaccion.Select(dt => new DetalleOrden()
                    {
                        Id = dt.id,
                        Precio = dt.precio_unitario,
                        Cantidad = dt.cantidad,
                        Importe = dt.total,
                        Estado = (int)dt.indicadorMultiproposito,
                        DetalleItemRestauranteJson = dt.detalle,
                        NombreItem = dt.Concepto_negocio.nombre,
                        IdItem = dt.id_concepto_negocio,
                        IdTransaccion = dt.id_transaccion
                    })
                }).ToList();

                //foreach (Orden_Atencion orden in ordenes)
                //{
                //    Mesa mesa = new Mesa(ObtenerMesaPorId(orden.IdMesa));
                //    orden.NombreMesa = mesa.Nombre;
                //    orden.DetallesDeOrden = ObtenerDetallesDeOrdenDeUnaOrdenIncluyendoItemsDeRestauranteYDetallesDeComplemento(orden.Id).ToList();
                //}


                return ordenes;

            }
            catch (Exception e)
            {
                throw e;
            }
        }
     
        public IEnumerable<Orden_Atencion> ObtenerOrdenesPorEstadoDeUnAmbiente(int NumEstado, int IdAmbiente)
        {
            try
            {

                IEnumerable<Orden_Atencion> ordenes = _db.Transaccion
                    .Include(t => t.Transaccion2)
                    .Include(t => t.Transaccion2.Actor_negocio4)
                    .Include(t => t.Transaccion2.Actor_negocio4.Actor)
                    .Include(t => t.Detalle_transaccion)
                    .Where(t => t.Transaccion2.Actor_negocio4.id_actor_negocio_padre == IdAmbiente && t.id_tipo_transaccion == RestauranteSettings.Default.IdTipoTransaccionOrdenDeRestaurante && t.id_estado_actual == NumEstado)
                    .Select(t => new Orden_Atencion()
                    {
                        Id = t.id,
                        Codigo = t.codigo,
                        Estado = (int)t.id_estado_actual,
                        IdAtencion = (long)t.id_transaccion_padre,
                        ImporteOrden = t.importe_total,
                        IdMesa = (int)t.Transaccion2.id_actor_negocio_interno1,
                        NombreMesa = t.Transaccion2.Actor_negocio4.Actor.primer_nombre,
                        DetallesDeOrden = t.Detalle_transaccion.Select(dt => new DetalleOrden()
                        {
                            Id = dt.id,
                            Precio = dt.precio_unitario,
                            Cantidad = dt.cantidad,
                            Importe = dt.total,
                            Estado = (int)dt.indicadorMultiproposito,
                            DetalleItemRestauranteJson = dt.detalle,
                            NombreItem = dt.Concepto_negocio.nombre,
                            IdItem = dt.id_concepto_negocio,
                            IdTransaccion = dt.id_transaccion
                        })
                    }).ToList();

                return ordenes;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<AtencionRestaurante> ObtenerAtencionesPorEstado(int IdEstado)
        {
            try
            {

                var tipoTransaccionAtencion = RestauranteSettings.Default.IdTipoTransaccionAtencionDeRestaurante;
                var tipoTransaccionOrden = RestauranteSettings.Default.IdTipoTransaccionOrdenDeRestaurante;
                IEnumerable<Transaccion> transacciones = _db.Transaccion
                    .Include(t => t.Transaccion1)
                    .Include(t => t.Actor_negocio1.Actor)
                    .Include(t => t.Transaccion1.Select(to => to.Detalle_transaccion))
                    .Include(t => t.Transaccion1.Select(to => to.Detalle_transaccion.Select(dt => dt.Concepto_negocio)))
                    .Where(t => t.id_tipo_transaccion == tipoTransaccionAtencion && t.id_estado_actual == IdEstado);

                List<AtencionRestaurante> atenciones = new List<AtencionRestaurante>();
                foreach (var transaccion in transacciones)
                {
                    atenciones.Add(AtencionRestaurante.Convert(transaccion));
                }

                return atenciones;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public IEnumerable<AtencionSinMesa> ObtenerAtencionesSinMesaPorEstados(int[] idsEstados, int idCentroAtencion)
        {
            try
            {

                return _db.Transaccion.Where(t => t.id_tipo_transaccion == RestauranteSettings.Default.IdTipoTransaccionAtencionDeRestaurante && t.Actor_negocio4.id_actor_negocio_padre == idCentroAtencion && idsEstados.Contains((int)t.id_estado_actual) && t.indicador1 && t.enum1 == 0 && t.importe_total > 0).Select(t => new AtencionSinMesa()
                {
                    Id = t.id,
                    IdMesa = (int)t.id_actor_negocio_interno1,
                    Fecha = t.fecha_inicio,
                    Total = t.importe_total,
                    Cliente = t.Actor_negocio4.Actor.primer_nombre,
                    EsDelivery = t.indicador2
                });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Orden_Atencion ObtenerOrdenDeAtencionIncluidoDetallesDeOrdenItemsDeRestauranteYDetallesDeComplemento(long idOrden)
        {
            Orden_Atencion Orden = _db.Transaccion.Where(t => t.id == idOrden).Select(t => new Orden_Atencion()
            {
                Codigo = t.codigo,
                ImporteOrden = t.importe_total,
                Id = t.id,
                IdAtencion = (long)t.id_transaccion_padre,
                Estado = (int)t.id_estado_actual,
                IdMesa = (int)t.Transaccion2.id_actor_negocio_interno1,
                NombreMesa = t.Transaccion2.Actor_negocio4.Actor.primer_nombre,
                Mozo = new ItemGenerico()
                {
                    Id = t.id_actor_negocio_interno,
                    Nombre = t.Actor_negocio2.Actor.tercer_nombre
                },
                FechaHoraRegistro = t.fecha_inicio,
                DetallesDeOrden = t.Detalle_transaccion.Select(dt => new DetalleOrden()
                {
                    Id = dt.id,
                    Precio = dt.precio_unitario,
                    Cantidad = dt.cantidad,
                    Importe = dt.total,
                    Estado = (int)dt.indicadorMultiproposito,
                    DetalleItemRestauranteJson = dt.detalle,
                    NombreItem = dt.Concepto_negocio.nombre,
                    IdItem = dt.id_concepto_negocio,
                    IdTransaccion = dt.id_transaccion
                })
            }).SingleOrDefault();
            Orden.DetallesDeOrden.ToList().ForEach(dor =>
            {
                dor.DetalleItemRestaurante = JsonConvert.DeserializeObject<Detalle_Item_Restaurante>(dor.DetalleItemRestauranteJson);
                dor.ValoresComplementos = new List<string>();
                foreach (var detalleComplemento in dor.DetalleItemRestaurante.DetallesComplemento)
                {
                    if (detalleComplemento.ItemsComplemento.Select(dc => dc.Nombre).ToList().Count > 0)
                        dor.ValoresComplementos.Add(String.Join(", ", detalleComplemento.ItemsComplemento.Select(dc => dc.Nombre).ToList()));
                }
            });
            return Orden;
        }
        public IEnumerable<Orden_Atencion> ObtenerOrdenesDeAtencionIncluidoDetallesDeOrdenItemsDeRestauranteYDetallesDeComplemento(long idAtencion)
        {
            IEnumerable<Orden_Atencion> Ordenes = _db.Transaccion.Where(t => t.id_transaccion_padre == idAtencion && t.id_tipo_transaccion == RestauranteSettings.Default.IdTipoTransaccionOrdenDeRestaurante).Select(t => new Orden_Atencion()
            {
                Codigo = t.codigo,
                ImporteOrden = t.importe_total,
                Id = t.id,
                IdAtencion = (long)t.id_transaccion_padre,
                Estado = (int)t.id_estado_actual,
                IdMesa = (int)t.Transaccion2.id_actor_negocio_interno1,
                NombreMesa = t.Transaccion2.Actor_negocio4.Actor.primer_nombre,
                DetallesDeOrden = t.Detalle_transaccion.Select(dt => new DetalleOrden()
                {
                    Id = dt.id,
                    Precio = dt.precio_unitario,
                    Cantidad = dt.cantidad,
                    Importe = dt.total,
                    Estado = (int)dt.indicadorMultiproposito,
                    DetalleItemRestauranteJson = dt.detalle,
                    NombreItem = dt.Concepto_negocio.nombre,
                    IdItem = dt.id_concepto_negocio,
                    IdTransaccion = dt.id_transaccion
                })
            }).ToList();
            return Ordenes;
        }

        public Orden_Atencion ObtenerOrdenDeAtencionDeDetalleOrden(long idDetalleOrden)
        {
            Orden_Atencion Orden = _db.Detalle_transaccion.Where(dt => dt.id == idDetalleOrden).Select(dt => dt.Transaccion).Select(t => new Orden_Atencion()
            {
                Codigo = t.codigo,
                ImporteOrden = t.importe_total,
                Id = t.id,
                IdAtencion = (long)t.id_transaccion_padre,
                Estado = (int)t.id_estado_actual,
                IdMesa = (int)t.Transaccion2.id_actor_negocio_interno1,
                NombreMesa = t.Transaccion2.Actor_negocio4.Actor.primer_nombre,
                DetallesDeOrden = t.Detalle_transaccion.Select(dt => new DetalleOrden()
                {
                    Id = dt.id,
                    Precio = dt.precio_unitario,
                    Cantidad = dt.cantidad,
                    Importe = dt.total,
                    Estado = (int)dt.indicadorMultiproposito,
                    DetalleItemRestauranteJson = dt.detalle,
                    NombreItem = dt.Concepto_negocio.nombre,
                    IdItem = dt.id_concepto_negocio,
                    IdTransaccion = dt.id_transaccion
                })
            }).SingleOrDefault();
            return Orden;
        }


        public Orden_Atencion ObtenerOrdenDeAtencion(long idOrden)
        {
            Orden_Atencion Orden = _db.Transaccion.Where(t => t.id == idOrden).Select(t => new Orden_Atencion()
            {
                Codigo = t.codigo,
                ImporteOrden = t.importe_total,
                Id = t.id,
                IdAtencion = (long)t.id_transaccion_padre,
                Estado = (int)t.id_estado_actual,
                IdMesa = (int)t.Transaccion2.id_actor_negocio_interno1,
                NombreMesa = t.Transaccion2.Actor_negocio4.Actor.primer_nombre,
                DetallesDeOrden = t.Detalle_transaccion.Select(dt => new DetalleOrden()
                {
                    Id = dt.id,
                    Precio = dt.precio_unitario,
                    Cantidad = dt.cantidad,
                    Importe = dt.total,
                    Estado = (int)dt.indicadorMultiproposito,
                    DetalleItemRestauranteJson = dt.detalle,
                    NombreItem = dt.Concepto_negocio.nombre,
                    IdItem = dt.id_concepto_negocio,
                    IdTransaccion = dt.id_transaccion
                })
            }).SingleOrDefault();
            return Orden;
        }

        public IEnumerable<DetalleOrden> ObtenerDetallesDeOrdenDeUnaOrdenIncluyendoItemsDeRestauranteYDetallesDeComplemento(long idOrden)
        {
            IEnumerable<DetalleOrden> detalles = _db.Detalle_transaccion.Include(dt => dt.Concepto_negocio).Include(dt => dt.Concepto_negocio.Detalle_maestro1.Categoria_concepto).Where(dt => dt.id_transaccion == idOrden).Select(dt => new DetalleOrden()
            {

                Id = dt.id,
                Precio = dt.precio_unitario,
                Cantidad = dt.cantidad,
                Importe = dt.total,
                Estado = (int)dt.indicadorMultiproposito,
                DetalleItemRestauranteJson = dt.detalle,
                NombreItem = dt.Concepto_negocio.nombre,
                IdItem = dt.id_concepto_negocio,
                IdTransaccion = idOrden,

            });
            return detalles;
        }
        public Transaccion ObtenerTransaccionDeAtencionDeMesaOcupada(int idMesa)
        {

            try
            {
                Transaccion transaccion = _db.Transaccion
                    .Include(t => t.Transaccion11)
                    .Include(t => t.Transaccion11.Select(to => to.Detalle_transaccion))
                    .FirstOrDefault(t => t.id_actor_negocio_interno1 == idMesa && (!t.Evento_transaccion.Select(tt => tt.id_evento).Contains(MaestroSettings.Default.IdDetalleMaestroEstadoFacturado) && !(t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoCerrado && t.importe_total == 0)));
                return transaccion;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public IEnumerable<Complemento> ObtenerComplementosPorFamilia(int idFamilia)
        {
            List<Complemento> complementos =
                _db.Caracteristica_concepto.Where(cc => cc.id_concepto == idFamilia && cc.indicador == (int)IndicadorModuloCaracteristica.Restaurante).Select(cc => new Complemento()
                {
                    Id = cc.id_caracteristica,
                    Nombre = cc.Detalle_maestro1.nombre,
                    Maximo = (int)cc.maximo,
                    EsMultiple = cc.es_multiple,
                    EstaActivoRestaurante = cc.indicador == (int)IndicadorModuloCaracteristica.Restaurante,
                    Detalles_Complemento = cc.Detalle_maestro1.Valor_caracteristica.Select(vc => new Item_Complemento()
                    {
                        Id = vc.id,
                        Nombre = vc.valor
                    })

                }).ToList();
            complementos.Where(c => c.EsMultiple).ToList().ForEach(cc => cc.Maximo = cc.Detalles_Complemento.Count());
            return complementos;

        }

        public IEnumerable<Complemento> ObtenerComplementos()
        {
            IEnumerable<Caracteristica_concepto> caracteristicasDeConceptos = _db.Caracteristica_concepto.Include(cc => cc.Detalle_maestro1).Include(cc => cc.Detalle_maestro1.Valor_caracteristica).ToList();
            List<Complemento> complementos = new List<Complemento>();
            foreach (var cc in caracteristicasDeConceptos)
            {
                complementos.Add(new Complemento()
                {
                    Id = cc.id,
                    Nombre = cc.Detalle_maestro1.nombre,
                    Familia = cc.Detalle_maestro.nombre,
                    Maximo = (int)cc.maximo,
                    EsMultiple = cc.es_multiple,
                    EstaActivoRestaurante = cc.indicador == (int)IndicadorModuloCaracteristica.Restaurante,
                    Detalles_Complemento = cc.Detalle_maestro1.Valor_caracteristica.Select(vc => new Item_Complemento()
                    {
                        Id = vc.id,
                        Nombre = vc.valor
                    }).ToList()
                });
            };
            complementos.Where(c => c.EsMultiple).ToList().ForEach(cc => cc.Maximo = cc.Detalles_Complemento.Count());
            return complementos;
        }

        public OperationResult ActualizarComplemento(Complemento complemento)
        {
            try
            {
                Caracteristica_concepto dbCaracteristicaConcepto = _db.Caracteristica_concepto.Single(c => c.id == complemento.Id);
                dbCaracteristicaConcepto.indicador = complemento.EstaActivoRestaurante ? (int)IndicadorModuloCaracteristica.Restaurante : (int)IndicadorModuloCaracteristica.Ninguno;
                dbCaracteristicaConcepto.es_multiple = complemento.EsMultiple;
                var result = Save();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<DetalleOrden> ObtenerDetallesDeOrden(int idOrden)
        {
            return _db.Detalle_transaccion.Where(dt => dt.id_transaccion == idOrden).Select(dt => new DetalleOrden()
            {
                Id = dt.id,
                Cantidad = dt.cantidad,
                Importe = dt.total,
                Precio = dt.precio_unitario,
                Estado = (int)dt.indicadorMultiproposito,
                NombreItem = dt.Concepto_negocio.nombre,
                DetalleItemRestauranteJson = dt.detalle,
                IdTransaccion = idOrden,
                IdItem = dt.id_concepto_negocio
            });
        }
        public async Task<IEnumerable<ItemRestaurante>> ObtenerItemsDeRestaurante()
        {
            try
            {
                var _asyncDb = new SigescomEntities();
                var items = (await _asyncDb.Concepto_negocio_rol.Where(cnr => cnr.id_rol == RestauranteSettings.Default.IdRolConceptoRestaurante).Select(cnr => cnr.Concepto_negocio).Where(cn => cn.es_vigente).Include(cn => cn.Detalle_maestro4).ToListAsync())
                    .Select(cn => new ItemRestaurante()
                    {
                        Id = cn.id,
                        CodigoBarra = cn.codigo_barra,
                        Nombre = cn.nombre,
                        IdFamilia = cn.id_concepto_basico,
                        NombreFamilia = cn.Detalle_maestro4.nombre
                    });
                return items;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<ItemJerarquico>> ObtenerDetallesJerarquicos(int idMaestro)
        {
            try
            {
                var _dbAsync = new SigescomEntities();
                return (await _dbAsync.Detalle_detalle_maestro.Where(m => m.Detalle_maestro.id_maestro == idMaestro).ToListAsync()).Select(ddm => new ItemJerarquico { IdPadre = ddm.id_detalle_maestro_principal, Id = ddm.id_detalle_maestro_secundario });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<ItemRestaurante> ObtenerItemsDeRestaurantePorCategorias(List<int> idsCategoria)
        {
            int[] IdsFamilias = _db.Categoria_concepto.Where(cc => idsCategoria.Contains(cc.id_categoria)).Select(cc => cc.id_concepto).ToArray();
            IEnumerable<ItemRestaurante> items = _db.Concepto_negocio_rol.Where(cnr => cnr.id_rol == RestauranteSettings.Default.IdRolConceptoRestaurante).Select(cnr => cnr.Concepto_negocio).Where(cn => IdsFamilias.Contains(cn.id_concepto_basico)).Select(cn => new ItemRestaurante()
            {
                Id = cn.id,
                CodigoBarra = cn.codigo_barra,
                Nombre = cn.nombre,
                IdFamilia = cn.id_concepto_basico,
                NombreFamilia = cn.Detalle_maestro4.nombre
            });
            return items;
        }
        public long[] ObtenerIdsDeItemsDeRestaurantePorCategoria(int idCategoria)
        {
            int[] IdsFamilias = _db.Categoria_concepto.Where(cc => cc.id_categoria == idCategoria).Select(cc => cc.id_concepto).ToArray();
            long[] Ids;

            Ids = _db.Concepto_negocio.Where(cn => IdsFamilias.Contains(cn.id_concepto_basico)).Select(cn => (long)cn.id).ToArray();

            return Ids;
        }

        /// <summary>
        /// es necesario que tenga precio
        /// </summary>
        /// <param name="IdItemDeRestaurante"></param>
        /// <returns></returns>
        public ItemRestaurante ObtenerItemDeRestauranteConPrecio(int IdItemDeRestaurante)
        {
            try
            {

                Concepto_negocio conceptoNegocio = _db.Concepto_negocio
                    .Include(cn => cn.Precio1)
                    .Include(cn => cn.Detalle_maestro4)
                    .Include(cn => cn.Detalle_maestro4.Caracteristica_concepto)
                    .FirstOrDefault(cn => cn.id == IdItemDeRestaurante);
                if (conceptoNegocio.Precio1.Count < 1)
                {
                    throw new PreciosNotFoundException(conceptoNegocio.nombre);
                }
                return new ItemRestaurante(conceptoNegocio)
                {
                    Precio = conceptoNegocio.Precio1.Where(p => p.es_vigente && p.id_tarifa_d == RestauranteSettings.Default.IdTarifaSeleccionadaEnRestaurante).First().valor
                };
            }
            catch (Exception e)
            {
                throw new DatosException("Error al intentar obtener concepto de negocio", e);
            }
        }

        public IEnumerable<Concepto_negocio> ObtenerConceptoDeNegocioIncluyendoDetallesDeTransaccion()
        {
            try
            {
                var idRolConceptoRestaurante = RestauranteSettings.Default.IdRolConceptoRestaurante;
                IEnumerable<Concepto_negocio> conceptos = _db.Concepto_negocio_rol.Where(cnr => cnr.id_rol == idRolConceptoRestaurante).Select(cnr => cnr.Concepto_negocio).Where(cn => cn.es_vigente);
                return conceptos;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public IEnumerable<ResumenOrden_Consulta> ObtenerReporteOrdenesIncluyendoMozo(DateTime Desde, DateTime Hasta)
        {
            try
            {
                int tipoTransaccion = RestauranteSettings.Default.IdTipoTransaccionOrdenDeRestaurante;
                IEnumerable<ResumenOrden_Consulta> InfoReporte = _db.Transaccion.Where(t => t.id_tipo_transaccion == tipoTransaccion && t.fecha_inicio >= Desde && t.fecha_inicio <= Hasta).Select(t => new ResumenOrden_Consulta()
                {
                    Id = t.id,
                    Codigo = t.codigo,
                    Importe = t.importe_total,
                    Mozo = t.Actor_negocio2.Actor.primer_nombre,
                    Fecha = t.fecha_inicio
                });
                var resultadoFinal = InfoReporte.ToList();
                resultadoFinal.ForEach(m =>
                {
                    var nombreArray = m.Mozo.Split('|');
                    m.Mozo = nombreArray[2] + " " + nombreArray[0] + " " + nombreArray[1];
                });
                return resultadoFinal;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<ResumenOrdenesPorMozo_Consulta> ObtenerReporteMozoIncluyendoCantidadDeOrdenes(DateTime Desde, DateTime Hasta)
        {
            try
            {
                int tipoTransaccion = RestauranteSettings.Default.IdTipoTransaccionOrdenDeRestaurante;
                IEnumerable<ResumenOrdenesPorMozo_Consulta> InfoReporte = _db.Transaccion.Where(t => t.fecha_inicio >= Desde && t.fecha_inicio <= Hasta && t.id_tipo_transaccion == tipoTransaccion).GroupBy(t => new { t.id_empleado, t.Actor_negocio2.Actor.primer_nombre }).Select(ta => new ResumenOrdenesPorMozo_Consulta()
                {
                    Id = ta.Key.id_empleado,
                    Mozo = ta.Key.primer_nombre,
                    Cantidad = ta.Count(),
                    Importe = ta.Sum(t => t.importe_total)
                }); ;

                var info = InfoReporte.ToList();

                info.ForEach(m =>
                {
                    var nombreArray = m.Mozo.Split('|');
                    m.Mozo = nombreArray[2] + " " + nombreArray[0] + " " + nombreArray[1];
                });
                return info;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public IEnumerable<ItemRestaurante_Consulta> ObtenerOrdenesInclusiveItemsYDetallesDeOrden(DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                int tipo = RestauranteSettings.Default.IdTipoTransaccionOrdenDeRestaurante;
                var estadoDeDetalle = (int)EstadoDeDetalleDeOrden.Atendido;
                var Resumen = _db.Transaccion
                 .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && t.id_tipo_transaccion == tipo)
                 .SelectMany(t => t.Detalle_transaccion).Where(dt => dt.indicadorMultiproposito == estadoDeDetalle)
                 .GroupBy(dt => new
                 {
                     codigoConcepto = dt.Concepto_negocio.codigo,
                     nombreConcepto = dt.Concepto_negocio.nombre,
                     idConcepto = dt.id_concepto_negocio,
                     precioReferencial = dt.precio_unitario
                 }).Select(dt => new ItemRestaurante_Consulta()
                 {

                     Codigo = dt.Key.codigoConcepto,
                     NombreItem = dt.Key.nombreConcepto,
                     Monto = dt.Sum(a => a.total),
                     Cantidad = dt.Sum(a => a.cantidad),
                     Precio = dt.Key.precioReferencial
                 });
                return Resumen;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public IEnumerable<DetalleOrden_Consulta> ObtenerOrdenesDetalladasIncluyendoMozoyDetalleDeOrden(DateTime fechaDesde, DateTime fechaHasta, EstadoDeDetalleDeOrden estadoDeDetalle)
        {
            try
            {
                int tipo = RestauranteSettings.Default.IdTipoTransaccionOrdenDeRestaurante;
                var estadoDetalle = (int)estadoDeDetalle;
                var Resultado = _db.Transaccion
                 .Where(t => t.fecha_inicio >= fechaDesde && t.fecha_inicio <= fechaHasta && t.id_tipo_transaccion == tipo)
                 .SelectMany(t => t.Detalle_transaccion).Where(dt => dt.indicadorMultiproposito == estadoDetalle)
                 .Select(dt => new DetalleOrden_Consulta()
                 {
                     Mozo = dt.Transaccion.Actor_negocio2.Actor.primer_nombre,
                     Cantidad = dt.cantidad,
                     Codigo = dt.Transaccion.codigo,
                     Monto = dt.total,
                     NombreItem = dt.Concepto_negocio.nombre,
                     Precio = dt.precio_unitario,
                     FechaHora = dt.Transaccion.fecha_inicio,
                     Mesa = dt.Transaccion.Transaccion2.Actor_negocio4.Actor.primer_nombre
                 }).ToList();
                var grupo = Resultado.GroupBy(dor => new { dor.Codigo, dor.NombreItem, dor.Mozo, dor.Mesa, dor.FechaHora, dor.Precio });
                Resultado = grupo.Select(dor => new DetalleOrden_Consulta()
                {
                    Codigo = dor.Key.Codigo,
                    Mozo = dor.Key.Mozo,
                    Mesa = dor.Key.Mesa,
                    NombreItem = dor.Key.NombreItem,
                    FechaHora = dor.Key.FechaHora,
                    Cantidad = dor.Sum(g => g.Cantidad),
                    Precio = dor.Key.Precio,
                    Monto = dor.Sum(g => g.Monto),
                }).ToList();

                var resultadoFinal = Resultado.ToList();
                resultadoFinal.ForEach(m =>
                {
                    var nombreArray = m.Mozo.Split('|');
                    m.Mozo = nombreArray[2] + " " + nombreArray[0] + " " + nombreArray[1];
                });
                return resultadoFinal;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<OrdenAtencion_Consulta> ObtenerOrdenesAtencion(DateTime desde, DateTime hasta, int idEstablecimiento)
        {
            try
            {
                IEnumerable<OrdenAtencion_Consulta> ordenes = _db.Transaccion.Where(t => t.fecha_inicio >= desde && t.fecha_inicio <= hasta && t.id_tipo_transaccion == RestauranteSettings.Default.IdTipoTransaccionOrdenDeRestaurante && t.Transaccion2.Actor_negocio4.Actor_negocio2.id_actor_negocio_padre == idEstablecimiento).Select(t => new OrdenAtencion_Consulta()
                {
                    IdAtencion = t.Transaccion2.id,
                    FechaHoraAtencion = t.Transaccion2.fecha_inicio,
                    Mesa = t.Transaccion2.Actor_negocio4.Actor.primer_nombre,
                    Mozo = t.Actor_negocio2.Actor.primer_nombre,
                    CodigoOrden = t.codigo,
                    Importe = t.importe_total,
                    EsAtencionEnSalon = !t.Transaccion2.indicador1,
                    Facturado = t.Transaccion2.Evento_transaccion.Select(ev => ev.id_evento).Contains(MaestroSettings.Default.IdDetalleMaestroEstadoFacturado)
                });
                var resultadoFinal = ordenes.ToList();
                resultadoFinal.ForEach(m =>
                {
                    var nombreArray = m.Mozo.Split('|');
                    m.Mozo = nombreArray[2] + " " + nombreArray[0] + " " + nombreArray[1];
                });
                return resultadoFinal;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener ordenes de atención", e);
            }
        }
        public IEnumerable<OrdenPorModoAtencion_Consulta> ObtenerOrdenesPorModoAtencion(DateTime desde, DateTime hasta, int idEstablecimiento)
        {
            try
            {
                IEnumerable<OrdenPorModoAtencion_Consulta> ordenes = _db.Transaccion.Where(t => t.fecha_inicio >= desde && t.fecha_inicio <= hasta && t.id_tipo_transaccion == RestauranteSettings.Default.IdTipoTransaccionOrdenDeRestaurante && t.Transaccion2.Actor_negocio4.Actor_negocio2.id_actor_negocio_padre == idEstablecimiento).Select(t => new OrdenPorModoAtencion_Consulta()
                {
                    IdAtencion = t.Transaccion2.id,
                    FechaHoraAtencion = t.Transaccion2.fecha_inicio,
                    Mesa = t.Transaccion2.Actor_negocio4.Actor.primer_nombre,
                    Mozo = t.Actor_negocio2.Actor.primer_nombre,
                    CodigoOrden = t.codigo,
                    Importe = t.importe_total,
                    EsAtencionEnSalon = !t.Transaccion2.indicador1,
                    EsAtencionPorDelivery = t.Transaccion2.indicador2,
                    Ambiente = t.Transaccion2.Actor_negocio4.Actor_negocio2.Actor.primer_nombre,
                    Facturado = t.Transaccion2.Evento_transaccion.Select(ev => ev.id_evento).Contains(MaestroSettings.Default.IdDetalleMaestroEstadoFacturado)
                });
                var resultadoFinal = ordenes.ToList();
                resultadoFinal.ForEach(m =>
                {
                    var nombreArray = m.Mozo.Split('|');
                    m.Mozo = nombreArray[2] + " " + nombreArray[0] + " " + nombreArray[1];
                });
                return resultadoFinal;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener ordenes de atención", e);
            }
        }
        public IEnumerable<ResumenAtencion> ObtenerResumenDeAtencionesPorEstado(DateTime fechaDesde, DateTime facheHasta, int idEstado, int[] IdsCentrosDeAtencion)
        {
            try
            {
                IEnumerable<ResumenAtencion> atenciones = _db.Estado_transaccion.Where(et => et.id_estado == idEstado && et.fecha >= fechaDesde && et.fecha <= facheHasta).Select(et => et.Transaccion).Where(t => t.id_tipo_transaccion == RestauranteSettings.Default.IdTipoTransaccionAtencionDeRestaurante && IdsCentrosDeAtencion.Contains(t.id_actor_negocio_interno)).Select(t => new ResumenAtencion()
                {
                    Id = t.id,
                    FechaAtencion = t.fecha_inicio,
                    NombreAmbiente = t.Actor_negocio4.Actor_negocio2.Actor.primer_nombre,
                    NombreMozo = t.Estado_transaccion.FirstOrDefault(et => et.id_estado == MaestroSettings.Default.IdDetalleMaestroEstadoCerrado).Actor_negocio.Actor.tercer_nombre,
                    IdMesa = t.Actor_negocio4.id,
                    NombreMesa = t.Actor_negocio4.Actor.primer_nombre,
                    ImporteTotal = t.importe_total,
                    Facturado = t.Evento_transaccion.Select(ev => ev.id_evento).Contains(MaestroSettings.Default.IdDetalleMaestroEstadoFacturado),
                    Confirmado = t.Evento_transaccion.Select(ev => ev.id_evento).Contains(MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                });
                var resultado = atenciones.ToList();
                //resultado.ForEach(m => { var nombreArray = m.NombreMozo.Split('|'); m.NombreMozo = nombreArray[2] + " " + nombreArray[0] + " " + nombreArray[1]; });
                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public AtencionRestaurante ObtenerAtencionEspecifica(long id)
        {
            try
            {
                Transaccion transaccion = _db.Transaccion
                    .Include(t => t.Transaccion1)
                    .Include(t => t.Actor_negocio1.Actor)
                    .Include(t => t.Transaccion1.Select(to => to.Detalle_transaccion))
                    .Include(t => t.Transaccion1.Select(to => to.Detalle_transaccion.Select(dt => dt.Concepto_negocio)))
                    .FirstOrDefault(t => t.id == id);

                AtencionRestaurante atencion = AtencionRestaurante.Convert(transaccion);

                return atencion;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<long> ObtenerIdsTransaccionDeTransaccionReferencia(long idTransaccion)
        {
            try
            {
                var result = _db.Transaccion.Where(t => t.id_transaccion_referencia == idTransaccion).Select(t => t.id);
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public OperationResult ActualizarDetalleDeDetalleTransaccion(long idDetalle, string detalleString)
        {
            try
            {
                var detalle_transaccion = _db.Detalle_transaccion.FirstOrDefault(t => t.id == idDetalle);
                detalle_transaccion.detalle = detalleString;
                var result = Save();
                return result;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al calcular atención", e);

            }
        }

    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Tsp.Sigescom.AccesoDatos.Generico;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Restaurant;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;
using Tsp.Sigescom.Modelo.SigesRestaurant;

namespace Tsp.Sigescom.AccesoDatos.Restaurant
{
    public partial class Atencion_Datos : IAtencion_Repositorio
    {

        public OperationResult GuardarCambioDeMesa(AtencionRestaurante atencion, Mesa nuevaMesa)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                var atencionDb = _db.Transaccion.FirstOrDefault(t => t.id == atencion.Id);
                atencionDb.id_actor_negocio_interno1 = nuevaMesa.Id;
                var mesaAnteriorDb = _db.Actor_negocio.FirstOrDefault(an => an.id == atencion.Mesa.Id);
                mesaAnteriorDb.indicador1 = false;//desocupada
                var mesaNuevaDb = _db.Actor_negocio.FirstOrDefault(an => an.id == nuevaMesa.Id);
                mesaNuevaDb.indicador1 = true;//ocupada

                _db.SaveChanges();
                return new OperationResult(OperationResultEnum.Success);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al guardar datos: Cambio de mesa", e);
            }
        }

        public AtencionRestaurante ObtenerAtencionDeMesaOcupada(int idMesa)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                var tipoTransaccionOrden = RestauranteSettings.Default.IdTipoTransaccionOrdenDeRestaurante;
                AtencionRestaurante atencion = _db.Transaccion
                    .Where(t => t.id_actor_negocio_interno1 == idMesa && (!t.Evento_transaccion.Select(tt => tt.id_evento).Contains(MaestroSettings.Default.IdDetalleMaestroEstadoFacturado) && !(t.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoCerrado && t.importe_total == 0))).Select(transaccion => new AtencionRestaurante()
                    {
                        Id = transaccion.id,
                        ImporteAtencion = transaccion.importe_total,
                        Mesa = new Mesa()
                        {
                            Id = (int)transaccion.id_actor_negocio_interno1,
                            IdAmbiente = (int)transaccion.Actor_negocio4.id_actor_negocio_padre,
                            JsonData = transaccion.Actor_negocio4.extension_json
                        },
                        TipoDePago = transaccion.enum1,
                        Ordenes = transaccion.Transaccion1.Where(to => to.id_tipo_transaccion == tipoTransaccionOrden).Select(to => new Orden_Atencion()
                        {
                            Id = to.id,
                            Codigo = to.codigo,
                            Estado = (int)to.id_estado_actual,
                            IdAtencion = (long)to.id_transaccion_padre,
                            ImporteOrden = to.importe_total,
                            IdMesa = (int)transaccion.id_actor_negocio_interno1,
                            Mozo = new ItemGenerico()
                            {
                                Id = to.id_actor_negocio_interno,
                                Nombre = to.Actor_negocio2.Actor.tercer_nombre,
                            },
                            FechaHoraRegistro = to.fecha_inicio,
                            DetallesDeOrden = to.Detalle_transaccion.Select(dt => new DetalleOrden()
                            {
                                Id = dt.id,
                                Precio = dt.precio_unitario,
                                Cantidad = dt.cantidad,
                                Importe = dt.total,
                                Estado = (int)dt.indicadorMultiproposito,
                                EsBien = dt.Concepto_negocio.Detalle_maestro4.valor == "1",
                                DetalleItemRestauranteJson = dt.detalle,
                                NombreItem = dt.Concepto_negocio.nombre,
                                IdItem = dt.id_concepto_negocio,
                                IdTransaccion = dt.id_transaccion
                            })
                        }),
                        Estado = (int)transaccion.id_estado_actual,
                        EsAtencionConMesa = !transaccion.indicador1 ,
                        EsAtencionPorDelivery = transaccion.indicador2
                    }).FirstOrDefault();

                Data_Mesa dataMesa = JsonConvert.DeserializeObject<Data_Mesa>(atencion.Mesa.JsonData);
                atencion.Mesa.Nombre = dataMesa.nombre;
                atencion.Ordenes.ToList().ForEach(o => o.NombreMesa = dataMesa.nombre);
                return atencion;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener datos de atención", e);
            }
        }

        private void AgregarEstadoCerradoAAtencion(SigescomEntities _db, long idAtencionACerrar, int idEmpleado, string comentario)
        {
            Estado_transaccion estadoCerradoAtencion = new Estado_transaccion()
            {
                id_transaccion = idAtencionACerrar,
                id_estado = MaestroSettings.Default.IdDetalleMaestroEstadoCerrado,
                fecha = DateTimeUtil.FechaActual(),
                id_empleado = idEmpleado,
                comentario = comentario
            };
            _db.Estado_transaccion.Add(estadoCerradoAtencion);
        }

        private void AgregarEstadoCerradoAOrdenes(SigescomEntities _db, long[] idOrdenesACerrar, int idEmpleado, string comentario)
        {
            foreach (var idOrden in idOrdenesACerrar)
            {
                Estado_transaccion estadoCerradoOrden = new Estado_transaccion()
                {
                    id_transaccion = idOrden,
                    id_estado = MaestroSettings.Default.IdDetalleMaestroEstadoCerrado,
                    fecha = DateTimeUtil.FechaActual(),
                    id_empleado = idEmpleado,
                    comentario = comentario
                };
                _db.Estado_transaccion.Add(estadoCerradoOrden);
            }
        }
        private void AtenderDetallesDeOrden(SigescomEntities _db, long[] idDetallesAAtender)
        {
            var detalles = _db.Detalle_transaccion.Where(dt => idDetallesAAtender.Contains(dt.id)).ToList();

            detalles.ForEach(d => d.indicadorMultiproposito = (int)EstadoDeDetalleDeOrden.Atendido);
            foreach (var detalle in detalles)
            {
                detalle.indicadorMultiproposito = (int)EstadoDeDetalleDeOrden.Atendido;
                _db.Entry(detalle).State = EntityState.Modified;
            }
        }
        private void LiberarMesa(SigescomEntities _db, int idMesa)
        {
            var mesa = _db.Actor_negocio.FirstOrDefault(an => an.id == idMesa);
            mesa.indicador1 = false;
            _db.Entry(mesa).State = EntityState.Modified;

        }
        public OperationResult CerrarAtencionYOrdenesYAtenderDetalles(long idAtencionACerrar, long[] idsOrdenesACerrar, long[] idsDetallesAAtender, int idMesa, bool liberarMesa, int idEmpleado)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                AgregarEstadoCerradoAAtencion(_db, idAtencionACerrar, idEmpleado, "Cierre rápido de atención");
                AgregarEstadoCerradoAOrdenes(_db, idsOrdenesACerrar, idEmpleado, "Cierre de orden debido a cierre rápido de atención");
                AtenderDetallesDeOrden(_db, idsDetallesAAtender);
                if (liberarMesa)
                {
                    LiberarMesa(_db, idMesa);
                }

                _db.SaveChanges();
                return new OperationResult(OperationResultEnum.Success);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener datos de atención", e);
            }
        }

        public AtencionRestaurante ObtenerAtencionConDatosMinimosDeOrdenYDetallesSoloParaCerrarla(long idAtencion)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                var tipoTransaccionOrden = RestauranteSettings.Default.IdTipoTransaccionOrdenDeRestaurante;
                AtencionRestaurante atencion = _db.Transaccion
                    .Where(t => t.id == idAtencion).Select(transaccion => new AtencionRestaurante()
                    {
                        Id = transaccion.id,
                        ImporteAtencion = transaccion.importe_total,
                        TipoDePago = transaccion.enum1,
                        Mesa = new Mesa()
                        {
                            Id = (int)transaccion.id_actor_negocio_interno1
                        },
                        Ordenes = transaccion.Transaccion1.Where(to => to.id_tipo_transaccion == tipoTransaccionOrden).Select(to => new Orden_Atencion()
                        {
                            Id = to.id,
                            Estado = (int)to.id_estado_actual,
                            ImporteOrden = to.importe_total,
                            DetallesDeOrden = to.Detalle_transaccion.Select(dt => new DetalleOrden()
                            {
                                Id = dt.id,
                                Estado = (int)dt.indicadorMultiproposito,
                            })
                        }),
                        Estado = (int)transaccion.id_estado_actual,
                        EsAtencionConMesa = !transaccion.indicador1,
                        EsAtencionPorDelivery = transaccion.indicador2,
                        Confirmado = transaccion.Evento_transaccion.Select(ev => ev.id_evento).Contains(MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                    }).FirstOrDefault();
                return atencion;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener datos de atención", e);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Entidades.Sesion;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Almacen;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Compras;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Transacciones;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;
using Tsp.Sigescom.Modelo.Negocio.Almacen;

namespace Tsp.Sigescom.Logica.Core.Almacen
{
    public class OrdenAlmacen_Logica : IOrdenAlmacen_Logica
    {
        protected readonly IOrdenAlmacen_Repositorio _ordenAlmacenDatos;
        protected readonly ICentroDeAtencion_Logica _centroDeAtencionLogica;
        protected readonly IActorNegocioLogica _actorNegocioLogica;
        protected readonly IOperacionLogica _operacionLogica;
        protected readonly ITransaccionRepositorio _transaccionRepositorio;

        public OrdenAlmacen_Logica(IOrdenAlmacen_Repositorio ordenAlmacenDatos, ICentroDeAtencion_Logica centroDeAtencionLogica, IActorNegocioLogica actorNegocioLogica, IOperacionLogica operacionLogica, ITransaccionRepositorio transaccionRepositorio)
        {
            _ordenAlmacenDatos = ordenAlmacenDatos;
            _centroDeAtencionLogica = centroDeAtencionLogica;
            _actorNegocioLogica = actorNegocioLogica;
            _operacionLogica = operacionLogica;
            _transaccionRepositorio = transaccionRepositorio;
        }

        public PrincipalOrdenAlmacenData ObtenerDatosParaOrdenAlmacenPrincipal(UserProfileSessionData profileData)
        {
            var tieneRolAdministradorDeNegocio = profileData.Empleado.TieneRol(ActorSettings.Default.idRolAdministradorDeNegocio);

            var data = new PrincipalOrdenAlmacenData()
            {
                FechaActual = DateTimeUtil.FechaActual(),
                EsAdministrador = tieneRolAdministradorDeNegocio,
                Almacenes = tieneRolAdministradorDeNegocio ? ItemGenerico.ConvertirCentroDeAtencionConEstablecimientoComercial(_centroDeAtencionLogica.ObtenerAlmacenesVigentes().ToList()) : new List<ItemGenerico>() { profileData.CentroDeAtencionSeleccionado.ToItemGenerico() },
                AlmacenSesion = profileData.CentroDeAtencionSeleccionado.ToItemGenerico(),
            };
            return data;
        }

        public List<OrdenAlmacenResumen> ObtenerOrdenesAlmacen(DateTime fechaDesde, DateTime fechaHasta, bool porIngresar, bool entregaInmediata, bool entregaDiferida, bool estadoPendiente, bool estadoParcial, bool estadoCompletada, int[] idsAlmacenes, UserProfileSessionData profileData)
        {
            try
            {
                List<int> idsModoEntrega = new List<int>();
                if (entregaInmediata) idsModoEntrega.Add((int)IndicadorImpactoAlmacen.Inmediata);
                if (entregaDiferida) idsModoEntrega.Add((int)IndicadorImpactoAlmacen.Diferida);
                List<int> idsEstado = new List<int>();
                if (estadoPendiente) idsEstado.Add(MaestroSettings.Default.IdDetalleMaestroEstadoPendiente);
                if (estadoParcial) idsEstado.Add(MaestroSettings.Default.IdDetalleMaestroEstadoParcial);
                if (estadoCompletada) idsEstado.Add(MaestroSettings.Default.IdDetalleMaestroEstadoCompletada);
                var ordenesAlmacen = _ordenAlmacenDatos.ObtenerOrdenesAlmacen(fechaDesde, fechaHasta, porIngresar, idsModoEntrega.ToArray(), idsEstado.ToArray(), idsAlmacenes).ToList();
                ordenesAlmacen.AddRange(_ordenAlmacenDatos.ObtenerOrdenesAlmacenBidireccional(fechaDesde, fechaHasta, porIngresar, idsModoEntrega.ToArray(), idsEstado.ToArray(), idsAlmacenes).ToList());
                return ordenesAlmacen;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener las ordenes de almacen", e);
            }
        }

        public OrdenAlmacen ObtenerOrdenAlmacen(long idOrdenAlmacen)
        {
            try
            {
                var ordenAlmacen = _ordenAlmacenDatos.ObtenerOrdenAlmacen(idOrdenAlmacen);
                ordenAlmacen.IdsOrdenes.Insert(0, ordenAlmacen.Id);
                ordenAlmacen.Ordenes = ObtenerOrdenesDeOrdenAlmacen(ordenAlmacen.IdsOrdenes.ToArray());
                ordenAlmacen.Movimientos = ObtenerMovimientosDeOrdenAlmacen(ordenAlmacen.IdsOrdenes.ToArray());
                var detallesNotas = _ordenAlmacenDatos.ObtenerNotasCreditoDeOrdenAlmacen(ordenAlmacen.Id).SelectMany(n => n.Detalles).ToList();
                var detallesMovimientos = ordenAlmacen.Movimientos.Where(m => m.EsVigente).SelectMany(m => m.Detalles).ToList();
                foreach (var detalle in ordenAlmacen.Detalles)
                {
                    detalle.Entregado = detallesMovimientos.Where(dmo => dmo.IdConcepto == detalle.IdConcepto).Sum(d => d.Cantidad);
                    detalle.Pendiente = detalle.Ordenado - detalle.Revocado - detalle.Entregado;
                    detalle.Devuelto = detallesNotas.Where(dmo => dmo.IdConcepto == detalle.IdConcepto).Sum(d => d.Cantidad);
                }
                return ordenAlmacen;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener la orden de almacen", e);
            }
        }

        public OrdenAlmacen ObtenerOrdenAlmacen(long idOrdenAlmacen, bool porIngresar)
        {
            try
            {
                var ordenAlmacenBidireccional = _ordenAlmacenDatos.VerificarOrdenAlmacenBidireccional(idOrdenAlmacen);
                var ordenAlmacen = ordenAlmacenBidireccional ? _ordenAlmacenDatos.ObtenerOrdenAlmacenBireccional(idOrdenAlmacen, porIngresar) : _ordenAlmacenDatos.ObtenerOrdenAlmacen(idOrdenAlmacen);
                ordenAlmacen.IdsOrdenes.Insert(0, ordenAlmacen.Id);
                ordenAlmacen.Ordenes = ObtenerOrdenesDeOrdenAlmacen(ordenAlmacen.IdsOrdenes.ToArray());
                ordenAlmacen.Movimientos = ObtenerMovimientosDeOrdenAlmacen(ordenAlmacen.IdsOrdenes.ToArray(), porIngresar);
                var detallesMovimientos = ordenAlmacen.Movimientos.Where(m => m.EsVigente).SelectMany(m => m.Detalles).ToList();
                foreach (var detalle in ordenAlmacen.Detalles)
                {
                    detalle.Entregado = detallesMovimientos.Where(dmo => dmo.IdConcepto == detalle.IdConcepto).Sum(d => d.Cantidad);
                    detalle.Pendiente = detalle.Ordenado - detalle.Revocado - detalle.Entregado;
                }
                return ordenAlmacen;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener la orden de almacen", e);
            }
        }

        public List<OrdenDeOrdenAlmacen> ObtenerOrdenesDeOrdenAlmacen(long[] idsOrdenes)
        {
            try
            {
                var ordenesDeOrdenAlmacen = _ordenAlmacenDatos.ObtenerOrdenesDeOrdenAlmacen(idsOrdenes).ToList();
                ordenesDeOrdenAlmacen.ForEach(o => o.Comprobante = new ComprobanteDeAlmacen() { Id = o.Id });
                return ordenesDeOrdenAlmacen;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener las ordenes de orden de almacen", e);
            }
        }
        public List<MovimientoDeOrdenAlmacen> ObtenerMovimientosDeOrdenAlmacen(long[] idsOrdenes)
        {
            try
            {
                var movimientosDeOrdenAlmacen = _ordenAlmacenDatos.ObtenerMovimientosDeOrdenAlmacen(idsOrdenes).ToList();
                movimientosDeOrdenAlmacen.ForEach(o => o.Comprobante = new ComprobanteDeAlmacen() { Id = o.Id });
                return movimientosDeOrdenAlmacen;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener las movimientos de orden de almacen", e);
            }
        }
        public List<MovimientoDeOrdenAlmacen> ObtenerMovimientosDeOrdenAlmacen(long[] idsOrdenes, bool porIngresar)
        {
            try
            {
                var movimientosDeOrdenAlmacen = _ordenAlmacenDatos.ObtenerMovimientosDeOrdenAlmacen(idsOrdenes, porIngresar).ToList();
                movimientosDeOrdenAlmacen.ForEach(o => o.Comprobante = new ComprobanteDeAlmacen() { Id = o.Id });
                return movimientosDeOrdenAlmacen;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener las movimientos de orden de almacen", e);
            }
        }
        public List<MovimientoDeOrdenAlmacen> ObtenerMovimientosConfirmadosDeOrdenAlmacen(long[] idsOrdenes)
        {
            try
            {
                var movimientosDeOrdenAlmacen = _ordenAlmacenDatos.ObtenerMovimientosConfirmadosDeOrdenAlmacen(idsOrdenes).ToList();
                movimientosDeOrdenAlmacen.ForEach(o => o.Comprobante = new ComprobanteDeAlmacen() { Id = o.Id });
                return movimientosDeOrdenAlmacen;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener las movimientos de orden de almacen", e);
            }
        }
        public RegistroMovimientoAlmacen ObtenerRegistroMovimientoOrdenAlmacen(long idOrdenAlmacen, bool porIngresar, UserProfileSessionData profileData)
        {
            try
            {
                var registroMovimientoOrdenAlmacen = new RegistroMovimientoAlmacen();
                var ordenAlmacen = ObtenerOrdenAlmacen(idOrdenAlmacen);
                var detallesStockActual = _ordenAlmacenDatos.ObtenerStockActualDetallesOrdenAlmacen(ordenAlmacen.Detalles.Select(d => d.IdConcepto).ToArray(), ordenAlmacen.IdAlmacen);
                foreach (var detalle in ordenAlmacen.Detalles)
                {
                    registroMovimientoOrdenAlmacen.Detalles.Add(new DetalleMovimientoDeAlmacen
                    {
                        IdProducto = detalle.IdConcepto,
                        Descripcion = detalle.Concepto,
                        StockActual = detallesStockActual.Single(d => d.IdConcepto == detalle.IdConcepto).StockActual,
                        Pendiente = detalle.Pendiente,
                        IngresoSalidaActual = detalle.Pendiente
                    });
                }
                registroMovimientoOrdenAlmacen.IdOrdenDeAlmacen = ordenAlmacen.Id;
                registroMovimientoOrdenAlmacen.Tercero.Id = ordenAlmacen.IdOrigenDestino;
                registroMovimientoOrdenAlmacen.UbigeoOrigen = porIngresar ? _actorNegocioLogica.ObtenerUbigeoDireccionActorComercial(registroMovimientoOrdenAlmacen.Tercero.Id) : profileData.Sede.DomicilioFiscal.Ubigeo;
                registroMovimientoOrdenAlmacen.DireccionOrigen = porIngresar ? _actorNegocioLogica.ObtenerDetalleDireccionActorComercial(registroMovimientoOrdenAlmacen.Tercero.Id) : profileData.Sede.DomicilioFiscal.Detalle;
                registroMovimientoOrdenAlmacen.UbigeoDestino = porIngresar ? profileData.Sede.DomicilioFiscal.Ubigeo : _actorNegocioLogica.ObtenerUbigeoDireccionActorComercial(registroMovimientoOrdenAlmacen.Tercero.Id);
                registroMovimientoOrdenAlmacen.DireccionDestino = porIngresar ? profileData.Sede.DomicilioFiscal.Detalle : _actorNegocioLogica.ObtenerDetalleDireccionActorComercial(registroMovimientoOrdenAlmacen.Tercero.Id);
                return registroMovimientoOrdenAlmacen;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener la orden de almacen", e);
            }
        }

        public OperationResult GuardarMovimientoOrdenAlmacen(RegistroMovimientoAlmacen movimientoOrdenAlmacen, UserProfileSessionData sesionUsuario)
        {
            try
            {
                return _operacionLogica.GuardarMovimientoOrdenAlmacen(movimientoOrdenAlmacen, sesionUsuario);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar guardar el movimiento en el orden de almacen", e);
            }
        }

        public OperationResult InvalidarMovimientoOrdenAlmacen(long idMovimientoOrdenAlmacen, string observacion, UserProfileSessionData sesionUsuario)
        {
            try
            {
                var fechaActual = DateTimeUtil.FechaActual();
                var ordenAlmacen = _ordenAlmacenDatos.ObtenerOrdenAlmacenConIdMovimientoOrdenAlmacen(idMovimientoOrdenAlmacen);
                var movimientos = ObtenerMovimientosConfirmadosDeOrdenAlmacen(new long[] { ordenAlmacen.Id });
                var estadosTransacciones = new List<Estado_transaccion>();
                if (movimientos.Where(m => m.EsVigente).Count() > 1)
                {
                    if (!ordenAlmacen.EstaParcial)
                    {
                        estadosTransacciones.Add(new Estado_transaccion(ordenAlmacen.Id, sesionUsuario.Empleado.Id, MaestroSettings.Default.IdDetalleMaestroEstadoParcial, fechaActual, "Estado entrega parcial asignado al invalidar un movimiento de la orden de almacen"));
                    }
                }
                else
                {
                    if (!ordenAlmacen.EstaPendiente)
                    {
                        estadosTransacciones.Add(new Estado_transaccion(ordenAlmacen.Id, sesionUsuario.Empleado.Id, MaestroSettings.Default.IdDetalleMaestroEstadoPendiente, fechaActual, "Estado pendiente asignado al invalidar un movimiento de la orden de almacen"));
                    }
                }
                var movimientoOrdenAlmacenAInvalidar = _transaccionRepositorio.ObtenerTransaccionInclusiveEstadoTransaccionYDetalleTransaccion(idMovimientoOrdenAlmacen);
                movimientoOrdenAlmacenAInvalidar.id_tipo_transaccion = movimientoOrdenAlmacenAInvalidar.Tipo_transaccion.Accion_de_negocio_por_tipo_transaccion.First().valor ? TransaccionSettings.Default.IdTipoTransaccionSalidaBienesAjusteInventario : TransaccionSettings.Default.IdTipoTransaccionEntradaBienesAjusteInventario;
                estadosTransacciones.Add(new Estado_transaccion(movimientoOrdenAlmacenAInvalidar.id, sesionUsuario.Empleado.Id, MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado, fechaActual, observacion));
                return _operacionLogica.AfectarInventarioFisicoYGuardarInventarioEstadosTransacciones(movimientoOrdenAlmacenAInvalidar, estadosTransacciones, sesionUsuario);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar invalidar el movimiento de orden de almacen", e);
            }
        }
    }
}

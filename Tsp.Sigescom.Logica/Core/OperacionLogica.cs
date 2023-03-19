using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Entidades.Sesion;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Establecimientos;
using Tsp.Sigescom.Modelo.Interfaces.Datos.CentrosDeAtencion;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Actores;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Almacen;
using Tsp.Sigescom.Modelo.Negocio.Almacen;

namespace Tsp.Sigescom.Logica
{
    public partial class OperacionLogica : IOperacionLogica
    {
        private readonly ITransaccionRepositorio transaccionRepositorio;
        private readonly IMaestroRepositorio maestroRepositorio;
        private readonly IConceptoRepositorio conceptoRepositorio;
        private readonly IActorRepositorio actorRepositorio;
        private readonly IFacturacionRepositorio facturacionRepositorio;
        private readonly ICodigosOperacion_Logica codigosOperacion_Logica;
        private readonly IPermisos_Logica permisos_Logica;
        private readonly ICentroDeAtencion_Repositorio centroDeAtencionDatos;
        private readonly IActor_Repositorio actor_Datos;
        private readonly IInventarioHistorico_Repositorio inventarioHistorico_Datos;
        private readonly IOrdenAlmacen_Repositorio ordenAlmacen_Datos;


        public OperacionLogica()
        {
        }

        public OperacionLogica(ITransaccionRepositorio repositorioTransaccion, IMaestroRepositorio maestroRepositorio, IActorRepositorio actorRepositorio, IConceptoRepositorio conceptoRepositorio, IFacturacionRepositorio facturacionRepositorio, ICodigosOperacion_Logica codigosOperacion_Logica, IPermisos_Logica permisos_Logica, ICentroDeAtencion_Repositorio centroDeAtencionDatos, IActor_Repositorio actor_Datos, IInventarioHistorico_Repositorio inventarioHistorico_Datos, IOrdenAlmacen_Repositorio ordenAlmacen_Datos)
        {
            this.transaccionRepositorio = repositorioTransaccion;
            this.maestroRepositorio = maestroRepositorio;
            this.actorRepositorio = actorRepositorio;
            this.conceptoRepositorio = conceptoRepositorio;
            this.facturacionRepositorio = facturacionRepositorio;
            this.codigosOperacion_Logica = codigosOperacion_Logica;
            this.permisos_Logica = permisos_Logica;
            this.centroDeAtencionDatos = centroDeAtencionDatos;
            this.actor_Datos = actor_Datos;
            this.inventarioHistorico_Datos = inventarioHistorico_Datos;
            this.ordenAlmacen_Datos = ordenAlmacen_Datos;
        }



        public void ValidarRolPuntoDeCompra(int idPuntoDeCompra)
        {
            ValidarRol(idPuntoDeCompra, ActorSettings.Default.IdRolPuntoDeCompra, "Centro de atencion no tiene el rol punto de compra");
        }

        public void ValidarRolCaja(int idCaja)
        {
            ValidarRol(idCaja, ActorSettings.Default.IdRolCaja, "Centro de atencion no tiene el rol caja");
        }

        public void ValidarRolAlmacen(int idAlmacen)
        {
            ValidarRol(idAlmacen, ActorSettings.Default.IdRolAlmacen, "Centro de atencion no tiene el rol almacen");
        }
        //*****
        public void ValidarRolesPuntoDeCompra(int idPuntoDeCompra)
        {
            ValidarRol(idPuntoDeCompra, ActorSettings.Default.IdRolPuntoDeCompra, "Centro de atencion no tiene el rol punto de compra");
        }

        public void ValidarRolesPuntoDeCompraCaja(int idPuntoDeCompra, int idCaja)
        {
            ValidarRol(idPuntoDeCompra, ActorSettings.Default.IdRolPuntoDeCompra, "Centro de atencion no tiene el rol punto de compra");
            ValidarRol(idCaja, ActorSettings.Default.IdRolCaja, "Centro de atencion no tiene el rol caja");
        }

        public void ValidarRolesPuntoDeCompraAlmacen(int idPuntoDeCompra, int idAlmacen)
        {
            ValidarRol(idPuntoDeCompra, ActorSettings.Default.IdRolPuntoDeCompra, "Centro de atencion no tiene el rol punto de compra");
            ValidarRol(idAlmacen, ActorSettings.Default.IdRolAlmacen, "Centro de atencion no tiene el rol almacen");
        }

        public void ValidarRolesPuntoDeCompraCajaAlmacen(int idPuntoDeCompra, int idCaja, int idAlmacen)
        {
            ValidarRol(idPuntoDeCompra, ActorSettings.Default.IdRolPuntoDeCompra, "Centro de atencion no tiene el rol punto de compra");
            ValidarRol(idCaja, ActorSettings.Default.IdRolCaja, "Centro de atencion no tiene el rol caja");
            ValidarRol(idAlmacen, ActorSettings.Default.IdRolAlmacen, "Centro de atencion no tiene el rol almacen");
        }
        //*****
        public void ValidarRol(int idCentroAtencion, int idRol, string mensajeDeError)
        {
            if (actorRepositorio.ObtenerActorDeNegocioVigente(idCentroAtencion, idRol) == null)
            {
                throw new LogicaException(mensajeDeError);
            }
        }






        #region GENERAR NOTAS DE DEBITO Y CREDITO

        public List<TipoDeComprobanteParaTransaccion> ObtenerTiposDeComprobanteParaNotaDeDebito(int idEmpleado, int idCentroAtencion)
        {
            try
            {
                var resultado = transaccionRepositorio.ObtenerTipoComprobantePorTipoDeComprobante(MaestroSettings.Default.IdDetalleMaestroComprobanteNotaDeDebito).GroupBy(r => new { r.id_tipo_comprobante, r.es_propio }).Select(r => r.FirstOrDefault());
                foreach (var tipo in resultado)
                {
                    tipo.Detalle_maestro.Serie_comprobante = tipo.Detalle_maestro.Serie_comprobante.Where(sc => sc.id_propietario == idCentroAtencion && sc.es_vigente).ToList();
                }
                return TipoDeComprobanteParaTransaccion.Convert(resultado.ToList());
            }
            catch (Exception e) { throw e; }
        }

        public List<TipoDeComprobanteParaTransaccion> ObtenerTiposDeComprobanteParaNotaDeCredito(int idEmpleado, int idCentroAtencion)
        {
            try
            {
                var resultado = transaccionRepositorio.ObtenerTipoComprobantePorTipoDeComprobante(MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito).GroupBy(r => new { r.id_tipo_comprobante, r.es_propio }).Select(r => r.FirstOrDefault());
                foreach (var tipo in resultado)
                {
                    tipo.Detalle_maestro.Serie_comprobante = tipo.Detalle_maestro.Serie_comprobante.Where(sc => sc.id_propietario == idCentroAtencion && sc.es_vigente).ToList();
                }
                return TipoDeComprobanteParaTransaccion.Convert(resultado.ToList());
            }
            catch (Exception e) { throw e; }
        }

        //Genera una transaccion tanto para nota de credito y debito
        private Transaccion GenerarNotaDeCreditoDebito(int idEmpleado, int idUnidadNegocio, bool esPropio, int idSerieComprobante, int idTipoComprobante, int numeroDeComprobante, string numeroSerieDeComprobante, DateTime fechaRegistro, string sufijoCodigo, int idTipoTransaccion, decimal importeTotal, string observacion, int idProveedor, int idCentroAtencion, UserProfileSessionData sesionDeUsuario)
        {
            try
            {
                int idMoneda = MaestroSettings.Default.IdDetalleMaestroMonedaSoles;
                decimal tipoDeCambio = sesionDeUsuario.TipoDeCambio.ValorVenta; //_repositorioTransaccion.obtenerTipoDeCambio(fechaRegistro).valorVenta;
                //obtener operacion generica actual
                Operacion operacionGenerica = new Operacion(transaccionRepositorio.ObtenerUltimaTransaccion(TransaccionSettings.Default.IdTipoTransaccionOperacion));
                //obtenemos el codigo
                string codigo = codigosOperacion_Logica.ObtenerSiguienteCodigoParaFacturacion(sufijoCodigo, idTipoTransaccion);
                //Generamos el comprobante
                Comprobante comprobante = GenerarComprobante(esPropio, idSerieComprobante, idTipoComprobante, numeroSerieDeComprobante, numeroDeComprobante);
                //crear una Compra
                Transaccion nota = new Transaccion(codigo, operacionGenerica.Id, fechaRegistro, idTipoTransaccion, idUnidadNegocio, true, fechaRegistro, fechaRegistro, observacion, fechaRegistro, idEmpleado, importeTotal, idCentroAtencion, idMoneda, tipoDeCambio, null, idProveedor)
                {
                    //agregamos el comprobante a la compra
                    Comprobante = comprobante
                };
                return nota;
            }
            catch (Exception e)
            {
                throw new LogicaException("error al generar ta transaccion", e);
            }
        }

        //Genera una transaccion orden tanto para compra y gasto
        private Transaccion GenerarOrdenNotaDeCreditoDebito(Transaccion operacion, int idEmpleado, int idUnidadNegocio, int idTipoNota, DateTime fechaRegistro, string modoDePago, string sufijoCodigo, int idOrdenTransaccion, string observacion, int idTercero, string aliasTercero, int idCentroAtencion, List<Detalle_transaccion> detalles, int estadoTransaccion, string observacionEstadoTransacciones, bool esIntegrado, bool hayMovimientoDeMercaderia)
        {
            decimal descuentoGlobal = 0, descuentoPorItem = 0, anticipo = 0, gravada = 0, exonerada = 0, inafecta = 0, gratuita = 0, igv = 0, isc = 0, icbper = 0, otrosCargos = 0, otrosTributos = 0;
            if (detalles.Sum(d => d.igv) > 0)
            {
                gravada = detalles.Sum(d => d.total - d.igv);
                igv = detalles.Sum(d => d.igv);
            }
            else
            {
                exonerada = detalles.Sum(d => d.total);
            }
            //crear una orden de compra
            Transaccion ordenDeNota = new Transaccion(codigosOperacion_Logica.ObtenerSiguienteCodigoParaFacturacion(operacion.codigo + "_O" + sufijoCodigo, idOrdenTransaccion), null, fechaRegistro, idOrdenTransaccion, idUnidadNegocio, true, fechaRegistro, fechaRegistro, observacion, fechaRegistro, idEmpleado, operacion.importe_total, idCentroAtencion, operacion.id_moneda, operacion.tipo_cambio, null, idTercero, descuentoGlobal, descuentoPorItem, anticipo, gravada, exonerada, inafecta, gratuita, igv, isc, icbper, otrosCargos, otrosTributos)
            {
                //agregamos el comprobante a la orden de compra
                Comprobante = operacion.Comprobante
            };
            //Agregamos los detalles
            ordenDeNota.AgregarDetalles(Detalle_transaccion.Clone(detalles));
            //Agregamos el estado de la orden por defecto
            Estado_transaccion estadoDeLaOrdenDeCompra = new Estado_transaccion(idEmpleado, estadoTransaccion, fechaRegistro, observacionEstadoTransacciones);
            ordenDeNota.Estado_transaccion.Add(estadoDeLaOrdenDeCompra);
            ordenDeNota.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroModoDePago, modoDePago));
            if (!esIntegrado && hayMovimientoDeMercaderia)
            {
                ordenDeNota.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroEstadoEntregaMercaderia, "0"));
            }
            ordenDeNota.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroCodigoTransaccionSunat, maestroRepositorio.ObtenerDetalle(idTipoNota).codigo));
            if (aliasTercero != "")
            {
                ordenDeNota.Parametro_transaccion.Add(new Parametro_transaccion(MaestroSettings.Default.IdDetalleMaestroParametroAliasCliente, aliasTercero));
            }
            return ordenDeNota;
        }
        public List<Detalle_transaccion> CalcularDetalleNotaDebitoCredito(List<DetalleOrdenDeNota> detallesDeNota, List<Detalle_transaccion> detallesDeOrdenOperacion, int idTipoDeNota, string valorDeNota, string motivo, bool gravaIgv)
        {
            List<Detalle_transaccion> detalleResultado = new List<Detalle_transaccion>();
            bool calcularIgv = idTipoDeNota == MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaDescuentoGlobal || idTipoDeNota == MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaDescuentoPorItem || idTipoDeNota == MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaDevolucionPorItem || idTipoDeNota == MaestroSettings.Default.IdDetalleMaestroNotaDeDebitoElectronicaInteresesPorMora || idTipoDeNota == MaestroSettings.Default.IdDetalleMaestroNotaDeDebitoElectronicaAumentoEnElValor;
            //NOTA DE CREDITO
            //idDetalleMaestroAnulacionDeLaOperacion // idDetalleMaestroDevolucionTotal 
            if (idTipoDeNota == MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaAnulacionDeLaOperacion || idTipoDeNota == MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaDevolucionTotal)
            {
                detalleResultado = detallesDeOrdenOperacion;
            }
            // idDetalleMaestroAnulacionPorErrorEnElRuc
            else if (idTipoDeNota == MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaAnulacionPorErrorEnElRuc)
            {
                throw new LogicaException("Nota de credito anulacion por error en el ruc todavia no implementado");
                //detalleResultado = detallesDeOrdenOperacion;
            }
            //idDetalleMaestroDescuentoGlobal
            else if (idTipoDeNota == MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaDescuentoGlobal)
            {
                detalleResultado = new List<Detalle_transaccion>() { new Detalle_transaccion(1, ConceptoSettings.Default.IdConceptoNegocioDescuentoGlobal, motivo, Decimal.Parse(valorDeNota), Decimal.Parse(valorDeNota), null, 0, null, null, 0, 0, 0, null, null, null) };
            }
            //idDetalleMaestroCorreccionPorErrorEnLaDescripcion
            else if (idTipoDeNota == MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaCorreccionPorErrorEnLaDescripcion)
            {
                throw new LogicaException("Nota de credito coreccion por error en la descripcion todavia no implementado");
                //detalleResultado = detallesDeNota.Where(d => !String.IsNullOrEmpty(d.valorDeDetalle)).Select(d => { d.Detalle = d.valorDeDetalle; return d; }).Select(d => d.DetalleTransaccion()).ToList();
            }
            //idDetalleMaestroDescuentoPorItem
            else if (idTipoDeNota == MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaDescuentoPorItem)
            {
                detalleResultado = detallesDeNota.Where(d => d.MontoDetalle != 0).Select(d => { d.PrecioUnitario = d.MontoDetalle / d.Cantidad; d.Importe =  d.MontoDetalle; return d; }).Select(d => d.DetalleTransaccion()).ToList();
            }
            //idDetalleMaestroDevolucionPorItem
            else if (idTipoDeNota == MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaDevolucionPorItem)
            {
                detalleResultado = detallesDeNota.Where(d => d.MontoDetalle != 0).Select(d => { d.Cantidad = d.MontoDetalle; d.Importe = Math.Round(d.MontoDetalle * d.PrecioUnitario, 2); return d; }).Select(d => d.DetalleTransaccion()).ToList();
            }
            //NOTA DE DEBITO
            //idDetalleMaestroInteresesPorMora
            else if (idTipoDeNota == MaestroSettings.Default.IdDetalleMaestroNotaDeDebitoElectronicaInteresesPorMora)
            {
                detalleResultado = new List<Detalle_transaccion>() { new Detalle_transaccion(1, ConceptoSettings.Default.IdConceptoNegocioInteresPorMora, motivo, decimal.Parse(valorDeNota), decimal.Parse(valorDeNota), null, 0, null, null, 0, 0, 0, null, null, null) };
            }
            //idDetalleMaestroAumentoEnElValor
            else if (idTipoDeNota == MaestroSettings.Default.IdDetalleMaestroNotaDeDebitoElectronicaAumentoEnElValor)
            {
                detalleResultado = detallesDeNota.Where(d => d.MontoDetalle != 0).Select(d => { d.PrecioUnitario = d.MontoDetalle / d.Cantidad; d.Importe = d.MontoDetalle; return d; }).Select(d => d.DetalleTransaccion()).ToList();
            }
            //Verificar si se calculara el igv
            if (calcularIgv && gravaIgv)
            {
                detalleResultado.ForEach(d => d.igv = Decimal.Round(d.total - (d.total / (1 + TransaccionSettings.Default.TasaIGV)), 2));
            }
            return detalleResultado;
        }

        private Transaccion GenerarPagoPorNotaCreditoODebito(Transaccion ordenNota, string codigoPago, decimal totalPago, int idEmpleado, DateTime fechaDePago, string observacion, int idCentroAtencion, int estadoTransaccion, string observacionEstadoTransacciones)
        {
            int idTipoTransaccionPago = Diccionario.MapeoOrdenVsMovimientoEconomico.Single(m => m.Key == ordenNota.id_tipo_transaccion).Value;
            Transaccion pago = new Transaccion(codigoPago, null, fechaDePago, idTipoTransaccionPago, ordenNota.id_unidad_negocio, true, fechaDePago, fechaDePago, observacion, fechaDePago, idEmpleado, totalPago, idCentroAtencion, ordenNota.id_moneda, ordenNota.tipo_cambio, null, ordenNota.id_actor_negocio_externo);
            pago.Comprobante = ordenNota.Comprobante;
            pago.Traza_pago.Add(new Traza_pago(MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo, "Pago de nota", MaestroSettings.Default.IdDetalleMaestroEntidadBancariaPorDefecto));
            pago.Estado_transaccion.Add(new Estado_transaccion(idEmpleado, estadoTransaccion, fechaDePago, observacionEstadoTransacciones));
            return pago;
        }

        #endregion
        #region GENERAR COMPROBANTE
        public void ResolverComprobantes(List<OperacionIntegradaSerie> operaciones)
        {
            foreach (var idSerieUnica in operaciones.Select(vis => vis.IdSerieComprobante).Distinct())
            {
                Serie_comprobante serie = transaccionRepositorio.ObtenerSerieDeComprobante(idSerieUnica);
                transaccionRepositorio.MarcarSerieComoModificada(serie);//Para asegurar que se actualice el numero siguiente.
                foreach (var operacion in operaciones)
                {
                    if (operacion.IdSerieComprobante == serie.id)
                    {
                        Comprobante comprobante = GenerarComprobantePropioAutonumerable(serie);
                        operacion.OperacionIntegrada.AsignarComprobante(comprobante);
                    }
                }
            }
        }

        public void ResolverComprobantes(List<OperacionIntegrada> operaciones)
        {
            foreach (var idSerie in operaciones.Select(o => (int)o.OrdenDeOperacion.Comprobante.id_serie_comprobante).Distinct())
            {
                Serie_comprobante serie = transaccionRepositorio.ObtenerSerieDeComprobante(idSerie);
                transaccionRepositorio.MarcarSerieComoModificada(serie);//Para asegurar que se actualice el numero siguiente.
                foreach (var operacion in operaciones.Where(o => o.OrdenDeOperacion.Comprobante.id_serie_comprobante == serie.id))
                {
                    operacion.ReemplazarComprobante(GenerarComprobantePropioAutonumerable(serie));
                };
            }
        }

        public Comprobante GenerarComprobante(bool esPropio, int idSerieComprobante, int idTipoComprobante, string numeroSerieDeComprobante, int numeroDeComprobante)
        {
            Comprobante comprobante;
            if (esPropio)
            {
                comprobante = GenerarComprobantePropio(idSerieComprobante, numeroDeComprobante);
            }
            else
            {
                comprobante = GenerarComprobanteNoPropio(idTipoComprobante, numeroSerieDeComprobante, numeroDeComprobante);
            }
            return comprobante;
        }

        public Comprobante GenerarComprobantePropio(int idSerieComprobante, int numeroDeComprobante)
        {
            Serie_comprobante serie = transaccionRepositorio.ObtenerSerieDeComprobante(idSerieComprobante);
            Comprobante comprobante;
            if (serie.es_autonumerable)
            {
                comprobante = GenerarComprobantePropioAutonumerableMarcandoSerieComoModificada(serie);
            }
            else
            {
                comprobante = GenerarComprobantePropioNoAutonumerable(serie, numeroDeComprobante);
            }
            return comprobante;
        }

        public Comprobante GenerarComprobantePropioAutonumerable(int idSerieComprobante)
        {
            Serie_comprobante serie = transaccionRepositorio.ObtenerSerieDeComprobante(idSerieComprobante);
            return GenerarComprobantePropioAutonumerableMarcandoSerieComoModificada(serie);
        }

        public Comprobante GenerarComprobantePropioAutonumerableMarcandoSerieComoModificada(Serie_comprobante serie)
        {
            Comprobante comprobante = GenerarComprobantePropioAutonumerable(serie);
            transaccionRepositorio.MarcarSerieComoModificada(serie);//Para asegurar que se actualice el numero siguiente.
            return comprobante;
        }

        public void AutoIncrementarSerieMarcandolaComoModificada(Serie_comprobante serie)
        {
            serie.proximo_numero++;
            transaccionRepositorio.MarcarSerieComoModificada(serie);//Para asegurar que se actualice el numero siguiente.
        }

        public Comprobante GenerarComprobantePropioAutonumerable(Serie_comprobante serie)
        {
            Comprobante comprobante = new Comprobante(serie.id_tipo_comprobante, serie.id, serie.proximo_numero, true, serie.numero);
            serie.proximo_numero++;
            return comprobante;
        }

        public Comprobante GenerarComprobantePropioNoAutonumerable(Serie_comprobante serie, int numeroDeComprobante)
        {
            if (numeroDeComprobante <= 0) throw new LogicaException("El numero de comprobante debe de ser ingresado");
            Comprobante comprobante = new Comprobante(serie.id_tipo_comprobante, serie.id, numeroDeComprobante, true, serie.numero);
            return comprobante;
        }

        public Comprobante GenerarComprobanteNoPropio(int idTipoComprobante, string numeroSerieDeComprobante, int numeroDeComprobante)
        {
            if (numeroDeComprobante <= 0) throw new LogicaException("El numero de comprobante debe de ser ingresado");
            if (string.IsNullOrEmpty(numeroSerieDeComprobante)) throw new LogicaException("El numero de serie de comprobante debe de ser ingresado");
            Comprobante comprobante = new Comprobante(idTipoComprobante, null, numeroDeComprobante, true, numeroSerieDeComprobante);
            return comprobante;
        }

        public Operacion ObtenerOperacionSesionContenedora(int idCentroAtencion)
        {
            return new Operacion(transaccionRepositorio.ObtenerUltimaTransaccion(TransaccionSettings.Default.IdTipoTransaccionOperacion));
        }








        #endregion



        #region CODIGO NO USADO


        #endregion

    }
}

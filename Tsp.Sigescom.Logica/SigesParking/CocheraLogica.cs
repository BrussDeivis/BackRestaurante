using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Logica;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Custom.SigesParking;
using Tsp.Sigescom.Modelo.Custom.SigesParking.Exceptions;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Entidades.Sesion;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Actores;
using Tsp.Sigescom.Modelo.Interfaces.Datos.CentrosDeAtencion;
using Tsp.Sigescom.Modelo.Interfaces.Infraestructura;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;

namespace Tsp.Sigescom.Parking.Logica
{
    public class CocheraLogica : ICocheraLogica
    {
        private readonly IActorRepositorio _actorRepositorio;
        private readonly IActor_Repositorio _actor_Repositorio;


        private readonly IMaestroRepositorio _maestroRepositorio;
        private readonly IConceptoRepositorio _conceptoRepositorio;
        private readonly ICocheraRepositorio _cocheraRepositorio;
        private readonly ITransaccionRepositorio _transaccionRepositorio;
        private readonly IPrecioRepositorio _precioRepositorio;
        private readonly IOperacionLogica _operacionLogica;
        private readonly IBarCodeUtil _barCodeUtil;

        private readonly IVinculoActor_Repositorio _vinculoActorDatos;
        private readonly ICentroDeAtencion_Repositorio _centroDeAtencionDatos;


        public CocheraLogica(IActorRepositorio actorRepositorio, ITransaccionRepositorio transaccionRepositorio, IMaestroRepositorio maestroRepositorio, IConceptoRepositorio conceptoRepositorio, ICocheraRepositorio cocheraRepositorio, IPrecioRepositorio precioRepositorio, IBarCodeUtil barCodeUtil, IFacturacionRepositorio facturacionRepositorio,  ICodigosOperacion_Logica codigosOperacionLogica, IPermisos_Logica permisosLogica, IActor_Repositorio actor_Repositorio, IVinculoActor_Repositorio vinculoActorDatos, ICentroDeAtencion_Repositorio centroDeAtencionDatos)
        {
            _actorRepositorio = actorRepositorio;
            _transaccionRepositorio = transaccionRepositorio;
            _conceptoRepositorio = conceptoRepositorio;
            _maestroRepositorio = maestroRepositorio;
            _cocheraRepositorio = cocheraRepositorio;
            _precioRepositorio = precioRepositorio;
            _barCodeUtil = barCodeUtil;
            _operacionLogica = new OperacionLogica(_transaccionRepositorio, _maestroRepositorio, _actorRepositorio, _conceptoRepositorio, facturacionRepositorio, codigosOperacionLogica, permisosLogica, null,null,null, null);
            _actor_Repositorio = actor_Repositorio;
            _vinculoActorDatos = vinculoActorDatos;
            _centroDeAtencionDatos = centroDeAtencionDatos;
        }

        public Actor_negocio GenerarActorDeNegocio(Vehiculo vehiculo)
        {
            try
            {
                DateTime fechaActual = DateTimeUtil.FechaActual();
                DateTime fechaFin = fechaActual.AddYears(ActorSettings.Default.vigenciaEnAnyosPorDefectoDeActorDeNegocioEntidadInterna);
                //Crear el actor de negocio
                Actor_negocio _vehiculoActorDeNegocio = new Actor_negocio(CocheraSettings.Default.IdRolVehiculo, fechaActual, fechaFin, "", true, false, "");
                //Crear al actor
                Actor actor = new Actor(CocheraSettings.Default.IdTipoDocumentoIdentidadPlaca, fechaActual, vehiculo.Placa, vehiculo.TipoDeVehiculo.Nombre, vehiculo.Marca.Nombre, "", CocheraSettings.Default.IdTipoActorVehiculo,
                    ActorSettings.Default.IdFotoActorPorDefecto, vehiculo.TipoDeVehiculo.Id, CocheraSettings.Default.IdEstadoLegalNoEspecificadoDeTipoActorVehiculo, "", vehiculo.Color, "")
                {
                    id_detalle_multiproposito = vehiculo.Marca.Id
                };
                //Asignar el actor al vehiculo
                _vehiculoActorDeNegocio.Actor = actor;
                return _vehiculoActorDeNegocio;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar crear actor de negocio a partir del vehículo", e);
            }
        }
        public OperationResult RegistrarVehiculo(Vehiculo vehiculo)
        {
            try
            {
                var vehiculoActorDeNegocio = GenerarActorDeNegocio(vehiculo);
                var resultado = _actor_Repositorio.CrearActorNegocio(vehiculoActorDeNegocio);
                //conseguir datos luego de guardar
                vehiculo.Id = vehiculoActorDeNegocio.id;
                vehiculo.IdActor = vehiculoActorDeNegocio.id_actor;
                resultado.information = vehiculo;
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar registrar vehiculo", e);
            }
        }

        public OperationResult EditarVehiculo(Vehiculo vehiculo)
        {
            try
            {
                DateTime fechaActual = DateTimeUtil.FechaActual();
                DateTime fechaFin = fechaActual.AddYears(ActorSettings.Default.vigenciaEnAnyosPorDefectoDeActorDeNegocioEntidadInterna);
                //Crear el actor de negocio
                Actor_negocio _vehiculoActorDeNegocio = new Actor_negocio(vehiculo.Id, vehiculo.IdActor, CocheraSettings.Default.IdRolVehiculo, fechaActual, fechaFin, "", true, false, "");
                //Crear al actor
                Actor actor = new Actor(vehiculo.IdActor, CocheraSettings.Default.IdTipoDocumentoIdentidadPlaca, fechaActual, vehiculo.Placa, vehiculo.TipoDeVehiculo.Nombre, vehiculo.Marca.Nombre, "", CocheraSettings.Default.IdTipoActorVehiculo, ActorSettings.Default.IdFotoActorPorDefecto, vehiculo.TipoDeVehiculo.Id, CocheraSettings.Default.IdEstadoLegalNoEspecificadoDeTipoActorVehiculo, "", vehiculo.Color, "")
                {
                    id_detalle_multiproposito = vehiculo.Marca.Id
                };
                //Asignar en actor al vehiculo
                _vehiculoActorDeNegocio.Actor = actor;
                var resultado = _actor_Repositorio.ActualizarActorNegocio(_vehiculoActorDeNegocio);
                resultado.information = vehiculo;
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar registrar vehiculo", e);
            }
        }

        public OperationResult ExonerarVehiculo(Vehiculo vehiculo, int idCochera)
        {
            try
            {
                //obtener fecha actual
                DateTime fechaActual = DateTimeUtil.FechaActual();
                //asegurar que no exista el vinculo o lanzar excepción
                if (_vinculoActorDatos.ExisteVinculoVigente(vehiculo.Id, idCochera))
                {
                    throw new LogicaException("El vehiculo ya se encuentra exonerado");
                }
                else
                {
                    //crear el vinculo 
                    Vinculo_Actor_Negocio vinculo = new Vinculo_Actor_Negocio { id_actor_negocio_vinculado = vehiculo.Id, id_actor_negocio_principal = idCochera, tipo_vinculo = (int)TipoVinculo.VehiculoExoneradoEnCochera, desde = fechaActual, hasta = fechaActual.AddDays(CocheraSettings.Default.DiasDeExoneracionDelPagoPorDefecto), descripcion = "", es_vigente = true };
                    return _vinculoActorDatos.CrearVinculoActorNegocio(vinculo);
                }
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar Exonerar Vehículo " + vehiculo.Placa, e);
            }
        }
        public OperationResult QuitarExoneracionAVehiculo(ExoneracionDeVehiculo vehiculoExonerado)
        {
            try
            {
                //obtener fecha actual
                DateTime fechaActual = DateTimeUtil.FechaActual();
                //caducar el vinculo 
                return _vinculoActorDatos.CaducarVinculoActorNegocio(vehiculoExonerado.Id, fechaActual);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar quitar la exoneración al Vehículo: " + vehiculoExonerado.Vehiculo.Placa, e);
            }
        }

        public List<ExoneracionDeVehiculo> ObtenerVehiculosExonerados(int idCochera)
        {
            try
            {
                return _cocheraRepositorio.ObtenerExoneraciones(idCochera);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener vehículos exonerados", e);
            }
        }

        public List<ExoneracionDeVehiculo> ObtenerVehiculosExoneradosVigentes(int idCochera)
        {
            try
            {
                return _cocheraRepositorio.ObtenerExoneracionesVigentes(idCochera);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener vehículos exonerados vigentes", e);
            }
        }

        public List<ItemGenerico> ObtenerCocheras()
        {
            try
            {
                return _centroDeAtencionDatos.ObtenerCentrosDeAtencionComoItemsGenericosSegunRolHijo(CocheraSettings.Default.IdRolCochera,true).ToList();
                    
                    //.ObtenerActoresDeNegocioPrincipalesVigentesComoItemsGenericos(ActorSettings.Default.IdRolEntidadInterna, CocheraSettings.Default.IdRolCochera)).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener cocheras", e);
            }
        }

        public bool VehiculoSeEncuentraIngresado(Vehiculo vehiculo, SesionCochera sesionCochera)
        {
            return _transaccionRepositorio.ExisteTransaccion(CocheraSettings.Default.IdTipoTransaccionMovimientoDeCochera, sesionCochera.IdCochera, vehiculo.Id, MaestroSettings.Default.IdDetalleMaestroEstadoIngresado);
        }
        public Modelo.Entidades.Comprobante GenerarComprobante(SesionCochera sesionCochera)
        {
            //Obtener la serie del comprobante
            Serie_comprobante serie = _transaccionRepositorio.ObtenerPrimeraSerieDeComprobantePorCentroDeAtencionYComprobante(CocheraSettings.Default.IdDetalleMaestroComprobanteNotaDeMovimientoDeCochera, sesionCochera.SesionDeUsuario.IdCentroDeAtencionSeleccionado);
            if (serie == null)
            {
                throw new LogicaException("No se ha encontrado una SERIE DE COMPROBANTE para este tipo de transaccion.");
            }
            //Crear el comprobante
            Modelo.Entidades.Comprobante comprobante = _operacionLogica.GenerarComprobantePropioAutonumerableMarcandoSerieComoModificada(serie);
            return comprobante;
        }

        public Transaccion GenerarTransaccionIngreso(SesionCochera sesionCochera, Ingreso ingreso, DateTime fecha)
        {
            //creamos la transaccion
            Transaccion transaccionIngreso = ingreso.Convert(sesionCochera.SesionDeUsuario, fecha);
            ingreso.FechaHora = fecha;
            //generamos el comprobante
            transaccionIngreso.Comprobante = GenerarComprobante(sesionCochera);
            //Crear estado de transaccion
            Estado_transaccion estadoIngresado = new Estado_transaccion(sesionCochera.SesionDeUsuario.IdCentroDeAtencionSeleccionado, MaestroSettings.Default.IdDetalleMaestroEstadoIngresado, fecha, "Estado asignado al momento de ingresar a cochera");
            //Asignar estado ingresado al movimiento
            transaccionIngreso.Estado_transaccion.Add(estadoIngresado);
            return transaccionIngreso;
        }

        public OperationResult RegistrarIngreso(SesionCochera sesionCochera, Ingreso ingreso)
        {
            try
            {

                //validar que no exista un movimiento en estado ingresado para el vehiculo
                if (VehiculoSeEncuentraIngresado(ingreso.Vehiculo, sesionCochera))
                {
                    throw new VehiculoYaIngresadoException(ingreso.Vehiculo.Placa);
                }
                DateTime fechaActual = DateTimeUtil.FechaActual();
                //Generamos la transaccion
                var transaccionIngreso = GenerarTransaccionIngreso(sesionCochera, ingreso, fechaActual);
                var resultado = GuardarIngreso(transaccionIngreso);
                //devolvemos el ticket de ingreso para su impresión
                ingreso.EsValido = true;
                resultado.information = new TicketIngresoCochera(sesionCochera.SesionDeUsuario.Sede, sesionCochera.SesionDeUsuario.EstablecimientoComercialSeleccionado, ingreso, transaccionIngreso.Comprobante.numero_serie, transaccionIngreso.Comprobante.numero, _barCodeUtil.ObtenerCodigoBarras(ingreso.Vehiculo.Placa));
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar registrar ingreso de vehiculo", e);
            }
        }

        public OperationResult GuardarIngreso(Transaccion transaccionIngreso)
        {
            OperationResult resultado = null;
            bool hayProblemaDeConcurrenciaSerieComprobante;
            do
            {
                hayProblemaDeConcurrenciaSerieComprobante = false;
                try
                {
                    //Crear la transaccion movimiento
                    resultado = _transaccionRepositorio.CrearTransaccion(transaccionIngreso);
                }
                catch (SerieComprobanteException e)
                {
                    _transaccionRepositorio.RefreshEntity(e.serieComprobante);
                    //corregir el numero del comprobante
                    _operacionLogica.RegenerarNumeracionComprobantePropioAutonumerable(transaccionIngreso.Comprobante, e.serieComprobante);
                    hayProblemaDeConcurrenciaSerieComprobante = true;
                }
                catch (Exception)
                {

                    throw;
                }
                return resultado;
            }
            while (hayProblemaDeConcurrenciaSerieComprobante);
        }

        public SesionCochera ObtenerSesion(UserProfileSessionData sesionDeUsuario)
        {
            var configuracion = ObtenerConfiguracion(sesionDeUsuario.CentroDeAtencionSeleccionado.Id);
            var turnos = TurnoCochera.Convert(_maestroRepositorio.ObtenerTurnos(configuracion.IdTipoDeTurno));
            ///ids de conceptos de servicio cochera y exceso para el sistema de pago PLANA por turnos
            var idsConceptos = (turnos.Select(t => t.Configuracion.IdConceptoExceso).Union(turnos.Select(t => t.Configuracion.IdConceptoServicioCochera))).Distinct().ToList();
            ///agregamos ids de conceptos para el sistema de pago por HORA
            idsConceptos.AddRange(new int[] { configuracion.IdConceptoPerdidaDeTicket, configuracion.IdConceptoServicioCocheraEnSistemaDePagoPorHora });
            ///Conseguimos los precios para todos los ids de conceptos
            var precios = ImporteConcepto.Convert(_precioRepositorio.ObtenerPreciosVigentes(idsConceptos.ToArray(), MaestroSettings.Default.IdDetalleMaestroTarifaNormal).ToList());

            return new SesionCochera
            {
                Configuracion = configuracion,
                Turnos = turnos,
                SesionDeUsuario = sesionDeUsuario,
                Precios = precios
            };
        }

        protected ConfiguracionCochera ObtenerConfiguracion(int idCochera)
        {
            if (CocheraSettings.Default.TipoDeConfiguracion == (int)TipoConfiguracion.GENERAL)
            {
                return new ConfiguracionCochera
                {
                    IdConceptoPerdidaDeTicket = CocheraSettings.Default.IdConceptoNegocioPerdidaTickect,
                    IdConceptoServicioCocheraEnSistemaDePagoPorHora = CocheraSettings.Default.IdConceptoNegocioServicioCocheraEnSistemaDePagoPorHora,
                    MinutosDeToleranciaEnSistemaDePagoPorHora = CocheraSettings.Default.ToleranciaEnMinutosSistemaDePagoPorHora,
                    MinutosDeToleranciaExcesoEnSistemaDePagoPlanaPorTurnos = CocheraSettings.Default.ToleranciaExcesoEnMinutosSistemaDePagoPlanaPorTurnos,
                    IdTipoDeTurno = CocheraSettings.Default.IdDetalleMaestroTipoTurnoCochera,
                    PeriodosHabilitadosEnSistemasDePagoAbonados = Convertir.Periodos(CocheraSettings.Default.PeriodosHabilitadosEnSistemasDePagoAbonados),
                    SistemasDePagoHabilitados = Convertir.SistemasDePagoCochera(CocheraSettings.Default.SistemasDePagoHabilitados)
                };
            }
            else
            {
                return _cocheraRepositorio.ObtenerConfiguracion(idCochera);
            }

        }

        public void DeterminarTiempoDeExceso(MovimientoCochera movimiento, SesionCochera sesionCochera)
        {
            var toleranciaEnMinutos = sesionCochera.Configuracion.MinutosDeToleranciaExcesoEnSistemaDePagoPlanaPorTurnos;
            var hayExceso = DateTime.Compare(movimiento.Salida, movimiento.Turno.Fin(movimiento.Ingreso).AddMinutes(toleranciaEnMinutos)) > 0;
            var horasExcesoSinRedondeo = hayExceso ? movimiento.Salida.Subtract(movimiento.Turno.Fin(movimiento.Ingreso.AddMinutes(toleranciaEnMinutos))).TotalHours : 0;
            movimiento.TiempoExcesoSistemaPlanaPorTurnos = new TiempoHoras();
            movimiento.TiempoExcesoSistemaPlanaPorTurnos.Horas = (decimal)horasExcesoSinRedondeo;
            movimiento.TiempoExcesoSistemaPlanaPorTurnos.HorasString = Math.Floor(horasExcesoSinRedondeo).ToString() + "h : " + Math.Ceiling((horasExcesoSinRedondeo - Math.Floor(horasExcesoSinRedondeo)) * 60).ToString() + "min";
        }
        public MovimientoCochera ObtenerMovimientoParaSalida(string placaVehiculo, SesionCochera sesionCochera)
        {
            try
            {
                if (!_cocheraRepositorio.ExisteVehiculo(placaVehiculo))
                {
                    throw new VehiculoNoExisteException(placaVehiculo);
                }
                MovimientoCochera movimiento = _cocheraRepositorio.ObtenerTransaccion_MovimientoCochera(placaVehiculo, sesionCochera.SesionDeUsuario.CentroDeAtencionSeleccionado.Id, MaestroSettings.Default.IdDetalleMaestroEstadoIngresado);

                if (movimiento == null)
                {
                    throw new VehiculoNoIngresadoException(placaVehiculo);
                }
                DateTime fechaActual = DateTimeUtil.FechaActual();
                //establecemos fecha de salida 
                movimiento.Salida = fechaActual;
                //en caso corresponda, establecemos el turno que le corresponde y calculamos el exceso
                if (movimiento.SistemaDePago.Id == (int)SistemaPagoCochera.PLN)
                {
                    movimiento.Turno = sesionCochera.ObtenerTurno(movimiento.Ingreso);
                    DeterminarTiempoDeExceso(movimiento, sesionCochera);
                }
                //determinamos los importes a cobrar
                movimiento.DetallesACobrar = this.DetallesACobrar(movimiento, sesionCochera);

                return movimiento;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener movimiento de ingreso a cochera", e);
            }
        }

        private DetallesACobrar DetallesACobrarSistemaDePagoPlanaPorTurnos(MovimientoCochera movimiento, SesionCochera sesion)
        {
            try
            {
                DetallesACobrar detalles = new DetallesACobrar();
                //conseguir el precio de los conceptos
                var precioServicioCochera = sesion.Precio(movimiento.Turno.Configuracion.IdConceptoServicioCochera);
                var precioExceso = sesion.Precio(movimiento.Turno.Configuracion.IdConceptoExceso);
                detalles.Principal = precioServicioCochera.Importe;
                detalles.Exceso = (decimal)(movimiento.TiempoExcesoSistemaPlanaPorTurnos.HorasACobrar * precioExceso.Importe);
                return detalles;
            }
            catch (Exception e)
            {
                throw new LogicaException("ERROR al intentar determinar los importes a cobrar para el sistema de pago Plana por Turnos", e);
            }
        }

        private DetallesACobrar DetallesACobrarSistemaDePagoPorHora(MovimientoCochera movimiento, SesionCochera sesion)
        {
            try
            {
                DetallesACobrar detalles = new DetallesACobrar();
                var precioServicioCochera = sesion.Precio(sesion.Configuracion.IdConceptoServicioCocheraEnSistemaDePagoPorHora);
                var toleranciaEnMinutos = sesion.Configuracion.MinutosDeToleranciaEnSistemaDePagoPorHora;
                detalles.Principal = precioServicioCochera.Importe * Math.Ceiling((decimal)movimiento.Salida.Subtract(movimiento.Ingreso.AddMinutes(toleranciaEnMinutos)).TotalHours);
                detalles.Exceso = 0;
                return detalles;
            }
            catch (Exception e)
            {
                throw new LogicaException("ERROR al intentar determinar los importes a cobrar", e);
            }
        }
        private DetallesACobrar DetallesACobrar(MovimientoCochera movimiento, SesionCochera sesion)
        {
            try
            {
                DetallesACobrar detalles = new DetallesACobrar();
                if (movimiento.Vehiculo.ExoneradoDePagos || movimiento.SistemaDePago.Id == (int)SistemaPagoCochera.ABN)
                {
                    detalles = new DetallesACobrar { Principal = 0, Ticket = 0, Exceso = 0 };
                }
                else
                {
                    if (movimiento.SistemaDePago.Id == (int)SistemaPagoCochera.PLN)
                    {
                        detalles = DetallesACobrarSistemaDePagoPlanaPorTurnos(movimiento, sesion);
                    }
                    else if (movimiento.SistemaDePago.Id == (int)SistemaPagoCochera.HOR)
                    {
                        detalles = DetallesACobrarSistemaDePagoPorHora(movimiento, sesion);
                    }
                    var precioPerdidaDeTicket = sesion.Precio(sesion.Configuracion.IdConceptoPerdidaDeTicket);
                    detalles.Ticket = movimiento.PerdidaTicket ? precioPerdidaDeTicket.Importe : 0;
                }
                return detalles;
            }
            catch (Exception e)
            {
                throw new LogicaException("ERROR al intentar determinar los importes a cobrar", e);
            }
        }


        private List<DetalleDeOperacion> DetallesDeVentaSistemaDePagoPlanaPorHoras(MovimientoCochera movimiento, SesionCochera sesionCochera)
        {
            try
            {
                List<DetalleDeOperacion> detalles = new List<DetalleDeOperacion>
                {
                    //servicio cochera
                    new DetalleDeOperacion()
                    {
                        Producto = new Concepto_Negocio_Comercial { Id = sesionCochera.Configuracion.IdConceptoServicioCocheraEnSistemaDePagoPorHora },
                        Cantidad = movimiento.TiempoSistemaPorHoras.HorasACobrar,
                        PrecioUnitario = sesionCochera.Precio(sesionCochera.Configuracion.IdConceptoServicioCocheraEnSistemaDePagoPorHora).Importe,
                        Importe = movimiento.DetallesACobrar.Exceso,
                        MascaraDeCalculo = VentasSettings.Default.MascaraDeCalculoPorDefectoEnVentas

                    }
                };
                return detalles;
            }
            catch (Exception e)
            {
                throw new LogicaException("ERROR al intentar determinar los detalles de venta", e);
            }
        }
        private List<DetalleDeOperacion> DetallesDeVentaSistemaDePagoPlanaPorTurnos(MovimientoCochera movimiento, SesionCochera sesionCochera)
        {
            try
            {
                List<DetalleDeOperacion> detalles = new List<DetalleDeOperacion>();
                //servicio cochera
                detalles.Add(new DetalleDeOperacion()
                {
                    Producto = new Concepto_Negocio_Comercial { Id = movimiento.Turno.Configuracion.IdConceptoServicioCochera },
                    Cantidad = 1,
                    PrecioUnitario = sesionCochera.Precio(movimiento.Turno.Configuracion.IdConceptoServicioCochera).Importe,
                    Importe = movimiento.DetallesACobrar.Principal,
                    MascaraDeCalculo = VentasSettings.Default.MascaraDeCalculoPorDefectoEnVentas


                });
                //exceso
                if (movimiento.DetallesACobrar.Exceso > 0)
                {
                    detalles.Add(new DetalleDeOperacion()
                    {
                        Producto = new Concepto_Negocio_Comercial { Id = movimiento.Turno.Configuracion.IdConceptoExceso },
                        Cantidad = movimiento.TiempoExcesoSistemaPlanaPorTurnos.HorasACobrar,
                        PrecioUnitario = sesionCochera.Precio(movimiento.Turno.Configuracion.IdConceptoExceso).Importe,
                        Importe = movimiento.DetallesACobrar.Exceso,
                        MascaraDeCalculo = VentasSettings.Default.MascaraDeCalculoPorDefectoEnVentas


                    });
                }
                return detalles;
            }
            catch (Exception e)
            {
                throw new LogicaException("ERROR al intentar determinar los detalles de venta", e);
            }
        }
        private List<DetalleDeOperacion> DetallesDeVenta(MovimientoCochera movimiento, SesionCochera sesion)
        {
            try
            {
                List<DetalleDeOperacion> detalles = new List<DetalleDeOperacion>();
                if (!movimiento.Vehiculo.ExoneradoDePagos && !(movimiento.SistemaDePago.Id == (int)SistemaPagoCochera.ABN))
                {
                    if (movimiento.SistemaDePago.Id == (int)SistemaPagoCochera.PLN)
                    {
                        detalles = DetallesDeVentaSistemaDePagoPlanaPorTurnos(movimiento, sesion);
                    }
                    else if (movimiento.SistemaDePago.Id == (int)SistemaPagoCochera.HOR)
                    {
                        detalles = DetallesDeVentaSistemaDePagoPlanaPorHoras(movimiento, sesion);
                    }
                    if (movimiento.PerdidaTicket)
                    {
                        detalles.Add(new DetalleDeOperacion()
                        {
                            Producto = new Concepto_Negocio_Comercial { Id = sesion.Configuracion.IdConceptoPerdidaDeTicket },
                            Cantidad = 1,
                            PrecioUnitario = sesion.Precio(sesion.Configuracion.IdConceptoPerdidaDeTicket).Importe,
                            Importe = sesion.Precio(sesion.Configuracion.IdConceptoPerdidaDeTicket).Importe,
                            MascaraDeCalculo = VentasSettings.Default.MascaraDeCalculoPorDefectoEnVentas

                        });
                    }
                    if (movimiento.VentaConsolidada)
                    {
                        detalles = new List<DetalleDeOperacion>
                        {
                            new DetalleDeOperacion()
                            {
                            Producto = new Concepto_Negocio_Comercial { Id = movimiento.Turno.Configuracion.IdConceptoServicioCochera },
                            Cantidad = 1,
                            PrecioUnitario = movimiento.DetallesACobrar.Total,
                            Importe = movimiento.DetallesACobrar.Total,
                            MascaraDeCalculo = VentasSettings.Default.MascaraDeCalculoPorDefectoEnVentas

                            }
                        };
                    }
                }
                return detalles;
            }
            catch (Exception e)
            {
                throw new LogicaException("ERROR al intentar determinar los detalles de venta", e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="movimiento"></param>
        /// <param name="TipoDeComprobante">aunque no se indique en la vista, el controlador debe definirlo. </param>
        /// <param name="userProfileSessionData"></param>
        /// <returns></returns>
        public ResultadoRegistroMovimientoCochera RegistrarSalida(MovimientoCochera movimiento, DatosVentaIntegrada datosVenta, SesionCochera sesionCochera)
        {
            OperationResult resultado = null;
            try
            {
                //traemos y modificamos la transaccion movimiento de cochera          
                //conseguimos la transaccion movimiento de cochera
                Transaccion transaccionOrigen = _transaccionRepositorio.ObtenerTransaccion(movimiento.Id);
                movimiento.Salida = DateTimeUtil.FechaActual();
                //establecemos importes e indicadores de perdida de ticket y consolidacion
                movimiento.EstablecerDatosDeSalida(transaccionOrigen);
                //Crear estado de Finalizado a la transaccion movimiento de cochera
                Estado_transaccion estadoFinalizado = new Estado_transaccion(sesionCochera.SesionDeUsuario.IdCentroDeAtencionSeleccionado, MaestroSettings.Default.IdDetalleMaestroEstadoFinalizado, movimiento.Salida, "Estado asignado al momento de ingresar a cochera");
                //Asignar estado ingresado al movimiento
                transaccionOrigen.Estado_transaccion.Add(estadoFinalizado);
                //Agregamos la transaccion origen a la transaccion 
                datosVenta.TransaccionOrigen = transaccionOrigen;
                if (movimiento.Vehiculo.ExoneradoDePagos)
                {
                    resultado = _transaccionRepositorio.ActualizarYCrear(transaccionOrigen, null, new List<Estado_transaccion>() { estadoFinalizado }, null);
                }
                else
                {
                    CompletarDatosDeVenta(datosVenta, movimiento, sesionCochera);
                    resultado = _operacionLogica.ConfirmarVentaIntegrada(ModoOperacionEnum.PorMostrador, sesionCochera.SesionDeUsuario, datosVenta);
                }
                ResultadoRegistroMovimientoCochera resultadoFinal = new ResultadoRegistroMovimientoCochera() { Movimiento = movimiento, OrdenDeVenta = movimiento.Vehiculo.ExoneradoDePagos ? null : (OrdenDeVenta)resultado.objeto };


                return resultadoFinal;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar registrar movimiento SALIDA DE COCHERA", e);
            }
        }

        /// <summary>
        /// establece datos por defecto: punto de venta, vendedor, caja, cajero.
        /// </summary>
        /// <param name="datosVenta"></param>
        /// <param name="sesionCochera"></param>
        public void CompletarDatosDeVenta(DatosVentaIntegrada datosVenta, MovimientoCochera movimiento, SesionCochera sesionCochera)
        {
            datosVenta.Orden.PuntoDeVenta = new ItemGenerico { Id = sesionCochera.IdCochera, Nombre = sesionCochera.SesionDeUsuario.CentroDeAtencionSeleccionado.Nombre };
            datosVenta.Orden.Vendedor = new ItemGenerico { Id = sesionCochera.SesionDeUsuario.Empleado.Id };
            datosVenta.Orden.NumeroBolsasDePlastico = 0;
            datosVenta.Orden.Detalles = DetallesDeVenta(movimiento, sesionCochera);
            datosVenta.Pago.Caja = datosVenta.Orden.PuntoDeVenta;
            datosVenta.Pago.Cajero = datosVenta.Orden.Vendedor;
            datosVenta.Pago.ModoDePago = ModoPago.Contado;
            datosVenta.MovimientoAlmacen = new DatosMovimientoDeAlmacen { HayComprobanteDeSalidaDeMercaderia = false };
        }

        #region Obtener
        public List<MovimientoCocheraBasico> ObtenerMovimientosDatosBasicos(DateTime fechaDesde, DateTime fechaHasta, int idCentroAtencion)
        {
            try
            {
                List<MovimientoCocheraBasico> lista = new List<MovimientoCocheraBasico>();

                lista = _cocheraRepositorio.ObtenerTransacciones_MovimientosCochera(idCentroAtencion, fechaDesde, fechaHasta).ToList();
                return lista;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener los movimientos de cochera", e);
            }
        }

        public List<EntradaSalidaUsuario> ObtenerVehiculosIngresados(int idCochera)
        {
            try
            {
                return _cocheraRepositorio.ObtenerMovimientos(idCochera, MaestroSettings.Default.IdDetalleMaestroEstadoIngresado).OrderByDescending(vi => vi.FechaHora).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener los vehículos ingresados", e);
            }
        }

        public List<EntradaSalida> ObtenerEntradasYSalidas(int idCochera, DateTime desde, DateTime hasta)
        {
            try
            {
                return _cocheraRepositorio.ObtenerMovimientos(idCochera, desde, hasta).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener las entradas y salidas de cochera", e);
            }
        }

        public Vehiculo ObtenerVehiculoPorPlaca(string placa)
        {
            try
            {
                return _cocheraRepositorio.ObtenerVehiculo(placa);
            }
            catch (Exception e)
            {
                throw new Exception("Error al intentar obtener vehiculo por placa", e);
            }
        }

        public List<ItemGenerico> ObtenerTiposDeVehiculo()
        {
            try
            {
                return ItemGenerico.Convert(_actorRepositorio.obtenerClasesDeActor(CocheraSettings.Default.IdTipoActorVehiculo).ToList());
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<ItemGenerico> ObtenerSistemasDePago()
        {
            try
            {
                List<ItemGenerico> items = new List<ItemGenerico>();
                items.Add(new ItemGenerico() { Id = (int)SistemaPagoCochera.PLN, Nombre = SistemaPagoCochera.PLN.ToString() });
                items.Add(new ItemGenerico() { Id = (int)SistemaPagoCochera.HOR, Nombre = SistemaPagoCochera.HOR.ToString() });
                return items;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar marcas de vehiculo", e);
            }
        }

        public List<ItemGenerico> ObtenerMarcasDeVehiculo()
        {
            try
            {
                return _maestroRepositorio.ObtenerDetallesComoItemsGenericos(CocheraSettings.Default.IdMaestroMarcaDeVehiculo).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar marcas de vehiculo", e);
            }
        }
        #endregion
    }
}

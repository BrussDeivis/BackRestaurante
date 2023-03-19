angular.
    module('app').
    component('registradorReserva', {
        templateUrl: "../Scripts/controller/hotel/registradorReserva/registradorReserva.html",
        bindings: {
            api: '=',
            idEstablecimiento: '<',
            changed: '&',
        },

        controller: function ($q, $scope, $timeout, SweetAlert, actorComercialService, maestroService, hotelService) {
            $('select:not(.normal)').each(function () {
                $(this).select2({
                    dropdownParent: $(this).parent()
                });
            });
            $('.td-datepicker').datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true,
                todayHighlight: true,
                language: 'es'
            });
            var ctrl = this;

            ctrl.reservaAgregada = function () {
                ctrl.changed();
            };

            ctrl.inicializar = function () {
                ctrl.limpiarReserva();
                ctrl.cargarParametros().then(function () {
                    ctrl.establecerDatosPorDefecto();
                    ctrl.iniciarComponentes();
                    ctrl.verificarInconsistenciasReserva();
                }, function (error) {
                    SweetAlert.error2("Ocurrio un Problema", error);
                });
                ctrl.cargarColeccionesAsync();
            };

            ctrl.limpiarReserva = function () {
                ctrl.reserva = {
                    Atenciones: [],
                    FacturadoGlobal: false,
                    Responsable: {},
                    Total: 0
                };
                ctrl.busqueda = {};
                ctrl.resumenHabitaciones = [];
                ctrl.reserva.Comprobante = {};
                $timeout(function () { $('#idAmbiente').trigger("change"); }, 100);
            }

            ctrl.cargarParametros = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                hotelService.obtenerParametrosParaRegistradorReserva({}).success(function (data) {
                    ctrl.parametros = data.data;
                    defered.resolve();
                }).error(function (data) {
                    SweetAlert.error2(data);
                    defered.reject(data);
                });
                return promise;
            };

            ctrl.establecerDatosPorDefecto = function () {
            };

            ctrl.iniciarComponentes = function () {
                ctrl.rolHuesped = { Id: ctrl.parametros.IdRolCliente, Nombre: 'CLIENTE' };
                ctrl.rolUsuario = { Id: ctrl.parametros.IdRolCliente, Nombre: 'CLIENTE' };
                ctrl.inicializacionRealizada = true;
            };

            ctrl.cargarColeccionesAsync = function () {
                ctrl.obtenerTiposHabitacion();
                ctrl.obtenerAmbientesHabitacion();
                ctrl.obtenerMotivosDeViaje();
            };

            ctrl.obtenerTiposHabitacion = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                hotelService.obtenerTiposHabitacionVigentesSimplificado({}).success(function (data) {
                    ctrl.tiposHabitacion = data;
                    defered.resolve();
                }).error(function (data) {
                    SweetAlert.error2(data);
                    defered.reject(data);
                });
                return promise;
            };

            ctrl.obtenerAmbientesHabitacion = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                hotelService.obtenerAmbientesVigentesPorEstablecimientoSimplificado({ idEstablecimiento: ctrl.idEstablecimiento }).success(function (data) {
                    ctrl.ambientesHabitacion = data;
                    defered.resolve();
                }).error(function (data) {
                    SweetAlert.error2(data);
                    defered.reject(data);
                });
                return promise;
            };

            ctrl.obtenerMotivosDeViaje = function () {
                maestroService.obtenerMotivosDeViaje().success(function (data) {
                    ctrl.motivosDeViaje = data;
                }).error(function (data) {
                    SweetAlert.error2(data);
                });
            };

            ctrl.obtenerHabitacionesDisponibles = function () {
                let idAmbiente = (ctrl.busqueda.Ambiente == undefined || ctrl.busqueda.Ambiente == null) ? 0 : ctrl.busqueda.Ambiente.Id;
                hotelService.obtenerHabitacionesDisponibles({ idTipoHabitacion: ctrl.busqueda.TipoHabitacion.Id, fechaDesde: ctrl.busqueda.FechaDesde, fechaHasta: ctrl.busqueda.FechaHasta, idEstablecimiento: ctrl.idEstablecimiento, idAmbiente: idAmbiente }).success(function (data) {
                    ctrl.limpiarHabitacionesDisponibles();
                    let habitacionesPosibles = data;
                    if (habitacionesPosibles.length > 0 && ctrl.reserva.Atenciones.length > 0) {
                        for (var i = 0; i < habitacionesPosibles.length; i++) {
                            let estadoHabitacion = true;
                            for (var j = 0; j < ctrl.reserva.Atenciones.length; j++) {
                                if (habitacionesPosibles[i].Id == ctrl.reserva.Atenciones[j].Habitacion.Id) {
                                    if ((ctrl.convertirFecha(ctrl.busqueda.FechaDesde) >= ctrl.convertirFecha(ctrl.reserva.Atenciones[j].FechaIngreso) && ctrl.convertirFecha(ctrl.busqueda.FechaDesde) < ctrl.convertirFecha(ctrl.reserva.Atenciones[j].FechaSalida)) || (ctrl.convertirFecha(ctrl.busqueda.FechaHasta) > ctrl.convertirFecha(ctrl.reserva.Atenciones[j].FechaIngreso) && ctrl.convertirFecha(ctrl.busqueda.FechaHasta) <= ctrl.convertirFecha(ctrl.reserva.Atenciones[j].FechaSalida))) {
                                        estadoHabitacion = false;
                                    }
                                }
                            }
                            if (estadoHabitacion) {
                                ctrl.habitacionesDisponibles.push(habitacionesPosibles[i]);
                            }
                        }
                    } else {
                        ctrl.habitacionesDisponibles = habitacionesPosibles;
                    }
                }).error(function (data) {
                    SweetAlert.error2(data);
                });
            };

            ctrl.seleccionarHabitacionDisponible = function (habitacionDisponible) {
                let posicion = ctrl.habitacionesDisponibles.indexOf(habitacionDisponible);
                ctrl.habitacionesDisponibles.splice(posicion, 1);
                let nuevoDetalleReserva = {
                    Habitacion: habitacionDisponible,
                    Huespedes: []
                };
                nuevoDetalleReserva.FechaIngreso = ctrl.busqueda.FechaDesde;
                nuevoDetalleReserva.FechaSalida = ctrl.busqueda.FechaHasta;
                nuevoDetalleReserva.Noches = ctrl.diferenciaDiasEntreFechas(ctrl.busqueda.FechaDesde, ctrl.busqueda.FechaHasta);
                nuevoDetalleReserva.PrecioUnitario = nuevoDetalleReserva.Habitacion.Precio.Valor;
                nuevoDetalleReserva.Importe = nuevoDetalleReserva.Noches * nuevoDetalleReserva.Habitacion.Precio.Valor;
                ctrl.reserva.Atenciones.push(nuevoDetalleReserva);
                ctrl.calcularResumenReserva();
            }


            ctrl.calcularTotalDetalleReserva = function (index) {
                ctrl.reserva.Atenciones[index].PrecioUnitario = ctrl.reserva.Atenciones[index].Habitacion.Precio.Valor;
                ctrl.reserva.Atenciones[index].Importe = ctrl.reserva.Atenciones[index].Noches * ctrl.reserva.Atenciones[index].PrecioUnitario;
                ctrl.calcularResumenReserva();
            }

            ctrl.calcularResumenReserva = function () {
                let totalReserva = 0;
                ctrl.resumenHabitaciones = [];
                let idTiposHabitacionesDistintas = [];
                for (var i = 0; i < ctrl.reserva.Atenciones.length; i++) {
                    if (!idTiposHabitacionesDistintas.find(m => m == ctrl.reserva.Atenciones[i].Habitacion.TipoHabitacion.Id)) {
                        idTiposHabitacionesDistintas.push(ctrl.reserva.Atenciones[i].Habitacion.TipoHabitacion.Id);
                    }
                }
                for (var i = 0; i < idTiposHabitacionesDistintas.length; i++) {
                    let tipoHabitacion = ctrl.reserva.Atenciones.filter(m => m.Habitacion.TipoHabitacion.Id == idTiposHabitacionesDistintas[i]);
                    let cantidadNoches = 0;
                    for (var j = 0; j < tipoHabitacion.length; j++) {
                        cantidadNoches += tipoHabitacion[j].Noches;
                    }
                    let habitacionResumen = { Nombre: tipoHabitacion[0].Habitacion.TipoHabitacion.Nombre, Cantidad: cantidadNoches, PrecioUnitario: tipoHabitacion[0].Habitacion.Precio.ValorString, Importe: (cantidadNoches * tipoHabitacion[0].Habitacion.Precio.Valor).toFixed(2) };
                    totalReserva += parseFloat(habitacionResumen.Importe);
                    ctrl.resumenHabitaciones.push(habitacionResumen);
                }
                ctrl.reserva.Total = totalReserva;
                if (ctrl.reserva.FacturadoGlobal)
                    ctrl.facturacionVentaAPI.SetTotalVenta(ctrl.reserva.Total);
                ctrl.verificarInconsistenciasReserva();
            }

            ctrl.quitarHabitacionSeleccinada = function (index) {
                ctrl.reserva.Atenciones.splice(index, 1);
                ctrl.calcularResumenReserva();
            }

            ctrl.cambioResponsable = function (actorComercial) {
                ctrl.reserva.Responsable = actorComercial;
                ctrl.verificarInconsistenciasReserva();
            };

            ctrl.agregarHuespedHabitacion = function (index) {
                ctrl.huesped.MotivoDeViaje = ctrl.motivoDeViaje;
                ctrl.reserva.Atenciones[index].Huespedes.push(angular.copy(ctrl.huesped));
                ctrl.verificarInconsistenciasReserva();
            };

            ctrl.quitarHuespedHabitacion = function (detalle, huesped) {
                let posicionDetalle = ctrl.reserva.Atenciones.indexOf(detalle);
                let posicionHuesped = ctrl.reserva.Atenciones[posicionDetalle].Huespedes.indexOf(huesped);
                ctrl.reserva.Atenciones[posicionDetalle].Huespedes.splice(posicionHuesped, 1);
            };

            ctrl.diferenciaDiasEntreFechas = function (fechaInicio, fechaFin) {
                var dt1 = fechaInicio.split('/'),
                    dt2 = fechaFin.split('/'),
                    one = new Date(dt1[2], dt1[1] - 1, dt1[0]),
                    two = new Date(dt2[2], dt2[1] - 1, dt2[0]);
                var millisecondsPerDay = 1000 * 60 * 60 * 24;
                var millisBetween = two.getTime() - one.getTime();
                var days = millisBetween / millisecondsPerDay;
                return Math.floor(days);
            }

            ctrl.convertirFecha = function (fecha) {
                let datosFecha = fecha.split('/');
                let fechaDate = new Date(datosFecha[2], datosFecha[1] - 1, datosFecha[0]);
                return fechaDate;
            }

            ctrl.fechaHastaMayorAlMaximo = function (fecha) {
                let datosFecha = fecha.split('/');
                let fechaDate = new Date(datosFecha[2], datosFecha[1] - 1, datosFecha[0]);

                return fechaDate;
            }

            ctrl.limpiarHabitacionesDisponibles = function () {
                ctrl.habitacionesDisponibles = [];
            }

            ctrl.nuevaReserva = function () {
                ctrl.obtenerAmbientesHabitacion();
                ctrl.limpiarReserva();
                ctrl.limpiarHabitacionesDisponibles();
                ctrl.selectorUsuarioAPI.EstablecerActorPorDefecto();
            }

            ctrl.nuevaReservaCargada = function (idHabitacion, fechaDesde, fechaHasta) {
                ctrl.nuevaReserva();
                ctrl.busqueda.FechaDesde = fechaDesde;
                ctrl.busqueda.FechaHasta = fechaHasta;
                ctrl.obtenerHabitacionDisponible(idHabitacion).then(function () {
                    ctrl.habitacionesDisponibles[0].Precio = ctrl.habitacionesDisponibles[0].TipoHabitacion.Precios[0];
                    ctrl.seleccionarHabitacionDisponible(ctrl.habitacionesDisponibles[0]);
                }, function (error) {
                    SweetAlert.error2("Ocurrio un Problema", error);
                });
            }

            ctrl.obtenerHabitacionDisponible = function (idHabitacion) {
                var defered = $q.defer();
                var promise = defered.promise;
                hotelService.obtenerHabitacionDisponible({ idHabitacion: idHabitacion }).success(function (data) {
                    ctrl.habitacionesDisponibles.push(data);
                    defered.resolve();
                }).error(function (data) {
                    SweetAlert.error2(data);
                    defered.reject(data);
                });
                return promise;
            };

            ctrl.guardarReserva = function () {
                hotelService.guardarReserva({ reserva: ctrl.reserva }).success(function (data) {
                    SweetAlert.success("Correcto", data.result_description);
                    ctrl.cerrarReserva();
                    ctrl.reservaAgregada();
                }).error(function (data, status) {
                    SweetAlert.error2(data);
                });
            }

            ctrl.checkInReserva = function () {
                hotelService.checkInReserva({ reserva: ctrl.reserva }).success(function (data) {
                    SweetAlert.success("Correcto", data.result_description);
                    ctrl.cerrarReserva();
                    ctrl.reservaAgregada();
                }).error(function (data, status) {
                    SweetAlert.error2(data);
                });
            }

            ctrl.hayAtencionesSinHuespedes = function () {
                let hayAtencionesSinHuespedes = false; 
                for (var i = 0; i < ctrl.reserva.Atenciones.length; i++) {
                    hayAtencionesSinHuespedes = hayAtencionesSinHuespedes || (ctrl.reserva.Atenciones[i].Huespedes.length == 0);
                }
                return hayAtencionesSinHuespedes;
            }

            ctrl.cerrarReserva = function () {
                $('#modal-registrador-reserva').modal('hide');
            }

            ctrl.verificarInconsistenciasReserva = function () {
                ctrl.inconsistenciasReserva = [];
                if (ctrl.reserva.Atenciones.length == 0) {
                    ctrl.inconsistenciasReserva.push("Es necesario seleccionar alguna habitación.");
                }
                if (ctrl.reserva.Responsable.Id == undefined || ctrl.reserva.Responsable.Id == ctrl.parametros.IdClienteGenerico) {
                    ctrl.inconsistenciasReserva.push("Es necesario seleccionar el responsable de la reserva.");
                }
                if (ctrl.reserva.Total == undefined || ctrl.reserva.Total == 0) {
                    ctrl.inconsistenciasReserva.push("Es necesario que el total sea mayor a 0.");
                }
                ctrl.reserva.esValido = ctrl.inconsistenciasReserva.length > 0 ? false : true;
            }

            this.$onInit = function () {
                ctrl.api = {};
                ctrl.inicializar();
                ctrl.api.NuevaReserva = ctrl.nuevaReserva;
                ctrl.api.NuevaReservaCargada = ctrl.nuevaReservaCargada;
                //ctrl.api.InicializarRegistroGuiaRemision = ctrl.inicializarRegistroGuiaRemision;
                //ctrl.api.LimpiarGuiaRemision = ctrl.limpiarGuiaRemision;
                //ctrl.api.CargarDatosDesdeVenta = ctrl.cargarDatosDesdeVenta;
                //ctrl.api.NuevoRegistroGuiaRemision = ctrl.nuevoRegistroGuiaRemision;
            };
        }
    });

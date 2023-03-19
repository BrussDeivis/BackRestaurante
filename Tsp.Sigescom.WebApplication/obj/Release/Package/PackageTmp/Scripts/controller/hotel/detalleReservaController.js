app.controller('detalleReservaController', function ($scope, $q, $timeout, hotelService, centroDeAtencionService, $rootScope, SweetAlert) {

    $scope.inicializar = function () {
        $scope.cargarParametros();
        $scope.inicializarComponentes();
        $scope.limpiarAtencionMacro();
        $scope.cargarAtencionMacro();
    }

    $scope.cargarParametros = function () {
        $scope.accionesProcesoHabitacion = accionesProcesoHabitacion;
        $scope.idAtencion = idAtencion;
        $scope.idAtencionMacro = idAtencionMacro;
        $scope.idClienteGenerico = idClienteGenerico;
        $scope.tiempoEsperaBusquedaSelector = tiempoEsperaBusquedaSelector;
        $scope.minimoCaracteresBuscarActorComercial = minimoCaracteresBuscarActorComercial;
        $scope.mascaraDeVisualizacionValidacionRegistroCliente = mascaraDeVisualizacionValidacionRegistroCliente;
        $scope.idEstadoRegistrado = idEstadoRegistrado;
        $scope.idEstadoConfirmado = idEstadoConfirmado;
        $scope.idEstadoCheckedIn = idEstadoCheckedIn;
        $scope.idEstadoCheckedOut = idEstadoCheckedOut;
        $scope.idEstadoFacturado = idEstadoFacturado;
        $scope.idEstadoAnulado = idEstadoAnulado;
        $scope.idEstadoEntradaCambiado = idEstadoEntradaCambiado;
        $scope.idEstadoSalidaCambiado = idEstadoSalidaCambiado;
        $scope.fechaActual = fechaActual;
        $scope.idEstablecimiento = idEstablecimiento;
    }

    $scope.limpiarAtencionMacro = function () {
        $scope.atencionMacro = { Atenciones: [] };
        $scope.accionGlobal = false;
        $scope.observacionConfirmar = '';
        $scope.observacionCheckIn = '';
        $scope.observacionChechOut = '';
        $scope.observacionAnular = '';
    }

    $scope.establecerDatosPorDefecto = function () {
    }

    $scope.inicializarComponentes = function () {
        $scope.rolCliente = { Id: idRolCliente, Nombre: 'CLIENTE' };
        $scope.inicializacionRealizada = true;
    }

    $scope.cargarAtencionMacro = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        hotelService.obtenerAtencionMacroHotel({ idAtencion: $scope.idAtencionMacro }).success(function (data) {
            $scope.atencionMacro = data;
            $scope.atencionMacro.SrcImagenVoucherExtranet = "data:image/png;base64," + $scope.atencionMacro.ImagenVoucherExtranet;
            //$scope.atencionMacro.SrcImagenVoucherExtranet = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAUAAAAFCAYAAACNbyblAAAAHElEQVQI12P4//8/w38GIAXDIBKE0DHxgljNBAAO9TXL0Y4OHwAAAABJRU5ErkJggg==";
            $timeout(function () { $('#imagenVoucher').trigger("change"); }, 100);
            $scope.obtenerTotalesAtencionMacro();
            $scope.selectorResponsableAPI.SetActorComercialPorId($scope.atencionMacro.Responsable.Id);
            defered.resolve();
        }).error(function (data) {
            SweetAlert.error2(data);
            defered.reject(data);
        });
        return promise;
    }

    $scope.obtenerTotalesAtencionMacro = function () {
        let numeroNoches = 0; let numeroHuespedes = 0; let importeTotal = 0;
        for (var i = 0; i < $scope.atencionMacro.Atenciones.length; i++) {
            if ($scope.atencionMacro.Atenciones[i].EstadoActual.Id != $scope.idEstadoAnulado) {
                numeroNoches += $scope.atencionMacro.Atenciones[i].Noches;
                numeroHuespedes += $scope.atencionMacro.Atenciones[i].Importe > 0 ? $scope.atencionMacro.Atenciones[i].Huespedes.length : 0;
                importeTotal += $scope.atencionMacro.Atenciones[i].Importe;
            }
        }
        $scope.atencionMacro.TotalNoches = numeroNoches;
        $scope.atencionMacro.TotalHuespedes = numeroHuespedes;
        $scope.atencionMacro.Total = importeTotal;
    }

    $scope.mostrarObservacioEstado = function (estado) {
        Swal.fire(estado.Nombre, estado.Observacion);
    }

    $scope.cargarAtencionParaFacturacion = function (id) {
        var defered = $q.defer();
        var promise = defered.promise;
        if ($scope.accionGlobal) {
            hotelService.obtenerAtencionDesdeAtencionMacro({ idAtencionMacro: id }).success(function (data) {
                $scope.atencionAfacturar = data;
                defered.resolve();
            }).error(function (data) {
                SweetAlert.error2(data);
                defered.reject(data);
            });
        } else {
            hotelService.obtenerAtencionDesdeAtencion({ idAtencion: id }).success(function (data) {
                $scope.atencionAfacturar = data;
                defered.resolve();
            }).error(function (data) {
                SweetAlert.error2(data);
                defered.reject(data);
            });
        }
        return promise;
    }

    $scope.puedeConfirmarTodasLasAtenciones = function () {
        let numeroAtencionesPuedenConfirmar = $scope.atencionMacro.Atenciones.filter(a => a.PuedeConfirmar).length;
        return ($scope.atencionMacro.Atenciones.length == numeroAtencionesPuedenConfirmar);
    }

    $scope.puedeCheckInTodasLasAtenciones = function () {
        let numeroAtencionesPuedenCheckIn = $scope.atencionMacro.Atenciones.filter(a => a.PuedeCheckIn).length;
        return ($scope.atencionMacro.Atenciones.length == numeroAtencionesPuedenCheckIn);
    }

    $scope.puedeCheckOutTodasLasAtenciones = function () {
        let numeroAtencionesPuedenCheckOut = $scope.atencionMacro.Atenciones.filter(a => a.PuedeCheckOut).length;
        return ($scope.atencionMacro.Atenciones.length == numeroAtencionesPuedenCheckOut);
    }

    $scope.puedeFacturarTodasLasAtenciones = function () {
        let numeroAtencionesPuedenFacturar = $scope.atencionMacro.Atenciones.filter(a => a.PuedeFacturar).length;
        return ($scope.atencionMacro.Atenciones.length == numeroAtencionesPuedenFacturar);
    }

    $scope.activarBotonesGlobales = function () {
        let numeroAtenciones = $scope.atencionMacro.Atenciones.filter(a => a.Importe > 0).length;
        return numeroAtenciones > 1;
    }

    $scope.estanFacturadasTodasLasAtenciones = function () {
        let numeroAtencionesFacturado = $scope.atencionMacro.Atenciones.filter(a => a.Facturado && a.Importe > 0).length;
        return ($scope.atencionMacro.Atenciones.filter(a => a.Importe > 0).length == numeroAtencionesFacturado);
    }

    $scope.estanIngresoTodasLasAtenciones = function () {
        let numeroAtencionesCheckedIn = $scope.atencionMacro.Atenciones.filter(a => a.EstadoActual.Id == $scope.idEstadoCheckedIn || a.EstadoActual.Id == $scope.idEstadoEntradaCambiado).length;
        return ($scope.atencionMacro.Atenciones.filter(a => a.Importe > 0).length == numeroAtencionesCheckedIn);
    }

    $scope.puedeAnularTodasLasAtenciones = function () {
        var numeroAtencionesPuedenAnular = $scope.atencionMacro.Atenciones.filter(a => a.PuedeAnular).length;
        return ($scope.atencionMacro.Atenciones.length == numeroAtencionesPuedenAnular);
    }

    $scope.desabilitarCheckInTodasLasAtenciones = function () {
        let numeroAtencionesConHuesped = $scope.atencionMacro.Atenciones.filter(a => a.Huespedes.length > 0).length;
        return ($scope.atencionMacro.Atenciones.length != numeroAtencionesConHuesped);
    }

    $scope.desabilitarCheckOutTodasLasAtenciones = function () {
        let numeroAtencionesFacturadas = $scope.atencionMacro.Atenciones.filter(a => a.Facturado).length;
        return ($scope.atencionMacro.Atenciones.length != numeroAtencionesFacturadas);
    }

    $scope.abrirEditarResponsable = function () {
        $scope.respaldoResponsable = angular.copy($scope.atencionMacro.Responsable);
        $('#modal-editar-responsable').modal('show');
    }

    $scope.cerrarResponsable = function () {
        $scope.atencionMacro.Responsable = $scope.respaldoResponsable;
        $('#modal-editar-responsable').modal('hide');
    };

    $scope.guardarResponsable = function () {
        hotelService.editarResponsableAtencionMacro({ idAtencionMacro: $scope.atencionMacro.Id, idResponsable: $scope.atencionMacro.Responsable.Id }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $('#modal-editar-responsable').modal('hide');
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    };

    $scope.abrirEditarFechaAtencion = function (index) {
        $scope.indexAtencion = index;
        $scope.atencionEditarFecha = angular.copy($scope.atencionMacro.Atenciones[index]);
        $scope.atencionEditarFecha.FechaIngreso = $scope.atencionEditarFecha.FechaIngresoString;
        $scope.atencionEditarFecha.FechaSalida = $scope.atencionEditarFecha.FechaSalidaString;
        $('#modal-editar-fecha-reserva').modal('show');
    }

    $scope.diferenciaDiasEntreFechas = function (fechaInicio, fechaFin) {
        var dt1 = fechaInicio.split('/'),
            dt2 = fechaFin.split('/'),
            one = new Date(dt1[2], dt1[1] - 1, dt1[0]),
            two = new Date(dt2[2], dt2[1] - 1, dt2[0]);
        var millisecondsPerDay = 1000 * 60 * 60 * 24;
        var millisBetween = two.getTime() - one.getTime();
        var days = millisBetween / millisecondsPerDay;
        return Math.floor(days);
    }

    $scope.cerrarFechaAtencion = function () {
        $scope.atencionEditarFecha = {};
        $('#modal-editar-fecha-reserva').modal('hide');
    };

    $scope.guardarFechaAtencion = function () {
        $scope.atencionEditarFecha.Noches = $scope.diferenciaDiasEntreFechas($scope.atencionEditarFecha.FechaIngreso, $scope.atencionEditarFecha.FechaSalida);
        hotelService.editarFechaAtencion({ atencion: $scope.atencionEditarFecha }).success(function (data) {
            $scope.atencionMacro.Total = parseFloat($scope.atencionMacro.Total) - parseFloat($scope.atencionMacro.Atenciones[$scope.indexAtencion].Importe) + parseFloat($scope.atencionEditarFecha.Noches * $scope.atencionEditarFecha.PrecioUnitario);
            $scope.atencionMacro.Atenciones[$scope.indexAtencion].FechaIngreso = $scope.atencionEditarFecha.FechaIngreso;
            $scope.atencionMacro.Atenciones[$scope.indexAtencion].FechaSalida = $scope.atencionEditarFecha.FechaSalida;
            $scope.atencionMacro.Atenciones[$scope.indexAtencion].FechaIngresoString = $scope.atencionEditarFecha.FechaIngreso;
            $scope.atencionMacro.Atenciones[$scope.indexAtencion].FechaSalidaString = $scope.atencionEditarFecha.FechaSalida;
            $scope.atencionMacro.Atenciones[$scope.indexAtencion].Noches = $scope.atencionEditarFecha.Noches;
            $scope.atencionMacro.Atenciones[$scope.indexAtencion].Importe = $scope.atencionEditarFecha.Noches * $scope.atencionEditarFecha.PrecioUnitario;
            $scope.obtenerTotalesAtencionMacro();
            SweetAlert.success("Correcto", data.result_description);
            $('#modal-editar-fecha-reserva').modal('hide');
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    };

    $scope.puedeEditarResponsable = function () {
        let numeroAtencionesEnCheckOut = $scope.atencionMacro.Atenciones.filter(a => a.EstadoActual.Id == $scope.idEstadoCheckedOut).length;
        let numeroAtencionesEnAnulado = $scope.atencionMacro.Atenciones.filter(a => a.EstadoActual.Id == $scope.idEstadoAnulado).length;
        return ($scope.atencionMacro.Atenciones.length != numeroAtencionesEnCheckOut + numeroAtencionesEnAnulado);
    }

    $scope.puedeCambiarModoFacturacion = function () {
        let numeroAtencionesEnCheckOut = $scope.atencionMacro.Atenciones.filter(a => a.EstadoActual.Id == $scope.idEstadoCheckedOut).length;
        let numeroAtencionesEnAnulado = $scope.atencionMacro.Atenciones.filter(a => a.EstadoActual.Id == $scope.idEstadoAnulado).length;
        return ($scope.atencionMacro.Atenciones.length != numeroAtencionesEnCheckOut + numeroAtencionesEnAnulado);
    }

    $scope.abrirConfirmarAtencion = function (index) {
        $scope.accionGlobal = false;
        $scope.observacionConfirmar = '';
        $scope.indexAtencion = index;
        $('#modal-confirmar-reserva').modal('show');
    }

    $scope.abrirCheckInAtencion = function (index) {
        $scope.accionGlobal = false;
        $scope.observacionCheckIn = '';
        $scope.indexAtencion = index;
        $('#modal-registrar-checkIn').modal('show');
    }



    $scope.abrirFacturarAtencion = function (index) {
        $scope.accionGlobal = false;
        $scope.indexAtencion = index;
        let idAtencion = $scope.atencionMacro.Atenciones[index].Id;
        $scope.cargarAtencionParaFacturacion(idAtencion).then(function (result) {
            $scope.inicializacionFacturarRealizada = true;
            $scope.facturadorAPI.SetAtencionAFacturar($scope.atencionAfacturar);
            $('#modal-facturador').modal('show');
        }, function (error) {
            SweetAlert.error("Ocurrio un Problema", error);
        });
    }

    $scope.abrirCheckOutAtencion = function (index) {
        $scope.accionGlobal = false;
        $scope.observacionChechOut = '';
        $scope.indexAtencion = index;
        $('#modal-registrar-checkOut').modal('show');
    }

    $scope.abrirAnularAtencion = function (index) {
        $scope.accionGlobal = false;
        $scope.hayAnulacion = true;
        $scope.observacionAnular = '';
        $scope.anulacion = {};
        $scope.indexAtencion = index;
        $scope.anulacion.MontoAnulacion = 0;
        $scope.anulacion.MontoDescuento = 0;
        $scope.anulacion.MontoDiferencia = 0;
        $scope.obtenerComprobantesParaOperaciones(true);
        $('#modal-anular-reserva').modal('show');
    }

    $scope.abrirIncidenteAtencion = function (index) {
        $scope.accionGlobal = false;
        $scope.hayIncidente = true;
        $scope.justificacionIncidente = '';
        $scope.incidente = {};
        $scope.indexAtencion = index;
        $scope.incidente.Total = 0;
        $scope.incidente.MontoHospedaje = 0;
        $scope.incidente.MontoDescuento = 0;
        $scope.incidente.MontoDiferencia = 0;
        $scope.obtenerComprobantesParaOperaciones(false);
        $scope.incidente.EsDevolucion = 'true';
        $scope.incidente.EsPorcentaje = 'false';
        $scope.incidente.DevolucionPorcentaje = '';
        $scope.incidente.DevolucionSoles = '';
        $scope.incidente.TotalPorcentaje = 0;
        $scope.incidente.TotalSoles = 0;
        $('#modal-registro-incidente').modal('show');
    }

    $scope.abrirCambiarHabitacionAtencion = function (index) {
        $scope.accionGlobal = false;
        $scope.indexAtencion = index;
        $scope.informacionHabitacionACambiar = $scope.atencionMacro.Atenciones[index].Habitacion.TipoHabitacion.Nombre + ' - ' + $scope.atencionMacro.Atenciones[index].Habitacion.CodigoHabitacion;
        $scope.atencionCambioHabitacion = angular.copy($scope.atencionMacro.Atenciones[index]);
        $scope.idTipoHabitacion = $scope.atencionCambioHabitacion.Habitacion.TipoHabitacion.Id;
        $scope.atencionCambioHabitacion.Habitacion = undefined;
        $scope.atencionCambioHabitacion.FechaIngreso = fechaActual;
        $scope.atencionCambioHabitacion.FechaSalida = $scope.atencionCambioHabitacion.FechaSalidaString;
        $scope.atencionCambioHabitacion.Noches = $scope.diferenciaDiasEntreFechas($scope.atencionCambioHabitacion.FechaIngreso, $scope.atencionCambioHabitacion.FechaSalidaString);
        $scope.atencionCambioHabitacion.Importe = $scope.atencionCambioHabitacion.Noches * $scope.atencionCambioHabitacion.PrecioUnitario;
        $scope.obtenerHabitacionesDisponibles();
        $('#modal-cambiar-habitacion').modal('show');
    }

    $scope.obtenerHabitacionesDisponibles = function () {
        hotelService.obtenerHabitacionesDisponibles({ idTipoHabitacion: $scope.idTipoHabitacion, fechaDesde: $scope.atencionCambioHabitacion.FechaIngreso, fechaHasta: $scope.atencionCambioHabitacion.FechaSalida, idEstablecimiento: $scope.idEstablecimiento, idAmbiente: 0 }).success(function (data) {
            $scope.habitacionesDisponibles = data;
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    };
    $scope.cambiarHabitacion = function () {
        hotelService.cambiarHabitacionAtencion({ atencionCambioHabitacion: $scope.atencionCambioHabitacion }).success(function (data) {
            $scope.habitacionesDisponibles = [];
            $('#modal-cambiar-habitacion').modal('hide');
            $scope.cargarAtencionMacro();
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    };



    $scope.cerrarModal = function (nombreDelModal) {
        $('#' + nombreDelModal).modal('hide');
    }

    $scope.abrirConfirmarAtencionMacro = function () {
        $scope.accionGlobal = true;
        $scope.observacionConfirmar = '';
        $('#modal-confirmar-reserva').modal('show');
    }

    $scope.abrirCheckInAtencionMacro = function () {
        $scope.accionGlobal = true;
        $scope.observacionCheckIn = '';
        $('#modal-registrar-checkIn').modal('show');
    }
    $scope.abrirFacturarAtencionMacro = function () {
        $scope.accionGlobal = true;
        $scope.cargarAtencionParaFacturacion($scope.atencionMacro.Id).then(function (result) {
            $scope.inicializacionFacturarRealizada = true;
            $scope.facturadorAPI.SetAtencionAFacturar($scope.atencionAfacturar);
            $('#modal-facturador').modal('show');
        }, function (error) {
            SweetAlert.error("Ocurrio un Problema", error);
        });
    }

    $scope.abrirCheckOutAtencionMacro = function () {
        $scope.accionGlobal = true;
        $scope.observacionChechOut = '';
        $('#modal-registrar-checkOut').modal('show');
    }

    $scope.abrirAnularAtencionMacro = function () {
        $scope.accionGlobal = true;
        $scope.observacionAnular = '';
        $scope.anulacion = {};
        $scope.anulacion.MontoAnulacion = 0;
        $scope.anulacion.MontoDescuento = 0;
        $scope.anulacion.MontoDiferencia = 0;
        $scope.obtenerComprobantesParaOperaciones(true);
        $('#modal-anular-reserva').modal('show');
    }

    $scope.abrirIncidenteAtencionMacro = function () {
        $scope.accionGlobal = true;
        $scope.justificacionIncidente = '';
        $scope.incidente = {};
        $scope.incidente.Total = 0;
        $scope.incidente.MontoHospedaje = 0;
        $scope.incidente.MontoDescuento = 0;
        $scope.incidente.MontoDiferencia = 0;
        $scope.obtenerComprobantesParaOperaciones(false);
        $scope.incidente.EsDevolucion = 'true';
        $scope.incidente.EsPorcentaje = 'false';
        $scope.incidente.DevolucionPorcentaje = '';
        $scope.incidente.DevolucionSoles = '';
        $scope.incidente.TotalPorcentaje = 0;
        $scope.incidente.TotalSoles = 0;
        $('#modal-registro-incidente').modal('show');
    }

    $scope.guardarAnotacion = function (index) {
        if ($scope.atencionMacro.Atenciones[index].Anotacion != undefined && $scope.atencionMacro.Atenciones[index].Anotacion != '') {
            hotelService.guardarAnotacion({ atencion: $scope.atencionMacro.Atenciones[index] }).success(function (data) {
                if ($scope.atencionMacro.Atenciones[index].Anotaciones == null || $scope.atencionMacro.Atenciones[index].Anotaciones == undefined)
                    $scope.atencionMacro.Atenciones[index].Anotaciones = [];
                $scope.atencionMacro.Atenciones[index].Anotaciones.push(data.information.Anotaciones[data.information.Anotaciones.length - 1]);
                $scope.atencionMacro.Atenciones[index].Anotacion = '';
            }).error(function (data) {
                SweetAlert.error2(data);
            });
        }
    }

    $scope.actualizarEstadoAtencionMacroFacturar = function (infosEstado) {
        for (var i = 0; i < $scope.atencionMacro.Atenciones.length; i++) {
            if ($scope.atencionMacro.Atenciones[i].Importe > 0) {
                let infoEstado = infosEstado.find(ie => ie.IdAuxiliar == $scope.atencionMacro.Atenciones[i].Id);
                $scope.actualizarEstadoAtencion(infoEstado, i);
            }
        }
    }

    $scope.actualizarEstadoAtencionMacro = function (infoEstado) {
        for (var i = 0; i < $scope.atencionMacro.Atenciones.length; i++) {
            $scope.actualizarEstadoAtencion(infoEstado, i);
        }
    }
    $scope.actualizarEstadoAtencion = function (infoEstado, indexAtencion) {
        $scope.atencionMacro.Atenciones[indexAtencion].Estados.push(infoEstado.EstadoEventoFinal);
        $scope.atencionMacro.Atenciones[indexAtencion].EstadoActual = infoEstado.EstadoActual;
        $scope.atencionMacro.Atenciones[indexAtencion].PuedeFacturar = infoEstado.PuedeFacturar;
        $scope.atencionMacro.Atenciones[indexAtencion].PuedeConfirmar = infoEstado.PuedeConfirmar;
        $scope.atencionMacro.Atenciones[indexAtencion].PuedeCheckIn = infoEstado.PuedeCheckIn;
        $scope.atencionMacro.Atenciones[indexAtencion].PuedeCheckOut = infoEstado.PuedeCheckOut;
        $scope.atencionMacro.Atenciones[indexAtencion].PuedeAnular = infoEstado.PuedeAnular;
        $scope.atencionMacro.Atenciones[indexAtencion].PuedeCambiarHabitacion = infoEstado.PuedeCambiarHabitacion;
        if (infoEstado.EstadoEventoFinal.Id == $scope.idEstadoFacturado) {
            $scope.atencionMacro.FacturadoGlobal = infoEstado.FacturadoGlobal;
            $scope.atencionMacro.TieneFacturacion = infoEstado.TieneFacturacion;
            $scope.atencionMacro.Atenciones[indexAtencion].FacturadoGlobal = infoEstado.FacturadoGlobal;
            $scope.atencionMacro.Atenciones[indexAtencion].TieneFacturacion = infoEstado.TieneFacturacion;
            $scope.atencionMacro.Atenciones[indexAtencion].Facturado = infoEstado.Facturado;
        }
    }

    $scope.confirmarAtencion = function () {
        if ($scope.observacionConfirmar != undefined || $scope.observacionConfirmar != '') {
            if ($scope.accionGlobal) {
                hotelService.confirmarAtencionMacro({ idAtencionMacro: $scope.atencionMacro.Id, observacion: $scope.observacionConfirmar }).success(function (data) {
                    $scope.actualizarEstadoAtencionMacro(data.information);
                    $scope.observacionConfirmar = '';
                    SweetAlert.success("Correcto", data.result_description);
                    $('#modal-confirmar-reserva').modal('hide');
                }).error(function (data) {
                    SweetAlert.error2(data);
                });
            } else {
                hotelService.confirmarAtencion({ idAtencion: $scope.atencionMacro.Atenciones[$scope.indexAtencion].Id, observacion: $scope.observacionConfirmar }).success(function (data) {
                    $scope.actualizarEstadoAtencion(data.information, $scope.indexAtencion);
                    $scope.observacionConfirmar = '';
                    SweetAlert.success("Correcto", data.result_description);
                    $('#modal-confirmar-reserva').modal('hide');
                }).error(function (data) {
                    SweetAlert.error2(data);
                });
            }
        }
    }

    $scope.checkInAtencion = function (index) {
        if ($scope.observacionCheckIn != undefined || $scope.observacionCheckIn != '') {
            if ($scope.accionGlobal) {
                hotelService.checkInAtencionMacro({ idAtencionMacro: $scope.atencionMacro.Id, observacion: $scope.observacionCheckIn }).success(function (data) {
                    $scope.actualizarEstadoAtencionMacro(data.information);
                    $scope.observacionCheckIn = '';
                    SweetAlert.success("Correcto", data.result_description);
                    $('#modal-registrar-checkIn').modal('hide');
                }).error(function (data) {
                    SweetAlert.error2(data);
                });
            } else {
                hotelService.checkInAtencion({ idAtencion: $scope.atencionMacro.Atenciones[$scope.indexAtencion].Id, observacion: $scope.observacionCheckIn }).success(function (data) {
                    $scope.actualizarEstadoAtencion(data.information, $scope.indexAtencion);
                    $scope.observacionCheckIn = '';
                    SweetAlert.success("Correcto", data.result_description);
                    $('#modal-registrar-checkIn').modal('hide');
                }).error(function (data) {
                    SweetAlert.error2(data);
                });
            }
        }
    }

    $scope.checkOutAtencion = function (index) {
        if ($scope.observacionCheckOut != undefined || $scope.observacionCheckOut != '') {
            if ($scope.accionGlobal) {
                hotelService.checkOutAtencionMacro({ idAtencionMacro: $scope.atencionMacro.Id, observacion: $scope.observacionCheckOut }).success(function (data) {
                    $scope.actualizarEstadoAtencionMacro(data.information);
                    $scope.observacionCheckOut = '';
                    SweetAlert.success("Correcto", data.result_description);
                    $('#modal-registrar-checkOut').modal('hide');
                }).error(function (data) {
                    SweetAlert.error2(data);
                });
            } else {
                hotelService.checkOutAtencion({ idAtencion: $scope.atencionMacro.Atenciones[$scope.indexAtencion].Id, observacion: $scope.observacionCheckOut }).success(function (data) {
                    $scope.actualizarEstadoAtencion(data.information, $scope.indexAtencion);
                    $scope.observacionCheckOut = '';
                    SweetAlert.success("Correcto", data.result_description);
                    $('#modal-registrar-checkOut').modal('hide');
                }).error(function (data) {
                    SweetAlert.error2(data);
                });
            }
        }
    }

    $scope.obtenerComprobantesParaOperaciones = function (paraAnulacion) {
        $scope.comprobantes = [];
        if ($scope.accionGlobal) {
            hotelService.obtenerComprobantesAtencionMacro({ idAtencionMacro: $scope.atencionMacro.Id }).success(function (data) {
                $scope.comprobantes = data;
                $scope.comprobantes.forEach(comprobante => {
                    comprobante.DarDeBaja = comprobante.DarDeBaja ? "true" : "false";
                })
                $scope.resolverComprobantesIncidente(paraAnulacion);
            }).error(function (data) {
                SweetAlert.error2(data);
            });
        } else {
            hotelService.obtenerComprobantesAtencion({ idAtencionMacro: $scope.atencionMacro.Id, idAtencion: $scope.atencionMacro.Atenciones[$scope.indexAtencion].Id }).success(function (data) {
                $scope.comprobantes = data;
                $scope.comprobantes.forEach(comprobante => {
                    comprobante.DarDeBaja = comprobante.DarDeBaja ? "true" : "false";
                })
                $scope.resolverComprobantesIncidente(paraAnulacion);
            }).error(function (data) {
                SweetAlert.error2(data);
            });
        }
    };

    $scope.resolverComprobantesIncidente = function (paraAnulacion) {
        if (paraAnulacion) {
            $scope.anulacion.Seleccionado = $scope.accionGlobal ? true : ($scope.comprobantes.length == 1 ? true : !$scope.atencionMacro.FacturadoGlobal);
            $scope.comprobantes.forEach(comprobante => {
                $scope.anulacion.MontoAnulacion = $scope.anulacion.MontoAnulacion + comprobante.Importe;
                $scope.anulacion.MontoDescuento = $scope.anulacion.MontoDescuento + comprobante.Descuento;
                $scope.anulacion.MontoDiferencia = $scope.anulacion.MontoDiferencia + comprobante.Diferencia;
                comprobante.MontoSoles = $scope.anulacion.Seleccionado ? comprobante.Diferencia.toFixed(2) : '0.00';
                comprobante.Seleccionado = $scope.anulacion.Seleccionado;
            });
            $scope.sumarMontoAnulacion();
        } else {
            if ($scope.comprobantes.length > 1) {
                $scope.comprobantes.forEach(comprobante => {
                    $scope.incidente.Total = $scope.incidente.Total + comprobante.Importe;
                    $scope.incidente.MontoHospedaje = $scope.incidente.MontoHospedaje + comprobante.MontoHospedaje;
                    $scope.incidente.MontoDescuento = $scope.incidente.MontoDescuento + comprobante.Descuento;
                    $scope.incidente.MontoDiferencia = $scope.incidente.MontoDiferencia + comprobante.Diferencia;
                    comprobante.MontoPorcentaje = '0';
                    comprobante.MontoSoles = '0.00';
                });
            }
        }
    }

    $scope.anularAtencion = function (index) {
        $scope.comprobantes.forEach(comprobante => {
            comprobante.DarDeBaja = comprobante.DarDeBaja == "true" ? true : false;
        })
        if ($scope.observacionAnular != undefined || $scope.observacionAnular != '') {
            if ($scope.accionGlobal) {
                hotelService.anularAtencionMacro({ idAtencionMacro: $scope.atencionMacro.Id, comprobantes: $scope.comprobantes, observacion: $scope.observacionAnular }).success(function (data) {
                    $scope.actualizarEstadoAtencionMacro(data.information);
                    $scope.obtenerTotalesAtencionMacro();
                    $scope.observacionAnular = '';
                    SweetAlert.success("Correcto", data.result_description);
                    $('#modal-anular-reserva').modal('hide');
                }).error(function (data) {
                    SweetAlert.error2(data);
                });
            } else {
                hotelService.anularAtencion({ idAtencion: $scope.atencionMacro.Atenciones[$scope.indexAtencion].Id, idAtencionMacro: $scope.atencionMacro.Id, comprobantes: $scope.comprobantes, observacion: $scope.observacionAnular }).success(function (data) {
                    $scope.actualizarEstadoAtencion(data.information, $scope.indexAtencion);
                    $scope.obtenerTotalesAtencionMacro();
                    $scope.observacionAnular = '';
                    SweetAlert.success("Correcto", data.result_description);
                    $('#modal-anular-reserva').modal('hide');
                }).error(function (data) {
                    SweetAlert.error2(data);
                });
            }
        }
    }

    $scope.facturarAtencion = function () {
        if ($scope.accionGlobal) {
            hotelService.facturarAtencionMacro({ atencion: $scope.atencionAfacturar }).success(function (data) {
                if (!$scope.atencionAfacturar.TieneFacturacion) {
                    $scope.actualizarEstadoAtencionMacroFacturar(data.information);
                } else {
                    for (var i = 0; i < $scope.atencionMacro.Atenciones.length; i++) {
                        $scope.atencionMacro.Atenciones[i].Facturado = true;
                    }
                }
                $scope.facturadorAPI.SetComprobantesFacturados(data.documentos);
            }).error(function (data) {
                SweetAlert.error2(data);
            });
        } else {
            hotelService.facturarAtencion({ atencion: $scope.atencionAfacturar }).success(function (data) {
                if (!$scope.atencionAfacturar.TieneFacturacion) {
                    $scope.actualizarEstadoAtencion(data.information, $scope.indexAtencion);
                } else {
                    $scope.atencionMacro.Atenciones[$scope.indexAtencion].Facturado = true;
                }
                $scope.facturadorAPI.SetComprobantesFacturados(data.documentos);
            }).error(function (data) {
                SweetAlert.error2(data);
            });
        }
    }

    $scope.registrarIncidente = function (index) {
        $scope.comprobantes.forEach(comprobante => {
            comprobante.DarDeBaja = comprobante.DarDeBaja == "true" ? true : false;
        })
        var comprobantesSeleccionados = angular.copy($scope.comprobantes.filter(c => c.Seleccionado));
        if ($scope.justificacionIncidente != undefined || $scope.justificacionIncidente != '') {
            if ($scope.accionGlobal) {
                hotelService.registrarIncidenteAtencionMacro({ idAtencionMacro: $scope.atencionMacro.Id, esDevolucion: $scope.incidente.EsDevolucion == "true", comprobantes: ($scope.comprobantes.length == 1 ? $scope.comprobantes : comprobantesSeleccionados), observacion: $scope.justificacionIncidente }).success(function (data) {
                    $scope.actualizarEstadoAtencionMacroFacturar(data.information);
                    $scope.justificacionIncidente = '';
                    SweetAlert.success("Correcto", data.result_description);
                    $('#modal-registro-incidente').modal('hide');
                }).error(function (data) {
                    SweetAlert.error2(data);
                });
            } else {
                hotelService.registrarIncidenteAtencion({ idAtencion: $scope.atencionMacro.Atenciones[$scope.indexAtencion].Id, idAtencionMacro: $scope.atencionMacro.Id, esDevolucion: $scope.incidente.EsDevolucion == "true", comprobantes: ($scope.comprobantes.length == 1 ? $scope.comprobantes : comprobantesSeleccionados), observacion: $scope.justificacionIncidente }).success(function (data) {
                    $scope.actualizarEstadoAtencion(data.information, $scope.indexAtencion);
                    $scope.justificacionIncidente = '';
                    SweetAlert.success("Correcto", data.result_description);
                    $('#modal-registro-incidente').modal('hide');
                }).error(function (data) {
                    SweetAlert.error2(data);
                });
            }
        }
    }

    $scope.formatoDecimal = function (event) {
        let valor = event.target.value;
        if (valor != '')
            event.target.value = parseFloat(valor).toFixed(2);
    }

    $scope.formatoPorcentaje = function (event) {
        let valor = event.target.value;
        if (valor != '')
            event.target.value = parseFloat(valor).toFixed(0);
    }

    $scope.resolverMontoPorcentajeIncidente = function () {
        $scope.incidente.DevolucionSoles = '';
        $scope.incidente.DevolucionPorcentaje = $scope.incidente.DevolucionPorcentaje == '' ? 0 : $scope.incidente.DevolucionPorcentaje;
        $scope.comprobantes.forEach(comprobante => {
            comprobante.MontoPorcentaje = $scope.incidente.DevolucionPorcentaje;
            comprobante.MontoSoles = parseFloat(comprobante.MontoHospedaje * (parseFloat(comprobante.MontoPorcentaje) / 100)).toFixed(2);
        })
        $scope.sumarMontoIncidente();
    }

    $scope.resolverMontoSolesIncidente = function () {
        $scope.incidente.DevolucionPorcentaje = '';
        $scope.incidente.DevolucionSoles = $scope.incidente.DevolucionSoles == '' ? 0 : $scope.incidente.DevolucionSoles;
        $scope.comprobantes.forEach(comprobante => {
            comprobante.MontoSoles = parseFloat($scope.incidente.DevolucionSoles).toFixed(2);
            comprobante.MontoPorcentaje = parseFloat((comprobante.MontoSoles / comprobante.MontoHospedaje) * 100).toFixed(0);
        })
        $scope.sumarMontoIncidente();
    }

    $scope.calcularMontoPorcentajeComprobante = function (index) {
        $scope.comprobantes[index].MontoPorcentaje = parseFloat(($scope.comprobantes[index].MontoSoles / $scope.comprobantes[index].MontoHospedaje) * 100).toFixed(0);
        $scope.sumarMontoIncidente();
    }

    $scope.calcularMontoSolesComprobante = function (index) {
        $scope.comprobantes[index].MontoSoles = parseFloat($scope.comprobantes[index].MontoHospedaje * (parseFloat($scope.comprobantes[index].MontoPorcentaje) / 100)).toFixed(2);
        $scope.sumarMontoIncidente();
    }

    $scope.sumarMontoIncidente = function () {
        $scope.incidente.TotalSoles = 0;
        $scope.comprobantes.forEach(comprobante => {
            $scope.incidente.TotalSoles = $scope.incidente.TotalSoles + parseFloat(comprobante.MontoSoles);
        })
        $scope.incidente.TotalPorcentaje = parseFloat(($scope.incidente.TotalSoles / $scope.incidente.MontoHospedaje) * 100).toFixed(0);
    }

    $scope.seleccionarNotaCreditoComprobantes = function () {
        $scope.comprobantes.forEach(comprobante => {
            comprobante.DarDeBaja = 'false';
        })
    }

    $scope.seleccionarDefaultComprobantes = function () {
        $scope.comprobantes.forEach(comprobante => {
            comprobante.DarDeBaja = comprobante.PuedeDarDeBaja ? 'true' : 'false';
        })
    }

    $scope.seleccionarTodosComprobantes = function () {
        $scope.comprobantes.forEach(comprobante => {
            comprobante.Seleccionado = $scope.incidente.Seleccionado;
        })
    }

    $scope.seleccionarComprobante = function () {
        let numeroSeleccionados = $scope.comprobantes.filter(c => c.Seleccionado == true).length;
        $scope.incidente.Seleccionado = $scope.comprobantes.length == numeroSeleccionados;
        $scope.comprobantes.forEach(comprobante => {
            comprobante.MontoSoles = comprobante.Seleccionado ? comprobante.MontoSoles : '0.00';
            comprobante.MontoPorcentaje = comprobante.Seleccionado ? comprobante.MontoPorcentaje : 0;
        })
        $scope.sumarMontoIncidente();
    }

    $scope.hayInconsistenciaIncidente = function () {
        let inconsistenciaIncidente = 0;
        if ($scope.comprobantes != undefined) {
            if ($scope.incidente) {
                if ($scope.comprobantes.length == 1) {
                    inconsistenciaIncidente += ($scope.incidente.EsDevolucion == 'false' && (parseFloat($scope.comprobantes[0].MontoSoles) == 0 || parseFloat($scope.comprobantes[0].MontoSoles) > parseFloat($scope.comprobantes[0].Diferencia))) ? 1 : 0;
                } else {
                    $scope.comprobantes.forEach(comprobante => {
                        inconsistenciaIncidente += ((comprobante.Seleccionado && $scope.incidente.EsDevolucion == 'false' && (parseFloat(comprobante.MontoSoles) == 0 || parseFloat(comprobante.MontoSoles) > parseFloat(comprobante.Diferencia))) ? 1 : 0);
                    })
                    inconsistenciaIncidente += ($scope.comprobantes.filter(c => c.Seleccionado == true).length == 0) ? 1 : 0;
                }
            }
        }
        return inconsistenciaIncidente > 0;
    }

    $scope.cambioRegistroHuesped = function () {
        $scope.obtenerTotalesAtencionMacro();
    }

    $scope.seleccionarTodosComprobantesAnulacion = function () {
        $scope.comprobantes.forEach(comprobante => {
            comprobante.Seleccionado = $scope.anulacion.Seleccionado;
        });
        for (var i = 0; i < $scope.comprobantes.length; i++) {
            $scope.seleccionarComprobanteAnulacion(i);
        }
    }

    $scope.seleccionarComprobanteAnulacion = function (index) {
        let numeroSeleccionados = $scope.comprobantes.filter(c => c.Seleccionado == true).length;
        $scope.anulacion.Seleccionado = $scope.comprobantes.length == numeroSeleccionados;
        $scope.comprobantes[index].MontoSoles = $scope.comprobantes[index].Seleccionado ? $scope.comprobantes[index].Diferencia.toFixed(2) : '0.00';
        $scope.sumarMontoAnulacion();
    }

    $scope.calcularMontoAnulacion = function (index) {
        $scope.comprobantes[index].DarDeBaja = $scope.comprobantes[index].MontoSoles == $scope.comprobantes[index].Diferencia ? 'true' : 'false';
        $scope.sumarMontoAnulacion();
    }

    $scope.sumarMontoAnulacion = function () {
        $scope.anulacion.TotalSoles = 0;
        $scope.comprobantes.forEach(comprobante => {
            $scope.anulacion.TotalSoles = $scope.anulacion.TotalSoles + parseFloat(comprobante.MontoSoles);
        })
    }

    $scope.hayInconsistenciaAnulacion = function () {
        let inconsistenciaAnulacion = 0;
        if ($scope.comprobantes != undefined) {
            if ($scope.comprobantes.length > 0) {
                if ($scope.anulacion) {
                    inconsistenciaAnulacion += ($scope.anulacion.TotalSoles != $scope.anulacion.MontoAnulacion) ? 1 : 0;
                    $scope.comprobantes.forEach(comprobante => {
                        inconsistenciaAnulacion += ((comprobante.Seleccionado && (parseFloat(comprobante.MontoSoles) == 0 || parseFloat(comprobante.MontoSoles) > parseFloat(comprobante.Diferencia))) ? 1 : 0);
                    });
                }
            }
        }
        return inconsistenciaAnulacion > 0;
    }
});

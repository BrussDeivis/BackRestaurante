app.controller('planificadorController', function ($scope, $q, $rootScope, SweetAlert, DTOptionsBuilder, DTColumnDefBuilder, hotelService, centroDeAtencionService) {

    $scope.inicializar = function () {
        $scope.cargarParametros();
        $scope.cargarColeccionesSync().then(function () {
            $scope.establecerDatosPorDefecto();
            $scope.inicializarComponentes();
            $scope.obtenerPlanificador();
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
        $scope.establecerDatosPorDefecto();
        $scope.inicializarComponentes();
        window.oncontextmenu = function () { return false; }
    };

    $scope.cargarParametros = function () {
        $scope.accionesEstadoHabitacion = [];
        $scope.buscador = {};
        $scope.fechaDesde = fechaDesde;
        $scope.fechaHasta = fechaHasta;
        $scope.fechaActual = fechaActual;
        $scope.urlActionDetalleReserva = urlActionDetalleReserva;
        $scope.accionesEstadoDisponible = AccionesEstadoDisponible;
        $scope.accionesEstadoOcupada = AccionesEstadoOcupada;
        $scope.accionesEstadoReservada = AccionesEstadoReservada;
        $scope.Establecimientos = Establecimientos;
        $scope.EstablecimientoSesion = EstablecimientoSesion;
        $scope.buscador.establecimientoSeleccionado = $scope.EstablecimientoSesion;
        $scope.UsuarioTieneRolAdministradorDeNegocio = UsuarioTieneRolAdministradorDeNegocio;
        $scope.maximoDiasMostrarEnPlanificador = MaximoDiasMostrarEnPlanificador;
        $scope.parametrosEstadoHabitacion = ParametrosEstadoHabitacion;
    }

    $scope.establecerDatosPorDefecto = function () {
        $scope.filtro = { estado: $scope.parametrosEstadoHabitacion.Todo }
        $scope.habitacionesDisponibles = [];
        $scope.afiltrar = [];
        $scope.HabitacionMenuContextual = {}
    }

    $scope.inicializarComponentes = function () {
        $scope.inicializacionRealizada = true;
    }

    $scope.cargarColeccionesSync = function () {
        var defered = $q.defer();
        var promiseList = [];
        promiseList.push($scope.obtenerAmbiente());
        promiseList.push($scope.obtenerTiposDeHabitaciones());
        return $q.all(promiseList).then(function (response) {
            defered.resolve();
        }).catch(function (error) {
            defered.reject(e);
        });
    };

    $scope.obtenerAmbiente = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        hotelService.obtenerAmbientesVigentesPorEstablecimientoSimplificado({ idEstablecimiento: $scope.buscador.establecimientoSeleccionado.Id }).success(function (data) {
            $scope.ambientes = data;
            $scope.ambientes.unshift({ Codigo: null, Id: 0, Nombre: "Todos", Valor: null });
            $scope.ambienteSeleccionado = $scope.ambientes[0];
            defered.resolve();
        }).error(function (data) {
            SweetAlert.error2(data);
            defered.reject(data);
        });
        return promise;
    };

    $scope.obtenerTiposDeHabitaciones = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        hotelService.obtenerTiposHabitacionVigentesSimplificado({}).success(function (data) {
            $scope.tiposDeHabitaciones = data;
            $scope.tiposDeHabitaciones.unshift({ Codigo: null, Id: 0, Nombre: "Todos", Valor: null });
            $scope.tipoHabitacionSeleccionado = $scope.tiposDeHabitaciones[0];
            defered.resolve();
        }).error(function (data) {
            SweetAlert.error2(data);
            defered.reject(data);
        });
        return promise;
    };

    $scope.obtenerPlanificador = function () {
        $scope.accionesEstadoHabitacion = [];
        $scope.obtenerReportePlanificador(); 
        $scope.obtenerPlanificadorHabitaciones();
    };

    $scope.obtenerReportePlanificador = function () {
        $scope.accionesEstadoHabitacion = [];
        hotelService.obtenerReportePlanificador({ idEstablecimiento: $scope.buscador.establecimientoSeleccionado.Id, }).success(function (data) {
            $scope.planificador = data;
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    };

    $scope.obtenerPlanificadorHabitaciones = function () {
        $scope.accionesEstadoHabitacion = [];
        hotelService.obtenerPlanificadorHabitaciones({ idEstablecimiento: $scope.buscador.establecimientoSeleccionado.Id, fechaDesde: $scope.fechaDesde, fechaHasta: $scope.fechaHasta, idAmbiente: $scope.ambienteSeleccionado.Id, idTipoHabitacion: $scope.tipoHabitacionSeleccionado.Id }).success(function (data) {
            $scope.planificadorHabitaciones = data;
            $scope.habitacionesEnPlanificador = $scope.planificadorHabitaciones.HabitacionesEnPlanificador;
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    };

    $scope.cambiarLimpiezaDeHabitacion = function (index) {
        $scope.accionesEstadoHabitacion = [];
        hotelService.cambiarEnLimpiezaHabitacion({ idHabitacion: $scope.planificadorHabitaciones.HabitacionesEnPlanificador[index].Id }).success(function (data) {
            $scope.obtenerPlanificador();
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    };

    $scope.cambiarEstablecimiento = () => {
        $scope.obtenerAmbiente();
        $scope.obtenerTiposDeHabitaciones();
        $scope.obtenerPlanificador();
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

    $scope.deshabilitarBotonBusquedaPlanificador = () => {
        let bandera = true;
        let diferencia = $scope.diferenciaDiasEntreFechas($scope.fechaDesde, $scope.fechaHasta);
        if (diferencia <= $scope.maximoDiasMostrarEnPlanificador) {
            bandera = false;
        }
        return bandera;
    }

    $scope.pintarCeldaPorEstado = (estadoHabitacion) => {
        let claseEstilo = '';
        switch (estadoHabitacion) {

            case $scope.parametrosEstadoHabitacion.EstadoDisponible:
                claseEstilo = 'disponible'
                break;
            case $scope.parametrosEstadoHabitacion.EstadoOcupado:
                claseEstilo = 'ocupado'
                break;
            case $scope.parametrosEstadoHabitacion.EstadoReservado:
                claseEstilo = 'reservado'
                break;
            case $scope.parametrosEstadoHabitacion.EstadoOcupadoDisponible:
                claseEstilo = 'ocupado-disponible'
                break;
        }
        return claseEstilo;
    }

    $scope.buscarPorFiltro = () => {
        $scope.accionesEstadoHabitacion = [];
        $scope.habitacionesEnPlanificador = $scope.planificadorHabitaciones.HabitacionesEnPlanificador;
        let habitacionesTemp = [];

        if ($scope.filtro.estado === $scope.parametrosEstadoHabitacion.EstadoDisponible) {
            for (let i = 0; i < $scope.habitacionesEnPlanificador.length; i++) {
                let existeAlmenosUno = $scope.habitacionesEnPlanificador[i].EstadosHabitacion.every(celda => celda.EstadoHabitacion === $scope.filtro.estado);
                if (existeAlmenosUno) {
                    habitacionesTemp.push($scope.habitacionesEnPlanificador[i]);
                }
            }
            $scope.habitacionesEnPlanificador = habitacionesTemp;
        } else if ($scope.filtro.estado === $scope.parametrosEstadoHabitacion.Todo) {
            $scope.habitacionesEnPlanificador = $scope.planificadorHabitaciones.HabitacionesEnPlanificador;
        }
        else {
            for (let i = 0; i < $scope.habitacionesEnPlanificador.length; i++) {
                let existeAlmenosUno = $scope.habitacionesEnPlanificador[i].EstadosHabitacion.some(celda => celda.EstadoHabitacion === $scope.filtro.estado);
                if (existeAlmenosUno) {
                    habitacionesTemp.push($scope.habitacionesEnPlanificador[i]);
                }
            }
            $scope.habitacionesEnPlanificador = habitacionesTemp;
        }
    }

    //#region EVENTOS DE LA TABLA PLANIFICADOR 
    $scope.trInicial = -1;
    $scope.trActual = -1;

    $scope.compararFechaMayorAFechaActual = (fechaEntrante) => {
        let bandera = false;
        let fechaEntrantePartes = fechaEntrante.split("/");
        let fechaAcutalPartes = $scope.fechaActual.split("/");
        let fechaEntranteComparar = new Date(+fechaEntrantePartes[2], fechaEntrantePartes[1] - 1, +fechaEntrantePartes[0]);
        let fechaAcutalComparar = new Date(+fechaAcutalPartes[2], fechaAcutalPartes[1] - 1, +fechaAcutalPartes[0]);
        if (fechaEntranteComparar >= fechaAcutalComparar) {
            bandera = true;
        }
        return bandera;
    }

    $scope.sePuedeSeleccionar = (estadoHabitacion) => {
        let bandera = false;
        if (estadoHabitacion.EsFechaAtencion && (estadoHabitacion.EstadoHabitacion == $scope.parametrosEstadoHabitacion.EstadoDisponible || estadoHabitacion.EstadoHabitacion == $scope.parametrosEstadoHabitacion.EstadoOcupadoDisponible)) {
            bandera = true;
        }
        return bandera;
    }

    $scope.cerrarContextMenu = function () {
        document.getElementById("context-menu").classList.remove("active")
    }

    $scope.limpiarCeldasSeleccionadas = function () {
        $scope.cerrarContextMenu();
        let celdas = document.querySelectorAll("#tableCalendar td");
        for (let i = 0; i < celdas.length; i++) {
            celdas[i].classList.remove("highlight");
        }
    };

    $scope.eventoClick = ($event, estadoHabitacion, habitacionPlanificador) => {
        $scope.limpiarCeldasSeleccionadas();
        let elemento = document.getElementById($event.target.id);
        elemento.classList.add("highlight");
        $scope.HabitacionMenuContextual.Id = habitacionPlanificador.Id
        $scope.HabitacionMenuContextual.fechaIngreso = estadoHabitacion.FechaString;
        $scope.HabitacionMenuContextual.fechaSalida = estadoHabitacion.FechaString;
    };

    $scope.eventoMousedown = function ($event, estadoHabitacion) {
        if ($scope.sePuedeSeleccionar(estadoHabitacion)) {
            $scope.limpiarCeldasSeleccionadas();
            $scope.habitacionesDisponibles = [];
            let elemento = angular.element($event.toElement)[0];
            $scope.trInicial = elemento.closest("tr").rowIndex;
            $scope.isMouseDown = true;
            elemento.classList.add("highlight");
            $scope.HabitacionMenuContextual.fechaIngreso = estadoHabitacion.FechaString;
        }
    };

    $scope.eventoMouseover = function ($event, estadoHabitacion) {
        if ($scope.sePuedeSeleccionar(estadoHabitacion)) {
            $scope.atencionDiaEstadoSeleccionado = {}
            $scope.atencionDiaEstadoSeleccionado = estadoHabitacion;
            let elemento = angular.element($event.toElement)[0];
            $scope.trActual = elemento.closest("tr").rowIndex;
            if ($scope.isMouseDown) {
                if ($scope.trActual !== $scope.trInicial)
                    $scope.limpiarCeldasSeleccionadas();
                else
                    elemento.classList.toggle("highlight");
            }
        }
    };

    $scope.eventoMouseup = function ($event, estadoHabitacion, habitacionesEnPlanificador) {
        let elemento = angular.element($event.toElement)[0];
        $scope.isMouseDown = false;
        $scope.habitacionesDisponibles.push(habitacionesEnPlanificador);
        $scope.HabitacionMenuContextual.Id = habitacionesEnPlanificador.Id;
        $scope.HabitacionMenuContextual.IdAtencion = estadoHabitacion.IdAtencion;
        $scope.HabitacionMenuContextual.IdAtencionMacro = estadoHabitacion.IdAtencionMacro;
        $scope.HabitacionMenuContextual.fechaSalida = estadoHabitacion.FechaSiguienteString;
        if ($scope.trActual === $scope.trInicial) {
            $scope.eventoContextMenu($event, estadoHabitacion);
        }
    };

    $scope.eventoContextMenu = function ($event, estadoHabitacion) {
        if (estadoHabitacion !== undefined) {
            switch (estadoHabitacion.EstadoHabitacion) {
                case $scope.parametrosEstadoHabitacion.EstadoReservado:
                    if (estadoHabitacion.EsFechaAtencion) {
                        $scope.accionesEstadoHabitacion = $scope.accionesEstadoReservada.FechaPasada;
                    }
                    else {
                        $scope.accionesEstadoHabitacion = $scope.accionesEstadoReservada.General;
                    }
                    break;
                case $scope.parametrosEstadoHabitacion.EstadoOcupado:
                    if (estadoHabitacion.EsFechaAtencion) {
                        if (estadoHabitacion.PuedeHacerConsumo) {
                            $scope.accionesEstadoHabitacion = $scope.accionesEstadoOcupada.General;
                        } else {
                            $scope.accionesEstadoHabitacion = $scope.accionesEstadoOcupada.FechaPasada;
                        }
                    }
                    else {
                        $scope.accionesEstadoHabitacion = $scope.accionesEstadoOcupada.FechaPasada;
                    }
                    break;
                case $scope.parametrosEstadoHabitacion.EstadoDisponible:
                    if (estadoHabitacion.EsFechaAtencion) {
                        $scope.accionesEstadoHabitacion = $scope.accionesEstadoDisponible.General;
                    }
                    else {
                        $scope.accionesEstadoHabitacion = [];
                    }
                    break;
                case $scope.parametrosEstadoHabitacion.EstadoOcupadoDisponible:
                    if (estadoHabitacion.EsFechaAtencion) {
                        $scope.accionesEstadoHabitacion = $scope.accionesEstadoDisponible.General;
                    }
                    else {
                        $scope.accionesEstadoHabitacion = [];
                    }
                    break;
            }
            let contextElement = document.getElementById("context-menu");
            contextElement.style.top = $event.clientY + "px";
            contextElement.style.left = $event.clientX + "px";
            contextElement.classList.add("active");
        }
    }
    //#endregion

    $scope.verDetalleReserva = () => {
        $scope.accionesEstadoHabitacion = [];
        window.open($scope.urlActionDetalleReserva + '?idEstablecimiento=' + $scope.buscador.establecimientoSeleccionado.Id + '&&idAtencionMacro=' + $scope.HabitacionMenuContextual.IdAtencionMacro + '&&idAtencion=' + $scope.HabitacionMenuContextual.IdAtencion, '_blank');
    }

    $scope.nuevaReserva = function () {
        $scope.accionesEstadoHabitacion = [];
        $scope.abrirModal('modal-registrador-reserva');
        $scope.registradorReservaAPI.NuevaReserva();
    }

    $scope.reservaAgregada = function () {
        $scope.obtenerPlanificador();
    }

    $scope.abrirNuevaReserva = function () {
        $scope.accionesEstadoHabitacion = [];
        $scope.abrirModal('modal-registrador-reserva');
        $scope.registradorReservaAPI.NuevaReservaCargada($scope.HabitacionMenuContextual.Id, $scope.HabitacionMenuContextual.fechaIngreso, $scope.HabitacionMenuContextual.fechaSalida);
    }

    $scope.registrarConsumo = () => {
        $scope.accionesEstadoHabitacion = [];
        $('#modal-registrador-consumo').modal('show');
        $scope.registradorConsumoAPI.NuevoConsumoCargado($scope.HabitacionMenuContextual.IdAtencion);
    }

    $scope.abrirModal = function (nombreDelModal) {
        $('#' + nombreDelModal).modal('show');
    }

    $scope.cerrarModal = function (nombreDelModal) {
        $('#' + nombreDelModal).modal('hide');
    }

    //$scope.abrirModalNuevaReserva = function () {
    //    $('#' + 'modal-nueva-reserva').modal('show');
    //}

    //$scope.cerrar = function (nombreDelModal) {
    //    $scope.limpiarCeldasSeleccionadas();
    //    $('#' + nombreDelModal).modal('hide');
    //    $scope.habitacionesDisponibles = [];
    //    $scope.detalleReservas = [];
    //}

});
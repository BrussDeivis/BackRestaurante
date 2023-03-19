app.controller('habitacionesController', function ($scope, $q, hotelService, centroDeAtencionService, $rootScope, SweetAlert) {

    $scope.inicializar = function () {
        $scope.cargarParametros();
        $scope.limpiarRegistro();
        $scope.cargarColeccionesAsync();
        $scope.cargarColeccionesSync().then(function () {
            $scope.establecerDatosPorDefecto();
            $scope.inicializarComponentes();
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
    }
    $scope.cargarParametros = function () {
        $scope.idEstablecimientoPorDefecto = idEstablecimientoPorDefecto;
    }
    $scope.cargarColeccionesAsync = function () {
        $scope.obtenerTipoHabitacionesHotel();
    }
    $scope.cargarColeccionesSync = function () {
        var defered = $q.defer();
        var promiseList = [];
        promiseList.push($scope.obtenerEstablecimientosComerciales());
        return $q.all(promiseList).then(function (response) {
            defered.resolve();
        }).catch(function (error) {
            defered.reject(e);
        });
    };
    $scope.obtenerEstablecimientosComerciales = () => {
        var defered = $q.defer();
        var promise = defered.promise;
        centroDeAtencionService.obtenerEstablecimientosComerciales({}).success(function (data) {
            $scope.establecimientos = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    };
    $scope.obtenerTipoHabitacionesHotel = () => {
        hotelService.obtenerTiposHabitacionVigentesSimplificado({}).success(function (data) {
            $scope.tipoHabitaciones = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    };
    $scope.limpiarRegistro = function () {
        $scope.habitacion = { Camas: [], EsVigente: true };
    }
    $scope.inicializarComponentes = function () {
        $scope.inicializacionRealizada = true;
    }
    $scope.establecerDatosPorDefecto = function () {
        let establecimiento = $scope.establecimientos.find(est => est.Id == $scope.idEstablecimientoPorDefecto);
        $scope.establecimiento = establecimiento != undefined ? establecimiento : $scope.establecimientos[0];
        $scope.obtenerHabitaciones();
    }
    $rootScope.dtOptions.withLightColumnFilter({
        '0': { type: 'text', className: 'form-control padding-left-right-3' },
        '1': { type: 'text', className: 'form-control padding-left-right-3' },
        '2': { type: 'text', className: 'form-control padding-left-right-3' },
        '3': { type: 'text', className: 'form-control padding-left-right-3' },
        '4': { type: 'text', className: 'form-control padding-left-right-3' },
        '5': { type: 'text', className: 'form-control padding-left-right-3' },
        '6': { type: 'text', className: 'form-control padding-left-right-3' },
        '7': { type: 'text', className: 'form-control padding-left-right-3' }
    });
    $scope.obtenerHabitaciones = () => {
        hotelService.obtenerHabitacionesBandeja({ idEstablecimiento: $scope.establecimiento.Id }).success(function (data) {
            $scope.habitaciones = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    };
    $scope.cambiarEstadoHabitacion = (id) => {
        hotelService.cambiarEsVigenteHabitacion({ id: id }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.obtenerHabitaciones();
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    //#region MODAL NUEVA HABITACION
    $scope.obtenerAmbientes = (id) => {
        hotelService.obtenerAmbientesVigentesPorEstablecimientoSimplificado({ idEstablecimiento: id }).success(function (data) {
            $scope.ambientesHotel = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    };
    $scope.obtenerTipoCamas = () => {
        var defered = $q.defer();
        var promise = defered.promise;
        hotelService.obtenerTipoCamas({}).success(function (data) {
            $scope.tipoCamas = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    };
    $scope.hayCamasSeleccionadasConNumeroNoValido = () => {
        let numeroCamaNovalido = 0;
        if ($scope.habitacion.Camas.length > 0) {
            numeroCamaNovalido = $scope.habitacion.Camas.filter(c => c.Valor == undefined || c.Valor == '' || c.Valor == 0);
        }
        return numeroCamaNovalido != 0;
    };
    $scope.verificarSeleccionCama = (id) => {
        let camaSeleccionada = $scope.habitacion.Camas.findIndex(c => c.Id == id);
        if (camaSeleccionada < 0) {
            let camaDeseleccionada = $scope.tipoCamas.findIndex(c => c.Id == id);
            $scope.tipoCamas[camaDeseleccionada].Valor = undefined;
        }
    };
    $scope.obtenerHabitacion = (id) => {
        var defered = $q.defer();
        var promise = defered.promise;
        hotelService.obtenerHabitacion({ id: id }).success(function (data) {
            $scope.habitacion = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    };
    $scope.guardarHabitacion = () => {
        $scope.habitacion.Ambiente.Establecimiento = $scope.habitacion.Establecimiento;
        hotelService.guardarHabitacion({ habitacion: $scope.habitacion }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.cerrarRegistroHabitacion();
            $scope.obtenerHabitaciones();
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    };
    $scope.editarHabitacion = (id) => {
        $scope.limpiarRegistro();
        $scope.obtenerTipoCamas().then(function (resultado_) {
            $scope.obtenerHabitacion(id).then(function (resultado_) {
                for (var i = 0; i < $scope.habitacion.Camas.length; i++) {
                    $scope.tipoCamas.map(cama => {
                        if (cama.Id === $scope.habitacion.Camas[i].Id) {
                            cama.Valor = $scope.habitacion.Camas[i].Valor;
                        }
                        return cama;
                    })
                }
                $scope.habitacion.Establecimiento = $scope.habitacion.Ambiente.Establecimiento;
                $scope.obtenerAmbientes($scope.habitacion.Establecimiento.Id)
                $('#modal-nueva-habitacion').modal('show');
            }, function (error) {
                $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
            });
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
    }
    $scope.abrirRegistroHabitacion = function () {
        $scope.limpiarRegistro();
        $scope.obtenerTipoCamas();
        $scope.habitacion.Establecimiento = $scope.establecimiento;
        $scope.obtenerAmbientes($scope.habitacion.Establecimiento.Id);
        $('#modal-nueva-habitacion').modal('show');
    }
    $scope.cerrarRegistroHabitacion = function () {
        $scope.limpiarRegistro();
        $('#modal-nueva-habitacion').modal('hide');
    }
    //#endregion
});
app.controller('exoneracionesCocheraController', function ($scope, $q, $timeout, $rootScope, SweetAlert, $filter, DTOptionsBuilder, DTColumnDefBuilder, cocheraService, centroDeAtencionService) {
    $scope.cocheraSeleccionada = {}
    //#region BANDEJA DE exoneraciones
    $scope.listarExoneraciones = function () {
        cocheraService.obtenerExoneraciones({ idCochera: $scope.cocheraSeleccionada.Id }).success(function (data) {
            $scope.exoneraciones = data;
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema", data.error);
        });
    };

    $rootScope.dtOptions.withLightColumnFilter({
        '0': { type: 'text', className: 'form-control padding-left-right-3' },
        '1': { type: 'text', className: 'form-control padding-left-right-3' },
        '2': { type: 'text', className: 'form-control padding-left-right-3' },
        '3': { type: 'text', className: 'form-control padding-left-right-3' },
        '4': { type: 'text', className: 'form-control padding-left-right-3' },
    });
    //#endregion

    //#region INICIO DE VARIABLES
    $scope.inicializar = function () {
        $scope.vehiculoAExonerar = {};
        $scope.cargarParametros();
        $scope.cargarColeccionesSync().then(function (resultado_) {
            $scope.establecerDatosPorDefecto();
            $scope.listarExoneraciones();
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
        $scope.cargarColeccionesAsync();

    };

    $scope.cargarParametros = function () {
    };

    $scope.cargarColeccionesAsync = function () {

    };

    $scope.cargarColeccionesSync = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        var promiseList = [];
        promiseList.push($scope.obtenerCocheras());
        return $q.all(promiseList).then(function (response) {
            defered.resolve();
        }).catch(function (error) {
            defered.reject(e);
        });
    };
    $scope.validarSeleccioanda = function () {

        console.log($scope.cocheraSeleccionada);
        //$scope.cocheraSeleccionada = $scope.cocheras[1];

    };
    $scope.establecerDatosPorDefecto = function () {
        //seleccionar primera cochera
        //$scope.cocheraSeleccionada.Id = $scope.cocheras[0].Id;
        $scope.cocheraSeleccionada = $scope.cocheras[0];
        //$scope.cocheraSeleccionada.Cochera = $scope.cocheras[0];

    };

    //#endregion
    $scope.obtenerCocheras = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        cocheraService.obtenerCocheras().success(function (data) {
            $scope.cocheras = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    //#region OPERACIONES
    $scope.exonerar = function () {
        cocheraService.exonerarVehiculo({ vehiculo: $scope.vehiculoAExonerar, idcochera: $scope.cocheraSeleccionada.Id }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.listarExoneraciones();
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema", data.error);
        });
    };
    $scope.retirarExoneracion = function (exoneracion) {
        cocheraService.quitarExoneracionAVehiculo(exoneracion).success(function (data) {
        SweetAlert.success("Correcto", data.result_description);
            $scope.listarExoneraciones();
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema", data.error);
        });
    };

    $scope.cancelarExoneracion = function () {
        $scope.estaRegistrandoExoneracion = false;
        $scope.limpiarRegistroExoneracion();
    };
        //#endregion
});

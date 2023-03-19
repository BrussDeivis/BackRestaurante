app.controller('maestroController', function ($scope, $rootScope  , maestroService) {
    $scope.maestro = {};
    $scope.maestro_ = {};

    $scope.maestroCopia = {};
    $scope.detalle = {};
    $scope.detalleCopia = {};
    $scope.maestros = { Lista: [] };
    $scope.detalles = { Lista: [] };

    $scope.modalMaestro = false;
    $scope.modalDetalle = false;

    $scope.nuevoRegistroMaestro = function () {
        $scope.maestro = {};
        $scope.maestroCopia = angular.copy($scope.maestro);
        $scope.modalMaestro = true;
    }
    $scope.nuevoRegistroDetalle = function () {
        $scope.detalle = {};
        $scope.detalleCopia = angular.copy($scope.detalle);
        $scope.modalDetalle = true;
    }
    $scope.cerrar = function () {
        if (angular.equals($scope.maestro, $scope.maestroCopia)) {
            $('#modal-registro-maestro').modal('hide');
            $scope.modalMaestro = false;
        }
        else {
            $('#modal-cambio').click();
        }
    }
    $scope.cerrarModalDetalle = function () {
        if (angular.equals($scope.detalle, $scope.detalleCopia)) {
            $('#modal-registro-detalle').modal('hide');
            $scope.modalDetalle = false;
        }
        else {
            $('#modal-cambio').click();
        }
    }
    $scope.closeModal = function () {
        if ($scope.modalMaestro) {
            $('#modal-registro-maestro').modal('hide');
        }
        if ($scope.modalDetalle) {
            $('#modal-registro-detalle').modal('hide');
        }
    }

    $scope.nuevoMaestro = function () {
        maestroService.guardarMaestro({ maestro: $scope.maestro }).success(function (data) {
            $scope.messageSuccess(data.result_description);
            $scope.listarMaestros();
            $scope.maestro = {};
            $('#modal-registro-maestro').modal('hide');
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
    $scope.nuevoDetalle = function () {
        $scope.detalle.IdMaestro = $scope.maestro_.Id;
        maestroService.guardarDetalleMaestro({ detalle: $scope.detalle }).success(function (data) {
            $scope.messageSuccess(data.result_description);
            $scope.listarDetalles($scope.maestro_.Id);
            $scope.detalle = {};
            $('#modal-registro-detalle').modal('hide');
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
    $scope.verDetalles = function (item) {
        $scope.maestro_ = item;
        $scope.listarDetalles($scope.maestro_.Id);
    }
    $scope.listarDetalles = function (id) {
        maestroService.listarDetallesMaestro({ idMaestro: id }).success(function (data) {
            $scope.detalles.Lista = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
    $scope.editarMaestro = function (model) {
        $scope.maestro = angular.copy(model);
        $scope.maestroCopia = angular.copy($scope.maestro);
        $scope.modalMaestro = true;
    }
    $scope.editarDetalle = function (model) {
        $scope.detalle = angular.copy(model);
        $scope.detalleCopia = angular.copy($scope.detalle);
        $scope.modalDetalle = true;
    }
    $scope.listarMaestros = function () {
        maestroService.listarMaestros({}).success(function (data) {
            $scope.maestros.Lista = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
    $scope.cargarDatos = function (model) {
        $scope.maestro = angular.copy(model);
    }
    $scope.listarMaestros();

});



app.controller('presentacionController', function ($scope, $rootScope , maestroService, SweetAlert) {

    $scope.nuevoRegistroPresentacion = function () {
        $scope.presentacion = {};
    }


    $scope.crearPresentacion = function () {

        $scope.presentacion.IdMaestro = idMaestroPresentacionConcepto;

        maestroService.guardarDetalleMaestro({ detalle: $scope.presentacion }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.obtenerPresentaciones($scope.presentacion.IdMaestro);
            $scope.presentacion = {};
            $('#modal-registro-presentacion').modal('hide');
        }).error(function (data) {
            SweetAlert.error("Error", data.error);

        });
    }

    $scope.obtenerPresentaciones = function () {

        $scope.idMaestro = idMaestroPresentacionConcepto;

        maestroService.listarDetallesMaestro({ idMaestro: $scope.idMaestro }).success(function (data) {
            $scope.listaPresentaciones = data;
        }).error(function (data) {
            SweetAlert.error("Error",data.error);
        });
    }

    $scope.editarPresentacion = function (model) {
        $scope.presentacion = angular.copy(model);
    }

    $scope.cerrar = function () {
        $('#modal-registro-presentacion').modal('hide');
    }

    $rootScope.dtOptions.withLightColumnFilter({
        '1': { type: 'text', className: 'form-control padding-left-right-3' },
        '2': { type: 'text', className: 'form-control padding-left-right-3' },
        '3': { type: 'text', className: 'form-control padding-left-right-3' },
    });


});

app.controller('serieComprobanteController', function ($scope, maestroService, serieComprobanteService) {
    $scope.tiposDeComprobante = [];
    $scope.sedes = [];
    $scope.series = {
        lista: []
    };
    $scope.detalle = {};
    $scope.model = {};
    $scope.guardar = function () {
        serieComprobanteService.guardar($scope.model).success(function (data) {
            $scope.messageSuccess(data.result_description);
            $scope.model.Id = data.data;
            $scope.elementos.lista.push(angular.copy($scope.model));
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
    $scope.actualizar = function (model) {
        serieComprobanteService.guardar(model).success(function (data) {
            $scope.messageSuccess(data.result_description);
            model.activoGuardar = false;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
    $scope.cargarTipoDeComprobante = function () {
        var i = 0;
        for (i = 0; i < $scope.tiposDeComprobante.length; i++) {
            if ($scope.model.IdTipoDeComprobante == $scope.tiposDeComprobante[i].Id) {
                $scope.model.TipoDeComprobante = $scope.tiposDeComprobante[i].Nombre;
                i = $scope.tiposDeComprobante.length;
            }
        }
    }
    $scope.cargarSede = function () {
        var i = 0;
        for (i = 0; i < $scope.sedes.length; i++) {
            if ($scope.model.IdSede == $scope.sedes[i].Id) {
                $scope.model.Sede = $scope.sedes[i].Nombre;
                i = $scope.sedes.length;
            }
        }
    }
    $scope.listarTiposDeComprobante = function () {
        serieComprobanteService.listarTiposDeComprobante({}).success(function (data) {
            $scope.tiposDeComprobante = data;
        });
    }
    $scope.obtenerSeries = function () {
        serieComprobanteService.obtenerSeries({}).success(function (data) {
            $scope.series.lista = data;
        });
    }
    $scope.listarSedes = function () {
        serieComprobanteService.listarSedes({}).success(function (data) {
            $scope.sedes = data;
        });
    }
    $scope.activarEditado = function (model) {
        model.activoGuardar = true;
    }

    /*$scope.listarSedes();
    $scope.listarTiposDeComprobante();*/
    $scope.obtenerSeries();
});

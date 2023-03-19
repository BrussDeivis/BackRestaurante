app.controller('inventarioLogicoController', function ($scope, $q, $compile, SweetAlert, $rootScope, $timeout, DTOptionsBuilder, DTColumnDefBuilder, almacenService) {

    $scope.inicializar = function () {
        $scope.obtenerFechaDelUltimoInventarioLogico();
    }

    $scope.obtenerFechaDelUltimoInventarioLogico = function () {
        almacenService.obtenerFechaDelUltimoInventarioLogico({}).success(function (data) {
            $scope.fechaUltimoInventarioLogico = data;
        }).error(function (data, status) {
            SweetAlert.error("Error", data.error);
        });
    }

    $scope.generarInventarioLogico = function () {
        almacenService.generarInventarioLogico({}).success(function (data) {
            SweetAlert.success("El inventario lógico fue generado con éxito", data.result_description);
            $scope.obtenerFechaDelUltimoInventarioLogico();
        }).error(function (data, status) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }



    
});




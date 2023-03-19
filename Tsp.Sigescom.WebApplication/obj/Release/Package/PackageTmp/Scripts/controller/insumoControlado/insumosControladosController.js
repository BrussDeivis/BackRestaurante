app.controller('insumosControladosController', function ($scope, $rootScope, librosElectronicosService) {

    $scope.listaDePeriodos = [];
    $scope.periodoSeleccionado = {};

    //Obtinene Libros Electronicos
    $scope.obtenerPeriodosDeLibrosElectronicos = function () {
        librosElectronicosService.obtenerPeriodosDeLibrosElectronicos({ idPeriodo: $scope.periodoSeleccionado.Id }).success(function (data) {
            $scope.listaDePeriodos = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    //otra funcionalidad: Ventas e Ingresos Detallada
    $scope.actualizarURLReporteDeVentaEIngresos = function () {//usa Contador
        $scope.URLReporteDeVentaEIngresos = URL_ + "/InsumosControlados/ReporteDeVentasEIngresos?idPeriodo=" + $scope.periodoSeleccionado.Id;
    }

    $scope.obtenerPeriodosDeLibrosElectronicos();
});
app.controller('inicializarCajaController', function ($scope, $timeout, $q, SweetAlert, maestroService, finanzaService) {

    $scope.inicializar = function () {
        $scope.cajasAInicializar = [];
        $scope.obtenerCajasAInicializar();
    };

    $scope.obtenerCajasAInicializar = function () {
        finanzaService.obtenerInicializarCaja({}).success(function (data) {
            $scope.cajasAInicializar = data;
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    };

    $scope.guardarInicializarCaja = function () {
        finanzaService.guardarInicializarCaja({ cajasAInicializar: $scope.cajasAInicializar }).success(function (data) {
            $scope.cajasAInicializar = data;
            $scope.obtenerCajasAInicializar();
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    } 
});

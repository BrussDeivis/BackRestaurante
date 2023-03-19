app.controller('ejemploController', function ($scope, $q, $rootScope, DTOptionsBuilder, DTColumnDefBuilder, ejemploService) {

    $scope.inicializar = function () {
        $scope.obtenerClientes();
        $scope.obtenerComprobantes();
    };

    $scope.obtenerClientes = function () {
        ejemploService.obtenerClientesDeEjemplo().success(function (data) {
            $scope.clientesDeEjemplo = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    };

    $scope.obtenerComprobantes = function () {
        ejemploService.obtenerComprobantesDeEjemplo().success(function (data) {
            $scope.comprobantesDeEjemplo = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    };


}); 
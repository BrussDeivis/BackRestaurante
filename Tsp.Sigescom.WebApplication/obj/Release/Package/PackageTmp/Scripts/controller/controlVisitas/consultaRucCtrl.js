app.controller('consultaRucCtrl', function ($scope, $timeout, DTOptionsBuilder, DTColumnDefBuilder, controlVisitasService ) {
    $scope.ruc = { lista: [] };

    $scope.buscar = function () {
        controlVisitasService.obtenerListaRuc({ }).success(function (data) {
            $scope.ruc.lista = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.buscar();
});
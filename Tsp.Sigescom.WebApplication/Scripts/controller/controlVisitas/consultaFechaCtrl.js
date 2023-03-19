app.controller('consultaFechaCtrl', function ($scope, $timeout, DTOptionsBuilder, DTColumnDefBuilder, controlVisitasService ) {
    $scope.fecha = "";
    $scope.persona = { lista: [] };

    $scope.buscar = function (fechaInicial, fechaFinal) {
        controlVisitasService.obtenerDatosPorFecha({ fechaDesde: fechaInicial, fechaHasta: fechaFinal }).success(function (data) {
            $scope.persona.lista = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
});
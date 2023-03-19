app.controller('consultaDniCtrl', function ($scope, $timeout, DTOptionsBuilder, DTColumnDefBuilder, controlVisitasService ) {
    $scope.fecha = "";
    $scope.persona = { lista: [] };

    $scope.buscar = function (dni, fechaInicial, fechaFinal) {
        console.log("llego");
        controlVisitasService.obtenerDatosPorDni({dni: dni, fechaDesde: fechaInicial, fechaHasta: fechaFinal }).success(function (data) {
            $scope.persona.lista = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
});
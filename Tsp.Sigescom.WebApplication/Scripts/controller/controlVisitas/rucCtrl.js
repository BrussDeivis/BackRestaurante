app.controller('rucCtrl', function ($scope, controlVisitasService) {
    $scope.ruc = "";

    $scope.guardar = function (numeroRuc) {
        controlVisitasService.obtenerDatosRucYGuardar({ ruc: numeroRuc }).success(function (data) {
            $scope.messageSuccess(data.result_description);
            $scope.ruc = "";
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.firstInput = function () {
        angular.element('#ingreso').focus()
    }

    $scope.firstInput();


});

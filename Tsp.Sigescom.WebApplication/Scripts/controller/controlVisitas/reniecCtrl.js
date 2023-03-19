app.controller('reniecCtrl', function ($scope, controlVisitasService) {
    $scope.dni = "";

    $scope.guardar = function (numeroDni) {
        controlVisitasService.obtenerDatosDniYGuardar({ dni: numeroDni }).success(function (data) {
            $scope.messageSuccess(data.result_description);
            //if (data.es_menor) {
            //    alert("El dni es de un menor");
            //}
            $scope.dni = "";
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.firstInput = function () {
        angular.element('#ingreso').focus()
    }

    $scope.firstInput();


});

app.controller('complementosController', function ($scope, $timeout, DTOptionsBuilder, DTColumnBuilder, SweetAlert, restauranteService) {

    $scope.obtenerComplementos = function () {
        restauranteService.obtenerComplementos({}).success(function (data) {
            $scope.complementos = data;
            $scope.complementos.forEach(complemento => {
                complemento.EsMultiple = complemento.EsMultiple ? "true" : "false";
            })
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.actualizarComplemento = function (complemento) {
        restauranteService.actualizarComplemento({ complemento : complemento }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.cambiarComplementoActivoRestaurante = function (complemento) {
        complemento.EstaActivoRestaurante = !complemento.EstaActivoRestaurante;
        restauranteService.actualizarComplemento({ complemento: complemento }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.obtenerComplementos();
});
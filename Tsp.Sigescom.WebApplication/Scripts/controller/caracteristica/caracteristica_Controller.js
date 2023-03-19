app.controller('caracteristica_Controller', function ($scope, $timeout, SweetAlert, DTOptionsBuilder, DTColumnBuilder, conceptoService) {
    /**
     * Archivo javascript que se utiliza en el menu de la aplicacion, para registrar valor caracteristica
     * 
     * */
    $scope.valores = { Lista: [] };
    $scope.valorCaracteristica = {};
    $scope.idCaracteristica = idCaracteristica;
    $scope.nombre = "";

    $scope.inicializar = function () {
        $scope.verValoresCaracteristica();
    }


    $scope.nuevoRegistro = function () {
        $scope.valorCaracteristica = {};
    }

    $scope.editarValorCaracteristica = function (valorCaracteristica) {
        $scope.valorCaracteristica = {};
        $scope.valorCaracteristica = angular.copy(valorCaracteristica);
    }

    $scope.guardarValorCaracteristica = function (valorCaracteristica) {
        conceptoService.guardarValorCaracteristica({ valorCaracteristica: valorCaracteristica, idCaracteristica: $scope.idCaracteristica }).success(function (data) {
            SweetAlert.success("Correcto",data.result_description);
            $('#modal-valorCaracteristica').modal('hide');
            $scope.verValoresCaracteristica();
            $scope.valorCarcateristica = {};
        }).error(function (data) {
            SweetAlert.error("Error", data.error);

        });
    }

    $scope.verValoresCaracteristica = function () {
        conceptoService.verValoresCaracteristica({ idCaracteristica: $scope.idCaracteristica }).success(function (data) {
            $scope.valores.Lista = data;
        }).error(function (data) {
            SweetAlert.error("Error", data.error);
        });
    }
});


app.controller('caracteristica_controller_mercaderia', function ($scope, $timeout, SweetAlert, conceptoService) {

    $scope.valorCaracteristica = {};
    $scope.nombre = "";

    $scope.inicializar = function () {
        $scope.nombre = "";
        $scope.valorCaracteristica = {};
    }

    $scope.nuevoRegistroValorCaracteristicaEnMercaderia = function (index, id, nombre) {
        $scope.inicializar();
        $scope.nombre = nombre;
        $scope.guardadoParaFuera = true;
        $scope.tipoRegistroFuera = 1;
        $scope.idCaracteristica = id;
        $scope.index = index;
    }

    $scope.guardarValorCaracteristica = function (valorCaracteristica) {
        conceptoService.guardarValorCaracteristica({ valorCaracteristica: valorCaracteristica, idCaracteristica: $scope.idCaracteristica }).success(function (data) {
            SweetAlert.success("Correcto",data.result_description);
            if ($scope.guardadoParaFuera) {
                var modeloScope = angular.element('#modelo-mercaderia').scope();
                switch ($scope.tipoRegistroFuera) {
                    case 1:
                        modeloScope.actualizarValoesCaracteristicasIngresados($scope.index, data.data, $scope.valorCaracteristica.Nombre);
                        break;
                    default:
                        break;
                }
                $('#modal-valorCaracteristica').modal('hide');
                return;
            }
            $('#modal-valorCaracteristica').modal('hide');
        }).error(function (data) {
            SweetAlert.error("Error", data.error);
        });
    }
});
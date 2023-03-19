app.controller('configuracionController', function ($scope, $rootScope, $compile, confirm, $timeout, blockUI, DTOptionsBuilder, DTColumnDefBuilder, facturacionElectronicaService) {

    $scope.configuracion = { Dias: [], Dia: []};
    $scope.diasSemama = [];

    $scope.cargarDias = function () {
        if ($scope.configuracion.Dia != null) {
            $scope.configuracion.Dias = [{ DiaName: {} }];
            for (var i = 0; i < $scope.configuracion.Dia.length; i++) {
                $scope.configuracion.Dias[i].DiaName = $scope.configuracion.Dia[i];
            }
        }
    }

    $scope.obtenerDias = function () {
        facturacionElectronicaService.obtenerDias({}).success(function (data) {
            $scope.diasSemama = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
    $scope.deshabilitarCampos = function (valor) {
        console.log(valor);
        if (valor == "1") {
            $("#diaIndividual").prop("disabled", true);
            $("#diaIndividual").val("");  
            $("#horaIndividual").prop("disabled", true);
            $("#horaIndividual").val("");
            $("#duracionIndividual").prop("disabled", true);
            $("#duracionIndividual").val("");
            $("#diaResumen").prop("disabled", true);
            $("#diaResumen").val("");
            $("#horaResumen").prop("disabled", true);
            $("#horaResumen").val("");
            $("#duracionResumen").prop("disabled", true);
            $("#duracionResumen").val("");
        } else if (valor == "2") {
            $("#diaIndividual").prop("disabled", false);
            $("#horaIndividual").prop("disabled", false);
            $("#duracionIndividual").prop("disabled", false);
            $("#diaResumen").prop("disabled", false);
            $("#horaResumen").prop("disabled", false);
            $("#duracionResumen").prop("disabled", false);  
            $("#horaIndividual").val("12:00 AM");
            $("#horaResumen").val("12:00 AM");
        }
    }
    
    $scope.obtenerDias();
    $scope.deshabilitarCampos(1);
});
app.controller('invalidarRechazadasController', function ($scope, $q, $timeout, $rootScope, SweetAlert, $filter, DTOptionsBuilder, DTColumnDefBuilder, facturacionElectronicaService ) {

    //#region BANDEJA DE COTIZACIONES 

    $scope.iniciarBandejaDeComprobantesRechazados = function () {
        //Cargar parametros
        $scope.fechaInicio = fechaInicio;
        $scope.fechaFin = fechaFin;
        //Listar bandeja
        $scope.listarBandeja();
    }

    $scope.listarBandeja = function () {
        facturacionElectronicaService.obtenerComprobantesRechazadosPorLimiteDeFecha({ desde: $scope.fechaInicio, hasta: $scope.fechaFin }).success(function (data) {
            $scope.comprobantesRechazados = data;
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema", data.error);
        });
    }

    //#endregion

    $scope.invalidarYReemitirComprobantesRechazados = function () {
        facturacionElectronicaService.invalidarYReemitirComprobantesRechazadosPorLimiteDeFecha({ desde: $scope.fechaInicio, hasta: $scope.fechaFin }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.listarBandeja();
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema", data.error);
        });
    }
    

});


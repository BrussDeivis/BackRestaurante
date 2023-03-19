app.controller('envioComprobantesController', function ($scope, $compile, SweetAlert, $rootScope, confirm, $timeout, blockUI, DTOptionsBuilder, DTColumnDefBuilder, facturacionElectronicaService) {
    $scope.bandeja = {};
    $scope.ComprobantesEnviados = { Lista: [] };
    $scope.verEnvio = {};

    $scope.listarBandeja = function (desde, hasta) {
        facturacionElectronicaService.listarEnviosEntreFechas({ desde: desde, hasta: hasta }).success(function (data) {
            $scope.ComprobantesEnviados.Lista = data;
            $scope.totalComprobantesEnviados = data;
            $scope.hayPendientes = data.find(envio => envio.EsPendiente) !== undefined ? true : false;
            $scope.hayRechazados = data.find(envio => envio.EsRechazado) !== undefined ? true : false;
            $scope.hayObservados = data.find(envio => envio.EsObservado) !== undefined ? true : false;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.cargarVerEnvio = function (envio) {
        $scope.verEnvio = angular.copy(envio);
    }

    $scope.consultarTickets = function () {
        facturacionElectronicaService.consultarTickets().success(function (data) {
            if (data.fallidas != null && data.fallidas != '') {
                SweetAlert.warning("Advertencia", data.result_description + data.fallidas);
            } else {
                SweetAlert.success("Correcto", data.result_description);
            }
            $scope.listarBandeja($scope.bandeja.FechaInicio, $scope.bandeja.FechaFinal);
        }).error(function (data) {
            SweetAlert.error2("Ocurrio un Problema", data.error);
        });
    }
    
    $scope.resolverResumenDiarioConExcepcion = function () {
        facturacionElectronicaService.resolverEnviosConExcepcion({}).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.listarBandeja($scope.bandeja.FechaInicio, $scope.bandeja.FechaFinal);
        }).error(function (data) {
            if (data.warning) SweetAlert.warning("Advertencia", data.error);
            else SweetAlert.error2("Ocurrio un Problema", data.error);
        });
    }

    $scope.reenviarEnvioRechazado = function (envio) {
        facturacionElectronicaService.resolverEnvioRechazado({idEnvio : envio.Id}).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.listarBandeja($scope.bandeja.FechaInicio, $scope.bandeja.FechaFinal);
        }).error(function (data) {
            SweetAlert.error2("Ocurrio un Problema", data.error);
        });
    }

    $scope.reenviarEnvioPendiente = function (envio) {
        facturacionElectronicaService.resolverEnvioPendiente({ idEnvio: envio.Id }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.listarBandeja($scope.bandeja.FechaInicio, $scope.bandeja.FechaFinal);
        }).error(function (data) {
            SweetAlert.error2("Ocurrio un Problema", data.error);
        });
    }
    $scope.verSoloEnviosPendientes = function () {
        let enviosPendientes = $scope.totalComprobantesEnviados.filter(envio => envio.EsPendiente);
        $scope.ComprobantesEnviados.Lista = undefined;
        $scope.ComprobantesEnviados.Lista = enviosPendientes;
    }
    
    $scope.verSoloEnviosRechazados = function () {
        let enviosRechazados = $scope.totalComprobantesEnviados.filter(envio => envio.EsRechazado);
        $scope.ComprobantesEnviados.Lista = undefined;
        $scope.ComprobantesEnviados.Lista = enviosRechazados;
    }

    $scope.verSoloEnviosObservados = function () {
        let enviosObservados = $scope.totalComprobantesEnviados.filter(envio => envio.EsObservado);
        $scope.ComprobantesEnviados.Lista = undefined;
        $scope.ComprobantesEnviados.Lista = enviosObservados;
    }

    $rootScope.dtOptions.withLightColumnFilter({
        '1': { type: 'text', className: 'form-control padding-left-right-3' },
        '2': { type: 'text', className: 'form-control padding-left-right-3' },
        '3': { type: 'text', className: 'form-control padding-left-right-3' },
        '4': { type: 'text', className: 'form-control padding-left-right-3' },
        '5': { type: 'text', className: 'form-control padding-left-right-3' },
        '6': { type: 'text', className: 'form-control padding-left-right-3' },
        '7': { type: 'text', className: 'form-control padding-left-right-3' },
        '8': { type: 'text', className: 'form-control padding-left-right-3' },
        '9': { type: 'text', className: 'form-control padding-left-right-3' }
    });

    $scope.inicio = function () {
        $scope.bandeja.FechaInicio = Desde;
        $scope.bandeja.FechaFinal = Hasta;
        $scope.listarBandeja(Desde, Hasta);
    }

    $scope.inicio();
});
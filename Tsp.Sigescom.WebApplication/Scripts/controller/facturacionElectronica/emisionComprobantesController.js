app.controller('emisionComprobantesController', function ($scope, $compile, $rootScope, SweetAlert, confirm, $timeout, blockUI, DTOptionsBuilder, DTColumnDefBuilder, facturacionElectronicaService) {
    $scope.bandeja = {};
    $scope.ComprobantesEmitidos = { Lista: [] };
    $scope.primeroNoEnviado = {};
    $scope.ultimoId = 0;
    $scope.idDocumentos = { Lista: [] };
    $scope.temporal = false;

    $scope.listarBandeja = function (fecha_inicio, fecha_fin) {
        facturacionElectronicaService.listarDocumentosEntreFechas({ desde: fecha_inicio, hasta: fecha_fin }).success(function (data) {
            $scope.ComprobantesEmitidos.Lista = data;
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }
    
    $scope.transmitirAFacturacionElectronica = function () {
        facturacionElectronicaService.transmitirAFacturacionElectronicaManual().success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.listarBandeja($scope.bandeja.FechaInicio, $scope.bandeja.FechaFinal);
        }).error(function (data) {
            SweetAlert.error2(data);
            $scope.listarBandeja($scope.bandeja.FechaInicio, $scope.bandeja.FechaFinal);
        });
    }
    $scope.enviarDocumentos = function () {
        facturacionElectronicaService.enviarDocumentos({}).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.listarBandeja($scope.bandeja.FechaInicio, $scope.bandeja.FechaFinal);
        }).error(function (data) {
            SweetAlert.error2(data);
            $scope.listarBandeja($scope.bandeja.FechaInicio, $scope.bandeja.FechaFinal);
        });
    }
    //$scope.enviarBoletas = function () {
    //    facturacionElectronicaService.resolverResumenDiarioBoletas({}).success(function (data) {
    //        SweetAlert.success("Correcto", data.result_description);
    //        $scope.listarBandeja($scope.bandeja.FechaInicio, $scope.bandeja.FechaFinal);
    //    }).error(function (data) {
    //        if (data.warning) SweetAlert.warning("Advertencia", data.error);
    //        SweetAlert.error2("Ocurrio un Problema", data.error);
    //    });
    //}

    //$scope.envioFacturas = function () {
    //    facturacionElectronicaService.envioFacturas({}).success(function (data) {
    //        SweetAlert.success("Correcto", data.result_description);
    //        $scope.listarBandeja($scope.bandeja.FechaInicio, $scope.bandeja.FechaFinal);
    //    }).error(function (data) {
    //        if (data.warning) SweetAlert.warning("Advertencia", data.error);
    //        else SweetAlert.error2("Ocurrio un Problema", data.error);
    //    });
    //}

    //$scope.enviarNotas = function () {
    //    facturacionElectronicaService.envioNotas({}).success(function (data) {
    //        SweetAlert.success("Correcto", data.result_description);
    //        $scope.listarBandeja($scope.bandeja.FechaInicio, $scope.bandeja.FechaFinal);
    //    }).error(function (data) {
    //        if (data.warning) SweetAlert.warning("Advertencia", data.error);
    //        else SweetAlert.error2("Ocurrio un Problema", data.error);
    //    });
    //}

    //$scope.enviarBajaDeFacturas = function () {
    //    facturacionElectronicaService.resolverComunicacionBajaFacturas({}).success(function (data) {
    //        SweetAlert.success("Correcto", data.result_description);
    //        $scope.listarBandeja($scope.bandeja.FechaInicio, $scope.bandeja.FechaFinal);
    //    }).error(function (data) {
    //        if (data.warning) SweetAlert.warning("Advertencia", data.error);
    //        SweetAlert.error2("Ocurrio un Problema", data.error);
    //    });
    //}

    //$scope.enviarGuiasDeRemision = function () {
    //    facturacionElectronicaService.envioGuiasDeRemision({}).success(function (data) {
    //        SweetAlert.success("Correcto", data.result_description);
    //        $scope.listarBandeja($scope.bandeja.FechaInicio, $scope.bandeja.FechaFinal);
    //    }).error(function (data) {
    //        if (data.warning) SweetAlert.warning("Advertencia", data.error);
    //        else SweetAlert.error2("Ocurrio un Problema", data.error);
    //    });
    //}

    $scope.regenerarJson = function (documento) {
        facturacionElectronicaService.regenerarJsonDocumento({ idDocumento: documento.Id }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.listarBandeja($scope.bandeja.FechaInicio, $scope.bandeja.FechaFinal);
        }).error(function (data) {
            SweetAlert.error2(data);
            $scope.listarBandeja($scope.bandeja.FechaInicio, $scope.bandeja.FechaFinal);
        });
    }

    $scope.regenerarJsonBandeja = function () {
        for (var i = 0; i < $scope.ComprobantesEmitidos.Lista.length; i++) {
            facturacionElectronicaService.regenerarJsonDocumento({ idDocumento: $scope.ComprobantesEmitidos.Lista[i].Id }).success(function (data) {
                console.log("Json regenerado correctamente");
            }).error(function (data) {
                SweetAlert.error2(data);
            });
        }
    }

    $rootScope.dtOptions.withLightColumnFilter({
        '0': { type: 'text', className: 'form-control padding-left-right-3' },
        '1': { type: 'text', className: 'form-control padding-left-right-3' },
        '2': { type: 'text', className: 'form-control padding-left-right-3' },
        '3': { type: 'text', className: 'form-control padding-left-right-3' },
        '4': { type: 'text', className: 'form-control padding-left-right-3' },
        '5': { type: 'text', className: 'form-control padding-left-right-3' },
        '6': { type: 'text', className: 'form-control padding-left-right-3' }
    });

    $scope.inicio = function () {
        $scope.bandeja.FechaInicio = Desde;
        $scope.bandeja.FechaFinal = Hasta;
        $scope.listarBandeja(Desde, Hasta);
    }

    $scope.inicio();
    
});
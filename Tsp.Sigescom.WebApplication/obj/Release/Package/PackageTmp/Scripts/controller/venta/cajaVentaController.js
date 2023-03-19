app.controller('cajaVentaController', function ($scope, $timeout, $rootScope, DTOptionsBuilder, DTColumnDefBuilder, ventaService, maestroService) {
    $scope.caja = { IdMedioDePago: IdMedioDePagoEfectivo };
    $scope.entidadesFinancieras = [];
    $scope.tiposDeTarjeta = [];
    $scope.IdMedioDePagoEfectivo = IdMedioDePagoEfectivo;
    $scope.caja.Observacion = 'Ninguna';
    $scope.ventas = { lista: [] };
    $scope.bandeja = {};

    $scope.inicio = function () {
        $scope.obtenerEntidadesFinancieras();
        $scope.obtenerOperadoresDeTarjeta();
        $scope.obtenerMediosDePago();
        $scope.bandeja.FechaInicio = Desde;
        $scope.bandeja.FechaFinal = Hasta;
        $scope.listarBandeja(Desde, Hasta);
    }

    $scope.listarBandeja = function (desde, hasta) {
        ventaService.obtenerOrdenesDeVentas({ desde: desde, hasta: hasta }).success(function (data) {
            $scope.ventas.lista = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.obtenerMediosDePago = function () {
        ventaService.obtenerMediosDePagoVenta({}).success(function (data) {
            $scope.mediosDePago = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.obtenerEntidadesFinancieras = function () {
        maestroService.obtenerEntidadesBancarias({}).success(function (data) {
            $scope.entidadesFinancieras = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.obtenerOperadoresDeTarjeta = function () {
        maestroService.obtenerOperadoresDeTarjeta({}).success(function (data) {
            $scope.tiposDeTarjeta = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.confimarPagarOrdenVenta = function (item) {
        
        $scope.itemPaga = item;
        $scope.caja = { IdMedioDePago: IdMedioDePagoEfectivo };
        $scope.caja.Observacion = 'Ninguna';
        $scope.caja.Total = item.Total;
    }
    
    $scope.guardarConfirmado = function () {
        ventaService.confirmarYPagarOrdenVenta({ idVenta: $scope.itemPaga.IdVenta, idSerieComprobante: $scope.itemPaga.IdTipoDocumento, confirmado: $scope.caja }).success(function (data) {
            $scope.messageSuccess(data.result_description);
            $scope.venta = { Detalles: [], IdMedioDePago: IdMedioDePagoEfectivo };
            $scope.listarBandeja(Desde, Hasta);
            $('#modal-pago-orden-venta').modal('hide');
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.anularOrdenVenta = function (item) {
        ventaService.anularOrdenDeVentaRegistrada({ idOrdenVenta: item.Id, observacion: $scope.caja.Observacion }).success(function (data) {
            $scope.messageSuccess(data.result_description);
            $scope.venta = { Detalles: [], IdMedioDePago: IdMedioDePagoEfectivo };
            $scope.listarBandeja(Desde, Hasta);
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.verOrdenVenta = function (item) {
        $scope.ver = item;
    }

    

    $scope.cancelar = function () {
        $scope.caja = { IdMedioDePago: IdMedioDePagoEfectivo };
        $scope.caja.Observacion = 'Ninguna';
    }
});

app.controller('bonificacionController', function ($scope, DTOptionsBuilder, DTColumnDefBuilder, precioService,maestroService, productoService) {
    $scope.bonificacion = {};
    $scope.bonificaciones = { Lista: [] }
    $scope.bonificacionInicial = {};
    $scope.productos = [];
    $scope.tarifas = {};
    $scope.productosMasPrecios = [];
    $scope.productoPrecio = {};
    $scope.mostrarBloque = true;
    $scope.VerDetalleProducto = false;

    $scope.obtenerBonificaciones = function () {
        precioService.obtenerBonificaciones({}).success(function (data) {
            $scope.bonificaciones.Lista = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
    $scope.obtenerProductos = function () {
        productoService.obtenerMercaderiasPorConceptoBasicoIncluyendoStockYPrecios({}).success(function (data) {
            $scope.productos = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
    
    $scope.nuevoRegistro = function () {
        $scope.bonificacion = {};
        $scope.bonificacionPrecio = {};
        $scope.bonificacionInicial = angular.copy($scope.bonificacion);
        $(".datepicker-start").datepicker('setStartDate', $scope.formatDate(new Date(), "ES"));
        $('#producto').select2("val", "");
        $('#productoReferencia').select2("val", "");
    }
    $scope.close = function () {
        if (angular.equals($scope.descuento, $scope.descuentoInicial)) {
            $('#modal-registro').modal('hide');
        }
        else {
            $('#modal-pregunta').click();
        }
    }
    $scope.closeModal = function () {
        $('#modal-registro').modal('hide');
    }


    $scope.guardarBonificacion = function () {
        precioService.guardarBonificacion({ bonificacion: $scope.bonificacion }).success(function (data) {
            $scope.messageSuccess(data.result_description);
            $scope.obtenerBonificaciones();
            $('#modal-registro').modal('hide');
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
    $scope.caducarPrecio = function (item) {
        precioService.caducarPrecio({ idPrecio: item.Id }).success(function (data) {
            $scope.messageSuccess(data.result_description);
            $scope.obtenerBonificaciones();
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
    $scope.obtenerBonificaciones();
    $scope.obtenerProductos();
});
app.controller('descuentoController', function ($scope, DTOptionsBuilder, DTColumnDefBuilder, precioService,maestroService) {
    $scope.descuento = {};
    $scope.descuentos = { Lista: [] }
    $scope.descuentoInicial = {};
    $scope.productos = [];
    $scope.tarifas = {};
    $scope.productosMasPrecios = [];
    $scope.productoPrecio = {};
    $scope.mostrarBloque = true;
    $scope.VerDetalleProducto = false;

    $scope.obtenerDescuentos = function () {
        precioService.obtenerDescuentos({}).success(function (data) {
            $scope.descuentos.Lista = data;
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
        $scope.descuento = {};
        $scope.descuentoInicial = angular.copy($scope.descuento);
        $(".datepicker-start").datepicker('setStartDate', $scope.formatDate(new Date(), "ES"));
        $('#producto').select2("val", "");
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


    $scope.guardarDescuento = function () {
        precioService.guardarDescuento({ descuento: $scope.descuento }).success(function (data) {
            $scope.messageSuccess(data.result_description);
            $scope.obtenerDecuentos();
            $('#modal-registro').modal('hide');
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
    $scope.caducarPrecio = function (item) {
        precioService.caducarPrecio({ idPrecio: item.Id }).success(function (data) {
            $scope.messageSuccess(data.result_description);
            $scope.obtenerDescuentos();
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
    $scope.obtenerDescuentos();
    $scope.obtenerProductos();
});
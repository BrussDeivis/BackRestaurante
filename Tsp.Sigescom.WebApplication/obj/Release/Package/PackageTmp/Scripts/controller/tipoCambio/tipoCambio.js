app.controller('tipoCambioController', function ($scope, $rootScope, DTOptionsBuilder, DTColumnDefBuilder, tipoCambioService) {

    $scope.tipoCambio = {};
    $scope.listaTipoCambio = [];


    //INICIALIZADORES
    $scope.inicializar  = function () {
        $scope.tipoCambio = {};

    }

    $scope.obtenerTipoCambio = function () {
        if ($scope.tipoCambio.Dia && $scope.tipoCambio.Mes && $scope.tipoCambio.Anho) {
            $scope.obtenerTipoCambioPorFecha();
        } else {
            if ($scope.tipoCambio.Mes && $scope.tipoCambio.Anho) {
                $scope.obtenerTiposDeCambioPorAnhoMes();
            }
        }
        
    }

    $scope.obtenerTiposDeCambioPorAnhoMes = function () {
        tipoCambioService.obtenerTiposDeCambioPorAnhoMes({ mes: $scope.tipoCambio.Mes, anho: $scope.tipoCambio.Anho }).success(function (data) {
            $scope.listaTipoCambio = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.obtenerTipoCambioPorFecha = function () {
        tipoCambioService.obtenerTipoCambioPorFecha({ dia: $scope.tipoCambio.Dia, mes: $scope.tipoCambio.Mes, anho: $scope.tipoCambio.Anho }).success(function (data) {
            $scope.listaTipoCambio = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    //MANEJO  DE LA TABLA 
    $rootScope.dtOptions.withLightColumnFilter({
        '0': { type: 'text', className: 'form-control padding-left-right-3' },
        '1': { type: 'text', className: 'form-control padding-left-right-3' },
        '2': { type: 'text', className: 'form-control padding-left-right-3' },
    });




});


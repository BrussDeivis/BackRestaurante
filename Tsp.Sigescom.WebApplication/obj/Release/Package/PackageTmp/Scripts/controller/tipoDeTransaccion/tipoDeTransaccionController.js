
app.controller('tipoDeTransaccionController', function ($scope, $rootScope, $timeout, DTOptionsBuilder, DTColumnDefBuilder, configuracionService) {

    $scope.tipoDeTransaccion = {};
    $scope.listaTiposDeTransaccionGenerico = [];
    $scope.listaAccionesDeNegocioPorTipoDeTransaccion = [];
    $scope.listaTiposDeTransaccionBandeja = [];

    //INICIALIZADORES

    $scope.nuevoRegistroTipoDeTransaccion = function (idTipoDeTransaccion) {
        $scope.tipoDeTransaccion = {};
        $scope.obtenerTiposDeTransaccionGenerico();
        $scope.obtenerAccionesDeNegocioPorTipoDeTransaccion();

    }

    $scope.inicializarTipoDeTransaccion = function () {
        $scope.tipoDeTransaccion = {};
        $scope.obtenerTiposDeTransaccionGenerico();
        $scope.listaAccionesDeNegocioPorTipoDeTransaccion = [];
    }

    $scope.obtenerTiposDeTransaccionGenerico = function () {
        configuracionService.obtenerTiposDeTransaccionGenerico().success(function (data) {
            $scope.listaTiposDeTransaccionGenerico = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.obtenerAccionesDeNegocioPorTipoDeTransaccion = function () {
        configuracionService.obtenerAccionesDeNegocioPorTipoDeTransaccion().success(function (data) {
            $scope.listaAccionesDeNegocioPorTipoDeTransaccion = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    //CREAR TIPO DE TRANSACCION
    $scope.crearTipoDeTransaccion = function () {
        configuracionService.crearTipoDeTransaccion({ tipoDeTransaccion: $scope.tipoDeTransaccion }).success(function (data) {
            $scope.messageSuccess(data.result_description);
            $scope.cerrar("modal-registro-tipo-de-transaccion");
            $scope.obtenerTiposDeTransaccionBandeja();
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    //OBTENER TIPOS DE TRANSACCION - BANDEJA 
    $scope.obtenerTiposDeTransaccionBandeja = function () {
        configuracionService.obtenerTiposDeTransaccionBandeja().success(function (data) {
            $scope.listaTiposDeTransaccionBandeja = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    //OBTENER TIPO DE TRANSACCION - EDITAR
    $scope.editarTipoDeTransaccion = function (idTipoDeTransaccion) {
        $scope.inicializarTipoDeTransaccion();
        $scope.tipoDeTransaccionInicial = {};
        configuracionService.obtenerTipoDeTransaccion({ idTipoDeTransaccion: idTipoDeTransaccion }).success(function (data) {
            $scope.tipoDeTransaccion = data;
            configuracionService.obtenerAccionesDeNegocioPorTipoDeTransaccionEditar({ accionesDeNegocioPorTipoDeTransaccion: $scope.tipoDeTransaccion.AccionesDeNegocioPorTipoDeTransaccion }).success(function (data) {
                $scope.listaAccionesDeNegocioPorTipoDeTransaccion = data;
            }).error(function (data) {
                $scope.messageError(data.error);
            });
            $scope.tipoDeTransaccionInicial = angular.copy($scope.tipoDeTransaccion);
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.habilitarDeshabilitarRadio = function (id) {
        if ($('#' + "accionDeNegocio" + id).is(':checked')) {
            $('#' + "checkbox-entrada-" + id).prop('disabled', false);
            $('#' + "checkbox-salida-" + id).prop('disabled', false);

        } else {
            $('#' + "checkbox-entrada-" + id).prop('disabled', true);
            $('#' + "checkbox-salida-" + id).prop('disabled', true);

            if ($('#' + "checkbox-salida-" + id).is(':checked')) {
                $('#' + "checkbox-salida-" + id).prop('checked', false);
            }

            if ($('#' + "checkbox-entrada-" + id).is(':checked')) {
                $('#' + "checkbox-entrada-" + id).prop('checked', false);
            }
            /*
            if ($('#' + "radio-entrada-" + id).is(':checked')) {
                $('#' + "radio-entrada-" + id).prop('checked', false);
                //$('#' + "radio-entrada-" + id).prop('required', false);
            } else {
                $('#' + "radio-salida-" + id).prop('checked', false);
                //$('#' + "radio-entrada-" + id).prop('required', false);
            } 
            */
        }
    }

    //CERRAR MODAL
    $scope.cerrar = function (nombreDelModal) {
        $('#' + nombreDelModal).modal('hide');
    }

    //MANEJO  DE LA TABLA 
    $rootScope.dtOptions.withLightColumnFilter({
        '0': { type: 'text', className: 'form-control padding-left-right-3' },
        '1': { type: 'text', className: 'form-control padding-left-right-3' },
        '2': { type: 'text', className: 'form-control padding-left-right-3' },
        '3': { type: 'text', className: 'form-control padding-left-right-3' },
        '4': { type: 'text', className: 'form-control padding-left-right-3' },

    });
});
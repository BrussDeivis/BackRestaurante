app.controller('configuracionController', function ($scope, configuracionService) {
    $scope.configuracion = {};
    $scope.configuracion_ = {};
    $scope.configuracionCopia = {};
    $scope.parametro = {};
    $scope.parametroCopia = {};
    $scope.configuraciones = { Lista: [] };
    $scope.parametros = { Lista: [] };

    $scope.tipoDatos = [{ tipo: "int" }, { tipo: "decimal" }, { tipo: "string" }, { tipo: "bool" }];

    $scope.nuevoRegistroConfiguracion = function () {
        $scope.configuracion = {};
        $scope.configuracionCopia = angular.copy($scope.configuracion);
    }

    $scope.cerrar = function () {
        if (angular.equals($scope.configuracion, $scope.configuracionCopia)) {
            $('#modal-registro-configuracion').modal('hide');
        }
        else {
            $('#modal-cambio').click();
        }
    }

    $scope.closeModal = function () {
            $('#modal-registro-configuracion').modal('hide');
    }

    $scope.nuevaConfiguracion = function () {
        configuracionService.guardarConfiguracion({ configuracion: $scope.configuracion }).success(function (data) {
            $scope.messageSuccess(data.result_description);
            $scope.listarConfiguraciones();
            $scope.configuracion = {};
            $('#modal-registro-configuracion').modal('hide');
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
    $scope.nuevoParametro = function () {
        $scope.parametro.IdConfiguracion = $scope.configuracion_.Id;
        configuracionService.guardarParametroConfiguracion({ parametro: $scope.parametro }).success(function (data) {
            $scope.messageSuccess(data.result_description);
            $scope.listarParametros($scope.configuracion_.Id);
            $scope.parametro = {};
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    
    $scope.editarParametro = function (item) {
        item.IdConfiguracion = $scope.configuracion_.Id;
        configuracionService.guardarParametroConfiguracion({ parametro: item }).success(function (data) {
            $scope.messageSuccess(data.result_description);
            $scope.listarParametros($scope.configuracion_.Id);
            $scope.parametro = {};
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.verParametros = function (item) {
        $scope.configuracion_ = item;
        $scope.listarParametros($scope.configuracion_.Id);
    }
    $scope.listarParametros = function (id) {
        configuracionService.listarParametrosConfiguracion({ idConfiguracion: id }).success(function (data) {
            $scope.parametros.Lista = data;
            $(function () {
                $(".select2").select2({
                    language: "es"
                });
            });
            //$timeout(function () {
            //    $('.parametro').select2({
            //        language: "es"
            //    });
            //}, 600);
            //$timeout(function () {
            //    $('.parametro').trigger("change");
            //}, 100);
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
    $scope.editarConfiguracion = function (model) {
        $scope.configuracion = angular.copy(model);
        $scope.configuracionCopia = angular.copy($scope.configuracion);
        $scope.modalConfiguracion = true;
    }
    $scope.listarConfiguraciones = function () {
        configuracionService.listarConfiguraciones({ }).success(function (data) {
            $scope.configuraciones.Lista = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
    $scope.cargarDatos = function (model) {
        $scope.configuracion=angular.copy(model);
    }

    $scope.listarConfiguraciones();
});

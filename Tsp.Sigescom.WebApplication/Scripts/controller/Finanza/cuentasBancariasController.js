app.controller('cuentasBancariasController', function ($scope, $timeout, $q, SweetAlert, maestroService, finanzaService) {

    $scope.inicializar = function () {
        $scope.idEntidadBancariaNinguna = idEntidadBancariaNinguna
        $scope.limpiarRegistro();
        $scope.cargarParametros();
        $scope.cargarColeccionesAsync();
        $scope.cargarColeccionesSync().then(function (resultado_) {
            $scope.establecerDatosPorDefecto();
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
    };

    $scope.limpiarRegistro = function () {
        $scope.cuentasBancarias = [];
        $scope.cuenta = {};
    };

    $scope.cargarParametros = function () {
        $scope.estaAgregandoCuentaBancaria = false;
    };

    $scope.cargarColeccionesAsync = function () {

    };

    $scope.cargarColeccionesSync = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        var promiseList = [];
        promiseList.push($scope.obtenerTiposCuentaBancaria());
        promiseList.push($scope.obtenerEntidadesFinancieras());
        promiseList.push($scope.obtenerMonedas());
        promiseList.push($scope.obtenerUbigeoDistrito());
        return $q.all(promiseList).then(function (response) {
            defered.resolve();
        }).catch(function (error) {
            defered.reject(e);
        });
    };

    $scope.establecerDatosPorDefecto = function () {
        $scope.listarCuentasBancarias();
    };

    $scope.obtenerTiposCuentaBancaria = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        maestroService.obtenerTiposCuentaBancaria({}).success(function (data) {
            $scope.tiposCuenta = data;
            defered.resolve();
        }).error(function (data) {
            SweetAlert.error2(data);
            defered.reject(data);
        });
        return promise;
    };

    $scope.obtenerEntidadesFinancieras = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        maestroService.obtenerEntidadesFinancieras({}).success(function (data) {
            $scope.entidadesFinancieras = data;
            defered.resolve();
        }).error(function (data) {
            SweetAlert.error2(data);
            defered.reject(data);
        });
        return promise;
    };

    $scope.obtenerMonedas = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        maestroService.obtenerMonedas({}).success(function (data) {
            $scope.monedas = data;
            defered.resolve();
        }).error(function (data) {
            SweetAlert.error2(data);
            defered.reject(data);
        });
        return promise;
    };

    $scope.obtenerUbigeoDistrito = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        maestroService.listarUbigeoDistrito().success(function (data) {
            $scope.ubigeos = data;
            defered.resolve();
        }).error(function (data) {
            SweetAlert.error2(data);
            defered.reject(data);
        });
        return promise;
    };

    $scope.listarCuentasBancarias = function () {
        finanzaService.obtenerCuentasBancarias({}).success(function (data) {
            for (var i = 0; i < data.length; i++) {
                data[i].Ubigeo = Enumerable.from($scope.ubigeos).where("$.Id == '" + parseInt(data[i].Ubigeo) + "'").toArray()[0];
            }
            $scope.cuentasBancarias = data;
            $(function () {
                $(".select2").select2({
                    language: "es"
                });
            });
            $timeout(function () { $('.selectCuentaBancaria').trigger("change"); }, 100);
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }
     
    $scope.agregarCuentaBancaria = function () {
        $scope.cuenta.Ubigeo = $scope.cuenta.Ubigeo.Id.toString();
        finanzaService.guardarCuentaBancaria({ cuentaBancaria : $scope.cuenta }).success(function (data) {
            $scope.listarCuentasBancarias();
            $scope.cuenta = {};
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.editarCuentaBancaria = function (item) {
        let ubigeo = angular.copy(item.Ubigeo);
        item.Ubigeo = item.Ubigeo.Id.toString();
        let cuenta = angular.copy(item);
        item.Ubigeo = ubigeo;
        finanzaService.guardarCuentaBancaria({ cuentaBancaria: cuenta }).success(function (data) {
            cuenta = {};
        }).error(function (data) {
            SweetAlert.error2(data);
            $scope.listarCuentasBancarias();
        });
    }

    $scope.iniciarAgregarCuentaBancaria = function () {
        $scope.estaAgregandoCuentaBancaria = true;
        $scope.cuenta.TipoCuenta = $scope.tiposCuenta[0];
        $scope.cuenta.EntidadFinanciera = $scope.entidadesFinancieras[0];
        $scope.cuenta.Moneda = $scope.monedas[0];
        $scope.cuenta.Ubigeo = $scope.ubigeos[0];
        $scope.cuenta.EstaActivo = true;
    }
    
    $scope.cancelarAgregarCuentaBancaria = function () {
        $scope.estaAgregandoCuentaBancaria = false;
        $scope.cuenta = {};
    }
 
});

angular.
    module('app').
    component('selectorVehiculo', {
        templateUrl: "../Scripts/controller/cochera/selectorVehiculo/selectorVehiculo.html",
        bindings: {
            vehiculo: '=',
            vchanged: '&',
            estaRegistrando:'='
        },

        controller: function ($q, $scope, $element, SweetAlert, cocheraService, $http) {
            $('select:not(.normal)').each(function () {
                $(this).select2({
                    dropdownParent: $(this).parent()
                });
            });
            var ctrl = this;
            ctrl.fireEventChanged = function () {
                ctrl.vchanged();
            };

            ctrl.inicializar = function () {
                //ctrl.cargarParametros();
                ctrl.cargarColeccionesAsync();
                ctrl.cargarColeccionesSync().then(function (resultado_) {
                    ctrl.vehiculo = { Marca: {}, TipoDeVehiculo: {}};
                    ctrl.establecerDatosPorDefecto();
                }, function (error) {
                    $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
                });
                ctrl.estaRegistrando= false;
            };

            ctrl.establecerDatosPorDefecto = function () {
                
                ctrl.vehiculo.TipoDeVehiculo = ctrl.tiposDeVehiculos[0];
                ctrl.vehiculo.Marca = ctrl.marcasDeVehiculos[0];
            };

            ctrl.cargarColeccionesAsync = function () {
            };

            ctrl.limpiarInputsExceptoPlaca = function () {
                var placa = ctrl.vehiculo.Placa;
                ctrl.vehiculo = { Placa:placa,
                    Marca: {}, TipoDeVehiculo: {}};
            }

            ctrl.cargarColeccionesSync = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                var promiseList = [];
                promiseList.push(ctrl.obtenerTiposDeVehiculos());
                promiseList.push(ctrl.obtenerMarcasDeVehiculos());
                return $q.all(promiseList).then(function (response) {
                    defered.resolve();
                }).catch(function (error) {
                    defered.reject(e);
                });
            };

            ctrl.obtenerTiposDeVehiculos = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                cocheraService.obtenerTiposDeVehiculo({}).success(function (data) {
                    ctrl.tiposDeVehiculos = data;
                    defered.resolve();
                }).error(function (data) {
                    $scope.messageError(data.error);
                    SweetAlert.error("Ocurrio un Problema", data.error);
                    defered.reject(data);
                });
                return promise;
            };

            ctrl.obtenerMarcasDeVehiculos = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                cocheraService.obtenerMarcasDeVehiculo({}).success(function (data) {
                    ctrl.marcasDeVehiculos = data;

                   

                    defered.resolve();
                }).error(function (data) {
                    $scope.messageError(data.error);
                    SweetAlert.error("Ocurrio un Problema", data.error);
                    defered.reject(data);
                });
                return promise;
            };

            ctrl.validarPlaca = function () {
                if (ctrl.vehiculo.Placa != null && ctrl.vehiculo.Placa != "") {
                    cocheraService.obtenerVehiculoPorPlaca({ placa: ctrl.vehiculo.Placa }).success(function (data) {
                        if (data.encontrado) {
                            ctrl.vehiculo = data.data;
                            ctrl.vehiculo.TipoDeVehiculo = ctrl.tiposDeVehiculos.find(tv => tv.Id === ctrl.vehiculo.TipoDeVehiculo.Id);
                            ctrl.estaRegistrando= false;
                            ctrl.fireEventChanged();

                        } else {
                            ctrl.estaRegistrando= true;
                            ctrl.estaAgregandoVehiculo = true;
                            ctrl.limpiarInputsExceptoPlaca();
                            ctrl.establecerDatosPorDefecto();

                        }
                    }).error(function (data) {
                        SweetAlert.error("Ocurrio un Problema", data.error);
                    });
                }
            };

            ctrl.habilitarEdicion = function (placa) {
                ctrl.estaEditando = true;
                ctrl.estaRegistrando = true;
                ctrl.vehiculoAEditar = angular.copy(ctrl.vehiculo);
            };

            ctrl.cancelar = function (form) {
                if (form) {
                    form.$setPristine();
                    form.$setUntouched();
                }
                ctrl.estaEditando = false;
                ctrl.estaAgregando = false;
                ctrl.estaRegistrando= false;
                ctrl.vehiculo = ctrl.vehiculoAEditar;
                ctrl.vehiculo.TipoDeVehiculo = ctrl.tiposDeVehiculos.find(tv => tv.Id === ctrl.vehiculo.TipoDeVehiculo.Id);
            };

            ctrl.guardar = function () {
                //ctrl.vehiculo.TipoDeVehiculo = ctrl.tipoDeVehiculoSeleccionado;
                cocheraService.guardarVehiculo({ vehiculo: ctrl.vehiculo }).success(function (data) {
                    SweetAlert.success("Correcto", data.result_description);
                    ctrl.vehiculo = data.information;
                    ctrl.vehiculo.TipoDeVehiculo = ctrl.tiposDeVehiculos.find(tv => tv.Id === ctrl.vehiculo.TipoDeVehiculo.Id);
                    ctrl.fireEventChanged();
                    ctrl.estaEditando = false;
                    ctrl.estaAgregando = false;
                    ctrl.estaRegistrando = false;
                }).error(function (data) {
                    SweetAlert.error("Ocurrio un Problema", data.error);
                });
            };

            this.$onInit = function () {

            };
        }
    });
angular.
    module('app').
    component('registradorConsumos', {
        templateUrl: "../Scripts/controller/hotel/registradorConsumos/registradorConsumos.html",
        bindings: {
            api: '=',
            idEstablecimiento: '<',
            consumoGuardado: '&',
        },

        controller: function ($q, $scope, $timeout, SweetAlert, actorComercialService, maestroService, hotelService) {
            $('select:not(.normal)').each(function () {
                $(this).select2({
                    dropdownParent: $(this).parent()
                });
            });
            var ctrl = this;

            ctrl.inicializar = function () {
                ctrl.cargarParametros();
                ctrl.obtenerAtencionesCheckedIn();
                ctrl.inicializarComponentes();
            };

            ctrl.cargarParametros = function () {
            };

            ctrl.inicializarComponentes = function () {
                ctrl.inicializacionRealizada = true;
            }

            ctrl.establecerDatosPorDefecto = function () {
            };

            ctrl.confirmarConsumo = function () {
                hotelService.confirmarConsumo({ consumoHabitacion: ctrl.consumoHabitacion }).success(function (data) {
                    SweetAlert.success("Correcto", data.result_description);
                    ctrl.cerrarModal('modal-registrador-consumo');
                    ctrl.consumoGuardado({});
                }).error(function (data) {
                    SweetAlert.error2(data);
                });
            }

            ctrl.obtenerAtencionesCheckedIn = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                hotelService.obtenerAtencionesEnCheckedInComoHabitaciones({ idEstablecimiento: ctrl.idEstablecimiento }).success(function (data) {
                    ctrl.atencionesCheckedIn = data;
                    defered.resolve();
                }).error(function (data) {
                    SweetAlert.error2(data);
                    defered.reject(data);
                });
                return promise;
            }

            ctrl.obtenerConsumosDeHabitacion = function () {
                hotelService.obtenerConsumoHabitacionAtencion({ idAtencion: ctrl.atencionSeleccionada.Id }).success(function (data) {
                    ctrl.consumoHabitacion = data;
                    ctrl.consumoHabitacion.Detalles = [];
                    ctrl.importeTotalDeConsumos = 0;
                    ctrl.consumoHabitacion.Consumos.forEach(ch => ctrl.importeTotalDeConsumos += ch.Importe);
                    ctrl.verificarInconsistencias();
                }).error(function (data) {
                    SweetAlert.error2(data);
                });
            }

            ctrl.nuevoConsumo = function () {
                ctrl.consumoHabitacion = {};
                ctrl.atencionSeleccionada = undefined;
                ctrl.verificarInconsistencias();
                ctrl.obtenerAtencionesCheckedIn();
            }

            ctrl.nuevoConsumoCargado = function (idAtencion) {
                if (idAtencion !== undefined) {
                    ctrl.obtenerAtencionesCheckedIn().then(function () {
                        ctrl.idAtencion = idAtencion;
                        ctrl.atencionSeleccionada = ctrl.atencionesCheckedIn.find(h => h.Id === ctrl.idAtencion);
                        $timeout(function () { $('#atencionSeleccionada').trigger("change"); }, 100);
                        ctrl.obtenerConsumosDeHabitacion();
                    }, function (error) {
                        $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
                    });
                }
            }

            ctrl.cambioImporteTotal = function (importeTotal) {
                ctrl.consumoHabitacion.Importe = parseFloat(importeTotal);
                ctrl.verificarInconsistencias();
            }

            ctrl.cerrarRegistrarConsumos = function () {
                ctrl.cerrarModal('modal-registrador-consumo');
            }

            ctrl.cerrarModal = function (nombreDelModal) {
                $('#' + nombreDelModal).modal('hide');
            }

            ctrl.verificarInconsistencias = function () {
                ctrl.inconsistenciasConsumo = [];
                if (ctrl.atencionSeleccionada == undefined) {
                    ctrl.inconsistenciasConsumo.push("Es necesario seleccionar una habitación.")
                } else {
                    if (ctrl.consumoHabitacion.HuespedConsumo == undefined) {
                        ctrl.inconsistenciasConsumo.push("Es necesario seleccionar el huesped a cargo del consumo.")
                    }
                    if (ctrl.consumoHabitacion.Detalles.length == 0) {
                        ctrl.inconsistenciasConsumo.push("Es necesario seleccionar al menos un detalle.")
                    }
                    if (ctrl.consumoHabitacion.Importe == 0) {
                        ctrl.inconsistenciasConsumo.push("Es necesario que el importe del consumo sea mayor a 0.")
                    }
                }
            }

            this.$onInit = function () {
                ctrl.api = {};
                ctrl.inicializar();
                ctrl.api.NuevoConsumo = ctrl.nuevoConsumo;
                ctrl.api.NuevoConsumoCargado = ctrl.nuevoConsumoCargado;
            };



        }
    });

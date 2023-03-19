angular.
    module('app').
    component('registradorHuesped', {
        templateUrl: "../Scripts/controller/hotel/registradorHuesped/registradorHuesped.html",
        bindings: {
            api: '=',
            atencion: '=',
            puedeGuardarHuesped: '<',
            puedeModificarHuesped: '<',
            changed: '&',
            externalId: '<'
        },

        controller: function ($q, $scope, $timeout, SweetAlert, actorComercialService, maestroService, hotelService) {
            $('select:not(.normal)').each(function () {
                $(this).select2({
                    dropdownParent: $(this).parent()
                });
            });
            var ctrl = this;

            ctrl.cambioRegistradorHuesped = function () {
                ctrl.changed();
            };

            ctrl.inicializar = function () {
                ctrl.limpiarHuesped();
                ctrl.cargarColeccionesSync().then(function () {
                    ctrl.establecerDatosPorDefecto();
                    ctrl.iniciarComponentes();
                }, function (error) {
                    SweetAlert.error("Ocurrio un Problema", error);
                });
                ctrl.cargarColeccionesAsync();
                ctrl.verificarHuespedTitular();
            };

            ctrl.limpiarHuesped = function () {
                ctrl.huesped = {};
            }

            ctrl.cargarColeccionesSync = function () {
                var defered = $q.defer();
                var promiseList = [];
                promiseList.push(ctrl.cargarParametros());
                promiseList.push(ctrl.obtenerMotivosDeViaje());
                return $q.all(promiseList).then(function (response) {
                    defered.resolve();
                }).catch(function (error) {
                    defered.reject(e);
                });
            };

            ctrl.verificarHuespedTitular = function () {
                ctrl.indexHuespedTitular = ctrl.atencion.Huespedes.findIndex(m => m.EsTitular);
            }

            ctrl.cargarParametros = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                hotelService.obtenerParametrosParaRegistradorHuesped({}).success(function (data) {
                    ctrl.parametros = data.data;
                    defered.resolve();
                }).error(function (data) {
                    SweetAlert.error2(data);
                    defered.reject(data);
                });
                return promise;
            };

            ctrl.establecerDatosPorDefecto = function () {
                ctrl.motivoDeViaje = ctrl.motivosDeViaje[0];
            };

            ctrl.cambioHuesped = function (actorComercial) {
                ctrl.obtenerUltimoMotivoViajeHuesped(actorComercial.Id);
            }

            ctrl.iniciarComponentes = function () {
                ctrl.rolHuesped = { Id: ctrl.parametros.IdRolCliente, Nombre: 'CLIENTE' };
                ctrl.inicializacionRealizada = true;
            };

            ctrl.cargarColeccionesAsync = function () {
            };

            ctrl.obtenerMotivosDeViaje = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                maestroService.obtenerMotivosDeViaje().success(function (data) {
                    ctrl.motivosDeViaje = data;
                    defered.resolve();
                }).error(function (data) {
                    SweetAlert.error2(data);
                    defered.reject(data);
                });
                return promise;
            };

            ctrl.obtenerUltimoMotivoViajeHuesped = function (idHuesped) {
                hotelService.obtenerUltimoMotivoViajeHuesped({ idHuesped: idHuesped }).success(function (data) {
                    if (data != "") {
                        ctrl.motivoDeViaje = data;
                    }
                    $timeout(function () { $('.motivoDeViaje').trigger("change"); }, 100);
                }).error(function (data) {
                    SweetAlert.error2(data);
                });
            };

            ctrl.exiteHuespedRegistrado = function () {
                let existeHuesped = ctrl.atencion.Huespedes.find(m => m.Id == ctrl.huesped.Id);
                return existeHuesped;
            };

            ctrl.agregarHuespedHabitacion = function () {
                ctrl.huesped.EsTitular = ctrl.atencion.Huespedes.length == 0 ? true : false;
                if (ctrl.puedeGuardarHuesped) {
                    hotelService.agregarHuesped({ idAtencion: ctrl.atencion.Id, idActorComercial: ctrl.huesped.Id, idMotivoViaje: ctrl.motivoDeViaje.Id, esTitular: ctrl.huesped.EsTitular }).success(function (data) {
                        ctrl.huesped.IdHuesped = data.data;
                        ctrl.agregarHuesped();
                    }).error(function (data) {
                        SweetAlert.error2(data);
                    });
                } else {
                    ctrl.agregarHuesped();
                }
            };

            ctrl.cambiarTitularHuesped = function (huespedes, index) {
                ctrl.indexHuespedTitular = huespedes.findIndex(h => h.EsTitular && h.IdHuesped != huespedes[index].IdHuesped);
                if (ctrl.puedeGuardarHuesped) {
                    ctrl.indexHuespedTitular = huespedes.findIndex(h => h.EsTitular && h.IdHuesped != huespedes[index].IdHuesped);
                    hotelService.cambiarTitularHuesped({ idHuespedCambiado: ctrl.atencion.Huespedes[ctrl.indexHuespedTitular].IdHuesped, idHuespedNuevoTitular: ctrl.atencion.Huespedes[index].IdHuesped }).success(function (data) {
                        huespedes.forEach(h => h.EsTitular = false);
                        huespedes[index].EsTitular = true;
                    }).error(function (data) {
                        SweetAlert.error2(data);
                    });
                } else {
                    huespedes.forEach(h => h.EsTitular = false);
                    huespedes[index].EsTitular = true;
                }
            };

            //ctrl.cambiarTitularHuesped = function (index) {
            //    if (ctrl.puedeGuardarHuesped) {
            //        hotelService.cambiarTitularHuesped({ idHuespedCambiado: ctrl.idHuespedTitular, idHuespedNuevoTitular: ctrl.atencion.Huespedes[index].IdHuesped }).success(function (data) {
            //            //ctrl.atencion.Huespedes[ctrl.atencion.Huespedes.findIndex(m => m.IdHuesped == ctrl.idHuespedTitular)].EsTitular = false;
            //            //ctrl.idHuespedTitular = ctrl.atencion.Huespedes[index].IdHuesped;
            //        }).error(function (data) {
            //            SweetAlert.error2(data);
            //        });
            //    } //else {
            //       // ctrl.atencion.Huespedes[ctrl.atencion.Huespedes.findIndex(m => m.IdHuesped == idHuespedTitular)].EsTitular = false;
            //    //}
            //};

            ctrl.agregarHuesped = function () {
                ctrl.huesped.MotivoDeViaje = ctrl.motivoDeViaje;
                if (ctrl.huesped.EsTitular) ctrl.idHuespedTitular = ctrl.huesped.IdHuesped;
                ctrl.atencion.Huespedes.push(angular.copy(ctrl.huesped));
                ctrl.cambioRegistradorHuesped();
                ctrl.selectorHuespedAPI.EstablecerActorPorDefecto();
            };

            ctrl.quitarHuespedHabitacion = function (index) {
                if (ctrl.puedeGuardarHuesped) {
                    hotelService.eliminarHuesped({ idHuesped: ctrl.atencion.Huespedes[index].IdHuesped }).success(function (data) {
                        ctrl.atencion.Huespedes.splice(index, 1);
                        ctrl.cambioRegistradorHuesped();
                    }).error(function (data) {
                        SweetAlert.error2(data);
                    });
                } else {
                    ctrl.atencion.Huespedes.splice(index, 1);
                }
            };

            this.$onInit = function () {
                ctrl.api = {};
                ctrl.inicializar();
                //ctrl.api.InicializarRegistroGuiaRemision = ctrl.inicializarRegistroGuiaRemision;
                //ctrl.api.LimpiarGuiaRemision = ctrl.limpiarGuiaRemision;
                //ctrl.api.CargarDatosDesdeVenta = ctrl.cargarDatosDesdeVenta;
                //ctrl.api.NuevoRegistroGuiaRemision = ctrl.nuevoRegistroGuiaRemision;

            };
        }
    });


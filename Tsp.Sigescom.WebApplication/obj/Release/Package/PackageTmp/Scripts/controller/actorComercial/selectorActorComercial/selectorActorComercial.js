angular.
    module('app').
    component('selectorActorComercial', {
        templateUrl: "../Scripts/controller/actorComercial/selectorActorComercial/selectorActorComercial.html",
        bindings: {
            api: '=',
            actorComercial: '=',
            rol: '<',
            mascaraVisualizacionValidacion: '<',
            etiquetaRol: '<',
            changed: '&',
            minimoCaracteresBuscarActorComercial: '<',
            tiempoEsperaBusquedaSelector: '<',
            externalId: '<',
            inicioRealizado: '&',
            permitirSeleccionarGrupo: '<',
        },

        controller: function ($q, $http, $scope, $timeout, SweetAlert, actorComercialService, maestroService) {
            var ctrl = this;

            ctrl.actorComercialSeleccionado = function () {
                ctrl.changed({ actorComercial: ctrl.actorComercial });
            };

            ctrl.inicioTerminado = function () {
                ctrl.inicioRealizado({});
            };

            ctrl.inicializar = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                ctrl.busqueda = {};
                ctrl.actorComercial = {};
                ctrl.actorComercialPorDefecto = {};
                ctrl.etiquetaRol = ctrl.etiquetaRol == undefined ? ctrl.rol.Nombre : ctrl.etiquetaRol;
                //ctrl.mostrarBusquedaActorComercial = false;
                ctrl.cargarParametros().then(function () {
                    ctrl.obtenerActorComercialPorDefecto().then(function () {
                        ctrl.establecerActorPorDefecto();
                        if (ctrl.cargarActorInicio) {
                            ctrl.obtenerActorComercialPorId(ctrl.idActorCargarInicio).then(function (resultado_) {
                                ctrl.actorComercialSeleccionado();
                            }, function (error) {
                                ctrl.mensaje = "Se ha producido un error al obtener el dato:" + error;
                            });
                        }
                        ctrl.inicioTerminado();
                        defered.resolve();
                    }, function (error) {
                        ctrl.mensaje = "Se ha producido un error al obtener el dato:" + error;
                        defered.reject(error);
                    });
                    if (ctrl.permitirSeleccionarGrupo) {
                        ctrl.obtenerGruposActoresComerciales();
                    }
                }, function (error) {
                    ctrl.mensaje = "Se ha producido un error al obtener el dato:" + error;
                    defered.reject(error);
                });
                return promise;
            };

            ctrl.cargarParametros = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                actorComercialService.obtenerParametrosParaSelectorDeActorComercial({}).success(function (data) {
                    ctrl.parametros = data.data;
                    defered.resolve();
                }).error(function (data) {
                    ctrl.messageError(data.error);
                    defered.reject(data);
                });
                return promise;
            };

            ctrl.establecerActorPorDefecto = function () {
                ctrl.actorComercial = angular.copy(ctrl.actorComercialPorDefecto);
                ctrl.actorComercialSeleccionado();
            };

            ctrl.obtenerActorComercialPorDefecto = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                let idActorGenerico = (ctrl.rol.Id == ctrl.parametros.IdRolCliente) ? ctrl.parametros.IdClientePorDefecto : ((ctrl.rol.Id == ctrl.parametros.IdRolProveedor) ? ctrl.parametros.IdProveedorPorDefecto : ctrl.parametros.IdEmpleadoPorDefecto);
                actorComercialService.obtenerActorComercialPorId({ idRol: ctrl.rol.Id, id: idActorGenerico }).success(function (data) {
                    ctrl.actorComercialPorDefecto = data.information;
                    ctrl.actorComercialPorDefecto.esValido = true;
                    ctrl.actorComercialPorDefecto.esGenerico = ctrl.rol.Id != ctrl.parametros.IdRolEmpleado;
                    defered.resolve();
                }).error(function (data) {
                    SweetAlert.error("Ocurrio un Problema", data.error);
                    ctrl.actorComercial.esValido = false;
                    defered.reject(data);
                });
                return promise;
            };

            ctrl.obtenerActorComercialPorId = function (idActorComercial) {
                var defered = $q.defer();
                var promise = defered.promise;
                actorComercialService.obtenerActorComercialPorId({ idRol: ctrl.rol.Id, id: idActorComercial }).success(function (data) {
                    ctrl.actorComercial = data.information;
                    ctrl.validarActorComercial();
                    defered.resolve();
                }).error(function (data) {
                    SweetAlert.error("Ocurrio un Problema", data.error);
                    ctrl.actorComercial.esValido = false;
                    defered.reject(data);
                });
                return promise;
            };

            ctrl.obtenerGruposActoresComerciales = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                actorComercialService.obtenerGruposActoresComercialesPorRol({ idRol: ctrl.rol.Id }).success(function (data) {
                    ctrl.busqueda.GruposActorComercial = data;
                    defered.resolve();
                }).error(function (data) {
                    SweetAlert.error("Ocurrio un Problema", data.error);
                    defered.reject(data);
                });
                return promise;
            };

            ctrl.obtenerActoresComercialesDeActoresComerciales = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                actorComercialService.obtenerActoresComercialesDeGrupoActoresComercialesPorRol({ idRol: ctrl.rol.Id, idGrupoActoresComerciales: ctrl.busqueda.GrupoActorComercial.Id }).success(function (data) {
                    ctrl.busqueda.ActorComercialGrupo = undefined;
                    ctrl.busqueda.ActoresComerciales = data;
                    defered.resolve();
                }).error(function (data) {
                    SweetAlert.error("Ocurrio un Problema", data.error);
                    defered.reject(data);
                });
                return promise;
            };

            ctrl.obtenerGruposActoresComercialesDeActorComercial = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                actorComercialService.obtenerGruposActoresComercialesPorRolDeActorComercial({ idRol: ctrl.rol.Id, idActorComercial: ctrl.actorComercial.Id }).success(function (data) {
                    ctrl.gruposActorComercial = data;
                    ctrl.actorComercial.SeleccionarGrupo = ctrl.gruposActorComercial.length > 0;
                    defered.resolve();
                }).error(function (data) {
                    SweetAlert.error("Ocurrio un Problema", data.error);
                    defered.reject(data);
                });
                return promise;
            };

            ctrl.obtenerActorComercialYAvisar = function () {
                ctrl.numeroDocumentoActorComercial = ctrl.actorComercial.NumeroDocumentoIdentidad;
                if (ctrl.actorComercial.NumeroDocumentoIdentidad) {
                    ctrl.obtenerActorComercial().then(function (resultado_) {
                        ctrl.actorComercialSeleccionado();
                    }, function (error) {
                        ctrl.mensaje = "Se ha producido un error al obtener el dato:" + error;
                    });
                }
            };

            ctrl.obtenerActorComercial = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                actorComercialService.resolverActorComercialPorDocumentoDeIdentidad({ idRol: ctrl.rol.Id, numeroDocumento: ctrl.actorComercial.NumeroDocumentoIdentidad }).success(function (data) {
                    ctrl.actorComercial = data.information;
                    ctrl.validarActorComercial();
                    defered.resolve();
                }).error(function (data) {
                    SweetAlert.error("Ocurrio un Problema", data.error);
                    ctrl.actorComercial.esValido = false;
                    defered.reject(data);
                });
                return promise;
            };

            ctrl.validarActorComercial = function () {
                ctrl.actorComercial.esValido = true;
                ctrl.actorComercial.esGenerico = (ctrl.rol.Id == ctrl.parametros.IdRolCliente) ? (ctrl.parametros.IdClientePorDefecto == ctrl.actorComercial.Id) : (ctrl.parametros.IdProveedorPorDefecto == ctrl.actorComercial.Id);
                if (ctrl.permitirSeleccionarGrupo && !ctrl.actorComercial.esGenerico) {
                    ctrl.obtenerGruposActoresComercialesDeActorComercial().then(function (resultado_) {
                        ctrl.actorComercial.Grupo = ctrl.mostrarBusquedaGrupoActorComercial ? ctrl.gruposActorComercial.find(m => m.Id === ctrl.busqueda.GrupoActorComercial.Id) : ctrl.gruposActorComercial[0];
                        if (ctrl.busqueda.busquedaActiva)
                            ctrl.cancelarBusquedaActor();
                    }, function (error) {
                        ctrl.mensaje = "Se ha producido un error al obtener el dato:" + error;
                    });
                } else {
                    if (ctrl.busqueda.busquedaActiva)
                        ctrl.cancelarBusquedaActor();
                }
            };

            ctrl.setActorComercial = function (actorComercial) {
                ctrl.actorComercial = actorComercial;
                ctrl.actorComercialSeleccionado();
            };

            ctrl.setActorComercialPorId = function (id) {
                ctrl.cargarActorInicio = !ctrl.actorComercialPorDefecto.esValido;
                ctrl.idActorCargarInicio = id;
                if (ctrl.cargarActorInicio == false) {
                    ctrl.obtenerActorComercialPorId(id).then(function (resultado_) {
                        ctrl.actorComercialSeleccionado();
                    }, function (error) {
                        ctrl.mensaje = "Se ha producido un error al obtener el dato:" + error;
                    });
                }
            };

            ctrl.establecerActorComercial = function (actorComercial) {
                ctrl.actorComercial = actorComercial;
                ctrl.actorComercialSeleccionado();
            };

            ctrl.habilitarCreacion = function () {
                ctrl.registradorActorComercialAPI.AgregarActorComercial(ctrl.actorComercial);
            };

            ctrl.habilitarEdicion = function () {
                ctrl.registradorActorComercialAPI.EditarActorComercial(ctrl.actorComercial);
            };

            ctrl.habilitarBusqueda = function () {
                ctrl.mostrarBusquedaActorComercial = true;
                ctrl.busqueda.busquedaActiva = true;
                ctrl.actorComercial.esValido = false;
                ctrl.setActorComercial(ctrl.actorComercial);
            };

            ctrl.habilitarBusquedaGrupo = function () {
                ctrl.mostrarBusquedaGrupoActorComercial = true;
                ctrl.busqueda.busquedaActiva = true;
                ctrl.actorComercial.esValido = false;
                ctrl.setActorComercial(ctrl.actorComercial);
            };

            ctrl.cargarActoresComercialesPorBusqueda = function (query) {
                var reqStr = URL_ + "/ActorComercial/ObtenerActoresComercialesPorRolYBusqueda";
                $http.post(reqStr, { idRol: ctrl.rol.Id, cadenaBusqueda: query }).then(
                    function (response) {
                        ctrl.actoresComercialesPorBusqueda = response.data;
                    },
                    function () {
                        SweetAlert.error("Ocurrio un Problema", response.error);
                    }
                );
            }

            ctrl.seleccionarActorComercialBusqueda = function () {
                if (ctrl.mostrarBusquedaActorComercial ? ctrl.busqueda.ActorComercial : ctrl.busqueda.ActorComercialGrupo != undefined) {
                    ctrl.obtenerActorComercialPorId(ctrl.mostrarBusquedaActorComercial ? ctrl.busqueda.ActorComercial.Id : ctrl.busqueda.ActorComercialGrupo.Id);
                }
            };

            ctrl.cancelarBusqueda = function () {
                ctrl.mostrarBusquedaActorComercial = false;
                ctrl.busqueda.busquedaActiva = false;
                ctrl.actorComercial.esValido = true;
                ctrl.setActorComercial(ctrl.actorComercial);
            };

            ctrl.aceptarBusquedaCliente = function () {
                ctrl.seleccionarActorComercialBusqueda();
            };

            ctrl.cancelarBusquedaGrupo = function () {
                ctrl.mostrarBusquedaGrupoActorComercial = false;
                ctrl.busqueda.busquedaActiva = false;
                ctrl.busqueda.GrupoActorComercial = undefined;
                ctrl.busqueda.ActoresComerciales = [];
                ctrl.busqueda.ActorComercialGrupo = undefined;
                ctrl.actorComercial.esValido = true;
                ctrl.setActorComercial(ctrl.actorComercial);
            };

            ctrl.aceptarClienteGrupoClientes = function () {
                ctrl.seleccionarActorComercialBusqueda();
            };

            ctrl.cancelarBusquedaActor = function () {
                if (ctrl.mostrarBusquedaActorComercial)
                    ctrl.cancelarBusqueda();
                if (ctrl.mostrarBusquedaGrupoActorComercial)
                    ctrl.cancelarBusquedaGrupo();
            };

            ctrl.Seleccionar_NingunGrupoActorComercial = function () {
                if (ctrl.actorComercial.NingunGrupo) {
                    ctrl.actorComercial.Grupo = {};
                } else {
                    ctrl.actorComercial.Grupo = ctrl.gruposActorComercial[0];
                }
            };

            ctrl.guardarDocumentoActorComercial = function () {
                ctrl.numeroDocumentoActorComercial = ctrl.actorComercial.NumeroDocumentoIdentidad;
                ctrl.numeroDocumentoActorNegocioGuardado = true;
            };

            ctrl.verificarDocumentoActorComercial = function () {
                if (ctrl.numeroDocumentoActorNegocioGuardado) {
                    ctrl.actorComercial.NumeroDocumentoIdentidad = ctrl.numeroDocumentoActorComercial;
                }
                ctrl.numeroDocumentoActorNegocioGuardado = false;
            };

            ctrl.cargarActorRegistardo = function (actorRegistrado) {
                ctrl.actorComercial = angular.copy(actorRegistrado);
            };

            ctrl.limpiarActorComercial = function () {
                ctrl.actorComercial = {};
            };

            this.$onInit = function () {
                ctrl.api = {};
                ctrl.inicializar();
                ctrl.api.SetActorComercial = ctrl.setActorComercial;
                ctrl.api.SetActorComercialPorId = ctrl.setActorComercialPorId;
                ctrl.api.EstablecerActorPorDefecto = ctrl.establecerActorPorDefecto;
                ctrl.api.LimpiarActorComercial = ctrl.limpiarActorComercial;
            };
        }
    });
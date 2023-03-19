angular.
    module('app').
    component('registradorActorComercial', {
        templateUrl: "../Scripts/controller/actorComercial/registradorActorComercial/registradorActorComercial.html",
        bindings: {
            api: '=',
            rol: '<',
            etiquetaRol: '<',
            mascaraVisualizacionValidacion: '<',
            changed: '&'
        },

        controller: function ($q, $scope, $timeout, SweetAlert, actorComercialService, maestroService, ventaService) {
            $('select:not(.normal)').each(function () {
                $(this).select2({
                    dropdownParent: $(this).parent()
                });
            });
            $('.td-datepicker').datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true,
                todayHighlight: true,
                language: 'es'
            });
            var ctrl = this;

            ctrl.actorComercialRegistrado = function () {
                ctrl.changed({ actorRegistrado: ctrl.actorComercial });
            };

            ctrl.inicializar = function () {
                ctrl.cargaDatosRealizada = false;
                ctrl.mascaraArray = ctrl.mascaraVisualizacionValidacion.split('');
                ctrl.limpiarActorComercial();
                ctrl.etiquetaRol = ctrl.etiquetaRol == undefined ? ctrl.rol.Nombre : ctrl.etiquetaRol;
            };

            ctrl.cargarOrigenesDeDatos = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                if (ctrl.cargaDatosRealizada == false)
                    ctrl.cargarParametros().then(function () {
                        ctrl.cargarColeccionesSync().then(function () {
                            ctrl.establecerDatosPorDefecto();
                            ctrl.cargaDatosRealizada = true;
                            defered.resolve();
                        }, function (error) {
                            ctrl.mensaje = "Se ha producido un error al obtener el dato:" + error;
                            defered.reject(data);
                        });
                    }, function (error) {
                        ctrl.mensaje = "Se ha producido un error al obtener el dato:" + error;
                        defered.reject(data);
                    });
                else {
                    defered.resolve();
                }
                return promise;
            };

            ctrl.limpiarActorComercial = function () {
                ctrl.actorComercial = {
                    TipoDocumentoIdentidad: {},
                    DomicilioFiscal: {},
                    Roles: []
                };
                $timeout(function () { $('#rolEmpleado').trigger("change"); }, 100);
            };

            ctrl.cargarParametros = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                actorComercialService.obtenerParametrosParaRegistroDeActorComercial({}).success(function (data) {
                    ctrl.parametros = data.data;
                    defered.resolve();
                }).error(function (data) {
                    ctrl.messageError(data.error);
                    defered.reject(data);
                });
                return promise;
            };

            ctrl.cargarColeccionesSync = function () {
                var defered = $q.defer();
                var promiseList = [];
                promiseList.push(ctrl.obtenerTiposDePersona());
                promiseList.push(ctrl.obtenerTiposDeDocumentosIdentidad());
                promiseList.push(ctrl.obtenerUbigeoDistrito());
                promiseList.push(ctrl.obtenerNaciones());
                promiseList.push(ctrl.obtenerSexos());
                promiseList.push(ctrl.obtenerTiposDeSociedad());
                promiseList.push(ctrl.obtenerEstadosCiviles());
                if (ctrl.rol.Id == ctrl.parametros.IdRolCliente)
                    promiseList.push(ctrl.obtenerTiposDeComprobante());
                if (ctrl.rol.Id == ctrl.parametros.IdRolEmpleado)
                    promiseList.push(ctrl.obtenerRolesPersonal());
                return $q.all(promiseList).then(function (response) {
                    defered.resolve();
                }).catch(function (error) {
                    defered.reject(e);
                });
            };

            ctrl.establecerDatosPorDefecto = function () {
                ctrl.estaEditando = false;
                ctrl.longitudCaracteresTipoDocumentoIdentidad = 0;
                ctrl.establecerTipoDocumentoIdentidadPorDefecto();
                ctrl.establecerTipoPersonaPorTipoDocumentoIdentidad();
                ctrl.establecerUbigeoPorDefecto();
                ctrl.establecerPaisNacionalidadPorDefecto();
                if (ctrl.actorComercial.TipoPersona != undefined) {
                    if (ctrl.actorComercial.TipoPersona.Id == ctrl.parametros.IdTipoActorPersonaJuridica) {
                        ctrl.establecerTipoSociedadPorDefecto();

                    } else {
                        ctrl.establecerSexoPorDefecto();

                    }
                }
                ctrl.establecerEstadoCivilPorDefecto();
                if (ctrl.rol.Id === ctrl.parametros.IdRolCliente && ctrl.parametros.PermitirComprobantePorDefectoEnCliente) {
                    ctrl.establecerComprobantePredeterminadoPorDefecto();
                }
                if (ctrl.mascaraArray[9] == ctrl.parametros.MascaraObligatorio) {
                    ctrl.actorComercial.FechaNacimiento = '';
                } else {
                    ctrl.actorComercial.FechaNacimiento = ctrl.parametros.FechaActual;
                }
                $timeout(function () { $('#fechaNacimiento').trigger("change"); }, 100);
            };

            //#region Obtencion de datos
            ctrl.obtenerTiposDePersona = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                maestroService.listarTiposDeActor({}).success(function (data) {
                    ctrl.tiposDeActor = data;
                    defered.resolve();
                }).error(function (data) {
                    ctrl.messageError(data.error);
                    defered.reject(data);
                });
                return promise;
            };

            ctrl.obtenerTiposDeDocumentosIdentidad = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                maestroService.listarTiposDeDocumentosDeIdentidad({}).success(function (data) {
                    ctrl.tiposDeDocumentoDeIdentidad = data;
                    defered.resolve();
                }).error(function (data) {
                    ctrl.messageError(data.error);
                    defered.reject(data);
                });
                return promise;
            };

            ctrl.obtenerUbigeoDistrito = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                maestroService.listarUbigeoDistrito().success(function (data) {
                    ctrl.ubigeosPeru = data;
                    defered.resolve();
                }).error(function (data) {
                    ctrl.messageError(data.error);
                    defered.reject(data);
                });
                return promise;
            };

            ctrl.obtenerTiposDeComprobante = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                ventaService.obtenerTiposDeComprobanteParaClientes({}).success(function (data) {
                    ctrl.tiposDeComprobantes = data;
                    defered.resolve();
                }).error(function (data) {
                    ctrl.messageError(data.error);
                    defered.reject(data);
                });
                return promise;
            };

            ctrl.obtenerRolesPersonal = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                maestroService.obtenerRolesPersonal({}).success(function (data) {
                    ctrl.rolesPersonal = data;
                    defered.resolve();
                }).error(function (data) {
                    ctrl.messageError(data.error);
                    defered.reject(data);
                });
                return promise;
            }

            ctrl.obtenerNaciones = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                maestroService.listarNaciones({}).success(function (data) {
                    ctrl.naciones = data;
                    defered.resolve();
                }).error(function (data) {
                    ctrl.messageError(data.error);
                    defered.reject(data);
                });
                return promise;
            }

            ctrl.obtenerSexos = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                maestroService.obtenerListaSexos({}).success(function (data) {
                    ctrl.sexos = data;
                    defered.resolve();
                }).error(function (data) {
                    ctrl.messageError(data.error);
                    defered.reject(data);
                });
                return promise;
            }

            ctrl.obtenerTiposDeSociedad = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                maestroService.obtenerListaTiposDeSociedad({}).success(function (data) {
                    ctrl.tiposDeSociedad = data;
                    defered.resolve();
                }).error(function (data) {
                    ctrl.messageError(data.error);
                    defered.reject(data);
                });
                return promise;
            }

            ctrl.obtenerEstadosCiviles = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                maestroService.obtenerListaEstadosCiviles({}).success(function (data) {
                    ctrl.estadosCiviles = data;
                    defered.resolve();
                }).error(function (data) {
                    ctrl.messageError(data.error);
                    defered.reject(data);
                });
                return promise;
            }

            //#endregion

            //#region Establecer valores por defecto
            ctrl.establecerTipoDocumentoIdentidadPorDefecto = function () {
                var tipoDocumentoIdentidad = Enumerable.from(ctrl.tiposDeDocumentoDeIdentidad)
                    .where("$.Id == '" + ctrl.parametros.IdTipoTipoDocumentoSeleccionadaPorDefecto + "'").toArray()[0];
                ctrl.actorComercial.TipoDocumentoIdentidad = tipoDocumentoIdentidad !== null ? tipoDocumentoIdentidad : ctrl.tiposDeDocumentoDeIdentidad[0];
                ctrl.establecerlongitudCaracteresTipoDocumentoIdentidad();
                $timeout(function () { $('.tipoDocumento').trigger("change"); }, 100);
            };

            ctrl.establecerUbigeoPorDefecto = function () {
                var ubigeo = Enumerable.from(ctrl.ubigeosPeru)
                    .where("$.Id == '" + ctrl.parametros.IdUbigeoSeleccionadoPorDefecto + "'").toArray()[0];
                ctrl.actorComercial.DomicilioFiscal = (ctrl.actorComercial.DomicilioFiscal === null) ? {} : ctrl.actorComercial.DomicilioFiscal;
                ctrl.actorComercial.DomicilioFiscal.Ubigeo = ubigeo !== null ? ubigeo : ctrl.ubigeosPeru[0];
                $timeout(function () { $('.ubigeo').trigger("change"); }, 100);
            };

            ctrl.establecerComprobantePredeterminadoPorDefecto = function () {
                var comprobante = Enumerable.from(ctrl.tiposDeComprobantes)
                    .where("$.Id == '" + ctrl.parametros.IdComprobantePredeterminadoPorDefecto + "'").toArray()[0];
                ctrl.actorComercial.ComprobantePredeterminado = comprobante !== null ? comprobante : ctrl.tiposDeComprobantes[0];
                $timeout(function () { $('.comprobantePreteterminado').trigger("change"); }, 100);
            };

            ctrl.establecerPaisNacionalidadPorDefecto = function () {
                var pais = Enumerable.from(ctrl.naciones)
                    .where("$.Id == '" + ctrl.parametros.IdNacionPeru + "'").toArray()[0];
                ctrl.actorComercial.Nacionalidad = pais !== null ? pais : ctrl.naciones[0];
                ctrl.actorComercial.DomicilioFiscal.Pais = pais !== null ? pais : ctrl.naciones[0];
                $timeout(function () { $('.nacionalidad').trigger("change"); }, 100);
                $timeout(function () { $('.pais').trigger("change"); }, 100);
            };

            ctrl.establecerSexoPorDefecto = function () {
                ctrl.actorComercial.ClaseActor = ctrl.sexos[0];
                $timeout(function () { $('.sexo').trigger("change"); }, 100);
            };

            ctrl.establecerTipoSociedadPorDefecto = function () {
                ctrl.actorComercial.ClaseActor = ctrl.tiposDeSociedad[0];
                $timeout(function () { $('.tipoSociedad').trigger("change"); }, 100);
            };

            ctrl.establecerEstadoCivilPorDefecto = function () {
                ctrl.actorComercial.EstadoLegal = ctrl.estadosCiviles[0];
                $timeout(function () { $('.estadoCivil').trigger("change"); }, 100);
            };
            //#endregion

            ctrl.establecerComprobantePredeterminadoAlEditar = function (id) {
                var comprobante = Enumerable.from(ctrl.tiposDeComprobantes).where("$.Id === '" + id + "'").toArray()[0];
                ctrl.actorComercial.ComprobantePredeterminado = comprobante;
                $timeout(function () { $('.comprobantePreteterminado').trigger("change"); }, 100);
            };

            ctrl.establecerTipoPersonaPorTipoDocumentoIdentidad = function () {
                if (ctrl.actorComercial.TipoDocumentoIdentidad.Id === ctrl.parametros.IdTipoDocumentoIdentidadDni) {
                    ctrl.seleccionarTipoPersona(ctrl.parametros.IdTipoActorPersonaNatural);
                } else if (ctrl.actorComercial.TipoDocumentoIdentidad.Id === ctrl.parametros.IdTipoDocumentoIdentidadRuc) {
                    if (ctrl.actorComercial.NumeroDocumentoIdentidad) {
                        let caracteresConQueEmpieza = ctrl.actorComercial.NumeroDocumentoIdentidad.substr(0, 2);
                        if (caracteresConQueEmpieza === "10") {
                            ctrl.seleccionarTipoPersona(ctrl.parametros.IdTipoActorPersonaNatural);
                        } else if (caracteresConQueEmpieza === "20") {
                            ctrl.seleccionarTipoPersona(ctrl.parametros.IdTipoActorPersonaJuridica);
                        } else {
                            SweetAlert.warning("Advertencia", "Verificar el numero de documento, algo esta mal");
                        }
                    } else {
                        ctrl.seleccionarTipoPersona(ctrl.parametros.IdTipoActorPersonaJuridica);
                    }
                } else {
                    ctrl.seleccionarTipoPersona(ctrl.parametros.IdTipoActorPersonaNatural);
                }
            };

            ctrl.seleccionarTipoPersona = function (idTipoPersona) {
                var tipoPersona = Enumerable.from(ctrl.tiposDeActor).where("$.Id == '" + idTipoPersona + "'").toArray()[0];
                ctrl.actorComercial.TipoPersona = tipoPersona;
                ctrl.mostrarRazonSocial = (ctrl.parametros.IdTipoActorPersonaJuridica == idTipoPersona) ? true : false;
            };

            ctrl.establecerlongitudCaracteresTipoDocumentoIdentidad = function () {
                ctrl.longitudCaracteresTipoDocumentoIdentidad = ctrl.actorComercial.TipoDocumentoIdentidad == undefined ? 50 : (ctrl.actorComercial.TipoDocumentoIdentidad.Id === ctrl.parametros.IdTipoDocumentoIdentidadDni ? 8 : ctrl.actorComercial.TipoDocumentoIdentidad.Id === ctrl.parametros.IdTipoDocumentoIdentidadRuc ? ctrl.longitudCaracteresTipoDocumentoIdentidad = 11 : 50);
            };

            ctrl.removeSpacesInPaste = function (e) {
                var item = e.clipboardData.items[0];
                item.getAsString(function (data) {
                    ctrl.actorComercial.NumeroDocumentoIdentidad = data.split(' ').join('');
                });
            };

            ctrl.obtenerActorComercial = function () {
                let idActorComercial = ctrl.actorComercial.Id;
                if (ctrl.actorComercial.NumeroDocumentoIdentidad !== null && ctrl.actorComercial.TipoDocumentoIdentidad !== null) {
                    actorComercialService.resolverObtenerActorComercial({ idRol: ctrl.rol.Id, idTipoDocumento: ctrl.actorComercial.TipoDocumentoIdentidad.Id, numeroDocumento: ctrl.actorComercial.NumeroDocumentoIdentidad }).success(function (data) {
                        if (data.respuesta == 1) {
                            ctrl.actorComercial = data.actorComercial;
                            if (!ctrl.estaEditando)
                                SweetAlert.warning("Advertencia", "Ya existe un actor comercial con el número de documento ingresado, Se va a actualizar sus datos");
                        }
                        if (data.respuesta == 2) {
                            ctrl.actorComercial = data.actorComercial;
                            if (ctrl.estaEditando) { ctrl.actorComercial.Id = idActorComercial; }
                            SweetAlert.warning("Advertencia", "Ya existe un registro con el número de documento ingresado y seran usados para el nuevo actor comercial");
                        }
                        if (data.respuesta == 3) {
                            SweetAlert.success("Correcto", "Número de documento valido");
                            ctrl.actorComercial = data.actorComercial;
                            ctrl.establecerComprobantePredeterminadoPorDefecto();
                            ctrl.establecerTipoPersonaPorTipoDocumentoIdentidad();
                            if (ctrl.estaEditando) { ctrl.actorComercial.Id = idActorComercial; }
                        }
                        if (data.respuesta == 5) {
                            ctrl.establecerTipoPersonaPorTipoDocumentoIdentidad();
                            SweetAlert.warning("Advertencia", "El numero de documento ingresado no es correcto");
                        }
                        ctrl.actorComercial.FechaNacimiento = ctrl.actorComercial.FechaNacimientoString;
                        ctrl.verificarRegistradorActorComercial();
                    }).error(function (data) {
                        ctrl.establecerTipoPersonaPorTipoDocumentoIdentidad();
                        SweetAlert.warning("Advertencia", data.error);
                    });
                }
            };

            ctrl.obtenerUbigeo = function (id) {
                var ubigeo = Enumerable.from(ctrl.ubigeosPeru).where("$.Id == '" + id + "'").toArray()[0];
                return ubigeo !== null ? ubigeo : Enumerable.from(ctrl.ubigeosPeru).where("$.Id == '" + ctrl.parametros.IdUbigeoSeleccionadoPorDefecto + "'").toArray()[0];
            };

            ctrl.verificarUbigeoSeleccionado = function () {
                if (ctrl.actorComercial.DomicilioFiscal != undefined) {
                    if (ctrl.actorComercial.DomicilioFiscal.Ubigeo.Id === ctrl.parametros.IdUbigeoNoEspecificado) {
                        ctrl.ubigeoSeleccionadoNoEspecificado = true;
                    } else {
                        ctrl.ubigeoSeleccionadoNoEspecificado = false;
                    }
                }
            };

            ctrl.establecerUbigeoYDetalle = function () {
                if (ctrl.actorComercial.DomicilioFiscal.Pais != undefined || ctrl.actorComercial.DomicilioFiscal.Pais != null) {
                    if (ctrl.actorComercial.DomicilioFiscal.Pais.Id != ctrl.parametros.IdNacionPeru) {
                        ctrl.actorComercial.DomicilioFiscal.Ubigeo = ctrl.obtenerUbigeo(ctrl.parametros.IdUbigeoNoEspecificado);
                        ctrl.actorComercial.DomicilioFiscal.Detalle = '-';
                    } else {
                        ctrl.actorComercial.DomicilioFiscal.Ubigeo = ctrl.obtenerUbigeo(ctrl.parametros.IdUbigeoSeleccionadoPorDefecto);
                    }
                }
                $timeout(function () { $('.ubigeo').trigger("change"); }, 100);
            };

            ctrl.guardar = function () {
                actorComercialService.guardarActorComercial({ idRol: ctrl.rol.Id, actorComercial: ctrl.actorComercial }).success(function (data) {
                    SweetAlert.success("Correcto", data.result_description);
                    ctrl.actorComercial = data.information;
                    ctrl.actorComercialRegistrado();
                    $('#modal-registro-actor-comercial').modal('hide');
                }).error(function (data) {
                    SweetAlert.error("Ocurrio un problema", data.error);
                });
            };

            ctrl.verificarRegistradorActorComercial = function () {
                ctrl.inconsitencias = 0;
                if (ctrl.actorComercial.TipoDocumentoIdentidad == undefined && ctrl.mascaraArray[0] == 2)
                    ctrl.inconsitencias++;
                if ((ctrl.actorComercial.NumeroDocumentoIdentidad == undefined || ctrl.actorComercial.NumeroDocumentoIdentidad == '') && ctrl.mascaraArray[1] == 2)
                    ctrl.inconsitencias++;
                if (ctrl.actorComercial.TipoDocumentoIdentidad != undefined || ctrl.actorComercial.TipoDocumentoIdentidad != null) {
                    if (ctrl.actorComercial.TipoDocumentoIdentidad.Id == ctrl.parametros.IdTipoDocumentoIdentidadRuc) {
                        if (ctrl.actorComercial.NombreORazonSocial == undefined || ctrl.actorComercial.NombreORazonSocial == '' && ctrl.mascaraArray[2] == 2)
                            ctrl.inconsitencias++;
                        if (ctrl.actorComercial.ClaseActor == undefined && ctrl.mascaraArray[3] == 2)
                            ctrl.inconsitencias++;
                    } else {
                        if ((ctrl.actorComercial.ApellidoPaterno == undefined || ctrl.actorComercial.ApellidoPaterno == '') && ctrl.mascaraArray[4] == 2)
                            ctrl.inconsitencias++;
                        if ((ctrl.actorComercial.ApellidoMaterno == undefined || ctrl.actorComercial.ApellidoMaterno == '') && ctrl.mascaraArray[5] == 2)
                            ctrl.inconsitencias++;
                        if ((ctrl.actorComercial.Nombres == undefined || ctrl.actorComercial.Nombres == '') && ctrl.mascaraArray[6] == 2)
                            ctrl.inconsitencias++;
                    }
                }
                if ((ctrl.actorComercial.NombreComercial == undefined || ctrl.actorComercial.NombreComercial == '') && ctrl.mascaraArray[7] == 2)
                    ctrl.inconsitencias++;
                if (ctrl.parametros != undefined) {
                    if (ctrl.actorComercial.TipoPersona == undefined) {
                        ctrl.inconsitencias++;
                    } else {
                        if (ctrl.actorComercial.TipoPersona.Id == ctrl.parametros.IdTipoActorPersonaNatural) {
                            if (ctrl.actorComercial.Nacionalidad == undefined && ctrl.mascaraArray[8] == 2)
                                ctrl.inconsitencias++;
                            if ((ctrl.actorComercial.FechaNacimiento == undefined || ctrl.actorComercial.FechaNacimiento == '') && ctrl.mascaraArray[9] == 2)
                                ctrl.inconsitencias++;
                            if (ctrl.actorComercial.ClaseActor == undefined && ctrl.mascaraArray[10] == 2)
                                ctrl.inconsitencias++;
                            if (ctrl.actorComercial.EstadoLegal == undefined && ctrl.mascaraArray[11] == 2)
                                ctrl.inconsitencias++;
                        } else if (ctrl.actorComercial.TipoPersona.Id == ctrl.parametros.IdTipoActorPersonaJuridica) {
                            if ((ctrl.actorComercial.NombreCorto == undefined || ctrl.actorComercial.NombreCorto == '') && ctrl.mascaraArray[12] == 2)
                                ctrl.inconsitencias++;
                        }
                    }
                }
                if ((ctrl.actorComercial.Correo == undefined || ctrl.actorComercial.Correo == '') && ctrl.mascaraArray[13] == 2)
                    ctrl.inconsitencias++;
                if ((ctrl.actorComercial.Telefono == undefined || ctrl.actorComercial.Telefono == '') && ctrl.mascaraArray[14] == 2)
                    ctrl.inconsitencias++;
                if ((ctrl.actorComercial.Codigo == undefined || ctrl.actorComercial.Codigo == '') && ctrl.mascaraArray[15] == 2)
                    ctrl.inconsitencias++;
                if (ctrl.actorComercial.Roles == undefined && ctrl.mascaraArray[16] == 2)
                    ctrl.inconsitencias++;
                if (ctrl.actorComercial.DomicilioFiscal.Pais == undefined && ctrl.mascaraArray[17] == 2)
                    ctrl.inconsitencias++;
                if (ctrl.actorComercial.DomicilioFiscal.Ubigeo == undefined && ctrl.mascaraArray[19] == 2)
                    ctrl.inconsitencias++;
                if ((ctrl.actorComercial.DomicilioFiscal.Detalle == undefined || ctrl.actorComercial.DomicilioFiscal.Detalle == '') && ctrl.mascaraArray[19] == 2)
                    ctrl.inconsitencias++;
                if (ctrl.actorComercial.ComprobantePredeterminado == undefined && ctrl.mascaraArray[20] == 2)
                    ctrl.inconsitencias++;
            };

            ctrl.editarActorComercial = function (actorComercial) {
                ctrl.actorComercialTemporal = angular.copy(actorComercial);
                ctrl.actorComercial = angular.copy(actorComercial);
                ctrl.cargarOrigenesDeDatos().then(function () {
                    ctrl.estaEditando = true;
                    ctrl.actorComercial = angular.copy(ctrl.actorComercialTemporal);
                    ctrl.actorComercial.FechaNacimiento = ctrl.actorComercial.FechaNacimientoString;
                    ctrl.establecerTipoPersonaPorTipoDocumentoIdentidad();
                    ctrl.verificarRegistradorActorComercial();
                    $timeout(function () { $('#rolEmpleado').trigger("change"); }, 100);
                    $timeout(function () { $('.tipoDocumento').trigger("change"); }, 100);
                    $timeout(function () { $('.estadoCivil').trigger("change"); }, 100);
                    $timeout(function () { $('.sexo').trigger("change"); }, 100);
                    $timeout(function () { $('.tipoSociedad').trigger("change"); }, 100);
                    $timeout(function () { $('.pais').trigger("change"); }, 100);
                    $timeout(function () { $('.nacionalidad').trigger("change"); }, 100);
                    $timeout(function () { $('.ubigeo').trigger("change"); }, 100);
                    $timeout(function () { $('.comprobantePreteterminado').trigger("change"); }, 100);
                }, function (error) {
                    $scope.mensaje = error;
                });
            };

            ctrl.agregarActorComercial = function (actorComercial) {
                ctrl.actorComercialTemporal = angular.copy(actorComercial);
                ctrl.cargarOrigenesDeDatos().then(function () {
                    ctrl.limpiarActorComercial();
                    ctrl.establecerDatosPorDefecto();
                    ctrl.verificarRegistradorActorComercial();
                }, function (error) {
                    $scope.mensaje = error;
                });
            };

            ctrl.cerrar = function () {
                ctrl.actorComercial = ctrl.actorComercialTemporal;
                ctrl.actorComercialRegistrado();
            };

            this.$onInit = function () {
                ctrl.api = {};
                ctrl.inicializar();
                ctrl.api.EditarActorComercial = ctrl.editarActorComercial;
                ctrl.api.AgregarActorComercial = ctrl.agregarActorComercial;
            };
        }
    });



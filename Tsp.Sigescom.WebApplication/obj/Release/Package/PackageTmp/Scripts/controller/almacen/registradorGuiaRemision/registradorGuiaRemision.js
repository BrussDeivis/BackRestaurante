angular.
    module('app').
    component('registradorGuiaRemision', {
        templateUrl: "../Scripts/controller/almacen/registradorGuiaRemision/registradorGuiaRemision.html",
        bindings: {
            api: '=',
            guiaRemision: '=',
            permitirSeleccionConcepto: '<',
            permitirDocumentoReferencia: '<',
            hayInconsitenciasGuiaRemision: '=',
            vinculadoVenta: '<',
            venta: '<',
            vinculadoOrdenAlmacen: '<',
            ordenAlmacen: '<',
            verHistorialOrdenAlmacen: '&',
            permitirMostrarStockActual: '<',
            registroDesdeOrdenAlmacen: '<',
            permitirRegistroIngresoSalidaActual: '<',
            nombreEtiquetaTercero: '<',
            seleccionadoPorIngresar: '<',
            permitirNotaAlmacen: '<',
            iniciadoPendiente: '<',//Indica que el componente se inicia y tiene una cargad e datos pendiente, 
        },

        controller: function ($q, $scope, $timeout, SweetAlert, actorComercialService, maestroService, ventaService, almacenService) {
            $('.td-datepicker').datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true,
                todayHighlight: true,
                language: 'es'
            });
            var ctrl = this;

            ctrl.cargarHistorialOrdenAlmacen = function () {
                ctrl.verHistorialOrdenAlmacen({ id: ctrl.ordenAlmacen.IdOrdenDeAlmacen });
            };

            ctrl.inicializar = function () {
                ctrl.limpiarGuiaRemision();
                ctrl.cargarParametros().then(function () {
                    ctrl.iniciarComponentes();
                }, function (error) {
                    SweetAlert.error("Ocurrio un Problema", error);
                });
            };

            ctrl.cargarParametros = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                almacenService.obtenerParametrosParaRegistradorGuiaRemision({}).success(function (data) {
                    ctrl.parametros = data.data;
                    defered.resolve();
                }).error(function (data) {
                    ctrl.messageError(data.error);
                    defered.reject(data);
                });
                return promise;
            };

            ctrl.iniciarComponentes = function () {
                ctrl.rolTransportista = { Id: ctrl.parametros.IdRolProveedor, Nombre: 'PROVEEDOR' };
                ctrl.rolConductor = { Id: ctrl.parametros.IdRolProveedor, Nombre: 'PROVEEDOR' };
                ctrl.rolTercero = { Id: ctrl.parametros.IdRolCliente, Nombre: 'CLIENTE' };
                ctrl.etiquetaTercero = 'DESTINATARIO';
                ctrl.inicializacionRealizada = true;
                //ctrl.inicializarRegistroGuiaRemision();//todo.
            };

            ctrl.inicializarRegistroGuiaRemision = function () {
                ctrl.cargarColeccionesSync().then(function () {
                    ctrl.establecerDatosPorDefecto();
                    if (ctrl.vinculadoVenta) {
                        ctrl.cargarDatosDesdeVenta();
                    }
                    if (ctrl.vinculadoOrdenAlmacen) {
                        ctrl.cargarDatosDesdeOrdenAlmacen();
                    }
                }, function (error) {
                    SweetAlert.error("Ocurrio un Problema", error);
                });
            };

            ctrl.inicioRealizado = function () {
                ctrl.inicializarRegistroGuiaRemision();
            };

            ctrl.limpiarGuiaRemision = function () {
                ctrl.guiaRemision = {
                    Detalles: [], Transportista: {}, Conductor: {}
                };
                ctrl.detalle = {};
            }

            ctrl.nuevoRegistroGuiaRemision = function () {
                ctrl.limpiarGuiaRemision();
                ctrl.selectorTerceroAPI.EstablecerActorPorDefecto();
                ctrl.selectorTransportistaAPI.EstablecerActorPorDefecto();
                ctrl.selectorConductorAPI.EstablecerActorPorDefecto();
                ctrl.inicializarRegistroGuiaRemision();
            }

            ctrl.cargarColeccionesSync = function () {
                var defered = $q.defer();
                var promiseList = [];
                promiseList.push(ctrl.obtenerTiposDeComprobanteGuiaRemision());
                promiseList.push(ctrl.obtenerModalidadesTraslado());
                promiseList.push(ctrl.obtenerMotivosTraslado());
                promiseList.push(ctrl.obtenerUbigeoDistrito());
                return $q.all(promiseList).then(function () {
                    defered.resolve();
                }).catch(function (error) {
                    defered.reject(e);
                });
            };

            ctrl.obtenerTiposDeComprobanteGuiaRemision = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                if (ctrl.permitirNotaAlmacen) {
                    almacenService.obtenerTiposDeComprobanteParaGuiaRemisionNotaAlmacen({}).success(function (data) {
                        ctrl.comprobantesDeGuiaRemision = data;
                        defered.resolve();
                    }).error(function (data) {
                        SweetAlert.error2(data);
                        defered.reject(data);
                    });
                } else {
                    almacenService.obtenerTiposDeComprobanteParaGuiaRemision({}).success(function (data) {
                        ctrl.comprobantesDeGuiaRemision = data;
                        defered.resolve();
                    }).error(function (data) {
                        SweetAlert.error2(data);
                        defered.reject(data);
                    });
                }
                return promise;
            }

            ctrl.cargarSeries = function (tipoComprobante) {
                if (tipoComprobante != null) {
                    ctrl.series = angular.copy(tipoComprobante.Series);
                }
            }

            ctrl.obtenerModalidadesTraslado = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                maestroService.obtenerModalidadesTraslado().success(function (data) {
                    ctrl.modalidadesTraslado = data;
                    defered.resolve();
                }).error(function (data) {
                    SweetAlert.error2(data);
                    defered.reject(data);
                });
                return promise;
            }

            ctrl.obtenerMotivosTraslado = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                maestroService.obtenerMotivosTraslado().success(function (data) {
                    ctrl.motivosTraslado = data;
                    defered.resolve();
                }).error(function (data) {
                    SweetAlert.error2(data);
                    defered.reject(data);
                });
                return promise;
            }

            ctrl.obtenerUbigeoDistrito = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                maestroService.listarUbigeoDistrito().success(function (data) {
                    ctrl.ubigeosPeru = data;
                    defered.resolve();
                }).error(function (data) {
                    SweetAlert.error2(data);
                    defered.reject(data);
                });
                return promise;
            }

            ctrl.establecerDatosPorDefecto = function () {
                ctrl.guiaRemision.Detalles = [];
                ctrl.guiaRemision.Observacion = 'NINGUNO';
                ctrl.guiaRemision.DireccionOrigen = ctrl.parametros.DireccionSede;
                ctrl.establecerModalidadTrasladoPorDefecto();
                if (!ctrl.vinculadoOrdenAlmacen) {
                    ctrl.establecerMotivoTrasladoPorDefecto();
                }
                ctrl.establecerUbigeoOrigenPorDefecto();
                ctrl.establecerUbigeoDestinoPorDefecto();
                ctrl.establecerTipoDeComprobantePorDefecto();
            }

            ctrl.establecerModalidadTrasladoPorDefecto = function () {
                var modalidadTrasladoPorDefecto = Enumerable.from(ctrl.modalidadesTraslado)
                    .where("$.Id == '" + ctrl.parametros.IdModalidadTrasladoPorDefecto + "'").toArray()[0];
                ctrl.guiaRemision.ModalidadTransporte = modalidadTrasladoPorDefecto != null ? modalidadTrasladoPorDefecto : ctrl.modalidadesTraslado[0];
                $timeout(function () { $('#modalidad').trigger("change"); }, 100);
            }

            ctrl.establecerMotivoTrasladoPorDefecto = function () {
                var motivoTrasladoPorDefecto = Enumerable.from(ctrl.motivosTraslado)
                    .where("$.Id == '" + ctrl.parametros.IdMotivoTrasladoPorVenta + "'").toArray()[0];
                ctrl.guiaRemision.MotivoTraslado = motivoTrasladoPorDefecto != null ? motivoTrasladoPorDefecto : ctrl.motivosTraslado[0];
                ctrl.cambioMotivoTraslado();
                $timeout(function () { $('#motivo').trigger("change"); }, 100);
            }

            ctrl.establecerTipoDeComprobantePorDefecto = function () {
                var tipoDeComprobantePorDefecto = Enumerable.from(ctrl.comprobantesDeGuiaRemision)
                    .where("$.Id == '" + ctrl.parametros.IdTipoDeComprobantePorDefecto + "'").toArray()[0];
                ctrl.guiaRemision.TipoDeComprobante = tipoDeComprobantePorDefecto != null ? tipoDeComprobantePorDefecto : ctrl.comprobantesDeGuiaRemision[0];
                $timeout(function () { $('#documento').trigger("change"); }, 100);
                ctrl.cargarSeries(tipoDeComprobantePorDefecto);
            }

            ctrl.establecerUbigeoOrigenPorDefecto = function () {
                var ubigeo = Enumerable.from(ctrl.ubigeosPeru)
                    .where("$.Id == '" + ctrl.parametros.IdUbigeoSede + "'").toArray()[0];
                ctrl.guiaRemision.UbigeoOrigen = ubigeo != null ? ubigeo : ctrl.ubigeosPeru[0];
                $timeout(function () { $('#ubigeoOrigen').trigger("change"); }, 100);
            }

            ctrl.establecerUbigeoDestinoPorDefecto = function () {
                var ubigeo = Enumerable.from(ctrl.ubigeosPeru)
                    .where("$.Id == '" + ctrl.parametros.IdUbigeoSede + "'").toArray()[0];
                ctrl.guiaRemision.UbigeoDestino = ubigeo != null ? ubigeo : ctrl.ubigeosPeru[0];
                $timeout(function () { $('#ubigeoDestino').trigger("change"); }, 100);
            }

            ctrl.seleccionarUbigeoOrigenPorDefecto = function (idUbigeo) {
                if (ctrl.ubigeosPeru != undefined) {
                    var ubigeo = Enumerable.from(ctrl.ubigeosPeru).where("$.Id == '" + idUbigeo + "'").toArray()[0];
                    ctrl.guiaRemision.UbigeoOrigen = ubigeo != null ? ubigeo : ctrl.ubigeosPeru[0];
                    $timeout(function () { $('#ubigeoOrigen').trigger("change"); }, 100);
                }
            }

            ctrl.seleccionarUbigeoDestinoPorDefecto = function (idUbigeo) {
                if (ctrl.ubigeosPeru != undefined) {
                    var ubigeo = Enumerable.from(ctrl.ubigeosPeru).where("$.Id == '" + idUbigeo + "'").toArray()[0];
                    ctrl.guiaRemision.UbigeoDestino = ubigeo != null ? ubigeo : ctrl.ubigeosPeru[0];
                    $timeout(function () { $('#ubigeoDestino').trigger("change"); }, 100);
                }
            }

            ctrl.seleccionConcepto = function (conceptoComercial) {
                ctrl.detalle.Producto = angular.copy(conceptoComercial);
                ctrl.agregarDetalle();
            }

            ctrl.agregarDetalle = function () {
                var validar = false;
                var index = 0;
                if (ctrl.guiaRemision.Detalles.length > 0) {
                    for (var i = 0; i < ctrl.guiaRemision.Detalles.length; i++) {
                        if (ctrl.detalle.Producto.Id == ctrl.guiaRemision.Detalles[i].IdProducto) {
                            validar = true;
                            index = i;
                            break;
                        }
                    }
                }
                if (validar) {
                    ctrl.guiaRemision.Detalles[index].IngresoSalidaActual++;
                    ctrl.detalle = {};
                }
                else {
                    ctrl.detalle.IngresoSalidaActual = 1;
                    ctrl.detalle.IdProducto = ctrl.detalle.Producto.Id;
                    ctrl.detalle.Descripcion = ctrl.detalle.Producto.Nombre;
                    //Agrega a la tabla de detalles de guia remision
                    ctrl.guiaRemision.Detalles.unshift(ctrl.detalle);
                    ctrl.detalle = {};
                }
                ctrl.validarGuiaRemision();
            }

            ctrl.quitarCantidad = function (detalles, index) {
                detalles[index].IngresoSalidaActual--;
                if (detalles[index].IngresoSalidaActual == 0) {
                    ctrl.guiaRemision.Detalles.splice(index, 1);
                }
            }

            ctrl.quitarDetalle = function (index) {
                ctrl.guiaRemision.Detalles.splice(index, 1);
                ctrl.validarGuiaRemision();
            };

            ctrl.formatoDecimalCantidad = function (event) {
                let valor = event.target.value;
                event.target.value = parseFloat(valor).toFixed(ctrl.parametros.NumeroDecimalesEnCantidad);
            }

            ctrl.formatoDosDecimales = function (event) {
                let valor = event.target.value;
                event.target.value = parseFloat(valor == '' ? 0 : valor).toFixed(2);
            }

            ctrl.cambioMotivoTraslado = function () {
                if (ctrl.guiaRemision.MotivoTraslado.Id != ctrl.parametros.IdMotivoTrasladoOtros) {
                    ctrl.guiaRemision.DescripcionMotivo = '';
                }
                if (ctrl.guiaRemision.MotivoTraslado.Id == ctrl.parametros.IdMotivoTrasladoPorCompra || ctrl.guiaRemision.MotivoTraslado.Id == ctrl.parametros.IdMotivoTrasladoPorImportacion) {
                    ctrl.etiquetaTercero = 'REMITENTE';
                } else {
                    ctrl.etiquetaTercero = 'DESTINATARIO';
                }
            };

            ctrl.inicioRealizadoTercero = function () {
                if (ctrl.iniciadoPendiente) {
                    ctrl.terceroRealizado = true;
                    if (ctrl.terceroRealizado && ctrl.tranportistaRealizado) {
                        ctrl.inicioRealizado();
                    }
                }
            };

            ctrl.cambioTercero = function (actorComercial) {
                ctrl.guiaRemision.Tercero = actorComercial;
                if (ctrl.guiaRemision.Tercero != undefined)
                    if (ctrl.registroDesdeOrdenAlmacen) {
                        if (ctrl.seleccionadoPorIngresar == true) {
                            ctrl.guiaRemision.DireccionOrigen = ctrl.guiaRemision.Tercero.DomicilioFiscal.Detalle;
                            ctrl.seleccionarUbigeoOrigenPorDefecto(ctrl.guiaRemision.Tercero.DomicilioFiscal.Ubigeo.Id);
                        } else {
                            ctrl.guiaRemision.DireccionDestino = ctrl.guiaRemision.Tercero.DomicilioFiscal.Detalle;
                            ctrl.seleccionarUbigeoDestinoPorDefecto(ctrl.guiaRemision.Tercero.DomicilioFiscal.Ubigeo.Id);
                        }
                    } else {
                        ctrl.guiaRemision.DireccionDestino = ctrl.guiaRemision.Tercero.DomicilioFiscal.Detalle;
                    }
                ctrl.validarGuiaRemision();
            };

            ctrl.inicioRealizadoTranportista = function () {
                if (ctrl.iniciadoPendiente) {
                    ctrl.tranportistaRealizado = true;
                    if (ctrl.terceroRealizado && ctrl.tranportistaRealizado) {
                        ctrl.inicioRealizado();
                    }
                }
            };

            ctrl.cambioTransportista = function (actorComercial) {
                ctrl.guiaRemision.Transportista.Transportista = actorComercial;
                ctrl.validarGuiaRemision();
            };

            ctrl.cambioConductor = function (actorComercial) {
                ctrl.guiaRemision.Conductor.Conductor = actorComercial;
                ctrl.validarGuiaRemision();
            };

            ctrl.validarGuiaRemision = function () {
                ctrl.inconsistenciasGuiaRemision = [];
                if (ctrl.guiaRemision.Tercero == undefined || ctrl.guiaRemision.Tercero.esGenerico == true) {
                    ctrl.inconsistenciasGuiaRemision.push("Es necesario identificar el destinatario/remitente. ");
                } else {
                    if (ctrl.guiaRemision.Tercero.esGenerico) {
                        ctrl.inconsistenciasGuiaRemision.push("Es necesario ingresar la fecha de inicio de traslado.");
                    }
                }
                if (ctrl.guiaRemision.TipoDeComprobante == undefined) {
                    ctrl.inconsistenciasGuiaRemision.push("Es necesario seleccionar un documento.");
                } else {
                    if (ctrl.guiaRemision.TipoDeComprobante.EsPropio == false) {
                        if (ctrl.guiaRemision.TipoDeComprobante.SerieIngresada == '' || ctrl.guiaRemision.TipoDeComprobante.SerieIngresada == null) {
                            ctrl.inconsistenciasGuiaRemision.push("Es necesario ingresar la serie de comprobante.");
                        } if (ctrl.guiaRemision.TipoDeComprobante.NumeroIngresado == '' || ctrl.guiaRemision.TipoDeComprobante.NumeroIngresado == null) {
                            ctrl.inconsistenciasGuiaRemision.push("Es necesario ingresar el numero de comprobante.");
                        }
                    }
                }
                if (ctrl.guiaRemision.FechaInicioTraslado == '' || ctrl.guiaRemision.FechaInicioTraslado == undefined) {
                    ctrl.inconsistenciasGuiaRemision.push("Es necesario ingresar la fecha de inicio de traslado.");
                }
                if (ctrl.guiaRemision.PesoBrutoTotal == '' || ctrl.guiaRemision.PesoBrutoTotal == undefined) {
                    ctrl.inconsistenciasGuiaRemision.push("Es necesario ingresar el peso bruto total.");
                } else {
                    if (parseFloat(ctrl.guiaRemision.PesoBrutoTotal) == 0) {
                        ctrl.inconsistenciasGuiaRemision.push("Es necesario que el peso bruto total sea mayor a 0.");
                    }
                }
                if (ctrl.guiaRemision.NumeroBultos == '' || ctrl.guiaRemision.NumeroBultos == undefined) {
                    ctrl.inconsistenciasGuiaRemision.push("Es necesario ingresar el número de bultos.");
                } else {
                    if (parseFloat(ctrl.guiaRemision.NumeroBultos) == 0) {
                        ctrl.inconsistenciasGuiaRemision.push("Es necesario que el número de bultos sea mayor a 0.");
                    }
                }
                if (ctrl.guiaRemision.ModalidadTransporte != undefined) {
                    if (ctrl.guiaRemision.ModalidadTransporte.Id == ctrl.parametros.IdModalidadTrasladoPublico) {
                        if (ctrl.guiaRemision.Transportista.Transportista == undefined) {
                            ctrl.inconsistenciasGuiaRemision.push("Es necesario ingresar el transportista.");
                        } else {
                            if (ctrl.guiaRemision.Transportista.Transportista.esValido == false) {
                                ctrl.inconsistenciasGuiaRemision.push("Es necesario que el transportista sea valido.");
                            }
                            if (ctrl.guiaRemision.Transportista.Transportista.TipoDocumentoIdentidad.Id != ctrl.parametros.IdTipoDocumentoIdentidadRuc) {
                                ctrl.inconsistenciasGuiaRemision.push("Es necesario que el transportista tenga ruc.");
                            }
                        }
                    }
                    if (ctrl.guiaRemision.ModalidadTransporte.Id == ctrl.parametros.IdModalidadTrasladoPrivado) {

                        if (ctrl.guiaRemision.Transportista.Placa == '' || ctrl.guiaRemision.Transportista.Placa == undefined) {
                            ctrl.inconsistenciasGuiaRemision.push("Es necesario ingresar la placa del vehiculo.");
                        } else {
                            if (ctrl.guiaRemision.Transportista.Placa.length < 6 || ctrl.guiaRemision.Transportista.Placa.length > 8) {
                                ctrl.inconsistenciasGuiaRemision.push("Es necesario ingresar la placa del vehiculo correctamente.");
                            }
                            //if (parseFloat(ctrl.guiaRemision.Transportista.Placa) == 0) {
                            //    ctrl.inconsistenciasGuiaRemision.push("Es necesario que la placa del vehiculo sea mayor a 0.");
                            //}
                        }
                        if (ctrl.guiaRemision.Conductor.Conductor == undefined) {
                            ctrl.inconsistenciasGuiaRemision.push("Es necesario ingresar el conductor.");
                        } else {
                            if (ctrl.guiaRemision.Conductor.Conductor.esValido == false) {
                                ctrl.inconsistenciasGuiaRemision.push("Es necesario que el conductor sea valido.");
                            }
                            if (ctrl.guiaRemision.Conductor.Conductor.TipoDocumentoIdentidad.Id != ctrl.parametros.IdTipoDocumentoIdentidadDni) {
                                ctrl.inconsistenciasGuiaRemision.push("Es necesario que el conductor sea con dni.");
                            }
                            if (ctrl.guiaRemision.Conductor.Conductor.Id == ctrl.parametros.IdProveedorGenerico) {
                                ctrl.inconsistenciasGuiaRemision.push("Es necesario que el conductor sea diferente al generico.");
                            }
                        }
                        if (ctrl.guiaRemision.Conductor.NumeroLicencia == '' || ctrl.guiaRemision.Conductor.NumeroLicencia == undefined) {
                            ctrl.inconsistenciasGuiaRemision.push("Es necesario ingresar el numero de licencia del conductor.");
                        } else {
                            if (ctrl.guiaRemision.Conductor.NumeroLicencia.length < 9 || ctrl.guiaRemision.Conductor.NumeroLicencia.length > 10) {
                                ctrl.inconsistenciasGuiaRemision.push("Es necesario ingresar el numero de licencia del conductor correctamente.");
                            }
                            //if (parseFloat(ctrl.guiaRemision.Conductor.NumeroLicencia) == 0) {
                            //    ctrl.inconsistenciasGuiaRemision.push("Es necesario que el numero de licencia del conductor sea diferente 0.");
                            //}
                        }
                    }
                }
                if (ctrl.guiaRemision.ModalidadTransporte == undefined) {
                    ctrl.inconsistenciasGuiaRemision.push("Es necesario seleccionar la modalidad de transporte.");
                }
                if (ctrl.guiaRemision.MotivoTraslado == undefined) {
                    ctrl.inconsistenciasGuiaRemision.push("Es necesario seleccionar el motivo de traslado.");
                }
                else {
                    if (ctrl.guiaRemision.MotivoTraslado.Id == ctrl.parametros.IdMotivoTrasladoOtros) {
                        if (ctrl.guiaRemision.DescripcionMotivo == '' || ctrl.guiaRemision.DescripcionMotivo == undefined) {
                            ctrl.inconsistenciasGuiaRemision.push("Es necesario ingresar el detalle del motivo.");
                        }
                    }
                    if (ctrl.guiaRemision.MotivoTraslado.Id == ctrl.parametros.IdMotivoTrasladoPorTrasladoEntreEstablecimientosDeLaMismaEmpresa) {
                        if (ctrl.guiaRemision.Tercero.NumeroDocumentoIdentidad != ctrl.parametros.NumeroDocumentoSede) {
                            ctrl.inconsistenciasGuiaRemision.push("Es necesario que el destinatario sea el mismo al remitente.");
                        }
                    } else {
                        if (ctrl.guiaRemision.Tercero.NumeroDocumentoIdentidad == ctrl.parametros.NumeroDocumentoSede) {
                            ctrl.inconsistenciasGuiaRemision.push("Es necesario que el destinatario sea diferencte al remitente.");
                        }
                    }
                }
                if (ctrl.guiaRemision.UbigeoOrigen == undefined) {
                    ctrl.inconsistenciasGuiaRemision.push("Es necesario seleccionar el ubigeo origen del traslado.");
                }
                if (ctrl.guiaRemision.DireccionOrigen == '' || ctrl.guiaRemision.DireccionOrigen == undefined) {
                    ctrl.inconsistenciasGuiaRemision.push("Es necesario ingresar la direccion origen del traslado.");
                }
                if (ctrl.guiaRemision.UbigeoDestino == undefined) {
                    ctrl.inconsistenciasGuiaRemision.push("Es necesario seleccionar el ubigeo destino del traslado.");
                }
                if (ctrl.guiaRemision.DireccionDestino == '' || ctrl.guiaRemision.DireccionDestino == undefined) {
                    ctrl.inconsistenciasGuiaRemision.push("Es necesario ingresarla direccion destino del traslado.");
                }
                if (ctrl.guiaRemision.Detalles != undefined) {
                    for (var i = 0; i < ctrl.guiaRemision.Detalles.length; i++) {
                        if (!ctrl.registroDesdeOrdenAlmacen) {
                            if (ctrl.guiaRemision.Detalles[i].IngresoSalidaActual == 0) {
                                ctrl.inconsistenciasGuiaRemision.push('Es necesario que la cantidad sea mayor a 0.');
                                break;
                            }
                        }
                    }
                    if (ctrl.registroDesdeOrdenAlmacen) {
                        let diferenteCero = false;
                        let maximoPendiente = false;
                        let maximoStock = false;
                        for (var i = 0; i < ctrl.guiaRemision.Detalles.length; i++) {
                            diferenteCero = diferenteCero || (ctrl.guiaRemision.Detalles[i].IngresoSalidaActual != 0 && ctrl.guiaRemision.Detalles[i].IngresoSalidaActual != null);
                            maximoPendiente = maximoPendiente || (ctrl.guiaRemision.Detalles[i].IngresoSalidaActual > ctrl.guiaRemision.Detalles[i].Pendiente);
                            maximoStock = maximoStock || (ctrl.guiaRemision.Detalles[i].IngresoSalidaActual > ctrl.guiaRemision.Detalles[i].StockActual);
                        }
                        if (!diferenteCero) {
                            ctrl.inconsistenciasGuiaRemision.push('Es necesario que al menos un detalle sea diferente de 0.');
                        }
                        if (maximoPendiente) {
                            ctrl.inconsistenciasGuiaRemision.push('Es necesario que el ingreso actual sea menor o igual al monto pendiente.');
                        }
                        if (maximoStock) {
                            ctrl.inconsistenciasGuiaRemision.push('Es necesario que el ingreso actual sea menor o igual al monto pendiente.');
                        }
                    }

                    if (ctrl.guiaRemision.Detalles.length == 0) {
                        ctrl.inconsistenciasGuiaRemision.push('Es necesario que tenga detalles la guia.');
                    }
                }

                ctrl.hayInconsitenciasGuiaRemision = ctrl.inconsistenciasGuiaRemision.length > 0 ? true : false;
            }

            ctrl.cargarDatosDesdeVenta = function () {
                ctrl.guiaRemision.MotivoTraslado = Enumerable.from(ctrl.motivosTraslado).where("$.Id == '" + ctrl.parametros.IdMotivoTrasladoPorVenta + "'").toArray()[0];
                $timeout(function () { $('#motivo').trigger("change"); }, 100);
                $("#motivo").prop("disabled", true);
                ctrl.venta.Orden.Cliente.SeleccionarGrupo = false;
                ctrl.selectorTerceroAPI.SetActorComercial(angular.copy(ctrl.venta.Orden.Cliente));
                if (ctrl.venta.Orden.Detalles != undefined) {
                    for (var i = 0; i < ctrl.venta.Orden.Detalles.length; i++) {
                        ctrl.guiaRemision.Detalles.push({ IdProducto: ctrl.venta.Orden.Detalles[i].Producto.Id, Descripcion: ctrl.venta.Orden.Detalles[i].Producto.Nombre, EsBien: ctrl.venta.Orden.Detalles[i].Producto.EsBien, Ordenado: parseFloat(ctrl.venta.Orden.Detalles[i].Cantidad), RecibidoEntregado: 0.00, IngresoSalidaActual: parseFloat(ctrl.venta.Orden.Detalles[i].Cantidad) });
                    }
                }
            }

            ctrl.cargarDatosDesdeOrdenAlmacen = function () {
                ctrl.guiaRemision.MotivoTraslado = undefined;
                ctrl.guiaRemision.IdOrdenDeAlmacen = ctrl.ordenAlmacen.IdOrdenDeAlmacen;
                ctrl.guiaRemision.DocumentoReferencia = ctrl.ordenAlmacen.DocumentoReferencia;
                ctrl.selectorTerceroAPI.SetActorComercialPorId(ctrl.ordenAlmacen.Tercero.Id);
                ctrl.selectorTransportistaAPI.EstablecerActorPorDefecto();
                ctrl.selectorConductorAPI.EstablecerActorPorDefecto();
                ctrl.guiaRemision.Detalles = ctrl.ordenAlmacen.Detalles;
                if (ctrl.registroDesdeOrdenAlmacen) {
                    ctrl.etiquetaTercero = ctrl.nombreEtiquetaTercero;
                    if (ctrl.seleccionadoPorIngresar == true) {
                        ctrl.guiaRemision.DireccionDestino = ctrl.parametros.DireccionSede;
                        ctrl.seleccionarUbigeoDestinoPorDefecto(ctrl.parametros.IdUbigeoSede);
                    } else {
                        ctrl.guiaRemision.DireccionOrigen = ctrl.parametros.DireccionSede;
                        ctrl.seleccionarUbigeoOrigenPorDefecto(ctrl.parametros.IdUbigeoSede);
                    }
                }
            }

            this.$onInit = function () {
                ctrl.api = {};
                ctrl.inicializar();
                //ctrl.api.Inicializar = ctrl.inicializar;
                ctrl.api.InicializarRegistroGuiaRemision = ctrl.inicializarRegistroGuiaRemision;
                ctrl.api.LimpiarGuiaRemision = ctrl.limpiarGuiaRemision;
                ctrl.api.CargarDatosDesdeVenta = ctrl.cargarDatosDesdeVenta;
                ctrl.api.NuevoRegistroGuiaRemision = ctrl.nuevoRegistroGuiaRemision;

            };

            $('input[type="checkbox"]').on('change', function () {
                $('input[type="checkbox"]').not(this).prop('checked', false);
            });

        }
    });
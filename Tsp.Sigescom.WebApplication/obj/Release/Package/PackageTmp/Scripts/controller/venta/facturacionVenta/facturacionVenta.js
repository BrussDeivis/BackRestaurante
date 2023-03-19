angular.
    module('app').
    component('facturacionVenta', {
        templateUrl: "../Scripts/controller/venta/facturacionVenta/facturacionVenta.html",
        bindings: {
            api: '=',
            externalId: '<',
            facturacion: '=',
            mostrarPuntoDeVentaVendedor: '=',
            debeSeleccionarPuntoDeVentaVendedor: '=',
            debeSeleccionarAlmacenAlmacenero: '=',
            debeSeleccionarCajaCajero: '=',
            debePermitirDetalleUnificado: '<',
            permitirRegistroFechaEmision: '<',
            permitirRegistroGuiaRemision: '<',
            permitirRegistroPlaca: '<',
            idMedioPagoDefault: '<',
            importeTotal: '<',
            cambioIgv: '&',
            inicioRealizado: '&',
            cargaTotalRealizada: '&',
            serieComprobanteEnter: '&',
            changeFacturacion: '&',
            evitarAsignarComprobantePorDefecto: '<'
        },

        controller: function ($q, $scope, $parse, $timeout, SweetAlert, ventaService, clienteService, empleadoService, centroDeAtencionService) {
            $timeout(function () {
                $('.td-datepicker').datepicker({
                    format: 'dd/mm/yyyy',
                    autoclose: true,
                    todayHighlight: true,
                    language: 'es',
                    orientation: 'bottom'
                });
            }, 100);
            $('select:not(.normal)').each(function () {
                $(this).select2({
                    dropdownParent: $(this).parent()
                });
            });
            var ctrl = this;

            ctrl.cambioModoIGV = function () {
                ctrl.cambioIgv({ aplicarIgv: ctrl.facturacion.Orden.AplicarIGVCuandoEsAmazonia });
            };

            ctrl.inicioTerminado = function () {
                ctrl.inicioRealizado({ facturacion: ctrl.facturacion });
            };

            ctrl.cargaTotalTerminado = function () {
                ctrl.cargaTotalRealizada({});
            };

            ctrl.serieComprobante_enter = function () {
                ctrl.serieComprobanteEnter({});
            };

            ctrl.cambioFacturacion = function () {
                ctrl.changeFacturacion({});
            };

            ctrl.inicializar = function () {
                ctrl.limpiarFacturacion();
                ctrl.cargarColeccionesAsync();
                ctrl.cargarColeccionesSync().then(function (resultado_) {
                    ctrl.establecerDatosPorDefecto();
                    ctrl.inicioTerminado();
                }, function (error) {
                    ctrl.mensaje = "Se ha producido un error al obtener el dato:" + error;
                });
            };

            ctrl.limpiarFacturacion = function () {
                ctrl.facturacion = {
                    Orden: {
                        Cliente: { TipoDocumentoIdentidad: {} }, Comprobante: { Tipo: {} }, PuntoVenta: {}, Vendedor: {}, HayBienesEnLosDetalles: true, Flete: 0
                    },
                    Pago: {
                        ModoDePago: 1, Traza: { MedioDePago: {}, Info: { EntidadFinanciera: {}, OperadorTarjeta: {}, ImporteAPagar: 0 } }, Caja: {}, Cajero: {}
                    },
                    MovimientoAlmacen: { HayComprobanteDeSalidaDeMercaderia: false, EntregaDiferida: "false", Almacen: {}, Almacenero: {} }
                };
                ctrl.facturacion.esValido = false;
                ctrl.inicioRealizado({ facturacion: ctrl.facturacion });
            };

            ctrl.cargarParametros = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                ventaService.obtenerParametrosDeFacturacion({}).success(function (data) {
                    ctrl.parametros = data.data;
                    ctrl.inicializacionRealizada = true;
                    defered.resolve();
                }).error(function (data) {
                    SweetAlert.error("Ocurrio un Problema", data.error);
                    defered.reject(data);
                });
                return promise;
            };

            ctrl.cargarColeccionesAsync = function () {
            };

            ctrl.cargarColeccionesSync = function () {
                var defered = $q.defer();
                var promiseList = [];
                promiseList.push(ctrl.cargarParametros());
                if (ctrl.debeSeleccionarPuntoDeVentaVendedor) {
                    promiseList.push(ctrl.obtenerPuntosDeVenta());
                    promiseList.push(ctrl.obtenerVendedores());
                }
                if (ctrl.debeSeleccionarCajaCajero) {
                    promiseList.push(ctrl.obtenerCajas());
                    promiseList.push(ctrl.obtenerCajeros());
                }
                if (ctrl.debeSeleccionarAlmacenAlmacenero) {
                    promiseList.push(ctrl.obtenerAlmacenes());
                    promiseList.push(ctrl.obtenerAlmaceneros());
                }
                return $q.all(promiseList).then(function (response) {
                    defered.resolve();
                }).catch(function (error) {
                    defered.reject(e);
                });
            };

            ctrl.establecerDatosPorDefecto = function () {
                ctrl.facturacion.Orden.Observacion = '';
                ctrl.facturacion.Orden.AplicarIGVCuandoEsAmazonia = false;
                ctrl.facturacion.UnificarDetalles = ctrl.parametros.CheckedDetalleUnificado;
                ctrl.facturacion.Orden.ValorDetalleUnificado = ctrl.parametros.ValorDetalleUnificado;
                if (ctrl.debeSeleccionarPuntoDeVentaVendedor) {
                    ctrl.facturacion.Orden.PuntoDeVenta = ctrl.puntosDeVenta[0];
                    ctrl.facturacion.Orden.Vendedor = ctrl.vendedores[0];
                }
                if (ctrl.debeSeleccionarCajaCajero) {
                    ctrl.facturacion.Pago.Caja = ctrl.cajas[0];
                    ctrl.facturacion.Pago.Cajero = ctrl.cajeros[0];
                }
                if (ctrl.debeSeleccionarAlmacenAlmacenero) {
                    ctrl.facturacion.MovimientoAlmacen.Almacen = ctrl.almacenes[0];
                    ctrl.facturacion.MovimientoAlmacen.Almacenero = ctrl.almaceneros[0];
                }
            };

            ctrl.limpiarDatosDespuesFacturar = function () {
                ctrl.limpiarFacturacion();
                ctrl.selectorClienteAPI.LimpiarActorComercial();
                ctrl.establecerDatosPorDefecto();
                ctrl.cancelarGuiaRemision();
                ctrl.editorPagoAPI.RestablecerInfoPago();
                ctrl.selectorClienteAPI.EstablecerActorPorDefecto();
            };

            ctrl.setActorComercialPorId = function (idActorComercial) {
                ctrl.selectorClienteAPI.SetActorComercialPorId(idActorComercial);
            };

            ctrl.setActorComercial = function (actorComercial) {
                ctrl.facturacion.Orden.Cliente = actorComercial;
            };


            ctrl.inicioRealizadoCliente = function () {
                ctrl.cargaTotalTerminado();
            };

            ctrl.cambioCliente = function (actorComercial) {
                ctrl.facturacion.Orden.Cliente = actorComercial;
                if (ctrl.permitirRegistroPlaca && ctrl.facturacion.Orden.Cliente.Id != ctrl.parametros.IdClienteGenerico) {
                    ctrl.obtenerUltimaPlacaCliente().then(function (resultado_) {
                        ctrl.complementosCliente();
                    }, function (error) {
                        ctrl.mensaje = "Se ha producido un error al obtener el dato:" + error;
                    });
                } else {
                    ctrl.complementosCliente();
                }
            };

            ctrl.complementosCliente = function () {
                ctrl.asignarComprobantePorDefecto();
                ctrl.editorPagoAPI.VerificarMediosDePagoAMostrar(ctrl.facturacion.Orden.Cliente.Id);
                ctrl.verificarInconsistenciasFacturacion();
            };

            ctrl.obtenerUltimaPlacaCliente = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                clienteService.obtenerUltimaPlacaDeCliente({ idCliente: ctrl.facturacion.Orden.Cliente.Id }).success(function (data) {
                    ctrl.facturacion.Orden.Placa = data;
                    defered.resolve();
                }).error(function (data) {
                    SweetAlert.error("Ocurrio un Problema", data.error);
                    defered.reject(data);
                });
                return promise;
            };

            ctrl.cambioComprobante = function (comprobante) {
                ctrl.facturacion.Orden.Comprobante = comprobante;
                if (document.getElementById("observacion") != null) {
                    if (ctrl.facturacion.Orden.Comprobante.Id == ctrl.parametros.IdComprobanteBoleta || ctrl.facturacion.Orden.Comprobante.Id == ctrl.parametros.IdComprobanteFactura) {
                        document.getElementById("observacion").maxLength = "200";
                    } else {
                        document.getElementById("observacion").maxLength = "500";
                    }
                }
                ctrl.verificarInconsistenciasFacturacion();
            };

            ctrl.cambioPago = function (pago) {
                ctrl.facturacion.Pago = pago;
                ctrl.verificarInconsistenciasFacturacion();
            };

            ctrl.inicioRealizadoPago = function () {
                ctrl.inicializacionRealizadaPago = true;
                ctrl.setTotalVenta(ctrl.importeTotal);
            };

            ctrl.mostrarDetalleUnificado = function () {
                ctrl.debePermitirDetalleUnificado = true;
            };

            ctrl.ocultarDetalleUnificado = function () {
                ctrl.debePermitirDetalleUnificado = false;
            };

            ctrl.seleccionarDetalleUnificado = function () {
                if (ctrl.facturacion.Orden.UnificarDetalles == false) {
                    ctrl.facturacion.Orden.ValorDetalleUnificado = ''
                }
                ctrl.verificarInconsistenciasFacturacion();
            };

            ctrl.asignarComprobantePorDefecto = function () {
                if (ctrl.evitarAsignarComprobantePorDefecto) {
                } else {
                    if (ctrl.facturacion.Orden.Cliente.Id === ctrl.parametros.IdClienteGenerico) {
                        ctrl.selectorComprobanteAPI.SelecccionarTipo(ctrl.parametros.IdTipoDocumentoCuandoClienteEsGenerico);
                    } else {
                        if (ctrl.facturacion.Orden.Cliente.NumeroDocumentoIdentidad.length == 11) {
                            ctrl.selectorComprobanteAPI.SelecccionarTipo(ctrl.parametros.IdComprobanteFactura);
                        } else {
                            ctrl.selectorComprobanteAPI.SelecccionarTipo(ctrl.parametros.IdTipoDocumentoPorDefectoParaVenta);
                        }
                    }
                }
            };

            ctrl.asignarComprobante = function (idComprobante) {
                ctrl.selectorComprobanteAPI.SelecccionarTipo(idComprobante);
            }

            ctrl.setTotalVenta = function (importe) {
                ctrl.actualizarImporteAPagar(importe);
                ctrl.validarGuiaRemision();
                ctrl.verificarInconsistenciasFacturacion();
            };

            ctrl.initSeleccionMedioPago = function () {
                ctrl.editorPagoAPI.InitSeleccionMedioPago();
            };

            ctrl.actualizarImporteAPagar = function (importe) {
                ctrl.facturacion.Orden.Total = importe;
                ctrl.editorPagoAPI.SetImporteAPagar(importe);
            };

            ctrl.validarGuiaRemision = function () {
                if (ctrl.facturacion.MovimientoAlmacen.HayComprobanteDeSalidaDeMercaderia) {
                    for (var i = 0; i < ctrl.facturacion.Orden.Detalles.length; i++) {
                        let detalle = ctrl.guiaRemisionVenta.Detalles.find(d => d.IdProducto == ctrl.facturacion.Orden.Detalles[i].Producto.Id);
                        if (detalle != undefined) {
                            if (detalle.IngresoSalidaActual != ctrl.facturacion.Orden.Detalles[i].Cantidad) {
                                ctrl.mostrarBorrarGuiaRemision();
                            }
                        } else {
                            ctrl.mostrarBorrarGuiaRemision();
                        }
                    }
                }
            };

            ctrl.verificarInconsistenciasFacturacion = function () {
                ctrl.inconsistenciasFacturacion = [];
                if (ctrl.debePermitirDetalleUnificado == true && ctrl.facturacion.Orden.UnificarDetalles == true && ctrl.parametros.ActivarDetalleUnificadoPersonalizado == true && (ctrl.facturacion.Orden.ValorDetalleUnificado == null || ctrl.facturacion.Orden.ValorDetalleUnificado == '')) {
                    ctrl.inconsistenciasFacturacion.push("Es necesario indicar el detalle unificado.");
                }
                if (ctrl.facturacion.Orden.Cliente.esValido == false) {
                    ctrl.inconsistenciasFacturacion.push("Es necesario seleccionar un cliente.");
                }
                if (ctrl.facturacion.Orden.Comprobante.Tipo == undefined) {
                    ctrl.inconsistenciasFacturacion.push("Es necesario seleccionar un documento.");
                }
                if (ctrl.facturacion.Orden.Comprobante.Serie == undefined) {
                    ctrl.inconsistenciasFacturacion.push("Es necesario que el documento tenga alguna serie.");
                }
                if (ctrl.facturacion.Orden.Comprobante.Tipo.Id == ctrl.parametros.IdComprobanteFactura && ctrl.facturacion.Orden.Cliente.TipoDocumentoIdentidad.Id != ctrl.parametros.IdTipoDocumentoIdentidadRuc) {
                    ctrl.inconsistenciasFacturacion.push("Es necesario que el cliente tenga Ruc.");
                }
                if (ctrl.facturacion.Orden.Cliente.Id == ctrl.parametros.IdClienteGenerico && ctrl.facturacion.Orden.Comprobante.Tipo.Id == ctrl.parametros.IdComprobanteBoleta && ctrl.facturacion.Pago.Traza.Info.ImporteAPagar >= ctrl.parametros.MontoMaximoAVenderCuandoClienteNoEstaIdentificado) {
                    ctrl.inconsistenciasFacturacion.push("Es necesario identificar al cliente, el total es mayor a S/.700");
                }
                if (parseFloat(ctrl.facturacion.Pago.Traza.Info.ImporteAPagar) == 0) {
                    ctrl.inconsistenciasFacturacion.push("Es necesario que el importe total sea mayor a 0.00");
                }
                if (ctrl.permitirRegistroFechaEmision) {
                    if (ctrl.facturacion.Orden.FechaEmision == undefined || ctrl.facturacion.Orden.FechaEmision == "") {
                        ctrl.inconsistenciasFacturacion.push("Es necesario ingresar la fecha de venta");
                    } else {
                        if (new Date(ctrl.facturacion.Orden.FechaEmision.split("/")[1] + '-' + ctrl.facturacion.Orden.FechaEmision.split("/")[0] + '-' + ctrl.facturacion.Orden.FechaEmision.split("/")[2]) > new Date(ctrl.parametros.FechaActual.split("/")[1] + '-' + ctrl.parametros.FechaActual.split("/")[0] + '-' + ctrl.parametros.FechaActual.split("/")[2])) {
                            ctrl.inconsistenciasFacturacion.push("Es necesario que la fecha sea menor o igual a la fecha actual.");
                        }
                    }
                }
                if (ctrl.facturacion.MovimientoAlmacen.HaySalidaDeMercaderia && ctrl.facturacion.Orden.Cliente.Id == ctrl.parametros.IdClienteGenerico) {
                    ctrl.inconsistenciasFacturacion.push("Es necesario identificar el cliente al emitir una guia de remision");
                }
                if (ctrl.facturacion.Pago.ModoDePago == 2) {
                    if (ctrl.facturacion.Pago.Inicial != null && ctrl.facturacion.Pago.Inicial != 0) {
                        if (parseFloat(ctrl.facturacion.Pago.Inicial) > parseFloat(ctrl.facturacion.Orden.Total)) {
                            ctrl.inconsistenciasFacturacion.push("Es necesario que el monto inicial sea menor al total de venta.");
                        }
                    }
                } else if (ctrl.facturacion.Pago.ModoDePago == 3) {
                    if (ctrl.facturacion.Pago.Cuotas == undefined) {
                        ctrl.inconsistenciasFacturacion.push("Es necesario al seleccionar credito configurado genere las cuotas respectivas.");
                    }
                }
                if (ctrl.facturacion.Pago.ModoDePago == 2 || ctrl.facturacion.Pago.ModoDePago == 3) {
                    if (ctrl.facturacion.Orden.Cliente.Id == ctrl.parametros.IdClienteGenerico) {
                        ctrl.inconsistenciasFacturacion.push("Es necesario seleccionar un cliente cuando es una venta al credito.");
                    }
                }
                if (ctrl.facturacion.Pago.Traza.MedioDePago.Id == ctrl.parametros.IdMedioDePagoEfectivo) {
                    if (ctrl.facturacion.Pago.Traza.Info.ImporteEntregado != null && ctrl.facturacion.Pago.Traza.Info.ImporteEntregado != 0) {
                        if (parseFloat(ctrl.facturacion.Pago.Traza.Info.ImporteEntregado) < parseFloat(ctrl.facturacion.Pago.Traza.Info.ImporteAPagar)) {
                            ctrl.inconsistenciasFacturacion.push("Es necesario que el monto a recibir debe de ser mayor al importe total de la venta.");
                        }
                    }
                } else if (ctrl.facturacion.Pago.Traza.MedioDePago.Id == ctrl.parametros.IdMedioDePagoTarjetaCredito || ctrl.facturacion.Pago.Traza.MedioDePago.Id == ctrl.parametros.IdMedioDePagoTarjetaDebito) {
                    if (ctrl.facturacion.Pago.Traza.Info.EntidadFinanciera == null) {
                        ctrl.inconsistenciasFacturacion.push("Es necesario seleccionar una entidad bancaria.");
                    } if (ctrl.facturacion.Pago.Traza.Info.OperadorTarjeta == null) {
                        ctrl.inconsistenciasFacturacion.push("Es necesario seleccionar el tipo de tarjeta.");
                    } if (ctrl.facturacion.Pago.Traza.Info.Observacion == null || ctrl.facturacion.Pago.Traza.Info.Observacion == '') {
                        ctrl.inconsistenciasFacturacion.push("Es necesario ingresar información del pago.");
                    }
                } if (ctrl.facturacion.Pago.Traza.MedioDePago.Id == ctrl.parametros.IdMedioDePagoDepositoEnCuenta || ctrl.facturacion.Pago.Traza.MedioDePago.Id == ctrl.parametros.IdMedioDePagoTransferenciaDeFondos) {
                    if (ctrl.facturacion.Pago.Traza.Info.CuentaBancaria == null) {
                        ctrl.inconsistenciasFacturacion.push("Es necesario seleccionar la cuenta bancaria.");
                    } if (ctrl.facturacion.Pago.Traza.Info.Observacion == null || ctrl.facturacion.Pago.Traza.Info.Observacion == '') {
                        ctrl.inconsistenciasFacturacion.push("Es necesario ingresar información del pago.");
                    }
                } else if (ctrl.facturacion.Pago.Traza.MedioDePago.Id == ctrl.parametros.IdMedioDePagoPuntos) {
                    if (ctrl.facturacion.Pago.Traza.Info.MontoCajeado != null && ctrl.facturacion.Pago.Traza.Info.MontoCajeado != 0) {
                        if (ctrl.facturacion.Pago.ModoDePago == 1) {
                            if (parseFloat(ctrl.facturacion.Pago.Traza.Info.MontoCajeado) != parseFloat(ctrl.facturacion.Pago.Traza.Info.ImporteAPagar)) {
                                ctrl.inconsistenciasFacturacion.push("Es necesario que el monto a canjear sea igual al importe a pagar.");
                            }
                        } else {
                            if (parseFloat(ctrl.facturacion.Pago.Traza.Info.MontoCajeado) != parseFloat(ctrl.facturacion.Pago.Inicial)) {
                                ctrl.inconsistenciasFacturacion.push("Es necesario que el monto a canjear sea igual al importe a pagar.");
                            }
                        }
                        if (parseFloat(ctrl.facturacion.Pago.Traza.Info.MontoCajeado) > parseFloat(ctrl.facturacion.Pago.Traza.Info.ValorPuntosPorCanjear)) {
                            ctrl.inconsistenciasFacturacion.push("Es necesario que el monto a canjear sea menor o igual al valor de los puntos por canjear.");
                        }
                    } else {
                        ctrl.inconsistenciasFacturacion.push("Es necesario que el monto a canjear sea mayor a cero.");
                    }
                    if (ctrl.facturacion.Pago.Traza.Info.PuntosPorCanjear == 0) {
                        ctrl.inconsistenciasFacturacion.push("Es necesario que tenga puntos pendientes para canjear.");
                    }
                }
                ctrl.facturacion.esValido = ctrl.inconsistenciasFacturacion.length > 0 ? false : true;
                ctrl.cambioFacturacion();
            }

            ctrl.obtenerPuntosDeVenta = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                centroDeAtencionService.obtenerCentrosDeAtencionConRolPuntoDeVentaVigentesConCodigoYEstablecimientoComercial().success(function (data) {
                    ctrl.puntosDeVenta = data;
                    defered.resolve();
                }).error(function (data) {
                    SweetAlert.error2(data);
                    defered.reject(data);
                });
                return promise;
            };

            ctrl.obtenerVendedores = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                empleadoService.obtenerEmpleadosConRolVendedorVigentesConCodigo().success(function (data) {
                    ctrl.vendedores = data;
                    defered.resolve();
                }).error(function (data) {
                    SweetAlert.error2(data);
                    defered.reject(data);
                });
                return promise;
            };

            ctrl.obtenerCajas = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                centroDeAtencionService.obtenerCentrosDeAtencionConRolCajaVigentesConCodigoYEstablecimientoComercial().success(function (data) {
                    ctrl.cajas = data;
                    defered.resolve();
                }).error(function (data) {
                    SweetAlert.error2(data);
                    defered.reject(data);
                });
                return promise;
            };

            ctrl.obtenerCajeros = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                empleadoService.obtenerEmpleadosConRolCajeroVigentesConCodigo().success(function (data) {
                    ctrl.cajeros = data;
                    defered.resolve();
                }).error(function (data) {
                    SweetAlert.error2(data);
                    defered.reject(data);
                });
                return promise;
            };

            ctrl.obtenerAlmacenes = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                centroDeAtencionService.obtenerCentrosDeAtencionConRolAlmacenVigentesConCodigoYEstablecimientoComercial().success(function (data) {
                    ctrl.almacenes = data;
                    defered.resolve();
                }).error(function (data) {
                    SweetAlert.error2(data);
                    defered.reject(data);
                });
                return promise;
            };

            ctrl.obtenerAlmaceneros = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                empleadoService.obtenerEmpleadosConRolAlmaceneroVigentesConCodigo().success(function (data) {
                    ctrl.almaceneros = data;
                    defered.resolve();
                }).error(function (data) {
                    SweetAlert.error2(data);
                    defered.reject(data);
                });
                return promise;
            };

            ctrl.seleccionarPuntoDeVentaConCodigo = function (codigo) {
                var puntoDeVenta = Enumerable.from(ctrl.puntosDeVenta).where("$.Codigo == '" + codigo + "'").toArray()[0];
                ctrl.facturacion.Orden.PuntoDeVenta = puntoDeVenta != null ? puntoDeVenta : ctrl.puntosDeVenta[0];
                $timeout(function () { $('#puntoDeVenta').trigger("change"); }, 100);
            }

            ctrl.seleccionarVendedorConCodigo = function (codigo) {
                var vendedor = Enumerable.from(ctrl.vendedores).where("$.Codigo == '" + codigo + "'").toArray()[0];
                ctrl.facturacion.Orden.Vendedor = vendedor != null ? vendedor : ctrl.vendedores[0];
                $timeout(function () { $('#vendedor').trigger("change"); }, 100);
            }

            ctrl.setFocusSerieComprobante = function () {
                ctrl.selectorComprobanteAPI.SetFocusSerieComprobante();
            }

            ctrl.verificarEntregaDiferida = function () {
                if (ctrl.facturacion.MovimientoAlmacen.HayComprobanteDeSalidaDeMercaderia) {
                    $('#modal-confirmar-borrar-guia-remision').modal('show');
                }
            }

            ctrl.mostrarBorrarGuiaRemision = function () {
                ctrl.borrarGuiaRemision();
                $('#modal-borrar-guia-remision').modal('show');
            }

            //#region GUIAS DE REMISION 
            ctrl.inicializarRegistroGuiaRemision = function () {
                if (ctrl.inicializarGuiaRemision) {
                    if (!ctrl.facturacion.MovimientoAlmacen.HayComprobanteDeSalidaDeMercaderia) {
                        ctrl.registradorGuiaRemisionApi.NuevoRegistroGuiaRemision();
                    }
                } else {
                    ctrl.inicializarGuiaRemision = true;
                }
            }

            ctrl.aceptarGuiaRemision = function () {
                ctrl.facturacion.MovimientoAlmacen.HayComprobanteDeSalidaDeMercaderia = true;
                ctrl.facturacion.MovimientoAlmacen.RegistroDeMovimientosDeAlmacen = [];
                ctrl.facturacion.MovimientoAlmacen.RegistroDeMovimientosDeAlmacen.push(ctrl.guiaRemisionVenta);
                $('#modal-registro-guia-remision').modal('hide');
            }

            ctrl.borrarGuiaRemision = function () {
                ctrl.facturacion.MovimientoAlmacen.HayComprobanteDeSalidaDeMercaderia = false;
                ctrl.facturacion.MovimientoAlmacen.RegistroDeMovimientosDeAlmacen = [];
                ctrl.guiaRemisionVenta = {};
            }

            ctrl.cancelarGuiaRemision = function () {
                ctrl.borrarGuiaRemision();
                $('#modal-registro-guia-remision').modal('hide');
            }

            ctrl.cancelarGuiaRemisionAdvertencia = function () {
                ctrl.borrarGuiaRemision();
                $('#modal-confirmar-borrar-guia-remision').modal('hide');
            }

            ctrl.mantenerGuiaRemisionAdvertencia = function () {
                ctrl.facturacion.MovimientoAlmacen.EntregaDiferida = "false";
                $('#modal-confirmar-borrar-guia-remision').modal('hide');
            }

            ctrl.verificarCancelarGuiaRemision = function () {
                if (ctrl.facturacion.MovimientoAlmacen.HayComprobanteDeSalidaDeMercaderia) {
                    ctrl.cancelarGuiaRemision();
                }
            }
            //#endregion

            this.$onInit = function () {
                ctrl.api = {};
                ctrl.inicializar();
                ctrl.api.SetTotalVenta = ctrl.setTotalVenta;
                ctrl.api.InitSeleccionMedioPago = ctrl.initSeleccionMedioPago;
                ctrl.api.SeleccionarPuntoDeVentaConCodigo = ctrl.seleccionarPuntoDeVentaConCodigo;
                ctrl.api.SeleccionarVendedorConCodigo = ctrl.seleccionarVendedorConCodigo;
                ctrl.api.SetActorComercialPorId = ctrl.setActorComercialPorId;
                ctrl.api.SetFocusSerieComprobante = ctrl.setFocusSerieComprobante;
                ctrl.api.LimpiarDatosDespuesFacturar = ctrl.limpiarDatosDespuesFacturar;
                ctrl.api.MostrarDetalleUnificado = ctrl.mostrarDetalleUnificado;
                ctrl.api.OcultarDetalleUnificado = ctrl.ocultarDetalleUnificado;
                ctrl.api.AsignarComprobante = ctrl.asignarComprobante;
                ctrl.api.SetActorComercial = ctrl.setActorComercial;
            };
        }
    });
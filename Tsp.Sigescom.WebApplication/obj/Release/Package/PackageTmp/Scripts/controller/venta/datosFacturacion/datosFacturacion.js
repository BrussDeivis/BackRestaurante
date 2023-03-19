angular.
    module('app').
    component('datosFacturacion', {
        templateUrl: "../Scripts/controller/venta/datosFacturacion/datosFacturacion.html",
        bindings: {
            api: '=',
            externalId: '<',
            datosFacturacion: '=',
            mostrarPuntoDeVentaVendedor: '=',
            debeSeleccionarPuntoDeVentaVendedor: '=',
            debeSeleccionarAlmacenAlmacenero: '=',
            debeSeleccionarCajaCajero: '=',
            debePermitirDetalleUnificado: '<',
            permitirRegistroFechaEmision: '<',
            permitirRegistroPlaca: '<',
            idMedioPagoDefault: '<',
            importeTotal: '<',
            cambioIgv: '&',
            inicioRealizado: '&',
            cargaTotalRealizada: '&',
            serieComprobanteEnter: '&',
            changeFacturacion: '&',
            tipoComprobantePara: '<',
            idComprobantePorDefecto: '<',
            permitirAsignarComprobantePorDefecto: '<',
            changeCliente: '&',
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
                ctrl.cambioIgv({ aplicarIgv: ctrl.datosFacturacion.Orden.AplicarIGVCuandoEsAmazonia });
            };

            ctrl.inicioTerminado = function () {
                ctrl.inicioRealizado({ datosFacturacion: ctrl.datosFacturacion });
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
                ctrl.datosFacturacion = {
                    Orden: {
                        Detalles: [], Cliente: { TipoDocumentoIdentidad: {} }, Comprobante: { Tipo: {} }, PuntoVenta: {}, Vendedor: {}, HayBienesEnLosDetalles: true, Flete: 0
                    },
                    Pago: {
                        ModoDePago: 1, Traza: { MedioDePago: {}, Info: { EntidadFinanciera: {}, OperadorTarjeta: {}, ImporteAPagar: 0 } }, Caja: {}, Cajero: {}
                    },
                    MovimientoAlmacen: { EntregaDiferida: "false", Almacen: {}, Almacenero: {} }
                };
                ctrl.datosFacturacion.esValido = false;
                ctrl.inicioRealizado({ datosFacturacion: ctrl.datosFacturacion });
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
                ctrl.datosFacturacion.Orden.Observacion = '';
                ctrl.datosFacturacion.Orden.AplicarIGVCuandoEsAmazonia = false;
                ctrl.datosFacturacion.UnificarDetalles = ctrl.parametros.CheckedDetalleUnificado;
                ctrl.datosFacturacion.Orden.ValorDetalleUnificado = ctrl.parametros.ValorDetalleUnificado;
                if (ctrl.debeSeleccionarPuntoDeVentaVendedor) {
                    ctrl.datosFacturacion.Orden.PuntoDeVenta = ctrl.puntosDeVenta[0];
                    ctrl.datosFacturacion.Orden.Vendedor = ctrl.vendedores[0];
                }
                if (ctrl.debeSeleccionarCajaCajero) {
                    ctrl.datosFacturacion.Pago.Caja = ctrl.cajas[0];
                    ctrl.datosFacturacion.Pago.Cajero = ctrl.cajeros[0];
                }
                if (ctrl.debeSeleccionarAlmacenAlmacenero) {
                    ctrl.datosFacturacion.MovimientoAlmacen.Almacen = ctrl.almacenes[0];
                    ctrl.datosFacturacion.MovimientoAlmacen.Almacenero = ctrl.almaceneros[0];
                }
            };

            ctrl.limpiarDatosDespuesFacturar = function () {
                ctrl.limpiarFacturacion();
                ctrl.selectorClienteAPI.LimpiarActorComercial();
                ctrl.establecerDatosPorDefecto();
                ctrl.selectorClienteAPI.EstablecerActorPorDefecto();
            };

            ctrl.setActorComercialPorId = function (idActorComercial) {
                ctrl.selectorClienteAPI.SetActorComercialPorId(idActorComercial);
            };

            ctrl.inicioRealizadoCliente = function () {
                ctrl.cargaTotalTerminado();
            };

            ctrl.cambioCliente = function (actorComercial) {
                ctrl.datosFacturacion.Orden.Cliente = actorComercial;
                if (ctrl.permitirRegistroPlaca && ctrl.datosFacturacion.Orden.Cliente.Id != ctrl.parametros.IdClienteGenerico) {
                    ctrl.obtenerUltimaPlacaCliente().then(function (resultado_) {
                        ctrl.complementosCliente(actorComercial);
                    }, function (error) {
                        ctrl.mensaje = "Se ha producido un error al obtener el dato:" + error;
                    });
                } else {
                    ctrl.complementosCliente(actorComercial);
                }
            };

            ctrl.complementosCliente = function (actorComercial) {
                ctrl.asignarComprobantePorDefecto();
                ctrl.changeCliente({cliente: actorComercial});
                ctrl.cambioFacturacion();
            };

            ctrl.obtenerUltimaPlacaCliente = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                clienteService.obtenerUltimaPlacaDeCliente({ idCliente: ctrl.datosFacturacion.Orden.Cliente.Id }).success(function (data) {
                    ctrl.datosFacturacion.Orden.Placa = data;
                    defered.resolve();
                }).error(function (data) {
                    SweetAlert.error("Ocurrio un Problema", data.error);
                    defered.reject(data);
                });
                return promise;
            };

            ctrl.cambioComprobante = function (comprobante) {
                ctrl.datosFacturacion.Orden.Comprobante = comprobante;
                if (document.getElementById("observacion") != null) {
                    if (ctrl.datosFacturacion.Orden.Comprobante.Id == ctrl.parametros.IdComprobanteBoleta || ctrl.datosFacturacion.Orden.Comprobante.Id == ctrl.parametros.IdComprobanteFactura) {
                        document.getElementById("observacion").maxLength = "200";
                    } else {
                        document.getElementById("observacion").maxLength = "500";
                    }
                }
                ctrl.cambioFacturacion();
            };

            ctrl.mostrarDetalleUnificado = function () {
                ctrl.debePermitirDetalleUnificado = true;
            };

            ctrl.ocultarDetalleUnificado = function () {
                ctrl.debePermitirDetalleUnificado = false;
            };

            ctrl.seleccionarDetalleUnificado = function () {
                if (ctrl.datosFacturacion.Orden.UnificarDetalles == false) {
                    ctrl.datosFacturacion.Orden.ValorDetalleUnificado = ''
                }
                ctrl.cambioFacturacion();
            };

            ctrl.asignarComprobantePorDefecto = function () {
                if (ctrl.permitirAsignarComprobantePorDefecto) {
                    ctrl.selectorComprobanteAPI.SelecccionarTipo(ctrl.idComprobantePorDefecto);
                } else {
                    if (ctrl.datosFacturacion.Orden.Cliente.Id === ctrl.parametros.IdClienteGenerico) {
                        ctrl.selectorComprobanteAPI.SelecccionarTipo(ctrl.parametros.IdTipoDocumentoCuandoClienteEsGenerico);
                    } else {
                        if (ctrl.datosFacturacion.Orden.Cliente.NumeroDocumentoIdentidad.length == 11) {
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
                ctrl.cambioFacturacion();
            };

            ctrl.initSeleccionMedioPago = function () {
                ctrl.editorPagoAPI.InitSeleccionMedioPago();
            };

            ctrl.actualizarImporteAPagar = function (importe) {
                ctrl.datosFacturacion.Orden.Total = importe;
            };


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
                ctrl.datosFacturacion.Orden.PuntoDeVenta = puntoDeVenta != null ? puntoDeVenta : ctrl.puntosDeVenta[0];
                $timeout(function () { $('#puntoDeVenta').trigger("change"); }, 100);
            }

            ctrl.seleccionarVendedorConCodigo = function (codigo) {
                var vendedor = Enumerable.from(ctrl.vendedores).where("$.Codigo == '" + codigo + "'").toArray()[0];
                ctrl.datosFacturacion.Orden.Vendedor = vendedor != null ? vendedor : ctrl.vendedores[0];
                $timeout(function () { $('#vendedor').trigger("change"); }, 100);
            }

            ctrl.setFocusSerieComprobante = function () {
                ctrl.selectorComprobanteAPI.SetFocusSerieComprobante();
            }

            ctrl.verificarInconsistenciasFacturacion = function () {
                ctrl.inconsistenciasFacturacion = [];
                if (ctrl.debePermitirDetalleUnificado == true && ctrl.datosFacturacion.Orden.UnificarDetalles == true && ctrl.parametros.ActivarDetalleUnificadoPersonalizado == true && (ctrl.datosFacturacion.Orden.ValorDetalleUnificado == null || ctrl.datosFacturacion.Orden.ValorDetalleUnificado == '')) {
                    ctrl.inconsistenciasFacturacion.push("Es necesario indicar el detalle unificado.");
                }
                if (ctrl.datosFacturacion.Orden.Cliente.esValido == false) {
                    ctrl.inconsistenciasFacturacion.push("Es necesario seleccionar un cliente.");
                }
                if (ctrl.datosFacturacion.Orden.Comprobante.Tipo == undefined) {
                    ctrl.inconsistenciasFacturacion.push("Es necesario seleccionar un documento.");
                }
                if (ctrl.datosFacturacion.Orden.Comprobante.Serie.Id == undefined) {
                    ctrl.inconsistenciasFacturacion.push("Es necesario que el documento tenga alguna serie.");
                }
                if (ctrl.datosFacturacion.Orden.Comprobante.Tipo.Id == ctrl.parametros.IdComprobanteFactura && ctrl.datosFacturacion.Orden.Cliente.TipoDocumentoIdentidad.Id != ctrl.parametros.IdTipoDocumentoIdentidadRuc) {
                    ctrl.inconsistenciasFacturacion.push("Es necesario que el cliente tenga Ruc.");
                }
                if (ctrl.datosFacturacion.Orden.Cliente.Id == ctrl.parametros.IdClienteGenerico && ctrl.datosFacturacion.Orden.Comprobante.Tipo.Id == ctrl.parametros.IdComprobanteBoleta && ctrl.datosFacturacion.Pago.Traza.Info.ImporteAPagar >= ctrl.parametros.MontoMaximoAVenderCuandoClienteNoEstaIdentificado) {
                    ctrl.inconsistenciasFacturacion.push("Es necesario identificar al cliente, el total es mayor a S/.700");
                }
                if (parseFloat(ctrl.datosFacturacion.Pago.Traza.Info.ImporteAPagar) == 0) {
                    ctrl.inconsistenciasFacturacion.push("Es necesario que el importe total sea mayor a 0.00");
                }
                if (ctrl.permitirRegistroFechaEmision) {
                    if (ctrl.datosFacturacion.Orden.FechaEmision == undefined || ctrl.datosFacturacion.Orden.FechaEmision == "") {
                        ctrl.inconsistenciasFacturacion.push("Es necesario ingresar la fecha de venta");
                    } else {
                        if (new Date(ctrl.datosFacturacion.Orden.FechaEmision.split("/")[1] + '-' + ctrl.datosFacturacion.Orden.FechaEmision.split("/")[0] + '-' + ctrl.datosFacturacion.Orden.FechaEmision.split("/")[2]) > new Date(ctrl.parametros.FechaActual.split("/")[1] + '-' + ctrl.parametros.FechaActual.split("/")[0] + '-' + ctrl.parametros.FechaActual.split("/")[2])) {
                            ctrl.inconsistenciasFacturacion.push("Es necesario que la fecha sea menor o igual a la fecha actual.");
                        }
                    }
                }
                if (ctrl.datosFacturacion.MovimientoAlmacen.HaySalidaDeMercaderia && ctrl.datosFacturacion.Orden.Cliente.Id == ctrl.parametros.IdClienteGenerico) {
                    ctrl.inconsistenciasFacturacion.push("Es necesario identificar el cliente al emitir una guia de remision");
                }
                if (ctrl.datosFacturacion.Pago.ModoDePago == 2) {
                    if (ctrl.datosFacturacion.Pago.Inicial != null && ctrl.datosFacturacion.Pago.Inicial != 0) {
                        if (parseFloat(ctrl.datosFacturacion.Pago.Inicial) > parseFloat(ctrl.datosFacturacion.Orden.Total)) {
                            ctrl.inconsistenciasFacturacion.push("Es necesario que el monto inicial sea menor al total de venta.");
                        }
                    }
                } else if (ctrl.datosFacturacion.Pago.ModoDePago == 3) {
                    if (ctrl.datosFacturacion.Pago.Cuotas == undefined) {
                        ctrl.inconsistenciasFacturacion.push("Es necesario al seleccionar credito configurado genere las cuotas respectivas.");
                    }
                }
                if (ctrl.datosFacturacion.Pago.ModoDePago == 2 || ctrl.datosFacturacion.Pago.ModoDePago == 3) {
                    if (ctrl.datosFacturacion.Orden.Cliente.Id == ctrl.parametros.IdClienteGenerico) {
                        ctrl.inconsistenciasFacturacion.push("Es necesario seleccionar un cliente cuando es una venta al credito.");
                    }
                }
                if (ctrl.datosFacturacion.Pago.Traza.MedioDePago.Id == ctrl.parametros.IdMedioDePagoEfectivo) {
                    if (ctrl.datosFacturacion.Pago.Traza.Info.ImporteEntregado != null && ctrl.datosFacturacion.Pago.Traza.Info.ImporteEntregado != 0) {
                        if (parseFloat(ctrl.datosFacturacion.Pago.Traza.Info.ImporteEntregado) < parseFloat(ctrl.datosFacturacion.Pago.Traza.Info.ImporteAPagar)) {
                            ctrl.inconsistenciasFacturacion.push("Es necesario que el monto a recibir debe de ser mayor al importe total de la venta.");
                        }
                    }
                } else if (ctrl.datosFacturacion.Pago.Traza.MedioDePago.Id == ctrl.parametros.IdMedioDePagoTarjetaCredito || ctrl.datosFacturacion.Pago.Traza.MedioDePago.Id == ctrl.parametros.IdMedioDePagoTarjetaDebito) {
                    if (ctrl.datosFacturacion.Pago.Traza.Info.EntidadFinanciera == null) {
                        ctrl.inconsistenciasFacturacion.push("Es necesario seleccionar una entidad bancaria.");
                    } if (ctrl.datosFacturacion.Pago.Traza.Info.OperadorTarjeta == null) {
                        ctrl.inconsistenciasFacturacion.push("Es necesario seleccionar el tipo de tarjeta.");
                    } if (ctrl.datosFacturacion.Pago.Traza.Info.Observacion == null || ctrl.datosFacturacion.Pago.Traza.Info.Observacion == '') {
                        ctrl.inconsistenciasFacturacion.push("Es necesario ingresar información del pago.");
                    }
                } if (ctrl.datosFacturacion.Pago.Traza.MedioDePago.Id == ctrl.parametros.IdMedioDePagoDepositoEnCuenta || ctrl.datosFacturacion.Pago.Traza.MedioDePago.Id == ctrl.parametros.IdMedioDePagoTransferenciaDeFondos) {
                    if (ctrl.datosFacturacion.Pago.Traza.Info.CuentaBancaria == null) {
                        ctrl.inconsistenciasFacturacion.push("Es necesario seleccionar la cuenta bancaria.");
                    } if (ctrl.datosFacturacion.Pago.Traza.Info.Observacion == null || ctrl.datosFacturacion.Pago.Traza.Info.Observacion == '') {
                        ctrl.inconsistenciasFacturacion.push("Es necesario ingresar información del pago.");
                    }
                } else if (ctrl.datosFacturacion.Pago.Traza.MedioDePago.Id == ctrl.parametros.IdMedioDePagoPuntos) {
                    if (ctrl.datosFacturacion.Pago.Traza.Info.MontoCajeado != null && ctrl.datosFacturacion.Pago.Traza.Info.MontoCajeado != 0) {
                        if (ctrl.datosFacturacion.Pago.ModoDePago == 1) {
                            if (parseFloat(ctrl.datosFacturacion.Pago.Traza.Info.MontoCajeado) != parseFloat(ctrl.datosFacturacion.Pago.Traza.Info.ImporteAPagar)) {
                                ctrl.inconsistenciasFacturacion.push("Es necesario que el monto a canjear sea igual al importe a pagar.");
                            }
                        } else {
                            if (parseFloat(ctrl.datosFacturacion.Pago.Traza.Info.MontoCajeado) != parseFloat(ctrl.datosFacturacion.Pago.Inicial)) {
                                ctrl.inconsistenciasFacturacion.push("Es necesario que el monto a canjear sea igual al importe a pagar.");
                            }
                        }
                        if (parseFloat(ctrl.datosFacturacion.Pago.Traza.Info.MontoCajeado) > parseFloat(ctrl.datosFacturacion.Pago.Traza.Info.ValorPuntosPorCanjear)) {
                            ctrl.inconsistenciasFacturacion.push("Es necesario que el monto a canjear sea menor o igual al valor de los puntos por canjear.");
                        }
                    } else {
                        ctrl.inconsistenciasFacturacion.push("Es necesario que el monto a canjear sea mayor a cero.");
                    }
                    if (ctrl.datosFacturacion.Pago.Traza.Info.PuntosPorCanjear == 0) {
                        ctrl.inconsistenciasFacturacion.push("Es necesario que tenga puntos pendientes para canjear.");
                    }
                }
                ctrl.datosFacturacion.esValido = ctrl.inconsistenciasFacturacion.length > 0 ? false : true;
                ctrl.cambioFacturacion();
            }

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
                ctrl.api.VerificarInconsistenciasFacturacion = ctrl.verificarInconsistenciasFacturacion;
                ctrl.api.AsignarComprobante = ctrl.asignarComprobante;
            };
        }
    });
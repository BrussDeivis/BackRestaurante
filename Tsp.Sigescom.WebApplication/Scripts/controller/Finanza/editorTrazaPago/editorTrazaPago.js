angular.
    module('app').
    component('editorTrazaPago', {
        templateUrl: "../Scripts/controller/finanza/editorTrazaPago/editorTrazaPago.html",
        bindings: {
            api: '=',
            trazaPago: '=',
            idCliente: '<',
            externalId: '<',
            idMedioPagoDefault: '<',
            trazaPagoChanged: '&',
            inicioRealizado: '&'
        },
        controller: function ($q, $scope, $timeout, $compile, SweetAlert, ventaService, maestroService, finanzaService) {
            var ctrl = this;

            ctrl.cambioTrazaPago = function () {
                ctrl.trazaPagoChanged({});
            };

            ctrl.inicioTerminado = function () {
                ctrl.inicioRealizado();
            };

            ctrl.inicializar = function () {
                ctrl.limpiarTrazaPago();
                ctrl.cargarColeccionesAsync();
                ctrl.cargarColeccionesSync().then(function (result) {
                    ctrl.establecerDatosPorDefecto();
                }, function (error) {
                    SweetAlert.error("Ocurrio un Problema", error);
                });
            };

            ctrl.limpiarTrazaPago = function () {
                ctrl.trazaPago = { MedioDePago: {}, Info: { EntidadFinanciera: {}, OperadorTarjeta: {}, CuentaBancaria: {} } };
            };


            ctrl.establecerDatosPorDefecto = function () {
                ctrl.seleccionarMedioDePago(ctrl.idMedioPagoDefault);
                ctrl.inicioTerminado();
            };

            ctrl.cargarColeccionesAsync = function () {
                ctrl.obtenerEntidadesFinancieras();
                ctrl.obtenerOperadoresDeTarjeta();
                ctrl.obtenerCuentasBancarias();
            };

            ctrl.cargarColeccionesSync = function () {
                var defered = $q.defer();
                var promiseList = [];
                promiseList.push(ctrl.cargarParametros());
                promiseList.push(ctrl.obtenerMediosDePago());
                return $q.all(promiseList).then(function (response) {
                    defered.resolve();
                }).catch(function (error) {
                    defered.reject(e);
                });
            };

            ctrl.cargarParametros = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                ventaService.obtenerParametrosParaTrazaDePago({}).success(function (data) {
                    ctrl.parametros = data.data;
                    defered.resolve();
                }).error(function (data) {
                    SweetAlert.error("Ocurrio un Problema", data.error);
                    defered.reject(data);
                });
                return promise;
            };

            ctrl.obtenerMediosDePago = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                ventaService.obtenerMediosDePagoVenta({}).success(function (data) {
                    ctrl.mediosDePago = data;
                    ctrl.cargarMediosDePago();
                    defered.resolve();
                }).error(function (data) {
                    SweetAlert.error2(data);
                    defered.reject(data);
                });
                return promise;
            };

            ctrl.obtenerEntidadesFinancieras = function () {
                maestroService.obtenerEntidadesFinancieras({}).success(function (data) {
                    ctrl.entidadesFinancieras = data;
                }).error(function (data) {
                    SweetAlert.error2(data);
                });
            };

            ctrl.obtenerOperadoresDeTarjeta = function () {
                maestroService.obtenerOperadoresDeTarjeta({}).success(function (data) {
                    ctrl.operadoresDeTarjeta = data;
                }).error(function (data) {
                    SweetAlert.error2(data);
                });
            };

            ctrl.obtenerCuentasBancarias = function () {
                finanzaService.obtenerCuentasBancariasConEntidadFinancieraConMoneda({}).success(function (data) {
                    ctrl.cuentasBancarias = data;
                }).error(function (data) {
                    SweetAlert.error2(data);
                });
            };

            ctrl.cargarMediosDePago = function () {
                var html = '<div class="radio-toolbar">';
                for (var i = 0; i < ctrl.mediosDePago.length; i++) {
                    html += '<input type="radio" id="medioPago-' + ctrl.externalId + "-" + ctrl.mediosDePago[i].Id + '" name="medioPago-' + ctrl.externalId + '" value="' + ctrl.mediosDePago[i].Id + '" ng-click="$ctrl.seleccionarMedioDePago(' + ctrl.mediosDePago[i].Id + ')"><label id="labelMedioPago-' + ctrl.externalId + "-" + ctrl.mediosDePago[i].Id + '" for="medioPago-' + ctrl.externalId + "-" + ctrl.mediosDePago[i].Id + '">' + ctrl.mediosDePago[i].Nombre + '</label>';
                }
                html += '</div>';
                $('#medioPago' + ctrl.externalId).append($compile(html)($scope));
            }

            ctrl.seleccionarMedioDePagoPorDefecto = function () {
                ctrl.seleccionarMedioDePago(ctrl.idMedioPagoDefault);
            }

            ctrl.seleccionarMedioDePago = function (idMedioPago) {
                let medioPago = { Id: idMedioPago, Nombre: Enumerable.from(ctrl.mediosDePago).where("$.Id == '" + idMedioPago + "'").toArray()[0].Nombre };
                let radioMedioPago = document.getElementById('medioPago-' + ctrl.externalId + '-' + idMedioPago);
                if (radioMedioPago != null)
                    radioMedioPago.checked = true;
                ctrl.trazaPago.MedioDePago = medioPago;
                ctrl.seleccionarMedioPago();
                ctrl.cambioTrazaPago();
            }

            ctrl.seleccionarMedioPago = function () {
                if (ctrl.trazaPago.MedioDePago.Id == ctrl.parametros.IdMedioDePagoEfectivo) {
                    ctrl.seleccionarMedioPagoEfectivo();
                } else if (ctrl.trazaPago.MedioDePago.Id == ctrl.parametros.IdMedioDePagoTarjetaCredito) {
                    ctrl.seleccionarMedioPagoTarjetaDeCredito();
                } else if (ctrl.trazaPago.MedioDePago.Id == ctrl.parametros.IdMedioDePagoTarjetaDebito) {
                    ctrl.seleccionarMedioPagoTarjetaDebito();
                } else if (ctrl.trazaPago.MedioDePago.Id == ctrl.parametros.IdMedioDePagoDepositoEnCuenta) {
                    ctrl.seleccionarMedioPagoDepositoEnCuenta();
                } else if (ctrl.trazaPago.MedioDePago.Id == ctrl.parametros.IdMedioDePagoTransferencia) {
                    ctrl.seleccionarMedioPagoTransferencia();
                } else if (ctrl.trazaPago.MedioDePago.Id == ctrl.parametros.IdMedioDePagoPuntos) {
                    ctrl.seleccionarMedioPagoPuntos();
                }
            }

            ctrl.seleccionarMedioPagoEfectivo = function () {
                ctrl.trazaPago.Info.ImporteAPagar = ctrl.trazaPago.Info.ImporteAPagar == undefined ? '0.00' : parseFloat(ctrl.trazaPago.Info.ImporteAPagar).toFixed(2);
                ctrl.trazaPago.Info.ImporteEntregado = parseFloat(ctrl.trazaPago.Info.ImporteAPagar).toFixed(2);
                ctrl.trazaPago.Info.Observacion = '';
                ctrl.calcularEfectivo();
            }

            ctrl.seleccionarMedioPagoTarjetaDeCredito = function () {
                ctrl.trazaPago.Info.EntidadFinanciera = ctrl.entidadesFinancieras[0];
                ctrl.trazaPago.Info.OperadorTarjeta = ctrl.operadoresDeTarjeta[0];
                ctrl.trazaPago.Info.NumeroOperacion = '';
                $timeout(function () { $('#idEntidadFinancera').trigger("change"); }, 10);
                $timeout(function () { $('#idTipoTarjeta').trigger("change"); }, 10);
            }

            ctrl.seleccionarMedioPagoTarjetaDebito = function () {
                ctrl.trazaPago.Info.EntidadFinanciera = ctrl.entidadesFinancieras[0];
                ctrl.trazaPago.Info.OperadorTarjeta = ctrl.operadoresDeTarjeta[0];
                ctrl.trazaPago.Info.NumeroOperacion = '';
                $timeout(function () { $('#idEntidadFinancera').trigger("change"); }, 10);
                $timeout(function () { $('#idTipoTarjeta').trigger("change"); }, 10);
            }


            ctrl.seleccionarMedioPagoDepositoEnCuenta = function () {
                ctrl.trazaPago.Info.CuentaBancaria = ctrl.cuentasBancarias[0];
                ctrl.trazaPago.Info.NumeroOperacion = '';
                $timeout(function () { $('#cuentaBancaria').trigger("change"); }, 10);
            }


            ctrl.seleccionarMedioPagoTransferencia = function () {
                ctrl.trazaPago.Info.CuentaBancaria = ctrl.cuentasBancarias[0];
                ctrl.trazaPago.Info.NumeroOperacion = '';
                $timeout(function () { $('#cuentaBancaria').trigger("change"); }, 10);
            }

            ctrl.seleccionarMedioPagoPuntos = function () {
                ctrl.obtenerPuntosDeCliente().then(function () {
                    ctrl.cargarPuntos();
                    ctrl.cambioTrazaPago();
                }, function (error) {
                    ctrl.mensaje = "Se ha producido un error al obtener el dato:" + error;
                });
            }

            ctrl.obtenerPuntosDeCliente = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                if (ctrl.idCliente != undefined || ctrl.idCliente != null) {
                    ventaService.obtenerPuntosDeCliente({ idCliente: ctrl.idCliente }).success(function (data) {
                        ctrl.trazaPago.Info.PuntosPorCanjear = data.PuntosPorCanjear;
                        ctrl.trazaPago.Info.ValorPuntosPorCanjear = data.ValorPuntosPorCanjear;
                        ctrl.trazaPago.Info.ValorDeUnPuntoComoMedioDePago = data.ValorDeUnPuntoComoMedioDePago;
                        defered.resolve();
                    }).error(function (data, status) {
                        SweetAlert.error2(data);
                        defered.reject(data);
                    });
                }// else {
                //    ctrl.trazaPago.Info.PuntosPorCanjear = 0;
                //    ctrl.trazaPago.Info.ValorPuntosPorCanjear = 0;
                //    ctrl.trazaPago.Info.ValorDeUnPuntoComoMedioDePago = 0;
                //    defered.resolve();
                //}
                return promise;
            }

            ctrl.cargarPuntos = function () {
                ctrl.trazaPago.Info.MontoCajeado = ctrl.trazaPago.Info.ImporteAPagar;
                ctrl.trazaPago.Info.Observacion = '';
                ctrl.calcularPuntos();
            }

            ctrl.verificarMediosDePagoAMostrar = function (idCliente) {
                if (ctrl.trazaPago.MedioDePago.Id == ctrl.parametros.IdMedioDePagoPuntos) {
                    ctrl.seleccionarMedioDePago(ctrl.parametros.IdMedioDePagoEfectivo);
                }
                if (idCliente == ctrl.parametros.IdClienteGenerico) {
                    $('#medioPago-' + ctrl.externalId + "-" + ctrl.parametros.IdMedioDePagoPuntos).hide();
                    $('#labelMedioPago-' + ctrl.externalId + "-" + ctrl.parametros.IdMedioDePagoPuntos).hide();
                } else {
                    $('#medioPago-' + ctrl.externalId + "-" + ctrl.parametros.IdMedioDePagoPuntos).show();
                    $('#labelMedioPago-' + ctrl.externalId + "-" + ctrl.parametros.IdMedioDePagoPuntos).show();
                }
            }

            ctrl.calcularEfectivo = function () {
                ctrl.trazaPago.Info.Vuelto = parseFloat(parseFloat(ctrl.trazaPago.Info.ImporteEntregado) - parseFloat(ctrl.trazaPago.Info.ImporteAPagar)).toFixed(2);
            }

            ctrl.calcularPuntos = function () {
                ctrl.trazaPago.Info.PuntosCajeados = ctrl.trazaPago.Info.MontoCajeado / ctrl.trazaPago.Info.ValorDeUnPuntoComoMedioDePago;
                ctrl.trazaPago.Info.PuntosPendientes = ctrl.trazaPago.Info.PuntosPorCanjear - ctrl.trazaPago.Info.PuntosCajeados;
                ctrl.trazaPago.Info.ValorPuntosPendientes = ctrl.trazaPago.Info.PuntosPendientes * ctrl.trazaPago.Info.ValorDeUnPuntoComoMedioDePago;

            }

            ctrl.recalcularValoresMedioPago = function () {
                if (ctrl.trazaPago.MedioDePago.Id == ctrl.parametros.IdMedioDePagoEfectivo) {
                    ctrl.seleccionarMedioPagoEfectivo();
                } else if (ctrl.trazaPago.MedioDePago.Id == ctrl.parametros.IdMedioDePagoPuntos) {
                    ctrl.cargarPuntos();
                }
            }

            ctrl.setImporteAPagar = function (importe) {
                ctrl.trazaPago.Info.ImporteAPagar = importe;
                ctrl.recalcularValoresMedioPago();
            };

            ctrl.formatoDosDecimales = function (event, $filter) {
                let valor = event.target.value;
                event.target.value = parseFloat(valor).toFixed(2);
            }

            ctrl.$onInit = function () {
                ctrl.api = {};
                ctrl.inicializar();
                ctrl.api.SetImporteAPagar = ctrl.setImporteAPagar;
                ctrl.api.VerificarMediosDePagoAMostrar = ctrl.verificarMediosDePagoAMostrar;
                ctrl.api.SeleccionarMedioDePagoPorDefecto = ctrl.seleccionarMedioDePagoPorDefecto;
            };
        }
    });
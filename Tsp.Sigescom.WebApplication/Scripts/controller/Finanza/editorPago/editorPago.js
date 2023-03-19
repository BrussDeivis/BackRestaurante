angular.
    module('app').
    component('editorPago', {
        templateUrl: "../Scripts/controller/finanza/editorPago/editorPago.html",
        bindings: {
            api: '=',
            externalId: '<',
            pago: '=',
            idCliente: '<',
            idMedioPagoDefault: '<',
            permitirVentaAlCredito: '<',
            changed: '&',
            inicioRealizado: '&',
        },
        controller: function ($q, $scope, $timeout, $compile, SweetAlert, ventaService, maestroService, finanzaService) {
            var ctrl = this;

            ctrl.cambioPago = function () {
                ctrl.changed({ pago: ctrl.pago });
            };

            ctrl.inicioTerminado = function () {
                ctrl.inicioRealizado();
            };

            ctrl.inicializar = function () {
                ctrl.establecerDatosPorDefecto();
                ctrl.cargarParametros().then(function (result) {
                    ctrl.inicializacionRealizada = true;
                }, function (error) {
                    SweetAlert.error("Ocurrio un Problema", error);
                });
            };

            ctrl.cargarParametros = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                ventaService.obtenerParametrosParaPago({}).success(function (data) {
                    ctrl.parametros = data.data;
                    defered.resolve();
                }).error(function (data) {
                    SweetAlert.error("Ocurrio un Problema", data.error);
                    defered.reject(data);
                });
                return promise;
            };

            ctrl.establecerDatosPorDefecto = function () {
                ctrl.editorTrazaPagoAPI.SeleccionarMedioDePagoPorDefecto();
                ctrl.inicioTerminado();
            };

            ctrl.seleccionarContado = function () {
                ctrl.pago.Inicial = (0).toFixed(2);
                ctrl.pago.Traza.Info.ImporteAPagar = ctrl.pago.Total;
                ctrl.editorTrazaPagoAPI.SeleccionarMedioDePagoPorDefecto();
                ctrl.cambioPago();
            }

            ctrl.seleccionarCreditoRapido = function () {
                ctrl.pago.Inicial = (0).toFixed(2);
                ctrl.editorTrazaPagoAPI.SeleccionarMedioDePagoPorDefecto();
                ctrl.cambioPago();
            }

            ctrl.seleccionarCreditoConfigurado = function () {
                ctrl.pago.Inicial = (0).toFixed(2);
                ctrl.editorTrazaPagoAPI.SeleccionarMedioDePagoPorDefecto();
                ctrl.financiamientoConfigurado();
                ctrl.cambioPago();
            }

            ctrl.financiamientoConfigurado = function () {
                ctrl.financiamientoRealizado = false;
                ctrl.registradorFinanciamientoAPI.IniciarFinanciamiento(ctrl.pago.Total);
            };

            ctrl.resolverPagoInicialCredito = function () {
                ctrl.pago.Traza.Info.ImporteAPagar = ctrl.pago.ModoDePago == ctrl.parametros.ModoPagoContado ? ctrl.pago.Total : ctrl.pago.Inicial;
                ctrl.editorTrazaPagoAPI.SeleccionarMedioDePagoPorDefecto();

            }

            ctrl.cambioFinanciamiento = function (financiamiento) {
                if (financiamiento.financiamientoRealizado == true) {
                    if (financiamiento.inicial > 0) {
                        ctrl.pago.Inicial = financiamiento.inicial;
                        ctrl.pago.Traza.Info.ImporteAPagar = ctrl.pago.Inicial;
                        ctrl.resolverPagoInicialCredito();
                    }
                    ctrl.pago.Cuotas = financiamiento.cuotas;
                } else {
                    ctrl.pago.Cuotas = undefined;
                    ctrl.pago.ModoDePago = 1;
                    ctrl.pago.Traza.Info.ImporteAPagar = ctrl.pago.Total;
                    ctrl.seleccionarContado();
                }
                ctrl.cambioPago();
            };

            ctrl.verificarMediosDePagoAMostrar = function (idCliente) {
                ctrl.editorTrazaPagoAPI.VerificarMediosDePagoAMostrar(idCliente);
            }

            ctrl.setImporteAPagar = function (importe) {
                ctrl.pago.Total = importe;
                ctrl.editorTrazaPagoAPI.SetImporteAPagar(ctrl.pago.Total);
                if (ctrl.pago.ModoDePago == 3) {
                    ctrl.registradorFinanciamientoAPI.ReiniciarFinanciamiento(ctrl.pago.Total);
                }
            };

            ctrl.formatoDosDecimales = function (event, $filter) {
                let valor = event.target.value;
                event.target.value = parseFloat(valor).toFixed(2);
            }

            ctrl.inicioRealizadoTrazaPago = function (importe) {
                ctrl.inicializar();
            };

            ctrl.restablecerInfoPago = function () {
                ctrl.pago.ModoDePago = ctrl.parametros.ModoPagoContado;
                ctrl.pago.Inicial = (0).toFixed(2);
                ctrl.pago.Traza.Info.ImporteAPagar = (0).toFixed(2);
                ctrl.editorTrazaPagoAPI.SeleccionarMedioDePagoPorDefecto();
                ctrl.cambioPago();
            }

            ctrl.cambioTrazaPago = function () {
                ctrl.cambioPago();
            };

            ctrl.$onInit = function () {
                ctrl.api = {};
                ctrl.api.SetImporteAPagar = ctrl.setImporteAPagar;
                ctrl.api.VerificarMediosDePagoAMostrar = ctrl.verificarMediosDePagoAMostrar;
                ctrl.api.RestablecerInfoPago = ctrl.restablecerInfoPago;
                //ctrl.inicializar();
            };
        }
    });
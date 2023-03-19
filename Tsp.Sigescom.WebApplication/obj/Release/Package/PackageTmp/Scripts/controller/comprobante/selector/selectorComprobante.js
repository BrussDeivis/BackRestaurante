angular.
    module('app').
    component('selectorComprobante', {
        templateUrl: "../Scripts/controller/comprobante/selector/selectorComprobante.html",
        bindings: {
            api: '=',
            externalId: '<',
            comprobante: '=',
            comprobantePara: '<',
            idComprobanteDefault: '<',
            changed: '&',
            serieComprobanteEnter: '&'
        },

        controller: function ($q, $scope, $timeout, SweetAlert, ventaService, configuracionService) {
            $('select:not(.normal)').each(function () {
                $(this).select2({
                    dropdownParent: $(this).parent()
                });
            });
            var ctrl = this;

            ctrl.comprobanteChanged = function () {
                ctrl.changed({ comprobante: ctrl.comprobante });
            };

            ctrl.serieComprobante_enter = function () {
                ctrl.serieComprobanteEnter({});
            };

            ctrl.cargarTiposDeComprobantes = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                configuracionService.obtenerSelectoresTiposDeComprobante({ comprobantePara: ctrl.comprobantePara }).success(function (data) {
                    ctrl.tiposDeComprobantes = data;
                    ctrl.selecccionarTipo(ctrl.idComprobanteDefault);
                    defered.resolve();
                }).error(function (data) {
                    SweetAlert.error("Ocurrio un Problema", data.error);
                    defered.reject(data);
                });
                return promise;
            };

            ctrl.selecccionarTipo = function (idTipo) {
                if (ctrl.tiposDeComprobantes != undefined) {
                    if (ctrl.tiposDeComprobantes.find(tc => tc.TipoComprobante.Id === idTipo) != undefined) {
                        ctrl.tipoSeleccionado = ctrl.tiposDeComprobantes.find(tc => tc.TipoComprobante.Id === idTipo);
                        $timeout(function () { $('.tipoDocumento').trigger("change"); }, 100);
                        ctrl.cambioTipoComprobante();
                    }
                }
            };

            ctrl.cambioTipoComprobante = function () {
                if (ctrl.comprobante != undefined && ctrl.tipoSeleccionado != undefined) {
                    ctrl.comprobante.Tipo = ctrl.tipoSeleccionado.TipoComprobante;
                    ctrl.comprobante.Serie = ctrl.tipoSeleccionado.Series[0];
                    $timeout(function () { $('#radio-' + ctrl.externalId + '-0').trigger("change"); }, 100);
                    //$timeout(function () { $('.tipoDocumento').trigger("change"); }, 100);
                    ctrl.comprobanteChanged(); 
                }
            };

            ctrl.verificarAutonumerable = function (serieSeleccionada) {
                ctrl.serieSeleccionaesEsAutonumerica = serieSeleccionada.EsAutonumerica;
            };

            ctrl.setFocusSerieComprobante = function () {
                if (ctrl.tipoSeleccionado.MostrarSelectorSerie) {
                    $timeout(function () { $('#radio-' + ctrl.externalId + '-0').trigger("focus"); }, 100);
                } else {
                    ctrl.serieComprobante_enter();
                }
            };

            this.$onInit = function () {
                ctrl.api = {};
                ctrl.cargarTiposDeComprobantes();
                ctrl.api.SelecccionarTipo = ctrl.selecccionarTipo;
                ctrl.api.SetFocusSerieComprobante = ctrl.setFocusSerieComprobante;
            };
        }
    });
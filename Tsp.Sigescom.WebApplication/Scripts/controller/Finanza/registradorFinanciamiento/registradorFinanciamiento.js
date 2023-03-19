angular.
    module('app').
    component('registradorFinanciamiento', {
        templateUrl: "../Scripts/controller/finanza/registradorFinanciamiento/registradorFinanciamiento.html",
        bindings: {
            api: '=',
            changed: '&',
            externalId: '<',
            fechaActual: '<'
        },
        controller: function ($scope, ventaService, maestroService) {
            var ctrl = this;
            var scope = $scope.$root;
            $('select:not(.normal)').each(function () {
                $(this).select2({
                    dropdownParent: $(this).parent()
                });
            });

            ctrl.financiamientoChanged = function () {
                ctrl.changed({ financiamiento: ctrl.financiamiento });
            };

            ctrl.diasDeVencimiento = [{ id: 1, nombre: "1 de cada mes" }, { id: 2, nombre: "2 de cada mes" }, { id: 3, nombre: "3 de cada mes" }, { id: 4, nombre: "4 de cada mes" }, { id: 5, nombre: "5 de cada mes" }, { id: 6, nombre: "6 de cada mes" }, { id: 7, nombre: "7 de cada mes" }, { id: 8, nombre: "8 de cada mes" }, { id: 9, nombre: "9 de cada mes" }, { id: 10, nombre: "10 de cada mes" },
            { id: 11, nombre: " 11 de cada mes" }, { id: 12, nombre: "12 de cada mes" }, { id: 13, nombre: "13 de cada mes" }, { id: 14, nombre: "14 de cada mes" }, { id: 15, nombre: "15 de cada mes" }, { id: 16, nombre: "16 de cada mes" }, { id: 17, nombre: "17 de cada mes" }, { id: 18, nombre: "18 de cada mes" }, { id: 19, nombre: "19 de cada mes" }, { id: 20, nombre: "20 de cada mes" },
            { id: 21, nombre: "21 de cada mes" }, { id: 22, nombre: "22 de cada mes" }, { id: 23, nombre: "23 de cada mes" }, { id: 24, nombre: "24 de cada mes" }, { id: 25, nombre: "25 de cada mes" }, { id: 26, nombre: "26 de cada mes" }, { id: 27, nombre: "27 de cada mes" }, { id: 28, nombre: "28 de cada mes" }];

            ctrl.iniciarFinanciamiento = function (importeAPagar) {
                ctrl.limpiarFinanciamiento(importeAPagar);
                $('#registro-financiamiento-'+ ctrl.externalId).modal('show');
                $(function () {
                    $('.td-datepicker').datepicker({
                        format: 'dd/mm/yyyy',
                        autoclose: true,
                        todayHighlight: true,
                        language: 'es'
                    });
                });
            };

            ctrl.limpiarFinanciamiento = function (importeAPagar) {
                ctrl.totalAPagar = importeAPagar;
                ctrl.financiamiento = { cuota: 1, inicial: 0, capital: ctrl.totalAPagar, total: ctrl.totalAPagar };
                ctrl.financiamiento.cuotas = [];
                ctrl.financiamiento.financiamientoRealizado = false;
            };

            ctrl.calcularTotalFinanciamientoPorcentaje = function () {
                ctrl.financiamiento.total = (ctrl.financiamiento.inicial > 0 || ctrl.financiamiento.inicial != "") ? (ctrl.totalAPagar - ctrl.financiamiento.inicial).toFixed(2) : (ctrl.totalAPagar).toFixed(2);
                ctrl.limpiarCronogramaDePagos();
            };

            ctrl.calcularTotalFinanciamientoMonto = function () {
                ctrl.financiamiento.total = (ctrl.financiamiento.inicial > 0 || ctrl.financiamiento.inicial != "") ? (ctrl.totalAPagar - ctrl.financiamiento.inicial).toFixed(2) : (ctrl.totalAPagar).toFixed(2);
                ctrl.limpiarCronogramaDePagos();
            };

            ctrl.limpiarCronogramaDePagos = function () {
                if (ctrl.financiamiento.cuota > 1) {
                    ctrl.financiamiento.fechaRegistro = undefined;
                } else {
                    if (ctrl.financiamiento.cuota == 1) {
                        ctrl.financiamiento.diavencimiento = undefined;
                    }
                }
                ctrl.financiamiento.cuotas = [];
            };

            ctrl.generarCuota = function () {
                ctrl.financiamiento.cuotas = [];
                ctrl.cuota = {};
                let numeroCuotas = ctrl.financiamiento.cuota;
                let arrayDeFechaRegistro = ctrl.fechaActual.split("/");
                let anioHora = arrayDeFechaRegistro[2].split(" ");
                let anio = anioHora[0];
                let mes = arrayDeFechaRegistro[1];
                let dia = ctrl.financiamiento.diavencimiento;
                let inicial = parseFloat(ctrl.financiamiento.inicial);
                if (inicial != ctrl.totalAPagar) {
                    if (inicial > 0) {
                        ctrl.cuota = { CapitalCuota: ctrl.financiamiento.inicial, InteresCuota: 0, ImporteCuota: ctrl.financiamiento.inicial, FechaVencimiento: scope.formatDate(new Date(), "ES"), EsCuotaInicial: true }
                        ctrl.financiamiento.cuotas.push(ctrl.cuota);
                        ctrl.cuota = {};
                    }
                    if (numeroCuotas == 1) {
                        ctrl.cuota.CapitalCuota = parseFloat(parseFloat(ctrl.financiamiento.total) / parseFloat(numeroCuotas)).toFixed(2);
                        ctrl.cuota.InteresCuota = 0;
                        ctrl.cuota.ImporteCuota = parseFloat(parseFloat(ctrl.financiamiento.total) / parseFloat(numeroCuotas)).toFixed(2);
                        ctrl.cuota.FechaVencimiento = ctrl.financiamiento.fechaRegistro;
                        ctrl.cuota.EsCuotaInicial = false;
                        ctrl.financiamiento.cuotas.push(ctrl.cuota);
                        ctrl.cuota = {};
                    } else {
                        var sumaCuotas = 0;
                        for (let i = 0; i < numeroCuotas; i++) {
                            
                            ctrl.cuota.CapitalCuota = parseFloat(parseFloat(ctrl.financiamiento.total) / parseFloat(numeroCuotas)).toFixed(2);
                            ctrl.cuota.InteresCuota = 0;
                            ctrl.cuota.ImporteCuota = parseFloat(parseFloat(ctrl.financiamiento.total) / parseFloat(numeroCuotas)).toFixed(2);
                            sumaCuotas += parseFloat(ctrl.cuota.CapitalCuota);
                            if (mes == 13) {
                                mes = 1;
                                anio++;
                            }
                            if (mes == 2 && (dia == 28 || dia == 29)) {
                                ctrl.cuota.FechaVencimiento = scope.formatDate(new Date(anio, mes, 0), "ES");
                            } else if ((mes == 1 || mes == 3 || mes == 5 || mes == 7 || mes == 8 || mes == 10 || mes == 12) && dia == 31) {
                                ctrl.cuota.FechaVencimiento = scope.formatDate(new Date(anio, mes, 0), "ES");
                            } else {
                                ctrl.cuota.FechaVencimiento = scope.formatDate(new Date(anio, mes, dia), "ES");
                            }
                            ctrl.cuota.EsCuotaInicial = false;
                            ctrl.financiamiento.cuotas.push(ctrl.cuota);
                            ctrl.cuota = {};
                            mes++;
                        }
                        var numeroTotalCuotas = ((inicial > 0) ? 1 : 0) + numeroCuotas;
                        ctrl.financiamiento.cuotas[numeroTotalCuotas - 1].ImporteCuota = ctrl.financiamiento.cuotas[numeroTotalCuotas - 1].CapitalCuota = parseFloat(ctrl.financiamiento.cuotas[numeroTotalCuotas - 1].CapitalCuota) + (parseFloat(ctrl.financiamiento.total) - parseFloat(sumaCuotas))

                    }
                }
            };

            ctrl.formatoDosDecimales = function (event, $filter) {
                let valor = event.target.value;
                event.target.value = parseFloat(valor).toFixed(2);
            }

            ctrl.deshabilitarBotonGenerar = function (fechaRegistro, diavencimiento) {
                return fechaRegistro == undefined && diavencimiento == undefined;
            };

            ctrl.registrarFinanciamiento = function () {
                ctrl.financiamiento.financiamientoRealizado = true;
                ctrl.financiamientoChanged();
            };

            ctrl.reiniciarFinanciamiento = function (importeAPagar) {
                ctrl.totalAPagar = importeAPagar;
                $('#modal-reiniciar-financiamiento-' + ctrl.externalId).modal('show');
            };

            ctrl.finalizarFinanciamiento = function () {
                ctrl.financiamiento = {};
                ctrl.financiamiento.financiamientoRealizado = false;
                ctrl.financiamientoChanged();
            };

            ctrl.recalcularFinanciamiento = function () {
                ctrl.iniciarFinanciamiento(ctrl.totalAPagar);
            }

            ctrl.$onInit = function () {
                ctrl.api = {};
                ctrl.api.IniciarFinanciamiento = ctrl.iniciarFinanciamiento;
                ctrl.api.ReiniciarFinanciamiento = ctrl.reiniciarFinanciamiento;
            };
        }
    });
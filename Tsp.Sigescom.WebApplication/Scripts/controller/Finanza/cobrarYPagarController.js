app.controller('cobrarYPagarController', function ($scope, SweetAlert, $rootScope, $timeout, $q, DTOptionsBuilder, DTColumnDefBuilder, finanzaService, maestroService, actorComercialService) {

    $rootScope.dtOptions.withLightColumnFilter({
        '2': { type: 'text', className: 'form-control padding-left-right-3' },
        '3': { type: 'text', className: 'form-control padding-left-right-3' },
        '4': { type: 'text', className: 'form-control padding-left-right-3' },
        '5': { type: 'text', className: 'form-control padding-left-right-3' },
        '6': { type: 'text', className: 'form-control padding-left-right-3' },
        '7': { type: 'text', className: 'form-control padding-left-right-3' },
        '8': { type: 'text', className: 'form-control padding-left-right-3' },
        '9': { type: 'text', className: 'form-control padding-left-right-3' },
        '10': { type: 'text', className: 'form-control padding-left-right-3' },
    });

    $scope.inicializar = function () {
        $scope.limpiarRegistro();
        $scope.cargarParametros();
        $scope.cargarColeccionesAsync();
        $scope.cargarColeccionesSync().then(function (resultado_) {
            $scope.obtenerCuentasPorCobrarPagar();
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
    };

    $scope.limpiarRegistro = function () {
        $scope.grupos = {};
        $scope.grupos.TodosLosGrupos = true;
        $scope.grupos.GruposSeleccionados = [];
        $scope.grupos.SaldoTotalGrupo = 0;
        $scope.cuotasSeleccionadas = [];
        $scope.movimiento = {
            Traza: {}
        };
    };

    $scope.cargarParametros = function () {
        $scope.idDetalleMaestroMedioDePagoEfectivo = idDetalleMaestroMedioDePagoEfectivo;
        $scope.idDetalleMaestroEntidadBancariaNinguna = idDetalleMaestroEntidadBancariaNinguna;
        $scope.idDetalleMaestroMedioDePagoDepositoCuenta = idDetalleMaestroMedioDePagoDepositoCuenta;
        $scope.idDetalleMaestroMedioDePagoTransferenciaDeFondos = idDetalleMaestroMedioDePagoTransferenciaDeFondos;
        $scope.fechaInicio = fechaInicio;
        $scope.fechaFin = fechaFin;
        $scope.porCobrar = 'true';
        $scope.mostrarSeleccionCuentaBancaria = false;
        $scope.permitirGruposEnCuentasPorCobrarPagar = permitirGruposEnCuentasPorCobrarPagar;
    };

    $scope.cargarColeccionesAsync = function () {
        $scope.obtenerMediosDePago();
        $scope.obtenerEntidadesFinancieras();
    };

    $scope.cargarColeccionesSync = function () {
        var defered = $q.defer();
        var promiseList = [];
        promiseList.push($scope.obtenerGruposActoresComerciales());
        return $q.all(promiseList).then(function (response) {
            defered.resolve();
        }).catch(function (error) {
            defered.reject(e);
        });
    };

    $scope.obtenerCuentasPorCobrarPagar = function () {
        if ($scope.permitirGruposEnCuentasPorCobrarPagar) {
            $scope.obtenerCuotasPorGrupos();
        } else {
            $scope.obtenerCuotas();
        }
    };

    $scope.obtenerMediosDePago = function () {
        maestroService.obtenerMediosDePago({}).success(function (data) {
            $scope.mediosDePago = data;
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.seleccionMedioDePago = function () {
        if ($scope.movimiento.Traza.MedioDePago.Id == $scope.idDetalleMaestroMedioDePagoDepositoCuenta || $scope.movimiento.Traza.MedioDePago.Id == $scope.idDetalleMaestroMedioDePagoTransferenciaDeFondos) {
            $scope.mostrarSeleccionCuentaBancaria = true;
        } else {
            $scope.mostrarSeleccionCuentaBancaria = false;
        }
    }

    $scope.obtenerEntidadesFinancieras = function (idMedioDePago) {
        maestroService.obtenerEntidadesFinancieras({}).success(function (data) {
            $scope.entidadesFinancieras = data;
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.obtenerGruposActoresComerciales = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        actorComercialService.obtenerGruposActoresComerciales({}).success(function (data) {
            $scope.gruposActoresComerciales = data;
            defered.resolve();
        }).error(function (data) {
            SweetAlert.error("Ocurrio un Problema", data.error);
            defered.reject(data);
        });
        return promise;
    };

    $scope.seleccionEntidadFinanciera = function () {
        if ($scope.movimiento.Traza.MedioDePago.Id == $scope.idDetalleMaestroMedioDePagoDepositoCuenta || $scope.movimiento.Traza.MedioDePago.Id == $scope.idDetalleMaestroMedioDePagoTransferenciaDeFondos) {
            $scope.obtenerCuentasBancarias();
        }
        if ($scope.movimiento.Traza.EntidadFinanciera.Id == $scope.idDetalleMaestroEntidadBancariaNinguna) {
            $scope.movimiento.Traza.InformacionDePago = 'NINGUNO';
        } else {
            $scope.movimiento.Traza.InformacionDePago = '';
        }
    }

    $scope.obtenerCuentasBancarias = function () {
        finanzaService.obtenerCuentasBancariasPorEntidadFinanciera({ idEntidadFinanciera: $scope.movimiento.Traza.EntidadFinanciera.Id }).success(function (data) {
            $scope.cuentasBancarias = data;
            $scope.movimiento.Traza.CuentaBancaria = $scope.cuentasBancarias[0];
            $timeout(function () { $('#cuentaBancaria').trigger("change"); }, 100);
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.obtenerTiposDeComprobante = function (idTipoTransacion) {
        var defered = $q.defer();
        var promise = defered.promise;
        finanzaService.obtenerTiposDeComprobanteParaCobrarPagar({ porCobrar: $scope.porCobrar, idTipoTransaccion: idTipoTransacion }).success(function (data) {
            $scope.tiposDeComprobantes = data;
            defered.resolve();
        }).error(function (data) {
            SweetAlert.error2(data);
            defered.reject(data);
        });
        return promise;
    }

    $scope.cargarSeries = function (tipoComprobante) {
        if (tipoComprobante != null) {
            $scope.series = angular.copy(tipoComprobante.Series);
            $scope.movimiento.Comprobante.Serie = $scope.series[0];
            $timeout(function () { $('#serie').trigger("change"); }, 100);
        }
    }

    $scope.obtenerCuotas = function () {
        $scope.cuotasSeleccionadas = [];
        finanzaService.obtenerCuentasPorCobrarOPagar({ porCobrar: $scope.porCobrar }).success(function (data) {
            $scope.cuotas = data;
            $scope.establecerEtiquetas();
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.obtenerCuotasPorGrupos = function () {
        $scope.cuotasSeleccionadas = [];
        let idsGruposSeleccionados = [];
        $scope.grupos.GruposSeleccionados.forEach(g => { idsGruposSeleccionados.push(g.Id); });
        finanzaService.obtenerCuentasPorCobrarOPagarPorGrupos({ porCobrar: $scope.porCobrar, todosLosGrupos: $scope.grupos.TodosLosGrupos, idsGrupos: idsGruposSeleccionados }).success(function (data) {
            $scope.cuotas = data;
            $scope.grupos.SaldoTotalGrupo = 0;
            $scope.cuotas.forEach(g => { $scope.grupos.SaldoTotalGrupo += g.Saldo; });
            $scope.establecerEtiquetas();
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.establecerEtiquetas = function () {
        $scope.denominadorTercero = $scope.porCobrar == 'true' ? 'DEUDOR' : 'ACREEDOR';
        $scope.tituloModal = $scope.porCobrar == 'true' ? 'CUOTA POR COBRAR' : 'CUOTA POR PAGAR';
        $scope.denominadorAccion = $scope.porCobrar == 'true' ? 'COBRAR' : 'PAGAR';
    }

    $scope.todosLosGruposChanged = function () {
        if ($scope.grupos.TodosLosGrupos) {
            $scope.grupos.GruposSeleccionados = [];
            $timeout(function () { $('#gruposseleccionados').trigger("change"); }, 100);
        }
    }

    $scope.gruposChanged = function () {
        if ($scope.grupos.TodosLosGrupos != undefined) {
            $scope.grupos.TodosLosGrupos = !($scope.grupos.GruposSeleccionados.length > 0)
        }
    }

    $scope.verDetalleCuota = function (cuota) {
        finanzaService.obtenerCuotaOperacionDetalle({ idCuota: cuota.IdCuota }).success(function (data) {
            $scope.verCuota = data;
            document.getElementById("pdfDocumento").innerHTML = $scope.verCuota.CadenaHtmlDeComprobante;
            $timeout(function () { $('#pdfDocumento').trigger("change"); }, 100);
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.validarCuotasAPagar = function (cuotas) {
        $scope.mensajeAdvertencia = [];
        $scope.movimiento = angular.copy(cuotas[0]);
        $scope.movimiento.Traza = {};
        $scope.movimiento.Total = 0;
        $scope.movimiento.PagoCuota = [];
        $scope.nombreCajero = nombreCajero;
        var cont = 1;
        var text = "";
        var pagoOCobro = $scope.porCobrar ? "cobro" : "pago";

        $scope.movimiento.PagoCuota.push({ IdCuota: cuotas[0].IdCuota, CodigoCuota: cuotas[0].CodigoCuota, Saldo: parseFloat(cuotas[0].Saldo).toFixed(2), Importe: parseFloat(cuotas[0].Saldo).toFixed(2) });
        $scope.movimiento.Total = parseFloat(parseFloat($scope.movimiento.Total) + parseFloat(cuotas[0].Saldo)).toFixed(2);
        if (cuotas.length > 1) {
            for (var i = 1; i < cuotas.length; i++) {
                if ($scope.movimiento.IdActorComercial != cuotas[i].IdActorComercial) {
                    cont++;
                    text = "No se puede realizar el " + pagoOCobro + ", el cliente debe ser el mismo";
                    break;
                }
                if ($scope.movimiento.IdTipoTransaccion != cuotas[i].IdTipoTransaccion) {
                    cont++;
                    text = "No se puede realizar el " + pagoOCobro + ", la operacion debe ser la misma";
                    break;
                }
                $scope.movimiento.PagoCuota.push({ IdCuota: cuotas[i].IdCuota, CodigoCuota: cuotas[i].CodigoCuota, Saldo: parseFloat(cuotas[i].Saldo).toFixed(2), Importe: parseFloat(cuotas[i].Saldo).toFixed(2) });
                $scope.movimiento.Total = parseFloat($scope.movimiento.Total) + parseFloat(cuotas[i].Saldo);
            }
        } if (cont == 1) {
            $('#modal-cobrar-pagar-cuota').modal('show');
            $scope.obtenerTiposDeComprobante(cuotas[0].IdTipoTransaccion).then(function (resultado_) {
                $scope.establecerDatosPorDefectoParaCobrarPagar();
            }, function (error) {
                $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
            });

        } else if (cont >= 2) {
            $('#modal-cobrar-pagar-cuota').modal('hide');
            SweetAlert.warning("Advertencia", text);
            $scope.movimiento = {};
        }
    }

    $scope.validarNumero = function (index) {
        $scope.movimiento.PagoCuota[index].Importe = ($scope.movimiento.PagoCuota[index].Importe == undefined) ? '0' : $scope.movimiento.PagoCuota[index].Importe;
        if ($scope.movimiento.PagoCuota[index].Importe.split(".").length > 2) {
            var montos = $scope.movimiento.PagoCuota[index].Importe.split(".");
            $scope.movimiento.PagoCuota[index].Importe = montos[0] + '.' + montos[1];
        }
        $scope.movimiento.PagoCuota[index].Importe = parseFloat($scope.movimiento.PagoCuota[index].Importe).toFixed(2);
    }

    $scope.calcularTotal = function () {
        $scope.movimiento.Total = 0;
        for (var i = 0; i < $scope.movimiento.PagoCuota.length; i++) {
            $scope.movimiento.Total = parseFloat($scope.movimiento.Total) + parseFloat($scope.movimiento.PagoCuota[i].Importe);
        }
    }

    $scope.validarModal = function () {
        $scope.mensajeAdvertencia = [];
        for (var i = 0; i < $scope.movimiento.PagoCuota.length; i++) {
            if ($scope.movimiento.PagoCuota[i].Importe == undefined || parseFloat($scope.movimiento.PagoCuota[i].Importe) == 0 || parseFloat($scope.movimiento.PagoCuota[i].Importe) > parseFloat($scope.movimiento.PagoCuota[i].Saldo)) {
                $scope.mensajeAdvertencia.push('Es necesario que el monto sea correcto.');
                break;
            }
        }
        if ($scope.movimiento.Comprobante == undefined) {
            $scope.mensajeAdvertencia.push('Es necesario seleccionar un comprobante.');
        }
        if ($scope.movimiento.Traza.MedioDePago == undefined) {
            $scope.mensajeAdvertencia.push('Es necesario seleccionar un medio de pago.');
        }
        if ($scope.movimiento.Traza.EntidadFinanciera == undefined) {
            $scope.mensajeAdvertencia.push('Es necesario seleccionar una entidad financiera.');
        }
        if ($scope.mostrarSeleccionCuentaBancaria) {
            if ($scope.movimiento.Traza.CuentaBancaria == undefined) {
                $scope.mensajeAdvertencia.push('Es necesario seleccionar una cuenta bancaria.');
            }
        }
        if ($scope.movimiento.Traza.InformacionDePago == undefined) {
            $scope.mensajeAdvertencia.push('Es necesario ingresar la informacion de pago.');
        }
        if ($scope.movimiento.Traza.EntidadFinanciera.Id != $scope.idDetalleMaestroEntidadBancariaNinguna) {
            if ($scope.movimiento.Traza.InformacionDePago == undefined || $scope.movimiento.Traza.InformacionDePago == '') {
                $scope.mensajeAdvertencia.push('Es necesario ingresar la informacion de pago.');
            }
        }
    }

    $scope.seleccionarCuota = function (item) {
        var idx = $scope.cuotasSeleccionadas.indexOf(item);
        if (idx > -1) {
            $scope.cuotasSeleccionadas.splice(idx, 1);
        }
        else {
            $scope.cuotasSeleccionadas.push(item);
        }
    };

    $scope.establecerDatosPorDefectoParaCobrarPagar = function () {
        $scope.movimiento.Observacion = 'NINGUNA';
        $scope.movimiento.Traza.InformacionDePago = 'NINGUNA';
        $scope.establecerMedioDePagoPorDefecto();
        $scope.establecerTipoComprobantePorDefecto();
        $scope.validarModal();
    };

    $scope.establecerMedioDePagoPorDefecto = function () {
        var medioDePago = Enumerable.from($scope.mediosDePago)
            .where("$.Id == '" + $scope.idDetalleMaestroMedioDePagoEfectivo + "'").toArray()[0];
        $scope.movimiento.Traza.MedioDePago = medioDePago != null ? medioDePago : $scope.mediosDePago[0];
        $timeout(function () { $('#medioDePago').trigger("change"); }, 100);
    }

    $scope.establecerBancoPorDefecto = function () {
        if ($scope.movimiento.Traza.MedioDePago.Id == $scope.idDetalleMaestroMedioDePagoEfectivo) {
            var banco = Enumerable.from($scope.entidadesFinancieras)
                .where("$.Id == '" + $scope.idDetalleMaestroEntidadBancariaNinguna + "'").toArray()[0];
            $scope.movimiento.Traza.EntidadFinanciera = banco != null ? banco : $scope.entidadesFinancieras[0];
            $timeout(function () { $('#entidadFinanciera').trigger("change"); }, 100);
        }
    }

    $scope.establecerTipoComprobantePorDefecto = function () {
        if ($scope.tiposDeComprobantes != undefined && $scope.tiposDeComprobantes.length > 0) {
            $scope.movimiento.Traza.Comprobante = $scope.tiposDeComprobantes[0];
            $scope.cargarSeries($scope.movimiento.Traza.Comprobante);
            $timeout(function () { $('#tipoComprobante').trigger("change"); }, 100);
        }

    }

    $scope.cobrarPagarCuota = function () {
        finanzaService.cobrarPagarCuota({ movimiento: $scope.movimiento }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.limpiarRegistro();
            $scope.obtenerCuentasPorCobrarPagar();
            $('#modal-cobrar-pagar-cuota').modal('hide');
            $scope.cuotasSeleccionadas = [];
            jsWebClientPrint.print('idOperacion=' + data.data);
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

});
app.controller('gastoController', function ($scope, $q, $compile, $rootScope, SweetAlert, confirm, $timeout, blockUI, DTOptionsBuilder, DTColumnDefBuilder, gastoService, compraService, maestroService, conceptoService, productoService) {

    //#region Bandeja de Gastos 

    $scope.inicializarBandejaDeGastos = function () {
        $scope.limpiarGasto();
        $scope.cargarParametros();
        $scope.cargarColeccionesAsync();
        $scope.inicializarComponentes();
        $scope.listarBandeja();
    }

    $scope.cargarParametros = function () {
        $scope.fechaInicio = fechaInicio;
        $scope.fechaFin = fechaFin;
        $scope.fechaActual = fechaActual;
        $scope.tasaIGV = tasaIGV;
        $scope.aplicaLeyAmazonia = aplicaLeyAmazonia;
        $scope.idTipoActorPersonaNatural = idTipoActorPersonaNatural;
        $scope.idTipoActorPersonaJuridica = idTipoActorPersonaJuridica;
        $scope.idProveedorGenerico = idProveedorGenerico;
        $scope.idTipoPersonaSeleccionadaPorDefecto = idTipoPersonaSeleccionadaPorDefecto;
        $scope.idTipoDocumentoSeleccionadaConTipoPersonaNatural = idTipoDocumentoSeleccionadaConTipoPersonaNatural;
        $scope.idTipoDocumentoSeleccionadaConTipoPersonaJuridica = idTipoDocumentoSeleccionadaConTipoPersonaJuridica;
        $scope.idTipoDocumentoIdentidadDni = idTipoDocumentoIdentidadDni;
        $scope.idTipoDocumentoIdentidadRuc = idTipoDocumentoIdentidadRuc;
        $scope.idDetalleMaestroCatalogoDocumentoFactura = idDetalleMaestroCatalogoDocumentoFactura;
        $scope.diaVencimiento = {
            Lista: [{ id: 1, nombre: "1 de cada mes" }, { id: 2, nombre: "2 de cada mes" }, { id: 3, nombre: "3 de cada mes" }, { id: 4, nombre: "4 de cada mes" }, { id: 5, nombre: "5 de cada mes" }, { id: 6, nombre: "6 de cada mes" }, { id: 7, nombre: "7 de cada mes" }, { id: 8, nombre: "8 de cada mes" }, { id: 9, nombre: "9 de cada mes" }, { id: 10, nombre: "10 de cada mes" },
            { id: 11, nombre: " 11 de cada mes" }, { id: 12, nombre: "12 de cada mes" }, { id: 13, nombre: "13 de cada mes" }, { id: 14, nombre: "14 de cada mes" }, { id: 15, nombre: "15 de cada mes" }, { id: 16, nombre: "16 de cada mes" }, { id: 17, nombre: "17 de cada mes" }, { id: 18, nombre: "18 de cada mes" }, { id: 19, nombre: "19 de cada mes" }, { id: 20, nombre: "20 de cada mes" },
            { id: 21, nombre: "21 de cada mes" }, { id: 22, nombre: "22 de cada mes" }, { id: 23, nombre: "23 de cada mes" }, { id: 24, nombre: "24 de cada mes" }, { id: 25, nombre: "25 de cada mes" }, { id: 26, nombre: "26 de cada mes" }, { id: 27, nombre: "27 de cada mes" }, { id: 28, nombre: "28 de cada mes" },]
        };
        $scope.tiempoEsperaBusquedaSelector = tiempoEsperaBusquedaSelector;
        $scope.minimoCaracteresBuscarActorComercial = minimoCaracteresBuscarActorComercial;
        $scope.mascaraDeVisualizacionValidacionRegistroProveedor = mascaraDeVisualizacionValidacionRegistroProveedor;
        $scope.permitirSeleccionarGrupoProveedor = permitirSeleccionarGrupoProveedor;
    }

    $scope.cargarColeccionesAsync = function () {
        $scope.obtenerTiposDeComprobante();
        $scope.obtenerConceptos();
        $scope.obtenerProveedores();
    }

    $scope.inicializarComponentes = function () {
        $scope.rolProveedor = { Id: idRolProveedor, Nombre: 'PROVEEDOR' };
        $scope.inicializacionRealizada = true;
    }

    $scope.listarBandeja = function () {
        if ($scope.fechaInicio != "" && $scope.fechaFin != "") {
            gastoService.obtenerGastos({ desde: $scope.fechaInicio, hasta: $scope.fechaFin }).success(function (data) {
                $scope.listaGastos = data;
            }).error(function (data) {
                SweetAlert.error("Ocurrio un Problema", data.error);
            });
        }
    }

    $rootScope.dtOptions.withLightColumnFilter({
        '1': { type: 'text', className: 'form-control padding-left-right-3' },
        '2': { type: 'text', className: 'form-control padding-left-right-3' },
        '3': { type: 'text', className: 'form-control padding-left-right-3' },
        '4': { type: 'text', className: 'form-control padding-left-right-3' },
        '5': { type: 'text', className: 'form-control padding-left-right-3' },
        '6': { type: 'text', className: 'form-control padding-left-right-3' },
        '7': { type: 'text', className: 'form-control padding-left-right-3' },
    });


    $scope.cambioProveedor = function (actorComercial) {
        $scope.gasto.Proveedor = actorComercial;
        $scope.validarGasto();
    };

    $scope.obtenerGasto = function (item) {
        gastoService.obtenerGasto({ idOrdenGasto: item.Id }).success(function (data) {
            $scope.verGasto = data;
        }).error(function (data) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }

    $scope.cerrarVerGasto = function (item) {
        $scope.verGasto = {};
    }


    $scope.iniciarInvalidarGasto = function (item) {
        $scope.idOrdenGastoAInvalidar = item.Id;
    }

    $scope.invalidarGasto = function () {
        gastoService.invalidarGasto({ idOrden: $scope.idOrdenGastoAInvalidar, observacion: $scope.invalidacion.observacion }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $('#modal-invalidar-gasto').modal('hide');
            $scope.invalidacion = {};
            $scope.listarBandeja();
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema", data.error);
        });
    }
    //#endregion

    //#region Registro Gasto
    $scope.nuevoRegistroGasto = function () {
        $scope.limpiarGasto();
        $scope.establecerDatosPorDefecto();
    }

    $scope.limpiarGasto = function () {
        $scope.gasto = {
            Proveedor: {} };
    }

    $scope.establecerDatosPorDefecto = function () {
        $scope.gasto = { Observacion: "NINGUNA", EsGastoACredito: false, EsCreditoRapido: {}, Cuotas: [], Importe: 0.00, Igv: 0.00, Total: 0.00 };
        $scope.financiamientoRealizado = false;
        $scope.gasto.FechaEmision = $scope.fechaActual.split(" ")[0];
        $scope.gasto.Proveedor = $scope.proveedores.find(p => p.Id === $scope.idProveedorGenerico);
        $scope.gasto.TipoDeComprobante = $scope.tiposDeComprobantesMasSeries[0];
        $scope.cargarSeries($scope.gasto.TipoDeComprobante);
        $scope.gasto.Concepto = $scope.conceptosDeGasto[0];
        if (!$scope.aplicaLeyAmazonia) { $scope.gasto.GrabaIgv = true; }
        $scope.selectorProveedorAPI.EstablecerActorPorDefecto();
        $scope.validarGasto();
        $timeout(function () { $('#comboProveedor').trigger("change"); }, 100);
        $timeout(function () { $('#tipoDocumento').trigger("change"); }, 100);
        $timeout(function () { $('#concepto').trigger("change"); }, 100);
    }

    $scope.obtenerTiposDeComprobante = function () {
        gastoService.obtenerTiposDeComprobanteParaGasto({}).success(function (data) {
            $scope.tiposDeComprobantesMasSeries = data;
        }).error(function (data) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }

    $scope.cargarSeries = function (tipoComprobante) {
        if (tipoComprobante != null) {
            $scope.series = angular.copy(tipoComprobante.Series);
        }
    }

    $scope.obtenerConceptos = function () {
        conceptoService.obtenerConceptosGastos().success(function (data) {
            $scope.conceptosDeGasto = data;
        }).error(function (data) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }

    $scope.obtenerProveedores = function () {
        compraService.obtenerProveedores({}).success(function (data) {
            $scope.proveedores = data;
        }).error(function (data) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }

    $scope.actualizarProveedoresIngresadosYEditados = function (actualizacionProveedor, id, numeroDocumento, razonSocial) {
        $scope.nuevoProveedor = { Id: id, RazonSocial: razonSocial, NumeroDocumentoIdentidad: numeroDocumento };
        if (actualizacionProveedor == 0) {
            $scope.proveedores.push($scope.nuevoProveedor);
            $scope.gasto.Proveedor = $scope.nuevoProveedor;
        } else {
            $scope.proveedores[document.getElementById("comboProveedor").selectedIndex] = $scope.nuevoProveedor;
        }
    }

    $scope.cambioDeGrabarIgv = function () {
        $scope.gasto.Igv = parseFloat($scope.gasto.GrabaIgv ? ($scope.gasto.Importe * $scope.tasaIGV) : 0).toFixed(2);
        $scope.gasto.Total = parseFloat(parseFloat($scope.gasto.Importe) + parseFloat($scope.gasto.Igv)).toFixed(2);
    }

    $scope.calcularIngresandoImporte = function () {
        $scope.gasto.Igv = parseFloat($scope.gasto.GrabaIgv ? ($scope.gasto.Importe * $scope.tasaIGV) : 0).toFixed(2);
        $scope.gasto.Total = parseFloat(parseFloat($scope.gasto.Importe) + parseFloat($scope.gasto.Igv)).toFixed(2);
    }

    $scope.calcularIngresandoIgv = function () {
        $scope.gasto.Igv = $scope.gasto.GrabaIgv ? ($scope.gasto.Igv) : 0;
        if ($scope.gasto.Igv != 0) {
            $scope.gasto.Importe = parseFloat($scope.gasto.Igv / $scope.tasaIGV).toFixed(2);
            $scope.gasto.Total = parseFloat(parseFloat($scope.gasto.Importe) + parseFloat($scope.gasto.Igv)).toFixed(2);
        }
    }

    $scope.calcularIngresandoTotal = function () {
        $scope.gasto.Igv = parseFloat($scope.gasto.GrabaIgv ? ($scope.gasto.Total - ($scope.gasto.Total / (1 + $scope.tasaIGV))) : 0).toFixed(2);
        $scope.gasto.Importe = parseFloat(parseFloat($scope.gasto.Total) - parseFloat($scope.gasto.Igv)).toFixed(2);
    }

    $scope.close = function () {
        $('#modal-registro-gasto').modal('hide');
    }
    //#endregion

    //#region METODO DE GASTO

    $scope.guardar = function () {
        gastoService.guardarGasto({ gasto: $scope.gasto }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $('#modal-registro-gasto').modal('hide');
            $scope.limpiarGasto();
            $scope.establecerDatosPorDefecto();
            $scope.listarBandeja();
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema", data.error);
        });
    }

    $scope.cerrar = function () {
        $scope.gasto = {};
    }

    $scope.validarGasto = function () {
        $scope.inconsistenciasGasto = [];
        if ($scope.gasto.Proveedor == undefined) {
            $scope.inconsistenciasGasto.push("Es necesario identificar el proveedor.");
        }
        if ($scope.gasto.FechaEmision == '' || $scope.gasto.FechaEmision == undefined) {
            $scope.inconsistenciasGasto.push("Es necesario ingresar la fecha de gasto.");
        }
        if ($scope.gasto.TipoDeComprobante == undefined) {
            $scope.inconsistenciasGasto.push("Es necesario seleccionar un documento.");
        } else {
            if ($scope.gasto.TipoDeComprobante.EsPropio == false) {
                if ($scope.gasto.TipoDeComprobante.SerieIngresada == '' || $scope.gasto.TipoDeComprobante.SerieIngresada == null) {
                    $scope.inconsistenciasGasto.push("Es necesario ingresar la serie de comprobante.");
                } if ($scope.gasto.TipoDeComprobante.NumeroIngresado == '' || $scope.gasto.TipoDeComprobante.NumeroIngresado == null) {
                    $scope.inconsistenciasGasto.push("Es necesario ingresar el numero de comprobante.");
                }
            }
        }
        if ($scope.gasto.Concepto == undefined) {
            $scope.inconsistenciasGasto.push("Es necesario seleccionar un concepto para el gasto.");
        }
        if (parseFloat($scope.gasto.Importe) == 0) {
            $scope.inconsistenciasGasto.push("Es necesario que el importe sea mayor a 0.");
        }
        if (parseFloat($scope.gasto.Total) == 0) {
            $scope.inconsistenciasGasto.push("Es necesario que el total sea mayor a 0.");
        }
    }

    //#endregion

    //#region FINANCIAMIENTO

    $scope.iniciarFinanciamiento = function () {
        if ($scope.gasto.EsGastoACredito == true) {
            $scope.gasto.EsCreditoRapido = true;
        } else {
            $scope.gasto.EsCreditoRapido = {};
        }
        $scope.limpiarCuotas();
    }

    $scope.financiamientoConfigurado = function () {
        $scope.financiamientoRealizado = false;
        $scope.gasto.EsCreditoRapido = false;
        $scope.limpiarCuotas();
        $("#cuota-modal").click();
    }

    $scope.limpiarCuotas = function () {
        $scope.financiamientodetalle = { cuota: 1, inicial: 0, capital: $scope.gasto.Total, total: $scope.gasto.Total, interes: 0 };
        $scope.cuotasdetalle = [];
        $scope.montoTotal = $scope.gasto.Total;
    }

    $scope.calcularTotalFinanciamiento = function () {
        $scope.financiamientodetalle.capital = $scope.financiamientodetalle.inicial > 0 || $scope.financiamientodetalle.inicial != "" ? $scope.gasto.Total - $scope.financiamientodetalle.inicial : $scope.gasto.Total;
        $scope.financiamientodetalle.total = $scope.financiamientodetalle.interes > 0 || $scope.financiamientodetalle.interes != "" ? $scope.financiamientodetalle.capital + ($scope.financiamientodetalle.capital * $scope.financiamientodetalle.interes / 100) : $scope.financiamientodetalle.capital;
        $scope.limpiarCronogramaDePagos();
    }

    $scope.limpiarCronogramaDePagos = function () {
        if ($scope.financiamientodetalle.cuota > 1) {
            $scope.financiamientodetalle.fechaRegistro = undefined;
        } else {
            if ($scope.financiamientodetalle.cuota == 1) {
                $scope.financiamientodetalle.diavencimiento = undefined;
            }
        }
        $scope.cuotasdetalle = [];
    }

    $scope.generarCuota = function () {
        $scope.cuotasdetalle = [];
        $scope.cuotadetalle = {};
        let temp = $scope.financiamientodetalle.cuota;
        let arrayDeFechaRegistro = $scope.fechaActual.split("/");
        let anioHora = arrayDeFechaRegistro[2].split(" ");
        let anio = anioHora[0];
        let mes = arrayDeFechaRegistro[1];
        let dia = $scope.financiamientodetalle.diavencimiento;
        let inicial = parseFloat($scope.financiamientodetalle.inicial);
        $scope.gasto.Inicial = inicial;

        if (inicial !== $scope.gasto.Total) {
            if (inicial > 0) {
                $scope.cuotadetalle = { CapitalCuota: $scope.financiamientodetalle.inicial, InteresCuota: 0.00, ImporteCuota: $scope.financiamientodetalle.inicial, FechaVencimiento: $scope.formatDate(new Date(), "ES"), EsCuotaInicial: true }
                $scope.cuotasdetalle.push($scope.cuotadetalle);
                $scope.cuotadetalle = {};
            }
            if (temp == 1) {
                $scope.cuotadetalle.CapitalCuota = $scope.formatNumber(parseFloat($scope.financiamientodetalle.capital) / parseFloat(temp), 2);
                $scope.cuotadetalle.InteresCuota = $scope.financiamientodetalle.interes > 0 || $scope.financiamientodetalle.interes != "" ? $scope.formatNumber(parseFloat($scope.financiamientodetalle.capital * $scope.financiamientodetalle.interes / 100) / parseFloat(temp), 2) : 0.00;
                $scope.cuotadetalle.ImporteCuota = $scope.formatNumber(parseFloat($scope.financiamientodetalle.total) / parseFloat(temp), 2);
                $scope.cuotadetalle.FechaVencimiento = $scope.financiamientodetalle.fechaRegistro;
                $scope.cuotadetalle.EsCuotaInicial = false;
                $scope.cuotasdetalle.push($scope.cuotadetalle);
                $scope.cuotadetalle = {};
            } else {
                for (let i = 0; i < temp; i++) {
                    $scope.cuotadetalle.CapitalCuota = $scope.formatNumber(parseFloat($scope.financiamientodetalle.capital) / parseFloat(temp), 2);
                    $scope.cuotadetalle.InteresCuota = $scope.financiamientodetalle.interes > 0 || $scope.financiamientodetalle.interes != "" ? $scope.formatNumber(parseFloat($scope.financiamientodetalle.capital * $scope.financiamientodetalle.interes / 100) / parseFloat(temp), 2) : 0.00;
                    $scope.cuotadetalle.ImporteCuota = $scope.formatNumber(parseFloat($scope.financiamientodetalle.total) / parseFloat(temp), 2);
                    if (mes == 13) {
                        mes = 1;
                        anio++;
                    }
                    if (mes == 2 && (dia == 28 || dia == 29)) {
                        $scope.cuotadetalle.FechaVencimiento = $scope.formatDate(new Date(anio, mes, 0), "ES");
                    } else if ((mes == 1 || mes == 3 || mes == 5 || mes == 7 || mes == 8 || mes == 10 || mes == 12) && dia == 31) {
                        $scope.cuotadetalle.FechaVencimiento = $scope.formatDate(new Date(anio, mes, 0), "ES");
                    } else {
                        $scope.cuotadetalle.FechaVencimiento = $scope.formatDate(new Date(anio, mes, dia), "ES");
                    }
                    $scope.cuotadetalle.EsCuotaInicial = false;
                    $scope.cuotasdetalle.push($scope.cuotadetalle);
                    $scope.cuotadetalle = {};
                    mes++;
                }
            }
        }
        $scope.gasto.Cuotas = angular.copy($scope.cuotasdetalle);
        $scope.financiamientoRealizado = true;
    }

    $scope.deshabilitarBotonGenerar = function (fechaRegistro, diavencimiento) {
        return fechaRegistro === undefined && diavencimiento === undefined;
    }

    $scope.seleccionarEsCredito = function () {
        if ($scope.gasto.EsGastoACredito != {}) {
            $scope.gasto.EsGastoACredito = true;
        }
    }

    $scope.finalizarCredito = function () {
        $scope.gasto.EsGastoACredito = false;
        $scope.gasto.PagarInicialAlConfimar = false;
        $scope.gasto.EsCreditoRapido = {};
    }

    $scope.seleccionarCreditoRapido = function () {
        $scope.gasto.PagarInicialAlConfimar = false;
    }

    //#endregion

});
app.controller('reporteVentaController', function ($scope, $q, $timeout, $rootScope, SweetAlert, $filter, DTOptionsBuilder, DTColumnDefBuilder, ventaService, maestroService, clienteService, productoService, centroDeAtencionService, empleadoService, conceptoService, reporteService) {


    $scope.tiposDeComprobantes = [];
    $scope.tiposDeComprobantesMasSeries = [];
    $scope.series = [];
    $scope.detalle = {};
    $scope.listaDeEntidadesInternasConRolPuntoDeVenta = [];
    $scope.listaDeEntidadesInternasConRolPuntoDeVentaNoVigentes = [];
    $scope.caracteristicasConcepto = [];

    $scope.mensajeAdvertencia = [];
    $scope.URLReporteIntervaloFecha = "";
    $scope.URLReporteConceptoPuntoVenta = "";
    $scope.IntervaloEnDias= 0


    //METODO PARA EL MANEJO DE LAS HORAS
    $scope.formatoAMPM = function (hour) {

        var hours = hour.split(":")[0];
        var minutes = hour.split(":")[1];
        var seconds = hour.split(":")[2].split(".")[0];


        var ampm = hours >= 12 ? 'pm' : 'am';
        hours = hours % 12;
        hours = hours ? hours : 12; // the hour '0' should be '12'
        minutes = minutes < 10 ? '0' + minutes : minutes;
        //seconds = seconds < 10 ? '0' + seconds : seconds;

        var strTime = hours + ':' + minutes + ':' + seconds + ' ' + ampm;
        return strTime;
    }



    //METODOS PARA LAS APIS
    $scope.obtenerEmpleadosConRolVendedor = function () {
        empleadoService.obtenerEmpleadosConRolVendedor().success(function (data) {
            $scope.listaEmpleadosGenerico = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.obtenerModalidadesGenerico = function () {
        ventaService.obtenerModalidadesGenerico().success(function (data) {
            $scope.listaDeModalidadesGenerico = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.obtenerEntidadesInternasConRolPuntoVenta = function () {
        centroDeAtencionService.obtenerCentrosDeAtencionConRolPuntoDeVenta().success(function (data) {
            $scope.listaDeEntidadesInternasConRolPuntoDeVenta = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
    $scope.obtenerEntidadesInternasConRolPuntoVentaNoVigentes = function () {
        centroDeAtencionService.obtenerCentrosDeAtencionConRolPuntoDeVentaNoVigentes().success(function (data) {
            $scope.listaDeEntidadesInternasConRolPuntoDeVentaNoVigentes = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
    $scope.cargarSeries = function (tipoComprobante) {
        if (tipoComprobante !== null) {
            $scope.series = angular.copy(tipoComprobante.Series);
        }
    }

    $scope.obtenerVendedoresConIdUsuario = function () {
        empleadoService.obtenerVendedoresConIdUsuario().success(function (data) {
            $scope.listaDeVendedores = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.obtenerSeriesConTipoDeComprobante = function () {
        ventaService.obtenerSeriesConTipoComprobanteParaVenta({}).success(function (data) {
            $scope.seriesConTipoComprobante = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.obtenerTiposDeComprobante = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        ventaService.obtenerTiposDeComprobanteParaVenta({}).success(function (data) {
            $scope.selectorTiposDeComprobantesMasSeries = data;
            $scope.tiposDeComprobantes = [];
            for (var i = 0; i < $scope.selectorTiposDeComprobantesMasSeries.length; i++) {
                $scope.tiposDeComprobantes.push($scope.selectorTiposDeComprobantesMasSeries[i]);
            }

            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.obtenerTiposDeComprobanteParaReporte = function () {
        ventaService.obtenerTiposDeComprobanteParaVentasYSusNotas({}).success(function (data) {
            $scope.selectorTiposDeComprobantesMasSeries = data;
            $scope.tiposDeComprobantes = [];
            for (var i = 0; i < $scope.selectorTiposDeComprobantesMasSeries.length; i++) {
                $scope.tiposDeComprobantes.push($scope.selectorTiposDeComprobantesMasSeries[i]);
            }
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.obtenerCaracteristicasConcepto = function () {
        conceptoService.obtenerCaracteristicasComunesConcepto().success(function (data) {
            $scope.caracteristicasConcepto = data;
            $scope.reporte.CaracteristicasSeleccionadas_Para_DetalladoPorConceptoCaracteristicasFormaPago = angular.copy($scope.caracteristicasConcepto);
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.obtenerCaracteristicaParaFamiliaCaracteristica = function () {
        conceptoService.obtenerCaracteristicasPorFamilia({ idFamilia: $scope.reporte.FamiliaReporteVentasPorFamiliaCaracteristica.Id }).success(function (data) {
            $scope.caracteristicaReporteVentasPorFamiliaCaracteristica = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.obtenerValoresCaracteristicaParaFamiliaCaracteristica = function () {
        conceptoService.verValoresCaracteristica({ idCaracteristica: $scope.reporte.CaracteristicaReporteVentasPorFamiliaCaracteristica.Id }).success(function (data) {
            $scope.valoresCaracteristicaReporteVentasPorFamiliaCaracteristica = data;
        }).error(function (data) {
            SweetAlert.error("Error", data.error);
        });
    }

    $scope.obtenerValoresCaracteristicaParaCaracteristica = function () {
        conceptoService.verValoresCaracteristica({ idCaracteristica: $scope.reporte.CaracteristicaReporteVentasPorCaracteristica.Id }).success(function (data) {
            $scope.valoresCaracteristicaReporteVentasPorCaracteristica = data;
        }).error(function (data) {
            SweetAlert.error("Error", data.error);
        });
    }

    $scope.obtenerFamilias = function () {
        if ($scope.familias == undefined) {
            maestroService.obtenerFamiliasConceptosComercialesVigentes({}).success(function (data) {
                $scope.familias = data;
            }).error(function (data) {
                SweetAlert.error2(data);
            });
        }
    }

    $scope.obtenerConceptos = function () {
        if ($scope.conceptos == undefined) {
            conceptoService.obtenerConceptosDeNegociosComerciales({ modoSeleccionTipoFamilia: '2', informacionSelectorConcepto: '1' }).success(function (data) {
                $scope.conceptos = data;
            }).error(function (data) {
                SweetAlert.error2(data);
            });
        }
    }

    //METODOS PARA LA GESTION DE MENSAJES DE ADVERTENCIAS
    $scope.comprobarElementoObjectIngresado = function (elemento, idElemento, idBtn, idSelect) {
        let buttonIsDisabled = Object.keys(elemento).length !== 0 ? false : true;
        mensajeDeAdvertencia(buttonIsDisabled, idElemento, idBtn, idSelect);
        return !buttonIsDisabled;
    }

    $scope.comprobarElementoEnArrayIngresado = function (elemento, idElemento, idBtn, idSelect) {
        let buttonIsDisabled = elemento && elemento.length > 0 ? false : true;
        mensajeDeAdvertencia(buttonIsDisabled, idElemento, idBtn, idSelect);
        return !buttonIsDisabled;
    }

    $scope.comprobarIdEnArrayIngresado = function (id, idElemento, idBtn, idSelect) {
        let buttonIsDisabled = id > 0 ? false : true;
        mensajeDeAdvertencia(buttonIsDisabled, idElemento, idBtn, idSelect);
        return !buttonIsDisabled;
    }

    function mensajeDeAdvertencia(buttonIsDisabled, idElemento, idBtn, idSelect) {
        if (buttonIsDisabled) {
            $('#' + idBtn).attr("disabled", "disabled");
            $('#' + idElemento).addClass("input-report-content");
            $('#' + idElemento).removeClass("no-after-input-report-content");
            $('#' + idSelect).addClass("error-in-select");
        } else {
            $('#' + idBtn).removeAttr("disabled");
            $('#' + idElemento).addClass("no-after-input-report-content");
            $('#' + idElemento).removeClass("input-report-content");
            $('#' + idSelect).removeClass("error-in-select");
        }
    }

    function validacionBotonVer(idBtn, buttonIsDisabled) {

        if (buttonIsDisabled) {
            $('#' + idBtn).attr("disabled", "disabled");
        } else {
            $('#' + idBtn).removeAttr("disabled");
        }
    }

    function mensajeDeValidacion(idElemento, idSelect, buttonIsDisabled) {

        if (buttonIsDisabled) {
            $('#' + idElemento).addClass("input-report-content");
            $('#' + idElemento).removeClass("no-after-input-report-content");
            $('#' + idSelect).addClass("error-in-select");

        } else {
            $('#' + idElemento).addClass("no-after-input-report-content");
            $('#' + idElemento).removeClass("input-report-content");
            $('#' + idSelect).removeClass("error-in-select");

        }
    }

    //METODOS PARA LAS FOCUS
    $scope.focusNext = function (id) {
        $timeout(function () {
            $('#' + id).trigger("focus");
        }, 100);
    }

    $scope.focusSelectNext = function (id) {
        $timeout(function () {
            $('#' + id).trigger("focus");
        }, 100);
        $timeout(function () {
            $('#' + id).trigger("select");
        }, 100);
    }

    $scope.go = function (path) {
        $window.location.href = path;
    };



    /*============================================================================================ =METODOS REPORTE ADMINISTRADOR  ========================================================================================================== */


    $scope.nuevoReporteGerente = function () {
        $scope.fechaInicio = fechaHoraInicio.split(" ")[0];
        $scope.fechaFin = fechaHoraFin.split(" ")[0];
        $scope.parametrosReportesAdministrador= parametrosReportesAdministrador;

        $scope.horaInicio = fechaHoraInicio.split(" ")[1];
        $scope.horaFin = fechaHoraFin.split(" ")[1];

        $scope.reporte = {
            PuntosDeVenta: [], TipoDeComprobante: {}, Serie: {},
            FechaInicio: $scope.fechaInicio,
            FechaFinal: $scope.fechaFin,
            HoraInicio: $scope.formatoAMPM($scope.horaInicio),
            HoraFinal: $scope.formatoAMPM($scope.horaFin),
            PuntosDeVentaParaConcepto: [],
            PuntosDeVentaParaComprobanteVigentes: [],
            PuntosDeVentaParaComprobanteNoVigentes: [],
            PuntosDeVentaParaFamilia: [],
            PuntosDeVentaParaCategoria: [],
            SeriesConTipoComprobante: [],
            PuntoDeVentaReporteInvalidaciones: {},
            PuntoDeVentaReporteNotasDeCredito: {},
            PuntoDeVentaReporteNotasDebito: {},

            EmpleadosParaVender: [],
            EmpleadosParaVender1: {},
            EmpleadosParaVender2: {},
            VendedorParaReportePorConceptoBasico: {},
            PuntosDeVentaParaConceptoDeAdministrador: {},
            Modalidades1: [],
            Modalidades2: [],
            PuntosDeVentaParaReportePorNumeroDeComprobanteConIcbper: [],
            PuntosDeVentaParaReportePorIntervaloDiario: [],
            PuntosDeVentaSeleccionados_Para_ReportePorCaracteristica: [],
            CaracteristicaSeleccionada_Para_ReportePorCaracteristica: [],
            CaracteristicasSeleccionadas_Para_DetalladoPorConceptoCaracteristicasFormaPago: [],
            PuntoDeVentaParaDetalladoPorConceptoCaracteristicasFormaPago: {},
            PuntosDeVentaReporteVentasPorFamiliaCaracteristica: [],
            FamiliaReporteVentasPorFamiliaCaracteristica: {},
            CaracteristicaReporteVentasPorFamiliaCaracteristica: {},
            ValorCaracteristicaReporteVentasPorFamiliaCaracteristica: {},
            PuntosDeVentaReporteVentasPorCaracteristica: [],
            CaracteristicaReporteVentasPorCaracteristica: {},
            ValorCaracteristicaReporteVentasPorCaracteristica: {},
            PuntosDeVentaReporteVentasPorConcepto: [],
            ConceptoReporteVentasPorConcepto: {},

        };
        $scope.obtenerEntidadesInternasConRolPuntoVenta();
        $scope.obtenerEntidadesInternasConRolPuntoVentaNoVigentes();
        $scope.obtenerTiposDeComprobanteParaReporte();
        $scope.obtenerSeriesConTipoDeComprobante();
        $scope.obtenerEmpleadosConRolVendedor();
        $scope.obtenerVendedoresConIdUsuario();
        $scope.obtenerModalidadesGenerico();
        $scope.obtenerCaracteristicasConcepto();
        $scope.validarSeleccionDePuntosDeVentaParaComprobanteVigentes();
        $scope.validarSeleccionDePuntosDeVentaParaComprobanteNoVigentes();
        $scope.obtenerFamilias();
        $scope.obtenerConceptos();
    }

    $scope.calcularIntervaloEnDias = function (){
        $scope.IntervaloEnDias = $scope.diferenciaDiasEntreFechas($scope.reporte.FechaInicio, $scope.reporte.FechaFinal);
    }

    $scope.actualizarURLsAdministrador = function () {
        $scope.calcularIntervaloEnDias();

        $scope.actualizarURLReporteDeVentaDeLosPuntosDeVentaPorComprobante();
        $scope.validarSeleccionDePuntosDeVentaParaComprobanteVigentes();
        $scope.validarSeleccionDePuntosDeVentaParaComprobanteNoVigentes();
        $scope.actualizarURLReporteDeVentaPorSerieDeComprobante();
        $scope.actualizarURLReporteDeVentaPorConceptoDeVariosPuntoDeVenta();
        $scope.actualizarURLReporteDeVentaDeVendedoresPorConcepto();
        $scope.actualizarURLReporteDeVentaDeLosPuntosDeVentaPorSerieDeComprobanteYFamilia();
        $scope.actualizarURLReporteDeVentaDeLosPuntosDeVentaPorSerieDeComprobanteYConcepto();
        $scope.actualizarURLReporteDeVentaDeLosPuntosDeVentaPorSerieDeComprobanteYCategoria();
        $scope.actualizarURLReporteDeVentasPorModalidadConsolidadoPorConcepto();
        $scope.actualizarURLReporteDeVentasPorModalidadConsolidadoPorPrecioUnitario();
        $scope.actualizarURLReporteDeVentaPorNumeroDeComprobanteConIcbper();
        $scope.actualizarURLReporteDeVentaPorIntervaloDiario();
        $scope.actualizarURLReporteDeVentasPorConceptoBasicoPorVendedor();
        $scope.actualizarURLReporteDeVentasConsolidadoPorConceptoBasicoPorVendedores();
        $scope.actualizarURLReporteDeVentasConsolidadoAlContadoAlCreditoPorVendedores();
        $scope.actualizarURLReporteDeVentasPorCaracteristica();
        $scope.actualizarURLReporteDeVentaDetalladoPorConceptoCaracteristicasFormaPago();
        $scope.actualizarURLReporteVentasPorFamiliaCaracteristica();
        $scope.actualizarURLReporteVentasPorCaracteristica();
        $scope.actualizarURLReporteVentasPorConcepto();
        $scope.actualizarURLReporteInvalidacionesAdministrador();
        $scope.actualizarURLReporteNotasDeCredito();
        $scope.actualizarURLReporteNotasDebito();

    }

    //REPORTE DE PUNTOS DE VENTA POR COMPROBANTE
    $scope.validarSeleccionDePuntosDeVentaParaComprobanteVigentes = function () {
        let estadoBotonInactivo = !(($scope.reporte.PuntosDeVentaParaComprobanteVigentes && $scope.reporte.PuntosDeVentaParaComprobanteVigentes.length > 0) || ($scope.reporte.PuntosDeVentaParaComprobanteNoVigentes && $scope.reporte.PuntosDeVentaParaComprobanteNoVigentes.length > 0));
        validacionBotonVer('btn-see-in-report-1', estadoBotonInactivo);
        let mostrarMensaje = (!$scope.reporte.PuntosDeVentaParaComprobanteVigentes) || ($scope.reporte.PuntosDeVentaParaComprobanteVigentes.length == 0);
        mensajeDeValidacion('input-report-puntos-venta-comprobante-vigentes', 'select-report-1', mostrarMensaje);
    }

    $scope.validarSeleccionDePuntosDeVentaParaComprobanteNoVigentes = function () {
        let estadoBotonInactivo = !(($scope.reporte.PuntosDeVentaParaComprobanteVigentes && $scope.reporte.PuntosDeVentaParaComprobanteVigentes.length > 0) || ($scope.reporte.PuntosDeVentaParaComprobanteNoVigentes && $scope.reporte.PuntosDeVentaParaComprobanteNoVigentes.length > 0));
        validacionBotonVer('btn-see-in-report-1', estadoBotonInactivo);
        let mostrarMensaje = (!$scope.reporte.PuntosDeVentaParaComprobanteNoVigentes) || ($scope.reporte.PuntosDeVentaParaComprobanteNoVigentes.length == 0);
        mensajeDeValidacion('input-report-puntos-venta-comprobante-no-vigentes', 'select-report-1', mostrarMensaje);
    }

    $scope.actualizarURLReporteDeVentaDeLosPuntosDeVentaPorComprobante = function () {
        var idsPuntosVenta = "";

        if ($scope.reporte.PuntosDeVentaParaComprobanteVigentes) {
            for (var i = 0; i < $scope.reporte.PuntosDeVentaParaComprobanteVigentes.length; i++) {
                idsPuntosVenta += "&idsPuntosDeVentas=" + $scope.reporte.PuntosDeVentaParaComprobanteVigentes[i].Id;
            }
        }
        if ($scope.reporte.PuntosDeVentaParaComprobanteNoVigentes) {
            for (var i = 0; i < $scope.reporte.PuntosDeVentaParaComprobanteNoVigentes.length; i++) {
                idsPuntosVenta += "&idsPuntosDeVentas=" + $scope.reporte.PuntosDeVentaParaComprobanteNoVigentes[i].Id;
            }
        }
        //$scope.comprobarElementoEnArrayIngresado($scope.reporte.PuntosDeVentaParaComprobante, 'input-report-puntos-venta-comprobante', 'btn-see-in-report-1', 'select-report-1');

        $scope.URLReporteDeVentaDeLosPuntosDeVentaPorComprobante = URL_ + "/Reporte/ReporteDeVentaDeLosPuntosDeVentaPorComprobante?fechaInicio=" + $scope.reporte.FechaInicio + ' ' + $scope.reporte.HoraInicio + "&fechaFin=" + $scope.reporte.FechaFinal + ' ' + $scope.reporte.HoraFinal + idsPuntosVenta;
    }


    //REPORTE POR SERIE DE COMPROBANTE
    $scope.actualizarURLReporteDeVentaPorSerieDeComprobante = function () {

        var validado = $scope.comprobarElementoObjectIngresado($scope.reporte.TipoDeComprobante, 'input-report-comprobante', 'btn-see-in-report-2', 'select-report-2');
        if (validado)
            $scope.comprobarIdEnArrayIngresado($scope.reporte.Serie, 'input-report-serie', 'btn-see-in-report-2', 'select-report-2');

        $scope.URLReporteDeVentaPorSerieDeComprobante = URL_ + "/Reporte/ReporteIntervaloFechasPorComprobante?fechaInicio=" + $scope.reporte.FechaInicio + ' ' + $scope.reporte.HoraInicio + "&fechaFin=" + $scope.reporte.FechaFinal + ' ' + $scope.reporte.HoraFinal + "&idSerie=" + $scope.reporte.Serie;
    }

    //REPORTE DE PUNTOS DE VENTA POR CONCEPTO
    $scope.actualizarURLReporteDeVentaPorConceptoDeVariosPuntoDeVenta = function () {
        var idsPuntosVenta = "";

        if ($scope.reporte.PuntosDeVentaParaConcepto) {
            for (var i = 0; i < $scope.reporte.PuntosDeVentaParaConcepto.length; i++) {
                idsPuntosVenta += "&idsPuntosDeVentas=" + $scope.reporte.PuntosDeVentaParaConcepto[i].Id;
            }
        }

        $scope.comprobarElementoEnArrayIngresado($scope.reporte.PuntosDeVentaParaConcepto, 'input-report-puntos-venta-concepto', 'btn-see-in-report-3', 'select-report-3');

        $scope.URLReporteDeVentaPorConceptoDeVariosPuntoDeVenta = URL_ + "/Reporte/ReporteIntervaloFechasPorConcepto?fechaInicio=" + $scope.reporte.FechaInicio + ' ' + $scope.reporte.HoraInicio + "&fechaFin=" + $scope.reporte.FechaFinal + ' ' + $scope.reporte.HoraFinal + idsPuntosVenta;
    }

    //REPORTE DE VENDEDORES POR CONCEPTO
    $scope.actualizarURLReporteDeVentaDeVendedoresPorConcepto = function () {
        var idsVendedor = "";

        if ($scope.reporte.EmpleadosParaVender) {
            for (var i = 0; i < $scope.reporte.EmpleadosParaVender.length; i++) {
                idsVendedor += "&idsVendedor=" + $scope.reporte.EmpleadosParaVender[i].Id;
            }

        }
        $scope.comprobarElementoEnArrayIngresado($scope.reporte.EmpleadosParaVender, 'input-report-vendedores-concepto', 'btn-see-in-report-4', 'select-report-4');

        $scope.URLReporteDeVentaDeVendedoresPorConcepto = URL_ + "/Reporte/ReporteDeVentaDeVendedoresPorConcepto?fechaInicio=" + $scope.reporte.FechaInicio + ' ' + $scope.reporte.HoraInicio + "&fechaFin=" + $scope.reporte.FechaFinal + ' ' + $scope.reporte.HoraFinal + idsVendedor;
    }

    //REPORTE DE PUNTOS DE VENTA POR SERIE Y FAMILIA
    $scope.actualizarURLReporteDeVentaDeLosPuntosDeVentaPorSerieDeComprobanteYFamilia = function () {
        var idsPuntosVenta = "";

        if ($scope.reporte.PuntosDeVentaParaFamilia) {
            for (var i = 0; i < $scope.reporte.PuntosDeVentaParaFamilia.length; i++) {
                idsPuntosVenta += "&idsPuntosDeVentas=" + $scope.reporte.PuntosDeVentaParaFamilia[i].Id;
            }
        }

        $scope.comprobarElementoEnArrayIngresado($scope.reporte.PuntosDeVentaParaFamilia, 'input-report-puntos-venta-serie-familia', 'btn-see-in-report-5', 'select-report-5');

        $scope.URLReporteDeVentaDeLosPuntosDeVentaPorSerieDeComprobanteYFamilia = URL_ + "/Reporte/ReporteDeVentaDeLosPuntosDeVentaPorSerieDeComprobanteYFamilia?fechaInicio=" + $scope.reporte.FechaInicio + ' ' + $scope.reporte.HoraInicio + "&fechaFin=" + $scope.reporte.FechaFinal + ' ' + $scope.reporte.HoraFinal + idsPuntosVenta;
    }


    //REPORTE DE PUNTO DE VENTA POR SERIE Y CONCEPTO
    $scope.actualizarURLReporteDeVentaDeLosPuntosDeVentaPorSerieDeComprobanteYConcepto = function () {

        $scope.comprobarIdEnArrayIngresado($scope.reporte.PuntosDeVentaParaConceptoDeAdministrador.Id, 'input-report-punto-venta-serie-concepto', 'btn-see-in-report-6', 'select-report-6');

        $scope.URLReporteDeVentaDeLosPuntosDeVentaPorSerieDeComprobanteYConcepto = URL_ + "/Reporte/ReporteDeVentaDeLosPuntosDeVentaPorSerieDeComprobanteYConcepto?fechaInicio=" + $scope.reporte.FechaInicio + ' ' + $scope.reporte.HoraInicio + "&fechaFin=" + $scope.reporte.FechaFinal + ' ' + $scope.reporte.HoraFinal + "&idPuntoDeVenta=" + $scope.reporte.PuntosDeVentaParaConceptoDeAdministrador.Id + "&nombrePuntoDeVenta=" + $scope.reporte.PuntosDeVentaParaConceptoDeAdministrador.Nombre;
    }

    //REPORTE DE PUNTOS DE VENTA POR SERIE Y CATEGORIA
    $scope.actualizarURLReporteDeVentaDeLosPuntosDeVentaPorSerieDeComprobanteYCategoria = function () {
        var idsPuntosVenta = "";

        if ($scope.reporte.PuntosDeVentaParaCategoria) {
            for (var i = 0; i < $scope.reporte.PuntosDeVentaParaCategoria.length; i++) {
                idsPuntosVenta += "&idsPuntosDeVentas=" + $scope.reporte.PuntosDeVentaParaCategoria[i].Id;
            }
        }

        $scope.comprobarElementoEnArrayIngresado($scope.reporte.PuntosDeVentaParaCategoria, 'input-report-puntos-venta-serie-categoria', 'btn-see-in-report-cat-5', 'select-report-cat-5');

        $scope.URLReporteDeVentaDeLosPuntosDeVentaPorSerieDeComprobanteYCategoria = URL_ + "/Reporte/ReporteDeVentaDeLosPuntosDeVentaPorSerieDeComprobanteYCategoria?fechaInicio=" + $scope.reporte.FechaInicio + ' ' + $scope.reporte.HoraInicio + "&fechaFin=" + $scope.reporte.FechaFinal + ' ' + $scope.reporte.HoraFinal + idsPuntosVenta;
    }
    //REPORTE DE PUNTOS DE VENTA POR SERIES
    $scope.actualizarURLReporteDeVentaPorSeriesConTipoComprobante = function () {
        var idsSeries = "";
        var nombresSeries = "&nombresSeries=";
        if ($scope.reporte.SeriesConTipoComprobante) {
            for (var i = 0; i < $scope.reporte.SeriesConTipoComprobante.length; i++) {
                idsSeries += "&idsSeries=" + $scope.reporte.SeriesConTipoComprobante[i].Id;
                nombresSeries += $scope.reporte.SeriesConTipoComprobante[i].Nombre + ", ";
            }
        }

        $scope.comprobarElementoEnArrayIngresado($scope.reporte.SeriesConTipoComprobante, 'input-report-series', 'btn-see-in-report-ser-5', 'select-report-ser-5');

        $scope.URLReporteDeVentaPorSeriesConTipoComprobante = URL_ + "/Reporte/ReporteDeVentaPorSerieDeComprobanteYConcepto?fechaInicio=" + $scope.reporte.FechaInicio + ' ' + $scope.reporte.HoraInicio + "&fechaFin=" + $scope.reporte.FechaFinal + ' ' + $scope.reporte.HoraFinal + idsSeries + nombresSeries;
    }


    //REPORTE CONSOLIDADO POR CONCEPTO
    $scope.actualizarURLReporteDeVentasPorModalidadConsolidadoPorConcepto = function () {
        var modalidades = "";
        if ($scope.reporte.Modalidades1) {
            for (var i = 0; i < $scope.reporte.Modalidades1.length; i++) {
                modalidades += "&modalidades=" + $scope.reporte.Modalidades1[i].Id;
            }
        }

        var validado = $scope.comprobarIdEnArrayIngresado($scope.reporte.EmpleadosParaVender1.Id, 'input-report-venta-modalidad-consolidado-concepto-vendedor', 'btn-see-in-report-7', 'select-report-7');
        if (validado)
            $scope.comprobarElementoEnArrayIngresado($scope.reporte.Modalidades1, 'input-report-venta-modalidad-consolidado-concepto-modalidades', 'btn-see-in-report-7', 'select-report-7');


        $scope.URLReporteDeVentasPorModalidadConsolidadoPorConcepto = URL_ + "/Reporte/VentasPorModalidadConsolidadoPorConcepto?fechaInicio=" + $scope.reporte.FechaInicio + ' ' + $scope.reporte.HoraInicio + "&fechaFin=" + $scope.reporte.FechaFinal + ' ' + $scope.reporte.HoraFinal + "&idVendedor=" + $scope.reporte.EmpleadosParaVender1.Id + "&nombreVendedor=" + $scope.reporte.EmpleadosParaVender1.Nombre + modalidades;
    }


    // REPORTE CONSOLIDADO POR PRECIO UNITARIO
    $scope.actualizarURLReporteDeVentasPorModalidadConsolidadoPorPrecioUnitario = function () {
        var modalidades = "";
        if ($scope.reporte.Modalidades2) {
            for (var i = 0; i < $scope.reporte.Modalidades2.length; i++) {
                modalidades += "&modalidades=" + $scope.reporte.Modalidades2[i].Id;
            }
        }

        var validado = $scope.comprobarIdEnArrayIngresado($scope.reporte.EmpleadosParaVender2.Id, 'input-report-venta-modalidad-consolidado-precio-unitario-vendedor', 'btn-see-in-report-8', 'select-report-8');
        if (validado)
            $scope.comprobarElementoEnArrayIngresado($scope.reporte.Modalidades2, 'input-report-venta-modalidad-consolidado-precio-unitario-modalidades', 'btn-see-in-report-8', 'select-report-8');

        $scope.URLReporteDeVentasPorModalidadConsolidadoPorPrecioUnitario = URL_ + "/Reporte/VentasPorModalidadConsolidadoPorPrecioUnitario?fechaInicio=" + $scope.reporte.FechaInicio + ' ' + $scope.reporte.HoraInicio + "&fechaFin=" + $scope.reporte.FechaFinal + ' ' + $scope.reporte.HoraFinal + "&idVendedor=" + $scope.reporte.EmpleadosParaVender2.Id + "&nombreVendedor=" + $scope.reporte.EmpleadosParaVender2.Nombre + modalidades;
    }


    //REPORTE DE PUNTOS DE VENTA POR NÚMERO DE COMPROBANTE CON ICBPER
    $scope.actualizarURLReporteDeVentaPorNumeroDeComprobanteConIcbper = function () {
        var idsPuntosVenta = "";

        if ($scope.reporte.PuntosDeVentaParaReportePorNumeroDeComprobanteConIcbper) {
            for (var i = 0; i < $scope.reporte.PuntosDeVentaParaReportePorNumeroDeComprobanteConIcbper.length; i++) {
                idsPuntosVenta += "&idsPuntosDeVentas=" + $scope.reporte.PuntosDeVentaParaReportePorNumeroDeComprobanteConIcbper[i].Id;
            }
        }

        $scope.comprobarElementoEnArrayIngresado($scope.reporte.PuntosDeVentaParaReportePorNumeroDeComprobanteConIcbper, 'input-report-venta-por-numero-de-comprobante-con-icbper', 'btn-see-in-report-9', 'select-report-9');

        $scope.URLReporteDeVentaPorNumeroDeComprobanteConIcbper = URL_ + "/Reporte/ReportePorNumeroDeComprobanteConIcbper?fechaInicio=" + $scope.reporte.FechaInicio + ' ' + $scope.reporte.HoraInicio + "&fechaFin=" + $scope.reporte.FechaFinal + ' ' + $scope.reporte.HoraFinal + idsPuntosVenta;
    }

    //REPORTE DE PUNTOS DE VENTA POR INTERVALO DIARIO 
    $scope.actualizarURLReporteDeVentaPorIntervaloDiario = function () {
        var idsPuntosVenta = "";

        if ($scope.reporte.PuntosDeVentaParaReportePorIntervaloDiario) {
            for (var i = 0; i < $scope.reporte.PuntosDeVentaParaReportePorIntervaloDiario.length; i++) {
                idsPuntosVenta += "&idsPuntosDeVentas=" + $scope.reporte.PuntosDeVentaParaReportePorIntervaloDiario[i].Id;
            }
        }

        $scope.comprobarElementoEnArrayIngresado($scope.reporte.PuntosDeVentaParaReportePorIntervaloDiario, 'input-report-venta-por-intervalo-diario', 'btn-see-in-report-10', 'select-report-10');

        $scope.URLReporteDeVentaPorIntervaloDiario = URL_ + "/Venta/ReporteDeVentaDeLosPuntosDeVentaPorIntervaloDiario?fechaInicio=" + $scope.reporte.FechaInicio + ' ' + $scope.reporte.HoraInicio + "&fechaFin=" + $scope.reporte.FechaFinal + ' ' + $scope.reporte.HoraFinal + idsPuntosVenta;
    }

    //REPORTE DE VENTAS POR CONCEPTO BASICO POR VENDEDOR
    $scope.actualizarURLReporteDeVentasPorConceptoBasicoPorVendedor = function () {

        $scope.comprobarIdEnArrayIngresado($scope.reporte.VendedorParaReportePorConceptoBasico.Id, 'input-report-venta-concepto-basico-vendedor', 'btn-see-in-report-11', 'select-report-11');

        $scope.URLReporteDeVentasPorConceptoBasicoPorVendedor = URL_ + "/Reporte/ReporteDeVentasPorConceptoBasicoPorVendedor?fechaInicio=" + $scope.reporte.FechaInicio + ' ' + $scope.reporte.HoraInicio + "&fechaFin=" + $scope.reporte.FechaFinal + ' ' + $scope.reporte.HoraFinal + "&idVendedor=" + $scope.reporte.VendedorParaReportePorConceptoBasico.Id + "&nombreVendedor=" + $scope.reporte.VendedorParaReportePorConceptoBasico.Nombre;
    };

    //REPORTE DE VENTAS CONSOLIDADO POR CONCEPTO BASICO POR VENDEDORES
    $scope.actualizarURLReporteDeVentasConsolidadoPorConceptoBasicoPorVendedores = function () {

        $scope.URLReporteDeVentasConsolidadoPorConceptoBasicoPorVendedores = URL_ + "/Reporte/ReporteDeVentasConsolidadoPorConceptoBasicoPorVendedores?fechaInicio=" + $scope.reporte.FechaInicio + ' ' + $scope.reporte.HoraInicio + "&fechaFin=" + $scope.reporte.FechaFinal + ' ' + $scope.reporte.HoraFinal;
    };


    //REPORTE DE VENTAS CONSOLIDADO AL CONTADO Y AL CREDITO POR VENDEDORES
    $scope.actualizarURLReporteDeVentasConsolidadoAlContadoAlCreditoPorVendedores = function () {
        $scope.URLReporteDeVentasConsolidadoAlContadoAlCreditoPorVendedores = URL_ + "/Reporte/ReporteDeVentasConsolidadoAlContadoAlCreditoPorVendedores?fechaInicio=" + $scope.reporte.FechaInicio + ' ' + $scope.reporte.HoraInicio + "&fechaFin=" + $scope.reporte.FechaFinal + ' ' + $scope.reporte.HoraFinal;
    };

    $scope.actualizarURLReporteDeVentasPorCaracteristica = function () {
        var nombres = [];
        if ($scope.reporte.PuntosDeVentaSeleccionados_Para_ReportePorCaracteristica != undefined)
            $scope.reporte.PuntosDeVentaSeleccionados_Para_ReportePorCaracteristica.forEach(ca => nombres.push(ca.Nombre));
        nombres = nombres.join(', ');
        var idsCentrosAtencion = [];
        if ($scope.reporte.PuntosDeVentaSeleccionados_Para_ReportePorCaracteristica != undefined)
            $scope.reporte.PuntosDeVentaSeleccionados_Para_ReportePorCaracteristica.forEach(ca => idsCentrosAtencion.push('&idsCentrosAtencion=' + ca.Id));
        $scope.URLReporteDeVentaPorCaracteristica = URL_ + "/Reporte/VentasPorCaracteristica?fechaInicio=" + $scope.reporte.FechaInicio + ' ' + $scope.reporte.HoraInicio +
            "&fechaFin=" + $scope.reporte.FechaFinal + ' ' + $scope.reporte.HoraFinal +
            "&idsCentrosAtencion=" + idsCentrosAtencion.join('') +
            "&nombresCentrosAtencion=" + nombres +
            "&idcaracteristica=" + $scope.reporte.CaracteristicaSeleccionada_Para_ReportePorCaracteristica.Id +
            "&nombreCaracteristicaString=" + $scope.reporte.CaracteristicaSeleccionada_Para_ReportePorCaracteristica.Nombre;

        var validado = $scope.comprobarIdEnArrayIngresado($scope.reporte.CaracteristicaSeleccionada_Para_ReportePorCaracteristica.Id, 'input-report-venta-caracteristica-caracteristica', 'btn-see-in-report-caracteristica', 'select-report-venta-caracteristica-caracteristica');
        if (validado)
            $scope.comprobarElementoEnArrayIngresado($scope.reporte.PuntosDeVentaSeleccionados_Para_ReportePorCaracteristica, 'input-report-venta-caracteristica-puntoventa', 'btn-see-in-report-caracteristica', 'select-report-venta-caracteristica-puntoventa');

    };

    //DETALLADO POR CONCEPTO, CARACTERISTICA Y FORMA DE PAGO
    $scope.actualizarURLReporteDeVentaDetalladoPorConceptoCaracteristicasFormaPago = function () {
        var nombres = [];
        if ($scope.reporte.PuntosDeVentaSeleccionados_Para_ReportePorCaracteristica != undefined)
            $scope.reporte.PuntosDeVentaSeleccionados_Para_ReportePorCaracteristica.forEach(ca => nombres.push(ca.Nombre));
        nombres = nombres.join(', ');
        var idsCaracteristicas = [];
        if ($scope.reporte.CaracteristicasSeleccionadas_Para_DetalladoPorConceptoCaracteristicasFormaPago != undefined)
            $scope.reporte.CaracteristicasSeleccionadas_Para_DetalladoPorConceptoCaracteristicasFormaPago.forEach(ca => idsCaracteristicas.push('&idsCaracteristicas=' + ca.Id));
        $scope.URLReporteDeVentaDetalladoPorConceptoCaracteristicasFormaPago = URL_ + "/Reporte/VentasDetalladasPorConceptoCaracteristicasFormaPago?fechaInicio=" + $scope.reporte.FechaInicio + ' ' + $scope.reporte.HoraInicio +
            "&fechaFin=" + $scope.reporte.FechaFinal + ' ' + $scope.reporte.HoraFinal +
            "&idsCaracteristicas=" + idsCaracteristicas.join('') +
            "&idPuntoVenta=" + $scope.reporte.PuntoDeVentaParaDetalladoPorConceptoCaracteristicasFormaPago.Id +
            "&nombrePuntoVenta=" + $scope.reporte.PuntoDeVentaParaDetalladoPorConceptoCaracteristicasFormaPago.Nombre;

        var validado = $scope.comprobarIdEnArrayIngresado($scope.reporte.PuntoDeVentaParaDetalladoPorConceptoCaracteristicasFormaPago.Id, 'input-report-punto-venta-detallado-concepto-caracteristica-formapago', 'btn-see-in-report-detallado-concepto-caracteristica-formapago', 'select-report-punto-venta-detallado-concepto-caracteristica-formapago');
        if (validado)
            $scope.comprobarElementoEnArrayIngresado($scope.reporte.CaracteristicasSeleccionadas_Para_DetalladoPorConceptoCaracteristicasFormaPago, 'input-report-caracteristicas-detallado-concepto-caracteristica-formapago', 'btn-see-in-report-detallado-concepto-caracteristica-formapago', 'select-report-caracteristicas-detallado-concepto-caracteristica-formapago');
    };


    $scope.actualizarURLReporteInvalidacionesAdministrador = function () {

        $scope.comprobarElementoObjectIngresado($scope.reporte.PuntoDeVentaReporteInvalidaciones, 'input-report-venta-invalidaciones-administrador', 'btn-see-in-report-12', 'select-report-12');
        if ($scope.IntervaloEnDias > $scope.parametrosReportesAdministrador.MaximoDiasInvalidaciones) {
            $scope.reporte.MensajeErrorInvalidaciones = "Maxima cantidad de días a mostrar: " + $scope.parametrosReportesAdministrador.MaximoDiasInvalidaciones;
        }
        else {
            $scope.reporte.MensajeErrorInvalidaciones = "";

        }
        
        $scope.URLReporteInvalidacionesAdministrador = $scope.reporte.MensajeErrorInvalidaciones != "" ?'': URL_ + "/Reporte/InvalidacionesAdministrador?fechaInicio=" + $scope.reporte.FechaInicio + ' ' + $scope.reporte.HoraInicio + "&fechaFin=" + $scope.reporte.FechaFinal + ' ' + $scope.reporte.HoraFinal + "&idPuntoVenta=" + $scope.reporte.PuntoDeVentaReporteInvalidaciones.Id + "&nombrePuntoVenta=" + $scope.reporte.PuntoDeVentaReporteInvalidaciones.Nombre;
    }

    $scope.actualizarURLReporteNotasDeCredito = function () {

        $scope.comprobarElementoObjectIngresado($scope.reporte.PuntoDeVentaReporteNotasDeCredito, 'input-report-venta-notas-credito', 'btn-see-in-report-13', 'select-report-13');
        if ($scope.IntervaloEnDias > $scope.parametrosReportesAdministrador.MaximoDiasNotasCredito) {
            $scope.reporte.MensajeErrorReporteNotasDeCredito = "Maxima cantidad de días a mostrar: " + $scope.parametrosReportesAdministrador.MaximoDiasNotasCredito;
        }
        else {
            $scope.reporte.MensajeErrorReporteNotasDeCredito = "";

        }

        $scope.URLReporteNotasDeCredito = $scope.reporte.MensajeErrorReporteNotasDeCredito != "" ? '' : URL_ + "/Reporte/NotasCredito?fechaInicio=" + $scope.reporte.FechaInicio + ' ' + $scope.reporte.HoraInicio + "&fechaFin=" + $scope.reporte.FechaFinal + ' ' + $scope.reporte.HoraFinal + "&idPuntoVenta=" + $scope.reporte.PuntoDeVentaReporteNotasDeCredito.Id + "&nombrePuntoVenta=" + $scope.reporte.PuntoDeVentaReporteNotasDeCredito.Nombre;
    }

    $scope.actualizarURLReporteNotasDebito = function () {

        $scope.comprobarElementoObjectIngresado($scope.reporte.PuntoDeVentaReporteNotasDebito, 'input-report-venta-notas-debito', 'btn-see-in-report-14', 'select-report-14');
        if ($scope.IntervaloEnDias > $scope.parametrosReportesAdministrador.MaximoDiasNotasDebito) {
            $scope.reporte.MensajeErrorReporteNotasDebito = "Maxima cantidad de días a mostrar: " + $scope.parametrosReportesAdministrador.MaximoDiasNotasDebito;
        }
        else {
            $scope.reporte.MensajeErrorReporteNotasDebito = "";

        }

        $scope.URLReporteNotasDebito = $scope.reporte.MensajeErrorReporteNotasDebito != "" ? '' : URL_ + "/Reporte/NotasDebito?fechaInicio=" + $scope.reporte.FechaInicio + ' ' + $scope.reporte.HoraInicio + "&fechaFin=" + $scope.reporte.FechaFinal + ' ' + $scope.reporte.HoraFinal + "&idPuntoVenta=" + $scope.reporte.PuntoDeVentaReporteNotasDebito.Id + "&nombrePuntoVenta=" + $scope.reporte.PuntoDeVentaReporteNotasDebito.Nombre;
    }


    $scope.diferenciaDiasEntreFechas = function (fechaInicio, fechaFin) {
        var dt1 = fechaInicio.split('/'),
            dt2 = fechaFin.split('/'),
            one = new Date(dt1[2], dt1[1] - 1, dt1[0]),
            two = new Date(dt2[2], dt2[1] - 1, dt2[0]);
        var millisecondsPerDay = 1000 * 60 * 60 * 24;
        var millisBetween = two.getTime() - one.getTime();
        var days = millisBetween / millisecondsPerDay;
        return Math.floor(days);
    }
    /*============================================================================================ METODOS REPORTE VENDEDOR  ========================================================================================================== */


    $scope.nuevoReporte = function () {

        $scope.fechaInicio = fechaHoraInicio.split(" ")[0];
        $scope.fechaFin = fechaHoraFin.split(" ")[0];
        $scope.horaInicio = fechaHoraInicio.split(" ")[1];
        $scope.horaFin = fechaHoraFin.split(" ")[1];
        $scope.parametrosReportesVendedor = parametrosReportesVendedor;

        $scope.reporte = {
            PuntosDeVenta: [], TipoDeComprobante: {}, Serie: {},
            FechaInicio: $scope.fechaInicio,
            FechaFinal: $scope.fechaFin,
            HoraInicio: $scope.formatoAMPM($scope.horaInicio),
            HoraFinal: $scope.formatoAMPM($scope.horaFin),
            Modalidades1: [],
            Modalidades2: [],
        };
        $scope.actualizarURLsVendedor();

        $scope.obtenerTiposDeComprobante();
        $scope.obtenerEntidadesInternasConRolPuntoVenta();
        $scope.obtenerEntidadesInternasConRolPuntoVentaNoVigentes();
        $scope.obtenerTiposDeComprobanteParaReporte();
        $scope.obtenerModalidadesGenerico();

    }


    $scope.actualizarURLsVendedor = function () {
        $scope.calcularIntervaloEnDias();

        $scope.actualizarURLReporteDeVentaPorComprobanteDetalladoDeUnSoloPuntoDeVenta();
        $scope.actualizarURLReporteDeVentaPorConceptoDeUnSoloPuntoDeVenta();
        $scope.actualizarURLReporteDeVentaPorSerieYConceptoBasicoResumidoDeUnSoloPuntoDeVenta();
        $scope.actualizarURLReporteDeVentaPorSerieYConceptoDeUnSoloPuntoDeVenta();
        $scope.actualizarURLReporteDeVentaPorConceptoDelVendedor();
        $scope.actualizarURLReporteDeVentasPorModalidadConsolidadoPorConceptoDelVendedor();
        $scope.actualizarURLReporteDeVentasPorModalidadConsolidadoPorPrecioUnitarioDelVendedor();
        $scope.actualizarURLReporteInvalidacionesVendedor();

    }

    //REPORTE DE PUNTO DE VENTA OR COMPROBANTE
    $scope.actualizarURLReporteDeVentaPorComprobanteDetalladoDeUnSoloPuntoDeVenta = function () {
        $scope.URLReporteDeVentaPorComprobanteDetalladoDeUnSoloPuntoDeVenta = URL_ + "/Venta/ReporteDeVentaPorComprobanteDetalladoDeUnSoloPuntoDeVenta?fechaInicio=" + $scope.reporte.FechaInicio + ' ' + $scope.reporte.HoraInicio + "&fechaFin=" + $scope.reporte.FechaFinal + ' ' + $scope.reporte.HoraFinal;
    }

    //REPORTE DE PUNTO DE VENTA POR CONCEPTO
    $scope.actualizarURLReporteDeVentaPorConceptoDeUnSoloPuntoDeVenta = function () {
        $scope.URLReporteDeVentaPorConceptoDeUnSoloPuntoDeVenta = URL_ + "/Venta/ReporteDeVentaPorConceptoDeUnSoloPuntoDeVenta?fechaInicio=" + $scope.reporte.FechaInicio + ' ' + $scope.reporte.HoraInicio + "&fechaFin=" + $scope.reporte.FechaFinal + ' ' + $scope.reporte.HoraFinal;
    }

    //REPORTE DE VENDEDOR POR CONCEPTO
    $scope.actualizarURLReporteDeVentaPorConceptoDelVendedor = function () {
        $scope.URLReporteDeVentaPorConceptoDelVendedor = URL_ + "/Reporte/ReporteDeVentaDelVendedorPorConcepto?fechaInicio=" + $scope.reporte.FechaInicio + ' ' + $scope.reporte.HoraInicio + "&fechaFin=" + $scope.reporte.FechaFinal + ' ' + $scope.reporte.HoraFinal;
    }

    //REPORTE DE PUNTO DE VENTA POR SERIE Y CATEGORIA
    $scope.actualizarURLReporteDeVentaPorSerieYConceptoBasicoResumidoDeUnSoloPuntoDeVenta = function () {
        $scope.URLReporteDeVentaPorSerieYConceptoBasicoResumidoDeUnSoloPuntoDeVenta = URL_ + "/Venta/ReporteDeVentaPorSerieYConceptoBasicoResumidoDeUnSoloPuntoDeVenta?fechaInicio=" + $scope.reporte.FechaInicio + ' ' + $scope.reporte.HoraInicio + "&fechaFin=" + $scope.reporte.FechaFinal + ' ' + $scope.reporte.HoraFinal;
    }

    //REPORTE DE PUNTO DE VENTA POR SERIE Y CONCEPTO
    $scope.actualizarURLReporteDeVentaPorSerieYConceptoDeUnSoloPuntoDeVenta = function () {
        $scope.URLReporteDeVentaPorSerieYConceptoDeUnSoloPuntoDeVenta = URL_ + "/Venta/ReportePorSerieYConceptoDeUnSoloPuntoDeVenta?fechaInicio=" + $scope.reporte.FechaInicio + ' ' + $scope.reporte.HoraInicio + "&fechaFin=" + $scope.reporte.FechaFinal + ' ' + $scope.reporte.HoraFinal;
    }

    //REPORTE DE VENTAS POR MODALIDAD CONSOLIDADO POR CONCEPTO
    $scope.actualizarURLReporteDeVentasPorModalidadConsolidadoPorConceptoDelVendedor = function () {
        var modalidades = "";
        if ($scope.reporte.Modalidades1) {
            for (var i = 0; i < $scope.reporte.Modalidades1.length; i++) {
                modalidades += "&modalidades=" + $scope.reporte.Modalidades1[i].Id;
            }
        }

        $scope.comprobarElementoEnArrayIngresado($scope.reporte.Modalidades1, 'input-report-venta-modalidad-consolidado-concepto-modalidades', 'btn-see-in-report-7', 'select-report-7');

        $scope.URLReporteDeVentasPorModalidadConsolidadoPorConceptoDelVendedor = URL_ + "/Reporte/VentasPorModalidadConsolidadoPorConcepto?fechaInicio=" + $scope.reporte.FechaInicio + ' ' + $scope.reporte.HoraInicio + "&fechaFin=" + $scope.reporte.FechaFinal + ' ' + $scope.reporte.HoraFinal + "&idVendedor=" + 0 + "&nombreVendedor=" + "" + modalidades;
    }

    //REPORTE DE VENTAS POR MODALIDAD CONSOLIDADO POR PRECIO UNITARIO
    $scope.actualizarURLReporteDeVentasPorModalidadConsolidadoPorPrecioUnitarioDelVendedor = function () {
        var modalidades = "";
        if ($scope.reporte.Modalidades2) {
            for (var i = 0; i < $scope.reporte.Modalidades2.length; i++) {
                modalidades += "&modalidades=" + $scope.reporte.Modalidades2[i].Id;
            }
        }

        $scope.comprobarElementoEnArrayIngresado($scope.reporte.Modalidades2, 'input-report-venta-modalidad-consolidado-precio-unitario-modalidades', 'btn-see-in-report-8', 'select-report-8');

        $scope.URLReporteDeVentasPorModalidadConsolidadoPorPrecioUnitarioDelVendedor = URL_ + "/Reporte/VentasPorModalidadConsolidadoPorPrecioUnitario?fechaInicio=" + $scope.reporte.FechaInicio + ' ' + $scope.reporte.HoraInicio + "&fechaFin=" + $scope.reporte.FechaFinal + ' ' + $scope.reporte.HoraFinal + "&idVendedor=" + 0 + "&nombreVendedor=" + "" + modalidades;
    }

    //REPORTE DE VENTAS POR FAMILIA CARACTERISTICA Y CONCEPTO

    $scope.actualizarURLReporteVentasPorFamiliaCaracteristica = function () {
        var nombres = [];
        if ($scope.reporte.PuntosDeVentaReporteVentasPorFamiliaCaracteristica != undefined)
            $scope.reporte.PuntosDeVentaReporteVentasPorFamiliaCaracteristica.forEach(ca => nombres.push(ca.Nombre));
        nombres = nombres.join(', ');
        var idsCentrosAtencion = [];
        if ($scope.reporte.PuntosDeVentaReporteVentasPorFamiliaCaracteristica != undefined)
            $scope.reporte.PuntosDeVentaReporteVentasPorFamiliaCaracteristica.forEach(ca => idsCentrosAtencion.push('&idsCentrosAtencion=' + ca.Id));
        $scope.URLReporteVentasPorFamiliaCaracteristica = URL_ + "/Reporte/VentasPorFamiliaCaracteristica?fechaInicio=" + $scope.reporte.FechaInicio + ' ' + $scope.reporte.HoraInicio +
            "&fechaFin=" + $scope.reporte.FechaFinal + ' ' + $scope.reporte.HoraFinal +
            "&idsCentrosAtencion=" + idsCentrosAtencion.join('') +
            "&nombresCentrosAtencion=" + nombres +
            "&idFamilia=" + $scope.reporte.FamiliaReporteVentasPorFamiliaCaracteristica.Id +
            "&nombreFamilia=" + $scope.reporte.FamiliaReporteVentasPorFamiliaCaracteristica.Nombre +
            "&idCaracteristica=" + $scope.reporte.CaracteristicaReporteVentasPorFamiliaCaracteristica.Id +
            "&nombreCaracteristica=" + $scope.reporte.CaracteristicaReporteVentasPorFamiliaCaracteristica.Nombre +
            "&idValorCaracteristica=" + $scope.reporte.ValorCaracteristicaReporteVentasPorFamiliaCaracteristica.Id +
            "&nombreValorCaracteristica=" + $scope.reporte.ValorCaracteristicaReporteVentasPorFamiliaCaracteristica.Nombre;

        var validado1 = $scope.comprobarIdEnArrayIngresado($scope.reporte.FamiliaReporteVentasPorFamiliaCaracteristica.Id, 'input-report-venta-familia-caracteristica-familia', 'btn-see-in-report-venta-familia-caracteristica', 'select-report-venta-familia-caracteristica-familia');
        if (validado1)
            var validado2 = $scope.comprobarIdEnArrayIngresado($scope.reporte.CaracteristicaReporteVentasPorFamiliaCaracteristica.Id, 'input-report-venta-familia-caracteristica-caracteristica', 'btn-see-in-report-venta-familia-caracteristica', 'select-report-venta-familia-caracteristica-caracteristica');
        if (validado2)
            var validado3 = $scope.comprobarIdEnArrayIngresado($scope.reporte.ValorCaracteristicaReporteVentasPorFamiliaCaracteristica.Id, 'input-report-venta-familia-caracteristica-valor-caracteristica', 'btn-see-in-report-venta-familia-caracteristica', 'select-report-venta-familia-caracteristica-valor-caracteristica');
        if (validado3)
            $scope.comprobarElementoEnArrayIngresado($scope.reporte.PuntosDeVentaReporteVentasPorFamiliaCaracteristica, 'input-report-venta-familia-caracteristica-punto-venta', 'btn-see-in-report-venta-familia-caracteristica', 'select-report-venta-caracteristica-puntoventa');

    };

    $scope.actualizarURLReporteVentasPorCaracteristica = function () {
        var nombres = [];
        if ($scope.reporte.PuntosDeVentaReporteVentasPorCaracteristica != undefined)
            $scope.reporte.PuntosDeVentaReporteVentasPorCaracteristica.forEach(ca => nombres.push(ca.Nombre));
        nombres = nombres.join(', ');
        var idsCentrosAtencion = [];
        if ($scope.reporte.PuntosDeVentaReporteVentasPorCaracteristica != undefined)
            $scope.reporte.PuntosDeVentaReporteVentasPorCaracteristica.forEach(ca => idsCentrosAtencion.push('&idsCentrosAtencion=' + ca.Id));
        $scope.URLReporteVentasPorCaracteristica = URL_ + "/Reporte/VentasPorValorCaracteristica?fechaInicio=" + $scope.reporte.FechaInicio + ' ' + $scope.reporte.HoraInicio +
            "&fechaFin=" + $scope.reporte.FechaFinal + ' ' + $scope.reporte.HoraFinal +
            "&idsCentrosAtencion=" + idsCentrosAtencion.join('') +
            "&nombresCentrosAtencion=" + nombres +
            "&idCaracteristica=" + $scope.reporte.CaracteristicaReporteVentasPorCaracteristica.Id +
            "&nombreCaracteristica=" + $scope.reporte.CaracteristicaReporteVentasPorCaracteristica.Nombre +
            "&idValorCaracteristica=" + $scope.reporte.ValorCaracteristicaReporteVentasPorCaracteristica.Id +
            "&nombreValorCaracteristica=" + $scope.reporte.ValorCaracteristicaReporteVentasPorCaracteristica.Nombre;

        var validado1 = $scope.comprobarIdEnArrayIngresado($scope.reporte.CaracteristicaReporteVentasPorCaracteristica.Id, 'input-report-venta-valor-caracteristica-caracteristica', 'btn-see-in-report-venta-valor-caracteristica', 'select-report-venta-valor-caracteristica-caracteristica');
        if (validado1)
            var validado2 = $scope.comprobarIdEnArrayIngresado($scope.reporte.ValorCaracteristicaReporteVentasPorCaracteristica.Id, 'input-report-venta-valor-caracteristica-valor-caracteristica', 'btn-see-in-report-venta-valor-caracteristica', 'select-report-venta-valor-caracteristica-valor-caracteristica');
        if (validado2)
            $scope.comprobarElementoEnArrayIngresado($scope.reporte.PuntosDeVentaReporteVentasPorCaracteristica, 'input-report-venta-valor-caracteristica-punto-venta', 'btn-see-in-report-venta-valor-caracteristica', 'select-report-venta-valor-caracteristica-punto-venta');
    };

    $scope.actualizarURLReporteVentasPorConcepto = function () {
        var nombres = [];
        if ($scope.reporte.PuntosDeVentaReporteVentasPorConcepto != undefined)
            $scope.reporte.PuntosDeVentaReporteVentasPorConcepto.forEach(ca => nombres.push(ca.Nombre));
        nombres = nombres.join(', ');
        var idsCentrosAtencion = [];
        if ($scope.reporte.PuntosDeVentaReporteVentasPorConcepto != undefined)
            $scope.reporte.PuntosDeVentaReporteVentasPorConcepto.forEach(ca => idsCentrosAtencion.push('&idsCentrosAtencion=' + ca.Id));
        $scope.URLReporteVentasPorConcepto = URL_ + "/Reporte/VentasPorConcepto?fechaInicio=" + $scope.reporte.FechaInicio + ' ' + $scope.reporte.HoraInicio +
            "&fechaFin=" + $scope.reporte.FechaFinal + ' ' + $scope.reporte.HoraFinal +
            "&idsCentrosAtencion=" + idsCentrosAtencion.join('') +
            "&nombresCentrosAtencion=" + nombres +
            "&idConcepto=" + $scope.reporte.ConceptoReporteVentasPorConcepto.Id +
            "&nombreConcepto=" + $scope.reporte.ConceptoReporteVentasPorConcepto.Nombre;

        var validado = $scope.comprobarIdEnArrayIngresado($scope.reporte.ConceptoReporteVentasPorConcepto.Id, 'input-report-venta-concepto-concepto', 'btn-see-in-report-venta-concepto', 'select-report-venta-concepto-concepto');
        if (validado)
            $scope.comprobarElementoEnArrayIngresado($scope.reporte.PuntosDeVentaReporteVentasPorConcepto, 'input-report-venta-concepto-punto-venta', 'btn-see-in-report-venta-concepto', 'select-report-venta-conepto-punto-venta');
    };


    $scope.actualizarURLReporteInvalidacionesVendedor = function () {

        if ($scope.IntervaloEnDias > $scope.parametrosReportesVendedor.maximoDiasInvalidaciones) {
            $scope.reporte.MensajeErrorInvalidaciones = "Maxima cantidad de días a mostrar: " + $scope.parametrosReportesVendedor.maximoDiasInvalidaciones;
        }
        else {
            $scope.reporte.MensajeErrorInvalidaciones = "";
        }
        $scope.URLReporteInvalidacionesVendedor = $scope.reporte.MensajeErrorInvalidaciones != "" ? '' : URL_ + "/Reporte/InvalidacionesVendedor?fechaInicio=" + $scope.reporte.FechaInicio + ' ' + $scope.reporte.HoraInicio + "&fechaFin=" + $scope.reporte.FechaFinal + ' ' + $scope.reporte.HoraFinal;
    }
});


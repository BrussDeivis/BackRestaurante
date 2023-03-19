app.controller('precioConceptoController', function ($scope, $q, $timeout, $compile, SweetAlert, caracteristicaService, conceptoService, precioService) {

    $scope.inicializar = function () {
        $scope.cargarParametros();
        $scope.limpiarRegistro();
        $scope.cargarColeccionesAsync();
    }

    $scope.cargarParametros = function () {
        $scope.numeroDecimalesEnPrecio = numeroDecimalesEnPrecio;
        $scope.mostrarBuscadorCodigoBarra = mostrarBuscadorCodigoBarra;
        $scope.modoSeleccionConcepto = modoSeleccionConcepto;
        $scope.modoSeleccionTipoFamilia = modoSeleccionTipoFamilia;
        $scope.minimoCaracteresBuscarConcepto = minimoCaracteresBuscarConcepto;
        $scope.tiempoEsperaBusquedaSelector = tiempoEsperaBusquedaSelector;
        $scope.informacionSelectorConcepto = informacionSelectorConcepto;
        $scope.idTarifaPorDefecto = idTarifaPorDefecto;
        $scope.etiquetaSeleccionadaPorDefecto = etiquetaSeleccionadaPorDefecto;
        $scope.altoEtiqueta3 = altoEtiqueta3;
        $scope.anchoEtiqueta3 = anchoEtiqueta3;
        $scope.filasEtiqueta3 = filasEtiqueta3;
        $scope.columnasEtiqueta3 = columnasEtiqueta3;
        $scope.fuenteEtiqueta3 = fuenteEtiqueta3;
        $scope.altoEtiqueta4 = altoEtiqueta4;
        $scope.anchoEtiqueta4 = anchoEtiqueta4;
        $scope.filasEtiqueta4 = filasEtiqueta4;
        $scope.columnasEtiqueta4 = columnasEtiqueta4;
        $scope.fuenteEtiqueta4 = fuenteEtiqueta4;
    }

    $scope.limpiarRegistro = function () {
        $scope.codigoBarraABuscar = "";
        $scope.conceptos = [];
        $scope.concepto = { Item: {} };
        $scope.precio = {};
        $scope.listaPrecios = [];
        $scope.familiaSeleccionada = {};
    }

    $scope.cargarColeccionesAsync = function () {
    }

    $scope.cargarColeccionesSync = function () {
    }

    $scope.establecerDatosPorDefecto = function () {
    }
    //#endregion

    //#region Manejo de los precios
    $scope.cargarConcepto = function () {
        $scope.listaPrecios = [];
        $scope.concepto.NombreEtiqueta = $scope.concepto.Nombre;
        $scope.concepto.NombreConPrecio = $scope.concepto.NombreDetalle;
        let precioPorDefecto = $scope.concepto.Precios.find(p => p.IdTarifa == $scope.idTarifaPorDefecto);
        $scope.concepto.PrecioPorDefecto = precioPorDefecto == undefined ? '0.00' : precioPorDefecto.ValorString;
        if ($scope.concepto.Precios.length == 1) {
            $scope.listaPrecios.push("S/ " + parseFloat($scope.concepto.Precios[0].Valor).toFixed($scope.numeroDecimalesEnPrecio));
            $scope.concepto.NombreConPrecio += ' | ' + $scope.concepto.Precios[0].Codigo + ' - ' + parseFloat($scope.concepto.Precios[0].Valor).toFixed($scope.numeroDecimalesEnPrecio);
        } else {
            for (var i = 0; i < $scope.concepto.Precios.length; i++) {
                $scope.listaPrecios.push("S/ " + parseFloat($scope.concepto.Precios[i].Valor).toFixed($scope.numeroDecimalesEnPrecio) + " (" + $scope.concepto.Precios[i].Codigo + ") ");
                $scope.concepto.NombreConPrecio += (i == 0) ? ' | ' : ' ; ';
                $scope.concepto.NombreConPrecio += $scope.concepto.Precios[i].Codigo + ' - ' + parseFloat($scope.concepto.Precios[i].Valor).toFixed($scope.numeroDecimalesEnPrecio);
            }
        }
        $scope.cargarCodigoBarraUnico();
        $scope.obtenerPreciosDeConcepto();
    }

    $scope.seleccionConcepto = function (conceptoComercial) {
        $scope.concepto = conceptoComercial;
        $scope.cargarCaracteristicasConceptoNegocio().then(function () {
            $scope.cargarConcepto();
            $scope.seleccionarEtiqueta($scope.etiquetaSeleccionadaPorDefecto);
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
    }

    $scope.cargarCaracteristicasConceptoNegocio = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        caracteristicaService.obtenerCaracteristicasYValoresDeCaracteristicasDeConceptoNegocio({ idConcepto: $scope.concepto.Id }).success(function (data) {
            $scope.concepto.NombreConceptoSinCaracteristicas = data.NombreConceptoSinCaracteristicas;
            $scope.concepto.Caracteristicas = data.Caracteristicas;
            defered.resolve();
        }).error(function (data) {
            SweetAlert.error2(data);
            defered.reject(data);
        });
        return promise;
    }

    $scope.seleccionarEtiqueta = function (numeroEtiqueta) {
        $scope.etiquetaImpresion = numeroEtiqueta;
        if (numeroEtiqueta == 1) {
            $scope.nombreEtiquetaSeleccionada = 'etiqueta-nombre-precio';
        } else if (numeroEtiqueta == 2) {
            $scope.nombreEtiquetaSeleccionada = 'etiqueta-codigo-barra';
        } else if (numeroEtiqueta == 3) {
            $scope.altoImpresion = $scope.altoEtiqueta3;
            $scope.anchoImpresion = $scope.anchoEtiqueta3;
            $scope.filasImpresion = $scope.filasEtiqueta3;
            $scope.columnasImpresion = $scope.columnasEtiqueta3;
            $scope.fuenteImpresion = $scope.fuenteEtiqueta3;
            $scope.nombreEtiquetaSeleccionada = 'etiqueta-nombre-precio-caracteristica-codigo-barra';
            $scope.cargarVistaEtiqueta3Impresion();
        } else if (numeroEtiqueta == 4) { 
            $scope.altoImpresion = $scope.altoEtiqueta4;
            $scope.anchoImpresion = $scope.anchoEtiqueta4;
            $scope.filasImpresion = $scope.filasEtiqueta4;
            $scope.columnasImpresion = $scope.columnasEtiqueta4;
            $scope.fuenteImpresion = $scope.fuenteEtiqueta4;
            $scope.nombreEtiquetaSeleccionada = 'etiqueta-nombre-precio-codigo-barra';
            $scope.cargarVistaEtiqueta4Impresion();
        }
        let radioEtiqueta = document.getElementById('etiqueta-' + numeroEtiqueta);
        radioEtiqueta.checked = true;
    }


    $scope.imprimirEtiqueta = function (sectionId) {
        var htmlContent = document.getElementById(sectionId).innerHTML;
        var ventanaImpresion = window.open(' ', 'popimpr');
        ventanaImpresion.document.write(htmlContent);
        ventanaImpresion.document.close();
        ventanaImpresion.print();
        ventanaImpresion.close();
    }

    $scope.cargarCodigoBarraUnico = function () {
        if ($scope.concepto.CodigoBarra != undefined) {
            if ($scope.concepto.CodigoBarra.length == 13) {
                $("#codigoBarra").barcode($scope.concepto.CodigoBarra, "ean13", { barWidth: 1, barHeight: 90 }); 
            } else {
                $("#codigoBarra").barcode($scope.concepto.CodigoBarra, "code39", { barWidth: 1, barHeight: 90 }); 
            }
        } else {
            document.getElementById("codigoBarra").innerHTML = ' ';
            $timeout(function () { $('#codigoBarra').trigger("change"); }, 100);
        }
    }

    $scope.obtenerPreciosDeConcepto = function () {
        if ($scope.concepto.Id > 0) {
            precioService.obtenerPreciosDeConceptoNegocio({ idConceptoNegocio: $scope.concepto.Id }).success(function (data) {
                $scope.precio = data;
                for (var i = 0; i < $scope.precio.PuntosDePrecio.length; i++) {
                    for (var j = 0; j < $scope.precio.PuntosDePrecio[i].Tarifas.length; j++) {
                        date = $scope.precio.PuntosDePrecio[i].Tarifas[j].FechaDesde.slice($scope.precio.PuntosDePrecio[i].Tarifas[j].FechaDesde.indexOf("(") + 1, $scope.precio.PuntosDePrecio[i].Tarifas[j].FechaDesde.indexOf(")"));
                        $scope.precio.PuntosDePrecio[i].Tarifas[j].FechaDesde = $scope.formatDate(new Date(+date), "ES");
                        $("#fechaDesde" + i + j).datepicker('setDate', $scope.precio.PuntosDePrecio[i].Tarifas[j].FechaDesde);
                        date = $scope.precio.PuntosDePrecio[i].Tarifas[j].FechaHasta.slice($scope.precio.PuntosDePrecio[i].Tarifas[j].FechaHasta.indexOf("(") + 1, $scope.precio.PuntosDePrecio[i].Tarifas[j].FechaHasta.indexOf(")"));
                        $scope.precio.PuntosDePrecio[i].Tarifas[j].FechaHasta = $scope.formatDate(new Date(+date), "ES");
                        $("#fechaHasta" + i + j).datepicker('setDate', $scope.precio.PuntosDePrecio[i].Tarifas[j].FechaHasta);
                    }
                }
                $(function () {
                    $('.td-datepicker').datepicker({
                        format: 'dd/mm/yyyy',
                        autoclose: true,
                        todayHighlight: true,
                        language: 'es'
                    });
                });
            }).error(function (data) {
                SweetAlert.error2(data);
            });
        }
    }

    $scope.guardarPrecio = function () {
        precioService.guardarPrecio({ registroPrecio: $scope.precio }).success(function (data) {
            $scope.cargarPrecioCreado();
            SweetAlert.success("Correcto", data.result_description);
        }).error(function (data, status) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }

    $scope.cargarPrecioCreado = function () {
        conceptoService.obtenerConceptoDeNegocioComercialPorIdConcepto({ idConceptoNegocio: $scope.concepto.Id, complementoStock: false, complementoPrecio: true }).success(function (data) {
            $scope.concepto = data;
            $scope.cargarCaracteristicasConceptoNegocio().then(function () {
                $scope.cargarConcepto();
            }, function (error) {
                $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
            });
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.cargarVistaEtiquetaImpresion = function () {
        if ($scope.etiquetaImpresion == 3) {
            $scope.cargarVistaEtiqueta3Impresion();
        } else if ($scope.etiquetaImpresion == 4) {
            $scope.cargarVistaEtiqueta4Impresion();
        }
    }

    $scope.cargarVistaEtiqueta3Impresion = function () {
        if ($("#divEtiqueta3")) {
            $("#divEtiqueta3").remove();
        }
        let numeroCodigoBarra = 1;
        let htmlImpresion = '<div id="divEtiqueta3"> <style> @@page { size: ' + $scope.anchoImpresion * $scope.columnasImpresion + 'mm ' + $scope.altoImpresion * $scope.filasImpresion + 'mm; } @@media print { .receipt { width: ' + $scope.anchoImpresion + 'mm; height:' + $scope.altoImpresion + 'mm; } } </style > <table>';
        for (var i = 0; i < $scope.filasImpresion; i++) {
            htmlImpresion += '<tr>';
            for (var j = 0; j < $scope.columnasImpresion; j++) {
                htmlImpresion += '<th> <div class="centrar-contenido imprimir-contenido receipt borde-delgado" style="width:' + $scope.anchoImpresion + 'mm; height:' + $scope.altoImpresion + 'mm; text-align:center; font-size: ' + $scope.fuenteImpresion + 'px;"> <p style="font-family: Consolas; margin: 0; font-weight:bold; white-space: nowrap; overflow: hidden; font-weight: bold; line-height: 10px;">{{ concepto.NombreConceptoSinCaracteristicas }}</p> <p style="font-family: Consolas; margin: 0; text-transform: capitalize; white-space: nowrap; overflow: hidden; font-weight: normal; line-height: 10px;" ng-repeat="caracteristica in concepto.Caracteristicas">{{ caracteristica.Nombre }}: {{ caracteristica.Valor }}</p> <p style="font-family: Consolas; margin: 1px 0px; line-height: 8px;">Precio: S/{{ concepto.PrecioPorDefecto }}</p> <div id="codigoBarra' + $scope.etiquetaImpresion + numeroCodigoBarra++ + '" style="font-family: Consolas; font-size: 8px;"></div> </div> </th>';
            }
            htmlImpresion += '</tr>';
        }
        htmlImpresion += '</table>  </div>';
        $("#etiqueta-nombre-precio-caracteristica-codigo-barra").append($compile(htmlImpresion)($scope));
        $scope.cargarCodigoBarra($scope.filasImpresion * $scope.columnasImpresion);
    }

    $scope.cargarVistaEtiqueta4Impresion = function () {
        if ($("#divEtiqueta4")) {
            $("#divEtiqueta4").remove();
        }
        let numeroCodigoBarra = 1;
        let htmlImpresion = '<div id="divEtiqueta4"> <style> @@page { size: ' + $scope.anchoImpresion * $scope.columnasImpresion + 'mm ' + $scope.altoImpresion * $scope.filasImpresion + 'mm; } @@media print { .receipt { width: ' + $scope.anchoImpresion + 'mm; height:' + $scope.altoImpresion + 'mm; } } </style > <table>';
        for (var i = 0; i < $scope.filasImpresion; i++) {
            htmlImpresion += '<tr>';
            for (var j = 0; j < $scope.columnasImpresion; j++) {
                htmlImpresion += '<th> <div class="centrar-contenido imprimir-contenido receipt borde-delgado" style="width:' + $scope.anchoImpresion + 'mm; height:' + $scope.altoImpresion + 'mm; text-align:center; font-size: ' + $scope.fuenteImpresion + 'px;"> <p style="font-family: Consolas; margin: 0; font-weight:bold; overflow: hidden; font-weight: bold; line-height: 10px;">{{ concepto.NombreEtiqueta }}</p> <p style="font-family: Consolas; margin: 1px 0px; font-weight: normal; line-height: 10px;">Precio: S/{{ concepto.PrecioPorDefecto }}</p> <div id="codigoBarra' + $scope.etiquetaImpresion + numeroCodigoBarra++ + '" style="font-family: Consolas; font-size: 8px;"></div> </div> </th>';
            }
            htmlImpresion += '</tr>';
        }
        htmlImpresion += '</table>  </div>';
        $("#etiqueta-nombre-precio-codigo-barra").append($compile(htmlImpresion)($scope));
        $scope.cargarCodigoBarra($scope.filasImpresion * $scope.columnasImpresion);
    }


    $scope.cargarCodigoBarra = function (numeroEtiquetas) {
        for (var i = 1; i <= numeroEtiquetas; i++) {
            if ($scope.concepto.CodigoBarra != undefined) {
                if ($scope.concepto.CodigoBarra.length == 13) {
                    $("#codigoBarra" + $scope.etiquetaImpresion + i).barcode($scope.concepto.CodigoBarra, "ean13", { barWidth: 1, barHeight: 30 });
                } else {
                    $("#codigoBarra" + $scope.etiquetaImpresion + i).barcode($scope.concepto.CodigoBarra, "code39", { barWidth: 1, barHeight: 30 });
                }
            } else {
                document.getElementById("codigoBarra" + $scope.etiquetaImpresion + i).innerHTML = ' ';
                $timeout(function () { $('#codigoBarra' + $scope.etiquetaImpresion + i).trigger("change"); }, 100);
            }
        }
    }
    //#endregion
});

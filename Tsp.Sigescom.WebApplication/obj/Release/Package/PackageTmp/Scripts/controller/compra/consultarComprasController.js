app.controller('consultarComprasController', function ($scope, $timeout, $rootScope, SweetAlert, $filter, DTOptionsBuilder,SweetAlert, DTColumnDefBuilder, compraService, maestroService,$rootScope) {

    //$scope.compra = { Detalles: [] };
    //$scope.proveedores = [];
    //$scope.productos = [];
    //$scope.tiposDeComprobante = [];
    //$scope.detalle = {};
    //$scope.financiamientodetalle = {};
    //$scope.cuotasdetalle = 

    $scope.compras = { lista: [] };
    $scope.bandeja = {};
    $scope.mostrarTrazabilidadDeConcepto = permitirLoteEnDetalleDeCompra;
    $scope.permitirLoteEnDetalleDeCompra = permitirLoteEnDetalleDeCompra;
    $scope.permitirRegistroEnDetalleDeCompra = permitirRegistroEnDetalleDeCompra;
    $scope.permitirVencimientoEnDetalleDeCompra = permitirVencimientoEnDetalleDeCompra;
    $scope.verCompra = { Detalles: [] };

    $scope.comprobanteSeleccionado = [];
    //$scope.diavencimiento = {
    //    Lista: [{ id: 1, nombre: "1 de cada mes" }, { id: 2, nombre: "2 de cada mes" }, { id: 3, nombre: "3 de cada mes" }, { id: 4, nombre: "4 de cada mes" }, { id: 5, nombre: "5 de cada mes" }, { id: 6, nombre: "6 de cada mes" }, { id: 7, nombre: "7 de cada mes" }, { id: 8, nombre: "8 de cada mes" }, { id: 9, nombre: "9 de cada mes" }, { id: 10, nombre: "10 de cada mes" },
    //            { id: 11, nombre: " 11 de cada mes" }, { id: 12, nombre: "12 de cada mes" }, { id: 13, nombre: "13 de cada mes" }, { id: 14, nombre: "14 de cada mes" }, { id: 15, nombre: "15 de cada mes" }, { id: 16, nombre: "16 de cada mes" }, { id: 17, nombre: "17 de cada mes" }, { id: 18, nombre: "18 de cada mes" }, { id: 19, nombre: "19 de cada mes" }, { id: 20, nombre: "20 de cada mes" },
    //            { id: 21, nombre: "21 de cada mes" }, { id: 22, nombre: "22 de cada mes" }, { id: 23, nombre: "23 de cada mes" }, { id: 24, nombre: "24 de cada mes" }, { id: 25, nombre: "25 de cada mes" }, { id: 26, nombre: "26 de cada mes" }, { id: 27, nombre: "27 de cada mes" }, { id: 28, nombre: "28 de cada mes" }, { id: 29, nombre: "Antepenultimo dia de cada mes" }, { id: 30, nombre: "Penultimo dia de cada mes" }, { id: 0, nombre: "Ultimo dia de cada mes" }, ]
    //};

    //$scope.nuevoRegistro = function () {
    //    $scope.compra = {};
    //    $scope.compra = {Detalles:[]};
    //    $scope.compraInicial = {};
    //    $scope.compraInicial = angular.copy($scope.compra);
    //    $scope.obtenerProveedores();
    //    $scope.obtenerTiposDeComprobante();
    //    $scope.obtenerProductos();
    //} 
    $scope.limpiarObservacion = function () {
        $scope.invalidacion = {};
    }
    $scope.invalidar = function () {
        compraService.invalidarCompra({ idOrden: $scope.IdOrden, observacion: $scope.invalidacion.observacion }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $('#modal-anulacion-interna').modal('hide');
            $scope.EsAnuladoConNotaInterna = false;
            $scope.EsAnuladoConNotaDeCredito = false;
            $scope.comprobanteSeleccionado = [];
            $scope.inicio();
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema",data.error);
        });
    }
    $scope.invalidarAnulacionDeCompra = function () {
        compraService.invalidarAnulacionDeCompra({ idOrden: $scope.IdOrden, observacion: $scope.invalidacion.observacion }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $('#modal-anulacion-interna').modal('hide');
            $scope.EsAnuladoConNotaInterna = false;
            $scope.EsAnuladoConNotaDeCredito = false;
            $scope.comprobanteSeleccionado = [];
            $scope.inicio();
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema", data.error);
        });
    }
    $scope.anular = function () {
        compraService.anularCompra({ anulacion: $scope.anulacion }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $('#modal-anulacion-nota-credito').modal('hide');
            $scope.EsAnuladoConNotaInterna = false;
            $scope.EsAnuladoConNotaDeCredito = false;
            $scope.comprobanteSeleccionado = [];
            $scope.inicio();
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema", data.error);
        });
    }
    $scope.cargarDatosParaAnulacion = function () {
        $scope.anulacion = {};
        $scope.obtenerOrdenDeCompraParaAnulacion($scope.verCompra.IdOrden);
        $scope.obtenerTiposDeComprobante();
    }
    $scope.obtenerTiposDeComprobante = function () {
        compraService.obtenerTiposDeComprobanteParaAnulacionCompra({}).success(function (data) {
            $scope.selectorTiposDeComprobantesMasSeries = data;
            $scope.tiposDeComprobantes = [];
            for (var i = 0; i < $scope.selectorTiposDeComprobantesMasSeries.length; i++) {
                $scope.tiposDeComprobantes.push($scope.selectorTiposDeComprobantesMasSeries[i]);
            }
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema", data.error);
        });
    }
    $scope.cargarSeries = function (tipoComprobante) {
        if (tipoComprobante != null) {
            $scope.series = angular.copy(tipoComprobante.Series);
        }
    }
    $scope.obtenerOrdenDeCompraParaAnulacion = function (idOrdenCompra) {
        compraService.obtenerOrdenCompraYDetallesOrden({ idOrdenCompra: idOrdenCompra }).success(function (data) { 
            $scope.anulacion = data;
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema", data.error);
        });
    }
    $scope.listarBandeja = function (desde, hasta) {
        compraService.obtenerCompras({ desde: desde, hasta: hasta }).success(function (data) {
            $scope.compras.lista = data;
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema", data.error);
        });
    }


    $scope.verDetallesCompra = function (item) {
        compraService.obtenerOrdenCompraYDetallesOrden({ idOrdenCompra: item.Id }).success(function (data) { 
            $scope.verCompra = data; 
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema", data.error);
        });
    }
    $scope.existe = function (item) {
        return $scope.comprobanteSeleccionado.indexOf(item) > -1;
    }
    $scope.seleccionarComprobante = function (item) {
        var idx = $scope.comprobanteSeleccionado.indexOf(item);

        if (idx > -1) {
            $scope.EsAnuladoConNotaInterna = false;
            $scope.EsAnuladoConNotaDeCredito = false;
            $scope.comprobanteSeleccionado = [];
        }
        else {
            $scope.EsAnuladoConNotaInterna = item.EsAnuladoConNotaInterna;
            $scope.EsOrdenDeCompra = item.EsOrdenDeCompra;
            $scope.EsAnuladoConNotaDeCredito = item.EsAnuladoConNotaDeCredito;
            $scope.comprobanteSeleccionado = [];
            $scope.comprobanteSeleccionado.push(item);
            $scope.IdOrden = item.Id; 
        }
    };
    //$scope.obtenerTiposDeComprobante = function () {
    //    maestroService.obtenerTiposDeComprobanteParaCompra({}).success(function (data) {
    //        $scope.tiposDeComprobante = data;
    //    }).error(function (data) {
    //        $scope.messageError(data.error);
    //    });
    //}
    //$scope.obtenerProductos = function () {
    //    maestroService.obtenerProductosCompra({}).success(function (data) {
    //        $scope.productos = data;
    //    }).error(function (data) {
    //        $scope.messageError(data.error);
    //    });
    //}
    //$scope.obtenerProveedores = function () {
    //    compraService.obtenerProveedores({}).success(function (data) {
    //        $scope.proveedores = data;
    //    }).error(function (data) {
    //        $scope.messageError(data.error);
    //    });
    //}

    //$scope.agregarDetalle = function () {
    //    var validar = false;
    //    var index = 0;

    //    if ($scope.compra.Detalles.length > 0) {
    //        for (var i = 0; i < $scope.compra.Detalles.length; i++) {
    //            if ($scope.detalle.Producto.Id == $scope.compra.Detalles[i].Producto.Id) {
    //                validar = true;
    //                index = i;
    //                break;
    //            }
    //        }
    //    }
    //    if (validar) {
    //        $scope.agregarCantidad($scope.compra.Detalles, index);
    //        $scope.detalle = {};
    //    }
    //    else {
    //        $scope.detalle.Cantidad = 1;
    //        $scope.detalle.Descuento = 0;
    //        $scope.detalle.PrecioUnitario = $scope.detalle.Producto.PrecioUnitario;
    //        $scope.detalle.Igv = 0;
    //        $scope.detalle.ValorCompra = ($scope.detalle.PrecioUnitario * $scope.detalle.Cantidad);
    //        $scope.detalle.Importe = $scope.detalle.ValorCompra + $scope.detalle.Igv - $scope.detalle.Descuento;
    //        $scope.detalle.VersionFila = $scope.detalle.Producto.VersionFila;
    //        $scope.VersionFila = $scope.detalle.Producto.VersionFila;

    //        $scope.compra.Detalles.push($scope.detalle);
    //        $scope.detalle = {};
    //        $scope.calcularTotal($scope.compra.Detalles);
    //    }
    //}

    //$scope.calcularImporte = function (detalles, index) {
    //    detalles[index].ValorCompra = detalles[index].PrecioUnitario * detalles[index].Cantidad;
    //    detalles[index].Igv = detalles[index].ConIgv ? detalles[index].ValorCompra * tasaIGV : 0;
    //    detalles[index].Importe = parseFloat(detalles[index].ValorCompra) + detalles[index].Igv - detalles[index].Descuento;
    //    $scope.calcularTotal(detalles);
    //}
    //$scope.agregarCantidad = function (detalles, index) {

    //    detalles[index].Cantidad++;
    //    detalles[index].Importe = detalles[index].Cantidad * detalles[index].PrecioUnitario;
    //    $scope.calcularTotal(detalles);
    //}
    //$scope.quitarCantidad = function (detalles, index) {
    //    detalles[index].Cantidad--;
    //    if (detalles[index].Cantidad != 0) {
    //        detalles[index].Importe = detalles[index].Cantidad * detalles[index].PrecioUnitario;
    //        $scope.calcularTotal($scope.compra.Detalles);
    //    } else {
    //        $scope.quitarDetalle(index);
    //    }

    //}

    //$scope.numeroComprobanteEsValido = function () {
    //    if ($scope.compra.Proveedor && $scope.compra.TipoDeComprobante && $scope.compra.NumeroSerieDeComprobante && $scope.compra.NumeroDeComprobante) {
    //        compraService.numeroComprobanteEsValido({
    //            idProveedor: $scope.compra.Proveedor.Id, idTipoComprobante: $scope.compra.TipoDeComprobante.Id,
    //            numeroDeSerie: $scope.compra.NumeroSerieDeComprobante, numeroComprobante: $scope.compra.NumeroDeComprobante
    //        }).success(function (data) {
    //            if (data.valido) {
    //                alert("El Proveedor: " + $scope.compra.Proveedor.RazonSocial + " ya tiene una " + $scope.compra.TipoDeComprobante.Nombre + " registrado con el número de comprobante " + $scope.compra.NumeroDeComprobante + ", por favor ingrese otro número");
    //                $scope.compra.NumeroDeComprobante = "";
    //            }
    //        }).error(function (data) { alert("ERROR"); });
    //    }
    //}

    //$scope.quitarDetalle = function (index) {
    //    $scope.compra.Detalles.splice(index);
    //    $scope.calcularTotal($scope.compra.Detalles);
    //}
    //$scope.calcularTotal = function (detalles) {
    //    $scope.compra.SubTotal = 0;
    //    $scope.compra.Igv = 0;
    //    $scope.compra.Descuento = 0;
    //    $scope.compra.Total = 0;
    //    for (i = 0; i < detalles.length; i++) {
    //        $scope.compra.Total += detalles[i].Importe;
    //        $scope.compra.Descuento += detalles[i].Descuento;
    //        $scope.compra.Igv += detalles[i].Igv;
    //        $scope.compra.SubTotal += detalles[i].ValorCompra;
    //    }
    //}

    $scope.cerrar = function () {
        $scope.verCompra = { Detalles: [] };

    }

    //$scope.close = function () {
    //    if (angular.equals($scope.compra, $scope.compraInicial)) {
    //        $('#modal-registro').modal('hide');
    //        $scope.verCompra = { Detalles: [] };
    //    }
    //    else {
    //        $('#modal-pregunta').click();
    //    }
    //}
    //$scope.closeModal = function () {
    //    $('#modal-registro').modal('hide');
    //    $scope.verCompra = {};
    //}
    //$scope.agregarCuotas = function () {
    //    $scope.financiamientodetalle = { cuota: 1, capital: $scope.compra.Total, total: $scope.compra.Total, interes: "" };
    //    $scope.cuotasdetalle = [];
    //    if ($scope.compra.Credito) {
    //        $("#cuota-modal").click();
    //    }
    //}
    //$scope.calcularTotalFinanciamiento = function () {
    //    $scope.financiamientodetalle.capital = $scope.financiamientodetalle.inicial > 0 || $scope.financiamientodetalle.inicial != "" ? $scope.compra.Total - $scope.financiamientodetalle.inicial : $scope.compra.Total;
    //    $scope.financiamientodetalle.total = $scope.financiamientodetalle.interes > 0 || $scope.financiamientodetalle.interes != "" ? $scope.financiamientodetalle.capital + ($scope.financiamientodetalle.capital * $scope.financiamientodetalle.interes / 100) : $scope.financiamientodetalle.capital;
    //}
    //$scope.generarCuota = function () {
    //    $scope.cuotasdetalle = [];
    //    $scope.cuotadetalle = {};
    //    var fecha_actual = new Date(), anio = fecha_actual.getFullYear();
    //    var temp = $scope.financiamientodetalle.cuota;
    //    var mes = fecha_actual.getDate() < $scope.financiamientodetalle.diavencimiento ? fecha_actual.getMonth() : fecha_actual.getMonth() + 1;
    //    var dia = $scope.financiamientodetalle.diavencimiento;
    //    if ($scope.financiamientodetalle.inicial > 0 || $scope.financiamientodetalle.inicial != "") {
    //        $scope.cuotadetalle = { CapitalCuota: $scope.financiamientodetalle.inicial, InteresCuota: 0.00, ImporteCuota: $scope.financiamientodetalle.inicial, FechaVencimiento: $scope.formatDate(new Date(), "ES") }
    //        $scope.cuotasdetalle.push($scope.cuotadetalle);
    //        $scope.cuotadetalle = {};
    //    }
    //    for (i = 0; i < temp; i++) {
    //        $scope.cuotadetalle.CapitalCuota = $scope.formatNumber(parseFloat($scope.financiamientodetalle.capital) / parseFloat(temp), 2);
    //        $scope.cuotadetalle.InteresCuota = $scope.financiamientodetalle.interes > 0 || $scope.financiamientodetalle.interes != "" ? $scope.formatNumber(parseFloat($scope.financiamientodetalle.capital * $scope.financiamientodetalle.interes / 100) / parseFloat(temp), 2) : 0.00;
    //        $scope.cuotadetalle.ImporteCuota = $scope.formatNumber(parseFloat($scope.financiamientodetalle.Total) / parseFloat(temp), 2);

    //        if (mes == 13) {
    //            mes = 1;
    //            anio++;
    //        }

    //        if (mes == 2 && (dia == 28 || dia == 29)) {
    //            $scope.cuotadetalle.FechaVencimiento = $scope.formatDate(new Date(anio, mes, 0), "ES");
    //        } else if ((mes == 1 || mes == 3 || mes == 5 || mes == 7 || mes == 8 || mes == 10 || mes == 12) && dia == 31) {
    //            $scope.cuotadetalle.FechaVencimiento = $scope.formatDate(new Date(anio, mes, 0), "ES");
    //        } else {
    //            $scope.cuotadetalle.FechaVencimiento = $scope.formatDate(new Date(anio, mes, dia), "ES");
    //        }
    //        $scope.cuotasdetalle.push($scope.cuotadetalle);
    //        $scope.cuotadetalle = {};
    //        mes++;
    //    }
    //}
    //$scope.guardarEnModal = function () {
    //    compraService.guardarCompra({ compra: $scope.compra, cuotasdetalle: $scope.cuotasdetalle, inicial: $scope.financiamientodetalle.inicial }).success(function (data) {
    //        $scope.messageSuccess(data.result_description);
    //        $('#modal-registro').modal('hide');
    //        if ($scope.bandeja.FechaInicio != "" && $scope.bandeja.FechaFinal!=""){
    //            $scope.listarBandeja($scope.bandeja.FechaInicio, $scope.bandeja.FechaFinal);
    //        }
    //    }).error(function (data) {
    //        $scope.messageError(data.error);
    //    });
    //}
    $scope.inicio = function () {
        $scope.bandeja.FechaInicio = Desde;
        $scope.bandeja.FechaFinal = Hasta;
        $scope.listarBandeja(Desde, Hasta);
    }
    $scope.inicio();


    

});
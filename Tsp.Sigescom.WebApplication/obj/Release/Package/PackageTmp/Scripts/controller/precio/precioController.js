app.controller('precioController', function ($scope, DTOptionsBuilder, DTColumnDefBuilder, SweetAlert, precioService, maestroService, centroDeAtencionService, productoService) {

    //************************BANDEJA DE PRECIOS *******************//

    $scope.inicializarPrecios = function () {
        $scope.obtenerPrecios();
        $scope.obtenerConceptos();
        $scope.numeroDecimalesEnPrecio = numeroDecimalesEnPrecio;
    }

    $scope.obtenerPrecios = function () {
        precioService.obtenerPrecios({}).success(function (data) {
            $scope.precios = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    //**********************REGISTRO DE PRECIOS ********************//

    $scope.inicializar = function () {
        $scope.precio = {};
        $scope.conceptoSeleccionado = {};
    }

    $scope.obtenerConceptos = function () {
        maestroService.obtenerFamiliasConceptosComercialesVigentes({}).success(function (data) {
            $scope.conceptos = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.obtenerProductos = function () {
        if ($scope.conceptoBasicoSeleccionado.Id > 0) {
            productoService.obtenerMercaderiasPorConceptoBasicoIncluyendoStockYPrecios({ idConceptoBasico: $scope.conceptoBasicoSeleccionado.Id }).success(function (data) {
                $scope.productos = data;
            }).error(function (data) {
                SweetAlert.error("Ocurrio un Problema", data.error);
            });
        }
    }

    $scope.obtenerPreciosDeConcepto = function () {
        if ($scope.conceptoSeleccionado.Id > 0) {
            precioService.obtenerPreciosDeConceptoNegocio({ idConceptoNegocio: $scope.conceptoSeleccionado.Id }).success(function (data) {
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
                SweetAlert.error("Ocurrio un Problema", data.error);
            });
        }
    }

    $scope.verificarFecha = function (parentIndex, index, esFechaDesde) {
        var fechaDesde = new Date($scope.precio.PuntosDePrecio[parentIndex].Tarifas[index].FechaDesde.split("/")[1] + '-' + $scope.precio.PuntosDePrecio[parentIndex].Tarifas[index].FechaDesde.split("/")[0] + '-' + $scope.precio.PuntosDePrecio[parentIndex].Tarifas[index].FechaDesde.split("/")[2]);
        var fechaHasta = new Date($scope.precio.PuntosDePrecio[parentIndex].Tarifas[index].FechaHasta.split("/")[1] + '-' + $scope.precio.PuntosDePrecio[parentIndex].Tarifas[index].FechaHasta.split("/")[0] + '-' + $scope.precio.PuntosDePrecio[parentIndex].Tarifas[index].FechaHasta.split("/")[2]);
        if (fechaDesde > fechaHasta) {
            SweetAlert.warning("Advertencia", "La fecha desde no puede ser mayor a la fecha hasta, se cambio la fecha " + (esFechaDesde ? "desde" : "hasta"));
            if (esFechaDesde) {
                $scope.precio.PuntosDePrecio[parentIndex].Tarifas[index].FechaDesde = $scope.formatDate(fechaHasta, "ES");
            }
            else {
                $scope.precio.PuntosDePrecio[parentIndex].Tarifas[index].FechaHasta = $scope.formatDate(fechaDesde, "ES");
            }
            $("#fecha" + (esFechaDesde ? "Desde" : "Hasta") + parentIndex + index).datepicker('setDate', (esFechaDesde ? $scope.precio.PuntosDePrecio[parentIndex].Tarifas[index].FechaDesde : $scope.precio.PuntosDePrecio[parentIndex].Tarifas[index].FechaHasta));
        }
    }
    $scope.guardarPrecio = function () {
        precioService.guardarPrecio({ registroPrecio: $scope.precio }).success(function (data) {
            $scope.obtenerPrecios();
            SweetAlert.success("Correcto", data.result_description);
            $scope.cerrar('modal-registro-precio');
        }).error(function (data, status) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }

    $scope.caducarPrecio = function (item) {
        precioService.caducarPrecio({ idPrecio: item.Id }).success(function (data) {
            $scope.messageSuccess(data.result_description);
            $scope.obtenerPrecios();
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.cerrar = function (nombreDelModal) {
        $scope.precio = {};
        $('#' + nombreDelModal).modal('hide');
    }
});
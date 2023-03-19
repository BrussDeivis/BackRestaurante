app.controller('reporteConceptoController', function ($scope, $rootScope, $timeout, conceptoService, maestroService, centroDeAtencionService, SweetAlert) {

    $scope.almacenes = [];
    $scope.familias = [];
    $scope.familiasReporteStockPorEstado = [];
    $scope.estadoBajoReporteStock = true;
    $scope.estadoModeradoReporteStock = true;
    $scope.estadoConfortableReporteStock = true;

    $(".familia-reporte-estado").select2({
        maximumSelectionLength: 10
    });

    $scope.inicializarReporteAlmacenero = function () {
        $scope.obtenerAlmacenes();
        $scope.obtenerFamilias();
    }

    $scope.obtenerAlmacenes = function () {
        centroDeAtencionService.obtenerCentrosDeAtencionConRolAlmacen().success(function (data) {
            $scope.almacenes = data;
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.obtenerFamilias = function () {
        maestroService.obtenerFamiliasConceptosComercialesVigentes({}).success(function (data) {
            $scope.familias = data;
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }
    //REPORTE DE STOCK ACTUAL
    $scope.actualizarURLReporteStockActualGeneral = function () {
        var idsEntidadInterna = ""
        for (var i = 0; i < $scope.almacenesReporteStockGeneral.length; i++) {
            idsEntidadInterna += "&idsEntidadInterna=" + $scope.almacenesReporteStockGeneral[i].Id;
        }
        $scope.URLReporteStockActualGeneral = URL_ + "/Almacen/ReporteStockActualEnGeneral?" + idsEntidadInterna;
    }
    //REPORTE DE STOCK POR ESTADO
    $scope.actualizarURLReporteStockPorEstado = function () {
        var hayFamiliasSeleccionadas = "&hayFamiliasSeleccionadas=";
        var idsFamilia = "";
        if ($scope.familiasReporteStockPorEstado != undefined) {
            if ($scope.familiasReporteStockPorEstado.length > 0) {
                hayFamiliasSeleccionadas += 'true';
                for (var i = 0; i < $scope.familiasReporteStockPorEstado.length; i++) {
                    idsFamilia += "&idsFamilia=" + $scope.familiasReporteStockPorEstado[i].Id;
                }
            } else {
                hayFamiliasSeleccionadas += 'false';
                idsFamilia += "&idsFamilia=0"
            }
        } else {
            hayFamiliasSeleccionadas += 'false';
            idsFamilia += "&idsFamilia=0"
        }
        var estadoBajo = "&estadoBajo=" + $scope.estadoBajoReporteStock;
        var estadoModerado = "&estadoModerado=" + $scope.estadoModeradoReporteStock;
        var estadoConfortable = "&estadoConfortable=" + $scope.estadoConfortableReporteStock;
        $scope.URLReporteStockPorEstado = URL_ + "/Almacen/ReporteStockPorEstado?&idAlmacen=" + $scope.almacenReporteStockPorEstado.Id + "&nombreAlmacen=" + $scope.almacenReporteStockPorEstado.Nombre + hayFamiliasSeleccionadas + idsFamilia + estadoBajo + estadoModerado + estadoConfortable;
    }
});
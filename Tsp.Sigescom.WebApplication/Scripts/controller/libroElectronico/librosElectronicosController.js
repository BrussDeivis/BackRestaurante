app.controller('librosElectronicosController', function ($scope, $rootScope, librosElectronicosService, SweetAlert) {

    $scope.listaDePeriodos = [];
    $scope.periodoSeleccionado = {};

    //Obtine Libros Electronicos
    $scope.obtenerPeriodosDeLibrosElectronicos = function () {
        librosElectronicosService.obtenerPeriodosDeLibrosElectronicos({ idPeriodo: $scope.periodoSeleccionado.Id }).success(function (data) {
            $scope.listaDePeriodos = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    //Eliminar Libros Electronicos
    $scope.eliminarLibrosElectronicos = function () {
        librosElectronicosService.eliminarLibrosElectronicos({ idPeriodo: $scope.periodoSeleccionado.Id }).success(function (data) {

        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    //Genera Libros Electronicos
    $scope.generarLibrosElectronicos = function () {
        librosElectronicosService.generarLibrosElectronicos({ idPeriodo: $scope.periodoSeleccionado.Id }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
        }).error(function (data) {
            SweetAlert.error("Ocurrio un Problema", data.error);

        });
    }

    $scope.obtenerPeriodosDeLibrosElectronicos();

    ////Ventas e Ingresos Detallada sin Concepto
    //$scope.actualizarURLReporteDeVentaEIngresosSinConcepto = function () {//usa Contador
    //    $scope.URLReporteDeVentaEIngresosSinConcepto = URL + "/LibrosElectronicos/ReporteDeVentasEIngresosSinConcepto?idPeriodo=" + $scope.periodoSeleccionado.Id;
    //}

    ////Reporte formato ADSOFT
    //$scope.atualizarURLReporteDeVentaFormatoAdsoft = function () {
    //    $scope.URLReporteDeVentaFormatoAdsoft = URL + "/LibrosElectronicos/ReporteDeVentaFormatoAdsoft?idPeriodo=" + $scope.periodoSeleccionado.Id;
    //}

    ////Reporte formato FOXCOM
    //$scope.atualizarURLReporteDeVentaFormatoFoxcom = function () {
    //    $scope.URLReporteDeVentaFormatoFoxcom = URL + "/LibrosElectronicos/ReporteDeVentaFormatoFoxcontBoletaDeVentaFactura?idPeriodo=" + $scope.periodoSeleccionado.Id;
    //}

    ////Reporte formato FOXCOM NC y ND
    //$scope.atualizarURLReporteDeVentaFormatoFoxcomNotaCreditoyDebito = function () {
    //    $scope.URLReporteDeVentaFormatoFoxcomNotaCreditoyDebito = URL + "/LibrosElectronicos/ReporteDeVentaFormatoFoxcontNotaCreditoyDebito?idPeriodo=" + $scope.periodoSeleccionado.Id;
    //}

    //$scope.actualizarURLS = function () {
    //    $scope.actualizarURLReporteDeVentaEIngresosSinConcepto();
    //    $scope.atualizarURLReporteDeVentaFormatoFoxcom();
    //    $scope.atualizarURLReporteDeVentaFormatoAdsoft();
    //    $scope.atualizarURLReporteDeVentaFormatoFoxcomNotaCreditoyDebito();
    //}

});



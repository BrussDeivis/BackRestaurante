KardexAlmacenController = function ($scope, $q, $compile, SweetAlert, $rootScope, $timeout, DTOptionsBuilder, DTColumnDefBuilder) {

    $scope.Kardex_FiltrosValidos = function () {
        return ($scope.reporteador.Kardex.FechaHasta != undefined && $scope.reporteador.Kardex.FechaHasta != undefined && $scope.reporteador.Kardex.AlmacenSeleccionado != undefined && $scope.reporteador.Kardex.ConceptoSeleccionado != undefined);

        $scope.reporteador.Kardex.Valorizado.ActivarBotonVer = ($scope.reporteador.Kardex.FechaHasta != undefined && $scope.reporteador.Kardex.FechaHasta != undefined && $scope.reporteador.Kardex.AlmacenSeleccionado != undefined && $scope.reporteador.Kardex.ConceptoSeleccionado != undefined);
    }
    $scope.Kardex_EvaluarBotonesVer = function () {
        $scope.reporteador.Kardex.Fisico.ActivarBotonVer = $scope.Kardex_FiltrosValidos();
        $scope.reporteador.Kardex.Valorizado.ActivarBotonVer = $scope.Kardex_FiltrosValidos();
    }

    $scope.Kardex_EstablecimientoChanged = function () {
        $scope.Kardex_SetAlmacenPorDefecto();
        $scope.Kardex_actualizarURLs();
    }

    $scope.Kardex_SetAlmacenPorDefecto = function () {
        $scope.reporteador.Kardex.AlmacenSeleccionado = $scope.reporteador.Kardex.EstablecimientoSeleccionado.CentrosAtencion[0];
    }

    $scope.Kardex_actualizarURLs = function () {
        if ($scope.Kardex_FiltrosValidos()) {
        $scope.Actualizar_URLKardex_Fisico();
        $scope.Actualizar_URLKardex_Valorizado();
            $scope.Kardex_EvaluarBotonesVer();
        }
    }

    $scope.ConcatenarAlmacenFechasYConcepto = function () {
        var resultado = "idAlmacen=" + $scope.reporteador.Kardex.AlmacenSeleccionado.Id
            + "&nombreAlmacen=" + $scope.reporteador.Kardex.AlmacenSeleccionado.Nombre
            + "&fechaDesde=" + (new Date($scope.reporteador.Kardex.FechaDesde)).toUTCString()
            + "&fechaHasta=" + (new Date($scope.reporteador.Kardex.FechaHasta)).toUTCString()
            + "&idConcepto=" + $scope.reporteador.Kardex.ConceptoSeleccionado.Id
            + "&nombreConcepto=" + $scope.reporteador.Kardex.ConceptoSeleccionado.Nombre;
        return resultado;
    }
    $scope.Actualizar_URLKardex_Fisico = function () {
        var almacenFechasYConcepto = $scope.ConcatenarAlmacenFechasYConcepto();
        $scope.URLKardex_Fisico = URL_ + "/AlmacenReportes/Kardex_Fisico?" + almacenFechasYConcepto;
    }
    $scope.Actualizar_URLKardex_Valorizado = function () {
        var almacenFechasYConcepto = $scope.ConcatenarAlmacenFechasYConcepto();
        $scope.URLKardex_Valorizado = URL_ + "/AlmacenReportes/Kardex_Valorizado?" + almacenFechasYConcepto;
        $scope.URLKardex_Valorizado = $scope.URLKardex_Valorizado.replace("#", " ")
    }


};




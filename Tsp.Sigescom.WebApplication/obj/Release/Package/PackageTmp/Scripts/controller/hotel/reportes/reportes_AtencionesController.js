AtencionesController = function ($scope, $q, $compile, SweetAlert, $rootScope, $timeout, DTOptionsBuilder, DTColumnDefBuilder) {

    $scope.Atenciones_EvaluarBotonesVer = function () {
        let NoExcedeMaximoDias_Atenciones = $scope.diferenciaDiasEntreFechas($scope.reporteador.Atenciones.FechaDesde, $scope.reporteador.Atenciones.FechaHasta) <= $scope.data.MaximoDiasReporteHotel;

        $scope.reporteador.Atenciones.Ingresos.ActivarBotonVer = ($scope.reporteador.Atenciones.FechaDesde != undefined && $scope.reporteador.Atenciones.FechaHasta != undefined && $scope.reporteador.Atenciones.EstablecimientoSeleccionado != undefined && NoExcedeMaximoDias_Atenciones);

        $scope.reporteador.Atenciones.Salidas.ActivarBotonVer = ($scope.reporteador.Atenciones.FechaDesde != undefined && $scope.reporteador.Atenciones.FechaHasta != undefined && $scope.reporteador.Atenciones.EstablecimientoSeleccionado != undefined && NoExcedeMaximoDias_Atenciones);

        $scope.reporteador.Atenciones.Anuladas.ActivarBotonVer = ($scope.reporteador.Atenciones.FechaDesde != undefined && $scope.reporteador.Atenciones.FechaHasta != undefined && $scope.reporteador.Atenciones.EstablecimientoSeleccionado != undefined && NoExcedeMaximoDias_Atenciones);

        $scope.reporteador.Atenciones.Mincetur.ActivarBotonVer = ($scope.reporteador.Atenciones.FechaDesde != undefined && $scope.reporteador.Atenciones.FechaHasta != undefined && $scope.reporteador.Atenciones.EstablecimientoSeleccionado != undefined);
    }

    $scope.Atenciones_EstablecimientoChanged = function () {
        $scope.Atenciones_ActualizarURLs();
    }

    $scope.Atenciones_ActualizarURLs = function () {
        $scope.Actualizar_URLAtenciones_Ingresos();
        $scope.Actualizar_URLAtenciones_Salidas();
        $scope.Actualizar_URLAtenciones_Anuladas();
        $scope.Actualizar_URLAtenciones_Mincetur();
        $scope.Atenciones_EvaluarBotonesVer();
    }

    $scope.ConcatenarEstablecimientoYFechas_Atenciones = function () {
        var resultado = "idEstablecimiento=" + $scope.reporteador.Atenciones.EstablecimientoSeleccionado.Id
            + "&nombreEstablecimiento=" + $scope.reporteador.Atenciones.EstablecimientoSeleccionado.Nombre
            + "&fechaDesde=" + (new Date($scope.reporteador.Atenciones.FechaDesde)).toUTCString()
            + "&fechaHasta=" + (new Date($scope.reporteador.Atenciones.FechaHasta)).toUTCString();
        return resultado;
    }

    $scope.Actualizar_URLAtenciones_Ingresos = function () {
        var establecimientoYFechas = $scope.ConcatenarEstablecimientoYFechas_Atenciones();
        $scope.URLAtenciones_Ingresos = URL_ + "/HotelReportes_Atenciones/Atenciones_Ingresos?" + establecimientoYFechas;
    }

    $scope.Actualizar_URLAtenciones_Salidas = function () {
        var establecimientoYFechas = $scope.ConcatenarEstablecimientoYFechas_Atenciones();
        $scope.URLAtenciones_Salidas = URL_ + "/HotelReportes_Atenciones/Atenciones_Salidas?" + establecimientoYFechas;
    }

    $scope.Actualizar_URLAtenciones_Anuladas = function () {
        var establecimientoYFechas = $scope.ConcatenarEstablecimientoYFechas_Atenciones();
        $scope.URLAtenciones_Anuladas = URL_ + "/HotelReportes_Atenciones/Atenciones_Anuladas?" + establecimientoYFechas;
    }

    $scope.Actualizar_URLAtenciones_Mincetur = function () {
        var establecimientoYFechas = $scope.ConcatenarEstablecimientoYFechas_Atenciones();
        $scope.URLAtenciones_Mincetur = URL_ + "/HotelReportes_Atenciones/Atenciones_FormularioT1?" + establecimientoYFechas;
    }


};




FacturacionController = function ($scope, $q, $compile, SweetAlert, $rootScope, $timeout, DTOptionsBuilder, DTColumnDefBuilder) {

    $scope.Facturacion_EvaluarBotonesVer = function () {
        let NoExcedeMaximoDias_Facturacion = $scope.diferenciaDiasEntreFechas($scope.reporteador.Facturacion.FechaDesde, $scope.reporteador.Facturacion.FechaHasta) <= $scope.data.MaximoDiasReporteHotel;

        $scope.reporteador.Facturacion.Facturadas.ActivarBotonVer = ($scope.reporteador.Facturacion.FechaDesde != undefined && $scope.reporteador.Facturacion.FechaHasta != undefined && $scope.reporteador.Facturacion.EstablecimientoSeleccionado != undefined && NoExcedeMaximoDias_Facturacion);

        $scope.reporteador.Facturacion.NoFacturadas.ActivarBotonVer = ($scope.reporteador.Facturacion.FechaDesde != undefined && $scope.reporteador.Facturacion.FechaHasta != undefined && $scope.reporteador.Facturacion.EstablecimientoSeleccionado != undefined && NoExcedeMaximoDias_Facturacion);

        $scope.reporteador.Facturacion.Incidentes.ActivarBotonVer = ($scope.reporteador.Facturacion.FechaDesde != undefined && $scope.reporteador.Facturacion.FechaHasta != undefined && $scope.reporteador.Facturacion.EstablecimientoSeleccionado != undefined && NoExcedeMaximoDias_Facturacion);
    }

    $scope.Facturacion_EstablecimientoChanged = function () {
        $scope.Facturacion_ActualizarURLs();
    }

    $scope.Facturacion_ActualizarURLs = function () {
        $scope.Actualizar_URLFacturacion_Facturadas();
        $scope.Actualizar_URLFacturacion_NoFacturadas();
        $scope.Actualizar_URLFacturacion_Incidentes();
        $scope.Facturacion_EvaluarBotonesVer();
    }

    $scope.ConcatenarEstablecimientoYFechas_Facturacion = function () {
        var resultado = "idEstablecimiento=" + $scope.reporteador.Facturacion.EstablecimientoSeleccionado.Id
            + "&nombreEstablecimiento=" + $scope.reporteador.Facturacion.EstablecimientoSeleccionado.Nombre
            + "&fechaDesde=" + (new Date($scope.reporteador.Facturacion.FechaDesde)).toUTCString()
            + "&fechaHasta=" + (new Date($scope.reporteador.Facturacion.FechaHasta)).toUTCString();
        return resultado;
    }

    $scope.Actualizar_URLFacturacion_Facturadas = function () {
        var establecimientoYFechas = $scope.ConcatenarEstablecimientoYFechas_Facturacion();
        $scope.URLFacturacion_Facturadas = URL_ + "/HotelReportes_Facturacion/Facturacion_Facturadas?" + establecimientoYFechas;
    }

    $scope.Actualizar_URLFacturacion_NoFacturadas = function () {
        var establecimientoYFechas = $scope.ConcatenarEstablecimientoYFechas_Facturacion();
        $scope.URLFacturacion_NoFacturadas = URL_ + "/HotelReportes_Facturacion/Facturacion_NoFacturadas?" + establecimientoYFechas;
    }

    $scope.Actualizar_URLFacturacion_Incidentes = function () {
        var establecimientoYFechas = $scope.ConcatenarEstablecimientoYFechas_Facturacion();
        $scope.URLFacturacion_Incidentes = URL_ + "/HotelReportes_Facturacion/Facturacion_Incidentes?" + establecimientoYFechas;
    }
};




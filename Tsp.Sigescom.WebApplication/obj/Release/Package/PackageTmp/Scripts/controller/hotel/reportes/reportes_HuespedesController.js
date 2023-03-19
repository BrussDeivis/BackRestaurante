HuespedesController = function ($scope, $q, $compile, SweetAlert, $rootScope, $timeout, DTOptionsBuilder, DTColumnDefBuilder) {

    $scope.Huespedes_EvaluarBotonesVer = function () {
        let NoExcedeMaximoDias_Huespedes = $scope.diferenciaDiasEntreFechas($scope.reporteador.Huespedes.FechaDesde, $scope.reporteador.Huespedes.FechaHasta) <= $scope.data.MaximoDiasReporteHotel;

        $scope.reporteador.Huespedes.RegistroHuespedes.ActivarBotonVer = ($scope.reporteador.Huespedes.FechaDesde != undefined && $scope.reporteador.Huespedes.FechaHasta != undefined && $scope.reporteador.Huespedes.EstablecimientoSeleccionado != undefined && NoExcedeMaximoDias_Huespedes);
    }

    $scope.Huespedes_EstablecimientoChanged = function () {
        $scope.Huespedes_ActualizarURLs();
    }

    $scope.Huespedes_ActualizarURLs = function () {
        $scope.Actualizar_URLHuespedes_RegistroHuespedes();
        $scope.Huespedes_EvaluarBotonesVer();
    }

    $scope.ConcatenarEstablecimientoYFechas_Huespedes = function () {
        var resultado = "idEstablecimiento=" + $scope.reporteador.Huespedes.EstablecimientoSeleccionado.Id
            + "&nombreEstablecimiento=" + $scope.reporteador.Huespedes.EstablecimientoSeleccionado.Nombre
            + "&fechaDesde=" + (new Date($scope.reporteador.Huespedes.FechaDesde)).toUTCString()
            + "&fechaHasta=" + (new Date($scope.reporteador.Huespedes.FechaHasta)).toUTCString();
        return resultado;
    }

    $scope.Actualizar_URLHuespedes_RegistroHuespedes = function () {
        var establecimientoYFechas = $scope.ConcatenarEstablecimientoYFechas_Huespedes();
        $scope.URLHuespedes_RegistroHuespedes = URL_ + "/HotelReportes_Huespedes/Huespedes_RegistroHuespedes?" + establecimientoYFechas;
    }

};




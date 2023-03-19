app.controller('reportesRestauranteController', function ($scope, $rootScope, SweetAlert) {

    $scope.inicializar = function () {
        $scope.reporteador = {}
        $scope.reporteador.fechaHoraInicio = fechaHoraInicio;
        $scope.reporteador.fechaHoraFin = fechaHoraFin;
        $scope.reporteador.establecimientos = establecimientos;
        $scope.reporteador.establecimientoSeleccionado = $scope.reporteador.establecimientos[0];

        $scope.reporteador.parametros = parametros;
        $scope.reporteAtenciones = {}
        $scope.reporteOrdenesPorConcepto = {}
        $scope.reporteOrdenesPorMozo = {}
        $scope.reporteOrdenesDetalladas = {}
        $scope.reporteDevolucionesEnOrdenes = {}
        $scope.reportePorModoAtenciones = {}

        $scope.actualizarURLReportes();
    }

    $scope.actualizarURLReportes = function () {
        $scope.actualizarURLReporteAtenciones();
        $scope.actualizarURLReporteOrdenesPorConcepto();
        $scope.actualizarURLReporteOrdenesPorMozo();
        $scope.actualizarURLReporteOrdenesDetalladas();
        $scope.actualizarURLReporteDevolucionesEnOrdenes();
        $scope.actualizarURLReportePorModoAtenciones();
    }

    $scope.actualizarURLReporteAtenciones = function () {
        var validacionFechas = validarFechas($scope.reporteador.fechaHoraInicio, $scope.reporteador.fechaHoraFin, $scope.reporteador.parametros.MaximoDiasAtenciones);
        $scope.reporteAtenciones.MensajeError = validacionFechas.textoError;
        $scope.reporteAtenciones.Url = validacionFechas.error ? '' : URL_ + "/RestauranteReportes/ReporteDeAtenciones?desde=" + (new Date($scope.reporteador.fechaHoraInicio)).toUTCString() + "&hasta=" + (new Date($scope.reporteador.fechaHoraFin)).toUTCString() + '&idEstablecimiento=' + $scope.reporteador.establecimientoSeleccionado.Id + '&nombreEstablecimiento=' + $scope.reporteador.establecimientoSeleccionado.Nombre;
    }

    $scope.actualizarURLReporteOrdenesPorConcepto = function () {
        var validacionFechas = validarFechas($scope.reporteador.fechaHoraInicio, $scope.reporteador.fechaHoraFin, $scope.reporteador.parametros.MaximoDiasOrdenesPorConcepto);
        $scope.reporteOrdenesPorConcepto.MensajeError = validacionFechas.textoError;
        $scope.reporteOrdenesPorConcepto.URl = validacionFechas.error ? '' : URL_ + "/RestauranteReportes/ReporteDeOrdenesPorConcepto?desde=" + (new Date($scope.reporteador.fechaHoraInicio)).toUTCString() + "&hasta=" + (new Date($scope.reporteador.fechaHoraFin)).toUTCString();
    }

    $scope.actualizarURLReporteOrdenesPorMozo = function () {
        var validacionFechas = validarFechas($scope.reporteador.fechaHoraInicio, $scope.reporteador.fechaHoraFin, $scope.reporteador.parametros.MaximoDiasOrdenesPorMozo);
        $scope.reporteOrdenesPorMozo.MensajeError = validacionFechas.textoError;
        $scope.reporteOrdenesPorMozo.Url = validacionFechas.error ? '' : URL_ + "/RestauranteReportes/ReporteOrdenesPorMozo?desde=" + (new Date($scope.reporteador.fechaHoraInicio)).toUTCString() + "&hasta=" + (new Date($scope.reporteador.fechaHoraFin)).toUTCString();
    }


    $scope.actualizarURLReporteOrdenesDetalladas = function () {
        var validacionFechas = validarFechas($scope.reporteador.fechaHoraInicio, $scope.reporteador.fechaHoraFin, $scope.reporteador.parametros.MaximoDiasOrdenesDetalladas);
        $scope.reporteOrdenesDetalladas.MensajeError = validacionFechas.textoError;
        $scope.reporteOrdenesDetalladas.Url = validacionFechas.error ? '' : URL_ + "/RestauranteReportes/ReporteOrdenesDetallado?desde=" + (new Date($scope.reporteador.fechaHoraInicio)).toUTCString() + "&hasta=" + (new Date($scope.reporteador.fechaHoraFin)).toUTCString();
    }

    $scope.actualizarURLReporteDevolucionesEnOrdenes = function () {
        var validacionFechas = validarFechas($scope.reporteador.fechaHoraInicio, $scope.reporteador.fechaHoraFin, $scope.reporteador.parametros.MaximoDiasDevolucionesEnOrdenes);
        $scope.reporteDevolucionesEnOrdenes.MensajeError = validacionFechas.textoError;
        $scope.reporteDevolucionesEnOrdenes.Url = validacionFechas.error ? '' : URL_ + "/RestauranteReportes/ReporteDevolucionesEnOrdenes?desde=" + (new Date($scope.reporteador.fechaHoraInicio)).toUTCString() + "&hasta=" + (new Date($scope.reporteador.fechaHoraFin)).toUTCString();
    }

    $scope.actualizarURLReportePorModoAtenciones = function () {
        var validacionFechas = validarFechas($scope.reporteador.fechaHoraInicio, $scope.reporteador.fechaHoraFin, $scope.reporteador.parametros.MaximoDiasPorModoAtenciones);
        $scope.reportePorModoAtenciones.MensajeError = validacionFechas.textoError;
        $scope.reportePorModoAtenciones.Url = validacionFechas.error ? '' : URL_ + "/RestauranteReportes/ReportePorModoAtenciones?desde=" + (new Date($scope.reporteador.fechaHoraInicio)).toUTCString() + "&hasta=" + (new Date($scope.reporteador.fechaHoraFin)).toUTCString() + '&idEstablecimiento=' + $scope.reporteador.establecimientoSeleccionado.Id + '&nombreEstablecimiento=' + $scope.reporteador.establecimientoSeleccionado.Nombre;
    }
}); 
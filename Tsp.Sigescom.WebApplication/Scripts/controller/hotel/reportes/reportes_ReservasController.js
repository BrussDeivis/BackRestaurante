ReservasController=function($scope, $q, $compile, SweetAlert, $rootScope, $timeout, DTOptionsBuilder, DTColumnDefBuilder) {

    $scope.Reservas_EvaluarBotonesVer = function () {
        let NoExcedeMaximoDias_Reservas = $scope.diferenciaDiasEntreFechas($scope.reporteador.Reservas.FechaDesde, $scope.reporteador.Reservas.FechaHasta) <= $scope.data.MaximoDiasReporteHotel;

        $scope.reporteador.Reservas.Confirmadas.ActivarBotonVer = (($scope.reporteador.Reservas.FechaDesde != undefined && $scope.reporteador.Reservas.FechaHasta != undefined && $scope.reporteador.Reservas.EstablecimientoSeleccionado != undefined && NoExcedeMaximoDias_Reservas) && ($scope.reporteador.Reservas.SeleccionarTodosTiposHabitacion || $scope.reporteador.Reservas.TiposHabitacionSeleccionados.length > 0));
    }

    $scope.Reservas_EstablecimientoChanged = function () {
        $scope.Reservas_ActualizarURLs();
    }

    $scope.Reservas_ActualizarURLs = function () {
        $scope.Actualizar_URLReservas_Confirmadas();
        $scope.Reservas_EvaluarBotonesVer();
    }

    $scope.Reservas_TodosTiposHabitacionChanged = function () {
        if ($scope.reporteador.Reservas.SeleccionarTodosTiposHabitacion) {
            $scope.reporteador.Reservas.TiposHabitacionSeleccionados = [];
            $timeout(function () { $('#Reservas_TiposHabitacion').trigger("change"); }, 100);
        }
        $scope.Reservas_ActualizarURLs();
    }

    $scope.Reservas_TiposHabitacionChanged = function () {
        $scope.reporteador.Reservas.SeleccionarTodosTiposHabitacion = !($scope.reporteador.Reservas.TiposHabitacionSeleccionados.length > 0)
        $scope.Reservas_ActualizarURLs();

    }

    $scope.Actualizar_URLReservas_Confirmadas = function () {
        var idsTiposHabitacion = "";
        var nombresTiposHabitacion = "";
        if (!$scope.reporteador.Reservas.SeleccionarTodosTiposHabitacion) {
            nombresTiposHabitacion = "&nombresTiposHabitacion=";
            for (var i = 0; i < $scope.reporteador.Reservas.TiposHabitacionSeleccionados.length; i++) {
                idsTiposHabitacion += "&idsTiposHabitacion=" + $scope.reporteador.Reservas.TiposHabitacionSeleccionados[i].Id;
                nombresTiposHabitacion += $scope.reporteador.Reservas.TiposHabitacionSeleccionados[i].Nombre + " | ";
            }
        }
        $scope.URLReservas_Confirmadas = URL_ + "/HotelReportes_Reservas/Reservas_Confirmadas?"
            + "idEstablecimiento=" + $scope.reporteador.Reservas.EstablecimientoSeleccionado.Id
            + "&nombreEstablecimiento=" + $scope.reporteador.Reservas.EstablecimientoSeleccionado.Nombre
            + "&fechaDesde=" + (new Date($scope.reporteador.Reservas.FechaDesde)).toUTCString()
            + "&fechaHasta=" + (new Date($scope.reporteador.Reservas.FechaHasta)).toUTCString()
            + idsTiposHabitacion + nombresTiposHabitacion
            + "&todosTiposHabitacion=" + $scope.reporteador.Reservas.SeleccionarTodosTiposHabitacion;
    }

};

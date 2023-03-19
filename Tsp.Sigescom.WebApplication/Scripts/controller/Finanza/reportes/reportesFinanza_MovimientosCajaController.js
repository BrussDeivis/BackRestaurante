MovimientosCajaController = function ($scope, $q, $compile, SweetAlert, $rootScope, $timeout, DTOptionsBuilder, DTColumnDefBuilder) {
    $scope.MovimientosCaja_EvaluarBotonesVer = function () {

        $scope.reporteador.MovimientosCaja.Ingresos.ActivarBotonVer = ($scope.reporteador.MovimientosCaja.FechaDesde != undefined && $scope.reporteador.MovimientosCaja.FechaHasta != undefined && $scope.reporteador.MovimientosCaja.CajaSeleccionada != undefined && ($scope.reporteador.MovimientosCaja.Ingresos.SeleccionarTodosLosMediosPago || $scope.reporteador.MovimientosCaja.Ingresos.MediosPagoSeleccionados.length > 0) && ($scope.reporteador.MovimientosCaja.Ingresos.SeleccionarTodasLasOperaciones || $scope.reporteador.MovimientosCaja.Ingresos.OperacionesSeleccionadas.length > 0));

        $scope.reporteador.MovimientosCaja.Egresos.ActivarBotonVer = ($scope.reporteador.MovimientosCaja.FechaDesde != undefined && $scope.reporteador.MovimientosCaja.FechaHasta != undefined && $scope.reporteador.MovimientosCaja.CajaSeleccionada != undefined && ($scope.reporteador.MovimientosCaja.Egresos.SeleccionarTodosLosMediosPago || $scope.reporteador.MovimientosCaja.Egresos.MediosPagoSeleccionados.length > 0) && ($scope.reporteador.MovimientosCaja.Egresos.SeleccionarTodasLasOperaciones || $scope.reporteador.MovimientosCaja.Egresos.OperacionesSeleccionadas.length > 0));

        $scope.reporteador.MovimientosCaja.Flujo.ActivarBotonVer = ($scope.reporteador.MovimientosCaja.FechaDesde != undefined && $scope.reporteador.MovimientosCaja.FechaHasta != undefined && $scope.reporteador.MovimientosCaja.CajaSeleccionada != undefined && ($scope.reporteador.MovimientosCaja.Flujo.SeleccionarTodosLosMediosPago || $scope.reporteador.MovimientosCaja.Flujo.MediosPagoSeleccionados.length > 0));
    }

    $scope.MovimientosCaja_EstablecimientoChanged = function () {
        $scope.MovimientosCaja_SetCajaPorDefecto();
        $scope.MovimientosCaja_actualizarURLs();
    }

    $scope.MovimientosCaja_SetCajaPorDefecto = function () {
        $scope.reporteador.MovimientosCaja.CajaSeleccionada = $scope.reporteador.MovimientosCaja.EstablecimientoSeleccionado.CentrosAtencion[0];
        $scope.MovimientosCaja_actualizarMediosPago();
    }

    $scope.MovimientosCaja_actualizarURLs = function () {
        $scope.Actualizar_URLMovimientosCaja_Ingresos();
        $scope.Actualizar_URLMovimientosCaja_Egresos();
        $scope.Actualizar_URLMovimientosCaja_Flujo();
        $scope.MovimientosCaja_EvaluarBotonesVer();
    }

    $scope.MovimientosCaja_actualizarMediosPago = function () {
        if ($scope.reporteador.MovimientosCaja.CajaSeleccionada.Valor == 'true') {
            $scope.mediosPago = $scope.data.MediosPagoCuenta;
        } else {
            $scope.mediosPago = $scope.data.MediosPago;
        }
        $scope.reporteador.MovimientosCaja.Ingresos.SeleccionarTodosLosMediosPago = true;
        $scope.reporteador.MovimientosCaja.Ingresos.MediosPagoSeleccionados = [];
        $timeout(function () { $('#MovimientosCaja_mediospago_ingresos').trigger("change"); }, 100);
        $scope.reporteador.MovimientosCaja.Egresos.SeleccionarTodosLosMediosPago = true;
        $scope.reporteador.MovimientosCaja.Egresos.MediosPagoSeleccionados = [];
        $timeout(function () { $('#MovimientosCaja_mediospago_egresos').trigger("change"); }, 100);
        $scope.reporteador.MovimientosCaja.Flujo.SeleccionarTodosLosMediosPago = true;
        $scope.reporteador.MovimientosCaja.Flujo.MediosPagoSeleccionados = [];
        $timeout(function () { $('#MovimientosCaja_mediospago_flujo').trigger("change"); }, 100);
        $scope.MovimientosCaja_actualizarURLs();
    }

    $scope.MovimientosCaja_TodosLosMediosPagoIngresosChanged = function () {
        if ($scope.reporteador.MovimientosCaja.Ingresos.SeleccionarTodosLosMediosPago) {
            $scope.reporteador.MovimientosCaja.Ingresos.MediosPagoSeleccionados = [];
            $timeout(function () { $('#MovimientosCaja_mediospago_ingresos').trigger("change"); }, 100);
        }
        $scope.Actualizar_URLMovimientosCaja_Ingresos();
        $scope.MovimientosCaja_EvaluarBotonesVer();
    }

    $scope.MovimientosCaja_MediosPagoIngresosChanged = function () {
        if ($scope.reporteador.MovimientosCaja.Ingresos.SeleccionarTodosLosMediosPago != undefined) {
            $scope.reporteador.MovimientosCaja.Ingresos.SeleccionarTodosLosMediosPago = !($scope.reporteador.MovimientosCaja.Ingresos.MediosPagoSeleccionados.length > 0)
            $scope.Actualizar_URLMovimientosCaja_Ingresos();
            $scope.MovimientosCaja_EvaluarBotonesVer();
        }
    }

    $scope.MovimientosCaja_TodasLasOperacionesIngresosChanged = function () {
        if ($scope.reporteador.MovimientosCaja.Ingresos.SeleccionarTodasLasOperaciones) {
            $scope.reporteador.MovimientosCaja.Ingresos.OperacionesSeleccionadas = [];
            $timeout(function () { $('#MovimientosCaja_operaciones_ingresos').trigger("change"); }, 100);
        }
        $scope.Actualizar_URLMovimientosCaja_Ingresos();
        $scope.MovimientosCaja_EvaluarBotonesVer();
    }

    $scope.MovimientosCaja_OperacionesIngresosChanged = function () {
        if ($scope.reporteador.MovimientosCaja.Ingresos.SeleccionarTodasLasOperaciones != undefined) {
            $scope.reporteador.MovimientosCaja.Ingresos.SeleccionarTodasLasOperaciones = !($scope.reporteador.MovimientosCaja.Ingresos.OperacionesSeleccionadas.length > 0)
            $scope.Actualizar_URLMovimientosCaja_Ingresos();
            $scope.MovimientosCaja_EvaluarBotonesVer();
        }
    }

    $scope.Actualizar_URLMovimientosCaja_Ingresos = function () {
        var idsMediosPago = "";
        var nombresMediosPago = "";
        if (!$scope.reporteador.MovimientosCaja.Ingresos.SeleccionarTodosLosMediosPago) {
            nombresMediosPago = "&nombresMediosPago=";
            for (var i = 0; i < $scope.reporteador.MovimientosCaja.Ingresos.MediosPagoSeleccionados.length; i++) {
                idsMediosPago += "&idsMediosPago=" + $scope.reporteador.MovimientosCaja.Ingresos.MediosPagoSeleccionados[i].Id;
                nombresMediosPago += $scope.reporteador.MovimientosCaja.Ingresos.MediosPagoSeleccionados[i].Nombre + " | ";
            }
        }
        var idsOperaciones = "";
        var nombresOperaciones = "";
        if (!$scope.reporteador.MovimientosCaja.Ingresos.SeleccionarTodasLasOperaciones) {
            nombresOperaciones = "&nombresOperaciones=";
            for (var i = 0; i < $scope.reporteador.MovimientosCaja.Ingresos.OperacionesSeleccionadas.length; i++) {
                idsOperaciones += "&idsOperaciones=" + $scope.reporteador.MovimientosCaja.Ingresos.OperacionesSeleccionadas[i].Id;
                nombresOperaciones += $scope.reporteador.MovimientosCaja.Ingresos.OperacionesSeleccionadas[i].Nombre + " | ";
            }
        }
        $scope.URLMovimientosCaja_Ingresos = URL_ + "/FinanzaReportes_MovimientosCaja/MovimientosCaja_Ingresos?"
            + "esCuenta=" + ($scope.reporteador.MovimientosCaja.CajaSeleccionada.Valor == 'true')
            + "&idCaja=" + $scope.reporteador.MovimientosCaja.CajaSeleccionada.Id
            + "&nombreCaja=" + $scope.reporteador.MovimientosCaja.CajaSeleccionada.Nombre
            + "&fechaDesde=" + (new Date($scope.reporteador.MovimientosCaja.FechaDesde)).toUTCString()
            + "&fechaHasta=" + (new Date($scope.reporteador.MovimientosCaja.FechaHasta)).toUTCString()
            + "&todosLosMediosPago=" + $scope.reporteador.MovimientosCaja.Ingresos.SeleccionarTodosLosMediosPago
            + idsMediosPago + nombresMediosPago
            + "&todasLasOperaciones=" + $scope.reporteador.MovimientosCaja.Ingresos.SeleccionarTodasLasOperaciones
            + idsOperaciones + nombresOperaciones;
    }

    $scope.MovimientosCaja_TodosLosMediosPagoEgresosChanged = function () {
        if ($scope.reporteador.MovimientosCaja.Egresos.SeleccionarTodosLosMediosPago) {
            $scope.reporteador.MovimientosCaja.Egresos.MediosPagoSeleccionados = [];
            $timeout(function () { $('#MovimientosCaja_mediospago_egresos').trigger("change"); }, 100);
        }
        $scope.Actualizar_URLMovimientosCaja_Egresos();
    }

    $scope.MovimientosCaja_MediosPagoEgresosChanged = function () {
        if ($scope.reporteador.MovimientosCaja.Egresos.SeleccionarTodosLosMediosPago != undefined) {
            $scope.reporteador.MovimientosCaja.Egresos.SeleccionarTodosLosMediosPago = !($scope.reporteador.MovimientosCaja.Egresos.MediosPagoSeleccionados.length > 0)
            $scope.Actualizar_URLMovimientosCaja_Egresos();
        }
    }

    $scope.MovimientosCaja_TodasLasOperacionesEgresosChanged = function () {
        if ($scope.reporteador.MovimientosCaja.Egresos.SeleccionarTodasLasOperaciones) {
            $scope.reporteador.MovimientosCaja.Egresos.OperacionesSeleccionadas = [];
            $timeout(function () { $('#MovimientosCaja_operaciones_egresos').trigger("change"); }, 100);
        }
        $scope.Actualizar_URLMovimientosCaja_Egresos();
    }

    $scope.MovimientosCaja_OperacionesEgresosChanged = function () {
        if ($scope.reporteador.MovimientosCaja.Egresos.SeleccionarTodasLasOperaciones != undefined) {
            $scope.reporteador.MovimientosCaja.Egresos.SeleccionarTodasLasOperaciones = !($scope.reporteador.MovimientosCaja.Egresos.OperacionesSeleccionadas.length > 0)
            $scope.Actualizar_URLMovimientosCaja_Egresos();
        }
    }

    $scope.Actualizar_URLMovimientosCaja_Egresos = function () {
        var idsMediosPago = "";
        var nombresMediosPago = "";
        if (!$scope.reporteador.MovimientosCaja.Egresos.SeleccionarTodosLosMediosPago) {
            nombresMediosPago = "&nombresMediosPago=";
            for (var i = 0; i < $scope.reporteador.MovimientosCaja.Egresos.MediosPagoSeleccionados.length; i++) {
                idsMediosPago += "&idsMediosPago=" + $scope.reporteador.MovimientosCaja.Egresos.MediosPagoSeleccionados[i].Id;
                nombresMediosPago += $scope.reporteador.MovimientosCaja.Egresos.MediosPagoSeleccionados[i].Nombre + " | ";
            }
        }
        var idsOperaciones = "";
        var nombresOperaciones = "";
        if (!$scope.reporteador.MovimientosCaja.Egresos.SeleccionarTodasLasOperaciones) {
            nombresOperaciones = "&nombresOperaciones=";
            for (var i = 0; i < $scope.reporteador.MovimientosCaja.Egresos.OperacionesSeleccionadas.length; i++) {
                idsOperaciones += "&idsOperaciones=" + $scope.reporteador.MovimientosCaja.Egresos.OperacionesSeleccionadas[i].Id;
                nombresOperaciones += $scope.reporteador.MovimientosCaja.Egresos.OperacionesSeleccionadas[i].Nombre + " | ";
            }
        }
        $scope.URLMovimientosCaja_Egresos = URL_ + "/FinanzaReportes_MovimientosCaja/MovimientosCaja_Egresos?"
            + "esCuenta=" + ($scope.reporteador.MovimientosCaja.CajaSeleccionada.Valor == 'true')
            + "&idCaja=" + $scope.reporteador.MovimientosCaja.CajaSeleccionada.Id
            + "&nombreCaja=" + $scope.reporteador.MovimientosCaja.CajaSeleccionada.Nombre
            + "&fechaDesde=" + (new Date($scope.reporteador.MovimientosCaja.FechaDesde)).toUTCString()
            + "&fechaHasta=" + (new Date($scope.reporteador.MovimientosCaja.FechaHasta)).toUTCString()
            + "&todosLosMediosPago=" + $scope.reporteador.MovimientosCaja.Egresos.SeleccionarTodosLosMediosPago
            + idsMediosPago + nombresMediosPago
            + "&todasLasOperaciones=" + $scope.reporteador.MovimientosCaja.Egresos.SeleccionarTodasLasOperaciones
            + idsOperaciones + nombresOperaciones;
    }

    $scope.MovimientosCaja_TodosLosMediosPagoFlujoChanged = function () {
        if ($scope.reporteador.MovimientosCaja.Flujo.SeleccionarTodosLosMediosPago) {
            $scope.reporteador.MovimientosCaja.Flujo.MediosPagoSeleccionados = [];
            $timeout(function () { $('#MovimientosCaja_mediospago_flujo').trigger("change"); }, 100);
        }
        $scope.Actualizar_URLMovimientosCaja_Flujo();
    }

    $scope.MovimientosCaja_MediosPagoFlujoChanged = function () {
        if ($scope.reporteador.MovimientosCaja.Flujo.SeleccionarTodosLosMediosPago != undefined) {
            $scope.reporteador.MovimientosCaja.Flujo.SeleccionarTodosLosMediosPago = !($scope.reporteador.MovimientosCaja.Flujo.MediosPagoSeleccionados.length > 0)
            $scope.Actualizar_URLMovimientosCaja_Flujo();
        }
    }

    $scope.Actualizar_URLMovimientosCaja_Flujo = function () {
        var idsMediosPago = "";
        var nombresMediosPago = "";
        if (!$scope.reporteador.MovimientosCaja.Flujo.SeleccionarTodosLosMediosPago) {
            nombresMediosPago = "&nombresMediosPago=";
            for (var i = 0; i < $scope.reporteador.MovimientosCaja.Flujo.MediosPagoSeleccionados.length; i++) {
                idsMediosPago += "&idsMediosPago=" + $scope.reporteador.MovimientosCaja.Flujo.MediosPagoSeleccionados[i].Id;
                nombresMediosPago += $scope.reporteador.MovimientosCaja.Flujo.MediosPagoSeleccionados[i].Nombre + " | ";
            }
        }
        $scope.URLMovimientosCaja_Flujo = URL_ + "/FinanzaReportes_MovimientosCaja/MovimientosCaja_Flujo?"
            + "esCuenta=" + ($scope.reporteador.MovimientosCaja.CajaSeleccionada.Valor == 'true')
            + "&idCaja=" + $scope.reporteador.MovimientosCaja.CajaSeleccionada.Id
            + "&nombreCaja=" + $scope.reporteador.MovimientosCaja.CajaSeleccionada.Nombre
            + "&fechaDesde=" + (new Date($scope.reporteador.MovimientosCaja.FechaDesde)).toUTCString()
            + "&fechaHasta=" + (new Date($scope.reporteador.MovimientosCaja.FechaHasta)).toUTCString()
            + "&todosLosMediosPago=" + $scope.reporteador.MovimientosCaja.Flujo.SeleccionarTodosLosMediosPago
            + idsMediosPago + nombresMediosPago;
    }


};




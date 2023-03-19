MovimientosAlmacenController = function ($scope, $q, $compile, SweetAlert, $rootScope, $timeout, DTOptionsBuilder, DTColumnDefBuilder) {
    $scope.Movimientos_EvaluarBotonesVer = function () {
        $scope.reporteador.Movimientos.Entradas.ActivarBotonVer = ($scope.reporteador.Movimientos.FechaHasta != undefined && $scope.reporteador.Movimientos.FechaHasta != undefined && $scope.reporteador.Movimientos.AlmacenSeleccionado != undefined && ($scope.reporteador.Movimientos.SeleccionarTodasLasFamilias || $scope.reporteador.Movimientos.FamiliasSeleccionadas.length > 0));

        $scope.reporteador.Movimientos.Salidas.ActivarBotonVer = ($scope.reporteador.Movimientos.FechaHasta != undefined && $scope.reporteador.Movimientos.FechaHasta != undefined && $scope.reporteador.Movimientos.AlmacenSeleccionado != undefined && ($scope.reporteador.Movimientos.SeleccionarTodasLasFamilias || $scope.reporteador.Movimientos.FamiliasSeleccionadas.length > 0));

        $scope.reporteador.Movimientos.Vencimientos.ActivarBotonVer = ($scope.reporteador.Movimientos.FechaHasta != undefined && $scope.reporteador.Movimientos.FechaHasta != undefined && $scope.reporteador.Movimientos.AlmacenSeleccionado != undefined && ($scope.reporteador.Movimientos.SeleccionarTodasLasFamilias || $scope.reporteador.Movimientos.FamiliasSeleccionadas.length > 0));
    }


    $scope.Movimientos_EstablecimientoChanged = function () {
        $scope.Movimientos_SetAlmacenPorDefecto();
        $scope.Movimientos_actualizarURLs();

    }

    $scope.Movimientos_TodasLasFamiliasChanged = function () {
        if ($scope.reporteador.Movimientos.SeleccionarTodasLasFamilias) {
            $scope.reporteador.Movimientos.FamiliasSeleccionadas = [];
            $timeout(function () { $('#Movimientos_familias').trigger("change"); }, 100);
        }
        $scope.Movimientos_actualizarURLs();
    }

    $scope.Movimientos_FamiliasChanged = function () {
        $scope.reporteador.Movimientos.SeleccionarTodasLasFamilias = !($scope.reporteador.Movimientos.FamiliasSeleccionadas.length > 0)
        $scope.Movimientos_actualizarURLs();

    }

    $scope.Movimientos_SetAlmacenPorDefecto = function () {
        $scope.reporteador.Movimientos.AlmacenSeleccionado = $scope.reporteador.Movimientos.EstablecimientoSeleccionado.CentrosAtencion[0];
    }

    $scope.Movimientos_actualizarURLs = function () {
        $scope.Actualizar_URLMovimientos_Entradas();
        $scope.Actualizar_URLMovimientos_Salidas();
        $scope.Actualizar_URLMovimientos_Vencimientos();
        $scope.Movimientos_EvaluarBotonesVer();
    }

    $scope.ConcatenarAlmacenYFechas = function () {
        var resultado = "idAlmacen=" + $scope.reporteador.Movimientos.AlmacenSeleccionado.Id
            + "&nombreAlmacen=" + $scope.reporteador.Movimientos.AlmacenSeleccionado.Nombre
            + "&fechaDesde=" + (new Date($scope.reporteador.Movimientos.FechaDesde)).toUTCString()
            + "&fechaHasta=" + (new Date($scope.reporteador.Movimientos.FechaHasta)).toUTCString();
        return resultado;
    }
    $scope.Actualizar_URLMovimientos_Entradas = function () {
        var familias = $scope.ConcatenarFamilias($scope.reporteador.Movimientos.SeleccionarTodasLasFamilias, $scope.reporteador.Movimientos.FamiliasSeleccionadas);
        var almacenYFechas = $scope.ConcatenarAlmacenYFechas();
        $scope.URLMovimientos_Entradas = URL_ + "/AlmacenReportes/Movimientos_Entradas?" + almacenYFechas + "&" +familias;
    }
    $scope.Actualizar_URLMovimientos_Salidas = function () {
        var familias = $scope.ConcatenarFamilias($scope.reporteador.Movimientos.SeleccionarTodasLasFamilias, $scope.reporteador.Movimientos.FamiliasSeleccionadas);
        var almacenYFechas = $scope.ConcatenarAlmacenYFechas();
        $scope.URLMovimientos_Salidas = URL_ + "/AlmacenReportes/Movimientos_Salidas?" + almacenYFechas + "&"+familias;
    }

    $scope.Actualizar_URLMovimientos_Vencimientos = function () {
        var familias = $scope.ConcatenarFamilias($scope.reporteador.Movimientos.SeleccionarTodasLasFamilias, $scope.reporteador.Movimientos.FamiliasSeleccionadas);
        var almacenYFechas = $scope.ConcatenarAlmacenYFechas();
        $scope.URLMovimientos_Vencimientos = URL_ + "/AlmacenReportes/Movimientos_Vencimientos?" + almacenYFechas + "&" + familias;
    }


};




InventariosPorFechaAlmacenController=function($scope, $q, $compile, SweetAlert, $rootScope, $timeout, DTOptionsBuilder, DTColumnDefBuilder) {
    $scope.InventariosPorFecha_EvaluarBotonesVer = function () {

        $scope.reporteador.InventariosPorFecha.Inventario.ActivarBotonVer = ($scope.reporteador.InventariosPorFecha.FechaHasta != undefined && $scope.reporteador.InventariosPorFecha.AlmacenSeleccionado != undefined && ($scope.reporteador.InventariosPorFecha.SeleccionarTodasLasFamilias || $scope.reporteador.InventariosPorFecha.FamiliasSeleccionadas.length > 0));

        $scope.reporteador.InventariosPorFecha.InventarioSemaforo.ActivarBotonVer = ($scope.reporteador.InventariosPorFecha.FechaHasta != undefined && $scope.reporteador.InventariosPorFecha.AlmacenSeleccionado != undefined && ($scope.reporteador.InventariosPorFecha.SeleccionarTodasLasFamilias || $scope.reporteador.InventariosPorFecha.FamiliasSeleccionadas.length > 0) && ($scope.reporteador.InventariosPorFecha.InventarioSemaforo.Semaforo.Alto || $scope.reporteador.InventariosPorFecha.InventarioSemaforo.Semaforo.Normal || $scope.reporteador.InventariosPorFecha.InventarioSemaforo.Semaforo.Bajo));

        $scope.reporteador.InventariosPorFecha.InventarioValorizado.ActivarBotonVer = ($scope.reporteador.InventariosPorFecha.FechaHasta != undefined && $scope.reporteador.InventariosPorFecha.AlmacenSeleccionado != undefined && ($scope.reporteador.InventariosPorFecha.SeleccionarTodasLasFamilias || $scope.reporteador.InventariosPorFecha.FamiliasSeleccionadas.length > 0));
    }


    $scope.InventariosPorFecha_EstablecimientoChanged = function () {
        $scope.InventariosPorFecha_SetAlmacenPorDefecto();
        $scope.InventariosPorFecha_actualizarURLs();

    }

    $scope.InventariosPorFecha_TodasLasFamiliasChanged = function () {
        if ($scope.reporteador.InventariosPorFecha.SeleccionarTodasLasFamilias) {
            $scope.reporteador.InventariosPorFecha.FamiliasSeleccionadas = [];
            $timeout(function () { $('#InventariosPorFecha_familias').trigger("change"); }, 100);
        }
        $scope.InventariosPorFecha_actualizarURLs();
    }

    $scope.InventariosPorFecha_FamiliasChanged = function () {
        $scope.reporteador.InventariosPorFecha.SeleccionarTodasLasFamilias = !($scope.reporteador.InventariosPorFecha.FamiliasSeleccionadas.length > 0)
        $scope.InventariosPorFecha_actualizarURLs();

    }

    $scope.InventariosPorFecha_SetAlmacenPorDefecto = function () {
        $scope.reporteador.InventariosPorFecha.AlmacenSeleccionado = $scope.reporteador.InventariosPorFecha.EstablecimientoSeleccionado.CentrosAtencion[0];
    }

    $scope.InventariosPorFecha_actualizarURLs = function () {
        $scope.Actualizar_URLInventariosPorFecha_Inventario();
        $scope.Actualizar_URLInventariosPorFecha_InventarioSemaforo();
        $scope.Actualizar_URLInventariosPorFecha_InventarioValorizado();
        $scope.InventariosPorFecha_EvaluarBotonesVer();
    }

    $scope.Actualizar_URLInventariosPorFecha_Inventario = function () {
        var idsFamilias = "";
        var nombresFamilias = "";
        if (!$scope.reporteador.InventariosPorFecha.SeleccionarTodasLasFamilias) {
            nombresFamilias = "&nombresFamilias=";
        for (var i = 0; i < $scope.reporteador.InventariosPorFecha.FamiliasSeleccionadas.length; i++) {
            idsFamilias += "&idsFamilias=" + $scope.reporteador.InventariosPorFecha.FamiliasSeleccionadas[i].Id;
            nombresFamilias += $scope.reporteador.InventariosPorFecha.FamiliasSeleccionadas[i].Nombre+" | ";
            }
        }
        $scope.URLInventariosPorFecha_Inventario = URL_ + "/AlmacenReportes/InventariosPorFecha_Inventario?"
            + "idAlmacen=" + $scope.reporteador.InventariosPorFecha.AlmacenSeleccionado.Id
            + "&nombreAlmacen=" + $scope.reporteador.InventariosPorFecha.AlmacenSeleccionado.Nombre
            + "&fechaHasta=" + (new Date($scope.reporteador.InventariosPorFecha.FechaHasta)).toUTCString()
            + idsFamilias + nombresFamilias
            + "&todasLasFamilias=" + $scope.reporteador.InventariosPorFecha.SeleccionarTodasLasFamilias;
    }



    $scope.Actualizar_URLInventariosPorFecha_InventarioSemaforo = function () {
        var idsFamilias = "";
        var nombresFamilias = "";
        if (!$scope.reporteador.InventariosPorFecha.SeleccionarTodasLasFamilias) {
            nombresFamilias = "&nombresFamilias=";
            for (var i = 0; i < $scope.reporteador.InventariosPorFecha.FamiliasSeleccionadas.length; i++) {
                idsFamilias += "&idsFamilias=" + $scope.reporteador.InventariosPorFecha.FamiliasSeleccionadas[i].Id;
                nombresFamilias += $scope.reporteador.InventariosPorFecha.FamiliasSeleccionadas[i].Nombre + " | ";
            }
        }
        $scope.URLInventariosPorFecha_InventarioSemaforo = URL_ + "/AlmacenReportes/InventariosPorFecha_InventarioSemaforo?"
            + "idAlmacen=" + $scope.reporteador.InventariosPorFecha.AlmacenSeleccionado.Id
            + "&nombreAlmacen=" + $scope.reporteador.InventariosPorFecha.AlmacenSeleccionado.Nombre
            + "&fechaHasta=" + (new Date($scope.reporteador.InventariosPorFecha.FechaHasta)).toUTCString()
            + idsFamilias + nombresFamilias
            + "&todasLasFamilias=" + $scope.reporteador.InventariosPorFecha.SeleccionarTodasLasFamilias
            + "&estadoBajo=" + $scope.reporteador.InventariosPorFecha.InventarioSemaforo.Semaforo.Bajo
            + "&estadoNormal=" + $scope.reporteador.InventariosPorFecha.InventarioSemaforo.Semaforo.Normal
            + "&estadoAlto=" + $scope.reporteador.InventariosPorFecha.InventarioSemaforo.Semaforo.Alto;
    }

    $scope.Actualizar_URLInventariosPorFecha_InventarioValorizado = function () {
        var idsFamilias = "";
        var nombresFamilias = "";
        if (!$scope.reporteador.InventariosPorFecha.SeleccionarTodasLasFamilias) {
            nombresFamilias = "&nombresFamilias=";
            for (var i = 0; i < $scope.reporteador.InventariosPorFecha.FamiliasSeleccionadas.length; i++) {
                idsFamilias += "&idsFamilias=" + $scope.reporteador.InventariosPorFecha.FamiliasSeleccionadas[i].Id;
                nombresFamilias += $scope.reporteador.InventariosPorFecha.FamiliasSeleccionadas[i].Nombre + " | ";
            }
        }
        $scope.URLInventariosPorFecha_InventarioValorizado = URL_ + "/AlmacenReportes/InventariosPorFecha_InventarioValorizado?"
            + "idAlmacen=" + $scope.reporteador.InventariosPorFecha.AlmacenSeleccionado.Id
            + "&nombreAlmacen=" + $scope.reporteador.InventariosPorFecha.AlmacenSeleccionado.Nombre
            + "&fechaHasta=" + (new Date($scope.reporteador.InventariosPorFecha.FechaHasta)).toUTCString()
            + idsFamilias + nombresFamilias
            + "&todasLasFamilias=" + $scope.reporteador.InventariosPorFecha.SeleccionarTodasLasFamilias;
    }
};




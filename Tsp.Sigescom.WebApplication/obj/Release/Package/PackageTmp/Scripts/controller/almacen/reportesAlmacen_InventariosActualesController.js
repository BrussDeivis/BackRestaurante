InventariosActualesAlmacenController = function ($scope, $q, $compile, SweetAlert, $rootScope, $timeout, DTOptionsBuilder, DTColumnDefBuilder) {

    $scope.InventariosActuales_EvaluarBotonesVer = function () {

        $scope.reporteador.InventariosActuales.Inventario.ActivarBotonVer = (($scope.reporteador.InventariosActuales.SeleccionarTodosLosAlmacenes || $scope.reporteador.InventariosActuales.AlmacenesSeleccionados.length > 0) && ($scope.reporteador.InventariosActuales.SeleccionarTodasLasFamilias || $scope.reporteador.InventariosActuales.FamiliasSeleccionadas.length > 0));

        $scope.reporteador.InventariosActuales.InventarioSemaforo.ActivarBotonVer = (($scope.reporteador.InventariosActuales.SeleccionarTodosLosAlmacenes || $scope.reporteador.InventariosActuales.AlmacenesSeleccionados.length > 0) && ($scope.reporteador.InventariosActuales.SeleccionarTodasLasFamilias || $scope.reporteador.InventariosActuales.FamiliasSeleccionadas.length > 0) && ($scope.reporteador.InventariosActuales.InventarioSemaforo.Semaforo.Alto || $scope.reporteador.InventariosActuales.InventarioSemaforo.Semaforo.Normal || $scope.reporteador.InventariosActuales.InventarioSemaforo.Semaforo.Bajo));
        $scope.reporteador.InventariosActuales.InventarioValorizado.ActivarBotonVer = (($scope.reporteador.InventariosActuales.SeleccionarTodosLosAlmacenes || $scope.reporteador.InventariosActuales.AlmacenesSeleccionados.length > 0) && ($scope.reporteador.InventariosActuales.SeleccionarTodasLasFamilias || $scope.reporteador.InventariosActuales.FamiliasSeleccionadas.length > 0));
    }

    $scope.InventariosActuales_TodosLosAlmacenesChanged = function () {
        if ($scope.reporteador.InventariosActuales.SeleccionarTodosLosAlmacenes) {
            $scope.reporteador.InventariosActuales.AlmacenesSeleccionados = [];
            $timeout(function () { $('#InventariosActuales_almacenes').trigger("change"); }, 100);
        }
        $scope.InventariosActuales_actualizarURLs();
    }

    $scope.InventariosActuales_AlmacenesChanged = function () {
        if ($scope.reporteador.InventariosActuales.AlmacenesSeleccionados != undefined) {
            $scope.reporteador.InventariosActuales.SeleccionarTodosLosAlmacenes = !($scope.reporteador.InventariosActuales.AlmacenesSeleccionados.length > 0)
            $scope.InventariosActuales_actualizarURLs();
        }
    }

    $scope.InventariosActuales_TodasLasFamiliasChanged = function () {
        if ($scope.reporteador.InventariosActuales.SeleccionarTodasLasFamilias) {
            $scope.reporteador.InventariosActuales.FamiliasSeleccionadas = [];
            $timeout(function () { $('#InventariosActuales_familias').trigger("change"); }, 100);
        }
        $scope.InventariosActuales_actualizarURLs();
    }

    $scope.InventariosActuales_FamiliasChanged = function () {
        if ($scope.reporteador.InventariosActuales.FamiliasSeleccionadas != undefined) {
            $scope.reporteador.InventariosActuales.SeleccionarTodasLasFamilias = !($scope.reporteador.InventariosActuales.FamiliasSeleccionadas.length > 0)
            $scope.InventariosActuales_actualizarURLs();
        }
    }

    $scope.InventariosActuales_actualizarURLs = function () {
        $scope.Actualizar_URLInventariosActuales_Inventario();
        $scope.Actualizar_URLInventariosActuales_InventarioSemaforo();
        $scope.Actualizar_URLInventariosActuales_InventarioValorizado();
        $scope.InventariosActuales_EvaluarBotonesVer();
    }

    $scope.ConcatenarAlmacenes = function () {
        var almacenes = "";
        var almacenesAConsultar = $scope.reporteador.InventariosActuales.SeleccionarTodosLosAlmacenes ? $scope.data.Almacenes : $scope.reporteador.InventariosActuales.AlmacenesSeleccionados;
        for (var i = 0; i < almacenesAConsultar.length; i++) {
            almacenes += "almacenes="+almacenesAConsultar[i].Id + "|" + almacenesAConsultar[i].Nombre + "&";
        }
        return almacenes;
    }

    $scope.Actualizar_URLInventariosActuales_Inventario = function () {
        var familias = $scope.ConcatenarFamilias($scope.reporteador.InventariosActuales.SeleccionarTodasLasFamilias, $scope.reporteador.InventariosActuales.FamiliasSeleccionadas);
        var almacenes = $scope.ConcatenarAlmacenes();
        $scope.URLInventariosActuales_Inventario = URL_ + "/AlmacenReportes/InventariosActuales_Inventario?"
            + almacenes + familias;
    }

    $scope.Actualizar_URLInventariosActuales_InventarioSemaforo = function () {
        var familias = $scope.ConcatenarFamilias($scope.reporteador.InventariosActuales.SeleccionarTodasLasFamilias, $scope.reporteador.InventariosActuales.FamiliasSeleccionadas);
        var almacenes = $scope.ConcatenarAlmacenes();
        $scope.URLInventariosActuales_InventarioSemaforo = URL_ + "/AlmacenReportes/InventariosActuales_InventarioSemaforo?"
            + almacenes + familias + "&estadoBajo=" + $scope.reporteador.InventariosPorFecha.InventarioSemaforo.Semaforo.Bajo
            + "&estadoNormal=" + $scope.reporteador.InventariosPorFecha.InventarioSemaforo.Semaforo.Normal
            + "&estadoAlto=" + $scope.reporteador.InventariosPorFecha.InventarioSemaforo.Semaforo.Alto;
    }

    $scope.Actualizar_URLInventariosActuales_InventarioValorizado = function () {
        var familias = $scope.ConcatenarFamilias($scope.reporteador.InventariosActuales.SeleccionarTodasLasFamilias, $scope.reporteador.InventariosActuales.FamiliasSeleccionadas);
        var almacenes = $scope.ConcatenarAlmacenes();
        $scope.URLInventariosActuales_InventarioValorizado = URL_ + "/AlmacenReportes/InventariosActuales_InventarioValorizado?"
            + almacenes + familias;
    }

};




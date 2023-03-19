PorGruposController = function ($scope, $q, $compile, SweetAlert, $rootScope, $timeout, DTOptionsBuilder, DTColumnDefBuilder, actorComercialService) {
    $scope.PorGrupos_EvaluarBotonesVer = function () {

        $scope.reporteador.PorGrupos.VentasPorFamiliasGrupos.ActivarBotonVer = ($scope.reporteador.PorGrupos.FechaDesde != undefined && $scope.reporteador.PorGrupos.FechaHasta != undefined && $scope.reporteador.PorGrupos.PuntoVentaSeleccionado != undefined && ($scope.reporteador.PorGrupos.VentasPorFamiliasGrupos.SeleccionarTodasLasFamilias || $scope.reporteador.PorGrupos.VentasPorFamiliasGrupos.FamiliasSeleccionadas.length > 0) && ($scope.reporteador.PorGrupos.VentasPorFamiliasGrupos.SeleccionarTodosLosGrupos || $scope.reporteador.PorGrupos.VentasPorFamiliasGrupos.GruposSeleccionados.length > 0));

        $scope.reporteador.PorGrupos.VentasPorGrupos.ActivarBotonVer = ($scope.reporteador.PorGrupos.FechaDesde != undefined && $scope.reporteador.PorGrupos.FechaHasta != undefined && $scope.reporteador.PorGrupos.PuntoVentaSeleccionado != undefined && ($scope.reporteador.PorGrupos.VentasPorGrupos.SeleccionarTodosLosGrupos || $scope.reporteador.PorGrupos.VentasPorGrupos.GruposSeleccionados.length > 0));

        $scope.reporteador.PorGrupos.VentasPorGrupoDetallado.ActivarBotonVer = ($scope.reporteador.PorGrupos.FechaDesde != undefined && $scope.reporteador.PorGrupos.FechaHasta != undefined && $scope.reporteador.PorGrupos.PuntoVentaSeleccionado != undefined && $scope.reporteador.PorGrupos.VentasPorGrupoDetallado.GrupoSeleccionado != undefined);

    }

    $scope.PorGrupos_EstablecimientoChanged = function () {
        $scope.PorGrupos_SetPuntoVentaPorDefecto();
        $scope.PorGrupos_ActualizarURLs();
    }

    $scope.PorGrupos_SetPuntoVentaPorDefecto = function () {
        $scope.reporteador.PorGrupos.PuntoVentaSeleccionado = $scope.reporteador.PorGrupos.EstablecimientoSeleccionado.CentrosAtencion[0];
    }

    $scope.PorGrupos_ActualizarURLs = function () {
        $scope.Actualizar_URLPorGrupos_VentasPorFamiliasGrupos();
        $scope.Actualizar_URLPorGrupos_VentasPorGrupos();
        $scope.Actualizar_URLPorGrupos_VentasPorGrupoDetallado();
        $scope.PorGrupos_EvaluarBotonesVer();
    }

    $scope.obtenerGruposActoresComerciales = function () {
        actorComercialService.obtenerGruposActoresComerciales({}).success(function (data) {
            $scope.grupos = data;
        }).error(function (data) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }

    $scope.ConcatenarPuntoVentaEstablecimientoYFechas_PorGrupos = function () {
        var resultado = "idPuntoVenta=" + $scope.reporteador.PorGrupos.PuntoVentaSeleccionado.Id
            + "&nombrePuntoVenta=" + $scope.reporteador.PorGrupos.PuntoVentaSeleccionado.Nombre
            + "&nombreEstablecimiento=" + $scope.reporteador.PorGrupos.EstablecimientoSeleccionado.Nombre
            + "&fechaDesde=" + (new Date($scope.reporteador.PorGrupos.FechaDesde)).toUTCString()
            + "&fechaHasta=" + (new Date($scope.reporteador.PorGrupos.FechaHasta)).toUTCString();
        return resultado;
    }

    $scope.PorGrupos_TodasLasFamiliasVentasPorFamiliasGruposChanged = function () {
        if ($scope.reporteador.PorGrupos.VentasPorFamiliasGrupos.SeleccionarTodasLasFamilias) {
            $scope.reporteador.PorGrupos.VentasPorFamiliasGrupos.FamiliasSeleccionadas = [];
            $timeout(function () { $('#PorGrupos_familias_VentasPorFamiliasGrupos').trigger("change"); }, 100);
        }
        $scope.Actualizar_URLPorGrupos_VentasPorFamiliasGrupos();
        $scope.PorGrupos_EvaluarBotonesVer();
    }

    $scope.PorGrupos_FamiliasVentasPorFamiliasGruposChanged = function () {
        if ($scope.reporteador.PorGrupos.VentasPorFamiliasGrupos.SeleccionarTodasLasFamilias != undefined) {
            $scope.reporteador.PorGrupos.VentasPorFamiliasGrupos.SeleccionarTodasLasFamilias = !($scope.reporteador.PorGrupos.VentasPorFamiliasGrupos.FamiliasSeleccionadas.length > 0)
            $scope.Actualizar_URLPorGrupos_VentasPorFamiliasGrupos();
            $scope.PorGrupos_EvaluarBotonesVer();
        }
    }

    $scope.PorGrupos_TodosLosGruposVentasPorFamiliasGruposChanged = function () {
        if ($scope.reporteador.PorGrupos.VentasPorFamiliasGrupos.SeleccionarTodosLosGrupos) {
            $scope.reporteador.PorGrupos.VentasPorFamiliasGrupos.GruposSeleccionados = [];
            $timeout(function () { $('#PorGrupos_grupos_VentasPorFamiliasGrupos').trigger("change"); }, 100);
        }
        $scope.Actualizar_URLPorGrupos_VentasPorFamiliasGrupos();
        $scope.PorGrupos_EvaluarBotonesVer();
    }

    $scope.PorGrupos_GruposVentasPorFamiliasGruposChanged = function () {
        if ($scope.reporteador.PorGrupos.VentasPorFamiliasGrupos.SeleccionarTodosLosGrupos != undefined) {
            $scope.reporteador.PorGrupos.VentasPorFamiliasGrupos.SeleccionarTodosLosGrupos = !($scope.reporteador.PorGrupos.VentasPorFamiliasGrupos.GruposSeleccionados.length > 0)
            $scope.Actualizar_URLPorGrupos_VentasPorFamiliasGrupos();
            $scope.PorGrupos_EvaluarBotonesVer();
        }
    }

    $scope.Actualizar_URLPorGrupos_VentasPorFamiliasGrupos = function () {
        var idsFamilias = "";
        var nombresFamilias = "";
        if (!$scope.reporteador.PorGrupos.VentasPorFamiliasGrupos.SeleccionarTodasLasFamilias) {
            nombresFamilias = "&nombresFamilias=";
            for (var i = 0; i < $scope.reporteador.PorGrupos.VentasPorFamiliasGrupos.FamiliasSeleccionadas.length; i++) {
                idsFamilias += "&idsFamilias=" + $scope.reporteador.PorGrupos.VentasPorFamiliasGrupos.FamiliasSeleccionadas[i].Id;
                nombresFamilias += $scope.reporteador.PorGrupos.VentasPorFamiliasGrupos.FamiliasSeleccionadas[i].Nombre + " | ";
            }
        }
        var idsGrupos = "";
        var nombresGrupos = "";
        if (!$scope.reporteador.PorGrupos.VentasPorFamiliasGrupos.SeleccionarTodosLosGrupos) {
            nombresGrupos = "&nombresGrupos=";
            for (var i = 0; i < $scope.reporteador.PorGrupos.VentasPorFamiliasGrupos.GruposSeleccionados.length; i++) {
                idsGrupos += "&idsGrupos=" + $scope.reporteador.PorGrupos.VentasPorFamiliasGrupos.GruposSeleccionados[i].Id;
                nombresGrupos += $scope.reporteador.PorGrupos.VentasPorFamiliasGrupos.GruposSeleccionados[i].Nombre + " | ";
            }
        }
        $scope.URLPorGrupos_VentasPorFamiliasGrupos = URL_ + "/VentaReportes_PorGrupos/PorGrupos_VentasPorFamiliasGrupos?"
            + $scope.ConcatenarPuntoVentaEstablecimientoYFechas_PorGrupos()
            + "&todasLasFamilias=" + $scope.reporteador.PorGrupos.VentasPorFamiliasGrupos.SeleccionarTodasLasFamilias
            + idsFamilias + nombresFamilias
            + "&todosLosGrupos=" + $scope.reporteador.PorGrupos.VentasPorFamiliasGrupos.SeleccionarTodosLosGrupos
            + idsGrupos + nombresGrupos;
    }

    $scope.PorGrupos_TodosLosGruposVentasPorGruposChanged = function () {
        if ($scope.reporteador.PorGrupos.VentasPorGrupos.SeleccionarTodosLosGrupos) {
            $scope.reporteador.PorGrupos.VentasPorGrupos.GruposSeleccionados = [];
            $timeout(function () { $('#PorGrupos_grupos_VentasPorGrupos').trigger("change"); }, 100);
        }
        $scope.Actualizar_URLPorGrupos_VentasPorGrupos();
        $scope.PorGrupos_EvaluarBotonesVer();
    }

    $scope.PorGrupos_GruposVentasPorGruposChanged = function () {
        if ($scope.reporteador.PorGrupos.VentasPorGrupos.SeleccionarTodosLosGrupos != undefined) {
            $scope.reporteador.PorGrupos.VentasPorGrupos.SeleccionarTodosLosGrupos = !($scope.reporteador.PorGrupos.VentasPorGrupos.GruposSeleccionados.length > 0)
            $scope.Actualizar_URLPorGrupos_VentasPorGrupos();
            $scope.PorGrupos_EvaluarBotonesVer();
        }
    }

    $scope.Actualizar_URLPorGrupos_VentasPorGrupos = function () {
        var idsGrupos = "";
        var nombresGrupos = "";
        if (!$scope.reporteador.PorGrupos.VentasPorGrupos.SeleccionarTodosLosGrupos) {
            nombresGrupos = "&nombresGrupos=";
            for (var i = 0; i < $scope.reporteador.PorGrupos.VentasPorGrupos.GruposSeleccionados.length; i++) {
                idsGrupos += "&idsGrupos=" + $scope.reporteador.PorGrupos.VentasPorGrupos.GruposSeleccionados[i].Id;
                nombresGrupos += $scope.reporteador.PorGrupos.VentasPorGrupos.GruposSeleccionados[i].Nombre + " | ";
            }
        }
        $scope.URLPorGrupos_VentasPorGrupos = URL_ + "/VentaReportes_PorGrupos/PorGrupos_VentasPorGrupos?"
            + $scope.ConcatenarPuntoVentaEstablecimientoYFechas_PorGrupos()
            + "&todosLosGrupos=" + $scope.reporteador.PorGrupos.VentasPorGrupos.SeleccionarTodosLosGrupos
            + idsGrupos + nombresGrupos;
    }

    $scope.PorGrupos_GrupoVentasPorGrupoDetalladoChanged = function () {
        $scope.Actualizar_URLPorGrupos_VentasPorGrupoDetallado();
        $scope.PorGrupos_EvaluarBotonesVer();
    }

    $scope.Actualizar_URLPorGrupos_VentasPorGrupoDetallado = function () {
        if ($scope.reporteador.PorGrupos.VentasPorGrupoDetallado.GrupoSeleccionado != undefined) {
            $scope.URLPorGrupos_VentasPorGrupoDetallado = URL_ + "/VentaReportes_PorGrupos/PorGrupos_VentasPorGrupoDetallado?"
                + $scope.ConcatenarPuntoVentaEstablecimientoYFechas_PorGrupos()
                + "&idGrupo=" + $scope.reporteador.PorGrupos.VentasPorGrupoDetallado.GrupoSeleccionado.Id
                + "&nombreGrupo=" + $scope.reporteador.PorGrupos.VentasPorGrupoDetallado.GrupoSeleccionado.Nombre;
        }
    }

    $scope.obtenerGruposActoresComerciales();
};




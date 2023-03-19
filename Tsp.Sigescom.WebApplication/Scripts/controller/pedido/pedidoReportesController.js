app.controller('pedidoReportesController', function ($scope, $q, $compile, SweetAlert, $rootScope, $timeout, DTOptionsBuilder, DTColumnDefBuilder, actorComercialService) {
    $scope.inicializar = function () {
        $scope.data = reportData;
        console.log($scope.data);
        $scope.reporteador = {
            PedidosInvalidadosPorFecha: {
                FechaHasta: new Date($scope.data.FechaHastaDefault),
                FechaDesde: new Date($scope.data.FechaDesdeDefault),
                EstablecimientoSeleccionado: $scope.data.Establecimientos[0],
                PuntosVentaSelecionados: [],
                SeleccionarTodosLosPuntosVenta: true,
                ActivarBotonVer: false,
            }
        };
        $scope.PorInvalidados_ActualizarURLs();
        $scope.PorInvalidados_EvaluarBotonVer();
    }

    //#region Establecer Datos por Defecto
    $scope.EstablecerEstablecimientoComercialPorDefecto = function () {
        $scope.establecimiento = $scope.Establecimientos[0];
    }
    $scope.EstablecerCentrosDeAtencionPorDefecto = function () {
        $scope.centroDeAtencion = $scope.centrosDeAtencion[0];
    }
    //#endregion


    //   #region Establecer Datos por Defecto
    $scope.PorPedidosInvalidados_SetPuntoVentaPorDefecto = function () {
        $scope.reporteador.PedidosInvalidadosPorFecha.PuntoVentaSeleccionado = $scope.reporteador.PedidosInvalidadosPorFecha.EstablecimientoSeleccionado.CentrosAtencion[0];
    }
    $scope.PorPedidosInvalidados_EstablecimientoChanged = function () {
        $scope.PorPedidosInvalidados_SetPuntoVentaPorDefecto();
        $scope.PorInvalidados_ActualizarURLs();
    }

    $scope.PorPedidosInvalidados_SeleccionarTodosLosPuntosVentaChanged = function () {
        if ($scope.reporteador.PedidosInvalidadosPorFecha.SeleccionarTodosLosPuntosVenta) {
            $scope.reporteador.PedidosInvalidadosPorFecha.PuntosVentaSelecionados = [];
            $timeout(function () { $('#InvalidadosPorFecha_puntos_venta').trigger("change"); }, 100);
        }
        $scope.Actualizar_URLPorInvalidados_Invalidados();
        $scope.PorInvalidados_EvaluarBotonVer();
    }

    $scope.PorInvalidados_PuntosVentaChanged = function () {
        if ($scope.reporteador.PedidosInvalidadosPorFecha.SeleccionarTodosLosPuntosVenta != undefined) {
            $scope.reporteador.PedidosInvalidadosPorFecha.SeleccionarTodosLosPuntosVenta = !($scope.reporteador.PedidosInvalidadosPorFecha.PuntosVentaSelecionados.length > 0)
            $scope.Actualizar_URLPorInvalidados_Invalidados();
            $scope.PorInvalidados_EvaluarBotonVer();
        }
    }

    $scope.PorInvalidados_EvaluarBotonVer = function () {
        console.log($scope.reporteador.PedidosInvalidadosPorFecha.SeleccionarTodosLosPuntosVenta);
        $scope.reporteador.PedidosInvalidadosPorFecha.ActivarBotonVer = ($scope.reporteador.PedidosInvalidadosPorFecha.FechaDesde != undefined && $scope.reporteador.PedidosInvalidadosPorFecha.FechaHasta != undefined && ($scope.reporteador.PedidosInvalidadosPorFecha.SeleccionarTodosLosPuntosVenta || $scope.reporteador.PedidosInvalidadosPorFecha.PuntosVentaSelecionados.length > 0) && ($scope.reporteador.PedidosInvalidadosPorFecha.SeleccionarTodosLosPuntosVenta || $scope.reporteador.PedidosInvalidadosPorFecha.PuntosVentaSelecionados.length > 0));

    }

    $scope.Actualizar_URLPorInvalidados_Invalidados = function () {
        var idsPuntosVenta = "";
        var nombresPuntosVenta = "";
        if (!$scope.reporteador.PedidosInvalidadosPorFecha.SeleccionarTodosLosPuntosVenta) {
            nombresPuntosVenta = "&nombresPuntosVenta=";
            for (var i = 0; i < $scope.reporteador.PedidosInvalidadosPorFecha.PuntosVentaSelecionados.length; i++) {
                idsPuntosVenta += "&idsPuntosVenta=" + $scope.reporteador.PedidosInvalidadosPorFecha.PuntosVentaSelecionados[i].Id;
                nombresPuntosVenta += $scope.reporteador.PedidosInvalidadosPorFecha.PuntosVentaSelecionados[i].Nombre + " | ";
            }
        }
        $scope.URLPorInvalidados_PedidosInvalidadosPuntosVenta = URL_ + "/PedidoReportes/PedidosInvalidados?"
            + $scope.ConcatenarPuntoVentaEstablecimientoYFechas_PorInvalidados()
            + "&todosLosPuntosVenta=" + $scope.reporteador.PedidosInvalidadosPorFecha.SeleccionarTodosLosPuntosVenta
            + idsPuntosVenta + nombresPuntosVenta;
    }

    $scope.ConcatenarPuntoVentaEstablecimientoYFechas_PorInvalidados = function () {
        var resultado = "&idEstablecimientoComercial=" + $scope.reporteador.PedidosInvalidadosPorFecha.EstablecimientoSeleccionado.Id
            + "&nombreEstablecimiento=" + $scope.reporteador.PedidosInvalidadosPorFecha.EstablecimientoSeleccionado.Nombre
            + "&fechaDesde=" + (new Date($scope.reporteador.PedidosInvalidadosPorFecha.FechaDesde)).toUTCString()
            + "&fechaHasta=" + (new Date($scope.reporteador.PedidosInvalidadosPorFecha.FechaHasta)).toUTCString()
        return resultado;
    }

    $scope.PorInvalidados_ActualizarURLs = function () {
        $scope.Actualizar_URLPorInvalidados_Invalidados();
        $scope.PorInvalidados_EvaluarBotonVer();

    }

    //#endregion


});
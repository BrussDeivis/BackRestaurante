MovimientosCajasController = function ($scope, $q, $compile, SweetAlert, $rootScope, $timeout, DTOptionsBuilder, DTColumnDefBuilder, centroDeAtencionService) {
    $scope.MovimientosCajas_EvaluarBotonesVer = function () {

        $scope.reporteador.MovimientosCajas.CobrosClientes.ActivarBotonVer = ($scope.reporteador.MovimientosCajas.FechaDesde != undefined && $scope.reporteador.MovimientosCajas.FechaHasta != undefined &&
            ($scope.reporteador.MovimientosCajas.SeleccionarTodasLasCajas || $scope.reporteador.MovimientosCajas.CajasSeleccionadas.length > 0) && ($scope.reporteador.MovimientosCajas.CobrosClientes.SeleccionarTodosLosClientes || $scope.reporteador.MovimientosCajas.CobrosClientes.ClientesSeleccionadas.length > 0));

        $scope.reporteador.MovimientosCajas.PagosProveedores.ActivarBotonVer = ($scope.reporteador.MovimientosCajas.FechaDesde != undefined && $scope.reporteador.MovimientosCajas.FechaHasta != undefined &&
            ($scope.reporteador.MovimientosCajas.SeleccionarTodasLasCajas || $scope.reporteador.MovimientosCajas.CajasSeleccionadas.length > 0) && ($scope.reporteador.MovimientosCajas.PagosProveedores.SeleccionarTodosLosProveedores || $scope.reporteador.MovimientosCajas.PagosProveedores.ProveedoresSeleccionados.length > 0));
    }

    $scope.MovimientosCajas_actualizarURLs = function () {
        $scope.Actualizar_URLMovimientosCajas_CobrosClientes();
        $scope.Actualizar_URLMovimientosCajas_PagosProveedores();
        $scope.MovimientosCajas_EvaluarBotonesVer();
    }

    $scope.obtenerCajas = function () {
        centroDeAtencionService.obtenerCentrosDeAtencionConRolPuntoDeVentaVigentesConEstablecimientoComercial().success(function (data) {
            $scope.cajas = data;
        }).error(function (data) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }

    $scope.MovimientosCajas_TodasLasCajasChanged = function () {
        if ($scope.reporteador.MovimientosCajas.SeleccionarTodasLasCajas) {
            $scope.reporteador.MovimientosCajas.CajasSeleccionadas = [];
            $timeout(function () { $('#MovimientosCajas_cajas').trigger("change"); }, 100);
        }
        $scope.MovimientosCajas_actualizarURLs();
    }

    $scope.MovimientosCajas_CajasChanged = function () {
        if ($scope.reporteador.MovimientosCajas.CajasSeleccionadas != undefined) {
            $scope.reporteador.MovimientosCajas.SeleccionarTodasLasCajas = !($scope.reporteador.MovimientosCajas.CajasSeleccionadas.length > 0)
            $scope.MovimientosCajas_actualizarURLs();
        }
    }

    $scope.MovimientosCajas_TodosLosClientesCobrosClientesChanged = function () {
        if ($scope.reporteador.MovimientosCajas.CobrosClientes.SeleccionarTodosLosClientes) {
            $scope.reporteador.MovimientosCajas.CobrosClientes.ClientesSeleccionados = [];
            $timeout(function () { $('#MovimientosCajas_clientes_CobrosClientes').trigger("change"); }, 100);
        }
        $scope.Actualizar_URLMovimientosCajas_CobrosClientes();
        $scope.MovimientosCajas_EvaluarBotonesVer();
    }

    $scope.MovimientosCajas_ClientesCobrosClientesChanged = function () {
        if ($scope.reporteador.MovimientosCajas.CobrosClientes.SeleccionarTodosLosClientes != undefined) {
            $scope.reporteador.MovimientosCajas.CobrosClientes.SeleccionarTodosLosClientes = !($scope.reporteador.MovimientosCajas.CobrosClientes.ClientesSeleccionados.length > 0)
            $scope.Actualizar_URLMovimientosCajas_CobrosClientes();
            $scope.MovimientosCajas_EvaluarBotonesVer();
        }
    }

    $scope.Actualizar_URLMovimientosCajas_CobrosClientes = function () {
        var idsCajas = "";
        var nombresCajas = "";
        if (!$scope.reporteador.MovimientosCajas.SeleccionarTodasLasCajas) {
            nombresCajas = "&nombresCajas=";
            for (var i = 0; i < $scope.reporteador.MovimientosCajas.CajasSeleccionadas.length; i++) {
                idsCajas += "&idsCajas=" + $scope.reporteador.MovimientosCajas.CajasSeleccionadas[i].Id;
                nombresCajas += $scope.reporteador.MovimientosCajas.CajasSeleccionadas[i].Nombre + " | ";
            }
        }
        var idsClientes = "";
        var nombresClientes = "";
        if (!$scope.reporteador.MovimientosCajas.SeleccionarTodosLosClientes) {
            nombresClientes = "&nombresClientes=";
            for (var i = 0; i < $scope.reporteador.MovimientosCajas.CobrosClientes.ClientesSeleccionados.length; i++) {
                idsClientes += "&idsClientes=" + $scope.reporteador.MovimientosCajas.CobrosClientes.ClientesSeleccionados[i].Id;
                nombresClientes += $scope.reporteador.MovimientosCajas.CobrosClientes.ClientesSeleccionados[i].Nombre + " | ";
            }
        }
        $scope.URLMovimientosCajas_CobrosClientes = URL_ + "/FinanzaReportes_MovimientosCajas/MovimientosCajas_CobrosClientes?"
            + "&fechaDesde=" + (new Date($scope.reporteador.MovimientosCajas.FechaDesde)).toUTCString()
            + "&fechaHasta=" + (new Date($scope.reporteador.MovimientosCajas.FechaHasta)).toUTCString()
            + "&todasLasCajas=" + $scope.reporteador.MovimientosCajas.SeleccionarTodasLasCajas
            + idsCajas + nombresCajas
            + "&todosLosClientes=" + $scope.reporteador.MovimientosCajas.CobrosClientes.SeleccionarTodosLosClientes
            + idsClientes + nombresClientes;
    }

    $scope.MovimientosCajas_TodosLosProveedoresPagosProveedoresChanged = function () {
        if ($scope.reporteador.MovimientosCajas.PagosProveedores.SeleccionarTodosLosProveedores) {
            $scope.reporteador.MovimientosCajas.PagosProveedores.ProveedoresSeleccionados = [];
            $timeout(function () { $('#MovimientosCajas_proveedores_PagosProveedores').trigger("change"); }, 100);
        }
        $scope.Actualizar_URLMovimientosCajas_PagosProveedores();
    }

    $scope.MovimientosCajas_ProveedoresPagosProveedoresChanged = function () {
        if ($scope.reporteador.MovimientosCajas.PagosProveedores.SeleccionarTodosLosProveedores != undefined) {
            $scope.reporteador.MovimientosCajas.PagosProveedores.SeleccionarTodosLosProveedores = !($scope.reporteador.MovimientosCajas.PagosProveedores.ProveedoresSeleccionados.length > 0)
            $scope.Actualizar_URLMovimientosCajas_PagosProveedores();
        }
    }
     
    $scope.Actualizar_URLMovimientosCajas_PagosProveedores = function () {
        var idsCajas = "";
        var nombresCajas = "";
        if (!$scope.reporteador.MovimientosCajas.SeleccionarTodasLasCajas) {
            nombresCajas = "&nombresCajas=";
            for (var i = 0; i < $scope.reporteador.MovimientosCajas.CajasSeleccionadas.length; i++) {
                idsCajas += "&idsCajas=" + $scope.reporteador.MovimientosCajas.CajasSeleccionadas[i].Id;
                nombresCajas += $scope.reporteador.MovimientosCajas.CajasSeleccionadas[i].Nombre + " | ";
            }
        }
        var idsProveedores = "";
        var nombresProveedores = "";
        if (!$scope.reporteador.MovimientosCajas.SeleccionarTodosLosProveedores) {
            nombresProveedores = "&nombresProveedores=";
            for (var i = 0; i < $scope.reporteador.MovimientosCajas.PagosProveedores.ProveedoresSeleccionados.length; i++) {
                idsProveedores += "&idsProveedores=" + $scope.reporteador.MovimientosCajas.PagosProveedores.ProveedoresSeleccionados[i].Id;
                nombresProveedores += $scope.reporteador.MovimientosCajas.PagosProveedores.ProveedoresSeleccionados[i].Nombre + " | ";
            }
        }
        $scope.URLMovimientosCajas_PagosProveedores = URL_ + "/FinanzaReportes_MovimientosCajas/MovimientosCajas_PagosProveedores?"
            + "&fechaDesde=" + (new Date($scope.reporteador.MovimientosCajas.FechaDesde)).toUTCString()
            + "&fechaHasta=" + (new Date($scope.reporteador.MovimientosCajas.FechaHasta)).toUTCString()
            + "&todasLasCajas=" + $scope.reporteador.MovimientosCajas.SeleccionarTodasLasCajas
            + idsCajas + nombresCajas
            + "&todosLosProveedores=" + $scope.reporteador.MovimientosCajas.PagosProveedores.SeleccionarTodosLosProveedores
            + idsProveedores + nombresProveedores;
    }

    $scope.obtenerCajas();
};




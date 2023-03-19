PorCobrarPagarController = function ($scope, $q, $compile, SweetAlert, $rootScope, $timeout, DTOptionsBuilder, DTColumnDefBuilder, actorComercialService) {
    $scope.PorCobrarPagar_EvaluarBotonesVer = function () {

        $scope.reporteador.PorCobrarPagar.PorCobrarPorCliente.ActivarBotonVer = ($scope.reporteador.PorCobrarPagar.PorCobrarPorCliente.SeleccionarTodosLosClientes || $scope.reporteador.PorCobrarPagar.PorCobrarPorCliente.ClientesSeleccionadas.length > 0);

        $scope.reporteador.PorCobrarPagar.PorPagarPorProveedor.ActivarBotonVer = ($scope.reporteador.PorCobrarPagar.PorPagarPorProveedor.SeleccionarTodosLosProveedores || $scope.reporteador.PorCobrarPagar.PorPagarPorProveedor.ProveedoresSeleccionados.length > 0);

        $scope.reporteador.PorCobrarPagar.PorCobrarGrupos.ActivarBotonVer = ($scope.reporteador.PorCobrarPagar.PorCobrarGrupos.SeleccionarTodosLosGrupos || $scope.reporteador.PorCobrarPagar.PorCobrarGrupos.GruposSeleccionados.length > 0);

        $scope.reporteador.PorCobrarPagar.PorCobrarGrupoDetallado.ActivarBotonVer = $scope.reporteador.PorCobrarPagar.PorCobrarGrupoDetallado.GrupoSeleccionado != undefined;
    }

    $scope.PorCobrarPagar_actualizarURLs = function () {
        $scope.Actualizar_URLPorCobrarPagar_PorCobrarPorCliente();
        $scope.Actualizar_URLPorCobrarPagar_PorPagarPorProveedor();
        $scope.Actualizar_URLPorCobrarPagar_PorCobrarGrupos();
        $scope.Actualizar_URLPorCobrarPagar_PorCobrarGrupoDetallado();
        $scope.PorCobrarPagar_EvaluarBotonesVer();
    }

    $scope.obtenerGruposActoresComerciales = function () {
        actorComercialService.obtenerGruposActoresComerciales({}).success(function (data) {
            $scope.grupos = data;
        }).error(function (data) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }

    $scope.PorCobrarPagar_TodosLosClientesPorCobrarPorClienteChanged = function () {
        if ($scope.reporteador.PorCobrarPagar.PorCobrarPorCliente.SeleccionarTodosLosClientes) {
            $scope.reporteador.PorCobrarPagar.PorCobrarPorCliente.ClientesSeleccionados = [];
            $timeout(function () { $('#PorCobrarPagar_clientes_PorCobrarPorCliente').trigger("change"); }, 100);
        }
        $scope.Actualizar_URLPorCobrarPagar_PorCobrarPorCliente();
        $scope.PorCobrarPagar_EvaluarBotonesVer();
    }

    $scope.PorCobrarPagar_ClientesPorCobrarPorClienteChanged = function () {
        if ($scope.reporteador.PorCobrarPagar.PorCobrarPorCliente.SeleccionarTodosLosClientes != undefined) {
            $scope.reporteador.PorCobrarPagar.PorCobrarPorCliente.SeleccionarTodosLosClientes = !($scope.reporteador.PorCobrarPagar.PorCobrarPorCliente.ClientesSeleccionados.length > 0)
            $scope.Actualizar_URLPorCobrarPagar_PorCobrarPorCliente();
            $scope.PorCobrarPagar_EvaluarBotonesVer();
        }
    }

    $scope.Actualizar_URLPorCobrarPagar_PorCobrarPorCliente = function () {
        var idsClientes = "";
        var nombresClientes = "";
        if (!$scope.reporteador.PorCobrarPagar.PorCobrarPorCliente.SeleccionarTodosLosClientes) {
            nombresClientes = "&nombresClientes=";
            for (var i = 0; i < $scope.reporteador.PorCobrarPagar.PorCobrarPorCliente.ClientesSeleccionados.length; i++) {
                idsClientes += "&idsClientes=" + $scope.reporteador.PorCobrarPagar.PorCobrarPorCliente.ClientesSeleccionados[i].Id;
                nombresClientes += $scope.reporteador.PorCobrarPagar.PorCobrarPorCliente.ClientesSeleccionados[i].Nombre + " | ";
            }
        }
        $scope.URLPorCobrarPagar_PorCobrarPorCliente = URL_ + "/FinanzaReportes_PorCobrarPagar/PorCobrarPagar_PorCobrarPorCliente?"
            + "&todosLosClientes=" + $scope.reporteador.PorCobrarPagar.PorCobrarPorCliente.SeleccionarTodosLosClientes
            + idsClientes + nombresClientes;
    }

    $scope.PorCobrarPagar_TodosLosProveedoresPorPagarPorProveedorChanged = function () {
        if ($scope.reporteador.PorCobrarPagar.PorPagarPorProveedor.SeleccionarTodosLosProveedores) {
            $scope.reporteador.PorCobrarPagar.PorPagarPorProveedor.ProveedoresSeleccionados = [];
            $timeout(function () { $('#PorCobrarPagar_proveedores_PorPagarPorProveedor').trigger("change"); }, 100);
        }
        $scope.Actualizar_URLPorCobrarPagar_PorPagarPorProveedor();
        $scope.PorCobrarPagar_EvaluarBotonesVer();
    }

    $scope.PorCobrarPagar_ProveedoresPorPagarPorProveedorChanged = function () {
        if ($scope.reporteador.PorCobrarPagar.PorPagarPorProveedor.SeleccionarTodosLosProveedores != undefined) {
            $scope.reporteador.PorCobrarPagar.PorPagarPorProveedor.SeleccionarTodosLosProveedores = !($scope.reporteador.PorCobrarPagar.PorPagarPorProveedor.ProveedoresSeleccionados.length > 0)
            $scope.Actualizar_URLPorCobrarPagar_PorPagarPorProveedor();
            $scope.PorCobrarPagar_EvaluarBotonesVer();
        }
    }

    $scope.Actualizar_URLPorCobrarPagar_PorPagarPorProveedor = function () {
        var idsProveedores = "";
        var nombresProveedores = "";
        if (!$scope.reporteador.PorCobrarPagar.PorPagarPorProveedor.SeleccionarTodosLosProveedores) {
            nombresProveedores = "&nombresProveedores=";
            for (var i = 0; i < $scope.reporteador.PorCobrarPagar.PorPagarPorProveedor.ProveedoresSeleccionados.length; i++) {
                idsProveedores += "&idsProveedores=" + $scope.reporteador.PorCobrarPagar.PorPagarPorProveedor.ProveedoresSeleccionados[i].Id;
                nombresProveedores += $scope.reporteador.PorCobrarPagar.PorPagarPorProveedor.ProveedoresSeleccionados[i].Nombre + " | ";
            }
        }
        $scope.URLPorCobrarPagar_PorPagarPorProveedor = URL_ + "/FinanzaReportes_PorCobrarPagar/PorCobrarPagar_PorPagarPorProveedor?"
            + "&todosLosProveedores=" + $scope.reporteador.PorCobrarPagar.PorPagarPorProveedor.SeleccionarTodosLosProveedores
            + idsProveedores + nombresProveedores;
    }

    $scope.PorCobrarPagar_TodosLosGruposPorCobrarGruposChanged = function () {
        if ($scope.reporteador.PorCobrarPagar.PorCobrarGrupos.SeleccionarTodosLosGrupos) {
            $scope.reporteador.PorCobrarPagar.PorCobrarGrupos.GruposSeleccionados = [];
            $timeout(function () { $('#PorCobrarPagar_grupos_PorCobrarGrupos').trigger("change"); }, 100);
        }
        $scope.Actualizar_URLPorCobrarPagar_PorCobrarGrupos();
        $scope.PorCobrarPagar_EvaluarBotonesVer();
    }

    $scope.PorCobrarPagar_GruposPorCobrarGruposChanged = function () {
        if ($scope.reporteador.PorCobrarPagar.PorCobrarGrupos.SeleccionarTodosLosGrupos != undefined) {
            $scope.reporteador.PorCobrarPagar.PorCobrarGrupos.SeleccionarTodosLosGrupos = !($scope.reporteador.PorCobrarPagar.PorCobrarGrupos.GruposSeleccionados.length > 0)
            $scope.Actualizar_URLPorCobrarPagar_PorCobrarGrupos();
            $scope.PorCobrarPagar_EvaluarBotonesVer();
        }
    }

    $scope.Actualizar_URLPorCobrarPagar_PorCobrarGrupos = function () {
        var idsGrupos = "";
        var nombresGrupos = "";
        if (!$scope.reporteador.PorCobrarPagar.PorCobrarGrupos.SeleccionarTodosLosGrupos) {
            nombresGrupos = "&nombresGrupos=";
            for (var i = 0; i < $scope.reporteador.PorCobrarPagar.PorCobrarGrupos.GruposSeleccionados.length; i++) {
                idsGrupos += "&idsGrupos=" + $scope.reporteador.PorCobrarPagar.PorCobrarGrupos.GruposSeleccionados[i].Id;
                nombresGrupos += $scope.reporteador.PorCobrarPagar.PorCobrarGrupos.GruposSeleccionados[i].Nombre + " | ";
            }
        }
        $scope.URLPorCobrarPagar_PorCobrarGrupos = URL_ + "/FinanzaReportes_PorCobrarPagar/PorCobrarPagar_PorCobrarGrupos?"
            + "&todosLosGrupos=" + $scope.reporteador.PorCobrarPagar.PorCobrarGrupos.SeleccionarTodosLosGrupos
            + idsGrupos + nombresGrupos;
    }

    $scope.PorCobrarPagar_GrupoPorCobrarGrupoDetalladoChanged = function () {
        $scope.Actualizar_URLPorCobrarPagar_PorCobrarGrupoDetallado();
        $scope.PorCobrarPagar_EvaluarBotonesVer();
    }

    $scope.Actualizar_URLPorCobrarPagar_PorCobrarGrupoDetallado = function () {
        if ($scope.reporteador.PorCobrarPagar.PorCobrarGrupoDetallado.GrupoSeleccionado != undefined) {
            $scope.URLPorCobrarPagar_PorCobrarGrupoDetallado = URL_ + "/FinanzaReportes_PorCobrarPagar/PorCobrarPagar_PorCobrarGrupoDetallado?"
                + "&idGrupo=" + $scope.reporteador.PorCobrarPagar.PorCobrarGrupoDetallado.GrupoSeleccionado.Id
                + "&nombreGrupo=" + $scope.reporteador.PorCobrarPagar.PorCobrarGrupoDetallado.GrupoSeleccionado.Nombre;
        }
    }

    $scope.obtenerGruposActoresComerciales();
};




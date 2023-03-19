app.controller('ingresosYEgresosVariosController', function ($scope, SweetAlert, $timeout, DTOptionsBuilder, DTColumnDefBuilder, finanzaService, maestroService, ventaService, clienteService, compraService, empleadoService) {

    $scope.tiposDeComprobantesMasSeries = [];
    $scope.tiposDeComprobantes = [];
    $scope.series = [];
    $scope.emisores = [];
    $scope.pagadoresBeneficiarios = [];
    $scope.modelo = {};
    $scope.tipoMovimiento = {};

    $scope.obtenerTiposDeComprobante = function () {
        finanzaService.obtenerTiposDeComprobanteParaIngresoEgresoVarios({ esIngreso: $scope.tipoMovimiento }).success(function (data) {
            $scope.selectorTiposDeComprobantesMasSeries = data;
            $scope.tiposDeComprobantes = [];
            for (var i = 0; i < $scope.selectorTiposDeComprobantesMasSeries.length; i++) {
                $scope.tiposDeComprobantes.push($scope.selectorTiposDeComprobantesMasSeries[i]);
            }
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.cargarSeries = function (tipoComprobante) {
        if (tipoComprobante != null) {
            $scope.series = angular.copy(tipoComprobante.Series);
        }
    }

    $scope.inicioIngresoVarios = function () {
        $scope.tipoMovimiento = true;
        $scope.inicioRegistroIngresoEgreso();
    }

    $scope.inicioEgresoVarios = function () {
        $scope.tipoMovimiento = false;
        $scope.inicioRegistroIngresoEgreso();
    }

    $scope.inicioRegistroIngresoEgreso = function () {
        $scope.etiquetaPagadorBeneficiario = $scope.tipoMovimiento ? 'PAGADOR' : 'BENEFICIARIO';
        $scope.etiquetaIngresoEgreso = $scope.tipoMovimiento ? 'INGRESO' : 'EGRESO';
        $scope.modelo = {};
        $scope.modelo.Observacion = 'NINGUNA';
        $scope.tipoPagadorBeneficiario = 1;
        $scope.obtenerTiposDeComprobante();
        $scope.nombreCajero = nombreCajero;
        $scope.idClienteGenerico = idClienteGenerico;
        $scope.rolCliente = { Id: idRolCliente, Nombre: 'CLIENTE' };
        $scope.rolProveedor = { Id: idRolProveedor, Nombre: 'PROVEEDOR' };
        $scope.rolEmpleado = { Id: idRolEmpleado, Nombre: 'EMPLEADO' };
        $scope.tiempoEsperaBusquedaSelector = tiempoEsperaBusquedaSelector;
        $scope.minimoCaracteresBuscarActorComercial = minimoCaracteresBuscarActorComercial;
        $scope.mascaraDeVisualizacionValidacionRegistroCliente = mascaraDeVisualizacionValidacionRegistroCliente;
        $scope.mascaraDeVisualizacionValidacionRegistroProveedor = mascaraDeVisualizacionValidacionRegistroProveedor;
        $scope.mascaraDeVisualizacionValidacionRegistroEmpleado = mascaraDeVisualizacionValidacionRegistroEmpleado;
        $scope.inicializacionRealizada = true; 
        $scope.validarModal();
    }

    
    $scope.cambioAutorizado = function (actorComercial) {
        $scope.modelo.Emisor = actorComercial;
        $scope.validarModal();
    };

    $scope.cambioPagadorBeneficiario = function (actorComercial) {
        $scope.modelo.PagadorBeneficiario = actorComercial;
        $scope.validarModal();
    };

    $scope.registrarIngresoEgresoVarios = function () {
        finanzaService.guardarIngresoEgresoVarios({ esIngreso: $scope.tipoMovimiento, modelo: $scope.modelo }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.modelo = {};
            $('#modal-registro-ingreso-egreso-varios').modal('hide');
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.validarImporte = function () {
        $scope.modelo.Importe = ($scope.modelo.Importe == undefined) ? '0' : $scope.modelo.Importe;
        if ($scope.modelo.Importe.split(".").length > 2) {
            var montos = $scope.modelo.Importe.split(".");
            $scope.modelo.Importe = montos[0] + '.' + montos[1];
        }
        $scope.modelo.Importe = parseFloat($scope.modelo.Importe).toFixed(2);
    }

    $scope.validarModal = function () {
        $scope.mensajeAdvertencia = [];
        if ($scope.modelo.PagadorBeneficiario == undefined) {
            $scope.mensajeAdvertencia.push('Es necesario seleccionar un pagador o beneficiario.');
        }
        if ($scope.modelo.Emisor == undefined) {
            $scope.mensajeAdvertencia.push('Es necesario seleccionar un autorizado.');
        }
        if ($scope.modelo.TipoDeComprobante == undefined) {
            $scope.mensajeAdvertencia.push('Es necesario seleccionar un comprobante.');
        }
        if ($scope.modelo.Importe == undefined || $scope.modelo.Importe == 0) {
            $scope.mensajeAdvertencia.push('Es necesario que el importe sea mayor a 0.');
        }
        if ($scope.modelo.Observacion == undefined) {
            $scope.mensajeAdvertencia.push('Es necesario ingresar la observacion.');
        }
    }
});
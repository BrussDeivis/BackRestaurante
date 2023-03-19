EstadoCuentaController = function ($scope, $q, $compile, SweetAlert, $rootScope, $timeout, DTOptionsBuilder, DTColumnDefBuilder) {
    $scope.EstadoCuenta_EvaluarBotonesVer = function () {
        $scope.reporteador.EstadoCuenta.EstadoCuentaPorCliente.ActivarBotonVer = ($scope.reporteador.EstadoCuenta.FechaDesde != undefined && $scope.reporteador.EstadoCuenta.FechaHasta != undefined && $scope.reporteador.EstadoCuenta.EstadoCuentaPorCliente.ClienteSeleccionado != undefined);
    }

    $scope.EstadoCuenta_actualizarURLs = function () {
        $scope.Actualizar_URLEstadoCuenta_EstadoCuentaPorCliente();
        $scope.EstadoCuenta_EvaluarBotonesVer();
    }

    $scope.EstadoCuenta_ClienteEstadoCuentaPorCliente = function () {
        $scope.Actualizar_URLEstadoCuenta_EstadoCuentaPorCliente();
        $scope.EstadoCuenta_EvaluarBotonesVer();
    }

    $scope.Actualizar_URLEstadoCuenta_EstadoCuentaPorCliente = function () {
        if ($scope.reporteador.EstadoCuenta.EstadoCuentaPorCliente.ClienteSeleccionado != undefined) {
            $scope.URLEstadoCuenta_EstadoCuentaPorCliente = URL_ + "/FinanzaReportes_EstadoCuenta/EstadoCuenta_EstadoCuentaPorCliente?"
                + "&fechaDesde=" + (new Date($scope.reporteador.EstadoCuenta.FechaDesde)).toUTCString()
                + "&fechaHasta=" + (new Date($scope.reporteador.EstadoCuenta.FechaHasta)).toUTCString()
                + "&idCliente=" + $scope.reporteador.EstadoCuenta.EstadoCuentaPorCliente.ClienteSeleccionado.Id
                + "&nombreCliente=" + $scope.reporteador.EstadoCuenta.EstadoCuentaPorCliente.ClienteSeleccionado.Nombre;
        }
    }
};




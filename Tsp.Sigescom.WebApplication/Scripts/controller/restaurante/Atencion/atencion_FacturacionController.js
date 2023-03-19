FacturacionAtencionController = function ($scope, $q, $timeout, $rootScope, $compile, SweetAlert, restauranteService) {

    $scope.iniciarFacturacion = function () {
        $('#modal-facturador-restaurante').modal('show');
        $scope.facturadorRestauranteAPI.SetAtencionAFacturar($scope.atencionActual);
    }

    $scope.verBotonFacturarAtencion = function () {
        return $scope.atencionActual.Estado == $scope.ParametrosDeAtencion.IdEstadoRegistrado || $scope.atencionActual.Estado == $scope.ParametrosDeAtencion.IdEstadoCerrado;
    }
    $scope.puedeFacturarAtencion = function () {
        return $scope.atencionActual.Estado == $scope.ParametrosDeAtencion.IdEstadoCerrado;
    }
    $scope.cerrarFacturacion = function (seFacturo) {
        if (seFacturo) {
            if ($scope.atencionConMesa == 'true') {
                $scope.obtenerMesasDeAmbiente($scope.ambienteActual.Id);
                $scope.iniciarVisualizadorFacturador();
            } else {
                $scope.iniciarVisualizadorFacturador();
            }
        }
        else {
            $scope.limpiarAtencion();
            $scope.limpiarOrden();
            $scope._banderaMesaSeleccionada = false;
            if ($scope.atencionConMesa == 'false') {
                $scope.nuevaAtencionPorLista();
            }
        }
    }
    $scope.iniciarVisualizadorFacturador = function () {
        $('#modal-visualizador-facturador').modal('show');
        $scope.visualizadorFacturadorAPI.SetAtencionAVisualizar($scope.atencionActual);
    }
    $scope.actualizarNumeroDeComprobantes = function () {
        if ($scope.numeroDeComprobantes > 0 && $scope.numeroDeComprobantes < 10) {
            if ($scope.numeroDeComprobantes > $scope.atencionActual.Comprobantes.length) {
                for (var i = 0; i < $scope.numeroDeComprobantes - $scope.atencionActual.Comprobantes.length; i++) {
                    var comprobante = $scope.obtenerFormatoDeComprobante();
                    if ($scope.atencionActual.TipoDePago == 1) {
                        comprobante.DetallesDeComprobante.push({
                            Id: 0,
                            Descripcion: "Por consumo"
                        })
                    }
                    $scope.atencionActual.Comprobantes.push(comprobante)
                }
                $scope.actualizarImporteDeConsumoPagoDividido();
            } else if ($scope.numeroDeComprobantes < $scope.atencionActual.Comprobantes.length) {
                for (var i = $scope.atencionActual.Comprobantes.length - 1; i >= $scope.numeroDeComprobantes; i--) {
                    $scope.atencionActual.Comprobantes.splice(i, 1);
                }
                $scope.actualizarImporteDeConsumoPagoDividido();
            }
        }
    }
    $scope.obtenerFormatoDeComprobante = function () {
        var comprobante = {
            Id: 0,
            Detallado: false,
            DetallesDeComprobante: [],
            IdAtencion: $scope.atencionActual.Id,
            TipoComprobante: 0,
            ImporteComprobante: 0,
            ClienteNombre: "VARIOS",
            ClienteDocumento: "00000000"
        }
        return comprobante;
    }
    $scope.actualizarImporteDeConsumoPagoDividido = function () {
        var montoDePartesIguales = $scope.atencionActual.ImporteAtencion / $scope.numeroDeComprobantes;
        for (var i = 0; i < $scope.atencionActual.Comprobantes.length; i++) {
            for (var j = 0; j < $scope.atencionActual.Comprobantes[i].DetallesDeComprobante.length; j++) {
                $scope.atencionActual.Comprobantes[i].DetallesDeComprobante[j].Importe = montoDePartesIguales;
            }
        }
    }
    $scope.configurarPagoSimple = function () {
        $scope.facturacionAPI.setTotalVenta($scope.salida.DetallesACobrar.Total);
    }
    $scope.configurarPagoDividoEnPartesIguales = function () {
        $scope.facturacionAPI.setTotalVenta($scope.salida.DetallesACobrar.Total);
    }
    $scope.configurarPagoDivididoDetallado = function () {
        $scope.facturacionAPI.setTotalVenta($scope.salida.DetallesACobrar.Total);
    }

    $scope.cerrarVisualizador = function () {
        if ($scope.atencionConMesa == 'true') {
            $scope.limpiarAtencion();
            $scope.limpiarOrden();
            $scope._banderaMesaSeleccionada = false;
        } else {
            $scope.obtenerAtencionesSinMesa();
            $scope.nuevaAtencionPorLista();
        }
    }

}

DetalleOrdenAtencionController = function ($scope, $q, $timeout, $rootScope, $compile, SweetAlert, restauranteService) {

    $scope.quitarAnotacionIndicacion = (indexOrden, indexDetalle) => {
        $scope.atencionActual.Ordenes[indexOrden].DetallesDeOrden[indexDetalle].DetalleItemRestaurante.AnotacionIndicacion = $scope.atencionActual.Ordenes[indexOrden].DetallesDeOrden[indexDetalle].DetalleItemRestaurante.AnotacionIndicacionExistente;
    }

    $scope.quitarAnotacionObservacion = (indexOrden, indexDetalle) => {
        $scope.atencionActual.Ordenes[indexOrden].DetallesDeOrden[indexDetalle].DetalleItemRestaurante.AnotacionObservacion = '';
    }

    $scope.quitarRegistroAnotacionDetalle = (indexOrden, indexDetalle) => {
        let nodo = document.getElementById('anotacion-detalleOrden' + indexOrden + indexDetalle);
        for (var i = 0; i < nodo.classList.length; i++) {
            if (nodo.classList[i] == 'in')
                nodo.classList.toggle('in');
        }
    }
    $scope.ponleFocusAnotacion = (indexOrden) => {
        $timeout(function () { $('#input-anotacion' + indexOrden).trigger("focus"); }, 100);
    }


    $scope.ponleFocusAnotacionDetalle = (indexOrden, indexDetalle) => {
        $scope.quitarAnotacionIndicacion(indexOrden, indexDetalle);
        $scope.quitarAnotacionObservacion(indexOrden, indexDetalle);
        $timeout(function () { $('#input-anotacion-detalleOrden' + indexOrden + indexDetalle).trigger("focus"); }, 100);
    }

    $scope.puedeAnularDetalleOrden = function (detalle) {
        return detalle.Estado == $scope.ParametrosDeDetallesDeOrden.EstadoRegistrado;
    }

    $scope.puedeAtenderDetalleOrden = function (detalle) {
        return (detalle.Estado == $scope.ParametrosDeDetallesDeOrden.EstadoServido || detalle.Estado == $scope.ParametrosDeDetallesDeOrden.EstadoObservado || detalle.Estado == $scope.ParametrosDeDetallesDeOrden.EstadoRegistrado || detalle.Estado == $scope.ParametrosDeDetallesDeOrden.EstadoPreparando);
    }

    $scope.puedeObservarDetalleOrden = function (detalle) {
        return (detalle.Estado == $scope.ParametrosDeDetallesDeOrden.EstadoServido || detalle.Estado == $scope.ParametrosDeDetallesDeOrden.EstadoAtendido);
    }

    $scope.puedeDevolverDetalleOrden = function (detalle) {
        return detalle.Estado == $scope.ParametrosDeDetallesDeOrden.EstadoAtendido;
    }

    $scope.puedeReanudarDetalleOrden = function (detalle) {
        return detalle.Estado == $scope.ParametrosDeDetallesDeOrden.EstadoAnulado;
    }

    /**
     * Todos los detalles deben estar anulados
     * @param {any} orden
     */
    $scope.puedeReanudarTodosLosDetallesDeOrden = function (orden) {
        var detallesAnulados = orden.DetallesDeOrden.filter(dor => dor.Estado == $scope.ParametrosDeDetallesDeOrden.EstadoAnulado).length;
        return (orden.DetallesDeOrden.length == detallesAnulados && orden.Estado != $scope.ParametrosDeOrden.EstadoCerrado);
    }
    /**
     * Todos los detalles deben estar registrados
     * @param {any} orden
     */
    $scope.puedeAtenderTodosLosDetallesDeOrden = function (orden) {
        var detallesRegistrados = orden.DetallesDeOrden.filter(dor => dor.Estado == $scope.ParametrosDeDetallesDeOrden.EstadoRegistrado).length;
        return (orden.DetallesDeOrden.length == detallesRegistrados && orden.Estado != $scope.ParametrosDeOrden.EstadoCerrado);
    }

    /**
     * Todos los  deben estar o anulados o registrados
     * @param {any} orden
     */
    $scope.puedeAnularTodosLosDetallesDeOrden = function (orden) {
        var totalDetalles = orden.DetallesDeOrden.length;
        var detallesAnulados = orden.DetallesDeOrden.filter(dor => dor.Estado == $scope.ParametrosDeDetallesDeOrden.EstadoAnulado).length;
        var detallesRegistrados = orden.DetallesDeOrden.filter(dor => dor.Estado == $scope.ParametrosDeDetallesDeOrden.EstadoRegistrado).length;
        return (totalDetalles == (detallesAnulados + detallesRegistrados) && detallesAnulados < totalDetalles && orden.Estado != $scope.ParametrosDeOrden.EstadoCerrado);
    }

    $scope.puedeCerrarOrden = function (orden) {
        var totalDetalles = orden.DetallesDeOrden.length;
        var detallesAnulados = orden.DetallesDeOrden.filter(dor => dor.Estado == $scope.ParametrosDeDetallesDeOrden.EstadoAnulado).length;
        var detallesDevueltos = orden.DetallesDeOrden.filter(dor => dor.Estado == $scope.ParametrosDeDetallesDeOrden.EstadoDevuelto).length;
        var detallesAtendidos = orden.DetallesDeOrden.filter(dor => dor.Estado == $scope.ParametrosDeDetallesDeOrden.EstadoAtendido).length;
        return (totalDetalles == (detallesAnulados + detallesDevueltos + detallesAtendidos));
    }

    $scope.verBotonCerrarOrden = function (orden) {
        return orden.Estado == $scope.ParametrosDeOrden.EstadoConfirmado;
    }
    //endregion


    $scope.guardarAnotacionIndicacionDetalleOrden = function (IndexOrden, IndexDetalle) {
        let detalle = $scope.atencionActual.Ordenes[IndexOrden].DetallesDeOrden[IndexDetalle];
        let jsonString = JSON.stringify(detalle.DetalleItemRestaurante);
        restauranteService.actualizarJsonDetalleDeDetalleOrden({ idDetalle: detalle.Id, jsonString: jsonString }).success(data => {
            $scope.atencionActual.Ordenes[IndexOrden].DetallesDeOrden[IndexDetalle].DetalleItemRestaurante.AnotacionIndicacionExistente = $scope.atencionActual.Ordenes[IndexOrden].DetallesDeOrden[IndexDetalle].DetalleItemRestaurante.AnotacionIndicacion;
        }).error(function (data) {
            SweetAlert.error2(data);
        })
    }

    $scope.guardarAnotacionObservacionDetalleOrden = function (IndexOrden, IndexDetalle) {
        let detalle = $scope.atencionActual.Ordenes[IndexOrden].DetallesDeOrden[IndexDetalle];
        let jsonString = JSON.stringify(detalle.DetalleItemRestaurante);
        restauranteService.actualizarJsonDetalleDeDetalleOrden({ idDetalle: detalle.Id, jsonString: jsonString }).success(data => {

        }).error(function (data) {
            SweetAlert.error2(data);
        })
    }
    $scope.cambiarEstadoDeDetalleDeOrden = function (IndexOrden, IndexDetalle, Estado) {
        var detalle = $scope.atencionActual.Ordenes[IndexOrden].DetallesDeOrden[IndexDetalle];
        var EstadoAnterior = detalle.Estado;


        restauranteService.cambiarEstadoDeDetalleDeOrden({ IdDetalleDeOrden: detalle.Id, Estado: Estado }).success(data => {
            detalle.Estado = Estado;
            if ((EstadoAnterior != $scope.ParametrosDeDetallesDeOrden.EstadoAnulado && EstadoAnterior != $scope.ParametrosDeDetallesDeOrden.EstadoDevuelto) && (Estado == $scope.ParametrosDeDetallesDeOrden.EstadoAnulado || Estado == $scope.ParametrosDeDetallesDeOrden.EstadoDevuelto)) {
                $scope.atencionActual.Ordenes[IndexOrden].ImporteOrden -= detalle.Importe;
                $scope.atencionActual.ImporteAtencion -= detalle.Importe;
            } else if ((Estado != $scope.ParametrosDeDetallesDeOrden.EstadoAnulado && Estado != $scope.ParametrosDeDetallesDeOrden.EstadoDevuelto) && (EstadoAnterior == $scope.ParametrosDeDetallesDeOrden.EstadoAnulado || Estado == $scope.ParametrosDeDetallesDeOrden.EstadoDevuelto)) {
                $scope.atencionActual.Ordenes[IndexOrden].ImporteOrden += detalle.Importe;
                $scope.atencionActual.ImporteAtencion += detalle.Importe;
            }
        }).error(function (data) {
            SweetAlert.error2(data);
        })
    }
    $scope.atenderDetalleDeOrden = function (IndexOrden, IndexDetalle) {
        var detalle = $scope.atencionActual.Ordenes[IndexOrden].DetallesDeOrden[IndexDetalle];
        $scope.quitarAnotacionIndicacion(IndexOrden, IndexDetalle);
        $scope.quitarRegistroAnotacionDetalle(IndexOrden, IndexDetalle);
        restauranteService.atenderDetalleDeOrden({ id: detalle.Id }).success(data => {
            detalle.Estado = $scope.ParametrosDeDetallesDeOrden.EstadoAtendido;
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }
    $scope.servirDetalleDeOrden = function (IndexOrden, IndexDetalle) {
        var detalle = $scope.atencionActual.Ordenes[IndexOrden].DetallesDeOrden[IndexDetalle];
        restauranteService.servirDetalleDeOrden({ id: detalle.Id }).success(function (data) {
            detalle.Estado = $scope.ParametrosDeDetallesDeOrden.EstadoServido;
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }
    $scope.prepararDetalleDeOrden = function (IndexOrden, IndexDetalle) {
        var detalle = $scope.atencionActual.Ordenes[IndexOrden].DetallesDeOrden[IndexDetalle];
        restauranteService.prepararDetalleDeOrden({ id: detalle.Id }).success(data => {
            detalle.Estado = $scope.ParametrosDeDetallesDeOrden.EstadoServido;
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }
    $scope.anularDetalleDeOrden = function (IndexOrden, IndexDetalle) {
        var detalle = $scope.atencionActual.Ordenes[IndexOrden].DetallesDeOrden[IndexDetalle];
        var orden = $scope.atencionActual.Ordenes[IndexOrden];
        $scope.quitarAnotacionIndicacion(IndexOrden, IndexDetalle);
        $scope.quitarRegistroAnotacionDetalle(IndexOrden, IndexDetalle);
        restauranteService.anularDetalleDeOrden({ id: detalle.Id }).success(data => {
            detalle.Estado = $scope.ParametrosDeDetallesDeOrden.EstadoAnulado;
            $scope.calcularImporteOrden(orden);
            $scope.calcularImporteAtencion($scope.atencionActual);
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }
    $scope.observarDetalleDeOrden = function (IndexOrden, IndexDetalle) {
        var detalle = $scope.atencionActual.Ordenes[IndexOrden].DetallesDeOrden[IndexDetalle];
        restauranteService.observarDetalleDeOrden({ id: detalle.Id }).success(data => {
            detalle.Estado = $scope.ParametrosDeDetallesDeOrden.EstadoObservado;
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }
    $scope.devolverDetalleDeOrden = function (IndexOrden, IndexDetalle) {
        var orden = $scope.atencionActual.Ordenes[IndexOrden];
        var detalle = orden.DetallesDeOrden[IndexDetalle];
        $scope.quitarAnotacionObservacion(IndexOrden, IndexDetalle);
        $scope.quitarRegistroAnotacionDetalle(IndexOrden, IndexDetalle);
        restauranteService.devolverDetalleDeOrden({ id: detalle.Id }).success(data => {
            detalle.Estado = $scope.ParametrosDeDetallesDeOrden.EstadoDevuelto;
            $scope.calcularImporteOrden(orden);
            $scope.calcularImporteAtencion($scope.atencionActual);
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }
    $scope.reanudarDetalleDeOrden = function (IndexOrden, IndexDetalle) {
        var orden = $scope.atencionActual.Ordenes[IndexOrden];
        var detalle = orden.DetallesDeOrden[IndexDetalle];
        restauranteService.reanudarDetalleDeOrden({ id: detalle.Id }).success(data => {
            detalle.Estado = $scope.ParametrosDeDetallesDeOrden.EstadoRegistrado;
            $scope.calcularImporteOrden(orden);
            $scope.calcularImporteAtencion($scope.atencionActual);
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

}

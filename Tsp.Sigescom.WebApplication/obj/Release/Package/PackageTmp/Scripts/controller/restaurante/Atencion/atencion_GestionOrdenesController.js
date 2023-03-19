GestionOrdenesAtencionController = function ($scope, $q, $timeout, $rootScope, $compile, SweetAlert, restauranteService) {

    $scope.cambiarEstadoDeOrdenDeConfirmadoAAtendido = function (orden) {
        var NuevoEstado = $scope.ParametrosDeOrden.EstadoAtendido;

        var Ids = [];
        var IndexDetalles = [];

        for (var i = 0; i < orden.DetallesDeOrden.length; i++) {
            let detalle = orden.DetallesDeOrden[i];
            if (detalle.Estado != $scope.ParametrosDeDetallesDeOrden.EstadoAnulado || detalle.Estado != $scope.ParametrosDeDetallesDeOrden.EstadoDevuelto) {
                Ids.push(detalle.Id);
                IndexDetalles.push(i);
            }
        }
        restauranteService.cambiarEstadoDeOrden({ IdOrden: orden.Id, Estado: $scope.ParametrosDeOrden.EstadoAtendido }).success(data => {
            orden.Estado = NuevoEstado;
            restauranteService.cambiarEstadoDeDetallesDeOrden({ Ids: Ids, NumEstado: $scope.ParametrosDeDetallesDeOrden.EstadoAtendido }).success(data => {
                for (var i = 0; i < orden.DetallesDeOrden.length; i++) {
                    if (IndexDetalles.includes(i)) {
                        orden.DetallesDeOrden[i].Estado = $scope.ParametrosDeDetallesDeOrden.EstadoAtendido;
                    }
                }
            }).error(data => {
                $scope.messageError(data);
            });
        }).error(error => {
            $scope.messageError(error);
        })


    }

    $scope.cambiarEstadoDeOrdenDeConfirmadoAAnulado = function (orden) {
        var IdsDetallesNoAnuladosNoDevueltosNoAtendidos = [];
        var NuevoEstadoOrden = $scope.ParametrosDeOrden.EstadoAnulado;
        var IndexDetallesNoAnuladosNoDevueltosNoAtendidos = [];
        let existeAtendido = false;
        let IdsAtendidos = [];
        let IndexAtendidos = [];
        for (var i = 0; i < orden.DetallesDeOrden.length; i++) {
            let detalle = orden.DetallesDeOrden[i];
            if (detalle.Estado == $scope.ParametrosDeDetallesDeOrden.EstadoAtendido) {
                existeAtendido = true;
                IdsAtendidos.push(detalle.Id);
                IndexAtendidos.push(i);
            } else if (detalle.Estado != $scope.ParametrosDeDetallesDeOrden.EstadoAnulado && detalle.Estado != $scope.ParametrosDeDetallesDeOrden.EstadoDevuelto) {
                IdsDetallesNoAnuladosNoDevueltosNoAtendidos.push(detalle.Id);
                IndexDetallesNoAnuladosNoDevueltosNoAtendidos.push(i);
            }
        }
        if (existeAtendido) {
            Swal.fire({
                title: '¿Estas seguro?',
                text: "Actualmente cuenta con items que ya han sido ATENDIDOS, ¿Que desea hacer? . ",
                showDenyButton: true,
                showCancelButton: true,
                confirmButtonText: `Anular los items no atendidos y devolver los que ya están atendidos!`,
                denyButtonText: `Anular solo los items no atendidos. La orden quedará como atendida`,
            }).then((result) => {
                /* Read more about isConfirmed, isDenied below */
                if (result.isConfirmed) {
                    if (IdsDetallesNoAnuladosNoDevueltosNoAtendidos.length > 0) {
                        
                        var nuevoEstadoDetalle = $scope.ParametrosDeDetallesDeOrden.EstadoAnulado;
                        $scope.cambiarEstadoDeDetallesDeOrdenAAnuladoODevuelto(orden, IdsDetallesNoAnuladosNoDevueltosNoAtendidos, nuevoEstadoDetalle, NuevoEstadoOrden, IndexDetallesNoAnuladosNoDevueltosNoAtendidos);
                    }
                    if (IdsAtendidos > 0) {
                        var nuevoEstadoDetalle = $scope.ParametrosDeDetallesDeOrden.EstadoDevuelto;
                        $scope.cambiarEstadoDeDetallesDeOrdenAAnuladoODevuelto(orden, IdsAtendidos, nuevoEstadoDetalle, NuevoEstadoOrden, IndexDetallesNoAnuladosNoDevueltosNoAtendidos);
                    }
                } else if (result.isDenied) {
                    if (IdsDetallesNoAnuladosNoDevueltosNoAtendidos.length > 0) {
                        var nuevoEstadoDetalle = $scope.ParametrosDeDetallesDeOrden.EstadoAnulado;
                        $scope.cambiarEstadoDeDetallesDeOrdenAAnuladoODevuelto(orden, IdsDetallesNoAnuladosNoDevueltosNoAtendidos, nuevoEstadoDetalle, NuevoEstadoOrden, IndexDetallesNoAnuladosNoDevueltosNoAtendidos);
                    }
                    restauranteService.cambiarEstadoDeOrden({ IdOrden: orden.Id, Estado: $scope.ParametrosDeOrden.EstadoAtendido }).success(data => {
                        orden.Estado = $scope.ParametrosDeOrden.EstadoAtendido;
                    }).error(error => $scope.messageError(error));
                }
            })
        }
        else {
            Swal.fire({
                title: '¿Estas seguro?',
                text: "Se anulará la orden y todos sus items",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Confirmar!'
            }).then((result) => {
                if (result.isConfirmed) {
                    $scope.anularOrdenYSusDetalles(orden, IdsDetallesNoAnuladosNoDevueltosNoAtendidos);
                }
            })
        }
    }

    $scope.anularOrdenYSusDetalles== function (orden, idsDetallesNoAnuladosNoDevueltosNoAtendidos) {
        var nuevoEstadoOrden = $scope.ParametrosDeOrden.EstadoAnulado;
        if (idsDetallesNoAnuladosNoDevueltosNoAtendidos.length > 0) {
            var nuevoEstadoDetalle = $scope.ParametrosDeDetallesDeOrden.EstadoAnulado
            restauranteService.cambiarEstadoDeDetallesDeOrden({ Ids: idsDetallesNoAnuladosNoDevueltosNoAtendidos, NumEstado: nuevoEstadoDetalle }).success(data => {
                orden.Estado = nuevoEstadoOrden;
                for (var i = 0; i < orden.DetallesDeOrden.length; i++) {
                    if (IndexDetallesNoAnuladosNoDevueltosNoAtendidos.includes(i)) {
                        orden.DetallesDeOrden[i].Estado = $scope.ParametrosDeDetallesDeOrden.EstadoAnulado;
                    }
                }
            }).error(error => {
                $scope.messageError(error);
            })
        }


    }

    $scope.cambiarEstadoDeDetallesDeOrdenAAnuladoODevuelto = function (orden, idsDetalles, nuevoEstadoDetalle, nuevoEstadoOrden, indexDetallesNoAnuladosNoDevueltosNoAtendidos) {
        restauranteService.cambiarEstadoDeDetallesDeOrden({ Ids: idsDetalles, NumEstado: nuevoEstadoDetalle }).success(data => {
            orden.Estado = nuevoEstadoOrden;
            for (var i = 0; i < orden.DetallesDeOrden.length; i++) {
                if (indexDetallesNoAnuladosNoDevueltosNoAtendidos.includes(i)) {
                    orden.DetallesDeOrden[i].Estado = nuevoEstadoDetalle;
                    orden.ImporteOrden -= orden.DetallesDeOrden[i].Importe;
                    $scope.atencionActual -= orden.DetallesDeOrden[i].Importe;
                }
            }
        }).error(error => {
            $scope.messageError(error);
        })
    }

    $scope.cambiarEstadoDeOrdenDeAtendidoAConfirmado = function (orden) {
        var NuevoEstado = $scope.ParametrosDeOrden.EstadoConfirmado;
        restauranteService.cambiarEstadoDeOrden({ IdOrden: orden.Id, Estado: NuevoEstado }).success(data => {
            orden.Estado = NuevoEstado;
        }).error(error => {
            $scope.messageError(error);
        })
    }
    $scope.cambiarEstadoDeOrden = function (IndexOrden, NuevoEstado) {

        var orden = $scope.atencionActual.Ordenes[IndexOrden];
        var AnteriorEstado = orden.Estado;
        
        if (AnteriorEstado == $scope.ParametrosDeOrden.EstadoConfirmado && NuevoEstado == $scope.ParametrosDeOrden.EstadoAtendido) {
            $scope.cambiarEstadoDeOrdenDeConfirmadoAAtendido(orden);
        }
        else if (AnteriorEstado == $scope.ParametrosDeOrden.EstadoConfirmado && NuevoEstado == $scope.ParametrosDeOrden.EstadoAnulado) {
            $scope.cambiarEstadoDeOrdenDeConfirmadoAAnulado(orden);
        }
        else if (AnteriorEstado == $scope.ParametrosDeOrden.EstadoAtendido && NuevoEstado == $scope.ParametrosDeOrden.EstadoConfirmado) {
            $scope.cambiarEstadoDeOrdenDeAtendidoAConfirmado(orden);
        }
    }
    //region calcular

    /**
    * Todos los detalles deben estar anulados
    * @param {any} orden
    */
    $scope.reanudarTodosLosDetallesDeOrden = function (orden) {
        restauranteService.reanudarTodosLosDetallesDeOrden({ idOrden: orden.Id }).success(data => {
            orden.DetallesDeOrden.forEach(detalle => {
                detalle.Estado = $scope.ParametrosDeDetallesDeOrden.EstadoRegistrado;
            })
            $scope.calcularImporteOrden(orden);
            $scope.calcularImporteAtencion($scope.atencionActual);
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }
    /**
     * Todos los detalles deben estar registrados
     * @param {any} orden
     */
    $scope.atenderTodosLosDetallesDeOrden = function (orden) {
        restauranteService.atenderTodosLosDetallesDeOrden({ idOrden: orden.Id }).success(data => {
            orden.DetallesDeOrden.forEach(detalle => {
                detalle.Estado = $scope.ParametrosDeDetallesDeOrden.EstadoAtendido;
            })
            $scope.calcularImporteOrden(orden);
            $scope.calcularImporteAtencion($scope.atencionActual);
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }
    /**
     * Todos los  deben estar o anulados o registrados
     * @param {any} orden
     */
    $scope.anularTodosLosDetallesDeOrden = function (orden) {
        restauranteService.anularTodosLosDetallesDeOrden({ idOrden: orden.Id }).success(data => {
            orden.DetallesDeOrden.forEach(detalle => {
                detalle.Estado = $scope.ParametrosDeDetallesDeOrden.EstadoAnulado;
            })
            $scope.calcularImporteOrden(orden);
            $scope.calcularImporteAtencion($scope.atencionActual);
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }
    /**
     * La orden puede cerrarse cuando sus detalles se encuentran devueltos, anulados o atendidos
     * @param {any} orden
     */
    $scope.cerrarOrden = function (orden) {
        restauranteService.cerrarOrden({ idOrden: orden.Id }).success(data => {
            orden.Estado = $scope.ParametrosDeOrden.EstadoCerrado;
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }
    $scope.imprimirOrden = function (orden) {
        jsWebClientPrint.print('tipoDocumento=1&idDocumento=' + orden.Id);

        //restauranteService.obtenerOrdenHtml({ idOrden: orden.Id }).success(data => {
        //    let printWin = window.open(' ', 'popimpr' /*, 'left=0,top=0,width=350,height=250,toolbar=0,scrollbars=0,status  =0'*/);
        //    let html = data;
        //    printWin.document.write(html);
        //    printWin.document.close();
        //    printWin.focus();
        //    printWin.print();
        //    printWin.close();
        //}).error(function (data) {
        //    SweetAlert.error2(data);
        //});
    }
    $scope.modificarImportesOrdenYAtencionAlAnularODevolver = function (detalle, orden, atencion, estadoAnterior) {
        if (estadoAnterior != $scope.ParametrosDeDetallesDeOrden.EstadoAnulado && estadoAnterior != $scope.ParametrosDeDetallesDeOrden.EstadoDevuelto) {
            orden.ImporteOrden -= detalle.Importe;
            atencion.ImporteAtencion -= detalle.Importe;
        }
    }

}

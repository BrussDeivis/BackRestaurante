app.controller('ventaCorporativaController', function ($scope, $timeout, $rootScope, $q, SweetAlert, $filter, DTOptionsBuilder, DTColumnDefBuilder, ventaService, maestroService, clienteService, productoService, centroDeAtencionService, empleadoService, conceptoService, compraService, almacenService) {

    //#region VENTA CORPORATIVA

    $scope.inicializarVentaCorporativa = function () {
        $scope.cargarParametrosVentaCorporativa();
        $scope.limpiarVentaCorporativa();
        $scope.cargarColeccionesAsyncVentaCorporativa();
        $scope.cargarColeccionesSyncVentaCorporativa().then(function (resultado_) {
            $scope.establecerDatosPorDefectoVentaCorporativa();
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
    }

    $scope.cargarParametrosVentaCorporativa = function () {
        $scope.idEstablecimientoComercialPorDefecto = idEstablecimientoComercialPorDefecto;
        $scope.idCentroAtencionPorDefecto = idCentroAtencionPorDefecto;
        $scope.idEmpleadoPorDefecto = idEmpleadoPorDefecto;
    }

    $scope.limpiarVentaCorporativa = function () {
        $scope.esVentaCorporativa = true;
    }

    $scope.cargarColeccionesAsyncVentaCorporativa = function () {

    }

    $scope.cargarColeccionesSyncVentaCorporativa = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        var promiseList = [];
        promiseList.push($scope.obtenerEstablecimientosComerciales());
        promiseList.push($scope.obtenerEmpleadosConRolVendedor());
        return $q.all(promiseList).then(function (response) {
            defered.resolve();
        }).catch(function (error) {
            defered.reject(e);
        });
        return promise;
    }

    $scope.establecerDatosPorDefectoVentaCorporativa = function () {
        $scope.esVentaCorporativa = true;
        $scope.establecerEstablecimientoComercialDeVendedorPorDefecto();
        $scope.establecerVendedorPorDefecto();
    }

    $scope.obtenerEstablecimientosComerciales = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        centroDeAtencionService.obtenerEstablecimientosComerciales().success(function (data) {
            $scope.listaEstablecimientosComerciales = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.obtenerCentrosAtencionDeVendedor = function (id) {
        var defered = $q.defer();
        var promise = defered.promise;
        centroDeAtencionService.obtenerCentrosDeAtencionConRolPuntoDeVentaVigentesPorEstablecimientoComercial({ idEstablecimientoComercial: id }).success(function (data) {
            $scope.listaCentrosDeAtencionDeVendedor = data;
            $scope.establecerCentroAtencionDeVendedorPorDefecto();
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.obtenerEmpleadosConRolVendedor = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        empleadoService.obtenerEmpleadosConRolVendedor({}).success(function (data) {
            $scope.listaVendedores = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.establecerEstablecimientoComercialDeVendedorPorDefecto = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        var establecimientoComercial = Enumerable.from($scope.listaEstablecimientosComerciales)
            .where("$.Id == '" + $scope.idEstablecimientoComercialPorDefecto + "'").toArray()[0];
        $scope.venta.EstablecimientoComercial = establecimientoComercial != null ? establecimientoComercial : $scope.listaEstablecimientosComerciales[0];
        $timeout(function () { $('#establecimientoComercial').trigger("change"); }, 100);
        $scope.obtenerCentrosAtencionDeVendedor($scope.idEstablecimientoComercialPorDefecto);
        return promise;
    }

    $scope.establecerCentroAtencionDeVendedorPorDefecto = function () {
        var centroAtencion = Enumerable.from($scope.listaCentrosDeAtencionDeVendedor)
            .where("$.Id == '" + $scope.idCentroAtencionPorDefecto + "'").toArray()[0];
        $scope.venta.CentroDeAtencion = centroAtencion != null ? centroAtencion : $scope.listaCentrosDeAtencionDeVendedor[0];
        $timeout(function () { $('#centroDeAtencion').trigger("change"); }, 100);
    }

    $scope.establecerVendedorPorDefecto = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        var vendedor = Enumerable.from($scope.listaVendedores)
            .where("$.Id == '" + $scope.idEmpleadoPorDefecto + "'").toArray()[0];
        $scope.venta.Vendedor = vendedor != null ? vendedor : $scope.listaVendedores[0];
        $timeout(function () { $('#vendedor').trigger("change"); }, 100);
        defered.resolve();
        return promise;
    }

    //#endregion

    //#region TRAZA DE PAGO DE VENTA CORPORATIVA

    $scope.inicializarTrazaDePago = function () {
        $scope.cargarParametrosTrazaDePago();
        $scope.limpiarTrazaDePago();
        $scope.cargarColeccionesAsyncTrazaDePago();
        $scope.cargarColeccionesSyncTrazaDePago().then(function (resultado_) {
            $scope.establecerDatosPorDefectoTrazaDePago();
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
    }

    $scope.cargarParametrosTrazaDePago = function () {
        //$scope.idEstablecimientoComercialPorDefecto = idEstablecimientoComercialPorDefecto;
        //$scope.idCentroAtencionPorDefecto = idCentroAtencionPorDefecto;
        //$scope.idEmpleadoPorDefecto = idEmpleadoPorDefecto;
        $scope.idDetalleMaestroMedioDePagoEfectivo = idDetalleMaestroMedioDePagoEfectivo;
        $scope.idDetalleMaestroEntidadBancariaNinguna = idDetalleMaestroEntidadBancariaNinguna;
    }

    $scope.limpiarTrazaDePago = function () {
        $scope.trazaDePago = {};
        $scope.hayInconsistenciasTrazaDePago = {};
        $scope.inconsistenciasTrazaDePago = [];
        $scope.desabilitarCheckboxTrazaDePago = {};
        $scope.venta.HayRegistroTrazaDePago = {};
    }

    $scope.cargarColeccionesAsyncTrazaDePago = function () {

    }

    $scope.cargarColeccionesSyncTrazaDePago = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        var promiseList = [];
        promiseList.push($scope.obtenerEstablecimientosComerciales());
        promiseList.push($scope.obtenerEmpleadosConRolCajero());
        promiseList.push($scope.obtenerMediosDePagoDeTrazaDePago());
        return $q.all(promiseList).then(function (response) {
            defered.resolve();
        }).catch(function (error) {
            defered.reject(e);
        });
        return promise;
    }

    $scope.establecerDatosPorDefectoTrazaDePago = function () {
        $scope.trazaDePago.InformacionDePago = "NINGUNA";
        $scope.hayInconsistenciasTrazaDePago = false;
        $scope.venta.HayRegistroTrazaDePago = false;
        $scope.establecerEstablecimientoComercialDeCajeroPorDefecto();
        $scope.establecerCajeroPorDefecto();
        $scope.establecerMedioDePagoDeTrazaDePagoPorDefecto();
        if (!$scope.venta.EsCompraACredito || ($scope.venta.Inicial > 0 && !$scope.venta.EsCreditoRapido)) { $scope.desabilitarCheckboxTrazaDePago = false; }
        else { $scope.desabilitarCheckboxTrazaDePago = true; }
        $scope.deshabilitarTrazaDePago();
    }

    $scope.obtenerCentrosAtencionDeCajero = function (id) {
        var defered = $q.defer();
        var promise = defered.promise;
        centroDeAtencionService.obtenerCentrosDeAtencionConRolCajaVigentesPorEstablecimientoComercial({ idEstablecimientoComercial: id }).success(function (data) {
            $scope.listaCentrosDeAtencionDeCajero = data;
            $scope.establecerCentroAtencionDeCajeroPorDefecto();
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.obtenerEmpleadosConRolCajero = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        empleadoService.obtenerEmpleadosConRolCajero({}).success(function (data) {
            $scope.listaCajeros = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.obtenerMediosDePagoDeTrazaDePago = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        maestroService.obtenerMediosDePago({}).success(function (data) {
            $scope.listaMediosDePago = data;
            defered.resolve();
        }).error(function (data) {
            SweetAlert.error("Ocurrio un Problema", data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.obtenerEntidadesFinancierasDeTrazaDePago = function (idMedioDePago) {
        maestroService.obtenerEntidadesBancarias({}).success(function (data) {
            $scope.listaEntidadesFinancieras = data;
            $scope.establecerEntidadFinancieraPorDefecto();
        }).error(function (data) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }

    $scope.establecerEstablecimientoComercialDeCajeroPorDefecto = function () {
        var establecimientoComercial = Enumerable.from($scope.listaEstablecimientosComerciales)
            .where("$.Id == '" + $scope.idEstablecimientoComercialPorDefecto + "'").toArray()[0];
        $scope.trazaDePago.EstablecimientoComercial = establecimientoComercial != null ? establecimientoComercial : $scope.listaEstablecimientosComerciales[0];
        $timeout(function () { $('#establecimientoComercialTrazaDePago').trigger("change"); }, 100);
        $scope.obtenerCentrosAtencionDeCajero($scope.idEstablecimientoComercialPorDefecto);
    }

    $scope.establecerCentroAtencionDeCajeroPorDefecto = function () {
        var centroAtencion = Enumerable.from($scope.listaCentrosDeAtencionDeCajero)
            .where("$.Id == '" + $scope.idCentroAtencionPorDefecto + "'").toArray()[0];
        $scope.trazaDePago.CentroDeAtencion = centroAtencion != null ? centroAtencion : $scope.listaCentrosDeAtencionDeCajero[0];
        $timeout(function () { $('#centroDeAtencionTrazaDePago').trigger("change"); }, 100);
    }

    $scope.establecerCajeroPorDefecto = function () {
        var cajero = Enumerable.from($scope.listaCajeros)
            .where("$.Id == '" + $scope.idEmpleadoPorDefecto + "'").toArray()[0];
        $scope.trazaDePago.Cajero = cajero != null ? cajero : $scope.listaCajeros[0];
        $timeout(function () { $('#cajero').trigger("change"); }, 100);
    }

    $scope.establecerMedioDePagoDeTrazaDePagoPorDefecto = function () {
        var medioDePago = Enumerable.from($scope.listaMediosDePago)
            .where("$.Id == '" + $scope.idDetalleMaestroMedioDePagoEfectivo + "'").toArray()[0];
        $scope.trazaDePago.MedioDePago = medioDePago != null ? medioDePago : $scope.listaMediosDePago[0];
        $timeout(function () { $('#medioDePago').trigger("change"); }, 100);
        $scope.obtenerEntidadesFinancierasDeTrazaDePago($scope.idDetalleMaestroMedioDePagoEfectivo);
    }

    $scope.establecerEntidadFinancieraPorDefecto = function () {
        if ($scope.trazaDePago.MedioDePago.Id == $scope.idDetalleMaestroMedioDePagoEfectivo) {
            var entidadFinanciera = Enumerable.from($scope.listaEntidadesFinancieras)
                .where("$.Id == '" + $scope.idDetalleMaestroEntidadBancariaNinguna + "'").toArray()[0];
            $scope.trazaDePago.EntidadFinanciera = entidadFinanciera != null ? entidadFinanciera : $scope.listaEntidadesFinancieras[0];
            $timeout(function () { $('#entidadFinanciera').trigger("change"); }, 100);
        }
    }

    $scope.accionTrazaDePago = function () {
        if ($scope.venta.HayRegistroTrazaDePago) {
            $scope.habilitarTrazaDePago();
            $scope.hayInconsistenciasTrazaDePago = true;
            $scope.verificarInconsistenciasTrazaDePago();
        } else {
            $scope.deshabilitarTrazaDePago();
            $scope.hayInconsistenciasTrazaDePago = false;
        }
    }

    $scope.deshabilitarTrazaDePago = function () {
        document.getElementById("establecimientoComercialTrazaDePago").disabled = true;
        document.getElementById("centroDeAtencionTrazaDePago").disabled = true;
        document.getElementById("cajero").disabled = true;
        document.getElementById("medioDePago").disabled = true;
        document.getElementById("entidadFinanciera").disabled = true;
        document.getElementById("informacionPago").disabled = true;
    }

    $scope.habilitarTrazaDePago = function () {
        document.getElementById("establecimientoComercialTrazaDePago").disabled = false;
        document.getElementById("centroDeAtencionTrazaDePago").disabled = false;
        document.getElementById("cajero").disabled = false;
        document.getElementById("medioDePago").disabled = false;
        document.getElementById("entidadFinanciera").disabled = false;
        document.getElementById("informacionPago").disabled = false;
    }

    $scope.verificarInconsistenciasTrazaDePago = function () {
        $scope.inconsistenciasTrazaDePago = [];
        if ($scope.trazaDePago.EstablecimientoComercial == undefined) {
            $scope.inconsistenciasTrazaDePago.push("Es necesario seleccionar un establecimiento comercial.");
        } if ($scope.trazaDePago.CentroDeAtencion == undefined) {
            $scope.inconsistenciasTrazaDePago.push("Es necesario seleccionar un centro de atencion.");
        } if ($scope.trazaDePago.Cajero == undefined) {
            $scope.inconsistenciasTrazaDePago.push("Es necesario seleccionar un cajero.");
        } if ($scope.trazaDePago.MedioDePago == undefined) {
            $scope.inconsistenciasTrazaDePago.push("Es necesario seleccionar un medio de pago.");
        } if ($scope.trazaDePago.EntidadFinanciera == undefined) {
            $scope.inconsistenciasTrazaDePago.push("Es necesario seleccionar una entidad financiera.");
        }
        // if ($scope.trazaDePago.InformacionDePago == undefined) {
        //    $scope.trazaDePago.InformacionDePago = "NINGUNA";
        //}
        $scope.hayInconsistenciasTrazaDePago = ($scope.inconsistenciasTrazaDePago.length == 0) ? false : true;
    }

    //#endregion

    //#region SALIDA DE MERCADERIA

    $scope.inicializarSalidaDeMercaderia = function () {
        $scope.cargarParametrosSalidaDeMercaderia();
        $scope.limpiarSalidaDeMercaderia();
        $scope.cargarColeccionesAsyncSalidaDeMercaderia();
        $scope.cargarColeccionesSyncSalidaDeMercaderia().then(function (resultado_) {
            $scope.establecerDatosPorDefectoSalidaDeMercaderia();
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
    }

    $scope.cargarParametrosSalidaDeMercaderia = function () {
        $scope.idEstablecimientoComercialPorDefecto = idEstablecimientoComercialPorDefecto;
        $scope.idCentroAtencionPorDefecto = idCentroAtencionPorDefecto;
        $scope.idEmpleadoPorDefecto = idEmpleadoPorDefecto;
        $scope.idDocumentoNotaAlamacenInterna = idDocumentoNotaAlamacenInterna;
        $scope.direccionSede = direccionSede;
        $scope.idModalidadTrasladoPorDefecto = idModalidadTrasladoPorDefecto;
        $scope.idMotivoTrasladoPorDefecto = idMotivoTrasladoPorDefecto;

    }

    $scope.limpiarSalidaDeMercaderia = function () {
        $scope.movimiento = { Detalles: [] };
        $scope.movimiento.EsTrasladoTotal = {};
        $scope.hayInconsistenciasSalidaDeMercaderia = {};
        $scope.inconsistenciasSalidaDeMercaderia = [];
        $scope.venta.HayRegistroSalidaDeMercaderia = {};
        $scope.venta.UsaComprobanteOrden = {};

    }

    $scope.cargarColeccionesAsyncSalidaDeMercaderia = function () {
        $scope.obtenerTransportistas();
        $scope.obtenerTiposDeComprobanteMovimientoMercaderia();
    }

    $scope.cargarColeccionesSyncSalidaDeMercaderia = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        var promiseList = [];
        promiseList.push($scope.obtenerEstablecimientosComerciales());
        promiseList.push($scope.obtenerEmpleadosConRolAlmacenero());
        promiseList.push($scope.obtenerModalidadesTraslado());
        promiseList.push($scope.obtenerMotivosTraslado());
        promiseList.push($scope.obtenerDireccionDeCliente());

        return $q.all(promiseList).then(function (response) {
            defered.resolve();
        }).catch(function (error) {
            defered.reject(e);
        });
        return promise;
    }

    $scope.establecerDatosPorDefectoSalidaDeMercaderia = function () {
        $scope.movimiento.DireccionDestino = $scope.direccionDeCliente;
        $scope.accionDeshabilitar(true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
        $scope.movimiento.Observacion = 'NINGUNO';
        $scope.movimiento.EsTrasladoTotal = true;
        $scope.movimiento.DireccionOrigen = $scope.direccionSede;
        $scope.movimiento.DireccionDestino = $scope.direccionDeCliente;
        $scope.establecerModalidadTrasladoPorDefecto();
        $scope.establecerMotivoTrasladoPorDefecto();
        $scope.establecerEstablecimientoComercialDeAlmaceneroPorDefecto();
        $scope.establecerAlmaceneroPorDefecto();
        $scope.hayInconsistenciasSalidaDeMercaderia = false;
        $scope.venta.HayRegistroSalidaDeMercaderia = false;
        $scope.venta.UsaComprobanteOrden = false;
        $scope.tipoMovimiento = 'false';
        //Iniciar la tabla de salida de mercaderia
        for (var i = 0; i < $scope.venta.Detalles.length; i++) {
            $scope.movimiento.Detalles.push({ IdProducto: $scope.venta.Detalles[i].Producto.Id, Descripcion: $scope.venta.Detalles[i].Producto.Nombre, Ordenado: parseFloat($scope.venta.Detalles[i].Cantidad), RecibidoEntregado: 0.00, IngresoSalidaActual: parseFloat($scope.venta.Detalles[i].Cantidad) });
        }
    }

    $scope.obtenerCentrosAtencionDeAlmacenero = function (id) {
        var defered = $q.defer();
        var promise = defered.promise;
        centroDeAtencionService.obtenerCentrosDeAtencionConRolAlmacenVigentesPorEstablecimientoComercial({ idEstablecimientoComercial: id }).success(function (data) {
            $scope.listaCentrosDeAtencionDeAlmacenero = data;
            $scope.establecerCentroAtencionDeAlmaceneroPorDefecto();
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.obtenerEmpleadosConRolAlmacenero = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        empleadoService.obtenerEmpleadosConRolAlmacenero({}).success(function (data) {
            $scope.listaAlmaceneros = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.obtenerTransportistas = function () {
        compraService.obtenerProveedores({}).success(function (data) {
            $scope.transportistas = data;
        }).error(function (data) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }

    $scope.obtenerModalidadesTraslado = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        maestroService.obtenerModalidadesTraslado().success(function (data) {
            $scope.modalidadesTraslado = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.obtenerMotivosTraslado = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        maestroService.obtenerMotivosTraslado().success(function (data) {
            $scope.motivosTraslado = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;

    }

    $scope.establecerEstablecimientoComercialDeAlmaceneroPorDefecto = function () {
        var establecimientoComercial = Enumerable.from($scope.listaEstablecimientosComerciales)
            .where("$.Id == '" + $scope.idEstablecimientoComercialPorDefecto + "'").toArray()[0];
        $scope.movimiento.EstablecimientoComercial = establecimientoComercial != null ? establecimientoComercial : $scope.listaEstablecimientosComerciales[0];
        $timeout(function () { $('#establecimientoComercialSalidaDeMercaderia').trigger("change"); }, 100);
        $scope.obtenerCentrosAtencionDeAlmacenero($scope.idEstablecimientoComercialPorDefecto);
    }

    $scope.establecerCentroAtencionDeAlmaceneroPorDefecto = function () {
        var centroAtencion = Enumerable.from($scope.listaCentrosDeAtencionDeAlmacenero)
            .where("$.Id == '" + $scope.idCentroAtencionPorDefecto + "'").toArray()[0];
        $scope.movimiento.CentroDeAtencion = centroAtencion != null ? centroAtencion : $scope.listaCentrosDeAtencionDeAlmacenero[0];
        $timeout(function () { $('#centroDeAtencionSalidaDeMercaderia').trigger("change"); }, 100);
    }

    $scope.establecerAlmaceneroPorDefecto = function () {
        var almacenero = Enumerable.from($scope.listaAlmaceneros)
            .where("$.Id == '" + $scope.idEmpleadoPorDefecto + "'").toArray()[0];
        $scope.movimiento.Almacenero = almacenero != null ? almacenero : $scope.listaAlmaceneros[0];
        $timeout(function () { $('#almacenero').trigger("change"); }, 100);
    }

    $scope.establecerModalidadTrasladoPorDefecto = function () {
        //Modalidad: el parametro de modalidad por defecto o el primero
        var modalidadTrasladoPorDefecto = Enumerable.from($scope.modalidadesTraslado)
            .where("$.Id == '" + $scope.idModalidadTrasladoPorDefecto + "'").toArray()[0];
        $scope.movimiento.ModalidadTransporte = modalidadTrasladoPorDefecto != null ? modalidadTrasladoPorDefecto : $scope.modalidadesTraslado[0];
        $timeout(function () { $('#modalidad').trigger("change"); }, 100);
    }

    $scope.establecerMotivoTrasladoPorDefecto = function () {
        //Almacen: el parametro de motivo por defecto o el primero
        var motivoTrasladoPorDefecto = Enumerable.from($scope.motivosTraslado)
            .where("$.Id == '" + $scope.idMotivoTrasladoPorDefecto + "'").toArray()[0];
        $scope.movimiento.MotivoTraslado = motivoTrasladoPorDefecto != null ? motivoTrasladoPorDefecto : $scope.motivosTraslado[0];
        $timeout(function () { $('#motivo').trigger("change"); }, 100);
    }

    $scope.obtenerTiposDeComprobanteMovimientoMercaderia = function () {
        almacenService.obtenerTiposDeComprobanteParaAlmacen({}).success(function (data) {
            $scope.tiposDeComprobantesMasSeriesMovimientoMercaderia = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.cargarSeriesMovimientoMercaderia = function (tipoComprobante) {
        if (tipoComprobante != null) {
            $scope.seriesMovimientoMercaderia = angular.copy(tipoComprobante.Series);
        }
    }

    $scope.obtenerDireccionDeCliente = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        if ($scope.venta.Cliente != undefined) {
            clienteService.obtenerDireccionCliente({ idCliente: $scope.venta.Cliente.Id }).success(function (data) {
                $scope.direccionDeCliente = data;
                defered.resolve();
            }).error(function (data) {
                $scope.messageError(data.error);
                defered.reject(data);
            });
        } else {
            defered.resolve();
        }
        return promise;
    }

    $scope.accionSalidaDeMercaderia = function () {
        if ($scope.venta.HayRegistroSalidaDeMercaderia) {
            $scope.accionDeshabilitar(false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);
            $scope.hayInconsistenciasSalidaDeMercaderia = true;
            $scope.verificarInconsistenciasMovimientoMercaderia();
        } else {
            $scope.accionDeshabilitar(true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
            $scope.venta.UsaComprobanteOrden = false;
            $scope.hayInconsistenciasSalidaDeMercaderia = false;
            $scope.limpiarSalidaDeMercaderia();
            $scope.establecerDatosPorDefectoSalidaDeMercaderia();
        }
    }

    $scope.accionUsaComprobanteOrden = function () {
        if ($scope.venta.UsaComprobanteOrden) {
            $scope.accionDeshabilitar(false, false, false, true, true, true, true, true, true, true, false, true, true, true, true, true, false);
            $scope.hayInconsistenciasSalidaDeMercaderia = false;
        } else {
            $scope.accionDeshabilitar(false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);
            $scope.hayInconsistenciasSalidaDeMercaderia = true;
        }
        $scope.verificarInconsistenciasMovimientoMercaderia();
    }

    $scope.verificarInconsistenciasMovimientoMercaderia = function () {
        var hayCantidadNoValida = false;
        $scope.inconsistenciasSalidaDeMercaderia = [];
        if ($scope.venta.HayRegistroSalidaDeMercaderia) {
            if ($scope.venta.UsaComprobanteOrden == false) {
                if ($scope.movimiento.TipoDeComprobante == undefined) {
                    $scope.inconsistenciasSalidaDeMercaderia.push("Es necesario seleccionar un documento.");
                } else {
                    if ($scope.movimiento.TipoDeComprobante.EsPropio == false) {
                        if ($scope.movimiento.TipoDeComprobante.SerieIngresada == "" || $scope.movimiento.TipoDeComprobante.SerieIngresada == null) {
                            $scope.inconsistenciasSalidaDeMercaderia.push("Es necesario ingresar la serie de comprobante.");
                        } if ($scope.movimiento.TipoDeComprobante.NumeroIngresado == "" || $scope.movimiento.TipoDeComprobante.NumeroIngresado == null) {
                            $scope.inconsistenciasSalidaDeMercaderia.push("Es necesario ingresar el numero de comprobante.");
                        }
                    }
                    if ($scope.movimiento.TipoDeComprobante.TipoComprobante.Id != $scope.idDocumentoNotaAlamacenInterna) {
                        if ($scope.movimiento.FechaInicioTraslado == "" || $scope.movimiento.FechaInicioTraslado == undefined) {
                            $scope.inconsistenciasSalidaDeMercaderia.push("Es necesario ingresar la fecha de inicio de traslado.");
                        } if ($scope.movimiento.Transporte != undefined) {
                            if ($scope.movimiento.Transporte.Transportista == undefined) {
                                $scope.inconsistenciasSalidaDeMercaderia.push("Es necesario ingresar el transportista.");
                            } if ($scope.movimiento.Transporte.MarcaYPlaca == "" || $scope.movimiento.Transporte.MarcaYPlaca == undefined) {
                                $scope.inconsistenciasSalidaDeMercaderia.push("Es necesario ingresar la marca y placa del vehiculo.");
                            } if ($scope.movimiento.Transporte.NumeroLicencia == "" || $scope.movimiento.Transporte.NumeroLicencia == undefined) {
                                $scope.inconsistenciasSalidaDeMercaderia.push("Es necesario ingresar el numero de licencia del conductor.");
                            }
                        } if ($scope.movimiento.ModalidadTransporte == undefined) {
                            $scope.inconsistenciasSalidaDeMercaderia.push("Es necesario seleccionar la modalidad de transporte.");
                        } if ($scope.movimiento.MotivoTraslado == undefined) {
                            $scope.inconsistenciasSalidaDeMercaderia.push("Es necesario seleccionar el motivo de transporte.");
                        } if ($scope.movimiento.DireccionOrigen == "" || $scope.movimiento.DireccionOrigen == undefined) {
                            $scope.inconsistenciasSalidaDeMercaderia.push("Es necesario ingresar la direccion origen del destino.");
                        } if ($scope.movimiento.DireccionDestino == "" || $scope.movimiento.DireccionDestino == undefined) {
                            $scope.inconsistenciasSalidaDeMercaderia.push("Es necesario ingresarla direccion destino del traslado.");
                        }
                    }
                }
            }
            //    $scope.hayInconsistenciasSalidaDeMercaderia = (temp == 0) ? false : true;
            //} else {
            //    $scope.inconsistenciasSalidaDeMercaderia = [];
            //    $scope.hayInconsistenciasSalidaDeMercaderia = false;
            //}
            for (var i = 0; i < $scope.movimiento.Detalles.length; i++) {
                hayCantidadNoValida = $scope.movimiento.Detalles[i].IngresoSalidaActual < 0 || $scope.movimiento.Detalles[i].IngresoSalidaActual > ($scope.movimiento.Detalles[i].Ordenado - $scope.movimiento.Detalles[i].RecibidoEntregado);
                break;
            }
            if (hayCantidadNoValida) {
                $scope.inconsistenciasSalidaDeMercaderia.push("Es necesario la cantidad de los detalles sea mayor o igual a 0 y menor a la diferencia de ordenado y recibido.");
            }
            $scope.hayInconsistenciasSalidaDeMercaderia = ($scope.inconsistenciasSalidaDeMercaderia.length == 0) ? false : true;
        } else {
            $scope.inconsistenciasSalidaDeMercaderia = [];
            $scope.hayInconsistenciasSalidaDeMercaderia = false;
        }
    }

    $scope.accionDeshabilitar = function (establecimientoComercial, centroDeAtencion, almacenero, documento, serieIngresada, numeroIngresado, fechaRegistro, editarTransportista, nuevoTransportista, transportista, marcaPlaca, nLicencia, observacion, modalidad, motivo, direccionOrigen, direccionDestino) {
        document.getElementById("establecimientoComercialSalidaDeMercaderia").disabled = establecimientoComercial;
        document.getElementById("centroDeAtencionSalidaDeMercaderia").disabled = centroDeAtencion;
        document.getElementById("almacenero").disabled = almacenero;
        document.getElementById("documento").disabled = documento;
        document.getElementById("serieIngresada").disabled = serieIngresada;
        document.getElementById("numeroIngresado").disabled = numeroIngresado;
        document.getElementById("fechaInicioTraslado").disabled = fechaRegistro;
        //document.getElementById("editarTransportista").disabled = editarTransportista;
        //document.getElementById("nuevoTransportista").disabled = nuevoTransportista;
        document.getElementById("transportista").disabled = transportista;
        document.getElementById("marcaPlaca").disabled = marcaPlaca;
        document.getElementById("nLicencia").disabled = nLicencia;
        document.getElementById("observacionMovimientoMercaderia").disabled = observacion;
        document.getElementById("modalidad").disabled = modalidad;
        document.getElementById("motivo").disabled = motivo;
        document.getElementById("direccionOrigen").disabled = direccionOrigen;
        document.getElementById("direccionDestino").disabled = direccionDestino;
    }

    //#endregion




});



/*app.controller('ventaController', function ($scope, $q, $timeout, $rootScope, SweetAlert, $filter, DTOptionsBuilder, DTColumnDefBuilder, ventaService, compraService, maestroService, almacenService, clienteService, productoService, centroDeAtencionService, empleadoService, conceptoService, proveedorService) {

    //#region Inicio de Variables

    $scope.clientes = [];
    $scope.conceptos = [];
    $scope.conceptoBasicoSeleccionado = {};
    $scope.productos = [];
    $scope.tiposDeComprobantesMasSeries = [];
    $scope.tiposDeComprobantes = [];
    $scope.series = [];
    $scope.detalle = {};
    $scope.entidadesFinancieras = [];
    $scope.tiposDeTarjetas = [];
    $scope.listaDeEntidadesInternasConRolPuntoDeVenta = [];
    $scope.mensajeAdvertencia = [];
    $scope.cuotasdetalle = [];
    $scope.diaVencimiento = {
        Lista: [{ id: 1, nombre: "1 de cada mes" }, { id: 2, nombre: "2 de cada mes" }, { id: 3, nombre: "3 de cada mes" }, { id: 4, nombre: "4 de cada mes" }, { id: 5, nombre: "5 de cada mes" }, { id: 6, nombre: "6 de cada mes" }, { id: 7, nombre: "7 de cada mes" }, { id: 8, nombre: "8 de cada mes" }, { id: 9, nombre: "9 de cada mes" }, { id: 10, nombre: "10 de cada mes" },
        { id: 11, nombre: " 11 de cada mes" }, { id: 12, nombre: "12 de cada mes" }, { id: 13, nombre: "13 de cada mes" }, { id: 14, nombre: "14 de cada mes" }, { id: 15, nombre: "15 de cada mes" }, { id: 16, nombre: "16 de cada mes" }, { id: 17, nombre: "17 de cada mes" }, { id: 18, nombre: "18 de cada mes" }, { id: 19, nombre: "19 de cada mes" }, { id: 20, nombre: "20 de cada mes" },
        { id: 21, nombre: "21 de cada mes" }, { id: 22, nombre: "22 de cada mes" }, { id: 23, nombre: "23 de cada mes" }, { id: 24, nombre: "24 de cada mes" }, { id: 25, nombre: "25 de cada mes" }, { id: 26, nombre: "26 de cada mes" }, { id: 27, nombre: "27 de cada mes" }, { id: 28, nombre: "28 de cada mes" }]
    };
    $scope.conceptoBasicoSeleccionado = {};

    $scope.inicializar = function () {
        $scope.cargarParametros();
        $scope.limpiarRegistro();
        $scope.cargarColeccionesAsync();
        $scope.cargarColeccionesSync().then(function (resultado_) {
            $scope.establecerDatosPorDefecto();

        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
    }

    $scope.cargarParametros = function () {
        $scope.idMedioDePagoEfectivo = idMedioDePagoEfectivo;
        $scope.aplicaLeyAmazonia = aplicaLeyAmazonia;
        $scope.idClienteGenerico = idClienteGenerico;
        $scope.ventasSujetasADisponibilidadStock = ventasSujetasADisponibilidadStock;
        $scope.idTipoDocumentoCuandoClienteEsGenerico = idTipoDocumentoCuandoClienteEsGenerico;
        $scope.idTipoDocumentoPorDefectoParaVenta = idTipoDocumentoPorDefectoParaVenta;
        $scope.idTipoActorPersonaJuridica = idTipoActorPersonaJuridica;
        $scope.mostrarAliasDeClienteGenerico = mostrarAliasDeClienteGenerico;
        $scope.precioUnitarioCalculadoVenta = precioUnitarioCalculadoVenta;
        $scope.tasaIGV = tasaIGV;
        $scope.mostrarDetalleUnificado = mostrarDetalleUnificado;
        $scope.checketDetalleUnificado = checketDetalleUnificado;
        $scope.valorDetalleUnificado = valorDetalleUnificado;
        $scope.aplicarCantidadPorDefectoEnVentas = aplicarCantidadPorDefectoEnVentas;
        $scope.cantidadPorDefectoEnVentas = cantidadPorDefectoEnVentas;
        $scope.idTipoPersonaSeleccionadaPorDefecto = idTipoPersonaSeleccionadaPorDefecto;
        $scope.idTipoDocumentoSeleccionadaConTipoPersonaNatural = idTipoDocumentoSeleccionadaConTipoPersonaNatural;
        $scope.idTipoDocumentoSeleccionadaConTipoPersonaJuridica = idTipoDocumentoSeleccionadaConTipoPersonaJuridica;
        $scope.precioUnitarioIngresadoVenta = precioUnitarioIngresadoVenta;
        $scope.idTipoDocumentoIdentidadDni = idTipoDocumentoIdentidadDni;
        $scope.idTipoDocumentoIdentidadRuc = idTipoDocumentoIdentidadRuc;
        $scope.idDetalleMaestroCatalogoDocumentoBoleta = idDetalleMaestroCatalogoDocumentoBoleta;
        $scope.montoMaximoAVenderCuandoClienteNoEstaIdenticicado = montoMaximoAVenderCuandoClienteNoEstaIdenticicado;
        $scope.permitirVentaAlCredito = permitirVentaAlCredito;
        $scope.permitirVentaConFechaPasada = permitirVentaConFechaPasada;
        $scope.fechaActual = fechaActual;
        $scope.permitirRegistroFlete = permitirRegistroFlete;
        $scope.modoIngresoCodigoBarra = modoIngresoCodigoBarra;
        $scope.cursorPorDefectoCodigoBarra = cursorPorDefectoCodigoBarra;
        $scope.venderConBoleta = false;
        $scope.permitirRegistroDeGuiasDeRemision = permitirRegistroDeGuiasDeRemision;
        $scope.costoUnitarioPorBolsaDePlastico = costoUnitarioPorBolsaDePlastico;
        $scope.permitirRegistroNumeroBolsaDePlastico = permitirRegistroNumeroBolsaDePlastico;
        $scope.permitirRegistroDeLoteEnDetalleDeVenta = permitirRegistroDeLoteEnDetalleDeVenta;
        $scope.idConceptoBasicoBolsaPlastica = idConceptoBasicoBolsaPlastica;
        $scope.idTarifaSeleccionadoPorDefecto = idTarifaSeleccionadoPorDefecto;
        $scope.permitirIngresarCantidad = permitirIngresarCantidad;
        $scope.permitirIngresarPrecioUnitario = permitirIngresarPrecioUnitario;
        $scope.permitirIngresarImporte = permitirIngresarImporte;
        $scope.ingresarCantidadCalcularPrecioUnitario = ingresarCantidadCalcularPrecioUnitario;
        $scope.ingresarPrecioUnitarioCalcularImporte = ingresarPrecioUnitarioCalcularImporte;
        $scope.ingresarImporteCalcularCantidad = ingresarImporteCalcularCantidad;
    }

    $scope.cargarColeccionesAsync = function () {
        $scope.obtenerConceptos();
        $scope.obtenerEntidadesFinancieras();
        $scope.obtenerOperadoresDeTarjeta();
        $scope.obtenerMediosDePago();
    }

    $scope.cargarColeccionesSync = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        try {
            $scope.obtenerClientes().then(function (resultado_) {
                $scope.obtenerTiposDeComprobante().then(function (resultado_) {
                    defered.resolve();
                }, function (error) {
                    $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
                });
            }, function (error) {
                $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
            });
        } catch (e) {
            defered.reject(e);
        }
        return promise;
    }

    $scope.establecerDatosPorDefecto = function () {
        $scope.seleccionarClienteGenerico();
        $scope.asignarComprobantePorDefecto($scope.venta.Cliente);
        $scope.venta.Observacion = 'NINGUNA';
        if (!$scope.aplicaLeyAmazonia) { $scope.venta.GrabaIgv = true; }
        $scope.mostrarDetalleUnificado == true ? ($scope.checketDetalleUnificado == true ? $scope.venta.DetalleUnificado = true : $scope.venta.DetalleUnificado = false) : $scope.venta.DetalleUnificado = false;
        $scope.focusNext($scope.cursorPorDefectoCodigoBarra == 1 ? 'idCodigoBarraConcepto' : 'idCodigoBarraBalanza');
    }

    $scope.limpiarRegistro = function () {
        $scope.venta = {
            //TipoDeComprobante: {},
            Detalles: [], IdMedioDePago: idMedioDePagoEfectivo, GrabaIgv: false, EsVentaPasada: false, EsVentaACredito: false, EsCreditoRapido: {}, Cuotas: [], Inicial: 0, PagarInicialAlConfimar: false,
            Total: 0, Flete: 0, NumeroBolsasDePlastico: 0, Icbper: 0
        };
        $scope.venta.HaySalidaDeMercaderia = false;
        $scope.venta.SalidasDeMercaderia = [];
        $scope.rucValidado = true;
        $scope.logicaValida = false;
        $scope.esVentaCorporativa = false;
        $scope.serieSeleccionaesEsAutonumerica = false;
        $scope.financiamientoRealizado = false;
        $scope.hayLoteDuplicado = false;
        $scope.mensajeAdvertencia = [];
        $scope.conceptoBasicoSeleccionado = {};
        $scope.productos = {};
        $scope.venta.Observacion = 'NINGUNA';
        $scope.montoTotal = 0;
        //$scope.valorVenta = 0;
    }

    $scope.obtenerClientes = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        ventaService.obtenerClientes({}).success(function (data) {
            $scope.clientes = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.obtenerTiposDeComprobante = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        ventaService.obtenerTiposDeComprobanteParaVenta({}).success(function (data) {
            $scope.selectorTiposDeComprobantesMasSeries = data;
            $scope.tiposDeComprobantes = [];
            for (var i = 0; i < $scope.selectorTiposDeComprobantesMasSeries.length; i++) {
                $scope.tiposDeComprobantes.push($scope.selectorTiposDeComprobantesMasSeries[i]);
            }
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    //#endregion

    //#region Obtencion de Origenes de Datos

    $scope.obtenerConceptos = function () {
        maestroService.obtenerConceptos({}).success(function (data) {
            $scope.conceptos = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.obtenerProductos = function (idConceptoBasico) {
        conceptoService.obtenerConceptosDeNegociosComercialesParaVenta({ idConceptoBasico }).success(function (data) {
            $scope.productos = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.obtenerMediosDePago = function () {
        ventaService.obtenerMediosDePagoVenta({}).success(function (data) {
            $scope.mediosDePago = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.ver = function (medioDepago) {
        $scope.venta.MontoRecibido = "";
    }

    $scope.obtenerEntidadesFinancieras = function () {
        maestroService.obtenerEntidadesBancarias({}).success(function (data) {
            $scope.entidadesFinancieras = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.obtenerOperadoresDeTarjeta = function () {
        maestroService.obtenerOperadoresDeTarjeta({}).success(function (data) {
            $scope.tiposDeTarjeta = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    //#endregion

    //#region Acciones 
    $scope.seleccionarClienteGenerico = function () {
        for (var i = 0; i < $scope.clientes.length; i++) {
            if ($scope.clientes[i].Id == $scope.idClienteGenerico) {
                $scope.venta.Cliente = $scope.clientes[i];
                break;
            }
        }
        $timeout(function () { $('#comboCliente').trigger("change"); }, 100);

    }

    $scope.cargarSeries = function (tipoComprobante) {
        if (tipoComprobante != null) {
            $scope.series = angular.copy(tipoComprobante.Series);
        }
    }

    $scope.verificarAutonumerable = function (serieSeleccionada) {
        $scope.serieSeleccionaesEsAutonumerica = serieSeleccionada.EsAutonumerica;
    }

    $scope.cargarSeriesYValidarClienteYRucIntegrado = function (tipoComprobante) {
        $scope.cargarSeries(tipoComprobante);
        $scope.validarClienteYRucIntegrado($scope.venta.Cliente);
        if (tipoComprobante.Series.length > 0) {
            $scope.verificarAutonumerable(tipoComprobante.Series[0]);
        }
    }

    $scope.cargarSeriesYValidarClienteYRucPuntoVenta = function (tipoComprobante) {
        $scope.cargarSeries(tipoComprobante);
        $scope.validarClienteYRucPuntoVenta($scope.venta.Cliente);
        if (tipoComprobante.Series.length > 0) {
            $scope.verificarAutonumerable(tipoComprobante.Series[0]);
        }
    }

    $scope.seleccionarComprobante = function (idTipoDocumentoASeleccionar) {
        var serieSeleccionada = null;
        for (var i = 0; i < $scope.tiposDeComprobantes.length; i++) {
            if ($scope.tiposDeComprobantes[i].TipoComprobante.Id == idTipoDocumentoASeleccionar) {
                $scope.venta.TipoDeComprobante = $scope.tiposDeComprobantes[i];
                $scope.serieSeleccionada = $scope.venta.TipoDeComprobante.Series[0];
                break;
            }
        }
        $scope.cargarSeries($scope.venta.TipoDeComprobante);
        if (serieSeleccionada != null) {
            $scope.verificarAutonumerable($scope.serieSeleccionada);
        }
    }

    $scope.asignarComprobantePorDefecto = function (cliente) {
        if ($scope.venta.Cliente.Id == $scope.idClienteGenerico) {
            $scope.seleccionarComprobante(idTipoDocumentoCuandoClienteEsGenerico);
        }
        else {
            $scope.seleccionarComprobante(idTipoDocumentoPorDefectoParaVenta);
        }
        //en caso se deba seleccionar automaticamente el tipo de comprobante Boleta de Venta
        if ($scope.venderConBoleta) {
            $scope.seleccionarComprobante(idDetalleMaestroCatalogoDocumentoBoleta);
        }

        $timeout(function () {
            $('.tipoDocumento').trigger("change");
        }, 100);
    }

    $scope.actualizarClientesIngresadosYEditados = function (actualizacionCliente, id, numeroDocumento, razonSocial) {
        $scope.nuevoCliente = { Id: id, RazonSocial: razonSocial, NumeroDocumentoIdentidad: numeroDocumento };
        if (!actualizacionCliente) {
            $scope.clientes.push($scope.nuevoCliente);
            $scope.venta.Cliente = angular.copy($scope.nuevoCliente);
            //$scope.venta.Cliente = $scope.nuevoCliente;
        } else {
            $scope.clientes[document.getElementById("comboCliente").selectedIndex] = $scope.nuevoCliente;
        }
        $scope.validarClienteYRucIntegrado($scope.venta.Cliente);
    }

    $scope.focusNext = function (id) {
        $timeout(function () {
            $('#' + id).trigger("focus");
        }, 100);
    }

    $scope.focusSelectNext = function (id) {
        $timeout(function () {
            $('#' + id).trigger("focus");
        }, 100);
        $timeout(function () {
            $('#' + id).trigger("select");
        }, 100);
    }

    $scope.go = function (path) {
        $window.location.href = path;
    };

    $scope.buscarConceptoPorCodigoDeBarra = function () {
        conceptoService.obtenerConceptosDeNegociosComercialesParaVentaPorCodigoBarra({ codigoBarra: $scope.venta.CodigoBarraABuscar }).success(function (data) {
            if (data.data == 0) {//si data es igual a 0 no existe el producto o no tiene precio
                alert(data.data_description);
            }
            else {
                $scope.detalle.Producto = data;
                $scope.agregarDetalle();
                $scope.ValidarModoIntegrado();

            }
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.checkIfEnterKeyWasPressedPuntoVenta = function ($event) {
        var keyCode = $event.which || $event.keyCode;
        if (keyCode == 13) {
            ventaService.obtenerMercaderiaPorCodigoBarra({ codigoBarra: $scope.venta.CodigoBarraABuscar }).success(function (data) {
                if (data.data == 0) {//Si data es igual a 0 no existe el producto o no tiene registrado el precio
                    alert(data.data_description);
                }
                else {
                    $scope.detalle.Producto = data;
                    $scope.venta.NombreConceptoBuscadoCodigoBarra = $scope.detalle.Producto.Nombre;
                    $scope.agregarDetalle();
                    $scope.ValidarPuntoVenta();
                }
            }).error(function (data) {
                $scope.messageError(data.error);
            });
        }
    }

    $scope.buscarConceptoPorCodigoDeBarraBalanza2 = function () {//BorrarEstoDeVerde

        let codigoConcepto = $scope.venta.CodigoBarraBalanza.substring(2, 6);
        let cantidad = parseFloat($scope.venta.CodigoBarraBalanza.substring(12, 17)) / 1000;
        let importe = parseFloat($scope.venta.CodigoBarraBalanza.substring(6, 12)) / 100;

        ventaService.obtenerMercaderiaPorCodigoBarra({ codigoBarra: parseInt(codigoConcepto) }).success(function (data) {
            if (data.data == 0) {//si data es igual a 0 no existe el producto o no tiene precio
                alert(data.data_description);
            }
            else {
                $scope.detalle.Producto = data;
                $scope.detalle.Cantidad = Math.round((cantidad) * 100) / 100;
                $scope.detalle.Importe = Math.round((importe) * 100) / 100;
                $scope.detalle.PrecioUnitario = Math.round(($scope.detalle.Importe / $scope.detalle.Cantidad) * 100) / 100;
                $scope.detalle.PrecioCalculadoVenta = true;
                if ($scope.venta.GrabaIgv || !$scope.aplicaLeyAmazonia) {
                    $scope.detalle.Igv = Math.round((parseFloat($scope.detalle.Importe - ($scope.detalle.Importe / (1 + $scope.tasaIGV)))) * 100) / 100;
                } else {
                    $scope.detalle.Igv = 0;
                }
                $scope.detalle.Descuento = 0;
                $scope.detalle.VersionFila = $scope.detalle.Producto.VersionFila;
                $scope.detalle.IdPrecioUnitario = $scope.detalle.Producto.Precios[0].Id;
                $scope.venta.Detalles.unshift($scope.detalle);
                $scope.detalle = {};
                var $index = $scope.venta.Detalles.length - 1;
            }
            $scope.ValidarModoIntegrado();
            $scope.calcularTotal($scope.venta.Detalles);
            $scope.venta.CodigoBarraBalanza = "";
            $timeout(function () { $('.tarifa').trigger("change"); }, 100);
            $timeout(function () { $('#radio-0').trigger("focus"); }, 100);
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    //#endregion

    //#region Calculos y Guardado

    $scope.sumarStock = function (complementos) {
        var sum = 0;
        for (var i = 0; i < complementos.length; i++) {
            sum += complementos[i].Stock;
        }
        return sum;
    }
    $scope.agregarDetalle = function (esBusquedaPorCodigoBarra) {
        $scope.manejarAgregarDetalle(esBusquedaPorCodigoBarra);
    }

    $scope.resolverDetalle = function () {
        var validar = false;
        var index = 0;

        //Asignar el stock, lotes,caracteristicas propias,
        //Establecer el complemneto por defecto
        if ($scope.detalle.Producto.Complementos.length > 0) {
            if ($scope.permitirRegistroDeLoteEnDetalleDeVenta) {
                $scope.detalle.Lote = $scope.detalle.Producto.Complementos[0].Lote;
                $scope.detalle.Producto.Stock = $scope.detalle.Producto.Complementos[0].Stock;
                $scope.detalle.Producto.CaracteristicasPropias = $scope.detalle.Producto.Complementos[0].CaracteristicasPropias;
            } else {
                $scope.detalle.Producto.Stock = $scope.sumarStock($scope.detalle.Producto.Complementos);
                $scope.detalle.Producto.Complementos = $scope.detalle.Producto.Complementos;
            }
            $scope.detalle.Producto.CaracteristicasPropias = $scope.detalle.Producto.Complementos[0].CaracteristicasPropias;
        }
        //Verificamos el stock del producto, verificando la disponibilidad de stock
        if ($scope.detalle.Producto.EsBien && ventasSujetasADisponibilidadStock && (!$scope.detalle.Producto.Stock || $scope.detalle.Producto.Stock <= 0)) {
            SweetAlert.warning("Advertencia", "El Stock de " + $scope.detalle.Producto.Nombre + " es " + "0");
        } else {
            if ($scope.venta.Detalles.length > 0) {
                for (var i = 0; i < $scope.venta.Detalles.length; i++) {
                    if ($scope.detalle.Producto.Id == $scope.venta.Detalles[i].Producto.Id) {
                        validar = true;
                        index = i;
                        break;
                    }
                }
            }
            if (validar && !$scope.permitirRegistroDeLoteEnDetalleDeVenta) {
                $scope.agregarCantidad($scope.venta.Detalles, index);
                $scope.detalle = {};
                $timeout(function () { $('#cantidad-' + index).trigger("focus"); }, 100);
            }
            else {
                if ($scope.detalle.Producto.EsBien) {
                    $scope.detalle.Cantidad = $scope.aplicarCantidadPorDefectoEnVentas ? ($scope.detalle.Producto.Stock > parseInt($scope.cantidadPorDefectoEnVentas) || !ventasSujetasADisponibilidadStock) ? parseInt($scope.cantidadPorDefectoEnVentas) : $scope.detalle.Producto.Stock : "";
                } else {
                    $scope.detalle.Cantidad = $scope.aplicarCantidadPorDefectoEnVentas || !ventasSujetasADisponibilidadStock ? parseInt($scope.cantidadPorDefectoEnVentas) : 1;
                }

                if ($scope.detalle.Producto.IdConceptoBasico == $scope.idConceptoBasicoBolsaPlastica) {
                    $scope.venta.NumeroBolsasDePlastico += $scope.detalle.Cantidad;
                }
                var precio = Enumerable.from($scope.detalle.Producto.Precios).where("$.IdTarifa == '" + $scope.idTarifaSeleccionadoPorDefecto + "'").toArray()[0];

                $scope.detalle.PrecioUnitario = precio ? precio.Valor : $scope.detalle.Producto.Precios[0].Valor;
                $scope.detalle.PrecioCalculadoVenta = false;
                $scope.detalle.VersionFila = $scope.detalle.Producto.VersionFila;
                $scope.detalle.IdPrecioUnitario = $scope.detalle.Producto.Precios[0].Id;//Precio por defecto

                $timeout(function () { $('.tarifa').trigger("change"); }, 100);
                $timeout(function () { $('.lote').trigger("change"); }, 100);


                $scope.detalle.Descuento = 0;
                $scope.detalle.Importe = Math.round((parseFloat($scope.detalle.PrecioUnitario * $scope.detalle.Cantidad) - $scope.detalle.Descuento) * 100) / 100;
                $scope.detalle.MascaraDeCalculo = '110';
                $scope.detalle.Igv = ($scope.venta.GrabaIgv || !$scope.aplicaLeyAmazonia) ? Math.round((parseFloat($scope.detalle.Importe - ($scope.detalle.Importe / (1 + $scope.tasaIGV)))) * 100) / 100 : 0;

                $scope.venta.Detalles.unshift($scope.detalle);
                $scope.detalle = {};
                $timeout(function () { $('#cantidad-0').trigger("focus"); }, 100);
            }
            $scope.calcularTotal($scope.venta.Detalles);
            //$scope.VerificarLoteDuplicado($scope.venta.Detalles.length - 1);
            $scope.ValidarModoIntegrado();
        }
    }

    $scope.manejarAgregarDetalle = function (esBusquedaPorCodigoBarra) {
        //Si la busqueda es por codigo de barra no tiene que llamar a obtener complementos
        if (esBusquedaPorCodigoBarra) {
            $scope.resolverDetalle();
        } else {
            $scope.obtenerComplementosConceptoDeNegocioComercialParaVenta().then(function (resultado_) {
                $scope.resolverDetalle();
            }, function (error) {
                $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
            });
        }
    }

    $scope.obtenerComplementosConceptoDeNegocioComercialParaVenta = function () {
        //Obtener idDetalle con lotes, stock, caracteristicas propias
        var defered = $q.defer();
        var promise = defered.promise;
        conceptoService.obtenerComplementosConceptoDeNegocioComercialParaVenta({ idConceptoNegocio: $scope.detalle.Producto.Id, esBien: $scope.detalle.Producto.EsBien }).success(function (data) {
            $scope.detalle.Producto.Complementos = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.ponerErrorEnTabla = function () {
        var elemento = document.getElementById("item-0");
        console.log(elemento);
        $('#' + "item-0").addClass("error-in-table");

    }



    $scope.actualizarComplementosDeProducto = function (index, lote) {
        //Actualizar stock, caracteristicas propias
        if ($scope.venta.Detalles[index].Producto.Complementos.length > 0 && $scope.permitirRegistroDeLoteEnDetalleDeVenta) {
            if (lote) {
                var complemento = Enumerable.from($scope.venta.Detalles[index].Producto.Complementos).where("$.Lote == '" + lote + "'").toArray()[0];
            } else {
                var complemento = Enumerable.from($scope.venta.Detalles[index].Producto.Complementos).where("$.NombreLote == 'SIN LOTE'").toArray()[0];
            }
            $scope.venta.Detalles[index].Producto.Stock = complemento.Stock;
            $scope.venta.Detalles[index].Producto.CaracteristicasPropias = angular.copy(complemento.CaracteristicasPropias);
            $scope.verificarStockDescuentoYCalcularDetalle($scope.venta.Detalles, index);
            $scope.calcularNumeroDeBolsasPlasticas($scope.venta.Detalles);
            $scope.calcularTotal($scope.venta.Detalles);
            $scope.ValidarModoIntegrado();
            //$scope.VerificarLoteDuplicado(index);
        }
    }

    $scope.VerificarLoteDuplicado = function (index) {
        var hayLoteDuplicado = false;
        for (var i = 0; i < $scope.venta.Detalles.length; i++) {
            if (i != index) {
                if ($scope.venta.Detalles[index].Lote == $scope.venta.Detalles[i].Lote) {
                    hayLoteDuplicado = true;
                    break;
                } else {
                    hayLoteDuplicado = false;
                }
            }
        }
        $scope.hayLoteDuplicado = hayLoteDuplicado;
        $scope.ValidarModoIntegrado();
    }



    //CLONAR DETALLES DE VENTA POR LOTE
    $scope.clonarDetalle = function (index) {
        var detalle = angular.copy($scope.venta.Detalles[index]);
        $scope.venta.Detalles.splice(index, 0, detalle);
        $timeout(function () { $('.tarifa').trigger("change"); }, 100);
        $timeout(function () { $('.lote').trigger("change"); }, 100);
        $scope.ValidarModoIntegrado();
        //$scope.VerificarLoteDuplicado(index);
    }

    $scope.agregarCantidad = function (detalles, index) {
        detalles[index].Cantidad++;
        detalles[index].Importe = Math.round((parseFloat(detalles[index].Cantidad * detalles[index].PrecioUnitario) - parseFloat(detalles[index].Descuento)) * 100) / 100;
        $scope.verificarStockDescuentoYCalcularDetalle(detalles, index);
        $scope.calcularNumeroDeBolsasPlasticas(detalles);
        $scope.calcularTotal(detalles);
    }

    $scope.quitarCantidad = function (detalles, index) {
        detalles[index].Cantidad--;
        if (detalles[index].Cantidad != 0) {
            detalles[index].Importe = Math.round((parseFloat(detalles[index].Cantidad * detalles[index].PrecioUnitario) - parseFloat(detalles[index].Descuento)) * 100) / 100;
            $scope.calcularNumeroDeBolsasPlasticas(detalles);
            $scope.calcularTotal($scope.venta.Detalles);
        } else {
            $scope.quitarDetalle(index);
        }
    }

    $scope.quitarDetalle = function (index) {
        if ($scope.venta.Detalles[index].Producto.IdConceptoBasico == $scope.idConceptoBasicoBolsaPlastica) {
            $scope.venta.NumeroBolsasDePlastico -= $scope.venta.Detalles[index].Cantidad;
        }
        $scope.venta.Detalles.splice(index, 1);
        $scope.calcularTotal($scope.venta.Detalles);
    }

    $scope.calcularNumeroDeBolsasPlasticas = function (detalles) {
        $scope.venta.NumeroBolsasDePlastico = 0;
        for (var i = 0; i < detalles.length; i++) {
            if (detalles[i].Producto.IdConceptoBasico == $scope.idConceptoBasicoBolsaPlastica) {
                $scope.venta.NumeroBolsasDePlastico += detalles[i].Cantidad;
            }
        }
    }

    $scope.calcularImporteDesdePrecio = function (detalles, index, ingresaPrecio) {
        var precioUnitario;
        if (!ingresaPrecio) {
            for (var i = 0; i < detalles[index].Producto.Precios.length; i++) {
                if (detalles[index].Producto.Precios[i].Id == detalles[index].IdPrecioUnitario) {
                    precioUnitario = detalles[index].Producto.Precios[i].Valor
                    detalles[index].PrecioUnitario = precioUnitario;
                    break;
                }
            }
        }
        else {
            detalles[index].PrecioUnitario = detalles[index].PrecioUnitario;
        }

    }

    $scope.calcularImporte = function (detalles, index, siIngresaPrecio) {
        var precioUnitario;
        if (!siIngresaPrecio) {
            for (var i = 0; i < detalles[index].Producto.Precios.length; i++) {
                if (detalles[index].Producto.Precios[i].Id == detalles[index].IdPrecioUnitario) {
                    precioUnitario = detalles[index].Producto.Precios[i].Valor;
                    detalles[index].PrecioUnitario = precioUnitario;
                    break;
                }
            }
        } else {
            detalles[index].PrecioUnitario = detalles[index].PrecioUnitario;
        }

        detalles[index].Importe = Math.round((parseFloat(detalles[index].Cantidad * detalles[index].PrecioUnitario) - parseFloat(detalles[index].Descuento)) * 100) / 100;
        if ($scope.venta.GrabaIgv || !$scope.aplicaLeyAmazonia) {
            detalles[index].Igv = Math.round((parseFloat(detalles[index].Importe - (detalles[index].Importe / (1 + $scope.tasaIGV)))) * 100) / 100;
        } else {
            detalles[index].Igv = 0;
        }
        $scope.calcularTotal(detalles);
    }

    $scope.calcularTotal = function (detalles) {
        if ($scope.venta.Flete > 0) {
            $scope.venta.Total = parseFloat($scope.venta.Flete);
            $scope.venta.Igv = ($scope.venta.GrabaIgv || !$scope.aplicaLeyAmazonia) ? (Math.round((parseFloat($scope.venta.Flete - ($scope.venta.Flete / (1 + $scope.tasaIGV)))) * 100) / 100) : 0;
            $scope.venta.SubTotal = parseFloat($scope.venta.Total) - parseFloat($scope.venta.Igv);
        } else {
            $scope.venta.SubTotal = 0;
            $scope.venta.Igv = 0;
            $scope.venta.Total = 0;
        }
        $scope.venta.Descuento = 0;
        $scope.venta.Icbper = Math.round(($scope.venta.NumeroBolsasDePlastico * $scope.costoUnitarioPorBolsaDePlastico) * 100) / 100;
        for (i = 0; i < detalles.length; i++) {
            $scope.venta.Total += parseFloat(detalles[i].Importe);
            $scope.venta.Descuento += parseFloat(detalles[i].Descuento);
            $scope.venta.Igv += parseFloat(detalles[i].Igv);
        }
        $scope.venta.SubTotal = parseFloat($scope.venta.Total - $scope.venta.Igv);
        $scope.venta.Total += $scope.venta.Icbper;
        $scope.ValidarModoIntegrado();
        if ($scope.venta.EsVentaACredito == true && $scope.financiamientoRealizado == true) {
            $("#reiniciar-financimiento").click();
        }
    }

    $scope.verificarStockDescuentoYCalcularDetalle = function (detalles, index) {
        if (detalles[index].EsBien && ventasSujetasADisponibilidadStock && detalles[index].Cantidad > detalles[index].Producto.Stock) {
            SweetAlert.warning("Advertencia", "El Stock de " + detalles[index].Producto.Nombre + " es " + detalles[index].Producto.Stock);
            detalles[index].Cantidad = detalles[index].Producto.Stock;
        }
        if (detalles[index].Producto.HayDescuento) {
            if (detalles[index].Producto.DescuentoPorcentaje) {
                if (detalles[index].Cantidad < detalles[index].Producto.CantidadMinimaDescuento) {
                    detalles[index].Descuento = 0;
                }
                if (detalles[index].Cantidad <= detalles[index].Producto.CantidadMaximaDescuento && detalles[index].Cantidad >= detalles[index].Producto.CantidadMinimaDescuento) {
                    detalles[index].Descuento = detalles[index].Cantidad * detalles[index].PrecioUnitario * (detalles[index].Producto.Descuento / 100);
                }
                if (detalles[index].Cantidad > detalles[index].Producto.CantidadMaximaDescuento) {
                    detalles[index].Descuento = detalles[index].Producto.CantidadMaximaDescuento * detalles[index].PrecioUnitario * (detalles[index].Producto.Descuento / 100);
                }
            }
            else {
                if (detalles[index].Cantidad < detalles[index].Producto.CantidadMinimaDescuento) {
                    detalles[index].Descuento = 0;
                }
                if (detalles[index].Cantidad <= detalles[index].Producto.CantidadMaximaDescuento && detalles[index].Cantidad >= detalles[index].Producto.CantidadMinimaDescuento) {
                    detalles[index].Descuento = detalles[index].Cantidad * detalles[index].Producto.Descuento;
                }
                if (detalles[index].Cantidad > detalles[index].Producto.CantidadMaximaDescuento) {
                    detalles[index].Descuento = detalles[index].Producto.CantidadMaximaDescuento * detalles[index].Producto.Descuento;
                }
            }
        }
        $scope.calcularNumeroDeBolsasPlasticas(detalles);
        $scope.calcularImporte(detalles, index, true);
    }

    $scope.cambioDeGrabarIgv = function (detalles) {
        for (i = 0; i < detalles.length; i++) {
            if ($scope.venta.GrabaIgv || !$scope.aplicaLeyAmazonia) {
                detalles[i].Igv = Math.round((parseFloat(detalles[i].Importe - (detalles[i].Importe / (1 + $scope.tasaIGV)))) * 100) / 100;
            } else {
                detalles[i].Igv = 0;
            }
        }
        $scope.calcularTotal(detalles);
    }

    $scope.calcularValoresIngresadoImporte = function (detalles, index) {
        detalles[index].PrecioUnitario = Math.round((detalles[index].Importe / detalles[index].Cantidad) * 100) / 100;
        detalles[index].PrecioCalculadoVenta = true;
        if ($scope.venta.GrabaIgv || !$scope.aplicaLeyAmazonia) {
            detalles[index].Igv = Math.round((parseFloat(detalles[index].Importe - (detalles[index].Importe / (1 + $scope.tasaIGV)))) * 100) / 100;
        } else {
            detalles[index].Igv = 0;
        }
        $scope.calcularTotal(detalles);
    }

    $scope.guardar = function () {
        //Si es venta modo caja
        if ($scope.esVentaPorMostradorIntegradoModoCaja) {
            $scope.guardarVentaPorMostradorIntegradoModoCaja();
        } else {
            ventaService.guardarVenta({ venta: $scope.venta }).success(function (data) {
                $scope.venderConBoleta = data.VenderConBoleta;
                $scope.limpiarRegistro();
                $scope.establecerDatosPorDefecto();
                SweetAlert.success("Correcto", data.result_description);
                jsWebClientPrint.print('idVenta=' + data.data);
            }).error(function (data, status) {
                SweetAlert.error("Ocurrio un Problema", data.error);
            });
        }

    }

    $scope.registarVenta = function () {
        ventaService.guardarVentaCorporativa({ venta: $scope.venta }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.limpiarRegistro();
            $('#modal-registro-venta-corporativa').modal('hide');
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema", data.error);
        });
    }

    $scope.editarRegistro = function (id) {
        ventaService.cargarVentaCorporativaYDetallesAEditar({ idOrdenVenta: id }).success(function (data) {
            $scope.inicializar();
            $scope.venta = data;
            var date = $scope.modelo.FechaRegistro.slice($scope.modelo.FechaRegistro.indexOf("(") + 1, $scope.modelo.FechaRegistro.indexOf(")"));
            $("#fechaRegistro").datepicker('setDate', $scope.formatDate(new Date(+date), "ES"));
            $scope.cambioDeExoneradoIgv($scope.modelo.Detalles);
            $scope.cargarFinanciamiento();
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema", data.error);
        });
    }

    $scope.guardarEnModal = function () {
        ventaService.guardarVenta({ venta: $scope.venta, esVentaPasada: false }).success(function (data) {
            $scope.messageSuccess(data.result_description);
            $scope.venta = { Detalles: [], IdMedioDePago: idMedioDePagoEfectivo };
            jsWebClientPrint.print('idVenta=' + data.data);
            $('#modal-registro').modal('hide');
            $scope.listarBandeja($scope.bandeja.FechaInicio, $scope.bandeja.FechaFinal);
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.guardarVentaDesdePuntoDeVenta = function () {
        ventaService.registrarOrdenVenta({ venta: $scope.venta }).success(function (data) {
            $scope.messageSuccess(data.result_description);
            $scope.venta = { Detalles: [], IdMedioDePago: idMedioDePagoEfectivo };
            jsWebClientPrint.print('idVenta=' + data.data);
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    //#endregion

    //#region Validacion de Ventas

    $scope.validarClienteYRucIntegrado = function (cliente) {
        if ($scope.venta.TipoDeComprobante != null && cliente != null) {
            clienteService.validarClienteYRuc({ idCliente: cliente.Id, idTipoDocumento: $scope.venta.TipoDeComprobante.TipoComprobante.Id }).success(function (data) {
                $scope.dataResult = data;
                if ($scope.dataResult.respuesta == 1) {//1 el valor que nos devuelve si no encuentre que el cliente tenga ruc 
                    //SweetAlert.warning("Adventencia", " El cliente no cuenta con ruc");
                    $scope.rucValidado = false;
                } else {
                    $scope.rucValidado = true;
                }
                $scope.ValidarModoIntegrado();
            });
        } else {
            $scope.ValidarModoIntegrado();
        }
    }

    $scope.validarClienteYRucPuntoVenta = function (cliente) {
        if ($scope.venta.TipoDeComprobante != null && cliente != null) {
            clienteService.validarClienteYRuc({ idCliente: cliente.Id, idTipoDocumento: $scope.venta.TipoDeComprobante.TipoComprobante.Id }).success(function (data) {
                $scope.dataResult = data;
                if ($scope.dataResult.respuesta == 1) {//1 el valor que nos devuelve si no encuentre que el cliente tenga ruc 
                    SweetAlert.warning("Advertencia", "El cliente no cuenta con RUC")
                    //alert("El cliente no cuenta con RUC");
                    $scope.rucValidado = false;
                } else {
                    $scope.rucValidado = true;
                }
                $scope.ValidarPuntoVenta();
            });
        } else {
            $scope.ValidarPuntoVenta();
        }
    }

    $scope.ValidarModoIntegrado = function () {
        var temp = 0;
        $scope.mensajeAdvertencia = [];
        if ($scope.ValidacionPagoOrdenVenta($scope.ValidacionOrdenVenta(temp)) == 0) {
            $scope.logicaValida = true;
        }
        else {
            $scope.logicaValida = false;
        }
    }

    $scope.ValidarPuntoVenta = function () {
        var temp = 0;
        $scope.mensajeAdvertencia = [];
        if ($scope.ValidacionOrdenVenta(temp) == 0) {
            $scope.logicaValida = true;
        }
        else {
            $scope.logicaValida = false;
        }
    }



    $scope.ValidacionOrdenVenta = function (temporal) {

        var temp = temporal;
        var hayCantidad0 = false;
        var esCantidadNegativo = false;
        var esCantidadMayorAStock = false;
        var hayLoteDuplicado = false;

        if ($scope.venta.Detalles.length <= 0) {
            $scope.mensajeAdvertencia[temp] = "Es necesario seleccionar al menos un producto.";
            temp++;
        } else {
            for (var i = 0; i < $scope.venta.Detalles.length; i++) {
                hayCantidad0 = hayCantidad0 || $scope.venta.Detalles[i].Cantidad == 0 ? true : false;
                esCantidadNegativo = esCantidadNegativo || $scope.venta.Detalles[i].Cantidad == 0 ? true : false;
                esCantidadMayorAStock = esCantidadMayorAStock || $scope.venta.Detalles[i].Producto.EsBien && $scope.ventasSujetasADisponibilidadStock && $scope.venta.Detalles[i].Cantidad > $scope.venta.Detalles[i].Producto.Stock ? true : false;
            }

            var detallesConImportesInvalidados = $scope.venta.Detalles.filter(detalle => detalle.Importe < 0);
            if ((Array.isArray(detallesConImportesInvalidados) && detallesConImportesInvalidados.length)) {
                var esImporteInvalido = true;
            }

            var detallesConCantidadesNegativas = $scope.venta.Detalles.filter(detalle => detalle.Cantidad === undefined);
            if ((Array.isArray(detallesConCantidadesNegativas) && detallesConCantidadesNegativas.length)) {
                esCantidadNegativo = true;
            }

            if ($scope.venta.TipoDeComprobante.TipoComprobante.Id == $scope.idDetalleMaestroCatalogoDocumentoBoleta && $scope.venta.Cliente.Id == $scope.idClienteGenerico && $scope.venta.Total >= $scope.montoMaximoAVenderCuandoClienteNoEstaIdenticicado) {
                $scope.mensajeAdvertencia[temp] = "Es nesesario identificar al cliente, el total es mayor a S/.700";
                temp++;
            }
        } if ($scope.venta.Cliente == null) {
            $scope.mensajeAdvertencia[temp] = "Es necesario seleccionar un cliente.";
            temp++;
        } if ($scope.venta.TipoDeComprobante == undefined) {
            $scope.mensajeAdvertencia[temp] = "Es necesario seleccionar un documento.";
            temp++;
        } else {
            if (!$scope.serieSeleccionaesEsAutonumerica) {
                if ($scope.venta.TipoDeComprobante.NumeroIngresado == undefined || $scope.venta.TipoDeComprobante.NumeroIngresado == "") {
                    $scope.mensajeAdvertencia[temp] = "Es necesario ingresar el numero de comprobante";
                    temp++;
                }
            }
        }
        if ($scope.permitirRegistroDeLoteEnDetalleDeVenta) {
            for (var j = 0; j < $scope.venta.Detalles.length; j++) {
                for (var i = 0; i < $scope.venta.Detalles.length; i++) {
                    if (i != j) {
                        if ($scope.venta.Detalles[j].Lote == $scope.venta.Detalles[i].Lote) {
                            hayLoteDuplicado = true;
                            break;
                        } else {
                            hayLoteDuplicado = false;
                        }
                    }
                }
                if (hayLoteDuplicado) {
                    $scope.mensajeAdvertencia[temp] = "Es necesario que no haya conceptos con lotes duplicados";
                    temp++;
                    break;
                }
            }
        }

        if ($scope.venta.Total == 0 || $scope.venta.Total == '0' || $scope.venta.Total == '0.00') {
            $scope.mensajeAdvertencia[temp] = "Es necesario que el importe total sea mayor a 0.00";
            temp++;
        }

        if (esImporteInvalido) {
            $scope.mensajeAdvertencia[temp] = "Es necesario que el importe total no sea menor a 0.00";
            temp++;
        }


        if (hayCantidad0) {
            $scope.mensajeAdvertencia[temp] = "Es necesario la cantidad sea mayor a 0";
            temp++;
        }
        if (esCantidadNegativo) {
            $scope.mensajeAdvertencia[temp] = "Es necesario que la cantidad no sea negativo";
            temp++;
        }
        if (esCantidadMayorAStock) {
            $scope.mensajeAdvertencia[temp] = "Es necesario que la cantidad no sea mayor al stock";
            temp++;
        }
        if ($scope.venta.TipoDeComprobante != null) {
            if ($scope.venta.TipoDeComprobante.SerieSeleccionada == 0 && $scope.venta.TipoDeComprobante.MostrarSelectorSerie && $scope.venta.TipoDeComprobante.EsPropio) {
                $scope.mensajeAdvertencia[temp] = "Es necesario seleccionar un serie.";
                temp++;
            }
        } if (!$scope.rucValidado) {
            $scope.mensajeAdvertencia[temp] = "Es necesario que el cliente tenga Ruc.";
            temp++;
        } if ($scope.venta.EsVentaPasada) {
            if ($scope.venta.FechaRegistro == undefined || $scope.venta.FechaRegistro == "") {
                $scope.mensajeAdvertencia[temp] = "Es necesario ingresar la fecha de venta";
                temp++;
            } else {
                var fr = new Date($scope.venta.FechaRegistro.split("/")[1] + '-' + $scope.venta.FechaRegistro.split("/")[0] + '-' + $scope.venta.FechaRegistro.split("/")[2]);
                if (new Date($scope.venta.FechaRegistro.split("/")[1] + '-' + $scope.venta.FechaRegistro.split("/")[0] + '-' + $scope.venta.FechaRegistro.split("/")[2]) > new Date($scope.fechaActual.split("/")[1] + '-' + $scope.fechaActual.split("/")[0] + '-' + $scope.fechaActual.split("/")[2])) {
                    $scope.mensajeAdvertencia[temp] = "Es necesario que la fecha sea menor o igual a la fecha actual";
                    temp++;
                }
            }
        } if ($scope.venta.HaySalidaDeMercaderia) {
            for (var i = 0; i < $scope.venta.Detalles.length; i++) {
                var producto = Enumerable.from($scope.detallesDeGuiaDeRemision).where("$.IdProducto == '" + $scope.venta.Detalles[i].Producto.Id + "'").toArray()[0];
                if (producto == null) {
                    $scope.mensajeAdvertencia[temp] = "Es necesario que todos los productos esten con registro de salida de mercaderia";
                    temp++;
                } else {
                    if (producto.RecibidoEntregado != $scope.venta.Detalles[i].Cantidad) {
                        $scope.mensajeAdvertencia[temp] = "Es necesario que toda la cantidad de productos esten registrados en la salida de mercaderia";
                        temp++;
                    }
                }
            }
        }
        if ($scope.esVentaPorMostradorIntegradoModoCaja) {
            if ($scope.venta.PuntoDeVenta == undefined) {
                $scope.mensajeAdvertencia[temp] = "Es necesario seleccionar un punto de venta.";
                temp++;
            }
            if ($scope.venta.Vendedor == undefined) {
                $scope.mensajeAdvertencia[temp] = "Es necesario seleccionar un vendedor.";
                temp++;
            }
            if ($scope.permitirRegistroAlmacenEnVentaPorMostradorIntegradoModoCaja) {
                if ($scope.venta.Almacen == undefined) {
                    $scope.mensajeAdvertencia[temp] = "Es necesario seleccionar un almacen.";
                    temp++;
                }
                if ($scope.venta.Almacenero == undefined) {
                    $scope.mensajeAdvertencia[temp] = "Es necesario seleccionar un almacenero.";
                    temp++;
                }
            }
        }
        return temp;
    }

    $scope.ValidacionPagoOrdenVenta = function (temporal) {
        var temp = temporal;

        if ($scope.venta.IdMedioDePago == idMedioDePagoEfectivo) {
            if ($scope.venta.MontoRecibido != null && $scope.venta.MontoRecibido != 0) {
                if ($scope.venta.MontoRecibido < $scope.venta.Total) {
                    $scope.mensajeAdvertencia[temp] = "Es nesesario que el monto a recibir debe de ser mayor al importe total de la venta.";
                    temp++;
                }
            }
        } else {
            if ($scope.venta.EntidadFinanciera == null) {
                $scope.mensajeAdvertencia[temp] = "Es necesario seleccionar una entidad bancaria.";
                temp++;
            } if ($scope.venta.TipoTarjeta == null) {
                $scope.mensajeAdvertencia[temp] = "Es necesario seleccionar el tipo de tarjeta.";
                temp++;
            } if ($scope.venta.informacion == null) {
                $scope.mensajeAdvertencia[temp] = "Es necesario ingresar informacion del pago.";
                temp++;
            }
        }
        return temp;
    }

    //#endregion
    $scope.formatoAMPM = function (date) {

        var hours = date.getHours();
        var minutes = date.getMinutes();
        var ampm = hours >= 12 ? 'pm' : 'am';
        hours = hours % 12;
        hours = hours ? hours : 12; // the hour '0' should be '12'
        minutes = minutes < 10 ? '0' + minutes : minutes;
        var strTime = hours + ':' + minutes + ' ' + ampm;
        return strTime;
    }

    //#region Financiamiento

    $scope.iniciarFinanciamiento = function () {
        if ($scope.venta.EsVentaACredito == true) {
            $scope.venta.EsCreditoRapido = true;
        } else {
            $scope.venta.EsCreditoRapido = {};
        }
        $scope.limpiarCuotas();
    }

    $scope.financiamientoConfigurado = function () {
        $scope.financiamientoRealizado = false;
        $scope.venta.EsCreditoRapido = false;
        $scope.limpiarCuotas();
        $("#cuota-modal").click();
    }

    $scope.limpiarCuotas = function () {
        $scope.financiamientodetalle = { cuota: 1, inicial: 0, capital: $scope.venta.Total, total: $scope.venta.Total, interes: 0 };
        $scope.cuotasdetalle = [];
        $scope.montoTotal = $scope.venta.Total;
    }

    $scope.calcularTotalFinanciamiento = function () {
        $scope.financiamientodetalle.capital = $scope.financiamientodetalle.inicial > 0 || $scope.financiamientodetalle.inicial != "" ? $scope.venta.Total - $scope.financiamientodetalle.inicial : $scope.venta.Total;
        $scope.financiamientodetalle.total = $scope.financiamientodetalle.interes > 0 || $scope.financiamientodetalle.interes != "" ? $scope.financiamientodetalle.capital + ($scope.financiamientodetalle.capital * $scope.financiamientodetalle.interes / 100) : $scope.financiamientodetalle.capital;
        $scope.limpiarCronogramaDePagos();
    }

    $scope.limpiarCronogramaDePagos = function () {
        if ($scope.financiamientodetalle.cuota > 1) {
            $scope.financiamientodetalle.fechaRegistro = undefined;
        } else {
            if ($scope.financiamientodetalle.cuota == 1) {
                $scope.financiamientodetalle.diavencimiento = undefined;
            }
        }
        $scope.cuotasdetalle = [];
    }

    $scope.generarCuota = function () {
        $scope.cuotasdetalle = [];
        $scope.cuotadetalle = {};

        let temp = $scope.financiamientodetalle.cuota;
        let arrayDeFechaRegistro = $scope.fechaActual.split("/");
        let anioHora = arrayDeFechaRegistro[2].split(" ");
        let anio = anioHora[0];
        let mes = arrayDeFechaRegistro[1];
        let dia = $scope.financiamientodetalle.diavencimiento;
        let inicial = parseFloat($scope.financiamientodetalle.inicial);
        $scope.venta.Inicial = inicial;

        if (inicial !== $scope.venta.Total) {
            if (inicial > 0) {
                $scope.cuotadetalle = { CapitalCuota: $scope.financiamientodetalle.inicial, InteresCuota: 0.00, ImporteCuota: $scope.financiamientodetalle.inicial, FechaVencimiento: $scope.formatDate(new Date(), "ES"), EsCuotaInicial: true }
                $scope.cuotasdetalle.push($scope.cuotadetalle);
                $scope.cuotadetalle = {};
            }
            if (temp == 1) {
                $scope.cuotadetalle.CapitalCuota = $scope.formatNumber(parseFloat($scope.financiamientodetalle.capital) / parseFloat(temp), 2);
                $scope.cuotadetalle.InteresCuota = $scope.financiamientodetalle.interes > 0 || $scope.financiamientodetalle.interes != "" ? $scope.formatNumber(parseFloat($scope.financiamientodetalle.capital * $scope.financiamientodetalle.interes / 100) / parseFloat(temp), 2) : 0.00;
                $scope.cuotadetalle.ImporteCuota = $scope.formatNumber(parseFloat($scope.financiamientodetalle.total) / parseFloat(temp), 2);
                $scope.cuotadetalle.FechaVencimiento = $scope.financiamientodetalle.fechaRegistro;
                $scope.cuotadetalle.EsCuotaInicial = false;
                $scope.cuotasdetalle.push($scope.cuotadetalle);
                $scope.cuotadetalle = {};
            } else {
                for (let i = 0; i < temp; i++) {
                    $scope.cuotadetalle.CapitalCuota = $scope.formatNumber(parseFloat($scope.financiamientodetalle.capital) / parseFloat(temp), 2);
                    $scope.cuotadetalle.InteresCuota = $scope.financiamientodetalle.interes > 0 || $scope.financiamientodetalle.interes != "" ? $scope.formatNumber(parseFloat($scope.financiamientodetalle.capital * $scope.financiamientodetalle.interes / 100) / parseFloat(temp), 2) : 0.00;
                    $scope.cuotadetalle.ImporteCuota = $scope.formatNumber(parseFloat($scope.financiamientodetalle.total) / parseFloat(temp), 2);
                    if (mes == 13) {
                        mes = 1;
                        anio++;
                    }
                    if (mes == 2 && (dia == 28 || dia == 29)) {
                        $scope.cuotadetalle.FechaVencimiento = $scope.formatDate(new Date(anio, mes, 0), "ES");
                    } else if ((mes == 1 || mes == 3 || mes == 5 || mes == 7 || mes == 8 || mes == 10 || mes == 12) && dia == 31) {
                        $scope.cuotadetalle.FechaVencimiento = $scope.formatDate(new Date(anio, mes, 0), "ES");
                    } else {
                        $scope.cuotadetalle.FechaVencimiento = $scope.formatDate(new Date(anio, mes, dia), "ES");
                    }
                    $scope.cuotadetalle.EsCuotaInicial = false;
                    $scope.cuotasdetalle.push($scope.cuotadetalle);
                    $scope.cuotadetalle = {};
                    mes++;
                }
            }
        }
        $scope.venta.Cuotas = angular.copy($scope.cuotasdetalle);
        $scope.financiamientoRealizado = true;
    }

    $scope.deshabilitarBotonGenerar = function (fechaRegistro, diavencimiento) {
        return fechaRegistro === undefined && diavencimiento === undefined;
    }

    $scope.seleccionarEsCredito = function () {
        if ($scope.venta.EsVentaACredito != {}) {
            $scope.venta.EsVentaACredito = true;
        }
    }

    $scope.finalizarCredito = function () {
        $scope.venta.EsVentaACredito = false;
        $scope.venta.EsCreditoRapido = {};
    }

    //#endregion

    //-------------------------------------- VENTA CORPORATIVA  -------------------------------------------//

    $scope.iniciarBandeja = function () {
        $scope.fechaInicio = fechaInicio;
        $scope.fechaFin = fechaFin;
        $scope.listarBandeja();
    }

    $scope.listarBandeja = function () {
        ventaService.obtenerVentas({ desde: $scope.fechaInicio, hasta: $scope.fechaFin }).success(function (data) {
            $scope.ventasCorporativas = data;
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema", data.error);
        });
    }

    //#region CONTABILIZACION DE VENTA.

    $scope.listaDePeriodos = [];
    $scope.periodoSeleccionado = {};

    $scope.obtenerPeriodosParaContabilizacion = function () {
        librosElectronicosService.obtenerPeriodosParaContabilizacion({ idPeriodo: $scope.periodoSeleccionado.Id }).success(function (data) {
            $scope.listaDePeriodos = data;
        }).error(function (data) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }

    //#endregion

    //#region ANULACION INVALIDACION

    //////$scope.existe = function (item) {
    //////    return $scope.comprobanteSeleccionado.indexOf(item) > -1;
    //////}
    //////$scope.seleccionarComprobanteAnular = function (item) {
    //////    var idx = $scope.comprobanteSeleccionado.indexOf(item);
    //////    $scope.serieDocumentoSeleccionado = item.Numero;
    //////    if (idx > -1) {
    //////        $scope.EsAnulableConNotaInterna = false;
    //////        $scope.EsAnulableConNotaDeCredito = false;
    //////        $scope.comprobanteSeleccionado = [];
    //////    }
    //////    else {
    //////        $scope.EsAnulableConNotaInterna = item.EsAnulableConNotaInterna;
    //////        $scope.EsOrdenDeVenta = item.EsOrdenDeVenta;
    //////        $scope.EsAnulableConNotaDeCredito = item.EsAnulableConNotaDeCredito;
    //////        $scope.comprobanteSeleccionado = [];
    //////        $scope.comprobanteSeleccionado.push(item);
    //////        $scope.IdOrden = item.Id;
    //////    }
    //////};

    $scope.limpiarObservacion = function () {
        $scope.invalidacion = {};
    }
    $scope.invalidar = function () {

        ventaService.invalidarVenta({ idOrden: $scope.IdOrden, observacion: $scope.invalidacion.observacion }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $('#modal-anulacion-interna').modal('hide');
            $scope.EsAnulableConNotaInterna = false;
            $scope.EsAnulableConNotaDeCredito = false;
            $scope.comprobanteSeleccionado = [];
            $scope.inicio();
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema", data.error);
        });
    }
    $scope.invalidarAnulacionDeVenta = function () {
        ventaService.invalidarAnulacionDeVenta({ idOrden: $scope.IdOrden, observacion: $scope.invalidacion.observacion }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $('#modal-anulacion-interna').modal('hide');
            $scope.EsAnulableConNotaInterna = false;
            $scope.EsAnulableConNotaDeCredito = false;
            $scope.comprobanteSeleccionado = [];
            $scope.inicio();
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema", data.error);
        });
    }
    $scope.anular = function () {
        ventaService.anularVenta({ anulacion: $scope.anulacion }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $('#modal-anulacion-nota-credito').modal('hide');
            $scope.EsAnulableConNotaInterna = false;
            $scope.EsAnulableConNotaDeCredito = false;
            $scope.comprobanteSeleccionado = [];
            $scope.inicio();
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema", data.error);
        });
    }
    $scope.cargarDatosParaAnulacion = function () {
        $scope.anulacion = {};
        $scope.obtenerOrdenDeVentaParaAnulacion($scope.IdOrden);
        $scope.obtenerTiposDeComprobanteParaAnulacionVenta();
        $scope.obtenerMediosDePagoParaAnulacionVenta();
    }
    $scope.obtenerTiposDeComprobanteParaAnulacionVenta = function () {
        ventaService.obtenerTiposDeComprobanteParaAnulacionVenta({ serieComprobanteVenta: $scope.serieDocumentoSeleccionado }).success(function (data) {
            $scope.selectorTiposDeComprobantesMasSeries = data;
            $scope.tiposDeComprobantes = [];
            for (var i = 0; i < $scope.selectorTiposDeComprobantesMasSeries.length; i++) {
                $scope.tiposDeComprobantes.push($scope.selectorTiposDeComprobantesMasSeries[i]);
            }
            console.log($scope.tiposDeComprobantes);
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema", data.error);
        });
    }
    $scope.obtenerOrdenDeVentaParaAnulacion = function (idOrdenVenta) {
        ventaService.obtenerOrdenVentaYDetallesParaAnulacionDeVenta({ idOrdenVenta: idOrdenVenta }).success(function (data) {
            $scope.anulacion = data;
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema", data.error);
        });
    }
    $scope.cargarSeriesParaAnulacionVenta = function (tipoComprobante) {
        console.log(tipoComprobante);
        if (tipoComprobante != null) {
            $scope.series = angular.copy(tipoComprobante.Series);
        }
    }
    $scope.obtenerMediosDePagoParaAnulacionVenta = function () {
        ventaService.obtenerMediosDePagoVenta({}).success(function (data) {
            $scope.mediosDePagoParaAnulacionVenta = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    //#endregion

    //#region CONFIRMACION DE VENTA
    $scope.trazaDeInicial = {};

    $scope.iniciarConfirmacion = function () {
        $scope.trazaDeInicial = {};
        $scope.obtenerMediosDePagoConfirmacionVenta();
        $scope.obtenerEntidadesFinancierasConfirmacionVenta();
    }

    $scope.obtenerMediosDePagoConfirmacionVenta = function () {
        maestroService.obtenerMediosDePago({}).success(function (data) {
            $scope.mediosDePagoConfirmacionVenta = data;
        }).error(function (data) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }

    $scope.obtenerEntidadesFinancierasConfirmacionVenta = function () {
        maestroService.obtenerEntidadesBancarias({}).success(function (data) {
            $scope.entidadesFinancierasConfirmacionVenta = data;
        }).error(function (data) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }

    $scope.mostrarIngresoTrazaDePago = function () {
        return ($scope.verVentaCorporativa.ModoDePago == "1" || ($scope.verVentaCorporativa.ModoDePago == "3" && $scope.verVentaCorporativa.PagarInicialAlConfimar));
    }

    $scope.datosRequeridosParaRealizarConfimacionVenta = function () {
        return $scope.trazaDeInicial.MedioDePago == undefined || $scope.trazaDeInicial.EntidadFinanciera == undefined || $scope.trazaDeInicial.InformacionDePago == undefined;
    }

    $scope.guardarConfirmacionVenta = function () {
        ventaService.guardarConfirmacionVenta({ idVenta: $scope.verVentaCorporativa.idVenta, trazaPagoInicial: $scope.trazaDeInicial }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.listarBandeja();
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema", data.error);
        });
    }

    //#endregion

    //#region REGISTRO DE COMPROBANTE

    $scope.comprobanteVenta = {};
    $scope.tiposDeComprobantesMasSeriesVenta = [];
    $scope.tiposDeComprobantesVenta = [];
    $scope.seriesVenta = [];

    $scope.obtenerTiposDeComprobanteVenta = function () {
        ventaService.obtenerTiposDeComprobanteParaVenta({}).success(function (data) {
            $scope.selectorTiposDeComprobantesMasSeriesVenta = data;
            $scope.tiposDeComprobantesVenta = [];
            for (var i = 0; i < $scope.selectorTiposDeComprobantesMasSeriesVenta.length; i++) {
                $scope.tiposDeComprobantesVenta.push($scope.selectorTiposDeComprobantesMasSeriesVenta[i]);
            }
        }).error(function (data) {
            SweetAlert.error("Ocurrio un Problema", data.error);
            $scope.messageError(data.error);
        });
    }
    $scope.cargarSeriesVenta = function (tipoComprobante) {
        if (tipoComprobante != null) {
            $scope.series = angular.copy(tipoComprobante.Series);
        }
    }
    $scope.guardarComprobanteVenta = function () {
        ventaService.registrarComprobanteVentaCorporativa({ idVenta: $scope.verVentaCorporativa.idVenta, comprobante: $scope.comprobanteVenta }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.listarBandeja();//ACCION DESPUES DE GUARDAR COMPROBANTE
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema", data.error);
        });
    }

    //#endregion

    //#region VER VENTA CORPORATIVA
    $scope.verVentaCorporativa = {};

    $scope.limpiarVerVentaCorporativa = function () {
        $scope.verVentaCorporativa = {};
    }

    $scope.verDetallesVenta = function (item) {
        ventaService.obtenerOrdenVentaCorporativaYDetalles({ idOrdenVenta: item.Id }).success(function (data) {
            $scope.verVentaCorporativa = data;
        }).error(function (data) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }

    //#endregion

    //#region VENTA PASADA

    $scope.iniciarVentaPasada = function () {
        if ($scope.venta.EsVentaPasada == false) {
            $scope.venta.fechaRegistro = '';
        }
        $timeout(function () { $('#fechaRegistro').trigger("change"); }, 100);
    }

    //#endregion

    //#region VENTA POR CONTINGENCIA

    $scope.inicializarVentasPorContingencia = function () {
        $scope.cargarParametros();
        $scope.limpiarRegistro();
        $scope.cargarColeccionesAsync();
        $scope.cargarColeccionesSyncDeVentasPorContingencia().then(function (resultado_) {
            $scope.establecerDatosPorDefecto();
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
        $scope.permitirVentaConFechaPasada = true;
        $scope.venta.EsVentaPasada = true;
        $timeout(function () { $('#esVentaPasada').trigger("change"); }, 100);
        document.getElementById("esVentaPasada").disabled = true;

    }

    $scope.cargarColeccionesSyncDeVentasPorContingencia = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        try {
            $scope.obtenerClientes().then(function (resultado_) {
                $scope.obtenerTiposDeComprobanteParaVentasPorContingencia().then(function (resultado_) {
                    defered.resolve();
                }, function (error) {
                    $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
                });
            }, function (error) {
                $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
            });
        } catch (e) {
            defered.reject(e);
        }
        return promise;
    }

    $scope.obtenerTiposDeComprobanteParaVentasPorContingencia = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        ventaService.obtenerTiposDeComprobanteParaVentasPorContingencia({}).success(function (data) {
            $scope.selectorTiposDeComprobantesMasSeries = data;
            $scope.tiposDeComprobantes = [];
            for (var i = 0; i < $scope.selectorTiposDeComprobantesMasSeries.length; i++) {
                $scope.tiposDeComprobantes.push($scope.selectorTiposDeComprobantesMasSeries[i]);
            }
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.guardarVentaPorContingencia = function () {
        ventaService.guardarVentaPorContingencia({ venta: $scope.venta }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.limpiarRegistro();
            if (!$scope.aplicaLeyAmazonia) { $scope.venta.GrabaIgv = true; }
            $scope.venderConBoleta = data.VenderConBoleta;
            $scope.inicializarVentasPorContingencia();
        }).error(function (data, status) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }

    //#endregion

    //#region GUIAS DE REMISION 
    $scope.inicializarGuiasDeRemision = function () {
        $scope.cargarParametrosGuiasDeRemision();
        $scope.limpiarGuiasDeRemision();
        $scope.cargarColeccionesAsyncGuiasDeRemision();
        $scope.cargarColeccionesSyncGuiasDeRemision().then(function (resultado_) {
            $scope.establecerDatosPorDefectoGuiasDeRemision();
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
    }

    $scope.cargarParametrosGuiasDeRemision = function () {
        $scope.idUbigeoSede = idUbigeoSede;
        $scope.direccionSede = direccionSede;
        $scope.idModalidadTrasladoPorDefecto = idModalidadTrasladoPorDefecto;
        $scope.idMotivoTrasladoPorDefecto = idMotivoTrasladoPorDefecto;
        $scope.idTransportistaPorDefecto = idTransportistaPorDefecto;
        $scope.idTipoDeComprobantePorDefecto = idTipoDeComprobantePorDefecto;
    }

    $scope.limpiarGuiasDeRemision = function () {
        $scope.movimiento = { Detalles: [] };
        $scope.movimiento.EsTrasladoTotal = {};
        $scope.hayInconsistenciasGuiasDeRemision = {};
        $scope.inconsistenciasGuiasDeRemision = [];
    }

    $scope.cargarColeccionesAsyncGuiasDeRemision = function () {
        $scope.detallesDeGuiaDeRemision = [];
        //$scope.venta.HaySalidaDeMercaderia = true;
    }

    $scope.cargarColeccionesSyncGuiasDeRemision = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        var promiseList = [];
        promiseList.push($scope.obtenerModalidadesTraslado());
        promiseList.push($scope.obtenerMotivosTraslado());
        promiseList.push($scope.obtenerUbigeoDistrito());
        promiseList.push($scope.obtenerTransportistas());
        promiseList.push($scope.obtenerTipoDeComprobanteGuiaDeRemision());
        promiseList.push($scope.obtenerUbigeoDireccionCliente());
        promiseList.push($scope.obtenerDetalleDireccionCliente());

        return $q.all(promiseList).then(function (response) {
            defered.resolve();
        }).catch(function (error) {
            defered.reject(e);
        });
        return promise;
    }

    $scope.establecerDatosPorDefectoGuiasDeRemision = function () {
        $scope.movimiento.Observacion = 'NINGUNO';
        $scope.movimiento.EsTrasladoTotal = true;
        $scope.movimiento.DireccionOrigen = $scope.direccionSede;
        $scope.movimiento.DireccionDestino = $scope.direccionDeCliente;
        $scope.establecerUbigeoOrigenPorDefecto();
        $scope.establecerUbigeoDestinoPorDefecto();
        $scope.establecerModalidadTrasladoPorDefecto();
        $scope.establecerMotivoTrasladoPorDefecto();
        $scope.establecerTransportistaPorDefecto();
        $scope.establecerTipoDeComprobantePorDefecto();
        $scope.hayInconsistenciasGuiasDeRemision = true;
        $scope.tipoMovimiento = 'false';
        //Iniciar la tabla de salida de mercaderia
        if ($scope.venta.HaySalidaDeMercaderia == true) {
            $scope.movimiento.Detalles = angular.copy($scope.detallesDeGuiaDeRemision);
        } else {
            for (var i = 0; i < $scope.venta.Detalles.length; i++) {
                $scope.detallesDeGuiaDeRemision.push({ IdProducto: $scope.venta.Detalles[i].Producto.Id, Descripcion: $scope.venta.Detalles[i].Producto.Nombre, Lote: $scope.venta.Detalles[i].Lote, Ordenado: parseFloat($scope.venta.Detalles[i].Cantidad), RecibidoEntregado: 0.00, IngresoSalidaActual: parseFloat($scope.venta.Detalles[i].Cantidad) });
                $scope.movimiento.Detalles = angular.copy($scope.detallesDeGuiaDeRemision);
            }
        }
    }

    $scope.obtenerModalidadesTraslado = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        maestroService.obtenerModalidadesTraslado().success(function (data) {
            $scope.modalidadesTraslado = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.obtenerMotivosTraslado = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        maestroService.obtenerMotivosTraslado().success(function (data) {
            $scope.motivosTraslado = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.obtenerTransportistas = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        compraService.obtenerProveedores().success(function (data) {
            $scope.transportistas = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.obtenerTipoDeComprobanteGuiaDeRemision = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        almacenService.obtenerTipoDeComprobanteGuiaDeRemision({}).success(function (data) {
            $scope.tiposDeComprobantesMasSeriesMovimientoDeAlmacen = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.cargarSeriesGuiaDeRemision = function (tipoComprobante) {
        if (tipoComprobante != null) {
            $scope.seriesMovimientoDeAlmacen = angular.copy(tipoComprobante.Series);
            //Establece por defecto la primera serie que tenga, en el caso de que tengan mas de 2 series.
            if ($scope.seriesMovimientoDeAlmacen.length > 1) {
                $scope.movimiento.SerieSeleccionada = $scope.seriesMovimientoDeAlmacen[0];
            }
        }
    }

    $scope.obtenerUbigeoDistrito = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        maestroService.listarUbigeoDistrito().success(function (data) {
            $scope.ubigeosPeru = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.obtenerUbigeoDireccionCliente = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        if ($scope.venta.Cliente != undefined) {
            clienteService.obtenerUbigeoDireccionCliente({ idCliente: $scope.venta.Cliente.Id }).success(function (data) {
                $scope.idUbigeoDeCliente = data;
                defered.resolve();
            }).error(function (data) {
                $scope.messageError(data.error);
                defered.reject(data);
            });
        } else {
            defered.resolve();
        }
        return promise;
    }

    $scope.obtenerDetalleDireccionCliente = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        if ($scope.venta.Cliente != undefined) {
            clienteService.obtenerDetalleDireccionCliente({ idCliente: $scope.venta.Cliente.Id }).success(function (data) {
                $scope.direccionDeCliente = data;
                defered.resolve();
            }).error(function (data) {
                $scope.messageError(data.error);
                defered.reject(data);
            });
        } else {
            defered.resolve();
        }
        return promise;
    }

    $scope.establecerModalidadTrasladoPorDefecto = function () {
        //Modalidad: el parametro de modalidad por defecto o el primero
        var modalidadTrasladoPorDefecto = Enumerable.from($scope.modalidadesTraslado)
            .where("$.Id == '" + $scope.idModalidadTrasladoPorDefecto + "'").toArray()[0];
        $scope.movimiento.ModalidadTransporte = modalidadTrasladoPorDefecto != null ? modalidadTrasladoPorDefecto : $scope.modalidadesTraslado[0];
        $timeout(function () { $('#modalidad').trigger("change"); }, 100);
    }

    $scope.establecerMotivoTrasladoPorDefecto = function () {
        //Almacen: el parametro de motivo por defecto o el primero
        var motivoTrasladoPorDefecto = Enumerable.from($scope.motivosTraslado)
            .where("$.Id == '" + $scope.idMotivoTrasladoPorDefecto + "'").toArray()[0];
        $scope.movimiento.MotivoTraslado = motivoTrasladoPorDefecto != null ? motivoTrasladoPorDefecto : $scope.motivosTraslado[0];
        $timeout(function () { $('#motivo').trigger("change"); }, 100);
    }

    $scope.establecerTransportistaPorDefecto = function () {
        //Almacen: el parametro de motivo por defecto o el primero
        var transportistaPorDefecto = Enumerable.from($scope.transportistas)
            .where("$.Id == '" + $scope.idTransportistaPorDefecto + "'").toArray()[0];
        $scope.movimiento.Transportista = transportistaPorDefecto != null ? transportistaPorDefecto : $scope.transportistas[0];
        $timeout(function () { $('#transportista').trigger("change"); }, 100);
    }

    $scope.establecerTipoDeComprobantePorDefecto = function () {
        //Almacen: el parametro de motivo por defecto o el primero
        var tipoDeComprobantePorDefecto = Enumerable.from($scope.tiposDeComprobantesMasSeriesMovimientoDeAlmacen)
            .where("$.Id == '" + $scope.idTipoDeComprobantePorDefecto + "'").toArray()[0];
        $scope.movimiento.TipoDeComprobante = tipoDeComprobantePorDefecto != null ? tipoDeComprobantePorDefecto : $scope.tiposDeComprobantesMasSeriesMovimientoDeAlmacen[0];
        $timeout(function () { $('#documento').trigger("change"); }, 100);
        $scope.cargarSeriesGuiaDeRemision(tipoDeComprobantePorDefecto);
    }

    $scope.establecerUbigeoOrigenPorDefecto = function () {
        var ubigeo = Enumerable.from($scope.ubigeosPeru)
            .where("$.Id == '" + $scope.idUbigeoSede + "'").toArray()[0];
        $scope.movimiento.UbigeoOrigen = ubigeo != null ? ubigeo : $scope.ubigeosPeru[0];
        $timeout(function () { $('#ubigeoOrigen').trigger("change"); }, 100);
    }

    $scope.establecerUbigeoDestinoPorDefecto = function () {
        var ubigeo = Enumerable.from($scope.ubigeosPeru)
            .where("$.Id == '" + $scope.idUbigeoDeCliente + "'").toArray()[0];
        $scope.movimiento.UbigeoDestino = ubigeo != null ? ubigeo : $scope.ubigeosPeru[0];
        $timeout(function () { $('#ubigeoDestino').trigger("change"); }, 100);
    }

    $scope.verificarInconsistenciasMovimientoDeAlmacen = function () {
        var cantidadDiferenteDeCero = false;
        $scope.inconsistenciasGuiasDeRemision = [];
        if ($scope.movimiento.TipoDeComprobante == undefined) {
            $scope.inconsistenciasGuiasDeRemision.push("Es necesario seleccionar un documento.");
        } else {
            if ($scope.movimiento.TipoDeComprobante.EsPropio == false) {
                if ($scope.movimiento.TipoDeComprobante.SerieIngresada == "" || $scope.movimiento.TipoDeComprobante.SerieIngresada == null) {
                    $scope.inconsistenciasGuiasDeRemision.push("Es necesario ingresar la serie de comprobante.");
                } if ($scope.movimiento.TipoDeComprobante.NumeroIngresado == "" || $scope.movimiento.TipoDeComprobante.NumeroIngresado == null) {
                    $scope.inconsistenciasGuiasDeRemision.push("Es necesario ingresar el numero de comprobante.");
                }
            }
            if ($scope.movimiento.TipoDeComprobante.TipoComprobante.Id != $scope.idDocumentoNotaAlamacenInterna) {
                if ($scope.movimiento.FechaInicioTraslado == "" || $scope.movimiento.FechaInicioTraslado == undefined) {
                    $scope.inconsistenciasGuiasDeRemision.push("Es necesario ingresar la fecha de inicio de traslado.");
                } if ($scope.movimiento.Transporte != undefined) {
                    if ($scope.movimiento.Transporte.Transportista == undefined) {
                        $scope.inconsistenciasGuiasDeRemision.push("Es necesario ingresar el transportista.");
                    } if ($scope.movimiento.Transporte.MarcaYPlaca == "" || $scope.movimiento.Transporte.MarcaYPlaca == undefined) {
                        $scope.inconsistenciasGuiasDeRemision.push("Es necesario ingresar la marca y placa del vehiculo.");
                    } if ($scope.movimiento.Transporte.NumeroLicencia == "" || $scope.movimiento.Transporte.NumeroLicencia == undefined) {
                        $scope.inconsistenciasGuiasDeRemision.push("Es necesario ingresar el numero de licencia del conductor.");
                    }
                } if ($scope.movimiento.ModalidadTransporte == undefined) {
                    $scope.inconsistenciasGuiasDeRemision.push("Es necesario seleccionar la modalidad de transporte.");
                } if ($scope.movimiento.MotivoTraslado == undefined) {
                    $scope.inconsistenciasGuiasDeRemision.push("Es necesario seleccionar el motivo de transporte.");
                } if ($scope.movimiento.UbigeoOrigen == undefined) {
                    $scope.inconsistenciasGuiasDeRemision.push("Es necesario seleccionar el ubigeo origen del traslado.");
                } if ($scope.movimiento.DireccionOrigen == "" || $scope.movimiento.DireccionOrigen == undefined) {
                    $scope.inconsistenciasGuiasDeRemision.push("Es necesario ingresar la direccion origen del traslado.");
                } if ($scope.movimiento.UbigeoDestino == undefined) {
                    $scope.inconsistenciasGuiasDeRemision.push("Es necesario seleccionar el ubigeo destino del traslado.");
                } if ($scope.movimiento.DireccionDestino == "" || $scope.movimiento.DireccionDestino == undefined) {
                    $scope.inconsistenciasGuiasDeRemision.push("Es necesario ingresarla direccion destino del traslado.");
                }
            }
        }
        for (var i = 0; i < $scope.movimiento.Detalles.length; i++) {
            if ($scope.movimiento.Detalles[i].IngresoSalidaActual < 0 || $scope.movimiento.Detalles[i].IngresoSalidaActual > ($scope.movimiento.Detalles[i].Ordenado - $scope.movimiento.Detalles[i].RecibidoEntregado)) {
                $scope.inconsistenciasGuiasDeRemision.push("Es necesario la cantidad de los detalles sea mayor o igual a 0 y menor a la diferencia de ordenado y recibido o entregado.");
                break;
            }
        }
        for (var i = 0; i < $scope.movimiento.Detalles.length; i++) {
            if ($scope.movimiento.Detalles[i].IngresoSalidaActual != 0 && $scope.movimiento.Detalles[i].IngresoSalidaActual > 0) {
                cantidadDiferenteDeCero = true;
                break;
            }
        }
        if (cantidadDiferenteDeCero == false) {
            $scope.inconsistenciasGuiasDeRemision.push("Es necesario la cantidad de alguno de los detalles sea mayor a 0.");
        }
        $scope.hayInconsistenciasGuiasDeRemision = ($scope.inconsistenciasGuiasDeRemision.length == 0) ? false : true;
    }

    $scope.quitarGuiasDeRemision = function () {
        $scope.venta.HaySalidaDeMercaderia = false;
        $scope.venta.SalidasDeMercaderia = [];
        $scope.ValidarModoIntegrado();
    }

    $scope.verGuiasDeRemision = function () {
        $scope.guiaSeleccionada = 0;
        $scope.movimiento = $scope.venta.SalidasDeMercaderia[$scope.guiaSeleccionada];
        $scope.verificarInconsistenciasMovimientoDeAlmacen();
        $('#boton-editar-' + $scope.guiaSeleccionada).removeClass('btn-default').addClass('btn-primary');
    }

    $scope.nuevaGuiaDeRemision = function () {
        $scope.limpiarGuiasDeRemision();
        $scope.establecerDatosPorDefectoGuiasDeRemision();
        $('#boton-editar-' + $scope.guiaSeleccionada).removeClass('btn-primary').addClass('btn-default');
        $scope.guiaSeleccionada = -1;
    }

    $scope.editarGuiaDeRemision = function (index) {
        $scope.movimiento = $scope.venta.SalidasDeMercaderia[index];
        $scope.verificarInconsistenciasMovimientoDeAlmacen();
        if ($scope.guiaSeleccionada != index) {
            $('#boton-editar-' + index).removeClass('btn-default').addClass('btn-primary');
            $('#boton-editar-' + $scope.guiaSeleccionada).removeClass('btn-primary').addClass('btn-default');
            $scope.guiaSeleccionada = index;
        }
    }

    $scope.agregarGuiaDeRemision = function () {
        $scope.venta.SalidasDeMercaderia.push($scope.movimiento);
        $scope.agregarDetallesDeGuiaDeRemision();
        $scope.limpiarGuiasDeRemision();
        $scope.establecerDatosPorDefectoGuiasDeRemision();
        $scope.guiaSeleccionada = -1;
        $scope.ValidarModoIntegrado();
    }

    $scope.quitarGuiaDeRemision = function () {
        $scope.quitarDetallesDeGuiaDeRemision($scope.movimiento.Detalles)
        $scope.venta.SalidasDeMercaderia.splice($scope.guiaSeleccionada, 1);
        if ($scope.venta.SalidasDeMercaderia.length == 0) {
            $scope.venta.HaySalidaDeMercaderia = false;
        }
        $scope.guiaSeleccionada = -1;
        $scope.limpiarGuiasDeRemision();
        $scope.establecerDatosPorDefectoGuiasDeRemision();
        $scope.ValidarModoIntegrado();
    }

    $scope.guardarGuiaDeRemision = function () {
        var movimientoTotal = true;
        for (var i = 0; i < $scope.movimiento.Detalles.length; i++) {
            if ($scope.movimiento.Detalles[i].Ordenado - $scope.movimiento.Detalles[i].RecibidoEntregado != parseFloat($scope.movimiento.Detalles[i].IngresoSalidaActual)) {
                movimientoTotal = false;
                break;
            }
        }
        $scope.venta.SalidasDeMercaderia.push($scope.movimiento);
        $scope.agregarDetallesDeGuiaDeRemision();
        $scope.venta.HaySalidaDeMercaderia = true;
        $scope.limpiarGuiasDeRemision();
        $scope.establecerDatosPorDefectoGuiasDeRemision();
        if (movimientoTotal) {
            $('#modal-registro-salida-mercaderia-integrada').modal('hide');
        }
        $scope.guiaSeleccionada = -1;
        $scope.ValidarModoIntegrado();
    }

    $scope.agregarDetallesDeGuiaDeRemision = function () {
        for (var i = 0; i < $scope.detallesDeGuiaDeRemision.length; i++) {
            var producto = Enumerable.from($scope.movimiento.Detalles).where("$.IdProducto == '" + $scope.detallesDeGuiaDeRemision[i].IdProducto + "'").toArray()[0];
            if (producto != null) {
                $scope.detallesDeGuiaDeRemision[i].RecibidoEntregado = parseFloat($scope.detallesDeGuiaDeRemision[i].RecibidoEntregado) + parseFloat(producto.IngresoSalidaActual);
                $scope.detallesDeGuiaDeRemision[i].IngresoSalidaActual = parseFloat($scope.detallesDeGuiaDeRemision[i].Ordenado) - parseFloat($scope.detallesDeGuiaDeRemision[i].RecibidoEntregado);
            }
        }
        for (var i = 0; i < $scope.detallesDeGuiaDeRemision.length; i++) {
            if (parseFloat($scope.detallesDeGuiaDeRemision[i].Ordenado) - parseFloat($scope.detallesDeGuiaDeRemision[i].RecibidoEntregado) != 0) {
                $scope.estadoBotonLimpiar = true;
                break;
            } else {
                $scope.estadoBotonLimpiar = false;
            }
        }
    }

    $scope.quitarDetallesDeGuiaDeRemision = function () {
        for (var i = 0; i < $scope.detallesDeGuiaDeRemision.length; i++) {
            var producto = Enumerable.from($scope.movimiento.Detalles).where("$.IdProducto == '" + $scope.detallesDeGuiaDeRemision[i].IdProducto + "'").toArray()[0];
            if (producto != null) {
                $scope.detallesDeGuiaDeRemision[i].RecibidoEntregado = parseFloat($scope.detallesDeGuiaDeRemision[i].RecibidoEntregado) - parseFloat(producto.IngresoSalidaActual);
                $scope.detallesDeGuiaDeRemision[i].IngresoSalidaActual = parseFloat($scope.detallesDeGuiaDeRemision[i].Ordenado) - parseFloat($scope.detallesDeGuiaDeRemision[i].RecibidoEntregado);
            }
        }
        $scope.estadoBotonLimpiar = true;
    }




    /*IdProducto
    $scope.verificarTrasladoTotal = function () {
        var temp = true;
        for (var i = 0; i < $scope.movimiento.Detalles.length; i++) {
            if ($scope.movimiento.Detalles[i].Ordenado - $scope.movimiento.Detalles[i].RecibidoEntregado != parseFloat($scope.movimiento.Detalles[i].IngresoSalidaActual)) {
                temp = false;
                break;
            }
        }
        $scope.movimiento.EsTrasladoTotal = temp;
    }
    *//*
    //#endregion

    //#region VENTA POR MOSTRADOR INTEGRADO CAJA

    $scope.inicializarVentaPorMostradorIntegradoModoCaja = function () {
        $scope.esVentaPorMostradorIntegradoModoCaja = true;
        $scope.permitirRegistroAlmacenEnVentaPorMostradorIntegradoModoCaja = permitirRegistroAlmacenEnVentaPorMostradorIntegradoModoCaja;
        $scope.idAlmacenSeleccionadoPorDefecto = idAlmacenSeleccionadoPorDefecto;
        $scope.inicializar();
        $scope.cargarColeccionesSyncVentaPorMostradorIntegradoModoCaja().then(function (resultado_) {
            $scope.establecerDatosPorDefectoVentaPorMostradorIntegradoModoCaja();
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
    }

    $scope.cargarColeccionesSyncVentaPorMostradorIntegradoModoCaja = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        var promiseList = [];
        promiseList.push($scope.obtenerPuntosDeVenta());
        promiseList.push($scope.obtenerVendedores());
        promiseList.push($scope.obtenerAlmacenes());
        promiseList.push($scope.obtenerAlmaceneros());
        return $q.all(promiseList).then(function (response) {
            defered.resolve();
        }).catch(function (error) {
            defered.reject(e);
        });
        return promise;
    }


    $scope.establecerDatosPorDefectoVentaPorMostradorIntegradoModoCaja = function () {
        $scope.venta.PuntoDeVenta = $scope.listaPuntosDeVenta[0];
        $scope.venta.Vendedor = $scope.listaVendedores[0];
        $scope.venta.Almacenero = $scope.listaAlmaceneros[0];
        $scope.establecerAlmacenPorDefecto();
    }


    $scope.establecerAlmacenPorDefecto = function () {
        //Almacen: el mismo que ya es punto de venta o el primer 
        var almacen = Enumerable.from($scope.listaAlmacenes).where("$.Id == '" + $scope.idAlmacenSeleccionadoPorDefecto + "'").toArray()[0];
        $scope.venta.Almacen = almacen != null ? almacen : $scope.listaAlmacenes[0];
        $timeout(function () { $('#comboAlmacen').trigger("change"); }, 100);
    }

    $scope.obtenerPuntosDeVenta = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        centroDeAtencionService.obtenerCentrosDeAtencionConRolPuntoDeVentaVigentesConCodigoYEstablecimientoComercial().success(function (data) {
            $scope.listaPuntosDeVenta = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.obtenerVendedores = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        empleadoService.obtenerEmpleadosConRolVendedorVigentesConCodigo().success(function (data) {
            $scope.listaVendedores = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.obtenerAlmacenes = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        centroDeAtencionService.obtenerCentrosDeAtencionConRolAlmacenVigentesConCodigoYEstablecimientoComercial().success(function (data) {
            $scope.listaAlmacenes = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.obtenerAlmaceneros = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        empleadoService.obtenerEmpleadosConRolAlmaceneroVigentesConCodigo().success(function (data) {
            $scope.listaAlmaceneros = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.buscarConceptoPorCodigoDeBarraBalanza = function () {

        let codigoConcepto = $scope.venta.CodigoBarraBalanza.substring(2, 6);
        let cantidad = parseFloat($scope.venta.CodigoBarraBalanza.substring(12, 17)) / 1000;
        let importe = parseFloat($scope.venta.CodigoBarraBalanza.substring(6, 12)) / 100;
        let codigoPuntoVenta = $scope.venta.CodigoBarraBalanza.substring(18, 21);
        let codigoVendedor = $scope.venta.CodigoBarraBalanza.substring(21, 24);

        ventaService.obtenerMercaderiaPorCodigoBarra({ codigoBarra: parseInt(codigoConcepto) }).success(function (data) {
            if (data.data == 0) {//si data es igual a 0 no existe el producto o no tiene precio
                SweetAlert.warning("Advertencia", data.data_description);
            }
            else {
                if ($scope.venta.Vendedor.Codigo != codigoVendedor && $scope.venta.Detalles.length > 0) {
                    SweetAlert.warning("Advertencia", "EL VENDEDOR ES DISTINTO AL QUE ESTA REALIZANDO LA VENTA");
                } else {
                    $scope.detalle.Producto = data;
                    $scope.detalle.Cantidad = Math.round((cantidad) * 100) / 100;
                    $scope.detalle.Importe = Math.round((importe) * 100) / 100;
                    $scope.detalle.PrecioUnitario = Math.round(($scope.detalle.Importe / $scope.detalle.Cantidad) * 100) / 100;
                    $scope.detalle.PrecioCalculadoVenta = true;
                    if ($scope.venta.GrabaIgv || !$scope.aplicaLeyAmazonia) {
                        $scope.detalle.Igv = Math.round((parseFloat($scope.detalle.Importe - ($scope.detalle.Importe / (1 + $scope.tasaIGV)))) * 100) / 100;
                    } else {
                        $scope.detalle.Igv = 0;
                    }
                    $scope.detalle.Descuento = 0;
                    $scope.detalle.VersionFila = $scope.detalle.Producto.VersionFila;
                    $scope.detalle.IdPrecioUnitario = $scope.detalle.Producto.Precios[0].Id;
                    $scope.venta.Detalles.unshift($scope.detalle);
                    $scope.detalle = {};
                    var $index = $scope.venta.Detalles.length - 1;

                    $scope.seleccionarPuntoDeVentaConCodigo(codigoPuntoVenta);
                    $scope.seleccionarVendedorConCodigo(codigoVendedor);
                }
            }
            $scope.ValidarModoIntegrado();
            $scope.calcularTotal($scope.venta.Detalles);
            $scope.venta.CodigoBarraBalanza = "";
            $timeout(function () { $('.tarifa').trigger("change"); }, 100);
            $timeout(function () { $('#radio-0').trigger("focus"); }, 100);

        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.seleccionarPuntoDeVentaConCodigo = function (codigo) {
        //puntoDeVenta: el que tenga el codigo o el primero
        var puntoDeVenta = Enumerable.from($scope.listaPuntosDeVenta).where("$.Codigo == '" + codigo + "'").toArray()[0];
        $scope.venta.PuntoDeVenta = puntoDeVenta != null ? puntoDeVenta : $scope.listaPuntosDeVenta[0];
        $timeout(function () { $('#puntoDeVenta').trigger("change"); }, 100);
    }

    $scope.seleccionarVendedorConCodigo = function (codigo) {
        //vendedor: el que tenga el codigo o el primero
        var vendedor = Enumerable.from($scope.listaVendedores).where("$.Codigo == '" + codigo + "'").toArray()[0];
        $scope.venta.Vendedor = vendedor != null ? vendedor : $scope.listaVendedores[0];
        $timeout(function () { $('#vendedor').trigger("change"); }, 100);
    }

    $scope.guardarVentaPorMostradorIntegradoModoCaja = function () {
        ventaService.guardarVentaPorMostradorIntegradoModoCaja({ venta: $scope.venta }).success(function (data) {
            $scope.venderConBoleta = data.VenderConBoleta;
            $scope.limpiarRegistro();
            $scope.establecerDatosPorDefecto();
            $scope.establecerDatosPorDefectoVentaPorMostradorIntegradoModoCaja();
            SweetAlert.success("Correcto", data.result_description);
            jsWebClientPrint.print('idVenta=' + data.data);
        }).error(function (data, status) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }


    $scope.seleccionarCaracteristicasPropias = function (indexDetalle) {
        $scope.indexDetalle = indexDetalle;
    }



    //$scope.mouseClonar = function (index) {
    //    $('#clonar-' + index).removeClass('btn-oculto');
    //    $timeout(function () { $('#clonar-' + index).trigger("change"); }, 100);
    //}

    //$scope.api = { cliente: { Direcciones: [] } };

    //$scope.obtenerUbigeoDistrito = function () {
    //    maestroService.listarUbigeoDistrito().success(function (data) {
    //        $scope.ubigeosPeru = data;
    //    }).error(function (data) {
    //        $scope.messageError(data.error);
    //        defered.reject(data);
    //    });
    //}

    //$scope.obtenerUbigeoDistrito();

    //$scope.obtenerUbigeo = function (data) {
    //    for (var i = 0; i < $scope.ubigeosPeru.length; i++) {
    //        if ($scope.ubigeosPeru[i].Nombre == data) {
    //            return $scope.ubigeosPeru[i];
    //        }
    //    }

    //    let ubigeo = { Id: 100102, Nombre: "" };
    //    return ubigeo;
    //}

    //$scope.obtenerClientePorNumeroDeDocumento = function () {

    //    //Validar logitud de numero de documento ingresado: 8 o 11 digitos permitidos

    //    //Preguntar por los dos primeros caracteres ingresados del numero de documento

    //    //Si el documento comienza con 10 y tiene 11 digitos entonces es una persona natural con RUC

    //    //Si el documento comiezan con 20 y tiene 11 digitos entonces es una persona juridica con RUC

    //    //Si el documento tiene 8 digitos entonces es una persona natural

    //    // RUC 20 APELLIDO MATERNO NULL, APELLIDO PATERNO NULL
    //    // DNI 8  DIRECCION NULL, RAZON SOCIAL NULL
    //    // RUC 10 RAZON SOCIAL NULL , UBIGEO NULL

    //    var nome = $("#comboCliente").data('select2').$dropdown.find("input").val();

    //    console.log("combo cliente", nome);

    //    console.log("El numero de documento", $scope.api.cliente.NumeroDocumentoIdentidad);
    //    if ($scope.api.cliente.NumeroDocumentoIdentidad) {
    //        let longitudCaracteresDeNumeroDocumento = $scope.api.cliente.NumeroDocumentoIdentidad.length;
    //        if (longitudCaracteresDeNumeroDocumento == 8 || longitudCaracteresDeNumeroDocumento == 11) {
    //            $scope.obtenerIdTipoDocumentoIdentidad();
    //            clienteService.validar({ idTipoDocumento: $scope.api.cliente.TipoDocumentoIdentidad.Id, numeroDocumento: $scope.api.cliente.NumeroDocumentoIdentidad }).success(function (data) {
    //                $scope.dataResult = data;
    //                if ($scope.validarRespuestaDeObtenerClientePorNumeroDocumento()) {
    //                    $scope.obtenerIdTipoPersona();
    //                    $scope.crearClienteParaRegistroRapido();
    //                    $scope.guardarClienteRegistroRapido();
    //                }
    //            }).error(function (data) {
    //                SweetAlert.warning("Advertencia", data.mensaje);
    //            });
    //        } else {
    //            SweetAlert.warning("Advertencia", "Ingrese un número de 8 dígitos o 11 dígitos");
    //        }
    //    }

    //}

    //$scope.validarRespuestaDeObtenerClientePorNumeroDocumento = function () {
    //    let esDocumentoValido = false;
    //    if ($scope.dataResult.respuesta == 3) {
    //        SweetAlert.success("Correcto", "Número de documento valido");
    //        esDocumentoValido = true;
    //    }
    //    if ($scope.dataResult.respuesta == 5) {
    //        SweetAlert.warning("Advertencia", "El número de documento RUC ingresado no es correcto");
    //        esExitoso = false;
    //    }
    //    return esDocumentoValido;
    //}

    //$scope.obtenerIdTipoDocumentoIdentidad = function () {
    //    let caracteresConQueEmpiezaNumeroDocumentoIdentidad = $scope.api.cliente.NumeroDocumentoIdentidad.substr(0, 2);
    //    let longitudCaracteresDeNumeroDocumento = $scope.api.cliente.NumeroDocumentoIdentidad.length;

    //    let idTipoDocumentoIdentidad =
    //        longitudCaracteresDeNumeroDocumento == 8 ? idTipoDocumentoIdentidadDni
    //            : longitudCaracteresDeNumeroDocumento == 11 && caracteresConQueEmpiezaNumeroDocumentoIdentidad === "10" ? idTipoDocumentoIdentidadRuc
    //                : longitudCaracteresDeNumeroDocumento == 11 && caracteresConQueEmpiezaNumeroDocumentoIdentidad === "20" ? idTipoDocumentoIdentidadRuc : null;

    //    $scope.api.cliente.TipoDocumentoIdentidad = { Id: idTipoDocumentoIdentidad, Nombre: "" };
    //}

    //$scope.obtenerIdTipoPersona = function () {
    //    let caracteresConQueEmpiezaNumeroDocumentoIdentidad = $scope.api.cliente.NumeroDocumentoIdentidad.substr(0, 2);
    //    let longitudCaracteresDeNumeroDocumento = $scope.api.cliente.NumeroDocumentoIdentidad.length;

    //    let idTipoPersona =
    //        longitudCaracteresDeNumeroDocumento == 8 ? idTipoActorPersonaNatural
    //            : longitudCaracteresDeNumeroDocumento == 11 && caracteresConQueEmpiezaNumeroDocumentoIdentidad === "10" ? idTipoActorPersonaNatural
    //                : longitudCaracteresDeNumeroDocumento == 11 && caracteresConQueEmpiezaNumeroDocumentoIdentidad === "20" ? idTipoActorPersonaJuridica : null;

    //    $scope.api.cliente.TipoPersona = { Id: idTipoPersona, Nombre: "" };
    //}

    //$scope.crearClienteParaRegistroRapido = function () {
    //    $scope.api.cliente.ApellidoPaterno = $scope.dataResult.dataApi.ApellidoPaterno;
    //    $scope.api.cliente.ApellidoMaterno = $scope.dataResult.dataApi.ApellidoMaterno;
    //    $scope.api.cliente.ClaseActor = { Id: 1, Nombre: "MASCULINO" };
    //    $scope.direccion = {};
    //    $scope.api.cliente.Direcciones = [];
    //    $scope.direccion.Ubigeo = $scope.obtenerUbigeo($scope.dataResult.dataApi.Ubigeo);
    //    $scope.direccion.Detalle = $scope.dataResult.dataApi.Direccion == null ? '-' : $scope.dataResult.dataApi.Direccion;
    //    $scope.direccion.esVigente = true;
    //    $scope.direccion.esPrincipal = true;
    //    $scope.direccion.Tipo = { Id: 12, Nombre: "Domicilio Fiscal" };
    //    $scope.api.cliente.Direcciones.push($scope.direccion);
    //    $scope.api.cliente.EstadoLegalActor = { Id: 1, Nombre: "NO ESPECIFICADO" };
    //    $scope.api.cliente.NombreComercial = $scope.dataResult.dataApi.NombreComercial;
    //    $scope.api.cliente.Nombres = $scope.dataResult.dataApi.Nombres;
    //    $scope.api.cliente.RazonSocial = $scope.dataResult.dataApi.RazonSocial;
    //}

    //$scope.guardarClienteRegistroRapido = function () {
    //    clienteService.guardarCliente({ cliente: $scope.api.cliente }).success(function (data) {
    //        //SweetAlert.success("Correcto", data.result_description);
    //    }).error(function (data) {
    //        //SweetAlert.error("Ocurrio un problema", data.error);
    //    });
    //}
    //#endregion


    $scope.calcularValoresDetalle = function (identificador, detalles, index) {
        if (identificador == 1)//Cantidad
        {
            $scope.cambiarMascaraDeCalculo(detalles, index, 0, '1');
            if ($scope.ingresarCantidadCalcularPrecioUnitario) {
                detalles[index].PrecioUnitario = Math.round((parseFloat(detalles[index].Importe) / parseFloat(detalles[index].Cantidad)) * 100) / 100;
                $scope.cambiarMascaraDeCalculo(detalles, index, 1, '0');
            }
            else {
                detalles[index].Importe = Math.round((parseFloat(detalles[index].PrecioUnitario) * parseFloat(detalles[index].Cantidad)) * 100) / 100;
                $scope.cambiarMascaraDeCalculo(detalles, index, 2, '0');
            }
        }
        if (identificador == 2)//Precio Unitario
        {
            $scope.cambiarMascaraDeCalculo(detalles, index, 1, '1');
            if ($scope.ingresarPrecioUnitarioCalcularImporte) {
                detalles[index].Importe = Math.round((parseFloat(detalles[index].PrecioUnitario) * parseFloat(detalles[index].Cantidad)) * 100) / 100;
                $scope.cambiarMascaraDeCalculo(detalles, index, 2, '0');
            }
            else {
                detalles[index].Cantidad = Math.round((parseFloat(detalles[index].Importe) / parseFloat(detalles[index].PrecioUnitario)) * 100) / 100;
                $scope.cambiarMascaraDeCalculo(detalles, index, 0, '0');
            }
        }
        if (identificador == 3)//Importe
        {
            $scope.cambiarMascaraDeCalculo(detalles, index, 2, '1');
            if ($scope.ingresarImporteCalcularCantidad) {
                detalles[index].Cantidad = Math.round((parseFloat(detalles[index].Importe) / parseFloat(detalles[index].PrecioUnitario)) * 100) / 100;
                $scope.cambiarMascaraDeCalculo(detalles, index, 0, '0');
            }
            else {
                detalles[index].PrecioUnitario = Math.round((parseFloat(detalles[index].Importe) / parseFloat(detalles[index].Cantidad)) * 100) / 100;
                $scope.cambiarMascaraDeCalculo(detalles, index, 1, '0');
            }
        }
        //Verificar que la cantidad sea menor que el stock
        if (detalles[index].EsBien && ventasSujetasADisponibilidadStock && detalles[index].Cantidad > detalles[index].Producto.Stock) {
            SweetAlert.warning("Advertencia", "El Stock de " + detalles[index].Producto.Nombre + " es " + detalles[index].Producto.Stock);
            detalles[index].Cantidad = detalles[index].Producto.Stock;
        }
        //Calcular el numero de bolsas de plastico
        $scope.calcularNumeroDeBolsasPlasticas(detalles);
        detalles[index].Igv = ($scope.venta.GrabaIgv || !$scope.aplicaLeyAmazonia) ? Math.round((parseFloat(detalles[index].Importe - (detalles[index].Importe / (1 + $scope.tasaIGV)))) * 100) / 100 : 0;
        $scope.calcularTotal(detalles);
    }

    $scope.calcularTotal = function (detalles) {
        if ($scope.venta.Flete > 0) {
            $scope.venta.Total = parseFloat($scope.venta.Flete);
            $scope.venta.Igv = ($scope.venta.GrabaIgv || !$scope.aplicaLeyAmazonia) ? (Math.round((parseFloat($scope.venta.Flete - ($scope.venta.Flete / (1 + $scope.tasaIGV)))) * 100) / 100) : 0;
            $scope.venta.SubTotal = parseFloat($scope.venta.Total) - parseFloat($scope.venta.Igv);
        } else {
            $scope.venta.SubTotal = 0;
            $scope.venta.Igv = 0;
            $scope.venta.Total = 0;
        }
        $scope.venta.Descuento = 0;
        for (i = 0; i < detalles.length; i++) {
            $scope.venta.Total += parseFloat(detalles[i].Importe);
            $scope.venta.Descuento += parseFloat(detalles[i].Descuento);
            $scope.venta.Igv += parseFloat(detalles[i].Igv);
        }
        $scope.venta.SubTotal = parseFloat($scope.venta.Total - $scope.venta.Igv);
        $scope.venta.Icbper = Math.round(($scope.venta.NumeroBolsasDePlastico * $scope.costoUnitarioPorBolsaDePlastico) * 100) / 100;
        $scope.venta.Total += $scope.venta.Icbper;
        $scope.ValidarModoIntegrado();
        if ($scope.venta.EsVentaACredito == true && $scope.financiamientoRealizado == true) {
            $("#reiniciar-financimiento").click();
        }
    }

    $scope.cambiarMascaraDeCalculo = function (detalles, index, campo, valor) {
        var mascaraDeCalculoArray = detalles[index].MascaraDeCalculo.split('');
        mascaraDeCalculoArray[campo] = valor;
        detalles[index].MascaraDeCalculo = mascaraDeCalculoArray.join('');
    }

    //$scope.cambioFocus = function (identificador, index) {
    //    var ultimoFocus = identificador == 1 ? '#cantidad' : identificador == 2 ? '#precio' : '#importe';
    //    $timeout(function () { $('#cantidad-' + index).trigger("focus"); }, 100);
    //    $timeout(function () { $('#precio-' + index).trigger("focus"); }, 100);
    //    $timeout(function () { $('#importe-' + index).trigger("focus"); }, 100);
    //    $timeout(function () { $(ultimoFocus + '-' + index).trigger("focus"); }, 100);
    //}
});

*/
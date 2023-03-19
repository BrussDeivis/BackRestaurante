app.controller('compraCorporativaController', function ($scope, $timeout, $rootScope, $q, SweetAlert, $filter, DTOptionsBuilder, DTColumnDefBuilder, compraService, maestroService, clienteService, productoService, centroDeAtencionService, empleadoService, conceptoService, compraService, almacenService) {

    //********** BANDEJA DE COMPRA CORPORATIVA  ***********//

    $scope.iniciarBandejaComprasCorporativas = function () {
        $scope.fechaInicio = fechaInicio;
        $scope.fechaFin = fechaFin;
        //Listamos bandejas de compras
        $scope.listarBandejaComprasCorporativas();
    }

    $scope.listarBandejaComprasCorporativas = function () {
        compraService.obtenerCompras({ desde: $scope.fechaInicio, hasta: $scope.fechaFin }).success(function (data) {
            $scope.comprasCorporativas = data;
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    //#region VER COMPRA CORPORATIVA

    $scope.verCompraCorporativa = {};

    $scope.limpiarVerCompraCorporativa = function () {
        $scope.verCompraCorporativa = {};
    }

    $scope.verCompraCorporativa = function (item) {
        compraService.obtenerCompraCorporativa({ idOrdenCompra: item.Id }).success(function (data) {
            $scope.verCompraCorporativa = data;
            $scope.EsOrdenDeCompra = item.EsOrdenDeCompra;
            $scope.IdOrden = item.Id;

        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.cargarCompraAEditar = function (id) {
        compraService.cargarCompraCorporativaYDetallesAEditar({ idOrdenCompra: id }).success(function (data) {
            $scope.modelo = data;
            $scope.esEdicion = true;
            var date;
            date = $scope.modelo.FechaRegistro.slice($scope.modelo.FechaRegistro.indexOf("(") + 1, $scope.modelo.FechaRegistro.indexOf(")"));
            $scope.modelo.FechaRegistro = $scope.formatDate(new Date(+date), "ES");
            $("#fechaRegistro").datepicker('setDate', $scope.modelo.FechaRegistro);
            $scope.cambioDeExoneradoIgv($scope.modelo.Detalles);
            if ($scope.modelo.EsCompraACredito == false) {
                $scope.modelo.EsCreditoRapido = {};
            }
            for (var i = 0; i < $scope.modelo.Detalles.length; i++) {
                var dat;
                dat = $scope.modelo.Detalles[i].Vencimiento.slice($scope.modelo.Detalles[i].Vencimiento.indexOf("(") + 1, $scope.modelo.Detalles[i].Vencimiento.indexOf(")"));
                $scope.modelo.Detalles[i].Vencimiento = $scope.formatDate(new Date(+dat), "ES");
                $("#fechaVencimiento" + i).datepicker('setDate', $scope.modelo.Detalles[i].Vencimiento);
            }

        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    //#endregion

    //#region COMPRA CORPORATIVA

    $scope.inicializarCompraCorporativa = function () {
        $scope.cargarParametrosCompraCorporativa();
        $scope.limpiarCompraCorporativa();
        $scope.cargarColeccionesAsyncCompraCorporativa();
        $scope.cargarColeccionesSyncCompraCorporativa().then(function (resultado_) {
            $scope.establecerDatosPorDefectoCompraCorporativa();
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
    }

    $scope.cargarParametrosCompraCorporativa = function () {
        $scope.idEstablecimientoComercialPorDefecto = idEstablecimientoComercialPorDefecto;
        $scope.idCentroAtencionPorDefecto = idCentroAtencionPorDefecto;
        $scope.idEmpleadoPorDefecto = idEmpleadoPorDefecto;
    }

    $scope.limpiarCompraCorporativa = function () {
        $scope.esCompraCorporativa = true;
    }

    $scope.cargarColeccionesAsyncCompraCorporativa = function () {

    }

    $scope.cargarColeccionesSyncCompraCorporativa = function () {
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

    $scope.establecerDatosPorDefectoCompraCorporativa = function () {
        $scope.esCompraCorporativa = true;
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
            SweetAlert.error2(data);
            defered.reject(data);
        });
        return promise;
    }

    $scope.obtenerCentrosAtencionDeVendedor = function (id) {
        var defered = $q.defer();
        var promise = defered.promise;
        centroDeAtencionService.obtenerCentrosDeAtencionConRolPuntoDeCompraVigentesPorEstablecimientoComercial({ idEstablecimientoComercial: id }).success(function (data) {
            $scope.listaCentrosDeAtencionDeVendedor = data;
            $scope.establecerCentroAtencionDeVendedorPorDefecto();
            defered.resolve();
        }).error(function (data) {
            SweetAlert.error2(data);
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
            SweetAlert.error2(data);
            defered.reject(data);
        });
        return promise;
    }

    $scope.establecerEstablecimientoComercialDeVendedorPorDefecto = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        var establecimientoComercial = Enumerable.from($scope.listaEstablecimientosComerciales)
            .where("$.Id == '" + $scope.idEstablecimientoComercialPorDefecto + "'").toArray()[0];
        $scope.modelo.EstablecimientoComercial = establecimientoComercial != null ? establecimientoComercial : $scope.listaEstablecimientosComerciales[0];
        $timeout(function () { $('#establecimientoComercial').trigger("change"); }, 100);
        $scope.obtenerCentrosAtencionDeVendedor($scope.idEstablecimientoComercialPorDefecto);
        return promise;
    }

    $scope.establecerCentroAtencionDeVendedorPorDefecto = function () {
        var centroAtencion = Enumerable.from($scope.listaCentrosDeAtencionDeVendedor)
            .where("$.Id == '" + $scope.idCentroAtencionPorDefecto + "'").toArray()[0];
        $scope.modelo.CentroDeAtencion = centroAtencion != null ? centroAtencion : $scope.listaCentrosDeAtencionDeVendedor[0];
        $timeout(function () { $('#centroDeAtencion').trigger("change"); }, 100);
    }

    $scope.establecerVendedorPorDefecto = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        var vendedor = Enumerable.from($scope.listaVendedores)
            .where("$.Id == '" + $scope.idEmpleadoPorDefecto + "'").toArray()[0];
        $scope.modelo.Vendedor = vendedor != null ? vendedor : $scope.listaVendedores[0];
        $timeout(function () { $('#vendedor').trigger("change"); }, 100);
        defered.resolve();
        return promise;
    }

    //#endregion

    //#region TRAZA DE PAGO DE COMPRA CORPORATIVA

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
        $scope.modelo.HayRegistroTrazaDePago = {};
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
        $scope.modelo.HayRegistroTrazaDePago = false;
        $scope.establecerEstablecimientoComercialDeCajeroPorDefecto();
        $scope.establecerCajeroPorDefecto();
        $scope.establecerMedioDePagoDeTrazaDePagoPorDefecto();
        if (!$scope.modelo.EsCompraACredito || ($scope.modelo.Inicial > 0 && !$scope.modelo.EsCreditoRapido)) { $scope.desabilitarCheckboxTrazaDePago = false; }
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
            SweetAlert.error2(data);
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
            SweetAlert.error2(data);
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
            SweetAlert.error2(data);
            defered.reject(data);
        });
        return promise;
    }

    $scope.obtenerEntidadesFinancierasDeTrazaDePago = function (idMedioDePago) {
        maestroService.obtenerEntidadesBancarias({}).success(function (data) {
            $scope.listaEntidadesFinancieras = data;
            $scope.establecerEntidadFinancieraPorDefecto();
        }).error(function (data) {
            SweetAlert.error2(data);
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
        if ($scope.modelo.HayRegistroTrazaDePago) {
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
        $scope.modelo.HayRegistroSalidaDeMercaderia = {};
        $scope.modelo.UsaComprobanteOrden = {};

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
        $scope.modelo.HayRegistroSalidaDeMercaderia = false;
        $scope.modelo.UsaComprobanteOrden = false;
        $scope.tipoMovimiento = 'false';
        //Iniciar la tabla de salida de mercaderia
        for (var i = 0; i < $scope.modelo.Detalles.length; i++) {
            $scope.movimiento.Detalles.push({ IdProducto: $scope.modelo.Detalles[i].Producto.Id, Descripcion: $scope.modelo.Detalles[i].Producto.Nombre, Ordenado: parseFloat($scope.modelo.Detalles[i].Cantidad), RecibidoEntregado: 0.00, IngresoSalidaActual: parseFloat($scope.modelo.Detalles[i].Cantidad) });
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
            SweetAlert.error2(data);
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
            SweetAlert.error2(data);
            defered.reject(data);
        });
        return promise;
    }

    $scope.obtenerTransportistas = function () {
        compraService.obtenerProveedores({}).success(function (data) {
            $scope.transportistas = data;
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.obtenerModalidadesTraslado = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        maestroService.obtenerModalidadesTraslado().success(function (data) {
            $scope.modalidadesTraslado = data;
            defered.resolve();
        }).error(function (data) {
            SweetAlert.error2(data);
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
            SweetAlert.error2(data);
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
        almacenService.obtenerTiposDeComprobanteMovimientoDeAlmacen({}).success(function (data) {
            $scope.tiposDeComprobantesMasSeriesMovimientoMercaderia = data;
        }).error(function (data) {
            SweetAlert.error2(data);
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
        if ($scope.modelo.Cliente != undefined) {
            clienteService.obtenerDireccionCliente({ idCliente: $scope.modelo.Cliente.Id }).success(function (data) {
                $scope.direccionDeCliente = data;
                defered.resolve();
            }).error(function (data) {
                SweetAlert.error2(data);
                defered.reject(data);
            });
        } else {
            defered.resolve();
        }
        return promise;
    }

    $scope.accionSalidaDeMercaderia = function () {
        if ($scope.modelo.HayRegistroSalidaDeMercaderia) {
            $scope.accionDeshabilitar(false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);
            $scope.hayInconsistenciasSalidaDeMercaderia = true;
            $scope.verificarInconsistenciasMovimientoMercaderia();
        } else {
            $scope.accionDeshabilitar(true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
            $scope.modelo.UsaComprobanteOrden = false;
            $scope.hayInconsistenciasSalidaDeMercaderia = false;
            $scope.limpiarSalidaDeMercaderia();
            $scope.establecerDatosPorDefectoSalidaDeMercaderia();
        }
    }

    $scope.accionUsaComprobanteOrden = function () {
        if ($scope.modelo.UsaComprobanteOrden) {
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
        if ($scope.modelo.HayRegistroSalidaDeMercaderia) {
            if ($scope.modelo.UsaComprobanteOrden == false) {
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


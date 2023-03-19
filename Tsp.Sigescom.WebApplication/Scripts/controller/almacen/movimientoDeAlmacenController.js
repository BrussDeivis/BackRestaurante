app.controller('movimientoDeAlmacenController', function ($scope, $q, $compile, SweetAlert, $rootScope, $timeout, DTOptionsBuilder, DTColumnDefBuilder, almacenService, maestroService, centroDeAtencionService, productoService, ventaService, compraService, centroDeAtencionService) {

    //#region BANDEJA DE MOVIMIENTOS DE ALMACEN
    $scope.inicializar = function () {
        $scope.idEstablecimientoPorDefecto = idEstablecimientoPorDefecto;
        $scope.idCentroDeAtencionPorDefecto = idCentroDeAtencionPorDefecto;
        $scope.tieneRolAdministradorDeNegocio = tieneRolAdministradorDeNegocio;
        $scope.buscador = { fechaInicio: fechaInicio, fechaFin: fechaFin, esEntrada: true, establecimientosComerciales: [], centrosDeAtencion: [] }
        $scope.obtenerEstablecimientosComerciales().then(function (resultado_) {
            $scope.establecerEstablecimientoComercialPorDefecto();
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
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

    $scope.establecerEstablecimientoComercialPorDefecto = function () {
        var establecimientoComercial = Enumerable.from($scope.listaEstablecimientosComerciales)
            .where("$.Id == '" + $scope.idEstablecimientoComercialPorDefecto + "'").toArray()[0];
        $scope.buscador.establecimientosComerciales.push(establecimientoComercial != null ? establecimientoComercial : $scope.listaEstablecimientosComerciales[0]);
        $timeout(function () { $('#establecimientoComercial').trigger("change"); }, 100);
        $scope.obtenerCentrosDeAtencionConRolAlmacen().then(function (resultado_) {
            $scope.establecerCentroAtencionConRolAlmacenPorDefecto();
            $scope.listarBandeja();
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
    }

    $scope.obtenerCentrosDeAtencionConRolAlmacen = function () {
        if ($scope.buscador.establecimientosComerciales == undefined) {
            $scope.listaCentrosDeAtencionConRolAlmacen = [];
            $scope.buscador.centrosDeAtencion = [];
            $timeout(function () { $('#centroDeAtencion').trigger("change"); }, 100);
        } else {
            var defered = $q.defer();
            var promise = defered.promise;
            $scope.idsEstablecimientosComerciales = [];
            for (var i = 0; i < $scope.buscador.establecimientosComerciales.length; i++) {
                $scope.idsEstablecimientosComerciales.push($scope.buscador.establecimientosComerciales[i].Id)
            }
            centroDeAtencionService.obtenerCentrosDeAtencionConRolAlmacenVigentesPorEstablecimientosComerciales({ idsEstablecimientosComerciales: $scope.idsEstablecimientosComerciales }).success(function (data) {
                $scope.listaCentrosDeAtencionConRolAlmacen = data;
                defered.resolve();
            }).error(function (data) {
                $scope.messageError(data.error);
                defered.reject(data);
            });
            return promise;
        }
    }

    $scope.establecerCentroAtencionConRolAlmacenPorDefecto = function () {
        var centroAtencion = Enumerable.from($scope.listaCentrosDeAtencionConRolAlmacen)
            .where("$.Id == '" + $scope.idCentroDeAtencionPorDefecto + "'").toArray()[0];
        $scope.buscador.centrosDeAtencion.push(centroAtencion != null ? centroAtencion : $scope.listaCentrosDeAtencionConRolAlmacen[0]);
        $timeout(function () { $('#centroDeAtencion').trigger("change"); }, 100);
    }

    $rootScope.dtOptions.withLightColumnFilter({
        '1': { type: 'text', className: 'form-control padding-left-right-3' },
        '2': { type: 'text', className: 'form-control padding-left-right-3' },
        '3': { type: 'text', className: 'form-control padding-left-right-3' },
        '4': { type: 'text', className: 'form-control padding-left-right-3' },
        '5': { type: 'text', className: 'form-control padding-left-right-3' },
        '6': { type: 'text', className: 'form-control padding-left-right-3' },
        '7': { type: 'text', className: 'form-control padding-left-right-3' },
        '8': { type: 'text', className: 'form-control padding-left-right-3' }
    });

    $scope.listarBandeja = function () {
        $scope.idsCentrosDeAtencion = [];
        for (var i = 0; i < $scope.buscador.centrosDeAtencion.length; i++) {
            $scope.idsCentrosDeAtencion.push($scope.buscador.centrosDeAtencion[i].Id)
        }
        almacenService.obtenerMovimientosDeAlmacen({ esEntrada: $scope.buscador.esEntrada, idsCentrosDeAtencion: $scope.idsCentrosDeAtencion, desde: $scope.buscador.fechaInicio, hasta: $scope.buscador.fechaFin }).success(function (data) {
            $scope.movimientosDeAlmacen = data;
            $scope.labelOrigenDestino = ($scope.buscador.esEntrada == true || $scope.buscador.esEntrada == 'true') ? 'Origen' : 'Destino';
            $scope.labelEntradaSalida = ($scope.buscador.esEntrada == true || $scope.buscador.esEntrada == 'true') ? 'ENTRADA' : 'SALIDA';
            $timeout(function () { $('#tabla-movimiento-almacen').trigger("change"); }, 100);
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
    //#endregion

    //#region VER MOVIMIENTO DE ALMACEN
    $scope.inicializarVerMovimiento = function (item) {
        $scope.ordenDeMovimientoAcordion = false;
        $scope.verMovimiento = {};
        $scope.idMovimiento = item.Id;
        $scope.obtenerFormatosDeImpresion().then(function (resultado_) {
            $scope.formato = $scope.formatosDeImpresion[0];
            $scope.verMovimientoDeAlmacen();
            $scope.hayMovimiento = true;
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
    }

    $scope.verMovimientoDeAlmacen = function () {
        almacenService.obtenerMovimientoDeAlmacen({ idMovimiento: $scope.idMovimiento, formato: $scope.formato.Id }).success(function (data) {
            $scope.verMovimiento = data;
            $scope.tamanioComprobante = $scope.formato.Id == 1 ? '80mm' : "210mm";
            document.getElementById("pdfMovimiento").innerHTML = $scope.verMovimiento.CadenaHtmlDeMovimientoDeAlmacen;
            $timeout(function () { $('#pdfMovimiento').trigger("change"); }, 100);
            document.getElementById("pdfOrden").innerHTML = $scope.verMovimiento.CadenaHtmlDeOrdenDeMovimiento;
            $timeout(function () { $('#pdfOrden').trigger("change"); }, 100);
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema", data.error);
        });
    }

    $scope.obtenerFormatosDeImpresion = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        ventaService.obtenerFormatosDeImpresion({}).success(function (data) {
            $scope.formatosDeImpresion = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.cerrarVerMovimiento = function () {
        $scope.verMovimiento = {};
        $scope.formato = {};
        document.getElementById("pdfMovimiento").innerHTML = '';
        $timeout(function () { $('#pdfMovimiento').trigger("change"); }, 100);
        document.getElementById("pdfOrden").innerHTML = '';
        $timeout(function () { $('#pdfOrden').trigger("change"); }, 100);
    }

    $scope.accionOrdenDeMovimiento = function (bandera) {
        if (bandera) {
            $scope.ordenDeMovimientoAcordion = { open: true };
        }
        else {
            $scope.ordenDeMovimientoAcordion = { open: false };
        }
    }
    //#endregion

    //#region ENVIO DE DOCUMENTO POR CORREO
    $scope.inicializarEnvioCorreoElectronico = function () {
        $scope.correosElectronicos = [];
    }

    $scope.agregarCorreoElectronico = function () {
        $scope.correosElectronicos.push($scope.correoElectronico);
        $scope.correoElectronico = '';
        $timeout(function () { $('#correoImput').trigger("change"); }, 100);
    }

    $scope.eliminarCorreoElectronico = function (index) {
        $scope.correosElectronicos.splice(index);
    }

    $scope.enviarCorreoElectronico = function () {
        almacenService.enviarCorreoElectronicoDeMovimientoDeAlmacen({ idMovimiento: $scope.idMovimiento, formato: $scope.formato.Id, correosElectronicos: $scope.correosElectronicos }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema", data.error);
        });
    }

    $scope.limpiarEnvioDeCorrero = function () {
        $scope.correosElectronicos = {};
    }
    //#endregion

    //#region IMPRESION DEL DOCUMENTO DE MOVIMIENTO  
    $scope.imprimirMovimientoDeAlmacen = function () {
        $scope.tamanioComprobante = $scope.formato.Id == 1 ? '80mm' : "210mm";
        var ventanaImpresion = window.open(' ', 'popimpr');
        ventanaImpresion.document.write($scope.verMovimiento.CadenaHtmlDeMovimientoDeAlmacen);
        ventanaImpresion.document.close();
        ventanaImpresion.print();
        ventanaImpresion.close();
    }
    //#endregion
});



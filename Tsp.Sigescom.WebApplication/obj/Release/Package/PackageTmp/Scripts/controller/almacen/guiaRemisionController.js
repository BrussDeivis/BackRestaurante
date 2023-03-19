app.controller('guiaRemisionController', function ($scope, $q, $compile, SweetAlert, $rootScope, $timeout, DTOptionsBuilder, DTColumnDefBuilder, almacenService, maestroService, productoService, compraService, ventaService, centroDeAtencionService, conceptoService) {

    //#region BANDEJA DE GUIAS DE REMISION
    $scope.inicializarBandejaGuiaRemision = function () {
        $scope.cargarParametros();
        $scope.iniciarVariables();
        $scope.cargarColeccionesAsync();
        $scope.obtenerEstablecimientosComerciales().then(function (resultado_) {
            $scope.establecerEstablecimientoComercialPorDefecto();
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
    }

    $scope.cargarParametros = function () {
        $scope.fechaInicio = fechaInicio;
        $scope.fechaFin = fechaFin;
        $scope.idEstablecimientoPorDefecto = idEstablecimientoPorDefecto;
        $scope.idCentroDeAtencionPorDefecto = idCentroDeAtencionPorDefecto;

    }

    $scope.iniciarVariables = function () {
        $scope.establecimientosComerciales = [];
        $scope.centrosDeAtencion = [];
        $scope.conceptoComercial = {};
    }

    $scope.cargarColeccionesAsync = function () {
    }

    $scope.establecerEstablecimientoComercialPorDefecto = function () {
        var establecimientoComercial = Enumerable.from($scope.listaEstablecimientosComerciales).where("$.Id == '" + $scope.idEstablecimientoComercialPorDefecto + "'").toArray()[0];
        $scope.establecimientosComerciales.push(establecimientoComercial != null ? establecimientoComercial : $scope.listaEstablecimientosComerciales[0]);
        $timeout(function () { $('#establecimientoComercial').trigger("change"); }, 100);
        $scope.obtenerCentrosDeAtencionConRolAlmacen().then(function (resultado_) {
            $scope.establecerCentroAtencionConRolAlmacenPorDefecto();
            $scope.listarBandejaGuiasRemision();
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
    }

    $scope.establecerCentroAtencionConRolAlmacenPorDefecto = function () {
        var centroAtencion = Enumerable.from($scope.listaCentrosDeAtencionConRolAlmacen).where("$.Id == '" + $scope.idCentroAtencionPorDefecto + "'").toArray()[0];
        $scope.centrosDeAtencion.push(centroAtencion != null ? centroAtencion : $scope.listaCentrosDeAtencionConRolAlmacen[0]);
        $timeout(function () { $('#centroDeAtencion').trigger("change"); }, 100);
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

    $scope.obtenerCentrosDeAtencionConRolAlmacen = function () {
        if ($scope.establecimientosComerciales == undefined) {
            $scope.centrosDeAtencion = [];
            $timeout(function () { $('#centroDeAtencion').trigger("change"); }, 100);
        } else {
            var defered = $q.defer();
            var promise = defered.promise;
            $scope.idsEstablecimientosComerciales = Enumerable.from($scope.establecimientosComerciales).select("$.Id").toArray();
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

    $scope.listarBandejaGuiasRemision = function () {
        $scope.idsCentrosDeAtencion = [];
        for (var i = 0; i < $scope.centrosDeAtencion.length; i++) {
            $scope.idsCentrosDeAtencion.push($scope.centrosDeAtencion[i].Id)
        }
        almacenService.obtenerGuiasRemision({ idsCentrosDeAtencion: $scope.idsCentrosDeAtencion, desde: $scope.fechaInicio, hasta: $scope.fechaFin }).success(function (data) {
            $scope.guiasRemision = data;
            $timeout(function () { $('#tabla-guia-remision').trigger("change"); }, 100);
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $rootScope.dtOptions.withLightColumnFilter({
        '1': { type: 'text', className: 'form-control padding-left-right-3' },
        '2': { type: 'text', className: 'form-control padding-left-right-3' },
        '3': { type: 'text', className: 'form-control padding-left-right-3' },
        '4': { type: 'text', className: 'form-control padding-left-right-3' },
        '5': { type: 'text', className: 'form-control padding-left-right-3' },
        '6': { type: 'text', className: 'form-control padding-left-right-3' },
        '7': { type: 'text', className: 'form-control padding-left-right-3' },
    });
     


    //#endregion

    //#region VER GUIA REMISION
    $scope.inicializarVerGuiaRemision = function (item) {
        $scope.formato80 = 1;
        $scope.formatoA4 = 2;
        $scope.verGuiaRemision = {};
        $scope.idGuiaRemision = item.Id;
        $scope.obtenerFormatosDeImpresion().then(function (resultado_) {
            $scope.formato = $scope.formatosDeImpresion[0];
            $scope.obtenerGuiaRemision();
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
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

    $scope.obtenerGuiaRemision = function () {
        almacenService.obtenerGuiaRemision({ idGuiaRemision: $scope.idGuiaRemision, formato: $scope.formato.Id }).success(function (data) {
            $scope.verGuiaRemision = data;
            $scope.tamanioComprobante = $scope.formato.Id == 1 ? '80mm' : "210mm";
            document.getElementById("pdfDocumento").innerHTML = $scope.formato.Id === $scope.formato80 ? $scope.verGuiaRemision.CadenaHtmlDeComprobante80 : $scope.verGuiaRemision.CadenaHtmlDeComprobanteA4;
            $timeout(function () { $('#pdfDocumento').trigger("change"); }, 100);
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema", data.error);
        });
    }

    $scope.cargarGuiaRemision = function () {
        $scope.tamanioComprobante = $scope.formato.Id == 1 ? '80mm' : "210mm";
        document.getElementById("pdfDocumento").innerHTML = $scope.formato.Id === $scope.formato80 ? $scope.verGuiaRemision.CadenaHtmlDeComprobante80 : $scope.verGuiaRemision.CadenaHtmlDeComprobanteA4;
        $timeout(function () { $('#pdfDocumento').trigger("change"); }, 100);
    }

    $scope.cerrarVerGuiaRemision = function () {
        $scope.verGuiaRemision = {};
        $scope.formato = {};
        document.getElementById("pdfDocumento").innerHTML = '';
        $timeout(function () { $('#pdfDocumento').trigger("change"); }, 100);
    }
    //#endregion

    //#region ENVIO DE GUIA DE REMISION
    $scope.inicializarEnvio = function () {
        $scope.envio = {};
    }

    $scope.SeleccionarEnvioPorCorreo = function () {
        $scope.envio.CorreoElectronico = '';
        $scope.envio.CorreosElectronicos = [];
    }

    $scope.agregarCorreoElectronico = function () {
        $scope.envio.CorreosElectronicos.push($scope.envio.CorreoElectronico);
        $scope.envio.CorreoElectronico = '';
        $timeout(function () { $('#correoImput').trigger("change"); }, 100);
    }

    $scope.eliminarCorreoElectronico = function (index) {
        $scope.envio.CorreosElectronicos.splice(index);
    }

    $scope.enviarCorreoElectronico = function () {
        almacenService.enviarCorreoElectronicoConDocumento({ idGuiaRemision: $scope.idGuiaRemision, formato: $scope.formato.Id, correosElectronicos: $scope.envio.CorreosElectronicos }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema", data.error);
        });
    }

    $scope.limpiarEnvioDocumento = function () {
        $scope.envio.CorreosElectronicos = [];
        $scope.envio.CorreoElectronico = '';
    }
    //#endregion

    //#region IMPRIMIR GUIA REMISION
    $scope.imprimirGuiaRemision = function () {
        $scope.tamanioComprobante = $scope.formato.Id == 1 ? '80mm' : "210mm";
        let cadenaHtml = $scope.formato.Id === $scope.formato80 ? $scope.verGuiaRemision.CadenaHtmlDeComprobante80 : $scope.verGuiaRemision.CadenaHtmlDeComprobanteA4;
        var ventanaImpresion = window.open(' ', 'popimpr');
        ventanaImpresion.document.write(cadenaHtml);
        ventanaImpresion.document.close();
        ventanaImpresion.print();
        ventanaImpresion.close();
    }
    //#endregion

    //#region INVALIDAR GUIA DE REMISION
    $scope.inicializarInvalidacion = function () {
        $scope.invalidacion = {};
    }

    $scope.invalidarDocumento = function () {
        almacenService.invalidarGuiaRemision({ idGuiaRemision: $scope.verGuiaRemision.Id, observacion: $scope.invalidacion.observacion }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $('#modal-invalidar-documento').modal('hide');
            $('#modal-ver-guia-remision').modal('hide');
            $scope.listarBandejaGuiasRemision();
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }
    //#endregion

    //#region REGISTRO GUIAS DE REMISION
    $scope.inicializarRegistroGuiaRemision = function () {
        $scope.registradorGuiaRemisionApi.NuevoRegistroGuiaRemision();
    } 

    $scope.guardarGuiaRemision = function () {
        almacenService.guardarGuiaRemision({ guiaRemision: $scope.guiaRemision }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $('#modal-registro-guia-remision').modal('hide');
            jsWebClientPrint.print('idMovimiento=' + data.data);
            $scope.registradorGuiaRemisionApi.LimpiarGuiaRemision();
            $scope.listarBandejaGuiasRemision();
        }).error(function (data) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }

    $scope.cancelarGuiaRemision = function () {
        $scope.registradorGuiaRemisionApi.LimpiarGuiaRemision();
        $('#modal-registro-guia-remision').modal('hide');
    }

    $scope.enviarGuiasRemision = function () {
        almacenService.enviarGuiasRemision({ desde: $scope.fechaInicio, hasta: $scope.fechaFin}).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.listarBandejaGuiasRemision();
        }).error(function (data) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }
    $scope.consultarEnvioGuiasRemision = function () {
        almacenService.consultarEnvioGuiasRemision({ }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.listarBandejaGuiasRemision();
        }).error(function (data) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }
});

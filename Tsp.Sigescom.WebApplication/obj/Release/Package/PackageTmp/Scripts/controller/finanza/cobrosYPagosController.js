app.controller('cobrosYPagosController', function ($scope, $rootScope, $timeout, $q, SweetAlert, DTOptionsBuilder, DTColumnDefBuilder, finanzaService, ventaService) {

    $scope.tiposDeComprobantesMasSeries = [];
    $scope.tiposDeComprobantes = [];
    $scope.series = [];
    $scope.emisores = [];
    $scope.pagadoresBeneficiarios = [];
    $scope.modelo = {};
    $scope.tipoMovimiento = {};

    //#region Bandeja de cobros y pagos
    $scope.inicializarBandeja = function () {
        $scope.fechaInicio = fechaInicio;
        $scope.fechaFin = fechaFin;
        $scope.esIngreso = true;
        $scope.listarBandeja();
    }

    $scope.listarBandeja = function () {
        if ($scope.fechaInicio != '' && $scope.fechaFin != '') {
            finanzaService.obtenerCobrosPagos({ esCobro: $scope.esIngreso, desde: $scope.fechaInicio, hasta: $scope.fechaFin }).success(function (data) {
                $scope.cobrosPagos = data;
            }).error(function (data) {
                $scope.messageError(data.error);
            });
        }
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

    //#region Ver Documento 
    $scope.inicializarVerDocumento = function (item) {
        $scope.verDocumento = {};
        $scope.verDocumento.idOperacion = item.Id;
        $scope.verDocumento.serieNumeroComprobante = item.SerieNumeroComprobante;
        $scope.verDocumento.total = item.Total;

        $scope.obtenerFormatosDeImpresion().then(function (resultado_) {
            $scope.formato = $scope.formatosDeImpresion[0];
            $scope.obtenerDocumento(item.Id);
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
    }

    $scope.obtenerDocumento = function (id) {
        finanzaService.obtenerDocumentoIngresoEgreso({ idOperacion: id }).success(function (data) {
            $scope.verDocumento.CadenaHtmlDeComprobante = data.htmlString;
            $scope.verDocumento.AccionInvalidar = data.accionInvalidar;
            $scope.tamanioComprobante = $scope.formato.Id == 1 ? '80mm' : "210mm";
            document.getElementById("pdfDocumento").innerHTML = $scope.verDocumento.CadenaHtmlDeComprobante;
            $timeout(function () { $('#pdfDocumento').trigger("change"); }, 100);
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.obtenerFormatosDeImpresion = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        ventaService.obtenerFormatosDeImpresionSolo80mm({}).success(function (data) {
            $scope.formatosDeImpresion = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.cargarDocumento = function () {
        finanzaService.obtenerHtmlIngresoEgreso({ idOperacion: $scope.verDocumento.idOperacion, formato: $scope.formato.Id }).success(function (data) {
            $scope.tamanioComprobante = $scope.formato.Id == 1 ? '80mm' : "210mm";
            $scope.verDocumento.CadenaHtmlDeComprobante = data;
            document.getElementById("pdfDocumento").innerHTML = data;
            $timeout(function () { $('#pdfDocumento').trigger("change"); }, 100);
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.cerrarVerDocumento = function () {
        $scope.verDocumento = {};
        $scope.formato = {};
        document.getElementById("pdfDocumento").innerHTML = '';
        $timeout(function () { $('#pdfDocumento').trigger("change"); }, 100);
    }
    //#endregion

    //#region Envio de documento por correo
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
        finanzaService.enviarCorreoElectronicoConDocumentoIngresoEgreso({ idOperacion: $scope.verDocumento.idOperacion, formato: $scope.formato.Id, correosElectronicos: $scope.correosElectronicos }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.limpiarEnvioDeCorrero = function () {
        $scope.correosElectronicos = {};
    }
    //#endregion

    //#region Modal INgresoY Egresos
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
        $scope.cargarParametros();
        $scope.inicializacionRealizada = true;
        $scope.validarModal();
    }
    $scope.cargarParametros = () => {
        $scope.tiempoEsperaBusquedaSelector = tiempoEsperaBusquedaSelector;
        $scope.minimoCaracteresBuscarActorComercial = minimoCaracteresBuscarActorComercial;
        $scope.mascaraDeVisualizacionValidacionRegistroCliente = mascaraDeVisualizacionValidacionRegistroCliente;
        $scope.mascaraDeVisualizacionValidacionRegistroProveedor = mascaraDeVisualizacionValidacionRegistroProveedor;
        $scope.mascaraDeVisualizacionValidacionRegistroEmpleado = mascaraDeVisualizacionValidacionRegistroEmpleado;
    }

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
            $scope.listarBandeja();
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
    //#endregion

    //#region Impresion de documento  
    $scope.imprimirDocumento = function () {
        $scope.tamanioComprobante = $scope.formato.Id == 1 ? '80mm' : "210mm";
        var ventanaImpresion = window.open(' ', 'popimpr');
        ventanaImpresion.document.write($scope.verDocumento.CadenaHtmlDeComprobante);
        ventanaImpresion.document.close();
        ventanaImpresion.print();
        ventanaImpresion.close();
    }
    //#endregion

    //#region Invalidar Documento
    $scope.limpiarObservacion = function () {
        $scope.invalidacion = {};
    }

    $scope.invalidarDocumento = function () {
        finanzaService.invalidarMovimientoEconomico({ idOperacion: $scope.verDocumento.idOperacion, observacion: $scope.invalidacion.observacion }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $('#modal-invalidar-documento').modal('hide');
            $('#modal-ver-ingreso-egreso').modal('hide');
            $scope.limpiarObservacion();
            $scope.listarBandeja();
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }
    //#endregion
});
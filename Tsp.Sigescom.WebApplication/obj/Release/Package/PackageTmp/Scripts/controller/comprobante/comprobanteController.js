app.controller('comprobanteController', function ($scope, $q, $rootScope, DTOptionsBuilder, DTColumnDefBuilder, SweetAlert,configuracionService, centroDeAtencionService, configuracionService) {

    /*----------------------------------------MANEJO TIPO DE COMPROBANTE----------------------------------------*/

    $scope.tipoDeComprobante = {};
    $scope.listaTiposDeComprobanteBandeja = [];
    $scope.listaTiposDeTransaccionGenerico = [];

    //INICIALIZADORES
    $scope.nuevoRegistroTipoDeComprobante = function () {
        $scope.tipoDeComprobante = {};
        $scope.obtenerTiposDeTransaccionGenerico();
    }

    $scope.obtenerTiposDeTransaccionGenerico = function () {
        configuracionService.obtenerTiposDeTransaccionGenerico().success(function (data) {
            $scope.listaTiposDeTransaccionGenerico = data;
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema",data.error);
        });
    }

    //GUARDAR TIPO DE COMPROBANTE
    $scope.crearTipoDeComprobante = function () {
        configuracionService.crearTipoDeComprobante({ tipoDeComprobante: $scope.tipoDeComprobante }).success(function (data) {
            SweetAlert.success("Correcto",data.result_description);
            $scope.cerrar('modal-registro-tipo-de-comprobante');
            $scope.obtenerTiposDeComprobanteBandeja();
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema",data.error);
        });
    }

    //OBTENER TIPO DE COMPROBANTE CON TIPO DE TRANSCCION PROPIAS Y DE TERCEROS - BANDEJA
    $scope.obtenerTiposDeComprobanteBandeja = function () {
        configuracionService.obtenerTiposDeComprobanteBandeja().success(function (data) {
            $scope.listaTiposDeComprobanteBandeja = data;
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema",data.error);
        });
    }

    //OBTENER TIPO DE COMPROBANTE CON TIPO DE TRANSCCION PROPIAS Y DE TERCEROS - EDITAR
    $scope.editarTipoDeComprobante = function (idTipoDeComprobante) {
        $scope.nuevoRegistroTipoDeComprobante()
        $scope.tipoDeComprobanteInicial = {};
        configuracionService.obtenerTipoDeComprobante({ idTipoDeComprobante: idTipoDeComprobante }).success(function (data) {
            $scope.tipoDeComprobante = data;
            $scope.tipoDeComprobanteInicial = angular.copy($scope.tipoDeComprobante);
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema",data.error);
        });
    }

    //MANEJO  DE LA TABLA 
    $rootScope.dtOptions.withLightColumnFilter({
        '0': { type: 'text', className: 'form-control padding-left-right-3' },
        '1': { type: 'text', className: 'form-control padding-left-right-3' },
        '2': { type: 'text', className: 'form-control padding-left-right-3' },
        '3': { type: 'text', className: 'form-control padding-left-right-3' },
        '4': { type: 'text', className: 'form-control padding-left-right-3' },
    });

    /*----------------------------------------MANEJO DE SERIE DE COMPROBANTE----------------------------------------*/

    $scope.serieDeComprobante = {};
    $scope.listaCentrosDeAtencionGenerico = [];
    $scope.listaTiposDeComprobanteGenerico = [];
    $scope.listaSeriesDeComprobante = [];

    //INICIALIZADORES
    $scope.nuevoRegistroSerieDeComprobante = function () {
        $scope.serieDeComprobante = {};
        $scope.obtenerEstablecimientosComerciales();
        $scope.obtenerTiposDeComprobante();
    }

    $scope.obtenerEstablecimientosComerciales = function () {
        centroDeAtencionService.obtenerEstablecimientosComerciales().success(function (data) {
            $scope.listaEstablecimientosComerciales = data;
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema",data.error);
        });
    }
    //OBTIENE LOS CENTROS DE ATENCION
    $scope.obtenerCentrosAtencion = function (id) {
        var defered = $q.defer();
        var promise = defered.promise;
        centroDeAtencionService.obtenerCentrosDeAtencionPorEstablecimientoComercial({ idEstablecimientoComercial: id }).success(function (data) {
            $scope.listaCentrosDeAtencion = data;
            defered.resolve();
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema",data.error);
            defered.reject(data);
        });
        return promise;
    }

    //OBTIENE LOS TIPOS DE COMPROBANTES 
    $scope.obtenerTiposDeComprobante = function () {
        configuracionService.obtenerTiposDeComprobanteGenerico().success(function (data) {
            $scope.listaTiposDeComprobante = data;
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema",data.error);
        });
    }

    //GUARDAR SERIE DE COMPROBANTE
    $scope.crearSerieDeComprobante = function () {
        configuracionService.crearSerieDeComprobante({ serieDeComprobante: $scope.serieDeComprobante }).success(function (data) {
            SweetAlert.success("Correcto",data.result_description);
            $scope.cerrar('modal-registro-serie-de-comprobante');
            $scope.obtenerSeriesDeComprobanteBandeja();
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema", data.error);
        });
    }

    //OBTENER SERIES DE COMPROBANTE - BANDEJA 
    $scope.obtenerSeriesDeComprobanteBandeja = function () {
        configuracionService.obtenerSeriesDeComprobanteBandeja().success(function (data) {
            $scope.listaSeriesDeComprobante = data;
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema",data.error);
        });
    }

    //OBTENER SERIE DE COMPROBANTE - EDITAR
    $scope.editarSerieDeComprobante = function (idSerieDeComprobante) {
        $scope.nuevoRegistroSerieDeComprobante();
        $scope.serieDeComprobanteInicial = {};
        configuracionService.obtenerSerieDeComprobante({ idSerieDeComprobante: idSerieDeComprobante }).success(function (data) {
            $scope.obtenerCentrosAtencion(data.EstablecimientoComercial.Id).then(function (resultado_) {
            $scope.serieDeComprobante = data;
            }, function (error) {
                $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
            });
            $scope.serieDeComprobanteInicial = angular.copy($scope.serieDeComprobante);
        }).error(function (data) {
            SweetAlert("Error",data.error);
        });
    }

    //CERRAR MODAL
    $scope.cerrar = function (nombreDelModal) {
        $('#' + nombreDelModal).modal('hide');
    }

});
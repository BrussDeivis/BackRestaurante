app.controller('cajaController', function ($scope, $q, $rootScope, $compile, SweetAlert, restauranteService) {

    $scope.inicializar = function () {
        $scope.cargarParametros();
        document.getElementById('fechaInicio').value = $scope.configuracion.FechaDesdeString;
        document.getElementById('fechaFin').value = $scope.configuracion.FechaHastaString;
        $scope.obtenerAtencionesCerradas();
    }
    $scope.cargarParametros = function () {
        $scope.configuracion = Configuracion;
    }

    $scope.obtenerAtencionesCerradas = function () {
        $scope.fechaInicio = document.getElementById('fechaInicio').value;
        $scope.fechaFin = document.getElementById('fechaFin').value;
        if ($scope.fechaInicio !== '' && $scope.fechaFin !== '') {
            restauranteService.obtenerAtencionesCerradas({ desde: $scope.fechaInicio, hasta: $scope.fechaFin }).success(function (data) {
                $scope.atenciones = data;
            }).error(function (data) {
                SweetAlert.error2(data);
            });
        }
    };
    //$scope.inicializar = function () {
    //    $scope.cargarParametros();
    //    $scope.caja = {
    //        FechaDesde: $scope.configuracion.FechaDesdeString,
    //        FechaHasta: $scope.configuracion.FechaHastaString
    //    };
    //    $scope.obtenerAtencionesCerradas();
    //}

    //$scope.cargarParametros = function () {
    //    $scope.configuracion = Configuracion;
    //}
    
    //$scope.obtenerAtencionesCerradas = function () {
    //    restauranteService.obtenerAtencionesCerradas({ desde: $scope.caja.FechaDesde, hasta: $scope.caja.FechaHasta}).success(function (data) {
    //            $scope.atenciones = data;
    //        }).error(function (data) {
    //            SweetAlert.error2(data);
    //        });
    //};

    $scope.confirmarPagoAtencion = function (atencion) {
        restauranteService.confirmarPagoAtencion({ idAtencion: atencion.Id }).success(data => {
            $scope.obtenerAtencionesCerradas();
            SweetAlert.success("Correcto", data.result_description);
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.iniciarFacturacion = function (atencion) {
        restauranteService.obtenerAtencionDeMesa({ IdMesa: atencion.IdMesa }).success(data => {
            $scope.atencionAFacturar = data;
            $('#modal-facturador-restaurante').modal('show');
            $scope.facturadorRestauranteAPI.SetAtencionAFacturar($scope.atencionAFacturar);
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    };

    $scope.cerrarFacturacion = function (seFacturo) {
        if (seFacturo) {
            $scope.iniciarVisualizadorFacturador($scope.atencionAFacturar);
        }
    }

    $scope.iniciarVisualizadorFacturador = function (atencionAVisualizar) {
        $('#modal-visualizador-facturador').modal('show');
        $scope.visualizadorFacturadorAPI.SetAtencionAVisualizar(atencionAVisualizar);
    }

    $scope.cerrarVisualizador = function () {
        $scope.obtenerAtencionesCerradas();
    }

    $scope.verAtencion = function (item) {
        $scope.atencionAVisualizar = item;
        $scope.obtenerDocumentos(item).then(function (resultado_) {
            $scope.atencionAVisualizar.Mesa = { Nombre: item.NombreMesa };
            $scope.atencionAVisualizar.ImporteAtencion = parseFloat(item.Total);
            $scope.atencionAVisualizar.documentoAtencion = $scope.atencionData.objeto;
            $scope.atencionAVisualizar.documentosVenta = $scope.atencionData.information;
            $scope.iniciarVisualizadorFacturador($scope.atencionAVisualizar);
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
    }

    $scope.obtenerDocumentos = function (item) {
        var defered = $q.defer();
        var promise = defered.promise;
        if (item.Facturado) {
            restauranteService.obtenerDocumentosAtencion({ idAtencion: item.Id }).success(data => {
                $scope.atencionData = data;
                defered.resolve();
            }).error(function (data) {
                SweetAlert.error2(data);
                defered.reject(data);
            });
        } else {
            restauranteService.obtenerAtencionHtml({ idAtencion: item.Id }).success(data => {
                $scope.atencionData = {};
                $scope.atencionData.objeto = data;
                $scope.atencionData.information = [];
                defered.resolve();
            }).error(function (data) {
                SweetAlert.error2(data);
                defered.reject(data);
            });
        }
        return promise;
    };
});





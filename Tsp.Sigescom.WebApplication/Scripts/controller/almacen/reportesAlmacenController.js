app.controller('reportesAlmacenController', function ($scope, $q, $compile, SweetAlert, $rootScope, $timeout, DTOptionsBuilder, DTColumnDefBuilder) {
    $scope.inicializar = function () {
        $scope.data = reportData;
        $scope.reporteador = {
            InventariosPorFecha: {
                FechaHasta: new Date($scope.data.FechaHastaDefault),
                EstablecimientoSeleccionado: $scope.data.Establecimientos[0],
                FamiliasSeleccionadas: [],
                SeleccionarTodasLasFamilias: true,
                Inventario: {
                    ActivarBotonVer: false
                },
                InventarioSemaforo: {
                    ActivarBotonVer: false,
                    Semaforo: {
                        Bajo: true,
                        Normal: true,
                        Alto:true,
                    }
                },
                InventarioValorizado: {
                    ActivarBotonVer: false
                },
            },
            InventariosActuales: {
                AlmacenesSeleccionados: [],
                SeleccionarTodosLosAlmacenes: true,
                FamiliasSeleccionadas: [],
                SeleccionarTodasLasFamilias: true,
                Inventario: {
                    ActivarBotonVer: false
                },
                InventarioSemaforo: {
                    ActivarBotonVer: false,
                    Semaforo: {
                        Bajo: true,
                        Normal: true,
                        Alto: true,
                    }
                },
                InventarioValorizado: {
                    ActivarBotonVer: false
                },
            },
            Movimientos: {
                EstablecimientoSeleccionado: $scope.data.Establecimientos[0],
                FechaDesde: new Date($scope.data.FechaDesdeDefault),
                FechaHasta: new Date($scope.data.FechaHastaDefault),
                FamiliasSeleccionadas: [],
                SeleccionarTodasLasFamilias: true,
                Entradas: {
                    ActivarBotonVer: false
                },
                Salidas: {
                    ActivarBotonVer: false
                },
                Vencimientos: {
                    ActivarBotonVer: false
                }
            },
            Kardex: {
                EstablecimientoSeleccionado: $scope.data.Establecimientos[0],
                FechaDesde: new Date($scope.data.FechaDesdeDefault),
                FechaHasta: new Date($scope.data.FechaHastaDefault),
                Fisico: {
                    ActivarBotonVer: false
                },
                Valorizado: {
                    ActivarBotonVer: false
                }
            },

            SeccionSeleccionada: 'seccion1'
            
        };
        
        $scope.InventariosPorFecha_SetAlmacenPorDefecto();
        $scope.InventariosPorFecha_actualizarURLs();
        $scope.InventariosActuales_actualizarURLs();
        $scope.Movimientos_SetAlmacenPorDefecto();
        $scope.Movimientos_actualizarURLs();
        $scope.Kardex_SetAlmacenPorDefecto();
        $scope.Kardex_actualizarURLs();


    }

    InventariosPorFechaAlmacenController($scope, $q, $compile, SweetAlert, $rootScope, $timeout, DTOptionsBuilder, DTColumnDefBuilder);
    InventariosActualesAlmacenController($scope, $q, $compile, SweetAlert, $rootScope, $timeout, DTOptionsBuilder, DTColumnDefBuilder);
    MovimientosAlmacenController($scope, $q, $compile, SweetAlert, $rootScope, $timeout, DTOptionsBuilder, DTColumnDefBuilder);
    KardexAlmacenController($scope, $q, $compile, SweetAlert, $rootScope, $timeout, DTOptionsBuilder, DTColumnDefBuilder);


    //ESTABLECER DATOS POR DEFECTO
    $scope.establecerEstablecimientoComercialPorDefecto = function () {
        $scope.establecimiento = $scope.establecimientos[0];
    }
    $scope.establecerCentroDeAtencionPorDefecto = function () {
        $scope.centroDeAtencion = $scope.centrosDeAtencion[0];
    }


    $scope.ConcatenarFamilias = function (SeleccionarTodasLasFamilias, FamiliasSeleccionadas) {
        var idsFamilias = "";
        var nombresFamilias = "";
        var familias = "";
        if (!SeleccionarTodasLasFamilias) {
            nombresFamilias = "nombresFamilias=";
            for (var i = 0; i < FamiliasSeleccionadas.length; i++) {
                idsFamilias += "idsFamilias=" + FamiliasSeleccionadas[i].Id + "&";
                nombresFamilias += FamiliasSeleccionadas[i].Nombre + " | ";
            }
            familias += idsFamilias + nombresFamilias + "&";
        }
        familias += "todasLasFamilias=" + SeleccionarTodasLasFamilias
        return familias;
    }

    $scope.obtenerConceptosGenerico = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        maestroService.obtenerFamiliasConceptosComercialesVigentes({}).success(function (data) {
            $scope.conceptos = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.obtenerMercaderiasPorConceptoBasicoGenerico = function () {
        if ($scope.conceptoBasicoSeleccionado.Id > 0) {
            productoService.obtenerMercaderiasPorConceptoBasicoGenerico({ idConceptoBasico: $scope.conceptoBasicoSeleccionado.Id }).success(function (data) {
                $scope.mercaderias = data;
            }).error(function (data) {
                $scope.messageError(data.error);
            });
        }
    }
    $scope.obtenerCentrosDeAtencionGenerico = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        centroDeAtencionService.obtenerCentrosDeAtencionPorEstablecimientoComercial({ idEstablecimientoComercial: $scope.establecimiento.Id }).success(function (data) {
            $scope.centrosDeAtencion = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);

        });
        return promise;
    }

    $scope.obtenerCentrosDeAtencionConRolAlmacenVigentesConEstablecimientoComercialPromise = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        centroDeAtencionService.obtenerCentrosDeAtencionConRolAlmacenVigentesConEstablecimientoComercial().success(function (data) {
            $scope.listaAlmacenes = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    /*---------------------------------------- REPORTE  CONCEPTO BASICO ----------------------------------------*/

    $scope.actualizarURLReporteConceptoBasico = function () {
        var idsEntidadInterna = "";
        if ($scope.almacenes) {
            for (var i = 0; i < $scope.almacenes.length; i++) {
                idsEntidadInterna += "&idsEntidadInterna=" + $scope.almacenes[i].Id;
            }
        }
        $scope.URLReporteConceptoBasico = URL_ + "/Almacen/ObtenerReporteDeSalidasDeAlcohol?fechaInicio=" + $scope.fechaInicio + ' ' + $scope.horaInicio + "&fechaFin=" + $scope.fechaFin + ' ' + $scope.horaFin + idsEntidadInterna;
    }
       
});




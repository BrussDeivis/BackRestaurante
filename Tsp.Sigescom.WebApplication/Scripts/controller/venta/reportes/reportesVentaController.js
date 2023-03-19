app.controller('reportesVentaController', function ($scope, $q, $compile, SweetAlert, $rootScope, $timeout, DTOptionsBuilder, DTColumnDefBuilder, actorComercialService) {

    $scope.inicializar = function () {
        $scope.data = reportData;
        $scope.reporteador = {
            PorGrupos: {
                FechaDesde: new Date($scope.data.FechaDesdeDefault),
                FechaHasta: new Date($scope.data.FechaHastaDefault),
                EstablecimientoSeleccionado: $scope.data.Establecimientos[0],

                VentasPorFamiliasGrupos: {
                    ActivarBotonVer: false,
                    FamiliasSeleccionadas: [],
                    SeleccionarTodasLasFamilias: true,
                    GruposSeleccionados: [],
                    SeleccionarTodosLosGrupos: true,
                },
                VentasPorGrupos: {
                    ActivarBotonVer: false,
                    GruposSeleccionados: [],
                    SeleccionarTodosLosGrupos: true,
                },
                VentasPorGrupoDetallado: {
                    ActivarBotonVer: false,
                },
            },
             
            SeccionSeleccionada: 'seccion1'
        };

        $scope.PorGrupos_SetPuntoVentaPorDefecto();
        $scope.PorGrupos_ActualizarURLs();
    }

    PorGruposController($scope, $q, $compile, SweetAlert, $rootScope, $timeout, DTOptionsBuilder, DTColumnDefBuilder, actorComercialService);

});




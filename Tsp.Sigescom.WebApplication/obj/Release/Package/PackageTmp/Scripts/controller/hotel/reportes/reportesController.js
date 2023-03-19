app.controller('reportesController', function ($scope, $q, $compile, SweetAlert, $rootScope, $timeout, DTOptionsBuilder, DTColumnDefBuilder) {
    $scope.inicializar = function () {
        $scope.data = reportData;
        $scope.reporteador = {
            Huespedes: {
                EstablecimientoSeleccionado: $scope.data.Establecimientos[0],
                FechaDesde: new Date($scope.data.FechaDesdeDefault),
                FechaHasta: new Date($scope.data.FechaHastaDefault),
                RegistroHuespedes: {
                    ActivarBotonVer: false
                }
            },
            Reservas: {
                EstablecimientoSeleccionado: $scope.data.Establecimientos[0],
                FechaDesde: new Date($scope.data.FechaDesdeDefault),
                FechaHasta: new Date($scope.data.FechaHastaDefault),
                TiposHabitacionSeleccionados: [],
                SeleccionarTodosTiposHabitacion: true,
                Confirmadas: {
                    ActivarBotonVer: false
                }
            },
            Atenciones: {
                EstablecimientoSeleccionado: $scope.data.Establecimientos[0],
                FechaDesde: new Date($scope.data.FechaDesdeDefault),
                FechaHasta: new Date($scope.data.FechaHastaDefault),
                Ingresos: {
                    ActivarBotonVer: false
                },
                Salidas: {
                    ActivarBotonVer: false
                },
                Anuladas: {
                    ActivarBotonVer: false
                },
                Mincetur: {
                    ActivarBotonVer: false
                }
            },
            Facturacion: {
                EstablecimientoSeleccionado: $scope.data.Establecimientos[0],
                FechaDesde: new Date($scope.data.FechaDesdeDefault),
                FechaHasta: new Date($scope.data.FechaHastaDefault),
                Facturadas: {
                    ActivarBotonVer: false
                },
                NoFacturadas: {
                    ActivarBotonVer: false
                },
                Incidentes: {
                    ActivarBotonVer: false
                }
            },
            SeccionSeleccionada: 'seccion1'
        };

        $scope.diferenciaDiasEntreFechas = function (fechaInicio, fechaFin) {
            var millisecondsPerDay = 1000 * 60 * 60 * 24;
            var millisBetween = fechaFin.getTime() - fechaInicio.getTime();
            var days = millisBetween / millisecondsPerDay;
            return Math.floor(days);
        }
        
        $scope.Huespedes_ActualizarURLs();
        $scope.Reservas_ActualizarURLs();
        $scope.Atenciones_ActualizarURLs();
        $scope.Facturacion_ActualizarURLs();
    }

    HuespedesController($scope, $q, $compile, SweetAlert, $rootScope, $timeout, DTOptionsBuilder, DTColumnDefBuilder);
    ReservasController($scope, $q, $compile, SweetAlert, $rootScope, $timeout, DTOptionsBuilder, DTColumnDefBuilder);
    AtencionesController($scope, $q, $compile, SweetAlert, $rootScope, $timeout, DTOptionsBuilder, DTColumnDefBuilder);
    FacturacionController($scope, $q, $compile, SweetAlert, $rootScope, $timeout, DTOptionsBuilder, DTColumnDefBuilder);


       
});




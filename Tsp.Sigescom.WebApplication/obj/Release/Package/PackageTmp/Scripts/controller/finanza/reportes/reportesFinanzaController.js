app.controller('reportesFinanzaController', function ($scope, $q, $compile, SweetAlert, $rootScope, $timeout, DTOptionsBuilder, DTColumnDefBuilder, clienteService, proveedorService, actorComercialService, centroDeAtencionService) {

    $scope.inicializar = function () {
        $scope.data = reportData;
        $scope.mediosPago = $scope.data.MediosPago;
        $scope.reporteador = {
            MovimientosCaja: {
                FechaDesde: new Date($scope.data.FechaDesdeDefault),
                FechaHasta: new Date($scope.data.FechaHastaDefault),
                EstablecimientoSeleccionado: $scope.data.Establecimientos[0],

                Ingresos: {
                    ActivarBotonVer: false,
                    MediosPagoSeleccionados: [],
                    SeleccionarTodosLosMediosPago: true,
                    OperacionesSeleccionadas: [],
                    SeleccionarTodasLasOperaciones: true,
                },
                Egresos: {
                    ActivarBotonVer: false,
                    MediosPagoSeleccionados: [],
                    SeleccionarTodosLosMediosPago: true, 
                    OperacionesSeleccionadas: [],
                    SeleccionarTodasLasOperaciones: true,
                },
                Flujo: {
                    ActivarBotonVer: false,
                },
            },

            MovimientosCajas: {
                FechaDesde: new Date($scope.data.FechaDesdeDefault),
                FechaHasta: new Date($scope.data.FechaHastaDefault),
                CajasSeleccionadas: [],
                SeleccionarTodasLasCajas: true,

                CobrosClientes: {
                    ActivarBotonVer: false,
                    SeleccionarTodosLosClientes: true,
                    ClientesSeleccionados: [],
                },
                PagosProveedores: {
                    ActivarBotonVer: false,
                    SeleccionarTodosLosProveedores: true,
                    ProveedoresSeleccionados: [],
                },
            },

            PorCobrarPagar: {

                PorCobrarClientes: {
                    ActivarBotonVer: false,
                },
                PorPagarProveedores: {
                    ActivarBotonVer: false,
                },
                PorCobrarPorCliente: {
                    ActivarBotonVer: false,
                    SeleccionarTodosLosClientes: true,
                    ClientesSeleccionados: [],
                },
                PorPagarPorProveedor: {
                    ActivarBotonVer: false,
                    SeleccionarTodosLosProveedores: true,
                    ProveedoresSeleccionados: [],
                },
                PorCobrarGrupos: {
                    ActivarBotonVer: false,
                    SeleccionarTodosLosGrupos: true,
                    GruposSeleccionados: [],
                },
                PorCobrarGrupoDetallado: {
                    ActivarBotonVer: false,
                },
            },

            EstadoCuenta: {
                FechaDesde: new Date($scope.data.FechaDesdeDefault),
                FechaHasta: new Date($scope.data.FechaHastaDefault),

                EstadoCuentaPorCliente: {
                    ActivarBotonVer: false,
                },
            },

            SeccionSeleccionada: 'seccion1'
        };

        $scope.MovimientosCaja_SetCajaPorDefecto();
        $scope.MovimientosCaja_actualizarURLs();
        $scope.MovimientosCajas_actualizarURLs();
        $scope.PorCobrarPagar_actualizarURLs();
        $scope.EstadoCuenta_actualizarURLs();

    }

    MovimientosCajaController($scope, $q, $compile, SweetAlert, $rootScope, $timeout, DTOptionsBuilder, DTColumnDefBuilder);
    MovimientosCajasController($scope, $q, $compile, SweetAlert, $rootScope, $timeout, DTOptionsBuilder, DTColumnDefBuilder, centroDeAtencionService);
    PorCobrarPagarController($scope, $q, $compile, SweetAlert, $rootScope, $timeout, DTOptionsBuilder, DTColumnDefBuilder, actorComercialService);
    EstadoCuentaController($scope, $q, $compile, SweetAlert, $rootScope, $timeout, DTOptionsBuilder, DTColumnDefBuilder);

    $scope.obtenerClientes = function () {
        clienteService.obtenerClientesGenericoSinParentRol().success(function (data) {
            $scope.clientes = data;
        }).error(function (data) {
            $scope.messageError(data.error);    
        });
    }

    $scope.obtenerProveedores = function () {
        proveedorService.obtenerProveedoresGenerico().success(function (data) {
            $scope.proveedores = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.obtenerClientes();
    $scope.obtenerProveedores();
});




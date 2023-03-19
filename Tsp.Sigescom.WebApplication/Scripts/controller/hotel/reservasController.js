app.controller('reservasController', function ($scope, $q, $rootScope, SweetAlert, DTOptionsBuilder, DTColumnDefBuilder, hotelService, centroDeAtencionService) {


    $scope.inicializar = function () {
        $scope.cargarParametros();
        $scope.limpiarRegistro();
        $scope.obtenerReservasBandeja();
        $scope.inicializarComponentes();
    }

    $scope.cargarParametros = function () {
        $scope.fechaDesde = fechaDesde;
        $scope.fechaHasta = fechaHasta;
        $scope.establecimientos = establecimientos;
        $scope.establecimiento = establecimientoSesion;
        $scope.usuarioTieneRolAdministradorDeNegocio = usuarioTieneRolAdministradorDeNegocio;
    }

    $scope.limpiarRegistro = function () {
        $scope.reservas = [];
        $scope.reserva = {};
    }

    $scope.inicializarComponentes = function () {
        $scope.inicializacionRealizada = true;
    }

    $scope.obtenerReservasBandeja = function () {
        hotelService.obtenerReservasBandeja({ idEstablecimiento: $scope.establecimiento.Id, fechaDesde: $scope.fechaDesde, fechaHasta: $scope.fechaHasta }).success(function (data) {
            $scope.reservas = data;
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $rootScope.dtOptions.withLightColumnFilter({
        '0': { type: 'text', className: 'form-control padding-left-right-3' },
        '1': { type: 'text', className: 'form-control padding-left-right-3' },
        '2': { type: 'text', className: 'form-control padding-left-right-3' },
        '3': { type: 'text', className: 'form-control padding-left-right-3' },
        '4': { type: 'text', className: 'form-control padding-left-right-3' },
        '5': { type: 'text', className: 'form-control padding-left-right-3' },
        '6': { type: 'text', className: 'form-control padding-left-right-3' },
        '7': { type: 'text', className: 'form-control padding-left-right-3' },
        '8': { type: 'text', className: 'form-control padding-left-right-3' },
        '9': { type: 'text', className: 'form-control padding-left-right-3' },
        '10': { type: 'text', className: 'form-control padding-left-right-3' },
    });

    $scope.nuevaReserva = function () {
        $('#modal-registrador-reserva').modal('show');
        $scope.registradorReservaAPI.NuevaReserva();
    }

    $scope.reservaAgregada = function () {
        $scope.obtenerReservasBandeja();
    }
});
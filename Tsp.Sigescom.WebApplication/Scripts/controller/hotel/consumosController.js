app.controller('consumosController', function ($scope, $q, $rootScope, SweetAlert, DTOptionsBuilder, DTColumnDefBuilder, hotelService, centroDeAtencionService) {


    $scope.inicializar = function () {
        $scope.cargarParametros();
        $scope.establecerDatosPorDefecto();
        $scope.obtenerConsumos();
    }

    $scope.cargarParametros = function () {
        $scope.establecimientos = Establecimientos;
        $scope.establecimiento = EstablecimientoSesion;
        $scope.usuarioTieneRolAdministradorDeNegocio = UsuarioTieneRolAdministradorDeNegocio;
        $scope.fechaActual = fechaActual;
    }

    $scope.establecerDatosPorDefecto = function () {
        $scope.fechaDesde = $scope.fechaActual;
        $scope.fechaHasta = $scope.fechaActual;
        $scope.buscarEnTabla = '';
    }

    $scope.obtenerConsumos = function () {
        hotelService.obtenerConsumos({ idEstablecimiento: $scope.establecimiento.Id, fechaDesde: $scope.fechaDesde, fechaHasta: $scope.fechaHasta }).success(function (data) {
            $scope.consumos = data;
            $scope.consumosEnTabla = [...$scope.consumos];
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.invalidarConsumo = (consumo) => {
        hotelService.invalidarConsumo({ idConsumo: consumo.Id }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.obtenerConsumos();
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.filtrarTabla = () => {
        let consumosTemp = []
        if ($scope.buscarEnTabla !== undefined) {
            if ($scope.buscarEnTabla !== '') {

                for (var i = 0; i < $scope.consumos.length; i++) {
                    let existe = $scope.buscarenObjeto($scope.consumos[i]);
                    if (existe) {
                        consumosTemp.push($scope.consumos[i]);
                    }
                }
                $scope.consumosEnTabla = [...consumosTemp];
            }
            else {
                $scope.consumosEnTabla = [...$scope.consumos];
            }
        }
    }

    $scope.buscarenObjeto = (consumo) => {
        let arrayTemp = [];
        let stringTemp = "";
        for (var property in consumo) {
            arrayTemp.push(String(consumo[property]).toUpperCase());
        }
        stringTemp = arrayTemp.toString();
        return stringTemp.includes($scope.buscarEnTabla.toUpperCase());
    }

    $rootScope.dtOptions.withLightColumnFilter({
        '0': { type: 'text', className: 'form-control padding-left-right-3' },
        '1': { type: 'text', className: 'form-control padding-left-right-3' },
        '2': { type: 'text', className: 'form-control padding-left-right-3' },
        '3': { type: 'text', className: 'form-control padding-left-right-3' },
        '4': { type: 'text', className: 'form-control padding-left-right-3' },
        '5': { type: 'text', className: 'form-control padding-left-right-3' },

    });

    $scope.nuevaReserva = function () {
        $('#modal-registrador-reserva').modal('show');
        $scope.registradorReservaAPI.NuevaReserva();
    }

    $scope.consumoGuardado = function () {
        $scope.obtenerConsumos();
    }

    $scope.nuevoConsumo = function () {
        $('#modal-registrador-consumo').modal('show');
        $scope.registradorConsumoAPI.NuevoConsumo();
    }
}); 
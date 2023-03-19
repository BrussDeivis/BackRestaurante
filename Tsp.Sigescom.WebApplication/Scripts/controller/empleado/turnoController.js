app.controller('turnoController', function ($scope, $q, $rootScope, SweetAlert, DTOptionsBuilder, DTColumnDefBuilder, centroDeAtencionService, empleadoService) {

    //************************  MANEJO DE BANDEJA *******************//

    //Manejo de busqueda en la tabla 
    $rootScope.dtOptions.withLightColumnFilter({
        '1': { type: 'text', className: 'form-control padding-left-right-3' },
        '2': { type: 'text', className: 'form-control padding-left-right-3' },
        '3': { type: 'text', className: 'form-control padding-left-right-3' },
        '4': { type: 'text', className: 'form-control padding-left-right-3' },
        '5': { type: 'text', className: 'form-control padding-left-right-3' },
        '6': { type: 'text', className: 'form-control padding-left-right-3' },
    });

    $scope.obtenerTurnosBandeja = function () {
        empleadoService.obtenerTurnosBandeja().success(function (data) {
            $scope.listaTurnos = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    //********************** GESTION DE TURNO ****************//
    $scope.nuevoRegistroTurno = function () {
        $scope.turno = {};
        $scope.obtenerEstablecimientosComerciales();
        $scope.obtenerEmpleados();
    }

    $scope.obtenerEstablecimientosComerciales = function () {
        centroDeAtencionService.obtenerEstablecimientosComerciales().success(function (data) {
            $scope.listaEstablecimientosComerciales = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.obtenerCentrosAtencion = function (id) {
        var defered = $q.defer();
        var promise = defered.promise;
        centroDeAtencionService.obtenerCentrosDeAtencionPorEstablecimientoComercial({ idEstablecimientoComercial: id }).success(function (data) {
            $scope.listaCentrosDeAtencion = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.obtenerEmpleados = function () {
        empleadoService.obtenerEmpleadosGenerico().success(function (data) {
            $scope.listaEmpleados = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.guardarTurno = function () {
        empleadoService.guardarTurno({ turno: $scope.turno }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.cerrar("modal-registro-turno");
            $scope.obtenerTurnosBandeja();
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema", data.error);
        });
    }

    $scope.editarTurno = function (idturno) {
        $scope.nuevoRegistroTurno();
        $scope.turnoInicial = {};
        empleadoService.obtenerTurno({ idturno: idturno }).success(function (data) {
            $scope.obtenerCentrosAtencion(data.EstablecimientoComercial.Id).then(function (resultado_) {
                $scope.turno = data;
                var date;
                date = $scope.turno.Desde.slice($scope.turno.Desde.indexOf("(") + 1, $scope.turno.Desde.indexOf(")"));
                $scope.turno.Desde = $scope.formatDate(new Date(+date), "ES");
                $("#desde").datepicker('setDate', $scope.turno.Desde);
                date = $scope.turno.Hasta.slice($scope.turno.Hasta.indexOf("(") + 1, $scope.turno.Hasta.indexOf(")"));
                $scope.turno.Hasta = $scope.formatDate(new Date(+date), "ES");
                $("#hasta").datepicker('setDate', $scope.turno.Hasta);
            }, function (error) {
                $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
            });
            $scope.turnoInicial = angular.copy($scope.turno);
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.cerrar = function (nombreDelModal) {
        $('#' + nombreDelModal).modal('hide');
    }

});


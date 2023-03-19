app.controller('ambienteController', function ($scope, $q, $timeout, hotelService, centroDeAtencionService, $rootScope, SweetAlert) {

    //#region VARIABLES
    $scope.ambiente = {Establecimiento: {}};
    $scope.ambientes = [];
    $scope.establecimientos = [];
    $scope.establecimientoSeleccionado = {};
    $scope.establecimientoSeleccionadoModal = {};
    $scope.idEstablecimientoPorDefecto = idEstablecimientoPorDefecto;
    //#endregion

    $rootScope.dtOptions.withLightColumnFilter({
        '0': { type: 'text', className: 'form-control padding-left-right-3' },
        '1': { type: 'text', className: 'form-control padding-left-right-3' },
        '2': { type: 'text', className: 'form-control padding-left-right-3' }
    });

    $scope.inicializar = function () {
        $scope.obtenerEstablecimientosComerciales().then(function (resultado_) {
            $scope.obtenerAmbientes();
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al traer ambientes por establecimiento" + error;
        });
    }

    $scope.limpiar = () => {
        $scope.ambiente = { Establecimiento: {} };
        $scope.establecimientoSeleccionadoModal = {}
    }

    $scope.obtenerEstablecimientosComerciales = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        centroDeAtencionService.obtenerEstablecimientosComerciales({}).success(function (data) {
            $scope.establecimientos = data;
            let establecimiento = $scope.establecimientos.find(est => est.Id == $scope.idEstablecimientoPorDefecto);
            $scope.establecimientoSeleccionado = establecimiento != undefined ? establecimiento : $scope.establecimientos[0];
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    };

    $scope.obtenerAmbientes = () => {
        hotelService.obtenerAmbientesHotelPorEstablecimiento({ idEstablecimiento: $scope.establecimientoSeleccionado.Id}).success(function (data) {
            $scope.ambientes = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    };

    $scope.guardarAmbiente = () => {
        $scope.ambiente.EsVigente = $scope.ambiente.EsVigente == undefined ? true : $scope.ambiente.EsVigente;
        $scope.establecimientoSeleccionado = $scope.establecimientoSeleccionadoModal;
        $scope.ambiente.Establecimiento = $scope.establecimientoSeleccionadoModal;
        hotelService.guardarAmbiente({ ambiente: $scope.ambiente }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.cerrarModal('modal-nuevo-ambiente');
            $scope.limpiar();
            $scope.obtenerAmbientes();
            $timeout(function () { $('#establecimientoAmbiente').trigger("change"); }, 100);
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }
    
    $scope.editarAmbiente = (ambiente) => {
        $scope.ambiente = angular.copy(ambiente);
        $scope.establecimientoSeleccionadoModal = $scope.establecimientoSeleccionado;
        $scope.abrirModalNuevoAmbiente();
    }

    $scope.cambiarEstadoAmbiente = (id) => {
        console.log($scope.establecimientoSeleccionado);
        hotelService.cambiarEsVigenteAmbienteHotel({ id: id }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.cerrarModal('modal-nuevo-habitacion');
            $scope.obtenerAmbientes();
        }).error(function (data) {
            SweetAlert.error2(data);
        });
        $scope.limpiar();
    }
    $scope.abrirNuevoAmbiente = () => {
        $scope.establecimientoSeleccionadoModal = $scope.establecimientoSeleccionado;
        $scope.abrirModalNuevoAmbiente();
    }
    //#region MODAL NUEVO AMBIENTE
   
    //#endregion
    $scope.abrirModal = function (nombreDelModal) {
        $('#' + nombreDelModal).modal('show');
        
    }
    $scope.abrirModalNuevoAmbiente = function () {
        $('#' + 'modal-nuevo-ambiente').modal('show');
    }
    $scope.cerrarModal = function (nombreDelModal) {
        $scope.limpiar();
        $('#' + nombreDelModal).modal('hide');
    }
});

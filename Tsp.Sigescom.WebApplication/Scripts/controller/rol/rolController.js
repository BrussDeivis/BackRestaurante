
app.controller('rolController', function ($scope, $rootScope, $timeout, DTOptionsBuilder, DTColumnDefBuilder, rolService) {

    $scope.tipoDeTransaccion = {};
    $scope.listaTiposDeTransaccionGenerico = [];
    $scope.listaAccionesDeNegocioPorTipoDeTransaccion = [];
    $scope.listaTiposDeTransaccionBandeja = [];

    //INICIALIZADORES
    $scope.nuevoRegistroRolDeNegocio = function () {
        $scope.rol = {};
        $scope.rolesDeNegocioGenerico();
    }

    //CREAR TIPO DE TRANSACCION
    $scope.crearRol = function () {
        console.log("Este tipoDeTransaccion voy a registrar");
        console.log($scope.rol);
            rolService.crearRol({ rol: $scope.rol }).success(function (data) {
            $scope.messageSuccess(data.result_description);
            $scope.cerrar("modal-registro-tipo-de-transaccion");
            $scope.obtenerTiposDeTransaccionBandeja();
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    //OBTENER ROLES DE NEGOCIO 
    $scope.rolesDeNegocioGenerico = function () {
        rolService.obtenerRolesDeNegocioGenerico().
            success(function (data) {
                $scope.listaRolesGenerico = data;
                console.log("Lista de Roles de Negocio");
            //console.log($scope.listaTiposDeTransaccionBandeja);
            }).error(function (data) {
                $scope.messageError(data.error);
            });
    }

    //OBTENER TIPO DE TRANSACCION - EDITAR
    $scope.editarTipoDeTransaccion = function (idTipoDeTransaccion) {
        $scope.nuevoRegistroTipoDeTransaccion(idTipoDeTransaccion);
        $scope.tipoDeTransaccionInicial = {};
        configuracionService.obtenerTipoDeTransaccion({ idTipoDeTransaccion: idTipoDeTransaccion }).success(function (data) {
            $scope.tipoDeTransaccion = data;
            console.log('Este Tipo De Transaccion Voy A Editar');
            console.log($scope.tipoDeTransaccion);
            configuracionService.obtenerAccionesDeNegocioPorTipoDeTransaccionEditar({ accionesDeNegocioPorTipoDeTransaccion: $scope.tipoDeTransaccion.AccionesDeNegocioPorTipoDeTransaccion }).success(function (data) {
                $scope.listaAccionesDeNegocioPorTipoDeTransaccion = data;
                console.log("Esto Es Lista AccionesDeNegocioPorTipoDeTransaccion Editar");
                console.log($scope.listaAccionesDeNegocioPorTipoDeTransaccion);
            }).error(function (data) {
                $scope.messageError(data.error);
            });
            $scope.tipoDeTransaccionInicial = angular.copy($scope.tipoDeTransaccion);
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }


    //CERRAR MODAL
    $scope.cerrar = function (nombreDelModal) {
        $('#' + nombreDelModal).modal('hide');
    }

    //MANEJO  DE LA TABLA 
    $rootScope.dtOptions.withLightColumnFilter({
        '0': { type: 'text', className: 'form-control padding-left-right-3' },
        '1': { type: 'text', className: 'form-control padding-left-right-3' },
        '2': { type: 'text', className: 'form-control padding-left-right-3' },
        '3': { type: 'text', className: 'form-control padding-left-right-3' },
        '4': { type: 'text', className: 'form-control padding-left-right-3' },

    });
});
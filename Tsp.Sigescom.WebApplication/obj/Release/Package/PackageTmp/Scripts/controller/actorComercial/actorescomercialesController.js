app.controller('actorescomercialesController', function ($scope, $compile, $timeout, blockUI, DTOptionsBuilder, DTColumnDefBuilder, actorComercialService, maestroService) {
    $scope.elementos = { lista: [] };
    $scope.rol = {};
    $scope.modelo = {};
   
    $scope.listarElementos = function (index) {
        actorComercialService.listar({ idRol: $('#model-idRol').text() }).success(function (data) {
            $scope.elementos.lista = data;
        });
    }

    $scope.cargar = function (item) {
        $scope.modelo = item;
    }

    $scope.eliminar = function () {
        actorComercialService.eliminar($scope.modelo).success(function (data) {
            $scope.messageSuccess(data.result_description);
            $scope.listarElementos();
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.newTab = function (url, id) {
        //console.debug(id);
        $("#editar").attr('href', url + "&idActor=" + id);
        $("#editar")[0].click();
    }

    $scope.listarElementos();
});
app.controller('categoriaController', function ($scope, $timeout, DTOptionsBuilder, DTColumnBuilder, conceptoService) {
    $scope.categorias = { Lista: [] };
    $scope.categoria = {};
    $scope.categoriaTmp = {};
    $scope.idCategoriaNula = idCategoriaNula;
    
    $scope.verCategorias = function () {
        conceptoService.verCategorias({}).success(function (data) {
            $scope.categorias.Lista = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
    $scope.nuevoRegistro = function () {
        $scope.categoria = { DetalleMaestroPadre: $scope.categorias.Lista.find(c => c.Id === $scope.idCategoriaNula) };
        $timeout(function () { $('#categoriaPadre').trigger("change"); }, 100);
    }
    $scope.editarCategoria = function (categoria) {
        $scope.categoria = {};
        $scope.categoria = angular.copy(categoria);
        $timeout(function () { $('#categoriaPadre').trigger("change"); }, 100);
    }
    $scope.nuevaCategoria = function (categoria) {
        conceptoService.guardarCategoria({ categoria : categoria }).success(function (data){
            $scope.messageSuccess(data.result_description);
            $('#modal-categoria').modal('hide');
            $scope.verCategorias();
            $scope.categoria={};
        }).error(function (data){
            $scope.messageError(data.error);
        });
    }
    $scope.verCategorias();
});
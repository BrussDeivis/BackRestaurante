app.controller('mercaderiaController', function ($scope, $rootScope, blockUI, productoService,maestroService) {
    $scope.conceptos = [];
    $scope.unidadesDeMedida = [];
    $scope.presentaciones = [];
    $scope.tiposDeSubContenido = [];
    $scope.verSubContenido = false;
    $scope.caracteristicas = [];
    $scope.valores = [];
    $scope.mercaderia = { IdsCaracteristicas: [] };
    $scope.nombreCaracteristica = "";

    $scope.obtenerCaracteristicasConcepto = function () {
        maestroService.obtenerCaracteristicasParaConcepto({idConcepto:$scope.mercaderia.Concepto.Id}).success(function (data) {
            $scope.caracteristicas = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.obtenerConceptos = function () {
        maestroService.obtenerFamiliasConceptosComercialesVigentes({}).success(function (data) {
            $scope.conceptos = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
    $scope.obtenerUnidadesDeMedida = function () {
        maestroService.obtenerUnidadesDeMedida({}).success(function (data) {
            $scope.unidadesDeMedida = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    //$scope.agregarNuevoConcepto = function (nombre) {
    //    $scope.nombre = nombre;
    //    console.debug(nombre);
    //}
    //$scope.agregarNuevaUnidad = function (nombre) {
    //    $scope.unidad = nombre;
    //    console.debug(nombre);
    //}
    
    $scope.obtenerPresentaciones = function () {
        maestroService.obtenerPresentaciones({}).success(function (data) {
            $scope.presentaciones = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
    $scope.listarSubContenidos = function (IdModelo) {
        maestroService.listarSubContenidos({ IdModelo: IdModelo }).success(function (data) {
            $scope.tiposDeSubContenido = data;
            $scope.ultimoCodigo = $scope.tiposDeSubContenido.length + 1;
            $scope.cargarNombreYCodigoProducto();
            $scope.cargarSubContenido();
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
    //$scope.listarCaracteristicas = function (IdArticulo) {
    //    maestroService.listarCaracteristicas({ IdArticulo: IdArticulo }).success(function (data) {
    //        $scope.caracteristicas = data;
    //    }).error(function (data) {
    //        $scope.messageError(data.error);
    //    });
    //}
    
    $scope.codigoProducto = function () {
        $scope.listarSubContenidos($scope.mercaderia.Modelo.Id);
    }
    $scope.agregarNombre = function () {
        $scope.nombreCaracteristica = "";
        for (var i = 0; i < $scope.mercaderia.IdsCaracteristicaConcepto.length; i++) {
            if ($scope.mercaderia.IdsCaracteristicaConcepto != null) {
                for (var j = 0; i < $scope.caracteristicas[i].Valores.length; j++) {
                    if ($scope.mercaderia.IdsCaracteristicaConcepto[i] == $scope.caracteristicas[i].Valores[j].Id) {
                        $scope.nombreCaracteristica += " | " + $scope.caracteristicas[i].Nombre + " " + $scope.caracteristicas[i].Valores[j].Nombre + " ";
                        break;
                    }
                }
            }
        }
        $scope.cargarNombreProducto();
    }
    $scope.cargarNombreYCodigoProducto = function () {
        $scope.mercaderia.Nombre = "";
        $scope.mercaderia.Codigo = "";
        $scope.mercaderia.Codigo = $scope.mercaderia.ConceptoBasico.Id + "-" + $scope.mercaderia.Marca.Id + "-" + $scope.mercaderia.Modelo.Id + "-" + $scope.ultimoCodigo;
        $scope.mercaderia.Nombre = $scope.mercaderia.ConceptoBasico.Nombre + " " + $scope.mercaderia.Sufijo + ($scope.mercaderia.Marca.Id != $scope.tiposDeMarca[0].Id ? " | " + $scope.mercaderia.Marca.Nombre : "") + $scope.nombreCaracteristica +
            " | " + $scope.mercaderia.UnidadDeMedidaCom.Nombre + " | " + $scope.mercaderia.UnidadDeMedidaRef.Nombre + " | PRES. " + $scope.mercaderia.Presentacion.Nombre;

    }
    $scope.cargarNombreProducto = function () {
        $scope.mercaderia.Nombre = "";
        $scope.mercaderia.Nombre = $scope.mercaderia.ConceptoBasico.Nombre + " " + $scope.mercaderia.Sufijo + ($scope.mercaderia.Marca.Id != $scope.tiposDeMarca[0].Id ? " | " + $scope.mercaderia.Marca.Nombre : "") +
            $scope.nombreCaracteristica+
          " | " + $scope.mercaderia.UnidadDeMedidaCom.Nombre + " | " + $scope.mercaderia.UnidadDeMedidaRef.Nombre + " | PRES. " + $scope.mercaderia.Presentacion.Nombre +
          ($scope.verSubContenido ? " x " + $scope.mercaderia.CantidadPresentacion + " " + $scope.mercaderia.UnidadDeMedidaPres.Nombre : "");
    }
    $scope.cargarCombos = function () {
        $scope.listarArticulos($scope.mercaderia.CategoriaConcepto.Id);
    }
    //$scope.cargarValor = function (item) {
    //    for (var i = 0; i < $scope.caracteristicas.length; i++) {
    //        if ($scope.caracteristicas[i].Nombre == item.Nombre) {
    //            $scope.modelo.IdsCaracteristicaConcepto[i] = $scope.valores.Id;
    //        }
    //    }
    //}
    $scope.cargarSubContenido = function () {
        $scope.verSubContenido = false;
        if ($scope.mercaderia.UnidadDeMedidaPres.Id == idUnidadDeMedidaSubUnidad)
        {
            $scope.verSubContenido = true;
            //$scope.listarSubContenidos($scope.modelo.Modelo.Id);
        }
        $scope.cargarNombreProducto();
    }
    $scope.cargarComboMarca = function () {
        $scope.listarMarcas($scope.modelo.ConceptoBasico.Id);
        $scope.listarCaracteristicas($scope.modelo.ConceptoBasico.Id);

    }
    
    $scope.guardar = function () {
console.debug($scope.mercaderia);
        productoService.guardarProducto({ producto: $scope.mercaderia }).success(function (data) {
            $scope.messageSuccess(data.result_description);
            $scope.listarProductos();
            $('#modal-registro').modal('hide');
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
    $scope.close = function () {
        if (angular.equals($scope.empleado, $scope.empleadoInicial)) {
            $('#modal-registro').modal('hide');
        }
        else {
            $('#modal-pregunta').click();
        }
    }

    $scope.closeModal = function () {
        $('#modal-registro').modal('hide');
    }

    $scope.obtenerConceptos();
    $scope.obtenerUnidadesDeMedida();
    $scope.obtenerPresentaciones();
});
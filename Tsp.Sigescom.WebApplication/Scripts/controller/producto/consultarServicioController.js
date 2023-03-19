app.controller('consultarServicioController', function ($scope, $rootScope, blockUI, DTOptionsBuilder, DTColumnDefBuilder, productoService,maestroService) {
    $scope.mercaderias = { Lista: [] }
    $scope.tiposDeCategoria = [];
    $scope.tiposDeArticulo = [];
    $scope.tiposDeMarca = [];
    $scope.tiposDeModelo = [];
    $scope.tiposDeUnidadDeMedida = [];
    $scope.tiposDePresentacion = [];
    $scope.tiposDeSubContenido = [];
    $scope.verSubContenido = false;
    $scope.caracteristicas = [];
    $scope.valores = [];
    $scope.nombreCaracteristica = "";
    
    $scope.listarArticulos = function () {
        maestroService.listarArticulos({}).success(function (data) {
            $scope.tiposDeArticulo = data;
            $scope.modelo.ConceptoBasico = $scope.tiposDeArticulo[0];
            $scope.listarMarcas($scope.modelo.ConceptoBasico.Id);
            $scope.listarCaracteristicas($scope.modelo.ConceptoBasico.Id);
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
    $scope.listarMarcas = function (IdArticulo) {
        maestroService.listarMarcas({IdArticulo:IdArticulo}).success(function (data) {
            $scope.tiposDeMarca = data;
            $scope.modelo.Marca = $scope.tiposDeMarca[0];
            $scope.listarModelos($scope.modelo.Marca.Id,IdArticulo);
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
    $scope.listarModelos = function (IdMarca, IdArticulo) {
        maestroService.listarModelos({IdMarca : IdMarca , IdArticulo : IdArticulo}).success(function (data) {
            $scope.tiposDeModelo = data;
            $scope.modelo.Modelo = $scope.tiposDeModelo[0];
            $scope.codigoProducto();
            //$scope.cargarNombreYCodigoProducto();
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
    $scope.listarUnidadesDeMedida = function () {
        maestroService.listarUnidadesDeMedida({}).success(function (data) {
            $scope.tiposDeUnidadDeMedida = data;
            $scope.modelo.UnidadDeMedidaRef = $scope.tiposDeUnidadDeMedida[0];
            $scope.modelo.UnidadDeMedidaCom = $scope.tiposDeUnidadDeMedida[0];
            $scope.modelo.UnidadDeMedidaPres = $scope.tiposDeUnidadDeMedida[0];
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
    $scope.listarPresentaciones = function () {
        maestroService.listarPresentaciones({}).success(function (data) {
            $scope.tiposDePresentacion = data;
            $scope.modelo.Presentacion = $scope.tiposDePresentacion[0];
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
    $scope.listarCaracteristicas = function (IdArticulo) {
        maestroService.listarCaracteristicas({ IdArticulo: IdArticulo }).success(function (data) {
            $scope.caracteristicas = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
    $scope.nuevoRegistro = function () {
        $scope.modelo = {Sufijo:"",CantidadPresentacion:1, IdsCaracteristicaConcepto: []};
        $scope.modeloInicial = {};
        $scope.modeloInicial = angular.copy($scope.modelo);
        $scope.listarArticulos();
        $scope.listarPresentaciones();
        $scope.listarUnidadesDeMedida();

    }
    
    $scope.codigoProducto = function () {
        $scope.listarSubContenidos($scope.modelo.Modelo.Id);
    }
    $scope.agregarNombre = function () {
        $scope.nombreCaracteristica = "";
        for (var i = 0; i < $scope.modelo.IdsCaracteristicaConcepto.length; i++) {
            if ($scope.modelo.IdsCaracteristicaConcepto != null) {
                for (var j = 0; i < $scope.caracteristicas[i].Valores.length; j++) {
                    if ($scope.modelo.IdsCaracteristicaConcepto[i] == $scope.caracteristicas[i].Valores[j].Id) {
                        $scope.nombreCaracteristica += " | " + $scope.caracteristicas[i].Nombre + " " + $scope.caracteristicas[i].Valores[j].Nombre + " ";
                        break;
                    }
                }
            }
        }
        $scope.cargarNombreProducto();
    }
    $scope.cargarNombreYCodigoProducto = function () {
        $scope.modelo.Nombre = "";
        $scope.modelo.Codigo = "";
        $scope.modelo.Codigo = $scope.modelo.ConceptoBasico.Id + "-" + $scope.modelo.Marca.Id + "-" + $scope.modelo.Modelo.Id + "-" + $scope.ultimoCodigo;
        $scope.modelo.Nombre = $scope.modelo.ConceptoBasico.Nombre + " " + $scope.modelo.Sufijo + ($scope.modelo.Marca.Id != $scope.tiposDeMarca[0].Id ? " | " + $scope.modelo.Marca.Nombre : "") + $scope.nombreCaracteristica +
            " | " + $scope.modelo.UnidadDeMedidaCom.Nombre + " | " + $scope.modelo.UnidadDeMedidaRef.Nombre + " | PRES. " + $scope.modelo.Presentacion.Nombre;

    }
    $scope.cargarNombreProducto = function () {
        $scope.modelo.Nombre = "";
        $scope.modelo.Nombre = $scope.modelo.ConceptoBasico.Nombre + " " + $scope.modelo.Sufijo + ($scope.modelo.Marca.Id != $scope.tiposDeMarca[0].Id ? " | " + $scope.modelo.Marca.Nombre : "") +
            $scope.nombreCaracteristica+
          " | " + $scope.modelo.UnidadDeMedidaCom.Nombre + " | " + $scope.modelo.UnidadDeMedidaRef.Nombre + " | PRES. " + $scope.modelo.Presentacion.Nombre +
          ($scope.verSubContenido ? " x " + $scope.modelo.CantidadPresentacion + " " + $scope.modelo.UnidadDeMedidaPres.Nombre : "");
    }
    $scope.cargarCombos = function () {
        $scope.listarArticulos($scope.modelo.CategoriaConcepto.Id);
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
        if ($scope.modelo.UnidadDeMedidaPres.Id == idUnidadDeMedidaSubUnidad)
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
    $scope.listarProductos = function () {
       productoService.verMercaderias({}).success(function (data) {
           $scope.mercaderias.Lista = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
    $scope.guardar = function () {
        productoService.guardarProducto({ producto: $scope.modelo }).success(function (data) {
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

    $scope.listarProductos();

});
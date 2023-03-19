app.controller('caracteristicaController', function ($scope,$rootScope, DTOptionsBuilder, DTColumnBuilder,SweetAlert, conceptoService) {

    $scope.caracteristicas = [];
    $scope.caracteristica = { EsComun:true, Valores:[] };
    $scope.valorCaracteristica = {};

    $scope.nuevoRegistro = function () {
        $scope.caracteristica = { EsComun: true, Valores: [] };
    }

    $scope.guardarCaracteristica = function (caracteristica) {
        conceptoService.guardarCaracteristica({ caracteristica: caracteristica }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $('#modal-caracteristica').modal('hide');
            $scope.verCaracteristicas();
        }).error(function (data) {
            SweetAlert.error("Error", data.error);
        });
    }

    $scope.editarCaracteristica = function (caracteristica) {
        $scope.caracteristica={};
        $scope.caracteristica = angular.copy(caracteristica);
    }

    $scope.cambiarEstadoCaracteristica = function (caracteristica) {
        conceptoService.cambioEsVigenteCaracteristica({ caracteristica: caracteristica }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.verCaracteristicas();
        }).error(function (data) {
            SweetAlert.error("Error", data.error);
        });
    }
    
    $scope.verCaracteristicas = function () {
        conceptoService.obtenerBandejaCaracteristicas({}).success(function (data) {
            $scope.caracteristicas = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
    
    //VALOR CARACTERISTICA 
    $scope.agregarValor = function () {
        //Comprobar si ya registro un elemento con el mismo nombre
        var producto = Enumerable.from($scope.caracteristica.Valores).where("$.Nombre == '" + $scope.valorCaracteristica.Nombre + "'").toArray()[0];
        let divMessage = $("#container-msg");
        let content = "";
        if (!producto) {
            $scope.caracteristica.Valores.push($scope.valorCaracteristica)
            content = `<div class="msg success-msg"> <i class="fa fa-check">Se ingreso correctamente </i></div>`;
            divMessage.append(content)
        } else {
            content = `<div class="msg warning-msg"> <i class="fa fa-warning"> Ya se ingreso ${$scope.valorCaracteristica.Nombre} </i></div>`;
            divMessage.append(content)
        }
        setTimeout(function () {
            //$(".msg").addClass("show-msg");
            $(".msg").remove();
        }, 2000);

        $scope.valorCaracteristica = {};
    }

    $scope.eliminarValor = function (index) {
        $scope.caracteristica.Valores.splice(index, 1);
    }

    $rootScope.dtOptions.withLightColumnFilter({
        '0': { type: 'text', className: 'form-control padding-left-right-3' },
        '1': { type: 'text', className: 'form-control padding-left-right-3' },
        '2': { type: 'text', className: 'form-control padding-left-right-3' },
    });


});
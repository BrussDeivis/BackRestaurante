app.controller('complementosController', function ($scope, hotelService, centroDeAtencionService, $rootScope, SweetAlert) {
   

    //#region VARIABLES
    $scope.complemento = { Valores: [] };
    $scope.valorComplemento = {}

    //#endregion

    $rootScope.dtOptions.withLightColumnFilter({
        '0': { type: 'text', className: 'form-control padding-left-right-3' },
        '1': { type: 'text', className: 'form-control padding-left-right-3' },
        '2': { type: 'text', className: 'form-control padding-left-right-3' }
        
    });

    $scope.inicializar = function () {
      
    };

    $scope.obtenerComplementos = () => {

    }
    //#region MODAL NUEVO COMPLEMENTO
    $scope.agregarValorComplemento = () => {
        //Comprobar si ya registro un elemento con el mismo nombre
        let producto = Enumerable.from($scope.complemento.Valores).where("$.Nombre == '" + $scope.valorComplemento.Nombre + "'").toArray()[0];
        let divMessage = document.getElementById('container-msg');
        let content = "";
        if (!producto) {
            $scope.complemento.Valores.push($scope.valorComplemento);
            $scope.valorComplemento = {};
            divMessage.innerHTML = `<div class="msg success-msg"> <i class="fa fa-check">Se ingreso correctamente </i></div>`;
           
        } else {
            divMessage.innerHTML = `<div class="msg warning-msg"> <i class="fa fa-warning"> Ya se ingreso ${$scope.valorComplemento.Nombre} </i></div>`;
           
        }
        setTimeout(function () {
            //$(".msg").addClass("show-msg");
            $(".msg").remove();
            //document.getElementsByClassName('msg').clasList.remove();
        }, 2000);
        
    };
    $scope.eliminarValor = (valorComplemento) => {
        const pos = $scope.complemento.Valores.indexOf(valorComplemento);
        $scope.complemento.Valores.splice(pos, 1);

    }
    $scope.guardarComplemento = (complemento) => {
        hotelService.guardarComplemento({ complemento: complemento }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $('#modal-nuevo-complemento').modal('hide');
            $scope.obtenerComplementos();
        }).error(function (data) {
            SweetAlert.error("Error", data.error);
        });
        $scope.complemento = { Valores: [] };
    }
    
    //#region ABRIR MODAL NUEVA CARACTERISTICA
    $scope.abrirModal = function (nombreDelModal) {
        $('#' + nombreDelModal).modal('show');
    }
    $scope.abrirModalNuevaCaracteristica = function () {
        $('#' + 'modal-nueva-caracteristica').modal('show');
    }
    $scope.cerrar = function (nombreDelModal) {
        $('#' + nombreDelModal).modal('hide');
    }
    //#endregion
    //#endregion

    
});


app.controller('tipoDeCambioController', function ($scope, $timeout, blockUI, DTOptionsBuilder, DTColumnDefBuilder, contabilidadService) {
   
    $scope.model = {};
    $scope.elementos = {lista:[]};
    $scope.existeFecha = false;
    $scope.fechaDesde = "";
    $scope.fechaHasta = "";
    $scope.agregarTipoCambio = function () {
        if(confirm("¿Esta Seguro de Guardar este tipo de Cambio?")){
            $scope.verificarFecha();
            if (!$scope.existeFecha) {
                $scope.elementos.lista.push(angular.copy($scope.model));
                $scope.guardar();
                $scope.model = {};
                $timeout(function () {
                    $('.table-cotizacion tr:last input.datepicker-yyyy').datepicker({
                        format: 'dd/mm/yyyy',
                        autoclose: true,
                        todayHighlight: true,
                    });
                }, 1);
            }
        }
    }
    $scope.elementos.dtColumnDefs = [
        DTColumnDefBuilder.newColumnDef(3).withOption('className', 'width-300'),
    ];
    $scope.verificarFecha = function () {
        var i = 0;
        $scope.existeFecha = false;
        for (i = 0; i < $scope.elementos.lista.length; i++) {
            if ($scope.elementos.lista[i].Fecha == $scope.model.Fecha) {
                $scope.existeFecha = true;
            }
        }
        if ($scope.existeFecha) {
            $scope.model.Fecha = '';
            alert("ya existe el tipo de cambio para esta fecha");
        }
    }
    $scope.verificarFechaEditado = function (index) {
        var i = 0;
        bool = false;
        for (i = 0; i < $scope.elementos.lista.length; i++) {
            if (i!=index && $scope.elementos.lista[i].Fecha == $scope.elementos.lista[index].Fecha) {
                bool = true;
                i = $scope.elementos.lista.length;
            }
        }
        if (bool) {
            $scope.elementos.lista[index].Fecha = '';
            alert("ya existe el tipo de cambio para esta fecha");
        }
    }
    $scope.eliminarTipoCambio = function (index) {
        $scope.elementos.lista.slice(index,1);
    }

    $scope.guardar = function () {

        contabilidadService.guardar($scope.model).success(function (data) {
            $scope.listarElementos();
            $scope.messageSuccess(data.descripcion);
        }).error(function (data) {
            $scope.messageError(data.error);
        });
        
    }
    $scope.listarElementos = function () {

        contabilidadService.listar({ desde: $scope.fechaDesde, hasta: $scope.fechaHasta }).success(function (data) {
            $scope.elementos.lista=data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });

    }
    $scope.inicio = function () {
        $scope.fechaDesde = Desde;
        $scope.fechaHasta = Hasta;
        $scope.listarElementos();
    }
    $scope.inicio();
    
    
});
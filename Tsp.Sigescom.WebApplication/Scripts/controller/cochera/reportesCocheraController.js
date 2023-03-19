app.controller('reportesCocheraController', function ($scope, $q, $rootScope, DTOptionsBuilder, DTColumnDefBuilder, cocheraService) {
    

    $scope.inicializar = function () {
        $scope.fechaHoraInicio = fechaHoraInicio;
        $scope.fechaHoraFin = fechaHoraFin;
        $scope.validarFechas();
        $scope.obtenerCocheras();
        $scope.actualizarURLReportes();
    }

    $scope.validarFechas = function () {
        return $scope.fechaHoraFin.getTime() > $scope.fechaHoraInicio.getTime() && dateDiffInDays(new Date($scope.fechaHoraInicio), new Date($scope.fechaHoraFin)) <= cantidadMaximaDias;
    }
        
    $scope.obtenerCocheras = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        cocheraService.obtenerCocheras().success(function (data) {
            $scope.cocheras = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.actualizarURLReportes= function(){
        $scope.actualizarURLReporteDeVehiculosIngresados();
        $scope.actualizarURLReporteDeVehiculosExonerados();
        $scope.actualizarURLReporteDeIngresosYSalidas();
    }

    $scope.actualizarURLReporteDeIngresosYSalidas = function () {
        console.log($scope.fechaHoraInicio);
        $scope.URLReporteDeIngresosYSalidas = URL_ + "/Cochera/ReporteIngresosYSalidas?idCochera=" + $scope.cocheraSeleccionada.Id + "&nombreCochera=" + $scope.cocheraSeleccionada.Nombre + "&desde=" + (new Date($scope.fechaHoraInicio)).toUTCString() + "&hasta=" + (new Date($scope.fechaHoraFin)).toUTCString();
    }

    $scope.actualizarURLReporteDeVehiculosIngresados = function () {
        $scope.URLReporteDeVehiculosIngresados = URL_ + "/Cochera/ReporteVehiculosIngresados?idCochera=" + $scope.cocheraSeleccionada.Id + "&nombreCochera=" + $scope.cocheraSeleccionada.Nombre;
    }

    $scope.actualizarURLReporteDeVehiculosExonerados = function () {
        $scope.URLReporteDeVehiculosExonerados = URL_ + "/Cochera/ReporteExoneraciones?idCochera=" + $scope.cocheraSeleccionada.Id + "&nombreCochera=" + $scope.cocheraSeleccionada.Nombre
    }


}); 
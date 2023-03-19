app.controller('mesasController', function ($scope, $rootScope) {
    
    $scope.itemSeleccionado = (valor, $event) => {
        if ($event.currentTarget.style.backgroundColor == "red") {
            $event.currentTarget.style.backgroundColor = "green";
        } else {
            $event.currentTarget.style.backgroundColor = "red";
        }
       
    }

}); 
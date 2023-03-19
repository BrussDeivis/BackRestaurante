app.controller('carteraDeClientesController', function ($scope, $q, $rootScope, $timeout, DTOptionsBuilder, DTColumnDefBuilder, centroDeAtencionService, clienteService, SweetAlert) {

    //************************  MANEJO DE BANDEJA *******************//
    $scope.cartera = {} ;

    $rootScope.dtOptions.withLightColumnFilter({
        '0': { type: 'text', className: 'form-control padding-left-right-3' },
        '1': { type: 'text', className: 'form-control padding-left-right-3' },
        '2': { type: 'text', className: 'form-control padding-left-right-3' },
        '3': { type: 'text', className: 'form-control padding-left-right-3' },
    });

    $scope.obtenerCarterasDeClientesBandeja = function () {
        clienteService.obtenerCarterasDeClientes().success(function (data) {
            $scope.listaCarterasDeClientes = data;
        }).error(function (data) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }
    
     
    //********************** GESTION DE CARTERA DE CLIENTES ****************//

    $scope.iniciarCarteraDeClientes = function () {
        $scope.cartera = { Detalles: [] };
    }

    $scope.nuevaCarteraDeClientes = function () {
        $scope.iniciarCarteraDeClientes();
        $scope.editando = false;
        $scope.obtenerEstablecimientosComerciales();
        $scope.obtenerClientes();
        //$timeout(function () { $('#establecimientoComercial').trigger("change"); }, 100);
        //$timeout(function () { $('#centroDeAtencion').trigger("change"); }, 100);
        //$timeout(function () { $('#cliente').trigger("change"); }, 100);
    }

    $scope.obtenerEstablecimientosComerciales = function () {
        centroDeAtencionService.obtenerEstablecimientosComerciales().success(function (data) {
            $scope.listaEstablecimientosComerciales = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.obtenerCentrosAtencion = function (id) {
        centroDeAtencionService.obtenerCentrosDeAtencionPorEstablecimientoComercialParaCarteraDeClientes({ idEstablecimientoComercial: id }).success(function (data) {
            $scope.listaCentrosDeAtencion = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.obtenerClientes = function () {
        clienteService.obtenerClientesGenericoSinParentRol().success(function (data) {
            $scope.listaClientes  = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.guardarCarteraDeClientes = function () {
        clienteService.guardarCarteraDeClientes({ cartera: $scope.cartera }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.cerrar("modal-registro-cartera-de-cliente");
            $scope.obtenerCarterasDeClientesBandeja();
        }).error(function (data, status) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }

    $scope.editarCarteraDeClientes = function (idCarteraDeCliente) {
        $scope.editando = true;
        $scope.iniciarCarteraDeClientes();
        $scope.obtenerClientes();
        clienteService.obtenerCarteraDeClientesAEditar({ idCarteraDeCliente: idCarteraDeCliente }).success(function (data) {
            $scope.cartera = data;
            $scope.nombreEstablecimientoComercial = $scope.cartera.EstablecimientoComercial.Nombre;
            $scope.nombreCentroDeAtencion = $scope.cartera.CentroDeAtencion.Nombre;
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }
 
    $scope.verCarteraDeClientes = function (idCarteraDeCliente) {
        clienteService.obtenerCarteraDeClientes({ idCarteraDeCliente: idCarteraDeCliente }).success(function (data) {
            $scope.verCartera = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.agregarClienteALaCartera = function () {
        var validar = false;
        if ($scope.cartera.Detalles.length > 0) {
            for (let i = 0; i < $scope.cartera.Detalles.length; i++) {
                if ($scope.cliente.Id == $scope.cartera.Detalles[i].Id) {
                    validar = true;
                    break;
                }
            }
        }
        if (validar) {
            $scope.cliente = {};
        }
        else {
            $scope.cartera.Detalles.unshift($scope.cliente);
            $scope.cliente = {};
        }
    }

    $scope.quitarClienteDeLaCartera = function (index) {
        $scope.cartera.Detalles.splice(index, 1);
    }
    
    $scope.cerrar = function (nombreDelModal) {
        $('#' + nombreDelModal).modal('hide');
    }

    $scope.datosRequeridosParaCarteraClientes = function () {
        return $scope.cartera.CentroDeAtencion == undefined || $scope.cartera.Detalles.length == 0 || $scope.cartera.Detalles == undefined || $scope.cartera.Detalles == null;
    }

});
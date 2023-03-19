app.controller('grupoClientesController', function ($scope, $q, $rootScope, $timeout, DTOptionsBuilder, DTColumnDefBuilder, maestroService, clienteService, SweetAlert) {

    //************************  BANDEJA DE GRUPO DE CLIENTES *******************//

    $scope.inicializar = function () {
        $scope.inicializacionRealizada = false;
        $scope.cargarParametros();
        $scope.cargarColeccionesAsync();
        $scope.inicializarComponentes();
        $scope.obtenerGruposClientes();
    }
    $scope.inicializarComponentes = function () {
        $scope.inicializacionRealizada = true;
    }
    $scope.cargarParametros = function () {
        $scope.idClienteGenerico = idClienteGenerico;
        $scope.rolCliente = { Id: idRolCliente, Nombre: 'CLIENTE' };
        $scope.tiempoEsperaBusquedaSelector = tiempoEsperaBusquedaSelector;
        $scope.minimoCaracteresBuscarActorComercial = minimoCaracteresBuscarActorComercial;
        $scope.mascaraDeVisualizacionValidacionRegistroCliente = mascaraDeVisualizacionValidacionRegistroCliente;
    }

    $scope.cargarColeccionesAsync = function () {
        $scope.obtenerTiposGrupoClientes();
        $scope.obtenerClasificacionesGrupoClientes();
    }

    $scope.obtenerTiposGrupoClientes = function () {
        maestroService.obtenerTiposGrupoClientes({}).success(function (data) {
            $scope.tiposGrupoClientes = data;
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.obtenerClasificacionesGrupoClientes = function () {
        maestroService.obtenerClasificacionesGrupoClientes({}).success(function (data) {
            $scope.clasificacionesGrupoClientes = data;
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.obtenerGruposClientes = function () {
        clienteService.obtenerGruposClientes().success(function (data) {
            $scope.gruposClientes = data;
        }).error(function (data) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }

    $rootScope.dtOptions.withLightColumnFilter({
        '1': { type: 'text', className: 'form-control padding-left-right-3' },
        '2': { type: 'text', className: 'form-control padding-left-right-3' },
        '3': { type: 'text', className: 'form-control padding-left-right-3' },
        '4': { type: 'text', className: 'form-control padding-left-right-3' },
        '5': { type: 'text', className: 'form-control padding-left-right-3' },
        '6': { type: 'text', className: 'form-control padding-left-right-3' },
        '7': { type: 'text', className: 'form-control padding-left-right-3' },
        '8': { type: 'text', className: 'form-control padding-left-right-3' },
        '9': { type: 'text', className: 'form-control padding-left-right-3' },
    });

    //********************** REGISTRO DE GRUPO DE CLIENTES ****************//

    $scope.nuevoGrupoClientes = function () {
        $scope.limpiarGrupoClientes();
        $scope.establecerDatosPorDefecto();
        $scope.accionModal = 'REGISTRAR';
    }

    $scope.limpiarGrupoClientes = function () {
        $scope.grupoClientes = { Clientes: [] };
    }

    $scope.establecerDatosPorDefecto = function () {
        $scope.selectorResponsableAPI.EstablecerActorPorDefecto();
        $scope.selectorClienteAPI.EstablecerActorPorDefecto();
    }

    $scope.cambioResponsable = function (actorComercial) {
        $scope.grupoClientes.Responsable = actorComercial;
        $scope.verificarInconsistencias();
    };

    $scope.cambioCliente = function (actorComercial) {
        $scope.grupoClientes.Cliente = actorComercial;
        $scope.verificarInconsistencias();
    };

    $scope.guardarGrupoClientes = function () {
        clienteService.guardarGrupoClientes({ grupoClientes: $scope.grupoClientes }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.cerrar("modal-registro-grupo-clientes");
            $scope.obtenerGruposClientes();
        }).error(function (data, status) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }

    $scope.editarGrupoClientes = function (idGrupoCliente) {
        $scope.limpiarGrupoClientes();
        $scope.establecerDatosPorDefecto();
        $scope.accionModal = 'EDITAR';
        clienteService.obtenerGrupoClientes({ idGrupoCliente: idGrupoCliente }).success(function (data) {
            $scope.grupoClientes = data;
            $scope.selectorClienteAPI.EstablecerActorPorDefecto();
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.cargarVerGrupoClientes = function (cliente) {
        $scope.verGrupoClientes = cliente;

    }

    $scope.agregarClienteAGrupoClientes = function () {
        var validar = false;
        if ($scope.grupoClientes.Clientes.length > 0) {
            for (let i = 0; i < $scope.grupoClientes.Clientes.length; i++) {
                if ($scope.grupoClientes.Cliente.Id == $scope.grupoClientes.Clientes[i].Id) {
                    validar = true;
                    break;
                }
            }
        }
        if (validar) {
            $scope.selectorClienteAPI.EstablecerActorPorDefecto();
        }
        else {
            let cliente = { Id: $scope.grupoClientes.Cliente.Id, Documento: $scope.grupoClientes.Cliente.TipoDocumentoIdentidad.Valor + " - " + $scope.grupoClientes.Cliente.NumeroDocumentoIdentidad, Nombre: $scope.grupoClientes.Cliente.NombreORazonSocial };
            $scope.grupoClientes.Clientes.unshift(cliente);
            $scope.selectorClienteAPI.EstablecerActorPorDefecto();
        }
    }

    $scope.quitarClienteDeGrupoClientes = function (index) {
        $scope.grupoClientes.Clientes.splice(index, 1);
        $scope.verificarInconsistencias();
    }

    $scope.datosRequeridosParaGrupoClientesClientes = function () {
        return $scope.grupoClientes.CentroDeAtencion == undefined || $scope.grupoClientes.Clientes.length == 0 || $scope.grupoClientes.Clientes == undefined || $scope.grupoClientes.Clientes == null;
    }

    $scope.darBajaGrupoClientes = function (idGrupoCliente) {
        clienteService.darBajaGrupoClientes({ idGrupoCliente: idGrupoCliente }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.cerrar("modal-ver-grupo-clientes");
            $scope.obtenerGruposClientes();
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.verificarInconsistencias = function () {
        $scope.inconsistencias = [];
        if ($scope.grupoClientes.Codigo == undefined || $scope.grupoClientes.Codigo == '') {
            $scope.inconsistencias.push("Es necesario ingresar el código del grupo de clientes.");
        }
        if ($scope.grupoClientes.Nombre == undefined || $scope.grupoClientes.Nombre == '') {
            $scope.inconsistencias.push("Es necesario ingresar el nombre del grupo de clientes.");
        }
        if ($scope.grupoClientes.Tipo == undefined) {
            $scope.inconsistencias.push("Es necesario seleccionar un tipo del grupo de clientes.");
        }
        if ($scope.grupoClientes.Clasificacion == undefined) {
            $scope.inconsistencias.push("Es necesario seleccionar una clasificacion del grupo de clientes.");
        }
        if ($scope.grupoClientes.Responsable == undefined) {
            $scope.inconsistencias.push("Es necesario seleccionar un responsable del grupo de clientes.");
        } else {
            if ($scope.grupoClientes.Responsable.Id == $scope.idClienteGenerico) {
                $scope.inconsistencias.push("Es necesario identificar el responsable del grupo de clientes.");
            }
        }
        if ($scope.grupoClientes.Clientes == undefined) {
            $scope.inconsistencias.push("Es necesario seleccionar al menos un cliente para el grupo de clientes.");
        } else {
            if ($scope.grupoClientes.Clientes.length < 1) {
                $scope.inconsistencias.push("Es necesario seleccionar al menos un cliente para el grupo de clientes.");
            }
        }
        
    }

    $scope.cerrar = function (nombreDelModal) {
        $('#' + nombreDelModal).modal('hide');
    }
});
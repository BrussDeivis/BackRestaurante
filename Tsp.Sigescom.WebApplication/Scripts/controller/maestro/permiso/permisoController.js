app.controller('permisoController', function ($scope, $timeout, maestroService , permisoService) {
    $scope.operacionesGenericas = [] ;
    /*$scope.operacionGenerica = {};*/
    $scope.accionesEstadoEnVista =  [ ];/* acciones en vista pueden estar no seleccionadas*/
    $scope.roles = [];
    /* $scope.rol = {};*/
    $scope.rolAccion = {};
    $scope.rolAcciones = [];
    $scope.estadoAcciones_Old = [];/*lista de acciones estados ya registrados*/
    $scope.estadoAcciones_Upd = [];/*lista de acciones estados seleccionados para persistir*/
    $scope.accionesEdicion = false;
    $scope.estadoAccion={Id:'',IdTipoTransaccion:'',IdEstadoAccion:'',IdAccion:''};
    $scope.estado = {};
    $scope.unidadesDeNegocio = [];
    $scope.estados = [];/* lista de estados */
    $scope.accionesSeleccionadas = [];
    /*funcion que lista todos los estados para mostrar en la vista*/
    $scope.obtenerEstados = function () {
        maestroService.obtenerEstados({}).success(function (data) {
            $scope.estados = data;
        });
        ;
    }

    /*funcion que lista todos las acciones para mostrar en el la vista*/
    $scope.obtenerAcciones = function () {
        maestroService.obtenerAcciones({}).success(function (data) {
            $scope.accionesEstadoEnVista = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
    /*funcion que obtiene las unidades de negocio en la vista*/
    $scope.obtenerUnidadesDeNegocio = function () {
        maestroService.obtenerUnidadesDeNegocio({}).success(function (data) {
            $scope.unidadesDeNegocio = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
    /* funcion que lista  los roles para mostrar en la vista*/
    $scope.obtenerRolesPersonal = function () {
        maestroService.obtenerRolesPersonal({}).success(function (data) {
            $scope.roles = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    /*funcion que obtiene todos los tipos de transaccion para la vista*/
    $scope.obtenerTiposDeTransaccion = function () {
        maestroService.obtenerTiposDeTransaccion({}).success(function (data) {
            $scope.operacionesGenericas = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    /*funcion que carga los conbos de roles y estados cuando selecctionas una transaccion*/
    $scope.cargarCombos = function () {
        $scope.obtenerRolesPersonal();
        $scope.obtenerEstados();
    }

    /*funcion que elimina una accion de la lista de acciones a enviar*/
    $scope.eliminarRolAccion = function (item) {
        $scope.accionesEdicion = true;
        var index = $scope.rolAcciones.indexOf(item)
        $scope.rolAcciones.splice(index, 1);
        console.debug($scope.rolAcciones);
        console.debug($scope.estadoAcciones_Upd);
    }

 
    /*funcion que asigna una nuevo rol accion a la lista a enviar*/
    $scope.nuevoRolAccion = function () {
        if($scope.esRolAccionDuplicado()){
            alert("ACCION DUPLICADA");
            }
        else{
            $scope.accionesEdicion = true;
            $scope.rolAccion.TipoTransaccion = $scope.operacionGenerica;
            $scope.rolAccion.RolPersonal = $scope.rol;
            $scope.rolAcciones.push($scope.rolAccion);
            $scope.limpiarRolAccion();
        }
    }
    /*funcion que selecciona y deselecciona un chek de la lista de acciones*/
    $scope.seleccion = function (item) {
        $scope.accionesEdicion = true;

        var idx = $scope.accionesSeleccionadas.indexOf(item.Id);

        /* is currently selected*/
        if (idx > -1) {
            $scope.accionesSeleccionadas.splice(idx, 1);
        }
            /* is newly selected*/
        else {
            $scope.accionesSeleccionadas.push(item.Id);
        }
        console.debug($scope.accionesSeleccionadas);
    };
    $scope.limpiarRolAccion = function () {
        $scope.rolAccion = {};
        $("#unidadNegocio").select2("val", "");
        $("#rolAccion").select2("val", "");
    }
    

    /*funcion que guarda todos los cambios asignados a una transaccion*/
    $scope.guardarAcciones = function () {

        for (var i = 0; i < $scope.accionesSeleccionadas.length; i++) {
            $scope.estadoAccion = { Accion: {}};
            var encontrado = false;

            for (var j = 0; j < $scope.estadoAcciones_Old.length; j++) {
                if ($scope.accionesSeleccionadas[i] == $scope.estadoAcciones_Old[j].Accion.Id)
                {
                    $scope.estadoAcciones_Upd.push($scope.estadoAcciones_Old[j]);
                    encontrado=true;
                }
            }
            if(!encontrado)
            {   
                /*si no existe agrego uno nuevo*/
            $scope.estadoAccion.Id = 0;
            $scope.estadoAccion.TipoTransaccion = $scope.operacionGenerica;
            $scope.estadoAccion.Estado = $scope.estado;
            $scope.estadoAccion.Accion.Id = $scope.accionesSeleccionadas[i];
            $scope.estadoAcciones_Upd.push($scope.estadoAccion);
            }
        }
        permisoService.guardarAcciones({ rolAcciones: $scope.rolAcciones, estadoAcciones: $scope.estadoAcciones_Upd, idTT: $scope.operacionGenerica.Id, idR: $scope.rol.Id, idEA: $scope.estado.Id }).success(function (data) {
            if (data.codigo == 0) {
                var message = data.descripcion + ", \n " + data.excepciones.length + " problema(s): ";
                for (var i = 0; i < data.excepciones.length; i++) {
                    message = message + "\n " + data.excepciones[i] + ". ";
                }
                $scope.messageError(message);

                } else {
                    $scope.messageSuccess(data.descripcion);
                    $scope.limpiarTodo();
                }
            });
    }

    $scope.validarSeleccion = function () {
        $scope.rol = {};
        $scope.estado = {};
        $scope.rolAcciones = [];
        $scope.estadoAcciones_Upd = [];
        $("#estado").select2("val", "");
        $("#rol").select2("val", "");
        $scope.rolAccion = {};
        $("#unidadNegocio").select2("val", "");
        $("#rolAccion").select2("val", "");
        $scope.accionesEdicion = false;
    }
    
    $scope.limpiarTodo = function () {
        $scope.rol = {};
        $scope.estado = {};
        $scope.operacionGenerica = {};
        $scope.rolAcciones = [];
        $scope.estadoAcciones_Upd = [];
        $("#operacion").select2("val", "");
        $("#estado").select2("val", "");
        $("#rol").select2("val", "");
        $scope.accionesEdicion = false;
        $scope.rolAccion = {};
        $("#unidadNegocio").select2("val", "");
        $("#rolAccion").select2("val", "");
    }

    $scope.esRolAccionDuplicado = function () {

        var encontrado = false;
        var i =0;
        while (i < $scope.rolAcciones.length && !encontrado) {
            if ($scope.rolAcciones[i].UnidadNegocio.Id == $scope.rolAccion.UnidadNegocio.Id && $scope.rolAcciones[i].Accion.Id == $scope.rolAccion.Accion.Id) {
                encontrado = true;
            }
            i++;
        }
        return encontrado;
    }


        


    /*funcion que carga los checks con los de la BD*/
    $scope.cargarEstadoAccion = function () {
        $scope.accionesSeleccionadas = [];
        permisoService.obtenerEstadoAccion({ idTT: $scope.operacionGenerica.Id, idEA: $scope.estado.Id }).success(function (data) {
             $scope.estadoAcciones_Old = data;
                 for (var i = 0; i < $scope.estadoAcciones_Old.length; i++) {
                     $scope.accionesSeleccionadas.push($scope.estadoAcciones_Old[i].Accion.Id);
                 }
        });
    }
    
    /*funcion lista las acciones por rol en la vista*/
    $scope.cargarRolAccion = function () {
        permisoService.obtenerRolAccion({ idTT: $scope.operacionGenerica.Id, idR: $scope.rol.Id }).success(function (data) {
            $scope.rolAcciones = data;
        });
    }

    

    $scope.obtenerAcciones();
    $scope.obtenerTiposDeTransaccion();
    $scope.obtenerUnidadesDeNegocio();
});
app.controller('bandejaUsuariosController', function ($scope, $q, $timeout, DTOptionsBuilder, DTColumnDefBuilder, adminService, empleadoService) {
    $scope.elementos = { lista: [] };
    $scope.UserName = {};
    $scope.usuariosEmpleado = [];
    $scope.tempUsuariosEmpleado = [];
    $scope.usuariosEmpleadoInicial = [];
    $scope.usuarioEmail = "";
    $scope.idUsuario = "";
    $scope.registro = { UsuariosEmpleado: [], UsuariosEmpleadoInicial: [] };
    $scope.listaDeEmpleadosRegistrado = [];
    $scope.divListaDeEmpleadosRegistrado = false;
    $scope.listaUsuarios = [];
    $scope.listaEmpleadosConIdUsuario = [];


    $scope.listarUsuarios = function () {
        var defered = $q.defer();
        var promise = defered.promise;

        adminService.listarUsuarios().success(function (data) {
            $scope.listaUsuarios = data;
            if ($scope.listaUsuarios.length > 0) {
                for (let i = 0; i < $scope.listaUsuarios.length; i++) {
                    let empleados = "";
                    for (let j = 0; j < $scope.listaEmpleadosConIdUsuario.length; j++) {
                        if ($scope.listaUsuarios[i].IdUsuario === $scope.listaEmpleadosConIdUsuario[j].IdUsuario) {
                            empleados += $scope.listaEmpleadosConIdUsuario[j].Nombre + ", ";
                        }
                    }
                    empleados = empleados == "" ? empleados : empleados.substring(0, empleados.length - 2);
                    $scope.listaUsuarios[i].NombreEmpleado = empleados;
                }
            }
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.cargarListaUsuariosSync = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        try {

            $scope.listarEmpleadosConIdUsuario().then(function (resultado_) {
                defered.resolve();

                $scope.listarUsuarios().then(function (resultado_) {
                    $scope.elementos.lista = angular.copy($scope.listaUsuarios);
                    defered.resolve();
                }, function (error) {
                    $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
                });

            }, function (error) {
                $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
            });

        } catch (e) {
            defered.reject(e);
        }
        return promise;
    }



    $scope.listarEmpleadosConIdUsuario = function () {
        var defered = $q.defer();
        var promise = defered.promise;

        empleadoService.obtenerEmpleadosConIdUsuario().success(function (data) {
            $scope.listaEmpleadosConIdUsuario = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });

        return promise;
    }

    $scope.init = function () {
        $scope.listaUsuarios = [];
        $scope.listaEmpleadosConIdUsuario = [];
        $scope.cargarListaUsuariosSync();
    }

    $scope.cargarModalAsignarUsuarioEmpleado = function (idUsuario, email) {
        $scope.usuarioEmail = email;
        $scope.usuariosEmpleado = [];
        $scope.tempUsuariosEmpleado = [];
        $scope.usuariosEmpleadoInicial = [];
        $scope.registro = { UsuariosEmpleado: [], UsuariosEmpleadoInicial: [] };
        $scope.divListaDeEmpleadosRegistrado = false;
        $scope.idUsuario = idUsuario;

        for (let j = 0; j < $scope.listaEmpleadosConIdUsuario.length; j++) {
            if (idUsuario === $scope.listaEmpleadosConIdUsuario[j].IdUsuario) {
                $scope.tempUsuariosEmpleado.push($scope.listaEmpleadosConIdUsuario[j]);
            }
        }
        $timeout(function () {
            $('#empleado').trigger("change");
        }, 200);
        console.log("----------------EL SCOPE USUARIOS EMPLEADO AL CARGAR MODAL---------------------");
        $scope.usuariosEmpleado = $scope.tempUsuariosEmpleado;
        console.log($scope.usuariosEmpleados);
        console.log("----------------EL SCOPE USUARIOS EMPLEADO  INICIAL AL CARGAR MODAL---------------------");
        $scope.usuariosEmpleadoInicial = $scope.usuariosEmpleado;
        console.log($scope.usuariosEmpleadosInicial);

    }
    $scope.asignarUsuarioEmpleado = function () {
        $scope.registro.UsuariosEmpleadoInicial = $scope.usuariosEmpleadoInicial;
        $scope.registro.UsuariosEmpleado = $scope.usuariosEmpleado;
        empleadoService.asignarUsuarioEmpleado({ idUsuario: $scope.idUsuario, registro: $scope.registro }).success(function (data) {
            $scope.messageSuccess(data.result_description);
            $scope.cerrar();
            $scope.init();
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
    $scope.preguntarSiDeseaReasignarUsuario = function () {


        $timeout(function () {
            $scope.listaDeEmpleadosRegistrado = [];
            $('#padre').empty();
            for (let i = 0; i < $scope.usuariosEmpleado.length; i++) {
                if ($scope.usuariosEmpleado[i].IdUsuario != null && $scope.usuariosEmpleado[i].IdUsuario != $scope.idUsuario) {
                    $scope.listaDeEmpleadosRegistrado.push($scope.usuariosEmpleado[i]);
                }
            }
            let nombre = "";
            for (let i = 0; i < $scope.listaDeEmpleadosRegistrado.length; i++) {
                nombre = $scope.listaDeEmpleadosRegistrado[i].Nombre;
                $("#padre").append(`<li>${nombre}</li>`);
            }
        }, 200);


        $timeout(function () {
            if ($scope.listaDeEmpleadosRegistrado.length > 0) {
                $scope.divListaDeEmpleadosRegistrado = true;
                $('#reasignarUsuario').click();
            } else {
                $scope.asignarUsuarioEmpleado();
            }
        }, 200);
    }
    $scope.cerrar = function () {
        $('#modal-asignacion-usuario-empleado').modal('hide');
    }
    $scope.cargarModal = function (UserName) {
        $scope.UserName = UserName;
        console.debug($scope.UserName);
    }
    $scope.eliminar = function () {
        adminService.eliminarUsuario({ UserName: $scope.UserName }).success(function (data) {
            $scope.messageSuccess(data.result_description);
            $scope.cargarListaUsuariosSync();
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
});



app.controller('bandejaRolesController', function ($scope, DTOptionsBuilder, DTColumnDefBuilder, adminService) {
    $scope.elementos = { lista: [] };
    $scope.RoleName = {};
    $scope.listarRoles = function () {
        adminService.listarRoles().success(function (data) {
            $scope.elementos.lista = data;
        }).error(function (data) {
            $scope.elementos.lista = data;
        });
    }
    $scope.cargarModal = function (RoleName) {
        $scope.RoleName = RoleName;
    }

    $scope.eliminarRol = function () {
        adminService.eliminarRol({ RoleName: $scope.RoleName }).success(function (data) {
            $scope.messageSuccess(data.result_description);
            $scope.listarRoles();
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
    $scope.listarRoles();

});
app.controller('usuarioController', function ($scope, $timeout, SweetAlert, adminService) {
    $scope.roles = { lista: [] };
    $scope.model = { Roles: [] };
    $scope.rol = { RoleName: {} };
    $scope.obtenerRoles = function () {
        adminService.obtenerRoles().success(function (data) {
            $scope.roles = data;
        });
    }
    $scope.agregarRol = function () {
        $scope.model.Roles = [];
        $scope.model.Roles.push($scope.rol);
    }
    $scope.guardarUsuario = function () {
        adminService.guardarUsuario({ paramExpandedUserDTO: $scope.model }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.model = { Roles: [] };
            $scope.rol = { RoleName: {} };
        }).error(function (data) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }
    $scope.obtenerRoles();

});
app.controller('rolController', function ($scope, $timeout, adminService) {
    $scope.model = {};

    $scope.guardarRol = function () {
        adminService.guardarRol({ paramRoleDTO: $scope.model }).success(function (data) {
            $scope.messageSuccess(data.result_description);
            $scope.model = {};
            /*$timeout(function () { $(".cancelar")[0].click(); }, 1000);*/
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
});


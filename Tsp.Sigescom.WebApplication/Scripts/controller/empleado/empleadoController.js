app.controller('empleadoController', function ($scope, $q, $compile, $rootScope, SweetAlert, confirm, $timeout, blockUI, DTOptionsBuilder, DTColumnBuilder, empleadoService, actorComercialService) {

    $scope.inicializar = function () {
        $scope.limpiar();
        $scope.cargarParametros();
        $scope.cargarColeccionesAsync();
    }

    $scope.limpiar = function () {
        $scope.empleado = {
            DomicilioFiscal: {},
            TipoDocumentoIdentidad: {},
            Roles: []
        };
    }

    $scope.cargarParametros = function () {
        $scope.rolEmpleado = { Id: idRolEmpleado, Nombre: 'EMPLEADO' };
        $scope.mascaraDeVisualizacionValidacionRegistroEmpleado = mascaraDeVisualizacionValidacionRegistroEmpleado;
    }

    $scope.cargarColeccionesAsync = function () {
        $scope.listarEmpleados();
    }

    $scope.listarEmpleados = function () {
        empleadoService.listarEmpleados({}).success(function (data) {
            $scope.empleados = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.establecerDatosPorDefecto = function () {
    }

    $scope.nuevoEmpleado = function () {
        $scope.registradorActorComercialAPI.AgregarActorComercial($scope.empleado);
    };

    $scope.editarEmpleado = function (id) {
        actorComercialService.obtenerActorComercialPorId({ idRol: $scope.rolEmpleado.Id, id: id }).success(function (data) {
            $scope.empleado = data.information;
            $scope.registradorActorComercialAPI.EditarActorComercial($scope.empleado);
        }).error(function (data) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    };

    $scope.cargarActorRegistardo = function (actorComercial) {
        $scope.limpiar();
        $scope.listarEmpleados();
    };

    $scope.cargarEliminarEmpleado = function (item) {
        $scope.actor = angular.copy(item);
    }

    $scope.eliminarEmpleado = function (id) {
        empleadoService.eliminarEmpleado({ IdEmpleado: id }).success(function (data) {
            $scope.messageSuccess(data.result_description);
            $scope.listarEmpleados();
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $rootScope.dtOptions.withLightColumnFilter({
        '0': { type: 'text', className: 'form-control' },
        '1': { type: 'text', className: 'form-control' },
        '2': { type: 'text', className: 'form-control padding-left-right-1' },
        '3': { type: 'text', className: 'form-control padding-left-right-1' },
        '4': { type: 'text', className: 'form-control padding-left-right-1' },
        '5': { type: 'text', className: 'form-control padding-left-right-1' },
        '6': { type: 'text', className: 'form-control padding-left-right-1' },
        '7': { type: 'text', className: 'form-control padding-left-right-1' },
        '8': { type: 'text', className: 'form-control padding-left-right-1' },

    });
});



//app.controller('empleadoController', function ($scope, $compile, $q, $rootScope, confirm, $timeout, blockUI, empleadoService, maestroService) {

//    /*----------------------------------------MANEJO DE EMPLEADO----------------------------------------*/

//    $scope.tiposDeDocumentosDeIdentidad = [];

//    $scope.empleados = { lista: [] };
//    $scope.empleado = { Direcciones: [],Roles:[]};
//    $scope.empleadoInicial = {};
//    $scope.direccion={};

//    $scope.tiposDeDireccion = [];
//    $scope.tiposDeClaseActor = [];
//    $scope.tiposDeEstadoLegal = [];
//    $scope.rolesPersonal = [];
//    $scope.rolesUsuario = [];
//    $scope.model = {Roles:[],Rol:[]};
//    $scope.ubigeosPeru = [];

//    //registrar => true, editar =>false
//    $scope.estado = true;

//    //INICIALIZADORES
//    $scope.initModalRegistroEmpleado= function () {
//        $scope.listarTiposDeDocumentosDeIdentidad();
//        $scope.listarUbigeoDistrito();
//        $scope.listarTiposDeDireccion();
//    }
//    $scope.listarTiposDeEstadoLegal = function () {
//        maestroService.listarTiposDeEstadoLegalActor({ IdTipoDeActor: IdTipoActorPersonaNarural }).success(function (data) {
//            $scope.tiposDeEstadoLegal = data;
//            if ($scope.estado) {
//                $scope.empleado.EstadoLegalActor = $scope.tiposDeEstadoLegal[0];
//            }
//        }).error(function (data) {
//            $scope.messageError(data.error);
//        });
//    }
//    $scope.listarTiposDeClaseActor = function () {
//        maestroService.listarTiposDeClaseActor({ IdTipoDeActor: IdTipoActorPersonaNarural }).success(function (data) {
//            $scope.tiposDeClaseActor = data;
//            if ($scope.estado) {
//                $scope.empleado.ClaseActor = $scope.tiposDeClaseActor[0];
//            }
//        }).error(function (data) {
//            $scope.messageError(data.error);
//        });
//    }
//    $scope.listarTiposDeDocumentosDeIdentidad = function () {
//        maestroService.listarTiposDeDocumentosDeIdentidad({}).success(function (data) {
//            $scope.tiposDeDocumentosDeIdentidad = data;
//        }).error(function (data) {
//            $scope.messageError(data.error);
//        });
//    }
//    $scope.seleccionarTipoDocumento = function (idTipoDocumento) {
//        for (var i = 0; i < $scope.tiposDeDocumentosDeIdentidad.length; i++) {
//            if ($scope.tiposDeDocumentosDeIdentidad[i].Id == idTipoDocumento) {
//                $scope.empleado.TipoDocumentoIdentidad = $scope.tiposDeDocumentosDeIdentidad[i];
//                $scope.validarLongitudNumero();
//                break;
//            }
//        }
//    }
//    $scope.listarTiposDeDireccion = function () {
//        maestroService.listarTiposDeDireccion({}).success(function (data) {
//            $scope.tiposDeDireccion = data;
//        }).error(function (data) {
//            $scope.messageError(data.error);
//        });
//    }
//    $scope.listarUbigeoDistrito = function () {
//        maestroService.listarUbigeoDistrito().success(function (data) {
//            $scope.ubigeosPeru = data;
//        }).error(function (data) {
//            $scope.messageError(data.error);
//        });
//    }

//    // INICIAR REGISTRO EMPLEADO
//    $scope.iniciarRegistroEmpleado = function () {
//        $scope.empleado = { Direcciones: [], Roles: [] };
//        $scope.direccion = {};
//        $scope.empleadoInicial = {};
//        $scope.empleadoInicial = angular.copy($scope.empleado);
//        $timeout(function () { $('#rol-empleado').trigger("change"); }, 100);
//        $scope.idTipoDocumentoSeleccionadaConTipoPersonaNatural = idTipoDocumentoSeleccionadaConTipoPersonaNatural;
//        $scope.idTipoDocumentoIdentidadDni = idTipoDocumentoIdentidadDni;
//        $scope.idTipoDocumentoIdentidadRuc = idTipoDocumentoIdentidadRuc;
//        $scope.listarTiposDeEstadoLegal();
//        $scope.listarTiposDeClaseActor();
//        $scope.guardadoParaFuera = false;
//        $scope.longitudCaracteresTipoDocumentoIdentidad = 0;
//        $scope.tipoRegistroFuera = 0;  //EmisorIngresoEgreso(1) //PagadorBeneficiarioIngresoEgreso(2)
//    }

//    //NUEVO EMPLEADO
//    $scope.nuevoRegistroEmpleado = function () {
//        $scope.estado = true;
//        $scope.iniciarRegistroEmpleado();
//        $scope.seleccionarTipoDocumento($scope.idTipoDocumentoSeleccionadaConTipoPersonaNatural);
//    }

//    $scope.validarLongitudNumero = function () {
//        $scope.empleado.TipoDocumentoIdentidad.Id == $scope.idTipoDocumentoIdentidadDni ? $scope.longitudCaracteresTipoDocumentoIdentidad = 8 :
//            $scope.empleado.TipoDocumentoIdentidad.Id == $scope.idTipoDocumentoIdentidadRuc ? $scope.longitudCaracteresTipoDocumentoIdentidad = 11 : $scope.longitudCaracteresTipoDocumentoIdentidad = 50;
//    }

//    // Numero de documento - Validar numero de documento
//    $scope.validarNumero = function () {

//        $scope.empleado.TipoDocumentoIdentidad.Id === $scope.idTipoDocumentoIdentidadDni ? $scope.longitudCaracteresTipoDocumentoIdentidad = 8 :
//            $scope.empleado.TipoDocumentoIdentidad.Id === $scope.idTipoDocumentoIdentidadRuc ? $scope.longitudCaracteresTipoDocumentoIdentidad = 11 :
//                $scope.longitudCaracteresTipoDocumentoIdentidad = 50;

//        if ($scope.empleado.NumeroDocumentoIdentidad != null && $scope.empleado.TipoDocumentoIdentidad != null) {
//            empleadoService.validar({ idTipoDocumento: $scope.empleado.TipoDocumentoIdentidad.Id, numeroDocumento: $scope.empleado.NumeroDocumentoIdentidad }).success(function (data) {
//                $scope.dataResult = data;
//                if ($scope.dataResult.respuesta == 1) {
//                    confirm("Ya existe un empleado con el número de documento ingresado, Desea actualizar sus datos").then(
//                        function () {
//                            $scope.cargarEmpleado();
//                        },
//                        function () {
//                            console.debug("cancel");
//                        }
//                    );
//                }
//                if ($scope.dataResult.respuesta == 2) {
//                    alert("Ya existe un registro con el número de documentos ingresados y seran usados para el nuevo Empleado");
//                    $scope.cargar();
//                }
//                if ($scope.dataResult.respuesta == 3) {
//                    $scope.cargarDataApi();
//                    $.notify({ title: '', message: 'Número de documento valido' }, { type: 'success' });
//                }
//            }).error(function (data) {
//                $.notify({ title: '', message: data.mensaje }, { type: 'error' });
//            });
//        }
//    }
//    $scope.removeSpacesInPaste = function (e) {
//        var item = e.clipboardData.items[0];
//        item.getAsString(function (data) {
//            $scope.empleado.NumeroDocumentoIdentidad = data.split(' ').join('');
//            $scope.$apply();
//        });
//    }
//    //Apellido paterno - Apellido materno - nombre - ubigeo - detalle
//    $scope.cargarDataApi = function () {
//        $scope.empleado.ApellidoPaterno = $scope.dataResult.dataApi.ApellidoPaterno;
//        $scope.empleado.ApellidoMaterno = $scope.dataResult.dataApi.ApellidoMaterno;
//        $scope.empleado.Nombres = $scope.dataResult.dataApi.Nombres;
//        $scope.direccion.Ubigeo = $scope.obtenerUbigeo($scope.dataResult.dataApi.Ubigeo);
//        $scope.direccion.Detalle = $scope.dataResult.dataApi.Direccion;
//    }

//   //GUARDAR EMPLEADO
//    $scope.guardar = function () {
//        $scope.resolverDireccion();
//        empleadoService.guardarEmpleado({ empleado: $scope.empleado, usuario: $scope.model }).success(function (data) {
//            $scope.messageSuccess(data.result_description);
//            $scope.listarEmpleados();
//            if ($scope.guardadoParaFuera) {
//                var modeloScope = angular.element('#modelo').scope();
//                $scope.NombresYApellidos = $scope.empleado.ApellidoPaterno + ' ' + $scope.empleado.ApellidoMaterno + ' ' + $scope.empleado.Nombres;
//                switch ($scope.tipoRegistroFuera) {
//                    case 1:
//                        modeloScope.actualizarEmisoresIngresadosYEditados(!$scope.estado, data.data, $scope.empleado.NumeroDocumentoIdentidad, $scope.NombresYApellidos);
//                        break;
//                    case 2:
//                        modeloScope.actualizarPagadoresBeneficiariosIngresadosYEditados(!$scope.estado, data.data, $scope.empleado.NumeroDocumentoIdentidad, $scope.NombresYApellidos);
//                        break;
//                    default:
//                        break;
//                }
//            }
//            $scope.iniciarRegistroEmpleado();
//            $('#modal-registro').modal('hide');
//        }).error(function (data) {
//            $scope.messageError(data.error);
//        });
//    }

//    $scope.resolverDireccion = function () {
//        if (!$scope.estado) {
//            if ($scope.empleado.Direcciones != null) {
//                $scope.empleado.Direcciones[0].Ubigeo = $scope.direccion.Ubigeo;
//                $scope.empleado.Direcciones[0].Detalle = $scope.direccion.Detalle;
//            } else {
//                $scope.empleado.Direcciones = [];
//                $scope.direccion.esVigente = true;
//                $scope.direccion.esPrincipal = true;
//                $scope.direccion.Detalle = " ";
//                $scope.empleado.Direcciones.push($scope.direccion);

//            }
    
//        } else {
//            if ($scope.direccion.Ubigeo != undefined) {
//                $scope.empleado.Direcciones = [];
//                $scope.direccion.esVigente = true;
//                $scope.direccion.esPrincipal = true;
//                $scope.direccion.Detalle = " ";
//                $scope.empleado.Direcciones.push($scope.direccion);
//            }

//        }
//    }

//    //OBTENER EMPLEADOS - BANDEJA 
//    $scope.listarEmpleados = function () {
//        empleadoService.listarEmpleados({}).success(function (data) {
//            $scope.empleados.lista = data;
//        }).error(function (data) {
//            $scope.messageError(data.error);
//        });
//    }

//    //OBTENER EMPLEADO - EDITAR
//    $scope.editar = function (id) {
//        $scope.estado = false;
//        $scope.empleado = { Direcciones: [], Roles: [] };
//        $scope.direccion = {};
//        $scope.iniciarRegistroEmpleado();

//        empleadoService.cargarEmpleado({ idEmpleado: id }).success(function (data) {
//            $scope.empleado = data;
//            if ($scope.empleado.Direcciones != null) {
//                if ($scope.empleado.Direcciones.length > 0) {
//                    $scope.direccion = $scope.empleado.Direcciones[0];
//                }
//            }

//            $timeout(function () {
//                $('#rol-empleado').trigger("change");
//            }, 100);

//            if ($scope.empleado.FechaNacimiento != null) {
//                var date = $scope.empleado.FechaNacimiento.slice($scope.empleado.FechaNacimiento.indexOf("(") + 1, $scope.empleado.FechaNacimiento.indexOf(")"));
//                $scope.empleado.FechaNacimiento = $scope.formatDate(new Date(+date), "ES");
//                $(".datepicker").datepicker('setDate', $scope.empleado.FechaNacimiento);
//            }
//            $scope.empleadoInicial = angular.copy($scope.empleado);
//        }).error(function (data) {
//            $scope.messageError(data.error);
//        });
//    }
//    $scope.obtenerUbigeo = function (data) {
//        for (var i = 0; i < $scope.ubigeosPeru.length; i++) {
//            if ($scope.ubigeosPeru[i].Id == data) {
//                return $scope.ubigeosPeru[i];
//            }
//        }
//        return $scope.ubigeosPeru[0];
//    }
//    $scope.cargarRoles = function () {
//        if ($scope.model.Rol != null) {
//            $scope.model.Roles = [{ RoleName: {} }];
//            for (var i = 0; i < $scope.model.Rol.length; i++) {
//                $scope.model.Roles[i].RoleName = $scope.model.Rol[i].Value;
//            }
//        }
//    }
//    $scope.cargarEmpleado = function () {
//        $scope.empleado = $scope.dataResult.empleado;
//        //Aqui iria las direcciones
//        if ($scope.empleado.FechaNacimiento != null) {
//            var date = $scope.empleado.FechaNacimiento.slice($scope.empleado.FechaNacimiento.indexOf("(") + 1, $scope.empleado.FechaNacimiento.indexOf(")"));
//            $scope.empleado.FechaNacimiento = $scope.formatDate(new Date(+date), "ES");
//            $(".datepicker").datepicker('setDate', $scope.empleado.FechaNacimiento);
//        }
//    }
//    $scope.cargar = function () {
//        $scope.empleado = $scope.dataResult.actor;
//        if ($scope.empleado.FechaNacimiento != null) {

//            var date = $scope.empleado.FechaNacimiento.slice($scope.empleado.FechaNacimiento.indexOf("(") + 1, $scope.empleado.FechaNacimiento.indexOf(")"));
//            $scope.empleado.FechaNacimiento = $scope.formatDate(new Date(+date), "ES");
//            $(".datepicker").datepicker('setDate', $scope.empleado.FechaNacimiento);
//        }
//    }
//    $scope.activarEditado = function (model) {
//        model.editar = true;
//    }
//    $scope.actualizar = function (model) {
//        model.editar = false;
//    }

//    //ELIMINAR EMPLEADO
//    $scope.eliminar = function (id) {
//        empleadoService.eliminarEmpleado({ IdEmpleado: id }).success(function (data) {
//            $scope.messageSuccess(data.result_description);
//            $scope.listarEmpleados();
//        }).error(function (data) {
//            $scope.messageError(data.error);
//        });
//    }

//    //DIRECCION Y UBIGEO
//    $scope.agregarDireccion = function () {
//        $scope.empleado.Direcciones.push($scope.direccion);
//        $scope.direccion = {};
//    }
//    $scope.eliminarDireccion = function (index) {
//        $scope.empleado.Direcciones.splice(index);
//    }

//    $scope.copiarCorreoAUsuario = function () {
//        $scope.model.Email = angular.copy($scope.empleado.Correo);
//    }
//    $scope.copiarCorreoAEmpleado = function () {
//        $scope.empleadoCorreo = angular.copy($scope.model.Email);
//    }
//    $scope.cargarModelo = function (item) {
//       $scope.modelo = angular.copy(item);
//    }

//    //MODAL
//    $scope.closeModal = function () {
//        $('#modal-registro').modal('hide');
//    }
//    $scope.close = function () {
//        if (angular.equals($scope.empleado, $scope.empleadoInicial)) {
//            $('#modal-registro').modal('hide');
//        }
//        else {
//            $('#modal-pregunta').click();
//        }
//    }
//    //MANEJO  DE LA TABLA 
//    $rootScope.dtOptions.withLightColumnFilter({
//        '0': { type: 'text', className: 'form-control' },
//        '1': { type: 'text', className: 'form-control' },
//        '2': { type: 'text', className: 'form-control padding-left-right-1' },
//        '3': { type: 'text', className: 'form-control padding-left-right-1' },
//        '4': { type: 'text', className: 'form-control padding-left-right-1' },
//        '5': { type: 'text', className: 'form-control padding-left-right-1' },
//        '6': { type: 'text', className: 'form-control padding-left-right-1' },
//        '7': { type: 'text', className: 'form-control padding-left-right-1' },
//        '8': { type: 'text', className: 'form-control padding-left-right-1' },

//    });
    
//    $scope.obtenerRolesPersonal = function () {
//        maestroService.obtenerRolesPersonal({}).success(function (data) {
//            $scope.rolesPersonal = data;
//        }).error(function (data) {
//            $scope.messageError(data.error);
//        });
//    }

//    //NO SE USA
//    $scope.obtenerRoles = function () {
//        maestroService.obtenerRoles({}).success(function (data) {
//            $scope.rolesUsuario = data;
//        }).error(function (data) {
//            $scope.messageError(data.error);
//        });
//    }

//    $scope.obtenerRoles();
//    $scope.obtenerRolesPersonal();

//    $scope.inicializarDesdeFuera = function () {
//        var defered = $q.defer();
//        var promise = defered.promise;
//        $scope.iniciarRegistroEmpleado();
//        defered.resolve();
//        //$scope.cargarParametros();
//        //$scope.cargarColeccionesAsync();
//        //$scope.cargarColeccionesSync().then(function (resultado_) {
//        //    $scope.establecerDatosPorDefecto();
//        //    $scope.actorInicial = angular.copy($scope.actor);
//        //    defered.resolve();
//        //}, function (error) {
//        //    $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
//        //    defered.reject(data);
//        //});
//        return promise;
//    }

//    $scope.nuevoRegistroEmisorEnIngresoEgresoVarios = function () {
//        $scope.inicializarDesdeFuera().then(function (resultado_) {
//            $scope.guardadoParaFuera = true;
//            $scope.tipoRegistroFuera = 1;
//        }, function (error) {
//            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
//        });
//    }

//    $scope.editarEmisorEnIngresoEgresoVarios = function (id) {
//        $scope.editar(id);
//        $scope.guardadoParaFuera = true;
//        $scope.tipoRegistroFuera = 1;
//    }

//    $scope.nuevoRegistroPagadorBeneficiarioEnIngresoEgresoVarios = function () {
//        $scope.inicializarDesdeFuera().then(function (resultado_) {
//            $scope.guardadoParaFuera = true;
//            $scope.tipoRegistroFuera = 2;
//        }, function (error) {
//            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
//        });
//    }

//    $scope.editarPagadorBeneficiarioEnIngresoEgresoVarios = function (id) {
//        $scope.editar(id);
//        $scope.guardadoParaFuera = true;
//        $scope.tipoRegistroFuera = 2;
//    }

//});
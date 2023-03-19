app.controller('proveedorController', function ($scope, $q, $compile, $rootScope, SweetAlert, confirm, $timeout, blockUI, DTOptionsBuilder, DTColumnBuilder, proveedorService, actorComercialService) {

    $scope.inicializar = function () {
        $scope.limpiar();
        $scope.cargarParametros();
        $scope.cargarColeccionesAsync();
    }

    $scope.limpiar = function () {
        $scope.proveedor = {
            DomicilioFiscal: {},
            TipoDocumentoIdentidad: {}
        };
    }

    $scope.cargarParametros = function () {
        $scope.rolProveedor = { Id: idRolProveedor, Nombre: 'PROVEEDOR' };
        $scope.idProveedorGenerico = idProveedorGenerico;
        $scope.mascaraDeVisualizacionValidacionRegistroProveedor = mascaraDeVisualizacionValidacionRegistroProveedor;
    }

    $scope.cargarColeccionesAsync = function () {
        $scope.listarProveedores();
    }

    $scope.listarProveedores = function () {
        proveedorService.listarProveedores({}).success(function (data) {
            $scope.proveedores = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.establecerDatosPorDefecto = function () {
    }

    $scope.nuevoProveedor = function () {
        $scope.registradorActorComercialAPI.AgregarActorComercial($scope.proveedor);
    };

    $scope.editarProveedor = function (id) {
        actorComercialService.obtenerActorComercialPorId({ idRol: $scope.rolProveedor.Id, id: id }).success(function (data) {
            $scope.proveedor = data.information;
            $scope.registradorActorComercialAPI.EditarActorComercial($scope.proveedor);
        }).error(function (data) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    };

    $scope.cargarActorRegistardo = function (actorComercial) {
        $scope.limpiar();
        $scope.listarProveedores();
    };

    $scope.cargarEliminarProveedor = function (item) {
        $scope.actor = angular.copy(item);
    }

    $scope.eliminarProveedor = function (id) {
        proveedorService.eliminarProveedor({ IdProveedor: id }).success(function (data) {
            $scope.messageSuccess(data.result_description);
            $scope.listarProveedores();
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $rootScope.dtOptions.withLightColumnFilter({
        '1': { type: 'text', className: 'form-control padding-left-right-3' },
        '2': { type: 'text', className: 'form-control padding-left-right-3' },
        '3': { type: 'text', className: 'form-control padding-left-right-3' },
        '4': { type: 'text', className: 'form-control padding-left-right-3' },
        '5': { type: 'text', className: 'form-control padding-left-right-3' },
    });
});


 

//app.controller('', function ($scope, $q, $compile, SweetAlert, confirm, $timeout, blockUI, DTOptionsBuilder, DTColumnBuilder, , maestroService, ventaService) {


//    $scope.iniciarBandejaProveedores = function () {
//        $scope.listarProveedores();
//    }

//    $scope.listarProveedores = function () {
//        proveedorService.listarProveedores({}).success(function (data) {
//            $scope.proveedores = data;
//        }).error(function (data) {
//            $scope.messageError(data.error);
//        });
//    }

//    //********** REGISTRO DE PROVEEDORES **********//

//    $scope.inicializar = function () {
//        $scope.limpiar();
//        $scope.cargarParametros();
//        $scope.cargarColeccionesAsync();
//        $scope.cargarColeccionesSync().then(function (resultado_) {
//            $scope.establecerDatosPorDefecto();
//            $scope.actorInicial = angular.copy($scope.actor);
//        }, function (error) {
//            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
//        });
//    }

//    $scope.limpiar = function () {
//        $scope.actor = { Direcciones: [] };
//        $scope.direccion = {};
//        $scope.actorInicial = {};
//    }

//    $scope.cargarParametros = function () {
//        $scope.idTipoActorPersonaJuridica = idTipoActorPersonaJuridica;
//        $scope.idTipoActorPersonaNatural = idTipoActorPersonaNatural;
//        $scope.idProveedorGenerico = idProveedorGenerico;
//        $scope.idTipoPersonaSeleccionadaPorDefecto = idTipoPersonaSeleccionadaPorDefecto;
//        $scope.idUbigeoSeleccionadoPorDefecto = idUbigeoSeleccionadoPorDefecto;
//        $scope.idUbigeoNoEspecificado = idUbigeoNoEspecificado;
//        $scope.idTipoDocumentoSeleccionadaConTipoPersonaNatural = idTipoDocumentoSeleccionadaConTipoPersonaNatural;
//        $scope.idTipoDocumentoSeleccionadaConTipoPersonaJuridica = idTipoDocumentoSeleccionadaConTipoPersonaJuridica;
//        $scope.idTipoDocumentoIdentidadDni = idTipoDocumentoIdentidadDni;
//        $scope.idTipoDocumentoIdentidadRuc = idTipoDocumentoIdentidadRuc;
//    }

//    $scope.cargarColeccionesAsync = function () {

//    }

//    $scope.cargarColeccionesSync = function () {
//        var defered = $q.defer();
//        var promise = defered.promise;
//        var promiseList = [];
//        promiseList.push($scope.obtenerTiposDePersona());
//        promiseList.push($scope.obtenerTiposDeDocumentosIdentidad());
//        promiseList.push($scope.obtenerUbigeoDistrito());
//        return $q.all(promiseList).then(function (response) {
//            defered.resolve();
//        }).catch(function (error) {
//            defered.reject(e);
//        });
//        return promise;
//    }

//    $scope.establecerDatosPorDefecto = function () {
//        $scope.mostrarApellidoPaternoMaternoYNombre = false;
//        $scope.edicionProveedor = false;
//        $scope.guardadoParaFuera = false;
//        $scope.tipoRegistroFuera = 0; //ProveedorCompraGasto(1) 
//        $scope.longitudCaracteresTipoDocumentoIdentidad = 0;
//        $scope.establecerTipoPersonaPorDefecto();
//        $scope.establecerUbigeoPorDefecto();

//    }

//    $scope.establecerTipoPersonaPorDefecto = function () {
//        var tipoDePersona = Enumerable.from($scope.tiposDeActor)
//            .where("$.Id == '" + $scope.idTipoPersonaSeleccionadaPorDefecto + "'").toArray()[0];
//        $scope.actor.TipoPersona = tipoDePersona != null ? tipoDePersona : $scope.tiposDeActor[0];
//        $scope.cargarDatosComplementarios();

//        if ($scope.idTipoActorPersonaJuridica == $scope.actor.TipoPersona.Id) {
//            $scope.mostrarApellidoPaternoMaternoYNombre = false;
//            $scope.seleccionarTipoDocumento($scope.idTipoDocumentoSeleccionadaConTipoPersonaJuridica);
//        } else {
//            $scope.mostrarApellidoPaternoMaternoYNombre = true;
//            $scope.seleccionarTipoDocumento($scope.idTipoDocumentoSeleccionadaConTipoPersonaNatural);
//        }

//        //$timeout(function () { $('#tipoPersona').trigger("change"); }, 100);
//    }

//    $scope.establecerUbigeoPorDefecto = function () {
//        var ubigeo = Enumerable.from($scope.ubigeosPeru)
//            .where("$.Id == '" + $scope.idUbigeoSeleccionadoPorDefecto + "'").toArray()[0];
//        $scope.direccion.Ubigeo = ubigeo != null ? ubigeo : $scope.ubigeosPeru[0];
//        $timeout(function () { $('.ubigeo').trigger("change"); }, 100);
//    }



//    $scope.obtenerTiposDePersona = function () {
//        var defered = $q.defer();
//        var promise = defered.promise;
//        maestroService.listarTiposDeActor({}).success(function (data) {
//            $scope.tiposDeActor = data;
//            defered.resolve();
//        }).error(function (data) {
//            $scope.messageError(data.error);
//            defered.reject(data);
//        });
//        return promise;
//    }

//    $scope.obtenerTiposDeDocumentosIdentidad = function () {
//        var defered = $q.defer();
//        var promise = defered.promise;
//        maestroService.listarTiposDeDocumentosDeIdentidad({}).success(function (data) {
//            $scope.tiposDeDocumentoDeIdentidad = data;
//            defered.resolve();
//        }).error(function (data) {
//            $scope.messageError(data.error);
//            defered.reject(data);
//        });
//        return promise;
//    }

//    $scope.obtenerUbigeoDistrito = function () {
//        var defered = $q.defer();
//        var promise = defered.promise;
//        maestroService.listarUbigeoDistrito().success(function (data) {
//            $scope.ubigeosPeru = data;
//            defered.resolve();
//        }).error(function (data) {
//            $scope.messageError(data.error);
//            defered.reject(data);
//        });
//        return promise;
//    }

//    $scope.listarTiposDeClaseActor = function (IdTipoDeActor) {
//        maestroService.listarTiposDeClaseActor({ IdTipoDeActor: IdTipoDeActor }).success(function (data) {
//            $scope.tiposDeClaseActor = data;
//            if (!$scope.edicionProveedor) {
//                $scope.actor.ClaseActor = $scope.tiposDeClaseActor[0];
//            }
//        }).error(function (data) {
//            $scope.messageError(data.error);
//        });
//    }

//    $scope.listarTiposDeEstadoLegal = function (IdTipoDeActor) {
//        maestroService.listarTiposDeEstadoLegalActor({ IdTipoDeActor: IdTipoDeActor }).success(function (data) {
//            $scope.tiposDeEstadoLegal = data;
//            if (!$scope.edicionProveedor) {
//                $scope.actor.EstadoLegalActor = $scope.tiposDeEstadoLegal[0];
//            }
//        }).error(function (data) {
//            $scope.messageError(data.error);
//        });
//    }

//    $scope.cargarDatosComplementarios = function () {

//        $scope.setDenominacionClaseActor($scope.actor.TipoPersona.Id);
//        $scope.listarTiposDeClaseActor($scope.actor.TipoPersona.Id);
//        $scope.listarTiposDeEstadoLegal($scope.actor.TipoPersona.Id);


//        if ($scope.actor.NumeroDocumentoIdentidad && $scope.actor.NumeroDocumentoIdentidad.length == 11 && $scope.idTipoActorPersonaNatural == $scope.actor.TipoPersona.Id) {
//            $scope.mostrarApellidoPaternoMaternoYNombre = true;
//            $scope.seleccionarTipoDocumento($scope.idTipoDocumentoSeleccionadaConTipoPersonaJuridica);
//        } else {

//            if ($scope.actor.NumeroDocumentoIdentidad && $scope.actor.NumeroDocumentoIdentidad.length == 11 || $scope.idTipoActorPersonaJuridica == $scope.actor.TipoPersona.Id) {
//                $scope.mostrarApellidoPaternoMaternoYNombre = false;
//                $scope.seleccionarTipoDocumento($scope.idTipoDocumentoSeleccionadaConTipoPersonaJuridica);
//            } else {
//                $scope.mostrarApellidoPaternoMaternoYNombre = true;
//                $scope.seleccionarTipoDocumento($scope.idTipoDocumentoSeleccionadaConTipoPersonaNatural);
//            }

//        }
//        $timeout(function () { $('.tipoDocumento').trigger("change"); }, 100);
//    }

//    $scope.setDenominacionClaseActor = function (IdTipoDeActor) {
//        maestroService.obtenerDenominacionClaseActor({ IdTipoDeActor: IdTipoDeActor }).success(function (data) {
//            $scope.denominacionClaseActor = data;
//        }).error(function (data) {
//            $scope.messageError(data.error);
//        });
//    }

//    $scope.seleccionarTipoDocumento = function (idTipoDocumento) {
//        for (var i = 0; i < $scope.tiposDeDocumentoDeIdentidad.length; i++) {
//            if ($scope.tiposDeDocumentoDeIdentidad[i].Id == idTipoDocumento) {
//                $scope.actor.TipoDocumentoIdentidad = $scope.tiposDeDocumentoDeIdentidad[i];
//                break;
//            }
//        }
//        $scope.actor.TipoDocumentoIdentidad.Id == $scope.idTipoDocumentoIdentidadDni ? $scope.longitudCaracteresTipoDocumentoIdentidad = 8 :
//            $scope.actor.TipoDocumentoIdentidad.Id == $scope.idTipoDocumentoIdentidadRuc ? $scope.longitudCaracteresTipoDocumentoIdentidad = 11 : $scope.longitudCaracteresTipoDocumentoIdentidad = 50;
//    }

//    $scope.seleccionarTipoPersona = function (idTipoPersona) {
//        for (var i = 0; i < $scope.tiposDeActor.length; i++) {
//            if ($scope.tiposDeActor[i].Id == idTipoPersona) {
//                $scope.actor.TipoPersona = $scope.tiposDeActor[i];
//                break;
//            }
//        }
//    }


//    $scope.removeSpacesInPaste = function (e) {
//        var item = e.clipboardData.items[0];
//        item.getAsString(function (data) {
//            $scope.actor.NumeroDocumentoIdentidad = data.split(' ').join('');
//            $scope.$apply();
//        });
//    }

//    $scope.validarLongitudNumero = function () {
//        $scope.actor.TipoDocumentoIdentidad.Id == $scope.idTipoDocumentoIdentidadDni ? $scope.longitudCaracteresTipoDocumentoIdentidad = 8 :
//            $scope.actor.TipoDocumentoIdentidad.Id == $scope.idTipoDocumentoIdentidadRuc ? $scope.longitudCaracteresTipoDocumentoIdentidad = 11 : $scope.longitudCaracteresTipoDocumentoIdentidad = 50;
//    }

//    $scope.validarNumero = function () {

//        $scope.actor.TipoDocumentoIdentidad.Id == $scope.idTipoDocumentoIdentidadDni ? $scope.longitudCaracteresTipoDocumentoIdentidad = 8 :
//            $scope.actor.TipoDocumentoIdentidad.Id == $scope.idTipoDocumentoIdentidadRuc ? $scope.longitudCaracteresTipoDocumentoIdentidad = 11 : $scope.longitudCaracteresTipoDocumentoIdentidad = 50;

//        if ($scope.actor.NumeroDocumentoIdentidad != null && $scope.actor.TipoDocumentoIdentidad != null) {
//            proveedorService.validar({ idTipoDocumento: $scope.actor.TipoDocumentoIdentidad.Id, numeroDocumento: $scope.actor.NumeroDocumentoIdentidad }).success(function (data) {
//                $scope.dataResult = data;
//                if ($scope.dataResult.respuesta == 1) {
//                    if (!$scope.edicionProveedor)
//                        SweetAlert.warning("Adventencia","Ya existe un Proveedor con el número de documento ingresado, Se va a actualizar sus datos");
//                    $scope.cargarProveedor();
//                }
//                if ($scope.dataResult.respuesta == 2) {
//                    SweetAlert.warning("Advertencia","Ya existe un registro con el número de documento ingresado y seran usados para el nuevo Proveedor");
//                    $scope.cargar();
//                }
//                if ($scope.dataResult.respuesta == 3) {
//                    $scope.cargarDataApi();
//                    SweetAlert.success("Correcto", "Número de documento valido");
//                }
//                if ($scope.dataResult.respuesta == 5) {
//                    SweetAlert.warning("Advertencia", "El numero de documento RUC ingresado no es correcto" );
//                }
//                $timeout(function () { $('.ubigeo').trigger("change"); }, 100);
//            }).error(function (data) {
//                    SweetAlert.warning("Advertencia", data.mensaje);
//            });
//        }
//    }

//    $scope.inicializarDesdeFuera = function () {
//        var defered = $q.defer();
//        var promise = defered.promise;
//        $scope.limpiar();
//        $scope.cargarParametros();
//        $scope.cargarColeccionesAsync();
//        $scope.cargarColeccionesSync().then(function (resultado_) {
//            $scope.establecerDatosPorDefecto();
//            $scope.actorInicial = angular.copy($scope.actor);
//            defered.resolve();
//        }, function (error) {
//            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
//            defered.reject(data);
//        });
//        return promise;
//    }

//    $scope.nuevoRegistroProveedorEnCompra = function () {
//        $scope.inicializarDesdeFuera().then(function (resultado_) {
//            $scope.guardadoParaFuera = true;
//            $scope.tipoRegistroFuera = 1;
//        }, function (error) {
//            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
//        });
//    }

//    $scope.editarProveedorEnCompra = function (id) {
//        $scope.editarRegistro(id);
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
//        $scope.editarRegistro(id);
//        $scope.guardadoParaFuera = true;
//        $scope.tipoRegistroFuera = 2;
//    }

//    $scope.nuevoRegistroTransportistaEnMovimientoDeAlmacen = function () {
//        $scope.inicializarDesdeFuera().then(function (resultado_) {
//            $scope.guardadoParaFuera = true;
//            $scope.tipoRegistroFuera = 3;
//        }, function (error) {
//            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
//        });
//    }

//    $scope.editarTransportistaEnMovimientoDeAlmacen = function (id) {
//        $scope.editarRegistro(id);
//        $scope.guardadoParaFuera = true;
//        $scope.tipoRegistroFuera = 3;
//    }
//    $scope.nuevoRegistroProveedorEnGasto = function () {
//        $scope.inicializarDesdeFuera().then(function (resultado_) {
//            $scope.guardadoParaFuera = true;
//            $scope.tipoRegistroFuera = 4;
//        }, function (error) {
//            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
//        });
//    }

//    $scope.editarProveedorEnGasto = function (id) {
//        $scope.editarRegistro(id);
//        $scope.guardadoParaFuera = true;
//        $scope.tipoRegistroFuera = 4;
//    }
//    $scope.cargarProveedor = function () {
//        $scope.actor = $scope.dataResult.proveedor;
//    }

//    $scope.cargar = function () {

//        $scope.actor = $scope.dataResult.actor;

//        if ($scope.actor.Direcciones.length > 0) {
//            $scope.direccion.Ubigeo = $scope.actor.Direcciones[0].Ubigeo;
//            $scope.direccion.Detalle = $scope.actor.Direcciones[0].Detalle;
//            $scope.verificarUbigeoSeleccionado();
//        }
//    }

//    $scope.cargarDataApi = function () {
//        $scope.actor.ApellidoPaterno = $scope.dataResult.dataApi.ApellidoPaterno;
//        $scope.actor.ApellidoMaterno = $scope.dataResult.dataApi.ApellidoMaterno;
//        $scope.actor.Nombres = $scope.dataResult.dataApi.Nombres;
//        $scope.actor.RazonSocial = $scope.dataResult.dataApi.RazonSocial;
//        $scope.actor.NombreComercial = $scope.dataResult.dataApi.NombreComercial;
//        $scope.direccion.Ubigeo = $scope.obtenerUbigeo($scope.dataResult.dataApi.Ubigeo);
//        $scope.direccion.Detalle = $scope.dataResult.dataApi.Direccion == null ? '-' : $scope.dataResult.dataApi.Direccion;
//    }

//    $scope.obtenerUbigeo = function (data) {
//        for (var i = 0; i < $scope.ubigeosPeru.length; i++) {
//            if ($scope.ubigeosPeru[i].Id == data) {
//                return $scope.ubigeosPeru[i];
//            }
//        }
//        return Enumerable.from($scope.ubigeosPeru).where("$.Id == '" + $scope.idUbigeoNoEspecificado + "'").toArray()[0];
//        //var ubigeo = Enumerable.from($scope.ubigeosPeru).where("$.Nombre == '" + data + "'").toArray()[0];
//        //return $scope.ubigeoSeleccionado != null ? $scope.ubigeoSeleccionado : Enumerable.from($scope.ubigeosPeru).where("$.Id == '" + $scope.idUbigeoNoEspecificado + "'").toArray()[0];
//    }

//    $scope.verificarUbigeoSeleccionado = function () {
//        if ($scope.direccion.Ubigeo.Id == $scope.idUbigeoNoEspecificado) {
//            $scope.ubigeoSeleccionadoNoEspecificado = true;
//        } else {
//            $scope.ubigeoSeleccionadoNoEspecificado = false;
//        }
//    }

//    $scope.editarRegistro = function (id) {
//        proveedorService.cargarProveedor({ idProveedor: id }).success(function (data) {
//            $scope.limpiar();
//            $scope.cargarParametros();
//            $scope.cargarColeccionesAsync();
//            $scope.cargarColeccionesSync().then(function (resultado_) {
//                $scope.edicionProveedor = true;
//                $scope.actor = data;
//                $scope.setDenominacionClaseActor(data.TipoPersona.Id);
//                $scope.listarTiposDeClaseActor(data.TipoPersona.Id);
//                $scope.listarTiposDeEstadoLegal(data.TipoPersona.Id);
//                $scope.mostrarApellidoPaternoMaternoYNombre = ($scope.idTipoActorPersonaJuridica == $scope.actor.TipoPersona.Id) ? false : true;
//                $scope.actor.TipoDocumentoIdentidad.Id == $scope.idTipoDocumentoIdentidadDni ? $scope.longitudCaracteresTipoDocumentoIdentidad = 8 :
//                    $scope.actor.TipoDocumentoIdentidad.Id == $scope.idTipoDocumentoIdentidadRuc ? $scope.longitudCaracteresTipoDocumentoIdentidad = 11 : $scope.longitudCaracteresTipoDocumentoIdentidad = 50;
//                if ($scope.actor.Direcciones.length > 0) {
//                    $scope.direccion.Ubigeo = $scope.actor.Direcciones[0].Ubigeo;
//                    $scope.direccion.Detalle = $scope.actor.Direcciones[0].Detalle;
//                    $scope.verificarUbigeoSeleccionado();
//                }
//                $scope.actorInicial = angular.copy($scope.actor);
//            }, function (error) {
//                $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
//            });
//        }).error(function (data) {
//            $scope.messageError(data.error);
//        });
//    }

//    $scope.guardar = function () {
//        $scope.resolverDireccion();
//        proveedorService.guardarProveedor({ proveedor: $scope.actor }).success(function (data) {
//            SweetAlert.success("Correcto", data.result_description);
//            if ($scope.guardadoParaFuera) {
//                var modeloScope = angular.element('#modelo').scope();
//                $scope.RazonSocial = $scope.idTipoActorPersonaJuridica == $scope.actor.TipoPersona.Id ? $scope.actor.RazonSocial : $scope.actor.ApellidoPaterno + ' ' + $scope.actor.ApellidoMaterno + ' ' + $scope.actor.Nombres;
//                switch ($scope.tipoRegistroFuera) {
//                    case 1:
//                        $('#modal-registro-proveedor').modal('hide');
//                        modeloScope.actualizarProveedoresIngresadosYEditados($scope.edicionProveedor, data.data, $scope.actor.NumeroDocumentoIdentidad, $scope.RazonSocial);
//                        break;
//                    case 2:
//                        $('#modal-registro-proveedor').modal('hide');
//                        modeloScope.actualizarPagadoresBeneficiariosIngresadosYEditados($scope.edicionProveedor, data.data, $scope.actor.NumeroDocumentoIdentidad, $scope.RazonSocial);
//                        break;
//                    case 3:
//                        $('#modal-registro-proveedor').modal('hide');
//                        modeloScope.actualizarTransportistaIngresadosYEditados($scope.edicionProveedor, data.data, $scope.actor.NumeroDocumentoIdentidad, $scope.RazonSocial);
//                        break;
//                    case 4:
//                        $('#modal-registro-proveedor').modal('hide');
//                        modeloScope.actualizarProveedoresIngresadosYEditados($scope.edicionProveedor, data.data, $scope.actor.NumeroDocumentoIdentidad, $scope.RazonSocial);
//                        break;
//                    default:
//                        break;
//                }
//            } else {
//                $scope.listarProveedores();
//            }
//            $scope.limpiar();
//            $('#modal-registro-proveedor').modal('hide');
//        }).error(function (data) {
//            SweetAlert.error("Ocurrio un problema", data.error);
//        });
//    }

//    $scope.resolverDireccion = function () {
//        if ($scope.edicionProveedor && $scope.actor.Direcciones.length > 0) {
//            $scope.actor.Direcciones[0].Ubigeo = $scope.direccion.Ubigeo;
//            $scope.actor.Direcciones[0].Detalle = $scope.direccion.Detalle;
//        } else {
//            $scope.direccion.esVigente = true;
//            $scope.direccion.esPrincipal = true;
//            $scope.direccion.Tipo = { Id: 12, Nombre: "Domicilio Fiscal" }; //Tipo por defecto el tipo de domicilio fizcal 
//            $scope.actor.Direcciones.push($scope.direccion);
//        }
//    }

//    $scope.cargarModelo = function (item) {
//        $scope.actor = angular.copy(item);
//    }

//    $scope.eliminar = function (id) {
//        proveedorService.eliminarProveedor({ IdProveedor: id }).success(function (data) {
//            $scope.messageSuccess(data.result_description);
//            $scope.listarProveedores();
//        }).error(function (data) {
//            $scope.messageError(data.error);
//        });
//    }

//    $scope.cambiarElTiPoDocumentoYElTipoDePersona = function () {
//        if ($scope.actor.NumeroDocumentoIdentidad) {
//            var longitudCaracteresDeNumeroDocumento = $scope.actor.NumeroDocumentoIdentidad.length
//            if (longitudCaracteresDeNumeroDocumento === 8) {
//                $scope.seleccionarTipoPersona($scope.idTipoActorPersonaNatural);
//                $scope.seleccionarTipoDocumento($scope.idTipoDocumentoIdentidadDni);
//            } else {
//                if (longitudCaracteresDeNumeroDocumento === 11) {
//                    var caracteresConQueEmpieza = $scope.actor.NumeroDocumentoIdentidad.substr(0, 2);
//                    if (caracteresConQueEmpieza === "10") {
//                        $scope.seleccionarTipoPersona($scope.idTipoActorPersonaNatural);
//                        $scope.seleccionarTipoDocumento($scope.idTipoDocumentoIdentidadRuc);
//                    } else {
//                        if (caracteresConQueEmpieza === "20") {
//                            $scope.seleccionarTipoPersona($scope.idTipoActorPersonaJuridica);
//                            $scope.seleccionarTipoDocumento($scope.idTipoDocumentoIdentidadRuc);
//                        } else {
//                            $scope.seleccionarTipoPersona($scope.idTipoActorPersonaJuridica);
//                            $scope.seleccionarTipoDocumento($scope.idTipoDocumentoIdentidadRuc);
//                        }
//                    }
//                }
//            }
//        }
//        $timeout(function () { $('.tipoPersona').trigger("change"); }, 100);
//        $timeout(function () { $('.tipoDocumento').trigger("change"); }, 100);
//    }

//    $scope.close = function () {
//        //if (angular.equals($scope.actor, $scope.actorInicial)) {
//            $('#modal-registro-proveedor').modal('hide');
//        //}
//        //else {
//        //    $('#modal-pregunta').click();
//        //}
//    }

//    $scope.closeModal = function () {
//        $('#modal-registro-proveedor').modal('hide');
//    }



//});
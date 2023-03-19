app.controller('clienteController', function ($scope, $q, $compile, $rootScope, SweetAlert, confirm, $timeout, blockUI, DTOptionsBuilder, DTColumnBuilder, clienteService, actorComercialService) {

    $scope.inicializar = function () {
        $scope.limpiar();
        $scope.cargarParametros();
        $scope.cargarColeccionesAsync();
    }

    $scope.limpiar = function () {
        $scope.cliente = {
            DomicilioFiscal: {},
            TipoDocumentoIdentidad: {}
        };
    }

    $scope.cargarParametros = function () {
        $scope.rolCliente = { Id: idRolCliente, Nombre: 'CLIENTE' };
        $scope.idClienteGenerico = idClienteGenerico;
        $scope.mascaraDeVisualizacionValidacionRegistroCliente = mascaraDeVisualizacionValidacionRegistroCliente;
        $scope.mascaraDatosAdicionalesEnBandejaClientes = mascaraDatosAdicionalesEnBandejaClientes;
    }

    $scope.cargarColeccionesAsync = function () {
        $scope.listarClientes();
    }

    $scope.listarClientes = function () {
        clienteService.listarClientes({}).success(function (data) {
            $scope.clientes = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.establecerDatosPorDefecto = function () {
    }

    $scope.nuevoCliente = function () {
        $scope.registradorActorComercialAPI.AgregarActorComercial($scope.cliente);
    };

    $scope.editarCliente = function (id) {
        actorComercialService.obtenerActorComercialPorId({ idRol: $scope.rolCliente.Id, id: id }).success(function (data) {
            $scope.cliente = data.information;
            $scope.registradorActorComercialAPI.EditarActorComercial($scope.cliente);
        }).error(function (data) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    };

    $scope.cargarActorRegistardo = function (actorComercial) {
        $scope.limpiar();
        $scope.listarClientes();
    };

    $scope.cargarEliminarCliente = function (item) {
        $scope.actor = angular.copy(item);
    }

    $scope.eliminarCliente = function (id) {
        clienteService.eliminarCliente({ IdCliente: id }).success(function (data) {
            $scope.messageSuccess(data.result_description);
            $scope.listarClientes();
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
        '6': { type: 'text', className: 'form-control padding-left-right-3' },
        '7': { type: 'text', className: 'form-control padding-left-right-3' },
    });
});

    /*

    $scope.establecerTipoPersonaPorDefecto = function () {
        var tipoDePersona = Enumerable.from($scope.tiposDeActor)
            .where("$.Id == '" + $scope.idTipoPersonaSeleccionadaPorDefecto + "'").toArray()[0];
        $scope.actor.TipoPersona = tipoDePersona != null ? tipoDePersona : $scope.tiposDeActor[0];
        $scope.cargarDatosComplementarios();

        if ($scope.idTipoActorPersonaJuridica == $scope.actor.TipoPersona.Id) {
            $scope.mostrarApellidoPaternoMaternoYNombre = false;
            $scope.seleccionarTipoDocumento($scope.idTipoDocumentoSeleccionadaConTipoPersonaJuridica);
        } else {
            $scope.mostrarApellidoPaternoMaternoYNombre = true;
            $scope.seleccionarTipoDocumento($scope.idTipoDocumentoSeleccionadaConTipoPersonaNatural);
        }

        //$timeout(function () { $('#tipoPersona').trigger("change"); }, 100);
    }

    $scope.establecerUbigeoPorDefecto = function () {
        var ubigeo = Enumerable.from($scope.ubigeosPeru)
            .where("$.Id == '" + $scope.idUbigeoSeleccionadoPorDefecto + "'").toArray()[0];
        $scope.direccion.Ubigeo = ubigeo != null ? ubigeo : $scope.ubigeosPeru[0];
        $timeout(function () { $('.ubigeo').trigger("change"); }, 100);
    }

    $scope.establecerComprobantePredeterminadoPorDefecto = function () {
        var comprobante = Enumerable.from($scope.tiposDeComprobantes)
            .where("$.Id == '" + $scope.idComprobantePredeterminadoPorDefecto + "'").toArray()[0];
        $scope.comprobantePredeterminado = comprobante != null ? comprobante : $scope.tiposDeComprobantes[0] ;
        $timeout(function () { $('#comprobantePreteterminado').trigger("change"); }, 100);
    }

    $scope.obtenerTiposDeComprobante = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        ventaService.obtenerTiposDeComprobanteParaClientes({}).success(function (data) {
            $scope.tiposDeComprobantes = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.obtenerTiposDePersona = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        maestroService.listarTiposDeActor({}).success(function (data) {
            $scope.tiposDeActor = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.obtenerTiposDeDocumentosIdentidad = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        maestroService.listarTiposDeDocumentosDeIdentidad({}).success(function (data) {
            $scope.tiposDeDocumentoDeIdentidad = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.obtenerUbigeoDistrito = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        maestroService.listarUbigeoDistrito().success(function (data) {
            $scope.ubigeosPeru = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.establecerTipoComprobanteAlEditar = function (id) {
        var comprobante = Enumerable.from($scope.tiposDeComprobantes).where("$.Id == '" + id + "'").toArray()[0];
        $scope.comprobantePredeterminado = comprobante;
    }

    $scope.listarTiposDeClaseActor = function (IdTipoDeActor) {
        maestroService.listarTiposDeClaseActor({ IdTipoDeActor: IdTipoDeActor }).success(function (data) {
            $scope.tiposDeClaseActor = data;
            if (!$scope.edicionCliente) {
                $scope.actor.ClaseActor = $scope.tiposDeClaseActor[0];
            }
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.listarTiposDeEstadoLegal = function (IdTipoDeActor) {
        maestroService.listarTiposDeEstadoLegalActor({ IdTipoDeActor: IdTipoDeActor }).success(function (data) {
            $scope.tiposDeEstadoLegal = data;
            if (!$scope.edicionCliente) {
                $scope.actor.EstadoLegalActor = $scope.tiposDeEstadoLegal[0];
            }
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.cargarDatosComplementarios = function () {

        $scope.setDenominacionClaseActor($scope.actor.TipoPersona.Id);
        $scope.listarTiposDeClaseActor($scope.actor.TipoPersona.Id);
        $scope.listarTiposDeEstadoLegal($scope.actor.TipoPersona.Id);

        if ($scope.actor.NumeroDocumentoIdentidad && $scope.actor.NumeroDocumentoIdentidad.length == 11 && $scope.idTipoActorPersonaNatural == $scope.actor.TipoPersona.Id) {
            $scope.mostrarApellidoPaternoMaternoYNombre = true;
            $scope.seleccionarTipoDocumento($scope.idTipoDocumentoSeleccionadaConTipoPersonaJuridica);
        }
        else {
            if ($scope.idTipoActorPersonaJuridica == $scope.actor.TipoPersona.Id) {
                $scope.mostrarApellidoPaternoMaternoYNombre = false;
                $scope.seleccionarTipoDocumento($scope.idTipoDocumentoSeleccionadaConTipoPersonaJuridica);
            } else {
                $scope.mostrarApellidoPaternoMaternoYNombre = true;
                $scope.seleccionarTipoDocumento($scope.idTipoDocumentoSeleccionadaConTipoPersonaNatural);
            }
        }
        $timeout(function () { $('.tipoDocumento').trigger("change"); }, 100);
    }

    $scope.setDenominacionClaseActor = function (IdTipoDeActor) {
        maestroService.obtenerDenominacionClaseActor({ IdTipoDeActor: IdTipoDeActor }).success(function (data) {
            $scope.denominacionClaseActor = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }


    $scope.seleccionarTipoPersona = function (idTipoPersona) {
        for (var i = 0; i < $scope.tiposDeActor.length; i++) {
            if ($scope.tiposDeActor[i].Id == idTipoPersona) {
                $scope.actor.TipoPersona = $scope.tiposDeActor[i];
                break;
            }
        }

    }

    $scope.seleccionarTipoDocumento = function (idTipoDocumento) {
        for (var i = 0; i < $scope.tiposDeDocumentoDeIdentidad.length; i++) {
            if ($scope.tiposDeDocumentoDeIdentidad[i].Id == idTipoDocumento) {
                $scope.actor.TipoDocumentoIdentidad = $scope.tiposDeDocumentoDeIdentidad[i];
                break;
            }
        }
        $scope.actor.TipoDocumentoIdentidad.Id == $scope.idTipoDocumentoIdentidadDni ? $scope.longitudCaracteresTipoDocumentoIdentidad = 8 :
            $scope.actor.TipoDocumentoIdentidad.Id == $scope.idTipoDocumentoIdentidadRuc ? $scope.longitudCaracteresTipoDocumentoIdentidad = 11 : $scope.longitudCaracteresTipoDocumentoIdentidad = 50;
    }

    $scope.removeSpacesInPaste = function (e) {
        var item = e.clipboardData.items[0];
        item.getAsString(function (data) {
            $scope.actor.NumeroDocumentoIdentidad = data.split(' ').join('');
            $scope.$apply();
        });
    }

    $scope.validarLongitudNumero = function () {
        $scope.actor.TipoDocumentoIdentidad.Id == $scope.idTipoDocumentoIdentidadDni ? $scope.longitudCaracteresTipoDocumentoIdentidad = 8 :
            $scope.actor.TipoDocumentoIdentidad.Id == $scope.idTipoDocumentoIdentidadRuc ? $scope.longitudCaracteresTipoDocumentoIdentidad = 11 : $scope.longitudCaracteresTipoDocumentoIdentidad = 50;
    }

    $scope.validarNumero = function () {

        $scope.actor.TipoDocumentoIdentidad.Id == $scope.idTipoDocumentoIdentidadDni ? $scope.longitudCaracteresTipoDocumentoIdentidad = 8 :
            $scope.actor.TipoDocumentoIdentidad.Id == $scope.idTipoDocumentoIdentidadRuc ? $scope.longitudCaracteresTipoDocumentoIdentidad = 11 : $scope.longitudCaracteresTipoDocumentoIdentidad = 50;

        if ($scope.actor.NumeroDocumentoIdentidad != null && $scope.actor.TipoDocumentoIdentidad != null) {
            clienteService.validar({ idTipoDocumento: $scope.actor.TipoDocumentoIdentidad.Id, numeroDocumento: $scope.actor.NumeroDocumentoIdentidad }).success(function (data) {
                $scope.dataResult = data;
                if ($scope.dataResult.respuesta == 1) {
                    if (!$scope.edicionCliente)
                        SweetAlert.warning("Advertencia", "Ya existe un Cliente con el número de documento ingresado, Se va a actualizar sus datos");
                    $scope.cargarCliente();
                }
                if ($scope.dataResult.respuesta == 2) {
                    SweetAlert.warning("Advertencia", "Ya existe un registro con el número de documento ingresado y seran usados para el nuevo Cliente");
                    $scope.cargar();
                }
                if ($scope.dataResult.respuesta == 3) {
                    $scope.cargarDataApi();
                    SweetAlert.success("Correcto", "Número de documento valido");
                }
                if ($scope.dataResult.respuesta == 5) {
                    SweetAlert.warning("Advertencia", "El numero de documento RUC ingresado no es correcto");
                }
                $timeout(function () { $('.ubigeo').trigger("change"); }, 100);
            }).error(function (data) {
                SweetAlert.warning("Advertencia", data.mensaje);
            });
        }
    }


    $scope.cambiarElTiPoDocumentoYElTipoDePersona = function () {
        if ($scope.actor.NumeroDocumentoIdentidad) {
            var longitudCaracteresDeNumeroDocumento = $scope.actor.NumeroDocumentoIdentidad.length
            if (longitudCaracteresDeNumeroDocumento === 8) {
                $scope.seleccionarTipoPersona($scope.idTipoActorPersonaNatural);
                $scope.seleccionarTipoDocumento($scope.idTipoDocumentoIdentidadDni);
            } else {
                if (longitudCaracteresDeNumeroDocumento === 11) {
                    var caracteresConQueEmpieza = $scope.actor.NumeroDocumentoIdentidad.substr(0, 2);
                    if (caracteresConQueEmpieza === "10") {
                        $scope.seleccionarTipoPersona($scope.idTipoActorPersonaNatural);
                        $scope.seleccionarTipoDocumento($scope.idTipoDocumentoIdentidadRuc);
                    } else {
                        if (caracteresConQueEmpieza === "20") {
                            $scope.seleccionarTipoPersona($scope.idTipoActorPersonaJuridica);
                            $scope.seleccionarTipoDocumento($scope.idTipoDocumentoIdentidadRuc);
                        } else {
                            $scope.seleccionarTipoPersona($scope.idTipoActorPersonaJuridica);
                            $scope.seleccionarTipoDocumento($scope.idTipoDocumentoIdentidadRuc);
                        }
                    }
                }
            }
        }
        $timeout(function () { $('.tipoPersona').trigger("change"); }, 100);
        $timeout(function () { $('.tipoDocumento').trigger("change"); }, 100);
    }



    $scope.inicializarDesdeFuera = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        $scope.limpiar();
        $scope.cargarParametros();
        $scope.cargarColeccionesAsync();
        $scope.cargarColeccionesSync().then(function (resultado_) {
            $scope.establecerDatosPorDefecto();
            $scope.actorInicial = angular.copy($scope.actor);
            defered.resolve();
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
            defered.reject(data);
        });
        return promise;
    }

    $scope.nuevoRegistroClienteEnVenta = function () {
        $scope.inicializarDesdeFuera().then(function (resultado_) {
            $scope.guardadoParaFuera = true;
            $scope.tipoRegistroFuera = 1;
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
    }

    $scope.editarClienteEnVenta = function (id) {
        $scope.editarRegistro(id);
        $scope.guardadoParaFuera = true;
        $scope.tipoRegistroFuera = 1;
    }
    //No se Usa, Se dejo por si talvez se llegue a usar
    $scope.nuevoRegistroEmisorEnIngresoEgresoVarios = function () {
        $scope.inicializarDesdeFuera().then(function (resultado_) {
            $scope.guardadoParaFuera = true;
            $scope.tipoRegistroFuera = 2;
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
    }
    //No se Usa, Se dejo por si talvez se llegue a usar
    $scope.editarEmisorEnIngresoEgresoVarios = function (id) {
        $scope.editarRegistro(id);
        $scope.guardadoParaFuera = true;
        $scope.tipoRegistroFuera = 2;
    }

    $scope.nuevoRegistroPagadorBeneficiarioEnIngresoEgresoVarios = function () {
        $scope.inicializarDesdeFuera().then(function (resultado_) {
            $scope.guardadoParaFuera = true;
            $scope.tipoRegistroFuera = 3;
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
    }

    $scope.editarPagadorBeneficiarioEnIngresoEgresoVarios = function (id) {
        $scope.editarRegistro(id);
        $scope.guardadoParaFuera = true;
        $scope.tipoRegistroFuera = 3;
    }

    $scope.nuevoRegistroClienteEnCotizacion = function () {
        $scope.inicializarDesdeFuera().then(function (resultado_) {
            $scope.guardadoParaFuera = true;
            $scope.tipoRegistroFuera = 4;
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
    }

    $scope.editarClienteEnCotizacion = function (id) {
        $scope.editarRegistro(id);
        $scope.guardadoParaFuera = true;
        $scope.tipoRegistroFuera = 4;
    }

    $scope.cargarCliente = function () {
        $scope.actor = $scope.dataResult.cliente;
    }

    $scope.cargar = function () {
        $scope.actor = $scope.dataResult.actor;
        if ($scope.actor.Direcciones.length > 0) {
            $scope.direccion.Ubigeo = $scope.actor.Direcciones[0].Ubigeo;
            $scope.direccion.Detalle = $scope.actor.Direcciones[0].Detalle;
            $scope.verificarUbigeoSeleccionado();
        }
    }

    $scope.cargarDataApi = function () {
        $scope.actor.ApellidoPaterno = $scope.dataResult.dataApi.ApellidoPaterno;
        $scope.actor.ApellidoMaterno = $scope.dataResult.dataApi.ApellidoMaterno;
        $scope.actor.Nombres = $scope.dataResult.dataApi.Nombres;
        $scope.actor.RazonSocial = $scope.dataResult.dataApi.RazonSocial;
        $scope.actor.NombreComercial = $scope.dataResult.dataApi.NombreComercial;
        $scope.direccion.Ubigeo = $scope.obtenerUbigeo($scope.dataResult.dataApi.Ubigeo);
        $scope.direccion.Detalle = $scope.dataResult.dataApi.Direccion == null ? '-' : $scope.dataResult.dataApi.Direccion; $scope.direccion.Ubigeo = $scope.obtenerUbigeo($scope.dataResult.dataApi.Ubigeo);
        $scope.direccion.Detalle = $scope.dataResult.dataApi.Direccion == null ? '-' : $scope.dataResult.dataApi.Direccion;
    }

    $scope.obtenerUbigeo = function (data) {
        for (var i = 0; i < $scope.ubigeosPeru.length; i++) {
            if ($scope.ubigeosPeru[i].Id == data) {
                return $scope.ubigeosPeru[i];
            }
        }
        return Enumerable.from($scope.ubigeosPeru).where("$.Id == '" + $scope.idUbigeoNoEspecificado + "'").toArray()[0];
    }

    $scope.verificarUbigeoSeleccionado = function () {
        if ($scope.direccion.Ubigeo.Id == $scope.idUbigeoNoEspecificado) {
            $scope.ubigeoSeleccionadoNoEspecificado = true;
        } else {
            $scope.ubigeoSeleccionadoNoEspecificado = false;
        }
    }


    $scope.editarRegistro = function (id) {
        clienteService.cargarCliente({ idCliente: id }).success(function (data) {
            $scope.limpiar();
            $scope.cargarParametros();
            $scope.cargarColeccionesAsync();
            $scope.cargarColeccionesSync().then(function (resultado_) {
                $scope.edicionCliente = true;
                $scope.actor = data;
                $scope.setDenominacionClaseActor(data.TipoPersona.Id);
                $scope.listarTiposDeClaseActor(data.TipoPersona.Id);
                $scope.listarTiposDeEstadoLegal(data.TipoPersona.Id);
                $scope.mostrarApellidoPaternoMaternoYNombre = ($scope.idTipoActorPersonaJuridica == $scope.actor.TipoPersona.Id) ? false : true;
                if (data.IdComprobantePredeterminado != 0) {
                    $scope.establecerTipoComprobanteAlEditar(data.IdComprobantePredeterminado);
                }
                if ($scope.actor.Direcciones.length > 0) {
                    $scope.direccion.Ubigeo = $scope.actor.Direcciones[0].Ubigeo;
                    $scope.direccion.Detalle = $scope.actor.Direcciones[0].Detalle;
                    $scope.verificarUbigeoSeleccionado();
                }
                $scope.actorInicial = angular.copy($scope.actor);
            }, function (error) {
                $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
            });
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.guardar = function () {
        $scope.resolverActor();
        clienteService.guardarCliente({ cliente: $scope.actor }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            //$scope.listarClientes();
            if ($scope.guardadoParaFuera) {
                var modeloScope = angular.element('#modelo').scope();
                $scope.RazonSocial = $scope.idTipoActorPersonaJuridica == $scope.actor.TipoPersona.Id ? $scope.actor.RazonSocial : $scope.actor.ApellidoPaterno + ' ' + $scope.actor.ApellidoMaterno + ' ' + $scope.actor.Nombres;
                switch ($scope.tipoRegistroFuera) {
                    case 1:
                        modeloScope.actualizarClientesIngresadosYEditados($scope.edicionCliente, data.data, $scope.actor.NumeroDocumentoIdentidad, $scope.RazonSocial);
                        break;
                    case 2:
                        modeloScope.actualizarEmisoresIngresadosYEditados($scope.edicionCliente, data.data, $scope.actor.NumeroDocumentoIdentidad, $scope.RazonSocial);
                        break;
                    case 3:
                        modeloScope.actualizarPagadoresBeneficiariosIngresadosYEditados($scope.edicionCliente, data.data, $scope.actor.NumeroDocumentoIdentidad, $scope.RazonSocial);
                        break;
                    case 4:
                        modeloScope.actualizarClientesIngresadosYEditados($scope.edicionCliente, data.data, $scope.actor.NumeroDocumentoIdentidad, $scope.RazonSocial);
                        break;
                    default:
                        break;
                }
            } else {
                $scope.listarClientes();
            }
            $scope.limpiar();
            $('#modal-registro-cliente').modal('hide');
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema", data.error);
        });
    }

    $scope.resolverActor = function () {
        if ($scope.edicionCliente && $scope.actor.Direcciones.length > 0) {
            $scope.actor.Direcciones[0].Ubigeo = $scope.direccion.Ubigeo;
            $scope.actor.Direcciones[0].Detalle = $scope.direccion.Detalle;
        } else {
            $scope.direccion.esVigente = true;
            $scope.direccion.esPrincipal = true;
            $scope.direccion.Tipo = { Id: 12, Nombre: "Domicilio Fiscal" }; //Tipo por defecto el tipo de domicilio fiscal 
            $scope.actor.Direcciones.push($scope.direccion);
        }
        $scope.actor.IdComprobantePredeterminado = $scope.comprobantePredeterminado.Id;
    }

    $scope.cargarModelo = function (item) {
        $scope.actor = angular.copy(item);
    }

    $scope.eliminar = function (id) {
        clienteService.eliminarCliente({ IdCliente: id }).success(function (data) {
            $scope.messageSuccess(data.result_description);
            $scope.listarClientes();
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.close = function () {
        //if (angular.equals($scope.actor, $scope.actorInicial)) {
        $('#modal-registro-cliente').modal('hide');
        //}
        //else {
        //    $('#modal-pregunta').click();
        //}
    }



    $scope.closeModal = function () {
        $('#modal-registro-cliente').modal('hide');
    }
    */







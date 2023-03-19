app.controller('centroDeAtencionController', function ($scope, $rootScope, $timeout, $q, DTOptionsBuilder, DTColumnDefBuilder, maestroService, centroDeAtencionService, SweetAlert) {

    /*----------------------------------------MANEJO SEDE---------------------------------------*/

    $scope.listarUbigeoDistrito = function () {
        maestroService.listarUbigeoDistrito().success(function (data) {
            $scope.ubigeosPeru = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.listarTiposPersona = function () {
        maestroService.listarTiposDeActor({}).success(function (data) {
            $scope.tiposDeActor = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.listarTiposDeClaseActor = function (IdTipoDeActor) {
        maestroService.listarTiposDeClaseActor({ IdTipoDeActor: IdTipoDeActor }).success(function (data) {
            $scope.tiposDeClaseActor = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.obtenerRolesDeCentrosDeAtencion = function () {
        centroDeAtencionService.obtenerRolesDeCentrosDeAtencion({}).success(function (data) {
            $scope.rolesCentroDeAtencion = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.cargarDatosComplementarios = function () {
        $scope.setDenominacionClaseActor($scope.sede.TipoPersona.Id);
        $scope.listarTiposDeClaseActor($scope.sede.TipoPersona.Id);
    }

    $scope.processFile = function (file) {
        var BASE64_MARKER = ';base64,';
        var fileReader = new FileReader();
        fileReader.readAsDataURL(file.file);
        fileReader.onload = function () {
            var dataURI = fileReader.result;
            var base64Index = dataURI.indexOf(BASE64_MARKER) + BASE64_MARKER.length;
            $scope.sede.Foto.Foto = dataURI.substring(base64Index);
            $scope.sede.Foto.HayFoto = true;
        };
    }

    $scope.setDenominacionClaseActor = function (IdTipoDeActor) {
        maestroService.obtenerDenominacionClaseActor({ IdTipoDeActor: IdTipoDeActor }).success(function (data) {
            $scope.denominacionClaseActor = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    //INICIALIZADORES
    $scope.inicializadorSede = function () {
        $scope.permitirRegistroCodigoDigemidEnEstableciemientoComercial = permitirRegistroCodigoDigemidEnEstableciemientoComercial;
        $scope.idRolAlmacen = idRolAlmacen;
        $scope.cargarSedeSync().then(function (resultado_) {
            $scope.cargarDatosComplementarios();
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });;
        $scope.listarTiposPersona();
        $scope.listarUbigeoDistrito();
    }

    $scope.nuevoRegistroSede = function () {
        $scope.sede = {
            Foto: {
                FotoSrc: "http://www.placehold.it/200x150/EFEFEF/AAAAAA&amp;text=no+image"
            }
        };
    }

    //CREAR SEDE
    $scope.crearSede = function () {
        centroDeAtencionService.crearSede({ sede: $scope.sede }).success(function (data) {
            $scope.cerrar('modal-registro-sede');
            SweetAlert.success("Correcto", data.result_description);
            $scope.dasactivarBotonRestablecer();
            $scope.obtenerSede();
        }).error(function (data, status) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }

    $scope.cerrarSede = function () {
        $scope.dasactivarBotonRestablecer();
        $scope.obtenerSede();
    }
    //OBTENER - SEDE
    $scope.obtenerSede = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        $scope.sedeInicial = { Foto: {} };
        centroDeAtencionService.obtenerSede().success(function (data) {
            $scope.sede = data;
            let idSede = $scope.sede.Id;
            if (idSede > 0) {
                $scope.obtenerCentrosDeAtencionBandeja(idSede);
                $scope.idCentroDeAtencionPadre = $scope.sede.Id;
                if ($scope.sede.Foto.HayFoto) {
                    $scope.sedeInicial.Foto = angular.copy($scope.sede.Foto);
                }
            } else {
                $scope.sede = {
                    Id: 0,
                    TipoPersona: {}
                };
            }
            defered.resolve();
        }).error(function (data) {
            SweetAlert.error("Ocurrio un Problema", data.error);
            defered.reject(data);
        });
        return promise;
    }


    $scope.cargarSedeSync = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        try {
            $scope.obtenerSede().then(function (resultado_) {
                defered.resolve();
            }, function (error) {
                $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
            });
        } catch (e) {
            defered.reject(e);
        }
        return promise;
    }

    /*----------------------------------------MANEJO CENTRO DE ATENCIÓN DE LA SEDE---------------------------------------*/

    //INICIALIZADORES
    $scope.inicializadorCentroDeAtencion = function () {
        $scope.obtenerRolesDeCentrosDeAtencion();
    }

    $scope.nuevoRegistroCentroDeAtencion = function () {
        $scope.centroDeAtencion = {};
        $timeout(function () {
            $('#rol-centro-de-atencion').trigger("change");
        }, 100);
    }
    //CREAR CENTRO DE ATENCION
    $scope.crearCentroDeAtencion = function () {
        centroDeAtencionService.crearCentroDeAtencion({ centroDeAtencion: $scope.centroDeAtencion, idEstablecimientoComercial: $scope.idCentroDeAtencionPadre }).success(function (data) {
            $scope.cerrar('modal-registro-centro-de-atencion');
            SweetAlert.success("Correcto", data.result_description);
            $scope.obtenerCentrosDeAtencionBandeja($scope.idCentroDeAtencionPadre);
        }).error(function (data, status) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }
    //OBTENER CENTRO DE ATENCION - BANDEJA 
    $scope.obtenerCentrosDeAtencionBandeja = function (idSede) {
        centroDeAtencionService.obtenerCentrosDeAtencionBandeja({ idEstablecimientoComercial: idSede }).success(function (data) {
            $scope.listaCentrosDeAtencion = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
    //OBTENER CENTRO DE ATENCION - EDITAR
    $scope.editarCentroDeAtencion = function (idCentroDeAtencion) {
        $scope.nuevoRegistroCentroDeAtencion();
        $scope.centroDeAtencionInicial = {};
        centroDeAtencionService.obtenerCentroDeAtencion({ idCentroDeAtencion: idCentroDeAtencion, idEstablecimientoComercial: $scope.idCentroDeAtencionPadre }).success(function (data) {
            $scope.centroDeAtencion = data;
            $timeout(function () {
                $('#rol-centro-de-atencion').trigger("change");
            }, 100);
            $scope.centroDeAtencionInicial = angular.copy($scope.centroDeAtencion);
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
    // DAR DE BAJA CENTRO DE ATENCION
    $scope.enviarDatosParaModalDarDeBajaCentroDeAtencion = function (idCentroDeAtencion, nombreCentroDeAtencion) {
        $scope.idCentroDeAtencion = idCentroDeAtencion;
        $scope.nombreCentroDeAtencion = nombreCentroDeAtencion;
    }

    $scope.eliminarCentroDeAtencion = function () {
        centroDeAtencionService.eliminarCentroDeAtencion({ idCentroDeAtencion: $scope.idCentroDeAtencion }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.obtenerCentrosDeAtencionBandeja($scope.idCentroDeAtencionPadre);
        }).error(function (data, status) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }
    //ELIMINAR FOTO
    $scope.eliminarFotoSede = function () {
        $scope.sede.Foto.Foto = null;
        $scope.sede.Foto.HayFoto = false;
        $scope.sede.Foto.FotoSrc = "http://www.placehold.it/200x150/EFEFEF/AAAAAA&amp;text=no+image";
        if ($scope.sedeInicial.Foto.HayFoto) {
            $scope.botonRestablecer = true;
        }
    }

    $scope.restablecerFotoSede = function () {
        $scope.sede.Foto = angular.copy($scope.sedeInicial.Foto);
        $scope.dasactivarBotonRestablecer();

    }

    $scope.dasactivarBotonRestablecer = function () {
        $scope.botonRestablecer = false;
    }

    $scope.verificarSalidaBienesSinStock = function () {
        $scope.mostrarSelectorSalidaBienesSinStock = $scope.centroDeAtencion.Roles.filter(c => c.Id == $scope.idRolAlmacen).length > 0 ? true : false;
    }

    //MANEJO DE LA TABLA
    $rootScope.dtOptions.withLightColumnFilter({
        '0': { type: 'text', className: 'form-control padding-left-right-3' },
        '1': { type: 'text', className: 'form-control padding-left-right-3' },
        '2': { type: 'text', className: 'form-control padding-left-right-3' },

    });
    //CERRAR MODAL
    $scope.cerrar = function (nombreDelModal) {
        $('#' + nombreDelModal).modal('hide');
    }

    //***************************ESTABLECER CENTRO DE ATENCION PRINCIPAL ******************//
    $scope.cargarCentrosDeAtencionParaObtencionPreciosYStock = function () {
        $scope.centroDeAtencionParaPrecios = {};
        $scope.centroDeAtencionParaStock = {};
        $scope.centrosDeAtencion = angular.copy($scope.listaCentrosDeAtencion);
        $scope.centroDeAtencionParaPrecios.Id = $scope.sede.IdCentroDeAtencionParaObtencionPrecios;
        $scope.centroDeAtencionParaStock.Id = $scope.sede.IdCentroDeAtencionParaObtencionStock;
        //if ($scope.hayCentroDeAtencionPrincipal) {
        //    $scope.centroDeAtencionPrincipal.Id = $scope.sede.IdCentroDeAtencionPrincipal;
        //}
        //if ($scope.hayCentroDeAtencionPrincipal) {
        //    $scope.centroDeAtencionPrincipal.Id = $scope.sede.IdCentroDeAtencionPrincipal;
        //}
    }

    $scope.establecerCentroDeAtencionParaPreciosYStock = function () {
        centroDeAtencionService.establecerCentrosDeAtencionParaPreciosYStockDeEstablecimientoComercial({
            idEstablecimientoComercial: $scope.idCentroDeAtencionPadre, idCentroDeAtencionPrecios:
                $scope.centroDeAtencionParaPrecios.Id == undefined ? 0 : $scope.centroDeAtencionParaPrecios.Id, idCentroDeAtencionStock: $scope.centroDeAtencionParaStock.Id == undefined ? 0 :
                    $scope.centroDeAtencionParaStock.Id
        }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.cerrar('modal-establecer-centro-de-atencion-para-obtencion-precios-stock');
            $scope.obtenerSede();
        }).error(function (data, status) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }

});

/*----------------------------------------MANEJO CENTRO DE ATENCIÓN DE SUCURSAL---------------------------------------*/
app.controller('sucursalCentroDeAtencionController', function ($scope, $rootScope, $timeout, DTOptionsBuilder, DTColumnDefBuilder, maestroService, centroDeAtencionService, SweetAlert) {

    $scope.idRolAlmacen = idRolAlmacen;

    $scope.obtenerRolesDeCentrosDeAtencion = function () {
        centroDeAtencionService.obtenerRolesDeCentrosDeAtencion({}).success(function (data) {
            $scope.rolesCentroDeAtencion = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.inicializadorCentroDeAtencion = function () {
        $scope.obtenerRolesDeCentrosDeAtencion();
    }

    $scope.nuevoRegistroCentroDeAtencion = function () {
        $scope.centroDeAtencion = {};
        $timeout(function () {
            $('#rol-centro-de-atencion').trigger("change");
        }, 100);
    }

    $scope.crearCentroDeAtencion = function () {
        centroDeAtencionService.crearCentroDeAtencion({ centroDeAtencion: $scope.centroDeAtencion, idEstablecimientoComercial: IdCentroDeAtencionPadre }).success(function (data) {
            $scope.cerrar('modal-registro-centro-de-atencion');
            SweetAlert.success("Correcto", data.result_description);
            $scope.obtenerCentrosDeAtencionBandeja();
        }).error(function (data, status) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }

    $scope.obtenerCentrosDeAtencionBandeja = function () {
        centroDeAtencionService.obtenerCentrosDeAtencionBandeja({ idEstablecimientoComercial: IdCentroDeAtencionPadre }).success(function (data) {
            $scope.listaCentrosDeAtencion = data;
            $scope.centroDeAtencionParaPrecios = {};
            $scope.centroDeAtencionParaStock = {};
            var centroAtencionParaObtencioDePrecios = Enumerable.from($scope.listaCentrosDeAtencion).where("$.EsCentroAtencionParaObtencioDePrecios == true ").toArray()[0];
            var centroAtencionParaObtencioDeStock = Enumerable.from($scope.listaCentrosDeAtencion).where("$.EsCentroAtencionParaObtencioDeStock == true ").toArray()[0];
            $scope.centroDeAtencionParaPrecios.Id = centroAtencionParaObtencioDePrecios != undefined ? centroAtencionParaObtencioDePrecios.Id : 0;
            $scope.centroDeAtencionParaStock.Id = centroAtencionParaObtencioDeStock != undefined ? centroAtencionParaObtencioDeStock.Id : 0;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
    //OBTENER CENTRO DE ATENCION - EDITAR
    $scope.editarCentroDeAtencion = function (idCentroDeAtencion) {
        $scope.nuevoRegistroCentroDeAtencion();
        $scope.centroDeAtencionInicial = {};
        centroDeAtencionService.obtenerCentroDeAtencion({ idCentroDeAtencion: idCentroDeAtencion, idEstablecimientoComercial: IdCentroDeAtencionPadre }).success(function (data) {
            $scope.centroDeAtencion = data;
            $timeout(function () {
                $('#rol-centro-de-atencion').trigger("change");
            }, 100);
            $scope.centroDeAtencionInicial = angular.copy($scope.centroDeAtencion);
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
    // DAR DE BAJA CENTRO DE ATENCION
    $scope.enviarDatosParaModalDarDeBajaCentroDeAtencion = function (idCentroDeAtencion, nombreCentroDeAtencion) {
        $scope.idCentroDeAtencion = idCentroDeAtencion;
        $scope.nombreCentroDeAtencion = nombreCentroDeAtencion;
    }

    $scope.eliminarCentroDeAtencion = function () {
        centroDeAtencionService.eliminarCentroDeAtencion({ idCentroDeAtencion: $scope.idCentroDeAtencion }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.obtenerCentrosDeAtencionBandeja();
        }).error(function (data, status) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }

    $rootScope.dtOptions.withLightColumnFilter({
        '0': { type: 'text', className: 'form-control padding-left-right-3' },
        '1': { type: 'text', className: 'form-control padding-left-right-3' },
        '2': { type: 'text', className: 'form-control padding-left-right-3' },

    });

    $scope.cerrar = function (nombreDelModal) {
        $('#' + nombreDelModal).modal('hide');
    }

    $scope.verificarSalidaBienesSinStock = function () {
        $scope.mostrarSelectorSalidaBienesSinStock = $scope.centroDeAtencion.Roles.filter(c => c.Id == $scope.idRolAlmacen).length > 0 ? true : false;
    }

    $scope.obtenerCentrosDeAtencionBandeja();

    //***************************ESTABLECER CENTRO DE ATENCION PRINCIPAL ******************//
    $scope.cargarCentrosDeAtencionParaObtencionPreciosYStock = function () {
        $scope.centrosDeAtencion = angular.copy($scope.listaCentrosDeAtencion);
    }

    $scope.establecerCentroDeAtencionParaPreciosYStock = function () {
        centroDeAtencionService.establecerCentrosDeAtencionParaPreciosYStockDeEstablecimientoComercial({
            idEstablecimientoComercial: IdCentroDeAtencionPadre, idCentroDeAtencionPrecios:
                $scope.centroDeAtencionParaPrecios.Id == undefined ? 0 : $scope.centroDeAtencionParaPrecios.Id, idCentroDeAtencionStock: $scope.centroDeAtencionParaStock.Id == undefined ? 0 :
                    $scope.centroDeAtencionParaStock.Id
        }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.cerrar('modal-establecer-centro-de-atencion-para-obtencion-precios-stock');
            $scope.obtenerCentrosDeAtencionBandeja();
        }).error(function (data, status) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }
});


/*----------------------------------------MANEJO SUCURSAL---------------------------------------*/
app.controller('sucursalController', function ($scope, $rootScope, $timeout, DTOptionsBuilder, DTColumnDefBuilder, maestroService, centroDeAtencionService, SweetAlert) {

    $scope.permitirRegistroCodigoDigemidEnEstableciemientoComercial = permitirRegistroCodigoDigemidEnEstableciemientoComercial;

    $scope.listarUbigeoDistrito = function () {
        maestroService.listarUbigeoDistrito().success(function (data) {
            $scope.ubigeosPeru = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
    $scope.processFile = function (file) {
        var BASE64_MARKER = ';base64,';
        var fileReader = new FileReader();
        fileReader.readAsDataURL(file.file);
        fileReader.onload = function () {
            var dataURI = fileReader.result;
            var base64Index = dataURI.indexOf(BASE64_MARKER) + BASE64_MARKER.length;
            $scope.sucursal.Foto.HayFoto = true;
            $scope.sucursal.Foto.Foto = dataURI.substring(base64Index);
        };
    }
    $scope.nuevoRegistroSucursal = function () {
        $scope.sucursal = {
            Foto: {
                FotoSrc: "http://www.placehold.it/200x150/EFEFEF/AAAAAA&amp;text=no+image"
            }
        };
        $scope.listarUbigeoDistrito();
    }
    //CREAR SUCURSAL
    $scope.crearSucursal = function () {
        centroDeAtencionService.crearSucursal({ sucursal: $scope.sucursal }).success(function (data) {
            $scope.cerrar('modal-registro-sucursal');
            SweetAlert.success("Correcto", data.result_description);
            $scope.obtenerSucursalesBandeja();
            $scope.dasactivarBotonRestablecer();
        }).error(function (data, status) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }
    //OBTENER SUCURSALES - BANDEJA
    $scope.obtenerSucursalesBandeja = function () {
        centroDeAtencionService.obtenerSucursalesBandeja().success(function (data) {
            $scope.listaSucursales = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
    //OBTENER SUCURSAL - EDITAR
    $scope.editarSucursal = function (idSucursal) {
        $scope.nuevoRegistroSucursal();
        $scope.sucursalInicial = { Foto: {} };
        centroDeAtencionService.obtenerSucursal({ idSucursal: idSucursal }).success(function (data) {
            $scope.sucursal = data;
            if ($scope.sucursal.Foto.HayFoto) {
                $scope.sucursalInicial.Foto = angular.copy($scope.sucursal.Foto);

            }
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }
    // DAR DE BAJA SUCURSALES
    $scope.enviarDatosParaModalDarDeBajaSucursal = function (idSucursal, nombreSucursal) {
        $scope.idSucursal = idSucursal;
        $scope.nombreSucursal = nombreSucursal;
    }

    $scope.eliminarSucursal = function () {
        centroDeAtencionService.eliminarSucursal({ idSucursal: $scope.idSucursal }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.obtenerSucursalesBandeja();
        }).error(function (data, status) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }
    //ELIMINAR FOTO
    $scope.eliminarFotoSucursal = function () {
        $scope.sucursal.Foto.Foto = null;
        $scope.sucursal.Foto.HayFoto = false;
        $scope.sucursal.Foto.FotoSrc = "http://www.placehold.it/200x150/EFEFEF/AAAAAA&amp;text=no+image";
        if ($scope.sucursalInicial.Foto.HayFoto) {
            $scope.botonRestablecer = true;
        }
    }

    $scope.restablecerFotoSucursal = function () {
        $scope.sucursal.Foto = angular.copy($scope.sucursalInicial.Foto);
        $scope.dasactivarBotonRestablecer();
    }

    $scope.dasactivarBotonRestablecer = function () {
        $scope.botonRestablecer = false;
    }

    //MANEJO DE LA TABLA
    $rootScope.dtOptions.withLightColumnFilter({
        '0': { type: 'text', className: 'form-control padding-left-right-3' },
        '1': { type: 'text', className: 'form-control padding-left-right-3' },
        '2': { type: 'text', className: 'form-control padding-left-right-3' },
        '3': { type: 'text', className: 'form-control padding-left-right-3' },
        '4': { type: 'text', className: 'form-control padding-left-right-3' },
        '5': { type: 'text', className: 'form-control padding-left-right-3' },

    });
    //CERRAR MODAL
    $scope.cerrar = function (nombreDelModal) {
        $('#' + nombreDelModal).modal('hide');
    }
    //INICIALIZAR
    $scope.obtenerSucursalesBandeja();


});
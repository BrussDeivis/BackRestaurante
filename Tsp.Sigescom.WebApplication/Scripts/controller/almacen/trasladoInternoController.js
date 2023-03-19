app.controller('trasladoInternoController', function ($scope, $q, SweetAlert, $rootScope, $timeout, DTOptionsBuilder, DTColumnDefBuilder, almacenService, actorComercialService, centroDeAtencionService, ventaService) {

    $scope.inicializar = function () {
        $scope.inicializacionRealizada = false;
        $scope.cargarParametros();
        $scope.cargarColeccionesAsync();
        $scope.inicializarComponentes();
        $scope.listarBandejaTraslados();
    }

    $scope.inicializarComponentes = function () {
        $scope.inicializacionRealizada = true;
    }

    $scope.cargarParametros = function () {
        $scope.fechaInicio = fechaInicio;
        $scope.fechaFin = fechaFin;
        $scope.nombreEmpleadoDeSesion = NombreEmpleadoDeSesion;
        $scope.nombreCentroDeAtencion = NombreCentroDeAtencion;
        $scope.idCentroDeAtencionSeleccionado = idCentroDeAtencionSeleccionado;
        $scope.modoSeleccionTipoFamilia = modoSeleccionTipoFamilia;
        $scope.mostrarBuscadorCodigoBarra = mostrarBuscadorCodigoBarra;
        $scope.modoSeleccionConcepto = modoSeleccionConcepto;
        $scope.numeroDecimalesEnCantidad = numeroDecimalesEnCantidad;
        $scope.minimoCaracteresBuscarConcepto = minimoCaracteresBuscarConcepto;
        $scope.tiempoEsperaBusquedaSelector = tiempoEsperaBusquedaSelector;
        $scope.salidaBienesSujetasADisponibilidadStock = salidaBienesSujetasADisponibilidadStock; 
        $scope.informacionSelectorConcepto = informacionSelectorConcepto;
        $scope.inicializacionRealizada = true;

    }

    $scope.cargarColeccionesAsync = function () {
        $scope.obtenerAlmacenesDestino();
        //$scope.obtenerEmpleadosPorAlmacen();
        $scope.obtenerComprobantesDeAlmacen();
    }

    $scope.obtenerAlmacenesDestino = function () {
        centroDeAtencionService.obtenerCentrosDeAtencionConRolAlmacen().success(function (data) {
            $scope.almacenesDestino = data;
            $scope.almacenesDestino.splice($scope.almacenesDestino.indexOf(Enumerable.from($scope.almacenesDestino).where("$.Id == '" + $scope.idCentroDeAtencionSeleccionado + "'").toArray()[0]), 1);
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.obtenerEmpleadosPorAlmacen = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        actorComercialService.obtenerActoresComercialesPorCentroDeAtencion({ idCentroDeAtencion: $scope.traslado.AlmacenDestino.Id }).success(function (data) {
            $scope.empleadosPorAlmacen = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.obtenerComprobantesDeAlmacen = function () {
        almacenService.obtenerTipoDeComprobanteNotaDeAlmacen().success(function (data) {
            $scope.comprobantesDeAlmacen = data;
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.cargarSeries = function (comprobante) {
        if (comprobante != null) {
            $scope.series = angular.copy(comprobante.Series);
        }
    }

    $rootScope.dtOptions.withLightColumnFilter({
        '0': { type: 'text', className: 'form-control padding-left-right-3' },
        '1': { type: 'text', className: 'form-control padding-left-right-3' },
        '2': { type: 'text', className: 'form-control padding-left-right-3' },
        '3': { type: 'text', className: 'form-control padding-left-right-3' },
        '4': { type: 'text', className: 'form-control padding-left-right-3' },
        '5': { type: 'text', className: 'form-control padding-left-right-3' }
    });

    $scope.listarBandejaTraslados = function () {
        almacenService.obtenerOrdenesMovimientoInternoMercaderia({ tipoEntradaSalida: false, desde: $scope.fechaInicio, hasta: $scope.fechaFin }).success(function (data) {
            $scope.trasladosInternos = data;
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    //#region VER TRASLADO INTERNO
    $scope.inicializarVerTraslado = function (item) {
        $scope.verTraslado = {};
        $scope.idMovimiento = item.Id;
        $scope.obtenerFormatosDeImpresion().then(function (resultado_) {
            $scope.formato = $scope.formatosDeImpresion[0];
            $scope.verTrasladoMercaderia();
            $scope.hayMovimiento = true;
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
    }

    $scope.verTrasladoMercaderia = function () {
        almacenService.obtenerMovimientoDeAlmacen({ idMovimiento: $scope.idMovimiento, formato: $scope.formato.Id }).success(function (data) {
            $scope.verTraslado = data;
            $scope.tamanioComprobante = $scope.formato.Id == 1 ? '80mm' : "210mm";
            document.getElementById("pdfMovimiento").innerHTML = $scope.verTraslado.CadenaHtmlDeMovimientoDeAlmacen;
            $timeout(function () { $('#pdfMovimiento').trigger("change"); }, 100);
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema", data.error);
        });
    }

    $scope.obtenerFormatosDeImpresion = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        ventaService.obtenerFormatosDeImpresion({}).success(function (data) {
            $scope.formatosDeImpresion = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.cerrarVerMovimiento = function () {
        $scope.verTraslado = {};
        $scope.formato = {};
        document.getElementById("pdfMovimiento").innerHTML = '';
        $timeout(function () { $('#pdfMovimiento').trigger("change"); }, 100);
    }
    //#endregion

    //#region IMPRESION DEL DOCUMENTO DE MOVIMIENTO  
    $scope.imprimirTrasladoMercaderia = function () {
        $scope.tamanioComprobante = $scope.formato.Id == 1 ? '80mm' : "210mm";
        var ventanaImpresion = window.open(' ', 'popimpr');
        ventanaImpresion.document.write($scope.verTraslado.CadenaHtmlDeMovimientoDeAlmacen);
        ventanaImpresion.document.close();
        ventanaImpresion.print();
        ventanaImpresion.close();
    }
    //#endregion


    //#region REGISTRO DE TRASLADO INTERNO DE MERCADERIA

    $scope.nuevoRegistro = function () {
        $scope.limpiarRegistro();
        $scope.establecerDatosPorDefecto();
    }

    $scope.limpiarRegistro = function () {
        $scope.detalle = {};
        $scope.conceptoComercial = {};
        $scope.traslado = { Detalles: [] };
    }

    $scope.establecerDatosPorDefecto = function () {
        $scope.traslado.AlmacenDestino = $scope.almacenesDestino[0];//Por defecto insertar el primer almacen destino
        //$scope.traslado.ResponsableDestino = $scope.empleadosPorAlmacen[0];//Por defecto insertar el primer almacen destino
        $scope.traslado.TipoDeComprobante = $scope.comprobantesDeAlmacen[0];//Por defecto el primer comprobante de almacen
        $scope.cargarSeries($scope.traslado.TipoDeComprobante);
        $scope.traslado.Observacion = 'NINGUNO';
        $scope.obtenerEmpleadosPorAlmacen().then(function (resultado_) {
            $scope.traslado.ResponsableDestino = $scope.empleadosPorAlmacen[0];
            $scope.verificarInconsistencias();
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
        
    }

    $scope.seleccionConcepto = function (conceptoComercial) {
        $scope.detalle.Producto = angular.copy(conceptoComercial);
        $scope.agregarDetalle();
    }

    $scope.agregarDetalle = function () {
        var validar = false;
        var index = 0;
        if ($scope.salidaBienesSujetasADisponibilidadStock && (!$scope.detalle.Producto.Stock || $scope.detalle.Producto.Stock <= 0)) {
            SweetAlert.warning("Advertencia", "El Stock de " + $scope.detalle.Producto.Nombre + " es " + "0");
        }
        else {
            if ($scope.traslado.Detalles.length > 0) {
                for (var i = 0; i < $scope.traslado.Detalles.length; i++) {
                    if ($scope.detalle.Producto.Id == $scope.traslado.Detalles[i].Producto.Id) {
                        validar = true;
                        index = i;
                        break;
                    }
                }
            }
            if (validar) {
                $scope.agregarCantidad($scope.traslado.Detalles, index);
                $scope.detalle = {};
            }
            else {
                $scope.detalle.Cantidad = 1;
                $scope.detalle.Observacion = 'NINGUNO';
                $scope.traslado.Detalles.unshift($scope.detalle);
                $scope.detalle = {};
            }
        }
        $scope.verificarInconsistencias();
    }

    $scope.formatoDecimalCantidad = function (event, $filter) {
        let valor = event.target.value;
        event.target.value = parseFloat(valor).toFixed($scope.numeroDecimalesEnCantidad);
    }

    $scope.agregarCantidad = function (detalles, index) {
        detalles[index].Cantidad++;
    }

    $scope.quitarDetalle = function (index) {
        $scope.traslado.Detalles.splice(index, 1);
    }

    $scope.guardarTraslado = function () {
        almacenService.guardarMovimientoInternoMercaderia({ movimientoInternoMercaderia: $scope.traslado }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            jsWebClientPrint.print('idMovimiento=' + data.data);
            $scope.listarBandejaTraslados();
            $scope.cerrar();
        }).error(function (data) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }

    $scope.cerrar = function () {
        $scope.limpiarRegistro();
        $('#modal-registro-traslado-interno-mercaderia').modal('hide');
    }

    $scope.verificarInconsistencias = function () {
        $scope.inconsistencias = [];
        var banderaCantidad0 = false;
        var esCantidadMayorAStock = false;
        if ($scope.traslado.AlmacenDestino == undefined) {
            $scope.inconsistencias.push('Es necesario seleccionar un almacen destino.');
        }
        if ($scope.traslado.TipoDeComprobante == undefined) {
            $scope.inconsistencias.push('Es necesario seleccionar un comprobante.');
        }
        if ($scope.traslado.Detalles.length <= 0) {
            $scope.inconsistencias.push('Es necesario seleccionar al menos un producto.');
        } else {
            for (var i = 0; i < $scope.traslado.Detalles.length; i++) {
                banderaCantidad0 = banderaCantidad0 || ($scope.traslado.Detalles[i].Cantidad == 0 || $scope.traslado.Detalles[i].Cantidad == undefined) ? true : false;
                esCantidadMayorAStock = esCantidadMayorAStock || $scope.traslado.Detalles[i].Cantidad > $scope.traslado.Detalles[i].Producto.Stock ? true : false;
            }
        } if (banderaCantidad0) {
            $scope.inconsistencias.push('Es necesario la cantidad sea mayor a 0.');
        } 
        if ($scope.salidaBienesSujetasADisponibilidadStock && esCantidadMayorAStock) {
            $scope.inconsistencias.push("Es necesario que la cantidad no sea mayor al stock");
        }
    }
 
});

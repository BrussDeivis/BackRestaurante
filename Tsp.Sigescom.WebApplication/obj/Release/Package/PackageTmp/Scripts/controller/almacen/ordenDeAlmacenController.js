app.controller('ordenDeAlmacenController', function ($scope, $q, $compile, SweetAlert, $rootScope, $timeout, DTOptionsBuilder, DTColumnDefBuilder, almacenService, maestroService, productoService, compraService, ventaService, centroDeAtencionService) {

    //GETS
    $scope.obtenerEstablecimientosComerciales = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        centroDeAtencionService.obtenerEstablecimientosComerciales().success(function (data) {
            $scope.listaEstablecimientosComerciales = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.obtenerCentrosDeAtencionConRolAlmacen = function () {
        if ($scope.establecimientosComerciales == undefined) {
            $scope.centrosDeAtencion = [];
            $timeout(function () { $('#centroDeAtencion').trigger("change"); }, 100);
        } else {
            var defered = $q.defer();
            var promise = defered.promise;
            $scope.idsEstablecimientosComerciales = [];
            for (var i = 0; i < $scope.establecimientosComerciales.length; i++) {
                $scope.idsEstablecimientosComerciales.push($scope.establecimientosComerciales[i].Id)
            }
            centroDeAtencionService.obtenerCentrosDeAtencionConRolAlmacenVigentesPorEstablecimientosComerciales({ idsEstablecimientosComerciales: $scope.idsEstablecimientosComerciales }).success(function (data) {
                $scope.listaCentrosDeAtencionConRolAlmacen = data;
                defered.resolve();
            }).error(function (data) {
                $scope.messageError(data.error);
                defered.reject(data);
            });
            return promise;
        }
    }

    $scope.obtenerCentrosAtencionDeAlmacenero = function (id) {
        var defered = $q.defer();
        var promise = defered.promise;
        centroDeAtencionService.obtenerCentrosDeAtencionConRolAlmacenVigentesPorEstablecimientoComercial({ idEstablecimientoComercial: id }).success(function (data) {
            $scope.listaCentrosDeAtencionDeAlmacenero = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.obtenerTiposDeComprobanteMovimientoDeAlmacen = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        almacenService.obtenerTiposDeComprobanteMovimientoDeAlmacen({}).success(function (data) {
            $scope.tiposDeComprobantesMasSeriesMovimientoDeAlmacen = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.obtenerTiposDeComprobanteOrdenDeAlmacen = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        almacenService.obtenerTiposDeComprobanteOrdenDeAlmacen({}).success(function (data) {
            $scope.tiposDeComprobantesMasSeriesOrdenDeAlmacen = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.obtenerModalidadesTraslado = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        maestroService.obtenerModalidadesTraslado().success(function (data) {
            $scope.modalidadesTraslado = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.obtenerMotivosTraslado = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        maestroService.obtenerMotivosTraslado().success(function (data) {
            $scope.motivosTraslado = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.obtenerTransportistas = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        compraService.obtenerProveedores().success(function (data) {
            $scope.transportistas = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
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

    //BANDEJA DE ORDENES DE ALMACEN
    $scope.inicializar = function () {
        //OBTENCION DE PARAMETROS
        $scope.idEstablecimientoPorDefecto = idEstablecimientoPorDefecto;
        $scope.idCentroDeAtencionPorDefecto = idCentroDeAtencionPorDefecto;
        $scope.fechaInicio = fechaInicio;
        $scope.fechaFin = fechaFin;
        //INICIO DE VARIABLES
        $scope.porRecibir = true;
        $scope.establecimientosComerciales = [];
        $scope.centrosDeAtencion = [];
        $scope.ordenDeAlmacen = {};

        //CARGA DE DATOS
        $scope.obtenerEstablecimientosComerciales().then(function (resultado_) {
            $scope.establecerEstablecimientoComercialPorDefecto();
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
    }

    $scope.establecerEstablecimientoComercialPorDefecto = function () {
        var establecimientoComercial = Enumerable.from($scope.listaEstablecimientosComerciales)
            .where("$.Id == '" + $scope.idEstablecimientoComercialPorDefecto + "'").toArray()[0];
        $scope.establecimientosComerciales.push(establecimientoComercial != null ? establecimientoComercial : $scope.listaEstablecimientosComerciales[0]);
        $timeout(function () { $('#establecimientoComercial').trigger("change"); }, 100);
        $scope.obtenerCentrosDeAtencionConRolAlmacen().then(function (resultado_) {
            $scope.establecerCentroAtencionConRolAlmacenPorDefecto();
            $scope.listarBandeja();
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
    }

    $scope.establecerCentroAtencionConRolAlmacenPorDefecto = function () {
        var centroAtencion = Enumerable.from($scope.listaCentrosDeAtencionConRolAlmacen)
            .where("$.Id == '" + $scope.idCentroAtencionPorDefecto + "'").toArray()[0];
        $scope.centrosDeAtencion.push(centroAtencion != null ? centroAtencion : $scope.listaCentrosDeAtencionConRolAlmacen[0]);
        $timeout(function () { $('#centroDeAtencion').trigger("change"); }, 100);
    }

    $scope.listarBandeja = function () {
        $scope.idsCentrosDeAtencion = [];
        for (var i = 0; i < $scope.centrosDeAtencion.length; i++) {
            $scope.idsCentrosDeAtencion.push($scope.centrosDeAtencion[i].Id)
        }
        almacenService.obtenerOrdenesDeAlmacen({ porRecibir: $scope.porRecibir, idsCentrosDeAtencion: $scope.idsCentrosDeAtencion, desde: fechaInicio, hasta: fechaFin }).success(function (data) {
            $scope.ordenesDeAlmacen = data;
            $scope.labelRemitenteDestinatario = $scope.porRecibir ? 'REMITENTE' : 'DESTINATARIO';
            $scope.labelEntradaSalida = $scope.porRecibir ? 'ENTRADA' : 'SALIDA';
            $timeout(function () { $('#tabla-orden-de-almacen').trigger("change"); }, 100);
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    //VER ORDEN DE ALMACEN
    $scope.inicializarVerOrdenDeAlmacen = function (item) {
        $scope.verOrden = {};
        $scope.idOrdenDeAlmacen = item.Id;
        $scope.obtenerFormatosDeImpresion().then(function (resultado_) {
            $scope.formato = $scope.formatosDeImpresion[0];
            $scope.verOrdenDeAlmacen();
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
    }

    $scope.verOrdenDeAlmacen = function () {
        almacenService.obtenerOrdenDeAlmacen({ idOrdenDeAlmacen: $scope.idOrdenDeAlmacen, formato: $scope.formato.Id }).success(function (data) {
            $scope.verOrden = data;
            $scope.tamanioComprobante = $scope.formato.Id == 1 ? '80mm' : "210mm";
            document.getElementById("pdfOrdenDeAlmacen").innerHTML = $scope.verOrden.CadenaHtmlDeOrdenDeAlmacen;
            $timeout(function () { $('#pdfOrdenDeAlmacen').trigger("change"); }, 100);
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema", data.error);
        });
    }

    $scope.cerrarVerMovimiento = function () {
        $scope.verOrden = {};
        $scope.formato = {};
        document.getElementById("pdfOrdenDeAlmacen").innerHTML = '';
        $timeout(function () { $('#pdfOrdenDeAlmacen').trigger("change"); }, 100);
    }

    // ENVIO DE DOCUMENTO POR CORREO
    $scope.inicializarEnvioCorreoElectronico = function () {
        $scope.correosElectronicos = [];
    }

    $scope.agregarCorreoElectronico = function () {
        $scope.correosElectronicos.push($scope.correoElectronico);
        $scope.correoElectronico = '';
        $timeout(function () { $('#correoImput').trigger("change"); }, 100);
    }

    $scope.eliminarCorreoElectronico = function (index) {
        $scope.correosElectronicos.splice(index);
    }

    $scope.enviarCorreoElectronico = function () {
        almacenService.enviarCorreoElectronicoDeOrdenDeAlmacen({ idOrden: $scope.idOrdenDeAlmacen, formato: $scope.formato.Id, correosElectronicos: $scope.correosElectronicos }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema", data.error);
        });
    }

    $scope.limpiarEnvioDeCorrero = function () {
        $scope.correosElectronicos = {};
    }

    $scope.imprimirOrdenDeAlmacen = function () {
        $scope.tamanioComprobante = $scope.formato.Id == 1 ? '80mm' : "210mm";
        var ventanaImpresion = window.open(' ', 'popimpr');
        ventanaImpresion.document.write($scope.verOrden.CadenaHtmlDeOrdenDeAlmacen);
        ventanaImpresion.document.close();
        ventanaImpresion.print();
        ventanaImpresion.close();
    }

    //REGISTRO DE MOVIMIENTO DE ALMACEN
    $scope.inicializarMovimientoDeAlmacen = function (item) {
        $scope.cargarParametrosMovimientoDeAlmacen();
        $scope.limpiarMovimientoDeAlmacen();
        $scope.cargarColeccionesMovimientoDeAlmacenAsync();
        $scope.cargarOrdenDeAlmacen(item).then(function (result_) {
            $scope.cargarColeccionesMovimientoDeAlmacenSync().then(function (resultado_) {
                $scope.establecerDatosPorDefectoMovimientoDeAlmacen();
            }, function (error) {
                $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
            });
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
    }

    $scope.cargarParametrosMovimientoDeAlmacen = function () {
        $scope.idUbigeoSede = idUbigeoSede;
        $scope.direccionSede = direccionSede;
        $scope.idModalidadTrasladoPorDefecto = idModalidadTrasladoPorDefecto;
        $scope.idMotivoTrasladoPorDefecto = idMotivoTrasladoPorDefecto;
        $scope.idTransportistaPorDefecto = idTransportistaPorDefecto;
        $scope.idTipoDeComprobantePorDefecto = idTipoDeComprobantePorDefecto;
        $scope.idProveedorGenerico = idProveedorGenerico;
    }

    $scope.cargarOrdenDeAlmacen = function (item) {
        var defered = $q.defer();
        var promise = defered.promise;
        almacenService.obtenerOrdenDeAlmacenParaMovimientoDeAlmacen({ idOrdenDeAlmacen: item.Id }).success(function (data) {
            $scope.movimiento = data;
            $scope.movimiento.EsTrasladoTotal = true;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.cargarColeccionesMovimientoDeAlmacenAsync = function () {
        $scope.detallesDeMovimientoDeAlmacen = [];
        //$scope.venta.HaySalidaDeMercaderia = true;
    }

    $scope.cargarColeccionesMovimientoDeAlmacenSync = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        var promiseList = [];
        promiseList.push($scope.obtenerModalidadesTraslado());
        promiseList.push($scope.obtenerMotivosTraslado());
        promiseList.push($scope.obtenerUbigeoDistrito());
        promiseList.push($scope.obtenerTransportistas());
        promiseList.push($scope.obtenerTiposDeComprobanteMovimientoDeAlmacen());
        //promiseList.push($scope.obtenerUbigeoDireccionTercero());
        //promiseList.push($scope.obtenerDetalleDireccionTercero());
        return $q.all(promiseList).then(function (response) {
            defered.resolve();
        }).catch(function (error) {
            defered.reject(e);
        });
        return promise;
    }

    $scope.establecerDatosPorDefectoMovimientoDeAlmacen = function () {
        $scope.movimiento.Observacion = 'NINGUNO';
        $scope.movimiento.EsTrasladoTotal = true;
        $scope.movimiento.DireccionOrigen = $scope.direccionSede;
        $scope.movimiento.DireccionDestino = $scope.movimiento.DireccionTercero;
        $scope.establecerModalidadTrasladoPorDefecto();
        $scope.establecerMotivoTrasladoPorDefecto();
        $scope.establecerTransportistaPorDefecto();
        $scope.establecerUbigeoOrigenPorDefecto();
        $scope.establecerUbigeoDestinoPorDefecto();
        $scope.establecerTipoDeComprobantePorDefecto();
        $scope.hayInconsistenciasMovimientoDeAlmacen = true;
        //Iniciar la tabla de salida de mercaderia
        //if ($scope.venta.HaySalidaDeMercaderia == true) {
        //    $scope.movimiento.Detalles = angular.copy($scope.detallesDeGuiaDeRemision);
        //} else {
        //    for (var i = 0; i < $scope.venta.Detalles.length; i++) {
        //        $scope.detallesDeGuiaDeRemision.push({ IdProducto: $scope.venta.Detalles[i].Producto.Id, Descripcion: $scope.venta.Detalles[i].Producto.Nombre, Ordenado: parseFloat($scope.venta.Detalles[i].Cantidad), RecibidoEntregado: 0.00, IngresoSalidaActual: parseFloat($scope.venta.Detalles[i].Cantidad) });
        //        $scope.movimiento.Detalles = angular.copy($scope.detallesDeGuiaDeRemision);
        //    }
        //}
    }

    $scope.establecerModalidadTrasladoPorDefecto = function () {
        var modalidadTrasladoPorDefecto = Enumerable.from($scope.modalidadesTraslado)
            .where("$.Id == '" + $scope.idModalidadTrasladoPorDefecto + "'").toArray()[0];
        $scope.movimiento.ModalidadTransporte = modalidadTrasladoPorDefecto != null ? modalidadTrasladoPorDefecto : $scope.modalidadesTraslado[0];
        $timeout(function () { $('#modalidad').trigger("change"); }, 100);
    }

    $scope.establecerMotivoTrasladoPorDefecto = function () {
        var motivoTrasladoPorDefecto = Enumerable.from($scope.motivosTraslado)
            .where("$.Id == '" + $scope.idMotivoTrasladoPorDefecto + "'").toArray()[0];
        $scope.movimiento.MotivoTraslado = motivoTrasladoPorDefecto != null ? motivoTrasladoPorDefecto : $scope.motivosTraslado[0];
        $timeout(function () { $('#motivo').trigger("change"); }, 100);
    }

    $scope.establecerTransportistaPorDefecto = function () {
        var transportistaPorDefecto = Enumerable.from($scope.transportistas)
            .where("$.Id == '" + $scope.idTransportistaPorDefecto + "'").toArray()[0];
        $scope.movimiento.Transportista = transportistaPorDefecto != null ? transportistaPorDefecto : $scope.transportistas[0];
        $timeout(function () { $('#transportista').trigger("change"); }, 100);
    }

    $scope.establecerTipoDeComprobantePorDefecto = function () {
        var tipoDeComprobantePorDefecto = Enumerable.from($scope.tiposDeComprobantesMasSeriesMovimientoDeAlmacen)
            .where("$.Id == '" + $scope.idTipoDeComprobantePorDefecto + "'").toArray()[0];
        $scope.movimiento.TipoDeComprobante = tipoDeComprobantePorDefecto != null ? tipoDeComprobantePorDefecto : $scope.tiposDeComprobantesMasSeriesMovimientoDeAlmacen[0];
        $timeout(function () { $('#documento').trigger("change"); }, 100);
        $scope.cargarSeriesMovimientoDeAlmacen(tipoDeComprobantePorDefecto);
    }

    $scope.establecerUbigeoOrigenPorDefecto = function () {
        var ubigeo = Enumerable.from($scope.ubigeosPeru)
            .where("$.Id == '" + $scope.idUbigeoSede + "'").toArray()[0];
        $scope.movimiento.UbigeoOrigen = ubigeo != null ? ubigeo : $scope.ubigeosPeru[0];
        $timeout(function () { $('#ubigeoOrigen').trigger("change"); }, 100);
    }

    $scope.establecerUbigeoDestinoPorDefecto = function () {
        var ubigeo = Enumerable.from($scope.ubigeosPeru)
            .where("$.Id == '" + $scope.movimiento.IdUbigeoTercero + "'").toArray()[0];
        $scope.movimiento.UbigeoDestino = ubigeo != null ? ubigeo : $scope.ubigeosPeru[0];
        $timeout(function () { $('#ubigeoDestino').trigger("change"); }, 100);
    }

    $scope.limpiarMovimientoDeAlmacen = function () {
        $scope.movimiento = { Detalles: [] };
        $scope.movimiento.EsTrasladoTotal = {};
        $scope.hayInconsistenciasMovimientoDeAlmacen = {};
        $scope.inconsistenciasMovimientoDeAlmacen = [];
    }

    $scope.cargarSeriesMovimientoDeAlmacen = function (tipoComprobante) {
        if (tipoComprobante != null) {
            $scope.seriesMovimientoDeAlmacen = angular.copy(tipoComprobante.Series);
            //Establece por defecto la primera serie que tenga, en el caso de que tengan mas de 2 series.
            if ($scope.seriesMovimientoDeAlmacen.length > 1) {
                $scope.movimiento.SerieSeleccionada = $scope.seriesMovimientoDeAlmacen[0];
            }
            if (tipoComprobante.TipoComprobante.Id == $scope.idDocumentoNotaAlamacenInterna) {
                $scope.accionDeshabilitar(false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);
            } else {
                $scope.accionDeshabilitar(false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);
            }
        }
    }

    $scope.accionDeshabilitar = function (documento, serieIngresada, numeroIngresado, fechaRegistro, editarTransportista, nuevoTransportista, transportista, marcaPlaca, nLicencia, observacion, modalidad, motivo, ubigeoOrigen, direccionOrigen, ubigeoDestino, direccionDestino) {
        document.getElementById("documento").disabled = documento;
        document.getElementById("serieIngresada").disabled = serieIngresada;
        document.getElementById("numeroIngresado").disabled = numeroIngresado;
        document.getElementById("fechaInicioTraslado").disabled = fechaRegistro;
        document.getElementById("editarTransportista").disabled = editarTransportista;
        document.getElementById("nuevoTransportista").disabled = nuevoTransportista;
        document.getElementById("transportista").disabled = transportista;
        document.getElementById("marcaPlaca").disabled = marcaPlaca;
        document.getElementById("nLicencia").disabled = nLicencia;
        document.getElementById("observacionMovimientoDeAlmacen").disabled = observacion;
        document.getElementById("modalidad").disabled = modalidad;
        document.getElementById("motivo").disabled = motivo;
        document.getElementById("ubigeoOrigen").disabled = ubigeoOrigen;
        document.getElementById("direccionOrigen").disabled = direccionOrigen;
        document.getElementById("ubigeoDestino").disabled = ubigeoDestino;
        document.getElementById("direccionDestino").disabled = direccionDestino;
    }

    $scope.guardarMovimientoDeAlmacen = function () {
        $scope.verificarTrasladoTotal();
        $scope.movimiento.idOrdenDeAlmacen = 
        almacenService.guardarMovimientoDeAlmacen({ movimientoDeAlmacen: $scope.movimiento, esIngresoMercaderia: $scope.porRecibir }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $('#modal-registro-movimiento-de-almacen').modal('hide');
            jsWebClientPrint.print('idMovimiento=' + data.data);
            $scope.limpiarMovimientoDeAlmacen();
            $scope.listarBandeja();
        }).error(function (data) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }

    $scope.verificarTrasladoTotal = function () {
        var temp = true;
        for (var i = 0; i < $scope.movimiento.Detalles.length; i++) {
            if ($scope.movimiento.Detalles[i].Ordenado - $scope.movimiento.Detalles[i].RecibidoEntregado != parseFloat($scope.movimiento.Detalles[i].IngresoSalidaActual)) {
                temp = false;
                break;
            }
        }
        $scope.movimiento.EsTrasladoTotal = temp;
    }

    $scope.verificarInconsistenciasMovimientoDeAlmacen = function () {
        var cantidadDiferenteDeCero = false;
        $scope.inconsistenciasMovimientoDeAlmacen = [];
        if ($scope.movimiento.TipoDeComprobante == undefined) {
            $scope.inconsistenciasMovimientoDeAlmacen.push("Es necesario seleccionar un documento.");
        } else {
            if ($scope.movimiento.TipoDeComprobante.EsPropio == false) {
                if ($scope.movimiento.TipoDeComprobante.SerieIngresada == "" || $scope.movimiento.TipoDeComprobante.SerieIngresada == null) {
                    $scope.inconsistenciasMovimientoDeAlmacen.push("Es necesario ingresar la serie de comprobante.");
                } if ($scope.movimiento.TipoDeComprobante.NumeroIngresado == "" || $scope.movimiento.TipoDeComprobante.NumeroIngresado == null) {
                    $scope.inconsistenciasMovimientoDeAlmacen.push("Es necesario ingresar el numero de comprobante.");
                }
            }
            if ($scope.movimiento.TipoDeComprobante.TipoComprobante.Id != $scope.idDocumentoNotaAlamacenInterna) {
                if ($scope.movimiento.FechaInicioTraslado == "" || $scope.movimiento.FechaInicioTraslado == undefined) {
                    $scope.inconsistenciasMovimientoDeAlmacen.push("Es necesario ingresar la fecha de inicio de traslado.");
                } if ($scope.movimiento.Transporte != undefined) {
                    if ($scope.movimiento.Transporte.Transportista == undefined) {
                        $scope.inconsistenciasMovimientoDeAlmacen.push("Es necesario ingresar el transportista.");
                    } if ($scope.movimiento.Transporte.MarcaYPlaca == "" || $scope.movimiento.Transporte.MarcaYPlaca == undefined) {
                        $scope.inconsistenciasMovimientoDeAlmacen.push("Es necesario ingresar la marca y placa del vehiculo.");
                    } if ($scope.movimiento.Transporte.NumeroLicencia == "" || $scope.movimiento.Transporte.NumeroLicencia == undefined) {
                        $scope.inconsistenciasMovimientoDeAlmacen.push("Es necesario ingresar el numero de licencia del conductor.");
                    }
                } if ($scope.movimiento.ModalidadTransporte == undefined) {
                    $scope.inconsistenciasMovimientoDeAlmacen.push("Es necesario seleccionar la modalidad de transporte.");
                } if ($scope.movimiento.MotivoTraslado == undefined) {
                    $scope.inconsistenciasMovimientoDeAlmacen.push("Es necesario seleccionar el motivo de transporte.");
                } if ($scope.movimiento.UbigeoOrigen == undefined) {
                    $scope.inconsistenciasMovimientoDeAlmacen.push("Es necesario seleccionar el ubigeo origen del traslado.");
                } if ($scope.movimiento.DireccionOrigen == "" || $scope.movimiento.DireccionOrigen == undefined) {
                    $scope.inconsistenciasMovimientoDeAlmacen.push("Es necesario ingresar la direccion origen del traslado.");
                } if ($scope.movimiento.UbigeoDestino == undefined) {
                    $scope.inconsistenciasMovimientoDeAlmacen.push("Es necesario seleccionar el ubigeo destino del traslado.");
                } if ($scope.movimiento.DireccionDestino == "" || $scope.movimiento.DireccionDestino == undefined) {
                    $scope.inconsistenciasMovimientoDeAlmacen.push("Es necesario ingresarla direccion destino del traslado.");
                }
            }
        }
        for (var i = 0; i < $scope.movimiento.Detalles.length; i++) {
            if ($scope.movimiento.Detalles[i].IngresoSalidaActual < 0 || $scope.movimiento.Detalles[i].IngresoSalidaActual > ($scope.movimiento.Detalles[i].Ordenado - $scope.movimiento.Detalles[i].RecibidoEntregado)) {
                $scope.inconsistenciasMovimientoDeAlmacen.push("Es necesario la cantidad de los detalles sea mayor o igual a 0 y menor a la diferencia de ordenado y recibido o entregado.");
                break;
            }
        }
        for (var i = 0; i < $scope.movimiento.Detalles.length; i++) {
            if ($scope.movimiento.Detalles[i].IngresoSalidaActual != 0 && $scope.movimiento.Detalles[i].IngresoSalidaActual > 0) {
                cantidadDiferenteDeCero = true;
                break;
            }
        }
        if (cantidadDiferenteDeCero == false) {
            $scope.inconsistenciasMovimientoDeAlmacen.push("Es necesario la cantidad de alguno de los detalles sea mayor a 0.");
        }
        $scope.hayInconsistenciasMovimientoDeAlmacen = ($scope.inconsistenciasMovimientoDeAlmacen.length == 0) ? false : true;
    }

    $scope.actualizarTransportistaIngresadosYEditados = function (actualizacionTransportista, id, numeroDocumento, razonSocial) {
        $scope.nuevoTransportista = { Id: id, RazonSocial: razonSocial, NumeroDocumentoIdentidad: numeroDocumento };
        if (actualizacionTransportista == 0) {
            $scope.transportistas.push($scope.nuevoTransportista);
            $scope.movimiento.Transportista = angular.copy($scope.nuevoTransportista);
        } else {
            $scope.transportistas[document.getElementById("transportista").selectedIndex] = $scope.nuevoTransportista;
        }
    }


    //REGISTRO DE ORDEN DE ALMACEN
    $scope.inicializarRegistroOrdenDeAlmacen = function (idOrden) {
        $scope.limpiarOrdenDeAlmacen();
        $scope.obtenerDetallesDeOrdenDeAlmacen(idOrden);
        $scope.establecerDatosPorDefectoOrdenDeAlmacenSync();

    }

    $scope.establecerDatosPorDefectoOrdenDeAlmacenSync = function () {
        $scope.cargarColeccionesOrdenDeAlmacenSync().then(function (resultado_) {
            $scope.ordenDeAlmacen.Observacion = "NINGUNO";
            $scope.establecerEstablecimientoComercialPorDefecto();
            $scope.establecerTipoDeComprobantePorDefecto();
            $scope.cargarCentroDeAtencionPorDefectoSync().then(function (resultado_) {
                $scope.establecerCentroDeAtencionPorDefecto();
                $scope.verificarInconsistenciasOrdenDeAlmacen();

            }, function (error) {
                $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
            });
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });

    }


    $scope.cargarColeccionesOrdenDeAlmacenSync = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        var promiseList = [];
        promiseList.push($scope.obtenerEstablecimientosComerciales());
        promiseList.push($scope.obtenerTiposDeComprobanteOrdenDeAlmacen());
        return $q.all(promiseList).then(function (response) {
            defered.resolve();
        }).catch(function (error) {
            defered.reject(e);
        });
        return promise;
    }

    $scope.cargarSeriesOrdenDeAlmacen = function (tipoComprobante) {
        if ($scope.ordenDeAlmacen.TipoDeComprobante != null) {
            $scope.seriesOrdenDeAlmacen = angular.copy($scope.ordenDeAlmacen.TipoDeComprobante.Series);
        }
    }

    $scope.cargarCentroDeAtencionPorDefectoSync = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        $scope.obtenerCentrosAtencionDeAlmacenero($scope.ordenDeAlmacen.EstablecimientoComercial.Id).then(function (resultado_) {
            defered.resolve();
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
            defered.reject(error);
        });
        return promise;
    }


    $scope.establecerEstablecimientoComercialPorDefecto = function () {
        var establecimiento = Enumerable.from($scope.listaEstablecimientosComerciales).toArray()[0];
        $scope.ordenDeAlmacen.EstablecimientoComercial = establecimiento;
    }

    $scope.establecerCentroDeAtencionPorDefecto = function () {
        var centroDeAtencion = Enumerable.from($scope.listaCentrosDeAtencionDeAlmacenero).toArray()[0];
        $scope.ordenDeAlmacen.CentroDeAtencion = centroDeAtencion;
    }

    $scope.establecerTipoDeComprobantePorDefecto = function () {
        var comprobante = Enumerable.from($scope.tiposDeComprobantesMasSeriesOrdenDeAlmacen).toArray()[0];
        $scope.ordenDeAlmacen.TipoDeComprobante = comprobante;
    }

    $scope.verificarInconsistenciasOrdenDeAlmacen = function () {
        //var cantidadDiferenteDeCero = false;
        $scope.inconsistenciasOrdenDeAlmacen = [];

        //Validar establecimiento
        if (!($scope.ordenDeAlmacen.EstablecimientoComercial)) {
            $scope.inconsistenciasOrdenDeAlmacen.push("Es necesario ingresar el establecimiento comercial.");
        }

        if (!($scope.ordenDeAlmacen.CentroDeAtencion)) {
            $scope.inconsistenciasOrdenDeAlmacen.push("Es necesario ingresar el centro de atención.");
        }

        //Validar tipo comprobante
        if (!($scope.ordenDeAlmacen.TipoDeComprobante)) {
            $scope.inconsistenciasOrdenDeAlmacen.push("Es necesario seleccionar un documento.");
        } else {
            if ($scope.ordenDeAlmacen.TipoDeComprobante.EsPropio == false) {
                if (!($scope.ordenDeAlmacen.TipoDeComprobante.SerieIngresada)) {
                    $scope.inconsistenciasOrdenDeAlmacen.push("Es necesario ingresar la serie de comprobante.");
                } if (!($scope.ordenDeAlmacen.TipoDeComprobante.NumeroIngresado)) {
                    $scope.inconsistenciasOrdenDeAlmacen.push("Es necesario ingresar el número de comprobante.");
                }
            }
        }
        if (!($scope.ordenDeAlmacen.FechaTraslado)) {
            $scope.inconsistenciasOrdenDeAlmacen.push("Es necesario ingresar la fecha de traslado.");
        }


        //for (var i = 0; i < $scope.movimiento.Detalles.length; i++) {
        //    if ($scope.movimiento.Detalles[i].IngresoSalidaActual < 0 || $scope.movimiento.Detalles[i].IngresoSalidaActual > ($scope.movimiento.Detalles[i].Ordenado - $scope.movimiento.Detalles[i].RecibidoEntregado)) {
        //        $scope.inconsistenciasMovimientoDeAlmacen.push("Es necesario la cantidad de los detalles sea mayor o igual a 0 y menor a la diferencia de ordenado y recibido o entregado.");
        //        break;
        //    }
        //}
        //for (var i = 0; i < $scope.movimiento.Detalles.length; i++) {
        //    if ($scope.movimiento.Detalles[i].IngresoSalidaActual != 0 && $scope.movimiento.Detalles[i].IngresoSalidaActual > 0) {
        //        cantidadDiferenteDeCero = true;
        //        break;
        //    }
        //}
        //if (cantidadDiferenteDeCero == false) {
        //    $scope.inconsistenciasMovimientoDeAlmacen.push("Es necesario la cantidad de alguno de los detalles sea mayor a 0.");
        //}
        $scope.hayInconsistenciasOrdenDeAlmacen = ($scope.inconsistenciasOrdenDeAlmacen.length == 0) ? false : true;
    }

    $scope.guardarOrdenDeAlmacen = function () {
        almacenService.guardarOrdenDeAlmacen({ ordenDeAlmacen: $scope.ordenDeAlmacen }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            jsWebClientPrint.print('idOrdenDeAlmacen=' + data.data);
            $scope.limpiarOrdenDeAlmacen();
            $('#modal-registo-orden-de-almacen').modal('hide');
        }).error(function (data) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }

    $scope.limpiarOrdenDeAlmacen = function () {
        $scope.ordenDeAlmacen = { Observacion: "", Detalles: [] };
    }

    $scope.cerrarRegistroOrdenDeAlmacen = function () {
        $('#modal-registo-orden-de-almacen').modal('hide');
    }



    //RECURSOS
    $rootScope.dtOptions.withLightColumnFilter({
        '1': { type: 'text', className: 'form-control padding-left-right-3' },
        '2': { type: 'text', className: 'form-control padding-left-right-3' },
        '3': { type: 'text', className: 'form-control padding-left-right-3' },
        '4': { type: 'text', className: 'form-control padding-left-right-3' },
        '5': { type: 'text', className: 'form-control padding-left-right-3' },
        '6': { type: 'text', className: 'form-control padding-left-right-3' },
        '7': { type: 'text', className: 'form-control padding-left-right-3' },
        '8': { type: 'text', className: 'form-control padding-left-right-3' }
    });

    $scope.cerrar = function () {
        $scope.verMovimientoDeAlmacen = {};
        if (angular.equals($scope.actor, $scope.actorInicial)) {
            $('#modal-registro-movimiento-de-almacen').modal('hide');
        }
        else {
            $('#modal-pregunta').click();
        }
    }

    $scope.cerrarModal = function () {
        $('#modal-registro-movimiento-de-almacen').modal('hide');
    }

    $scope.cerrarModal = function () {
        $('#modal-registro-movimiento-de-almacen').modal('hide');
    }

    $scope.obtenerDetallesDeOrdenDeAlmacen = function (idOrden) {
        var defered = $q.defer();
        var promise = defered.promise;
        almacenService.obtenerDetalleDeCompraParaOrdenDeAlmacen({ idOrdenDeCompra: idOrden }).success(function (data) {
            $scope.ordenDeAlmacen.IdOrdenDeOperacion = idOrden;
            $scope.ordenDeAlmacen.Detalles = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }
    
    //$scope.obtenerUbigeoDireccionTercero = function () {
    //    var defered = $q.defer();
    //    var promise = defered.promise;
    //    if ($scope.movimiento.Tercero != undefined) {
    //        clienteService.obtenerUbigeoDireccionTercero({ idTercero: $scope.movimiento.IdTercero }).success(function (data) {
    //            $scope.idUbigeoDeTercero = data;
    //            defered.resolve();
    //        }).error(function (data) {
    //            $scope.messageError(data.error);
    //            defered.reject(data);
    //        });
    //    } else {
    //        defered.resolve();
    //    }
    //    return promise;
    //}

    //$scope.obtenerDetalleDireccionTercero = function () {
    //    var defered = $q.defer();
    //    var promise = defered.promise;
    //    if ($scope.movimiento.Tercero != undefined) {
    //        clienteService.obtenerDetalleDireccionTercero({ idTercero: $scope.movimiento.IdTercero }).success(function (data) {
    //            $scope.direccionDeTercero = data;
    //            defered.resolve();
    //        }).error(function (data) {
    //            $scope.messageError(data.error);
    //            defered.reject(data);
    //        });
    //    } else {
    //        defered.resolve();
    //    }
    //    return promise;
    //}
});




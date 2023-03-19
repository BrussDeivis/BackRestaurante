app.controller('ventaMasivaController', function ($scope, $q, $timeout, $rootScope, SweetAlert, $filter, DTOptionsBuilder, DTColumnDefBuilder, ventaService, centroDeAtencionService, empleadoService, conceptoService) {

    //#region INICIALIZACION
    $scope.inicializar = function () {
        $scope.limpiarRegistro();
        $scope.cargarParametros();
        $scope.cargarColeccionesAsync();
        $scope.cargarColeccionesSync().then(function (resultado_) {
            $scope.establecerDatosPorDefecto();
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
    };

    $scope.limpiarRegistro = function () {
        $scope.ventaMasiva = { Detalles: [] };
        $scope.importeTotal = {};
        $scope.itemVenta = {};
    };

    $scope.cargarParametros = function () {
        $scope.idComprobantePorDefecto = idComprobantePorDefecto;
        $scope.fechaActual = fechaActual;
        $scope.permitirIngresarCantidad = permitirIngresarCantidad;
        $scope.permitirIngresarPrecioUnitario = permitirIngresarPrecioUnitario;
        $scope.permitirIngresarImporte = permitirIngresarImporte;
        $scope.ingresarCantidadCalcularPrecioUnitario = ingresarCantidadCalcularPrecioUnitario;
        $scope.ingresarPrecioUnitarioCalcularImporte = ingresarPrecioUnitarioCalcularImporte;
        $scope.ingresarImporteCalcularCantidad = ingresarImporteCalcularCantidad;
        $scope.mascaraDeCalculoPorDefecto = mascaraDeCalculoPorDefecto;
        $scope.numeroDecimalesEnCantidad = numeroDecimalesEnCantidad;
        $scope.numeroDecimalesEnPrecio = numeroDecimalesEnPrecio;
    };

    $scope.cargarColeccionesAsync = function () {

    };

    $scope.cargarColeccionesSync = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        var promiseList = [];
        promiseList.push($scope.obtenerTiposDeComprobante());
        promiseList.push($scope.obtenerPuntosDeVenta());
        promiseList.push($scope.obtenerCajas());
        promiseList.push($scope.obtenerAlmacenes());
        promiseList.push($scope.obtenerVendedores());
        promiseList.push($scope.obtenerCajeros());
        promiseList.push($scope.obtenerAlmaceneros());
        return $q.all(promiseList).then(function (response) {
            defered.resolve();
        }).catch(function (error) {
            defered.reject(e);
        });
    };

    $scope.establecerDatosPorDefecto = function () {
        $scope.establecerComprobantePorDefecto();
        $scope.establecerPuntoDeVentaPorDefecto();
        $scope.establecerAlmacenPorDefecto();
        $scope.establecerCajaPorDefecto();
        $scope.establecerVendedorPorDefecto();
        $scope.establecerAlmaceneroPorDefecto();
        $scope.establecerCajeroPorDefecto();
        $scope.importeTotal = 0;
        var soloFechaActual = $scope.fechaActual.split(" ")[0];
        $scope.ventaMasiva.FechaDeEmision = soloFechaActual;
    };

    $scope.establecerComprobantePorDefecto = function () {
        for (var i = 0; i < $scope.tiposDeComprobantesMasSeries.length; i++) {
            if ($scope.tiposDeComprobantesMasSeries[i].TipoComprobante.Id === $scope.idComprobantePorDefecto) {
                $scope.ventaMasiva.TipoDeComprobante = $scope.tiposDeComprobantesMasSeries[i];
                $scope.ventaMasiva.TipoDeComprobante.SerieSeleccionada = $scope.ventaMasiva.TipoDeComprobante.Series[0];
                break;
            }
        }
        $scope.cargarSeries($scope.ventaMasiva.TipoDeComprobante);
        $timeout(function () { $('#comboDocumento').trigger("change"); }, 100);
    };

    $scope.cargarSeries = function (tipoComprobante) {
        if (tipoComprobante !== null) {
            $scope.series = angular.copy(tipoComprobante.Series);
        }
    };

    $scope.establecerPuntoDeVentaPorDefecto = function () {
        //Punto de venta: el primer item
        $scope.ventaMasiva.PuntoDeVenta = $scope.puntosDeVenta[0];
    };

    $scope.establecerVendedorPorDefecto = function () {
        //Vendedor: el primer item
        $scope.ventaMasiva.Vendedor = $scope.listaVendedoresGenerico[0];
    };

    $scope.establecerAlmacenPorDefecto = function () {
        //Almacen: el mismo que ya es punto de venta o el primer 
        var almacenIgualAPuntoDeVenta = Enumerable.from($scope.listaAlmacenesGenerico)
            .where("$.Id === '" + $scope.ventaMasiva.PuntoDeVenta.Id + "'").toArray()[0];
        $scope.ventaMasiva.Almacen = almacenIgualAPuntoDeVenta !== null ? almacenIgualAPuntoDeVenta : $scope.listaAlmacenesGenerico[0];
        $timeout(function () { $('#comboAlmacen').trigger("change"); }, 100);
    };

    $scope.establecerCajaPorDefecto = function () {
        //Caja: el mismo que ya es punto de venta o el primer item
        var cajaIgualAPuntoDeVenta = Enumerable.from($scope.listaCajasGenerico)
            .where("$.Id === '" + $scope.ventaMasiva.PuntoDeVenta.Id + "'").toArray()[0];
        $scope.ventaMasiva.Caja = cajaIgualAPuntoDeVenta !== null ? cajaIgualAPuntoDeVenta : $scope.listaCajasGenerico[0];
        $timeout(function () { $('#comboCaja').trigger("change"); }, 100);
    };

    $scope.establecerCajeroPorDefecto = function () {
        //Cajero: el primer item
        $scope.ventaMasiva.Cajero = $scope.listaCajerosGenerico[0];
    };

    $scope.establecerAlmaceneroPorDefecto = function () {
        //Almacenero: el primer item
        $scope.ventaMasiva.Almacenero = $scope.listaAlmacenerosGenerico[0];
    };
    //#endregion

    //#region OBTENCION DE DATOS
    $scope.obtenerTiposDeComprobante = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        ventaService.obtenerTiposDeComprobanteConSeriesAutonumericasParaVentaExcluidoFactura({}).success(function (data) {
            $scope.tiposDeComprobantesMasSeries = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    };

    $scope.obtenerPuntosDeVenta = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        centroDeAtencionService.obtenerCentrosDeAtencionConRolPuntoDeVenta().success(function (data) {
            $scope.puntosDeVenta = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    };

    $scope.obtenerVendedores = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        empleadoService.obtenerEmpleadosConRolVendedor().success(function (data) {
            $scope.listaVendedoresGenerico = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    };

    $scope.obtenerAlmacenes = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        centroDeAtencionService.obtenerCentrosDeAtencionConRolAlmacen().success(function (data) {
            $scope.listaAlmacenesGenerico = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    };

    $scope.obtenerAlmaceneros = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        empleadoService.obtenerEmpleadosConRolAlmacenero().success(function (data) {
            $scope.listaAlmacenerosGenerico = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    };

    $scope.obtenerCajas = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        centroDeAtencionService.obtenerCentrosDeAtencionConRolCaja().success(function (data) {
            $scope.listaCajasGenerico = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    };

    $scope.obtenerCajeros = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        empleadoService.obtenerEmpleadosConRolCajero().success(function (data) {
            $scope.listaCajerosGenerico = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    };
    //#endregion

    //#region METODOS DE CALCULOS
    $scope.cambiarPuntoDeVenta = function () {
        $scope.establecerAlmacenPorDefecto();
        $scope.establecerCajaPorDefecto();
    };

    $scope.cargarConceptoPorCodigoBarra = function (item) {
        if (item.Codigo) {
            ventaService.obtenerMercaderiaPorCodigoBarra({ codigoBarra: item.Codigo }).success(function (data) {
                if (data.data === 0) {//si data es igual a 0 no existe el producto o no tiene precio
                    SweetAlert.warning("Advertencia", data.data_description);
                }
                else {
                    item.Concepto = data.Nombre;
                    item.IdConcepto = data.Id;
                    item.EsBien = data.EsBien;
                }
            }).error(function (data) {
                SweetAlert.error("Ocurrio un Problema", data.error);
            });
        }
        if (item.MascaraDeCalculo === undefined || item.MascaraDeCalculo === null || item.MascaraDeCalculo === '') {
            item.MascaraDeCalculo = $scope.mascaraDeCalculoPorDefecto;
        }
    };

    $scope.agregarItemVenta = function () {
        if (parseFloat($scope.itemVenta.Cantidad) > 0 && parseFloat($scope.itemVenta.PrecioUnitario) > 0 && parseFloat($scope.itemVenta.Importe) > 0 && $scope.itemVenta.Concepto != undefined) {
            $scope.itemVenta.Cantidad = parseFloat($scope.itemVenta.Cantidad).toFixed($scope.numeroDecimalesEnCantidad);
            $scope.itemVenta.PrecioUnitario = parseFloat($scope.itemVenta.PrecioUnitario).toFixed($scope.numeroDecimalesEnPrecio);
            $scope.itemVenta.Importe = parseFloat($scope.itemVenta.Importe).toFixed(2);

            $scope.ventaMasiva.Detalles.push($scope.itemVenta);
            $scope.calcularTotal();
            $scope.itemVenta = {};
        }
    };

    $scope.quitarItemVenta = function (index) {
        $scope.ventaMasiva.Detalles.splice(index, 1);
        $scope.calcularTotal();
    };

    $scope.calcularTotal = function () {
        $scope.importeTotal = 0;
        if ($scope.ventaMasiva.Detalles.length > 0) {
            for (var i = 0; i < $scope.ventaMasiva.Detalles.length; i++) {
                $scope.importeTotal += parseFloat($scope.ventaMasiva.Detalles[i].PrecioUnitario) * parseFloat($scope.ventaMasiva.Detalles[i].Cantidad);
            }
        }
        $scope.importeTotal = $scope.importeTotal.toFixed(2);
    };

    $scope.datosRequeridosParaRealizarVentaMasiva = function () {
        return $scope.ventaMasiva.TipoDeComprobante === undefined || $scope.ventaMasiva.PuntoDeVenta === undefined || $scope.ventaMasiva.Vendedor === undefined || $scope.ventaMasiva.Detalles === null ||
            $scope.ventaMasiva.FechaDeEmision === undefined || $scope.ventaMasiva.FechaDeEmision === "" || $scope.ventaMasiva.Detalles.length === 0 || $scope.ventaMasiva.Caja === undefined ||
            $scope.ventaMasiva.Cajero === undefined;
    };

    $scope.guardarVentaMasiva = function () {
        ventaService.guardarVentaMasiva({ ventaMasiva: $scope.ventaMasiva }).success(function (data) {
            $scope.limpiarRegistro();
            $scope.establecerDatosPorDefecto();
            SweetAlert.success("Correcto", data.result_description);
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema", data.error);
        });
    };

    $scope.focusSelectNext = function (id) {
        $timeout(function () {
            $('#' + id).trigger("focus");
        }, 100);
        $timeout(function () {
            $('#' + id).trigger("select");
        }, 100);
    };

    $scope.focusDespuesCodigo = function () {
        if ($scope.permitirIngresarCantidad) {
            $scope.focusSelectNext('cantidad');
        } else if ($scope.permitirIngresarPrecioUnitario) {
            $scope.focusSelectNext('precioUnitario');
        } else if ($scope.permitirIngresarImporte) {
            $scope.focusSelectNext('importe');
        } else {
            $scope.agregarItemVenta();
            $scope.focusSelectNext('codigo');
        }
    };

    $scope.focusDespuesCantidad = function () {
        if ($scope.permitirIngresarPrecioUnitario) {
            $scope.focusSelectNext('precioUnitario');
        } else if ($scope.permitirIngresarImporte) {
            $scope.focusSelectNext('importe');
        } else {
            $scope.agregarItemVenta();
            $scope.focusSelectNext('codigo');
        }
    };

    $scope.focusDespuesPrecioUnitario = function () {
        if ($scope.permitirIngresarImporte) {
            $scope.focusSelectNext('importe');
        } else {
            $scope.agregarItemVenta();
            $scope.focusSelectNext('codigo');
        }
    };

    $scope.focusDespuesImporte = function () {
        $scope.agregarItemVenta();
        $scope.focusSelectNext('codigo');
    };

    $scope.calcularValoresDetalle = function (identificador, detalle) {
        if (identificador === 1)//Cantidad
        {
            $scope.cambiarMascaraDeCalculo(detalle, 0, '1');
            if ($scope.ingresarCantidadCalcularPrecioUnitario) {
                detalle.PrecioUnitario = parseFloat(detalle.Importe / detalle.Cantidad).toFixed($scope.numeroDecimalesEnPrecio);
                $scope.cambiarMascaraDeCalculo(detalle, 1, '0');
            }
            else {
                detalle.Importe = parseFloat(detalle.PrecioUnitario * detalle.Cantidad).toFixed(2);
                $scope.cambiarMascaraDeCalculo(detalle, 2, '0');
            }
        }
        if (identificador === 2)//Precio Unitario
        {
            $scope.cambiarMascaraDeCalculo(detalle, 1, '1');
            if ($scope.ingresarPrecioUnitarioCalcularImporte) {
                detalle.Importe = parseFloat(detalle.PrecioUnitario * detalle.Cantidad).toFixed(2);
                $scope.cambiarMascaraDeCalculo(detalle, 2, '0');
            }
            else {
                detalle.Cantidad = parseFloat(detalle.Importe / detalle.PrecioUnitario).toFixed($scope.numeroDecimalesEnCantidad);
                $scope.cambiarMascaraDeCalculo(detalle, 0, '0');
            }
        }
        if (identificador === 3)//Importe
        {
            $scope.cambiarMascaraDeCalculo(detalle, 2, '1');
            if ($scope.ingresarImporteCalcularCantidad) {
                detalle.Cantidad = parseFloat(detalle.Importe / detalle.PrecioUnitario).toFixed($scope.numeroDecimalesEnCantidad);
                $scope.cambiarMascaraDeCalculo(detalle, 0, '0');
            }
            else {
                detalle.PrecioUnitario = parseFloat(detalle.Importe / detalle.Cantidad).toFixed($scope.numeroDecimalesEnPrecio);
                $scope.cambiarMascaraDeCalculo(detalle, 1, '0');
            }
        }
    };

    $scope.cambiarMascaraDeCalculo = function (detalle, campo, valor) {
        var mascaraDeCalculoArray = detalle.MascaraDeCalculo.split('');
        mascaraDeCalculoArray[campo] = valor;
        detalle.MascaraDeCalculo = mascaraDeCalculoArray.join('');
    };

    $scope.verificarInconsistenciasYGuardar = function () {
        var temp = 0;
        var bandera = false;
        var banderaMayor700 = false;
        $scope.hayinconsistencias = false;
        $scope.inconsistencias = [];
        if ($scope.itemVenta.Codigo !== undefined && $scope.itemVenta.Cantidad !== undefined && $scope.itemVenta.PrecioUnitario !== undefined && $scope.itemVenta.Importe !== undefined) {
            $scope.inconsistencias[temp] = "Agrega el ultimo registro, no esta en la venta masiva.";
            temp++;
        } if ($scope.ventaMasiva.Detalles.length < 1) {
            $scope.inconsistencias[temp] = "Es necesario que tenga al menos un registro la venta masiva.";
            temp++;
        } else {
            for (var i = 0; i < $scope.ventaMasiva.Detalles.length; i++) {
                if (parseFloat($scope.ventaMasiva.Detalles[i].Importe) === 0) {
                    bandera = true;
                    break;
                }
            }
            if (bandera) {
                $scope.inconsistencias[temp] = "Es necesario que ningun importe total sea 0.";
                temp++;
            }
            for (var j = 0; j < $scope.ventaMasiva.Detalles.length; j++) {
                if (parseFloat($scope.ventaMasiva.Detalles[j].Importe) >= 700) {
                    banderaMayor700 = true;
                    break;
                }
            }
            if (banderaMayor700) {
                $scope.inconsistencias[temp] = "Es necesario el importe de cada venta sea menor a 700 soles.";
                temp++;
            }
        }
        if (temp === 0) {
            $scope.guardarVentaMasiva();
        } else {
            $scope.hayinconsistencias = true;
            $timeout(function () {
                $scope.hayinconsistencias = false;
            }, 5000);
        }
    };
    //#endregion
});
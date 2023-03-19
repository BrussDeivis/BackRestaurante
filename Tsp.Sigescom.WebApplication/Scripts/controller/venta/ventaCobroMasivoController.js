app.controller('ventaCobroMasivoController', function ($scope, $q, $timeout, $rootScope, SweetAlert, $filter, DTOptionsBuilder, DTColumnDefBuilder, ventaService, centroDeAtencionService, empleadoService, conceptoService) {

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
        $scope.ventasYCobrosPorVendedor = { Clientes: [] };
        $scope.item = [];
    };

    $scope.cargarParametros = function () {
        $scope.fechaActual = fechaActual;
        $scope.permitirIngresarCantidad = permitirIngresarCantidad;
        $scope.permitirIngresarPrecioUnitario = permitirIngresarPrecioUnitario;
        $scope.permitirIngresarImporte = permitirIngresarImporte;
        $scope.ingresarCantidadCalcularPrecioUnitario = ingresarCantidadCalcularPrecioUnitario;
        $scope.ingresarPrecioUnitarioCalcularImporte = ingresarPrecioUnitarioCalcularImporte;
        $scope.ingresarImporteCalcularCantidad = ingresarImporteCalcularCantidad;
        $scope.tieneRolCajero = tieneRolCajero;
        $scope.idEmpleado = tieneRolCajero;
        $scope.idCentroAtencion = idCentroAtencion;
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
        $scope.establecerPuntoDeVentaPorDefecto();
        $scope.establecerAlmacenPorDefecto();
        $scope.establecerCajaPorDefecto();
        $scope.establecerVendedorPorDefecto();
        $scope.establecerAlmaceneroPorDefecto();
        $scope.establecerCajeroPorDefecto();
        var soloFechaActual = $scope.fechaActual.split(" ")[0];
        $scope.ventasYCobrosPorVendedor.Fecha = soloFechaActual;
        $scope.obtenerCarteraDeClientes();
        $scope.importeTotal = 0;
        $scope.cobroTotal = 0;
        $scope.saldoActualTotal = 0;
    };

    $scope.establecerPuntoDeVentaPorDefecto = function () {
        //Punto de venta: el primer item
        $scope.ventasYCobrosPorVendedor.PuntoDeVenta = $scope.puntosDeVenta[0];
    };

    $scope.establecerAlmacenPorDefecto = function () {
        //Almacen: el mismo que ya es punto de venta o el primer 
        var almacenIgualAPuntoDeVenta = Enumerable.from($scope.listaAlmacenesGenerico)
            .where("$.Id == '" + $scope.ventasYCobrosPorVendedor.PuntoDeVenta.Id + "'").toArray()[0];
        $scope.ventasYCobrosPorVendedor.Almacen = almacenIgualAPuntoDeVenta !== null ? almacenIgualAPuntoDeVenta : $scope.listaAlmacenesGenerico[0];
        $timeout(function () { $('#comboAlmacen').trigger("change"); }, 100);
    };

    $scope.establecerCajaPorDefecto = function () {
        //Caja: verificar rol cajero, si lo tiene poner el mismo centro de atencion de la seccion, sino el mismo que el punto de venta o el primer item
        var cajaIgualAPuntoDeVenta = $scope.tieneRolCajero ? Enumerable.from($scope.listaCajasGenerico).where("$.Id == '" + $scope.idCentroAtencion + "'").toArray()[0]
            : Enumerable.from($scope.listaCajasGenerico).where("$.Id == '" + $scope.ventasYCobrosPorVendedor.PuntoDeVenta.Id + "'").toArray()[0];
        $scope.ventasYCobrosPorVendedor.Caja = cajaIgualAPuntoDeVenta !== null ? cajaIgualAPuntoDeVenta : $scope.listaCajasGenerico[0];
        $timeout(function () { $('#comboCaja').trigger("change"); }, 100);
    };

    $scope.establecerVendedorPorDefecto = function () {
        //Vendedor: el primer item
        $scope.ventasYCobrosPorVendedor.Vendedor = $scope.listaVendedoresGenerico[0];
    };

    $scope.establecerAlmaceneroPorDefecto = function () {
        //Almacenero: el primer item
        $scope.ventasYCobrosPorVendedor.Almacenero = $scope.listaAlmacenerosGenerico[0];
    };

    $scope.establecerCajeroPorDefecto = function () {
        let cajeroSeleccionado = $scope.tieneRolCajero ? Enumerable.from($scope.listaCajasGenerico).where("$.Id == '" + $scope.idEmpleado + "'").toArray()[0] : $scope.listaCajerosGenerico[0];
        $scope.ventasYCobrosPorVendedor.Cajero = cajeroSeleccionado;
        $timeout(function () { $('#comboCajero').trigger("change"); }, 100);
    };
    //#endregion

    //#region OBTENCION DE DATOS
    $scope.obtenerEmpleados = function () {
        empleadoService.obtenerEmpleadosGenerico().success(function (data) {
            $scope.listaEmpleadosGenerico = data;
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    };

    $scope.obtenerCajeros = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        empleadoService.obtenerEmpleadosConRolCajero().success(function (data) {
            $scope.listaCajerosGenerico = data;
            defered.resolve();
        }).error(function (data) {
            SweetAlert.error2(data);
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
            SweetAlert.error2(data);
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
            SweetAlert.error2(data);
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
            SweetAlert.error2(data);
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
            SweetAlert.error2(data);
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
            SweetAlert.error2(data);
            defered.reject(data);
        });
        return promise;
    };
    //#endregion

    //#region METODOS DE CALCULOS
    $scope.cambiarPuntoDeVenta = function () {
        $scope.establecerAlmacenPorDefecto();
        $scope.establecerCajaPorDefecto();
        $scope.obtenerCarteraDeClientes();
    };

    $scope.obtenerPrecioConceptoSegunPuntoDeVenta = function () {
        conceptoService.obtenerPrecioVentaNormalDelConceptoParaVentasMasivasSegunElPuntoDeventa({ idPuntoDeVenta: $scope.ventasYCobrosPorVendedor.PuntoDeVenta.Id }).success(function (data) {
            $scope.PrecioConcepto = parseFloat(data.PrecioConcepto);
            $scope.NombreConcepto = data.Nombre;
            $scope.PrecioConceptoInicial = parseFloat(data.PrecioConcepto);
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.obtenerCarteraDeClientes = function () {
        if ($scope.ventasYCobrosPorVendedor.PuntoDeVenta !== undefined && $scope.ventasYCobrosPorVendedor.Fecha !== undefined && $scope.ventasYCobrosPorVendedor.Fecha !== "") {
            ventaService.obtenerDeudasMasivas({ idPuntoDeVenta: $scope.ventasYCobrosPorVendedor.PuntoDeVenta.Id, fecha: $scope.ventasYCobrosPorVendedor.Fecha }).success(function (data) {
                $scope.ventasYCobrosPorVendedor.Clientes = data;
            }).error(function (data) {
                $scope.ventasYCobrosPorVendedor = { Clientes: [] };
                SweetAlert.error2(data);
            });
        }
    };

    $scope.cargarConceptoPorCodigoBarra = function (item) {
        if (item) {
            if (item.Codigo !== "" && item.Codigo !== null && item.Codigo !== undefined) {
                ventaService.obtenerMercaderiaPorCodigoBarra({ codigoBarra: item.Codigo }).success(function (data) {
                    if (data.data === 0) {
                        SweetAlert.error("Ocurrio un Problema", data.data_description);
                    }
                    else {
                        item.Concepto = data.Nombre;
                        item.IdConcepto = data.Id;
                        item.EsBien = data.EsBien;
                    }
                }).error(function (data) {
                    SweetAlert.error2(data);
                });
            }
            if (item.MascaraDeCalculo === undefined || item.MascaraDeCalculo === null || item.MascaraDeCalculo === '') {
                item.MascaraDeCalculo = $scope.mascaraDeCalculoPorDefecto;
            }
        }
    };

    $scope.calcularValoresDetallesRegistrados = function (identificador, detalle, parentIndex) {
        $scope.calcularValoresDetalle(identificador, detalle);
        $scope.calcularImporteTotalDetalle(parentIndex, $scope.ventasYCobrosPorVendedor.Clientes[parentIndex].Detalles);
        $scope.calcularTotales();
    }

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

    $scope.calcularTotales = function () {
        $scope.importeTotal = 0;
        $scope.cobroTotal = 0;
        var saldoAnterior = 0;
        $scope.saldoActualTotal = 0;
        for (var i = 0; i < $scope.ventasYCobrosPorVendedor.Clientes.length; i++) {
            var cliente = $scope.ventasYCobrosPorVendedor.Clientes[i];
            for (var j = 0; j < cliente.Detalles.length; j++) {
                $scope.importeTotal += parseFloat(cliente.Detalles[j].Importe);
            }
            saldoAnterior += parseFloat(cliente.SaldoAnterior);
            $scope.cobroTotal += parseFloat(cliente.Cobro);
        }
        $scope.saldoActualTotal = parseFloat(saldoAnterior) - parseFloat($scope.cobroTotal) + parseFloat($scope.importeTotal);
    };

    $scope.accionCampoCodigo = function (parentIndex) {
        if ($scope.item[parentIndex]) {
            if ($scope.item[parentIndex].Codigo) {
                $scope.focusSelectNext('cantidad-' + parentIndex);
            } else {
                $scope.focusSelectNext('codigo-' + (parentIndex + 1));
            }
        }
    };

    $scope.focusDespuesCantidad = function (parentIndex) {
        if ($scope.permitirIngresarPrecioUnitario) {
            $scope.focusSelectNext('precioUnitario-' + parentIndex);
        } else if ($scope.permitirIngresarImporte) {
            $scope.focusSelectNext('importe-' + parentIndex);
        } else {
            $scope.agregarItemVenta(parentIndex, item);
            $scope.focusSelectNext('codigo-' + parentIndex);
        }
    };

    $scope.focusDespuesPrecioUnitario = function (parentIndex) {
        if ($scope.permitirIngresarImporte) {
            $scope.focusSelectNext('importe-' + parentIndex);
        } else {
            $scope.agregarItemVenta(parentIndex, item);
            $scope.focusSelectNext('codigo-' + parentIndex);
        }
    };

    $scope.focusDespuesImporte = function (parentIndex, item) {
        $scope.agregarItemVenta(parentIndex, item);
        $scope.focusSelectNext('codigo-' + parentIndex);
    };

    $scope.focusSelectNext = function (id) {
        $timeout(function () {
            $('#' + id).trigger("focus");
        }, 100);
        $timeout(function () {
            $('#' + id).trigger("select");
        }, 100);
    };

    $scope.agregarItemVenta = function (parentIndex, item) {
        if (parseFloat(item.Cantidad) > 0 && parseFloat(item.PrecioUnitario) > 0 && parseFloat(item.Importe) > 0 && item.Concepto != undefined) {
            item.Cantidad = parseFloat(item.Cantidad).toFixed($scope.numeroDecimalesEnCantidad);
            item.PrecioUnitario = parseFloat(item.PrecioUnitario).toFixed($scope.numeroDecimalesEnPrecio);
            item.Importe = parseFloat(item.Importe).toFixed(2);

            $scope.ventasYCobrosPorVendedor.Clientes[parentIndex].Detalles.push(item);
            $scope.calcularImporteTotalDetalle(parentIndex, $scope.ventasYCobrosPorVendedor.Clientes[parentIndex].Detalles);
            $scope.item[parentIndex] = { MascaraDeCalculo: $scope.mascaraDeCalculoPorDefecto };
            $scope.calcularTotales();
        }
    };

    $scope.quitarItemVenta = function (parentIndex, index) {
        $scope.ventasYCobrosPorVendedor.Clientes[parentIndex].Detalles.splice(index, 1);
        $scope.calcularImporteTotalDetalle(parentIndex, $scope.ventasYCobrosPorVendedor.Clientes[parentIndex].Detalles);
        $scope.calcularTotales();
    };

    $scope.calcularImporteTotalDetalle = function (parentIndex, detalles) {
        $scope.importeTotalDetalle = 0;
        if (detalles.length > 0) {
            for (var i = 0; i < detalles.length; i++) {
                $scope.importeTotalDetalle += Math.round(detalles[i].Importe * 100) / 100;
            }
        }
        $scope.ventasYCobrosPorVendedor.Clientes[parentIndex].ImporteTotal = $scope.importeTotalDetalle;
    };
    //#endregion

    //#region METODOS PARA GUARDAR
    $scope.datosRequeridosParaRealizarVentasYCobrosPorVendedor = function () {
        return $scope.ventasYCobrosPorVendedor.PuntoDeVenta === undefined || $scope.ventasYCobrosPorVendedor.Vendedor === undefined || $scope.ventasYCobrosPorVendedor.Fecha === undefined ||
            $scope.ventasYCobrosPorVendedor.Fecha === "" || $scope.ventasYCobrosPorVendedor.Caja === undefined || $scope.ventasYCobrosPorVendedor.Almacen === undefined;
    };

    $scope.verificarInconsistenciasYGuardar = function () {
        $scope.inconsistencias = [];
        var inconsistenciaDetalles = $scope.verificarQueHayanIngresadoLosDetallesCorrectamente();
        if (inconsistenciaDetalles.length > 0) {
            for (var i = 0; i < inconsistenciaDetalles.length; i++) {
                $scope.inconsistencias.push(inconsistenciaDetalles[i]);
            }
        }
        if ($scope.inconsistencias.length <= 0) {
            $scope.guardarVentasYCobrosPorVendedor();
        } else {
            $scope.hayinconsistencias = true;
            $timeout(function () { $scope.hayinconsistencias = false; }, 10000);
        }
    };

    $scope.verificarSiExisteAlMenosUnDetalleIngresado = function () {
        var bandera = false;
        for (var i = 0; i < $scope.ventasYCobrosPorVendedor.Clientes.length; i++) {
            var cliente = $scope.ventasYCobrosPorVendedor.Clientes[i];
            if (cliente.Detalles.length >= 1) {
                return bandera = true;
            }
        }
        return bandera;
    };

    $scope.verificarQueHayanIngresadoLosDetallesCorrectamente = function () {
        var inconsistencias = [];
        var hayRegistros = false;
        for (var i = 0; i < $scope.ventasYCobrosPorVendedor.Clientes.length; i++) {
            var cliente = $scope.ventasYCobrosPorVendedor.Clientes[i];
            var band = false;
            var mensaje = "";
            if (cliente.Detalles.length >= 1) {
                for (var j = 0; j < $scope.ventasYCobrosPorVendedor.Clientes[i].Detalles.length; j++) {
                    var detalle = cliente.Detalles[j];
                    if (parseFloat(detalle.Importe) === 0) { band = true; }
                    if (!detalle.Codigo) { mensaje = "- No ingresaste el codigo "; }
                }
                hayRegistros = hayRegistros || true;
            }
            if (band) {
                let inconsistencia = "El cliente " + cliente.Cliente.Nombre + mensaje + "- Tiene detalles de productos con importe S/. 0.00";
                inconsistencias.push(inconsistencia);
            }
        }
        if (!hayRegistros) {
            let inconsistencia = "No hay registros de venta y cobro por vendedor.";
            inconsistencias.push(inconsistencia);
        }
        return inconsistencias;
    };

    $scope.guardarVentasYCobrosPorVendedor = function () {
        ventaService.guardarVentasYCobrosPorVendedor({ ventasYCobrosPorVendedor: $scope.ventasYCobrosPorVendedor }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.inicializar();
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    };
    //#endregion

    //#region IMPRIMIR
    $scope.datosRequeridosParaImprimir = function () {
        return $scope.ventasYCobrosPorVendedor.Fecha === undefined || $scope.ventasYCobrosPorVendedor.Fecha === "";
    };

    $scope.imprimirComprobantes = function () {
        jsWebClientPrint.print('fecha=' + $scope.ventasYCobrosPorVendedor.Fecha);
    };

    $scope.imprimirRecibos = function () {
        jsWebClientPrint.print('fecha=' + $scope.ventasYCobrosPorVendedor.Fecha);
    };
    //#endregion

    //#region BANDEJA DE VENTAS Y COBROS
    $scope.iniciarBandejaVentasYCobrosMasivos = function () {
        $scope.fechaInicio = fechaInicio;
        $scope.fechaFin = fechaFin;
        $scope.listarBandejaVentasYCobrosMasivos();
    };

    $scope.listarBandejaVentasYCobrosMasivos = function () {
        ventaService.obtenerVentasYCobrosMasivos({ desde: $scope.fechaInicio, hasta: $scope.fechaFin }).success(function (data) {
            $scope.listaVentasYCobrosMasivasPorVendedor = data;
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    };

    $scope.obtenerVentasYCobrosPorVendedor = function (ventasYCobrosPorVendedor) {
        ventaService.obtenerVentasYCobrosMasivo({ idCentroDeAtencion: ventasYCobrosPorVendedor.IdCentroDeAtencion, fecha: ventasYCobrosPorVendedor.FechaEmision, idTransaccion: ventasYCobrosPorVendedor.IdTransaccion }).success(function (data) {
            $scope.verVentasYCobrosPorVendedor = data;
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    };

    $scope.cerrarVerVentaYCobro = function () {
        $scope.verVentasYCobrosPorVendedor = {};
    };
    //#endregion
});

app.controller('canjeDeComprobanteController', function ($scope, $q, $timeout, $rootScope, SweetAlert, $filter, DTOptionsBuilder, DTColumnDefBuilder, ventaService, centroDeAtencionService, empleadoService, conceptoService) {

    $scope.iniciarCanjeDeComprobante = function () {
        $scope.canjeDeComprobante = {};
        $scope.canjeDeComprobanteActivado = false;
        $scope.labelBotonDeCanjeDeComprobante = 'ACTIVAR CANJE DE COMPROBANTE';
        $scope.comprobantesSeleccionados = [];
    };

    $scope.accionCanjeDeComprobantes = function () {
        if ($scope.canjeDeComprobanteActivado) {
            $scope.canjeDeComprobanteActivado = false;
            $scope.labelBotonDeCanjeDeComprobante = 'ACTIVAR CANJE DE COMPROBANTE';
        } else {
            $scope.canjeDeComprobanteActivado = true;
            $scope.labelBotonDeCanjeDeComprobante = 'DESACTIVAR CANJE DE COMPROBANTE';
        }
    };

    $scope.verificarCanjeDeComprobante = function () {
        for (var i = 0; i < $scope.comprobantesSeleccionados.length; i++) {
            if ($scope.comprobantesSeleccionados[0].IdCliente !== $scope.comprobantesSeleccionados[i].IdCliente) {
                var mensaje = "No se puede realizar el caje de comprobante, el cliente debe ser el mismo";
                var hayClienteDistinto = true;
                break;
            }
        }
        if (hayClienteDistinto) {
            SweetAlert.warning("Advertencia", mensaje);
        }
    };

    $scope.registarCanjeDeComprobante = function () {
        ventaService.registrarCanjeDeComprobante({ idsOrdenes: $scope.comprobantesSeleccionados, TipoDeComprobante: $scope.canjeDeComprobante.TipoDeComprobante }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.canjeDeComprobante = {};
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema", data.error);
        });
    };

    $scope.obtenerTiposDeComprobante = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        ventaService.obtenerTiposDeComprobanteParaVenta({}).success(function (data) {
            $scope.tiposDeComprobantes = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    };

    $scope.cargarSeries = function (tipoComprobante) {
        if (tipoComprobante !== null) {
            $scope.series = angular.copy(tipoComprobante.Series);
        }
    };

});

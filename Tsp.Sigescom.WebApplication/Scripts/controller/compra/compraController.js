app.controller('compraController', function ($scope, $q, $timeout, $rootScope, SweetAlert, $filter, DTOptionsBuilder, DTColumnDefBuilder, compraService, maestroService, productoService, precioService, empleadoService, almacenService, proveedorService, centroDeAtencionService, conceptoService) {

    $scope.inicializar = function () {
        $scope.cargarParametros();
        $scope.inicializacionRealizada = false;
        $scope.limpiar();
        $scope.cargarColeccionesAsync();
        $scope.inicializarComponentes();
    }

    $scope.limpiar = function () {
        $scope.compra = { Detalles: [], EsCompraACredito: false, EsCreditoRapido: {}, Cuotas: [], Inicial: 0, Total: 0, Flete: 0, PagarInicialAlConfimar: false };
        $scope.financiamientoRealizado = {};
        $scope.hayInconsistencias = {};
        $scope.inconsistencias = [];
        $scope.valores = [];
    }

    $scope.cargarParametros = function () {
        $scope.tasaIGV = tasaIGV;
        $scope.aplicaLeyAmazonia = aplicaLeyAmazonia;
        $scope.idTipoActorPersonaJuridica = idTipoActorPersonaJuridica;
        $scope.realizaCompraAlCredito = realizaCompraAlCredito;
        $scope.mostrarTrazabilidadDeConcepto = permitirLoteEnDetalleDeCompra;
        $scope.permitirLoteEnDetalleDeCompra = permitirLoteEnDetalleDeCompra;
        $scope.permitirRegistroEnDetalleDeCompra = permitirRegistroEnDetalleDeCompra;
        $scope.permitirVencimientoEnDetalleDeCompra = permitirVencimientoEnDetalleDeCompra;
        $scope.idProveedorGenerico = idProveedorGenerico;
        $scope.idTipoPersonaSeleccionadaPorDefecto = idTipoPersonaSeleccionadaPorDefecto;
        $scope.idTipoDocumentoSeleccionadaConTipoPersonaNatural = idTipoDocumentoSeleccionadaConTipoPersonaNatural;
        $scope.idTipoDocumentoSeleccionadaConTipoPersonaJuridica = idTipoDocumentoSeleccionadaConTipoPersonaJuridica;
        $scope.idTipoDocumentoIdentidadDni = idTipoDocumentoIdentidadDni;
        $scope.idTipoDocumentoIdentidadRuc = idTipoDocumentoIdentidadRuc;
        $scope.fechaActual = fechaActual;
        $scope.permitirRegistroFlete = permitirRegistroFlete;
        $scope.permitirMultipleIngresoDelMismoDetalle = permitirMultipleIngresoDelMismoDetalle;
        $scope.diaVencimiento = {
            Lista: [{ id: 1, nombre: "1 de cada mes" }, { id: 2, nombre: "2 de cada mes" }, { id: 3, nombre: "3 de cada mes" }, { id: 4, nombre: "4 de cada mes" }, { id: 5, nombre: "5 de cada mes" }, { id: 6, nombre: "6 de cada mes" }, { id: 7, nombre: "7 de cada mes" }, { id: 8, nombre: "8 de cada mes" }, { id: 9, nombre: "9 de cada mes" }, { id: 10, nombre: "10 de cada mes" },
            { id: 11, nombre: " 11 de cada mes" }, { id: 12, nombre: "12 de cada mes" }, { id: 13, nombre: "13 de cada mes" }, { id: 14, nombre: "14 de cada mes" }, { id: 15, nombre: "15 de cada mes" }, { id: 16, nombre: "16 de cada mes" }, { id: 17, nombre: "17 de cada mes" }, { id: 18, nombre: "18 de cada mes" }, { id: 19, nombre: "19 de cada mes" }, { id: 20, nombre: "20 de cada mes" },
            { id: 21, nombre: "21 de cada mes" }, { id: 22, nombre: "22 de cada mes" }, { id: 23, nombre: "23 de cada mes" }, { id: 24, nombre: "24 de cada mes" }, { id: 25, nombre: "25 de cada mes" }, { id: 26, nombre: "26 de cada mes" }, { id: 27, nombre: "27 de cada mes" }, { id: 28, nombre: "28 de cada mes" },]
        };
        $scope.numeroDecimalesEnCantidad = numeroDecimalesEnCantidad;
        $scope.numeroDecimalesEnPrecio = numeroDecimalesEnPrecio;
        $scope.mostrarBuscadorCodigoBarra = mostrarBuscadorCodigoBarra;
        $scope.modoSeleccionConcepto = modoSeleccionConcepto;
        $scope.modoSeleccionTipoFamilia = modoSeleccionTipoFamilia;
        $scope.permitirCambioPrecio = permitirCambioPrecio;
        $scope.minimoCaracteresBuscarConcepto = minimoCaracteresBuscarConcepto;
        $scope.tiempoEsperaBusquedaSelector = tiempoEsperaBusquedaSelector;
        $scope.minimoCaracteresBuscarActorComercial = minimoCaracteresBuscarActorComercial;
        $scope.idDetalleMaestroCatalogoDocumentoFactura = idDetalleMaestroCatalogoDocumentoFactura;
        $scope.mascaraDeVisualizacionValidacionRegistroProveedor = mascaraDeVisualizacionValidacionRegistroProveedor;
        $scope.informacionSelectorConcepto = informacionSelectorConcepto;
        $scope.permitirSeleccionarGrupoProveedor = permitirSeleccionarGrupoProveedor;

    }

    $scope.inicializarComponentes = function () {
        $scope.rolProveedor = { Id: idRolProveedor, Nombre: 'PROVEEDOR' };
        $scope.inicializacionRealizada = true;
    }

    $scope.cargarColeccionesAsync = function () {
        $scope.obtenerTiposDeComprobante();
    }

    $scope.cargarColeccionesSync = function () {

    }

    $scope.establecerDatosPorDefecto = function () {
        $scope.financiamientoRealizado = false;
        $scope.compra.Observacion = "NINGUNA";
        $scope.compra.TipoCompra = $scope.aplicaLeyAmazonia ? 1 : 2;
        $scope.compra.FechaRegistro = $scope.fechaActual.split(" ")[0];
        $scope.hayInconsistencias = true;
        $scope.montoTotal = 0;
        $scope.esConfirmacionVenta = false;
        $scope.rucValidado = true;
        $scope.selectorProveedorAPI.EstablecerActorPorDefecto();
        $scope.verificarInconsistencias();
    }

    $scope.inicioRealizadoProveedor = function () {
        $scope.establecerDatosPorDefecto();
    };

    //#region OBTENCION DE DATOS
    $scope.obtenerTiposDeComprobante = function () {
        compraService.obtenerTiposDeComprobanteParaCompra({}).success(function (data) {
            $scope.tiposDeComprobantesMasSeries = data;
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.cargarSeries = function () {
        $scope.compra.TipoDeComprobante.NumeroIngresado = '';//el numero ingresado es 0, para que no se muestre se pone vacio 
        if ($scope.compra.TipoDeComprobante != null) {
            $scope.series = angular.copy($scope.compra.TipoDeComprobante.Series);
        }
        $scope.verificarInconsistencias();
    }

    $rootScope.dtOptions.withLightColumnFilter({
        '1': { type: 'text', className: 'form-control padding-left-right-3' },
        '2': { type: 'text', className: 'form-control padding-left-right-3' },
        '3': { type: 'text', className: 'form-control padding-left-right-3' },
        '4': { type: 'text', className: 'form-control padding-left-right-3' },
        '5': { type: 'text', className: 'form-control padding-left-right-3' },
        '6': { type: 'text', className: 'form-control padding-left-right-3' },
        '7': { type: 'text', className: 'form-control padding-left-right-3' },
        '8': { type: 'text', className: 'form-control padding-left-right-3' },
    });


    $scope.cambioProveedor = function (actorComercial) {
        $scope.compra.Proveedor = actorComercial;
        $scope.verificarInconsistencias();
    };

    //#endregion

    //#region OPERACIONES Y CALCULOS

    $scope.seleccionConcepto = function (conceptoComercial) {
        $scope.detalle = {};
        $scope.detalle.Producto = angular.copy(conceptoComercial);
        $scope.agregarDetalle();
    }


    $scope.agregarDetalle = function () {
        var validar = false;
        var index = 0;
        if ($scope.compra.Detalles.length > 0) {
            for (var i = 0; i < $scope.compra.Detalles.length; i++) {
                if ($scope.detalle.Producto.Id == $scope.compra.Detalles[i].Producto.Id) {
                    validar = true;
                    index = i;
                    break;
                }
            }
        }
        if (validar && !$scope.permitirMultipleIngresoDelMismoDetalle) {
            $scope.agregarCantidad($scope.compra.Detalles, index);
            $scope.detalle = {};
        }
        else {
            $scope.detalle.Cantidad = 1;
            $scope.detalle.Descuento = 0;
            $scope.detalle.PrecioUnitario = 0;
            $scope.detalle.Igv = 0;
            $scope.detalle.Neto = 0;
            $scope.detalle.ValorCompra = 0;
            $scope.detalle.Importe = 0;

            $scope.detalle.VersionFila = $scope.detalle.Producto.VersionFila;
            //$scope.detalle.Producto.ConceptoBasico.TieneCaracteristicasPropias = $scope.conceptoBasicoSeleccionado.TieneCaracteristicasPropias;
            //$scope.detalle.Producto.ConceptoBasico.NombresCaracteristicasPropias = angular.copy($scope.conceptoBasicoSeleccionado.Caracteristicas);

            $scope.detalle.Producto.ValoresCaracteristicasPropias = [];
            //$scope.detalle.Lote = $scope.mostrarTrazabilidadDeConcepto ? '' : null;
            //$scope.detalle.Vencimiento = $scope.mostrarTrazabilidadDeConcepto ? '' : null;
            //$scope.detalle.Registro = $scope.mostrarTrazabilidadDeConcepto ? '' : null;
            $scope.VersionFila = $scope.detalle.Producto.VersionFila;
            //Agrega a la tabla de detalles de compra
            $scope.compra.Detalles.unshift($scope.detalle);
            $scope.detalle = {};
            $scope.calcularTotal($scope.compra.Detalles);
        }

        $(function () {
            $('.td-datepicker').datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true,
                todayHighlight: true,
                language: 'es'
            });
        });
    }

    $scope.formatoDecimalCantidad = function (event, $filter) {
        let valor = event.target.value;
        event.target.value = parseFloat(valor).toFixed($scope.numeroDecimalesEnCantidad);
    }

    $scope.agregarCantidad = function (detalles, index) {
        detalles[index].Cantidad++;
        $scope.calcularImporteDesdeCantidad(detalles, index);
        $scope.calcularTotal(detalles);
    }

    $scope.quitarCantidad = function (detalles, index) {
        detalles[index].Cantidad--;
        if (detalles[index].Cantidad != 0) {
            $scope.calcularImporteDesdeCantidad(detalles, index);
            $scope.calcularTotal($scope.compra.Detalles);
        } else {
            $scope.quitarDetalle(index);
        }
    }

    $scope.quitarDetalle = function (index) {
        $scope.compra.Detalles.splice(index, 1);
        $scope.calcularTotal($scope.compra.Detalles);
    }

    $scope.calcularImporteDesdeCantidad = function (detalles, index) {
        detalles[index].Cantidad = (detalles[index].Cantidad == "") ? 0 : detalles[index].Cantidad;
        detalles[index].PrecioUnitario = parseFloat(detalles[index].PrecioUnitario).toFixed($scope.numeroDecimalesEnPrecio);
        detalles[index].ValorCompra = parseFloat(parseFloat(detalles[index].Cantidad) * parseFloat( detalles[index].PrecioUnitario)).toFixed(2);
        detalles[index].Descuento = parseFloat(detalles[index].Descuento).toFixed(2);
        detalles[index].Neto = parseFloat(parseFloat(detalles[index].ValorCompra) - parseFloat(detalles[index].Descuento)).toFixed(2);
        detalles[index].Igv = ($scope.compra.TipoCompra != 1) ? (parseFloat(detalles[index].Neto) * $scope.tasaIGV).toFixed(2) : (0).toFixed(2);
        detalles[index].Importe = parseFloat(parseFloat(detalles[index].Neto) + parseFloat(detalles[index].Igv)).toFixed(2);
        $scope.calcularTotal(detalles);
    }

    $scope.calcularImporteDesdePrecio = function (detalles, index) {
        detalles[index].PrecioUnitario = (detalles[index].PrecioUnitario == "") ? 0 : detalles[index].PrecioUnitario;
        detalles[index].ValorCompra = parseFloat(parseFloat(detalles[index].Cantidad) * parseFloat(detalles[index].PrecioUnitario)).toFixed(2);
        detalles[index].Descuento = parseFloat(detalles[index].Descuento).toFixed(2);
        detalles[index].Neto = parseFloat(parseFloat(detalles[index].ValorCompra) - parseFloat(detalles[index].Descuento)).toFixed(2);
        detalles[index].Igv = ($scope.compra.TipoCompra != 1) ? (parseFloat(detalles[index].Neto) * $scope.tasaIGV).toFixed(2) : (0).toFixed(2);
        detalles[index].Importe = parseFloat(parseFloat(detalles[index].Neto) + parseFloat(detalles[index].Igv)).toFixed(2);
        $scope.calcularTotal(detalles);
    }

    $scope.calcularValoresIngresadoImporte = function (detalles, index) {
        detalles[index].Importe = (detalles[index].Importe == "") ? 0 : detalles[index].Importe;
        detalles[index].Descuento = parseFloat(detalles[index].Descuento).toFixed(2);
        detalles[index].ValorCompra = ($scope.compra.TipoCompra != 1) ? parseFloat(parseFloat(detalles[index].Importe) - parseFloat(parseFloat(detalles[index].Importe) - parseFloat(detalles[index].Importe / (1 + $scope.tasaIGV))) + parseFloat(detalles[index].Descuento)).toFixed(2) : parseFloat(detalles[index].Importe).toFixed(2);
        detalles[index].Neto = parseFloat(parseFloat(detalles[index].ValorCompra) - parseFloat(detalles[index].Descuento)).toFixed(2);
        detalles[index].Igv = ($scope.compra.TipoCompra != 1) ? (parseFloat(detalles[index].Neto) * $scope.tasaIGV).toFixed(2) : (0).toFixed(2);
        detalles[index].PrecioUnitario = parseFloat(parseFloat(detalles[index].ValorCompra / detalles[index].Cantidad)).toFixed($scope.numeroDecimalesEnPrecio);
        $scope.calcularTotal(detalles);
    }

    $scope.calcularValoresIngresadoValorCompra = function (detalles, index) {
        detalles[index].ValorCompra = (detalles[index].ValorCompra == "") ? 0 : detalles[index].ValorCompra;
        detalles[index].PrecioUnitario = parseFloat(detalles[index].ValorCompra / detalles[index].Cantidad).toFixed($scope.numeroDecimalesEnPrecio);
        detalles[index].Descuento = parseFloat(detalles[index].Descuento).toFixed(2);
        detalles[index].Neto = parseFloat(parseFloat(detalles[index].ValorCompra) - parseFloat(detalles[index].Descuento)).toFixed(2);
        detalles[index].Igv = ($scope.compra.TipoCompra != 1) ? (detalles[index].Neto * $scope.tasaIGV).toFixed(2) : (0).toFixed(2);
        detalles[index].Importe = parseFloat(parseFloat(detalles[index].Neto) + parseFloat(detalles[index].Igv)).toFixed(2);
        $scope.calcularTotal(detalles);
    }

    $scope.calcularImporteDesdeDescuento = function (detalles, index) {
        detalles[index].Descuento = (detalles[index].Descuento == "") ? 0 : detalles[index].Descuento;
        detalles[index].Neto = parseFloat(parseFloat(detalles[index].ValorCompra) - parseFloat(detalles[index].Descuento)).toFixed(2);
        detalles[index].Igv = ($scope.compra.TipoCompra != 1) ? (detalles[index].Neto * $scope.tasaIGV).toFixed(2) : (0).toFixed(2);
        detalles[index].Importe = parseFloat(parseFloat(detalles[index].Neto) + parseFloat(detalles[index].Igv)).toFixed(2);
        $scope.calcularTotal(detalles);
    }

    $scope.cambioDeExoneradoIgv = function (detalles) {
        for (let i = 0; i < detalles.length; i++) {
            $scope.calcularImporteDesdeCantidad(detalles, i);
        }
    }

    $scope.calcularTotal = function (detalles) {
        if ($scope.compra.Flete > 0) {
            $scope.compra.Total = parseFloat($scope.compra.Flete);
            $scope.compra.Igv = ($scope.compra.TipoCompra != 1) ? (Math.round((parseFloat($scope.compra.Flete - ($scope.compra.Flete / (1 + $scope.tasaIGV)))) * 100) / 100) : 0;
            $scope.compra.SubTotal = parseFloat($scope.compra.Total) - parseFloat($scope.compra.Igv);
        } else {
            $scope.compra.SubTotal = 0;
            $scope.compra.Igv = 0;
            $scope.compra.Total = 0;
        }
        $scope.compra.Descuento = 0;

        for (i = 0; i < detalles.length; i++) {
            $scope.compra.Total += parseFloat(detalles[i].Importe);
            $scope.compra.Descuento += parseFloat(detalles[i].Descuento);
            $scope.compra.Igv += parseFloat(detalles[i].Igv);
        }
        $scope.compra.SubTotal = parseFloat($scope.compra.Total) - parseFloat($scope.compra.Igv) + parseFloat($scope.compra.Descuento);
        if ($scope.compra.EsCompraACredito == true && $scope.financiamientoRealizado == true) {
            $("#reiniciar-financimiento").click();
        }
        $scope.verificarInconsistencias();
    }

  
    //#endregion

    //#region VALIDACIONES Y GUARDADO

    $scope.numeroComprobanteEsValido = function () {
        if ($scope.compra.Proveedor && $scope.compra.TipoDeComprobante && $scope.compra.NumeroSerieDeComprobante && $scope.compra.NumeroDeComprobante) {
            compraService.numeroComprobanteEsValido({ idProveedor: $scope.compra.Proveedor.Id, idTipoComprobante: $scope.compra.TipoDeComprobante.Id, numeroDeSerie: $scope.compra.NumeroSerieDeComprobante, numeroComprobante: $scope.compra.NumeroDeComprobante }).success(function (data) {
                if (data.valido) {
                    alert("El Proveedor: " + $scope.compra.Proveedor.RazonSocial + " ya tiene una " + $scope.compra.TipoDeComprobante.Nombre + " registrado con el número de comprobante " + $scope.compra.NumeroDeComprobante + ", por favor ingrese otro número");
                    $scope.compra.NumeroDeComprobante = "";
                }
            }).error(function (data) {
                SweetAlert.error2(data);
            });
        }
    }

    $scope.verificarInconsistencias = function () {
        var hayCantidad0 = false;
        var hayImporte0 = false;
        $scope.inconsistencias = [];
        if ($scope.compra.Proveedor == undefined) {
            $scope.inconsistencias.push("Es necesario seleccionar un proveedor.");
        }
        if ($scope.compra.FechaRegistro == undefined || $scope.compra.FechaRegistro == "") {
            $scope.inconsistencias.push("Es necesario ingresar fecha de registro.");
        }
        if ($scope.compra.TipoCompra == undefined || $scope.compra.TipoCompra == "") {
            $scope.inconsistencias.push("Es necesario seleccionar el tipo de compra.");
        }
        if ($scope.compra.TipoDeComprobante == undefined) {
            $scope.inconsistencias.push("Es necesario seleccionar un documento.");
        } else {
            if ($scope.compra.TipoDeComprobante.EsPropio == false) {
                if ($scope.compra.TipoDeComprobante.SerieIngresada == "" || $scope.compra.TipoDeComprobante.SerieIngresada == null) {
                    $scope.inconsistencias.push("Es necesario ingresar la serie de comprobante.");
                } if ($scope.compra.TipoDeComprobante.NumeroIngresado == "" || $scope.compra.TipoDeComprobante.NumeroIngresado == null) {
                    $scope.inconsistencias.push("Es necesario ingresar el numero de comprobante.");
                }
            }
            if ($scope.compra.TipoDeComprobante.TipoComprobante.Id == $scope.idDetalleMaestroCatalogoDocumentoFactura && $scope.compra.Proveedor.TipoDocumentoIdentidad.Id != $scope.idTipoDocumentoIdentidadRuc) {
                $scope.inconsistencias.push("Es necesario que el proveedor tenga Ruc.");
            }
        }
        if ($scope.compra.Detalles.length <= 0) {
            $scope.inconsistencias.push("Es necesario seleccionar al menos un producto.");
        } else {
            for (var i = 0; i < $scope.compra.Detalles.length; i++) {
                hayCantidad0 = $scope.compra.Detalles[i].Cantidad <= 0 ? true : false;
                hayImporte0 = $scope.compra.Detalles[i].Importe <= 0 ? true : false;
            }
        }
        if (hayCantidad0) {
            $scope.inconsistencias.push("Es necesario la cantidad de los detalles sea mayor a 0.");
        }
        if (hayImporte0) {
            $scope.inconsistencias.push("Es necesario que el importe de cada detalle sea mayor a 0.");
        }
        if (parseFloat($scope.compra.Total) <= 0) {
            $scope.inconsistencias.push("Es necesario que el importe total sea mayor a 0.");
        }
        if (parseFloat($scope.compra.Total) - parseFloat($scope.compra.Flete) <= 0) {
            $scope.inconsistencias.push("Es necesario que el importe total de la compra sea mayor a 0.");
        }
        if ($scope.esCompraCorporativa == true) {
            if ($scope.compra.CentroDeAtencion == undefined) {
                $scope.inconsistencias.push("Es necesario seleccionar el centro de atencion que registra la compra.");
            }
            if ($scope.compra.Comprador == undefined) {
                $scope.inconsistencias.push("Es necesario seleccionar el comprador que esta realizando la operacion.");
            }
        }
        $scope.hayInconsistencias = ($scope.inconsistencias.length == 0) ? false : true;
    }

    $scope.guardar = function () {
        compraService.confirmarCompra({ compra: $scope.compra }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.limpiar();
            $scope.establecerDatosPorDefecto();
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.guardarEnModal = function () {
        compraService.confirmarCompra({ compra: $scope.compra }).success(function (data) {
            $scope.messageSuccess(data.result_description);
            $('#modal-registro-compra').modal('hide');
            $scope.limpiar();
            $scope.establecerDatosPorDefecto();
            $scope.listarBandeja();
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.verificarTrasladoTotal = function () {
        var temp = false;
        if ($scope.compra.HayRegistroMovimientoDeAlmacen) {
            temp = true;
            for (var i = 0; i < $scope.movimiento.Detalles.length; i++) {
                if ($scope.movimiento.Detalles[i].Ordenado - $scope.movimiento.Detalles[i].RecibidoEntregado != parseFloat($scope.movimiento.Detalles[i].IngresoSalidaActual)) {
                    temp = false;
                    break;
                }
            }
        }
        $scope.movimiento.EsTrasladoTotal = temp;
    }

    $scope.registarCompra = function () {
        compraService.guardarCompra({ compra: $scope.compra }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.limpiar();
            $scope.establecerDatosPorDefecto();
            $('#modal-registro-compra-corporativa').modal('hide');
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.registarCompraEnModal = function () {
        compraService.guardarCompra({ compra: $scope.compra }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            if ($scope.esEdicion) {
                $scope.limpiarVerCompraCorporativa();
                $('#modal-ver-compra-corporativa').modal('hide');
            }
            $scope.limpiar();
            $scope.establecerDatosPorDefecto();
            $scope.listarBandejaComprasCorporativas();
            $('#modal-registro-compra-corporativa').modal('hide');
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.resolverGuardarCompraCorporativa = function () {
        if ($scope.esConfirmacionVenta) {
            $scope.guardarConfirmacionCompra();
        } else {
            $scope.guardarCompraCorporativa();
        }
    }

    $scope.guardarCompraCorporativa = function () {
        $scope.verificarTrasladoTotal();
        compraService.guardarCompraCorporativa({ compra: $scope.compra, trazaPago: $scope.trazaDePago, ingresoMercaderia: $scope.movimiento }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.limpiar();
            $scope.establecerDatosPorDefecto();
            $('#modal-registro-compra-corporativa').modal('hide');
            $('#modal-registro-traza-pago').modal('hide');
            $('#modal-registro-movimiento-mercaderia').modal('hide');
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.guardarCompraCorporativaEnModal = function () {
        $scope.verificarTrasladoTotal();
        compraService.guardarCompraCorporativa({ compra: $scope.compra, trazaPago: $scope.trazaDePago, ingresoMercaderia: $scope.movimiento }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.limpiar();
            $scope.establecerDatosPorDefecto();
            $scope.listarBandejaComprasCorporativas();
            $('#modal-registro-compra-corporativa').modal('hide');
            $('#modal-registro-traza-pago').modal('hide');
            $('#modal-registro-movimiento-mercaderia').modal('hide');
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    //#endregion

    //#region FIJAR PRECIO DE VENTA

    $scope.abrirModalFijarPrecioDeVenta = function (idMercaderia, precioUnitario, nombreMercaderia) {
        $scope.idMercaderia = idMercaderia;
        precioService.obtenerPreciosDeConceptoNegocio({ idConceptoNegocio: $scope.idMercaderia }).success(function (data) {
            $scope.precio = data;
            for (var i = 0; i < $scope.precio.PuntosDePrecio.length; i++) {
                for (var j = 0; j < $scope.precio.PuntosDePrecio[i].Tarifas.length; j++) {
                    date = $scope.precio.PuntosDePrecio[i].Tarifas[j].FechaDesde.slice($scope.precio.PuntosDePrecio[i].Tarifas[j].FechaDesde.indexOf("(") + 1, $scope.precio.PuntosDePrecio[i].Tarifas[j].FechaDesde.indexOf(")"));
                    $scope.precio.PuntosDePrecio[i].Tarifas[j].FechaDesde = $scope.formatDate(new Date(+date), "ES");
                    $("#fechaDesde" + i + j).datepicker('setDate', $scope.precio.PuntosDePrecio[i].Tarifas[j].FechaDesde);
                    date = $scope.precio.PuntosDePrecio[i].Tarifas[j].FechaHasta.slice($scope.precio.PuntosDePrecio[i].Tarifas[j].FechaHasta.indexOf("(") + 1, $scope.precio.PuntosDePrecio[i].Tarifas[j].FechaHasta.indexOf(")"));
                    $scope.precio.PuntosDePrecio[i].Tarifas[j].FechaHasta = $scope.formatDate(new Date(+date), "ES");
                    $("#fechaHasta" + i + j).datepicker('setDate', $scope.precio.PuntosDePrecio[i].Tarifas[j].FechaHasta);
                }
            }
            $(function () {
                $('.td-datepicker').datepicker({
                    format: 'dd/mm/yyyy',
                    autoclose: true,
                    todayHighlight: true,
                    language: 'es'
                });
            });
        }).error(function (data) {
            SweetAlert.error2(data);
        });
        $scope.precioUnitario = precioUnitario;
        $scope.nombreMercaderia = nombreMercaderia;
        $("#modal-precio-venta").click();
    }

    $scope.guardarPrecioDeVenta = function () {
        precioService.guardarPrecio({ registroPrecio: $scope.precio }).success(function (data) {
            $scope.obtenerPrecios();
            SweetAlert.success("Correcto", data.result_description);
            $scope.cerrar('modal-registro-precio');
        }).error(function (data, status) {
            SweetAlert.error2(data);
        });
    }

    $scope.calcularGanancia = function (nuevoPrecio) {
        $scope.ganancia = parseFloat(nuevoPrecio) > parseFloat($scope.precioUnitario) ? 10 : 0;
    }

    $scope.cerrarFijarPrecio = function () {
        $scope.precio = {};
    }
    //#endregion

    //#region FINANCIAMIENTO

    $scope.iniciarFinanciamiento = function () {
        if ($scope.compra.EsCompraACredito == true) {
            $scope.compra.EsCreditoRapido = true;
        } else {
            $scope.compra.EsCreditoRapido = {};
        }
        $scope.limpiarCuotas();
    }

    $scope.financiamientoConfigurado = function () {
        $scope.financiamientoRealizado = false;
        $scope.compra.EsCreditoRapido = false;
        $scope.limpiarCuotas();
        $("#cuota-modal").click();
    }

    $scope.limpiarCuotas = function () {
        $scope.financiamientodetalle = { cuota: 1, inicial: 0, capital: $scope.compra.Total, total: $scope.compra.Total, interes: 0 };
        $scope.cuotasdetalle = [];
        $scope.montoTotal = $scope.compra.Total;
    }

    $scope.calcularTotalFinanciamiento = function () {
        $scope.financiamientodetalle.capital = $scope.financiamientodetalle.inicial > 0 || $scope.financiamientodetalle.inicial != "" ? $scope.compra.Total - $scope.financiamientodetalle.inicial : $scope.compra.Total;
        $scope.financiamientodetalle.total = $scope.financiamientodetalle.interes > 0 || $scope.financiamientodetalle.interes != "" ? $scope.financiamientodetalle.capital + ($scope.financiamientodetalle.capital * $scope.financiamientodetalle.interes / 100) : $scope.financiamientodetalle.capital;
        $scope.limpiarCronogramaDePagos();
    }

    $scope.limpiarCronogramaDePagos = function () {
        if ($scope.financiamientodetalle.cuota > 1) {
            $scope.financiamientodetalle.fechaRegistro = undefined;
        } else {
            if ($scope.financiamientodetalle.cuota == 1) {
                $scope.financiamientodetalle.diavencimiento = undefined;
            }
        }
        $scope.cuotasdetalle = [];
    }

    $scope.generarCuota = function () {
        $scope.cuotasdetalle = [];
        $scope.cuotadetalle = {};
        let temp = $scope.financiamientodetalle.cuota;
        let arrayDeFechaRegistro = $scope.fechaActual.split("/");
        let anioHora = arrayDeFechaRegistro[2].split(" ");
        let anio = anioHora[0];
        let mes = arrayDeFechaRegistro[1];
        let dia = $scope.financiamientodetalle.diavencimiento;
        let inicial = parseFloat($scope.financiamientodetalle.inicial);
        $scope.compra.Inicial = inicial;

        if (inicial !== $scope.compra.Total) {
            if (inicial > 0) {
                $scope.cuotadetalle = { CapitalCuota: $scope.financiamientodetalle.inicial, InteresCuota: 0.00, ImporteCuota: $scope.financiamientodetalle.inicial, FechaVencimiento: $scope.formatDate(new Date(), "ES"), EsCuotaInicial: true }
                $scope.cuotasdetalle.push($scope.cuotadetalle);
                $scope.cuotadetalle = {};
            }
            if (temp == 1) {
                $scope.cuotadetalle.CapitalCuota = $scope.formatNumber(parseFloat($scope.financiamientodetalle.capital) / parseFloat(temp), 2);
                $scope.cuotadetalle.InteresCuota = $scope.financiamientodetalle.interes > 0 || $scope.financiamientodetalle.interes != "" ? $scope.formatNumber(parseFloat($scope.financiamientodetalle.capital * $scope.financiamientodetalle.interes / 100) / parseFloat(temp), 2) : 0.00;
                $scope.cuotadetalle.ImporteCuota = $scope.formatNumber(parseFloat($scope.financiamientodetalle.total) / parseFloat(temp), 2);
                $scope.cuotadetalle.FechaVencimiento = $scope.financiamientodetalle.fechaRegistro;
                $scope.cuotadetalle.EsCuotaInicial = false;
                $scope.cuotasdetalle.push($scope.cuotadetalle);
                $scope.cuotadetalle = {};
            } else {
                for (let i = 0; i < temp; i++) {
                    $scope.cuotadetalle.CapitalCuota = $scope.formatNumber(parseFloat($scope.financiamientodetalle.capital) / parseFloat(temp), 2);
                    $scope.cuotadetalle.InteresCuota = $scope.financiamientodetalle.interes > 0 || $scope.financiamientodetalle.interes != "" ? $scope.formatNumber(parseFloat($scope.financiamientodetalle.capital * $scope.financiamientodetalle.interes / 100) / parseFloat(temp), 2) : 0.00;
                    $scope.cuotadetalle.ImporteCuota = $scope.formatNumber(parseFloat($scope.financiamientodetalle.total) / parseFloat(temp), 2);
                    if (mes == 13) {
                        mes = 1;
                        anio++;
                    }
                    if (mes == 2 && (dia == 28 || dia == 29)) {
                        $scope.cuotadetalle.FechaVencimiento = $scope.formatDate(new Date(anio, mes, 0), "ES");
                    } else if ((mes == 1 || mes == 3 || mes == 5 || mes == 7 || mes == 8 || mes == 10 || mes == 12) && dia == 31) {
                        $scope.cuotadetalle.FechaVencimiento = $scope.formatDate(new Date(anio, mes, 0), "ES");
                    } else {
                        $scope.cuotadetalle.FechaVencimiento = $scope.formatDate(new Date(anio, mes, dia), "ES");
                    }
                    $scope.cuotadetalle.EsCuotaInicial = false;
                    $scope.cuotasdetalle.push($scope.cuotadetalle);
                    $scope.cuotadetalle = {};
                    mes++;
                }
            }
        }
        $scope.compra.Cuotas = angular.copy($scope.cuotasdetalle);
        $scope.financiamientoRealizado = true;
    }

    $scope.deshabilitarBotonGenerar = function (fechaRegistro, diavencimiento) {
        return fechaRegistro === undefined && diavencimiento === undefined;
    }

    $scope.seleccionarEsCredito = function () {
        if ($scope.compra.EsCompraACredito != {}) {
            $scope.compra.EsCompraACredito = true;
        }
    }

    $scope.finalizarCredito = function () {
        $scope.compra.EsCompraACredito = false;
        $scope.compra.PagarInicialAlConfimar = false;
        $scope.compra.EsCreditoRapido = {};
    }

    $scope.seleccionarCreditoRapido = function () {
        $scope.compra.PagarInicialAlConfimar = false;
    }

    //#endregion

    /////////////////////////////////////////// BANDEJA COMPRAS  //////////////////////////////////////////////

    $scope.inicializarDesdeBandejaCompras = function () {
        //Cargar parametros
        $scope.fechaInicio = fechaInicio;
        $scope.fechaFin = fechaFin;
        //Listar bandejas de compras
        $scope.listarBandeja();
        $scope.inicializar();
    }

    $scope.listarBandeja = function () {
        compraService.obtenerCompras({ desde: $scope.fechaInicio, hasta: $scope.fechaFin }).success(function (data) {
            $scope.compras = data;
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.verDetallesCompra = function (item) {
        compraService.obtenerOrdenCompraYDetallesOrden({ idOrdenCompra: item.Id }).success(function (data) {
            $scope.verCompra = data;
            $scope.EsOrdenDeCompra = item.EsOrdenDeCompra;
            $scope.IdOrden = item.Id;
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.cerrarVerCompra = function () {
        $scope.verCompra = {};
    }

    $scope.cerrarCompra = function () {
        $scope.inicializar();
    }

    //#region NOTAS DE DEBITO Y CREDITO

    $scope.verDocumentoDeCompra = function (item) {
        compraService.obtenerDocumentoParaOperacionesEnCompra({ idOrden: item.Id }).success(function (data) {
            $scope.verDocumento = data;
            $scope.hayRegistroDeNota = false;
            $scope.EsOrdenDeCompra = item.EsOrdenDeCompra;
            $scope.IdOrden = item.Id;
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.iniciarInvalidacion = function () {
        $scope.invalidacion = {};
    }

    $scope.iniciarEmisionDeNotaDeDebito = function () {
        $scope.etiquetaDeNota = "DEBITO";
        $scope.cargarDatosParaEmisionDeNota(true);
    }

    $scope.iniciarEmisionDeNotaDeCredito = function () {
        $scope.etiquetaDeNota = "CREDITO";
        $scope.cargarDatosParaEmisionDeNota(false);
    }

    $scope.cargarDatosParaEmisionDeNota = function (esParaNotaDeDebito) {
        $scope.limpiarNotaDeDocumento();
        $scope.cargarParametrosParaNota();
        $scope.cargarColeccionesSyncParaNota(esParaNotaDeDebito).then(function (resultado_) {
            $scope.establecerDatosPorDefectoParaNota(esParaNotaDeDebito);
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
    }

    $scope.limpiarNotaDeDocumento = function () {
        $scope.notaDeDocumento = {};
    }

    $scope.cargarParametrosParaNota = function () {
        $scope.idDetalleMaestroAnulacionDeLaOperacion = idDetalleMaestroAnulacionDeLaOperacion;
        $scope.idDetalleMaestroAnulacionPorErrorEnElRuc = idDetalleMaestroAnulacionPorErrorEnElRuc;
        $scope.idDetalleMaestroCorreccionPorErrorEnLaDescripcion = idDetalleMaestroCorreccionPorErrorEnLaDescripcion;
        $scope.idDetalleMaestroDescuentoGlobal = idDetalleMaestroDescuentoGlobal;
        $scope.idDetalleMaestroDescuentoPorItem = idDetalleMaestroDescuentoPorItem;
        $scope.idDetalleMaestroDevolucionTotal = idDetalleMaestroDevolucionTotal;
        $scope.idDetalleMaestroDevolucionPorItem = idDetalleMaestroDevolucionPorItem;
        $scope.idDetalleMaestroBonificacion = idDetalleMaestroBonificacion;
        $scope.idDetalleMaestroDisminucionEnElValor = idDetalleMaestroDisminucionEnElValor;
        $scope.idDetalleMaestroOtrosConceptos = idDetalleMaestroOtrosConceptos;
        $scope.idDetalleMaestroInteresesPorMora = idDetalleMaestroInteresesPorMora;
        $scope.idDetalleMaestroAumentoEnElValor = idDetalleMaestroAumentoEnElValor;
        $scope.idDetalleMaestroPenalidadesYOtrosConceptos = idDetalleMaestroPenalidadesYOtrosConceptos;
    }

    $scope.establecerDatosPorDefectoParaNota = function (esParaNotaDeDebito) {
        $scope.hayRegistroDeNota = true;
        $scope.notaDeOperacionAcordion = true;
        $scope.detalleDocumentoAcordion = false;
        $scope.notaDeDocumento.EsDebito = esParaNotaDeDebito;
        $scope.tasaIGVParaNota = $scope.verDocumento.Igv > 0 ? $scope.tasaIGV : 0;
        $scope.notaDeDocumento.Detalles = angular.copy($scope.verDocumento.Detalles);
        $scope.notaDeDocumento.IdOrdenDeOperacion = $scope.IdOrden;
        $scope.mostrarIngresoDelMonto = false;
        $scope.mostrarIngresoDeDetalle = false;
        $scope.mostrarMontoDeNotaEnIngresoMonto = false;
        $scope.mostrarMontoEnDetalle = false;
        $scope.numeroDecimalesEnMontoDetalle = 2;
        $scope.totalDeNota = 0;
        $scope.igvDeNota = 0;
        $scope.subTotalDeNota = 0;
        $scope.verificarInconsistenciasDeNota();
    }

    $scope.cargarColeccionesSyncParaNota = function (esParaNotaDeDebito) {
        var defered = $q.defer();
        var promise = defered.promise;
        var promiseList = [];
        promiseList.push($scope.obtenerTiposDeNota(esParaNotaDeDebito));
        promiseList.push($scope.obtenerTiposDeComprobantesParaNota(esParaNotaDeDebito));
        return $q.all(promiseList).then(function (response) {
            defered.resolve();
        }).catch(function (error) {
            defered.reject(e);
        });
        return promise;
    }

    $scope.obtenerTiposDeNota = function (esParaNotaDeDebito) {
        var defered = $q.defer();
        var promise = defered.promise;
        maestroService.obtenerTiposDeNota({ esParaNotaDeDebito }).success(function (data) {
            $scope.tiposDeNotas = data;
            defered.resolve();
        }).error(function (data) {
            SweetAlert.error2(data);
            defered.reject(data);
        });
        return promise;
    }
    //Metodo diferente en venta ya que en venta se enviara la serie para que pueda traer los comprobantes de acuerdo a la primera letra
    $scope.obtenerTiposDeComprobantesParaNota = function (esParaNotaDeDebito) {
        var defered = $q.defer();
        var promise = defered.promise;

        compraService.obtenerTiposDeComprobanteParaNotaDeDebitoCreditoDeCompra({ esParaNotaDeDebito }).success(function (data) {
            $scope.tiposDeComprobantesMasSeriesDeNotas = data;
            defered.resolve();
        }).error(function (data) {
            SweetAlert.error2(data);
            defered.reject(data);
        });
        return promise;
    }

    $scope.cargarSeriesParaNota = function (tipoComprobante) {
        if (tipoComprobante != null) {
            $scope.seriesParaNota = angular.copy(tipoComprobante.Series);
        }
    }

    $scope.accionNotaDeOperacion = function (bandera) {
        if (bandera) {
            $scope.notaDeOperacionAcordion = { open: true };
        }
        else {
            $scope.notaDeOperacionAcordion = { open: false };
        }
    }

    $scope.accionDetalleDocumento = function (bandera) {
        if (bandera) {
            $scope.detalleDocumentoAcordion = { open: true };
        }
        else {
            $scope.detalleDocumentoAcordion = { open: false };
        }
    }

    $scope.cargarTipoNota = function (tipoNota) {
        $scope.numeroDecimalesEnMontoDetalle = 2;
        $scope.mostrarIngresoDelMonto = false;
        $scope.mostrarIngresoDeDetalle = false;
        $scope.mostrarMontoDeNotaEnIngresoMonto = false;
        $scope.mostrarMontoEnDetalle = false;
        $scope.mostrarTotalDetalleDeNota = false;
        $scope.numeroColumnasDetalleNota = 2;
        $scope.etiquetaDeMonto = "";
        $scope.etiquetaDeDetalleDeNota = "";
        // $scope.idDetalleMaestroAnulacionDeLaOperacion // $scope.idDetalleMaestroDevolucionTotal  
        if (tipoNota.Id === $scope.idDetalleMaestroAnulacionPorErrorEnElRuc) {
            $scope.mostrarIngresoDelMonto = true;
            //El valor en el monto a ingresar viene a ser un valor string 
            $scope.etiquetaDeMonto = "NUEVO COMPROBANTE";
        } else if (tipoNota.Id === $scope.idDetalleMaestroDescuentoGlobal) {
            $scope.mostrarIngresoDelMonto = true;
            $scope.mostrarMontoDeNotaEnIngresoMonto = true;
            $scope.notaDeDocumento.MontoNota = parseFloat(0).toFixed(2);
            $scope.etiquetaDeMonto = "DESCUENTO GLOBAL";
        } else if (tipoNota.Id === $scope.idDetalleMaestroInteresesPorMora) {
            $scope.mostrarIngresoDelMonto = true;
            $scope.mostrarMontoDeNotaEnIngresoMonto = true;
            $scope.notaDeDocumento.MontoNota = parseFloat(0).toFixed(2);
            $scope.etiquetaDeMonto = "INTERES TOTAL";
        } else if (tipoNota.Id === $scope.idDetalleMaestroCorreccionPorErrorEnLaDescripcion) {
            $scope.mostrarIngresoDeDetalle = true;
            //El valor en detalle el monto a ingresar viene a ser un valor string 
            $scope.etiquetaDeDetalleDeNota = "DESCRIPCION";
        } else if (tipoNota.Id === $scope.idDetalleMaestroDescuentoPorItem) {
            $scope.mostrarIngresoDeDetalle = true;
            $scope.mostrarMontoEnDetalle = true;
            $scope.etiquetaDeDetalleDeNota = "DESCUENTO";
        } else if (tipoNota.Id === $scope.idDetalleMaestroDevolucionPorItem) {
            $scope.mostrarIngresoDeDetalle = true;
            $scope.mostrarMontoEnDetalle = true;
            $scope.numeroDecimalesEnMontoDetalle = $scope.numeroDecimalesEnCantidad;
            $scope.mostrarTotalDetalleDeNota = true;
            $scope.numeroColumnasDetalleNota = 3;
            $scope.etiquetaDeDetalleDeNota = "DEVOLUCION";
        } else if (tipoNota.Id === $scope.idDetalleMaestroAumentoEnElValor) {
            $scope.mostrarIngresoDeDetalle = true;
            $scope.mostrarMontoEnDetalle = true;
            $scope.etiquetaDeDetalleDeNota = "AUMENTO DEL VALOR";
        }
        $scope.limpiarDetalleDeNota();
    }

    $scope.guardarNotaDeDocumento = function () {
        compraService.guardarNotaDeDebitoCreditoDeCompra({ registroDeNota: $scope.notaDeDocumento }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.cerrarVerDocumento();
            $('#modal-ver-documento').modal('hide');
            $scope.listarBandeja();
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.limpiarDetalleDeNota = function () {
        for (var i = 0; i < $scope.notaDeDocumento.Detalles.length; i++) {
            if ($scope.notaDeDocumento.TipoNota.Id == $scope.idDetalleMaestroCorreccionPorErrorEnLaDescripcion) {
                $scope.notaDeDocumento.Detalles[i].MontoDetalle = '';
            } else {
                $scope.notaDeDocumento.Detalles[i].MontoDetalle = parseFloat(0).toFixed($scope.numeroDecimalesEnMontoDetalle);
                $scope.notaDeDocumento.Detalles[i].MontoDetalleIgv = parseFloat(0).toFixed(2);
            }
        }
    }

    $scope.calcularTotalDeNota = function () {
        var totalDeNota = 0;
        for (var i = 0; i < $scope.notaDeDocumento.Detalles.length; i++) {
            if ($scope.notaDeDocumento.Detalles[i].MontoDetalle != undefined) {
                if ($scope.notaDeDocumento.TipoNota.Id == $scope.idDetalleMaestroDevolucionPorItem) {
                    $scope.notaDeDocumento.Detalles[i].MontoDetalle = $scope.notaDeDocumento.Detalles[i].MontoDetalle == '' ? 0 : $scope.notaDeDocumento.Detalles[i].MontoDetalle;
                    var totalNotaItem = parseFloat($scope.notaDeDocumento.Detalles[i].Precio) * parseFloat($scope.notaDeDocumento.Detalles[i].MontoDetalle);
                    totalDeNota += totalNotaItem;
                    $scope.notaDeDocumento.Detalles[i].MontoDetalleIgv = (totalNotaItem - (totalNotaItem / (1 + parseFloat($scope.tasaIGVParaNota))));
                } else if ($scope.notaDeDocumento.TipoNota.Id == $scope.idDetalleMaestroCorreccionPorErrorEnLaDescripcion) {
                    if ($scope.notaDeDocumento.Detalles[i].MontoDetalle != '')
                        totalDeNota += parseFloat($scope.notaDeDocumento.Detalles[i].Importe);
                    $scope.notaDeDocumento.Detalles[i].MontoDetalleIgv = $scope.notaDeDocumento.Detalles[i].Igv;
                } else {
                    $scope.notaDeDocumento.Detalles[i].MontoDetalle = $scope.notaDeDocumento.Detalles[i].MontoDetalle == '' ? 0 : $scope.notaDeDocumento.Detalles[i].MontoDetalle;
                    totalDeNota += parseFloat($scope.notaDeDocumento.Detalles[i].MontoDetalle);
                    $scope.notaDeDocumento.Detalles[i].MontoDetalleIgv = (parseFloat($scope.notaDeDocumento.Detalles[i].MontoDetalle) - (parseFloat($scope.notaDeDocumento.Detalles[i].MontoDetalle) / (1 + parseFloat($scope.tasaIGVParaNota))));
                }
            }
        }
        $scope.totalDeNota = totalDeNota;
        $scope.igvDeNota = (totalDeNota - (totalDeNota / (1 + parseFloat($scope.tasaIGVParaNota))));
        $scope.subTotalDeNota = parseFloat($scope.totalDeNota) - parseFloat($scope.igvDeNota);

    }

    $scope.calcularTotalDeNotaMonto = function () {
        if ($scope.notaDeDocumento.TipoNota.Id == $scope.idDetalleMaestroAnulacionDeLaOperacion || $scope.notaDeDocumento.TipoNota.Id == $scope.idDetalleMaestroDevolucionTotal) {
            $scope.totalDeNota = $scope.verDocumento.Total;
            $scope.igvDeNota = $scope.verDocumento.Igv;
            $scope.subTotalDeNota = parseFloat($scope.totalDeNota) - parseFloat($scope.igvDeNota);
        } else if ($scope.notaDeDocumento.TipoNota.Id == $scope.idDetalleMaestroDescuentoGlobal || $scope.notaDeDocumento.TipoNota.Id == $scope.idDetalleMaestroInteresesPorMora) {
            $scope.notaDeDocumento.MontoNota = ($scope.notaDeDocumento.MontoNota == '') ? 0 : $scope.notaDeDocumento.MontoNota;
            $scope.totalDeNota = $scope.notaDeDocumento.MontoNota;
            $scope.igvDeNota = ($scope.totalDeNota - ($scope.totalDeNota / (1 + parseFloat($scope.tasaIGVParaNota))));
            $scope.subTotalDeNota = parseFloat($scope.totalDeNota) - parseFloat($scope.igvDeNota);
            if (parseFloat($scope.notaDeDocumento.MontoNota) > parseFloat($scope.verDocumento.Total)) {
                $scope.inconsistenciasDeNota.push("Es necesario que el monto de nota sea menor al total.");
            }
        } else {
            $scope.totalDeNota = 0;
            $scope.igvDeNota = 0;
            $scope.subTotalDeNota = 0;
        }
    }

    $scope.cancelar = function () {
        $scope.hayRegistroDeNota = false;
    }

    $scope.cerrarVerDocumento = function () {
        $scope.hayRegistroDeNota = false;
    }

    $scope.verificarInconsistenciasDeNota = function () {
        var banderaDeDetalle = true;
        var cantidadMayor = false;
        var montoMayor = false;
        $scope.inconsistenciasDeNota = [];
        if ($scope.notaDeDocumento.TipoNota === undefined) {
            $scope.inconsistenciasDeNota.push("Es necesario seleccionar el tipo de nota.");
        } else {
            if ($scope.notaDeDocumento.TipoNota.Id === $scope.idDetalleMaestroAnulacionPorErrorEnElRuc) {
                if ($scope.notaDeDocumento.MontoNota === "" || $scope.notaDeDocumento.MontoNota === null) {
                    $scope.inconsistenciasDeNota.push("Es necesario ingresar al valor de la nota.");
                }
            } else if ($scope.notaDeDocumento.TipoNota.Id === $scope.idDetalleMaestroDescuentoGlobal) {
                if ($scope.notaDeDocumento.MontoNota === "" || $scope.notaDeDocumento.MontoNota === null) {
                    $scope.inconsistenciasDeNota.push("Es necesario ingresar al valor de descuento de la nota.");
                }
            } else if ($scope.notaDeDocumento.TipoNota.Id === $scope.idDetalleMaestroInteresesPorMora) {
                if ($scope.notaDeDocumento.MontoNota === "" || $scope.notaDeDocumento.MontoNota === null) {
                    $scope.inconsistenciasDeNota.push("Es necesario ingresar al valor de interes de la nota.");
                }
            } else if ($scope.notaDeDocumento.TipoNota.Id === $scope.idDetalleMaestroDescuentoPorItem || $scope.notaDeDocumento.TipoNota.Id === $scope.idDetalleMaestroDevolucionPorItem || $scope.notaDeDocumento.TipoNota.Id === $scope.idDetalleMaestroAumentoEnElValor) {
                for (var i = 0; i < $scope.notaDeDocumento.Detalles.length; i++) {
                    banderaDeDetalle = banderaDeDetalle && ($scope.notaDeDocumento.Detalles[i].MontoDetalle == 0);
                    cantidadMayor = cantidadMayor || (parseFloat($scope.notaDeDocumento.Detalles[i].MontoDetalle) > parseFloat($scope.notaDeDocumento.Detalles[i].Cantidad));
                    montoMayor = montoMayor || (parseFloat($scope.notaDeDocumento.Detalles[i].MontoDetalle) > parseFloat($scope.notaDeDocumento.Detalles[i].Importe));
                }
                if (banderaDeDetalle)
                    $scope.inconsistenciasDeNota.push("Es necesario ingresar un valor de detalle de la nota.");
                if ($scope.notaDeDocumento.TipoNota.Id === $scope.idDetalleMaestroDevolucionPorItem && cantidadMayor)
                    $scope.inconsistenciasDeNota.push("Es necesario que la cantidad a devolver sea menor a la cantidad del documento.");
                if (montoMayor)
                    $scope.inconsistenciasDeNota.push("Es necesario que el monto sea menor a la importe.");
            } else if ($scope.notaDeDocumento.TipoNota.Id === $scope.idDetalleMaestroCorreccionPorErrorEnLaDescripcion) {
                banderaDeDetalle = true;
                for (var i = 0; i < $scope.notaDeDocumento.Detalles.length; i++) {
                    banderaDeDetalle = banderaDeDetalle && ($scope.notaDeDocumento.Detalles[i].MontoDetalle == '');
                }
                if (banderaDeDetalle)
                    $scope.inconsistenciasDeNota.push("Es necesario ingresar un valor de detalle de la nota.");
            }
            if ($scope.notaDeDocumento.TipoNota.Id == $scope.idDetalleMaestroDescuentoGlobal || $scope.notaDeDocumento.TipoNota.Id == $scope.idDetalleMaestroInteresesPorMora) {
                if (parseFloat($scope.notaDeDocumento.MontoNota) > parseFloat($scope.verDocumento.Total)) {
                    $scope.inconsistenciasDeNota.push("Es necesario que el monto de nota sea menor al total.");
                }
            }
        } if ($scope.notaDeDocumento.Observacion === "" || $scope.notaDeDocumento.Observacion === null || $scope.notaDeDocumento.Observacion === undefined) {
            $scope.inconsistenciasDeNota.push("Es necesario ingresar el motivo de la nota.");
        } if ($scope.notaDeDocumento.Comprobante === undefined) {
            $scope.inconsistenciasDeNota.push("Es necesario seleccionar un documento.");
        } else {
            if ($scope.notaDeDocumento.Comprobante.EsPropio === false) {
                if ($scope.notaDeDocumento.Comprobante.SerieIngresada === "" || $scope.notaDeDocumento.Comprobante.SerieIngresada === null) {
                    $scope.inconsistenciasDeNota.push("Es necesario ingresar la serie de comprobante.");
                } if ($scope.notaDeDocumento.Comprobante.NumeroIngresado === "" || $scope.notaDeDocumento.Comprobante.NumeroIngresado === null) {
                    $scope.inconsistenciasDeNota.push("Es necesario ingresar el numero de comprobante.");
                }
            }
        }
    }

    $scope.actualizarConceptosIngresados = function (concepto) {
        $scope.selectorConceptoComercialAPI.AgregarFamiliaIngresada(concepto);
    }


    $scope.actualizarProductosIngresados = function (idProducto, nombreProducto, esBien) {
        $scope.selectorConceptoComercialAPI.AgregarConceptoIngresado(idProducto, nombreProducto, esBien);
    }
    //#endregion

   


    //METODOS PARA EL MANEJO DE "VER" DOCUMENTOS DE IMPRESION
    $scope.inicializarVerDeDocumento = function (item) {
        $scope.verDocumento = {};
        $scope.serieDocumentoSeleccionado = item.Numero;
        $scope.hayRegistroDeNota = false;
        $scope.EsOrdenDeCompra = item.EsOrdenDeCompra;
        $scope.IdOrden = item.Id;
        $scope.numeroDeGuiasDeRemision = [];
        //$scope.obtenerFormatosDeImpresion().then(function (resultado_) {
        //    $scope.formato = $scope.formatosDeImpresion[0];
        $scope.obtenerDocumento(item.Id);

    }


    //   $scope.obtenerFormatosDeImpresion = function () {
    //    var defered = $q.defer();
    //    var promise = defered.promise;
    //    ventaService.obtenerFormatosDeImpresion({}).success(function (data) {
    //        $scope.formatosDeImpresion = data;
    //        defered.resolve();
    //    }).error(function (data) {
    //        SweetAlert.error2(data);
    //        defered.reject(data);
    //    });
    //    return promise;
    //}

    $scope.obtenerDocumento = function (id) {
        compraService.obtenerDocumentoDeCompra({ idOrdenDeCompra: id }).success(function (data) {
            $scope.verDocumento = data;
            $scope.tamanioComprobante = $scope.verDocumento.Formato;

            document.getElementById("pdfDocumento").innerHTML = $scope.verDocumento.CadenaHtmlDeComprobante80;
            $timeout(function () { $('#pdfDocumento').trigger("change"); }, 100);
            document.getElementById("pdfDocumento1").innerHTML = $scope.verDocumento.CadenaHtmlDeComprobante80;
            $timeout(function () { $('#pdfDocumento1').trigger("change"); }, 100);

            if ($scope.verDocumento.TieneGuiaDeRemision) {
                var stringHtml = "";
                for (var i = 0; i < $scope.verDocumento.CadenasHtmlDeGuiaDeRemision.length; i++) {
                    stringHtml = stringHtml + ' <div class="col-md-12"> ' +
                        ' <div id = "pdfGuia' + i + '" style = "width:' + $scope.tamanioComprobante + '; border:solid; border-width: thin; font-family: auto; margin:auto; margin-top:15px" > ' +
                        $scope.verDocumento.CadenasHtmlDeGuiaDeRemision[i] +
                        ' </div > ' +
                        ' </div > ';
                }
                document.getElementById("pdfGuia").innerHTML = stringHtml;
                $timeout(function () { $('#pdfGuia').trigger("change"); }, 100);
            }
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    //MANEJO DE CARACTERISTICAS PROPIAS

    $scope.nuevoRegistroCaracteristicasPropias = function (indexDetalle) {
        $scope.valores = [];
        $scope.limpiarValoresDeCaracteristicas();
        $scope.indexDetalle = indexDetalle;
    }

    $scope.agregarItemValorCaracteristicaPropia = function (indexValor) {
        if (!($scope.verificarValoresIngresadosDeCaracteristicas())) {
            var itemValorCaracteristica = {};
            itemValorCaracteristica.Valores = angular.copy($scope.valores);
            itemValorCaracteristica.Numero = $scope.compra.Detalles[$scope.indexDetalle].Producto.ConceptoBasico.CaracteristicasPropias.length + 1;
            $scope.compra.Detalles[$scope.indexDetalle].Producto.ConceptoBasico.CaracteristicasPropias.push(itemValorCaracteristica);
            $scope.limpiarValoresDeCaracteristicas();
            $scope.focusSelectNext('valor-' + $scope.indexDetalle + '-' + '0');
        } else {
            if ($scope.valores[indexValor].Valor !== "") {
                $scope.focusSelectNext('valor-' + $scope.indexDetalle + '-' + (indexValor + 1));
            }
        }
    }

    $scope.quitarItemValorCaracteristicaPropia = function (indexValorCaracteristica) {
        $scope.compra.Detalles[$scope.indexDetalle].Producto.ConceptoBasico.CaracteristicasPropias.splice(indexValorCaracteristica, 1);
    }

    $scope.limpiarValoresDeCaracteristicas = function () {
        for (var i = 0; i < $scope.valores.length; i++) {
            $scope.valores[i].Valor = "";
        }
    }

    $scope.verificarValoresIngresadosDeCaracteristicas = function () {
        //Verificar si existen valores de caracteristicas vacios
        for (var i = 0; i < $scope.valores.length; i++) {
            if (!($scope.valores[i].Valor) || $scope.valores[i].Valor === "") {
                return true;
            }
        }
        return false;
    }

    $scope.focusSelectNext = function (id) {
        $timeout(function () {
            $('#' + id).trigger("focus");
        }, 100);
        $timeout(function () {
            $('#' + id).trigger("select");
        }, 100);
    }

    $scope.guardarCaracteristicasPropias = function () {
        if ($scope.compra.Detalles[$scope.indexDetalle].Producto.ConceptoBasico.CaracteristicasPropias.length > $scope.compra.Detalles[$scope.indexDetalle].Cantidad) {
            $scope.compra.Detalles[$scope.indexDetalle].Cantidad = $scope.compra.Detalles[$scope.indexDetalle].Producto.ConceptoBasico.CaracteristicasPropias.length;
            $scope.calcularImporteDesdeCantidad($scope.compra.Detalles, $scope.indexDetalle);
        }
    }

    //#region ANULACION INVALIDACION

    $scope.invalidar = function () {
        compraService.invalidarCompra({ idOrden: $scope.IdOrden, observacion: $scope.invalidacion.observacion }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $('#modal-invalidar-documento').modal('hide');
            if ($scope.esCompraCorporativa) {
                $('#modal-ver-compra-corporativa').modal('hide');
                $scope.listarBandejaComprasCorporativas();
            } else {
                $('#modal-ver-documento').modal('hide');
                $scope.listarBandeja();
            }
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.invalidarAnulacionDeCompra = function () {
        compraService.invalidarAnulacionDeCompra({ idOrden: $scope.IdOrden, observacion: $scope.invalidacion.observacion }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $('#modal-invalidar-documento').modal('hide');
            $('#modal-ver-documento').modal('hide');
            $scope.listarBandeja();
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.anular = function () {
        compraService.anularCompra({ anulacion: $scope.anulacion }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $('#modal-anulacion-nota-credito').modal('hide');
            $scope.listarBandeja();
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.cargarDatosParaAnulacionCompra = function () {
        $scope.anulacion = {};
        $scope.obtenerOrdenDeCompraParaAnulacion($scope.verCompra.Id);
        $scope.obtenerTiposDeComprobanteParaAnulacionCompra();
    }

    $scope.cargarDatosParaAnulacionCompraCorporativa = function () {
        $scope.anulacion = {};
        $scope.obtenerOrdenDeCompraParaAnulacion($scope.verCompraCorporativa.IdOrden);
        $scope.obtenerTiposDeComprobanteParaAnulacionCompra();
    }

    $scope.obtenerTiposDeComprobanteParaAnulacionCompra = function () {
        compraService.obtenerTiposDeComprobanteParaAnulacionCompra({}).success(function (data) {
            $scope.tiposDeComprobantesMasSeriesAnulacionCompra = data;
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.cargarSeriesParaAnulacionCompra = function (tipoComprobante) {
        if (tipoComprobante != null) {
            $scope.seriesAnulacionCompra = angular.copy(tipoComprobante.Series);
        }
    }

    $scope.obtenerOrdenDeCompraParaAnulacion = function (idOrdenCompra) {
        compraService.obtenerOrdenCompraYDetallesOrden({ idOrdenCompra: idOrdenCompra }).success(function (data) {
            $scope.anulacion = data;
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.limpiarObservacion = function () {
        $scope.invalidacion = {};
    }

    //#endregion
});
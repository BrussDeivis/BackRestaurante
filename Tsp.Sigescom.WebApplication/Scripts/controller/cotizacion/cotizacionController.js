app.controller('cotizacionController', function ($scope, $q, $timeout, $location, $rootScope, SweetAlert, $filter, DTOptionsBuilder, DTColumnDefBuilder, cotizacionService, ventaService, maestroService, clienteService, productoService, centroDeAtencionService, empleadoService, conceptoService) {

    //#region BANDEJA DE COTIZACIONES 

    $scope.inicializar = function () {
        $scope.inicializacionRealizada = false;

        $scope.limpiarRegistro();
        $scope.cargarParametros();
        $scope.cargarColeccionesAsync();
        $scope.inicializarComponentes();
        $scope.listarBandejaCotizacion();
    }
    $scope.inicializarComponentes = function () {
        //$scope.rolProveedor = { Id: idRolProveedor, Nombre: 'PROVEEDOR' };
        $scope.inicializacionRealizada = true;
    }
    $scope.cargarParametros = function () {
        $scope.URL = URL_ + "Venta/PregeneracionVenta?idOrden=";
        $scope.fechaInicio = fechaInicio;
        $scope.fechaFin = fechaFin;
        $scope.idMedioDePagoEfectivo = idMedioDePagoEfectivo;
        $scope.aplicaLeyAmazonia = aplicaLeyAmazonia;
        $scope.idClienteGenerico = idClienteGenerico;
        $scope.ventasSujetasADisponibilidadStock = ventasSujetasADisponibilidadStock;
        $scope.idTipoDocumentoCuandoClienteEsGenerico = idTipoDocumentoCuandoClienteEsGenerico;
        $scope.idTipoDocumentoPorDefectoParaVenta = idTipoDocumentoPorDefectoParaVenta;
        $scope.idTipoActorPersonaJuridica = idTipoActorPersonaJuridica;
        $scope.mostrarAliasDeClienteGenerico = mostrarAliasDeClienteGenerico;
        $scope.precioUnitarioCalculadoVenta = precioUnitarioCalculadoVenta;
        $scope.tasaIGV = tasaIGV;
        $scope.mostrarDetalleUnificado = mostrarDetalleUnificado;
        $scope.checketDetalleUnificado = checketDetalleUnificado;
        $scope.valorDetalleUnificado = valorDetalleUnificado;
        $scope.aplicarCantidadPorDefectoEnVentas = aplicarCantidadPorDefectoEnVentas;
        $scope.cantidadPorDefectoEnVentas = cantidadPorDefectoEnVentas;
        $scope.idTipoPersonaSeleccionadaPorDefecto = idTipoPersonaSeleccionadaPorDefecto;
        $scope.idTipoDocumentoSeleccionadaConTipoPersonaNatural = idTipoDocumentoSeleccionadaConTipoPersonaNatural;
        $scope.idTipoDocumentoSeleccionadaConTipoPersonaJuridica = idTipoDocumentoSeleccionadaConTipoPersonaJuridica;
        $scope.precioUnitarioIngresadoVenta = precioUnitarioIngresadoVenta;
        $scope.idTipoDocumentoIdentidadDni = idTipoDocumentoIdentidadDni;
        $scope.idTipoDocumentoIdentidadRuc = idTipoDocumentoIdentidadRuc;
        $scope.idDetalleMaestroCatalogoDocumentoBoleta = idDetalleMaestroCatalogoDocumentoBoleta;
        $scope.idDetalleMaestroCatalogoDocumentoFactura = idDetalleMaestroCatalogoDocumentoFactura;
        $scope.montoMaximoAVenderCuandoClienteNoEstaIdenticicado = montoMaximoAVenderCuandoClienteNoEstaIdenticicado;
        $scope.permitirVentaAlCredito = permitirVentaAlCredito;
        $scope.permitirVentaConFechaPasada = permitirVentaConFechaPasada;
        $scope.fechaActual = fechaActual;
        $scope.permitirRegistroFlete = permitirRegistroFlete;
        $scope.modoIngresoCodigoBarra = modoIngresoCodigoBarra;
        $scope.cursorPorDefectoCodigoBarra = cursorPorDefectoCodigoBarra;
        $scope.venderConBoleta = false;
        $scope.permitirRegistroDeGuiasDeRemision = permitirRegistroDeGuiasDeRemision;
        $scope.costoUnitarioPorBolsaDePlastico = costoUnitarioPorBolsaDePlastico;
        $scope.permitirRegistroNumeroBolsaDePlastico = permitirRegistroNumeroBolsaDePlastico;
        $scope.permitirRegistroDeLoteEnDetalleDeVenta = permitirRegistroDeLoteEnDetalleDeVenta;
        $scope.idConceptoBasicoBolsaPlastica = idConceptoBasicoBolsaPlastica;
        $scope.idTarifaSeleccionadoPorDefecto = idTarifaSeleccionadoPorDefecto;
        $scope.permitirIngresarCantidad = permitirIngresarCantidad;
        $scope.permitirIngresarPrecioUnitario = permitirIngresarPrecioUnitario;
        $scope.permitirIngresarImporte = permitirIngresarImporte;
        $scope.ingresarCantidadCalcularPrecioUnitario = ingresarCantidadCalcularPrecioUnitario;
        $scope.ingresarPrecioUnitarioCalcularImporte = ingresarPrecioUnitarioCalcularImporte;
        $scope.ingresarImporteCalcularCantidad = ingresarImporteCalcularCantidad;
        $scope.flujoDespuesDeCodigoBarraEnVenta = flujoDespuesDeCodigoBarraEnVenta;
        $scope.mascaraDeCalculoPorDefecto = mascaraDeCalculoPorDefecto;
        $scope.mascaraDeCalculoPrecioUnitarioCalculado = mascaraDeCalculoPrecioUnitarioCalculado;
        $scope.esPregeneracionVentaDesdeCotizacion = esPregeneracionVentaDesdeCotizacion;
        $scope.idOrdenCotizacionAPregenerar = idOrdenCotizacionAPregenerar;
        $scope.permitirRegistroConceptoServicio = permitirRegistroConceptoServicio;
        $scope.numeroDecimalesEnCantidad = numeroDecimalesEnCantidad;
        $scope.numeroDecimalesEnPrecio = numeroDecimalesEnPrecio;
        $scope.mostrarBuscadorCodigoBarra = mostrarBuscadorCodigoBarra;
        $scope.modoSeleccionConcepto = modoSeleccionConcepto;
        $scope.modoSeleccionTipoFamilia = modoSeleccionTipoFamilia;
        $scope.minimoCaracteresBuscarConcepto = minimoCaracteresBuscarConcepto;
        $scope.rolCliente = { Id: idRolCliente, Nombre: 'CLIENTE' };
        $scope.tiempoEsperaBusquedaSelector = tiempoEsperaBusquedaSelector;
        $scope.minimoCaracteresBuscarActorComercial = minimoCaracteresBuscarActorComercial;
        $scope.mascaraDeVisualizacionValidacionRegistroCliente = mascaraDeVisualizacionValidacionRegistroCliente;
        $scope.informacionSelectorConcepto = informacionSelectorConcepto;
        $scope.permitirSeleccionarGrupoCliente = permitirSeleccionarGrupoCliente;
        $scope.permitirConvertirCotizacionAVenta = permitirConvertirCotizacionAVenta;
        $scope.permitirConvertirCotizacionAPedido = permitirConvertirCotizacionAPedido;
    }

    $scope.cargarColeccionesAsync = function () {
        $scope.productos = [];
        //$scope.obtenerClientes();
        $scope.obtenerTiposDeComprobante();
    }

    $scope.cambioCliente = function (actorComercial) {
        $scope.cotizacion.Cliente = actorComercial;
        $scope.verificarInconsistencias();
    };
    //$scope.obtenerClientes = function () {
    //    var defered = $q.defer();
    //    var promise = defered.promise;
    //    ventaService.obtenerClientes({}).success(function (data) {
    //        $scope.clientes = data;
    //        defered.resolve();
    //    }).error(function (data) {
    //        SweetAlert.error2(data);
    //        defered.reject(data);
    //    });
    //    return promise;
    //}

    $scope.obtenerTiposDeComprobante = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        cotizacionService.obtenerTiposDeComprobanteConSeriesAutonumericasParaCotizacion({}).success(function (data) {
            $scope.selectorTiposDeComprobantesMasSeries = data;
            defered.resolve();
        }).error(function (data) {
            SweetAlert.error2(data);
            defered.reject(data);
        });
        return promise;
    }

    $scope.listarBandejaCotizacion = function () {
        cotizacionService.obtenerCotizaciones({ desde: $scope.fechaInicio, hasta: $scope.fechaFin }).success(function (data) {
            $scope.cotizaciones = data;
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema", data.error);
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
        '8': { type: 'text', className: 'form-control padding-left-right-3' },
    });
    //#endregion

    //#region Ver Cotizacion 
    $scope.verDocumentoDeCotizacion = function (item) {
        $scope.verCotizacion = {};
        $scope.obtenerFormatosDeImpresion().then(function (resultado_) {
            $scope.formato = $scope.formatosDeImpresion[0];
            $scope.obtenerCotizacion(item.IdOrden);
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
    }

    $scope.obtenerCotizacion = function (id) {
        cotizacionService.obtenerDocumentoDeCotizacion({ idOrdenDeCotizacion: id }).success(function (data) {
            $scope.verCotizacion = data;
            $scope.tamanioComprobante = $scope.formato.Id == 1 ? '80mm' : "210mm";
            document.getElementById("pdfDocumento").innerHTML = $scope.verCotizacion.CadenaHtmlDeDocumento;
            $timeout(function () { $('#pdfDocumento').trigger("change"); }, 100);
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
            SweetAlert.error2(data);
            defered.reject(data);
        });
        return promise;
    }

    $scope.cargarDocumentoDeCotizacion = function () {
        cotizacionService.obtenerHtmlDocumentoDeCotizacion({ idOrden: $scope.verCotizacion.IdOrden, formato: $scope.formato.Id }).success(function (data) {
            $scope.tamanioComprobante = $scope.formato.Id == 1 ? '80mm' : "210mm";
            $scope.verCotizacion.CadenaHtmlDeDocumento = data;
            document.getElementById("pdfDocumento").innerHTML = data;
            $timeout(function () { $('#pdfDocumento').trigger("change"); }, 100);
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema", data.error);
        });
    }

    $scope.cerrarVerCotizacion = function () {
        $scope.verCotizacion = {};
        $scope.formato = {};
        document.getElementById("pdfDocumento").innerHTML = '';
        $timeout(function () { $('#pdfDocumento').trigger("change"); }, 100);
    }
    //#endregion

    //#region Envio de cotizaciones por correo
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
        cotizacionService.enviarCorreoElectronicoConDocumentoDeCotizacion({ idOrden: $scope.verCotizacion.IdOrden, formato: $scope.formato.Id, correosElectronicos: $scope.correosElectronicos }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema", data.error);
        });
    }

    $scope.limpiarEnvioDeCorrero = function () {
        $scope.correosElectronicos = {};
    }
    //#endregion

    //#region Impresion de documento de cotizacion
    $scope.imprimirDocumentoDeCotizacion = function (id) {

        var ficha = document.getElementById(id);
        var ventanaImpresion = window.open(' ', 'popimpr');
        ventanaImpresion.document.write(ficha.innerHTML);
        ventanaImpresion.document.close();
        ventanaImpresion.print();
        ventanaImpresion.close();
    }
    //#endregion

    //#region Nueva cotizacion
    $scope.inicializarNuevaCotizacion = function () {
        $scope.limpiarRegistro();
        $scope.establecerDatosPorDefecto();
    }

    $scope.limpiarRegistro = function () {
        $scope.accionModal = 'REGISTRAR';
        $scope.cotizacion = {
            Detalles: [], GrabaIgv: false, Total: 0, Flete: 0, Observacion: 'NINGUNA'
        };
        $scope.detalle = {};
        $scope.serieSeleccionaesEsAutonumerica = false;
    }

    $scope.establecerDatosPorDefecto = function () {
        $scope.selectorClienteAPI.EstablecerActorPorDefecto();
        $scope.cotizacion.TipoDeComprobante = $scope.selectorTiposDeComprobantesMasSeries[0];
        $scope.cotizacion.GrabaIgv = $scope.aplicaLeyAmazonia ? false : true;
        $("#fechaVencimiento").datepicker('setDate', undefined);
        $scope.focusNext('idCodigoBarraConcepto');
    }

    //$scope.seleccionarClienteGenerico = function () {
    //    for (var i = 0; i < $scope.clientes.length; i++) {
    //        if ($scope.clientes[i].Id == $scope.idClienteGenerico) {
    //            $scope.cotizacion.Cliente = $scope.clientes[i];
    //            break;
    //        }
    //    }
    //    $timeout(function () { $('#selector-cliente').trigger("change"); }, 100);
    //}

    $scope.cargarSeries = function (tipoComprobante) {
        if (tipoComprobante != null) {
            $scope.series = angular.copy(tipoComprobante.Series);
        }
    }


    //#endregion

    //#region Edicion de cotizacion
    $scope.inicializarEdicionCotizacion = function (item) {
        $scope.limpiarRegistro();
        $scope.establecerDatosPorDefecto();
        $scope.establecerDatosDeCotizacion(item);
    }

    $scope.establecerDatosDeCotizacion = function (item) {
        $scope.accionModal = 'EDITAR';
        cotizacionService.obtenerCotizacionParaEditar({ idOrden: item.IdOrden }).success(function (data) {
            var cotizacionAEditar = data;
            $scope.obtenerDetallesAEditar(cotizacionAEditar);
            $scope.cotizacion.Id = cotizacionAEditar.Id;
            $scope.cotizacion.IdOrden = cotizacionAEditar.IdOrden;
            $scope.cotizacion.IdEstado = cotizacionAEditar.IdEstado;
            $scope.cotizacion.IdComprobante = cotizacionAEditar.IdComprobante;
            $scope.selectorClienteAPI.SetActorComercialPorId(cotizacionAEditar.Cliente.Id);
            $scope.cotizacion.Alias = cotizacionAEditar.Alias;
            $scope.cotizacion.Observacion = cotizacionAEditar.Observacion;
            $scope.cotizacion.GrabaIgv = cotizacionAEditar.GrabaIgv;
            if (!$scope.aplicaLeyAmazonia) {
                $scope.cotizacion.NoGrabaIgv = !cotizacionAEditar.GrabaIgv;
            }
            $scope.cambioDeGrabarIgv($scope.cotizacion.Detalles);
            let date = cotizacionAEditar.FechaVencimiento.slice(cotizacionAEditar.FechaVencimiento.indexOf("(") + 1, cotizacionAEditar.FechaVencimiento.indexOf(")"));
            $scope.cotizacion.FechaVencimiento = $scope.formatDate(new Date(+date), "ES");
            $("#fechaVencimiento").datepicker('setDate', $scope.cotizacion.FechaVencimiento);
            $timeout(function () { $('#selector-cliente').trigger("change"); }, 100);
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.obtenerDetallesAEditar = function (cotizacionAEditar) {
        for (var i = cotizacionAEditar.Detalles.length - 1; i >= 0; i--) {
            $scope.detalle.Producto = cotizacionAEditar.Detalles[i].ConceptoCompleto;
            $scope.agregarDetalle();
        }
        for (var i = 0; i < $scope.cotizacion.Detalles.length; i++) {
            $scope.cotizacion.Detalles[i].IdDetalle = cotizacionAEditar.Detalles[i].IdDetalle;
            $scope.cotizacion.Detalles[i].PrecioUnitario = cotizacionAEditar.Detalles[i].PrecioUnitario;
            $scope.cotizacion.Detalles[i].Cantidad = cotizacionAEditar.Detalles[i].Cantidad;
            $scope.cotizacion.Detalles[i].Importe = cotizacionAEditar.Detalles[i].Importe.toFixed(2);
            $scope.cotizacion.Detalles[i].Igv = cotizacionAEditar.Detalles[i].Igv.toFixed(2);
        }
        $scope.calcularTotal($scope.cotizacion.Detalles);
    }
    //#endregion

    //#region Acciones 
    //$scope.actualizarClientesIngresadosYEditados = function (actualizacionCliente, id, numeroDocumento, razonSocial) {
    //    $scope.nuevoCliente = { Id: id, RazonSocial: razonSocial, NumeroDocumentoIdentidad: numeroDocumento };
    //    if (!actualizacionCliente) {
    //        $scope.clientes.push($scope.nuevoCliente);
    //        $scope.cotizacion.Cliente = $scope.nuevoCliente;
    //    } else {
    //        $scope.clientes[document.getElementById("selector-cliente").selectedIndex] = $scope.nuevoCliente;
    //    }
    //}

    $scope.focusNext = function (id) {
        $timeout(function () {
            $('#' + id).trigger("focus");
        }, 100);
    }

    $scope.focusSelectNext = function (id) {
        $timeout(function () {
            $('#' + id).trigger("focus");
        }, 100);
        $timeout(function () {
            $('#' + id).trigger("select");
        }, 100);
    }

    //#endregion

    //#region Calculos y Guardado

    $scope.seleccionConcepto = function (conceptoComercial) {
        $scope.detalle.Producto = angular.copy(conceptoComercial);
        $scope.agregarDetalle();
    }

    $scope.agregarDetalle = function () {
        var validar = false;
        var index = 0;
        if ($scope.cotizacion.Detalles.length > 0) {
            for (var i = 0; i < $scope.cotizacion.Detalles.length; i++) {
                if ($scope.detalle.Producto.Id == $scope.cotizacion.Detalles[i].Producto.Id) {
                    validar = true;
                    index = i;
                    break;
                }
            }
        }
        if (validar) {
            $scope.agregarCantidad($scope.cotizacion.Detalles, index);
            $scope.detalle = {};
        }
        else {
            if ($scope.detalle.Producto.Precios == undefined || $scope.detalle.Producto.Precios.length == 0) {
                SweetAlert.warning("Advertencia", "El concepto " + $scope.detalle.Producto.NombreParaDetalle + " no tiene precios establecidos.");
                return;
            }
            let precio = Enumerable.from($scope.detalle.Producto.Precios).where("$.IdTarifa == '" + $scope.idTarifaSeleccionadoPorDefecto + "'").toArray()[0];
            if (precio === undefined) {
                SweetAlert.warning("Advertencia", "El concepto " + $scope.detalle.Producto.NombreParaDetalle + " no tiene establecido el precio en la tarifa por defecto. Se seleccionara la tarifa siguiente.");
                precio = $scope.detalle.Producto.Precios[0];
            }
            $('#tarifa-0').select2('data', $scope.detalle.Producto.Precios);
            $scope.detalle.PrecioTarifa = precio;
            $scope.detalle.PrecioUnitario = parseFloat(precio.Valor).toFixed($scope.numeroDecimalesEnPrecio);
            $scope.detalle.Cantidad = 1;
            $scope.detalle.Importe = parseFloat($scope.detalle.PrecioUnitario * $scope.detalle.Cantidad).toFixed(2);
            $scope.detalle.MascaraDeCalculo = '110';
            $scope.detalle.Igv = $scope.cotizacion.GrabaIgv ? (parseFloat($scope.detalle.Importe - ($scope.detalle.Importe / (1 + $scope.tasaIGV))).toFixed(2)) : 0;
            $scope.cotizacion.Detalles.unshift($scope.detalle);
            $timeout(function () { $('#tarifa-0').trigger("change"); }, 100);
            $scope.copydetalle = angular.copy($scope.detalle);
            $scope.detalle = {};
            $(function () {
                $('select:not(.normal)').each(function () {
                    $(this).select2({
                        dropdownParent: $(this).parent()
                    });
                });
            });
        }
        $scope.calcularTotal($scope.cotizacion.Detalles);
        $scope.verificarInconsistencias();
    }

    $scope.formatoDecimalCantidad = function (event, $filter) {
        let valor = event.target.value;
        event.target.value = parseFloat(valor).toFixed($scope.numeroDecimalesEnCantidad);
    }

    $scope.agregarCantidad = function (detalles, index) {
        $scope.detalle.Cantidad = parseFloat(detalles[index].Cantidad++).toFixed($scope.numeroDecimalesEnCantidad);
        detalles[index].Importe = parseFloat(detalles[index].Cantidad * detalles[index].PrecioUnitario).toFixed(2);
        $scope.calcularTotal(detalles);
    }

    $scope.quitarDetalle = function (index) {
        $scope.cotizacion.Detalles.splice(index, 1);
        $scope.calcularTotal($scope.cotizacion.Detalles);
    }

    $scope.calcularValoresIngresadoImporte = function (detalles, index) {
        detalles[index].MascaraDeCalculo = '101';
        detalles[index].PrecioUnitario = parseFloat(detalles[index].Importe / detalles[index].Cantidad).toFixed($scope.numeroDecimalesEnPrecio);
        if ($scope.cotizacion.GrabaIgv || !$scope.aplicaLeyAmazonia) {
            detalles[index].Igv = parseFloat(detalles[index].Importe - (detalles[index].Importe / (1 + $scope.tasaIGV))).toFixed(2);
        } else {
            detalles[index].Igv = 0;
        }
        $scope.calcularTotal(detalles);
    }

    $scope.calcularImporteIngresandoPrecio = function (detalles, index, ingresaPrecio) {
        if (ingresaPrecio) {
            //detalles[index].PrecioUnitario = parseFloat(detalles[index].PrecioUnitario);
        } else {
            for (var i = 0; i < detalles[index].Producto.Precios.length; i++) {
                if (detalles[index].Producto.Precios[i].Id == detalles[index].IdPrecioUnitario) {
                    detalles[index].PrecioUnitario = parseFloat(detalles[index].Producto.Precios[i].Valor).toFixed($scope.numeroDecimalesEnPrecio);
                    break;
                }
            }
        }
        detalles[index].MascaraDeCalculo = '110';
        detalles[index].Importe = parseFloat(detalles[index].Cantidad * detalles[index].PrecioUnitario).toFixed(2);
        detalles[index].Igv = $scope.cotizacion.GrabaIgv ? parseFloat(detalles[index].Importe - (detalles[index].Importe / (1 + $scope.tasaIGV))).toFixed(2) : 0;
        $scope.calcularTotal(detalles);
    }

    $scope.calcularImporteIngresandoCantidad = function (detalles, index) {
        detalles[index].MascaraDeCalculo = '110';
        detalles[index].Importe = parseFloat(detalles[index].Cantidad * detalles[index].PrecioUnitario).toFixed(2);
        detalles[index].Igv = $scope.cotizacion.GrabaIgv ? parseFloat(detalles[index].Importe - (detalles[index].Importe / (1 + $scope.tasaIGV))).toFixed(2) : 0;
        $scope.calcularTotal(detalles);
    }

    $scope.calcularTotal = function (detalles) {
        if ($scope.cotizacion.Flete > 0) {
            $scope.cotizacion.Total = parseFloat($scope.cotizacion.Flete);
            $scope.cotizacion.Igv = ($scope.cotizacion.GrabaIgv || !$scope.aplicaLeyAmazonia) ? (parseFloat($scope.cotizacion.Flete - ($scope.cotizacion.Flete / (1 + $scope.tasaIGV))).toFixed(2)) : 0;
            $scope.cotizacion.SubTotal = parseFloat($scope.cotizacion.Total) - parseFloat($scope.cotizacion.Igv);
        } else {
            $scope.cotizacion.SubTotal = 0;
            $scope.cotizacion.Igv = 0;
            $scope.cotizacion.Total = 0;
        }
        for (i = 0; i < detalles.length; i++) {
            $scope.cotizacion.Total += parseFloat(detalles[i].Importe);
            $scope.cotizacion.Igv += parseFloat(detalles[i].Igv);
        }
        $scope.cotizacion.SubTotal = parseFloat($scope.cotizacion.Total - $scope.cotizacion.Igv);
    }

    $scope.cambioDeGrabarIgv = function (detalles) {
        for (i = 0; i < detalles.length; i++) {
            if ($scope.cotizacion.GrabaIgv) {
                detalles[i].Igv = parseFloat(detalles[i].Importe - (detalles[i].Importe / (1 + $scope.tasaIGV))).toFixed(2);
            } else {
                detalles[i].Igv = 0;
            }
        }
        $scope.calcularTotal(detalles);
    }

    $scope.cambioDeNoGrabarIgv = function (detalles) {
        $scope.cotizacion.GrabaIgv = !$scope.cotizacion.NoGrabaIgv;
        $scope.cambioDeGrabarIgv(detalles);
    }

    $scope.guardarCotizacion = function () {
        cotizacionService.guardarCotizacion({ cotizacion: $scope.cotizacion }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.cotizacion = {};
            jsWebClientPrint.print('idOrdenCotizacion=' + data.IdOrden);
            $('#modal-registro-cotizacion').modal('hide');
            $scope.listarBandejaCotizacion();
        }).error(function (data) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }
    //#endregion

    //#region Validacion 
    $scope.verificarInconsistencias = function () {
        var banderaCantidad0 = false;
        $scope.inconsistencias = [];
        if ($scope.cotizacion.Detalles.length <= 0) {
            $scope.inconsistencias.push("Es necesario seleccionar al menos un producto.");
        } else {
            for (var i = 0; i < $scope.cotizacion.Detalles.length; i++) {
                banderaCantidad0 = ($scope.cotizacion.Detalles[i].Cantidad == 0 || $scope.cotizacion.Detalles[i].Cantidad == undefined) ? true : false;
            }
        } if ($scope.cotizacion.Cliente == null) {
            $scope.inconsistencias.push("Es necesario seleccionar un cliente.");
        } if ($scope.cotizacion.TipoDeComprobante == undefined) {
            $scope.inconsistencias.push("Es necesario seleccionar un documento.");
        } if ($scope.cotizacion.Total == '0.00') {
            $scope.inconsistencias.push("Es necesario que el importe total sea mayor a 0.00.");
        } if (banderaCantidad0) {
            $scope.inconsistencias.push("Es necesario que la cantidad sea mayor a 0.");
        } if ($scope.cotizacion.TipoDeComprobante != null) {
            if ($scope.cotizacion.TipoDeComprobante.SerieSeleccionada == 0 && $scope.cotizacion.TipoDeComprobante.MostrarSelectorSerie && $scope.cotizacion.TipoDeComprobante.EsPropio) {
                $scope.inconsistencias.push("Es necesario seleccionar un serie.");
            }
        } if ($scope.cotizacion.FechaVencimiento == undefined || $scope.cotizacion.FechaVencimiento == "") {
            $scope.inconsistencias.push("Es necesario ingresar la fecha de cotización.");
        }
    }
    //#endregion

    //#region REGISTRO DE CONCEPTO DE SERVICIO 
    $scope.nuevoRegistroConceptoServicio = function () {
        $scope.obtenerConceptosBasicosServicio().then(function (resultado_) {
            $scope.conceptoServicio = { ConceptoBasicoSeleccionado: true, Valor: "0", Sufijo: "", ConceptoBasico: $scope.listaConceptosBasicosServicio[0] };
            $timeout(function () { $('#selectorConceptoBasico').trigger("change"); }, 100);

        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
    }

    $scope.obtenerConceptosBasicosServicio = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        maestroService.obtenerConceptosBasicosServicio({}).success(function (data) {
            $scope.listaConceptosBasicosServicio = data;
            defered.resolve();
        }).error(function (data) {
            SweetAlert.error2(data);
            defered.reject(data);
        });
        return promise;
    }

    $scope.ingresarConceptoBasicoServicio = function () {
        $scope.conceptoServicio.ConceptoBasicoSeleccionado = false;
    }

    $scope.seleccionarConceptoBasicoServicio = function () {
        $scope.conceptoServicio.ConceptoBasicoSeleccionado = true;
    }

    $scope.actualizarNombreConceptoServicio = function () {
        $scope.conceptoServicio.NombreCompleto = (($scope.conceptoServicio.ConceptoBasicoSeleccionado == true) ? $scope.conceptoServicio.ConceptoBasico.Nombre : ($scope.conceptoServicio.NombreConceptoBasico == undefined ? "" : $scope.conceptoServicio.NombreConceptoBasico)) + " " + ($scope.conceptoServicio.Sufijo == undefined ? "" : $scope.conceptoServicio.Sufijo);
    }

    $scope.cerrar = function () {
        $('#modal-registro-concepto-servicio').modal('hide');
    }

    $scope.guardarConceptoServicio = function () {
        conceptoService.guardarConceptoServicio({ conceptoServicio: $scope.conceptoServicio }).success(function (data) {
            $scope.actualizarSelectoresVenta(data);
            SweetAlert.success("Correcto", data.result_description);
            $('#modal-registro-concepto-servicio').modal('hide');
        }).error(function (data) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }

    $scope.actualizarSelectoresVenta = function (conceptoServicioNuevo) {
        precioService.obtenerPrecioConceptoUnico({ idConcepto: conceptoServicioNuevo.data }).success(function (data) {
            var nuevoConceptoServicio = {
                Id: conceptoServicioNuevo.data, CodigoBarra: "", Nombre: $scope.conceptoServicio.NombreCompleto, SoloNombre: $scope.conceptoServicio.NombreCompleto, NombreParaDetalle: $scope.conceptoServicio.NombreCompleto, EsBien: false, IdConceptoBasico: conceptoServicioNuevo.basico_data, Precios: data
            };
            if ($scope.modoDeSeleccionDeConceptoDeNegocio == 1 || $scope.modoDeSeleccionDeConceptoDeNegocio == 4) {
                $scope.productos.push(nuevoConceptoServicio);
            }
            if ($scope.modoDeSeleccionDeConceptoDeNegocio == 2) {
                if ($scope.conceptoServicio.ConceptoBasicoSeleccionado) {
                    if ($scope.idConceptoBasicoSeleccionado == $scope.conceptoServicio.ConceptoBasico.Id) {
                        $scope.productos.push(nuevoConceptoServicio);
                    }
                } else {
                    var nuevoConceptoBasicoServicio = { Id: conceptoServicioNuevo.basico_data, Nombre: $scope.conceptoServicio.NombreConceptoBasico };
                    $scope.conceptos.push(nuevoConceptoBasicoServicio);
                }
            }
            $scope.detalle.Producto = nuevoConceptoServicio;
            var esBusquedaPorCodigoBarra = false;
            $scope.agregarDetalle(esBusquedaPorCodigoBarra);
            $scope.ValidarModoIntegrado();
            $scope.conceptoServicio = {};
        }).error(function (data) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }
    //#endregion
});


app.controller('ventaController', function ($scope, $q, $timeout, $rootScope, $compile, SweetAlert, $filter, DTOptionsBuilder, DTColumnDefBuilder, ventaService, compraService, maestroService, almacenService, clienteService, productoService, centroDeAtencionService, empleadoService, conceptoService, proveedorService, cotizacionService, precioService) {

    //#region INICIO DE VARIABLES
    $scope.inicializar = function () {
        $scope.cargarParametros();
        $scope.limpiarRegistro();
        $scope.inicializarComponentes();
        $scope.cambiarVistaNuevoConceptoServicio();
        $scope.establecerDatosPorDefecto();
    }

    $scope.cargarParametros = function () {
        $scope.aplicaLeyAmazonia = aplicaLeyAmazonia;
        $scope.mostrarCodigoBarraBalanza = mostrarCodigoBarraBalanza;
        $scope.cursorInicialEnCodigoBarra = cursorInicialEnCodigoBarra;
        $scope.esVentaPorContingencia = esVentaPorContingencia;
        $scope.esVentaModoCajaAlmacen = esVentaModoCajaAlmacen;
        $scope.permitirRegistroAlmacenEnVentaPorMostradorIntegradoModoCaja = permitirRegistroAlmacenEnVentaPorMostradorIntegradoModoCaja;
        $scope.permitirRegistroDeGuiasDeRemision = permitirRegistroDeGuiasDeRemision;
        $scope.permitirRegistroConceptoServicio = permitirRegistroConceptoServicio;
        $scope.permitirRegistroFlete = permitirRegistroFlete;
        $scope.permitirRegistroPlaca = permitirRegistroPlaca;
        $scope.idClienteGenerico = idClienteGenerico;
        $scope.idMedioDePagoEfectivo = idMedioDePagoEfectivo;
        $scope.idTipoDocumentoFactura = idTipoDocumentoFactura;
        $scope.esPregeneracionVenta = esPregeneracionVenta;
        $scope.idOrdenAPregenerar = idOrdenAPregenerar;
        $scope.tipoOperacionAPregenerar = tipoOperacionAPregenerar;
        $scope.permitirEnvioPorWhatsApp = permitirEnvioPorWhatsApp;
        $scope.envioComprobantePostVenta = envioComprobantePostVenta;
        $scope.guardadoActivado = false;
        $scope.venderConBoleta = false;
    }

    $scope.limpiarRegistro = function () {
        $scope.codigo = { codigoBarraBalanza: '' };
        $scope.inconsistenciasVenta = [];
    }

    $scope.inicializarComponentes = function () {
        $scope.inicializacionRealizada = true;
    }

    $scope.establecerDatosPorDefecto = function () {
    }

    $scope.cambiarVistaNuevoConceptoServicio = function () {
        if ($scope.mostrarCodigoBarraBalanza) {
            $('#nuevoConceptoServicio').addClass("col-md-offset-8");
        } else {
            $('#nuevoConceptoServicio').addClass("col-md-offset-11");
        }
    }

    $scope.inicioRealizadoFacturacion = function (facturacion) {
        $scope.venta = facturacion;
        $scope.venta.Orden.Detalles = [];
        $scope.venta.MovimientoAlmacen.HayComprobanteDeSalidaDeMercaderia = false;
        $scope.venta.MovimientoAlmacen.RegistroDeMovimientosDeAlmacen = [];
        $scope.inicializacionRealizadaFacturacion = true;
        $scope.venta.EsVentaModoCaja = $scope.esVentaModoCajaAlmacen;
        $scope.venta.Orden.EsVentaPasada = $scope.esVentaPorContingencia;
        $scope.verificarInconsistencias();
        //if ($scope.esPregeneracionVenta && $scope.inicializacionRealizadaRegistroDetalles)
        //    $scope.cargarPregeneracionVenta();
    }

    $scope.inicioRealizadoRegistroDetalles = function () {
        $scope.inicializacionRealizadaRegistroDetalles = true;
        //if ($scope.esPregeneracionVenta && $scope.inicializacionRealizadaFacturacion)
        //    $scope.cargarPregeneracionVenta();
        //($scope.cursorInicialEnCodigoBarra == 1) ? $scope.registradorDetallesAPI.FocusNextCodigoBarra() : $scope.focusCodigoBalanza();
    }

    $scope.cargaRealizadaFacturacion = function () {
        if ($scope.esPregeneracionVenta && $scope.inicializacionRealizadaRegistroDetalles && $scope.inicializacionRealizadaFacturacion)
            $scope.cargarPregeneracionVenta();
        ($scope.cursorInicialEnCodigoBarra == 1) ? $scope.registradorDetallesAPI.FocusNextCodigoBarra() : $scope.focusCodigoBalanza();
    }

    $scope.focusCodigoBalanza = function () {
        $timeout(function () {
            $('#idCodigoBarraBalanza').trigger("focus");
        }, 100);
    };

    $scope.setFocusSerieComprobante = function () {
        $scope.facturacionVentaAPI.SetFocusSerieComprobante();
    };

    $scope.serieComprobante_enter = function () {
        if ($scope.guardadoActivado == false) {
            $scope.guardar();
            $scope.guardadoActivado = true;
        }
    };

    $scope.cambiarAfeccionIgv = function (aplicaIgvEnAmazonia) {
        $scope.registradorDetallesAPI.CambioDeGrabarIgv(aplicaIgvEnAmazonia);
    };

    $scope.cambioFacturacion = function () {
        $scope.verificarInconsistencias();
    };

    //#endregion

    //#region PREGENERACION DE VENTA
    $scope.cargarPregeneracionVenta = function () {
        $scope.operacionAPregenerar = {};
        $scope.obtenerOperacionAPregenerar().then(function () {
            $scope.registradorDetallesAPI.CargarDetallesOperacionAPregenerar($scope.operacionAPregenerar);
            $scope.venta.Orden.Observacion = $scope.operacionAPregenerar.Observacion;
            $scope.venta.Orden.AplicarIGVCuandoEsAmazonia = $scope.aplicaLeyAmazonia ? $scope.operacionAPregenerar.GrabaIgv : true;
            if ($scope.venta.Orden.Cliente.Id == undefined || $scope.operacionAPregenerar.Cliente.Id != $scope.venta.Orden.Cliente.Id) $scope.facturacionVentaAPI.SetActorComercialPorId($scope.operacionAPregenerar.Cliente.Id);
            $scope.venta.Orden.Cliente.Alias = $scope.operacionAPregenerar.Alias;
            if ($scope.venta.Orden.AplicarIGVCuandoEsAmazonia) {
                $scope.cambiarAfeccionIgv($scope.venta.Orden.AplicarIGVCuandoEsAmazonia)
            }
            $scope.venta.Orden.EsOperacionPreGenerada = $scope.tipoOperacionAPregenerar == 1;
            $scope.venta.Orden.IdOperacionPreGenerada = $scope.operacionAPregenerar.IdOrden;
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
    }

    $scope.obtenerOperacionAPregenerar = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        if ($scope.tipoOperacionAPregenerar == 1) {
            cotizacionService.obtenerCotizacionParaEditar({ idOrden: $scope.idOrdenAPregenerar }).success(function (data) {
                $scope.operacionAPregenerar = data;
                defered.resolve();
            }).error(function (data) {
                $scope.messageError(data.error);
                defered.reject(data);
            });
        } else if ($scope.tipoOperacionAPregenerar == 2) {
            ventaService.obtenerOrdenVentaParaClonar({ idOrden: $scope.idOrdenAPregenerar }).success(function (data) {
                $scope.operacionAPregenerar = data;
                defered.resolve();
            }).error(function (data) {
                $scope.messageError(data.error);
                defered.reject(data);
            });
        }
        return promise;
    }
    //#endregion

    //#region BUSQUEDA POR CODIGO BARRA DE BALANZA
    $scope.buscarConceptoPorCodigoDeBarraBalanza = function () {
        if ($scope.codigo.codigoBarraBalanza == undefined || $scope.codigo.codigoBarraBalanza == '' || $scope.codigo.codigoBarraBalanza.length != 24) {
            SweetAlert.warning("Advertencia", "Ingresar un código de balanza valido");
            $scope.codigo.codigoBarraBalanza = '';
            $scope.focusCodigoBalanza();
            //document.getElementById("idCodigoBarraBalanza").value = '';
        } else {
            let codigoConcepto = $scope.codigo.codigoBarraBalanza.substring(2, 6);
            let cantidad = parseFloat($scope.codigo.codigoBarraBalanza.substring(12, 17)) / 1000;
            let importe = parseFloat($scope.codigo.codigoBarraBalanza.substring(6, 12)) / 100;
            let codigoPuntoVenta = $scope.codigo.codigoBarraBalanza.substring(18, 21);
            let codigoVendedor = $scope.codigo.codigoBarraBalanza.substring(21, 24);
            $scope.codigo.codigoBarraBalanza = '';
            //document.getElementById("idCodigoBarraBalanza").value = '';
            if ($scope.venta.Orden.Vendedor.Codigo != codigoVendedor && $scope.venta.Orden.Detalles.length > 0) {
                SweetAlert.warning("Advertencia", "EL VENDEDOR ES DISTINTO AL QUE ESTA REALIZANDO LA VENTA");
            } else {
                $scope.registradorDetallesAPI.AgregarConceptoPorCodigoBarra(codigoConcepto, cantidad, importe);
                $scope.facturacionVentaAPI.SeleccionarPuntoDeVentaConCodigo(codigoPuntoVenta);
                $scope.facturacionVentaAPI.SeleccionarVendedorConCodigo(codigoVendedor);
                $scope.verificarInconsistencias();
                /*$timeout(function () { $('#radio-0').trigger("focus"); }, 100);*/
            }
        }
    }
    //#endregion

    //#region VALIDACION DE VENTAS
    $scope.verificarInconsistencias = function () {
        $scope.inconsistenciasVenta = [];
        var hayCantidad0 = false;
        var hayImporte0 = false;
        var esCantidadNegativo = false;
        var esCantidadMayorAStock = false;
        if ($scope.venta.Orden.Detalles.length <= 0) {
            $scope.inconsistenciasVenta.push("Es necesario seleccionar al menos un producto.");
        } else {
            for (var i = 0; i < $scope.venta.Orden.Detalles.length; i++) {
                hayCantidad0 = hayCantidad0 || $scope.venta.Orden.Detalles[i].Cantidad == 0 ? true : false;
                hayImporte0 = hayImporte0 || parseFloat($scope.venta.Orden.Detalles[i].Importe) == 0 ? true : false;
                esCantidadNegativo = esCantidadNegativo || $scope.venta.Orden.Detalles[i].Cantidad == 0 ? true : false;
                esCantidadMayorAStock = esCantidadMayorAStock || $scope.venta.Orden.Detalles[i].Producto.EsBien && $scope.registradorDetallesAPI.SalidaBienesSujetasAStock() && $scope.venta.Orden.Detalles[i].Cantidad > $scope.venta.Orden.Detalles[i].Producto.Stock ? true : false;
            }
            var detallesConImportesInvalidados = $scope.venta.Orden.Detalles.filter(detalle => detalle.Importe < 0);
            if ((Array.isArray(detallesConImportesInvalidados) && detallesConImportesInvalidados.length)) {
                var esImporteInvalido = true;
            }
            var detallesConCantidadesNegativas = $scope.venta.Orden.Detalles.filter(detalle => detalle.Cantidad == undefined);
            if ((Array.isArray(detallesConCantidadesNegativas) && detallesConCantidadesNegativas.length)) {
                esCantidadNegativo = true;
            }
        }
        if (esImporteInvalido) {
            $scope.inconsistenciasVenta.push("Es necesario que el importe total no sea menor a 0.00");
        }
        if (hayCantidad0) {
            $scope.inconsistenciasVenta.push("Es necesario la cantidad sea mayor a 0");
        }
        if ($scope.venta.Orden.Comprobante.Tipo.Id == $scope.idTipoDocumentoFactura && hayImporte0) {
            $scope.inconsistenciasVenta.push("Es necesario que el importe de los detalles sean mayores a 0");
        }
        if (esCantidadNegativo) {
            $scope.inconsistenciasVenta.push("Es necesario que la cantidad no sea negativo");
        }
        if (esCantidadMayorAStock) {
            $scope.inconsistenciasVenta.push("Es necesario que la cantidad no sea mayor al stock");
        }
        if ($scope.venta.EsVentaModoCaja) {
            if ($scope.venta.Orden.PuntoDeVenta == undefined) {
                $scope.inconsistenciasVenta.push("Es necesario seleccionar un punto de venta.");
            }
            if ($scope.venta.Orden.Vendedor == undefined) {
                $scope.inconsistenciasVenta.push("Es necesario seleccionar un vendedor.");
            }
        }
    };
    //#endregion

    //#region VALIDACION DE VENTAS
    $scope.guardar = function () {
        ventaService.guardarVenta({ venta: $scope.venta }).success(function (data) {
            $scope.telefonoCliente = $scope.venta.Orden.Cliente.Telefono;
            $scope.venderConBoleta = data.VenderConBoleta;
            $scope.establecerDatosPosVenta();
            $scope.registradorDetallesAPI.RecargarConceptos();
            if ($scope.envioComprobantePostVenta) {
                $scope.idVenta = data.data;
                $scope.serieNumeroDocumento = data.SerieNumeroDocumento;
                $scope.idEncriptado = data.IdEncriptado;
                $scope.inicializarVentaRealizada();
                $('#modal-post-venta').modal('show');
            } else {
                jsWebClientPrint.print('idVenta=' + data.data);
                SweetAlert.success("Correcto", data.result_description);
            }
            $scope.guardadoActivado = false;
        }).error(function (data, status) {
            SweetAlert.error2(data);
        });
    };

    $scope.establecerDatosPosVenta = function () {
        $scope.limpiarRegistro();
        $scope.venta.Orden.Cliente = {};
        $scope.registradorDetallesAPI.LimpiarDetallesOperacion();
        $scope.facturacionVentaAPI.LimpiarDatosDespuesFacturar();
        ($scope.cursorInicialEnCodigoBarra == 1) ? $scope.registradorDetallesAPI.FocusNextCodigoBarra() : $scope.focusCodigoBalanza();
    }
    //#endregion

    //#region Modal envio de documento 
    $scope.inicializarVentaRealizada = function () {
        $scope.envio = {};
        $scope.envio.ModoEnvio = $scope.permitirEnvioPorWhatsApp ? 1 : 2;
        let radioEtiqueta = document.getElementById('modoenvio-' + $scope.envio.ModoEnvio);
        radioEtiqueta.checked = true;
        if ($scope.permitirEnvioPorWhatsApp) {
            $scope.SeleccionarEnvioPorWhatsApp();
            $scope.envio.NumeroCelular = $scope.telefonoCliente;
            $scope.ActualizarEnvioPorWhatsApp();
        } else {
            $scope.SeleccionarEnvioPorCorreo();
        }
    }

    $scope.SeleccionarEnvioPorWhatsApp = function () {
        $scope.envio.CodigoPais = '+51';
        $scope.envio.NumeroCelular = '';
    }

    $scope.ActualizarEnvioPorWhatsApp = function () {
        $scope.envio.UrlWhatsApp = 'https://api.whatsapp.com/send?phone=' + $scope.envio.CodigoPais + $scope.envio.NumeroCelular + '&text=Estimado%20cliente%2C%0ASe%20env%C3%ADa%20el%20documento%20' + $scope.serieNumeroDocumento + '.%20Para%20ver%20click%20en%20el%20siguiente%20enlace%3A%0A%0A' + URL_ + '/Comprobante/DescargarComprobante?id=' + $scope.idEncriptado;
    }

    $scope.enviarWhatsApp = function () {
        $('#modal-post-venta').modal('hide');
    }

    $scope.SeleccionarEnvioPorCorreo = function () {
        $scope.envio.CorreoElectronico = '';
        $scope.envio.CorreosElectronicos = [];
    }

    $scope.agregarCorreoElectronico = function () {
        $scope.envio.CorreosElectronicos.push($scope.envio.CorreoElectronico);
        $scope.envio.CorreoElectronico = '';
        $timeout(function () { $('#correoImput').trigger("change"); }, 100);
    }

    $scope.eliminarCorreoElectronico = function (index) {
        $scope.envio.CorreosElectronicos.splice(index);
    }

    $scope.enviarCorreoElectronico = function () {
        ventaService.enviarCorreoElectronicoConDocumento({ idOrdenDeVenta: $scope.verDocumento.IdOrden, formato: $scope.formato.Id, correosElectronicos: $scope.envio.CorreosElectronicos }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.limpiarEnvioDocumento = function () {
        $scope.envio.CorreosElectronicos = [];
        $scope.envio.CorreoElectronico = '';
        $scope.envio.CodigoPais = '+51';
        $scope.envio.NumeroCelular = '';
    }

    $scope.imprimirComprobante = function () {
        jsWebClientPrint.print('idVenta=' + $scope.idVenta);
    }
    //#endregion

    $scope.nuevoRegistroConceptoServicio = function () {
        $scope.registradorConceptoServicioAPI.NuevoRegistroConceptoServicio();
    }

    $scope.ingresoConceptoServicio = function (conceptoServicio) {
        $scope.registradorDetallesAPI.AgregarConceptoServicio(conceptoServicio);
    }



    $scope.cambiarImporteTotal = function (importeTotal) {
        $scope.facturacionVentaAPI.SetTotalVenta(importeTotal);
        $scope.verificarInconsistencias();
    };
});

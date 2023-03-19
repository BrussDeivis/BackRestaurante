app.controller('pedidoController', function ($scope, $q, $timeout, $rootScope, $compile, SweetAlert, $filter, DTOptionsBuilder, DTColumnDefBuilder, pedidoService, ventaService, compraService, maestroService, almacenService, clienteService, productoService, centroDeAtencionService, empleadoService, conceptoService, proveedorService, cotizacionService, precioService, configuracionService) {

    //#region Inicializar variables
    $scope.inicializar = function () {
        $scope.cargarParametros();
        $scope.limpiarRegistro();
        $scope.cambiarVistaNuevoConceptoServicio();
        $scope.cargarParametrosParaPedidos().then(function (resultado) {
            $scope.obtenerTiposDeComprobante().then(function (resultado) {
                $scope.inicializarComponentes();
                $scope.establecerDatosPorDefecto();
            });
        });
        $scope.listarPedidos();
    }

    $scope.cargarParametros = function () {
        $scope.aplicaLeyAmazonia = aplicaLeyAmazonia;
        $scope.tasaIGV = tasaIGV;
        $scope.mostrarCodigoBarraBalanza = mostrarCodigoBarraBalanza;
        $scope.cursorInicialEnCodigoBarra = cursorInicialEnCodigoBarra;
        $scope.esVentaPorContingencia = esVentaPorContingencia;
        $scope.esVentaModoCajaAlmacen = esVentaModoCajaAlmacen;
        $scope.permitirRegistroDeGuiasDeRemision = permitirRegistroDeGuiasDeRemision;
        $scope.permitirRegistroConceptoServicio = permitirRegistroConceptoServicio;
        $scope.permitirRegistroFlete = permitirRegistroFlete;
        $scope.permitirRegistroPlaca = permitirRegistroPlaca;
        $scope.idClienteGenerico = idClienteGenerico;
        $scope.idMedioDePagoEfectivo = idMedioDePagoEfectivo;
        $scope.idTipoDocumentoFactura = idTipoDocumentoFactura;
        $scope.idTipoDocumentoCuandoClienteEsGenerico = idTipoDocumentoCuandoClienteEsGenerico;
        $scope.idTipoDocumentoPorDefectoParaVenta = idTipoDocumentoPorDefectoParaVenta;
        $scope.esPregeneracionPedido = esPregeneracionPedido;
        $scope.idOrdenAPregenerar = idOrdenAPregenerar;
        $scope.fechaInicio = fechaInicio;
        $scope.fechaFin = fechaFin;
        $scope.data = data;
        $scope.imprimirPedido = imprimirPedido;
        $scope.idTipoComprobantePedido = idTipoComprobantePedido;
        $scope.idTipoComprobanteEmitirPorDefecto = idTipoComprobanteEmitirPorDefecto;
    }

    $scope.limpiarRegistro = function () {
        $scope.codigo = { codigoBarraBalanza: '' };
        $scope.accionModal = '';
        $scope.pedido = {};
        $scope.tipoSeleccionado = {};
        $scope.inconsistenciasRegistrarPedido = [];
        $scope.inconsistenciasConfirmarPedido = [];
        $scope.esNuevoPedido = true;
    }

    $scope.cargarParametrosParaPedidos = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        pedidoService.ObtenerParametrosDePedidos({}).success(function (data) {
            $scope.parametrosParaPedidos = data.parametrosConfiguracion;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
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
    };

    $scope.establecerTiposDeComprobante = function () {
        if ($scope.esNuevoPedido) {
            $scope.tipoSeleccionado = $scope.tiposDeComprobantes.find(tc => tc.Id == $scope.idTipoComprobanteEmitirPorDefecto);
        } else {
            $scope.tipoSeleccionado = $scope.tiposDeComprobantes.find(tc => tc.Id == $scope.pedido.Orden.IdTipoComprobanteaEmitir);
        }
        $timeout(function () { $('#comprobantePredeterminado').trigger("change"); }, 100);
    };

    $scope.listarPedidos = function () {
        pedidoService.ObtenerPedidos({ desde: $scope.fechaInicio, hasta: $scope.fechaFin }).success(function (data) {
            $scope.pedidos = data;
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema al traer los pedidos", data.error)
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
    //#endregion

    //#region Registro Pedido

    $scope.nuevoRegistroConceptoServicio = function () {
        $scope.registradorConceptoServicioAPI.NuevoRegistroConceptoServicio();
    }

    $scope.ingresoConceptoServicio = function (conceptoServicio) {
        $scope.registradorDetallesAPI.AgregarConceptoServicio(conceptoServicio);
    }

    $scope.cambiarImporteTotal_DatosFacturacion = function (importeTotal) {
        //$scope.verificarinconsistenciasRegistrarPedido();
    };

    $scope.validarRegistradorDetallesPedidos_DatosFacturacion = function () {
        $scope.verificarinconsistenciasRegistrarPedido();
    }

    $scope.inicioRealizadoRegistroDetalles_DatosFacturacion = function () {
        $scope.inicializacionRealizadaRegistroDetalles = true;
        $scope.verificarPregeneracionPedido();
    }

    $scope.focusCodigoBalanza_DatosFacturacion = function () {
        $timeout(function () { $('#idCodigoBarraBalanza').trigger("focus"); }, 100);
    };

    $scope.setFocusSerieComprobante_DatosFacturacion = function () {
        $scope.datosFacturacionVentaAPI.SetFocusSerieComprobante();
    };

    $scope.cambiarAfeccionIgv_DatosFacturacion = function (aplicaIgvEnAmazonia) {
        $scope.registradorDetallesAPI.CambioDeGrabarIgv(aplicaIgvEnAmazonia);
    };

    $scope.inicioRealizado_DatosFacturacion = function (datosFacturacion) {
        $scope.pedido = datosFacturacion;
        $scope.pedido.MovimientoAlmacen.EntregaDiferida = 'false';
        $scope.pedido.MovimientoAlmacen.HayComprobanteDeSalidaDeMercaderia = false;
        $scope.pedido.MovimientoAlmacen.RegistroDeMovimientosDeAlmacen = [];
        //$scope.inicializacionRealizadaDatosFacturacion = true;
        $scope.pedido.EsVentaModoCaja = $scope.esVentaModoCajaAlmacen;
        $scope.pedido.Orden.EsVentaPasada = $scope.esVentaPorContingencia;
        //$scope.verificarinconsistenciasRegistrarPedido();
    }

    $scope.cargaRealizada_DatosFacturacion = function () {
        $scope.inicializacionRealizadaDatosFacturacion = true;
        $scope.verificarPregeneracionPedido();
    }

    $scope.verificarPregeneracionPedido = function () {
        if ($scope.esPregeneracionPedido && $scope.inicializacionRealizadaRegistroDetalles && $scope.inicializacionRealizadaDatosFacturacion && $scope.inicializacionRealizadaFacturacion) {
            $('#modal-registro-pedido').modal('show');
            $scope.cargarPregeneracionPedido();
        }
    }

    $scope.cargarPregeneracionPedido = function () {
        $scope.limpiarRegistro();
        $scope.esNuevoPedido = true;
        $scope.accionModal = 'NUEVO';
        $scope.registradorDetallesAPI.LimpiarDetallesOperacion();
        $scope.datosFacturacionVentaAPI.LimpiarDatosDespuesFacturar();
        $scope.obtenerPedido($scope.idOrdenAPregenerar).then(function (resultado) {
            $scope.pedido.Orden.Id = 0;
            //$scope.datosFacturacionVentaAPI.SetActorComercialPorId($scope.idClientePedido);
            //$scope.facturacionVentaAPI.AsignarComprobante($scope.pedido.Orden.Comprobante.Tipo.Id); 
            $scope.pedido.Orden.Cliente = $scope.clientePedido;
            $scope.asignarComprobantePorDefecto($scope.clientePedido);
            $scope.datosFacturacionVentaAPI.AsignarComprobante($scope.idTipoComprobantePedido);
            $scope.verificarinconsistenciasRegistrarPedido();
        });
    }

    $scope.serieComprobanteEnter_DatosFacturacion = function () {
        $scope.guardarPedido();
    };

    $scope.cambio_DatosFacturacion = function () {
        $scope.verificarinconsistenciasRegistrarPedido();
    };

    $scope.cambioCliente_DatosFacturacion = function (cliente) {
        $scope.asignarComprobantePorDefecto(cliente);
    };

    $scope.asignarComprobantePorDefecto = function (cliente) {
        if ($scope.tiposDeComprobantes != undefined) {
            if (cliente.Id === $scope.idClienteGenerico) {
                $scope.tipoSeleccionado = $scope.tiposDeComprobantes.find(tc => tc.Id == $scope.idTipoDocumentoCuandoClienteEsGenerico);
            } else {
                if (cliente.NumeroDocumentoIdentidad.length == 11) {
                    $scope.tipoSeleccionado = $scope.tiposDeComprobantes.find(tc => tc.Id == $scope.idTipoDocumentoFactura);
                } else {
                    $scope.tipoSeleccionado = $scope.tiposDeComprobantes.find(tc => tc.Id == $scope.idTipoDocumentoPorDefectoParaVenta);
                }
            }
            $timeout(function () { $('#comprobantePredeterminado').trigger("change"); }, 100);
        }
    };
    //#endregion

    //#region Verificacion Inconsistencias Registro Pedido
    $scope.verificarinconsistenciasRegistrarPedido = function () {
        $scope.inconsistenciasRegistrarPedido = [];
        var hayCantidad0 = false;
        var hayImporte0 = false;
        var esCantidadMayorAStock = false;
        if ($scope.pedido.Orden != undefined) {
            if ($scope.pedido.Orden.Detalles != undefined) {
                if ($scope.pedido.Orden.Detalles.length <= 0) {
                    $scope.inconsistenciasRegistrarPedido.push("Es necesario seleccionar al menos un producto.");
                } else {
                    for (var i = 0; i < $scope.pedido.Orden.Detalles.length; i++) {
                        hayCantidad0 = hayCantidad0 || ($scope.pedido.Orden.Detalles[i].Cantidad == '' || parseFloat($scope.pedido.Orden.Detalles[i].Cantidad) == 0) ? true : false;
                        hayImporte0 = hayImporte0 || ($scope.pedido.Orden.Detalles[i].Importe == '' || parseFloat($scope.pedido.Orden.Detalles[i].Importe) == 0) ? true : false;
                        esCantidadMayorAStock = esCantidadMayorAStock || $scope.pedido.Orden.Detalles[i].Producto.EsBien && $scope.registradorDetallesAPI.SalidaBienesSujetasAStock() && $scope.pedido.Orden.Detalles[i].Cantidad > $scope.pedido.Orden.Detalles[i].Producto.Stock ? true : false;
                    }
                }

                if (hayCantidad0) {
                    $scope.inconsistenciasRegistrarPedido.push("Es necesario que la cantidad sea mayor a 0");
                }
                if (hayImporte0) {
                    $scope.inconsistenciasRegistrarPedido.push("Es necesario que importe de detalle sea mayor a 0");
                }
                if (esCantidadMayorAStock) {
                    $scope.inconsistenciasRegistrarPedido.push("Es necesario que la cantidad no sea mayor al stock");
                }
            }
            if ($scope.pedido.Orden.Comprobante.Tipo.Id == undefined) {
                $scope.inconsistenciasRegistrarPedido.push("Es necesario seleccionar un documento.");
            }
            if ($scope.pedido.Orden.Comprobante.Serie == undefined) {
                $scope.inconsistenciasRegistrarPedido.push("Es necesario que el documento tenga alguna serie.");
            }
            if ($scope.pedido.MovimientoAlmacen.HaySalidaDeMercaderia && $scope.pedido.Orden.Cliente.Id == $scope.idClienteGenerico) {
                $scope.inconsistenciasRegistrarPedido.push("Es necesario identificar el cliente al emitir una guia de remision");
            }
            if ($scope.pedido.EsVentaModoCaja) {
                if ($scope.pedido.Orden.PuntoDeVenta == undefined) {
                    $scope.inconsistenciasRegistrarPedido.push("Es necesario seleccionar un punto de venta.");
                }
                if ($scope.pedido.Orden.Vendedor == undefined) {
                    $scope.inconsistenciasRegistrarPedido.push("Es necesario seleccionar un vendedor.");
                }
            }
            if ($scope.pedido.Orden.Comprobante.Tipo == undefined) {
                $scope.inconsistenciasRegistrarPedido.push("Es necesario seleccionar un documento.");
            }
            if ($scope.pedido.Orden.Cliente.Id == $scope.idClienteGenerico) {
                if ($scope.pedido.Orden.Cliente.Alias == undefined || $scope.pedido.Orden.Cliente.Alias == '') {
                    $scope.inconsistenciasRegistrarPedido.push("Es necesario ingresar un alias.");
                }
            }
        }
    };
    //#endregion

    //#region Obtener Pedido Para Operaciones
    $scope.obtenerPedido = function (idOrdenPedido) {
        var defered = $q.defer();
        var promise = defered.promise;
        pedidoService.ObtenerPedidoParaEditar({ IdOrdenPedido: idOrdenPedido }).success(function (data) {
            $scope.registradorDetallesAPI.LimpiarDetallesOperacion();
            $scope.pedido.Orden.Cliente = {};
            var pedidoAEditar = data.OrdenPedido;
            var Conceptos = data.Conceptos;
            pedidoAEditar.Orden.SubTotal = pedidoAEditar.Orden.Igv > 0 ? parseFloat(pedidoAEditar.Orden.Flete - parseFloat(pedidoAEditar.Orden.Flete - (pedidoAEditar.Orden.Flete / (1 + $scope.tasaIGV)))) : pedidoAEditar.Orden.Flete;
            pedidoAEditar.Orden.Igv = pedidoAEditar.Orden.Flete - pedidoAEditar.Orden.SubTotal;
            for (var i = 0; i < pedidoAEditar.Orden.Detalles.length; i++) {
                pedidoAEditar.Orden.Detalles[i].Producto = Conceptos.find(x => x.Id == pedidoAEditar.Orden.Detalles[i].Producto.Id);
                pedidoAEditar.Orden.SubTotal += pedidoAEditar.Orden.Detalles[i].Importe - pedidoAEditar.Orden.Detalles[i].Igv;
                pedidoAEditar.Orden.Igv += pedidoAEditar.Orden.Detalles[i].Igv;
            };
            $scope.pedido.Orden = pedidoAEditar.Orden;
            $scope.pedido.Orden.Detalles.forEach(d => { d.PrecioTarifa = d.Producto.Precios.find(x => x.Valor == d.PrecioUnitario); });
            $scope.pedido.MovimientoAlmacen = pedidoAEditar.MovimientoAlmacen;
            $scope.pedido.MovimientoAlmacen.EntregaDiferida = $scope.pedido.MovimientoAlmacen.EntregaDiferida ? 'true' : 'false';
            $scope.clientePedido = pedidoAEditar.Orden.Cliente;
            $scope.pedido.Orden.Cliente = pedidoAEditar.Orden.Cliente;
            $scope.validarDetalles($scope.pedido.Orden.Detalles);
            defered.resolve();
        }).error(function (error) {
            defered.reject(error);
        });
        return promise;
    }

    $scope.validarDetalles = function (detalles) {
        let detallesConBienes = false;
        for (i = 0; i < detalles.length; i++) {
            detallesConBienes = detallesConBienes || detalles[i].Producto.EsBien;
        }
        $scope.pedido.Orden.HayBienesEnLosDetalles = detallesConBienes;
    };
    //#endregion

    //#region Confirmar Pedido

    $scope.cambiarAfeccionIgv_Facturacion = function (aplicaIgvEnAmazonia) {
        let idOrdenPedido = $scope.pedido.Orden.Id;
        $scope.registradorDetallesAPI.LimpiarDetallesOperacion();
        $scope.datosFacturacionVentaAPI.LimpiarDatosDespuesFacturar();
        $scope.obtenerPedido(idOrdenPedido).then(function (resultado) {
            $scope.establecerTiposDeComprobante();
            $scope.facturacionVentaAPI.AsignarComprobante($scope.pedido.Orden.Comprobante.Tipo.Id);
            $scope.registradorDetallesAPI.CargarDetallesOperacion($scope.pedido.Orden);
            $scope.pedido.Orden.AplicarIGVCuandoEsAmazonia = aplicaIgvEnAmazonia;
            let ordenCambiada = $scope.registradorDetallesAPI.CambioDeGrabarIgv(aplicaIgvEnAmazonia);
            $scope.pedido.Orden.Detalles = ordenCambiada.Detalles;
            pedidoService.GuardarPedido({ pedido: $scope.pedido }).success(function (data) {
                $scope.cerrarConfirmarPedido();
                $scope.iniciarConfirmarPedido(idOrdenPedido);
            }).error(function (data, status) {
                SweetAlert.error2(data);
            });
        });
    };

    $scope.inicioRealizado_Facturacion = function (facturacion) {
        $scope.pedido = facturacion;
        $scope.pedido.MovimientoAlmacen.EntregaDiferida = 'false';
        $scope.pedido.MovimientoAlmacen.HayComprobanteDeSalidaDeMercaderia = false;
        $scope.pedido.MovimientoAlmacen.RegistroDeMovimientosDeAlmacen = [];
        //$scope.inicializacionRealizadaFacturacion = true;
        $scope.pedido.EsVentaModoCaja = $scope.esVentaModoCajaAlmacen;
        $scope.pedido.Orden.EsVentaPasada = $scope.esVentaPorContingencia;
        //$scope.verificarinconsistenciasConfirmarPedido();
    }

    $scope.cargaRealizada_Facturacion = function () {
        $scope.inicializacionRealizadaFacturacion = true;
        $scope.verificarPregeneracionPedido();
    }

    $scope.serieComprobanteEnter_Facturacion = function () {
        $scope.confirmarPedido();
    };

    $scope.cambio_Facturacion = function () {
        $scope.verificarinconsistenciasConfirmarPedido();
    };
    //#endregion

    //#region Verificacion Inconsistencias Confirmar Pedido
    $scope.verificarinconsistenciasConfirmarPedido = function () {
        $scope.inconsistenciasConfirmarPedido = [];
        var hayCantidad0 = false;
        var hayImporte0 = false;
        var esCantidadMayorAStock = false;
        if ($scope.pedido.Orden != undefined) {
            if ($scope.pedido.Orden.Detalles != undefined) {
                if ($scope.pedido.Orden.Detalles.length <= 0) {
                    $scope.inconsistenciasConfirmarPedido.push("Es necesario seleccionar al menos un producto.");
                } else {
                    for (var i = 0; i < $scope.pedido.Orden.Detalles.length; i++) {
                        hayCantidad0 = hayCantidad0 || ($scope.pedido.Orden.Detalles[i].Cantidad == '' || parseFloat($scope.pedido.Orden.Detalles[i].Cantidad) == 0) ? true : false;
                        hayImporte0 = hayImporte0 || ($scope.pedido.Orden.Detalles[i].Importe == '' || parseFloat($scope.pedido.Orden.Detalles[i].Importe) == 0) ? true : false;
                        esCantidadMayorAStock = esCantidadMayorAStock || $scope.pedido.Orden.Detalles[i].Producto.EsBien && $scope.registradorDetallesAPI.SalidaBienesSujetasAStock() && $scope.pedido.Orden.Detalles[i].Cantidad > $scope.pedido.Orden.Detalles[i].Producto.Stock ? true : false;
                    }
                }

                if (hayCantidad0) {
                    $scope.inconsistenciasConfirmarPedido.push("Es necesario que la cantidad sea mayor a 0");
                }
                if (hayImporte0) {
                    $scope.inconsistenciasConfirmarPedido.push("Es necesario que importe de detalle sea mayor a 0");
                }
                if (esCantidadMayorAStock) {
                    $scope.inconsistenciasConfirmarPedido.push("Es necesario que la cantidad no sea mayor al stock");
                }
            }
            if ($scope.pedido.Orden.Comprobante.Tipo.Id == undefined) {
                $scope.inconsistenciasConfirmarPedido.push("Es necesario seleccionar un documento.");
            }
            if ($scope.pedido.Orden.Comprobante.Serie == undefined) {
                $scope.inconsistenciasConfirmarPedido.push("Es necesario que el documento tenga alguna serie.");
            }
            if ($scope.pedido.MovimientoAlmacen.HaySalidaDeMercaderia && $scope.pedido.Orden.Cliente.Id == $scope.idClienteGenerico) {
                $scope.inconsistenciasConfirmarPedido.push("Es necesario identificar el cliente al emitir una guia de remision");
            }
            if ($scope.pedido.EsVentaModoCaja) {
                if ($scope.pedido.Orden.PuntoDeVenta == undefined) {
                    $scope.inconsistenciasConfirmarPedido.push("Es necesario seleccionar un punto de venta.");
                }
                if ($scope.pedido.Orden.Vendedor == undefined) {
                    $scope.inconsistenciasConfirmarPedido.push("Es necesario seleccionar un vendedor.");
                }
            }
            if ($scope.pedido.Orden.Comprobante.Tipo == undefined) {
                $scope.inconsistenciasConfirmarPedido.push("Es necesario seleccionar un documento.");
            }
            if ($scope.pedido.Orden.Cliente.Id == $scope.idClienteGenerico) {
                if ($scope.pedido.Orden.Cliente.Alias == undefined || $scope.pedido.Orden.Cliente.Alias == '') {
                    $scope.inconsistenciasConfirmarPedido.push("Es necesario ingresar un alias.");
                }
            }
        }
    };
    //#endregion

    //#region Nuevo Pedido
    $scope.iniciarNuevoPedido = function () {
        $scope.limpiarRegistro();
        $scope.esNuevoPedido = true;
        $scope.accionModal = 'NUEVO';
        $scope.establecerTiposDeComprobante();
        $scope.registradorDetallesAPI.LimpiarDetallesOperacion();
        $scope.datosFacturacionVentaAPI.LimpiarDatosDespuesFacturar();
        $scope.verificarinconsistenciasRegistrarPedido();
    }
    //#endregion

    //#region Editar Pedido
    $scope.iniciarEditarPedido = function (item) {
        $scope.limpiarRegistro();
        $scope.esNuevoPedido = false;
        $scope.accionModal = 'EDITAR';
        $scope.registradorDetallesAPI.LimpiarDetallesOperacion();
        $scope.datosFacturacionVentaAPI.LimpiarDatosDespuesFacturar();
        $scope.obtenerPedido(item.Id).then(function (resultado) {
            $scope.establecerTiposDeComprobante();
            $scope.facturacionVentaAPI.AsignarComprobante($scope.pedido.Orden.Comprobante.Tipo.Id);
            $scope.verificarinconsistenciasRegistrarPedido();
        });
    }
    //#endregion

    //#region Guardar Pedido
    $scope.guardarPedido = function () {
        $scope.pedido.Orden.IdTipoComprobanteaEmitir = $scope.tipoSeleccionado.Id;
        pedidoService.GuardarPedido({ pedido: $scope.pedido }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            if ($scope.imprimirPedido) {
                jsWebClientPrint.print('idOperacion=' + data.IdOrden + '&tipoOperacion=' + 1);
            }
            $('#modal-registro-pedido').modal('hide');
            $scope.registradorDetallesAPI.RecargarConceptos();
            $scope.establecerDatosPosGuardadoPedido();
            $scope.listarPedidos();
        }).error(function (data, status) {
            SweetAlert.error2(data);
        });
    }

    $scope.establecerDatosPosGuardadoPedido = function () {
        $scope.limpiarRegistro();
        $scope.registradorDetallesAPI.LimpiarDetallesOperacion();
        $scope.datosFacturacionVentaAPI.LimpiarDatosDespuesFacturar();
        ($scope.cursorInicialEnCodigoBarra == 1) ? $scope.registradorDetallesAPI.FocusNextCodigoBarra() : $scope.focusCodigoBalanza();
    }
    //#endregion

    //#region Confirmar Pedido
    $scope.iniciarConfirmarPedido = function (idOrdenPedido) {
        $scope.esNuevoPedido = false;
        $scope.facturacionVentaAPI.LimpiarDatosDespuesFacturar();
        $scope.obtenerDocumentoPedido(idOrdenPedido);
        $scope.obtenerPedido(idOrdenPedido).then(function (resultado) {
            $scope.facturacionVentaAPI.SetTotalVenta($scope.pedido.Orden.Total);
            $scope.facturacionVentaAPI.SetActorComercial($scope.pedido.Orden.Cliente);
            $scope.facturacionVentaAPI.AsignarComprobante($scope.pedido.Orden.IdTipoComprobanteaEmitir);
            $scope.verificarinconsistenciasConfirmarPedido();
        });
    }

    $scope.obtenerDocumentoPedido = function (idOrdenPedido) {
        pedidoService.ObtenerDocumentoDePedido({ idOrdenDePedido: idOrdenPedido }).success(function (data) {
            $scope.verPedido = data;
            $scope.tamanioComprobante = '80mm';
            document.getElementById("pdfDocumento").innerHTML = $scope.verPedido.CadenaHtmlDeDocumento;
            $timeout(function () { $('#pdfDocumento').trigger("change"); }, 100);
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema", data.error);
        });
    }

    $scope.confirmarPedido = function () {
        pedidoService.ConfirmarPedido({ pedido: $scope.pedido }).success(function (data) {
            $('#modal-confirmar-pedido').modal('hide');
            SweetAlert.success("Correcto", data.result_description);
            jsWebClientPrint.print('idOperacion=' + data.IdOrden + '&tipoOperacion=' + 2);
            $scope.listarPedidos();
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.cerrarConfirmarPedido = function () {
        $scope.facturacionVentaAPI.LimpiarDatosDespuesFacturar();
        document.getElementById("pdfDocumento").innerHTML = '';
        $timeout(function () { $('#pdfDocumento').trigger("change"); }, 100);
    }
    //#endregion

    //#region Invalidar Pedido
    $scope.iniciarInvalidarPedido = function (item) {
        $scope.invalidacion = {};
        $scope.limpiarInvalidarPedido();
        $scope.pedidoAInvalidar = item;
    }

    $scope.invalidarPedido = function () {
        pedidoService.InvalidarPedido({ IdOrdenPedido: $scope.pedidoAInvalidar.Id, Observacion: $scope.invalidacion.Observacion }).success(function (data) {
            $('#modal-invalidar-pedido').modal('hide');
            SweetAlert.success("Correcto", data.result_description);
            $scope.listarPedidos();
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema al traer los pedidos", data.result_description);
        })
    }

    $scope.limpiarInvalidarPedido = function () {
        $scope.invalidacion.Observacion = '';
    }
    //#endregion
});

    ////////#region BUSQUEDA POR CODIGO BARRA DE BALANZA
    //////$scope.buscarConceptoPorCodigoDeBarraBalanza = function () {
    //////    if ($scope.codigo.codigoBarraBalanza == undefined || $scope.codigo.codigoBarraBalanza == '' || $scope.codigo.codigoBarraBalanza.length != 24) {
    //////        SweetAlert.warning("Advertencia", "Ingresar un código de balanza valido");
    //////        $scope.codigo.codigoBarraBalanza = '';
    //////        $scope.focusCodigoBalanza();
    //////        //document.getElementById("idCodigoBarraBalanza").value = '';
    //////    } else {
    //////        let codigoConcepto = $scope.codigo.codigoBarraBalanza.substring(2, 6);
    //////        let cantidad = parseFloat($scope.codigo.codigoBarraBalanza.substring(12, 17)) / 1000;
    //////        let importe = parseFloat($scope.codigo.codigoBarraBalanza.substring(6, 12)) / 100;
    //////        let codigoPuntoVenta = $scope.codigo.codigoBarraBalanza.substring(18, 21);
    //////        let codigoVendedor = $scope.codigo.codigoBarraBalanza.substring(21, 24);
    //////        $scope.codigo.codigoBarraBalanza = '';
    //////        //document.getElementById("idCodigoBarraBalanza").value = '';
    //////        if ($scope.pedido.Orden.Vendedor.Codigo != codigoVendedor && $scope.pedido.Orden.Detalles.length > 0) {
    //////            SweetAlert.warning("Advertencia", "EL VENDEDOR ES DISTINTO AL QUE ESTA REALIZANDO LA VENTA");
    //////        } else {
    //////            $scope.registradorDetallesAPI.AgregarConceptoPorCodigoBarra(codigoConcepto, cantidad, importe);
    //////            //$scope.facturacionVentaAPI.SeleccionarPuntoDeVentaConCodigo(codigoPuntoVenta);----
    //////            //$scope.facturacionVentaAPI.SeleccionarVendedorConCodigo(codigoVendedor);-----
    //////            $scope.verificarInconsistencias();
    //////            /*$timeout(function () { $('#radio-0').trigger("focus"); }, 100);*/
    //////        }
    //////    }
    //////}
    ////////#endregion







    //$scope.agregarDetalle = function () {
    //    var validar = false;
    //    var index = 0;
    //    if ($scope.pedido.Detalles.length > 0) {
    //        for (var i = 0; i < $scope.pedido.Detalles.length; i++) {
    //            if ($scope.detalle.Producto.Id == $scope.pedido.Detalles[i].Producto.Id) {
    //                validar = true;
    //                index = i;
    //                break;
    //            }
    //        }
    //    }
    //    if (validar) {
    //        $scope.agregarCantidad($scope.pedido.Detalles, index);
    //        $scope.detalle = {};
    //    }
    //    else {
    //        if ($scope.detalle.Producto.Precios == undefined || $scope.detalle.Producto.Precios.length == 0) {
    //            SweetAlert.warning("Advertencia", "El concepto " + $scope.detalle.Producto.NombreParaDetalle + " no tiene precios establecidos.");
    //            return;
    //        }
    //        let precio = Enumerable.from($scope.detalle.Producto.Precios).where("$.IdTarifa == '" + $scope.idTarifaSeleccionadoPorDefecto + "'").toArray()[0];
    //        if (precio === undefined) {
    //            SweetAlert.warning("Advertencia", "El concepto " + $scope.detalle.Producto.NombreParaDetalle + " no tiene establecido el precio en la tarifa por defecto. Se seleccionara la tarifa siguiente.");
    //            precio = $scope.detalle.Producto.Precios[0];
    //        }
    //        $('#tarifa-0').select2('data', $scope.detalle.Producto.Precios);
    //        $scope.detalle.PrecioTarifa = precio;
    //        $scope.detalle.PrecioUnitario = parseFloat(precio.Valor).toFixed($scope.numeroDecimalesEnPrecio);
    //        $scope.detalle.Cantidad = 1;
    //        $scope.detalle.Importe = parseFloat($scope.detalle.PrecioUnitario * $scope.detalle.Cantidad).toFixed(2);
    //        $scope.detalle.MascaraDeCalculo = '110';
    //        $scope.detalle.Igv = $scope.pedido.GrabaIgv ? (parseFloat($scope.detalle.Importe - ($scope.detalle.Importe / (1 + $scope.tasaIGV))).toFixed(2)) : 0;
    //        $scope.pedido.Detalles.unshift($scope.detalle);
    //        $timeout(function () { $('#tarifa-0').trigger("change"); }, 100);
    //        $scope.copydetalle = angular.copy($scope.detalle);
    //        $scope.detalle = {};
    //        $(function () {
    //            $('select:not(.normal)').each(function () {
    //                $(this).select2({
    //                    dropdownParent: $(this).parent()
    //                });
    //            });
    //        });
    //    }
    //    $scope.calcularTotal($scope.pedido.Detalles);
    //    $scope.verificarInconsistencias();
    //}
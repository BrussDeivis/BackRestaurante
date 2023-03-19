angular.
    module('app').
    component('registradorNota', {
        templateUrl: "../Scripts/controller/nota/registradorNota/registradorNota.html",
        bindings: {
            api: '=',
            operacion: '=',
            esPropio: '<', //Tendra en cuenta si la nota que se emite es propia o no propia
            idMedioPagoEfectivo: '<',
            permitirVentaCredito: '<',
            cancelarRegistroNota: '&',
            guardadoRegistroNota: '&',
        },
        controller: function ($q, $scope, $timeout, $compile, SweetAlert, ventaService, maestroService, finanzaService) {
            //$('select:not(.normal)').each(function () {
            //    $(this).select2({
            //        dropdownParent: $(this).parent()
            //    });
            //});
            var ctrl = this;

            ctrl.inicializar = function () {
                ctrl.limpiarNota();
                ctrl.cargarParametros().then(function (resultado_) {
                    ctrl.inicioTerminado();
                }, function (error) {
                    ctrl.mensaje = "Se ha producido un error al obtener el dato:" + error;
                });
            };

            ctrl.limpiarNota = function () {
                ctrl.nota = {};
            };

            ctrl.cargarParametros = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                ventaService.obtenerParametrosParaNota({}).success(function (data) {
                    ctrl.parametros = data.data;
                    defered.resolve();
                }).error(function (data) {
                    SweetAlert.error("Ocurrio un Problema", data.error);
                    defered.reject(data);
                });
                return promise;
            };

            ctrl.inicioTerminado = function () {
                ctrl.inicializacionRealizada = true;
            }

            //Registro de notas de operacion

            ctrl.cargarColeccionesNota = function () {
                var defered = $q.defer();
                var promiseList = [];
                promiseList.push(ctrl.obtenerTiposDeNota());
                if (ctrl.esPropio)
                    promiseList.push(ctrl.obtenerTiposComprobantesPropio());
                else
                    promiseList.push(ctrl.obtenerTiposComprobantesNoPropio());
                promiseList.push(ctrl.obtenerDetalleParaNota());
                return $q.all(promiseList).then(function (response) {
                    defered.resolve();
                }).catch(function (error) {
                    defered.reject(e);
                });
            }

            ctrl.obtenerTiposDeNota = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                maestroService.obtenerTiposDeNota({ esParaNotaDeDebito: ctrl.nota.EsDebito }).success(function (data) {
                    ctrl.tiposDeNotas = data;
                    defered.resolve();
                }).error(function (data) {
                    SweetAlert.error2(data);
                    defered.reject(data);
                });
                return promise;
            }

            ctrl.obtenerTiposComprobantesPropio = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                ventaService.obtenerTiposDeComprobanteParaNotaDeDebitoCreditoDeVenta({ esParaNotaDeDebito: ctrl.nota.EsDebito, serieComprobanteVenta: ctrl.operacion.SerieNumeroDocumento }).success(function (data) {
                    ctrl.tiposComprobantes = data;
                    defered.resolve();
                }).error(function (data) {
                    SweetAlert.error2(data);
                    defered.reject(data);
                });
                return promise;
            }

            ctrl.obtenerTiposComprobantesNoPropio = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                compraService.obtenerTiposDeComprobanteParaNotaDeDebitoCreditoDeCompra({ esParaNotaDeDebito: ctrl.nota.EsDebito }).success(function (data) {
                    ctrl.tiposDeComprobantesMasSeriesDeNotas = data;
                    defered.resolve();
                }).error(function (data) {
                    SweetAlert.error2(data);
                    defered.reject(data);
                });
                return promise;
            }

            ctrl.obtenerDetalleParaNota = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                ventaService.obtenerDetalleParaNotaDebitoCredito({ idOrdenVenta: ctrl.operacion.IdOrden }).success(function (data) {
                    ctrl.detallesOperacion = data;
                    defered.resolve();
                }).error(function (data) {
                    SweetAlert.error2(data);
                    defered.reject(data);
                });
                return promise;
            }

            ctrl.establecerDatosPorDefecto = function () {
                ctrl.etiquetaNota = ctrl.nota.EsDebito ? "DÉBITO" : "CRÉDITO";
                ctrl.etiquetaMontoNota = "";
                ctrl.etiquetaDetalleNota = "";
                ctrl.mostrarDetalleNota = false;
                ctrl.mostrarIngresoMonto = false;
                ctrl.mostrarIngresoDetalle = false;
                ctrl.mostrarIngresoDetalleAlmacen = false;
                ctrl.mostrarIngresoDetalleTesoreria = false;
                ctrl.mostrarDetallesOrdenAlmacenNota = false;
                ctrl.mostrarDetallesTesoreriaNota = false;
                ctrl.nota.EsDiferida = 'false';
                ctrl.nota.tasaIGVParaNota = ctrl.operacion.Igv > 0 ? ctrl.parametros.TasaIGV : 0;
                ctrl.nota.Detalles = angular.copy(ctrl.operacion.Detalles);
                ctrl.nota.Detalles.forEach(d => {
                    d.Pendiente = ctrl.detallesOperacion.detallesOrdenAlmacen.find(dd => dd.IdConcepto == d.IdConcepto) == undefined ? 0 : ctrl.detallesOperacion.detallesOrdenAlmacen.find(dd => dd.IdConcepto == d.IdConcepto).Pendiente;
                    d.Entregado = ctrl.detallesOperacion.detallesOrdenAlmacen.find(dd => dd.IdConcepto == d.IdConcepto) == undefined ? 0 : ctrl.detallesOperacion.detallesOrdenAlmacen.find(dd => dd.IdConcepto == d.IdConcepto).Entregado;
                });
                ctrl.operacion.DetallesOrdenAlmacen = angular.copy(ctrl.detallesOperacion.detallesOrdenAlmacen);
                ctrl.operacion.DetallesTesoreria = angular.copy(ctrl.detallesOperacion.detallesTesoreria);
                ctrl.nota.DetallesOrdenAlmacenOriginal = [];
                ctrl.nota.DetallesTesoreriaOriginal = [];
                ctrl.nota.DetallesOrdenAlmacenNota = [];
                ctrl.nota.DetallesTesoreriaNota = [];
                ctrl.nota.HayMovimientoAlmacen = ctrl.operacion.MostrarEntregaAlmacen && ctrl.operacion.HayMovimientoAlmacen && !ctrl.nota.EsDebito;
                ctrl.nota.HayRevocacionAlmacen = !ctrl.operacion.EstadoAlmacenCompletada && !ctrl.nota.EsDebito;
                ctrl.nota.HayMovimientoEconomico = ctrl.operacion.HayMovimientoEconomico;
            };

            ctrl.cargarTipoNota = function () {
                ctrl.etiquetaMontoNota = "";
                ctrl.etiquetaDetalleNota = "";
                ctrl.mostrarDetalleNota = false;
                ctrl.mostrarIngresoMonto = false;
                ctrl.mostrarIngresoDetalle = false;
                ctrl.mostrarIngresoDetalleAlmacen = false;
                ctrl.mostrarIngresoDetalleTesoreria = false;
                ctrl.mostrarDetallesOrdenAlmacenNota = false;
                ctrl.mostrarDetallesTesoreriaNota = false;
                ctrl.nota.DetallesOrdenAlmacenOriginal = [];
                ctrl.nota.DetallesTesoreriaOriginal = [];
                ctrl.nota.DetallesOrdenAlmacenNota = [];
                ctrl.nota.DetallesTesoreriaNota = [];
                ctrl.nota.HayMovimientoAlmacen = false;
                ctrl.nota.HayRevocacionAlmacen = false;
                ctrl.nota.HayMovimientoEconomico = false;
                if (ctrl.nota.TipoNota.Id == ctrl.parametros.IdDetalleMaestroAnulacionDeLaOperacion || ctrl.nota.TipoNota.Id == ctrl.parametros.IdDetalleMaestroDevolucionTotal) {
                    ctrl.mostrarIngresoDetalle = true;
                    ctrl.mostrarDetalleNota = true;
                    ctrl.nota.HayMovimientoAlmacen = ctrl.operacion.MostrarEntregaAlmacen && ctrl.operacion.HayMovimientoAlmacen;
                    ctrl.nota.HayRevocacionAlmacen = !ctrl.operacion.EstadoAlmacenCompletada;
                    //Orden Almacen
                    for (var i = 0; i < ctrl.operacion.DetallesOrdenAlmacen.length; i++) {
                        let detalleOriginal = angular.copy(ctrl.operacion.DetallesOrdenAlmacen[i]);
                        detalleOriginal.Revocado += detalleOriginal.Pendiente;
                        detalleOriginal.Pendiente = detalleOriginal.Ordenado - detalleOriginal.Revocado - detalleOriginal.Entregado;
                        ctrl.nota.DetallesOrdenAlmacenOriginal.push(detalleOriginal);
                        if (ctrl.operacion.DetallesOrdenAlmacen[i].Entregado > 0) {
                            let detalleNota = angular.copy(ctrl.operacion.DetallesOrdenAlmacen[i]);
                            detalleNota.Ordenado = detalleNota.Ordenado;
                            detalleNota.Revocado = detalleNota.Pendiente;
                            detalleNota.Entregado = 0;
                            detalleNota.Pendiente = detalleNota.Ordenado - detalleNota.Revocado - detalleNota.Entregado;
                            ctrl.nota.DetallesOrdenAlmacenNota.push(detalleNota);
                            ctrl.mostrarDetallesOrdenAlmacenNota = true;
                        } else {
                            ctrl.nota.DetallesOrdenAlmacenNota.push(detalleOriginal);
                        }
                    }
                    let valorTotal = ctrl.operacion.Total;
                    //Tesoreria
                    for (var i = ctrl.operacion.DetallesTesoreria.length - 1; i >= 0; i--) {
                        let detalleOriginal = angular.copy(ctrl.operacion.DetallesTesoreria[i]);
                        detalleOriginal.Revocado += parseFloat((valorTotal > detalleOriginal.Saldo) ? detalleOriginal.Saldo : valorTotal);
                        valorTotal = parseFloat(parseFloat(valorTotal - ((valorTotal > detalleOriginal.Saldo) ? detalleOriginal.Saldo : valorTotal)).toFixed(2));
                        detalleOriginal.Saldo = detalleOriginal.Total - detalleOriginal.Revocado - detalleOriginal.Pagado;
                        ctrl.nota.DetallesTesoreriaOriginal.push(detalleOriginal);
                    }
                    if (valorTotal > 0) {
                        let detalleNota = {};
                        detalleNota.Codigo = "C1";
                        detalleNota.Total = valorTotal;
                        detalleNota.Revocado = 0;
                        detalleNota.Pagado = 0;
                        detalleNota.Saldo = detalleNota.Total - detalleNota.Revocado - detalleNota.Pagado;
                        ctrl.nota.DetallesTesoreriaNota.push(detalleNota);
                        ctrl.editorPagoNotaAPI.SetImporteAPagar(detalleNota.Total);
                        ctrl.mostrarDetallesTesoreriaNota = true;
                        ctrl.nota.HayMovimientoEconomico = true;
                    } else {
                        ctrl.mostrarDetallesTesoreriaNota = false;
                        ctrl.nota.HayMovimientoEconomico = false;
                    }
                } else if (ctrl.nota.TipoNota.Id == ctrl.parametros.IdDetalleMaestroDescuentoGlobal) {
                    ctrl.mostrarIngresoMonto = true;
                    ctrl.nota.MontoNota = 0;
                    ctrl.etiquetaMontoNota = "DESCUENTO GLOBAL";
                } else if (ctrl.nota.TipoNota.Id == ctrl.parametros.IdDetalleMaestroInteresesPorMora) {
                    ctrl.mostrarIngresoMonto = true;
                    ctrl.nota.MontoNota = 0;
                    ctrl.nota.HayMovimientoAlmacen = false;
                    ctrl.etiquetaMontoNota = "INTERES TOTAL";
                } else if (ctrl.nota.TipoNota.Id == ctrl.parametros.IdDetalleMaestroDescuentoPorItem) {
                    ctrl.mostrarIngresoDetalle = true;
                    ctrl.mostrarIngresoDetalleTesoreria = true;
                    ctrl.nota.Detalles.forEach(d => {
                        d.MontoDetalle = 0;
                    });
                    ctrl.etiquetaDetalleNota = "DESCUENTO";
                } else if (ctrl.nota.TipoNota.Id == ctrl.parametros.IdDetalleMaestroDevolucionPorItem) {
                    ctrl.mostrarIngresoDetalle = true;
                    ctrl.mostrarIngresoDetalleAlmacen = true;
                    ctrl.nota.Detalles.forEach(d => {
                        d.MontoRevocado = 0;
                        d.IgvRevocado = 0;
                        d.IcbperRevocado = 0;
                        d.ImporteRevocado = 0;
                        d.MontoDevuelto = 0;
                        d.IgvDevuelto = 0;
                        d.IcbperDevuelto = 0;
                        d.ImporteDevuelto = 0;
                    });
                    ctrl.etiquetaDetalleNota = "DEVOLUCION";
                } else if (ctrl.nota.TipoNota.Id == ctrl.parametros.IdDetalleMaestroAumentoEnElValor) {
                    ctrl.mostrarIngresoDetalle = true;
                    ctrl.mostrarIngresoDetalleTesoreria = true;
                    ctrl.nota.Detalles.forEach(d => {
                        d.MontoDetalle = 0;
                    });
                    ctrl.nota.HayMovimientoAlmacen = false;
                    ctrl.etiquetaDetalleNota = "AUMENTO DEL VALOR";
                }
                ctrl.verificarInconsistenciasNota();
            }

            ctrl.calcularTotalNotaMonto = function () {
                ctrl.nota.DetallesOrdenAlmacenOriginal = [];
                ctrl.nota.DetallesTesoreriaOriginal = [];
                ctrl.nota.DetallesOrdenAlmacenNota = [];
                ctrl.nota.DetallesTesoreriaNota = [];
                let valorTotal = parseFloat(ctrl.nota.MontoNota);
                //Orden Almacen
                ctrl.nota.DetallesOrdenAlmacenOriginal = angular.copy(ctrl.operacion.DetallesOrdenAlmacen);
                if (ctrl.nota.TipoNota.Id == ctrl.parametros.IdDetalleMaestroDescuentoGlobal) {
                    //Tesoreria
                    for (var i = ctrl.operacion.DetallesTesoreria.length - 1; i >= 0; i--) {
                        let detalleOriginal = angular.copy(ctrl.operacion.DetallesTesoreria[i]);
                        detalleOriginal.Revocado += parseFloat((valorTotal > detalleOriginal.Saldo) ? detalleOriginal.Saldo : valorTotal);
                        valorTotal = parseFloat(parseFloat(valorTotal - ((valorTotal > detalleOriginal.Saldo) ? detalleOriginal.Saldo : valorTotal)).toFixed(2));
                        detalleOriginal.Saldo = detalleOriginal.Total - detalleOriginal.Revocado - detalleOriginal.Pagado;
                        ctrl.nota.DetallesTesoreriaOriginal.push(detalleOriginal);
                    }
                    if (valorTotal > 0) {
                        let detalleNota = {};
                        detalleNota.Codigo = "C1";
                        detalleNota.Total = valorTotal;
                        detalleNota.Revocado = 0;
                        detalleNota.Pagado = 0;
                        detalleNota.Saldo = detalleNota.Total - detalleNota.Revocado - detalleNota.Pagado;
                        ctrl.nota.DetallesTesoreriaNota.push(detalleNota);
                        ctrl.editorPagoNotaAPI.SetImporteAPagar(detalleNota.Total);
                        ctrl.mostrarDetallesTesoreriaNota = true;
                        ctrl.nota.HayMovimientoEconomico = true;
                    } else {
                        ctrl.mostrarDetallesTesoreriaNota = false;
                        ctrl.nota.HayMovimientoEconomico = false;
                    }
                } else if (ctrl.nota.TipoNota.Id == ctrl.parametros.IdDetalleMaestroInteresesPorMora) {
                    //Tesoreria
                    ctrl.nota.DetallesTesoreriaOriginal = angular.copy(ctrl.operacion.DetallesTesoreria);
                    if (valorTotal > 0) {
                        let detalleNota = {};
                        detalleNota.Codigo = "C1";
                        detalleNota.Total = valorTotal;
                        detalleNota.Revocado = 0;
                        detalleNota.Pagado = 0;
                        detalleNota.Saldo = detalleNota.Total - detalleNota.Revocado - detalleNota.Pagado;
                        ctrl.nota.DetallesTesoreriaNota.push(detalleNota);
                        ctrl.editorPagoNotaAPI.SetImporteAPagar(detalleNota.Total);
                        ctrl.mostrarDetallesTesoreriaNota = true;
                        ctrl.nota.HayMovimientoEconomico = true;
                    } else {
                        ctrl.mostrarDetallesTesoreriaNota = false;
                        ctrl.nota.HayMovimientoEconomico = false;
                    }
                }
                ctrl.verificarInconsistenciasNota();
            }

            ctrl.calcularTotalNotaDetalle = function () {
                ctrl.nota.DetallesOrdenAlmacenOriginal = [];
                ctrl.nota.DetallesTesoreriaOriginal = [];
                ctrl.nota.DetallesOrdenAlmacenNota = [];
                ctrl.nota.DetallesTesoreriaNota = [];
                let valorTotal = 0;
                if (ctrl.nota.TipoNota.Id == ctrl.parametros.IdDetalleMaestroDevolucionPorItem) {
                    ctrl.nota.Detalles.forEach(d => {
                        d.MontoRevocadoText = d.MontoRevocado;
                        d.MontoDevueltoText = d.MontoDevuelto;
                    });
                    ctrl.nota.Detalles.forEach(d => {
                        d.MontoRevocado = parseFloat(d.MontoRevocado == '' ? 0 : d.MontoRevocado);
                        d.ImporteRevocado = d.Precio * d.MontoRevocado;
                        d.IgvRevocado = d.ImporteRevocado - (d.ImporteRevocado / (1 + ctrl.nota.tasaIGVParaNota));
                        d.IcbperRevocado = d.MontoRevocado * d.ValorIcbper;
                        d.MontoDevuelto = parseFloat(d.MontoDevuelto == '' ? 0 : d.MontoDevuelto);
                        d.ImporteDevuelto = d.Precio * d.MontoDevuelto;
                        d.IgvDevuelto = d.ImporteDevuelto - (d.ImporteDevuelto / (1 + ctrl.nota.tasaIGVParaNota));
                        d.IcbperDevuelto = d.MontoDevuelto * d.ValorIcbper;
                    });
                    ctrl.nota.Detalles.forEach(d => {
                        valorTotal += parseFloat(d.ImporteRevocado + d.IcbperRevocado + d.ImporteDevuelto + d.IcbperDevuelto);
                    });
                    //Orden Almacen
                    for (var i = 0; i < ctrl.nota.Detalles.length; i++) {
                        let detalleOriginal = angular.copy(ctrl.operacion.DetallesOrdenAlmacen.find(d => d.IdConcepto == ctrl.nota.Detalles[i].IdConcepto));
                        detalleOriginal.Revocado += ctrl.nota.Detalles[i].MontoRevocado;
                        detalleOriginal.Pendiente = detalleOriginal.Ordenado - detalleOriginal.Revocado - detalleOriginal.Entregado;
                        ctrl.nota.DetallesOrdenAlmacenOriginal.push(detalleOriginal);
                        ctrl.nota.HayRevocacionAlmacen = ctrl.nota.HayRevocacionAlmacen || (ctrl.nota.Detalles[i].MontoRevocado > 0);
                        ctrl.nota.HayMovimientoAlmacen = ctrl.nota.HayMovimientoAlmacen || (ctrl.nota.Detalles[i].MontoDevuelto > 0);
                        if ((ctrl.nota.Detalles[i].MontoRevocado + ctrl.nota.Detalles[i].MontoDevuelto) > 0) {
                            let detalleNota = angular.copy(ctrl.operacion.DetallesOrdenAlmacen.find(d => d.IdConcepto == ctrl.nota.Detalles[i].IdConcepto));
                            detalleNota.Ordenado = ctrl.nota.Detalles[i].MontoRevocado + ctrl.nota.Detalles[i].MontoDevuelto;
                            detalleNota.Revocado = ctrl.nota.Detalles[i].MontoRevocado;
                            detalleNota.Entregado = 0;
                            detalleNota.Pendiente = ctrl.nota.Detalles[i].MontoDevuelto;
                            ctrl.nota.DetallesOrdenAlmacenNota.push(detalleNota);
                            ctrl.mostrarDetallesOrdenAlmacenNota = true;
                        }
                    }
                    //Tesoreria
                    for (var i = ctrl.operacion.DetallesTesoreria.length - 1; i >= 0; i--) {
                        let detalleOriginal = angular.copy(ctrl.operacion.DetallesTesoreria[i]);
                        detalleOriginal.Revocado += parseFloat((valorTotal > detalleOriginal.Saldo) ? detalleOriginal.Saldo : valorTotal);
                        valorTotal = parseFloat(parseFloat(valorTotal - ((valorTotal > detalleOriginal.Saldo) ? detalleOriginal.Saldo : valorTotal)).toFixed(2));
                        detalleOriginal.Saldo = detalleOriginal.Total - detalleOriginal.Revocado - detalleOriginal.Pagado;
                        ctrl.nota.DetallesTesoreriaOriginal.push(detalleOriginal);
                    }
                    if (valorTotal > 0) {
                        let detalleNota = {};
                        detalleNota.Codigo = "C1";
                        detalleNota.Total = valorTotal;
                        detalleNota.Revocado = 0;
                        detalleNota.Pagado = 0;
                        detalleNota.Saldo = detalleNota.Total - detalleNota.Revocado - detalleNota.Pagado;
                        ctrl.nota.DetallesTesoreriaNota.push(detalleNota);
                        ctrl.editorPagoNotaAPI.SetImporteAPagar(detalleNota.Total);
                        ctrl.mostrarDetallesTesoreriaNota = true;
                        ctrl.nota.HayMovimientoEconomico = true;
                    } else {
                        ctrl.mostrarDetallesTesoreriaNota = false;
                        ctrl.nota.HayMovimientoEconomico = false;
                    }
                    ctrl.nota.Detalles.forEach(d => {
                        d.MontoRevocado = d.MontoRevocadoText;
                        d.MontoDevuelto = d.MontoDevueltoText;
                    });
                } else if (ctrl.nota.TipoNota.Id == ctrl.parametros.IdDetalleMaestroDescuentoPorItem) {
                    ctrl.nota.Detalles.forEach(d => {
                        d.Igv = parseFloat(d.MontoDetalle) - (parseFloat(d.MontoDetalle) / (1 + ctrl.nota.tasaIGVParaNota));
                    });
                    ctrl.nota.Detalles.forEach(d => {
                        valorTotal += parseFloat(d.MontoDetalle == '' ? 0 : d.MontoDetalle)
                    });
                    //Orden Almacen
                    ctrl.nota.DetallesOrdenAlmacenOriginal = angular.copy(ctrl.operacion.DetallesOrdenAlmacen);
                    //Tesoreria
                    for (var i = ctrl.operacion.DetallesTesoreria.length - 1; i >= 0; i--) {
                        let detalleOriginal = angular.copy(ctrl.operacion.DetallesTesoreria[i]);
                        detalleOriginal.Revocado += parseFloat((valorTotal > detalleOriginal.Saldo) ? detalleOriginal.Saldo : valorTotal);
                        valorTotal = parseFloat(parseFloat(valorTotal - ((valorTotal > detalleOriginal.Saldo) ? detalleOriginal.Saldo : valorTotal)).toFixed(2));
                        detalleOriginal.Saldo = detalleOriginal.Total - detalleOriginal.Revocado - detalleOriginal.Pagado;
                        ctrl.nota.DetallesTesoreriaOriginal.push(detalleOriginal);
                    }
                    if (valorTotal > 0) {
                        let detalleNota = {};
                        detalleNota.Codigo = "C1";
                        detalleNota.Total = valorTotal;
                        detalleNota.Revocado = 0;
                        detalleNota.Pagado = 0;
                        detalleNota.Saldo = detalleNota.Total - detalleNota.Revocado - detalleNota.Pagado;
                        ctrl.nota.DetallesTesoreriaNota.push(detalleNota);
                        ctrl.editorPagoNotaAPI.SetImporteAPagar(detalleNota.Total);
                        ctrl.mostrarDetallesTesoreriaNota = true;
                        ctrl.nota.HayMovimientoEconomico = true;
                    } else {
                        ctrl.mostrarDetallesTesoreriaNota = false;
                        ctrl.nota.HayMovimientoEconomico = false;
                    }
                } else if (ctrl.nota.TipoNota.Id == ctrl.parametros.IdDetalleMaestroAumentoEnElValor) {
                    ctrl.nota.Detalles.forEach(d => {
                        d.Igv = parseFloat(d.MontoDetalle) - (parseFloat(d.MontoDetalle) / (1 + ctrl.nota.tasaIGVParaNota));
                    });
                    ctrl.nota.Detalles.forEach(d => {
                        valorTotal += parseFloat(d.MontoDetalle == '' ? 0 : d.MontoDetalle)
                    });
                    //Orden Almacen
                    ctrl.nota.DetallesOrdenAlmacenOriginal = angular.copy(ctrl.operacion.DetallesOrdenAlmacen);
                    //Tesoreria
                    ctrl.nota.DetallesTesoreriaOriginal = angular.copy(ctrl.operacion.DetallesTesoreria);
                    if (valorTotal > 0) {
                        let detalleNota = {};
                        detalleNota.Codigo = "C1";
                        detalleNota.Total = valorTotal;
                        detalleNota.Revocado = 0;
                        detalleNota.Pagado = 0;
                        detalleNota.Saldo = detalleNota.Total - detalleNota.Revocado - detalleNota.Pagado;
                        ctrl.nota.DetallesTesoreriaNota.push(detalleNota);
                        ctrl.editorPagoNotaAPI.SetImporteAPagar(detalleNota.Total);
                        ctrl.mostrarDetallesTesoreriaNota = true;
                        ctrl.nota.HayMovimientoEconomico = true;
                    } else {
                        ctrl.mostrarDetallesTesoreriaNota = false;
                        ctrl.nota.HayMovimientoEconomico = false;
                    }
                }
                ctrl.verificarInconsistenciasNota();
            }

            ctrl.formatoDosDecimales = function (event) {
                let valor = event.target.value == '' ? 0 : event.target.value;
                event.target.value = parseFloat(valor).toFixed(2);
            }

            ctrl.formatoCantidadDecimales = function (event) {
                let valor = event.target.value == '' ? 0 : event.target.value;
                event.target.value = parseFloat(valor).toFixed(ctrl.parametros.NumeroDecimalesEnCantidad);
            }

            ctrl.cargarSeries = function () {
                if (ctrl.nota.Comprobante !== null) {
                    ctrl.series = angular.copy(ctrl.nota.Comprobante.Series);
                    ctrl.nota.Comprobante.SerieSeleccionada = ctrl.series[0].Id;
                }
                ctrl.verificarInconsistenciasNota();
            }

            ctrl.guardarNota = function () {
                if (ctrl.esPropio) {
                    ventaService.guardarNotaDeDebitoCreditoDeVenta({ registroDeNota: ctrl.nota }).success(function (data) {
                        SweetAlert.success("Correcto", data.result_description);
                        ctrl.guardadoRegistroNota({ idOrdenNota: data.data });
                        ctrl.limpiarNota();
                    }).error(function (data) {
                        SweetAlert.error2(data);
                    });
                }
            }

            ctrl.cancelarNota = function () {
                ctrl.limpiarNota();
                ctrl.cancelarRegistroNota();
            }

            ctrl.limpiarDetalleDeNota = function () {
                for (var i = 0; i < ctrl.nota.Detalles.length; i++) {
                    if (ctrl.nota.TipoNota.Id == ctrl.parametros.IdDetalleMaestroCorreccionPorErrorEnLaDescripcion) {
                        ctrl.nota.Detalles[i].ValorDeDetalle = '';
                    } else {
                        ctrl.nota.Detalles[i].ValorDeDetalle = parseFloat(0).toFixed(ctrl.numeroDecimalesEnValorDeDetalle);
                        ctrl.nota.Detalles[i].ValorDeDetalleIgv = parseFloat(0).toFixed(2);
                    }
                }
            }

            ctrl.calcularTotalDeNota = function () {
                var totalDeNota = 0;
                for (var i = 0; i < ctrl.nota.Detalles.length; i++) {
                    if (ctrl.nota.Detalles[i].ValorDeDetalle != undefined) {
                        if (ctrl.nota.TipoNota.Id == ctrl.parametros.IdDetalleMaestroDevolucionPorItem) {
                            ctrl.nota.Detalles[i].ValorDeDetalle = ctrl.nota.Detalles[i].ValorDeDetalle == '' ? 0 : ctrl.nota.Detalles[i].ValorDeDetalle;
                            var totalNotaItem = parseFloat(ctrl.nota.Detalles[i].Precio) * parseFloat(ctrl.nota.Detalles[i].ValorDeDetalle);
                            totalDeNota += totalNotaItem;
                            ctrl.nota.Detalles[i].ValorDeDetalleIgv = (totalNotaItem - (totalNotaItem / (1 + parseFloat(ctrl.tasaIGVParaNota))));
                        } else if (ctrl.nota.TipoNota.Id == ctrl.parametros.IdDetalleMaestroCorreccionPorErrorEnLaDescripcion) {
                            if (ctrl.nota.Detalles[i].ValorDeDetalle != '')
                                totalDeNota += parseFloat(ctrl.nota.Detalles[i].Importe);
                            ctrl.nota.Detalles[i].ValorDeDetalleIgv = ctrl.nota.Detalles[i].Igv;
                        } else {
                            ctrl.nota.Detalles[i].ValorDeDetalle = ctrl.nota.Detalles[i].ValorDeDetalle == '' ? 0 : ctrl.nota.Detalles[i].ValorDeDetalle;
                            totalDeNota += parseFloat(ctrl.nota.Detalles[i].ValorDeDetalle);
                            ctrl.nota.Detalles[i].ValorDeDetalleIgv = (parseFloat(ctrl.nota.Detalles[i].ValorDeDetalle) - (parseFloat(ctrl.nota.Detalles[i].ValorDeDetalle) / (1 + parseFloat(ctrl.tasaIGVParaNota))));
                        }
                    }
                }
                ctrl.totalDeNota = totalDeNota;
                ctrl.igvDeNota = (totalDeNota - (totalDeNota / (1 + parseFloat(ctrl.tasaIGVParaNota))));
                ctrl.subTotalDeNota = parseFloat(ctrl.totalDeNota) - parseFloat(ctrl.igvDeNota);
            }

            ctrl.calcularTotalDeNotaMonto = function () {
                if (ctrl.nota.TipoNota.Id == ctrl.parametros.IdDetalleMaestroAnulacionDeLaOperacion || ctrl.nota.TipoNota.Id == ctrl.parametros.IdDetalleMaestroDevolucionTotal) {
                    ctrl.totalDeNota = ctrl.operacion.Total;
                    ctrl.igvDeNota = ctrl.operacion.Igv;
                    ctrl.subTotalDeNota = parseFloat(ctrl.totalDeNota) - parseFloat(ctrl.igvDeNota);
                } else if (ctrl.nota.TipoNota.Id == ctrl.parametros.IdDetalleMaestroDescuentoGlobal || ctrl.nota.TipoNota.Id == ctrl.parametros.IdDetalleMaestroInteresesPorMora) {
                    ctrl.nota.MontoNota = (ctrl.nota.MontoNota == '') ? 0 : ctrl.nota.MontoNota;
                    ctrl.totalDeNota = ctrl.nota.MontoNota;
                    ctrl.igvDeNota = (ctrl.totalDeNota - (ctrl.totalDeNota / (1 + parseFloat(ctrl.tasaIGVParaNota))));
                    ctrl.subTotalDeNota = parseFloat(ctrl.totalDeNota) - parseFloat(ctrl.igvDeNota);
                    if (parseFloat(ctrl.nota.MontoNota) > parseFloat(ctrl.operacion.Total)) {
                        ctrl.inconsistenciasNota.push("Es necesario que el monto de nota sea menor al total.");
                    }
                } else {
                    ctrl.totalDeNota = 0;
                    ctrl.igvDeNota = 0;
                    ctrl.subTotalDeNota = 0;
                }
            }

            ctrl.verificarInconsistenciasNota = function () {
                ctrl.inconsistenciasNota = [];
                if (ctrl.nota.Observacion == "" || ctrl.nota.Observacion == null || ctrl.nota.Observacion == undefined) {
                    ctrl.inconsistenciasNota.push("Es necesario ingresar el motivo de la nota.");
                }
                if (ctrl.nota.Comprobante == undefined) {
                    ctrl.inconsistenciasNota.push("Es necesario seleccionar un documento.");
                } else {
                    if (ctrl.nota.Comprobante.EsPropio == false) {
                        if (ctrl.nota.Comprobante.SerieIngresada == "" || ctrl.nota.Comprobante.SerieIngresada == null) {
                            ctrl.inconsistenciasNota.push("Es necesario ingresar la serie de comprobante.");
                        } if (ctrl.nota.Comprobante.NumeroIngresado == "" || ctrl.nota.Comprobante.NumeroIngresado == null) {
                            ctrl.inconsistenciasNota.push("Es necesario ingresar el numero de comprobante.");
                        }
                    }
                }
                if (ctrl.nota.TipoNota == undefined) {
                    ctrl.inconsistenciasNota.push("Es necesario seleccionar el tipo de nota.");
                } else {
                    let valorTotal = 0;
                    if (ctrl.nota.TipoNota.Id == ctrl.parametros.IdDetalleMaestroDescuentoGlobal) {
                        if (ctrl.nota.MontoNota == "" || ctrl.nota.MontoNota == 0) {
                            ctrl.inconsistenciasNota.push("Es necesario ingresar al valor de descuento de la nota.");
                        }
                        if (parseFloat(ctrl.nota.MontoNota) > parseFloat(ctrl.operacion.Total)) {
                            ctrl.inconsistenciasNota.push("Es necesario que el monto de nota sea menor al total.");
                        }
                    } else if (ctrl.nota.TipoNota.Id == ctrl.parametros.IdDetalleMaestroInteresesPorMora) {
                        if (ctrl.nota.MontoNota == "" || ctrl.nota.MontoNota == 0) {
                            ctrl.inconsistenciasNota.push("Es necesario ingresar al valor de interes de la nota.");
                        }
                    } else if (ctrl.nota.TipoNota.Id == ctrl.parametros.IdDetalleMaestroDescuentoPorItem) {
                        ctrl.nota.Detalles.forEach(d => {
                            valorTotal += parseFloat(d.MontoDetalle);
                        });
                        if (valorTotal == 0) {
                            ctrl.inconsistenciasNota.push("Es necesario ingresar al menos un descuento de la nota.");
                        } else {
                            for (var i = 0; i < ctrl.nota.Detalles.length; i++) {
                                if (ctrl.nota.Detalles[i].MontoDetalle == '' && parseFloat(ctrl.nota.Detalles[i].MontoDetalle) != 0) {
                                    ctrl.inconsistenciasNota.push("Es necesario ingresar un descuento valido.");
                                    break;
                                }
                                if (parseFloat(ctrl.nota.Detalles[i].MontoDetalle) > parseFloat(ctrl.nota.Detalles[i].Importe)) {
                                    ctrl.inconsistenciasNota.push("Es necesario que el descuento sea menor a la importe.");
                                    break;
                                }
                            }
                        }
                    } else if (ctrl.nota.TipoNota.Id == ctrl.parametros.IdDetalleMaestroAumentoEnElValor) {
                        ctrl.nota.Detalles.forEach(d => {
                            valorTotal += parseFloat(d.MontoDetalle);
                        });
                        if (valorTotal == 0) {
                            ctrl.inconsistenciasNota.push("Es necesario ingresar al menos un aumento de valor de la nota.");
                        }
                    } else if (ctrl.nota.TipoNota.Id == ctrl.parametros.IdDetalleMaestroDevolucionPorItem) {
                        ctrl.nota.Detalles.forEach(d => {
                            valorTotal += parseFloat(d.ImporteRevocado + d.ImporteDevuelto);
                        });
                        if (valorTotal == 0) {
                            ctrl.inconsistenciasNota.push("Es necesario ingresar al menos una devolucion de la nota.");
                        } else {
                            for (var i = 0; i < ctrl.nota.Detalles.length; i++) {
                                if (parseFloat(ctrl.nota.Detalles[i].MontoRevocado) > parseFloat(ctrl.nota.Detalles[i].Pendiente)) {
                                    ctrl.inconsistenciasNota.push("Es necesario que la cantidad a revocar sea menor a la cantidad pendiente.");
                                    break;
                                }
                                if (parseFloat(ctrl.nota.Detalles[i].MontoDevuelto) > parseFloat(parseFloat(ctrl.nota.Detalles[i].Entregado) - parseFloat(ctrl.nota.DetallesOrdenAlmacenOriginal.find(d => d.IdConcepto == ctrl.nota.Detalles[i].IdConcepto).Devuelto))) {
                                    ctrl.inconsistenciasNota.push("Es necesario que la cantidad a devolver sea menor a la cantidad entregada.");
                                    break;
                                }
                            }
                        }
                    }
                    if (ctrl.operacion.EstaPagadoConPuntos) {
                        if (ctrl.nota.TipoNota.Id != ctrl.parametros.IdDetalleMaestroAnulacionDeLaOperacion) {
                            ctrl.inconsistenciasNota.push("Es necesario anular la operacion, por el manejo de puntos en el pago.");
                        }
                    }
                }
            }


            ctrl.iniciarNotaDebito = function () {
                ctrl.limpiarNota();
                ctrl.nota.EsDebito = true;
                ctrl.nota.IdOrdenDeOperacion = ctrl.operacion.IdOrden;
                ctrl.cargarInformacionPagoNota();
                ctrl.cargarColeccionesNota().then(function (resultado_) {
                    ctrl.establecerDatosPorDefecto();
                    ctrl.verificarInconsistenciasNota();
                    ctrl.cargarComponentePagoNota();
                }, function (error) {
                    ctrl.mensaje = "Se ha producido un error al obtener el dato:" + error;
                });
            }

            ctrl.iniciarNotaCredito = function () {
                ctrl.limpiarNota();
                ctrl.nota.EsDebito = false;
                ctrl.nota.IdOrdenDeOperacion = ctrl.operacion.IdOrden;
                ctrl.cargarInformacionPagoNota();
                ctrl.cargarColeccionesNota().then(function (resultado_) {
                    ctrl.establecerDatosPorDefecto();
                    ctrl.verificarInconsistenciasNota();
                    ctrl.cargarComponentePagoNota();
                }, function (error) {
                    ctrl.mensaje = "Se ha producido un error al obtener el dato:" + error;
                });
            }

            ctrl.cargarInformacionPagoNota = function () {
                ctrl.nota.Pago = { ModoDePago: 1, Traza: { MedioDePago: {}, Info: { EntidadFinanciera: {}, OperadorTarjeta: {}, ImporteAPagar: ctrl.operacion.TotalMovimientoEconomico } }, Caja: {}, Cajero: {}, Total: ctrl.operacion.TotalMovimientoEconomico };
            }

            ctrl.cargarComponentePagoNota = function () {
                ctrl.editorPagoNotaAPI.RestablecerInfoPago();
                ctrl.editorPagoNotaAPI.VerificarMediosDePagoAMostrar(ctrl.operacion.IdCliente);
                ctrl.editorPagoNotaAPI.SetImporteAPagar(ctrl.operacion.TotalMovimientoEconomico);
            }

            ctrl.cambioPago = function (pago) {
                ctrl.nota.Pago = pago;
            }

            ctrl.inicioRealizadoPago = function () {

            }

            ctrl.$onInit = function () {
                ctrl.api = {};
                ctrl.api.IniciarNotaDebito = ctrl.iniciarNotaDebito;
                ctrl.api.IniciarNotaCredito = ctrl.iniciarNotaCredito;
                ctrl.inicializar();
            };
        }
    });
angular.
    module('app').
    component('registradorDetalles', {
        templateUrl: "../Scripts/controller/venta/registradorDetalles/registradorDetalles.html",
        bindings: {
            api: '=',
            operacion: '=',
            permitirRegistroFlete: '<',
            cursorPorDefectoCodigoBarra: '<',
            cambiarImporteTotal: '&',
            validarRegistradorDetalles: '&',
            inicioRealizado: '&',
            seleccionarCodigoBalanza: '&',
            setFocusSerieComprobante: '&'
        },

        controller: function ($q, $compile, $scope, $parse, $timeout, SweetAlert, ventaService, conceptoService) {
            $('select:not(.normal)').each(function () {
                $(this).select2({
                    dropdownParent: $(this).parent()
                });
            });
            var ctrl = this;

            ctrl.cambioTotal = function () {
                ctrl.cambiarImporteTotal({ importeTotal: ctrl.operacion.Total });
            }

            ctrl.focusCodigoBalanza = function () {
                ctrl.seleccionarCodigoBalanza({});
            };

            ctrl.establecerFocusSerieComprobante = function () {
                ctrl.setFocusSerieComprobante({});
            };


            ctrl.inicioTerminado = function () {
                ctrl.inicioRealizado();
            };

            ctrl.inicializar = function () {
                ctrl.limpiarRegistroDetalle();
                ctrl.cargarParametros().then(function () {
                    ctrl.inicializarComponentes();
                    ctrl.inicioTerminado();
                }, function (error) {
                    SweetAlert.error("Ocurrio un Problema", error);
                });
            };

            ctrl.limpiarRegistroDetalle = function () {
                ctrl.detalle = {};
            }

            ctrl.cargarParametros = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                ventaService.obtenerParametrosParaRegistroDetalles({}).success(function (data) {
                    ctrl.parametros = data.data;
                    defered.resolve();
                }).error(function (data) {
                    ctrl.messageError(data.error);
                    defered.reject(data);
                });
                return promise;
            };

            ctrl.inicializarComponentes = function () {
                ctrl.inicializacionRealizada = true;
            }

            ctrl.seleccionConcepto = function (conceptoComercial) {
                ctrl.detalle.Producto = angular.copy(conceptoComercial);
                ctrl.agregarDetalle();
            }

            ctrl.calcularDesdeTarifa = function (detalles, index) {
                detalles[index].PrecioUnitario = parseFloat(detalles[index].PrecioTarifa.Valor).toFixed(ctrl.parametros.NumeroDecimalesEnPrecio);
                ctrl.calcularValoresDetalle(2, detalles, index);
            }

            ctrl.establecerFocusDesdeImporte = function () {
                ctrl.parametros.FlujoDespuesDeImporteEnVenta == 1 ? (ctrl.cursorPorDefectoCodigoBarra ? ctrl.selectorConceptoComercialAPI.FocusNextCodigoBarra() : ctrl.focusCodigoBalanza()) : ctrl.setFocusSerieComprobante({});
            };

            ctrl.calcularValoresDetalle = function (identificador, detalles, index) {
                //detalles[index].Cantidad = (detalles[index].Cantidad == '' || detalles[index].Cantidad == null) ? 0 : detalles[index].Cantidad = detalles[index].Cantidad;
                //detalles[index].PrecioUnitario = detalles[index].PrecioUnitario == '' ? 0 : detalles[index].PrecioUnitario = detalles[index].PrecioUnitario;
                //detalles[index].Importe = detalles[index].Importe == '' ? 0 : detalles[index].Importe = detalles[index].Importe;
                if (identificador == 1)//Cantidad
                {
                    ctrl.cambiarMascaraDeCalculo(detalles, index, 0, '1');
                    if (ctrl.parametros.IngresarCantidadCalcularPrecioUnitario) {
                        detalles[index].PrecioUnitario = parseFloat(detalles[index].Importe / detalles[index].Cantidad).toFixed(ctrl.parametros.NumeroDecimalesEnPrecio);
                        ctrl.cambiarMascaraDeCalculo(detalles, index, 1, '0');
                    }
                    else {
                        detalles[index].Importe = parseFloat(detalles[index].PrecioUnitario * detalles[index].Cantidad).toFixed(2);
                        ctrl.cambiarMascaraDeCalculo(detalles, index, 2, '0');
                    }
                }
                if (identificador == 2)//Precio Unitario
                {
                    ctrl.cambiarMascaraDeCalculo(detalles, index, 1, '1');
                    if (ctrl.parametros.IngresarPrecioUnitarioCalcularImporte) {
                        detalles[index].Importe = parseFloat(detalles[index].PrecioUnitario * detalles[index].Cantidad).toFixed(2);
                        ctrl.cambiarMascaraDeCalculo(detalles, index, 2, '0');
                    }
                    else {
                        detalles[index].Cantidad = parseFloat(detalles[index].Importe / detalles[index].PrecioUnitario).toFixed(ctrl.parametros.NumeroDecimalesEnCantidad);
                        ctrl.cambiarMascaraDeCalculo(detalles, index, 0, '0');
                    }
                }
                if (identificador == 3)//Importe
                {
                    ctrl.cambiarMascaraDeCalculo(detalles, index, 2, '1');
                    if (ctrl.parametros.IngresarImporteCalcularCantidad) {
                        detalles[index].Cantidad = parseFloat(detalles[index].Importe / detalles[index].PrecioUnitario).toFixed(ctrl.parametros.NumeroDecimalesEnCantidad);
                        ctrl.cambiarMascaraDeCalculo(detalles, index, 0, '0');
                    }
                    else {
                        detalles[index].PrecioUnitario = parseFloat(detalles[index].Importe / detalles[index].Cantidad).toFixed(ctrl.parametros.NumeroDecimalesEnPrecio);
                        ctrl.cambiarMascaraDeCalculo(detalles, index, 1, '0');
                    }
                }
                //Verificar que la cantidad sea menor que el stock
                if (detalles[index].EsBien && ctrl.parametros.SalidaBienesSujetasADisponibilidadStock && detalles[index].Cantidad > detalles[index].Producto.Stock) {
                    SweetAlert.warning("Advertencia", "El Stock de " + detalles[index].Producto.NombreDetalle + " es " + detalles[index].Producto.Stock);
                    detalles[index].Cantidad = detalles[index].Producto.Stock;
                }
                //Calcular el numero de bolsas de plastico
                ctrl.calcularNumeroDeBolsasPlasticas(detalles);
                detalles[index].Igv = (ctrl.operacion.AplicarIGVCuandoEsAmazonia || !ctrl.parametros.AplicaLeyAmazonia) ? parseFloat(detalles[index].Importe - (detalles[index].Importe / (1 + ctrl.parametros.TasaIGV))).toFixed(2) : 0;
                ctrl.calcularTotal(detalles);
            };

            ctrl.cambiarMascaraDeCalculo = function (detalles, index, campo, valor) {
                let mascaraDeCalculoArray = detalles[index].MascaraDeCalculo.split('');
                mascaraDeCalculoArray[campo] = valor;
                detalles[index].MascaraDeCalculo = mascaraDeCalculoArray.join('');
            };

            ctrl.cambioDeGrabarIgv = function (aplicarIGVCuandoEsAmazonia) {
                for (i = 0; i < ctrl.operacion.Detalles.length; i++) {
                    if (aplicarIGVCuandoEsAmazonia) {
                        ctrl.operacion.Detalles[i].Igv = parseFloat(parseFloat(ctrl.operacion.Detalles[i].Importe) - (ctrl.operacion.Detalles[i].Importe / (1 + ctrl.parametros.TasaIGV))).toFixed(2);
                    } else {
                        ctrl.operacion.Detalles[i].Igv = 0;
                    }
                }
                ctrl.calcularTotal(ctrl.operacion.Detalles);
                return ctrl.operacion;
            };

            ctrl.agregarDetalle = function () {
                var validar = false;
                var index = 0;
                if (ctrl.detalle.Producto.EsBien && ctrl.parametros.SalidaBienesSujetasADisponibilidadStock && (!ctrl.detalle.Producto.Stock || ctrl.detalle.Producto.Stock <= 0)) {
                    SweetAlert.warning("Advertencia", "El Stock de " + ctrl.detalle.Producto.NombreDetalle + " es " + "0");
                } else {
                    if (ctrl.operacion.Detalles.length > 0) {
                        for (var i = 0; i < ctrl.operacion.Detalles.length; i++) {
                            if (ctrl.detalle.Producto.Id == ctrl.operacion.Detalles[i].Producto.Id) {
                                validar = true;
                                index = i;
                                break;
                            }
                        }
                    }
                    if (validar) {
                        ctrl.agregarCantidad(ctrl.operacion.Detalles, index);
                        ctrl.detalle = {};
                    }
                    else {
                        if (ctrl.detalle.Producto.EsBien) {
                            ctrl.detalle.Cantidad = ctrl.parametros.AplicarCantidadPorDefecto ? (ctrl.detalle.Producto.Stock > parseInt(ctrl.parametros.CantidadPorDefecto) || !ctrl.parametros.SalidaBienesSujetasADisponibilidadStock) ? parseInt(ctrl.parametros.CantidadPorDefecto) : ctrl.detalle.Producto.Stock : "";
                        } else {
                            ctrl.detalle.Cantidad = ctrl.parametros.AplicarCantidadPorDefecto || !ctrl.parametros.SalidaBienesSujetasADisponibilidadStock ? parseInt(ctrl.parametros.CantidadPorDefecto) : 1;
                        }

                        if (ctrl.detalle.Producto.IdFamilia == ctrl.parametros.IdFamiliaBolsaPlastica) {
                            ctrl.operacion.NumeroBolsasDePlastico += ctrl.detalle.Cantidad;
                        }
                        if (ctrl.detalle.Producto.Precios == undefined || ctrl.detalle.Producto.Precios.length == 0) {
                            SweetAlert.warning("Advertencia", "El concepto " + ctrl.detalle.Producto.NombreDetalle + " no tiene precios establecidos.");
                            return;
                        }
                        var precio = Enumerable.from(ctrl.detalle.Producto.Precios).where("$.IdTarifa == '" + ctrl.parametros.IdTarifaSeleccionadoPorDefecto + "'").toArray()[0];
                        if (precio == undefined) {
                            SweetAlert.warning("Advertencia", "El concepto " + ctrl.detalle.Producto.NombreDetalle + " no tiene establecido el precio en la tarifa por defecto. Se seleccionara la tarifa siguiente.");
                            precio = ctrl.detalle.Producto.Precios[0];
                        }
                        ctrl.detalle.PrecioUnitario = parseFloat(precio.Valor).toFixed(ctrl.parametros.NumeroDecimalesEnPrecio);
                        ctrl.detalle.PrecioCalculadoOperacion = false;
                        ctrl.detalle.VersionFila = ctrl.detalle.Producto.VersionFila;
                        ctrl.detalle.PrecioTarifa = precio;
                        //$timeout(function () { $('#tarifa-0').trigger("change"); }, 100);
                        ctrl.detalle.Descuento = 0;
                        ctrl.detalle.Importe = parseFloat(parseFloat(ctrl.detalle.PrecioUnitario * ctrl.detalle.Cantidad) - ctrl.detalle.Descuento).toFixed(2);
                        ctrl.detalle.MascaraDeCalculo = ctrl.parametros.MascaraDeCalculoPorDefecto;
                        ctrl.detalle.Igv = (ctrl.operacion.AplicarIGVCuandoEsAmazonia || !ctrl.parametros.AplicaLeyAmazonia) ? parseFloat(ctrl.detalle.Importe - (ctrl.detalle.Importe / (1 + ctrl.parametros.TasaIGV))).toFixed(2) : 0;
                        ctrl.operacion.Detalles.unshift(ctrl.detalle);
                        ctrl.detalle = {};
                    }
                    ctrl.parametros.FlujoDespuesDeCodigoBarraEnVenta == 1 ? (ctrl.cursorPorDefectoCodigoBarra ? ctrl.selectorConceptoComercialAPI.FocusNextCodigoBarra() : ctrl.focusCodigoBalanza()) : ctrl.focusSelectNext('cantidad-0');
                    ctrl.calcularNumeroDeBolsasPlasticas(ctrl.operacion.Detalles);
                    ctrl.calcularTotal(ctrl.operacion.Detalles);
                }
            };

            ctrl.formatoDecimalCantidad = function (event) {
                let valor = event.target.value;
                event.target.value = parseFloat(valor == '' ? 0 : valor).toFixed(ctrl.parametros.NumeroDecimalesEnCantidad);
            }

            ctrl.formatoDecimalPrecioUnitario = function (event) {
                let valor = event.target.value;
                event.target.value = parseFloat(valor == '' ? 0 : valor).toFixed(ctrl.parametros.NumeroDecimalesEnPrecio);
            }

            ctrl.formatoDosDecimales = function (event) {
                let valor = event.target.value;
                event.target.value = parseFloat(valor == '' ? 0 : valor).toFixed(2);
            }

            ctrl.agregarCantidad = function (detalles, index) {
                detalles[index].Cantidad++;
                detalles[index].Importe = parseFloat((detalles[index].Cantidad * detalles[index].PrecioUnitario) - detalles[index].Descuento).toFixed(2);
                ctrl.verificarStockYCalcularDetalle(detalles, index);
                ctrl.calcularNumeroDeBolsasPlasticas(detalles);
                ctrl.calcularTotal(detalles);
            };

            ctrl.quitarDetalle = function (index) {
                if (ctrl.operacion.Detalles[index].Producto.IdFamilia == ctrl.parametros.IdFamiliaBolsaPlastica) {
                    ctrl.operacion.NumeroBolsasDePlastico -= ctrl.operacion.Detalles[index].Cantidad;
                }
                ctrl.operacion.Detalles.splice(index, 1);
                ctrl.calcularNumeroDeBolsasPlasticas(ctrl.operacion.Detalles);
                ctrl.calcularTotal(ctrl.operacion.Detalles);
            };

            ctrl.calcularNumeroDeBolsasPlasticas = function (detalles) {
                ctrl.operacion.NumeroBolsasDePlastico = 0;
                for (var i = 0; i < detalles.length; i++) {
                    if (detalles[i].Producto.IdFamilia == ctrl.parametros.IdFamiliaBolsaPlastica) {
                        ctrl.operacion.NumeroBolsasDePlastico += detalles[i].Cantidad;
                    }
                }
            };

            ctrl.verificarStockYCalcularDetalle = function (detalles, index) {
                if (detalles[index].EsBien && ctrl.parametros.SalidaBienesSujetasADisponibilidadStock && detalles[index].Cantidad > detalles[index].Producto.Stock) {
                    SweetAlert.warning("Advertencia", "El Stock de " + detalles[index].Producto.NombreDetalle + " es " + detalles[index].Producto.Stock);
                    detalles[index].Cantidad = detalles[index].Producto.Stock;
                }
                ctrl.calcularNumeroDeBolsasPlasticas(detalles);
                ctrl.calcularValoresDetalle(1, detalles, index);
            };

            ctrl.calcularTotal = function (detalles) {
                if (ctrl.operacion.Flete > 0) {
                    ctrl.operacion.Total = parseFloat(ctrl.operacion.Flete);
                    ctrl.operacion.Igv = (ctrl.operacion.AplicarIGVCuandoEsAmazonia || !ctrl.parametros.AplicaLeyAmazonia) ? parseFloat(ctrl.operacion.Flete - (ctrl.operacion.Flete / (1 + ctrl.parametros.TasaIGV))) : 0;
                    ctrl.operacion.SubTotal = ctrl.operacion.Total - ctrl.operacion.Igv;
                } else {
                    ctrl.operacion.SubTotal = 0;
                    ctrl.operacion.Igv = 0;
                    ctrl.operacion.Total = 0;
                }
                ctrl.operacion.Descuento = 0;
                for (i = 0; i < detalles.length; i++) {
                    ctrl.operacion.Total += parseFloat(detalles[i].Importe == '' ? 0 : detalles[i].Importe);
                    ctrl.operacion.Descuento += parseFloat(detalles[i].Descuento);
                    ctrl.operacion.Igv += parseFloat(detalles[i].Igv);
                }
                ctrl.operacion.SubTotal = parseFloat(ctrl.operacion.Total - ctrl.operacion.Igv).toFixed(2);
                ctrl.operacion.Icbper = parseFloat(ctrl.operacion.NumeroBolsasDePlastico * ctrl.parametros.CostoUnitarioDelIcbper).toFixed(2);
                ctrl.operacion.Total = parseFloat(ctrl.operacion.Total + parseFloat(ctrl.operacion.Icbper)).toFixed(2);
                ctrl.validarDetalles(detalles);
                ctrl.cambioTotal();
                ctrl.validarRegistradorDetalles();
            };

            ctrl.validarDetalles = function (detalles) {
                let detallesConBienes = false;
                for (i = 0; i < detalles.length; i++) {
                    detallesConBienes = detallesConBienes || detalles[i].Producto.EsBien;
                }
                ctrl.operacion.HayBienesEnLosDetalles = detallesConBienes;
            };

            ctrl.establecerDatosDetalles = function () {
                ctrl.limpiarRegistroDetalle();
                ctrl.establecerDatosPorDefecto();
            };

            ctrl.agregarConceptoPorCodigoBarra = function (codigoBarra, cantidad, importe) {
                conceptoService.obtenerConceptoDeNegocioComercialPorCodigoBarra({ codigoBarra: parseInt(codigoBarra), complementoStock: true, complementoPrecio: true, modoSeleccionTipoFamilia: ctrl.parametros.ModoSeleccionTipoFamilia }).success(function (data) {
                    let conceptoComercial = data;
                    let detallesOperacion = Enumerable.from(ctrl.operacion.Detalles).where("$.Producto.Id == '" + conceptoComercial.Id + "'").toArray();
                    let detalleOperacion = undefined;
                    if (detallesOperacion.length > 0) { detalleOperacion = Enumerable.from(detallesOperacion).where("$.PrecioUnitario == '" + parseFloat(parseFloat(importe) / parseFloat(cantidad)).toFixed(ctrl.numeroDecimalesEnPrecio) + "'").toArray()[0]; }
                    if (detalleOperacion != undefined) {
                        detalleOperacion.Cantidad = detalleOperacion.Cantidad + cantidad;
                        detalleOperacion.Importe = parseFloat(parseFloat(detalleOperacion.Importe) + importe).toFixed(2);
                        detalleOperacion.PrecioUnitario = parseFloat(detalleOperacion.Importe / detalleOperacion.Cantidad).toFixed(ctrl.numeroDecimalesEnPrecio);
                        detalleOperacion.MascaraDeCalculo = ctrl.parametros.MascaraDeCalculoPrecioUnitarioCalculado;
                        detalleOperacion.Igv = (ctrl.operacion.AplicarIGVCuandoEsAmazonia || !ctrl.parametros.AplicaLeyAmazonia) ? parseFloat(detalleOperacion.Importe - (detalleOperacion.Importe / (1 + ctrl.parametros.TasaIGV))).toFixed(2) : 0;
                        detalleOperacion.Descuento = 0;
                    } else {
                        if (detallesOperacion.length > 0) {
                            var detallesDeOperacion = angular.copy(ctrl.operacion.Detalles);
                            ctrl.operacion.Detalles = [];
                        }
                        ctrl.detalle.Producto = conceptoComercial;
                        ctrl.agregarDetalle();
                        let detalleNuevo = Enumerable.from(ctrl.operacion.Detalles).where("$.Producto.Id == '" + conceptoComercial.Id + "'").toArray()[0];
                        detalleNuevo.Cantidad = cantidad;
                        detalleNuevo.Importe = parseFloat(importe).toFixed(2);
                        detalleNuevo.PrecioUnitario = parseFloat(detalleNuevo.Importe / detalleNuevo.Cantidad).toFixed(ctrl.numeroDecimalesEnPrecio);
                        detalleNuevo.MascaraDeCalculo = ctrl.parametros.MascaraDeCalculoPrecioUnitarioCalculado;
                        detalleNuevo.Igv = (ctrl.operacion.AplicarIGVCuandoEsAmazonia || !ctrl.parametros.AplicaLeyAmazonia) ? parseFloat(ctrl.detalleNuevo.Importe - (ctrl.detalleNuevo.Importe / (1 + ctrl.parametros.TasaIGV))).toFixed(2) : 0;
                        detalleNuevo.Descuento = 0;
                        if (detallesOperacion.length > 0) {
                            for (var i = 0; i < detallesDeOperacion.length; i++) {
                                ctrl.operacion.Detalles.push(detallesDeOperacion[i]);
                            }
                        }
                    }
                    ctrl.calcularTotal(ctrl.operacion.Detalles);
                }).error(function (data) {
                    if (data.warning) SweetAlert.warning("Advertencia", data.error);
                    else SweetAlert.error2(data);
                });
            }

            ctrl.cargarDetallesOperacionAPregenerar = function (operacionAPregenerar) {
                for (var i = operacionAPregenerar.Detalles.length - 1; i >= 0; i--) {
                    ctrl.detalle.Producto = operacionAPregenerar.Detalles[i].ConceptoCompleto;
                    ctrl.agregarDetalle();
                }
                for (var i = 0; i < ctrl.operacion.Detalles.length; i++) {
                    let detalleOperacion = Enumerable.from(operacionAPregenerar.Detalles).where("$.Producto.Id == '" + ctrl.operacion.Detalles[i].Producto.Id + "'").toArray()[0]
                    ctrl.operacion.Detalles[i].PrecioUnitario = detalleOperacion.PrecioUnitario;
                    ctrl.operacion.Detalles[i].Cantidad = detalleOperacion.Cantidad;
                    ctrl.operacion.Detalles[i].Importe = detalleOperacion.Importe.toFixed(2);
                    ctrl.operacion.Detalles[i].Igv = detalleOperacion.Igv.toFixed(2);
                }
                ctrl.calcularTotal(ctrl.operacion.Detalles);
            }

            ctrl.cargarDetallesOperacion = function (operacion) {
                for (var i = operacion.Detalles.length - 1; i >= 0; i--) {
                    ctrl.detalle.Producto = operacion.Detalles[i].Producto;
                    ctrl.agregarDetalle();
                }
                for (var i = 0; i < ctrl.operacion.Detalles.length; i++) {
                    let detalleOperacion = Enumerable.from(operacion.Detalles).where("$.Producto.Id == '" + ctrl.operacion.Detalles[i].Producto.Id + "'").toArray()[0]
                    ctrl.operacion.Detalles[i].PrecioUnitario = detalleOperacion.PrecioUnitario;
                    ctrl.operacion.Detalles[i].Cantidad = detalleOperacion.Cantidad;
                    ctrl.operacion.Detalles[i].Importe = detalleOperacion.Importe.toFixed(2);
                    ctrl.operacion.Detalles[i].Igv = detalleOperacion.Igv.toFixed(2);
                }
                ctrl.calcularTotal(ctrl.operacion.Detalles);
            }

            ctrl.focusSelectNext = function (id) {
                $timeout(function () {
                    $('#' + id).trigger("focus");
                }, 100);
                $timeout(function () {
                    $('#' + id).trigger("select");
                }, 100);
            };

            ctrl.focusNextCodigoBarra = function () {
                ctrl.selectorConceptoComercialAPI.FocusNextCodigoBarra()
            };

            ctrl.salidaBienesSujetasAStock = function () {
                return ctrl.parametros.SalidaBienesSujetasADisponibilidadStock;
            };

            ctrl.limpiarDetallesOperacion = function () {
                ctrl.operacion.Detalles = [];
                ctrl.operacion.SubTotal = 0;
                ctrl.operacion.Igv = 0;
                ctrl.operacion.Icbper = 0;
                ctrl.operacion.Total = 0;
            };

            ctrl.agregarConceptoServicio = function (conceptoServicio) {
                ctrl.selectorConceptoComercialAPI.AgregarNuevaFamiliaConcepto(conceptoServicio);
                ctrl.detalle.Producto = conceptoServicio.ConceptoComercial;
                ctrl.agregarDetalle();
            };

            ctrl.recargarConceptos = function () {
                ctrl.selectorConceptoComercialAPI.RecargarConceptos();
            }


            this.$onInit = function () {
                ctrl.api = {};
                ctrl.inicializar();
                ctrl.api.EstablecerDatosDetalles = ctrl.establecerDatosDetalles;
                ctrl.api.AgregarConceptoPorCodigoBarra = ctrl.agregarConceptoPorCodigoBarra;
                ctrl.api.AgregarConceptoServicio = ctrl.agregarConceptoServicio;
                ctrl.api.CargarDetallesOperacionAPregenerar = ctrl.cargarDetallesOperacionAPregenerar;
                ctrl.api.FocusNextCodigoBarra = ctrl.focusNextCodigoBarra;
                ctrl.api.LimpiarDetallesOperacion = ctrl.limpiarDetallesOperacion;
                ctrl.api.CambioDeGrabarIgv = ctrl.cambioDeGrabarIgv;
                ctrl.api.SalidaBienesSujetasAStock = ctrl.salidaBienesSujetasAStock;
                ctrl.api.RecargarConceptos = ctrl.recargarConceptos;
                ctrl.api.CargarDetallesOperacion = ctrl.cargarDetallesOperacion;
            };
        }
    });
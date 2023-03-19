angular.
    module('app').
    component('selectorConceptoComercial', {
        templateUrl: "../Scripts/controller/concepto/selectorConceptoComercial/selectorConceptoComercial.html",
        bindings: {
            api: '=',//Api para ejecutar metodos desde fuera el componente
            conceptoComercial: '=',//Concepto comercial que se devolvera para quien use el componente
            modoSeleccionTipoFamilia: '<',//Mostrar la seleccion del tipo de familia (bien, servicio, ambos)
            mostrarBuscadorCodigoBarra: '<',//Mostrar o no el buscador de codigo de barra
            modoSeleccionConcepto: '<',//Modo de como te mostrara el selector de conceptos, bien un combobox o dos o selector con filtros
            complementoStock: '<',//Variable bool que va a decir si se obtiene el stock en los complementos
            complementoPrecio: '<',//Variable bool que va a decir si se obtiene los precios en los complementos
            minimoCaracteresBuscarConcepto: '<',//Minimo de caracteres para la busqueda del concepto
            tiempoEsperaBusquedaSelector: '<',//Tiempo de espera para realizar la busqueda automatica
            informacionAMostrar: '<',//Informacion para mostrar en el selector de concepto 1: nombre, 2: nombre + precio + stock
            changed: '&'//Funcion de cambio que se ejecutara al cambiar el selectro de concepto
        },

        controller: function ($q, $http, $scope, $timeout, $compile, SweetAlert, conceptoService, maestroService, ventaService) {
            $('select:not(.normal)').each(function () {
                $(this).select2({
                    dropdownParent: $(this).parent()
                });
            });
            var ctrl = this;

            ctrl.conceptoSeleccionado = function () {
                ctrl.changed({ conceptoComercial: ctrl.selector.Concepto });
                ctrl.selector.Concepto = {};
            };

            ctrl.inicializar = function () {
                ctrl.selector = {};
                ctrl.selector.BuscadorConceptoComercial = false;
                ctrl.establecerVistaModoSelector();
                ctrl.establecerDatosIniciales();
                //ctrl.focusNextCodigoBarra();
            };

            ctrl.establecerVistaModoSelector = function () {
                if (ctrl.modoSeleccionConcepto == 4) {//Modo de seleccion un selector de conceptos simple
                    if (ctrl.mostrarBuscadorCodigoBarra) {
                        ctrl.classParaCodigoBarra = 'col-md-3';
                        ctrl.classParaConcepto = 'col-md-9 no-padding-left';
                    } else {
                        ctrl.classParaConcepto = 'col-md-12';
                    }
                } else if (ctrl.modoSeleccionConcepto == 1) {//Modo de seleccion un selector de conceptos personalizado
                    ctrl.selector.BuscadorConceptoComercial = true;
                    if (ctrl.mostrarBuscadorCodigoBarra) {
                        ctrl.classParaCodigoBarra = 'col-md-3';
                        ctrl.classParaConcepto = 'col-md-9 no-padding-left';
                    } else {
                        ctrl.classParaConcepto = 'col-md-12';
                    }
                } else if (ctrl.modoSeleccionConcepto == 2) {//Modo de seleccion 2 selectores uno de familias y el otro de conceptos
                    if (ctrl.mostrarBuscadorCodigoBarra) {
                        ctrl.classParaCodigoBarra = 'col-md-3';
                        ctrl.classParaFamilia = 'col-md-3 no-padding-left';
                        ctrl.classParaConcepto = 'col-md-6 no-padding-left';
                    } else {
                        ctrl.classParaFamilia = 'col-md-3';
                        ctrl.classParaConcepto = 'col-md-9 no-padding-left';
                    }
                } else {//Modo de seleccion buscador por nombre y caracteristicas
                    if (ctrl.mostrarBuscadorCodigoBarra) {
                        ctrl.classParaCodigoBarra = 'col-md-3';
                        ctrl.classParaBuscadorConcepto = 'col-md-9 no-padding-left';
                    } else {
                        ctrl.classParaBuscadorConcepto = 'col-md-12';
                    }
                }
            };

            ctrl.establecerDatosIniciales = function () {
                if (ctrl.modoSeleccionConcepto == 4) {//Modo de seleccion un selector de conceptos pocos
                    ctrl.obtenerConceptos();
                    //} else if (ctrl.modoSeleccionConcepto == 2) {//Modo de seleccion un selector de conceptos muchos
                    //    ctrl.obtenerConceptosModoSelector2();
                } else if (ctrl.modoSeleccionConcepto == 2) {//Modo de seleccion 2 selectores uno de familias y el otro de conceptos
                    ctrl.obtenerFamilias();
                }
            };

            ctrl.buscarConceptoPorCodigoDeBarra = function () {
                conceptoService.obtenerConceptoDeNegocioComercialPorCodigoBarra({ codigoBarra: ctrl.selector.CodigoBarraABuscar, complementoStock: ctrl.complementoStock, complementoPrecio: ctrl.complementoPrecio, modoSeleccionTipoFamilia: ctrl.modoSeleccionTipoFamilia }).success(function (data) {
                    ctrl.selector.Concepto = data;
                    ctrl.selector.CodigoBarraABuscar = '';
                    ctrl.conceptoSeleccionado();
                }).error(function (data) {
                    if (data.warning) SweetAlert.warning("Advertencia", data.error);
                    else SweetAlert.error2(data);
                    ctrl.selector.CodigoBarraABuscar = '';
                });
            };

            ctrl.obtenerFamilias = function () {
                maestroService.obtenerFamiliasConceptosComercialesVigentesPorModoSeleccionTipoFamilia({ modoSeleccionTipoFamilia: ctrl.modoSeleccionTipoFamilia }).success(function (data) {
                    ctrl.familias = data;
                }).error(function (data) {
                    SweetAlert.error2(data);
                });
            };

            ctrl.obtenerConceptosPorFamilia = function (idFamilia) {
                conceptoService.obtenerConceptosDeNegociosComercialesPorFamilia({ idFamilia: idFamilia, informacionSelectorConcepto: ctrl.informacionAMostrar }).success(function (data) {
                    ctrl.conceptosPorFamilia = data;
                }).error(function (data) {
                    SweetAlert.error2(data);
                });
            };

            ctrl.obtenerConceptos = function () {
                conceptoService.obtenerConceptosDeNegociosComerciales({ modoSeleccionTipoFamilia: ctrl.modoSeleccionTipoFamilia, informacionSelectorConcepto: ctrl.informacionAMostrar }).success(function (data) {
                    ctrl.conceptos = data;
                }).error(function (data) {
                    SweetAlert.error2(data);
                });
            }

            ctrl.cargarConceptosPorBusqueda = function (query) {
                if (ctrl.modoSeleccionTipoFamilia != undefined) {
                    var reqStr = URL_ + "/Concepto/ObtenerConceptosDeNegociosComercialesPorBusquedaConcepto";
                    $http.post(reqStr, { cadenaBusqueda: query, modoSeleccionTipoFamilia: ctrl.modoSeleccionTipoFamilia, informacionSelectorConcepto: ctrl.informacionAMostrar }).then(
                        function (response) {
                            ctrl.conceptosPorBusqueda = response.data;
                        },
                        function () {
                            SweetAlert.error("Ocurrio un Problema", response.error);
                        }
                    );
                }

            }

            ctrl.limpiarSeleccionConcepto = function () {
                ctrl.selector.BuscadorConceptoComercial = true;
                ctrl.conceptosPorBusqueda = undefined;
                ctrl.selector.CadenaBusquedaConcepto = '';
            }

            ctrl.seleccionarConcepto = function () {
                if (ctrl.selector.Concepto != undefined) {
                    conceptoService.obtenerConceptoDeNegocioComercialPorIdConcepto({ idConceptoNegocio: ctrl.selector.Concepto.Id, complementoStock: ctrl.complementoStock, complementoPrecio: ctrl.complementoPrecio }).success(function (data) {
                        ctrl.selector.Concepto = data;
                        ctrl.conceptoSeleccionado();
                        if (ctrl.modoSeleccionConcepto == 2) {
                            ctrl.limpiarSeleccionConcepto();
                        }
                    }).error(function (data) {
                        SweetAlert.error2(data);
                    });
                }
            };

            ctrl.obtenerConceptosDeNegociosComercialesParaVentaPorNombre = function () {
                conceptoService.obtenerConceptosDeNegociosComercialesParaVentaPorNombre({ nombre: ctrl.selector.NombreABuscar }).success(function (data) {
                    if (data.data == 0) {
                        SweetAlert.warning("Advertencia", data.data_description);
                    }
                    else {
                        ctrl.listaConceptosNegociosComercialesPorNombre = data.ConceptosNegociosComerciales;
                        ctrl.FiltrosConOpciones = data.FiltrosConOpciones;
                        ctrl.inicializarFiltroDeConceptoDeNegociosComerciales();
                    }
                }).error(function (data) {
                    SweetAlert.error2(data);
                });
            };

            ctrl.seleccionarConceptoModoBusquedaNombre = function (item) {
                ctrl.selector.Concepto = item;
                ctrl.conceptoSeleccionado();
            };

            ctrl.inicializarFiltroDeConceptoDeNegociosComerciales = function () {
                ctrl.venta.DesplegarAcordion = true;
                ctrl.tmpListaConceptosNegociosComercialesPorNombre = angular.copy(ctrl.listaConceptosNegociosComercialesPorNombre);
                ctrl.tmpFiltrosConOpciones = angular.copy(ctrl.FiltrosConOpciones);
            };

            ctrl.filtrarConceptoNegociosComercialesSegunElValorDeCaracteristica = function () {
                // De los filtro que obtuve, hay algunas opciones que estan seleccionadas de esas opciones realizar el filtro, recorrer cuales fueron seleccionados
                var valoresFiltrosSeleccionados = [];
                for (var i = 0; i < ctrl.tmpFiltrosConOpciones.length; i++) {
                    let filtro = ctrl.tmpFiltrosConOpciones[i];
                    for (var j = 0; j < filtro.Opciones.length; j++) {
                        let opcion = filtro.Opciones[j];
                        if (opcion.EsSeleccionado) {
                            valoresFiltrosSeleccionados.push(opcion.Valor);
                        }
                    }
                }
                if (valoresFiltrosSeleccionados.length > 0) {
                    //Buscar aquellos conceptos negocios comerciales que tengan ese valor
                    var productos = [];
                    for (var k = 0; k < ctrl.listaConceptosNegociosComercialesPorNombre.length; k++) {
                        var producto = ctrl.listaConceptosNegociosComercialesPorNombre[k];
                        var cantidadEncontradaDeValores = 0;
                        for (var l = 0; l < producto.Filtros.length; l++) {
                            let filtro = producto.Filtros[l];
                            for (var n = 0; n < valoresFiltrosSeleccionados.length; n++) {
                                let valorFiltroSeleccionado = valoresFiltrosSeleccionados[n];
                                if (filtro.Valor === valorFiltroSeleccionado) {
                                    cantidadEncontradaDeValores++;
                                    //Si la cantidad encontrada de valores es igual a lo que fue seleccionado agregar el producto al arreglo
                                    if (cantidadEncontradaDeValores === valoresFiltrosSeleccionados.length) {
                                        productos.push(producto);
                                    }
                                }
                            }
                        }
                    }

                    ctrl.tmpListaConceptosNegociosComercialesPorNombre = angular.copy(productos);
                    //Nuevos nombres nombres y valores de caracteristica
                    var filtros = [];
                    _.each(ctrl.tmpListaConceptosNegociosComercialesPorNombre, function (conceptoNegocioComercial) {
                        _.each(conceptoNegocioComercial.Filtros, function (nombreFiltro) {
                            //Preguntar si ya existe el nombre del filtro
                            var filtroExistente = _.findWhere(filtros, { Nombre: nombreFiltro.Nombre });
                            //Existe el nombre del filtro
                            if (filtroExistente) {
                                //Preguntar si ya existe la propiedad
                                var opcionExistente = _.findWhere(filtroExistente.Opciones, { Valor: nombreFiltro.Valor });
                                //Existe la opcion
                                if (opcionExistente) {
                                    //Si existe la opcion aumentar el contador
                                    opcionExistente.Cantidad += 1;
                                } else {
                                    //Si no existe opcion agregar esa opcion y poner que existe una sola
                                    //filtrar las opciones que fueron selecionados
                                    let valorEsSeleccionado = valoresFiltrosSeleccionados.filter(valor => valor === nombreFiltro.Valor).length > 0;
                                    if (valorEsSeleccionado) {
                                        filtroExistente.Opciones.push({ Valor: nombreFiltro.Valor, Cantidad: 1, EsSeleccionado: true });
                                    } else {
                                        filtroExistente.Opciones.push({ Valor: nombreFiltro.Valor, Cantidad: 1 });
                                    }
                                }
                            } else {
                                // Si no existe el filtro crearlo
                                var filtro = {};
                                filtro.Nombre = nombreFiltro.Nombre;
                                filtro.Opciones = [];
                                let valorEsSeleccionado = valoresFiltrosSeleccionados.filter(valor => valor === nombreFiltro.Valor).length > 0;
                                if (valorEsSeleccionado) {
                                    filtro.Opciones.push({ Valor: nombreFiltro.Valor, Cantidad: 1, EsSeleccionado: true });
                                } else {
                                    filtro.Opciones.push({ Valor: nombreFiltro.Valor, Cantidad: 1 });
                                }
                                filtros.push(filtro);
                            }
                            if (filtroExistente && filtroExistente.Opciones.length > 0) {
                                filtroExistente.Opciones = filtroExistente.Opciones.sort(function (a, b) {
                                    if (a.Valor < b.Valor) { return -1; }
                                    if (a.Valor > b.Valor) { return 1; }
                                    return 0;
                                });
                            }
                        });
                    });
                    filtros.sort(function (a, b) {
                        if (a.Nombre < b.Nombre) { return -1; }
                        if (a.Nombre > b.Nombre) { return 1; }
                        return 0;
                    });
                    ctrl.tmpFiltrosConOpciones = angular.copy(filtros);
                } else {
                    ctrl.reinicializarFiltros();
                }
            };

            ctrl.reinicializarFiltros = function () {
                ctrl.tmpListaConceptosNegociosComercialesPorNombre = angular.copy(ctrl.listaConceptosNegociosComercialesPorNombre);
                ctrl.tmpFiltrosConOpciones = angular.copy(ctrl.FiltrosConOpciones);
            };

            ctrl.agregarNuevaFamilia = function (familia) {
                ctrl.familias.push(familia);
            };

            ctrl.agregarNuevoConcepto = function (concepto) {
                ctrl.conceptos.push(concepto);
            };

            ctrl.agregarNuevaFamiliaConcepto = function (conceptoServicio) {
                if (ctrl.modoSeleccionConcepto == 4) {
                    ctrl.conceptos.push(conceptoServicio.Concepto);
                }
                if (ctrl.modoSeleccionConcepto == 2) {
                    if (conceptoServicio.ConceptoBasicoSeleccionado) {
                        if (ctrl.selector.FamiliaSeleccionada.Id == conceptoServicio.Familia.Id) {
                            ctrl.conceptosPorFamilia.push(conceptoServicio.Concepto);
                        }
                    } else {
                        ctrl.familias.push(conceptoServicio.Familia);
                    }
                }
            };

            ctrl.focusNextCodigoBarra = function () {
                $timeout(function () {
                    $('#idCodigoBarra').trigger("focus");
                }, 100);
            };

            ctrl.agregarFamiliaIngresada = function (familia) {
                ctrl.familiaAgregada = { Id: familia.Id, Nombre: familia.Nombre, EsBien: familia.EsBien };
                if (ctrl.modoSeleccionConcepto == 2) {
                    ctrl.familias.unshift(ctrl.familiaAgregada);
                    ctrl.selector.FamiliaSeleccionada = ctrl.ConceptoBasicoAgregado;
                }
            };

            ctrl.agregarConceptoIngresado = function (idConcepto, nombreConcepto, esBien) {
                ctrl.conceptoAgregado = { Id: idConcepto, Nombre: nombreConcepto, EsBien: esBien };
                if (ctrl.modoSeleccionConcepto == 4) {
                    ctrl.conceptos.unshift(ctrl.conceptoAgregado);
                }
                ctrl.selector.Concepto = ctrl.conceptoAgregado;
                ctrl.seleccionarConcepto();
            }

            ctrl.recargarConceptos = function () {
                if (ctrl.informacionAMostrar == 2) {//Informacion a mostrar es Concepto + Stock + Precio
                    if (ctrl.modoSeleccionConcepto == 4) {//Modo de seleccion un selector de conceptos pocos
                        ctrl.obtenerConceptos();
                    } else if (ctrl.modoSeleccionConcepto == 2) {//Modo de seleccion 2 selectores uno de familias y el otro de conceptos
                        if (ctrl.selector.FamiliaSeleccionada != undefined || ctrl.selector.FamiliaSeleccionada != null)
                            ctrl.obtenerConceptosPorFamilia(ctrl.selector.FamiliaSeleccionada.Id)
                    }
                }
            };

            this.$onInit = function () {
                ctrl.api = {};
                ctrl.inicializar();
                ctrl.api.LimpiarSelectorFamilia = ctrl.limpiarSelectorFamilia;
                ctrl.api.AgregarNuevaFamilia = ctrl.agregarNuevaFamilia;
                ctrl.api.AgregarNuevoConcepto = ctrl.agregarNuevoConcepto;
                ctrl.api.AgregarNuevaFamiliaConcepto = ctrl.agregarNuevaFamiliaConcepto;
                ctrl.api.FocusNextCodigoBarra = ctrl.focusNextCodigoBarra;
                ctrl.api.AgregarFamiliaIngresada = ctrl.agregarFamiliaIngresada;
                ctrl.api.AgregarConceptoIngresado = ctrl.agregarConceptoIngresado;
                ctrl.api.RecargarConceptos = ctrl.recargarConceptos;

            };
        }
    });
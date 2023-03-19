angular.
    module('app').
    component('facturadorRestaurante', {
        templateUrl: "../Scripts/controller/restaurante/facturadorRestaurante/facturadorRestaurante.html",
        bindings: {
            api: '=',
            atencion: '=',
            cerrarFacturacion: '&'
        },

        controller: function ($q, $compile, $scope, $parse, $timeout, SweetAlert, ventaService, clienteService, restauranteService) {
            var ctrl = this;
            $('select:not(.normal)').each(function () {
                $(this).select2({
                    dropdownParent: $(this).parent()
                });
            });
            var scope = $scope.$root;

            ctrl.closeFacturacion = function (seFacturo) {
                $('#modal-facturador-restaurante').modal('hide');
                ctrl.cerrarFacturacion({ seFacturo: seFacturo });
            };

            //Variables

            ctrl.arrayDeCarrilesDiferenciado = [{ monto: 0, detalleOrden: [] }, { monto: 0, detalleOrden: [] }];
            ctrl.arrayDeCarrilesDividido = [];
            ctrl.carril = {};
            ctrl.detalleOrden = {};
            ctrl.seFacturo = false;
            ctrl.detalleUnificado = true;
            ctrl.tamanioComprobante = '80mm';
            ctrl.atencion = { Comprobantes: [] };



            ctrl.inicializar = function () {
                ctrl.limpiar();
                ctrl.cargarColeccionesAsync();
                ctrl.cargarColeccionesSync().then(function (resultado_) {
                    ctrl.establecerDatosPorDefecto();
                    ctrl.atencion.TipoDePago = ctrl.parametros.TipoDePagoSimple;
                }, function (error) {
                    ctrl.mensaje = "Se ha producido un error al obtener el dato:" + error;
                });

            };

            ctrl.limpiar = function () {
                ctrl.atencion = {};
                ctrl.atencion.esValido = false;
            };

            ctrl.cargarParametros = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                restauranteService.obtenerConfiguracionRestauranteFacturacion({}).success(function (data) {
                    ctrl.parametros = data.data;
                    ctrl.inicializacionRealizada = true;
                    defered.resolve();
                }).error(function (data) {
                    SweetAlert.error("Ocurrio un Problema", data.error);
                    defered.reject(data);
                });
                return promise;
            };

            ctrl.cargarColeccionesAsync = function () {
            };

            ctrl.cargarColeccionesSync = function () {
                var defered = $q.defer();
                var promiseList = [];
                promiseList.push(ctrl.cargarParametros());
                return $q.all(promiseList).then(function (response) {
                    defered.resolve();
                }).catch(function (error) {
                    defered.reject(e);
                });
            };

            ctrl.establecerDatosPorDefecto = function () {
            };

            //#region SELECCIONAR RADIO FACTURACION 
            ctrl.cargarAtencionEnComprobantes = function () {
                ctrl.seFacturo = false;
                ctrl.seleccionarFacturacionSimple();
            };
            ctrl.limpiarTiposFacturacion = () => {

                ctrl.formato = {};
                ctrl.detalleUnificado = true;
                ctrl.arrayDeCarrilesDividido = [];
                ctrl.atencion.Comprobantes = [];
                ctrl.arrayDeCarrilesDiferenciado = [];


            }
            ctrl.seleccionarFacturacionSimple = function () {
                ctrl.limpiarTiposFacturacion();
                ctrl.atencion.TipoDePago = ctrl.parametros.TipoDePagoSimple;
                ctrl.inicializarFacturacionSimple();
            };

            ctrl.seleccionarFacturacionDividido = function () {
                ctrl.limpiarTiposFacturacion();
                ctrl.detalleUnificado = false;

                ctrl.atencion.TipoDePago = ctrl.parametros.TipoDePagoCuentaDividida;
                ctrl.inicializarFacturacionDividido();
                /*ctrl.iniciarComprobantesFacturacion();*/

            };

            ctrl.seleccionarFacturacionDiferenciado = function () {
                ctrl.limpiarTiposFacturacion();
                ctrl.atencion.TipoDePago = ctrl.parametros.TipoDePagoCuentaDiferenciadaDetallada;
                ctrl.inicializarFacturacionDiferenciado();


            };
            //#endregion

            //#region FACTURACION 


            ctrl.setAtencionAFacturar = function (atencion) {
                ctrl.atencion = atencion;
                //seleccionar solo los detalle de ordenes Atendidas 
                ctrl.atencion.Ordenes = ctrl.atencion.Ordenes.filter(ctrl.ordenesAtendidas);
                for (let i = 0; i < ctrl.atencion.Ordenes.length; i++) {
                    ctrl.atencion.Ordenes[i].DetallesDeOrden = ctrl.atencion.Ordenes[i].DetallesDeOrden.filter(ctrl.detallesDeOrdenAtendidas)
                }
                for (let i = 0; i < ctrl.atencion.Ordenes.length; i++) {
                    ctrl.atencion.Ordenes[i].DetallesFacturacionDiferenciada = [];
                    for (let j = 0; j < ctrl.atencion.Ordenes[i].DetallesDeOrden.length; j++) {
                        for (let k = 0; k < ctrl.atencion.Ordenes[i].DetallesDeOrden[j].Cantidad; k++) {
                            let detalleDiferenciado = angular.copy(ctrl.atencion.Ordenes[i].DetallesDeOrden[j]);
                            detalleDiferenciado.Cantidad = 1;
                            detalleDiferenciado.Importe = detalleDiferenciado.Cantidad * detalleDiferenciado.Precio;
                            ctrl.atencion.Ordenes[i].DetallesFacturacionDiferenciada.push(detalleDiferenciado);
                        }
                    }
                }
                ctrl.cargarAtencionEnComprobantes();
            };
            ctrl.detallesDeOrdenAtendidas = (detalleOrden) => {
                return detalleOrden.Estado === ctrl.parametros.ConfiguracionDetallesDeOrden.EstadoAtendido
            }
            ctrl.ordenesAtendidas = (orden) => {
                return orden.ImporteOrden > 0;
            }



            ctrl.agregarNuevoComprobante = (index, total) => {

                let html = '';
                ctrl.atencion.Comprobantes.push({ Id: index });
                html += "<facturacion-venta id='facturacionVenta-" + index + "' name='facturacionVenta-" + index + "' external-id='facturacionVenta-" + index + "' api='$ctrl.facturacionAPI[" + index + "]' facturacion='$ctrl.atencion.Comprobantes[" + index + "]' mostrar-punto-de-venta-vendedor='false' debe-seleccionar-punto-de-venta-vendedor='false' debe-seleccionar-almacen-almacenero='false' debe-seleccionar-caja-cajero='false' id-medio-pago-default='$ctrl.parametros.IdMedioDePagoEfectivo' permitir-registro-fecha-emision='false' permitir-registro-placa='false' debe-permitir-detalle-unificado='" + ctrl.detalleUnificado + "' cambio-igv='$ctrl.cambiarAfeccionIgv" + index + "(aplicarIgv)' inicio-realizado='$ctrl.inicioRealizadoFacturacion(" + index + ")'  importe-total='" + total.toFixed(2) + "'></facturacion-venta>";
                $("#idComprobantesVenta-" + index).append($compile(html)($scope));
            }
            //ctrl.limpiar = function () {
            //    ctrl.facturacion = {
            //        Atencion: {}
            //    };
            //    ctrl.facturacion.esValido = false;
            //};
            ctrl.establecerDatosPorDefecto = function () {
                //ctrl.facturacion.FormaPago = ctrl.parametrosDeAtencion.TipoPagoSimple;
            };

            ctrl.facturarAtencion = function () {
                ctrl.AgregarDetallesDeOrdenAcarril();
                for (let i = 0; i < ctrl.atencion.Comprobantes.length; i++) {
                    ctrl.atencion.Comprobantes[i].Id = i;
                    if (ctrl.atencion.TipoDePago == ctrl.parametros.TipoDePagoCuentaDiferenciadaDetallada)
                        ctrl.atencion.Comprobantes[i].Orden.Detalles = ctrl.arrayDeCarrilesDiferenciado[i].detalleOrden;
                }
                restauranteService.facturarAtencion({ atencion: ctrl.atencion }).success(function (data) {
                    ctrl.seFacturo = true;
                    ctrl.atencion.documentoAtencion = data.objeto;
                    ctrl.atencion.documentosVenta = data.information;
                    //SweetAlert.success("Correcto", data.result_description);
                    ctrl.closeFacturacion(true);
                    $('#modal-facturador-restaurante').modal('hide');
                }).error(function (data, status) {
                    SweetAlert.error2(data);
                });
            };

            ctrl.actualizarTotalDeComprobante = (index, total) => {
                ctrl.facturacionAPI[index].SetTotalVenta(total);
            }

            //#endregion

            //#region FACTURACION SIMPLE
            ctrl.inicializarFacturacionSimple = () => {
                let idSimple = 0;
                ctrl.quitarFacturacion(idSimple);
                $timeout(() => { ctrl.agregarNuevoComprobante(idSimple, ctrl.atencion.ImporteAtencion); }, 100);

            }
            //#endregion 

            //#region FACTURACION DIFERENCIADO --------------------------------

            //--- MObile----
            ctrl.inicializarDragAndDropMobile = () => {
                let detalleOrdenes = document.getElementsByClassName('detalle-orden');
                for (let i = 0; i < detalleOrdenes.length; i++) {
                    //detalleOrdenes[i].addEventListener('touchmove', function (ev) {
                    //    let touchLocation = ev.targetTouches[0];
                    //    detalleOrdenes[i].style.left = touchLocation.pageX + 'px';
                    //    detalleOrdenes[i].style.top = touchLocation.pageY + 'px';
                    //    console.log(detalleOrdenes[i].style.left, detalleOrdenes[i].style.top)
                    //})
                    detalleOrdenes[i].addEventListener('touchend', function () {
                        ctrl.sumarMontoEntodoLosElementosCarril();
                    })
                }
            }
            //--
            ctrl.allowDrop = function (ev) {
                ev.preventDefault();
            }
            ctrl.drag = (ev) => {

                ev.dataTransfer.setData('text', ev.target.id);

            }
            ctrl.drop = (ev) => {
                ev.preventDefault();
                var data = ev.dataTransfer.getData("text");
                ev.target.appendChild(document.getElementById(data));
            }

            ctrl.inicializarDragAndDrop = function () {
                let elementos = document.querySelectorAll('.carril-detalleOrden');
                for (let i = 0; i < ctrl.arrayDeCarrilesDiferenciado.length; i++) {
                    elementos[i].addEventListener('drop', function (event) {
                        event.preventDefault();
                        var data = event.dataTransfer.getData("text");
            
                        //event.target.appendChild(document.getElementById(data));
                        document.getElementById(`bandeja-detalle-ordenes${i}`).appendChild(document.getElementById(data));
                        $timeout(() => { ctrl.sumarMontoEntodoLosElementosCarril(); }, 100)
                        
                        //ctrl.agregarItem(data, i)

                    });
                    elementos[i].addEventListener('dragover', function (event) {
                        event.preventDefault();
                    });

                }
            }
            ctrl.pasarDetalleOrden = (detalleOrden, $event) => {
                ctrl.detalleOrden = detalleOrden;
           
            }
            ctrl.buscarDetalleOrden = (nombre) => {
                let i = 0;
                let resultado = {};
                do {
                    resultado = ctrl.atencion.Ordenes[i].DetallesDeOrden.find(detalleOrden => detalleOrden.NombreItem === nombre)
                    i++;
                } while (resultado === undefined )
                return resultado;
            }
            ctrl.AgregarDetallesDeOrdenAcarril = () => {
                if (ctrl.arrayDeCarrilesDiferenciado.length>0) {
                    for (var i = 0; i < ctrl.arrayDeCarrilesDiferenciado.length; i++) {
                        let elemento = document.getElementById(`bandeja-detalle-ordenes${i}`)
                        let nombresDetalleOrden = elemento.getElementsByClassName('nombre-detalleOrden');
                        for (var j = 0; j < nombresDetalleOrden.length; j++) {
                            let detalleOrden = ctrl.buscarDetalleOrden(nombresDetalleOrden[j].innerHTML);
                            detalleOrden.Cantidad = 1;
                            detalleOrden.Importe = detalleOrden.Cantidad * detalleOrden.Precio;
                            ctrl.arrayDeCarrilesDiferenciado[i].detalleOrden.push(detalleOrden);
                        }
                    }
                }
                console.log(ctrl.arrayDeCarrilesDiferenciado)
            }
            ctrl.agregarItem = (idItem, idElemento) => {
                let id = String(idItem);
                let pos = id.indexOf('_');
                let suma = 0;
                let idDetalleOrden = id.substring((pos + 1), id.length)
                if (idDetalleOrden == ctrl.detalleOrden.Id) {
                  /*  ctrl.arrayDeCarrilesDiferenciado[idElemento].detalleOrden.push(ctrl.detalleOrden);*/
                    ctrl.sumarMontoEntodoLosElementosCarril();

                    //como no renderiza automatico la vista, esto ayuda 
                    scope.$apply();
                }
                ctrl.detalleOrden = {};
            }

            //ctrl.sumarMontoArrayCarrilDiferenciada = (id) => {
            //    let suma = 0;
            //    ctrl.arrayDeCarrilesDiferenciado[id].detalleOrden.forEach(detO => {
            //        suma += detO.Precio;
            //    });
            //    return suma;
            //}
            //ctrl.sumarMontoEntodoLosCarriles = () => {
            //    for (var i = 0; i < ctrl.arrayDeCarrilesDiferenciado.length; i++) {
            //        let suma = 0;
            //        ctrl.arrayDeCarrilesDiferenciado[i].detalleOrden.forEach(detO => {
            //            suma += detO.Precio;
            //        });
            //        ctrl.arrayDeCarrilesDiferenciado[i].monto = suma;
            //    }
            //}
            ctrl.sumarMontoEntodoLosElementosCarril = () => {
                let carriles = document.getElementsByClassName('carril-detalleOrden');

                for (var i = 0; i < carriles.length; i++) {
                    let suma = 0;
                    let detallesOrdenes = carriles[i].getElementsByTagName('label');
                    if (detallesOrdenes.length > 0) {
                        for (var j = 0; j < detallesOrdenes.length; j++) {
                            suma += parseFloat(detallesOrdenes[j].innerHTML);
                        }
                    }
                    ctrl.arrayDeCarrilesDiferenciado[i].monto = suma;
                    ctrl.actualizarTotalDeComprobante(i, ctrl.arrayDeCarrilesDiferenciado[i].monto);
                }

            }
            ctrl.deshabilitarFacutarDiferenciado = () => {
                let montoTotalDeCarriles = 0;
                let montoCero = false;
                let bandera = true;
                ctrl.arrayDeCarrilesDiferenciado.forEach(c => {
                    montoTotalDeCarriles = montoTotalDeCarriles + c.monto;
                    if (c.monto === 0) {
                        montoCero = true;
                    }
                });
                // hay que tener mucho cuidado con las variables del ctrl, porque la vista se pinta antes de JS
                if (montoTotalDeCarriles !== 0) {
                    if (ctrl.atencion.ImporteAtencion === montoTotalDeCarriles && montoCero === false) {
                        bandera = false;
                    }
                }
                else {
                    bandera = true;
                }
                let hayInconsistencias = false;
                if (ctrl.atencion.Comprobantes !== undefined) {
                    for (var i = 0; i < ctrl.atencion.Comprobantes.length; i++) {
                        hayInconsistencias = hayInconsistencias || !ctrl.atencion.Comprobantes[i].esValido;
                    }
                }
                return bandera || hayInconsistencias;
            }

            ctrl.inicializarFacturacionDiferenciado = function () {
                let montoDiferenciada = 0;
                for (let i = 0; i < 2; i++) {
                    ctrl.arrayDeCarrilesDiferenciado.push({ monto: montoDiferenciada, detalleOrden: [] });
                    $timeout(() => { ctrl.agregarNuevoComprobante(i, montoDiferenciada); }, 100);
                }
                $timeout(() => { ctrl.inicializarDragAndDrop(); ctrl.inicializarDragAndDropMobile(); }, 100);
                $timeout(() => { ctrl.inicializarDragAndDropMobile(); }, 100)
             
            }

            ctrl.agregarCarrilDiferenciado = () => {
                let nuevoCarrilDiverenciado = { monto: 0, detalleOrden: [] }
                ctrl.arrayDeCarrilesDiferenciado.push(nuevoCarrilDiverenciado);
                $timeout(() => { ctrl.agregarNuevoComprobante(ctrl.arrayDeCarrilesDiferenciado.length - 1, 0); }, 100);
                $timeout(() => { ctrl.inicializarDragAndDrop(); }, 100)


            }
            ctrl.eliminarCarrilDiferenciado = (carril, carrilIndex) => {
                ctrl.eliminarDetallesDeOrdenDeUnCarril(carrilIndex);

                ctrl.arrayDeCarrilesDiferenciado.splice(carrilIndex, 1);
                ctrl.atencion.Comprobantes.pop();
                console.log(ctrl.arrayDeCarrilesDiferenciado, carrilIndex);
             
            }
            ctrl.eliminarDetalleOrdenDeUnCarril = (detalleOrden, carrilIndex) => {
                const pos = ctrl.arrayDeCarrilesDiferenciado[carrilIndex].detalleOrden.indexOf(detalleOrden);
                ctrl.arrayDeCarrilesDiferenciado[carrilIndex].detalleOrden.splice(pos, 1);
            }
            ctrl.quitarDetalleOrden = (ordenIndex, detalleOrdenIndex, detalleOrdenId) => {
                let idDetalleOrden = `detalleOrden*${ordenIndex}+${detalleOrdenIndex}_${detalleOrdenId}`;
                let elemento = document.getElementById(`contenedor-detalleOrdenes${ordenIndex}`);

                elemento.appendChild(document.getElementById(idDetalleOrden));
                ctrl.sumarMontoEntodoLosElementosCarril();
            }
            ctrl.eliminarDetallesDeOrdenDeUnCarril = (idCarril) => {
                let elemento = document.getElementById(`bandeja-detalle-ordenes${idCarril}`);
                if (elemento !== null) {
                    const detalleOrdenes = elemento.querySelectorAll('.detalle-orden')
                    let detalleOrdenesLength = detalleOrdenes.length;
                    for (let j = 0; j < detalleOrdenesLength; j++) {
                        let id = detalleOrdenes[j].getAttribute('id')
                        let asterisco = id.indexOf('*');
                        let mas = id.indexOf('+');
                        let guion = id.indexOf('_');
                        ctrl.quitarDetalleOrden(id.slice(asterisco + 1, mas), id.slice(mas + 1, guion), id.slice(guion + 1));
                    }
                }
            }
            ctrl.actualizarCarrilDiferenciado = () => {
                for (let i = 0; i < ctrl.arrayDeCarrilesDiferenciado.length; i++) {
                    let elemento = document.getElementById(`bandeja-detalle-ordenes${i}`);

                    if (elemento !== null) {
                        const detalleOrdenes = elemento.querySelectorAll('.detalle-orden')
                        let detalleOrdenesLength = detalleOrdenes.length;
                        for (let j = 0; j < detalleOrdenesLength; j++) {
                            let id = detalleOrdenes[j].getAttribute('id')
                            let asterisco = id.indexOf('*');
                            let mas = id.indexOf('+');
                            let guion = id.indexOf('_');
                            ctrl.quitarDetalleOrden(id.slice(asterisco + 1, mas), id.slice(mas + 1, guion), id.slice(guion + 1));
                        }
                    }

                }
            }
            //#endregion

            //#region FACTURACION DIVIDIO
            ctrl.inicializarFacturacionDividido = () => {
                
                let montoDividido = parseFloat((ctrl.atencion.ImporteAtencion / 2).toFixed(2))
                let montoDiferencia = ctrl.atencion.ImporteAtencion-(montoDividido*2)
                for (let i = 0; i < 2; i++) {
                    ctrl.arrayDeCarrilesDividido.push({ monto: montoDividido });
                    $timeout(() => { ctrl.agregarNuevoComprobante(i, montoDividido); }, 100);
                }
                ctrl.arrayDeCarrilesDividido[1].monto += montoDiferencia;

            }

            ctrl.agregarCarrilDividido = () => {
                let nuevoCarrilDividido = { monto: 0 }
                ctrl.arrayDeCarrilesDividido.push(nuevoCarrilDividido);
                $timeout(() => { ctrl.agregarNuevoComprobante(ctrl.arrayDeCarrilesDividido.length - 1, 0); }, 100);
            }
            ctrl.eliminarCarrilDividido = (carril) => {
                const pos = ctrl.arrayDeCarrilesDividido.indexOf(carril);
                ctrl.arrayDeCarrilesDividido.splice(pos, 1);
                ctrl.atencion.Comprobantes.splice(pos, 1);
            }
            ctrl.deshabilitarFacutarDividido = () => {

                let montoTotalDeCarriles = 0;
                let montoCero = false;
                let bandera = true;
                ctrl.arrayDeCarrilesDividido.forEach(c => {
                    montoTotalDeCarriles = montoTotalDeCarriles + c.monto;
                    if (c.monto === 0) {
                        montoCero = true;
                    }
                });
                // hay que tener mucho cuidado con las variables del ctrl, porque la vista se pinta antes de JS
                if (montoTotalDeCarriles !== 0) {
                    if (ctrl.atencion.ImporteAtencion === montoTotalDeCarriles && montoCero === false) {
                        bandera = false;
                    }
                }
                else {
                    bandera = true;
                }
                let hayInconsistencias = false;
                if (ctrl.atencion.Comprobantes !== undefined) {
                    for (var i = 0; i < ctrl.atencion.Comprobantes.length; i++) {
                        hayInconsistencias = hayInconsistencias || !ctrl.atencion.Comprobantes[i].esValido;
                    }
                }
                return bandera || hayInconsistencias;
            }
            ctrl.dividirPrecio = () => {
                let precio = (ctrl.atencion.ImporteAtencion / ctrl.arrayDeCarrilesDividido.length);
                let precioDividido = parseFloat(precio.toFixed(2));
                let precioActual = precioDividido * ctrl.arrayDeCarrilesDividido.length;
                let precioDiferencia = ctrl.atencion.ImporteAtencion - precioActual;
                for (var i = 0; i < ctrl.arrayDeCarrilesDividido.length; i++) {
                    ctrl.arrayDeCarrilesDividido[i].monto = precioDividido;
                    ctrl.actualizarTotalDeComprobante(i, precioDividido);
                }
                ctrl.arrayDeCarrilesDividido[ctrl.arrayDeCarrilesDividido.length - 1].monto = parseFloat((parseFloat(precioDividido.toFixed(2)) + parseFloat(precioDiferencia.toFixed(2))).toFixed(2));
                ctrl.actualizarTotalDeComprobante((ctrl.arrayDeCarrilesDividido.length - 1), ctrl.arrayDeCarrilesDividido[ctrl.arrayDeCarrilesDividido.length - 1].monto);
            }
            //#endregion

            ctrl.quitarFacturacion = (id) => {
                let parent = document.getElementById(`idComprobantesVenta-${id}`)
                if (parent !== null) {
                    let nodos = parent.childNodes
                    if (nodos.length > 0) {
                        parent.removeChild(nodos[0]);
                    }
                }
            }

            this.$onInit = function () {
                ctrl.api = {};
                ctrl.inicializar();
                ctrl.api.SetAtencionAFacturar = ctrl.setAtencionAFacturar;
            };


        }
    });

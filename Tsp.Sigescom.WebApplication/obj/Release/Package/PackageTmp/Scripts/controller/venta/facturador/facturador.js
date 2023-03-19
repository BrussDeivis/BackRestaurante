angular.
    module('app').
    component('facturador', {
        templateUrl: "../Scripts/controller/venta/facturador/facturador.html",
        bindings: {
            api: '=',
            atencion: '=',
            guardarFacturacion: '&',
            cerrarFacturacion: '&'
        },

        controller: function ($q, $compile, $scope, $parse, $timeout, SweetAlert, ventaService, clienteService, hotelService) {
            var ctrl = this;
            var scope = $scope.$root;
            $('select:not(.normal)').each(function () {
                $(this).select2({
                    dropdownParent: $(this).parent()
                });
            });

            ctrl.closeFacturacion = function (seFacturo) {
                $('#modal-facturador').modal('hide');
                ctrl.cerrarFacturacion({ seFacturo: seFacturo });
            };

            ctrl.saveFacturacion = function (seFacturo) {
                ctrl.ordenarNuevosComprobantes();
                ctrl.guardarFacturacion({});
            };

            ctrl.inicializar = function () {
                ctrl.inicializarVariables();
                ctrl.cargarParametros().then(function (resultado_) {
                    ctrl.establecerDatosPorDefecto();
                }, function (error) {
                    ctrl.mensaje = "Se ha producido un error al obtener el dato:" + error;
                });

            };

            ctrl.inicializarVariables = function () {
                ctrl.arrayDeCarrilesDiferenciado = [];
                ctrl.arrayDeCarrilesGeneral = [];
                ctrl.carril = {};
                ctrl.detalleOrden = {};
                ctrl.seFacturo = false;
                ctrl.detalleUnificado = true;
                ctrl.tamanioComprobante = '80mm';
                ctrl.atencion = { NuevosComprobantes: [], esValido: false };
            };

            ctrl.cargarParametros = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                hotelService.obtenerConfiguracionParaFacturar({}).success(function (data) {
                    ctrl.parametros = data.data;
                    ctrl.inicializacionRealizada = true;
                    defered.resolve();
                }).error(function (data) {
                    SweetAlert.error("Ocurrio un Problema", data.error);
                    defered.reject(data);
                });
                return promise;
            };

            ctrl.establecerDatosPorDefecto = function () {
                ctrl.atencion.TipoDePago = ctrl.parametros.TipoDePagoGeneral;
            };

            ctrl.ampliarPorFacturar = () => {
                ctrl.atencion.botonMaximizar = ctrl.atencion.botonMaximizar == 'fa-arrow-right' ? 'fa-arrow-left' : 'fa-arrow-right';
                let contenedorFacturar = document.getElementById("contenedorPorFacturar");
                contenedorFacturar.classList.toggle("maximizar-por-facturar");
                let contenedorFacturado = document.getElementById("contenedorFacturado");
                contenedorFacturado.classList.toggle("minimizar-facturado");
            }

            //#region SELECCIONAR RADIO FACTURACION 
            ctrl.cargarAtencionEnComprobantes = function () {
                ctrl.seFacturo = false;
                ctrl.seleccionarFacturacionGeneral();
            };

            ctrl.limpiarTiposFacturacion = () => {
                ctrl.formato = {};
                ctrl.detalleUnificado = true;
                ctrl.atencion.NuevosComprobantes = [];
                ctrl.arrayDeCarrilesGeneral = [];
                ctrl.arrayDeCarrilesDiferenciado = [];
                ctrl.atencion.botonMaximizar = 'fa-arrow-right';
            }

            ctrl.seleccionarFacturacionGeneral = function () {
                ctrl.limpiarTiposFacturacion();
                ctrl.detalleUnificado = true;
                ctrl.atencion.TipoDePago = ctrl.parametros.TipoDePagoGeneral;
                ctrl.inicializarFacturacionGeneral();
            };

            ctrl.seleccionarFacturacionDiferenciado = function () {
                ctrl.limpiarTiposFacturacion();
                ctrl.atencion.TipoDePago = ctrl.parametros.TipoDePagoDiferenciado;
                ctrl.inicializarFacturacionDiferenciado();
            };
            //#endregion

            //#region FACTURACION 
            ctrl.setAtencionAFacturar = function (atencion) {
                ctrl.atencion = atencion;
                if (atencion.TieneFacturacion) {
                    ctrl.atencion.OrdenPrincipal.TieneFacturacion = true;
                }
                ctrl.OrdenesAFacturar = ctrl.atencion.Ordenes;
                if (!(Object.keys(ctrl.atencion.OrdenPrincipal).length === 0)) {
                    ctrl.OrdenesAFacturar.unshift(ctrl.atencion.OrdenPrincipal);
                }
                ctrl.cargarAtencionEnComprobantes();
            };

            ctrl.setComprobantesFacturados = function (documentos) {
                if (ctrl.atencion.ComprobantesFacturados.length > 0) {
                    for (let i = 0; i < documentos.length; i++) {
                        ctrl.atencion.ComprobantesFacturados.push(documentos[i]);
                    }
                } else {
                    ctrl.atencion.ComprobantesFacturados = documentos;
                }
                ctrl.atencion.Importe = 0;
                for (let i = 0; i < ctrl.OrdenesAFacturar.length; i++) {
                    ctrl.OrdenesAFacturar[i].TieneFacturacion = true;
                }
            };

            ctrl.detallesDeOrdenAtendidas = (detalleOrden) => {
                return detalleOrden.Estado === ctrl.parametros.ConfiguracionDetallesDeOrden.EstadoAtendido
            }
            ctrl.ordenesAtendidas = (orden) => {
                return orden.ImporteOrden > 0;
            }

            ctrl.ordenarNuevosComprobantes = function () {
                ctrl.AgregarDetallesDeOrdenAcarril();
                let ComprobantesTemporales = [];
                for (let i = 0; i < ctrl.atencion.NuevosComprobantes.length; i++) {
                    if (ctrl.atencion.NuevosComprobantes[i] !== undefined && ctrl.atencion.NuevosComprobantes[i] !== null) {
                        ComprobantesTemporales.push(ctrl.atencion.NuevosComprobantes[i]);
                    }
                }
                ctrl.atencion.NuevosComprobantes = ComprobantesTemporales;
                for (let i = 0; i < ctrl.atencion.NuevosComprobantes.length; i++) {
                    ctrl.atencion.NuevosComprobantes[i].Id = i;
                    if (ctrl.atencion.TipoDePago == ctrl.parametros.TipoDePagoDiferenciado)
                        ctrl.atencion.NuevosComprobantes[i].Orden.Detalles = ctrl.arrayDeCarrilesDiferenciado[i].detalleOrden;
                }
            }

            ctrl.agregarNuevoComprobante = (index, total) => {
                let html = '';
                ctrl.atencion.NuevosComprobantes[index] = { Id: index };
                html += "<facturacion-venta id='facturacionVenta-" + index + "' name='facturacionVenta-" + index + "' external-id='facturacionVenta-" + index + "' api='$ctrl.facturacionAPI[" + index + "]' facturacion='$ctrl.atencion.NuevosComprobantes[" + index + "]' debe-seleccionar-punto-de-venta-vendedor='false' debe-seleccionar-almacen-almacenero='false' debe-seleccionar-caja-cajero='false' id-medio-pago-default='$ctrl.parametros.IdMedioDePagoEfectivo' permitir-registro-fecha-emision='false' permitir-registro-placa='false' debe-permitir-detalle-unificado='" + ctrl.detalleUnificado + "' cambio-igv='$ctrl.cambiarAfeccionIgv" + index + "(aplicarIgv)' inicio-realizado='$ctrl.inicioRealizadoFacturacion(" + index + ")'  importe-total='" + total.toFixed(2) + "'></facturacion-venta>";
                $("#idComprobantesVenta-" + index).append($compile(html)($scope));
            }

            ctrl.actualizarTotalDeComprobante = (index, total) => {
                ctrl.facturacionAPI[index].SetTotalVenta(total);
            }

            ctrl.formatoDecimalImporte = function (event) {
                let valor = event.target.value;
                event.target.value = parseFloat(valor).toFixed(2);
            }

            //#endregion

            //#region FACTURACION DIFERENCIADO --------------------------------
            ctrl.inicializarDragAndDropMobile = () => {
                let detalleOrdenes = document.getElementsByClassName('detalle-orden');
                for (let i = 0; i < detalleOrdenes.length; i++) {
                    detalleOrdenes[i].addEventListener('touchend', function () {
                        ctrl.sumarMontoEntodoLosElementosCarril();
                    })
                }
            }

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
                        document.getElementById(`bandeja-detalle-ordenes${i}`).appendChild(document.getElementById(data));
                        $timeout(() => { ctrl.sumarMontoEntodoLosElementosCarril(); }, 100)
                    });
                    elementos[i].addEventListener('dragover', function (event) {
                        event.preventDefault();
                    });
                }
            }

            ctrl.pasarDetalleOrden = (detalleOrden, $event) => {
                ctrl.detalleOrden = detalleOrden;
            }

            ctrl.buscarDetalleOrden = (id) => {
                let i = 0;
                let resultado = {};
                do {
                    resultado = ctrl.OrdenesAFacturar[i].Detalles.find(detalleOrden => detalleOrden.Id == id)
                    i++;
                } while (resultado == undefined)
                return resultado;
            }

            ctrl.AgregarDetallesDeOrdenAcarril = () => {
                if (ctrl.arrayDeCarrilesDiferenciado.length > 0) {
                    for (var i = 0; i < ctrl.arrayDeCarrilesDiferenciado.length; i++) {
                        let elemento = document.getElementById(`bandeja-detalle-ordenes${i}`)
                        let idsDetalleOrden = elemento.getElementsByClassName('id-detalleOrden');
                        for (var j = 0; j < idsDetalleOrden.length; j++) {
                            let detalleOrden = ctrl.buscarDetalleOrden(idsDetalleOrden[j].innerHTML);
                            detalleOrden.Cantidad = 1;
                            detalleOrden.Importe = detalleOrden.Cantidad * detalleOrden.PrecioUnitario;
                            ctrl.arrayDeCarrilesDiferenciado[i].detalleOrden.push(detalleOrden);
                        }
                    }
                }
            }

            ctrl.agregarItem = (idItem, idElemento) => {
                let id = String(idItem);
                let pos = id.indexOf('_');
                let suma = 0;
                let idDetalleOrden = id.substring((pos + 1), id.length)
                if (idDetalleOrden == ctrl.detalleOrden.Id) {
                    ctrl.sumarMontoEntodoLosElementosCarril();
                    scope.$apply();
                }
                ctrl.detalleOrden = {};
            }

            ctrl.sumarMontoEntodoLosElementosCarril = () => {
                let carriles = document.getElementsByClassName('carril-detalleOrden');

                for (var i = 0; i < carriles.length; i++) {
                    let suma = 0;
                    let detallesOrdenes = carriles[i].getElementsByClassName('monto-detalleOrden');
                    if (detallesOrdenes.length > 0) {
                        for (var j = 0; j < detallesOrdenes.length; j++) {
                            suma += parseFloat(detallesOrdenes[j].innerHTML);
                        }
                    }
                    ctrl.arrayDeCarrilesDiferenciado[i].monto = suma;
                    ctrl.actualizarTotalDeComprobante(i, ctrl.arrayDeCarrilesDiferenciado[i].monto);
                }
            }

            ctrl.deshabilitarFacturarDiferenciado = () => {
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
                    if (ctrl.atencion.Importe == parseFloat(montoTotalDeCarriles.toFixed(2)) && montoCero === false) {
                        bandera = false;
                    }
                }
                else {
                    bandera = true;
                }
                let hayInconsistencias = false;
                if (ctrl.atencion.NuevosComprobantes !== undefined) {
                    for (var i = 0; i < ctrl.atencion.NuevosComprobantes.length; i++) {
                        if (ctrl.atencion.NuevosComprobantes[i] !== undefined && ctrl.atencion.NuevosComprobantes[i] !== null) {
                            hayInconsistencias = hayInconsistencias || !ctrl.atencion.NuevosComprobantes[i].esValido;
                        }
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
                ctrl.atencion.NuevosComprobantes.pop();
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

            //#region FACTURACION GENERAL
            ctrl.inicializarFacturacionGeneral = () => {
                let id = 0;
                ctrl.arrayDeCarrilesGeneral.push({ idCarril: id, monto: ctrl.atencion.Importe });
                $timeout(() => { ctrl.agregarNuevoComprobante(id, ctrl.atencion.Importe); }, 100);
            }

            ctrl.idUnico = () => {
                let nuevoId = 0;
                let existe = true;
                do {
                    nuevoId = Math.floor(Math.random() * 20);
                    existe = ctrl.arrayDeCarrilesGeneral.some((carril) => carril.idCarril === nuevoId);
                } while (existe);
                return nuevoId;
            }

            ctrl.agregarCarrilGeneral = () => {
                ctrl.resolverDetalleUnificado(true);
                let id = ctrl.idUnico();
                let nuevoCarrilGeneral = { idCarril: id, monto: 0 }
                ctrl.arrayDeCarrilesGeneral.push(nuevoCarrilGeneral);
                $timeout(() => { ctrl.agregarNuevoComprobante(id, 0); }, 100);
            }

            ctrl.eliminarCarrilGeneral = (carril) => {
                ctrl.resolverDetalleUnificado(false);
                const pos = ctrl.arrayDeCarrilesGeneral.indexOf(carril);
                ctrl.arrayDeCarrilesGeneral.splice(pos, 1);
                ctrl.atencion.NuevosComprobantes[carril.idCarril] = null;
            }

            ctrl.resolverDetalleUnificado = (estaAgregando) => {
                let detalleUnificadoActual = ctrl.detalleUnificado;
                if ((estaAgregando && ctrl.arrayDeCarrilesGeneral.length >= 1) || (!estaAgregando && ctrl.arrayDeCarrilesGeneral.length > 2)) {
                    ctrl.detalleUnificado = false;
                } else {
                    ctrl.detalleUnificado = true;
                }
                if (detalleUnificadoActual != ctrl.detalleUnificado) {
                    for (let i = 0; i < ctrl.arrayDeCarrilesGeneral.length; i++) {
                        if (ctrl.detalleUnificado) {
                            ctrl.facturacionAPI[ctrl.arrayDeCarrilesGeneral[i].idCarril].MostrarDetalleUnificado();
                        } else {
                            ctrl.facturacionAPI[ctrl.arrayDeCarrilesGeneral[i].idCarril].OcultarDetalleUnificado();
                        }
                    }
                }
            }

            ctrl.deshabilitarFacturarGeneral = () => {
                let montoTotalDeCarriles = 0;
                let montoCero = false;
                let bandera = true;
                ctrl.arrayDeCarrilesGeneral.forEach(c => {
                    montoTotalDeCarriles = montoTotalDeCarriles + c.monto;
                    if (c.monto === 0) {
                        montoCero = true;
                    }
                });
                // hay que tener mucho cuidado con las variables del ctrl, porque la vista se pinta antes de JS
                if (montoTotalDeCarriles !== 0) {
                    if (ctrl.atencion.Importe === montoTotalDeCarriles && montoCero === false) {
                        bandera = false;
                    }
                }
                else {
                    bandera = true;
                }
                let hayInconsistencias = false;
                if (ctrl.atencion.NuevosComprobantes !== undefined) {
                    for (var i = 0; i < ctrl.atencion.NuevosComprobantes.length; i++) {
                        if (ctrl.atencion.NuevosComprobantes[i] !== undefined && ctrl.atencion.NuevosComprobantes[i] !== null) {
                            hayInconsistencias = hayInconsistencias || !ctrl.atencion.NuevosComprobantes[i].esValido;
                        }
                    }
                }
                return bandera || hayInconsistencias;
            }

            ctrl.dividirPrecio = () => {
                let precio = (ctrl.atencion.Importe / ctrl.arrayDeCarrilesGeneral.length);
                let precioGeneral = parseFloat(precio.toFixed(2));
                let precioActual = precioGeneral * ctrl.arrayDeCarrilesGeneral.length;
                let precioDiferencia = ctrl.atencion.Importe - precioActual;
                for (var i = 0; i < ctrl.arrayDeCarrilesGeneral.length; i++) {
                    ctrl.arrayDeCarrilesGeneral[i].monto = precioGeneral;
                    ctrl.actualizarTotalDeComprobante(ctrl.arrayDeCarrilesGeneral[i].idCarril, precioGeneral);
                }
                ctrl.arrayDeCarrilesGeneral[ctrl.arrayDeCarrilesGeneral.length - 1].monto = parseFloat((parseFloat(precioGeneral.toFixed(2)) + parseFloat(precioDiferencia.toFixed(2))).toFixed(2));
                ctrl.actualizarTotalDeComprobante((ctrl.arrayDeCarrilesGeneral[ctrl.arrayDeCarrilesGeneral.length - 1].idCarril), ctrl.arrayDeCarrilesGeneral[ctrl.arrayDeCarrilesGeneral.length - 1].monto);
            }
            //#endregion

            this.$onInit = function () {
                ctrl.api = {};
                ctrl.inicializar();
                ctrl.api.SetAtencionAFacturar = ctrl.setAtencionAFacturar;
                ctrl.api.SetComprobantesFacturados = ctrl.setComprobantesFacturados;
            };
        }
    });

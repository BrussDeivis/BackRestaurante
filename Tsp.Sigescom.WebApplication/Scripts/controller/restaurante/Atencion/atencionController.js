app.controller('atenciónController', function ($scope, $q, $timeout, $rootScope, $compile, SweetAlert, restauranteService) {

    $scope.categorias = [];
    $scope.items = [];
    $scope.itemsVisibles = [];
    $scope.ambientesConMesa = [];
    $scope.ambientesSinMesa = [];
    $scope.mesas = [];
    $scope.familias = []
    $scope.ordenDeVista = {};
    $scope.atencionActual = {};
    $scope.ordenActual = {};
    $scope.categoriaSeleccionada = "";
    $scope.ambienteActual = {};
    $scope.alturaContenedor = $(".content-wrapper").get(0).clientHeight;
    $scope.numeroDeAmbiente = 1;
    $scope.numeroDeComprobantes = 1;
    $scope.atencionConMesa = ParametrosDeConfiguracion.PermitirVentaEnMesa ? 'true' : 'false';
    $scope.atencionDelivery = 'true';
    $scope.nuevaAtencionSinMesa = true;
    $scope._banderaMesaSeleccionada = false;
    $scope._banderaRealizandoseOrden = false;
    $scope._configurandoInformacionDePago = false;
    $scope.itemSeleccionado = { Cantidad: 1 };

    $scope.cargarParametros = function () {
        $scope.ParametrosDeAtencion = ParametrosDeConfiguracion;
        $scope.ParametrosDeOrden = ParametrosDeConfiguracion.ConfiguracionDeOrden;
        $scope.ParametrosDeFacturacion = ParametrosDeConfiguracion.ConfiguracionDeFacturacion;
        $scope.ParametrosDeDetallesDeOrden = $scope.ParametrosDeOrden.ConfiguracionDetallesDeOrden;
        $scope.ModuloPreparacionActivado = ParametrosDeConfiguracion.ModuloPreparacionActivado;
        $scope.inicializacionRealizada = true;
    };

    AmbienteAtencionController($scope, $q, $timeout, $rootScope, $compile, SweetAlert, restauranteService);
    TomarOrdenAtencionController($scope, $q, $timeout, $rootScope, $compile, SweetAlert, restauranteService);
    GestionOrdenesAtencionController($scope, $q, $timeout, $rootScope, $compile, SweetAlert, restauranteService);
    DetalleOrdenAtencionController($scope, $q, $timeout, $rootScope, $compile, SweetAlert, restauranteService);
    FacturacionAtencionController($scope, $q, $timeout, $rootScope, $compile, SweetAlert, restauranteService);

    $scope.cambioAmbienteConMesa = function (numeroDeAmbiente) {
        if ($scope._banderaRealizandoseOrden == true) {
            $scope.alertaSalvarAtencion().then(result => {
                if (result.isConfirmed) {
                    $scope.cambiarAmbienteConMesa(numeroDeAmbiente);
                    $scope.limpiarAtencionOrden();
                } else {
                    let radioAmbiente = document.getElementById('ambienteconmesa-' + ($scope.numeroDeAmbiente - 1));
                    radioAmbiente.checked = true;
                }
            })
        } else {
            $scope.cambiarAmbienteConMesa(numeroDeAmbiente);
        }
    }

    $scope.cambiarAmbienteConMesa = function (numeroDeAmbiente) {
        $scope.numeroDeAmbiente = numeroDeAmbiente;
        $scope.ambienteActual = $scope.ambientesConMesa[$scope.numeroDeAmbiente - 1];
        $scope.obtenerMesasDeAmbiente($scope.ambienteActual.Id);
    }

    $scope.cambioAmbienteSinMesa = function (numeroDeAmbiente) {
        if ($scope._banderaRealizandoseOrden == true) {
            $scope.alertaSalvarAtencion().then(result => {
                if (result.isConfirmed) {
                    $scope.numeroDeAmbiente = numeroDeAmbiente;
                    $scope.ambienteActual = $scope.ambientesSinMesa[$scope.numeroDeAmbiente - 1];
                    $scope.obtenerAtencionesSinMesa();
                    $scope.limpiarAtencionOrden();
                    $scope.iniciarAtencionPorLista();
                } else {
                    let radioAmbiente = document.getElementById('ambientesinmesa-' + ($scope.numeroDeAmbiente - 1));
                    radioAmbiente.checked = true;
                }
            })
        } else {
            $scope.numeroDeAmbiente = numeroDeAmbiente;
            $scope.ambienteActual = $scope.ambientesSinMesa[$scope.numeroDeAmbiente - 1];
            $scope.obtenerAtencionesSinMesa();
            $scope.limpiarAtencionOrden();
            $scope.iniciarAtencionPorLista();
        }
    }

    $scope.habilitarCambioDeMesa = function () {
        $scope._banderaCambiandoMesa = true;
    }

    $scope.cambiarMesaDeAtencionActualPor = function (mesaNueva) {
        if ($scope.atencionActual.Id == 0) {
            $scope.atencionActual.Mesa = mesaNueva;
        }
        else {
            restauranteService.cambiarMesaDeAtencion({ atencion: $scope.atencionActual, idNuevaMesa: mesaNueva.Id }).success(function (data) {
                $scope.obtenerMesasDeAmbiente($scope.ambienteActual.Id);
                $scope.actualizarAtencion(mesaNueva.Id);
            }).error(function (data) {
                SweetAlert.error2(data);
            });
        }
        $scope._banderaCambiandoMesa = false;
    }

    $scope.cancelarCambioDeMesa = function () {
        $scope._banderaCambiandoMesa = false;
    }

    $scope.cerrarAtencion = function (idAtencion) {
        restauranteService.cerrarAtencion({ Id: $scope.atencionActual.Id }).success(data => {
            $scope.atencionActual.Estado = data.data;
            if (data.objeto) {
                $scope.obtenerMesasDeAmbiente($scope.ambienteActual.Id);
                $scope.limpiarAtencion();
                $scope.limpiarOrden();
                $scope._banderaMesaSeleccionada = false;
            }
            else {
                $scope.actualizarAtencion($scope.atencionActual.Mesa.Id);
            }
            SweetAlert.success("Correcto", data.result_description)
        }).error(function (data, status) {
            SweetAlert.error2(data);
        });
    }

    $scope.puedeCerrarAtencion = function () {
        var totalOrdenes = $scope.atencionActual.Ordenes.length;
        var ordenesCerradas = $scope.atencionActual.Ordenes.filter(av => av.Estado == $scope.ParametrosDeAtencion.IdEstadoCerrado).length;
        return ($scope.ParametrosDeAtencion.PermitirCierreRapidoDeAtencion || (totalOrdenes == ordenesCerradas));
    }

    $scope.verBotonCerrarAtencion = function () {
        return $scope.atencionActual.Estado == $scope.ParametrosDeAtencion.IdEstadoRegistrado;
    }

    $scope.calcularImportes = function () {
        $scope.atencionActual.ImporteAtencion = 0;
        for (var i = 0; i < $scope.atencionActual.Ordenes.length; i++) {
            var orden = $scope.atencionActual.Ordenes[i];
            $scope.calcularImporteOrden($scope.atencionActual.Ordenes[i]);
            $scope.atencionActual.ImporteAtencion += orden.ImporteOrden;
        }
    }

    $scope.calcularImporteOrden = function (orden) {
        orden.ImporteOrden = 0;
        for (var j = 0; j < orden.DetallesDeOrden.length; j++) {
            var detalle = orden.DetallesDeOrden[j];
            if (detalle.Estado != $scope.ParametrosDeDetallesDeOrden.EstadoAnulado && detalle.Estado != $scope.ParametrosDeDetallesDeOrden.EstadoDevuelto) {
                orden.ImporteOrden += detalle.Importe;
            }
        }
    }

    $scope.calcularImporteAtencion = function (atencion) {
        atencion.ImporteAtencion = 0;
        for (var i = 0; i < atencion.Ordenes.length; i++) {
            atencion.ImporteAtencion += atencion.Ordenes[i].ImporteOrden;
        }
    }

    $scope.obtenerEstiloDeReporteDeOrden = function (numEstado) {
        var Estilo;
        switch (numEstado) {
            case $scope.ParametrosDeOrden.EstadoCerrado:
                Estilo = { 'border-top': '4px solid green' };
                break;
            case $scope.ParametrosDeOrden.EstadoConfirmado:
                Estilo = { 'border-top': '4px solid #f39c12' };
                break;
        }
        return Estilo;
    }

    $scope.obtenerEstiloDeDetalleDeOrden = function (estado) {
        var Estilo;
        switch (estado) {
            case $scope.ParametrosDeDetallesDeOrden.EstadoRegistrado:
                Estilo = {};
                break;
            case $scope.ParametrosDeDetallesDeOrden.EstadoPreparando:
                Estilo = { 'background-color': 'rgba(255, 165, 0, 0.3)', 'color': 'black' };
                break
            case $scope.ParametrosDeDetallesDeOrden.EstadoServido:
                Estilo = { 'background-color': 'cyan', 'color': 'black' };
                break;
            case $scope.ParametrosDeDetallesDeOrden.EstadoAtendido:
                Estilo = { 'background-color': 'rgba(0, 255, 0, 0.3)', 'color': 'black' };
                break;
            case $scope.ParametrosDeDetallesDeOrden.EstadoObservado:
                Estilo = { 'background-color': 'rgba(255, 0, 0, 0.3)', 'color': 'black' };
                break;
            case $scope.ParametrosDeDetallesDeOrden.EstadoDevuelto:
                Estilo = { 'text-decoration': 'underline overline line-through', 'color': 'orange' };
                break;
            case $scope.ParametrosDeDetallesDeOrden.EstadoAnulado:
                Estilo = { 'text-decoration': 'underline overline line-through', 'color': 'red' };
                break;
            default:
                Estilo = {};
                break;
        }
        return Estilo;
    }

    $scope.eliminarItemOrden = function (indexItemOrden) {
        $scope.ordenDeVista.ImporteOrden -= $scope.ordenDeVista.DetallesDeOrden[indexItemOrden].Importe.toFixed(2);
        $scope.ordenDeVista.DetallesDeOrden.splice(indexItemOrden, 1);
        $scope._banderaRealizandoseOrden = $scope.ordenDeVista.DetallesDeOrden.length > 0;
    }

    $scope.iniciarDatos = function () {
        $scope.cargarParametros();
        if ($scope.ParametrosDeAtencion.UsuarioTieneRolAdministradorDeNegocio) {
            $scope.EstablecimientoSeleccionado = $scope.ParametrosDeAtencion.Establecimientos[0];
        } else {
            $scope.EstablecimientoSeleccionado = $scope.ParametrosDeAtencion.EstablecimientoSesion;
        };
        $scope.obtenerCategorias();
        $scope.obtenerItems();
        $scope.obtenerMozos().then(function (result) {
            if ($scope.ParametrosDeAtencion.PermitirVentaEnMesa) {
                $scope.obtenerAmbientesConMesa($scope.numeroDeAmbiente);
            }
            if ($scope.ParametrosDeAtencion.PermitirVentaPorDelivery || $scope.ParametrosDeAtencion.PermitirVentaAlPaso) {
                $scope.obtenerAmbientesSinMesa($scope.numeroDeAmbiente);
            }
            if ($scope.ParametrosDeAtencion.PermitirVentaEnMesa) {
                $scope.atencionconmesa = 'true';
                let radioModoAtencion = document.getElementById('atencionconmesa');
                radioModoAtencion.checked = true;
            } else {
                $scope.seleccionarAtencionSinMesa();
                $scope.atencionconmesa = 'false';
                let radioModoAtencion = document.getElementById('atencionsinmesa');
                radioModoAtencion.checked = true;
            }
            $scope.limpiarOrden();
            $scope.limpiarAtencion();
            $scope.itemsVisibles = $scope.items;
        }, function (error) {
            SweetAlert.error("Ocurrio un Problema", error);
        });
    }

    $scope.iniciarCategorias = function () {
        var categorias = [];
        //OBTENER CATEGORIAS PADRES
        if ($scope.categorias.length != 0) {
            for (var i = 0; i < $scope.categorias.length; i++) {

                if ($scope.categorias[i].IdPadre == $scope.ParametrosDeAtencion.IdCategoriaNula) { // ENVIAR CATEGORIA NULA DESDE PADRE Y CARGARLO COMO VARIABLE ESTATICA.
                    categorias.push($scope.categorias[i]);
                }
            }
        }
        //PINTAR CATEGORIAS PADRE
        if (categorias.length > 0) {
            var html = "<div class='categoria'>"
            for (var i = 0; i < categorias.length; i++) {
                html += "<button type='button' class='btn btn-xs btn-info' style='margin: 0px 5px 5px 5px' ng-click='seleccionarCategoria(" + categorias[i].Id + ",1,$event)' >" + categorias[i]['Nombre'] + "</button>"
            }
            html += "</div>"
            $('#categorias').append($compile(html)($scope));
        }
    }

    $scope.limpiarAtencion = function () {
        $scope.atencionActual.Mesa = {};
        $scope.atencionActual.Id = 0;
        $scope.atencionActual.Mesa.Id = 0;
        $scope.atencionActual.IdAmbiente = $scope.ambienteActual.Id;
        $scope.atencionActual.Ordenes = [];
        $scope.atencionActual.ImporteAtencion = 0;
        $scope.atencionActual.Estado = 0;
        $scope.atencionActual.TipoDePago = 0;
        $scope.atencionActual.Comprobantes = [];
        $scope.atencionActual.Cliente = '';
    }

    $scope.obtenerMozos = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        restauranteService.obtenerMozos({}).success(function (data) {
            $scope.mozos = data;
            defered.resolve();
        }).error(function (data) {
            SweetAlert.error2(data);
            defered.reject(data);
        });
        return promise;
    };

    $scope.obtenerCategorias = function () {
        restauranteService.obtenerCategorias({}).success(function (data) {
            $scope.categorias = data;
            $scope.iniciarCategorias();
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    };

    $scope.obtenerAtencionesSinMesa = function () {
        restauranteService.obtenerAtencionesSinMesa({ idCentroAtencion: $scope.ambienteActual.Id }).success(function (data) {
            $scope.atencionesSinMesa = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    };

    $scope.establecerMozoParaAtencion = function () {
        if ($scope.atencionActual.Id == undefined || $scope.atencionActual.Id == 0) {
            if ($scope.ParametrosDeAtencion.UsuarioTieneRolCajeroYMozo) {
                $scope.ordenDeVista.Mozo = $scope.mozos.find(m => m.Id === $scope.ParametrosDeAtencion.IdEmpleado);
            }
            else {
                if ($scope.ParametrosDeAtencion.UsuarioTieneRolCajero) {
                    $scope.ordenDeVista.Mozo = {};
                }
            }
        } else {
            $scope.ordenDeVista.Mozo = $scope.atencionActual.Ordenes[$scope.atencionActual.Ordenes.length - 1].Mozo;
        }
        $timeout(function () { $('#mozo').trigger("change"); }, 100);
    };

    $scope.obtenerAtencionDeMesa = function (idMesa) {
        var defered = $q.defer();
        var promise = defered.promise;
        restauranteService.obtenerAtencionDeMesa({ IdMesa: idMesa }).success(data => {
            $scope.atencionActual = data;
            if ($scope.atencionActual.Id != 0) {
                for (var i = 0; i < $scope.atencionActual.Ordenes.length; i++) {
                    var orden = $scope.atencionActual.Ordenes[i];
                    for (var j = 0; j < orden.DetallesDeOrden.length; j++) {
                        var detalle = orden.DetallesDeOrden[j];
                        detalle.DetalleItemRestaurante = JSON.parse(detalle.DetalleItemRestauranteJson);
                        detalle.DetalleItemRestaurante.AnotacionIndicacionExistente = detalle.DetalleItemRestaurante.AnotacionIndicacion;
                        $scope.calcularImportes();
                    }
                }
            }
            if ($scope.atencionActual.IdAmbiente == 0) {
                $scope.atencionActual.IdAmbiente == $scope.ambienteActual.Id;
            }
            if ($scope.Comprobantes != undefined && $scope.Comprobantes.length) {
                $scope.numeroDeComprobantes = $scope.Comprobantes.length;
            } else {
                $scope.numeroDeComprobantes = 1;
                $scope.atencionActual.Comprobantes = [];
                $scope.actualizarNumeroDeComprobantes();
            }
            $scope.establecerMozoParaAtencion();
            defered.resolve();
        }).error(function (data) {
            SweetAlert.error2(data);
            defered.reject(data);
        })
        return promise;
    }

    $scope.alertaSalvarAtencion = function () {
        return Swal.fire({
            title: '¿Estas Seguro?',
            text: "Actualmente se esta realizando una orden",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Si!'
        });
    }

    $scope.actualizarAtencion = function (mesaId) {
        if ($scope._banderaRealizandoseOrden == false) {
            $scope._banderaMesaSeleccionada = true;
            $scope.detallesDeOrdenesVisibles = [];
            $scope.obtenerAtencionDeMesa(mesaId).then(result => {
                $("#tomarPedido").collapse($scope.atencionActual.Ordenes.length > 0 ? 'hide' : 'show');
                $("#ordenes").collapse($scope.atencionActual.Ordenes.length > 0 ? 'show' : 'hide');
            })
        } else {
            $scope.alertaSalvarAtencion().then((result) => {
                if (result.isConfirmed) {
                    $scope.limpiarOrden();
                    $scope._banderaMesaSeleccionada = true;
                    $scope._banderaRealizandoseOrden = false;
                    $scope.detallesDeOrdenesVisibles = [];
                    $scope.obtenerAtencionDeMesa(mesaId).then(result => {
                        $("#tomarPedido").collapse($scope.atencionActual.Ordenes.length > 0 ? 'hide' : 'show');
                        $("#ordenes").collapse($scope.atencionActual.Ordenes.length > 0 ? 'show' : 'hide');
                    });
                }
            })
        }
    }

    $scope.actualizarAtencionSinMesa = function (mesaId) {
        if ($scope._banderaRealizandoseOrden == false) {
            $scope.nuevaAtencionSinMesa = false;
            $scope._banderaMesaSeleccionada = true;
            $scope.detallesDeOrdenesVisibles = [];
            $scope.obtenerAtencionDeMesa(mesaId).then(result => {
                $("#tomarPedido").collapse($scope.atencionActual.Ordenes.length > 0 ? 'hide' : 'show');
                $("#ordenes").collapse($scope.atencionActual.Ordenes.length > 0 ? 'show' : 'hide');
            })
        } else {
            $scope.alertaSalvarAtencion().then((result) => {
                if (result.isConfirmed) {
                    $scope.limpiarAtencion();
                    $scope.limpiarOrden();
                    $scope.nuevaAtencionSinMesa = false;
                    $scope._banderaMesaSeleccionada = true;
                    $scope._banderaRealizandoseOrden = false;
                    $scope.detallesDeOrdenesVisibles = [];
                    $scope.obtenerAtencionDeMesa(mesaId).then(result => {
                        $("#tomarPedido").collapse($scope.atencionActual.Ordenes.length > 0 ? 'hide' : 'show');
                        $("#ordenes").collapse($scope.atencionActual.Ordenes.length > 0 ? 'show' : 'hide');
                    });
                } else {

                }
            })
        }
    }

    $scope.imprimirCuenta = function (atencion) {
        jsWebClientPrint.print('tipoDocumento=2&idDocumento=' + atencion.Id);
    }

    $scope.blobPdfFromBase64String = function (base64String) {
        const byteArray = Uint8Array.from(
            atob(base64String)
                .split('')
                .map(char => char.charCodeAt(0))
        );
        return new Blob([byteArray], { type: 'application/pdf' });
    };

    $scope.imprimirCuentaDirecta = function (atencion) {
        restauranteService.obtenerAtencionPDFString({ idAtencion: atencion.Id }).success(function (data) {
            //const blob = $scope.blobPdfFromBase64String(data);
            //const blobUrl = URL.createObjectURL(blob);
            /*window.open(blobUrl, '_blank');*/
            //var printWin = window.open(blobUrl, '');;
            //printWin.print();
            //printWin.focus();
            /*printJS({
                printable: data,
                type: 'pdf',
                base64: true
            })*/
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.seleccionarAtencionConMesa = function () {
        if ($scope.atencionConMesa == 'false' && $scope.ambientesConMesa.length > 0) {
            if ($scope._banderaRealizandoseOrden == true) {
                $scope.alertaSalvarAtencion().then(result => {
                    if (result.isConfirmed) {
                        $scope.atencionConMesa = 'true';
                        let numeroDeAmbiente = 1;
                        $scope._banderaRealizandoseOrden = false;
                        $scope.cambioAmbienteConMesa(numeroDeAmbiente);
                        $scope.limpiarAtencionOrden();
                    } else {
                        let radioModoAtencion = document.getElementById('atencionsinmesa');
                        radioModoAtencion.checked = true;
                    }
                })
            } else {
                $scope.atencionConMesa = 'true';
                let numeroDeAmbiente = 1;
                $scope.cambioAmbienteConMesa(numeroDeAmbiente);
                $scope.limpiarAtencionOrden();
            }
        }
    }

    $scope.seleccionarAtencionSinMesa = function () {
        if ($scope.atencionConMesa == 'true' && $scope.ambientesSinMesa.length > 0) {
            if ($scope._banderaRealizandoseOrden == true) {
                $scope.alertaSalvarAtencion().then(result => {
                    if (result.isConfirmed) {
                        $scope.atencionConMesa = 'false';
                        let numeroDeAmbiente = 1;
                        $scope._banderaRealizandoseOrden = false;
                        $scope.cambioAmbienteSinMesa(numeroDeAmbiente);
                    } else {
                        let radioModoAtencion = document.getElementById('atencionconnmesa');
                        radioModoAtencion.checked = true;
                    }
                })
            } else {
                $scope.atencionConMesa = 'false';
                let numeroDeAmbiente = 1;
                $scope.cambioAmbienteSinMesa(numeroDeAmbiente);
            }
        }
    }

    $scope.limpiarAtencionOrden = function () {
        $scope.limpiarAtencion();
        $scope.limpiarOrden();
        $scope._banderaRealizandoseOrden = false;
        $scope._banderaMesaSeleccionada = false;
    }

    $scope.iniciarAtencionPorLista = function () {
        if ($scope.ambienteActual.EsPuntoDelivery) {
            $scope.seleccionarAtencionDelivery();
        } else {
            $scope.seleccionarAtencionAlPaso();
        }
        $scope.nuevaAtencionSinMesa = true;
        $scope._banderaMesaSeleccionada = true;
        $scope.detallesDeOrdenesVisibles = [];
        $("#tomarPedido").collapse('show');
        $("#ordenes").collapse('hide');
    }

    $scope.nuevaAtencionPorLista = function () {
        if ($scope._banderaRealizandoseOrden == true) {
            $scope.alertaSalvarAtencion().then(result => {
                if (result.isConfirmed) {
                    $scope.limpiarAtencionOrden();
                    $scope.iniciarAtencionPorLista();
                }
            })
        } else {
            $scope.limpiarAtencionOrden();
            $scope.iniciarAtencionPorLista();
        }
    }

    $scope.seleccionarAtencionDelivery = function () {
        $scope.atencionDelivery = 'true';
        let radioModoAtencion = document.getElementById('atenciondelivery');
        radioModoAtencion.checked = true;
    }

    $scope.seleccionarAtencionAlPaso = function () {
        $scope.atencionDelivery = 'false';
        let radioModoAtencion = document.getElementById('atencionalpaso');
        radioModoAtencion.checked = true;
    }


    $scope.cargarAmbienteSinMesaPorDefecto = function () {
        if (!ParametrosDeConfiguracion.PermitirVentaEnMesa) {
            $scope.cambioAmbienteSinMesa($scope.numeroDeAmbiente);
        }
    }
});
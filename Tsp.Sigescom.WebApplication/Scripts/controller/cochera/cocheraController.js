app.controller('cocheraController', function ($scope, $q, $timeout, $rootScope, SweetAlert, $filter, DTOptionsBuilder, DTColumnDefBuilder, cocheraService, ventaService, maestroService, clienteService, productoService, centroDeAtencionService, empleadoService, conceptoService, $http) {
    //$scope.comprobante = { Tipo: "BV", Numero: 123, Importe: 25.00 };
    //$scope.total = { Id: 1, Importe: 30 };
    //ctrl = this;

    //#region BANDEJA DE COCHERA
    $scope.iniciarBandejaDeCochera = function () {
        //Cargar parametros
        $scope.fechaInicio = fechaInicio;
        $scope.fechaFin = fechaFin;
        $scope.IdEstadoFinalizado = IdEstadoFinalizado;
        //Listar bandeja cochera
        $scope.listarBandejaCochera();
        $scope.inicializar();
    };

    $scope.listarBandejaCochera = function () {
        cocheraService.obtenerMovimientosCochera({ desde: $scope.fechaInicio, hasta: $scope.fechaFin }).success(function (data) {
            $scope.movimientosCochera = data;
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema", data.error);
        });
    };

    $rootScope.dtOptions.withLightColumnFilter({
        '0': { type: 'text', className: 'form-control padding-left-right-3' },
        '1': { type: 'text', className: 'form-control padding-left-right-3' },
        '2': { type: 'text', className: 'form-control padding-left-right-3' },
        '3': { type: 'text', className: 'form-control padding-left-right-3' },
        '4': { type: 'text', className: 'form-control padding-left-right-3' },
        '5': { type: 'text', className: 'form-control padding-left-right-3' },
    });
    //#endregion

    //#region INICIO DE VARIABLES
    $scope.inicializar = function () {
        $scope.cargarParametros();
        $scope.limpiarRegistroIngreso();
        $scope.cargarColeccionesAsync();
        $scope.cargarColeccionesSync().then(function (resultado_) {
            $scope.establecerDatosPorDefecto();
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
        $scope.estaRegistrandoIngreso = false;
        $scope.estaRegistrandoVehiculo = false;
    };

    $scope.inicializarRegistroVehiculo = function () {
        $scope.estaRegistrandoVehiculo = true;
        $scope.limpiarRegistroVehiculo();
        $scope.establecerDatosPorDefectoRegistroVehiculo();
    };

    $scope.inicializarRegistroIngreso = function () {
        $scope.limpiarRegistroIngreso();
        $scope.establecerClientePorDefecto();
        // $scope.selectorActorComercialAPI()
    };

    $scope.inicializarRegistroSalida = function () {
        $scope.limpiarRegistroSalida();
    };

    $scope.cargarParametros = function () {
        $scope.IdSistemDePagoPlanaPorTurnos = IdSistemDePagoPlanaPorTurnos;
        $scope.IdSistemDePagoPorHora = IdSistemDePagoPorHora;
        $scope.IdSistemDePagoAbonados = IdSistemDePagoAbonados;
        $scope.IdEstadoIngresado = IdEstadoIngresado;
        $scope.PrecioPerdidaTicket = PrecioPerdidaTicket;
        $scope.IdMedioPagoDefault = IdMedioPagoDefault;
        $scope.rolCliente = { Id: IdRolCliente, Nombre: NombreRolCliente };
        $scope.AplicaLeyAmazonia = AplicaLeyAmazonia;
        $scope.TasaIGV = TasaIGV;
        $scope.TiempoEsperaBusquedaSelector = TiempoEsperaBusquedaSelector;
        $scope.MinimoCaracteresBuscarActorComercial = MinimoCaracteresBuscarActorComercial;
        $scope.MascaraDeVisualizacionValidacionRegistroCliente = MascaraDeVisualizacionValidacionRegistroCliente;
    }; 

    $scope.limpiarRegistroVehiculo = function () {
        $scope.vehiculoIngreso = {
            TipoDeVehiculo: {}, Marca: {}, Observacion: "Ninguna", Color: ""
        };
    };

    $scope.limpiarRegistroIngreso = function () {
        $scope.ingreso = { Observacion: '' };
    };

    $scope.limpiarRegistroSalida = function () {
        $scope.salida = { Observacion: '' };
    };

    $scope.cargarColeccionesAsync = function () {
    };

    $scope.cargarColeccionesSync = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        var promiseList = [];
        promiseList.push($scope.obtenerTiposDeVehiculos());
        promiseList.push($scope.obtenerMarcasDeVehiculos());
        promiseList.push($scope.obtenerSistemaDePagoParaCochera());
        return $q.all(promiseList).then(function (response) {
            defered.resolve();
        }).catch(function (error) {
            defered.reject(e);
        });
    };

    $scope.establecerDatosPorDefecto = function () {
        $scope.ingreso.SistemaDePago = $scope.listaDeSistemaDePagoCochera[0];
        $scope.establecerClientePorDefecto();
        $scope.inicializarRegistroVehiculo();
        $scope.estaRegistrandoVehiculo = false;
        $scope.estaRegistrandoSalida = false;

    };

    $scope.establecerDatosPorDefectoRegistroVehiculo = function () {
        $scope.vehiculoIngreso.TipoDeVehiculo = $scope.listaDeTiposDeVehiculos[0];
        $scope.vehiculoIngreso.Marca = $scope.listaDeMarcasDeVehiculos[0];
    };

    $scope.establecerClientePorDefecto = function () {
        ventaService.obtenerClientePorDefecto({}).success(function (data) {
            $scope.ingreso.cliente = data;
        }).error(function (data) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    };

    $scope.cambioCliente = function () {
        //$scope.ingreso.cliente = clienteIngresado;
        //ctrl.asignarComprobantePorDefecto(ctrl.facturacion.Orden.Cliente.Id);
        //ctrl.changeValueComprobante();
        //ctrl.validarCliente();
    };

    $scope.obtenerTiposDeVehiculos = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        cocheraService.obtenerTiposDeVehiculo({}).success(function (data) {
            $scope.listaDeTiposDeVehiculos = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            SweetAlert.error("Ocurrio un Problema", data.error);
            defered.reject(data);
        });
        return promise;
    };

    $scope.obtenerMarcasDeVehiculos = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        cocheraService.obtenerMarcasDeVehiculo({}).success(function (data) {
            $scope.listaDeMarcasDeVehiculos = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            SweetAlert.error("Ocurrio un Problema", data.error);
            defered.reject(data);
        });
        return promise;
    };

    $scope.obtenerSistemaDePagoParaCochera = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        cocheraService.obtenerSistemaDePagoParaCochera({}).success(function (data) {
            $scope.listaDeSistemaDePagoCochera = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    };
    //#endregion

    //#region OPERACIONES
    $scope.validarPlaca = function (placa) {
        if (placa) {
            cocheraService.obtenerVehiculoPorPlaca({ placa: placa }).success(function (data) {
                if (data.encontrado) {
                    $scope.vehiculoIngreso = data.data;
                    $scope.ingreso.vehiculo = data.data;
                } else {
                    $scope.estaRegistrandoVehiculo = true;
                }
                $scope.estaRegistrandoIngreso = true;
                $scope.establecerClientePorDefecto();
            }).error(function (data) {
                SweetAlert.error("Ocurrio un Problema", data.error);
            });
        }
    };

    $scope.activarRegistro = function ($event) {
        $scope.estaRegistrandoIngreso = true;
    }

    $scope.obtenerVehiculoParaEditarPorPlaca = function (placa) {
        if (placa) {
            cocheraService.obtenerVehiculoParaEditarPorPlaca({ placa: placa }).success(function (data) {
                $scope.vehiculoIngreso = data;
            }).error(function (data) {
                SweetAlert.error("Ocurrio un Problema", data.error);
            });
        }
    };



    $scope.registrarIngreso = function () {
        cocheraService.registrarIngreso({ ingreso: $scope.ingreso }).success(function (data) {
            $scope.limpiarRegistroIngreso();
            SweetAlert.success("Correcto", data.result_description);
            comprobantesAImprimir = [data.information];
            $scope.imprimirComprobantes(data.information, data.information);
            $scope.estaRegistrandoIngreso = false;
            $scope.listarBandejaCochera();
            $scope.inicializarRegistroIngreso();
            $scope.establecerDatosPorDefecto();
        }).error(function (data, status) {
            SweetAlert.error2(data);
        });
    };

    $scope.imprimirComprobantes = function (documento1, documento2) {
        let documento1Html = $scope.construirHtmlAImprimir(documento1);
        var pdf1 = new jsPDF('p', 'pt', [226.77, 800]);
        pdf1.html(documento1Html, {
            callback: function (pdf1) {
                let documento2Html = $scope.construirHtmlAImprimir(documento2);
                var pdf2 = new jsPDF('p', 'pt', [226.77, 800]);
                pdf2.html(documento2Html, {
                    callback: function (pdf2) {
                        JSPM.JSPrintManager.start()
                            .then(_ => {
                                var cpj = new JSPM.ClientPrintJob();
                                cpj.clientPrinter = new JSPM.DefaultPrinter();
                                var my_file = new JSPM.PrintFilePDF(pdf1.output('datauristring'), JSPM.FileSourceType.URL, 'test.pdf', 1);
                                if (documento2 !== null) var my_file2 = new JSPM.PrintFilePDF(pdf2.output('datauristring'), JSPM.FileSourceType.URL, 'test.pdf', 1);
                                cpj.files.push(my_file);
                                if (documento2 !== null) cpj.files.push(my_file2);
                                cpj.sendToClient();
                            })
                            .catch((e) => {
                                alert(e);
                            });
                    }
                });
            }
        });
    };

    $scope.construirHtmlAImprimir = function (documento) {
        let documentoHtml = document.createElement("div");
        documentoHtml.id = "documentoHtml";
        documentoHtml.style.cssText = "width:55mm; border:solid; border-width: thin; font-family: Arial; font-size-adjust:none";
        documentoHtml.innerHTML = documento;
        document.getElementById("htmlStringDocumento").innerHTML = '';
        document.getElementById("htmlStringDocumento").appendChild(documentoHtml);
        let documentoHtmlAImprimir = document.getElementById("documentoHtml");
        return documentoHtmlAImprimir;
    };

    $scope.cargarDatosParaSalida = function (placa) {
        if (placa) {
            cocheraService.obtenerMovimientoParaSalida({ placa: placa }).success(function (data) {
                if (data.encontrado) {
                    $scope.salida = data.data;
                    $scope.estaRegistrandoSalida = true;
                    $scope.salida.GrabaIgv = !$scope.AplicaLeyAmazonia;
                    $scope.calcularTotalAPagar();
                    $scope.facturacionAPI.InitSeleccionMedioPago();
                } else {
                    SweetAlert.warning("ATENCIÓN", "Vehículo no encontrado");
                    $scope.estaRegistrandoSalida = false;
                    $scope.placaSalida = "";
                }
            }).error(function (data) {
                SweetAlert.error("Ocurrio un Problema", data.error);
            });
        }
    };

    $scope.calcularCostoPerdidaTicket = function () {
        $scope.salida.DetallesACobrar.Ticket = $scope.salida.PerdidaTicket ? $scope.PrecioPerdidaTicket : 0;
        $scope.calcularTotalAPagar();
    }

    $scope.calcularTotalAPagar = function () {
        $scope.salida.DetallesACobrar.Total = parseFloat($scope.salida.DetallesACobrar.Ticket) + parseFloat($scope.salida.DetallesACobrar.Principal) + parseFloat($scope.salida.DetallesACobrar.Exceso);
        $scope.salida.DetallesACobrar.Igv = $scope.salida.GrabaIgv ? parseFloat($scope.salida.DetallesACobrar.Total - ($scope.salida.DetallesACobrar.Total / (1 + $scope.TasaIGV))).toFixed(2) : 0;
        $scope.salida.DetallesACobrar.SubTotal = $scope.salida.DetallesACobrar.Total - $scope.salida.DetallesACobrar.Igv;
        $scope.facturacionAPI.SetTotalVenta($scope.salida.DetallesACobrar.Total);
    }

    $scope.cambiarAfeccionIgv = function (aplicarIgv) {
        $scope.salida.GrabaIgv = aplicarIgv;//($scope.salida.GrabaIgv == true) ? false : true;
        $scope.calcularTotalAPagar();
    }

    $scope.registrarSalida = function () {
        cocheraService.registrarSalida({ salida: $scope.salida, datosVentaIntegrada: $scope.facturacionPago }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.imprimirComprobantes(data.ticket, data.comprobanteVenta);
            $scope.estaRegistrandoSalida = false;
            $scope.inicializarRegistroSalida;
            $scope.listarBandejaCochera();
        }).error(function (data, status) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    };

    $scope.cancelarRegistrarIngreso = function (form) {
        if (form) {
            form.$setPristine();
            form.$setUntouched();
        }
        $scope.estaRegistrandoIngreso = false;
        $scope.limpiarRegistroIngreso();
        $scope.establecerDatosPorDefecto();

    };

    $scope.cancelarRegistrarSalida = function () {
        $scope.estaRegistrandoSalida = false;
        $scope.limpiarRegistroSalida();

    };

    $scope.tipoComprobante = {
        nombre: 'BV'
    };
    //#endregion
});

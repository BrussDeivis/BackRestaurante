app.controller('consultarVentasController', function ($scope, $q, $http, $rootScope, $timeout, SweetAlert, DTOptionsBuilder, DTColumnDefBuilder, ventaService, maestroService,) {

    //#region BANDEJA DE VENTAS
    $scope.inicializarBandejaDeVentas = function () {
        $scope.verDocumento = {};
        $scope.fechaInicio = fechaInicio;
        $scope.permitirClonarVenta = permitirClonarVenta;
        $scope.idRolCliente = idRolCliente;
        $scope.tiempoEsperaBusquedaSelector = tiempoEsperaBusquedaSelector;
        $scope.minimoCaracteresBuscarActorComercial = minimoCaracteresBuscarActorComercial;
        $scope.mostrarSelectorClienteEnVerVentas = mostrarSelectorClienteEnVerVentas;
        $scope.mostrarBuscadorComprobanteEnVerVentas = mostrarBuscadorComprobanteEnVerVentas;
        $scope.permitirVentaAlCredito = permitirVentaAlCredito;
        $scope.idMedioDePagoEfectivo = idMedioDePagoEfectivo;
        document.getElementById('fechaInicio').value = $scope.fechaInicio;
        $scope.fechaFin = fechaFin;
        document.getElementById('fechaFin').value = $scope.fechaFin;
        $scope.clienteSeleccionado = {};
        $scope.comprobanteIngresado = '';
        $scope.inicializacionRealizada = true;
        $scope.busqueda = {};
        $scope.busqueda.FiltroFecha = 'true';
        $scope.busqueda.TieneClienteSeleccionado = false;
        $scope.canjeDeComprobanteActivado = false;
        $scope.permitirEnvioPorWhatsApp = permitirEnvioPorWhatsApp;
        $scope.envioComprobantePostVenta = envioComprobantePostVenta;
        $scope.listarBandeja();
        $scope.iniciarCanjeDeComprobante();
    };

    $scope.listarBandeja = function () {
        $scope.fechaInicio = document.getElementById('fechaInicio').value;
        $scope.fechaFin = document.getElementById('fechaFin').value;
        $scope.comprobanteIngresado = $scope.busqueda.FiltroFecha == 'false' ? $scope.comprobanteIngresado : '';
        if ($scope.fechaInicio !== '' && $scope.fechaFin !== '') {
            ventaService.obtenerResumenesVentas({ desde: $scope.fechaInicio, hasta: $scope.fechaFin, idCliente: $scope.clienteSeleccionado.Id, comprobante: $scope.comprobanteIngresado }).success(function (data) {
                $scope.ventas = data;
            }).error(function (data) {
                SweetAlert.error2(data);
            });
        }
    };

    $scope.cargarClientesPorBusqueda = function (query) {
        var reqStr = URL_ + "/ActorComercial/ObtenerActoresComercialesPorRolYBusqueda";
        $http.post(reqStr, { idRol: $scope.idRolCliente, cadenaBusqueda: query }).then(
            function (response) {
                $scope.clientesPorBusqueda = response.data;
            },
            function () {
                SweetAlert.error("Ocurrio un Problema", response.error);
            }
        );
    }

    $scope.seleccionarActorComercial = function (clienteSeleccionado) {
        $scope.clienteSeleccionado = clienteSeleccionado;
        $scope.busqueda.TieneClienteSeleccionado = true;

    }

    $scope.limpiarClienteSeleccionado = function () {
        $scope.clienteSeleccionado = {};
        $scope.busqueda.TieneClienteSeleccionado = false;
    }


    $rootScope.dtOptions.withLightColumnFilter({
        //'0': { type: 'text', className: 'form-control padding-left-right-3' },
        '1': { type: 'text', className: 'form-control padding-left-right-3' },
        '2': { type: 'text', className: 'form-control padding-left-right-3' },
        '3': { type: 'text', className: 'form-control padding-left-right-3' },
        '4': { type: 'text', className: 'form-control padding-left-right-3' },
        '5': { type: 'text', className: 'form-control padding-left-right-3' },
        '6': { type: 'text', className: 'form-control padding-left-right-3' },
        '7': { type: 'text', className: 'form-control padding-left-right-3' },
        '8': { type: 'text', className: 'form-control padding-left-right-3' },
        '9': { type: 'text', className: 'form-control padding-left-right-3' },
        '10': { type: 'text', className: 'form-control padding-left-right-3' },
        '11': { type: 'text', className: 'form-control padding-left-right-3' },
    });
    //#endregion

    //#region VER DOCUMENTO 
    $scope.inicializarVerDeDocumento = function (item) {
        $scope.verDocumento = {};
        $scope.serieDocumentoSeleccionado = item.Numero;
        $scope.formato80 = 1;
        $scope.formatoA4 = 2;
        $scope.hayRegistroDeNota = false;
        $scope.EsOrdenDeVenta = item.EsOrden;
        $scope.IdOrden = item.Id;
        $scope.numeroDeGuiasDeRemision = [];
        $scope.obtenerFormatosDeImpresion().then(function (resultado_) {
            $scope.formato = $scope.formatosDeImpresion[0];
            $scope.obtenerDocumento(item.Id);
            //$timeout(function () { $('#comboFormato').trigger("change"); }, 100);
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
    }

    $scope.obtenerDocumento = function (id) {
        ventaService.obtenerDocumentoParaOperacionesEnVenta({ idOrdenDeVenta: id }).success(function (data) {
            $scope.verDocumento = data;
            $scope.verDocumento.CadenaHtmlDeComprobante = $scope.formato.Id === $scope.formato80 ? $scope.verDocumento.CadenaHtmlDeComprobante80 : $scope.verDocumento.CadenaHtmlDeComprobanteA4;
            $scope.tamanioComprobante = $scope.formato.Id === $scope.formato80 ? '80mm' : "210mm";
            document.getElementById("pdfDocumento").innerHTML = $scope.verDocumento.CadenaHtmlDeComprobante;
            $timeout(function () { $('#pdfDocumento').trigger("change"); }, 100);
            if ($scope.verDocumento.TieneGuiaDeRemision) {
                var stringHtml = "";
                $scope.verDocumento.CadenasHtmlDeGuiaDeRemision = $scope.formato.Id == $scope.formato80 ? $scope.verDocumento.CadenasHtmlDeGuiaDeRemision80 : $scope.verDocumento.CadenasHtmlDeGuiaDeRemisionA4;
                for (var i = 0; i < $scope.verDocumento.CadenasHtmlDeGuiaDeRemision.length; i++) {
                    stringHtml = stringHtml + ' <div class="col-md-12"> ' +
                        ' <div id = "pdfGuia' + i + '" style = "width:' + $scope.tamanioComprobante + '; border:solid; border-width: thin; font-family: auto; margin:auto; margin-top:15px" > ' +
                        $scope.verDocumento.CadenasHtmlDeGuiaDeRemision[i] +
                        ' </div > ' +
                        ' </div > ';
                }
                document.getElementById("pdfGuia").innerHTML = stringHtml;
                $timeout(function () { $('#pdfGuia').trigger("change"); }, 100);
            }
        }).error(function (data) {
            SweetAlert.error2(data);
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

    $scope.cargarDocumento_ = function () {
        if ($scope.verDocumento.TieneGuiaDeRemision) {
            ventaService.obtenerHtmlVentaYGuiaDeRemision({ idOrdenDeVenta: $scope.verDocumento.IdOrden, formato: $scope.formato.Id }).success(function (data) {
                $scope.tamanioComprobante = $scope.formato.Id === $scope.formato80 ? '80mm' : "210mm";
                $scope.verDocumento.CadenaHtmlDeComprobante = data.CadenaHtmlDeComprobante;
                document.getElementById("pdfDocumento").innerHTML = data.CadenaHtmlDeComprobante;
                $timeout(function () { $('#pdfDocumento').trigger("change"); }, 100);
                if ($scope.verDocumento.TieneGuiaDeRemision) {
                    var stringHtml = "";
                    $scope.verDocumento.CadenasHtmlDeGuiaDeRemision = data.CadenasHtmlDeGuiaDeRemision;
                    for (var i = 0; i < $scope.verDocumento.CadenasHtmlDeGuiaDeRemision.length; i++) {
                        stringHtml = stringHtml + ' <div class="col-md-12"> ' +
                            ' <div id = "pdfGuia' + i + '" style = "width:' + $scope.tamanioComprobante + '; border:solid; border-width: thin; margin:auto; margin-top:15px" > ' +
                            $scope.verDocumento.CadenasHtmlDeGuiaDeRemision[i] +
                            ' </div > ' +
                            ' </div > ';
                    }
                    document.getElementById("pdfGuia").innerHTML = stringHtml;
                    $timeout(function () { $('#pdfGuia').trigger("change"); }, 100);
                }
            }).error(function (data) {
                SweetAlert.error2(data);
            });
        } else {
            ventaService.obtenerHtmlVenta({ idOrdenDeVenta: $scope.verDocumento.IdOrden, formato: $scope.formato.Id }).success(function (data) {
                $scope.tamanioComprobante = $scope.formato.Id === $scope.formato80 ? '80mm' : "210mm";
                $scope.verDocumento.CadenaHtmlDeComprobante = data;
                document.getElementById("pdfDocumento").innerHTML = data;
                $timeout(function () { $('#pdfDocumento').trigger("change"); }, 100);
            }).error(function (data) {
                SweetAlert.error2(data);
            });
        }
    }

    $scope.cargarDocumento = function () {
        $scope.tamanioComprobante = $scope.formato.Id === $scope.formato80 ? '80mm' : "210mm";
        $scope.verDocumento.CadenaHtmlDeComprobante = $scope.formato.Id === $scope.formato80 ? $scope.verDocumento.CadenaHtmlDeComprobante80 : $scope.verDocumento.CadenaHtmlDeComprobanteA4;
        document.getElementById("pdfDocumento").innerHTML = $scope.verDocumento.CadenaHtmlDeComprobante;
        $timeout(function () { $('#pdfDocumento').trigger("change"); }, 100);
        if ($scope.verDocumento.TieneGuiaDeRemision) {
            var stringHtml = "";
            $scope.verDocumento.CadenasHtmlDeGuiaDeRemision = $scope.formato.Id === $scope.formato80 ? $scope.verDocumento.CadenasHtmlDeGuiaDeRemision80 : $scope.verDocumento.CadenasHtmlDeGuiaDeRemisionA4;
            for (var i = 0; i < $scope.verDocumento.CadenasHtmlDeGuiaDeRemision.length; i++) {
                stringHtml = stringHtml + ' <div class="col-md-12"> ' +
                    ' <div id = "pdfGuia' + i + '" style = "width:' + $scope.tamanioComprobante + '; border:solid; border-width: thin; margin:auto; margin-top:15px" > ' +
                    $scope.verDocumento.CadenasHtmlDeGuiaDeRemision[i] +
                    ' </div > ' +
                    ' </div > ';
            }
            document.getElementById("pdfGuia").innerHTML = stringHtml;
            $timeout(function () { $('#pdfGuia').trigger("change"); }, 100);
        }
    }

    $scope.cerrarVerDocumento = function () {
        $scope.hayRegistroDeNota = false;
        $scope.verDocumento = {};
        $scope.formato = {};
        document.getElementById("pdfDocumento").innerHTML = '';
        $timeout(function () { $('#pdfDocumento').trigger("change"); }, 100);
        document.getElementById("pdfGuia").innerHTML = '';
        $timeout(function () { $('#pdfGuia').trigger("change"); }, 100);
        $('#modal-ver-venta').modal('hide');
    }

    //#endregion

    //#region Envio de documento por correo
    $scope.inicializarEnvioDocumento = function () {
        $scope.envio = {};
        $scope.envio.ModoEnvio = $scope.permitirEnvioPorWhatsApp ? 1 : 2;
        let radioEtiqueta = document.getElementById('modoenvio-' + $scope.envio.ModoEnvio);
        radioEtiqueta.checked = true;
        if ($scope.permitirEnvioPorWhatsApp) {
            $scope.SeleccionarEnvioPorWhatsApp();
            $scope.envio.NumeroCelular = $scope.verDocumento.TelefonoCliente;
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
        $scope.envio.UrlWhatsApp = 'https://api.whatsapp.com/send?phone=' + $scope.envio.CodigoPais + $scope.envio.NumeroCelular + '&text=Estimado%20cliente%2C%0ASe%20env%C3%ADa%20el%20documento%20' + $scope.verDocumento.SerieNumeroDocumento + '.%20Para%20ver%20click%20en%20el%20siguiente%20enlace%3A%0A%0A' + URL_ + '/Comprobante/DescargarComprobante?id=' + $scope.verDocumento.IdEncriptado;
    }

    $scope.enviarWhatsApp = function () {
        $('#modal-envio-documento').modal('hide');
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
    //#endregion

    //#region Impresion de documento  
    $scope.imprimirDocumento = function () {
        $scope.tamanioComprobante = $scope.formato.Id === $scope.formato80 ? '80mm' : "210mm";
        let ventanaImpresion = window.open(' ', 'popimpr');
        ventanaImpresion.document.write($scope.verDocumento.CadenaHtmlDeComprobante);
        ventanaImpresion.document.close();
        ventanaImpresion.print();
        ventanaImpresion.close();
        //var ficha = document.getElementById(id);
        for (var i = 0; i < $scope.verDocumento.CadenasHtmlDeGuiaDeRemision.length; i++) {
            $scope.tamanioComprobante = $scope.formato.Id === $scope.formato80 ? '80mm' : "210mm";
            let ventanaImpresion = window.open(' ', 'popimpr');
            ventanaImpresion.document.write($scope.verDocumento.CadenasHtmlDeGuiaDeRemision[i]);
            ventanaImpresion.document.close();
            ventanaImpresion.print();
            ventanaImpresion.close();
        }
    }


    //#endregion

    //#region Invalidar Documento
    $scope.iniciarInvalidacion = function () {
        $scope.limpiarObservacion();
    }

    $scope.limpiarObservacion = function () {
        $scope.invalidacion = {};
        $scope.invalidacion.Id = $scope.IdOrden;
        $scope.invalidacion.Pago = { ModoDePago: 1, Traza: { MedioDePago: {}, Info: { EntidadFinanciera: {}, OperadorTarjeta: {}, ImporteAPagar: $scope.verDocumento.TotalMovimientoEconomico } }, Caja: {}, Cajero: {} };
        $scope.invalidacion.EntregaDiferida = 'false';
        $scope.invalidacion.ImporteTotal = $scope.verDocumento.TotalMovimientoEconomico;
        $scope.editorPagoInvalidarAPI.RestablecerInfoPago();
        $scope.editorPagoInvalidarAPI.VerificarMediosDePagoAMostrar($scope.verDocumento.IdCliente);
        $scope.editorPagoInvalidarAPI.SetImporteAPagar($scope.verDocumento.TotalMovimientoEconomico);
    }

    $scope.invalidarDocumento = function () {
        ventaService.invalidarVenta({ invalidarVenta: $scope.invalidacion }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $('#modal-invalidar-documento').modal('hide');
            $('#modal-ver-venta').modal('hide');
            $scope.listarBandeja();
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.cambioPago = function (pago) {
        $scope.invalidacion.Pago = pago;
    }

    $scope.hayInconsistenciasInvalidacion = function () {
        $scope.inconsistenciasInvalidacion = [];
        if ($scope.invalidacion != undefined) {
            if ($scope.invalidacion.Pago != undefined) {
                if ($scope.invalidacion.Pago.ModoDePago == 2) {
                    if ($scope.invalidacion.Pago.Inicial != null && $scope.invalidacion.Pago.Inicial != 0) {
                        if (parseFloat($scope.invalidacion.Pago.Inicial) > parseFloat($scope.invalidacion.Pago.Total)) {
                            $scope.inconsistenciasInvalidacion.push("Es necesario que el monto inicial sea menor al total de venta.");
                        }
                    }
                } else if ($scope.invalidacion.Pago.ModoDePago == 3) {
                    if ($scope.invalidacion.Pago.Cuotas == undefined) {
                        $scope.inconsistenciasInvalidacion.push("Es necesario al seleccionar credito configurado genere las cuotas respectivas.");
                    }
                }
                if ($scope.invalidacion.Pago.Traza.MedioDePago.Id == $scope.idMedioDePagoEfectivo) {
                    if ($scope.invalidacion.Pago.Traza.Info.ImporteEntregado != null && $scope.invalidacion.Pago.Traza.Info.ImporteEntregado != 0) {
                        if (parseFloat($scope.invalidacion.Pago.Traza.Info.ImporteEntregado) < parseFloat($scope.invalidacion.Pago.Traza.Info.ImporteAPagar)) {
                            $scope.inconsistenciasInvalidacion.push("Es necesario que el monto a recibir debe de ser mayor al importe total de la venta.");
                        }
                    }
                }
            }
        }
        return $scope.inconsistenciasInvalidacion.length > 0;
    }

    $scope.inicioRealizadoPago = function () {

    }
    //#endregion

    //#region Notas de debito y credito  

    $scope.iniciarEmisionDeNotaDeDebito = function () {
        $scope.hayRegistroDeNota = true;
        $scope.registroNotaAPI.IniciarNotaDebito();
    }

    $scope.iniciarEmisionDeNotaDeCredito = function () {
        $scope.hayRegistroDeNota = true;
        $scope.registroNotaAPI.IniciarNotaCredito();
    }

    $scope.cancelarRegistroNota = function () {
        $scope.hayRegistroDeNota = false;
    }

    $scope.guardadoRegistroNota = function (idOrdenNota) {
        jsWebClientPrint.print('idVenta=' + idOrdenNota);
        $('#modal-ver-venta').modal('hide');
        $scope.listarBandeja();
    }



    /* 
  
      $scope.cargarDatosParaEmisionDeNota = function (esParaNotaDeDebito) {
          $scope.limpiarNotaDeDocumento();
          $scope.cargarParametrosParaNota();
          $scope.cargarColeccionesSyncParaNota(esParaNotaDeDebito).then(function (resultado_) {
              $scope.establecerDatosPorDefectoParaNota(esParaNotaDeDebito);
          }, function (error) {
              $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
          });
      }
  
      $scope.limpiarNotaDeDocumento = function () {
          $scope.notaDeDocumento = {};
      }
  
      $scope.cargarParametrosParaNota = function () {
          $scope.idDetalleMaestroAnulacionDeLaOperacion = idDetalleMaestroAnulacionDeLaOperacion;
          $scope.idDetalleMaestroAnulacionPorErrorEnElRuc = idDetalleMaestroAnulacionPorErrorEnElRuc;
          $scope.idDetalleMaestroCorreccionPorErrorEnLaDescripcion = idDetalleMaestroCorreccionPorErrorEnLaDescripcion;
          $scope.idDetalleMaestroDescuentoGlobal = idDetalleMaestroDescuentoGlobal;
          $scope.idDetalleMaestroDescuentoPorItem = idDetalleMaestroDescuentoPorItem;
          $scope.idDetalleMaestroDevolucionTotal = idDetalleMaestroDevolucionTotal;
          $scope.idDetalleMaestroDevolucionPorItem = idDetalleMaestroDevolucionPorItem;
          $scope.idDetalleMaestroBonificacion = idDetalleMaestroBonificacion;
          $scope.idDetalleMaestroDisminucionEnElValor = idDetalleMaestroDisminucionEnElValor;
          $scope.idDetalleMaestroOtrosConceptos = idDetalleMaestroOtrosConceptos;
          $scope.idDetalleMaestroInteresesPorMora = idDetalleMaestroInteresesPorMora;
          $scope.idDetalleMaestroAumentoEnElValor = idDetalleMaestroAumentoEnElValor;
          $scope.idDetalleMaestroPenalidadesYOtrosConceptos = idDetalleMaestroPenalidadesYOtrosConceptos;
          $scope.numeroDecimalesEnCantidad = numeroDecimalesEnCantidad;
          $scope.numeroDecimalesEnPrecio = numeroDecimalesEnPrecio; 
      }
  
      $scope.establecerDatosPorDefectoParaNota = function (esParaNotaDeDebito) {
          $scope.hayRegistroDeNota = true;
          $scope.notaDeOperacionAcordion = true;
          $scope.detalleDocumentoAcordion = false;
          $scope.notaDeDocumento.EsNotaDeDebito = esParaNotaDeDebito;
          $scope.tasaIGVParaNota = $scope.verDocumento.Igv > 0 ? tasaIGV : 0;
          $scope.notaDeDocumento.Detalles = angular.copy($scope.verDocumento.Detalles);
          $scope.verDocumento.DetallesOrdenAlmacen = angular.copy($scope.detallesOperacion.detallesOrdenAlmacen);
          $scope.verDocumento.DetallesTesoreria = angular.copy($scope.detallesOperacion.detallesTesoreria);
          $scope.notaDeDocumento.IdOrdenDeOperacion = $scope.IdOrden;
          $scope.mostrarIngresoDelMonto = false;
          $scope.mostrarIngresoDeDetalle = false;
          $scope.mostrarMontoDeNotaEnIngresoMonto = false;
          $scope.mostrarMontoEnDetalle = false;
          $scope.numeroDecimalesEnValorDeDetalle = 2;
          $scope.totalDeNota = 0;
          $scope.igvDeNota = 0;
          $scope.subTotalDeNota = 0;
          document.getElementById("pdfDocumentoAcordion").innerHTML = $scope.verDocumento.CadenaHtmlDeComprobante;
          $timeout(function () { $('#pdfDocumentoAcordion').trigger("change"); }, 100);
          $scope.verificarInconsistenciasDeNota();
      }
  
      $scope.cargarColeccionesSyncParaNota = function (esParaNotaDeDebito) {
          var defered = $q.defer();
          var promise = defered.promise;
          var promiseList = [];
          promiseList.push($scope.obtenerTiposDeNota(esParaNotaDeDebito));
          promiseList.push($scope.obtenerTiposDeComprobantesParaNota(esParaNotaDeDebito));
          promiseList.push($scope.obtenerDetalleParaNota());
          return $q.all(promiseList).then(function (response) {
              defered.resolve();
          }).catch(function (error) {
              defered.reject(e);
          });
      }
  
      $scope.obtenerTiposDeNota = function (esParaNotaDeDebito) {
          var defered = $q.defer();
          var promise = defered.promise;
          maestroService.obtenerTiposDeNota({ esParaNotaDeDebito }).success(function (data) {
              $scope.tiposDeNotas = data;
              defered.resolve();
          }).error(function (data) {
              SweetAlert.error2(data);
              defered.reject(data);
          });
          return promise;
      }
  
      $scope.obtenerDetalleParaNota = function () {
          var defered = $q.defer();
          var promise = defered.promise;
          ventaService.obtenerDetalleParaNotaDebitoCredito({ idOrdenVenta: $scope.IdOrden }).success(function (data) {
              $scope.detallesOperacion = data;
              defered.resolve();
          }).error(function (data) {
              SweetAlert.error2(data);
              defered.reject(data);
          });
          return promise;
      }
  
      //Metodo diferente en venta ya que en venta se enviara la serie para que pueda traer los comprobantes de acuerdo a la primera letra
      $scope.obtenerTiposDeComprobantesParaNota = function (esParaNotaDeDebito) {
          var defered = $q.defer();
          var promise = defered.promise;
          ventaService.obtenerTiposDeComprobanteParaNotaDeDebitoCreditoDeVenta({ esParaNotaDeDebito, serieComprobanteVenta: $scope.verDocumento.SerieNumeroDocumento }).success(function (data) {
              $scope.tiposDeComprobantesMasSeriesDeNotas = data;
              defered.resolve();
          }).error(function (data) {
              SweetAlert.error2(data);
              defered.reject(data);
          });
          return promise;
      }
  
      $scope.cargarSeriesParaNota = function (tipoComprobante) {
          if (tipoComprobante !== null) {
              $scope.seriesParaNota = angular.copy(tipoComprobante.Series);
          }
      }
  
      $scope.accionNotaDeOperacion = function (bandera) {
          if (bandera) {
              $scope.notaDeOperacionAcordion = { open: true };
          }
          else {
              $scope.notaDeOperacionAcordion = { open: false };
          }
      }
  
      $scope.accionDetalleDocumento = function (bandera) {
          if (bandera) {
              $scope.detalleDocumentoAcordion = { open: true };
          }
          else {
              $scope.detalleDocumentoAcordion = { open: false };
          }
      }
  
      $scope.cargarTipoDeNota = function (tipoDeNota) {
          $scope.numeroDecimalesEnValorDeDetalle = 2;
          $scope.mostrarIngresoDelMonto = false;
          $scope.mostrarIngresoDeDetalle = false;
          $scope.mostrarMontoDeNotaEnIngresoMonto = false;
          $scope.mostrarMontoEnDetalle = false;
          $scope.mostrarTotalDetalleDeNota = false;
          $scope.numeroColumnasDetalleNota = 2;
          $scope.etiquetaDeMonto = "";
          $scope.etiquetaDeDetalleDeNota = "";
          // $scope.idDetalleMaestroAnulacionDeLaOperacion // $scope.idDetalleMaestroDevolucionTotal  
          if (tipoDeNota.Id === $scope.idDetalleMaestroDescuentoGlobal) {
              $scope.mostrarIngresoDelMonto = true;
              $scope.mostrarMontoDeNotaEnIngresoMonto = true;
              $scope.notaDeDocumento.ValorDeNota = parseFloat(0).toFixed(2);
              $scope.etiquetaDeMonto = "DESCUENTO GLOBAL";
          } else if (tipoDeNota.Id === $scope.idDetalleMaestroInteresesPorMora) {
              $scope.mostrarIngresoDelMonto = true;
              $scope.mostrarMontoDeNotaEnIngresoMonto = true;
              $scope.notaDeDocumento.ValorDeNota = parseFloat(0).toFixed(2);
              $scope.etiquetaDeMonto = "INTERES TOTAL";
          } else if (tipoDeNota.Id === $scope.idDetalleMaestroDescuentoPorItem) {
              $scope.mostrarIngresoDeDetalle = true;
              $scope.mostrarMontoEnDetalle = true;
              $scope.etiquetaDeDetalleDeNota = "DESCUENTO";
          } else if (tipoDeNota.Id === $scope.idDetalleMaestroDevolucionPorItem) {
              $scope.mostrarIngresoDeDetalle = true;
              $scope.mostrarMontoEnDetalle = true;
              $scope.numeroDecimalesEnValorDeDetalle = $scope.numeroDecimalesEnCantidad;
              $scope.mostrarTotalDetalleDeNota = true;
              $scope.numeroColumnasDetalleNota = 3;
              $scope.etiquetaDeDetalleDeNota = "DEVOLUCION";
          } else if (tipoDeNota.Id === $scope.idDetalleMaestroAumentoEnElValor) {
              $scope.mostrarIngresoDeDetalle = true;
              $scope.mostrarMontoEnDetalle = true;
              $scope.etiquetaDeDetalleDeNota = "AUMENTO DEL VALOR";
          }
          $scope.limpiarDetalleDeNota();
      }
  
      $scope.guardarNotaDeDocumento = function () {
          ventaService.guardarNotaDeDebitoCreditoDeVenta({ registroDeNota: $scope.notaDeDocumento }).success(function (data) {
              SweetAlert.success("Correcto", data.result_description);
              $scope.cerrarVerDocumento();
              jsWebClientPrint.print('idVenta=' + data.data);
              $('#modal-ver-venta').modal('hide');
              $scope.listarBandeja();
          }).error(function (data) {
              SweetAlert.error2(data);
          });
      }
  
      $scope.limpiarDetalleDeNota = function () {
          for (var i = 0; i < $scope.notaDeDocumento.Detalles.length; i++) {
              if ($scope.notaDeDocumento.TipoDeNota.Id == $scope.idDetalleMaestroCorreccionPorErrorEnLaDescripcion) {
                  $scope.notaDeDocumento.Detalles[i].ValorDeDetalle = '';
              } else {
                  $scope.notaDeDocumento.Detalles[i].ValorDeDetalle = parseFloat(0).toFixed($scope.numeroDecimalesEnValorDeDetalle);
                  $scope.notaDeDocumento.Detalles[i].ValorDeDetalleIgv = parseFloat(0).toFixed(2);
              }
          }
      }
  
      $scope.calcularTotalDeNota = function () {
          var totalDeNota = 0;
          for (var i = 0; i < $scope.notaDeDocumento.Detalles.length; i++) {
              if ($scope.notaDeDocumento.Detalles[i].ValorDeDetalle != undefined) {
                  if ($scope.notaDeDocumento.TipoDeNota.Id == $scope.idDetalleMaestroDevolucionPorItem) {
                      $scope.notaDeDocumento.Detalles[i].ValorDeDetalle = $scope.notaDeDocumento.Detalles[i].ValorDeDetalle == '' ? 0 : $scope.notaDeDocumento.Detalles[i].ValorDeDetalle;
                      var totalNotaItem = parseFloat($scope.notaDeDocumento.Detalles[i].Precio) * parseFloat($scope.notaDeDocumento.Detalles[i].ValorDeDetalle);
                      totalDeNota += totalNotaItem;
                      $scope.notaDeDocumento.Detalles[i].ValorDeDetalleIgv = (totalNotaItem - (totalNotaItem / (1 + parseFloat($scope.tasaIGVParaNota))));
                  } else if ($scope.notaDeDocumento.TipoDeNota.Id == $scope.idDetalleMaestroCorreccionPorErrorEnLaDescripcion) {
                      if ($scope.notaDeDocumento.Detalles[i].ValorDeDetalle != '')
                          totalDeNota += parseFloat($scope.notaDeDocumento.Detalles[i].Importe);
                      $scope.notaDeDocumento.Detalles[i].ValorDeDetalleIgv = $scope.notaDeDocumento.Detalles[i].Igv;
                  } else {
                      $scope.notaDeDocumento.Detalles[i].ValorDeDetalle = $scope.notaDeDocumento.Detalles[i].ValorDeDetalle == '' ? 0 : $scope.notaDeDocumento.Detalles[i].ValorDeDetalle;
                      totalDeNota += parseFloat($scope.notaDeDocumento.Detalles[i].ValorDeDetalle);
                      $scope.notaDeDocumento.Detalles[i].ValorDeDetalleIgv = (parseFloat($scope.notaDeDocumento.Detalles[i].ValorDeDetalle) - (parseFloat($scope.notaDeDocumento.Detalles[i].ValorDeDetalle) / (1 + parseFloat($scope.tasaIGVParaNota))));
                  }
              }
          }
          $scope.totalDeNota = totalDeNota;
          $scope.igvDeNota = (totalDeNota - (totalDeNota / (1 + parseFloat($scope.tasaIGVParaNota))));
          $scope.subTotalDeNota = parseFloat($scope.totalDeNota) - parseFloat($scope.igvDeNota);
  
      }
  
      $scope.calcularTotalDeNotaMonto = function () {
          if ($scope.notaDeDocumento.TipoDeNota.Id == $scope.idDetalleMaestroAnulacionDeLaOperacion || $scope.notaDeDocumento.TipoDeNota.Id == $scope.idDetalleMaestroDevolucionTotal) {
              $scope.totalDeNota = $scope.verDocumento.Total;
              $scope.igvDeNota = $scope.verDocumento.Igv;
              $scope.subTotalDeNota = parseFloat($scope.totalDeNota) - parseFloat($scope.igvDeNota);
          } else if ($scope.notaDeDocumento.TipoDeNota.Id == $scope.idDetalleMaestroDescuentoGlobal || $scope.notaDeDocumento.TipoDeNota.Id == $scope.idDetalleMaestroInteresesPorMora) {
              $scope.notaDeDocumento.ValorDeNota = ($scope.notaDeDocumento.ValorDeNota == '') ? 0 : $scope.notaDeDocumento.ValorDeNota;
              $scope.totalDeNota = $scope.notaDeDocumento.ValorDeNota;
              $scope.igvDeNota = ($scope.totalDeNota - ($scope.totalDeNota / (1 + parseFloat($scope.tasaIGVParaNota))));
              $scope.subTotalDeNota = parseFloat($scope.totalDeNota) - parseFloat($scope.igvDeNota);
              if (parseFloat($scope.notaDeDocumento.ValorDeNota) > parseFloat($scope.verDocumento.Total)) {
                  $scope.inconsistenciasDeNota.push("Es necesario que el monto de nota sea menor al total.");
              }
          } else {
              $scope.totalDeNota = 0;
              $scope.igvDeNota = 0;
              $scope.subTotalDeNota = 0;
          }
      }
  
      $scope.cancelar = function () {
          $scope.hayRegistroDeNota = false;
      }
  
      $scope.verificarInconsistenciasDeNota = function () {
          var banderaDeDetalle = true;
          var cantidadMayor = false;
          var montoMayor = false;
          $scope.inconsistenciasDeNota = [];
          if ($scope.notaDeDocumento.TipoDeNota === undefined) {
              $scope.inconsistenciasDeNota.push("Es necesario seleccionar el tipo de nota.");
          } else {
              if ($scope.notaDeDocumento.TipoDeNota.Id === $scope.idDetalleMaestroAnulacionPorErrorEnElRuc) {
                  if ($scope.notaDeDocumento.ValorDeNota === "" || $scope.notaDeDocumento.ValorDeNota === null) {
                      $scope.inconsistenciasDeNota.push("Es necesario ingresar al valor de la nota.");
                  }
              } else if ($scope.notaDeDocumento.TipoDeNota.Id === $scope.idDetalleMaestroDescuentoGlobal) {
                  if ($scope.notaDeDocumento.ValorDeNota === "" || $scope.notaDeDocumento.ValorDeNota === null) {
                      $scope.inconsistenciasDeNota.push("Es necesario ingresar al valor de descuento de la nota.");
                  }
              } else if ($scope.notaDeDocumento.TipoDeNota.Id === $scope.idDetalleMaestroInteresesPorMora) {
                  if ($scope.notaDeDocumento.ValorDeNota === "" || $scope.notaDeDocumento.ValorDeNota === null) {
                      $scope.inconsistenciasDeNota.push("Es necesario ingresar al valor de interes de la nota.");
                  }
              } else if ($scope.notaDeDocumento.TipoDeNota.Id === $scope.idDetalleMaestroDescuentoPorItem || $scope.notaDeDocumento.TipoDeNota.Id === $scope.idDetalleMaestroDevolucionPorItem || $scope.notaDeDocumento.TipoDeNota.Id === $scope.idDetalleMaestroAumentoEnElValor) {
                  for (var i = 0; i < $scope.notaDeDocumento.Detalles.length; i++) {
                      banderaDeDetalle = banderaDeDetalle && ($scope.notaDeDocumento.Detalles[i].ValorDeDetalle == 0);
                      cantidadMayor = cantidadMayor || (parseFloat($scope.notaDeDocumento.Detalles[i].ValorDeDetalle) > parseFloat($scope.notaDeDocumento.Detalles[i].Cantidad));
                      montoMayor = montoMayor || (parseFloat($scope.notaDeDocumento.Detalles[i].ValorDeDetalle) > parseFloat($scope.notaDeDocumento.Detalles[i].Importe));
                  }
                  if (banderaDeDetalle)
                      $scope.inconsistenciasDeNota.push("Es necesario ingresar un valor de detalle de la nota.");
                  if ($scope.notaDeDocumento.TipoDeNota.Id === $scope.idDetalleMaestroDevolucionPorItem && cantidadMayor)
                      $scope.inconsistenciasDeNota.push("Es necesario que la cantidad a devolver sea menor a la cantidad del documento.");
                  if (montoMayor)
                      $scope.inconsistenciasDeNota.push("Es necesario que el monto sea menor a la importe.");
              } else if ($scope.notaDeDocumento.TipoDeNota.Id === $scope.idDetalleMaestroCorreccionPorErrorEnLaDescripcion) {
                  banderaDeDetalle = true;
                  for (var i = 0; i < $scope.notaDeDocumento.Detalles.length; i++) {
                      banderaDeDetalle = banderaDeDetalle && ($scope.notaDeDocumento.Detalles[i].ValorDeDetalle == '');
                  }
                  if (banderaDeDetalle)
                      $scope.inconsistenciasDeNota.push("Es necesario ingresar un valor de detalle de la nota.");
              }
              if ($scope.notaDeDocumento.TipoDeNota.Id == $scope.idDetalleMaestroDescuentoGlobal || $scope.notaDeDocumento.TipoDeNota.Id == $scope.idDetalleMaestroInteresesPorMora) {
                  if (parseFloat($scope.notaDeDocumento.ValorDeNota) > parseFloat($scope.verDocumento.Total)) {
                      $scope.inconsistenciasDeNota.push("Es necesario que el monto de nota sea menor al total.");
                  }
              }
          } if ($scope.notaDeDocumento.Motivo === "" || $scope.notaDeDocumento.Motivo === null || $scope.notaDeDocumento.Motivo === undefined) {
              $scope.inconsistenciasDeNota.push("Es necesario ingresar el motivo de la nota.");
          } if ($scope.notaDeDocumento.TipoDeComprobante === undefined) {
              $scope.inconsistenciasDeNota.push("Es necesario seleccionar un documento.");
          } else {
              if ($scope.notaDeDocumento.TipoDeComprobante.EsPropio === false) {
                  if ($scope.notaDeDocumento.TipoDeComprobante.SerieIngresada === "" || $scope.notaDeDocumento.TipoDeComprobante.SerieIngresada === null) {
                      $scope.inconsistenciasDeNota.push("Es necesario ingresar la serie de comprobante.");
                  } if ($scope.notaDeDocumento.TipoDeComprobante.NumeroIngresado === "" || $scope.notaDeDocumento.TipoDeComprobante.NumeroIngresado === null) {
                      $scope.inconsistenciasDeNota.push("Es necesario ingresar el numero de comprobante.");
                  }
              }
          }
      }
      //#endregion*/

    $scope.iniciarCanjeDeComprobante = function () {
        $scope.permitirCanjeDeComprobante = permitirCanjeDeComprobante;
        $scope.idTipoComprobanteNotaDeVenta = idTipoComprobanteNotaDeVenta;
        $scope.idTipoComprobanteFactura = idTipoComprobanteFactura;
        $scope.idTipoDocumentoIdentidadRuc = idTipoDocumentoIdentidadRuc;
        $scope.canjeDeComprobante = {};
        $scope.canjeDeComprobanteActivado = false;
        $scope.comprobantesSeleccionados = [];
        $scope.idsComprobantesSeleccionados = [];
        $scope.obtenerTiposDeComprobanteCanje();
    };

    $scope.accionCanjeDeComprobante = function () {
        if ($scope.canjeDeComprobanteActivado) {
            $scope.canjeDeComprobanteActivado = false;
        } else {
            $scope.canjeDeComprobanteActivado = true;
        }
    };

    $scope.verificarCanjeDeComprobante = function () {
        for (var i = 0; i < $scope.comprobantesSeleccionados.length; i++) {
            $scope.idsComprobantesSeleccionados[i] = $scope.comprobantesSeleccionados[i].Id;
            if ($scope.comprobantesSeleccionados[0].IdCliente != $scope.comprobantesSeleccionados[i].IdCliente) {
                var mensaje = "No se puede realizar el caje de comprobante, el cliente debe ser el mismo.";
                var hayInconsistencia = true;
                break;
            }
            if ($scope.idTipoComprobanteNotaDeVenta != $scope.comprobantesSeleccionados[i].IdTipoComprobante) {
                var mensaje = "No se puede realizar el caje de comprobante, los comprobantes deben de ser nota de venta.";
                var hayInconsistencia = true;
                break;
            }
        }
        if (hayInconsistencia) {
            SweetAlert.warning("Advertencia", mensaje);
        } else {
            $('#modal-canje-de-comprobante').modal('show');
        }
    };

    $scope.verificarInconsistenciasCanje = function () {
        $scope.inconsistenciasCanje = [];
        if ($scope.canjeDeComprobante.TipoDeComprobante == undefined) {
            $scope.inconsistenciasCanje.push("Es necesario seleccionar un documento.");
        } else {
            if ($scope.canjeDeComprobante.TipoDeComprobante.EsPropio == false) {
                if ($scope.canjeDeComprobante.TipoDeComprobante.SerieIngresada == '' || $scope.canjeDeComprobante.TipoDeComprobante.SerieIngresada == null) {
                    $scope.inconsistenciasCanje.push("Es necesario ingresar la serie de comprobante.");
                } if ($scope.canjeDeComprobante.TipoDeComprobante.NumeroIngresado == '' || $scope.canjeDeComprobante.TipoDeComprobante.NumeroIngresado == null) {
                    $scope.inconsistenciasCanje.push("Es necesario ingresar el numero de comprobante.");
                }
            }
            if ($scope.canjeDeComprobante.TipoDeComprobante.TipoComprobante.Id == $scope.idTipoComprobanteFactura && $scope.comprobantesSeleccionados[0].IdTipoDocumentoCliente != idTipoDocumentoIdentidadRuc) {
                $scope.inconsistenciasCanje.push("Es necesario que el cliente tenga ruc.");
            }
        }
    }


    $scope.registarCanjeDeComprobante = function () {
        ventaService.registrarCanjeDeComprobante({ idsOrdenes: $scope.idsComprobantesSeleccionados, TipoDeComprobante: $scope.canjeDeComprobante.TipoDeComprobante }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.comprobantesSeleccionados = [];
            $scope.idsComprobantesSeleccionados = [];
            $scope.canjeDeComprobante = {};
            $scope.listarBandeja();
            $('#modal-canje-de-comprobante').modal('hide');
        }).error(function (data) {
            SweetAlert.error("Ocurrio un problema", data.error);
        });
    };



    $scope.obtenerTiposDeComprobanteCanje = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        ventaService.obtenerTiposDeComprobanteParaVenta({}).success(function (data) {
            $scope.tiposDeComprobantes = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    };

    $scope.cargarSeriesCanje = function (tipoComprobante) {
        if (tipoComprobante !== null) {
            $scope.series = angular.copy(tipoComprobante.Series);
        }
    };

});




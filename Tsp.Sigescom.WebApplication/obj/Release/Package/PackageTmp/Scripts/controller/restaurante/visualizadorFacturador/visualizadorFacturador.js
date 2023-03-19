

angular.
    module('app').
    component('visualizadorFacturador', {
        templateUrl: "../Scripts/controller/restaurante/visualizadorFacturador/visualizadorFacturador.html",
        bindings: {
            api: '=',
            atencion: '=',
            cerrarVisualizador: '&'
        },

        controller: function ($q, $compile, $scope, $parse, $timeout, SweetAlert, ventaService, clienteService, restauranteService) {
            var ctrl = this;
            $('select:not(.normal)').each(function () {
                $(this).select2({
                    dropdownParent: $(this).parent()
                });
            });
            var scope = $scope.$root;

            ctrl.closeVisualizador = function () {
                if ($(".contenedorDocumentoVenta")) {
                    $(".contenedorDocumentoVenta").remove();
                }
                if ($(".contenedorDocumentoAtencion")) {
                    $(".contenedorDocumentoAtencion").remove();
                }
                $('#modal-visualizador-facturador').modal('hide');
                ctrl.cerrarVisualizador();
            };

            ctrl.inicializar = function () {
                ctrl.limpiar();
                ctrl.cargarColeccionesAsync();
                ctrl.cargarColeccionesSync().then(function (resultado_) {
                    ctrl.establecerDatosPorDefecto();
                }, function (error) {
                    ctrl.mensaje = "Se ha producido un error al obtener el dato:" + error;
                });
            };

            //#region Cargar parametros
            ctrl.limpiar = function () {
                ctrl.atencion = {};
                
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
                promiseList.push(ctrl.obtenerFormatosDeImpresion());
                return $q.all(promiseList).then(function (response) {
                    defered.resolve();
                }).catch(function (error) {
                    defered.reject(e);
                });
            };

            ctrl.establecerDatosPorDefecto = function () {
                ctrl.formato = [];
                ctrl.tamanioComprobante = [];
            };

            ctrl.obtenerFormatosDeImpresion = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                ventaService.obtenerFormatosDeImpresion({}).success(function (data) {
                    ctrl.formatosDeImpresion = data;
                    defered.resolve();
                }).error(function (data) {
                    SweetAlert.error2(data);
                    defered.reject(data);
                });
                return promise;
            }
            //#endregion

            //#region Envio de documento por correo
            ctrl.iniciarEnvioCorreo = function (index) {
                ctrl.indexDocumentoEnvio = index;
                ctrl.correosElectronicos = [];
                ctrl.correoElectronico = '';
            }

            ctrl.agregarCorreoElectronico = function () {
                ctrl.correosElectronicos.push(ctrl.correoElectronico);
                ctrl.correoElectronico = '';
                $timeout(function () { $('#correoImput').trigger("change"); }, 100);
            }

            ctrl.eliminarCorreoElectronico = function (index) {
                ctrl.correosElectronicos.splice(index);
            }

            ctrl.enviarCorreoElectronico = function () {
                ventaService.enviarCorreoElectronicoConDocumento({ idOrdenDeVenta: ctrl.atencion.documentosVenta[ctrl.indexDocumentoEnvio].IdOrden, formato: ctrl.formato[ctrl.indexDocumentoEnvio].Id, correosElectronicos: ctrl.correosElectronicos }).success(function (data) {
                    SweetAlert.success("Correcto", data.result_description);
                }).error(function (data) {
                    SweetAlert.error2(data);
                });
            }

            ctrl.limpiarEnvioDeCorrero = function () {
                ctrl.correosElectronicos = {};
            }
            //#endregion

            ctrl.imprimirDocumentoAtencion = function () {
                jsWebClientPrint.print('tipoDocumento=2&idDocumento=' + ctrl.atencion.Id);

            }

            ctrl.imprimirDocumentoVenta = function (index) {
                var printWin = window.open(' ', '');
                var html = ctrl.formato[index].Id == ctrl.parametros.Formato80 ? ctrl.atencion.documentosVenta[index].CadenaHtmlDeComprobante80 : ctrl.atencion.documentosVenta[index].CadenaHtmlDeComprobanteA4;
                printWin.document.write(html);
                printWin.document.close();
                printWin.focus();
                printWin.print();
                printWin.close();
            }

            ctrl.cargarDocumento = function (index) {
                ctrl.tamanioComprobante[index] = ctrl.formato[index].Id === ctrl.parametros.Formato80 ? '80mm' : "210mm";
                let cadenaHtml = ctrl.formato[index].Id === ctrl.parametros.Formato80 ? ctrl.atencion.documentosVenta[index].CadenaHtmlDeComprobante80 : ctrl.atencion.documentosVenta[index].CadenaHtmlDeComprobanteA4;
                document.getElementById("pdfDocumento-" + index).innerHTML = cadenaHtml;
                $timeout(function () { $("pdfDocumento-" + index).trigger("change"); }, 100);
            }

            ctrl.agregarPrevisualizacion = (index, htmlComprobante) => {
                let html = '';
                html += "<div class='col-xs-12 contenedorDocumentoVenta'><div class='row'><div class='col-xs-6 col-md-6 form-group px-5'><label>Formato :</label><select class='select2 form-control' id='comboFormato' ng-model='$ctrl.formato[" + index + "]' ng-change='$ctrl.cargarDocumento(" + index + ");' ng-options='item as item.Nombre for item in $ctrl.formatosDeImpresion track by item.Id'></select></div><div class='col-xs-6 col-md-6 px-5'><label>Acciones: </label><div class='col-xs-12 col-md-12 padding-0'><button title='IMPRIMIR EL DOCUMENTO' class='btn btn-md btn-default' ng-click='$ctrl.imprimirDocumentoVenta(" + index + ")'><span class='glyphicon glyphicon-print'></span></button><a title='DESCARGAR EN PDF' class='btn btn-md btn-default' href='/Venta/DescargarDocumentoPdf?idOrdenDeVenta={{$ctrl.atencion.documentosVenta[" + index + "].IdOrden}}&&formato={{$ctrl.formato[" + index + "].Id}}' target='_blank'><span class='fa fa-file-pdf-o'></span></a><button title='ENVIAR POR CORREO ELECTRONICO' class='btn btn-md btn-default' data-toggle='modal' data-target='#modal-envio-documento' ng-click='$ctrl.iniciarEnvioCorreo(" + index + ")'><span class='glyphicon glyphicon-envelope'></span></button></div></div></div ><div class='row'><form name='comprobante' class='form-horizontal'><div class='col-md-12 table-responsive' style='padding-left:0'><div id='pdfDocumento-" + index + "' style='width:{{$ctrl.tamanioComprobante[" + index + "]}}; border:solid; border-width: thin; margin:auto; margin-top:20px'>";
                html += htmlComprobante;
                html += "</div></div></form></div></div>";
                return html;
            }

            ctrl.SetAtencionAVisualizar = function (atencion) {
                ctrl.atencion = atencion;
                ctrl.cargarComprobantesDeAtencion();
            };

            ctrl.cargarComprobantesDeAtencion = () => {
                let htmlTotal = '';
                for (var i = 0; i < ctrl.atencion.documentosVenta.length; i++) {
                    ctrl.formato.push(ctrl.formatosDeImpresion[0]);
                    ctrl.tamanioComprobante.push('80mm');
                    htmlTotal += ctrl.agregarPrevisualizacion(i, ctrl.atencion.documentosVenta[i].CadenaHtmlDeComprobante80)
                }
                $("#documentoDeAtencion").append($compile("<div class='contenedorDocumentoAtencion'>" + ctrl.atencion.documentoAtencion + "</div>")($scope));
                $("#documentosDeVenta").append($compile(htmlTotal)($scope));
            }

            this.$onInit = function () {
                ctrl.api = {};
                ctrl.inicializar();
                ctrl.api.SetAtencionAVisualizar = ctrl.SetAtencionAVisualizar;
            };


        }
    });
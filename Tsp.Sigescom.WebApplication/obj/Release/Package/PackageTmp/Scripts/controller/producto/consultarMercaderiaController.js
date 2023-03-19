app.controller('consultarMercaderiaController', function ($scope, $rootScope, $timeout, $q, blockUI, DTOptionsBuilder, DTColumnDefBuilder, productoService, maestroService, conceptoService, precioService, SweetAlert, centroDeAtencionService) {

    $scope.mercaderias = { Lista: [] };
    $scope.mercaderia = {
        IdsCaracteristicas: [], ModulosAdicionales: [], Foto: {
            FotoSrc: "http://www.placehold.it/200x150/EFEFEF/AAAAAA&amp;text=no+image"
        },
        CantidadPresentacion: 1
    };
    $scope.mercaderiaInicial = {
        IdsCaracteristicas: [], ModulosAdicionales: [], Foto: {
            FotoSrc: "http://www.placehold.it/200x150/EFEFEF/AAAAAA&amp;text=no+image"
        },
        CantidadPresentacion: 1
    };
    $scope.caracteristicas = [];
    $scope.caracteristicasTodas = [];
    $scope.conceptos = [];
    $scope.conceptoBasicoSeleccionado = {};
    $scope.categoriaSeleccionada = {};
    $scope.presentaciones = [];
    $scope.tarifas = [];
    $scope.precioPorDefecto = [];
    $scope.unidadesDeMedida = [];
    $scope.tiposDeSubContenido = [];
    $scope.verSubContenido = false;
    //caracteristicas que se usarán como filtro para actualizar la bandeja de mercaderias
    $scope.valoresSeleccionadosCaracteristicasFiltro = [];
    $scope.tipoGuardar = 1;
    $scope.valorPrecioVentaPorDefectoQueNoSeDebeGuardar = valorPrecioVentaPorDefectoQueNoSeDebeGuardar;
    $scope.idPresentacionPorDefecto = idPresentacionPorDefecto;
    $scope.idUnidadMedidaPorDefecto = idUnidadMedidaPorDefecto;
    $scope.modulosAdicionales = modulosAdicionales;
    $scope.mercaderia.ModulosAdicionales = modulosAdicionales.map(ma => ma.Id);


    //GETS
    $scope.obtenerPresentaciones = function () {
        maestroService.obtenerPresentaciones({}).success(function (data) {
            $scope.presentaciones = data;
            let presentacion = Enumerable.from($scope.presentaciones).where("$.Id == '" + $scope.idPresentacionPorDefecto + "'").toArray()[0];
            $scope.mercaderia.Presentacion = presentacion != null ? presentacion : $scope.presentaciones[0];
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.obtenerTarifas = function () {
        maestroService.obtenerTarifas({}).success(function (data) {
            $scope.tarifas = data;
            //$scope.cargarPreciosPorDefecto($scope.mercaderia);
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.obtenerUnidadesDeMedida = function () {
        maestroService.obtenerUnidadesDeMedida({}).success(function (data) {
            $scope.unidadesDeMedida = data;
            let unidadMedida = Enumerable.from($scope.unidadesDeMedida).where("$.Id == '" + $scope.idUnidadMedidaPorDefecto + "'").toArray()[0];
            $scope.mercaderia.UnidadDeMedidaCom = unidadMedida != null ? unidadMedida : $scope.unidadesDeMedida[0];
            $scope.mercaderia.UnidadDeMedidaRef = unidadMedida != null ? unidadMedida : $scope.unidadesDeMedida[0];
            $scope.mercaderia.UnidadDeMedidaPres = unidadMedida != null ? unidadMedida : $scope.unidadesDeMedida[0];
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.obtenerConceptos = function () {
        maestroService.obtenerFamiliasVigentes({}).success(function (data) {
            $scope.conceptos = data;
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.obtenerCategorias = function () {
        maestroService.obtenerCategoriasConcepto({}).success(function (data) {
            $scope.categorias = data;
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.obtenerCaracteristicasTodas = function () {
        conceptoService.verCaracteristicas({}).success(function (data) {
            $scope.caracteristicasTodas = data;
            $timeout(function () {
                $('.caracteristica').select2({
                    language: "es"
                });
            }, 600);
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    //NO SE USA
    $scope.actualizarNombre = function () {
        $scope.mercaderia.Nombre = '';
        $scope.texto = '';
        if ($scope.mercaderia.IdsCaracteristicas.length > 0) {
            for (var i = 0; i < $scope.mercaderia.IdsCaracteristicas.length; i++) {
                for (var j = 0; j < $scope.caracteristicas.length; j++) {
                    for (var k = 0; k < $scope.caracteristicas[j].Valores.length; k++) {
                        if ($scope.mercaderia.IdsCaracteristicas[i] == $scope.caracteristicas[j].Valores[k].Id) {
                            $scope.texto += $scope.caracteristicas[j].Valores[k].Nombre + " ";
                        }
                    }
                }
            }
        }
        $scope.mercaderia.Nombre += $scope.mercaderia.Concepto != null ? $scope.mercaderia.Concepto.Nombre + ' ' : '';
        $scope.mercaderia.Nombre += $scope.mercaderia.Sufijo != null ? $scope.mercaderia.Sufijo : '';
        $scope.mercaderia.Nombre += $scope.texto == '' ? '' : ' ' + $scope.texto;
        $scope.mercaderia.Nombre += $scope.mercaderia.Presentacion != null ? (idPresentacionAOcultarEnNombreConceptoNegocio != $scope.mercaderia.Presentacion.Id) ? ' ' + $scope.mercaderia.Presentacion.Nombre + ' ' : '' : '';
        $scope.mercaderia.Nombre += $scope.mercaderia.CantidadPresentacion != null ? (idPresentacionAOcultarEnNombreConceptoNegocio != $scope.mercaderia.Presentacion.Id) ? $scope.mercaderia.CantidadPresentacion + ' ' : '' : '';
        $scope.mercaderia.Nombre += $scope.mercaderia.UnidadDeMedidaPres != null ? (idPresentacionAOcultarEnNombreConceptoNegocio != $scope.mercaderia.Presentacion.Id) ? $scope.mercaderia.UnidadDeMedidaPres.Nombre : '' : '';
        $scope.mercaderia.Nombre += (idUnidadDeMedidaComercialAOcultarEnNombreConceptoNegocio != $scope.mercaderia.UnidadDeMedidaCom.Id) ? " x " + ($scope.mercaderia.UnidadDeMedidaCom != null ? $scope.mercaderia.UnidadDeMedidaCom.Nombre : '') : '';

        const last = $scope.mercaderia.Nombre.charAt($scope.mercaderia.Nombre.length - 1);
        if (last === ' ') $scope.mercaderia.Nombre = $scope.mercaderia.Nombre.slice(0, -1);
    }

    $scope.obtenerCaracteristicasConcepto = function () {
        //$scope.mercaderia.IdsCaracteristicas= [] ;
        maestroService.obtenerCaracteristicasParaConcepto({ idConcepto: $scope.mercaderia.Concepto.Id }).success(function (data) {
            $scope.caracteristicas = data;
            if ($scope.mercaderia.edicion && $scope.mercaderia.IdsCaracteristicas.length > 0) {
                $scope.mercaderia.IdsCaracteristicas = $scope.setIdsCaracteristicas();
            }
            $scope.actualizarNombre();
            $timeout(function () {
                $('.caracteristica').select2({
                    language: "es"
                });
            }, 600);
            //console.log($scope.mercaderia.IdsCaracteristicas);
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.listarSubContenidos = function (IdModelo) {
        maestroService.listarSubContenidos({ IdModelo: IdModelo }).success(function (data) {
            $scope.tiposDeSubContenido = data;
            $scope.ultimoCodigo = $scope.tiposDeSubContenido.length + 1;
            $scope.cargarNombreYCodigoProducto();
            $scope.cargarSubContenido();
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.codigoProducto = function () {
        $scope.listarSubContenidos($scope.mercaderia.Modelo.Id);
    }

    /**************************************************RECURSOS******************************************************/
    //FOCUS
    $scope.focus = function () {
        $('.select2').select2({}).focus();
        $('#nombre-basico').select2({}).focus();
    }

    $scope.verCodigoBarra = function (codigo) {
        if (codigo.length == 13) {
            $("#inputdata").barcode(codigo, "ean13", { barWidth: 2, barHeight: 90 });
        } else {
            $("#inputdata").barcode(codigo, "code39", { barWidth: 2, barHeight: 90 });
        }
    }

    $scope.imprimirCodigoBarra = function (sectionId) {
        var htmlContent = document.getElementById(sectionId).innerHTML;
        var ventanaImpresion = window.open(' ', 'popimpr');
        ventanaImpresion.document.write(htmlContent);
        ventanaImpresion.document.close();
        ventanaImpresion.print();
        ventanaImpresion.close();
    }

    //METODOS PARA MANEJAR LAS FOTO
    $scope.eliminarFotoMercaderia = function () {
        $scope.mercaderia.Foto.Foto = null;
        $scope.mercaderia.Foto.HayFoto = false;
        $scope.mercaderia.Foto.FotoSrc = "http://www.placehold.it/200x150/EFEFEF/AAAAAA&amp;text=no+image";
        if ($scope.mercaderiaInicial.Foto.HayFoto) {
            $scope.botonRestablecer = true;
        }
    }

    $scope.restablecerFotoMercaderia = function () {
        $scope.mercaderia.Foto = angular.copy($scope.mercaderiaInicial.Foto);
        $scope.dasactivarBotonRestablecer();

    }

    $scope.processFile = function (file) {
        var BASE64_MARKER = ';base64,';
        var fileReader = new FileReader();
        fileReader.readAsDataURL(file.file);
        fileReader.onload = function () {
            var dataURI = fileReader.result;
            var base64Index = dataURI.indexOf(BASE64_MARKER) + BASE64_MARKER.length;
            $scope.mercaderia.Foto.HayFoto = true;
            $scope.mercaderia.Foto.Foto = dataURI.substring(base64Index);
        };
    }

    $scope.dasactivarBotonRestablecer = function () {
        $scope.botonRestablecer = false;
    }

    /************************** METODOS EN COMUN NUEVA MERCADERIA Y CONSULTAR MERCADERIA *****************************/

    $scope.inicializarRegistroMercaderia = function () {
        $scope.cargarPreciosPorDefecto();
        $scope.obtenerUnidadesDeMedida();
        $scope.obtenerPresentaciones();
        $scope.obtenerTarifas();
        $scope.obtenerConceptos();
        $scope.obtenerCategorias();
        $scope.obtenerCaracteristicasTodas();
        $scope.focus();
        $scope.dasactivarBotonRestablecer();
        $scope.numeroDecimalesEnPrecio = numeroDecimalesEnPrecio;
        $scope.modoDeSeleccionTipoFamiliaEnRegistroFamilia = modoDeSeleccionTipoFamiliaEnRegistroFamilia;
        $scope.mostrarCampoCodigoAlRegistrarConcepto = mostrarCampoCodigoAlRegistrarConcepto;
        $scope.mostrarCampoCodigoDigemidAlRegistrarConcepto = mostrarCampoCodigoDigemidAlRegistrarConcepto;
        $scope.classParaCodigo = ($scope.mostrarCampoCodigoAlRegistrarConcepto && $scope.mostrarCampoCodigoDigemidAlRegistrarConcepto) ? 'col-md-6': 'col-md-4';
        $scope.classParaCodigoDigemid = ($scope.mostrarCampoCodigoAlRegistrarConcepto && $scope.mostrarCampoCodigoDigemidAlRegistrarConcepto) ? 'col-md-6' : 'col-md-4';
        $scope.classParaCodigoBarra = ($scope.mostrarCampoCodigoAlRegistrarConcepto && $scope.mostrarCampoCodigoDigemidAlRegistrarConcepto) ? 'col-md-6' : ($scope.mostrarCampoCodigoAlRegistrarConcepto || $scope.mostrarCampoCodigoDigemidAlRegistrarConcepto) ? 'col-md-4' : 'col-md-6';
        $scope.classParaFamilia = ($scope.mostrarCampoCodigoAlRegistrarConcepto && $scope.mostrarCampoCodigoDigemidAlRegistrarConcepto) ? 'col-md-6' : ($scope.mostrarCampoCodigoAlRegistrarConcepto || $scope.mostrarCampoCodigoDigemidAlRegistrarConcepto) ? 'col-md-4' : 'col-md-6';
    }


    $scope.nuevoRegistroMercaderia = function () {
        if ($scope.tipoGuardar === 1) {
            $scope.mercaderia = {
                IdsCaracteristicas: [], ModulosAdicionales: [], Foto: {
                    FotoSrc: "http://www.placehold.it/200x150/EFEFEF/AAAAAA&amp;text=no+image"
                }
            };
            $scope.mercaderia.CantidadPresentacion = 1;
            $scope.mercaderia.edicion = false;
            let presentacion = Enumerable.from($scope.presentaciones).where("$.Id == '" + $scope.idPresentacionPorDefecto + "'").toArray()[0];
            $scope.mercaderia.Presentacion = presentacion != null ? presentacion : $scope.presentaciones[0];
            let unidadMedida = Enumerable.from($scope.unidadesDeMedida).where("$.Id == '" + $scope.idUnidadMedidaPorDefecto + "'").toArray()[0];
            $scope.mercaderia.UnidadDeMedidaCom = unidadMedida != null ? unidadMedida : $scope.unidadesDeMedida[0];
            $scope.mercaderia.UnidadDeMedidaRef = unidadMedida != null ? unidadMedida : $scope.unidadesDeMedida[0];
            $scope.mercaderia.UnidadDeMedidaPres = unidadMedida != null ? unidadMedida : $scope.unidadesDeMedida[0];
            //$scope.cargarPreciosPorDefecto($scope.mercaderia);
            $scope.mercaderiaInicial = angular.copy($scope.mercaderia);
            $scope.mercaderia.Concepto = {};
            $scope.precio = angular.copy($scope.precioPorDefecto);
            $scope.dasactivarBotonRestablecer();
            $scope.mercaderia.ModulosAdicionales = modulosAdicionales.map(ma => ma.Id);
            //$scope.cargarPreciosPorDefecto($scope.mercaderia);
            $("#select-model").each(function () {
                $(this).select2('val', '')
            });
            $timeout(function () { $('.selectAtributoMercaderia').trigger("change"); }, 100);
        }
    }


    /************************************** MODULO NUEVA MERCADERIA ***************************************************/

    $scope.setIdsCaracteristicas = function () {
        var lengthArray = $scope.mercaderia.IdsCaracteristicas.length;
        var arrayIds = [];
        for (var i = 0; i < $scope.mercaderia.IdsCaracteristicas.length; i++) {
            for (var j = 0; j < $scope.caracteristicas.length; j++) {
                for (var k = 0; k < $scope.caracteristicas[j].Valores.length; k++) {
                    if ($scope.mercaderia.IdsCaracteristicas[i] == $scope.caracteristicas[j].Valores[k].Id) {
                        arrayIds[j] = $scope.mercaderia.IdsCaracteristicas[i];
                    }
                }
            }
        }
        return arrayIds;
    }

    $scope.obtenerCaracteristicasConceptoPromise = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        maestroService.obtenerCaracteristicasParaConcepto({ idConcepto: $scope.mercaderia.Concepto.Id }).success(function (data) {
            $scope.caracteristicas = data;
            if ($scope.mercaderia.edicion && $scope.mercaderia.IdsCaracteristicas.length > 0) {
                $scope.mercaderia.IdsCaracteristicas = $scope.setIdsCaracteristicas();
            }
            // $scope.actualizarNombreSync();
            console.log("caracteristicas", $scope.caracteristicas);
            $timeout(function () {
                $('.caracteristica').select2({
                    language: "es"
                });
            }, 100);
            defered.resolve();

        }).error(function (data) {
            SweetAlert.error2(data);
            defered.reject(data);
        });
        return promise;
    }

    $scope.cargarCaracteristicasConceptoSync = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        try {
            $scope.obtenerCaracteristicasConceptoPromise().then(function (resultado_) {
                defered.resolve();
            }, function (error) {
                $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
            });
        } catch (e) {
            defered.reject(e);
        }
        return promise;
    }

    $scope.actualizarNombrePromise = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        try {
            $scope.mercaderia.Nombre = '';
            $scope.texto = '';
            if ($scope.mercaderia.IdsCaracteristicas.length > 0) {
                for (var i = 0; i < $scope.mercaderia.IdsCaracteristicas.length; i++) {
                    for (var j = 0; j < $scope.caracteristicas.length; j++) {
                        for (var k = 0; k < $scope.caracteristicas[j].Valores.length; k++) {
                            if ($scope.mercaderia.IdsCaracteristicas[i] == $scope.caracteristicas[j].Valores[k].Id) {
                                $scope.texto += $scope.caracteristicas[j].Valores[k].Nombre + " ";
                            }
                        }
                    }
                }
            }
            $scope.mercaderia.Nombre += $scope.mercaderia.Concepto != null ? $scope.mercaderia.Concepto.Nombre + ' ' : '';
            $scope.mercaderia.Nombre += $scope.mercaderia.Sufijo != null ? $scope.mercaderia.Sufijo : ' ';
            $scope.mercaderia.Nombre += $scope.texto == '' ? '' : ' ' + $scope.texto;
            $scope.mercaderia.Nombre += $scope.mercaderia.Presentacion != null ? (idPresentacionAOcultarEnNombreConceptoNegocio != $scope.mercaderia.Presentacion.Id) ? ' ' + $scope.mercaderia.Presentacion.Nombre + ' ' : '' : '';
            $scope.mercaderia.Nombre += $scope.mercaderia.CantidadPresentacion != null ? (idPresentacionAOcultarEnNombreConceptoNegocio != $scope.mercaderia.Presentacion.Id) ? $scope.mercaderia.CantidadPresentacion + ' ' : '' : '';
            $scope.mercaderia.Nombre += $scope.mercaderia.UnidadDeMedidaPres != null ? (idPresentacionAOcultarEnNombreConceptoNegocio != $scope.mercaderia.Presentacion.Id) ? $scope.mercaderia.UnidadDeMedidaPres.Nombre : '' : '';
            $scope.mercaderia.Nombre += (idUnidadDeMedidaComercialAOcultarEnNombreConceptoNegocio != $scope.mercaderia.UnidadDeMedidaCom.Id) ? " x " + ($scope.mercaderia.UnidadDeMedidaCom != null ? $scope.mercaderia.UnidadDeMedidaCom.Nombre : '') : '';
            const last = $scope.mercaderia.Nombre.charAt($scope.mercaderia.Nombre.length - 1);
            if (last === ' ') $scope.mercaderia.Nombre = $scope.mercaderia.Nombre.slice(0, -1);

            defered.resolve();
        } catch (e) {
            defered.reject(e);
        }
        return promise;


    }

    $scope.actualizarNombreSync = function () {

        var defered = $q.defer();
        var promise = defered.promise;
        try {
            $scope.actualizarNombrePromise().then(function (resultado_) {
                defered.resolve();
            }, function (error) {
                $scope.mensaje = "Se ha producido un error al actualizar el nombre:" + error;
            });
        } catch (e) {
            defered.reject(e);
        }
        return promise;

    }

    $scope.cargarSubContenido = function () {
        $scope.verSubContenido = false;
        if ($scope.mercaderia.UnidadDeMedidaPres.Id == idUnidadDeMedidaSubUnidad) {
            $scope.verSubContenido = true;
            //$scope.listarSubContenidos($scope.modelo.Modelo.Id);
        }
        $scope.cargarNombreProducto();
    }

    $scope.cargarPreciosPorDefecto = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        try {
            precioService.obtenerPreciosDeConceptoNegocio({ idConceptoNegocio: 0 }).success(function (data) {
                $scope.precio = data;
                for (var i = 0; i < $scope.precio.PuntosDePrecio.length; i++) {
                    for (var j = 0; j < $scope.precio.PuntosDePrecio[i].Tarifas.length; j++) {
                        date = $scope.precio.PuntosDePrecio[i].Tarifas[j].FechaDesde.slice($scope.precio.PuntosDePrecio[i].Tarifas[j].FechaDesde.indexOf("(") + 1, $scope.precio.PuntosDePrecio[i].Tarifas[j].FechaDesde.indexOf(")"));
                        $scope.precio.PuntosDePrecio[i].Tarifas[j].FechaDesde = $scope.formatDate(new Date(+date), "ES");
                        $("#fechaDesde" + i + j).datepicker('setDate', $scope.precio.PuntosDePrecio[i].Tarifas[j].FechaDesde);
                        date = $scope.precio.PuntosDePrecio[i].Tarifas[j].FechaHasta.slice($scope.precio.PuntosDePrecio[i].Tarifas[j].FechaHasta.indexOf("(") + 1, $scope.precio.PuntosDePrecio[i].Tarifas[j].FechaHasta.indexOf(")"));
                        $scope.precio.PuntosDePrecio[i].Tarifas[j].FechaHasta = $scope.formatDate(new Date(+date), "ES");
                        $("#fechaHasta" + i + j).datepicker('setDate', $scope.precio.PuntosDePrecio[i].Tarifas[j].FechaHasta);
                    }
                }
                $(function () {
                    $('.td-datepicker').datepicker({
                        format: 'dd/mm/yyyy',
                        autoclose: true,
                        todayHighlight: true,
                        language: 'es'
                    });
                });
                $scope.precioPorDefecto = angular.copy($scope.precio);
            }).error(function (data) {
                SweetAlert.error2(data);
            });
        } catch (e) {
            defered.reject(e);
        }
        return promise;
    }


    /************************************* *CONSULTAR MERCADERIA***************************************************/

    $scope.nuevoRegistroMercaderiaEnConsultarMercaderia = function () {
        $scope.tipoGuardar = 1;
        $scope.nuevoRegistroMercaderia();
    }

    $scope.obtenerConceptosNegociosComerciales = function () {
        //if ($scope.conceptoBasicoSeleccionado.Id > 0 || $scope.valoresSeleccionadosCaracteristicasFiltro.length > 0) {
        productoService.obtenerConceptosNegociosComerciales({
            idConceptoBasico: $scope.conceptoBasicoSeleccionado.Id,
            idCategoria: $scope.categoriaSeleccionada.Id,
            idValoresCaracteristicas: $scope.valoresSeleccionadosCaracteristicasFiltro
        }).success(function (data) {
            $scope.mercaderias.Lista = data;
        }).error(function (data) {
            SweetAlert.error2(data);
        });
        //}
    }

    $scope.cargarMercaderia = function (item) {
        $scope.mercaderia = angular.copy(item);
    }

    $scope.editarProducto = function (idMercaderia) {
        $("#select-model").each(function () {
            $(this).select2('val', '')
        });
        $scope.mercaderiaInicial = { Foto: {} };
        productoService.cargarProducto({ idMercaderia: idMercaderia }).success(function (data) {
            $scope.caracteristicas = [];
            $scope.mercaderia = data;
            $scope.precio = data.PreciosVenta;
            $scope.mercaderia.edicion = true;
            $scope.mercaderiaInicial = angular.copy($scope.mercaderia);
            if ($scope.mercaderia.Foto.HayFoto) {
                $scope.mercaderiaInicial.Foto = angular.copy($scope.mercaderia.Foto);
            }
            $scope.cargarCaracteristicasConceptoSync();
            $timeout(function () {
                $('.selectAtributoMercaderia').trigger("change");
            }, 100);

            for (var i = 0; i < $scope.precio.PuntosDePrecio.length; i++) {
                for (var j = 0; j < $scope.precio.PuntosDePrecio[i].Tarifas.length; j++) {
                    date = $scope.precio.PuntosDePrecio[i].Tarifas[j].FechaDesde.slice($scope.precio.PuntosDePrecio[i].Tarifas[j].FechaDesde.indexOf("(") + 1, $scope.precio.PuntosDePrecio[i].Tarifas[j].FechaDesde.indexOf(")"));
                    $scope.precio.PuntosDePrecio[i].Tarifas[j].FechaDesde = $scope.formatDate(new Date(+date), "ES");
                    $("#fechaDesde" + i + j).datepicker('setDate', $scope.precio.PuntosDePrecio[i].Tarifas[j].FechaDesde);
                    date = $scope.precio.PuntosDePrecio[i].Tarifas[j].FechaHasta.slice($scope.precio.PuntosDePrecio[i].Tarifas[j].FechaHasta.indexOf("(") + 1, $scope.precio.PuntosDePrecio[i].Tarifas[j].FechaHasta.indexOf(")"));
                    $scope.precio.PuntosDePrecio[i].Tarifas[j].FechaHasta = $scope.formatDate(new Date(+date), "ES");
                    $("#fechaHasta" + i + j).datepicker('setDate', $scope.precio.PuntosDePrecio[i].Tarifas[j].FechaHasta);
                }
            }
            $(function () {
                $('.td-datepicker').datepicker({
                    format: 'dd/mm/yyyy',
                    autoclose: true,
                    todayHighlight: true,
                    language: 'es'
                });
            });
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.eliminarMercaderia = function (id) {
        productoService.eliminarMercaderia({ IdMercaderia: id }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            if ($scope.conceptosNegociosComercialesConStockYPrecio) {
                $scope.obtenerConceptosNegociosComercialesIncluyendoStockYPrecios();
            } else {
                $scope.obtenerConceptosNegociosComerciales();
            }
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.guardar = function () {
        var defered = $q.defer();
        $scope.mercaderia.PreciosVenta = $scope.precio;
        $scope.actualizarNombreSync().then(function (resultado_) {
            if ($scope.mercaderia.Sufijo) {
                productoService.guardarProducto({ producto: $scope.mercaderia }).success(function (data) {
                    SweetAlert.success("Correcto", data.result_description);
                    if ($scope.guardadoParaFuera) {
                        switch ($scope.tipoRegistroFuera) {
                            case 1:
                                var modeloScopeMercaderia = angular.element('#modelo').scope();
                                modeloScopeMercaderia.actualizarConceptosIngresados($scope.mercaderia.Concepto);
                                modeloScopeMercaderia.actualizarProductosIngresados(data.data, $scope.mercaderia.Nombre, $scope.mercaderia.Concepto.EsBien);
                                break;
                            default:
                                break;
                        }
                        $timeout(function () { angular.element('#remover').triggerHandler('click'); }, 300);
                        $scope.tipoGuardar = 1;
                        $('#modal-mercaderia').modal('hide');
                        return;
                    }
                    $timeout(function () { angular.element('#remover').triggerHandler('click'); }, 300);
                    $scope.tipoGuardar = 1;
                    $('#modal-mercaderia').modal('hide');
                    if ($scope.conceptosNegociosComercialesConStockYPrecio) {
                        $scope.obtenerConceptosNegociosComercialesIncluyendoStockYPrecios();
                    } else {
                        $scope.obtenerConceptosNegociosComerciales();
                    }
                    $scope.nuevoRegistroMercaderia();
                }).error(function (data) {
                    if (data.warning) SweetAlert.warning("Advertencia", data.error);
                    else SweetAlert.error2(data);
                });
            } else {
                SweetAlert.error("Ocurrio un Problema", "Ingrese el Sufijo");
            }
            defered.resolve();
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener al actualizar el nombre:" + error;
            defered.reject(error);
        });

    }

    $scope.guardarYClonar = function () {
        var defered = $q.defer();
        $scope.mercaderia.PreciosVenta = $scope.precio;
        $scope.actualizarNombreSync().then(function (resultado_) {
            if ($scope.mercaderia.Sufijo) {
                productoService.guardarProducto({ producto: $scope.mercaderia }).success(function (data) {
                    SweetAlert.success("Correcto", data.result_description);
                    if ($scope.guardadoParaFuera) {
                        switch ($scope.tipoRegistroFuera) {
                            case 1:
                                var modeloScopeMercaderia = angular.element('#modelo').scope();
                                modeloScopeMercaderia.actualizarConceptosIngresados($scope.mercaderia.Concepto).then(function (resultado_) {
                                    modeloScopeMercaderia.actualizarProductosIngresados(data.data, $scope.mercaderia.Nombre, $scope.mercaderia.Concepto.EsBien).then(function (resultado_) {
                                        modeloScopeMercaderia.agregarDetalle();
                                    }, function (error) {
                                        $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
                                    });
                                }, function (error) {
                                    $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
                                });
                                break;
                            default:
                                break;
                        }
                        $timeout(function () {
                            angular.element('#remover').triggerHandler('click');
                        }, 300);
                        $scope.tipoGuardar = 2;
                        return;
                    }
                    $scope.tipoGuardar = 2;
                    if ($scope.conceptosNegociosComercialesConStockYPrecio) {
                        $scope.obtenerConceptosNegociosComercialesIncluyendoStockYPrecios();
                    } else {
                        $scope.obtenerConceptosNegociosComerciales();
                    }
                }).error(function (data) {
                    if (data.warning) SweetAlert.warning("Advertencia", data.error);
                    else SweetAlert.error2(data);
                });
            } else {
                SweetAlert.error("Ocurrio un Problema", "Ingrese el Sufijo");
            }

            defered.resolve();
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener al acutalizar:" + error;
            defered.reject(error);
        });

    }

    $scope.limpiarFiltros = function () {
        $scope.conceptoBasicoSeleccionado = {};
        $scope.categoriaSeleccionada = {};
        $scope.valoresSeleccionadosCaracteristicasFiltro = [];
    }

    // METODO UTILIZADO POR ARCHIVOS JS EXTERNOS
    $scope.nuevoRegistroMercaderiaEnCompras = function () {
        $scope.tipoGuardar = 1;
        $scope.guardadoParaFuera = true;
        $scope.tipoRegistroFuera = 1;
        $scope.nuevoRegistroMercaderia();
    }

    $scope.actualizarConceptosIngresados = function (id, nombre, valor) {
        $scope.nuevoConcepto = { Id: id, Nombre: nombre, Valor: valor };
        $scope.nuevoConcepto.EsBien = valor === "1";
        $scope.conceptos.unshift($scope.nuevoConcepto);
        $scope.mercaderia.Concepto = $scope.nuevoConcepto;
        $scope.cargarCaracteristicasConceptoSync();
    }

    $scope.actualizarValoesCaracteristicasIngresados = function (index, id, nombre) {
        $scope.item = { Id: id, Nombre: nombre };
        console.log('Actualizar caracteristicas', $scope.caracteristicas);
        $scope.caracteristicas[index].Valores.push($scope.item);
        $scope.mercaderia.IdsCaracteristicas[index] = $scope.item.Id;
        $scope.actualizarNombrePromise();
    }


    /************************************* SELECCIONAR CENTRO DE ATENCION ***************************************************/

    $scope.inicializarManejoCentrosAtencion = function () {
        //OBTENCION DE PARAMETROS
        $scope.idEstablecimientoPorDefecto = idEstablecimientoPorDefecto;
        $scope.idCentroDeAtencionPorDefecto = idCentroDeAtencionPorDefecto;
        //INICIO DE VARIABLES
        $scope.establecimientoComercial = {};
        $scope.centroDeAtencion = {};
        $scope.conceptosNegociosComercialesConStockYPrecio = true;
        $scope.obtenerEstablecimientosComerciales().then(function (resultado_) {
            $scope.establecerEstablecimientoComercialPorDefecto();
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
    }

    $scope.obtenerEstablecimientosComerciales = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        centroDeAtencionService.obtenerEstablecimientosComerciales().success(function (data) {
            $scope.listaEstablecimientosComerciales = data;
            defered.resolve();
        }).error(function (data) {
            SweetAlert.error2(data);
            defered.reject(data);
        });
        return promise;
    }

    $scope.establecerEstablecimientoComercialPorDefecto = function () {
        var establecimientoComercial = Enumerable.from($scope.listaEstablecimientosComerciales)
            .where("$.Id == '" + $scope.idEstablecimientoPorDefecto + "'").toArray()[0];
        $scope.establecimientoComercial = establecimientoComercial ? establecimientoComercial : $scope.listaEstablecimientosComerciales[0];
        $timeout(function () { $('#establecimientoComercial').trigger("change"); }, 100);
        $scope.obtenerCentrosDeAtencionConRolAlmacen().then(function (resultado_) {
            $scope.establecerCentroAtencionConRolAlmacenPorDefecto();
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
    }


    $scope.obtenerCentrosDeAtencionConRolAlmacen = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        $scope.idsEstablecimientosComerciales = [];
        if ($scope.establecimientoComercial) {
            $scope.idsEstablecimientosComerciales.push($scope.establecimientoComercial.Id);
        }
        centroDeAtencionService.obtenerCentrosDeAtencionConRolAlmacenVigentesPorEstablecimientosComerciales({ idsEstablecimientosComerciales: $scope.idsEstablecimientosComerciales }).success(function (data) {
            $scope.listaCentrosDeAtencionConRolAlmacen = data;
            defered.resolve();
        }).error(function (data) {
            SweetAlert.error2(data);
            defered.reject(data);
        });
        return promise;
    }

    $scope.establecerCentroAtencionConRolAlmacenPorDefecto = function () {
        var centroAtencion = Enumerable.from($scope.listaCentrosDeAtencionConRolAlmacen)
            .where("$.Id == '" + $scope.idCentroDeAtencionPorDefecto + "'").toArray()[0];
        $scope.centroDeAtencion = centroAtencion ? centroAtencion : $scope.listaCentrosDeAtencionConRolAlmacen[0];
        $timeout(function () { $('#centroDeAtencion').trigger("change"); }, 100);
    }

    $scope.obtenerConceptosNegociosComercialesIncluyendoStockYPrecios = function () {
        if ($scope.centroDeAtencion.Id > 0) {/* || $scope.conceptoBasicoSeleccionado.Id > 0 || $scope.valoresSeleccionadosCaracteristicasFiltro.length > 0*/
            productoService.obtenerConceptosNegociosComercialesIncluyendoStockYPrecios({
                idAlmacen: $scope.centroDeAtencion.Id,
                idConceptoBasico: $scope.conceptoBasicoSeleccionado.Id,
                idCategoria: $scope.categoriaSeleccionada.Id,
                idValoresCaracteristicas: $scope.valoresSeleccionadosCaracteristicasFiltro
            }).success(function (data) {
                $scope.mercaderias.Lista = data;
            }).error(function (data) {
                SweetAlert.error2(data);
            });
        }
    }

    /* Metodo para obtener la cantidad de compras y ventas ocurridas segun concepto de negocio */
    //$scope.obtenerCantidadDeVentasYComprasOcurridasPorConceptoNegocio = function () {
    //    Al momento de dar click guardar una copia del json original y comparar con el modificado antes de guardar
    //    Si son diferentes irme al servicio y mostrar el mensaje de alerta 
    //    Advertencia Si ustede actualiza este concepto de negocio se veran afectas N COMPRAS M VENTAS, Esta seguro de querer continuar ?
    //}
    //$scope.cargarPreciosPorDefecto = function (idConcepto) {
    //    $scope.mercaderia.PreciosVenta = [];
    //    for (var i = 0; i < $scope.tarifas.length; i++) {
    //        $scope.mercaderia.PreciosVenta[($scope.tarifas.length - 1) - i] = { Id: 0, Tarifa: $scope.tarifas[i].Nombre, IdTarifa: $scope.tarifas[i].Id, Valor: $scope.valorPrecioVentaPorDefectoQueNoSeDebeGuardar };
    //    }
    //}

    //$scope.cargarPreciosPorDefectoEditar = function (mercaderia) {

    //    var cantidadDePrecios = mercaderia.PreciosVenta.length;

    //    for (var i = 0; i < $scope.tarifas.length; i++) {
    //        var temp = 0;
    //        for (var j = 0; j < cantidadDePrecios; j++) {
    //            if ($scope.tarifas[i].Id == mercaderia.PreciosVenta[j].IdTarifa) {
    //                temp++;
    //                mercaderia.PreciosVenta[j] = { Id: mercaderia.PreciosVenta[j].Id, Tarifa: $scope.tarifas[i].Nombre, IdTarifa: mercaderia.PreciosVenta[j].IdTarifa, Valor: mercaderia.PreciosVenta[j].Valor };
    //                break;
    //            }
    //        }
    //        if (temp == 0) {
    //            mercaderia.PreciosVenta[cantidadDePrecios] = { Id: 0, Tarifa: $scope.tarifas[i].Nombre, IdTarifa: $scope.tarifas[i].Id, Valor: $scope.valorPrecioVentaPorDefectoQueNoSeDebeGuardar };
    //            cantidadDePrecios++;
    //        }
    //    }
    //}
});

app.controller('conceptoController', function ($scope, $rootScope, $timeout, conceptoService, maestroService, centroDeAtencionService, SweetAlert) {

    //CONCEPTO Y CARACTERISTICAS
    $scope.concepto = { Valor: "1", Caracteristicas: [], Categorias: [] };
    $scope.conceptos = { Lista: [] };
    $scope.categorias = [];
    $scope.caracteristica = { EsComun: true, open: false };
    $scope.caracteristicas = [];
    $scope.caracteristicasTodas = [];
    $scope.caracteristicasSeleccionadas = [];
    $scope.valoresCaracteristicas = [];
    $scope.modoDeSeleccionTipoFamiliaEnRegistroFamilia = modoDeSeleccionTipoFamiliaEnRegistroFamilia;
    $scope.permitirRegistroDeBien = $scope.modoDeSeleccionTipoFamiliaEnRegistroFamilia == 1 || $scope.modoDeSeleccionTipoFamiliaEnRegistroFamilia == 2;
    $scope.permitirRegistroDeServicio = $scope.modoDeSeleccionTipoFamiliaEnRegistroFamilia == 0 || $scope.modoDeSeleccionTipoFamiliaEnRegistroFamilia == 2;
    $scope.concepto.Valor = $scope.permitirRegistroDeBien ? "1" : "0";

    //REPORTE
    $scope.URLReporteStockActual = "";
    $scope.entidadesInternasSeleccionadasGeneral = [];
    $scope.fechaActual = new Date();
    $scope.listaDeEntidadesInternas = [];
    $scope.entidadInternaSeleccionada = {};

    //GETS
    $scope.obtenerAlmacenes = function (){
        centroDeAtencionService.obtenerCentrosDeAtencionConRolAlmacen().success(function (data) {
            $scope.listaDeEntidadesInternas=data;
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

    $scope.obtenerCaracteristicas = function () {
        maestroService.obtenerCaracteristicasConcepto({}).success(function (data) {
            $scope.caracteristicas = data;
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.obtenerCaracteristicasTodas = function () {
        conceptoService.verCaracteristicas({}).success(function (data) {
            $scope.caracteristicasTodas = data;
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.obtenerValoresCaracteristicas = function () {
        conceptoService.verValoresCaracteristica().success(function (data) {
            $scope.valoresCaracteristicas = data;
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    //CONCEPTOS
    $scope.inicializarRegistroConcepto = function () {
        $scope.obtenerCategorias();
        $scope.obtenerCaracteristicas();
    }

    $scope.inicializarBandejaConcepto = function () {
        $scope.verConceptos();
        $scope.obtenerCategorias();
        $scope.obtenerCaracteristicas();
    }

    $scope.nuevoRegistroConcepto = function () {
        $scope.concepto = { Valor: "1", Caracteristicas: [], Categorias: [] };
        $timeout(function () { $('#categoria').trigger("change"); }, 100);
        $scope.caracteristicasSeleccionadas = [];
    }

    //$scope.guardarConcepto = function () {
    //    let resultado = $scope.validarDatosDeRegistroDeConcepto();
    //    if (resultado) {
    //        conceptoService.guardarConcepto({ concepto: $scope.concepto }).success(function (data) {
    //        SweetAlert.success("Correcto", data.result_description);
    //        $scope.nue
    //        }).error(function (data) {
    //            SweetAlert.error2(data);
    //        });
    //    }
    //}

    $scope.guardarConcepto = function () {
        let resultado = $scope.validarDatosDeRegistroDeConcepto();
        if (resultado) {

            conceptoService.guardarConcepto({ concepto: $scope.concepto }).success(function (data) {
                SweetAlert.success("Correcto", data.result_description); 

                    if ($scope.guardadoParaFuera) {
                        switch ($scope.tipoRegistroFuera) {
                            case 1:
                                let modeloScope = angular.element('#modelo-mercaderia').scope();
                                //Enviar los datos del concepto al controlador de mercaderia
                                modeloScope.actualizarConceptosIngresados(data.data, $scope.concepto.Nombre, $scope.concepto.Valor);

                                break;
                            default:
                                break;
                        }
                        $('#modal-concepto').modal('hide');
                        return;
                    }
                    $('#modal-concepto').modal('hide');
                    $scope.nuevoRegistroConcepto();
                    $scope.verConceptos();
          
            }).error(function (data) {
                SweetAlert.error("Ocurrio un problema",data.error);
            });

        }
    }


    $scope.editarConcepto = function (idConcepto) {
        $scope.caracteristicasSeleccionadas = [];
        conceptoService.obtenerConcepto({ idConcepto: idConcepto }).success(function (data) {
            $scope.concepto = angular.copy(data);
            for (var i = 0; i < $scope.concepto.Caracteristicas.length; i++) {
                $scope.caracteristicasSeleccionadas.push($scope.concepto.Caracteristicas[i].Id);
            }
            $timeout(function () {
                $('#categoria').trigger("change");
            }, 100);
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.cargarConcepto = function (item) {
        $scope.conceptoAEliminar = angular.copy(item);
    }

    $scope.cambiarEstadoFamilia = function (concepto) {
        conceptoService.cambioEsVigenteFamilia({ idConcepto: concepto.Id, esVigente: concepto.EsVigente}).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.verConceptos();
        }).error(function (data) {
            SweetAlert.error("Error", data.error);
        });
    }
   
    $scope.verConceptos = function () {
        conceptoService.obtenerFamilias().success(function (data) {
            $scope.conceptos.Lista = data;
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.validarDatosDeRegistroDeConcepto = function () {
        if (!$scope.concepto.Nombre) {
            SweetAlert.warning("Advertencia", "Ingrese nombre del concepto");
            return false;
        }
        if (!$scope.concepto.Valor) {
            SweetAlert.warning("Advertencia", "Ingrese si es un bien o servicio");
            return false;
        }
        return true;
    }

    $scope.close = function () {
        if (angular.equals($scope.empleado, $scope.empleadoInicial)) {
            $('#modal-registro').modal('hide');
        }
        else {
            $('#modal-pregunta').click();
        }
    }

    $scope.closeModal = function () {
        $('#modal-registro').modal('hide');

    }
   
   //GUARDAR CONCEPTOS DESDE OTRAS VISTAS
    $scope.nuevoRegistroConceptoEnMercaderia = function () {
        $scope.concepto = { Valor: "1", Caracteristicas: [], Categorias: [] };
        $scope.concepto.EsBien = true;
        $timeout(function () {
            $('#categoria').trigger("change");
        }, 100);
        $scope.caracteristicasSeleccionadas = [];
        $scope.guardadoParaFuera = true;
        $scope.tipoRegistroFuera = 1;
    }




    //CARACTERISTICAS
    $scope.guardarCaracteristica = function () {
        console.debug($scope.caracteristica);
        conceptoService.guardarCaracteristica({ caracteristica: $scope.caracteristica }).success(function (data) {
            $scope.caracteristicas.push({ Id: data.data, Nombre: $scope.caracteristica.Nombre, EsComun : $scope.caracteristica.EsComun });
            $scope.caracteristica = { EsComun: true, open: false };
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.seleccion = function (item) {
        var idx = $scope.caracteristicasSeleccionadas.indexOf(item.Id);
        /* is currently selected*/
        if (idx > -1) {
            $scope.caracteristicasSeleccionadas.splice(idx, 1);
            $scope.concepto.Caracteristicas.splice(idx, 1);
        }
        /* is newly selected*/
        else {
            $scope.caracteristicasSeleccionadas.push(item.Id);
            $scope.concepto.Caracteristicas.push(item);
        }
    }

    $scope.accion = function (bandera) {
        if (bandera) {
            $scope.caracteristica = { EsComun: true, open: true };
            //$('input:not(".input-panel")').each(function () { $(this).prop("disabled", false); });
        }
        else {
            $scope.caracteristica = { EsComun: true, open: false };
            //$('input:not(".input-panel")').each(function () { $(this).prop("disabled", true); });
        }
    }
   

    
});
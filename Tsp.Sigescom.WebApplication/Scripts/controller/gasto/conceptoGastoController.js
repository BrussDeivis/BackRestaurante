app.controller('conceptoGastoController', function ($scope, $compile, $rootScope, SweetAlert, confirm, $timeout, blockUI, DTOptionsBuilder, DTColumnDefBuilder, maestroService, conceptoService, productoService) {

    $scope.inicializar = function () {
        $scope.accionModal = 'REGISTRAR';
        $scope.obtenerConceptos();
        $scope.obtenerConceptosBasicos();
    }

    $scope.obtenerConceptos = function () {
        conceptoService.obtenerConceptosGastos().success(function (data) {
            $scope.conceptosDeGasto = data;
        }).error(function (data) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }


    $scope.editarConcepto = function (id) {
        $scope.accionModal = 'EDITAR';
        conceptoService.obtenerConceptoGasto({id}).success(function (data) {
            let conceptoAEditar = data;
            $scope.concepto = { ConceptoBasicoSeleccionado: true };
            $scope.concepto.Id = conceptoAEditar.Id;
            $scope.concepto.ConceptoBasico = $scope.conceptosBasicos.find(concepto => concepto.Id === conceptoAEditar.IdConceptoBasico);
            $scope.concepto.Sufijo = conceptoAEditar.Sufijo;
            $timeout(function () { $('#selectorConceptoBasico').trigger("change"); }, 100);
        }).error(function (data) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }

  
    

    $scope.nuevoRegistro = function () {
        $scope.accionModal = 'REGISTRAR';
        $scope.obtenerConceptosBasicos();
        $scope.concepto = { ConceptoBasicoSeleccionado : true };
    }

    $scope.obtenerConceptosBasicos = function () {
        maestroService.obtenerConceptosBasicos({}).success(function (data) {
            //obtenerConceptosDeConceptoGasto
            $scope.conceptosBasicos = data;
        }).error(function (data) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }
    $scope.ingresarConceptoBasico = function () {
        $scope.concepto.ConceptoBasicoSeleccionado = false; 
    }

    $scope.seleccionarConceptoBasico = function () {
        $scope.concepto.ConceptoBasicoSeleccionado = true; 
    }

    $scope.cerrar = function () {
        $('#modal-registro-concepto-gasto').modal('hide');
    }

    $scope.guardar = function () {
        conceptoService.guardarConceptoGasto({ concepto: $scope.concepto }).success(function (data) {
            $scope.concepto = {};
            $scope.obtenerConceptos();
            $scope.obtenerConceptosBasicos();
            SweetAlert.success("Correcto", data.result_description);
            $('#modal-registro-concepto-gasto').modal('hide');
        }).error(function (data) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }

});

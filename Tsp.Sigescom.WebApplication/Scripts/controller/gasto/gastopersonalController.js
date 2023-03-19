app.controller('gastopersonalController', function ($scope, $compile, $rootScope, confirm, $timeout, blockUI, DTOptionsBuilder, DTColumnDefBuilder, maestroService, empleadoService) {

    $scope.numerodocumento = {};
    $scope.idtipocomprobante = [];
    $scope.idempleado = [];
    $scope.conceptospago = [];
    $scope.nombre = {};
    //$scope.vistagastopersonal = { Lista: [] };
    $scope.gastopersonal = { Detalles: [] };

    $scope.vistagastopersonal = {
        Lista: [{ fecha: "04/05/2017", tipocomprobante: "Boleta", numerocomprobante: "0001000001", numerodocumento: "84747423", nombre: "Jose", apellido: "Espinoza", importetotal: "800.00", fechavencimiento: "04/05/2017", estado: "Pagado" },
                { fecha: "10/05/2017", tipocomprobante: "Boleta", numerocomprobante: "0001000002", numerodocumento: "29340167", nombre: "Victor", apellido: "Urbina", importetotal: "1500.00", fechavencimiento: "10/05/2017", estado: "Anulado" },
                { fecha: "15/05/2017", tipocomprobante: "Factura", numerocomprobante: "0001000003", numerodocumento: "74929634", nombre: "Manuel ", apellido: "Perez", importetotal: "500.00", fechavencimiento: "15/05/2017", estado: "Pagado" }]
    };

    $scope.ObtenerComprobante = function () {
        maestroService.obtenerTiposDeComprobante({}).success(function (data) {
            $scope.idtipocomprobante = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.ObtenerEmpleados = function () {
        maestroService.listarEmpleados({}).success(function (data) {
            $scope.idempleado = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.ObtenerConceptosPagoEmpleados = function () {
        maestroService.listarConceptosPagoEmpleados({}).success(function (data) {
            $scope.conceptospago = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.CargarDatosEmpleado = function () {
        console.debug($scope.gastopersonal.idempleado);
        maestroService.obtenerDatosEmpleado({ idEmpleado: $scope.gastopersonal.idempleado }).success(function (data) {
            $scope.gastopersonal.nombreapellido = data.nombreApellido;
            $scope.gastopersonal.cargo = data.cargo
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.guardarGastoPersonal = function () {
        maestroService.guardarMaestro({ maestro: $scope.maestro }).success(function (data) {
            $scope.messageSuccess(data.result_description);
            $scope.listarMaestros();
            $scope.maestro = {};
            $('#modal-registro-maestro').modal('hide');
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $rootScope.dtOptions.withLightColumnFilter({
        '1': { type: 'text', className: 'form-control padding-left-right-3' },
        '2': { type: 'select', values: [{ value: "Boleta", label: "BOLETA" }, { value: "Factura", label: "FACTURA" }], className: 'form-control' },
        '3': { type: 'text', className: 'form-control padding-left-right-3' },
        '4': { type: 'text', className: 'form-control padding-left-right-3' },
        '5': { type: 'text', className: 'form-control padding-left-right-3' },
        '6': { type: 'text', className: 'form-control padding-left-right-3' },
        '7': { type: 'text', className: 'form-control padding-left-right-3' },
        '8': { type: 'select', values: [{ value: "Pendiente", label: "PENDIENTE" }, { value: "Pago Parcial", label: "PAGO PARCIAL" }, { value: "Pagado", label: "PAGADO" }, { value: "Anulado", label: "ANULADO" }], className: 'form-control' }
    });

    $scope.agregarDetalle = function () {
        $scope.gastopersonal.Detalles.push($scope.gastopersonaldetalle);
        $scope.gastopersonaldetalle = {};
        $timeout(function () { $("#conceptospago").trigger("change"); }, 100);
        $scope.calcularTotal($scope.gastopersonal.Detalles);
    }

    $scope.quitarDetalle = function (index) {
        $scope.gastopersonal.Detalles.splice(index, 1);
        $scope.calcularTotal($scope.gastopersonal.Detalles);
    }

    $scope.calcularTotal = function (detalles) {
        $scope.gastopersonal.importetotal = 0;
        for (i = 0; i < detalles.length; i++) {

            $scope.gastopersonal.importetotal += 1*(detalles[i].importe);
        }
    }

    $scope.cerrar = function () {
        $('#modal-cambio').click();
    }

    $scope.closeModal = function () {
        $('#modal-registro-gastopersonal').modal('hide');
        $scope.gastopersonal = {};

        $timeout(function () {
            $("#conceptospago").trigger("change");
            $("#idempleado").trigger("change");
            $("#idtipocomprobante").trigger("change");}, 100);
        $scope.gastopersonal.Detalles = [];
    }

    //$rootScope.dtOptions.withLightColumnFilter({
    //    '1': { type: 'text', className: 'form-control padding-left-right-3' },
    //    '2': { type: 'select', values: [{ value: "Boleta", label: "BOLETA" }, { value: "Factura", label: "FACTURA" }], className: 'form-control' },
    //    '3': { type: 'text', className: 'form-control padding-left-right-3' },
    //    '4': { type: 'text', className: 'form-control padding-left-right-3' },
    //    '5': { type: 'text', className: 'form-control padding-left-right-3' },
    //    '6': { type: 'text', className: 'form-control padding-left-right-3' },
    //    '7': { type: 'select', values: [{ value: "S/F", label: "SIN FINANCIAMIENTO" }, { value: "Cuotas", label: "CUOTAS" }], className: 'form-control' },
    //    '8': { type: 'text', className: 'form-control padding-left-right-3' },
    //    '9': { type: 'select', values: [{ value: "Pendiente", label: "PENDIENTE" }, { value: "Pago Parcial", label: "PAGO PARCIAL" }, { value: "Pagado", label: "PAGADO" }, { value: "Anulado", label: "ANULADO" }], className: 'form-control' }
    //});


    //$scope.guardarGastoPersonal = function () {
    //        gastoService.registrarGatoPersonal({ CompraEME: $scope.compraeme}).success(function (data) {
    //            $scope.messageSuccess(data.result_description);
    //            $('#modal-registro-compraeme').modal('hide');
    //            $scope.listarBandeja($scope.bandeja.FechaInicio, $scope.bandeja.FechaFinal);
    //        }).error(function (data) {
    //            $scope.messageError(data.error);
    //        });
      
    //}

    //$scope.listarBandeja = function (fecha_inicio, fecha_fin) {
    //    console.debug("rfefe");
    //    console.debug(fecha_inicio);
    //    console.debug(fecha_fin);
    //    gastoService.listarCompraEME({ fecha_inicial: fecha_inicio, fecha_final: fecha_fin }).success(function (data) {
    //        $scope.vistacompraeme.Lista = data;
    //    }).error(function (data) {
    //        $scope.messageError(data.error);
    //    });
    //}

    $scope.ObtenerComprobante();
    $scope.ObtenerEmpleados();
    $scope.ObtenerConceptosPagoEmpleados();
});




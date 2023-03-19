app.controller('servicioeimpuestoController', function ($scope, $compile, $rootScope, confirm, $timeout, blockUI, DTOptionsBuilder, DTColumnDefBuilder, clienteService, maestroService) {

    $scope.servicioeimpuesto = {};
    $scope.idtipocomprobante = [];
    $scope.numerodocumento = {};
    $scope.idproveedor = [];
    $scope.idtiposervicioimpuesto = []; 
    $scope.idservicio = [];
    $scope.financiamiento = { Detalles: [] };
    $scope.financiamientodetalle = {};
    $scope.vistaservicioeimpuesto = { Lista: [] };

    $scope.vistaservicioeimpuesto = {
        Lista: [{ fecha: "05/06/2017", tipocomprobante: "Boleta", numerocomprobante: "0001000001", tiposervicio: "Tercero", servicio: "Contabilidad", proveedor: "Import Peru", detalle: "Junio", importetotal: "800.00", financiamiento: "S/F", fechavencimiento: "07/06/2017", estado: "Pagado" },
                { fecha: "05/06/2017", tipocomprobante: "Boleta", numerocomprobante: "0001000002", tiposervicio: "Tercero", servicio: "Electricidad", proveedor: "Electra EIRL", detalle: "Junio", importetotal: "1500.00", financiamiento: "S/F", fechavencimiento: "07/06/2017", estado: "Pagado" },
                { fecha: "06/06/2017", tipocomprobante: "Factura", numerocomprobante: "0001000003", tiposervicio: "Impuesto", servicio: "IGV", proveedor: "SUNAT", detalle: "IGV", importetotal: "500.00", financiamiento: "S/F", fechavencimiento: "10/06/2017", estado: "Pagado" }]
    };

    $scope.diavencimiento = {
        Lista: [{ id: "1", nombre: "1 de cada mes" }, { id: "2", nombre: "2 de cada mes" }, { id: "3", nombre: "3 de cada mes" }, { id: "4", nombre: "4 de cada mes" }, { id: "5", nombre: "5 de cada mes" }, { id: "6", nombre: "6 de cada mes" }, { id: "7", nombre: "7 de cada mes" }, { id: "8", nombre: "8 de cada mes" }, { id: "9", nombre: "9 de cada mes" }, { id: "10", nombre: "10 de cada mes" },
                { id: "11", nombre: "11 de cada mes" }, { id: "12", nombre: "12 de cada mes" }, { id: "13", nombre: "13 de cada mes" }, { id: "14", nombre: "14 de cada mes" }, { id: "15", nombre: "15 de cada mes" }, { id: "16", nombre: "16 de cada mes" }, { id: "17", nombre: "17 de cada mes" }, { id: "18", nombre: "18 de cada mes" }, { id: "19", nombre: "19 de cada mes" }, { id: "20", nombre: "20 de cada mes" },
                { id: "21", nombre: "21 de cada mes" }, { id: "22", nombre: "22 de cada mes" }, { id: "23", nombre: "23 de cada mes" }, { id: "24", nombre: "24 de cada mes" }, { id: "25", nombre: "25 de cada mes" }, { id: "26", nombre: "26 de cada mes" }, { id: "27", nombre: "27 de cada mes" }, { id: "28", nombre: "28 de cada mes" }, { id: "29", nombre: "Antepenultimo dia de cada mes" }, { id: "30", nombre: "Penultimo dia de cada mes" }, { id: "31", nombre: "Ultimo dia de cada mes" }, ]
    };

    $scope.serviciotemporal = {
        Lista: [{ id: "1", nombre: "Agua" }, { id: "2", nombre: "IGV" }, { id: "3", nombre: "Electricidad" }, { id: "4", nombre: "Telefono" }, { id: "5", nombre: "Publicidad" }, { id: "6", nombre: "Impuesto financiero" }, { id: "7", nombre: "Multa municipal" }, { id: "8", nombre: "Otro" }]
    };

    $scope.ObtenerComprobante = function () {
        maestroService.obtenerTiposDeComprobante({}).success(function (data) {
            $scope.idtipocomprobante = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.ObtenerProveedores = function () {
        maestroService.obtenerProveedores({}).success(function (data) {
            $scope.idproveedor = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.ObtenerTipoServicioImpuesto = function () {
        maestroService.listarTiposServicioImpuesto({}).success(function (data) {
            $scope.idtiposervicioimpuesto = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.ObtenerServicio = function () {
        maestroService.listarTiposDeComprobante({}).success(function (data) {
            $scope.tipocomprobante = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.IniciarFinanciamiento = function () {
        $scope.financiamiento.importepagar = $scope.servicioeimpuesto.importe;
        $scope.financiamiento.creditocapital = $scope.servicioeimpuesto.importe;
    }

    $scope.calcularcreditocapital = function () {
        $scope.financiamiento.creditocapital = $scope.financiamiento.importepagar - $scope.financiamiento.inicial;
        $scope.calculartotalinteres();
    }

    $scope.calculartotalinteres = function () {
        $scope.financiamiento.totalinteres = $scope.formatNumber(parseFloat($scope.financiamiento.creditocapital) + parseFloat($scope.financiamiento.interes),2);
    }

    $scope.calcularinteres = function () {
        $scope.financiamiento.interes = $scope.formatNumber(parseFloat($scope.financiamiento.totalinteres) - parseFloat($scope.financiamiento.creditocapital), 2);
    }

    $scope.agregarCuota = function () {
        $scope.financiamientodetalle.importe = $scope.formatNumber(parseFloat($scope.financiamientodetalle.capital) + parseFloat($scope.financiamientodetalle.interes), 2);
        $scope.financiamiento.Detalles.push($scope.financiamientodetalle);
        $scope.financiamientodetalle = {};
        $scope.calcularTotalFinanciamiento($scope.financiamiento.Detalles);
    }

    $scope.calcularTotalFinanciamiento = function (detalles) {
        $scope.financiamiento.importetotal = 0;
        for (i = 0; i < detalles.length; i++) {
            $scope.financiamiento.importetotal += 1*detalles[i].importe; 
        }  
    }

    $scope.generarCuota = function () {
        var fecha_actual = new Date(),anio=fecha_actual.getFullYear() ;
        var temp = $scope.financiamientodetalle.cuota;
        var mes=fecha_actual.getDate()<$scope.financiamientodetalle.diavencimiento?fecha_actual.getMonth():fecha_actual.getMonth()+1;
        var dia = $scope.financiamientodetalle.diavencimiento;
        for (i = 0; i < temp; i++) {
            $scope.financiamientodetalle.capital = $scope.formatNumber(parseFloat($scope.financiamiento.creditocapital) / parseFloat(temp), 2);
            $scope.financiamientodetalle.interes = $scope.formatNumber(parseFloat($scope.financiamiento.interes) / parseFloat(temp), 2);
            $scope.financiamientodetalle.importe = $scope.formatNumber(parseFloat($scope.financiamientodetalle.capital) + parseFloat($scope.financiamientodetalle.interes), 2);
            $scope.financiamientodetalle.detalle = "-";
            
            if (mes == 13) {
                mes = 1;
                anio++;
            }

            if(mes==2 && (dia == 28 || dia == 29)){
                $scope.financiamientodetalle.fechavencimiento = $scope.formatDate(new Date(anio, mes, 0), "ES");
            }else if ((mes==1 || mes == 3 || mes== 5 || mes==7 || mes==8 || mes == 10 || mes== 12) && dia == 31){
                $scope.financiamientodetalle.fechavencimiento = $scope.formatDate(new Date(anio, mes, 0), "ES");
            }else{
                $scope.financiamientodetalle.fechavencimiento = $scope.formatDate(new Date(anio, mes, dia), "ES");
            }
            $scope.financiamiento.Detalles.push($scope.financiamientodetalle);
            $scope.financiamientodetalle = {};
            mes++;
        }
        $scope.calcularTotalFinanciamiento($scope.financiamiento.Detalles);
    }

    $scope.deshabilitarCampos = function (valor) {

    if (valor == "1") {
        //habilitamos - - Capital - Interes - Fecha Venc - Detalle
        //deshabilitamos - Dia Venc Cuota 
        $("#capitalcuota").prop("disabled",false);
        $("#interescuota").prop("disabled", false);
        $("#fechavencimientocuota").prop("disabled", false);
        $("#detallecuota").prop("disabled", false);
        $("#diavencimiento").prop("disabled", true);
        $("#actualizarCuota").attr("disabled", "disabled");
    } else if (valor == "2") {
        //habilitamos - Dia Venc Cuota
        //deshabilitamos - Capital - Interes - Fecha Venc - Detalle
        $("#capitalcuota").prop("disabled", true);
        $("#interescuota").prop("disabled", true);
        $("#fechavencimientocuota").prop("disabled", true);
        $("#detallecuota").prop("disabled", true);
        $("#diavencimiento").prop("disabled", false);
        $("#diavencimiento").val("");
        $("#actualizarCuota").attr("disabled", "disabled");
        }
    }
    $rootScope.dtOptions.withLightColumnFilter({
        '1': { type: 'text', className: 'form-control padding-left-right-3' },
        '2': { type: 'select', values: [{ value: "Boleta", label: "BOLETA" }, { value: "Factura", label: "FACTURA" }], className: 'form-control' },
        '3': { type: 'text', className: 'form-control padding-left-right-3' },
        '4': { type: 'select', values: [{ value: "Basico", label: "BASICO" }, { value: "Tercero", label: "TERCERO" }, { value: "Impuesto", label: "IMPUESTO" }], className: 'form-control' },
        '5': { type: 'text', className: 'form-control padding-left-right-3' },
        '6': { type: 'text', className: 'form-control padding-left-right-3' },
        '7': { type: 'text', className: 'form-control padding-left-right-3' },
        '8': { type: 'text', className: 'form-control padding-left-right-3' },
        '9': { type: 'select', values: [{ value: "S/F", label: "SIN FINANCIAMIENTO" }, { value: "Cuotas", label: "CUOTAS" }], className: 'form-control' },
        '10': { type: 'text', className: 'form-control padding-left-right-3' },
        '11': { type: 'select', values: [{ value: "Pendiente", label: "PENDIENTE" }, { value: "Pago Parcial", label: "PAGO PARCIAL" }, { value: "Pagado", label: "PAGADO" }, { value: "Anulado", label: "ANULADO" }], className: 'form-control' }
    });
    $scope.ObtenerComprobante();
    $scope.ObtenerProveedores();
    $scope.ObtenerTipoServicioImpuesto();
    
});

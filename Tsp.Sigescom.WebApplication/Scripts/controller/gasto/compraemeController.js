app.controller('compraemeController', function ($scope, $rootScope, $compile, confirm, $timeout, blockUI, DTOptionsBuilder, DTColumnDefBuilder, maestroService, gastoService) {
    
    $scope.numerodocumento = {};
    $scope.idtipocomprobante = [];
    $scope.idproveedor = [];
    $scope.tipoproducto = [];
    $scope.tipobien = [];
    $scope.compraeme = { Detalles: []};
    $scope.compraemedetalle = {};
    $scope.financiamiento = { Detalles: [] };
    $scope.financiamientodetalle = {};

    $scope.vistacompraeme = {
        Lista: [{ fecha: "29/06/2017", tipocomprobante: "Boleta", numerocomprobante: "0001", proveedor: "Import Peru", detalle: "Lapto HP", importetotal: "800.00", financiamiento: "S/F", fechavencimiento: "13/07/2017", estado: "Pagado" },
                { fecha: "01/07/2017", tipocomprobante: "Boleta", numerocomprobante: "0002", proveedor: "Electra EIRL", detalle: "Utiles Oficina", importetotal: "1500.00", financiamiento: "S/F", fechavencimiento: "30/07/2017", estado: "Anulado" },
                { fecha: "05/07/2017", tipocomprobante: "Factura", numerocomprobante: "0003", proveedor: "Maldonado SA", detalle: "Varies Muebles", importetotal: "500.00", financiamiento: "Cuotas", fechavencimiento: "30/07/2017", estado: "Pago Parcial" }]
    };
   
    $scope.listatipoproducto = [{ id: 9, nombre: "EQUIPO" }, { id: 10, nombre: "MUEBLE" }, { id: 11, nombre:"ENSERE" }];


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

    //$scope.ObtenerTipoProducto = function () {
    //    maestroService.listarTiposProductoDeCompra({}).success(function (data) {
    //        $scope.tipoproducto = data;
    //    }).error(function (data) {
    //        $scope.messageError(data.error);
    //    });
    //}

    $scope.ObtenerTipoBien = function () {
        maestroService.listarTiposBien({}).success(function (data) {
            $scope.tipobien = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.calcularimporte = function () {
        $scope.compraemedetalle.importe = 0.00;
        if ($scope.compraemedetalle.cantidad != null && $scope.compraemedetalle.preciounitario != null) {
            $scope.compraemedetalle.importe = parseFloat($scope.compraemedetalle.cantidad) * parseFloat($scope.compraemedetalle.preciounitario);
        }
    }

    $scope.agregarDetalle = function () {
        $scope.compraeme.Detalles.push($scope.compraemedetalle);
        $timeout(function () {
            $("#tipobien").trigger("change");
            $("#tipoproducto").trigger("change");
        }, 100);
        $scope.calcularTotal($scope.compraeme.Detalles);
        $scope.compraemedetalle = {};
    }

    $scope.quitarDetalle = function (index) {
        $scope.compraeme.Detalles.splice(index,1);
        $scope.calcularTotal($scope.compraeme.Detalles);
    }
    
    $scope.calcularTotal = function (detalles) {
        $scope.compraeme.importetotal = 0;
        for (i = 0; i < detalles.length; i++) {
            $scope.compraeme.importetotal += detalles[i].importe;
        }
        $scope.financiamiento.importepagar = $scope.compraeme.importetotal;
        $scope.financiamiento.creditocapital = $scope.compraeme.importetotal;
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
        var cont=$scope.financiamiento.Detalles.length+1; cont ++;
        $scope.financiamientodetalle.importe = $scope.formatNumber(parseFloat($scope.financiamientodetalle.capital) + parseFloat($scope.financiamientodetalle.interes), 2);
        $scope.financiamiento.Detalles.push($scope.financiamientodetalle);
        $scope.financiamientodetalle = {};
        $scope.calcularTotalFinanciamiento($scope.financiamiento.Detalles);
        $scope.financiamientodetalle.cuota = cont;
    }

    $scope.editarCuota = function (model) {
        model.activoGuardar = true;
    }
    
    $scope.actualizarCuota = function (model) {
        model.activoGuardar = false;
    }
    $scope.quitarCuota = function (index) {
        $scope.financiamiento.Detalles.splice(index, 1);
        $scope.financiamientodetalle.cuota = $scope.financiamiento.Detalles.length + 1;
        $scope.calcularTotalFinanciamiento($scope.financiamiento.Detalles);
    }


    $scope.calcularTotalFinanciamiento = function (detalles) {
        $scope.financiamiento.importetotal = 0;
        for (i = 0; i < detalles.length; i++) {
            $scope.financiamiento.importetotal += 1*detalles[i].importe;
        }  
    }

    $scope.generarCuota = function () {
        $scope.financiamiento.Detalles = [];
        var fecha_actual = new Date(),anio=fecha_actual.getFullYear() ;
        var temp = $scope.financiamientodetalle.cuota;
        var mes=fecha_actual.getDate()<$scope.financiamientodetalle.diavencimiento?fecha_actual.getMonth():fecha_actual.getMonth()+1;
        var dia = $scope.financiamientodetalle.diavencimiento;
        for (i = 0; i < temp; i++) {
            $scope.financiamientodetalle.capital = $scope.formatNumber(parseFloat($scope.financiamiento.creditocapital) / parseFloat(temp), 2);
            $scope.financiamientodetalle.interes = $scope.formatNumber(parseFloat($scope.financiamiento.interes) / parseFloat(temp), 2);
            $scope.financiamientodetalle.importe = $scope.formatNumber(parseFloat($scope.financiamientodetalle.capital) + parseFloat($scope.financiamientodetalle.interes), 2);
            $scope.financiamientodetalle.detalle = "-";
            $scope.financiamientodetalle.diavencimiento = dia;
            
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

    $scope.cerrar = function () {
        $('#modal-cambio').click();
    }

    $scope.closeModal = function () {
        $('#modal-registro-compraeme').modal('hide');
        $scope.compraeme = {};
        $scope.compraeme.Detalles = [];
        $scope.financiamiento = {};
        $scope.financiamiento.Detalles = [];
        $timeout(function () {
            $("#idtipocomprobante").trigger("change");
            $("#idproveedor").trigger("change");  }, 100);
    }

    $rootScope.dtOptions.withLightColumnFilter({
        '1': { type: 'text', className: 'form-control padding-left-right-3' },
        '2': { type: 'select', values: [{ value: "Boleta", label: "BOLETA" }, { value: "Factura", label: "FACTURA" }], className: 'form-control' },
        '3': { type: 'text', className: 'form-control padding-left-right-3' },
        '4': { type: 'text', className: 'form-control padding-left-right-3' },
        '5': { type: 'text', className: 'form-control padding-left-right-3' },
        '6': { type: 'text', className: 'form-control padding-left-right-3' },
        '7': { type: 'select', values: [{ value: "S/F", label: "SIN FINANCIAMIENTO" }, { value: "Cuotas", label: "CUOTAS" }], className: 'form-control' },
        '8': { type: 'text', className: 'form-control padding-left-right-3' },
        '9': { type: 'select', values: [{ value: "Pendiente", label: "PENDIENTE" },{ value: "Pago Parcial", label: "PAGO PARCIAL" },{ value: "Pagado", label: "PAGADO" },{ value: "Anulado", label: "ANULADO" }], className: 'form-control' }
    });

    $scope.deshabilitarCampos = function (valor) {
        if (valor == "1") {
            //habilitamos - - Capital - Interes - Fecha Venc - Detalle
            //deshabilitamos - Dia Venc Cuota 
            $("#capitalcuota").prop("disabled", false);
            $("#interescuota").prop("disabled", false);
            $("#cuota").prop("disabled", true);
            $("#fechavencimientocuota").prop("disabled", false);
            $("#detallecuota").prop("disabled", false);
            $("#diavencimiento").prop("disabled", true);
        } else if (valor == "2") {
            //habilitamos - Dia Venc Cuota
            //deshabilitamos - Capital - Interes - Fecha Venc - Detalle
            $("#capitalcuota").prop("disabled", true);
            $("#interescuota").prop("disabled", true);
            $("#cuota").prop("disabled", false);
            $("#fechavencimientocuota").prop("disabled", true);
            $("#detallecuota").prop("disabled", true);
            $("#diavencimiento").prop("disabled", false);
            $("#diavencimiento").val("Seleccionar");
        }
    }
    $scope.guardarCompraEME = function () {
        if ($scope.compraeme.bandera) {
            gastoService.registrarCompraEMEFinanciado({ CompraEME: $scope.compraeme, Financiamiento: $scope.financiamiento }).success(function (data) {
                $scope.messageSuccess(data.result_description);
                $('#modal-registro-compraeme').modal('hide');
                $scope.listarBandeja($scope.bandeja.FechaInicio, $scope.bandeja.FechaFinal);
            }).error(function (data) {
                $scope.messageError(data.error);
            });
        } else {
            gastoService.registrarCompraEME({ CompraEME: $scope.compraeme }).success(function (data) {
                $scope.messageSuccess(data.result_description);
                $('#modal-registro-compraeme').modal('hide');
                $scope.listarBandeja($scope.bandeja.FechaInicio, $scope.bandeja.FechaFinal);
            }).error(function (data) {
                $scope.messageError(data.error);
            });
        } 
    }
    
    $scope.listarBandeja = function (fecha_inicio, fecha_fin) {
        console.debug("rfefe");
        console.debug(fecha_inicio);
        console.debug(fecha_fin);
        gastoService.listarCompraEME({ fecha_inicial: fecha_inicio, fecha_final: fecha_fin }).success(function (data) {
            $scope.vistacompraeme.Lista = data;
        }).error(function (data) { 
            $scope.messageError(data.error);
        });
    }

    $scope.ObtenerComprobante();
    $scope.ObtenerProveedores();
    //$scope.ObtenerTipoProducto();
    $scope.ObtenerTipoBien();
});
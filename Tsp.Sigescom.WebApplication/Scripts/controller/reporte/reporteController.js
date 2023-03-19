app.controller('reporteController', function ($scope, $q, $rootScope, DTOptionsBuilder, DTColumnDefBuilder, maestroService, clienteService, proveedorService, centroDeAtencionService) {


    $scope.formatoAMPM = function (hour) {

        var hours = hour.split(":")[0];
        var minutes = hour.split(":")[1];
        var seconds = hour.split(":")[2].split(".")[0];
        var ampm = hours >= 12 ? 'pm' : 'am';
        hours = hours % 12;
        hours = hours ? hours : 12; // the hour '0' should be '12'
        minutes = minutes < 10 ? '0' + minutes : minutes;
        var strTime = hours + ':' + minutes + ':' + seconds + ' ' + ampm;
        return strTime;
    }

    /*============================================================================================ METODOS GETS  ========================================================================================================== */

    $scope.obtenerClientes = function () {
        clienteService.obtenerClientesGenericoSinParentRol().success(function (data) {
            $scope.listaClientes = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.obtenerProveedores = function () {
        proveedorService.obtenerProveedoresGenerico().success(function (data) {
            $scope.listaProveedores = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.obtenerPuntosDeVenta = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        centroDeAtencionService.obtenerCentrosDeAtencionConRolPuntoDeVentaVigentesConEstablecimientoComercial().success(function (data) {
            $scope.listaPuntosDeVenta = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }


    $scope.obtenerPuntosDeCompra = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        centroDeAtencionService.obtenerCentrosDeAtencionConRolPuntoDeCompraVigentesConEstablecimientoComercial().success(function (data) {
            $scope.listaPuntosDeCompra = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.obtenerEstablecimientos = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        centroDeAtencionService.obtenerEstablecimientosComerciales().success(function (data) {
            $scope.listaEstablecimientos = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.cargarCentrosAtencionConRolCaja = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        centroDeAtencionService.obtenerCentrosDeAtencionConRolCajaVigentesPorEstablecimientoComercial({ idEstablecimientoComercial: $scope.establecimiento.Id }).success(function (data) {
            $scope.listaCentrosAtencion = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.cargarCentrosAtencionConRolPuntoDeVenta = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        centroDeAtencionService.obtenerCentrosDeAtencionConRolPuntoDeVentaVigentesPorEstablecimientoComercial({ idEstablecimientoComercial: $scope.establecimiento.Id }).success(function (data) {
            $scope.listaCentrosAtencion = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    /*====================================================================================== METODOS PARA LA GESTION DE MENSAJES DE ADVERTENCIA ========================================================================================================== */

    $scope.comprobarElementoEnArrayIngresado = function (elemento, idElemento, idBtn) {
        let buttonIsDisabled = elemento && elemento.length > 0 ? false : true;
        mensajeDeAdvertencia(buttonIsDisabled, idElemento, idBtn);
    }


    function mensajeDeAdvertencia(buttonIsDisabled, idElemento, idBtn) {
        if (buttonIsDisabled) {
            $('#' + idBtn).attr("disabled", "disabled");
            $('#' + idElemento).addClass("input-report-content");
            $('#' + idElemento).removeClass("no-after-input-report-content");
        } else {
            $('#' + idBtn).removeAttr("disabled");
            $('#' + idElemento).addClass("no-after-input-report-content");
            $('#' + idElemento).removeClass("input-report-content");
        }
    }

    /*============================================================================================ METODOS REPORTE ADMINISTRADOR  ========================================================================================================== */

    $scope.inicializarReporteGerente = function () {
        $scope.fechaInicio = fechaHoraInicio.split(" ")[0];
        $scope.fechaFin = fechaHoraFin.split(" ")[0];
        $scope.horaInicio = fechaHoraInicio.split(" ")[1];
        $scope.horaFin = fechaHoraFin.split(" ")[1];
        $scope.horaInicio = $scope.formatoAMPM($scope.horaInicio);
        $scope.horaFin = $scope.formatoAMPM($scope.horaFin);
        $scope.reporte = {
            FechaInicio: $scope.fechaInicio,
            FechaFinal: $scope.fechaFin,
            HoraInicio: $scope.horaInicio,
            HoraFinal: $scope.horaFin,
            //Reporte de administrador de compra por tipo
            FechaInicioReporteDeCompraPorTipo: $scope.fechaInicio,
            FechaFinalReporteDeCompraPorTipo: $scope.fechaFin,
            TipoComprobante: 1,//Todos los comprobantes
            //cod.XC1  de reportePorComprobante de gerente
            FechaInicioReporteDeCompraPorComprobante: $scope.fechaInicio,
            FechaFinalReporteDeCompraPorComprobante: $scope.fechaFin,
            //cod.XC2  de reportePorComprobante de gerente
            FechaInicioReporteDeCompraPorConcepto: $scope.fechaInicio,
            FechaFinalReporteDeCompraPorConcepto: $scope.fechaFin,
        };

        $scope.actualizarURLReporteConsolidado();
        $scope.actualizarURLReporteDeCompraPorTipo();
        $scope.actualizarURLReporteDeCompraPorComprobante();
        $scope.actualizarURLReporteDeCompraPorConcepto();
    }

    
    $scope.actualizarURLReporteConsolidado = function () {//cod:XY7,  usa Gerente
        $scope.URLReporteConsolidado = URL_ + "/Reporte/ReporteConsolidado?fechaInicio=" + $scope.reporte.FechaInicio + ' ' + $scope.reporte.HoraInicio + "&fechaFin=" + $scope.reporte.FechaFinal + ' ' + $scope.reporte.HoraFinal;
    }

    $scope.inicializarReporteGasto = function () {
        $scope.fechaInicio = fechaHoraInicio.split(" ")[0];
        $scope.fechaFin = fechaHoraFin.split(" ")[0];
        $scope.horaInicio = fechaHoraInicio.split(" ")[1];
        $scope.horaFin = fechaHoraFin.split(" ")[1];
        $scope.horaInicio = $scope.formatoAMPM($scope.horaInicio);
        $scope.horaFin = $scope.formatoAMPM($scope.horaFin);

        $scope.reporteGasto = {
            FechaInicio: $scope.fechaInicio,
            FechaFinal: $scope.fechaFin,
            HoraInicio: $scope.horaInicio,
            HoraFinal: $scope.horaFin
        };

        $scope.obtenerEstablecimientos();
        $scope.modoSelectorReporte = 1;

        $scope.actualizarURLReporteDeGastosPorConcepto();//cod. XY8
    }

    $scope.cargarCentrosAtencionYActualizarURLReporteDeGastosPorConcepto = function () {
        $scope.cargarCentrosAtencionConRolCaja().then(function (resultado_) {
            $scope.actualizarURLReporteDeGastosPorConcepto();
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
    }

    $scope.actualizarURLReporteDeGastosPorConcepto = function () {
        let esReporteGlobal = $scope.modoSelectorReporte == 1;//EsReporteGlobal
        let idsCentrosAtencion = "";
        let nombreCentroAtencion = ($scope.modoSelectorReporte == 3) ? $scope.centroAtencion.Nombre : ($scope.modoSelectorReporte == 2) ? $scope.establecimiento.Nombre : "nn";

        if ($scope.modoSelectorReporte == 2)
        {
            for (var i = 0; i < $scope.listaCentrosAtencion.length; i++)
            {
                idsCentrosAtencion += "&idsCentrosAtencion=" + $scope.listaCentrosAtencion[i].Id;
            }
        } else
        {
            idsCentrosAtencion = "&idsCentrosAtencion=" + (esReporteGlobal ? "0" : $scope.centroAtencion.Id);
        }
        $scope.URLReporteDeGastosPorConcepto = URL_ + "/Reporte/ReporteDeGastosPorConcepto?fechaInicio=" + $scope.reporteGasto.FechaInicio +
            "&fechaFin=" + $scope.reporteGasto.FechaFinal + "&esReporteGlobal=" + esReporteGlobal + "&nombreCentroDeAtencion=" + nombreCentroAtencion +
            idsCentrosAtencion;
    }

    $scope.actualizarURLReporteDeCompraPorTipo = function () {
        //llamando metodo de controlador Compra COD XC1
        $scope.URLReporteDeCompraPorTipo = URL_ + "/Reporte/ReporteDecompraPorTipo?fechaInicio=" + $scope.reporte.FechaInicioReporteDeCompraPorTipo + "&fechaFin=" + $scope.reporte.FechaFinalReporteDeCompraPorTipo + "&tipoComprobante=" + $scope.reporte.TipoComprobante;
    }

    $scope.actualizarURLReporteDeCompraPorComprobante = function () {
        //llamando metodo de controlador Compra COD XC1
        $scope.URLReporteDeCompraPorComprobante = URL_ + "/Reporte/ReporteDecompraPorComprobante?fechaInicio=" + $scope.reporte.FechaInicioReporteDeCompraPorComprobante + "&fechaFin=" + $scope.reporte.FechaFinalReporteDeCompraPorComprobante;
    }

    $scope.actualizarURLReporteDeCompraPorConcepto = function () {
        //llamando metodo de controlador Compra COD XC2
        $scope.URLReporteDeCompraPorConcepto = URL_ + "/Reporte/ReporteDeCompraPorConcepto?fechaInicio=" + $scope.reporte.FechaInicioReporteDeCompraPorConcepto + "&fechaFin=" + $scope.reporte.FechaFinalReporteDeCompraPorConcepto;
    }


    /*============================================================================================ METODOS REPORTE DEUDA POR CLIENTE  ========================================================================================================== */

    $scope.inicializadorReporteDeudaPorCliente = function () {

        $scope.fechaInicio = fechaHoraInicio.split(" ")[0];
        $scope.fechaFin = fechaHoraFin.split(" ")[0];

        $scope.horaInicio = fechaHoraInicio.split(" ")[1];
        $scope.horaFin = fechaHoraFin.split(" ")[1];

        $scope.horaInicio = $scope.formatoAMPM($scope.horaInicio);
        $scope.horaFin = $scope.formatoAMPM($scope.horaFin);

        $scope.reporteDeudaPorCliente = {
            Cliente: {
                Id: 0, Nombre: ''
            },
            ClienteDeCartera: {
                Id: 0, Nombre: ''
            },
            PuntoDeVentaDeCartera: {
                Id: 0, Nombre: ''
            },
            FechaInicio: $scope.fechaInicio,
            FechaFinal: $scope.fechaFin,
            HoraInicio: $scope.horaInicio,
            HoraFinal: $scope.horaFin,
        };
        $scope.actualizarURLReporteDeDeudaPorCliente();
        $scope.actualizarURLReporteDeDeudaPorCarteraCliente();
        $scope.obtenerPuntosDeVenta();
        $scope.obtenerClientesDeCartera();
       // $scope.obtenerClientes();
    };

    $scope.obtenerClientesDeCartera = function () {
        clienteService.obtenerClientesDeCartera({ idCarteraDeCliente: $scope.reporteDeudaPorCliente.PuntoDeVentaDeCartera.Id }).success(function (data) {
            $scope.listaClientesDeCartera = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    };

    $scope.actualizarURLReporteDeDeudaPorCliente = function () {
        $scope.URLReporteDeDeudaPorCliente = URL_ + "/Reporte/ReporteEstadoDeCuenta?fechaInicio=" + $scope.reporteDeudaPorCliente.FechaInicio + ' ' + $scope.reporteDeudaPorCliente.HoraInicio +
            "&fechaFin=" + $scope.reporteDeudaPorCliente.FechaFinal + ' ' + $scope.reporteDeudaPorCliente.HoraFinal +
            "&idCliente=" + $scope.reporteDeudaPorCliente.Cliente.Id + "&nombreCliente=" + $scope.reporteDeudaPorCliente.Cliente.Nombre;
    }

    $scope.actualizarURLReporteDeDeudaPorCarteraCliente = function () {
        $scope.URLReporteDeEstadoCuentaPorCarteraDeCliente = URL_ + "/Reporte/ReporteDeEstadoDeCuentaClienteVentaCobro?fechaInicio="
            + $scope.reporteDeudaPorCliente.FechaInicio + ' ' + $scope.reporteDeudaPorCliente.HoraInicio + "&fechaFin=" + $scope.reporteDeudaPorCliente.FechaFinal + ' '
            + $scope.reporteDeudaPorCliente.HoraFinal + "&idCliente=" + $scope.reporteDeudaPorCliente.ClienteDeCartera.Id + "&nombreCliente="
            + $scope.reporteDeudaPorCliente.ClienteDeCartera.Nombre + "&idPuntoDeVenta=" + $scope.reporteDeudaPorCliente.PuntoDeVentaDeCartera.Id + "&nombrePuntoDeVenta="
            + $scope.reporteDeudaPorCliente.PuntoDeVentaDeCartera.Nombre;
    }
    /*============================================================================================ REPORTE DE DEUDAS Y PAGOS  ========================================================================================================== */

    //INICIALIZADOR
    $scope.inicializadorReporteDeudaYPago = function () {

        $scope.fechaInicio = fechaHoraInicio.split(" ")[0];
        $scope.fechaFin = fechaHoraFin.split(" ")[0];

        $scope.horaInicio = fechaHoraInicio.split(" ")[1];
        $scope.horaFin = fechaHoraFin.split(" ")[1];


        $scope.horaInicio = $scope.formatoAMPM($scope.horaInicio);
        $scope.horaFin = $scope.formatoAMPM($scope.horaFin);


        $scope.reportePago = {
            FechaInicio: $scope.fechaInicio,
            FechaFinal: $scope.fechaFin,
            HoraInicio: $scope.horaInicio,
            HoraFinal: $scope.horaFin,
            PuntoDeVentaParaReporteDePagoDeCliente: [],
            ClienteParaReporteDePago: [],
            PuntoDeVentaParaReporteDePagoAProveedor: [],
            ProveedorParaReporteDePago: []
        };

        $scope.reporteDeuda = {
            FechaInicio: $scope.fechaInicio,
            FechaFinal: $scope.fechaFin,
            HoraInicio: $scope.horaInicio,
            HoraFinal: $scope.horaFin,
            PuntoDeVentaParaReporteDeDeudaDeCliente: [],
            ClienteParaReporteDeDeuda: [],
            PuntoDeVentaParaReporteDeDeudaAProveedor: [],
            ProveedorParaReporteDeDeuda: []
        };

        $scope.obtenerClientes();
        $scope.obtenerProveedores();
        $scope.obtenerPuntosDeVenta();
        $scope.obtenerPuntosDeCompra();

    }

    $scope.actualizarURLReporteDePago = function () {
        $scope.actualizarURLReporteDePagoDeCliente();
        $scope.actualizarURLReporteDePagoAProveedor();
        $scope.actualizarURLReporteDeDeudaDeCliente();
        $scope.actualizarURLReporteDeDeudaAProveedor();

    }

    //REPORTE DE PAGOS (CLIENTES - PROVEEDORES)
    $scope.actualizarURLReporteDePagoDeCliente = function () {

        let idsPuntosDeCompraOVenta = "", idsProveedoresOClientes = "";

        if ($scope.reportePago.PuntoDeVentaParaReporteDePagoDeCliente) {
            for (var i = 0; i < $scope.reportePago.PuntoDeVentaParaReporteDePagoDeCliente.length; i++) {
                idsPuntosDeCompraOVenta += "&idsPuntosDeCompraOVenta=" + $scope.reportePago.PuntoDeVentaParaReporteDePagoDeCliente[i].Id;
            }
        }

        if ($scope.reportePago.ClienteParaReporteDePago) {
            for (var i = 0; i < $scope.reportePago.ClienteParaReporteDePago.length; i++) {
                idsProveedoresOClientes += "&idsProveedoresOClientes=" + $scope.reportePago.ClienteParaReporteDePago[i].Id;
            }
        }

        $scope.comprobarElementoEnArrayIngresado($scope.reportePago.PuntoDeVentaParaReporteDePagoDeCliente, 'input-puntos-venta-reporte-pago-cliente', 'btn-see-in-reporte-pago-cliente');
        $scope.comprobarElementoEnArrayIngresado($scope.reportePago.ClienteParaReporteDePago, 'input-clientes-reporte-pago-cliente', 'btn-see-in-reporte-pago-cliente');


        $scope.URLReporteDePagoDeCliente = URL_ + "/Reporte/ReporteDePago?fechaInicio=" + $scope.reportePago.FechaInicio + ' ' + $scope.reportePago.HoraInicio +
            "&fechaFin=" + $scope.reportePago.FechaFinal + ' ' + $scope.reportePago.HoraFinal + "&esCliente=" + true + idsPuntosDeCompraOVenta + idsProveedoresOClientes;
    }

    $scope.actualizarURLReporteDePagoAProveedor = function () {

        let idsPuntosDeCompraOVenta = "", idsProveedoresOClientes = "";

        if ($scope.reportePago.PuntoDeVentaParaReporteDePagoAProveedor) {
            for (var i = 0; i < $scope.reportePago.PuntoDeVentaParaReporteDePagoAProveedor.length; i++) {
                idsPuntosDeCompraOVenta += "&idsPuntosDeCompraOVenta=" + $scope.reportePago.PuntoDeVentaParaReporteDePagoAProveedor[i].Id;
            }
        }

        if ($scope.reportePago.ProveedorParaReporteDePago) {
            for (var i = 0; i < $scope.reportePago.ProveedorParaReporteDePago.length; i++) {
                idsProveedoresOClientes += "&idsProveedoresOClientes=" + $scope.reportePago.ProveedorParaReporteDePago[i].Id;
            }
        }

        $scope.comprobarElementoEnArrayIngresado($scope.reportePago.PuntoDeVentaParaReporteDePagoAProveedor, 'input-puntos-compra-reporte-pago-proveedor', 'btn-see-in-reporte-pago-proveedor');
        $scope.comprobarElementoEnArrayIngresado($scope.reportePago.ProveedorParaReporteDePago, 'input-proveedores-reporte-pago-proveedor', 'btn-see-in-reporte-pago-proveedor');

        $scope.URLReporteDePagoAProveedor = URL_ + "/Reporte/ReporteDePago?fechaInicio=" + $scope.reportePago.FechaInicio + ' ' + $scope.reportePago.HoraInicio +
            "&fechaFin=" + $scope.reportePago.FechaFinal + ' ' + $scope.reportePago.HoraFinal + "&esCliente=" + false + idsPuntosDeCompraOVenta + idsProveedoresOClientes;

    }

    //REPORTE DE DEUDAS (CLIENTES - PROVEEDORES)
    $scope.actualizarURLReporteDeDeudaDeCliente = function () {

        let idsPuntosDeCompraOVenta = "", idsProveedoresOClientes = "";

        if ($scope.reporteDeuda.PuntoDeVentaParaReporteDeDeudaDeCliente) {
            for (var i = 0; i < $scope.reporteDeuda.PuntoDeVentaParaReporteDeDeudaDeCliente.length; i++) {
                idsPuntosDeCompraOVenta += "&idsPuntosDeCompraOVenta=" + $scope.reporteDeuda.PuntoDeVentaParaReporteDeDeudaDeCliente[i].Id;
            }
        }

        if ($scope.reporteDeuda.ClienteParaReporteDeDeuda) {
            for (var i = 0; i < $scope.reporteDeuda.ClienteParaReporteDeDeuda.length; i++) {
                idsProveedoresOClientes += "&idsProveedoresOClientes=" + $scope.reporteDeuda.ClienteParaReporteDeDeuda[i].Id;
            }
        }

        $scope.comprobarElementoEnArrayIngresado($scope.reporteDeuda.PuntoDeVentaParaReporteDeDeudaDeCliente, 'input-puntos-venta-reporte-deuda-cliente', 'btn-see-in-reporte-deuda-cliente');
        $scope.comprobarElementoEnArrayIngresado($scope.reporteDeuda.ClienteParaReporteDeDeuda, 'input-clientes-reporte-deuda-cliente', 'btn-see-in-reporte-deuda-cliente');

        $scope.URLReporteDeDeudaDeCliente = URL_ + "/Reporte/ReporteDeDeuda?esCliente=" + true + idsPuntosDeCompraOVenta + idsProveedoresOClientes;
    }

    $scope.actualizarURLReporteDeDeudaAProveedor = function () {

        let idsPuntosDeCompraOVenta = "", idsProveedoresOClientes = "";

        if ($scope.reporteDeuda.PuntoDeVentaParaReporteDeDeudaAProveedor) {
            for (var i = 0; i < $scope.reporteDeuda.PuntoDeVentaParaReporteDeDeudaAProveedor.length; i++) {
                idsPuntosDeCompraOVenta += "&idsPuntosDeCompraOVenta=" + $scope.reporteDeuda.PuntoDeVentaParaReporteDeDeudaAProveedor[i].Id;
            }
        }

        if ($scope.reporteDeuda.ProveedorParaReporteDeDeuda) {
            for (var i = 0; i < $scope.reporteDeuda.ProveedorParaReporteDeDeuda.length; i++) {
                idsProveedoresOClientes += "&idsProveedoresOClientes=" + $scope.reporteDeuda.ProveedorParaReporteDeDeuda[i].Id;
            }
        }


        $scope.comprobarElementoEnArrayIngresado($scope.reporteDeuda.PuntoDeVentaParaReporteDeDeudaAProveedor, 'input-puntos-compra-reporte-deuda-proveedor', 'btn-see-in-reporte-deuda-proveedor');
        $scope.comprobarElementoEnArrayIngresado($scope.reporteDeuda.ProveedorParaReporteDeDeuda, 'input-proveedores-reporte-deuda-proveedor', 'btn-see-in-reporte-deuda-proveedor');

        $scope.URLReporteDeDeudaAProveedor = URL_ + "/Reporte/ReporteDeDeuda?esCliente=" + false + idsPuntosDeCompraOVenta + idsProveedoresOClientes;
    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    $scope.inicializarReporteUtilidadVenta = function () {
        $scope.fechaInicio = fechaHoraInicio.split(" ")[0];
        $scope.fechaFin = fechaHoraFin.split(" ")[0];
        $scope.horaInicio = fechaHoraInicio.split(" ")[1];
        $scope.horaFin = fechaHoraFin.split(" ")[1];
        $scope.horaInicio = $scope.formatoAMPM($scope.horaInicio);
        $scope.horaFin = $scope.formatoAMPM($scope.horaFin);
        $scope.reporte = {
            FechaInicio: $scope.fechaInicio,
            FechaFinal: $scope.fechaFin,
            HoraInicio: $scope.horaInicio,
            HoraFinal: $scope.horaFin,
        };

        $scope.obtenerEstablecimientos();
        $scope.obtenerConceptos();
        $scope.modoSelectorReporte = 1;
        $scope.establecimiento = {};
        $scope.centroAtencion = {};
        $scope.familias = {};

        $scope.actualizarURLReporteUtilidadVenta();  
    }

    $scope.obtenerConceptos = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        maestroService.obtenerFamiliasConceptosComercialesVigentes({}).success(function (data) {
            $scope.listaFamilias = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    };


    $scope.cargarCentrosAtencionYActualizarURLReporteUtilidadVenta = function () {
        $scope.cargarCentrosAtencionConRolPuntoDeVenta().then(function (resultado_) {
            $scope.actualizarURLReporteUtilidadVenta();
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
    }

    $scope.actualizarURLReporteUtilidadVenta = function () {
        let esReporteGlobal = $scope.modoSelectorReporte == 1;//EsReporteGlobal
        let idsCentrosAtencion = "";
        let idsFamilias = "";
        let nombreCentroAtencion = ($scope.modoSelectorReporte == 3) ? $scope.centroAtencion.Nombre : ($scope.modoSelectorReporte == 2) ? $scope.establecimiento.Nombre : "nn";

        if ($scope.familias) {
            for (var i = 0; i < $scope.familias.length; i++) {
                idsFamilias += "&idsFamilia=" + $scope.familias[i].Id;
            }
        }

        if ($scope.modoSelectorReporte == 2) {
            for (var i = 0; i < $scope.listaCentrosAtencion.length; i++) {
                idsCentrosAtencion += "&idsCentrosAtencion=" + $scope.listaCentrosAtencion[i].Id;
            }
        } else {
            idsCentrosAtencion = "&idsCentrosAtencion=" + (esReporteGlobal ? "0" : $scope.centroAtencion.Id);
        }
        $scope.URLReporteUtilidadVentaPorFamilia = URL_ + "/Reporte/ReporteDeUtilidadDeVentaPorFamilia?fechaInicio=" + $scope.reporte.FechaInicio + ' ' + $scope.reporte.HoraInicio + "&fechaFin=" + $scope.reporte.FechaFinal + ' ' + $scope.reporte.HoraFinal + "&esReporteGlobal=" + esReporteGlobal + "&nombreCentroDeAtencion=" + nombreCentroAtencion + idsCentrosAtencion;
        $scope.URLReporteUtilidadVentaPorConcepto = URL_ + "/Reporte/ReporteDeUtilidadDeVentaPorConcepto?fechaInicio=" + $scope.reporte.FechaInicio + ' ' + $scope.reporte.HoraInicio + "&fechaFin=" + $scope.reporte.FechaFinal + ' ' + $scope.reporte.HoraFinal + "&esReporteGlobal=" + esReporteGlobal + "&nombreCentroDeAtencion=" + nombreCentroAtencion + idsCentrosAtencion + idsFamilias;
    }
    //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    $scope.inicializarReporteCaja = function () {
        $scope.fechaInicio = fechaHoraInicio.split(" ")[0];
        $scope.fechaFin = fechaHoraFin.split(" ")[0];
        $scope.horaInicio = fechaHoraInicio.split(" ")[1];
        $scope.horaFin = fechaHoraFin.split(" ")[1];
        $scope.horaInicio = $scope.formatoAMPM($scope.horaInicio);
        $scope.horaFin = $scope.formatoAMPM($scope.horaFin);
        $scope.reporte = {
            FechaInicio: $scope.fechaInicio,
            FechaFinal: $scope.fechaFin,
            HoraInicio: $scope.horaInicio,
            HoraFinal: $scope.horaFin,
        };

        $scope.obtenerEstablecimientos();
        $scope.modoSelectorReporte = 1;
        $scope.establecimiento = {};
        $scope.centroAtencion = {};

        $scope.actualizarURLReporteCaja();
    }

    $scope.cargarCentrosAtencionYActualizarURLReporteCaja = function () {
        $scope.cargarCentrosAtencionConRolCaja().then(function (resultado_) {
            $scope.actualizarURLReporteCaja();
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
    }

    $scope.actualizarURLReporteCaja = function () {
        let esReporteGlobal = $scope.modoSelectorReporte == 1;//EsReporteGlobal
        let idsCentrosAtencion = "";
        let nombreCentroAtencion = ($scope.modoSelectorReporte == 3) ? $scope.centroAtencion.Nombre : ($scope.modoSelectorReporte == 2) ? $scope.establecimiento.Nombre : "nn";

        if ($scope.modoSelectorReporte == 2) {
            for (var i = 0; i < $scope.listaCentrosAtencion.length; i++) {
                idsCentrosAtencion += "&idsCentrosAtencion=" + $scope.listaCentrosAtencion[i].Id;
            }
        } else {
            idsCentrosAtencion = "&idsCentrosAtencion=" + (esReporteGlobal ? "0" : $scope.centroAtencion.Id);
        }
        $scope.URLReporteCaja = URL_ + "/Reporte/ReporteDeCaja?fechaInicio=" + $scope.reporte.FechaInicio + ' ' + $scope.reporte.HoraInicio + "&fechaFin=" + $scope.reporte.FechaFinal + ' ' + $scope.reporte.HoraFinal + "&esReporteGlobal=" + esReporteGlobal + "&nombreCentroDeAtencion=" + nombreCentroAtencion + idsCentrosAtencion;
    }
      //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    $scope.obtenerCajas = function () {
        centroDeAtencionService.obtenerCentrosDeAtencionConRolCaja().success(function (data) {
            $scope.listaCajas = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.inicializarReportePuntos = function () {
        $scope.fechaInicio = fechaHoraInicio.split(" ")[0];
        $scope.fechaFin = fechaHoraFin.split(" ")[0];
        $scope.horaInicio = fechaHoraInicio.split(" ")[1];
        $scope.horaFin = fechaHoraFin.split(" ")[1];
        $scope.horaInicio = $scope.formatoAMPM($scope.horaInicio);
        $scope.horaFin = $scope.formatoAMPM($scope.horaFin);
        $scope.reporte = {
            FechaInicio: $scope.fechaInicio,
            FechaFinal: $scope.fechaFin,
            HoraInicio: $scope.horaInicio,
            HoraFinal: $scope.horaFin,
        };

        $scope.obtenerCajas();
        $scope.URLReportePuntosPendientes = URL_ + "/Reporte/ReporteDePuntosPendientes";
        $scope.actualizarURLReportePuntosCanjeados();
    }

    $scope.actualizarURLReportePuntosCanjeados = function () {
        var idsCentrosAtencion = "";

        if ($scope.reporte.CajasSeleccionadas) {
            for (var i = 0; i < $scope.reporte.CajasSeleccionadas.length; i++) {
                idsCentrosAtencion += "&idsCentrosAtencion=" + $scope.reporte.CajasSeleccionadas[i].Id;
            }

        }
        $scope.URLReportePuntosCanjeados = URL_ + "/Reporte/ReporteDePuntosCanjeados?fechaInicio=" + $scope.reporte.FechaInicio + ' ' + $scope.reporte.HoraInicio + "&fechaFin=" + $scope.reporte.FechaFinal + ' ' + $scope.reporte.HoraFinal + idsCentrosAtencion;
    }
     
}); 
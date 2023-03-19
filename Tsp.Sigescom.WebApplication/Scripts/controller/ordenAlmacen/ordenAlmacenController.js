app.controller('ordenAlmacenController', function ($scope, $q, $compile, SweetAlert, $rootScope, $timeout, DTOptionsBuilder, DTColumnDefBuilder, ordenAlmacenService) {
    //************************  BANDEJA DE ORDENES DE ALMACEN *******************//

    $scope.inicializar = function () {
        $scope.cargarParametros();
        $scope.cargarColeccionesAsync();
        $scope.establecerDatosPorDefecto();
        $scope.inicializarComponentes();
        $scope.obtenerOrdenesAlmacen();
    }

    $scope.cargarParametros = function () {
        $scope.data = data;
    }

    $scope.cargarColeccionesAsync = function () {
    }

    $scope.establecerDatosPorDefecto = function () {
        $scope.principal = {
            Desde: $scope.data.FechaDesdeDefault, Hasta: $scope.data.FechaHastaDefault, PorIngresar: "false", EntregaInmediata: false, EntregaDiferida: true, EstadoPendiente: true, EstadoParcial: true, EstadoCompletada: true, AlmacenesSeleccionados: []
        };
        $scope.principal.AlmacenesSeleccionados.push($scope.data.AlmacenSesion);
    }

    $scope.inicializarComponentes = function () {
        $scope.inicializacionRealizada = true;
    }

    $scope.validarObtenerOrdenesAlmacen = function () {
        return (($scope.principal.Desde != undefined && $scope.principal.Desde != "") && ($scope.principal.Hasta != undefined && $scope.principal.Hasta != "") && ($scope.principal.EntregaInmediata || $scope.principal.EntregaDiferida || $scope.principal.EstadoPendiente || $scope.principal.EstadoParcial || $scope.principal.EstadoCompletada) && $scope.principal.AlmacenesSeleccionados != undefined && $scope.principal.AlmacenesSeleccionados.length > 0);
    }

    $scope.obtenerOrdenesAlmacen = function () {
        if ($scope.validarObtenerOrdenesAlmacen()) {
            let idsAlmacenes = [];
            for (var i = 0; i < $scope.principal.AlmacenesSeleccionados.length; i++) {
                idsAlmacenes.push($scope.principal.AlmacenesSeleccionados[i].Id)
            }
            ordenAlmacenService.obtenerOrdenesAlmacen({ desde: $scope.principal.Desde, hasta: $scope.principal.Hasta, porIngresar: $scope.principal.PorIngresar, entregaInmediata: $scope.principal.EntregaInmediata, entregaDiferida: $scope.principal.EntregaDiferida, estadoPendiente: $scope.principal.EstadoPendiente, estadoParcial: $scope.principal.EstadoParcial, estadoCompletada: $scope.principal.EstadoCompletada,  idsAlmacenes: idsAlmacenes }).success(function (data) {
                $scope.ordenesAlmacen = data;
                $scope.principal.EtiquetaInterna = $scope.principal.PorIngresar == "true" ? 'DESTINO' : 'ORIGEN';
                $scope.principal.EtiquetaExterna = $scope.principal.PorIngresar == "true" ? 'ORIGEN' : 'DESTINO';
                $scope.principal.EtiquetaTercero = $scope.principal.PorIngresar == "true" ? 'REMITENTE' : 'DESTINATARIO';
                $scope.principal.SeleccionadoPorIngresar = $scope.principal.PorIngresar == "true";
            }).error(function (data) {
                SweetAlert.error("Ocurrio un Problema", data.error);
            });
        }
    }

    $rootScope.dtOptions.withLightColumnFilter({
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
    });

    //********************** VER DE ORDEN DE ALMACEN ****************//

    $scope.cargarVerOrdenAlmacen = function (id) {
        ordenAlmacenService.obtenerOrdenAlmacen({ idOrdenAlmacen: id, porIngresar: $scope.principal.SeleccionadoPorIngresar }).success(function (data) {
            $scope.verOrdenAlmacen = data;
            $scope.verOrdenAlmacen.EsHistorial = false;
            $scope.verOrdenAlmacen.ClassParaModal = 'width-40';
            $scope.verOrdenAlmacen.ClassParaInformacion = 'col-md-12';
            $scope.verOrdenAlmacen.MostrarComprobante = false;
            $scope.verOrdenAlmacen.ClassParaComprobante = 'col-md-7';
            $('#id-comprobante-visualizacion').addClass("flex-center");
        }).error(function (data, status) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }

    $scope.cargarVerOrden = function (id) {
        let orden = $scope.verOrdenAlmacen.Ordenes.find(m => m.Id == id);
        $scope.verOrdenAlmacen.Comprobante = orden.Comprobante;
        $scope.verOrdenAlmacen.ClassParaModal = 'width-90';
        $scope.verOrdenAlmacen.ClassParaInformacion = 'col-md-5';
        $scope.verOrdenAlmacen.MostrarComprobante = true;
        $timeout(function () { $('#comboFormato').trigger("change"); }, 100);
    }

    $scope.cargarVerMovimiento = function (id) {
        let movimiento = $scope.verOrdenAlmacen.Movimientos.find(m => m.Id == id);
        $scope.verOrdenAlmacen.Comprobante = movimiento.Comprobante;
        $scope.verOrdenAlmacen.Comprobante.Operacion = movimiento;
        $scope.verOrdenAlmacen.Comprobante.Operacion.PuedeCambiarOperacion = $scope.verOrdenAlmacen.EsDiferida;
        $scope.verOrdenAlmacen.ClassParaModal = 'width-90';
        $scope.verOrdenAlmacen.ClassParaInformacion = 'col-md-5';
        $scope.verOrdenAlmacen.MostrarComprobante = true;
        $timeout(function () { $('#comboFormato').trigger("change"); }, 100);
    }

    $scope.cerrarVerComprobante = function () {
        $scope.verOrdenAlmacen.ClassParaModal = 'width-40';
        $scope.verOrdenAlmacen.ClassParaInformacion = 'col-md-12';
        $scope.verOrdenAlmacen.MostrarComprobante = false;
    }

    $scope.cerrarVerOrdenAlmacen = function () {
        let verOrdenAlmacenDesdeRegistro = $scope.verOrdenAlmacen.EsHistorial;
        $scope.verOrdenAlmacen = {};
        $scope.cerrar('modal-ver-orden-almacen');
        if (verOrdenAlmacenDesdeRegistro) {
            $('#modal-registro-movimiento-orden-almacen').modal('show');
        }
    }

    //********************** REGISTRO DE MOVIMIENTO DE ALMACEN ****************//

    $scope.cargarRegistroMovimientoOrdenAlmacen = function (orden) {
        $scope.registroMovimientoOrdenAlmacen = {};
        ordenAlmacenService.obtenerRegistroMovimientoOrdenAlmacen({ idOrdenAlmacen: orden.Id, porIngresar: $scope.principal.SeleccionadoPorIngresar }).success(function (data) {
            $scope.ordenAlmacenDeRegistro = data; 
            $scope.ordenAlmacenDeRegistro.DocumentoReferencia = orden.SerieNumeroDocumento;
            $scope.registradorGuiaRemisionApi.NuevoRegistroGuiaRemision();
        }).error(function (data, status) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }

    $scope.verHistorialOrdenAlmacen = function (id) {
        $('#modal-registro-movimiento-orden-almacen').modal('hide');
        $('#modal-ver-orden-almacen').modal('show');
        ordenAlmacenService.obtenerOrdenAlmacen({ idOrdenAlmacen: id, porIngresar: $scope.principal.SeleccionadoPorIngresar }).success(function (data) {
            $scope.verOrdenAlmacen = data;
            $scope.verOrdenAlmacen.EsHistorial = true;
            $scope.verOrdenAlmacen.ClassParaModal = 'width-40';
            $scope.verOrdenAlmacen.ClassParaInformacion = 'col-md-12';
            $scope.verOrdenAlmacen.MostrarComprobante = false;
            $scope.verOrdenAlmacen.ClassParaComprobante = 'col-md-7';
            $('#id-comprobante-visualizacion').addClass("flex-center");
        }).error(function (data, status) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        });
    }


    $scope.guardarRegistroMovimientoOrdenAlmacen = function () {
        ordenAlmacenService.guardarMovimientoOrdenAlmacen({ movimientoOrdenAlmacen: $scope.registroMovimientoOrdenAlmacen }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.verOrdenAlmacen = {};
            $scope.cerrarRegistroMovimientoOrdenAlmacen();
            $scope.obtenerOrdenesAlmacen();
        }).error(function (data) {
            SweetAlert.error("Ocurrio un Problema", data.error);
        }); 
    }

    $scope.cerrarRegistroMovimientoOrdenAlmacen = function () {
        $scope.ordenAlmacenDeRegistro = {};
        $scope.registroMovimientoOrdenAlmacen = {};
        $scope.cerrar('modal-registro-movimiento-orden-almacen');
    }

   // $scope.imprimirVerOrdenAlmacen = function () {
        //let ventanaImpresion = window.open(' ', 'popimpr');
        //ventanaImpresion.document.write(document.getElementById("verOrdenAlmacen").innerHTML);
        //ventanaImpresion.document.close();
        //ventanaImpresion.print();
        //ventanaImpresion.close();


        //let ventanaImpresion = window.open(' ', 'popimpr');
        //let contenido = document.getElementById("verOrdenAlmacen").innerHTML;
        //ventanaImpresion.document.open();
        //let contenidoConEstilos = '<html><head><link href = "/Content/bootstrap.min.css" type = "text/css" rel = "stylesheet" /><link href = "/Content/general.css" type = "text/css" rel = "stylesheet" />   <link href="/Content/style.css" type="text/css" rel="stylesheet" /><link href="/Content/datatables.css" type="text/css" rel="stylesheet" /><link href="/Content/default.css" type="text/css" rel="stylesheet" /></head ><body>' + contenido + ' </body></html>';
        //ventanaImpresion.document.write(contenidoConEstilos);
        //ventanaImpresion.document.close();
        //ventanaImpresion.print();
        //ventanaImpresion.close();


    //    var ventanaImpresion = window.open('', 'popimpr');
    //    ventanaImpresion.document.open();
    //    let contenido = document.getElementById("verOrdenAlmacen").innerHTML;
    //    let contenidoConEstilos = '<html><head><link href = "/Content/bootstrap.min.css" type = "text/css" rel = "stylesheet" /><link href = "/Content/general.css" type = "text/css" rel = "stylesheet" /><link href="/Content/style.css" type="text/css" rel="stylesheet" /><link href="/Content/datatables.css" type="text/css" rel="stylesheet" /><link href="/Content/default.css" type="text/css" rel="stylesheet" /></head><body onload="window.print()"><label class="col-md-12 modal-title no-bold fs-17 centrar-contenido">ORDEN DE ALMACEN ' + $scope.verOrdenAlmacen.TipoDocumento + '(' +  $scope.verOrdenAlmacen.SerieNumeroDocumento + ')' +'</label>' + contenido + '</body></html>';
    //    $timeout(function () { $('#verOrdenAlmacen').trigger("change"); }, 100);
    //    ventanaImpresion.document.write(contenidoConEstilos);
    //    ventanaImpresion.document.close();

    //}

    $scope.imprimirVerOrdenAlmacen = function () {
        var ventanaImpresion = window.open('', 'popimpr');
        ventanaImpresion.document.open();
        let contenido = document.getElementById("verOrdenAlmacen").innerHTML;
        let contenidoConEstilos = '<html><head><link href = "/Content/bootstrap.min.css" type = "text/css" rel = "stylesheet" /><link href = "/Content/general.css" type = "text/css" rel = "stylesheet" /><link href="/Content/style.css" type="text/css" rel="stylesheet" /><link href="/Content/datatables.css" type="text/css" rel="stylesheet" /><link href="/Content/default.css" type="text/css" rel="stylesheet" /></head><body onload="window.print()"><label class="col-md-12 modal-title no-bold fs-17 centrar-contenido">ORDEN DE ALMACEN ' + $scope.verOrdenAlmacen.TipoDocumento + '(' + $scope.verOrdenAlmacen.SerieNumeroDocumento + ')' + '</label>' + contenido + '</body></html>';
        $timeout(function () { $('#verOrdenAlmacen').trigger("change"); }, 100);
        ventanaImpresion.document.write(contenidoConEstilos);
        ventanaImpresion.document.close();
    }

    //********************** REGISTRO DE ORDENES DE ALMACEN ****************//

    //$scope.nuevoGrupoClientes = function () {
    //    $scope.limpiarOrdenes();
    //    $scope.establecerOrdenPorDefecto();
    //    $scope.accionModal = 'REGISTRAR';
    //}

    //$scope.limpiarOrdenes = function () {
    //    $scope.Ordenes = { Ordenes: [] };
    //}

    //$scope.establecerOrdenPorDefecto = function () {
    //    $scope.selectorResponsableAPI.EstablecerActorPorDefecto();
    //    $scope.selectorOrdenAPI.EstablecerActorPorDefecto();
    //}

    //$scope.cambioResponsable = function (actorComercial) {
    //    $scope.grupoClientes.Responsable = actorComercial;
    //    $scope.verificarInconsistencias();
    //};

    //$scope.cambioCliente = function (actorComercial) {
    //    $scope.grupoClientes.Cliente = actorComercial;
    //    $scope.verificarInconsistencias();
    //};

    //$scope.guardarGrupoClientes = function () {
    //    clienteService.guardarGrupoClientes({ grupoClientes: $scope.grupoClientes }).success(function (data) {
    //        SweetAlert.success("Correcto", data.result_description);
    //        $scope.cerrar("modal-registro-grupo-clientes");
    //        $scope.obtenerGruposClientes();
    //    }).error(function (data, status) {
    //        SweetAlert.error("Ocurrio un Problema", data.error);
    //    });
    //}

    //$scope.editarGrupoClientes = function (idGrupoCliente) {
    //    $scope.limpiarGrupoClientes();
    //    $scope.establecerDatosPorDefecto();
    //    $scope.accionModal = 'EDITAR';
    //    clienteService.obtenerGrupoClientes({ idGrupoCliente: idGrupoCliente }).success(function (data) {
    //        $scope.grupoClientes = data;
    //        $scope.selectorClienteAPI.EstablecerActorPorDefecto();
    //    }).error(function (data) {
    //        SweetAlert.error2(data);
    //    });
    //}


    $scope.invalidarMovimientoOrdenAlmacen = function (id, observacion) {
        ordenAlmacenService.invalidarMovimientoOrdenAlmacen({ idMovimientoOrdenAlmacen: id, observacion: observacion }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            let idOrdenAlmacen = $scope.verOrdenAlmacen.Id;
            $scope.cerrarVerComprobante();
            $scope.verOrdenAlmacen = {};
            $scope.obtenerOrdenesAlmacen();
            $scope.cargarVerOrdenAlmacen(idOrdenAlmacen);
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    //$scope.agregarClienteAGrupoClientes = function () {
    //    var validar = false;
    //    if ($scope.grupoClientes.Clientes.length > 0) {
    //        for (let i = 0; i < $scope.grupoClientes.Clientes.length; i++) {
    //            if ($scope.grupoClientes.Cliente.Id == $scope.grupoClientes.Clientes[i].Id) {
    //                validar = true;
    //                break;
    //            }
    //        }
    //    }
    //    if (validar) {
    //        $scope.selectorClienteAPI.EstablecerActorPorDefecto();
    //    }
    //    else {
    //        let cliente = { Id: $scope.grupoClientes.Cliente.Id, Documento: $scope.grupoClientes.Cliente.TipoDocumentoIdentidad.Valor + " - " + $scope.grupoClientes.Cliente.NumeroDocumentoIdentidad, Nombre: $scope.grupoClientes.Cliente.NombreORazonSocial };
    //        $scope.grupoClientes.Clientes.unshift(cliente);
    //        $scope.selectorClienteAPI.EstablecerActorPorDefecto();
    //    }
    //}

    //$scope.quitarClienteDeGrupoClientes = function (index) {
    //    $scope.grupoClientes.Clientes.splice(index, 1);
    //}

    //$scope.datosRequeridosParaGrupoClientesClientes = function () {
    //    return $scope.grupoClientes.CentroDeAtencion == undefined || $scope.grupoClientes.Clientes.length == 0 || $scope.grupoClientes.Clientes == undefined || $scope.grupoClientes.Clientes == null;
    //}

    //$scope.darBajaGrupoClientes = function (idGrupoCliente) {
    //    clienteService.darBajaGrupoClientes({ idGrupoCliente: idGrupoCliente }).success(function (data) {
    //        SweetAlert.success("Correcto", data.result_description);
    //        $scope.cerrar("modal-ver-grupo-clientes");
    //        $scope.obtenerGruposClientes();
    //    }).error(function (data) {
    //        $scope.messageError(data.error);
    //    });
    //}

    //$scope.verificarInconsistencias = function () {
    //    $scope.inconsistencias = [];
    //    if ($scope.grupoClientes.Codigo == undefined || $scope.grupoClientes.Codigo == '') {
    //        $scope.inconsistencias.push("Es necesario ingresar el código del grupo de clientes.");
    //    }
    //    if ($scope.grupoClientes.Nombre == undefined || $scope.grupoClientes.Nombre == '') {
    //        $scope.inconsistencias.push("Es necesario ingresar el nombre del grupo de clientes.");
    //    }
    //    if ($scope.grupoClientes.Tipo == undefined) {
    //        $scope.inconsistencias.push("Es necesario seleccionar un tipo del grupo de clientes.");
    //    }
    //    if ($scope.grupoClientes.Clasificacion == undefined) {
    //        $scope.inconsistencias.push("Es necesario seleccionar una clasificacion del grupo de clientes.");
    //    }
    //    if ($scope.grupoClientes.Responsable == undefined) {
    //        $scope.inconsistencias.push("Es necesario seleccionar un responsable del grupo de clientes.");
    //    } else {
    //        if ($scope.grupoClientes.Responsable.Id == $scope.idClienteGenerico) {
    //            $scope.inconsistencias.push("Es necesario identificar el responsable del grupo de clientes.");
    //        }
    //    }
    //    if ($scope.grupoClientes.Clientes == undefined) {
    //        $scope.inconsistencias.push("Es necesario seleccionar al menos un cliente para el grupo de clientes.");
    //    } else {
    //        if ($scope.grupoClientes.Clientes.length < 1) {
    //            $scope.inconsistencias.push("Es necesario seleccionar al menos un cliente para el grupo de clientes.");
    //        }
    //    }

    //}

    $scope.cerrar = function (nombreDelModal) {
        $('#' + nombreDelModal).modal('hide');
    }
});




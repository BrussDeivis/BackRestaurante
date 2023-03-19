app.controller('reporteInventarioValorizadoController', function ($scope, $rootScope, $timeout, $q,$http ,blockUI, DTColumnBuilder, DTOptionsBuilder, DTColumnDefBuilder, productoService, maestroService, conceptoService, centroDeAtencionService, almacenService, SweetAlert) {

    $scope.inventario = { Lista: [] };
    $scope.conceptos = [];
    $scope.establecimientos = [];
    $scope.establecimiento = {};
    $scope.centrosAtencion = [];
    $scope.conceptoBasico = {};
    $scope.centroAtencion = {};
    $scope.caracteristicas = [];
    $scope.caracteristicasSeleccionadas = [];
    $scope.caracteristica = {};
    $scope.listNombreCaracteristicasSeleccionadas = [];
    $scope.idsCaracteristicasSeleccionadas = [];
    $scope.itemTotal = {};
    //she doesnt love me 
    $scope.inicializar = function () {
        $scope.inventario = { Lista: [] };
        $scope.fecha = fechaActual;

        $scope.conLote = false;
        $scope.cargarColeccionesAsync();
        $scope.cargarColeccionesSync().then(function (resultado_) {
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
    
    }

    // CARGAR COLECCIONES ASYNC
    $scope.cargarColeccionesAsync = function () {

    }

    //CARGAR COLECCIONES SYNC
    $scope.cargarColeccionesSync = function () {
        var defered = $q.defer();
        var promiseList = [];
        //promiseList.push($scope.obtenerEstablecimientosComercialesGenerico());
        promiseList.push($scope.obtenerConceptos());
        promiseList.push($scope.obtenerCaracteristicas());
        promiseList.push($scope.obtenerCentrosDeAtencionConRolAlmacenVigentesConEstablecimientoComercialPromise());
        return $q.all(promiseList).then(function (response) {
            defered.resolve();
        }).catch(function (error) {
            defered.reject(e);
        });
    }

    //ESTABLECER DATOS POR DEFECTO
    $scope.establecerEstablecimientoComercialPorDefecto = function () {
        $scope.establecimiento = $scope.establecimientos[0];
    }

    $scope.establecerDatosPorDefecto = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        $scope.establecerEstablecimientoComercialPorDefecto();
        defered.resolve();
        return promise;
    }

    //METODOS PARA LOS COMBO BOX
    $scope.obtenerConceptos = function () {
        var defered = $q.defer();
        var promise = defered.promise;

        maestroService.obtenerFamiliasConceptosComercialesVigentes({}).success(function (data) {
            $scope.listaConceptos = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);

        });
        return promise;
    }

    $scope.obtenerCaracteristicas = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        conceptoService.verCaracteristicas({}).success(function (data) {
            $scope.caracteristicas = data;
            $timeout(function () {
                $('.caracteristica').select2({
                    language: "es"
                });
            }, 600);
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });

        return promise;
    }

    $scope.obtenerCentrosDeAtencion = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        centroDeAtencionService.obtenerCentrosDeAtencionPorEstablecimientoComercial({ idEstablecimientoComercial: $scope.establecimiento.Id }).success(function (data) {
            $scope.centrosAtencion = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.obtenerEstablecimientosComercialesGenerico = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        centroDeAtencionService.obtenerEstablecimientosComerciales().success(function (data) {
            $scope.establecimientos = data;
            defered.resolve();

        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    $scope.obtenerCentrosDeAtencionConRolAlmacenVigentesConEstablecimientoComercialPromise = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        centroDeAtencionService.obtenerCentrosDeAtencionConRolAlmacenVigentesConEstablecimientoComercial().success(function (data) {
            $scope.listaAlmacenes = data;
            defered.resolve();
        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    }

    // OBTENER INVENTARIO
    $scope.obtenerInventario = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        if ($scope.almacen) {
            almacenService.obtenerInventarioValorizado({
                idAlmacen: $scope.almacen.Id,
                idsConceptosBasicos: $scope.idsConceptosSeleccionados,
                idsValoresDeCaracteristicas: $scope.idsCaracteristicasSeleccionadas,
                conLote : $scope.conLote
            }).success(function (data) {
                $scope.inventario.Lista = data.cadena;
                $scope.itemTotal = data.totales
                defered.resolve();
            }).error(function (data) {
                $scope.messageError(data.error);
                defered.reject(data);
            });
        }
        return promise;
    }

    $scope.resolverIdsConceptosSeleccionados = function () {
        if ($scope.conceptos.length > 0) {
            $scope.idsConceptosSeleccionados = [];
            for (var i = 0; i < $scope.conceptos.length; i++) {
                $scope.idsConceptosSeleccionados.push($scope.conceptos[i].Id);
            }
        } else {
            $scope.idsConceptosSeleccionados = [];

        }
    }

    $scope.resolverIdsCaracteristicasSeleccionadas = function () {
        if ($scope.caracteristicasSeleccionadas.length > 0) {
            $scope.idsCaracteristicasSeleccionadas = [];
            for (var i = 0; i < $scope.caracteristicasSeleccionadas.length; i++) {
                if ($scope.caracteristicasSeleccionadas[i] != null) {
                    for (var j = 0; j < $scope.caracteristicasSeleccionadas[i].length; j++) {
                        $scope.idsCaracteristicasSeleccionadas.push($scope.caracteristicasSeleccionadas[i][j].Id);
                    }
                }
            }
        } else {
            $scope.idsCaracteristicasSeleccionadas = [];
        }
    }

    $scope.nombreCaracteristicasSeleccionadas = function (item) {
        $scope.listNombreCaracteristicasSeleccionadas.push(item);
    }

    //MANEJO DE DATATABLE
    $scope.crearColumnsDataTable = function () {
        var json = JSON.parse($scope.inventario.Lista[0]);
        var columns = [];
        for (var key in json) {
            var columnItem = new Object();
            columnItem.title = key;
            columns.push(columnItem);
        }
        return columns;
    }

    $scope.crearDataDataTable = function () {
        const lista = []
        for (var i = 0; i < $scope.inventario.Lista.length; i++) {
            var json = JSON.parse($scope.inventario.Lista[i]);
            var itemArray = [];
            for (var key in json) {
                var value = json[key];
                itemArray.push(value);
            }
            lista.push(itemArray);
        }
        return lista;
    }

    $scope.crearDataTable = function (data, columns) {
        $('#reporte-inventario-valorizado').DataTable({
            "data": data,
            "columns": columns,
            "destroy": true,
            "buttons": [
                'copy', 'csv', 'excel', 'pdf'
            ],
            "scrollX": true,
            "autoWidth": true,
            "language": {
                "sProcessing": "Procesando...",
                "sLengthMenu": "Mostrar _MENU_ registros",
                "sZeroRecords": "No se encontraron resultados",
                "sEmptyTable": "Ningún dato disponible en esta tabla =(",
                "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
                "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
                "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
                "sInfoPostFix": "",
                "sSearch": "Buscar:",
                "sUrl": "",
                "sInfoThousands": ",",
                "sLoadingRecords": "Cargando...",
                "oPaginate": {
                    "sFirst": "Primero",
                    "sLast": "Último",
                    "sNext": "Siguiente",
                    "sPrevious": "Anterior"
                },
                "oAria": {
                    "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                    "sSortDescending": ": Activar para ordenar la columna de manera descendente"
                },
                "buttons": {
                    "copy": "Copiar",
                    "colvis": "Visibilidad"
                }
            }
        });
    }

    $scope.mostrarDataTable = function () {
        $scope.obtenerInventario().then(function (resultado_) {
            if ($scope.inventario.Lista.length > 0) {
                $scope.crearDataTable($scope.crearDataDataTable(), $scope.crearColumnsDataTable());
                $('#reporte-inventario-valorizado tr:contains("PRODUCTO")').addClass('col-md-3');

            }
        }, function (error) {
            $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
        });
    }

    //LIMPIAR DATATABLE
    $scope.limpiarDataTable = function () {
        $timeout(function () {
            $('#reporte-inventario-valorizado').DataTable().clear().draw();
        }, 100);
    }

    $scope.limpiarFiltros = function () {
        $scope.conceptos = [];
        $scope.establecimiento = {};
        $scope.conLote = false;
        $scope.centroAtencion = {};
        $scope.caracteristicasSeleccionadas = [];
        $scope.idsCaracteristicasSeleccionadas = [];
        $scope.idsConceptosSeleccionados = [];
        $scope.nombreCaracteristicasSeleccionadas = [];

        if ($scope.inventario.Lista.length > 0) {
            $scope.limpiarDataTable();
        }
        $timeout(function () {
            $('.caracteristica').trigger("change");
        }, 100);

        $timeout(function () {
            $('.concepto-basico').trigger("change");
        }, 100);
    }
    $scope.focus = function () {
        $('.select2').select2({}).focus();
        $('#concepto-basico').select2({}).focus();
    }
    $scope.focus();
});
 
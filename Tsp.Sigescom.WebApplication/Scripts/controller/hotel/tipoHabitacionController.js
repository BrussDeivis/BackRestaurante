app.controller('tipoHabitacionController', function ($scope, $q, $rootScope, $timeout, $http, Upload, hotelService, centroDeAtencionService, precioService, conceptoService, maestroService, SweetAlert) {

    $scope.inicializar = function () {
        $scope.cargarParametros();
        $scope.limpiarRegistro();
        $scope.cargarColeccionesAsync();
        $scope.establecerDatosPorDefecto();
        $scope.inicializarComponentes();
    }

    $scope.cargarParametros = function () {
        $scope.idFamiliaHabitacion = idFamiliaHabitacion;
        $scope.idCaracteristicaAforoAdultos = idCaracteristicaAforoAdultos;
        $scope.idCaracteristicaAforoNinos = idCaracteristicaAforoNinos;
        $scope.numeroDecimalesEnPrecio = numeroDecimalesEnPrecio;
        $scope.tamanioMaximoFotoTipoHabitacion = tamanioMaximoFotoTipoHabitacion;
        $scope.maximaCantidadFotoTipoHabitacion = maximaCantidadFotoTipoHabitacion;
    }

    $scope.cargarColeccionesAsync = function () {
        $scope.obtenerCaracteristicas();
    }

    $scope.obtenerEstablecimientosComerciales = () => {
        centroDeAtencionService.obtenerEstablecimientosComerciales({}).success(function (data) {
            $scope.establecimientos = data;
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    };

    $scope.limpiarRegistro = function () {
        $scope.tipoHabitacion = { Fotos: [], Caracteristicas: [] };
        $scope.precio = {};
        $scope.caracteristicasSeleccionadas = [];
    }

    $scope.inicializarComponentes = function () {
        $scope.inicializacionRealizada = true;
    }

    $scope.establecerDatosPorDefecto = function () {
        $scope.obtenerTipoHabitaciones();
    }

    $rootScope.dtOptions.withLightColumnFilter({
        '0': { type: 'text', className: 'form-control padding-left-right-3' },
        '1': { type: 'text', className: 'form-control padding-left-right-3' },
        '2': { type: 'text', className: 'form-control padding-left-right-3' },
        '3': { type: 'text', className: 'form-control padding-left-right-3' },
    });

    $scope.obtenerTipoHabitaciones = function () {
        hotelService.obtenerTipoHabitacionesBandeja({}).success(function (data) {
            $scope.tipoHabitaciones = data;
        }).error(function (data) {
            SweetAlert.error2(data);
        });
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

    $scope.noHayPrecioIngresado = function () {
        let numeroPreciosIngresados = 0;
        if ($scope.precio.PuntosDePrecio != undefined) {
            for (var i = 0; i < $scope.precio.PuntosDePrecio.length; i++) {
                for (var j = 0; j < $scope.precio.PuntosDePrecio[i].Tarifas.length; j++) {
                    if ($scope.precio.PuntosDePrecio[i].Tarifas[j].EsIngresado)
                        numeroPreciosIngresados++;
                }
            }
        }
        return numeroPreciosIngresados == 0;
    }

    $scope.hayInconsistenciasPrecioIngresado = function () {
        let numeroInconsistencias = 0;
        if ($scope.precio.PuntosDePrecio != undefined) {
            for (var i = 0; i < $scope.precio.PuntosDePrecio.length; i++) {
                for (var j = 0; j < $scope.precio.PuntosDePrecio[i].Tarifas.length; j++) {
                    if ($scope.precio.PuntosDePrecio[i].Tarifas[j].EsIngresado) {
                        if ($scope.precio.PuntosDePrecio[i].Tarifas[j].Valor == 0)
                            numeroInconsistencias++;
                        if ($scope.diferenciaDiasEntreFechas($scope.precio.PuntosDePrecio[i].Tarifas[j].FechaDesde, $scope.precio.PuntosDePrecio[i].Tarifas[j].FechaHasta) <= 0)
                            numeroInconsistencias++;
                    }

                }
            }
        }
        return numeroInconsistencias != 0;
    }

    $scope.diferenciaDiasEntreFechas = function (fechaInicio, fechaFin) {
        var dt1 = fechaInicio.split('/'),
            dt2 = fechaFin.split('/'),
            one = new Date(dt1[2], dt1[1] - 1, dt1[0]),
            two = new Date(dt2[2], dt2[1] - 1, dt2[0]);
        var millisecondsPerDay = 1000 * 60 * 60 * 24;
        var millisBetween = two.getTime() - one.getTime();
        var days = millisBetween / millisecondsPerDay;
        return Math.floor(days);
    }

    $scope.obtenerCaracteristicas = function () {
        maestroService.obtenerCaracteristicasParaConcepto({ idConcepto: $scope.idFamiliaHabitacion }).success(function (data) {
            $scope.caracteristicas = data;
            $scope.caracteristicas.forEach(c => c.Nombre = c.Nombre.toLowerCase());
            $scope.aforoAdultos = $scope.caracteristicas.find(c => c.Id === $scope.idCaracteristicaAforoAdultos);
            $scope.caracteristicas.splice($scope.caracteristicas.findIndex(c => c.Id === $scope.idCaracteristicaAforoAdultos), 1);
            $scope.aforoNinos = $scope.caracteristicas.find(c => c.Id === $scope.idCaracteristicaAforoNinos);
            $scope.caracteristicas.splice(parseInt($scope.caracteristicas.findIndex(c => c.Id === $scope.idCaracteristicaAforoNinos)), 1);
            $scope.caracteristicasConcepto = angular.copy($scope.caracteristicas);
            $timeout(function () { $('.caracteristica').select2({ language: "es" }); }, 100);
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.editarTipoHabitacion = function (id) {
        $scope.limpiarRegistro();
        $scope.obtenerTipoHabitacion(id);
    }

    $scope.obtenerTipoHabitacion = function (id) {
        hotelService.obtenerTipoHabitacion({ id: id }).success(function (data) {
            $scope.tipoHabitacion = data.tipoHabitacion;
            $scope.tipoHabitacion.IdsValoresCaracteristicas = null;
            $scope.caracteristicasSeleccionadas = [];
            $scope.tipoHabitacion.FotosEliminadas = [];
            for (var i = 0; i < $scope.tipoHabitacion.Caracteristicas.length; i++) {
                $scope.caracteristicasSeleccionadas.push({ Id: $scope.tipoHabitacion.Caracteristicas[i].Id, Nombre: $scope.tipoHabitacion.Caracteristicas[i].Nombre })
            }
            for (var i = 0; i < $scope.tipoHabitacion.Fotos.length; i++) {
                $scope.tipoHabitacion.Fotos[i].Foto = "data:image/png;base64," + $scope.tipoHabitacion.Fotos[i].Foto;
                $timeout(function () { $('#file' + i).trigger("change"); }, 100);
            }
            $timeout(function () { $('.caracteristica').select2({ language: "es" }); }, 100);
            $scope.precio = data.precios;
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
            $('#modal-nuevo-tipo-habitacion').modal('show');
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.cambiarEstadoTipoHabitacion = (id) => {
        hotelService.cambiarEsVigenteTipoHabitacion({ id: id }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.obtenerTipoHabitaciones();
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.processFile = function (files) {
        for (var i = 0; i < files.length; i++) {
            $scope.getBase64(files[i]).then(
                data => ($scope.tipoHabitacion.Fotos.length < $scope.maximaCantidadFotoTipoHabitacion) ? $scope.tipoHabitacion.Fotos.push({ Foto: data }) : ''
            );
            $timeout(function () { $('#file' + i).trigger("change"); }, 100);
        }
    }

    $scope.getBase64 = function (file) {
        return new Promise((resolve, reject) => {
            const reader = new FileReader();
            reader.readAsDataURL(file);
            reader.onload = () => resolve(reader.result);
            reader.onerror = error => reject(error);
        });
    }

    $scope.guardarTipoHabitacion = function () {
        $scope.tipoHabitacion.Caracteristicas = [];
        $scope.caracteristicasSeleccionadas.forEach(vc => $scope.tipoHabitacion.Caracteristicas.push(vc));
        hotelService.guardarTipoHabitacion({ tipoHabitacion: $scope.tipoHabitacion, precios: $scope.precio }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);
            $scope.limpiarRegistro();
            $scope.obtenerTipoHabitaciones();
            $('#modal-nuevo-tipo-habitacion').modal('hide');
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.abrirArchivo = function () {
        let archivo = document.getElementById('fileFoto');
        archivo.click();
    }

    $scope.eliminarFoto = function (file) {
        if (file.Nombre != undefined) {
            $scope.tipoHabitacion.FotosEliminadas.push(file.Nombre);
        }
        const pos = $scope.tipoHabitacion.Fotos.indexOf(file);
        $scope.tipoHabitacion.Fotos.splice(pos, 1);
    }

    $scope.abrirRegistroTipoHabitacion = function () {
        $scope.limpiarRegistro();
        $scope.cargarPreciosPorDefecto();
        $('#modal-nuevo-tipo-habitacion').modal('show');
    }
    $scope.cerrarRegistroTipoHabitacion = function () {
        $scope.limpiarRegistro();
        $('#modal-nuevo-tipo-habitacion').modal('hide');
    }
});

angular.
    module('app').
    component('registradorConceptoServicio', {
        templateUrl: "../Scripts/controller/concepto/registradorConceptoServicio/registradorConceptoServicio.html",
        bindings: {
            api: '=',//Api para ejecutar metodos desde fuera el componente
            conceptoServicio: '=',//Concepto comercial de servicio que se devolvera para quien use el componente
            changed: '&'//Funcion de cambio que se ejecutara al crear concepto de servicio
        },

        controller: function ($q, $scope, $timeout, $compile, SweetAlert, conceptoService, maestroService, ventaService) {
            $('select:not(.normal)').each(function () {
                $(this).select2({
                    dropdownParent: $(this).parent()
                });
            });
            var ctrl = this;

            ctrl.conceptoIngresado = function () {
                ctrl.changed({ conceptoServicio: ctrl.conceptoServicio });
                ctrl.conceptoServicio = {};
            };

            ctrl.inicializar = function () {
                ctrl.conceptoServicio = {};
            };

            ctrl.nuevoRegistroConceptoServicio = function () {
                $('#modal-registro-concepto-servicio').modal('show');
                ctrl.obtenerConceptosBasicosServicio().then(function (resultado_) {
                    ctrl.conceptoServicio = { ConceptoBasicoSeleccionado: true, Valor: "0", Sufijo: "", ConceptoBasico: ctrl.listaConceptosBasicosServicio[0] };
                    $timeout(function () { $('#selectorConceptoBasico').trigger("change"); }, 100);
                }, function (error) {
                    ctrl.mensaje = "Se ha producido un error al obtener el dato:" + error;
                });
            }

            ctrl.obtenerConceptosBasicosServicio = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                maestroService.obtenerConceptosBasicosServicio({}).success(function (data) {
                    ctrl.listaConceptosBasicosServicio = data;
                    defered.resolve();
                }).error(function (data) {
                    SweetAlert.error2(data);
                    defered.reject(data);
                });
                return promise;
            }

            ctrl.ingresarConceptoBasicoServicio = function () {
                ctrl.conceptoServicio.ConceptoBasicoSeleccionado = false;
            }

            ctrl.seleccionarConceptoBasicoServicio = function () {
                ctrl.conceptoServicio.ConceptoBasicoSeleccionado = true;
            }

            ctrl.actualizarNombreConceptoServicio = function () {
                ctrl.conceptoServicio.NombreCompleto = ((ctrl.conceptoServicio.ConceptoBasicoSeleccionado == true) ? ctrl.conceptoServicio.ConceptoBasico.Nombre : (ctrl.conceptoServicio.NombreConceptoBasico == undefined ? "" : ctrl.conceptoServicio.NombreConceptoBasico)) + " " + (ctrl.conceptoServicio.Sufijo == undefined ? "" : ctrl.conceptoServicio.Sufijo);
            }

            ctrl.cerrar = function () {
                ctrl.conceptoServicio = {};
                $('#modal-registro-concepto-servicio').modal('hide');
            }

            ctrl.guardar = function () {
                conceptoService.guardarConceptoServicio({ conceptoServicio: ctrl.conceptoServicio }).success(function (data) {
                    ctrl.obtenerConceptoServicio(data);
                    SweetAlert.success("Correcto", data.result_description);
                    $('#modal-registro-concepto-servicio').modal('hide');
                }).error(function (data) {
                    SweetAlert.error("Ocurrio un Problema", data.error);
                });
            }

            ctrl.obtenerConceptoServicio = function (conceptoServicioNuevo) {
                conceptoService.obtenerConceptoDeNegocioComercialPorIdConcepto({ idConceptoNegocio: conceptoServicioNuevo.data, complementoStock: false, complementoPrecio: true }).success(function (data) {
                    ctrl.conceptoServicio = {
                        Familia: { Id: conceptoServicioNuevo.basico_data, Nombre: ctrl.conceptoServicio.NombreConceptoBasico },
                        Concepto: { Id: conceptoServicioNuevo.data, Nombre: ctrl.conceptoServicio.NombreCompleto },
                        ConceptoBasicoSeleccionado: ctrl.conceptoServicio.ConceptoBasicoSeleccionado,
                        ConceptoComercial : data
                    };
                    ctrl.conceptoIngresado();
                }).error(function (data) {
                    SweetAlert.error("Ocurrio un Problema", data.error);
                });
            }

            this.$onInit = function () {
                ctrl.api = {};
                ctrl.inicializar();
                ctrl.api.NuevoRegistroConceptoServicio = ctrl.nuevoRegistroConceptoServicio;
            };
        }
    });
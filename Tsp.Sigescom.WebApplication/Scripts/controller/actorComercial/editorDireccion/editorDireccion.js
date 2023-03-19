angular.
    module('app').
    component('editorDireccion', {
        templateUrl: "../Scripts/controller/actorComercial/editorDireccion/editorDireccion.html",
        bindings: {
            direccion: '=',
            //changed:'&'
        },

        controller: function ($q, $scope, $timeout, $element, SweetAlert, actorComercialService, maestroService) {

            var ctrl = this;

            //ctrl.fireEventChanged = function () {
            //    ctrl.changed();
            //};

            ctrl.inicializar = function () {
                ctrl.cargarParametros();
                ctrl.cargarColeccionesAsync();
                ctrl.cargarColeccionesSync().then(function (resultado_) {
                    ctrl.establecerDatosPorDefecto();
                }, function (error) {
                    $scope.mensaje = "Se ha producido un error al obtener el dato:" + error;
                });
                ctrl.estaRegistrandoActorComercial = false;
            };
        
            ctrl.cargarParametros = function () {
                ctrl.idUbigeoSeleccionadoPorDefecto = idUbigeoSeleccionadoPorDefecto;
                ctrl.idUbigeoNoEspecificado = idUbigeoNoEspecificado;
            };

            ctrl.cargarColeccionesAsync = function () {

            };

            ctrl.cargarColeccionesSync = function () {
                var defered = $q.defer();
                var promiseList = [];
                promiseList.push(ctrl.listarUbigeoDistrito());
                return $q.all(promiseList).then(function (response) {
                    defered.resolve();
                }).catch(function (error) {
                    defered.reject(e);
                });
            };

            ctrl.establecerDatosPorDefecto = function () {
                ctrl.establecerUbigeoPorDefecto();
            };

            ctrl.obtenerUbigeoDistrito = function () {
                var defered = $q.defer();
                var promise = defered.promise;
                maestroService.listarUbigeoDistrito().success(function (data) {
                    ctrl.ubigeosPeru = data;
                    defered.resolve();
                }).error(function (data) {
                    ctrl.messageError(data.error);
                    defered.reject(data);
                });
                return promise;
            };

            ctrl.establecerUbigeoPorDefecto = function () {
                var ubigeo = Enumerable.from(ctrl.ubigeosPeru)
                    .where("$.Id === '" + ctrl.idUbigeoSeleccionadoPorDefecto + "'").toArray()[0];
                ctrl.direccion.Ubigeo = ubigeo !== null ? ubigeo : ctrl.ubigeosPeru[0];
                $timeout(function () { $('.ubigeo').trigger("change"); }, 100);
            };

            ctrl.verificarUbigeoSeleccionado = function () {
                if (ctrl.direccion.Ubigeo.Id === ctrl.idUbigeoNoEspecificado) {
                    ctrl.ubigeoSeleccionadoNoEspecificado = true;
                } else {
                    ctrl.ubigeoSeleccionadoNoEspecificado = false;
                }
            };

            this.$onInit = function () {
                ctrl.inicializar();
            };
        }
    });
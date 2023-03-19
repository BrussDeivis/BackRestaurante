app.factory('controlVisitasService', function ($http) {
    return {
        obtenerDatosDniYGuardar: function (params) {
            return $http.post(URL + "/ControlVisitas/ObtenerDatosDniYGuardar", params);
        },
        obtenerDatosPorFecha: function (params) {
            return $http.post(URL + "/ControlVisitas/ConsultarListaPorFecha", params);
        },
        obtenerDatosPorDni: function (params) {
            return $http.post(URL + "/ControlVisitas/ConsultarListaPorDni", params);
        },
        obtenerDatosRucYGuardar: function (params) {
            return $http.post(URL + "/ControlVisitas/ObtenerDatosRucYGuardar", params);
        },
        obtenerListaRuc: function (params) {
            return $http.post(URL + "/ControlVisitas/ConsultarListaRuc", params);
        },
    };
});
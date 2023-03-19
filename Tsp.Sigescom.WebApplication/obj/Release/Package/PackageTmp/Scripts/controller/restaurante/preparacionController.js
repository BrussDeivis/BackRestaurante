app.controller('preparacionController', function ($scope, $q, $rootScope, $compile, SweetAlert, restauranteService) {


    $scope.parametrosDeAtencion = ParametrosDeConfiguracionAtencion;
    $scope.ParametrosDeOrden = ParametrosDeConfiguracion;
    $scope.ParametrosDeDetallesDeOrden = $scope.ParametrosDeOrden.ConfiguracionDetallesDeOrden;
    $scope.ParametroVista = { EstadoServido: 1, EstadoAtendido: 2, EstadoAnulado: 3 };

    $scope.OrdenesSeleccionadas = [];
    $scope.EstadoAnteriorOrdenesSeleccionadas = [];
    $scope._EstadoDeCambioDeEstado = false;
    $scope._ActualizarConCategoria = false;

    $scope.ordenes = [];
    $scope.ambientes = [];
    $scope.categorias = [];
    $scope.idAmbienteSeleccionado = 0;
    $scope.idCategoriaSeleccionada = 0;
    $scope.idsItemsDeCategoria = [];

    $scope.categoriasPadre = [];
    $scope.subCategoriasSeleccionado = [];
    $scope.detalleDeOrdenesSelecionados = [];
    $scope.ordenesAtendidas = [];




    $scope.iniciarDatos = function () {
        $scope.obtenerOrdenesConfirmadas();
        $scope.obtenerAmbientes();
        $scope.obtenerCategorias();
    }

    $scope.obtenerCategorias = function () {
        var defered = $q.defer();
        var promise = defered.promise;
        restauranteService.obtenerCategorias({}).success(function (data) {
            $scope.categorias = data;
            defered.resolve();

        }).error(function (data) {
            $scope.messageError(data.error);
            defered.reject(data);
        });
        return promise;
    };

    $scope.obtenerAmbientes = function () {
        restauranteService.obtenerTodosLosAmbientes({}).success(function (data) {
            $scope.ambientes = data;
            if ($scope.ambientes.length == 1) {
                $scope.idAmbienteSeleccionado = $scope.ambientes[0].Id;
            } else if ($scope.ambientes.length == 0) {
                $scope.idAmbienteSeleccionado = -1;
            }
        }).error(function (data) {
            $scope.messageError(data.error);
        })
    }

    $scope.cambioDeAmbienteSeleccionado = function (idAmbiente) {
        $scope.idAmbienteSeleccionado = idAmbiente;
        $scope.actualizarOrdenes();
    }

    $scope.cambioDeCategoriaSeleccionada = function (idCategoria, categoriasHijo) {
        //for (var i = 0; i < $scope.categoriasPadre.length; i++) {
        //    if (categoria.Id === $scope.categoriasPadre[i].Id) {
        //        $scope.categoriasPadre[i].subCategoria = !$scope.categoriasPadre[i].subCategoria;
        //    }
        //    else {
        //        $scope.categoriasPadre[i].subCategoria = false;
        //    }
        //}

        $scope.idCategoriaSeleccionada = idCategoria;
        $scope.subCategoriasSeleccionado = categoriasHijo;

        $scope.actualizarOrdenes();
    }

    $scope.obtenerOrdenesConfirmadas = function () {
        $scope.ordenes = [];
        $scope.ordenesAtendidas = [];
        restauranteService.obtenerOrdenesConfirmadas({}).success(data => {
            $scope.ordenes = data;

            if ($scope._ActualizarConCategoria) {
                var ordenesValidas = [];
                for (var i = 0; i < $scope.ordenes.length; i++) {
                    var orden = $scope.ordenes[i];
                    var DetallesValidos = [];
                    for (var j = 0; j < orden.DetallesDeOrden.length; j++) {
                        var detalle = orden.DetallesDeOrden[j];
                        if ($scope.idsItemsDeCategoria.includes(detalle.IdItem)) {
                            DetallesValidos.push(detalle);
                        }
                    }
                    orden.DetallesDeOrden = DetallesValidos;
                    if (orden.DetallesDeOrden.length > 0) {
                        ordenesValidas.push(orden);
                    }
                }
                $scope.ordenes = ordenesValidas;
                $scope._ActualizarConCategoria = false;
            }

            for (var i = 0; i < $scope.ordenes.length; i++) {
                $scope.ordenes[i].Estado = $scope.ParametrosDeOrden.EstadoConfirmado;
                for (var j = 0; j < $scope.ordenes[i].DetallesDeOrden.length; j++) {
                    $scope.ordenes[i].DetallesDeOrden[j].DetalleItemRestaurante = JSON.parse($scope.ordenes[i].DetallesDeOrden[j].DetalleItemRestauranteJson);
                }
            }
            $scope.verificarEstadoDeDetalleDeOrden();
            $scope.ObtenerOrdenesAtendidas();
        }).error(error =>
            $scope.messageError(error)
        );
    }

    $scope.obtenerOrdenesPorEstadoYAmbiente = function (numEstado, idAmbiente) {
        $scope.ordenesAtendidas = [];
        restauranteService.obtenerOrdenesPorEstadoDesdeUnAmbiente({ NumEstado: numEstado, IdAmbiente: idAmbiente }).success(data => {
            $scope.ordenes = data;
            if ($scope._ActualizarConCategoria) {
                var ordenesValidas = [];
                for (var i = 0; i < $scope.ordenes.length; i++) {
                    var orden = $scope.ordenes[i];
                    var DetallesValidos = [];
                    for (var j = 0; j < orden.DetallesDeOrden.length; j++) {
                        var detalle = orden.DetallesDeOrden[j];
                        if ($scope.idsItemsDeCategoria.includes(detalle.IdItem)) {
                            DetallesValidos.push(detalle);
                        }
                    }
                    orden.DetallesDeOrden = DetallesValidos;
                    if (orden.DetallesDeOrden.length > 0) {
                        ordenesValidas.push(orden);
                    }
                }
                $scope.ordenes = ordenesValidas;
                $scope._ActualizarConCategoria = false;
            }


            for (var i = 0; i < $scope.ordenes.length; i++) {
                $scope.ordenes[i].Estado = $scope.ParametrosDeOrden.EstadoConfirmado;
                for (var j = 0; j < $scope.ordenes[i].DetallesDeOrden.length; j++) {
                    $scope.ordenes[i].DetallesDeOrden[j].DetalleItemRestaurante = JSON.parse($scope.ordenes[i].DetallesDeOrden[j].DetalleItemRestauranteJson);
                }
            }
            $scope.verificarEstadoDeDetalleDeOrden();
            $scope.ObtenerOrdenesAtendidas();
        }).error(error =>
            $scope.messageError(error)
        );
    }
    $scope.ObtenerOrdenesAtendidas = () => {
        let atendido = (detalleOrden) => {
            let bandera = false;
            if ((detalleOrden.Estado === $scope.ParametrosDeDetallesDeOrden.EstadoAtendido) || (detalleOrden.Estado === $scope.ParametrosDeDetallesDeOrden.EstadoDevuelto) || (detalleOrden.Estado === $scope.ParametrosDeDetallesDeOrden.EstadoAnulado)) {
                bandera = true;
            }
            return bandera;
        }
        for (let i = 0; i < $scope.ordenes.length; i++) {
            let bandera = $scope.ordenes[i].DetallesDeOrden.every(atendido);
            if (bandera) {
                $scope.ordenesAtendidas.push($scope.ordenes[i]);
            }

        }
        for (var i = 0; i < $scope.ordenesAtendidas.length; i++) {
            const pos = $scope.ordenes.indexOf($scope.ordenesAtendidas[i]);
            $scope.ordenes.splice(pos, 1);
        }
    }

    $scope.obtenerEstiloFila = function (idEstado) {
        var Estilo = {};

        switch (idEstado) {
            case -1: {
                Estilo = { 'border': '5px dashed yellow' }
                break;
            }
            case $scope.ParametrosDeDetallesDeOrden.EstadoPreparando: {
                Estilo = { 'background-color': 'rgba(255, 165, 0, 0.3)', 'color': 'black' };
                break;
            }
            case $scope.ParametrosDeDetallesDeOrden.EstadoServido: {
                Estilo = { 'background-color': 'cyan', 'color': 'black' }
                break;
            }
            case $scope.ParametrosDeDetallesDeOrden.EstadoAnulado: {
                Estilo = { 'background-color': '#e4e4e4', 'text-decoration': 'line-through', 'color': 'red' }
                break;
            }
            case $scope.ParametrosDeDetallesDeOrden.EstadoAtendido: {
                Estilo = { 'background-color': 'rgba(0, 255, 0, 0.3)', 'color': 'black' }
                break;
            }
            case $scope.ParametrosDeDetallesDeOrden.EstadoDevuelto: {
                Estilo = { 'background-color': '#e4e4e4', 'text-decoration': 'line-through', 'color': 'orange' }
                break;
            }
            case $scope.ParametrosDeDetallesDeOrden.EstadoObservado: {
                Estilo = { 'background-color': 'rgba(255, 0, 0, 0.3)', 'color': 'black' }
                break;
            }

        }
        return Estilo;
    }
    $scope.deshabilitarBotonOrden = function (idEstado) {
        var bandera = false;

        switch (idEstado) {
            case -1: {
                bandera = false;
                break;
            }
            case $scope.ParametrosDeDetallesDeOrden.EstadoPreparando: {

                bandera = false;
                break;
            }
            case $scope.ParametrosDeDetallesDeOrden.EstadoServido: {
                bandera = true;
                break;
            }
            case $scope.ParametrosDeDetallesDeOrden.EstadoAnulado: {
                bandera = true;
                break;
            }
            case $scope.ParametrosDeDetallesDeOrden.EstadoAtendido: {
                bandera = true;
                break;
            }
            case $scope.ParametrosDeDetallesDeOrden.EstadoDevuelto: {
                bandera = true;
                break;
            }

        }
        return bandera;
    }
    $scope.obtenerClasesParaDiferentesTiposDeOrdenes = function (Estado) {
        var clase = ['btn'];
        switch (Estado) {

            case $scope.ParametroVista.EstadoServido: {
                clase.push('btn-warning');
                break;
            }
            case $scope.ParametroVista.EstadoAtendido: {
                clase.push('btn-success');
                break;
            }
            case $scope.ParametroVista.EstadoAnulado: {
                clase.push('btn-danger');
                break;
            }
            default: {
                clase.push('btn-primary');
                break;
            }
        }
        return clase;
    }
    $scope.deshabilitarBotonPreparar = function () {
        let bandera = true;
        if ($scope.detalleDeOrdenesSelecionados.length > 0) {
            if ($scope.detalleDeOrdenesSelecionados[0].Estado === $scope.ParametrosDeDetallesDeOrden.EstadoRegistrado || $scope.detalleDeOrdenesSelecionados[0].Estado === $scope.ParametrosDeDetallesDeOrden.EstadoObservado) {
                bandera = false;
            }
        }
        return bandera;
    }
    $scope.deshabilitarBotonServir = function () {
        let bandera = true;
        if ($scope.detalleDeOrdenesSelecionados.length > 0) {
            if ($scope.detalleDeOrdenesSelecionados[0].Estado === $scope.ParametrosDeDetallesDeOrden.EstadoPreparando) {
                bandera = false;
            }
        }
        return bandera;
    }
    $scope.detalleDeOrdenSeleccionado = function (detalleOrden, indexCartilla, indexDetalleOrden) {
        let id = `checkbox-detalleOrden${indexCartilla}${indexDetalleOrden}`;
        let checkbox = document.getElementById(id);
        let detalleOrdenAnterior = { Estado: -3 }
        detalleOrdenAnterior = $scope.detalleDeOrdenesSelecionados[0];
        if ($scope.detalleDeOrdenesSelecionados.length === 0) {
            checkbox.checked = !checkbox.checked;
            checkbox.parentNode.classList.toggle("boton-detalle-orden-seleccionado");
            $scope.detalleDeOrdenesSelecionados.push(detalleOrden);
            document.getElementById('boton-preparar').disabled = $scope.deshabilitarBotonPreparar();
        } else {
            if (detalleOrdenAnterior.Estado === $scope.ParametrosDeDetallesDeOrden.EstadoObservado || detalleOrdenAnterior.Estado === $scope.ParametrosDeDetallesDeOrden.EstadoRegistrado) {
                if (detalleOrden.Estado === $scope.ParametrosDeDetallesDeOrden.EstadoObservado || detalleOrden.Estado === $scope.ParametrosDeDetallesDeOrden.EstadoRegistrado) {
                    checkbox.checked = !checkbox.checked;
                    checkbox.parentNode.classList.toggle("boton-detalle-orden-seleccionado");
                    if (checkbox.checked === false) {
                        const pos = $scope.detalleDeOrdenesSelecionados.indexOf(detalleOrden);
                        $scope.detalleDeOrdenesSelecionados.splice(pos, 1);
                    } else {
                        $scope.detalleDeOrdenesSelecionados.push(detalleOrden);
                    }
                    document.getElementById('boton-preparar').disabled = $scope.deshabilitarBotonPreparar();
                }
            }
            if (detalleOrdenAnterior.Estado === $scope.ParametrosDeDetallesDeOrden.EstadoPreparando) {
                if (detalleOrden.Estado === $scope.ParametrosDeDetallesDeOrden.EstadoPreparando) {
                    checkbox.checked = !checkbox.checked;
                    checkbox.parentNode.classList.toggle("boton-detalle-orden-seleccionado");
                    if (checkbox.checked === false) {
                        const pos = $scope.detalleDeOrdenesSelecionados.indexOf(detalleOrden);
                        $scope.detalleDeOrdenesSelecionados.splice(pos, 1);
                    } else {
                        $scope.detalleDeOrdenesSelecionados.push(detalleOrden);

                    }
                    document.getElementById('boton-preparar').disabled = $scope.deshabilitarBotonPreparar();
                }
            }
        }
    }
    $scope.prepararDetallesDeOrdenes = function () {
        let idDetalleDeOrdenSeleccionados = [];
        $scope.detalleDeOrdenesSelecionados.forEach(detO => {
            idDetalleDeOrdenSeleccionados.push(detO.Id);
        });
        restauranteService.prepararDetallesDeOrdenes({ idsDetallesDeOrdenes: idDetalleDeOrdenSeleccionados }).success(function (data) {
            document.getElementById('boton-preparar').disabled = true;
            SweetAlert.success("Correcto", data.result_description);
            $scope.obtenerOrdenesConfirmadas();
        }).error(function (data) {
            SweetAlert.error("Error", data.error);

        });
        $scope.detalleDeOrdenesSelecionados = [];
    }

    $scope.servirDetallesDeOrdenes = function () {
        let idDetalleDeOrdenSeleccionados = [];
        $scope.detalleDeOrdenesSelecionados.forEach(detO => {
            idDetalleDeOrdenSeleccionados.push(detO.Id);
        });
        restauranteService.servirDetallesDeOrdenes({ idsDetallesDeOrdenes: idDetalleDeOrdenSeleccionados }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description);

            $scope.obtenerOrdenesConfirmadas();


        }).error(function (data) {
            SweetAlert.error("Error", data.error);

        });
        $scope.detalleDeOrdenesSelecionados = [];
    }
    $scope.verificarEstadoDeDetalleDeOrden = function () {

        for (var i = 0; i < $scope.ordenes.length; i++) {
            var EstadoServido = true;
            var EstadoAtendido = true;
            var EstadoAnulado = true;

            for (var j = 0; j < $scope.ordenes[i].DetallesDeOrden.length; j++) {
                if ($scope.ordenes[i].DetallesDeOrden[j].Estado != $scope.ParametrosDeDetallesDeOrden.EstadoServido) {
                    EstadoServido = false;
                }
                if ($scope.ordenes[i].DetallesDeOrden[j].Estado != $scope.ParametrosDeDetallesDeOrden.EstadoAtendido) {
                    EstadoAtendido = false;
                }
                if ($scope.ordenes[i].DetallesDeOrden[j].Estado != $scope.ParametrosDeDetallesDeOrden.EstadoAnulado || $scope.ordenes[i].DetallesDeOrden[j].Estado != $scope.ParametrosDeDetallesDeOrden.EstadoDevuelto) {
                    EstadoAnulado = false;
                }

            }

            if (!EstadoServido && !EstadoAtendido && !EstadoAnulado) {
                $scope.ordenes[i].Estado = $scope.ParametrosDeOrden.EstadoConfirmado;
                break;
            } else {
                if (EstadoServido) {
                    $scope.ordenes[i].Estado = $scope.ParametroVista.EstadoServido;
                }
                if (EstadoAtendido) {
                    $scope.ordenes[i].Estado = $scope.ParametroVista.EstadoAtendido;
                }
                if (EstadoAnulado) {
                    $scope.ordenes[i].Estado = $scope.ParametroVista.EstadoAnulado;
                }
            }
        }

    }

    $scope.cambiarEstadoDeDetalles = function (NumEstado) {
        var Ids = [];
        for (var i = 0; i < $scope.OrdenesSeleccionadas.length; i++) {
            Ids.push($scope.OrdenesSeleccionadas[i].Id);
        }

        restauranteService.cambiarEstadoDeDetallesDeOrden({ Ids: Ids, NumEstado: NumEstado }).success(data => {
            for (var i = 0; i < $scope.OrdenesSeleccionadas.length; i++) {
                $scope.OrdenesSeleccionadas[i].Estado = NumEstado;
            }
            $scope.OrdenesSeleccionadas = [];
            $scope.EstadoAnteriorOrdenesSeleccionadas = [];
            $scope._EstadoDeCambioDeEstado = false;
            $scope.verificarEstadoDeDetalleDeOrden();
        }).error(error => {
            $scope.messageError(error);
        })


    }
    $scope.cancelarCambioDeEstado = function () {
        for (var i = 0; i < $scope.OrdenesSeleccionadas.length; i++) {
            $scope.OrdenesSeleccionadas[i].Estado = $scope.EstadoAnteriorOrdenesSeleccionadas[i];
        }
        $scope.OrdenesSeleccionadas = [];
        $scope._EstadoDeCambioDeEstado = false;
    }

    $scope.seleccionarDetalleDeOrden = function (indexOrden, indexDetalle) {
        var detalle = $scope.ordenes[indexOrden].DetallesDeOrden[indexDetalle];
        if (detalle.Estado != -1 && detalle.Estado != $scope.ParametrosDeDetallesDeOrden.EstadoAtendido && detalle.Estado != $scope.ParametrosDeDetallesDeOrden.EstadoDevuelto) {
            $scope.EstadoAnteriorOrdenesSeleccionadas.push(detalle.Estado)
            detalle.Estado = -1;
            $scope._EstadoDeCambioDeEstado = true;
            $scope.OrdenesSeleccionadas.push(detalle);
        }

    }

    $scope.actualizarOrdenes = function () {
        if ($scope.idAmbienteSeleccionado > 0 && $scope.idCategoriaSeleccionada > 0) {
            restauranteService.ObtenerIdsDeitemsPorCategoria({ idCategoria: $scope.idCategoriaSeleccionada }).success(data => {
                $scope.idsItemsDeCategoria = data;
                $scope._ActualizarConCategoria = true;
                $scope.obtenerOrdenesPorEstadoYAmbiente($scope.ParametrosDeOrden.EstadoConfirmado, $scope.idAmbienteSeleccionado);
            }).error(error => $scope.messageError(error));
        } else if ($scope.idAmbienteSeleccionado > 0) {
            $scope.obtenerOrdenesPorEstadoYAmbiente($scope.ParametrosDeOrden.EstadoConfirmado, $scope.idAmbienteSeleccionado);
        } else if ($scope.idCategoriaSeleccionada > 0) {
            restauranteService.ObtenerIdsDeitemsPorCategoria({ idCategoria: $scope.idCategoriaSeleccionada }).success(data => {
                $scope.idsItemsDeCategoria = data;
                $scope._ActualizarConCategoria = true;
                $scope.obtenerOrdenesConfirmadas();
            }).error(error => $scope.messageError(error));
        } else {
            $scope.obtenerOrdenesConfirmadas();
        }
        $scope._EstadoDeCambioDeEstado = false;

    }
    $scope.iniciarCategorias = function () {
        $scope.obtenerCategorias().then(function () {
            var categoriaPadre = { categoriasHijo: [] };
            var categoriasHijo = [];
            //OBTENER CATEGORIAS PADRES
            if ($scope.categorias.length != 0) {

                for (var i = 0; i < $scope.categorias.length; i++) {

                    if ($scope.categorias[i].IdPadre == $scope.parametrosDeAtencion.IdCategoriaNula) { // ENVIAR CATEGORIA NULA DESDE PADRE Y CARGARLO COMO VARIABLE ESTATICA.

                        $scope.categoriasPadre.push($scope.categorias[i]);
                    }
                }
            }

            //PINTAR CATEGORIAS PADRE
            if ($scope.categoriasPadre.length > 0) {
                for (var i = 0; i < $scope.categoriasPadre.length; i++) {
                    for (var j = 0; j < $scope.categorias.length; j++) {
                        if ($scope.categoriasPadre[i].Id == $scope.categorias[j].IdPadre) {

                            categoriasHijo.push($scope.categorias[j]);
                        }
                    }
                    $scope.categoriasPadre[i].categoriasHijo = categoriasHijo;
                    categoriasHijo = [];
                }
            }

        });

    }


});





TomarOrdenAtencionController = function ($scope, $q, $timeout, $rootScope, $compile, SweetAlert, restauranteService) {

    $scope.agregarItemSeleccionado = function () {
        $scope.obtenerItemCompleto($scope.itemSeleccionado.Concepto.Id).success(data => {
            let item = data;
            item.Cantidad = $scope.itemSeleccionado.Cantidad;
            $scope.agregarItemAOrden(item);

            //for (var i = 0; i < $scope.itemSeleccionado.Cantidad; i++) {
            //    $scope.agregarItemAOrden(item);
            //}
            $scope._banderaRealizandoseOrden = true;
            $scope.itemSeleccionado = { Cantidad: 1 };
            $timeout(function () { $('#itemSeleccionado').trigger("change"); }, 100);
        }).error(data => {
            SweetAlert.error2(data);
        });
    }

    $scope.focusNextConcepto = function () {
        $timeout(function () { $('#cantidadItem').trigger("focus"); }, 100);
        $timeout(function () { $('#cantidadItem').trigger("select"); }, 100);
    }

    $scope.agregarItemAOrden = function (item) {
        item.DetalleItemRestaurante = { AnotacionIndicacion: "", AnotacionObservacion: "", DetallesComplemento: [] }
        $scope.ordenDeVista.ImporteOrden += item.Cantidad * item.Precio;
        var complementos = item.ComplementosDeFamilia;
        for (var i = 0; i < complementos.length; i++) {
            item.DetalleItemRestaurante.DetallesComplemento.push({
                Id: complementos[i].Id,
                Nombre: complementos[i].Nombre,
                ItemsComplemento: []
            })
        }

        $scope.ordenDeVista.DetallesDeOrden.push({
            Id: 0,
            NombreItem: item.Nombre,
            Precio: parseFloat(item.Precio),
            Cantidad: item.Cantidad,
            Importe: item.Cantidad * item.Precio,
            DetalleItemRestauranteJson: "",
            IdItem: item.Id,
            ContadorComplementosSeleccionados: 0,
            Estado: false,
            ComplementosDeFamilia: item.ComplementosDeFamilia,
            DetalleItemRestaurante: item.DetalleItemRestaurante
        })
    }

    $scope.buscarItemPorCodigoBarra = function () {
        var codigoBarra = $("#codigoBarraItem").val();
        var result = $scope.items.find(item => item.CodigoBarra == codigoBarra);
        if (result != undefined) {
            $scope.obtenerItemCompleto(result.Id).success(data => {
                let item = data;
                item.Cantidad = 1;
                $scope.agregarItemAOrden(item);
                $scope._banderaRealizandoseOrden = true;
            }).error(data => {
                $scope.messageError(data);
            })
            $("#codigoBarraItem").val("");
            $("#itemSeleccionado").val("");
        } else {
            SweetAlert.error("ERROR", "No hay un item con este codigo de barra.");
            $("#codigoBarraItem").val("");
        }
    }

    $scope.calcularImporteOrden = function () {
        $scope.ordenDeVista.ImporteOrden = 0;
        for (var i = 0; i < $scope.ordenDeVista.DetallesDeOrden.length; i++) {
            $scope.ordenDeVista.DetallesDeOrden[i].Importe = ($scope.ordenDeVista.DetallesDeOrden[i].Cantidad * $scope.ordenDeVista.DetallesDeOrden[i].Precio).toFixed(2);
            $scope.ordenDeVista.ImporteOrden += parseFloat($scope.ordenDeVista.DetallesDeOrden[i].Importe);
        }
    }

    $scope.complementoSeleccionado = function (indexComplementos, complemento, $event, indexItemOrden) {
        let ItemOrden = $scope.ordenDeVista.DetallesDeOrden[indexItemOrden];
        let complementos = ItemOrden.DetalleItemRestaurante.DetallesComplemento;
        let itemsDelComplemento = complementos[indexComplementos].ItemsComplemento;
        if ($event.target.checked == false) {
            for (var i = 0; i < itemsDelComplemento.length; i++) {
                if (itemsDelComplemento[i].Id == complemento.Id) {
                    itemsDelComplemento.splice(i, 1);
                }
                ItemOrden.ContadorComplementosSeleccionados--;
            }
        } else if (itemsDelComplemento.length >= ItemOrden.ComplementosDeFamilia[indexComplementos].Maximo) {
            $event.target.checked = false;
        } else {
            itemsDelComplemento.push(complemento);
            ItemOrden.ContadorComplementosSeleccionados++;
        }
    }

    $scope.eliminarAnotacion = (indexOrden) => {
        let nodo = document.getElementById(`anotacion${indexOrden}`);
        nodo.classList.toggle('in');
    }
    $scope.limpiarOrden = function () {
        $scope.ordenDeVista = {
            Id: 0, Codigo: 0, DetallesDeOrden: [], ImporteOrden: 0, Estado: $scope.ParametrosDeOrden.EstadoConfirmado, IdMesa: 0, IdAtencion: $scope.atencionActual.Id, Mozo: {}
        }
        $scope._banderaRealizandoseOrden = false;
        $scope.establecerMozoParaAtencion();
    }
    $scope.limpiarCategorias = function () {
        $scope.itemsVisibles = $scope.items;
        var nivelesDeCategoria = $('.categoria');
        for (var i = 0; i < nivelesDeCategoria.length; i++) {
            nivelesDeCategoria[i].remove();
        }
        $scope.categoriaSeleccionada = "";
        $scope.iniciarCategorias();
    }

    $scope.limpiarSeleccionComplementos = function (indexItemOrden) {
        var itemOrden = $scope.ordenDeVista.DetallesDeOrden[indexItemOrden];

        for (var i = 0; i < $('.ItemComplemento' + indexItemOrden).length; i++) {
            $('.ItemComplemento' + indexItemOrden)[i].checked = false;
        }
        for (var i = 0; i < itemOrden.DetalleItemRestaurante.DetallesComplemento.length; i++) {
            itemOrden.DetalleItemRestaurante.DetallesComplemento[i].ItemsComplemento = [];
        }
        itemOrden.ContadorComplementosSeleccionados = 0;
    }

    $scope.mostrarComplementos = function (indexItemOrden) {
        $('#complementos' + indexItemOrden).show(250);
    }

    $scope.ocultarComplementos = function (indexItemOrden) {
        $('#complementos' + indexItemOrden).hide(250);
    }

    $scope.obtenerItemCompleto = function (idItem) {
        return restauranteService.ObtenerItemCompleto({ id: idItem });
    }

    $scope.obtenerItems = function () {
        restauranteService.obtenerItemsSuperficial({}).success(function (data) {
            $scope.items = data;
            $scope.itemsVisibles = data;
        }).error(function (data) {
            $scope.messageError(data.error);
        });
    }

    $scope.obtenerItemsDeCategoria = function (idCategoria) {
        restauranteService.obtenerItemsDeCategoria({ IdCategoria: idCategoria }).success(data => {
            $scope.itemsVisibles = data;
        }).error(data => {
            $scope.messageError(data);
        })
    }
    $scope.realizarOrden = function () {
        $scope.atencionActual.EsAtencionConMesa = $scope.atencionConMesa;
        $scope.atencionActual.EsAtencionPorDelivery = $scope.atencionDelivery;
        if ($scope.atencionConMesa == 'false') $scope.atencionActual.Mesa.IdAmbiente = $scope.ambienteActual.Id;
        $scope.atencionActual.ImporteAtencion += $scope.ordenDeVista.ImporteOrden;
        $scope.ordenDeVista.DetallesDeOrden.forEach(detalle => {
            detalle.DetalleItemRestauranteJson = JSON.stringify(detalle.DetalleItemRestaurante);
        });
        if ($scope.atencionActual.Id > 0) {
            $scope.ordenDeVista.IdAtencion = $scope.atencionActual.Id;
            restauranteService.agregarOrdenDeAtencion({ Orden: $scope.ordenDeVista }).success(data => {
                $scope.ordenDeVista.Id = data.data.Id
                $scope.ordenDeVista.Codigo = data.data.Codigo
                $scope.atencionActual.Ordenes.push($scope.ordenDeVista);
                $scope.ordenDeVista.DetallesDeOrden = data.data.DetallesDeOrden;
                $scope.ordenDeVista.DetallesDeOrden.forEach(detalle => {
                    detalle.DetalleItemRestaurante.AnotacionIndicacionExistente = detalle.DetalleItemRestaurante.AnotacionIndicacion;
                });
                $scope.realizarOrdenSucess(data);
            }).error(function (data) {
                $scope.ordenDeVista.IdAtencion = 0;
                $scope.atencionActual.ImporteAtencion -= $scope.ordenDeVista.ImporteOrden;
                //$scope.atencionActual.Ordenes.pop();
                SweetAlert.error2(data);
                $scope.messageError(data);
            })
        } else {
            $scope.atencionActual.Ordenes.push($scope.ordenDeVista);
            restauranteService.crearAtencionConOrden({ Atencion: $scope.atencionActual }).success(data => {
                $scope.atencionActual.Id = data.data.Id;
                $scope.atencionActual.Estado = data.data.Estado;
                $scope.atencionActual.Ordenes[0].Id = data.data.Ordenes[0].Id
                $scope.atencionActual.Ordenes[0].Codigo = data.data.Ordenes[0].Codigo
                $scope.atencionActual.Ordenes[0].DetallesDeOrden = data.data.Ordenes[0].DetallesDeOrden;
                if ($scope.atencionConMesa == 'true') {
                    $scope.mesas.find(m => m.Id == $scope.atencionActual.Mesa.Id).EstadoOcupado = true;
                    $scope.iniciarMesas();
                } else {
                    $scope.nuevaAtencionSinMesa = false;
                    $scope.atencionActual.Mesa = data.data.Mesa;
                    $scope.obtenerAtencionesSinMesa();
                }
                $scope.realizarOrdenSucess(data);
            }).error(function (data) {
                $scope.atencionActual.ImporteAtencion -= $scope.ordenDeVista.ImporteOrden;
                $scope.atencionActual.Ordenes.pop();
                SweetAlert.error2(data);
                $scope.messageError(data);
            });
        }
    }
    $scope.realizarOrdenSucess = function (data) {

        $scope.limpiarOrden();
        SweetAlert.success("Correcto", data.result_description);
        $("#tomarPedido").collapse('hide');
        $("#ordenes").collapse('show');
    }
    $scope.seleccionarCategoria = function (idCategoria, IndiceNivelCategoria, $event) {
        //OBTENER ITEMS POR CATEGORIAS Y ACTUALIZAR LOS ITEMS VISIBLES
        $scope.obtenerItemsDeCategoria(idCategoria);
        // MOSTRAR LAS OTRAS CATEGORIAS EN VISTA.
        var temp = $scope.categoriaSeleccionada.split('.');
        temp.length = IndiceNivelCategoria;
        temp[IndiceNivelCategoria - 1] = idCategoria;
        var nivelesDeCategoria = $('.categoria');
        if (nivelesDeCategoria.length > IndiceNivelCategoria) {
            for (var i = IndiceNivelCategoria; i < nivelesDeCategoria.length; i++) {
                nivelesDeCategoria[i].remove();
            }
        }
        $($event.target).parent().children().removeClass('btn-primary').addClass('btn-info');
        $($event.target).removeClass('btn-info').addClass('btn-primary');
        var categorias = [];
        if ($scope.categorias.length != 0) {
            for (var i = 0; i < $scope.categorias.length; i++) {
                if ($scope.categorias[i].IdPadre == idCategoria) {
                    categorias.push($scope.categorias[i]);
                }
            }
        }
        if (categorias.length > 0) {
            var html = "<div class='categoria'>"
            for (var i = 0; i < categorias.length; i++) {
                html += "<button type='button' class='btn btn-xs btn-info' style='margin: 0px 5px 5px 5px' ng-click='seleccionarCategoria(" + categorias[i].Id + "," + (IndiceNivelCategoria + 1) + ",$event)' >" + categorias[i]['Nombre'] + "</button>"
            }
            html += "</div>"
            $('#categorias').append($compile(html)($scope));
        }
    }

    $scope.agregarItemSeleccionadoDesdeCantidad = function () {
        if ($scope.itemSeleccionado.Concepto.Id != undefined && $scope.itemSeleccionado.Cantidad != null && $scope.itemSeleccionado.Cantidad != 0 && $scope.itemSeleccionado.Cantidad != '') {
            $scope.agregarItemSeleccionado();
        }
    }

    $scope.desabilitarTomarOrden = function () {
        return $scope.itemSeleccionado.Concepto == undefined || $scope.itemSeleccionado.Cantidad == undefined;
    }
}

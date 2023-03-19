AmbienteAtencionController = function ($scope, $q, $timeout, $rootScope, $compile, SweetAlert, restauranteService) {

    $scope.iniciarMesas = function () {
        //REMUEVE EL DIV DE LAS MESAS DE LA VISTA
        if ($("#divMesas")) {
            $("#divMesas").remove();
        }
        window.oncontextmenu = function () { return false; }
        //EFECTO DE ACORDEON DEL PANEL DE ATENCION
        let tomarpedido = document.querySelector('#tomarpedido-box').querySelector('.box-tools').querySelector('a');
        let ordenes = document.querySelector('#ordenes-box').querySelector('a');
        tomarpedido.addEventListener('click', function () {
            document.getElementById('ordenes').classList.remove('in')
        });
        ordenes.addEventListener('click', function () {
            document.getElementById('tomarPedido').classList.remove('in')
        });
        //INICIAMOS LA ETIQUETA DEL DIV "DIVMESAS" QUE CONTENDRA LAS MESASsa mesa-ocupada
        let html = "<div id='divMesas'>";
        let dimensionMesa = "width:" + 100 / $scope.ambienteActual.Columnas + "%; height:" + $scope.alturaContenedor / $scope.ambienteActual.Filas + "px;";
        // AGREGAMOS LAS MESAS AL STRING HTML
        for (var fila = 1; fila <= $scope.ambienteActual.Filas; fila++) {
            html += "<div style='display: flex; flex - direction: row; justify-content: space-around;'>";
            for (var columna = 1; columna <= $scope.ambienteActual.Columnas; columna++) {
                var mesa = $scope.mesas.find(mesa => mesa.Fila == fila && mesa.Columna == columna);
                if (mesa != undefined) {

                    if (!mesa.EstadoOcupado) {
                        html += "<button class='btn btn-mesa-disponible' ng-click='mostrarMenuClick(" + mesa.Id + ")'  ng-right-click='mostrarMenuRightClick(" + mesa.Id + ",{ fila: " + fila + ", columna: " + columna + "})' on-long-press='mostrarMenuRightClick(" + mesa.Id + ",{ fila: " + fila + ", columna: " + columna + "})' style='" + dimensionMesa + " border: 1.5px solid; border-radius: 2px;  text-align: center; vertical-align: middle; font-size: 20px; color:white ; cursor: pointer; margin:1px;'><span>" + mesa.Nombre + "</span></button>";
                    } else {
                        html += "<button class='btn btn-mesa-ocupada' ng-disabled='_banderaCambiandoMesa' ng-click='mostrarMenuClick(" + mesa.Id + ")' ng-right-click='mostrarMenuRightClick(" + mesa.Id + ",{ fila: " + fila + ", columna: " + columna + "})' on-long-press='mostrarMenuRightClick(" + mesa.Id + ",{ fila: " + fila + ", columna: " + columna + "})' style='" + dimensionMesa + " border: 1.5px solid;border-radius: 2px; font-size: 20px; cursor: pointer; margin:1px;'><span>" + mesa.Nombre + "</span></button>";
                    }
                } else {
                    html += "<div class='btn btn-info'  ng-click='mostrarMenuClick(-1)' ng-right-click='mostrarMenuRightClick(-1,{ fila: " + fila + ", columna: " + columna + "})' on-long-press='mostrarMenuRightClick(-1,{ fila: " + fila + ", columna: " + columna + "})' style='" + dimensionMesa + " border: 1.5px solid gray; background-color: white; border-radius: 2px; cursor: pointer; margin:1px;'></div>";
                }
            }
            html += "</div>";
        }
        html += "</div>";
        //AGREGAMOS EL DIV "DIVMESAS" AL CONTENEDOR DE MESAS.
        $("#contenedorDeMesas").append($compile(html)($scope));
    }

    $scope.mostrarMenuClick = (mesaId) => {
        var mesa = $scope.mesas.find(mesa => mesa.Id == mesaId);
        //SI HACE CLICK SOBRE UN ESPACIO SIN MESA, SE LIMPIA LA ATENCIÒN ACTUAL
        if (mesa == undefined) {
            if ($scope._banderaRealizandoseOrden == false) {
                $scope.limpiarAtencion();
                $scope._banderaMesaSeleccionada = false;
            } else {
                $scope.alertaSalvarAtencion().then((result) => {
                    if (result.isConfirmed) {
                        $scope.limpiarAtencion();
                        $scope.limpiarOrden();
                        $scope._banderaMesaSeleccionada = false;
                        $scope._banderaRealizandoseOrden = false;
                    }
                })
            }
        } else {//SI HACE CLICK SOBRE UNA MESA
            if ($scope._banderaCambiandoMesa) {
                var mesaNueva = $scope.mesas.find(mesa => mesa.Id == mesaId);
                if (!mesaNueva.EstadoOcupado) {
                    $scope.cambiarMesaDeAtencionActualPor(mesaNueva)
                }
            }
            else {
                $scope.actualizarAtencion(mesaId)
            }
        }
    }

    $scope.mostrarMenuRightClick = function (mesaId, posicion) {
        var mesa = $scope.mesas.find(mesa => mesa.Id == mesaId);
        //SI ESTA VACIO LE APARECERA UN MENU DE CREACIÒN DE MESA
        if (mesa == undefined) {
            //MUESTRA UN INPUT PARA EL NOMBRE DE LA MESA
            Swal.fire({
                title: 'Creación de Mesa',
                text: 'Indique el nombre de la mesa : EJEMPLO: "AB" , "1" , "A1" ',
                input: 'text',
                inputAttributes: {
                    autocapitalize: 'off'
                },
                showCancelButton: true,
                confirmButtonText: 'Crear',
                showLoaderOnConfirm: true,
                allowOutsideClick: () => !Swal.isLoading()
            }).then((result) => {
                if (result.isConfirmed) {
                    //SE VALIDA QUE EL VALOR DEL INPUT NO SEA NULO, Y QUE LA MESA NO EXISTA.
                    let mesa = $scope.mesas.find(mesa => mesa.Nombre == result.value);
                    if (result.value != "") {
                        if (mesa == undefined) {
                            var nuevaMesa = {};
                            nuevaMesa.IdAmbiente = $scope.ambienteActual.Id;
                            nuevaMesa.Nombre = result.value;
                            nuevaMesa.Fila = posicion.fila;
                            nuevaMesa.Columna = posicion.columna;
                            nuevaMesa.EstadoOcupado = false;
                            //SE GUARDA LA MESA
                            restauranteService.crearMesa(nuevaMesa).success(data => {
                                //SE ACTUALIZAN LAS MESAS DEL AMBIENTE.
                                $scope.obtenerMesasDeAmbiente($scope.ambienteActual.Id);
                            }).error(data => {
                                SweetAlert.error2(data);
                            });
                        } else {
                            Swal.fire('Error', 'La mesa con el número de mesa indicado , ya existe.', 'error');
                        }
                    } else {
                        Swal.fire('Error', 'Ingrese algùn valor', 'error');
                    }
                }
            })
        } else {
            if (mesa.EstadoOcupado) {
                Swal.fire('No permitido', "No se pueden realizar cambios cuando la mesa esta ocupada", 'error');
            } else if (!mesa.EstadoOcupado) {
                Swal.fire({
                    title: 'OPCIONES DE MESA',
                    showDenyButton: true,
                    showCancelButton: true,
                    confirmButtonText: `Editar`,
                    denyButtonText: `Eliminar`,
                }).then((result) => {
                    if (result.isConfirmed) {
                        Swal.fire({
                            title: 'Cambio de nombre de mesa',
                            text: 'Indica el valor de la mesa : EJEMPLO: "AB" , "1" , "A1" ',
                            input: 'text',
                            inputAttributes: {
                                autocapitalize: 'off'
                            },
                            showCancelButton: true,
                            confirmButtonText: 'Modificar',
                            showLoaderOnConfirm: true,
                            allowOutsideClick: () => !Swal.isLoading()
                        }).then((result) => {
                            if (result.isConfirmed) {
                                let mesa = $scope.mesas.find(mesa => mesa.Nombre == result.value);
                                if (result.value != "") {
                                    if (mesa == undefined) {
                                        let mesaSeleccionada = $scope.mesas.find(mesa => mesa.Id == mesaId);
                                        mesaSeleccionada.Nombre = result.value;
                                        restauranteService.actualizarMesa(mesaSeleccionada).success(data => {
                                            //ACTUALIZAMOS EN VISTA
                                            mesaSeleccionada.Nombre = result.value;
                                            $scope.iniciarMesas();
                                            if ($scope.atencionActual.Mesa.Id == mesaSeleccionada.Id) {
                                                $scope.atencionActual.Mesa.Nombre = mesaSeleccionada.Nombre;
                                            }
                                        }).error(data => {
                                            SweetAlert.error2(data);
                                        });
                                    } else {
                                        Swal.fire('Fallo', 'La mesa con el valor indicado , ya existe.', 'error');
                                    }
                                } else {
                                    Swal.fire('Fallo', 'Ingrese algùn valor', 'error');
                                }
                            }
                        })
                    } else if (result.isDenied) {
                        Swal.fire({
                            title: '¿Estas seguro de eliminar la mesa?',
                            icon: 'warning',
                            showCancelButton: true,
                            confirmButtonColor: '#3085d6',
                            cancelButtonColor: '#d33',
                            confirmButtonText: 'Si, eliminalo!'
                        }).then((result) => {
                            if (result.isConfirmed) {
                                //ELIMINAR MESA
                                restauranteService.eliminarMesa({ idMesa: mesaId }).success(data => {
                                    //ACTUALIZAR MESAS DE AMBIENTE
                                    var mesaIndex = $scope.mesas.findIndex(m => m.Id == mesaId);
                                    $scope.mesas.splice(mesaIndex, 1);
                                    $scope.iniciarMesas();
                                }).error(data => {
                                    SweetAlert.error2(data);
                                });
                            }
                        })
                    }
                })
            }
        }
    }

    $scope.agregarAmbiente = async function () {
        //MODAL FORMULARIO PARA CREACIÒN DE AMBIENTE
        const { value: formValues } = await Swal.fire({
            title: 'Crear Ambiente',
            html:
                '<input id="swal-ambiente-input1" class="swal2-input" placeholder="Nombre de Ambiente">' +
                '<input id="swal-ambiente-input2" type="number" class="swal2-input" placeholder="Filas" min=0>' +
                '<input id="swal-ambiente-input3" type="number" class="swal2-input" placeholder="Columnas" min=0>' +
                '<input type="checkbox" id="mesa-temporal" name="mesaTemporal">' +
                '<label for= "mesa-temporal"> &nbsp; PERMITIR MESA TEMPORAL</label> <br>',
            focusConfirm: false,
            confirmButtonText: "Guardar",
            showCancelButton: true,
            cancelButtonText: "Cerrar",
            preConfirm: () => {
                return [
                    document.getElementById('swal-ambiente-input1').value,
                    document.getElementById('swal-ambiente-input2').value,
                    document.getElementById('swal-ambiente-input3').value,
                    document.getElementById('mesa-temporal').checked
                ]
            }
        });
        if (formValues) {
            //VALIDA LOS CAMPOS DEL FORMULARIO
            if (formValues[1] <= 0 || formValues[1] == "" || formValues[2] <= 0 || formValues[2] == "" || formValues[0] == "") {
                Swal.fire({
                    icon: 'error',
                    title: 'ERROR',
                    text: 'INGRESAR LOS DATOS CORRECTAMENTE',
                })
            } else {
                var ambiente = {
                    Id: 0,
                    Nombre: formValues[0],
                    Filas: formValues[1],
                    Columnas: formValues[2],
                    MesasTemporales: formValues[3],
                    Establecimiento: {
                        Id: $scope.EstablecimientoSeleccionado.Id, Nombre: $scope.EstablecimientoSeleccionado.Nombre
                    }
                }
                //Agrega Ambiente y Actualiza Pagina
                restauranteService.crearAmbiente(ambiente).success(function (data) {
                    SweetAlert.success("Correcto", data.result_description);
                    location.reload();
                }).error(function (data, status) {
                    SweetAlert.error2(data);
                });
            }
        }
    }

    $scope.editarAmbiente = async function (ambiente) {
        //MODAL FORMULARIO PARA CREACIÒN DE AMBIENTE
        const { value: formValues } = await Swal.fire({
            title: 'Actualizar Ambiente',
            html:
                '<input id="swal-ambiente-input1" class="swal2-input" value="' + ambiente.Nombre + '" placeholder="Nombre de Ambiente">' +
                '<input id="swal-ambiente-input2" type="number" class="swal2-input" value="' + ambiente.Filas + '" placeholder="Filas" min=0>' +
                '<input id="swal-ambiente-input3" type="number" class="swal2-input" value="' + ambiente.Columnas + '" placeholder="Columnas" min=0>' +
                '<input type="checkbox" id="mesa-temporal" name="mesaTemporal" ' + (ambiente.MesasTemporales ? 'checked="true"' : '') + '">' +
                '<label for= "mesa-temporal"> &nbsp; PERMITIR MESA TEMPORAL</label> <br>',
            focusConfirm: false,
            confirmButtonText: "Guardar",
            showCancelButton: true,
            cancelButtonText: "Cerrar",
            preConfirm: () => {
                return [
                    document.getElementById('swal-ambiente-input1').value,
                    document.getElementById('swal-ambiente-input2').value,
                    document.getElementById('swal-ambiente-input3').value,
                    document.getElementById('mesa-temporal').checked
                ]
            }
        });
        if (formValues) {
            //Valida los campos del formulario
            if (formValues[1] <= 0 || formValues[1] == "" || formValues[2] <= 0 || formValues[2] == "" || formValues[0] == "") {
                Swal.fire({
                    icon: 'error',
                    title: 'ERROR',
                    text: 'INGRESAR LOS DATOS CORRECTAMENTE',
                })
            } else {
                var ambienteUpdate = {
                    Id: ambiente.Id,
                    Nombre: formValues[0],
                    Filas: formValues[1],
                    Columnas: formValues[2],
                    MesasTemporales: formValues[3],
                    Establecimiento: {
                        Id: $scope.EstablecimientoSeleccionado.Id, Nombre: $scope.EstablecimientoSeleccionado.Nombre
                    }
                }
                let maximoFilas = Math.max.apply(Math, $scope.mesas.map(function (item) { return item.Fila; }));
                let maximoColumnas = Math.max.apply(Math, $scope.mesas.map(function (item) { return item.Fila; }));
                if (ambienteUpdate.Filas < maximoFilas || ambienteUpdate.Columnas < maximoColumnas) {
                    Swal.fire({
                        icon: 'error',
                        title: 'ERROR',
                        text: 'EL NUMERO DE FILAS/COLUMNAS INCORRECTO',
                    })
                } else {
                    //Actualizar Ambiente y Actualiza Pagina
                    restauranteService.actualizarAmbiente(ambienteUpdate).success(function (data) {
                        SweetAlert.success("Correcto", data.result_description);
                        location.reload();
                    }).error(function (data, status) {
                        SweetAlert.error2(data);
                    });
                }
            }
        }
    }

    $scope.eliminarAmbiente = async function (ambiente) {
        Swal.fire({
            title: '¿Estas seguro de eliminar el ambiente?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Si, eliminalo!'
        }).then((result) => {
            if (result.isConfirmed) {
                //ELIMINAR AMBIENTE
                restauranteService.eliminarAmbiente({ idAmbiente: ambiente.Id }).success(data => {
                    location.reload();
                }).error(data => {
                    SweetAlert.error2(data);
                });
            }
        })
    }

    $scope.agregarMesa = function (mesa) {
        restauranteService.crearMesa({ Mesa: mesa }).success(function (data) {
            SweetAlert.success("Correcto", data.result_description)
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.eliminarMesa = function (idMesa) {
        restauranteService.eliminarMesa(idMesa).success(function (data) {
            SweetAlert.success("Correcto", data.result_description)
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.actualizarMesa = function (mesa) {
        restauranteService.actualizarMesa(mesa).success(function (data) {
            SweetAlert.success("Correcto", data.result_description)
            $scope.iniciarMesas();
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.obtenerMesasDeAmbiente = function (idAmbiente) {
        restauranteService.obtenerMesasDeAmbiente({ idAmbiente: idAmbiente }).success(function (data) {
            $scope.mesas = data;
            //UNA VEZ OBTENIDA LAS MESAS , SE PINTAN EN VISTA.
            $scope.iniciarMesas();
        }).error(function (data) {
            SweetAlert.error2(data);
        });
    }

    $scope.obtenerMesaDeVista = function (idMesa) {
        return $scope.mesas.find(m => m.Id == idMesa);
    }

    $scope.obtenerAmbientesConMesa = function (numeroAmbienteASeleccionar) {
        restauranteService.obtenerAmbientes({ IdEstablecimiento: $scope.EstablecimientoSeleccionado.Id }).success(function (data) {
            $scope.ambientesConMesa = data;
            //SI EXISTE POR LO MENOS 1 AMBIENTE , SE SELECCIONA EL PRIMERO POR DEFECTO Y SE OBTIENEN SUS MESAS.
            if ($scope.ambientesConMesa.length != 0) {
                $scope.ambienteActual = $scope.ambientesConMesa[numeroAmbienteASeleccionar - 1];
                $scope.numeroDeAmbiente = numeroAmbienteASeleccionar;
                $scope.obtenerMesasDeAmbiente($scope.ambienteActual.Id);
            }
            $scope.ambientesConMesaCargados = true;
        }).error(function (data) {
            SweetAlert.error2(data);
        })
    }

    $scope.obtenerAmbientesSinMesa = function (numeroAmbienteASeleccionar) {
        restauranteService.obtenerCentrosAtencionRestaurante({ IdEstablecimiento: $scope.EstablecimientoSeleccionado.Id }).success(function (data) {
            $scope.ambientesSinMesa = data;
            //SI EXISTE POR LO MENOS 1 AMBIENTE , SE SELECCIONA EL PRIMERO POR DEFECTO Y SE OBTIENEN SUS MESAS.
            if ($scope.ambientesSinMesa.length != 0 && !$scope.ParametrosDeAtencion.PermitirVentaEnMesa) {
                $scope.ambienteActual = $scope.ambientesSinMesa[numeroAmbienteASeleccionar - 1];
                $scope.numeroDeAmbiente = numeroAmbienteASeleccionar;
                $scope.cambioAmbienteSinMesa($scope.numeroDeAmbiente);
            }
            $scope.ambientesSinMesaCargados = true;
        }).error(function (data) {
            SweetAlert.error2(data);
        })
    }
}

app.factory('maestroService', function ($http) {
    return {
        listarMaestros: function (params) {
            return $http.post(URL_ + "/Maestro/ListarMaestros", params);
        },
        listarDetallesMaestro: function (params) {
            return $http.post(URL_ + "/Maestro/ListarDetallesMaestro", params);
        },
        guardarMaestro: function (params) {
            return $http.post(URL_ + "/Maestro/GuardarMaestro", params);
        },
        guardarDetalleMaestro: function (params) {
            return $http.post(URL_ + "/Maestro/GuardarDetalleMaestro", params);
        },
        obtenerCategoriasConcepto: function (params) {
            return $http.post(URL_ + "/Maestro/ObtenerCategoriasConcepto", params);
        },
        obtenerCaracteristicasConcepto: function (params) {
            return $http.post(URL_ + "/Maestro/ObtenerCaracteristicasConcepto", params);
        },
        obtenerCaracteristicasParaConcepto: function (params) {
            return $http.post(URL_ + "/Maestro/ObtenerCaracteristicasParaConcepto", params);
        },
        obtenerConceptosDeCompraVenta: function (params) {
            return $http.post(URL_ + "/Maestro/ObtenerConceptosDeCompraVenta", params);
        },
        obtenerConceptosDeConceptoGasto: function (params) {
            return $http.post(URL_ + "/Maestro/ObtenerConceptosDeConceptoGasto", params);
        },
        obtenerUnidadesDeNegocio: function (params) {
            return $http.post(URL_ + "/Maestro/ObtenerUnidadesDeNegocio", params);
        },
        obtenerTiposDeTransaccion: function (params) {
            return $http.post(URL_ + "/Maestro/ObtenerTiposDeTransaccion", params);
        },
        obtenerUnidadesDeMedida: function (params) {
            return $http.post(URL_ + "/Maestro/ObtenerUnidadesDeMedida", params);
        },
        obtenerPresentaciones: function (params) {
            return $http.post(URL_ + "/Maestro/ObtenerPresentaciones", params);
        },
        obtenerProductosCompra: function (params) {
            return $http.post(URL_ + "/Maestro/ObtenerProductosCompra", params);
        },
        obtenerEntidadesFinancieras: function (params) {
            return $http.post(URL_ + "/Maestro/ObtenerEntidadesFinancieras", params);
        },
        obtenerEntidades: function (params) {
            return $http.post(URL_ + "/Maestro/ObtenerEntidades", params);
        },
        obtenerOperadoresDeTarjeta: function (params) {
            return $http.post(URL_ + "/Maestro/ObtenerOperadoresDeTarjeta", params);
        },
        obtenerTiposDePago: function (params) {
            return $http.post(URL_ + "/Maestro/ObtenerTiposDePago", params);
        },
        obtenerAcciones: function (params) {
            return $http.post(URL_ + "/Maestro/ObtenerAcciones", params);
        },
        obtenerEstados: function (params) {
            return $http.post(URL_ + "/Maestro/ObtenerEstados", params);
        },
        obtenerRolesPersonal: function (params) {
            return $http.post(URL_ + "/Maestro/ObtenerRolesPersonal", params);
        },
        obtenerMediosDePago: function (params) {
            return $http.post(URL_ + "/Maestro/ObtenerMediosDePago", params);
        },
        obtenerTiposDeComprobante: function (params) {
            return $http.post(URL_ + "/Maestro/ObtenerTiposDeComprobante", params);
        },
        obtenerTarifas: function (params) {
            return $http.post(URL_ + "/Maestro/ObtenerTarifas", params);
        },
        obtenerTercero: function (params) {
            return $http.post(URL_ + "/Maestro/ObtenerTercero", params);
        },
        listarTiposDeDocumentosDeIdentidad: function (params) {
            return $http.post(URL_ + "/Maestro/ListarTiposDeDocumentosDeIdentidad", params);
        },
        listarUbigeoDistrito: function (params) {
            return $http.post(URL_ + "/Maestro/ListarUbigeoDistrito", params);
        },
        listarTiposDeZona: function (params) {
            return $http.post(URL_ + "/Maestro/ListarTiposDeZona", params);
        },
        listarNaciones: function (params) {
            return $http.post(URL_ + "/Maestro/ListarNaciones", params);
        },
        listarTiposDeDireccion: function (params) {
            return $http.post(URL_ + "/Maestro/ListarTiposDeDireccion", params);
        },
        listarCategoriasProducto: function (params) {
            return $http.post(URL_ + "/Maestro/ListarCategoriasProducto", params);
        },
        listarSubContenidos: function (params) {
            return $http.post(URL_ + "/Maestro/ListarSubContenidos", params);
        },
        listarCaracteristicas: function (params) {
            return $http.post(URL_ + "/Maestro/ListarCaracteristicas", params);
        },
        obtenerRoles: function (params) {
            return $http.post(URL_ + "/Admin/GetAllRolesAsSelectList", params);
        },
        listarEmpleados: function (params) {
            return $http.post(URL_ + "/Maestro/ListarEmpleados", params);
        },
        obtenerDatosEmpleado: function (params) {
            return $http.post(URL_ + "/Maestro/ObtenerDatosEmpleado", params);
        },
        listarConceptosPagoEmpleados: function (params) {
            return $http.post(URL_ + "/Maestro/ListarConceptosPagoEmpleados", params);
        },
        listarTiposServicioImpuesto: function (params) {
            return $http.post(URL_ + "/Maestro/ObtenerTiposServicioImpuesto", params);
        },
        listarTiposProductoDeCompra: function (params) {
            return $http.post(URL_ + "/Maestro/ObtenerTiposProductoDeCompra", params);
        },
        listarTiposBien: function (params) {
            return $http.post(URL_ + "/Maestro/ObtenerTiposBien", params);
        },
        listarTiposDeActor: function (params) {
            return $http.post(URL_ + "/Persona/ListarTiposDeActor", params);
        },
        listarTiposDeClaseActor: function (params) {
            return $http.post(URL_ + "/Persona/ListarTiposDeClaseActor", params);
        },
        listarTiposDeEstadoLegalActor: function (params) {
            return $http.post(URL_ + "/Persona/ListarTiposDeEstadoLegalActor", params);
        },
        obtenerDenominacionClaseActor: function (params) {
            return $http.post(URL_ + "/Persona/ObtenerDenominacionClaseActor", params);
        },
        obtenerModalidadesTraslado: function (params) {
            return $http.post(URL_ + "/Maestro/ObtenerModalidadesTraslado", params);
        },
        obtenerMotivosTraslado: function (params) {
            return $http.post(URL_ + "/Maestro/ObtenerMotivosTraslado", params);
        },
        obtenerTiposDeNota: function (params) {
            return $http.post(URL_ + "/Maestro/ObtenerTiposDeNota", params);
        },
        obtenerConceptosBasicos: function (params) {
            return $http.post(URL_ + "/Maestro/ObtenerConceptosBasicos", params);
        },
        obtenerConceptosBasicosServicio: function (params) {
            return $http.post(URL_ + "/Maestro/ObtenerConceptosBasicosServicio", params);
        },

        obtenerMonedas: function (params) {
            return $http.post(URL_ + "/Maestro/ObtenerMonedas", params);
        },
        obtenerTiposCuentaBancaria: function (params) {
            return $http.post(URL_ + "/Maestro/ObtenerTiposCuentaBancaria", params);
        },
        obtenerFamiliasVigentes: function (params) {
            return $http.post(URL_ + "/Maestro/ObtenerFamiliasVigentes", params);
        },
        obtenerFamiliasVigentesPorModoSeleccionTipoFamilia: function (params) {
            return $http.post(URL_ + "/Maestro/ObtenerFamiliasVigentesPorModoSeleccionTipoFamilia", params);
        },
        obtenerFamiliasConceptosComercialesVigentes: function (params) {
            return $http.post(URL_ + "/Maestro/ObtenerFamiliasConceptosComercialesVigentes", params);
        },
        obtenerFamiliasConceptosComercialesVigentesPorModoSeleccionTipoFamilia: function (params) {
            return $http.post(URL_ + "/Maestro/ObtenerFamiliasConceptosComercialesVigentesPorModoSeleccionTipoFamilia", params);
        },
        obtenerMotivosDeViaje: function (params) {
            return $http.post(URL_ + "/Maestro/ObtenerMotivosDeViaje", params);
        },
        obtenerListaSexos: function (params) {
            return $http.post(URL_ + "/Persona/ObtenerListaSexos", params);
        },
        obtenerListaTiposDeSociedad: function (params) {
            return $http.post(URL_ + "/Persona/ObtenerListaTiposDeSociedad", params);
        },
        obtenerListaEstadosCiviles: function (params) {
            return $http.post(URL_ + "/Persona/ObtenerListaEstadosCiviles", params);
        },
        obtenerTiposGrupoClientes: function (params) {
            return $http.post(URL_ + "/Maestro/ObtenerTiposGrupoClientes", params);
        },
        obtenerClasificacionesGrupoClientes: function (params) {
            return $http.post(URL_ + "/Maestro/ObtenerClasificacionesGrupoClientes", params);
        },
    };
});

app.factory('conceptoService', function ($http) {
    return {
        obtenerConcepto: function (params) {
            return $http.post(URL_ + "/Concepto/ObtenerConcepto", params);
        },
        verConceptos: function (params) {
            return $http.post(URL_ + "/Concepto/ObtenerConceptos", params);
        },
        obtenerFamilias: function (params) {
            return $http.post(URL_ + "/Concepto/ObtenerFamilias", params);
        },
        verCategorias: function (params) {
            return $http.post(URL_ + "/Concepto/ObtenerCategorias", params);
        },
        verCaracteristicas: function (params) {
            return $http.post(URL_ + "/Concepto/ObtenerCaracteristicas", params);
        },
        obtenerCaracteristicasComunesConcepto: function (params) {
            return $http.post(URL_ + "/Concepto/ObtenerCaracteristicasComunesConcepto", params);
        },

        verValoresCaracteristica: function (params) {
            return $http.post(URL_ + "/Concepto/ObtenerValoresCaracteristica", params);
        },
        guardarConcepto: function (params) {
            return $http.post(URL_ + "/Concepto/GuardarConcepto", params);
        },
        cambioEsVigenteFamilia: function (params) {
            return $http.post(URL_ + "/Concepto/CambioEsVigenteFamilia", params);
        },
        guardarCategoria: function (params) {
            return $http.post(URL_ + "/Concepto/GuardarCategoria", params);
        },
        guardarCaracteristica: function (params) {
            return $http.post(URL_ + "/Concepto/GuardarCaracteristica", params);
        },
        guardarValorCaracteristica: function (params) {
            return $http.post(URL_ + "/Concepto/GuardarValorCaracteristica", params);
        },
        obtenerRolesConceptosGasto: function (params) {
            return $http.post(URL_ + "/Concepto/ObtenerRolesConceptosGasto", params);
        },
        guardarConceptoGasto: function (params) {
            return $http.post(URL_ + "/Concepto/GuardarConceptoGasto", params);
        },
        obtenerConceptosGastos: function (params) {
            return $http.post(URL_ + "/Concepto/ObtenerConceptosGastos", params);
        },
        obtenerConceptoGasto: function (params) {
            return $http.post(URL_ + "/Concepto/ObtenerConceptoGasto", params);
        },
        obtenerPrecioVentaNormalDelConceptoParaVentasMasivasSegunElPuntoDeventa: function (params) {
            return $http.post(URL_ + "/Concepto/ObtenerPrecioVentaNormalDelConceptoParaVentasMasivasSegunElPuntoDeVenta", params);
        },
        obtenerConceptosDeNegociosComercialesParaCompra: function (params) {
            return $http.post(URL_ + "/Concepto/ObtenerConceptosDeNegociosComercialesParaCompra", params);
        },
        obtenerConceptosDeNegociosComercialesParaCompraPorCodigoBarra: function (params) {
            return $http.post(URL_ + "/Concepto/ObtenerConceptosDeNegociosComercialesParaCompraPorCodigoBarra", params);
        },
        obtenerConceptosDeNegociosComercialesParaVenta: function (params) {
            return $http.post(URL_ + "/Concepto/ObtenerConceptosDeNegociosComercialesParaVenta", params);
        },
        obtenerConceptosDeNegociosComercialesParaVentaPorCodigoBarra: function (params) {
            return $http.post(URL_ + "/Concepto/ObtenerConceptosDeNegociosComercialesParaVentaPorCodigoBarra", params);
        },
        obtenerConceptosDeNegociosComercialesParaVentaPorId: function (params) {
            return $http.post(URL_ + "/Concepto/ObtenerConceptosDeNegociosComercialesParaVentaPorId", params);
        },
        obtenerComplementosConceptoDeNegocioComercialParaVenta: function (params) {
            return $http.post(URL_ + "/Concepto/ObtenerComplementosConceptoDeNegocioComercialParaVenta", params);
        },
        obtenerConceptosBasicosIncluyendoCaracteristicas: function (params) {
            return $http.post(URL_ + "/Concepto/ObtenerConceptosBasicosIncluyendoCaracteristicas", params);
        },
        obtenerConceptosDeNegociosComercialesParaVentaPorNombre: function (params) {
            return $http.post(URL_ + "/Concepto/ObtenerConceptosDeNegociosComercialesParaVentaPorNombre", params);
        },
        obtenerTodosLosConceptosDeNegociosComercialesParaVenta: function (params) {
            return $http.post(URL_ + "/Concepto/ObtenerTodosLosConceptosDeNegociosComercialesParaVenta", params);
        },
        obtenerConceptosDeNegociosComercialesParaVentaPorIdConceptoNegocio: function (params) {
            return $http.post(URL_ + "/Concepto/ObtenerConceptosDeNegociosComercialesParaVentaPorIdConceptoNegocio", params);
        },
        guardarConceptoServicio: function (params) {
            return $http.post(URL_ + "/Concepto/GuardarConceptoServicio", params);
        },
        obtenerConceptosDeNegociosComerciales: function (params) {
            return $http.post(URL_ + "/Concepto/ObtenerConceptosDeNegociosComerciales", params);
        },
        obtenerConceptosDeNegociosComercialesPorBusquedaConcepto: function (params) {
            return $http.post(URL_ + "/Concepto/obtenerConceptosDeNegociosComercialesPorBusquedaConcepto", params);
        },
        obtenerConceptosDeNegociosComercialesPorFamilia: function (params) {
            return $http.post(URL_ + "/Concepto/ObtenerConceptosDeNegociosComercialesPorFamilia", params);
        },
        obtenerConceptoDeNegocioComercialPorCodigoBarra: function (params) {
            return $http.post(URL_ + "/Concepto/ObtenerConceptoDeNegocioComercialPorCodigoBarra", params);
        },
        obtenerConceptoDeNegocioComercialPorIdConcepto: function (params) {
            return $http.post(URL_ + "/Concepto/ObtenerConceptoDeNegocioComercialPorIdConcepto", params);
        },
        cambioEsVigenteCaracteristica: function (params) {
            return $http.post(URL_ + "/Concepto/CambioEsVigenteCaracteristica", params);
        },
        obtenerBandejaCaracteristicas: function (params) {
            return $http.post(URL_ + "/Concepto/ObtenerBandejaCaracteristicas", params);
        },
        obtenerCaracteristicasPorFamilia: function (params) {
            return $http.post(URL_ + "/Concepto/ObtenerCaracteristicasPorFamilia", params);
        },
    };
});
app.factory('caracteristicaService', function ($http) {
    return {
        obtenerCaracteristicasYValoresDeCaracteristicasDeConceptoNegocio: function (params) {
            return $http.post(URL_ + "/Caracteristica/ObtenerCaracteristicasYValoresDeCaracteristicasDeConceptoNegocio", params);
        },

    };
});
app.factory('almacenService', function ($http) {
    return {
        obtenerTiposDeComprobanteMovimientoDeAlmacen: function (params) {
            return $http.post(URL_ + "/Almacen/ObtenerTiposDeComprobanteMovimientoDeAlmacen", params);
        },
        obtenerTiposDeComprobanteOrdenDeAlmacen: function (params) {
            return $http.post(URL_ + "/Almacen/ObtenerTiposDeComprobanteOrdenDeAlmacen", params);
        },
        obtenerOrdenesMovimientoInternoMercaderia: function (params) {
            return $http.post(URL_ + "/Almacen/ObtenerOrdenesMovimientoInternoMercaderia", params);
        },
        obtenerMovimientoInternoMercaderiaIncluidoDetallesOrden: function (params) {
            return $http.post(URL_ + "/Almacen/ObtenerMovimientoInternoMercaderiaIncluidoDetallesOrden", params);
        },
        guardarMovimientoInternoMercaderia: function (params) {
            return $http.post(URL_ + "/Almacen/GuardarMovimientoInternoMercaderia", params);
        },
        obtenerOrdenesMovimientoMercaderia: function (params) {
            return $http.post(URL_ + "/Almacen/ObtenerOrdenesMovimientoMercaderia", params);
        },
        obtenerMovimientoMercaderiaIncluidoDetallesOrden: function (params) {
            return $http.post(URL_ + "/Almacen/ObtenerMovimientoMercaderiaIncluidoDetallesOrden", params);
        },
        guardarMovimientoMercaderia: function (params) {
            return $http.post(URL_ + "/Almacen/GuardarMovimientoMercaderia", params);
        },
        obtenerOrdenMovimientoMercaderiaIncluidoDetalles: function (params) {
            return $http.post(URL_ + "/Almacen/ObtenerOrdenMovimientoMercaderiaIncluidoDetalles", params);
        },
        obtenerStockDeProducto: function (params) {
            return $http.post(URL_ + "/Almacen/ObtenerStockDeProducto", params);
        },
        obtenerOperacionConDetalleParaTraslado: function (params) {
            return $http.post(URL_ + "/Almacen/ObtenerOperacionConDetalleParaTraslado", params);
        },
        obtenerKardex: function (params) {
            return $http.post(URL_ + "/Almacen/ObtenerKardex", params);
        },
        descargarKardex: function (params) {
            return $http.post(URL_ + "/Almacen/DescargarKardex", params);
        },
        obtenerInventarioValorizado: function (params) {
            return $http.post(URL_ + "/AlmacenReportes/ObtenerInventarioValorizado", params);
        },
        obtenerTipoDeComprobanteGuiaDeRemision: function (params) {
            return $http.post(URL_ + "/Almacen/ObtenerTipoDeComprobanteGuiaDeRemision", params);
        },
        obtenerTipoDeComprobanteNotaDeAlmacen: function (params) {
            return $http.post(URL_ + "/Almacen/ObtenerTipoDeComprobanteNotaDeAlmacen", params);
        },
        generarInventarioLogico: function (params) {
            return $http.post(URL_ + "/Almacen/GenerarInventarioLogico", params);
        },
        obtenerFechaDelUltimoInventarioLogico: function (params) {
            return $http.post(URL_ + "/Almacen/ObtenerFechaDelUltimoInventarioLogico", params);
        },
        obtenerMovimientosDeAlmacen: function (params) {
            return $http.post(URL_ + "/Almacen/ObtenerMovimientosDeAlmacen", params);
        },
        obtenerMovimientoDeAlmacen: function (params) {
            return $http.post(URL_ + "/Almacen/ObtenerMovimientoDeAlmacen", params);
        },
        enviarCorreoElectronicoDeMovimientoDeAlmacen: function (params) {
            return $http.post(URL_ + "/Almacen/EnviarCorreoElectronicoDeMovimientoDeAlmacen", params);
        },
        obtenerOrdenesDeAlmacen: function (params) {
            return $http.post(URL_ + "/Almacen/ObtenerOrdenesDeAlmacen", params);
        },
        obtenerOrdenDeAlmacen: function (params) {
            return $http.post(URL_ + "/Almacen/ObtenerOrdenDeAlmacen", params);
        },
        enviarCorreoElectronicoDeOrdenDeAlmacen: function (params) {
            return $http.post(URL_ + "/Almacen/EnviarCorreoElectronicoDeOrdenDeAlmacen", params);
        },
        obtenerOrdenDeAlmacenParaMovimientoDeAlmacen: function (params) {
            return $http.post(URL_ + "/Almacen/ObtenerOrdenDeAlmacenParaMovimientoDeAlmacen", params);
        },
        obtenerDetalleDeCompraParaOrdenDeAlmacen: function (params) {
            return $http.post(URL_ + "/Almacen/ObtenerDetalleDeCompraParaOrdenDeAlmacen", params);
        },
        guardarOrdenDeAlmacen: function (params) {
            return $http.post(URL_ + "/Almacen/GuardarOrdenDeAlmacen", params);
        },
        guardarMovimientoDeAlmacen: function (params) {
            return $http.post(URL_ + "/Almacen/GuardarMovimientoDeAlmacen", params);
        },
        obtenerGuiasRemision: function (params) {
            return $http.post(URL_ + "/Almacen/ObtenerGuiasRemision", params);
        },
        obtenerGuiaRemision: function (params) {
            return $http.post(URL_ + "/Almacen/ObtenerGuiaRemision", params);
        },
        guardarGuiaRemision: function (params) {
            return $http.post(URL_ + "/Almacen/GuardarGuiaRemision", params);
        },
        enviarCorreoElectronicoConDocumento: function (params) {
            return $http.post(URL_ + "/Almacen/EnviarCorreoElectronicoConDocumento", params);
        },
        descargarDocumento: function (params) {
            return $http.post(URL_ + "/Almacen/DescargarDocumento", params);
        },
        obtenerTiposDeComprobanteParaGuiaRemision: function (params) {
            return $http.post(URL_ + "/Almacen/ObtenerTiposDeComprobanteParaGuiaRemision", params);
        },
        obtenerTiposDeComprobanteParaGuiaRemisionNotaAlmacen: function (params) {
            return $http.post(URL_ + "/Almacen/ObtenerTiposDeComprobanteParaGuiaRemisionNotaAlmacen", params);
        },
        invalidarGuiaRemision: function (params) {
            return $http.post(URL_ + "/Almacen/InvalidarGuiaRemision", params);
        },
        obtenerParametrosParaRegistradorGuiaRemision: function (params) {
            return $http.post(URL_ + "/Almacen/ObtenerParametrosParaRegistradorGuiaRemision", params);
        },
        enviarGuiasRemision: function (params) {
            return $http.post(URL_ + "/Almacen/EnviarGuiasRemision", params);
        },
        consultarEnvioGuiasRemision: function (params) {
            return $http.post(URL_ + "/Almacen/ConsultarEnvioGuiasRemision", params);
        },
    };
});
app.factory('ordenAlmacenService', function ($http) {
    return {
        obtenerOrdenesAlmacen: function (params) {
            return $http.post(URL_ + "/OrdenAlmacen/ObtenerOrdenesAlmacen", params);
        },
        obtenerOrdenAlmacen: function (params) {
            return $http.post(URL_ + "/OrdenAlmacen/ObtenerOrdenAlmacen", params);
        },
        obtenerRegistroMovimientoOrdenAlmacen: function(params) {
            return $http.post(URL_ + "/OrdenAlmacen/ObtenerRegistroMovimientoOrdenAlmacen", params);
        },
        guardarMovimientoOrdenAlmacen: function (params) {
            return $http.post(URL_ + "/OrdenAlmacen/GuardarMovimientoOrdenAlmacen", params);
        },
        invalidarMovimientoOrdenAlmacen: function (params) {
            return $http.post(URL_ + "/OrdenAlmacen/InvalidarMovimientoOrdenAlmacen", params);
        },

    };
});
app.factory('precioService', function ($http) {
    return {
        obtenerPrecios: function (params) {
            return $http.post(URL_ + "/Precio/ObtenerPrecios", params);
        },
        obtenerDescuentos: function (params) {
            return $http.post(URL_ + "/Precio/ObtenerDescuentos", params);
        },
        obtenerBonificaciones: function (params) {
            return $http.post(URL_ + "/Precio/ObtenerBonificaciones", params);
        },
        guardarPrecio: function (params) {
            return $http.post(URL_ + "/Precio/GuardarPrecio", params);
        },
        guardarDescuento: function (params) {
            return $http.post(URL_ + "/Precio/GuardarDescuento", params);
        },
        guardarBonificacion: function (params) {
            return $http.post(URL_ + "/Precio/GuardarBonificacion", params);
        },
        cargarPrecio: function (params) {
            return $http.post(URL_ + "/Precio/CargarPrecio", params);
        },
        cargarDescuento: function (params) {
            return $http.post(URL_ + "/Precio/CargarDescuento", params);
        },
        cargarBonificacion: function (params) {
            return $http.post(URL_ + "/Precio/CargarBonificacion", params);
        },
        caducarPrecio: function (params) {
            return $http.post(URL_ + "/Precio/CaducarPrecio", params);
        },
        caducarDescuento: function (params) {
            return $http.post(URL_ + "/Precio/CaducarDescuento", params);
        },
        caducarBonificacion: function (params) {
            return $http.post(URL_ + "/Precio/CaducarBonificacion", params);
        },
        obtenerTarifasConPrecioMercaderiaUnica: function (params) {
            return $http.post(URL_ + "/Precio/ObtenerTarifasConPrecioMercaderiaUnica", params);
        },
        guardarTarifasConPrecioMercaderiaUnica: function (params) {
            return $http.post(URL_ + "/Precio/GuardarTarifasConPrecioMercaderiaUnica", params);
        },
        obtenerPreciosDeConceptoNegocio: function (params) {
            return $http.post(URL_ + "/Precio/ObtenerPreciosDeConceptoNegocio", params);
        },
        obtenerPrecioConceptoUnico: function (params) {
            return $http.post(URL_ + "/Precio/ObtenerPrecioConceptoUnico", params);
        },

    };
});

app.factory('ventaService', function ($http) {
    return {
        obtenerModalidadesGenerico: function (params) {
            return $http.post(URL_ + "/Venta/ObtenerModalidadesGenerico", params);
        },
        obtenerVentas: function (params) {
            return $http.post(URL_ + "/Venta/ObtenerVentas", params);
        },
        obtenerResumenesVentas: function (params) {
            return $http.post(URL_ + "/Venta/ObtenerResumenesVentas", params);
        },
        obtenerOrdenesDeVentas: function (params) {
            return $http.post(URL_ + "/Venta/ObtenerOrdenesDeVentas", params);
        },
        confirmarYPagarOrdenVenta: function (params) {
            return $http.post(URL_ + "/Venta/ConfirmarYPagarOrdenVenta", params);
        },
        guardarVenta: function (params) {
            return $http.post(URL_ + "/Venta/GuardarVentaPorMostrador", params);
        },
        guardarVentaCorporativa: function (params) {
            return $http.post(URL_ + "/Venta/GuardarVentaCorporativa", params);
        },
        guardarVentaPorContingencia: function (params) {
            return $http.post(URL_ + "/Venta/GuardarVentaPorContingencia", params);
        },
        obtenerImagenVenta: function (params) {
            return $http.post(URL_ + "/Venta/ObtenerImagenVenta", params);
        },
        cargarOperacionesDeVenta: function (params) {
            return $http.post(URL_ + "/Venta/CargarOperacionesDeVenta", params);
        },

        obtenerParametrosDeFacturacion: function (params) {
            return $http.post(URL_ + "/Venta/ObtenerParametrosDeFacturacion", params);
        },

        obtenerParametrosParaTrazaDePago: function (params) {
            return $http.post(URL_ + "/Venta/ObtenerParametrosParaTrazaDePago", params);
        },
        obtenerMediosDePagoVenta: function (params) {
            return $http.post(URL_ + "/Venta/ObtenerMediosDePagoVenta", params);
        },
        obtenerSeriesConTipoComprobanteParaVenta: function (params) {
            return $http.post(URL_ + "/Venta/ObtenerSeriesConTipoComprobanteParaVenta", params);
        },
        obtenerTiposDeComprobanteParaVenta: function (params) {
            return $http.post(URL_ + "/Venta/ObtenerTiposDeComprobanteParaVenta", params);
        },
        obtenerTiposDeComprobanteParaVentasYSusNotas: function (params) {
            return $http.post(URL_ + "/Venta/ObtenerTiposDeComprobanteParaVentasYSusNotas", params);
        },
        obtenerTiposDeComprobanteParaVentasPorContingencia: function (params) {
            return $http.post(URL_ + "/Venta/ObtenerTiposDeComprobanteParaVentasPorContingencia", params);
        },
        obtenerTiposDeComprobanteParaReporteDeVenta: function (params) {
            return $http.post(URL_ + "/Venta/ObtenerTiposDeComprobanteParaReporteDeVenta", params);
        },
        obtenerTiposDeComprobanteParaAnularVenta: function (params) {
            return $http.post(URL_ + "/Venta/ObtenerTiposDeComprobanteParaAnularVenta", params);
        },
        obtenerTiposDeComprobanteParaDescuentoSobreVenta: function (params) {
            return $http.post(URL_ + "/Venta/ObtenerTiposDeComprobanteParaDescuentoSobreVenta", params);
        },
        obtenerTiposDeComprobanteParaRecargoSobreVenta: function (params) {
            return $http.post(URL_ + "/Venta/ObtenerTiposDeComprobanteParaRecargoSobreVenta", params);
        },
        obtenerTiposDeComprobanteParaClientes: function (params) {
            return $http.post(URL_ + "/Venta/ObtenerTiposDeComprobanteParaClientes", params);
        },
        obtenerProductosVenta: function (params) {
            return $http.post(URL_ + "/Venta/ObtenerProductosVenta", params);
        },
        obtenerProductosVentaPorConceptos: function (params) {
            return $http.post(URL_ + "/Venta/ObtenerProductosVentaPorConceptos", params);
        },
        obtenerOrdenVenta: function (params) {
            return $http.post(URL_ + "/Venta/ObtenerOrdenVenta", params);
        },
        registrarOrdenVenta: function (params) {
            return $http.post(URL_ + "/Venta/RegistrarOrdenVenta", params);
        },
        obtenerClientes: function (params) {
            return $http.post(URL_ + "/Cliente/ObtenerClientes", params);
        },
        anularOrdenDeVentaRegistrada: function (params) {
            return $http.post(URL_ + "/Venta/AnularOrdenDeVentaRegistrada", params);
        },
        obtenerMercaderiaPorCodigoBarra: function (params) {
            return $http.post(URL_ + "/Venta/ObtenerMercaderiaPorCodigoBarra", params);
        },
        obtenerTiposDeComprobanteParaAnulacionVenta: function (params) {
            return $http.post(URL_ + "/Venta/ObtenerTiposDeComprobanteParaAnulacionVenta", params);
        },
        obtenerOrdenVentaYDetallesParaAnulacionDeVenta: function (params) {
            return $http.post(URL_ + "/Venta/ObtenerOrdenVentaYDetallesParaAnulacionDeVenta", params);
        },
        invalidarVenta: function (params) {
            return $http.post(URL_ + "/venta/InvalidarVenta", params);
        },
        anularVenta: function (params) {
            return $http.post(URL_ + "/Venta/AnularVenta", params);
        },
        guardarVentasYCobrosPorVendedor: function (params) {
            return $http.post(URL_ + "/Venta/GuardarVentasYCobrosPorVendedor", params);
        },
        obtenerDeudasMasivas: function (params) {
            return $http.post(URL_ + "/Venta/ObtenerDeudasMasivas", params);
        },
        obtenerVentasYCobrosMasivos: function (params) {
            return $http.post(URL_ + "/Venta/ObtenerVentasYCobrosMasivos", params);
        },
        obtenerVentasYCobrosMasivo: function (params) {
            return $http.post(URL_ + "/Venta/ObtenerVentasYCobrosMasivo", params);
        },
        guardarVentaMasiva: function (params) {
            return $http.post(URL_ + "/Venta/guardarVentaMasiva", params);
        },
        obtenerTiposDeComprobanteConSeriesAutonumericasParaVenta: function (params) {
            return $http.post(URL_ + "/Venta/ObtenerTiposDeComprobanteConSeriesAutonumericasParaVenta", params);
        },
        obtenerTiposDeComprobanteConSeriesAutonumericasParaVentaExcluidoFactura: function (params) {
            return $http.post(URL_ + "/Venta/ObtenerTiposDeComprobanteConSeriesAutonumericasParaVentaExcluidoFactura", params);
        },
        obtenerVisualizadorDeComprobanteDeOrdenDeVenta: function (params) {
            return $http.post(URL_ + "/Venta/ObtenerVisualizadorDeComprobanteDeOrdenDeVenta", params);
        },
        obtenerFormatosDeImpresion: function (params) {
            return $http.post(URL_ + "/Venta/ObtenerFormatosDeImpresion", params);
        },
        obtenerHtmlVenta: function (params) {
            return $http.post(URL_ + "/Venta/ObtenerHtmlVenta", params);
        },
        enviarCorreoElectronicoConDocumento: function (params) {
            return $http.post(URL_ + "/Venta/EnviarCorreoElectronicoConDocumento", params);
        },
        descargarDocumento: function (params) {
            return $http.post(URL_ + "/Venta/DescargarDocumento", params);
        },
        obtenerDocumentoParaOperacionesEnVenta: function (params) {
            return $http.post(URL_ + "/Venta/ObtenerDocumentoParaOperacionesEnVenta", params);
        },
        obtenerTiposDeComprobanteParaNotaDeDebitoCreditoDeVenta: function (params) {
            return $http.post(URL_ + "/Venta/ObtenerTiposDeComprobanteParaNotaDeDebitoCreditoDeVenta", params);
        },
        guardarNotaDeDebitoCreditoDeVenta: function (params) {
            return $http.post(URL_ + "/Venta/GuardarNotaDeDebitoCreditoDeVenta", params);
        },
        obtenerHtmlVentaYGuiaDeRemision: function (params) {
            return $http.post(URL_ + "/Venta/ObtenerHtmlVentaYGuiaDeRemision", params);
        },
        guardarVentaPorMostradorIntegradoModoCaja: function (params) {
            return $http.post(URL_ + "/Venta/GuardarVentaPorMostradorIntegradoModoCaja", params);
        },
        obtenerClientePorDefecto: function (params) {
            return $http.post(URL_ + "/Venta/ObtenerClientePorDefecto", params);
        },
        registrarCanjeDeComprobante: function (params) {
            return $http.post(URL_ + "/Venta/RegistrarCanjeDeComprobante", params);
        },
        obtenerFormatosDeImpresionSolo80mm: function (params) {
            return $http.post(URL_ + "/Venta/ObtenerFormatosDeImpresionSolo80mm", params);
        },
        obtenerPuntosDeCliente: function (params) {
            return $http.post(URL_ + "/Venta/ObtenerPuntosDeCliente", params);
        },
        obtenerOrdenVentaParaClonar: function (params) {
            return $http.post(URL_ + "/Venta/ObtenerOrdenVentaParaClonar", params);
        },
        obtenerParametrosParaRegistroDetalles: function (params) {
            return $http.post(URL_ + "/Venta/ObtenerParametrosParaRegistroDetalles", params);
        },
        obtenerParametrosParaPago: function (params) {
            return $http.post(URL_ + "/Venta/ObtenerParametrosParaPago", params);
        },
        obtenerDetalleParaNotaDebitoCredito: function (params) {
            return $http.post(URL_ + "/Venta/ObtenerDetalleParaNotaDebitoCredito", params);
        },
        obtenerParametrosParaNota: function (params) {
            return $http.post(URL_ + "/Venta/ObtenerParametrosParaNota", params);
        },
    };
});

app.factory('compraService', function ($http) {
    return {
        obtenerCompras: function (params) {
            return $http.post(URL_ + "/Compra/ObtenerCompras", params);
        },
        guardarCompra: function (params) {
            return $http.post(URL_ + "/Compra/GuardarCompra", params);
        },
        numeroComprobanteEsValido: function (params) {
            return $http.post(URL_ + "/Compra/NumeroComprobanteEsValido", params);
        },
        obtenerProveedores: function (params) {
            return $http.post(URL_ + "/Proveedor/ObtenerProveedores", params);
        },
        obtenerTiposDeComprobanteParaCompra: function (params) {
            return $http.post(URL_ + "/Compra/ObtenerTiposDeComprobanteParaCompra", params);
        },
        obtenerTiposDeComprobanteParaAnulacionCompra: function (params) {
            return $http.post(URL_ + "/Compra/ObtenerTiposDeComprobanteParaAnulacionCompra", params);
        },
        obtenerDocumentoDeCompra: function (params) {
            return $http.post(URL_ + "/Compra/ObtenerDocumentoDeCompra", params);
        },
        obtenerOrdenDeCompraYDetalles: function (params) {
            return $http.post(URL_ + "/Compra/ObtenerOrdenCompraYDetalles", params);
        },
        obtenerOrdenCompraYDetallesOrden: function (params) {
            return $http.post(URL_ + "/Compra/ObtenerOrdenCompraYDetallesOrden", params);
        },
        invalidarCompra: function (params) {
            return $http.post(URL_ + "/Compra/InvalidarCompra", params);
        },
        anularCompra: function (params) {
            return $http.post(URL_ + "/Compra/AnularCompra", params);
        },
        invalidarAnulacionDeCompra: function (params) {
            return $http.post(URL_ + "/Compra/InvalidarAnulacionDeCompra", params);
        },
        obtenerCompraCorporativa: function (params) {
            return $http.post(URL_ + "/Compra/ObtenerCompraCorporativa", params);
        },
        guardarCompraCorporativa: function (params) {
            return $http.post(URL_ + "/Compra/GuardarCompraCorporativa", params);
        },
        cargarCompraCorporativaYDetallesAEditar: function (params) {
            return $http.post(URL_ + "/Compra/CargarCompraCorporativaYDetallesAEditar", params);
        },
        confirmarCompraCorporativa: function (params) {
            return $http.post(URL_ + "/Compra/ConfirmarCompraCorporativa", params);
        },
        registrarComprobanteCompraCorporativa: function (params) {
            return $http.post(URL_ + "/Compra/RegistrarComprobanteCompraCorporativa", params);
        },
        confirmarCompra: function (params) {
            return $http.post(URL_ + "/Compra/ConfirmarCompra", params);
        },
        obtenerDocumentoParaOperacionesEnCompra: function (params) {
            return $http.post(URL_ + "/Compra/ObtenerDocumentoParaOperacionesEnCompra", params);
        },
        obtenerTiposDeComprobanteParaNotaDeDebitoCreditoDeCompra: function (params) {
            return $http.post(URL_ + "/Compra/ObtenerTiposDeComprobanteParaNotaDeDebitoCreditoDeCompra", params);
        },
        guardarNotaDeDebitoCreditoDeCompra: function (params) {
            return $http.post(URL_ + "/Compra/GuardarNotaDeDebitoCreditoDeCompra", params);
        },
    };
});

app.factory('productoService', function ($http) {
    return {
        guardarProducto: function (params) {
            return $http.post(URL_ + "/Concepto/GuardarProducto", params);
        },
        verMercaderias: function (params) {
            return $http.post(URL_ + "/Concepto/ObtenerMercaderias", params);
        },
        obtenerMercaderiasPorConceptoBasicoGenerico: function (params) {
            return $http.post(URL_ + "/Concepto/ObtenerMercaderiasPorConceptoBasicoGenerico", params);
        },
        verMercaderiasFiltradas: function (params) {
            return $http.post(URL_ + "/Concepto/ObtenerMercaderiasFiltradas", params);
        },
        obtenerMercaderiasFiltradasPorConceptoBasicoYCaracteristicas: function (params) {
            return $http.post(URL_ + "/Concepto/ObtenerMercaderiasFiltradasPorConceptoBasicoYCaracteristicas", params);
        },
        cargarProducto: function (params) {
            return $http.post(URL_ + "/Concepto/ObtenerProducto", params);
        },
        obtenerMercaderiasPorConceptoBasicoIncluyendoStockYPrecios: function (params) {
            return $http.post(URL_ + "/Concepto/ObtenerMercaderiasPorConceptoBasicoIncluyendoStockYPrecios", params);
        },
        obtenerMercaderiasPorConceptoBasicoIncluyendoStockYPreciosVigentes: function (params) {
            return $http.post(URL_ + "/Concepto/ObtenerMercaderiasPorConceptoBasicoIncluyendoStockYPreciosVigentes", params);
        },
        obtenerMercaderiaPorCodigoBarraIncluyendoStockYPrecios: function (params) {
            return $http.post(URL_ + "/Concepto/ObtenerMercaderiaPorCodigoBarraIncluyendoStockYPrecios", params);
        },
        eliminarMercaderia: function (params) {
            return $http.post(URL_ + "/Concepto/EliminarMercaderia", params);
        },
        obtenerConceptosNegociosComerciales: function (params) {
            return $http.post(URL_ + "/Concepto/ObtenerConceptosNegociosComerciales", params);
        },
        obtenerConceptosNegociosComercialesIncluyendoStockYPrecios: function (params) {
            return $http.post(URL_ + "/Concepto/ObtenerConceptosNegociosComercialesIncluyendoStockYPrecios", params);
        },


    };
});
app.factory('actorComercialService', function ($http) {
    return {
        resolverActorComercialPorDocumentoDeIdentidad: function (params) {
            return $http.post(URL_ + "/ActorComercial/ResolverActorComercialPorDocumentoDeIdentidad", params);
        },
        resolverObtenerActorComercial: function (params) {
            return $http.post(URL_ + "/ActorComercial/ResolverObtenerActorComercial", params);
        },
        guardarActorComercial: function (params) {
            return $http.post(URL_ + "/ActorComercial/GuardarActorComercial", params);
        },
        obtenerParametrosParaRegistroDeActorComercial: function (params) {
            return $http.post(URL_ + "/ActorComercial/ObtenerParametrosParaRegistroDeActorComercial", params);
        },
        obtenerParametrosParaSelectorDeActorComercial: function (params) {
            return $http.post(URL_ + "/ActorComercial/ObtenerParametrosParaSelectorDeActorComercial", params);
        },
        obtenerActorComercialPorId: function (params) {
            return $http.post(URL_ + "/ActorComercial/ObtenerActorComercialPorId", params);
        },
        obtenerActoresComercialesPorCentroDeAtencion: function (params) {
            return $http.post(URL_ + "/ActorComercial/ObtenerActoresComercialesPorCentroDeAtencion", params);
        },
        obtenerGruposActoresComerciales: function (params) {
            return $http.post(URL_ + "/ActorComercial/ObtenerGruposActoresComerciales", params);
        },
        obtenerGruposActoresComercialesPorRol: function (params) {
            return $http.post(URL_ + "/ActorComercial/ObtenerGruposActoresComercialesPorRol", params);
        },
        obtenerActoresComercialesDeGrupoActoresComercialesPorRol: function (params) {
            return $http.post(URL_ + "/ActorComercial/ObtenerActoresComercialesDeGrupoActoresComercialesPorRol", params);
        },
        obtenerGruposActoresComercialesPorRolDeActorComercial: function (params) {
            return $http.post(URL_ + "/ActorComercial/ObtenerGruposActoresComercialesPorRolDeActorComercial", params);
        },
    };
});
app.factory('clienteService', function ($http) {
    return {
        listarClientes: function (params) {
            return $http.post(URL_ + "/Cliente/ListarClientes", params);
        },
        obtenerClientesGenerico: function (params) {
            return $http.post(URL_ + "/Cliente/ObtenerClientesGenerico", params);
        },
        obtenerClientesGenericoSinParentRol: function (params) {
            return $http.post(URL_ + "/Cliente/ObtenerClientesGenericoSinParentRol", params);
        },
        guardarCliente: function (params) {
            return $http.post(URL_ + "/Cliente/GuardarCliente", params);
        },
        eliminarCliente: function (params) {
            return $http.post(URL_ + "/Cliente/EliminarCliente", params);
        },
        cargarCliente: function (params) {
            return $http.post(URL_ + "/Cliente/CargarCliente", params);
        },
        validar: function (params) {
            return $http.post(URL_ + "/Cliente/Validar", params);
        },
        validarClienteYRuc: function (params) {
            return $http.post(URL_ + "/Cliente/ValidarClienteYRuc", params);
        },
        guardarCarteraDeClientes: function (params) {
            return $http.post(URL_ + "/Cliente/GuardarCarteraDeClientes", params);
        },
        obtenerClientesParaVentasYCobrosMasiva: function (params) {
            return $http.post(URL_ + "/Cliente/ObtenerClientesParaVentasYCobrosMasiva", params);
        },
        obtenerDireccionCliente: function (params) {
            return $http.post(URL_ + "/Cliente/ObtenerDireccionCliente", params);
        },
        obtenerCarterasDeClientes: function (params) {
            return $http.post(URL_ + "/Cliente/ObtenerCarterasDeClientes", params);
        },
        obtenerCarteraDeClientes: function (params) {
            return $http.post(URL_ + "/Cliente/ObtenerCarteraDeClientes", params);
        },
        obtenerCarteraDeClientesAEditar: function (params) {
            return $http.post(URL_ + "/Cliente/ObtenerCarteraDeClientesAEditar", params);
        },
        obtenerUbigeoDireccionCliente: function (params) {
            return $http.post(URL_ + "/Cliente/ObtenerUbigeoDireccionCliente", params);
        },
        obtenerDetalleDireccionCliente: function (params) {
            return $http.post(URL_ + "/Cliente/ObtenerDetalleDireccionCliente", params);
        },
        ObtenerUbigeoDireccionTercero: function (params) {
            return $http.post(URL_ + "/Cliente/ObtenerUbigeoDireccionTercero", params);
        },
        ObtenerDetalleDireccionTercero: function (params) {
            return $http.post(URL_ + "/Cliente/ObtenerDetalleDireccionTercero", params);
        },
        resolverClientePorDni: function (params) {
            return $http.post(URL_ + "/Cliente/ResolverClientePorDni", params);
        },
        obtenerClientesDeCartera: function (params) {
            return $http.post(URL_ + "/Cliente/ObtenerClientesDeCartera", params);
        },
        obtenerUltimaPlacaDeCliente: function (params) {
            return $http.post(URL_ + "/Cliente/ObtenerUltimaPlacaDeCliente", params);
        },
        guardarGrupoClientes: function (params) {
            return $http.post(URL_ + "/Cliente/GuardarGrupoClientes", params);
        },
        obtenerGruposClientes: function (params) {
            return $http.post(URL_ + "/Cliente/ObtenerGruposClientes", params);
        },
        obtenerGrupoClientes: function (params) {
            return $http.post(URL_ + "/Cliente/ObtenerGrupoClientes", params);
        },
        darBajaGrupoClientes: function (params) {
            return $http.post(URL_ + "/Cliente/DarBajaGrupoClientes", params);
        },
    };
});

app.factory('empleadoService', function ($http) {
    return {
        listarEmpleados: function (params) {
            return $http.post(URL_ + "/Empleado/ListarEmpleados", params);
        },
        guardarEmpleado: function (params) {
            return $http.post(URL_ + "/Empleado/GuardarEmpleado", params);
        },
        eliminarEmpleado: function (params) {
            return $http.post(URL_ + "/Empleado/EliminarEmpleado", params);
        },
        cargarEmpleado: function (params) {
            return $http.post(URL_ + "/Empleado/CargarEmpleado", params);
        },
        validar: function (params) {
            return $http.post(URL_ + "/Empleado/Validar", params);
        },
        obtenerEmpleadosGenerico: function (params) {
            return $http.post(URL_ + "/Empleado/ObtenerEmpleadosGenerico", params);
        },
        asignarUsuarioEmpleado: function (params) {
            return $http.post(URL_ + "/Empleado/AsignarUsuarioEmpleado", params);
        },
        obtenerEmpleadosConIdUsuario: function (params) {
            return $http.post(URL_ + "/Empleado/ObtenerEmpleadosConIdUsuario", params);
        },
        obtenerVendedoresConIdUsuario: function (params) {
            return $http.post(URL_ + "/Empleado/ObtenerVendedoresConIdUsuario", params);
        },
        obtenerEmpleadosConRolCajero: function (params) {
            return $http.post(URL_ + "/Empleado/ObtenerEmpleadosConRolCajero", params);
        },
        obtenerEmpleadosConRolVendedor: function (params) {
            return $http.post(URL_ + "/Empleado/ObtenerEmpleadosConRolVendedor", params);
        },
        obtenerEmpleadosConRolAlmacenero: function (params) {
            return $http.post(URL_ + "/Empleado/ObtenerEmpleadosConRolAlmacenero", params);
        },
        obtenerEmpleados: function (params) {
            return $http.post(URL_ + "/Empleado/ObtenerEmpleados", params);
        },
        guardarTurno: function (params) {
            return $http.post(URL_ + "/Empleado/GuardarTurno", params);
        },
        obtenerTurno: function (params) {
            return $http.post(URL_ + "/Empleado/ObtenerTurno", params);
        },
        obtenerTurnosBandeja: function (params) {
            return $http.post(URL_ + "/Empleado/ObtenerTurnosBandeja", params);
        },
        obtenerEmpleadosConRolComprador: function (params) {
            return $http.post(URL_ + "/Empleado/ObtenerEmpleadosConRolComprador", params);
        },
        obtenerEmpleadosConRolVendedorVigentesConCodigo: function (params) {
            return $http.post(URL_ + "/Empleado/ObtenerEmpleadosConRolVendedorVigentesConCodigo", params);
        },
        obtenerEmpleadosConRolCompradorVigentesConCodigo: function (params) {
            return $http.post(URL_ + "/Empleado/ObtenerEmpleadosConRolCompradorVigentesConCodigo", params);
        },
        obtenerEmpleadosConRolCajeroVigentesConCodigo: function (params) {
            return $http.post(URL_ + "/Empleado/ObtenerEmpleadosConRolCajeroVigentesConCodigo", params);
        },
        obtenerEmpleadosConRolAlmaceneroVigentesConCodigo: function (params) {
            return $http.post(URL_ + "/Empleado/ObtenerEmpleadosConRolAlmaceneroVigentesConCodigo", params);
        },

    };
});

app.factory('adminService', function ($http) {
    return {
        listarUsuarios: function (params) {
            return $http.post(URL_ + "/Admin/ListarUsuarios", params);
        },
        listarRoles: function (params) {
            return $http.post(URL_ + "/Admin/ListarRoles", params);
        },
        eliminarUsuario: function (params) {
            return $http.post(URL_ + "/Admin/DeleteUser", params);
        },
        eliminarRol: function (params) {
            return $http.post(URL_ + "/Admin/DeleteUserRole", params);
        },
        obtenerRoles: function (params) {
            return $http.post(URL_ + "/Admin/GetAllRolesAsSelectList", params);
        },
        guardarUsuario: function (params) {
            return $http.post(URL_ + "/Admin/GuardarUsuario", params);
        },
        guardarRol: function (params) {
            return $http.post(URL_ + "/Admin/GuardarRol", params);
        }
    };
});

app.factory('proveedorService', function ($http) {
    return {
        listarProveedores: function (params) {
            return $http.post(URL_ + "/Proveedor/ListarProveedores", params);
        },
        guardarProveedor: function (params) {
            return $http.post(URL_ + "/Proveedor/GuardarProveedor", params);
        },
        eliminarProveedor: function (params) {
            return $http.post(URL_ + "/Proveedor/EliminarProveedor", params);
        },
        cargarProveedor: function (params) {
            return $http.post(URL_ + "/Proveedor/CargarProveedor", params);
        },
        validar: function (params) {
            return $http.post(URL_ + "/Proveedor/Validar", params);
        },
        obtenerDireccionProveedor: function (params) {
            return $http.post(URL_ + "/Proveedor/ObtenerDireccionProveedor", params);
        },
        validarProveedorYRuc: function (params) {
            return $http.post(URL_ + "/Proveedor/ValidarProveedorYRuc", params);
        },
        obtenerUbigeoDireccionProveedor: function (params) {
            return $http.post(URL_ + "/Proveedor/ObtenerUbigeoDireccionProveedor", params);
        },
        obtenerDetalleDireccionProveedor: function (params) {
            return $http.post(URL_ + "/Proveedor/ObtenerDetalleDireccionProveedor", params);
        },
        obtenerProveedores: function (params) {
            return $http.post(URL_ + "/Proveedor/ObtenerProveedores", params);
        },
        obtenerProveedoresGenerico: function (params) {
            return $http.post(URL_ + "/Proveedor/ObtenerProveedoresGenerico", params);
        },

    };
});

app.factory('librosElectronicosService', function ($http) {
    return {
        obtenerPeriodosDeLibrosElectronicos: function (params) {
            return $http.post(URL_ + "/LibrosElectronicos/ObtenerPeriodosDeLibrosElectronicos", params);
        },
        eliminarLibrosElectronicos: function (params) {
            return $http.post(URL_ + "/LibrosElectronicos/EliminarLibrosElectronicos", params);
        },
        generarLibrosElectronicos: function (params) {
            return $http.post(URL_ + "/LibrosElectronicos/GenerarLibrosElectronicos", params);
        },

    };
});

app.factory('permisoService', function ($http) {
    return {

        obtenerRolAccion: function (params) {
            return $http.post(URL_ + "/Permiso/ObtenerRolAccion", params);
        },
        obtenerEstadoAccion: function (params) {
            return $http.post(URL_ + "/Permiso/ObtenerEstadoAccion", params);
        },
        guardarAcciones: function (params) {
            return $http.post(URL_ + "/Permiso/GuardarAcciones", params);
        },
    };
});

app.factory('gastoService', function ($http) {
    return {
        guardarGasto: function (params) {
            return $http.post(URL_ + "/Gasto/GuardarGasto", params);
        },
        obtenerGastos: function (params) {
            return $http.post(URL_ + "/Gasto/ObtenerGastos", params);
        },
        obtenerGasto: function (params) {
            return $http.post(URL_ + "/Gasto/ObtenerGasto", params);
        },
        invalidarGasto: function (params) {
            return $http.post(URL_ + "/Gasto/InvalidarGasto", params);
        },
        obtenerTiposDeComprobanteParaGasto: function (params) {
            return $http.post(URL_ + "/Gasto/ObtenerTiposDeComprobanteParaGasto", params);
        },

    };
});

app.factory('finanzaService', function ($http) {
    return {
        guardarIngresoEgresoVarios: function (params) {
            return $http.post(URL_ + "/Finanza/GuardarIngresoEgresoVarios", params);
        },
        obtenerTiposDeComprobanteParaIngresoEgresoVarios: function (params) {
            return $http.post(URL_ + "/Finanza/ObtenerTiposDeComprobanteParaIngresoEgresoVarios", params);
        },
        obtenerCuentasPorCobrarOPagar: function (params) {
            return $http.post(URL_ + "/Finanza/ObtenerCuentasPorCobrarOPagar", params);
        },
        cobrarPagarCuota: function (params) {
            return $http.post(URL_ + "/Finanza/CobrarPagarCuota", params);
        },
        pagarCuotaNotaInterna: function (params) {
            return $http.post(URL_ + "/Finanza/PagarCuotaNotaInterna", params);
        },
        pagarCuotaConComprobante: function (params) {
            return $http.post(URL_ + "/Finanza/PagarCuotaConComprobante", params);
        },
        obtnerTipoDeCambioCompraPorFecha: function (params) {
            return $http.post(URL_ + "/Finanza/ObtnerTipoDeCambioCompraPorFecha", params);
        },
        listarPagos: function (params) {
            return $http.post(URL_ + "/Finanza/ListarPagos", params);
        },
        listarNotasDeCredito: function (params) {
            return $http.post(URL_ + "/Finanza/ListarNotasDeCredito", params);
        },
        listarNotasContables: function (params) {
            return $http.post(URL_ + "/Finanza/ListarNotasContables", params);
        },
        listarNotasInternas: function (params) {
            return $http.post(URL_ + "/Finanza/ListarNotasInternas", params);
        },
        obtenerTiposDeComprobanteParaCobrarPagar: function (params) {
            return $http.post(URL_ + "/Finanza/ObtenerTiposDeComprobanteParaCobrarPagar", params);
        },
        obtenerCuotaOperacionDetalle: function (params) {
            return $http.post(URL_ + "/Finanza/ObtenerCuotaOperacionDetalle", params);
        },
        obtenerCobrosPagos: function (params) {
            return $http.post(URL_ + "/Finanza/ObtenerCobrosPagos", params);
        },
        obtenerDocumentoIngresoEgreso: function (params) {
            return $http.post(URL_ + "/Finanza/ObtenerDocumentoIngresoEgreso", params);
        },
        obtenerHtmlIngresoEgreso: function (params) {
            return $http.post(URL_ + "/Finanza/ObtenerHtmlIngresoEgreso", params);
        },
        enviarCorreoElectronicoConDocumentoIngresoEgreso: function (params) {
            return $http.post(URL_ + "/Finanza/EnviarCorreoElectronicoConDocumentoIngresoEgreso", params);
        },
        guardarCuentaBancaria: function (params) {
            return $http.post(URL_ + "/Finanza/GuardarCuentaBancaria", params);
        },
        obtenerCuentasBancarias: function (params) {
            return $http.post(URL_ + "/Finanza/ObtenerCuentasBancarias", params);
        },
        obtenerCuentasBancariasPorEntidadFinanciera: function (params) {
            return $http.post(URL_ + "/Finanza/ObtenerCuentasBancariasPorEntidadFinanciera", params);
        },
        obtenerInicializarCaja: function (params) {
            return $http.post(URL_ + "/Finanza/ObtenerInicializarCaja", params);
        },
        guardarInicializarCaja: function (params) {
            return $http.post(URL_ + "/Finanza/GuardarInicializarCaja", params);
        },
        obtenerCuentasBancariasConEntidadFinancieraConMoneda: function (params) {
            return $http.post(URL_ + "/Finanza/ObtenerCuentasBancariasConEntidadFinancieraConMoneda", params);
        },
        obtenerCuentasPorCobrarOPagarPorGrupos: function (params) {
            return $http.post(URL_ + "/Finanza/ObtenerCuentasPorCobrarOPagarPorGrupos", params);
        },
        invalidarMovimientoEconomico: function (params) {
            return $http.post(URL_ + "/Finanza/InvalidarMovimientoEconomico", params);
        },

    };
});

app.factory('serieComprobanteService', function ($http) {
    return {
        obtenerSeries: function (params) {
            return $http.post(URL_ + "/Contabilidad/ObtenerSeries", params);
        },
    };
});

app.factory('facturacionElectronicaService', function ($http) {
    return {
        listarComprobantesEmitidos: function (params) {
            return $http.post(URL_ + "/FacturacionElectronica/ListarDocumentos", params);
        },
        obtenerDias: function (params) {
            return $http.post(URL_ + "/FacturacionElectronica/ObtenerDias", params);
        },
        resolverResumenDiarioBoletas: function (params) {
            return $http.post(URL_ + "/FacturacionElectronica/ResolverResumenDiarioBoletas", params);
        },
        envioFacturas: function (params) {
            return $http.post(URL_ + "/FacturacionElectronica/EnvioFacturas", params);
        },
        envioNotas: function (params) {
            return $http.post(URL_ + "/FacturacionElectronica/EnvioNotas", params);
        },
        resolverComunicacionBajaFacturas: function (params) {
            return $http.post(URL_ + "/FacturacionElectronica/ResolverComunicacionBajaFacturas", params);
        },
        listarDocumentosEntreFechas: function (params) {
            return $http.post(URL_ + "/FacturacionElectronica/ListarDocumentosEntreFechas", params);
        },
        listarEnviosEntreFechas: function (params) {
            return $http.post(URL_ + "/FacturacionElectronica/ListarEnviosEntreFechas", params);
        },
        transmitirAFacturacionElectronicaManual: function (params) {
            return $http.post(URL_ + "/FacturacionElectronica/TransmitirAFacturacionElectronicaManual", params);
        },
        consultarTickets: function (params) {
            return $http.post(URL_ + "/FacturacionElectronica/ConsultarTickets", params);
        },
        resolverEnviosConExcepcion: function (params) {
            return $http.post(URL_ + "/FacturacionElectronica/ResolverEnviosConExcepcion", params);
        },
        obtenerComprobantesRechazadosPorLimiteDeFecha: function (params) {
            return $http.post(URL_ + "/FacturacionElectronica/ObtenerComprobantesRechazadosPorLimiteDeFecha", params);
        },
        invalidarYReemitirComprobantesRechazadosPorLimiteDeFecha: function (params) {
            return $http.post(URL_ + "/FacturacionElectronica/InvalidarYReemitirComprobantesRechazadosPorLimiteDeFecha", params);
        },
        envioGuiasDeRemision: function (params) {
            return $http.post(URL_ + "/FacturacionElectronica/EnvioGuiasDeRemision", params);
        },
        resolverEnvioRechazado: function (params) {
            return $http.post(URL_ + "/FacturacionElectronica/ResolverEnvioRechazado", params);
        },
        regenerarJsonDocumento: function (params) {
            return $http.post(URL_ + "/FacturacionElectronica/RegenerarJsonDocumento", params);
        },
        enviarDocumentos: function (params) {
            return $http.post(URL_ + "/FacturacionElectronica/EnviarDocumentos", params);
        },
        resolverEnvioPendiente: function (params) {
            return $http.post(URL_ + "/FacturacionElectronica/ResolverEnvioPendiente", params);
        },



    };
});

app.factory('configuracionService', function ($http) {
    return {
        //------------------------------Manejo De Tipos de transaccion------------------------------
        obtenerTiposDeTransaccionBandeja: function (params) {
            return $http.post(URL_ + "/TipoDeTransaccion/ObtenerTiposDeTransaccionBandeja", params);
        },

        obtenerAccionesDeNegocioPorTipoDeTransaccion: function (params) {
            return $http.post(URL_ + "/TipoDeTransaccion/ObtenerAccionesDeNegocioPorTipoDeTransaccion", params);
        },
        obtenerAccionesDeNegocioPorTipoDeTransaccionEditar: function (params) {
            return $http.post(URL_ + "/TipoDeTransaccion/ObtenerAccionesDeNegocioPorTipoDeTransaccionEditar", params);
        },
        obtenerTiposDeTransaccionGenerico: function (params) {
            return $http.post(URL_ + "/TipoDeTransaccion/ObtenerTiposDeTransaccionGenerico", params);
        },
        crearTipoDeTransaccion: function (params) {
            return $http.post(URL_ + "/TipoDeTransaccion/CrearTipoDeTransaccion", params);
        },
        obtenerTipoDeTransaccion: function (params) {
            return $http.post(URL_ + "/TipoDeTransaccion/ObtenerTipoDeTransaccion", params);
        },
        //------------------------------Manejo De Tipos De Comprobante------------------------------
        crearTipoDeComprobante: function (params) {
            return $http.post(URL_ + "/Comprobante/CrearTipoDeComprobante", params);
        },
        obtenerTipoDeComprobante: function (params) {
            return $http.post(URL_ + "/Comprobante/ObtenerTipoDeComprobante", params);
        },
        obtenerSelectoresTiposDeComprobante: function (params) {
            return $http.post(URL_ + "/Comprobante/ObtenerSelectoresTiposDeComprobante", params);
        },
        obtenerTiposDeComprobanteBandeja: function (params) {
            return $http.post(URL_ + "/Comprobante/ObtenerTiposDeComprobanteBandeja", params);
        },
        //------------------------------Manejo de Series De Comprobante------------------------------
        obtenerTiposDeComprobanteGenerico: function (params) {
            return $http.post(URL_ + "/Comprobante/ObtenerTiposDeComprobanteGenerico", params);
        },
        crearSerieDeComprobante: function (params) {
            return $http.post(URL_ + "/Comprobante/CrearSerieDeComprobante", params);
        },
        obtenerSerieDeComprobante: function (params) {
            return $http.post(URL_ + "/Comprobante/ObtenerSerieDeComprobante", params);
        },
        obtenerSeriesDeComprobanteBandeja: function (params) {
            return $http.post(URL_ + "/Comprobante/ObtenerSeriesDeComprobanteBandeja", params);
        },
        //-----------------------------------------------------------------------------------------
        listarConfiguraciones: function (params) {
            return $http.post(URL_ + "/Configuracion/ListarConfiguraciones", params);
        },
        listarParametrosConfiguracion: function (params) {
            return $http.post(URL_ + "/Configuracion/ListarParametrosConfiguracion", params);
        },
        guardarConfiguracion: function (params) {
            return $http.post(URL_ + "/Configuracion/GuardarConfiguracion", params);
        },
        guardarParametroConfiguracion: function (params) {
            return $http.post(URL_ + "/Configuracion/GuardarParametroConfiguracion", params);
        }
    };
});

app.factory('centroDeAtencionService', function ($http) {
    return {

        crearSede: function (params) {
            return $http.post(URL_ + "/CentroDeAtencion/CrearSede", params);
        },
        obtenerSede: function (params) {
            return $http.post(URL_ + "/CentroDeAtencion/ObtenerSedePrincipal", params);
        },
        crearCentroDeAtencion: function (params) {
            return $http.post(URL_ + "/CentroDeAtencion/CrearCentroDeAtencion", params);
        },
        obtenerCentrosDeAtencionBandeja: function (params) {
            return $http.post(URL_ + "/CentroDeAtencion/ObtenerCentrosDeAtencionBandeja", params);
        },
        eliminarCentroDeAtencion: function (params) {
            return $http.post(URL_ + "/CentroDeAtencion/EliminarCentroDeATencion", params);
        },
        crearSucursal: function (params) {
            return $http.post(URL_ + "/CentroDeAtencion/CrearSucursal", params);
        },
        obtenerSucursales: function (params) {
            return $http.post(URL_ + "/CentroDeAtencion/ObtenerSucursales", params);
        },
        obtenerSucursalesBandeja: function (params) {
            return $http.post(URL_ + "/CentroDeAtencion/ObtenerSucursalesBandeja", params);
        },
        obtenerSucursal: function (params) {
            return $http.post(URL_ + "/CentroDeAtencion/ObtenerSucursal", params);
        },
        eliminarSucursal: function (params) {
            return $http.post(URL_ + "/CentroDeAtencion/EliminarSucursal", params);
        },
        obtenerCentroDeAtencion: function (params) {
            return $http.post(URL_ + "/CentroDeAtencion/ObtenerCentroDeAtencion", params);
        },
        obtenerCentrosDeAtencion: function (params) {
            return $http.post(URL_ + "/CentroDeAtencion/ObtenerCentrosDeAtencion", params);
        },
        obtenerCentrosDeAtencionPorEstablecimientoComercial: function (params) {
            return $http.post(URL_ + "/CentroDeAtencion/ObtenerCentrosDeAtencionPorEstablecimientoComercial", params);
        },
        obtenerEstablecimientosComerciales: function (params) {
            return $http.post(URL_ + "/CentroDeAtencion/ObtenerEstablecimientosComerciales", params);
        },
        obtenerCentrosDeAtencionConRolAlmacen: function (params) {
            return $http.post(URL_ + "/CentroDeAtencion/ObtenerCentrosDeAtencionConRolAlmacen", params);
        },
        obtenerCentrosDeAtencionConRolCaja: function (params) {
            return $http.post(URL_ + "/CentroDeAtencion/ObtenerCentrosDeAtencionConRolCaja", params);
        },
        obtenerCentrosDeAtencionConRolPuntoDeVenta: function (params) {
            return $http.post(URL_ + "/CentroDeAtencion/ObtenerCentrosDeAtencionConRolPuntoDeVenta", params);
        },
        obtenerRolesDeCentrosDeAtencion: function (params) {
            return $http.post(URL_ + "/CentroDeAtencion/ObtenerRolesCentrosDeAtencion", params);
        },
        obtenerCentrosDeAtencionParaCarteraDeClientes: function (params) {
            return $http.post(URL_ + "/CentroDeAtencion/ObtenerCentrosDeAtencionParaCarteraDeClientes", params);
        },
        establecerCentrosDeAtencionParaPreciosYStockDeEstablecimientoComercial: function (params) {
            return $http.post(URL_ + "/CentroDeAtencion/EstablecerCentrosDeAtencionParaPreciosYStockDeEstablecimientoComercial", params);
        },
        obtenerCentrosDeAtencionParaPrecios: function (params) {
            return $http.post(URL_ + "/CentroDeAtencion/ObtenerCentrosDeAtencionParaPrecios", params);
        },
        obtenerCentrosDeAtencionPorEstablecimientoComercialParaCarteraDeClientes: function (params) {
            return $http.post(URL_ + "/CentroDeAtencion/ObtenerCentrosDeAtencionPorEstablecimientoComercialParaCarteraDeClientes", params);
        },
        obtenerCentrosDeAtencionConRolPuntoDeVentaVigentesPorEstablecimientoComercial: function (params) {
            return $http.post(URL_ + "/CentroDeAtencion/ObtenerCentrosDeAtencionConRolPuntoDeVentaVigentesPorEstablecimientoComercial", params);
        },
        obtenerCentrosDeAtencionConRolPuntoDeCompraVigentesPorEstablecimientoComercial: function (params) {
            return $http.post(URL_ + "/CentroDeAtencion/ObtenerCentrosDeAtencionConRolPuntoDeCompraVigentesPorEstablecimientoComercial", params);
        },
        obtenerCentrosDeAtencionConRolCajaVigentesPorEstablecimientoComercial: function (params) {
            return $http.post(URL_ + "/CentroDeAtencion/ObtenerCentrosDeAtencionConRolCajaVigentesPorEstablecimientoComercial", params);
        },
        obtenerCentrosDeAtencionConRolAlmacenVigentesPorEstablecimientoComercial: function (params) {
            return $http.post(URL_ + "/CentroDeAtencion/ObtenerCentrosDeAtencionConRolAlmacenVigentesPorEstablecimientoComercial", params);
        },
        obtenerCentrosDeAtencionConRolPuntoDeVentaVigentesConEstablecimientoComercial: function (params) {
            return $http.post(URL_ + "/CentroDeAtencion/ObtenerCentrosDeAtencionConRolPuntoDeVentaVigentesConEstablecimientoComercial", params);
        },
        obtenerCentrosDeAtencionConRolPuntoDeCompraVigentesConEstablecimientoComercial: function (params) {
            return $http.post(URL_ + "/CentroDeAtencion/ObtenerCentrosDeAtencionConRolPuntoDeCompraVigentesConEstablecimientoComercial", params);
        },
        obtenerCentrosDeAtencionConRolCajaVigentesConEstablecimientoComercial: function (params) {
            return $http.post(URL_ + "/CentroDeAtencion/ObtenerCentrosDeAtencionConRolCajaVigentesConEstablecimientoComercial", params);
        },
        obtenerCentrosDeAtencionConRolAlmacenVigentesConEstablecimientoComercial: function (params) {
            return $http.post(URL_ + "/CentroDeAtencion/ObtenerCentrosDeAtencionConRolAlmacenVigentesConEstablecimientoComercial", params);
        },
        obtenerCentrosDeAtencionConRolPuntoDeVentaVigentesConCodigoYEstablecimientoComercial: function (params) {
            return $http.post(URL_ + "/CentroDeAtencion/ObtenerCentrosDeAtencionConRolPuntoDeVentaVigentesConCodigoYEstablecimientoComercial", params);
        },
        obtenerCentrosDeAtencionConRolPuntoDeCompraVigentesConCodigoYEstablecimientoComercial: function (params) {
            return $http.post(URL_ + "/CentroDeAtencion/ObtenerCentrosDeAtencionConRolPuntoDeCompraVigentesConCodigoYEstablecimientoComercial", params);
        },
        obtenerCentrosDeAtencionConRolCajaVigentesConCodigoYEstablecimientoComercial: function (params) {
            return $http.post(URL_ + "/CentroDeAtencion/ObtenerCentrosDeAtencionConRolCajaVigentesConCodigoYEstablecimientoComercial", params);
        },
        obtenerCentrosDeAtencionConRolAlmacenVigentesConCodigoYEstablecimientoComercial: function (params) {
            return $http.post(URL_ + "/CentroDeAtencion/ObtenerCentrosDeAtencionConRolAlmacenVigentesConCodigoYEstablecimientoComercial", params);
        },
        obtenerCentrosDeAtencionConRolPuntoDeVentaVigentesPorEstablecimientosComerciales: function (params) {
            return $http.post(URL_ + "/CentroDeAtencion/ObtenerCentrosDeAtencionConRolPuntoDeVentaVigentesPorEstablecimientosComerciales", params);
        },
        obtenerCentrosDeAtencionConRolPuntoDeCompraVigentesPorEstablecimientosComerciales: function (params) {
            return $http.post(URL_ + "/CentroDeAtencion/ObtenerCentrosDeAtencionConRolPuntoDeCompraVigentesPorEstablecimientosComerciales", params);
        },
        obtenerCentrosDeAtencionConRolCajaVigentesPorEstablecimientosComerciales: function (params) {
            return $http.post(URL_ + "/CentroDeAtencion/ObtenerCentrosDeAtencionConRolCajaVigentesPorEstablecimientosComerciales", params);
        },
        obtenerCentrosDeAtencionConRolAlmacenVigentesPorEstablecimientosComerciales: function (params) {
            return $http.post(URL_ + "/CentroDeAtencion/ObtenerCentrosDeAtencionConRolAlmacenVigentesPorEstablecimientosComerciales", params);
        },
        obtenerCentrosDeAtencionConRolPuntoDeVentaNoVigentes: function (params) {
            return $http.post(URL_ + "/CentroDeAtencion/ObtenerCentrosDeAtencionConRolPuntoDeVentaNoVigentes", params);
        },




    };
});
// Crear Roles de Negocio
app.factory('rolService', function ($http) {
    return {

        crearRol: function (params) {
            return $http.post(URL_ + "/Rol/CrearRol", params);
        },
        obtenerRolDeNegocio: function (params) {
            return $http.post(URL_ + "/Rol/ObtenerRolDeNegocio", params);
        },
        //15/12/18
        obtenerRolesDeNegocioGenerico: function (params) {
            return $http.post(URL_ + "/Rol/ObtenerRolesDeNegocioGenerico", params);
        }
    };
});

app.factory('cotizacionService', function ($http) {
    return {

        obtenerCotizaciones: function (params) {
            return $http.post(URL_ + "/Cotizacion/ObtenerCotizaciones", params);
        },
        guardarCotizacion: function (params) {
            return $http.post(URL_ + "/Cotizacion/GuardarCotizacion", params);
        },
        obtenerTiposDeComprobanteConSeriesAutonumericasParaCotizacion: function (params) {
            return $http.post(URL_ + "/Cotizacion/ObtenerTiposDeComprobanteConSeriesAutonumericasParaCotizacion", params);
        },
        obtenerDocumentoDeCotizacion: function (params) {
            return $http.post(URL_ + "/Cotizacion/ObtenerDocumentoDeCotizacion", params);
        },
        obtenerHtmlDocumentoDeCotizacion: function (params) {
            return $http.post(URL_ + "/Cotizacion/ObtenerHtmlDocumentoDeCotizacion", params);
        },
        enviarCorreoElectronicoConDocumentoDeCotizacion: function (params) {
            return $http.post(URL_ + "/Cotizacion/EnviarCorreoElectronicoConDocumentoDeCotizacion", params);
        },
        obtenerCotizacionParaEditar: function (params) {
            return $http.post(URL_ + "/Cotizacion/ObtenerCotizacionParaEditar", params);
        },

    };
});

app.factory('tipoCambioService', function ($http) {
    return {

        obtenerTiposDeCambioPorAnhoMes: function (params) {
            return $http.post(URL_ + "/TipoCambio/ObtenerTiposDeCambioPorAnhoMes", params);
        },
        obtenerTipoCambioPorFecha: function (params) {
            return $http.post(URL_ + "/TipoCambio/ObtenerTipoCambioPorFecha", params);
        }
    };
});




app.factory('cocheraService', function ($http) {
    return {
        obtenerMovimientosCochera: function (params) {
            return $http.post(URL_ + "/Cochera/ObtenerMovimientosCochera", params);
        },
        registrarIngreso: function (params) {
            return $http.post(URL_ + "/Cochera/RegistrarIngreso", params);
        },
        obtenerVehiculoParaEditarPorPlaca: function (params) {
            return $http.post(URL_ + "/Cochera/ObtenerVehiculoParaEditarPorPlaca", params);
        },
        obtenerVehiculoPorPlaca: function (params) {
            return $http.post(URL_ + "/Cochera/ObtenerVehiculoPorPlaca", params);
        },

        guardarVehiculo: function (params) {
            return $http.post(URL_ + "/Cochera/GuardarVehiculo", params);
        },
        obtenerTiposDeVehiculo: function (params) {
            return $http.post(URL_ + "/Cochera/ObtenerTiposDeVehiculo", params);
        },
        obtenerCocheras: function (params) {
            return $http.post(URL_ + "/Cochera/ObtenerCocheras", params);
        },
        obtenerMarcasDeVehiculo: function (params) {
            return $http.post(URL_ + "/Cochera/ObtenerMarcasDeVehiculo", params);
        },
        obtenerSistemaDePagoParaCochera: function (params) {
            return $http.post(URL_ + "/Cochera/ObtenerSistemaDePagoParaCochera", params);
        },
        obtenerMovimientoParaSalida: function (params) {
            return $http.post(URL_ + "/Cochera/ObtenerMovimientoParaSalida", params);
        },
        registrarSalida: function (params) {
            return $http.post(URL_ + "/Cochera/RegistrarSalida", params);
        }
        ,
        obtenerExoneraciones: function (params) {
            return $http.post(URL_ + "/Cochera/ObtenerExoneraciones", params);
        },
        exonerarVehiculo: function (params) {
            return $http.post(URL_ + "/Cochera/ExonerarVehiculo", params);
        },
        quitarExoneracionAVehiculo: function (params) {
            return $http.post(URL_ + "/Cochera/QuitarExoneracionAVehiculo", params);
        }
    };
});



app.factory('hotelService', function ($http) {
    return {
        obtenerTiposHabitacion: function (params) {
            return $http.post(URL_ + "/Hotel/ObtenerTiposHabitacion", params);
        },
        obtenerReservasBandeja: function (params) {
            return $http.post(URL_ + "/Hotel/ObtenerReservasBandeja", params);
        },
        obtenerHabitacionesDisponibles: function (params) {
            return $http.post(URL_ + "/Hotel/ObtenerHabitacionesDisponibles", params);
        },
        obtenerEstadoHabitaciones: function (params) {
            return $http.post(URL_ + "/Hotel/ObtenerEstadoHabitaciones", params);
        },
        obtenerHabitacionesEnPlanificador: function (params) {
            return $http.post(URL_ + "/Hotel/ObtenerPlanificador", params);
        },
        buscarHabitacionesEnPlanificador: function (params) {
            return $http.post(URL_ + "/Hotel/BuscarHabitacionesEnPlanificador", params);
        },
        obtenerRangoFechaPlanificador: function (params) {
            return $http.post(URL_ + "/Hotel/ObtenerRangoFechaPlanificador", params);
        },
        obtenerHabitacionesBandeja: function (params) {
            return $http.post(URL_ + "/Hotel/ObtenerHabitacionesBandeja", params);
        },
        obtenerAmbientesHotelPorEstablecimiento: function (params) {
            return $http.post(URL_ + "/Hotel/ObtenerAmbientesHotelPorEstablecimiento", params);
        },
        obtenerTipoHabitacionesHotel: function (params) {
            return $http.post(URL_ + "/Hotel/ObtenerTipoHabitacionesHotel", params);
        },
        obtenerTipoCamas: function (params) {
            return $http.post(URL_ + "/Hotel/ObtenerTipoCamas", params);
        },
        guardarComplemento: function (params) {
            return $http.post(URL_ + "/Hotel/GuardarComplemento", params);
        },
        guardarHabitacion: function (params) {
            return $http.post(URL_ + "/Hotel/GuardarHabitacion", params);
        },
        obtenerHabitacion: function (params) {
            return $http.post(URL_ + "/Hotel/ObtenerHabitacion", params);
        },
        cambiarEsVigenteHabitacion: function (params) {
            return $http.post(URL_ + "/Hotel/CambiarEsVigenteHabitacion", params);
        },
        guardarTipoHabitacion: function (params) {
            return $http.post(URL_ + "/Hotel/GuardarTipoHabitacion", params);
        },
        obtenerTipoHabitacionesBandeja: function (params) {
            return $http.post(URL_ + "/Hotel/ObtenerTipoHabitacionesBandeja", params);
        },
        obtenerTiposHabitacionVigentesSimplificado: function (params) {
            return $http.post(URL_ + "/Hotel/ObtenerTiposHabitacionVigentesSimplificado", params);
        },
        obtenerTipoHabitacion: function (params) {
            return $http.post(URL_ + "/Hotel/ObtenerTipoHabitacion", params);
        },
        obtenerParametrosParaRegistradorReserva: function (params) {
            return $http.post(URL_ + "/Hotel/ObtenerParametrosParaRegistradorReserva", params);
        },
        cambiarEsVigenteTipoHabitacion: function (params) {
            return $http.post(URL_ + "/Hotel/CambiarVigenciaDelTipoHabitacion", params);
        },
        guardarAmbiente: function (params) {
            return $http.post(URL_ + "/Hotel/GuardarAmbiente", params);
        },
        cambiarEsVigenteAmbienteHotel: function (params) {
            return $http.post(URL_ + "/Hotel/CambiarVigenciaDelAmbienteHotel", params);
        },
        obtenerAmbientesVigentesPorEstablecimientoSimplificado: function (params) {
            return $http.post(URL_ + "/Hotel/ObtenerAmbientesVigentesPorEstablecimientoSimplificado", params);
        },
        obtenerAtencionMacroHotel: function (params) {
            return $http.post(URL_ + "/Hotel/ObtenerAtencionMacroHotel", params);
        },
        guardarReserva: function (params) {
            return $http.post(URL_ + "/Hotel/GuardarReserva", params);
        },
        obtenerParametrosParaRegistradorHuesped: function (params) {
            return $http.post(URL_ + "/Hotel/ObtenerParametrosParaRegistradorHuesped", params);
        },
        obtenerUltimoMotivoViajeHuesped: function (params) {
            return $http.post(URL_ + "/Hotel/ObtenerUltimoMotivoViajeHuesped", params);
        },
        guardarAnotacion: function (params) {
            return $http.post(URL_ + "/Hotel/GuardarAnotacion", params);
        },
        confirmarAtencionMacro: function (params) {
            return $http.post(URL_ + "/Hotel/ConfirmarAtencionMacro", params);
        },
        confirmarAtencion: function (params) {
            return $http.post(URL_ + "/Hotel/ConfirmarAtencion", params);
        },
        checkInAtencionMacro: function (params) {
            return $http.post(URL_ + "/Hotel/CheckInAtencionMacro", params);
        },
        checkInAtencion: function (params) {
            return $http.post(URL_ + "/Hotel/CheckInAtencion", params);
        },
        checkOutAtencionMacro: function (params) {
            return $http.post(URL_ + "/Hotel/CheckOutAtencionMacro", params);
        },
        checkOutAtencion: function (params) {
            return $http.post(URL_ + "/Hotel/CheckOutAtencion", params);
        },
        anularAtencionMacro: function (params) {
            return $http.post(URL_ + "/Hotel/AnularAtencionMacro", params);
        },
        anularAtencion: function (params) {
            return $http.post(URL_ + "/Hotel/AnularAtencion", params);
        },
        agregarHuesped: function (params) {
            return $http.post(URL_ + "/Hotel/AgregarHuesped", params);
        },
        eliminarHuesped: function (params) {
            return $http.post(URL_ + "/Hotel/EliminarHuesped", params);
        },
        editarResponsableAtencionMacro: function (params) {
            return $http.post(URL_ + "/Hotel/EditarResponsableAtencionMacro", params);
        },
        editarFechaAtencion: function (params) {
            return $http.post(URL_ + "/Hotel/EditarFechaAtencion", params);
        },
        obtenerConfiguracionParaFacturar: function (params) {
            return $http.post(URL_ + "/Hotel/ObtenerConfiguracionParaFacturar", params);
        },
        facturarAtencionMacro: function (params) {
            return $http.post(URL_ + "/Hotel/FacturarAtencionMacro", params);
        },
        facturarAtencion: function (params) {
            return $http.post(URL_ + "/Hotel/FacturarAtencion", params);
        },
        obtenerAtencionDesdeAtencionMacro: function (params) {
            return $http.post(URL_ + "/Hotel/ObtenerAtencionDesdeAtencionMacro", params);
        },
        obtenerAtencionDesdeAtencion: function (params) {
            return $http.post(URL_ + "/Hotel/ObtenerAtencionDesdeAtencion", params);
        },
        obtenerReportePlanificador: function (params) {
            return $http.post(URL_ + "/Hotel/ObtenerReportePlanificador", params);
        },
        obtenerPlanificadorHabitaciones: function (params) {
            return $http.post(URL_ + "/Hotel/ObtenerPlanificadorHabitaciones", params);
        },
        cambiarEnLimpiezaHabitacion: function (params) {
            return $http.post(URL_ + "/Hotel/CambiarEnLimpiezaHabitacion", params);
        },
        obtenerHabitacionDisponible: function (params) {
            return $http.post(URL_ + "/Hotel/ObtenerHabitacionDisponible", params);
        },
        cambiarHabitacionAtencion: function (params) {
            return $http.post(URL_ + "/Hotel/CambiarHabitacionAtencion", params);
        },
        checkInReserva: function (params) {
            return $http.post(URL_ + "/Hotel/CheckInReserva", params);
        },
        cambiarTitularHuesped: function (params) {
            return $http.post(URL_ + "/Hotel/CambiarTitularHuesped", params);
        },
        obtenerConsumos: function (params) {
            return $http.post(URL_ + "/Hotel/ObtenerConsumos", params);
        },
        cambiarVigenciaDelConsumo: function (params) {
            return $http.post(URL_ + "/Hotel/CambiarVigenciaDelConsumo", params);
        },
        obtenerAtencionesEnCheckedInComoHabitaciones: function (params) {
            return $http.post(URL_ + "/Hotel/ObtenerAtencionesEnCheckedInComoHabitaciones", params);
        },
        obtenerConsumoHabitacionAtencion: function (params) {
            return $http.post(URL_ + "/Hotel/ObtenerConsumoHabitacionAtencion", params);
        },
        confirmarConsumo: function (params) {
            return $http.post(URL_ + "/Hotel/ConfirmarConsumo", params);
        },
        invalidarConsumo: function (params) {
            return $http.post(URL_ + "/Hotel/InvalidarConsumo", params);
        },
        obtenerComprobantesAtencionMacro: function (params) {
            return $http.post(URL_ + "/Hotel/ObtenerComprobantesAtencionMacro", params);
        },
        obtenerComprobantesAtencion: function (params) {
            return $http.post(URL_ + "/Hotel/ObtenerComprobantesAtencion", params);
        },
        registrarIncidenteAtencionMacro: function (params) {
            return $http.post(URL_ + "/Hotel/RegistrarIncidenteAtencionMacro", params);
        },
        registrarIncidenteAtencion: function (params) {
            return $http.post(URL_ + "/Hotel/RegistrarIncidenteAtencion", params);
        },
        
    };
})

app.factory('restauranteService', function ($http) {
    return {
        obtenerCategorias: function (params) {
            return $http.post(URL_ + "/Restaurante/ObtenerCategorias", params);
        },
        obtenerMesasDeAmbiente: function (params) {
            return $http.post(URL_ + "/Restaurante/ObtenerMesasDeAmbiente", params)
        },
        obtenerAmbientes: function (params) {
            return $http.post(URL_ + "/Restaurante/ObtenerAmbientes", params);
        },
        obtenerTodosLosAmbientes: function (params) {
            return $http.post(URL_ + "/Restaurante/ObtenerTodosLosAmbientes", params);
        },
        obtenerAtencionDeMesa: function (params) {
            return $http.post(URL_ + "/Restaurante/ObtenerAtencionDeMesa", params)
        },
        obtenerAtencionEspecifica: function (params) {
            return $http.post(URL_ + "/Restaurante/ObtenerAtencionEspecifica")
        },
        crearAmbiente: function (params) {
            return $http.post(URL_ + "/Restaurante/AgregarAmbiente", params);
        },
        actualizarAmbiente: function (params) {
            return $http.post(URL_ + "/Restaurante/ActualizarAmbiente", params);
        },
        crearMesa: function (params) {
            return $http.post(URL_ + "/Restaurante/AgregarMesa", params);
        },
        eliminarAmbiente: function (params) {
            return $http.post(URL_ + "/Restaurante/EliminarAmbiente", params);
        },
        eliminarMesa: function (params) {
            return $http.post(URL_ + "/Restaurante/EliminarMesa", params);
        },
        actualizarMesa: function (params) {
            return $http.post(URL_ + "/Restaurante/ActualizarMesa", params);
        },
        obtenerItemsSuperficial: function (params) {
            return $http.post(URL_ + "/Restaurante/ObtenerItemsSuperficial", params);
        },
        obtenerItemsDeCategoria: function (params) {
            return $http.post(URL_ + "/Restaurante/ObtenerItemsDeCategoria", params);
        },
        obtenerItem: function (params) {
            return $http.post(URL_ + "/Restaurante/ObtenerItemDeRestaurante", params);
        },
        obtenerComplementosDeFamilia: function (params) {
            return $http.post(URL_ + "/Restaurante/ObtenerComplementosDeFamilia", params);
        },
        agregarOrdenDeAtencion: function (params) {
            return $http.post(URL_ + "/Restaurante/AgregarOrdenDeAtencion", params);
        },
        crearAtencionConOrden: function (params) {
            return $http.post(URL_ + "/Restaurante/CrearAtencionConOrden", params);
        },
        finalizarAtencion: function (params) {
            return $http.post(URL_ + "/Restaurante/FinalizarAtencionCocina", params);
        },
        obtenerAtencionesConfirmadas: function (params) {
            return $http.post(URL_ + "Restaurante/ObtenerAtencionesConfirmadas", params);
        },
        cambiarEstadoDeDetallesDeOrden: function (params) {
            return $http.post(URL_ + "Restaurante/CambiarEstadoDeDetallesDeOrden", params);
        },
        atenderDetalleDeOrden: function (params) {
            return $http.post(URL_ + "Restaurante/AtenderDetalleDeOrden", params);
        },
        servirDetalleDeOrden: function (params) {
            return $http.post(URL_ + "Restaurante/ServirDetalleDeOrden", params);
        },
        prepararEstadoDeDetalleDeOrden: function (params) {
            return $http.post(URL_ + "Restaurante/PrepararDetalleDeOrden", params);
        },
        anularDetalleDeOrden: function (params) {
            return $http.post(URL_ + "Restaurante/AnularDetalleDeOrden", params);
        },
        devolverDetalleDeOrden: function (params) {
            return $http.post(URL_ + "Restaurante/DevolverDetalleDeOrden", params);
        },
        observarDetalleDeOrden: function (params) {
            return $http.post(URL_ + "Restaurante/ObservarDetalleDeOrden", params);
        },
        reanudarDetalleDeOrden: function (params) {
            return $http.post(URL_ + "Restaurante/ReanudarDetalleDeOrden", params);
        },


        reanudarTodosLosDetallesDeOrden: function (params) {
            return $http.post(URL_ + "Restaurante/ReanudarTodosLosDetallesDeOrden", params);
        },
        atenderTodosLosDetallesDeOrden: function (params) {
            return $http.post(URL_ + "Restaurante/AtenderTodosLosDetallesDeOrden", params);
        },
        anularTodosLosDetallesDeOrden: function (params) {
            return $http.post(URL_ + "Restaurante/AnularTodosLosDetallesDeOrden", params);
        },
        cerrarOrden: function (params) {
            return $http.post(URL_ + "Restaurante/CerrarOrden", params);
        },


        obtenerOrdenesPorEstado: function (params) {
            return $http.post(URL_ + "Restaurante/ObtenerOrdenesPorEstado", params)
        },
        obtenerOrdenesConfirmadas: function (params) {
            return $http.post(URL_ + "Restaurante/ObtenerOrdenesConfirmadas", params)
        },
        obtenerOrdenesPorEstadoDesdeUnAmbiente: function (params) {
            return $http.post(URL_ + "Restaurante/ObtenerOrdenesPorEstadoDesdeUnAmbiente", params);
        },
        cambiarEstadoDeOrden: function (params) {
            return $http.post(URL_ + "Restaurante/CambiarEstadoDeOrden", params);
        },
        cambiarEstadosDeOrdenes: function (params) {
            return $http.post(URL_ + "Restaurante/CambiarEstadosDeOrdenes", params);
        },
        actualizarImportesDeTransaccion: function (params) {
            return $http.post(ULR_ + "Restaurante/ActualizarImportesDeTransaccion", params);
        },

        cambiarMesaDeAtencion: function (params) {
            return $http.post(URL_ + "RestauranteAtencion/CambiarMesa", params);
        },
        anularAtencion: function (params) {
            return $http.post(URL_ + "Restaurante/AnularAtencion", params);
        },
        cerrarAtencion: function (params) {
            return $http.post(URL_ + "Restaurante/CerrarAtencion", params);
        },
        ObtenerIdsDeitemsPorCategoria: function (params) {
            return $http.post(URL_ + "Restaurante/ObtenerIdsDeitemsPorCategoria", params);
        },
        salvarConfiguracionDePago: function (params) {
            return $http.post(URL_ + "Restaurante/SalvarConfiguracionDePago", params);
        },
        ObtenerItemCompleto: function (params) {
            return $http.post(URL_ + "Restaurante/ObtenerItemDeRestauranteIncluyendoComplementosDeFamilia", params);
        },
        obtenerAtencionesCerradas: function (params) {
            return $http.post(URL_ + "Restaurante/ObtenerAtencionesCerradas", params);
        },
        prepararDetallesDeOrdenes: function (params) {
            return $http.post(URL_ + "Restaurante/PrepararDetallesDeOrdenes", params);
        },
        servirDetallesDeOrdenes: function (params) {
            return $http.post(URL_ + "Restaurante/ServirDetallesDeOrdenes", params);
        },
        obtenerOrdenHtml: function (params) {
            return $http.post(URL_ + "Restaurante/ObtenerOrdenHtml", params);
        },
        obtenerAtencionHtml: function (params) {
            return $http.post(URL_ + "Restaurante/ObtenerAtencionHtml", params);
        },
        obtenerConfiguracionRestauranteFacturacion: function (params) {
            return $http.post(URL_ + "Restaurante/ObtenerConfiguracionRestauranteFacturacion", params);
        },
        obtenerMozos: function (params) {
            return $http.post(URL_ + "Restaurante/ObtenerMozos", params);
        },
        facturarAtencion: function (params) {
            return $http.post(URL_ + "Restaurante/FacturarAtencion", params);
        },
        confirmarPagoAtencion: function (params) {
            return $http.post(URL_ + "Restaurante/ConfirmarPagoAtencion", params);
        },
        obtenerAtencionPDF: function (params) {
            return $http.post(URL_ + "Restaurante/ObtenerAtencionPDF", params);
        },
        obtenerAtencionPDFString: function (params) {
            return $http.post(URL_ + "Restaurante/ObtenerAtencionPDFString", params);
        },
        obtenerDocumentosAtencion: function (params) {
            return $http.post(URL_ + "Restaurante/ObtenerDocumentosAtencion", params);
        },
        actualizarJsonDetalleDeDetalleOrden: function (params) {
            return $http.post(URL_ + "Restaurante/ActualizarJsonDetalleDeDetalleOrden", params);
        },
        obtenerComplementos: function (params) {
            return $http.post(URL_ + "Restaurante/ObtenerComplementos", params);
        },
        actualizarComplemento: function (params) {
            return $http.post(URL_ + "Restaurante/ActualizarComplemento", params);
        },
        obtenerCentrosAtencionRestaurante: function (params) {
            return $http.post(URL_ + "Restaurante/ObtenerCentrosAtencionRestaurante", params);
        },
        obtenerAtencionesSinMesa: function (params) {
            return $http.post(URL_ + "Restaurante/ObtenerAtencionesSinMesa", params);
        }
    };
})

app.factory('reporteService', function ($http) {
    return {
        reporteVentasPorCaracteristicas: function (params) {
            return $http.post(URL_ + "/Reporte/VentasPorCaracteristica", params);
        }
    };
});
app.factory('ejemploService', function ($http) {
    return {
        obtenerClientesDeEjemplo: function (params) {
            return $http.post(URL_ + "/Ejemplo/ObtenerClientesDeEjemplo", params);
        },
        obtenerComprobantesDeEjemplo: function (params) {
            return $http.post(URL_ + "/Ejemplo/ObtenerComprobantesDeEjemplo", params);
        }
    };
});

app.factory('pedidoService', function ($http) {
    return {
        ObtenerPedidos: function (params) {
            return $http.post(URL_ + "/Pedido/ObtenerPedidos", params);
        },
        ObtenerParametrosDePedidos: function (params) {
            return $http.post(URL_ + "/Pedido/ObtenerParametrosDePedidos", params);
        },
        GuardarPedido: function (params) {
            return $http.post(URL_ + "/Pedido/GuardarPedido", params);
        },
        InvalidarPedido: function (params) {
            return $http.post(URL_ + "/Pedido/InvalidarPedido", params);
        },
        ObtenerPedidoParaEditar: function (params) {
            return $http.post(URL_ + "/Pedido/ObtenerPedidoParaEditar", params);
        },
        ObtenerDocumentoDePedido: function (params) {
            return $http.post(URL_ + "/Pedido/ObtenerDocumentoDePedido", params);
        },
        obtenerFormatosDeImpresion: function(params){
        return $http.post(URL_ + "/Pedido/ObtenerFormatosDeImpresion",params);
        },
        ObtenerHtmlDocumentoDePedido: function (params) {
            return $http.post(URL_ + "/Pedido/ObtenerHtmlDocumentoDePedido", params);
        },
        ObtenerTipoDeComprobante: function (params) {
            return $http.post(URL_ + "/Pedido/ObtenerTipoDeComprobante", params);
        },
        ConfirmarPedido: function (params) {
            return $http.post(URL_ + "/Pedido/ConfirmarPedido",params)
        }
    };
});


app.factory('comprobanteService', function ($http) {
    return {
        descargarDocumentoZip: function (params) {
            return $http.post(URL_ + "/Comprobante/DescargarDocumentoZip", params);
        },
        descargarDocumentoPdf: function (params) {
            return $http.post(URL_ + "/Comprobante/DescargarDocumentoPdf", params);
        },
        descargarDocumentoXml: function (params) {
            return $http.post(URL_ + "/Comprobante/DescargarDocumentoXml", params);
        },
        enviarCorreoElectronicoConDocumento: function (params) {
            return $http.post(URL_ + "/Comprobante/EnviarCorreoElectronicoConDocumento", params);
        },
        
    };
});
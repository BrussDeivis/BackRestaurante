using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.WebApplication.Models;
using Tsp.Sigescom.Config;
using System.Globalization;
using Tsp.Sigescom.Utilitarios;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Negocio.Establecimientos;

namespace Tsp.Sigescom.WebApplication.MenuData
{
    public class Data
    {
        public IEnumerable<NavBar> navbarItems(IConceptoLogica _logicaConcepto, IActorNegocioLogica _actorNegocioLogica, ISucursal_Logica sucursal_Logica)
        {
            var cont = 0;
            var caracteristicas = _logicaConcepto.ObtenerCaracteristicas();
            var sucursales = sucursal_Logica.ObtenerSucursalesVigentes();
            var menu = new List<NavBar>();

            #region Ventas
            menu.Add(new NavBar { Id = 100, Nombre = "Venta", Icono = "mdi mdi-cart-outline", Estado = true, EsPadre = true, IdPadre = 0, Roles = new List<string> { "SoloVendedor", "Vendedor", "Contador", "AdministradorNegocio", "Venta Masiva", "Gerente" } });
            if (AplicacionSettings.Default.TipoVenta == 1)
                menu.Add(new NavBar { Id = 101, Nombre = "Nueva Venta", Controlador = "Venta", Accion = "Ventas", Icono = "fa fa-cart-plus fa-fw", Estado = true, EsPadre = false, IdPadre = 100, Roles = new List<string> { "SoloVendedor", "Vendedor" } });
            else if (AplicacionSettings.Default.TipoVenta == 2)
                menu.Add(new NavBar { Id = 102, Nombre = "Nueva Venta", Controlador = "Venta", Accion = "PuntoDeVenta", Icono = "fa fa-cart-plus fa-fw", Estado = true, EsPadre = false, IdPadre = 100, Roles = new List<string> { "SoloVendedor", "Vendedor" } });
            menu.Add(new NavBar { Id = 103, Nombre = "Venta Modo Caja", Controlador = "Venta", Accion = "VentaPorMostradorIntegradoModoCaja", Icono = "fa fa-plug fa-fw", Estado = true, EsPadre = false, IdPadre = 100, Roles = new List<string> { "Vendedor" } });
            menu.Add(new NavBar { Id = 106, Nombre = "Venta por Contingencia", Controlador = "Venta", Accion = "VentasPorContingencia", Icono = "fa fa-file-text-o", Estado = true, EsPadre = false, IdPadre = 100, Roles = new List<string> { "Vendedor" } });
            menu.Add(new NavBar { Id = 104, Nombre = "Ver Ventas", Controlador = "Venta", Accion = "ConsultarVentas", Icono = "fa fa-cart-arrow-down fa-fw", Estado = true, EsPadre = false, IdPadre = 100, Roles = new List<string> { "Vendedor" } });
            menu.Add(new NavBar { Id = 109, Nombre = "Venta Masiva", Controlador = "Venta", Accion = "VentasMasivas", Icono = "fa fa-file-text-o", Estado = true, EsPadre = false, IdPadre = 100, Roles = new List<string> { "VentaMasiva" } });
            menu.Add(new NavBar { Id = 107, Nombre = "Ventas por Vendedor", Controlador = "Venta", Accion = "VentasYCobrosPorVendedor", Icono = "fa fa-file-text-o", Estado = true, EsPadre = false, IdPadre = 100, Roles = new List<string> { "VentaMasiva" } });
            menu.Add(new NavBar { Id = 108, Nombre = "Ver Ventas por Vendedor", Controlador = "Venta", Accion = "ConsultarVentasCobrosPorVendedor", Icono = "fa fa-file-text-o", Estado = true, EsPadre = false, IdPadre = 100, Roles = new List<string> { "VentaMasiva" } });
            //menu.Add(new NavBar { Id = 111, Nombre = "Ventas Corporativas", Controlador = "Venta", Accion = "VentasCorporativas", Icono = "fa fa-cart-arrow-up fa-fw", Estado = true, EsPadre = false, IdPadre = 5, Roles = new List<string>{ "Vendedor" } });
            //menu.Add(new NavBar { Id = 112, Nombre = "Ver Ventas Corporativas", Controlador = "Venta", Accion = "ConsultarVentasCorporativas", Icono = "fa fa-cart-arrow-up fa-fw", Estado = true, EsPadre = false, IdPadre = 5, Roles = new List<string>{ "Vendedor" } });
            menu.Add(new NavBar { Id = 105, Nombre = "Reportes Vendedor", Controlador = "Venta", Accion = "ReportesVendedor", Icono = "fa fa-file-text-o", Estado = true, EsPadre = false, IdPadre = 100, Roles = new List<string> { "Vendedor" } });
            if (VentasSettings.Default.GenerarPuntosEnVentas || VentasSettings.Default.UsarPuntosComoMedioDePago)
                menu.Add(new NavBar { Id = 111, Nombre = "Reportes Puntos", Controlador = "Reporte", Accion = "ReportePuntos", Icono = "fa fa-file-text-o", Estado = true, EsPadre = false, IdPadre = 100, Roles = new List<string> { "AdministradorNegocio", "Gerente" } });
            menu.Add(new NavBar { Id = 112, Nombre = "Reportes Gerente", Controlador = "Venta", Accion = "ReportesAdministrador", Icono = "fa fa-file-text-o", Estado = true, EsPadre = false, IdPadre = 100, Roles = new List<string> { "Gerente", "Contador", "AdministradorNegocio" } });
            //menu.Add(new NavBar { Id = 110, Nombre = "Invalidar Rechazadas", Controlador = "FacturacionElectronica", Accion = "InvalidarRechazadas", Icono = "fa fa-plug fa-fw", Estado = true, EsPadre = false, IdPadre = 100, Roles = new List<string> { "AdministradorNegocio" } });
            menu.Add(new NavBar { Id = 113, Nombre = "Reportes", Controlador = "VentaReportes", Accion = "Principal", Icono = "fa fa-plug fa-fw", Estado = true, EsPadre = false, IdPadre = 100, Roles = new List<string> { "Gerente", "AdministradorNegocio", "JefeVenta" } });
            #endregion


            #region Pedidos
            menu.Add(new NavBar { Id = 2200, Nombre = "Pedido", Controlador = "Pedido", Accion = "Index", Icono = "fa fa-shopping-basket", Estado = true, EsPadre = true, IdPadre = 0, Roles = new List<string> { "Vendedor", "Cajero", "JefeVenta", "AdministradorNegocio", "Gerente" } });
            menu.Add(new NavBar { Id = 2201, Nombre = "Ver Pedido", Controlador = "Pedido", Accion = "Index", Icono = "fa fa-file-text-o", Estado = true, EsPadre = false, IdPadre = 2200, Roles = new List<string> { "Vendedor", "Cajero", "JefeVenta", "AdministradorNegocio" } });
            menu.Add(new NavBar { Id = 2202, Nombre = "Reportes", Controlador = "PedidoReportes", Accion = "Index", Icono = "fa fa-file-text-o", Estado = true, EsPadre = false, IdPadre = 2200, Roles = new List<string> { "JefeVenta", "AdministradorNegocio", "Gerente" } });
            #endregion

            #region Cotizaciones
            menu.Add(new NavBar { Id = 200, Nombre = "Cotización", Controlador = "Cotizacion", Accion = "Index", Icono = "mdi mdi-clipboard-text", Estado = true, EsPadre = false, IdPadre = 0, Roles = new List<string> { "Vendedor" } });
            #endregion

            #region Precios
            menu.Add(new NavBar { Id = 300, Nombre = "Precio", Icono = "mdi mdi-cash-100", Estado = true, EsPadre = true, IdPadre = 0, Roles = new List<string> { "JefeVenta" } });
            menu.Add(new NavBar { Id = 301, Nombre = "Lista de Precios", Controlador = "Precio", Accion = "Precio", Icono = "mdi mdi-cash-100", Estado = true, EsPadre = false, IdPadre = 300, Roles = new List<string> { "JefeVenta" } });
            //menu.Add(new NavBar { Id = 27, Nombre = "Descuento", Controlador = "Precio", Accion = "Descuento", Icono = "mdi mdi-cash-100", Estado = true, EsPadre = false, IdPadre = 25, Roles = new List<string>{ "JefeVenta" } });
            //menu.Add(new NavBar { Id = 28, Nombre = "Bonificación", Controlador = "Precio", Accion = "Bonificacion", Icono = "mdi mdi-cash-100", Estado = true, EsPadre = false, IdPadre = 25, Roles = new List<string>{ "JefeVenta" } });
            #endregion

            #region Compra
            menu.Add(new NavBar { Id = 400, Nombre = "Compra", Icono = "mdi mdi-cart-plus", Estado = true, EsPadre = true, IdPadre = 0, Roles = new List<string> { "Comprador", "AdministradorNegocio", "Gerente" } });
            menu.Add(new NavBar { Id = 401, Nombre = "Nueva Compra", Controlador = "Compra", Accion = "Compras", Icono = "mdi mdi-cart", Estado = true, EsPadre = false, IdPadre = 400, Roles = new List<string> { "Comprador" } });
            menu.Add(new NavBar { Id = 402, Nombre = "Ver Compras", Controlador = "Compra", Accion = "ConsultarCompras", Icono = "fa fa-cart-arrow-up fa-fw", Estado = true, EsPadre = false, IdPadre = 400, Roles = new List<string> { "Comprador" } });
            //menu.Add(new NavBar { Id = 107, Nombre = "Compras Corporativas", Controlador = "Compra", Accion = "ComprasCorporativas", Icono = "fa fa-cart-arrow-up fa-fw", Estado = true, EsPadre = false, IdPadre = 8, Roles = new List<string>{ "Corporativo" } });
            //menu.Add(new NavBar { Id = 108, Nombre = "Ver Compras Corporativas", Controlador = "Compra", Accion = "ConsultarComprasCorporativas", Icono = "fa fa-cart-arrow-up fa-fw", Estado = true, EsPadre = false, IdPadre = 8, Roles = new List<string>{ "Corporativo" } });
            menu.Add(new NavBar { Id = 403, Nombre = "Reportes", Controlador = "Reporte", Accion = "ReportesAdministradorDeCompras", Icono = "fa fa-file-text-o", Estado = true, EsPadre = false, IdPadre = 400, Roles = new List<string> { "Gerente", "AdministradorNegocio" } });
            #endregion

            #region Gerencia
            menu.Add(new NavBar { Id = 500, Nombre = "Gerencia", Icono = "mdi mdi-cart-outline", Estado = true, EsPadre = true, IdPadre = 0, Roles = new List<string> { "Gerente" } });
            menu.Add(new NavBar { Id = 501, Nombre = "Utilidad de Ventas", Controlador = "Reporte", Accion = "ReporteUtilidadDeVentas", Icono = "fa fa-file-text-o", Estado = true, EsPadre = false, IdPadre = 500, Roles = new List<string> { "Gerente" } });
            menu.Add(new NavBar { Id = 502, Nombre = "Consolidados", Controlador = "Reporte", Accion = "ConsolidadoReportesAdministrador", Icono = "fa fa-file-text-o", Estado = true, EsPadre = false, IdPadre = 500, Roles = new List<string> { "AdministradorTI" } });
            #endregion

            #region Almacen
            menu.Add(new NavBar { Id = 600, Nombre = "Almacén", Icono = "glyphicon glyphicon-home", Estado = true, EsPadre = true, IdPadre = 0, Roles = new List<string> { "Almacenero", "AdministradorNegocio", "Gerente" } });
            menu.Add(new NavBar { Id = 612, Nombre = "Órdenes", Controlador = "OrdenAlmacen", Accion = "Principal", Icono = "glyphicon glyphicon-home", Estado = true, EsPadre = false, IdPadre = 600, Roles = new List<string> { "Almacenero", "AdministradorNegocio" } });
            menu.Add(new NavBar { Id = 602, Nombre = "Traslados", Controlador = "Almacen", Accion = "TrasladosInternos", Icono = "glyphicon glyphicon-home", Estado = true, EsPadre = false, IdPadre = 600, Roles = new List<string> { "Almacenero" } });
            menu.Add(new NavBar { Id = 601, Nombre = "Entradas/Salidas", Controlador = "Almacen", Accion = "MovimientosDeAlmacen", Icono = "glyphicon glyphicon-home", Estado = true, EsPadre = false, IdPadre = 600, Roles = new List<string> { "Almacenero" } });
            menu.Add(new NavBar { Id = 603, Nombre = "Guías de Remisión", Controlador = "Almacen", Accion = "ConsultarGuiasRemision", Icono = "mdi mdi-cube-send", Estado = true, EsPadre = false, IdPadre = 600, Roles = new List<string> { "Almacenero" } });
            menu.Add(new NavBar { Id = 604, Nombre = "Inventario Valorizado Actual", Controlador = "AlmacenReportes", Accion = "ReporteInventarioValorizado", Icono = "mdi mdi-cube-send", Estado = true, EsPadre = false, IdPadre = 600, Roles = new List<string> { "Almacenero", "AdministradorNegocio", "Gerente" } });
            menu.Add(new NavBar { Id = 605, Nombre = "Inventario Histórico", Controlador = "Almacen", Accion = "InventarioHistorico", Icono = "mdi mdi-cube-send", Estado = true, EsPadre = false, IdPadre = 600, Roles = new List<string> { "AdministradorTI" } });
            if (AplicacionSettings.Default.MostrarReporteSalidasDeAlcohol)
                menu.Add(new NavBar { Id = 608, Nombre = "Salidas de Alcohol", Controlador = "Almacen", Accion = "ReporteConceptoBasico", Icono = "mdi mdi-cube-send", Estado = true, EsPadre = false, IdPadre = 600, Roles = new List<string> { "Almacenero", "AdministradorNegocio", "Gerente" } });

            menu.Add(new NavBar { Id = 611, Nombre = "Reportes", Controlador = "AlmacenReportes", Accion = "Principal", Icono = "fa fa-plug fa-fw", Estado = true, EsPadre = false, IdPadre = 600, Roles = new List<string> { "Gerente", "AdministradorNegocio", "Almacenero" } });
            #endregion

            #region caja
            menu.Add(new NavBar { Id = 700, Nombre = "Tesorería y Finanzas", Icono = "glyphicon glyphicon-home", Estado = true, EsPadre = true, IdPadre = 0, Roles = new List<string> { "Cajero", "FalsoCajero", "AdministradorNegocio", "Gerente" } });
             
            if (AplicacionSettings.Default.TipoVenta == 2)
                menu.Add(new NavBar { Id = 701, Nombre = "Caja - Venta", Controlador = "Venta", Accion = "CajaDeVenta", Icono = "fa fa-money fa-fw", Estado = true, EsPadre = false, IdPadre = 700, Roles = new List<string> { "Cajero" } });
            //menu.Add(new NavBar { Id = 702, Nombre = "Reporte de Cajero", Controlador = "Venta", Accion = "ReportesCajero", Icono = "fa fa-user-circle-o fa-fw", Estado = true, EsPadre = false, IdPadre = 700, Roles = new List<string> { "Cajero", "FalsoCajero" } });

            menu.Add(new NavBar { Id = 703, Nombre = "Inicializar Cajas", Controlador = "Finanza", Accion = "InicializarCaja", Icono = "mdi mdi-cube-send", Estado = true, EsPadre = false, IdPadre = 700, Roles = new List<string> { "AdministradorTI" } }); 
            menu.Add(new NavBar { Id = 704, Nombre = "Cuentas por Cobrar/Pagar", Controlador = "Finanza", Accion = "CobrarYPagar", Icono = "mdi mdi-cube-send", Estado = true, EsPadre = false, IdPadre = 700, Roles = new List<string> { "AdministradorNegocio" } });
            menu.Add(new NavBar { Id = 705, Nombre = "Ingresos/Egresos", Controlador = "Finanza", Accion = "CobrosYPagos", Icono = "mdi mdi-cube-send", Estado = true, EsPadre = false, IdPadre = 700, Roles = new List<string> { "AdministradorNegocio", "Cajero" } });
            menu.Add(new NavBar { Id = 706, Nombre = "Cuentas Bancarias", Controlador = "Finanza", Accion = "CuentasBancarias", Icono = "mdi mdi-cube-send", Estado = true, EsPadre = false, IdPadre = 700, Roles = new List<string> { "AdministradorNegocio" } });
            menu.Add(new NavBar { Id = 707, Nombre = "Reportes", Controlador = "FinanzaReportes", Accion = "Principal", Icono = "fa fa-plug fa-fw", Estado = true, EsPadre = false, IdPadre = 700, Roles = new List<string> { "Gerente", "AdministradorNegocio", "Cajero" } });

            #endregion

            #region Finanzas
            //menu.Add(new NavBar { Id = 800, Nombre = "Finanzas", Icono = "mdi mdi-cube-send", Estado = true, EsPadre = true, IdPadre = 0, Roles = new List<string> { "AdministradorNegocio", "Gerente", "AdministradorTI" } });

            //menu.Add(new NavBar { Id = 801, Nombre = "Cuentas por Cobrar/Pagar", Controlador = "Finanza", Accion = "CobrarYPagar", Icono = "mdi mdi-cube-send", Estado = true, EsPadre = false, IdPadre = 800, Roles = new List<string> { "AdministradorNegocio" } });

            //menu.Add(new NavBar { Id = 811, Nombre = "Gastos", Controlador = "Gasto", Accion = "Index", Icono = "mdi mdi-clipboard-text", Estado = true, EsPadre = false, IdPadre = 800, Roles = new List<string> { "AdministradorNegocio" } });
            //menu.Add(new NavBar { Id = 807, Nombre = "Reporte de Gastos", Controlador = "Reporte", Accion = "GastosReportesAdministrador", Icono = "fa fa-file-text-o", Estado = true, EsPadre = false, IdPadre = 800, Roles = new List<string> { "Gerente" } });
            //menu.Add(new NavBar { Id = 806, Nombre = "Reportes de Caja", Controlador = "Reporte", Accion = "ReporteFinanzas", Icono = "fa fa-file-text-o", Estado = true, EsPadre = false, IdPadre = 800, Roles = new List<string> { "Gerente" } });

            //menu.Add(new NavBar { Id = 808, Nombre = "Reporte por Cobrar/Pagar", Controlador = "Reporte", Accion = "DeudasPorPagarYDeudasPorCobrarsAdministrador", Icono = "fa fa-plug fa-fw", Estado = true, EsPadre = false, IdPadre = 800, Roles = new List<string> { "Gerente" } });
            //menu.Add(new NavBar { Id = 809, Nombre = "Reporte de Clientes", Controlador = "Reporte", Accion = "ReporteDeDeudaPorCliente", Icono = "fa fa-plug fa-fw", Estado = true, EsPadre = false, IdPadre = 800, Roles = new List<string> { "Gerente", "AdministradorNegocio" } });
            //menu.Add(new NavBar { Id = 810, Nombre = "Reporte de Deudas/Pagos", Controlador = "Reporte", Accion = "DeudasYPagos", Icono = "fa fa-plug fa-fw", Estado = true, EsPadre = false, IdPadre = 800, Roles = new List<string> { "Gerente", "AdministradorNegocio" } });


            #endregion

            #region GASTOS
            menu.Add(new NavBar { Id = 2100, Nombre = "Gasto", Icono = "fa fa-clipboard", Estado = true, EsPadre = true, IdPadre = 0, Roles = new List<string> { "AdministradorNegocio" } });
            menu.Add(new NavBar { Id = 2101, Nombre = "Ver ", Controlador = "Gasto", Accion = "Index", Icono = "fa fa-user-circle-o fa-fw", Estado = true, EsPadre = false, IdPadre = 2100, Roles = new List<string> { "AdministradorNegocio" } });
            menu.Add(new NavBar { Id = 2102, Nombre = "Concepto", Controlador = "Gasto", Accion = "Concepto", Icono = "fa fa-user-circle-o fa-fw", Estado = true, EsPadre = false, IdPadre = 2100, Roles = new List<string> { "AdministradorNegocio"} });
            menu.Add(new NavBar { Id = 2103, Nombre = "Reporte", Controlador = "Reporte", Accion = "GastosReportesAdministrador", Icono = "fa fa-file-text-o", Estado = true, EsPadre = false, IdPadre = 2100, Roles = new List<string> { "Gerente" } });

            #endregion

            #region Conceptos
            menu.Add(new NavBar { Id = 900, Nombre = "Conceptos", Icono = "mdi mdi-cube-send", Estado = true, EsPadre = true, IdPadre = 0, Roles = new List<string> { "Comprador", "AdministradorTI", "JefeVenta", "AdministradorNegocio" } });
            menu.Add(new NavBar { Id = 901, Nombre = "Nueva Familia", Controlador = "Concepto", Accion = "ConceptoBasico", Icono = "mdi mdi-cube-send", Estado = true, EsPadre = false, IdPadre = 900, Roles = new List<string> { "AdministradorNegocio" } });
            menu.Add(new NavBar { Id = 902, Nombre = "Ver Familias", Controlador = "Concepto", Accion = "ConsultarConceptosBasicos", Icono = "mdi mdi-cube-send", Estado = true, EsPadre = false, IdPadre = 900, Roles = new List<string> { "AdministradorNegocio" } });
            menu.Add(new NavBar { Id = 903, Nombre = "Categorias", Controlador = "Concepto", Accion = "Categorias", Icono = "mdi mdi-cube-send", Estado = true, EsPadre = false, IdPadre = 900, Roles = new List<string> { "AdministradorNegocio" } });
            menu.Add(new NavBar { Id = 904, Nombre = "Presentaciones", Controlador = "Maestro", Accion = "Presentaciones", Icono = "mdi mdi-cube-send", Estado = true, EsPadre = false, IdPadre = 900, Roles = new List<string> { "AdministradorNegocio" } });
            menu.Add(new NavBar { Id = 905, Nombre = "Nuevo Concepto", Controlador = "Concepto", Accion = "Concepto", Icono = "mdi mdi-cube-send", Estado = true, EsPadre = false, IdPadre = 900, Roles = new List<string> { "Comprador", "AdministradorNegocio" } });
            menu.Add(new NavBar { Id = 906, Nombre = "Ver Conceptos", Controlador = "Concepto", Accion = "ConsultarConceptos", Icono = "mdi mdi-cube-send", Estado = true, EsPadre = false, IdPadre = 900, Roles = new List<string> { "Comprador", "AdministradorNegocio" } });
            menu.Add(new NavBar { Id = 907, Nombre = "Reportes Conceptos", Controlador = "ConceptoReportes", Accion = "ReportesAdministrador", Icono = "fa fa-plug fa-fw", Estado = true, EsPadre = false, IdPadre = 900, Roles = new List<string> { "Gerente", "AdministradorNegocio" } });

            menu.Add(new NavBar { Id = 908, Nombre = "Stock - Precios - Conceptos", Controlador = "Concepto", Accion = "Conceptos", Icono = "mdi mdi-cube-send", Estado = true, EsPadre = false, IdPadre = 900, Roles = new List<string> { "Comprador" } });
            menu.Add(new NavBar { Id = 909, Nombre = "Precios - Etiquetas", Controlador = "Concepto", Accion = "Precio", Icono = "mdi mdi-cube-send", Estado = true, EsPadre = false, IdPadre = 900, Roles = new List<string> { "AdministradorNegocio", "JefeVenta" } });
            if (ActorSettings.Default.PermitirRegistroCodigoDigemidEnEstableciemientoComercial)
                menu.Add(new NavBar { Id = 910, Nombre = "Reporte Digemid", Controlador = "Concepto", Accion = "ReporteDigemid", Icono = "mdi mdi-cube-send", Estado = true, EsPadre = false, IdPadre = 900, Roles = new List<string> { "AdministradorNegocio", "Comprador" } });
            menu.Add(new NavBar { Id = 920, Nombre = "Caracteristicas", Controlador = "Concepto", Accion = "Caracteristicas", Icono = "mdi mdi-cube-send", Estado = true, EsPadre = false, IdPadre = 900, Roles = new List<string> { "AdministradorNegocio" } });
            menu.Add(new NavBar { Id = 930, Nombre = "Valor de Características", Icono = "mdi mdi-cube-send", Estado = true, EsPadre = true, IdPadre = 900, Roles = new List<string> { "AdministradorNegocio" } });
            //agregamos como menu las caracteristicas
            foreach (var item in caracteristicas)
            {
                menu.Add(new NavBar { Id = (931 + cont), Nombre = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(CultureInfo.CurrentCulture.TextInfo.ToLower(item.nombre)), Controlador = "Concepto", Accion = "Caracteristica_", Icono = "mdi mdi-cube-send", Estado = true, EsPadre = false, IdPadre = 930, IdCaracteristica = item.id, Roles = new List<string> { "AdministradorNegocio" } });
                cont++;
            }
            #endregion

            #region Cliente
            menu.Add(new NavBar { Id = 1000, Nombre = "Cliente", Icono = "mdi mdi-account-multiple", Estado = true, EsPadre = true, IdPadre = 0, Roles = new List<string> { "Vendedor", "AdministradorNegocio" } });
            menu.Add(new NavBar { Id = 1001, Nombre = "Ver", Controlador = "Persona", Accion = "Cliente", Icono = "fa fa-user-circle-o fa-fw", Estado = true, EsPadre = false, IdPadre = 1000, Roles = new List<string> { "Vendedor" } });
            menu.Add(new NavBar { Id = 1002, Nombre = "Cartera de clientes", Controlador = "Cliente", Accion = "CarteraDeClientes", Icono = "fa fa-tasks fa-fw", Estado = true, EsPadre = false, IdPadre = 1000, Roles = new List<string> { "AdministradorNegocio" } });
            menu.Add(new NavBar { Id = 1003, Nombre = "Grupo de clientes", Controlador = "Cliente", Accion = "GrupoClientes", Icono = "fa fa-tasks fa-fw", Estado = true, EsPadre = false, IdPadre = 1000, Roles = new List<string> { "AdministradorNegocio" } });
            #endregion

            #region Proveedor
            menu.Add(new NavBar { Id = 1100, Nombre = "Proveedor", Controlador = "Persona", Accion = "Proveedor", Icono = "fa fa-user-circle fa-fw", Estado = true, EsPadre = false, IdPadre = 0, Roles = new List<string> { "Comprador" } });
            #endregion

            #region Empleado
            menu.Add(new NavBar { Id = 1200, Nombre = "Empleado", Icono = "fa fa-group fa-fw", Estado = true, EsPadre = true, IdPadre = 0, Roles = new List<string> { "AdministradorNegocio", "AdministradorTI" } });
            menu.Add(new NavBar { Id = 1201, Nombre = "Ver", Controlador = "Persona", Accion = "Empleado", Icono = "fa fa-user-md fa-fw", Estado = true, EsPadre = false, IdPadre = 1200, Roles = new List<string> { "AdministradorTI", "AdministradorNegocio" } });
            menu.Add(new NavBar { Id = 1202, Nombre = "Turno", Controlador = "Empleado", Accion = "Turnos", Icono = "fa fa-calendar fa-fw", Estado = true, EsPadre = false, IdPadre = 1200, Roles = new List<string> { "AdministradorTI", "AdministradorNegocio" } });
            #endregion

            #region Centro de atencion
            menu.Add(new NavBar { Id = 1300, Nombre = "Centro de Atención", Icono = "fa fa-home fa-fw", Estado = true, EsPadre = true, IdPadre = 0, Roles = new List<string> { "AdministradorTI" } });
            menu.Add(new NavBar { Id = 1301, Nombre = "Sede", Controlador = "CentroDeAtencion", Accion = "Sede", Icono = "fa fa-file-text-o", Estado = true, EsPadre = false, IdPadre = 1300, Roles = new List<string> { "AdministradorTI" } });
            menu.Add(new NavBar { Id = 1302, Nombre = "Surcursal", Controlador = "CentroDeAtencion", Accion = "Sucursal", Icono = "fa fa-file-text-o", Estado = true, EsPadre = false, IdPadre = 1300, Roles = new List<string> { "AdministradorTI" } });
            ////agregamos como menu las sucursales
            foreach (var item in sucursales)
            {
                menu.Add(new NavBar { Id = (1310 + cont), Nombre = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(CultureInfo.CurrentCulture.TextInfo.ToLower(item.Nombre)), Controlador = "CentroDeAtencion", Accion = "Sucursal_", Icono = "fa fa-file-text-o", Estado = true, EsPadre = false, IdPadre = 1300, IdSucursal = item.Id, Roles = new List<string> { "AdministradorNegocio" } });
                cont++;
            }
            #endregion

            #region Contabilidad
            menu.Add(new NavBar { Id = 1400, Nombre = "Contabilidad", Icono = "fa fa-paste fa-fw", Estado = true, EsPadre = true, IdPadre = 0, Roles = new List<string> { "Contador", "AdministradorTI" } });
            menu.Add(new NavBar { Id = 1402, Nombre = "Documentos Emitidos", Controlador = "FacturacionElectronica", Accion = "EmisionComprobantes", Icono = "fa fa-plug fa-fw", Estado = true, EsPadre = false, IdPadre = 1400, Roles = new List<string> { "Contador" } });
            menu.Add(new NavBar { Id = 1403, Nombre = "Documentos Enviados", Controlador = "FacturacionElectronica", Accion = "EnvioComprobantes", Icono = "fa fa-plug fa-fw", Estado = true, EsPadre = false, IdPadre = 1400, Roles = new List<string> { "Contador" } });
            menu.Add(new NavBar { Id = 1404, Nombre = "Configuración Fact. Electrónica", Controlador = "FacturacionElectronica", Accion = "Configuracion", Icono = "fa fa-plug fa-fw", Estado = true, EsPadre = false, IdPadre = 1400, Roles = new List<string> { "AdministradorTI" } });
            menu.Add(new NavBar { Id = 1405, Nombre = "Libros Electronicos", Controlador = "LibrosElectronicos", Accion = "LibroElectronico", Icono = "fa fa-book fa-fw", Estado = true, EsPadre = false, IdPadre = 1400, Roles = new List<string> { "Contador" } });
            menu.Add(new NavBar { Id = 1406, Nombre = "Tipo Cambio", Controlador = "TipoCambio", Accion = "TipoCambio", Icono = "fa fa-user-circle-o fa-fw", Estado = true, EsPadre = false, IdPadre = 1400, Roles = new List<string> { "AdministradorTI" } });
            menu.Add(new NavBar { Id = 1407, Nombre = "Tipos", Controlador = "Comprobante", Accion = "TiposDeComprobante", Icono = "fa fa-file-text-o", Estado = true, EsPadre = false, IdPadre = 1400, Roles = new List<string> { "AdministradorTI" } });
            menu.Add(new NavBar { Id = 1408, Nombre = "Series", Controlador = "Comprobante", Accion = "SeriesDeComprobante", Icono = "fa fa-file-text-o", Estado = true, EsPadre = false, IdPadre = 1400, Roles = new List<string> { "AdministradorTI" } });
            #endregion

            #region Configuración
            menu.Add(new NavBar { Id = 1500, Nombre = "Configuración", Icono = "fa fa-cog fa-fw", Estado = true, EsPadre = true, IdPadre = 0, Roles = new List<string> { "AdministradorTI" } });
            menu.Add(new NavBar { Id = 1501, Nombre = "Maestro", Controlador = "Maestro", Accion = "Index", Icono = "mdi mdi-clipboard-text", Estado = true, EsPadre = false, IdPadre = 1500, Roles = new List<string> { "AdministradorTI" } });
            menu.Add(new NavBar { Id = 1502, Nombre = "Parámetro", Controlador = "Configuracion", Accion = "Index", Icono = "fa fa-plug fa-fw", Estado = true, EsPadre = false, IdPadre = 1500, Roles = new List<string> { "AdministradorTI" } });
            menu.Add(new NavBar { Id = 1503, Nombre = "Tipos de Transacción", Controlador = "TipoDeTransaccion", Accion = "TiposDeTransaccion", Icono = "fa fa-plug fa-fw", Estado = true, EsPadre = false, IdPadre = 1500, Roles = new List<string> { "AdministradorTI" } });
            menu.Add(new NavBar { Id = 1504, Nombre = "Rol", Controlador = "Rol", Accion = "Roles", Icono = "fa fa-user-circle-o fa-fw", Estado = true, EsPadre = false, IdPadre = 1500, Roles = new List<string> { "AdministradorTI" } });
            #endregion

            #region seguridad
            menu.Add(new NavBar { Id = 1600, Nombre = "Seguridad y Accesos", Icono = "fa fa-shield fa-fw", Estado = true, EsPadre = true, IdPadre = 0, Roles = new List<string> { "AdministradorTI" } });
            menu.Add(new NavBar { Id = 1601, Nombre = "Usuarios y Roles", Controlador = "Admin", Accion = "Index", Icono = "fa fa-plug fa-fw", Estado = true, EsPadre = false, IdPadre = 1600, Roles = new List<string> { "AdministradorTI" } });
            menu.Add(new NavBar { Id = 1602, Nombre = "Estados y Acciones", Controlador = "Permiso", Accion = "Index", Icono = "fa fa-plug fa-fw", Estado = true, EsPadre = false, IdPadre = 1600, Roles = new List<string> { "AdministradorTI" } });


            #endregion

            #region cochera
            menu.Add(new NavBar { Id = 1700, Nombre = "Cochera", Icono = "mdi mdi-car", Estado = true, EsPadre = true, IdPadre = 0, Roles = new List<string> { "Cochera" } });
            menu.Add(new NavBar { Id = 1701, Nombre = "Ingresos - Salidas", Controlador = "Cochera", Accion = "Index", Icono = "fa fa-file-text-o", Estado = true, EsPadre = false, IdPadre = 1700, Roles = new List<string> { "Cochera" } });
            menu.Add(new NavBar { Id = 1702, Nombre = "Exoneraciones", Controlador = "Cochera", Accion = "Exoneraciones", Icono = "fa fa-file-text-o", Estado = true, EsPadre = false, IdPadre = 1700, Roles = new List<string> { "Cochera" } });
            menu.Add(new NavBar { Id = 1703, Nombre = "Reportes", Controlador = "Cochera", Accion = "Reportes", Icono = "fa fa-file-text-o", Estado = true, EsPadre = false, IdPadre = 1700, Roles = new List<string> { "Cochera" } });
            menu.Add(new NavBar { Id = 1704, Nombre = "Configuración", Controlador = "Cochera", Accion = "Configuracion", Icono = "fa fa-file-text-o", Estado = true, EsPadre = false, IdPadre = 1700, Roles = new List<string> { "Cochera" } });
            #endregion

            #region Restaurante
            if (MascaraArrayItemGenerico.ConvertirMascaraModulosAdicionales(AplicacionSettings.Default.MascaraModulosAdicionalesHabilitados).Any(m => m.Id == (int)ModulosAdicionales.Restaurante))
            {
                menu.Add(new NavBar
                { Id = 1800, Nombre = "Restaurante", Icono = "mdi mdi-food", Estado = true, EsPadre = true, IdPadre = 0, Roles = new List<string> { "AdministradorNegocio", "CajeroRestaurante", "MozoRestaurante", "PreparadorRestaurante" } });
                menu.Add(new NavBar
                { Id = 1801, Nombre = "Atención", Icono = "mdi mdi-content-paste", Controlador = "Restaurante", Accion = "Index", Estado = true, EsPadre = false, IdPadre = 1800, Roles = new List<string> { "CajeroRestaurante", "MozoRestaurante" } });
                if (RestauranteSettings.Default.ModuloPreparacionActivado)
                    menu.Add(new NavBar
                    { Id = 1802, Nombre = "Preparación", Icono = "mdi mdi-nutriton", Estado = true, Controlador = "Restaurante", Accion = "Preparacion", EsPadre = false, IdPadre = 1800, Roles = new List<string> { "PreparadorRestaurante" } });
                menu.Add(new NavBar { Id = 1803, Nombre = "Caja", Icono = "mdi mdi-tag-text-outline", Controlador = "Restaurante", Accion = "Caja", Estado = true, EsPadre = false, IdPadre = 1800, Roles = new List<string> { "CajeroRestaurante" } });
                menu.Add(new NavBar { Id = 1804, Nombre = "Complementos", Icono = "mdi mdi-tag-text-outline", Controlador = "Restaurante", Accion = "Complementos", Estado = true, EsPadre = false, IdPadre = 1800, Roles = new List<string> { "AdministradorNegocio" } });
                menu.Add(new NavBar
                { Id = 1805, Nombre = "Reportes", Icono = "mdi mdi-tag-text-outline", Controlador = "RestauranteReportes", Accion = "Reportes", Estado = true, EsPadre = false, IdPadre = 1800, Roles = new List<string> { "CajeroRestaurante", "AdministradorNegocio" } });
            }

            #endregion

            #region Control Visitas
            menu.Add(new NavBar { Id = 1900, Nombre = "Control Visitas", Icono = "fa fa-shield fa-fw", Estado = true, EsPadre = true, IdPadre = 0, Roles = new List<string> { "Vigilante" } });
            menu.Add(new NavBar { Id = 1901, Nombre = "Ingresos", Controlador = "ControlVisitas", Accion = "Ingresos", Icono = "fa fa-plug fa-fw", Estado = true, EsPadre = false, IdPadre = 1900, Roles = new List<string> { "Vigilante" } });
            menu.Add(new NavBar { Id = 1902, Nombre = "Reporte por Fecha", Controlador = "ControlVisitas", Accion = "Reporte_fecha", Icono = "fa fa-plug fa-fw", Estado = true, EsPadre = false, IdPadre = 1900, Roles = new List<string> { "AdministradorNegocio" } });
            menu.Add(new NavBar { Id = 1604, Nombre = "Reporte por Dni", Controlador = "ControlVisitas", Accion = "Reporte_dni", Icono = "fa fa-plug fa-fw", Estado = true, EsPadre = false, IdPadre = 1600, Roles = new List<string> { "AdministradorNegocio" } });
            #endregion

            #region Hotel
            if (MascaraArrayItemGenerico.ConvertirMascaraModulosAdicionales(AplicacionSettings.Default.MascaraModulosAdicionalesHabilitados).Any(m => m.Id == (int)ModulosAdicionales.Hotel))
            {
                menu.Add(new NavBar { Id = 2000, Nombre = "Hotel", Icono = "fa fa-h-square fa-fw", Estado = true, EsPadre = true, IdPadre = 0, Roles = new List<string> { "AdministradorNegocio", "RecepcionistaHotel", } });
                menu.Add(new NavBar { Id = 2001, Nombre = "Planificador", Controlador = "Hotel", Accion = "Index", Icono = "fa fa-user-circle-o fa-fw", Estado = true, EsPadre = false, IdPadre = 2000, Roles = new List<string> { "AdministradorNegocio", "RecepcionistaHotel" } });
                menu.Add(new NavBar { Id = 2002, Nombre = "Reservas", Controlador = "Hotel", Accion = "Reservas", Icono = "fa fa-user-circle-o fa-fw", Estado = true, EsPadre = false, IdPadre = 2000, Roles = new List<string> { "AdministradorNegocio", "RecepcionistaHotel" } });
                menu.Add(new NavBar { Id = 2003, Nombre = "Consumos", Controlador = "Hotel", Accion = "Consumos", Icono = "fa fa-user-circle-o fa-fw", Estado = true, EsPadre = false, IdPadre = 2000, Roles = new List<string> { "AdministradorNegocio", "RecepcionistaHotel" } });
                menu.Add(new NavBar { Id = 2004, Nombre = "Habitaciones", Controlador = "Hotel", Accion = "Habitaciones", Icono = "fa fa-user-circle-o fa-fw", Estado = true, EsPadre = false, IdPadre = 2000, Roles = new List<string> { "AdministradorNegocio" } });
                //menu.Add(new NavBar { Id = 2004, Nombre = "Complementos", Controlador = "Hotel", Accion = "Complementos", Icono = "fa fa-user-circle-o fa-fw", Estado = true, EsPadre = false, IdPadre = 2000, Roles = new List<string> { "Hotelero" } });
                menu.Add(new NavBar { Id = 2005, Nombre = "Tipos de Habitacion", Controlador = "Hotel", Accion = "TipoHabitacion", Icono = "fa fa-user-circle-o fa-fw", Estado = true, EsPadre = false, IdPadre = 2000, Roles = new List<string> { "AdministradorNegocio" } });
                menu.Add(new NavBar { Id = 2006, Nombre = "Ambientes", Controlador = "Hotel", Accion = "Ambientes", Icono = "fa fa-user-circle-o fa-fw", Estado = true, EsPadre = false, IdPadre = 2000, Roles = new List<string> { "AdministradorNegocio" } });
                menu.Add(new NavBar { Id = 2007, Nombre = "Reportes", Controlador = "HotelReportes", Accion = "Reportes", Icono = "fa fa-user-circle-o fa-fw", Estado = true, EsPadre = false, IdPadre = 2000, Roles = new List<string> { "AdministradorNegocio" } });
            }
            #endregion

            //servicio
            /////menu.Add(new NavBar { Id = 18, Nombre = "Servicio",Icono= "mdi mdi-cube-send", Estado = true, EsPadre = true, IdPadre = 0 });
            /////menu.Add(new NavBar { Id = 19, Nombre = "Nuevo servicio", Controlador = "Concepto", Accion = "Servicio", Icono = "mdi mdi-cube-send", Estado = true, EsPadre = false, IdPadre = 18 });
            /////menu.Add(new NavBar { Id = 20, Nombre = "Ver servicios", Controlador = "Concepto", Accion = "Consulta_servicio", Icono = "mdi mdi-cube-send", Estado = true, EsPadre = false, IdPadre = 18 });
            //persona
           
            return menu.ToList();
        }
    }
}
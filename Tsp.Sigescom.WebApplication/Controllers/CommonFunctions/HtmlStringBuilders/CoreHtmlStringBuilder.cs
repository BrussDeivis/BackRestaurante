using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.ComprobantesModel;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Interfaces.Logica;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public static class CoreHtmlStringBuilder
    {



        public static string ObtenerHtmlString(OrdenDeVenta ordenDeVenta, FormatoImpresion formato, byte[] qrBytes, EstablecimientoComercialExtendidoConLogo sede, Controller controller, IMaestroLogica maestroLogica)
        {
            StringBuilder sbControlHtml = new StringBuilder();
            string result = "";
            string nombreVista = "";
            if (ordenDeVenta.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta)
            {
                nombreVista = formato == FormatoImpresion._80mm ? "../Venta/BoletaDeVenta80" : "../Venta/BoletaDeVentaA4";
                result = HtmlStringBuilder.RenderRazorViewToString(nombreVista, new BoletaDeVenta(ordenDeVenta, sede, new EstablecimientoComercialExtendido(ordenDeVenta.Transaccion().Actor_negocio2.Actor_negocio2), qrBytes, AplicacionSettings.Default.MostrarCabeceraVoucher, (ModoImpresionCaracteristicasEnum)VentasSettings.Default.modoImpresionCaracteristicas), controller);
            }
            else if (ordenDeVenta.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteFactura)
            {
                nombreVista = formato == FormatoImpresion._80mm ? "../Venta/Factura80" : "../Venta/FacturaA4";

                result = HtmlStringBuilder.RenderRazorViewToString(nombreVista, new Factura(ordenDeVenta, sede, new EstablecimientoComercialExtendido(ordenDeVenta.Transaccion().Actor_negocio2.Actor_negocio2), qrBytes, AplicacionSettings.Default.MostrarCabeceraVoucher, (ModoImpresionCaracteristicasEnum)VentasSettings.Default.modoImpresionCaracteristicas), controller);
            }
            else if (ordenDeVenta.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaVentaInterna)
            {
                nombreVista = formato == FormatoImpresion._80mm ? "../Venta/NotaDeVenta80" : "../Venta/NotaDeVentaA4";
                result = HtmlStringBuilder.RenderRazorViewToString(nombreVista, new NotaDeVenta(ordenDeVenta, sede, new EstablecimientoComercialExtendido(ordenDeVenta.Transaccion().Actor_negocio2.Actor_negocio2), qrBytes, AplicacionSettings.Default.MostrarCabeceraVoucher, (ModoImpresionCaracteristicasEnum)VentasSettings.Default.modoImpresionCaracteristicas), controller);
            }
            else if (ordenDeVenta.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaInvalidacionVenta)
            {
                nombreVista = formato == FormatoImpresion._80mm ? "../Venta/NotaInvalidacionVenta80" : "../Venta/NotaInvalidacionVentaA4";
                result = HtmlStringBuilder.RenderRazorViewToString(nombreVista, new NotaInvalidacionVenta(ordenDeVenta, sede, new EstablecimientoComercialExtendido(ordenDeVenta.Transaccion().Actor_negocio2.Actor_negocio2), qrBytes, AplicacionSettings.Default.MostrarCabeceraVoucher, (ModoImpresionCaracteristicasEnum)VentasSettings.Default.modoImpresionCaracteristicas), controller);
            }
            else if (ordenDeVenta.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCreditoInterna)
            {
                List<Detalle_maestro> tiposNotasDeCredito = maestroLogica.ObtenerDetallesMaestros(MaestroSettings.Default.IdMaestroTipoDeNotaDeCreditoElectronica);
                nombreVista = formato == FormatoImpresion._80mm ? "../NotaCreditoDebito/NotaDeCreditoInterna80" : "../NotaCreditoDebito/NotaDeCreditoInternaA4";
                result = HtmlStringBuilder.RenderRazorViewToString(nombreVista, new NotaDeCreditoInterna(ordenDeVenta, sede, new EstablecimientoComercialExtendido(ordenDeVenta.Transaccion().Actor_negocio2.Actor_negocio2), tiposNotasDeCredito, qrBytes, AplicacionSettings.Default.MostrarCabeceraVoucher, (ModoImpresionCaracteristicasEnum)VentasSettings.Default.modoImpresionCaracteristicas), controller);
            }
            else if (ordenDeVenta.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito && ordenDeVenta.OperacionDeReferencia().IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta)
            {
                List<Detalle_maestro> tiposNotasDeCredito = maestroLogica.ObtenerDetallesMaestros(MaestroSettings.Default.IdMaestroTipoDeNotaDeCreditoElectronica);
                nombreVista = formato == FormatoImpresion._80mm ? "../NotaCreditoDebito/NotaDeCreditoBoleta80" : "../NotaCreditoDebito/NotaDeCreditoBoletaA4";
                result = HtmlStringBuilder.RenderRazorViewToString(nombreVista, new NotaDeCredito(ordenDeVenta, sede, new EstablecimientoComercialExtendido(ordenDeVenta.Transaccion().Actor_negocio2.Actor_negocio2), tiposNotasDeCredito, qrBytes, AplicacionSettings.Default.MostrarCabeceraVoucher, (ModoImpresionCaracteristicasEnum)VentasSettings.Default.modoImpresionCaracteristicas), controller);
            }
            else if (ordenDeVenta.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaDeDebito && ordenDeVenta.OperacionDeReferencia().IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta)
            {
                List<Detalle_maestro> tiposNotasDeDebito = maestroLogica.ObtenerDetallesMaestros(MaestroSettings.Default.IdMaestroTipoDeNotaDeDebitoElectronica);
                nombreVista = formato == FormatoImpresion._80mm ? "../NotaCreditoDebito/NotaDeDebitoBoleta80" : "../NotaCreditoDebito/NotaDeDebitoBoletaA4";
                result = HtmlStringBuilder.RenderRazorViewToString(nombreVista, new NotaDeDebito(ordenDeVenta, sede, new EstablecimientoComercialExtendido(ordenDeVenta.Transaccion().Actor_negocio2.Actor_negocio2), tiposNotasDeDebito, qrBytes, AplicacionSettings.Default.MostrarCabeceraVoucher, (ModoImpresionCaracteristicasEnum)VentasSettings.Default.modoImpresionCaracteristicas), controller);
            }
            else if (ordenDeVenta.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito && ordenDeVenta.OperacionDeReferencia().IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteFactura)
            {
                List<Detalle_maestro> tiposNotasDeCredito = maestroLogica.ObtenerDetallesMaestros(MaestroSettings.Default.IdMaestroTipoDeNotaDeCreditoElectronica);
                nombreVista = formato == FormatoImpresion._80mm ? "../NotaCreditoDebito/NotaDeCreditoFactura80" : "../NotaCreditoDebito/NotaDeCreditoFacturaA4";
                result = HtmlStringBuilder.RenderRazorViewToString(nombreVista, new NotaDeCredito(ordenDeVenta, sede, new EstablecimientoComercialExtendido(ordenDeVenta.Transaccion().Actor_negocio2.Actor_negocio2), tiposNotasDeCredito, qrBytes, AplicacionSettings.Default.MostrarCabeceraVoucher, (ModoImpresionCaracteristicasEnum)VentasSettings.Default.modoImpresionCaracteristicas), controller);
            }
            else if (ordenDeVenta.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaDeDebito && ordenDeVenta.OperacionDeReferencia().IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteFactura)
            {
                List<Detalle_maestro> tiposNotasDeDebito = maestroLogica.ObtenerDetallesMaestros(MaestroSettings.Default.IdMaestroTipoDeNotaDeDebitoElectronica);
                nombreVista = formato == FormatoImpresion._80mm ? "../NotaCreditoDebito/NotaDeDebitoFactura80" : "../NotaCreditoDebito/NotaDeDebitoFacturaA4";
                result = HtmlStringBuilder.RenderRazorViewToString(nombreVista, new NotaDeDebito(ordenDeVenta, sede, new EstablecimientoComercialExtendido(ordenDeVenta.Transaccion().Actor_negocio2.Actor_negocio2), tiposNotasDeDebito, qrBytes, AplicacionSettings.Default.MostrarCabeceraVoucher, (ModoImpresionCaracteristicasEnum)VentasSettings.Default.modoImpresionCaracteristicas), controller);
            }
            return result;
        }

        public static string ObtenerHtmlString(OrdenDeCompra ordenDeCompra, FormatoImpresion formato, byte[] qrBytes, EstablecimientoComercialExtendidoConLogo sede, Controller controller, IMaestroLogica maestroLogica)
        {
            StringBuilder sbControlHtml = new StringBuilder();
            string result = "";
            string nombreVista = "";

            if (ordenDeCompra.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta)
            {
                nombreVista = formato == FormatoImpresion._80mm ? "../Compra/BoletaDeCompra80" : "../Compra/BoletaDeCompraA4";
                result = HtmlStringBuilder.RenderRazorViewToString(nombreVista, new BoletaDeCompra(ordenDeCompra, sede, qrBytes, AplicacionSettings.Default.MostrarCabeceraVoucher, (ModoImpresionCaracteristicasEnum)VentasSettings.Default.modoImpresionCaracteristicas), controller);
            }
            else if (ordenDeCompra.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteFactura)
            {
                nombreVista = formato == FormatoImpresion._80mm ? "../Compra/FacturaDeCompra80" : "../Compra/FacturaDeCompraA4";

                result = HtmlStringBuilder.RenderRazorViewToString(nombreVista, new FacturaCompra(ordenDeCompra, sede, qrBytes, AplicacionSettings.Default.MostrarCabeceraVoucher, (ModoImpresionCaracteristicasEnum)VentasSettings.Default.modoImpresionCaracteristicas), controller);
            }
            else if (ordenDeCompra.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCompraInterna)
            {
                nombreVista = formato == FormatoImpresion._80mm ? "../Compra/NotaDeCompra80" : "../Compra/NotaDeCompraA4";
                result = HtmlStringBuilder.RenderRazorViewToString(nombreVista, new NotaDeCompra(ordenDeCompra, sede, qrBytes, AplicacionSettings.Default.MostrarCabeceraVoucher, (ModoImpresionCaracteristicasEnum)VentasSettings.Default.modoImpresionCaracteristicas), controller);
            }
            else if (ordenDeCompra.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaInvalidacionCompra)
            {
                nombreVista = formato == FormatoImpresion._80mm ? "../Compra/NotaInvalidacionCompra80" : "../Compra/NotaInvalidacionCompraA4";
                result = HtmlStringBuilder.RenderRazorViewToString(nombreVista, new NotaInvalidacionCompra(ordenDeCompra, sede, qrBytes, AplicacionSettings.Default.MostrarCabeceraVoucher, (ModoImpresionCaracteristicasEnum)VentasSettings.Default.modoImpresionCaracteristicas), controller);
            }
            else if (ordenDeCompra.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito && ordenDeCompra.OperacionDeReferencia().IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta)
            {
                List<Detalle_maestro> tiposNotasDeCredito = maestroLogica.ObtenerDetallesMaestros(MaestroSettings.Default.IdMaestroTipoDeNotaDeCreditoElectronica);
                nombreVista = formato == FormatoImpresion._80mm ? "../NotaCreditoDebito/NotaDeCreditoBoletaDeCompra80" : "../NotaCreditoDebito/NotaDeCreditoBoletaDeCompraA4";
                result = HtmlStringBuilder.RenderRazorViewToString(nombreVista, new NotaDeCreditoCompra(ordenDeCompra, sede, tiposNotasDeCredito, qrBytes, AplicacionSettings.Default.MostrarCabeceraVoucher, (ModoImpresionCaracteristicasEnum)VentasSettings.Default.modoImpresionCaracteristicas), controller);
            }
            else if (ordenDeCompra.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaDeDebito && ordenDeCompra.OperacionDeReferencia().IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta)
            {
                List<Detalle_maestro> tiposNotasDeDebito = maestroLogica.ObtenerDetallesMaestros(MaestroSettings.Default.IdMaestroTipoDeNotaDeDebitoElectronica);
                nombreVista = formato == FormatoImpresion._80mm ? "../NotaCreditoDebito/NotaDeDebitoBoletaDeCompra80" : "../NotaCreditoDebito/NotaDeDebitoBoletaDeCompraA4";
                result = HtmlStringBuilder.RenderRazorViewToString(nombreVista, new NotaDeDebitoCompra(ordenDeCompra, sede, tiposNotasDeDebito, qrBytes, AplicacionSettings.Default.MostrarCabeceraVoucher, (ModoImpresionCaracteristicasEnum)VentasSettings.Default.modoImpresionCaracteristicas), controller);
            }
            else if (ordenDeCompra.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito && ordenDeCompra.OperacionDeReferencia().IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteFactura)
            {
                List<Detalle_maestro> tiposNotasDeCredito = maestroLogica.ObtenerDetallesMaestros(MaestroSettings.Default.IdMaestroTipoDeNotaDeCreditoElectronica);
                nombreVista = formato == FormatoImpresion._80mm ? "../NotaCreditoDebito/NotaDeCreditoFacturaDeCompra80" : "../NotaCreditoDebito/NotaDeCreditoFacturaDeCompraA4";
                result = HtmlStringBuilder.RenderRazorViewToString(nombreVista, new NotaDeCreditoCompra(ordenDeCompra, sede, tiposNotasDeCredito, qrBytes, AplicacionSettings.Default.MostrarCabeceraVoucher, (ModoImpresionCaracteristicasEnum)VentasSettings.Default.modoImpresionCaracteristicas), controller);
            }
            else if (ordenDeCompra.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaDeDebito && ordenDeCompra.OperacionDeReferencia().IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteFactura)
            {
                List<Detalle_maestro> tiposNotasDeDebito = maestroLogica.ObtenerDetallesMaestros(MaestroSettings.Default.IdMaestroTipoDeNotaDeDebitoElectronica);
                nombreVista = formato == FormatoImpresion._80mm ? "../NotaCreditoDebito/NotaDeDebitoFacturaDeCompra80" : "../NotaCreditoDebito/NotaDeDebitoFacturaDeCompraA4";
                result = HtmlStringBuilder.RenderRazorViewToString(nombreVista, new NotaDeDebitoCompra(ordenDeCompra, sede, tiposNotasDeDebito, qrBytes, AplicacionSettings.Default.MostrarCabeceraVoucher, (ModoImpresionCaracteristicasEnum)VentasSettings.Default.modoImpresionCaracteristicas), controller);
            }
            return result;
        }

        public static string ObtenerHtmlString(OrdenDeMovimientoDeAlmacen ordenDeMovimiento, FormatoImpresion formato, byte[] qrBytes, EstablecimientoComercialExtendido sede, Controller controller)
        {
            StringBuilder sbControlHtml = new StringBuilder();
            string result = "";
            string nombreVista = "";

            if (ordenDeMovimiento.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteOrdenDeAlmacen)
            {
                nombreVista = formato == FormatoImpresion._80mm ? "../Almacen/OrdenDeAlmacen80" : "../Almacen/OrdenDeAlmacenA4";
                result = HtmlStringBuilder.RenderRazorViewToString(nombreVista, new OrdenDeAlmacen(ordenDeMovimiento, sede, new EstablecimientoComercialExtendido(ordenDeMovimiento.Transaccion().Actor_negocio2.Actor_negocio2), qrBytes, AplicacionSettings.Default.MostrarCabeceraVoucher, (ModoImpresionCaracteristicasEnum)VentasSettings.Default.modoImpresionCaracteristicas), controller);
            }
            return result;
        }

        public static string ObtenerHtmlString(MovimientoDeAlmacen movimientoDeAlmacen, FormatoImpresion formato, byte[] qrBytes, EstablecimientoComercialExtendidoConLogo sede, List<Proveedor> proveedores, List<Detalle_maestro> modalidadesDeTraslado, List<Detalle_maestro> motivosDeTraslado, Controller controller)
        {
            StringBuilder sbControlHtml = new StringBuilder();
            string result = "";
            string nombreVista = "";

            if (movimientoDeAlmacen.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteGuiaDeRemisionRemitente)
            {
                nombreVista = formato == FormatoImpresion._80mm ? "../Almacen/GuiaDeRemision80" : "../Almacen/GuiaDeRemisionA4";
                result = HtmlStringBuilder.RenderRazorViewToString(nombreVista, new GuiaDeRemision(movimientoDeAlmacen, sede, new EstablecimientoComercialExtendido(movimientoDeAlmacen.Transaccion().Actor_negocio2.Actor_negocio2), qrBytes, AplicacionSettings.Default.MostrarCabeceraVoucher, (ModoImpresionCaracteristicasEnum)VentasSettings.Default.modoImpresionCaracteristicas, proveedores, modalidadesDeTraslado, motivosDeTraslado), controller);
            }
            else if (movimientoDeAlmacen.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaAlmacenInterna)
            {
                nombreVista = formato == FormatoImpresion._80mm ? "../Almacen/NotaDeAlmacen80" : "../Almacen/NotaDeAlmacenA4";
                result = HtmlStringBuilder.RenderRazorViewToString(nombreVista, new NotaDeAlmacen(movimientoDeAlmacen, sede, new EstablecimientoComercialExtendido(movimientoDeAlmacen.Transaccion().Actor_negocio2.Actor_negocio2), qrBytes, AplicacionSettings.Default.MostrarCabeceraVoucher, (ModoImpresionCaracteristicasEnum)VentasSettings.Default.modoImpresionCaracteristicas), controller);
            }
            else
            {
                nombreVista = formato == FormatoImpresion._80mm ? "../Almacen/MovimientoAlmacen80" : "../Almacen/MovimientoAlmacenA4";
                result = HtmlStringBuilder.RenderRazorViewToString(nombreVista, new DocumentoMovimientoDeAlmacen(movimientoDeAlmacen, sede, new EstablecimientoComercialExtendido(movimientoDeAlmacen.Transaccion().Actor_negocio2.Actor_negocio2), qrBytes, AplicacionSettings.Default.MostrarCabeceraVoucher, (ModoImpresionCaracteristicasEnum)VentasSettings.Default.modoImpresionCaracteristicas), controller);
            }
            return result;
        }

        public static string ObtenerHtmlString(MovimientoEconomico movimiento, FormatoImpresion formato, EstablecimientoComercialExtendidoConLogo sede, Controller controller)
        {
            StringBuilder sbControlHtml = new StringBuilder();
            string result = "";
            string nombreVista = "";
            nombreVista = formato == FormatoImpresion._80mm ? "../Finanza/ReciboDeIngresoEgreso80" : "../Finanza/ReciboDeIngresoEgreso80";
            result = HtmlStringBuilder.RenderRazorViewToString(nombreVista, ReciboDeIngresoEgreso.Convert(movimiento, sede, new EstablecimientoComercialExtendido(movimiento.Transaccion().Actor_negocio2.Actor_negocio2), AplicacionSettings.Default.MostrarCabeceraVoucher), controller);
            return result;
        }
    }



}
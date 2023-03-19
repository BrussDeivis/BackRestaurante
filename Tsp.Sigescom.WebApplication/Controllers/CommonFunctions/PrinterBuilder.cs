using Microsoft.Reporting.WebForms;
using Neodynamic.SDK.Web;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Pedido;
using Tsp.Sigescom.Modelo.ClasesNegocio.EBookViewModel.Custom;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.ComprobantesModel;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Entidades.Sesion;
using Tsp.Sigescom.Modelo.Interfaces.Infraestructura;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Interfaces.Negocio.Pedido;
using Tsp.Sigescom.WebApplication.Models;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public class PrinterBuilder
    {
        public void ImprimirOperacion(long idOperacion, int tipoOperacion, EstablecimientoComercialExtendidoConLogo sede, PedidoController pedidoController, IPedido_Logica pedidoLogica, IOperacionLogica operacionLogica, IActorNegocioLogica actorNegocioLogica, IMaestroLogica maestroLogica, IFacturacionElectronicaLogica facturacionElectronicaLogica, IBarCodeUtil barCodeUtil, IPdfUtil pdfUtil)
        {
            if (tipoOperacion == 1)
            {
                OrdenDePedido orden = pedidoLogica.ObtenerOrdenDePedidoParaImprimir(ObtenerOrdenDePedidoParaImprimirEnAplicacion(idOperacion));
                EliminarOrdenDePedidoParaImprimirEnAplicacion(idOperacion);
                PrintFile_(orden, sede, pedidoController, maestroLogica, pdfUtil);
            }
            else if (tipoOperacion == 2)
            {
                OrdenDeVenta orden = operacionLogica.ObtenerOrdenDeVentaParaImprimir(ObtenerOrdenDeVentaParaImprimirEnAplicacion(idOperacion));
                EliminarOrdenDeVentaParaImprimirEnAplicacion(idOperacion);
                PrintFile_(orden, sede, pedidoController, operacionLogica, actorNegocioLogica, maestroLogica, facturacionElectronicaLogica, barCodeUtil, pdfUtil);
            }
        }

        public void PrintFile_(OrdenDePedido orden, EstablecimientoComercialExtendidoConLogo sede, PedidoController pedidoController, IMaestroLogica maestroLogica, IPdfUtil pdfUtil)
        {
            var PDFventa = ObtenerPdfPedido(orden, sede, null, (FormatoImpresion)VentasSettings.Default.formatoImpresionPorDefecto, pedidoController, maestroLogica, pdfUtil);
            //Si tiene asignado guias de remision
            PrintFilePDF file = new PrintFilePDF(PDFventa.Save(), orden.Id + ".pdf");
            file.PrintRotation = PrintRotation.None;

            ClientPrintJob cpj = new ClientPrintJob();
            cpj.PrintFile = file;
            cpj.PrintFile.Copies = AplicacionSettings.Default.NumeroCopiasAImprimirComprobanteVenta;
            cpj.ClientPrinter = new DefaultPrinter();

            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.BinaryWrite(cpj.GetContent());
            HttpContext.Current.Response.End();
        }

        public PdfDocument ObtenerPdfPedido(OrdenDePedido ordenDePedido, EstablecimientoComercialExtendidoConLogo sede, byte[] qrBytes, FormatoImpresion formato, PedidoController pedidoController, IMaestroLogica maestroLogica, IPdfUtil pdfUtil)
        {
            string htmlString = pedidoController.ObtenerCadenaHtmlDePedido(ordenDePedido, formato, qrBytes, sede);
            return pdfUtil.ObtenerPdfDocumento(htmlString, formato);
        }

        #region Armado Orden de Pedido y Guardado en Variables de Aplicacion para Imprimir Pedidos
        public OrdenDePedido ArmarOrdenPedidoParaImprimir(OrdenDePedido ordenDePedido, DatosVentaIntegrada pedido, UserProfileSessionData sesionUsuario)
        {
            ordenDePedido.Transaccion().Detalle_maestro1 = sesionUsuario.MaestrosFrecuentes.Moneda.Convert();
            ordenDePedido.Transaccion().Comprobante.Detalle_maestro = pedido.Orden.Comprobante.Tipo.Convert();
            ordenDePedido.Transaccion().Actor_negocio = sesionUsuario.Empleado.Convert();
            ordenDePedido.Transaccion().Actor_negocio1 = pedido.Orden.Cliente.Convert();
            //ordenDeVenta.Transaccion().Actor_negocio2 = ProfileData().CentroDeAtencionSeleccionado;
            foreach (var item in ordenDePedido.Transaccion().Detalle_transaccion)
            {
                item.registro = pedido.Orden.Placa;
                item.Concepto_negocio = new Concepto_negocio()
                {
                    id = pedido.Orden.Detalles.FirstOrDefault(d => d.Producto.Id == item.id_concepto_negocio).Producto.Id,
                    codigo = pedido.Orden.Detalles.FirstOrDefault(d => d.Producto.Id == item.id_concepto_negocio).Producto.Codigo,
                    nombre = pedido.Orden.Detalles.FirstOrDefault(d => d.Producto.Id == item.id_concepto_negocio).Producto.NombreConcepto,
                    Detalle_maestro4 = new Detalle_maestro() { valor = pedido.Orden.Detalles.FirstOrDefault(d => d.Producto.Id == item.id_concepto_negocio).Producto.EsBien ? "1" : "0" }
                };
            }
            return ordenDePedido;
        }

        public void GuardarOrdenDePedidoParaImprimirEnAplicacion(OrdenDePedido ordenDePedido)
        {
            List<OrdenDePedido> ordenesDePedido = (List<OrdenDePedido>)HttpContext.Current.Application["PedidosAImprimir"] ?? new List<OrdenDePedido>();
            //Agregar a las ordenes de venta de la variable de alicacion la nueva orden de venta
            ordenesDePedido.Add(ordenDePedido);
            //Guardar en la variable de aplicacion a las ordenes de venta
            HttpContext.Current.Application.Lock();
            HttpContext.Current.Application["PedidosAImprimir"] = ordenesDePedido;
            HttpContext.Current.Application.UnLock();
        }

        public OrdenDePedido ObtenerOrdenDePedidoParaImprimirEnAplicacion(long idOrdenDePedido)
        {
            //Obtener la lista de ordenes de pedido de la variable de aplicacion
            List<OrdenDePedido> ordenesDePedidos = (List<OrdenDePedido>)HttpContext.Current.Application["PedidosAImprimir"];
            //Buscar en la lista de las ordenes de pedido el idOrdenDePedido
            OrdenDePedido ordenDePedido = ordenesDePedidos.Single(ov => ov.Id == idOrdenDePedido);
            //Retornar la orden de pedido encontrada
            return ordenDePedido;
        }

        public void EliminarOrdenDePedidoParaImprimirEnAplicacion(long idOrdenDePedido)
        {
            //Obtener la lista de ordenes de pedido de la variable de aplicacion
            List<OrdenDePedido> ordenesDePedido = (List<OrdenDePedido>)HttpContext.Current.Application["PedidosAImprimir"];
            //Buscar en la lista de las ordenes de pedido el idOrdenDeVenta 
            ordenesDePedido.Remove(ordenesDePedido.Single(ov => ov.Id == idOrdenDePedido));
            //Guardar en la variable de aplicacion a las ordenes de pedido
            HttpContext.Current.Application.Lock();
            HttpContext.Current.Application["PedidosAImprimir"] = ordenesDePedido;
            HttpContext.Current.Application.UnLock();
        }
        #endregion

        public void PrintFile_(OrdenDeVenta orden, EstablecimientoComercialExtendidoConLogo sede, Controller controller, IOperacionLogica operacionLogica, IActorNegocioLogica actorNegocioLogica, IMaestroLogica maestroLogica, IFacturacionElectronicaLogica facturacionElectronicaLogica, IBarCodeUtil barCodeUtil, IPdfUtil pdfUtil)
        {

            string QrContent = facturacionElectronicaLogica.ObtenerQR(orden, sede);
            byte[] QrBytes = barCodeUtil.ObtenerCodigoQR(QrContent);
            var PDFventa = ObtenerPdfVenta(orden, sede, QrBytes, (FormatoImpresion)VentasSettings.Default.formatoImpresionPorDefecto, controller, maestroLogica, pdfUtil);

            //Si tiene asignado guias de remision
            if (orden.TieneGuiaDeRemision())
            {
                var proveedores = actorNegocioLogica.ObtenerProveedoresVigentes();
                var modalidadesDeTransporte = maestroLogica.ObtenerModalidadesTraslado();
                var motivosDeTransporte = maestroLogica.ObtenerMotivosTraslado();
                var salidaDeMercaderia = operacionLogica.ObtenerSalidaDeMercaderiaDeVenta(orden.IdVenta);
                foreach (var salida in salidaDeMercaderia)
                {
                    int[] idsUbigeos = { Convert.ToInt32(salida.IdUbigeoOrigenDeTraslado()), Convert.ToInt32(salida.IdUbigeoDestinoDeTraslado()) };
                    var ubigeos = maestroLogica.obtenerUbigeo(idsUbigeos);
                    salida.UbigeoOrigen = ubigeos.Single(u => u.id == Convert.ToInt32(salida.IdUbigeoOrigenDeTraslado())).descripcion_corta;
                    salida.UbigeoDestino = ubigeos.Single(u => u.id == Convert.ToInt32(salida.IdUbigeoDestinoDeTraslado())).descripcion_corta;
                    byte[] byteQr = barCodeUtil.ObtenerCodigoQR(salida.UrlDocumentoSunat);
                    PDFventa.Append(PdfBuilder.ObtenerPdfMovimientoDeMercaderia(salida, sede, byteQr, (FormatoImpresion)VentasSettings.Default.formatoImpresionPorDefecto, proveedores, modalidadesDeTransporte, motivosDeTransporte, controller));
                }
            }
            PrintFilePDF file = new PrintFilePDF(PDFventa.Save(), orden.Id + ".pdf");
            file.PrintRotation = PrintRotation.None;

            ClientPrintJob cpj = new ClientPrintJob();
            cpj.PrintFile = file;
            cpj.PrintFile.Copies = AplicacionSettings.Default.NumeroCopiasAImprimirComprobanteVenta;
            cpj.ClientPrinter = new DefaultPrinter();

            System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
            System.Web.HttpContext.Current.Response.BinaryWrite(cpj.GetContent());
            System.Web.HttpContext.Current.Response.End();
        }

        public PdfDocument ObtenerPdfVenta(OrdenDeVenta ordenDeVenta, EstablecimientoComercialExtendidoConLogo sede, byte[] qrBytes, FormatoImpresion formato, Controller controller, IMaestroLogica maestroLogica, IPdfUtil pdfUtil)
        {
            string htmlString = CoreHtmlStringBuilder.ObtenerHtmlString(ordenDeVenta, formato, qrBytes, sede, controller, maestroLogica);
            return pdfUtil.ObtenerPdfDocumento(htmlString, formato);
        }

        #region Armado Orden de Venta y Guardado en Variables de Aplicacion para Imprimir Ventas
        public OrdenDeVenta ArmarOrdenVentaParaImprimir(OrdenDeVenta ordenDeVenta, DatosVentaIntegrada venta, UserProfileSessionData sesionUsuario)
        {
            ordenDeVenta.Transaccion().Detalle_maestro1 = sesionUsuario.MaestrosFrecuentes.Moneda.Convert();
            ordenDeVenta.Transaccion().Comprobante.Detalle_maestro = venta.Orden.Comprobante.Tipo.Convert();
            ordenDeVenta.Transaccion().Actor_negocio = sesionUsuario.Empleado.Convert();
            ordenDeVenta.Transaccion().Actor_negocio1 = venta.Orden.Cliente.Convert();
            //ordenDeVenta.Transaccion().Actor_negocio2 = ProfileData().CentroDeAtencionSeleccionado;
            foreach (var item in ordenDeVenta.Transaccion().Detalle_transaccion)
            {
                item.registro = venta.Orden.Placa;
                item.Concepto_negocio = new Concepto_negocio()
                {
                    id = venta.Orden.Detalles.FirstOrDefault(d => d.Producto.Id == item.id_concepto_negocio).Producto.Id,
                    codigo = venta.Orden.Detalles.FirstOrDefault(d => d.Producto.Id == item.id_concepto_negocio).Producto.Codigo,
                    nombre = venta.Orden.Detalles.FirstOrDefault(d => d.Producto.Id == item.id_concepto_negocio).Producto.NombreConcepto,
                    Detalle_maestro4 = new Detalle_maestro() { valor = venta.Orden.Detalles.FirstOrDefault(d => d.Producto.Id == item.id_concepto_negocio).Producto.EsBien ? "1" : "0" }
                };
            }
            return ordenDeVenta;
        }
        public void GuardarOrdenDeVentaParaImprimirEnAplicacion(OrdenDeVenta ordenDeVenta)
        {
            //MEJORAR EL ARMADO DE LA ORDEN DE VENTA, CON LOS DATOS DEL REGISTRO DE VENTA

            //Obtener la lista de ordenes de venta de la variable de aplicacion
            List<OrdenDeVenta> ordenesDeVenta = (List<OrdenDeVenta>)HttpContext.Current.Application["VentasAImprimir"] ?? new List<OrdenDeVenta>();
            //Agregar a las ordenes de venta de la variable de alicacion la nueva orden de venta
            ordenesDeVenta.Add(ordenDeVenta);
            //Guardar en la variable de aplicacion a las ordenes de venta
            HttpContext.Current.Application.Lock();
            HttpContext.Current.Application["VentasAImprimir"] = ordenesDeVenta;
            HttpContext.Current.Application.UnLock();
        }

        public OrdenDeVenta ObtenerOrdenDeVentaParaImprimirEnAplicacion(long idOrdenDeVenta)
        {
            //Obtener la lista de ordenes de venta de la variable de aplicacion
            List<OrdenDeVenta> ordenesDeVenta = (List<OrdenDeVenta>)HttpContext.Current.Application["VentasAImprimir"];
            //Buscar en la lista de las ordenes de venta el idOrdenDeVenta 
            OrdenDeVenta ordenDeVenta = ordenesDeVenta.Single(ov => ov.Id == idOrdenDeVenta);
            //Retornar la orden de venta encontrada
            return ordenDeVenta;
        }

        public void EliminarOrdenDeVentaParaImprimirEnAplicacion(long idOrdenDeVenta)
        {
            //Obtener la lista de ordenes de venta de la variable de aplicacion
            List<OrdenDeVenta> ordenesDeVenta = (List<OrdenDeVenta>)HttpContext.Current.Application["VentasAImprimir"];
            //Buscar en la lista de las ordenes de venta el idOrdenDeVenta 
            ordenesDeVenta.Remove(ordenesDeVenta.Single(ov => ov.Id == idOrdenDeVenta));
            //Guardar en la variable de aplicacion a las ordenes de venta
            HttpContext.Current.Application.Lock();
            HttpContext.Current.Application["VentasAImprimir"] = ordenesDeVenta;
            HttpContext.Current.Application.UnLock();
        }
        #endregion
    }
}
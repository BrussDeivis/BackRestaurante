using Microsoft.Reporting.WebForms;
using OpenInvoicePeru.Comun.Dto.Intercambio;
using OpenInvoicePeru.Comun.Dto.Modelos;
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
using Tsp.Sigescom.Modelo.ClasesNegocio.EBookViewModel.Custom;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.ComprobantesModel;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Utilitarios.RestHelper;
using Tsp.Sigescom.WebApplication.Models;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public static class XmlBuilder
    {
        public static DocumentoXml ObtenerXmlComprobante(OrdenDeVenta ordenDeVenta, EstablecimientoComercialExtendido sede, IMaestroLogica maestroLogica, IGeneracionArchivosLogica generacionArchivosLogica, IFacturacionElectronicaLogica facturacionElectronicaLogica)
        {
            try
            {
                string metodoApi = "", nombreArchivo = "";
                DocumentoElectronico documento;
                switch (ordenDeVenta.Comprobante().CodigoTipo)
                {
                    case "07":
                        metodoApi = "api/GenerarNotaCredito";
                        var tiposNotasDeCredito = maestroLogica.ObtenerDetallesMaestros(MaestroSettings.Default.IdMaestroTipoDeNotaDeCreditoElectronica);
                        var notaDeCredito = new NotaDeCredito(ordenDeVenta, sede, new EstablecimientoComercialExtendido(ordenDeVenta.Transaccion().Actor_negocio2.Actor_negocio2) , tiposNotasDeCredito, null, false, (ModoImpresionCaracteristicasEnum)VentasSettings.Default.modoImpresionCaracteristicas);
                        documento = generacionArchivosLogica.ObtenerDocumentoElectronicoNotaCredito(notaDeCredito);
                        break;
                    case "08":
                        metodoApi = "api/GenerarNotaDebito";
                        var tiposNotasDeDebito = maestroLogica.ObtenerDetallesMaestros(MaestroSettings.Default.IdMaestroTipoDeNotaDeDebitoElectronica);
                        var notaDeDebito = new NotaDeDebito(ordenDeVenta, sede, new EstablecimientoComercialExtendido(ordenDeVenta.Transaccion().Actor_negocio2.Actor_negocio2), tiposNotasDeDebito, null, false, (ModoImpresionCaracteristicasEnum)VentasSettings.Default.modoImpresionCaracteristicas);
                        documento = generacionArchivosLogica.ObtenerDocumentoElectronicoNotaDebito(notaDeDebito);
                        break;
                    default:
                        metodoApi = "api/GenerarFactura";
                        var factura = new Factura(ordenDeVenta, sede, new EstablecimientoComercialExtendido(ordenDeVenta.Transaccion().Actor_negocio2.Actor_negocio2), null, false, (ModoImpresionCaracteristicasEnum)VentasSettings.Default.modoImpresionCaracteristicas);
                        documento = generacionArchivosLogica.ObtenerDocumentoElectronicoFactura(factura);
                        break;
                }
                var documentoResponse = RestHelper<DocumentoElectronico, DocumentoResponse>.Execute(metodoApi, documento, FacturacionElectronicaSettings.Default.UrlApiFacturacionElectronica);
                nombreArchivo = documento.IdDocumento;
                if (!documentoResponse.Exito)
                {
                    throw new ControllerException(documentoResponse.MensajeError);
                }
                return FimarXmlComprobante(sede, facturacionElectronicaLogica, documentoResponse, nombreArchivo);
            }
            catch (Exception ex)
            {
                throw new ControllerException("Error al descargar el XML del comprobante", ex);
            }
        }

        public static DocumentoXml ObtenerXmlComprobante(MovimientoDeAlmacen movimientoDeAlmacen, EstablecimientoComercialExtendido sede, List<Proveedor> proveedores, List<Detalle_maestro> modalidadesDeTraslado, List<Detalle_maestro> motivosDeTraslado, IGeneracionArchivosLogica generacionArchivosLogica, IFacturacionElectronicaLogica facturacionElectronicaLogica)
        {
            try
            {
                string metodoApi = "", nombreArchivo = "";
                metodoApi = "api/GenerarGuiaRemision";
                var guiaDeRemision = new GuiaDeRemision(movimientoDeAlmacen, sede, new EstablecimientoComercialExtendido(movimientoDeAlmacen.Transaccion().Actor_negocio2.Actor_negocio2), null, false, (ModoImpresionCaracteristicasEnum)VentasSettings.Default.modoImpresionCaracteristicas, proveedores, modalidadesDeTraslado, motivosDeTraslado);
                var guiaRemision = generacionArchivosLogica.ObtenerDocumentoElectronicoGuiaDeRemision(guiaDeRemision);
                var documentoResponse = RestHelper<GuiaRemision, DocumentoResponse>.Execute(metodoApi, guiaRemision, FacturacionElectronicaSettings.Default.UrlApiFacturacionElectronica);
                nombreArchivo = guiaRemision.IdDocumento;
                if (!documentoResponse.Exito)
                {
                    throw new ControllerException(documentoResponse.MensajeError);
                }
                return FimarXmlComprobante(sede, facturacionElectronicaLogica, documentoResponse, nombreArchivo);
            }
            catch (Exception ex)
            {
                throw new ControllerException("Error al descargar el XML del comprobante", ex);
            }
        }

        public static DocumentoXml FimarXmlComprobante(EstablecimientoComercial sede, IFacturacionElectronicaLogica facturacionElectronicaLogica, DocumentoResponse documentoResponse, string nombreArchivo)
        {
            try
            {
                byte[] archivoCertificado = facturacionElectronicaLogica.ObtenerCertificado(sede.DocumentoIdentidad);
                var firmado = new FirmadoRequest
                {
                    TramaXmlSinFirma = documentoResponse.TramaXmlSinFirma,
                    CertificadoDigital = Convert.ToBase64String(archivoCertificado),
                    PasswordCertificado = FacturacionElectronicaSettings.Default.ClaveCertificadoDigital,
                };
                var responseFirma = RestHelper<FirmadoRequest, FirmadoResponse>.Execute("api/Firmar", firmado, FacturacionElectronicaSettings.Default.UrlApiFacturacionElectronica);
                if (!responseFirma.Exito)
                {
                    throw new ControllerException(responseFirma.MensajeError);
                }
                byte[] xmlFirmadoByte = Convert.FromBase64String(responseFirma.TramaXmlFirmado);
                //byte[] xmlFirmadoByte = Convert.FromBase64String(documentoResponse.TramaXmlSinFirma);
                var documentoXml = new DocumentoXml { NombreDocumento = nombreArchivo, Documento = xmlFirmadoByte };
                return documentoXml;
            }
            catch (Exception ex)
            {
                throw new ControllerException("Error al descargar el XML del comprobante", ex);
            }
        }
    }
}
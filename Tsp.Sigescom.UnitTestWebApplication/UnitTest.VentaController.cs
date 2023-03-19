//using System;
//using System.Text;
//using System.IO;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Collections.Generic;
//using Tsp.Sigescom.WebApplication.Controllers;
//using Tsp.Sigescom.WebApplication.Models;
//using Tsp.Sigescom.Modelo.Interfaces.Logica;
//using Tsp.Sigescom.Inyeccion;
//using Tsp.Sigescom.Modelo.Entidades;
//using Tsp.FacturacionElectronica.Logica;
//using System.Configuration;
//using OpenInvoicePeru.Comun.Dto.Modelos;
//using System.Threading.Tasks;
//using System.Net.Http;

//namespace Tsp.Sigescom.UnitTestWebApplication
//{
//    [TestClass]
//    public class UnitTest
//    {

//        private readonly IFacturacionLogica _logicaFacturacion;


//        private readonly VentaController ventaController;
//        private readonly RegistroVentaViewModel ventaViewModel;
//        public GeneracionArchivosLogica generacionArchivosLogica = new GeneracionArchivosLogica();
//        private FacturacionElectronicaLogica _facturacionElectronicaLogica = new FacturacionElectronicaLogica();


//        DateTime fecha = new DateTime(2018, 05, 11, 12, 00, 00);

//        public UnitTest()
//        {
//            _logicaFacturacion = Dependencia.Resolve<IFacturacionLogica>();
//            ventaController = new VentaController();
//            ventaViewModel = new RegistroVentaViewModel();
//        }
//        public async Task ResolverDocumentoElectronico(long idVenta)
//        {
            

//                try
//                {
//                    DocumentoElectronico _documento = generacionArchivosLogica.obtenerDocumentoElectronico(result.data);

//                    var proxy = new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["UrlOpenInvoicePeruApi"]) };

//                    string metodoApi;
//                    switch (_documento.TipoDocumento)
//                    {
//                        case "07":
//                            metodoApi = "api/GenerarNotaCredito";
//                            break;
//                        case "08":
//                            metodoApi = "api/GenerarNotaDebito";
//                            break;
//                        default:
//                            metodoApi = "api/GenerarFactura";
//                            break;
//                    }

//                    var response = await proxy.PostAsJsonAsync(metodoApi, _documento);
//                    var respuesta = await response.Content.ReadAsAsync<DocumentoResponse>();

//                    if (!respuesta.Exito)
//                    {
//                        throw new ApplicationException(respuesta.MensajeError);
//                    }
//                    else
//                    {

//                        _facturacionElectronicaLogica.crearComprobanteElectronico(idVenta, _documento.TipoDocumento, _logicaMaestro.obtenerDetalleCatalogoDocumentoSunat(_documento.TipoDocumento).nombre, _documento.IdDocumento.Split('-')[0], _documento.IdDocumento.Split('-')[1], DateTime.Parse(_documento.FechaEmision), Convert.FromBase64String(respuesta.TramaXmlSinFirma));
//                    }

//                }
//                catch (Exception ex)
//                {
//                    ex.Message.ToString();
//                }
            
//        }

//        [TestMethod]
//        public async Task GuardarVentaTestMethod()
//        {


//            List<Detalle_transaccion> detalles = new List<Detalle_transaccion>();
//            detalles.Add(new Detalle_transaccion(1,1,"",1,1,null,0,null,null,0,0,0));

//            var result = _logicaFacturacion.guardarRegistroDeVentaConFechaActual(DateTime.Now, 5, 11, 3, "prueba unitaria", 281, null, "", detalles);
//            if (Convert.ToString(result.code_result) == "Success")
//            {
//                await ResolverDocumentoElectronico(result.data);
//            }
//        }
//    }
//}


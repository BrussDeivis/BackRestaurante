using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Custom.SigesParking;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Interfaces.Infraestructura;
using Tsp.Sigescom.Modelo.Interfaces.Logica;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public class CocheraController : BaseController
    {
        private readonly ICocheraLogica cocheraLogica;
        protected readonly IBarCodeUtil barCodeUtil;
        protected readonly IOperacionLogica operacionLogica;
        protected readonly IMaestroLogica maestroLogica;
        protected readonly IFacturacionElectronicaLogica facturacionElectronicaLogica;


        public CocheraController()
        {
            cocheraLogica = Dependencia.Resolve<ICocheraLogica>();
            barCodeUtil = Dependencia.Resolve<IBarCodeUtil>();
            operacionLogica = Dependencia.Resolve<IOperacionLogica>();
            maestroLogica = Dependencia.Resolve<IMaestroLogica>();
            facturacionElectronicaLogica = Dependencia.Resolve<IFacturacionElectronicaLogica>();

        }

        SesionCochera SesionCochera 
        { 
            get {
                return (SesionCochera)this.Session["SesionCochera"];
                }
            set {
                this.Session["SesionCochera"] = value;
            } 
        }

        public ActionResult Index()
        {
            //GUARDAMOS EN SESION LOS DATOS DE LA COCHERA, TURNOS, CONFIGURACION, PRECIOS
            SesionCochera = cocheraLogica.ObtenerSesion(ProfileData());

            ViewBag.fechaInicio =DateTimeUtil.FechaActual().AddDays(-7).ToString("dd/MM/yyyy");
            ViewBag.fechaFin =DateTimeUtil.FechaActual().ToString("dd/MM/yyyy");
            ViewBag.IdSistemDePagoPlanaPorTurnos = (int)SistemaPagoCochera.PLN;
            ViewBag.IdSistemDePagoPorHora = (int)SistemaPagoCochera.HOR;
            ViewBag.IdSistemDePagoAbonados = (int)SistemaPagoCochera.ABN;
            ViewBag.IdEstadoIngresado = MaestroSettings.Default.IdDetalleMaestroEstadoIngresado;
            ViewBag.IdEstadoFinalizado = MaestroSettings.Default.IdDetalleMaestroEstadoFinalizado;
            ViewBag.IdMedioPagoDefault = MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo;
            ViewBag.PrecioPerdidaTicket = SesionCochera.Precio(SesionCochera.Configuracion.IdConceptoPerdidaDeTicket).Importe;
            ViewBag.IdRolCliente = ActorSettings.Default.IdRolCliente;
            ViewBag.NombreRolCliente = "CLIENTE";
            ViewBag.AplicaLeyAmazonia = TransaccionSettings.Default.AplicaLeyAmazonia;
            ViewBag.TasaIGV = TransaccionSettings.Default.TasaIGV;
            ViewBag.TiempoEsperaBusquedaSelector = AplicacionSettings.Default.TiempoDeEsperaEnBusquedaSelectoresDeGranCantidad;
            ViewBag.MinimoCaracteresBuscarActorComercial = ActorSettings.Default.MinimoDeCaracteresParaBuscarEnSelectorActorComercial;
            ViewBag.MascaraDeVisualizacionValidacionRegistroCliente = ActorSettings.Default.MascaraDeVisualizacionValidacionRegistroCliente;
            return View();
        }

        public ActionResult Exoneraciones()
        {
            ViewBag.fechaInicio =DateTimeUtil.FechaActual().AddDays(-7).ToString("dd/MM/yyyy");
            ViewBag.fechaFin =DateTimeUtil.FechaActual().ToString("dd/MM/yyyy");
            return View();
        }

        public JsonResult ObtenerMovimientosCochera(string desde, string hasta)
        {
            DateTime fechaDesde = DateTime.Parse(desde);
            //todo> resolver problema de bordes
            DateTime fechaHasta = DateTime.Parse(hasta + " 23:59:59");
            try
            {
                List<MovimientoCocheraBasico> ordenesDeVehiculos = cocheraLogica.ObtenerMovimientosDatosBasicos(fechaDesde, fechaHasta, ProfileData().IdCentroDeAtencionSeleccionado).OrderByDescending(ov=>ov.Id).ToList();
                return Json(ordenesDeVehiculos);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult ObtenerVehiculoParaEditarPorPlaca(string placa)
        {
            try
            {
                var vehiculo = cocheraLogica.ObtenerVehiculoPorPlaca(placa);
                return Json(vehiculo);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
                

        public JsonResult ObtenerVehiculoPorPlaca(string placa)
        {
            try
            {
                var vehiculo = cocheraLogica.ObtenerVehiculoPorPlaca(placa);
                 return Json(new { encontrado = vehiculo != null, data = vehiculo });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult ObtenerTiposDeVehiculo()
        {
            try
            {
                return Json(cocheraLogica.ObtenerTiposDeVehiculo());
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult ObtenerExoneraciones(int idCochera)
        {
            try
            {
                var exoneraciones = cocheraLogica.ObtenerVehiculosExonerados(idCochera);
                return Json(exoneraciones);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ObtenerMarcasDeVehiculo()
        {
            try
            {
                List<ItemGenerico> marcas = cocheraLogica.ObtenerMarcasDeVehiculo();
                return Json(marcas);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult ObtenerCocheras()
        {
            try
            {
                List<ItemGenerico> cocheras = cocheraLogica.ObtenerCocheras();
                return Json(cocheras);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult ObtenerSistemaDePagoParaCochera()
        {
            try
            {
                List<ItemGenerico> sistemasDePago = cocheraLogica.ObtenerSistemasDePago();
                return Json(sistemasDePago);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
       

        public JsonResult GuardarVehiculo(Vehiculo vehiculo)
        {
            try
            {
                OperationResult resultado = null;
                if (vehiculo.Id != 0)
                {
                    resultado = cocheraLogica.EditarVehiculo(vehiculo);
                }
                else
                {
                    resultado = cocheraLogica.RegistrarVehiculo(vehiculo);
                }
                Util.ManageIfResultIsNotSuccess(resultado, "ERROR AL GUARDAR VEHICULO");
                var information = resultado.information;//aqui debe estar el vechiculo con todos sus ids despues de haber sido guardado en bd.
                return new JsonHttpStatusResult(new { resultado.code_result, resultado.data, result_description = resultado.title, information}, HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ExonerarVehiculo(Vehiculo vehiculo, int idCochera)
        {
            try
            {
                OperationResult resultado = null;
                resultado = cocheraLogica.ExonerarVehiculo(vehiculo, idCochera);
                Util.ManageIfResultIsNotSuccess(resultado, "ERROR AL REGISTRAR EXONERACION DE VEHICULO");
               return new JsonHttpStatusResult(new { resultado.code_result, resultado.data, result_description = resultado.title}, HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult QuitarExoneracionAVehiculo(ExoneracionDeVehiculo exoneracion)
        {
            try
            {
                OperationResult resultado = null;
                resultado = cocheraLogica.QuitarExoneracionAVehiculo(exoneracion);
                Util.ManageIfResultIsNotSuccess(resultado, "ERROR AL QUITAR EXONERACION A VEHICULO");
                return new JsonHttpStatusResult(new { resultado.code_result, resultado.data, result_description = resultado.title }, HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult RegistrarIngreso(Ingreso ingreso)
        {
            try
            {
                OperationResult resultado = null;
                resultado = cocheraLogica.RegistrarIngreso(SesionCochera, ingreso);
                Util.ManageIfResultIsNotSuccess(resultado, "ERROR AL REGISTRAR INGRESO A COCHERA");
                var nombreVista = "../Cochera/TicketIngresoCochera80_";
                var result =HtmlStringBuilder.RenderRazorViewToString(nombreVista, resultado.information, this);

                return new JsonHttpStatusResult(new { resultado.code_result, resultado.data, result_description = resultado.title, information = result }, HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                //TODO: POR EJEMPLO EN ESTAS PARTES DE LOS CONTROLADORES, SE PODRIA ENVIAR UN CORREO A SOPORTE SIGES CADA VEZ QUE OCURRA UN ERROR EN UN AMBIENTE PRODUCCION, SIEMPRE Y CUANDO SEA UN DESPLIEGUE EN LINEA, DE OTRO MODO, SE PODRIA ALMACENAR LOS ERRORES EN UN ARCHIVO DE TEXTO POR CADA DIA. OSEA IMPLEMENTAR UN LOG DE ERRORES Y SI ESTA EN LINEA, QUE SE ENVIE ADJUNTO POR CORREO CADA DIA.
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        /// <summary>
        /// busca el movimiento de cochera con la placa indicada, al encontrarlo, devuelve el movimiento con los datos para procesar la salida: importes a pagar, fecha y hora actual, etc. En caso de no encontrar un vehiculo ingresado con esa placa, retorna error del servidor.
        /// </summary>
        /// <param name="movimiento"></param>
        /// <returns></returns>
        public JsonResult ObtenerMovimientoParaSalida(string placa)
        {
            try
            {
                MovimientoCochera movimiento = cocheraLogica.ObtenerMovimientoParaSalida(placa, SesionCochera);
                return Json(new { encontrado = movimiento!=null, data = movimiento });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult RegistrarSalida(MovimientoCochera salida, DatosVentaIntegrada datosVentaIntegrada)
        {
            try
            {
                ResultadoRegistroMovimientoCochera resultado = cocheraLogica.RegistrarSalida(salida, datosVentaIntegrada, SesionCochera);
                var nombreVistaTicketSalida = "../Cochera/TicketSalidaCochera80_";
                var ticketHtmlString = HtmlStringBuilder.RenderRazorViewToString(nombreVistaTicketSalida, new TicketSalidaCochera(SesionCochera.SesionDeUsuario.Sede, SesionCochera.SesionDeUsuario.EstablecimientoComercialSeleccionado, resultado.Movimiento, resultado.Movimiento.Comprobante.Serie, Convert.ToInt32(resultado.Movimiento.Comprobante.Numero), barCodeUtil.ObtenerCodigoBarras(resultado.Movimiento.Vehiculo.Placa)), this);
                var ordenDeVenta = operacionLogica.ObtenerOrdenDeVentaParaImprimir(resultado.OrdenDeVenta);
                string qrContent = facturacionElectronicaLogica.ObtenerQR(ordenDeVenta, SesionCochera.SesionDeUsuario.Sede);
                byte[] qrBytes = barCodeUtil.ObtenerCodigoQR(qrContent);
                var comprobanteVentaHtmlString = CoreHtmlStringBuilder.ObtenerHtmlString(ordenDeVenta, FormatoImpresion._80mm, qrBytes, SesionCochera.SesionDeUsuario.Sede, this, maestroLogica);   
                return new JsonHttpStatusResult(new { OperationResultEnum.Success  , result_description = "SE REGISTRÓ LA SALIDA CON ÉXITO", ticket=ticketHtmlString, comprobanteVenta= comprobanteVentaHtmlString}, HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }

        public ActionResult Reportes()
        {
            ViewBag.fechaHoraInicio = DateTime.Now.AddDays(-30);
            ViewBag.fechaHoraFin = DateTime.Now;
            ViewBag.cantidadMaximaDias = 30;//todo: parametrizar
            return View();
        }



        public ActionResult ReporteVehiculosIngresados(int idCochera, string nombreCochera)
        {
            var cochera = new ItemGenerico { Id = idCochera, Nombre = nombreCochera };
            var rptviewer = GenerarReporteVehiculosIngresados(cochera, true);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }

        public ReportViewer GenerarReporteVehiculosIngresados(ItemGenerico cochera, bool fromRequest)
        {
            List<EntradaSalidaUsuario> vehiculosIngresados = cocheraLogica.ObtenerVehiculosIngresados(cochera.Id);
            ReportParameter nombreSede = new ReportParameter("NombreSede", ObtenerSede().Nombre);
            ReportParameter nombreCentroAtencion = new ReportParameter("NombreCentroAtencion", cochera.Nombre);
            var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100)};
            string path = @"/Content/reports/Cochera/VehiculosIngresados.rdlc";
            rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
            rptviewer.LocalReport.SetParameters(new ReportParameter[] {nombreSede, nombreCentroAtencion});
            ReportDataSource rptdatasource = new ReportDataSource("vehiculosIngresadosDataset", vehiculosIngresados);
            rptviewer.LocalReport.DataSources.Add(rptdatasource);
            return rptviewer;
        }

        public ActionResult ReporteIngresosYSalidas(int idCochera, string nombreCochera, DateTime desde, DateTime hasta)
        {
            var cochera = new ItemGenerico { Id = idCochera, Nombre = nombreCochera };

            var rptviewer = GenerarReporteIngresosYSalidas(cochera, desde, hasta, true);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }

        public ReportViewer GenerarReporteIngresosYSalidas(ItemGenerico cochera, DateTime desde, DateTime hasta, bool fromRequest)
        {
            List<EntradaSalida> ingresosYSalidas = cocheraLogica.ObtenerEntradasYSalidas(cochera.Id,desde, hasta);
            ReportParameter nombreSede = new ReportParameter("NombreSede", ObtenerSede().Nombre);
            ReportParameter nombreCentroAtencion = new ReportParameter("NombreCentroAtencion", cochera.Nombre);
            string path = @"/Content/reports/Cochera/IngresosYSalidas.rdlc";
            ReportDataSource rptdatasource = new ReportDataSource("entradasYSalidasDataset", ingresosYSalidas);
            var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
            rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { nombreSede, nombreCentroAtencion });
            rptviewer.LocalReport.DataSources.Add(rptdatasource);
            return rptviewer;
        }
         
        public ActionResult ReporteExoneraciones(int idCochera, string nombreCochera)
        {
            var cochera = new ItemGenerico { Id = idCochera, Nombre = nombreCochera };
            var rptviewer = GenerarReporteExoneraciones(cochera, true);
            ViewBag.ReportViewer = rptviewer;
            return View("VisualizadorReporte");
        }

        public ReportViewer GenerarReporteExoneraciones(ItemGenerico cochera, bool fromRequest)
        {
            List<ExoneracionDeVehiculo_Flat> ingresosYSalidas = ExoneracionDeVehiculo_Flat.Convert(cocheraLogica.ObtenerVehiculosExoneradosVigentes(cochera.Id)) ;
            ReportParameter nombreSede = new ReportParameter("NombreSede", ObtenerSede().Nombre);
            ReportParameter nombreCentroAtencion = new ReportParameter("NombreCentroAtencion", cochera.Nombre);
            string path = @"/Content/reports/Cochera/VehiculosExonerados.rdlc";
            ReportDataSource rptdatasource = new ReportDataSource("exoneracionesDataset", ingresosYSalidas);
            var rptviewer = new ReportViewer() { ProcessingMode = ProcessingMode.Local, SizeToReportContent = true, Width = Unit.Percentage(100), Height = Unit.Percentage(100) };
            rptviewer.LocalReport.ReportPath = fromRequest ? Request.MapPath(Request.ApplicationPath) + path : HostingEnvironment.MapPath(path);
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { nombreSede, nombreCentroAtencion });
            rptviewer.LocalReport.DataSources.Add(rptdatasource);
            return rptviewer;
        }
    }
}
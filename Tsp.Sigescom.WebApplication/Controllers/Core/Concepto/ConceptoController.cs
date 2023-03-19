using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.WebApplication.Models;
using System.Web.UI.WebControls;
using Tsp.Sigescom.Utilitarios;
using System.Threading.Tasks;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public class ConceptoController : BaseController
    {
        private readonly IMaestroLogica _logicaMaestro;
        private readonly IConceptoLogica _logicaConcepto;
        private readonly IActorNegocioLogica _actorNegocioLogica;
        protected readonly ICentroDeAtencion_Logica centroDeAtencion_Logica;

        public ConceptoController()
        {
            _logicaMaestro = Dependencia.Resolve<IMaestroLogica>();
            _logicaConcepto = Dependencia.Resolve<IConceptoLogica>();
            _actorNegocioLogica = Dependencia.Resolve<IActorNegocioLogica>();
            centroDeAtencion_Logica = Dependencia.Resolve<ICentroDeAtencion_Logica>();


        }


        #region GETS

        [Authorize(Roles = "Comprador")]
        public ActionResult Concepto()
        {
            ViewBag.ValorPrecioVentaPorDefectoQueNoSeDebeGuardar = AplicacionSettings.Default.ValorPrecioVentaPorDefectoQueNoSeDebeGuardar;
            ViewBag.IdUnidadDeMedidaSubUnidad = MaestroSettings.Default.IdDetalleMaestroUnidadDeMedidaSubUnidad;
            ViewBag.IdPresentacionPorDefecto = ConceptoSettings.Default.idPresentacionPorDefecto;
            ViewBag.IdUnidadMedidaPorDefecto = ConceptoSettings.Default.idUnidadMedidaPorDefecto;
            ViewBag.IdPresentacionAOcultarEnNombreConceptoNegocio = ConceptoSettings.Default.IdPresentacionAOcultarEnNombreConceptoNegocio;
            ViewBag.IdUnidadDeMedidaComercialAOcultarEnNombreConceptoNegocio = ConceptoSettings.Default.IdUnidadDeMedidaComercialAOcultarEnNombreConceptoNegocio;
            ViewBag.numeroDecimalesEnCantidad = AplicacionSettings.Default.NumeroDecimalesEnCantidad;
            ViewBag.numeroDecimalesEnPrecio = AplicacionSettings.Default.NumeroDecimalesEnPrecio;
            ViewBag.modoDeSeleccionTipoFamiliaEnRegistroFamilia = ConceptoSettings.Default.ModoDeSeleccionTipoFamiliaEnRegistroFamilia;
            ViewBag.mostrarCampoCodigoAlRegistrarConcepto = ConceptoSettings.Default.MostrarCampoCodigoAlRegistrarConcepto;
            ViewBag.mostrarCampoCodigoDigemidAlRegistrarConcepto = ConceptoSettings.Default.PermitirRegistroCodigoDigemidEnConceptoComercial;
            ViewBag.modulosAdicionales = MascaraArrayItemGenerico.ConvertirMascaraModulosAdicionales(AplicacionSettings.Default.MascaraModulosAdicionalesHabilitados);
            return View();

        }

        [Authorize(Roles = "Comprador")]
        public ActionResult Conceptos()
        {
            ViewBag.idEstablecimientoPorDefecto = ProfileData().CentroDeAtencionSeleccionado.EstablecimientoComercial.Id;
            ViewBag.idCentroDeAtencionPorDefecto = ProfileData().CentroDeAtencionSeleccionado.Id;
            ViewBag.IdPresentacionPorDefecto = ConceptoSettings.Default.idPresentacionPorDefecto;
            ViewBag.IdUnidadMedidaPorDefecto = ConceptoSettings.Default.idUnidadMedidaPorDefecto;
            ViewBag.ValorPrecioVentaPorDefectoQueNoSeDebeGuardar = AplicacionSettings.Default.ValorPrecioVentaPorDefectoQueNoSeDebeGuardar;
            ViewBag.IdUnidadDeMedidaSubUnidad = MaestroSettings.Default.IdDetalleMaestroUnidadDeMedidaSubUnidad;
            ViewBag.IdPresentacionAOcultarEnNombreConceptoNegocio = ConceptoSettings.Default.IdPresentacionAOcultarEnNombreConceptoNegocio;
            ViewBag.IdUnidadDeMedidaComercialAOcultarEnNombreConceptoNegocio = ConceptoSettings.Default.IdUnidadDeMedidaComercialAOcultarEnNombreConceptoNegocio;
            ViewBag.numeroDecimalesEnCantidad = AplicacionSettings.Default.NumeroDecimalesEnCantidad;
            ViewBag.numeroDecimalesEnPrecio = AplicacionSettings.Default.NumeroDecimalesEnPrecio;
            ViewBag.modoDeSeleccionTipoFamiliaEnRegistroFamilia = ConceptoSettings.Default.ModoDeSeleccionTipoFamiliaEnRegistroFamilia;
            ViewBag.mostrarCampoCodigoAlRegistrarConcepto = ConceptoSettings.Default.MostrarCampoCodigoAlRegistrarConcepto;
            ViewBag.mostrarCampoCodigoDigemidAlRegistrarConcepto = ConceptoSettings.Default.PermitirRegistroCodigoDigemidEnConceptoComercial;
            ViewBag.modulosAdicionales = MascaraArrayItemGenerico.ConvertirMascaraModulosAdicionales(AplicacionSettings.Default.MascaraModulosAdicionalesHabilitados);
            return View();
        }

        [Authorize(Roles = "Comprador")]
        public ActionResult ConsultarConceptos()
        {
            ViewBag.ValorPrecioVentaPorDefectoQueNoSeDebeGuardar = AplicacionSettings.Default.ValorPrecioVentaPorDefectoQueNoSeDebeGuardar;
            ViewBag.IdUnidadDeMedidaSubUnidad = MaestroSettings.Default.IdDetalleMaestroUnidadDeMedidaSubUnidad;
            ViewBag.IdPresentacionPorDefecto = ConceptoSettings.Default.idPresentacionPorDefecto;
            ViewBag.IdUnidadMedidaPorDefecto = ConceptoSettings.Default.idUnidadMedidaPorDefecto;
            ViewBag.IdPresentacionAOcultarEnNombreConceptoNegocio = ConceptoSettings.Default.IdPresentacionAOcultarEnNombreConceptoNegocio;
            ViewBag.IdUnidadDeMedidaComercialAOcultarEnNombreConceptoNegocio = ConceptoSettings.Default.IdUnidadDeMedidaComercialAOcultarEnNombreConceptoNegocio;
            ViewBag.numeroDecimalesEnCantidad = AplicacionSettings.Default.NumeroDecimalesEnCantidad;
            ViewBag.numeroDecimalesEnPrecio = AplicacionSettings.Default.NumeroDecimalesEnPrecio;
            ViewBag.modoDeSeleccionTipoFamiliaEnRegistroFamilia = ConceptoSettings.Default.ModoDeSeleccionTipoFamiliaEnRegistroFamilia;
            ViewBag.mostrarCampoCodigoAlRegistrarConcepto = ConceptoSettings.Default.MostrarCampoCodigoAlRegistrarConcepto;
            ViewBag.mostrarCampoCodigoDigemidAlRegistrarConcepto = ConceptoSettings.Default.PermitirRegistroCodigoDigemidEnConceptoComercial;
            ViewBag.modulosAdicionales = MascaraArrayItemGenerico.ConvertirMascaraModulosAdicionales(AplicacionSettings.Default.MascaraModulosAdicionalesHabilitados);
            return View();
        }

        [Authorize(Roles = "AdministradorNegocio")]
        public ActionResult ConceptoBasico()
        {
            ViewBag.modoDeSeleccionTipoFamiliaEnRegistroFamilia = ConceptoSettings.Default.ModoDeSeleccionTipoFamiliaEnRegistroFamilia;
            return View();
        }

        [Authorize(Roles = "AdministradorNegocio")]
        public ActionResult ConsultarConceptosBasicos()
        {
            ViewBag.modoDeSeleccionTipoFamiliaEnRegistroFamilia = ConceptoSettings.Default.ModoDeSeleccionTipoFamiliaEnRegistroFamilia;
            return View();
        }

        [Authorize(Roles = "AdministradorNegocio")]
        public ActionResult Caracteristicas()
        {
            return View();
        }

        [Authorize(Roles = "AdministradorNegocio")]
        public ActionResult Caracteristica_(int c)
        {
            Detalle_maestro caracteristica = _logicaConcepto.ObtenerCaracteristica(c);
            ViewBag.id = caracteristica.id;
            ViewBag.nombre = caracteristica.nombre;

            return View();
        }

        [Authorize(Roles = "AdministradorNegocio")]
        public ActionResult Categorias()
        {
            ViewBag.IdCategoriaNula = ConceptoSettings.Default.IdCategoriaNula;
            return View();
        }

        public ActionResult Transformacion()
        {

            return View();
        }
        [Authorize(Roles = "Gerente,AdministradorNegocio")]
        public ActionResult ReportesAdministrador()
        {
            return View();
        }
        [Authorize(Roles = "Comprador,AdministradorNegocio")]
        public ActionResult ReporteDigemid()
        {
            return View();
        }
        [Authorize(Roles = "Almacenero,Gerente,AdministradorNegocio")]
        public ActionResult ReportesAlmacenero()
        {
            return View();
        }
        [Authorize(Roles = "AdministradorNegocio,JefeVenta")]
        public ActionResult Precio()
        {
            ViewBag.numeroDecimalesEnPrecio = AplicacionSettings.Default.NumeroDecimalesEnPrecio;
            ViewBag.mostrarBuscadorCodigoBarra = AplicacionSettings.Default.MostrarBuscadorCodigoBarraEnCotizacion;
            ViewBag.modoDeSeleccionDeConcepto = AplicacionSettings.Default.ModoDeSeleccionDeConceptoDeNegocioEnCotizacion;
            ViewBag.modoSeleccionTipoFamilia = AplicacionSettings.Default.ModoDeSeleccionTipoDeFamiliaEnCotizacion;
            ViewBag.minimoCaracteresBuscarConcepto = AplicacionSettings.Default.MinimoDeCaracteresParaBuscarEnSelectorConcepto;
            ViewBag.tiempoEsperaBusquedaSelector = AplicacionSettings.Default.TiempoDeEsperaEnBusquedaSelectoresDeGranCantidad;
            ViewBag.informacionSelectorConcepto = (int)InformacionSelectorConcepto.Nombre;
            ViewBag.idTarifaPorDefecto = MaestroSettings.Default.IdDetalleMaestroTarifaNormal;
            ViewBag.etiquetaSeleccionadaPorDefecto = ConceptoSettings.Default.EtiquetaSeleccionadaPorDefectoEnImpresionEtiquetas;
            ViewBag.altoEtiqueta3 = ConceptoSettings.Default.ParametrosEtiquetaImpresionEtiqueta3.Split('|')[0];
            ViewBag.anchoEtiqueta3 = ConceptoSettings.Default.ParametrosEtiquetaImpresionEtiqueta3.Split('|')[1];
            ViewBag.filasEtiqueta3 = ConceptoSettings.Default.ParametrosEtiquetaImpresionEtiqueta3.Split('|')[2];
            ViewBag.columnasEtiqueta3 = ConceptoSettings.Default.ParametrosEtiquetaImpresionEtiqueta3.Split('|')[3];
            ViewBag.fuenteEtiqueta3 = ConceptoSettings.Default.ParametrosEtiquetaImpresionEtiqueta3.Split('|')[4];
            ViewBag.altoEtiqueta4 = ConceptoSettings.Default.ParametrosEtiquetaImpresionEtiqueta4.Split('|')[0];
            ViewBag.anchoEtiqueta4 = ConceptoSettings.Default.ParametrosEtiquetaImpresionEtiqueta4.Split('|')[1];
            ViewBag.filasEtiqueta4 = ConceptoSettings.Default.ParametrosEtiquetaImpresionEtiqueta4.Split('|')[2];
            ViewBag.columnasEtiqueta4 = ConceptoSettings.Default.ParametrosEtiquetaImpresionEtiqueta4.Split('|')[3];
            ViewBag.fuenteEtiqueta4 = ConceptoSettings.Default.ParametrosEtiquetaImpresionEtiqueta4.Split('|')[4];
            return View();
        }
        //public ActionResult ReporteStockActual(int idAlmacen) //creo que nadies lo utiliza borrar OMAR
        //{
        //    List<Stock> stocks = _logicaConcepto.ObtenerExistencias(idAlmacen);
        //    List<StockViewModel> reporteStockViewModel = StockViewModel.Convert(stocks);

        //    ReportParameter entidadInterna = new ReportParameter("EntidadInterna", ProfileData().NombreCentroDeAtencionSeleccionado);
        //    ReportParameter fecha = new ReportParameter("Fecha",DateTimeUtil.FechaActual().ToString());
        //    ReportParameter nombreEmpresa = new ReportParameter("NombreEmpresa", ObtenerSede().RazonSocial);
        //    ReportParameter nombreAlmacen = new ReportParameter("NombreAlmacen", actorNegocioLogica.ObtenerCentroDeAtencion(idAlmacen).Nombre);

        //    var rptviewer = new ReportViewer();
        //    rptviewer.ProcessingMode = ProcessingMode.Local;

        //    rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"/Content/reports/StockFisicoEntidadInterna.rdlc";
        //    rptviewer.LocalReport.SetParameters(new ReportParameter[] { entidadInterna, fecha, nombreEmpresa, nombreAlmacen });

        //    ReportDataSource rptdatasourceStock = new ReportDataSource("StockFisicoDataSet", reporteStockViewModel);

        //    rptviewer.LocalReport.DataSources.Add(rptdatasourceStock);
        //    rptviewer.SizeToReportContent = true;
        //    ViewBag.ReportViewer = rptviewer;

        //    return View("VisualizadorReporte");
        //}

        #endregion

        #region MERCADERIAS

        public JsonResult GuardarProducto(ProductoViewModel producto)
        {
            try
            {
                OperationResult resultado;

                ComprobarSiYaExisteNombreDeUnProducto(producto.Id, producto.Nombre);

                if (producto.Id > 0)
                {
                    resultado = _logicaConcepto.ActualizarProducto(producto.Id, producto.CodigoBarra, producto.Nombre, producto.Codigo, producto.CodigoDigemid, producto.Sufijo, producto.Concepto.Id, producto.Concepto.EsBien, producto.UnidadDeMedidaCom.Id, producto.UnidadDeMedidaRef.Id, producto.IdsCaracteristicas?.ToArray(), producto.ModulosAdicionales?.ToArray(), producto.Presentacion.Id, producto.CantidadPresentacion, producto.UnidadDeMedidaPres.Id, producto.PresentacionSubContenido != null ? producto.PresentacionSubContenido.Id : (int?)null, producto.Foto.HayFoto ? Convert.FromBase64String(producto.Foto.Foto) : null, producto.Foto.HayFoto, RegistroPrecioViewModel.Convert(producto.PreciosVenta), producto.StockMinimo, ProfileData().Empleado.Id,/* TransaccionSettings.Default.PreciosCentralizados ? ObtenerSede().Id :*/ ProfileData().IdCentroAtencionQueTieneLosPrecios);
                }
                else
                {
                    resultado = _logicaConcepto.GuardarProducto(
                        producto.CodigoBarra, producto.Nombre, producto.Codigo, producto.CodigoDigemid, producto.Sufijo, producto.Concepto.Id, producto.Concepto.EsBien, producto.UnidadDeMedidaCom.Id, producto.UnidadDeMedidaRef.Id, producto.IdsCaracteristicas?.ToArray(), producto.ModulosAdicionales?.ToArray(), producto.Presentacion.Id, producto.CantidadPresentacion, producto.UnidadDeMedidaPres.Id, producto.PresentacionSubContenido != null ? producto.PresentacionSubContenido.Id : (int?)null, producto.Foto.HayFoto ? Convert.FromBase64String(producto.Foto.Foto) : null, producto.Foto.HayFoto, RegistroPrecioViewModel.Convert(producto.PreciosVenta), producto.StockMinimo, ProfileData().Empleado.Id, /*TransaccionSettings.Default.PreciosCentralizados ? ObtenerSede().Id :*/ ProfileData().IdCentroAtencionQueTieneLosPrecios);
                }
                Util.ManageIfResultIsNotSuccess(resultado, "ERROR AL REGISTRAR EL CONCEPTO");
                return Json(new { resultado.code_result, resultado.data, result_description = resultado.title });
            }
            catch (AdvertenciaException e)
            {
                return new JsonHttpStatusResult(new { error = e.Message, warning = true }, HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult EliminarMercaderia(int IdMercaderia)
        {
            try
            {
                OperationResult result = _logicaConcepto.DarDeBajaMercaderia(IdMercaderia);
                Util.VerificarError(result);
                return Json(new { code_result = result.code_result, data = result.data, result_description = result.title });
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerProducto(int idMercaderia)
        {
            try
            {
                List<ComboGenericoViewModel> puntosDePrecio = ComboGenericoViewModel.Convert(centroDeAtencion_Logica.ObtenerPuntosDePrecio());
                var tarifasDePrecio = ComboGenericoViewModel.Convert(_logicaMaestro.obtenerTarifas());
                var preciosCompraVenta = _logicaConcepto.ObtenerPreciosCompraVentaDeConceptoNegocio(idMercaderia);
                ProductoViewModel respuesta = new ProductoViewModel(_logicaConcepto.obtenerProducto(idMercaderia), preciosCompraVenta, puntosDePrecio, tarifasDePrecio,DateTimeUtil.FechaActual());
                return Json(respuesta);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al obtener producto", e)), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult ObtenerMercaderias()
        {
            try
            {
                List<BandejaProductoViewModel> respuesta = BandejaProductoViewModel.Convert_(_logicaConcepto.ObtenerMercaderiasPorConceptoBasicoIncluyendoStockYPrecios(ProfileData().IdCentroDeAtencionSeleccionado), ProfileData().IdCentroAtencionQueTieneLosPrecios);
                return Json(respuesta);
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerMercaderiasPorConceptoBasicoGenerico(int idConceptoBasico)
        {
            try
            {
                List<ComboGenericoViewModel> respuesta = ComboGenericoViewModel.Convert(_logicaConcepto.ObtenerMercaderiasPorConceptoBasico(idConceptoBasico));
                return Json(respuesta);
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerMercaderiasPorConceptoBasicoIncluyendoStockYPrecios(int idConceptoBasico)
        {
            try
            {
                //Metodo que se utiliza en compra en el combo para elegir concepto_negocio
                List<ConceptoDeNegocio> resultados = _logicaConcepto.ObtenerMercaderiasPorConceptoBasicoIncluyendoStockYPrecios(idConceptoBasico);
                List<ProductoParaCompraViewModel> productos = ProductoParaCompraViewModel.ConvertProductoParaCompra(resultados, ProfileData().IdCentroDeAtencionSeleccionado);
                return Json(productos);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("ERROR AL OBTENER PRODUCTOS", e)), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult ObtenerMercaderiaPorCodigoBarraIncluyendoStockYPrecios(string codigoBarra)
        {
            try
            {
                ConceptoDeNegocio resultado = _logicaConcepto.ObtenerMercaderiaPorCodigoBarraIncluyendoStockYPrecios(codigoBarra);
                if (resultado != null)
                {
                    ProductoParaCompraViewModel producto = new ProductoParaCompraViewModel(resultado, ProfileData().IdCentroDeAtencionSeleccionado);
                    return Json(producto);
                }
                else
                {
                    throw new AdvertenciaException("Producto no registrado o no tiene precio");
                }
            }
            catch (AdvertenciaException e)
            {
                return new JsonHttpStatusResult(new { error = e.Message, warning = true }, HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("ERROR AL OBTENER PRODUCTO", e)), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult ObtenerMercaderiasPorConceptoBasicoIncluyendoStockYPreciosVigentes(int idConceptoBasico)
        {
            try
            {
                int idCentroAtencionQueTieneLosPrecios = ProfileData().IdCentroAtencionQueTieneLosPrecios;
                int idCentroAtencionQueTieneLaExistencia = ProfileData().IdCentroAtencionQueTieneElStockIntegrada;
                List<ConceptoDeNegocio> resultados = _logicaConcepto.ObtenerMercaderiasIncluyendoStockYPrecios
                    (idConceptoBasico, idCentroAtencionQueTieneLosPrecios);
                List<ProductoParaVentaViewModel> productos = ProductoParaVentaViewModel.ConvertProductoParaVentaViewModel(resultados, idCentroAtencionQueTieneLosPrecios, idCentroAtencionQueTieneLaExistencia);
                return Json(productos);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult ObtenerMercaderiasFiltradas(int idConceptoBasico)
        {
            try
            {
                List<BandejaProductoViewModel> respuesta = BandejaProductoViewModel.Convert_(_logicaConcepto.ObtenerMercaderiasPorConceptoBasicoIncluyendoStockPreciosYCaracteristicas(idConceptoBasico), /*TransaccionSettings.Default.PreciosCentralizados ? ObtenerSede().Id :*/ ProfileData().IdCentroAtencionQueTieneLosPrecios);
                return Json(respuesta);
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerMercaderiasFiltradasPorConceptoBasicoYCaracteristicas(int? idConceptoBasico, int[] idValoresCaracteristicas)
        {

            try
            {
                if (idValoresCaracteristicas != null)
                {
                    idValoresCaracteristicas = idValoresCaracteristicas.Where(id => id > 0).ToArray();
                }

                List<BandejaProductoViewModel> respuesta = new List<BandejaProductoViewModel>();
                if (idConceptoBasico != null && idValoresCaracteristicas != null && idValoresCaracteristicas.Length > 0)
                {
                    respuesta = BandejaProductoViewModel.Convert_(_logicaConcepto.ObtenerMercaderiasPorConceptoBasicoYCaracteristicasIncluyendoStockPreciosYCaracteristicas((int)idConceptoBasico, idValoresCaracteristicas), /*TransaccionSettings.Default.PreciosCentralizados ? ObtenerSede().Id :*/ ProfileData().IdCentroAtencionQueTieneLosPrecios);
                }
                else if (idConceptoBasico == null && idValoresCaracteristicas != null && idValoresCaracteristicas.Length > 0)
                {
                    respuesta = BandejaProductoViewModel.Convert_(_logicaConcepto.ObtenerMercaderiasPorCaracteristicasIncluyendoStockPreciosYCaracteristicas(idValoresCaracteristicas), /*TransaccionSettings.Default.PreciosCentralizados ? ObtenerSede().Id :*/ ProfileData().IdCentroAtencionQueTieneLosPrecios);
                }
                else if (idConceptoBasico != null && (idValoresCaracteristicas == null || idValoresCaracteristicas.Length <= 0))
                {
                    respuesta = BandejaProductoViewModel.Convert_(_logicaConcepto.ObtenerMercaderiasPorConceptoBasicoIncluyendoStockPreciosYCaracteristicas((int)idConceptoBasico), /*TransaccionSettings.Default.PreciosCentralizados ? ObtenerSede().Id :*/ ProfileData().IdCentroAtencionQueTieneLosPrecios);
                }

                return Json(respuesta);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("ERROR AL OBTENER PRODUCTOS", e)), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult ObtenerPrecioVentaNormalDelConceptoParaVentasMasivasSegunElPuntoDeVenta(int idPuntoDeVenta)
        {
            try
            {
                var productoParaVentasYCobros = _logicaConcepto.obtenerProducto(ConceptoSettings.Default.IdConceptoEnRegistroUnificadosdeVentasyCobros);
                int idCentroAtencionQueTieneElPrecio = idPuntoDeVenta;
                var conceptoConPrecio = new { Nombre = productoParaVentasYCobros.Nombre, PrecioConcepto = productoParaVentasYCobros.PrecioVentaNormal(idCentroAtencionQueTieneElPrecio) };

                return Json(conceptoConPrecio);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al obtener precio", e)), HttpStatusCode.InternalServerError);
            }

        }

        public void ComprobarSiYaExisteNombreDeUnProducto(int idProducto, string nombreProducto)
        {
            try
            {
                if (!ConceptoSettings.Default.PermitirConceptosConNombreRepetido)
                {
                    if (idProducto > 0)
                    {
                        var result = _logicaConcepto.obtenerProducto(idProducto);
                        //Si cambio el nombre del concepto de negocio, se tiene que consultar si ese nombre existe
                        if (nombreProducto != result.Nombre)
                        {
                            //Buscamos el concepto negocio
                            bool existeNombreProducto = _logicaConcepto.ExisteNombreConceptoComercial(nombreProducto);
                            //Si existe un registro con el mismo nombre se mostrara un mensaje
                            if (existeNombreProducto)
                            {
                                throw new ControllerException("Ya existe un producto registrado con el mismo nombre.");
                            }
                        }
                    }
                    else
                    {
                        bool existeNombreProducto = _logicaConcepto.ExisteNombreConceptoComercial(nombreProducto);
                        if (existeNombreProducto)
                        {
                            throw new ControllerException("Ya existe un producto registrado con el mismo nombre.");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new ControllerException("Error al comprobar si existe el nombre de un producto.", e);
            }
        }

        #endregion

        #region CONCEPTOS COMERCIALES

        public JsonResult ObtenerConceptosNegociosComerciales(int? idConceptoBasico, int? idCategoria, int[] idValoresCaracteristicas)
        {
            try
            {
                var respuesta = _logicaConcepto.ObtenerConceptosNegociosComerciales(idConceptoBasico, idCategoria, idValoresCaracteristicas);
                var jsonResult = Json(respuesta, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al obtener conceptos de negocios comerciales", e)), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult ObtenerConceptosNegociosComercialesIncluyendoStockYPrecios(int idAlmacen, int? idConceptoBasico, int? idCategoria, int[] idValoresCaracteristicas)
        {
            try
            {
                long idTransaccionInventarioFisico = ProfileData().ObtenerIdInventarioActual(idAlmacen);
                int idCentroAtencionQueTieneLosPrecios = ProfileData().IdCentroAtencionQueTieneLosPrecios;

                var respuesta = _logicaConcepto.ObtenerConceptosNegociosComercialesConStockYPrecios(idTransaccionInventarioFisico, idCentroAtencionQueTieneLosPrecios, idConceptoBasico, idCategoria, idValoresCaracteristicas);
                var jsonResult = Json(respuesta, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al obtener conceptos de negocios comerciales", e)), HttpStatusCode.InternalServerError);
            }
        }

        #endregion

        #region CONCEPTOS NEGOCIO COMERCIALES PARA OPERACIONES
        public JsonResult ObtenerConceptosDeNegociosComerciales(int modoSeleccionTipoFamilia, int informacionSelectorConcepto)
        {
            try
            {
                List<Selector_Concepto_Negocio_Comercial> conceptosNegociosComerciales = _logicaConcepto.ObtenerConceptosDeNegociosComerciales(modoSeleccionTipoFamilia, informacionSelectorConcepto, ProfileData());
                var jsonResult = Json(conceptosNegociosComerciales, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al obtener los conceptos de negocio comerciales", e)), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ObtenerConceptosDeNegociosComercialesPorBusquedaConcepto(string cadenaBusqueda, int modoSeleccionTipoFamilia, int informacionSelectorConcepto)
        {
            try
            {
                List<Selector_Concepto_Negocio_Comercial> conceptosNegociosComerciales = _logicaConcepto.ObtenerConceptosDeNegociosComercialesPorBusquedaConcepto(cadenaBusqueda, modoSeleccionTipoFamilia, informacionSelectorConcepto, ProfileData());
                var jsonResult = Json(conceptosNegociosComerciales, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al obtener los conceptos de negocio comerciales", e)), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ObtenerConceptosDeNegociosComercialesPorFamilia(int idFamilia, int informacionSelectorConcepto)
        {
            try
            {
                List<Selector_Concepto_Negocio_Comercial> conceptosNegociosComerciales = _logicaConcepto.ObtenerConceptosDeNegociosComercialesPorFamilia(idFamilia, informacionSelectorConcepto, ProfileData());
                var jsonResult = Json(conceptosNegociosComerciales, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al obtener los conceptos de negocio comerciales", e)), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ObtenerConceptoDeNegocioComercialPorCodigoBarra(string codigoBarra, bool complementoStock, bool complementoPrecio, int modoSeleccionTipoFamilia)
        {
            try
            {
                Concepto_Negocio_Comercial_ conceptoNegocioComercial = _logicaConcepto.ObtenerConceptoDeNegocioComercialPorCodigoBarra(ProfileData(), codigoBarra, complementoStock, complementoPrecio, modoSeleccionTipoFamilia);
                return Json(conceptoNegocioComercial);
            }
            catch (AdvertenciaException e)
            {
                return new JsonHttpStatusResult(new { error = e.Message, warning = true }, HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ObtenerConceptoDeNegocioComercialPorIdConcepto(int idConceptoNegocio, bool complementoStock, bool complementoPrecio)
        {
            try
            {
                Concepto_Negocio_Comercial_ conceptoNegocioComercial = _logicaConcepto.ObtenerConceptoDeNegocioComercialPorIdConcepto(ProfileData(), idConceptoNegocio, complementoStock, complementoPrecio);
                return Json(conceptoNegocioComercial);
            }
            catch (AdvertenciaException e)
            {
                return new JsonHttpStatusResult(new { error = e.Message, warning = true }, HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ObtenerConceptosDeNegociosComercialesParaVentaPorNombre(string nombre)
        {
            try
            {
                int idCentroAtencionQueTieneLosPrecios = ProfileData().IdCentroAtencionQueTieneLosPrecios;
                List<Concepto_Negocio_Comercial> conceptosNegociosComerciales = _logicaConcepto.ObtenerConceptosDeNegociosComercialParaVentaPorNombre(idCentroAtencionQueTieneLosPrecios, nombre);
                var conceptosNegociosComercialesConFiltros = ProductoParaVentaConFiltrosViewModel.Convert(conceptosNegociosComerciales);
                return conceptosNegociosComercialesConFiltros.ConceptosNegociosComerciales.Count > 0 ? Json(conceptosNegociosComercialesConFiltros) : Json(new { data = 0, data_description = "No existe conceptos de negocios con el nombre ingresado  " });

            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("Error al obtener productos para venta", e)), HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region CONCEPTO BASICOS

        public JsonResult ObtenerConcepto(int idConcepto)
        {
            try
            {

                RegistroConceptoViewModel concepto = new RegistroConceptoViewModel(_logicaConcepto.ObtenerConceptoBasicoVigente(idConcepto));

                return Json(concepto);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerConceptos()
        {
            try
            {
                List<Detalle_maestro> resultados = _logicaConcepto.ObtenerConceptosBasicosVigentesIncluyendoCategoriaConcepto();
                List<BandejaConceptoViewModel> conceptos = new List<BandejaConceptoViewModel>();
                foreach (var item in resultados)
                {
                    conceptos.Add(new BandejaConceptoViewModel(item));
                }
                return Json(conceptos);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        public JsonResult ObtenerFamilias()
        {
            try
            {
                List<Detalle_maestro> resultados = _logicaConcepto.ObtenerTodosLasFamilia();
                List<BandejaConceptoViewModel> conceptos = new List<BandejaConceptoViewModel>();
                foreach (var item in resultados)
                {
                    conceptos.Add(new BandejaConceptoViewModel(item));
                }
                return Json(conceptos);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerConceptosBasicosIncluyendoCaracteristicas()
        {
            try
            {
                List<Concepto_Basico> resultados = _logicaConcepto.ObtenerConceptosaBasicosVigentesIncluyendoCaracteristicas();
                return Json(resultados);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult GuardarConcepto(RegistroConceptoViewModel concepto)
        {
            try
            {

                OperationResult resultado;
                int[] idsCategorias = concepto.Categorias != null ? concepto.Categorias.Select(c => c.Id).ToArray() : new int[] { };
                int[] idsCaracteristicas = concepto.Caracteristicas != null ? concepto.Caracteristicas.Select(c => c.Id).ToArray() : new int[] { };

                if (concepto.IdConcepto != 0)
                {
                    resultado = _logicaConcepto.ActualizarFamilia(concepto.IdConcepto, concepto.Nombre, concepto.Valor, idsCategorias, idsCaracteristicas);
                }
                else
                {
                    resultado = _logicaConcepto.GuardarConceptoBasico(concepto.Nombre, concepto.Valor, idsCategorias, idsCaracteristicas);
                }
                Util.ManageIfResultIsNotSuccess(resultado, "");
                return Json(new { resultado.code_result, resultado.data, result_description = resultado.title });
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult CambioEsVigenteFamilia(int idConcepto, bool esVigente)
        {
            try
            {
                OperationResult result = _logicaConcepto.CambiarEsVigenteFamilia(idConcepto, esVigente);
                Util.VerificarError(result);
                return Json(new { code_result = result.code_result, data = result.data, result_description = result.title });
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }

      
        #endregion

        #region CATEGORIAS

        public JsonResult GuardarCategoria(DetalleMaestroViewModel categoria)
        {
            try
            {
                OperationResult resultado;
                if (categoria.Id != 0)
                {
                    resultado = _logicaMaestro.ActualizarDetalleDetalleMaestro(categoria.Id, MaestroSettings.Default.IdMaestroCategoriaConcepto, categoria.Nombre, categoria.Nombre, categoria.Valor, categoria.DetalleMaestroPadre.Id);
                }
                else
                {
                    resultado = _logicaMaestro.GuardarDetalleDetalleMaestro(MaestroSettings.Default.IdMaestroCategoriaConcepto, categoria.Nombre, categoria.Nombre, categoria.Valor, categoria.DetalleMaestroPadre.Id);
                }
                Util.VerificarError(resultado);
                return Json(new { code_result = resultado.code_result, data = resultado.data, result_description = resultado.title });
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }

        public async Task<JsonResult> ObtenerCategorias()
        {
            try
            {
                List<Detalle_maestro> detalles = await _logicaMaestro.ObtenerCategorias();
                List<DetalleMaestroViewModel> categorias = DetalleMaestroViewModel.ConvertirCategoriasDetalleDetalleMaestro(detalles);
                return Json(categorias);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        #endregion

        #region CARACTERISTICA

        public JsonResult GuardarCaracteristica(CaracteristicaConceptoViewModel caracteristica)
        {
            try
            {
                OperationResult resultado;
                int idMaestroCaracteristica = caracteristica.EsComun ? MaestroSettings.Default.IdMaestroCaracteristicaConcepto : MaestroSettings.Default.IdMaestroCaracteristicaPropiaConcepto;
                //Verificar si existe nombre de la caracteristica
                _logicaMaestro.ComprobarSiYaExisteNombreDeDetalleMaestro(idMaestroCaracteristica, caracteristica.Id, caracteristica.Nombre);
                List<Valor_caracteristica> valores = new List<Valor_caracteristica>();
                if (caracteristica.Valores != null && caracteristica.EsComun)
                {
                    foreach (var item in caracteristica.Valores)
                    {
                        if (item.Id != 0)
                        {
                            valores.Add(new Valor_caracteristica() { id = item.Id, id_caracteristica = caracteristica.Id, valor = item.Nombre });
                        }
                        else
                        {
                            valores.Add(new Valor_caracteristica() { id_caracteristica = caracteristica.Id, valor = item.Nombre });
                        }
                    }
                }
                if (caracteristica.Id != 0)
                {
                    resultado = _logicaConcepto.ActualizarCaracteristica(caracteristica.Id, idMaestroCaracteristica, caracteristica.Nombre, caracteristica.Nombre, caracteristica.Descripcion, valores, caracteristica.EsVigente);
                }
                else
                {
                    resultado = _logicaConcepto.GuardarCarcateristica(idMaestroCaracteristica, caracteristica.Nombre, caracteristica.Nombre, caracteristica.Descripcion, valores);
                }
                Util.VerificarError(resultado);
                return Json(new { code_result = resultado.code_result, data = resultado.data, result_description = resultado.title });
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult CambioEsVigenteCaracteristica(CaracteristicaConceptoViewModel caracteristica)
        {
            try
            {
                OperationResult resultado;
                int idMaestroCaracteristica = caracteristica.EsComun ? MaestroSettings.Default.IdMaestroCaracteristicaConcepto : MaestroSettings.Default.IdMaestroCaracteristicaPropiaConcepto;
                List<Valor_caracteristica> valores = new List<Valor_caracteristica>();
                if (caracteristica.Valores != null && caracteristica.EsComun)
                {
                    foreach (var item in caracteristica.Valores)
                    {
                        if (item.Id != 0)
                        {
                            valores.Add(new Valor_caracteristica() { id = item.Id, id_caracteristica = caracteristica.Id, valor = item.Nombre });
                        }
                        else
                        {
                            valores.Add(new Valor_caracteristica() { id_caracteristica = caracteristica.Id, valor = item.Nombre });
                        }
                    }
                }
                caracteristica.EsVigente = !caracteristica.EsVigente;
                if (!caracteristica.EsVigente)
                {
                    if (_logicaConcepto.ExisteCaracteristicaEnConceptosVigentes(caracteristica.Id))
                    {
                        throw new ControllerException("No se puede eliminar la caracteristica, debido a que hay conceptos de negocio vigentes con esta caracteristica.");
                    }
                    if (_logicaConcepto.ExisteCaracteristicaEnFamiliasVigentes(caracteristica.Id))
                    {
                        throw new ControllerException("No se puede eliminar la caracteristica, debido a que hay familias vigentes con esta caracteristica.");
                    }
                }
                resultado = _logicaConcepto.ActualizarCaracteristica(caracteristica.Id, idMaestroCaracteristica, caracteristica.Nombre, caracteristica.Nombre, caracteristica.Descripcion, valores, caracteristica.EsVigente);
                Util.VerificarError(resultado);
                return Json(new { resultado.code_result, resultado.data, result_description = resultado.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        public JsonResult ObtenerBandejaCaracteristicas()
        {
            try
            {
                List<Detalle_maestro> detalles = _logicaConcepto.ObtenerCaracteristicasIncluyendoValores();
                List<CaracteristicaConceptoViewModel> caracteristicas = CaracteristicaConceptoViewModel.Convert(detalles);
                return Json(caracteristicas);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerCaracteristicas()
        {
            try
            {
                List<Detalle_maestro> detalles = _logicaConcepto.ObtenerCaracteristicasVigentesIncluyendoValores();
                List<CaracteristicaConceptoViewModel> caracteristicas = CaracteristicaConceptoViewModel.Convert(detalles);
                return Json(caracteristicas);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerCaracteristicasPorFamilia(int idFamilia)
        {
            try
            {
                var caracteristicas = _logicaConcepto.ObtenerCaracteristicasVigentesPorFamilia(idFamilia);
                return Json(caracteristicas);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerCaracteristicasComunesConcepto()
        {
            try
            {
                var caracteristicas = _logicaConcepto.ObtenerCaracteristicasComunes();

                return Json(caracteristicas);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
         
        #endregion

        #region VALOR CARACTERISTICA

        public JsonResult GuardarValorCaracteristica(ModeloBasicoViewModel valorCaracteristica, int idCaracteristica)
        {
            try
            {
                OperationResult resultado;

                ComprobarSiYaExisteNombreDeValorCaracteristica(valorCaracteristica.Id, idCaracteristica, valorCaracteristica.Nombre);

                if (valorCaracteristica.Id != 0)
                {

                    resultado = _logicaConcepto.ActualizarValorCaracteristica(valorCaracteristica.Id, idCaracteristica, valorCaracteristica.Nombre);
                }
                else
                {
                    resultado = _logicaConcepto.GuardarValorCarcateristica(idCaracteristica, valorCaracteristica.Nombre);
                }

                Util.VerificarError(resultado);
                return Json(new { code_result = resultado.code_result, data = resultado.data, result_description = resultado.title });
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }

        public void ComprobarSiYaExisteNombreDeValorCaracteristica(int idValorCaracteristica, int idCaracteristica, string nombreValorCaracteristica)
        {
            try
            {
                if (idValorCaracteristica > 0)
                {
                    Valor_caracteristica result = _logicaConcepto.ObtenerValorCaracteristica(idValorCaracteristica);

                    //Si cambio el nombre del valor caracteristica, se tiene que consultar si ese nombre existe
                    if (nombreValorCaracteristica != result.valor)
                    {
                        //Buscamos el concepto negocio
                        bool existeNombreValorCaracteristica = _logicaConcepto.ExisteNombreDeValorCaracteristica(idCaracteristica, nombreValorCaracteristica);

                        //Si existe un registro con el mismo nombre se mostrara un mensaje
                        if (existeNombreValorCaracteristica)
                        {
                            throw new ControllerException("Ya existe un registro con el mismo nombre.");
                        }
                    }
                }
                else
                {
                    bool existeNombreValorCaracteristica = _logicaConcepto.ExisteNombreDeValorCaracteristica(idCaracteristica, nombreValorCaracteristica);

                    if (existeNombreValorCaracteristica)
                    {
                        throw new ControllerException("Ya existe un registro con el mismo nombre.");
                    }
                }
            }
            catch (Exception e)
            {

                throw new ControllerException("Error al comprobar si existe nombre de valor caracteristica.", e);
            }

        }

        public JsonResult ObtenerValoresCaracteristica(int idCaracteristica)
        {
            try
            {
                List<Valor_caracteristica> resultados = _logicaConcepto.ObtenerValoresDeCaracteristica(idCaracteristica);
                List<ModeloBasicoViewModel> valores = new List<ModeloBasicoViewModel>();
                foreach (var item in resultados)
                {
                    valores.Add(new ModeloBasicoViewModel(item.id, item.valor));
                }
                return Json(valores);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        #endregion

        #region CONCEPTO DE GASTOS
        public JsonResult ObtenerConceptoGasto(int id)
        {
            try
            {
                var resultado = _logicaConcepto.obtenerProducto(id);
                return Json(resultado);
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerConceptosGastos()
        {
            try
            {
                List<DetalleGenerico> resultados = _logicaConcepto.ObtenerConceptosVigentesGasto();
                List<ComboGenericoViewModel> conceptos = ComboGenericoViewModel.Convert(resultados);
                return Json(conceptos);
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult GuardarConceptoGasto(RegistroConceptoGastoViewModel concepto)
        {
            try
            {
                OperationResult resultado;
                if (!concepto.ConceptoBasicoSeleccionado)
                {
                    var resultadoConceptoBasico = _logicaConcepto.GuardarConceptoBasico(concepto.NombreConceptoBasico, concepto.Valor, null, null);
                    Util.ManageIfResultIsNotSuccess(resultadoConceptoBasico, "Error al registrar el concepto basico de gasto");
                    concepto.ConceptoBasico = new ComboGenericoViewModel((int)resultadoConceptoBasico.data, concepto.NombreConceptoBasico);
                }
                if (concepto.Id > 0)
                {
                    resultado = _logicaConcepto.ActualizarConceptoGasto(concepto.Id, concepto.ConceptoBasico.Nombre + " " + concepto.Sufijo, concepto.ConceptoBasico.Id, concepto.Sufijo, ProfileData().Empleado.Id, ProfileData().IdCentroAtencionQueTieneLosPrecios);
                }
                else
                {
                    resultado = _logicaConcepto.GuardarConceptoGasto(concepto.ConceptoBasico.Nombre + " " + concepto.Sufijo, concepto.ConceptoBasico.Id, concepto.Sufijo, ProfileData().Empleado.Id, ProfileData().IdCentroAtencionQueTieneLosPrecios);
                }
                Util.ManageIfResultIsNotSuccess(resultado, "Erro al registrar el concepto de gasto");
                return Json(new { resultado.code_result, resultado.data, result_description = resultado.title });
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerRolesConceptosGasto()//No se usa
        {
            try
            {
                var resultados = _logicaConcepto.ObtenerRolesQueAplicaAConceptosDeNegocioExceptoMercaderiaYServicios();
                List<ComboGenericoViewModel> rolesDeNegocio = new List<ComboGenericoViewModel>();
                rolesDeNegocio = ComboGenericoViewModel.Convert(resultados);
                return Json(rolesDeNegocio);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        #endregion

        public ActionResult ReporteDeProductoConCodigoDeBarra()// 
        {

            List<ConceptoDeNegocio> resultado = _logicaConcepto.obtenerMercaderias();
            List<ReporteProductoViewModel> reporteProductoViewModel = ReporteProductoViewModel.Convert(resultado);

            ReportParameter entidadInterna = new ReportParameter("NombreCentroAtencion", ProfileData().NombreCentroDeAtencionSeleccionado);
            ReportParameter fecha = new ReportParameter("FechaActual",DateTimeUtil.FechaActual().ToString());
            ReportParameter nombreEmpresa = new ReportParameter("NombreEmpresa", ObtenerSede().Nombre);

            var sede = ObtenerSede();
            string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
            ReportParameter logoSede = new ReportParameter("LogoSede", logoString);

            ReportParameter empleadoSede = new ReportParameter("Usuario", ProfileData().Empleado.NombresYApellidos);

            DateTime fechaActual =DateTimeUtil.FechaActual();
            ReportParameter fechaActualSistema = new ReportParameter("FechaActualConHora", fechaActual.ToString());

            var rptviewer = new ReportViewer();
            rptviewer.ProcessingMode = ProcessingMode.Local;

            rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"/Content/reports/Core/Conceptos/Conceptos.rdlc";
            rptviewer.LocalReport.SetParameters(new ReportParameter[] { entidadInterna, nombreEmpresa, fecha, logoSede, empleadoSede, fechaActualSistema });

            ReportDataSource rptdatasourceStock = new ReportDataSource("DataSetReporteDeProducto", reporteProductoViewModel);

            rptviewer.LocalReport.DataSources.Add(rptdatasourceStock);
            rptviewer.SizeToReportContent = true;
            ViewBag.ReportViewer = rptviewer;

            return View("VisualizadorReporte");

        }
        public JsonResult GuardarConceptoServicio(RegistroConceptoServicioViewModel conceptoServicio)
        {
            try
            {
                OperationResult resultado;
                if (!conceptoServicio.ConceptoBasicoSeleccionado)
                {
                    var resultadoConceptoBasico = _logicaConcepto.GuardarConceptoBasico(conceptoServicio.NombreConceptoBasico, conceptoServicio.Valor, null, null);
                    Util.ManageIfResultIsNotSuccess(resultadoConceptoBasico, "Error al registrar el concepto basico de gasto");
                    conceptoServicio.ConceptoBasico = new ComboGenericoViewModel((int)resultadoConceptoBasico.data, conceptoServicio.NombreConceptoBasico);
                }
                resultado = _logicaConcepto.GuardarConceptoServicio(conceptoServicio.NombreCompleto, conceptoServicio.ConceptoBasico.Id, conceptoServicio.Sufijo, ProfileData().Empleado.Id, ProfileData().IdCentroAtencionQueTieneLosPrecios);
                Util.ManageIfResultIsNotSuccess(resultado, "Error al registrar el concepto de servicio");
                return Json(new { resultado.code_result, resultado.data, basico_data = conceptoServicio.ConceptoBasico.Id, result_description = resultado.title });
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult DescargarReporteDigemid()
        {
            try
            {
                var sede = ObtenerSede();

                List<ReporteDigemid> reporteConceptosDigemid = _logicaConcepto.ObtenerReporteConceptosDigemid(ProfileData());

                var rptviewer = new ReportViewer();

                string path = @"/Content/reports/Core/Conceptos/ReporteConceptosDigemid.rdlc";
                rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + path;
                ReportDataSource rptdatasourcereporteVenta = new ReportDataSource("DataSetReporteDigemid", reporteConceptosDigemid);

                rptviewer.ProcessingMode = ProcessingMode.Local;
                rptviewer.LocalReport.DataSources.Add(rptdatasourcereporteVenta);
                rptviewer.SizeToReportContent = true;
                rptviewer.Width = Unit.Percentage(100);
                rptviewer.Height = Unit.Percentage(100);


                string filenameContent = sede.DocumentoIdentidad + "_" + sede.Nombre + "_DIGEMID_" +DateTimeUtil.FechaActual().ToString("yyyy-MM-dd");
                string filename = string.Format("{0}.{1}", filenameContent, "xls");
                filename = filename.Replace(" ", "");

                byte[] bytes = rptviewer.LocalReport.Render("Excel", null, out string mimeType, out string encoding, out string extension, out string[] streamids, out Warning[] warnings);

                Response.ClearHeaders();
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
                Response.ContentType = mimeType;
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();

                return Json("Operacion realizada con exito");
            }

            catch (Exception e)
            {
                throw new ControllerException("Error descargar el reporte de digemid", e);
            }
        }


    }
}
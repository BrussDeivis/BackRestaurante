using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.WebApplication.MenuData;
using Tsp.Sigescom.WebApplication.Models;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Negocio.Establecimientos;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public class MaestroController : BaseController
    {
        private readonly IMaestroLogica maestroLogica;
        private new readonly IActorNegocioLogica actorNegocioLogica;
        private readonly IConceptoLogica conceptoLogica;
        private readonly IPermisos_Logica permisoLogica;

        public MaestroController()
        {
            maestroLogica = Dependencia.Resolve<IMaestroLogica>();
            actorNegocioLogica = Dependencia.Resolve<IActorNegocioLogica>();
            conceptoLogica = Dependencia.Resolve<IConceptoLogica>();
            permisoLogica = Dependencia.Resolve<IPermisos_Logica>();
        }

        [Authorize(Roles = "AdministradorNegocio")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "AdministradorNegocio")]
        public ActionResult Presentaciones()
        {
            ViewBag.idMaestroPresentacionConcepto = MaestroSettings.Default.IdMaestroPresentacionConcepto;
            return View();
        }

        #region METODOS DE MAESTRO

        public JsonResult GuardarMaestro(MaestroViewModel maestro)
        {
            try
            {
                OperationResult resultado;  
                if (maestro.Id != 0)
                {
                    resultado = maestroLogica.actualizarMaestro(maestro.Id, maestro.Codigo, maestro.Nombre);
                }
                else
                {
                    resultado = maestroLogica.guardarMaestro(maestro.Codigo, maestro.Nombre);
                }

                Util.VerificarError(resultado);
                return Json(new { resultado.code_result, resultado.data, result_description = resultado.title });
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        public JsonResult GuardarDetalleMaestro(DetalleMaestroViewModel detalle)
        {
            try
            {
                OperationResult resultado;
                if (detalle.Id != 0)
                {
                    resultado = maestroLogica.actualizarDetalleMaestro(detalle.Id, detalle.IdMaestro, detalle.Codigo, detalle.Nombre, detalle.Valor);
                }
                else
                {
                    resultado = maestroLogica.guardarDetalleMaestro(detalle.IdMaestro, detalle.Codigo, detalle.Nombre, detalle.Valor);
                }

                Util.VerificarError(resultado);
                return Json(new { resultado.code_result, resultado.data, result_description = resultado.title });
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        public JsonResult ListarMaestros()
        {
            try
            {
                var resultados = maestroLogica.obtenerMaestros();
                List<MaestroViewModel> maestros = new List<MaestroViewModel>();
                foreach (var item in resultados)
                {
                    maestros.Add(new MaestroViewModel(item));
                }
                return Json(maestros);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        public async Task<JsonResult> ListarDetallesMaestro(int idMaestro)
        {
            try
            {
                List<Detalle_maestro> resultados = await maestroLogica.ObtenerDetallesMaestrosAsync(idMaestro);
                List<DetalleMaestroViewModel> detalles = new List<DetalleMaestroViewModel>();
                foreach (var item in resultados)
                {
                    detalles.Add(new DetalleMaestroViewModel(item));
                }
                return Json(detalles);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        #endregion

        public async Task<JsonResult> ObtenerCategoriasConcepto()
        {
            try
            {
                List<Detalle_maestro> resultados = await maestroLogica.ObtenerCategorias();
                List<ModeloBasicoViewModel> categorias = new List<ModeloBasicoViewModel>();

                foreach (var item in resultados)
                {
                    categorias.Add(new ModeloBasicoViewModel(item.id, item.nombre));
                }
                return Json(categorias);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerCaracteristicasConcepto()
        {
            try
            {
                List<Detalle_maestro> detalles = conceptoLogica.ObtenerCaracteristicas();
                List<CaracteristicaViewModel> caracteristicas = CaracteristicaViewModel.Convert(detalles);
                return Json(caracteristicas);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerCaracteristicasParaConcepto(int idConcepto)
        {
            try
            {
                Detalle_maestro concepto = conceptoLogica.ObtenerConceptoBasicoVigenteIncluyendonCaracteristicasYValoresCaracteristicas(idConcepto);
                List<CaracteristicaConceptoViewModel> caracteristicas = CaracteristicaConceptoViewModel.Convert(concepto);
                return Json(caracteristicas);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
       
        public async Task<JsonResult> ObtenerMediosDePago()
        {
            try
            {
                var resultados = await maestroLogica.ObtenerMediosDePago();
                List<ComboGenericoViewModel> mediosDePago = new List<ComboGenericoViewModel>();
                foreach (var item in resultados)
                {
                    mediosDePago.Add(new ComboGenericoViewModel(item.id, item.nombre));
                }
                return Json(mediosDePago);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public async Task<JsonResult> ObtenerEntidadesFinancieras()
        {
            try
            {
                var resultados = await maestroLogica.ObtenerEntidadesFinancieras();
                List<ComboGenericoViewModel> entidadesFinancieras = new List<ComboGenericoViewModel>();
                foreach (var item in resultados)
                {
                    entidadesFinancieras.Add(new ComboGenericoViewModel(item.id, item.nombre));
                }
                return Json(entidadesFinancieras);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public async Task<JsonResult> ObtenerMonedas()
        {
            try
            {
                var resultados = await maestroLogica.ObtenerMonedas();
                List<ComboGenericoViewModel> monedas = new List<ComboGenericoViewModel>();
                foreach (var item in resultados)
                {
                    monedas.Add(new ComboGenericoViewModel(item.id, item.nombre));
                }
                return Json(monedas);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public async Task< JsonResult> ObtenerTiposCuentaBancaria()
        {
            try
            {
                var resultados =await maestroLogica.ObtenerTiposCuentaBancaria();
                List<ComboGenericoViewModel> tipos = new List<ComboGenericoViewModel>();
                foreach (var item in resultados)
                {
                    tipos.Add(new ComboGenericoViewModel(item.id, item.nombre));
                }
                return Json(tipos);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        public async Task< JsonResult> ObtenerEntidades(int idMedioDePago)
        {
            try
            {
                var bancos = await maestroLogica .ObtenerEntidadesFinancieras();
                var tarjetas = await maestroLogica.ObtenerOperadoresDeTarjeta();

                List<ItemGenerico> entidades = new List<ItemGenerico>();
                foreach (var item in bancos)
                {
                    entidades.Add(new ItemGenerico(item.id, item.nombre));
                }
                foreach (var item in tarjetas)
                {
                    entidades.Add(new ItemGenerico(item.id, item.nombre));
                }
                return Json(entidades);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        public async Task<JsonResult> ObtenerOperadoresDeTarjeta()
        {
            try
            {
                var resultados = await maestroLogica.ObtenerOperadoresDeTarjeta();
                List<ItemGenerico> operadores = new List<ItemGenerico>();
                foreach (var item in resultados)
                {
                    operadores.Add(new ItemGenerico(item.id, item.nombre));
                }
                return Json(operadores);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        
        public async Task< JsonResult> ObtenerTiposDeComprobante()
        {
            try
            {
                List<Detalle_maestro> comprobantes = await maestroLogica.ObtenerTiposDeComprobante(MaestroSettings.Default.IdMaestroDocumento);
                List<ComboGenericoViewModel> listaComprobantes = new List<ComboGenericoViewModel>();
                foreach (var item in comprobantes)
                {
                    listaComprobantes.Add(new ComboGenericoViewModel(item.id, item.nombre));
                }
                return Json(listaComprobantes);
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }
        public async Task< JsonResult> ObtenerModalidadesTraslado()
        {
            try
            {
                var resultados = await maestroLogica.ObtenerModalidadesTrasladoAsync();
                List<ComboGenericoViewModel> entidadesBancarias = new List<ComboGenericoViewModel>();
                foreach (var item in resultados)
                {
                    entidadesBancarias.Add(new ComboGenericoViewModel(item.id, item.nombre));
                }
                return Json(entidadesBancarias);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        public async Task<JsonResult> ObtenerMotivosTraslado()
        {
            try
            {
                var resultados = await maestroLogica.ObtenerMotivosTrasladoVigentesAsync();
                List<ComboGenericoViewModel> motivos = new List<ComboGenericoViewModel>();
                foreach (var item in resultados)
                {
                    motivos.Add(new ComboGenericoViewModel(item.id, item.nombre));
                }
                return Json(motivos);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public async Task<JsonResult> ObtenerMotivosDeViaje()
        {
            try
            {
                var resultados = await maestroLogica.ObtenerMotivosDeViajeAsync();
                List<ComboGenericoViewModel> motivos = new List<ComboGenericoViewModel>();
                foreach (var item in resultados)
                {
                    motivos.Add(new ComboGenericoViewModel(item.id, item.nombre));
                }
                return Json(motivos);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public async Task<JsonResult> ObtenerTiposDeNota(bool esParaNotaDeDebito)
        {
            try
            {
                var resultados = esParaNotaDeDebito ? await maestroLogica.ObtenerTiposDeNotaDeDebito() : await maestroLogica .ObtenerTiposDeNotaDeCredito();
                List<ComboGenericoViewModel> tiposDeNota = ComboGenericoViewModel.Convert(resultados);
                return Json(tiposDeNota);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ListarEmpleados()
        {
            try
            {
                var resultados = actorNegocioLogica.ObtenerEmpleadosVigentes();
                List<ComboGenericoViewModel> empleados = new List<ComboGenericoViewModel>();
                foreach (var item in resultados)
                {
                    empleados.Add(new ComboGenericoViewModel(item.Id, item.DocumentoIdentidad + " | " + item.Nombres + " " + item.ApellidoPaterno + " " + item.ApellidoMaterno));
                    //item.DocumentoIdentidad.ToString() +" | " + item.Nombres.ToString() + " " + item.ApellidoPaterno + " " + item.ApellidoMaterno
                }
                return Json(empleados);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerDatosEmpleado(int idEmpleado)
        {
            try
            {
                var resultado = actorNegocioLogica.ObtenerEmpleado(idEmpleado);
                
                return Json(new { nombreApellido = resultado.Nombres+" "+resultado.ApellidoPaterno+" "+resultado.ApellidoMaterno, cargo = resultado.cargo()});
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

       
        public async  Task<JsonResult> ListarTiposDeDocumentosDeIdentidad()
        {
            try
            {
                var resultados = await maestroLogica.ObtenerTiposDeDocumentosDeIdentidad();
                List<ComboGenericoViewModel> tiposDeDocuemntosDeIdentidad = new List<ComboGenericoViewModel>();
                foreach (var item in resultados)
                {
                    tiposDeDocuemntosDeIdentidad.Add(new ComboGenericoViewModel(item.id, item.nombre));
                }
                return Json(tiposDeDocuemntosDeIdentidad);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        public JsonResult ListarUbigeoDistrito()
        {
            try
            {
                List<Ubigeo> respuestas = maestroLogica.obtenerUbigeoDistrito();
                List<ComboGenericoViewModel> ubigeos = new List<ComboGenericoViewModel>();
                foreach (var item in respuestas)
                {
                    ubigeos.Add(new ComboGenericoViewModel(item.id, item.descripcion_larga));
                }
                return Json(ubigeos);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        public JsonResult ObtenerRolesPersonal()
        {
            try
            {
                var resultados = actorNegocioLogica.ObtenerRolesPersonal();
                List<ComboGenericoViewModel> roles = new List<ComboGenericoViewModel>();
                foreach (var item in resultados)
                {
                    roles.Add(new ComboGenericoViewModel(item.id, item.nombre));
                }
                return Json(roles);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        public async Task< JsonResult> ListarTiposDeVia()
        {
            try
            {
                var resultados = await maestroLogica.ObtenerTiposDeVia();
                List<ComboGenericoViewModel> roles = new List<ComboGenericoViewModel>();
                foreach (var item in resultados)
                {
                    roles.Add(new ComboGenericoViewModel(item.id, item.nombre));
                }
                return Json(roles);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public async Task<JsonResult> ListarTiposDeZona()
        {
            try
            {
                var resultados = await maestroLogica .ObtenerTiposDeZona();
                List<ComboGenericoViewModel> roles = new List<ComboGenericoViewModel>();
                foreach (var item in resultados)
                {
                    roles.Add(new ComboGenericoViewModel(item.id, item.nombre));
                }
                return Json(roles);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        public async Task<JsonResult> ListarNaciones()
        {
            try
            {
                var resultados = await maestroLogica.ObtenerNaciones();
                List<ComboGenericoViewModel> roles = new List<ComboGenericoViewModel>();
                foreach (var item in resultados)
                {
                    roles.Add(new ComboGenericoViewModel(item.id, item.nombre));
                }
                return Json(roles);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        
        public async Task<JsonResult> ListarTiposDeDireccion()
        {
            try
            {
                List<Detalle_maestro> resultados = await maestroLogica .ObtenerTiposDeDireccion();
                List<ComboGenericoViewModel> roles = new List<ComboGenericoViewModel>();
                foreach (var item in resultados)
                {
                    roles.Add(new ComboGenericoViewModel(item.id, item.nombre));
                }
                return Json(roles);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        
        public JsonResult ObtenerTarifas()
        {
            try
            {
                var resultados = maestroLogica.obtenerTarifas();
                List<ComboGenericoViewModel> roles = new List<ComboGenericoViewModel>();
                foreach (var item in resultados)
                {
                    roles.Add(new ComboGenericoViewModel(item.id, item.nombre));
                }
                return Json(roles);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }


        #region CONCEPTOS
        public JsonResult ObtenerFamiliasVigentes()
        {
            try
            {
                List<Familia_Concepto_Comercial> resultado = maestroLogica.ObtenerFamiliasVigentes();
                return Json(resultado);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerFamiliasVigentesPorModoSeleccionTipoFamilia(int modoSeleccionTipoFamilia)
        {
            try
            {
                List<Familia_Concepto_Comercial> resultado = maestroLogica.ObtenerFamiliasVigentes(modoSeleccionTipoFamilia);
                return Json(resultado);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerFamiliasConceptosComercialesVigentes()
        {
            try
            {
                List<Familia_Concepto_Comercial> resultado = maestroLogica.ObtenerFamiliasConceptosComercialesVigentes();
                return Json(resultado);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        public JsonResult ObtenerFamiliasConceptosComercialesVigentesPorModoSeleccionTipoFamilia(int modoSeleccionTipoFamilia)
        {
            try
            {
                List<Familia_Concepto_Comercial> resultado = maestroLogica.ObtenerFamiliasConceptosComercialesVigentes(modoSeleccionTipoFamilia);
                return Json(resultado);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerConceptosDeCompraVenta()
        {
            try
            {
                List<Detalle_maestro> resultados = maestroLogica.ObtenerConceptosVigentesDeCompraVenta();
                List<ComboGenericoViewModel> conceptos = new List<ComboGenericoViewModel>();

                foreach (var item in resultados)
                {
                    conceptos.Add(new ComboGenericoViewModel(item.id, item.nombre));
                }
                return Json(conceptos);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerConceptosDeConceptoGasto()
        {
            try
            {
                List<Detalle_maestro> detalles = conceptoLogica.ObtenerConceptosBasicosDeGasto();
                List<ComboGenericoViewModel> conceptos = ComboGenericoViewModel.Convert(detalles);
                return Json(conceptos);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerConceptosBasicos()
        {
            try
            {
                List<Detalle_maestro> resultados = maestroLogica.ObtenerConceptosVigentes();
                List<ComboGenericoViewModel> conceptosBasicosViewModel = ComboGenericoViewModel.Convert(resultados).ToList();

                return Json(conceptosBasicosViewModel);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerConceptosBasicosServicio()
        {
            try
            {
                List<Detalle_maestro> resultados = maestroLogica.ObtenerConceptosServicioVigentes();
                List<ComboGenericoViewModel> conceptosBasicosViewModel = ComboGenericoViewModel.Convert(resultados).ToList();

                return Json(conceptosBasicosViewModel);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public async Task<JsonResult>ListarConceptosPagoEmpleados()
        {
            try
            {
                List<Detalle_maestro> resultados = await maestroLogica.ObtenerConceptosPagoEmpleados();
                List<ComboGenericoViewModel> conceptos = new List<ComboGenericoViewModel>();

                foreach (var item in resultados)
                {
                    conceptos.Add(new ComboGenericoViewModel(item.id, item.nombre));
                }
                return Json(conceptos);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        #endregion

        public async Task<JsonResult> ObtenerTiposServicioImpuesto()
        {
            try
            {
                List<Detalle_maestro> resultados = await maestroLogica.ObtenerTiposServicioImpuesto();
                List<ComboGenericoViewModel> conceptos = new List<ComboGenericoViewModel>();

                foreach (var item in resultados)
                {
                    conceptos.Add(new ComboGenericoViewModel(item.id, item.nombre));
                }
                return Json(conceptos);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

      


        public JsonResult ObtenerTercero(int id)
        {
            try
            {
                ActorComercial tercero = actorNegocioLogica.ObtenerActorComercial(id);
                return Json(new ComboActorComercialViewModel(tercero.Id, tercero.RazonSocial, tercero.DocumentoIdentidad));
            }
            catch(Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public async Task<JsonResult> ObtenerTiposProductoDeCompra()
        {
            try
            {
                List<Detalle_maestro> resultados = await maestroLogica .ObtenerTiposProductoDeCompra();
                List<ComboGenericoViewModel> conceptos = new List<ComboGenericoViewModel>();

                foreach (var item in resultados)
                {
                    conceptos.Add(new ComboGenericoViewModel(item.id, item.nombre));
                }
                return Json(conceptos);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public async Task< JsonResult> ObtenerTiposBien()
        {
            try
            {
                List<Detalle_maestro> resultados = await maestroLogica .ObtenerTiposBien();
                List<ComboGenericoViewModel> conceptos = new List<ComboGenericoViewModel>();

                foreach (var item in resultados)
                {
                    conceptos.Add(new ComboGenericoViewModel(item.id, item.nombre));
                }
                return Json(conceptos);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ListarMarcas(int IdArticulo)
        {
            try
            {
                List<Detalle_maestro> resultados = maestroLogica.obtenerMarcas(IdArticulo);
                List<ComboGenericoViewModel> articulos = new List<ComboGenericoViewModel>();

                foreach (var item in resultados)
                {
                    articulos.Add(new ComboGenericoViewModel(item.id, item.nombre));
                }
                return Json(articulos);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
       
        public async Task< JsonResult> ObtenerUnidadesDeMedida()
        {
            try
            {
                List<Detalle_maestro> resultados = await maestroLogica .ObtenerUnidadesDeMedida();
                List<ComboGenericoViewModel> unidades = new List<ComboGenericoViewModel>();

                foreach (var item in resultados)
                {
                    unidades.Add(new ComboGenericoViewModel(item.id, item.codigo));
                }
                return Json(unidades);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        public async Task<JsonResult> ObtenerPresentaciones()
        {
            try
            {
                List<Detalle_maestro> resultados = await maestroLogica.ObtenerPresentaciones();
                List<ComboGenericoViewModel> articulos = new List<ComboGenericoViewModel>();

                foreach (var item in resultados)
                {
                    articulos.Add(new ComboGenericoViewModel(item.id, item.nombre));
                }
                return Json(articulos);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        public JsonResult ListarSubContenidos(int IdModelo)
        {
            try
            {
                List<ConceptoDeNegocio> resultados = conceptoLogica.obtenerSubContenidos(IdModelo);
                List<ComboGenericoViewModel> articulos = new List<ComboGenericoViewModel>();

                foreach (var item in resultados)
                {
                    articulos.Add(new ComboGenericoViewModel(item.Id, item.Nombre));
                }
                return Json(articulos);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        public JsonResult ListarCaracteristicas(int IdArticulo)
        {
            try
            {
                List<Valor_caracteristica_concepto> resultados = maestroLogica.obtenercaracteristicasConceptoValor(IdArticulo);
                List<Detalle_maestro> listas= resultados.Select(r => r.Valor_caracteristica.Detalle_maestro).Distinct().ToList();
                List<CaracteristicaValorViewModel> caracteristicas = new List<CaracteristicaValorViewModel>();

                foreach (var item in listas)
                {
                    var valores = resultados.Where(r => r.Valor_caracteristica.id_caracteristica == item.id).Select(r => r.Valor_caracteristica).ToList();
                    caracteristicas.Add(new CaracteristicaValorViewModel(item.nombre, valores));
                }
                return Json(caracteristicas);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        public  async Task<JsonResult> ObtenerEstados()
        {
            try
            {
                List<Detalle_maestro> resultados = await maestroLogica.ObtenerEstadosTransaccion();
                List<ComboGenericoViewModel> estados = new List<ComboGenericoViewModel>();
                foreach (var item in resultados)
                {
                    estados.Add(new ComboGenericoViewModel(item.id, item.nombre));
                }
                return Json(estados);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public async Task<JsonResult> ObtenerAcciones()
        {
            try
            { 
                List<Detalle_maestro> resultados = await maestroLogica.ObtenerAccionesTransaccion();
                List<ComboGenericoViewModel> acciones = new List<ComboGenericoViewModel>();
                foreach (var item in resultados)
                {
                    acciones.Add(new ComboGenericoViewModel(item.id, item.nombre));
                }
                return Json(acciones);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerTiposDeTransaccion()
        {
            try
            {
                List<Tipo_transaccion> resultados = permisoLogica.ObtenerTiposDeTransaccion();
                List<ComboGenericoViewModel> tiposDeTransaccion = new List<ComboGenericoViewModel>();
                foreach (var item in resultados)
                {
                    tiposDeTransaccion.Add(new ComboGenericoViewModel(item.id, item.nombre));
                }
                return Json(tiposDeTransaccion);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        public async Task<JsonResult> ObtenerUnidadesDeNegocio()
        {
            try
            {
                List<Detalle_maestro> resultados = await maestroLogica.ObtenerUnidadesDeNegocio();
                List<ComboGenericoViewModel> unidadesDeNegocio = new List<ComboGenericoViewModel>();
                foreach (var item in resultados)
                {
                    unidadesDeNegocio.Add(new ComboGenericoViewModel(item.id, item.codigo));
                }
                return Json(unidadesDeNegocio);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        
        public ActionResult NavSidebar()
        {
            var data = new Data();
            ViewBag.AplicacionDesplegadaLocalmente = AplicacionSettings.Default.AplicacionDesplegadaLocalmente;
            return PartialView(data.navbarItems(conceptoLogica, actorNegocioLogica, Dependencia.Resolve<ISucursal_Logica>()).ToList());
        }
        public ActionResult ContentTop()
        {
            return PartialView();
        }

        public async Task<JsonResult> ObtenerTiposGrupoClientes()
        {
            try
            {
                var resultado = await maestroLogica.ObtenerTiposGrupoClientes();
                return Json(resultado);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        public async Task<JsonResult> ObtenerClasificacionesGrupoClientes()
        {
            try
            {
                var resultado = await maestroLogica.ObtenerClasificacionesGrupoClientes();
                return Json(resultado);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
    }
}
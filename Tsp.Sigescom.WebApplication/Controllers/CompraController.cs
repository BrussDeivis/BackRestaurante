using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.WebApplication.Models;
using System.Threading.Tasks;
using Tsp.Sigescom.Utilitarios;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public class CompraController : BaseController
    {
        private readonly IOperacionLogica _logicaOperacion;
        private readonly IConceptoLogica _logicaConcepto;
        private readonly IMaestroLogica _logicaMaestro;

        public CompraController()
        {
            _logicaOperacion = Dependencia.Resolve<IOperacionLogica>();
            _logicaConcepto = Dependencia.Resolve<IConceptoLogica>();
            _logicaMaestro = Dependencia.Resolve<IMaestroLogica>();
        }

        [Authorize(Roles = "Comprador")]
        public ActionResult ConsultarCompras()
        {
            ViewBag.idTipoActorPersonaNatural = ActorSettings.Default.IdTipoActorPersonaNatural;
            ViewBag.fechaInicio =DateTimeUtil.FechaActual().AddDays(-7).ToString("dd/MM/yyyy");
            ViewBag.fechaFin =DateTimeUtil.FechaActual().ToString("dd/MM/yyyy");
            ViewBag.tasaIGV = TransaccionSettings.Default.TasaIGV;
            ViewBag.aplicaLeyAmazonia = TransaccionSettings.Default.AplicaLeyAmazonia;
            ViewBag.comprasDestinadasAPorDefecto = TransaccionSettings.Default.ComprasDestinadasAPorDefecto;
            ViewBag.idTipoActorPersonaJuridica = ActorSettings.Default.IdTipoActorPersonaJuridica;
            ViewBag.realizaCompraAlCredito = TransaccionSettings.Default.RealizaCompraAlCredito;
            ViewBag.permitirLoteEnDetalleDeCompra = AplicacionSettings.Default.PermitirLoteEnDetalleDeCompra;
            ViewBag.permitirRegistroEnDetalleDeCompra = AplicacionSettings.Default.PermitirRegistroEnDetalleDeCompra;
            ViewBag.permitirVencimientoEnDetalleDeCompra = AplicacionSettings.Default.PermitirVencimientoEnDetalleDeCompra;
            ViewBag.idProveedorGenerico = ActorSettings.Default.idProveedorGenerico;
            ViewBag.idTipoPersonaSeleccionadaPorDefecto = ActorSettings.Default.IdTipoPersonaSeleccionadaPorDefecto;
            ViewBag.idTipoDocumentoSeleccionadaConTipoPersonaNatural = ActorSettings.Default.IdTipoDocumentoSeleccionadaConTipoPersonaNatural;
            ViewBag.idTipoDocumentoSeleccionadaConTipoPersonaJuridica = ActorSettings.Default.IdTipoDocumentoSeleccionadaConTipoPersonaJuridica;
            ViewBag.idTipoDocumentoIdentidadDni = ActorSettings.Default.IdTipoDocumentoIdentidadDni;
            ViewBag.idTipoDocumentoIdentidadRuc = ActorSettings.Default.IdTipoDocumentoIdentidadRuc;
            ViewBag.fechaActual =DateTimeUtil.FechaActual().Date;
            ViewBag.permitirRegistroFlete = AplicacionSettings.Default.PermitirRegistroFleteEnCompra;
            ViewBag.idDocumentoNotaAlamacenInterna = MaestroSettings.Default.IdDetalleMaestroComprobanteNotaAlmacenInterna;
            ViewBag.idUbigeoSeleccionadoPorDefecto = ActorSettings.Default.idUbigeoSeleccionadoPorDefectoEnProveedor;
            ViewBag.idUbigeoNoEspecificado = ActorSettings.Default.idUbigeoNoEspecificado;
            ViewBag.permitirMultipleIngresoDelMismoDetalle = AplicacionSettings.Default.PermitirMultipleIngresoDelMismoDetalleEnCompra;
            ViewBag.idDetalleMaestroAnulacionDeLaOperacion = MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaAnulacionDeLaOperacion;
            ViewBag.idDetalleMaestroAnulacionPorErrorEnElRuc = MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaAnulacionPorErrorEnElRuc;
            ViewBag.idDetalleMaestroCorreccionPorErrorEnLaDescripcion = MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaCorreccionPorErrorEnLaDescripcion;
            ViewBag.idDetalleMaestroDescuentoGlobal = MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaDescuentoGlobal;
            ViewBag.idDetalleMaestroDescuentoPorItem = MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaDescuentoPorItem;
            ViewBag.idDetalleMaestroDevolucionTotal = MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaDevolucionTotal;
            ViewBag.idDetalleMaestroDevolucionPorItem = MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaDevolucionPorItem;
            ViewBag.idDetalleMaestroBonificacion = MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaBonificacion;
            ViewBag.idDetalleMaestroDisminucionEnElValor = MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaDisminucionEnElValor;
            ViewBag.idDetalleMaestroOtrosConceptos = MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaOtrosConceptos;
            ViewBag.idDetalleMaestroInteresesPorMora = MaestroSettings.Default.IdDetalleMaestroNotaDeDebitoElectronicaInteresesPorMora;
            ViewBag.idDetalleMaestroAumentoEnElValor = MaestroSettings.Default.IdDetalleMaestroNotaDeDebitoElectronicaAumentoEnElValor;
            ViewBag.idDetalleMaestroPenalidadesYOtrosConceptos = MaestroSettings.Default.IdDetalleMaestroNotaDeDebitoElectronicaPenalidadesYOtrosConceptos;
            ViewBag.ValorPrecioVentaPorDefectoQueNoSeDebeGuardar = AplicacionSettings.Default.ValorPrecioVentaPorDefectoQueNoSeDebeGuardar;
            ViewBag.idEstablecimientoPorDefecto = ProfileData().CentroDeAtencionSeleccionado.EstablecimientoComercial.Id;
            ViewBag.idCentroDeAtencionPorDefecto = ProfileData().CentroDeAtencionSeleccionado.Id;
            ViewBag.IdPresentacionPorDefecto = ConceptoSettings.Default.idPresentacionPorDefecto;
            ViewBag.IdUnidadMedidaPorDefecto = ConceptoSettings.Default.idUnidadMedidaPorDefecto;
            ViewBag.IdPresentacionAOcultarEnNombreConceptoNegocio = ConceptoSettings.Default.IdPresentacionAOcultarEnNombreConceptoNegocio;
            ViewBag.IdUnidadDeMedidaComercialAOcultarEnNombreConceptoNegocio = ConceptoSettings.Default.IdUnidadDeMedidaComercialAOcultarEnNombreConceptoNegocio;
            ViewBag.numeroDecimalesEnCantidad = AplicacionSettings.Default.NumeroDecimalesEnCantidad;
            ViewBag.numeroDecimalesEnPrecio = AplicacionSettings.Default.NumeroDecimalesEnPrecio;
            ViewBag.mostrarBuscadorCodigoBarra = AplicacionSettings.Default.MostrarBuscadorCodigoBarraEnCotizacion;
            ViewBag.modoDeSeleccionDeConcepto = AplicacionSettings.Default.ModoDeSeleccionDeConceptoDeNegocioEnCompras;
            ViewBag.modoSeleccionTipoFamilia = AplicacionSettings.Default.ModoDeSeleccionTipoDeFamiliaEnCompras;
            ViewBag.permitirCambioPrecio = AplicacionSettings.Default.PermitirCambioPrecioEnCompras;
            ViewBag.minimoCaracteresBuscarConcepto = AplicacionSettings.Default.MinimoDeCaracteresParaBuscarEnSelectorConcepto;
            ViewBag.modoDeSeleccionTipoFamiliaEnRegistroFamilia = ConceptoSettings.Default.ModoDeSeleccionTipoFamiliaEnRegistroFamilia;
            ViewBag.mostrarCampoCodigoAlRegistrarConcepto = ConceptoSettings.Default.MostrarCampoCodigoAlRegistrarConcepto;
            ViewBag.mostrarCampoCodigoDigemidAlRegistrarConcepto = ConceptoSettings.Default.PermitirRegistroCodigoDigemidEnConceptoComercial;
            ViewBag.modulosAdicionales = MascaraArrayItemGenerico.ConvertirMascaraModulosAdicionales(AplicacionSettings.Default.MascaraModulosAdicionalesHabilitados);
            ViewBag.idRolProveedor = ActorSettings.Default.IdRolProveedor;
            ViewBag.tiempoEsperaBusquedaSelector = AplicacionSettings.Default.TiempoDeEsperaEnBusquedaSelectoresDeGranCantidad;
            ViewBag.minimoCaracteresBuscarActorComercial = ActorSettings.Default.MinimoDeCaracteresParaBuscarEnSelectorActorComercial;
            ViewBag.idDetalleMaestroCatalogoDocumentoFactura = MaestroSettings.Default.IdDetalleMaestroComprobanteFactura;
            ViewBag.mascaraDeVisualizacionValidacionRegistroProveedor = ActorSettings.Default.MascaraDeVisualizacionValidacionRegistroProveedor;
            ViewBag.informacionSelectorConcepto = (int)InformacionSelectorConcepto.Nombre;
            ViewBag.permitirSeleccionarGrupoProveedor = Diccionario.MapeoOperacionesGruposVsPermitirGrupos.Single(m => m.Key == (int)OperacionesGruposActoresComerciales.Compra).Value;
            return View();

        }
        [Authorize(Roles = "Comprador")]
        public ActionResult Compras()
        {
            ViewBag.idTipoActorPersonaNatural = ActorSettings.Default.IdTipoActorPersonaNatural;
            ViewBag.tasaIGV = TransaccionSettings.Default.TasaIGV;
            ViewBag.aplicaLeyAmazonia = TransaccionSettings.Default.AplicaLeyAmazonia;
            ViewBag.comprasDestinadasAPorDefecto = TransaccionSettings.Default.ComprasDestinadasAPorDefecto;
            ViewBag.idTipoActorPersonaJuridica = ActorSettings.Default.IdTipoActorPersonaJuridica;
            ViewBag.realizaCompraAlCredito = TransaccionSettings.Default.RealizaCompraAlCredito;
            ViewBag.permitirLoteEnDetalleDeCompra = AplicacionSettings.Default.PermitirLoteEnDetalleDeCompra;
            ViewBag.permitirRegistroEnDetalleDeCompra = AplicacionSettings.Default.PermitirRegistroEnDetalleDeCompra;
            ViewBag.permitirVencimientoEnDetalleDeCompra = AplicacionSettings.Default.PermitirVencimientoEnDetalleDeCompra;
            ViewBag.idProveedorGenerico = ActorSettings.Default.idProveedorGenerico;
            ViewBag.idTipoPersonaSeleccionadaPorDefecto = ActorSettings.Default.IdTipoPersonaSeleccionadaPorDefecto;
            ViewBag.idTipoDocumentoSeleccionadaConTipoPersonaNatural = ActorSettings.Default.IdTipoDocumentoSeleccionadaConTipoPersonaNatural;
            ViewBag.idTipoDocumentoSeleccionadaConTipoPersonaJuridica = ActorSettings.Default.IdTipoDocumentoSeleccionadaConTipoPersonaJuridica;
            ViewBag.idTipoDocumentoIdentidadDni = ActorSettings.Default.IdTipoDocumentoIdentidadDni;
            ViewBag.idTipoDocumentoIdentidadRuc = ActorSettings.Default.IdTipoDocumentoIdentidadRuc;
            ViewBag.fechaActual =DateTimeUtil.FechaActual();
            ViewBag.permitirRegistroFlete = AplicacionSettings.Default.PermitirRegistroFleteEnCompra;
            ViewBag.idUbigeoSeleccionadoPorDefecto = ActorSettings.Default.idUbigeoSeleccionadoPorDefectoEnProveedor;
            ViewBag.idUbigeoNoEspecificado = ActorSettings.Default.idUbigeoNoEspecificado;
            ViewBag.permitirMultipleIngresoDelMismoDetalle = AplicacionSettings.Default.PermitirMultipleIngresoDelMismoDetalleEnCompra;
            ViewBag.ValorPrecioVentaPorDefectoQueNoSeDebeGuardar = AplicacionSettings.Default.ValorPrecioVentaPorDefectoQueNoSeDebeGuardar;
            ViewBag.idEstablecimientoPorDefecto = ProfileData().CentroDeAtencionSeleccionado.EstablecimientoComercial.Id;
            ViewBag.idCentroDeAtencionPorDefecto = ProfileData().CentroDeAtencionSeleccionado.Id;
            ViewBag.IdPresentacionPorDefecto = ConceptoSettings.Default.idPresentacionPorDefecto;
            ViewBag.IdUnidadMedidaPorDefecto = ConceptoSettings.Default.idUnidadMedidaPorDefecto;
            ViewBag.IdPresentacionAOcultarEnNombreConceptoNegocio = ConceptoSettings.Default.IdPresentacionAOcultarEnNombreConceptoNegocio;
            ViewBag.IdUnidadDeMedidaComercialAOcultarEnNombreConceptoNegocio = ConceptoSettings.Default.IdUnidadDeMedidaComercialAOcultarEnNombreConceptoNegocio;
            ViewBag.numeroDecimalesEnCantidad = AplicacionSettings.Default.NumeroDecimalesEnCantidad;
            ViewBag.numeroDecimalesEnPrecio = AplicacionSettings.Default.NumeroDecimalesEnPrecio;
            ViewBag.mostrarBuscadorCodigoBarra = AplicacionSettings.Default.MostrarBuscadorCodigoBarraEnCotizacion;
            ViewBag.modoDeSeleccionDeConcepto = AplicacionSettings.Default.ModoDeSeleccionDeConceptoDeNegocioEnCompras;
            ViewBag.modoSeleccionTipoFamilia = AplicacionSettings.Default.ModoDeSeleccionTipoDeFamiliaEnCompras;
            ViewBag.permitirCambioPrecio = AplicacionSettings.Default.PermitirCambioPrecioEnCompras;
            ViewBag.minimoCaracteresBuscarConcepto = AplicacionSettings.Default.MinimoDeCaracteresParaBuscarEnSelectorConcepto;
            ViewBag.modoDeSeleccionTipoFamiliaEnRegistroFamilia = ConceptoSettings.Default.ModoDeSeleccionTipoFamiliaEnRegistroFamilia;
            ViewBag.mostrarCampoCodigoAlRegistrarConcepto = ConceptoSettings.Default.MostrarCampoCodigoAlRegistrarConcepto;
            ViewBag.mostrarCampoCodigoDigemidAlRegistrarConcepto = ConceptoSettings.Default.PermitirRegistroCodigoDigemidEnConceptoComercial;
            ViewBag.modulosAdicionales = MascaraArrayItemGenerico.ConvertirMascaraModulosAdicionales(AplicacionSettings.Default.MascaraModulosAdicionalesHabilitados);
            ViewBag.idRolProveedor = ActorSettings.Default.IdRolProveedor;
            ViewBag.tiempoEsperaBusquedaSelector = AplicacionSettings.Default.TiempoDeEsperaEnBusquedaSelectoresDeGranCantidad;
            ViewBag.minimoCaracteresBuscarActorComercial = ActorSettings.Default.MinimoDeCaracteresParaBuscarEnSelectorActorComercial;
            ViewBag.idDetalleMaestroCatalogoDocumentoFactura = MaestroSettings.Default.IdDetalleMaestroComprobanteFactura;
            ViewBag.mascaraDeVisualizacionValidacionRegistroProveedor = ActorSettings.Default.MascaraDeVisualizacionValidacionRegistroProveedor;
            ViewBag.informacionSelectorConcepto = (int)InformacionSelectorConcepto.Nombre;
            ViewBag.permitirSeleccionarGrupoProveedor = Diccionario.MapeoOperacionesGruposVsPermitirGrupos.Single(m => m.Key == (int)OperacionesGruposActoresComerciales.Compra).Value;
            return View();
        }
        [Authorize(Roles = "Comprador")]
        public ActionResult ComprasCorporativas()
        {
            ViewBag.idTipoActorPersonaNatural = ActorSettings.Default.IdTipoActorPersonaNatural;
            ViewBag.fechaInicio =DateTimeUtil.FechaActual().AddDays(-7).ToString("dd/MM/yyyy");
            ViewBag.fechaFin =DateTimeUtil.FechaActual().ToString("dd/MM/yyyy");
            ViewBag.tasaIGV = TransaccionSettings.Default.TasaIGV;
            ViewBag.aplicaLeyAmazonia = TransaccionSettings.Default.AplicaLeyAmazonia;
            ViewBag.comprasDestinadasAPorDefecto = TransaccionSettings.Default.ComprasDestinadasAPorDefecto;
            ViewBag.idTipoActorPersonaJuridica = ActorSettings.Default.IdTipoActorPersonaJuridica;
            ViewBag.realizaCompraAlCredito = TransaccionSettings.Default.RealizaCompraAlCredito;
            ViewBag.permitirLoteEnDetalleDeCompra = AplicacionSettings.Default.PermitirLoteEnDetalleDeCompra;
            ViewBag.permitirRegistroEnDetalleDeCompra = AplicacionSettings.Default.PermitirRegistroEnDetalleDeCompra;
            ViewBag.permitirVencimientoEnDetalleDeCompra = AplicacionSettings.Default.PermitirVencimientoEnDetalleDeCompra;
            ViewBag.idProveedorGenerico = ActorSettings.Default.idProveedorGenerico;
            ViewBag.idTipoPersonaSeleccionadaPorDefecto = ActorSettings.Default.IdTipoPersonaSeleccionadaPorDefecto;
            ViewBag.idTipoDocumentoSeleccionadaConTipoPersonaNatural = ActorSettings.Default.IdTipoDocumentoSeleccionadaConTipoPersonaNatural;
            ViewBag.idTipoDocumentoSeleccionadaConTipoPersonaJuridica = ActorSettings.Default.IdTipoDocumentoSeleccionadaConTipoPersonaJuridica;
            ViewBag.idTipoDocumentoIdentidadDni = ActorSettings.Default.IdTipoDocumentoIdentidadDni;
            ViewBag.idTipoDocumentoIdentidadRuc = ActorSettings.Default.IdTipoDocumentoIdentidadRuc;
            ViewBag.fechaActual =DateTimeUtil.FechaActual();
            ViewBag.permitirRegistroFlete = AplicacionSettings.Default.PermitirRegistroFleteEnCompra;
            ViewBag.idDocumentoNotaAlamacenInterna = MaestroSettings.Default.IdDetalleMaestroComprobanteNotaAlmacenInterna;
            ViewBag.direccionSede = (ObtenerSede().DomicilioFiscal != null) ? ObtenerSede().DomicilioFiscal.Detalle : " ";
            ViewBag.idUbigeoSede = (ObtenerSede().DomicilioFiscal != null) ? ObtenerSede().DomicilioFiscal.Ubigeo.Id : 0;
            ViewBag.idModalidadTrasladoPorDefecto = MaestroSettings.Default.IdDetalleMaestroModalidadDeTrasladoTransportePublico;
            ViewBag.idMotivoTrasladoPorDefecto = MaestroSettings.Default.IdDetalleMaestroMotivoDeTrasladoPorCompra;
            ViewBag.idUbigeoSeleccionadoPorDefecto = ActorSettings.Default.idUbigeoSeleccionadoPorDefectoEnProveedor;
            ViewBag.idUbigeoNoEspecificado = ActorSettings.Default.idUbigeoNoEspecificado;
            ViewBag.permitirMultipleIngresoDelMismoDetalle = AplicacionSettings.Default.PermitirMultipleIngresoDelMismoDetalleEnCompra;
            ViewBag.idDetalleMaestroMedioDePagoEfectivo = MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo;
            ViewBag.idDetalleMaestroEntidadBancariaNinguna = MaestroSettings.Default.IdDetalleMaestroEntidadBancariaNinguna;
            ViewBag.ValorPrecioVentaPorDefectoQueNoSeDebeGuardar = AplicacionSettings.Default.ValorPrecioVentaPorDefectoQueNoSeDebeGuardar;
            ViewBag.idEstablecimientoPorDefecto = ProfileData().CentroDeAtencionSeleccionado.EstablecimientoComercial.Id;
            ViewBag.idCentroDeAtencionPorDefecto = ProfileData().CentroDeAtencionSeleccionado.Id;
            ViewBag.IdPresentacionPorDefecto = ConceptoSettings.Default.idPresentacionPorDefecto;
            ViewBag.IdUnidadMedidaPorDefecto = ConceptoSettings.Default.idUnidadMedidaPorDefecto;
            ViewBag.IdPresentacionAOcultarEnNombreConceptoNegocio = ConceptoSettings.Default.IdPresentacionAOcultarEnNombreConceptoNegocio;
            ViewBag.IdUnidadDeMedidaComercialAOcultarEnNombreConceptoNegocio = ConceptoSettings.Default.IdUnidadDeMedidaComercialAOcultarEnNombreConceptoNegocio;
            ViewBag.numeroDecimalesEnCantidad = AplicacionSettings.Default.NumeroDecimalesEnCantidad;
            ViewBag.numeroDecimalesEnPrecio = AplicacionSettings.Default.NumeroDecimalesEnPrecio;
            return View();
        }
        [Authorize(Roles = "Comprador")]
        public ActionResult ConsultarComprasCorporativas()
        {
            ViewBag.idTipoActorPersonaNatural = ActorSettings.Default.IdTipoActorPersonaNatural;
            ViewBag.fechaInicio =DateTimeUtil.FechaActual().AddDays(-7).ToString("dd/MM/yyyy");
            ViewBag.fechaFin =DateTimeUtil.FechaActual().ToString("dd/MM/yyyy");
            ViewBag.tasaIGV = TransaccionSettings.Default.TasaIGV;
            ViewBag.aplicaLeyAmazonia = TransaccionSettings.Default.AplicaLeyAmazonia;
            ViewBag.comprasDestinadasAPorDefecto = TransaccionSettings.Default.ComprasDestinadasAPorDefecto;
            ViewBag.idTipoActorPersonaJuridica = ActorSettings.Default.IdTipoActorPersonaJuridica;
            ViewBag.realizaCompraAlCredito = TransaccionSettings.Default.RealizaCompraAlCredito;
            ViewBag.permitirLoteEnDetalleDeCompra = AplicacionSettings.Default.PermitirLoteEnDetalleDeCompra;
            ViewBag.permitirRegistroEnDetalleDeCompra = AplicacionSettings.Default.PermitirRegistroEnDetalleDeCompra;
            ViewBag.permitirVencimientoEnDetalleDeCompra = AplicacionSettings.Default.PermitirVencimientoEnDetalleDeCompra;
            ViewBag.idProveedorGenerico = ActorSettings.Default.idProveedorGenerico;
            ViewBag.idTipoPersonaSeleccionadaPorDefecto = ActorSettings.Default.IdTipoPersonaSeleccionadaPorDefecto;
            ViewBag.idTipoDocumentoSeleccionadaConTipoPersonaNatural = ActorSettings.Default.IdTipoDocumentoSeleccionadaConTipoPersonaNatural;
            ViewBag.idTipoDocumentoSeleccionadaConTipoPersonaJuridica = ActorSettings.Default.IdTipoDocumentoSeleccionadaConTipoPersonaJuridica;
            ViewBag.idTipoDocumentoIdentidadDni = ActorSettings.Default.IdTipoDocumentoIdentidadDni;
            ViewBag.idTipoDocumentoIdentidadRuc = ActorSettings.Default.IdTipoDocumentoIdentidadRuc;
            ViewBag.fechaActual =DateTimeUtil.FechaActual().Date;
            ViewBag.permitirRegistroFlete = AplicacionSettings.Default.PermitirRegistroFleteEnCompra;
            ViewBag.idDocumentoNotaAlamacenInterna = MaestroSettings.Default.IdDetalleMaestroComprobanteNotaAlmacenInterna;
            ViewBag.direccionSede = (ObtenerSede().DomicilioFiscal != null) ? ObtenerSede().DomicilioFiscal.Detalle : " ";
            ViewBag.idUbigeoSede = (ObtenerSede().DomicilioFiscal != null) ? ObtenerSede().DomicilioFiscal.Ubigeo.Id : 0;
            ViewBag.idModalidadTrasladoPorDefecto = MaestroSettings.Default.IdDetalleMaestroModalidadDeTrasladoTransportePublico;
            ViewBag.idMotivoTrasladoPorDefecto = MaestroSettings.Default.IdDetalleMaestroMotivoDeTrasladoPorCompra;
            ViewBag.idUbigeoSeleccionadoPorDefecto = ActorSettings.Default.idUbigeoSeleccionadoPorDefectoEnProveedor;
            ViewBag.idUbigeoNoEspecificado = ActorSettings.Default.idUbigeoNoEspecificado;
            ViewBag.permitirMultipleIngresoDelMismoDetalle = AplicacionSettings.Default.PermitirMultipleIngresoDelMismoDetalleEnCompra;
            ViewBag.idDetalleMaestroMedioDePagoEfectivo = MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo;
            ViewBag.idDetalleMaestroEntidadBancariaNinguna = MaestroSettings.Default.IdDetalleMaestroEntidadBancariaNinguna;
            ViewBag.ValorPrecioVentaPorDefectoQueNoSeDebeGuardar = AplicacionSettings.Default.ValorPrecioVentaPorDefectoQueNoSeDebeGuardar;
            ViewBag.idEstablecimientoPorDefecto = ProfileData().CentroDeAtencionSeleccionado.EstablecimientoComercial.Id;
            ViewBag.idCentroDeAtencionPorDefecto = ProfileData().CentroDeAtencionSeleccionado.Id;
            ViewBag.IdPresentacionPorDefecto = ConceptoSettings.Default.idPresentacionPorDefecto;
            ViewBag.IdUnidadMedidaPorDefecto = ConceptoSettings.Default.idUnidadMedidaPorDefecto;
            ViewBag.IdPresentacionAOcultarEnNombreConceptoNegocio = ConceptoSettings.Default.IdPresentacionAOcultarEnNombreConceptoNegocio;
            ViewBag.IdUnidadDeMedidaComercialAOcultarEnNombreConceptoNegocio = ConceptoSettings.Default.IdUnidadDeMedidaComercialAOcultarEnNombreConceptoNegocio;
            ViewBag.numeroDecimalesEnCantidad = AplicacionSettings.Default.NumeroDecimalesEnCantidad;
            ViewBag.numeroDecimalesEnPrecio = AplicacionSettings.Default.NumeroDecimalesEnPrecio;
            return View();
        }

        #region COMPRA
        public JsonResult ObtenerCompras(string desde, string hasta)
        {

            DateTime fechaDesde = DateTimeUtil.ObtenerFechaDesdeConPrecisionDeMilisegundos(desde);
            DateTime fechaHasta = DateTimeUtil.ObtenerFechaHastaConPrecisionDeMilisegundos(hasta + " 23:59:59");

            try
            {
                List<OperacionDeCompra> operacionesDeCompra = _logicaOperacion.ObtenerOperacionesDeCompra(ProfileData().Empleado.Id, fechaDesde, fechaHasta);
                List<BandejaCompraViewModel> respuesta = BandejaCompraViewModel.Convert_(operacionesDeCompra);
                return Json(respuesta);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult ObtenerCompraYDetallesOrdenCompra(long idCompra)
        {
            try
            {
                Compra resultado = _logicaOperacion.ObtenerCompra(idCompra);
                return Json(new CompraConDetallesViewModel(resultado.OrdenDeCompra()));
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerOrdenCompraYDetallesOrden(long idOrdenCompra)
        {
            try
            {
                OrdenDeCompra resultado = _logicaOperacion.ObtenerOrdenDeCompra(idOrdenCompra);
                return Json(new CompraConDetallesViewModel(resultado));
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public async Task<JsonResult> ObtenerTiposDeComprobanteParaCompra()
        {
            try
            {
                var resultados = await _logicaOperacion.ObtenerTiposDeComprobanteParaCompra(ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado);
                List<SelectorTipoDeComprobante> comprobantes = SelectorTipoDeComprobante.Convert(resultados);

                return Json(comprobantes);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult NumeroComprobanteEsValido(int idProveedor, int idTipoComprobante, string numeroDeSerie, int numeroComprobante)
        {
            try
            {
                bool valido = _logicaOperacion.ExisteNumeroDeComprobante(idProveedor, idTipoComprobante, numeroDeSerie, numeroComprobante, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado);
                return Json(new { valido });
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ConfirmarCompra(RegistroCompraViewModel compra)
        {
            try
            {
                OperationResult result = null;
                List<DetalleDeOperacion> detalles = RegistroDetalleCompraViewModel.Convert(compra.Detalles.ToList());

                if (compra.EsCompraACredito)
                {
                    if (compra.EsCreditoRapido)
                    {
                        result = _logicaOperacion.ConfirmarCompraAlCreditoRapido(ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado, ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado, compra.Proveedor.Id, compra.TipoCompra, compra.TipoDeComprobante.EsPropio == true ? 0 : compra.TipoDeComprobante.TipoComprobante.Id, compra.TipoDeComprobante.EsPropio == true && compra.TipoDeComprobante.SerieSeleccionada == 0 ? compra.TipoDeComprobante.Series.First().Id : compra.TipoDeComprobante.SerieSeleccionada, compra.TipoDeComprobante.EsPropio, compra.TipoDeComprobante.SerieIngresada, compra.TipoDeComprobante.NumeroIngresado, compra.Observacion, compra.FechaRegistro, detalles, compra.Flete, ProfileData());
                    }
                    else
                    {//Compra a credito configurado
                        result = _logicaOperacion.ConfirmarCompraAlCredito(ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado, ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado, ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado, compra.Proveedor.Id, compra.TipoCompra, compra.TipoDeComprobante.EsPropio == true ? 0 : compra.TipoDeComprobante.TipoComprobante.Id, compra.TipoDeComprobante.EsPropio == true && compra.TipoDeComprobante.SerieSeleccionada == 0 ? compra.TipoDeComprobante.Series.First().Id : compra.TipoDeComprobante.SerieSeleccionada, compra.TipoDeComprobante.EsPropio, compra.TipoDeComprobante.SerieIngresada, compra.TipoDeComprobante.NumeroIngresado, compra.Observacion, compra.FechaRegistro, detalles, compra.Flete, ConstruirCuotas(compra.Cuotas.ToList()), ProfileData());
                    }
                }
                else
                {
                    result = _logicaOperacion.ConfirmarCompra(ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado, ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado, ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado, compra.Proveedor.Id, compra.TipoCompra, compra.TipoDeComprobante.EsPropio == true ? 0 : compra.TipoDeComprobante.TipoComprobante.Id, compra.TipoDeComprobante.EsPropio == true && compra.TipoDeComprobante.SerieSeleccionada == 0 ? compra.TipoDeComprobante.Series.First().Id : compra.TipoDeComprobante.SerieSeleccionada, compra.TipoDeComprobante.EsPropio, compra.TipoDeComprobante.SerieIngresada, compra.TipoDeComprobante.NumeroIngresado, compra.Observacion, compra.FechaRegistro, detalles, compra.Flete, ProfileData());
                }

                Util.ManageIfResultIsNotSuccess(result, "ERROR AL CONFIRMAR LA COMPRA");
                return Json(new { result.code_result, result.data, result_description = result.title });
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);

            }
        }
        #endregion

        #region INVALIDACION DE COMPRA
        public JsonResult InvalidarCompra(long idOrden, string observacion)
        {
            try
            {
                OperationResult result = _logicaOperacion.InvalidarCompra(idOrden, ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado, observacion, ProfileData());

                Util.ManageIfResultIsNotSuccess(result, "Error al invalidar la compra");
                return Json(new { result.code_result, result.data, result_description = result.title });
            }
            catch (LogicaException oe)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(oe), HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }

        public JsonResult ObtenerOrdenCompraYDetallesParaAnulacionDeCompra(long idOrdenCompra)
        {
            try
            {
                OrdenDeCompra resultado = _logicaOperacion.ObtenerOrdenDeCompra(idOrdenCompra);
                return Json(new RegistroAnulacionCompraViewModel(resultado));
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }
        #endregion


        public JsonResult RegistrarComprobanteCompraCorporativa(long idCompra, SelectorTipoDeComprobante comprobante)
        {
            try
            {
                OperationResult result;
                result = _logicaOperacion.RegistrarComprobanteCompraCorporativa(ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado, idCompra, comprobante.TipoComprobante.Id, comprobante.SerieSeleccionada, comprobante.EsPropio, comprobante.SerieIngresada, comprobante.NumeroIngresado);
                Util.ManageIfResultIsNotSuccess(result, "ERROR AL REGISTRAR EL COMPROBANTE LA COMPRA");
                return Json(new { code_result = result.code_result, data = result.data, result_description = result.title });
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public async Task<JsonResult> CargarCompraCorporativaYDetallesAEditar(long idOrdenCompra)
        {
            try
            {
                OrdenDeCompra resultado = _logicaOperacion.ObtenerOrdenDeCompra(idOrdenCompra);
                return Json(new RegistroCompraViewModel(resultado, SelectorTipoDeComprobante.Convert(await _logicaOperacion.ObtenerTiposDeComprobanteParaCompra(ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado))));
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }

        public List<Cuota> ConstruirCuotas(List<RegistroDetalleFinanciamientoViewModel> cuotas)
        {
            List<Cuota> cuotasConstruidas = new List<Cuota>();
            foreach (var item in cuotas)
            {
                cuotasConstruidas.Add(new Cuota()
                {
                    id = item.IdCuota,
                    codigo = "",
                    fecha_emision = item.FechaVencimiento,
                    fecha_vencimiento = item.FechaVencimiento,
                    capital = item.CapitalCuota,
                    interes = item.InteresCuota,
                    total = item.ImporteCuota,
                    por_cobrar = false,
                    cuota_inicial = item.EsCuotaInicial
                });
            }
            return cuotasConstruidas;
        }

        public List<DetalleDeOperacion> ConstruirDetalleMovimientoMercaderia(RegistroMovimientoDeAlmacenViewModel ingresoMercaderia)
        {
            List<DetalleDeOperacion> detallesConstruidos = new List<DetalleDeOperacion>();
            foreach (var item in ingresoMercaderia.Detalles)
            {
                detallesConstruidos.Add(new DetalleDeOperacion(item.IdProducto, item.IngresoSalidaActual, 1, 1, 0, 0, 0, null, item.Lote, null, null, item.EsBien,null, null));
            }
            return detallesConstruidos;
        }

        public List<DetalleDeOperacion> ConstruirDetalleMovimientoMercaderiaDeBienes(RegistroMovimientoDeAlmacenViewModel ingresoMercaderia)
        {
            List<DetalleDeOperacion> detallesConstruidos = new List<DetalleDeOperacion>();
            foreach (var item in ingresoMercaderia.Detalles)
            {
                detallesConstruidos.Add(new DetalleDeOperacion(item.IdProducto, item.IngresoSalidaActual, 1, 1, 0, 0, 0, null, item.Lote, null, null, item.EsBien, null, null));
            }
            return detallesConstruidos;
        }


  

        #region OBTENCION DE DOCUMENTO PARA OPERACIONES
        public JsonResult ObtenerDocumentoDeCompra(long idOrdenDeCompra)
        {
            try
            {
                DocumentoDeOperacionViewModel respuesta;
                OrdenDeCompra ordenDeCompra = _logicaOperacion.ObtenerOrdenDeCompra(idOrdenDeCompra);
                var sede = ObtenerSede();

                var htmlString =CoreHtmlStringBuilder.ObtenerHtmlString(ordenDeCompra, FormatoImpresion.A4, new byte[] { }, sede, this, _logicaMaestro);
                respuesta = new DocumentoDeOperacionViewModel(ordenDeCompra, htmlString, false, null);

                return Json(respuesta);
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult ObtenerCompraCorporativa(long idOrdenDeCompra)
        {
            try
            {
                CompraCorporativaViewModel respuesta;
                OrdenDeCompra ordenDeCompra = _logicaOperacion.ObtenerOrdenDeCompra(idOrdenDeCompra);
                var sede = ObtenerSede();

                var htmlString = CoreHtmlStringBuilder.ObtenerHtmlString(ordenDeCompra, FormatoImpresion.A4, new byte[] { }, sede, this, _logicaMaestro);
                respuesta = new CompraCorporativaViewModel(ordenDeCompra, htmlString);

                return Json(respuesta);
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return new JsonHttpStatusResult(Util.ErrorJson(e), HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region NOTAS DE CREDITO Y DEBITO EN COMPRA
        public JsonResult ObtenerTiposDeComprobanteParaNotaDeDebitoCreditoDeCompra(bool esParaNotaDeDebito)
        {
            try
            {
                var resultados = esParaNotaDeDebito ? _logicaOperacion.ObtenerTiposDeComprobanteParaNotaDeDebito(ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado) :
                    _logicaOperacion.ObtenerTiposDeComprobanteParaNotaDeCredito(ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado);
                List<SelectorTipoDeComprobante> comprobantes = SelectorTipoDeComprobante.ConvertirNoPropios(resultados);
                return Json(comprobantes);
            }
            catch (Exception e)
            {
                return Json(Util.ErrorJson(e));
            }
        }

        public JsonResult GuardarNotaDeDebitoCreditoDeCompra(RegistroDeNotaViewModel registroDeNota)
        {
            try
            {
                List<DetalleOrdenDeNota> detalles = ConstruirDetalleTransaccionParaNotaDeCompra(registroDeNota);
                OperationResult resultado = registroDeNota.EsDebito ?
                    _logicaOperacion.GuardarNotaDeDebitoDeCompra(registroDeNota.IdOrdenDeOperacion, registroDeNota.TipoNota.Id, registroDeNota.Observacion, registroDeNota.Comprobante.EsPropio == true ? 0 : registroDeNota.Comprobante.TipoComprobante.Id, registroDeNota.Comprobante.SerieSeleccionada, registroDeNota.Comprobante.EsPropio, registroDeNota.Comprobante.SerieIngresada, registroDeNota.Comprobante.NumeroIngresado, registroDeNota.MontoNota.ToString(), detalles, ProfileData()) :
                    _logicaOperacion.GuardarNotaDeCreditoDeCompra(registroDeNota.IdOrdenDeOperacion, registroDeNota.TipoNota.Id, registroDeNota.Observacion, registroDeNota.Comprobante.EsPropio == true ? 0 : registroDeNota.Comprobante.TipoComprobante.Id, registroDeNota.Comprobante.SerieSeleccionada, registroDeNota.Comprobante.EsPropio, registroDeNota.Comprobante.SerieIngresada, registroDeNota.Comprobante.NumeroIngresado, registroDeNota.MontoNota.ToString(), detalles, ProfileData());
                Util.ManageIfResultIsNotSuccess(resultado, "ERROR AL REGISTRAR LA NOTA");
                return Json(new { resultado.code_result, resultado.data, result_description = resultado.title });
            }
            catch (LogicaException oe)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(oe), HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                return new JsonHttpStatusResult(Util.ErrorJson(new Exception("ERROR AL INTENTAR GUARDAR LA NOTA", e)), HttpStatusCode.InternalServerError);
            }
        }

        public List<DetalleOrdenDeNota> ConstruirDetalleTransaccionParaNotaDeCompra(RegistroDeNotaViewModel registroDeNota)
        {
            List<DetalleOrdenDeNota> detallesConstruidos = new List<DetalleOrdenDeNota>();
            foreach (var item in registroDeNota.Detalles)
            {
                detallesConstruidos.Add(new DetalleOrdenDeNota()
                {
                    Cantidad = item.Cantidad,
                    Producto = new Concepto_Negocio_Comercial()
                    {
                        Id = item.IdConcepto,
                    },
                    Detalle = item.Observacion,
                    PrecioUnitario = item.Precio,
                    Importe = item.Importe,
                    Descuento = item.Descuento,
                    MontoDetalle = item.MontoDetalle
                });
            }
            return detallesConstruidos;
        }
        #endregion

        #region VISUALIZACION Y ENVIO DE COMPROBANTES


        #endregion

        #region CODIGO NO USADO

        //public JsonResult AnularCompra(RegistroAnulacionCompraViewModel anulacion)
        //{
        //    try
        //    {
        //        OperationResult result = null; //_logicaOperacion.AnularCompra(anulacion.Id, ProfileData().Empleado.Id,
        //        //        ProfileData().IdCentroDeAtencionSeleccionado,
        //        //        anulacion.TipoDeComprobante.EsPropio == true ? 0 : anulacion.TipoDeComprobante.TipoComprobante.Id,
        //        //        anulacion.TipoDeComprobante.EsPropio == true && anulacion.TipoDeComprobante.SerieSeleccionada == 0 ? anulacion.TipoDeComprobante.Series.First().Id : anulacion.TipoDeComprobante.SerieSeleccionada,
        //        //        anulacion.TipoDeComprobante.EsPropio,
        //        //        anulacion.TipoDeComprobante.SerieIngresada,
        //        //        anulacion.TipoDeComprobante.NumeroIngresado,
        //        //        anulacion.Observacion);
        //        Util.verificarError(result);
        //        return Json(new { code_result = result.code_result, data = result.data, result_description = result.title });
        //    }
        //    catch (LogicaException oe)
        //    {
        //        return new JsonHttpStatusResult(Util.errorJson(oe), HttpStatusCode.InternalServerError);
        //    }
        //    catch (Exception e)
        //    {
        //        return new JsonHttpStatusResult(Util.errorJson(e), HttpStatusCode.InternalServerError);
        //    }
        //}

        //public JsonResult InvalidarAnulacionDeCompra(long idOrden, string observacion)
        //{
        //    try
        //    {
        //        OperationResult result = null;// _logicaOperacion.InvalidarAnulacionDeCompra(idOrden, ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado, observacion);

        //        Util.verificarError(result);
        //        return Json(new { code_result = result.code_result, data = result.data, result_description = result.title });
        //    }
        //    catch (LogicaException oe)
        //    {
        //        return new JsonHttpStatusResult(Util.errorJson(oe), HttpStatusCode.InternalServerError);
        //    }
        //    catch (Exception e)
        //    {
        //        return new JsonHttpStatusResult(Util.errorJson(e), HttpStatusCode.InternalServerError);
        //    }
        //}

        //public JsonResult ObtenerTiposDeComprobanteParaAnulacionCompra()
        //{
        //    try
        //    {
        //        var resultados = _logicaOperacion.obtenerTiposDeComprobanteParaAnulacionCompra(ProfileData().Empleado.Id, ProfileData().IdCentroDeAtencionSeleccionado);
        //        List<SelectorTipoDeComprobante> comprobantes = SelectorTipoDeComprobante.Convert(resultados);

        //        return Json(comprobantes);
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(Util.errorJson(e));
        //    }
        //}

        /*/public List<Detalle_transaccion> ConstruirDetalleMovimientoMercaderia(RegistroTrasladoDeMercaderiaViewModel ingresoMercaderia)
        //{
        //    List<Detalle_transaccion> detallesConstruidos = new List<Detalle_transaccion>();
        //    foreach (var item in ingresoMercaderia.Detalles)
        //    {
        //        detallesConstruidos.Add(new Detalle_transaccion(item.IngresoSalidaActual, item.IdProducto, "", 1, 1, null, 0, null, null, 0, 0, 0));
        //    }
        //    return detallesConstruidos;
        //}*/

        //public List<Detalle_transaccion> ConstruirDetalleTransaccion(RegistroCompraViewModel compra)
        //{
        //    List<Detalle_transaccion> detallesConstruidos = new List<Detalle_transaccion>();
        //    foreach (var item in compra.Detalles)
        //    {
        //        detallesConstruidos.Add(new Detalle_transaccion(
        //            item.IdDetalle,
        //            item.Cantidad,
        //            item.Producto.Id,
        //            item.Observacion,
        //            item.PrecioUnitario,
        //            item.Importe,
        //            null, 0, null, null, 0, 0,
        //            item.Descuento,
        //            item.Lote,
        //            item.Vencimiento == null ? null : item.Vencimiento,
        //            item.Registro));
        //    }
        //    return detallesConstruidos;
        //}

        /*
       public JsonResult AnularOrdenesDeCompra(RegistroCompraViewModel facturacion, long[] idsOrdenes)
       {
           try
           {
               OperationResult result = _logicaOperacion.anularOperacionesDeCompra(ProfileData().Empleado.Id, ProfileData().IdEntidadInternaSeleccionada, facturacion.Proveedor.Id, facturacion.TipoDeComprobante.SerieSeleccionada == 0 ? facturacion.TipoDeComprobante.Series.First().Id : facturacion.TipoDeComprobante.SerieSeleccionada, facturacion.TipoDeComprobante.SerieIngresada, facturacion.TipoDeComprobante.NumeroIngresado, facturacion.FechaRegistro, facturacion.FechaRegistro,
                   facturacion.Observacion, idsOrdenes);

               Util.verificarError(result);
               return Json(new { result.code_result, result.data, result_description = result.title });
           }
           catch (Exception e)
           {
               HttpContext.Response.StatusCode = 500;
               return Json(Util.errorJson(e));
           }
       }*/

        //public JsonResult DescontarOrdenesDeCompra(RegistroCompraViewModel facturacion, List<OrdenVentaCompraViewModel> ordenes)
        //{
        //    try
        //    {
        //        ///obtener detalles a anular
        //        List<Detalle_transaccion> detalles = new List<Detalle_transaccion>();
        //        List<OrdenDeVentaCompraParaDescuentoDebito> ordenDescuento = new List<OrdenDeVentaCompraParaDescuentoDebito>();
        //        foreach (var orden in ordenes)
        //        {
        //            List<Detalle_transaccion> detallesDescuento = new List<Detalle_transaccion>();

        //            foreach (var detalle in orden.Detalles)
        //            {
        //                if (detalle.Descuento > 0)
        //                {
        //                    detalles.Add(new Detalle_transaccion(1, detalle.Concepto.Id, "", detalle.Descuento, detalle.Descuento, null, 0, null, null, 0, 0, 0));
        //                    detallesDescuento.Add(new Detalle_transaccion(1, detalle.Concepto.Id, "", detalle.Descuento, detalle.Descuento, null, 0, null, null, 0, 0, 0));
        //                }

        //            }
        //            ordenDescuento.Add(new OrdenDeVentaCompraParaDescuentoDebito(orden.Id, detallesDescuento));
        //        }
        //        List<Detalle_transaccion> detallesfiltrados = detalles.GroupBy(d => d.id_concepto_negocio).Select(g => new Detalle_transaccion
        //        {
        //            id = 0,
        //            cantidad = g.First().cantidad,
        //            precio_unitario = g.Sum(s => s.precio_unitario),
        //            total = g.Sum(s => s.total),
        //            id_concepto_negocio = g.First().id_concepto_negocio
        //        }).ToList();
        //        long[] idsOrdenes = ordenes.Select(o => o.Id).ToArray();

        //        OperationResult result = _logicaFacturacion.descontarOperacionesDeCompra(IdEmpleado(), facturacion.Proveedor.Id, facturacion.TipoDeComprobante.Id,
        //            facturacion.NumeroSerieDeComprobante, facturacion.NumeroDeComprobante, facturacion.FechaRegistro, facturacion.FechaRegistro,
        //            facturacion.Observacion, idsOrdenes, detallesfiltrados, ordenDescuento);
        //        Util.verificarError(result);
        //        return Json(new { code_result = result.code_result, data = result.data, result_description = result.title });
        //    }
        //    catch (Exception e)
        //    {
        //        HttpContext.Response.StatusCode = 500;
        //        return Json(Util.errorJson(e));
        //    }
        //}

        //public JsonResult DebitoOrdenesDeCompra(RegistroCompraViewModel facturacion, List<OrdenVentaCompraViewModel> ordenes)
        //{
        //    try
        //    {
        //        ///obtener detalles a anular
        //        List<Detalle_transaccion> detalles = new List<Detalle_transaccion>();
        //        List<OrdenDeVentaCompraParaDescuentoDebito> ordenDebito = new List<OrdenDeVentaCompraParaDescuentoDebito>();
        //        foreach (var orden in ordenes)
        //        {
        //            List<Detalle_transaccion> detallesDebito = new List<Detalle_transaccion>();

        //            foreach (var detalle in orden.Detalles)
        //            {
        //                if (detalle.Descuento > 0)
        //                {
        //                    detalles.Add(new Detalle_transaccion(1, detalle.Concepto.Id, "", detalle.Descuento, detalle.Descuento, null, 0, null, null, 0, 0, 0));
        //                    detallesDebito.Add(new Detalle_transaccion(1, detalle.Concepto.Id, "", detalle.Descuento, detalle.Descuento, null, 0, null, null, 0, 0, 0));
        //                }
        //            }
        //            ordenDebito.Add(new OrdenDeVentaCompraParaDescuentoDebito(orden.Id, detallesDebito));
        //        }
        //        List<Detalle_transaccion> detallesfiltrados = detalles.GroupBy(d => d.id_concepto_negocio).Select(g => new Detalle_transaccion
        //        {
        //            id = 0,
        //            cantidad = g.First().cantidad,
        //            precio_unitario = g.Sum(s => s.precio_unitario),
        //            total = g.Sum(s => s.total),
        //            id_concepto_negocio = g.First().id_concepto_negocio
        //        }).ToList();
        //        long[] idsOrdenes = ordenes.Select(o => o.Id).ToArray();

        //        OperationResult result = _logicaFacturacion.debitoOperacionesDeCompra(IdEmpleado(), facturacion.Proveedor.Id, facturacion.TipoDeComprobante.Id,
        //            facturacion.NumeroDeComprobante, facturacion.NumeroDeComprobante, facturacion.FechaRegistro, facturacion.FechaRegistro,
        //            facturacion.Observacion, idsOrdenes, detallesfiltrados, ordenDebito);

        //        Util.verificarError(result);
        //        return Json(new { code_result = result.code_result, data = result.data, result_description = result.title });
        //    }
        //    catch (Exception e)
        //    {
        //        HttpContext.Response.StatusCode = 500;
        //        return Json(Util.errorJson(e));
        //    }
        //}

        /*/public JsonResult ContabilizarCompraCorporativa(long idOrdenCompra)
        //{
        //    try
        //    {
        //        OrdenDeCompra resultado = _logicaOperacion.ObtenerOrdenDeCompra(idOrdenCompra);
        //        return Json(new CompraConDetallesViewModel(resultado));
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(Util.errorJson(e));
        //    }
        //}*/

        //public JsonResult ObtenerOrdenCompraCorporativaYDetalles(long idOrdenCompra)
        //{
        //    try
        //    {
        //        OrdenDeCompra resultado = _logicaOperacion.ObtenerOrdenDeCompra(idOrdenCompra);
        //        return Json(new CompraCorporativaConDetallesViewModel(resultado));
        //    }
        //    catch (Exception e)
        //    {
        //        return new JsonHttpStatusResult(Util.errorJson(e), HttpStatusCode.InternalServerError);
        //    }
        //}

        #endregion
    }
}
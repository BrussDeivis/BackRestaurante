using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades
{
    public class ConfiguracionRegistradorGuiaRemision
    {
        public readonly int IdUbigeoSeleccionadoPorDefecto = ActorSettings.Default.idUbigeoSeleccionadoPorDefectoEnProveedor;
        public readonly int IdUbigeoNoEspecificado = ActorSettings.Default.idUbigeoNoEspecificado;
        public readonly int IdDocumentoNotaAlamacenInterna = MaestroSettings.Default.IdDetalleMaestroComprobanteNotaAlmacenInterna;
        public string DireccionSede;
        public int IdUbigeoSede;
        public readonly int IdModalidadTrasladoPorDefecto = MaestroSettings.Default.IdDetalleMaestroModalidadDeTrasladoTransportePublico;
        public readonly int IdModalidadTrasladoPublico = MaestroSettings.Default.IdDetalleMaestroModalidadDeTrasladoTransportePublico;
        public readonly int IdModalidadTrasladoPrivado = MaestroSettings.Default.IdDetalleMaestroModalidadDeTrasladoTransportePrivado;
        public readonly int IdMotivoTrasladoPorVenta = MaestroSettings.Default.IdDetalleMaestroMotivoDeTrasladoPorVenta;
        public readonly int IdMotivoTrasladoPorCompra = MaestroSettings.Default.IdDetalleMaestroMotivoDeTrasladoPorCompra;
        public readonly int IdMotivoTrasladoPorImportacion = MaestroSettings.Default.IdDetalleMaestroMotivoDeTrasladoPorImportacion;
        public readonly int IdMotivoTrasladoPorExportacion = MaestroSettings.Default.IdDetalleMaestroMotivoDeTrasladoPorExportacion;
        public readonly int IdMotivoTrasladoPorTrasladoAZonaPrimaria = MaestroSettings.Default.IdDetalleMaestroMotivoDeTrasladoPorTrasladoAZonaPrimaria;
        public readonly int IdMotivoTrasladoPorTrasladoEmisorItineranteCP = MaestroSettings.Default.IdDetalleMaestroMotivoDeTrasladoPorTrasladoEmisorItineranteCP;
        public readonly int IdMotivoTrasladoPorVentaSujetaAConfirmacionDeComprador = MaestroSettings.Default.IdDetalleMaestroMotivoDeTrasladoPorVentaSujetaAConfirmacionDeComprador;
        public readonly int IdMotivoTrasladoPorTrasladoEntreEstablecimientosDeLaMismaEmpresa = MaestroSettings.Default.IdDetalleMaestroMotivoDeTrasladoPorTrasladoEntreEstablecimientosDeLaMismaEmpresa;
        public readonly int IdMotivoTrasladoOtros = MaestroSettings.Default.IdDetalleMaestroMotivoDeTrasladoOtros;
        public readonly int IdTipoDeComprobantePorDefecto = AplicacionSettings.Default.IdTipoDeComprobantePorDefectoEnSalidaDeMercaderia;
        public readonly int IdRolCliente = ActorSettings.Default.IdRolCliente;
        public readonly int IdRolProveedor = ActorSettings.Default.IdRolProveedor;
        public readonly bool MostrarBuscadorCodigoBarra = AplicacionSettings.Default.MostrarBuscadorCodigoBarraEnGuiaRemision;
        public readonly int ModoSeleccionConcepto = AplicacionSettings.Default.ModoDeSeleccionDeConceptoDeNegocioEnGuiaRemision;
        public readonly int ModoSeleccionTipoFamilia = AplicacionSettings.Default.ModoDeSeleccionTipoDeFamiliaEnGuiaRemision;
        public readonly int NumeroDecimalesEnCantidad = AplicacionSettings.Default.NumeroDecimalesEnCantidad;
        public readonly int MinimoCaracteresBuscarConcepto = AplicacionSettings.Default.MinimoDeCaracteresParaBuscarEnSelectorConcepto;
        public readonly int TiempoEsperaBusquedaSelector = AplicacionSettings.Default.TiempoDeEsperaEnBusquedaSelectoresDeGranCantidad;
        public readonly int MinimoCaracteresBuscarActorComercial = ActorSettings.Default.MinimoDeCaracteresParaBuscarEnSelectorActorComercial;
        public readonly string MascaraDeVisualizacionValidacionRegistroProveedor = ActorSettings.Default.MascaraDeVisualizacionValidacionRegistroProveedor;
        public string NumeroDocumentoSede;
        public readonly int InformacionSelectorConcepto = (int)Entidades.InformacionSelectorConcepto.Nombre;
        public readonly int IdTipoDocumentoIdentidadRuc = ActorSettings.Default.IdTipoDocumentoIdentidadRuc;
        public readonly int IdTipoDocumentoIdentidadDni = ActorSettings.Default.IdTipoDocumentoIdentidadDni;
        public readonly int IdProveedorGenerico = ActorSettings.Default.idProveedorGenerico;

    }
}

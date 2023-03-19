using OpenInvoicePeru.Comun.Dto.Modelos;
using System;
using System.Collections.Generic;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.Entidades.ComprobantesModel;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.EFactura;

namespace Tsp.Sigescom.Modelo.Interfaces.Logica
{
    public interface IGeneracionArchivosLogica
    {
        List<DocumentoBaja> CrearBajasComunicacionBaja(Documento[] documentos);
        List<DetalleDocumento> CrearDetalles(List<Detalle> detalles);
        List<DetalleDocumento> CrearDetalles(List<Detalle> detalles, decimal valorIcbper);
        List<DetalleGuia> CrearDetallesGuia(List<Detalle> detalles);
        List<GrupoResumenNuevo> CrearGrupoResumenNuevo(Documento[] documentos, bool cambiarEstadoItemDeAnuladoAAdicionado);
        bool DocumentoReferenciaFueAceptado(string tipoDocumento, string numeroDocumento);
        ComunicacionBaja ObtenerComunicacionBaja(Documento[] documentos, EstablecimientoComercialExtendido emisor);
        DocumentoElectronico ObtenerDocumentoElectronicoBoleta(BoletaDeVenta boletaDeVenta);
        DocumentoElectronico ObtenerDocumentoElectronicoFactura(Factura factura);
        GuiaRemision ObtenerDocumentoElectronicoGuiaDeRemision(GuiaDeRemision guiaDeRemision);
        DocumentoElectronico ObtenerDocumentoElectronicoNotaCredito(NotaDeCredito notaDeCredito);
        DocumentoElectronico ObtenerDocumentoElectronicoNotaDebito(NotaDeDebito notaDeDebito);
        int ObtenerIdentificadorComunicacionBaja(DateTime fechaConsulta);
        int ObtenerIdentificadorResumenDiario(DateTime fechaConsulta);
        ResumenDiarioNuevo ObtenerResumenDiario(Documento[] documentos, EstablecimientoComercialExtendido emisor, bool cambiarEstadoItemDeAnuladoAAdicionado);
    }
}
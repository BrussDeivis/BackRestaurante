using System;
using System.Collections.Generic;
using Tsp.FacturacionElectronica.Modelo;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.EFactura;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;

namespace Tsp.Sigescom.Modelo.Interfaces.Repositorio
{
    public interface IFacturacionRepositorio: IRepositorioBase
    {
        IFacturacionRepositorio ObtenerNuevoRepositorio();
        OperationResult ActualizarBinario(Binario binario);
        OperationResult ActualizarDocumento(Documento documento);
        OperationResult ActualizarDocumentoAEnviado(long idDocumento);
        OperationResult ActualizarDocumentosAEnviados(long[] idsDocumentos);
        OperationResult ActualizarEnvio(Envio envio);
        OperationResult ActualizarEnvios(List<Envio> enviosAActualizar);
        OperationResult ActualizarEstadoEnvio(Envio envioAActualizar);
        OperationResult ActualizarEstadoObservacionTicketEnvio(Envio envio);
        OperationResult ActualizarEstadoYObservacionEnvio(Envio envioAActualizar);
        OperationResult ActualizarObservacionYEstadoEnvios(List<Envio> enviosAActualizar);
        OperationResult CrearBinario(Binario archivoBinario);
        OperationResult CrearDocumento(Documento documento);
        OperationResult CrearDocumentosMasivos(List<Documento> ListaDocumentos);
        OperationResult CrearEnvio(Envio envio);
        OperationResult CrearEnvioDocumento(EnvioDocumento envioDocumento);
        OperationResult CrearEnvioDocumentoMasivo(long idEnvio, long[] idsDocumentos);
        bool DocumentoReferenciaFueAceptado(string tipoDocumentoReferencia, string numeroDocumentoReferencia, List<int> estadosDeAceptacion);
        bool HayDocumentosNoEnviados(string codigoTipoComprobante);
        bool HayDocumentosNoEnviados(string codigoTipoComprobante, int estado);
        bool HayEnvios(string tipoEnvio, int estado);
        IEnumerable<long> IdEnvios(string tipoEnvio, int estado);
        byte[] ObtenerBinario(long idBinario);
        Documento ObtenerDocumentoElectronico(long idDocumento);
        Documento ObtenerDocumentoElectronicoIncluidoBinario(long idDocumento);
        IEnumerable<Documento> ObtenerDocumentos();
        IEnumerable<Documento> ObtenerDocumentos(List<long> idDocumentos);
        IEnumerable<Documento> ObtenerDocumentos(string codigoTipoComprobante, EstadoSigescomDocumentoElectronico estadoSigescom, string tipoEnvio1, int numeroEnviosAceptados1, string tipoEnvio2, int numeroEnviosAceptados2);
        IEnumerable<Documento> ObtenerDocumentosAEnviar(string codigoTipoComprobante);
        IEnumerable<Documento> ObtenerDocumentosAEnviarPorDia(string codigoTipoComprobante, int estado);
        IEnumerable<Documento> ObtenerDocumentosEntreFechas(DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<Documento> ObtenerDocumentosEnviados(long idEnvio);
        IEnumerable<Documento> ObtenerDocumentosFaltantesAEmitirAnulacion(EstadoSigescomDocumentoElectronico estadoSigescom, int numeroEnviosAceptados, string codigoTipoComprobante, int[] estadosEnvioQueNoSeQuiere);
        IEnumerable<Documento> ObtenerDocumentosIncluidoBinarioAEnviar(string codigoTipoComprobante);
        IEnumerable<Documento> ObtenerDocumentosIncluidoBinarioAEnviar(string codigoTipoComprobante, int estadoDocumento);
        IEnumerable<Documento> ObtenerDocumentosIncluidoBinarioAEnviarPorDia(string codigoTipoComprobante);
        Envio ObtenerEnvio(long idEnvio);
        IEnumerable<Envio> ObtenerEnvios();
        IEnumerable<Envio> ObtenerEnviosIncluidoDocumentoIncluidoBinario(int estado);
        IEnumerable<Envio> ObtenerEnviosIncluidoDocumentoIncluidoBinario(string tipoEnvio, int estado);
        IEnumerable<Envio> ObtenerEnviosInclusiveEnvioDocumentoYDocumentoEntreFechas(DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<EnvioSimplificado> ObtenerEnviosSinCodigoDeRespuesta();
        IEnumerable<long> ObtenerIdDocumentosAEnviar(string codigoTipoComprobante);
        IEnumerable<long> ObtenerIdDocumentosAEnviarPorDia(string codigoTipoComprobante);
        IEnumerable<long> ObtenerIdDocumentosEnviados(long idEnvio);
        int ObtenerIdentificador(DateTime fechaConsulta, string tipoEnvio);
        OperationResult ActualizarEstadoDocumento(long idSigescom, EstadoDocumentoElectronico estado, EstadoSigescomDocumentoElectronico estadoSigescom);
    }
}
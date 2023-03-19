using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.EFactura;

namespace Tsp.Sigescom.WebApplication.Models.FacturacionElectronica
{
    public class DocumentoViewModel
    {
        public long Id { get; set; }
        public long IdOperacion { get; set; }
        public string FechaEmision { get; set; }
        public string CodigoTipoDocumento { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string SerieDocumento { get; set; }
        public string Estado { get; set; }
        public string EstadoSigescom { get; set; }
        public long IdBinarioDocumento { get; set; }

        public DocumentoViewModel(Documento documento)
        {
            Id = documento.id;
            IdOperacion = documento.idSigescom;
            FechaEmision = documento.fechaEmision.ToString("dd/MM/yyyy"); ;
            CodigoTipoDocumento = documento.codigoTipoComprobante;
            TipoDocumento = documento.tipoComprobante;
            NumeroDocumento = documento.numeroComprobante;
            SerieDocumento = documento.serieComprobante;
            Estado = documento.EnvioDocumento.Any() ? "ENVIADO" : "NO ENVIADO";
            EstadoSigescom = Enumerado.GetDescription((EstadoSigescomDocumentoElectronico)documento.estadoSigescom); 
            IdBinarioDocumento = documento.idBinarioDocumento;
        }
        //public static List<DocumentoViewModel> convert(List<Documento> comprobantes)
        //{
        //    List<DocumentoViewModel> Resultado = new List<DocumentoViewModel>();
        //    foreach (var item in comprobantes)
        //    {
        //        Resultado.Add(new DocumentoViewModel(item));
        //    }
        //    return Resultado;
        //}

    }
}
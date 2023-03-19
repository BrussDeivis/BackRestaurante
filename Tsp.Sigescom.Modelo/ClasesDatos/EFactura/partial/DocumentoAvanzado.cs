using Newtonsoft.Json;
using OpenInvoicePeru.Comun.Dto.Modelos;
using System;
using System.Text;
using Tsp.Sigescom.Modelo.Entidades.EFactura;

namespace Tsp.FacturacionElectronica.Modelo
{
    class DocumentoAvanzado : DocumentoElectronico
    {

        public DocumentoAvanzado(Documento documento)
        {
            try
            {
                DocumentoElectronico documentoElectronicoAvanzado = JsonConvert.DeserializeObject<DocumentoElectronico>(Encoding.UTF8.GetString(documento.Binario.archivoBinario));
                //documentoElectronicoAvanzado.Relacionados
                //deserializar_(documentoElectronicoAvanzado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

    }
}

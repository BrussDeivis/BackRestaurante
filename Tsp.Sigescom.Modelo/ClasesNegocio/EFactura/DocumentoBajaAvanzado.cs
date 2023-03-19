using Newtonsoft.Json;
using OpenInvoicePeru.Comun.Dto.Modelos;
using System;
using System.Text;

namespace Tsp.Sigescom.Modelo.Entidades.EFactura
{
    class DocumentoBajaAvanzado : DocumentoBaja
    {

        public DocumentoBajaAvanzado(Documento documento,int idLinea)
        {
            try
            {
                DocumentoElectronico documentoElectronicoAvanzado = JsonConvert.DeserializeObject<DocumentoElectronico>(Encoding.UTF8.GetString(documento.Binario.archivoBinario));

                deserializar_(documentoElectronicoAvanzado,idLinea);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void deserializar_(DocumentoElectronico documentoElectronico, int idLinea)
        {
            this.Id = idLinea;
            this.TipoDocumento = documentoElectronico.TipoDocumento;
            this.Serie = documentoElectronico.IdDocumento.Split('-')[0];
            this.Correlativo = documentoElectronico.IdDocumento.Split('-')[1];
            this.MotivoBaja = "ANULACION DE LA FACTURA "+ documentoElectronico.IdDocumento;
        }

       
    }
}

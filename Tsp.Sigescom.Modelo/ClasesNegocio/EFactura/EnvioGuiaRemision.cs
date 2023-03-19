using OpenInvoicePeru.Comun.Dto.Intercambio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.EFactura
{
    public class TokenEnvioRequest
    {
        public string grant_type { get; set; }
        public string scope { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
    public class TokenEnvioResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }

    public class EnvioDocumentoRequest
    {
        public Archivo archivo { get; set; }
    }

    public class Archivo
    {
        public string nomArchivo { get; set; }
        public string arcGreZip { get; set; }
        public string hashZip { get; set; }
    }
    public class EnvioDocumentoResponse
    {
        public string numTicket { get; set; }
        public DateTime fecRecepcion { get; set; }
    }

    public class RespuestaEnvioDocumentoResponse
    {
        public string codRespuesta { get; set; }
        public ErrorEnvioDocumentoResponse error { get; set; }
        public string arcCdr { get; set; }
        public string indCdrGenerado { get; set; }

        public EnviarDocumentoResponse Convertir()
        {
            return new EnviarDocumentoResponse
            {
                Exito = true,
                CodigoRespuesta = codRespuesta == FacturacionElectronicaSettings.Default.CodigoApiErrorRespuestaGuiaRemision ? error.numError : codRespuesta,
                MensajeRespuesta = codRespuesta == FacturacionElectronicaSettings.Default.CodigoApiErrorRespuestaGuiaRemision ? error.desError : (codRespuesta == FacturacionElectronicaSettings.Default.CodigoApiEnProcesoRespuestaGuiaRemision ? "EN PROCESO" : ""),
                TramaZipCdr = codRespuesta == FacturacionElectronicaSettings.Default.CodigoApiEnProcesoRespuestaGuiaRemision ? null : arcCdr,
                NroTicketCdr = "",
            };
        }
    }

    public class ErrorEnvioDocumentoResponse
    {
        public string numError { get; set; }
        public string desError { get; set; }
    }
}

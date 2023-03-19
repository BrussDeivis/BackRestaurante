using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades
{
    
    public class LogEnvio
    {
        public List<ItemEnvio> Exito { get; set; }
        public List<string> LogError { get; set; }
        public List<string> Error { get; set; }
        public string MensajeNoHayDocumentos { get; set; }
        public bool NoHayDocumentos { get; set; }
        public EstadoLogEnvio Estado()
        {
            return NoHayDocumentos ? EstadoLogEnvio.NoHay : (Exito.Count > 0 && Error.Count > 0) ? EstadoLogEnvio.Ambos : Error.Count > 0 ? EstadoLogEnvio.Error : EstadoLogEnvio.Exito;
        }
        public void LogNoHayDocumentos(bool noHayDocumentos, string mensajeNoHayDocumentos)
        {
            NoHayDocumentos = noHayDocumentos;
            MensajeNoHayDocumentos = mensajeNoHayDocumentos;
        }
        public LogEnvio()
        {
            Exito = new List<ItemEnvio>();
            LogError = new List<string>();
            Error = new List<string>();
        }
    }

    public class LogEnvioFacturacionElectronica
    {
        public LogEnvio Factura { get; set; }
        public LogEnvio BoletasVenta { get; set; }
        public LogEnvio NotaCredito { get; set; }
        public LogEnvio NotaDebito { get; set; }
        public LogEnvio GuiaRemision { get; set; }
        public LogEnvio ComunicacionBaja { get; set; }

        public LogEnvioFacturacionElectronica()
        {
            Factura = new LogEnvio();
            BoletasVenta = new LogEnvio();
            NotaCredito = new LogEnvio();
            NotaDebito = new LogEnvio();
            GuiaRemision = new LogEnvio();
        }

        public string CodigoEstadoEnvio()
        {
            return ((int)Factura.Estado()).ToString() + ((int)BoletasVenta.Estado()).ToString() + ((int)NotaCredito.Estado()).ToString() + ((int)NotaDebito.Estado()).ToString() + ((int)GuiaRemision.Estado()).ToString();
        }
    }

    public class ItemEnvio
    {
        public ModoEnvio ModoEnvio { get; set; }
        public string Mensaje { get; set; }

        public ItemEnvio()
        {

        }

        /// <summary>
        /// Crea y retorna un item de envio en modo = ADICION y se adjunta el <paramref name="mensaje"/>
        /// </summary>
        /// <param name="mensaje"></param>
        /// <returns></returns>
        public static ItemEnvio ItemAdicionado(string mensaje)
        {
            return new ItemEnvio()
            {
                ModoEnvio = ModoEnvio.Adicion,
                Mensaje = mensaje
            };
        }
        /// <summary>
        /// Crea y retorna un item de envio en modo = ANULACION y se adjunta el <paramref name="mensaje"/>
        /// </summary>
        /// <param name="mensaje"></param>
        /// <returns></returns>
        public static ItemEnvio ItemAnulado(string mensaje)
        {
            return new ItemEnvio()
            {
                ModoEnvio = ModoEnvio.Anulacion,
                Mensaje = mensaje
            };
        }
    }
}

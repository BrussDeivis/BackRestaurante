using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.EFactura;

namespace Tsp.Sigescom.WebApplication.Models.FacturacionElectronica
{
    public class EnvioViewModel
    {
        public long Id;
        public string FechaEnvio;
        public string FechaEmision;
        public string IdentificadorEnvio;
        public string TipoEnvio;
        public string ModoEnvio;
        public string DocumentosEnviados;
        public string Estado;
        public long IdBinarioEnvio;
        public long IdBinarioRespuesta;
        public string CodigoRespuesta;
        public string Observacion;
        public string NumeroTicket;
        public bool EsPendiente;
        public bool EsRechazado;
        public bool EsObservado;

        public EnvioViewModel()
        {
        }

        public EnvioViewModel(Envio envio)
        {
            Id = envio.id;
            FechaEnvio = envio.fechaEnvio.ToString("dd/MM/yyyy hh:mm:ss tt");
            FechaEmision = envio.EnvioDocumento.FirstOrDefault().Documento.fechaEmision.ToString("dd/MM/yyyy");
            IdentificadorEnvio = envio.identificadorEnvio;
            TipoEnvio = envio.tipoEnvio;
            ModoEnvio = Enumerado.GetDescription((ModoEnvio)envio.modoEnvio);
            if (TipoEnvio == FacturacionElectronicaSettings.Default.TipoEnvioIndividual)
            {
                DocumentosEnviados = envio.EnvioDocumento.SingleOrDefault().Documento.serieComprobante + "-" + envio.EnvioDocumento.SingleOrDefault().Documento.numeroComprobante;
            }
            else
            {
                if (envio.modoEnvio == (int)Modelo.Entidades.ModoEnvio.Anulacion)
                {
                    foreach (var item in envio.EnvioDocumento.Select(ed => ed.Documento).ToList())
                    {
                        DocumentosEnviados = DocumentosEnviados + "[" + item.serieComprobante + ":" + item.numeroComprobante + "], ";
                    }
                }
                else
                {
                    foreach (var item in envio.EnvioDocumento.Select(ed => ed.Documento.serieComprobante).Distinct().ToList())
                    {
                        DocumentosEnviados = DocumentosEnviados + "[" + item + ":" + envio.EnvioDocumento.Select(ed => ed.Documento).Where(s => s.serieComprobante == item).OrderBy(d => System.Convert.ToInt32(d.numeroComprobante)).FirstOrDefault().numeroComprobante + "-" + envio.EnvioDocumento.Select(ed => ed.Documento).Where(s => s.serieComprobante == item).OrderByDescending(d => System.Convert.ToInt32(d.numeroComprobante)).FirstOrDefault().numeroComprobante + "], ";
                    }
                }
                DocumentosEnviados = DocumentosEnviados.Substring(0, DocumentosEnviados.Length - 2);
            }
            Estado = Enumerado.GetDescription((EstadoEnvio)envio.estado);
            EsPendiente = envio.estado == (int)EstadoEnvio.Pendiente;
            EsRechazado = envio.estado == (int)EstadoEnvio.Rechazado;
            EsObservado = envio.estado == (int)EstadoEnvio.AceptadoConObservaciones;
            IdBinarioEnvio = envio.idBinarioEnvio;
            IdBinarioRespuesta = (envio.idBinarioRespuesta == null) ? 0 : (long)envio.idBinarioRespuesta;
            CodigoRespuesta = envio.codigoRespuesta;
            Observacion = envio.observacion;
            NumeroTicket = envio.numeroTicket;

        }
        public static List<EnvioViewModel> Convert(List<Envio> envios)
        {
            List<EnvioViewModel> Resultado = new List<EnvioViewModel>();
            foreach (var item in envios)
            {
                Resultado.Add(new EnvioViewModel(item));
            }
            return Resultado;
        }
    }
}
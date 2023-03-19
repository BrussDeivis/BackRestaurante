using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.EFactura;

namespace Tsp.Sigescom.WebApplication.Models.FacturacionElectronica
{
    public class EnvioIndividualVielModel : EnvioViewModel
    {
        public long IdSigescom;

        public EnvioIndividualVielModel()
        {
        }

        public EnvioIndividualVielModel(Envio envio) : base (envio)
        {
            IdSigescom = envio.EnvioDocumento.SingleOrDefault().Documento.idSigescom;
        }

        public new static List<EnvioIndividualVielModel> Convert(List<Envio> envios)
        {
            List<EnvioIndividualVielModel> Resultado = new List<EnvioIndividualVielModel>();
            foreach (var item in envios)
            {
                Resultado.Add(new EnvioIndividualVielModel(item));
            }
            return Resultado;
        }
    }
}
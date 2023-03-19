using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.TipoCambio;
using Tsp.Sigescom.Modelo.Interfaces.Logica;

namespace Tsp.Sigescom.Logica.Core.TipoDeCambio
{
    public class TipoDeCambio_Logica : ITipoDeCambio_Logica
    {
        public TipoCambio ObtenerTipoCambioDolarActual()
        {
            TipoCambio tipoCambio = new TipoCambio();
            try
            {
                string url = AplicacionSettings.Default.UrlApiConsultaTipoCambio + AplicacionSettings.Default.IdMonedaDolarApiTipoCambio;
                var response = Task.Run(() => GetURI(new Uri(url)));
                response.Wait();
                string result = response.Result;
                tipoCambio = Newtonsoft.Json.JsonConvert.DeserializeObject<TipoCambio>(result);
                tipoCambio.IdMoneda = MaestroSettings.Default.IdDetalleMaestroMonedaDolares;
                if (tipoCambio.Estado == false)
                {
                    tipoCambio = new TipoCambio();
                }
            }
            catch (Exception)
            {
                tipoCambio = new TipoCambio();
            }
            return tipoCambio;
        }

        protected static async Task<string> GetURI(Uri u)
        {
            var response = string.Empty;
            using (var client = new HttpClient())
            {
                HttpResponseMessage result = await client.GetAsync(u);
                if (result.IsSuccessStatusCode)
                {
                    response = await result.Content.ReadAsStringAsync();
                }
            }
            return response;
        }

    }
}
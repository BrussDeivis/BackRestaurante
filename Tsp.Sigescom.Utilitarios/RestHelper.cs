using Newtonsoft.Json;
using RestSharp;
using System.Net;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Utilitarios.RestHelper
{
    public static class RestHelper<TRequest, TResponse>
      where TRequest : class
      where TResponse : class, new()
    {
        public static TResponse Execute(string metodo, TRequest request, string clientUrl)
        {
            var client = new RestClient(clientUrl);

            var restRequest = new RestRequest(metodo, Method.POST)
            {
                RequestFormat = DataFormat.Json
            };

            restRequest.AddBody(request);

            var restResponse = client.Execute<TResponse>(restRequest);
            return restResponse.Data;
        }
        public static TResponse GetTokenSeguridad(string numeroDocumento)
        {
            string urlRequest = $"{FacturacionElectronicaSettings.Default.UrlApiSeguridadSunat}{FacturacionElectronicaSettings.Default.PreMetodoApiSeguridadSunat}{FacturacionElectronicaSettings.Default.IdClienteTokenSeguridadSunat}{FacturacionElectronicaSettings.Default.PosMetodoApiSeguridadSunat}";
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient(urlRequest);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", FacturacionElectronicaSettings.Default.GrantTypeTokenSeguridadSunat);
            request.AddParameter("scope", FacturacionElectronicaSettings.Default.UrlApiCpeSunat);
            request.AddParameter("client_id", FacturacionElectronicaSettings.Default.IdClienteTokenSeguridadSunat);
            request.AddParameter("client_secret", FacturacionElectronicaSettings.Default.ClaveClienteTokenSeguridadSunat);
            request.AddParameter("username", numeroDocumento + FacturacionElectronicaSettings.Default.UsuarioSol);
            request.AddParameter("password", FacturacionElectronicaSettings.Default.ClaveSol);
            var response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return new TResponse();
            }
            return JsonConvert.DeserializeObject<TResponse>(response.Content);
        }

        public static TResponse Execute(string clientUrl, string authorization, string request)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient(clientUrl);
            var restRequest = new RestRequest(Method.POST);
            restRequest.AddHeader("Authorization", authorization);
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddParameter("application/json", request, ParameterType.RequestBody);

            var restResponse = client.Execute<TResponse>(restRequest);
            return restResponse.Data;
        }

        public static TResponse ExecuteGet(string clientUrl, string authorization)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient(clientUrl);
            var restRequest = new RestRequest(Method.GET);
            restRequest.AddHeader("Authorization", authorization);

            var restResponse = client.Execute<TResponse>(restRequest);
            return restResponse.Data;
        }
    }
}

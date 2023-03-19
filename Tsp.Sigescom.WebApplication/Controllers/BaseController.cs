using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.TipoCambio;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Entidades.Sesion;
using Tsp.Sigescom.WebApplication.Models;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {

        }

        protected UserProfileSessionData ProfileData()
        {
            var profileData = this.Session["UserProfile"] as UserProfileSessionData;
            return profileData;
        }

        /// <summary>
        /// Convierte una cadena con formato "id|nombre" en una lista de objetos del tipo ItemGenericoBase
        /// </summary>
        /// <param name="almacenesString"></param>
        /// <returns></returns>
        protected List<ItemGenericoBase> ObtenerItems(string[] almacenesString)
        {
            return almacenesString.Select(a => new ItemGenericoBase() { Id = int.Parse(a.Split('|')[0]), Nombre = a.Split('|')[1] }).ToList();
        }

        public ParametrosBasicosReporte ObtenerParametrosBasicos()
        {
            var sede = ObtenerSede();
            DateTime fechaActual = DateTimeUtil.FechaActual();
            string logoString = Convert.ToBase64String(sede.Logo, 0, sede.Logo.Length);
            return new ParametrosBasicosReporte
            {
                NombreSede = new ReportParameter("NombreSede", sede.Nombre),
                LogoSede = new ReportParameter("LogoSede", logoString),
                FechaActualSistema = new ReportParameter("FechaActual", fechaActual.ToString()),
                Usuario = new ReportParameter("Usuario", ProfileData().Empleado.NombresYApellidos)
            };
        }
        public EstablecimientoComercialExtendidoConLogo ObtenerSede()
        {
            return (EstablecimientoComercialExtendidoConLogo)System.Web.HttpContext.Current.Application["Sede"];
        }

        public static string FormatearParametroDiasAntesDisponiblesParaReporte(int diasAntes)
        {
            return ("-" + diasAntes.ToString() + "d");
        }

        public static void ResetParameters()
        {

        }

        public TipoCambio ObtenerTipoCambioDolarActual()
        {
            TipoCambio tipoCambio = new TipoCambio();
            try
            {
                string url = AplicacionSettings.Default.UrlApiConsultaTipoCambio + AplicacionSettings.Default.IdMonedaDolarApiTipoCambio;
                var t = Task.Run(() => GetURI(new Uri(url)));
                t.Wait();
                string result = t.Result;
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

        public static string Encriptar(string _cadenaAencriptar)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.ASCII.GetBytes(_cadenaAencriptar);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        public static string DesEncriptar(string _cadenaAdesencriptar)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(_cadenaAdesencriptar);
            result = System.Text.Encoding.ASCII.GetString(decryted);
            return result;
        }
    }
}
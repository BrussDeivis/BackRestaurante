using System.Text;
using System.Web.Mvc;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.ClasesNegocio.SigesRestaurant.Comprobantes;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.ComprobantesModel;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public static class RestaurantHtmlStringBuilder
    {

      
        public static string ObtenerHtmlString(ComprobanteOrden comprobanteOrden, FormatoImpresion formato, EstablecimientoComercialExtendido sede, Controller controller)
        {
            StringBuilder sbControlHtml = new StringBuilder();
            string htmlString = HtmlStringBuilder.RenderRazorViewToString("../Restaurante/Comprobantes/ComprobanteOrden80mm", comprobanteOrden, controller);
            return htmlString;
        }

        public static string ObtenerHtmlString(ComprobanteCuentaAtencion comprobanteAtencion, FormatoImpresion formato, EstablecimientoComercialExtendido sede, Controller controller)
        {
            StringBuilder sbControlHtml = new StringBuilder();
            string htmlString = HtmlStringBuilder.RenderRazorViewToString("../Restaurante/Comprobantes/ComprobanteCuentaAtencion80mm", comprobanteAtencion, controller);
            return htmlString;
        }
    }

        
    
}
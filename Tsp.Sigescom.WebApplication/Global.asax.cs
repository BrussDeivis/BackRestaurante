using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Tsp.Sigescom.WebApplication.App_Start;

namespace Tsp.Sigescom.WebApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AntiForgeryConfig.SuppressIdentityHeuristicChecks = true;
            ValueProviderFactories.Factories.Add(new JsonValueProviderFactory());
            //Todo:Cuando se desee parametrizar tener en cuenta las siguientes lineas de codigo  
            //CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("es-PE");
            //CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("es-PE");
        }

        void Session_Start(object sender, EventArgs e)
        {
            HttpContext.Current.Session.Add("__MyAppSession", string.Empty);
            //Session["init"] = 0;
        }


    }
}

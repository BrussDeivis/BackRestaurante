using System.Web;
using System.Web.Optimization;

namespace Tsp.Sigescom.WebApplication
{
    public class BundleConfig
    {
        // Para obtener más información sobre Bundles, visite http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                        "~/Scripts/genericos/jquery/jquery-3.1.1.min.js",
                        //"~/Scripts/genericos/jquery/jquery-1.12.2.min.js",
                        "~/Scripts/jquery/jquery-ui.min.js",
                        "~/Scripts/genericos/moment/moment.min.js",
                        "~/Scripts/genericos/bootstrap/bootstrap.min.js",
                        "~/Scripts/genericos/bootstrap/bootstrap-datepicker.js",
                        "~/Scripts/genericos/bootstrap/bootstrap-datetimepicker.min.js",
                        "~/Scripts/bootstrap-notify.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/select2.min.js",
                        "~/Scripts/datatable/jquery.dataTables.min.js",
                        "~/Scripts/datatable/datetime-moment.js",
                        "~/Scripts/datatable/dataTables.colReorder.min.js",
                        "~/Scripts/datatable/dataTables.lightColumnFilter.min.js",
                        "~/Scripts/datatable/angular-datatables.min.js",
                        "~/Scripts/datatable/angular-datatables.colreorder.js",
                        "~/Scripts/datatable/dataTables.buttons.min.js",
                        "~/Scripts/datatable/buttons.colVis.min.js",
                        "~/Scripts/datatable/buttons.flash.js",
                        "~/Scripts/datatable/jszip.min.js",
                        "~/Scripts/datatable/buttons.html5.min.js",
                        "~/Scripts/datatable/buttons.print.js",
                        "~/Scripts/datatable/plugins/buttons/angular-datatables.buttons.min.js",
                        //"~/Scripts/datatable/plugins/columnfilter/angular-datatables.columnfilter.min.js",
                        //"~/Scripts/datatable/plugins/light-columnfilter/angular-datatables.light-columnfilter.min.js",
                        "~/Scripts/angular/angular.js",
                        "~/ Scripts/angular/angular-animate.min.js",
                        "~/Scripts/angular/angular-sanitize.min.js",
                        "~/Scripts/angular-block-ui.min.js",
                        "~/Scripts/angular/angular-resource.min.js",
                        "~/Scripts/respond.js", 
                        "~/Scripts/modernizr-*",
                        "~/Scripts/app.js",
                        "~/Scripts/service.js"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de compilación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/bundles/styles").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/bootstrap.css",
                        "~/Content/bootstrap-notify.css",
                        "~/Content/select2.css",
                        "~/Content/select2.png",
                        "~/Content/default.css",
                        "~/Scripts/datatable/css/angular-datatables.css",
                        "~/Scripts/datatable/css/colReorder.dataTables.min.css",
                        "~/Scripts/datatable/css/jquery.dataTables.min.css",
                        "~/Scripts/jquery/jquery-ui.min.css",
                        "~/Scripts/jquery/jquery-ui.css",
                        "~/Content/Site.css",

                        "~/Scripts/jquery/jquery-ui.structure.min.css",
                        "~/Scripts/jquery/jquery-ui.theme.min.css"
                        ));

            bundles.Add(new StyleBundle("~/bundles/styles/default").Include(
                        "~/Content/default.css"
                ));

            BundleTable.EnableOptimizations = true;

        }
    }
}

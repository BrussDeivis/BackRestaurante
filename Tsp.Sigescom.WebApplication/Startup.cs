
using DemoSendEmailHangfire.Models;
using Hangfire;
using Hangfire.Server;
using Hangfire.Storage;
using Microsoft.Owin;
using Owin;
using System;
using System.Globalization;
using System.Threading;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades.Sesion;
using Tsp.Sigescom.WebApplication.Controllers;

[assembly: OwinStartupAttribute(typeof(Tsp.Sigescom.WebApplication.Startup))]
namespace Tsp.Sigescom.WebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            Flags.FacturacionEnProceso = false;
            //System.Web.HttpContext.Current.Application["EFacturaTransmisionEnvioEnProceso"] = false;
            //Envio de documentos de facturacion electronica sunat
            if (FacturacionElectronicaSettings.Default.UtilizarHangfireParaFacturacionElectronica)
            {
                FacturacionElectronicaController facturacionElectronicaController = new FacturacionElectronicaController();
                GlobalConfiguration.Configuration.UseSqlServerStorage("SigescomEntities_");
                TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
                //We can authorized this Hangfire Dashboard from protected user 
                app.UseHangfireDashboard("/hangfire", new DashboardOptions()
                {
                    Authorization = new[] { new HangfireAuthorizationFilter() }
                });
                RecurringJob.AddOrUpdate(() => facturacionElectronicaController.TransmitirEnviarConsultarYReenviarComprobantes(), FacturacionElectronicaSettings.Default.CronExpressionTransmitirEnviarConsultarYReenviarComprobantes, timeZone);
                app.UseHangfireServer();
            }
            else
            {
                GlobalConfiguration.Configuration.UseSqlServerStorage("SigescomEntities_");
                using (var connection = JobStorage.Current.GetConnection())
                {
                    foreach (var recurringJob in connection.GetRecurringJobs())
                    {
                        if (recurringJob.Id.Equals("FacturacionElectronicaController.TransmitirEnviarConsultarYReenviarComprobantes"))
                            RecurringJob.RemoveIfExists(recurringJob.Id);
                    }
                }
            }
            //Envio automatico de reportes
            if (AplicacionSettings.Default.PermitirEnvioAutomaticoDeReportes)
            {
                ReporteController reporteController = new ReporteController();
                GlobalConfiguration.Configuration.UseSqlServerStorage("SigescomEntities_");
                TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
                //We can authorized this Hangfire Dashboard from protected user 
                app.UseHangfireDashboard("/hangfire", new DashboardOptions()
                {
                    Authorization = new[] { new HangfireAuthorizationFilter() }
                });
                RecurringJob.AddOrUpdate(() => reporteController.EnviarCorreoElectronicoConReportesAutomaticamente(), AplicacionSettings.Default.CronExpressionEnvioAutomaticoDeReportes, timeZone);
                app.UseHangfireServer();
            }
            else
            {
                GlobalConfiguration.Configuration.UseSqlServerStorage("SigescomEntities_");
                using (var connection = JobStorage.Current.GetConnection())
                {
                    foreach (var recurringJob in connection.GetRecurringJobs())
                    { 
                        if (recurringJob.Id.Equals("ReporteController.EnviarCorreoElectronicoConReportesAutomaticamente"))
                            RecurringJob.RemoveIfExists(recurringJob.Id);
                    }
                }
            }
            //Generacion automatica de inventarios logicos
            if (AplicacionSettings.Default.PermitirGenerarInventarioLogicoAutomaticamente)
            {
                AlmacenController almacenController = new AlmacenController();
                GlobalConfiguration.Configuration.UseSqlServerStorage("SigescomEntities_");
                TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
                //We can authorized this Hangfire Dashboard from protected user 
                app.UseHangfireDashboard("/hangfire", new DashboardOptions()
                {
                    Authorization = new[] { new HangfireAuthorizationFilter() }
                });
                RecurringJob.AddOrUpdate(() => almacenController.GenerarInventarioHistoricoAutomaticamente(), AplicacionSettings.Default.CronExpressionGenerarInventarioLogicoAutomaticamente, timeZone);
                app.UseHangfireServer();
            }
            else
            {
                GlobalConfiguration.Configuration.UseSqlServerStorage("SigescomEntities_");
                using (var connection = JobStorage.Current.GetConnection())
                {
                    foreach (var recurringJob in connection.GetRecurringJobs())
                    {
                        if (recurringJob.Id.Equals("AlmacenController.GenerarInventarioHistoricoAutomaticamente"))
                            RecurringJob.RemoveIfExists(recurringJob.Id);
                    }
                }
            }
            //Generacion automatica de arqueos de caja 
            if (AplicacionSettings.Default.PermitirGenerarArqueoCajaAutomaticamente)
            {
                FinanzaController finanzaController = new FinanzaController();
                GlobalConfiguration.Configuration.UseSqlServerStorage("SigescomEntities_");
                TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
                //We can authorized this Hangfire Dashboard from protected user 
                app.UseHangfireDashboard("/hangfire", new DashboardOptions()
                {
                    Authorization = new[] { new HangfireAuthorizationFilter() }
                });
                RecurringJob.AddOrUpdate(() => finanzaController.GenerarArqueoCajaAutomaticamente(), AplicacionSettings.Default.CronExpressionGenerarArqueoCajaAutomaticamente, timeZone);
                app.UseHangfireServer();
            }
            else
            {
                GlobalConfiguration.Configuration.UseSqlServerStorage("SigescomEntities_");
                using (var connection = JobStorage.Current.GetConnection())
                {
                    foreach (var recurringJob in connection.GetRecurringJobs())
                    {
                        if (recurringJob.Id.Equals("ReporteController.GenerarArqueoCajaAutomaticamente"))
                            RecurringJob.RemoveIfExists(recurringJob.Id);
                    }
                }
            }
        }
    }
    public partial class Flags
    {
        public static bool FacturacionEnProceso;
    }
}

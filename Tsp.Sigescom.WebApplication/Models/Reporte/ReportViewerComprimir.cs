using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tsp.Sigescom.WebApplication.Models
{
    public class ReportViewerComprimir
    {
        public ReportViewer ReportViewer{ get; set; }
        public string Nombre { get; set; }

        public ReportViewerComprimir(ReportViewer reportViewer, string nombre)
        {
            ReportViewer = reportViewer;
            Nombre = nombre;
        }
    }
}
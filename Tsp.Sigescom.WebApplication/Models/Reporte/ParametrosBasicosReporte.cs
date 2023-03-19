using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    public class ParametrosBasicosReporte
    {
        private ReportParameter nombreEmpresa;
        private ReportParameter logoSede;
        private ReportParameter fechaActualSistema;
        private ReportParameter usuario;
        public ParametrosBasicosReporte()
        { }

        public ReportParameter NombreSede { get => nombreEmpresa; set => nombreEmpresa = value; }
        public ReportParameter LogoSede { get => logoSede; set => logoSede = value; }
        public ReportParameter FechaActualSistema { get => fechaActualSistema; set => fechaActualSistema = value; }
        public ReportParameter Usuario { get => usuario; set => usuario = value; }
    }
}
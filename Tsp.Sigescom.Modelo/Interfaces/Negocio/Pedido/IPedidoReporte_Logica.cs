using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Pedido.ReportData;
using Tsp.Sigescom.Modelo.Entidades.Sesion;

namespace Tsp.Sigescom.Modelo.Interfaces.Negocio.Pedido
{
    public interface IPedidoReporte_Logica
    {
        PrincipalReportData ObtenerDatosParaReportePrincipal(UserProfileSessionData profileData);
    }
}

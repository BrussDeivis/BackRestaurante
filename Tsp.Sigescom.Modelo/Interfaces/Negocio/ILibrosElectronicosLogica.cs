using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.ClasesNegocio.EBookViewModel.Custom;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.Interfaces.Logica
{
    public interface ILibrosElectronicosLogica
    {
        List<Periodo> ObtenerPeriodos();

        Periodo ObtenerPeriodo(int idPeriodo);

        Periodo ObtenerPeriodo(string nombrePeriodo);
        List<ReporteVentaCliente> ObtenerRegistrosDeVenta(Periodo periodo, int idEmpleado);
        List<DetalleLibroVentasIngresos> ObtenerLibroElectronicoDeVentasEIngresos(Periodo periodo, int idEmpleado);
    }

}

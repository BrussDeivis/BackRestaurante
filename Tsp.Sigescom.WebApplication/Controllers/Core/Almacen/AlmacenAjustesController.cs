using System.Web.Mvc;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.Negocio.Almacen;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public partial class AlmacenAjustesController : BaseController
    {
        protected readonly IAjusteInventario_Logica _ajusteInventario;



        public AlmacenAjustesController():base()
        {
            _ajusteInventario = Dependencia.Resolve<IAjusteInventario_Logica>();
        }

        [Authorize(Roles = "AdministradorTI")]
        public void CuadrarStock()
        {
                _ajusteInventario.CuadrarStockFisicoEntreInventarioActualYMovimientos();
        }

        [Authorize(Roles = "AdministradorTI")]
        public void CorregirCostosUnitarios()
        {
            _ajusteInventario.RecalcularCostoUnitarioYTotalEnMovimientos();
        }



    }
}
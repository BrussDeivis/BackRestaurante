using System.Web.Mvc;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Almacen;
using Tsp.Sigescom.Modelo.Interfaces.Logica.Tesoreria;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public partial class TesoreriaAjustesController : BaseController
    {
        protected readonly IAjusteTesoreria_Logica _ajusteTesoreria;



        public TesoreriaAjustesController():base()
        {
            _ajusteTesoreria = Dependencia.Resolve<IAjusteTesoreria_Logica>();
        }

        
        [Authorize(Roles = "AdministradorTI")]
        public void CorregirTiposTransaccion()
        {
            _ajusteTesoreria.CorregirTipoTransaccionPagoEnNotasDeCreditoYDebito();
        }

    }
}
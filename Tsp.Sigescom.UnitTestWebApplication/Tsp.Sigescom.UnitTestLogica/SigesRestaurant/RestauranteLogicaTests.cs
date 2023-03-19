using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tsp.Sigescom.Parking.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.Negocio.Restaurant;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Negocio.Establecimientos;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Establecimientos;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Almacen;

namespace Tsp.Sigescom.Parking.Logica.Tests
{
    [TestClass()]
    public class RestauranteLogicaTests
    {
        private readonly IRestauranteLogica restauranteLogica;
        private readonly IActorNegocioLogica actorLogica;
        private readonly IMaestroLogica maestroLogica;
        private readonly IOperacionLogica operacionLogica;
        private readonly IConceptoLogica conceptoLogica;
        private readonly IEmpleado_Logica empleadoLogica;
        private readonly ICentroDeAtencion_Logica centroDeAtencionLogica;
        private readonly ISede_Logica sedeLogica;
        private readonly IEstablecimiento_Repositorio establecimientoDatos;
        private readonly IInventarioActualRepositorio inventarioActualDatos;





        public RestauranteLogicaTests()
        {
            restauranteLogica = Dependencia.Resolve<IRestauranteLogica>();
            actorLogica = Dependencia.Resolve<IActorNegocioLogica>();
            maestroLogica = Dependencia.Resolve<IMaestroLogica>();
            operacionLogica = Dependencia.Resolve<IOperacionLogica>();
            conceptoLogica = Dependencia.Resolve<IConceptoLogica>();
            empleadoLogica = Dependencia.Resolve<IEmpleado_Logica>();
            centroDeAtencionLogica = Dependencia.Resolve<ICentroDeAtencion_Logica>();
            sedeLogica = Dependencia.Resolve<ISede_Logica>();
            establecimientoDatos = Dependencia.Resolve<IEstablecimiento_Repositorio>();
            inventarioActualDatos = Dependencia.Resolve<IInventarioActualRepositorio>();



        }
        [TestMethod()]
        public void ObtenerCategoriasRestauranteTest()
        {
            var categorias = restauranteLogica.ObtenerCategoriasRestaurante();

        }
    }
}
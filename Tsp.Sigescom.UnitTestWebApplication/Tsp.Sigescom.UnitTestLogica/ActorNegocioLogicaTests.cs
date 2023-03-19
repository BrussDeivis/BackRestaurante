using Tsp.Sigescom.Logica;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Negocio.Establecimientos;

namespace Tsp.Sigescom.Logica.Tests
{
    [TestClass()]
    public class ActorNegocioLogicaTests
    {

        private readonly IActorNegocioLogica actorNegocioLogica;

        private readonly IEstablecimiento_Logica establecimientoLogica;
        private readonly ICentroDeAtencion_Logica centrosDeAtencionLogica;



        public ActorNegocioLogicaTests()
        {

            actorNegocioLogica = Dependencia.Resolve<IActorNegocioLogica>();
            establecimientoLogica = Dependencia.Resolve<IEstablecimiento_Logica>();
            centrosDeAtencionLogica = Dependencia.Resolve<ICentroDeAtencion_Logica>();


        }
        [TestMethod()]
        public void crearClienteTest()
        {

            ////Declacion de variables a guardar
            //int idTipoPersona = 2;
            //string razonSocial = "Universidad Nacional Agraria de la selva";
            //string apellidoMaterno = "";
            //string apellidoPaterno = "";
            //string nombres = "";
            //string nombreComercial = "UNAS";
            //string nombreCorto = "UNAS";
            //int idTipoDocumentoIdentidad = 71;
            //string numeroDocumentoIdentidad = "28882356720";
            //string correo = "unas@gmail.com";
            //string telefono = "956741235";
            //int idClaseActor = 3;
            //int idEstadoLegalActor = 2;
            //Direccion midireccion = new Direccion(12, 46, 150112, "Av. Universitaria 945", null, null, true, true);
            //List<Direccion> direcciones = new List<Direccion>();
            //direcciones.Add(midireccion);

            ////Ejecucion del metodo
            //OperationResult resultado = logica.crearCliente(idTipoPersona, razonSocial, apellidoPaterno, apellidoMaterno, 
            //    nombres, nombreComercial, nombreCorto, idTipoDocumentoIdentidad, numeroDocumentoIdentidad, idClaseActor, 
            //    idEstadoLegalActor, correo, telefono, direcciones);

            ////Comprobacion del assert
            //Assert.AreEqual(OperationResultEnum.Success, resultado.code_result);

        }

        [TestMethod()]
        public void ObtenerEstablecimientosComercialesVigentesTest()
        {
            var resultado = establecimientoLogica.ObtenerEstablecimientosComercialesVigentes();
            Assert.Fail();
        }

        [TestMethod()]
        public void ObtenerPuntosDeVentaVigentesTest()
        {
            var resultado = centrosDeAtencionLogica.ObtenerPuntosDeVentaVigentes();
            Assert.Fail();
        }

        [TestMethod()]
        public void ObtenerAlmacenesVigentesTest()
        {
            var resultado = centrosDeAtencionLogica.ObtenerAlmacenesVigentes();
            Assert.Fail();
        }

        [TestMethod()]
        public void ObtenerPersonasTest()
        {

            Assert.Fail();
        }

        [TestMethod()]
        public void ObtenerActorComercialPorIdTest()
        {
            var actor = actorNegocioLogica.ObtenerActorComercialPorId(6, 13);
        }
    }
}
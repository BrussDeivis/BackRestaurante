using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Interfaces.Logica;

namespace Tsp.Sigescom.Logica.Tests
{
    [TestClass()]
    public class LibrosElectronicosLogicaTests
    {

        private readonly ILibrosElectronicosLogica _librosElectronicosLogica;
        private readonly IActorNegocioLogica _actorNegocioLogica;
        public LibrosElectronicosLogicaTests()
        {
            _librosElectronicosLogica = Dependencia.Resolve<ILibrosElectronicosLogica>();
            _actorNegocioLogica = Dependencia.Resolve<IActorNegocioLogica>();
        }

        [TestMethod()]
        public void LibrosElectronicosLogicaTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EliminarLibrosElectronicosRegimenEspecialTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GenerarLibrosElectronicosRegimenEspecial_Test()
        {
            Assert.Fail();
        }


        //Tenemos que cambiar de ambiente para poder hacer pruebas unitarias
        //Nombre del ambiente de PRUEBAS:  "CLAVELINA" --> Ambiente no valido
        [TestMethod()]
        public void GenerarLibrosElectronicosRegimenEspecialTest()
        {
            int idPeriodo = 60;
            int idLibroVentas = 10;
            
            var libroVentas = _librosElectronicosLogica.ObtenerLibroElectronicoDeVentasEIngresos(new Periodo(), 1);
            Assert.IsTrue(libroVentas != null && libroVentas.Count > 0);
        }

        [TestMethod()]
        public void ObtenerLibrosElectronicos__Test()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ObtenerLibrosElectronicosTest()
        {
            int idPeriodo = 60;//agosto 2018

            //var libros = _librosElectronicosLogica..ObtenerLibrosElectronicos(1, idPeriodo);
            //Assert.IsTrue(libros != null);
        }

        [TestMethod()]
        public void ObtenerPeriodosTest()
        {
            var bdPeriodos = _librosElectronicosLogica.ObtenerPeriodos();
            Assert.IsNotNull(bdPeriodos);//el objeto no contiene NULL

            Assert.IsTrue(bdPeriodos.Count == 61);//La cantidad de Periodos que existe
        }
    }
}
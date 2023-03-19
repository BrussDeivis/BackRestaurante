using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tsp.Sigescom.AccesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Hotel;
using Tsp.Sigescom.Inyeccion;

namespace Tsp.Sigescom.AccesoDatos.Tests
{
    [TestClass()]
    public class HotelReporte_DatosTests
    {
        private readonly IHotelReporte_Repositorio _repositorio;

        public HotelReporte_DatosTests()
        {
            _repositorio = Dependencia.Resolve<IHotelReporte_Repositorio>();
        }
        [TestMethod()]
 
        public void ObtenerIncidentesTest()
        {
 
            var todo = _repositorio.ObtenerIncidentes(2, new DateTime(2022, 07, 11), new DateTime(2022, 07, 11, 23,59,59));
            Assert.Fail();
        }
    }
}
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.UnitTestDatos
{
    [TestClass]
    public class TransaccionDatosUnitTest
    {
        private readonly ITransaccionRepositorio _repositorio;

        public TransaccionDatosUnitTest()
        {
            _repositorio = Dependencia.Resolve<ITransaccionRepositorio>();
        }

        /// <summary>
        /// Metodo para guardar Ventas
        /// </summary>
        //[TestMethod]
        //public void GuardarVentasTestMethod()
        //{

        //    int idTipoTransaccion = 3, idUnidadNegocio = 17;
        //    string codigo = "v_0001";
        //    DateTime fecha = DateTimeUtil.FechaActual();

        //    Transaccion ventas = new Transaccion(codigo,null,fecha,idTipoTransaccion,idUnidadNegocio,true,fecha,fecha);

             
        //}
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;
using Tsp.Sigescom.Modelo.SigesRestaurant;

namespace Tsp.Sigescom.Restaurante.UnitTestDatos
{
    [TestClass]
    public class RestauranteDatosUnitTest
    {
        private readonly IRestauranteRepositorio _repositorio;

        public RestauranteDatosUnitTest()
        {
            _repositorio = Dependencia.Resolve<IRestauranteRepositorio>();
        }

        [TestMethod]
        public async void ObtenerItemsDeRestaurante()
        {
            List<ItemRestaurante> items = (await _repositorio.ObtenerItemsDeRestaurante()).ToList();
            foreach (var item in items)
            {
                Console.Write(item.Nombre);
            }
            Assert.IsNotNull(items);
        }

        [TestMethod]
        public async void ObtenerAmbientesDeRestaurante()
        {
            List<ItemRestaurante> items = (await _repositorio.ObtenerItemsDeRestaurante()).ToList();
            foreach (var item in items)
            {
                Console.Write(item.Nombre);
            }
            Assert.IsNotNull(items);
        }

        [TestMethod]
        public void ObtenerComplementosPorFamilia()
        {
            var id = 15080;
            List<Complemento> complementos = _repositorio.ObtenerComplementosPorFamilia(id).ToList();
            Assert.IsNotNull(complementos);
        }

        //[TestMethod]
        //public void ObtenerFamiliasIncluidoComplementos()
        //{
        //    List<Familia> familias = _repositorio.ObtenerFamiliasIncluidoComplementos().ToList();
        //    Assert.IsNotNull(familias);
        //}

        [TestMethod]
        public void ObtenerOrdenesPorEstado()
        {
            int estado = MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado;
            List<Orden_Atencion> ordenes = _repositorio.ObtenerOrdenesPorEstado(estado).ToList();
            Assert.IsNotNull(ordenes);
        }

        [TestMethod]
        public void ObtenerOrdenesPorEstadoDeUnAmbiente()
        {
            int idAmbiente = 167185;
            int estado = MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado;

            List<Orden_Atencion> ordenes = _repositorio.ObtenerOrdenesPorEstadoDeUnAmbiente(estado, idAmbiente).ToList();

            Assert.IsNotNull(ordenes);

        }
        [TestMethod]
        public void ObtenerAtencionesPorEstado()
        {
            int estado = (int)MaestroSettings.Default.IdDetalleMaestroEstadoRegistrado;
            List<AtencionRestaurante> atenciones = _repositorio.ObtenerAtencionesPorEstado(estado).ToList();

            Assert.IsNotNull(atenciones);
        }

        [TestMethod]
        public void ObtenerOrdenesDeAtencionIncluidoDetallesDeOrdenItemsDeRestauranteYDetallesDeComplemento()
        {
            long idAtencion = 167185;
            List<Orden_Atencion> ordenes = _repositorio.ObtenerOrdenesDeAtencionIncluidoDetallesDeOrdenItemsDeRestauranteYDetallesDeComplemento(idAtencion).ToList();
            Assert.IsNotNull(ordenes);
        }


        [TestMethod]
        public void ObtenerComplementosDetalladosPorFamilia()
        {
            int idFamilia = 15080;
            List<Complemento> complementos = _repositorio.ObtenerComplementosPorFamilia(idFamilia).ToList();
            Assert.IsNotNull(complementos);
        }

        [TestMethod]
        public void ObtenerAtencionEspecifica()
        {
            long idAtencion = 167185;
            AtencionRestaurante atencion = _repositorio.ObtenerAtencionEspecifica(idAtencion);
            Assert.IsNotNull(atencion);

        }

        //[TestMethod]
        //public void ObtenerTransaccionDeAtencionDeMesaExistente()
        //{
        //    var idMesa = 3180;
        //    Transaccion transaccion = _repositorio.ObtenerTransaccionDeAtencionDeMesa(idMesa);
        //    Assert.IsNotNull(transaccion); 
        //}

}

}

    


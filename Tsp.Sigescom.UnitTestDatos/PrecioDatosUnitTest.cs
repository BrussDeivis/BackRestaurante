using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo;

namespace Tsp.Sigescom.UnitTestDatos
{
    


    [TestClass]
    public class PrecioDatosUnitTest
    {
        //private object _precioRepositorio;
        private readonly IPrecioRepositorio _repositorio;

        public PrecioDatosUnitTest()
        {
            _repositorio = Dependencia.Resolve<IPrecioRepositorio>();
        }
        [TestMethod]
        public void guardarPrecioTestMethod()
        {
            Precio precio = new Precio();
            precio.id_actor_negocio = 3;//id mpleado
            precio.id_concepto_negocio =87;//id producto
            precio.id_unidad_negocio = 18;// 17 18
            precio.valor = 5;//
            precio.id_moneda = 584;
            precio.id_tarifa_d = 1;
            precio.fecha_inicio = DateTimeUtil.FechaActual();
            precio.fecha_fin = DateTimeUtil.FechaActual();
            precio.fecha_modificacion = DateTimeUtil.FechaActual();
            precio.indicador_multiproposito =true;
            precio.es_vigente = true;
            precio.porcentaje = false;
            precio.cantidad_maxima = null;
            precio.cantidad_minima = null;
            precio.id_tipo =null;
            precio.id_concepto_negocio_referencial =null;
            
            OperationResult result = _repositorio.CrearPrecio(precio);
            
            Assert.Equals(OperationResultEnum.Success, result.code_result);


        }
    }
}

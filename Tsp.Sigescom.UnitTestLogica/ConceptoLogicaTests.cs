using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Interfaces.Logica;

namespace Tsp.Sigescom.Logica.Tests
{
    [TestClass()]
    public class ConceptoLogicaTests
    {
        private readonly IConceptoLogica logica;


        public ConceptoLogicaTests()
        {
            logica = Dependencia.Resolve<IConceptoLogica>();

        }
        /*
        [TestMethod()]
        public void guardarProductoTest()
        {
            //Datos Generales
            string nombre = "CUARTO SIMPLE BAÑO_PROPIO CAMA_DOBLE_1.5_Y_2_PLAZAS TV_32_PULGADAS CON_VENTILADOR 5 |SIN PRESENTACION 1 UN | X UN";//nombre
            string codigo = "12356";//codigo de barra
            int idConceptoBasico = 438; //id del concepto basico
            string sufijo = "SIMPLE";
            int idUnidadDeMedidaCom = 5;// U.M.C
            int idUnidadDeMedidaRef = 5;//U.M.R

            //Presentacion
            int idPresentacion = 11;//idPresentacion
            decimal cantidadPresentacion = 1;//Cantidad
            int idUnidadDeMedidaPre = 5;//U.M
            int[] idCaracteristicas = new int[] { 39, 42, 44, 46, 51 };

            //Datos Complementarios
            decimal stockMinimo = 0;//stock minimo
            List<Precio> precios = new List<Precio>();
            //Precio al publico
            Precio precio1 = new Precio() { id_tarifa_d = 7, valor = 40 };
            precios.Add(precio1);
            //Precio Frecuente
            Precio precio2 = new Precio() { id_tarifa_d = 8, valor = 40 };
            precios.Add(precio2);
            //Precio por mayor
            Precio precio3 = new Precio() { id_tarifa_d = 9, valor = 40 };
            precios.Add(precio3);

            //Datos de sesion
            int idEmpleado = 9;// empleado logueado
            int idCentroDeAtencion = 1; //centro de atencion seleccionado al iniciar la sesion

            OperationResult resultado = new OperationResult();
            resultado = logica.guardarProducto(codigo, nombre, sufijo, idConceptoBasico, idUnidadDeMedidaCom, idUnidadDeMedidaRef, idCaracteristicas,
                                                idPresentacion, cantidadPresentacion, idUnidadDeMedidaPre, null, null, false, precios, stockMinimo,
                                                idEmpleado, idCentroDeAtencion);

            Assert.AreEqual(OperationResultEnum.Success, resultado.code_result);

        }
        */

        [TestMethod()]
        public void guardarProductoTest()
        {
            //Datos Generales
            string nombre = "PECHUGA PEC-PRUEBA-2 |SIN PRESENTACION 1 UN | X UN";//nombre
            string codigo = "12356";//codigo de barra
            int idConceptoBasico = 439; //id del concepto basico
            string sufijo = "PEC-PRUEBA-2";
            int idUnidadDeMedidaCom = 5;// U.M.C
            int idUnidadDeMedidaRef = 5;//U.M.R

            //Presentacion
            int idPresentacion = 11;//idPresentacion
            decimal cantidadPresentacion = 1;//Cantidad
            int idUnidadDeMedidaPre = 1;//U.M

            //Datos Complementarios
            decimal stockMinimo = 100;//stock minimo
            //List<Precio> precios = new List<Precio>();
            ////Precio al publico
            //Precio precio1 = new Precio() { id_tarifa_d = 7, valor = 10 };
            //precios.Add(precio1);
            ////Precio Frecuente
            //Precio precio2 = new Precio() { id_tarifa_d = 8, valor = 9 };
            //precios.Add(precio2);
            ////Precio por mayor
            //Precio precio3 = new Precio() { id_tarifa_d = 9, valor = 8 };
            //precios.Add(precio3);

            ////Datos de sesion
            //int idEmpleado = 7;// empleado logueado
            //int idCentroDeAtencion = 1; //centro de atencion seleccionado al iniciar la sesion

            //OperationResult resultado = new OperationResult();
            //resultado = logica.guardarProducto(codigo, nombre, sufijo, idConceptoBasico, idUnidadDeMedidaCom, idUnidadDeMedidaRef, null, idPresentacion, cantidadPresentacion, idUnidadDeMedidaPre, null, null,
            //false, precios, stockMinimo, idEmpleado, idCentroDeAtencion);
            //Assert.AreEqual(OperationResultEnum.Success, resultado.code_result);

        }

        [TestMethod()]
        public void obtenerCaracteristicasTest()
        {
            List<Detalle_maestro> resultados = logica.ObtenerCaracteristicas();
            Assert.Fail();
        }

        [TestMethod()]
        public void ObtenerConceptosIncluyendoCategoriaConceptoTest()
        {
            var va = logica.ObtenerConceptosBasicosVigentesIncluyendoCategoriaConcepto();
            Assert.Fail();
        }   

        [TestMethod()]
        public void ObtenerComplementoConceptoDeNegocioComercialParaVentaTest()
        {
            //var resultados = logica.ObtenerComplementoConceptoDeNegocioComercialParaVenta(3, 1154);
            Assert.Fail();
        }

        [TestMethod()]
        public void ObtenerConceptosDeNegocioComercialesPorIdsValoresCaracteristicasIncluyendoStockYPreciosTest()
        {
            int idAlmacen = 3;
            int[] idsValoresCaractetisticas = new int[] {1018 };
            var resultados = logica.ObtenerConceptosDeNegocioComercialesPorIdsValoresCaracteristicasIncluyendoStockYPrecios(idAlmacen,3, idsValoresCaractetisticas);
            Assert.Fail();
        }

        [TestMethod()]
        public void ObtenerConceptosDeNegociosComercialPorNombreTest()
        {
            /*
              Metodos para realizar la busqueda por coincidencias en el nombre del concepto de negocio

              1. Metodo StarWith
              "PRESERVATIVO","PRESERVATIV" => RETORNA 13 DATOS => COMIENZAN CON PRESERVATIVO
              "PRESERVATIV " => RETORNA 0 DATOS
              "SOBRE" => RETORNA 0 => LA PALBRA SOBRE ES INTERMEDIA

              2. Metodo Contains
               "PRESERVATIVO","PRESERVATIVO ", "PRESERVATIV" => RETORNA 13 DATOS => CONTIENE LA PALABRA PRESERVATIVO
               "PRESERVATIV" => RETORNA 0 DATOS => NO COINCIDE CON  LA PALABRA

             3. Metodo StarWith y Contains
               "PRESERVATIV " => RETORNA 0 DATOS
               "PRESERVATIV SOBRE " => RETORNA 0 DATOS

             Conclusion
             1. En el metodo 1 usando el metodo starwith busca conincidencias que comienzan con la palabra dada si hay palabra intermedias no funca.
             2. En el metodo 2 usando el metodo contains busca la palabra ingresada en la cadena como se ingreso la palabra "PRESERVATIV " no encuentra esa palabra en la cadena
             3. En el metodo 3 retornar 0 datos
             */
            string nombreConceptoNegocio = "PRESERVATIVO SOBRE";
            var resultado = logica.ObtenerConceptosDeNegociosComercialParaVentaPorNombre(3, nombreConceptoNegocio);
        }


    }
}
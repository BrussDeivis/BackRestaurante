using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tsp.FacturacionElectronica.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.FacturacionElectronica.AccesoDatos;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Config;

namespace Tsp.FacturacionElectronica.Logica.Tests
{
    [TestClass()]
    public class FacturacionElectronicaLogicaTests
    {
        FacturacionElectronicaLogica logica;


        public FacturacionElectronicaLogicaTests()
        {
            logica = new FacturacionElectronicaLogica();

        }

        [TestMethod()]
        public void CrearDocumentoTest()
        {
            //var result = logica.CrearDocumento(1, 1, 1, "03", "BOLETA DE VENTA", "B001", "15", DateTime.Now, Convert.FromBase64String("HOLA"));
            ////var result = logica.CrearEnvio("", "INDIVIDUAL", 1, "0", "El resumen RC-20180909-1, ha sido aceptado", "", Convert.FromBase64String("HOLA"), Convert.FromBase64String("MUNDO"));

            //Assert.Equals(result.code_result, OperationResultEnum.Success);
            Assert.Fail();
        }

        [TestMethod()]
        public void ActualizarDocumentoTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CrearEnvioDocumentoTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CrearBinarioTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CrearEnvioTest()
        {
            //var result = logica.CrearEnvio("00001","RESUMEN DIARIO",1,"0","El resumen RC-20180909-1, ha sido aceptado","2012445454454",Convert.FromBase64String("HOLA"),null);
            //var result = logica.CrearEnvio("", "INDIVIDUAL", 1, "0", "El resumen RC-20180909-1, ha sido aceptado", "", Convert.FromBase64String("HOLA"), Convert.FromBase64String("HOLA"));

            //Assert.Equals(result.code_result, OperationResultEnum.Success);
        }
        [TestMethod()]
        public void ActualizarEnvioTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ActualizarEnvioTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CrearDocumentosMasivosTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ObtenerDocumentosTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ObtenerEnviosTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ObtenerEnviosSinCodigoDeRespuestaTest()
        {
            //var result = logica.ObtenerEnviosSinCodigoDeRespuesta();

            //Assert.IsTrue(result.Count > 0);
            Assert.Fail();
        }

        [TestMethod()]
        public void ObtenerEnviosEntreFechasTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ObtenerDocumentosEntreFechasTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ObtenerEmisorTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ObtenerArchivoTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ObtenerDocumentoElectronicoTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void HayBoletasNoEnviadasTest()
        {
            var result = logica.HayBoletasNoEnviadas();

            Assert.Equals(result, true);
        }

        [TestMethod()]
        public void HayEnviosDeBoletasRechazadosOConExcepcionTest()
        {
            //var result = logica.HayEnviosDeResumenesDiarioRechazados();

            //Assert.Equals(result, true);
            Assert.Fail();
        }

        [TestMethod()]
        public void ObtenerIdDocumentosRechazadosOConExcepcionTest()
        {
            //int idEnvio = 1;

            //var result = logica.ObtenerIdDocumentosConExcepcion(idEnvio);

            //Assert.IsTrue(result.Count > 0);
            Assert.Fail();
        }

        [TestMethod()]
        public void ObtenerIdEnviosDeBoletasRechazadosOConExcepcionTest()
        {
            //var result = logica.ObtenerIdEnviosDeResumenDiarioConExcepcion();

            //Assert.IsTrue(result.Count > 0);
            Assert.Fail();
        }

        [TestMethod()]
        public void DevolverBoletasPorEnviarPorDiaTest()
        {
            //var result = logica.DevolverIdBoletasPorEnviarPorDia();

            //Assert.IsTrue(result.Count > 0);
            Assert.Fail();
        }

        [TestMethod()]
        public void DevolverFacturasPorEnviarTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DevolverFacturasInvilitadasPorEnviarTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DevolverNotasPorEnviarTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ObtenerIdentificadorResumenDiarioTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ObtenerImprimibleTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ObtenerImprimibleTest1()
        {
            Assert.Fail();
        }
    }
}
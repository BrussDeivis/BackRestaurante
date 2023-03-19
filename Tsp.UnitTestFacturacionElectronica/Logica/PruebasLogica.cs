using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tsp.FacturacionElectronica.Logica;
using Tsp.FacturacionElectronica.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Config;

namespace Tsp.FacturacionElectronica.Logica.Tests
{
    [TestClass()]
    public class PruebasLogica
    {
        

        [TestMethod()]
        public void ActualizarEnvioTest()
        {
          
        }

        [TestMethod()]
        public void ObtenerEnviosSinCodigoDeRespuestaTest()
        {
           
            
        }

        [TestMethod()]
        public void HayBoletasNoEnviadasTest()
        {
            
        }

        [TestMethod()]
        public void HayEnviosDeBoletasRechazadosOConExcepcionTest()
        {
            
        }

        [TestMethod()]
        public void ObtenerIdDocumentosRechazadosOConExcepcionTest()
        {
           
        }

        [TestMethod()]
        public void ObtenerIdEnviosDeBoletasRechazadosOConExcepcionTest()
        {

        }

        [TestMethod()]
        public void DevolverBoletasPorEnviarPorDiaTest()
        {
            string respuestaConsulta = "2108";
            int estado = (Convert.ToInt32(respuestaConsulta) == FacturacionElectronicaSettings.Default.CodigoRespuestaAceptado) ? (int)EstadoEnvio.Aceptado :
                                 (Convert.ToInt32(respuestaConsulta) <= FacturacionElectronicaSettings.Default.MaximoCodigoRespuestaConExcepcion && Convert.ToInt32(respuestaConsulta) >= FacturacionElectronicaSettings.Default.MinimoCodigoRespuestaConExcepcion) ? (int)EstadoEnvio.ConExcepcion :
                                 (Convert.ToInt32(respuestaConsulta) <= FacturacionElectronicaSettings.Default.MaximoCodigoRespuestaRechazado && Convert.ToInt32(respuestaConsulta) >= FacturacionElectronicaSettings.Default.MinimoCodigoRespuestaRechazado) ? (int)EstadoEnvio.Rechazado : (int)EstadoEnvio.AceptadoConObservaciones;
        }

        
    }
}

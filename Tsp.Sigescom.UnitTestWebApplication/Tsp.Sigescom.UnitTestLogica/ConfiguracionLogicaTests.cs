using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Negocio.Establecimientos;

namespace Tsp.Sigescom.Logica.Tests
{
    [TestClass()]
    public class ConfiguracionLogicaTests
    {
        private readonly IConfiguracionLogica logicaConfiguracion;
        private readonly IActorNegocioLogica logicaActor;
        private readonly ISucursal_Logica sucursalLogica;


        public ConfiguracionLogicaTests()
        {
            logicaConfiguracion = Dependencia.Resolve<IConfiguracionLogica>();
            logicaActor = Dependencia.Resolve<IActorNegocioLogica>();
            sucursalLogica = Dependencia.Resolve<ISucursal_Logica>();

        }

        [TestMethod()]
        public void CrearTipoDeTransaccionTest()
        {
            //Declacion de variables
            OperationResult resultado;
            string nombre = "PRUEBA COBRO A VARIOS";
            string descripcion = "COBRO A VARIOS";
            int idTransaccionMaestro = 152;//idTransaccionMaestro Cobro
            List<AccionDeNegocioPorTipoTransaccion> accionesDeNegocioPorTipoTransaccion = new List<AccionDeNegocioPorTipoTransaccion>();
            accionesDeNegocioPorTipoTransaccion.Add(new AccionDeNegocioPorTipoTransaccion(1, 0, true));//Movimiento en caja - Entrada
                                                                                                       //Ejecucion del metodo
            resultado = logicaConfiguracion.CrearTipoDeTransaccion(nombre, descripcion, idTransaccionMaestro, accionesDeNegocioPorTipoTransaccion);
            //Comprobacion del assert
            Assert.AreEqual(OperationResultEnum.Success, resultado.code_result);
        }

        [TestMethod()]
        public void CrearTipoDeComprobanteTest()
        {
            //Declacion de variables
            OperationResult resultado;
            string nombre = "PRUEBA BOLETA DE VENTA ELECTRONICA";
            string nombreCorto = "BV";
            string codigoSunat = "102";
            int[] idTransaccionEmisionPropia = new int[] {9};//Orden de venta
            int[] idTransaccionEmisionTerceros = new int[] { 4,32 };//Orden de compra, Orden de servicios de terceros 
            //Ejecucion del metodo
            resultado = logicaConfiguracion.CrearTipoDeComprobante(nombre,nombreCorto,codigoSunat,
                                                                    idTransaccionEmisionPropia, 
                                                                    idTransaccionEmisionTerceros);
            //Comprobacion del assert
            Assert.AreEqual(OperationResultEnum.Success, resultado.code_result);
        }

        [TestMethod()]
        public void CrearSerieDeComprobanteTest()
        {
            //Declacion de variables
            OperationResult resultado;
            int idCentroDeAtencion = 3;//Centro de atencion principal - AVICOLA KATTY - CA - PRINCIPAL
            int idTipoDeComprobante = 27;//Boleta de venta electronica
            string numeroDeSerie = "B001";
            int numeroDeComprobanteSiguiente = 1;
            bool autonumerica = true;
            bool vigente = true;
            //Ejecucion del metodo
            resultado = logicaConfiguracion.CrearSerieDeComprobante(idCentroDeAtencion, idTipoDeComprobante, 
                                                                    numeroDeSerie, numeroDeComprobanteSiguiente, 
                                                                    autonumerica, vigente);
           // Comprobacion del assert
            Assert.AreEqual(OperationResultEnum.Success, resultado.code_result);
        }

        [TestMethod()]
        public void CrearTurnoTest()
        {
            //Declacion de variables
            OperationResult resultado;
            int idCentroDeAtencion = 3;//AVICOLA KATTY - CA - PRINCIPAL
            int idEmpleado = 1076;//Miraval
            DateTime desde = new DateTime(2018, 12, 03, 00, 00, 00);
            DateTime hasta = new DateTime(2019, 12, 03, 00, 00, 00);
            //Ejecucion del metodo
            resultado = logicaActor.CrearTurno(idCentroDeAtencion, idEmpleado, desde, hasta);
            //Comprobacion del assert
            Assert.AreEqual(OperationResultEnum.Success, resultado.code_result);
        }

        [TestMethod()]
        public void CrearCentroDeAtencionTest()
        {
            //Declacion de variables
            OperationResult resultado;
            string nombre = "CA - MERCADO MODELO - TINGO MARIA";
            List<int> idRoles = new List<int> { 11, 12, 13};// PUNTO DE VENTA, CAJA, ALMACEN
            int idCentroDeAtencionPadre = 1 ;//La sede principal
            //Ejecucion del metodo
            resultado = null;//logicaActor.CrearCentroDeAtencion(nombre, idRoles, idCentroDeAtencionPadre);
            //Comprobacion del assert
            Assert.AreEqual(OperationResultEnum.Success, resultado.code_result);
        }

        [TestMethod()]
        public void CrearSucursalTest()
        {
            //Declacion de variables
            OperationResult resultado;
            string codigoEstablecimiento = "AK-LA MOLINA";
            string nombre = "SUCURSAL 1 - LA MOLINA";
            string nombreCorto = "S1 - LA MOLINA";
            string telefono = "";
            string correo = "";
            Direccion direccion = new Direccion(12, 46, 150114, "AV. LAGUNA GRANDE #945", null, null, true, true);//LIMA - LIMA - LA MOLINA
            List<Direccion> direcciones = new List<Direccion>();
            direcciones.Add(direccion);
            //Ejecucion del metodo
            resultado = sucursalLogica.CrearSucursal(codigoEstablecimiento,"", "",nombre, nombreCorto, 
                                                  telefono, correo, direccion, null);
            //Comprobacion del assert
            Assert.AreEqual(OperationResultEnum.Success, resultado.code_result);
        }
    }
}
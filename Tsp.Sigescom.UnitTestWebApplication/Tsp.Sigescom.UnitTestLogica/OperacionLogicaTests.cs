using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Custom;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Sesion;
using Tsp.Sigescom.Modelo.Interfaces.Logica;

namespace Tsp.Sigescom.Logica.Tests
{
    [TestClass()]
    public class OperacionLogicaTests
    {
        private const double V = 22.15;
        private const double V1 = 6.50;
        private readonly IOperacionLogica _operacion;
        private readonly IActorNegocioLogica _actorNegocioLogica;
        private readonly ICentroDeAtencion_Logica _centroDeAtencionLogica;

        private readonly IEmpleado_Logica _empleadoLogica;



        public OperacionLogicaTests()
        {
            _operacion = Dependencia.Resolve<IOperacionLogica>();
            _actorNegocioLogica= Dependencia.Resolve<IActorNegocioLogica>();
            _empleadoLogica = Dependencia.Resolve<IEmpleado_Logica>();
            _centroDeAtencionLogica = Dependencia.Resolve<ICentroDeAtencion_Logica>();



        }
        /*
        [TestMethod()]
        public void ConfirmarCompraNotaVenta()
        {
            bool esPropio = true;
            DateTime fechaRegistro = new DateTime(2018, 12, 04, 00, 00, 00);
            decimal flete = 0;
            int idCentroAtencion = 3;
            int idEmpleado = 9;
            int idProveedor = 1236;
            int idSerieComprobante = 11;
            int idTipoComprobante = 0;
            string numeroSerieDeComprobante = "null";
            int numeroDeComprobante = "null";
            string observacion = "NINGUNA";
            int tipoCompra = 1;
            List<Detalle_transaccion> detalles = new List<Detalle_transaccion>();
            Detalle_transaccion detalle = new Detalle_transaccion()
            {
                detalle = "null",
                id_precio = null,
                cantidad_secundaria = 0,
                indicadorMultiproposito = null,
                id_cuenta_contable = null,
                isc = 0,
                id_concepto_negocio = 5,
                id = 0,
                total = 10,
                cantidad = 1,
                descuento = 0,
                igv = 0,
                lote = "NN",
                precio_unitario = 10,
                registro = "NN",
                vencimiento = new DateTime(2001, 01, 1, 00, 00, 00)
            };

            {


                detalles.Add(detalle);
                OperationResult resultado = new OperationResult();
                resultado = _operacion.ConfirmarCompra(idEmpleado, idCentroAtencion, idProveedor, tipoCompra, idTipoComprobante, idSerieComprobante, esPropio,
                 numeroSerieDeComprobante, numeroDeComprobante, observacion, fechaRegistro, detalles, flete);


                Assert.AreEqual(OperationResultEnum.Success, resultado.code_result);
            }
        }

        [TestMethod()]
        public void ConfirmarVentaAlContadoTest()
        {

            int idEmpleado = 13;
            int idCentroDeAtencion = 3;
            int idCliente = 15;
            //int idTipoComprobante = 407;//comprobante nota interna
            int idTipoComprobante = 27;//comprobante boleta de venta electronica

            int idSerieComprobante = 2;
            bool esNumeroIngresado = false;
            bool gravaIgv = false;
            string observacion = "NINGUNA";
            DateTime fechaRegistro = new DateTime(2018, 12, 04, 00, 00, 00);
            bool esVentaPasada = false;
            bool pagarInicialAlConfirmar = false;
            int idMedioDePago = 281;
            int idEntidadFinanciera = 0;
            string informacionBancaria = "";
            bool detalleUnificado = false;
            decimal flete = 0;


            List<Detalle_transaccion> detalles = new List<Detalle_transaccion>();
            Detalle_transaccion detalle = new Detalle_transaccion()
            {
                cantidad = 5,
                id_concepto_negocio = 1,
                detalle = "",
                precio_unitario = 20,
                total = 701,
                id_precio = null,
                cantidad_secundaria = 0,
                indicadorMultiproposito = null,
                id_cuenta_contable = null,
                isc = 0,
                igv = 0,
                descuento = 0,
                lote = null,
                vencimiento = null,
                registro = null

                //vencimiento = new DateTime(2001, 01, 1, 00, 00, 00)
            };

            detalles.Add(detalle);
            OperationResult resultado = new OperationResult();
            //resultado = _operacion.ConfirmarVentaAlContado(idEmpleado, ModoOperacionEnum.PorMostrador, idCentroDeAtencion, idCliente, null, idTipoComprobante, idSerieComprobante, null, gravaIgv, observacion, fechaRegistro, esVentaPasada, pagarInicialAlConfirmar, idMedioDePago, idEntidadFinanciera, informacionBancaria, detalles, detalleUnificado, flete);

            Assert.AreEqual(OperationResultEnum.Success, resultado.code_result);


        }
        [TestMethod()]
        public void ConfirmarCompraBoletaVentaTest()
        {
            bool esPropio = false;
            DateTime fechaRegistro = new DateTime(2018, 10, 04, 00, 00, 00);
            decimal flete = 0;
            int idCentroAtencion = 3;
            int idEmpleado = 9;
            int idProveedor = 1236;
            int idSerieComprobante = 0;
            int idTipoComprobante = 26;
            string numeroSerieDeComprobante = "F001";
            int numeroDeComprobante = "8956";
            string observacion = "NINGUNA";
            int tipoCompra = 1;
            List<Detalle_transaccion> detalles = new List<Detalle_transaccion>();
            Detalle_transaccion detalle = new Detalle_transaccion()
            {
                detalle = "0",
                id_precio = null,
                cantidad_secundaria = 0,
                indicadorMultiproposito = null,
                id_cuenta_contable = null,
                isc = 0,
                id_concepto_negocio = 25,
                id = 0,
                total = 7,
                cantidad = 1,
                descuento = 2,
                igv = 0,
                lote = "NN",
                precio_unitario = 9,
                registro = "NN",
                vencimiento = new DateTime(2021, 01, 1, 00, 00, 00)
            };

            {


                detalles.Add(detalle);
                OperationResult resultado = new OperationResult();
                resultado = _operacion.ConfirmarCompra(idEmpleado, idCentroAtencion, idProveedor, tipoCompra, idTipoComprobante, idSerieComprobante, esPropio,
                 numeroSerieDeComprobante, numeroDeComprobante, observacion, fechaRegistro, detalles, flete);


                Assert.AreEqual(OperationResultEnum.Success, resultado.code_result);
            }
        }

        [TestMethod()]
        public void ConfirmarCompraFacturaTest()
        {
            bool esPropio = false;
            DateTime fechaRegistro = new DateTime(2018, 10, 04, 00, 00, 00);
            decimal flete = 0;
            int idCentroAtencion = 3;
            int idEmpleado = 9;
            int idProveedor = 1236;
            int idSerieComprobante = 0;
            int idTipoComprobante = 27;
            string numeroSerieDeComprobante = "B001";
            int numeroDeComprobante = "968";
            string observacion = "NINGUNA";
            int tipoCompra = 1;
            List<Detalle_transaccion> detalles = new List<Detalle_transaccion>();
            Detalle_transaccion detalle = new Detalle_transaccion()
            {
                detalle = "0",
                id_precio = null,
                cantidad_secundaria = 0,
                indicadorMultiproposito = null,
                id_cuenta_contable = null,
                isc = 0,
                id_concepto_negocio = 25,
                id = 0,
                total = 15,
                cantidad = 3,
                descuento = 0,
                igv = 0,
                lote = "NN",
                precio_unitario = 5,
                registro = "NN",
                vencimiento = new DateTime(2020, 01, 1, 00, 00, 00)
            };

            {
                detalles.Add(detalle);
                OperationResult resultado = new OperationResult();
                resultado = _operacion.ConfirmarCompra(idEmpleado, idCentroAtencion, idProveedor, tipoCompra, idTipoComprobante, idSerieComprobante, esPropio,
                 numeroSerieDeComprobante, numeroDeComprobante, observacion, fechaRegistro, detalles, flete);
                Assert.AreEqual(OperationResultEnum.Success, resultado.code_result);
            }
        }

        [TestMethod()]
        public void ObtenerVentasYCobranzasMasivasTest()
        {
            DateTime fecha = new DateTime(2019, 1, 15);
            var result = _operacion.ObtenerVentasYCobranzasMasivas(fecha.AddDays(-20), fecha);
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void ObtenerOrdenesMovimientoMercaderiaPorRecibirTest()
        {
            DateTime fecha = new DateTime(2019, 1, 15);
            var result = _operacion.ObtenerOrdenesMovimientoMercaderiaPorRecibir(3, 12, fecha.AddDays(-7), fecha);
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void ObtenerReporteDeudasDeClienteTest()
        {
            var resultado = _operacion.ObtenerReporteDeudasDeCliente();
            Assert.Fail();
        }
        */



        #region Pruebas unitarias de nota de bedito y credito de compra y ventas

        //[TestMethod()]
        //public void GuardarNotaDeDebitoDeCompra155Test()
        //{
        //    //Declaracion de la variables
        //    int idEmpleado = 7;
        //    int idCentroAtencion = 1;
        //    int idOrdenDeOperacion = 74694;
        //    int idTipoNota = 155;
        //    string motivo = "INTERESES POR MORA";
        //    int idTipoComprobante = 29;
        //    int idSerieComprobante = 0;
        //    bool esPropio = false;
        //    string numeroSerieDeComprobante = "BE01";
        //    int numeroDeComprobante = 1;
        //    string valorDeNota = "600";

        //    List<DetalleOrdenDeNotaDebitoCredito> detalles = new List<DetalleOrdenDeNotaDebitoCredito>();
        //    Detalle_transaccion detalle = new Detalle_transaccion()
        //    {
        //        detalle = "",
        //        id_precio = null,
        //        cantidad_secundaria = 0,
        //        indicadorMultiproposito = null,
        //        id_cuenta_contable = null,
        //        isc = 0,
        //        id_concepto_negocio = 1,
        //        id = 0,
        //        total = 5000,
        //        cantidad = 100,
        //        descuento = 0,
        //        igv = 0,
        //        lote = "NN",
        //        precio_unitario = 50,
        //        registro = "NN",
        //        vencimiento = new DateTime(2020, 01, 1, 00, 00, 00)
        //    };
        //    string valorDeDetalle = "";
        //    detalles.Add(new DetalleOrdenDeNotaDebitoCredito(detalle, valorDeDetalle));
        //    //Ejecucion de la prueba
        //    OperationResult resultado = new OperationResult();
        //    resultado = _operacion.GuardarNotaDeDebitoDeCompra(idEmpleado, idCentroAtencion, idOrdenDeOperacion, idTipoNota, motivo, idTipoComprobante, idSerieComprobante, esPropio, numeroSerieDeComprobante, numeroDeComprobante, valorDeNota, detalles);
        //    //Comprobancion de resultado
        //    Assert.AreEqual(OperationResultEnum.Success, resultado.code_result);
        //}

        //[TestMethod()]
        //public void GuardarNotaDeDebitoDeCompra157Test()
        //{
        //    //Declaracion de la variables
        //    int idEmpleado = 7;
        //    int idCentroAtencion = 1;
        //    int idOrdenDeOperacion = 74694;
        //    int idTipoNota = 157;
        //    string motivo = "AUMENTO EN EL VALOR";
        //    int idTipoComprobante = 29;
        //    int idSerieComprobante = 0;
        //    bool esPropio = false;
        //    string numeroSerieDeComprobante = "BE01";
        //    int numeroDeComprobante = 2;
        //    string valorDeNota = "";

        //    List<DetalleOrdenDeNotaDebitoCredito> detalles = new List<DetalleOrdenDeNotaDebitoCredito>();
        //    Detalle_transaccion detalle = new Detalle_transaccion()
        //    {
        //        detalle = "",
        //        id_precio = null,
        //        cantidad_secundaria = 0,
        //        indicadorMultiproposito = null,
        //        id_cuenta_contable = null,
        //        isc = 0,
        //        id_concepto_negocio = 1,
        //        id = 0,
        //        total = 5000,
        //        cantidad = 100,
        //        descuento = 0,
        //        igv = 0,
        //        lote = "NN",
        //        precio_unitario = 50,
        //        registro = "NN",
        //        vencimiento = new DateTime(2020, 01, 1, 00, 00, 00)
        //    };
        //    string valorDeDetalle = "10";
        //    detalles.Add(new DetalleOrdenDeNotaDebitoCredito(detalle, valorDeDetalle));
        //    //Ejecucion de la prueba
        //    OperationResult resultado = new OperationResult();
        //    resultado = _operacion.GuardarNotaDeDebitoDeCompra(idEmpleado, idCentroAtencion, idOrdenDeOperacion, idTipoNota, motivo, idTipoComprobante, idSerieComprobante, esPropio, numeroSerieDeComprobante, numeroDeComprobante, valorDeNota, detalles);
        //    //Comprobancion de resultado
        //    Assert.AreEqual(OperationResultEnum.Success, resultado.code_result);
        //}

        //[TestMethod()]
        //public void GuardarNotaDeCreditoDeCompra133Test()
        //{
        //    //Declaracion de la variables
        //    int idEmpleado = 7;
        //    int idCentroAtencion = 1;
        //    int idOrdenDeOperacion = 74694;
        //    int idTipoNota = 133;
        //    string motivo = "ANULACIÓN DE LA OPERACIÓN";
        //    int idTipoComprobante = 29;
        //    int idSerieComprobante = 0;
        //    bool esPropio = false;
        //    string numeroSerieDeComprobante = "BE01";
        //    int numeroDeComprobante = 3;
        //    string valorDeNota = "";

        //    List<DetalleOrdenDeNotaDebitoCredito> detalles = new List<DetalleOrdenDeNotaDebitoCredito>();
        //    Detalle_transaccion detalle = new Detalle_transaccion()
        //    {
        //        detalle = "",
        //        id_precio = null,
        //        cantidad_secundaria = 0,
        //        indicadorMultiproposito = null,
        //        id_cuenta_contable = null,
        //        isc = 0,
        //        id_concepto_negocio = 1,
        //        id = 0,
        //        total = 5000,
        //        cantidad = 100,
        //        descuento = 0,
        //        igv = 0,
        //        lote = "NN",
        //        precio_unitario = 50,
        //        registro = "NN",
        //        vencimiento = new DateTime(2020, 01, 1, 00, 00, 00)
        //    };
        //    string valorDeDetalle = "";
        //    detalles.Add(new DetalleOrdenDeNotaDebitoCredito(detalle, valorDeDetalle));
        //    //Ejecucion de la prueba
        //    OperationResult resultado = new OperationResult();
        //    resultado = _operacion.GuardarNotaDeCreditoDeCompra(idEmpleado, idCentroAtencion, idOrdenDeOperacion, idTipoNota, motivo, idTipoComprobante, idSerieComprobante, esPropio, numeroSerieDeComprobante, numeroDeComprobante, valorDeNota, detalles);
        //    //Comprobancion de resultado
        //    Assert.AreEqual(OperationResultEnum.Success, resultado.code_result);
        //}

        //[TestMethod()]
        //public void GuardarNotaDeCreditoDeCompra139Test()
        //{
        //    //Declaracion de la variables
        //    int idEmpleado = 7;
        //    int idCentroAtencion = 1;
        //    int idOrdenDeOperacion = 74694;
        //    int idTipoNota = 139;
        //    string motivo = "DESCUENTO GLOBAL";
        //    int idTipoComprobante = 29;
        //    int idSerieComprobante = 0;
        //    bool esPropio = false;
        //    string numeroSerieDeComprobante = "BE01";
        //    int numeroDeComprobante = 4;
        //    string valorDeNota = "1000";

        //    List<DetalleOrdenDeNotaDebitoCredito> detalles = new List<DetalleOrdenDeNotaDebitoCredito>();
        //    Detalle_transaccion detalle = new Detalle_transaccion()
        //    {
        //        detalle = "",
        //        id_precio = null,
        //        cantidad_secundaria = 0,
        //        indicadorMultiproposito = null,
        //        id_cuenta_contable = null,
        //        isc = 0,
        //        id_concepto_negocio = 1,
        //        id = 0,
        //        total = 5000,
        //        cantidad = 100,
        //        descuento = 0,
        //        igv = 0,
        //        lote = "NN",
        //        precio_unitario = 50,
        //        registro = "NN",
        //        vencimiento = new DateTime(2020, 01, 1, 00, 00, 00)
        //    };
        //    string valorDeDetalle = "";
        //    detalles.Add(new DetalleOrdenDeNotaDebitoCredito(detalle, valorDeDetalle));
        //    //Ejecucion de la prueba
        //    OperationResult resultado = new OperationResult();
        //    resultado = _operacion.GuardarNotaDeCreditoDeCompra(idEmpleado, idCentroAtencion, idOrdenDeOperacion, idTipoNota, motivo, idTipoComprobante, idSerieComprobante, esPropio, numeroSerieDeComprobante, numeroDeComprobante, valorDeNota, detalles);
        //    //Comprobancion de resultado
        //    Assert.AreEqual(OperationResultEnum.Success, resultado.code_result);
        //}

        //[TestMethod()]
        //public void GuardarNotaDeCreditoDeCompra141Test()
        //{
        //    //Declaracion de la variables
        //    int idEmpleado = 7;
        //    int idCentroAtencion = 1;
        //    int idOrdenDeOperacion = 74694;
        //    int idTipoNota = 141;
        //    string motivo = "DESCUENTO POR ÍTEM";
        //    int idTipoComprobante = 29;
        //    int idSerieComprobante = 0;
        //    bool esPropio = false;
        //    string numeroSerieDeComprobante = "BE01";
        //    int numeroDeComprobante = 5;
        //    string valorDeNota = "";

        //    List<DetalleOrdenDeNotaDebitoCredito> detalles = new List<DetalleOrdenDeNotaDebitoCredito>();
        //    Detalle_transaccion detalle = new Detalle_transaccion()
        //    {
        //        detalle = "",
        //        id_precio = null,
        //        cantidad_secundaria = 0,
        //        indicadorMultiproposito = null,
        //        id_cuenta_contable = null,
        //        isc = 0,
        //        id_concepto_negocio = 1,
        //        id = 0,
        //        total = 5000,
        //        cantidad = 100,
        //        descuento = 0,
        //        igv = 0,
        //        lote = "NN",
        //        precio_unitario = 50,
        //        registro = "NN",
        //        vencimiento = new DateTime(2020, 01, 1, 00, 00, 00)
        //    };
        //    string valorDeDetalle = "100";
        //    detalles.Add(new DetalleOrdenDeNotaDebitoCredito(detalle, valorDeDetalle));
        //    //Ejecucion de la prueba
        //    OperationResult resultado = new OperationResult();
        //    resultado = _operacion.GuardarNotaDeCreditoDeCompra(idEmpleado, idCentroAtencion, idOrdenDeOperacion, idTipoNota, motivo, idTipoComprobante, idSerieComprobante, esPropio, numeroSerieDeComprobante, numeroDeComprobante, valorDeNota, detalles);
        //    //Comprobancion de resultado
        //    Assert.AreEqual(OperationResultEnum.Success, resultado.code_result);
        //}

        //[TestMethod()]
        //public void GuardarNotaDeCreditoDeCompra145Test()
        //{
        //    //Declaracion de la variables
        //    int idEmpleado = 7;
        //    int idCentroAtencion = 1;
        //    int idOrdenDeOperacion = 74694;
        //    int idTipoNota = 145;
        //    string motivo = "DEVOLUCIÓN POR ÍTEM";
        //    int idTipoComprobante = 29;
        //    int idSerieComprobante = 0;
        //    bool esPropio = false;
        //    string numeroSerieDeComprobante = "BE01";
        //    int numeroDeComprobante = 6;
        //    string valorDeNota = "";

        //    List<DetalleOrdenDeNotaDebitoCredito> detalles = new List<DetalleOrdenDeNotaDebitoCredito>();
        //    Detalle_transaccion detalle = new Detalle_transaccion()
        //    {
        //        detalle = "",
        //        id_precio = null,
        //        cantidad_secundaria = 0,
        //        indicadorMultiproposito = null,
        //        id_cuenta_contable = null,
        //        isc = 0,
        //        id_concepto_negocio = 1,
        //        id = 0,
        //        total = 5000,
        //        cantidad = 100,
        //        descuento = 0,
        //        igv = 0,
        //        lote = "NN",
        //        precio_unitario = 50,
        //        registro = "NN",
        //        vencimiento = new DateTime(2020, 01, 1, 00, 00, 00)
        //    };
        //    string valorDeDetalle = "10";
        //    detalles.Add(new DetalleOrdenDeNotaDebitoCredito(detalle, valorDeDetalle));
        //    //Ejecucion de la prueba
        //    OperationResult resultado = new OperationResult();
        //    resultado = _operacion.GuardarNotaDeCreditoDeCompra(idEmpleado, idCentroAtencion, idOrdenDeOperacion, idTipoNota, motivo, idTipoComprobante, idSerieComprobante, esPropio, numeroSerieDeComprobante, numeroDeComprobante, valorDeNota, detalles);
        //    //Comprobancion de resultado
        //    Assert.AreEqual(OperationResultEnum.Success, resultado.code_result);
        //}

        ////Venta
        //[TestMethod()]
        //public void GuardarNotaDeDebitoDeVenta155Test()
        //{
        //    //Declaracion de la variables
        //    int idEmpleado = 7;
        //    int idCentroAtencion = 1;
        //    int idOrdenDeOperacion = 74642;
        //    int idTipoNota = 155;
        //    string motivo = "INTERESES POR MORA";
        //    int idTipoComprobante = 0;
        //    int idSerieComprobante = 26;
        //    bool esPropio = true;
        //    string numeroSerieDeComprobante = "";
        //    int numeroDeComprobante = 0;
        //    string valorDeNota = "50";
        //    List<DetalleOrdenDeNotaDebitoCredito> detalles = new List<DetalleOrdenDeNotaDebitoCredito>();
        //    Detalle_transaccion detalle = new Detalle_transaccion()
        //    {
        //        detalle = "",
        //        id_precio = null,
        //        cantidad_secundaria = 0,
        //        indicadorMultiproposito = null,
        //        id_cuenta_contable = null,
        //        isc = 0,
        //        id_concepto_negocio = 1,
        //        id = 0,
        //        total = 144,
        //        cantidad = (decimal)V,
        //        descuento = 0,
        //        igv = 0,
        //        lote = "NN",
        //        precio_unitario = (decimal)V1,
        //        registro = "NN",
        //        vencimiento = new DateTime(2020, 01, 1, 00, 00, 00)
        //    };
        //    string valorDeDetalle = "";
        //    detalles.Add(new DetalleOrdenDeNotaDebitoCredito(detalle, valorDeDetalle));
        //    //Ejecucion de la prueba
        //    OperationResult resultado = new OperationResult();
        //    resultado = _operacion.GuardarNotaDeDebitoDeVenta(idEmpleado, idCentroAtencion, idOrdenDeOperacion, idTipoNota, motivo, idTipoComprobante, idSerieComprobante, esPropio, numeroSerieDeComprobante, numeroDeComprobante, valorDeNota, detalles);
        //    //Comprobancion de resultado
        //    Assert.AreEqual(OperationResultEnum.Success, resultado.code_result);
        //}

        //[TestMethod()]
        //public void GuardarNotaDeDebitoDeVenta157Test()
        //{
        //    //Declaracion de la variables
        //    int idEmpleado = 7;
        //    int idCentroAtencion = 1;
        //    int idOrdenDeOperacion = 74642;
        //    int idTipoNota = 157;
        //    string motivo = "AUMENTO EN EL VALOR";
        //    int idTipoComprobante = 0;
        //    int idSerieComprobante = 26;
        //    bool esPropio = true;
        //    string numeroSerieDeComprobante = "";
        //    int numeroDeComprobante = 0;
        //    string valorDeNota = "";

        //    List<DetalleOrdenDeNotaDebitoCredito> detalles = new List<DetalleOrdenDeNotaDebitoCredito>();
        //    Detalle_transaccion detalle = new Detalle_transaccion()
        //    {
        //        detalle = "",
        //        id_precio = null,
        //        cantidad_secundaria = 0,
        //        indicadorMultiproposito = null,
        //        id_cuenta_contable = null,
        //        isc = 0,
        //        id_concepto_negocio = 1,
        //        id = 0,
        //        total = 144,
        //        cantidad = (decimal)V,
        //        descuento = 0,
        //        igv = 0,
        //        lote = "NN",
        //        precio_unitario = (decimal)V1,
        //        registro = "NN",
        //        vencimiento = new DateTime(2020, 01, 1, 00, 00, 00)
        //    };
        //    string valorDeDetalle = "10";
        //    detalles.Add(new DetalleOrdenDeNotaDebitoCredito(detalle, valorDeDetalle));
        //    //Ejecucion de la prueba
        //    OperationResult resultado = new OperationResult();
        //    resultado = _operacion.GuardarNotaDeDebitoDeVenta(idEmpleado, idCentroAtencion, idOrdenDeOperacion, idTipoNota, motivo, idTipoComprobante, idSerieComprobante, esPropio, numeroSerieDeComprobante, numeroDeComprobante, valorDeNota, detalles);
        //    //Comprobancion de resultado
        //    Assert.AreEqual(OperationResultEnum.Success, resultado.code_result);
        //}

        //[TestMethod()]
        //public void GuardarNotaDeCreditoDeVenta133Test()
        //{
        //    //Declaracion de la variables
        //    int idEmpleado = 7;
        //    int idCentroAtencion = 1;
        //    int idOrdenDeOperacion = 74694;
        //    int idTipoNota = 133;
        //    string motivo = "ANULACIÓN DE LA OPERACIÓN";
        //    int idTipoComprobante = 0;
        //    int idSerieComprobante = 24;
        //    bool esPropio = true;
        //    string numeroSerieDeComprobante = "";
        //    int numeroDeComprobante = 0;
        //    string valorDeNota = "";

        //    List<DetalleOrdenDeNotaDebitoCredito> detalles = new List<DetalleOrdenDeNotaDebitoCredito>();
        //    Detalle_transaccion detalle = new Detalle_transaccion()
        //    {
        //        detalle = "",
        //        id_precio = null,
        //        cantidad_secundaria = 0,
        //        indicadorMultiproposito = null,
        //        id_cuenta_contable = null,
        //        isc = 0,
        //        id_concepto_negocio = 1,
        //        id = 0,
        //        total = 144,
        //        cantidad = (decimal)V,
        //        descuento = 0,
        //        igv = 0,
        //        lote = "NN",
        //        precio_unitario = (decimal)V1,
        //        registro = "NN",
        //        vencimiento = new DateTime(2020, 01, 1, 00, 00, 00)
        //    };
        //    string valorDeDetalle = "";
        //    detalles.Add(new DetalleOrdenDeNotaDebitoCredito(detalle, valorDeDetalle));
        //    //Ejecucion de la prueba
        //    OperationResult resultado = new OperationResult();
        //    resultado = _operacion.GuardarNotaDeCreditoDeVenta(idEmpleado, idCentroAtencion, idOrdenDeOperacion, idTipoNota, motivo, idTipoComprobante, idSerieComprobante, esPropio, numeroSerieDeComprobante, numeroDeComprobante, valorDeNota, detalles);
        //    //Comprobancion de resultado
        //    Assert.AreEqual(OperationResultEnum.Success, resultado.code_result);
        //}

        //[TestMethod()]
        //public void GuardarNotaDeCreditoDeVenta139Test()
        //{
        //    //Declaracion de la variables
        //    int idEmpleado = 7;
        //    int idCentroAtencion = 1;
        //    int idOrdenDeOperacion = 74694;
        //    int idTipoNota = 139;
        //    string motivo = "DESCUENTO GLOBAL";
        //    int idTipoComprobante = 0;
        //    int idSerieComprobante = 24;
        //    bool esPropio = true;
        //    string numeroSerieDeComprobante = "";
        //    int numeroDeComprobante = 0;
        //    string valorDeNota = "14";

        //    List<DetalleOrdenDeNotaDebitoCredito> detalles = new List<DetalleOrdenDeNotaDebitoCredito>();
        //    Detalle_transaccion detalle = new Detalle_transaccion()
        //    {
        //        detalle = "",
        //        id_precio = null,
        //        cantidad_secundaria = 0,
        //        indicadorMultiproposito = null,
        //        id_cuenta_contable = null,
        //        isc = 0,
        //        id_concepto_negocio = 1,
        //        id = 0,
        //        total = 144,
        //        cantidad = (decimal)V,
        //        descuento = 0,
        //        igv = 0,
        //        lote = "NN",
        //        precio_unitario = (decimal)V1,
        //        registro = "NN",
        //        vencimiento = new DateTime(2020, 01, 1, 00, 00, 00)
        //    };
        //    string valorDeDetalle = "";
        //    detalles.Add(new DetalleOrdenDeNotaDebitoCredito(detalle, valorDeDetalle));
        //    //Ejecucion de la prueba
        //    OperationResult resultado = new OperationResult();
        //    resultado = _operacion.GuardarNotaDeCreditoDeVenta(idEmpleado, idCentroAtencion, idOrdenDeOperacion, idTipoNota, motivo, idTipoComprobante, idSerieComprobante, esPropio, numeroSerieDeComprobante, numeroDeComprobante, valorDeNota, detalles);
        //    //Comprobancion de resultado
        //    Assert.AreEqual(OperationResultEnum.Success, resultado.code_result);
        //}

        //[TestMethod()]
        //public void GuardarNotaDeCreditoDeVenta141Test()
        //{
        //    //Declaracion de la variables
        //    int idEmpleado = 7;
        //    int idCentroAtencion = 1;
        //    int idOrdenDeOperacion = 74694;
        //    int idTipoNota = 141;
        //    string motivo = "DESCUENTO POR ÍTEM";
        //    int idTipoComprobante = 0;
        //    int idSerieComprobante = 24;
        //    bool esPropio = true;
        //    string numeroSerieDeComprobante = "";
        //    int numeroDeComprobante = 0;
        //    string valorDeNota = "";

        //    List<DetalleOrdenDeNotaDebitoCredito> detalles = new List<DetalleOrdenDeNotaDebitoCredito>();
        //    Detalle_transaccion detalle = new Detalle_transaccion()
        //    {
        //        detalle = "",
        //        id_precio = null,
        //        cantidad_secundaria = 0,
        //        indicadorMultiproposito = null,
        //        id_cuenta_contable = null,
        //        isc = 0,
        //        id_concepto_negocio = 1,
        //        id = 0,
        //        total = 144,
        //        cantidad = (decimal)V,
        //        descuento = 0,
        //        igv = 0,
        //        lote = "NN",
        //        precio_unitario = (decimal)V1,
        //        registro = "NN",
        //        vencimiento = new DateTime(2020, 01, 1, 00, 00, 00)
        //    };
        //    string valorDeDetalle = "100";
        //    detalles.Add(new DetalleOrdenDeNotaDebitoCredito(detalle, valorDeDetalle));
        //    //Ejecucion de la prueba
        //    OperationResult resultado = new OperationResult();
        //    resultado = _operacion.GuardarNotaDeCreditoDeVenta(idEmpleado, idCentroAtencion, idOrdenDeOperacion, idTipoNota, motivo, idTipoComprobante, idSerieComprobante, esPropio, numeroSerieDeComprobante, numeroDeComprobante, valorDeNota, detalles);
        //    //Comprobancion de resultado
        //    Assert.AreEqual(OperationResultEnum.Success, resultado.code_result);
        //}

        //[TestMethod()]
        //public void GuardarNotaDeCreditoDeVenta145Test()
        //{
        //    //Declaracion de la variables
        //    int idEmpleado = 7;
        //    int idCentroAtencion = 1;
        //    int idOrdenDeOperacion = 74694;
        //    int idTipoNota = 145;
        //    string motivo = "DEVOLUCIÓN POR ÍTEM";
        //    int idTipoComprobante = 0;
        //    int idSerieComprobante = 24;
        //    bool esPropio = true;
        //    string numeroSerieDeComprobante = "";
        //    int numeroDeComprobante = 0;
        //    string valorDeNota = "";

        //    List<DetalleOrdenDeNotaDebitoCredito> detalles = new List<DetalleOrdenDeNotaDebitoCredito>();
        //    Detalle_transaccion detalle = new Detalle_transaccion()
        //    {
        //        detalle = "",
        //        id_precio = null,
        //        cantidad_secundaria = 0,
        //        indicadorMultiproposito = null,
        //        id_cuenta_contable = null,
        //        isc = 0,
        //        id_concepto_negocio = 1,
        //        id = 0,
        //        total = 144,
        //        cantidad = (decimal)V,
        //        descuento = 0,
        //        igv = 0,
        //        lote = "NN",
        //        precio_unitario = (decimal)V1,
        //        registro = "NN",
        //        vencimiento = new DateTime(2020, 01, 1, 00, 00, 00)
        //    };
        //    string valorDeDetalle = "2";
        //    detalles.Add(new DetalleOrdenDeNotaDebitoCredito(detalle, valorDeDetalle));
        //    //Ejecucion de la prueba
        //    OperationResult resultado = new OperationResult();
        //    resultado = _operacion.GuardarNotaDeCreditoDeVenta(idEmpleado, idCentroAtencion, idOrdenDeOperacion, idTipoNota, motivo, idTipoComprobante, idSerieComprobante, esPropio, numeroSerieDeComprobante, numeroDeComprobante, valorDeNota, detalles);
        //    //Comprobancion de resultado
        //    Assert.AreEqual(OperationResultEnum.Success, resultado.code_result);
        //}

        #endregion

        #region REPORTES DE VENTAS POR MODALIDAD CONSOLIDADO POR CONCEPTO
        [TestMethod()]
        public void ObtenerResumenDeVentasPorModalidadYPorVendedorConfirmadasTest()
        {
            //var resultado1 = _operacion.ObtenerResumenesDeVentasPorModalidadYPorVendedorConfirmadas(new string[] { "1" }, 13, new DateTime(2019, 04, 1, 12, 00, 00), new DateTime(2019, 04, 19, 14, 00, 00));

            //var resultado2 = _operacion.ObtenerDetallesDeVentasPorModalidadPorVendedorConfirmadas(new string[] { "1" }, 13, new DateTime(2019, 04, 1, 12, 00, 00), new DateTime(2019, 04, 19, 14, 00, 00));

            var resultado1 = _operacion.ObtenerResumenesDeVentasPorModalidadYPorVendedorConfirmadas(new string[] { "1", "4" }, 13, new DateTime(2019, 04, 21, 00, 00, 00), new DateTime(2019, 04, 21, 23, 59, 59));

            var resultado2 = _operacion.ObtenerDetallesDeVentasPorModalidadPorVendedorAgrupadoPorMercaderiaConfirmadas(new string[] { "1", "4" }, 13, new DateTime(2019, 04, 21, 00, 00, 00), new DateTime(2019, 04, 21, 23, 59, 59));

            Assert.Fail();
        }


        [TestMethod()]
        public void ObtenerDetalleDeVentasPorModalidadPorVendedorConfirmadasTest()
        {
            var resultado = _operacion.ObtenerDetallesDeVentasPorModalidadPorVendedorAgrupadoPorMercaderiaConfirmadas(new string[] { "1" }, 13, new DateTime(2019, 04, 18, 12, 00, 00), new DateTime(2019, 04, 18, 14, 00, 00));
            Assert.Fail();
        }
        #endregion



        #region METODOS PARA VENTAS Y COBROS MASIVOS
        [TestMethod()]
        public void ObtenerVentasYCobranzasMasivaTest()
        {
            //2019-05-01 15:56:03.187

            //Funciona
            //var resultado = _operacion.ObtenerVentasYCobranzasMasiva(new DateTime(2019, 04, 30, 17, 59, 20, 250));
            //No Funciona
            //var resultado = _operacion.ObtenerVentasYCobranzasMasiva(new DateTime(2019,05,02,14,40,41,387));

            var resultado = _operacion.ObtenerVentasYCobranzasMasiva(40550);

            Assert.Fail();
        }



        #endregion

        [TestMethod()]
        public void ObtenerConceptosProximosAVencerPorAlmacenTest()
        {
            //var result = _operacion.ObtenerConceptosProximosAVencerPorAlmacen(new int[] { 11 }, new DateTime(2019, 08, 26, 12, 00, 00), new DateTime(2019, 09, 02, 12, 00, 00));
            //Assert.IsNotNull(result);
        }
        /*
        [TestMethod()]
        public void GuardarCompraTest()
        {
            int idProveedor = 74;
            int idCentroDeAtencion = 3;
            int idEmpleado = 37;
            int idTipoComprobante = 27;
            string numeroSerieDeComprobante = "B001";
            int numeroDeComprobante = 111;
            string observacion = "NINGUNA";
            DateTime fechaRegistro = new DateTime(2019, 12, 15, 00, 00, 00);
            decimal flete = 0;


            List<DetalleDeOperacion> detallesOperacion = new List<DetalleDeOperacion>()
            {
                new DetalleDeOperacion(new Detalle_transaccion()
            {
                cantidad = 2,
                id_concepto_negocio = 12,
                detalle = "ninguno",
                precio_unitario = 20,
                total = 40,
                id_precio = null,
                cantidad_secundaria = 0,
                indicadorMultiproposito = null,
                id_cuenta_contable = null,
                isc = 0,
                igv = 0,
                descuento = 0,
                lote = null,
                vencimiento = null,
                registro = null
            },true,"110", new List<ValorDetalleMaestroDetalleTransaccion>())
            };

            OperationResult resultado = new OperationResult();

            resultado = _operacion.ConfirmarCompra(idEmpleado, idCentroDeAtencion, idEmpleado, idCentroDeAtencion, idEmpleado, idCentroDeAtencion, idProveedor, (int)TipoOperacionCompra.NoGravada, idTipoComprobante, 2, false, numeroSerieDeComprobante, numeroDeComprobante, observacion, fechaRegistro, detallesOperacion, flete);

            Assert.IsNotNull(resultado);
        }


        [TestMethod()]
        public void GuardarCompraTestLote()
        {
            int idProveedor = 74;
            int idCentroDeAtencion = 3;
            int idEmpleado = 37;
            int idTipoComprobante = 27;
            string numeroSerieDeComprobante = "B001";
            int numeroDeComprobante = 222;
            string observacion = "NINGUNA";
            DateTime fechaRegistro = new DateTime(2019, 12, 15, 00, 00, 00);
            decimal flete = 0;


            List<DetalleDeOperacion> detallesOperacion = new List<DetalleDeOperacion>()
            {
                new DetalleDeOperacion(new Detalle_transaccion()
            {
                cantidad = 2,
                id_concepto_negocio = 12,
                detalle = "ninguno",
                precio_unitario = 20,
                total = 40,
                id_precio = null,
                cantidad_secundaria = 0,
                indicadorMultiproposito = null,
                id_cuenta_contable = null,
                isc = 0,
                igv = 0,
                descuento = 0,
                lote = "lt1",
                vencimiento = new DateTime(2020, 01, 1, 00, 00, 00),
                registro = null
            },true,"110", new List<ValorDetalleMaestroDetalleTransaccion>())
            };

            OperationResult resultado = new OperationResult();

            resultado = _operacion.ConfirmarCompra(idEmpleado, idCentroDeAtencion, idEmpleado, idCentroDeAtencion, idEmpleado, idCentroDeAtencion, idProveedor, (int)TipoOperacionCompra.NoGravada, idTipoComprobante, 2, false, numeroSerieDeComprobante, numeroDeComprobante, observacion, fechaRegistro, detallesOperacion, flete);

            Assert.IsNotNull(resultado);
        }



        [TestMethod()]
        public void GuardarCompraTestCaracteristias()
        {
            int idProveedor = 74;
            int idCentroDeAtencion = 3;
            int idEmpleado = 37;
            int idTipoComprobante = 27;
            string numeroSerieDeComprobante = "B001";
            int numeroDeComprobante = 333;
            string observacion = "NINGUNA";
            DateTime fechaRegistro = new DateTime(2019, 12, 15, 00, 00, 00);
            decimal flete = 0;


            List<DetalleDeOperacion> detallesOperacion = new List<DetalleDeOperacion>()
            {
                new DetalleDeOperacion(new Detalle_transaccion()
            {
                cantidad = 2,
                id_concepto_negocio = 12,
                detalle = "ninguno",
                precio_unitario = 20,
                total = 40,
                id_precio = null,
                cantidad_secundaria = 0,
                indicadorMultiproposito = null,
                id_cuenta_contable = null,
                isc = 0,
                igv = 0,
                descuento = 0,
                lote = null,
                vencimiento = null,
                registro = null
            },true,"110", new List<ValorDetalleMaestroDetalleTransaccion>()
            {
                new ValorDetalleMaestroDetalleTransaccion(new Valor_detalle_maestro_detalle_transaccion(1,14318,null,"0000")),
                new ValorDetalleMaestroDetalleTransaccion(new Valor_detalle_maestro_detalle_transaccion(2,14318,null,"1111"))
            })
            };

            OperationResult resultado = new OperationResult();

            resultado = _operacion.ConfirmarCompra(idEmpleado, idCentroDeAtencion, idEmpleado, idCentroDeAtencion, idEmpleado, idCentroDeAtencion, idProveedor, (int)TipoOperacionCompra.NoGravada, idTipoComprobante, 2, false, numeroSerieDeComprobante, numeroDeComprobante, observacion, fechaRegistro, detallesOperacion, flete);

            Assert.IsNotNull(resultado);
        }

        [TestMethod()]
        public void GuardarCompraTestLoteCaracteristias()
        {
            int idProveedor = 74;
            int idCentroDeAtencion = 3;
            int idEmpleado = 37;
            int idTipoComprobante = 27;
            string numeroSerieDeComprobante = "B001";
            int numeroDeComprobante = 444;
            string observacion = "NINGUNA";
            DateTime fechaRegistro = new DateTime(2019, 12, 15, 00, 00, 00);
            decimal flete = 0;


            List<DetalleDeOperacion> detallesOperacion = new List<DetalleDeOperacion>()
            {
                new DetalleDeOperacion(new Detalle_transaccion() 
            {
                cantidad = 2,
                id_concepto_negocio = 12,
                detalle = "ninguno",
                precio_unitario = 20,
                total = 40,
                id_precio = null,
                cantidad_secundaria = 0,
                indicadorMultiproposito = null,
                id_cuenta_contable = null,
                isc = 0,
                igv = 0,
                descuento = 0,
                lote = "lt1",
                vencimiento = new DateTime(2020, 01, 1, 00, 00, 00),
                registro = null
            },true,"110", new List<ValorDetalleMaestroDetalleTransaccion>()
            {
                new ValorDetalleMaestroDetalleTransaccion(new Valor_detalle_maestro_detalle_transaccion(1,14318,null,"2222")),
                new ValorDetalleMaestroDetalleTransaccion(new Valor_detalle_maestro_detalle_transaccion(2,14318,null,"3333"))
            })
            };

            OperationResult resultado = new OperationResult();

            resultado = _operacion.ConfirmarCompra(idEmpleado, idCentroDeAtencion, idEmpleado, idCentroDeAtencion, idEmpleado, idCentroDeAtencion, idProveedor, (int)TipoOperacionCompra.NoGravada, idTipoComprobante, 2, false, numeroSerieDeComprobante, numeroDeComprobante, observacion, fechaRegistro, detallesOperacion, flete);

            Assert.IsNotNull(resultado);
        }

        */
        [TestMethod()]
        public void GuardarVentaConcurrenteTest()
        {
            OperationResult result1 = null, result2 = null;
            IOperacionLogica _operacion1 = new OperacionLogica();
            IOperacionLogica _operacion2 = new OperacionLogica();
            Parallel.Invoke(() => result1 = CrearGuardarVenta(_operacion1),
                            () => result2 = CrearGuardarVenta(_operacion1));
            
            Assert.AreNotEqual(((OrdenDeVenta)result1.objeto).Comprobante().NumeroDeComprobante, ((OrdenDeVenta)result2.objeto).Comprobante().NumeroDeComprobante);
        }
        
        public OperationResult CrearGuardarVenta(IOperacionLogica logica)
        {
            int idCliente = 1339;
            int idCentroDeAtencion = 3;
            int idEmpleado = 64;
            int idTipoComprobante = 27;
            //int numeroDeComprobante = 33582;
            int idSerieComprobante = 1037;
            int idMedioDePago = 281;
            string observacion = "NINGUNA";
            DateTime fechaRegistro = new DateTime(2021, 04, 22, 00, 00, 00);
            decimal flete = 0;
            string idUsuario = "a54c7cf4-c0b5-4681-b202-3830c455b8b5";

            List<DetalleDeOperacion> detallesOperacion = new List<DetalleDeOperacion>()
            {
                new DetalleDeOperacion()
                { 
                Cantidad = 1,
                Producto = new Concepto_Negocio_Comercial(){Id= 2861,EsBien=true},
                Detalle = "ninguno",
                PrecioUnitario = 20,
                Importe= 40,
                MascaraDeCalculo=  VentasSettings.Default.MascaraDeCalculoPrecioUnitarioCalculado,
                Isc = 0,
                Igv = 0,
                Descuento = 0,
                Lote = null,
                Vencimiento = null,
                Registro = null
                }
            };
                        Empleado_ empleado = _empleadoLogica.ObtenerEmpleadoInclusiveRoles(idUsuario);
            List<CentroDeAtencion> centrosDeAtencionProgramados = _centroDeAtencionLogica.ObtenerCentrosDeAtencionProgramados_(empleado.Id);
            //A partir del user logueado, construir el model
            var session = new UserProfileSessionData() { IdUsuario = idUsuario, NombreUsuario = "JUANCHO", CentrosDeAtencionProgramados = centrosDeAtencionProgramados };
            session.Empleado = empleado ?? throw new Exception("No existe un empleado para la cuenta de usuario, por lo tanto no se puede realizar la operación");
            session.SetCentrosDeAtencionProgramados(centrosDeAtencionProgramados);
            if (session.CentrosDeAtencionProgramados.Count >= 1) session.CentroDeAtencionSeleccionado = session.CentrosDeAtencionProgramados.FirstOrDefault();
            session.IdCentroAtencionQueTieneElStockIntegrada = session.CentroDeAtencionSeleccionado.Id;
            session.AlmacenesInventariosActuales = new Dictionary<long, long> { { 3, 128444 } };

            OperationResult resultado = new OperationResult();
            DatosVentaIntegrada datosVenta = new DatosVentaIntegrada()
            {
                FechaRegistro= fechaRegistro,
                EsVentaModoCaja=false,
                MovimientoAlmacen= new DatosMovimientoDeAlmacen() { HayComprobanteDeSalidaDeMercaderia=false},
                Orden=new DatosOrdenVenta()
                {
                    AplicarIGVCuandoEsAmazonia = false,
                    Cliente= new Modelo.Custom.ActorComercial_()
                    {
                        Id= idCliente
                    },
                    Comprobante = new ComprobanteDeNegocio_() {
                    Tipo= new ItemGenerico() { Id= idTipoComprobante},
                    Serie= new SerieComprobante_() { Id= idSerieComprobante},
                    },
                    Detalles= detallesOperacion,
                    EsVentaPasada= false,
                    FechaEmision= fechaRegistro,
                    DescuentoGlobal=0,
                    Flete=0,
                    Icbper=0,
                    NumeroBolsasDePlastico=0,
                    Observacion="ninguna",
                    Placa="",
                    PuntoDeVenta= new ItemGenerico() { Id= idCentroDeAtencion},
                    UnificarDetalles=false,
                    Vendedor= new ItemGenerico() { Id= idEmpleado}
                },
                Pago= new DatosPago()
                {
                    ModoDePago = ModoPago.Contado,
                    Inicial = 40,
                    Traza = new TrazaDePago_()
                    {
                        MedioDePago = new ItemGenerico { Id = idMedioDePago }
                    }
                }
            };
            resultado = logica.ConfirmarVentaIntegrada(ModoOperacionEnum.PorMostrador, session, datosVenta);

            return resultado;
        }
        /*
        [TestMethod()]
        public void GuardarVentaTestLote()
        {
            int idCliente = 70;
            int idCentroDeAtencion = 3;
            int idEmpleado = 37;
            int idTipoComprobante = 27;
            int numeroDeComprobante = 968;
            string observacion = "NINGUNA";
            DateTime fechaRegistro = new DateTime(2019, 12, 15, 00, 00, 00);
            decimal flete = 0;


            List<DetalleDeOperacion> detallesOperacion = new List<DetalleDeOperacion>()
            {
                new DetalleDeOperacion(new Detalle_transaccion()
            {
                cantidad = 2,
                id_concepto_negocio = 12,
                detalle = "ninguno",
                precio_unitario = 20,
                total = 40,
                id_precio = null,
                cantidad_secundaria = 0,
                indicadorMultiproposito = null,
                id_cuenta_contable = null,
                isc = 0,
                igv = 0,
                descuento = 0,
                lote = "lt1",
                vencimiento = new DateTime(2020, 01, 1, 00, 00, 00),
                registro = null
            },true,"110", new List<ValorDetalleMaestroDetalleTransaccion>())
            };

            OperationResult resultado = new OperationResult();
            long idInventarioFisico = 0;
            resultado = _operacion.ConfirmarVentaAlContado(ModoOperacionEnum.PorMostrador, idEmpleado, idCentroDeAtencion, idEmpleado, idCentroDeAtencion, idEmpleado, idCentroDeAtencion, idCliente, "", idTipoComprobante, 2, numeroDeComprobante, false, fechaRegistro, 1, 281, 0, "", false, false, detallesOperacion, observacion, flete, 0, 0, false, null,0);

            Assert.IsNotNull(resultado);
        }

        [TestMethod()]
        public void GuardarVentaTestCaracteristicas()
        {
            int idCliente = 70;
            int idCentroDeAtencion = 3;
            int idEmpleado = 37;
            int idTipoComprobante = 27;
            int numeroDeComprobante = 968;
            string observacion = "NINGUNA";
            DateTime fechaRegistro = new DateTime(2019, 12, 15, 00, 00, 00);
            decimal flete = 0;


            List<DetalleDeOperacion> detallesOperacion = new List<DetalleDeOperacion>()
            {
                new DetalleDeOperacion(new Detalle_transaccion()
            {
                cantidad = 2,
                id_concepto_negocio = 12,
                detalle = "ninguno",
                precio_unitario = 20,
                total = 40,
                id_precio = null,
                cantidad_secundaria = 0,
                indicadorMultiproposito = null,
                id_cuenta_contable = null,
                isc = 0,
                igv = 0,
                descuento = 0,
                lote = null,
                vencimiento = null,
                registro = null
            },true,"110", new List<ValorDetalleMaestroDetalleTransaccion>()
            {
                new ValorDetalleMaestroDetalleTransaccion(new Valor_detalle_maestro_detalle_transaccion(1,14318,null,"0000")),
                new ValorDetalleMaestroDetalleTransaccion(new Valor_detalle_maestro_detalle_transaccion(2,14318,null,"1111"))
            })
            };

            OperationResult resultado = new OperationResult();
            long idInventarioFisico = 0;
            resultado = _operacion.ConfirmarVentaAlContado(ModoOperacionEnum.PorMostrador, idEmpleado, idCentroDeAtencion, idEmpleado, idCentroDeAtencion, idEmpleado, idCentroDeAtencion, idCliente, "", idTipoComprobante, 2, numeroDeComprobante, false, fechaRegistro, 1, 281, 0, "", false, false, detallesOperacion, observacion, flete, 0, 0, false, null,idInventarioFisico);

            Assert.IsNotNull(resultado);
        }

        [TestMethod()]
        public void GuardarVentaTestLoteCaracteristicas()
        {
            int idCliente = 70;
            int idCentroDeAtencion = 3;
            int idEmpleado = 37;
            int idTipoComprobante = 27;
            int numeroDeComprobante = 968;
            string observacion = "NINGUNA";
            DateTime fechaRegistro = new DateTime(2019, 12, 15, 00, 00, 00);
            decimal flete = 0;


            List<DetalleDeOperacion> detallesOperacion = new List<DetalleDeOperacion>()
            {
                new DetalleDeOperacion(new Detalle_transaccion()
            {
                cantidad = 2,
                id_concepto_negocio = 12,
                detalle = "ninguno",
                precio_unitario = 20,
                total = 40,
                id_precio = null,
                cantidad_secundaria = 0,
                indicadorMultiproposito = null,
                id_cuenta_contable = null,
                isc = 0,
                igv = 0,
                descuento = 0,
                lote = "lt1",
                vencimiento = new DateTime(2020, 01, 1, 00, 00, 00),
                registro = null
            },true,"110", new List<ValorDetalleMaestroDetalleTransaccion>()
            {
                new ValorDetalleMaestroDetalleTransaccion(new Valor_detalle_maestro_detalle_transaccion(11,1,14318,null,"2222")),
                new ValorDetalleMaestroDetalleTransaccion(new Valor_detalle_maestro_detalle_transaccion(12,2,14318,null,"3333"))
            })
            };

            OperationResult resultado = new OperationResult();
            int idInventarioFisico = 0;

            resultado = _operacion.ConfirmarVentaAlContado(ModoOperacionEnum.PorMostrador, idEmpleado, idCentroDeAtencion, idEmpleado, idCentroDeAtencion, idEmpleado, idCentroDeAtencion, idCliente, "", idTipoComprobante, 2, numeroDeComprobante, false, fechaRegistro, 1, 281, 0, "", false, false, detallesOperacion, observacion, flete, 0, 0, false, null, idInventarioFisico);

            Assert.IsNotNull(resultado);
        }
*//*
        [TestMethod()]
        public void ConfirmarVentaAlContadoTest()
        {

            int idCliente = 70;
            int idCentroDeAtencion = 3;
            int idEmpleado = 37;
            int idTipoComprobante = 27;
            int numeroDeComprobante = 968;
            string observacion = "NINGUNA";
            DateTime fechaRegistro = new DateTime(2019, 12, 15, 00, 00, 00);
            decimal flete = 0;


            List<DetalleDeOperacion> detallesOperacion = new List<DetalleDeOperacion>()
            {
                new DetalleDeOperacion(new Detalle_transaccion()
            {
                cantidad = 15,
                id_concepto_negocio = 12,
                detalle = "ninguno",
                precio_unitario = 20,
                total = 40,
                id_precio = null,
                cantidad_secundaria = 0,
                indicadorMultiproposito = null,
                id_cuenta_contable = null,
                isc = 0,
                igv = 0,
                descuento = 0,
                lote = null,
                vencimiento = new DateTime(2020, 01, 1, 00, 00, 00),
                registro = null
            },true,"110", new List<ValorDetalleMaestroDetalleTransaccion>()
            {
                null
            }, new List<Complemento_Concepto_Negocio_Comercial>()
            {
                new Complemento_Concepto_Negocio_Comercial(1,10,"L1",null),
                new Complemento_Concepto_Negocio_Comercial(1,20,"L2",null)
            })
            };

            OperationResult resultado = new OperationResult();
            int idInventarioFisico = 0;

            UserProfileSessionData sesionDeUsuario = new UserProfileSessionData(){

            };

            RegistroDeVenta registroDeVenta = new RegistroDeVenta() {
                Vendedor = new ItemGenerico(),
                PuntoDeVenta = new ItemGenerico(),
                Cajero = new ItemGenerico(),
                Caja = new ItemGenerico(),
                Almacenero = new ItemGenerico(),
                Almacen = new ItemGenerico(),
                Cliente = new ComboActorComercial(idCliente, "", ""),
                Alias = "",
                TipoDeComprobante = new SelectorTipoDeComprobante()
                {
                    EsPropio = true,
                    TipoComprobante = new ItemGenerico(27, ""),
                    SerieSeleccionada = 968
                },
                EsVentaPasada = false,
                FechaRegistro = fechaRegistro,

                GrabaIgv = true,
                DetalleUnificado = false,
                Observacion = "",

                EsVentaACredito = true,
                EsCreditoRapido = true,
                Cuotas = null,
                Inicial = 0,

                Flete = 0,
                NumeroBolsasDePlastico = 0,
                Icbper = 0,

                //IdMedioDePago = 0,
                //EntidadFinanciera = new ItemGenerico(),
                //TipoTarjeta = new ItemGenerico(),
                //InformacionDeMedioPago = "",
                HayRegistroTrazaPago = false,

                HayRegistroMovimientoMercaderia = false,
                HaySalidaDeMercaderia = false,
                SalidasDeMercaderia = null,
                UsaComprobanteOrden = false,

                Detalles = null//detallesOperacion
            };

            resultado = _operacion.ConfirmarVentaIntegrada(ModoOperacionEnum.PorMostrador,sesionDeUsuario,registroDeVenta);

            Assert.IsNotNull(resultado);
        }*/
    }
}
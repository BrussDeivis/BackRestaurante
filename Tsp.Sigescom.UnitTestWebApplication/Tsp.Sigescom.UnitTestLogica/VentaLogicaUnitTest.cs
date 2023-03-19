using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.Interfaces.Logica;

namespace Tsp.Sigescom.UnitTestLogica
{
    [TestClass]
    public partial class VentaLogicaUnitTest
    {
        private readonly IOperacionLogica logica;

        static StreamReader ArchivoFechaL;
        static StreamWriter ArchivoFechaE;
        int idCentroAtencion = 4;
        
        DateTime fechaInicio = new DateTime(2014, 01, 01, 00, 00, 00);

                

        public VentaLogicaUnitTest()
        {
            logica = Dependencia.Resolve<IOperacionLogica>();
        }

        /// <summary>
        /// metodo para guardar una venta
        /// </summary>
        //[TestMethod]
        //public void GuardarVentaTestMethod()
        //{
        //    //idsEmpleados: 42042, 42046,42048, 42050, 42053, 42058, 42106, 42132, 42137, 42140
        //    //idsClientes: 42024, 42025, 42026, 42027, 42028, 42029, 42030, 42031, 42032, 42066, 42078, 42079, 42080, 42081, 42092, 42095, 42098, 42103
        //    //idsSeriesComprobantes:2:boleta, 3: FActura, 4. Nota Abono
        //    //idsMedioPago: 1. Efectivo,2. tarjeta 3. cheque
        //    //idsEntidadFinanciera: 870,871,872


        //    ArchivoFechaL = new StreamReader("ArchivoFecha.txt");
        //    DateTime fecha = DateTime.Parse(ArchivoFechaL.ReadLine());
        //    ArchivoFechaL.Close();

        //    int idEmpleado = 42042, idCliente= 42024, idSerieComprobante=2,idMedioDePago=1,idEntidadFinanciera=870;
        //    string Observacion = "ninguno",InformacionBancaria="1234567890";
        //    decimal Descuento = 0;
        //    Random aleatorio = new Random();
        //    List<Detalle_transaccion> detalles = obtenerDetalles();
        //    OperationResult result = logica.guardarRegistroDeVentaConFechaActual(idEmpleado, idCentroAtencion, idCliente, idSerieComprobante,
        //        Observacion,false,false,Descuento,idMedioDePago,idEntidadFinanciera,InformacionBancaria,detalles);

        //     Assert.AreEqual(OperationResultEnum.Success,result.code_result);
        //    ArchivoFechaE = new StreamWriter("ArchivoFecha.txt");
        //    ArchivoFechaE.WriteLine(fecha.AddMinutes(aleatorio.Next(1, 20)));
        //    ArchivoFechaE.Close();
        //}

        //private List<Detalle_transaccion> obtenerDetalles()
        //{
        //    Random aleatorio = new Random();
        //    int[] idsProductos = { 82 };

        //    decimal cantidad = aleatorio.Next(5, 30);
        //    decimal precio = aleatorio.Next(2, 40);
        //    decimal total=cantidad*precio;
           
        //    List<Detalle_transaccion> detalles = new List<Detalle_transaccion>();
        //    foreach (var id in idsProductos)
        //    {
        //        //detalles.Add(new Detalle_transaccion(cantidad,id,"",precio,total,null,0,null,null,0,0,0));
        //    }

        //    return detalles;
        //}

        [TestMethod()]
        public void ObtenerOrdenesDeSalidaDeMercaderiaDeVenta()
        {
            long idVenta = 1911115;
            
            var transacciones = logica.ObtenerSalidaDeMercaderiaDeVenta(idVenta);

            Assert.IsNotNull(transacciones);


        }
    }
}

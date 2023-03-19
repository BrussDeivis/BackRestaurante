using System;
using System.Text;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.Entidades;
using System.Collections.Generic;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using System.Linq;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;

namespace Tsp.Sigescom.UnitTestLogica
{
    [TestClass]
    public partial class CompraLogicaUnitTest
    {
        //private readonly IOperacionLogica logica;
        //private readonly ITransaccionRepositorio datos;

        //static StreamReader ArchivoFechaL;
        //static StreamWriter ArchivoFechaE;
        //int idCentroAtencion = 4;

        //DateTime fechaInicio = new DateTime(2014, 01, 01, 00, 00, 00);
        //int numeroDeComprobante =256;
        //private readonly IOperacionLogica _operacion;


        //public CompraLogicaUnitTest()
        //{
        //    _operacion = Dependencia.Resolve<IOperacionLogica>();
        //    datos = Dependencia.Resolve<ITransaccionRepositorio>();


        //}


        //[TestMethod]
        //public void ItemsMovimientoBienes()
        //{
        //    for (int i = 0; i < 30; i++)
        //    {
        //    //info: botica markos
        //    int idEmpleado = 64;
        //    int idCentroDeAtencion = 3;
        //    int idProveedor = 114;
        //    int tipoCompra = 1;
        //    int idTipoComprobante = 26;//factura
        //    int idSerieComprobante = 1039;
        //    bool esPropio = false;
        //    string numeroSerieDeComprobante = "Z002";
        //    numeroDeComprobante++;
        //    string observacion = "NINGUNA";
        //    DateTime fechaRegistro = DateTime.Now;
        //    decimal flete = 0;
        //    List<DetalleDeOperacion> detallesDeCompra = new List<DetalleDeOperacion>();

        //    DetalleDeOperacion detalle = new DetalleDeOperacion(new Detalle_transaccion()
        //    {
        //        //esBien = true,
        //        //mascaraDeCalculo = null,
        //        //complementos = null,
        //        id = 0,
        //        id_concepto_negocio = 3700,
        //        detalle = null,
        //        cantidad = 352.0m,
        //        cantidad_secundaria = 0,
        //        indicadorMultiproposito = null,
        //        id_cuenta_contable = null,
        //        isc = 0.0m,
        //        precio_unitario = 2.20m,
        //        igv = 0.0m,
        //        descuento = 0.00m,
        //        total = 774.40m,
        //        lote = "10503038",
        //        vencimiento = null,
        //        registro = null
                
        //    });
        //        detalle.esBien = true;
        //        detallesDeCompra.Add(detalle);
        //        var clonDetalle = detalle.Clone();
        //        clonDetalle.DetalleTransaccion().id_concepto_negocio=1755;
        //        clonDetalle.DetalleTransaccion().lote = "10115038";
        //        clonDetalle.esBien = true;
        //        detallesDeCompra.Add(clonDetalle);
        //    OperationResult resultado = new OperationResult();

        //    resultado = _operacion.ConfirmarCompra(idEmpleado, idCentroDeAtencion, idEmpleado, idCentroDeAtencion, idEmpleado, idCentroDeAtencion, idProveedor, tipoCompra, idTipoComprobante, idSerieComprobante, esPropio, numeroSerieDeComprobante, numeroDeComprobante, observacion, fechaRegistro, detallesDeCompra, flete);

        //    //comparar items de la orden con los del movimiento
        //    //orden:0, movimiento:2
        //    var detallesOrden = ((OrdenDeVenta)resultado.objeto).Transaccion().Transaccion2.Transaccion1.ElementAt(0).Detalle_transaccion.Count;
        //    var detallesMovimiento = ((OrdenDeVenta)resultado.objeto).Transaccion().Transaccion2.Transaccion1.ElementAt(2).Detalle_transaccion.Count;
        //    //var movimiento = resultado.objeto.
        //    Assert.AreEqual(detallesOrden,detallesMovimiento);
        //    }

        //}

        //[TestMethod]
        //public void ItemsMovimientoBienesClonando()
        //{
        //    for (int i = 0; i < 2; i++)
        //    {
        //        //info: botica markos
        //        int idEmpleado = 64;
        //        int idCentroDeAtencion = 3;
        //        int idProveedor = 114;
        //        int tipoCompra = 1;
        //        int idTipoComprobante = 26;//factura
        //        int idSerieComprobante = 1039;
        //        bool esPropio = false;
        //        string numeroSerieDeComprobante = "Z002";
        //        numeroDeComprobante++;
        //        string observacion = "NINGUNA";
        //        DateTime fechaRegistro = DateTime.Now;
        //        decimal flete = 0;
        //        List<DetalleDeOperacion> detallesDeCompra = new List<DetalleDeOperacion>();

        //        List<Detalle_transaccion> detallesTransaccion = new List<Detalle_transaccion>();
        //        detallesTransaccion = datos.ObtenerDetallesTransaccion(245427).ToList();
        //        foreach (var item in detallesTransaccion)
        //        {
        //            var clon = item.Clone();
        //            var nuevoDetalle = new DetalleDeOperacion(clon);
        //            detallesDeCompra.Add(nuevoDetalle);
        //            nuevoDetalle.esBien = true;
        //        }

        //        //Averiguar donde se determina si es bien o no es bien.... posiblemente puee estar fallando. tavez en el frontend.

        //        OperationResult resultado = new OperationResult();
        //        resultado = _operacion.ConfirmarCompra(idEmpleado, idCentroDeAtencion, idEmpleado, idCentroDeAtencion, idEmpleado, idCentroDeAtencion, idProveedor, tipoCompra, idTipoComprobante, idSerieComprobante, esPropio, numeroSerieDeComprobante, numeroDeComprobante, observacion, fechaRegistro, detallesDeCompra, flete);

        //        //comparar items de la orden con los del movimiento
        //        //orden:0, movimiento:2
        //        var detallesOrden = ((OrdenDeVenta)resultado.objeto).Transaccion().Transaccion2.Transaccion1.ElementAt(0).Detalle_transaccion.Count;
        //        var detallesMovimiento = ((OrdenDeVenta)resultado.objeto).Transaccion().Transaccion2.Transaccion1.ElementAt(2).Detalle_transaccion.Count;
        //        //var movimiento = resultado.objeto.
        //        Assert.AreEqual(detallesOrden, detallesMovimiento);
        //    }

        //}
        //[TestMethod()]
        //public void GuardarCompraTestLoteCaracteristias()
        //{
        //    int idProveedor = 74;
        //    int idCentroDeAtencion = 3;
        //    int idEmpleado = 37;
        //    int idTipoComprobante = 27;
        //    string numeroSerieDeComprobante = "B001";
        //    int numeroDeComprobante = 444;
        //    string observacion = "NINGUNA";
        //    DateTime fechaRegistro = new DateTime(2019, 12, 15, 00, 00, 00);
        //    decimal flete = 0;


        //    List<DetalleDeOperacion> detallesOperacion = new List<DetalleDeOperacion>()
        //    {
        //        new DetalleDeOperacion(new Detalle_transaccion()
        //    {
        //        cantidad = 2,
        //        id_concepto_negocio = 12,
        //        detalle = "ninguno",
        //        precio_unitario = 20,
        //        total = 40,
        //        id_precio = null,
        //        cantidad_secundaria = 0,
        //        indicadorMultiproposito = null,
        //        id_cuenta_contable = null,
        //        isc = 0,
        //        igv = 0,
        //        descuento = 0,
        //        lote = "lt1",
        //        vencimiento = new DateTime(2020, 01, 1, 00, 00, 00),
        //        registro = null
        //    },true,"110", new List<ValorDetalleMaestroDetalleTransaccion>()
        //    {
        //        new ValorDetalleMaestroDetalleTransaccion(new Valor_detalle_maestro_detalle_transaccion(1,14318,null,"2222")),
        //        new ValorDetalleMaestroDetalleTransaccion(new Valor_detalle_maestro_detalle_transaccion(2,14318,null,"3333"))
        //    })
        //    };

        //    OperationResult resultado = new OperationResult();

        //    resultado = _operacion.ConfirmarCompra(idEmpleado, idCentroDeAtencion, idEmpleado, idCentroDeAtencion, idEmpleado, idCentroDeAtencion, idProveedor, (int)TipoOperacionCompra.NoGravada, idTipoComprobante, 2, false, numeroSerieDeComprobante, numeroDeComprobante, observacion, fechaRegistro, detallesOperacion, flete);

        //    Assert.IsNotNull(resultado);
        //}
    }
}

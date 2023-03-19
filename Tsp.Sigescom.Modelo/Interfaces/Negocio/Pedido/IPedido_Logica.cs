using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Pedido;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Pedido.PlainModel;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Sesion;

namespace Tsp.Sigescom.Modelo.Interfaces.Negocio.Pedido
{
     public interface IPedido_Logica
    {
        PrincipalPedidoData ObetenerDatosParaPedidos(UserProfileSessionData profileSessionData);
        OperationResult AgregarPedido(DatosVentaIntegrada pedidoP, UserProfileSessionData profileSessionData);   
        List<ResumenOrdenPedido> ObtenerOrdenesDePedido(DateTime FechaDesde, DateTime FechaHasta);
        OperationResult InvalidarPedido(int IdOrdenPedido,string Observacion, UserProfileSessionData profileSessionData);
        DatosVentaIntegrada ObtenerOrdenDePedido(int idPedido);
        OperationResult EditarPedido(DatosVentaIntegrada datospedidoIntegrada, UserProfileSessionData profileSessionData);
        OrdenDePedido ObtenerOrdenDePedidoComprobante(long idPedido);
        OrdenDePedido ObtenerOrdenDePedidoParaImprimir(OrdenDePedido ordenDePedido);
        OperationResult ConfirmarPedido(ModoOperacionEnum tipoVenta, UserProfileSessionData sesionDeUsuario, DatosVentaIntegrada pedido);
        List<PedidosInvalidados> ObtenerReportePedidosInvalidados(DateTime fechaInicio, DateTime fechaFin, int[] idsPuntosVenta, bool todasLosPuntosVenta,int idEstablecimientoComercial);
    }
}

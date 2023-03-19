using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Pedido;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Pedido.PlainModel;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo.Interfaces.Datos.Pedido
{
    public interface IPedidoRepositorio
    {
        IEnumerable<ResumenOrdenPedido> ObtenerOrdenesPedidos(DateTime fechaDesde, DateTime fechaHasta, int idTipoTransaccion, int[] idEstados);
        Transaccion ObtenerOrdenDePedido(int idPedido);
        Transaccion ObtenerTransaccion(long id);
        IEnumerable<PedidosInvalidados> ObtenerOrdenesDePedidoInvalidados(DateTime fechaDesde, DateTime fechaHasta, int[] idsPuntosVenta);
    }
}

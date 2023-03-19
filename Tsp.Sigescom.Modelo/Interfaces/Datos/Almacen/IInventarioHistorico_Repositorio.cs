using System;
using System.Collections.Generic;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.Interfaces.Datos.Almacen
{
    public interface IInventarioHistorico_Repositorio
    {
        Transaccion ObtenerTransaccionInclusiveDetalleTransaccion(int idActorNegocioInterno, int idTipoTransaccion, int idUltimoEstado);

        InventarioFisico ObtenerUltimoInventarioFisicoHistoricoAnteriorA(int idAlmacen, int idConcepto, DateTime fecha);
        InventarioValorizado ObtenerUltimoInventarioValorizadoHistoricoAnteriorA(int idAlmacen, int idConcepto, DateTime fecha);
        IEnumerable<InventarioFisico> ObtenerUltimoInventarioFisicoHistoricoAnteriorA(int idAlmacen, DateTime anteriorA);
        IEnumerable<InventarioHistorico> ObtenerInventariosHistoricosDespuesDe(int idAlmacen, DateTime despuesDe);
        IEnumerable<InventarioValorizado> ObtenerUltimoInventarioValorizadoHistoricoAnteriorA(int idAlmacen, DateTime anteriorA);
        IEnumerable<InventarioFisico> ObtenerUltimoInventarioFisicoHistoricoAnteriorA(int idAlmacen, DateTime anteriorA, int[] familias);
        IEnumerable<InventarioValorizado> ObtenerUltimoInventarioValorizadoHistoricoAnteriorA(int idAlmacen, DateTime anteriorA, int[] familias);
        Transaccion ObtenerUltimoInventarioHistorico(int idAlmacen);
        InventarioConceptoNegocio ObtenerUltimoInventarioHistoricoAntesDe(int idAlmacen, int idConcepto, string lote, DateTime fecha);


    }
}

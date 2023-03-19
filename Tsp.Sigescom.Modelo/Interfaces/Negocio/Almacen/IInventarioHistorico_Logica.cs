using System;
using System.Collections.Generic;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Sesion;
using Tsp.Sigescom.Modelo.Negocio.Almacen.Report;

namespace Tsp.Sigescom.Modelo.Negocio.Almacen
{
    public interface IInventarioHistorico_Logica
    {
        OperationResult CrearInventariosLogicosHoy(int idEmpleado);
        OperationResult CrearInventariosLogicosPorLote(int idEmpleado);
        OperationResult CrearInventarioHistoricoClonandoInventarioFisico(int idEmpleado);
        DateTime ObtenerFechaDelUltimoInventarioHistorico(int idAlmacen);
        OperationResult CrearInventario(int idEmpleado, int idAlmacen, DateTime fecha);
        List<InventarioFisico> ObtenerInventariosFisicos(int idAlmacen, int idEmpleado, DateTime fecha);
        List<InventarioFisico> ObtenerInventariosFisicos(int idAlmacen, int idEmpleado, DateTime fecha, int[] familias);

        List<InventarioValorizado> ObtenerInventariosValorizados(int idEmpleado, int idAlmacen, DateTime fecha);
        List<InventarioValorizado> ObtenerInventariosValorizados(int idEmpleado, int idAlmacen, DateTime fecha, int[] familias);

        List<InventarioSemaforo> ObtenerInventariosSemaforo(int idEmpleado, int idAlmacen, DateTime fecha);
        List<InventarioSemaforo> ObtenerInventariosSemaforo(int idEmpleado, int idAlmacen, DateTime fecha, int[] familias);

        InventarioConceptoNegocio ObtenerInventarioHistoricoPorConceptoDeNegocio(int idAlmacen, int idConcepto, string lote, DateTime fechaHasta);
        Transaccion ObtenerUltimoInventarioLogico(int idAlmacen);



    }
}

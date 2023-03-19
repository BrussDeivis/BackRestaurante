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
    public interface IInventarioActual_Logica
    {
        List<Detalle_transaccion> ResolverYCrearDetallesDeInventarioActual(int idCentroDeAtencion);
        Transaccion GenerarInventarioActual(int idEmpleado, int idCentroDeAtencion);

        List<InventarioFisico> InventariosFisicosActuales(List<ItemGenericoBase> almacenes, bool todasLasFamilias, int[] idsFamilias);
        List<InventarioSemaforo> InventariosSemaforoActuales(List<ItemGenericoBase> almacenes, bool todasLasFamilias, int[] idsFamilias, bool estadoBajo, bool estadoNormal, bool estadoAlto);
        List<InventarioValorizado> InventariosValorizadosActuales(List<ItemGenericoBase> almacenes, bool todasLasFamilias, int[] idsFamilias);
        List<Reporte_Inventario_Valorizado> ObtenerInventarioValorizadoActual(int idCentroDeAtencion, int idCentroAtencionPrecios, int[] idsConceptosBasicos, int[] idsValoresDeCaracteristicas);
        //List<Reporte_Inventario_Valorizado> ObtenerInventarioValorizado(DateTime fecha, int idCentroDeAtencion, int[] idsConceptosBasicos, int[] idsValoresDeCaracteristicas);
        OperationResult CrearInventarioActual(int idAlmacen, int idEmpleado);


        Dictionary<long, long> ObtenerIdsAlmacenIdsInventarioActual();
        OperationResult CrearDetallesInventariosActualesDeConceptosDeNegocioDelConceptoBasico(int idConcepto);
        OperationResult EliminarDetallesInventariosActualesDeConceptosDeNegocioDelConceptoBasico(int idFamilia);

    }
}

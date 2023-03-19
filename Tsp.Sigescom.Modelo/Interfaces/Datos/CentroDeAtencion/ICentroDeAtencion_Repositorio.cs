using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Custom;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.SigesRestaurant;

namespace Tsp.Sigescom.Modelo.Interfaces.Datos.CentrosDeAtencion

{
    public interface ICentroDeAtencion_Repositorio
    {
        List<CentroDeAtencionExtendido> ObtenerCentrosDeAtencionProgramados(int idEmpleado);
        List<CentroDeAtencion> ObtenerCentrosDeAtencionProgramados_(int idEmpleado);

        Actor_negocio ObtenerActorDeNegocio(int id);
        CentroDeAtencionExtendido _ObtenerCentroDeAtencion(int id);
        CentroDeAtencion ObtenerCentroDeAtencion_(int id);
        IEnumerable<CentroDeAtencion> ObtenerCentrosDeAtencionVigentes();
        IEnumerable<CentroDeAtencion> ObtenerCentrosDeAtencionConPrecioDeCadaEstablecimientoVigente();
        IEnumerable<CentroDeAtencion> ObtenerCentrosDeAtencionSegunRolHijo(int idRolHijo, bool esVigente);
        
        
        IEnumerable<ItemGenerico> ObtenerCentrosDeAtencionComoItemsGenericosSegunRolHijo(int idRolHijo, bool esVigente);
        IEnumerable<ItemGenerico> ObtenerCentrosDeAtencionComoItemsGenericosSegunRolesHijos(int[] idsRolesHijo, bool esVigente);


        IEnumerable<CentroDeAtencionExtendido> ObtenerCentrosDeAtencionExtendidosSegunRolHijo(int idRolHijo, bool esVigente);

        IEnumerable<CentroDeAtencionExtendido> ObtenerCentrosDeAtencionExtendidosVigentes();
        CentroDeAtencionExtendido ObtenerCentroDeAtencionExtendidosSegunSerieComprobante(int idSerieComprobante);

        IEnumerable<CentroDeAtencionExtendido> ObtenerCentrosDeAtencionExtendidosVigentesPorEstablecimientoComercial(int idEstablecimiento);

        IEnumerable<CentroDeAtencionExtendido> ObtenerCentrosDeAtencionExtendidosVigentesSegunRolYEstablecimientoComercial(int idRol, int idEstablecimiento);

        int ObtenerIdDelCentroDeAtencionQueTieneLosPreciosSegunIdCentroDeAtencion(int idCentroAtencion);

        int ObtenerIdDelCentroDeAtencionQueTieneElStockSegunIdCentroDeAtencion(int idCentroAtencion);

        string ObtenerNombreDeCentroDeAtencion(int idCentroAtencion);

        int[] ObtenerIdsDeCentrosDeAtencionVigentesSegunRolYEstablecimientoComercial(int idRol, int idActorNegocioPadre);
        IEnumerable<CentroDeAtencionExtendido> ObtenerCentrosDeAtencionExtendidosVigentesSegunRolIdsDeEstablecimientos(int idRol, List<int> idsEstablecimientos);
        CentroDeAtencionExtendido ObtenerCentroDeAtencionExtendidoPorIdDeCentroDeAtencionEIdDeEstablecimiento(int idCentroAtencion, int idEstablecimiento);

        bool TieneInventarioActual(int idActorNegocioInterno);
        OperationResult ActualizarDocumentoIdentidadDeTodosLosCentrosDeAtencionVigentes(string documentoIdentidad);



    }
}

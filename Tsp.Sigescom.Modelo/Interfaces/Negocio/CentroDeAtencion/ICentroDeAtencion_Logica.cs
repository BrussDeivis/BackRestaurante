using System.Collections.Generic;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Custom;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Sesion;

namespace Tsp.Sigescom.Modelo.Interfaces.Logica
{
    public interface ICentroDeAtencion_Logica
    {
        List<CentroDeAtencionExtendido> ObtenerCentrosDeAtencionProgramados(int idEmpleado);
        List<CentroDeAtencion> ObtenerCentrosDeAtencionProgramados_(int idEmpleado);
        CentroDeAtencionExtendido ObtenerCentroDeAtencion(int id);

        int[] ObtenerIdsCentrosAtencionConRolPuntoVenta(int idEstablecimientoComercial);


        /// <summary>
        /// Obtiene el nombre del actor segun el id_actor_negocio
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        string ObtenerNombreDeCentroDeAtencion(int idActorNegocio);

        int ObtenerIdCentroDeAtencionParaObtencionDePrecios(int idCentroAtencion);

        int ObtenerIdCentroDeAtencionParaObtencionDeStock(ModoOperacionEnum tipoDeVenta, int idCentroAtencion);

        int ObtenerIdCentroDeAtencionParaObtencionDePrecios(CentroDeAtencion centroDeAtencion, EstablecimientoComercialExtendido establecimiento);
        int ObtenerIdCentroDeAtencionParaObtencionDeStock(ModoOperacionEnum tipoDeVenta, CentroDeAtencion centroDeAtencion, EstablecimientoComercialExtendidoConLogo establecimiento);
        List<RolDeNegocio> ObtenerRolesDeCentroDeAtencion();

        List<DetalleGenerico> ObtenerPuntosDePrecio();


        List<CentroDeAtencionExtendido> ObtenerCajasVigentes();

        CentroDeAtencion ObtenerCentroDeAtencion_(int idCentroAtencion);

        CentroDeAtencionExtendido ObtenerCentroDeAtencionSucursalOSede(int idCentroDeAtencion, int idActorDeNegocioPadre);

        OperationResult ActualizarCentroDeAtencion(int idEmpleado, int idActor, int idCentroDeAtencion, string codigo, string nombre, bool salidaBienesSinStock, List<int> idRoles, int idCentroDeAtencionPadre);

        OperationResult DarDeBajaCentroDeAtencion(int idCentroDeAtencion);

        CentroDeAtencionExtendido ObtenerCentroDeAtencionSegunSerieComprobante(int idSerie);

        OperationResult EstablecerCentroDeAtencionParaPreciosYStockDeEstablecimientoComercial(int idEstablecimientoComencial, int idCentroDeAtencionPrecios, int idCentroDeAtencionStock);

        OperationResult CrearCentroDeAtencion(int idEmpleado, string codigo, string nombre, bool salidaBienesSinStock, List<int> idRoles, int idCentroDeAtencionPadre);

        List<CentroDeAtencionExtendido> ObtenerCentrosDeAtencionExtendidosVigentes();

        List<CentroDeAtencion> ObtenerCentrosDeAtencionVigentes();

        List<CentroDeAtencionExtendido> ObtenerPuntosDeVentaNoVigentes();

        List<CentroDeAtencionExtendido> ObtenerPuntosDeCompraNoVigentes();

        List<CentroDeAtencionExtendido> ObtenerAlmacenesNoVigentes();

        List<CentroDeAtencionExtendido> ObtenerPuntosDeVentaVigentes();

        List<CentroDeAtencionExtendido> ObtenerPuntosDeCompraVigentes();

        List<CentroDeAtencionExtendido> ObtenerAlmacenesVigentes();

        List<CentroDeAtencionExtendido> ObtenerCentrosDeAtencionVigentesPorEstablecimientoComercial(int idEstablecimientoComercial);

        List<CentroDeAtencionExtendido> ObtenerPuntosDeVentaVigentesPorEstablecimientoComercial(int idEstablecimientoComercial);

        List<CentroDeAtencionExtendido> ObtenerPuntosDeCompraVigentesPorEstablecimientoComercial(int idEstablecimientoComercial);

        List<CentroDeAtencionExtendido> ObtenerAlmacenesVigentesPorEstablecimientoComercial(int idEstablecimientoComercial);

        List<CentroDeAtencionExtendido> ObtenerCajasVigentesPorEstablecimientoComercial(int idEstablecimientoComercial);

        List<CentroDeAtencionExtendido> ObtenerPuntosDeVentaVigentesPorEstablecimientosComerciales(List<int> idsEstablecimientosComerciales);

        List<CentroDeAtencionExtendido> ObtenerPuntosDeCompraVigentesPorEstablecimientosComerciales(List<int> idsEstablecimientosComerciales);

        List<CentroDeAtencionExtendido> ObtenerAlmacenesVigentesPorEstablecimientosComerciales(List<int> idsEstablecimientosComerciales);

        List<CentroDeAtencionExtendido> ObtenerCajasVigentesPorEstablecimientosComerciales(List<int> idsEstablecimientosComerciales);
    }
}


using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Custom;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.SigesRestaurant;

namespace Tsp.Sigescom.Modelo.Interfaces.Datos.Establecimientos

{
    public interface IEstablecimiento_Repositorio
    {
        IEnumerable<EstablecimientoComercial> ObtenerEstablecimientosComercialesVigentes();
        IEnumerable<EstablecimientoComercialExtendido> ObtenerEstablecimientosComercialesExtendidosVigentes();
        EstablecimientoComercial ObtenerEstablecimientoComercial(int id);
        ItemGenerico ObtenerEstablecimientoComercialComoItemGenerico(int id);
        EstablecimientoComercialExtendido ObtenerEstablecimientoComercialExtendido(int id);
        IEnumerable<ItemGenerico> ObtenerEstablecimientosComercialesVigentesComoItemsGenericos();
        IEnumerable<ItemGenericoConSubItems> ObtenerEstablecimientosConSusCentrosDeAtencionVigentesSegunRol(int idRolDeCentroDeAtencion);
        Parametro_actor_negocio ObtenerParametroCentroDeAtencionParaObtencionPrecios(int idEstablecimientoComencial);
        Parametro_actor_negocio ObtenerParametroCentroDeAtencionParaObtencionDeStock(int idEstablecimientoComencial);
        EstablecimientoComercialConLogo ObtenerEstablecimientoComercialConLogo(int idEstablecimiento);
        EstablecimientoComercialExtendidoConLogo ObtenerEstablecimientoComercialExtendidoConLogo(int idEstablecimiento);

    }
}

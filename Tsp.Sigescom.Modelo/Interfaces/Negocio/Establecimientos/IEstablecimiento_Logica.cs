using System.Collections.Generic;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Sesion;

namespace Tsp.Sigescom.Modelo.Negocio.Establecimientos
{
    public interface IEstablecimiento_Logica
    {
        List<ItemGenerico> ObtenerEstablecimientosComercialesVigentesComoItemsGenericos();
        List<ItemGenericoConSubItems> ObtenerEstablecimientosComercialesVigentesConSusAlmacenes();
        List<ItemGenericoConSubItems> ObtenerEstablecimientosComercialesVigentesConSusCajas();
        List<ItemGenericoConSubItems> ObtenerEstablecimientosComercialesVigentesConSusPuntosVentas();
        List<EstablecimientoComercial> ObtenerEstablecimientosComercialesVigentes();
        List<EstablecimientoComercialExtendido> ObtenerEstablecimientosComercialesExtendidosVigentes();



    }
}


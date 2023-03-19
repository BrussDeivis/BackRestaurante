using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Custom;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.SigesRestaurant;

namespace Tsp.Sigescom.Modelo.Interfaces.Datos.Actores
{
    public interface IConsultaActor_Repositorio
    {
        ActorComercial_ ObtenerActorComercial_(int idRol, int idActorComercial);
        ActorComercial_ ObtenerActorComercial_(int idRol, string numeroDocumento);
        ActorComercial_ ObtenerActorComercial_(int idRol, int idTipoDocumento, string numeroDocumento);
    }
}

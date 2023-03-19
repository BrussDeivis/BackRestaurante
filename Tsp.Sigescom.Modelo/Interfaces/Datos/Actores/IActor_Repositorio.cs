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
    public interface IActor_Repositorio
    {

        /// <summary>
        /// caduca al actor negocio en el sistema
        /// </summary>
        /// <param name="IdActorNegocio"></param>
        /// <param name="idRol"></param>
        /// <returns></returns>
        OperationResult DarDeBajaActorNegocioAhora(int IdActorNegocio, int idRol);
        OperationResult CrearParametroActorNegocio(Parametro_actor_negocio parametroActorNegocio);
        OperationResult ActualizarParametroActorNegocio(Parametro_actor_negocio parametroActorNegocio);
        OperationResult CrearActorNegocio(Actor_negocio actorNegocio);
        OperationResult CrearActorNegocioActualizandoActor(Actor_negocio actorNegocio);
        OperationResult ActualizarActorNegocio(Actor_negocio actorNegocio);
        OperationResult ActualizarActorNegocioCreandoActor(Actor_negocio updActorNegocio);
        OperationResult ActualizarActorNegocioYIdActorNegocioPadre(Actor_negocio updActorNegocio);
        bool ExisteDocumento(int idTipoDocumentoIdentidad, string numeroDocumentoIdentidad);

        OperationResult ActualizarActorNegocioSinTomarEnCuentaAParametros(Actor_negocio updActorNegocio);
        Actor_negocio ObtenerActorDeNegocio(int id);
        bool ExisteActorConElMismoDocumentoYDistintoId(int id, string documentoIdentidad);
        bool ExisteActorComercialConElMismoDocumentoVigente(int idRol, string documentoIdentidad);
        bool ExisteActorComercialConElMismoDocumentoVigente(int idActorComercialDiferente, int idRol, string documentoIdentidad);

        ActorComercial_ ObtenerActorComercial(int id);
        bool ActorParticipaEnTransacciones(int idActorComercial);
        IEnumerable<Actor_negocio> ObtenerActorDeNegocioPrincipalVigentes(int idParentRol, int idRol);
        OperationResult ActualizarActor(Actor actor);

        /// <summary>
        /// Retorna una coleccion de actores de negocio vigentes por rol (Cliente, empleado y proveedor)
        /// </summary>
        /// <param name="idRol"></param>
        /// <returns></returns>
        IEnumerable<Actor_negocio> ObtenerActorDeNegocioPorRolVigentesAhora(int idRol);
        //IEnumerable<Actor_negocio> ObtenerActorDeNegocioPrincipalVigentes(int idParentRol, int idRol, int idActorNegocioPadre);
        OperationResult InvertirEsVigenteActorNegocio(int idActorNegocio);
        OperationResult InvertirIndicador1ActorNegocio(int idActorNegocio);
        OperationResult ActualizarActorNegocioIncluidoActor(Actor_negocio updActorNegocio);
        int[] ObtenerIdsActorDeNegocioVigentePrincipal(int idRolPadre, int idRolAlmace);
        string ObtenerExtensionJsonDeActorNegocio(int idActorNegocio);
        IEnumerable<ResumenCliente> ObtenerResumenClientesVigentes();
    }
}

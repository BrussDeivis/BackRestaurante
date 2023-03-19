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
    public interface IVinculoActor_Repositorio
    {

        OperationResult CaducarVinculoActorNegocio(int idVinculo, DateTime fechaCaducidad);
        bool ExisteVinculoVigente(int idActorNegocioVinculado, int idActorNegocioPrincipal);

        /// <summary>
        /// devuelve todos los registros de vinculo_Actor_Negocio donde idActorNegocioPrincipal es el actor principal
        /// estos registros deberan estar vigentes a la fecha
        /// </summary>
        /// <param name="idActorNegocioPrincipal"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        IEnumerable<Vinculo_Actor_Negocio> ObtenerVinculosActorNegocioParaActorPrincipal(int idActorNegocioPrincipal, DateTime fecha, TipoVinculo tipo);

        OperationResult CrearVinculoActorNegocio(Vinculo_Actor_Negocio vinculoActorNegocio);

        bool ExisteCodigoGrupoClientes(int enumTipoVinculo, string codigoGrupoClientes);
        bool ExisteNombreGrupoClientesEnGruposClientesVigentes(int enumTipoVinculo, string nombreGrupoClientes);
        bool ExisteCodigoGrupoClientesExceptoGrupoClientes(int enumTipoVinculo, string codigoGrupoClientes, int idGrupoClientes);
        bool ExisteNombreGrupoClientesEnGruposClientesVigentesExceptoGrupoClientes(int enumTipoVinculo, string nombreGrupoClientes, int idGrupoClientes);
        bool ExisteDeudaDeClienteEnOperacionesVentaConGrupoClientes(int idCliente, int idGrupoClientes);
        bool ExisteDeudaDeGrupoClientesEnOperacionesVenta(int idGrupoClientes);
        OperationResult ActualizarActorPrincipalConVinculosActorNegocio(Actor_negocio actorNegocioConVinculos);
        IEnumerable<GrupoClientesResumen> ObtenerGruposClientes();
        GrupoClientes ObtenerGrupoClientes(int enumTipoVinculo, int idGrupoCliente);
        IEnumerable<ItemGenerico> ObtenerGruposActoresComerciales(int[] idsRolesGrupos); 
         IEnumerable<ItemGenerico> ObtenerGruposActoresComercialesPorRol(int idRolGrupo);
         IEnumerable<ItemGenerico> ObtenerActoresComercialesDeGrupoActoresComercialesPorRol(int enumTipoVinculo, int idRolGrupo, int idGrupoActoresComerciales);
         IEnumerable<ItemGenerico> ObtenerGruposActoresComercialesPorRolDeActorComercial(int enumTipoVinculo, int idRolGrupo, int idActorComercial);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Custom;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.Interfaces.Repositorio
{
   public interface IActorRepositorio
    {
        #region crear y actualizar



       
        /// <summary>
        /// crea un nuevo actor de negocio y actualiza los datos del actor
        /// </summary>
        /// <param name="actorDeNegocio"></param>
        /// <returns></returns>
        OperationResult CrearActorNegocioActualizandoActor(Actor_negocio actorDeNegocio);
        string obtenerDenominacionClase(int idTipoActor);




        OperationResult establecerIdUsuario(string idUsuario, int idActorNegocio);


        /// <summary>
        /// solo se actualizan las direcciones, no se eliminan
        /// </summary>
        /// <param name="updActorNegocio"></param>
        /// <returns></returns>
        OperationResult actualizarActorNegocioYDireccion(Actor_negocio updActorNegocio);
        #endregion
        #region Consultas

        /// <summary>
        /// retorna una lista de actores de negocio por rol 
        /// </summary>
        /// <param name="idRol"></param>
        /// <returns></returns>
        IEnumerable<Actor_negocio> obtenerActoresDeNegocioPorRolInclusiveActorYRoles(int idRol);

        /// <summary>
        /// Devuelve un actor de negocio en un rol especifico 
        /// Si no se encuentra un actor de negocio con el id indicado en el rol indicado, devuelve NULL
        /// </summary>
        /// <param name="id">Id del actor de negocio</param>
        /// <param name="idRol"></param>
        /// <returns></returns>
        Actor_negocio obtenerActorDeNegocio(int id, int idRol);

        int ObtenerIdActor(string documentoIdentidad, int idTipoDocumentoIdentidad);

        Actor_negocio ObtenerActorDeNegocioVigente(int idActorNegocio, int idRol);


        IEnumerable<Actor_negocio> ObtenerActoresDeNegocio(int[] idsActoresNegocio);

        Actor_negocio ObtenerActorDeNegocioInclusiveDireccionesYTipoDocumentoIdentidadYParametros(int id);
        Custom.EstablecimientoComercialConLogo_ ObtenerEstablecimiento(int id);


        /// <summary>
        /// verifica la existencia de un actor a partir de su documento de identidad
        /// </summary>
        /// <param name="documentoIdentidad"></param>
        /// <returns>True: si existe, False: NoExiste</returns>
        bool ExisteActorDeNegocio(string documentoIdentidad);

        /// <summary>
        /// indica si existe un cliente vigente (a la fecha de vigencia indicada)para el documento de identidad indicado.
        /// </summary>
        /// <param name="documentoIdentidad"></param>
        /// <param name="fechaVigencia"></param>
        /// <returns></returns>
        bool ExistActorDeNegocio(string documentoIdentidad, DateTime fechaVigencia);
        

        bool actorDeNegocioEsVigente(int idProveedor);

        /// <summary>
        /// verifica si existe un actor
        /// </summary>
        /// <param name="idTipoPersona"></param>
        /// <param name="numeroDocumento"></param>
        /// <param name="idRol"></param>
        /// <returns></returns>
        RespuestaVerificacionActorNegocio verificarActor(int idTipoPersona, string numeroDocumento, int idRol);


        /// <summary>
        /// a partir del id de usuario y del tipo de transaccion, devuelve un arreglo co los ids de las acciones permitidas
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="idTipoTransaccion"></param>
        /// <returns></returns>
        int[] obtenerAccionesPosibles(string idUsuario, int idTipoTransaccion);


        Actor_negocio obtenerActorDeNegocio(string idUsuario, int idRolEmpleado);

        /// <summary>
        /// entendiendose que el codigo es un valor numérico representadocomo string, devuelve el maximo valor para dicho campo.
        /// </summary>
        /// <param name="idRol"></param>
        /// <param name="idDesde">id a partir del cual debe considerar los registros para obtener el codigo maximo</param>
        /// <returns></returns>
        string obtenerMaximoCodigo(int idRol/*, int idDesde*/);

        /// <summary>
        /// Devuelve un actor de negocio vigente(en base a la fecha de vigencia) en un rol especifico 
        /// A partir de su documento de identidad
        /// </summary>
        /// <param name="documentoIdentidad"></param>
        /// <returns>Actor_negocio. NULL: Si no existe  </returns>
        Actor_negocio ObtenerActorDeNegocioVigentePorDocumentoIdentidad(string documentoIdentidad, int idRol);
        IEnumerable<SelectorActorComercial> ObtenerActoresComercialesVigentesPorRolYBusqueda(int idRol, string cadenaBusqueda);
        IEnumerable<ItemGenerico> ObtenerActoresComercialesPorCentroDeAtencion(int idCentroDeAtencion);

        //Actor_negocio obtenerActorDeNegocioPorId(int idActorNegocio);




        Actor_negocio obtenerActorDeNegocioConRoles(int id, int idRol);



        /// <summary>
        /// devuelve los actores de negocio pertenecientes a un rol de actor de negocio y que ademas se encuentren asociado a otro rol dado
        /// </summary>
        /// <param name="idRolActorNegocio">Rol de actor de negocio</param>
        /// <param name="idRolEn">otro rol asociado</param>
        /// <returns></returns>
        IEnumerable<Actor_negocio> obtenerActorDeNegocioVigentesAhoraEnRol(int idRolActorNegocio, int idRolEn);
        IEnumerable<Actor_negocio> obtenerActorDeNegocioVigentesAhoraEnRoles(int idRolActorNegocio, int[] idsRolesEn);






        IEnumerable<Actor_negocio> obtenerActorDeNegocioPorRol(int idRol);

        IEnumerable<Actor_negocio> obtenerActorDeNegocioPorRol(int idRol, DateTime fechaVigencia);

        IEnumerable<Actor_negocio> ObtenerActorDeNegocioPrincipal(int idParentRol, int idRol, bool esVigente);




        
        
        int[] ObtenerIdsActorDeNegocioPrincipal(int idRolPadre, int idRolAlmace);


         IEnumerable<Actor_negocio> obtenerActorDeNegocioIncluidoActorPorRolVigentesAhora(int idRol);
        Task<IEnumerable<ItemGenerico>> ObtenerActoresDeNegocioPrincipalesVigentesComoItemsGenericos(int idParentRol, int idRol);
        Task<IEnumerable<ItemGenerico>> ObtenerActoresDeNegocioPrincipalesVigentesComoItemsGenericosPorIdRol(int idRol);

        List<Actor_negocio> obtenerActoresDeNegocioPrincipalPorRolVigentesAhoraDatosBasicos(int idRol);



        Actor_negocio ObtenerActorDeNegocioPorId(int id, int idRolEmpleado);

        int obtenerIdActor(int id);
        int obtenerIdDireccion(int idActor);
        bool ExisteNumeroDocumentoIdDetalleMultiPropositoActor(int idDetalleMultiproposito, string numeroDocumento, string primerNombre);
        bool ExisteNumeroDocumentoIdDetalleMultiPropositoActor(int idActor, int idDetalleMultiproposito, string numeroDocumento, string primerNombre);
        /// <summary>
        /// Devuelve una lista de Actores de Negocio asiociados al Rol de negocio principal que se corresponde con un rol dado
        /// </summary>
        /// <param name="idRol">Rol dado</param>
        /// <returns></returns>
        IEnumerable<Actor_negocio> obtenerActoresDeNegocioPrincipalPorRol(int idRol);


        /// <summary>
        /// devuelve una coleccion de clases de actor segun el tipo de actor
        /// </summary>
        /// <param name="idTipoActor"></param>
        /// <returns></returns>
        IEnumerable<Clase_actor> obtenerClasesDeActor(int idTipoActor);

        /// <summary>
        /// devuelve una coleccion de estado legal segun el tipo de actor
        /// </summary>
        /// <param name="idTipoActor"></param>
        /// <returns></returns>
        IEnumerable<Estado_legal> obtenerEstadosLegales(int idTipoActor);

        /// <summary>
        /// devuelve los tipos de actor
        /// </summary>
        /// <returns></returns>
        IEnumerable<Tipo_actor> obtenerTiposDeActor();


        #endregion

        IEnumerable<Actor_negocio> ObtenerActorDeNegocioIncluidoTransaccion1PorRolVigentesAhora(int idRol);
        #region Crear, Obtener, Actualizar Vinculo Actor De Negocio
        Vinculo_Actor_Negocio ObtenerVinculoActorNegocioSegunElTipoDeVinculo(int idTurno, int tipoVinculo);
        OperationResult CrearVinculosActorNegocio(List<Vinculo_Actor_Negocio> carteraDeClientes);
        OperationResult ActualizarVinculosActorNegocio(int idActorNegocioPrincipal, List<Vinculo_Actor_Negocio> carteraDeClientes, int tipovinculo);
        IEnumerable<Vinculo_Actor_Negocio> ObtenerVinculosActorNegocioSegunElTipoDeVinculo(int tipoVinculo);
        OperationResult ActualizarVinculoActorNegocio(Vinculo_Actor_Negocio vinculoActorNegocio);
        IEnumerable<Vinculo_Actor_Negocio> ObtenerVinculosActorDeNegocio(int tipoVinculo);
        IEnumerable<Vinculo_Actor_Negocio> ObtenerVinculosActorDeNegocio(int idActorNegocioPrincipal, int tipoVinculo);
        IEnumerable<Actor_negocio> ObtenerActoresDeNegocioSegunActorNegocioPrincipalYElTipoDeVinculoIncluyendoCuotas(int idActorNegocioPrincipal, int tipoVinculo);
        IEnumerable<int> ObtenerIdsActoresDeNegocioPrincipalDeVinculoActorNegocio(int tipoVinculo);
        #endregion
        Task<IEnumerable<Actor_negocio>> ObtenerActoresDeNegocioVigentesPorIdActorNegocioPadre(int idActorNegocioPadre);


        Task<IEnumerable<Actor_negocio>> ObtenerActoresDeNegocioPorRolVigentes(int idRol);
        Task<IEnumerable<Actor_negocio>> ObtenerActoresDeNegocioPorRolVigentes(int idRol, int idActorNegocioPadre);


        IEnumerable<CuentaBancaria> ObtenerCuentasBancarias();
        IEnumerable<CuentaBancaria> ObtenerCuentasBancariasPorEntidadFinanciera(int idEntidadFinanciera);
        IEnumerable<ItemGenerico> ObtenerCuentasBancariasConEntidadFinancieraConMoneda();
    }

}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Custom;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.Interfaces.Logica
{
    public interface IActorNegocioLogica
    {
        #region Crear Actores de Negocio
        /// <summary>
        /// Crea un nuevo Cliente
        /// </summary>
        /// <param name="idTipoPersona"></param>
        /// <param name="razonSocial"></param>
        /// <param name="nombreComercial"></param>
        /// <param name="nombreCorto"></param>
        /// <param name="idTipoDocumentoIdentidad"></param>
        /// <param name="numeroDocumentoIdentidad"></param>
        /// <param name="idClaseActor"></param>
        /// <param name="idEstadoLegalActor"></param>
        /// <param name="correo"></param>
        /// <param name="telefono"></param>
        /// <param name="direcciones"></param>
        /// <returns></returns>
        OperationResult CrearCliente(int idTipoPersona, string razonSocial, string apellidoPaterno, string apellidoMaterno, string nombres, string nombreComercial, string nombreCorto, int idTipoDocumentoIdentidad, string numeroDocumentoIdentidad, int? idClaseActor, int? idEstadoLegalActor, string correo, string telefono, List<Direccion> direcciones, int idComprobantePredeterminado);
        ActorComercial_ ObtenerClienteGenerico();
        OperationResult CrearCliente(RegistroActorComercial cliente);

        /// <summary>
        /// crea un nuevo cliente actualizando datos de un actor
        /// </summary>
        /// <param name="idActor"></param>
        /// <param name="idTipoActor"></param>
        /// <param name="razonSocial"></param>
        /// <param name="nombreComercial"></param>
        /// <param name="nombreCorto"></param>
        /// <param name="idTipoDocumentoIdentidad"></param>
        /// <param name="numeroDocumentoIdentidad"></param>
        /// <param name="idClaseActor"></param>
        /// <param name="idEstadoLegalActor"></param>
        /// <param name="correo"></param>
        /// <param name="telefono"></param>
        /// <param name="direcciones"></param>
        /// <returns></returns>
        OperationResult CrearClienteActualizandoActor(int idActor, int idTipoActor, string razonSocial, string apellidoPaterno, string apellidoMaterno, string nombres, string nombreComercial, string nombreCorto, int idTipoDocumentoIdentidad, string numeroDocumentoIdentidad, int? idClaseActor, int? idEstadoLegalActor, string correo, string telefono, List<Direccion> direcciones, int idComprobantePredeterminado);

        /// <summary>
        /// crea un nuevo proveedor
        /// </summary>
        /// <param name="idTipoPersona"></param>
        /// <param name="razonSocial"></param>
        /// <param name="nombreComercial"></param>
        /// <param name="nombreCorto"></param>
        /// <param name="idTipoDocumentoIdentidad"></param>
        /// <param name="numeroDocumentoIdentidad"></param>
        /// <param name="idClaseActor"></param>
        /// <param name="idEstadoLegalActor"></param>
        /// <param name="correo"></param>
        /// <param name="telefono"></param>
        /// <param name="direcciones"></param>
        /// <returns></returns>
        OperationResult CrearProveedor(int idTipoPersona, string razonSocial, string apellidoPaterno, string apellidoMaterno, string nombres, string nombreComercial, string nombreCorto, int idTipoDocumentoIdentidad, string numeroDocumentoIdentidad, int? idClaseActor, int? idEstadoLegalActor, string correo, string telefono, List<Direccion> direcciones);
        ActorComercial_ ObtenerActorComercialCreandoloSiExisteSoloComoActor(int idRol, string documento);
        ActorComercial_ ObtenerActorComercialPorId(int idRol, int id);

        ActorComercial_ ObtenerActorComercialCreandoloSiExisteSoloComoActor(int idRol, int idTipoDocumento, string documento);
        ActorComercial_ ResolverYObtenerActorComercial(int idRol, RegistroActorComercial registroActorComercial);
        List<SelectorActorComercial> ObtenerActoresComercialesVigentesPorRolYBusqueda(int idRol, string cadenaBusqueda);
        List<ItemGenerico> ObtenerActoresComercialesPorCentroDeAtencion(int idCentroDeAtencion);
        /// <summary>
        /// crea un nuevo proveedor actualizando datos de un actor
        /// </summary>
        /// <param name="idActor"></param>
        /// <param name="idTipoActor"></param>
        /// <param name="razonSocial"></param>
        /// <param name="nombreComercial"></param>
        /// <param name="nombreCorto"></param>
        /// <param name="idTipoDocumentoIdentidad"></param>
        /// <param name="numeroDocumentoIdentidad"></param>
        /// <param name="idClaseActor"></param>
        /// <param name="idEstadoLegalActor"></param>
        /// <param name="correo"></param>
        /// <param name="telefono"></param>
        /// <param name="direcciones"></param>
        /// <returns></returns>
        OperationResult CrearProveedorActualizandoActor(int idActor, int idTipoActor, string razonSocial, string apellidoPaterno, string apellidoMaterno, string nombres, string nombreComercial, string nombreCorto, int idTipoDocumentoIdentidad, string numeroDocumentoIdentidad, int? idClaseActor, int? idEstadoLegalActor, string correo, string telefono, List<Direccion> direcciones);
        ItemGenerico DeterminarTipoDeDocumentoDeIdentidad(string documento);

        /// <summary>
        /// crea un nuevo empleado
        /// </summary>
        /// <param name="idTipoDocumentoIdentidad"></param>
        /// <param name="numeroDocumentoIdentidad"></param>
        /// <param name="apellidoPaterno"></param>
        /// <param name="apellidoMaterno"></param>
        /// <param name="nombres"></param>
        /// <param name="correo"></param>
        /// <param name="idClaseActor"></param>
        /// <param name="idEstadoLegalActor"></param>
        /// <param name="fechaNacimiento"></param>
        /// <param name="telefono"></param>
        /// <param name="roles"></param>
        /// <param name="direcciones"></param>
        /// <returns></returns>
        OperationResult CrearEmpleado(string codigo, int idTipoDocumentoIdentidad, string numeroDocumentoIdentidad, string apellidoPaterno, string apellidoMaterno, string nombres, string correo, int idClaseActor, int idEstadoLegalActor, DateTime fechaNacimiento, string telefono, List<int> roles, List<Direccion> direcciones);
        //OperationResult CrearActorComercial(int idRol, ActorComercial_ actorComercial);

        /// <summary>
        /// crea un nuevo empleado actualizando datos de un actor
        /// </summary>
        /// <param name="idActor"></param>
        /// <param name="idTipoDocumentoIdentidad"></param>
        /// <param name="numeroDocumentoIdentidad"></param>
        /// <param name="apellidoPaterno"></param>
        /// <param name="apellidoMaterno"></param>
        /// <param name="nombres"></param>
        /// <param name="correo"></param>
        /// <param name="idClaseActor"></param>
        /// <param name="idEstadoLegalActor"></param>
        /// <param name="fechaNacimiento"></param>
        /// <param name="telefono"></param>
        /// <param name="roles"></param>
        /// <param name="direcciones"></param>
        /// <returns></returns>
        OperationResult CrearEmpleadoActualizandoActor(int idActor, string codigo, int idTipoDocumentoIdentidad, string numeroDocumentoIdentidad, string apellidoPaterno, string apellidoMaterno, string nombres, string correo, int idClaseActor, int idEstadoLegalActor, DateTime fechaNacimiento, string telefono, List<int> roles, List<Direccion> direcciones);
        #endregion

        #region Actualizar Actores de Negocio
        /// <summary>
        /// Actualiza los datos de un Cliente
        /// </summary>
        /// <param name="id"></param>
        /// <param name="codigo"></param>
        /// <param name="idTipoPersona"></param>
        /// <param name="razonSocial"></param>
        /// <param name="nombreComercial"></param>
        /// <param name="nombreCorto"></param>
        /// <param name="idTipoDocumentoIdentidad"></param>
        /// <param name="numeroDocumentoIdentidad"></param>
        /// <param name="idCondicionPago"></param>
        /// <param name="idFormaPago"></param>
        /// <param name="tiposDeNegociacion"></param>
        /// <param name="idNacion"></param>
        /// <param name="idUbigeo"></param>
        /// <param name="idTipoVia"></param>
        /// <param name="idTipoZona"></param>
        /// <param name="direccion"></param>
        /// <param name="esActivo"></param>
        /// <returns></returns>
        OperationResult ActualizarCliente(int idProveedor, int idActor, string codigo, int idTipoActor, string razonSocial, string apellidoPaterno, string apellidoMaterno, string nombres, string nombreComercial, string nombreCorto, int idTipoDocumentoIdentidad, string numeroDocumentoIdentidad, int? idClaseActor, int? idEstadoLegalActor, string correo, string telefono, List<Direccion> direcciones, int idComprobantePredeterminado);

        /// <summary>
        /// actualiza los datos de un proveedor
        /// </summary>
        /// <param name="idProveedor"></param>
        /// <param name="idActor"></param>
        /// <param name="codigo"></param>
        /// <param name="idTipoActor"></param>
        /// <param name="razonSocial"></param>
        /// <param name="nombreComercial"></param>
        /// <param name="nombreCorto"></param>
        /// <param name="idTipoDocumentoIdentidad"></param>
        /// <param name="numeroDocumentoIdentidad"></param>
        /// <param name="idClaseActor"></param>
        /// <param name="idEstadoLegalActor"></param>
        /// <param name="correo"></param>
        /// <param name="telefono"></param>
        /// <param name="direcciones"></param>
        /// <returns></returns>
        OperationResult ActualizarProveedor(int idProveedor, int idActor, string codigo, int idTipoActor, string razonSocial, string apellidoPaterno, string apellidoMaterno, string nombres, string nombreComercial, string nombreCorto, int idTipoDocumentoIdentidad, string numeroDocumentoIdentidad, int? idClaseActor, int? idEstadoLegalActor, string correo, string telefono, List<Direccion> direcciones);

        /// <summary>
        /// actualiza al empleado
        /// </summary>
        /// <param name="idEmpleado"></param>
        /// <param name="idTipoDocumentoIdentidad"></param>
        /// <param name="numeroDocumentoIdentidad"></param>
        /// <param name="apellidoPaterno"></param>
        /// <param name="apellidoMaterno"></param>
        /// <param name="nombres"></param>
        /// <param name="correo"></param>
        /// <param name="idClaseActor"></param>
        /// <param name="idEstadoLegalActor"></param>
        /// <param name="fechaNacimiento"></param>
        /// <param name="telefono"></param>
        /// <param name="roles"></param>
        /// <param name="direcciones"></param>
        /// <returns></returns>
        OperationResult ActualizarEmpleado(int idEmpleado, int idActor, string codigo, int idTipoDocumentoIdentidad, string numeroDocumentoIdentidad, string apellidoPaterno,
            string apellidoMaterno, string nombres, string correo, int idClaseActor, int idEstadoLegalActor, DateTime fechaNacimiento,
            string telefono, List<int> roles, List<Direccion> direcciones);
        #endregion

        #region Dar de Baja Actores de Negocio
        /// <summary>
        /// Da de baja a un Cliente a partir de la fecha actual
        /// </summary>
        /// <param name="idCliente"></param>
        /// <returns></returns>
        OperationResult DarDeBajaCliente(int idCliente);

        /// <summary>
        /// Da de baja a un Empleado a partir de la fecha actual
        /// </summary>
        /// <param name="idEmpleado"></param>
        /// <returns></returns>
        OperationResult DarDeBajaEmpleado(int idEmpleado);

        /// <summary>
        /// Da de baja a un Proveedor a partir de la fecha actual.
        /// </summary>
        /// <param name="idProveedor"></param>
        /// <returns></returns>
        OperationResult DarDeBajaProveedor(int idProveedor);
        #endregion

        #region Consultas Actores de Negocio
        /// <summary>
        /// devuelve un listado de tipos de actor pueden ser persona natural, juridica y entidad interna.
        /// </summary>
        /// <returns></returns>
        List<Tipo_actor> ObtenerTiposDeActor();

        /// <summary>
        /// devuelve un listado de clase de actor segun el tipo de actor
        /// </summary>
        /// <param name="idTipoActor"></param>
        /// <returns></returns>
        List<Clase_actor> ObtenerTiposDeClaseActor(int idTipoActor);

        /// <summary>
        /// devuelve un listado de estado legal del actor segun el tipo de actor
        /// </summary>
        /// <param name="idTipoActor"></param>
        /// <returns></returns>
        List<Estado_legal> ObtenerTiposDeEstadoLegal(int idTipoActor);
        List<Clase_actor> ObtenerListaSexos();
        List<Clase_actor> ObtenerListaTiposDeSociedad();
        List<Estado_legal> ObtenerListaEstadosCiviles();
        /// <summary>
        /// actualiza los datos de usuario del empleado
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="idEmpleado"></param>
        /// <returns></returns>
        OperationResult EstablecerUsuario(string idUsuario, int idEmpleado);

        /// <summary>
        /// consulta si existe un actor con el numero de documento de identidad
        /// </summary>
        /// <param name="idTipoDocumento"></param>
        /// <param name="numeroDocumento"></param>
        /// <param name="idRol"></param>
        /// <returns></returns>
        RespuestaVerificacionActorNegocio VerificarActor(int idTipoDocumento, string numeroDocumento, int idRol);

        /// <summary>
        /// Devuelve un Empleado a partir de su id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Empleado ObtenerEmpleado(int id);

        /// <summary>
        /// Devuelve una lista de Clientes Vigentes
        /// </summary>
        /// <returns></returns>
        List<ResumenCliente> ObtenerClientesVigentes();
        Task<List<ItemGenerico>> ObtenerClientesVigentesComoItemsGenericos();
        Task<List<ItemGenerico>> ObtenerClientesVigentesComoItemsGenericosPorIdRol();
 

        /// <summary>
        /// Devuelve un Cliente a partir de su id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Cliente ObtenerCliente(int id);


        /// <summary>
        /// Obtiene la direccion de un actor comercial
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        string ObtenerDireccionActorComercial(int id);

        int ObtenerIdUbigeoDireccionActorComercial(int id);
        ItemGenerico ObtenerUbigeoDireccionActorComercial(int id);
        string ObtenerDetalleDireccionActorComercial(int id);

        /// <summary>
        /// Devuelve una lista de Empleados Vigentes
        /// </summary>
        /// <returns></returns>
        List<Empleado> ObtenerEmpleadosVigentes();

        //List<Empleado> ObtenerVendedoresVigentes();

        /// <summary>
        /// devuelve una lista de roles para empleados
        /// </summary>
        /// <returns></returns>
        List<Rol> ObtenerRolesPersonal();

        /// <summary>
        /// devuelve los ids de las acciones posibles para un empleado
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="idEmpleado"></param>
        /// <returns></returns>
        int[] ObtenerIdsAccionesPosibles(string idUsuario, int idEmpleado);

        /// <summary>
        /// Devuelve una lista de Proveedores Vigentes
        /// </summary>
        /// <returns></returns>
        List<Proveedor> ObtenerProveedoresVigentes();

        /// <summary>
        /// Devuelve una Proveedor a partir de su id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Proveedor ObtenerProveedor(int id);
        Task<List<ItemGenerico>> ObtenerProveedoresVigentesComoItemsGenericos();
        /// <summary>
        /// Devuelve una lista de clientes vigentes y no vigentes
        /// </summary>
        /// <returns></returns>
        List<Proveedor> ObtenerProveedores();


        ActorComercial ObtenerActorComercial(int id);

        string ObtenerDenominacionClase(int idTipoActor);



        List<Empleado> ObtenerCajerosVigentes();

        List<Empleado> ObtenerAlmacenerosVigentes();
        List<Empleado> ObtenerVendedoresVigentes();
        List<Empleado> ObtenerCompradoresVigentes();
        List<Cliente> ObtenerClientesVigentesParaVenta();

        #endregion

        #region Turnos de Empleados
        OperationResult CrearTurno(int idCentroDeAtencion, int idEmpleado, DateTime desde, DateTime hasta);

        TurnoDeEmpleado ObtenerTurno(int idTurno);

        List<TurnoDeEmpleado> ObtenerTurnos();

        OperationResult ActualizarTurno(int id, int idCentroDeAtencion, int idEmpleado, DateTime desde, DateTime hasta);
        #endregion

        #region Cartera de clientes
        OperationResult CrearCarteraDeClientes(int idCentroDeAtencion, List<int> idClientes);

        OperationResult ActualizarCarteraDeClientes(int idCentroDeAtencion, List<int> idClientes);

        List<CarteraDeClientes> ObtenerCarterasDeClientes();

        CarteraDeClientes ObtenerCarteraDeClientesSegunCentroDeAtencion(int idCentroAtencion);

        List<Cliente> ObtenerClientesSegunElCentroDeAtencionYElTipoDeVinculo(int idCentroDeAtencion, int tipoDeVinculo);
        List<int> ObtenerIdsCentrosDeAtencionDeLaCarteraDeClientes();

        #endregion



        List<Cliente> ObtenerClientesConCuotasVigentes();


        ActorComercial_ ObtenerClientePorDni(string dni);
        OperationResult GuardarActorComercial(int idRol, RegistroActorComercial actorComercial);
        string ObtenerUltimaPlacaDeCliente(int idCliente);

        List<ItemGenerico> ObtenerGruposActoresComerciales();
        List<ItemGenerico> ObtenerGruposActoresComercialesPorRol(int idRol); 
        List<ItemGenerico> ObtenerActoresComercialesDeGrupoActoresComercialesPorRol(int idRol, int idGrupoActoresComerciales); 
        List<ItemGenerico> ObtenerGruposActoresComercialesPorRolDeActorComercial(int idRol, int idActorComercial);
    }
}
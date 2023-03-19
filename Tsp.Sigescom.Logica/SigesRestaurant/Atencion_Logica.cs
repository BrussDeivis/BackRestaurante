using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom.configuraciones;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Restaurant;
using Tsp.Sigescom.Modelo.Negocio.Establecimientos;
using Tsp.Sigescom.Modelo.Negocio.Restaurant;
using Tsp.Sigescom.Modelo.SigesRestaurant;

namespace Tsp.Sigescom.Parking.Logica
{
    public partial class Atencion_Logica : IAtencion_Logica
    {

        private readonly IAtencion_Repositorio _atencionRepositorio;
        private readonly IMesa_Repositorio _mesaRepositorio;
        private readonly IEstablecimiento_Logica _establecimientoLogica;






        public Atencion_Logica(IAtencion_Repositorio atencionRepositorio, IMesa_Repositorio mesaRepositorio, IEstablecimiento_Logica establecimientoLogica)
        {
            _atencionRepositorio = atencionRepositorio;
            _mesaRepositorio = mesaRepositorio;
            _establecimientoLogica = establecimientoLogica;
        }


        public OperationResult CambiarDeMesa(AtencionRestaurante atencion, int idNuevaMesa, SesionRestaurante sesion)
        {
            try
            {
                Mesa nuevaMesa = new Mesa(_mesaRepositorio.ObtenerMesa(idNuevaMesa));
                if (nuevaMesa.EstadoOcupado)
                {
                    throw new LogicaException("No se puede cambiar de mesa, debido a que la mesa indicada está ocupada");
                }
                return _atencionRepositorio.GuardarCambioDeMesa(atencion, nuevaMesa);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public ConfiguracionRestauranteAtencion ObtenerConfiguracionParaAtencion(SesionRestaurante sesion)
        {
            var tieneRolAdministradorDeNegocio = sesion.SesionDeUsuario.Empleado.TieneRol(ActorSettings.Default.idRolAdministradorDeNegocio);
            var establecimientoSesion = sesion.SesionDeUsuario.EstablecimientoComercialSeleccionado.ToItemGenerico();
            var configuracion = new ConfiguracionRestauranteAtencion()
            {
                NombreRolCliente = "CLIENTE",
                IdEmpleado = sesion.SesionDeUsuario.Empleado.Id,
                UsuarioTieneRolCajero = sesion.SesionDeUsuario.Empleado.TieneRol(ActorSettings.Default.IdRolCajero),
                EstablecimientoSesion = establecimientoSesion,
                PermitirVentaEnMesa = RestauranteSettings.Default.PermitirVentaEnMesa,
                PermitirVentaPorDelivery = RestauranteSettings.Default.PermitirVentaPorDelivery,
                PermitirVentaAlPaso = RestauranteSettings.Default.PermitirVentaAlPaso,
                UsuarioTieneRolAdministradorDeNegocio = tieneRolAdministradorDeNegocio,
                UsuarioTieneRolCajeroYMozo = sesion.SesionDeUsuario.Empleado.TieneRol(ActorSettings.Default.IdRolCajero) && sesion.SesionDeUsuario.Empleado.TieneRol(RestauranteSettings.Default.IdRolMozo),
                Establecimientos = tieneRolAdministradorDeNegocio ? _establecimientoLogica.ObtenerEstablecimientosComercialesVigentesComoItemsGenericos() : new List<ItemGenerico>() { establecimientoSesion },
            };
            return configuracion;
        }

        public OperationResult CerrarAtencionYSusOrdenesAtendiendoDetalles(long idAtencion, int idEmpleado)
        {
            try
            {
                OperationResult result = new OperationResult();
                var fechaActual = DateTimeUtil.FechaActual();
                AtencionRestaurante atencion = _atencionRepositorio.ObtenerAtencionConDatosMinimosDeOrdenYDetallesSoloParaCerrarla(idAtencion);//todas sus ordenes estan anuladas o devueltas...
                if (atencion.Estado != MaestroSettings.Default.IdDetalleMaestroEstadoRegistrado)
                {
                    throw new LogicaException("No se puede cerrar la atencion, debido a que no cuenta con el estado registardo");
                }
                var idsOrdenesACerrar = atencion.Ordenes.Where(o => o.Estado != MaestroSettings.Default.IdDetalleMaestroEstadoCerrado).Select(o=>o.Id).ToArray();
                var idsEstadosDetallesAExcluir = new int[]{ (int)EstadoDeDetalleDeOrden.Atendido, (int)EstadoDeDetalleDeOrden.Devuelto, (int)EstadoDeDetalleDeOrden.Anulado};
                var idsDetallesAAtender = atencion.Ordenes.SelectMany(o => o.DetallesDeOrden).Where(d => !idsEstadosDetallesAExcluir.Contains(d.Estado)).Select(d=>d.Id).ToArray();
                var liberarMesa = atencion.ImporteAtencion == 0;
                result = _atencionRepositorio.CerrarAtencionYOrdenesYAtenderDetalles(idAtencion, idsOrdenesACerrar, idsDetallesAAtender,atencion.Mesa.Id, liberarMesa, idEmpleado);

                result.information = MaestroSettings.Default.IdDetalleMaestroEstadoCerrado;
                result.objeto = atencion.ImporteAtencion== 0;
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

    }
}



using Microsoft.Practices.Unity;
using Tsp.FacturacionElectronica.Logica;
using Tsp.Sigescom.AccesoDatos;
using Tsp.Sigescom.AccesoDatos.Actores;
using Tsp.Sigescom.AccesoDatos.Almacen;
using Tsp.Sigescom.AccesoDatos.Compras;
using Tsp.Sigescom.AccesoDatos.Conceptos;
using Tsp.Sigescom.AccesoDatos.Empleado;
using Tsp.Sigescom.AccesoDatos.Establecimientos;
using Tsp.Sigescom.AccesoDatos.Restaurant;
using Tsp.Sigescom.AccesoDatos.Roles;
using Tsp.Sigescom.AccesoDatos.Sigescom;
using Tsp.Sigescom.AccesoDatos.Sigescom.Actores;
using Tsp.Sigescom.AccesoDatos.Transacciones;
using Tsp.Sigescom.Logica;
using Tsp.Sigescom.Logica.Core.Actores;
using Tsp.Sigescom.Logica.Core.Almacen;
using Tsp.Sigescom.Logica.Core.CentrosDeAtencion;
using Tsp.Sigescom.Logica.Core.Empleado;
using Tsp.Sigescom.Logica.Core.Establecimientos;
using Tsp.Sigescom.Logica.Core.Finanza;
using Tsp.Sigescom.Logica.Core.Permisos;
using Tsp.Sigescom.Logica.Core.Tesoreria;
using Tsp.Sigescom.Logica.Core.TipoDeCambio;
using Tsp.Sigescom.Logica.Core.Venta;
using Tsp.Sigescom.Logica.Sigescom;
using Tsp.Sigescom.Logica.SigesHotel;
using Tsp.Sigescom.Modelo.Interfaces.Datos;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Actores;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Almacen;
using Tsp.Sigescom.Modelo.Interfaces.Datos.CentrosDeAtencion;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Compras;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Empleado;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Establecimientos;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Hotel;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Restaurant;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Transacciones;
using Tsp.Sigescom.Modelo.Interfaces.Infraestructura;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Interfaces.Logica.Tesoreria;
using Tsp.Sigescom.Modelo.Interfaces.Negocio;
using Tsp.Sigescom.Modelo.Interfaces.Negocio.Venta;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio.Conceptos;
using Tsp.Sigescom.Modelo.Negocio.Actores;
using Tsp.Sigescom.Modelo.Negocio.Almacen;
using Tsp.Sigescom.Modelo.Negocio.Venta;
using Tsp.Sigescom.Modelo.Negocio.Establecimientos;
using Tsp.Sigescom.Modelo.Negocio.Finanza;
using Tsp.Sigescom.Modelo.Negocio.LibrosElectronicos;
using Tsp.Sigescom.Modelo.Negocio.Restaurant;
using Tsp.Sigescom.Parking.Logica;
using Tsp.Sigescom.Utilitarios;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Venta;
using Tsp.Sigescom.AccesoDatos.Venta;
using Tsp.Sigescom.Modelo.Interfaces.Negocio.Pedido;
using Tsp.Sigescom.Logica.Core.Pedido;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Pedido;
using Tsp.Sigescom.AccesoDatos.Sigescom.Pedido;

namespace Tsp.Sigescom.Inyeccion
{
    public class ContenedorInyeccion
    {
        #region BASE
        /// <summary>
        /// Flujo base
        /// </summary>
        /// <param name="container">container</param>
        public static void ObtenerRegistros(IUnityContainer container)
        {
            RegistroRepositorios(container);
            RegistroLogicas(container);
            RegistroInfraestructura(container);

        }
        #endregion

        #region REPOSITORIO
        private static void RegistroRepositorios(IUnityContainer container)
        {
            container.RegisterType<IMaestroRepositorio, MaestroDatos>(new TransientLifetimeManager());

            container.RegisterType<IActorRepositorio, ActorDatos>(new TransientLifetimeManager());
            container.RegisterType<IEmpleado_Repositorio, Empleado_Datos>(new TransientLifetimeManager());
            container.RegisterType<ICentroDeAtencion_Repositorio, CentroDeAtencion_Datos>(new TransientLifetimeManager());

            container.RegisterType<IActor_Repositorio, Actor_Datos>(new TransientLifetimeManager());

            container.RegisterType<IEstablecimiento_Repositorio, Establecimiento_Datos>(new TransientLifetimeManager());
            container.RegisterType<ISede_Repositorio, Sede_Datos>(new TransientLifetimeManager());
            container.RegisterType<ISucursal_Repositorio, Sucursal_Datos>(new TransientLifetimeManager());

            container.RegisterType<IVinculoActor_Repositorio, VinculoActor_Datos>(new TransientLifetimeManager());
            container.RegisterType<IConsultaActor_Repositorio, ConsultaActor_Datos>(new TransientLifetimeManager());



            container.RegisterType<IConceptoRepositorio, ConceptoDatos>(new TransientLifetimeManager());
            container.RegisterType<ITransaccionRepositorio, TransaccionDatos>(new TransientLifetimeManager());
            container.RegisterType<IPrecioRepositorio, PrecioDatos>(new TransientLifetimeManager());
            container.RegisterType<IConfiguracionRepositorio, ConfiguracionDatos>(new TransientLifetimeManager());
            container.RegisterType<ICocheraRepositorio, CocheraDatos>(new TransientLifetimeManager());

            container.RegisterType<IRestauranteRepositorio, RestauranteDatos>(new TransientLifetimeManager());
            container.RegisterType<IMesa_Repositorio, Mesa_Datos>(new TransientLifetimeManager());
            container.RegisterType<IAmbiente_Repositorio, Ambiente_Datos>(new TransientLifetimeManager());
            container.RegisterType<IAtencion_Repositorio, Atencion_Datos>(new TransientLifetimeManager());




            container.RegisterType<IFacturacionRepositorio, FacturacionDatos>(new TransientLifetimeManager());
            container.RegisterType<IHotelRepositorio, HotelDatos>(new TransientLifetimeManager());
            container.RegisterType<IHotelReporte_Repositorio, HotelReporte_Datos>(new TransientLifetimeManager());
            container.RegisterType<IInventarioRepositorio, InventarioDatos>(new TransientLifetimeManager());
            container.RegisterType<IInventarioActualRepositorio, InventarioActual_Datos>(new TransientLifetimeManager());

            container.RegisterType<IInventarioHistorico_Repositorio, InventarioHistorico_Datos>(new TransientLifetimeManager());
            container.RegisterType<IMovimientos_Repositorio, Movimientos_Datos>(new TransientLifetimeManager());

            container.RegisterType<IFinanzaReporte_Repositorio, FinanzaReporte_Datos>(new TransientLifetimeManager());
            container.RegisterType<ILibrosElectronicosAdsoftRepositorio, LibrosElectronicosAdsoftDatos>(new TransientLifetimeManager());
            container.RegisterType<ILibrosElectronicosFoxcontRepositorio, LibrosElectronicosFoxcontDatos>(new TransientLifetimeManager());
            container.RegisterType<ILibrosElectronicosConcarRepositorio, LibrosElectronicosConcarDatos>(new TransientLifetimeManager());
            container.RegisterType<IRoles_Repositorio, Roles_Datos>(new TransientLifetimeManager());
            container.RegisterType<ICodigosTransaccion_Repositorio, CodigosTransaccion_Datos>(new TransientLifetimeManager());
            container.RegisterType<IConcepto_Repositorio, Concepto_Datos>(new TransientLifetimeManager());
            container.RegisterType<ICaracteristica_Repositorio, Caracteristica_Datos>(new TransientLifetimeManager());
            container.RegisterType<IMaestrosAlmacen_Repositorio, MaestroAlmacen_Datos>(new TransientLifetimeManager());

            container.RegisterType<ICrearTransaccion_Repositorio, CrearTransacciones_Datos>(new TransientLifetimeManager());
            container.RegisterType<IConsultaTransaccion_Repositorio, ConsultaTransaccion_Datos>(new TransientLifetimeManager());
            container.RegisterType<IActualizarTransaccion_Repositorio, ActualizarTransaccion_Datos>(new TransientLifetimeManager());
            
            
            container.RegisterType<IActualizarDetalleTransaccion_Repositorio, ActualizarDetallesTransaccion_Datos>(new TransientLifetimeManager());
            container.RegisterType<ICrearDetalleTransaccion_Repositorio, CrearDetallesTransaccion_Datos>(new TransientLifetimeManager());
            container.RegisterType<IConsultaDetalleTransaccion_Repositorio, ConsultaDetallesTransaccion_Datos>(new TransientLifetimeManager());
            container.RegisterType<IEliminarDetalleTransaccion_Repositorio, EliminarDetallesTransaccion_Datos>(new TransientLifetimeManager());

            container.RegisterType<IVentaReporte_Repositorio, VentaReporte_Datos>(new TransientLifetimeManager());
            container.RegisterType<IMaestrosVenta_Repositorio, MaestroVenta_Datos>(new TransientLifetimeManager());

            

            container.RegisterType<IConsultaCompras_Repositorio, ConsultaCompras_Datos>(new TransientLifetimeManager());
            container.RegisterType<IOrdenAlmacen_Repositorio, OrdenAlmacen_Datos>(new TransientLifetimeManager());

            container.RegisterType<IPedidoRepositorio, Pedido_Datos>(new TransientLifetimeManager());
        }
        #endregion

        #region LOGICA
        private static void RegistroLogicas(IUnityContainer container)
        {
            container.RegisterType<IOperacionLogica, OperacionLogica>(new TransientLifetimeManager());
            container.RegisterType<IMaestroLogica, MaestroLogica>(new TransientLifetimeManager());

            container.RegisterType<IActorNegocioLogica, ActorNegocioLogica>(new TransientLifetimeManager());
            container.RegisterType<IEmpleado_Logica, Empleado_Logica>(new TransientLifetimeManager());
            container.RegisterType<ICentroDeAtencion_Logica, CentroDeAtencion_Logica >(new TransientLifetimeManager());

            container.RegisterType<IEstablecimiento_Logica, Establecimiento_Logica>(new TransientLifetimeManager());
            container.RegisterType<ISede_Logica, Sede_Logica>(new TransientLifetimeManager());
            container.RegisterType<ISucursal_Logica, Sucursal_Logica>(new TransientLifetimeManager());


            container.RegisterType<IPrecioLogica, PrecioLogica>(new TransientLifetimeManager());
            container.RegisterType<IPermisos_Logica, Permisos_Logica>(new TransientLifetimeManager());
            container.RegisterType<ITipoDeCambio_Logica, TipoDeCambio_Logica>(new TransientLifetimeManager());
            container.RegisterType<ICodigosOperacion_Logica, CodigosOperacion_Logica>(new TransientLifetimeManager());

            container.RegisterType<IConceptoLogica, ConceptoLogica>(new TransientLifetimeManager());
            container.RegisterType<ICaracteristica_Logica, Caracteristica_Logica>(new TransientLifetimeManager());
            container.RegisterType<ICotizacionLogica, CotizacionLogica>(new TransientLifetimeManager());
            container.RegisterType<IConfiguracionLogica, ConfiguracionLogica>(new TransientLifetimeManager());
            container.RegisterType<ILibrosElectronicosLogica, LibrosElectronicosLogica>(new TransientLifetimeManager());
            container.RegisterType<ICocheraLogica, CocheraLogica>(new TransientLifetimeManager());

            container.RegisterType<IRestauranteLogica, RestauranteLogica>(new TransientLifetimeManager());
            container.RegisterType<IAtencion_Logica, Atencion_Logica>(new TransientLifetimeManager());
            container.RegisterType<ICaja_Logica, Caja_Logica>(new TransientLifetimeManager());





            container.RegisterType<IFacturacionElectronicaLogica, FacturacionElectronicaLogica>(new TransientLifetimeManager());
            container.RegisterType<IGeneracionArchivosLogica, GeneracionArchivosLogica>(new TransientLifetimeManager());
            container.RegisterType<IHotelLogica, HotelLogica>(new TransientLifetimeManager());
            container.RegisterType<IHotelReporte_Logica, HotelReporte_Logica>(new TransientLifetimeManager());
            container.RegisterType<IHotelUtilitario_Logica, HotelUtilitario_Logica>(new TransientLifetimeManager());
            container.RegisterType<IVentaUtilitarioLogica, VentaUtilitarioLogica>(new TransientLifetimeManager());
            container.RegisterType<IAlmacenReporte_Logica, AlmacenReporte_Logica>(new TransientLifetimeManager());
            container.RegisterType<IInventarioHistorico_Logica, InventarioHistorico_Logica>(new TransientLifetimeManager());
            container.RegisterType<IInventarioActual_Logica, InventarioActual_Logica>(new TransientLifetimeManager());

            container.RegisterType<IFinanzaReporte_Logica, FinanzaReporte_Logica>(new TransientLifetimeManager());
            container.RegisterType<ILibrosElectronicosAdsoftLogica, LibrosElectronicosAdsoftLogica>(new TransientLifetimeManager());
            container.RegisterType<ILibrosElectronicosFoxcontLogica, LibrosElectronicosFoxcontLogica>(new TransientLifetimeManager());
            container.RegisterType<ILibrosElectronicosConcarLogica, LibrosElectronicosConcarLogica>(new TransientLifetimeManager());
            container.RegisterType<IAjusteInventario_Logica, AjusteInventario_Logica>(new TransientLifetimeManager());
            container.RegisterType<IAjusteTesoreria_Logica, AjusteTesoreria_Logica>(new TransientLifetimeManager());

            container.RegisterType<ISession_Logica, Session_Logica>(new TransientLifetimeManager());
            container.RegisterType<IConsultaMasivaVentaLogica, ConsultaMasivaVentaLogica>(new TransientLifetimeManager());

            container.RegisterType<IValidacionActorNegocio_Logica, ValidacionActorNegocio_Logica>(new TransientLifetimeManager());
            container.RegisterType<IGrupoClientes_Logica, GrupoClientes_Logica>(new TransientLifetimeManager());

            container.RegisterType<IOrdenAlmacen_Logica, OrdenAlmacen_Logica>(new TransientLifetimeManager());
            container.RegisterType<IVentaReporte_Logica, VentaReporte_Logica>(new TransientLifetimeManager());
            container.RegisterType<IPedido_Logica, Pedido_Logica>(new TransientLifetimeManager());
            container.RegisterType<IPedidoReporte_Logica, PedidoReporte_Logica>(new TransientLifetimeManager());

        }
        #endregion

        #region Infraestructura
        private static void RegistroInfraestructura(IUnityContainer container)
        {
            container.RegisterType<IMailer, Mailer>(new TransientLifetimeManager());
            container.RegisterType<IBarCodeUtil, BarCodeUtil>(new TransientLifetimeManager());
            container.RegisterType<IPdfUtil, PdfUtil>(new TransientLifetimeManager());

        }
        #endregion
    }
}

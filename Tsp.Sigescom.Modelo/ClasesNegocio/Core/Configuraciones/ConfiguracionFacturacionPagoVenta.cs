using System.Linq;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;

namespace Tsp.Sigescom.Modelo.Entidades
{
    public class ConfiguracionVenta
    {
        //Cliente
        public readonly int IdTipoActorPersonaNatural = ActorSettings.Default.IdTipoActorPersonaNatural;
        public readonly int IdTipoActorPersonaJuridica = ActorSettings.Default.IdTipoActorPersonaJuridica;
        public readonly bool PermitirAliasDeClienteGenerico = TransaccionSettings.Default.MostrarAliasDeClienteGenerico;
        public readonly int IdTipoDocumentoIdentidadDni = ActorSettings.Default.IdTipoDocumentoIdentidadDni;
        public readonly int IdTipoDocumentoIdentidadRuc = ActorSettings.Default.IdTipoDocumentoIdentidadRuc;
        public readonly int IdClienteGenerico = ActorSettings.Default.IdClienteGenerico;
        public readonly int IdTipoDocumentoCuandoClienteEsGenerico = TransaccionSettings.Default.IdTipoDocumentoCuandoClienteEsGenerico;
        public readonly bool AplicaLeyAmazonia = TransaccionSettings.Default.AplicaLeyAmazonia;
        public readonly int IdTipoDocumentoPorDefectoParaVenta = TransaccionSettings.Default.IdTipoDocumentoPorDefectoParaVenta;
        public readonly bool PermitirDetalleUnificado = AplicacionSettings.Default.MostrarDetalleUnificado;
        public readonly bool CheckedDetalleUnificado = AplicacionSettings.Default.ChecketDetalleUnificado;
        public readonly bool ActivarDetalleUnificadoPersonalizado = VentasSettings.Default.ActivarDetalleUnificadoPersonalizado;
        public readonly string TextoDetalleUnificado = AplicacionSettings.Default.ValorDetalleUnificado;

        public readonly bool PermitirVentaAlCredito = AplicacionSettings.Default.PermitirVentaAlCredito;
        public readonly decimal MontoMaximoAVenderCuandoClienteNoEstaIdentificado = FacturacionElectronicaSettings.Default.MontoMaximoAVenderCuandoClienteNoEstaIdenticicado;
        public readonly int IdComprobantePredeterminadoPorDefecto = MaestroSettings.Default.IdDetalleMaestroComprobanteNotaVentaInterna;
        public readonly int IdComprobanteBoleta = MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta;
        public readonly int IdComprobanteFactura = MaestroSettings.Default.IdDetalleMaestroComprobanteFactura;
        public readonly int IdMedioDePagoEfectivo = MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo;
        public readonly int IdMedioDePagoTarjetaDebito = MaestroSettings.Default.IdDetalleMaestroMedioDePagoTarjetaDebito;
        public readonly int IdMedioDePagoTarjetaCredito = MaestroSettings.Default.IdDetalleMaestroMedioDePagoTarjetaCredito;
        public readonly int IdMedioDePagoDepositoEnCuenta = MaestroSettings.Default.IdDetalleMaestroMedioDepagoDepositoEnCuenta;
        public readonly int IdMedioDePagoTransferenciaDeFondos = MaestroSettings.Default.IdDetalleMaestroMedioDepagoTransferenciaDeFondos;
        public readonly ItemGenerico RolCliente = new ItemGenerico { Id = ActorSettings.Default.IdRolCliente, Nombre = "CLIENTE" };
        public readonly int ComprobanteParaVenta = (int)TipoComprobantePara.Venta;
        public readonly int ComprobanteParaVentaPorContingencia = (int)TipoComprobantePara.VentasPorContingencia;
        public readonly bool GenerarPuntosEnVentas = VentasSettings.Default.GenerarPuntosEnVentas;
        public readonly decimal ImporteDeVentaParaGenerarUnPunto = VentasSettings.Default.ImporteDeVentaParaGenerarUnPunto;
        public readonly bool UsarPuntosComoMedioDePago = VentasSettings.Default.UsarPuntosComoMedioDePago;
        public readonly decimal ValorDeUnPuntoComoMedioDePago = VentasSettings.Default.ValorDeUnPuntoComoMedioDePago;
        public readonly decimal IdMedioDePagoPuntos = MaestroSettings.Default.IdDetalleMaestroMedioDepagoPuntos;
        public readonly decimal MinimoCaracteresBuscarActorComercial = ActorSettings.Default.MinimoDeCaracteresParaBuscarEnSelectorActorComercial;
        public readonly decimal TiempoEsperaBusquedaSelector = AplicacionSettings.Default.TiempoDeEsperaEnBusquedaSelectoresDeGranCantidad;
        public readonly bool MostrarCheckIgvEnVentas = VentasSettings.Default.MostrarCheckIgvEnVentas;
        public readonly string MascaraDeVisualizacionValidacionRegistroCliente = ActorSettings.Default.MascaraDeVisualizacionValidacionRegistroCliente;
        public string FechaActual;
        public readonly bool PermitirSeleccionarGrupoCliente = Diccionario.MapeoOperacionesGruposVsPermitirGrupos.Single(m => m.Key == (int)OperacionesGruposActoresComerciales.Venta).Value ;
        public readonly bool MostrarSeccionEntregaEnVenta = VentasSettings.Default.MostrarSeccionEntregaEnVenta;

    }



    public sealed class ConfiguracionAccion
    {
        public int IdMonedaSoles = MaestroSettings.Default.IdDetalleMaestroMonedaSoles;
        public int IdUnidadDeNegocioTransversal = MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal;
        public int IdAccionDeVenta;
        public int IdEstadoDeVenta;
        public string ComentarioEstadoDeVenta;
        public int IdTipoTransaccionVenta = TransaccionSettings.Default.IdTipoTransaccionVenta;
        public int IdTipoTransaccionOrdenDeVenta = TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta;
        public int IdTipoTransaccionIngresoDeDinero = TransaccionSettings.Default.IdTipoTransaccionCobranzaFacturasClientes;
        public int IdTipoTransaccionSalidaDeMercaderia = TransaccionSettings.Default.IdTipoTransaccionSalidaBienesPorVenta;

        private static readonly ConfiguracionAccion confirmacionDeVenta = new ConfiguracionAccion();
        public static ConfiguracionAccion ConfirmacionOrdenVenta
        {
            get
            {
                confirmacionDeVenta.IdAccionDeVenta = MaestroSettings.Default.IdDetalleMaestroAccionConfirmar;
                confirmacionDeVenta.IdEstadoDeVenta = MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado;
                confirmacionDeVenta.ComentarioEstadoDeVenta = "Estado inicial asignado automaticamente al confirmar la venta";
                return confirmacionDeVenta;
            }
        }

        private static readonly ConfiguracionAccion registroOrdenDeVenta = new ConfiguracionAccion();
        public static ConfiguracionAccion RegistroOrdenVenta
        {
            get
            {
                confirmacionDeVenta.IdAccionDeVenta = MaestroSettings.Default.IdDetalleMaestroAccionRegistrar;
                confirmacionDeVenta.IdEstadoDeVenta = MaestroSettings.Default.IdDetalleMaestroEstadoRegistrado;
                confirmacionDeVenta.ComentarioEstadoDeVenta = "Estado inicial asignado automaticamente al registrar la venta";
                return registroOrdenDeVenta;
            }
        }
    }


    public sealed class ConfiguracionReportesAdministrador
    {
        public int MaximoDiasInvalidaciones = VentasSettings.Default.MaximoDiasReporteInvalidaciones;
        public int MaximoDiasNotasCredito = VentasSettings.Default.MaximoDiasReporteNotasCredito;
        public int MaximoDiasNotasDebito= VentasSettings.Default.MaximoDiasReporteNotasDebito;
       

        private static readonly ConfiguracionReportesAdministrador defaultInstance = new ConfiguracionReportesAdministrador();
        public static ConfiguracionReportesAdministrador Default
        {
            get
            {
                return defaultInstance;
            }
        }
    }

    public sealed class ConfiguracionReportesVendedor
    {
        public int MaximoDiasInvalidaciones = VentasSettings.Default.MaximoDiasReporteInvalidaciones;


        private static readonly ConfiguracionReportesVendedor defaultInstance = new ConfiguracionReportesVendedor();
        public static ConfiguracionReportesVendedor Default
        {
            get
            {
                return defaultInstance;
            }
        }
    }
}

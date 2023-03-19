using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades
{
      
    public enum TamanioPagina
    {
        Chico = 5,
        Normal = 10,
        Medio = 16,
        Grande = 24,
        ExtraGrande = 36
    }

    public enum DireccionOrden
    {
        Asc,
        Desc
    }

    public enum CambioDireccionOrden
    {
        Cambio,
        SinCambio
    }

    public enum AtributosPaginacion
    {
        DireccionOrden,
        HRef,
        OnClick,
        Orden,
        Pagina
    }

    public enum RespuestaMantenimiento
    {
        Inicial = -1,
        Cancelar = 0,
        Exito = 1,
        Error = 2,
        NoPermitido = 3
    }
    public enum AccionesMantenimiento
    {
        Nuevo = 1,
        Editar = 2,
        Consultar = 3

    }
    public enum OperationResultEnum
    {
        Error = 0,
        Warning = 1,
        Information = 2,
        Success = 3,
        Exit = 4
    }

    public enum MascaraVisualizacionValidacionRegistroActor
    {
        NoMostrar = 0,
        Opcional = 1,
        Obligatorio = 2,
    }

    //public enum TipoVentaEnum
    //{
    //    Integrado = 1,
    //    DosPasos = 2
    //}

    public enum RespuestaVerificacionEnum
    {
        ExisteActorNegocio = 1,
        ExisteSoloActor = 2,
        NoExisteActor = 3,
        NoSePudoVerificar = 4,
        RucNoValido = 5
    }

    public enum TipoFamiliaEnum{
        Sevicio = 0,
        Bien = 1
    }

    public enum ModoSeleccionTipoFamilia
    {
        Sevicio = 0,
        Bien = 1,
        Ambos = 2
    }

    public enum ServicoPrestadoA_ModoCarga
    {
        DatosBasicosBandeja = 1,
        DatosBasicosGeneral = 2,
        IncluirSobrecostos = 3,
        IncluirSobrecostosYCostos = 4,
        DatosFacturacion = 5
    }

    public enum InformacionSelectorConcepto
    {
        Nombre = 1,
        NombreStockPrecio = 2
    }

    public enum ViewModel_ModoCarga
    {
        //DatosBasicos = 1,
        DatosBandeja = 1,
        DatosGenerales = 2,
        DatosCombo = 3,
    }

    public enum SobreCostos_OrigenGuardado
    {
        Operacion = 1,
        Liquidacion = 2,
    }

    public enum AccionOrdenVenta
    {
        Anular = 1,
        Descontar = 2,
    }
    public enum TipoSolicitud
    {
        Anticipo = 1,
        Devolucion = 2,
    }
    public enum TipoOperacionCompra
    {
        [Description("Nimguno")]
        Ninguno = 0,
        [Description("No Gravadas")]
        NoGravada = 1,
        [Description("Gravadas Destinadas a Ventas Gravadas")]
        GravadaDestinadaAVentasGravadas = 2,
        [Description("Gravadas Destinadas a Ventas No Gravadas")]
        GravadaDestinadaAVentasNoGravadas = 3,
        [Description("Gravadas Destinadas a Ventas Gravadas y No Gravadas")]
        GravadaDestinadaAVentasGravadasYNoGravadas = 4
    }

    public enum EstadoSigescomDocumentoElectronico
    {
        [Description("Confirmado")]
        Confirmado = 1,
        [Description("Invalidado")]
        Invalidado = 2,
        [Description("Con Nota")]
        Anotado = 3 //Estado asignado cuando sea una nota de credito y debito
    }
    public enum EstadoDocumentoElectronico //Para los enumerados de EstadoDocumentoElectronico ver los codigos detalles de maestro del maestro 35	CS19	RESUMEN DIARIO DE BOLETAS DE VENTA Y NOTAS ELECTRÓNICAS - CÓDIGOS DE ESTADO DE ÍTEM
    {
        [Description("Adicionado")]
        Adicionado = 1, //Detalle de maestro
        [Description("Modificado")]
        Modificado = 2, //Detalle de maestro
        [Description("Anulado")]
        Anulado = 3, //Detalle de maestro
        [Description("Descartado")]
        Descartado = 4
    }

    public enum EstadoEnvio
    {
        [Description("Pendiente")]
        Pendiente = 1,
        [Description("Aceptado")]
        Aceptado = 2,
        [Description("Aceptado Con Observacion")]
        AceptadoConObservaciones = 3,
        [Description("Rechazado")]
        Rechazado = 4,
        [Description("Con Excepcion")]
        ConExcepcion = 5,
        [Description("Descartado")]
        Descartado = 6,
        [Description("Descartado Por Excepcion")]
        DescartadoPorExcepcion = 7,
        [Description("Descartado Por Rechazo")]
        DescartadoPorRechazo = 8,
        [Description("No Enviado")]
        NoEnviado = 8,
    }

    public enum ModoEnvio //Para los enumerados de ModoEnvio se ve el modo de envio hacia sunat
    {
        [Description("Adicion")]
        Adicion = 1,
        [Description("Anulacion")]
        Anulacion = 2,
        [Description("NA")]
        Ninguno = 3,
    }

    public enum TipoEnvio 
    {
        [Description("Individual")]
        Individual = 1,
        [Description("Resumen Diario")]
        ResumenDiario = 2,
        [Description("Comunicacion de Baja")]
        ComunicacionBaja = 3,
    }

    public enum ModoPago
    {
        [Description("Contado")]
        Contado = 1,
        [Description("Crédito Rapido")]
        CreditoRapido = 2,
        [Description("Crédito Configurado")]
        CreditoConfigurado = 3
    }

    public enum FormaPagoEnum
    {
        [Description("Contado")]
        Contado = 1,
        [Description("Credito")]
        Credito = 2,
    }

    public enum ProcesoEnvioFacturacion
    {
        [Description("INTERFAZ GRAFICA DE USUARIO")]
        InterfazGraficaUsuario = 1,
        [Description("HANGFIRE")]
        Hangfire = 2
    }

    public enum IngresoTotal
    {
        No = 0,
        Si = 1
    }

    public enum TipoVinculo
    {
        Turno = 1,
        CarteraDeCliente = 2,
        VehiculoExoneradoEnCochera = 3,
        MesaDeAmbiente = 4, 
        MiembroGrupo = 5,
        ResponsableGrupo = 6
    }
    public enum FormatoImpresion
    {
        _80mm = 1,
        A4 = 2,
        _56mm = 3

    }
    /// <summary>
    ///  Representa el tipo de afectacion de existencias en un almacen, cuando se realiza operaciones con bienes. 
    ///  Por ejemplo: 
    ///  Al vender se quitaran existencias, y al comprar se quitaran existencias
    /// </summary>
    public enum TipoDeAfectacionDeExistencia
    {
        Agregar = 1,
        Quitar = 2
    }
    public enum TipoMovimientoOperacion
    {
        [Description("Entrada")]
        Entrada = 1,
        [Description("Salida")]
        Salida = 2,

    }
    public enum ModoOperacionEnum
    {
        [Description("Por Mostrador")]
        PorMostrador = 1,
        [Description("Corporativa")]
        Corporativa = 2,
        [Description("Por Cartera Cliente")]
        VentaCobranzaCarteraClientesDigitada = 3,
        [Description("Masiva")]
        VentaIntegradaMasivaDigitada = 4,
        [Description("Contingencia")]
        VentaPorContingencia = 5,
        [Description("Mostrador En 2 Pasos")]
        PorMostradorEnDosPasos = 6,
        [Description("Ninguno")]
        Ninguno = 7
    }

    public enum ModoImpresionCaracteristicasEnum
    {
        [Description("No imprimir")]
        NoImprimir = 1,
        [Description("Solo comunes")]
        SoloComunes = 2,
        [Description("Solo propias")]
        SoloPropias = 3,
        [Description("Comunes y propias")]
        ComunesYPropias = 4
    }

    public enum ModoIngresoCodigoBarraEnVenta
    {
        CodigoBarraDeProducto = 1,
        CodigoBarraDeBalanza = 2,
        Ambos = 3
    }

    public enum CursorInicialCodigoBarraEnVenta
    {
        CodigoBarraDeProducto = 1,
        CodigoBarraDeBalanza = 2,
    }


    public enum CursorPorDefectoCodigoBarraEnVenta
    {
        CodigoBarraDeProducto = 1,
        CodigoBarraDeBalanza = 2
    }

    public enum PoliticaDePreciosParaVentaEnum
    {
        Global = 1,
        EstablecimientoComercial = 2,
        CentroDeAtencion = 3
    }

    public enum AccionesDeNegocioEnum
    {
        MovimientoEnCaja = 1,
        MovimientoEnAlmacen = 2,
        CompromisoDeCaja = 3,
        CompromisoDeAlmacen = 4
    }

    public enum ElementoDeCalculoEnVentasEnum
    {
        Cantidad = 0,
        PrecioUnitario = 1,
        Importe = 2
    }


    public enum SistemaPagoCochera
    {
        [Description("Tarifa Plana")]
        PLN = 1,
        [Description("Tarifa por Hora")]
        HOR = 2,
        [Description("Abonado")]
        ABN = 3,
    }
    public enum TipoConfiguracion
    {
        [Description("General")]
        GENERAL = 1,
        [Description("Individual")]
        INDIVIDUAL = 2,

    }
    public enum PeriodoCochera
    {
        [Description("Semanal")]
        SEMANAL = 1,
        [Description("Quincenal")]
        QUICENAL = 2,
        [Description("Mensual")]
        MENSUAL = 3
    }
    public enum TipoComprobantePara
    {
        [Description("Venta")]
        Venta = 1,
        [Description("ReporteDeVenta")]
        ReporteDeVenta = 2,
        [Description("VentasYSusNotas")]
        VentasYSusNotas = 3,
        [Description("VentasPorContingencia")]
        VentasPorContingencia = 4,
        [Description("AnularVenta")]
        AnularVenta = 5,
        [Description("DescuentoSobreVenta")]
        DescuentoSobreVenta = 6,
        [Description("RecargoSobreVenta")]
        RecargoSobreVenta = 7,
        [Description("SeriesAutonumericasParaVenta")]
        SeriesAutonumericasParaVenta = 8,
        [Description("SeriesAutonumericasParaVentaExcluidoFactura")]
        SeriesAutonumericasParaVentaExcluidoFactura = 9,
        [Description("Pedido")]
        Pedido=10,

    }


    public enum TipoComprobanteReporteCompra
    {
        Todos = 1,
        Tributables = 2,
        NoTributables = 3,
    }

    public enum EstadoLogEnvio
    {
        Error = 0,
        Exito = 1,
        Ambos = 2,
        NoHay = 3,
    }

    /// <summary>
    /// se usa en las extensiones de siges para indicar el modo en que se registrará una venta generada por otro tipo de transacción.
    /// </summary>
    public enum ModoGeneracionVenta
    {
        [Description("Venta y Orden")]
        SoloOrden = 1,
        [Description("Venta, Orden, pago, movimiento almacén.")]
        ConfirmarVentaCompleta = 2
    }
    public enum TipoPagoAtencion
    {
        [Description("General")]
        General = 1,
        [Description("Diferenciado")]
        Diferenciado = 2,
    }


    public enum ModulosAdicionales
    {
        [Description("ITEM RESTAURANTE")]
        Restaurante = 0,
        [Description("HOTEL")]
        Hotel = 1,
        [Description("COCHERA")]
        Cochera = 2,
        [Description("ITEM INSUMO")]
        Insumo = 3
    }

    public enum IndicadorModuloCaracteristica
    {
        Ninguno = 0,
        Restaurante = 1,
        Hotel = 2,
        Cochera = 3,
    }


    public enum InformacionDireccionEnCliente
    {
        SoloDetalle = 1,
        DetalleConUbigeo = 2
    }
    //public enum CodigoDeOperacionEnum
    //{


    //    //Orden
    //    [Description("OV")]
    //    OrdenDeVenta = 5,
    //    [Description("OC")]
    //    OrdenDeCompra = 6,
    //    [Description("OIV")]
    //    OrdenDeInvalidacionDeVenta = 7,
    //    [Description("OIC")]
    //    OrdenDeInvalidacionDeCompra = 8,

    //    //Mercaderia
    //    [Description("SMV")]
    //    SalidaDeMercaderiaPorVenta = 9,
    //    [Description("SMIC")]
    //    SalidaDeMercaderiaPorInvalidacionDeCompra = 12,




    //}


    public enum ModoVenta
    {
        VentaNormal = 1,
        VentaPorContingencia = 2,
        VentaModoCajaAlmacen = 3,
    }

    public enum ModuloSeleccionConcepto
    {
        Ventas = 1,
        Compras = 2,
        CambioPrecios = 3,
        GuiaRemision = 4,
        TrasladoMercaderia = 5,
        Cotizacion = 6,
    }

    public enum OperacionesGruposActoresComerciales
    {
        Venta = 1,
        Preventa = 2,
        Cotizacion = 3,
        Compra = 4,
        Gasto = 5,
    }

    public enum IndicadorImpactoAlmacen
    {
        [Description("Inmediata")]
        Inmediata = 0,
        [Description("Diferida")]
        Diferida = 1,
        NoImpactaNoBienes = 2,
        NoImpactaPorQueRevocaAOperacionInicial = 3,
    }

    public static class Enumerado
    {
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attr = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            //string descripcion = (attr.Length > 0) ? (attr[0] as DescriptionAttribute).Description : value.ToString();
            return (attr.Length > 0) ? (attr[0] as DescriptionAttribute).Description : value.ToString();
            //return (attr.Length == 0) ? value.ToString() : (attr[0] as DescriptionAttribute).Description;
        }
    }

}

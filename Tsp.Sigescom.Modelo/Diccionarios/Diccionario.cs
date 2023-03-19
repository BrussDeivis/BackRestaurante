using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades
{
    public static class Diccionario
    {


        /// <summary>
        /// La clave es el id de tipo de transaccion del wraper
        /// El valor es el id de tipo de transaccion de la orden
        /// </summary> 
        public static Dictionary<int, int> MapeoWraperVsOrden = new Dictionary<int, int> {
            {TransaccionSettings.Default.IdTipoTransaccionAnulacionDeCompra,TransaccionSettings.Default.IdTipoTransaccionOrdenDeAnulacionDeCompra },
            {TransaccionSettings.Default.IdTipoDeTransaccionAnulacionDeCompraPorErrorEnElRuc,TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAnulacionDeCompraPorErrorEnElRuc },
            {TransaccionSettings.Default.IdTipoDeTransaccionCorreccionDeCompraPorErrorEnLaDescripcion,TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeCorreccionDeCompraPorErrorEnLaDescripcion },
            {TransaccionSettings.Default.IdTipoDeTransaccionDescuentoGlobalEnCompra,TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDescuentoGlobalEnCompra },
            {TransaccionSettings.Default.IdTipoDeTransaccionDescuentoPorItemEnCompra,TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDescuentoPorItemEnCompra },
            {TransaccionSettings.Default.IdTipoDeTransaccionDevolucionTotalDeCompra,TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionTotalDeCompra },
            {TransaccionSettings.Default.IdTipoDeTransaccionDevolucionPorItemEnCompra,TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionPorItemEnCompra },
            {TransaccionSettings.Default.IdTipoDeTransaccionInteresesPorMoraEnCompra,TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeInteresesPorMoraEnCompra },
            {TransaccionSettings.Default.IdTipoDeTransaccionAumentoEnElValorDeCompra,TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAumentoEnElValorDeCompra },

            {TransaccionSettings.Default.IdTipoTransaccionAnulacionDeVenta,TransaccionSettings.Default.IdTipoTransaccionOrdenDeAnulacionDeVenta },
            {TransaccionSettings.Default.IdTipoDeTransaccionAnulacionDeVentaPorErrorEnElRuc,TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAnulacionDeVentaPorErrorEnElRuc },
            {TransaccionSettings.Default.IdTipoDeTransaccionCorreccionDeVentaPorErrorEnLaDescripcion,TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeCorreccionDeVentaPorErrorEnLaDescripcion },
            {TransaccionSettings.Default.IdTipoDeTransaccionDescuentoGlobalEnVenta,TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDescuentoGlobalEnVenta },
            {TransaccionSettings.Default.IdTipoDeTransaccionDescuentoPorItemEnVenta,TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDescuentoPorItemEnVenta },
            {TransaccionSettings.Default.IdTipoDeTransaccionDevolucionTotalDeVenta,TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionTotalDeVenta },
            {TransaccionSettings.Default.IdTipoDeTransaccionDevolucionPorItemEnVenta,TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionPorItemEnVenta },
            {TransaccionSettings.Default.IdTipoDeTransaccionInteresesPorMoraEnVenta,TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeInteresesPorMoraEnVenta },
            {TransaccionSettings.Default.IdTipoDeTransaccionAumentoEnElValorDeVenta,TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAumentoEnElValorDeVenta },

            {TransaccionSettings.Default.IdTipoTransaccionGasto,TransaccionSettings.Default.IdTipoTransaccionOrdenGasto },

            {TransaccionSettings.Default.IdTipoTransaccionInvalidacionDeVenta,TransaccionSettings.Default.IdTipoTransaccionOrdenDeInvalidacionDeVenta},
            {TransaccionSettings.Default.IdTipoTransaccionInvalidacionDeCompra,TransaccionSettings.Default.IdTipoTransaccionOrdenDeInvalidacionDeCompra},
            {TransaccionSettings.Default.IdTipoTransaccionInvalidacionDeAnulacionDeCompra,TransaccionSettings.Default.IdTipoTransaccionOrdenInvalidacionDeAnulacionDeCompra},
            {TransaccionSettings.Default.IdTipoTransaccionInvalidacionDeAnulacionDeVenta,TransaccionSettings.Default.IdTipoTransaccionOrdenInvalidacionDeAnulacionDeVenta}
        };


        /// <summary>
        /// La clave es el id del tipo de transaccion de la orden
        /// El valor es el id del tipo de transaccion del movimiento economico
        /// </summary> 
        public static Dictionary<int, int> MapeoOrdenVsMovimientoEconomico = new Dictionary<int, int> {
            {TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta,TransaccionSettings.Default.IdTipoTransaccionCobranzaFacturasClientes},
            {TransaccionSettings.Default.IdTipoTransaccionOrdenDeCompra,TransaccionSettings.Default.IdTipoTransaccionPagoFacturasProveedores},
            {TransaccionSettings.Default.IdTipoTransaccionOrdenDeCobroVarios,TransaccionSettings.Default.IdTipoTransaccionCobroDeCobroVarios},
            {TransaccionSettings.Default.IdTipoTransaccionOrdenPagoVarios,TransaccionSettings.Default.IdTipoTransaccionPagoDePagoVarios},

            {TransaccionSettings.Default.IdTipoTransaccionOrdenDeAnulacionDeCompra,TransaccionSettings.Default.IdTipoTransaccionIngresoDeDineroPorAnulacionDeCompra},
            {TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAnulacionDeCompraPorErrorEnElRuc,TransaccionSettings.Default.IdTipoDeTransaccionIngresoDeDineroPorAnulacionDeCompraPorErrorEnElRuc},
            {TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDescuentoGlobalEnCompra,TransaccionSettings.Default.IdTipoDeTransaccionIngresoDeDineroPorDescuentoGlobalEnCompra},
            {TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDescuentoPorItemEnCompra,TransaccionSettings.Default.IdTipoDeTransaccionIngresoDeDineroPorDescuentoPorItemEnCompra},
            {TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionTotalDeCompra,TransaccionSettings.Default.IdTipoDeTransaccionIngresoDeDineroPorDevolucionTotalDeCompra},
            {TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionPorItemEnCompra,TransaccionSettings.Default.IdTipoDeTransaccionIngresoDeDineroPorDevolucionPorItemEnCompra},
            {TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeInteresesPorMoraEnCompra,TransaccionSettings.Default.IdTipoDeTransaccionSalidaDeDineroPorInteresesPorMoraEnCompra},
            {TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAumentoEnElValorDeCompra,TransaccionSettings.Default.IdTipoDeTransaccionSalidaDeDineroPorAumentoEnElValorDeCompra },

            {TransaccionSettings.Default.IdTipoTransaccionOrdenDeAnulacionDeVenta,TransaccionSettings.Default.IdTipoTransaccionSalidaDeDineroPorAnulacionDeVenta},
            {TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAnulacionDeVentaPorErrorEnElRuc,TransaccionSettings.Default.IdTipoDeTransaccionSalidaDeDineroPorAnulacionDeVentaPorErrorEnElRuc},
            {TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDescuentoGlobalEnVenta,TransaccionSettings.Default.IdTipoDeTransaccionSalidaDeDineroPorDescuentoGlobalEnVenta},
            {TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDescuentoPorItemEnVenta,TransaccionSettings.Default.IdTipoDeTransaccionSalidaDeDineroPorDescuentoPorItemEnVenta},
            {TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionTotalDeVenta,TransaccionSettings.Default.IdTipoDeTransaccionSalidaDeDineroPorDevolucionTotalDeVenta},
            {TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionPorItemEnVenta,TransaccionSettings.Default.IdTipoDeTransaccionSalidaDeDineroPorDevolucionPorItemEnVenta},
            {TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeInteresesPorMoraEnVenta,TransaccionSettings.Default.IdTipoDeTransaccionIngresoDeDineroPorInteresesPorMoraEnVenta},
            {TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAumentoEnElValorDeVenta,TransaccionSettings.Default.IdTipoDeTransaccionIngresoDeDineroPorAumentoEnElValorDeVenta },

            {TransaccionSettings.Default.IdTipoTransaccionOrdenDeInvalidacionDeVenta,TransaccionSettings.Default.IdTipoTransaccionSalidaDeDineroPorInvalidacionDeVenta },
            {TransaccionSettings.Default.IdTipoTransaccionOrdenDeInvalidacionDeCompra,TransaccionSettings.Default.IdTipoTransaccionIngresoDeDineroPorInvalidacionDeCompra},

            {TransaccionSettings.Default.IdTipoTransaccionOrdenGasto,TransaccionSettings.Default.IdTipoTransaccionPagoGasto },

        };

        /// <summary>
        /// La clave es el id de tipo de transaccion de la orden
        /// El valor es el id de tipo de transaccion del movimiento de mercaderia
        /// </summary> 
        public static Dictionary<int, int> MapeoOrdenVsMovimientoDeAlmacen = new Dictionary<int, int> {
            {TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta,TransaccionSettings.Default.IdTipoTransaccionSalidaBienesPorVenta},
            {TransaccionSettings.Default.IdTipoTransaccionOrdenDeCompra,TransaccionSettings.Default.IdTipoTransaccionIngresoBienesPorCompra},

            {TransaccionSettings.Default.IdTipoTransaccionOrdenDeAnulacionDeCompra,TransaccionSettings.Default.IdTipoTransaccionSalidaDeMercaderiaPorAnulaciónDeCompra},
            {TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAnulacionDeCompraPorErrorEnElRuc,TransaccionSettings.Default.IdTipoDeTransaccionSalidaDeMercaderiaPorAnulacionDeCompraPorErrorEnElRuc},
            {TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionTotalDeCompra,TransaccionSettings.Default.IdTipoDeTransaccionSalidaDeMercaderiaPorDevolucionTotalDeCompra},
            {TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionPorItemEnCompra,TransaccionSettings.Default.IdTipoDeTransaccionSalidaDeMercaderiaPorDevolucionPorItemEnCompra},
            {TransaccionSettings.Default.IdTipoTransaccionOrdenDeInvalidacionDeCompra,TransaccionSettings.Default.IdTipoTransaccionSalidaDeMercaderiaPorInvalidacionDeCompra},


            {TransaccionSettings.Default.IdTipoTransaccionOrdenDeAnulacionDeVenta,TransaccionSettings.Default.IdTipoTransaccionIngresoDeMercaderiaPorAnulaciónDeVenta},
            {TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAnulacionDeVentaPorErrorEnElRuc,TransaccionSettings.Default.IdTipoDeTransaccionIngresoDeMercaderiaPorAnulacionDeVentaPorErrorEnElRuc},
            {TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionTotalDeVenta,TransaccionSettings.Default.IdTipoDeTransaccionIngresoDeMercaderiaPorDevolucionTotalDeVenta},
            {TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionPorItemEnVenta,TransaccionSettings.Default.IdTipoDeTransaccionIngresoDeMercaderiaPorDevolucionPorItemEnVenta},
            {TransaccionSettings.Default.IdTipoTransaccionOrdenDeInvalidacionDeVenta,TransaccionSettings.Default.IdTipoTransaccionIngresoDeMercaderiaPorInvalidacionDeVenta},
        };

        /// <summary>
        /// La clave es el id de tipo de transaccion de la orden
        /// El valor es el sufijo de la transaccion
        /// </summary> 
        public static Dictionary<int, string> MapeoTipoTransaccionVsCodigoDeOperacion = new Dictionary<int, string> {
            //WRAPPER'S
            {TransaccionSettings.Default.IdTipoTransaccionVentaYCobroEnBloque,"VCB"},
            {TransaccionSettings.Default.IdTipoTransaccionVenta,"V"},
            {TransaccionSettings.Default.IdTipoTransaccionCompra,"C"},
            {TransaccionSettings.Default.IdTipoTransaccionInvalidacionDeVenta,"IV"},
            {TransaccionSettings.Default.IdTipoTransaccionInvalidacionDeCompra,"IC"},
            //ORDEN
            {TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta,"OV"},
            {TransaccionSettings.Default.IdTipoTransaccionOrdenDeCompra,"OC"},
            {TransaccionSettings.Default.IdTipoTransaccionOrdenDeInvalidacionDeVenta,"OIV"},
            {TransaccionSettings.Default.IdTipoTransaccionOrdenDeInvalidacionDeCompra,"OIC"},
            {TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAlmacen,"OA"},
            //INGRESO MERCADERIA
            {TransaccionSettings.Default.IdTipoTransaccionIngresoBienesPorCompra,"IMC"},
            {TransaccionSettings.Default.IdTipoTransaccionIngresoDeMercaderiaPorAnulaciónDeVenta,"IMAV"},
            {TransaccionSettings.Default.IdTipoDeTransaccionIngresoDeMercaderiaPorAnulacionDeVentaPorErrorEnElRuc,"IMACER"},
            {TransaccionSettings.Default.IdTipoDeTransaccionIngresoDeMercaderiaPorDevolucionTotalDeVenta,"IMDTC"},
            {TransaccionSettings.Default.IdTipoDeTransaccionIngresoDeMercaderiaPorDevolucionPorItemEnVenta,"IMDIC"},
            {TransaccionSettings.Default.IdTipoTransaccionIngresoDeMercaderiaPorInvalidacionDeVenta,"IMIV"},
            //SALIDA MERCADERIA
            {TransaccionSettings.Default.IdTipoTransaccionSalidaBienesPorVenta,"SMV"},
            {TransaccionSettings.Default.IdTipoTransaccionSalidaDeMercaderiaPorAnulaciónDeCompra,"SMAC"},
            {TransaccionSettings.Default.IdTipoDeTransaccionSalidaDeMercaderiaPorAnulacionDeCompraPorErrorEnElRuc, "SMACER"},
            {TransaccionSettings.Default.IdTipoDeTransaccionSalidaDeMercaderiaPorDevolucionTotalDeCompra, "SMDTC"},
            {TransaccionSettings.Default.IdTipoDeTransaccionSalidaDeMercaderiaPorDevolucionPorItemEnCompra, "SMDIC"},
            {TransaccionSettings.Default.IdTipoTransaccionSalidaDeMercaderiaPorInvalidacionDeCompra, "SMIC"},

            {TransaccionSettings.Default.IdTipoTransaccionGasto,"G" },
            {TransaccionSettings.Default.IdTipoTransaccionOrdenGasto,"OG" },
            {TransaccionSettings.Default.IdTipoTransaccionPagoGasto,"PG" },

            {TransaccionSettings.Default.IdTipoTransaccionGuiaRemisionPorVenta, "GRV" },
            {TransaccionSettings.Default.IdTipoTransaccionGuiaRemisionPorVentaSujetaAConfirmacionDeComprador, "GRVSCC" },
            {TransaccionSettings.Default.IdTipoTransaccionGuiaRemisionPorCompra, "GRC" },
            {TransaccionSettings.Default.IdTipoTransaccionGuiaRemisionPorTrasladoEntreEstablecimientosDeLaMismaEmpresa, "GRTEEME" },
            {TransaccionSettings.Default.IdTipoTransaccionGuiaRemisionPorTrasladoEmisorItineranteCP, "GRTEI" },
            {TransaccionSettings.Default.IdTipoTransaccionGuiaRemisionPorImportacion, "GRI" },
            {TransaccionSettings.Default.IdTipoTransaccionGuiaRemisionPorExportacion, "GRE" },
            {TransaccionSettings.Default.IdTipoTransaccionGuiaRemisionPorTrasladoAZonaPrimaria, "GRTZP" },
            {TransaccionSettings.Default.IdTipoTransaccionGuiaRemisionPorOtrosMotivos, "GRO" },

            {TransaccionSettings.Default.IdTipoTransaccionConsumoHabitacion,"CH" },
            {TransaccionSettings.Default.IdTipoTransaccionOrdenConsumoHabitacion,"OCH" },
            {TransaccionSettings.Default.IdTipoTransaccionSalidaMercaderiaConsumoHabitacion,"SMCH" },

            {TransaccionSettings.Default.IdTipoTransaccionInvalidacionConsumoHabitacion,"ICH" },
            {TransaccionSettings.Default.IdTipoTransaccionOrdenInvalidacionConsumoHabitacion,"OICH" },
            {TransaccionSettings.Default.IdTipoTransaccionEntradaMercaderiaInvalidacionConsumoHabitacion,"SMICH" },

            {PedidoSettings.Default.IdTipoTransaccionPedido,"P" },
            {PedidoSettings.Default.IdTipoTransaccionOrdenPedido,"OP" }
        };

        /// <summary>
        /// La clave es el id de detalle de maestro
        /// El valor es el id de la tipo transaccion para transacciones que tendra compra
        /// </summary> 
        public static Dictionary<int, int> MapeoDetalleMaestroVsTipoTransaccionParaCompra = new Dictionary<int, int> {
            {MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaAnulacionDeLaOperacion,TransaccionSettings.Default.IdTipoTransaccionAnulacionDeCompra},
            {MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaAnulacionPorErrorEnElRuc,TransaccionSettings.Default.IdTipoDeTransaccionAnulacionDeCompraPorErrorEnElRuc},
            {MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaCorreccionPorErrorEnLaDescripcion,TransaccionSettings.Default.IdTipoDeTransaccionCorreccionDeCompraPorErrorEnLaDescripcion},
            {MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaDescuentoGlobal,TransaccionSettings.Default.IdTipoDeTransaccionDescuentoGlobalEnCompra},
            {MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaDescuentoPorItem,TransaccionSettings.Default.IdTipoDeTransaccionDescuentoPorItemEnCompra},
            {MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaDevolucionTotal,TransaccionSettings.Default.IdTipoDeTransaccionDevolucionTotalDeCompra},
            {MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaDevolucionPorItem,TransaccionSettings.Default.IdTipoDeTransaccionDevolucionPorItemEnCompra},
            {MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaBonificacion,1},//Todavia no se realiza
            {MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaDisminucionEnElValor,1},//Todavia no se realiza
            {MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaOtrosConceptos,1},//Todavia no se realiza
            {MaestroSettings.Default.IdDetalleMaestroNotaDeDebitoElectronicaInteresesPorMora,TransaccionSettings.Default.IdTipoDeTransaccionInteresesPorMoraEnCompra},
            {MaestroSettings.Default.IdDetalleMaestroNotaDeDebitoElectronicaAumentoEnElValor,TransaccionSettings.Default.IdTipoDeTransaccionAumentoEnElValorDeCompra},
            {MaestroSettings.Default.IdDetalleMaestroNotaDeDebitoElectronicaPenalidadesYOtrosConceptos,1},//Todavia no se realiza

        };
        /// <summary>
        /// La clave es el id de detalle de maestro
        /// El valor es el id de la tipo transaccion para transacciones que tendra venta
        /// </summary> 
        public static Dictionary<int, int> MapeoDetalleMaestroVsTipoTransaccionParaVenta = new Dictionary<int, int> {
            {MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaAnulacionDeLaOperacion,TransaccionSettings.Default.IdTipoTransaccionAnulacionDeVenta},
            {MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaAnulacionPorErrorEnElRuc,TransaccionSettings.Default.IdTipoDeTransaccionAnulacionDeVentaPorErrorEnElRuc},
            {MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaCorreccionPorErrorEnLaDescripcion,TransaccionSettings.Default.IdTipoDeTransaccionCorreccionDeVentaPorErrorEnLaDescripcion},
            {MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaDescuentoGlobal,TransaccionSettings.Default.IdTipoDeTransaccionDescuentoGlobalEnVenta},
            {MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaDescuentoPorItem,TransaccionSettings.Default.IdTipoDeTransaccionDescuentoPorItemEnVenta},
            {MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaDevolucionTotal,TransaccionSettings.Default.IdTipoDeTransaccionDevolucionTotalDeVenta},
            {MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaDevolucionPorItem,TransaccionSettings.Default.IdTipoDeTransaccionDevolucionPorItemEnVenta},
            {MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaBonificacion,1},//Todavia no se realiza
            {MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaDisminucionEnElValor,1},//Todavia no se realiza
            {MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaOtrosConceptos,1},//Todavia no se realiza
            {MaestroSettings.Default.IdDetalleMaestroNotaDeDebitoElectronicaInteresesPorMora,TransaccionSettings.Default.IdTipoDeTransaccionInteresesPorMoraEnVenta},
            {MaestroSettings.Default.IdDetalleMaestroNotaDeDebitoElectronicaAumentoEnElValor,TransaccionSettings.Default.IdTipoDeTransaccionAumentoEnElValorDeVenta},
            {MaestroSettings.Default.IdDetalleMaestroNotaDeDebitoElectronicaPenalidadesYOtrosConceptos,1},//Todavia no se realiza
           
        };

        public static Dictionary<int, int> MapeoOrdenVsInvalidacion = new Dictionary<int, int> {
            {TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta,TransaccionSettings.Default.IdTipoTransaccionInvalidacionDeVenta},
            {TransaccionSettings.Default.IdTipoTransaccionOrdenDeCompra,TransaccionSettings.Default.IdTipoTransaccionInvalidacionDeCompra},

            {TransaccionSettings.Default.IdTipoTransaccionOrdenDeAnulacionDeCompra,TransaccionSettings.Default.IdTipoTransaccionInvalidacionDeAnulacionDeCompra},
            {TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAnulacionDeCompraPorErrorEnElRuc,TransaccionSettings.Default.IdTipoTransaccionInvalidacionDeAnulacionDeCompra},
            {TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeCorreccionDeCompraPorErrorEnLaDescripcion,TransaccionSettings.Default.IdTipoTransaccionInvalidacionDeAnulacionDeCompra},
            {TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDescuentoGlobalEnCompra,TransaccionSettings.Default.IdTipoTransaccionInvalidacionDeAnulacionDeCompra},
            {TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDescuentoPorItemEnCompra,TransaccionSettings.Default.IdTipoTransaccionInvalidacionDeAnulacionDeCompra},
            {TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionTotalDeCompra,TransaccionSettings.Default.IdTipoTransaccionInvalidacionDeAnulacionDeCompra},
            {TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionPorItemEnCompra,TransaccionSettings.Default.IdTipoTransaccionInvalidacionDeAnulacionDeCompra},
            {TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeInteresesPorMoraEnCompra,TransaccionSettings.Default.IdTipoTransaccionInvalidacionDeAnulacionDeCompra},
            {TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAumentoEnElValorDeCompra,TransaccionSettings.Default.IdTipoTransaccionInvalidacionDeAnulacionDeCompra },

            {TransaccionSettings.Default.IdTipoTransaccionOrdenDeAnulacionDeVenta,TransaccionSettings.Default.IdTipoTransaccionInvalidacionDeAnulacionDeVenta},
            {TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAnulacionDeVentaPorErrorEnElRuc,TransaccionSettings.Default.IdTipoTransaccionInvalidacionDeAnulacionDeVenta},
            {TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeCorreccionDeVentaPorErrorEnLaDescripcion,TransaccionSettings.Default.IdTipoTransaccionInvalidacionDeAnulacionDeVenta},
            {TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDescuentoGlobalEnVenta,TransaccionSettings.Default.IdTipoTransaccionInvalidacionDeAnulacionDeVenta},
            {TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDescuentoPorItemEnVenta,TransaccionSettings.Default.IdTipoTransaccionInvalidacionDeAnulacionDeVenta},
            {TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionTotalDeVenta,TransaccionSettings.Default.IdTipoTransaccionInvalidacionDeAnulacionDeVenta},
            {TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionPorItemEnVenta,TransaccionSettings.Default.IdTipoTransaccionInvalidacionDeAnulacionDeVenta},
            {TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeInteresesPorMoraEnVenta,TransaccionSettings.Default.IdTipoTransaccionInvalidacionDeAnulacionDeVenta},
            {TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAumentoEnElValorDeVenta,TransaccionSettings.Default.IdTipoTransaccionInvalidacionDeAnulacionDeVenta },


        };


        public static int[] IdsTiposDeTransaccionOrdenesDeOperaciones = {
            TransaccionSettings.Default.IdTipoTransaccionOrdenDeCompra,
            TransaccionSettings.Default.IdTipoTransaccionOrdenDeInvalidacionDeCompra,
            TransaccionSettings.Default.IdTipoTransaccionOrdenDeAnulacionDeCompra,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAnulacionDeCompraPorErrorEnElRuc,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeCorreccionDeCompraPorErrorEnLaDescripcion,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDescuentoGlobalEnCompra,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDescuentoPorItemEnCompra,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionPorItemEnCompra,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionTotalDeCompra,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeInteresesPorMoraEnCompra,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAumentoEnElValorDeCompra,

            TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta,
            TransaccionSettings.Default.IdTipoTransaccionOrdenDeInvalidacionDeVenta,
            TransaccionSettings.Default.IdTipoTransaccionOrdenDeAnulacionDeVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAnulacionDeVentaPorErrorEnElRuc,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeCorreccionDeVentaPorErrorEnLaDescripcion,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDescuentoGlobalEnVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDescuentoPorItemEnVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionPorItemEnVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionTotalDeVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeInteresesPorMoraEnVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAumentoEnElValorDeVenta,

            TransaccionSettings.Default.IdTipoTransaccionOrdenConsumoHabitacion,
            TransaccionSettings.Default.IdTipoTransaccionOrdenInvalidacionConsumoHabitacion

        };


        public static int[] TiposDeTransaccionOrdenesDeOperacionesDeCompras = {
            TransaccionSettings.Default.IdTipoTransaccionOrdenDeCompra,
            //Tipo de transaccion generado al momento de invalidar una compra (Reestablecer en caja y almacen)
            TransaccionSettings.Default.IdTipoTransaccionOrdenDeInvalidacionDeCompra,
            //Notas de credito
            TransaccionSettings.Default.IdTipoTransaccionOrdenDeAnulacionDeCompra,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAnulacionDeCompraPorErrorEnElRuc,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeCorreccionDeCompraPorErrorEnLaDescripcion,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDescuentoGlobalEnCompra,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDescuentoPorItemEnCompra,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionPorItemEnCompra,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionTotalDeCompra,
            //Nota de Debito
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeInteresesPorMoraEnCompra,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAumentoEnElValorDeCompra,
        };


        public static int[] TiposDeTransaccionOrdenesDeOperacionesDeComprasExceptoInvalidacion = {
            TransaccionSettings.Default.IdTipoTransaccionOrdenDeCompra,
            //Notas de credito
            TransaccionSettings.Default.IdTipoTransaccionOrdenDeAnulacionDeCompra,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAnulacionDeCompraPorErrorEnElRuc,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeCorreccionDeCompraPorErrorEnLaDescripcion,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDescuentoGlobalEnCompra,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDescuentoPorItemEnCompra,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionPorItemEnCompra,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionTotalDeCompra,
            //Nota de Debito
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeInteresesPorMoraEnCompra,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAumentoEnElValorDeCompra,
        };


        public static int[] TiposDeTransaccionOrdenesDeOperacionesDeVentas = {
            TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta,
            //Tipo de transaccion generado al momento de invalidar una venta (Reestablecer en caja y almacen)
            TransaccionSettings.Default.IdTipoTransaccionOrdenDeInvalidacionDeVenta,
            //NOTAS DE CREDITO
            //Ahora es una nota de credito (anulacion de la operacion)
            TransaccionSettings.Default.IdTipoTransaccionOrdenDeAnulacionDeVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAnulacionDeVentaPorErrorEnElRuc,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeCorreccionDeVentaPorErrorEnLaDescripcion,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDescuentoGlobalEnVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDescuentoPorItemEnVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionPorItemEnVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionTotalDeVenta,
            //NOTAS DE DEBITO
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeInteresesPorMoraEnVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAumentoEnElValorDeVenta
        };

        public static int[] TiposDeTransaccionOrdenesDeOperacionesDeVentasSoloNotasDeCreditoYDebitoExceptoAnulacionDeVenta = {
            //NOTAS DE CREDITO
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAnulacionDeVentaPorErrorEnElRuc,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeCorreccionDeVentaPorErrorEnLaDescripcion,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDescuentoGlobalEnVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDescuentoPorItemEnVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionPorItemEnVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionTotalDeVenta,
            //NOTAS DE DEBITO
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeInteresesPorMoraEnVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAumentoEnElValorDeVenta
        };

        public static int[] TiposDeTransaccionOrdenesDeOperacionesDeVentasSoloNotasDeCreditoYDebito = {
            //NOTAS DE CREDITO
            TransaccionSettings.Default.IdTipoTransaccionOrdenDeAnulacionDeVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAnulacionDeVentaPorErrorEnElRuc,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeCorreccionDeVentaPorErrorEnLaDescripcion,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDescuentoGlobalEnVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDescuentoPorItemEnVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionPorItemEnVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionTotalDeVenta,
            //NOTAS DE DEBITO
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeInteresesPorMoraEnVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAumentoEnElValorDeVenta
        };

        public static int[] TiposDeTransaccionOrdenesDeOperacionesDeVentasSoloNotasDeCredito = {
            //NOTAS DE CREDITO
            TransaccionSettings.Default.IdTipoTransaccionOrdenDeAnulacionDeVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAnulacionDeVentaPorErrorEnElRuc,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeCorreccionDeVentaPorErrorEnLaDescripcion,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDescuentoGlobalEnVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDescuentoPorItemEnVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionPorItemEnVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionTotalDeVenta,

        };

        public static int[] TiposDeTransaccionOrdenesDeOperacionesDeVentasSoloNotasDeDebito = {
            //NOTAS DE DEBITO
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeInteresesPorMoraEnVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAumentoEnElValorDeVenta

        };

        public static int[] TiposDeTransaccionOrdenesDeOperacionesDeVentasExceptoInvalidaciones = {
            TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta,
            TransaccionSettings.Default.IdTipoTransaccionOrdenDeAnulacionDeVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAnulacionDeVentaPorErrorEnElRuc,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeCorreccionDeVentaPorErrorEnLaDescripcion,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDescuentoGlobalEnVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDescuentoPorItemEnVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionPorItemEnVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionTotalDeVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeInteresesPorMoraEnVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAumentoEnElValorDeVenta
        };



        public static int[] TiposDeTransaccionOrdenesDeOperacionesDeVentasDondeIngresaDineroYSalenBienes = new int[] {
            TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta,
        };

        /// <summary>
        /// Ordenes de intereses y aumentos en valor de venta
        /// </summary>
        public static int[] TiposDeTransaccionOrdenesDeOperacionesDeVentasDondeIngresaDinero = {
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeInteresesPorMoraEnVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAumentoEnElValorDeVenta,
        };

        public static int[] TiposDeTransaccionOrdenesDeOperacionesDeVentasDondeSaleDineroEIngresanBienes = {
            TransaccionSettings.Default.IdTipoTransaccionOrdenDeAnulacionDeVenta,
            TransaccionSettings.Default.IdTipoTransaccionOrdenDeInvalidacionDeVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAnulacionDeVentaPorErrorEnElRuc,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeCorreccionDeVentaPorErrorEnLaDescripcion,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDescuentoGlobalEnVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionPorItemEnVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionTotalDeVenta,
        };

        /// <summary>
        /// Ordenes de descuentos en ventas
        /// </summary>
        public static int[] TiposDeTransaccionOrdenesDeOperacionesDeVentasDondeSaleDinero = {
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDescuentoGlobalEnVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDescuentoPorItemEnVenta,
        };

        public static int[] TiposDeTransaccionOrdenesDeOperacionesDeVentasDondeSaleDineroEIngresanBienesExceptoInvalidaciones = {
            TransaccionSettings.Default.IdTipoTransaccionOrdenDeAnulacionDeVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAnulacionDeVentaPorErrorEnElRuc,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeCorreccionDeVentaPorErrorEnLaDescripcion,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDescuentoGlobalEnVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionPorItemEnVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionTotalDeVenta
        };

        public static int[] TiposDeTransaccionOrdenesDeOperacionesDeVentasDondeIngresaDinero_ = {
            TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeInteresesPorMoraEnVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAumentoEnElValorDeVenta,
        };

        public static int[] TiposDeTransaccionOrdenesDeOperacionesDeVentasDondeSaleDinero_ = {
            TransaccionSettings.Default.IdTipoTransaccionOrdenDeInvalidacionDeVenta,
            TransaccionSettings.Default.IdTipoTransaccionOrdenDeAnulacionDeVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAnulacionDeVentaPorErrorEnElRuc,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeCorreccionDeVentaPorErrorEnLaDescripcion,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDescuentoGlobalEnVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionPorItemEnVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionTotalDeVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDescuentoGlobalEnVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDescuentoPorItemEnVenta,
        };

        public static int[] TiposdeTransaccionInvalidacionesDeVentas = {
            TransaccionSettings.Default.IdTipoTransaccionOrdenDeInvalidacionDeVenta
        };



        public static int[] EstadosDeOperacionesValidasIgnorandoTransmitido = {
                MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado,
                MaestroSettings.Default.IdDetalleMaestroEstadoAnulado//aun cuando ha sido anulado con nota de credito, el comprobante sigue siendo válido
        };

        public static int[] EstadosDeOperacionesIgnorandoTransmitido = {
                MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado,
                MaestroSettings.Default.IdDetalleMaestroEstadoAnulado,
                MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado//aun cuando ha sido anulado con nota de credito, el comprobante sigue siendo válido
        };

        public static int[] TiposDeTransaccionOrdenesDeOperacionesDeMovimientoDeMercaderia = {
            TransaccionSettings.Default.IdTipoTransaccionOrdenDeDesplazamiento,

            TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta,
            TransaccionSettings.Default.IdTipoTransaccionOrdenDeInvalidacionDeVenta,
            TransaccionSettings.Default.IdTipoTransaccionOrdenDeAnulacionDeVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAnulacionDeVentaPorErrorEnElRuc,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeCorreccionDeVentaPorErrorEnLaDescripcion,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionPorItemEnVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionTotalDeVenta,

            TransaccionSettings.Default.IdTipoTransaccionOrdenDeCompra,
            TransaccionSettings.Default.IdTipoTransaccionOrdenDeInvalidacionDeCompra,
            TransaccionSettings.Default.IdTipoTransaccionOrdenDeAnulacionDeCompra,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAnulacionDeCompraPorErrorEnElRuc,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeCorreccionDeCompraPorErrorEnLaDescripcion,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionPorItemEnCompra,
            TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionTotalDeCompra,
        };

        public static int[] TiposDeComprobanteParaVenta = {
            MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta,
            MaestroSettings.Default.IdDetalleMaestroComprobanteFactura,
            MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito,
            MaestroSettings.Default.IdDetalleMaestroComprobanteNotaDeDebito,
            MaestroSettings.Default.IdDetalleMaestroComprobanteNotaVentaInterna,
            MaestroSettings.Default.IdDetalleMaestroComprobanteNotaInvalidacionVenta
        };

        public static int[] TiposDeComprobanteParaVentaExceptoNotaInvalidacion = {
            MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta,
            MaestroSettings.Default.IdDetalleMaestroComprobanteFactura,
            MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito,
            MaestroSettings.Default.IdDetalleMaestroComprobanteNotaDeDebito,
            MaestroSettings.Default.IdDetalleMaestroComprobanteNotaVentaInterna,
        };

        public static int[] TiposDeComprobanteParaCompra = {
            MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta,
            MaestroSettings.Default.IdDetalleMaestroComprobanteFactura,
            MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito,
            MaestroSettings.Default.IdDetalleMaestroComprobanteNotaDeDebito,
            MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCompraInterna,
            MaestroSettings.Default.IdDetalleMaestroComprobanteNotaInvalidacionCompra
        };

        public static int[] TiposDeComprobanteParaCompraExceptoNotaInvalidacion = {
            MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta,
            MaestroSettings.Default.IdDetalleMaestroComprobanteFactura,
            MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito,
            MaestroSettings.Default.IdDetalleMaestroComprobanteNotaDeDebito,
            MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCompraInterna,
        };

        public static int[] TiposDeComprobanteNoTributablesCompra = {
            MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCompraInterna,
        };

        public static int[] TiposDeComprobanteTributables = {
            MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta,
            MaestroSettings.Default.IdDetalleMaestroComprobanteFactura,
            MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito,
            MaestroSettings.Default.IdDetalleMaestroComprobanteNotaDeDebito
        };

        public static int[] TiposDeComprobanteTributablesMasGuiaRemision = {
            MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta,
            MaestroSettings.Default.IdDetalleMaestroComprobanteFactura,
            MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito,
            MaestroSettings.Default.IdDetalleMaestroComprobanteNotaDeDebito,
            MaestroSettings.Default.IdDetalleMaestroComprobanteGuiaDeRemisionRemitente
        };

        public static int[] TiposDeComprobanteTributablesExceptoNotasDeCreditoYDebito = {
            MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta,
            MaestroSettings.Default.IdDetalleMaestroComprobanteFactura,
        };

        public static int[] TiposDeComprobanteTributablesExceptoBoleta = {
            MaestroSettings.Default.IdDetalleMaestroComprobanteFactura,
            MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito,
            MaestroSettings.Default.IdDetalleMaestroComprobanteNotaDeDebito
        };

        public static int[] TiposDeComprobanteTributablesParaNotasDeCreditoYDebito = {
            MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito,
            MaestroSettings.Default.IdDetalleMaestroComprobanteNotaDeDebito
        };

        public static int[] TiposDeComprobanteDeIngresoDineroPorOperacionesDeVenta = {
            MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta,
            MaestroSettings.Default.IdDetalleMaestroComprobanteFactura,
            MaestroSettings.Default.IdDetalleMaestroComprobanteNotaDeDebito,
            MaestroSettings.Default.IdDetalleMaestroComprobanteNotaIngreso
        };

        public static int[] TiposDeTransaccionDeIngresoDeDineroPorOperacionesDeVenta = {
             TransaccionSettings.Default.IdTipoTransaccionCobranzaFacturasClientes,
             TransaccionSettings.Default.IdTipoDeTransaccionIngresoDeDineroPorInteresesPorMoraEnVenta,
             TransaccionSettings.Default.IdTipoDeTransaccionIngresoDeDineroPorAumentoEnElValorDeVenta,
        };

        public static int[] TiposDeTransaccionDeIngresoDeDineroProvenienteDeLosProveedores =
        {

           TransaccionSettings.Default.IdTipoTransaccionIngresoDeDineroPorAnulacionDeCompra,
           TransaccionSettings.Default.IdTipoDeTransaccionIngresoDeDineroPorAnulacionDeCompraPorErrorEnElRuc,
           TransaccionSettings.Default.IdTipoDeTransaccionIngresoDeDineroPorDescuentoGlobalEnCompra,
           TransaccionSettings.Default.IdTipoDeTransaccionIngresoDeDineroPorDescuentoPorItemEnCompra,
           TransaccionSettings.Default.IdTipoDeTransaccionIngresoDeDineroPorDevolucionTotalDeCompra,
           TransaccionSettings.Default.IdTipoDeTransaccionIngresoDeDineroPorDevolucionPorItemEnCompra,
            TransaccionSettings.Default.IdTipoTransaccionIngresoDeDineroPorInvalidacionDeCompra


        };

        public static int[] TiposDeTransaccionDeSalidaDeDineroHaciaLosProveedores =
        {
             TransaccionSettings.Default.IdTipoTransaccionPagoFacturasProveedores,
             TransaccionSettings.Default.IdTipoTransaccionPagoDePagoVarios,
             TransaccionSettings.Default.IdTipoDeTransaccionSalidaDeDineroPorInteresesPorMoraEnCompra,
             TransaccionSettings.Default.IdTipoDeTransaccionSalidaDeDineroPorAumentoEnElValorDeCompra ,
        };

        public static int[] TiposDeTransaccionDeIngresoDeDineroProvenienteDeLosClientes =
        {
             TransaccionSettings.Default.IdTipoTransaccionCobranzaFacturasClientes,
             TransaccionSettings.Default.IdTipoTransaccionCobroDeCobroVarios,
             TransaccionSettings.Default.IdTipoDeTransaccionIngresoDeDineroPorInteresesPorMoraEnVenta,
             TransaccionSettings.Default.IdTipoDeTransaccionIngresoDeDineroPorAumentoEnElValorDeVenta ,
        };

        public static int[] TiposDeTransaccionDeSalidaDeDineroHaciaLosClientes =
        {
            TransaccionSettings.Default.IdTipoTransaccionSalidaDeDineroPorAnulacionDeVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionSalidaDeDineroPorAnulacionDeVentaPorErrorEnElRuc,
            TransaccionSettings.Default.IdTipoDeTransaccionSalidaDeDineroPorDescuentoGlobalEnVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionSalidaDeDineroPorDescuentoPorItemEnVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionSalidaDeDineroPorDevolucionTotalDeVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionSalidaDeDineroPorDevolucionPorItemEnVenta,
            TransaccionSettings.Default.IdTipoTransaccionSalidaDeDineroPorInvalidacionDeVenta


        };

        public static int[] TiposDeTransaccionMovimientoDeBienes_Salidas =
         {
            TransaccionSettings.Default.IdTipoTransaccionSalidaBienesPorVenta,
            TransaccionSettings.Default.IdTipoTransaccionSalidaDeMercaderiaPorAnulaciónDeCompra,
            TransaccionSettings.Default.IdTipoTransaccionSalidaDeMercaderiaPorInvalidacionDeCompra,
            TransaccionSettings.Default.IdTipoDeTransaccionSalidaDeMercaderiaPorAnulacionDeCompraPorErrorEnElRuc,
            TransaccionSettings.Default.IdTipoDeTransaccionSalidaDeMercaderiaPorDevolucionPorItemEnCompra,
            TransaccionSettings.Default.IdTipoDeTransaccionSalidaDeMercaderiaPorDevolucionTotalDeCompra,
            TransaccionSettings.Default.IdTipoTransaccionSalidaMercaderíaDesplazamientoInterno,
            TransaccionSettings.Default.IdTipoTransaccionSalidaMercadInvalidacionDeAnulacionVenta,
            TransaccionSettings.Default.IdTipoTransaccionSalidaMercaderiaConsumoHabitacion,
            TransaccionSettings.Default.IdTipoTransaccionSalidaBienesAjusteInventario
        };

        public static int[] TiposDeTransaccionMovimientoDeBienes_Entradas =
 {
             TransaccionSettings.Default.IdTipoTransaccionIngresoBienesPorCompra,
             TransaccionSettings.Default.IdTipoTransaccionIngresoDeMercaderiaPorAnulaciónDeVenta,
             TransaccionSettings.Default.IdTipoTransaccionIngresoDeMercaderiaPorInvalidacionDeVenta,
             TransaccionSettings.Default.IdTipoDeTransaccionIngresoDeMercaderiaPorAnulacionDeVentaPorErrorEnElRuc,
             TransaccionSettings.Default.IdTipoDeTransaccionIngresoDeMercaderiaPorDevolucionPorItemEnVenta,
             TransaccionSettings.Default.IdTipoDeTransaccionIngresoDeMercaderiaPorDevolucionTotalDeVenta,
             TransaccionSettings.Default.IdTipoTransaccionIngresoMercadInvalidacionDeAnulacionCompra,
             TransaccionSettings.Default.IdTipoTransaccionIngresoMercaderíaDesplazamientoInterno,
             TransaccionSettings.Default.IdTipoTransaccionEntradaMercaderiaInvalidacionConsumoHabitacion,
             TransaccionSettings.Default.IdTipoTransaccionEntradaBienesAjusteInventario


        };

        public static int[] TiposDeTransaccionMovimientoDeBienes_Ventas = 
        {
            TransaccionSettings.Default.IdTipoTransaccionSalidaBienesPorVenta,
            TransaccionSettings.Default.IdTipoTransaccionIngresoDeMercaderiaPorInvalidacionDeVenta,
            TransaccionSettings.Default.IdTipoTransaccionIngresoDeMercaderiaPorAnulaciónDeVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionIngresoDeMercaderiaPorAnulacionDeVentaPorErrorEnElRuc,
            TransaccionSettings.Default.IdTipoDeTransaccionIngresoDeMercaderiaPorDevolucionPorItemEnVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionIngresoDeMercaderiaPorDevolucionTotalDeVenta,
            TransaccionSettings.Default.IdTipoTransaccionSalidaMercadInvalidacionDeAnulacionVenta,
        };

        public static int[] TiposDeTransaccionMovimientoDeBienes_Compras =
        {
            TransaccionSettings.Default.IdTipoTransaccionIngresoBienesPorCompra,
            TransaccionSettings.Default.IdTipoTransaccionSalidaDeMercaderiaPorInvalidacionDeCompra,
            TransaccionSettings.Default.IdTipoTransaccionSalidaDeMercaderiaPorAnulaciónDeCompra,
            TransaccionSettings.Default.IdTipoDeTransaccionSalidaDeMercaderiaPorAnulacionDeCompraPorErrorEnElRuc,
            TransaccionSettings.Default.IdTipoDeTransaccionSalidaDeMercaderiaPorDevolucionPorItemEnCompra,
            TransaccionSettings.Default.IdTipoDeTransaccionSalidaDeMercaderiaPorDevolucionTotalDeCompra,
            TransaccionSettings.Default.IdTipoTransaccionIngresoMercadInvalidacionDeAnulacionCompra,
        };


        public static int[] IdsFamilasANoMostrar
        {
            get
            {
                var IdsFamilasANoMostrar = new List<int>();
                var idsFamiliasNoMostrar = MaestroSettings.Default.IdsFamiliasANoMostrarEnSelectorFamilia.Split('|').ToArray();
                foreach (var idFamiliaNoMostrar in idsFamiliasNoMostrar)
                {
                    if (!string.IsNullOrEmpty(idFamiliaNoMostrar))
                        IdsFamilasANoMostrar.Add(Convert.ToInt32(idFamiliaNoMostrar));
                }
                return IdsFamilasANoMostrar.ToArray();
            }
        }



        //public static int[] TiposDeTransaccionOrdenesDeOperacionesQueGeneranIncrementoStockBienes = {

        //    TransaccionSettings.Default.IdTipoTransaccionOrdenDeCompra,
        //    TransaccionSettings.Default.IdTipoTransaccionOrdenDeInvalidacionDeVenta,
        //    TransaccionSettings.Default.IdTipoTransaccionOrdenDeAnulacionDeVenta,
        //    TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAnulacionDeVentaPorErrorEnElRuc,
        //    TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeCorreccionDeVentaPorErrorEnLaDescripcion,
        //    TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionPorItemEnVenta,
        //    TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionTotalDeVenta,
        //    TransaccionSettings.Default.IdTipoTransaccionOrdenDeDesplazamiento,
        //};

        //public static int[] TiposDeTransaccionOrdenesDeOperacionesQueGeneranDecrementoStockBienes = {
        //    TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta,
        //    TransaccionSettings.Default.IdTipoTransaccionOrdenDeInvalidacionDeCompra,
        //    TransaccionSettings.Default.IdTipoTransaccionOrdenDeAnulacionDeCompra,
        //    TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeAnulacionDeCompraPorErrorEnElRuc,
        //    TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeCorreccionDeCompraPorErrorEnLaDescripcion,
        //    TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionPorItemEnCompra,
        //    TransaccionSettings.Default.IdTipoDeTransaccionOrdenDeDevolucionTotalDeCompra,
        //};


        public static int[] TiposDeVentaConFechasPasadas = {
            (int)ModoOperacionEnum.VentaPorContingencia,
            (int)ModoOperacionEnum.VentaIntegradaMasivaDigitada,
            (int)ModoOperacionEnum.VentaCobranzaCarteraClientesDigitada
        };


        public static Dictionary<char, string> ValoresMascaraConceptoNegocioVenta = new Dictionary<char, string> {
            {'1',"CodigoBarra"},
            {'2',"NombreConceptoBasico"},
            {'3',"Sufijo"},
            {'4',"ValoresCaracteristicasComunes"},
            {'5',"Presentacion"},
            {'6',"NombreUnidadMedidaComercial"},
            {'7',"NombreDeTarifasYPrecios"},
        };

        public static Dictionary<char, string> ValoresMascaraConceptoNegocioCompra = new Dictionary<char, string> {
            {'1',"CodigoBarra"},
            {'2',"NombreConceptoBasico"},
            {'3',"Sufijo"},
            {'4',"ValoresCaracteristicasComunes"},
            {'5',"Presentacion"},
            {'6',"NombreUnidadMedidaComercial"},
        };


        public static Dictionary<int, string> ValoresMascaraReporteMensualDeEnvioAutomatico = new Dictionary<int, string> {
            {1,"Reporte de puntos de venta por comprobante"},
            {2,"Reporte de serie por comprobante"},
            {3,"Libro electronico de ventas en excel"},
            {4,"Libro electronico de ventas en txt"},
            {5,"Reporte Adsoft"},
            {6,"Reporte Foxcom"},
            {7,"Reporte de punto de venta por comprobante con icbper"},
            {8,"Libro electronico de ventas sin conceptos"},
            {9,"Reporte de insumos controlados"},
            {10,"Reporte de ventas en excel"},
        };

        public static Dictionary<int, int> ValoresMascaraMedioPagoAMostrarEnVentas = new Dictionary<int, int> {
            {0, MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo},
            {1, MaestroSettings.Default.IdDetalleMaestroMedioDePagoTarjetaCredito},
            {2, MaestroSettings.Default.IdDetalleMaestroMedioDePagoTarjetaDebito},
            {3, MaestroSettings.Default.IdDetalleMaestroMedioDepagoTransferenciaDeFondos},
            {4, MaestroSettings.Default.IdDetalleMaestroMedioDepagoDepositoEnCuenta},
            {5, MaestroSettings.Default.IdDetalleMaestroMedioDepagoPuntos},
        };

        public static int[] IdsMediosDePagoQueTienenEntidadBancaria = {
            (int)MaestroSettings.Default.IdDetalleMaestroMedioDePagoTarjetaCredito,
            (int)MaestroSettings.Default.IdDetalleMaestroMedioDePagoTarjetaDebito,
            (int)MaestroSettings.Default.IdDetalleMaestroMedioDepagoTransferenciaDeFondos,
            (int)MaestroSettings.Default.IdDetalleMaestroMedioDepagoDepositoEnCuenta,

        };


        //public static Dictionary<string, string> NombreVistaEnVenta = new Dictionary<string, string> {
        //    {string.Concat(MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta,FormatoImpresion._80mm),"BoletaDeVenta80"},
        //    {string.Concat(MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta,FormatoImpresion.A4),"BoletaDeVentaA4"},
        //    {string.Concat(MaestroSettings.Default.IdDetalleMaestroComprobanteFactura,FormatoImpresion._80mm),"Factura80"},
        //    {string.Concat(MaestroSettings.Default.IdDetalleMaestroComprobanteFactura,FormatoImpresion.A4),"FacturaA4"},
        //    {string.Concat(MaestroSettings.Default.IdDetalleMaestroComprobanteNotaVentaInterna,FormatoImpresion._80mm),"NotaDeVenta80"},
        //    {string.Concat(MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito,MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta,FormatoImpresion._80mm),"../NotaCreditoDebito/NotaDeCreditoBoleta80"},
        //    {string.Concat(MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito,MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta,FormatoImpresion.A4),"../NotaCreditoDebito/NotaDeCreditoBoletaA4"},
        //    {string.Concat(MaestroSettings.Default.IdDetalleMaestroComprobanteNotaDeDebito,MaestroSettings.Default.IdDetalleMaestroComprobanteFactura,FormatoImpresion._80mm),"../NotaCreditoDebito/NotaDeDebitoBoleta80"},
        //    {string.Concat(MaestroSettings.Default.IdDetalleMaestroComprobanteNotaDeDebito,MaestroSettings.Default.IdDetalleMaestroComprobanteFactura,FormatoImpresion.A4),"../NotaCreditoDebito/NotaDeDebitoBoletaA4"},
        //    {string.Concat(MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito,MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta,FormatoImpresion._80mm),"../NotaCreditoDebito/NotaDeCreditoFactura80"},
        //    {string.Concat(MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito,MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta,FormatoImpresion.A4),"../NotaCreditoDebito/NotaDeCreditoFacturaA4"},
        //    {string.Concat(MaestroSettings.Default.IdDetalleMaestroComprobanteNotaDeDebito,MaestroSettings.Default.IdDetalleMaestroComprobanteFactura,FormatoImpresion._80mm),"../NotaCreditoDebito/NotaDeDebitoFactura80"},
        //    {string.Concat(MaestroSettings.Default.IdDetalleMaestroComprobanteNotaDeDebito,MaestroSettings.Default.CodigoDetalleMaestroFactura,FormatoImpresion.A4),"../NotaCreditoDebito/NotaDeDebitoFacturaA4"},

        //};

        //public static Dictionary<string, string> NombreVistaEnCompra = new Dictionary<string, string> {
        //    {string.Concat(MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta,FormatoImpresion._80mm),"BoletaDeCompra80"},
        //    {string.Concat(MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta,FormatoImpresion.A4),"BoletaDeCompraA4"},
        //    {string.Concat(MaestroSettings.Default.IdDetalleMaestroComprobanteFactura,FormatoImpresion._80mm),"FacturaDeCompra80"},
        //    {string.Concat(MaestroSettings.Default.IdDetalleMaestroComprobanteFactura,FormatoImpresion.A4),"FacturaDeCompraA4"},
        //    {string.Concat(MaestroSettings.Default.IdDetalleMaestroComprobanteNotaVentaInterna,FormatoImpresion._80mm),"NotaDeCompra80"},
        //    {string.Concat(MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito,MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta,FormatoImpresion._80mm),"../NotaCreditoDebito/NotaDeCreditoBoletaDeCompra80"},
        //    {string.Concat(MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito,MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta,FormatoImpresion.A4),"../NotaCreditoDebito/NotaDeCreditoBoletaDeCompraA4"},
        //    {string.Concat(MaestroSettings.Default.IdDetalleMaestroComprobanteNotaDeDebito,MaestroSettings.Default.IdDetalleMaestroComprobanteFactura,FormatoImpresion._80mm),"../NotaCreditoDebito/NotaDeDebitoBoletaDeCompra80"},
        //    {string.Concat(MaestroSettings.Default.IdDetalleMaestroComprobanteNotaDeDebito,MaestroSettings.Default.IdDetalleMaestroComprobanteFactura,FormatoImpresion.A4),"../NotaCreditoDebito/NotaDeDebitoBoletaDeCompraA4"},
        //    {string.Concat(MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito,MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta,FormatoImpresion._80mm),"../NotaCreditoDebito/NotaDeCreditoFacturaDeCompra80"},
        //    {string.Concat(MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito,MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta,FormatoImpresion.A4),"../NotaCreditoDebito/NotaDeCreditoFacturaDeCompraA4"},
        //    {string.Concat(MaestroSettings.Default.IdDetalleMaestroComprobanteNotaDeDebito,MaestroSettings.Default.IdDetalleMaestroComprobanteFactura,FormatoImpresion._80mm),"../NotaCreditoDebito/NotaDeDebitoFacturaDeCompra80"},
        //    {string.Concat(MaestroSettings.Default.IdDetalleMaestroComprobanteNotaDeDebito,MaestroSettings.Default.CodigoDetalleMaestroFactura,FormatoImpresion.A4),"../NotaCreditoDebito/NotaDeDebitoFacturaDeCompraA4"},

        //};

        public static Dictionary<string, int> ValoresClaseDeActorSunatConIdSiges = new Dictionary<string, int> {
            {"2",1},
            {"3",2},
            {"EMPRESA INDIVIDUAL DE RESP. LTDA",3},
            {"SOCIEDAD ANONIMA CERRADA",4},
            {"SOCIEDAD ANONIMA",10},
            {"SOCIEDAD ANONIMA ABIERTA",11},
            {"SOC.COM.RESPONS. LTDA",4}
        };

        #region DICCIONARIOS PARA EL CALCULO DE LOS VALORES DE LOS DETALLES DE MOVIMIENTO DE ALMACEN - INVENTARIO VALORIZADO

        /// <summary>
        /// Vienen a ser las transacciones de movimiento las cuales al momento de realizar es nesesario el calculo de sus detalles de acuerdo al valor que tendan en el inventario 
        /// Ejemplo: al momento de realizar una venta se tiene en la orden con sus detalles, pero para podeer obtener ultilidades netas de las operaciones se nesecita las los costos de lo veendido ahi se obtiene los costos unitarios de los inventarios.
        /// </summary>
        public static int[] TiposDeTransaccionMovimientoDeBienesConCostoUnitarioSegunInventario =
        {
            TransaccionSettings.Default.IdTipoTransaccionSalidaBienesPorVenta,
            TransaccionSettings.Default.IdTipoTransaccionSalidaMercaderíaDesplazamientoInterno
        };
        /// <summary>
        /// Vienen a ser transacciones de movimiento las cuales sus costos unitarios son los mismos que de la orden de operacion 
        /// Ejemplo: al momento de realizar una compra ingresa al inventario con el costo de la misma orden de operacion
        /// </summary>
        public static int[] TiposDeTransaccionMovimientoDeBienesConCostoUnitarioObtenidoDeLaOrden =
        {
            TransaccionSettings.Default.IdTipoTransaccionIngresoBienesPorCompra,
            TransaccionSettings.Default.IdTipoTransaccionIngresoMercaderíaDesplazamientoInterno
        };
        /// <summary>
        /// Vienen a ser transacciones de movimiento las cuales sus costos unitarios de movimeinto son dependientes de una operacion origen, se aplica mas que todo en las invalidaciones o emisiones de notas de credito
        /// Ejemplo: Al momento de realizar una invalidacion de venta se tiene que obtener el mismo costo unitario que el moviemiento de la venta.
        /// </summary>
        public static int[] TiposDeTransaccionMovimientoDeBienesConCostoUnitarioObtenidoDeMovimientoDeTransaccionOrigen =
        {
            TransaccionSettings.Default.IdTipoTransaccionIngresoDeMercaderiaPorInvalidacionDeVenta,
            TransaccionSettings.Default.IdTipoTransaccionIngresoDeMercaderiaPorAnulaciónDeVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionIngresoDeMercaderiaPorAnulacionDeVentaPorErrorEnElRuc,
            TransaccionSettings.Default.IdTipoDeTransaccionIngresoDeMercaderiaPorDevolucionPorItemEnVenta,
            TransaccionSettings.Default.IdTipoDeTransaccionIngresoDeMercaderiaPorDevolucionTotalDeVenta,
            TransaccionSettings.Default.IdTipoTransaccionSalidaMercadInvalidacionDeAnulacionVenta,

            TransaccionSettings.Default.IdTipoTransaccionSalidaDeMercaderiaPorInvalidacionDeCompra,
            TransaccionSettings.Default.IdTipoTransaccionSalidaDeMercaderiaPorAnulaciónDeCompra,
            TransaccionSettings.Default.IdTipoDeTransaccionSalidaDeMercaderiaPorAnulacionDeCompraPorErrorEnElRuc,
            TransaccionSettings.Default.IdTipoDeTransaccionSalidaDeMercaderiaPorDevolucionPorItemEnCompra,
            TransaccionSettings.Default.IdTipoDeTransaccionSalidaDeMercaderiaPorDevolucionTotalDeCompra,
            TransaccionSettings.Default.IdTipoTransaccionIngresoMercadInvalidacionDeAnulacionCompra
        };

        public static int[] TiposDeTransaccionAjusteDeInventario =
        {
            TransaccionSettings.Default.IdTipoTransaccionEntradaBienesAjusteInventario,
            TransaccionSettings.Default.IdTipoTransaccionSalidaBienesAjusteInventario
        };

        #endregion

        /// <summary>
        /// La clave es el id de detalle de maestro del motivo de traslado 
        /// El valor es el id de tipo de transaccion de
        /// </summary> 
        public static Dictionary<int, int> MapeoMotivoTrasladoGuiaRemisionVsTipoTransaccion = new Dictionary<int, int> {
            {MaestroSettings.Default.IdDetalleMaestroMotivoDeTrasladoPorVenta,TransaccionSettings.Default.IdTipoTransaccionGuiaRemisionPorVenta},
            {MaestroSettings.Default.IdDetalleMaestroMotivoDeTrasladoPorVentaSujetaAConfirmacionDeComprador,TransaccionSettings.Default.IdTipoTransaccionGuiaRemisionPorVentaSujetaAConfirmacionDeComprador},
            {MaestroSettings.Default.IdDetalleMaestroMotivoDeTrasladoPorCompra,TransaccionSettings.Default.IdTipoTransaccionGuiaRemisionPorCompra},
            {MaestroSettings.Default.IdDetalleMaestroMotivoDeTrasladoPorTrasladoEntreEstablecimientosDeLaMismaEmpresa, TransaccionSettings.Default.IdTipoTransaccionGuiaRemisionPorTrasladoEntreEstablecimientosDeLaMismaEmpresa},
            {MaestroSettings.Default.IdDetalleMaestroMotivoDeTrasladoPorTrasladoEmisorItineranteCP,TransaccionSettings.Default.IdTipoTransaccionGuiaRemisionPorTrasladoEmisorItineranteCP},
            {MaestroSettings.Default.IdDetalleMaestroMotivoDeTrasladoPorImportacion,TransaccionSettings.Default.IdTipoTransaccionGuiaRemisionPorImportacion},
            {MaestroSettings.Default.IdDetalleMaestroMotivoDeTrasladoPorExportacion,TransaccionSettings.Default.IdTipoTransaccionGuiaRemisionPorExportacion},
            {MaestroSettings.Default.IdDetalleMaestroMotivoDeTrasladoPorTrasladoAZonaPrimaria,TransaccionSettings.Default.IdTipoTransaccionGuiaRemisionPorTrasladoAZonaPrimaria},
            {MaestroSettings.Default.IdDetalleMaestroMotivoDeTrasladoOtros,TransaccionSettings.Default.IdTipoTransaccionGuiaRemisionPorOtrosMotivos},
        };

        /// <summary>
        /// Vienen a ser transacciones de movimiento las cuales sus costos unitarios de movimeinto son dependientes de una operacion origen, se aplica mas que todo en las invalidaciones o emisiones de notas de credito
        /// Ejemplo: Al momento de realizar una invalidacion de venta se tiene que obtener el mismo costo unitario que el moviemiento de la venta.
        /// </summary>
        public static int[] TiposDeTransaccionGuiasDeRemision =
        {
            TransaccionSettings.Default.IdTipoTransaccionGuiaRemisionPorVenta,
            TransaccionSettings.Default.IdTipoTransaccionGuiaRemisionPorVentaSujetaAConfirmacionDeComprador,
            TransaccionSettings.Default.IdTipoTransaccionGuiaRemisionPorCompra,
            TransaccionSettings.Default.IdTipoTransaccionGuiaRemisionPorTrasladoEntreEstablecimientosDeLaMismaEmpresa,
            TransaccionSettings.Default.IdTipoTransaccionGuiaRemisionPorTrasladoEmisorItineranteCP,
            TransaccionSettings.Default.IdTipoTransaccionGuiaRemisionPorImportacion,
            TransaccionSettings.Default.IdTipoTransaccionGuiaRemisionPorExportacion,
            TransaccionSettings.Default.IdTipoTransaccionGuiaRemisionPorTrasladoAZonaPrimaria,
            TransaccionSettings.Default.IdTipoTransaccionGuiaRemisionPorOtrosMotivos,
        };

        public static int[] TiposDeDocumentoIdentidadParaTipoActorNaturalJuridico =
        {
            ActorSettings.Default.IdTipoDocumentoIdentidadDocTribNoDomSinRuc,
            ActorSettings.Default.IdTipoDocumentoIdentidadDni,
            ActorSettings.Default.IdTipoDocumentoIdentidadCarnetExtranjeria,
            ActorSettings.Default.IdTipoDocumentoIdentidadRuc,
            ActorSettings.Default.IdTipoDocumentoIdentidadPasaporte,
            ActorSettings.Default.IdTipoDocumentoIdentidadCedulaDiplomatica,
            ActorSettings.Default.IdTipoDocumentoIdentidadDocIdentPaisResidencia,
            ActorSettings.Default.IdTipoDocumentoIdentidadTaxIdentificationNumber,
            ActorSettings.Default.IdTipoDocumentoIdentidadIdentificationNumber,
            ActorSettings.Default.IdTipoDocumentoIdentidadTarjetaAndinaMigracion,
            ActorSettings.Default.IdTipoDocumentoIdentidadPermisoTemporalPermanencia,
            ActorSettings.Default.IdTipoDocumentoIdentidadSalvoConducto,
        };
        public static string[] CodigoFESunatConsideradosAceptadosEnSiges =
        {
            CodigoFESunatSettings.Default.C1032,
            CodigoFESunatSettings.Default.C2106,
            CodigoFESunatSettings.Default.C2223,
            CodigoFESunatSettings.Default.C2282,
            CodigoFESunatSettings.Default.C2323,
            CodigoFESunatSettings.Default.C2324,
            CodigoFESunatSettings.Default.C2987,
        };

        public static int[] IdsRolesGrupoActorComercial =
        {
            ActorSettings.Default.IdRolGrupoClientes
        };

        public static Dictionary<int, int> MapeoRolActorComercialVsIdRolGrupoActorComercial = new Dictionary<int, int> {
            {ActorSettings.Default.IdRolCliente, ActorSettings.Default.IdRolGrupoClientes },
        };

        public static Dictionary<int, int> MapeoModuloVsRolNegocio = new Dictionary<int, int> {
            { (int)ModulosAdicionales.Restaurante, (AplicacionSettings.Default.MascaraModulosAdicionalesHabilitados.ToCharArray()[0] == '1') ? RestauranteSettings.Default.IdRolConceptoRestaurante : 0},
            { (int)ModulosAdicionales.Hotel, (AplicacionSettings.Default.MascaraModulosAdicionalesHabilitados.ToCharArray()[1] == '1') ? HotelSettings.Default.IdRolConceptoHotel : 0},
            { (int)ModulosAdicionales.Cochera, (AplicacionSettings.Default.MascaraModulosAdicionalesHabilitados.ToCharArray()[2] == '1') ? CocheraSettings.Default.IdRolConceptoCochera : 0},
            { (int)ModulosAdicionales.Insumo, (AplicacionSettings.Default.MascaraModulosAdicionalesHabilitados.ToCharArray()[3] == '1') ? RestauranteSettings.Default.IdRolConceptoInsumo : 0},
        };

        public static Dictionary<int, bool> MapeoOperacionesGruposVsPermitirGrupos = new Dictionary<int, bool> {
            { (int)OperacionesGruposActoresComerciales.Venta, (AplicacionSettings.Default.MascaraGruposActoresComerialesActivados.ToCharArray()[0] == '1') },
            { (int)OperacionesGruposActoresComerciales.Preventa, (AplicacionSettings.Default.MascaraGruposActoresComerialesActivados.ToCharArray()[1] == '1') },
            { (int)OperacionesGruposActoresComerciales.Cotizacion, (AplicacionSettings.Default.MascaraGruposActoresComerialesActivados.ToCharArray()[2] == '1') },
            { (int)OperacionesGruposActoresComerciales.Compra, (AplicacionSettings.Default.MascaraGruposActoresComerialesActivados.ToCharArray()[3] == '1') },
            { (int)OperacionesGruposActoresComerciales.Gasto, (AplicacionSettings.Default.MascaraGruposActoresComerialesActivados.ToCharArray()[4] == '1') },
        };
    }
}

    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class ConfiguracionDeCompra
    {
        public ModoOperacionEnum tipoDeCompra;
        public ModoPago modoDePago;
        public bool hayIngresoDeDinero;
        public bool haySalidaDeMercaderia;
        public int idAccionARealizar;
        public int idEstadoTransaccion;
        public string observacionEstadoTransaccion;
        public int idMedioDePago;
        public int idEntidadFinanciera;
        public string informacionDePago;

        public bool esPropioIngresoMercaderia;
        public int idTipoComprobanteIngresoMercaderia;
        public int idSerieComprobanteIngresoMercaderia;
        public string serieDeComprobanteIngresoMercaderia;
        public int numeroDeComprobanteIngresoMercaderia;
        public DateTime? fechaInicioTransporte;
        public int idTransportista;
        public string placaYMarcaTransporte;
        public string numeroLicenciaTransporte;
        public int idModalidadTransaporte;
        public int idMotivoTransaporte;
        public string direccionOrigenTraslado;
        public int idUbigeoOrigenTraslado;
        public string direccionDestinoTraslado;
        public int idUbigeoDestinoTraslado;
        public string observacionIngresoMercaderia;
        public bool ingresoTotalOrden;
        public bool usaComprobanteDeOrden;

        public ConfiguracionDeCompra()
        {
            this.tipoDeCompra = 0;
            this.modoDePago = 0;
            this.hayIngresoDeDinero = false;
            this.haySalidaDeMercaderia = false;
            this.idAccionARealizar = 0;
            this.idEstadoTransaccion = 0;
            this.observacionEstadoTransaccion = null;
            this.idMedioDePago = 0;
            this.idEntidadFinanciera = 0;
            this.informacionDePago = null;

            this.esPropioIngresoMercaderia = false;
            this.idTipoComprobanteIngresoMercaderia = 0;
            this.idSerieComprobanteIngresoMercaderia = 0;
            this.serieDeComprobanteIngresoMercaderia = null;
            this.numeroDeComprobanteIngresoMercaderia = 0;
            this.fechaInicioTransporte = null;
            this.idTransportista = 0;
            this.placaYMarcaTransporte = null;
            this.numeroLicenciaTransporte = null;
            this.idModalidadTransaporte = 0;
            this.idMotivoTransaporte = 0;
            this.direccionOrigenTraslado = null;
            this.idUbigeoOrigenTraslado = 0;
            this.direccionDestinoTraslado = null;
            this.idUbigeoDestinoTraslado = 0;
            this.observacionIngresoMercaderia = null;
            this.ingresoTotalOrden = false;
            this.usaComprobanteDeOrden = false;

        }

        public static ConfiguracionDeCompra CompraPorMostrandorConfirmadaAlContado()
        {
            return new ConfiguracionDeCompra()
            {
                tipoDeCompra = ModoOperacionEnum.PorMostrador,
                modoDePago = ModoPago.Contado,
                hayIngresoDeDinero = true,
                haySalidaDeMercaderia = true,
                idAccionARealizar = MaestroSettings.Default.IdDetalleMaestroAccionConfirmar,
                idEstadoTransaccion = MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado,
                observacionEstadoTransaccion = "Estado inicial asignado automaticamente al confirmar una compra",
                idMedioDePago = MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo,
                idEntidadFinanciera = MaestroSettings.Default.IdDetalleMaestroEntidadBancariaPorDefecto,
                informacionDePago = "Pago efectivo",
            };
        }
        public static ConfiguracionDeCompra CompraPorMostrandorConfirmadaACreditoRapido()
        {
            return new ConfiguracionDeCompra()
            {
                tipoDeCompra = ModoOperacionEnum.PorMostrador,
                modoDePago = ModoPago.CreditoRapido,
                hayIngresoDeDinero = true,
                haySalidaDeMercaderia = true,
                idAccionARealizar = MaestroSettings.Default.IdDetalleMaestroAccionConfirmar,
                idEstadoTransaccion = MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado,
                observacionEstadoTransaccion = "Estado inicial asignado automaticamente al confirmar una compra",
                idMedioDePago = MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo,
                idEntidadFinanciera = MaestroSettings.Default.IdDetalleMaestroEntidadBancariaPorDefecto,
                informacionDePago = "Pago efectivo",
            };
        }
        public static ConfiguracionDeCompra CompraPorMostrandorConfirmadaACredito()
        {
            return new ConfiguracionDeCompra()
            {
                tipoDeCompra = ModoOperacionEnum.PorMostrador,
                modoDePago = ModoPago.CreditoConfigurado,
                hayIngresoDeDinero = true,
                haySalidaDeMercaderia = true,
                idAccionARealizar = MaestroSettings.Default.IdDetalleMaestroAccionConfirmar,
                idEstadoTransaccion = MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado,
                observacionEstadoTransaccion = "Estado inicial asignado automaticamente al confirmar una compra",
                idMedioDePago = MaestroSettings.Default.IdDetalleMaestroMedioDepagoEfectivo,
                idEntidadFinanciera = MaestroSettings.Default.IdDetalleMaestroEntidadBancariaPorDefecto,
                informacionDePago = "Pago efectivo",
            };
        }
      
        public static ConfiguracionDeCompra CompraCorporativaConfirmadaAlContado()
        {
            return new ConfiguracionDeCompra()
            {
                tipoDeCompra = ModoOperacionEnum.Corporativa,
                modoDePago = ModoPago.Contado,
                idAccionARealizar = MaestroSettings.Default.IdDetalleMaestroAccionConfirmar,
                idEstadoTransaccion = MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado,
                observacionEstadoTransaccion = "Estado inicial asignado automaticamente al confirmar una compra",
            };
        }

        public static ConfiguracionDeCompra CompraCorporativaConfirmadaACreditoRapido()
        {
            return new ConfiguracionDeCompra()
            {
                tipoDeCompra = ModoOperacionEnum.Corporativa,
                modoDePago = ModoPago.CreditoRapido,
                idAccionARealizar = MaestroSettings.Default.IdDetalleMaestroAccionConfirmar,
                idEstadoTransaccion = MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado,
                observacionEstadoTransaccion = "Estado inicial asignado automaticamente al confirmar una compra",
            };
        }
        public static ConfiguracionDeCompra CompraCorporativaConfirmadaACredito()
        {
            return new ConfiguracionDeCompra()
            {
                tipoDeCompra = ModoOperacionEnum.Corporativa,
                modoDePago = ModoPago.CreditoConfigurado,
                idAccionARealizar = MaestroSettings.Default.IdDetalleMaestroAccionConfirmar,
                idEstadoTransaccion = MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado,
                observacionEstadoTransaccion = "Estado inicial asignado automaticamente al confirmar una compra",
            };
        }
    }
}


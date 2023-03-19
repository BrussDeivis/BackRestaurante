using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.Entidades.ComprobantesModel
{
    public class DocumentoDeRetencion : DocumentoElectronicoImpreso
    {
        public override string NombreTipo { get; set; } = "DOCUMENTO DE RETENCIÓN";
        public decimal Igv { get; set; }

        public string PlacaDelVehiculo { get; set; }
        public bool TieneGuiaDeRemision { get; set; }
        public string TipoDeDocumentoGuiaDeRemision { get; set; }
        public string SerieDelDocumentoGuiaDeRemision { get; set; }
        public string NumeroDelDocumentoGuiaDeRemision { get; set; }
        /*
        public static DocumentoDeRetencion Convert(OperacionDeVenta operacion, ActorComercial sede, byte[] qrBytes, bool mostrarEncabezadoTestigo, ModoImpresionCaracteristicasEnum modoImpresionCaracteristicas)
        {
            return new DocumentoDeRetencion
            {
                IdOrden = operacion.Id,
                FechaEmision = operacion.FechaEmision,
                Emisor = new Emisor
                {
                    RazonSocial = sede.RazonSocial,
                    NombreComercial = sede.NombreComercial,
                    NumeroDocumentoIdentidad = sede.DocumentoIdentidad,
                    LogoBytes = sede.Logo(),
                    Direccion = sede.DomicilioFiscal().detalle,
                    OtrosDatosContacto = (String.IsNullOrEmpty(sede.Telefono()) ? "" : ("TF: " + sede.Telefono())) + Environment.NewLine + (String.IsNullOrEmpty(sede.Correo()) ? "" : ("Email: " + sede.Correo())),
                    Publicidad = sede.InformacionPublicitaria(),
                    CodigoSunatTipoDocumentoIdentidad = sede.CodigoSunatTipoDocumentoIdentidad()
                },
                Receptor = new Receptor
                {
                    RazonSocial = (operacion.Cliente().RazonSocial + operacion.AliasCliente()),
                    TipoDocumentoIdentidad = operacion.TipoDocumentoIdentidadCliente,
                    DocumentoIdentidad = operacion.NumeroDocumentoIdentidadCliente,
                    DocumentoIdentidadParaSunat = operacion.Cliente().DocumentoIdentidad,
                    Direccion = operacion.Cliente().DomicilioFiscal() != null ? operacion.Cliente().DomicilioFiscal().detalle : "",
                    CodigoSunatTipoDocumentoIdentidad = operacion.Cliente().CodigoSunatTipoDocumentoIdentidad()
                },
                Detalles = Detalle.Convert(operacion.Detalles(), modoImpresionCaracteristicas, operacion.DetalleUnificado()),
                MostrarMensajeAmazonia = operacion.AplicaLeyDeAmazonia,
                MostrarMensajeNegocio = AplicacionSettings.Default.MostrarMensajeDeNegocio,
                MensajeNegocio = AplicacionSettings.Default.MensajeDeNegocio,
                Observacion = operacion.Observacion() ?? "-",
                MostrarLogo = FacturacionElectronicaSettings.Default.MostrarLogoEnComprobanteImpreso,
                CodigoQR = qrBytes,
                CodigoSunatMoneda = operacion.Moneda().Codigo,
                CodigoSunatTipo = operacion.Comprobante().CodigoTipo,
                Serie = operacion.Comprobante().NumeroDeSerie,
                Numero = operacion.Comprobante().NumeroDeComprobante,
                ImporteTotal = operacion.Total,
                ImporteOperacionExonerada = operacion.ImporteTotalOperacionExonerada,
                ImporteOperacionInafecta = operacion.ImporteTotalOperacionInafecta,
                ImporteOperacionGravada = operacion.BaseImponibleOperacionGravada,
                ImporteTotalEnLetras = Util.APalabras(operacion.Total, operacion.MonedaPlural()),
                Descuento = operacion.Descuento(),
                Igv = operacion.Igv(),
                MostrarTestigo = mostrarEncabezadoTestigo,
                ResolucionAutorizacionSunat = FacturacionElectronicaSettings.Default.ResolucionEmisionElectronica,
                IdEstadoActual = operacion.IdEstadoActual,
                EsInvalidada = operacion.EsInvalidada,
            };
        }*/
    }
}

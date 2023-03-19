using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.Entidades.ComprobantesModel
{
    public class NotaDeVenta : DocumentoElectronicoVenta
    {
        public override string NombreTipo { get; set; } = "NOTA DE VENTA";
        public bool MostrarInformacionAdicional80 { get; set; }
        public string InformacionAdicional80 { get; set; }
        public bool MostrarInformacionAdicionalA4 { get; set; }
        public string InformacionAdicionalA4 { get; set; }
        public NotaDeVenta()
        {


        }
        public NotaDeVenta(OrdenDeVenta orden, EstablecimientoComercialExtendidoConLogo sede, EstablecimientoComercialExtendido establecimiento, byte[] qrBytes, bool mostrarEncabezadoTestigo, ModoImpresionCaracteristicasEnum modoImpresionCaracteristicas) : base(orden, sede, establecimiento, qrBytes, mostrarEncabezadoTestigo, modoImpresionCaracteristicas)
        {
            if (AplicacionSettings.Default.PermitirCambioNombreComprobanteNotaDeVenta)
            {
                NombreTipo = orden.Comprobante().NombreTipo;
            }
            ResolucionAutorizacionSunat = "";
            MostrarInformacionAdicional80 = !String.IsNullOrEmpty(VentasSettings.Default.InformacionAdicionalNotaVenta80);
            InformacionAdicional80 = VentasSettings.Default.InformacionAdicionalNotaVenta80;
            MostrarInformacionAdicionalA4 = !String.IsNullOrEmpty(VentasSettings.Default.InformacionAdicionalNotaVentaA4);
            InformacionAdicionalA4 = VentasSettings.Default.InformacionAdicionalNotaVentaA4;
            FormaPago = orden.ModoDePago() == ModoPago.Contado ? Enumerado.GetDescription(FormaPagoEnum.Contado) : Enumerado.GetDescription(FormaPagoEnum.Credito);
            MontoACredito = orden.ModoDePago() == ModoPago.Contado ? 0 : orden.Cuotas().Sum(c => c.total) - orden.PagoEnFechaEmision();
            Cuotas = orden.ModoDePago() == ModoPago.Contado ? new List<DetalleCuota>() : DetalleCuota.Convert(orden.Cuotas(), orden.ModoDePago(), orden.PagoEnFechaEmision());
        }
        //public static NotaDeVenta Convert(OrdenDeVenta orden, ActorComercial sede, byte[] qrBytes, bool mostrarEncabezadoTestigo, ModoImpresionCaracteristicasEnum modoImpresionCaracteristicas)
        //{
        //    return new NotaDeVenta
        //    {
        //        FechaEmision = orden.FechaEmision,
        //        Emisor = new Emisor
        //        {
        //            RazonSocial = sede.RazonSocial,
        //            NombreComercial = sede.NombreComercial,
        //            RUC = sede.DocumentoIdentidad,
        //            LogoBytes = sede.Logo(),
        //            Direccion = sede.DomicilioFiscal().detalle,
        //            OtrosDatosContacto = (String.IsNullOrEmpty(sede.Telefono()) ? "" : ("TF: " + sede.Telefono())) + Environment.NewLine + (String.IsNullOrEmpty(sede.Correo()) ? "" : ("Email: " + sede.Correo())),
        //            Publicidad = sede.InformacionPublicitaria()

        //        },
        //        Receptor = new Receptor
        //        {
        //            RazonSocial = (orden.Cliente().RazonSocial + orden.AliasCliente()),
        //            TipoDocumentoIdentidad = orden.TipoDocumentoIdentidadCliente,
        //            DocumentoIdentidad = orden.NumeroDocumentoIdentidadCliente,
        //            Direccion = orden.Cliente().DomicilioFiscal() != null ? orden.Cliente().DomicilioFiscal().detalle : ""
        //        },
        //        Detalles = Detalle.Convert(orden.Detalles(), modoImpresionCaracteristicas),
        //        MostrarMensajeAmazonia = orden.AplicaLeyDeAmazonia,
        //        MensajeNegocio = "",//todo: parametrizar
        //        Observacion = orden.Observacion(),
        //        MostrarLogo = FacturacionElectronicaSettings.Default.MostrarLogoEnComprobanteImpreso,
        //        CodigoQR = qrBytes,
        //        Serie = orden.Comprobante().NumeroDeSerie,
        //        Numero = orden.Comprobante().NumeroDeComprobante,
        //        ImporteTotal = orden.Total,
        //        ImporteOperacionExonerada = orden.ImporteTotalOperacionExonerada,
        //        ImporteOperacionInafecta = orden.ImporteTotalOperacionInafecta,
        //        ImporteOperacionGravada = orden.BaseImponibleOperacionGravada,
        //        ImporteTotalEnLetras = Util.APalabras(orden.Total, orden.MonedaPlural()),
        //        MostrarTestigo = mostrarEncabezadoTestigo,
        //        ResolucionAutorizacionSunat = ""
        //    };
        //}
    }
}

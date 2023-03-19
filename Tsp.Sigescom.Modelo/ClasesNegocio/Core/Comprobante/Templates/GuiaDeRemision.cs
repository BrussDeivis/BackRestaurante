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
    public class GuiaDeRemision : DocumentoElectronicoVenta
    {
        public override string NombreTipo { get; set; } = "GUIA DE REMISIÓN";
        public DateTime FechaInicioTraslado { get; set; }
        public Transportista Transportista { get; set; }
        public Conductor Conductor { get; set; }
        public string CodigoModalidadTransporte { get; set; }
        public string ModalidadTransporte { get; set; }
        public string CodigoMotivoTraslado { get; set; }
        public string MotivoTraslado { get; set; }
        public decimal PesoBrutoTotal { get; set; }
        public int NumeroBultos { get; set; }
        public string UbigeoDireccionOrigen { get; set; }
        public string DireccionOrigen { get; set; }
        public string UbigeoDireccionDestino { get; set; }
        public string DireccionDestino { get; set; }
        public string DocumentoReferencia { get; set; }
        public string EtiquetaTercero { get; set; } = "DESTINATARIO";

        public GuiaDeRemision()
        {

        }

        public GuiaDeRemision(MovimientoDeAlmacen movimiento, EstablecimientoComercialExtendido sede, EstablecimientoComercialExtendido establecimiento, byte[] qrBytes, bool mostrarEncabezadoTestigo, ModoImpresionCaracteristicasEnum modoImpresionCaracteristicas, List<Proveedor> proveedores, List<Detalle_maestro> modalidades, List<Detalle_maestro> motivos)
        {
            Emisor = new Emisor(sede, establecimiento);
            RegistroGuiaRemision(movimiento, establecimiento, qrBytes, mostrarEncabezadoTestigo, modoImpresionCaracteristicas, proveedores, modalidades, motivos);
        }

        public GuiaDeRemision(MovimientoDeAlmacen movimiento, EstablecimientoComercialExtendidoConLogo sede, EstablecimientoComercialExtendido establecimiento, byte[] qrBytes, bool mostrarEncabezadoTestigo, ModoImpresionCaracteristicasEnum modoImpresionCaracteristicas, List<Proveedor> proveedores, List<Detalle_maestro> modalidades, List<Detalle_maestro> motivos)
        {
            Emisor = new Emisor(sede, establecimiento);
            RegistroGuiaRemision(movimiento, establecimiento, qrBytes, mostrarEncabezadoTestigo, modoImpresionCaracteristicas, proveedores, modalidades, motivos);
        }

        public void RegistroGuiaRemision(MovimientoDeAlmacen movimiento, EstablecimientoComercialExtendido establecimiento, byte[] qrBytes, bool mostrarEncabezadoTestigo, ModoImpresionCaracteristicasEnum modoImpresionCaracteristicas, List<Proveedor> proveedores, List<Detalle_maestro> modalidades, List<Detalle_maestro> motivos)
        {
            IdOrden = movimiento.Id;
            FechaEmision = movimiento.FechaEmision;
            Receptor = new Receptor(new EstablecimientoComercialExtendido(movimiento.Transaccion().Actor_negocio1));
            Detalles = Detalle.Convert(movimiento.Detalles(), modoImpresionCaracteristicas, "", false);
            MostrarMensajeAmazonia = false;
            MensajeNegocio = AplicacionSettings.Default.MensajeDeNegocio;
            MostrarMensajeNegocio = AplicacionSettings.Default.MostrarMensajeDeNegocio;
            Observacion = movimiento.Observacion();
            MostrarLogo = FacturacionElectronicaSettings.Default.MostrarLogoEnComprobanteImpreso;
            CodigoQR = qrBytes;
            CodigoSunatMoneda = movimiento.Moneda().Codigo;
            CodigoSunatTipo = movimiento.Comprobante().CodigoTipo;
            Serie = movimiento.Comprobante().NumeroDeSerie;
            Numero = movimiento.Comprobante().NumeroDeComprobante;
            MostrarTestigo = mostrarEncabezadoTestigo;
            ResolucionAutorizacionSunat = FacturacionElectronicaSettings.Default.ResolucionEmisionElectronica;
            IdEstadoActual = MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado;
            FechaInicioTraslado = (DateTime)movimiento.FechaInicioDeTraslado();
            var transportista = proveedores.SingleOrDefault(p => p.Id == movimiento.IdTransporte());
            var conductor = proveedores.SingleOrDefault(p => p.Id == movimiento.IdConductorTransporte());
            Transportista = transportista == null ? null : new Transportista
            {
                RazonSocial = transportista.RazonSocial,
                TipoDocumentoIdentidad = transportista.CodigoTipoDocumentoIdentidad(),
                DocumentoIdentidad = transportista.DocumentoIdentidad,
            };
            Conductor = conductor == null ? null : new Conductor
            {
                Nombres = conductor.Nombres,
                Apellidos = conductor.ApellidoPaterno + " " + conductor.ApellidoMaterno,
                TipoDocumentoIdentidad = conductor.CodigoTipoDocumentoIdentidad(),
                DocumentoIdentidad = conductor.DocumentoIdentidad,
                NumeroLicencia = movimiento.LicenciaConductorTransporte(),
                Placa = movimiento.PlacaTransporte(),
            };
            CodigoModalidadTransporte = modalidades.Single(m => m.id == movimiento.IdModalidadDeTransporte()).codigo;
            ModalidadTransporte = modalidades.Single(m => m.id == movimiento.IdModalidadDeTransporte()).nombre;
            CodigoMotivoTraslado = motivos.Single(m => m.id == movimiento.IdMotivoDeTransporte()).codigo;
            MotivoTraslado = motivos.Single(m => m.id == movimiento.IdMotivoDeTransporte()).nombre;
            PesoBrutoTotal = movimiento.PesoBrutoTotalTransporte();
            NumeroBultos = movimiento.NumeroBultosTransporte();
            UbigeoDireccionOrigen = movimiento.IdUbigeoOrigenDeTraslado().PadLeft(6, '0');
            DireccionOrigen = movimiento.DireccionOrigenDeTraslado() + " - " + movimiento.UbigeoOrigen;
            UbigeoDireccionDestino = movimiento.IdUbigeoDestinoDeTraslado().PadLeft(6, '0');
            DireccionDestino = movimiento.DireccionDestinoDeTraslado() + " - " + movimiento.UbigeoDestino;
            DocumentoReferencia = movimiento.DocumentoDeReferencia();
            EtiquetaTercero = (motivos.Single(m => m.id == movimiento.IdMotivoDeTransporte()).id == MaestroSettings.Default.IdDetalleMaestroMotivoDeTrasladoPorCompra) ? "REMITENTE" : "DESTINATARIO";
            EsInvalidada = movimiento.EstaInvalidada();
            MotivoInvalidacion = movimiento.MotivoInvalidacion();
        }

        public static List<GuiaDeRemision> Convert(List<MovimientoDeAlmacen> ordenes, EstablecimientoComercialExtendido sede, ModoImpresionCaracteristicasEnum modoImpresionCaracteristicas, List<Proveedor> proveedores, List<Detalle_maestro> modalidades, List<Detalle_maestro> motivos)
        {
            List<GuiaDeRemision> resultado = new List<GuiaDeRemision>();
            foreach (var item in ordenes)
            {
                resultado.Add(new GuiaDeRemision(item, sede, new EstablecimientoComercialExtendidoConLogo(item.Transaccion().Actor_negocio2.Actor_negocio2), null, false, modoImpresionCaracteristicas, proveedores, modalidades, motivos));
            }
            return resultado;
        }
    }

    public class Transportista
    {
        public string RazonSocial { get; set; }
        public string TipoDocumentoIdentidad { get; set; }
        public string DocumentoIdentidad { get; set; }
    }

    public class Conductor
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string TipoDocumentoIdentidad { get; set; }
        public string DocumentoIdentidad { get; set; }
        public string NumeroLicencia { get; set; }
        public string Placa { get; set; }

    }
}

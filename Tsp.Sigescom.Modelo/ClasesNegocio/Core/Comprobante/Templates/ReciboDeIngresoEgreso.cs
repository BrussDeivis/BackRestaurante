using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Humanizer;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;

namespace Tsp.Sigescom.Modelo.Entidades.ComprobantesModel
{
    public class ReciboDeIngresoEgreso : DocumentoElectronicoImpreso
    {
        public override string NombreTipo { get; set; }
        public string Caja { get; set; }
        public string Cajero { get; set; }
        public string TipoPagadorRecibidor { get; set; }
        public string MedioDePago { get; set; }
        public string EntidadBancaria { get; set; }
        public string InformacionDePago { get; set; }
        public List<DetalleDeIngresoEgreso> DetallesDeIngresoEgreso { get; set; }

        public static ReciboDeIngresoEgreso Convert(MovimientoEconomico movimiento, EstablecimientoComercialExtendidoConLogo sede, EstablecimientoComercialExtendido establecimiento, bool mostrarEncabezadoTestigo)
        {
            return new ReciboDeIngresoEgreso
            {
                FechaEmision = movimiento.FechaEmision,
                Emisor = new Emisor(sede, establecimiento),
                Receptor = new Receptor(new EstablecimientoComercialExtendido(movimiento.Transaccion().Actor_negocio1)),                
                NombreTipo = movimiento.Comprobante().NombreTipo,
                Serie = movimiento.Comprobante().NumeroDeSerie,
                Numero = movimiento.Comprobante().NumeroDeComprobante,
                ImporteTotal = movimiento.Total,
                ImporteTotalEnLetras = Util.APalabras(movimiento.Total, movimiento.MonedaPlural()),
                Observacion = movimiento.Comentario,
                MostrarLogo = FacturacionElectronicaSettings.Default.MostrarLogoEnComprobanteImpreso,
                MostrarTestigo = AplicacionSettings.Default.MostrarCabeceraVoucher,
                MensajeNegocio = AplicacionSettings.Default.MensajeDeNegocio,
                DetallesDeIngresoEgreso = DetalleDeIngresoEgreso.Convert(movimiento.Detalles()),

                Caja = movimiento.Caja().RazonSocial,
                Cajero = movimiento.Cajero().NombreCompleto,
                TipoPagadorRecibidor = movimiento.EsIngreso() ? "RECIBIDO DE" : "PAGADO A",
                MedioDePago = movimiento.TrazaDePago().MedioDePago().nombre,
                EntidadBancaria = movimiento.TrazaDePago().EntidadBancaria().nombre,
                InformacionDePago = movimiento.TrazaDePago().Informacion,
            };
        }
    }

    public class DetalleDeIngresoEgreso
    {
        public string Comprobante { get; set; }
        public string CodigoCuota { get; set; }
        public decimal Importe { get; set; }
        public DetalleDeIngresoEgreso(DetalleMovimientoEconomico detalle)
        {
            this.Comprobante = detalle.Cuota().OperacionBase().Comprobante().NumeroDeSerie + " - " + detalle.Cuota().OperacionBase().Comprobante().NumeroDeComprobante;
            this.CodigoCuota = detalle.Cuota().Codigo;
            this.Importe = detalle.Importe;
        }
        internal static List<DetalleDeIngresoEgreso> Convert(List<DetalleMovimientoEconomico> detalles)
        {
            List<DetalleDeIngresoEgreso> resultado = new List<DetalleDeIngresoEgreso>();
            foreach (var item in detalles)
            {
                resultado.Add(new DetalleDeIngresoEgreso(item));
            }
            return resultado;
        }
    }
}


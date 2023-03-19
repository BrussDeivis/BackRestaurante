using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class BandejaMovimientoMercaderiaViewModel
    {
        [DataMember]
        public long Id { get; set; }
        public string FechaEmision { get; set; }
        public string NumeroComprobante { get; set; }
        public string DocumentoTercero { get; set; }
        public string NombreTercero { get; set; }
        public string Motivo { get; set; }
        public string Estado { get; set; }
        public bool Enviado { get; set; }
        public string EstaEnviado { get => Enviado ? "SI" : "NO"; }
        public bool Aceptado { get; set; }
        public string EstaAceptado { get => Aceptado ? "SI" : "NO"; }

        public BandejaMovimientoMercaderiaViewModel(MovimientoDeAlmacen movimiento, List<Detalle_maestro> motivos)
        {
            this.Id = movimiento.Id;
            this.FechaEmision = movimiento.FechaEmision.ToString("dd/MM/yyyy");
            this.NumeroComprobante = movimiento.Comprobante().NumeroDeSerie + " - " + movimiento.Comprobante().NumeroDeComprobante;
            this.DocumentoTercero = movimiento.Tercero().DocumentoIdentidad;
            this.NombreTercero = movimiento.Tercero().RazonSocial;
            this.Motivo = motivos.Single(m => m.id == (int)movimiento.IdMotivoDeTransporte()).nombre + " " + movimiento.DescripcionDeMotivo();
            this.Estado = movimiento.IdEstadoActual == 0 ? "NA" : movimiento.EstadoActual().nombre;
            this.Enviado = movimiento.Transaccion().Evento_transaccion.Select(ev => ev.id_evento).Contains(MaestroSettings.Default.IdDetalleMaestroEstadoTransmitido);
            this.Aceptado = !string.IsNullOrEmpty(movimiento.Informacion);
        }

        public static List<BandejaMovimientoMercaderiaViewModel> Convert(List<MovimientoDeAlmacen> ordenesDeDesplazamiento, List<Detalle_maestro> motivos)
        {
            List<BandejaMovimientoMercaderiaViewModel> ordenes = new List<BandejaMovimientoMercaderiaViewModel>();
            foreach (var ordenDeDesplazamiento in ordenesDeDesplazamiento)
            {
                ordenes.Add(new BandejaMovimientoMercaderiaViewModel(ordenDeDesplazamiento, motivos));
            }
            return ordenes;
        }
    }
}
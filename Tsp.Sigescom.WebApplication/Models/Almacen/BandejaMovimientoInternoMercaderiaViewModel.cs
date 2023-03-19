using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class BandejaMovimientoInternoMercaderiaViewModel
    {
        [DataMember]
        public long Id { get; set; }
        public long IdOperacion { get; set; }
        public string Fecha { get; set; }
        public string TipoOperacion { get; set; }
        public string AlmacenOrigenDestino { get; set; }
        //public string Transportista { get; set; }
        public string TipoDocumento { get; set; }
        public string Numero { get; set; }
        public string Estado { get; set; }


        public BandejaMovimientoInternoMercaderiaViewModel(OrdenDeTrasladoInterno orden)
        {
            this.Id = orden.Id;
            this.IdOperacion = orden.IdDesplazamiento();
            this.Fecha = orden.FechaOrden().ToString("dd/MM/yyyy");
            //this.Transportista = orden.Proveedor().RazonSocial;
            this.TipoDocumento = orden.Comprobante().NombreTipo;
            this.Numero = orden.Comprobante().NumeroDeSerie + " - " + orden.Comprobante().NumeroDeComprobante;
            this.AlmacenOrigenDestino = orden.AlmacenDestino().Nombre;
            this.Estado = orden.EstadoActual().nombre;

        }

        public static List<BandejaMovimientoInternoMercaderiaViewModel> Convert(List<OrdenDeTrasladoInterno> ordenesDeDesplazamiento)
        {
            List<BandejaMovimientoInternoMercaderiaViewModel> ordenes = new List<BandejaMovimientoInternoMercaderiaViewModel>();
            foreach (var ordenDeDesplazamiento in ordenesDeDesplazamiento)
            {
                ordenes.Add(new BandejaMovimientoInternoMercaderiaViewModel(ordenDeDesplazamiento));
            }
            return ordenes;
        }
    }
}
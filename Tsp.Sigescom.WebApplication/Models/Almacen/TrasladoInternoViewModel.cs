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
    public class MovimientoInternoDeMercaderiaViewModel
    {
        [DataMember]
        public long Id { get; set; }
        public long IdDesplazamiento { get; set; }
        public string Fecha { get; set; }
        public string NombreDocumento { get; set; }
        public string SerieNumeroDocumento { get; set; }
        public int IdAlmacenDestinoOrigen { get; set; }
        public string AlmacenDestinoOrigen { get; set; }
        public string Empleado { get; set; }
        public string Estado { get; set; }
        public List<DetalleOrdenDeOperacionViewModel> Detalles { get; set; }


        public MovimientoInternoDeMercaderiaViewModel(OrdenDeTrasladoInterno orden)
        {
            this.Id = orden.Id;
            this.IdDesplazamiento = orden.IdDesplazamiento();
            this.Fecha = orden.FechaOrden().ToString("dd/MM/yyyy");
            this.NombreDocumento = orden.Comprobante().Tipo().Nombre;
            this.SerieNumeroDocumento = orden.Comprobante().SerieYNumero();
            this.IdAlmacenDestinoOrigen = orden.AlmacenDestino().Id;
            this.AlmacenDestinoOrigen = orden.AlmacenDestino().Nombre;
            this.Empleado = orden.Empleado().Nombres;
            this.Estado = orden.EstadoActual().nombre;
            this.Detalles = DetalleOrdenDeOperacionViewModel.Convert(orden.Detalles());
        }

        public static List<MovimientoInternoDeMercaderiaViewModel> Convert(List<OrdenDeTrasladoInterno> ordenDeDesplazminetos)
        {
            List<MovimientoInternoDeMercaderiaViewModel> ordenes = new List<MovimientoInternoDeMercaderiaViewModel>();
            foreach (var ordenDeDesplazamiento in ordenDeDesplazminetos)
            {
                ordenes.Add(new MovimientoInternoDeMercaderiaViewModel(ordenDeDesplazamiento));
            }
            return ordenes;
        }
    }
}
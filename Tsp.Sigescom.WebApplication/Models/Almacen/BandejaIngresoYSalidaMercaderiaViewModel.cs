using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models
{
    //[Serializable]
    //[DataContract]
    //public class BandejaIngresoYSalidaMercaderiaViewModel
    //{
    //    [DataMember]
    //    public long Id { get; set; }
    //    public long IdOperacion { get; set; }
    //    public string Fecha { get; set; }
    //    public int IdTipoTransaccion { get; set; }
    //    public string TipoTransaccion { get; set; }
    //    public int IdActorComercial { get; set; }
    //    public string DocumentoActorComercial { get; set; }
    //    public string ActorComercial { get; set; }
    //    public string TipoDocumento { get; set; }
    //    public string CodigoDocumento { get; set; }
    //    public string NumeroComprobante { get; set; }


    //    public BandejaIngresoYSalidaMercaderiaViewModel(OrdenDeTrasladoInterno orden)
    //    {
    //        this.Id = orden.Id;
    //        this.IdOperacion = orden.IdDesplazamiento();
    //        this.Fecha = orden.FechaOrden().ToString("dd/MM/yyyy");
    //        this.IdTipoTransaccion = orden.TipoTransaccion().id;
    //        this.TipoTransaccion = orden.TipoTransaccionSuperior().nombre;
    //        this.IdActorComercial = orden.AlmacenDestino().Id;
    //        this.DocumentoActorComercial = orden.AlmacenDestino().DocumentoIdentidad;
    //        this.ActorComercial = orden.AlmacenDestino().Nombre;
    //        this.TipoDocumento = orden.Comprobante().NombreTipo;
    //        this.CodigoDocumento = orden.Comprobante().Tipo().Codigo;
    //        this.NumeroComprobante = orden.Comprobante().NumeroDeSerie + " - " + orden.Comprobante().NumeroDeComprobante.ToString();

    //    }

    //    public static List<BandejaIngresoYSalidaMercaderiaViewModel> Convert(List<OrdenDeTrasladoInterno> ordenesDeDesplazamiento)
    //    {
    //        List<BandejaIngresoYSalidaMercaderiaViewModel> ordenes = new List<BandejaIngresoYSalidaMercaderiaViewModel>();
    //        foreach (var ordenDeDesplazamiento in ordenesDeDesplazamiento)
    //        {
    //            ordenes.Add(new BandejaIngresoYSalidaMercaderiaViewModel(ordenDeDesplazamiento));
    //        }
    //        return ordenes;
    //    }
    //}
}
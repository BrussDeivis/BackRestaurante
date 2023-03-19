using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class BandejaGastoViewModel
    {

        [DataMember]
        public long Id { get; set; }
        public long IdGasto { get; set; }
        public string Fecha { get; set; }
        public string TipoDocumento { get; set; }
        public string TipoGasto { get; set; }
        public string Numero { get; set; }
        public string Proveedor { get; set; }
        public string Ruc { get; set; }
        public string Total { get; set; }
        public string Estado { get; set; }
        public bool EstaConfirmado { get; set; }
        public BandejaGastoViewModel(OrdenDeGasto orden)
        {
            this.Id = orden.Id;
            this.IdGasto = orden.IdGasto;
            this.Fecha = orden.FechaOrden().ToString("dd/MM/yyyy");
            this.TipoDocumento = orden.Comprobante().NombreTipo;
            this.TipoGasto = orden.Transaccion().Tipo_transaccion.nombre;
            this.Numero = orden.Comprobante().NumeroDeSerie + " - " + orden.Comprobante().NumeroDeComprobante;
            this.Proveedor = orden.proveedor().RazonSocial;
            this.Ruc = orden.proveedor().DocumentoIdentidad;
            this.Total = orden.Total.ToString("N2");
            this.Estado = orden.EstadoActual().nombre;
            this.EstaConfirmado = orden.EstadoActual().id ==  MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado;
        }

        public static List<BandejaGastoViewModel> Convert(List<OrdenDeGasto> ordenesDeGasto)
        {
            List<BandejaGastoViewModel> ordenes = new List<BandejaGastoViewModel>();
            foreach (var ordenDeCompra in ordenesDeGasto)
            {
                ordenes.Add(new BandejaGastoViewModel(ordenDeCompra));
            }
            return ordenes;
        }
    }
}
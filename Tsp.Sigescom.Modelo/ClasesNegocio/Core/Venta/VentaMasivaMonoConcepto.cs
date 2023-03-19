using System;
using System.Collections.Generic;
using Tsp.Sigescom.Config;
using System.Linq;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class VentaMasivaMonoConcepto : VentaMasiva
    { 
        
        private int idConcepto;
        private string nombreConcepto;

        public VentaMasivaMonoConcepto()
        {
        }

            public VentaMasivaMonoConcepto(int idPuntoDeVenta, int idAlmacen, int idConcepto, int idVendedor, DateTime fechaEmision, List<VentaMonoDetalle> detalles)
        {
            this.IdPuntoDeVenta = idPuntoDeVenta;
            this.IdAlmacen = idAlmacen;
            this.IdConcepto = idConcepto;
            this.IdVendedor = idVendedor;
            this.FechaEmision = fechaEmision;
            this.Detalles = detalles;
        }

        public VentaMasivaMonoConcepto(int idPuntoDeVenta, string nombrePuntoDeVenta, int idAlmacen, string nombreAlmacen, int idConcepto, string nombreConcepto, int idVendedor, string nombreVendedor, DateTime fechaEmision, DateTime fechaRegistro,List<VentaMonoDetalle> detalles)
        {
            this.IdPuntoDeVenta = idPuntoDeVenta;
            this.NombrePuntoDeVenta = nombrePuntoDeVenta;
            this.IdAlmacen = idAlmacen;
            this.NombreAlmacen = nombreAlmacen;
            this.IdConcepto = idConcepto;
            this.NombreConcepto = nombreConcepto;
            this.IdVendedor = idVendedor;
            this.NombreVendedor = nombreVendedor;
            this.FechaEmision = fechaEmision;
            this.FechaRegistro = fechaRegistro;
            this.Detalles = detalles;
        }
        
        public int IdConcepto { get => idConcepto; set => idConcepto = value; }
        public string NombreConcepto { get => nombreConcepto; set => nombreConcepto = value; }
      
    }

}

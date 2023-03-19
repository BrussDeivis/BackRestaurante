using System;
using System.Collections.Generic;
using Tsp.Sigescom.Config;
using System.Linq;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class VentaMasiva
    {

        private int idSerieDeComprobante;
        private int idTipoDeComprobante;
        private int idPuntoDeVenta;
        private string nombrePuntoDeVenta;
        private int idCaja;
        private string nombreCaja;
        private int idAlmacen;
        private string nombreAlmacen;
        private int idVendedor;
        private string nombreVendedor;
        private int idCajero;
        private string nombreCajero;
        private int idAlmacenero;
        private string nombreAlmacenero;
        private DateTime fechaEmision;
        private DateTime fechaRegistro;
        private List<VentaMonoDetalle> detalles;

        public VentaMasiva()
        {

        }

        public VentaMasiva(int idSerieDeComprobante, int idPuntoDeVenta, int idCaja, int idAlmacen, int idVendedor, int idCajero, int idAlmacenero, DateTime fechaEmision, List<VentaMonoDetalle> detalles)
        {
            this.IdSerieDeComprobante = idSerieDeComprobante;
            this.IdPuntoDeVenta = idPuntoDeVenta;
            this.IdCaja = idCaja;
            this.IdAlmacen = idAlmacen;
            this.IdCajero = idCajero;
            this.IdAlmacenero = idAlmacenero;
            this.IdVendedor = idVendedor;
            this.FechaEmision = fechaEmision;
            this.Detalles = detalles;
        }

        public VentaMasiva(int idPuntoDeVenta, int idAlmacen, int idAlmacenero, int idVendedor, int idCaja, int idCajero, DateTime fechaEmision, List<VentaMonoDetalle> detalles)
        {

            this.IdPuntoDeVenta = idPuntoDeVenta;
            this.IdAlmacen = idAlmacen;
            this.IdAlmacenero = idAlmacenero;
            this.IdVendedor = idVendedor;
            this.IdCaja = idCaja;
            this.FechaEmision = fechaEmision;
            this.IdCajero = idCajero;
            this.Detalles = detalles;
        }


        public VentaMasiva(int idPuntoDeVenta, string nombrePuntoDeVenta, int idVendedor, string nombreVendedor, DateTime fechaEmision, List<VentaMonoDetalle> detalles)
        {

            this.IdPuntoDeVenta = idPuntoDeVenta;
            this.nombrePuntoDeVenta = nombrePuntoDeVenta;
            this.IdVendedor = idVendedor;
            this.FechaEmision = fechaEmision;
            this.NombreVendedor = nombreVendedor;
            this.Detalles = detalles;
        }

        public VentaMasiva(int idPuntoDeVenta, string nombrePuntoDeVenta, int idAlmacen, string nombreAlmacen, int idVendedor, string nombreVendedor, int idAlmacenero, string nombreAlmacenero,
            DateTime fechaEmision, DateTime fechaRegistro, List<VentaMonoDetalle> detalles)
        {
            this.IdPuntoDeVenta = idPuntoDeVenta;
            this.NombrePuntoDeVenta = nombrePuntoDeVenta;
            this.IdAlmacen = idAlmacen;
            this.NombreAlmacen = nombreAlmacen;
            this.IdVendedor = idVendedor;
            this.NombreVendedor = nombreVendedor;
            this.IdAlmacenero = idAlmacenero;
            this.NombreAlmacenero = nombreAlmacenero;
            this.FechaEmision = fechaEmision;
            this.FechaRegistro = fechaRegistro;
            this.Detalles = detalles;
        }

        public long IdVentaCobroEnBloque { get; set; }
        public int IdSerieDeComprobante { get => idSerieDeComprobante; set => idSerieDeComprobante = value; }
        public int IdTipoDeComprobante { get => idTipoDeComprobante; set => idTipoDeComprobante = value; }
        public int IdPuntoDeVenta { get => idPuntoDeVenta; set => idPuntoDeVenta = value; }
        public string NombrePuntoDeVenta { get => nombrePuntoDeVenta; set => nombrePuntoDeVenta = value; }
        public int IdAlmacen { get => idAlmacen; set => idAlmacen = value; }
        public string NombreAlmacen { get => nombreAlmacen; set => nombreAlmacen = value; }
        public int IdAlmacenero { get => idAlmacenero; set => idAlmacenero = value; }
        public string NombreAlmacenero { get => nombreAlmacenero; set => nombreAlmacenero = value; }
        public int IdVendedor { get => idVendedor; set => idVendedor = value; }
        public string NombreVendedor { get => nombreVendedor; set => nombreVendedor = value; }
        public int IdCaja { get => idCaja; set => idCaja = value; }
        public string NombreCaja { get => nombreCaja; set => nombreCaja = value; }
        public int IdCajero { get => idCajero; set => idCajero = value; }
        public string NombreCajero { get => nombreCajero; set => nombreCajero = value; }
        public DateTime FechaEmision { get => fechaEmision; set => fechaEmision = value; }
        public DateTime FechaRegistro { get => fechaRegistro; set => fechaRegistro = value; }
        public List<VentaMonoDetalle> Detalles { get => detalles; set => detalles = value; }
    }

}

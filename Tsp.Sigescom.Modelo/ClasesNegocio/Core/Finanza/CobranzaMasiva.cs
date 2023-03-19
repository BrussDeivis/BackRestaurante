using System;
using System.Collections.Generic;
using Tsp.Sigescom.Config;
using System.Linq;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class CobranzaMasiva
    {
        // Representa el centro de atencion donde se registra el dinero
        private int idCaja;
        private string nombreCaja;
        // Empleado que recibe el dinero en la caja idCaja
        private int idCajero;
        private string nombreCajero;
        private DateTime fechaEmision;
        private DateTime fechaRegistro;
        private List<DetalleCobranzaMasiva> detalles;

        
        public CobranzaMasiva()
        {

        }

        public CobranzaMasiva(int idCaja, int idCajero, DateTime fechaEmision, List<DetalleCobranzaMasiva> detalles)
        {
            this.IdCaja = idCaja;
            this.IdCajero = idCajero;
            this.FechaEmision = fechaEmision;
            this.Detalles = detalles;
        }

        public CobranzaMasiva(int idCaja,string nombreCaja,int idCajero, string nombreCajero, DateTime fechaEmision, DateTime fechaRegistro, List<DetalleCobranzaMasiva> detalles)
        {
            this.IdCaja = idCaja;
            this.NombreCaja = nombreCaja;
            this.IdCajero = idCajero;
            this.NombreCajero = nombreCajero;
            this.FechaEmision = fechaEmision;
            this.FechaRegistro = fechaRegistro;
            this.Detalles = detalles;
        }

        public long IdVentaCobroEnBloque { get; set; }
        public int IdCaja { get => idCaja; set => idCaja = value; }
        public string NombreCaja { get => nombreCaja; set => nombreCaja = value; }
        public int IdCajero { get => idCajero; set => idCajero = value; }
        public string NombreCajero { get => nombreCajero; set => nombreCajero = value; }
        public DateTime FechaEmision { get => fechaEmision; set => fechaEmision = value; }
        public DateTime FechaRegistro { get => fechaRegistro; set => fechaRegistro = value; }
        public List<DetalleCobranzaMasiva> Detalles { get => detalles; set => detalles = value; }
    }

}

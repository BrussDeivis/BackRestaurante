using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.WebApplication.Models
{
    public class RegistroFinanciamientoViewModel
    {
        public decimal Inicial { get; set; }
        public decimal Capital { get; set; }
        public decimal Interes { get; set; }
        public decimal CapitalInteres { get; set; }
        public IEnumerable<RegistroDetalleFinanciamientoViewModel> Detalles { get; set; }
    }

    public class RegistroDetalleFinanciamientoViewModel
    {
        public int IdCuota { get; set; }
        public int Cuota { get; set; }
        public decimal CapitalCuota { get; set; }
        public decimal InteresCuota { get; set; }
        public decimal ImporteCuota { get; set; }
        public bool EsCuotaInicial { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string ObservacionCuota { get; set; }

        public RegistroDetalleFinanciamientoViewModel()
        {
        }

        public RegistroDetalleFinanciamientoViewModel(int numCuota,Cuota cuota)
        {
            IdCuota = cuota.id;
            Cuota = numCuota;
            CapitalCuota = cuota.capital;
            InteresCuota = cuota.interes;
            ImporteCuota = cuota.capital* cuota.interes;
            FechaVencimiento = cuota.fecha_vencimiento;
            ObservacionCuota = cuota.comentario;
        }
       


        public static List<RegistroDetalleFinanciamientoViewModel> Convert_(List<Cuota> cuotas)
        {
            List<RegistroDetalleFinanciamientoViewModel> nuevasCuotas = new List<RegistroDetalleFinanciamientoViewModel>();
            int numeroCuota = 1;
            foreach (var item in cuotas)
            {
                nuevasCuotas.Add(new RegistroDetalleFinanciamientoViewModel(numeroCuota++,item));
            }
            return nuevasCuotas;
        }
    }
}
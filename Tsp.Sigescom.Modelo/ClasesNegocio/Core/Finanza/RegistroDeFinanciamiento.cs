using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class RegistroDeFinanciamiento
    {
        public decimal Inicial { get; set; }
        public decimal Capital { get; set; }
        public decimal Interes { get; set; }
        public decimal CapitalInteres { get; set; }
        public IEnumerable<RegistroDetalleDeFinanciamiento> Detalles { get; set; }
    }

    public class RegistroDetalleDeFinanciamiento
    {
        public int IdCuota { get; set; }
        public int Cuota { get; set; }
        public decimal CapitalCuota { get; set; }
        public decimal InteresCuota { get; set; }
        public decimal ImporteCuota { get; set; }
        public bool EsCuotaInicial { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string ObservacionCuota { get; set; }

        public RegistroDetalleDeFinanciamiento()
        {
        }

        public RegistroDetalleDeFinanciamiento(int numCuota, Cuota cuota)
        {
            IdCuota = cuota.id;
            Cuota = numCuota;
            CapitalCuota = cuota.capital;
            InteresCuota = cuota.interes;
            ImporteCuota = cuota.capital * cuota.interes;
            FechaVencimiento = cuota.fecha_vencimiento;
            ObservacionCuota = cuota.comentario;
        }



        public static List<RegistroDetalleDeFinanciamiento> Convert_(List<Cuota> cuotas)
        {
            List<RegistroDetalleDeFinanciamiento> nuevasCuotas = new List<RegistroDetalleDeFinanciamiento>();
            int numeroCuota = 1;
            foreach (var item in cuotas)
            {
                nuevasCuotas.Add(new RegistroDetalleDeFinanciamiento(numeroCuota++, item));
            }
            return nuevasCuotas;
        }
        public static List<Cuota> Convert_(List<RegistroDetalleDeFinanciamiento> cuotas)
        {
            List<Cuota> cuotasConstruidas = new List<Cuota>();
            foreach (var item in cuotas)
            {
                cuotasConstruidas.Add(new Cuota()
                {
                    codigo = "",
                    fecha_emision = item.FechaVencimiento,
                    fecha_vencimiento = item.FechaVencimiento,
                    capital = item.CapitalCuota,
                    interes = item.InteresCuota,
                    total = item.ImporteCuota,
                    por_cobrar = false,
                    cuota_inicial = item.EsCuotaInicial
                });
            }
            return cuotasConstruidas;
        }
    }


}
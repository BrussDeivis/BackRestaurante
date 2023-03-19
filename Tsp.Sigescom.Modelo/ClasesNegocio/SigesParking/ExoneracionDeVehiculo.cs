using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.Custom.SigesParking
{
    public class ExoneracionDeVehiculo
    {
        public int Id { get; set; }
        public int IdCochera { get; set; }
        public Vehiculo Vehiculo{ get; set; }
        public DateTime Desde { get; set; }
        public string DesdeString { get { return Desde.ToString("dd/MM/yyyy h:mm tt"); } }
        public DateTime Hasta { get; set; }
        public string HastaString { get { return Hasta.ToString("dd/MM/yyyy h:mm tt"); } }

        public bool Vigente { get; set; }


        public ExoneracionDeVehiculo()
        {
        }

        public List<ExoneracionDeVehiculo> Convert()
        { return null; }

    }

    public class ExoneracionDeVehiculo_Flat
    {
        public string PlacaVehiculo { get; set; }
        public string DescripcionVehiculo { get; set; }
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
        public bool Vigente { get; set; }

        public ExoneracionDeVehiculo_Flat()
        { }
        public ExoneracionDeVehiculo_Flat(ExoneracionDeVehiculo exoneracion )
        {
            PlacaVehiculo = exoneracion.Vehiculo.Placa;
            DescripcionVehiculo = exoneracion.Vehiculo.NombreCompleto;
            Desde = exoneracion.Desde;
            Hasta = exoneracion.Hasta;
            Vigente = exoneracion.Vigente;
        }

        public static  List<ExoneracionDeVehiculo_Flat> Convert (List<ExoneracionDeVehiculo> exoneraciones)
        {
            List<ExoneracionDeVehiculo_Flat> resultado = new List<ExoneracionDeVehiculo_Flat>();
            foreach (var exoneracion in exoneraciones)
            {
                resultado.Add(new ExoneracionDeVehiculo_Flat(exoneracion));
            }      
            return resultado; 
        }

    }
}

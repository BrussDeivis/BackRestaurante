using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel.PlainModel
{
    public class FormularioT1
    {
        public List<DiaArribo> Dias1_8Arribos { get; set; }
        public List<DiaArribo> Dias9_16Arribos { get; set; }
        public List<DiaArribo> Dias17_24Arribos { get; set; }
        public List<DiaArribo> Dias25_TotalArribos { get; set; }
        public List<ArriboPernoctacion> ArribosPernoctacionesExtranjeros { get; set; }
        public List<ArriboPernoctacion> ArribosPernoctacionesNacionales { get; set; }
        public List<MotivoViaje> MotivoViajes { get; set; }

    }
    public class DiaArribo
    {
        //public int Id { get; set; }
        public string Nombre { get; set; }
        public int Arribo { get; set; }
        public DiaArribo()
        { 

        }
        public DiaArribo(string nombre, int arribo)
        {
            Nombre = nombre;
            Arribo = arribo;
        }

        public List<DiaArribo> Convert()
        {
            return new List<DiaArribo>();
        }
    }
    public class ArriboPernoctacion
    {
        //public int Id { get; set; }
        public string Nombre { get; set; }
        public int Arribo { get; set; }
        public int Pernoctacion { get; set; }
        public ArriboPernoctacion()
        {

        }
        public ArriboPernoctacion(string nombre, int arribo, int pernoctacion)
        {
            Nombre = nombre;
            Arribo = arribo;
            Pernoctacion = pernoctacion;
        }
        public List<ArriboPernoctacion> Convert()
        {
            return new List<ArriboPernoctacion>();
        }

    }
    public class MotivoViaje
    {
        //public int Id { get; set; }
        public string Nombre { get; set; }
        public int Total { get; set; }
        public int Vacaciones { get; set; }
        public int Visita { get; set; }
        public int Educacion { get; set; }
        public int Salud { get; set; }
        public int Religion { get; set; }
        public int Compras { get; set; }
        public int Negocios { get; set; }
        public int Otros { get; set; }

        public List<MotivoViaje> Convert()
        {
            return new List<MotivoViaje>();
        }
    }

}

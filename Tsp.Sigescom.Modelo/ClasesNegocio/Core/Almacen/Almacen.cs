using System.Collections.Generic;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;

namespace Tsp.Sigescom.Modelo.Negocio.Core.Almacen
{
    public class Almacen
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }

        public Almacen()
        {
        }



        public Almacen(int id, string codigo, string nombre)
        {
            Id = id;
            Codigo = codigo;
            Nombre = nombre;
        }
    }
}

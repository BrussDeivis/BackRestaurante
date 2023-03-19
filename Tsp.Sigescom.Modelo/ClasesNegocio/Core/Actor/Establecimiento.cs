using System;
using System.Collections.Generic;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;

namespace Tsp.Sigescom.Modelo.Negocio.Core.Actor
{
    public class Establecimiento
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public List<ItemGenerico> CentrosAtencion { get; set; }

        public Establecimiento()
        {
        }

        public Establecimiento ( ItemGenericoConSubItems item)
        {
            Id = item.Id;
            Codigo = item.Codigo;
            Nombre = item.Nombre;
            CentrosAtencion = item.SubItems;
        }

        public Establecimiento(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }

        public Establecimiento(EstablecimientoComercial establecimientoComercial)
        {
            Id = establecimientoComercial.Id;
            Codigo = establecimientoComercial.Codigo;
            Nombre = establecimientoComercial.Nombre;
        }

        public static List<Establecimiento> Convert(List<ItemGenericoConSubItems> _establecimientos)
        {
            List<Establecimiento> establecimientos = new List<Establecimiento>();
            _establecimientos.ForEach(e => establecimientos.Add(new Establecimiento(e)));
            return establecimientos;
        }

        public static List<Establecimiento> Convert(List<EstablecimientoComercial> _establecimientos)
        {
            List<Establecimiento> establecimientos = new List<Establecimiento>();
            _establecimientos.ForEach(e => establecimientos.Add(new Establecimiento(e)));
            return establecimientos;
        }
    }
}

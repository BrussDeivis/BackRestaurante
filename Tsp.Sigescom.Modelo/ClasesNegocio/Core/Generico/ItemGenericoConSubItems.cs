using System.Collections.Generic;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico
{
    public class ItemGenericoConSubItems: ItemGenerico
    {
        public List<ItemGenerico> SubItems { get; set; }

        public ItemGenericoConSubItems()
        {
        }



        public ItemGenericoConSubItems(int id, string codigo, string nombre, string valor)
        {
            Id = id;
            Codigo = codigo;
            Nombre = nombre;
            Valor = valor;
        }
    }
}

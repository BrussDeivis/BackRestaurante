using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class ValorCaracteristica 
    {
        public int Id { get; set; }
        public int IdCaracteristica { get; set; }
        public string NombreCaracteristica { get; set; }
        public string Valor { get; set; }


        public ValorCaracteristica()
        {
        }

        public ValorCaracteristica(int id, int idCaracteristica,string nombreCaracteristica ,string valor)
        {
            Id = id;
            IdCaracteristica = idCaracteristica;
            NombreCaracteristica = nombreCaracteristica;
            Valor = valor;
        }
    }
}
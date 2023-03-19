using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class ParametroDeTransaccion
    {
        private int id;
        private int idParametro;
        private int idTransaccion;
        private int valor;


        public int Id { get => id; set => id = value; }
        public int IdParametro { get => idParametro; set => idParametro = value; }
        public int IdTransaccion { get => idTransaccion; set => idTransaccion = value; }
        public int Valor { get => valor; set => valor = value; }

        public ParametroDeTransaccion()
        {

        } 
    }
}

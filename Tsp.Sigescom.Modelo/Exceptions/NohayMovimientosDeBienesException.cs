using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;

namespace Tsp.Sigescom.Modelo.Entidades
{
    public class NohayMovimientosDeBienesException : LogicaException
    {

        public bool HayInventarioPrevio { get; set; }
        public long? IdUltimoInventario { get; set; }
        public DateTime? FechaUltimoInventario { get; set; }
        public NohayMovimientosDeBienesException(string mensaje, bool hayInventarioPrevio, long idUltimoInventario, DateTime? fechaUltimoInventario) : base(mensaje)
        {
            HayInventarioPrevio = hayInventarioPrevio;
            IdUltimoInventario = idUltimoInventario;
            FechaUltimoInventario = fechaUltimoInventario;
        }
    }
}

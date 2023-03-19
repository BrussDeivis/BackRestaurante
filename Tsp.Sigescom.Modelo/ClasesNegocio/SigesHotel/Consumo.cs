using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel
{
    public class Consumo:ConsumoSimple
    {
     
        public string TipoHabitacion { get; set; }
        public string CodigoHabitacion { get; set; }
        public bool Facturado { get; set; }
        public bool PuedeInvalidar { get => Facturado == false; }
        public int IdEstado { get; set; }
        public bool EstaInvalidado { get => IdEstado == MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado; }
    }
   
    
}


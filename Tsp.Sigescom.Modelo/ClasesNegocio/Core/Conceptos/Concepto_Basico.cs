using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;

namespace Tsp.Sigescom.Modelo.Entidades
{
    public partial class Concepto_Basico
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Valor { get; set; }
        public IEnumerable<Caracteristica> Caracteristicas { get; set; }

        public Concepto_Basico()
        {

        }

        public bool EsBien { get =>  Valor == "1";  }
        public bool TieneCaracteristicasPropias { get => Caracteristicas != null ? Caracteristicas.Any(c => c.IdMaestro == MaestroSettings.Default.IdMaestroCaracteristicaPropiaConcepto) : false; }
    }

}

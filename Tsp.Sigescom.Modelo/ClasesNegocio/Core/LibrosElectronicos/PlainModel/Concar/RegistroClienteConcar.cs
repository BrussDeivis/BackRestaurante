using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.Core.LibrosElectronicos.PlainModel
{
    public class RegistroClienteConcar
    {
        public string Avanexo { get; set; }
        public string Acodane { get; set; }
        public string Adesane { get; set; }
        public string Arefane { get; set; }
        public string Aruc { get; set; }
        public string Acodmon { get; set; }
        public string Aestado { get; set; }
        public string Adate { get; set; }
        public string Ahora { get; set; }

        public List<RegistroClienteConcar> Convert()
        {
            return new List<RegistroClienteConcar>();
        }
    }
}

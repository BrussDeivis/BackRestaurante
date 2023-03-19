using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.Core.LibrosElectronicos.PlainModel
{
    public class LibroElectronicoConcar
    {
        public List<DetalleAsientoContableConcar> RegistroCobranzas { get; set; }
        public List<DetalleAsientoContableConcar> RegistroVentas { get; set; }
        public List<DetalleAsientoContableConcar> RegistroNotas { get; set; }
        public List<RegistroClienteConcar> RegistroClientes { get; set; }
    }
}

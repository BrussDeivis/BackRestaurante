using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class CarteraDeClientes
    {
        public EstablecimientoComercialExtendido establecimientoComercial;
        public CentroDeAtencionExtendido centroDeAtencion;
        public List<Cliente> clientes = new List<Cliente>();

        public CarteraDeClientes()
        {

        }

        public CarteraDeClientes(List<Vinculo_Actor_Negocio> vinculos)
        {
            establecimientoComercial = new EstablecimientoComercialExtendido(vinculos.First().Actor_negocio.Actor_negocio2);
            centroDeAtencion = new CentroDeAtencionExtendido(vinculos.First().Actor_negocio);
            clientes = vinculos.Select(v => new Cliente(v.Actor_negocio1)).ToList();
        }

        public static List<CarteraDeClientes> Convert(List<Vinculo_Actor_Negocio> vinculos)
        {
            var carterasDeClientes = new List<CarteraDeClientes>();
            foreach (var item in vinculos.Select(v => v.id_actor_negocio_principal).Distinct())
            {
                carterasDeClientes.Add(new CarteraDeClientes(vinculos.Where(v => v.id_actor_negocio_principal == item).ToList()));
            }
            return carterasDeClientes;
        }
    }
}

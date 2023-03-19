using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.SigesRestaurant;

namespace Tsp.Sigescom.Modelo.Interfaces.Datos.Restaurant
{
    public interface IMesa_Repositorio
    {
        Actor_negocio ObtenerMesa(int id);
        int ObtenerIdEstablecimiento(int idMesa);

    }
}

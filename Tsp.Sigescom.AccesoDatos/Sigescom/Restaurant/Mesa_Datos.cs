using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Tsp.Sigescom.AccesoDatos.Generico;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Restaurant;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;
using Tsp.Sigescom.Modelo.SigesRestaurant;

namespace Tsp.Sigescom.AccesoDatos.Restaurant
{
    public partial class Mesa_Datos : IMesa_Repositorio
    {

        public Actor_negocio ObtenerMesa(int id)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                return _db.Actor_negocio.FirstOrDefault(an => an.id == id);
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener mesa ", e);
            }
        }
        public int ObtenerIdEstablecimiento(int idMesa)
        {
            try
            {
                SigescomEntities _db = new SigescomEntities();
                return _db.Actor_negocio.Where(an => an.id==idMesa).Select(an=> an.Actor_negocio2.Actor_negocio2.id).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new DatosException("Error al obtener Id de establecimiento ", e);
            }
        }

    }
}

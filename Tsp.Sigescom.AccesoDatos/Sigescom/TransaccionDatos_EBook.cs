using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using Tsp.Sigescom.AccesoDatos.Generico;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;

namespace Tsp.Sigescom.AccesoDatos
{
    public partial class TransaccionDatos : RepositorioBase, ITransaccionRepositorio
    {

        public List<Periodo> ObtenerPeriodos()
        {
            try
            {
                return _db.Periodo.ToList(); 
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Periodo ObtenerPeriodo(int idPeriodo)
        {
            try
            {
                return _db.Periodo.SingleOrDefault(p => p.id == idPeriodo);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Periodo ObtenerPeriodo(string nombrePeriodo)
        {
            try
            {
                return _db.Periodo.SingleOrDefault(p => p.nombre == nombrePeriodo);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;
using Tsp.Sigescom.Modelo.Entidades.EFactura;
using Tsp.Sigescom.Modelo;

namespace Tsp.FacturacionElectronica.AccesoDatos
{
    public class EFacturaRepositorioBase : IRepositorioBase
    {
        protected FacturacionEntities _db = null;

        public EFacturaRepositorioBase()
        {
            _db = new FacturacionEntities();
        }

        public OperationResult Save()
        {
            try
            {
                _db.SaveChanges();
                return new OperationResult(OperationResultEnum.Success);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new OperationResult(ex);
            }
            catch (EntityException e)
            {
                OperationResult result = new OperationResult(OperationResultEnum.Error, "Error al intentar guardar datos, intentelo de nuevo o comuníquese con el administrador.");
                result.exceptions.Add(e);
                return result;
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }

        }

        public void RefreshEntity(object _object)
        {
            var context = ((IObjectContextAdapter)_db).ObjectContext;
            context.Refresh(System.Data.Entity.Core.Objects.RefreshMode.StoreWins, _object);
        }
        public void RefreshEntityCollection(IEnumerable<object> collection)
        {
            var context = ((IObjectContextAdapter)_db).ObjectContext;
            context.Refresh(System.Data.Entity.Core.Objects.RefreshMode.StoreWins, collection);
        }
    }
}

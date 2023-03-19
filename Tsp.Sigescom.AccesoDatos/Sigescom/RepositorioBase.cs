using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;

using System.Data.Entity.Core;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;
using System.Data.Entity.Infrastructure;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo;

namespace Tsp.Sigescom.AccesoDatos.Generico
{
    public class RepositorioBase:IRepositorioBase
    {
        protected SigescomEntities _db = null;

        public RepositorioBase()
        {
            _db = new SigescomEntities();
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

                throw ex;
                //OperationResult result = new OperationResult(OperationResultEnum.Error, "Error de concurrrencia al intentar guardar");
                //result.exceptions.Add(ex);
                //return result;
            }
            catch (EntityException e)
            {
                OperationResult result = new OperationResult(OperationResultEnum.Error, "Error al intentar guardar Datos, inentelo denuevo o comuníquese con un administrador.");
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

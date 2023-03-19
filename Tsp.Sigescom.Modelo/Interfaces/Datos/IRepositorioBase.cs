using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo.Interfaces.Repositorio
{
    public interface IRepositorioBase
    {


        /// <summary>
        /// guarda el contexto en la BD
        /// </summary>
        /// <returns></returns>
        OperationResult Save();

        void RefreshEntity(object _object);

        void RefreshEntityCollection(IEnumerable<object> collection);
        
    }
}

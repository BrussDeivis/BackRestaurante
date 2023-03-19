using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.Interfaces.Logica
{
    public interface ICodigosOperacion_Logica
    {
        string ObtenerSiguienteCodigoParaFacturacion(string prefijo, int idTipoTransaccion);
        int ObtenerMaximoCodigoParaTransaccion(string prefijo, int idTipoTransaccion);

    }
}


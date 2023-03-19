using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.SigesRestaurant;

namespace Tsp.Sigescom.Modelo.Interfaces.Datos.Restaurant
{
    public interface IAtencion_Repositorio
    {
        OperationResult GuardarCambioDeMesa(AtencionRestaurante atencion, Mesa nuevaMesa);
        AtencionRestaurante ObtenerAtencionDeMesaOcupada(int idMesa);
        AtencionRestaurante ObtenerAtencionConDatosMinimosDeOrdenYDetallesSoloParaCerrarla(long idAtencion);
        OperationResult CerrarAtencionYOrdenesYAtenderDetalles(long idAtencionACerrar, long[] idsOrdenesACerrar, long[] idsDetallesAAtender, int idMesa, bool liberarMesa, int idEmpleado);

    }
}

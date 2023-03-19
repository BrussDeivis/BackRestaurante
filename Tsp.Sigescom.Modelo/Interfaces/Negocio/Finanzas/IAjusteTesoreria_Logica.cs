using System;
using System.Collections.Generic;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Finanza.PlainModel;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Sesion;
using Tsp.Sigescom.Modelo.Negocio.Finanza.Report;

namespace Tsp.Sigescom.Modelo.Interfaces.Logica.Tesoreria
{
    public interface IAjusteTesoreria_Logica
    {
        OperationResult CorregirTipoTransaccionPagoEnNotasDeCreditoYDebito();



    }
}

using System;
using System.Collections.Generic;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Facturacion;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;

using Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel;
using Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel.ModeloExtranet;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;

namespace Tsp.Sigescom.Modelo.Negocio.Almacen

{
    public interface IAjusteInventario_Logica
    {
        /// <summary>
        /// Por cada almacén, para todos los conceptos que aparecen en el inventario actual, 
        /// Se obtiene el stock actual del inventario actual
        /// Luego se calcula el stock en base a los movimientos, sumando y restando según su tipo de transacción.
        /// Se crea e inserta una transaccion del tipo ingreso de bienes por ajuste de inventario para los casos en que el stock del inventario sea mayor al de los movimientos.
        /// Se inserta una transaccion del tipo salida de bienes por ajuste de inventario para los casos en que el stock del inventario sea menor al de los movimientos.
        /// </summary>
        /// <returns></returns>
        OperationResult CuadrarStockFisicoEntreInventarioActualYMovimientos();

        /// <summary>
        /// Obtiene todos los movimientos de mercadería, y calcula su costo unitario y total según el tipo de transaccion.
        /// Si es una entrada, tiene en cuenta si aplica ley de amazonía para incluir el IGV en el costo, caso contrario solo considera la base imponible.
        /// Si es una salida, debe considerarse el costo unitario promedio a la fecha de la operación.
        /// </summary>
        /// <returns></returns>
        OperationResult RecalcularCostoUnitarioYTotalEnMovimientos();
        
    }
}

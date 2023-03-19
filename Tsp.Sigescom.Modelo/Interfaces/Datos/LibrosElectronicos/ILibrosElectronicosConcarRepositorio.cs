using System;
using System.Collections.Generic;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.LibrosElectronicos.PlainModel;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Facturacion;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;

using Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel;
using Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel.ModeloExtranet;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;

namespace Tsp.Sigescom.Modelo.Interfaces.Datos
{
    public interface ILibrosElectronicosConcarRepositorio
    {
        IEnumerable<OperacionVenta> ObtenerOperacionesVenta(int[] idsTiposComprobantes, int[] idsTiposTransaccion, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<OperacionVenta> ObtenerOperacionesIngresoDineroPorVenta(int[] idsTiposComprobantes, int[] idsTiposTransaccion, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<RegistroCliente> ObtenerRegistroClientes(int[] idsTiposComprobantes, int[] idsTiposTransaccion, DateTime fechaDesde, DateTime fechaHasta);

    }
}

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

namespace Tsp.Sigescom.Modelo.Interfaces.Datos.Almacen

{
    public interface IInventarioRepositorio
    {
        IEnumerable<VencimientoConceptoNegocio> ObtenerVencimientoConceptosIngresados(int idAlmacen, DateTime fechaDesde, DateTime fechaHasta);
        IEnumerable<VencimientoConceptoNegocio> ObtenerVencimientoConceptosIngresados(int idAlmacen, DateTime fechaDesde, DateTime fechaHasta, int[] familias);





        IEnumerable<InventarioFisico> ObtenerInventarioFisico(long idInventario);
        IEnumerable<InventarioFisico> ObtenerInventarioFisico(long idInventario, int[] familias);
        InventarioFisico ObtenerInventarioFisico(long idInventario, int idConcepto);
        IEnumerable<InventarioValorizado> ObtenerInventarioValorizado(long idInventario);
        IEnumerable<InventarioValorizado> ObtenerInventarioValorizado(long idInventario, int[] familias);
        InventarioValorizado ObtenerInventarioValorizado(long idInventario, int idConcepto);
        IEnumerable<InventarioSemaforo> ObtenerInventarioSemaforo(long idInventario);
        IEnumerable<InventarioSemaforo> ObtenerInventarioSemaforo(long idInventario, int[] familias); 


        


    }
}

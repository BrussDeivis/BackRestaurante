using System;
using System.Collections.Generic;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Sesion;
using Tsp.Sigescom.Modelo.Negocio.Almacen.Report;

namespace Tsp.Sigescom.Modelo.Negocio.Almacen
{
    public interface IAlmacenReporte_Logica
    {
        PrincipalReportData ObtenerDatosParaReportePrincipal(UserProfileSessionData profileData);
        List<EntradaAlmacen> Entradas(int idAlmacen, DateTime fechaDesde, DateTime fechaHasta, bool todasLasFamilias, int[] familias);

        List<SalidaAlmacen> Salidas(int idAlmacen, DateTime fechaDesde, DateTime fechaHasta, bool todasLasFamilias, int[] idsFamilias);
        List<InventarioFisico> ObtenerInventarioFisicoHistorico(int idAlmacen, int idEmpleado, DateTime fechaHasta, bool todasLasFamilias, int[] idsFamilias);
        List<InventarioSemaforo> InventarioSemaforoHistorico(int idAlmacen, int idEmpleado, DateTime fechaHasta, bool todasLasFamilias, int[] idsFamilia, bool estadoBajo, bool estadoNormal, bool estadoAlto);
        List<InventarioValorizado> InventarioValorizadoHistorico(int idAlmacen, int idEmpleado, DateTime fechaHasta, bool todasLasFamilias, int[] idsFamilias);

        List<InventarioVencimiento> Vencimientos(int idAlmacen, DateTime fechaDesde, DateTime fechaHasta, bool todasLasFamilias, int[] familias);
        StockMinMax ObtenerStockMinimoYMaximo(int idConcepto);
        List<DetalleKardexFisico> KardexFisico(int idAlmacen, int idEmpleado, DateTime fechaDesde, DateTime fechaHasta, int idConcepto);
        List<DetalleKardexValorizado> KardexValorizado(int idAlmacen, int idEmpleado, DateTime fechaDesde, DateTime fechaHasta, int idConcepto);


      
    }
}

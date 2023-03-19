using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;

namespace Tsp.Sigescom.Modelo.Interfaces.Logica
{
    public partial interface IOperacionLogica
    {
        List<OrdenDeGasto> ObtenerOrdenesDeGasto(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta);
        Task<List<TipoDeComprobanteParaTransaccion>> ObtenerTiposDeComprobanteParaGasto(int idEmpleado, int idCentroAtencion);
        OperationResult GuardarGasto(int idEmpleado, int idCentroAtencion, int idProveedor, int idTipoComprobante, int idSerieComprobante, bool esPropio, string numeroSerieDeComprobante, int numeroDeComprobante, int idConcepto, string nombreConcepto, string detalle, string observacion, DateTime fechaEmision, decimal igv, decimal total, bool esContado, bool esCredito, List<Cuota> cuotas);
        OperationResult InvalidarGasto(long idOrdenDeGasto, int idEmpleado, int idCentroAtencion, string observacion);
        OrdenDeGasto ObtenerGasto(long idOrdenGasto);

        List<Resumen_Transaccion_Gasto_Por_Concepto> ObtenerReporteGastoPorConcepto(DateTime fechaDesde, DateTime fechaHasta, bool reporteGlobal, int[] idsCentrosAtencion);


    }

    

}

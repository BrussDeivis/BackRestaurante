using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Sesion;

namespace Tsp.Sigescom.Modelo.Interfaces.Logica
{
    public partial interface IOperacionLogica
    {

        /// <summary>
        /// Devuelve los tipos de comprobante para notas de debito incluyendo solo sus series vigentes y las que tengan como propietario al <paramref name="idCentroAtencion"/>
        /// </summary>
        /// <param name="idEmpleado"></param>
        /// <param name="idCentroAtencion"></param>
        /// <returns></returns>
        List<TipoDeComprobanteParaTransaccion> ObtenerTiposDeComprobanteParaNotaDeDebito(int idEmpleado, int idCentroAtencion);
        void RegenerarNumeracionComprobantePropioAutonumerable(Comprobante comprobante, Serie_comprobante serie);
        void RegenerarNumeracionComprobantesPropiosAutonumerables(List<Comprobante> comprobantes, Serie_comprobante serie);

        /// <summary>
        /// Devuelve los tipos de comprobante para notas de debito incluyendo solo sus series vigentes y las que tengan como propietario al <paramref name="idCentroAtencion"/>
        /// </summary>
        /// <param name="idEmpleado"></param>
        /// <param name="idCentroAtencion"></param>
        /// <returns></returns>
        List<TipoDeComprobanteParaTransaccion> ObtenerTiposDeComprobanteParaNotaDeCredito(int idEmpleado, int idCentroAtencion);


        Comprobante GenerarComprobantePropioAutonumerable(int idSerieComprobante);
        Comprobante GenerarComprobantePropioAutonumerableMarcandoSerieComoModificada(Serie_comprobante serie);
        void AutoIncrementarSerieMarcandolaComoModificada(Serie_comprobante serie);
        Operacion ObtenerOperacionSesionContenedora(int idCentroAtencion);
        OperationResult RegistrarOrdenDeVenta(ModoOperacionEnum porMostrador, UserProfileSessionData sesionDeUsuario, DatosVentaIntegrada venta);
        OperationResult RegistrarOrdenesDeVenta(ModoOperacionEnum porMostrador, UserProfileSessionData sesionUsuario, List<DatosVentaIntegrada> ventas);
       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Almacen;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Compras;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Transacciones;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Interfaces.Logica.Tesoreria;

namespace Tsp.Sigescom.Logica.Core.Tesoreria
{
    public class AjusteTesoreria_Logica : IAjusteTesoreria_Logica
    {


        protected readonly IActualizarTransaccion_Repositorio _actualizarTransaccionDatos;
        protected readonly IConsultaTransaccion_Repositorio _consultaTransaccionDatos;


        public AjusteTesoreria_Logica(IConsultaTransaccion_Repositorio consultaTransaccionDatos, IActualizarTransaccion_Repositorio actualizarTransaccionDatos)
        {
            _consultaTransaccionDatos = consultaTransaccionDatos;
            _actualizarTransaccionDatos = actualizarTransaccionDatos;
        }


        /// <summary>
        /// se ha detectado que todas las transacciones de pagos o cobros de notas de credito y debito en compras o ventas, apuntan al tipo de transaccion Cobranza de facturas a clientes
        /// Esto distorsiona los reportes de tesoreria.
        /// </summary>
        /// <returns></returns>
        public OperationResult CorregirTipoTransaccionPagoEnNotasDeCreditoYDebito()
        {
            try
            {
                ///obtener transacciones del tipo cobranza de facturas a clientes donde su tipo de transaccion padre es diferente a Venta
                ///estas son todas las transacciones que representan pagos o cobros donde su tipo de transaccion es incorrecto.
                var transaccionesErradas= _consultaTransaccionDatos.ObtenerTransaccionesSegunTipoYConTipoTransaccionPadreDiferenteA(TransaccionSettings.Default.IdTipoTransaccionCobranzaFacturasClientes, TransaccionSettings.Default.IdTipoTransaccionVenta).ToList();
                var transaccionesPadre = _consultaTransaccionDatos.ObtenerTransaccionesPadre(transaccionesErradas.Select(t=> t.id).ToArray()).ToList();
                ///conseguir el tipo de la orden para poder corregir
                foreach (var transaccionErrada in transaccionesErradas)
                {
                    var idTipoTransaccionPadre = transaccionesPadre.FirstOrDefault(t=> t.id == transaccionErrada.id_transaccion_padre).id_tipo_transaccion;
                    var idTipoTransaccionOrden = Diccionario.MapeoWraperVsOrden.Single(t => t.Key == idTipoTransaccionPadre).Value;
                    var idTipoTransaccionTesoreria = Diccionario.MapeoOrdenVsMovimientoEconomico.Single(t => t.Key == idTipoTransaccionOrden).Value;

                    transaccionErrada.id_tipo_transaccion = idTipoTransaccionTesoreria;
                }
                return _actualizarTransaccionDatos.ActualizarTransacciones(transaccionesErradas.ToList());
                
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al corregir tipos de transacción" + e.Message, e);
            }
        }

        

    }
}

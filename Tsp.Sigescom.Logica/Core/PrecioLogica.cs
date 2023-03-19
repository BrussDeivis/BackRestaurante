using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;
//using Tsp.Sigescom.Modelo.Properties;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo;

namespace Tsp.Sigescom.Logica
{
    public class PrecioLogica : IPrecioLogica
    {
        #region building
        private readonly IPrecioRepositorio _precioRepositorio;

        public PrecioLogica(IPrecioRepositorio precioRepositorio)
        {
            _precioRepositorio = precioRepositorio;
        }
        #endregion
        #region crear
        public OperationResult actualizarPrecio(int idCentroAttencion,int idPrecio, int idProducto, int idTarifa, decimal importe, DateTime fechaDesde, DateTime fechaHasta, string descripcion, int idResponsable)
        {
            try
            {
                DateTime fechaActual = DateTimeUtil.FechaActual();
                int idUnidadDeNegocio = MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal;
                int idMoneda = MaestroSettings.Default.IdDetalleMaestroMonedaSoles;
                int idTipo = MaestroSettings.Default.IdDetalleMaestroTipoPrecioPrecio;
                int cantidadMinima = 1;
                int cantidadMaxima = 0;

                //creamos el precio
                Precio precio = new Precio(idPrecio, idCentroAttencion, idUnidadDeNegocio, idProducto, importe, idTarifa, idMoneda, fechaDesde,
                    fechaHasta, fechaActual, true, true, false, cantidadMinima, cantidadMaxima, idTipo,descripcion, idResponsable);

                return _precioRepositorio.ActualizarPrecio(precio);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public OperationResult establecerPrecios(int idCentroAtencion, int idSede,int idEmpleado, List<Precio> tarifasConPrecio, int idMercaderia)
        {
            try {

                DateTime fechaActual = DateTimeUtil.FechaActual();
                List<Precio> precios = new List<Precio>();
                foreach (var item in tarifasConPrecio)
                {
                    //int idTipo = MaestroSettings.Default.IdDetalleMaestroTipoPrecioPrecio;
                    //int cantidadMinima = 1;
                    //int cantidadMaxima = 0;
                    //var idActorNegocio = TransaccionSettings.Default.PreciosCentralizados ? idSede : idCentroAtencion;
                    Precio precio = new Precio()
                    {
                        id_actor_negocio = idCentroAtencion,
                        id_unidad_negocio = MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal,
                        id_concepto_negocio = idMercaderia,
                        valor = item.valor,
                        id_tarifa_d = item.id_tarifa_d,
                        id_moneda = MaestroSettings.Default.IdDetalleMaestroMonedaSoles,
                        fecha_inicio = fechaActual,
                        fecha_fin = fechaActual.AddMonths(ConceptoSettings.Default.PrecioDuracionPorDefectoEnMeses),
                        fecha_modificacion = fechaActual,
                        indicador_multiproposito = true,
                        es_vigente = true,
                        cantidad_minima = 0,
                        cantidad_maxima = 1,
                        id_tipo = MaestroSettings.Default.IdDetalleMaestroTipoPrecioPrecio,
                        id_responsable = idEmpleado,


                };
                    precios.Add(precio);
                }
                return _precioRepositorio.establecerPrecios(precios);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public OperationResult establecerPrecio(int idCentroAtencion, int idProducto, int idTarifa, decimal importe, DateTime fechaDesde, 
            DateTime fechaHasta, string descripcion, int idResponsable)
        {
            try
            {
                DateTime fechaActual = DateTimeUtil.FechaActual();
               
                int idUnidadDeNegocio = MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal;
                int idMoneda = MaestroSettings.Default.IdDetalleMaestroMonedaSoles;
                int idTipo = MaestroSettings.Default.IdDetalleMaestroTipoPrecioPrecio;
                int cantidadMinima = 1;
                int cantidadMaxima = 0;

                //creamos el precio
                Precio precio = new Precio(idCentroAtencion, idUnidadDeNegocio, idProducto, importe, idTarifa, idMoneda, fechaDesde,
                    fechaHasta, fechaActual, true, true,false,cantidadMinima,cantidadMaxima,idTipo,descripcion, idResponsable);

                return _precioRepositorio.EstablecerPrecio(precio);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public OperationResult EstablecerPrecio(List<Precio_Compra_Venta_Concepto> precios, int idConcepto, int idEmpleado)
        {
            try
            {
                OperationResult result = new OperationResult();
                DateTime fechaActual = DateTimeUtil.FechaActual();
                int idUnidadDeNegocio = MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal;
                int idMoneda = MaestroSettings.Default.IdDetalleMaestroMonedaSoles;
                int idTipo = MaestroSettings.Default.IdDetalleMaestroTipoPrecioPrecio;
                int cantidadMinima = 1;
                int cantidadMaxima = 0;

                foreach (var item in precios)
                {
                    if (item.Seleccionado)
                    {
                        Precio precio = new Precio(item.IdPrecio, item.IdPuntoPrecio, idUnidadDeNegocio, idConcepto, item.Valor, item.IdTarifa, idMoneda, item.FechaInicio, item.FechaFin, fechaActual, true, true, false, cantidadMinima, cantidadMaxima, idTipo, item.Descripcion, idEmpleado);
                        result = _precioRepositorio.EstablecerPrecio(precio);
                    }
                    else
                    {
                        if(item.IdPrecio != 0)
                        {
                            result = caducarPrecio(item.IdPrecio);
                        }
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al establecer los precios", e);
            }
        }

        public OperationResult crearDescuento(int idCentroAtencion, int idProducto, bool porcentaje, decimal valor, int cantidadMinima, int cantidadMaxima, 
            DateTime fechaDesde, DateTime fechaHasta, string descripcion, int idResponsable)
        {
            DateTime fechaActual = DateTimeUtil.FechaActual();
            int idUnidadDeNegocio = MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal;
            int idMoneda = MaestroSettings.Default.IdDetalleMaestroMonedaSoles;
            int idTipo = MaestroSettings.Default.IdDetalleMaestroTipoPrecioDescuento;
            int idTarifa = MaestroSettings.Default.IdDetalleMaestroTarifaNormal;
            //creamos el precio
            Precio precio = new Precio(idCentroAtencion, idUnidadDeNegocio, idProducto, valor, idTarifa, idMoneda, fechaDesde,
                fechaHasta, fechaActual, true, true, porcentaje, cantidadMinima, cantidadMaxima, idTipo,descripcion, idResponsable);

            return _precioRepositorio.EstablecerPrecio(precio);
        }

        public OperationResult crearBonificacion(int idCentroAtencion, int idProducto, bool porcentaje, decimal valor, int cantidadMinima, int cantidadMaxima, DateTime fechaDesde, DateTime fechaHasta, int idProductoReferencia, string descripcion, int idResponsable)
        {
            DateTime fechaActual = DateTimeUtil.FechaActual();
            int idUnidadDeNegocio = MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal;
            int idMoneda = MaestroSettings.Default.IdDetalleMaestroMonedaSoles;
            int idTipo = MaestroSettings.Default.IdDetalleMaestroTipoPrecioBonificacion;
            int idTarifa = MaestroSettings.Default.IdDetalleMaestroTarifaNormal;
            //creamos el precio
            Precio precio = new Precio(idCentroAtencion, idUnidadDeNegocio, idProducto, valor, idTarifa, idMoneda, fechaDesde,
                fechaHasta, fechaActual, true, true, porcentaje, cantidadMinima, cantidadMaxima, idTipo,idProductoReferencia,descripcion, idResponsable);

            return _precioRepositorio.CrearPrecio(precio);
        }

        
        public OperationResult caducarPrecio(int idPrecio)
        {
            try
            {
                DateTime fechaActual = DateTimeUtil.FechaActual();
                Precio precio = _precioRepositorio.obtenerPrecio(idPrecio);
                precio.fecha_fin = fechaActual;
                precio.fecha_modificacion = fechaActual;
                precio.es_vigente = false;
                return _precioRepositorio.ActualizarPrecio(precio);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
        #region consultas
        public List<Precio> obtenerPreciosVigentes()
        {
            try
            {
                return _precioRepositorio.obtenerPreciosVigentes().ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Precio_Concepto> ObtenerPrecios()
        {
            try
            {
                return _precioRepositorio.ObtenerPrecios(MaestroSettings.Default.IdDetalleMaestroTipoPrecioPrecio).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Precio> obtenerDescuentos()
        {
            try
            {
                return null;// _precioRepositorio.obtenerPrecios(MaestroSettings.Default.IdDetalleMaestroTipoPrecioDescuento).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Precio> obtenerBonificaciones()
        {
            try
            {
                return null;// _precioRepositorio.obtenerPrecios(MaestroSettings.Default.IdDetalleMaestroTipoPrecioBonificacion).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Precio obtenerPrecio(int idPrecio)
        {
            try
            {
                return _precioRepositorio.obtenerPrecio(idPrecio);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Precio> ObtenerPreciosMercaderiaUnica(int idMercaderia)
        {
            try
            {
                return _precioRepositorio.ObtenerPreciosVigentesDeUnConceptoNegocio(MaestroSettings.Default.IdDetalleMaestroTipoPrecioPrecio,idMercaderia).OrderBy(  p => p.id_tarifa_d).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion
    }
}

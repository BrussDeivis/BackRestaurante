using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.AccesoDatos.Generico;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Custom;
using Tsp.Sigescom.Modelo.Custom.SigesParking;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;

namespace Tsp.Sigescom.AccesoDatos
{
    public partial class CocheraDatos : RepositorioBase, ICocheraRepositorio
    {
        public Vehiculo ObtenerVehiculo(string placaVehiculo)
        {

            return _db.Actor_negocio.Where(an => an.Actor.numero_documento_identidad == placaVehiculo && an.id_rol == CocheraSettings.Default.IdRolVehiculo && an.es_vigente).Select(an => new Vehiculo
            {
                Id = an.id,
                IdActor = an.id_actor,
                Placa = an.Actor.numero_documento_identidad,
                TipoDeVehiculo = new ItemGenerico { Id = an.Actor.id_clase_actor, Nombre = an.Actor.primer_nombre },
                Marca = new ItemGenerico { Id = (int)an.Actor.id_detalle_multiproposito, Nombre = an.Actor.segundo_nombre },
                Color = an.Actor.tercer_nombre,
                ExoneradoDePagos = an.indicador1
            }
                ).SingleOrDefault();

        }

        public bool ExisteVehiculo(string placaVehiculo)
        {
            return _db.Actor_negocio.Any(an => an.id_rol == CocheraSettings.Default.IdRolVehiculo && an.Actor.id_documento_identidad == CocheraSettings.Default.IdTipoDocumentoIdentidadPlaca && an.Actor.numero_documento_identidad == placaVehiculo);
        }

        public MovimientoCochera ObtenerTransaccion_MovimientoCochera(string placaVehiculo, int idCentroAtencionCochera, int idDetalleMaestroEstadoActual)
        {
            try
            {
                var result = _db.Actor_negocio.SingleOrDefault(an => an.Actor.id_documento_identidad == CocheraSettings.Default.IdTipoDocumentoIdentidadPlaca && an.Actor.numero_documento_identidad == placaVehiculo).Transaccion3.Where(t => t.id_actor_negocio_interno == idCentroAtencionCochera && t.id_tipo_transaccion == CocheraSettings.Default.IdTipoTransaccionMovimientoDeCochera && t.id_estado_actual == (int)idDetalleMaestroEstadoActual).Select(t => new MovimientoCochera()
                {
                    Id = t.id,
                    IdOrdenDeVenta = t.id_transaccion_referencia ?? 0,
                    IdCochera = t.id_actor_negocio_interno,
                    Estado = new ItemGenerico { Id = (int)t.id_estado_actual, Nombre = t.Detalle_maestro.nombre },
                    Comprobante = new ComprobanteDeNegocioBasico_
                    {
                        Id = t.id_comprobante,
                        Serie = t.Comprobante.numero_serie,
                        Numero = t.Comprobante.numero.ToString()
                    },
                    Cliente = new ActorComercial_
                    {
                        Id = t.id_actor_negocio_externo,
                        NumeroDocumentoIdentidad = t.Actor_negocio1.Actor.numero_documento_identidad,
                        NombreORazonSocial = t.Actor_negocio1.Actor.primer_nombre.Replace("|", " "),
                        TipoDocumentoIdentidad = new ItemGenerico(t.Actor_negocio1.Actor.id_documento_identidad),
                    },
                    Ingreso = t.fecha_inicio,
                    Salida = t.fecha_fin,
                    SistemaDePago = new ItemGenerico { Id = t.enum1, Nombre = ((SistemaPagoCochera)t.enum1).ToString() },
                    Vehiculo = new Vehiculo
                    {
                        Placa = t.Actor_negocio3.Actor.numero_documento_identidad,
                        TipoDeVehiculo = new ItemGenerico { Id = t.Actor_negocio3.Actor.id_clase_actor, Nombre = t.Actor_negocio3.Actor.primer_nombre },
                        Marca = new ItemGenerico { Id = (int)t.Actor_negocio3.Actor.id_detalle_multiproposito, Nombre = t.Actor_negocio3.Actor.segundo_nombre },
                        Color = t.Actor_negocio3.Actor.tercer_nombre,
                        ExoneradoDePagos = t.Actor_negocio3.indicador1
                    }
                }).FirstOrDefault();
                return result;
            }
            catch (Exception e)
            {
                throw new DatosException("Error al tratar de obtener movimiento de cochera", e);
            }
        }
        public IEnumerable<MovimientoCocheraBasico> ObtenerTransacciones_MovimientosCochera(int id_cochera, DateTime desde, DateTime hasta)
        {
            var result = _db.Transaccion.Where(t => t.id_actor_negocio_interno == id_cochera && t.id_tipo_transaccion == CocheraSettings.Default.IdTipoTransaccionMovimientoDeCochera && t.fecha_inicio >= desde && t.fecha_fin <= hasta).Select(t => new MovimientoCocheraBasico()
            {
                Id = t.id,
                IdOrdenDeVenta = t.id_transaccion_referencia ?? 0,
                IdCochera = t.id_actor_negocio_interno,
                Estado = new ItemGenerico { Id = (int)t.id_estado_actual, Nombre = t.Detalle_maestro.nombre },
                Comprobante = new ComprobanteDeNegocioBasico_
                {
                    Id = t.id_comprobante,
                    Serie = t.Comprobante.numero_serie,
                    Numero = t.Comprobante.numero.ToString()
                },
                Ingreso = t.fecha_inicio,
                Salida = t.fecha_fin,
                SistemaDePago = new ItemGenerico { Id = t.enum1, Nombre = ((SistemaPagoCochera)t.enum1).ToString() },
                Vehiculo = new Vehiculo
                {
                    Placa = t.Actor_negocio3.Actor.numero_documento_identidad,
                    TipoDeVehiculo = new ItemGenerico { Id = t.Actor_negocio3.Actor.id_clase_actor, Nombre = t.Actor_negocio3.Actor.primer_nombre },
                    Marca = new ItemGenerico { Id = (int)t.Actor_negocio3.Actor.id_detalle_multiproposito, Nombre = t.Actor_negocio3.Actor.segundo_nombre },
                    Color = t.Actor_negocio3.Actor.tercer_nombre,
                    ExoneradoDePagos = t.Actor_negocio3.indicador1
                }
            }); ;
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_cochera"></param>
        /// <param name="desde"></param>
        /// <param name="hasta"></param>
        /// <returns></returns>
        public IEnumerable<EntradaSalida> ObtenerMovimientos(int id_cochera, DateTime desde, DateTime hasta)
        {
            IEnumerable<EntradaSalida> result;

            result = from et in _db.Estado_transaccion
                     join t in _db.Transaccion on et.id_transaccion equals t.id
                     where et.fecha >= desde && et.fecha <= hasta && t.id_actor_negocio_interno == id_cochera && t.id_tipo_transaccion == CocheraSettings.Default.IdTipoTransaccionMovimientoDeCochera
                     select new EntradaSalida()
                     {
                         IdGeneral = t.id,
                         IdEspecifico = et.id,
                         FechaHora = et.fecha,
                         IdTipoMovimiento = et.id_estado,
                         Ticket = t.Comprobante.numero_serie + "-" + t.Comprobante.numero.ToString(),
                         PlacaVehiculo = t.Actor_negocio3.Actor.numero_documento_identidad,
                         DescripcionVehiculo = t.Actor_negocio3.Actor.primer_nombre + " " + t.Actor_negocio3.Actor.segundo_nombre + " " + t.Actor_negocio3.Actor.tercer_nombre
                     };

            return result;
        }

        /// <summary>
        /// Devuelve todos los movimientos cuyo estado actual  corresponda con <paramref name="idEstado"/>
        /// Por ejemplo si necesito los vehiculos actualmente en cochera, debo solicitar aquellos cuyo id sea el de ingresado en cochera.
        /// </summary>
        /// <param name="id_cochera"></param>
        /// <param name="idEstado"> indica el id del tipo de movimiento que representa a la entrada o salida. En terminos de datos es el id_estado de la transaccion</param>
        /// <returns></returns>
        public IEnumerable<EntradaSalidaUsuario> ObtenerMovimientos(int id_cochera, int idEstado)
        {
            IEnumerable<EntradaSalidaUsuario> result;

            result = from t in _db.Transaccion
                     where t.id_actor_negocio_interno == id_cochera && t.id_estado_actual == idEstado
                     select new EntradaSalidaUsuario()
                     {
                         FechaHora = t.fecha_inicio,
                         IdTipoMovimiento = idEstado,
                         Ticket = t.Comprobante.numero_serie + t.Comprobante.numero.ToString(),
                         PlacaVehiculo = t.Actor_negocio3.Actor.numero_documento_identidad,
                         DescripcionVehiculo = t.Actor_negocio3.Actor.primer_nombre + " " + t.Actor_negocio3.Actor.segundo_nombre + " " + t.Actor_negocio3.Actor.tercer_nombre,
                         DocumentoUsuario = t.Actor_negocio1.Actor.numero_documento_identidad,
                         NombreUsuario = t.Actor_negocio1.Actor.segundo_nombre,
                     };

            return result;
        }
        public ConfiguracionCochera ObtenerConfiguracion(int idCochera)
        {
            ConfiguracionCochera configuracion = null;
            var actor_negocio_Cochera = _db.Actor_negocio.SingleOrDefault(an => an.id == idCochera);
            configuracion = JsonConvert.DeserializeObject<ConfiguracionCochera>(actor_negocio_Cochera.extension_json);
            configuracion.IdTipoDeTurno = actor_negocio_Cochera.id_detalle_maestro_multiproposito ?? 0;
            return configuracion;
        }
        public List<ExoneracionDeVehiculo> ObtenerExoneracionesVigentes(int idCochera)
        {
            return
                _db.Vinculo_Actor_Negocio.Where(van => van.id_actor_negocio_principal == idCochera && van.es_vigente && van.tipo_vinculo == (int)TipoVinculo.VehiculoExoneradoEnCochera)
                .Select(van => new ExoneracionDeVehiculo
                {
                    Vehiculo = new Vehiculo
                    {
                        Placa = van.Actor_negocio1.Actor.numero_documento_identidad,
                        TipoDeVehiculo = new ItemGenerico { Id = van.Actor_negocio1.Actor.id_clase_actor, Nombre = van.Actor_negocio1.Actor.primer_nombre },
                        Marca = new ItemGenerico { Id = (int)van.Actor_negocio1.Actor.id_detalle_multiproposito, Nombre = van.Actor_negocio1.Actor.segundo_nombre },
                        Color = van.Actor_negocio1.Actor.tercer_nombre,
                        ExoneradoDePagos = van.Actor_negocio1.indicador1
                    },
                    Desde = van.desde,
                    Hasta = van.hasta
                }).ToList();
        }

        public List<ExoneracionDeVehiculo> ObtenerExoneraciones(int idCochera)
        {
            return
               _db.Vinculo_Actor_Negocio.Where(van => van.id_actor_negocio_principal == idCochera && van.tipo_vinculo == (int)TipoVinculo.VehiculoExoneradoEnCochera)
                .Select(van => new ExoneracionDeVehiculo
                {
                    Id = van.id,
                    IdCochera = van.id_actor_negocio_principal,
                    Vehiculo = new Vehiculo
                    {
                        Placa = van.Actor_negocio1.Actor.numero_documento_identidad,
                        TipoDeVehiculo = new ItemGenerico { Id = van.Actor_negocio1.Actor.id_clase_actor, Nombre = van.Actor_negocio1.Actor.primer_nombre },
                        Marca = new ItemGenerico { Id = (int)van.Actor_negocio1.Actor.id_detalle_multiproposito, Nombre = van.Actor_negocio1.Actor.segundo_nombre },
                        Color = van.Actor_negocio1.Actor.tercer_nombre,
                        ExoneradoDePagos = van.Actor_negocio1.indicador1
                    },
                    Desde = van.desde,
                    Hasta = van.hasta,
                    Vigente = van.es_vigente
                }).ToList();
        }

    }
}

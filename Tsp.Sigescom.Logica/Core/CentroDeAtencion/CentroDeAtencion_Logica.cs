using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Custom;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Actores;
using Tsp.Sigescom.Modelo.Interfaces.Datos.CentrosDeAtencion;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Establecimientos;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;
using Tsp.Sigescom.Modelo.Negocio.Almacen;
using Tsp.Sigescom.Modelo.SigesRestaurant;

namespace Tsp.Sigescom.Logica.Core.CentrosDeAtencion
{
    public class CentroDeAtencion_Logica : ICentroDeAtencion_Logica
    {
        private readonly ICentroDeAtencion_Repositorio _centroDeAtencionRepositorio;
        private readonly IEstablecimiento_Repositorio _establecimientoRepositorio;
        private readonly IRoles_Repositorio _rolesRepositorio;
        private readonly IActor_Repositorio _actorRepositorio;
        private readonly IInventarioActual_Logica _inventarioActual_Logica;

        public CentroDeAtencion_Logica(ICentroDeAtencion_Repositorio centroDeAtencionRepositorio, IEstablecimiento_Repositorio establecimientoRepositorio, IRoles_Repositorio rolesRepositorio, IActor_Repositorio actorRepositorio, IInventarioActual_Logica inventarioActual_Logica)
        {
            _centroDeAtencionRepositorio = centroDeAtencionRepositorio;
            _establecimientoRepositorio = establecimientoRepositorio;
            _rolesRepositorio = rolesRepositorio;
            _actorRepositorio = actorRepositorio;
            _inventarioActual_Logica = inventarioActual_Logica;

        }


        public List<CentroDeAtencionExtendido> ObtenerCentrosDeAtencionProgramados(int idEmpleado)
        {
            return _centroDeAtencionRepositorio.ObtenerCentrosDeAtencionProgramados(idEmpleado);
        }

        public List<CentroDeAtencion> ObtenerCentrosDeAtencionProgramados_(int idEmpleado)
        {
            return _centroDeAtencionRepositorio.ObtenerCentrosDeAtencionProgramados_(idEmpleado).ToList();
        }

        public CentroDeAtencionExtendido ObtenerCentroDeAtencion(int id)
        {
            try
            {
                return _centroDeAtencionRepositorio._ObtenerCentroDeAtencion(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        public List<CentroDeAtencionExtendido> ObtenerCentrosDeAtencionExtendidosVigentes()
        {

            return _centroDeAtencionRepositorio.ObtenerCentrosDeAtencionExtendidosVigentes().ToList();
        }

        public List<CentroDeAtencion> ObtenerCentrosDeAtencionVigentes()
        {
            try
            {
                return _centroDeAtencionRepositorio.ObtenerCentrosDeAtencionVigentes().ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<CentroDeAtencionExtendido> ObtenerPuntosDeVentaNoVigentes()
        {
            return _centroDeAtencionRepositorio.ObtenerCentrosDeAtencionExtendidosSegunRolHijo(ActorSettings.Default.IdRolPuntaDeVenta, false).ToList();
        }

        public List<CentroDeAtencionExtendido> ObtenerPuntosDeCompraNoVigentes()
        {
            return _centroDeAtencionRepositorio.ObtenerCentrosDeAtencionExtendidosSegunRolHijo(ActorSettings.Default.IdRolPuntoDeCompra, false).ToList();
        }

        public List<CentroDeAtencionExtendido> ObtenerAlmacenesNoVigentes()
        {
            return _centroDeAtencionRepositorio.ObtenerCentrosDeAtencionExtendidosSegunRolHijo(ActorSettings.Default.IdRolAlmacen, false).ToList();
        }
        public List<CentroDeAtencionExtendido> ObtenerPuntosDeVentaVigentes()
        {
            return _centroDeAtencionRepositorio.ObtenerCentrosDeAtencionExtendidosSegunRolHijo(ActorSettings.Default.IdRolPuntaDeVenta, true).ToList();
        }

        public List<CentroDeAtencionExtendido> ObtenerPuntosDeCompraVigentes()
        {
            return _centroDeAtencionRepositorio.ObtenerCentrosDeAtencionExtendidosSegunRolHijo(ActorSettings.Default.IdRolPuntoDeCompra, true).ToList();
        }

        public List<CentroDeAtencionExtendido> ObtenerAlmacenesVigentes()
        {
            return _centroDeAtencionRepositorio.ObtenerCentrosDeAtencionExtendidosSegunRolHijo(ActorSettings.Default.IdRolAlmacen, true).ToList();
        }

        public List<CentroDeAtencionExtendido> ObtenerCajasVigentes()
        {
            return _centroDeAtencionRepositorio.ObtenerCentrosDeAtencionExtendidosSegunRolHijo(ActorSettings.Default.IdRolCaja, true).ToList();
        }

        public int ObtenerIdCentroDeAtencionParaObtencionDePrecios(CentroDeAtencion centroDeAtencion, EstablecimientoComercialExtendido establecimiento)
        {
            try
            {
                var politicaDePrecio = VentasSettings.Default.PoliticaDePreciosParaVenta;
                int idCentroDeAtencionParaPrecio = 0;
                if (politicaDePrecio == (int)PoliticaDePreciosParaVentaEnum.Global)//Global
                {
                    idCentroDeAtencionParaPrecio = VentasSettings.Default.IdCentroAtencionParaObtencionDePreciosPorPoliticaGlobal;
                }
                else if (politicaDePrecio == (int)PoliticaDePreciosParaVentaEnum.EstablecimientoComercial)//Establecimientos Comerciales
                {

                    idCentroDeAtencionParaPrecio = (int)establecimiento.IdCentroDeAtencionParaObtencionDePrecios;
                }
                else if (politicaDePrecio == (int)PoliticaDePreciosParaVentaEnum.CentroDeAtencion)//Centro de Atencion
                {
                    idCentroDeAtencionParaPrecio = centroDeAtencion.Id;
                }
                return idCentroDeAtencionParaPrecio;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int ObtenerIdCentroDeAtencionParaObtencionDePrecios(int idCentroAtencion)
        {
            try
            {
                var politicaDePrecio = VentasSettings.Default.PoliticaDePreciosParaVenta;
                int idCentroDeAtencionParaPrecio = 0;
                if (politicaDePrecio == (int)PoliticaDePreciosParaVentaEnum.Global)//Global
                {
                    idCentroDeAtencionParaPrecio = VentasSettings.Default.IdCentroAtencionParaObtencionDePreciosPorPoliticaGlobal;
                }
                else if (politicaDePrecio == (int)PoliticaDePreciosParaVentaEnum.EstablecimientoComercial)//Establecimientos Comerciales
                {
                    //tengo un centro de atencion CA y necesito conseguir el id del centro de atención que tiene el precio para el establecimiento al que pertenece CA
                    idCentroDeAtencionParaPrecio = _centroDeAtencionRepositorio.ObtenerIdDelCentroDeAtencionQueTieneLosPreciosSegunIdCentroDeAtencion(idCentroAtencion);
                }
                else if (politicaDePrecio == (int)PoliticaDePreciosParaVentaEnum.CentroDeAtencion)//Centro de Atencion
                {
                    idCentroDeAtencionParaPrecio = idCentroAtencion;
                }
                return idCentroDeAtencionParaPrecio;
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        public int ObtenerIdCentroDeAtencionParaObtencionDeStock(ModoOperacionEnum tipoDeVenta, CentroDeAtencion centroDeAtencion, EstablecimientoComercialExtendidoConLogo establecimiento)
        {
            try
            {
                int politicaDeStock = 0;
                int idCentroDeAtencionParaObtenerStock = 0;
                if (tipoDeVenta == ModoOperacionEnum.PorMostrador)
                {
                    politicaDeStock = VentasSettings.Default.PoliticaDeStockParaVentaPorMostrador;
                }
                else if (tipoDeVenta == ModoOperacionEnum.PorMostradorEnDosPasos)
                {
                    politicaDeStock = VentasSettings.Default.PoliticaDeStockParaVentaPorMostradorEnDosPasos;
                }
                else if (tipoDeVenta == ModoOperacionEnum.Corporativa)
                {
                    politicaDeStock = VentasSettings.Default.PoliticaDeStockParaVentaCorporativa;
                }
                //Verificamos de acuerdo a la polica de manejeo del stock
                if (politicaDeStock == (int)PoliticaDePreciosParaVentaEnum.Global)//Global
                {
                    idCentroDeAtencionParaObtenerStock = VentasSettings.Default.IdCentroAtencionParaObtencionDeStockPorPoliticaGlobal;
                }
                else if (politicaDeStock == (int)PoliticaDePreciosParaVentaEnum.EstablecimientoComercial)//Establecimientos Comerciales
                {
                    idCentroDeAtencionParaObtenerStock = (int)establecimiento.IdCentroDeAtencionParaObtencionDeStock;
                }
                else if (politicaDeStock == (int)PoliticaDePreciosParaVentaEnum.CentroDeAtencion)//Centro de Atencion
                {
                    idCentroDeAtencionParaObtenerStock = centroDeAtencion.Id;
                }
                return idCentroDeAtencionParaObtenerStock;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public CentroDeAtencion ObtenerCentroDeAtencion_(int idCentroAtencion)
        {
            try
            {
                return _centroDeAtencionRepositorio.ObtenerCentroDeAtencion_(idCentroAtencion);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener el centro de atencion", e);
            }
        }
        public int ObtenerIdCentroDeAtencionParaObtencionDeStock(ModoOperacionEnum tipoDeVenta, int idCentroAtencion)
        {
            try
            {
                int politicaDeStock = 0;
                int idCentroDeAtencionParaObtenerStock = 0;
                if (tipoDeVenta == ModoOperacionEnum.PorMostrador)
                {
                    politicaDeStock = VentasSettings.Default.PoliticaDeStockParaVentaPorMostrador;
                }
                else if (tipoDeVenta == ModoOperacionEnum.PorMostradorEnDosPasos)
                {
                    politicaDeStock = VentasSettings.Default.PoliticaDeStockParaVentaPorMostradorEnDosPasos;
                }
                else if (tipoDeVenta == ModoOperacionEnum.Corporativa)
                {
                    politicaDeStock = VentasSettings.Default.PoliticaDeStockParaVentaCorporativa;
                }
                //Verificamos de acuerdo a la polica de manejeo del stock
                if (politicaDeStock == (int)PoliticaDePreciosParaVentaEnum.Global)//Global
                {
                    idCentroDeAtencionParaObtenerStock = VentasSettings.Default.IdCentroAtencionParaObtencionDeStockPorPoliticaGlobal;
                }
                else if (politicaDeStock == (int)PoliticaDePreciosParaVentaEnum.EstablecimientoComercial)//Establecimientos Comerciales
                {
                    idCentroDeAtencionParaObtenerStock = _centroDeAtencionRepositorio.ObtenerIdDelCentroDeAtencionQueTieneElStockSegunIdCentroDeAtencion(idCentroAtencion);
                }
                else if (politicaDeStock == (int)PoliticaDePreciosParaVentaEnum.CentroDeAtencion)//Centro de Atencion
                {
                    idCentroDeAtencionParaObtenerStock = idCentroAtencion;
                }
                return idCentroDeAtencionParaObtenerStock;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string ObtenerNombreDeCentroDeAtencion(int idActorNegocio)
        {
            try
            {
                return _centroDeAtencionRepositorio.ObtenerNombreDeCentroDeAtencion(idActorNegocio);
            }
            catch (Exception e)
            {
                throw new LogicaException("No se pudo obtener el primer nombre del centro de atencion", e);
            }
        }

        public CentroDeAtencionExtendido ObtenerCentroDeAtencionSegunSerieComprobante(int idSerie)
        {
            try
            {
                return _centroDeAtencionRepositorio.ObtenerCentroDeAtencionExtendidosSegunSerieComprobante(idSerie);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<RolDeNegocio> ObtenerRolesDeCentroDeAtencion()
        {
            try
            {
                return RolDeNegocio.Convert_(_rolesRepositorio.ObtenerRolesHijos(ActorSettings.Default.IdRolEntidadInterna).ToList());
            }
            catch (Exception e) { throw e; }
        }

        public List<DetalleGenerico> ObtenerPuntosDePrecio()
        {
            try
            {
                List<DetalleGenerico> puntosDePrecio = new List<DetalleGenerico>();
                int politicaDePrecio = VentasSettings.Default.PoliticaDePreciosParaVenta;
                if (politicaDePrecio == (int)PoliticaDePreciosParaVentaEnum.Global)//Global
                {
                    var centroDeAtencionPrecioGlobal = _centroDeAtencionRepositorio._ObtenerCentroDeAtencion(VentasSettings.Default.IdCentroAtencionParaObtencionDePreciosPorPoliticaGlobal);
                    puntosDePrecio.Add(new DetalleGenerico(centroDeAtencionPrecioGlobal.Id, centroDeAtencionPrecioGlobal.EstablecimientoComercial.NombreInterno + " - " + centroDeAtencionPrecioGlobal.Nombre));
                }
                else if (politicaDePrecio == (int)PoliticaDePreciosParaVentaEnum.EstablecimientoComercial)//Establecimientos Comerciales
                {
                    var centrosDeAtencionConPrecioPorEstablecimiento = _centroDeAtencionRepositorio.ObtenerCentrosDeAtencionConPrecioDeCadaEstablecimientoVigente().ToList();
                    centrosDeAtencionConPrecioPorEstablecimiento.ForEach(ca => puntosDePrecio.Add(new DetalleGenerico(ca.Id, ca.EstablecimientoComercial.NombreInterno + " - " + ca.Nombre)));
                }
                else if (politicaDePrecio == (int)PoliticaDePreciosParaVentaEnum.CentroDeAtencion)//Centro de Atencion
                {
                    var centrosDeAtencion = _centroDeAtencionRepositorio.ObtenerCentrosDeAtencionVigentes();
                    foreach (var centroDeAtencion in centrosDeAtencion)
                    {
                        puntosDePrecio.Add(new DetalleGenerico(centroDeAtencion.Id, centroDeAtencion.EstablecimientoComercial.NombreInterno + " - " + centroDeAtencion.Nombre));
                    }
                }
                puntosDePrecio = puntosDePrecio.OrderBy(pp => pp.Id).ToList();
                return puntosDePrecio;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener los puntos de ventas", e);
            }
        }


        public List<CentroDeAtencionExtendido> ObtenerCentrosDeAtencionVigentesPorEstablecimientoComercial(int idEstablecimientoComercial)
        {
            try
            {
                return _centroDeAtencionRepositorio.ObtenerCentrosDeAtencionExtendidosVigentesPorEstablecimientoComercial(idEstablecimientoComercial).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<CentroDeAtencionExtendido> ObtenerPuntosDeVentaVigentesPorEstablecimientoComercial(int idEstablecimientoComercial)
        {
            try
            {
                return _centroDeAtencionRepositorio.ObtenerCentrosDeAtencionExtendidosVigentesSegunRolYEstablecimientoComercial(ActorSettings.Default.IdRolPuntaDeVenta, idEstablecimientoComercial).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener centros de atencion con rol punto de venta", e);
            }

        }

        public List<CentroDeAtencionExtendido> ObtenerPuntosDeCompraVigentesPorEstablecimientoComercial(int idEstablecimientoComercial)
        {
            try
            {
                return _centroDeAtencionRepositorio.ObtenerCentrosDeAtencionExtendidosVigentesSegunRolYEstablecimientoComercial(ActorSettings.Default.IdRolPuntoDeCompra, idEstablecimientoComercial).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener centros de atencion con rol punto de compra", e);
            }

        }

        public List<CentroDeAtencionExtendido> ObtenerAlmacenesVigentesPorEstablecimientoComercial(int idEstablecimientoComercial)
        {
            try
            {
                return _centroDeAtencionRepositorio.ObtenerCentrosDeAtencionExtendidosVigentesSegunRolYEstablecimientoComercial(ActorSettings.Default.IdRolAlmacen, idEstablecimientoComercial).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener centros de atencion con rol almacen", e);
            }
        }

        public List<CentroDeAtencionExtendido> ObtenerCajasVigentesPorEstablecimientoComercial(int idEstablecimientoComercial)
        {
            try
            {
                return _centroDeAtencionRepositorio.ObtenerCentrosDeAtencionExtendidosVigentesSegunRolYEstablecimientoComercial(ActorSettings.Default.IdRolCaja, idEstablecimientoComercial).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener centros de atencion con rol caja", e);
            }
        }

        public int[] ObtenerIdsCentrosAtencionConRolPuntoVenta(int idEstablecimientoComercial)
        {
            try
            {
                return _centroDeAtencionRepositorio.ObtenerIdsDeCentrosDeAtencionVigentesSegunRolYEstablecimientoComercial(ActorSettings.Default.IdRolPuntaDeVenta, idEstablecimientoComercial);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener centros de atencion con rol caja", e);
            }
        }

        public List<CentroDeAtencionExtendido> ObtenerPuntosDeVentaVigentesPorEstablecimientosComerciales(List<int> idsEstablecimientosComerciales)
        {
            try
            {
                return _centroDeAtencionRepositorio.ObtenerCentrosDeAtencionExtendidosVigentesSegunRolIdsDeEstablecimientos(ActorSettings.Default.IdRolPuntaDeVenta, idsEstablecimientosComerciales).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener centros de atencion con rol punto de venta", e);
            }

        }

        public List<CentroDeAtencionExtendido> ObtenerPuntosDeCompraVigentesPorEstablecimientosComerciales(List<int> idsEstablecimientosComerciales)
        {
            try
            {
                return _centroDeAtencionRepositorio.ObtenerCentrosDeAtencionExtendidosVigentesSegunRolIdsDeEstablecimientos(ActorSettings.Default.IdRolPuntoDeCompra, idsEstablecimientosComerciales).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener centros de atencion con rol punto de compra", e);
            }

        }



        public List<CentroDeAtencionExtendido> ObtenerAlmacenesVigentesPorEstablecimientosComerciales(List<int> idsEstablecimientosComerciales)
        {
            try
            {
                return _centroDeAtencionRepositorio.ObtenerCentrosDeAtencionExtendidosVigentesSegunRolIdsDeEstablecimientos(ActorSettings.Default.IdRolAlmacen, idsEstablecimientosComerciales).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener centros de atencion con rol almacen", e);
            }
        }

        public List<CentroDeAtencionExtendido> ObtenerCajasVigentesPorEstablecimientosComerciales(List<int> idsEstablecimientosComerciales)
        {
            try
            {
                return _centroDeAtencionRepositorio.ObtenerCentrosDeAtencionExtendidosVigentesSegunRolIdsDeEstablecimientos(ActorSettings.Default.IdRolCaja, idsEstablecimientosComerciales).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener centros de atencion con rol caja", e);
            }
        }

        public CentroDeAtencionExtendido ObtenerCentroDeAtencionSucursalOSede(int idCentroDeAtencion, int idActorDeNegocioPadre)
        {
            try
            {
                return _centroDeAtencionRepositorio.ObtenerCentroDeAtencionExtendidoPorIdDeCentroDeAtencionEIdDeEstablecimiento(idCentroDeAtencion, idActorDeNegocioPadre);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public OperationResult DarDeBajaCentroDeAtencion(int idCentroDeAtencion)
        {
            try
            {
                return _actorRepositorio.DarDeBajaActorNegocioAhora(idCentroDeAtencion, ActorSettings.Default.IdRolEntidadInterna);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public OperationResult EstablecerCentroDeAtencionParaPreciosYStockDeEstablecimientoComercial(int idEstablecimientoComencial, int idCentroDeAtencionPrecios, int idCentroDeAtencionStock)
        {
            try
            {
                OperationResult result = new OperationResult();

                if (idCentroDeAtencionPrecios > 0)
                {
                    //Obtenemos el parametro de configuracion de actor de negocio el cual tiene el centro de atencion para el manejo de precios
                    Parametro_actor_negocio parametroCentroDeAtencionParaObtencionPrecios = _establecimientoRepositorio.ObtenerParametroCentroDeAtencionParaObtencionPrecios(idEstablecimientoComencial);
                    if (parametroCentroDeAtencionParaObtencionPrecios == null)
                    {
                        Parametro_actor_negocio parametro = new Parametro_actor_negocio(idEstablecimientoComencial, MaestroSettings.Default.IdDetalleMaestroParametroIdCentroDeAtencionParaObtencionPrecios, idCentroDeAtencionPrecios.ToString());
                        result = _actorRepositorio.CrearParametroActorNegocio(parametro);
                    }
                    else
                    {
                        parametroCentroDeAtencionParaObtencionPrecios.valor = idCentroDeAtencionPrecios.ToString();
                        result = _actorRepositorio.ActualizarParametroActorNegocio(parametroCentroDeAtencionParaObtencionPrecios);
                    }
                }
                if (idCentroDeAtencionStock > 0)
                {
                    Parametro_actor_negocio parametroCentroDeAtencionParaObtencionDeStock = _establecimientoRepositorio.ObtenerParametroCentroDeAtencionParaObtencionDeStock(idEstablecimientoComencial);
                    if (parametroCentroDeAtencionParaObtencionDeStock == null)
                    {
                        Parametro_actor_negocio parametro = new Parametro_actor_negocio(idEstablecimientoComencial, MaestroSettings.Default.IdDetalleMaestroParametroIdCentroDeAtencionParaObtencionStock, idCentroDeAtencionStock.ToString());
                        result = _actorRepositorio.CrearParametroActorNegocio(parametro);
                    }
                    else
                    {
                        parametroCentroDeAtencionParaObtencionDeStock.valor = idCentroDeAtencionStock.ToString();
                        result = _actorRepositorio.ActualizarParametroActorNegocio(parametroCentroDeAtencionParaObtencionDeStock);
                    }
                }

                return result;
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }


        public Actor CrearActorParaCentroDeAtencion(int idActor, string nombre)
        {
            string numeroDocumentoIdentidad;
            int idTipoPersona;
            int idEstadoLegalActor;
            int idClaseActor;
            ///La sucursal hereda o copia todos los datos basicos (tipo de cator, clase, estado legal) de sede
            EstablecimientoComercial sede = _establecimientoRepositorio.ObtenerEstablecimientoComercial(ActorSettings.Default.IdActorNegocioSede);
            numeroDocumentoIdentidad = sede.DocumentoIdentidad;
            idTipoPersona = sede.IdTipoPersona;
            idEstadoLegalActor = sede.IdEstadoLegal;
            idClaseActor = sede.IdClaseActor;
            Actor actor;
            if (idActor == 0)
            {
                actor = new Actor()
                {
                    id_documento_identidad = ActorSettings.Default.IdTipoDocumentoIdentidadRuc,
                    fecha_nacimiento = DateTimeUtil.FechaActual(),
                    numero_documento_identidad = numeroDocumentoIdentidad,
                    primer_nombre = nombre,
                    segundo_nombre = "",
                    telefono = "",
                    id_tipo_actor = idTipoPersona,
                    id_foto = ActorSettings.Default.IdFotoActorPorDefecto,
                    id_clase_actor = idClaseActor,
                    id_estado_legal = idEstadoLegalActor,
                    correo = "",
                    tercer_nombre = "",
                    pagina_web = "",
                    informacion_multiproposito = ""
                };
            }
            else
            {
                actor = new Actor()
                {
                    id = idActor,
                    id_documento_identidad = ActorSettings.Default.IdTipoDocumentoIdentidadRuc,
                    fecha_nacimiento = DateTimeUtil.FechaActual(),
                    numero_documento_identidad = numeroDocumentoIdentidad,
                    primer_nombre = nombre,
                    segundo_nombre = "",
                    telefono = "",
                    id_tipo_actor = idTipoPersona,
                    id_foto = ActorSettings.Default.IdFotoActorPorDefecto,
                    id_clase_actor = idClaseActor,
                    id_estado_legal = idEstadoLegalActor,
                    correo = "",
                    tercer_nombre = "",
                    pagina_web = "",
                    informacion_multiproposito = ""
                };
            }

            return actor;
        }

        public OperationResult CrearCentroDeAtencion(int idEmpleado, string codigo, string nombre, bool salidaBienesSinStock, List<int> idRoles, int idCentroDeAtencionPadre)
        {
            try
            {
                DateTime fechaActual = DateTimeUtil.FechaActual();
                DateTime fechaFin = fechaActual.AddYears(ActorSettings.Default.vigenciaEnAnyosPorDefectoDeActorDeNegocioEntidadInterna);
                Actor_negocio centroDeAtencion = new Actor_negocio(ActorSettings.Default.IdRolEntidadInterna, fechaActual, fechaFin, codigo, true, idCentroDeAtencionPadre, false, "")
                {
                    Actor = CrearActorParaCentroDeAtencion(0, nombre)
                };

                if (idRoles != null)
                {
                    foreach (var item in idRoles)
                    {
                        centroDeAtencion.Actor.Actor_negocio.Add(new Actor_negocio(item, fechaActual, fechaFin, "", true, idCentroDeAtencionPadre, false, ""));
                    }
                }
                centroDeAtencion.extension_json = idRoles.Contains(ActorSettings.Default.IdRolAlmacen) ? "{ salidabienessinstock: \"" + salidaBienesSinStock.ToString().ToLower() + "\" }" : "";
                OperationResult result = _actorRepositorio.CrearActorNegocio(centroDeAtencion);
                OperationResult resultadoInventarioActual = null;
                if (result.code_result == OperationResultEnum.Success && idRoles.Contains(ActorSettings.Default.IdRolAlmacen))
                {
                    try
                    {
                        resultadoInventarioActual = _inventarioActual_Logica.CrearInventarioActual((int)result.data, idEmpleado);
                    }
                    catch (Exception ee)
                    {
                        throw new LogicaException("Se ha logrado crear el centro de atención, pero no se logró crear el inventario. ", ee);
                    }

                }
                return resultadoInventarioActual ?? result;
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }

        public OperationResult ActualizarCentroDeAtencion(int idEmpleado, int idActor, int idCentroDeAtencion, string codigo, string nombre, bool salidaBienesSinStock, List<int> idRoles, int idCentroDeAtencionPadre)
        {
            try
            {
                codigo = String.IsNullOrEmpty(codigo) ? "" : codigo;
                List<_Existencia> existencias = new List<_Existencia>();
                List<Detalle_transaccion> detalles = new List<Detalle_transaccion>();
                DateTime fechaActual = DateTimeUtil.FechaActual();
                DateTime fechaFin = fechaActual.AddYears(ActorSettings.Default.vigenciaEnAnyosPorDefectoDeActorDeNegocioEntidadInterna);

                Actor actor = CrearActorParaCentroDeAtencion(idActor, nombre);
                Actor_negocio centroDeAtencion = new Actor_negocio(idCentroDeAtencion, idActor, ActorSettings.Default.IdRolEntidadInterna, fechaActual, fechaFin, codigo, true, false, "") { Actor = actor };
                if (idRoles != null)
                {
                    foreach (var item in idRoles)
                    {
                        centroDeAtencion.Actor.Actor_negocio.Add(new Actor_negocio(idActor, item, fechaActual, fechaFin, "", true, false, "", idCentroDeAtencionPadre));
                    }
                }
                centroDeAtencion.extension_json = idRoles.Contains(ActorSettings.Default.IdRolAlmacen) ? "{ salidabienessinstock: \"" + salidaBienesSinStock.ToString().ToLower() + "\" }" : "";
                //Actualizar el actor de negocio 
                OperationResult result = _actorRepositorio.ActualizarActorNegocio(centroDeAtencion);
                OperationResult resultInventarioActual = null;
                if (result.code_result == OperationResultEnum.Success && idRoles.Contains(ActorSettings.Default.IdRolAlmacen) && !_centroDeAtencionRepositorio.TieneInventarioActual(idCentroDeAtencion))
                {
                    resultInventarioActual = _inventarioActual_Logica.CrearInventarioActual((int)result.data, idEmpleado);
                }
                return resultInventarioActual ?? result;
            }
            catch (Exception e)
            {
                return new OperationResult(e);
            }
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Entidades.Sesion;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;
using Tsp.Sigescom.Modelo.Negocio.Almacen;

namespace Tsp.Sigescom.Logica
{
    public partial class ConceptoLogica : IConceptoLogica
    {
        private readonly ITransaccionRepositorio _transaccionRepositorio;
        private readonly IConceptoRepositorio _conceptoRepositorio;
        private readonly IMaestroRepositorio _maestroRepositorio;
        public readonly IPrecioRepositorio _precioRepositorio;
        public readonly IActorRepositorio _actorRepositorio;
        public readonly IActorNegocioLogica _actorLogica;
        public readonly IInventarioActual_Logica _inventarioActualLogica;


        public ConceptoLogica(ITransaccionRepositorio transaccionRepositorio, IConceptoRepositorio conceptoRepositorio, IMaestroRepositorio maestroRepositorio, IPrecioRepositorio precioRepositorio, IActorRepositorio actorRepositorio, IActorNegocioLogica actorLogica, IInventarioActual_Logica inventarioActualLogica)
        {
            _transaccionRepositorio = transaccionRepositorio;
            _conceptoRepositorio = conceptoRepositorio;
            _maestroRepositorio = maestroRepositorio;
            _precioRepositorio = precioRepositorio;
            _actorRepositorio = actorRepositorio;
            _actorLogica = actorLogica;
            _inventarioActualLogica = inventarioActualLogica;
        }

        #region CONCEPTOS

        public OperationResult GuardarConceptoBasico(string nombre, string valor, int[] idsCategoria, int[] idsCaracteristica)
        {
            try
            {
                //creamos el concepto como detalle maestro
                Detalle_maestro concepto = new Detalle_maestro
                {
                    id_maestro = MaestroSettings.Default.IdMaestroConcepto,
                    codigo = nombre.Length > 30 ? nombre.Substring(0, 30) : nombre,
                    nombre = nombre,
                    valor = valor,
                    fecha_registro = DateTime.Now,
                    es_vigente = true
                };
                if (idsCategoria != null)
                {
                    //Agregamos la categoria
                    foreach (var id in idsCategoria)
                    {
                        concepto.Categoria_concepto.Add(new Categoria_concepto() { id_categoria = id });
                    }
                }
                if (idsCaracteristica != null)
                {
                    //Agregamos las caracteristicas
                    foreach (var id in idsCaracteristica)
                    {
                        concepto.Caracteristica_concepto.Add(new Caracteristica_concepto() { id_caracteristica = id, maximo = 1, es_multiple = false });
                    }
                }
                return _maestroRepositorio.crearDetalleMaestro(concepto);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al Intentar guardar concepto", e);
            }
        }

        public OperationResult ActualizarFamilia(int idFamilia, string nombre, string valor, int[] idsCategoria, int[] idsCaracteristica)
        {
            try
            {
                var seCambioNombreConcepto = false;
                OperationResult resultadoCrearInventario = new OperationResult();
                OperationResult resultadoEliminarInventario = new OperationResult();
                OperationResult resultActualizacionConceptos = new OperationResult();
                //Obtener el concepto basico, se tendra que actualizar
                var conceptoBasico = _maestroRepositorio.ObtenerDetalle(idFamilia);
                //Verificar que el nombre del concepto basico sea diferente al que se esta ingresando
                if (!conceptoBasico.nombre.Equals(nombre))
                {
                    seCambioNombreConcepto = true;
                    //Preguntar si existe el nombre del concepto basico a actualizar 
                    var detalleMaestro = _maestroRepositorio.ObtenerDetalleMaestroPorIdMaestroYNombre(MaestroSettings.Default.IdMaestroConcepto, nombre);
                    if (detalleMaestro != null)
                    {
                        //Lanzar la excepcion de que existe un concepto con el mismo nombre 
                        throw new LogicaException("Ya existe un concepto registrado con el nombre " + detalleMaestro.nombre + ", si el problema persiste comuniquese con el administrador.");
                    }
                }
                //Verificar que se esta cambiando el valor de bien a servicio 
                if (!conceptoBasico.valor.Equals(valor))
                {
                    //Verificar que el valor del concepto basico a actualizar sea un bien
                    if (conceptoBasico.valor.Equals("1"))
                    {
                        if (_transaccionRepositorio.ObtenerNumeroDetallesDeTransaccionConCantidadMayorA0(idFamilia, TransaccionSettings.Default.IdTipoTransaccionInventarioActual) > 0)
                        {
                            throw new LogicaException("No se puede cambiar a servicio debido a que tiene conceptos de negocio con stock mayor a 0");
                        }
                        resultadoEliminarInventario = _inventarioActualLogica.EliminarDetallesInventariosActualesDeConceptosDeNegocioDelConceptoBasico(idFamilia);
                        if (resultadoEliminarInventario.code_result == OperationResultEnum.Error)
                        {
                            throw new LogicaException("No se puede eliminar los detalles de inventario");
                        }
                    }
                    else
                    {
                        resultadoCrearInventario = _inventarioActualLogica.CrearDetallesInventariosActualesDeConceptosDeNegocioDelConceptoBasico(idFamilia);
                        if (resultadoCrearInventario.code_result == OperationResultEnum.Error)
                        {
                            throw new LogicaException("No se puede crear los detalles de inventario de los nuevos");
                        }
                    }
                }
                //Crear el concepto como detalle maestro
                Detalle_maestro concepto = new Detalle_maestro
                {
                    id = idFamilia,
                    id_maestro = MaestroSettings.Default.IdMaestroConcepto,
                    codigo = nombre.Length > 30 ? nombre.Substring(0, 30) : nombre,
                    nombre = nombre,
                    valor = valor,
                    es_vigente = true
                };
                //Agregar las categorias
                foreach (var id in idsCategoria)
                {
                    concepto.Categoria_concepto.Add(new Categoria_concepto() { id_concepto = idFamilia, id_categoria = id });
                }
                //Agregar las caracteristicas
                foreach (var id in idsCaracteristica)
                {
                    concepto.Caracteristica_concepto.Add(new Caracteristica_concepto() { id_concepto = idFamilia, id_caracteristica = id, maximo = 1, es_multiple = false });
                }
                var resultadoActualizacionConcepto = _conceptoRepositorio.ActualizarConcepto(concepto);
                if (seCambioNombreConcepto && resultadoActualizacionConcepto.code_result == OperationResultEnum.Success)
                {
                    List<Concepto_negocio> conceptosNegocioActualizar = new List<Concepto_negocio>();
                    var conceptosNegocio = _conceptoRepositorio.ObtenerConceptosDeNegocioPorRolYPorConceptoBasico(ConceptoSettings.Default.IdRolMercaderia, idFamilia).ToList();
                    var numeroCaracteresNombreConceptoBasico = conceptoBasico.nombre.Length;
                    foreach (var conceptoNegocio in conceptosNegocio)
                    {
                        conceptoNegocio.nombre = conceptoNegocio.nombre.Substring(numeroCaracteresNombreConceptoBasico, conceptoNegocio.nombre.Length - numeroCaracteresNombreConceptoBasico);
                        conceptoNegocio.nombre = concepto.nombre + conceptoNegocio.nombre;
                    }
                    resultActualizacionConceptos = _conceptoRepositorio.ActualizarNombreConceptosNegocio(conceptosNegocioActualizar);
                    if (resultActualizacionConceptos.code_result != OperationResultEnum.Success)
                    {
                        throw new LogicaException("Error al actualizar los nombres de los conceptos de negocio");
                    }
                }
                return resultActualizacionConceptos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        public OperationResult CambiarEsVigenteFamilia(int idFamilia, bool esVigente)
        {
            try
            {
                if (esVigente)
                {
                    //Buscamos si tiene conceptos de negocio en estado vigente
                    bool tieneConceptosNegocio = _conceptoRepositorio.TieneConceptosDeNegocio(idFamilia, true);
                    if (tieneConceptosNegocio)
                    {
                        throw new LogicaException("No se pudo dar de baja al concepto básico, porque esta asociado con un concepto de negocio");
                    }
                    return _maestroRepositorio.DarDeBajaDetalleMaestro(idFamilia, MaestroSettings.Default.IdMaestroConcepto);
                }
                else
                {
                    return _maestroRepositorio.DarDeAltaDetalleMaestro(idFamilia, MaestroSettings.Default.IdMaestroConcepto);
                }
            }
            catch (Exception e)
            {
                throw new LogicaException("Error el dar de baja concepto básico", e);
            }
        }


        public Detalle_maestro ObtenerConceptoBasicoVigente(int idConcepto)
        {
            try
            {
                Detalle_maestro concepto = _maestroRepositorio.ObtenerDetalle(idConcepto, true);
                return concepto;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Detalle_maestro> ObtenerConceptosBasicos()
        {
            try
            {
                return _maestroRepositorio.obtenerDetallesMaestros(MaestroSettings.Default.IdMaestroConcepto).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Detalle_maestro ObtenerConceptoBasicoVigenteIncluyendonCaracteristicasYValoresCaracteristicas(int idConcepto)
        {
            try
            {
                Detalle_maestro concepto = _maestroRepositorio.ObtenerDetalleMaestroInclusiveCaracteristicaConceptoConDetalleMaestroYValorCaracteristica(idConcepto, true);
                return concepto;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Detalle_maestro> ObtenerConceptosBasicosVigentesIncluyendoCategoriaConcepto()
        {
            try
            {
                return _maestroRepositorio.ObtenerDetallesMaestrosIncluyendoCategoriaConcepto(MaestroSettings.Default.IdMaestroConcepto, true).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intertar obtener conceptos incluyendo categoria concepto", e);
            }
        }
        public List<Detalle_maestro> ObtenerTodosLasFamilia()
        {
            try
            {
                return _maestroRepositorio.ObtenerTodoLosDetallesMaestrosIncluyendoCategoriaConcepto(MaestroSettings.Default.IdMaestroConcepto).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intertar obtener conceptos incluyendo categoria concepto", e);
            }
        }
        #endregion


        #region CARACTERISTICAS

        public List<ItemGenerico> ObtenerCaracteristicasComunes()
        {
            try
            {
                int[] idsMaestrosCaracteristicas = { MaestroSettings.Default.IdMaestroCaracteristicaConcepto };
                return ItemGenerico.Convert(_maestroRepositorio.ObtenerDetalles(idsMaestrosCaracteristicas, true).ToList());
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener caracteristicas", e);
            }
        }
        public List<Detalle_maestro> ObtenerCaracteristicas()
        {
            try
            {
                int[] idsMaestrosCaracteristicas = { MaestroSettings.Default.IdMaestroCaracteristicaConcepto, MaestroSettings.Default.IdMaestroCaracteristicaPropiaConcepto };
                return _maestroRepositorio.ObtenerDetalles(idsMaestrosCaracteristicas, true).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener caracteristicas", e);
            }
        }

        public Detalle_maestro ObtenerCaracteristica(int idCaracteristica)
        {
            try
            {
                return _maestroRepositorio.ObtenerDetalle(idCaracteristica);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<ItemGenerico> ObtenerCaracteristicasVigentesPorFamilia(int idFamilia)
        {
            try
            {
                return ItemGenerico.Convert(_maestroRepositorio.ObtenerDetallesPorFamilia(idFamilia).ToList());
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener caracteristicas", e);
            }
        }

        public OperationResult GuardarCarcateristica(int idMaestroCaracteristica, string codigo, string nombre, string descripcion, List<Valor_caracteristica> valores)
        {
            try
            {
                Detalle_maestro caracteristica = new Detalle_maestro();
                caracteristica.id_maestro = idMaestroCaracteristica;
                caracteristica.codigo = codigo;
                caracteristica.nombre = nombre;
                caracteristica.valor = descripcion;
                caracteristica.fecha_registro = DateTime.Now;
                caracteristica.es_vigente = true;
                if (valores.Count > 0)
                {
                    foreach (var item in valores)
                    {
                        caracteristica.Valor_caracteristica.Add(item);
                    }
                }
                return _maestroRepositorio.crearDetalleMaestro(caracteristica);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public OperationResult ActualizarCaracteristica(int id, int idMaestroCaracteristica, string codigo, string nombre, string descripcion, List<Valor_caracteristica> valores, bool esVigente)
        {
            try
            {
                Detalle_maestro caracteristica = new Detalle_maestro();
                caracteristica.id = id;
                caracteristica.id_maestro = idMaestroCaracteristica;
                caracteristica.codigo = codigo;
                caracteristica.nombre = nombre;
                caracteristica.valor = descripcion;
                caracteristica.es_vigente = esVigente;
                if (valores.Count > 0)
                {
                    foreach (var item in valores)
                    {
                        caracteristica.Valor_caracteristica.Add(item);
                    }
                }
                return _conceptoRepositorio.ActualizarCaracteristica(caracteristica);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool ExisteCaracteristicaEnConceptosVigentes(int idCaracteristica)
        {
            try
            {
                var resultado = _conceptoRepositorio.ConceptosNegocioVigentesConCaracteristica(idCaracteristica).ToList();
                return resultado.Count() > 0;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool ExisteCaracteristicaEnFamiliasVigentes(int idCaracteristica)
        {
            try
            {
                var resultado = _conceptoRepositorio.FamiliasVigentesConCaracteristica(idCaracteristica).ToList();
                return resultado.Count() > 0;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Detalle_maestro> ObtenerCaracteristicasIncluyendoValores()
        {
            try { return _maestroRepositorio.ObtenerDetallesInclusiveValorCaracteristica(new int[] { MaestroSettings.Default.IdMaestroCaracteristicaConcepto, MaestroSettings.Default.IdMaestroCaracteristicaPropiaConcepto }).ToList(); }
            catch (Exception e) { throw e; }
        }

        public List<Detalle_maestro> ObtenerCaracteristicasVigentesIncluyendoValores()
        {
            try { return _maestroRepositorio.ObtenerDetallesVigentesInclusiveValorCaracteristica(new int[] { MaestroSettings.Default.IdMaestroCaracteristicaConcepto, MaestroSettings.Default.IdMaestroCaracteristicaPropiaConcepto }).ToList(); }
            catch (Exception e) { throw e; }
        }

        #endregion


        #region VALOR CARACTERISTICA

        public OperationResult GuardarValorCarcateristica(int idCaracteristica, string valorCaracteristica)
        {
            try
            {
                Valor_caracteristica valor = new Valor_caracteristica();
                valor.id_caracteristica = idCaracteristica;
                valor.valor = valorCaracteristica;

                return _conceptoRepositorio.GuardarValorCaracteristica(valor);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public OperationResult ActualizarValorCaracteristica(int idValorCaracteristica, int idCaracteristica, string valorCaracteristica)
        {
            try
            {
                Valor_caracteristica valor = new Valor_caracteristica();
                valor.id_caracteristica = idCaracteristica;
                valor.valor = valorCaracteristica;
                valor.id = idValorCaracteristica;

                return _conceptoRepositorio.ActualizarValorCaracteristica(valor);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Valor_caracteristica ObtenerValorCaracteristica(int idValorCaracteristica)
        {
            try
            {
                return _conceptoRepositorio.ObtenerValorCaracteristica(idValorCaracteristica);
            }
            catch (Exception e)
            {
                throw new DatosException("No se pudo obtener  valor caracteristica", e);
            }
        }

        public List<Valor_caracteristica> ObtenerValoresDeCaracteristica(int idCaracteristica)
        {
            try
            {
                return _conceptoRepositorio.ObtenerValoresDeCaracteristica(idCaracteristica).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion


        public OperationResult guardarProducto(string codigo, string nombre, string propiedades)
        {
            try
            {
                Concepto_negocio nuevoProducto = new Concepto_negocio();

                nuevoProducto.codigo = codigo;
                nuevoProducto.nombre = nombre;
                nuevoProducto.propiedades = propiedades;

                return _conceptoRepositorio.CrearConceptoDeNegocio(nuevoProducto);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ConceptoDeNegocio obtenerProducto(int id)
        {
            try
            {
                ConceptoDeNegocio producto = new ConceptoDeNegocio(_conceptoRepositorio.ObtenerConceptoNegocioIncluyendoValorCaracteristicaConceptoNegocioYDetalleMaestroYCaracteristicaConcepto(id));
                return producto;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Stock> ObtenerExistencias(int idAlmacen)
        {
            try
            {
                return Stock.Convert_(_conceptoRepositorio.ObtenerExistenciasIncluyendoConceptoNegocioYActorNegocio(idAlmacen).ToList());
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public List<ConceptoDeNegocio> ObtenerMercaderiasIncluyendoExistencias()
        {
            try
            {
                return ConceptoDeNegocio.Convert_(_conceptoRepositorio.ObtenerConceptosDeNegocioPorRolIncluyendoExistencia(ConceptoSettings.Default.IdRolMercaderia).ToList());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Reporte_Stock_General> ObtenerReporteStockGeneral(int[] idsEntidadInterna)
        {
            try
            {
                return _conceptoRepositorio.ObtenerReporteStockGeneral(idsEntidadInterna, TransaccionSettings.Default.IdTipoTransaccionInventarioActual, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado)
                                          .ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        public List<ConceptoDeNegocio> ObtenerMercaderiasIncluyendoStockYPrecios()
        {
            try
            {
                return ConceptoDeNegocio.Convert_(_conceptoRepositorio.ObtenerConceptosDeNegocioPorRolIncluyendoExistenciasYPrecios(ConceptoSettings.Default.IdRolMercaderia).ToList());
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public List<ConceptoDeNegocio> obtenerMercaderiasConPrecio()
        {
            try
            {
                List<ConceptoDeNegocio> productos = ConceptoDeNegocio.Convert_(_conceptoRepositorio.ObtenerConceptosDeNegocioPorRolIncluyendoExistenciasYPreciosConUnPrecioVigenteComoMinimo(ConceptoSettings.Default.IdRolMercaderia).ToList());
                return productos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ConceptoDeNegocio ObtenerMercaderia(int idMercaderia)
        {
            try
            {
                ConceptoDeNegocio producto = new ConceptoDeNegocio(_conceptoRepositorio.ObtenerConceptoDeNegocioPorRolYIdConceptoNegocio(ConceptoSettings.Default.IdRolMercaderia, idMercaderia));
                return producto;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ConceptoDeNegocio ObtenerConcepto(int idConcepto)
        {
            try
            {
                ConceptoDeNegocio concepto = new ConceptoDeNegocio(_conceptoRepositorio.ObtenerConceptoDeNegocioPorIdConceptoNegocio(idConcepto));
                return concepto;
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        public List<ConceptoDeNegocio> obtenerMercaderias()//EN PROCESO X0
        {
            try
            {
                List<ConceptoDeNegocio> productos = ConceptoDeNegocio.Convert_(_conceptoRepositorio.ObtenerConceptosDeNegocioPorRol(ConceptoSettings.Default.IdRolMercaderia).ToList());
                return productos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<ConceptoDeNegocio> ObtenerMercaderiasPorConceptoBasico(int idConceptoBasico)
        {
            try
            {
                var resultados = _conceptoRepositorio.ObtenerConceptosDeNegocioPorRolYPorConceptoBasico(ConceptoSettings.Default.IdRolMercaderia, idConceptoBasico).ToList();
                List<ConceptoDeNegocio> productos = ConceptoDeNegocio.Convert_(resultados);
                return productos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ConceptoDeNegocio ObtenerConceptoDeNegocioPorNombre(string nombre)
        {
            try
            {
                var result = _conceptoRepositorio.ObtenerConceptoNegocioPorNombre(nombre);
                return result != null ? new ConceptoDeNegocio(result) : null;

            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener concepto de negocio por nombre", e);
            }
        }

        public List<Precio_Compra_Venta_Concepto> ObtenerPreciosCompraVentaDeConceptoNegocio(int idConceptoDeNegocio)
        {
            try
            {
                List<Precio_Compra_Venta_Concepto> resultado = _conceptoRepositorio.ObtenerPreciosCompraVentaDeConceptoNegocio(idConceptoDeNegocio).ToList();
                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public decimal ObtenerCostoUnitarioDelIcbperALaFecha()
        {
            try
            {
                DateTime fechaActual = DateTimeUtil.FechaActual();
                return _conceptoRepositorio.ObtenerPrecioPublicoDelIcbper(fechaActual);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener el costo unitario del icbper en la fecha", e);
            }
        }



        public List<ConceptoDeNegocio> ObtenerMercaderiasPorCentroAtencionIncluyendoStockYPrecios(int idCentroAtencionQueTieneLosPrecios)
        {
            try
            {
                List<ConceptoDeNegocio> productos = ConceptoDeNegocio.Convert_(_conceptoRepositorio.ObtenerConceptosDeNegocioPorRolIncluyendoExistenciasYPreciosConUnPrecioVigenteComoMinimoParaActorNegocio(idCentroAtencionQueTieneLosPrecios, ConceptoSettings.Default.IdRolMercaderia).ToList());
                return productos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [Obsolete("Metodo cambiado por el manejo de clases personalizados")]
        public List<ConceptoDeNegocio> ObtenerMercaderiasIncluyendoStockYPrecios(int idConceptoBasico, int idCentroAtencionQueTieneLosPrecios)
        {
            try
            {

                return ConceptoDeNegocio.Convert_(_conceptoRepositorio.ObtenerConceptosDeNegocioPorRolIncluyendoExistenciasYPreciosConUnPrecioVigenteComoMinimoPorActorNegocio(
                    idCentroAtencionQueTieneLosPrecios, ConceptoSettings.Default.IdRolMercaderia, idConceptoBasico).ToList());
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener mercaderia", e);
            }
        }

        public List<ConceptoDeNegocio> ObtenerMercaderiasPorConceptoBasicoIncluyendoStockYPrecios(int idConceptoBasico)
        {
            try
            {
                return ConceptoDeNegocio.Convert_(_conceptoRepositorio.ObtenerConceptosDeNegocioPorRolIncluyendoExistenciasYPrecios(ConceptoSettings.Default.IdRolMercaderia, idConceptoBasico).ToList());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<ConceptoDeNegocio> ObtenerMercaderiasPorConceptoBasicoIncluyendoStockPreciosYCaracteristicas(int idConceptoBasico)
        {
            try
            {
                return ConceptoDeNegocio.Convert_(_conceptoRepositorio.ObtenerConceptosDeNegocioPorRolIncluyendoExistenciasPreciosYDetallesDeMaestro(ConceptoSettings.Default.IdRolMercaderia, idConceptoBasico).ToList());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<ConceptoDeNegocio> ObtenerMercaderiasPorConceptoBasicoYCaracteristicasIncluyendoStockPreciosYCaracteristicas(int idConceptoBasico, int[] idsValoresDeCaracteristicas)
        {
            try
            {
                return ConceptoDeNegocio.Convert_(_conceptoRepositorio.ObtenerConceptosDeNegocioPorRolIncluyendoExistenciasPreciosYDetallesDeMaestro(ConceptoSettings.Default.IdRolMercaderia, idConceptoBasico, idsValoresDeCaracteristicas).ToList());
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener producto por concepto basico y caracteristicas", e);
            }
        }

        public List<ConceptoDeNegocio> ObtenerMercaderiasPorCaracteristicasIncluyendoStockPreciosYCaracteristicas(int[] idsValoresDeCaracteristicas)
        {
            try
            {
                return ConceptoDeNegocio.Convert_(_conceptoRepositorio.ObtenerConceptosDeNegocioPorRolIncluyendoExistenciasPreciosYDetallesDeMaestro(ConceptoSettings.Default.IdRolMercaderia, idsValoresDeCaracteristicas).ToList());
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener productos por caracteristicas", e);
            }
        }

        [Obsolete("Metodo cambiado por una clase personalizada")]
        public ConceptoDeNegocio ObtenerMercaderiaPorCodigoBarraIncluyendoStockYPreciosConUnPrecioComoMinimo(int idCentroAtencion, string codigoBarra)
        {
            try
            {
                int idCentroAtencionQueTieneLosPrecios = idCentroAtencion;
                var resultado = _conceptoRepositorio.ObtenerConceptoDeNegocioPorCodigoBarraIncluyendoExistenciasYPreciosConUnPrecioVigenteComoMinimoParaActorNegocio(idCentroAtencionQueTieneLosPrecios, ConceptoSettings.Default.IdRolMercaderia, codigoBarra);
                return resultado != null ? new ConceptoDeNegocio(resultado) : null;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener producto por codigo de barra", e);
            }
        }

        public ConceptoDeNegocio ObtenerMercaderiaPorCodigoBarraIncluyendoStockYPrecios(string codigoBarra)
        {
            try
            {
                var resultado = _conceptoRepositorio.ObtenerConceptosDeNegocioPorRolYCodigoBarra(ConceptoSettings.Default.IdRolMercaderia, codigoBarra);
                if (resultado == null)
                {
                    throw new AdvertenciaException("Codigo de barra no existe.");
                }
                return new ConceptoDeNegocio(resultado);
            }
            catch (AdvertenciaException e)
            {
                throw new AdvertenciaException(e.Message);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener producto por código de barra", e);
            }
        }

        public List<ConceptoDeNegocio> obtenerSubContenidos(int IdModelo)
        {
            try
            {
                return null;// Producto.Convert_(_conceptoRepositorio.obtenerConceptosDeNegocioPorModelo(IdModelo).ToList());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public OperationResult GuardarProducto(string codigoBarra, string nombre, string codigo, string codigoDigemid, string sufijo, int idConceptoBasico, bool esBien, int idUnidadDeMedidaCom, int idUnidadDeMedidaRef, int[] idCaracteristicas, int[] modulosAdicionales, int idPresentacion, decimal cantidadPresentacion, int idUnidadDeMedidaPre, int? idPresentacionSubContenido, byte[] foto, bool hayFoto, List<Precio_Compra_Venta_Concepto> precios, decimal? stockMinimo, int idEmpleado, int idCentroAtencion)
        {
            try
            {
                int idRol = ConceptoSettings.Default.IdRolMercaderia;
                if (_conceptoRepositorio.ExisteCodigoBarraEnConceptoVigente(idRol, codigoBarra))
                {
                    throw new AdvertenciaException("Codigo de barra existe en un concepto vigente.");
                }
                codigoDigemid = (string.IsNullOrEmpty(codigoDigemid) || string.IsNullOrWhiteSpace(codigoDigemid)) ? "" : codigoDigemid;
                codigo = (string.IsNullOrEmpty(codigo) || string.IsNullOrWhiteSpace(codigo)) ? idConceptoBasico + "-" + ObtenerSiguienteCodigoParaMercaderia(idRol, idConceptoBasico) : codigo;
                //creammos el producto
                Concepto_negocio producto = new Concepto_negocio(codigo, codigoBarra, codigoDigemid, nombre, sufijo, "", idConceptoBasico, idUnidadDeMedidaCom, idPresentacion, cantidadPresentacion, idUnidadDeMedidaPre, idPresentacionSubContenido, null, true, idUnidadDeMedidaRef);
                producto.id = 0;
                producto.Concepto_negocio_rol.Add(new Concepto_negocio_rol() { id_rol = idRol });
                //Agregar rol a los concpetos con modulos adicionales
                if (modulosAdicionales != null)
                {
                    foreach (var moduloAdicional in modulosAdicionales)
                    {
                        producto.Concepto_negocio_rol.Add(new Concepto_negocio_rol() { id_rol = Diccionario.MapeoModuloVsRolNegocio.Single(m => m.Key == moduloAdicional).Value });
                    }
                }
                ResolverFotografia(hayFoto, producto, foto);
                AgregarCaracteristicas(idCaracteristicas, producto);
                DateTime fechaActual = DateTimeUtil.FechaActual();
                //agregamos el stock minimo como parametro
                producto.stock_minimo = stockMinimo != null ? (decimal)stockMinimo : 0;

                producto.Precio = GenerarPrecios(precios, fechaActual, 0, idEmpleado);
                //REGISTRAR EL CONCEPTO EN INVENTARIO FISICO DE CADA ALMACEN
                RegistrarConceptosEnInventario(esBien, producto);
                return _conceptoRepositorio.CrearConceptoDeNegocio(producto);
            }
            catch (AdvertenciaException e)
            {
                throw new AdvertenciaException(e.Message);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al guardar producto", e);
            }
        }

        public OperationResult GuardarConceptoServicio(string nombre, int idConceptoBasico, string sufijo, int idEmpleado, int idCentroAtencion)
        {
            try
            {
                if (ExisteNombreConceptoComercial(nombre))
                {
                    throw new ControllerException("Ya existe un concepto servicio registrado con el mismo nombre.");
                }
                DateTime fechaActual = DateTimeUtil.FechaActual();
                int idRol = ConceptoSettings.Default.IdRolMercaderia;
                string codigo = idConceptoBasico + "-" + ObtenerSiguienteCodigoParaMercaderia(idRol, idConceptoBasico);
                //Creamos el concepto de gasto
                Concepto_negocio conceptoServicio = new Concepto_negocio(codigo, null, "", nombre, sufijo, "", idConceptoBasico, ConceptoSettings.Default.idUnidadMedidaPorDefecto, ConceptoSettings.Default.idPresentacionPorDefecto, 1, ConceptoSettings.Default.idUnidadMedidaPorDefecto, null, null, true, ConceptoSettings.Default.idUnidadMedidaPorDefecto);

                //Agregamos el rol gasto
                conceptoServicio.Concepto_negocio_rol.Add(new Concepto_negocio_rol() { id_rol = idRol });

                //Creamos el precio para el producto si se ingreso
                Precio precio = new Precio(idCentroAtencion, MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal, 1, MaestroSettings.Default.IdDetalleMaestroTarifaNormal, MaestroSettings.Default.IdDetalleMaestroMonedaSoles, fechaActual, fechaActual.AddMonths(ConceptoSettings.Default.PrecioDuracionPorDefectoEnMeses), fechaActual, true, true, false, 1, 0, MaestroSettings.Default.IdDetalleMaestroTipoPrecioPrecio, "Creado al crear el concepto", idEmpleado);

                conceptoServicio.Precio.Add(precio);
                return _conceptoRepositorio.CrearConceptoDeNegocio(conceptoServicio);
            }
            catch (Exception e)
            {

                throw new LogicaException("Error al guardar concepto de gasto", e);
            }
        }

        public OperationResult DarDeBajaMercaderia(int idMercaderia)
        {
            try
            {
                if (_conceptoRepositorio.TieneStockConceptoNegocio(idMercaderia))
                {
                    throw new LogicaException("El concepto de negocio no se puede dar de baja porque tiene stock.");
                }
                return _conceptoRepositorio.DarDeBajaConceptoNegocio(idMercaderia);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error el dar de baja mercadería", e);
            }
        }

        public string ObtenerSiguienteCodigoParaMercaderia(int idRol, int idConceptoBasico)
        {

            try
            {
                int maximo = _conceptoRepositorio.ObtenerMaximoCodigoParaConceptoNegocio(idConceptoBasico + "-", idRol, idConceptoBasico);
                return (maximo + 1).ToString();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener siguiente código para producto", e);
            }
        }

        public void ResolverFotografia(bool hayFoto, Concepto_negocio concepto, byte[] foto)
        {
            if (hayFoto)//agragamos la foto al producto si hay
            {
                Foto fotografia = new Foto();
                fotografia.imagen = foto;
                //asociamos la foto al producto
                concepto.Foto = fotografia;
            }
        }

        /// <summary>
        /// Registra el concepto en el inventario fisico de cada centro de atención con rol almacen
        /// </summary>
        /// <param name="esBien"></param>
        /// <param name="concepto"></param>
        private void RegistrarConceptosEnInventario(bool esBien, Concepto_negocio concepto)
        {
            if (esBien)
            {
                var inventariosFisicos = _transaccionRepositorio.ObtenerTransacciones(TransaccionSettings.Default.IdTipoTransaccionInventarioActual, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado);
                //agregamos un detalle transaccion para el nuevo producto en todos los inventario fisicos
                foreach (var inventarioFisico in inventariosFisicos)
                {
                    //  producto.Detalle_transaccion.Add(new Detalle_transaccion(inventarioFisico.id,0, 0, "Inventario Físico", 0m, 0m, null, 0,0, null, 0m, 0m, 0m));
                    concepto.Detalle_transaccion.Add(new Detalle_transaccion()
                    {
                        id_transaccion = inventarioFisico.id,
                        cantidad = 0,
                        id_concepto_negocio = 0,
                        detalle = "Inventario Físico",
                        precio_unitario = 0m,
                        total = 0m,
                        id_precio = null,
                        cantidad_secundaria = 0,
                        indicadorMultiproposito = 0,
                        id_cuenta_contable = null,
                        igv = 0m,
                        isc = 0m,
                        descuento = 0m
                    });
                }
            }
        }

        public OperationResult ActualizarProducto(int id, string codigoBarra, string nombre, string codigo, string codigoDigemid, string sufijo, int idConceptoBasico, bool esBien, int idUnidadDeMedidaCom, int idUnidadDeMedidaRef, int[] idCaracteristicas, int[] modulosAdicionales, int idPresentacion, decimal cantidadPresentacion, int idUnidadDeMedidaPre, int? idPresentacionSubContenido, byte[] foto, bool hayFoto, List<Precio_Compra_Venta_Concepto> precios, decimal? stockMinimo, int idEmpleado, int idCentroAtencion)
        {
            try
            {
                int idRol = ConceptoSettings.Default.IdRolMercaderia;
                if (_conceptoRepositorio.ExisteCodigoBarraEnConceptoVigenteAlActuaizar(id, idRol, codigoBarra))
                {
                    throw new AdvertenciaException("Codigo de barra existe en un concepto vigente.");
                }
                codigoDigemid = (string.IsNullOrEmpty(codigoDigemid) || string.IsNullOrWhiteSpace(codigoDigemid)) ? "" : codigoDigemid;
                codigo = (string.IsNullOrEmpty(codigo) || string.IsNullOrWhiteSpace(codigo)) ? idConceptoBasico + "-" + ObtenerSiguienteCodigoParaMercaderia(idRol, idConceptoBasico) : codigo;
                //creammos el producto
                Concepto_negocio producto = new Concepto_negocio(id, codigo, codigoBarra, codigoDigemid, nombre, sufijo, "", idConceptoBasico, idUnidadDeMedidaCom, idPresentacion, cantidadPresentacion, idUnidadDeMedidaPre, idPresentacionSubContenido, null, true, idUnidadDeMedidaRef);
                producto.Concepto_negocio_rol.Add(new Concepto_negocio_rol() { id_concepto_negocio = id, id_rol = idRol });
                //Agregar rol a los concpetos con modulos adicionales
                if (modulosAdicionales != null)
                {
                    foreach (var moduloAdicional in modulosAdicionales)
                    {
                        producto.Concepto_negocio_rol.Add(new Concepto_negocio_rol() { id_concepto_negocio = id, id_rol = Diccionario.MapeoModuloVsRolNegocio.Single(m => m.Key == moduloAdicional).Value });
                    }
                }
                ResolverFotografia(hayFoto, producto, foto);
                AgregarCaracteristicas(idCaracteristicas, producto);
                DateTime fechaActual = DateTimeUtil.FechaActual();

                producto.stock_minimo = (decimal)(esBien && stockMinimo != null ? stockMinimo : 0);

                //creamos los precios para el producto si existen
                //List<Precio> nuevosPrecios = GenerarPrecios(precios, fechaActual, producto.id, idEmpleado);
                //actualizar concepto de negocio
                OperationResult resultActualizacionConceptoNegocio = _conceptoRepositorio.ActualizarConceptoNegocio(producto);
                OperationResult resultActualizacionPrecios = null;
                if (resultActualizacionConceptoNegocio.code_result == OperationResultEnum.Success)
                {
                    List<Precio> nuevosPrecios = new List<Precio>();
                    List<Precio> preciosCaducos = new List<Precio>();
                    foreach (var item in precios)
                    {
                        if (item.Seleccionado)
                        {
                            Precio precio = new Precio(item.IdPrecio, item.IdPuntoPrecio, MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal, id, item.Valor, item.IdTarifa, MaestroSettings.Default.IdDetalleMaestroMonedaSoles, item.FechaInicio, item.FechaFin, fechaActual, true, true, false, 1, 0, MaestroSettings.Default.IdDetalleMaestroTipoPrecioPrecio, item.Descripcion, idEmpleado);
                            nuevosPrecios.Add(precio);
                        }
                        else
                        {
                            if (item.IdPrecio != 0)
                            {
                                Precio precio = _precioRepositorio.obtenerPrecio(item.IdPrecio);
                                precio.fecha_fin = fechaActual;
                                precio.fecha_modificacion = fechaActual;
                                precio.es_vigente = false;
                                preciosCaducos.Add(precio);
                            }
                        }
                    }
                    resultActualizacionPrecios = _precioRepositorio.ResolverPrecios(nuevosPrecios, preciosCaducos);
                    if (resultActualizacionPrecios.code_result == OperationResultEnum.Success)
                    {
                        return new OperationResult(OperationResultEnum.Success);
                    }
                    else
                    {
                        return new OperationResult(OperationResultEnum.Warning, "La Mercaderia se guardo correctamente, pero no se actualizaron los precios. " + resultActualizacionPrecios.message);
                    }
                }
                else
                {
                    return resultActualizacionConceptoNegocio;
                }
            }
            catch (AdvertenciaException e)
            {
                throw new AdvertenciaException(e.Message);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al actualizar producto", e);
            }
        }

        public List<Precio> GenerarPrecios(List<Precio_Compra_Venta_Concepto> precios, DateTime fecha, int idProducto, int idEmpleado)
        {
            List<Precio> preciosGenerados = new List<Precio>();
            foreach (var precioVenta in precios)
            {
                if (precioVenta.Valor >= 0 && precioVenta.Valor != AplicacionSettings.Default.ValorPrecioVentaPorDefectoQueNoSeDebeGuardar)
                {
                    Precio precio = new Precio(precioVenta.IdPuntoPrecio, MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal, idProducto, precioVenta.Valor, precioVenta.IdTarifa, MaestroSettings.Default.IdDetalleMaestroMonedaSoles, fecha, fecha.AddMonths(ConceptoSettings.Default.PrecioDuracionPorDefectoEnMeses), fecha, true, true, false, 1, 0, MaestroSettings.Default.IdDetalleMaestroTipoPrecioPrecio, precioVenta.Descripcion, idEmpleado);
                    preciosGenerados.Add(precio);
                }
            }
            return preciosGenerados;
        }

        public void AgregarCaracteristicas(int[] idsCaracteristicas, Concepto_negocio producto)
        {
            if (idsCaracteristicas != null)
            {
                foreach (var item in idsCaracteristicas.Where(ic => ic != 0))
                {
                    Valor_caracteristica_concepto_negocio valorCaracteristica = new Valor_caracteristica_concepto_negocio();
                    valorCaracteristica.id_valor_caracteristica = item;
                    valorCaracteristica.id_concepto_negocio = producto.id;
                    producto.Valor_caracteristica_concepto_negocio.Add(valorCaracteristica);
                }
            }
        }

        #region CONCEPTO DE GASTO

        public List<Detalle_maestro> ObtenerConceptosBasicosDeGasto()
        {
            try
            {
                int IdRol = ConceptoSettings.Default.IdRolGasto;
                return _conceptoRepositorio.ObtenerDetalleMaestro4DeConceptoNegocioConRol(IdRol).Distinct().ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener el concepto basico de gasto", e);
            }
        }

        public List<DetalleGenerico> ObtenerConceptosVigentesGasto()
        {
            try
            {
                var result = _conceptoRepositorio.ObtenerConceptosDeNegocioPorRolesIncluyendoExistenciasYPrecios(ConceptoSettings.Default.IdRolGasto, true).ToList();
                List<DetalleGenerico> resultado = new List<DetalleGenerico>();
                foreach (var item in result)
                {
                    resultado.Add(new DetalleGenerico(item.id, item.nombre));
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener los conceptos de gasto", e);
            }
        }

        public OperationResult GuardarConceptoGasto(string nombre, int idConceptoBasico, string sufijo, int idEmpleado, int idCentroAtencion)
        {
            try
            {
                if (ExisteNombreConceptoComercial(nombre))
                {
                    throw new ControllerException("Ya existe un concepto de gasto registrado con el mismo nombre.");
                }
                DateTime fechaActual = DateTimeUtil.FechaActual();
                int idRol = ConceptoSettings.Default.IdRolGasto;
                string codigo = idConceptoBasico + "-" + ObtenerSiguienteCodigoParaMercaderia(idRol, idConceptoBasico);
                //Creamos el concepto de gasto
                Concepto_negocio conceptoGasto = new Concepto_negocio(codigo, null, "", nombre, sufijo, "", idConceptoBasico, ConceptoSettings.Default.idUnidadMedidaPorDefecto, ConceptoSettings.Default.idPresentacionPorDefecto, 1, ConceptoSettings.Default.idUnidadMedidaPorDefecto, null, null, true, ConceptoSettings.Default.idUnidadMedidaPorDefecto);

                //Agregamos el rol gasto
                conceptoGasto.Concepto_negocio_rol.Add(new Concepto_negocio_rol() { id_rol = idRol });

                //Creamos el precio para el producto si se ingreso
                Precio precio = new Precio(idCentroAtencion, MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal, 1, MaestroSettings.Default.IdDetalleMaestroTarifaNormal, MaestroSettings.Default.IdDetalleMaestroMonedaSoles, fechaActual, fechaActual.AddMonths(ConceptoSettings.Default.PrecioDuracionPorDefectoEnMeses), fechaActual, true, true, false, 1, 0, MaestroSettings.Default.IdDetalleMaestroTipoPrecioPrecio, "Creado al crear el concepto", idEmpleado);

                conceptoGasto.Precio.Add(precio);
                return _conceptoRepositorio.CrearConceptoDeNegocio(conceptoGasto);
            }
            catch (Exception e)
            {

                throw new LogicaException("Error al guardar concepto de gasto", e);
            }
        }

        public OperationResult ActualizarConceptoGasto(int id, string nombre, int idConceptoBasico, string sufijo, int idEmpleado, int idCentroAtencion)
        {
            try
            {
                //Si existe un registro con el mismo nombre se mostrara un mensaje
                if (ExisteNombreConceptoComercial(nombre))
                {
                    throw new ControllerException("Ya existe un concepto de gasto registrado con el mismo nombre.");
                }
                DateTime fechaActual = DateTimeUtil.FechaActual();
                int idRol = ConceptoSettings.Default.IdRolGasto;
                string codigo = idConceptoBasico + "-" + ObtenerSiguienteCodigoParaMercaderia(idRol, idConceptoBasico);
                //Creamos el concepto de gasto
                Concepto_negocio conceptoGasto = new Concepto_negocio(id, codigo, null, "", nombre, sufijo, "", idConceptoBasico, ConceptoSettings.Default.idUnidadMedidaPorDefecto, ConceptoSettings.Default.idPresentacionPorDefecto, 1, ConceptoSettings.Default.idUnidadMedidaPorDefecto, null, null, true, ConceptoSettings.Default.idUnidadMedidaPorDefecto);

                //Agregamos el rol gasto
                conceptoGasto.Concepto_negocio_rol.Add(new Concepto_negocio_rol() { id_rol = idRol });

                //Creamos el precio para el producto si se ingreso
                Precio precio = new Precio(idCentroAtencion, MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal, 1, MaestroSettings.Default.IdDetalleMaestroTarifaNormal, MaestroSettings.Default.IdDetalleMaestroMonedaSoles, fechaActual, fechaActual.AddMonths(ConceptoSettings.Default.PrecioDuracionPorDefectoEnMeses), fechaActual, true, true, false, 1, 0, MaestroSettings.Default.IdDetalleMaestroTipoPrecioPrecio, "Creado al crear el concepto", idEmpleado);

                conceptoGasto.Precio.Add(precio);
                return _conceptoRepositorio.ActualizarConceptoNegocio(conceptoGasto);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al actualizar concepto de gasto", e);
            }
        }

        public List<RolDeNegocio> ObtenerRolesQueAplicaAConceptosDeNegocioExceptoMercaderiaYServicios()
        {
            try
            {
                int[] idRolesAExcluir = new int[] { ConceptoSettings.Default.IdRolMercaderia };
                return RolDeNegocio.Convert_(_conceptoRepositorio.ObtenerRolesDeConceptosExceptoMercaderiaYServicios(ConceptoSettings.Default.RolAplicaAConceptoNegocio, idRolesAExcluir).ToList());

            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener roles", e);
            }

        }

        #endregion


        #region CONCEPTOS NEGOCIOS COMERCIALES SIN STOCK Y PRECIOS

        public List<Concepto_Negocio_Comercial> ObtenerConceptosNegociosComerciales(int? idConceptoBasico, int? idCategoria, int[] idValoresCaracteristicas)
        {
            try
            {
                List<Concepto_Negocio_Comercial> resultado = null;
                if (idValoresCaracteristicas != null)
                {
                    idValoresCaracteristicas = idValoresCaracteristicas.Where(id => id > 0).ToArray();
                    if (idValoresCaracteristicas.Length > 0)
                    {
                        if (idConceptoBasico != null)
                        {
                            if (idCategoria != null)
                            {
                                resultado = ObtenerConceptosDeNegocioComercialesPorRolesYIdConceptoBasicoYIdCategoriaYIdsValoresCaracteristicas((int)idConceptoBasico, (int)idCategoria, idValoresCaracteristicas);
                                return resultado;
                            }
                            else
                            {
                                resultado = ObtenerConceptosDeNegocioComercialesPorRolesYIdConceptoBasicoYIdsValoresCaracteristicas((int)idConceptoBasico, idValoresCaracteristicas);
                                return resultado;
                            }
                        }
                        else
                        {
                            if (idCategoria != null)
                            {
                                resultado = ObtenerConceptosDeNegocioComercialesPorIdCategoriaYIdsValoresCaracteristicas((int)idCategoria, idValoresCaracteristicas);
                                return resultado;
                            }
                            else
                            {
                                resultado = ObtenerConceptosDeNegocioComercialesPorIdsValoresCaracteristicas(idValoresCaracteristicas);
                                return resultado;
                            }
                        }
                    }
                }
                else
                {
                    if (idConceptoBasico != null)
                    {
                        if (idCategoria != null)
                        {
                            resultado = ObtenerConceptosDeNegocioComercialesPorRolesYIdConceptoBasicoYIdCategoria((int)idConceptoBasico, (int)idCategoria);
                            return resultado;
                        }
                        else
                        {
                            resultado = ObtenerConceptosDeNegocioComercialesPorRolesYIdConceptoBasico((int)idConceptoBasico);
                            return resultado;
                        }
                    }
                    else
                    {
                        if (idCategoria != null)
                        {
                            resultado = ObtenerConceptosDeNegocioComercialesPorRolesYIdCategoria((int)idCategoria);
                            return resultado;
                        }
                        else
                        {
                            resultado = ObtenerConceptosDeNegocioComercialesPorRoles();
                            return resultado;
                        }
                    }
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener conceptos de negocios comerciales", e);
            }
        }

        public List<Concepto_Negocio_Comercial> ObtenerConceptosDeNegocioComercialesPorRolesYIdConceptoBasicoYIdCategoriaYIdsValoresCaracteristicas(int idConceptoBasico, int idCategoria, int[] idsValoresDeCaracteristicas)
        {
            try
            {
                List<Concepto_Negocio_Comercial> resultado = _conceptoRepositorio.ObtenerConceptosDeNegocioComercialesPorRolesYIdConceptoBasicoYIdCategoriaYIdsValoresCaracteristicas(new int[] { ConceptoSettings.Default.IdRolMercaderia }, idConceptoBasico, idCategoria, idsValoresDeCaracteristicas).ToList();
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener conceptos de negocios comerciales por concepto basico y categoria y caracteristicas", e);
            }
        }
        public List<Concepto_Negocio_Comercial> ObtenerConceptosDeNegocioComercialesPorRolesYIdConceptoBasicoYIdsValoresCaracteristicas(int idConceptoBasico, int[] idsValoresDeCaracteristicas)
        {
            try
            {
                List<Concepto_Negocio_Comercial> resultado = _conceptoRepositorio.ObtenerConceptosDeNegocioComercialesPorRolesYIdConceptoBasicoYIdsValoresCaracteristicas(new int[] { ConceptoSettings.Default.IdRolMercaderia }, idConceptoBasico, idsValoresDeCaracteristicas).ToList();
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener conceptos de negocios comerciales por concepto basico y caracteristicas", e);
            }
        }

        public List<Concepto_Negocio_Comercial> ObtenerConceptosDeNegocioComercialesPorIdCategoriaYIdsValoresCaracteristicas(int idCategoria, int[] idsValoresDeCaracteristicas)
        {
            try
            {
                List<Concepto_Negocio_Comercial> resultado = _conceptoRepositorio.ObtenerConceptosDeNegociosComercialesPorRolesYIdCategoriaYIdsValoresDeCaracteristicas(new int[] { ConceptoSettings.Default.IdRolMercaderia }, idCategoria, idsValoresDeCaracteristicas).ToList();
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener conceptos de negocios comerciales por categoria y características", e);
            }
        }
        public List<Concepto_Negocio_Comercial> ObtenerConceptosDeNegocioComercialesPorIdsValoresCaracteristicas(int[] idsValoresDeCaracteristicas)
        {
            try
            {
                List<Concepto_Negocio_Comercial> resultado = _conceptoRepositorio.ObtenerConceptosDeNegociosComercialesPorRolesYIdsValoresDeCaracteristicas(new int[] { ConceptoSettings.Default.IdRolMercaderia }, idsValoresDeCaracteristicas).ToList();
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener conceptos de negocios comerciales por características", e);
            }
        }
        public List<Concepto_Negocio_Comercial> ObtenerConceptosDeNegocioComercialesPorRolesYIdConceptoBasicoYIdCategoria(int idConceptoBasico, int idCategoria)
        {
            try
            {
                List<Concepto_Negocio_Comercial> resultado = _conceptoRepositorio.ObtenerConceptosDeNegocioComercialesPorRolesYIdConceptoBasicoYIdCategoria(new int[] { ConceptoSettings.Default.IdRolMercaderia }, idCategoria, idConceptoBasico).ToList();
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener conceptos de negocios comerciales por concepto basico y categoria", e);
            }
        }
        public List<Concepto_Negocio_Comercial> ObtenerConceptosDeNegocioComercialesPorRolesYIdConceptoBasico(int idConceptoBasico)
        {
            try
            {
                List<Concepto_Negocio_Comercial> resultado = _conceptoRepositorio.ObtenerConceptosDeNegocioComercialesPorRolesYIdConceptoBasico(new int[] { ConceptoSettings.Default.IdRolMercaderia }, idConceptoBasico).ToList();
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener conceptos de negocios comerciales por id concepto basico", e);
            }
        }

        public List<Concepto_Negocio_Comercial> ObtenerConceptosDeNegocioComercialesPorRolesYIdCategoria(int idCategoria)
        {
            try
            {
                List<Concepto_Negocio_Comercial> resultado = _conceptoRepositorio.ObtenerConceptosDeNegocioComercialesPorRolesYIdCategoria(new int[] { ConceptoSettings.Default.IdRolMercaderia }, idCategoria).ToList();
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener conceptos de negocios comerciales por id categoria", e);
            }
        }

        public List<Concepto_Negocio_Comercial> ObtenerConceptosDeNegocioComercialesPorRoles()
        {
            try
            {
                List<Concepto_Negocio_Comercial> resultado = _conceptoRepositorio.ObtenerConceptosDeNegocioComercialesPorRoles(new int[] { ConceptoSettings.Default.IdRolMercaderia }).ToList();
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener conceptos de negocios comerciales", e);
            }
        }

        #endregion

        #region CONCEPTOS NEGOCIOS COMERCIALES CON STOCK Y PRECIOS
        public List<Concepto_Negocio_Comercial> ObtenerConceptosNegociosComercialesConStockYPrecios(long idTransaccionInventarioFisico, int idActorNegocioInternoQueTieneLosPrecios, int? idConceptoBasico, int? idCategoria, int[] idValoresCaracteristicas)
        {
            try
            {
                List<Concepto_Negocio_Comercial> resultado = null;
                if (idValoresCaracteristicas != null)
                {
                    idValoresCaracteristicas = idValoresCaracteristicas.Where(id => id > 0).ToArray();
                    if (idValoresCaracteristicas.Length > 0)
                    {
                        if (idConceptoBasico != null)
                        {
                            if (idCategoria != null)
                            {
                                resultado = ObtenerConceptosDeNegocioComercialesPorRolesYIdConceptoBasicoYIdCategoriaYIdsValoresCaracteristicasIncluyendoStockYPrecios(idTransaccionInventarioFisico, idActorNegocioInternoQueTieneLosPrecios, (int)idConceptoBasico, (int)idCategoria, idValoresCaracteristicas);
                                return resultado;
                            }
                            else
                            {
                                resultado = ObtenerConceptosDeNegocioComercialesPorRolesYIdConceptoBasicoYIdsValoresCaracteristicasIncluyendoStockYPrecios(idTransaccionInventarioFisico, idActorNegocioInternoQueTieneLosPrecios, (int)idConceptoBasico, idValoresCaracteristicas);
                                return resultado;
                            }
                        }
                        else
                        {
                            if (idCategoria != null)
                            {
                                resultado = ObtenerConceptosDeNegocioComercialesPorIdCategoriaYIdsValoresCaracteristicasIncluyendoStockYPrecios(idTransaccionInventarioFisico, idActorNegocioInternoQueTieneLosPrecios, (int)idCategoria, idValoresCaracteristicas);
                                return resultado;
                            }
                            else
                            {
                                resultado = ObtenerConceptosDeNegocioComercialesPorIdsValoresCaracteristicasIncluyendoStockYPrecios(idTransaccionInventarioFisico, idActorNegocioInternoQueTieneLosPrecios, idValoresCaracteristicas); ;
                                return resultado;
                            }
                        }
                    }
                }
                else
                {
                    if (idConceptoBasico != null)
                    {
                        if (idCategoria != null)
                        {
                            resultado = ObtenerConceptosDeNegocioComercialesPorRolesYIdConceptoBasicoYIdCategoriaIncluyendoStockYPrecios(idTransaccionInventarioFisico, idActorNegocioInternoQueTieneLosPrecios, (int)idConceptoBasico, (int)idCategoria);
                            return resultado;
                        }
                        else
                        {
                            resultado = ObtenerConceptosDeNegocioComercialesPorRolesYIdConceptoBasicoIncluyendoStockYPrecios(idTransaccionInventarioFisico, idActorNegocioInternoQueTieneLosPrecios, (int)idConceptoBasico);
                            return resultado;
                        }
                    }
                    else
                    {
                        if (idCategoria != null)
                        {
                            resultado = ObtenerConceptosDeNegocioComercialesPorRolesYIdCategoriaIncluyendoStockYPrecios(idTransaccionInventarioFisico, idActorNegocioInternoQueTieneLosPrecios, (int)idCategoria);
                            return resultado;
                        }
                        else
                        {
                            resultado = ObtenerConceptosDeNegocioComercialesIncluyendoStockYPrecios(idTransaccionInventarioFisico, idActorNegocioInternoQueTieneLosPrecios);
                            return resultado;
                        }
                    }
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener conceptos de negocios comerciales", e);
            }
        }
        public List<Concepto_Negocio_Comercial> ObtenerConceptosDeNegocioComercialesPorRolesYIdConceptoBasicoYIdCategoriaYIdsValoresCaracteristicasIncluyendoStockYPrecios(long IdTipoTransaccionInventarioActual, int idActorNegocioInternoQueTieneLosPrecios, int idConceptoBasico, int idCategoria, int[] idsValoresDeCaracteristicas)
        {
            try
            {
                List<Concepto_Negocio_Comercial> resultado = _conceptoRepositorio.ObtenerConceptosDeNegocioComercialesPorRolesYIdConceptoBasicoYIdCategoriaYIdsValoresCaracteristicasInclyendoStockYPrecios(IdTipoTransaccionInventarioActual, idActorNegocioInternoQueTieneLosPrecios, new int[] { ConceptoSettings.Default.IdRolMercaderia }, MaestroSettings.Default.IdDetalleMaestroTarifaNormal, new int[] { TransaccionSettings.Default.IdTipoTransaccionIngresoBienesPorCompra }, idConceptoBasico, idCategoria, idsValoresDeCaracteristicas).ToList();
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener conceptos de negocios comerciales por características", e);
            }
        }
        public List<Concepto_Negocio_Comercial> ObtenerConceptosDeNegocioComercialesPorRolesYIdConceptoBasicoYIdsValoresCaracteristicasIncluyendoStockYPrecios(long IdTipoTransaccionInventarioActual, int idActorNegocioInternoQueTieneLosPrecios, int idConceptoBasico, int[] idsValoresDeCaracteristicas)
        {
            try
            {
                List<Concepto_Negocio_Comercial> resultado = _conceptoRepositorio.ObtenerConceptosDeNegocioComercialesPorRolesYIdConceptoBasicoYIdsValoresCaracteristicasInclyendoStockYPrecios(IdTipoTransaccionInventarioActual, idActorNegocioInternoQueTieneLosPrecios, new int[] { ConceptoSettings.Default.IdRolMercaderia }, MaestroSettings.Default.IdDetalleMaestroTarifaNormal, new int[] { TransaccionSettings.Default.IdTipoTransaccionIngresoBienesPorCompra }, idConceptoBasico, idsValoresDeCaracteristicas).ToList();
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener conceptos de negocios comerciales por características", e);
            }
        }
        public List<Concepto_Negocio_Comercial> ObtenerConceptosDeNegocioComercialesPorIdCategoriaYIdsValoresCaracteristicasIncluyendoStockYPrecios(long idTransaccionInventarioFisico, int idActorNegocioInternoQueTieneLosPrecios, int idCategoria, int[] idsValoresDeCaracteristicas)
        {
            try
            {
                List<Concepto_Negocio_Comercial> resultado = _conceptoRepositorio.ObtenerConceptosDeNegociosComercialesPorRolesYIdCategoriaYIdsValoresDeCaracteristicasInclyendoStockYPrecios(idTransaccionInventarioFisico, idActorNegocioInternoQueTieneLosPrecios, new int[] { ConceptoSettings.Default.IdRolMercaderia }, MaestroSettings.Default.IdDetalleMaestroTarifaNormal, new int[] { TransaccionSettings.Default.IdTipoTransaccionIngresoBienesPorCompra }, idCategoria, idsValoresDeCaracteristicas).ToList();
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener conceptos de negocios comerciales por características", e);
            }
        }
        public List<Concepto_Negocio_Comercial> ObtenerConceptosDeNegocioComercialesPorIdsValoresCaracteristicasIncluyendoStockYPrecios(long idTransaccionInventarioFisico, int idActorNegocioInternoQueTieneLosPrecios, int[] idsValoresDeCaracteristicas)
        {
            try
            {
                List<Concepto_Negocio_Comercial> resultado = _conceptoRepositorio.ObtenerConceptosDeNegociosComercialesPorRolesYIdsValoresDeCaracteristicasInclyendoStockYPrecios(idTransaccionInventarioFisico, idActorNegocioInternoQueTieneLosPrecios, new int[] { ConceptoSettings.Default.IdRolMercaderia }, MaestroSettings.Default.IdDetalleMaestroTarifaNormal, new int[] { TransaccionSettings.Default.IdTipoTransaccionIngresoBienesPorCompra }, idsValoresDeCaracteristicas).ToList();
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener conceptos de negocios comerciales por características", e);
            }
        }
        public List<Concepto_Negocio_Comercial> ObtenerConceptosDeNegocioComercialesPorRolesYIdConceptoBasicoYIdCategoriaIncluyendoStockYPrecios(long idTransaccionInventarioFisico, int idActorNegocioInternoQueTieneLosPrecios, int idConceptoBasico, int idCategoria)
        {
            try
            {
                List<Concepto_Negocio_Comercial> resultado = _conceptoRepositorio.ObtenerConceptosDeNegocioComercialesPorRolesYIdConceptoBasicoYIdCategoriaInclyendoStockYPrecios(idTransaccionInventarioFisico, idActorNegocioInternoQueTieneLosPrecios, new int[] { ConceptoSettings.Default.IdRolMercaderia }, MaestroSettings.Default.IdDetalleMaestroTarifaNormal, new int[] { TransaccionSettings.Default.IdTipoTransaccionIngresoBienesPorCompra }, idConceptoBasico, idCategoria).ToList();
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener conceptos de negocios comerciales por concepto basico", e);
            }
        }
        public List<Concepto_Negocio_Comercial> ObtenerConceptosDeNegocioComercialesPorRolesYIdConceptoBasicoIncluyendoStockYPrecios(long idTransaccionInventarioFisico, int idActorNegocioInternoQueTieneLosPrecios, int idConceptoBasico)
        {
            try
            {
                List<Concepto_Negocio_Comercial> resultado = _conceptoRepositorio.ObtenerConceptosDeNegocioComercialesPorRolesYIdConceptoBasicoInclyendoStockYPrecios(idTransaccionInventarioFisico, idActorNegocioInternoQueTieneLosPrecios, new int[] { ConceptoSettings.Default.IdRolMercaderia }, MaestroSettings.Default.IdDetalleMaestroTarifaNormal, new int[] { TransaccionSettings.Default.IdTipoTransaccionIngresoBienesPorCompra }, idConceptoBasico).ToList();
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener conceptos de negocios comerciales por concepto basico", e);
            }
        }
        public List<Concepto_Negocio_Comercial> ObtenerConceptosDeNegocioComercialesPorRolesYIdCategoriaIncluyendoStockYPrecios(long idTransaccionInventarioFisico, int idActorNegocioInternoQueTieneLosPrecios, int idCategoria)
        {
            try
            {
                List<Concepto_Negocio_Comercial> resultado = _conceptoRepositorio.ObtenerConceptosDeNegociosComercialesPorRolesYIdCategoriaInclyendoStockYPrecios(idTransaccionInventarioFisico, idActorNegocioInternoQueTieneLosPrecios, new int[] { ConceptoSettings.Default.IdRolMercaderia }, MaestroSettings.Default.IdDetalleMaestroTarifaNormal, new int[] { TransaccionSettings.Default.IdTipoTransaccionIngresoBienesPorCompra }, idCategoria).ToList();
                return resultado;
            }
            catch (Exception e)
            {

                throw new LogicaException("Error al obtener conceptos de negocios comerciales", e);
            }
        }
        public List<Concepto_Negocio_Comercial> ObtenerConceptosDeNegocioComercialesIncluyendoStockYPrecios(long idTransaccionInventarioFisico, int idActorNegocioInternoQueTieneLosPrecios)
        {
            try
            {
                List<Concepto_Negocio_Comercial> resultado = _conceptoRepositorio.ObtenerConceptosDeNegociosComercialesPorRolesInclyendoStockYPrecios(idTransaccionInventarioFisico, idActorNegocioInternoQueTieneLosPrecios, new int[] { ConceptoSettings.Default.IdRolMercaderia }, MaestroSettings.Default.IdDetalleMaestroTarifaNormal, new int[] { TransaccionSettings.Default.IdTipoTransaccionIngresoBienesPorCompra }).ToList();
                return resultado;
            }
            catch (Exception e)
            {

                throw new LogicaException("Error al obtener conceptos de negocios comerciales", e);
            }
        }
        #endregion

        #region CONCEPTOS NEGOCIOS COMERCIALES PARA OPERACIONES
        public List<Selector_Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComerciales(int modoSeleccionTipoFamilia, int informacionSelectorConcepto, UserProfileSessionData sesionDeUsuario)
        {
            try
            {
                var idTransaccionInventario = sesionDeUsuario.ObtenerIdInventarioActual(sesionDeUsuario.IdCentroAtencionQueTieneElStockIntegrada);
                var idCentroAtencionDePrecios = sesionDeUsuario.IdCentroAtencionQueTieneLosPrecios;
                List<Selector_Concepto_Negocio_Comercial> resultado;
                var idRol = ConceptoSettings.Default.IdRolMercaderia;
                if (modoSeleccionTipoFamilia == (int)ModoSeleccionTipoFamilia.Ambos)
                {
                    resultado = (informacionSelectorConcepto == (int)InformacionSelectorConcepto.Nombre) ? _conceptoRepositorio.ObtenerConceptosDeNegociosComercialesPorRol(idRol).ToList() : _conceptoRepositorio.ObtenerConceptosDeNegociosComercialesConStockPrecioPorRol(idRol, idTransaccionInventario, idCentroAtencionDePrecios).ToList();
                    resultado.ForEach(cn => cn.SinControlStock = sesionDeUsuario.CentroAtencionQueTieneElStockIntegrada.SalidaBienesSinStock);
                    resultado.ForEach(cn => cn.MostrarStockYPrecio = informacionSelectorConcepto == (int)InformacionSelectorConcepto.NombreStockPrecio);
                }
                else
                {
                    resultado = (informacionSelectorConcepto == (int)InformacionSelectorConcepto.Nombre) ? _conceptoRepositorio.ObtenerConceptosDeNegociosComercialesPorRolYTipoFamilia(idRol, modoSeleccionTipoFamilia.ToString()).ToList() : _conceptoRepositorio.ObtenerConceptosDeNegociosComercialesConStockPrecioPorRolYTipoFamilia(idRol, idTransaccionInventario, idCentroAtencionDePrecios, modoSeleccionTipoFamilia.ToString()).ToList();
                    resultado.ForEach(cn => cn.SinControlStock = sesionDeUsuario.CentroAtencionQueTieneElStockIntegrada.SalidaBienesSinStock);
                    resultado.ForEach(cn => cn.MostrarStockYPrecio = informacionSelectorConcepto == (int)InformacionSelectorConcepto.NombreStockPrecio);
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener conceptos de negocio comerciales", e);
            }
        }
        public List<Selector_Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesPorBusquedaConcepto(string cadenaBusqueda, int modoSeleccionTipoFamilia, int informacionSelectorConcepto, UserProfileSessionData sesionDeUsuario)
        {
            try
            {
                var idTransaccionInventario = sesionDeUsuario.ObtenerIdInventarioActual(sesionDeUsuario.IdCentroAtencionQueTieneElStockIntegrada);
                var idCentroAtencionDePrecios = sesionDeUsuario.IdCentroAtencionQueTieneLosPrecios;
                List<Selector_Concepto_Negocio_Comercial> resultado;
                var idRol = ConceptoSettings.Default.IdRolMercaderia;
                if (modoSeleccionTipoFamilia == (int)ModoSeleccionTipoFamilia.Ambos)
                {
                    resultado = (informacionSelectorConcepto == (int)InformacionSelectorConcepto.Nombre) ? _conceptoRepositorio.ObtenerConceptosDeNegociosComercialesPorRolYBusquedaConcepto(idRol, cadenaBusqueda).ToList() : _conceptoRepositorio.ObtenerConceptosDeNegociosComercialesConStockPrecioPorRolYBusquedaConcepto(idRol, idTransaccionInventario, idCentroAtencionDePrecios, cadenaBusqueda).ToList();
                    resultado.ForEach(cn => cn.SinControlStock = sesionDeUsuario.CentroAtencionQueTieneElStockIntegrada.SalidaBienesSinStock);
                    resultado.ForEach(cn => cn.MostrarStockYPrecio = informacionSelectorConcepto == (int)InformacionSelectorConcepto.NombreStockPrecio);
                }
                else
                {
                    resultado = (informacionSelectorConcepto == (int)InformacionSelectorConcepto.Nombre) ? _conceptoRepositorio.ObtenerConceptosDeNegociosComercialesPorRolYBusquedaConceptoYTipoFamilia(idRol, cadenaBusqueda, modoSeleccionTipoFamilia.ToString()).ToList() : _conceptoRepositorio.ObtenerConceptosDeNegociosComercialesConStockPrecioPorRolYBusquedaConceptoYTipoFamilia(idRol, idTransaccionInventario, idCentroAtencionDePrecios, cadenaBusqueda, modoSeleccionTipoFamilia.ToString()).ToList();
                    resultado.ForEach(cn => cn.SinControlStock = sesionDeUsuario.CentroAtencionQueTieneElStockIntegrada.SalidaBienesSinStock);
                    resultado.ForEach(cn => cn.MostrarStockYPrecio = informacionSelectorConcepto == (int)InformacionSelectorConcepto.NombreStockPrecio);
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener conceptos de negocio comerciales", e);
            }
        }
        public List<Selector_Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesPorFamilia(int idFamilia, int informacionSelectorConcepto, UserProfileSessionData sesionDeUsuario)
        {
            try
            {
                var idTransaccionInventario = sesionDeUsuario.ObtenerIdInventarioActual(sesionDeUsuario.IdCentroAtencionQueTieneElStockIntegrada);
                var idCentroAtencionDePrecios = sesionDeUsuario.IdCentroAtencionQueTieneLosPrecios;
                var idRol = ConceptoSettings.Default.IdRolMercaderia;
                List<Selector_Concepto_Negocio_Comercial> resultado = (informacionSelectorConcepto == (int)InformacionSelectorConcepto.Nombre) ? _conceptoRepositorio.ObtenerConceptosDeNegociosComercialesPorRolYFamilia(idRol, idFamilia).ToList() : _conceptoRepositorio.ObtenerConceptosDeNegociosComercialesConStockPrecioPorRolYFamilia(idRol, idTransaccionInventario, idCentroAtencionDePrecios, idFamilia).ToList();
                resultado.ForEach(cn => cn.SinControlStock = sesionDeUsuario.CentroAtencionQueTieneElStockIntegrada.SalidaBienesSinStock);
                resultado.ForEach(cn => cn.MostrarStockYPrecio = informacionSelectorConcepto == (int)InformacionSelectorConcepto.NombreStockPrecio);
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener conceptos de negocio comerciales por familia", e);
            }
        }

        public Concepto_Negocio_Comercial_ ObtenerConceptoDeNegocioComercialPorCodigoBarra(UserProfileSessionData sesionDeUsuario, string codigoBarra, bool complementoStock, bool complementoPrecio, int modoSeleccionTipoFamilia)
        {
            try
            {
                if (string.IsNullOrEmpty(codigoBarra) || string.IsNullOrWhiteSpace(codigoBarra))
                {
                    throw new AdvertenciaException("Ingresar un código de barra válido.");
                }
                bool esBien = _conceptoRepositorio.EsBien(codigoBarra);
                var idRol = ConceptoSettings.Default.IdRolMercaderia;
                var idTransaccionInventarioFisico = sesionDeUsuario.ObtenerIdInventarioActual(sesionDeUsuario.IdCentroAtencionQueTieneElStockIntegrada);
                var idCentroAtencionDePrecios = sesionDeUsuario.IdCentroAtencionQueTieneLosPrecios;
                List<Concepto_Negocio_Comercial_> resultado;
                if (modoSeleccionTipoFamilia == (int)ModoSeleccionTipoFamilia.Ambos)
                {
                    if (complementoStock && complementoPrecio)
                    {
                        if (esBien)
                        {
                            resultado = _conceptoRepositorio.ObtenerConceptosDeNegociosComercialesConStockPrecioPorRolesYCodigoBarra(idTransaccionInventarioFisico, idCentroAtencionDePrecios, idRol, codigoBarra).ToList();
                        }
                        else
                        {
                            resultado = _conceptoRepositorio.ObtenerConceptosDeNegociosComercialesConPrecioPorRolesYCodigoBarra(idCentroAtencionDePrecios, idRol, codigoBarra).ToList();
                        }
                    }
                    else if (complementoStock && !complementoPrecio)
                    {
                        if (esBien)
                        {
                            resultado = _conceptoRepositorio.ObtenerConceptosDeNegociosComercialesConStockPorRolesYCodigoBarra(idTransaccionInventarioFisico, idRol, codigoBarra).ToList();
                        }
                        else
                        {
                            resultado = _conceptoRepositorio.ObtenerConceptosDeNegociosComercialesPorRolesYCodigoBarra(idRol, codigoBarra).ToList();
                        }
                    }
                    else if (!complementoStock && complementoPrecio)
                    {
                        resultado = _conceptoRepositorio.ObtenerConceptosDeNegociosComercialesConPrecioPorRolesYCodigoBarra(idCentroAtencionDePrecios, idRol, codigoBarra).ToList();
                    }
                    else
                    {
                        resultado = _conceptoRepositorio.ObtenerConceptosDeNegociosComercialesPorRolesYCodigoBarra(idRol, codigoBarra).ToList();
                    }
                }
                else
                {
                    if (complementoStock && complementoPrecio)
                    {
                        if (esBien)
                        {
                            resultado = _conceptoRepositorio.ObtenerConceptosDeNegociosComercialesConStockPrecioPorRolesYCodigoBarra(idTransaccionInventarioFisico, idCentroAtencionDePrecios, idRol, codigoBarra, modoSeleccionTipoFamilia.ToString()).ToList();
                        }
                        else
                        {
                            resultado = _conceptoRepositorio.ObtenerConceptosDeNegociosComercialesConPrecioPorRolesYCodigoBarra(idCentroAtencionDePrecios, idRol, codigoBarra, modoSeleccionTipoFamilia.ToString()).ToList();
                        }
                    }
                    else if (complementoStock && !complementoPrecio)
                    {
                        if (esBien)
                        {
                            resultado = _conceptoRepositorio.ObtenerConceptosDeNegociosComercialesConStockPorRolesYCodigoBarra(idTransaccionInventarioFisico, idRol, codigoBarra, modoSeleccionTipoFamilia.ToString()).ToList();
                        }
                        else
                        {
                            resultado = _conceptoRepositorio.ObtenerConceptosDeNegociosComercialesPorRolesYCodigoBarra(idRol, codigoBarra, modoSeleccionTipoFamilia.ToString()).ToList();
                        }
                    }
                    else if (!complementoStock && complementoPrecio)
                    {
                        resultado = _conceptoRepositorio.ObtenerConceptosDeNegociosComercialesConPrecioPorRolesYCodigoBarra(idCentroAtencionDePrecios, idRol, codigoBarra, modoSeleccionTipoFamilia.ToString()).ToList();
                    }
                    else
                    {
                        resultado = _conceptoRepositorio.ObtenerConceptosDeNegociosComercialesPorRolesYCodigoBarra(idRol, codigoBarra, modoSeleccionTipoFamilia.ToString()).ToList();
                    }
                }
                //Validaciones del resultado obtenido
                if (resultado == null)
                {
                    throw new AdvertenciaException("No existe concepto con el código de barra proporcionado.");
                }
                else
                {
                    if (resultado.Count() > 1)
                    {
                        throw new AdvertenciaException("Existe más de un concepto con el mismo código de barra.");
                    }
                    else
                    {
                        return resultado.First();
                    }
                }
            }
            catch (AdvertenciaException e)
            {
                throw new AdvertenciaException(e.Message);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener concepto de negocio comercial por código de barra.", e);
            }
        }

        public Concepto_Negocio_Comercial_ ObtenerConceptoDeNegocioComercialPorIdConcepto(UserProfileSessionData sesionDeUsuario, int idConceptoNegocio, bool complementoStock, bool complementoPrecio)
        {
            try
            {
                bool esBien = _conceptoRepositorio.EsBien(idConceptoNegocio);
                var idRol = ConceptoSettings.Default.IdRolMercaderia;
                var idTransaccionInventarioFisico = sesionDeUsuario.ObtenerIdInventarioActual(sesionDeUsuario.IdCentroAtencionQueTieneElStockIntegrada);
                var idCentroAtencionDePrecios = sesionDeUsuario.IdCentroAtencionQueTieneLosPrecios;
                Concepto_Negocio_Comercial_ resultado;
                if (complementoStock && complementoPrecio)
                {
                    if (esBien)
                    {
                        resultado = _conceptoRepositorio.ObtenerConceptoDeNegocioComercialConStockPrecioPorRolesEIdConcepto(idTransaccionInventarioFisico, idCentroAtencionDePrecios, idRol, idConceptoNegocio);
                    }
                    else
                    {
                        resultado = _conceptoRepositorio.ObtenerConceptoDeNegocioComercialConPrecioPorRolesEIdConcepto(idCentroAtencionDePrecios, idRol, idConceptoNegocio);
                    }
                }
                else if (complementoStock && !complementoPrecio)
                {
                    if (esBien)
                    {
                        resultado = _conceptoRepositorio.ObtenerConceptoDeNegocioComercialConStockPorRolesEIdConcepto(idTransaccionInventarioFisico, idRol, idConceptoNegocio);
                    }
                    else
                    {
                        resultado = _conceptoRepositorio.ObtenerConceptoDeNegocioComercialPorRolesEIdConcepto(idRol, idConceptoNegocio);
                    }
                }
                else if (!complementoStock && complementoPrecio)
                {
                    resultado = _conceptoRepositorio.ObtenerConceptoDeNegocioComercialConPrecioPorRolesEIdConcepto(idCentroAtencionDePrecios, idRol, idConceptoNegocio);
                }
                else
                {
                    resultado = _conceptoRepositorio.ObtenerConceptoDeNegocioComercialPorRolesEIdConcepto(idRol, idConceptoNegocio);
                }
                if (resultado == null)
                {
                    throw new AdvertenciaException("No existe concepto de negocio.");
                }
                return resultado;
            }
            catch (AdvertenciaException e)
            {
                throw new AdvertenciaException(e.Message);
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener concepto de negocio comerciale por id concepto negocio.", e);
            }
        }
        public List<Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialParaVentaPorNombre(int idCentroAtencionQueTieneLosPrecios, string nombre)
        {
            try
            {

                var resultado = _conceptoRepositorio.ObtenerConceptosDeNegociosComercialesPorNombreInclyendoStockPreciosYStock(idCentroAtencionQueTieneLosPrecios, TransaccionSettings.Default.IdTipoTransaccionInventarioActual, MaestroSettings.Default.IdDetalleMaestroTarifaNormal, MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado, new int[] { ConceptoSettings.Default.IdRolMercaderia }, nombre).OrderBy(cnc => cnc.Nombre).ToList();

                // Concepto_Negocio_Comercial.EstablecerNombreSegunLaMascara(resultado, AplicacionSettings.Default.MascaraConceptoNegocioVenta, Diccionario.ValoresMascaraConceptoNegocioVenta).ToList();
                return resultado;

            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener conceptos de negocio comerciales  para venta", e);
            }
        }




        //public List<Concepto_Negocio_Comercial> ObtenerConceptosDeNegociosComercialesParaVenta(int idCentroAtencionQueTieneLosPrecios, int idConceptoBasico)
        //{
        //    try
        //    {
        //        return _conceptoRepositorio.ObtenerConceptosDeNegociosComercialesIncluyendoPrecios(idCentroAtencionQueTieneLosPrecios, new int[] { ConceptoSettings.Default.IdRolMercaderia }, idConceptoBasico).ToList();
        //    }
        //    catch (Exception e)
        //    {
        //        throw new LogicaException("Error al obtener concepto de negocio comerciale por codigo de barra.", e);
        //    }
        //}



        //public Concepto_Negocio_Comercial ObtenerConceptoDeNegocioComercialParaVentaPorCodigoBarra(long idTransaccionInventarioFisico, int idCentroAtencionQueTieneLosPrecios, string codigoBarra)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(codigoBarra) || string.IsNullOrWhiteSpace(codigoBarra))
        //        {
        //            throw new AdvertenciaException("Ingresar un código de barra válido.");
        //        }
        //        bool esBien = _conceptoRepositorio.EsBien(codigoBarra);
        //        if (esBien || AplicacionSettings.Default.PermitirLoteEnDetalleDeCompra)
        //        {
        //            return _conceptoRepositorio.ObtenerConceptosDeNegociosComercialesPorCodigoBarraIncluyendoPrecioInclyendoComplementos(idTransaccionInventarioFisico, idCentroAtencionQueTieneLosPrecios, new int[] { ConceptoSettings.Default.IdRolMercaderia, ConceptoSettings.Default.IdRolServicio }, codigoBarra);
        //        }
        //        else
        //        {
        //            return _conceptoRepositorio.ObtenerConceptosDeNegociosComercialesPorCodigoBarraIncluyendoPrecios(idCentroAtencionQueTieneLosPrecios, new int[] { ConceptoSettings.Default.IdRolMercaderia }, codigoBarra);
        //        }
        //    }
        //    catch (AdvertenciaException e)
        //    {
        //        throw new AdvertenciaException(e.Message);
        //    }
        //    catch (Exception e)
        //    {
        //        throw new LogicaException("Error al obtener conceptos de negocio comerciales para venta", e);
        //    }
        //}
        //public Concepto_Negocio_Comercial ObtenerConceptoDeNegocioComercialParaVentaPorId(long idTransaccionInventarioFisico, int idCentroAtencionQueTieneLosPrecios, int id)
        //{
        //    try
        //    {
        //        bool esBien = _conceptoRepositorio.EsBien(id);
        //        if (esBien || AplicacionSettings.Default.PermitirLoteEnDetalleDeCompra)
        //        {
        //            return _conceptoRepositorio.ObtenerConceptosDeNegociosComercialesPorIdIncluyendoPrecioInclyendoComplementos(idTransaccionInventarioFisico, idCentroAtencionQueTieneLosPrecios, new int[] { ConceptoSettings.Default.IdRolMercaderia, ConceptoSettings.Default.IdRolServicio }, id);
        //        }
        //        else
        //        {
        //            return _conceptoRepositorio.ObtenerConceptosDeNegociosComercialesPorIdIncluyendoPrecios(idCentroAtencionQueTieneLosPrecios, new int[] { ConceptoSettings.Default.IdRolMercaderia }, id);
        //        }
        //    }
        //    catch (AdvertenciaException e)
        //    {
        //        throw new AdvertenciaException(e.Message);
        //    }
        //    catch (Exception e)
        //    {
        //        throw new LogicaException("Error al obtener conceptos de negocio comerciales para venta", e);
        //    }
        //}


        //public Concepto_Negocio_Comercial ObtenerConceptoDeNegocioComercialParaVentaPorIdConceptoNegocio(long idTransaccionInventarioFisico, int idCentroAtencionQueTieneLosPrecios, int idConceptoNegocio, bool esBien)
        //{
        //    try
        //    {
        //        if (esBien || AplicacionSettings.Default.PermitirLoteEnDetalleDeCompra)
        //        {
        //            return _conceptoRepositorio.ObtenerConceptosDeNegociosComercialesPorCodigoBarraIncluyendoPrecioInclyendoComplementos(idTransaccionInventarioFisico, idCentroAtencionQueTieneLosPrecios, new int[] { ConceptoSettings.Default.IdRolMercaderia, ConceptoSettings.Default.IdRolServicio }, idConceptoNegocio);
        //        }
        //        else
        //        {
        //            return _conceptoRepositorio.ObtenerConceptosDeNegociosComercialesPorCodigoBarraIncluyendoPrecios(idCentroAtencionQueTieneLosPrecios, new int[] { ConceptoSettings.Default.IdRolMercaderia, ConceptoSettings.Default.IdRolServicio }, idConceptoNegocio);
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        throw new LogicaException("Error al obtener conceptos de negocio comerciales par venta", e);
        //    }
        //}


        #endregion


        #region CONCEPTOS BASICOS

        public List<Concepto_Basico> ObtenerConceptosaBasicosVigentesIncluyendoCaracteristicas()
        {
            try
            {
                return _conceptoRepositorio.ObtenerConceptosBasicosIncluyendodoCaracteristicas(MaestroSettings.Default.IdMaestroConcepto, true).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener conceptos básicos", e);
            }
        }

        #endregion



        public List<ReporteDigemid> ObtenerReporteConceptosDigemid(UserProfileSessionData sesionUsuario)
        {
            try
            {
                int idCentroAtencionQueTieneLosPrecios = sesionUsuario.IdCentroAtencionQueTieneLosPrecios;
                var resultado = _conceptoRepositorio.ObtenerReporteConceptosDigemid(ConceptoSettings.Default.IdRolMercaderia, idCentroAtencionQueTieneLosPrecios, ConceptoSettings.Default.IdTarifaSeleccionadaParaPrecioUnitarioEnReporteDigemid).OrderBy(cnc => cnc.CodigoConcepto).ToList();
                var establecimiento = _actorRepositorio.ObtenerEstablecimiento(sesionUsuario.CentroDeAtencionSeleccionado.EstablecimientoComercial.Id);
                resultado.ForEach(r => r.CodigoEstablecimiento = establecimiento.CodigoEstablecimientoDigemid());

                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener reporte de conceptos de digemid", e);
            }
        }

    }
}

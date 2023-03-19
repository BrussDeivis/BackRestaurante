using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;

namespace Tsp.Sigescom.Logica
{
    public class MaestroLogica : IMaestroLogica
    {
        private readonly IMaestroRepositorio _repositorioMaestro;

        public MaestroLogica(IMaestroRepositorio repositorioMaestro)
        {
            _repositorioMaestro = repositorioMaestro;
        }

        public OperationResult guardarMaestro(string codigo, string nombre)
        {
            try
            {
                Maestro nuevoMaestro = new Maestro
                {
                    codigo = codigo,
                    nombre = nombre
                };
                return _repositorioMaestro.crearMaestro(nuevoMaestro);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public OperationResult guardarDetalleMaestro(int idMaestro, string codigo, string nombre, string valor)
        {
            try
            {
                Detalle_maestro detalleMaestro = new Detalle_maestro
                {
                    id_maestro = idMaestro,
                    codigo = codigo,
                    nombre = nombre,
                    valor = valor,
                    fecha_registro = DateTime.Now,
                    es_vigente = true
                };
                return _repositorioMaestro.crearDetalleMaestro(detalleMaestro);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public OperationResult GuardarDetalleDetalleMaestro(int idMaestro, string codigo, string nombre, string valor, int idMaestroPadre)
        {
            try
            {
                Detalle_detalle_maestro detalle = new Detalle_detalle_maestro()
                {
                    id_detalle_maestro_principal = idMaestroPadre,
                    Detalle_maestro1 = new Detalle_maestro
                    {
                        id_maestro = idMaestro,
                        codigo = codigo,
                        nombre = nombre,
                        valor = valor,
                        fecha_registro = DateTime.Now,
                        es_vigente = true
                    }
                };
                return _repositorioMaestro.CrearDetalleDetalleMaestro(detalle);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public OperationResult actualizarMaestro(int id, string codigo, string nombre)
        {
            try
            {
                Maestro maestro = new Maestro();
                maestro.id = id;
                maestro.codigo = codigo;
                maestro.nombre = nombre;
                return _repositorioMaestro.actualizarMaestro(maestro);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public OperationResult actualizarDetalleMaestro(int id, int idMaestro, string codigo, string nombre, string valor)
        {
            try
            {
                Detalle_maestro detalleMaestro = new Detalle_maestro
                {
                    id = id,
                    id_maestro = idMaestro,
                    codigo = codigo,
                    nombre = nombre,
                    valor = valor,
                    es_vigente = true
                };
                return _repositorioMaestro.actualizarDetalleMaestro(detalleMaestro);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public OperationResult ActualizarDetalleDetalleMaestro(int id, int idMaestro, string codigo, string nombre, string valor, int idMaestroPadre)
        {
            try
            {
                Detalle_detalle_maestro detalle = new Detalle_detalle_maestro()
                {
                    id_detalle_maestro_principal = idMaestroPadre,
                    Detalle_maestro1 = new Detalle_maestro
                    {
                        id = id,
                        id_maestro = idMaestro,
                        codigo = codigo,
                        nombre = nombre,
                        valor = valor,
                        fecha_registro = DateTime.Now,
                        es_vigente = true
                    }
                };
                return _repositorioMaestro.ActualizarDetalleDetalleMaestro(detalle);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Maestro> obtenerMaestros()
        {
            try { return _repositorioMaestro.obtenerMaestros().ToList(); }
            catch (Exception e) { throw e; }
        }

        public Detalle_maestro obtenerDetalleMaestro(int idDetalleMaestro)
        {
            try { return _repositorioMaestro.ObtenerDetalle(idDetalleMaestro); }
            catch (Exception e) { throw e; }
        }

        public Detalle_maestro ObtenerDetalleMaestroPorIdMaestroYNombre(int idMaestro, string nombre)
        {
            try { return _repositorioMaestro.ObtenerDetalleMaestroPorIdMaestroYNombre(idMaestro, nombre); }
            catch (Exception e) { throw new LogicaException("Error al obtener detalle maestro por idMaestro y nombre", e); }
        }


        public async Task<List<Detalle_maestro>> ObtenerDetallesMaestrosAsync(int idMaestro)
        {
            try
            {
                return (await _repositorioMaestro.ObtenerDetallesAsync(idMaestro)).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Detalle_maestro> ObtenerDetallesMaestros(int idMaestro)
        {
            try { return _repositorioMaestro.ObtenerDetalles(idMaestro).ToList(); }
            catch (Exception e) { throw e; }
        }

        public async Task<List<Detalle_maestro>> ObtenerCategorias()
        {
            try { return (await _repositorioMaestro.ObtenerDetallesAsync(MaestroSettings.Default.IdMaestroCategoriaConcepto)).ToList(); }
            catch (Exception e) { throw e; }
        }

        public async Task<List<Detalle_maestro>> ObtenerEntidadesFinancieras()
        {
            try { return (await _repositorioMaestro.ObtenerDetallesAsync(MaestroSettings.Default.IdMaestroEntidadesBancarias)).ToList(); }
            catch (Exception e) { throw e; }
        }
        public async Task<List<Detalle_maestro>> ObtenerMonedas()
        {
            try
            {
                return (await _repositorioMaestro.ObtenerDetallesAsync(MaestroSettings.Default.IdMaestroMonedas)).ToList();
            }
            catch (Exception e) { throw e; }
        }

        public async Task<List<Detalle_maestro>> ObtenerTiposCuentaBancaria()
        {
            try { return (await _repositorioMaestro.ObtenerDetallesAsync(MaestroSettings.Default.IdMaestroTiposCuentaBancaria)).ToList(); }
            catch (Exception e) { throw e; }
        }
        public async Task<List<Detalle_maestro>> ObtenerOperadoresDeTarjeta()
        {
            try { return (await _repositorioMaestro.ObtenerDetallesAsync(MaestroSettings.Default.IdMaestroTarjetasBancarias)).ToList(); }
            catch (Exception e) { throw e; }
        }

        public async Task<List<Detalle_maestro>> ObtenerTiposDeComprobante(int idMaestro)
        {
            try { return (await _repositorioMaestro.ObtenerDetallesAsync(MaestroSettings.Default.IdMaestroDocumento)).ToList(); }
            catch (Exception e) { throw e; }
        }

        public async Task<List<Detalle_maestro>> ObtenerMediosDePago()
        {
            try { return (await _repositorioMaestro.ObtenerDetallesAsync(MaestroSettings.Default.IdMaestroMedioDePago)).ToList(); }
            catch (Exception e) { throw e; }
        }

        public async Task<List<Detalle_maestro>> ObtenerModalidadesTrasladoAsync()
        {
            try { return (await _repositorioMaestro.ObtenerDetallesAsync(MaestroSettings.Default.IdMaestroModalidadDeTraslado)).ToList(); }
            catch (Exception e) { throw e; }
        }
        public List<Detalle_maestro> ObtenerModalidadesTraslado()
        {
            try { return _repositorioMaestro.ObtenerDetalles(MaestroSettings.Default.IdMaestroModalidadDeTraslado).ToList(); }
            catch (Exception e) { throw e; }
        }

        public async Task<List<Detalle_maestro>> ObtenerMotivosTrasladoAsync()
        {
            try { return (await _repositorioMaestro.ObtenerDetallesAsync(MaestroSettings.Default.IdMaestroMotivoDeTraslado)).ToList(); }
            catch (Exception e) { throw e; }
        }
        public async Task<List<Detalle_maestro>> ObtenerMotivosTrasladoVigentesAsync()
        {
            try { return (await _repositorioMaestro.ObtenerDetallesVigentesAsync(MaestroSettings.Default.IdMaestroMotivoDeTraslado)).ToList(); }
            catch (Exception e) { throw e; }
        }
        public List<Detalle_maestro> ObtenerMotivosTraslado()
        {
            try { return _repositorioMaestro.ObtenerDetalles(MaestroSettings.Default.IdMaestroMotivoDeTraslado).ToList(); }
            catch (Exception e) { throw e; }
        }

    
        public async Task<List<Detalle_maestro>> ObtenerMotivosDeViajeAsync()
        {
            try { return (await _repositorioMaestro.ObtenerDetallesAsync(HotelSettings.Default.IdMaestroMotivosDeViaje)).ToList(); }
            catch (Exception e) { throw e; }
        }

        public async Task<List<Detalle_maestro>> ObtenerTiposDeNotaDeDebito()
        {
            try
            {
                int[] idsTiposComprobantes = { MaestroSettings.Default.IdDetalleMaestroNotaDeDebitoElectronicaPenalidadesYOtrosConceptos };
                List<Detalle_maestro> detalles = (await _repositorioMaestro.ObtenerDetallesAsync(MaestroSettings.Default.IdMaestroTipoDeNotaDeDebitoElectronica)).ToList();
                detalles.RemoveAll(d => idsTiposComprobantes.Contains(d.id));
                return detalles;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Detalle_maestro>> ObtenerTiposDeNotaDeCredito()
        {
            try
            {
                int[] idsTiposComprobantes = { MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaAnulacionPorErrorEnElRuc, MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaCorreccionPorErrorEnLaDescripcion, MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaBonificacion, MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaDisminucionEnElValor, MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaOtrosConceptos };
                List<Detalle_maestro> detalles = (await _repositorioMaestro.ObtenerDetallesAsync(MaestroSettings.Default.IdMaestroTipoDeNotaDeCreditoElectronica)).ToList();
                detalles.RemoveAll(d => idsTiposComprobantes.Contains(d.id));
                return detalles;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Detalle_maestro>> ObtenerTiposDeVia()
        {
            try { return (await _repositorioMaestro.ObtenerDetallesAsync(MaestroSettings.Default.IdMaestroTipoDeVia)).ToList(); }
            catch (Exception e) { throw e; }
        }

        public async Task<List<Detalle_maestro>> ObtenerTiposDeZona()
        {
            try { return (await _repositorioMaestro.ObtenerDetallesAsync(MaestroSettings.Default.IdMaestroTipoDeZona)).ToList(); }
            catch (Exception e) { throw e; }
        }

        public async Task<List<Detalle_maestro>> ObtenerNaciones()
        {
            try { return (await _repositorioMaestro.ObtenerDetallesAsync(MaestroSettings.Default.IdMaestroNacion)).ToList(); }
            catch (Exception e) { throw e; }
        }

        public async Task<List<Detalle_maestro>> ObtenerTiposDeDocumentosDeIdentidad()
        {
            try
            {
                var idsTipoDocumento = Diccionario.TiposDeDocumentoIdentidadParaTipoActorNaturalJuridico;
                return (await _repositorioMaestro.ObtenerDetallesEspecificos(idsTipoDocumento)).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Ubigeo> obtenerUbigeoDistrito()
        {
            try { return _repositorioMaestro.obtenerUbigeos(MaestroSettings.Default.IdPaisUbigeoPeru).ToList(); }
            catch (Exception e) { throw e; }
        }

        public List<Ubigeo> obtenerUbigeo(int[] idUbigeos)
        {
            try { return _repositorioMaestro.obtenerUbigeos(idUbigeos).ToList(); }
            catch (Exception e) { throw e; }
        }

        public async Task<List<Detalle_maestro>> ObtenerTiposDeDireccion()
        {
            try { return (await _repositorioMaestro.ObtenerDetallesAsync(MaestroSettings.Default.IdMaestroTipoDireccion)).ToList(); }
            catch (Exception e) { throw e; }
        }

        public List<Detalle_maestro> obtenerTarifas()
        {
            try { return _repositorioMaestro.obtenerDetallesMaestros(MaestroSettings.Default.IdMaestroTarifa).OrderBy(t => t.id).ToList(); }
            catch (Exception e) { throw e; }
        }
        //public List<Detalle_maestro> obtenerCategoriasDeProducto()
        //{
        //    try { return _repositorioMaestro.obtenerDetalles(MaestroSettings.Default.IdMaestroCategriaProducto).ToList(); }
        //    catch (Exception e) { throw e; }
        //}
        public List<Detalle_maestro> ObtenerConceptosVigentes()
        {
            try
            {
                var familiasVigentes = true;
                return _repositorioMaestro.ObtenerDetalles(MaestroSettings.Default.IdMaestroConcepto, familiasVigentes).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener detalle maestro por idMaestro y nombre", e);
            }
        }

        public List<Familia_Concepto_Comercial> ObtenerFamiliasVigentes()
        {
            try
            {
                var familiasVigentes = true;
                var resultado = _repositorioMaestro.ObtenerFamiliasConceptoComercial(MaestroSettings.Default.IdMaestroConcepto, familiasVigentes).OrderBy(f => f.Nombre).ToList();
                foreach (var idFamiliaNoMostrar in Diccionario.IdsFamilasANoMostrar)
                {
                    resultado.Remove(resultado.Single(r => r.Id == Convert.ToInt32(idFamiliaNoMostrar)));
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al obtener detalle maestro por idMaestro y nombre", e);
            }
        }

        public List<Familia_Concepto_Comercial> ObtenerFamiliasVigentes(int modoSeleccionTipoFamilia)
        {
            try
            {
                List<Familia_Concepto_Comercial> resultado;
                var familiasVigentes = true;
                if (modoSeleccionTipoFamilia == (int)ModoSeleccionTipoFamilia.Ambos)
                {
                    resultado = _repositorioMaestro.ObtenerFamiliasConceptoComercial(MaestroSettings.Default.IdMaestroConcepto, familiasVigentes).OrderBy(f => f.Nombre).ToList();
                }
                else
                {
                    resultado = _repositorioMaestro.ObtenerFamiliasConceptoComercial(MaestroSettings.Default.IdMaestroConcepto, modoSeleccionTipoFamilia.ToString(), familiasVigentes).OrderBy(f => f.Nombre).ToList();
                }
                foreach (var idFamiliaNoMostrar in Diccionario.IdsFamilasANoMostrar)
                {
                    resultado.Remove(resultado.Single(r => r.Id == Convert.ToInt32(idFamiliaNoMostrar)));
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Familia_Concepto_Comercial> ObtenerFamiliasConceptosComercialesVigentes()
        {
            try
            {
                var conceptosVigentes = true;
                var resultado = _repositorioMaestro.ObtenerFamiliasConceptoComercialPorRol(ConceptoSettings.Default.IdRolMercaderia, conceptosVigentes).OrderBy(f => f.Nombre).ToList();
                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Familia_Concepto_Comercial> ObtenerFamiliasConceptosComercialesVigentes(int modoSeleccionTipoFamilia)
        {
            try
            {
                List<Familia_Concepto_Comercial> resultado;
                var conceptosVigentes = true;
                if (modoSeleccionTipoFamilia == (int)ModoSeleccionTipoFamilia.Ambos)
                {
                    resultado = _repositorioMaestro.ObtenerFamiliasConceptoComercialPorRol(ConceptoSettings.Default.IdRolMercaderia, conceptosVigentes).OrderBy(f => f.Nombre).ToList();
                }
                else
                {
                    resultado = _repositorioMaestro.ObtenerFamiliasConceptoComercial(ConceptoSettings.Default.IdRolMercaderia, modoSeleccionTipoFamilia.ToString(), conceptosVigentes).OrderBy(f => f.Nombre).ToList();
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        public List<Detalle_maestro> ObtenerConceptosServicioVigentes()
        {
            try
            {
                string valorServicioDeConceptoBasico = "0";//Todo:Parametrizar el valor de concepto basico de servicio
                return _repositorioMaestro.ObtenerDetallesVigentesPorValor(MaestroSettings.Default.IdMaestroConcepto, true, valorServicioDeConceptoBasico).ToList();
            }
            catch (Exception e) { throw e; }
        }


        public List<Detalle_maestro> ObtenerConceptosVigentesDeCompraVenta()
        {
            try
            {
                List<Detalle_maestro> conceptos = _repositorioMaestro.ObtenerDetalles(MaestroSettings.Default.IdMaestroConcepto, true).ToList();
                List<int> idConceptosExcluidos = new List<int>() { ConceptoSettings.Default.IdConceptoNegocioFlete };
                return conceptos.Where(c => !idConceptosExcluidos.Contains(c.id)).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Detalle_maestro>> ObtenerConceptosPagoEmpleados()
        {
            try { return (await _repositorioMaestro.ObtenerDetallesAsync(MaestroSettings.Default.IdMaestroConceptoPagoEmpleado)).ToList(); }
            catch (Exception e) { throw e; }
        }

        public async Task<List<Detalle_maestro>> ObtenerTiposServicioImpuesto()
        {
            try { return (await _repositorioMaestro.ObtenerDetallesAsync(MaestroSettings.Default.IdMaestroTipoServicioImpuesto)).ToList(); }
            catch (Exception e) { throw e; }
        }

        public async Task<List<Detalle_maestro>> ObtenerTiposProductoDeCompra()
        {
            try { return (await _repositorioMaestro.ObtenerDetallesAsync(MaestroSettings.Default.IdMaestroTipoProductoDeCompra)).ToList(); }
            catch (Exception e) { throw e; }
        }

        public async Task<List<Detalle_maestro>> ObtenerTiposBien()
        {
            try { return (await _repositorioMaestro.ObtenerDetallesAsync(MaestroSettings.Default.IdMaestroTipoBien)).ToList(); }
            catch (Exception e) { throw e; }
        }

        public List<Detalle_maestro> obtenerMarcas(int idArticulo)
        {
            try { return _repositorioMaestro.obtenerDetallesMarca(idArticulo).ToList(); }
            catch (Exception e) { throw e; }
        }

        public Detalle_maestro ObtenerMonedaPorDefecto()
        {
            try { return _repositorioMaestro.ObtenerDetalle(MaestroSettings.Default.IdDetalleMaestroMonedaSoles); }
            catch (Exception e) { throw e; }
        }

        public async Task<List<Detalle_maestro>> ObtenerUnidadesDeMedida()
        {
            try { return (await _repositorioMaestro.ObtenerDetallesAsync(MaestroSettings.Default.IdMaestroUnidadDeMedida)).ToList(); }
            catch (Exception e) { throw e; }
        }

        public async Task<List<Detalle_maestro>> ObtenerPresentaciones()
        {
            try { return (await _repositorioMaestro.ObtenerDetallesAsync(MaestroSettings.Default.IdMaestroPresentacionConcepto)).ToList(); }
            catch (Exception e) { throw e; }
        }

        public List<Valor_caracteristica_concepto> obtenercaracteristicasConceptoValor(int idArticulo)
        {
            try { return _repositorioMaestro.obtenerValorCaracteristicaConcepto(idArticulo).ToList(); }
            catch (Exception e) { throw e; }
        }

        public async Task<List<Detalle_maestro>> ObtenerEstadosTransaccion()
        {
            try { return (await _repositorioMaestro.ObtenerDetallesAsync(MaestroSettings.Default.IdMaestroEstadoTransaccion)).ToList(); }
            catch (Exception e) { throw e; }
        }

        public async Task<List<Detalle_maestro>> ObtenerAccionesTransaccion()
        {
            try { return (await _repositorioMaestro.ObtenerDetallesAsync(MaestroSettings.Default.IdMaestroAccionOperativaTransaccion)).ToList(); }
            catch (Exception e) { throw e; }
        }

        public async Task<List<Detalle_maestro>> ObtenerUnidadesDeNegocio()
        {
            try { return (await _repositorioMaestro.ObtenerDetallesAsync(MaestroSettings.Default.IdMaestroUnidadDeNegocio)).ToList(); }
            catch (Exception e) { throw e; }
        }
        public List<Menu_aplicacion> obtenerMenus()
        {
            try { return _repositorioMaestro.obtenerMenus().ToList(); }
            catch (Exception e) { throw e; }
        }

        public Detalle_maestro ObtenerDetalleMaestroDeDocumento(string codigoDocumento)
        {
            try
            {
                return _repositorioMaestro.obtenerDetalle(MaestroSettings.Default.IdMaestroDocumento, codigoDocumento);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Detalle_maestro ObtenerDetalleMaestroDeDocumentoIdentidad(string codigoDocumentoIdentidad)
        {
            try
            {
                return _repositorioMaestro.obtenerDetalle(MaestroSettings.Default.idMaestroDocumentoIdentidad, codigoDocumentoIdentidad);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #region VALIDACIONES

        public void ComprobarSiYaExisteNombreDeDetalleMaestro(int idMaestro, int idDetalleMaestro, string nombre)
        {
            try
            {
                Detalle_maestro detalle = null;
                if (idDetalleMaestro > 0)
                {
                    detalle = _repositorioMaestro.ObtenerDetalle(idDetalleMaestro);
                    //Si cambio el nombre , se tiene que consultar si ese nombre existe
                    if (nombre != detalle.nombre)
                    {
                        bool existeNombre = _repositorioMaestro.ExisteNombreDeDetalleMaestro(idMaestro, nombre, true);
                        //Si existe un registro con el mismo nombre se mostrara un mensaje
                        if (existeNombre)
                        {
                            throw new LogicaException("Ya existe un registro con el mismo nombre.");
                        }
                    }
                }
                else
                {
                    bool existeNombre = _repositorioMaestro.ExisteNombreDeDetalleMaestro(idMaestro, nombre, true);
                    if (existeNombre)
                    {
                        throw new LogicaException("Ya existe un registro con el mismo nombre.");
                    }
                }
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al comprobar si existe el nombre.", e);
            }
        }

        public bool ComprobarSiYaExisteNombreDeDetalleMaestro(int idDetalleMaesro, string nombreDetalleMaestro, Detalle_maestro detalleMaestro)
        {
            try
            {
                bool debeActualizar = false;
                if (detalleMaestro != null)
                {
                    if (detalleMaestro.es_vigente)
                    {
                        //Cuando se crea uno nuyevo
                        if (idDetalleMaesro == 0)
                        {
                            throw new LogicaException("Ya existe un registro con el mismo nombre.");
                        }
                        //Cuando se acutaliza
                        if (detalleMaestro.id != idDetalleMaesro && detalleMaestro.nombre == nombreDetalleMaestro)
                        {
                            throw new LogicaException("Ya existe un registro con el mismo nombre.");
                        }
                    }
                    else
                    {
                        //En caso de que no sea vigente y se haya encontrado tenemos que actualizar
                        debeActualizar = false;
                    }

                }

                return debeActualizar;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al comprobar si existe el nombre.", e);
            }
        }

        #endregion
        public List<ItemGenerico> ObtenerDetalleMaestroLibroElectronico()
        {
            try
            {
                List<ItemGenerico> librosElectronicos = ItemGenerico.Convert(_repositorioMaestro.ObtenerDetalles(MaestroSettings.Default.IdMaestroLibrosElectronicos, true).ToList());
                return librosElectronicos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public ItemGenerico ObtenerLibroElectronico(int idLibroElectronico)
        {
            try
            {
                ItemGenerico libroElectronico = ItemGenerico.Convert(_repositorioMaestro.ObtenerDetalle(idLibroElectronico, true));
                return libroElectronico;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #region TIPO DE CAMBIO DE DOLARES
        public Tipo_cambio ObtenerTipoCambioDolarActual()
        {
            try
            {
                DateTime fechaActual = DateTimeUtil.FechaActual().Date;
                Tipo_cambio tipoCambioActual = _repositorioMaestro.ObtenerTipoCambio(MaestroSettings.Default.IdDetalleMaestroMonedaDolares, fechaActual);
                return tipoCambioActual;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public OperationResult GuardarTipoCambioDolarActual(Tipo_cambio tipoCambio)
        {
            try
            {
                if (tipoCambio.fecha == DateTime.MinValue)
                {
                    tipoCambio.fecha = DateTimeUtil.FechaActual().Date;
                    tipoCambio.idMoneda = MaestroSettings.Default.IdDetalleMaestroMonedaDolares;
                }
                return _repositorioMaestro.GuardarTipoCambio(tipoCambio);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
        public async Task<List<ItemGenerico>> ObtenerTiposGrupoClientes()
        {
            try
            {
                var tiposGrupoClientes = ItemGenerico.Convert((await _repositorioMaestro.ObtenerDetallesAsync(MaestroSettings.Default.IdMaestroTiposGrupoClientes)).ToList());
                return tiposGrupoClientes;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener los tipos de grupo de clientes", e);
            }
        }
        public async Task<List<ItemGenerico>> ObtenerClasificacionesGrupoClientes()
        {
            try
            {
                var tiposGrupoClientes = ItemGenerico.Convert((await _repositorioMaestro.ObtenerDetallesAsync(MaestroSettings.Default.IdMaestroClasificacionesGrupoClientes)).ToList());
                return tiposGrupoClientes;
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener las clasificaciones de grupo de clientes", e);
            }
        }
    }
}

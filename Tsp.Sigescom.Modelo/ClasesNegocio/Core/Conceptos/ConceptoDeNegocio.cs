using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class ConceptoDeNegocio
    {
        private Concepto_negocio conceptoDeNegocio;


        public ConceptoDeNegocio(Concepto_negocio conceptoNegocio)
        {
            this.conceptoDeNegocio = conceptoNegocio;
        }
        public ConceptoDeNegocio(int idRol, string codigo, string codigoBarra, string nombre, string propiedades, int idUnidadMedidaPrimaria, int idModelo, bool esVigente)
        {

        }
        public Concepto_negocio Concepto_negocio()
        {
            return this.conceptoDeNegocio;
        }
        public int Id
        {
            get { return this.conceptoDeNegocio.id; }
        }

        public string Codigo
        {
            get { return this.conceptoDeNegocio.codigo; }
        }

        public string CodigoBarra
        {
            get { return this.conceptoDeNegocio.codigo_barra; }
        }

        public string Nombre
        {
            get { return this.conceptoDeNegocio.nombre; }
        }

        public string Sufijo
        {
            get { return this.conceptoDeNegocio.sufijo; }
        }

        public string CodigoDigemid
        {
            get { return this.conceptoDeNegocio.codigo_negocio1; }
        }

        public int IdConceptoBasico
        {
            get { return this.conceptoDeNegocio.id_concepto_basico; }
        }

        public string NombreBasico
        {
            get { return this.conceptoDeNegocio.Detalle_maestro4.nombre; }
        }

        public decimal CantidadPresentacion
        {
           get { return this.conceptoDeNegocio.contenido; }
        }

        /// <summary>
        /// Retorna true si es un bien
        /// </summary>
        public bool EsBien
        {
            get { return this.conceptoDeNegocio.Detalle_maestro4.valor == "1"; }
        }

        public bool TieneCaracteristicasPropias()
        {
            bool tieneCaracteristicasPropia = this.conceptoDeNegocio.Detalle_maestro4.Caracteristica_concepto != null ?
                                                  conceptoDeNegocio.Detalle_maestro4.Caracteristica_concepto
                                                 .Where(cc => cc.Detalle_maestro1.id_maestro == MaestroSettings.Default.IdMaestroCaracteristicaPropiaConcepto)
                                                 .Any() : false ;
            return tieneCaracteristicasPropia;
        }


        public List<Detalle_maestro> NombresCaracteristicasPropias()
        {

                List<Detalle_maestro> caracteristicasPropias = conceptoDeNegocio.Detalle_maestro4.Caracteristica_concepto != null ?
                                                                            conceptoDeNegocio.Detalle_maestro4.Caracteristica_concepto
                                                                            .Where(cc => cc.Detalle_maestro1.id_maestro == MaestroSettings.Default.IdMaestroCaracteristicaPropiaConcepto)
                                                                            .Select(cc => cc.Detalle_maestro1)
                                                                            .ToList() : null;
                return caracteristicasPropias;
        }

        public List<ValorDetalleMaestroDetalleTransaccion> ValoresCaracteristicasPropias ()
        {
         
                List<Valor_detalle_maestro_detalle_transaccion> valoresCaracteristicasPropias = conceptoDeNegocio
                                                                            .Detalle_maestro4.Caracteristica_concepto
                                                                            .Where(cc => cc.Detalle_maestro1.id_maestro == MaestroSettings.Default.IdMaestroCaracteristicaPropiaConcepto)
                                                                            .Select(cc => cc.Detalle_maestro1).SelectMany( dm => dm.Valor_detalle_maestro_detalle_transaccion).ToList();
                return ValorDetalleMaestroDetalleTransaccion.Convert_(valoresCaracteristicasPropias);
            
        }

        /// <summary>
        /// devuelve el sytock total de un concepto de negocio, para lo cual se suma el stock de cada entidad interna
        /// </summary>
        /// <returns></returns>
        public decimal Stock()
        {
            var stock = this.conceptoDeNegocio.Existencia.Sum(cn=>cn.existencia1);
            return  stock/*!=null?stock.existencia1:0*/;
        }

        /// <summary>
        /// devuelve el stock de un concepto de negocio, de una entidad interna especifica
        /// </summary>
        /// <param name="idEntidadInterna"></param>
        /// <returns></returns>
        public decimal Stock(int idEntidadInterna)
        {
            var stock = this.conceptoDeNegocio.Existencia.SingleOrDefault(cn => cn.id_punto_atencion== idEntidadInterna);
            return stock != null ? stock.existencia1 : 0;
        }

        public bool HayFoto()
        {
            return this.conceptoDeNegocio.id_foto != null;
        }

        public byte[] Foto()
        {
            if (HayFoto())
            {
                return this.conceptoDeNegocio.Foto.imagen;

            }
            return new byte[0];
        }

        /// <summary>
        /// Devuelve el registro de existencia o stock del producto
        /// </summary>
        /// <returns></returns>
        public Existencia Existencia()
        {
            return this.conceptoDeNegocio.Existencia.FirstOrDefault();
        }

        public int IdExistencia()
        {
            var existencia = this.conceptoDeNegocio.Existencia.FirstOrDefault();
            return existencia != null ? existencia.id : 0;
        }

        public int IdActorExistencia()
        {
            var existencia = this.conceptoDeNegocio.Existencia.FirstOrDefault();
            return existencia != null ? existencia.id_punto_atencion : 0;
        }

        public string NombreEntidadInterna(int idEntidadInterna)
        {
            var entidadInterna = this.conceptoDeNegocio.Existencia.SingleOrDefault(cn => cn.id_punto_atencion == idEntidadInterna);
            //string nombre = this.conceptoDeNegocio.Existencia.SingleOrDefault(cn => cn.id_punto_atencion == idEntidadInterna).Actor_negocio.PrimerNombre;
            return entidadInterna != null ? entidadInterna.Actor_negocio.PrimerNombre : "";
        }

        public byte[] VersionFila()
        {
            var version = this.conceptoDeNegocio.Existencia.FirstOrDefault();
            return version != null ? version.version_fila : null;
        }


        public decimal StockMinimo { get { return conceptoDeNegocio.stock_minimo; } }


        public DetalleGenerico ConceptoBasico()
        {
            return new DetalleGenerico(this.conceptoDeNegocio.Detalle_maestro4);
        }

        public Rol RolDeProducto()
        {
            return conceptoDeNegocio.Concepto_negocio_rol.SingleOrDefault().Rol;
        }

        public List<DetalleGenerico> Categorias()
        {
            return DetalleGenerico.convert(this.conceptoDeNegocio.Detalle_maestro4.Categoria_concepto1.Select(cc=>cc.Detalle_maestro1).ToList());
        }

        public DetalleGenerico Presentacion()
        {
            return new DetalleGenerico(this.conceptoDeNegocio.Detalle_maestro3);
        }

        public DetalleGenerico UnidadMedidaPresentacion()
        {
            return new DetalleGenerico(this.conceptoDeNegocio.Detalle_maestro1);
        }

        public DetalleGenerico UnidadMedidaComercial()
        {
            return new DetalleGenerico(this.conceptoDeNegocio.Detalle_maestro);
        }

        public DetalleGenerico UnidadMedidaReferencial()
        {
            return new DetalleGenerico(this.conceptoDeNegocio.Detalle_maestro2);
        }

        public List<int> IdsValorCaracteristicas()
        {
            return this.conceptoDeNegocio.Valor_caracteristica_concepto_negocio.Select(vcn => vcn.id_valor_caracteristica).ToList();
        }

        public List<int> IdsRolesAdicionales()
        {
            return this.conceptoDeNegocio.Concepto_negocio_rol.Select(cnr => cnr.id_rol).ToList();
        }

        public List<Valor_caracteristica_concepto_negocio> ValoresDeCaracteristicas()
        {
            return this.conceptoDeNegocio.Valor_caracteristica_concepto_negocio.ToList();
        }

        /// <summary>
        /// devuelve los precios vigentes del producto en un centro de atención
        /// </summary>
        /// <param name="idPuntoAtencion"></param>
        /// <returns></returns>
        public List<Precio> Precios(int idPuntoAtencion)
        {
            return this.conceptoDeNegocio.Precio1.Where(p => p.id_actor_negocio == idPuntoAtencion && p.id_tipo == MaestroSettings.Default.IdDetalleMaestroTipoPrecioPrecio && p.es_vigente == true).ToList();
        }

        /// <summary>
        /// retorna los ultimos 5 precios de compra sinimportar el centro de atencion para el cual se realizó la compra
        /// </summary>
        /// <returns></returns>
        public List<PrecioDeCompra> Ultimos5PreciosCompra()
        {
            return PrecioDeCompra.obtener(this.conceptoDeNegocio.Detalle_transaccion.Where(dt => dt.Transaccion.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenDeCompra).OrderByDescending(dt => dt.id).Take(5).ToList());
        }

        /// <summary>
        /// devuelve los ultimos 5 precios de venta de un producto, sin importar el punto de atención
        /// </summary>
        /// <returns></returns>
        public List<PrecioDeVenta> Ultimos5PreciosDeVenta()
        { 
            return PrecioDeVenta.obtener(this.conceptoDeNegocio.Precio1.Where(p => p.id_tipo == MaestroSettings.Default.IdDetalleMaestroTipoPrecioPrecio).OrderByDescending(p => p.id).Take(5).ToList());
        }

        /// <summary>
        /// devuelve el precio del producto en la tarifa por defecto: precio publico o tarifa normal par aun punto de atención específico
        /// 
        /// </summary>
        /// <param name="idPuntoAtencion"></param>
        /// <returns></returns>
        public decimal PrecioVentaNormal(int idPuntoAtencion)
        {
            try
            {

                var precio = this.conceptoDeNegocio.Precio1.SingleOrDefault(p => p.id_actor_negocio == idPuntoAtencion && p.id_tipo == MaestroSettings.Default.IdDetalleMaestroTipoPrecioPrecio && p.id_tarifa_d == MaestroSettings.Default.IdDetalleMaestroTarifaNormal && p.es_vigente == true);
                return precio != null ? precio.valor : 9999999999;
            }
            catch (Exception e)
            {
                throw new ModeloException("Error al obtener precio de venta", e);
            }
        }

        /// <summary>
        /// devuelve el ultimo precio de compra sinimportar el punto de atencion para el que se realiza la compra
        /// </summary>
        /// <returns></returns>
        public decimal PrecioCompra()
        {

            var precio = this.conceptoDeNegocio.Detalle_transaccion.Where(dt => dt.Transaccion.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenDeCompra).LastOrDefault();
            return precio != null ? precio.precio_unitario : 0;
        }

        public Precio Descuento()
        {
            return this.conceptoDeNegocio.Precio1.SingleOrDefault(p => p.id_tipo == MaestroSettings.Default.IdDetalleMaestroTipoPrecioDescuento && p.es_vigente == true);
        }

        public Precio Bonificacion()
        {
            return this.conceptoDeNegocio.Precio1.SingleOrDefault(p => p.id_tipo == MaestroSettings.Default.IdDetalleMaestroTipoPrecioBonificacion && p.es_vigente == true);
        }

        //public List<Caracteristica> CaracteristicasPropias()
        //{
        //    throw new NotImplementedException();
        //}

        //public List<Caracteristica> CaracteristicasComunes()
        //{
        //    List<Caracteristica> resultado = new List<Caracteristica>();
        //    foreach (var item in this.conceptoDeNegocio.Valor_caracteristica_concepto_negocio)
        //    {
        //        resultado.Add(new Caracteristica(item.Valor_caracteristica.Detalle_maestro.nombre, item.Valor_caracteristica.valor));
        //    }
        //    return resultado;
        //}

        //public string CadenaStringDeCaracteristicasPropias()
        //{
        //    throw new NotImplementedException();
        //}

        //public string CadenaStringDeCaracteristicasComunes()
        //{
        //    string resultado = "";
        //    foreach (var item in this.conceptoDeNegocio.Valor_caracteristica_concepto_negocio)
        //    {
        //        Caracteristica caracteristica = new Caracteristica(item.Valor_caracteristica.Detalle_maestro.nombre, item.Valor_caracteristica.valor);
        //        resultado = resultado + " | " + caracteristica.Nombre + ":" + caracteristica.Valor;
        //    }
        //    return resultado;
        //}

        public static List<ConceptoDeNegocio> Convert_(List<Concepto_negocio> conceptosDeNegocio)
        {
            List<ConceptoDeNegocio> productos = new List<ConceptoDeNegocio>();
            foreach (var conceptoDeNegocio in conceptosDeNegocio)
            {
                productos.Add(new ConceptoDeNegocio(conceptoDeNegocio));
            }
            return productos;
        }

    }
}

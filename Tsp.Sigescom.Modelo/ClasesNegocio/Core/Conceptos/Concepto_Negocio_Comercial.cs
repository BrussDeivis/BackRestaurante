using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    /// <summary>
    /// Se utiliza en la bandeja de mercaderias para mostrar los bienes y servicios
    /// </summary>
    public class Concepto_Negocio_Comercial
    {
        //Concepto negocio
        private int id;
        private string nombre;
        private string codigo;
        private string codigoBarra;
        private string sufijo;
        //Concepto basico
        private int idConceptoBasico;
        private string nombreConceptoBasico;
        private string valorConceptoBasico;
        //Caracteristicas comunes
        private IEnumerable<ValorCaracteristica> valoresCaracteristicas;
        //Presentacion
        private string nombrePresentacion;
        private decimal contenido;
        private string codigoUnidadMedidaPresentacion;
        private string codigoUnidadMedidaComercial;
        private string codigoUnidadMedidaReferencial;
        private string unidadMedidaPresentacion;
        private string unidadMedidaComercial;
        private string unidadMedidaReferencial;
        //Precios
        private IEnumerable<Precio_Concepto_Negocio_Comercial> precios;
        //Complementos
        private IEnumerable<Complemento_Concepto_Negocio_Comercial> complementos;
        //Stock y precios
        private decimal stock;
        private decimal? precioVenta;
        private decimal? costoUnitario;
        private IEnumerable<ValorDetalleMaestroDetalleTransaccion> valoresCaracteristicasPropios;
        public Concepto_Negocio_Comercial()
        {
            this.Complementos = new List<Complemento_Concepto_Negocio_Comercial>();
        }

        public int Id { get => id; set => id = value; }
        public string Codigo { get => codigo; set => codigo = value; }
        public string CodigoBarra { get => codigoBarra; set => codigoBarra = value; }
        public string Sufijo { get => sufijo; set => sufijo = value; }
        public int IdConceptoBasico { get => idConceptoBasico; set => idConceptoBasico = value; }
        public string NombreConceptoBasico { get => nombreConceptoBasico; set => nombreConceptoBasico = value; }
        public string ValorConceptoBasico { get => valorConceptoBasico; set => valorConceptoBasico = value; }
        public IEnumerable<ValorCaracteristica> ValoresCaracteristicas { get => valoresCaracteristicas; set => valoresCaracteristicas = value; }
        public string NombrePresentacion { get => nombrePresentacion; set => nombrePresentacion = value; }
        public decimal Contenido { get => contenido; set => contenido = value; }
        public string CodigoUnidadMedidaPresentacion { get => codigoUnidadMedidaPresentacion; set => codigoUnidadMedidaPresentacion = value; }
        public string CodigoUnidadMedidaComercial { get => codigoUnidadMedidaComercial; set => codigoUnidadMedidaComercial = value; }
        public string CodigoUnidadMedidaReferencial { get => codigoUnidadMedidaReferencial; set => codigoUnidadMedidaReferencial = value; }
        public string UnidadMedidaPresentacion { get => unidadMedidaPresentacion; set => unidadMedidaPresentacion = value; }
        public string UnidadMedidaComercial { get => unidadMedidaComercial; set => unidadMedidaComercial = value; }
        public string UnidadMedidaReferencial { get => unidadMedidaReferencial; set => unidadMedidaReferencial = value; }
        public IEnumerable<Precio_Concepto_Negocio_Comercial> Precios { get => precios; set => precios = value; }
        public IEnumerable<Complemento_Concepto_Negocio_Comercial> Complementos { get => complementos; set => complementos = value; }
        public decimal Stock { get => stock; set => stock = value; }
        public decimal? PrecioVenta { get => precioVenta; set => precioVenta = value; }
        public decimal? CostoUnitario { get => costoUnitario; set => costoUnitario = value; }
        public bool EsBien { get => ValorConceptoBasico == "1"; set => ValorConceptoBasico = value ? "1" : "0"; }
        //public string Presentacion { get => NombrePresentacion + " " + Contenido + " " + CodigoUnidadMedidaPresentacion; }
        public string NombreUnidadMedidaComercial
        {
            get => "X " + UnidadMedidaComercial;
        }
        public string Presentacion
        {
            get => NombrePresentacion + " " + Contenido + " " + CodigoUnidadMedidaPresentacion + " ";
        }

        public string ValoresCaracteristicasComunes
        {
            get => ValoresCaracteristicas != null ? ValoresCaracteristicas.Aggregate<ValorCaracteristica, string>("", (str, vc) => str += vc.Valor + " ") : "";
        }
        public IEnumerable<ValorDetalleMaestroDetalleTransaccion> ValoresCaracteristicasPropios { get => valoresCaracteristicasPropios; set => valoresCaracteristicasPropios = value; }

        public string NombreDeTarifasYPrecios
        {
            get => Precios != null ? Precios
           .Aggregate<Precio_Concepto_Negocio_Comercial, string>
           ("", (str, p) => str += p.Codigo + " " + p.Valor.ToString("N" + AplicacionSettings.Default.NumeroDecimalesEnPrecio) + ", ").TrimEnd(new Char[] { ',', ' ' }) : "";
        }
        public string CodigoBarraNombreConceptoNegocioNombreDeTarifasYPrecios
        {
            get => Precios != null ? Precios
           .Aggregate<Precio_Concepto_Negocio_Comercial, string>
           (CodigoBarra + " | " + NombreConceptoBasico + " | ", (str, p) => str += p.Codigo + " " + p.Valor + " | ") : "";
        }
        public string NombreConcepto
        {
            get
            {
                return nombre;
            }
        }
        public string Nombre
        {
            get
            {

                return (string.IsNullOrEmpty(codigoBarra) ? "" : (codigoBarra + " | ")) + nombre + (string.IsNullOrEmpty(NombreDeTarifasYPrecios) ? "" : (" | " + NombreDeTarifasYPrecios));
            }
            set
            {
                nombre = value;
            }
        }
        public string SoloNombre
        {
            get => nombre;
        }

        public string NombreParaDetalle
        {
            get => (string.IsNullOrEmpty(codigoBarra) ? "" : (codigoBarra + " | ")) + nombre;
        }
        public string NombreConceptoNegociorReflection(string marcaraConceptoNegocio, Dictionary<char, string> valoresMascaraConceptoNegocio)
        {
            var mascaraList = marcaraConceptoNegocio.ToCharArray();
            string nombreMascara = "";

            foreach (var item in mascaraList)
            {
                var valueProperty = ValorAtributoParaMascaraConceptoNegocio(valoresMascaraConceptoNegocio.SingleOrDefault(mcn => mcn.Key == item).Value);
                if (valueProperty != "")
                {
                    nombreMascara += valueProperty + " | ";
                }
            }
            return nombreMascara.TrimEnd(new Char[] { '|', ' ' });
        }

        public string ValorAtributoParaMascaraConceptoNegocio(string nameAtributte)
        {
            Type tModelType = this.GetType();
            var arrayPropertyInfos = tModelType.GetProperties();
            string valueProperty = "";

            foreach (System.Reflection.PropertyInfo property in arrayPropertyInfos)
            {
                if (property.Name == nameAtributte)
                {
                    valueProperty = property.GetValue(this) != null ? property.GetValue(this).ToString() : "";
                    return valueProperty;
                }
            }
            return valueProperty;
        }

        public static Concepto_Negocio_Comercial EstablecerNombreSegunLaMascara(Concepto_Negocio_Comercial conceptoNegocioComercial, string marcaraConceptoNegocio, Dictionary<char, string> valoresMascaraConceptoNegocio)
        {
            conceptoNegocioComercial.Nombre = conceptoNegocioComercial.NombreConceptoNegociorReflection(marcaraConceptoNegocio, valoresMascaraConceptoNegocio);
            return conceptoNegocioComercial;
        }
        public static List<Concepto_Negocio_Comercial> EstablecerNombreSegunLaMascara(List<Concepto_Negocio_Comercial> conceptosNegociosComerciales, string marcaraConceptoNegocio, Dictionary<char, string> valoresMascaraConceptoNegocio)
        {

            foreach (var item in conceptosNegociosComerciales)
            {
                item.Nombre = item.NombreConceptoNegociorReflection(marcaraConceptoNegocio, valoresMascaraConceptoNegocio);
            }
            return conceptosNegociosComerciales;
        }

        public List<Filtro> Filtros
        {
            get
            {
                var nombresFiltros = new List<Filtro>();
                nombresFiltros.Add(new Filtro(" CONCEPTO BÁSICO", NombreConceptoBasico, 1));
                nombresFiltros.Add(new Filtro(" CODIGO BARRA", CodigoBarra, 1));
                if (ValoresCaracteristicas != null)
                {
                    foreach (var item in ValoresCaracteristicas)
                    {
                        nombresFiltros.Add(new Filtro(item.NombreCaracteristica, item.Valor, 1));
                    }
                }
                return nombresFiltros;
            }
        }

        public List<ValorCaracteristica> CaracteristicasComunes()//Todo:vanvan
        {
            return ValoresCaracteristicas.ToList();
        }
        public List<ValorDetalleMaestroDetalleTransaccion> CaracteristicasPropias()//Todo:vanvan
        {
            List<ValorDetalleMaestroDetalleTransaccion> CaracteristicasPropias = new List<ValorDetalleMaestroDetalleTransaccion>();
            return CaracteristicasPropias;
        }

        public string CadenaStringDeCaracteristicasComunes()
        {
            string resultado = "";
            foreach (var item in this.valoresCaracteristicas)
            {
                resultado = resultado + " | " + item.NombreCaracteristica + ":" + item.Valor;
            }
            return resultado;
        }

        public string CadenaStringDeCaracteristicasPropias()
        {
            throw new NotImplementedException();
        }

    }

    public class Concepto_Negocio_Comercial_
    {
        public int Id { get; set; }
        public int IdFamilia { get; set; }
        public string CodigoBarra { get; set; }
        public string Nombre { get; set; }
        public string NombreDetalle
        {
            get
            {
                return (AplicacionSettings.Default.MostrarCodigoBarraEnDetalleAlRealizarOperaciones ? (String.IsNullOrEmpty(CodigoBarra) ? "" : (CodigoBarra + " | ")) : "") + Nombre;
            }
        }
        private string valorConceptoBasico;
        public string ValorConceptoBasico { set => valorConceptoBasico = value; }
        public bool EsBien { get => valorConceptoBasico == "1"; }
        //public Complemento_Concepto_Negocio_Comercial Complemento { get; set; }
        public decimal Stock { get; set; }
        public IEnumerable<Precio_Concepto_Negocio_Comercial> Precios { get; set; }
        public Concepto_Negocio_Comercial_()
        {

        }
    }

    public class Selector_Concepto_Negocio_Comercial
    {
        private bool sinControlStock;
        public int Id { get; set; }
        public string CodigoBarra { get; set; }
        public string Precio { get; set; }
        public decimal? Stock { get; set; }
        public bool EsBien { get; set; }
        /// <summary>
        /// sin codigo de barra ni stock ni precio.
        /// </summary>
        public string SoloNombre { get; set; }
        public bool SinControlStock { set => sinControlStock = value; }
        public bool MostrarStockYPrecio { get; set; }


        public string Nombre
        {
            get
            {
                return (String.IsNullOrEmpty(CodigoBarra) ? "" : (CodigoBarra + " | ")) + SoloNombre + (MostrarStockYPrecio ? this.StockPrecio() : "");
            }
        }

        public string StockPrecio()
        {
            return (sinControlStock ? "" : (EsBien ? (" | STOCK: " + ((decimal)Stock).ToString("N" + AplicacionSettings.Default.NumeroDecimalesEnCantidad)) : "")) + " | " + (Precio ?? "SIN PRECIO");
        }

        public bool StockCero { get => !sinControlStock && EsBien && (Stock <= 0); }

        public Selector_Concepto_Negocio_Comercial()
        {
        }
    }

    public class Familia_Concepto_Comercial
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Esbien { get; set; }
        public Familia_Concepto_Comercial()
        {
        }
    }
    public class Filtro
    {
        private string nombre;
        private string valor;
        private int cantidad;

        public Filtro()
        {
        }

        public Filtro(string nombre, string valor, int cantidad)
        {
            this.nombre = nombre;
            this.valor = valor;
            this.cantidad = cantidad;
        }

        public string Nombre { get => nombre; set => nombre = value; }
        public string Valor { get => valor; set => valor = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }

    }



}
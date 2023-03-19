using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.Partial
{
    /// <summary>
    /// Se utiliza en el modulo de compra
    /// </summary>
    public class Concepto_Negocio_Comercial_Venta
    {
        //concepto negocio
        private int id;
        private string nombre;
        private string codigoBarra;
        private string sufijo;
        //concepto basico
        private int idConceptoBasico;
        private string nombreConceptoBasico;
        private string valorConceptoBasico;

        //caracteristicas comunes
        private IEnumerable<ValorCaracteristica> valoresCaracteristicas;
        //Presentacion
        private string nombrePresentacion;
        private decimal contenido;
        private string unidadMedidaPresentacion;
        //Unidadd de medida comercial
        private string unidadMedidaComercial;
        //Unidadd de medida referencial
        private string unidadMedidaReferencial;
        //precios
        private IEnumerable<Precio_Concepto_Negocio_Comercial> precios;
        //complementos
        private IEnumerable<Complemento_Concepto_Negocio_Comercial> complementos;

        public Concepto_Negocio_Comercial_Venta()
        {
            this.Complementos = new List<Complemento_Concepto_Negocio_Comercial>();
        }

        public int Id { get => id; set => id = value; }
        public string CodigoBarra { get => codigoBarra; set => codigoBarra = value; }
        public int IdConceptoBasico { get => idConceptoBasico; set => idConceptoBasico = value; }
        public string NombreConceptoBasico { get => nombreConceptoBasico; set => nombreConceptoBasico = value; }
        public string ValorConceptoBasico { get => valorConceptoBasico; set => valorConceptoBasico = value; }
        public string Sufijo { get => sufijo; set => sufijo = value; }
        public IEnumerable<ValorCaracteristica> ValoresCaracteristicas { get => valoresCaracteristicas; set => valoresCaracteristicas = value; }
        public string NombrePresentacion { get => nombrePresentacion; set => nombrePresentacion = value; }
        public decimal Contenido { get => contenido; set => contenido = value; }
        public string UnidadMedidaPresentacion { get => unidadMedidaPresentacion; set => unidadMedidaPresentacion = value; }
        public string UnidadMedidaComercial { get => unidadMedidaComercial; set => unidadMedidaComercial = value; }
        public string UnidadMedidaReferencial { get => unidadMedidaReferencial; set => unidadMedidaReferencial = value; }
        public IEnumerable<Precio_Concepto_Negocio_Comercial> Precios { get => precios; set => precios = value; }
        public IEnumerable<Complemento_Concepto_Negocio_Comercial> Complementos { get => complementos; set => complementos = value; }

        #region GENERACION DEL NOMBRE DEL CONCEPTO DE NEGOCIO POR MASCARA

        public string ValoresCaracteristicasComunes
        {
            get => ValoresCaracteristicas != null  ? ValoresCaracteristicas.Aggregate<ValorCaracteristica, string>("", (str, vc) => str += vc.Valor + " ") : "";
        }

        public string Presentacion
        {
            get => NombrePresentacion + " " + Contenido + " " + UnidadMedidaPresentacion + " ";
        }

        public string NombreUnidadMedidaComercial
        {
            get => "X " + UnidadMedidaComercial;
        }

        public string NombreDeTarifasYPrecios
        {
            get => Precios != null  ? Precios
           .Aggregate<Precio_Concepto_Negocio_Comercial, string>
           ("", (str, p) => str += p.Codigo + " " + p.Valor + ", ").TrimEnd(new Char[] { ',', ' ' }) : " ";
        }

        public string CodigoBarraNombreConceptoNegocioNombreDeTarifasYPrecios
        {
            get => Precios != null ? Precios
           .Aggregate<Precio_Concepto_Negocio_Comercial, string>
           (CodigoBarra + " | " + NombreConceptoBasico + " | ", (str, p) => str += p.Codigo + " " + p.Valor + " | ") : "";
        }

        public string Nombre
        {
            get
            {
                //var ordenMascara = AplicacionSettings.Default.MascaraConceptoNegocioVenta;
                //var mascaraList = ordenMascara.ToCharArray();
                //string nombreMascara = "";
                //foreach (var item in mascaraList)
                //{
                //    var valueProperty = ValorAtributoParaMascaraConceptoNegocio(Diccionario.ValoresMascaraConceptoNegocioVenta.SingleOrDefault(mcn => mcn.Key == item).Value);
                //    if (valueProperty != "")
                //    {
                //        nombreMascara += valueProperty + " | ";
                //    }
                //}

                //return nombreMascara.TrimEnd(new Char[] { '|', ' ' });
                return nombre + " | " + NombreDeTarifasYPrecios;
            }

            set
            {
                nombre = value;
            }
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

        #endregion

        public bool EsBien
        {
            get => ValorConceptoBasico == "1";
        }
    }

}

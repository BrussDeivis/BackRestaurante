using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class Stock
    {
        private Existencia existencia;
              
        public Stock(Existencia existencia)
        {
            this.existencia = existencia;
        }

        //public Stock(int idRol, string codigo, string codigoBarra, string nombre, string propiedades,
        //                int idUnidadMedidaPrimaria, int idModelo, bool esVigente)
        //{
        //}

        public ConceptoDeNegocio producto()
        {
            return new ConceptoDeNegocio(this.existencia.Concepto_negocio);
        }
        public int Id
        {
            get { return this.existencia.id; }
        }

        public string CodigoBarraProducto
        {
            get { return this.existencia.Concepto_negocio.codigo_barra; }
        }
        public int IdProducto
        {
            get { return this.existencia.id_concepto_negocio; }
        }
        public int IdAlmacen
        {
            get { return this.existencia.id_punto_atencion; }
        }
        public string NombreProducto
        {
            get { return this.existencia.Concepto_negocio.nombre; }
        }
        public string NombreAlmacen
        {
            get { return this.existencia.Actor_negocio.PrimerNombre; }
        }
        public decimal Valor
        {
            get { return this.existencia.existencia1; }
        }
         
        public static List<Stock> Convert_(List<Existencia> existencias )
        {
            List<Stock> stocks = new List<Stock>();
            foreach (var existencia in existencias)
            {
                stocks.Add(new Stock(existencia));
            }
            return stocks;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class ProductoParaVentaConFiltrosViewModel
    {

        public List<Concepto_Negocio_Comercial> ConceptosNegociosComerciales { get; set; }
        public List<FiltroOpcion> FiltrosConOpciones { get; set; }


        public ProductoParaVentaConFiltrosViewModel()
        {
        }

        public ProductoParaVentaConFiltrosViewModel(List<Concepto_Negocio_Comercial> conceptosNegociosComerciales, List<FiltroOpcion> filtrosConOpciones)
        {
            ConceptosNegociosComerciales = conceptosNegociosComerciales;
            FiltrosConOpciones = filtrosConOpciones;
        }

        public static ProductoParaVentaConFiltrosViewModel Convert(List<Concepto_Negocio_Comercial> conceptosNegociosComerciales)
        {
            List<FiltroOpcion> filtrosConOpciones = new List<FiltroOpcion>();
            //Obtener nombres de caracteristicas unicas
            var nombresFiltrosUnicos = conceptosNegociosComerciales.SelectMany(cnc => cnc.Filtros).OrderBy(f => f.Nombre).Select(f => f.Nombre).Distinct();
            foreach (var item in nombresFiltrosUnicos)
            {
                //Obtener valos unicos segun el nombre de caracteristica

                var opciones = conceptosNegociosComerciales.SelectMany(cnc => cnc.Filtros)
                                                                         .Where(f => f.Nombre == item)
                                                                         .GroupBy(f => f.Valor)
                                                                         .Select(o => new Opcion()
                                                                         {
                                                                             Valor = o.Key,
                                                                             Cantidad = o.Sum( c => c.Cantidad),
                                                                             EsSeleccionado = false,
                                                                             
                                                                         }).Distinct().ToList();

                filtrosConOpciones.Add(new FiltroOpcion(item, opciones.OrderBy(o => o.Valor).ToList()));
            }


            return new ProductoParaVentaConFiltrosViewModel(conceptosNegociosComerciales, filtrosConOpciones);
        }

        public class FiltroOpcion {
            private string nombre;
            private List<Opcion> opciones;

            public FiltroOpcion(string nombre, List<Opcion> opciones)
            {
                this.nombre = nombre;
                this.opciones = opciones;
            }

            public string Nombre { get => nombre; set => nombre = value; }
            public List<Opcion> Opciones { get => opciones; set => opciones = value; }
        }

        public class Opcion
        {
            
            private string valor;
            private bool esSeleccionado;
            private int cantidad;
            public Opcion()
            {

            }

            public Opcion(string valor, bool esSeleccionado, int cantidad)
            {
                this.valor = valor;
                this.esSeleccionado = esSeleccionado;
                this.cantidad = cantidad;
            }

            public string Valor { get => valor; set => valor = value; }
            public bool EsSeleccionado { get => esSeleccionado; set => esSeleccionado = value; }
            public int Cantidad { get => cantidad; set => cantidad = value; }
        }

    }

}
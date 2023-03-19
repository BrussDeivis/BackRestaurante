using System;
using System.Collections.Generic;
using System.Linq;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class ConceptoComercial
    {
        private Concepto_negocio conceptoDeNegocio;

        public ConceptoComercial()
        {

        }
        public ConceptoComercial(Concepto_negocio conceptoDeNegocio)
        {
            this.conceptoDeNegocio = conceptoDeNegocio;
        }

        public static List<ConceptoComercial> Convert(List<Concepto_negocio> conceptosDeNegocio){
            var conceptos = new List<ConceptoComercial>();

            foreach (var conceptoDeNegocio in conceptosDeNegocio)
            {
                conceptos.Add(new ConceptoComercial(conceptoDeNegocio));
            }
            return conceptos;
        }
        public string Codigo
        {
            get { return this.conceptoDeNegocio.codigo; }
        }

        public string Codigo_Barra
        {
            get { return this.conceptoDeNegocio.codigo_barra; }
        }

        public int Id
        {
            get { return this.conceptoDeNegocio.id; }
        }

        public string Nombre
        {
            get { return this.conceptoDeNegocio.nombre; }
        }

        public string NombreBasico
        {
            get { return this.conceptoDeNegocio.Detalle_maestro4.nombre; }
        }

       

        public string Presentacion
        {
            get { return this.conceptoDeNegocio.Detalle_maestro3.nombre; }
        }

        //public Rol Rol
        //{
        //    get { return this.conceptoDeNegocio.Rol; }
        //}        

        public bool Vigente
        {
            get { return this.conceptoDeNegocio.es_vigente; }
        }



        public List<ValorDetalleMaestroDetalleTransaccion> CaracteristicasPropias()
        {
            throw new NotImplementedException();
        }

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
    }
}

using System.Collections.Generic;
using System.Linq;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class ConceptoDeServicio
    {
        private Concepto_negocio conceptoDeNegocio;

        public ConceptoDeServicio()
        {

        }
        public ConceptoDeServicio(Concepto_negocio conceptoDeNegocio)
        {
            this.conceptoDeNegocio = conceptoDeNegocio;
        }



        public ConceptoDeServicio(int idServicio, string codigo, int idModelo)
        {
            //conceptoDeNegocio = new Concepto_negocio(    idServicio, codigo, "", "", "", "", ConceptoSettings.Default.idUnidadMedidaPorDefecto, ConceptoSettings.Default.idPresentacionPorDefecto, 0, ConceptoSettings.Default.idUnidadMedidaPorDefecto, null, null, true, ConceptoSettings.Default.idUnidadMedidaPorDefecto);
        }


   
        public ConceptoDeServicio(int idServicio, string codigo, int idModelo, string nombre)
        {
            //conceptoDeNegocio = new Concepto_negocio(idServicio, codigo, "", nombre, "", "", ConceptoSettings.Default.idUnidadMedidaPorDefecto, ConceptoSettings.Default.idPresentacionPorDefecto, 0, ConceptoSettings.Default.idUnidadMedidaPorDefecto, null, null, true, ConceptoSettings.Default.idUnidadMedidaPorDefecto);
        }
        public List<PrecioDeServicio> PreciosVigentes()
        {
            return PrecioDeServicio.convert(this.conceptoDeNegocio.Precio.Where(p => p.es_vigente == true).ToList());
        }
        public static List<ConceptoDeServicio> convert(List<Concepto_negocio> conceptosDeNegocio)
        {
            var conceptos = new List<ConceptoDeServicio>();

            foreach (var conceptoDeNegocio in conceptosDeNegocio)
            {
                conceptos.Add(new ConceptoDeServicio(conceptoDeNegocio));
            }
            return conceptos;
        }

        public string Codigo
        {
            get { return this.conceptoDeNegocio.codigo; }
        }

        public int Id
        {
            get { return this.conceptoDeNegocio.id; }
        }

        

        public bool Vigente
        {
            get { return this.conceptoDeNegocio.es_vigente; }
        }

        public Concepto_negocio ConceptoDeNegocio
        {
            get { return conceptoDeNegocio; }
        }


    }
}

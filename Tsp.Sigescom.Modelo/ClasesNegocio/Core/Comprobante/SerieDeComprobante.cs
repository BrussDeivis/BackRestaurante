using Tsp.Sigescom.Config;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class SerieDeComprobante
    {
        private Serie_comprobante serie;

        public SerieDeComprobante()
        {

        }


        public SerieDeComprobante(Serie_comprobante serie)
        {
            this.serie= serie;
        }
 
        public int Id
        {
            get { return this.serie.id; }
        }

        public string Numero
        {
            get { return this.serie.numero; }
        }

        public int ProximoNumeroDeComprobante
        {
            get { return this.serie.proximo_numero; }
        }
        public DetalleGenerico Comprobante()
        {
            return new DetalleGenerico(this.serie.Detalle_maestro); 
        }


        /// <summary>
        /// Es el centro de atencion propietario de la serie
        /// </summary>
        /// <returns></returns>
        public CentroDeAtencionExtendido CentroDeAtencion()
        {
            return new CentroDeAtencionExtendido(this.serie.Actor_negocio);
        }
        public EstablecimientoComercial EstablecimientoComercial()
        {
            return new CentroDeAtencionExtendido(this.serie.Actor_negocio).EstablecimientoComercial;
        }
        public bool Vigente
        {
            get { return this.serie.es_vigente; }
        }

        public bool EsAutonumerable
        {
            get { return this.serie.es_autonumerable; }
        }



        public static List<SerieDeComprobante> Convert(List<Serie_comprobante> series)
        {
            var seriesDeComprobante = new List<SerieDeComprobante>();

            foreach (var serie in series)
            {
                seriesDeComprobante.Add(new SerieDeComprobante(serie));
            }
            return seriesDeComprobante;
        }

    }
}

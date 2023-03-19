using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class ComprobanteDeNegocio
    {
        private readonly Comprobante comprobante;

        public ComprobanteDeNegocio()
        {

        }

        public ComprobanteDeNegocio(Comprobante comprobante)
        {
            this.comprobante = comprobante;
        }

        public long Id
        {
            get { return this.comprobante.id; }
        }

        public Serie_comprobante SerieComprobante()
        {
            return this.comprobante.Serie_comprobante;
        }

        public Comprobante Comprobante()
        {
            return this.comprobante;
        }

        public int? IdSerie
        {
            get { return this.comprobante.id_serie_comprobante; }
        }

        public int IdTipo
        {
            get { return this.comprobante.id_tipo_comprobante; }
        }

        public string CodigoTipo
        {
            get { return this.comprobante.Detalle_maestro.codigo; }
        }

        public string NombreTipo
        {
            get { return this.comprobante.Detalle_maestro.nombre; }
        }

        public string NombreCortoTipo
        {
            get { return this.comprobante.Detalle_maestro.valor; }
        }
        public string NumeroDeSerie
        {
            get { return this.comprobante.numero_serie; }
        }

        public int NumeroDeComprobante
        {
            get { return (int)this.comprobante.numero; }
        }

        public string SerieYNumero()
        {
            return this.NumeroDeComprobante > 0 ? this.NumeroDeSerie + "-" + this.NumeroDeComprobante :this.NombreCortoTipo;
        }

        public TipoDeComprobanteParaTransaccion Tipo()
        {
            return new TipoDeComprobanteParaTransaccion(this.comprobante.Detalle_maestro.Tipo_transaccion_tipo_comprobante.First());
        }

        public static List<TipoDeComprobanteParaTransaccion> convert(List<Tipo_transaccion_tipo_comprobante> detalles)
        {
            var comprobante = new List<TipoDeComprobanteParaTransaccion>();

            foreach (var detalle in detalles)
            {
                comprobante.Add(new TipoDeComprobanteParaTransaccion(detalle));
            }
            return comprobante;
        }

       
    }
}

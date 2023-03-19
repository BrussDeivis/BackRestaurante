using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class TipoDeComprobanteParaTransaccion
    {
        private Tipo_transaccion_tipo_comprobante tipoComprobantePorTransaccion;



        public TipoDeComprobanteParaTransaccion(int id, int idTipoTransaccion)
        {
            this.tipoComprobantePorTransaccion = new Tipo_transaccion_tipo_comprobante();
            this.IdTipoTransaccionTipoComprobante = id;
            this.IdTipoTransaccion = idTipoTransaccion;

        }


        public TipoDeComprobanteParaTransaccion()
        {

        }
        public TipoDeComprobanteParaTransaccion(Tipo_transaccion_tipo_comprobante tipo)
        {
            this.tipoComprobantePorTransaccion = tipo;
        }

        public int IdTipoTransaccionTipoComprobante
        {
            get { return this.tipoComprobantePorTransaccion.id; }
            set { this.tipoComprobantePorTransaccion.id = value; }

        }
        public int IdTipoTransaccion
        {
            get { return this.tipoComprobantePorTransaccion.id_tipo_transaccion; }
            set { this.tipoComprobantePorTransaccion.id_tipo_transaccion = value; }
        }
        public int IdTipoComprobante
        {
            get { return this.tipoComprobantePorTransaccion.id_tipo_comprobante; }
            set { this.tipoComprobantePorTransaccion.id_tipo_comprobante = value; }
        }

        public bool EsPropio
        {
            get { return this.tipoComprobantePorTransaccion.es_propio; }
        }



        public static List<TipoDeComprobanteParaTransaccion> Convert(List<Tipo_transaccion_tipo_comprobante> tipo_transaccion_tipo_comprobante)
        {
            var tipoDeComprobantes = new List<TipoDeComprobanteParaTransaccion>();

            foreach (var item in tipo_transaccion_tipo_comprobante)
            {
                tipoDeComprobantes.Add(new TipoDeComprobanteParaTransaccion(item));
            }
            return tipoDeComprobantes;
        }




        public List<Serie_comprobante> Series()
        {
            return this.tipoComprobantePorTransaccion.Detalle_maestro.Serie_comprobante.ToList();
        }

        public List<Serie_comprobante> SeriesAutonumericas()
        {
            var series = this.tipoComprobantePorTransaccion.Detalle_maestro.Serie_comprobante.Where(sc => sc.es_autonumerable == true).ToList();
            return series;
        }
        public List<Serie_comprobante> SeriesNoAutonumericas()
        {
            var series = this.tipoComprobantePorTransaccion.Detalle_maestro.Serie_comprobante.Where(sc => sc.es_autonumerable == false).ToList();
           return series;
        }
        /// <summary>
        /// devuelve todas las series cuyo numero empieza con el prefijo @prefijoFiltro
        /// </summary>
        /// <param name="prefijoFiltro"></param>
        /// <returns></returns>
        public List<Serie_comprobante> Series(char prefijoFiltro)
        {
            return this.tipoComprobantePorTransaccion.Detalle_maestro.Serie_comprobante.Where(s=>s.numero.StartsWith(prefijoFiltro.ToString())).ToList();
        }
        public List<Serie_comprobante> Series(int idCentroAtencion)
        {
            return this.tipoComprobantePorTransaccion.Detalle_maestro.Serie_comprobante.Where(sc => sc.id_propietario == idCentroAtencion).ToList();
        }

        public List<Serie_comprobante> Series(int idCentroAtencion, string prefijo)
        {
            return this.tipoComprobantePorTransaccion.Detalle_maestro.Serie_comprobante.Where(sc => sc.id_propietario == idCentroAtencion && sc.numero.StartsWith(prefijo) ).ToList();
        }
        public int Id
        {
            get { return this.tipoComprobantePorTransaccion.id_tipo_comprobante; }
        }

        public string Codigo
        {
            get { return this.tipoComprobantePorTransaccion.Detalle_maestro.valor; }
        }

        //public string CodigoSunat
        //{
        //    get { return this.tipoComprobantePorTransaccion.Detalle_maestro.valor; }
        //}

        public string Nombre
        {
            get { return this.tipoComprobantePorTransaccion.Detalle_maestro.nombre; }
        }





    }
}

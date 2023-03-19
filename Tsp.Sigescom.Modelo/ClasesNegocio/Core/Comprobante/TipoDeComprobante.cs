using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class TipoDeComprobante
    {

        private Detalle_maestro tipoDeComprobante;

        public TipoDeComprobante()
        {
        }

        public TipoDeComprobante(Detalle_maestro tipoDeComprobante)
        {
            this.tipoDeComprobante = tipoDeComprobante;
        }

        public int Id
        {
            get { return this.tipoDeComprobante.id; }
        }

        public string Codigo
        {
            get { return this.tipoDeComprobante.codigo; }
        }

        public string Valor
        {
            get { return this.tipoDeComprobante.valor; }
        }


        public string Nombre
        {
            get { return this.tipoDeComprobante.nombre; }
        }


        public static List<TipoDeComprobante> Convert(List<Detalle_maestro> detalleMaestro)
        {
            var tipoDeComprobantes = new List<TipoDeComprobante>();

            foreach (var item in detalleMaestro)
            {
                tipoDeComprobantes.Add(new TipoDeComprobante(item));
            }
            return tipoDeComprobantes;
        }







        public List<int> IdDeTiposDeTransaccionesConEmisionPropia()
        {
            List<int> lista = new List<int>();
            lista = this.TiposDeTransaccionesConEmisionPropia().Select(ttcp => ttcp.Id).ToList();
            return lista;
        }

        public List<int> IdDeTiposDeTransaccionesConEmisionTerceros()
        {
            List<int> lista = new List<int>();
            lista = this.TiposDeTransaccionesConEmisionTerceros().Select(ttcp => ttcp.Id).ToList();
            return lista;
        }




        public List<TipoDeTransaccion> TiposDeTransaccionesConEmisionPropia()
        {
            return TipoDeTransaccion.Convert(  this.tipoDeComprobante.Tipo_transaccion_tipo_comprobante
                .Where(tttc => tttc.es_propio).Select(tttc => tttc.Tipo_transaccion).ToList());
        }

        public List<TipoDeTransaccion> TiposDeTransaccionesConEmisionTerceros()
        {
            return TipoDeTransaccion.Convert(this.tipoDeComprobante.Tipo_transaccion_tipo_comprobante
                .Where(tttc => !tttc.es_propio).Select(tttc => tttc.Tipo_transaccion).ToList());
        }



        public List<TipoDeComprobanteParaTransaccion> TipoComprobanteParaTransaccionTiposDeTransaccionsConEmisionPropia()
        {
            return TipoDeComprobanteParaTransaccion.Convert(this.tipoDeComprobante.Tipo_transaccion_tipo_comprobante
                .Where(tttc => tttc.es_propio).ToList());
        }

        public List<TipoDeComprobanteParaTransaccion> TipoComprobanteParaTransaccionTiposDeTransaccionsConEmisionTerceros()
        {
            return TipoDeComprobanteParaTransaccion.Convert(this.tipoDeComprobante.Tipo_transaccion_tipo_comprobante
                .Where(tttc => !tttc.es_propio).ToList());
        }




    }
}

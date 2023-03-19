using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class TipoComprobanteViewModel
    {
        [DataMember]
        public bool EsPropio; 
        public ComboGenericoViewModel TipoComprobante { get; set; }
        public List<ComboGenericoViewModel> Series { get; set; }

        public TipoComprobanteViewModel()
        {

        }
        public TipoComprobanteViewModel(TipoDeComprobanteParaTransaccion tc)
        {
            this.EsPropio = tc.EsPropio;
            this.TipoComprobante = new ComboGenericoViewModel(tc.Id, tc.Nombre);
            this.Series = new List<ComboGenericoViewModel>();
            foreach (var item in tc.Series())
            {
                this.Series.Add(new ComboGenericoViewModel(item.id,item.numero));
            }
        }

        /// <summary>
        /// filtra y considera solo las series que empiezan con el caracter @filtroSeries
        /// </summary>
        /// <param name="tc"></param>
        /// <param name="filtroSeries"></param>
        public TipoComprobanteViewModel(TipoDeComprobanteParaTransaccion tc, char prefijoFiltroSeries)
        {
            this.EsPropio = tc.EsPropio;
            this.TipoComprobante = new ComboGenericoViewModel(tc.Id, tc.Nombre);
            this.Series = new List<ComboGenericoViewModel>();
            foreach (var item in tc.Series(prefijoFiltroSeries))
            {
                this.Series.Add(new ComboGenericoViewModel(item.id, item.numero));
            }
        }

        public static List<TipoComprobanteViewModel> Convert(List<TipoDeComprobanteParaTransaccion> comprobantes)
        {
            var ListaComprobantes = new List<TipoComprobanteViewModel>();

            foreach (var comprobante in comprobantes)
            {
                ListaComprobantes.Add(new TipoComprobanteViewModel(comprobante));
            }
            return ListaComprobantes;
        }

        public static List<TipoComprobanteViewModel> Convert(List<TipoDeComprobanteParaTransaccion> comprobantes, char prefijoFiltroSeries)
        {
            var ListaComprobantes = new List<TipoComprobanteViewModel>();

            foreach (var comprobante in comprobantes)
            {
                ListaComprobantes.Add(new TipoComprobanteViewModel(comprobante,prefijoFiltroSeries));
            }
            return ListaComprobantes;
        }
    }
}
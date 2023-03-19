using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class SelectorTipoDeComprobante
    {
        public long IdComprobante { get; set; }
        public ItemGenerico TipoComprobante { get; set; }
        public List<SerieComprobante_> Series { get; set; }
        public bool EsPropio { get; set; }
        public int SerieSeleccionada { get; set; }
        public string SerieIngresada { get; set; }
        public int NumeroIngresado { get; set; }
        public bool MostrarSelectorSerie { get; set; }
        public bool MostrarEntradaSerie { get; set; }
        public bool MostrarEntradaNumero { get; set; }

        public SelectorTipoDeComprobante()
        {
        }
         
        public SelectorTipoDeComprobante(TipoDeComprobanteParaTransaccion tipoDeComprobante)
        {
            this.EsPropio = tipoDeComprobante.EsPropio;
            this.TipoComprobante = new ItemGenerico(tipoDeComprobante.Id, tipoDeComprobante.Codigo, tipoDeComprobante.Nombre);
            this.Series = new List<SerieComprobante_>();
            foreach (var item in tipoDeComprobante.Series())
            {
                this.Series.Add(new SerieComprobante_(item.id, item.numero, item.es_autonumerable));
            }
            this.MostrarSelectorSerie = Series.Count() > 1;
            this.MostrarEntradaSerie = !tipoDeComprobante.EsPropio;
            this.MostrarEntradaNumero = !tipoDeComprobante.EsPropio;
            if (Series.Count > 0)
            {
                this.SerieSeleccionada = this.Series[0].Id;
            }
        }

        public SelectorTipoDeComprobante(TipoDeComprobanteParaTransaccion tipoDeComprobante, int idCentroAtencion)
        {
            this.EsPropio = tipoDeComprobante.EsPropio;
            this.TipoComprobante = new ItemGenerico(tipoDeComprobante.Id, tipoDeComprobante.Codigo, tipoDeComprobante.Nombre);
            this.Series = new List<SerieComprobante_>();
            foreach (var item in tipoDeComprobante.Series(idCentroAtencion))
            {
                this.Series.Add(new SerieComprobante_(item.id, item.numero, item.es_autonumerable));
            }
            this.MostrarSelectorSerie = Series.Count() > 1;
            this.MostrarEntradaSerie = !tipoDeComprobante.EsPropio;
            this.MostrarEntradaNumero = !tipoDeComprobante.EsPropio;
            if (Series.Count > 0)
            {
                this.SerieSeleccionada = this.Series[0].Id;
            }
        }

        public SelectorTipoDeComprobante(TipoDeComprobanteParaTransaccion tipoDeComprobante, int idCentroAtencion, string prefijoFiltroSeries)
        {
            this.EsPropio = tipoDeComprobante.EsPropio;
            this.TipoComprobante = new ItemGenerico(tipoDeComprobante.Id, tipoDeComprobante.Codigo, tipoDeComprobante.Nombre);
            this.Series = new List<SerieComprobante_>();
            foreach (var item in tipoDeComprobante.Series(idCentroAtencion, prefijoFiltroSeries))
            {
                this.Series.Add(new SerieComprobante_(item.id, item.numero, item.es_autonumerable));
            }
            this.MostrarSelectorSerie = Series.Count() > 1;
            this.MostrarEntradaSerie = !tipoDeComprobante.EsPropio;
            this.MostrarEntradaNumero = !tipoDeComprobante.EsPropio;
            if (Series.Count > 0)
            {
                this.SerieSeleccionada = this.Series[0].Id;
            }
        }

        public SelectorTipoDeComprobante(ItemGenerico tipoComprobante, List<SerieComprobante_> series, bool esPropio, int serieSeleccionada, string serieIngresada, int numeroIngresado, bool mostrarSelectorSerie, bool mostrarEntradaSerie, bool mostrarEntradaNumero)
        {
            TipoComprobante = tipoComprobante;
            Series = series;
            EsPropio = esPropio;
            SerieSeleccionada = serieSeleccionada;
            SerieIngresada = serieIngresada;
            NumeroIngresado = numeroIngresado;
            MostrarSelectorSerie = mostrarSelectorSerie;
            MostrarEntradaSerie = mostrarEntradaSerie;
            MostrarEntradaNumero = mostrarEntradaNumero;
            if (Series.Count > 0)
            {
                this.SerieSeleccionada = this.Series[0].Id;
            }
        }

        public SelectorTipoDeComprobante(TipoDeComprobanteParaTransaccion tipoDeComprobante, bool esAutonumerica)
        {
            this.EsPropio = tipoDeComprobante.EsPropio;
            this.TipoComprobante = new ItemGenerico(tipoDeComprobante.Id, tipoDeComprobante.Codigo, tipoDeComprobante.Nombre);
            this.Series = new List<SerieComprobante_>();
            foreach (var item in esAutonumerica ? tipoDeComprobante.SeriesAutonumericas() : tipoDeComprobante.SeriesNoAutonumericas())
            {
                this.Series.Add(new SerieComprobante_(item.id, item.numero, item.es_autonumerable));
            }
            this.MostrarSelectorSerie = Series.Count() > 1;
            this.MostrarEntradaSerie = !tipoDeComprobante.EsPropio;
            this.MostrarEntradaNumero = !tipoDeComprobante.EsPropio;
            if (Series.Count > 0)
            {
                this.SerieSeleccionada = this.Series[0].Id;
            }
        }

        public SelectorTipoDeComprobante(TipoDeComprobanteParaTransaccion tipoDeComprobante, int idCentroAtencion, bool esAutonumerica)
        {
            this.EsPropio = tipoDeComprobante.EsPropio;
            this.TipoComprobante = new ItemGenerico(tipoDeComprobante.Id, tipoDeComprobante.Codigo, tipoDeComprobante.Nombre);
            this.Series = new List<SerieComprobante_>();
            var series = esAutonumerica ? tipoDeComprobante.SeriesAutonumericas() : tipoDeComprobante.SeriesNoAutonumericas();
            foreach (var item in series)
            {
                this.Series.Add(new SerieComprobante_(item.id, item.numero, item.es_autonumerable));
            }
            this.MostrarSelectorSerie = Series.Count() > 1;
            this.MostrarEntradaSerie = !tipoDeComprobante.EsPropio;
            this.MostrarEntradaNumero = !tipoDeComprobante.EsPropio;
            if (Series.Count > 0)
            {
                this.SerieSeleccionada = this.Series[0].Id;
            }
        }

        public static List<SelectorTipoDeComprobante> Convert(List<TipoDeComprobanteParaTransaccion> tipoDeComprobantes)
        {
            var ListaComprobantes = new List<SelectorTipoDeComprobante>();

            foreach (var comprobante in tipoDeComprobantes)
            {
                ListaComprobantes.Add(new SelectorTipoDeComprobante(comprobante));
            }
            return ListaComprobantes.Except(ListaComprobantes.Where(lc => lc.EsPropio && lc.Series.Count() == 0)).ToList();
        }

        public static List<SelectorTipoDeComprobante> Convert(List<TipoDeComprobanteParaTransaccion> tipoDeComprobantes, int idCentroAtencion)
        {
            var ListaComprobantes = new List<SelectorTipoDeComprobante>();

            foreach (var comprobante in tipoDeComprobantes)
            {
                ListaComprobantes.Add(new SelectorTipoDeComprobante(comprobante, idCentroAtencion));
            }
            return ListaComprobantes.Except(ListaComprobantes.Where(lc => lc.EsPropio && lc.Series.Count() == 0)).ToList();
        }

        public static List<SelectorTipoDeComprobante> Convert(List<TipoDeComprobanteParaTransaccion> tipoDeComprobantes, int idCentroAtencion, string prefijoFiltroSeries)
        {
            try
            {
                var ListaComprobantes = new List<SelectorTipoDeComprobante>();

                foreach (var comprobante in tipoDeComprobantes)
                {
                    ListaComprobantes.Add(new SelectorTipoDeComprobante(comprobante, idCentroAtencion, prefijoFiltroSeries));
                }
                return ListaComprobantes.Except(ListaComprobantes.Where(lc => lc.EsPropio && lc.Series.Count() == 0)).ToList();
            }
            catch (Exception e)
            {
                throw new ModeloException("Error al convertir tipo de comprobante", e);
            }
        }

        public static List<SelectorTipoDeComprobante> Convert(List<TipoDeComprobanteParaTransaccion> tipoDeComprobantes, bool esAutonumerica)
        {
            var ListaComprobantes = new List<SelectorTipoDeComprobante>();

            foreach (var comprobante in tipoDeComprobantes)
            {
                ListaComprobantes.Add(new SelectorTipoDeComprobante(comprobante));
            }
            return ListaComprobantes.Except(ListaComprobantes.Where(lc => lc.EsPropio && lc.Series.Count() == 0)).ToList();
        }

        public static List<SelectorTipoDeComprobante> Convert(List<TipoDeComprobanteParaTransaccion> tipoDeComprobantes, int idCentroAtencion, bool esAutonumerica)
        {
            var ListaComprobantes = new List<SelectorTipoDeComprobante>();

            foreach (var comprobante in tipoDeComprobantes)
            {
                ListaComprobantes.Add(new SelectorTipoDeComprobante(comprobante, idCentroAtencion, esAutonumerica));
            }
            return ListaComprobantes.Except(ListaComprobantes.Where(lc => lc.EsPropio && lc.Series.Count() == 0)).ToList();
        }

        /// <summary>
        /// Convierte una lista de tipos de comprobante para transaccion en una lista lista de selectores de los tipos de comprobantes, de acuerdo a un parametro el cual dice si es de emision propia o no, el centrod e atencion que este asignado en esta serie, si tiene prefijo de filtro de series y cual es el prefijo del filtro de las series
        /// </summary>
        /// <param name="tipoDeComprobantes"></param>
        /// <param name="idCentroAtencion"></param>
        /// <param name="EsEmisionPropia"></param>
        /// <param name="tienePrefijoFiltroSeries"></param>
        /// <param name="prefijoFiltroSeries"></param>
        /// <returns></returns>
        public static List<SelectorTipoDeComprobante> ConvertirPropios(List<TipoDeComprobanteParaTransaccion> tipoDeComprobantes, int idCentroAtencion, string prefijoFiltroSeries)
        {
            var ListaComprobantes = new List<SelectorTipoDeComprobante>();

            foreach (var comprobante in tipoDeComprobantes)
            {
                ListaComprobantes.Add(new SelectorTipoDeComprobante(comprobante, idCentroAtencion, prefijoFiltroSeries));
            }
            ListaComprobantes.Except(ListaComprobantes.Where((lc => lc.EsPropio && lc.Series.Count() == 0))).ToList();
            return ListaComprobantes.Except(ListaComprobantes.Where(lc => !lc.EsPropio)).ToList();
        }

        public static List<SelectorTipoDeComprobante> ConvertirNoPropios(List<TipoDeComprobanteParaTransaccion> tipoDeComprobantes)
        {
            var ListaComprobantes = new List<SelectorTipoDeComprobante>();

            foreach (var comprobante in tipoDeComprobantes)
            {
                ListaComprobantes.Add(new SelectorTipoDeComprobante(comprobante));
            }
            return ListaComprobantes.Except(ListaComprobantes.Where(lc => lc.EsPropio)).ToList();
        }
    }
}
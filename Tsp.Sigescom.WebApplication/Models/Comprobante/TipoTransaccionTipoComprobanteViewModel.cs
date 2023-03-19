using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models.Comprobante
{
    [Serializable]
    [DataContract]
    public class TipoDeTransaccionTipoDeComprobanteViewModel
    {
        [DataMember]
        public int Id { get; set; }
        public int IdTipoTransaccion { get; set; }

        public TipoDeTransaccionTipoDeComprobanteViewModel()
        {

        }

        public TipoDeTransaccionTipoDeComprobanteViewModel(int Id, int IdTipoTransaccion)
        {
            this.Id = Id;
            this.IdTipoTransaccion = IdTipoTransaccion;
        }

        public static List<TipoDeTransaccionTipoDeComprobanteViewModel> Convert(List<TipoDeComprobanteParaTransaccion> tiposDeComprobanteParaTransaccion)
        {
            List<TipoDeTransaccionTipoDeComprobanteViewModel> tiposTransaccionTipoComprobanteViewModel = new List<TipoDeTransaccionTipoDeComprobanteViewModel>();
            foreach (var item in tiposDeComprobanteParaTransaccion)
            {
                tiposTransaccionTipoComprobanteViewModel.Add(new TipoDeTransaccionTipoDeComprobanteViewModel(item.Id, item.IdTipoTransaccion));
            }
            return tiposTransaccionTipoComprobanteViewModel;
        }

        public static List<TipoDeTransaccionTipoDeComprobanteViewModel> MatchTiposTransaccion(List<TipoDeTransaccionTipoDeComprobanteViewModel> TiposDeTransaccion, List<int> IdDeTiposDeTransaccion)
        {

            if (IdDeTiposDeTransaccion == null)
            {
                TiposDeTransaccion.Clear();
            }
            else
            {
                if (TiposDeTransaccion.Count() != 0)
                {
                    int[] idsAEliminar = TiposDeTransaccion.Where(tdt => !IdDeTiposDeTransaccion.Contains(tdt.IdTipoTransaccion)).Select(tdt => tdt.IdTipoTransaccion).ToArray();
                    for (int i = 0; i < idsAEliminar.Length; i++)
                    {
                        TiposDeTransaccion.Remove(TiposDeTransaccion.Single(tdt => tdt.IdTipoTransaccion == idsAEliminar[i]));
                    }
                    foreach (var item in IdDeTiposDeTransaccion)
                    {
                        if (!TiposDeTransaccion.Any(tdtcp => tdtcp.IdTipoTransaccion == item))
                        {
                            TiposDeTransaccion.Add(new TipoDeTransaccionTipoDeComprobanteViewModel(0, item));
                        }
                    }
                }
                else
                {
                    foreach (var item in IdDeTiposDeTransaccion)
                    {
                        if (!TiposDeTransaccion.Any(tdtcp => tdtcp.IdTipoTransaccion == item))
                        {
                            TiposDeTransaccion.Add(new TipoDeTransaccionTipoDeComprobanteViewModel(0, item));
                        }
                    }

                }
            }

            return TiposDeTransaccion;
        }

        public static List<string> TiposDeTransaccionesConEmisionPropia(List<TipoDeTransaccion> listTiposDeTransaccionesConEmisionPropia)
        {
            List<string> lista = new List<string>();
            lista = listTiposDeTransaccionesConEmisionPropia.Select(ttcp => ttcp.Nombre).ToList();
          
            return lista;
        }

        public static List<string> TiposDeTransaccionesConEmisionTerceros(List<TipoDeTransaccion> listTiposDeTransaccionesConEmisionTerceros)
        {
            List<string> lista = new List<string>();
            lista = listTiposDeTransaccionesConEmisionTerceros.Select(ttcp => ttcp.Nombre).ToList();
            return lista;
        }

        public static string ConcatenarTiposDeTransacciones(List<string> lista)
        {
            string resultado = string.Join("\n", lista);
            return resultado;
        }
        public static List<TipoDeComprobanteParaTransaccion> ConvertTipoDeComprobanteParaTransaccion(List<TipoDeTransaccionTipoDeComprobanteViewModel> TipoTransaccionTipoComprobanteViewModel)
        {
            List<TipoDeComprobanteParaTransaccion> tiposTransaccionTipoComprobante = new List<TipoDeComprobanteParaTransaccion>();
            foreach (var item in TipoTransaccionTipoComprobanteViewModel)
            {
                tiposTransaccionTipoComprobante.Add(new TipoDeComprobanteParaTransaccion(item.Id, item.IdTipoTransaccion));
            }
            return tiposTransaccionTipoComprobante;
        }
    }
}
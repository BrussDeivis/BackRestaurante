using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class KardexInventarioValorizadoConceptoDeNegocioViewModel
    {
        public List<KardexViewModel> Kardex { get; set; }
        public List<InventarioValorizadoViewModel> InventarioValorizadoParaSaldoInicial { get; set; }
        public List<InventarioValorizadoViewModel> InventarioValorizadoParaSaldoFinal { get; set; }

        public KardexInventarioValorizadoConceptoDeNegocioViewModel(List<KardexViewModel> kardex, List<InventarioValorizadoViewModel> inventarioValorizadoParaSaldoInicial, List<InventarioValorizadoViewModel> inventarioValorizadoParaSaldoFinal)
        {
            this.Kardex = kardex;
            this.InventarioValorizadoParaSaldoInicial = inventarioValorizadoParaSaldoInicial;
            this.InventarioValorizadoParaSaldoFinal = inventarioValorizadoParaSaldoFinal;
        }

    }

    public class KardexViewModel
    {
        [DataMember]
        public int Index { get; set; }
        public string Fecha { get; set; }
        public string NombreActorNegocioExterno { get; set; }
        public int IdTipoTransaccion { get; set; }
        public string Operacion { get; set; }
        public string CodigoTipoComprobante { get; set; }
        public string NumeroDeSerieComprobanteYNumeroComprobante { get; set; }
        public string CantidadEntrada { get; set; }
        public string LoteEntrada { get; set; }
        public string ImporteUnitarioEntrada { get; set; }
        public string ImporteTotalEntrada { get; set; }
        public string CantidadSalida { get; set; }
        public string ImporteUnitarioSalida { get; set; }
        public string ImporteTotalSalida { get; set; }
        public string Saldo { get; set; }

        public KardexViewModel()
        {

        }

        public static List<KardexViewModel> Convert(List<MovimientoAlmacen> detallesTransaccionKardex, InventarioConceptoNegocio inventarioConceptoNegocio)
        {
            List<KardexViewModel> kardexViewModel = new List<KardexViewModel>();
            KardexViewModel item;
            decimal saldo = inventarioConceptoNegocio.CantidadPrincipal;
            int index = 0;
            foreach (var detalle in detallesTransaccionKardex)
            {
                item = new KardexViewModel();
                item.Index = index++;
                item.Fecha = detalle.Fecha.ToString("dd/MM/yyyy");
                item.NombreActorNegocioExterno = detalle.NombreActorNegocioExterno.Replace("|", " ");
                item.Operacion = detalle.NombreTipoTransaccion;
                item.CodigoTipoComprobante = detalle.CodigoTipoComprobante;
                item.NumeroDeSerieComprobanteYNumeroComprobante = detalle.NumeroSerie + "-" + detalle.NumeroComprobante;
     
                if (detalle.EsEntrada)
                {
                    item.CantidadEntrada = detalle.Cantidad.ToString("N" + AplicacionSettings.Default.NumeroDecimalesEnCantidad);
                    item.ImporteUnitarioEntrada = detalle.ImporteUnitario.ToString("N" + AplicacionSettings.Default.NumeroDecimalesEnPrecio);
                    item.ImporteTotalEntrada = (detalle.Cantidad * detalle.ImporteUnitario).ToString("N2");
                }
                else
                {
                    item.CantidadSalida = detalle.Cantidad.ToString("N" + AplicacionSettings.Default.NumeroDecimalesEnCantidad);
                    item.ImporteUnitarioSalida = detalle.ImporteUnitario.ToString("N" + AplicacionSettings.Default.NumeroDecimalesEnPrecio);
                    item.ImporteTotalSalida = (detalle.Cantidad * detalle.ImporteUnitario).ToString("N2");
                }
                saldo += (System.Convert.ToDecimal(item.CantidadEntrada) - System.Convert.ToDecimal(item.CantidadSalida));
                kardexViewModel.Add(item);
                item.Saldo = saldo.ToString("N2");
            }
            return kardexViewModel;
        }

        public static List<KardexViewModel> Convert()
        {
            return new List<KardexViewModel>();
        }

    }



}
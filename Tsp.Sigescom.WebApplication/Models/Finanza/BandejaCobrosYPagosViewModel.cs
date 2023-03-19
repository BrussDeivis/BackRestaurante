using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class BandejaCobrosYPagosViewModel
    {
        [DataMember]
        public long IdTransaccion { get; set; }
        public string ActorComercial { get; set; }
        public string FechaDePago { get; set; }
        public string MedioDePago { get; set; }
        public bool Afavor { get; set; }
        public string Documento { get; set; }
        public string Total { get; set; }

        public BandejaCobrosYPagosViewModel()
        {

        }

        public BandejaCobrosYPagosViewModel(MovimientoEconomico pago)
        {
            this.Documento = pago.PagadorRecibidor().CodigoTipoDocumentoIdentidad() + " " + pago.PagadorRecibidor().DocumentoIdentidad;
            this.ActorComercial = pago.PagadorRecibidor().RazonSocial;
            this.Total = pago.Total.ToString("N2");
            this.FechaDePago = pago.FechaEmision.ToString("dd/MM/yyyy");
            //this.Afavor = pago.aFavor();
            this.MedioDePago = pago.TrazaDePago() != null ? pago.TrazaDePago().MedioDePago().nombre : "Efectivo";
        }

        public static List<BandejaCobrosYPagosViewModel> Convert_(List<MovimientoEconomico> lista)
        {
            List<BandejaCobrosYPagosViewModel> respuesta = new List<BandejaCobrosYPagosViewModel>();
            foreach (var cuenta in lista)
            {
                respuesta.Add(new BandejaCobrosYPagosViewModel(cuenta));
            }
            return respuesta;
        }

    }
}
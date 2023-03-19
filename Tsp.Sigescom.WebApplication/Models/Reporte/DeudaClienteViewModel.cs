using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    public class DeudaClienteViewModel
    {
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public string NumeroDocumento { get; set; }
        public decimal TotalVenta { get; set; }
        public decimal ACuenta { get; set; }
        public decimal Deuda { get; set; }
        public string TipoComprobante { get; set; }
        public string Numeroserie { get; set; }
        public int NumeroComprobante{ get; set; }

        public DeudaClienteViewModel()
        { }

        public DeudaClienteViewModel(Cuota deuda)
        {
            this.IdCliente = deuda.Transaccion.Actor_negocio1.id;
            this.NombreCliente = deuda.Transaccion.Actor_negocio1.PrimerNombre.Replace("|", " ");
            this.NumeroDocumento = deuda.Transaccion.Actor_negocio1.Actor.numero_documento_identidad;
            this.TotalVenta = deuda.total * (deuda.por_cobrar ? 1 : -1); ;
            this.ACuenta = deuda.pago_a_cuenta * (deuda.por_cobrar ? 1 : -1);
            this.Deuda = deuda.saldo * (deuda.por_cobrar ? 1 : -1);
            this.TipoComprobante = deuda.Transaccion.Comprobante.Detalle_maestro.valor;
            this.Numeroserie = deuda.Transaccion.Comprobante.numero_serie;
            this.NumeroComprobante = deuda.Transaccion.Comprobante.numero;
        }

        public DeudaClienteViewModel(int idCliente, string cliente,decimal total,decimal acuenta,decimal deuda,string nombreCortoComprobante,string numeroSerie,int numeroComprobante)

        {
            this.IdCliente = idCliente;
            this.NombreCliente = cliente;
            this.TotalVenta = total;
            this.ACuenta = acuenta;
            this.Deuda = deuda;
            this.TipoComprobante = nombreCortoComprobante;
            this.Numeroserie = numeroSerie;
            this.NumeroComprobante = numeroComprobante;
        }

        public static List<DeudaClienteViewModel> Convert(List<Cuota> deudas)
        {
            var reporteConsolidadoViewModels = new List<DeudaClienteViewModel>();
            try
            {
                foreach (var deuda in deudas)
                {
                    reporteConsolidadoViewModels.Add(new DeudaClienteViewModel(deuda));
                }

                return reporteConsolidadoViewModels.OrderBy(r => r.NombreCliente).ThenBy(r => r.TipoComprobante).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        internal static List<DeudaClienteViewModel> Resumen(List<DeudaClienteViewModel> deudaResumen)
        {
            List<DeudaClienteViewModel> reporteResumen = new List<DeudaClienteViewModel>();

            reporteResumen = deudaResumen.GroupBy(d => d.NumeroDocumento)
                         .Select(dc =>
                               new DeudaClienteViewModel()
                               {
                                   NombreCliente = dc.FirstOrDefault().NombreCliente,
                                   ACuenta = dc.Sum(s => s.ACuenta),
                                   Deuda = dc.Sum(s => s.Deuda),
                                   TotalVenta = dc.Sum(s => s.TotalVenta),
                               }).OrderBy(r => r.NombreCliente).ThenBy(r => r.TipoComprobante).ToList();
            return reporteResumen;
        }



    }
}
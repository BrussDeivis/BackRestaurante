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
    public class DeudaProveedorViewModel
    {
        public string NombreProveedor { get; set; }
        public decimal Total { get; set; }
        public decimal Acuenta { get; set; }
        public decimal Deuda { get; set; }
        public string NombreCortoComprobante { get; set; }
        public string NumeroSerie { get; set; }
        public int NumeroComprobante{ get; set; }
        public string CodigoCuota { get; set; }

        public DeudaProveedorViewModel()
        { }

        public DeudaProveedorViewModel(Cuota deuda)
        {
           this.NombreProveedor = deuda.Transaccion.Actor_negocio1.PrimerNombre.Replace("|", " ");
            this.Total = deuda.total * (deuda.por_cobrar ?-1 :1 );
            this.Acuenta = deuda.pago_a_cuenta * (deuda.por_cobrar ? -1 : 1);
            this.Deuda = deuda.saldo * (deuda.por_cobrar ? -1 : 1);
            //this.NombreCortoComprobante = deuda.Comprobante.Serie_comprobante.Detalle_maestro.valor;
            //this.NumeroSerie = deuda.Comprobante.numero_serie;
            //this.NumeroComprobante= deuda.Comprobante.numero;
            this.NombreCortoComprobante = deuda.Transaccion.Comprobante.Detalle_maestro.valor;
            this.NumeroSerie = deuda.Transaccion.Comprobante.numero_serie;
            this.NumeroComprobante = (int)deuda.Transaccion.Comprobante.numero;
            this.CodigoCuota = deuda.codigo;
        }
        public DeudaProveedorViewModel(string proveedor,decimal total,decimal acuenta,decimal deuda,string nombreCortoComprobante,string numeroSerie,int numeroComprobante)
        {
            this.NombreProveedor = proveedor;
            this.Total = total;
            this.Acuenta = acuenta;
            this.Deuda = deuda;
            this.NombreCortoComprobante = nombreCortoComprobante;
            this.NumeroSerie = numeroSerie;
            this.NumeroComprobante = numeroComprobante;
        }

        public static List<DeudaProveedorViewModel> Convert(List<Cuota> deudas)
        {

            var reporteConsolidadoViewModels = new List<DeudaProveedorViewModel>();
            try
            {
                foreach (var deuda in deudas)
                {
                    reporteConsolidadoViewModels.Add(new DeudaProveedorViewModel(deuda));
                }

                return reporteConsolidadoViewModels.OrderBy(r=>r.NombreProveedor).ThenBy(r=>r.NombreCortoComprobante).ToList();
            }
            catch (Exception e)
            {

                throw e;
            }
        }
                
        internal static List<DeudaProveedorViewModel> Resumen(List<DeudaProveedorViewModel> deudas)
        {
            List<DeudaProveedorViewModel> reporteResumen = new List<DeudaProveedorViewModel>();
            List<string> proveedores = deudas.Select(d => d.NombreProveedor).Distinct().ToList();

            foreach (var proveedor in proveedores)
            {
                var deudasProveedor = deudas.Where(d => d.NombreProveedor == proveedor);
                reporteResumen.Add(new DeudaProveedorViewModel(proveedor, deudasProveedor.Sum(d => d.Total), deudasProveedor.Sum(d=> d.Acuenta), deudasProveedor.Sum(d=>d.Deuda), "","",0));
            }

            return reporteResumen.OrderBy(r=>r.NombreProveedor).ThenBy(r => r.NombreCortoComprobante).ToList();
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    public class DeudaAProveedorPorCuotaAdministradorViewModel
    {
        public DateTime FechadeVencimiento { get; set; }
        public int Cuota { get; set; }
        public string NombreProveedor { get; set; }
        public decimal Total { get; set; }
        public decimal Acuenta { get; set; }
        public decimal Deuda { get; set; }
        public string NombreCortoComprobante { get; set; }
        public string Comprobante { get; set; }




        public DeudaAProveedorPorCuotaAdministradorViewModel()
        { }

        //falta arreglar 
        public DeudaAProveedorPorCuotaAdministradorViewModel(Reporte_Transaccion_Deuda_A_Proveedor deuda)
        {   /*
            this.NombreProveedor = deuda.NombreProveedor;
            this.Total = deuda.Total;
            this.Acuenta = deuda.Acuenta;
            this.Deuda = deuda.Deuda;
            this.NombreCortoComprobante = deuda.NombreCortoComprobante;
            this.Comprobante = deuda.Comprobante;
            */
        }

        public static List<DeudaAProveedorPorCuotaAdministradorViewModel> Convert(List<Reporte_Transaccion_Deuda_A_Proveedor> deudas)
        {

            var reporteConsolidadoporCuotaViewModels = new List<DeudaAProveedorPorCuotaAdministradorViewModel>();
            try
            {
                foreach (var deuda in deudas)
                {
                    reporteConsolidadoporCuotaViewModels.Add(new DeudaAProveedorPorCuotaAdministradorViewModel(deuda));
                }

                return reporteConsolidadoporCuotaViewModels;
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }

        
    }
}
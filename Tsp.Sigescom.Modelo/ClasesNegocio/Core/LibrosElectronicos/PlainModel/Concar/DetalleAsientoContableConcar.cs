using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.Core.LibrosElectronicos.PlainModel
{
    public class DetalleAsientoContableConcar
    {
        public string SubDiario { get; set; }
        public string NumeroComprobante { get; set; }
        public string FechaComprobante { get; set; }
        public string CodigoMoneda { get; set; }
        public string GlosaPrincipal { get; set; }
        public decimal TipoCambio { get; set; }
        public string TipoConversion { get; set; }
        public string FlagConversionMoneda { get; set; }
        public string FechaTipoCambio { get; set; }
        public string CuentaContable { get; set; }
        public string CodigoAnexo { get; set; }
        public string CodigoCentroCosto { get; set; }
        public string DebeHaber { get; set; }
        public decimal ImporteOriginal { get; set; }
        public decimal ImporteDolares { get; set; }
        public decimal ImporteSoles { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string FechaDocumento { get; set; }
        public string FechaVencimiento { get; set; }
        public string CodigoArea { get; set; }
        public string GlosaDetalle { get; set; }
        public string CodigoAnexoAuxiliar { get; set; }
        public string MedioPago { get; set; }
        public string TipoDocumentoReferencia { get; set; }
        public string NumeroDocumentoReferencia { get; set; }
        public string FechaDocumentoReferencia { get; set; }
        public string NroMaqRegistradoraTipoDocRef { get; set; }
        public decimal BaseImponibleDocumentoReferencia { get; set; }
        public decimal IGVDocumentoProvision { get; set; }
        public string TipoReferenciaenestadoMQ { get; set; }
        public string NumeroSerieCajaRegistradora { get; set; }
        public string FechaOperacion { get; set; }
        public string TipoTasa { get; set; }
        public decimal TasaDetraccionPercepcion { get; set; }
        public decimal ImporteBaseDetraccionPercepcionDolares { get; set; }
        public decimal ImporteBaseDetraccionPercepcionSoles { get; set; }
        public string TipoCambioparaF { get; set; }
        public decimal ImporteIGVSinDerechoCreditoFiscal { get; set; }

        public List<DetalleAsientoContableConcar> Convert()
        {
            return new List<DetalleAsientoContableConcar>();
        }
    }
}

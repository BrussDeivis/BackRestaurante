using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.Entidades.ComprobantesModel
{
    public class FacturaCompra : DocumentoElectronicoCompra
    {
        public override string NombreTipo { get; set; } = "FACTURA ELECTRÓNICA";
        //public string PlacaDelVehiculo { get; set; }
        public bool TieneGuiaDeRemision { get; set; }
        public List<Relacionado> GuiasDeRemision { get; set; }
        //public string TipoDeDocumentoGuiaDeRemision { get; set; }
        //public string SerieDelDocumentoGuiaDeRemision { get; set; }
        //public int NumeroDelDocumentoGuiaDeRemision { get; set; }

        public FacturaCompra()
        { }

        public FacturaCompra(OrdenDeCompra orden, EstablecimientoComercialExtendidoConLogo sede, byte[] qrBytes, bool mostrarEncabezadoTestigo, ModoImpresionCaracteristicasEnum modoImpresionCaracteristicas) : base(orden, sede, qrBytes, mostrarEncabezadoTestigo, modoImpresionCaracteristicas)
        {
            TieneGuiaDeRemision = orden.TieneGuiaDeRemision();
            //TipoDeDocumentoGuiaDeRemision = orden.TieneGuiaDeRemision() ? orden.GuiaRemision().NombreTipo : "";
            //SerieDelDocumentoGuiaDeRemision = orden.TieneGuiaDeRemision() ? orden.GuiaRemision().NumeroDeSerie : "";
            //NumeroDelDocumentoGuiaDeRemision = orden.TieneGuiaDeRemision() ? orden.GuiaRemision().NumeroDeComprobante : 0;

        }

        public static List<FacturaCompra> Convert(List<OrdenDeCompra> ordenes, EstablecimientoComercialExtendidoConLogo sede, ModoImpresionCaracteristicasEnum modoImpresionCaracteristicas)
        {
            List<FacturaCompra> resultado = new List<FacturaCompra>();
            foreach (var item in ordenes)
            {
                resultado.Add(new FacturaCompra(item, sede, null, false, modoImpresionCaracteristicas));
            }
            return resultado;
        }
    }
}
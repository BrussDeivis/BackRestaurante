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
    public class NotaDeCompra : DocumentoElectronicoCompra
    {
        public override string NombreTipo { get; set; } = "NOTA DE COMPRA";
        public NotaDeCompra()
        {


        }
        public NotaDeCompra(OrdenDeCompra orden, EstablecimientoComercialExtendidoConLogo sede, byte[] qrBytes, bool mostrarEncabezadoTestigo, ModoImpresionCaracteristicasEnum modoImpresionCaracteristicas) : base(orden, sede, qrBytes, mostrarEncabezadoTestigo, modoImpresionCaracteristicas)
        {
            ResolucionAutorizacionSunat = "";
        }
    }
}

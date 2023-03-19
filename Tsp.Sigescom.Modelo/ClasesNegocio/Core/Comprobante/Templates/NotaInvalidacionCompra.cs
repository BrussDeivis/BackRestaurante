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
    public class NotaInvalidacionCompra : DocumentoElectronicoCompra
    {
        public override string NombreTipo { get; set; } = "NOTA DE INVALIDACIÓN DE COMPRA";
        public NotaInvalidacionCompra()
        {


        }
        public NotaInvalidacionCompra(OrdenDeCompra orden, EstablecimientoComercialExtendidoConLogo sede, byte[] qrBytes, bool mostrarEncabezadoTestigo, ModoImpresionCaracteristicasEnum modoImpresionCaracteristicas) : base(orden, sede, qrBytes, mostrarEncabezadoTestigo, modoImpresionCaracteristicas)
        {
            ResolucionAutorizacionSunat = "";
        }
    }
}

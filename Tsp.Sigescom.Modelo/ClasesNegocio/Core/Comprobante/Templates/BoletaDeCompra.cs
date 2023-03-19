using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Humanizer;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;

namespace Tsp.Sigescom.Modelo.Entidades.ComprobantesModel
{
    public class BoletaDeCompra: DocumentoElectronicoCompra
    {
        public override string NombreTipo { get; set; } = "BOLETA DE VENTA ELECTRÓNICA";

        public BoletaDeCompra()
        {

        }

        public BoletaDeCompra(OrdenDeCompra orden, EstablecimientoComercialExtendido sede, byte[] qrBytes, bool mostrarEncabezadoTestigo, ModoImpresionCaracteristicasEnum modoImpresionCaracteristicas) : base(orden, sede, qrBytes, mostrarEncabezadoTestigo, modoImpresionCaracteristicas)
        {

        }

        public static List<BoletaDeCompra> Convert(List<OrdenDeCompra> ordenes, EstablecimientoComercialExtendido sede, ModoImpresionCaracteristicasEnum modoImpresionCaracteristicas)
        {
            List<BoletaDeCompra> resultado = new List<BoletaDeCompra>();
            foreach (var item in ordenes)
            {
                resultado.Add(new BoletaDeCompra(item, sede, null, false, modoImpresionCaracteristicas));
            }
            return resultado;
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Pedido;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.ComprobantesModel;

namespace Tsp.Sigescom.Modelo.Entidades.ComprobantesModel
{
    public class DocumentoDePedido:DocumentoElectronicoVenta
    {
        public override string NombreTipo { get; set; } = "TICKET DE PEDIDO";
        public bool MostrarIgv { get; set; }
        public bool MostrarInformacionAdicional80 { get; set; }
        public string InformacionAdicional80 { get; set; }

        public DocumentoDePedido(OrdenDePedido orden, EstablecimientoComercialExtendidoConLogo sede, EstablecimientoComercialExtendido establecimiento, byte[] qrBytes, bool mostrarEncabezadoTestigo, ModoImpresionCaracteristicasEnum modoImpresionCaracteristicas):base(orden, sede, establecimiento, qrBytes, mostrarEncabezadoTestigo, modoImpresionCaracteristicas)
        {
            MostrarIgv = orden.Igv() > 0;
            MostrarInformacionAdicional80 = !String.IsNullOrEmpty(AplicacionSettings.Default.InformacionAdicionalCotizacion80);
            InformacionAdicional80 = AplicacionSettings.Default.InformacionAdicionalCotizacion80;
        }
    }
}

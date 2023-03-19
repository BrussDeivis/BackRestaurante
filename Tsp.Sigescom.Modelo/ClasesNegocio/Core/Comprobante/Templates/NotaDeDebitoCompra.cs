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
    public class NotaDeDebitoCompra : DocumentoElectronicoCompra
    {
        public override string NombreTipo { get; set; } = "NOTA DE DÉBITO ELECTRÓNICA";
        public string TipoDeNotaDebito { get; set; }
        public Referencia Discrepancia { get; set; }
        public Relacionado DocumentoRelacionado { get; set; }

        public NotaDeDebitoCompra(OperacionDeCompra operacion, EstablecimientoComercialExtendidoConLogo sede, List<Detalle_maestro> tiposDeNotaDeDebito, byte[] qrBytes, bool mostrarEncabezadoTestigo, ModoImpresionCaracteristicasEnum modoImpresionCaracteristicas) : base(operacion, sede, qrBytes, mostrarEncabezadoTestigo, modoImpresionCaracteristicas)
        {
            TipoDeNotaDebito = tiposDeNotaDeDebito.SingleOrDefault(t => t.codigo == operacion.CodigoSunatDeTransaccion()).nombre;
            Discrepancia = new Referencia
            {
                NroReferencia = operacion.OperacionDeReferencia().Comprobante().NumeroDeSerie + "-" + operacion.OperacionDeReferencia().Comprobante().NumeroDeComprobante,
                Tipo = operacion.CodigoSunatDeTransaccion(),
                Descripcion = operacion.Comentario,
            };
            DocumentoRelacionado = new Relacionado
            {
                NroDocumento = operacion.OperacionDeReferencia().Comprobante().NumeroDeSerie + "-" + operacion.OperacionDeReferencia().Comprobante().NumeroDeComprobante,
                TipoDocumento = operacion.OperacionDeReferencia().Comprobante().CodigoTipo,
            };
        }

        public static List<NotaDeDebitoCompra> Convert(List<OperacionDeCompra> operaciones, EstablecimientoComercialExtendidoConLogo sede, List<Detalle_maestro> tiposDeNotaDeDebito, ModoImpresionCaracteristicasEnum modoImpresionCaracteristicas)
        {
            List<NotaDeDebitoCompra> resultado = new List<NotaDeDebitoCompra>();
            foreach (var item in operaciones)
            {
                resultado.Add(new NotaDeDebitoCompra(item, sede, tiposDeNotaDeDebito, null, false, modoImpresionCaracteristicas));
            }
            return resultado;
        }
    }
}
//public string TipoDeDocumentoQueSeModifica { get; set; }
//public string SerieDelDocumentoQueModifica { get; set; }
//public string NumeroDelDocumentoQueModifica { get; set; }
//public string DocumentoDeReferencia { get; set; }

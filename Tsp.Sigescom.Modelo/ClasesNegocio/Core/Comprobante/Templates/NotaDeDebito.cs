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
    public class NotaDeDebito : DocumentoElectronicoVenta
    {
        public override string NombreTipo { get; set; } = "NOTA DE DÉBITO ELECTRÓNICA";
        public string TipoDeNotaDebito { get; set; }
        public Referencia Discrepancia { get; set; }
        public Relacionado DocumentoRelacionado { get; set; }

        public NotaDeDebito(OperacionDeVenta operacion, EstablecimientoComercialExtendido sede, EstablecimientoComercialExtendido establecimiento, List<Detalle_maestro> tiposDeNotaDeDebito, byte[] qrBytes, bool mostrarEncabezadoTestigo, ModoImpresionCaracteristicasEnum modoImpresionCaracteristicas) : base(operacion, sede, establecimiento, qrBytes, mostrarEncabezadoTestigo, modoImpresionCaracteristicas)
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

        public NotaDeDebito(OperacionDeVenta operacion, EstablecimientoComercialExtendidoConLogo sede, EstablecimientoComercialExtendido establecimiento, List<Detalle_maestro> tiposDeNotaDeDebito, byte[] qrBytes, bool mostrarEncabezadoTestigo, ModoImpresionCaracteristicasEnum modoImpresionCaracteristicas) : base(operacion, sede, establecimiento, qrBytes, mostrarEncabezadoTestigo, modoImpresionCaracteristicas)
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

        public static List<NotaDeDebito> Convert(List<OperacionDeVenta> operaciones, EstablecimientoComercialExtendido sede, List<Detalle_maestro> tiposDeNotaDeDebito, ModoImpresionCaracteristicasEnum modoImpresionCaracteristicas)
        {
            List<NotaDeDebito> resultado = new List<NotaDeDebito>();
            foreach (var item in operaciones)
            {
                resultado.Add(new NotaDeDebito(item, sede, new EstablecimientoComercialExtendidoConLogo(item.Transaccion().Actor_negocio2.Actor_negocio2), tiposDeNotaDeDebito, null, false, modoImpresionCaracteristicas));
            }
            return resultado;
        }
    }
}
//public string TipoDeDocumentoQueSeModifica { get; set; }
//public string SerieDelDocumentoQueModifica { get; set; }
//public string NumeroDelDocumentoQueModifica { get; set; }
//public string DocumentoDeReferencia { get; set; }

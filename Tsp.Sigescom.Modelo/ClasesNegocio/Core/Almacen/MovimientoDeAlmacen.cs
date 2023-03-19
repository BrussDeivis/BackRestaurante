using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class MovimientoDeAlmacen : OperacionGenericaNivel3
    {
        public MovimientoDeAlmacen() { }

        public MovimientoDeAlmacen(Transaccion transaccion) : base(transaccion)
        {

        }

        //private int idComprobanteSeleccionado;
        //private int idSerieSeleccionada;
        //private bool esPropio;
        //private string serieIngresada;
        //private int numeroIngresado;
        //private DateTime fechaInicioTraslado;
        //private int idTransportista;
        //private string marcaYPlaca;
        //private string numeroLicencia;
        //private int idModalidadTransporte;
        //private int idMotivoTraslado;
        //private string direccionOrigen;
        //private int idUbigeoOrigen;
        //private string direccionDestino;
        //private int idUbigeoDestino;

        public int IdTercero { get; set; }
        public int IdComprobanteSeleccionado { get; set; }
        public int IdSerieSeleccionada { get; set; }
        public bool EsPropio { get; set; }
        public string SerieIngresada { get; set; }
        public int NumeroIngresado { get; set; }
        public DateTime FechaInicioTraslado { get; set; }
        public int IdTransportista { get; set; }
        public string Placa { get; set; }
        public int IdConductor { get; set; }
        public string NumeroLicencia { get; set; }
        public int IdModalidadTransporte { get; set; }
        public int IdMotivoTraslado { get; set; }
        public string DescripcionMotivo { get; set; }
        public decimal PesoBrutoTotal { get; set; }
        public int NumeroBultos { get; set; }
        public string DireccionOrigen { get; set; }
        public int IdUbigeoOrigen { get; set; }
        public string DireccionDestino { get; set; }
        public int IdUbigeoDestino { get; set; }
        public string DocumentoReferencia { get; set; }

        public DateTime? FechaInicioDeTraslado()
        {
            var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroFechaInicioTransporte);
            return parametro != null ? (DateTime?)System.Convert.ToDateTime(parametro.valor) : null;
        }
        public int? IdTransporte()
        {
            var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroIdTransportista);
            return parametro != null ? (int?)System.Convert.ToInt32(parametro.valor) : null;
        }
        public string PlacaTransporte()
        {
            var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroPlacaMarcaTransportista);
            return parametro?.valor;
        }
        public int? IdConductorTransporte()
        {
            var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroIdConductor);
            return parametro != null ? (int?)System.Convert.ToInt32(parametro.valor) : null;
        }
        public string LicenciaConductorTransporte()
        {
            var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroNumeroLicenciaTransportista);
            return parametro?.valor;
        }
        public int? IdModalidadDeTransporte()
        {
            var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroIdModalidadTransporte);
            return parametro != null ? (int?)System.Convert.ToInt32(parametro.valor) : null; ;
        }
        public int? IdMotivoDeTransporte()
        {
            var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroIdMotivoTransporte);
            return parametro != null ? (int?)System.Convert.ToInt32(parametro.valor) : null;
        }
        public string DescripcionDeMotivo()
        {
            var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroDescripcionMotivoTraslado);
            return parametro?.valor;
        }
        public decimal PesoBrutoTotalTransporte()
        {
            var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroPesoBrutoTotal);
            return parametro != null ? System.Convert.ToDecimal(parametro.valor) : 0;
        }
        public int NumeroBultosTransporte()
        {
            var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroNumeroBultos);
            return parametro != null ? System.Convert.ToInt32(parametro.valor) : 0;
        }
        public string DireccionOrigenDeTraslado()
        {
            var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroDireccionOrigenTraslado);
            return parametro?.valor;
        }
        public string IdUbigeoOrigenDeTraslado()
        {
            var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroIdUbigeoOrigenTraslado);
            return parametro?.valor;
        }
        public string DireccionDestinoDeTraslado()
        {
            var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroDireccionDestinoTraslado);
            return parametro?.valor;
        }
        public string IdUbigeoDestinoDeTraslado()
        {
            var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroIdUbigeoDestinoTraslado);
            return parametro?.valor;
        }
        public string DocumentoDeReferencia()
        {
            var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroDocumentoReferenciaDeGuia);
            return parametro?.valor;
        }
        public string Observacion()
        {
            return this.transaccion.comentario;
        }
        public string UbigeoOrigen { get; set; }
        public string UbigeoDestino { get; set; }
        public int IdTipoComprobante
        {
            get { return this.transaccion.Comprobante.Detalle_maestro.id; }
        }

        public string UrlDocumentoSunat
        {
            get { return this.transaccion.informacion; }
        }

        public List<DetalleDeOperacion> Detalles()
        {
            return DetalleDeOperacion.Convert(this.transaccion.Detalle_transaccion.ToList());
        }

        public OperacionGenericaNivel3 OrdenDeOperacion()
        {
            return new OperacionGenericaNivel3(this.transaccion.Transaccion3);
        }

        public long IdOperacionReferencia()
        {
            return this.transaccion.Transaccion3.id;
        }

        public int IdTipoTransaccionOperacionReferencia()
        {
            return this.transaccion.Transaccion3.id_tipo_transaccion;
        }

        public static List<MovimientoDeAlmacen> Convertir(List<Transaccion> transacciones)
        {
            List<MovimientoDeAlmacen> movimientos = new List<MovimientoDeAlmacen>();
            foreach (var transaccion in transacciones)
            {
                movimientos.Add(new MovimientoDeAlmacen(transaccion));
            }
            return movimientos;
        }

        public bool EstaInvalidada()
        {
            return this.transaccion.id_estado_actual == MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado;
        }

        public String MotivoInvalidacion()
        {
            return EstaInvalidada() ? this.transaccion.Estado_transaccion.Where(t => t.id_estado == MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado).FirstOrDefault().comentario : string.Empty;
        }
    }
}






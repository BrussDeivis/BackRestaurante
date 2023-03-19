using Tsp.Sigescom.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Globalization;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class OperacionDeVenta : OperacionGenericaNivel3
    {
        /// <summary>
        /// se pueden anular con nota de credito
        /// </summary>
        static int[] idsTiposDeComprobantesAnulables = { MaestroSettings.Default.IdDetalleMaestroComprobanteFactura, MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta };

        /// <summary>
        /// la numeracion del comprobante de la transaccion se invalidar con nota interna
        /// </summary>
        static int[] idsTiposDeComprobantesInvalidables = { MaestroSettings.Default.IdDetalleMaestroComprobanteFactura, MaestroSettings.Default.IdDetalleMaestroComprobanteBoleta,
            MaestroSettings.Default.IdDetalleMaestroComprobanteNotaVentaInterna};

        public OperacionDeVenta()
        {

        }

        //public bool PagarInicialAlConfirmar()
        //{
        //    var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroPagarInicialAlConfirmar);
        //    return parametro != null ? parametro.valor == "1" ? true : false : false;
        //}

        public String AliasCliente()
        {
            var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroAliasCliente);
            return parametro != null ? Convert.ToString(parametro.valor) : "";
        }

        public ModoOperacionEnum TipoDeVenta()
        {
            var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroTipoVenta);
            return (ModoOperacionEnum)Convert.ToInt32(parametro.valor); ;
        }

        public ModoPago ModoDePago()
        {
            var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroModoDePago);
            return (ModoPago)Convert.ToInt32(parametro.valor);
        }

        //public decimal Icbper()
        //{
        //    var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroIcbper);
        //    return parametro != null ? Convert.ToInt32(parametro.valor) : 0;
        //}



        public OperacionDeVenta(Transaccion transaccion) : base(transaccion)
        {

        }

        public bool EsComprobanteQueModificaAOtro
        {
            get
            {
                return this.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito || this.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaDeDebito;
            }
        }

        public bool EsInvalidada
        {
            get
            {
                return (this.IdEstadoActual == MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado || (this.EstadoAnteriorAlActual != null && this.EstadoAnteriorAlActual.id == MaestroSettings.Default.IdDetalleMaestroEstadoInvalidado));
            }
        }

        public String MotivoInvalidacion()
        {
            var operacionInvalidacion = this.transaccion.Transaccion11.Where(t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenDeInvalidacionDeVenta).FirstOrDefault();
            var operacionAnulacion = this.transaccion.Transaccion11.Where(t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenDeAnulacionDeVenta).FirstOrDefault();
            return operacionInvalidacion == null ? (operacionAnulacion == null ? "" : operacionAnulacion.comentario) : operacionInvalidacion.comentario;
        }

        public bool TieneNotaEmitida()
        {
            return this.transaccion.Transaccion11.Where(t => Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasSoloNotasDeCreditoYDebito.Contains(t.id_tipo_transaccion)).Count() > 0;
        }

        public DateTime? FechaEmisionComprobantePagoQueSeModifica
        {
            get
            {
                return (this.EsComprobanteQueModificaAOtro && !this.EsInvalidada ? (DateTime?)this.OperacionDeReferencia().FechaEmision : null);
            }
        }

        //public string FechaEmisionComprobantePagoQueSeModifica_
        //{
        //    get
        //    {
        //        return (FechaEmisionComprobantePagoQueSeModifica != null ? ((DateTime)FechaEmisionComprobantePagoQueSeModifica).ToString("dd/MM/yyyy") : "");
        //    }
        //}

        public string TipoComprobantePagoQueSeModifica
        {
            get
            {
                return (EsComprobanteQueModificaAOtro && !EsInvalidada ? this.OperacionDeReferencia().Comprobante().CodigoTipo : "");
            }
        }

        public string NumeroSerieComprobantePagoQueSeModificaCódigoDependenciaAduanera
        {
            get
            {
                return (EsComprobanteQueModificaAOtro && !EsInvalidada ? this.OperacionDeReferencia().Comprobante().NumeroDeSerie : "");
            }
        }

        public int NumeroComprobantePagoQueSeModificaNúmeroDUA
        {
            get
            {
                return (EsComprobanteQueModificaAOtro && !EsInvalidada ? this.OperacionDeReferencia().Comprobante().NumeroDeComprobante : 0);
            }
        }
        /// <summary>
        /// Codigo Sunat para el tipo de documento de identidad
        /// </summary>
        public string CodigoSunatTipoDocumentoIdentidadCliente
        {
            get
            {
                return (IdCliente == ActorSettings.Default.IdClienteGenerico ? "" : Cliente().CodigoSunatTipoDocumentoIdentidad());
            }
        }

        public string TipoDocumentoIdentidadCliente
        {
            get
            {
                return (IdCliente == ActorSettings.Default.IdClienteGenerico ? "" : Cliente().CodigoTipoDocumentoIdentidad());
            }
        }
        public string NumeroDocumentoIdentidadCliente
        {
            get
            {
                return (IdCliente == ActorSettings.Default.IdClienteGenerico ? "" : Cliente().DocumentoIdentidad);
            }
        }

        public string ApellidosYNombres
        {
            get
            {
                return (IdCliente == ActorSettings.Default.IdClienteGenerico ? "" : Cliente().RazonSocial);
            }
        }

        public decimal BaseImponibleOperacionGravadaConSigno
        {
            get
            {
                if (FechaEmisionComprobantePagoQueSeModifica != null && FechaEmisionComprobantePagoQueSeModifica.Value.Month != FechaEmision.Month && IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito)
                {
                    return 0;
                }
                else
                {
                    //solo si es nota de credito se pone negativo. 
                    if (this.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito)
                    {
                        return (Igv() > 0 ? -ValorDeVenta : 0);
                    }
                    else//en caso de factura, boleta o nota de debito, es positivo
                    {
                        return (Igv() > 0 ? ValorDeVenta : 0);
                    }
                }
            }
        }

        public decimal BaseImponibleOperacionGravada
        {
            get
            {
                return (Igv() > 0 ? ValorDeVenta : 0);
            }
        }

        //public decimal BaseImponibleOperacionGravadaEnLibroElectronico
        //{
        //    get
        //    {
        //        return (Igv() > 0 && !EsInvalidada ? ValorDeVenta : 0);
        //    }
        //}
        public decimal DescuentoBaseImponible
        {
            get
            {
                //registrar aqui la base imponible de la orden, cuando es una nota de credito y ademas el documento relacionado es de un periodo anterior el debe ir valor negativo.
                if (FechaEmisionComprobantePagoQueSeModifica != null && FechaEmisionComprobantePagoQueSeModifica.Value.Month != FechaEmision.Month && IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito)
                {
                    return -ValorDeVenta;
                }
                else
                {
                    return 0;
                }
            }
        }


        public bool ElComprobanteOriginalEsDeUnperiodoAnterior()
        {
            return (this.OperacionDeReferencia().FechaEmision.Year <= this.FechaEmision.Year && this.OperacionDeReferencia().FechaEmision.Month < this.FechaEmision.Month);

        }
        //public decimal DescuentoBaseImponibleEnLibroElectronico
        //{
        //    get
        //    {
        //        return (!EsInvalidada ? Descuento() : 0);
        //    }
        //}

        public decimal ImpuestoGeneralVentasYOImpuestoPromocionMunicipal
        {
            get
            {
                if (FechaEmisionComprobantePagoQueSeModifica != null && FechaEmisionComprobantePagoQueSeModifica.Value.Month != FechaEmision.Month && IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito)
                {
                    return 0;
                }
                else
                {
                    //En caso de nota de credito, se considera negativo
                    if (this.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito)
                    {
                        return (Igv() > 0 ? -Igv() : 0);
                    }
                    else//en caso de factura, boleta o nota de debito, es positivo
                    {
                        return (Igv() > 0 ? Igv() : 0);
                    }
                }
            }
        }

        //public decimal ImpuestoGeneralVentasYOImpuestoPromocionMunicipalEnLibroElectronico
        //{
        //    get
        //    {
        //        return (Igv() > 0 && !EsInvalidada ? Igv() : 0);

        //    }
        //}

        //public string ImpuestoGeneralVentasYOImpuestoPromocionMunicipal_
        //{
        //    get
        //    {
        //        return (ImpuestoGeneralVentasYOImpuestoPromocionMunicipal != 0 ? ImpuestoGeneralVentasYOImpuestoPromocionMunicipal.ToString("0.##") : "");
        //    }
        //}

        public decimal DescuentoImpuestoGeneralVentasImpuestoPromociónMunicipal
        {
            get
            {
                //registrar aqui la base imponible de la orden, cuando es una nota de credito y ademas el documento relacionado es de un periodo anterior el debe ir valor negativo.
                if (FechaEmisionComprobantePagoQueSeModifica != null && FechaEmisionComprobantePagoQueSeModifica.Value.Month != FechaEmision.Month && IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito)
                {
                    return -Igv();
                }
                else
                {
                    return 0;
                }
                //if (this.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito )
                //{
                //    return -Igv();
                //}
                //else if (this.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaDeDebito)
                //{
                //    return Igv();
                //}
                //else
                //{
                //    return 0;
                //}
            }
        }

        public decimal ImporteTotalOperacionExoneradaConSigno
        {
            get
            {
                //return (Igv() <= 0 ? Total : 0);

                //solo si es nota de credito se pone negativo. 
                if (this.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito)
                {
                    return Igv() <= 0 ? -ValorDeVenta : 0;
                }
                else//en caso de factura, boleta o nota de debito, es positivo
                {
                    return Igv() <= 0 ? ValorDeVenta : 0;
                }


            }
        }

        public decimal ImporteTotalOperacionExonerada
        {
            get
            {
                return (Igv() <= 0 ? ValorDeVenta : 0);
            }
        }

        //public decimal ImporteTotalOperacionExoneradaEnLibroElectronico
        //{
        //    get
        //    {
        //        return (Igv() <= 0 && !EsInvalidada ? Total : 0);
        //    }
        //}


        //public string ImporteTotalOperacionExonerada_
        //{
        //    get
        //    {
        //        return (ImporteTotalOperacionExonerada != 0 ? ImporteTotalOperacionExonerada.ToString("0.##") : "");
        //    }
        //}

        public decimal ImporteTotalOperacionInafecta
        {
            get
            {
                return 0;
            }
        }

        public decimal ImpuestoSelectivoConsumo
        {
            get
            {
                return Isc();
            }
        }

        //public decimal ImpuestoSelectivoConsumoEnLibroElectronico
        //{
        //    get
        //    {
        //        return (!EsInvalidada ? Isc() : 0);
        //    }
        //}

        public bool AplicaLeyDeAmazonia
        {
            get
            {
                return (TransaccionSettings.Default.AplicaLeyAmazonia && Igv() <= 0);
            }
        }

        public bool AplicaIGVCuandoAplicaLeyAmazonia
        {
            get
            {
                return (TransaccionSettings.Default.AplicaLeyAmazonia && Igv() > 0);
            }
        }
        public bool EsVentaRegistradaConFechaPasada
        {
            get
            {
                return (this.FechaEmision < this.FechaDeRegistro);
            }
        }
        //public string ImpuestoSelectivoConsumo_
        //{
        //    get
        //    {
        //        return (ImpuestoSelectivoConsumo != 0 ? ImpuestoSelectivoConsumo.ToString("0.##") : "");
        //    }
        //}

        public decimal BaseImponibleOperacionGravadaImpuestoVentasArrozPilado
        {
            get
            {
                return 0;
            }
        }

        public decimal ImpuestoVentasArrozPilado
        {
            get
            {
                return 0;
            }
        }

        public decimal OtrosConceptosTributosCargosNoFormanParteBaseImponible
        {
            get
            {
                return 0;
            }
        }

        public decimal ImporteTotalComprobantePago
        {
            get
            {
                //return Total;
                return (this.IdTipoComprobante == MaestroSettings.Default.IdDetalleMaestroComprobanteNotaCredito) ? -Total : Total;
            }
        }

        //public decimal ImporteTotalComprobantePagoEnLibroElectronico
        //{
        //    get
        //    {
        //        return (!EsInvalidada ? Total : 0);
        //    }
        //}

        //public string ImporteTotalComprobantePago_
        //{
        //    get
        //    {
        //        return (ImporteTotalComprobantePago != 0 ? ImporteTotalComprobantePago.ToString("0.##") : "");
        //    }
        //}

        //public decimal TipoDeCambio
        //{
        //    get
        //    {
        //        return (IdMoneda == MaestroSettings.Default.IdDetalleMaestroMonedaDolares ? base.TipoDeCambio : 1);
        //    }
        //}

        //public decimal TipoCambioEnLibroElectronico
        //{
        //    get
        //    {
        //        return (IdMoneda == MaestroSettings.Default.IdDetalleMaestroMonedaDolares && !EsInvalidada ? TipoDeCambio : 0);
        //    }
        //}

        //public string TipoCambio_
        //{
        //    get
        //    {
        //        return (TipoCambio != 0 ? TipoCambio.ToString("0.###") : "");
        //    }
        //}
        //Aqui acaba lo agregado por CERNA

        public Venta Venta()
        {
            return new Venta(this.transaccion.Transaccion2);
        }

        public DateTime FechaOrden()
        {
            return this.transaccion.fecha_inicio;
        }

        public string Observacion()
        {
            return this.transaccion.comentario;
        }

        /// <summary>
        /// puede ser la orden de venta de referencia para una detraccion.
        /// </summary>
        /// <returns></returns>
        public OperacionDeVenta OperacionDeReferencia()
        {
            return this.transaccion.Transaccion3 != null ? new OperacionDeVenta(this.transaccion.Transaccion3) : null;
        }

        public List<DetalleDeOperacion> Detalles()
        {
            return DetalleDeOperacion.Convert(this.transaccion.Detalle_transaccion.ToList());
        }

        public List<DetalleDeOperacion> DetalleContemplandoUnificacionDeConceptos()
        {
            return new List<DetalleDeOperacion>() { new DetalleDeOperacion(new Detalle_transaccion(1, 1, DetalleUnificado(), Total, Total, null, 0, null, null, Isc(), Igv(), Descuento())) };
        }

        public List<Cuota> Cuotas()
        {
            return this.transaccion.Cuota.ToList();
        }


        public int IdCliente
        {
            get { return this.transaccion.id_actor_negocio_externo; }
        }

        public int IdVendedor
        {
            get { return this.transaccion.id_empleado; }
        }

        public int IdPuntoDeVenta
        {
            get { return this.transaccion.id_actor_negocio_interno; }
        }

        public long IdVenta
        {
            get { return (long)this.transaccion.id_transaccion_padre; }
        }
        public int IdTipoOperacionVenta
        {
            get { return this.transaccion.id_tipo_transaccion; }
        }

        public int IdTipoComprobante
        {
            get { return this.transaccion.Comprobante.id_tipo_comprobante; }
        }

        //public Empleado Empleado()
        //{
        //    return new Empleado(this.transaccion.Actor_negocio);
        //}
        public Cliente Cliente()
        {
            return new Cliente(this.transaccion.Actor_negocio1);
        }

        //public CentroDeAtencion CentroDeAtencion()
        //{
        //    return new CentroDeAtencion(this.transaccion.Actor_negocio2);
        //}
        public decimal PagoEnFechaEmision()
        {
            var pagoInicio = this.transaccion.Transaccion2.Transaccion1.SingleOrDefault(st => st.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionCobranzaFacturasClientes && st.fecha_inicio == this.FechaEmision);
            return pagoInicio == null ? 0 : pagoInicio.importe_total;
        }

        public bool HayPagos()
        {
            return this.transaccion.Transaccion2.Transaccion1.Any(st => st.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionCobranzaFacturasClientes);
        }

        public decimal Saldo()
        {
            var pago = HayPagos() ? this.transaccion.Transaccion2.Transaccion1.Where(st => st.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionCobranzaFacturasClientes).Sum(st => st.importe_total) : 0;
            return this.transaccion.importe_total - pago;
        }

        public static List<OperacionDeVenta> convert(List<Transaccion> transaciones)
        {
            List<OperacionDeVenta> ordenes = new List<OperacionDeVenta>();
            foreach (var transaccion in transaciones)
            {
                ordenes.Add(new OperacionDeVenta(transaccion));
            }
            return ordenes;
        }

        public string NombreUsuario()
        {
            var estado = this.transaccion.Estado_transaccion.SingleOrDefault(est => est.id_estado == MaestroSettings.Default.IdDetalleMaestroEstadoAnulado);
            int index = estado.Actor_negocio.Actor.correo.IndexOf("@");
            return estado != null ? index > -1 ? estado.Actor_negocio.Actor.correo.Substring(0, index) : null : null;
        }

        public bool EsAnulableConNotaInterna()
        {
            return (MaestroSettings.Default.IdDetalleMaestroComprobanteNotaVentaInterna == IdTipoComprobante) || (idsTiposDeComprobantesInvalidables.Contains(IdTipoComprobante)
                && IdTipoTransaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta
                && FechaEmision.AddDays(FacturacionElectronicaSettings.Default.PlazoEnDiasParaInvalidarComprobanteElectronico) >= DateTimeUtil.FechaActual()
                && IdEstadoActual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado
                && !TieneNotaEmitida());
        }

        public bool EsAnulableConNotaDeCredito()
        {
            return IdTipoTransaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta
            && idsTiposDeComprobantesAnulables.Contains(IdTipoComprobante)
            //&& EstaTransmitido() 
            && (IdEstadoActual == MaestroSettings.Default.IdDetalleMaestroEstadoConfirmado);
        }

        public bool EsOrdenDeVenta()
        {
            return IdTipoTransaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta;
        }

        public String DetalleUnificado()
        {
            var parametro = transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroDetalleUnificado);
            return parametro != null ? Convert.ToString(parametro.valor) : "";
        }

        public bool TieneLosDetallesUnificados()
        {
            var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroDetalleUnificado);
            return parametro != null;
        }

        public String CodigoSunatDeTransaccion()
        {
            var parametro = this.transaccion.Parametro_transaccion.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroCodigoTransaccionSunat);
            return parametro != null ? Convert.ToString(parametro.valor) : "";
        }



        public static List<OperacionDeVenta> Convert_(List<Transaccion> transacciones)
        {
            List<OperacionDeVenta> operacionesDeVenta = new List<OperacionDeVenta>();
            foreach (var transaccion in transacciones)
            {
                operacionesDeVenta.Add(new OperacionDeVenta(transaccion));
            }
            return operacionesDeVenta;
        }

        public int PuntosAcumulados()
        {
            return (int)this.transaccion.cantidad4;
        }

        public int PuntosGanados()
        {
            return (int)this.transaccion.cantidad1;
        }

        public IndicadorImpactoAlmacen IndicadorImpactoAlmacen
        {
            get { return (IndicadorImpactoAlmacen)this.transaccion.enum1; }
        }

        public int IdEstadoActualOrdenAlmacen
        {
            get { return this.transaccion.id_estado_actual_1 ?? 0; }
        }

        public int? IdGrupoCliente
        {
            get { return this.transaccion.id_actor_negocio_externo1; }
        }
    }
}

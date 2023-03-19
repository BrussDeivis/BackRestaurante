using System;
using System.Collections.Generic;
using System.Linq;
using OpenInvoicePeru.Comun.Dto.Modelos;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Config;
using System.Text;
using Newtonsoft.Json;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.ComprobantesModel;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Entidades.EFactura;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;

namespace Tsp.FacturacionElectronica.Logica
{
    public class GeneracionArchivosLogica : IGeneracionArchivosLogica
    {
        private readonly IOperacionLogica _operacionLogica;
        private readonly IMaestroLogica _maestroLogica;
        private readonly IFacturacionRepositorio _facturacionDatos = null;

        public GeneracionArchivosLogica(IOperacionLogica operacionLogica, IMaestroLogica maestroLogica, IFacturacionRepositorio facturacionDatos)
        {
            _operacionLogica = operacionLogica;
            _maestroLogica = maestroLogica;
            _facturacionDatos = facturacionDatos;
        }

        public DocumentoElectronico ObtenerDocumentoElectronicoFactura(Factura factura)
        {
            try
            {
                DocumentoElectronico documentoElectronico = new DocumentoElectronico
                {
                    Emisor = CrearEmisor(factura.Emisor),
                    Receptor = CrearReceptor(factura.Receptor),
                    IdDocumento = factura.Serie + "-" + factura.Numero,
                    FechaEmision = factura.FechaEmision.ToString("yyyy-MM-dd"),
                    HoraEmision = factura.FechaEmision.ToString("HH:mm:ss"),
                    Moneda = factura.CodigoSunatMoneda,
                    TipoDocumento = factura.CodigoSunatTipo,
                    TotalIgv = factura.Igv,
                    TotalIcbper = factura.Icbper,
                    TotalVenta = factura.ImporteTotal,
                    Gravadas = factura.ImporteOperacionGravada,
                    Exoneradas = factura.ImporteOperacionExonerada,
                    Inafectas = 0,
                    Gratuitas = 0,
                    DescuentoGlobal = factura.Descuento,
                    TotalOtrosTributos = 0,
                    MontoEnLetras = factura.ImporteTotalEnLetras,
                    Observacion = factura.Observacion.Length > 200 ? factura.Observacion.Substring(0, 200) : factura.Observacion,
                    Items = factura.MostrarPlaca ? CrearDetalles(factura.Detalles, factura.ValorIcbper, factura.Placa) : CrearDetalles(factura.Detalles, factura.ValorIcbper),
                    FormaPago = factura.FormaPago,
                    MontoACredito = factura.MontoACredito,
                    Cuotas = GenerarCuotas(factura.Cuotas)
                };
                return documentoElectronico;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public DocumentoElectronico ObtenerDocumentoElectronicoBoleta(BoletaDeVenta boletaDeVenta)
        {
            try
            {
                DocumentoElectronico documentoElectronico = new DocumentoElectronico
                {
                    Emisor = CrearEmisor(boletaDeVenta.Emisor),
                    Receptor = CrearReceptor(boletaDeVenta.Receptor),
                    IdDocumento = boletaDeVenta.Serie + "-" + boletaDeVenta.Numero,
                    FechaEmision = boletaDeVenta.FechaEmision.ToString("yyyy-MM-dd"),
                    HoraEmision = boletaDeVenta.FechaEmision.ToString("HH:mm:ss"),
                    Moneda = boletaDeVenta.CodigoSunatMoneda,
                    TipoDocumento = boletaDeVenta.CodigoSunatTipo,

                    TotalIgv = boletaDeVenta.Igv,
                    TotalIcbper = boletaDeVenta.Icbper,
                    TotalVenta = boletaDeVenta.ImporteTotal,
                    Gravadas = boletaDeVenta.ImporteOperacionGravada,
                    Exoneradas = boletaDeVenta.ImporteOperacionExonerada,
                    DescuentoGlobal = boletaDeVenta.Descuento,
                    TotalOtrosTributos = 0,
                    MontoEnLetras = boletaDeVenta.ImporteTotalEnLetras,
                    Observacion = boletaDeVenta.Observacion.Length > 200 ? boletaDeVenta.Observacion.Substring(0, 200) : boletaDeVenta.Observacion,
                    Items = boletaDeVenta.MostrarPlaca ? CrearDetalles(boletaDeVenta.Detalles, boletaDeVenta.ValorIcbper, boletaDeVenta.Placa) : CrearDetalles(boletaDeVenta.Detalles, boletaDeVenta.ValorIcbper)
                };
                return documentoElectronico;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public DocumentoElectronico ObtenerDocumentoElectronicoNotaCredito(NotaDeCredito notaDeCredito)
        {
            try
            {
                DocumentoElectronico documentoElectronico = new DocumentoElectronico
                {
                    Emisor = CrearEmisor(notaDeCredito.Emisor),
                    Receptor = CrearReceptor(notaDeCredito.Receptor),
                    IdDocumento = notaDeCredito.Serie + "-" + notaDeCredito.Numero,
                    FechaEmision = notaDeCredito.FechaEmision.ToString("yyyy-MM-dd"),
                    HoraEmision = notaDeCredito.FechaEmision.ToString("HH:mm:ss"),
                    Moneda = notaDeCredito.CodigoSunatMoneda,
                    TipoDocumento = notaDeCredito.CodigoSunatTipo,
                    TotalIgv = notaDeCredito.Igv,
                    TotalIcbper = notaDeCredito.Icbper,
                    TotalVenta = notaDeCredito.ImporteTotal,
                    Gravadas = notaDeCredito.ImporteOperacionGravada,
                    Exoneradas = notaDeCredito.ImporteOperacionExonerada,
                    DescuentoGlobal = notaDeCredito.Descuento,
                    MontoEnLetras = notaDeCredito.ImporteTotalEnLetras,
                    Observacion = notaDeCredito.Observacion.Length > 200 ? notaDeCredito.Observacion.Substring(0, 200) : notaDeCredito.Observacion,
                    Items = CrearDetalles(notaDeCredito.Detalles, notaDeCredito.ValorIcbper),
                    Discrepancias = CrearDiscrepancia(notaDeCredito.Discrepancia),
                    Relacionados = CrearRelacionado(notaDeCredito.DocumentoRelacionado)
                };
                return documentoElectronico;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public DocumentoElectronico ObtenerDocumentoElectronicoNotaDebito(NotaDeDebito notaDeDebito)
        {
            try
            {
                DocumentoElectronico documentoElectronico = new DocumentoElectronico
                {
                    Emisor = CrearEmisor(notaDeDebito.Emisor),
                    Receptor = CrearReceptor(notaDeDebito.Receptor),
                    IdDocumento = notaDeDebito.Serie + "-" + notaDeDebito.Numero,
                    FechaEmision = notaDeDebito.FechaEmision.ToString("yyyy-MM-dd"),
                    HoraEmision = notaDeDebito.FechaEmision.ToString("HH:mm:ss"),
                    Moneda = notaDeDebito.CodigoSunatMoneda,
                    TipoDocumento = notaDeDebito.CodigoSunatTipo,
                    TotalIgv = notaDeDebito.Igv,
                    TotalIcbper = notaDeDebito.Icbper,
                    TotalVenta = notaDeDebito.ImporteTotal,
                    Gravadas = notaDeDebito.ImporteOperacionGravada,
                    Exoneradas = notaDeDebito.ImporteOperacionExonerada,
                    DescuentoGlobal = notaDeDebito.Descuento,
                    MontoEnLetras = notaDeDebito.ImporteTotalEnLetras,
                    Observacion = notaDeDebito.Observacion.Length > 200 ? notaDeDebito.Observacion.Substring(0, 200) : notaDeDebito.Observacion,
                    Items = CrearDetalles(notaDeDebito.Detalles, notaDeDebito.ValorIcbper),
                    Discrepancias = CrearDiscrepancia(notaDeDebito.Discrepancia),
                    Relacionados = CrearRelacionado(notaDeDebito.DocumentoRelacionado)
                };
                return documentoElectronico;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public GuiaRemision ObtenerDocumentoElectronicoGuiaDeRemision(GuiaDeRemision guiaDeRemision)
        {
            try
            {
                GuiaRemision guiaRemision = new GuiaRemision
                {
                    Remitente = CrearContribuyente(guiaDeRemision.Emisor),
                    Destinatario = MaestroSettings.Default.CodigoDetalleMaestroMotivoDeTrasladoPorCompra == guiaDeRemision.CodigoMotivoTraslado ? CrearContribuyente(guiaDeRemision.Emisor) : CrearContribuyente(guiaDeRemision.Receptor),
                    Tercero = MaestroSettings.Default.CodigoDetalleMaestroMotivoDeTrasladoPorCompra == guiaDeRemision.CodigoMotivoTraslado ? CrearContribuyente(guiaDeRemision.Receptor) : null,
                    IdDocumento = guiaDeRemision.Serie + "-" + guiaDeRemision.Numero,
                    FechaEmision = guiaDeRemision.FechaEmision.ToString("yyyy-MM-dd"),
                    HoraEmision = guiaDeRemision.FechaEmision.ToString("HH:mm:ss"),
                    TipoDocumento = guiaDeRemision.CodigoSunatTipo,
                    Glosa = guiaDeRemision.Observacion.Length > 200 ? guiaDeRemision.Observacion.Substring(0, 200) : guiaDeRemision.Observacion,
                    ModalidadTraslado = guiaDeRemision.CodigoModalidadTransporte,
                    CodigoMotivoTraslado = guiaDeRemision.CodigoMotivoTraslado,
                    DescripcionMotivo = guiaDeRemision.MotivoTraslado,
                    PesoBrutoTotal = guiaDeRemision.PesoBrutoTotal,
                    NroPallets = guiaDeRemision.NumeroBultos,
                    FechaInicioTraslado = guiaDeRemision.FechaInicioTraslado.ToString("yyyy-MM-dd"),
                    RucTransportista = guiaDeRemision.Transportista?.DocumentoIdentidad,
                    RazonSocialTransportista = guiaDeRemision.Transportista?.RazonSocial,
                    NroDocumentoConductor = guiaDeRemision.Conductor?.DocumentoIdentidad,
                    NombresConductor = guiaDeRemision.Conductor?.Nombres,
                    ApellidosConductor = guiaDeRemision.Conductor?.Apellidos,
                    NroLicenciaConductor = guiaDeRemision.Conductor?.NumeroLicencia,
                    NroPlacaVehiculo = guiaDeRemision.Conductor?.Placa,
                    DireccionPartida = new OpenInvoicePeru.Comun.Dto.Modelos.Direccion
                    {
                        Ubigeo = guiaDeRemision.UbigeoDireccionOrigen,
                        DireccionCompleta = guiaDeRemision.DireccionOrigen.Length > 100 ? guiaDeRemision.DireccionOrigen.Substring(0, 100) : guiaDeRemision.DireccionOrigen,
                        CodigoEstablecimiento = guiaDeRemision.CodigoMotivoTraslado == MaestroSettings.Default.CodigoDetalleMaestroMotivoDeTrasladoPorTrasladoEntreEstablecimientos ? "0000" : null
                    },
                    DireccionLlegada = new OpenInvoicePeru.Comun.Dto.Modelos.Direccion
                    {
                        Ubigeo = guiaDeRemision.UbigeoDireccionDestino,
                        DireccionCompleta = guiaDeRemision.DireccionDestino.Length > 100 ? guiaDeRemision.DireccionDestino.Substring(0, 100) : guiaDeRemision.DireccionDestino,
                        CodigoEstablecimiento = guiaDeRemision.CodigoMotivoTraslado == MaestroSettings.Default.CodigoDetalleMaestroMotivoDeTrasladoPorTrasladoEntreEstablecimientos ? "0000" : null
                    },
                    Transbordo = false,
                    CodigoPuerto = string.Empty,
                    NumeroContenedor = string.Empty,
                    BienesATransportar = CrearDetallesGuia(guiaDeRemision.Detalles),
                };

                return guiaRemision;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private Compania CrearEmisor(Emisor emisor)
        {
            return new Compania()
            {
                TipoDocumento = emisor.CodigoSunatTipoDocumentoIdentidad,
                NroDocumento = emisor.NumeroDocumentoIdentidad,
                NombreLegal = emisor.RazonSocial,
                NombreComercial = emisor.NombreComercial,
                CodigoAnexo = emisor.CodigoEstablecimiento
            };
        }
        private Compania CrearEmisor(EstablecimientoComercialExtendido emisor)
        {
            return new Compania()
            {
                TipoDocumento = emisor.CodigoSunatTipoDocumentoIdentidad,
                NroDocumento = emisor.DocumentoIdentidad,
                NombreLegal = emisor.Nombre,
                NombreComercial = emisor.NombreComercial,
                CodigoAnexo = emisor.Codigo
            };
        }
        private Compania CrearReceptor(Receptor receptor)
        {
            return new Compania()
            {
                TipoDocumento = receptor.CodigoSunatTipoDocumentoIdentidad,
                NroDocumento = receptor.DocumentoIdentidadParaSunat,
                NombreLegal = receptor.RazonSocial,
                CodigoAnexo = ""
            };
        }
        private Contribuyente CrearContribuyente(Emisor emisor)
        {
            return new Contribuyente()
            {
                TipoDocumento = emisor.CodigoSunatTipoDocumentoIdentidad,
                NroDocumento = emisor.NumeroDocumentoIdentidad,
                NombreLegal = emisor.RazonSocial,
                NombreComercial = emisor.NombreComercial,
            };
        }

        private Contribuyente CrearContribuyente(Receptor receptor)
        {
            return new Contribuyente()
            {
                TipoDocumento = receptor.CodigoSunatTipoDocumentoIdentidad,
                NroDocumento = receptor.DocumentoIdentidadParaSunat,
                NombreLegal = receptor.RazonSocial,
            };
        }
        private List<Discrepancia> CrearDiscrepancia(Referencia referencia)
        {
            return new List<Discrepancia>
                    {
                        new Discrepancia
                        {
                            NroReferencia = referencia.NroReferencia,
                            Tipo = referencia.Tipo,
                            Descripcion = referencia.Descripcion
                        }
                    };
        }

        private List<CuotaDocumento> GenerarCuotas(List<DetalleCuota> cuotas)
        {
            List<CuotaDocumento> detallesCuota = new List<CuotaDocumento>();
            foreach (var cuota in cuotas)
            {
                CuotaDocumento detalle = new CuotaDocumento()
                {
                    Codigo = cuota.Codigo,
                    Monto = cuota.Monto,
                    FechaVencimiento = cuota.FechaVencimiento.ToString("yyyy-MM-dd")  
                };
                detallesCuota.Add(detalle);
            }
            return detallesCuota;
        }

        private List<DocumentoRelacionado> CrearRelacionado(Relacionado relacionado)
        {
            return new List<DocumentoRelacionado>
                    {
                        new DocumentoRelacionado
                        {
                            NroDocumento = relacionado.NroDocumento,
                            TipoDocumento = relacionado.TipoDocumento
                        }
                    };
        }

        public List<DetalleDocumento> CrearDetalles(List<Detalle> detalles)
        {
            List<DetalleDocumento> detallesDocumento = new List<DetalleDocumento>();
            int correlativo = 1;
            foreach (var item in detalles)
            {
                detallesDocumento.Add(new DetalleDocumento
                {
                    Id = correlativo++,
                    Cantidad = item.Cantidad,
                    UnidadMedida = "NIU",
                    CodigoItem = item.Codigo,
                    Descripcion = CrearCaracteristica(item),
                    PrecioUnitario = item.ImporteUnitario,
                    PrecioReferencial = item.ImporteUnitario,
                    TipoPrecio = "01",
                    TipoImpuesto = item.ImporteIgv > 0 ? "10" : "20",
                    Impuesto = item.ImporteIgv,
                    ImpuestoUnitario = item.ImporteIgv / item.Cantidad,
                    OtroImpuesto = 0,
                    Descuento = item.Descuento,
                    TotalVenta = item.ImporteTotal - item.ImporteIgv,
                });
            }
            return detallesDocumento;
        }

        public List<DetalleDocumento> CrearDetalles(List<Detalle> detalles, decimal valorIcbper)
        {
            List<DetalleDocumento> detallesDocumento = new List<DetalleDocumento>();
            int correlativo = 1;
            foreach (var item in detalles)
            {
                detallesDocumento.Add(new DetalleDocumento
                {
                    Id = correlativo++,
                    Cantidad = item.Cantidad,
                    UnidadMedida = "NIU",
                    CodigoItem = item.Codigo,
                    Descripcion = CrearCaracteristica(item),
                    PrecioUnitario = item.ImporteUnitario,
                    PrecioReferencial = item.ImporteUnitario,
                    TipoPrecio = "01",
                    TipoImpuesto = item.ImporteIgv > 0 ? "10" : "20",
                    Impuesto = item.ImporteIgv,
                    ImpuestoUnitario = item.ImporteIgv / item.Cantidad,
                    OtroImpuesto = 0,
                    Descuento = item.Descuento,
                    TotalVenta = item.ImporteTotal - item.ImporteIgv,
                    Icbper = (item.IdConceptoBasico == MaestroSettings.Default.IdDetalleMaestroConceptoBasicoBolsaPlastica) ? item.Cantidad * valorIcbper : 0,
                    TasaIcbper = valorIcbper
                });
            }
            return detallesDocumento;
        }

        public List<DetalleDocumento> CrearDetalles(List<Detalle> detalles, decimal valorIcbper, string placa)
        {
            List<DetalleDocumento> detallesDocumento = new List<DetalleDocumento>();
            int correlativo = 1;
            foreach (var item in detalles)
            {
                detallesDocumento.Add(new DetalleDocumento
                {
                    Id = correlativo++,
                    Cantidad = item.Cantidad,
                    UnidadMedida = "NIU",
                    CodigoItem = item.Codigo,
                    Descripcion = CrearCaracteristica(item),
                    PrecioUnitario = item.ImporteUnitario,
                    PrecioReferencial = item.ImporteUnitario,
                    TipoPrecio = "01",
                    TipoImpuesto = item.ImporteIgv > 0 ? "10" : "20",
                    Impuesto = item.ImporteIgv,
                    ImpuestoUnitario = item.ImporteIgv / item.Cantidad,
                    OtroImpuesto = 0,
                    Descuento = item.Descuento,
                    TotalVenta = item.ImporteTotal - item.ImporteIgv,
                    Icbper = (item.IdConceptoBasico == MaestroSettings.Default.IdDetalleMaestroConceptoBasicoBolsaPlastica) ? item.Cantidad * valorIcbper : 0,
                    TasaIcbper = valorIcbper,
                    PlacaVehiculo = placa
                });
            }
            return detallesDocumento;
        }

        private string CrearCaracteristica(Detalle detalle)
        {
            string resultado = detalle.Concepto;
            foreach (var item in detalle.CaracteristicasComunes)
            {
                resultado = resultado + " | " + item.NombreCaracteristica + ":" + item.Valor;
            }
            {
                foreach (var item in detalle.CaracteristicasPropias)
                    resultado = resultado + " | " + item.Nombre + ":" + item.Valor;
            }
            resultado = resultado.Length > 250 ? resultado.Substring(0, 250) : resultado;
            return resultado;
        }

        public List<DetalleGuia> CrearDetallesGuia(List<Detalle> detalles)
        {
            List<DetalleGuia> detallesGuia = new List<DetalleGuia>();
            int correlativo = 1;
            foreach (var item in detalles)
            {
                detallesGuia.Add(new DetalleGuia
                {
                    Correlativo = correlativo++,
                    CodigoItem = item.Codigo,
                    Descripcion = item.Concepto,
                    UnidadMedida = "NIU",
                    Cantidad = item.Cantidad,
                    LineaReferencia = 1
                });
            }
            return detallesGuia;
        }

        private Compania CrearEmisor(ActorComercial emisor)
        {
            return new Compania()
            {
                TipoDocumento = emisor.CodigoSunatTipoDocumentoIdentidad(),
                NroDocumento = emisor.DocumentoIdentidad,
                NombreLegal = emisor.RazonSocial,
                NombreComercial = emisor.NombreComercial,
                CodigoAnexo = emisor.Codigo
            };
        }

        public ResumenDiarioNuevo ObtenerResumenDiario(Documento[] documentos, EstablecimientoComercialExtendido emisor, bool cambiarEstadoItemDeAnuladoAAdicionado)
        {
            try
            {
                string fechaEmision =DateTimeUtil.FechaActual().ToString("yyyy-MM-dd");
                ResumenDiarioNuevo resumenDiarioNuevo = new ResumenDiarioNuevo
                {
                    IdDocumento = FacturacionElectronicaSettings.Default.CodigoTipoDocumentoResumenDiario + "-" + fechaEmision.Split('-')[0] + fechaEmision.Split('-')[1] + fechaEmision.Split('-')[2] + "-" + Convert.ToString(ObtenerIdentificadorResumenDiario(Convert.ToDateTime(fechaEmision))),
                    Emisor = CrearEmisor(emisor),
                    FechaEmision = fechaEmision,
                    FechaReferencia = documentos[0].fechaEmision.ToString("yyyy-MM-dd"),
                    Resumenes = CrearGrupoResumenNuevo(documentos, cambiarEstadoItemDeAnuladoAAdicionado)
                };
                return resumenDiarioNuevo;
            }
            catch (Exception e)
            {
                throw new LogicaException("ERROR AL GENERAR EL RESUMEN DIARIO", e);
            }
        }

        public List<GrupoResumenNuevo> CrearGrupoResumenNuevo(Documento[] documentos, bool cambiarEstadoItemDeAnuladoAAdicionado)
        {
            List<GrupoResumenNuevo> resumenesDiarios = new List<GrupoResumenNuevo>();

            for (int i = 0; i < documentos.Length; i++)
            {
                DocumentoElectronico documentoElectronico = JsonConvert.DeserializeObject<DocumentoElectronico>(Encoding.UTF8.GetString(documentos[i].Binario.archivoBinario));

                GrupoResumenNuevo grupoResumenNuevo = new GrupoResumenNuevo
                {
                    Id = i + 1,
                    TipoDocumento = documentoElectronico.TipoDocumento,
                    IdDocumento = documentoElectronico.IdDocumento,
                    NroDocumentoReceptor = documentoElectronico.Receptor.NroDocumento,
                    TipoDocumentoReceptor = documentoElectronico.Receptor.TipoDocumento,
                    CodigoEstadoItem = (documentos[i].estado == (int)EstadoDocumentoElectronico.Anulado && cambiarEstadoItemDeAnuladoAAdicionado) ? (int)EstadoDocumentoElectronico.Adicionado : documentos[i].estado,
                    Moneda = documentoElectronico.Moneda,
                    TotalIgv = documentoElectronico.TotalIgv,
                    TotalIcbper = documentoElectronico.TotalIcbper,
                    TotalVenta = documentoElectronico.TotalVenta,
                    Gravadas = documentoElectronico.Gravadas,
                    Exoneradas = documentoElectronico.Exoneradas,
                    Inafectas = documentoElectronico.Inafectas,
                    Gratuitas = documentoElectronico.Gratuitas,
                    TotalOtrosImpuestos = documentoElectronico.TotalOtrosTributos,
                    CorrelativoInicio = Convert.ToInt32(documentos.First().id),
                    CorrelativoFin = Convert.ToInt32(documentos.Last().id),
                    TotalDescuentos = documentoElectronico.DescuentoGlobal
                };

                if (grupoResumenNuevo.TipoDocumento == MaestroSettings.Default.CodigoDetalleMaestroNotaDeCredito || grupoResumenNuevo.TipoDocumento == MaestroSettings.Default.CodigoDetalleMaestroNotaDeDebito)
                {
                    if (!DocumentoReferenciaFueAceptado(documentoElectronico.Relacionados.FirstOrDefault().TipoDocumento, documentoElectronico.Relacionados.FirstOrDefault().NroDocumento))
                    {
                        throw new LogicaException("El documento de referencia de :" + documentoElectronico.Relacionados.FirstOrDefault().TipoDocumento + " " + documentoElectronico.Relacionados.FirstOrDefault().NroDocumento + " aun no a sido informado");
                    }
                    grupoResumenNuevo.TipoDocumentoRelacionado = documentoElectronico.Relacionados.FirstOrDefault().TipoDocumento;
                    grupoResumenNuevo.DocumentoRelacionado = documentoElectronico.Relacionados.FirstOrDefault().NroDocumento;
                }
                resumenesDiarios.Add(grupoResumenNuevo);
            }
            return resumenesDiarios;
        }

        public ComunicacionBaja ObtenerComunicacionBaja(Documento[] documentos, EstablecimientoComercialExtendido emisor)
        {
            string fechaEmision =DateTimeUtil.FechaActual().ToString("yyyy-MM-dd");
            return new ComunicacionBaja
            {
                Emisor = CrearEmisor(emisor),
                IdDocumento = "RA" + "-" + fechaEmision.Split('-')[0] + fechaEmision.Split('-')[1] + fechaEmision.Split('-')[2] + "-" + Convert.ToString(ObtenerIdentificadorComunicacionBaja(Convert.ToDateTime(fechaEmision))),
                FechaEmision =DateTimeUtil.FechaActual().ToString("yyyy-MM-dd"),
                FechaReferencia = documentos[0].fechaEmision.ToString("yyyy-MM-dd"),
                Bajas = CrearBajasComunicacionBaja(documentos)
            };
        }

        public List<DocumentoBaja> CrearBajasComunicacionBaja(Documento[] documentos)
        {
            List<DocumentoBaja> documentoBajas = new List<DocumentoBaja>();

            for (int i = 0; i < documentos.Length; i++)
            {
                DocumentoElectronico documentoElectronico = JsonConvert.DeserializeObject<DocumentoElectronico>(Encoding.UTF8.GetString(documentos[i].Binario.archivoBinario));

                documentoBajas.Add(new DocumentoBaja
                {
                    Id = i + 1,
                    TipoDocumento = documentoElectronico.TipoDocumento,
                    Serie = documentoElectronico.IdDocumento.Split('-')[0],
                    Correlativo = documentoElectronico.IdDocumento.Split('-')[1],
                    MotivoBaja = "ANULACION DE LA FACTURA " + documentoElectronico.IdDocumento,
                });
            }
            return documentoBajas;
        }

        public int ObtenerIdentificadorResumenDiario(DateTime fechaConsulta)
        {
            try
            {
                return _facturacionDatos.ObtenerIdentificador(fechaConsulta, FacturacionElectronicaSettings.Default.TipoEnvioResumenDiario) + 1;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool DocumentoReferenciaFueAceptado(string tipoDocumento, string numeroDocumento)
        {
            try
            {
                List<int> estadosDeAceptacion = new List<int> { (int)EstadoEnvio.Aceptado, (int)EstadoEnvio.AceptadoConObservaciones };
                return _facturacionDatos.DocumentoReferenciaFueAceptado(tipoDocumento, numeroDocumento, estadosDeAceptacion);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public int ObtenerIdentificadorComunicacionBaja(DateTime fechaConsulta)
        {
            try
            {
                return _facturacionDatos.ObtenerIdentificador(fechaConsulta, FacturacionElectronicaSettings.Default.TipoEnvioComunicacionDeBaja) + 1;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
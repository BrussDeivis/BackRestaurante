using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;

namespace Tsp.Sigescom.Modelo.Entidades.ComprobantesModel
{
    public class DocumentoElectronicoVenta : DocumentoElectronicoImpreso
    {
        public decimal Igv { get; set; }
        public decimal Icbper { get; set; }
        public decimal ValorIcbper { get; set; }
        public bool MostrarEmpleado { get; set; }
        public string EtiquetaEmpleado { get; set; }
        public string NombreEmpleado { get; set; }
        public bool MostrarPuntos { get; set; }
        public int PuntosGanados { get; set; }
        public int PuntosAcumulados { get; set; }


        public DocumentoElectronicoVenta()
        { }
        public DocumentoElectronicoVenta(OperacionDeVenta operacion, EstablecimientoComercialExtendido sede, EstablecimientoComercialExtendido establecimiento, byte[] qrBytes, bool mostrarEncabezadoTestigo, ModoImpresionCaracteristicasEnum modoImpresionCaracteristicas) : base()
        {
            IdOrden = operacion.Id;
            FechaEmision = operacion.FechaEmision;
            Emisor = new Emisor(sede, establecimiento);
            Receptor = new Receptor(operacion.Cliente(), operacion.AliasCliente());
            Detalles = Detalle.Convert(operacion.Detalles(), modoImpresionCaracteristicas, operacion.DetalleUnificado(), true);
            MostrarMensajeAmazonia = operacion.AplicaLeyDeAmazonia;
            MostrarMensajeNegocio = AplicacionSettings.Default.MostrarMensajeDeNegocio;
            MensajeNegocio = AplicacionSettings.Default.MensajeDeNegocio;
            Observacion = operacion.Observacion() ?? "-";
            MostrarLogo = FacturacionElectronicaSettings.Default.MostrarLogoEnComprobanteImpreso;
            MostrarTrazabilidadConceptoNegocio = VentasSettings.Default.PermitirRegistroDeLoteEnDetalleDeVenta;
            CodigoQR = qrBytes;
            CodigoSunatMoneda = operacion.Moneda().Codigo;
            CodigoSunatTipo = operacion.Comprobante().CodigoTipo;
            Serie = operacion.Comprobante().NumeroDeSerie;
            Numero = operacion.Comprobante().NumeroDeComprobante;
            ImporteTotal = operacion.Total;
            ImporteOperacionExonerada = operacion.ImporteTotalOperacionExonerada;
            ImporteOperacionInafecta = operacion.ImporteTotalOperacionInafecta;
            ImporteOperacionGravada = operacion.BaseImponibleOperacionGravada;
            ImporteTotalEnLetras = Util.APalabras(operacion.Total, operacion.MonedaPlural());
            Descuento = operacion.Descuento();
            Igv = operacion.Igv();
            Icbper = operacion.Icbper();
            ValorIcbper = operacion.ValorIcbper();
            MostrarTestigo = mostrarEncabezadoTestigo;
            ResolucionAutorizacionSunat = FacturacionElectronicaSettings.Default.ResolucionEmisionElectronica;
            IdEstadoActual = operacion.IdEstadoActual;
            MostrarEmpleado = VentasSettings.Default.MostrarEmpleadoEnComprobanteDeVenta;
            EtiquetaEmpleado = VentasSettings.Default.EtiquetaEmpleadoEnComprobanteDeVenta;
            NombreEmpleado = operacion.Empleado().Nombres + " " + operacion.Empleado().ApellidoPaterno;
            MostrarPlaca = VentasSettings.Default.PermitirRegistroDePlacaEnVenta && !string.IsNullOrEmpty(operacion.Detalles().First().Registro);
            Placa = operacion.Detalles().First().Registro;
            MostrarInformacion = !string.IsNullOrEmpty(operacion.Informacion);
            Informacion = operacion.Informacion;
            MostrarPuntos = VentasSettings.Default.GenerarPuntosEnVentas && operacion.IdCliente != ActorSettings.Default.IdClienteGenerico;
            PuntosGanados = operacion.PuntosGanados();
            PuntosAcumulados = operacion.PuntosAcumulados();
            EsInvalidada = operacion.EsInvalidada;
            MotivoInvalidacion = operacion.EsInvalidada ? operacion.MotivoInvalidacion() : "";
        }

        

            public DocumentoElectronicoVenta(OperacionDeVenta operacion, EstablecimientoComercialExtendidoConLogo sede, EstablecimientoComercialExtendido establecimiento, byte[] qrBytes, bool mostrarEncabezadoTestigo, ModoImpresionCaracteristicasEnum modoImpresionCaracteristicas) : base()
        {
            IdOrden = operacion.Id;
            FechaEmision = operacion.FechaEmision;
            Emisor = new Emisor(sede, establecimiento);
            Receptor = new Receptor(operacion.Cliente(), operacion.AliasCliente());
            Detalles = Detalle.Convert(operacion.Detalles(), modoImpresionCaracteristicas, operacion.DetalleUnificado(), true);
            MostrarMensajeAmazonia = operacion.AplicaLeyDeAmazonia;
            MostrarMensajeNegocio = AplicacionSettings.Default.MostrarMensajeDeNegocio;
            MensajeNegocio = AplicacionSettings.Default.MensajeDeNegocio;
            Observacion = operacion.Observacion() ?? "-";
            MostrarLogo = FacturacionElectronicaSettings.Default.MostrarLogoEnComprobanteImpreso;
            MostrarTrazabilidadConceptoNegocio = VentasSettings.Default.PermitirRegistroDeLoteEnDetalleDeVenta;
            CodigoQR = qrBytes;
            CodigoSunatMoneda = operacion.Moneda().Codigo;
            CodigoSunatTipo = operacion.Comprobante().CodigoTipo;
            Serie = operacion.Comprobante().NumeroDeSerie;
            Numero = operacion.Comprobante().NumeroDeComprobante;
            ImporteTotal = operacion.Total;
            ImporteOperacionExonerada = operacion.ImporteTotalOperacionExonerada;
            ImporteOperacionInafecta = operacion.ImporteTotalOperacionInafecta;
            ImporteOperacionGravada = operacion.BaseImponibleOperacionGravada;
            ImporteTotalEnLetras = Util.APalabras(operacion.Total, operacion.MonedaPlural());
            Descuento = operacion.Descuento();
            Igv = operacion.Igv();
            Icbper = operacion.Icbper();
            ValorIcbper = operacion.ValorIcbper();
            MostrarTestigo = mostrarEncabezadoTestigo;
            ResolucionAutorizacionSunat = FacturacionElectronicaSettings.Default.ResolucionEmisionElectronica;
            IdEstadoActual = operacion.IdEstadoActual;
            MostrarEmpleado = VentasSettings.Default.MostrarEmpleadoEnComprobanteDeVenta;
            EtiquetaEmpleado = VentasSettings.Default.EtiquetaEmpleadoEnComprobanteDeVenta;
            NombreEmpleado = operacion.Empleado().Nombres + " " + operacion.Empleado().ApellidoPaterno;
            MostrarPlaca = VentasSettings.Default.PermitirRegistroDePlacaEnVenta && !string.IsNullOrEmpty(operacion.Detalles().First().Registro);
            Placa = operacion.Detalles().First().Registro;
            MostrarInformacion = !string.IsNullOrEmpty(operacion.Informacion);
            Informacion = operacion.Informacion;
            MostrarPuntos = VentasSettings.Default.GenerarPuntosEnVentas && operacion.IdCliente != ActorSettings.Default.IdClienteGenerico;
            PuntosGanados = operacion.PuntosGanados();
            PuntosAcumulados = operacion.PuntosAcumulados();
            EsInvalidada = operacion.EsInvalidada;
            MotivoInvalidacion = operacion.EsInvalidada ? operacion.MotivoInvalidacion() : "";
        }
    }

    public class DocumentoElectronicoCompra : DocumentoElectronicoImpreso
    {
        public decimal Igv { get; set; }
        public decimal Icbper { get; set; }

        public DocumentoElectronicoCompra()
        {
        }
        public DocumentoElectronicoCompra(OperacionDeCompra operacion, EstablecimientoComercialExtendido sede, byte[] qrBytes, bool mostrarEncabezadoTestigo, ModoImpresionCaracteristicasEnum modoImpresionCaracteristicas) : base()
        {
            IdOrden = operacion.Id;
            FechaEmision = operacion.FechaEmision;
            Emisor = new Emisor(operacion.Proveedor());
            Receptor = new Receptor(sede);
            Detalles = Detalle.Convert(operacion.Detalles(), modoImpresionCaracteristicas, operacion.DetalleUnificado(), false);
            MostrarMensajeAmazonia = operacion.AplicaLeyDeAmazonia;
            MostrarMensajeNegocio = AplicacionSettings.Default.MostrarMensajeDeNegocio;
            MensajeNegocio = AplicacionSettings.Default.MensajeDeNegocio;
            Observacion = operacion.Observacion() ?? "-";
            MostrarLogo = FacturacionElectronicaSettings.Default.MostrarLogoEnComprobanteImpreso;
            MostrarTrazabilidadConceptoNegocio = AplicacionSettings.Default.PermitirLoteEnDetalleDeCompra;
            CodigoQR = qrBytes;
            CodigoSunatMoneda = operacion.Moneda().Codigo;
            CodigoSunatTipo = operacion.Comprobante().CodigoTipo;
            Serie = operacion.Comprobante().NumeroDeSerie;
            Numero = operacion.Comprobante().NumeroDeComprobante;
            ImporteTotal = operacion.Total;
            ImporteOperacionExonerada = operacion.ImporteTotalOperacionExonerada;
            ImporteOperacionInafecta = operacion.ImporteTotalOperacionInafecta;
            ImporteOperacionGravada = operacion.BaseImponibleOperacionGravada;
            ImporteTotalEnLetras = Util.APalabras(operacion.Total, operacion.MonedaPlural());
            Descuento = operacion.Descuento();
            Igv = operacion.Igv();
            Icbper = operacion.Icbper();
            MostrarTestigo = mostrarEncabezadoTestigo;
            ResolucionAutorizacionSunat = FacturacionElectronicaSettings.Default.ResolucionEmisionElectronica;
            IdEstadoActual = operacion.IdEstadoActual;
            EsInvalidada = operacion.EsInvalidada;
        }
    }

    public class DocumentoElectronicoImpreso : ComprobanteImpreso
    {
        /// <summary>
        /// muestra antes del logo, en texto grande, la serie y numero
        /// </summary>
        public bool MostrarTestigo { get; set; }
        //public bool MostrarLogo { get; set; }
        /// <summary>
        /// Muestra el lote, fecha de vencimiento y registro 
        /// </summary>
        public bool MostrarTrazabilidadConceptoNegocio { get; set; }
        //public DateTime FechaEmision { get; set; }
        //public Emisor Emisor { get; set; }
        public Receptor Receptor { get; set; }
        public List<Detalle> Detalles { get; set; }
        public decimal ImporteOperacionGravada { get; set; }
        public decimal ImporteOperacionInafecta { get; set; }
        public decimal ImporteOperacionExonerada { get; set; }
        public decimal Descuento { get; set; }
        public decimal ImporteTotal { get; set; }
        public string ImporteTotalEnLetras { get; set; }
        //public string Observacion { get; set; }
        //public bool MostrarMensajeNegocio { get; set; }
        //public string MensajeNegocio { get; set; }
        public string CodigoSunatMoneda { get; set; }
        public long IdOrden { get; set; }
        //public int IdEstadoActual { get; set; }
        /// <summary>
        /// Nombre del tipo de documento: BV, Factura, etc.
        /// </summary>
        //public virtual string NombreTipo { get; set; }
        //public string Serie { get; set; }
        //public int Numero { get; set; }
        public string CodigoSunatTipo { get; set; }

        //public bool EsInvalidada { get; set; }

        public ModoImpresionCaracteristicasEnum ModoImpresionCaracteristicas { get; set; }
        public byte[] CodigoQR { get; set; }
        public string CodigoQRSrc { get { return ("data:image/jpeg;base64," + Convert.ToBase64String(CodigoQR, 0, CodigoQR.Length)); } }
        public bool MostrarMensajeAmazonia { get; set; }
        public string MensajeAmazonia { get; } = ("BIENES Y/O SERVICIOS TRANSFERIDOS/PRESTADOS EN LA AMAZONÍA PARA SER CONSUMIDOS EN LA MISMA");
        public string UrlConsultaComprobante { get { return AplicacionSettings.Default.UrlConsultaComprobante; } }
        public bool MostrarUrlConsultaComprobante { get { return AplicacionSettings.Default.MostrarUrlConsultaComprobante; } }
        public string ResolucionAutorizacionSunat { get; set; }
        public string NumeroDecimalesEnCantidad { get { return AplicacionSettings.Default.NumeroDecimalesEnCantidad.ToString(); } }
        public string NumeroDecimalesEnPrecio { get { return AplicacionSettings.Default.NumeroDecimalesEnPrecio.ToString(); } }
        public string FormatoNumericoDecimal { get { return "N2"; } }
        public bool MostrarPlaca { get; set; }
        public string Placa { get; set; }
        public bool MostrarInformacion { get; set; }
        public string Informacion { get; set; }
        public string FormaPago { get; set; }
        public decimal MontoACredito { get; set; }
        public List<DetalleCuota> Cuotas { get; set; }

        public DocumentoElectronicoImpreso()
        {
        }
        public DocumentoElectronicoImpreso(OrdenDeVenta orden, ActorComercial sede, byte[] qrBytes, bool mostrarEncabezadoTestigo, ModoImpresionCaracteristicasEnum modoImpresionCaracteristicas)
        {
        }
    }
    public class ComprobanteImpreso
    {
        public int IdEstadoActual { get; set; }
        public virtual string NombreTipo { get; set; }
        public string Serie { get; set; }
        public int Numero { get; set; }
        public bool MostrarLogo { get; set; }
        public DateTime FechaEmision { get; set; }
        public Emisor Emisor { get; set; }
        public string Observacion { get; set; }
        public bool MostrarMensajeNegocio { get; set; }
        public string MensajeNegocio { get; set; }
        public bool EsInvalidada { get; set; }
        public string MotivoInvalidacion { get; set; }

        public ComprobanteImpreso()
        {
        }

    }

    public class Emisor
    {
        public byte[] LogoBytes { get; set; }
        public string LogoSrc { get { return ("data:image/jpeg;base64," + Convert.ToBase64String(LogoBytes, 0, LogoBytes.Length)); } }
        public string NombreComercial { get; set; }
        public string CodigoEstablecimiento { get; set; }
        public string RazonSocial { get; set; }
        public string Direccion { get; set; }
        public string OtrosDatosContacto { get; set; }
        public string CodigoSunatTipoDocumentoIdentidad { get; set; }
        public string TipoDocumentoIdentidad { get; set; }
        public string NumeroDocumentoIdentidad { get; set; }
        public string Publicidad { get; set; }
        //Datos que determina si es una sucursal el emisor del comprobante
        public bool EsSucursal { get; set; }
        public string NombreSucursal { get; set; }
        public string DireccionSucursal { get; set; }

        public Emisor(ActorComercial proveedor)
        {
            RazonSocial = proveedor.RazonSocial;
            NombreComercial = proveedor.NombreComercial;
            NumeroDocumentoIdentidad = proveedor.DocumentoIdentidad;
            TipoDocumentoIdentidad = proveedor.CodigoTipoDocumentoIdentidad();
            LogoBytes = proveedor.Logo();
            Direccion = proveedor.DomicilioFiscal() != null ? proveedor.DomicilioFiscal().detalle : "";
            OtrosDatosContacto = (String.IsNullOrEmpty(proveedor.Telefono()) ? "" : ("TF: " + proveedor.Telefono())) + Environment.NewLine + (String.IsNullOrEmpty(proveedor.Correo()) ? "" : ("Email: " + proveedor.Correo()));
            Publicidad = proveedor.InformacionPublicitaria();
            CodigoSunatTipoDocumentoIdentidad = proveedor.CodigoSunatTipoDocumentoIdentidad();
        }

        public Emisor(EstablecimientoComercialExtendidoConLogo establecimiento)
        {
            RazonSocial = establecimiento.Nombre;
            NombreComercial = establecimiento.NombreComercial;
            NumeroDocumentoIdentidad = establecimiento.DocumentoIdentidad;
            TipoDocumentoIdentidad = establecimiento.CodigoTipoDocumentoIdentidad;
            LogoBytes = establecimiento.Logo;
            Direccion = establecimiento.DomicilioFiscal != null ? establecimiento.DomicilioFiscal.Detalle : "";
            OtrosDatosContacto = (String.IsNullOrEmpty(establecimiento.Telefono) ? "" : ("TF: " + establecimiento.Telefono)) + Environment.NewLine + (String.IsNullOrEmpty(establecimiento.Correo) ? "" : ("Email: " + establecimiento.Correo));
            Publicidad = establecimiento.InformacionPublicitaria;
            CodigoSunatTipoDocumentoIdentidad = establecimiento.CodigoSunatTipoDocumentoIdentidad;
        }

        public Emisor(EstablecimientoComercialExtendido establecimiento)
        {
            RazonSocial = establecimiento.Nombre;
            NombreComercial = establecimiento.NombreComercial;
            NumeroDocumentoIdentidad = establecimiento.DocumentoIdentidad;
            TipoDocumentoIdentidad = establecimiento.CodigoTipoDocumentoIdentidad;
            Direccion = establecimiento.DomicilioFiscal != null ? establecimiento.DomicilioFiscal.Detalle : "";
            OtrosDatosContacto = (String.IsNullOrEmpty(establecimiento.Telefono) ? "" : ("TF: " + establecimiento.Telefono)) + Environment.NewLine + (String.IsNullOrEmpty(establecimiento.Correo) ? "" : ("Email: " + establecimiento.Correo));
            Publicidad = establecimiento.InformacionPublicitaria;
            CodigoSunatTipoDocumentoIdentidad = establecimiento.CodigoSunatTipoDocumentoIdentidad;
        }


        public Emisor(EstablecimientoComercialExtendidoConLogo sede, EstablecimientoComercialExtendido establecimiento)
        {
            RazonSocial = sede.Nombre;
            NombreComercial = sede.NombreComercial;
            TipoDocumentoIdentidad = sede.CodigoTipoDocumentoIdentidad;
            NumeroDocumentoIdentidad = sede.DocumentoIdentidad;
            LogoBytes = sede.Logo;
            Direccion = sede.DomicilioFiscal.Detalle + ", " + sede.DomicilioFiscal.Ubigeo.Nombre;
            OtrosDatosContacto = (String.IsNullOrEmpty(sede.Telefono) ? "" : ("TF: " + sede.Telefono)) + Environment.NewLine + (String.IsNullOrEmpty(sede.Correo) ? "" : ("Email: " + sede.Correo));
            Publicidad = sede.InformacionPublicitaria;
            CodigoSunatTipoDocumentoIdentidad = sede.CodigoSunatTipoDocumentoIdentidad;
            CodigoEstablecimiento = establecimiento.EsSucursal ? establecimiento.Codigo : sede.Codigo;
            EsSucursal = establecimiento.EsSucursal;
            NombreSucursal = establecimiento.Nombre;
            DireccionSucursal = establecimiento.DomicilioFiscal.Detalle + ", " + establecimiento.DomicilioFiscal.Ubigeo.Nombre;
        }

        public Emisor(EstablecimientoComercialExtendido sede, EstablecimientoComercialExtendido establecimiento)
        {
            RazonSocial = sede.Nombre;
            NombreComercial = sede.NombreComercial;
            TipoDocumentoIdentidad = sede.CodigoTipoDocumentoIdentidad;
            NumeroDocumentoIdentidad = sede.DocumentoIdentidad;
            Direccion = sede.DomicilioFiscal.Detalle + ", " + sede.DomicilioFiscal.Ubigeo.Nombre;
            OtrosDatosContacto = (String.IsNullOrEmpty(sede.Telefono) ? "" : ("TF: " + sede.Telefono)) + Environment.NewLine + (String.IsNullOrEmpty(sede.Correo) ? "" : ("Email: " + sede.Correo));
            Publicidad = sede.InformacionPublicitaria;
            CodigoSunatTipoDocumentoIdentidad = sede.CodigoSunatTipoDocumentoIdentidad;
            CodigoEstablecimiento = establecimiento.EsSucursal ? establecimiento.Codigo : sede.Codigo;
            EsSucursal = establecimiento.EsSucursal;
            NombreSucursal = establecimiento.Nombre;
            DireccionSucursal = establecimiento.DomicilioFiscal.Detalle + ", " + establecimiento.DomicilioFiscal.Ubigeo.Nombre;
        }

        public Emisor(ActorComercial sede, EstablecimientoComercialExtendidoConLogo establecimiento)
        {
            RazonSocial = sede.RazonSocial;
            NombreComercial = sede.NombreComercial;
            TipoDocumentoIdentidad = sede.CodigoTipoDocumentoIdentidad();
            NumeroDocumentoIdentidad = sede.DocumentoIdentidad;
            LogoBytes = sede.Logo();
            Direccion = sede.DomicilioFiscal().detalle + ", " + sede.DomicilioFiscal().Ubigeo.descripcion_corta;
            OtrosDatosContacto = (String.IsNullOrEmpty(sede.Telefono()) ? "" : ("TF: " + sede.Telefono())) + Environment.NewLine + (String.IsNullOrEmpty(sede.Correo()) ? "" : ("Email: " + sede.Correo()));
            Publicidad = sede.InformacionPublicitaria();
            CodigoSunatTipoDocumentoIdentidad = sede.CodigoSunatTipoDocumentoIdentidad();
            CodigoEstablecimiento = establecimiento.EsSucursal ? establecimiento.Codigo : sede.Codigo;
            EsSucursal = establecimiento.EsSucursal;
            NombreSucursal = establecimiento.Nombre;
            DireccionSucursal = establecimiento.DomicilioFiscal.Texto;
            
        }

        //public Emisor(ActorComercial_ sede, EstablecimientoComercial_ establecimiento)
        //{
        //    RazonSocial = sede.NombreORazonSocial;
        //    NombreComercial = sede.NombreComercial;
        //    TipoDocumentoIdentidad = sede.CodigoTipoDocumentoIdentidad;
        //    NumeroDocumentoIdentidad = sede.NumeroDocumentoIdentidad;
        //    LogoBytes = sede.Logo;
        //    Direccion = sede.DomicilioFiscal;
        //    OtrosDatosContacto = (String.IsNullOrEmpty(sede.Telefono) ? "" : ("TF: " + sede.Telefono)) + Environment.NewLine + (String.IsNullOrEmpty(sede.Correo) ? "" : ("Email: " + sede.Correo));
        //    Publicidad = sede.InformacionPublicitaria;
        //    CodigoSunatTipoDocumentoIdentidad = sede.CodigoSunatTipoDocumentoIdentidad;
        //    CodigoEstablecimiento = establecimiento.EsSucursal ? establecimiento.Codigo : sede.Codigo;
        //    EsSucursal = establecimiento.EsSucursal;
        //    NombreSucursal = establecimiento.NombreORazonSocial;
        //    DireccionSucursal = establecimiento.DomicilioFiscal;
        //}
    }

    public class Receptor
    {
        public string RazonSocial { get; set; }
        public string Direccion { get; set; }
        public string TipoDocumentoIdentidad { get; set; }
        public string CodigoSunatTipoDocumentoIdentidad { get; set; }
        public string DocumentoIdentidad { get; set; }
        /// <summary>
        /// Sunat necesita si o si el numero de identidad
        /// </summary>
        public string DocumentoIdentidadParaSunat { get; set; }

        public Receptor(EstablecimientoComercialExtendido actorComercial)
        {
            RazonSocial = actorComercial.Nombre.Replace("|"," ");
            TipoDocumentoIdentidad = actorComercial.CodigoTipoDocumentoIdentidad;
            DocumentoIdentidad = actorComercial.DocumentoIdentidad;
            DocumentoIdentidadParaSunat = actorComercial.DocumentoIdentidad;
            Direccion = actorComercial.DomicilioFiscal != null ? actorComercial.DomicilioFiscal.Detalle + ((VentasSettings.Default.InformacionAMostrarEnDireccionDeCliente == (int)InformacionDireccionEnCliente.SoloDetalle) ? "" : (" - " + actorComercial.DomicilioFiscal.Ubigeo.Nombre)) : "";
            CodigoSunatTipoDocumentoIdentidad = actorComercial.CodigoSunatTipoDocumentoIdentidad;
        }
        public Receptor(Cliente cliente, string aliasCliente)
        {
            RazonSocial = (cliente.Id == ActorSettings.Default.IdClienteGenerico ? (string.IsNullOrEmpty(aliasCliente) ? cliente.RazonSocial : aliasCliente) : cliente.RazonSocial).Replace("|", " ");
            TipoDocumentoIdentidad = (cliente.Id == ActorSettings.Default.IdClienteGenerico ? "" : cliente.CodigoTipoDocumentoIdentidad());
            DocumentoIdentidad = cliente.NumeroDocumentoIdentidadCliente;
            DocumentoIdentidadParaSunat = cliente.DocumentoIdentidad;
            Direccion = cliente.DomicilioFiscal() != null ? cliente.DomicilioFiscal().detalle + ((VentasSettings.Default.InformacionAMostrarEnDireccionDeCliente == (int)InformacionDireccionEnCliente.SoloDetalle) ? "" : (" - " + cliente.DomicilioFiscal().Ubigeo.descripcion_larga)) : "";
            CodigoSunatTipoDocumentoIdentidad = cliente.CodigoSunatTipoDocumentoIdentidad();
        }

    }

    public class Detalle
    {
        public decimal Cantidad { get; set; }
        public string Codigo { get; set; }
        public string Concepto { get; set; }
        public int IdConceptoBasico { get; set; }
        /// <summary>
        /// Es la concatenacion de lote, fecha vencimiento  registro sanitario
        /// </summary>
        public string Trazabilidad { get; set; }
        public decimal ImporteUnitario { get; set; }
        public decimal ImporteTotal { get; set; }
        public List<ValorCaracteristica> CaracteristicasComunes { get; set; }
        public List<ValorDetalleMaestroDetalleTransaccion> CaracteristicasPropias { get; set; }
        public decimal Descuento { get; set; }
        public decimal ImporteIgv { get; set; }
        public string CodigoBarra { get; set; }

        public Detalle()
        {
        }

        public Detalle(DetalleDeOperacion detalle, ModoImpresionCaracteristicasEnum modoImpresionCaracteristicas)
        {
            this.Cantidad = detalle.Cantidad;
            this.Codigo = detalle.Producto.Codigo;
            this.Concepto = detalle.Producto.NombreConcepto;
            this.IdConceptoBasico = detalle.Producto.IdConceptoBasico;
            this.Trazabilidad = (detalle.Lote != null && detalle.Lote != "" ? " LT: " + detalle.Lote : "") + (detalle.Vencimiento != null ? " FV: " + ((DateTime)detalle.Vencimiento).ToString("dd/MM/yyyy") : "") + (detalle.Registro != null && detalle.Registro != "" ? " REG: " + detalle.Registro : "");
            this.ImporteUnitario = Math.Abs(detalle.PrecioUnitario);
            this.ImporteTotal = Math.Abs(detalle.Importe);
            this.ImporteIgv = Math.Abs(detalle.Igv);
            this.CaracteristicasComunes = new List<ValorCaracteristica>();
            this.CaracteristicasPropias = new List<ValorDetalleMaestroDetalleTransaccion>();

            if (modoImpresionCaracteristicas == ModoImpresionCaracteristicasEnum.ComunesYPropias || modoImpresionCaracteristicas == ModoImpresionCaracteristicasEnum.SoloComunes)
            {
                this.CaracteristicasComunes = detalle.Producto.CaracteristicasComunes();
                this.Concepto = detalle.Producto.NombreConceptoBasico;
            }
            if (modoImpresionCaracteristicas == ModoImpresionCaracteristicasEnum.ComunesYPropias || modoImpresionCaracteristicas == ModoImpresionCaracteristicasEnum.SoloPropias)
            {
                this.CaracteristicasPropias = detalle.Producto.CaracteristicasPropias();
                this.Concepto = detalle.Producto.NombreConceptoBasico;
            }
        }

        internal static List<Detalle> Convert(List<DetalleDeOperacion> detalles, ModoImpresionCaracteristicasEnum modoImpresionCaracteristicas, string detalleUnificado, bool esOperacionVenta)
        {
            List<Detalle> resultado = new List<Detalle>();
            if ((String.IsNullOrEmpty(detalleUnificado)))
            {
                if (esOperacionVenta && VentasSettings.Default.MostrarDetallesUnificadosPorLoteEnComprobantesDeVenta)//&& AplicacionSettings.Default.PermitirGestionDeLotes
                {
                    var detallesAgrupadosPorLote = detalles.GroupBy(d => d.Producto.Id);
                    foreach (var item in detallesAgrupadosPorLote)
                    {
                        DetalleDeOperacion detalle = new DetalleDeOperacion()
                        {
                            Cantidad = item.Sum(i => i.Cantidad),
                            Importe = Math.Abs(item.Sum(i => i.Importe)),
                            Lote = null,
                            Registro = null,
                            Vencimiento = null,
                            Producto = new Concepto_Negocio_Comercial()
                            {
                                Id = item.Key,
                                IdConceptoBasico = item.First().Producto.IdConceptoBasico,
                                Codigo = item.First().Producto.Codigo,
                                Nombre = item.First().Producto.NombreConcepto,
                                EsBien = item.First().Producto.EsBien
                            },
                            PrecioUnitario = Math.Abs(item.Sum(i => i.Importe)) / item.Sum(i => i.Cantidad),
                            Igv = Math.Abs(item.Sum(i => i.Igv)),
                        };
                        //Todo: Agregar las caracteristicas propias al detalle
                        resultado.Add(new Detalle(detalle, modoImpresionCaracteristicas));
                    }
                }
                else
                {
                    foreach (var item in detalles)
                    {
                        resultado.Add(new Detalle(item, modoImpresionCaracteristicas));
                    }
                }
            }
            else
            {
                resultado.Add(new Detalle()
                {
                    Cantidad = 1,
                    Codigo = "nn",
                    Concepto = detalleUnificado,
                    ImporteUnitario = Math.Abs(detalles.Sum(d => d.Importe)),
                    ImporteTotal = Math.Abs(detalles.Sum(d => d.Importe)),
                    ImporteIgv = Math.Abs(detalles.Sum(d => d.Igv)),
                    CaracteristicasComunes = new List<ValorCaracteristica>(),
                    CaracteristicasPropias = new List<ValorDetalleMaestroDetalleTransaccion>(),
                });
            }
            return resultado;
        }

    }

    public class DetalleDeRetencion
    {

        public decimal Cantidad { get; set; }
        public string Concepto { get; set; }
        public decimal ImporteUnitario { get; set; }
        public decimal ImporteTotal { get; set; }
        public List<ValorCaracteristica> CaracteristicasComunes { get; set; }
        public List<ValorDetalleMaestroDetalleTransaccion> CaracteristicasPropias { get; set; }
        public decimal Descuento { get; set; }
        public decimal ImporteIgv { get; set; }
        public string CodigoBarra { get; set; }

        public DetalleDeRetencion()
        {
        }

        public DetalleDeRetencion(DetalleDeOperacion detalle, ModoImpresionCaracteristicasEnum modoImpresionCaracteristicas)
        {
            this.Cantidad = detalle.Cantidad;
            this.Concepto = detalle.Producto.NombreConcepto;
            this.ImporteUnitario = detalle.PrecioUnitario;
            this.ImporteTotal = detalle.Importe;
            this.ImporteIgv = detalle.Igv;
            this.CaracteristicasComunes = new List<ValorCaracteristica>();
            this.CaracteristicasPropias = new List<ValorDetalleMaestroDetalleTransaccion>();

            if (modoImpresionCaracteristicas == ModoImpresionCaracteristicasEnum.ComunesYPropias || modoImpresionCaracteristicas == ModoImpresionCaracteristicasEnum.SoloComunes)
            {
                this.CaracteristicasComunes = detalle.Producto.CaracteristicasComunes();
                this.Concepto = detalle.Producto.NombreConceptoBasico;
            }
            if (modoImpresionCaracteristicas == ModoImpresionCaracteristicasEnum.ComunesYPropias || modoImpresionCaracteristicas == ModoImpresionCaracteristicasEnum.SoloPropias)
            {
                //this.CaracteristicasPropias = detalle.Concepto().CaracteristicasPropias();
                this.Concepto = detalle.Producto.NombreConceptoBasico;
            }
        }

    }

    public class DetalleDePercepcion
    {
        public decimal Cantidad { get; set; }
        public string Concepto { get; set; }
        public decimal ImporteUnitario { get; set; }
        public decimal ImporteTotal { get; set; }
        public List<ValorCaracteristica> CaracteristicasComunes { get; set; }
        public List<ValorDetalleMaestroDetalleTransaccion> CaracteristicasPropias { get; set; }
        public decimal Descuento { get; set; }
        public decimal ImporteIgv { get; set; }
        public string CodigoBarra { get; set; }

        public DetalleDePercepcion()
        {
        }

        public DetalleDePercepcion(DetalleDeOperacion detalle, ModoImpresionCaracteristicasEnum modoImpresionCaracteristicas)
        {
            this.Cantidad = detalle.Cantidad;
            this.Concepto = detalle.Producto.NombreConcepto;
            this.ImporteUnitario = detalle.PrecioUnitario;
            this.ImporteTotal = detalle.Importe;
            this.ImporteIgv = detalle.Igv;
            this.CaracteristicasComunes = new List<ValorCaracteristica>();
            this.CaracteristicasPropias = new List<ValorDetalleMaestroDetalleTransaccion>();

            if (modoImpresionCaracteristicas == ModoImpresionCaracteristicasEnum.ComunesYPropias || modoImpresionCaracteristicas == ModoImpresionCaracteristicasEnum.SoloComunes)
            {
                this.CaracteristicasComunes = detalle.Producto.CaracteristicasComunes();
                this.Concepto = detalle.Producto.NombreConceptoBasico;
            }
            if (modoImpresionCaracteristicas == ModoImpresionCaracteristicasEnum.ComunesYPropias || modoImpresionCaracteristicas == ModoImpresionCaracteristicasEnum.SoloPropias)
            {
                //this.CaracteristicasPropias = detalle.Concepto().CaracteristicasPropias();
                this.Concepto = detalle.Producto.NombreConceptoBasico;
            }
        }
    }

    /// <summary>
    /// Discrepancia para facturacion electronica: NroReferencia, Tipo, Descripcion
    /// </summary>
    public class Referencia
    {
        public string NroReferencia { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
    }

    /// <summary>
    /// Documento Relacionado para facturacion electronica
    /// </summary>
    public class Relacionado
    {
        public string NroDocumento { get; set; }
        public string TipoDocumento { get; set; }
    }
    public class DetalleCuota
    {
        public int Numero { get; set; }
        public string Codigo { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaVencimiento { get; set; }

        public DetalleCuota()
        { }

        internal static List<DetalleCuota> Convert(List<Cuota> cuotas, ModoPago modoPago, decimal pagoEnFechaEmision)
        {
            List<DetalleCuota> detallesCuota = new List<DetalleCuota>();
            int numeroCuota = 1;
            foreach (var cuota in cuotas)
            {
                string codigoCuota = numeroCuota.ToString().PadLeft(3, '0');
                if (!cuota.cuota_inicial || modoPago == ModoPago.CreditoRapido)
                {
                    DetalleCuota detalle = new DetalleCuota()
                    {
                        Numero = numeroCuota,
                        Codigo = "Cuota" + codigoCuota,
                        Monto = !cuota.cuota_inicial ? cuota.total: cuota.total - pagoEnFechaEmision,
                        FechaVencimiento = cuota.fecha_vencimiento
                    };

                    detalle.Monto = Math.Round(detalle.Monto, 2);
                    detallesCuota.Add(detalle);
                    numeroCuota++;
                }
            }
            return detallesCuota;
        }
    }

}

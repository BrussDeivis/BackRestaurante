using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    //public class DetalleDeOperacion_
    //{
    //    protected readonly Detalle_transaccion detalleTransaccion;
    //    //public bool precioUnitarioCalculado;
    //    public bool esBien;
    //    public string mascaraDeCalculo;
    //    public List<Complemento_Concepto_Negocio_Comercial> complementos;

    //    public DetalleDeOperacion_()
    //    {
    //    }

    //    public DetalleDeOperacion_(Detalle_transaccion _detalleTransaccion)
    //    {
    //        this.detalleTransaccion = _detalleTransaccion;
    //    }

    //    public DetalleDeOperacion_(Detalle_transaccion _detalleTransaccion, bool esBien)
    //    {
    //        this.detalleTransaccion = _detalleTransaccion;
    //        this.esBien = esBien;
    //    }

    //    public DetalleDeOperacion_(Detalle_transaccion _detalleTransaccion, bool esBien, string mascaraDeCalculo)
    //    {
    //        this.detalleTransaccion = _detalleTransaccion;
    //        this.esBien = esBien;
    //        this.mascaraDeCalculo = mascaraDeCalculo;
    //    }
    //    public DetalleDeOperacion_(Detalle_transaccion _detalleTransaccion, bool esBien, List<ValorDetalleMaestroDetalleTransaccion> valores)
    //    {
    //        this.detalleTransaccion = _detalleTransaccion;
    //        //this.detalleTransaccion.Valor_detalle_maestro_detalle_transaccion = Valor_detalle_maestro_detalle_transaccion.Convert_(valores);
    //        this.esBien = esBien;
    //        this.mascaraDeCalculo = mascaraDeCalculo;
    //    }

    //    public DetalleDeOperacion_(Detalle_transaccion _detalleTransaccion, bool esBien, string mascaraDeCalculo, List<ValorDetalleMaestroDetalleTransaccion> valores)
    //    {
    //        this.detalleTransaccion = _detalleTransaccion;
    //        //this.detalleTransaccion.Valor_detalle_maestro_detalle_transaccion = Valor_detalle_maestro_detalle_transaccion.Convert_(valores);
    //        this.esBien = esBien;
    //        this.mascaraDeCalculo = mascaraDeCalculo;
    //    }

    //    public DetalleDeOperacion_(Detalle_transaccion _detalleTransaccion, bool esBien, string mascaraDeCalculo, List<ValorDetalleMaestroDetalleTransaccion> valores, List<Complemento_Concepto_Negocio_Comercial> complementos)
    //    {
    //        this.detalleTransaccion = _detalleTransaccion;
    //        //this.detalleTransaccion.Valor_detalle_maestro_detalle_transaccion = Valor_detalle_maestro_detalle_transaccion.Convert_(valores);
    //        this.esBien = esBien;
    //        this.mascaraDeCalculo = mascaraDeCalculo;
    //        this.complementos = complementos;
    //    }

    //    public DetalleDeOperacion_(VentaMonoDetalle ventaMonoDetalle) 
    //    {
    //        this.detalleTransaccion = new Detalle_transaccion(ventaMonoDetalle.Cantidad, ventaMonoDetalle.IdConcepto, "", ventaMonoDetalle.PrecioUnitario, ventaMonoDetalle.Cantidad * ventaMonoDetalle.PrecioUnitario, null, 0, null, null, 0, 0, 0);
    //        //this.detalleTransaccion.Valor_detalle_maestro_detalle_transaccion = Valor_detalle_maestro_detalle_transaccion.Convert_(valores);
    //        this.esBien = ventaMonoDetalle.EsBien;
    //        this.mascaraDeCalculo = ventaMonoDetalle.MascaraDeCalculo;
    //    }

    //    public DetalleDeOperacion_(long idDetalle, deciConvert_mal cantidad, int idConceptoNegocio, string detalle, decimal precioUnitario, decimal total,
    //       int? idPrecio, decimal cantidadSecundaria, int? indicadorMultiproposito, int? idCuentaContable, decimal isc, decimal igv, decimal descuento,
    //       string lote, DateTime? vencimiento, string registro, bool esBien, List<ValorDetalleMaestroDetalleTransaccion> valores)
    //    {
    //        this.detalleTransaccion = new Detalle_transaccion(idDetalle, cantidad, idConceptoNegocio, detalle, precioUnitario, total, idPrecio, cantidadSecundaria, indicadorMultiproposito, idCuentaContable, isc, igv, descuento, lote, vencimiento, registro);
    //        this.esBien = esBien;
    //        //this.detalleTransaccion.Valor_detalle_maestro_detalle_transaccion = Valor_detalle_maestro_detalle_transaccion.Convert_(valores);
    //        //this.valores = valores;
    //    }

    //    public DetalleDeOperacion_(long idDetalle, decimal cantidad, int idConceptoNegocio, string detalle, decimal precioUnitario, decimal total,
    //       int? idPrecio, decimal cantidadSecundaria, int? indicadorMultiproposito, int? idCuentaContable, decimal isc, decimal igv, decimal descuento,
    //       string lote, DateTime? vencimiento, string registro, bool esBien, string mascaraDeCalculo, List<ValorDetalleMaestroDetalleTransaccion> valores)
    //    {
    //        this.detalleTransaccion = new Detalle_transaccion(idDetalle, cantidad, idConceptoNegocio, detalle, precioUnitario, total, idPrecio, cantidadSecundaria, indicadorMultiproposito, idCuentaContable, isc, igv, descuento, lote, vencimiento, registro);
    //        this.esBien = esBien;
    //        this.mascaraDeCalculo = mascaraDeCalculo;
    //        //this.detalleTransaccion.Valor_detalle_maestro_detalle_transaccion = Valor_detalle_maestro_detalle_transaccion.Convert_(valores);
    //        //this.valores = valores;
    //    }

    //    //public DetalleDeOperacion(Detalle_transaccion _detalleTransaccion, bool precioUnitarioCalculado)
    //    //{
    //    //    this.detalleTransaccion = _detalleTransaccion;
    //    //    this.precioUnitarioCalculado = precioUnitarioCalculado;
    //    //}

    //    //public DetalleDeOperacion(bool esBien, Detalle_transaccion _detalleTransaccion)
    //    //{
    //    //    this.detalleTransaccion = _detalleTransaccion;
    //    //    this.esBien = esBien;
    //    //}

    //    //public DetalleDeOperacion(Detalle_transaccion _detalleTransaccion, bool precioUnitarioCalculado, bool esBien)
    //    //{
    //    //    this.detalleTransaccion = _detalleTransaccion;
    //    //    this.precioUnitarioCalculado = precioUnitarioCalculado;
    //    //    this.esBien = esBien;
    //    //}

    //    //public DetalleDeOperacion(Detalle_transaccion _detalleTransaccion, bool precioUnitarioCalculado, bool esBien, List<ValorDetalleMaestroDetalleTransaccion> valores)
    //    //{
    //    //    this.detalleTransaccion = _detalleTransaccion;
    //    //    //this.detalleTransaccion.Valor_detalle_maestro_detalle_transaccion = Valor_detalle_maestro_detalle_transaccion.Convert_(valores);
    //    //    this.precioUnitarioCalculado = precioUnitarioCalculado;
    //    //    this.esBien = esBien;
    //    //}

    //    //public DetalleDeOperacion(Detalle_transaccion _detalleTransaccion, bool precioUnitarioCalculado, bool esBien, List<ValorDetalleMaestroDetalleTransaccion> valores, List<Complemento_Concepto_Negocio_Comercial> complementos)
    //    //{
    //    //    this.detalleTransaccion = _detalleTransaccion;
    //    //    //this.detalleTransaccion.Valor_detalle_maestro_detalle_transaccion = Valor_detalle_maestro_detalle_transaccion.Convert_(valores);
    //    //    this.precioUnitarioCalculado = precioUnitarioCalculado;
    //    //    this.esBien = esBien;
    //    //    this.complementos = complementos;
    //    //}


    //    //public DetalleDeOperacion(long idDetalle, decimal cantidad, int idConceptoNegocio, string detalle, decimal precioUnitario, decimal total,
    //    //    int? idPrecio, decimal cantidadSecundaria, int? indicadorMultiproposito, int? idCuentaContable, decimal isc, decimal igv, decimal descuento,
    //    //    string lote, DateTime? vencimiento, string registro, bool precioUnitarioCalculado, bool esBien, List<ValorDetalleMaestroDetalleTransaccion> valores)
    //    //{
    //    //    this.detalleTransaccion = new Detalle_transaccion(idDetalle, cantidad, idConceptoNegocio, detalle, precioUnitario, total, idPrecio, cantidadSecundaria, indicadorMultiproposito, idCuentaContable, isc, igv, descuento, lote, vencimiento, registro);
    //    //    //this.detalleTransaccion.Valor_detalle_maestro_detalle_transaccion = Valor_detalle_maestro_detalle_transaccion.Convert_(valores);
    //    //    this.precioUnitarioCalculado = precioUnitarioCalculado;
    //    //    this.esBien = esBien;
    //    //    //this.valores = valores;
    //    //}




    //    public long Id
    //    {
    //        get { return this.detalleTransaccion.id; }
    //    }

    //    public OperacionGenericaNivel3 Operacion()
    //    {
    //        return new OperacionGenericaNivel3(this.detalleTransaccion.Transaccion);
    //    }

    //    public ConceptoDeNegocio Concepto()
    //    {
    //        return new ConceptoDeNegocio(this.detalleTransaccion.Concepto_negocio);
    //    }

    //    public CuentaContable CuentaContable()
    //    {
    //        return this.detalleTransaccion.Cuenta_contable != null ? new CuentaContable(this.detalleTransaccion.Cuenta_contable) : null;
    //    }

    //    public int IdConceptoNegocio
    //    {
    //        get { return this.detalleTransaccion.id_concepto_negocio; }
    //    }

    //    public decimal Cantidad
    //    {
    //        get { return this.detalleTransaccion.cantidad; }
    //    }

    //    public decimal Isc
    //    {
    //        get { return (decimal)this.detalleTransaccion.isc; }
    //    }
    //    public decimal Precio
    //    {
    //        get { return this.detalleTransaccion.precio_unitario; }
    //    }

    //    public decimal Igv
    //    {
    //        get { return (decimal)this.detalleTransaccion.igv; }
    //    }

    //    public decimal Descuento
    //    {
    //        get { return (decimal)this.detalleTransaccion.descuento; }
    //    }

    //    public decimal ImporteTotal
    //    {
    //        get { return this.detalleTransaccion.total; }
    //    }

    //    public string Lote
    //    {
    //        get { return this.detalleTransaccion.lote; }
    //    }

    //    public DateTime? Vencimiento
    //    {
    //        get { return this.detalleTransaccion.vencimiento; }
    //    }

    //    public string Registro
    //    {
    //        get { return this.detalleTransaccion.registro; }
    //    }

    //    public Detalle_transaccion DetalleTransaccion()
    //    {
    //        return this.detalleTransaccion;
    //    }

    //    public List<ValorDetalleMaestroDetalleTransaccion> ValoresDetalleMaestroDetalleTransaccion()
    //    {
    //        return ValorDetalleMaestroDetalleTransaccion.Convert_(this.detalleTransaccion.Valor_detalle_maestro_detalle_transaccion.ToList());
    //    }

    //    public static List<DetalleDeOperacion> Convert_(List<Detalle_transaccion> detallesTransaccion)
    //    {
    //        List<DetalleDeOperacion> detalles = new List<DetalleDeOperacion>();
    //        foreach (var detalleTransaccion in detallesTransaccion)
    //        {
    //            detalles.Add(new DetalleDeOperacion(detalleTransaccion));
    //        }
    //        return detalles;
    //    }

    //    public static List<DetalleDeOperacion> Convertir(List<Detalle_transaccion> detallesTransaccion)
    //    {
    //        List<DetalleDeOperacion> detalles = new List<DetalleDeOperacion>();
    //        foreach (var detalleTransaccion in detallesTransaccion)
    //        {
    //            detalles.Add(new DetalleDeOperacion(detalleTransaccion, detalleTransaccion.Concepto_negocio.EsBien));
    //        }
    //        return detalles;
    //    }


    //    public static List<DetalleDeOperacion> ConvertirABienes(List<Detalle_transaccion> detallesTransaccion)
    //    {
    //        List<DetalleDeOperacion> detalles = new List<DetalleDeOperacion>();
    //        foreach (var detalleTransaccion in detallesTransaccion)
    //        {
    //            detalles.Add(new DetalleDeOperacion(detalleTransaccion, true));
    //        }
    //        return detalles;
    //    }

    //    public static List<DetalleDeOperacion> ConvertirAServicios(List<Detalle_transaccion> detallesTransaccion)
    //    {
    //        List<DetalleDeOperacion> detalles = new List<DetalleDeOperacion>();
    //        foreach (var detalleTransaccion in detallesTransaccion)
    //        {
    //            detalles.Add(new DetalleDeOperacion(detalleTransaccion, false));
    //        }
    //        return detalles;
    //    }

    //    public DetalleDeOperacion Clone()
    //    {
    //        return new DetalleDeOperacion(this.DetalleTransaccion().Clone(), this.esBien, this.mascaraDeCalculo);
    //    }

    //    public static List<DetalleDeOperacion> Clone(List<DetalleDeOperacion> toClone)
    //    {
    //        List<DetalleDeOperacion> cloned = new List<DetalleDeOperacion>();
    //        foreach (var item in toClone)
    //        {
    //            cloned.Add(item.Clone());
    //        }
    //        return cloned;
    //    }

    //    public static List<DetalleDeOperacion> Convertir(List<VentaMonoDetalle> ventasModoDetalle)
    //    {
    //        List<DetalleDeOperacion> detalles = new List<DetalleDeOperacion>();
    //        foreach (var ventaModoDetalle in ventasModoDetalle)
    //        {
    //            detalles.Add(new DetalleDeOperacion(ventaModoDetalle));
    //        }
    //        return detalles;
    //    }

    //}
}

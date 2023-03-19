//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Tsp.Sigescom.Modelo.Entidades.Custom
//{
//    public class DetalleOrdenDeOperacion: DetalleDeOperacion
//    {
        

//        public DetalleOrdenDeOperacion()
//        {
//        }

//        public DetalleOrdenDeOperacion(Detalle_transaccion _detalleTransaccion)
//        {
//            this.detalleTransaccion = _detalleTransaccion;
//        }

//        public DetalleOrdenDeOperacion(Detalle_transaccion _detalleTransaccion, bool precioUnitarioCalculado)
//        {
//            this.detalleTransaccion = _detalleTransaccion;
//            this.precioUnitarioCalculado = precioUnitarioCalculado;
//        }

//        public DetalleOrdenDeOperacion(bool esBien, Detalle_transaccion _detalleTransaccion)
//        {
//            this.detalleTransaccion = _detalleTransaccion;
//            this.esBien = esBien;
//        }

//        public DetalleOrdenDeOperacion(Detalle_transaccion _detalleTransaccion, bool precioUnitarioCalculado, bool esBien)
//        {
//            this.detalleTransaccion = _detalleTransaccion;
//            this.precioUnitarioCalculado = precioUnitarioCalculado;
//            this.esBien = esBien;
//        }

//        public DetalleOrdenDeOperacion(Detalle_transaccion _detalleTransaccion, bool precioUnitarioCalculado, bool esBien, List<ValorDetalleMaestroDetalleTransaccion> valores)
//        {
//            this.detalleTransaccion = _detalleTransaccion;
//            this.detalleTransaccion.Valor_detalle_maestro_detalle_transaccion = Valor_detalle_maestro_detalle_transaccion.Convert_(valores);
//            this.precioUnitarioCalculado = precioUnitarioCalculado;
//            this.esBien = esBien;
//            //this.valores = valores;
//        }


//        public DetalleOrdenDeOperacion(long idDetalle, decimal cantidad, int idConceptoNegocio, string detalle, decimal precioUnitario, decimal total,
//            int? idPrecio, decimal cantidadSecundaria, int? indicadorMultiproposito, int? idCuentaContable, decimal isc, decimal igv, decimal descuento,
//            string lote, DateTime? vencimiento, string registro,  bool precioUnitarioCalculado, bool esBien, List<ValorDetalleMaestroDetalleTransaccion> valores)
//        {
//            this.detalleTransaccion = new Detalle_transaccion(idDetalle, cantidad, idConceptoNegocio, detalle, precioUnitario, total, idPrecio, cantidadSecundaria, indicadorMultiproposito, idCuentaContable, isc, igv, descuento, lote, vencimiento, registro);
//            this.detalleTransaccion.Valor_detalle_maestro_detalle_transaccion = Valor_detalle_maestro_detalle_transaccion.Convert_(valores);
//            this.precioUnitarioCalculado = precioUnitarioCalculado;
//            this.esBien = esBien;
//            //this.valores = valores;
//        }




//        public long Id
//        {
//            get { return this.detalleTransaccion.id; }
//        }

//        public OperacionGenericaNivel3 Orden()
//        {
//            return new OperacionGenericaNivel3(this.detalleTransaccion.Transaccion);
//        }

//        public ConceptoDeNegocio Concepto()
//        {
//            return new ConceptoDeNegocio(this.detalleTransaccion.Concepto_negocio);
//        }

//        public CuentaContable CuentaContable()
//        {
//            return this.detalleTransaccion.Cuenta_contable != null ? new CuentaContable(this.detalleTransaccion.Cuenta_contable) : null;
//        }

//        public int IdConceptoNegocio
//        {
//            get { return this.detalleTransaccion.id_concepto_negocio; }
//        }

//        public decimal Cantidad
//        {
//            get { return this.detalleTransaccion.cantidad; }
//        }

//        public decimal Isc
//        {
//            get { return (decimal)this.detalleTransaccion.isc; }
//        }
//        public decimal Precio
//        {
//            get { return this.detalleTransaccion.precio_unitario; }
//        }

//        public decimal Igv
//        {
//            get { return (decimal)this.detalleTransaccion.igv; }
//        }

//        public decimal Descuento
//        {
//            get { return (decimal)this.detalleTransaccion.descuento; }
//        }

//        public decimal ImporteTotal
//        {
//            get { return this.detalleTransaccion.total; }
//        }

//        public string Lote
//        {
//            get { return this.detalleTransaccion.lote; }
//        }

//        public DateTime? Vencimiento
//        {
//            get { return this.detalleTransaccion.vencimiento; }
//        }

//        public string Registro
//        {
//            get { return this.detalleTransaccion.registro; }
//        }

//        public Detalle_transaccion DetalleTransaccion()
//        {
//            return this.detalleTransaccion;
//        }

//        public List<ValorDetalleMaestroDetalleTransaccion> ValoresDetalleMaestroDetalleTransaccion()
//        {
//            return ValorDetalleMaestroDetalleTransaccion.Convert_(this.detalleTransaccion.Valor_detalle_maestro_detalle_transaccion.ToList());
//        }

//        public static List<DetalleOrdenDeOperacion> Convert_(List<Detalle_transaccion> detallesTransaccion)
//        {
//            List<DetalleOrdenDeOperacion> detalles = new List<DetalleOrdenDeOperacion>();
//            foreach (var detalleTransaccion in detallesTransaccion)
//            {
//                detalles.Add(new DetalleOrdenDeOperacion(detalleTransaccion));
//            }
//            return detalles;
//        }

//        public static List<DetalleOrdenDeOperacion> Convertir(List<Detalle_transaccion> detallesTransaccion)
//        {
//            List<DetalleOrdenDeOperacion> detalles = new List<DetalleOrdenDeOperacion>();
//            foreach (var detalleTransaccion in detallesTransaccion)
//            {
//                detalles.Add(new DetalleOrdenDeOperacion(detalleTransaccion.Concepto_negocio.EsBien,detalleTransaccion));
//            }
//            return detalles;
//        }


//        public static List<DetalleOrdenDeOperacion> ConvertirABienes(List<Detalle_transaccion> detallesTransaccion)
//        {
//            List<DetalleOrdenDeOperacion> detalles = new List<DetalleOrdenDeOperacion>();
//            foreach (var detalleTransaccion in detallesTransaccion)
//            {
//                detalles.Add(new DetalleOrdenDeOperacion(true, detalleTransaccion));
//            }
//            return detalles;
//        }

//        public static List<DetalleOrdenDeOperacion> ConvertirAServicios(List<Detalle_transaccion> detallesTransaccion)
//        {
//            List<DetalleOrdenDeOperacion> detalles = new List<DetalleOrdenDeOperacion>();
//            foreach (var detalleTransaccion in detallesTransaccion)
//            {
//                detalles.Add(new DetalleOrdenDeOperacion(false, detalleTransaccion));
//            }
//            return detalles;
//        }

//        public DetalleOrdenDeOperacion Clone()
//        {
//            return new DetalleOrdenDeOperacion (this.DetalleTransaccion().Clone());
//        }

//        public static List<DetalleOrdenDeOperacion> Clone(List<DetalleOrdenDeOperacion> toClone)
//        {
//            List<DetalleOrdenDeOperacion> cloned = new List<DetalleOrdenDeOperacion>();
//            foreach (var item in toClone)
//            {
//                cloned.Add(item.Clone());
//            }
//            return cloned;
//        }
//    }
//}

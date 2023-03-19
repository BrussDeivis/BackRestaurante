using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;

namespace Tsp.Sigescom.Modelo.Entidades
{
    public partial class Detalle_transaccion
    {
        //private List<Valor_detalle_maestro_detalle_transaccion> valores;

        //public List<Valor_detalle_maestro_detalle_transaccion> Valores { get => valores; set => valores = value; }

        public Detalle_transaccion Clone()
        {
            return new Detalle_transaccion(this.cantidad, this.id_concepto_negocio, this.detalle, this.precio_unitario, this.total, this.id_precio, (decimal)this.cantidad_secundaria, this.indicadorMultiproposito, this.id_cuenta_contable, (decimal)this.isc, (decimal)this.igv, (decimal)this.descuento, this.lote, this.vencimiento, this.registro)
            {
                cantidad_1 = this.cantidad_1,
                Valor_detalle_maestro_detalle_transaccion = (this.Valor_detalle_maestro_detalle_transaccion != null && this.Valor_detalle_maestro_detalle_transaccion.Count() > 0) ? Entidades.Valor_detalle_maestro_detalle_transaccion.Clone(this.Valor_detalle_maestro_detalle_transaccion.ToList()) : null
            };
        }

        public static List<Detalle_transaccion> Clone(List<Detalle_transaccion> toClone)
        {
            List<Detalle_transaccion> cloned = new List<Detalle_transaccion>();
            foreach (var item in toClone)
            {
                cloned.Add(item.Clone());
            }
            return cloned;
        }

        public Detalle_transaccion(long idTransaccion, decimal cantidad, int idConceptoNegocio, string detalle, decimal precioUnitario, decimal total, int? idPrecio, decimal cantidadSecundaria, int indicadorMultiproposito, int? idCuentaContable, decimal isc, decimal igv, decimal descuento)
        {
            SetData(idTransaccion, cantidad, idConceptoNegocio, detalle, precioUnitario, total, idPrecio, cantidadSecundaria, indicadorMultiproposito, idCuentaContable, isc, igv, descuento);
            ValidarId(idCuentaContable, idTransaccion, idConceptoNegocio, idPrecio);
        }

        public Detalle_transaccion(long idTransaccion, decimal cantidad, int idConceptoNegocio, string detalle, decimal precioUnitario, decimal total, int? idPrecio, decimal cantidadSecundaria, int indicadorMultiproposito, int? idCuentaContable, decimal isc, decimal igv, decimal descuento, string lote, DateTime? vencimiento, string registro)
        {
            SetData(idTransaccion, cantidad, idConceptoNegocio, detalle, precioUnitario, total, idPrecio, cantidadSecundaria, indicadorMultiproposito, idCuentaContable, isc, igv, descuento);
            ValidarId(idCuentaContable, idTransaccion, idConceptoNegocio, idPrecio);
        }

        public Detalle_transaccion(decimal cantidad, int idConceptoNegocio, string detalle, decimal precioUnitario, decimal total, int? idPrecio, decimal cantidadSecundaria, int? indicadorMultiproposito, int? idCuentaContable, decimal isc, decimal igv, decimal descuento)
        {
            SetData(cantidad, idConceptoNegocio, detalle, precioUnitario, total, idPrecio, cantidadSecundaria, indicadorMultiproposito, idCuentaContable, isc, igv, descuento);
            ValidarId(idCuentaContable, idConceptoNegocio, idPrecio);
        }

        public Detalle_transaccion(decimal cantidad, int idConceptoNegocio, string detalle, decimal precioUnitario, decimal total, int? idPrecio, decimal cantidadSecundaria, int? indicadorMultiproposito, int? idCuentaContable, decimal isc, decimal igv, decimal descuento, string lote, DateTime? vencimiento, string registro)
        {
            SetData(cantidad, idConceptoNegocio, detalle, precioUnitario, total, idPrecio, cantidadSecundaria, indicadorMultiproposito, idCuentaContable, isc, igv, descuento, lote, vencimiento, registro);
            ValidarId(idCuentaContable, idConceptoNegocio, idPrecio);
        }

        public Detalle_transaccion(decimal cantidad, int idConceptoNegocio, string detalle, decimal precioUnitario, decimal total, int? idPrecio, decimal cantidadSecundaria, int? indicadorMultiproposito, int? idCuentaContable, decimal isc, decimal igv, decimal descuento, string lote, DateTime? vencimiento, string registro, decimal cantidad_1)
        {
            this.cantidad_1 = cantidad_1;
            SetData(cantidad, idConceptoNegocio, detalle, precioUnitario, total, idPrecio, cantidadSecundaria, indicadorMultiproposito, idCuentaContable, isc, igv, descuento, lote, vencimiento, registro);
            ValidarId(idCuentaContable, idConceptoNegocio, idPrecio);
        }

        public Detalle_transaccion(long idDetalle, decimal cantidad, int idConceptoNegocio, string detalle, decimal precioUnitario, decimal total, int? idPrecio, decimal cantidadSecundaria, int? indicadorMultiproposito, int? idCuentaContable, decimal isc, decimal igv, decimal descuento, string lote, DateTime? vencimiento, string registro)
        {
            InitSets();
            this.id = idDetalle;
            SetData(cantidad, idConceptoNegocio, detalle, precioUnitario, total, idPrecio, cantidadSecundaria, indicadorMultiproposito, idCuentaContable, isc, igv, descuento, lote, vencimiento, registro);
            ValidarId(idCuentaContable, idConceptoNegocio, idPrecio);
        }
        public Detalle_transaccion(long idDetalle, decimal cantidad, int idConceptoNegocio, string detalle, decimal precioUnitario, decimal total, int? idPrecio, decimal cantidadSecundaria, int? indicadorMultiproposito, int? idCuentaContable, decimal isc, decimal igv, decimal descuento, string lote, DateTime? vencimiento, string registro, decimal cantidad_1)
        {
            InitSets();
            this.id = idDetalle;
            this.cantidad_1 = cantidad_1;
            SetData(cantidad, idConceptoNegocio, detalle, precioUnitario, total, idPrecio, cantidadSecundaria, indicadorMultiproposito, idCuentaContable, isc, igv, descuento, lote, vencimiento, registro);
            ValidarId(idCuentaContable, idConceptoNegocio, idPrecio);
        }
        public void SetData(long idTransaccion, decimal cantidad, int idConceptoNegocio, string detalle, decimal precioUnitario, decimal total, int? idPrecio,
            decimal cantidadSecundaria, int indicadorMultiproposito, int? idCuentaContable, decimal isc, decimal igv, decimal descuento)
        {
            this.id_transaccion = idTransaccion;
            SetData(cantidad, idConceptoNegocio, detalle, precioUnitario, total, idPrecio, cantidadSecundaria, indicadorMultiproposito, idCuentaContable, isc, igv, descuento);
        }

        public void SetData(long idTransaccion, decimal cantidad, int idConceptoNegocio, string detalle, decimal precioUnitario, decimal total, int? idPrecio,
            decimal cantidadSecundaria, int indicadorMultiproposito, int? idCuentaContable, decimal isc, decimal igv, decimal descuento, string lote, DateTime? vencimiento, string registro)
        {
            this.id_transaccion = idTransaccion;
            SetData(cantidad, idConceptoNegocio, detalle, precioUnitario, total, idPrecio, cantidadSecundaria, indicadorMultiproposito, idCuentaContable, isc, igv, descuento, lote, vencimiento, registro);
        }

        public void SetData(decimal cantidad, int idConceptoNegocio, string detalle, decimal precioUnitario, decimal total, int? idPrecio, decimal cantidadSecundaria, int? indicadorMultiproposito, int? idCuentaContable, decimal isc, decimal igv, decimal descuento)
        {
            this.cantidad = cantidad;
            this.id_concepto_negocio = idConceptoNegocio;
            this.detalle = detalle;
            this.precio_unitario = precioUnitario;
            this.total = total;
            this.id_precio = idPrecio;
            this.cantidad_secundaria = cantidadSecundaria;
            this.indicadorMultiproposito = indicadorMultiproposito;
            this.id_cuenta_contable = idCuentaContable;
            this.isc = isc;
            this.igv = igv;
            this.descuento = descuento;
        }

        public void SetData(decimal cantidad, int idConceptoNegocio, string detalle, decimal precioUnitario, decimal total, int? idPrecio, decimal cantidadSecundaria, int? indicadorMultiproposito, int? idCuentaContable, decimal isc, decimal igv, decimal descuento, string lote, DateTime? vencimiento, string registro)
        {
            this.cantidad = cantidad;
            this.id_concepto_negocio = idConceptoNegocio;
            this.detalle = detalle;
            this.precio_unitario = precioUnitario;
            this.total = total;
            this.id_precio = idPrecio;
            this.cantidad_secundaria = cantidadSecundaria;
            this.indicadorMultiproposito = indicadorMultiproposito;
            this.id_cuenta_contable = idCuentaContable;
            this.isc = isc;
            this.igv = igv;
            this.descuento = descuento;
            this.lote = lote;
            this.vencimiento = vencimiento;
            this.registro = registro;
        }

        public void InitSets()
        {
            this.Valor_detalle_maestro_detalle_transaccion = new HashSet<Valor_detalle_maestro_detalle_transaccion>();
        }

        protected void ValidarId(int? idCuentaContable, long idTransaccion, int idConceptoNegocio, int? idPrecio)
        {
            if (idTransaccion < 1) { throw new IdNoValidoException(idTransaccion, "transaccion"); }
            ValidarId(idCuentaContable, idConceptoNegocio, idPrecio);
        }
        protected void ValidarId(int? idCuentaContable, int idConceptoNegocio, int? idPrecio)
        {
            if (idCuentaContable < 1 && idCuentaContable != null) { throw new IdNoValidoException((int)idCuentaContable, "cuenta contable"); }
            if (idConceptoNegocio < 1) { throw new IdNoValidoException(idConceptoNegocio, "concepto negocio"); }
            if (idPrecio < 1 && idPrecio != null) { throw new IdNoValidoException((int)idPrecio, "precio"); }
        }
    }
}

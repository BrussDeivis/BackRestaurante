using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;

namespace Tsp.Sigescom.Modelo.Entidades
{
    public partial class Transaccion
    {
        public Transaccion(string codigo, long? idTransaccionPadre, DateTime fechaRegistroSistema, int idTipoTransaccion, int idUnidadNegocio, bool esConcreta,
            DateTime fechaInicio, DateTime fechaFin, long idComprobante, string comentario, DateTime fechaRegistroContable, int idEmpleado, decimal importeTotal,
            int idActorNegocioInterno, int idMoneda, decimal tipoCambio, long? idTransaccionReferencia, int idActorNegocioExterno)
        {
            InitSets();
            SetData(codigo, idTransaccionPadre, fechaRegistroSistema, idTipoTransaccion, idUnidadNegocio, esConcreta, fechaInicio, fechaFin, idComprobante,
                comentario, fechaRegistroContable, idEmpleado, importeTotal, idActorNegocioInterno, idMoneda, tipoCambio, idTransaccionReferencia, idActorNegocioExterno);
            ValidarId(idEmpleado, idActorNegocioExterno, idActorNegocioInterno, idComprobante, idMoneda, idTipoTransaccion, idTransaccionPadre, idTransaccionReferencia);
        }
        public Transaccion(string codigo, long? idTransaccionPadre, DateTime fechaRegistroSistema, int idTipoTransaccion, int idUnidadNegocio, bool esConcreta,
            DateTime fechaInicio, DateTime fechaFin, string comentario, DateTime fechaRegistroContable, int idEmpleado, decimal importeTotal,
            int idActorNegocioInterno, int idMoneda, decimal tipoCambio, long? idTransaccionReferencia, int idActorNegocioExterno, decimal descuentoGlobal, decimal descuentoPorItem, decimal anticipo, decimal gravada, decimal exonerada, decimal inafecta, decimal gratuita, decimal igv, decimal isc, decimal icbper, decimal otrosCargos, decimal otrosTributos)
        {
            InitSets();
            SetData(codigo, idTransaccionPadre, fechaRegistroSistema, idTipoTransaccion, idUnidadNegocio, esConcreta, fechaInicio, fechaFin, comentario, fechaRegistroContable, idEmpleado, importeTotal, idActorNegocioInterno, idMoneda, tipoCambio, idTransaccionReferencia, idActorNegocioExterno, descuentoGlobal, descuentoPorItem, anticipo, gravada, exonerada, inafecta, gratuita, igv, isc, icbper, otrosCargos, otrosTributos);
            ValidarId(idEmpleado, idActorNegocioExterno, idActorNegocioInterno, idMoneda, idTipoTransaccion, idTransaccionPadre, idTransaccionReferencia);
        }
        public void EstablecerCantidadesPuntos(decimal puntosGanados, decimal puntosCajeados, decimal puntosPendientes, decimal puntosAcumulados)
        {
            this.cantidad1 = puntosGanados;
            this.cantidad2 = puntosCajeados;
            this.cantidad3 = puntosPendientes;
            this.cantidad4 = puntosAcumulados;
        }
        public Transaccion(string codigo, long? idTransaccionPadre, DateTime fechaRegistroSistema, int idTipoTransaccion, int idUnidadNegocio, bool esConcreta,
            DateTime fechaInicio, DateTime fechaFin, string comentario, DateTime fechaRegistroContable, int idEmpleado, decimal importeTotal,
            int idActorNegocioInterno, int idMoneda, decimal tipoCambio, long? idTransaccionReferencia, int idActorNegocioExterno)
        {
            InitSets();
            SetData(codigo, idTransaccionPadre, fechaRegistroSistema, idTipoTransaccion, idUnidadNegocio, esConcreta, fechaInicio, fechaFin,
                comentario, fechaRegistroContable, idEmpleado, importeTotal, idActorNegocioInterno, idMoneda, tipoCambio, idTransaccionReferencia, idActorNegocioExterno);
            ValidarId(idEmpleado, idActorNegocioExterno, idActorNegocioInterno, idMoneda, idTipoTransaccion, idTransaccionPadre, idTransaccionReferencia);
        }
        public void SetData(string codigo, long? idTransaccionPadre, DateTime fechaRegistroSistema, int idTipoTransaccion, int idUnidadNegocio, bool esConcreta,
            DateTime fechaInicio, DateTime fechaFin, long idComprobante, string comentario, DateTime fechaRegistroContable, int idEmpleado, decimal importeTotal,
            int idActorNegocioInterno, int idMoneda, decimal tipoCambio, long? idTransaccionReferencia, int idActorNegocioExterno)
        {
            InitSets();
            this.id_comprobante = idComprobante;
            SetData(codigo, idTransaccionPadre, fechaRegistroSistema, idTipoTransaccion, idUnidadNegocio, esConcreta, fechaInicio, fechaFin,
                comentario, fechaRegistroContable, idEmpleado, importeTotal, idActorNegocioInterno, idMoneda, tipoCambio, idTransaccionReferencia, idActorNegocioExterno);
        }
        public void SetData(string codigo, long? idTransaccionPadre, DateTime fechaRegistroSistema, int idTipoTransaccion, int idUnidadNegocio, bool esConcreta,
            DateTime fechaInicio, DateTime fechaFin, string comentario, DateTime fechaRegistroContable, int idEmpleado, decimal importeTotal,
            int idActorNegocioInterno, int idMoneda, decimal tipoCambio, long? idTransaccionReferencia, int idActorNegocioExterno, decimal descuentoGlobal, decimal descuentoPorItem, decimal anticipo, decimal gravada, decimal exonerada, decimal inafecta, decimal gratuita, decimal igv, decimal isc, decimal icbper, decimal otrosCargos, decimal otrosTributos)
        {
            InitSets();
            this.importe1 = descuentoGlobal;
            this.importe2 = descuentoPorItem;
            this.importe3 = anticipo;
            this.importe4 = gravada;
            this.importe5 = exonerada;
            this.importe6 = inafecta;
            this.importe7 = gratuita;
            this.importe8 = igv;
            this.importe9 = isc;
            this.importe10 = icbper;
            this.importe11 = otrosCargos;
            this.importe12 = otrosTributos;
            SetData(codigo, idTransaccionPadre, fechaRegistroSistema, idTipoTransaccion, idUnidadNegocio, esConcreta, fechaInicio, fechaFin,
                comentario, fechaRegistroContable, idEmpleado, importeTotal, idActorNegocioInterno, idMoneda, tipoCambio, idTransaccionReferencia, idActorNegocioExterno);
        }
        public void SetData(string codigo, long? idTransaccionPadre, DateTime fechaRegistroSistema, int idTipoTransaccion, int idUnidadNegocio, bool esConcreta,
            DateTime fechaInicio, DateTime fechaFin, string comentario, DateTime fechaRegistroContable, int idEmpleado, decimal importeTotal,
            int idActorNegocioInterno, int idMoneda, decimal tipoCambio, long? idTransaccionReferencia, int idActorNegocioExterno)
        {
            this.codigo = codigo;
            this.id_transaccion_padre = idTransaccionPadre;
            this.fecha_registro_sistema = fechaRegistroSistema;
            this.id_tipo_transaccion = idTipoTransaccion;
            this.id_unidad_negocio = idUnidadNegocio;
            this.es_concreta = esConcreta;
            this.fecha_inicio = fechaInicio;
            this.fecha_fin = fechaFin;
            this.comentario = comentario;
            this.fecha_registro_contable = fechaRegistroContable;
            this.id_empleado = idEmpleado;
            this.importe_total = importeTotal;
            this.id_actor_negocio_interno = idActorNegocioInterno;
            this.id_moneda = idMoneda;
            this.tipo_cambio = tipoCambio;
            this.id_transaccion_referencia = idTransaccionReferencia;
            this.id_actor_negocio_externo = idActorNegocioExterno;
        }
        public void InitSets()
        {
            this.Actor_negocio_por_transaccion = new HashSet<Actor_negocio_por_transaccion>();
            this.Detalle_transaccion = new HashSet<Detalle_transaccion>();
            this.Estado_transaccion = new HashSet<Estado_transaccion>();
            this.Pago_cuota = new HashSet<Pago_cuota>();
            this.Cuota = new HashSet<Cuota>();
            this.Traza_pago = new HashSet<Traza_pago>();
            this.Parametro_transaccion = new HashSet<Parametro_transaccion>();
            this.Parametro_transaccion = new HashSet<Parametro_transaccion>();
            this.Transaccion1 = new HashSet<Transaccion>();
        }
        protected void ValidarId(int idEmpleado, int idActorNegocioExterno, int idActorNegocioInterno, long idComprobante, int idMoneda, int idTipoTransaccion, long? idTransaccionPadre, long? idTransaccionReferencia)
        {
            if (idComprobante < 1) { throw new IdNoValidoException(idComprobante, "comprobante"); }
            ValidarId(idEmpleado, idActorNegocioExterno, idActorNegocioInterno, idMoneda, idTipoTransaccion, idTransaccionPadre, idTransaccionReferencia);
        }
        protected void ValidarId(int idEmpleado, int idActorNegocioExterno, int idActorNegocioInterno, int idMoneda, int idTipoTransaccion, long? idTransaccionPadre, long? idTransaccionReferencia)
        {
            if (idEmpleado < 1) { throw new IdNoValidoException(idEmpleado, "empleado"); }
            if (idActorNegocioExterno < 1) { throw new IdNoValidoException(idActorNegocioExterno, "actor negocio externo"); }
            if (idActorNegocioInterno < 1) { throw new IdNoValidoException(idActorNegocioInterno, "actor negocio interno"); }
            if (idMoneda < 1) { throw new IdNoValidoException(idMoneda, "moneda"); }
            if (idTipoTransaccion < 1) { throw new IdNoValidoException(idTipoTransaccion, "tipo transaccion"); }
            if (idTransaccionPadre < 1 && idTransaccionPadre != null) { throw new IdNoValidoException((long)idTransaccionPadre, "transaccion padre"); }
            if (idTransaccionReferencia < 1 && idTransaccionReferencia != null) { throw new IdNoValidoException((long)idTransaccionReferencia, "transaccion referencia"); }
        }
        public Detalle_maestro EstadoActual
        {
            get { return this.Estado_transaccion.OrderByDescending(est => est.id).FirstOrDefault().Detalle_maestro; }
        }
        public int IdEstadoActual
        {
            get { return this.Estado_transaccion.OrderByDescending(est => est.id).FirstOrDefault().id_estado; }
        }
        public Detalle_maestro EstadoAnteriorAlActual
        {
            get
            {
                var estadoAnterior = this.Estado_transaccion.OrderByDescending(est => est.id).Skip(1).Take(1).SingleOrDefault();
                bool existeEstadoAnterior = estadoAnterior != null;
                return existeEstadoAnterior ? estadoAnterior.Detalle_maestro : null;
            }
        }

        public bool EstaTransmitido()
        {
            return this.Evento_transaccion.Select(est => est.id_evento).Contains(MaestroSettings.Default.IdDetalleMaestroEstadoTransmitido);
        }

        public void AgregarDetalles(List<Detalle_transaccion> detalles)
        {
            foreach (var detalle in detalles)
            {
                this.Detalle_transaccion.Add(detalle);
                detalle.Transaccion = this;
            }
        }
        public Transaccion CloneTransaccionYDetalles()
        {
            //No clonar id   
            var cloneTransaccion = new Transaccion(codigo, id_transaccion_padre, fecha_registro_sistema, id_tipo_transaccion, id_unidad_negocio, es_concreta, fecha_inicio, fecha_fin,
                comentario, fecha_registro_sistema, id_empleado, importe_total, id_actor_negocio_interno, id_moneda, tipo_cambio, id_transaccion_referencia, id_actor_negocio_externo, importe1, importe2, importe3, importe4,importe5,importe6, importe7, importe8, importe9, importe10, importe11, importe12);
            foreach (var detalle in this.Detalle_transaccion)
            {
                var detalleClone = detalle.Clone();
                cloneTransaccion.Detalle_transaccion.Add(detalleClone);
                detalleClone.Transaccion = cloneTransaccion;
            }
            return cloneTransaccion;
        }
        public Transaccion CloneTransaccion()
        {
            //No clonar id   
            var cloneTransaccion = new Transaccion(codigo, id_transaccion_padre, fecha_registro_sistema, id_tipo_transaccion, id_unidad_negocio, es_concreta, fecha_inicio, fecha_fin,
                comentario, fecha_registro_sistema, id_empleado, importe_total, id_actor_negocio_interno, id_moneda, tipo_cambio, id_transaccion_referencia, id_actor_negocio_externo, importe1, importe2, importe3, importe4, importe5, importe6, importe7, importe8, importe9, importe10, importe11, importe12);

            return cloneTransaccion;
        }
        public Transaccion TransaccionHija(int idTipoTransaccion)
        {
            return Transaccion1.Single(t1 => t1.id_tipo_transaccion == idTipoTransaccion);
        }
    }
}

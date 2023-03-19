using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class Cotizacion 
    {
        private readonly Transaccion transaccion;

        public Cotizacion(Transaccion _transaccion)
        {
            this.transaccion = _transaccion;
        }
        
        public OrdenDeCotizacion OrdenDeCotizacion()
        {
            return new OrdenDeCotizacion(this.transaccion.Transaccion1.Single(t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenDeCotizacion));
        }
        public long Id
        {
            get { return this.transaccion.id; }
        }
        public string Codigo
        {
            get { return this.transaccion.codigo; }
        }
        public DateTime FechaRegistro()
        {
            return this.transaccion.fecha_registro_sistema;
        }
        public int Vendedor()
        {
            return this.transaccion.id_empleado;
        }
        public int IdCliente()
        {
            return this.transaccion.id_actor_negocio_externo;
        }
        public Cliente Cliente()
        {
            return new Cliente(this.transaccion.Actor_negocio1);//Actor_Negocio => Empleado, Actor_Negocio1 => Cliente , Actor_Negocio2 => Entidad Interna
        }
        public Comprobante Comprobante()
        {
            return this.transaccion.Comprobante;
        }
        public Transaccion Transaccion()
        {
            return this.transaccion;
        }
        public static List<Cotizacion> Convert(List<Transaccion> transacciones)
        {
            List<Cotizacion> cotizaciones = new List<Cotizacion>();
            foreach (var transaccion in transacciones)
            {
                cotizaciones.Add(new Cotizacion(transaccion));
            }
            return cotizaciones;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class OrdenDeTrasladoInterno : OrdenDeMovimientoDeAlmacen
    {
        public OrdenDeTrasladoInterno()
        {
        }

        public OrdenDeTrasladoInterno(Transaccion _transaccion)
        {
            this.transaccion = _transaccion;
        }

        public CentroDeAtencionExtendido AlmacenOrigen()
        {
            return new CentroDeAtencionExtendido(this.transaccion.Actor_negocio2);
        }

        public CentroDeAtencionExtendido AlmacenDestino()
        {
            return new CentroDeAtencionExtendido(this.transaccion.Actor_negocio1);
        }

        public TrasladoInterno Desplazamiento()
        {
            return new TrasladoInterno(this.transaccion.Transaccion2);
        }

        public long IdDesplazamiento()
        {
            return (long)this.transaccion.id_transaccion_padre;
        }

        public DateTime FechaOrden()
        {
            return this.transaccion.fecha_inicio;
        }
       
        public Empleado Responsable()
        {
            return this.Empleado();
        }

        public long IdReponsable()
        {
            return this.transaccion.id_empleado;
        }

        public static List<OrdenDeTrasladoInterno> Convert_(List<Transaccion> transacciones)
        {
            List<OrdenDeTrasladoInterno> ordenesDesplazamiento = new List<OrdenDeTrasladoInterno>();
            foreach (var transaccion in transacciones)
            {
                ordenesDesplazamiento.Add(new OrdenDeTrasladoInterno(transaccion));
            }
            return ordenesDesplazamiento;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class TrasladoInterno
    {
        protected Transaccion transaccion;

        public TrasladoInterno(Transaccion _transaccion)
        {
            this.transaccion = _transaccion;
        }

        public Operacion OperacionGenerica()
        {
            return new Operacion(this.transaccion.Transaccion2);
        }

        public OrdenDeTrasladoInterno OrdenDeDesplazamiento()
        {
            return new OrdenDeTrasladoInterno(this.transaccion.Transaccion1.Single(t => t.id_tipo_transaccion == TransaccionSettings.Default.IdTipoTransaccionOrdenDeDesplazamiento));
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

        public long IdDestinatario()
        {
            return this.transaccion.id_actor_negocio_externo;
        }

        public static List<TrasladoInterno> Convert(List<Transaccion> transacciones)
        {
            List<TrasladoInterno> desplazamientos = new List<TrasladoInterno>();
            foreach (var transaccion in transacciones)
            {
                desplazamientos.Add(new TrasladoInterno(transaccion));
            }
            return desplazamientos;
        }
    }
}
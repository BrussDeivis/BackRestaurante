using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class TipoDeTransaccion
    {
        private readonly Tipo_transaccion tipoTransaccion;

        public TipoDeTransaccion()
        {
            this.tipoTransaccion = new Tipo_transaccion();
            this.Id = 0;
            this.Nombre = "";
        }

        public TipoDeTransaccion(Tipo_transaccion tipoTransaccion)
        {
            this.tipoTransaccion = tipoTransaccion;
        }

        public int Id
        {
            set { this.tipoTransaccion.id = value; }
            get { return this.tipoTransaccion.id; }
        }

        public string Nombre
        {
            set { this.tipoTransaccion.nombre = value;}
            get { return this.tipoTransaccion.nombre; }
        }

        public string Descripcion
        {
            get { return this.tipoTransaccion.descripcion; }
        }

        public string NombreTransaccionMaestro()
        {

            if (this.tipoTransaccion.id_tipo_transaccion_padre == null)
            {
                return "";
            }
            else
            {
                return this.tipoTransaccion.Tipo_transaccion2.nombre;
            }
           
        }

        public TipoDeTransaccion TransaccionMaestro()
        {
            if (this.tipoTransaccion.id_tipo_transaccion_padre == null)
            {
                return new TipoDeTransaccion();
            }
            else
            {
                return new TipoDeTransaccion(this.tipoTransaccion.Tipo_transaccion2);

            }
        }

        public List<AccionDeNegocioPorTipoTransaccion> ListaDeAccionesDeNegocioPorTipoTransaccion()
        {
                return AccionDeNegocioPorTipoTransaccion.Convert(this.tipoTransaccion.Accion_de_negocio_por_tipo_transaccion.ToList());
        }

        public static List<TipoDeTransaccion> Convert(List<Tipo_transaccion> tipoTransacciones)
        {
            var tipoDeTransacciones = new List<TipoDeTransaccion>();

            foreach (var tipoDeTrasaccion in tipoTransacciones)
            {
                tipoDeTransacciones.Add(new TipoDeTransaccion(tipoDeTrasaccion));
            }
            return tipoDeTransacciones;
        }
    }
}

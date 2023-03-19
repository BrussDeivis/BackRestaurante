using System.Collections.Generic;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class AccionDeNegocioPorTipoTransaccion
    {
        private Accion_de_negocio_por_tipo_transaccion accionDeNegocioPorTipoTransaccion;

        public AccionDeNegocioPorTipoTransaccion()
        {

        }

        public AccionDeNegocioPorTipoTransaccion(int  idAccionDeNeogcio, int id ,bool valor)
        {
            this.accionDeNegocioPorTipoTransaccion = new Accion_de_negocio_por_tipo_transaccion();
            this.IdAccionDeNegocio = idAccionDeNeogcio;
            this.Id = id;
            this.Valor = valor;
        }

        public AccionDeNegocioPorTipoTransaccion(Accion_de_negocio_por_tipo_transaccion accionDeNegocioPorTipoTransaccion)
        {
            this.accionDeNegocioPorTipoTransaccion = accionDeNegocioPorTipoTransaccion;
        }

        public int Id
        {
            set { this.accionDeNegocioPorTipoTransaccion.id = value; }
            get { return this.accionDeNegocioPorTipoTransaccion.id; }
        }

        /// <summary>
        /// True: Entrada
        /// False: Salida
        /// </summary>
        public bool  Valor 
        {
            get { return this.accionDeNegocioPorTipoTransaccion.valor; }
            set { this.accionDeNegocioPorTipoTransaccion.valor = value ; }
        }

        public int IdAccionDeNegocio
        {
            set { this.accionDeNegocioPorTipoTransaccion.id_accion_de_negocio = value; }
            get { return this.accionDeNegocioPorTipoTransaccion.id_accion_de_negocio; }
        }

        public TipoDeTransaccion TipoDeTransaccion()
        {
            return new TipoDeTransaccion(accionDeNegocioPorTipoTransaccion.Tipo_transaccion);
        }

        public AccionDeNegocio AccionDeNegocio()
        {
            return new AccionDeNegocio(accionDeNegocioPorTipoTransaccion.Accion_de_negocio);
        }

        public static List<AccionDeNegocioPorTipoTransaccion> Convert(List<Accion_de_negocio_por_tipo_transaccion> accionesDeNegocioPorTipoTransaccionDB)
        {
            List<AccionDeNegocioPorTipoTransaccion> accionesDeNegocioPorTipoTransaccion = new List<AccionDeNegocioPorTipoTransaccion>();
            foreach (var item in accionesDeNegocioPorTipoTransaccionDB)
            {
                accionesDeNegocioPorTipoTransaccion.Add(new AccionDeNegocioPorTipoTransaccion(item));
            }
            return accionesDeNegocioPorTipoTransaccion;
        }


    }
}

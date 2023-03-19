using System.Collections.Generic;
using System.Linq;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class AccionDeNegocio
    {
        private readonly Accion_de_negocio accionDeNegocio;

        public AccionDeNegocio()
        {

        }

        public AccionDeNegocio(int id, string nombre, string descripcion)
        {
            this.accionDeNegocio = new Accion_de_negocio();
            this.Id = id;
            this.Nombre = nombre;
            this.Descripcion = descripcion;
        }

        public AccionDeNegocio(Accion_de_negocio accionDeNegocio)
        {
            this.accionDeNegocio = accionDeNegocio;
        }

        public int Id
        {
            get { return this.accionDeNegocio.id; }
            set { this.accionDeNegocio.id = value; }
        }

        public string Nombre
        {
            get { return this.accionDeNegocio.nombre; }
            set { this.accionDeNegocio.nombre = value; }
        }

        public string Descripcion
        {
            get { return this.accionDeNegocio.descripcion; }
            set { this.accionDeNegocio.descripcion = value; }
        }
        
        public List<AccionDeNegocioPorTipoTransaccion> ListAccionesDeNegocioPorTipoTransaccion()
        {
                return AccionDeNegocioPorTipoTransaccion.Convert(this.accionDeNegocio.Accion_de_negocio_por_tipo_transaccion.ToList());
        }
 
        public static List<AccionDeNegocio> Convert(List<Accion_de_negocio> accionDenegocios)
        {
            var accionDeNegocios = new List<AccionDeNegocio>();
            foreach (var item in accionDenegocios)
            {
                accionDeNegocios.Add(new AccionDeNegocio(item));
            }
            return accionDeNegocios;
        }
    }
}

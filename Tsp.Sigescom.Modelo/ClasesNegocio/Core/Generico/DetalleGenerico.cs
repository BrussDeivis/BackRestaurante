using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades
{
    public class DetalleGenerico
    {

        private Detalle_maestro detalle;
        public DetalleGenerico()
        {

        }


        public DetalleGenerico(Detalle_maestro detalle)
        {
            this.detalle = detalle;
        }

        public DetalleGenerico(int id, string nombre)
        {
            this.detalle = new Detalle_maestro
            {
                id = id,
                nombre = nombre
            };
        }

        public int Id
        {
            get
            {
                return this.detalle.id;
            }
        }


        public string Nombre
        {
            get
            {
                return this.detalle.nombre;
            }
        }

        public string Codigo
        {
            get
            {
                return this.detalle.codigo;
            }
        }
        public string Valor
        {
            get
            {
                return this.detalle.valor;
            }
        }
        public List<DetalleGenerico> obtenerSubDetalles()
        {
            return DetalleGenerico.convert(this.detalle.Detalle_detalle_maestro.Select(ddm => ddm.Detalle_maestro1).ToList());
        }

        public static List<DetalleGenerico> convert(List<Detalle_maestro> detalles)
        {
            List<DetalleGenerico> detallesGenericos = new List<DetalleGenerico>();
            foreach (var detalle in detalles)
            {
                detallesGenericos.Add(new DetalleGenerico(detalle));
            }
            return detallesGenericos;
        }
    }
}

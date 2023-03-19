using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class Caracteristica //Refiere a la caracteristica que tenga el conceptopulsar 200
    {
        public int IdDetalleMaestro { get; set; }
        public string Nombre { get; set; }//Nombre de la caracteriistica ejm: Marca
        public string Codigo { get; set; }//Codigo del la caracteristica ejm: MRC
        public string Valor { get; set; }//Valor de la caracteristtica ejm: MRC 
        public int IdMaestro { get; set; }

        public Caracteristica()
        {
        }


        public Caracteristica(Detalle_maestro detalleMaestro)
        {
            this.IdDetalleMaestro = detalleMaestro.id;
            this.Nombre = detalleMaestro.nombre;
            this.Valor = detalleMaestro.valor;
            this.IdMaestro = detalleMaestro.id_maestro;
        }

        public static List<Caracteristica> Convert(IList<Detalle_maestro> detallesMaestros)
        {
            List<Caracteristica> caracteristicas = new List<Caracteristica>();
            foreach (var detalleMaestro in detallesMaestros)
            {
                caracteristicas.Add(new Caracteristica(detalleMaestro));
            }
            return caracteristicas;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Custom;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class GrupoClientes
    {
        public int Id { get; set; }
        public int IdActor { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public ItemGenericoBase Tipo { get; set; }
        public ItemGenericoBase Clasificacion { get; set; }
        public ActorComercial_ Responsable { get; set; }
        public List<MiembroGrupoClientes> Clientes { get; set; }

        public GrupoClientes()
        {

        }
    }
}

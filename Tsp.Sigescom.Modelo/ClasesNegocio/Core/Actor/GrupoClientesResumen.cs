using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class GrupoClientesResumen
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string Clasificacion { get; set; }
        public string DocumentoResponsable { get; set; }
        public string NombreResponsable { get; set; }
        public string TelefonoResponsable { get; set; }
        public string CorreoResponsable { get; set; }
        public int NumeroClientes { get => Clientes.Where(c => c.EsVigente).Count(); }
        public List<MiembroGrupoClientes> Clientes { get; set; }
        public bool EsVigente { get; set; }

        public GrupoClientesResumen()
        {

        } 
    }
}

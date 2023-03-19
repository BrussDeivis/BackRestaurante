using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.Core.Finanza.PlainModel
{
    public class OperacionFamiliaGrupo
    {
        public int IdTipoTransaccion { get; set; }
        public int IdFamilia { get; set; }
        public string NombreFamilia { get; set; }
        public int IdGrupo { get; set; }
        public string NombreGrupo { get; set; }
        public decimal Importe { get; set; }
        public decimal ImporteTotal { get => Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasDondeIngresaDinero_.Contains(IdTipoTransaccion)? Importe * 1 : Importe* -1; }

        public List<OperacionFamiliaGrupo> Convert()
        {
            return new List<OperacionFamiliaGrupo>();
        }

    }

}

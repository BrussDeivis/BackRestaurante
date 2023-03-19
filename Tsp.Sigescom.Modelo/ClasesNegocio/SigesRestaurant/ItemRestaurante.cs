using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo.SigesRestaurant
{
    public class ItemRestaurante
    {
        public int Id { get; set; }
        public string CodigoBarra { get; set; }
        public int IdFamilia { get; set; }
        public decimal Precio { get; set; }
        public string Nombre { get; set; }
        public string NombreCompleto { get => CodigoBarra == null ? Nombre : (CodigoBarra + "|" + Nombre); }
        public string NombreFamilia { get; set; }
        public List<Complemento> ComplementosDeFamilia { get; set; }

        public ItemRestaurante(Concepto_negocio conceptoNegocio)
        {
            this.Id = conceptoNegocio.id;
            this.CodigoBarra = conceptoNegocio.codigo_barra;
            this.IdFamilia = conceptoNegocio.id_concepto_basico;
            this.Nombre = conceptoNegocio.nombre;
            this.NombreFamilia = conceptoNegocio.Detalle_maestro4.nombre;
        }
        public ItemRestaurante()
        {

        }

    }
}
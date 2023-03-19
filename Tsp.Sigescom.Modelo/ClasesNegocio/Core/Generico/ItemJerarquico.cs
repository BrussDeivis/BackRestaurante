using System;
using System.Collections.Generic;
using Tsp.Sigescom.Config;
using System.Linq;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class ItemJerarquico
    {
        public int Id { get; set; }
        public int IdPadre { get; set; }
        public string Nombre { get; set; }

        public ItemJerarquico()
        {

        }

        public ItemJerarquico(Detalle_detalle_maestro detalleDetalleMaestro)
        {
            Id = detalleDetalleMaestro.id_detalle_maestro_secundario;
            IdPadre = detalleDetalleMaestro.id_detalle_maestro_principal;
            Nombre = detalleDetalleMaestro.Detalle_maestro1.nombre;
        }

        public static List<ItemJerarquico> Convert(List<Detalle_detalle_maestro> detalles_de_detalles)
        {
            List<ItemJerarquico> items = new List<ItemJerarquico>();
            foreach (var detalle in detalles_de_detalles)
            {
                items.Add(new ItemJerarquico(detalle));
            }
            return items;
        }
    }


}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo.SigesRestaurant
{
    public class Orden_Atencion
    {
        public long Id { get; set; }
        public string Codigo { get; set; }
        public IEnumerable<DetalleOrden> DetallesDeOrden { get; set; }
        public decimal ImporteOrden { get; set; }
        public long IdAtencion { get; set; }
        public int IdMesa { get; set; }
        public string NombreMesa { get; set; }
        public int Estado { get; set; }
        public ItemGenerico Mozo { get; set; }
        public DateTime FechaHoraRegistro { get; set; }

        public Mesa Mesa { get; set; }

        public Orden_Atencion()
        { }
        public Orden_Atencion(Transaccion transaccion)
        {
            Data_Mesa dataMesa = JsonConvert.DeserializeObject<Data_Mesa>(transaccion.Actor_negocio4.extension_json);
            Id = transaccion.id;
            Codigo = transaccion.codigo;
            Estado = (int)transaccion.id_estado_actual;
            IdAtencion = (long)transaccion.id_transaccion_padre;
            ImporteOrden = transaccion.importe_total;
            IdMesa = (int)transaccion.id_actor_negocio_interno1;
            NombreMesa = dataMesa.nombre;
            FechaHoraRegistro = transaccion.fecha_inicio;
            Mesa = new Mesa(transaccion.Actor_negocio1);
            DetallesDeOrden = transaccion.Detalle_transaccion.Select(dt => new DetalleOrden()
            {
                Id = dt.id,
                Precio = dt.precio_unitario,
                Cantidad = dt.cantidad,
                Importe = dt.total,
                Estado = (int)dt.indicadorMultiproposito,
                DetalleItemRestauranteJson = dt.detalle,
                NombreItem = dt.Concepto_negocio.nombre,
                IdItem = dt.id_concepto_negocio,
                IdTransaccion = dt.id_transaccion
            });
        }
    }
}

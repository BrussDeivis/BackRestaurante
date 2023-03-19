using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo.SigesRestaurant
{
    public class AtencionRestaurante
    {
        public long Id { get; set; }
        public Mesa Mesa { get; set; }
        public IEnumerable<Orden_Atencion> Ordenes { get; set; }
        public decimal ImporteAtencion { get; set; }
        public int TipoDePago { get; set; }
        public List<DatosVentaIntegrada> Comprobantes { get; set; }
        public int Estado { get; set; }
        public bool EsAtencionConMesa { get; set; }
        public bool EsAtencionPorDelivery { get; set; }
        public string Cliente { get; set; }
        public bool Confirmado { get; set; }
        public bool EstaRegistrado { get => Estado == MaestroSettings.Default.IdDetalleMaestroEstadoRegistrado; }

        public AtencionRestaurante()
        {
            this.Id = 0;
            this.Mesa = null;
            this.Ordenes = new List<Orden_Atencion>();
            this.ImporteAtencion = 0;
            this.TipoDePago = 1;
        }

        public AtencionRestaurante(Mesa mesa)
        {
            this.Id = 0;
            this.Mesa = mesa;
            this.Ordenes = new List<Orden_Atencion>();
            this.ImporteAtencion = 0;
            this.TipoDePago = 1;
        }

        public static AtencionRestaurante Convert(Transaccion transaccion)
        {
            var tipoTransaccionOrden = RestauranteSettings.Default.IdTipoTransaccionOrdenDeRestaurante;

            Data_Mesa dataMesa = JsonConvert.DeserializeObject<Data_Mesa>(transaccion.Actor_negocio4.extension_json);
            return new AtencionRestaurante()
            {
                Id = transaccion.id,
                ImporteAtencion = transaccion.importe_total,
                Mesa = new Mesa()
                {
                    Id = (int)transaccion.id_actor_negocio_interno1,
                    IdAmbiente = (int)transaccion.Actor_negocio4.id_actor_negocio_padre,
                    Nombre = dataMesa.nombre
                },
                TipoDePago = transaccion.enum1,
                Ordenes = transaccion.Transaccion1
                .Where(to => to.id_tipo_transaccion == tipoTransaccionOrden)
                .Select(to => new Orden_Atencion()
                {
                    Id = to.id,
                    Codigo = to.codigo,
                    Estado = (int)to.id_estado_actual,
                    IdAtencion = (long)to.id_transaccion_padre,
                    ImporteOrden = to.importe_total,
                    IdMesa = (int)transaccion.id_actor_negocio_interno1,
                    NombreMesa = dataMesa.nombre,
                    Mozo = new ItemGenerico()
                    {
                        Id = to.id_actor_negocio_interno,
                        Nombre = to.Actor_negocio2.Actor.tercer_nombre,
                    },
                    FechaHoraRegistro = to.fecha_inicio,
                    DetallesDeOrden = to.Detalle_transaccion.Select(dt => new DetalleOrden()
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
                    })
                }),
                Estado = (int)transaccion.id_estado_actual,
            };
        }
    }

}

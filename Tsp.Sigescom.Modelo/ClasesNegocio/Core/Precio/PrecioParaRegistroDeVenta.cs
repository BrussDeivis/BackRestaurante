using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class PrecioParaRegistroDeVenta
    {
        public int Id { get; set; }
        public string Tarifa { get; set; }
        public int IdTarifa { get; set; }
        public decimal Valor { get; set; }
        public string Codigo { get; set; }

        public PrecioParaRegistroDeVenta()
        {

        }


        public PrecioParaRegistroDeVenta(Precio precio)
        {
            this.Id = precio.id;
            this.Tarifa = precio.Detalle_maestro3.nombre;
            this.Valor = precio.valor;
            this.IdTarifa = precio.id_tarifa_d;
            this.Codigo = precio.Detalle_maestro3.codigo;
        }


        public PrecioParaRegistroDeVenta(Precio_Concepto_Negocio_Comercial precio)
        {
            this.Id = precio.Id;
            this.Tarifa = precio.Tarifa;
            this.Valor = precio.Valor;
            this.IdTarifa = precio.IdTarifa;
            this.Codigo = precio.Codigo;
        }

        internal static List<PrecioParaRegistroDeVenta> Convert(List<Precio> precios)
        {
            List<PrecioParaRegistroDeVenta> resultado = new List<PrecioParaRegistroDeVenta>();
            foreach (var precio in precios)
            {
                resultado.Add(new PrecioParaRegistroDeVenta(precio));
            }
            return resultado;
        }

        internal static List<PrecioParaRegistroDeVenta> Convert(List<Precio_Concepto_Negocio_Comercial> precios)
        {
            List<PrecioParaRegistroDeVenta> resultado = new List<PrecioParaRegistroDeVenta>();
            foreach (var precio in precios)
            {
                resultado.Add(new PrecioParaRegistroDeVenta(precio));
            }
            return resultado;
        }

        internal static List<Precio> Convert(List<PrecioParaRegistroDeVenta> precios)
        {

            List<Precio> resultado = new List<Precio>();
            Precio precio;
            foreach (var preciovm in precios)
            {
                precio = new Precio();
                precio.id_tarifa_d = preciovm.IdTarifa;
                precio.valor = preciovm.Valor;
                resultado.Add(precio);
            }
            return resultado;
        }

    }
}

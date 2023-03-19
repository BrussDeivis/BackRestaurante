using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.Entidades
{
    public partial class Valor_detalle_maestro_detalle_transaccion
    {
        public Valor_detalle_maestro_detalle_transaccion()
        {
        }

        public Valor_detalle_maestro_detalle_transaccion(int id, int numero, int id_detalle_maestro, int? id_valor_detalle_maestro, string valor)
        {
            this.id = id;
            this.numero = numero;
            this.id_detalle_maestro = id_detalle_maestro;
            this.id_valor_detalle_maestro = id_valor_detalle_maestro;
            this.valor = valor;
        }

        public Valor_detalle_maestro_detalle_transaccion(int numero, int id_detalle_maestro, int? id_valor_detalle_maestro, string valor)
        {
            this.numero = numero;
            this.id_detalle_maestro = id_detalle_maestro;
            this.id_valor_detalle_maestro = id_valor_detalle_maestro;
            this.valor = valor;
        }

        public Valor_detalle_maestro_detalle_transaccion Clone()
        {
            return new Valor_detalle_maestro_detalle_transaccion( this.numero, this.id_detalle_maestro, this.id_valor_detalle_maestro, this.valor);
        }

        public static List<Valor_detalle_maestro_detalle_transaccion> Clone(List<Valor_detalle_maestro_detalle_transaccion> toClone)
        {
            List<Valor_detalle_maestro_detalle_transaccion> cloned = new List<Valor_detalle_maestro_detalle_transaccion>();
            foreach (var item in toClone)
            {
                cloned.Add(item.Clone());
            }
            return cloned;
        }

        public static List<Valor_detalle_maestro_detalle_transaccion> Convert_(List<ValorDetalleMaestroDetalleTransaccion> valores)
        {
            List<Valor_detalle_maestro_detalle_transaccion> _valores = new List<Valor_detalle_maestro_detalle_transaccion>();
            foreach (var valor in valores)
            {
                _valores.Add(new Valor_detalle_maestro_detalle_transaccion(valor.Id,valor.Numero,valor.IdDetalleMaestro,null,valor.Valor));
            }
            return _valores;
        }

    }
}

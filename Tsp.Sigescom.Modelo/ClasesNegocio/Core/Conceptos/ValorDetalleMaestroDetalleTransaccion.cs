using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    //Son para obtener la caracteristicas propias del concepto de negocio 
    public class ValorDetalleMaestroDetalleTransaccion
    {
        public int Id { get; set; }
        public int IdDetalleMaestro { get; set; }
        public string Nombre { get; set; }
        public int Numero { get; set; }
        public string Valor { get; set; }
        public long IdDetalleTransaccion { get; set; }

        public ValorDetalleMaestroDetalleTransaccion()
        {
        }

        public ValorDetalleMaestroDetalleTransaccion(Valor_detalle_maestro_detalle_transaccion valor)
        {
            this.Id = valor.id;
            this.IdDetalleMaestro = valor.id_detalle_maestro;
            this.Nombre = valor.Detalle_maestro.nombre;
            this.Numero = valor.numero;
            this.Valor = valor.valor;
            this.IdDetalleTransaccion = valor.id_detalle_transaccion;
        }

        public ValorDetalleMaestroDetalleTransaccion(int id, int idDetalleMaestro, int numero, string valor)
        {
            this.Id = id;
            this.IdDetalleMaestro = idDetalleMaestro;
            this.Numero = numero;
            this.Valor = valor;
        }

        public ValorDetalleMaestroDetalleTransaccion(int id, int idDetalleMaestro, int numero, string valor, long idDetalleTransaccion)
        {
            this.Id = id;
            this.IdDetalleMaestro = idDetalleMaestro;
            this.Numero = numero;
            this.Valor = valor;
            this.IdDetalleTransaccion = idDetalleTransaccion;
        }

        public static List<ValorDetalleMaestroDetalleTransaccion> Convert_(List<Valor_detalle_maestro_detalle_transaccion> _valores)
        {
            List<ValorDetalleMaestroDetalleTransaccion> valores = new List<ValorDetalleMaestroDetalleTransaccion>();
            foreach (var valor in _valores)
            {
                valores.Add(new ValorDetalleMaestroDetalleTransaccion(valor));
            }
            return valores;
        }
    
    }
}

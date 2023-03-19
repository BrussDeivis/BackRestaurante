using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico
{
    public class ItemEstado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public string FechaString { get => Fecha.ToString("dd/MM/yyyy hh:mm tt"); }
        public string Observacion { get; set; }
        public string Valor { get => Id == 0 ? "default" : DiccionarioHotel.ValorEstado.Single(ve => ve.Key == Id).Value; }

        public ItemEstado() { }
        public ItemEstado(Estado_transaccion estado_Transaccion) 
        {
            Id = estado_Transaccion.id_estado;
            Nombre = estado_Transaccion.Detalle_maestro.nombre;
            Fecha = estado_Transaccion.fecha;
            Observacion = estado_Transaccion.comentario;
        }
        public ItemEstado(Evento_transaccion evento_transaccion)
        {
            Id = evento_transaccion.id_evento;
            Nombre = evento_transaccion.Detalle_maestro.nombre;
            Fecha = evento_transaccion.fecha;
            Observacion = evento_transaccion.comentario;
        }
    }
}

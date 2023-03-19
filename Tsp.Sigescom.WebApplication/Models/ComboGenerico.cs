using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class ComboGenerico
    {
        [DataMember]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public int Longitud { get; set; }
        public ComboGenerico()
        {

        }
        public ComboGenerico(int id, string nombre, string codigo)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Codigo = codigo;
        }
        public ComboGenerico(int id, string nombre)
        {
            this.Id = id;
            this.Nombre = nombre;
        }

        public ComboGenerico(int id, string nombre, int longitud)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Longitud = longitud;
        }
        public List<ComboGenerico> Convert(List<Detalle_maestro> list)
        {
            List<ComboGenerico> respuesta = new List<ComboGenerico>();
            for (int i = 0; i < list.Count; i++)
            {
                Detalle_maestro t = list[i];
                ComboGenerico c = new ComboGenerico(t.id, t.nombre, t.codigo);
                respuesta.Add(c);
            }
            return respuesta;
        }

        //public List<ComboGenerico> Convert(List<Modelo.Entidades.Modelo> list)
        //{
        //    List<ComboGenerico> respuesta = new List<ComboGenerico>();
        //    for (int i = 0; i < list.Count; i++)
        //    {
        //        Modelo.Entidades.Modelo t = list[i];
        //        //ComboGenerico c = new ComboGenerico(t.Id(), t.NombreConceptoBasico());
        //        //respuesta.Add(c);
        //    }
        //    return respuesta;
        //}
    }
}
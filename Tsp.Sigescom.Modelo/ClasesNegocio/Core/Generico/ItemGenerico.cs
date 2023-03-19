using System;
using System.Collections.Generic;
using Tsp.Sigescom.Config;
using System.Linq;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico
{
    public class ItemGenerico
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Valor { get; set; }

        public ItemGenerico()
        {

        }

        public ItemGenerico(int id)
        {
            this.Id = id;
        }

        public ItemGenerico(int id, string nombre)
        {
            this.Id = id;
            this.Nombre = nombre;
        }

        public ItemGenerico(int id, string codigo, string nombre)
        {
            this.Id = id;
            this.Codigo = codigo;
            this.Nombre = nombre;
        }

        public ItemGenerico(Detalle_maestro detalleMaestro)
        {
            this.Id = detalleMaestro.id;
            this.Codigo = detalleMaestro.codigo;
            this.Nombre = detalleMaestro.nombre;
            this.Valor = detalleMaestro.valor;
        }
        public Detalle_maestro Convert()
        {
            return new Detalle_maestro()
            {
                id = this.Id,
                codigo = this.Codigo,
                nombre = this.Nombre,
                valor = this.Valor,
            };
        }


        public ItemGenericoConSubItems Convert(ItemGenerico subItem)
        {
            return new ItemGenericoConSubItems()
            {
                Id = this.Id,
                Nombre = this.Nombre,
                Codigo = this.Codigo,
                Valor = this.Valor,
                SubItems = new List<ItemGenerico> { subItem }
            };
        }
        public static ItemGenerico Convert(Detalle_maestro detalleMaestro)
        {
            return new ItemGenerico()
            {
                Id = detalleMaestro.id,
                Codigo = detalleMaestro.codigo,
                Nombre = detalleMaestro.nombre,
                Valor = detalleMaestro.valor
            };
        }
        public static List<ItemGenerico> Convert(List<Clase_actor> clasesDeActor)
        {
            List<ItemGenerico> items = new List<ItemGenerico>();
            foreach (var claseActor in clasesDeActor)
            {
                items.Add(new ItemGenerico() { Id = claseActor.id, Nombre = claseActor.nombre });
            }
            return items;
        }
        public static List<ItemGenerico> Convert(List<Detalle_maestro> detallesMaestro)
        {
            List<ItemGenerico> items = new List<ItemGenerico>();
            detallesMaestro.ForEach(dm => items.Add(new ItemGenerico() { Id = dm.id, Nombre = dm.nombre, Codigo = dm.codigo }));
            return items;
        }

        public static List<ItemGenerico> ConvertirCentroDeAtencionConEstablecimientoComercial(List<CentroDeAtencionExtendido> centrosDeAtencion)
        {
            List<ItemGenerico> itemsGenericos = new List<ItemGenerico>();
            foreach (var item in centrosDeAtencion)
            {
                itemsGenericos.Add(new ItemGenerico(item.Id, item.EstablecimientoComercial.NombreInterno + " | " + item.Nombre));
            }
            return itemsGenericos;
        }

    }

}

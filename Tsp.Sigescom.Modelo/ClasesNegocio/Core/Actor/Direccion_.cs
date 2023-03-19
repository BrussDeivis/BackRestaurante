using System.Collections.Generic;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class Direccion_
    {
        public int Id { get; set; }
        public ItemGenerico Pais { get; set; }
        public ItemGenerico Ubigeo { get; set; }
        public string Residencia { get => (Pais == null ? "" : Pais.Nombre) + (Ubigeo.Id != ActorSettings.Default.idUbigeoNoEspecificado ? (" - " + (Ubigeo.Nombre != null ? Ubigeo.Nombre.Split('-')[0] : "")) : ""); }
        public string Detalle { get; set; }
        public string Texto { get { return Detalle + " - " + Ubigeo.Nombre; } }

        public Direccion Convert()
        {
            return new Direccion()
            {
                id = Id,
                id_tipo_direccion = MaestroSettings.Default.IdDetalleMaestroTipoDireccionDomicilioFiscal,
                es_activo = true,
                es_principal = true,
                id_nacion = Pais == null ? MaestroSettings.Default.IdDetalleMaestroNacionPeru : Pais.Id,
                id_tipo_via = null,
                id_tipo_zona = null,
                id_ubigeo = Ubigeo.Id,
                detalle = Detalle
            };
        }

        public Direccion ConvertirConUbigeo()
        {
            return new Direccion()
            {
                id = Id,
                id_tipo_direccion = MaestroSettings.Default.IdDetalleMaestroTipoDireccionDomicilioFiscal,
                es_activo = true,
                es_principal = true,
                id_nacion = MaestroSettings.Default.IdDetalleMaestroNacionPeru,
                id_tipo_via = null,
                id_tipo_zona = null,
                id_ubigeo = Ubigeo.Id,
                detalle = Detalle,
                Ubigeo = new Ubigeo() { id = Ubigeo.Id, descripcion_larga = Ubigeo.Nombre }
            };
        }
        public Direccion_()
        {
        }

        public Direccion_(Direccion direccion)
        {
            Id = direccion.id;
            Pais = new ItemGenerico(direccion.Detalle_maestro1.id, direccion.Detalle_maestro1.nombre);
            Ubigeo = new ItemGenerico(direccion.Ubigeo.id, direccion.Ubigeo.descripcion_corta);
            Detalle = direccion.detalle;
        }

        public static List<Direccion_> Convert(List<Direccion> direcciones)
        {
            List<Direccion_> resultado = new List<Direccion_>();
            foreach (var direccion in direcciones)
            {
                resultado.Add(new Direccion_(direccion));
            }
            return resultado;
        }
    }
}

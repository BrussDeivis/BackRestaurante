using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;

namespace Tsp.Sigescom.Modelo.Custom.SigesParking
{
    public class Vehiculo
    {
        public int Id { get; set; }
        public int IdActor { get; set; }
        public ItemGenerico TipoDeVehiculo { get; set; }
        public string Placa { get; set; }
        public ItemGenerico Marca { get; set; }
        public string Color { get; set; }
        public string NombreCompleto { get { return TipoDeVehiculo.Nombre + " " + Marca.Nombre + " " + Color; } }
        public bool ExoneradoDePagos { get; set; }

        //public Vehiculo(Actor_negocio actorDeNegocio)
        //{
        //    Id = actorDeNegocio.id;
        //    IdActor = actorDeNegocio.id_actor;
        //    TipoDeVehiculo = new ItemGenerico { Id = actorDeNegocio.IdTipoActor, Nombre = actorDeNegocio.PrimerNombre };
        //    Placa = actorDeNegocio.DocumentoIdentidad;
        //    Marca = new ItemGenerico { Id = actorDeNegocio.IdDetalleMultiproposito, Nombre = actorDeNegocio.SegundoNombre };
        //    Color = actorDeNegocio.TercerNombre;
        //    ExoneradoDePagos = actorDeNegocio.indicador1;
        //    //NombreCompleto = TipoDeVehiculo.Nombre + " " + Marca.Nombre + " " + Color;
        //}

 
        public Vehiculo()
        {
        }
    }
}

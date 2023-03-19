using System.Collections.Generic;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel
{
    public class TipoHabitacion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public ItemGenerico AforoNinos { get; set; }//son valores de caracteristicas 
        public ItemGenerico AforoAdultos { get; set; } //son valores de caracteristicas 
        public List<ItemGenerico> Caracteristicas { get; set; }//son valores de caracteristicas 
        public List<Precio_Compra_Venta_Concepto> Precios { get; set; }
        public List<int> IdsValoresCaracteristicas
        {
            get
            {
                List<int> IdsValoresCaracteristicas = new List<int>();
                IdsValoresCaracteristicas.Add(AforoAdultos.Id);
                IdsValoresCaracteristicas.Add(AforoNinos.Id);
                if (Caracteristicas != null)
                {
                    foreach (var caracteristica in Caracteristicas)
                    {
                        IdsValoresCaracteristicas.Add(caracteristica.Id);
                    }
                }
                return IdsValoresCaracteristicas;
            }
            set
            {
            }
        }
        public List<FotoTipoHabitacion> Fotos { get; set; }
        public bool HayFotos { get => Fotos != null; }
        public List<string> FotosEliminadas { get; set; }
        public bool HayFotosEliminadas { get => FotosEliminadas != null; }
        public bool EsVigente { get; set; }
    }
    public class FotoTipoHabitacion
    {
        public string Nombre { get; set; }
        public string Foto { get; set; }

        public FotoTipoHabitacion() { }
    }
}
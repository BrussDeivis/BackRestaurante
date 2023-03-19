using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class ComprobanteDeNegocioBasico_
    {
        public long Id { get; set; }
        public string Numero { get; set; }
        public string Serie { get; set; }
        public int IdTipo { get; set; }





        public ComprobanteDeNegocioBasico_()
        {

        }    
       
    }
    public class ComprobanteDeNegocio_
    { 
        public long Id { get; set; }
        public int Numero { get; set; }
        public int NumeroSerie { get; set; }
        public SerieComprobante_ Serie { get; set; }
        public ItemGenerico Tipo { get; set; }



        public ComprobanteDeNegocio_()
        {

        }

    }
}

namespace Tsp.FacturacionElectronica.Modelo
{
    public class DocumentoImprimible
    {
        
        public string Contenido { get; set; }
        public string Hashcode { get; set; }

        public DocumentoImprimible(string contenido, string hashcode)
        {
            Contenido = contenido;
            Hashcode = hashcode;
        }
    }
}

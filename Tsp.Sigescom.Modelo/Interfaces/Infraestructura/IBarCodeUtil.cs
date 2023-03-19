namespace Tsp.Sigescom.Modelo.Interfaces.Infraestructura
{
    public interface IBarCodeUtil
    {
        byte[] ObtenerCodigoBarras(string code);
        byte[] ObtenerCodigoQR(string code);
    }
}
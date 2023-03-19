using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo.Interfaces.Negocio
{
    public interface IVentaUtilitarioLogica
    {
        bool ObtenerCamposEditablesEnVentas(string mascaraDeFormaDeCalculo, ElementoDeCalculoEnVentasEnum orden);
        bool ObtenerFormasDeCalculosEnVentas(string mascaraDeFormaDeCalculo, ElementoDeCalculoEnVentasEnum orden, ElementoDeCalculoEnVentasEnum valor);
    }
}

using SelectPdf;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo.Interfaces.Infraestructura
{
    public interface IPdfUtil
    {
        PdfDocument ObtenerPdfDocumento(string htmlStringDocumento, FormatoImpresion formato);


    }
}

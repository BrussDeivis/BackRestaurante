using SelectPdf;
using System.Drawing;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Interfaces.Infraestructura;

namespace Tsp.Sigescom.Utilitarios
{
    public class PdfUtil : IPdfUtil
    {
        public PdfDocument ObtenerPdfDocumento(string htmlStringDocumento, FormatoImpresion formato)
        {
            var Renderer = new SelectPdf.HtmlToPdf();
            //Renderer.Options.WebPageFixedSize = true;
            if (formato == FormatoImpresion._80mm)
            {
                Renderer.Options.AutoFitWidth = HtmlToPdfPageFitMode.NoAdjustment;
                Renderer.Options.AutoFitHeight = HtmlToPdfPageFitMode.NoAdjustment;
                Renderer.Options.WebPageWidth = 1024;
                Renderer.Options.WebPageHeight = 0;
                Renderer.Options.WebPageFixedSize = false;
                Renderer.Options.PdfPageSize = PdfPageSize.Custom;
                Renderer.Options.PdfPageCustomSize = new SizeF(204, 756);
            }
            else if (formato == FormatoImpresion.A4)
            {
                Renderer.Options.PdfPageSize = PdfPageSize.A4;
            }
            else if (formato == FormatoImpresion._56mm)
            {
                Renderer.Options.AutoFitWidth = HtmlToPdfPageFitMode.NoAdjustment;
                Renderer.Options.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
                Renderer.Options.WebPageWidth = 661;
                Renderer.Options.WebPageHeight = 0;
                Renderer.Options.WebPageFixedSize = false;
                Renderer.Options.PdfPageSize = PdfPageSize.Custom;
                Renderer.Options.PdfPageCustomSize = new SizeF(159, 756);
            }
            Renderer.Options.MarginBottom = 0;
            Renderer.Options.MarginTop = 0;
            Renderer.Options.MarginLeft = 0;
            Renderer.Options.MarginRight = 0;
            Renderer.Options.DisplayHeader = false;
            Renderer.Options.JpegCompressionEnabled = false;
            return Renderer.ConvertHtmlString(htmlStringDocumento);
        }


    }
}
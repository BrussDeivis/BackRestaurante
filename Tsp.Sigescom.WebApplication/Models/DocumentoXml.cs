using SelectPdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tsp.Sigescom.WebApplication.Models
{
    public class DocumentoXml
    {
        public long Id { get; set; }
        public string NombreDocumento { get; set; }
        public byte[] Documento { get; set; }
    }

    public class DocumentoPdf
    {
        public long Id { get; set; }
        public string NombreDocumento { get; set; }
        public PdfDocument Documento { get; set; }
    }
}
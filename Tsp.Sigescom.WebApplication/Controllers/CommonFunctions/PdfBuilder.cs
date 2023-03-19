using Microsoft.Reporting.WebForms;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.ClasesNegocio.EBookViewModel.Custom;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.ComprobantesModel;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Infraestructura;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.WebApplication.Models;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    public static class PdfBuilder
    {

        public static PdfDocument ObtenerPdfMovimientoDeMercaderia(MovimientoDeAlmacen movimiento, EstablecimientoComercialExtendidoConLogo sede, byte[] qrBytes, FormatoImpresion formato, List<Proveedor> proveedores, List<Detalle_maestro> modalidadesDeTraslado, List<Detalle_maestro> motivosDeTraslado, Controller controller)
        {
            IPdfUtil pdfUtil = Dependencia.Resolve<IPdfUtil>();
            string htmlString = CoreHtmlStringBuilder.ObtenerHtmlString(movimiento, formato, qrBytes, sede, proveedores, modalidadesDeTraslado, motivosDeTraslado, controller);
            return pdfUtil.ObtenerPdfDocumento(htmlString, formato);
        }
    }



}
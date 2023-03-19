using System;
using System.Collections.Generic;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.EBookViewModel.Custom
{
    public class LibroCompras : LibroElectronico
    {
        public LibroCompras()
        {
        }

        public static string ContenidoTXT(List<LibroCompras> logs)
        {
            string lines = "";
            //lines += ContenidoFormatoPLE(logs, "|");
            return lines;
        }
        public static string NombreArchivoTXT(string documentoIdentidadSede, Periodo periodo, ItemGenerico libro, List<LibroCompras> logs)
        {
            return (@"\LE" + documentoIdentidadSede + periodo.nombre + "00" + libro.Codigo + "001" + (logs.Count > 0 ? "1" : "0") + "11.txt");
        }

        public static string NombreArchivoCSV(string documentoIdentidadSede, Periodo periodo, ItemGenerico libro, List<LibroCompras> logs)
        {
            return (@"\LE" + documentoIdentidadSede + periodo.nombre + "00" + libro.Codigo + "001" + (logs.Count > 0 ? "1" : "0") + "11.csv");
        }
        public static string CabeceraArchivoCSV
        {
            get => "Periodo;CUO;N° Correlativo;Fecha de emisión del;Fecha de Vencimiento;Tipo de Comprobante ;Serie del comprobant;Año de emisión de la;Número del comproban;N° final del comprob;Tipo de Documento de;N° de RUC del provee;Apellidos y nombres,;Base imponible de la;Monto del Impuesto G;Base imponible de la;Monto del Impuesto G;Base imponible de la;Monto del Impuesto G;Valor de las adquisi;Monto del Impuesto S;Icbper;Otros conceptos, tri;Importe total de las;Codigo de la moneda;Tipo de cambio (3).;Fecha de emisión del;Tipo de comprobante ;Número de serie del ;Código de la depende;Número del comproban;Fecha de emisión de ;Número de la Constan;Marca del comprobant;Clasificación de los;Identificación del C;Error tipo 1: incons;Error tipo 2: incons;Error tipo 3: incons;Error tipo 4: incons;Indicador de Comprob;Estado que identific";
        }

        public static string ContenidoCSV(List<LibroCompras> logs)
        {
            string lines = "";
            lines += CabeceraArchivoCSV;
            lines += Environment.NewLine;

            //foreach (var log in logs)
            //{
            //    var existeDetailDocumentoIdentidadCliente = log.NumeroDocumentoIdentidadCliente != "";
            //    if (existeDetailDocumentoIdentidadCliente)
            //    {
            //        log.NumeroDocumentoIdentidadCliente = "\t" + log.NumeroDocumentoIdentidadCliente;
            //    }
            //}
            //lines += ContenidoFormatoPLE(logs, ";");
            return lines;
        }
    }
}


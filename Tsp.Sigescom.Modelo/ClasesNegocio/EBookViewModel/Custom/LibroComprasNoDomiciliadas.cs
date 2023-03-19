using System;
using System.Collections.Generic;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.EBookViewModel.Custom
{
    public class LibroComprasNoDomiciliadas : LibroElectronico
    {
        public LibroComprasNoDomiciliadas()
        {
        }

        public static string ContenidoTXT(List<LibroComprasNoDomiciliadas> logs)
        {
            string lines = "";
            //lines += ContenidoFormatoPLE(logs, "|");
            return lines;
        }
        public static string NombreArchivoTXT(string documentoIdentidadSede, Periodo periodo, ItemGenerico libro, List<LibroComprasNoDomiciliadas> logs)
        {
            return (@"\LE" + documentoIdentidadSede + periodo.nombre + "00" + libro.Codigo + "001" + (logs.Count > 0 ? "1" : "0") + "11.txt");
        }

        public static string NombreArchivoCSV(string documentoIdentidadSede, Periodo periodo, ItemGenerico libro, List<LibroComprasNoDomiciliadas> logs)
        {
            return (@"\LE" + documentoIdentidadSede + periodo.nombre + "00" + libro.Codigo + "001" + (logs.Count > 0 ? "1" : "0") + "11.csv");
        }
        public static string ContenidoCSV(List<LibroComprasNoDomiciliadas> logs)
        {
            string lines = "";
            //lines += CabeceraArchivoCSV;
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


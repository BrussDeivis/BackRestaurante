using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Humanizer;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;

namespace Tsp.Sigescom.Modelo
{
    public class ListHelper<T>
    {
        public static bool ContainsAllItems(List<T> a, List<T> b)
        {
            return !b.Except(a).Any();
        }
    }
    public class ExceptionData
    {
        public string Error { get; set; }
        public string StackTrace { get; set; }
        public string InicioError { get; set; }
    }
    public class Util
    {
        public static string APalabras_(decimal numero, string monedaPlural)
        {
            long parteEntera = (long)decimal.Truncate(numero);
            int parteDecimal = (int)(decimal.Round((numero - parteEntera), 2) * 100);
            string cadenaDecimal = parteDecimal > 0 ? $" CON {parteDecimal}/100 " : " CON 00/100 ";
            return (parteEntera.ToWords(CultureInfo.CurrentCulture) + cadenaDecimal + monedaPlural).ToUpper();
        }

        public static string APalabras(decimal numero, string monedaPlural)
        {
            long parteEntera = (long)decimal.Truncate(numero);
            int parteDecimal = (int)(decimal.Round((numero - parteEntera), 2) * 100);
            string cadenaDecimal = parteDecimal > 0 ? $" CON {parteDecimal}/100 " : " CON 00/100 ";
            return (ToText(parteEntera) + cadenaDecimal + monedaPlural).ToUpper();
        }

        private static string ToText(double value)
        {
            string Num2Text = "";
            value = Math.Truncate(value);
            if (value == 0) Num2Text = "CERO";
            else if (value == 1) Num2Text = "UNO";
            else if (value == 2) Num2Text = "DOS";
            else if (value == 3) Num2Text = "TRES";
            else if (value == 4) Num2Text = "CUATRO";
            else if (value == 5) Num2Text = "CINCO";
            else if (value == 6) Num2Text = "SEIS";
            else if (value == 7) Num2Text = "SIETE";
            else if (value == 8) Num2Text = "OCHO";
            else if (value == 9) Num2Text = "NUEVE";
            else if (value == 10) Num2Text = "DIEZ";
            else if (value == 11) Num2Text = "ONCE";
            else if (value == 12) Num2Text = "DOCE";
            else if (value == 13) Num2Text = "TRECE";
            else if (value == 14) Num2Text = "CATORCE";
            else if (value == 15) Num2Text = "QUINCE";
            else if (value < 20) Num2Text = "DIECI" + ToText(value - 10);
            else if (value == 20) Num2Text = "VEINTE";
            else if (value < 30) Num2Text = "VEINTI" + ToText(value - 20);
            else if (value == 30) Num2Text = "TREINTA";
            else if (value == 40) Num2Text = "CUARENTA";
            else if (value == 50) Num2Text = "CINCUENTA";
            else if (value == 60) Num2Text = "SESENTA";
            else if (value == 70) Num2Text = "SETENTA";
            else if (value == 80) Num2Text = "OCHENTA";
            else if (value == 90) Num2Text = "NOVENTA";
            else if (value < 100) Num2Text = ToText(Math.Truncate(value / 10) * 10) + " Y " + ToText(value % 10);
            else if (value == 100) Num2Text = "CIEN";
            else if (value < 200) Num2Text = "CIENTO " + ToText(value - 100);
            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = ToText(Math.Truncate(value / 100)) + "CIENTOS";
            else if (value == 500) Num2Text = "QUINIENTOS";
            else if (value == 700) Num2Text = "SETECIENTOS";
            else if (value == 900) Num2Text = "NOVECIENTOS";
            else if (value < 1000) Num2Text = ToText(Math.Truncate(value / 100) * 100) + " " + ToText(value % 100);
            else if (value == 1000) Num2Text = "MIL";
            else if (value < 2000) Num2Text = "MIL " + ToText(value % 1000);
            else if (value < 1000000)
            {
                Num2Text = ToText(Math.Truncate(value / 1000)) + " MIL";
                if ((value % 1000) > 0) Num2Text = Num2Text + " " + ToText(value % 1000);
            }

            else if (value == 1000000) Num2Text = "UN MILLON";
            else if (value < 2000000) Num2Text = "UN MILLON " + ToText(value % 1000000);
            else if (value < 1000000000000)
            {
                Num2Text = ToText(Math.Truncate(value / 1000000)) + " MILLONES ";
                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + ToText(value - Math.Truncate(value / 1000000) * 1000000);
            }

            else if (value == 1000000000000) Num2Text = "UN BILLON";
            else if (value < 2000000000000) Num2Text = "UN BILLON " + ToText(value - Math.Truncate(value / 1000000000000) * 1000000000000);

            else
            {
                Num2Text = ToText(Math.Truncate(value / 1000000000000)) + " BILLONES";
                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + ToText(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            }
            return Num2Text;

        }
        public static void VerificarError(OperationResult result)
        {
            if (result.code_result != OperationResultEnum.Success)
            {
                throw new Exception(result.message);
            }
        }
        public static void ManageIfResultIsNotSuccess(OperationResult result, string generalErrorMessage)
        {
            if (result.code_result != OperationResultEnum.Success)
            {
                throw new Exception(generalErrorMessage + " " + result.title + " " + result.message + (result.exceptions != null ? GetExceptionsMessage(result.exceptions).Error : ""));
            }
        }
        public static void ManejoEnLogicaResultadoSinExito(OperationResult result, string mensajeErrorLogica)
        {
            if (result.code_result != OperationResultEnum.Success)
            {
                throw new LogicaException(mensajeErrorLogica, new Exception(result.title + " " + result.message + (result.exceptions != null ? GetExceptionsMessage(result.exceptions).Error : "")));
            }
        }
        internal static ExceptionData GetExceptionsMessage(IEnumerable<Exception> exceptions)
        {
            ExceptionData errorData = new ExceptionData();
            foreach (var e in exceptions)
            {
                var currentError = GetExceptionData(e);
                errorData.Error += currentError.Error;
                errorData.StackTrace += currentError.StackTrace;
            }
            return errorData;
        }
        internal static ExceptionData GetException(Exception exception)
        {
            ExceptionData errorData = new ExceptionData
            {
                Error = exception.Message + Environment.NewLine,
                StackTrace = exception.StackTrace + Environment.NewLine
            };
            return errorData;
        }
        internal static ExceptionData GetExceptionData(Exception exception)
        {
            ExceptionData errorData = new ExceptionData();
            var next = exception.InnerException;
            errorData.Error = exception.Message + Environment.NewLine;
            errorData.StackTrace = exception.StackTrace + Environment.NewLine;
            while (next != null)
            {
                errorData.Error += "==============================================" + Environment.NewLine + GetException(next).Error;
                errorData.StackTrace += "==============================================" + Environment.NewLine + GetException(next).StackTrace;
                errorData.InicioError = GetException(next).Error;
                next = next.InnerException;
            }
            return errorData;
        }
        public static Object SuccessJson(string e)
        {
            return new { result_description = e };
        }
        public static Object ErrorJson(Exception e)
        {
            var errorData = GetExceptionData(e);
            return new { error = errorData.Error, stackTrace = errorData.StackTrace };
        }
        public static string SoloErrorString(Exception e)
        {
            var errorData = GetExceptionData(e);
            return errorData.Error;
        }
        public static string InicioErrorString(Exception e)
        {
            var errorData = GetExceptionData(e);
            return errorData.InicioError;
        }
        public static int? ConvertToInt32(int? o)
        {
            int? r = null;
            if (o != null)
                r = Convert.ToInt32(o);
            return r;
        }
        public static int? ConvertToInt32(string o)
        {
            int? r = null;
            if (o != null)
                r = Convert.ToInt32(o.ToString());
            return r;
        }
        public static int? ConvertToInt32(decimal? o)
        {
            int? r = null;
            if (o != null)
                r = Convert.ToInt32(o);
            return r;
        }
    }
}

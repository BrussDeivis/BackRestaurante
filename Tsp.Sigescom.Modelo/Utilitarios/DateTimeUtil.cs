using System;

namespace Tsp.Sigescom.Modelo
{
    public class DateTimeUtil
    {
        #region MANEJO DE FECHAS

        //Metodos para establecer mas precision a las fechas
        public static DateTime ObtenerFechaDesdeConPrecisionDeHorasMinutosMilisegundos(string desde)
        {
            DateTime fechaDesde = DateTime.Parse(desde);
            return fechaDesde;
        }

        public static DateTime ObtenerFechaHastaConPrecisionDeHorasMinutosMilisegundos(string hasta)
        {
            DateTime fechaHasta = DateTime.Parse(hasta + " 23:59:59.999");
            return fechaHasta;
        }

        public static DateTime ObtenerFechaDesdeConPrecisionDeMilisegundos(string fechaInicio)
        {
            DateTime fechaDesde = DateTime.Parse(fechaInicio);
            fechaDesde = fechaDesde.AddMilliseconds(0);
            return fechaDesde;
        }

        public static DateTime ObtenerFechaHastaConPrecisionDeMilisegundos(string fechaFin)
        {
            DateTime fechaHasta = DateTime.Parse(fechaFin);
            fechaHasta = fechaHasta.AddMilliseconds(999);
            return fechaHasta;
        }

        public static DateTime FechaActual()
        {
            var info = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
            DateTimeOffset localServerTime = DateTimeOffset.Now;
            DateTimeOffset localTime = TimeZoneInfo.ConvertTime(localServerTime, info);
            return localTime.DateTime;
        }

        #endregion
    }

    public static class DateTimeJavaScript
    {
        private static readonly long DatetimeMinTimeTicks =
           (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Ticks;

        public static long ToJavaScriptMilliseconds(this DateTime dt)
        {
            return (long)((dt.ToUniversalTime().Ticks - DatetimeMinTimeTicks) / 10000);
        }
    }

 
}

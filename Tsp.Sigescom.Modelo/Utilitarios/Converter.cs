using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;

namespace Tsp.Sigescom.Modelo
{
    public class Convertir
    {
        public static List<PeriodoCochera> Periodos(string separadosPorComa)
        {
            try
            {
                //Periodo[] valores = (Periodo[])(object)(separadosPorComa.Split(',').Select(int.Parse).ToArray());
                IEnumerable<PeriodoCochera> valores = separadosPorComa.Split(',').Select(a => (PeriodoCochera)Enum.Parse(typeof(PeriodoCochera), a));
                return valores.ToList();
            }
            catch (Exception e)
            {
                throw new ModeloException("ERROR convirtiendo valores separados por coma a una lista de Periodo Enum",e);
            }

        }

        public static List<SistemaPagoCochera> SistemasDePagoCochera(string separadosPorComa)
        {
            try
            {
                IEnumerable<SistemaPagoCochera> valores = separadosPorComa.Split(',').Select(a => (SistemaPagoCochera)Enum.Parse(typeof(SistemaPagoCochera), a));

                return valores.ToList();
            }
            catch (Exception e)
            {
                throw new ModeloException("ERROR convirtiendo valores separados por coma a una lista de SistemaPagoCochera Enum", e);
            }

        }
    }

}

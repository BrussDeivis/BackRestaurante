using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.Custom.SigesParking
{
    public class ConfiguracionCochera
    {
        public List<SistemaPagoCochera> SistemasDePagoHabilitados { get; set; }
        public int IdTipoDeTurno { get; set; }
        public int IdConceptoServicioCocheraEnSistemaDePagoPorHora { get; set; }
        public int IdConceptoPerdidaDeTicket { get; set; }
        public int MinutosDeToleranciaEnSistemaDePagoPorHora { get; set; }
        public int MinutosDeToleranciaExcesoEnSistemaDePagoPlanaPorTurnos { get; set; }
        public List<PeriodoCochera> PeriodosHabilitadosEnSistemasDePagoAbonados { get; set; }
    }
}

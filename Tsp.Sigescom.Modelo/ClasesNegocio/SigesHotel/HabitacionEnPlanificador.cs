using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel
{
    public class HabitacionEnPlanificador
    {
        private List<Precio> precios;
        public List<Precio> Precios { set => precios = value; }
        public int Id { get; set; }
        public string Ambiente { get; set; }
        public string TipoHabitacion { get; set; }
        public string CodigoHabitacion { get; set; }
        public decimal PrecioUnitario
        {
            get
            {
                return precios.Count() > 0 ? precios.FirstOrDefault(p => p.id_tarifa_d == MaestroSettings.Default.IdDetalleMaestroTarifaNormal) != null ? precios.FirstOrDefault(p => p.id_tarifa_d == MaestroSettings.Default.IdDetalleMaestroTarifaNormal).valor : precios.First().valor : 0;
            }
        }
        public bool Disponible { get; set; }
        public bool Ocupada { get; set; }
        public bool PorIngresar { get; set; }
        public bool PorSalir { get; set; }
        public bool EnLimpieza { get; set; }
        public List<EstadoHabitacionEnPlanificador> EstadosHabitacion { get; set; }

        public HabitacionEnPlanificador() { }
    }
}

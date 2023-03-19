using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel
{
    public class InventarioSemaforo : InventarioFisico
    {
        public decimal StockMinimo { get; set; }
        public decimal StockMaximo { get => StockMinimo * (1 + (decimal)ConceptoSettings.Default.PorcentajeParaObtenerStockMaximo/100); }
        /// <summary>
        /// 3 niveles: -1 = bajo, 0= normal, 1= alto, 2= no determinado por que no hay stock minimo (es 0)
        /// </summary>
        public NivelStockSemaforoEnum ValorSemaforo { get => StockMinimo == 0 ? NivelStockSemaforoEnum.Indeterminado : Cantidad < StockMinimo ? NivelStockSemaforoEnum.Bajo : Cantidad < StockMaximo ? NivelStockSemaforoEnum.Normal : NivelStockSemaforoEnum.Alto; }

        public int ValorSemaforoInt { get => ((int)ValorSemaforo); }
        
        public InventarioSemaforo()
        {

        }
        new public List<InventarioSemaforo> Convert()
        {
            return new List<InventarioSemaforo>();
        }


    }
}

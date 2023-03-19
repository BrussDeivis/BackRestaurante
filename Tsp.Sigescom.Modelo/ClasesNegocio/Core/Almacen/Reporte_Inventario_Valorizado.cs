using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Tsp.Sigescom.Modelo.Entidades.Custom.Partial
{
    public class Reporte_Inventario_Valorizado
    {

        private int idConceptoNegocio;
        private string producto;
        private decimal cantidad;
        private string lote;
        private Precio precioVigente;

        private IEnumerable<Valor_caracteristica_concepto_negocio> valorCaracteristicasConceptoNegocio;

        public Reporte_Inventario_Valorizado()
        {

        }

        public Reporte_Inventario_Valorizado(int idConceptoNegocio, string producto, decimal cantidad, string lote, decimal costoUnitario,Precio precioVigente, IEnumerable< Valor_caracteristica_concepto_negocio> valorCaracteristicasConceptoNegocio)
        {
            this.idConceptoNegocio = idConceptoNegocio;
            this.producto = producto;
            this.CostoUnitario = costoUnitario;
            this.cantidad = cantidad;
            this.lote = lote;
            this.precioVigente = precioVigente;
            this.valorCaracteristicasConceptoNegocio = valorCaracteristicasConceptoNegocio;
        }

        public int IdConceptoNegocio { get => idConceptoNegocio; set => idConceptoNegocio = value; }
        public string Producto { get => producto; set => producto = value; }
        public decimal Cantidad { get => cantidad; set => cantidad = value; }
        public string Lote { get => lote; set => lote = value; }
        public Precio PrecioVigente { get => precioVigente; set => precioVigente = value; }

        /// <summary>
        /// Precio ultima compra
        /// </summary>
        public decimal CostoUnitario { get; set; }

        /// <summary>
        /// Ultimo precio de venta registrado
        /// </summary>
        public decimal PrecioVenta
        {
            get { return PrecioVigente != null ? PrecioVigente.valor : 0; }
        }

        public decimal CostoTotal {
            get { return Cantidad * CostoUnitario; }
        }
        public decimal ImporteTotal {
            get { return Cantidad * PrecioVenta; }
        }
        public decimal Utilidad
        {
            get { return ImporteTotal - CostoTotal;}
        }

        public IEnumerable<Valor_caracteristica> ValorCaracteristica
        {
            get { return ValorCaracteristicasConceptoNegocio.Select( vc => vc.Valor_caracteristica); }
        }

        public IEnumerable<Valor_caracteristica_concepto_negocio> ValorCaracteristicasConceptoNegocio { get => valorCaracteristicasConceptoNegocio; set => valorCaracteristicasConceptoNegocio = value; }

        public static List<Reporte_Inventario_Valorizado> AgruparPorIdConcpetoNegocio(List<Reporte_Inventario_Valorizado> inventarios)
        {
            List<Reporte_Inventario_Valorizado> resultado = new List<Reporte_Inventario_Valorizado>();
            foreach (var i in inventarios.GroupBy(inventario => inventario.IdConceptoNegocio))
            {
                var cantidad = i.Sum(c => c.Cantidad);
                var precioVigente = i.FirstOrDefault(upv => upv.PrecioVigente != null && upv.PrecioVigente.valor > 0);
                resultado.Add(new Reporte_Inventario_Valorizado(i.First().IdConceptoNegocio, i.First().Producto, cantidad,"", i.Sum(s => s.cantidad)!=0? i.Sum(s => s.CostoTotal) / i.Sum(s => s.cantidad):0, precioVigente != null ? precioVigente.PrecioVigente:null, i.First().ValorCaracteristicasConceptoNegocio  ));
            }

            return resultado;
        }

    }

}
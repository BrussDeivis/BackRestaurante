using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades
{
    /// <summary>
    /// contiene un consolidado de los movimientos de entrada y salida de un concepto de negocio para una entidad innterna (almacén)
    /// </summary>
    public class Movimientos_concepto_negocio_actor_negocio_interno
    {
        public int Id_entidad_interna { get; set; }
        public int Id_concepto_negocio { get; set; }
        public bool EsIngreso { get; set; }
        public decimal Costo_Unitario { get; set; }
        public decimal Total { get; set; }

        public decimal Cantidad_Principal { get; set; }
        public decimal Cantidad_Secundaria { get; set; }
        public DateTime Fecha_inicio { get; set; }
        public string Lote { get; set; }
        public decimal Entradas_principal { get; set; }
        public decimal Salidas_principal { get; set; }
        public decimal Entradas_secundaria { get; set; }
        public decimal Salidas_secundaria { get; set; }

        public Movimientos_concepto_negocio_actor_negocio_interno()
        {

        }

        public Movimientos_concepto_negocio_actor_negocio_interno(int id_entidad_interna, int id_concepto_negocio, decimal entradas_principal, decimal salidas_principal, decimal entradas_secundaria, decimal salidas_secundaria, DateTime fecha_inicio)
        {
            this.Id_entidad_interna = id_entidad_interna;
            this.Id_concepto_negocio = id_concepto_negocio;
            this.Entradas_principal = entradas_principal;
            this.Salidas_principal = salidas_principal;
            this.Entradas_secundaria = entradas_secundaria;
            this.Salidas_secundaria = salidas_secundaria;
            this.Fecha_inicio = fecha_inicio;
        }

        public Movimientos_concepto_negocio_actor_negocio_interno(int id_entidad_interna, int id_concepto_negocio, decimal entradas_principal, decimal salidas_principal, decimal entradas_secundaria, decimal salidas_secundaria, DateTime fecha_inicio, string lote, DateTime fecha_vencimiento)
        {
            this.Id_entidad_interna = id_entidad_interna;
            this.Id_concepto_negocio = id_concepto_negocio;
            this.Entradas_principal = entradas_principal;
            this.Salidas_principal = salidas_principal;
            this.Entradas_secundaria = entradas_secundaria;
            this.Salidas_secundaria = salidas_secundaria;
            this.Fecha_inicio = fecha_inicio;
            this.Lote = lote;
        }

      

    }
}

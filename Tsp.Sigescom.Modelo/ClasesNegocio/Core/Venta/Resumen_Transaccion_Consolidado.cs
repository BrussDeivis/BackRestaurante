using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom.Partial
{
    public class Resumen_Transaccion_Consolidado
    {
        public int IdTipoTransaccion;
        public string NombreTransaccion;
        public int IdEntidad;
        public string EntidadInterna;
        public decimal Importe;
        public bool EntradaSalida;

        //public decimal TotalDeGrupo;             
    }

    public class Resumen_Detalles_Consolidado_Por_Vendedor
    {
        public int IdTipoTransaccion { get; set; }
        public int IdEmpleado { get; set; }
        public string Empleado { get; set; }
        public int IdConceptoBasico { get; set; }
        public string ConceptoBasico { get; set; }
        public int IdConceptoNegocio { get; set; }
        public string CodigoBarra { get; set; }
        public string ConceptoNegocio { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario
        {
            get
            { return Cantidad != 0 ? Importe / Cantidad : 0; }
        }
        public decimal Importe { get; set; }
        /// <summary>
        /// De acuerdo al tipo de transaccion de determina si es una transaccion de ingreso de dinero o de salida de dinero, ya de acuerdo a eso es la cantidad de ingreso o de salida
        /// </summary>
        public decimal CantidadTotal
        {
            get
            { return Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasDondeIngresaDinero_.Contains(IdTipoTransaccion) ? Cantidad * 1 : Cantidad * -1; }
        }
        /// <summary>
        /// De acuerdo al tipo de transaccion de determina si es una transaccion de ingreso de dinero o de salida de dinero, ya de acuerdo a eso es el importe si es de ingrreso o de salida 
        /// </summary>
        public decimal ImporteTotal
        {
            get
            { return Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasDondeIngresaDinero_.Contains(IdTipoTransaccion) ? Importe * 1 : Importe * -1; }
        }


        public Resumen_Detalles_Consolidado_Por_Vendedor()
        { }

        public static List<Resumen_Detalles_Consolidado_Por_Vendedor> Convert()
        {
            return new List<Resumen_Detalles_Consolidado_Por_Vendedor>();
        }


    }

    public class Resumen_Por_Concepto_Por_Vendedor_Contado_Credito
    {
        public int IdTipoTransaccion { get; set; }
        public int IdEmpleado { get; set; }
        public string Empleado { get; set; }
        public int IdConceptoNegocio { get; set; }
        public string CodigoBarra { get; set; }
        public string ConceptoNegocio { get; set; }

        public decimal ImporteContado { get; set; }
        public decimal ImporteCredito { get; set; }
        public decimal CantidadContado { get; set; }
        public decimal CantidadCredito { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Importe { get; set; }
        public bool EsContado { get; set; }
        public decimal PrecioUnitarioContado
        {
            get
            { return CantidadContado != 0 ? ImporteContado / CantidadContado : 0; }
        }
        public decimal PrecioUnitarioCredito
        {
            get
            { return CantidadCredito != 0 ? ImporteCredito / CantidadCredito : 0; }
        }
        public decimal CantidadConSigno
        {
            get
            { return Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasDondeIngresaDinero_.Contains(IdTipoTransaccion) ? Cantidad * 1 : Cantidad * -1; }
        }
         
        public decimal ImporteConSigno
        {
            get
            { return Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasDondeIngresaDinero_.Contains(IdTipoTransaccion) ? Importe * 1 : Importe * -1; }
        }
        
        //public decimal CantidadContado
        //{
        //    get
        //    { return EsContado ? Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasDondeIngresaDinero_.Contains(IdTipoTransaccion) ? Cantidad * 1 : Cantidad * -1 : 0; }
        //}
        //public decimal CantidadCredito
        //{
        //    get
        //    { return EsContado ? 0 : Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasDondeIngresaDinero_.Contains(IdTipoTransaccion) ? Cantidad * 1 : Cantidad * -1; }
        //}
        //public decimal ImporteContado
        //{
        //    get
        //    { return EsContado ? Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasDondeIngresaDinero_.Contains(IdTipoTransaccion) ? Importe * 1 : Importe * -1 : 0; }
        //}
        //public decimal ImporteCredito
        //{
        //    get
        //    { return EsContado ? 0 : Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasDondeIngresaDinero_.Contains(IdTipoTransaccion) ? Importe * 1 : Importe * -1; }
        //}
        //public decimal PrecioUnitarioContadoTotal
        //{
        //    get
        //    { return CantidadContado != 0 ? ImporteContado / CantidadContado : 0; }
        //}
        //public decimal PrecioUnitarioCreditoTotal
        //{
        //    get
        //    { return CantidadCredito != 0 ? ImporteCredito / CantidadCredito : 0; }
        //}
        public Resumen_Por_Concepto_Por_Vendedor_Contado_Credito()
        { }

        public static List<Resumen_Por_Concepto_Por_Vendedor_Contado_Credito> Convert()
        {
            return new List<Resumen_Por_Concepto_Por_Vendedor_Contado_Credito>();
        }

    }

    
}

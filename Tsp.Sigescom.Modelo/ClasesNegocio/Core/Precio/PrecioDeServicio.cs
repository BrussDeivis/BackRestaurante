using System;
using System.Collections.Generic;
using System.Linq;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class PrecioDeServicio
    {
        private Precio precio;


        public PrecioDeServicio(Precio precio) 
        {
            this.precio = precio;
        }

        public PrecioDeServicio()
        {

        }
        //public PrecioDeServicio(int idProveedor, int idUnidadNegocio, int idConceptoNegocio, int idMoneda, decimal monto, bool indicadorTarifaFija,bool esVigente)
        //{
        //    precio = new Precio(idProveedor, idUnidadNegocio, idConceptoNegocio, monto, ConceptoSettings.Default.idTarifaGenerica, idMoneda, indicadorTarifaFija,esVigente);
        //}
        //public PrecioDeServicio(int idProveedor, int idUnidadNegocio, int idConceptoNegocio, int idMoneda, decimal monto, bool indicadorTarifaFija)
        //{
        //    precio = new Precio(idProveedor,  idUnidadNegocio, idConceptoNegocio, monto, ConceptoSettings.Default.idTarifaGenerica, idMoneda, indicadorTarifaFija);
        //}
        //public PrecioDeServicio(int idUnidadNegocio, int idConceptoNegocio, int idMoneda, decimal monto, bool indicadorTarifaFija)
        //{
        //    precio = new Precio(idUnidadNegocio, idConceptoNegocio, monto, ConceptoSettings.Default.idTarifaGenerica, idMoneda, indicadorTarifaFija);
        //}
        //public PrecioDeServicio(int id,int idProveedor, int idUnidadNegocio, int idConceptoNegocio, int idMoneda, decimal monto, bool indicadorTarifaFija)
        //{
        //    precio = new Precio(id,idProveedor, idUnidadNegocio, idConceptoNegocio, monto, ConceptoSettings.Default.idTarifaGenerica, idMoneda, indicadorTarifaFija);
        //}

        //public PrecioDeServicio(int idProveedor, int idUnidadNegocio, int idConceptoNegocio,int idTarifa, int idMoneda, decimal monto,DateTime fechaInicio,DateTime fechaFin,DateTime fechaModificacion, bool indicadorMultiproposito)
        //{   
        //    precio = new Precio(idProveedor, idUnidadNegocio, idConceptoNegocio, monto, idTarifa, idMoneda,fechaInicio,fechaFin,fechaModificacion, indicadorMultiproposito);
        //}


        public int Id
        {
            get { return this.precio.id; }
        }

        public Precio Precio
        {
            get { return this.precio; }
        }

        public ConceptoDeServicio Concepto()
        {
            return new ConceptoDeServicio(this.precio.Concepto_negocio);
        }


        public Proveedor Proveedor()
        {
            return  new Proveedor(this.precio.Actor_negocio);
        }

        public bool EsFrecuente
        {
            get { return this.precio.indicador_multiproposito; }
        }

        public int IdProveedor
        {
            get { return this.precio.id_actor_negocio; }
        }

        public int IdConceptoDeNegocio
        {
            get { return this.precio.id_concepto_negocio; }
        }

        public int IdUnidadNegocio
        {
            get { return this.precio.id_unidad_negocio; }
       }

        //public string UnidadNegocio
        //{
        //    get { return this.precio.Detalle_maestro.codigo; }
        //}
        public DetalleGenerico UnidadNegocio()
        {
            return new DetalleGenerico(this.precio.Detalle_maestro);
        }
        public DetalleGenerico Moneda()
        {
            return new DetalleGenerico(this.precio.Detalle_maestro1);
        }
        public int IdMoneda
        {
            get { return this.precio.id_moneda; }
        }

        //public string CodigoMoneda
        //{
        //    get { return this.precio.Detalle_maestro1.codigo; }
        //}

        public decimal Importe
        {
            get { return this.precio.valor; }
        }

        public static List<PrecioDeServicio> convert(List<Precio> precios)
        {
            var preciosDeServicios = new List<PrecioDeServicio>();

            foreach (var precio in precios)
            {
                preciosDeServicios.Add(new PrecioDeServicio(precio));
            }
            return preciosDeServicios;
        }

        
    }
    
}

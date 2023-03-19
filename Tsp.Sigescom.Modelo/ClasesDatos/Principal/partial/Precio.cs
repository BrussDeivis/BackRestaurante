using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;

namespace Tsp.Sigescom.Modelo.Entidades
{
    public partial class Precio
    {
        //para precio y descuento
        public Precio(int id, int idActorNegocio, int idUnidadNegocio, int idConceptoNegocio, decimal importe, int idTarifa, int idMoneda,
            DateTime fechaInicio, DateTime fechaFin, DateTime fechaModificacion, bool indicadorMultiproposito, bool esVigente, bool esPorcentaje,
            int cantidadMinima, int cantidadMaxima, int idTipo, string descripcion, int idResponsable)
        {
            setData(id, idActorNegocio, idUnidadNegocio, idConceptoNegocio, importe, idTarifa, idMoneda, fechaInicio, fechaFin, fechaModificacion,
                indicadorMultiproposito, esVigente, esPorcentaje, cantidadMinima, cantidadMaxima, idTipo, descripcion, idResponsable);
            validarId(idActorNegocio, idUnidadNegocio, idConceptoNegocio, idTarifa, idMoneda, idTipo, idResponsable);
        }

        //para precio y descuento
        public Precio(int idActorNegocio, int idUnidadNegocio, int idConceptoNegocio, decimal importe, int idTarifa, int idMoneda,
            DateTime fechaInicio, DateTime fechaFin, DateTime fechaModificacion, bool indicadorMultiproposito, bool esVigente,bool esPorcentaje,
            int cantidadMinima,int cantidadMaxima,int idTipo, string descripcion, int idResponsable)
        {
            setData(idActorNegocio, idUnidadNegocio, idConceptoNegocio, importe, idTarifa, idMoneda, fechaInicio, fechaFin, fechaModificacion,
                indicadorMultiproposito, esVigente, esPorcentaje, cantidadMinima, cantidadMaxima, idTipo, descripcion, idResponsable);
            //validarId(idActorNegocio, idUnidadNegocio, idConceptoNegocio, idTarifa, idMoneda, idTipo, idResponsable);
        }

        //para bonificacion
        public Precio(int id, int idActorNegocio, int idUnidadNegocio, int idConceptoNegocio, decimal importe, int idTarifa, int idMoneda,
            DateTime fechaInicio, DateTime fechaFin, DateTime fechaModificacion, bool indicadorMultiproposito, bool esVigente, bool esPorcentaje,
            int cantidadMinima, int cantidadMaxima, int idTipo, int idConceptoNegocioReferencial, string descripcion, int idResponsable)
        {
            setData(id, idActorNegocio, idUnidadNegocio, idConceptoNegocio, importe, idTarifa, idMoneda, fechaInicio, fechaFin, fechaModificacion,
                indicadorMultiproposito, esVigente, esPorcentaje, cantidadMinima, cantidadMaxima, idTipo, idConceptoNegocioReferencial, descripcion, idResponsable);
            validarId(idActorNegocio, idUnidadNegocio, idConceptoNegocio, idTarifa, idMoneda, idTipo,idConceptoNegocioReferencial, idResponsable);
        }

        //para bonificacion
        public Precio(int idActorNegocio, int idUnidadNegocio, int idConceptoNegocio, decimal importe, int idTarifa, int idMoneda,
            DateTime fechaInicio, DateTime fechaFin, DateTime fechaModificacion, bool indicadorMultiproposito, bool esVigente, bool esPorcentaje,
            int cantidadMinima, int cantidadMaxima, int idTipo, int idConceptoNegocioReferencial, string descripcion, int idResponsable)
        {
            setData(idActorNegocio, idUnidadNegocio, idConceptoNegocio, importe, idTarifa, idMoneda, fechaInicio, fechaFin, fechaModificacion,
                indicadorMultiproposito, esVigente, esPorcentaje, cantidadMinima, cantidadMaxima, idTipo, idConceptoNegocioReferencial, descripcion, idResponsable);
            validarId(idActorNegocio, idUnidadNegocio, idConceptoNegocio, idTarifa, idMoneda, idTipo,idConceptoNegocioReferencial, idResponsable);
        }

        //para precio
        public Precio(int idActorNegocio, int idUnidadNegocio, decimal importe, int idTarifa, int idMoneda, 
            DateTime fechaInicio, DateTime fechaFin, DateTime fechaModificacion, bool indicadorMultiproposito, bool esVigente, bool esPorcentaje,
            int cantidadMinima, int cantidadMaxima, int idTipo, string descripcion, int idResponsable)
        {
            setData(idActorNegocio, idUnidadNegocio, importe, idTarifa, idMoneda, fechaInicio, fechaFin, fechaModificacion,
                indicadorMultiproposito, esVigente, esPorcentaje, cantidadMinima, cantidadMaxima, idTipo, descripcion, idResponsable);
            validarId(idActorNegocio, idUnidadNegocio, idTarifa, idMoneda, idTipo, idResponsable);
        }

        private void setData(int id, int idActorNegocio, int idUnidadNegocio, int idConceptoNegocio, decimal importe, int idTarifa, 
            int idMoneda, DateTime fechaInicio, DateTime fechaFin, DateTime fechaModificacion, bool indicadorMultiproposito, 
            bool esVigente, bool esPorcentaje, int cantidadMinima, int cantidadMaxima, int idTipo, int idConceptoNegocioReferencial, string descripcion,
            int idResponsable)
        {
            this.id = id;
            setData(idActorNegocio, idUnidadNegocio, idConceptoNegocio, importe, idTarifa, idMoneda, fechaInicio, fechaFin, fechaModificacion,
                indicadorMultiproposito, esVigente, esPorcentaje, cantidadMinima, cantidadMaxima, idTipo, idConceptoNegocioReferencial,descripcion, idResponsable);
        }

        private void setData(int id, int idActorNegocio, int idUnidadNegocio, int idConceptoNegocio, decimal importe, int idTarifa,
            int idMoneda, DateTime fechaInicio, DateTime fechaFin, DateTime fechaModificacion, bool indicadorMultiproposito, 
            bool esVigente, bool esPorcentaje, int cantidadMinima, int cantidadMaxima, int idTipo, string descripcion, int idResponsable)
        {
            this.id = id;
            setData(idActorNegocio, idUnidadNegocio, idConceptoNegocio, importe, idTarifa, idMoneda, fechaInicio, fechaFin, fechaModificacion,
                indicadorMultiproposito, esVigente, esPorcentaje, cantidadMinima, cantidadMaxima, idTipo, descripcion, idResponsable);
        }

        private void setData(int idActorNegocio, int idUnidadNegocio, int idConceptoNegocio, decimal importe, int idTarifa,
            int idMoneda, DateTime fechaInicio, DateTime fechaFin, DateTime fechaModificacion, bool indicadorMultiproposito,
            bool esVigente, bool esPorcentaje, int cantidadMinima, int cantidadMaxima, int idTipo, int idConceptoNegocioReferencial, string descripcion,
            int idResponsable)
        {
            this.id_concepto_negocio_referencial = idConceptoNegocioReferencial;
            setData(idActorNegocio, idUnidadNegocio, idConceptoNegocio, importe, idTarifa, idMoneda, fechaInicio, fechaFin, fechaModificacion,
                indicadorMultiproposito, esVigente, esPorcentaje, cantidadMinima, cantidadMaxima, idTipo,descripcion, idResponsable);
        }

        private void setData(int idActorNegocio, int idUnidadNegocio, int idConceptoNegocio, decimal importe, int idTarifa, 
            int idMoneda, DateTime fechaInicio, DateTime fechaFin, DateTime fechaModificacion, bool indicadorMultiproposito, 
            bool esVigente, bool esPorcentaje, int cantidadMinima, int cantidadMaxima, int idTipo, string descripcion, int idResponsable)
        {
            this.id_concepto_negocio = idConceptoNegocio;
            setData(idActorNegocio, idUnidadNegocio, importe, idTarifa, idMoneda, fechaInicio, fechaFin, fechaModificacion,
                indicadorMultiproposito, esVigente, esPorcentaje, cantidadMinima, cantidadMaxima, idTipo,descripcion, idResponsable);
        }

        private void setData(int idActorNegocio, int idUnidadNegocio, decimal importe, int idTarifa, int idMoneda, 
            DateTime fechaInicio, DateTime fechaFin, DateTime fechaModificacion, bool indicadorMultiproposito, 
            bool esVigente, bool esPorcentaje, int cantidadMinima, int cantidadMaxima, int idTipo, string descripcion, int idResponsable)
        {
            this.id_actor_negocio = idActorNegocio;
            this.id_unidad_negocio = idUnidadNegocio;
            this.valor = importe;
            this.id_tarifa_d = idTarifa;
            this.id_moneda = idMoneda;
            this.fecha_inicio = fechaInicio;
            this.fecha_fin = fechaFin;
            this.fecha_modificacion = fechaModificacion;
            this.indicador_multiproposito = indicadorMultiproposito;
            this.es_vigente = esVigente;
            this.porcentaje = esPorcentaje;
            this.cantidad_minima = cantidadMinima;
            this.cantidad_maxima = cantidadMaxima;
            this.id_tipo = idTipo;
            this.id_responsable = idResponsable;
            this.descripcion = descripcion;
        }

        private void validarId(int idActorNegocio, int idUnidadNegocio, int idConceptoNegocio, int idTarifa, int idMoneda, 
            int idTipo, int idConceptoNegocioReferencial, int idResponsable)
        {
            if (idConceptoNegocioReferencial < 1) { throw new IdNoValidoException(idConceptoNegocioReferencial, "concepto negocio referencial"); }
            validarId(idActorNegocio, idUnidadNegocio, idConceptoNegocio, idTarifa, idMoneda, idTipo, idResponsable);
        }

        private void validarId(int idActorNegocio, int idUnidadNegocio, int idConceptoNegocio, int idTarifa, int idMoneda, 
            int idTipo, int idResponsable)
        {
            if (idConceptoNegocio < 1) { throw new IdNoValidoException(idConceptoNegocio, "concepto negocio"); }
            validarId(idActorNegocio, idUnidadNegocio, idTarifa, idMoneda, idTipo, idResponsable);
        }

        private void validarId(int idActorNegocio, int idUnidadNegocio, int idTarifa, int idMoneda, int idTipo, int idResponsable)
        {
            if (idActorNegocio < 1) { throw new IdNoValidoException(idActorNegocio, "Actor negocio"); }
            if (idUnidadNegocio < 1) { throw new IdNoValidoException(idUnidadNegocio, "Unidad negocio"); }
            if (idTarifa < 1) { throw new IdNoValidoException(idTarifa, "Tarifa"); }
            if (idMoneda < 1) { throw new IdNoValidoException(idMoneda, "Moneda"); }
            if (idTipo < 1) { throw new IdNoValidoException(idTipo, "Tipo"); }
            if (idResponsable < 1) { throw new IdNoValidoException(idResponsable, "Empleado responsable"); }
        }

    }
}

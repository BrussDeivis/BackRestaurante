using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades
{
    partial class Existencia
    {

        public Existencia()
        {

        }

        public Existencia(int idActorNegocio, int existencia)
        {
            setData(idActorNegocio, existencia);
            validarId(idActorNegocio);
        }
        
        public Existencia(int id, int idActorNegocio,int idConceptoNegocio, int existencia)
        {
            this.id = id;
            this.id_concepto_negocio = idConceptoNegocio;
            setData(idActorNegocio,existencia);
            validarId(idActorNegocio, idConceptoNegocio);
        }


        public void setData( int idActorNegocio, int existencia)
        {
            this.id_punto_atencion = idActorNegocio;
            this.existencia1 = existencia;
        }

        protected void validarId(int idActorNegocio, int idConceptoNegocio)
        {
            if (idConceptoNegocio < 1) { throw new IdNoValidoException(idConceptoNegocio, "Concepto Negocio"); }
            validarId(idActorNegocio);
        }

        protected void validarId(int idActorNegocio)
        {
            if (idActorNegocio < 1) { throw new IdNoValidoException(idActorNegocio, "Actor Negocio"); }
        }
    }
}

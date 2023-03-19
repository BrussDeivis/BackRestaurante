using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades
{
    partial class Parametro_actor_negocio
    {

        public Parametro_actor_negocio()
        {

        }

        public Parametro_actor_negocio(int idParametro, string valor)
        {
            this.id_parametro = idParametro;
            this.valor = valor;
            validarId(idParametro);
        }

        
        public Parametro_actor_negocio(int idParametro, int? idValorParametro, string valor)
        {
            setData(idParametro, idValorParametro, valor);
            validarId(idParametro, idValorParametro);
        }

        public Parametro_actor_negocio(int idActorNegocio, int idParametro, string valor)
        {
            this.id_actor_negocio = idActorNegocio;
            this.id_parametro = idParametro;
            this.valor = valor;
            validarId(idParametro);
        }

        public Parametro_actor_negocio(int idActorNegocio, int idParametro, int? idValorParametro, string valor)
        {
            setData(idActorNegocio, idParametro, idValorParametro, valor);
            validarId(idActorNegocio, idParametro, idValorParametro);
        }
        public void setData( int? idValorParametro, string valor)
        {
            this.id_valor_parametro = idValorParametro;
            this.valor = valor;
        }

        public void setData(int idParametro, int? idValorParametro, string valor)
        {
            this.id_parametro = idParametro;
            setData(idValorParametro, valor);

        }

        public void setData(int idActorNegocio, int idParametro, int? idValorParametro, string valor)
        {
            this.id_actor_negocio = idActorNegocio;
            setData(idParametro, idValorParametro, valor);
        }

        protected void validarId(int idActorNegocio, int idParametro, int? idValorParametro)
        {
            if (idActorNegocio < 1) { throw new IdNoValidoException(idActorNegocio, "Actor Negocio"); }
            validarId(idParametro, idValorParametro);

        }

        protected void validarId(int idParametro, int? idValorParametro)
        {
            if (idParametro < 1) { throw new IdNoValidoException(idParametro, "Parametro"); }
            validarId(idValorParametro);
        }

        protected void validarId(int? idValorParametro)
        {
            if (idValorParametro!=null && idValorParametro < 1) { throw new IdNoValidoException((int)idValorParametro, "Valor parametro"); }
        }
    }
}

using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo.Custom
{
    public class RespuestaVerificacionActorNegocio
    {
        public RespuestaVerificacionEnum respuesta;
        public Actor actor=null;
        public Actor_negocio actorNegocio=null;
        public string mensaje = "";
        
        public RespuestaVerificacionActorNegocio()
        {
            
        }

        public RespuestaVerificacionActorNegocio(RespuestaVerificacionEnum code, string mensaje)
        {
            this.mensaje = mensaje;
            this.respuesta = code;
        }
        public RespuestaVerificacionActorNegocio(RespuestaVerificacionEnum code)
        {
            this.respuesta = code;
        }

        public RespuestaVerificacionActorNegocio(RespuestaVerificacionEnum code, Actor actorObtenido)
        {
            this.respuesta = code;
            this.actor = actorObtenido;
        }

        public RespuestaVerificacionActorNegocio(RespuestaVerificacionEnum code,Actor_negocio actorNegocioObtenido)
        {
            this.respuesta = code;
            this.actorNegocio = actorNegocioObtenido;
        }
        
    }

    
}

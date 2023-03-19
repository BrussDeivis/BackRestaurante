using System;
using System.Collections.Generic;
using Tsp.Sigescom.Config;
using System.Linq;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class Deuda 
    {

        private ActorComercial actorComercial;
        private decimal Importe;
        private DateTime Fecha;

        public Deuda(ActorComercial actorComercial, decimal importe, DateTime fecha)
        {
            this.actorComercial = actorComercial;
            Importe = importe;
            Fecha = fecha;
        }

        public ActorComercial ActorComercial { get => actorComercial; set => actorComercial = value; }
        public decimal Importe1 { get => Importe; set => Importe = value; }
        public DateTime Fecha1 { get => Fecha; set => Fecha = value; }


    }

}

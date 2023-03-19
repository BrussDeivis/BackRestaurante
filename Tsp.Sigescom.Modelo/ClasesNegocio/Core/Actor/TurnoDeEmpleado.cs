using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class TurnoDeEmpleado
    {
        private readonly Vinculo_Actor_Negocio turno;

        public TurnoDeEmpleado()
        {

        }
        public TurnoDeEmpleado(Vinculo_Actor_Negocio turno)
        {
            this.turno = turno;
        }
        //Actor de negocio vinculado => actor_negocio1
        public Empleado Empleado()
        {
            return new Empleado(this.turno.Actor_negocio1);
        }
        //Actor de negocio principal => actor_negocio
        public CentroDeAtencionExtendido CentroDeAtencion()
        {
            return new CentroDeAtencionExtendido(this.turno.Actor_negocio);
        }

        public EstablecimientoComercial EstablecimientoComercial()
        {
            return new CentroDeAtencionExtendido(this.turno.Actor_negocio).EstablecimientoComercial;
        }
        public int Id
        {
            get { return this.turno.id; }
        }
        public int IdEmpleado
        {
            get { return this.turno.id_actor_negocio_vinculado; }
        }
        public int IdCentroDeAtencion
        {
            get { return this.turno.id_actor_negocio_principal ; }
        }
        public DateTime Desde()
        {
            return this.turno.desde;
        }
        public DateTime Hasta()
        {
            return this.turno.hasta;
        }
        public String Descripcion
        {
            get { return this.turno.descripcion; }
        }
        public static List<TurnoDeEmpleado> Convert(List<Vinculo_Actor_Negocio> vinculosActorNegocio)
        {
            var turnos = new List<TurnoDeEmpleado>();

            foreach (var item in vinculosActorNegocio)
            {
                turnos.Add(new TurnoDeEmpleado(item));
            }
            return turnos;
        }
    }
}

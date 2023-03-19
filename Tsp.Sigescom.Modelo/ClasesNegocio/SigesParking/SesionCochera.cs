using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Custom;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Sesion;

namespace Tsp.Sigescom.Modelo.Custom.SigesParking
{
    [Serializable]
    public class SesionCochera
    {
        public int IdCochera { get {return this.SesionDeUsuario.IdCentroDeAtencionSeleccionado; } }
        public UserProfileSessionData SesionDeUsuario { get; set; }
        public ConfiguracionCochera Configuracion { get; set; }
        public List<TurnoCochera> Turnos { get; set; }
        public List<ImporteConcepto> Precios { get; set; }
        public ImporteConcepto Precio(int  idConcepto)
        {
            return Precios.SingleOrDefault(p => p.Concepto.Id== idConcepto);
        }
        public TurnoCochera ObtenerTurno(DateTime fechaHoraReferencia)
        {
            return Turnos.SingleOrDefault(t=> fechaHoraReferencia >= t.Inicio(fechaHoraReferencia) && fechaHoraReferencia<= t.Fin(fechaHoraReferencia));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.WebApplication.Models;

namespace Tsp.Sigescom.WebApplication.Models
{
    [Serializable]
    [DataContract]
    public class PermisoViewModel
    {
        [DataMember]
        public int Id { get; set; }
        public ComboGenericoViewModel TipoTransaccion { get; set; }
        public ComboGenericoViewModel Estado { get; set; }
        public ComboGenericoViewModel RolPersonal { get; set; }
        public ComboGenericoViewModel UnidadNegocio { get; set; }
        public ComboGenericoViewModel Accion { get; set; }

        public PermisoViewModel()
        {

        }

        public PermisoViewModel(int id, int idTipoTransaccion, string tipoTransaccion,int idRolPersonal, string rolPersonal, int idAccion,string accion,int idUnidadNegocio,string unidadNegocio)
        {
            this.Id = id;
            this.TipoTransaccion = new ComboGenericoViewModel(idTipoTransaccion,tipoTransaccion);
            this.RolPersonal = new ComboGenericoViewModel(idRolPersonal,rolPersonal);
            this.Accion = new ComboGenericoViewModel(idAccion, accion);
            this.UnidadNegocio = new ComboGenericoViewModel(idUnidadNegocio, unidadNegocio);
        }

        public PermisoViewModel(int id, int idTipoTransaccion, string tipoTransaccion, int idEstado,string estado, int idAccion, string accion)
        {
            this.Id = id;
            this.TipoTransaccion = new ComboGenericoViewModel(idTipoTransaccion, tipoTransaccion);
            this.Estado = new ComboGenericoViewModel(idEstado, estado);
            this.Accion = new ComboGenericoViewModel(idAccion, accion);
        }
    }
}
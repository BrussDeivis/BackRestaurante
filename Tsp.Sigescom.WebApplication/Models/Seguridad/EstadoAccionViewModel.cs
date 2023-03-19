using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Tsp.Sigescom.WebApplication.Models.Seguridad
{
    [Serializable]
    [DataContract]
    public class EstadoAccionViewModel
    {


        [DataMember]
        public int Id { get; set; }
        public int IdTipoTransaccion { get; set; }
        public string TipoTransaccion { get; set; }
        public int IdEstado { get; set; }
        public int IdRolPersonal { get; set; }
        public string RolPersonal { get; set; }
        public int IdUnidadNegocio { get; set; }
        public string UnidadNegocio { get; set; }
        public int IdAccion { get; set; }
        public string Accion { get; set; }

        public EstadoAccionViewModel()
        {

        }

        public EstadoAccionViewModel(int id, int idTipoTransaccion, string tipoTransaccion, int idRolPersonal, string rolPersonal, int idAccion, string accion, int idUnidadNegocio, string unidadNegocio)
        {
            this.Id = id;
            this.IdTipoTransaccion = idTipoTransaccion;
            this.TipoTransaccion = tipoTransaccion;
            this.IdRolPersonal = idRolPersonal;
            this.RolPersonal = rolPersonal;
            this.IdAccion = idAccion;
            this.Accion = accion;
            this.IdUnidadNegocio = idUnidadNegocio;
            this.UnidadNegocio = unidadNegocio;
        }

        public EstadoAccionViewModel(int id, int idTipoTransaccion, int idEstado, int idAccion)
        {
            this.Id = id;
            this.IdTipoTransaccion = idTipoTransaccion;
            this.IdEstado = idEstado;
            this.IdAccion = idAccion;
        }
    }
}
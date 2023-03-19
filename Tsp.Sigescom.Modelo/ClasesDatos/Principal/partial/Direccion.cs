using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades
{
    partial class Direccion
    {
        public Direccion()
        {
        }
        public Direccion(int id,int idActor, int idTipoDireccion, int idNacion, int idUbigeo, string detalle, int? idTipoVia, int? idTipoZona, bool esPrincipal, bool esActivo)
        {
            setData(id,idActor, idTipoDireccion, idNacion, idUbigeo, detalle, idTipoVia, idTipoZona, esPrincipal, esActivo);
            validarId(idActor, idTipoDireccion, idNacion, idTipoVia, idTipoZona, idUbigeo);
        }

        public Direccion(int idActor,int idTipoDireccion, int idNacion, int idUbigeo, string detalle, int? idTipoVia, int? idTipoZona, bool esPrincipal, bool esActivo)
        {
            setData(idActor, idTipoDireccion, idNacion, idUbigeo, detalle, idTipoVia, idTipoZona, esPrincipal, esActivo);
            validarId( idActor, idTipoDireccion, idNacion,  idTipoVia, idTipoZona, idUbigeo);
        }

        public Direccion(int idTipoDireccion, int idNacion, int idUbigeo, string detalle, int? idTipoVia, int? idTipoZona, bool esPrincipal, bool esActivo)
        {
            setData(idTipoDireccion, idNacion, idUbigeo, detalle, idTipoVia, idTipoZona, esPrincipal, esActivo);
            validarId(idTipoDireccion, idNacion, idTipoVia, idTipoZona, idUbigeo);

        }
        public void setData(int idActor, int idTipoDireccion, int idNacion, int idUbigeo, string detalle, int? idTipoVia, int? idTipoZona, bool esPrincipal, bool esActivo)
        {
            this.id_actor = idActor;
            setData(idTipoDireccion, idNacion, idUbigeo, detalle, idTipoVia, idTipoZona, esPrincipal, esActivo);
        }
        public void setData(int id,int idActor, int idTipoDireccion, int idNacion, int idUbigeo, string detalle,int? idTipoVia, int? idTipoZona, bool esPrincipal, bool esActivo)
        {
            this.id = id;
            setData(idActor,idTipoDireccion, idNacion, idUbigeo, detalle, idTipoVia, idTipoZona, esPrincipal, esActivo);
        }

        public void setData(int idTipoDireccion, int idNacion, int idUbigeo, string detalle, int? idTipoVia, int? idTipoZona, bool esPrincipal, bool esActivo)
        {
            this.id_tipo_direccion = idTipoDireccion;
            this.id_nacion = idNacion;
            this.id_ubigeo = idUbigeo;
            this.detalle = detalle;
            this.id_tipo_via = idTipoVia;
            this.id_tipo_zona = idTipoZona;
            this.es_principal = esPrincipal;
            this.es_activo = esActivo;
        }

        protected void validarId( int idActor, int idTipoDireccion, int idNacion, int? idTipoVia, int? idTipoZona, int idUbigeo)
        {
            if (idActor < 1) { throw new IdNoValidoException(idActor, "Direccion"); }
            validarId(idTipoDireccion,idNacion,idTipoVia,idTipoZona,idUbigeo);
        }

        protected void validarId(int idTipoDireccion, int idNacion, int? idTipoVia, int? idTipoZona, int idUbigeo)
        {
            if (idTipoDireccion < 1) { throw new IdNoValidoException(idTipoDireccion, "Direccion"); }
            if (idNacion < 1) { throw new IdNoValidoException(idNacion, "Direccion"); }
            if (idTipoVia < 1 && idTipoVia!=null) { throw new IdNoValidoException((int)idTipoVia, "Direccion"); }
            if (idTipoZona < 1 && idTipoZona!=null) { throw new IdNoValidoException((int)idTipoZona, "Direccion"); }
            if (idUbigeo < 1) { throw new IdNoValidoException(idUbigeo, "Direccion"); }
        }
    }
}

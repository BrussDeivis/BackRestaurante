using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;

namespace Tsp.Sigescom.Modelo.Entidades
{
    public partial class Concepto_negocio
    {
      

        public Concepto_negocio(string codigo, string codigoBarra, string codigoNegocio1, string nombre, string sufijo, string propiedades, int idConceptoBasico, int idUnidadMedidaPrimaria,int idPresentacion, decimal contenido, int idUnidadMedidaContenido, int? idSubContenido, int? idFoto, bool esVigente, int idUnidadMedidaSecundaria)
        {
            InitSet();
            setData(codigo, codigoBarra, codigoNegocio1, nombre, sufijo, propiedades,idConceptoBasico, idUnidadMedidaPrimaria,idPresentacion, contenido, idUnidadMedidaContenido, idSubContenido, idFoto, esVigente, idUnidadMedidaSecundaria);
            validarId(idConceptoBasico, idSubContenido,idFoto, idPresentacion, idUnidadMedidaPrimaria, idUnidadMedidaContenido, idUnidadMedidaSecundaria);
        }

        public Concepto_negocio(int id, string codigo, string codigoBarra, string codigoNegocio1, string nombre, string sufijo, string propiedades, int idConceptoBasico, int idUnidadMedidaPrimaria, int idPresentacion, decimal contenido, int idUnidadMedidaContenido, int? idSubContenido, int? idFoto, bool esVigente, int idUnidadMedidaSecundaria)
        {
            InitSet();
            setData(id,codigo, codigoBarra, codigoNegocio1, nombre, sufijo, propiedades, idConceptoBasico, idUnidadMedidaPrimaria, idPresentacion, contenido, idUnidadMedidaContenido, idSubContenido, idFoto, esVigente, idUnidadMedidaSecundaria);
            validarId(idConceptoBasico, idSubContenido, idFoto, idPresentacion, idUnidadMedidaPrimaria, idUnidadMedidaContenido, idUnidadMedidaSecundaria);
        }

        private void setData(int id,string codigo, string codigoBarra, string codigoNegocio1, string nombre, string sufijo, string propiedades, int idConceptoBasico, int idUnidadMedidaPrimaria, int idPresentacion, decimal contenido, int idUnidadMedidaContenido, int? idSubContenido, int? idFoto, bool esVigente, int idUnidadMedidaSecundaria)
        {
            InitSet();
            this.id = id;
            setData(codigo, codigoBarra, codigoNegocio1, nombre, sufijo, propiedades, idConceptoBasico, idUnidadMedidaPrimaria, idPresentacion, contenido, idUnidadMedidaContenido, idSubContenido, idFoto, esVigente, idUnidadMedidaSecundaria);
        }

        public void setData(string codigo, string codigoBarra, string codigoNegocio1, string nombre, string sufijo, string propiedades, int idConceptoBasico, int idUnidadMedidaPrimaria, int idPresentacion, decimal contenido, int idUnidadMedidaContenido, int? idSubContenido, int? idFoto, bool esVigente, int idUnidadMedidaSecundaria)
        {
            this.codigo = codigo;
            this.codigo_barra = codigoBarra;
            this.codigo_negocio1 = codigoNegocio1;
            this.nombre = nombre;
            this.sufijo = sufijo;
            this.propiedades = propiedades;
            this.id_unidad_medida_primaria = idUnidadMedidaPrimaria;
            this.id_presentacion = idPresentacion;
            this.contenido = contenido;
            this.id_unidad_medida_contenido = idUnidadMedidaContenido;
            this.id_sub_contenido = idSubContenido;
            this.id_foto = idFoto;
            this.es_vigente = esVigente;
            this.id_unidad_medida_secundaria = idUnidadMedidaSecundaria;
            this.id_concepto_basico = idConceptoBasico;
        }
        protected void InitSet()
        {
            this.Concepto_negocio1 = new HashSet<Concepto_negocio>();
            this.Concepto_negocio_rol = new HashSet<Concepto_negocio_rol>();
            this.Valor_caracteristica_concepto_negocio = new HashSet<Valor_caracteristica_concepto_negocio>();
            this.Precio = new HashSet<Precio>();
            this.Existencia = new HashSet<Existencia>();
            this.Parametro_concepto_negocio = new HashSet<Parametro_concepto_negocio>();
            this.Detalle_transaccion = new HashSet<Detalle_transaccion>();
        }
        protected void validarId(int idConceptoBasico, int? idSubContenido, int? idFoto, int idPresentacion, int idUnidadMedidaPrimaria, int idUnidadMedidaContenido, int idUnidadMedidaSecundaria)
        {
            if (idConceptoBasico < 1) { throw new IdNoValidoException(idConceptoBasico, "Concepto basico"); }
            if (idSubContenido != null && idSubContenido < 1) { throw new IdNoValidoException((int)idSubContenido, "Contenido"); }
            if (idFoto != null && idFoto < 1) { throw new IdNoValidoException((int)idFoto, "Foto"); }
            if (idPresentacion < 1) { throw new IdNoValidoException(idPresentacion, "Presentacion"); }
            if (idUnidadMedidaPrimaria < 1) { throw new IdNoValidoException(idUnidadMedidaPrimaria, "Unidad de medida"); }
            if (idUnidadMedidaContenido < 1) { throw new IdNoValidoException(idUnidadMedidaContenido, "Unidad de medida"); }
            if (idUnidadMedidaSecundaria < 1) { throw new IdNoValidoException(idUnidadMedidaSecundaria, "Unidad de medida"); }
        }

        public bool EsBien
        {
            get { return this.Detalle_maestro4.valor == "1"; }
        }
    }
        
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;

namespace Tsp.Sigescom.Modelo.Entidades
{
    public partial class Comprobante
    {

        public Comprobante(int idTipoComprobante, int? idSerieComprobante, int numero, bool esValido, string numeroSerie)
        {
            setData(idTipoComprobante, idSerieComprobante, numero, esValido, numeroSerie);
            validarId(idTipoComprobante, idSerieComprobante);
        }

        public void setData(int idTipoComprobante, int? idSerieComprobante, int numero, bool esValido, string numeroSerie)
        {
            this.id_tipo_comprobante = idTipoComprobante;
            this.id_serie_comprobante = idSerieComprobante;
            this.numero = numero;
            this.es_valido = esValido;
            this.numero_serie = numeroSerie;

        }
        protected void validarId(int idTipoComprobante, int? idSerieComprobante)
        {
            if (idTipoComprobante < 1) { throw new IdNoValidoException(idTipoComprobante, "tipo comprobante"); }
            if (idSerieComprobante < 1 && idSerieComprobante!=null) { throw new IdNoValidoException((int)idSerieComprobante, "serie comprobante"); }

        }
    }
}

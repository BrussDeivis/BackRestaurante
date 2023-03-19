using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;

namespace Tsp.Sigescom.Modelo.Entidades
{
    public partial class Traza_pago
    {
        public Traza_pago()
        {
        }
        public Traza_pago(int idMedioPago, string traza, int idEntidadBancaria)
        {
            this.id_entidad_bancaria = idEntidadBancaria;
            SetData(idMedioPago, traza, idEntidadBancaria);
            ValidarId(idEntidadBancaria, idMedioPago);
        }
        public Traza_pago(int idMedioPago, string traza, int idEntidadBancaria, int? idTipoOperacion)
        {
            SetData(idMedioPago, traza, idEntidadBancaria, idTipoOperacion);
            ValidarId(idEntidadBancaria, idMedioPago);
        }
        public void SetData(int idMedioPago, string traza, int idEntidadBancaria, int? idTipoOperacion)
        {
            SetData(idMedioPago, traza, idEntidadBancaria);
            this.id_tipo_operador = idTipoOperacion;
            ValidarId(idEntidadBancaria, idMedioPago);
            if (idTipoOperacion != null && idTipoOperacion < 1) { throw new IdNoValidoException((int)idTipoOperacion, "tipo operacion"); }
        }
        public void SetData(int idMedioPago, string traza, int idEntidadBancaria)
        {
            this.traza = traza;
            this.id_medio_pago = idMedioPago;
            this.id_entidad_bancaria = idEntidadBancaria;
            this.extension_json = "";
        }
        protected void ValidarId(int idEntidadBancaria, int idMedioPago)
        {
            if (idEntidadBancaria < 1) { throw new IdNoValidoException(idEntidadBancaria, "entidad bancaria"); }
            if (idMedioPago < 1) { throw new IdNoValidoException(idMedioPago, "medio pago"); }
        }
    }
}

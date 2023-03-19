using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class TrazaDePago
    {
        protected Traza_pago traza;

        public TrazaDePago()
        {
        }

        public TrazaDePago(Traza_pago traza)
        {
            this.traza = traza;
        }
        //Medio de Pago
        public Detalle_maestro MedioDePago()
        {
            return this.traza.Detalle_maestro1;
        }
        ////Tipo de Pago
        //public Detalle_maestro TipoDePago()
        //{
        //    return this.traza.Detalle_maestro2;
        //}
        //Entidad
        public Detalle_maestro EntidadBancaria()
        {
            return this.traza.Detalle_maestro;
        }

        //Informacion del Pago
        public string Informacion
        {
            get { return this.traza.traza; }
        }
        /// <summary>
        /// Json de informacion de pago
        /// </summary>
        public string ExtensionJson
        {
            get { return this.traza.extension_json; }
        }

        public static List<TrazaDePago> convert(List<Traza_pago> trazas)
        {
            var trazas_ = new List<TrazaDePago>();

            foreach (var traza in trazas)
            {
                trazas_.Add(new TrazaDePago(traza));
            }
            return trazas_;
        }
    }
}

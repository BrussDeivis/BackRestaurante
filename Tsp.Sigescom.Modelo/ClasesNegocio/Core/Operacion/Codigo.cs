using System;
using System.Collections.Generic;
using Tsp.Sigescom.Config;
using System.Linq;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class Codigo
    {
        //private DateTime fechaEmision;
        //private int maximoCodigoVenta;
        //private int maximoCodigoOrdenVenta;
        //private int maximoCodigoCuota;
        //private int maximoCodigoPago;
        //private int maximoCodigoSalidaMercaderia;
        //private string siguienteCodigoVenta;
        //private string siguienteCodigoOrdenVenta;
        //private string siguienteCodigoCodigoCuota;
        //private string siguienteCodigoPago;
        //private string siguienteCodigoSalidaMercaderia;

        public Codigo()
        {

        }

        public Codigo(DateTime fechaEmision, int maximoCodigoVenta, int maximoCodigoCuota, int maximoCodigoPago)
        {
            this.FechaEmision = fechaEmision;
            this.MaximoCodigoVenta = maximoCodigoVenta;
            this.MaximoCodigoCuota = maximoCodigoCuota;
            this.MaximoCodigoPago = maximoCodigoPago;
        }

        public Codigo(DateTime fechaEmision, int maximoCodigoVenta, int maximoCodigoCuota, int maximoCodigoPago, int maximoCodigoSalidaMercaderia)
        {
            this.FechaEmision = fechaEmision;
            this.MaximoCodigoVenta = maximoCodigoVenta;
            this.MaximoCodigoCuota = maximoCodigoCuota;
            this.MaximoCodigoPago = maximoCodigoPago;
            this.MaximoCodigoSalidaMercaderia = maximoCodigoSalidaMercaderia;
        }


        public Codigo(DateTime fechaEmision, int maximoCodigoVenta, int maximoCodigoOrdenVenta, int maximoCodigoCuota, int maximoCodigoPago, int maximoCodigoSalidaMercaderia)
        {
            this.FechaEmision = fechaEmision;
            this.MaximoCodigoVenta = maximoCodigoVenta;
            this.MaximoCodigoOrdenVenta = maximoCodigoOrdenVenta;
            this.MaximoCodigoCuota = maximoCodigoCuota;
            this.MaximoCodigoPago = maximoCodigoPago;
            this.MaximoCodigoSalidaMercaderia = maximoCodigoSalidaMercaderia;
        }

        public Codigo(DateTime fechaEmision, int maximoCodigoVenta, int maximoCodigoCuota)
        {
            this.FechaEmision = fechaEmision;
            this.MaximoCodigoVenta = maximoCodigoVenta;
            this.MaximoCodigoCuota = maximoCodigoCuota;
        }


        public Codigo(int maximoCodigoVenta, int maximoCodigoCuota, int maximoCodigoSalidaMercaderia, DateTime fechaEmision)
        {
            this.FechaEmision = fechaEmision;
            this.MaximoCodigoVenta = maximoCodigoVenta;
            this.MaximoCodigoCuota = maximoCodigoCuota;
            this.MaximoCodigoSalidaMercaderia = maximoCodigoSalidaMercaderia;
        }

        public Codigo(DateTime fechaEmision, int maximoCodigoPago)
        {
            this.FechaEmision = fechaEmision;
            this.MaximoCodigoPago = maximoCodigoPago;
        }

        public Codigo(int maximoCodigoSalidaMercaeria)
        {
            this.MaximoCodigoSalidaMercaderia = maximoCodigoSalidaMercaeria;
        }
        public DateTime FechaEmision { get; set; }
        public int MaximoCodigoVenta { get; set; }
        public int MaximoCodigoOrdenVenta { get; set; }
        public int MaximoCodigoCuota { get; set; }
        public int MaximoCodigoPago { get; set; }
        public int MaximoCodigoSalidaMercaderia { get; set; }

        //public DateTime FechaEmision { get => fechaEmision; set => fechaEmision = value; }
        //public int MaximoCodigoVenta { get => maximoCodigoVenta; set => maximoCodigoVenta = value; }
        //public int MaximoCodigoOrdenVenta { get => maximoCodigoOrdenVenta; set => maximoCodigoOrdenVenta = value; }
        //public int MaximoCodigoCuota { get => maximoCodigoCuota; set => maximoCodigoCuota = value; }
        //public int MaximoCodigoPago { get => maximoCodigoPago; set => maximoCodigoPago = value; }
        //public int MaximoCodigoSalidaMercaderia { get => maximoCodigoSalidaMercaderia; set => maximoCodigoSalidaMercaderia = value; }

        public string SiguienteCodigoVenta { get => "V" + (MaximoCodigoVenta + 1).ToString();}
        public string SiguienteCodigoOrdenVenta { get => SiguienteCodigoVenta + "_OV" + (MaximoCodigoOrdenVenta + 1).ToString();}
        public string SiguienteCodigoCodigoCuota { get => "C" + FechaEmision.Year.ToString() + (MaximoCodigoCuota + 1).ToString() + "_" + 1;}
        public string SiguienteCodigoPago { get => "C" + FechaEmision.ToString("yyyy") + "_P" + (MaximoCodigoPago + 1).ToString();}
        public string SiguienteCodigoSalidaMercaderia { get => SiguienteCodigoVenta + "_SMV" + (MaximoCodigoSalidaMercaderia + 1).ToString();}

    }

}

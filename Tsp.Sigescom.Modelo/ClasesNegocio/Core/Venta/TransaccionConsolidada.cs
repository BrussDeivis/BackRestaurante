using Tsp.Sigescom.Config; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades
{
    public class Transaccion_consolidada
    {
        public DateTime FechaEmision { get { return new DateTime(Anyo, Mes, Dia); } }
        public int Anyo;
        public int Mes;
        public int Dia;
        public long IdTipoComprobante;
        public string CodigoTipoComprobante;
        public int? IdSerie;
        public string Serie;
        public int NumeroInicial;
        public int NumeroFinal;
        public decimal ValorDeVenta;
        public decimal IGV;
        public decimal Total;
        public bool GravaIgv;
        public long UltimoId;
        public string CodigoMoneda;
        public decimal TipoCambio;

        public int IdActorNegocioExterno;
        public string NombreActorNegocioExterno;
        public string NumeroDocumentoActorNegocioExterno;

        //private IEnumerable<string> icbpers;
        //private decimal icbper;

        //public IEnumerable<string> Icbpers { get => icbpers; set => icbpers = value; }

        //public decimal Icbper { get => (Icbpers != null) && Icbpers.Count() > 0 ? Icbpers.Sum(i => System.Convert.ToDecimal(i)) : 0; }

    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class Cliente : ActorComercial
    {
        public Cliente(Actor_negocio actorDeNegocio) : base(actorDeNegocio)
        {
             
        }

        public string TipoDocumentoIdentidadCliente
        {
            get
            {
                return (Id == ActorSettings.Default.IdClienteGenerico ? "" : this.CodigoTipoDocumentoIdentidad());
            }
        }
        public string NumeroDocumentoIdentidadCliente
        {
            get
            {
                return (Id == ActorSettings.Default.IdClienteGenerico ? "" : this.DocumentoIdentidad);
            }
        }

        public static List<Cliente> Convert(List<Actor_negocio> actoresDeNegocio)
        {
            var clientes = new List<Cliente>();

            foreach (var actorDeNegocio in actoresDeNegocio)
            {
                clientes.Add(new Cliente(actorDeNegocio));
            }
            return clientes;
        }

        public int IdComprobantePredeterminado()
        {
            var parametro = this.ActorDeNegocio.Parametro_actor_negocio.SingleOrDefault(pcn => pcn.id_parametro == MaestroSettings.Default.IdDetalleMaestroParametroComprobanteDeClientePredeterminado);
            return parametro != null ? System.Convert.ToInt32(parametro.valor) : 0;
        }
        
        public decimal ObtenerMontoTotalDeuda()

        {
            decimal montoTotalDeDeuda;
            montoTotalDeDeuda = this.ActorDeNegocio.Transaccion1.SelectMany(c => c.Cuota).Where(c => c.por_cobrar == true && c.saldo > 0).Sum(c => c.saldo);
            return montoTotalDeDeuda;
        }
    }
}

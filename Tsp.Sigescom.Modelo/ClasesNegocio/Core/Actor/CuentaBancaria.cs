using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class CuentaBancaria
    {
        public int Id { get; set; }
        public int IdActor { get; set; }
        public ItemGenericoBase TipoCuenta { get; set; }
        public ItemGenericoBase EntidadFinanciera { get; set; }
        public string Titular { get; set; }
        public ItemGenericoBase Moneda { get; set; }
        public string NumeroCuenta { get; set; }
        public string NumeroCCI { get; set; }
        public string Ubigeo { get; set; }
        public bool EstaActivo { get; set; }

        public CuentaBancaria()
        {
        }

        //    public CuentaBancaria(Actor_negocio actorNegocio)
        //{
        //    this.Id = actorNegocio.id;
        //    this.EntidadFinanciera = new ItemGenerico()
        //    {
        //        Id = actorNegocio.,
        //        Nombre = actorNegocio.
        //    };
        //    this.TipoCuenta = new ItemGenerico()
        //    {
        //        Id = actorNegocio.,
        //        Nombre = actorNegocio.
        //    };
        //    this.NumeroCuenta = actorNegocio.;
        //    this.NumeroCCI = actorNegocio.PrimerNombre;
        //    this.NumeroCCI = actorNegocio.TercerNombre;
        //    this.Moneda = new ItemGenerico()
        //    {
        //        Id = actorNegocio.,
        //        Nombre = actorNegocio.
        //        };
        //    this.Estado = actorNegocio.es_vigente;
        //}

        //public List<CuentaBancaria> Convert(List<Actor_negocio> actoresDeNegocio)
        //{
        //    List<CuentaBancaria> cuentasBancarias = new List<CuentaBancaria>();
        //    foreach (var actorNegocio in actoresDeNegocio)
        //    {
        //        cuentasBancarias.Add(new CuentaBancaria(actorNegocio));
        //    }
        //    return cuentasBancarias;
        //}
        public Actor_negocio ActorNegocio()
        {
            return new Actor_negocio()
            {
                id = this.Id,
                Actor = new Actor()
                {
                    id_documento_identidad = this.TipoCuenta.Id,
                    numero_documento_identidad = this.NumeroCuenta,
                    primer_nombre = this.NumeroCCI,
                    segundo_nombre = this.Titular,
                    id_detalle_multiproposito = this.EntidadFinanciera.Id,
                    id_detalle_multiproposito1 = this.Moneda.Id,
                    informacion_multiproposito = this.Ubigeo
                },
                es_vigente = EstaActivo
            };
        }
    }
    public class ItemGenericoBase
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public ItemGenericoBase()
        {

        }

        public ItemGenericoBase(int id)
        {
            this.Id = id;
        }

        public ItemGenericoBase(int id, string nombre)
        {
            this.Id = id;
            this.Nombre = nombre;
        }
    }

    public class ItemGenericoBaseLong
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
    }

}

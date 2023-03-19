using System;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Sesion;

namespace Tsp.Sigescom.Modelo.Custom.SigesParking
{
    public class Ingreso
    {
        public Vehiculo Vehiculo { get; set; }
        public ActorComercial_ Cliente { get; set; }
        public DateTime FechaHora { get; set; }
        public String Fecha { get; set; }
        public String Hora { get; set; }
        public bool EsValido { get; set; }

        public ItemGenerico SistemaDePago { get; set; }
        public string Observacion { get; set; }

        public Transaccion Convert(UserProfileSessionData userProfile, DateTime fecha)
        {
            Transaccion transaccion = new Transaccion()
            {
                //datos generales
                id_actor_negocio_interno =userProfile.IdCentroDeAtencionSeleccionado,
                id_empleado = userProfile.Empleado.Id,
                id_moneda = userProfile.IdMonedaPorDefecto(),
                id_unidad_negocio = userProfile.IdUnidadDeNegocioPorDefecto(),
                fecha_registro_sistema = fecha,
                fecha_fin = fecha,
                fecha_registro_contable = fecha,
                tipo_cambio = userProfile.TipoDeCambio.ValorVenta,
                id_transaccion_padre = userProfile.OperacionSesionContenedora.Id,

                //datos relacionados al ingreso a cochera
                id_actor_negocio_externo = this.Cliente.Id,
                id_actor_negocio_externo1 = this.Vehiculo.Id,
                comentario = this.Observacion,
                enum1 = this.SistemaDePago.Id,
                es_concreta = true,
                fecha_inicio = fecha,
                indicador1 = false,
                indicador2 = false,
                codigo = "",
                id_tipo_transaccion = CocheraSettings.Default.IdTipoTransaccionMovimientoDeCochera,
                importe_total = 0,
            };
            return transaccion;
        }
    }
}

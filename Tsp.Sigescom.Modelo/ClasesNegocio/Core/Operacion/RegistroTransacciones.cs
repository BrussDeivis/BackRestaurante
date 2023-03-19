using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class RegistroTransacciones
    {

        public List<Transaccion> Transacciones_Crear { get; set; }
        public List<Transaccion> Transacciones_Modificar { get; set; }
        public List<Detalle_transaccion> DetallesTransaccion_Crear { get; set; }
        public List<Detalle_transaccion> DetallesTransaccion_Modificar { get; set; }
        public List<Estado_transaccion> EstadosTransaccion_Crear { get; set; }
        public List<Estado_cuota> EstadosCuota_Crear { get; set; }
        public List<Actor_negocio> Actores_Negocio_Modificar { get; set; }

        public RegistroTransacciones()
        {
        }

        public RegistroTransacciones(List<Transaccion> transacciones_Crear, List<Transaccion> transacciones_Modificar, List<Detalle_transaccion> detallesTransaccion_Crear, List<Detalle_transaccion> detallesTransaccion_Modificar, List<Estado_transaccion> estadosTransaccion_Crear, List<Estado_cuota> estadosCuota_Crear)
        {
            Transacciones_Crear = transacciones_Crear;
            Transacciones_Modificar = transacciones_Modificar;
            DetallesTransaccion_Crear = detallesTransaccion_Crear;
            DetallesTransaccion_Modificar = detallesTransaccion_Modificar;
            EstadosTransaccion_Crear = estadosTransaccion_Crear;
            EstadosCuota_Crear = estadosCuota_Crear;
        }
    }
}
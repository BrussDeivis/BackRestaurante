using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Entidades.Sesion;
using Tsp.Sigescom.Modelo.Interfaces.Datos;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;
using Tsp.Sigescom.Modelo.Negocio.Finanza;
using Tsp.Sigescom.Modelo.Negocio.Finanza.Report;
using Tsp.Sigescom.Modelo.Negocio.Core.Actor;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Finanza.PlainModel;

namespace Tsp.Sigescom.Logica.Core.Finanza
{
    public class ArqueoCaja_Logica : IArqueoCaja_Logica
    {
        protected readonly IActorNegocioLogica _actorNegocioLogica;
        protected readonly IOperacionLogica _operacionLogica;
        protected readonly IMaestroRepositorio _maestroDatos;
        protected readonly IFinanzaReporte_Repositorio _finanzaReportingDatos;
        protected readonly ITransaccionRepositorio _transaccionDatos;
        public ArqueoCaja_Logica(IActorNegocioLogica actorNegocioLogica, IMaestroRepositorio maestroDatos, IFinanzaReporte_Repositorio finanzaReportingDatos, IOperacionLogica operacionLogica, ITransaccionRepositorio transaccionDatos)
        {
            _actorNegocioLogica = actorNegocioLogica;
            _maestroDatos = maestroDatos;
            _finanzaReportingDatos = finanzaReportingDatos;
            _operacionLogica = operacionLogica;
            _transaccionDatos = transaccionDatos;
        }

    }
}

using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Tsp.Sigescom.Inyeccion;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.WebApplication.Models;

namespace Tsp.Sigescom.WebApplication.Controllers
{
    /// <summary>
    /// Ventas e Ingresos que tienen concepto detallado
    /// </summary>
    public class InsumosControladosController : BaseController
    {
        private readonly ILibrosElectronicosLogica _librosElectronicosLogica;
        private readonly IActorNegocioLogica _actorNegocioLogica;
        private readonly IOperacionLogica _logicaOperacion;

        public InsumosControladosController()
        {
            _librosElectronicosLogica = Dependencia.Resolve<ILibrosElectronicosLogica>();
            _actorNegocioLogica = Dependencia.Resolve<IActorNegocioLogica>();
            _logicaOperacion = Dependencia.Resolve<IOperacionLogica>();

        }

        

        public ActionResult InsumoControlado()
        {
            return View();
        }
    }
}
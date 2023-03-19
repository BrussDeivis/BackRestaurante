using System;

using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Negocio.Core.Actor;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel
{
    public class PrincipalOrdenAlmacenData
    {
        public ItemGenerico AlmacenSesion { get; set; }
        public List<ItemGenerico> Almacenes { get; set; }

        public DateTime FechaActual { get; set; }

        public string FechaDesdeDefault { get { return FechaActual.ToString("dd/MM/yyyy"); } }
        public string FechaHastaDefault { get { return FechaActual.ToString("dd/MM/yyyy"); } }

        public bool EsAdministrador { get; set; }

    }
}


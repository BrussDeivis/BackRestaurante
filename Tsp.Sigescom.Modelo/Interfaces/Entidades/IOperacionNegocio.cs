using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.Interfaces.Entidades
{
    public interface IOperacionNegocio
    {
        List<AccionOperativa> ObtenerAccionesPosibles();
    }
}

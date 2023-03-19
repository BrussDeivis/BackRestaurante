using System.Collections.Generic;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Custom;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Sesion;

namespace Tsp.Sigescom.Modelo.Negocio.Actores
{
    public interface IGrupoClientes_Logica
    {
        OperationResult CrearGrupoClientes(GrupoClientes grupoClientes);
        OperationResult ActualizarGrupoClientes(GrupoClientes grupoClientes); 
        List<GrupoClientesResumen> ObtenerGruposClientes();
        GrupoClientes ObtenerGrupoClientes(int idGrupoCliente);
        OperationResult DarBajaGrupoClientes(int idGrupoCliente);

    }
}


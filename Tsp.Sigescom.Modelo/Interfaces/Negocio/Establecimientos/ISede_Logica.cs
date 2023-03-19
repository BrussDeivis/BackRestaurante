using System.Collections.Generic;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Custom;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Sesion;

namespace Tsp.Sigescom.Modelo.Negocio.Establecimientos
{
    public interface ISede_Logica
    {
        OperationResult CrearSede(string numeroDcoumentoIdentidad, string codigoEstablecimiento, string codigoEstablecimientoDigemid, string informacionPublicitaria, int idTipoPersona, int idClaseActor, string razonSocial, string nombreComercial, string nombreInterno, string telefono, string correo, Direccion direccion, byte[] foto);

        OperationResult ActualizarSede(int idActor, int idSede, string numeroDcoumentoIdentidad, string codigoEstablecimiento, string codigoEstablecimientoDigemid, string informacionPublicitaria, int idTipoPersona, int idClaseActor, string razonSocial, string nombreComercial, string nombreInterno, string telefono, string correo, Direccion direccion, byte[] foto);


        EstablecimientoComercial ObtenerSede();
        EstablecimientoComercialExtendidoConLogo ObtenerSedeConLogo();
        EstablecimientoComercialExtendido ObtenerSedeExtendida();


    }
}


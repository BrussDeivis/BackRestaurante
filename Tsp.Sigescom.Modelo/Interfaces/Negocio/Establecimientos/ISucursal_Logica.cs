using System.Collections.Generic;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Custom;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Sesion;

namespace Tsp.Sigescom.Modelo.Negocio.Establecimientos
{
    public interface ISucursal_Logica
    {

        OperationResult CrearSucursal(string codigoEstablecimiento, string codigoEstablecimientoDigemid, string informacionPublicitaria, string nombre, string nombreInterno, string telefono, string correo, Direccion direccion, byte[] foto);

        List<Sucursal> ObtenerSucursalesVigentes();

        EstablecimientoComercial ObtenerSucursalComoEstablecimiento(int idSucursal);

        OperationResult DarDeBajaSucursal(int idSucursal);

        OperationResult ActualizarSucursal(int idActor, int idSucursal, string informacionPublicitaria, string codigoEstablecimiento, string codigoEstablecimientoDigemid, string nombre, string nombreInterno, string telefono, string correo, Direccion direccion, byte[] foto);

        //OperationResult EstablecerCentroDeAtencionParaPreciosYStockDeEstablecimientoComercial(int idEstablecimientoComencial, int idCentroDeAtencionPrecios, int idCentroDeAtencionStock);


    }
}


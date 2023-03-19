using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Custom.SigesParking;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Sesion;

namespace Tsp.Sigescom.Modelo.Interfaces.Logica
{
    public interface ICocheraLogica
    {

        List<MovimientoCocheraBasico> ObtenerMovimientosDatosBasicos(DateTime fechaDesde, DateTime fechaHasta, int idCentroAtencion);
        List<EntradaSalida> ObtenerEntradasYSalidas(int idCochera, DateTime desde, DateTime hasta);
        List<EntradaSalidaUsuario> ObtenerVehiculosIngresados(int idCochera);
        List<ExoneracionDeVehiculo> ObtenerVehiculosExonerados(int idCochera);
        List<ExoneracionDeVehiculo> ObtenerVehiculosExoneradosVigentes(int idCochera);
        OperationResult RegistrarIngreso(SesionCochera sesionCochera, Ingreso ingreso);
        OperationResult RegistrarVehiculo(Vehiculo vehiculo);
        OperationResult EditarVehiculo(Vehiculo vehiculo);
        Vehiculo ObtenerVehiculoPorPlaca(string placa);
        List<ItemGenerico> ObtenerSistemasDePago();
        List<ItemGenerico> ObtenerMarcasDeVehiculo();
        List<ItemGenerico> ObtenerTiposDeVehiculo();

        MovimientoCochera ObtenerMovimientoParaSalida(string placaVehiculo, SesionCochera sesionCochera);

        ResultadoRegistroMovimientoCochera RegistrarSalida(MovimientoCochera movimiento, DatosVentaIntegrada datosVenta, SesionCochera SesionCochera);

        //ResultadoRegistroMovimientoCochera RegistrarSalida(MovimientoCochera movimiento, int idCliente, ItemGenerico TipoDeComprobante, ItemGenerico serieComprobante, UserProfileSessionData userProfileData);
        SesionCochera ObtenerSesion(UserProfileSessionData userProfileSessionData);
        OperationResult ExonerarVehiculo(Vehiculo vehiculo, int idCochera);

        OperationResult QuitarExoneracionAVehiculo(ExoneracionDeVehiculo vehiculoExonerado);
        List<ItemGenerico> ObtenerCocheras();
    }
}

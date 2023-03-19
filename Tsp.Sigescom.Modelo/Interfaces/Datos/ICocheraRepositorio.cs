using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Custom.SigesParking;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;//
using Comprobante = Tsp.Sigescom.Modelo.Entidades.Comprobante;

namespace Tsp.Sigescom.Modelo.Interfaces.Repositorio
{
    public interface ICocheraRepositorio : IRepositorioBase
    {
        bool ExisteVehiculo(string placaVehiculo);
        MovimientoCochera ObtenerTransaccion_MovimientoCochera(string placaVehiculo, int idCentroAtencionCochera, int idDetalleMaestroEstadoActual);
        IEnumerable<MovimientoCocheraBasico> ObtenerTransacciones_MovimientosCochera(int id_cochera, DateTime desde, DateTime hasta);
        Vehiculo ObtenerVehiculo(string placaVehiculo);
        ConfiguracionCochera ObtenerConfiguracion(int idCochera);
        List<ExoneracionDeVehiculo> ObtenerExoneracionesVigentes(int idCochera);
        List<ExoneracionDeVehiculo> ObtenerExoneraciones(int idCochera);
        IEnumerable<EntradaSalidaUsuario> ObtenerMovimientos(int id_cochera, int idEstado);
        IEnumerable<EntradaSalida> ObtenerMovimientos(int id_cochera, DateTime desde, DateTime hasta);

    }

}

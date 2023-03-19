using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.SigesHotel.PlainModel
{
    public class RegistroHuesped
    {
        private Estado_transaccion estadoSalida;
        public Estado_transaccion EstadoSalida { set => estadoSalida = value; }
        public DateTime FechaFinAtencion { get; set; }


        public long IdAtencion { get; set; }
        public string Huesped { get; set; }
        public bool EsMasculino { get; set; }
        public string Nacionalidad { get; set; }
        public int IdContinente { get; set; }
        public string Continente { get; set; }
        public int IdPais { get; set; }
        public string Pais { get; set; }
        public int IdUbigeo { get; set; }
        public int IdRegionUbigeo { get; set; }
        public int IdProvinciaUbigeo { get; set; }
        public string Ubigeo { get; set; }
        public string Residencia { get => IdPais == MaestroSettings.Default.IdDetalleMaestroNacionPeru ? Ubigeo.Split('-')[0] : Pais; }
        public string TipoDocumentoCliente { get; set; }
        public string NumeroDocumentoCliente { get; set; }
        public Estado_transaccion EstadoIngresoReferencia { get; set; }
        public Estado_transaccion EstadoIngreso { get; set; }
        public DateTime FechaIngreso { get => EstadoIngreso == null ? EstadoIngresoReferencia.fecha : EstadoIngreso.fecha; }
        public DateTime FechaSalida { get => estadoSalida == null ? FechaFinAtencion : estadoSalida.fecha; }
        public int IdTipoHabitacion { get; set; }
        public string TipoHabitacion { get; set; }
        public string CodigoHabitacion { get; set; }
        public decimal ImporteTotal { get; set; }
        public decimal Tarifa { get; set; }
        public int Arribos { get => 1; }
        public int Noches { get; set; }
        public int Pernoctaciones { get => Noches; }
        public int IdMotivoViaje { get; set; }
        public int EsMotivoVacaciones { get => IdMotivoViaje == HotelSettings.Default.IdDetalleMaestroMotivoDeViajeVacaciones ? 1 : 0; }
        public int EsMotivoVisita { get => IdMotivoViaje == HotelSettings.Default.IdDetalleMaestroMotivoDeViajeVisita ? 1 : 0; }
        public int EsMotivoEducacion { get => IdMotivoViaje == HotelSettings.Default.IdDetalleMaestroMotivoDeViajeEducacion ? 1 : 0; }
        public int EsMotivoSalud { get => IdMotivoViaje == HotelSettings.Default.IdDetalleMaestroMotivoDeViajeSalud ? 1 : 0; }
        public int EsMotivoReligion { get => IdMotivoViaje == HotelSettings.Default.IdDetalleMaestroMotivoDeViajeReligion ? 1 : 0; }
        public int EsMotivoCompras { get => IdMotivoViaje == HotelSettings.Default.IdDetalleMaestroMotivoDeViajeCompras ? 1 : 0; }
        public int EsMotivoNegocios { get => IdMotivoViaje == HotelSettings.Default.IdDetalleMaestroMotivoDeViajeNegocios ? 1 : 0; }
        public int EsMotivoTrabajo { get => IdMotivoViaje == HotelSettings.Default.IdDetalleMaestroMotivoDeViajeTrabajo ? 1 : 0; }
        public int EsMotivoOtros { get => IdMotivoViaje == HotelSettings.Default.IdDetalleMaestroMotivoDeViajeOtros ? 1 : 0; }

        public List<RegistroHuesped> Convert()
        {
            return new List<RegistroHuesped>();
        }
    }
}

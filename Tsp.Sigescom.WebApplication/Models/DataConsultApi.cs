using System;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models
{
    public class DataActorApi
    {
        public string NumeroDocumentoIdentidad { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Nombres { get; set; }
        public string RazonSocial { get; set; }
        public string NombreComercial { get; set; }
        public string Ubigeo { get; set; }
        public string Direccion { get; set; }
        public string Sexo { get; set; }
        public string TipoContribuyente { get; set; }
        public bool Estado { get; set; }

        public DataActorApi()
        {
        }
        public ActorComercial_ ConvertConDNI(string dni)
        {
            return ConvertirConDni(dni);
        }
        public RegistroActorComercial ConvertirConDni(string dni)
        {
            return new RegistroActorComercial()
            {
                NumeroDocumentoIdentidad = dni,
                ApellidoPaterno = this.ApellidoPaterno,
                ApellidoMaterno = this.ApellidoMaterno,
                Nombres = this.Nombres,
                NombreORazonSocial = this.Nombres + " " + this.ApellidoPaterno + " " + this.ApellidoMaterno,
                NombreComercial = this.Nombres + " " + this.ApellidoPaterno + " " + this.ApellidoMaterno,
                TipoDocumentoIdentidad = new ItemGenerico(ActorSettings.Default.IdTipoDocumentoIdentidadDni),
                TipoPersona = new ItemGenerico(ActorSettings.Default.IdTipoActorPersonaNatural),
                DomicilioFiscal = new Direccion_ { Pais = new ItemGenerico() { Id = MaestroSettings.Default.IdDetalleMaestroNacionPeru, Nombre = MaestroSettings.Default.NombreDetalleMaestroNacionPeru }, Ubigeo = new ItemGenerico() { Id = ActorSettings.Default.IdUbigeoSeleccionadoPorDefecto, Nombre = ActorSettings.Default.NombreUbigeoSeleccionadoPorDefecto }, Detalle = this.Direccion }
            };
        }
        public ActorComercial_ ConvertConRUC(string ruc)
        {
            return ConvertirConDni(ruc);
        }
        public RegistroActorComercial ConvertirConRuc(string ruc)
        {
            return new RegistroActorComercial()
            {
                NumeroDocumentoIdentidad = ruc,
                ApellidoPaterno = this.ApellidoPaterno,
                ApellidoMaterno = this.ApellidoMaterno,
                Nombres = this.Nombres,
                NombreORazonSocial = this.RazonSocial,
                NombreComercial = ruc.StartsWith("10") ? this.Nombres + " " + this.ApellidoPaterno + " " + this.ApellidoMaterno : this.NombreComercial,
                TipoDocumentoIdentidad = new ItemGenerico(ActorSettings.Default.IdTipoDocumentoIdentidadRuc),
                TipoPersona = new ItemGenerico() { Id = ruc.StartsWith("10") ? ActorSettings.Default.IdTipoActorPersonaNatural : ActorSettings.Default.IdTipoActorPersonaJuridica },
                DomicilioFiscal = ruc.StartsWith("20") ? new Direccion_ { Ubigeo = new ItemGenerico() { Id = Int32.Parse(this.Ubigeo) }, Detalle = this.Direccion } : new Direccion_ { Ubigeo = new ItemGenerico() { Id = ActorSettings.Default.IdUbigeoSeleccionadoPorDefecto }, Detalle = this.Direccion }
            };
        }
    }
     
}
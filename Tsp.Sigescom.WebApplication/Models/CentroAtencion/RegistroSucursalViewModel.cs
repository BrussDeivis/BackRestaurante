using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models.Comprobante
{
    public class RegistroSucursalViewModel
    {
        public int  Id { get; set; }
        public int IdActor { get; set; }
        public string CodigoEstablecimiento { get; set; }
        public string InformacionPublicitaria { get; set; }
        public string Nombre { get; set; }
        public string NombreInterno { get; set; }// Es el nombre con el que se conoce al establecimiento comercial, Ejm: Razon social : Comercial ABC EIRL, Nombre comercial: Comercial ABC, Nombre Interno: Tienda Principal Tingo
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public DireccionViewModel Direccion  { get; set; }
        public FotoViewModel Foto { get; set; }
        public int? IdCentroDeAtencionParaObtencionPrecios { get; set; }
        public int? IdCentroDeAtencionParaObtencionStock { get; set; }
        public string CodigoEstablecimientoDigemid { get; set; }

        public RegistroSucursalViewModel()
        {

        }

        public RegistroSucursalViewModel(EstablecimientoComercialExtendidoConLogo sucursal)
        {
            this.Id = sucursal.Id;
            this.IdActor = sucursal.IdActor;
            this.CodigoEstablecimiento = sucursal.Codigo;
            this.InformacionPublicitaria = sucursal.InformacionPublicitaria;
            this.Nombre = sucursal.Nombre;
            this.NombreInterno = sucursal.NombreInterno;
            this.Telefono = sucursal.Telefono;
            this.Correo = sucursal.Correo;
            this.Direccion = new DireccionViewModel(sucursal.Domicilio);
            this.Foto = new FotoViewModel(sucursal.TieneLogo, sucursal.Logo);
            this.IdCentroDeAtencionParaObtencionPrecios = sucursal.IdCentroDeAtencionParaObtencionDePrecios;
            this.IdCentroDeAtencionParaObtencionStock = sucursal.IdCentroDeAtencionParaObtencionDeStock;
            if (ActorSettings.Default.PermitirRegistroCodigoDigemidEnEstableciemientoComercial) this.CodigoEstablecimientoDigemid = sucursal.CodigoEstablecimientoDigemid;
        }

    }


    public class BandejaSucursalViewModel
    {
        [DataMember]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string TipoDeDocumento { get; set; }
        public string NumeroDocumentoDeIdentidad { get; set; }
        public string Direccion { get; set; }
        public bool Vigente { get; set; }

        public BandejaSucursalViewModel()
        {

        }

        public BandejaSucursalViewModel(EstablecimientoComercialExtendido sucursal)
        {
            this.Id = sucursal.Id;
            this.Nombre = sucursal.Nombre;
            this.TipoDeDocumento = sucursal.NombreTipoDocumento;
            this.NumeroDocumentoDeIdentidad = sucursal.DocumentoIdentidad;
            if (sucursal.DomicilioFiscal != null)
            {
                this.Direccion = DireccionViewModel.Direccion(sucursal.DomicilioFiscal);
            }
             
        }

        public static List<BandejaSucursalViewModel> Convert(List<Sucursal> sucursales)
        {
            List<BandejaSucursalViewModel> sucursalViewModel = new List<BandejaSucursalViewModel>();
            foreach (var item in sucursales)
            {
                sucursalViewModel.Add(new BandejaSucursalViewModel(item));
            }
            return sucursalViewModel;
        }
    }
}
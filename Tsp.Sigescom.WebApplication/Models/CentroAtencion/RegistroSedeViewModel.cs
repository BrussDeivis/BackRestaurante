using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.WebApplication.Models.Comprobante
{
    public class RegistroSedeViewModel
    {
        public int  Id { get; set; }
        public int IdActor { get; set; }
        public string NumeroDocumentoIdentidad { get; set; }
        public string CodigoEstablecimiento { get; set; }
        public string InformacionPublicitaria { get; set; }
        public ComboGenericoViewModel TipoPersona { get; set; }
        public ComboGenericoViewModel ClaseActor { get; set; }
        public string RazonSocial { get; set; }
        public string NombreComercial { get; set; }
        public string NombreInterno { get; set; }// Es el nombre con el que se conoce al establecimiento comercial, Ejm: Razon social : Comercial ABC EIRL, Nombre comercial: Comercial ABC, Nombre Interno: Tienda Principal Tingo
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public DireccionViewModel Direccion  { get; set; }
        public FotoViewModel Foto { get; set; }
        public int? IdCentroDeAtencionParaObtencionPrecios { get; set; }
        public int? IdCentroDeAtencionParaObtencionStock { get; set; }
        public string CodigoEstablecimientoDigemid { get; set; }


        public RegistroSedeViewModel()
        {

        }

        public RegistroSedeViewModel(EstablecimientoComercialExtendidoConLogo sede)
        {
            this.Id = sede.Id;
            this.IdActor = sede.IdActor;
            this.NumeroDocumentoIdentidad = sede.DocumentoIdentidad;
            this.CodigoEstablecimiento = sede.Codigo;
            this.InformacionPublicitaria = sede.InformacionPublicitaria;
            this.TipoPersona = new ComboGenericoViewModel(sede.IdTipoPersona, sede.TipoPersona);
            this.ClaseActor = new ComboGenericoViewModel(sede.IdClaseActor, sede.ClaseActor);
            this.RazonSocial = sede.Nombre;
            this.NombreComercial = sede.NombreComercial;
            this.NombreInterno = sede.NombreInterno;
            this.Telefono = sede.Telefono;
            this.Correo = sede.Correo;
            this.Direccion = new DireccionViewModel(sede.Domicilio);
            this.Foto = new FotoViewModel(sede.TieneLogo, sede.Logo);
            this.IdCentroDeAtencionParaObtencionPrecios = sede.IdCentroDeAtencionParaObtencionDePrecios;
            this.IdCentroDeAtencionParaObtencionStock = sede.IdCentroDeAtencionParaObtencionDeStock;
            if (ActorSettings.Default.PermitirRegistroCodigoDigemidEnEstableciemientoComercial) this.CodigoEstablecimientoDigemid = sede.CodigoEstablecimientoDigemid;
        }

    }


}
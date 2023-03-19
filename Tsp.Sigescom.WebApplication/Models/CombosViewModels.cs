using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Mvc;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.Custom;

namespace Tsp.Sigescom.WebApplication.Models
{
    public class ComboGenericoViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public ComboGenericoViewModel()
        {

        }

        public ComboGenericoViewModel(int id, string nombre)
        {
            this.Id = id;
            this.Nombre = nombre.ToUpper();
        }

        public static List<ComboGenericoViewModel> Convert(List<CentroDeAtencion> actores)
        {
            List<ComboGenericoViewModel> comboGenericoActores = new List<ComboGenericoViewModel>();
            foreach (var actor in actores)
            {
                comboGenericoActores.Add(new ComboGenericoViewModel(actor.Id, actor.Nombre));
            }
            return comboGenericoActores;
        }

        public static List<ComboGenericoViewModel> Convert(List<CentroDeAtencionExtendido> centrosDeAtencion)
        {
            List<ComboGenericoViewModel> comboGenericoActores = new List<ComboGenericoViewModel>();
            foreach (var item in centrosDeAtencion)
            {
                comboGenericoActores.Add(new ComboGenericoViewModel(item.Id, item.Nombre));
            }
            return comboGenericoActores;
        }

        public static List<ComboGenericoViewModel> ConvertirConCodigo(List<CentroDeAtencionExtendido> centrosDeAtencion)
        {
            List<ComboGenericoViewModel> comboGenericoActores = new List<ComboGenericoViewModel>();
            foreach (var item in centrosDeAtencion)
            {
                comboGenericoActores.Add(new ComboGenericoViewModel(item.Id, item.Codigo + " | "+ item.Nombre));
            }
            return comboGenericoActores;
        }

        public static List<ComboGenericoViewModel> Convert(List<Sucursal> sucursales)
        {
            List<ComboGenericoViewModel> comboGenericoSucursales = new List<ComboGenericoViewModel>();
            foreach (var item in sucursales)
            {
                comboGenericoSucursales.Add(new ComboGenericoViewModel(item.Id, item.NombreInterno));
            }
            return comboGenericoSucursales;
        }

        public static List<ComboGenericoViewModel> Convert(List<EstablecimientoComercialExtendido> establecimientos)
        {
            List<ComboGenericoViewModel> comboGenerico = new List<ComboGenericoViewModel>();
            foreach (var item in establecimientos)
            {
                comboGenerico.Add(new ComboGenericoViewModel(item.Id, item.NombreInterno));
            }
            return comboGenerico;
        }

        public static List<ComboGenericoViewModel> Convert(List<TipoDeTransaccion> tiposTransaccion)
        {
            List<ComboGenericoViewModel> comboGenericoTiposTransaccion = new List<ComboGenericoViewModel>();
            foreach (var item in tiposTransaccion)
            {
                comboGenericoTiposTransaccion.Add(new ComboGenericoViewModel(item.Id, item.Nombre));
            }
            return comboGenericoTiposTransaccion;
        }

        public static List<ComboGenericoViewModel> Convert(List<TipoDeComprobante> tiposDeComprobante)
        {
            List<ComboGenericoViewModel> comboGenericoTiposDeComprobante = new List<ComboGenericoViewModel>();
            foreach (var item in tiposDeComprobante)
            {
                comboGenericoTiposDeComprobante.Add(new ComboGenericoViewModel(item.Id, item.Nombre));
            }
            return comboGenericoTiposDeComprobante;
        }

        public static List<ComboGenericoViewModel> Convert(List<RolDeNegocio> rolesDeNegocio)
        {
            List<ComboGenericoViewModel> comboGenericoRolDeNegocio = new List<ComboGenericoViewModel>();
            foreach (var item in rolesDeNegocio)
            {
                comboGenericoRolDeNegocio.Add(new ComboGenericoViewModel(item.Id, item.Nombre));
            }
            return comboGenericoRolDeNegocio;
        }
        public static List<ComboGenericoViewModel> Convert(List<Empleado> empleados)
        {
            List<ComboGenericoViewModel> comboGenericoEmpleados = new List<ComboGenericoViewModel>();
            foreach (var item in empleados)
            {
                comboGenericoEmpleados.Add(new ComboGenericoViewModel(item.Id, item.NombreCompleto));
            }
            return comboGenericoEmpleados;
        }

        public static List<ComboGenericoViewModel> Convert(List<Cliente> clientes)
        {
            List<ComboGenericoViewModel> comboGenericoClientes = new List<ComboGenericoViewModel>();
            foreach (var item in clientes)
            {
                comboGenericoClientes.Add(new ComboGenericoViewModel(item.Id, item.RazonSocial + " " + item.DocumentoIdentidad));
            }
            return comboGenericoClientes;
        }

        public static List<ComboGenericoViewModel> Convert(List<ConceptoDeNegocio> conceptosDeNegocio)
        {
            List<ComboGenericoViewModel> comboGenericoConceptosDeNegocio = new List<ComboGenericoViewModel>();
            foreach (var conceptoDeNegocio in conceptosDeNegocio)
            {
                comboGenericoConceptosDeNegocio.Add(new ComboGenericoViewModel(conceptoDeNegocio.Id, conceptoDeNegocio.CodigoBarra + "  | " + conceptoDeNegocio.Nombre));
            }
            return comboGenericoConceptosDeNegocio;
        }

        public static List<ComboGenericoViewModel> Convert(List<DetalleGenerico> detalles)
        {
            List<ComboGenericoViewModel> comboGenericoDetalles = new List<ComboGenericoViewModel>();
            foreach (var detalle in detalles)
            {
                comboGenericoDetalles.Add(new ComboGenericoViewModel(detalle.Id, detalle.Nombre));
            }
            return comboGenericoDetalles;
        }

        public static List<ComboGenericoViewModel> Convert(List<Detalle_maestro> detalles)
        {
            List<ComboGenericoViewModel> comboGenericoDetalles = new List<ComboGenericoViewModel>();
            foreach (var detalle in detalles)
            {
                comboGenericoDetalles.Add(new ComboGenericoViewModel(detalle.id, detalle.nombre));
            }
            return comboGenericoDetalles;
        }

        public static List<ComboGenericoViewModel> ConvertirCentroDeAtencionConEstablecimientoComercial(List<CentroDeAtencionExtendido> centrosDeAtencion)
        {
            List<ComboGenericoViewModel> comboGenericoActores = new List<ComboGenericoViewModel>();
            foreach (var item in centrosDeAtencion)
            {
                comboGenericoActores.Add(new ComboGenericoViewModel(item.Id, item.EstablecimientoComercial.NombreInterno + " | " + item.Nombre));
            }
            return comboGenericoActores;
        }
        public static List<ComboGenericoViewModel> Convert(List<TipoDeComprobanteParaTransaccion> tipoDeComprobante)
        {
            List<ComboGenericoViewModel> comboGenericoActores = new List<ComboGenericoViewModel>();
            foreach (var tipoComprobante in tipoDeComprobante)
            {
                comboGenericoActores.Add(new ComboGenericoViewModel(tipoComprobante.Id, tipoComprobante.Nombre));
            }
            return comboGenericoActores;
        }
        public static List<ComboGenericoViewModel> Convert(List<CuentaBancaria> cuentasBancarias)
        {
            List<ComboGenericoViewModel> comboGenericoActores = new List<ComboGenericoViewModel>();
            foreach (var item in cuentasBancarias)
            {
                comboGenericoActores.Add(new ComboGenericoViewModel(item.Id, item.NumeroCuenta + " | " + item.Moneda.Nombre ));
            }
            return comboGenericoActores;
        }
    }




    public class ComboDocumentoIdentidadViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        //public int LongitudCaracteres { get; set; }

        public ComboDocumentoIdentidadViewModel()
        {

        }
        public ComboDocumentoIdentidadViewModel(int id, string nombre)
        {
            this.Id = id;
            this.Nombre = nombre;
            //this.LongitudCaracteres = longitud;
        }
    }

   


    public class ValorDeCaracteristicaConceptoNegocioViewModel
    {
        public int IdCaracteristica { get; set; }
        public int IdValor { get; set; }
        public string NombreValor { get; set; }

        public ValorDeCaracteristicaConceptoNegocioViewModel()
        {

        }
        public ValorDeCaracteristicaConceptoNegocioViewModel(int idCaracteristica, int idValor, string nombreValor)
        {
            this.IdCaracteristica = idCaracteristica;
            this.IdValor = idValor;
            this.NombreValor = nombreValor;
        }

        public ValorDeCaracteristicaConceptoNegocioViewModel(Valor_caracteristica_concepto_negocio valor)
        {
            this.IdCaracteristica = valor.Valor_caracteristica.id_caracteristica;
            this.IdValor = valor.id_valor_caracteristica;
            this.NombreValor = valor.Valor_caracteristica.valor;
        }

        public static List<ValorDeCaracteristicaConceptoNegocioViewModel> Convert(List<Valor_caracteristica_concepto_negocio> valores)
        {
            var valoresViewModel = new List<ValorDeCaracteristicaConceptoNegocioViewModel>();
            foreach (var valor in valores)
            {
                valoresViewModel.Add(new ValorDeCaracteristicaConceptoNegocioViewModel(valor));
            }
            return valoresViewModel;
        }

    }

    public class DetalleMaestroValorCaracteristicaViewModel
    {

        public string NombreValor { get; set; }
        public string NombreCaracteristica { get; set; }

        public DetalleMaestroValorCaracteristicaViewModel()
        {

        }

        public DetalleMaestroValorCaracteristicaViewModel(string nombreCaracteristica, string nombreValor)
        {
            this.NombreValor = nombreValor;
            this.NombreCaracteristica = nombreCaracteristica;
        }

        /// <summary>
        /// Este metodo es para mostrar las caracteristicas , si no hay caracteristicas se completa con espacios en blancos
        /// </summary>
        /// <param name="valores"></param>
        /// <param name="caracteristicas"></param>
        /// <returns></returns>
        public static List<DetalleMaestroValorCaracteristicaViewModel> Match(List<ValorDeCaracteristicaConceptoNegocioViewModel> valores, List<ComboGenericoViewModel> caracteristicas)
        {
            var valoresViewModel = new List<DetalleMaestroValorCaracteristicaViewModel>();
            if (valores.Count() == 0)
            {
                foreach (var item in caracteristicas)
                {
                    valoresViewModel.Add(new DetalleMaestroValorCaracteristicaViewModel(item.Nombre, ""));
                }

                return valoresViewModel;
            }
            else
            {
                foreach (var item in caracteristicas)
                {
                    int index = valores.FindIndex(v => v.IdCaracteristica == item.Id);

                    if (index >= 0)
                    {
                        valoresViewModel.Add(new DetalleMaestroValorCaracteristicaViewModel(item.Nombre, valores[index].NombreValor));
                    }
                    else
                    {
                        valoresViewModel.Add(new DetalleMaestroValorCaracteristicaViewModel(item.Nombre, ""));
                    }
                }
                return valoresViewModel;
            }

        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Almacen.PlainModel;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom.Partial;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Entidades.Sesion;
using Tsp.Sigescom.Modelo.Interfaces.Datos;
using Tsp.Sigescom.Modelo.Interfaces.Logica;
using Tsp.Sigescom.Modelo.Interfaces.Repositorio;
using Tsp.Sigescom.Modelo.Negocio.LibrosElectronicos;
using Tsp.Sigescom.Modelo.Negocio.Finanza.Report;
using Tsp.Sigescom.Modelo.Negocio.Core.Actor;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.LibrosElectronicos.PlainModel;

namespace Tsp.Sigescom.Logica
{
    public class LibrosElectronicosFoxcontLogica : ILibrosElectronicosFoxcontLogica
    {
        protected readonly ILibrosElectronicosFoxcontRepositorio _librosElectronicosFoxcontDatos;
        public LibrosElectronicosFoxcontLogica(ILibrosElectronicosFoxcontRepositorio librosElectronicosFoxcontDatos)
        {
            _librosElectronicosFoxcontDatos = librosElectronicosFoxcontDatos;
        }

        public List<ReporteVentaClienteFoxcom> ObtenerVentaClienteFoxcomBoletaVentaFactura(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                var ventasClientesFoxcom = _librosElectronicosFoxcontDatos.ObtenerVentasClienteFoxcom(Diccionario.TiposDeComprobanteTributablesExceptoNotasDeCreditoYDebito, new int[] { TransaccionSettings.Default.IdTipoTransaccionOrdenDeVenta }, fechaDesde, fechaHasta).ToList();
                var ventasFoxcont = ventasClientesFoxcom.OrderBy(v => v.FechaEmision).ToList();
                var reporteFoxcont = ReporteVentaClienteFoxcom.Convert(ventasFoxcont);
                return reporteFoxcont;
            }
            catch (Exception e)
            {
                throw new LogicaException("No se pudo obtener las ventas", e);
            }
        }

        public List<ReporteVentaClienteFoxcom> ObtenerVentaClienteFoxcomNotaCreditoDebito(int idEmpleado, DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                var ventasClientesFoxcom = _librosElectronicosFoxcontDatos.ObtenerVentasClienteFoxcomConOperacionReferencia(Diccionario.TiposDeComprobanteTributablesParaNotasDeCreditoYDebito, Diccionario.TiposDeTransaccionOrdenesDeOperacionesDeVentasSoloNotasDeCreditoYDebito, fechaDesde, fechaHasta).ToList();
                var ventasFoxcont = ventasClientesFoxcom.OrderBy(v => v.FechaEmision).ToList();
                var reporteFoxcont = ReporteVentaClienteFoxcom.Convert(ventasFoxcont);
                return reporteFoxcont;
            }
            catch (Exception e)
            {
                throw new LogicaException("No se pudo obtener las ventas", e);
            }
        }
    }
}

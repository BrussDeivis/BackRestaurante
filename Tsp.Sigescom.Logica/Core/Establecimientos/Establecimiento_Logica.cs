using System;
using System.Collections.Generic;
using System.Linq;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Generico;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
using Tsp.Sigescom.Modelo.Interfaces.Datos.Establecimientos;
using Tsp.Sigescom.Modelo.Negocio.Establecimientos;

namespace Tsp.Sigescom.Logica.Core.Establecimientos
{
    public class Establecimiento_Logica : IEstablecimiento_Logica
    {
        private readonly IEstablecimiento_Repositorio _establecimientoRepositorio;


        public Establecimiento_Logica(IEstablecimiento_Repositorio establecimientoRepositorio)
        {
            _establecimientoRepositorio = establecimientoRepositorio;
        }


        public List<EstablecimientoComercial> ObtenerEstablecimientosComercialesVigentes()
        {
            try
            {
                return _establecimientoRepositorio.ObtenerEstablecimientosComercialesVigentes().ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<EstablecimientoComercialExtendido> ObtenerEstablecimientosComercialesExtendidosVigentes()
        {
            try
            {
                return _establecimientoRepositorio.ObtenerEstablecimientosComercialesExtendidosVigentes().ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<ItemGenerico> ObtenerEstablecimientosComercialesVigentesComoItemsGenericos()
        {
            try
            {
                return _establecimientoRepositorio.ObtenerEstablecimientosComercialesVigentesComoItemsGenericos().ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener establecimientos comerciales", e);
            }
        }

        public List<ItemGenericoConSubItems> ObtenerEstablecimientosComercialesVigentesConSusAlmacenes()
        {
            try
            {
                return _establecimientoRepositorio.ObtenerEstablecimientosConSusCentrosDeAtencionVigentesSegunRol(ActorSettings.Default.IdRolAlmacen).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener establecimientos comerciales", e);
            }
        }

        public List<ItemGenericoConSubItems> ObtenerEstablecimientosComercialesVigentesConSusCajas()
        {
            try
            {
                return _establecimientoRepositorio.ObtenerEstablecimientosConSusCentrosDeAtencionVigentesSegunRol(ActorSettings.Default.IdRolCaja).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener establecimientos comerciales", e);
            }
        }
        public List<ItemGenericoConSubItems> ObtenerEstablecimientosComercialesVigentesConSusPuntosVentas()
        {
            try
            {
                return _establecimientoRepositorio.ObtenerEstablecimientosConSusCentrosDeAtencionVigentesSegunRol(ActorSettings.Default.IdRolPuntaDeVenta).ToList();
            }
            catch (Exception e)
            {
                throw new LogicaException("Error al intentar obtener establecimientos comerciales", e);
            }
        }
    }
}

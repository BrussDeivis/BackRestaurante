using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Establecimientos;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.Sesion;
using Tsp.Sigescom.Modelo.ClasesNegocio.Core.TipoCambio;
using Tsp.Sigescom.Modelo.Custom;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.Entidades.Sesion
{
    [Serializable]
    public class UserProfileSessionData
    {
        public string MensajeError { get; set; }
        public string IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public Empleado_ Empleado { get; set; }
        public List<CentroDeAtencion> CentrosDeAtencionProgramados { get; set; }
        public int IdCentroDeAtencionSeleccionado { get { return CentroDeAtencionSeleccionado != null ? CentroDeAtencionSeleccionado.Id : 0; } }
        public int IdCentroDeAtencionInicioSesion { get; set; }
        public CentroDeAtencion CentroDeAtencionSeleccionado { get; set; }
        public EstablecimientoComercialExtendidoConLogo EstablecimientoComercialSeleccionado { get; set; }
        public EstablecimientoComercialExtendidoConLogo Sede { get; set; }
        public ActorComercial_ ClientePorDefecto { get; set; }
        public Operacion OperacionSesionContenedora { get; set; }
        public int IdCentroAtencionQueTieneLosPrecios { get; set; }
        public CentroDeAtencion CentroAtencionQueTieneElStockIntegrada { get; set; }
        public int IdCentroAtencionQueTieneElStockIntegrada { get; set; }
        public int IdCentroAtencionQueTieneElStockDosPasos { get; set; }
        public int IdCentroAtencionQueTieneElStockCorporativa { get; set; }
        public string NombreSede { get; set; }
        public decimal CostoUnitarioDelIcbper { get; set; }
        public TipoCambio TipoDeCambio { get; set; }
        public MaestroSesion MaestrosFrecuentes { get; set; }
        public MensajesTransaccion MensajesTransaccion { get; set; }
        public int IdMonedaPorDefecto()
        {
            return MaestroSettings.Default.IdDetalleMaestroMonedaSoles;
        }
        public int IdUnidadDeNegocioPorDefecto()
        {
            return MaestroSettings.Default.IdDetalleMaestroUnidadDeNegocioTransversal;
        }
        /// <summary>
        /// Key: idAlmacen, Value: idTransaccionInventarioActual
        /// </summary>
        public Dictionary<long, long> AlmacenesInventariosActuales { get; set; }

        public long ObtenerIdInventarioActual(int idAlmacen)
        {
            return this.AlmacenesInventariosActuales.SingleOrDefault(aif => aif.Key == idAlmacen).Value;

        }

        public string NombreCentroDeAtencionSeleccionado
        {
            get { return CentroDeAtencionSeleccionado == null ? NombreSede : CentroDeAtencionSeleccionado.Nombre; }
        }

        public void SetCentrosDeAtencionProgramados(List<CentroDeAtencion> centrosDeAtencion)
        {
            CentrosDeAtencionProgramados = centrosDeAtencion;
        }

        public void SetIdAlmacenIdInventarioFisico(Dictionary<long, long> idsAlmacenIdsInventarioFisico)
        {
            AlmacenesInventariosActuales = new Dictionary<long, long>();
            foreach (var idAlmacenIdInventarioFisico in idsAlmacenIdsInventarioFisico)
            {
                AlmacenesInventariosActuales.Add(idAlmacenIdInventarioFisico.Key, idAlmacenIdInventarioFisico.Value);
            }
        }
    }
}

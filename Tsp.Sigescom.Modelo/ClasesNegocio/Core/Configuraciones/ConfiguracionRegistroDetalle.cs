using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo
{
    public sealed class ConfiguracionRegistroDetalle
    {
        public readonly bool AplicaLeyAmazonia = TransaccionSettings.Default.AplicaLeyAmazonia;
        public readonly decimal TasaIGV = TransaccionSettings.Default.TasaIGV;
        public bool SalidaBienesSujetasADisponibilidadStock;
        public bool PermitirIngresarCantidad;
        public bool PermitirIngresarPrecioUnitario;
        public bool PermitirIngresarImporte;
        public bool IngresarCantidadCalcularPrecioUnitario;
        public bool IngresarPrecioUnitarioCalcularImporte;
        public bool IngresarImporteCalcularCantidad;
        public readonly bool PermitirRegistroFlete = AplicacionSettings.Default.PermitirRegistroFleteEnVenta;
        public readonly int IdFamiliaBolsaPlastica = MaestroSettings.Default.IdDetalleMaestroConceptoBasicoBolsaPlastica;
        public decimal CostoUnitarioDelIcbper;
        public readonly int IdTarifaSeleccionadoPorDefecto = VentasSettings.Default.IdTarifaSeleccionadoPorDefecto;
        public readonly int NumeroDecimalesEnCantidad = AplicacionSettings.Default.NumeroDecimalesEnCantidad;
        public readonly int NumeroDecimalesEnPrecio = AplicacionSettings.Default.NumeroDecimalesEnPrecio;
        public readonly int ModoSeleccionTipoFamilia = AplicacionSettings.Default.ModoDeSeleccionTipoDeFamiliaEnVentas;
        public readonly bool MostrarBuscadorCodigoBarra = VentasSettings.Default.ModoDeIngresoDeCodigoDeBarraEnVenta == (int)ModoIngresoCodigoBarraEnVenta.CodigoBarraDeProducto || VentasSettings.Default.ModoDeIngresoDeCodigoDeBarraEnVenta == (int)ModoIngresoCodigoBarraEnVenta.Ambos;
        public readonly int ModoSeleccionConcepto = VentasSettings.Default.ModoDeSeleccionDeConceptoDeNegocio;
        public readonly int TiempoEsperaBusquedaSelector = AplicacionSettings.Default.TiempoDeEsperaEnBusquedaSelectoresDeGranCantidad;
        public readonly int MinimoCaracteresBuscarConcepto = AplicacionSettings.Default.MinimoDeCaracteresParaBuscarEnSelectorConcepto;
        public readonly bool AplicarCantidadPorDefecto = AplicacionSettings.Default.AplicarCantidadPorDefectoEnVentas;
        public readonly string CantidadPorDefecto = AplicacionSettings.Default.CantidadPorDefectoEnVentas;
        public readonly string MascaraDeCalculoPorDefecto = VentasSettings.Default.MascaraDeCalculoPorDefectoEnVentas;
        public readonly string MascaraDeCalculoPrecioUnitarioCalculado = VentasSettings.Default.MascaraDeCalculoPrecioUnitarioCalculado;
        public readonly int InformacionSelectorConcepto = VentasSettings.Default.InformacionSelectorConceptoEnVentas;
        public readonly int FlujoDespuesDeCodigoBarraEnVenta = VentasSettings.Default.FlujoDespuesDeCodigoBarraEnVenta;
        public readonly int FlujoDespuesDeImporteEnVenta = VentasSettings.Default.FlujoDespuesDeImporteEnVenta;

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades.Custom;

namespace Tsp.Sigescom.Modelo.Entidades
{
    public class ConfiguracionNota
    {
        public readonly int IdDetalleMaestroAnulacionDeLaOperacion = MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaAnulacionDeLaOperacion;
        public readonly int IdDetalleMaestroDescuentoGlobal = MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaDescuentoGlobal;
        public readonly int IdDetalleMaestroDescuentoPorItem = MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaDescuentoPorItem;
        public readonly int IdDetalleMaestroDevolucionTotal = MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaDevolucionTotal;
        public readonly int IdDetalleMaestroDevolucionPorItem = MaestroSettings.Default.IdDetalleMaestroNotaDeCreditoElectronicaDevolucionPorItem;
        public readonly int IdDetalleMaestroInteresesPorMora = MaestroSettings.Default.IdDetalleMaestroNotaDeDebitoElectronicaInteresesPorMora;
        public readonly int IdDetalleMaestroAumentoEnElValor = MaestroSettings.Default.IdDetalleMaestroNotaDeDebitoElectronicaAumentoEnElValor;
        public readonly int NumeroDecimalesEnCantidad = AplicacionSettings.Default.NumeroDecimalesEnCantidad;
        public readonly int NumeroDecimalesEnPrecio = AplicacionSettings.Default.NumeroDecimalesEnPrecio;
        public readonly bool MostrarSeccionEntregaEnVenta = VentasSettings.Default.MostrarSeccionEntregaEnVenta;
        public readonly decimal TasaIGV = TransaccionSettings.Default.TasaIGV;
 
       
    }
}

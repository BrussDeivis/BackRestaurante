using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades
{

    public class ConfiguracionRegistroActorComercial
    {

        public readonly int IdTipoActorPersonaNatural = ActorSettings.Default.IdTipoActorPersonaNatural;
        public readonly int IdTipoActorPersonaJuridica = ActorSettings.Default.IdTipoActorPersonaJuridica;
        public readonly int IdTipoDocumentoIdentidadDni = ActorSettings.Default.IdTipoDocumentoIdentidadDni;
        public readonly int IdTipoDocumentoIdentidadRuc = ActorSettings.Default.IdTipoDocumentoIdentidadRuc;
        public readonly int IdTipoTipoDocumentoSeleccionadaPorDefecto = ActorSettings.Default.IdTipoDocumentoIdentidadDni;
        public readonly int IdUbigeoSeleccionadoPorDefecto = ActorSettings.Default.IdUbigeoSeleccionadoPorDefecto;
        public readonly int IdUbigeoNoEspecificado = ActorSettings.Default.idUbigeoNoEspecificado;
        public readonly bool PermitirComprobantePorDefectoEnCliente = ActorSettings.Default.PermitirComprobantePorDefectoEnCliente;
        public readonly int IdComprobantePredeterminadoPorDefecto = MaestroSettings.Default.IdDetalleMaestroComprobanteNotaVentaInterna;
        public readonly int IdRolCliente = ActorSettings.Default.IdRolCliente;
        public readonly int IdRolProveedor = ActorSettings.Default.IdRolProveedor;
        public readonly int IdRolEmpleado = ActorSettings.Default.IdRolEmpleado;
        public readonly bool MostrarBotonCargarActorEnRegistroActorComercial = ActorSettings.Default.MostrarBotonCargarActorEnRegistroActorComercial;
        public readonly int IdNacionPeru = MaestroSettings.Default.IdDetalleMaestroNacionPeru;
        public string FechaActual;
        public readonly int MascaraNoMostrar = (int)MascaraVisualizacionValidacionRegistroActor.NoMostrar;
        public readonly int MascaraOpcional = (int)MascaraVisualizacionValidacionRegistroActor.Opcional;
        public readonly int MascaraObligatorio = (int)MascaraVisualizacionValidacionRegistroActor.Obligatorio;
        
    }

    public sealed class ConfiguracionSelectorActorComercial
    {
        public int IdEmpleadoPorDefecto;
        public readonly int IdClientePorDefecto = ActorSettings.Default.IdClienteGenerico;
        public readonly int IdProveedorPorDefecto = ActorSettings.Default.idProveedorGenerico;
        public readonly int IdRolCliente = ActorSettings.Default.IdRolCliente;
        public readonly int IdRolProveedor = ActorSettings.Default.IdRolProveedor;
        public readonly int IdRolEmpleado = ActorSettings.Default.IdRolEmpleado;
    }
}

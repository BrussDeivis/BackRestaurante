using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace Tsp.Sigescom.Modelo.Entidades.Custom
{
    public class OrdenAlmacenResumen
    {
        private Parametro_transaccion aliasOrigenDestino;    
        private int idActorInterno;
        private string establecimientoActorInterno;
        private string nombreActorInterno;
        private string tipoDocumentoActorInterno;
        private string documentoActorInterno;
        private int idActorExterno;
        private string establecimientoActorExterno;
        private string nombreActorExterno;
        private string tipoDocumentoActorExterno;
        private string documentoActorExterno;

        public int IdActorInterno { set => idActorInterno = value; }
        public string EstablecimientoActorInterno { set => establecimientoActorInterno = value; }
        public string NombreActorInterno { set => nombreActorInterno = value; }
        public string TipoDocumentoActorInterno { set => tipoDocumentoActorInterno = value; }
        public string DocumentoActorInterno { set => documentoActorInterno = value; }
        public int IdActorExterno { set => idActorExterno = value; }
        public string EstablecimientoActorExterno { set => establecimientoActorExterno = value; }
        public string NombreActorExterno { set => nombreActorExterno = value; }
        public string TipoDocumentoActorExterno { set => tipoDocumentoActorExterno = value; }
        public string DocumentoActorExterno { set => documentoActorExterno = value; }

        public long Id { get; set; }
        public DateTime Fecha { get; set; }
        public string FechaEmision { get => Fecha.ToString("dd/MM/yyyy"); }
        public int IdAlmacen { get => EsBidireccional ? (PorIngresar? idActorExterno : idActorInterno) : idActorInterno;  }
        public string Establecimiento { get => EsBidireccional ? (PorIngresar ? establecimientoActorExterno : establecimientoActorInterno) : establecimientoActorInterno; } 
        public string Almacen { get => EsBidireccional ? (PorIngresar ? nombreActorExterno : nombreActorInterno) : nombreActorInterno; }
        public string TipoDocumento { get; set; }
        public string SerieNumeroDocumento { get; set; }
        public int IdTipoOperacion { get; set; }
        public string TipoOperacion { get; set; }
        public string Ordenante { get; set; }
        public int IdOrigenDestino { get => EsBidireccional ? (PorIngresar ? idActorInterno : idActorExterno) : idActorExterno; }
    public string DocumentoOrigenDestino { get => EsBidireccional ? (PorIngresar ? (tipoDocumentoActorInterno + ": " + documentoActorInterno) : (tipoDocumentoActorExterno + ": " + documentoActorExterno)) : (tipoDocumentoActorExterno + ": " + documentoActorExterno); }
        public string NombreOrigenDestino { get => EsBidireccional ? (PorIngresar ? nombreActorInterno : nombreActorExterno) : nombreActorExterno; }
        public Parametro_transaccion AliasOrigenDestino { set => aliasOrigenDestino = value; }
        public string OrigenDestino { get => DocumentoOrigenDestino + " - " + (aliasOrigenDestino == null ? NombreOrigenDestino : aliasOrigenDestino.valor); }
        public int IdModoEntrega { get; set; }
        public string ModoEntrega { get => Enumerado.GetDescription((IndicadorImpactoAlmacen)IdModoEntrega); }
        public bool EsDiferida { get => IdModoEntrega == (int)IndicadorImpactoAlmacen.Diferida; }
        public int IdEstado { get; set; }
        public string Estado { get; set; }
        public bool EsBidireccional { get; set; }
        public bool PorIngresar { get; set; }
        public bool PuedeRegistrarMovimiento { get => IdEstado == MaestroSettings.Default.IdDetalleMaestroEstadoPendiente || IdEstado == MaestroSettings.Default.IdDetalleMaestroEstadoParcial; }

    }
}

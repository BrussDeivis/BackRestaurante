using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo.ClasesNegocio.Core.Facturacion
{
    public class Atencion
    {
        public long Id { get; set; }
        public string Codigo { get; set; }
        public IEnumerable<OrdenAtencion> Ordenes { get; set; }
        public OrdenAtencion OrdenPrincipal { get; set; }
        public decimal Importe { get => (TieneFacturacion ? 0 : OrdenPrincipal.Importe) + (Ordenes == null ? 0 : Ordenes.Where(o => o.Id != OrdenPrincipal.Id && !o.TieneFacturacion).Sum(d => d.Importe)); }
        public bool TieneFacturacion { get; set; }
        public IEnumerable<ComprobanteFacturado> ComprobantesOrden { get => ComprobantesOrdenPrincipal == null ? (ComprobantesOrdenSecundario ?? null) : (ComprobantesOrdenSecundario == null ? ComprobantesOrdenPrincipal : ComprobantesOrdenPrincipal.Union(ComprobantesOrdenSecundario)); }
        public IEnumerable<ComprobanteFacturado> ComprobantesOrdenPrincipal { get; set; }
        public IEnumerable<ComprobanteFacturado> ComprobantesOrdenSecundario { get; set; }
        public IEnumerable<ComprobanteFacturado> ComprobantesReferencia { get => ComprobantesReferenciaPrincipal == null ? (ComprobantesReferenciaSecundario ?? null) : (ComprobantesReferenciaSecundario == null ? ComprobantesReferenciaPrincipal : ComprobantesReferenciaPrincipal.Union(ComprobantesReferenciaSecundario)); }
        public IEnumerable<ComprobanteFacturado> ComprobantesReferenciaPrincipal { get; set; }
        public IEnumerable<ComprobanteFacturado> ComprobantesReferenciaSecundario { get; set; }
        public IEnumerable<ComprobanteFacturado> ComprobantesFacturados { get => ComprobantesOrden == null ? (ComprobantesReferencia ?? null) : (ComprobantesReferencia == null ? ComprobantesOrdenPrincipal : ComprobantesOrden.Union(ComprobantesReferencia)); }
         
        public List<DatosVentaIntegrada> NuevosComprobantes { get; set; }
        public int TipoDePago { get; set; }
        public Atencion() { }

    }

    public class ComprobanteFacturado
    {
        public long IdOrden { get; set; }
        public long Id { get => IdOrden; }
        public string CadenaHtmlDeComprobante80 { get; set; }
        public string CadenaHtmlDeComprobanteA4 { get; set; }
        public ComprobanteFacturado() { }

    }
}

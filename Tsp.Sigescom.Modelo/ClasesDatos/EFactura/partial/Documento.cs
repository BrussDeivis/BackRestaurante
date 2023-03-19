using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Sigescom.Modelo.Entidades.EFactura
{
    public partial class Documento
    {
        
        public Documento(long idSigescom, string codigoTipoComprobante, string serieComprobante, string numeroComprobante, string fechaEmision,string tipoComprobante, int estado, int estadoSigescom , byte[] archivo)
        {
            this.idSigescom = idSigescom;
            this.codigoTipoComprobante = codigoTipoComprobante;
            this.serieComprobante = serieComprobante;
            this.numeroComprobante = numeroComprobante;
            this.fechaEmision = DateTime.Parse(fechaEmision);
            this.tipoComprobante = tipoComprobante;
            this.estado = estado;
            this.estadoSigescom = estadoSigescom;
            this.Binario = new Binario();
            this.Binario.archivoBinario = archivo;
        }

        public string ComprobanteDocumento()
        {
            return serieComprobante + "-" + numeroComprobante;
        }
    }
}

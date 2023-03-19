//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tsp.Sigescom.Modelo.Entidades.EFactura
{
    using System;
    using System.Collections.Generic;
    
    public partial class Documento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Documento()
        {
            this.EnvioDocumento = new HashSet<EnvioDocumento>();
        }
    
        public long id { get; set; }
        public long idSigescom { get; set; }
        public string codigoTipoComprobante { get; set; }
        public string serieComprobante { get; set; }
        public string numeroComprobante { get; set; }
        public System.DateTime fechaEmision { get; set; }
        public long idBinarioDocumento { get; set; }
        public string tipoComprobante { get; set; }
        public int estado { get; set; }
        public int estadoSigescom { get; set; }
        public bool enviado { get; set; }
    
        public virtual Binario Binario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EnvioDocumento> EnvioDocumento { get; set; }
    }
}

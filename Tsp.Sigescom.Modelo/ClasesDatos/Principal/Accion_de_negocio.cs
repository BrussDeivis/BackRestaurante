//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tsp.Sigescom.Modelo.Entidades
{
    using System;
    using System.Collections.Generic;
    
    public partial class Accion_de_negocio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Accion_de_negocio()
        {
            this.Accion_de_negocio_por_tipo_transaccion = new HashSet<Accion_de_negocio_por_tipo_transaccion>();
        }
    
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Accion_de_negocio_por_tipo_transaccion> Accion_de_negocio_por_tipo_transaccion { get; set; }
    }
}

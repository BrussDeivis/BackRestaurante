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
    
    public partial class Atributo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Atributo()
        {
            this.Atributo_actor = new HashSet<Atributo_actor>();
        }
    
        public int id { get; set; }
        public string nombre { get; set; }
        public string regexp { get; set; }
        public Nullable<int> id_rol { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Atributo_actor> Atributo_actor { get; set; }
        public virtual Rol Rol { get; set; }
    }
}

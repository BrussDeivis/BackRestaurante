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
    
    public partial class Rol
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Rol()
        {
            this.Accion_por_rol = new HashSet<Accion_por_rol>();
            this.Actor_negocio = new HashSet<Actor_negocio>();
            this.Actor_negocio_por_transaccion = new HashSet<Actor_negocio_por_transaccion>();
            this.Actor_negocio_rol = new HashSet<Actor_negocio_rol>();
            this.Atributo = new HashSet<Atributo>();
            this.Concepto_negocio_rol = new HashSet<Concepto_negocio_rol>();
            this.Parametro_por_rol = new HashSet<Parametro_por_rol>();
            this.Registro_sucesos = new HashSet<Registro_sucesos>();
            this.Rol_por_unidad_negocio = new HashSet<Rol_por_unidad_negocio>();
            this.Rol1 = new HashSet<Rol>();
            this.Rol_por_tipo_actor = new HashSet<Rol_por_tipo_actor>();
        }
    
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public Nullable<bool> es_predeterminado { get; set; }
        public Nullable<int> id_rol_padre { get; set; }
        public Nullable<int> aplica_a { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Accion_por_rol> Accion_por_rol { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Actor_negocio> Actor_negocio { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Actor_negocio_por_transaccion> Actor_negocio_por_transaccion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Actor_negocio_rol> Actor_negocio_rol { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Atributo> Atributo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Concepto_negocio_rol> Concepto_negocio_rol { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Parametro_por_rol> Parametro_por_rol { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Registro_sucesos> Registro_sucesos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rol_por_unidad_negocio> Rol_por_unidad_negocio { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rol> Rol1 { get; set; }
        public virtual Rol Rol2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rol_por_tipo_actor> Rol_por_tipo_actor { get; set; }
    }
}

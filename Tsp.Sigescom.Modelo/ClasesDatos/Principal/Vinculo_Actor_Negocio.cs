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
    
    public partial class Vinculo_Actor_Negocio
    {
        public int id { get; set; }
        public int id_actor_negocio_principal { get; set; }
        public int id_actor_negocio_vinculado { get; set; }
        public System.DateTime desde { get; set; }
        public System.DateTime hasta { get; set; }
        public string descripcion { get; set; }
        public int tipo_vinculo { get; set; }
        public bool es_vigente { get; set; }
    
        public virtual Actor_negocio Actor_negocio { get; set; }
        public virtual Actor_negocio Actor_negocio1 { get; set; }
    }
}

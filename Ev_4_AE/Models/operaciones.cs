//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ev_4_AE.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class operaciones
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public operaciones()
        {
            this.operacion_rol = new HashSet<operacion_rol>();
        }
    
        public int id { get; set; }
        public string nombre { get; set; }
        public int moduloId { get; set; }
    
        public virtual modulo modulo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<operacion_rol> operacion_rol { get; set; }
    }
}
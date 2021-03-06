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
    
    public partial class OrdenTrabajo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrdenTrabajo()
        {
            this.AvanceOrdenTrabajo = new HashSet<AvanceOrdenTrabajo>();
        }
    
        public int id { get; set; }
        public string tipo { get; set; }
        public string descripcion { get; set; }
        public int equipoId { get; set; }
        public int usuarioId { get; set; }
        public System.DateTime fecha { get; set; }
        public Nullable<bool> estado { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AvanceOrdenTrabajo> AvanceOrdenTrabajo { get; set; }
        public virtual Equipo Equipo { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
